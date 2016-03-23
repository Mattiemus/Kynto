using System;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.User.Data;

namespace KyntoLib.Interfaces.Instances.Rooms
{
    /// <summary>
    /// Interfaces with a private room.
    /// </summary>
    public interface IPrivateRoom : IRoom
    {
        /// <summary>
        /// Gets this rooms database table.
        /// </summary>
        IRoomsDatabaseTable DatabaseTable { get; }

        /// <summary>
        /// Finds if a user instance has rights in this room.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <returns>The users rights data.</returns>
        IRoomRightsData GetRightsForUser(IUser UserInstance);
    }
}
