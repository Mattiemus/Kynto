using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the clothes database table.
    /// </summary>
    public interface IClothesDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this clothing item.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The rank id that can by default use this clothing item.
        /// </summary>
        int RankId { get; set; }

        /// <summary>
        /// The sex that this clothing item belongs too.
        /// </summary>
        string Sex { get; set; }

        /// <summary>
        /// The type of this item.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// The item id in the filesystem.
        /// </summary>
        int ItemId { get; set; }
    }

    /// <summary>
    /// Represents the clothes table fields.
    /// </summary>
    public static class ClothesTableFields
    {
        public const string Id = "id";
        public const string RankId = "rank_id";
        public const string Sex = "sex";
        public const string Type = "type";
        public const string ItemId = "item_id";
    }
}
