using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.User;

namespace KyntoProtocol.Game.ChatCommands
{
    /// <summary>
    /// Interface for a chat command.
    /// </summary>
    public interface IChatCommand
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        string Command
        {
            get;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <param name="FullMessage">The full message sent.</param>
        /// <returns>True for success.</returns>
        bool Execute(IUser UserInstance, string FullMessage);
    }
}
