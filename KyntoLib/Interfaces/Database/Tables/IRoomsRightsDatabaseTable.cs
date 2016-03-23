using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the rooms rights database table.
    /// </summary>
    public interface IRoomsRightsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this room right.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The room id this rights set belongs too.
        /// </summary>
        int RoomId { get; set; }

        /// <summary>
        /// The member id this set of rights belongs to.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The rights this member has.
        /// </summary>
        string Rights { get; set; }
    }

    /// <summary>
    /// Represents the room rights table fields.
    /// </summary>
    public static class RoomsRightsTableFields
    {
        public const string Id = "id";
        public const string RoomId = "room_id";
        public const string MemberId = "member_id";
        public const string Rights = "rights";
    }
}
