using System;
using System.Collections.Generic;
using System.Reflection;

namespace KyntoLib.Interfaces.Managers
{
    /// <summary>
    /// Manages and loads plugins into the server.
    /// </summary>
    public interface IPluginsManager
    {
        /// <summary>
        /// Gets the list of loaded assemblies.
        /// </summary>
        List<Assembly> LoadedAssemblys { get; }

        /// <summary>
        /// Initialises this plugins manager, and loads all available plugins.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Exits the plugins manager.
        /// </summary>
        void Exit();
    }
}
