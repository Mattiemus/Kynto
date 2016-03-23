using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Helpers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles connection related packets.
    /// </summary>
    public class ConnectionPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public ConnectionPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles a disconnect request.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void DisconnectRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Disconnect the connection.
            Params.GameSocketConnection.Disconnect();
        }

        /// <summary>
        /// Handles a login request.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void LoginRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is not already logged in.
            if (!Params.UserInstance.UserData.IsLoggedIn)
            {
                // Retrieve the login request.
                LoginRequestPacket PacketModel = JSON.DeSerialize<LoginRequestPacket>(Params.PacketBody);

                // Make sure the packet is correctly filled in!
                if (PacketModel != null)
                {
                    if (PacketModel.U == "guest")
                    {
                        // Call guest login event.
                        this._ServerManager.EventsManager.CallEvent(EventType.GuestLoginAccepted, this, new GuestLoginAcceptedEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance });
                    }
                    else
                    {
                        // Attempt to get the users data.
                        List<IDatabaseTable> Tables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Members)
                            .Select()
                            .Where(MembersTableFields.Username, DatabaseComparison.Equals, PacketModel.U)
                            .Limit(1)
                            .ExecuteRead();

                        // Check the data.
                        if (Tables == null || Tables.Count != 1)
                        {
                            // Incorrect information.
                            // Fire login failed event.
                            this._ServerManager.EventsManager.CallEvent(EventType.LoginFailed, this, new LoginFailedEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance, FailureReason = "wrong" });
                        }
                        else
                        {
                            // We have 1 record.
                           IMembersDatabaseTable UsersInfo = (IMembersDatabaseTable)Tables[0];

                            if (UsersInfo.Username != PacketModel.U || UsersInfo.Password != PacketModel.P)
                            {
                                // Incorrect information.
                                // Fire login failed event.
                                this._ServerManager.EventsManager.CallEvent(EventType.LoginFailed, this, new LoginFailedEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance, FailureReason = "wrong" });
                            }
                            else if (!UsersInfo.Activated)
                            {
                                // Account not activated.
                                // Fire login failed event.
                                this._ServerManager.EventsManager.CallEvent(EventType.LoginFailed, this, new LoginFailedEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance, FailureReason = "mail" });
                            }
                            else
                            {
                                if (UsersInfo.Ban)
                                {
                                    // Account is banned!
                                    // Get ban data.
                                    IMembersBanDatabaseTable BanTable = ((IMembersBanDatabaseTable)this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.MemberBan)
                                        .Select()
                                        .Where(MembersBanTableFields.MemberId, DatabaseComparison.Equals, UsersInfo.Id)
                                        .Limit(1)
                                        .ExecuteRead()[0]);

                                    // Get rid of ban if possible.
                                    if ((BanTable.Created + BanTable.Length) <= Timestamp.Now)
                                    {
                                        // Remove the ban.
                                        this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.MemberBan)
                                            .Delete()
                                            .Where(MembersBanTableFields.MemberId, DatabaseComparison.Equals, UsersInfo.Id)
                                            .Execute();

                                        // Update the user data.
                                        UsersInfo.Ban = false;
                                        UsersInfo.Update().Execute();
                                    }
                                    else
                                    {
                                        // Fire login failed event.
                                        this._ServerManager.EventsManager.CallEvent(EventType.LoginFailed, this, new LoginFailedEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance, FailureReason = "ban", BanReason = BanTable.Reason, BanEndTimestamp = BanTable.Created + BanTable.Length });

                                        // Cant do anything, exit.
                                        return;
                                    }
                                }

                                // Call login event.
                                this._ServerManager.EventsManager.CallEvent(EventType.UserLoginAccepted, this, new UserLoginAcceptedEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance, UsersInfo = UsersInfo });
                            }
                        }
                    }
                }
            }
        }
    }
}
