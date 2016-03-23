using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User.Data;

namespace KyntoLib.Interfaces.Instances.User
{
    /// <summary>
    /// Interfaces with a users dataset.
    /// </summary>
    public interface IUserData
    {
        /// <summary>
        /// Gets or sets if the user is logged in.
        /// </summary>
        bool IsLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets if the user is a guest.
        /// </summary>
        bool IsGuest { get; set; }

        /// <summary>
        /// Gets or sets the time the user logged in.
        /// </summary>
        DateTime LoggedInAt { get; set; }

        /// <summary>
        /// Gets or sets this users activated commands list.
        /// </summary>
        IActivatedCommands ActivatedCommands { get; set; }

        /// <summary>
        /// Gets or sets this users info.
        /// </summary>
        IMembersDatabaseTable UserInfo { get; set; }

        /// <summary>
        /// Gets or sets this users badges.
        /// </summary>
        List<int> UsersBadges { get; set; }

        /// <summary>
        /// Gets or sets the list of this users available clothes.
        /// </summary>
        List<IClothesDatabaseTable> UsersClothes { get; set; }

        /// <summary>
        /// Gets or sets the list of this users friends.
        /// </summary>
        List<IMembersFriendsDatabaseTable> UsersFriends { get; set; }

        /// <summary>
        /// Gets or sets this users available commands.
        /// </summary>
        IAvaliableCommands UsersCommands { get; set; }

        /// <summary>
        /// Gets or sets this users rank info.
        /// </summary>
        IRightsData UsersRank { get; set; }

        /// <summary>
        /// Gets or sets the list of known furni templates.
        /// </summary>
        List<string> KnownFurniTemplates { get; set; }
    }
}
