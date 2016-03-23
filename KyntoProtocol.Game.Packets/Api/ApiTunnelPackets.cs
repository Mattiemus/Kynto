using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The packet defining an api tunnel message.
    /// </summary>
    public class ApiTunnelPacket
    {
        /// <summary>
        /// The users to send the packet too.
        /// </summary>
        public string[] T;

        /// <summary>
        /// The message to send.
        /// </summary>
        public string M;
    }

    /// <summary>
    /// The packet defining an api tunnel message.
    /// </summary>
    public class ApiTunnelPacketResponse
    {
        /// <summary>
        /// The username where this message originates from.
        /// </summary>
        public string F;

        /// <summary>
        /// The message data.
        /// </summary>
        public string M;
    }
}
