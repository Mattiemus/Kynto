using System;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Rooms;

namespace KyntoLib.Interfaces.Services
{
    /// <summary>
    /// Handles logging of debug data and other information.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Initialises the logging service.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Called after the thread service has initialised, tells the logging service to start the write thread.
        /// </summary>
        void InitialiseThread();

        /// <summary>
        /// Exits the logging service, closing and finalising all logs.
        /// </summary>
        void Exit();

        /// <summary>
        /// Writes a line too the server log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        void Write(LogImportance Importance, string Message, Exception ExceptionThrown);

        /// <summary>
        /// Writes a line too the database log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        void WriteDatabase(LogImportance Importance, string Message, Exception ExceptionThrown);

        /// <summary>
        /// Writes a line too the room log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="RoomId">The id of the room.</param>
        /// <param name="RoomType">The rooms type.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        void WriteRoom(LogImportance Importance, int RoomId, RoomType RoomType, string Message, Exception ExceptionThrown);

        /// <summary>
        /// Writes a line to the users log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="UserId">The users id.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        void WriteMember(LogImportance Importance, int UserId, string Message, Exception ExceptionThrown);

        /// <summary>
        /// Writes a line to the packets log.
        /// </summary>
        /// <param name="ConnectionId">The connection id.</param>
        /// <param name="Direction">The packet direction.</param>
        /// <param name="PacketType">The type of packet.</param>
        /// <param name="Packet">The packet.</param>
        void WritePacket(int ConnectionId, PacketDirection Direction, PacketType PacketType, string Packet);
    }

    /// <summary>
    /// The message importance.
    /// </summary>
    public enum LogImportance
    {
        /// <summary>
        /// The log message was sent by the core server.
        /// </summary>
        Server,

        /// <summary>
        /// The log message is an initialisation message.
        /// </summary>
        Initialise,

        /// <summary>
        /// The log message is a normal logging message.
        /// </summary>
        Normal,

        /// <summary>
        /// The log message is an error.
        /// </summary>
        Error,

        /// <summary>
        /// The log message is a debugging message.
        /// </summary>
        Debug
    }

    /// <summary>
    /// The direction of the packet.
    /// </summary>
    public enum PacketDirection
    {
        /// <summary>
        /// Inbound.
        /// </summary>
        In,

        /// <summary>
        /// Outbound.
        /// </summary>
        Out
    }

    /// <summary>
    /// The type of packet.
    /// </summary>
    public enum PacketType
    {
        /// <summary>
        /// Game packet.
        /// </summary>
        Game,

        /// <summary>
        /// Backend packet.
        /// </summary>
        Backend
    }
}
