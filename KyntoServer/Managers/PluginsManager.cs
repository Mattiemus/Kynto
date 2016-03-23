using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Plugins;

namespace KyntoServer.Managers
{
    /// <summary>
    /// Manages and loads plugins into the server.
    /// </summary>
    public class PluginsManager : IPluginsManager
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the list of loaded assemblies.
        /// </summary>
        private List<Assembly> _LoadedAssemblys = new List<Assembly>();

        /// <summary>
        /// Gets the list of loaded assemblies.
        /// </summary>
        public List<Assembly> LoadedAssemblys
        {
            get
            {
                return this._LoadedAssemblys;
            }
        }

        /// <summary>
        /// Initialises this plugins manager, and loads all available plugins.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server.
            this._ServerManager = ServerInstance;

            // Create our lists of plugin directories and search patterns.
            int NumPluginTypes = 4;
            int[] PluginsLoaded = new int[NumPluginTypes];
            string[] PluginTypeNames = new string[] { "database libraries", "game protocol libraries", "backend protocol libraries", "plugin libraries" };
            string[] PluginDirectorys = new string[] { Program.BaseDirectory + "Databases/", Program.BaseDirectory + "Protocols/Game/", Program.BaseDirectory + "Protocols/Backend/", Program.BaseDirectory + "Plugins/" };
            string[] PluginSearchPatterns = new string[] { "KyntoDatabase.*.dll", "KyntoProtocol.Game.dll", "KyntoProtocol.Backend.dll", "KyntoPlugin.*.dll" };

            // Load in all assemblies in the specified directory.
            for (int i = 0; i < NumPluginTypes; i++)
            {
                int NumPlugins = 0;
                DirectoryInfo DirectoryInfo = new DirectoryInfo(PluginDirectorys[i]);
                FileInfo[] DirectoryFiles = DirectoryInfo.GetFiles(PluginSearchPatterns[i]);
                foreach (FileInfo FileInfo in DirectoryFiles)
                {
                    // Load in the assembly.
                    this._LoadedAssemblys.Add(Assembly.LoadFrom(FileInfo.FullName));
                    NumPlugins++;
                }

                PluginsLoaded[i] = NumPlugins;
            }

            // Write to the log.
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Plugins manager initialised.", null);
            for (int i = 0; i < NumPluginTypes; i++)
            {
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded \"" + PluginsLoaded[i] + "\" " + PluginTypeNames[i] + ".", null);
            }

            // Initialise the loaded plugins.
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Initialising plugins.", null);

            // Load all event handlers.
            foreach (Assembly Assembly in this._LoadedAssemblys)
            {
                foreach (Type Type in Assembly.GetTypes())
                {
                    foreach (Type InterfaceType in Type.GetInterfaces())
                    {
                        // Check for plugin.
                        if (InterfaceType == typeof(IPlugin))
                        {
                            // Create a copy.
                            IPlugin Plugin = ((IPlugin)Type.GetConstructor(new Type[] { }).Invoke(new object[] { }));
                            
                            // Initialise it.
                            this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Initialising \"" + Plugin.PluginName + "\" plugin.", null);
                            Plugin.Initialise(this._ServerManager);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Exits the plugins manager.
        /// </summary>
        public void Exit()
        {
        }
    }
}
