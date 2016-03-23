using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// Sends a message box
    /// </summary>
    public class MessageBoxPacket
    {
        /// <summary>
        /// The message to send.
        /// </summary>
        public string M;

        /// <summary>
        /// The message title.
        /// </summary>
        public string T;

        /// <summary>
        /// The message type.
        /// </summary>
        public string I;
    }
}
