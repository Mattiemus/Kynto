using System;

namespace KyntoLib.Interfaces.Instances.Rooms.Avatars
{
    /// <summary>
    /// Interfaces with an avatar instance.
    /// </summary>
    public interface IAvatar
    {
        /// <summary>
        /// Gets the current room this avatar is in.
        /// </summary>
        IRoom CurrentRoom { get; set; }

        /// <summary>
        /// Gets or sets the id that represents this avatar (only valid in a room, and may change 
        /// from room too room).
        /// </summary>
        int AvatarId { get; set; }

        /// <summary>
        /// Gets the user id behind this avatar.
        /// </summary>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of this avatar.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets or sets the motto of this avatar.
        /// </summary>
        string Motto { get; set; }

        /// <summary>
        /// Gets or sets the sex of this avatar.
        /// </summary>
        string Sex { get; set; }

        /// <summary>
        /// Gets or sets the clothes that this avatar is wearing.
        /// </summary>
        AvatarClothes Clothes { get; set; }

        /// <summary>
        /// Gets or sets the badge worn by this avatar.
        /// </summary>
        int Badge { get; set; }

        /// <summary>
        /// Gets or sets weather or not this avatar is a ghost (weather or not this avatar
        /// can be walk through).
        /// </summary>
        bool IsGhost { get; set; }

        /// <summary>
        /// Gets or sets this avatars movement method.
        /// </summary>
        MovementMethod MovementMethod { get; set; }

        /// <summary>
        /// Gets or sets the current status of this avatar.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the current walking status of this avatar.
        /// </summary>
        string WalkStatus { get; set; }

        /// <summary>
        /// Gets or sets the current x location of this avatar.
        /// </summary>
        int LocationX { get; set; }

        /// <summary>
        /// Gets or sets the current y location of this avatar.
        /// </summary>
        int LocationY { get; set; }

        /// <summary>
        /// Gets or sets the current heading of this avatar.
        /// </summary>
        int LocationH { get; set; }

        /// <summary>
        /// Gets or sets the current sit heading of this avatar.
        /// </summary>
        int LocationSH { get; set; }

        /// <summary>
        /// Gets or sets the target x location of this avatar.
        /// </summary>
        int TargetX { get; set; }

        /// <summary>
        /// Gets or sets the target y location of this avatar.
        /// </summary>
        int TargetY { get; set; }

        /// <summary>
        /// Gets or sets this avatars type information.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the heightmap this avatar uses for pathfinding.
        /// </summary>
        byte[,] Heightmap { get; set; }
    }

    /// <summary>
    /// Represents a model of the avatar clothes.
    /// </summary>
    public class AvatarClothes
    {
        /// <summary>
        /// The current body id.
        /// </summary>
        public int body;

        /// <summary>
        /// The current hat id.
        /// </summary>
        public int hair;

        /// <summary>
        /// The current hair id.
        /// </summary>
        public int hat;

        /// <summary>
        /// The current face id.
        /// </summary>
        public int face;

        /// <summary>
        /// The current top id.
        /// </summary>
        public int top;

        /// <summary>
        /// The current pants id.
        /// </summary>
        public int pants;

        /// <summary>
        /// The current accessories id.
        /// </summary>
        public int accessories;

        /// <summary>
        /// The current shoes id.
        /// </summary>
        public int shoes;
    }

    /// <summary>
    /// The walk method of an avatar.
    /// </summary>
    public enum MovementMethod
    {
        /// <summary>
        /// Normal walking.
        /// </summary>
        Walk,

        /// <summary>
        /// Admin-style warp (not checking if there is a valid path!).
        /// </summary>
        NoCheckWarp
    }
}
