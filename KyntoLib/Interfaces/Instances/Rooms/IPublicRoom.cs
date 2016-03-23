using System;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Instances.Rooms
{
    /// <summary>
    /// Interfaces with a public room.
    /// </summary>
    public interface IPublicRoom : IRoom
    {
        /// <summary>
        /// Gets this rooms database table.
        /// </summary>
        IPublicsDatabaseTable DatabaseTable { get; }
    }
}
