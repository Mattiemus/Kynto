using System;

using KyntoLib.Interfaces.Instances.Rooms.Avatars;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The packet representing a legacy login request.
    /// </summary>
    public class LoginRequestPacket
    {
        /// <summary>
        /// The username string.
        /// </summary>
        public string U;

        /// <summary>
        /// The password string.
        /// </summary>
        public string P;
    }

    /// <summary>
    /// The login failed response.
    /// </summary>
    public class LoginFailurePacket
    {
        /// <summary>
        /// The reason as to why the login failed.
        /// </summary>
        public string R;

        /// <summary>
        /// The ban message.
        /// </summary>
        public string M;

        /// <summary>
        /// The time stamp when the ban will be lifted.
        /// </summary>
        public long T;
    }

    /// <summary>
    /// The user data packet.
    /// </summary>
    public class UserDataPacket
    {
        /// <summary>
        /// The users clothes.
        /// </summary>
        public AvatarClothes C;

        /// <summary>
        /// The users motto.
        /// </summary>
        public string M;

        /// <summary>
        /// The users username.
        /// </summary>
        public string UN;

        /// <summary>
        /// The users sex.
        /// </summary>
        public string S;

        /// <summary>
        /// Stores the number of the users silver blocks.
        /// </summary>
        public int BS;

        /// <summary>
        /// Weather or not the user has logged in as a guest.
        /// </summary>
        public bool IG;

        /// <summary>
        /// The users inventory data.
        /// </summary>
        public FurnictureArrayPacket[] ID;
    }
}
