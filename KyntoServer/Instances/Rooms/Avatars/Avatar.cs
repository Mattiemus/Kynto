using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Services;

namespace KyntoServer.Instances.Rooms.Avatars
{
    /// <summary>
    /// Represents a room avatar.
    /// </summary>
    public class Avatar : IAvatar
    {
        /// <summary>
        /// Stores the current room the avatar is in.
        /// </summary>
        private IRoom _CurrentRoom;

        /// <summary>
        /// Gets the current room this avatar is in.
        /// </summary>
        public IRoom CurrentRoom
        {
            get
            {
                return this._CurrentRoom;
            }
            set
            {
                this._CurrentRoom = value;
            }
        }

        /// <summary>
        /// Stores this avatars id.
        /// </summary>
        private int _AvatarId;

        /// <summary>
        /// Gets or sets the id that represents this avatar (only valid in a room, and may change 
        /// from room too room).
        /// </summary>
        public int AvatarId
        {
            get
            {
                return this._AvatarId;
            }
            set
            {
                this._AvatarId = value;
            }
        }

        /// <summary>
        /// Stores this avatars user id.
        /// </summary>
        private int _UserId = -1;

        /// <summary>
        /// Gets the user id behind this avatar.
        /// </summary>
        public int UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }

        /// <summary>
        /// Stores this avatars username.
        /// </summary>
        private string _Username;

        /// <summary>
        /// Gets or sets the username of this avatar.
        /// </summary>
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
            }
        }

        /// <summary>
        /// Stores this avatars motto.
        /// </summary>
        private string _Motto;

        /// <summary>
        /// Gets or sets the motto of this avatar.
        /// </summary>
        public string Motto
        {
            get
            {
                return this._Motto;
            }
            set
            {
                this._Motto = value;
            }
        }

        /// <summary>
        /// Stores this avatars sex.
        /// </summary>
        private string _Sex;

        /// <summary>
        /// Gets or sets the sex of this avatar.
        /// </summary>
        public string Sex
        {
            get
            {
                return this._Sex;
            }
            set
            {
                this._Sex = value;
            }
        }

        /// <summary>
        /// Stores this avatars clothes.
        /// </summary>
        private AvatarClothes _Clothes;

        /// <summary>
        /// Gets or sets the clothes that this avatar is wearing.
        /// </summary>
        public AvatarClothes Clothes
        {
            get
            {
                return this._Clothes;
            }
            set
            {
                this._Clothes = value;
            }
        }

        /// <summary>
        /// Stores this avatars active badge.
        /// </summary>
        private int _Badge;

        /// <summary>
        /// Gets or sets the badge worn by this avatar.
        /// </summary>
        public int Badge
        {
            get
            {
                return this._Badge;
            }
            set
            {
                this._Badge = value;
            }
        }

        /// <summary>
        /// Stores if this avatar is AFK or not.
        /// </summary>
        private bool _IsGhost;

        /// <summary>
        /// Gets or sets weather or not this avatar is a ghost (weather or not this avatar
        /// can be walk through).
        /// </summary>
        public bool IsGhost
        {
            get
            {
                return this._IsGhost;
            }
            set
            {
                this._IsGhost = value;
            }
        }

        /// <summary>
        /// Stores this avatars movement method.
        /// </summary>
        private MovementMethod _MovementMethod;

        /// <summary>
        /// Gets or sets this avatars movement method.
        /// </summary>
        public MovementMethod MovementMethod
        {
            get
            {
                return this._MovementMethod;
            }
            set
            {
                this._MovementMethod = value;
            }
        }

        /// <summary>
        /// Stores this avatars status.
        /// </summary>
        private string _Status;

        /// <summary>
        /// Gets or sets the current status of this avatar.
        /// </summary>
        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }

        /// <summary>
        /// Stores this avatars walk status.
        /// </summary>
        private string _WalkStatus = "walk_end";

        /// <summary>
        /// Gets or sets the current walking status of this avatar.
        /// </summary>
        public string WalkStatus
        {
            get
            {
                return this._WalkStatus;
            }
            set
            {
                this._WalkStatus = value;
            }
        }

        /// <summary>
        /// Stores this avatars current location.
        /// </summary>
        private int _LocationX;

        /// <summary>
        /// Gets or sets the current x location of this avatar.
        /// </summary>
        public int LocationX
        {
            get
            {
                return this._LocationX;
            }
            set
            {
                this._LocationX = value;
            }
        }

        /// <summary>
        /// Stores this avatars current location.
        /// </summary>
        private int _LocationY;

        /// <summary>
        /// Gets or sets the current y location of this avatar.
        /// </summary>
        public int LocationY
        {
            get
            {
                return this._LocationY;
            }
            set
            {
                this._LocationY = value;
            }
        }

        /// <summary>
        /// Stores this avatars current heading.
        /// </summary>
        private int _LocationH;

        /// <summary>
        /// Gets or sets the current heading of this avatar.
        /// </summary>
        public int LocationH
        {
            get
            {
                return this._LocationH;
            }
            set
            {
                this._LocationH = value;
            }
        }

        /// <summary>
        /// Stores this avatars current sit heading.
        /// </summary>
        private int _LocationSH;

        /// <summary>
        /// Gets or sets the current sit heading of this avatar.
        /// </summary>
        public int LocationSH
        {
            get
            {
                return this._LocationSH;
            }
            set
            {
                this._LocationSH = value;
            }
        }

        /// <summary>
        /// Stores this avatars current target;
        /// </summary>
        private int _TargetX;

        /// <summary>
        /// Gets or sets the target x location of this avatar.
        /// </summary>
        public int TargetX
        {
            get
            {
                return this._TargetX;
            }
            set
            {
                this._TargetX = value;
            }
        }

        /// <summary>
        /// Stores this avatars current target;
        /// </summary>
        private int _TargetY;

        /// <summary>
        /// Gets or sets the target y location of this avatar.
        /// </summary>
        public int TargetY
        {
            get
            {
                return this._TargetY;
            }
            set
            {
                this._TargetY = value;
            }
        }

        /// <summary>
        /// Stores this avatars type information.
        /// </summary>
        private string _Type;

        /// <summary>
        /// Gets or sets this avatars type information.
        /// </summary>
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        /// <summary>
        /// Stores the height map this avatar uses.
        /// </summary>
        private byte[,] _Heightmap;

        /// <summary>
        /// Gets or sets the height map this avatar uses for path finding.
        /// </summary>
        public byte[,] Heightmap
        {
            get
            {
                return this._Heightmap;
            }
            set
            {
                this._Heightmap = value;
            }
        }

        /// <summary>
        /// Initialises this avatar with default information.
        /// </summary>
        public Avatar()
        {
            // Set default values.
            this._AvatarId = 0;
            this._IsGhost = false;
            this._MovementMethod = MovementMethod.Walk;
            this._Status = "idle";
        }
    }
}
