using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the members ban database table.
    /// </summary>
    public interface IMembersBanDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this ban.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The id of the member being banned.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The timestamp when this ban was created.
        /// </summary>
        int Created { get; set; }

        /// <summary>
        /// The id of the member who created this ban.
        /// </summary>
        int ById { get; set; }

        /// <summary>
        /// The reason this ban was made.
        /// </summary>
        string Reason { get; set; }

        /// <summary>
        /// The length of time this ban will last for.
        /// </summary>
        int Length { get; set; }
    }

    /// <summary>
    /// Represents the members ban table fields.
    /// </summary>
    public static class MembersBanTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string Created = "created";
        public const string ById = "by_id";
        public const string Reason = "reason";
        public const string Length = "length";
    }
}
