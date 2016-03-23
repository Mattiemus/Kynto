using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the members clothes database table.
    /// </summary>
    public interface IMembersClothesDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this set of clothes.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The member id who owns this set of clothes.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The clothes array.
        /// </summary>
        string Clothes { get; set; }
    }

    /// <summary>
    /// Represents the members clothes table fields.
    /// </summary>
    public static class MembersClothesTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string Clothes = "clothes";
    }
}
