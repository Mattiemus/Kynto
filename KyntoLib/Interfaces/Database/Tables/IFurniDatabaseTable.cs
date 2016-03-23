using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the furni database table.
    /// </summary>
    public interface IFurniDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this furni item.
        /// </summary>
        string Furni { get; set; }

        /// <summary>
        /// The name of this item.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description of this item.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The type of this item.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// If this item has an action.
        /// </summary>
        bool Action { get; set; }

        /// <summary>
        /// If this item can be stacked.
        /// </summary>
        bool Stacking { get; set; }

        /// <summary>
        /// The rows this item takes up.
        /// </summary>
        int Rows { get; set; }

        /// <summary>
        /// The cols this item takes up.
        /// </summary>
        int Cols { get; set; }

        /// <summary>
        /// The height of the item.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// The class this item uses for scriptable actions.
        /// </summary>
        string Class { get; set; }

        /// <summary>
        /// The xml data for this item.
        /// </summary>
        string XmlData { get; set; }
    }

    /// <summary>
    /// Represents the furni table fields.
    /// </summary>
    public static class FurniTableFields
    {
        public const string Furni = "furni";
        public const string Name = "name";
        public const string Description = "description";
        public const string Type = "type";
        public const string Action = "action";
        public const string Stacking = "stacking";
        public const string Rows = "rows";
        public const string Cols = "cols";
        public const string Height = "height";
        public const string Class = "class";
        public const string XmlData = "xml_data";
    }
}
