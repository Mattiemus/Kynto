using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the public rooms database table.
    /// </summary>
    public interface IPublicsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this public room.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The name of this public room.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description of this public room.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The rooms heightmap string.
        /// </summary>
        string Heightmap { get; set; }

        /// <summary>
        /// The number of cols this public room takes up.
        /// </summary>
        int Cols { get; set; }

        /// <summary>
        /// The number of rows this public room takes up.
        /// </summary>
        int Rows { get; set; }

        /// <summary>
        /// The entry tile coordinate.
        /// </summary>
        string EntryTile { get; set; }

        /// <summary>
        /// The offset x.
        /// </summary>
        int OffsetX { get; set; }

        /// <summary>
        /// The offset y.
        /// </summary>
        int OffsetY { get; set; }

        /// <summary>
        /// The background layer id.
        /// </summary>
        int BgLayer { get; set; }

        /// <summary>
        /// The background image id.
        /// </summary>
        int BgImage { get; set; }

        /// <summary>
        /// The xml data for this room.
        /// </summary>
        string XmlData { get; set; }
    }

    /// <summary>
    /// Represents the public rooms table fields.
    /// </summary>
    public static class PublicsTableFields
    {
        public const string Id = "id";
        public const string Name = "name";
        public const string Description = "description";
        public const string Heightmap = "heightmap";
        public const string Cols = "cols";
        public const string Rows = "rows";
        public const string EntryTile = "entrytile";
        public const string OffsetX = "offset_x";
        public const string OffsetY = "offset_y";
        public const string BgLayer = "bg_layer";
        public const string BgImage = "bg_image";
        public const string XmlData = "xml_data";
    }
}
