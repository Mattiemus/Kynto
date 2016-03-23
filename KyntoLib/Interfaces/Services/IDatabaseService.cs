using System;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Managers;

namespace KyntoLib.Interfaces.Services
{
    /// <summary>
    /// Handles the database and provides the database handler.
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Gets the default database handler.
        /// </summary>
        IDatabaseInterface Database { get; }

        /// <summary>
        /// Initialises the database service and should load in the appropreate database service.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Exits the database service, closing all databases.
        /// </summary>
        void Exit();
    }
}
