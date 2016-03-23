using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Helpers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.EventHandlers
{
    /// <summary>
    /// Handles connection related events.
    /// </summary>
    public class ConnectionEventHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public ConnectionEventHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the user connected event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void UserConnectedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GameSocketsEventArguments EventArguments = (GameSocketsEventArguments)Args;

            // If we are in production mode check if we have a policy request for this user.
            if (!bool.Parse(this._ServerManager.SettingsService.GetValue("DevMode")))
            {
                // Store if we found the policy file request for this use.
                bool FoundPreviousRequest = false;

                // Store the items to remove.
                List<RecentPolicyRequest> ToRemove = new List<RecentPolicyRequest>();
                List<RecentPolicyRequest> CurrentItems = this._ServerManager.GameSocketsService.RecentPolicyRequests;

                // Check through all recent policy requests.
                foreach (RecentPolicyRequest RecentPolicyRequest in CurrentItems)
                {
                    // If we recently received a policy request from this ip, remove it.
                    if (RecentPolicyRequest.IP == EventArguments.GameSocketConnection.SocketInstance.RemoteEndPoint.ToString().Split(':')[0] && (DateTime.Now - RecentPolicyRequest.Timestamp).TotalSeconds <= 10)
                    {
                        ToRemove.Add(RecentPolicyRequest);
                        FoundPreviousRequest = true;
                    }

                    // Remove old policy requests.
                    if ((DateTime.Now - RecentPolicyRequest.Timestamp).TotalSeconds > 10)
                    {
                        ToRemove.Add(RecentPolicyRequest);
                    }
                }

                // Remove old policy file requests.
                foreach (RecentPolicyRequest RecentPolicyRequest in ToRemove)
                {
                    this._ServerManager.GameSocketsService.RecentPolicyRequests.Remove(RecentPolicyRequest);
                }

                // If we have not found a request, don't send connection ok - 
                // the user probably wants there policy file!.
                if (!FoundPreviousRequest)
                {
                    // Exit.
                    return;
                }
            }

            // Create a copy of the server version packet.
            ServerVersionPacket ServerVersionPacket = new ServerVersionPacket();
            ServerVersionPacket.V = this._ServerManager.SettingsService.GetValue("Server.Version");
            string ServerVersionPacketString = JSON.Serializer<ServerVersionPacket>(ServerVersionPacket);
            // Send it too the user who just connected.
            EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.ConnectionOK + ProtocolInfo.Terminator + ProtocolInfo.ServerPackets.ServerVersion + ServerVersionPacketString + ProtocolInfo.Terminator, true);
        }

        /// <summary>
        /// Handles the user disconnected event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void UserDisconnectedEventHandler(object Sender, EventArgs Args)
        {
        }

        /// <summary>
        /// Handles the user data updated event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void UserDataUpdatedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GameSocketsEventArguments EventArguments = (GameSocketsEventArguments)Args;

            // Respond with this users data.
            UserDataPacket UserDataPacket = new UserDataPacket();
            UserDataPacket.C = EventArguments.UserInstance.Avatar.Clothes;
            UserDataPacket.M = EventArguments.UserInstance.Avatar.Motto;
            UserDataPacket.UN = EventArguments.UserInstance.Avatar.Username;
            UserDataPacket.S = EventArguments.UserInstance.Avatar.Sex;
            UserDataPacket.BS = (EventArguments.UserInstance.UserData.IsGuest) ? 0 : EventArguments.UserInstance.UserData.UserInfo.BlocksSilver;
            UserDataPacket.ID = (EventArguments.UserInstance.UserData.IsGuest) ? null : FurnictureArrayPacket.FromArray(EventArguments.UserInstance.InventoryService.ToArray());
            UserDataPacket.IG = EventArguments.UserInstance.UserData.IsGuest;
            // Convert to string.
            string UserDataString = JSON.Serializer<UserDataPacket>(UserDataPacket);
            // Send the data.
            EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.UserData + UserDataString + ProtocolInfo.Terminator, true);
        }

        /// <summary>
        /// Handles the login successful event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void LoginSuccessfulEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GameSocketsEventArguments EventArguments = (GameSocketsEventArguments)Args;

            // Fire user data updated.
            this._ServerManager.EventsManager.CallEvent(EventType.UserDataUpdated, this, new GameSocketsEventArguments() { GameSocketConnection = EventArguments.GameSocketConnection, UserInstance = EventArguments.UserInstance });

            // Store login time.
            EventArguments.UserInstance.UserData.LoggedInAt = DateTime.Now;

            // Send the data.
            EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.LoginAccepted + ProtocolInfo.Terminator, true);
        }

        /// <summary>
        /// Handles the login failed event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void LoginFailedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            LoginFailedEventArguments EventArguments = (LoginFailedEventArguments)Args;

            // Create the response.
            LoginFailurePacket LoginFailureMessage = new LoginFailurePacket();
            LoginFailureMessage.R = EventArguments.FailureReason;
            LoginFailureMessage.M = EventArguments.BanReason;
            LoginFailureMessage.T = EventArguments.BanEndTimestamp;
            string LoginMessageString = JSON.Serializer<LoginFailurePacket>(LoginFailureMessage);
            // Reply!
            EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.LoginFailed + LoginMessageString + ProtocolInfo.Terminator, true);
        }

        /// <summary>
        /// Handles the guest login accepted event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void GuestLoginAcceptedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GuestLoginAcceptedEventArguments EventArguments = (GuestLoginAcceptedEventArguments)Args;

            // Login ok!
            // Store this users avatar info.
            EventArguments.UserInstance.Avatar.UserId = 0;
            EventArguments.UserInstance.Avatar.Username = "Guest";
            EventArguments.UserInstance.Avatar.Motto = "I am new!";
            EventArguments.UserInstance.Avatar.Sex = "m";
            EventArguments.UserInstance.Avatar.Clothes = new AvatarClothes() { body = 1, hat = 0, hair = 2, face = 2, top = 4, pants = 2, accessories = 0, shoes = 0 };
            EventArguments.UserInstance.Avatar.Badge = 0;

            // Set user to logged in.
            EventArguments.UserInstance.UserData.IsGuest = true;
            EventArguments.UserInstance.UserData.IsLoggedIn = true;

            // Make the user always a ghost.
            EventArguments.UserInstance.Avatar.IsGhost = true;

            // Fire login successful event.
            this._ServerManager.EventsManager.CallEvent(EventType.GuestLoginSuccessful, this, new GameSocketsEventArguments() { GameSocketConnection = EventArguments.GameSocketConnection, UserInstance = EventArguments.UserInstance });
        }

        /// <summary>
        /// Handles the user login accepted event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void UserLoginAcceptedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            UserLoginAcceptedEventArguments EventArguments = (UserLoginAcceptedEventArguments)Args;

            // Login ok!
            // Store this users avatar info.
            EventArguments.UserInstance.Avatar.UserId = EventArguments.UsersInfo.Id;
            EventArguments.UserInstance.Avatar.Username = EventArguments.UsersInfo.Username;
            EventArguments.UserInstance.Avatar.Motto = EventArguments.UsersInfo.Motto;
            EventArguments.UserInstance.Avatar.Sex = EventArguments.UsersInfo.Sex;
            EventArguments.UserInstance.Avatar.Clothes = JSON.DeSerialize<AvatarClothes>(EventArguments.UsersInfo.Clothes);
            EventArguments.UserInstance.Avatar.Badge = EventArguments.UsersInfo.ActiveBadge;

            // Store users info.
            EventArguments.UserInstance.UserData.UserInfo = EventArguments.UsersInfo;
            EventArguments.UserInstance.UserData.UsersRank = this._ServerManager.UsersManager.Rights[EventArguments.UserInstance.UserData.UserInfo.RankId];

            // Load the users available commands.
            List<IDatabaseTable> AvaliableCommands = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.MembersCommands)
                .Select()
                .Where(MembersCommandsTableFields.MemberId, DatabaseComparison.Equals, EventArguments.UserInstance.UserData.UserInfo.Id)
                .Limit(1)
                .ExecuteRead();
            if (AvaliableCommands != null && AvaliableCommands.Count == 1)
            {
                EventArguments.UserInstance.UserData.UsersCommands.Parse((IMembersCommandsDatabaseTable)AvaliableCommands[0]);
            }

            // Load the list of clothes.
            EventArguments.UserInstance.UserData.UsersClothes.AddRange(this._ServerManager.CatalogueManager.GetClothesByRank(EventArguments.UserInstance.UserData.UserInfo.RankId));
            // Add the custom clothes.
            List<IDatabaseTable> Clothes = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.MembersClothes)
                .Select()
                .Where(MembersClothesTableFields.MemberId, DatabaseComparison.Equals, EventArguments.UserInstance.UserData.UserInfo.Id)
                .Limit(1)
                .ExecuteRead();
            if (Clothes != null && Clothes.Count == 1)
            {
                // Get the record.
                string[] MembersClothes = ((IMembersClothesDatabaseTable)Clothes[0]).Clothes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // Add the clothes by id.
                for (int i = 0; i < MembersClothes.Length; i++)
                {
                    EventArguments.UserInstance.UserData.UsersClothes.Add(this._ServerManager.CatalogueManager.Clothes[int.Parse(MembersClothes[i])]);
                }
            }

            // Load the list of badges this user has access too.
            List<IDatabaseTable> Badges = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.MembersBadges)
                .Select()
                .Where(MembersBadgesTableFields.MemberId, DatabaseComparison.Equals, EventArguments.UserInstance.UserData.UserInfo.Id)
                .Limit(1)
                .ExecuteRead();
            if (Badges != null && Badges.Count == 1)
            {
                // Get the record.
                string[] MembersBadges = ((IMembersBadgesDatabaseTable)Badges[0]).Badges.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int[] MembersBadgesInts = new int[MembersBadges.Length];

                // Convert all the records.
                for (int i = 0; i < MembersBadges.Length; i++)
                {
                    MembersBadgesInts[i] = int.Parse(MembersBadges[i]);
                }

                // Store.
                EventArguments.UserInstance.UserData.UsersBadges.AddRange(MembersBadgesInts);
            }

            // Update last login time and ip.
            EventArguments.UserInstance.UserData.UserInfo.LastOnline = (int)Timestamp.Now;
            EventArguments.UserInstance.UserData.UserInfo.Ip = ((IPEndPoint)EventArguments.GameSocketConnection.SocketInstance.RemoteEndPoint).Address.ToString();
            EventArguments.UserInstance.UserData.UserInfo.Update().Execute();

            // Set user to logged in.
            EventArguments.UserInstance.UserData.IsGuest = false;
            EventArguments.UserInstance.UserData.IsLoggedIn = true;

            // Initialise the users inventory service.
            EventArguments.UserInstance.InventoryService.Initialise();

            // Add the user to the users manager.
            this._ServerManager.UsersManager.AddUser(EventArguments.UserInstance);

            // Fire login successful event.
            this._ServerManager.EventsManager.CallEvent(EventType.UserLoginSuccessful, this, new GameSocketsEventArguments() { GameSocketConnection = EventArguments.GameSocketConnection, UserInstance = EventArguments.UserInstance });

            // Write to log.
            this._ServerManager.LoggingService.WriteMember(LogImportance.Normal, EventArguments.UserInstance.UserData.UserInfo.Id, "User logged in", null);

            // Initialise the users pda messenger.
            EventArguments.UserInstance.PdaService.Initialise();
        }
    }
}
