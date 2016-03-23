using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the catalogue items database table.
    /// </summary>
    public interface ICatalogueItemsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The catalogue item's id.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The page id that this item is in.
        /// </summary>
        int PageId { get; set; }

        /// <summary>
        /// The furni type of this item.
        /// </summary>
        string Furni { get; set; }

        /// <summary>
        /// The cost of this item in silver.
        /// </summary>
        int CostSilver { get; set; }
    }

    /// <summary>
    /// Represents the catalogue items table fields.
    /// </summary>
    public static class CatalogueItemsTableFields
    {
        public const string Id = "id";
        public const string PageId = "page_id";
        public const string Furni = "furni";
        public const string CostSilver = "cost_silver";
    }
}
