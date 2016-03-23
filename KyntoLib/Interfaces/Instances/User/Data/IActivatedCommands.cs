using System;

namespace KyntoLib.Interfaces.Instances.User.Data
{
    /// <summary>
    /// Represents all the possible activated commands of a user.
    /// </summary>
    public interface IActivatedCommands
    {
        /// <summary>
        /// Gets or sets if the be right back command is active.
        /// </summary>
        bool Brb { get; set; }
    }
}
