using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User.Data;

namespace KyntoServer.Instances.User.Data
{
    /// <summary>
    /// All the possible rights of a particular rank.
    /// </summary>
    public class RightsData : IRightsData
    {
        /// <summary>
        /// Stores weather or not this user can no-check admin style teleport.
        /// </summary>
        private bool _CanNoCheckTeleport;

        /// <summary>
        /// Gets or sets weather this user can no-check admin style teleport.
        /// </summary>
        public bool CanNoCheckTeleport
        {
            get
            {
                return this._CanNoCheckTeleport;
            }
            set
            {
                this._CanNoCheckTeleport = value;
            }
        }

        /// <summary>
        /// Stores if this user can ban another user.
        /// </summary>
        private bool _CanGameBanUser;

        /// <summary>
        /// Gets or sets weather this user can ban another user.
        /// </summary>
        public bool CanGameBanUser
        {
            get
            {
                return this._CanGameBanUser;
            }
            set
            {
                this._CanGameBanUser = value;
            }
        }

        /// <summary>
        /// Stores weather this user can kick a user from a private room.
        /// </summary>
        private bool _CanKickUserFromPrivateRoom;

        /// <summary>
        /// Gets or sets weather this user can kick a user from a private room.
        /// </summary>
        public bool CanKickUserFromPrivateRoom
        {
            get
            {
                return this._CanKickUserFromPrivateRoom;
            }
            set
            {
                this._CanKickUserFromPrivateRoom = value;
            }
        }

        /// <summary>
        /// Stores weather this user can kick a user from a public room.
        /// </summary>
        private bool _CanKickUserFromPublicRoom;

        /// <summary>
        /// Gets or sets weather this user can kick a user from a public room.
        /// </summary>
        public bool CanKickUserFromPublicRoom
        {
            get
            {
                return this._CanKickUserFromPublicRoom;
            }
            set
            {
                this._CanKickUserFromPublicRoom = value;
            }
        }

        /// <summary>
        /// Stores weather this user can move items in a private room.
        /// </summary>
        private bool _CanOverrideMoveFurniInPrivateRooms;

        /// <summary>
        /// Gets or sets weather this user can move items in a private room.
        /// </summary>
        public bool CanOverrideMoveFurniInPrivateRooms
        {
            get
            {
                return _CanOverrideMoveFurniInPrivateRooms;
            }
            set
            {
                this._CanOverrideMoveFurniInPrivateRooms = value;
            }
        }

        /// <summary>
        /// Stores weather this user can pickup items to his/her own inventory in a private room.
        /// </summary>
        private bool _CanOverridePickupFurniToOwnInPrivateRooms;

        /// <summary>
        /// Gets or sets weather this user can pickup items to his/her own inventory in a private room.
        /// </summary>
        public bool CanOverridePickupFurniToOwnInPrivateRooms
        {
            get
            {
                return this._CanOverridePickupFurniToOwnInPrivateRooms;
            }
            set
            {
                this._CanOverridePickupFurniToOwnInPrivateRooms = value;
            }
        }

        /// <summary>
        /// Stores weather this user can return items to the owner in a private room.
        /// </summary>
        private bool _CanOverridePickupFurniToOwnerInPrivateRooms;

        /// <summary>
        /// Gets or sets weather this user can return items to the owner in a private room.
        /// </summary>
        public bool CanOverridePickupFurniToOwnerInPrivateRooms
        {
            get
            {
                return this._CanOverridePickupFurniToOwnerInPrivateRooms;
            }
            set
            {
                this._CanOverridePickupFurniToOwnerInPrivateRooms = value;
            }
        }

        /// <summary>
        /// Stores weather this user can drop items in a private room.
        /// </summary>
        private bool _CanOverrideDropFurniInPrivateRooms;

        /// <summary>
        /// Gets or sets weather this user can drop items in a private room.
        /// </summary>
        public bool CanOverrideDropFurniInPrivateRooms
        {
            get
            {
                return this._CanOverrideDropFurniInPrivateRooms;
            }
            set
            {
                this._CanOverrideDropFurniInPrivateRooms = value;
            }
        }

        /// <summary>
        /// Stores weather this user can activate items in a private room.
        /// </summary>
        private bool _CanOverrideActivateFurniInPrivateRooms;

        /// <summary>
        /// Gets or sets weather this user can activate items in a private room.
        /// </summary>
        public bool CanOverrideActivateFurniInPrivateRooms
        {
            get
            {
                return _CanOverrideActivateFurniInPrivateRooms;
            }
            set
            {
                this._CanOverrideActivateFurniInPrivateRooms = value;
            }
        }

        /// <summary>
        /// Stores weather this user can delete items in a private room.
        /// </summary>
        private bool _CanOverrideDeleteFurniInPrivateRooms;

        /// <summary>
        /// Gets or sets weather this user can delete items in a private room.
        /// </summary>
        public bool CanOverrideDeleteFurniInPrivateRooms
        {
            get
            {
                return this._CanOverrideDeleteFurniInPrivateRooms;
            }
            set
            {
                this.CanOverrideDeleteFurniInPrivateRooms = value;
            }
        }

        /// <summary>
        /// Stores weather this user can ban a user from a private room.
        /// </summary>
        private bool _CanPrivateRoomBanUser;

        /// <summary>
        /// Gets or sets weather this user can ban a user from a private room.
        /// </summary>
        public bool CanPrivateRoomBanUser
        {
            get
            {
                return this._CanPrivateRoomBanUser;
            }
            set
            {
                this._CanPrivateRoomBanUser = value;
            }
        }

        /// <summary>
        /// Initialises this rights data.
        /// </summary>
        /// <param name="DataTable">The table info.</param>
        public RightsData(IRanksDatabaseTable DataTable)
        {
            Parse(DataTable);
        }

        /// <summary>
        /// Parses the table, reading the rights data.
        /// </summary>
        /// <param name="Table">The table to parse.</param>
        public void Parse(IRanksDatabaseTable Table)
        {
            // Split up the ranks data.
            string[] RankData = Table.Rights.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Stores all the rights.
            // Game-wide rights.
            this._CanGameBanUser = RankData.Contains("can_game_ban");
            this._CanNoCheckTeleport = RankData.Contains("can_game_nocheckteleport");

            // Public room rights.
            this._CanKickUserFromPublicRoom = RankData.Contains("can_publicroom_kick");

            // Private room rights.
            this._CanKickUserFromPrivateRoom = RankData.Contains("can_privateroom_kick");
            this._CanOverrideMoveFurniInPrivateRooms = RankData.Contains("can_privateroom_movefurni");
            this._CanOverridePickupFurniToOwnInPrivateRooms = RankData.Contains("can_privateroom_pickfurnitoown");
            this._CanOverridePickupFurniToOwnerInPrivateRooms = RankData.Contains("can_privateroom_pickfurnitoowner");
            this._CanOverrideDropFurniInPrivateRooms = RankData.Contains("can_privateroom_dropfurni");
            this._CanOverrideActivateFurniInPrivateRooms = RankData.Contains("can_privateroom_activatefurni");
            this._CanOverrideDeleteFurniInPrivateRooms = RankData.Contains("can_privateroom_deletefurni");
            this._CanPrivateRoomBanUser = RankData.Contains("can_privateroom_ban");
        }
    }
}
