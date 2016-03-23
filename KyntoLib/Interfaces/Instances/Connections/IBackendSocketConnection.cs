using System;
using System.Net.Sockets;

namespace KyntoLib.Interfaces.Instances.Connections
{
    /// <summary>
    /// Represents a backend socket connection.
    /// </summary>
    public interface IBackendSocketConnection
    {
        /// <summary>
        /// Gets the socket instance.
        /// </summary>
        Socket SocketInstance { get; }

        /// <summary>
        /// Gets this socket connection id.
        /// </summary>
        int SocketId { get; }

        /// <summary>
        /// Sends a string of data to the client.
        /// </summary>
        /// <param name="Data">The data to send.</param>
        void SendData(string Data);

        /// <summary>
        /// Disconnect this socket.
        /// </summary>
        void Disconnect();
    }
}
