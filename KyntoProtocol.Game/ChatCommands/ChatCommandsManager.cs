using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.User;

namespace KyntoProtocol.Game.ChatCommands
{
    /// <summary>
    /// Manages all chat commands.
    /// </summary>
    public class ChatCommandsManager
    {
        /// <summary>
        /// List of chat commands.
        /// </summary>
        private Dictionary<string, IChatCommand> _Commands;

        /// <summary>
        /// Initialises the list of chat commands.
        /// </summary>
        public ChatCommandsManager(IServerManager ServerManager)
        {
            _Commands = new Dictionary<string, IChatCommand>();

            foreach (Assembly Asmbly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type Type in Asmbly.GetTypes())
                {
                    foreach (Type InterfaceType in Type.GetInterfaces())
                    {
                        if (InterfaceType == typeof(IChatCommand))
                        {
                            IChatCommand Command = (IChatCommand)Type.GetConstructor(new Type[] { typeof(IServerManager) }).Invoke(new object[] { ServerManager });
                            _Commands.Add(Command.Command, Command);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <param name="FullMessage">The full message sent.</param>
        /// <returns>True for success.</returns>
        public bool Execute(IUser UserInstance, string FullMessage)
        {
            foreach (KeyValuePair<string, IChatCommand> CommandPair in _Commands)
            {
                if (FullMessage.StartsWith(CommandPair.Key) && CommandPair.Value.Execute(UserInstance, FullMessage))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
