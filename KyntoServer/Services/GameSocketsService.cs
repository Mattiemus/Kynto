using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

using KyntoServer.Instances.Connections;

namespace KyntoServer.Services
{
    /// <summary>
    /// Handles the game sockets.
    /// </summary>
    public class GameSocketsService : IGameSocketsService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the socket handler.
        /// </summary>
        private Socket _SocketHandler;

        /// <summary>
        /// Stores the connected clients.
        /// </summary>
        private HashSet<int> _ConnectedClients = new HashSet<int>();

        /// <summary>
        /// Gets the number of active connections.
        /// </summary>
        public int ActiveConnections
        {
            get
            {
                return this._ConnectedClients.Count;
            }
        }

        /// <summary>
        /// Stores the number of bytes downloaded.
        /// </summary>
        private long _DownloadedBytes = 0;

        /// <summary>
        /// Gets or sets the number of bytes downloaded.
        /// </summary>
        public long DownloadedBytes
        {
            get
            {
                return this._DownloadedBytes;
            }
            set
            {
                this._DownloadedBytes = value;
            }
        }

        /// <summary>
        /// Stores the number of bytes uploaded.
        /// </summary>
        private long _UploadedBytes = 0;

        /// <summary>
        /// Gets or sets the number of bytes uploaded.
        /// </summary>
        public long UploadedBytes
        {
            get
            {
                return this._UploadedBytes;
            }
            set
            {
                this._UploadedBytes = value;
            }
        }

        /// <summary>
        /// Stores the recently received policy file requests.
        /// </summary>
        private List<RecentPolicyRequest> _RecentPolicyRequests = new List<RecentPolicyRequest>();

        /// <summary>
        /// Gets the received policy file requests.
        /// </summary>
        public List<RecentPolicyRequest> RecentPolicyRequests
        {
            get
            {
                return this._RecentPolicyRequests;
            }
        }

        /// <summary>
        /// Initialises the back end sockets.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server.
            this._ServerManager = ServerInstance;

            // Create our sockets.
            this._SocketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Try to bind.
            try
            {
                // Bind.
                this._SocketHandler.Bind(new IPEndPoint(IPAddress.Any, int.Parse(this._ServerManager.SettingsService.GetValue("Network.Game.BindPort"))));
                this._SocketHandler.Listen(int.Parse(this._ServerManager.SettingsService.GetValue("Network.Game.MaxConnectionsBackLog")));
                this._SocketHandler.BeginAccept(new AsyncCallback(ConnectionRequest), _SocketHandler);

                // Write that we have started...
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Game sockets service initialised.", null);
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Listening on port " + int.Parse(this._ServerManager.SettingsService.GetValue("Network.Game.BindPort")), null);
            }
            catch (Exception Ex)
            {
                // Write that we failed.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "Game sockets failed to initialise!", null);
                this._ServerManager.Exit(Ex, "Failed to bind sockets.");
            }
        }

        /// <summary>
        /// Exits the game sockets service, closing all game sockets.
        /// </summary>
        public void Exit()
        {
        }

        /// <summary>
        /// Called when we have a connection request.
        /// </summary>
        /// <param name="Resault">The asynchronous result.</param>
        private void ConnectionRequest(IAsyncResult Resault)
        {
            // Attempt to accept this connection.
            try
            {
                // Find a free connection.
                int ConnectionID = 0;
                for (int i = 1; i < int.MaxValue; i++)
                {
                    // Loop until we find a free connection.
                    if (this._ConnectedClients.Contains(i) == false)
                    {
                        ConnectionID = i;
                        break;
                    }
                }

                // If the connection id isn't 0 we have found a free connection.
                if (ConnectionID > 0)
                {
                    // Snap out our socket.
                    Socket ConnectionSocket = ((Socket)Resault.AsyncState).EndAccept(Resault);

                    // Create our game socket user.
                    IGameSocketConnection SocketConnection = new GameSocketConnection(ConnectionID, ConnectionSocket, this._ServerManager);

                    // Store this connection.
                    this._ConnectedClients.Add(ConnectionID);
                }
            }
            catch (Exception Ex)
            {
                // Write the error to the log.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "Error during game sockets connection request.", Ex);
            }

            // Finally begin accepting connections again.
            this._SocketHandler.BeginAccept(new AsyncCallback(ConnectionRequest), this._SocketHandler);
        }

        /// <summary>
        /// Frees the connection with the specified id.
        /// </summary>
        /// <param name="Connection">The connection id to free.</param>
        public void FreeConnection(int Connection)
        {
            // If this service contains the connection, free it.
            if (this._ConnectedClients.Contains(Connection))
            {
                // Remove the connection.
                this._ConnectedClients.Remove(Connection);
                // Write that we released the connection.
                this._ServerManager.LoggingService.Write(LogImportance.Normal, "Freed game sockets connection id " + Connection + ".", null);
                return;
            }

            // Failed to find connection...
            this._ServerManager.LoggingService.Write(LogImportance.Error, "Failed to free game sockets connection id " + Connection + ".", null);
        }
    }
}
