using System;

using KyntoLib.Interfaces.Instances.Rooms.Avatars;

namespace KyntoLib.Interfaces.Instances.Rooms.Bots
{
    /// <summary>
    /// Interfaces with a bot.
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Gets this bots avatar.
        /// </summary>
        IAvatar Avatar { get; set; }

        /// <summary>
        /// Gets this bots parent room.
        /// </summary>
        IRoom ParentRoom { get; set; }
    }
}
