using System;

using KyntoLib.Interfaces.Services;

namespace KyntoLib.Interfaces.Managers
{
    /// <summary>
    /// Manages the entire server and holds all services and managers.
    /// </summary>
    public interface IServerManager
    {
        /// <summary>
        /// Gets the catalogue manager.
        /// </summary>
        ICatalogueManager CatalogueManager { get; }

        /// <summary>
        /// Gets the events manager.
        /// </summary>
        IEventsManager EventsManager { get; }

        /// <summary>
        /// Gets the plugins manager.
        /// </summary>
        IPluginsManager PluginsManager { get; }

        /// <summary>
        /// Gets the rooms manager.
        /// </summary>
        IRoomsManager RoomsManager { get; }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        IUsersManager UsersManager { get; }


        /// <summary>
        /// Gets the backend sockets service.
        /// </summary>
        IBackEndSocketsService BackendSocketsService { get; }

        /// <summary>
        /// Gets the database service.
        /// </summary>
        IDatabaseService DatabaseService { get; }

        /// <summary>
        /// Gets the game sockets service.
        /// </summary>
        IGameSocketsService GameSocketsService { get; }

        /// <summary>
        /// Gets the logging service.
        /// </summary>
        ILoggingService LoggingService { get; }

        /// <summary>
        /// Gets the settings service
        /// </summary>
        ISettingsService SettingsService { get; }

        /// <summary>
        /// Gets the thread service.
        /// </summary>
        IThreadService ThreadService { get; }

        /// <summary>
        /// Initialises the server, setting up and loading all required resources, servers and managers.
        /// </summary>
        /// <param name="Arguments">Any command line arguments passed to us.</param>
        void Initialise(string[] Arguments);

        /// <summary>
        /// Stops the server.
        /// </summary>
        /// <param name="Ex">The exception that caused the server to stop (if any).</param>
        /// <param name="Reason">The reason for the server stopping.</param>
        void Exit(Exception Ex, string Reason);
    }
}
