using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Managers;

using KyntoServer.Instances.Rooms;

namespace KyntoServer.Managers
{
    /// <summary>
    /// Manages all active rooms.
    /// </summary>
    public class RoomsManager : IRoomsManager
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the list of public rooms.
        /// </summary>
        private Dictionary<int, IRoom> _PublicRooms = new Dictionary<int, IRoom>();

        /// <summary>
        /// Gets the active list of public rooms.
        /// </summary>
        public Dictionary<int, IRoom> PublicRooms
        {
            get
            {
                return this._PublicRooms;
            }
        }

        /// <summary>
        /// Stores the list of private rooms.
        /// </summary>
        private Dictionary<int, IRoom> _PrivateRooms = new Dictionary<int, IRoom>();

        /// <summary>
        /// Gets the active list of private rooms.
        /// </summary>
        public Dictionary<int, IRoom> PrivateRooms
        {
            get
            {
                return this._PrivateRooms;
            }
        }

        /// <summary>
        /// Initialises this rooms manager.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Exits the rooms manager, stopping all rooms.
        /// </summary>
        public void Exit()
        {
        }

        /// <summary>
        /// Makes the specified user instance to join a room.
        /// </summary>
        /// <param name="UserInstance">The user instance to add too the room.</param>
        /// <param name="UserConnection">The users connection.</param>
        /// <param name="Entrytile">The entrytile to add the user too.</param>
        /// <param name="RoomId">The rooms id.</param>
        /// <param name="RoomTypeInteger">The rooms type.</param>
        public IRoom JoinRoom(IUser UserInstance, IGameSocketConnection UserConnection, string Entrytile, int RoomId, int RoomTypeInteger)
        {
            // Find out the type of room.
            if (RoomTypeInteger == (int)RoomType.PrivateRoom)
            {
                // Private room.
                if (!this._PrivateRooms.ContainsKey(RoomId))
                {
                    // Attempt to find it.
                    List<IDatabaseTable> Tables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Rooms)
                        .Select()
                        .Where(RoomsTableFields.Id, DatabaseComparison.Equals, RoomId)
                        .ExecuteRead();

                    // Attempt to join room.
                    if (Tables != null && Tables.Count == 1 && !String.IsNullOrEmpty(((IRoomsDatabaseTable)Tables[0]).Name))
                    {
                        // Create new room.
                        PrivateRoom Room = new PrivateRoom(this._ServerManager, RoomId);

                        // Add it (this.AddRoom).
                        AddRoom(Room, RoomType.PrivateRoom);

                        // Add user.
                        Room.AddUser(UserInstance, UserConnection, Entrytile);

                        // Return
                        return Room;
                    }
                    else
                    {
                        // Error.
                        return null;
                    }
                }
                else
                {
                    // Grab from array and return.
                    IRoom RoomInstance = this._PrivateRooms[RoomId];
                    RoomInstance.AddUser(UserInstance, UserConnection, Entrytile);
                    return RoomInstance;
                }
            }
            else if (RoomTypeInteger == (int)RoomType.PublicRoom)
            {
                // Public room.
                if (!this._PublicRooms.ContainsKey(RoomId))
                {
                    // Attempt to find it.
                    List<IDatabaseTable> Tables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Publics)
                        .Select()
                        .Where(PublicsTableFields.Id, DatabaseComparison.Equals, RoomId)
                        .ExecuteRead();

                    // Attempt to join room.
                    if (Tables != null && Tables.Count == 1 && !String.IsNullOrEmpty(((IPublicsDatabaseTable)Tables[0]).Name))
                    {
                        // Create new room.
                        PublicRoom Room = new PublicRoom(this._ServerManager, RoomId);
                        
                        // Add it (this.AddRoom).
                        AddRoom(Room , RoomType.PublicRoom);

                        // Add user.
                        Room.AddUser(UserInstance, UserConnection, Entrytile);

                        // Return
                        return Room;
                    }
                    else
                    {
                        // Error.
                        return null;
                    }
                }
                else
                {
                    // Grab from array and return.
                    IRoom RoomInstance = this._PublicRooms[RoomId];
                    RoomInstance.AddUser(UserInstance, UserConnection, Entrytile);
                    return RoomInstance;
                }
            }
            else
            {
                // Error.
                return null;
            }
        }

        /// <summary>
        /// Adds a room too the room arrays.
        /// </summary>
        /// <param name="Room">The room instance too add.</param>
        /// <param name="Type">The type of room.</param>
        public void AddRoom(IRoom Room, RoomType Type)
        {
            // Adds a room instance.
            if (Type == RoomType.PrivateRoom)
            {
                this._PrivateRooms.Add(((IPrivateRoom)Room).DatabaseTable.Id, Room);
            }
            else if (Type == RoomType.PublicRoom)
            {
                this._PublicRooms.Add(((IPublicRoom)Room).DatabaseTable.Id, Room);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Remove a room.
        /// </summary>
        /// <param name="RoomID">The room to remove.</param>
        /// <param name="Type">The rooms type.</param>
        public void RemoveRoom(IRoom Room, RoomType Type)
        {
            // Removes a room.
            if (Type == RoomType.PrivateRoom)
            {
                this._PrivateRooms.Remove(((IPrivateRoom)Room).DatabaseTable.Id);
            }
            else if (Type == RoomType.PublicRoom)
            {
                this._PublicRooms.Remove(((IPublicRoom)Room).DatabaseTable.Id);
            }
            else
            {
                return;
            }
        }
    }
}
