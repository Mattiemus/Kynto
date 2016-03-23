using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the members badges database table.
    /// </summary>
    public interface IMembersBadgesDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this set of badges.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The member id of the owner of these badges.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The badges.
        /// </summary>
        string Badges { get; set; }
    }

    /// <summary>
    /// Represents the members badges table fields.
    /// </summary>
    public static class MembersBadgesTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string Badges = "badges";
    }
}
