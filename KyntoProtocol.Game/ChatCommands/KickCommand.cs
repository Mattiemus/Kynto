using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Rooms;

namespace KyntoProtocol.Game.ChatCommands
{
    /// <summary>
    /// The room kick command.
    /// </summary>
    public class KickCommand : IChatCommand
    {
        /// <summary>
        /// Gets the command this handles.
        /// </summary>
        public string Command
        {
            get
            {
                return "/kick";
            }
        }

        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this command.
        /// </summary>
        /// <param name="ServerInstance">The main server instance.</param>
        public KickCommand(IServerManager ServerInstance)
        {
            // Store the server info.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <param name="FullMessage">The full message sent.</param>
        /// <returns>True for success.</returns>
        public bool Execute(IUser UserInstance, string FullMessage)
        {
            // Toggle users brb.
            // Make sure the user is online and in a room.
            if (UserInstance.UserData.IsLoggedIn)
            {
                // Check user is in a room.
                if (UserInstance.Avatar.CurrentRoom != null && ((UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (UserInstance.UserData.UsersRank.CanKickUserFromPrivateRoom || ((IPrivateRoom)UserInstance.Avatar.CurrentRoom).GetRightsForUser(UserInstance).CanKickUser)) || (UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PublicRoom && (UserInstance.UserData.UsersRank.CanKickUserFromPublicRoom))))
                {
                    // Find user(s).
                    List<IUser> UsersToKick = new List<IUser>();
                    for (int i = 0; i < UserInstance.Avatar.CurrentRoom.UsersInRoom.Count; i++)
                    {
                        if (UserInstance.Avatar.CurrentRoom.UsersInRoom[i].Avatar.Username.ToLowerInvariant().Contains(FullMessage.Substring(Command.Length + 1).ToLowerInvariant()))
                        {
                            UsersToKick.Add(UserInstance.Avatar.CurrentRoom.UsersInRoom[i]);
                        }
                    }

                    // Kick user(s).
                    for (int i = 0; i < UsersToKick.Count; i++)
                    {
                        UsersToKick[i].Avatar.CurrentRoom.KickUser(UsersToKick[i]);
                    }

                    // Finish.
                    return true;
                }
            }

            // User not logged in, error.
            return false;
        }
    }
}
