using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the furni data database table.
    /// </summary>
    public interface IFurniDataDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of the specific item data field.
        /// </summary>
        int ItemDataId { get; set; }

        /// <summary>
        /// The id of the furni that this corresponds too.
        /// </summary>
        string ItemId { get; set; }

        /// <summary>
        /// The key of the data value.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// The value of the data item.
        /// </summary>
        string Value { get; set; }
    }

    /// <summary>
    /// Represents the furni data table fields.
    /// </summary>
    public static class FurniDataTableFields
    {
        public const string ItemDataId = "item_data_id";
        public const string ItemId = "item_id";
        public const string Key = "key";
        public const string Value = "value";
    }
}
