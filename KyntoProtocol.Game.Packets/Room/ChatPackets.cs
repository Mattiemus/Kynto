using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The send message packet.
    /// </summary>
    public class SendMessagePacket
    {
        public string T;
        public string M;
    }

    /// <summary>
    /// The user sent message packet.
    /// </summary>
    public class SendMessageReplyPacket
    {
        /// <summary>
        /// The username of the user sending the message.
        /// </summary>
        public string U;

        /// <summary>
        /// The users id.
        /// </summary>
        public int I;

        /// <summary>
        /// The message to send.
        /// </summary>
        public string M;
    }

    /// <summary>
    /// The message spam packet.
    /// </summary>
    public class MessageSpamPacket
    {
        /// <summary>
        /// The time to wait until another message can be sent.
        /// </summary>
        public int T;
    }
}
