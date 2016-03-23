using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Managers;

namespace KyntoLib.Interfaces.Services
{
    /// <summary>
    /// Handles the game sockets.
    /// </summary>
    public interface IGameSocketsService
    {
        /// <summary>
        /// Gets the recived policy file requests.
        /// </summary>
        List<RecentPolicyRequest> RecentPolicyRequests { get; }

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
        /// Exits the game sockets service, closing all game sockets.
        /// </summary>
        void Exit();

        /// <summary>
        /// Frees the connection with the specified id.
        /// </summary>
        /// <param name="Connection">The connection id to free.</param>
        void FreeConnection(int Connection);
    }

    /// <summary>
    /// Represents a recived policy request.
    /// </summary>
    public struct RecentPolicyRequest
    {
        /// <summary>
        /// Stores the ip of the user that sent this request.
        /// </summary>
        public string IP;

        /// <summary>
        /// Stores the timestamp of when this request was received.
        /// </summary>
        public DateTime Timestamp;
    }
}
