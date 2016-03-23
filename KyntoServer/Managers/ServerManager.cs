using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Events;

using KyntoServer.Services;

namespace KyntoServer.Managers
{
    /// <summary>
    /// Manages the entire server and holds all services and managers.
    /// </summary>
    public class ServerManager : IServerManager
    {
        /// <summary>
        /// Stores the catalogue manager.
        /// </summary>
        private ICatalogueManager _CatalogueManager;

        /// <summary>
        /// Gets the catalogue manager.
        /// </summary>
        public ICatalogueManager CatalogueManager
        {
            get
            {
                return this._CatalogueManager;
            }
        }

        /// <summary>
        /// Stores the events manager.
        /// </summary>
        private IEventsManager _EventsManager;

        /// <summary>
        /// Gets the events manager.
        /// </summary>
        public IEventsManager EventsManager
        {
            get
            {
                return this._EventsManager;
            }
        }

        /// <summary>
        /// Stores the plugins manager.
        /// </summary>
        private IPluginsManager _PluginsManager;

        /// <summary>
        /// Gets the plugins manager.
        /// </summary>
        public IPluginsManager PluginsManager
        {
            get
            {
                return this._PluginsManager;
            }
        }

        /// <summary>
        /// Stores the rooms manager;
        /// </summary>
        private IRoomsManager _RoomsManager;

        /// <summary>
        /// Gets the rooms manager.
        /// </summary>
        public IRoomsManager RoomsManager
        {
            get
            {
                return this._RoomsManager;
            }
        }

        /// <summary>
        /// Stores the users manager.
        /// </summary>
        private IUsersManager _UsersManager;

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public IUsersManager UsersManager
        {
            get
            {
                return this._UsersManager;
            }
        }

        /// <summary>
        /// Stores the backend sockets service.
        /// </summary>
        private IBackEndSocketsService _BackendSocketsService;

        /// <summary>
        /// Gets the backend sockets service.
        /// </summary>
        public IBackEndSocketsService BackendSocketsService
        {
            get
            {
                return this._BackendSocketsService;
            }
        }

        /// <summary>
        /// Stores the database service.
        /// </summary>
        private IDatabaseService _DatabaseService;

        /// <summary>
        /// Gets the database service.
        /// </summary>
        public IDatabaseService DatabaseService
        {
            get
            {
                return this._DatabaseService;
            }
        }

        /// <summary>
        /// Stores the game sockets service.
        /// </summary>
        private IGameSocketsService _GameSocketService;

        /// <summary>
        /// Gets the game sockets service.
        /// </summary>
        public IGameSocketsService GameSocketsService
        {
            get
            {
                return this._GameSocketService;
            }
        }

        /// <summary>
        /// Stores the logging service.
        /// </summary>
        private ILoggingService _LoggingService;

        /// <summary>
        /// Gets the logging service.
        /// </summary>
        public ILoggingService LoggingService
        {
            get
            {
                return this._LoggingService;
            }
        }

        /// <summary>
        /// Stores the settings service.
        /// </summary>
        private ISettingsService _SettingsService;

        /// <summary>
        /// Gets the settings service
        /// </summary>
        public ISettingsService SettingsService
        {
            get
            {
                return this._SettingsService;
            }
        }

        /// <summary>
        /// Stores the thread service.
        /// </summary>
        private IThreadService _ThreadService;

        /// <summary>
        /// Gets the thread service.
        /// </summary>
        public IThreadService ThreadService
        {
            get
            {
                return this._ThreadService;
            }
        }

        /// <summary>
        /// Initialises the server, setting up and loading all required resources, servers and managers.
        /// </summary>
        /// <param name="Arguments">Any command line arguments passed to us.</param>
        public void Initialise(string[] Arguments)
        {
            // Initialise services and managers.
            // Initialise logging.
            this._LoggingService = new LoggingService();
            this._LoggingService.Initialise(this);
            // Load settings.
            this._SettingsService = new SettingsService();
            this._SettingsService.Initialise(Arguments, this);
            // Initialise threading.
            this._ThreadService = new ThreadService();
            this._ThreadService.Initialise(this);
            // Initialise events.
            this._EventsManager = new EventsManager();
            this._EventsManager.Initialise(this);
            // Initialise plugins manager.
            this._PluginsManager = new PluginsManager();
            this._PluginsManager.Initialise(this);
            // Initialise database.
            this._DatabaseService = new DatabaseService();
            this._DatabaseService.Initialise(this);
            // Initialise users manager.
            this._UsersManager = new UsersManager();
            this._UsersManager.Initialise(this);
            // Initialise rooms manager.
            this._RoomsManager = new RoomsManager();
            this._RoomsManager.Initialise(this);
            // Initialise catalogue manager.
            this._CatalogueManager = new CatalogueManager();
            this._CatalogueManager.Initialise(this);
            // Initialise game sockets.
            this._GameSocketService = new GameSocketsService();
            this._GameSocketService.Initialise(this);
            // Initialise backend sockets.
            this._BackendSocketsService = new BackendSocketsService();
            this._BackendSocketsService.Initialise(this);

            // Finally write that we are ready.
            this._LoggingService.Write(LogImportance.Initialise, "Ready...", null);
            this._LoggingService.Write(LogImportance.Server, "", null);

            // Call that we have started our server.
            this._EventsManager.CallEvent(EventType.ServerStarted, this, null);
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        /// <param name="Ex">The exception that caused the server to stop (if any).</param>
        /// <param name="Reason">The reason for the server stopping.</param>
        public void Exit(Exception Ex, string Reason)
        {
            // Exit.
            // Write to the log that we are closing...
            this._LoggingService.Write(LogImportance.Server, "", null);
            this._LoggingService.Write(LogImportance.Server, "################################################################", null);
            this._LoggingService.Write(LogImportance.Server, "#                                                              #", null);
            this._LoggingService.Write(LogImportance.Server, "# Server exiting!                                              #", null);
            this._LoggingService.Write(LogImportance.Server, "#                                                              #", null);
            this._LoggingService.Write(LogImportance.Server, "################################################################", null);
            this._LoggingService.Write(LogImportance.Server, "", null);
            this._LoggingService.Write(LogImportance.Server, " Server is now closing, \"" + Reason + "\"", Ex);
            this._LoggingService.Write(LogImportance.Server, "", null);
            // Call the server exiting event.
            if (this._EventsManager != null) this._EventsManager.CallEvent(EventType.ServerExiting, this, null);
            // Close all servers, managers and services.
            if (this._GameSocketService != null) this._GameSocketService.Exit();
            if (this._BackendSocketsService != null) this._BackendSocketsService.Exit();
            if (this._EventsManager != null) this._EventsManager.Exit();
            if (this._PluginsManager != null) this._PluginsManager.Exit();
            if (this._UsersManager != null) this._UsersManager.Exit();
            if (this._RoomsManager != null) this._RoomsManager.Exit();
            if (this._CatalogueManager != null) this._CatalogueManager.Exit();
            if (this._DatabaseService != null) this._DatabaseService.Exit();
            if (this._ThreadService != null) this._ThreadService.Exit();
            if (this._SettingsService != null) this._SettingsService.Exit();
            if (this._LoggingService != null) this._LoggingService.Exit();
            // Its been fun...
            Environment.Exit(0);
        }
    }
}
