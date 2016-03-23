using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the catalogue pages database table.
    /// </summary>
    public interface ICataloguePagesDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this page.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The title of this page.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The description of this page.
        /// </summary>
        string Description { get; set; }
    }

    /// <summary>
    /// Represents the catalogue pages table fields.
    /// </summary>
    public static class CataloguePagesTableFields
    {
        public const string Id = "id";
        public const string Title = "title";
        public const string Description = "description";
    }
}
