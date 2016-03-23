using System;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Instances.User.Data
{
    /// <summary>
    /// Represents all the possible rights a user can have for a room.
    /// </summary>
    public interface IRoomRightsData
    {       
        /// <summary>
        /// Gets or sets weather this user can move furni in the room.
        /// </summary>
        bool CanMoveFurni { get; set; }

        /// <summary>
        /// Gets or sets weather this user can pickup furni to the item owners inventory.
        /// </summary>
        bool CanPickupFurniToOwnersInventory { get; set; }

        /// <summary>
        /// Gets or sets weather this user can pickup furni to his/her own inventory.
        /// </summary>
        bool CanPickupFurniToOwnInventory { get; set; }

        /// <summary>
        /// Gets or sets weather this user can drop furni into the room.
        /// </summary>
        bool CanDropFurni { get; set; }

        /// <summary>
        /// Gets or sets weather this user can activate a furni item.
        /// </summary>
        bool CanActivateFurni { get; set; }

        /// <summary>
        /// Gets or sets weather this user can delete a furni item.
        /// </summary>
        bool CanDeleteFurni { get; set; }

        /// <summary>
        /// Gets or sets weather this user can kick a user.
        /// </summary>
        bool CanKickUser { get; set; }

        /// <summary>
        /// Gets or sets weather this user can ban a user from the room.
        /// </summary>
        bool CanBanUser { get; set; }

        /// <summary>
        /// Parses the table, reading the rights data.
        /// </summary>
        /// <param name="Table">The table to parse.</param>
        void Parse(IRoomsRightsDatabaseTable Table);
    }
}
