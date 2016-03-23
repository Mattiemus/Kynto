using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Managers;

namespace KyntoServer.Services
{
    /// <summary>
    /// Handles the database and provides the database handler.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the default database handler.
        /// </summary>
        private IDatabaseInterface _Database;

        /// <summary>
        /// Gets the default database handler.
        /// </summary>
        public IDatabaseInterface Database
        {
            get
            {
                return this._Database;
            }
        }

        /// <summary>
        /// Initialises the database service and should load in the appropreate database service.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;

            // Load all event handlers.
            foreach (Assembly Assembly in this._ServerManager.PluginsManager.LoadedAssemblys)
            {
                foreach (Type Type in Assembly.GetTypes())
                {
                    foreach (Type InterfaceType in Type.GetInterfaces())
                    {
                        // Check for database handler interface.
                        if (InterfaceType == typeof(IDatabaseInterface))
                        {
                            // Create a copy.
                            IDatabaseInterface DatabaseHandlerInstance = ((IDatabaseInterface)Type.GetConstructor(new Type[] { typeof(IServerManager) }).Invoke(new object[] { this._ServerManager }));
                            //Check its the correct type to load.
                            if (DatabaseHandlerInstance.DatabaseType == this._ServerManager.SettingsService.GetValue("Database.Connector"))
                            {
                                // Write that we have initialised.
                                this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Database service initialised.", null);

                                if (DatabaseHandlerInstance.Connect())
                                {
                                    // Store it!
                                    this._Database = DatabaseHandlerInstance;

                                    // Write that we have initialised.
                                    this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Database connected using \"" + DatabaseHandlerInstance.DatabaseType + "\".", null);
                                    return;
                                }
                                else
                                {
                                    // Could not connect to database.
                                    this._ServerManager.LoggingService.Write(LogImportance.Error, "Failed to connect using database connector \"" + this._ServerManager.SettingsService.GetValue("Database.Connector") + "\".", null);
                                    this._ServerManager.Exit(null, "Failed to connect using database connector \"" + this._ServerManager.SettingsService.GetValue("Database.Connector") + "\".");
                                }
                            }
                        }
                    }
                }
            }

            // Could not load database.
            this._ServerManager.LoggingService.Write(LogImportance.Error, "Failed to find database connector \"" + this._ServerManager.SettingsService.GetValue("Database.Connector") + "\".", null);
            this._ServerManager.Exit(null, "Failed to find database connector \"" + this._ServerManager.SettingsService.GetValue("Database.Connector") + "\".");
        }

        /// <summary>
        /// Exits the database service, closing all databases.
        /// </summary>
        public void Exit()
        {
            // Disconnect database.
            if (this._Database != null)
            {
                this._Database.Disconnect();
                this._ServerManager.LoggingService.Write(LogImportance.Normal, "Database service shutdown, disconnected database.", null);
            }
        }
    }
}
