using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.User.Data;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Events;

using KyntoServer.Instances.Rooms.Furni;
using KyntoServer.Instances.User.Data;

namespace KyntoServer.Instances.Rooms
{
    /// <summary>
    /// Handles the private room responsibilities.
    /// </summary>
    public class PrivateRoom : Room, IPrivateRoom
    {
        /// <summary>
        /// Stores this rooms database table.
        /// </summary>
        private IRoomsDatabaseTable _DatabaseTable;

        /// <summary>
        /// Gets this rooms database table.
        /// </summary>
        public IRoomsDatabaseTable DatabaseTable
        {
            get
            {
                return this._DatabaseTable;
            }
        }

        /// <summary>
        /// Gets this rooms type.
        /// </summary>
        public override RoomType RoomType
        {
            get
            {
                return RoomType.PrivateRoom;
            }
        }

        /// <summary>
        /// Gets this rooms id.
        /// </summary>
        public override int RoomId
        {
            get
            {
                return this._DatabaseTable.Id;
            }
        }

        /// <summary>
        /// Initialises this room.
        /// </summary>
        /// <param name="RoomId">This rooms id.</param>
        public PrivateRoom(IServerManager ServerInstance, int RoomId)
            : base(ServerInstance)
        {
            // Get this rooms database table.
            List<IDatabaseTable> Tables = ServerInstance.DatabaseService.Database.CreateQuery(DatabaseTables.Rooms)
                .Select()
                .Where(RoomsTableFields.Id, DatabaseComparison.Equals, RoomId)
                .ExecuteRead();
            this._DatabaseTable = (IRoomsDatabaseTable)Tables[0]; 

            // Initialise.
            this._HeightmapManager.Initialise();
            this._FurniManager.Initialise();

            // Write about us initialising.
            this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, this.DatabaseTable.Id, this.RoomType, "Room initialised.", null);

            // Fire the room initialised event handler.
            this._ServerManager.EventsManager.CallEvent(EventType.RoomInitialised, this, new RoomEventArguments() { GameSocketConnection = null, UserInstance = null, RoomInstance = this, AvatarInstance = null });
        }

        /// <summary>
        /// Finds if a user instance has rights in this room.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <returns>True if the user has rights.</returns>
        public IRoomRightsData GetRightsForUser(IUser UserInstance)
        {
            if (UserInstance.UserData.IsLoggedIn && !UserInstance.UserData.IsGuest)
            {
                // If the user is the room owner, then return full room rights by default.
                if (UserInstance.UserData.UserInfo.Id == this._DatabaseTable.OwnerId)
                {
                    return RoomRightsData.Full;
                }
                else
                {
                    // Get the possible table.
                    List<IDatabaseTable> Tables = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.RoomsRights)
                        .Select()
                        .Where(new string[] { RoomsRightsTableFields.MemberId, RoomsRightsTableFields.RoomId }, new DatabaseComparison[] { DatabaseComparison.Equals, DatabaseComparison.Equals }, new object[] { UserInstance.UserData.UserInfo.Id, this._DatabaseTable.Id })
                        .ExecuteRead();

                    // Make sure we got a record.
                    if (Tables != null && Tables.Count == 1)
                    {
                        return new RoomRightsData((IRoomsRightsDatabaseTable)Tables[0]);
                    }
                }
            }

            return RoomRightsData.None;
        }
    }
}
