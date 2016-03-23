using System;

using KyntoLib.Interfaces.Managers;

namespace KyntoLib.Interfaces.Plugins
{
    /// <summary>
    /// A plugin is a service that provides extended functionality for the server.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the plugin's name.
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Initialises the plugin.
        /// </summary>
        /// <param name="ServerManager">The parent server manager.</param>
        void Initialise(IServerManager ServerManager);

        /// <summary>
        /// Shuts down the plugin.
        /// </summary>
        void Exit();
    }
}
