using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles navigator related packets.
    /// </summary>
    public class NavigatorPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public NavigatorPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the request navigator packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RequestNavigatorPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn)
            {
                // Retrieve the login request.
                RequestNavigator PacketModel = JSON.DeSerialize<RequestNavigator>(Params.PacketBody);

                // Make sure the packet is correctly filled in!
                if (PacketModel != null)
                {
                    // Find out the type of request.
                    switch (PacketModel.T)
                    {
                        // Standard request - return all available rooms.
                        case 0:
                            {
                                // Create the navigator response.
                                NavigatorPacket NavigatorPacket = new NavigatorPacket();
                                NavigatorPacket.T = PacketModel.T;

                                // Get the publics.
                                List<IDatabaseTable> PublicsTables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Publics)
                                    .Select()
                                    .Limit(10)
                                    .ExecuteRead();
                                if (PublicsTables != null)
                                {
                                    IPublicsDatabaseTable[] PublicsTablesAsArray = new IPublicsDatabaseTable[PublicsTables.Count];
                                    for (int i = 0; i < PublicsTables.Count; i++)
                                    {
                                        PublicsTablesAsArray[i] = (IPublicsDatabaseTable)PublicsTables[i];
                                    }
                                    NavigatorPacket.Pu = PublicRoomDataPacket.FromArray(PublicsTablesAsArray, this._ServerManager);
                                }

                                // Get the privates.
                                List<IDatabaseTable> PrivatesTables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Rooms)
                                    .Select()
                                    .Limit(10)
                                    .ExecuteRead();
                                if (PrivatesTables != null)
                                {
                                    IRoomsDatabaseTable[] PrivatesTablesAsArray = new IRoomsDatabaseTable[PrivatesTables.Count];
                                    for (int i = 0; i < PrivatesTables.Count; i++)
                                    {
                                        PrivatesTablesAsArray[i] = (IRoomsDatabaseTable)PrivatesTables[i];
                                    }
                                    NavigatorPacket.Pr = PrivateRoomDataPacket.FromArray(PrivatesTablesAsArray, this._ServerManager);
                                }

                                // Create string
                                string NavigatorPacketString = JSON.Serializer<NavigatorPacket>(NavigatorPacket);
                                // Reply!
                                Params.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.NavigatorData + NavigatorPacketString + ProtocolInfo.Terminator, true);

                                // Exit this switch statement and method!
                                return;
                            }

                        // My rooms request - return all the users rooms.
                        case 1:
                            {
                                // Make sure the user is not a guest.
                                if (!Params.UserInstance.UserData.IsGuest)
                                {
                                    // Create the navigator response.
                                    NavigatorPacket NavigatorPacket = new NavigatorPacket();
                                    NavigatorPacket.T = PacketModel.T;

                                    // Get the privates.
                                    List<IDatabaseTable> PrivatesTables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Rooms)
                                        .Select()
                                        .Where(RoomsTableFields.OwnerId, DatabaseComparison.Equals, Params.UserInstance.UserData.UserInfo.Id)
                                        .Limit(10)
                                        .ExecuteRead();
                                    if (PrivatesTables != null)
                                    {
                                        IRoomsDatabaseTable[] PrivatesTablesAsArray = new IRoomsDatabaseTable[PrivatesTables.Count];
                                        for (int i = 0; i < PrivatesTables.Count; i++)
                                        {
                                            PrivatesTablesAsArray[i] = (IRoomsDatabaseTable)PrivatesTables[i];
                                        }
                                        NavigatorPacket.Pr = PrivateRoomDataPacket.FromArray(PrivatesTablesAsArray, this._ServerManager);
                                    }

                                    // Create string
                                    string NavigatorPacketString = JSON.Serializer<NavigatorPacket>(NavigatorPacket);
                                    // Reply!
                                    Params.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.NavigatorData + NavigatorPacketString + ProtocolInfo.Terminator, true);
                                }

                                // Exit this switch statement and method!
                                return;
                            }

                        // Search request - search for a room.
                        case 2:
                            {
                                // Create the navigator response.
                                NavigatorPacket NavigatorPacket = new NavigatorPacket();
                                NavigatorPacket.T = PacketModel.T;

                                // Get the publics.
                                List<IDatabaseTable> PublicsTables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Publics)
                                    .Select()
                                    .Where(PublicsTableFields.Name, DatabaseComparison.Equals, PacketModel.Q)
                                    .Limit(10)
                                    .ExecuteRead();
                                if (PublicsTables != null)
                                {
                                    IPublicsDatabaseTable[] PublicsTablesAsArray = new IPublicsDatabaseTable[PublicsTables.Count];
                                    for (int i = 0; i < PublicsTables.Count; i++)
                                    {
                                        PublicsTablesAsArray[i] = (IPublicsDatabaseTable)PublicsTables[i];
                                    }
                                    NavigatorPacket.Pu = PublicRoomDataPacket.FromArray(PublicsTablesAsArray, this._ServerManager);
                                }

                                // Get the privates.
                                List<IDatabaseTable> PrivatesTables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Rooms)
                                    .Select()
                                    .Where(RoomsTableFields.Name, DatabaseComparison.Equals, PacketModel.Q)
                                    .Limit(10)
                                    .ExecuteRead();
                                if (PrivatesTables != null)
                                {
                                    IRoomsDatabaseTable[] PrivatesTablesAsArray = new IRoomsDatabaseTable[PrivatesTables.Count];
                                    for (int i = 0; i < PrivatesTables.Count; i++)
                                    {
                                        PrivatesTablesAsArray[i] = (IRoomsDatabaseTable)PrivatesTables[i];
                                    }
                                    NavigatorPacket.Pr = PrivateRoomDataPacket.FromArray(PrivatesTablesAsArray, this._ServerManager);
                                }

                                // Create string
                                string NavigatorPacketString = JSON.Serializer<NavigatorPacket>(NavigatorPacket);
                                // Reply!
                                Params.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.NavigatorData + NavigatorPacketString + ProtocolInfo.Terminator, true);

                                // Exit this switch statement and method!
                                return;
                            }
                    }
                }
            }
        }
    }
}
