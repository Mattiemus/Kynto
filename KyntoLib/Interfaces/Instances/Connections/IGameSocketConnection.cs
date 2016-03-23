using System;
using System.Net.Sockets;

namespace KyntoLib.Interfaces.Instances.Connections
{
    /// <summary>
    /// Represents a game socket connection.
    /// </summary>
    public interface IGameSocketConnection
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
        /// <param name="Encrypt">Weather the data should be encrypted.</param>
        void SendData(string Data, bool Encrypt);

        /// <summary>
        /// Disconnect this socket.
        /// </summary>
        void Disconnect();
    }
}
