using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the public rooms items database table.
    /// </summary>
    public interface IPublicsItemsDatabaseTable : IDatabaseTable, IItemsDatabaseTable
    {
    }

    /// <summary>
    /// Represents the publics items table fields.
    /// </summary>
    public static class PublicsItemsTableFields
    {
        public const string Id = "id";
        public const string Furni = "furni";
        public const string RoomId = "room_id";
        public const string Tile = "tile";
        public const string Action = "action";
        public const string Height = "height";
        public const string Rotation = "rotation";
        public const string StackNr = "stacknr";
    }
}
