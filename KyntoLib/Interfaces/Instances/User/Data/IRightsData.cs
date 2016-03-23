using System;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Instances.User.Data
{
    /// <summary>
    /// Represents all the possible rights a rank can have.
    /// </summary>
    public interface IRightsData
    {
        /// <summary>
        /// Gets or sets weather this user can no-check admin style teleport.
        /// </summary>
        bool CanNoCheckTeleport { get; set; }

        /// <summary>
        /// Gets or sets weather this user can ban another user.
        /// </summary>
        bool CanGameBanUser { get; set; }

        /// <summary>
        /// Gets or sets weather this user can kick a user from a private room.
        /// </summary>
        bool CanKickUserFromPrivateRoom { get; set; }

        /// <summary>
        /// Gets or sets weather this user can kick a user from a public room.
        /// </summary>
        bool CanKickUserFromPublicRoom { get; set; }

        /// <summary>
        /// Gets or sets weather this user can move items in a private room.
        /// </summary>
        bool CanOverrideMoveFurniInPrivateRooms { get; set; }

        /// <summary>
        /// Gets or sets weather this user can pickup items to his/her own inventory in a private room.
        /// </summary>
        bool CanOverridePickupFurniToOwnInPrivateRooms { get; set; }

        /// <summary>
        /// Gets or sets weather this user can return items to the owner in a private room.
        /// </summary>
        bool CanOverridePickupFurniToOwnerInPrivateRooms { get; set; }

        /// <summary>
        /// Gets or sets weather this user can drop items in a private room.
        /// </summary>
        bool CanOverrideDropFurniInPrivateRooms { get; set; }

        /// <summary>
        /// Gets or sets weather this user can activate items in a private room.
        /// </summary>
        bool CanOverrideActivateFurniInPrivateRooms { get; set; }

        /// <summary>
        /// Gets or sets weather this user can delete items in a private room.
        /// </summary>
        bool CanOverrideDeleteFurniInPrivateRooms { get; set; }

        /// <summary>
        /// Gets or sets weather this user can ban a user from a private room.
        /// </summary>
        bool CanPrivateRoomBanUser { get; set; }

        /// <summary>
        /// Parses the table, reading the rights data.
        /// </summary>
        /// <param name="Table">The table to parse.</param>
        void Parse(IRanksDatabaseTable Table);
    }
}
