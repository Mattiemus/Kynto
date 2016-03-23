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
    /// Represents all the possible rights a user can have for a room.
    /// </summary>
    public class RoomRightsData : IRoomRightsData
    {
        /// <summary>
        /// Gets full room rights.
        /// </summary>
        public static RoomRightsData Full
        {
            get
            {
                return new RoomRightsData()
                {
                    CanMoveFurni = true,
                    CanPickupFurniToOwnersInventory = true,
                    CanPickupFurniToOwnInventory = true,
                    CanDropFurni = true, 
                    CanActivateFurni = true,
                    CanKickUser = true,
                    CanBanUser = true
                };
            }
        }

        /// <summary>
        /// Gets no room rights.
        /// </summary>
        public static RoomRightsData None
        {
            get
            {
                return new RoomRightsData()
                {
                    CanMoveFurni = false,
                    CanPickupFurniToOwnersInventory = false,
                    CanPickupFurniToOwnInventory = false,
                    CanDropFurni = false,
                    CanActivateFurni = false,
                    CanKickUser = false,
                    CanBanUser = false
                };
            }
        }

        /// <summary>
        /// Stores weather this user can move furni in the room.
        /// </summary>
        private bool _CanMoveFurni;

        /// <summary>
        /// Gets or sets weather this user can move furni in the room.
        /// </summary>
        public bool CanMoveFurni
        {
            get
            {
                return this._CanMoveFurni;
            }
            set
            {
                this._CanMoveFurni = value;
            }
        }

        /// <summary>
        /// Stores weather this user can pickup furni to the item owners inventory.
        /// </summary>
        private bool _CanPickupFurniToOwnersInventory;

        /// <summary>
        /// Gets or sets weather this user can pickup furni to the item owners inventory.
        /// </summary>
        public bool CanPickupFurniToOwnersInventory
        {
            get
            {
                return this._CanPickupFurniToOwnersInventory;
            }
            set
            {
                this._CanPickupFurniToOwnersInventory = value;
            }
        }

        /// <summary>
        /// Stores weather this user can pickup furni to his/her own inventory.
        /// </summary>
        private bool _CanPickupFurniToOwnInventory;

        /// <summary>
        /// Gets or sets weather this user can pickup furni to his/her own inventory.
        /// </summary>
        public bool CanPickupFurniToOwnInventory
        {
            get
            {
                return this._CanPickupFurniToOwnInventory;
            }
            set
            {
                this._CanPickupFurniToOwnInventory = value;
            }
        }

        /// <summary>
        /// Stores weather this user can drop furni into the room.
        /// </summary>
        private bool _CanDropFurni;

        /// <summary>
        /// Gets or sets weather this user can drop furni into the room.
        /// </summary>
        public bool CanDropFurni
        {
            get
            {
                return this._CanDropFurni;
            }
            set
            {
                this._CanDropFurni = value;
            }
        }

        /// <summary>
        /// Stores weather this user can activate a furni item.
        /// </summary>
        private bool _CanActivateFurni;

        /// <summary>
        /// Gets or sets weather this user can activate a furni item.
        /// </summary>
        public bool CanActivateFurni
        {
            get
            {
                return this._CanActivateFurni;
            }
            set
            {
                this._CanActivateFurni = value;
            }
        }

        /// <summary>
        /// Stores weather this user can delete a furni item.
        /// </summary>
        private bool _CanDeleteFurni;

        /// <summary>
        /// Gets or sets weather this user can delete a furni item.
        /// </summary>
        public bool CanDeleteFurni
        {
            get
            {
                return this._CanDeleteFurni;
            }
            set
            {
                this._CanDeleteFurni = value;
            }
        }

        /// <summary>
        /// Stores weather this user can kick a user.
        /// </summary>
        public bool _CanKickUser;

        /// <summary>
        /// Gets or sets weather this user can kick a user.
        /// </summary>
        public bool CanKickUser
        {
            get
            {
                return this._CanKickUser;
            }
            set
            {
                this._CanKickUser = value;
            }
        }

        /// <summary>
        /// Stores weather this user can ban a user from the room.
        /// </summary>
        private bool _CanBanUser;

        /// <summary>
        /// Gets or sets weather this user can ban a user from the room.
        /// </summary>
        public bool CanBanUser
        {
            get
            {
                return this._CanBanUser;
            }
            set
            {
                this._CanBanUser = value;
            }
        }

        /// <summary>
        /// Initialises this room rights data.
        /// </summary>
        public RoomRightsData()
        {
        }

        /// <summary>
        /// Initialises this room rights data.
        /// </summary>
        /// <param name="DataTable">The table info.</param>
        public RoomRightsData(IRoomsRightsDatabaseTable DataTable)
        {
            Parse(DataTable);
        }

        /// <summary>
        /// Parses the table, reading the rights data.
        /// </summary>
        /// <param name="Table">The table to parse.</param>
        public void Parse(IRoomsRightsDatabaseTable Table)
        {
            // Split up the ranks data.
            string[] RankData = Table.Rights.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Stores all the rights.
            this._CanMoveFurni = RankData.Contains("move_furni");
            this._CanPickupFurniToOwnersInventory = RankData.Contains("pick_owners");
            this._CanPickupFurniToOwnInventory = RankData.Contains("pick_own");
            this._CanDropFurni = RankData.Contains("drop_furni");
            this._CanActivateFurni = RankData.Contains("activate_furni");
            this._CanDeleteFurni = RankData.Contains("delete_furni");
            this._CanKickUser = RankData.Contains("kick_user");
            this._CanBanUser = RankData.Contains("ban_user");
        }
    }
}
