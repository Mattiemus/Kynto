using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles room maker related packets.
    /// </summary>
    public class RoomMakerPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public RoomMakerPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }
    }
}
