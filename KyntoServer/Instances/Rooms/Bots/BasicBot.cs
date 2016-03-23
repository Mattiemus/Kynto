using System;
using System.Collections.Generic;
using System.Linq;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Instances.Rooms.Bots;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

namespace KyntoServer.Instances.Rooms.Bots
{
    /// <summary>
    /// Represents a single bot.
    /// </summary>
    public class BasicBot : IBot
    {
        /// <summary>
        /// Stores this bots avatar.
        /// </summary>
        private IAvatar _Avatar;

        /// <summary>
        /// Gets this bots avatar.
        /// </summary>
        public IAvatar Avatar
        {
            get
            {
                return this._Avatar;
            }
            set
            {
                this._Avatar = value;
            }
        }

        /// <summary>
        /// Stores the room this avatar belongs in.
        /// </summary>
        private IRoom _ParentRoom;

        /// <summary>
        /// Gets this bots parent room.
        /// </summary>
        public IRoom ParentRoom
        {
            get
            {
                return this._ParentRoom;
            }
            set
            {
                this._ParentRoom = value;
            }
        }

        /// <summary>
        /// Initialises a new bot.
        /// </summary>
        public BasicBot()
        {
        }
    }
}
