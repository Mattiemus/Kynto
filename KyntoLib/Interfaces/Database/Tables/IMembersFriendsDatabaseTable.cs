using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the members friends database table.
    /// </summary>
    public interface IMembersFriendsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id for this array of friends.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The member id.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The friend id.
        /// </summary>
        int FriendId { get; set; }

        /// <summary>
        /// The timestamp that this friendship was created.
        /// </summary>
        int Created { get; set; }

        /// <summary>
        /// Weather or not this friendship is active.
        /// </summary>
        bool Active { get; set; }
    }

    /// <summary>
    /// Represents the members friends table fields.
    /// </summary>
    public static class MembersFriendsTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string FriendId = "friend_id";
        public const string Created = "created";
        public const string Active = "active";
    }
}
