using System;

using KyntoLib.Interfaces.Instances.Rooms.Avatars;

namespace KyntoLib.Interfaces.Instances.User
{
    /// <summary>
    /// Interfaces with a user.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the Pda service for this user.
        /// </summary>
        IPdaService PdaService { get; }

        /// <summary>
        /// Gets the inventory service for this user.
        /// </summary>
        IInventoryService InventoryService { get; }

        /// <summary>
        /// Gets this users data.
        /// </summary>
        IUserData UserData { get; }

        /// <summary>
        /// Gets this users avatar that would represent him/her in a room.
        /// </summary>
        IAvatar Avatar { get; }
    }
}
