using System;

using KyntoLib.Interfaces.Instances.Rooms.Avatars;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The add avatar too room packet.
    /// </summary>
    public class AddAvatarPacket
    {
        /// <summary>
        /// The id of the avatar.
        /// </summary>
        public int I;

        /// <summary>
        /// The user id of the avatar.
        /// </summary>
        public int UI;

        /// <summary>
        /// The username of the avatar.
        /// </summary>
        public string U;

        /// <summary>
        /// The mission of the avatar.
        /// </summary>
        public string M;

        /// <summary>
        /// The avatar clothes.
        /// </summary>
        public AvatarClothes C;

        /// <summary>
        /// The avatar sex.
        /// </summary>
        public string S;

        /// <summary>
        /// The avatar badge.
        /// </summary>
        public int B;

        /// <summary>
        /// The avatars x coordinate.
        /// </summary>
        public int X;

        /// <summary>
        /// The avatars y coordinate.
        /// </summary>
        public int Y;

        /// <summary>
        /// The avatars heading.
        /// </summary>
        public int H;

        /// <summary>
        /// ????
        /// </summary>
        public bool G;

        /// <summary>
        /// The avatar walk status.
        /// </summary>
        public string St;

        /// <summary>
        /// The avatar ghosting status.
        /// </summary>
        public string Sta;

        /// <summary>
        /// The avatars type.
        /// </summary>
        public string T;
    }

    /// <summary>
    /// The remove avatar packet.
    /// </summary>
    public class RemoveAvatarPacket
    {
        /// <summary>
        /// The id of the avatar to remove.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// The move avatar packet.
    /// </summary>
    public class MoveAvatarPacket
    {
        /// <summary>
        /// The avatar id to move.
        /// </summary>
        public int I;

        /// <summary>
        /// The avatars new x position.
        /// </summary>
        public int X;

        /// <summary>
        /// The avatars new y position.
        /// </summary>
        public int Y;

        /// <summary>
        /// The avatars new heading.
        /// </summary>
        public int H;

        /// <summary>
        /// The avatars new sit heading.
        /// </summary>
        public int SH;

        /// <summary>
        /// Weather the avatar is sitting.
        /// </summary>
        public int SI;

        /// <summary>
        /// The avatars status string.
        /// </summary>
        public string S;

        /// <summary>
        /// The avatars walk status.
        /// </summary>
        public string W;
    }

    /// <summary>
    /// The request to move the users avatar.
    /// </summary>
    public class MoveAvatarRequestPacket
    {
        /// <summary>
        /// The x coordinate too move too.
        /// </summary>
        public int X;

        /// <summary>
        /// The y coordinate too move too.
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// Updates an avatar.
    /// </summary>
    public class AvatarUpdatePacket
    {
        /// <summary>
        /// The id of the avatar to update.
        /// </summary>
        public int I;

        /// <summary>
        /// The avatars ghost status.
        /// </summary>
        public string S;

        /// <summary>
        /// The avatars sit status.
        /// </summary>
        public bool Si;

        /// <summary>
        /// The new avatars clothes.
        /// </summary>
        public AvatarClothes C;
    }
}
