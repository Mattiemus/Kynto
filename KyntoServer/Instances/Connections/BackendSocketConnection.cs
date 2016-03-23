using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Events;
using KyntoLib.Helpers;
using KyntoLib.Encryption;

using KyntoServer.Instances.User;
using KyntoServer.Instances.Rooms.Avatars;

namespace KyntoServer.Instances.Connections
{
    /// <summary>
    /// Represents a backend socket connection.
    /// </summary>
    public class BackendSocketConnection : IBackendSocketConnection
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the socket id.
        /// </summary>
        private int _SocketId;

        /// <summary>
        /// Gets this socket connection id.
        /// </summary>
        public int SocketId
        {
            get
            {
                return this._SocketId;
            }
        }

        /// <summary>
        /// Stores the socket instance.
        /// </summary>
        private Socket _SocketInstance;

        /// <summary>
        /// Gets the socket instance.
        /// </summary>
        public Socket SocketInstance
        {
            get
            {
                return this._SocketInstance;
            }
        }

        /// <summary>
        /// Stores weather or not this user has disconnected.
        /// </summary>
        private bool _HasDisconnected = false;

        /// <summary>
        /// Store our data buffer.
        /// </summary>
        private byte[] _DataBuffer = new byte[(1024 * 16)];

        /// <summary>
        /// Initialises this backend socket connection.
        /// </summary>
        /// <param name="SocketId">The socket id of this connection.</param>
        /// <param name="Socket">The socket that this client is connected via.</param>
        /// <param name="ServerInstance">The server instance.</param>
        public BackendSocketConnection(int SocketId, Socket Socket, IServerManager ServerInstance)
        {
            // Store the values.
            this._SocketId = SocketId;
            this._SocketInstance = Socket;
            this._ServerManager = ServerInstance;

            // Attempt to begin receiving from the socket.
            try
            {
                // Start listening.
                this._SocketInstance.BeginReceive(this._DataBuffer, 0, this._DataBuffer.Length, SocketFlags.None, new AsyncCallback(this.DataArrival), null);
                // Write to the log that we are now accepting connections.
                this._ServerManager.LoggingService.Write(LogImportance.Normal, "[Backend][" + this._SocketId + "] New user connected.", null);

                // Call the user connected event handler.
                this._ServerManager.EventsManager.CallEvent(EventType.BackendNewConnection, this, new BackendSocketsEventArguments() { BackendSocketConnection = this });
            }
            catch (Exception Ex)
            {
                // Write the error too the log.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "[Backend][" + this._SocketId + "] Error during creation.", Ex);
                // Disconnect the client due to error.
                Disconnect();
            }
        }

        /// <summary>
        /// Sends a string of data to the client.
        /// </summary>
        /// <param name="Data">The data to send.</param>
        public void SendData(string Data)
        {
            // If we have not disconnected send the data. 
            if (!this._HasDisconnected)
            {
                // This may be volatile - catch any errors that may be thrown.
                try
                {
                    // Log this being sent.
                    this._ServerManager.LoggingService.Write(LogImportance.Normal, "[Backend][" + this._SocketId + "] >> " + Data, null);

                    // Convert the data to send too a byte array.
                    byte[] DataBytes = System.Text.Encoding.ASCII.GetBytes(Data);
                    // Add the terminator null byte.
                    DataBytes = ArrayHelper.AddToArray<byte>(DataBytes, 0);

                    // Increment the number of uploaded bytes.
                    this._ServerManager.GameSocketsService.UploadedBytes += DataBytes.Length;

                    // Send the data.
                    this._SocketInstance.BeginSend(DataBytes, 0, DataBytes.Length, 0, new AsyncCallback(SentData), null);
                }
                catch (Exception Ex)
                {
                    // Write that we have encountered an error.
                    this._ServerManager.LoggingService.Write(LogImportance.Error, "[Backend][" + this._SocketId + "] Error during SendDataRaw", Ex);
                    this.Disconnect();
                }
            }
        }

        /// <summary>
        /// Send the data.
        /// </summary>
        /// <param name="Result">The asynchronous result.</param>
        private void SentData(IAsyncResult Result)
        {
            // Attempt this as it may throw exceptions.
            try
            {
                // Finish sending.
                this._SocketInstance.EndSend(Result);
            }
            catch (Exception Ex)
            {
                // Write the error.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "[Backend][" + this._SocketId + "] Error during SentData", Ex);
                this.Disconnect();
            }
        }

        /// <summary>
        /// Disconnect this socket.
        /// </summary>
        public void Disconnect()
        {
            // If we have not already d/c'ed disconnect.
            if (!this._HasDisconnected)
            {
                // Destroy our socket.
                if (this._SocketInstance != null)
                {
                    try
                    {
                        this._SocketInstance.Shutdown(SocketShutdown.Both);
                        this._SocketInstance.Close();
                    }
                    catch
                    {
                    }
                }

                // Free our connection.
                this._ServerManager.BackendSocketsService.FreeConnection(this._SocketId);

                // Store that we have d/ced
                this._HasDisconnected = true;

                // Fire the disconnected event.
                this._ServerManager.EventsManager.CallEvent(EventType.BackendLostConnection, this, null);
            }
        }

        /// <summary>
        /// Called when we receive data.
        /// </summary>
        /// <param name="Result">The data we received.</param>
        private void DataArrival(IAsyncResult Result)
        {
            // Attempt to read from our socket.
            try
            {
                // Get the amount of bytes received.
                int BytesReceived = 0;
                BytesReceived = this._SocketInstance.EndReceive(Result);

                // If we received some data, get its value.
                if (BytesReceived != 0)
                {
                    // Increment number of bytes received.
                    this._ServerManager.GameSocketsService.DownloadedBytes += BytesReceived;

                    // Get the string received.
                    string ConnectionData = Encoding.ASCII.GetString(this._DataBuffer, 0, BytesReceived - 1);

                    // Call the received data event.
                    this._ServerManager.EventsManager.CallEvent(EventType.BackendRecivedPacket, this, new BackendPacketReceivedEventArguments() { BackendSocketConnection = this, RecivedData = ConnectionData });

                    // If we still have a connection, being receiving again.
                    if (this._SocketInstance != null && this._SocketInstance.Connected)
                    {
                        this._SocketInstance.BeginReceive(this._DataBuffer, 0, this._DataBuffer.Length, SocketFlags.None, new AsyncCallback(this.DataArrival), null);
                    }
                    else
                    {
                        Disconnect();
                    }
                }
                else
                {
                    // Disconnect due to null data received.
                    Disconnect();
                }
            }
            catch (Exception Ex)
            {
                // Write that we failed.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "[Backend][" + this._SocketId + "] Error during DataArrival", Ex);
                // Disconnect due to an error.
                Disconnect();
            }
        }
    }
}
