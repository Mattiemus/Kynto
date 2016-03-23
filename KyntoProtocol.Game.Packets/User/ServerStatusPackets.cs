using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The server version packet.
    /// </summary>
    public class ServerVersionPacket
    {
        /// <summary>
        /// The current server version.
        /// </summary>
        public string V;
    }

    /// <summary>
    /// The server status packet.
    /// </summary>
    public class ServerStatusPacket
    {
        /// <summary>
        /// The number of users online.
        /// </summary>
        public int UO;
    }
}
