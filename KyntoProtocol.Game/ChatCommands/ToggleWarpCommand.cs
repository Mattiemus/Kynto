using System;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;

namespace KyntoProtocol.Game.ChatCommands
{
    /// <summary>
    /// The toggle warp command.
    /// </summary>
    public class ToggleWarpCommand : IChatCommand
    {
        /// <summary>
        /// Gets the command this handles.
        /// </summary>
        public string Command
        {
            get
            {
                return "/warp";
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
        public ToggleWarpCommand(IServerManager ServerInstance)
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
                if (UserInstance.Avatar.CurrentRoom != null && UserInstance.UserData.UsersRank.CanNoCheckTeleport)
                {
                    // Toggle teleport.
                    if (UserInstance.Avatar.MovementMethod == MovementMethod.NoCheckWarp)
                    {
                        UserInstance.Avatar.MovementMethod = MovementMethod.Walk;
                    }
                    else
                    {
                        UserInstance.Avatar.MovementMethod = MovementMethod.NoCheckWarp;
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
