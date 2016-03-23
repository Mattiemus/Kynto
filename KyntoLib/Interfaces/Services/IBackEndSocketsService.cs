using System;

using KyntoLib.Interfaces.Managers;

namespace KyntoLib.Interfaces.Services
{
    /// <summary>
    /// Handles the back end sockets.
    /// </summary>
    public interface IBackEndSocketsService
    {
        /// <summary>
        /// Gets or sets the number of bytes downloaded.
        /// </summary>
        long DownloadedBytes { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes uploaded.
        /// </summary>
        long UploadedBytes { get; set; }

        /// <summary>
        /// Gets the number of active connections.
        /// </summary>
        int ActiveConnections { get; }

        /// <summary>
        /// Initialises the back end sockets.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Exits the backend sockets service, closing all sockets.
        /// </summary>
        void Exit();

        /// <summary>
        /// Frees the connection with the specified id.
        /// </summary>
        /// <param name="Connection">The connection id to free.</param>
        void FreeConnection(int Connection);
    }
}
