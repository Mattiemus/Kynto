using System;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Data;

namespace KyntoProtocol.Game.ChatCommands
{
    /// <summary>
    /// The toggle brb command.
    /// </summary>
    public class ToggleBrbCommand : IChatCommand
    {
        /// <summary>
        /// Gets the command this handles.
        /// </summary>
        public string Command
        {
            get
            {
                return "/brb";
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
        public ToggleBrbCommand(IServerManager ServerInstance)
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
                if (UserInstance.Avatar.CurrentRoom != null)
                {
                    // Toggle brb status.
                    UserInstance.UserData.ActivatedCommands.Brb = !UserInstance.UserData.ActivatedCommands.Brb;
                    UserInstance.Avatar.IsGhost = UserInstance.UserData.ActivatedCommands.Brb;

                    // Fire the avatar updated event.
                    this._ServerManager.EventsManager.CallEvent(EventType.AvatarUpdated, this, new GameSocketsEventArguments() { GameSocketConnection = (IGameSocketConnection)UserInstance, UserInstance = UserInstance });
                }
            }

            // Cannot fail - always return true.
            return true;
        }
    }
}
