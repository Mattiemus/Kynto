using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Events;

using KyntoServer.Instances.Rooms.Furni;

namespace KyntoServer.Instances.Rooms
{
    /// <summary>
    /// Handles the public room responsibilities.
    /// </summary>
    public class PublicRoom : Room, IPublicRoom
    {
        /// <summary>
        /// Stores this rooms database table.
        /// </summary>
        private IPublicsDatabaseTable _DatabaseTable;

        /// <summary>
        /// Gets this rooms database table.
        /// </summary>
        public IPublicsDatabaseTable DatabaseTable
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
                return RoomType.PublicRoom;
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
        public PublicRoom(IServerManager ServerInstance, int RoomId)
            : base(ServerInstance)
        {
            // Get this rooms database table.
            List<IDatabaseTable> Tables = ServerInstance.DatabaseService.Database.CreateQuery(DatabaseTables.Publics)
                .Select()
                .Where(PublicsTableFields.Id, DatabaseComparison.Equals, RoomId)
                .ExecuteRead();
            this._DatabaseTable = (IPublicsDatabaseTable)Tables[0];

            // Initialise.
            this._HeightmapManager.Initialise();
            this._FurniManager.Initialise();

            // Write about us initialising.
            this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, this.DatabaseTable.Id, this.RoomType, "Room initialised.", null);

            // Fire the room initialised event handler.
            this._ServerManager.EventsManager.CallEvent(EventType.RoomInitialised, this, new RoomEventArguments() { GameSocketConnection = null, UserInstance = null, RoomInstance = this, AvatarInstance = null });
        }
    }
}
