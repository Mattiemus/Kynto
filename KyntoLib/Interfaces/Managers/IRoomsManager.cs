using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms;

namespace KyntoLib.Interfaces.Managers
{
    /// <summary>
    /// Manages all active rooms.
    /// </summary>
    public interface IRoomsManager
    {
        /// <summary>
        /// Gets the active list of public rooms.
        /// </summary>
        Dictionary<int, IRoom> PublicRooms { get; }

        /// <summary>
        /// Gets the active list of private rooms.
        /// </summary>
        Dictionary<int, IRoom> PrivateRooms { get; }

        /// <summary>
        /// Initialises this rooms manager.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Exits the rooms manager, stopping all rooms.
        /// </summary>
        void Exit();

        /// <summary>
        /// Makes the specified user instance to join a room.
        /// </summary>
        /// <param name="UserInstance">The user instance to add too the room.</param>
        /// <param name="UserConnection">The users connection.</param>
        /// <param name="Entrytile">The entrytile to add the user too.</param>
        /// <param name="RoomId">The rooms id.</param>
        /// <param name="RoomType">The rooms type.</param>
        IRoom JoinRoom(IUser UserInstance, IGameSocketConnection UserConnection, string Entrytile, int RoomId, int RoomType);

        /// <summary>
        /// Adds a room too the room arrays.
        /// </summary>
        /// <param name="Room">The room instance too add.</param>
        /// <param name="Type">The type of room.</param>
        void AddRoom(IRoom Room, RoomType Type);

        /// <summary>
        /// Remove a room.
        /// </summary>
        /// <param name="RoomID">The room to remove.</param>
        /// <param name="Type">The rooms type.</param>
        void RemoveRoom(IRoom Room, RoomType Type);
    }
}
