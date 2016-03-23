using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Instances.Rooms.Bots;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;

namespace KyntoLib.Interfaces.Instances.Rooms
{
    /// <summary>
    /// Interfaces with a standard room.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Gets this rooms height map manager.
        /// </summary>
        IHeightmapManager HeightmapManger { get; }

        /// <summary>
        /// Gets this rooms furni manager.
        /// </summary>
        IFurniManager FurniManger { get; }

        /// <summary>
        /// Gets the lists of avatars in this room.
        /// </summary>
        List<IAvatar> AvatarsInRoom { get; }

        /// <summary>
        /// Gets the lists of users in this room.
        /// </summary>
        List<IUser> UsersInRoom { get; }

        /// <summary>
        /// Gets the lists of bots in this room.
        /// </summary>
        List<IBot> BotsInRoom { get; }

        /// <summary>
        /// Gets this rooms type.
        /// </summary>
        RoomType RoomType { get; }

        /// <summary>
        /// Gets this rooms id.
        /// </summary>
        int RoomId { get; }

        /// <summary>
        /// Gets if this room is active.
        /// </summary>
        bool RoomActive { get; }

        /// <summary>
        /// Adds a bot too the room.
        /// </summary>
        /// <param name="Bot">The bot too add.</param>
        void AddBot(IBot Bot);

        /// <summary>
        /// Adds a user to this room.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <param name="UserConnection">The users connection.</param>
        /// <param name="Entrytile">The requested entry tile.</param>
        void AddUser(IUser UserInstance, IGameSocketConnection UserConnection, string Entrytile);

        /// <summary>
        /// Adds an avatar too this room.
        /// </summary>
        /// <param name="Avatar">The avatar too add.</param>
        void AddAvatar(IAvatar Avatar);

        /// <summary>
        /// Removes an avatar from this room.
        /// </summary>
        /// <param name="Avatar">The avatar too remove.</param>
        void RemoveAvatar(IAvatar Avatar);

        /// <summary>
        /// Kicks a user from this room.
        /// </summary>
        /// <param name="User">The user to kick.</param>
        void KickUser(IUser User);

        /// <summary>
        /// Sends a chat message too the room.
        /// </summary>
        /// <param name="Avatar">The avatar sending the chat message.</param>
        /// <param name="ChatMessage">The chat message being sent.</param>
        void SendChat(IAvatar Avatar, string ChatMessage);

        /// <summary>
        /// Sends all the users in the room a piece of data.
        /// </summary>
        /// <param name="Data">The data too send</param>
        void SendData(string Data);

        /// <summary>
        /// Sends all the users in the room, except the socket not too send to, the piece of data.
        /// </summary>
        /// <param name="Data">The data to send.</param>
        /// <param name="NotToSendToo">Who not to send the data too.</param>
        void SendData(string Data, IGameSocketConnection NotToSendToo);
    }

    /// <summary>
    /// Represents a room type.
    /// </summary>
    public enum RoomType : int
    {
        /// <summary>
        /// Unknown room.
        /// </summary>
        None = -1,

        /// <summary>
        /// Private room.
        /// </summary>
        PrivateRoom = 1,

        /// <summary>
        /// Public room.
        /// </summary>
        PublicRoom = 2
    }
}
