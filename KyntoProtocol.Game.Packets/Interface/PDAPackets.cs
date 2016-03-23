using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The pda friends list.
    /// </summary>
    public class PDAFriendsList
    {
        /// <summary>
        /// The list of current friends.
        /// </summary>
        public PDAFriend[] F;

        /// <summary>
        /// The list of current friend requests.
        /// </summary>
        public PDAFriendRequest[] R;
    }

    /// <summary>
    /// Represents a single friend.
    /// </summary>
    public class PDAFriend
    {
        public int I;
        public string U;
        public string M;
        public bool O;
    }

    /// <summary>
    /// Represents a single friend request.
    /// </summary>
    public class PDAFriendRequest
    {
        public int I;
        public string U;
        public string M;
        public bool O;
    }

    /// <summary>
    /// Updates the status of a friend in the pda.
    /// </summary>
    public class PDAUpdateFriendStatus
    {
        /// <summary>
        /// The id of the friend.
        /// </summary>
        public int I;

        /// <summary>
        /// The username of the friend.
        /// </summary>
        public string U;

        /// <summary>
        /// The motto of the friend.
        /// </summary>
        public string M;

        /// <summary>
        /// The online status of the friend.
        /// </summary>
        public bool O;
    }

    /// <summary>
    /// Request a friend request to a user of id.
    /// </summary>
    public class PDARequestFriendRequest
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// Accept the friend request too a user.
    /// </summary>
    public class PDAAcceptFriendRequest
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// Deny the friend request too a user.
    /// </summary>
    public class PDADenyFriendRequest
    {
        /// <summary>
        /// The id of the user.
        /// </summary>
        public int I;
    }
}
