using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the messages database table.
    /// </summary>
    public interface IMessagesDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this message
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The member id this message was sent to.
        /// </summary>
        int MemberId { get; set; }
        
        /// <summary>
        /// The member id of the sender of this message
        /// </summary>
        int FriendId { get; set; }

        /// <summary>
        /// The timestamp when this message was sent.
        /// </summary>
        int Created { get; set; }

        /// <summary>
        /// The title of this message.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The message body.
        /// </summary>
        string Message { get; set; }
    }

    /// <summary>
    /// Represents the messages table fields.
    /// </summary>
    public static class MessagesTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string FriendId = "friend_id";
        public const string Created = "created";
        public const string Title = "title";
        public const string Message = "message";
    }
}
