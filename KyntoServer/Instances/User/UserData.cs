using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.User.Data;

using KyntoServer.Instances.User.Data;

namespace KyntoServer.Instances.User
{
    /// <summary>
    /// Stores the users data.
    /// </summary>
    public class UserData : IUserData
    {
        /// <summary>
        /// Stores weather or not this user is logged in.
        /// </summary>
        private bool _IsLoggedIn;

        /// <summary>
        /// Gets or sets if the user is logged in.
        /// </summary>
        public bool IsLoggedIn
        {
            get
            {
                return _IsLoggedIn;
            }
            set
            {
                _IsLoggedIn = value;
            }
        }

        /// <summary>
        /// Stores weather or not this user is a guest.
        /// </summary>
        private bool _IsGuest;

        /// <summary>
        /// Gets or sets if the user is a guest.
        /// </summary>
        public bool IsGuest
        {
            get
            {
                return _IsGuest;
            }
            set
            {
                _IsGuest = value;
            }
        }

        /// <summary>
        /// Stores the time the user logged in.
        /// </summary>
        private DateTime _LoggedInAt;

        /// <summary>
        /// Gets or sets the time the user logged in.
        /// </summary>
        public DateTime LoggedInAt
        {
            get
            {
                return _LoggedInAt;
            }
            set
            {
                _LoggedInAt = value;
            }
        }

        /// <summary>
        /// Stores the activated commands list.
        /// </summary>
        private IActivatedCommands _ActivatedCommands;

        /// <summary>
        /// Gets or sets this users activated commands list.
        /// </summary>
        public IActivatedCommands ActivatedCommands
        {
            get
            {
                return this._ActivatedCommands;
            }
            set
            {
                this._ActivatedCommands = value;
            }
        }

        /// <summary>
        /// Stores the users info.
        /// </summary>
        private IMembersDatabaseTable _UserInfo;

        /// <summary>
        /// Gets or sets this users info.
        /// </summary>
        public IMembersDatabaseTable UserInfo
        {
            get
            {
                return this._UserInfo;
            }
            set
            {
                this._UserInfo = value;
            }
        }

        /// <summary>
        /// Gets the users badges.
        /// </summary>
        private List<int> _UsersBadges;

        /// <summary>
        /// Gets or sets this users badges.
        /// </summary>
        public List<int> UsersBadges
        {
            get
            {
                return this._UsersBadges;
            }
            set
            {
                this._UsersBadges = value;
            }
        }

        /// <summary>
        /// Stores the list of users clothes.
        /// </summary>
        private List<IClothesDatabaseTable> _UsersClothes;

        /// <summary>
        /// Gets or sets the list of this users available clothes.
        /// </summary>
        public List<IClothesDatabaseTable> UsersClothes
        {
            get
            {
                return this._UsersClothes;
            }
            set
            {
                this._UsersClothes = value;
            }
        }

        /// <summary>
        /// Stores the list of users friends.
        /// </summary>
        private List<IMembersFriendsDatabaseTable> _UsersFriends;

        /// <summary>
        /// Gets or sets the list of this users friends.
        /// </summary>
        public List<IMembersFriendsDatabaseTable> UsersFriends
        {
            get
            {
                return this._UsersFriends;
            }
            set
            {
                this._UsersFriends = value;
            }
        }

        /// <summary>
        /// Stores the list of users commands.
        /// </summary>
        private IAvaliableCommands _UsersCommands; 

        /// <summary>
        /// Gets or sets this users available commands.
        /// </summary>
        public IAvaliableCommands UsersCommands
        {
            get
            {
                return this._UsersCommands;
            }
            set
            {
                this._UsersCommands = value;
            }
        }

        /// <summary>
        /// Stores the rank of this user.
        /// </summary>
        private IRightsData _UsersRank;

        /// <summary>
        /// Gets or sets this users rank info.
        /// </summary>
        public IRightsData UsersRank
        {
            get
            {
                return this._UsersRank;
            }
            set
            {
                this._UsersRank = value;
            }
        }

        /// <summary>
        /// Stores the list of known furni templates.
        /// </summary>
        private List<string> _KnownFurniTemplates;

        /// <summary>
        /// Gets or sets the list of known furni templates.
        /// </summary>
        public List<string> KnownFurniTemplates
        {
            get
            {
                return this._KnownFurniTemplates;
            }
            set
            {
                this._KnownFurniTemplates = value;
            }
        }

        /// <summary>
        /// Initialises this user data.
        /// </summary>
        public UserData()
        {
            this._ActivatedCommands = new ActivatedCommands();
            this._UsersCommands = new AvaliableCommands();
            this._UsersClothes = new List<IClothesDatabaseTable>();
            this._UsersBadges = new List<int>();
            this._KnownFurniTemplates = new List<string>();
        }
    }
}
