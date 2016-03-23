using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the items database table.
    /// </summary>
    public interface IItemsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this item.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The furni type of this item.
        /// </summary>
        string Furni { get; set; }

        /// <summary>
        /// The owner of this item.
        /// </summary>
        int OwnerId { get; set; }

        /// <summary>
        /// The room id this item is in.
        /// </summary>
        int RoomId { get; set; }

        /// <summary>
        /// The tile this item is on.
        /// </summary>
        string Tile { get; set; }

        /// <summary>
        /// The action flag.
        /// </summary>
        bool Action { get; set; }

        /// <summary>
        /// The height this item is at.
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// The rotation this item is in.
        /// </summary>
        int Rotation { get; set; }

        /// <summary>
        /// The stack number of this item.
        /// </summary>
        int StackNr { get; set; }
    }

    /// <summary>
    /// Represents the items table fields.
    /// </summary>
    public static class ItemsTableFields
    {
        public const string Id = "id";
        public const string Furni = "furni";
        public const string OwnerId = "owner_id";
        public const string RoomId = "room_id";
        public const string Tile = "tile";
        public const string Action = "action";
        public const string Height = "height";
        public const string Rotation = "rotation";
        public const string StackNr = "stacknr";
    }
}
