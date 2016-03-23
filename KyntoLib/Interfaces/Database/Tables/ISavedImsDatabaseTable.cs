using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the saved ims database table.
    /// </summary>
    public interface ISavedImsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this im.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The member id who created this im.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The member id of who this im was sent too.
        /// </summary>
        int FriendId { get; set; }

        /// <summary>
        /// The timestamp when this im was created.
        /// </summary>
        int Created { get; set; }

        /// <summary>
        /// The message contained in this im.
        /// </summary>
        string Message { get; set; }
    }

    /// <summary>
    /// Represents the saved ims table fields.
    /// </summary>
    public static class SavedImsTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string FriendId = "friend_id";
        public const string Created = "created";
        public const string Message = "message";
    }
}
