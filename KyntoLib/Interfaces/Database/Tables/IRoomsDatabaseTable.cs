using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the rooms database table.
    /// </summary>
    public interface IRoomsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this room.
        /// </summary>
        int Id  { get; set; }

        /// <summary>
        /// The name of this room.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description of this room.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The number of views this room has had.
        /// </summary>
        int Views { get; set; }

        /// <summary>
        /// The owner id of this room.
        /// </summary>
        int OwnerId { get; set; }

        /// <summary>
        /// The id of the floor of this room.
        /// </summary>
        int FloorId { get; set; }

        /// <summary>
        /// The id of the walls this room has.
        /// </summary>
        int WallId { get; set; }

        /// <summary>
        /// The number of cols in this room.
        /// </summary>
        int Cols { get; set; }

        /// <summary>
        /// The number of rows in this room.
        /// </summary>
        int Rows { get; set; }

        /// <summary>
        /// The locations of the floor tile holes.
        /// </summary>
        string Holes { get; set; }

        /// <summary>
        /// The background id.
        /// </summary>
        int BgId { get; set; }

        /// <summary>
        /// The number of users currently in this room.
        /// </summary>
        int Users { get; set; }

        /// <summary>
        /// The entry tile location.
        /// </summary>
        string EntryTile { get; set; }
    }

    /// <summary>
    /// Represents the private rooms table fields.
    /// </summary>
    public static class RoomsTableFields
    {
        public const string Id = "id";
        public const string Name = "name";
        public const string Description = "description";
        public const string Views = "views";
        public const string OwnerId = "owner_id";
        public const string FloorId = "floor_id";
        public const string WallId = "wall_id";
        public const string Cols = "cols";
        public const string Rows = "rows";
        public const string Holes = "holes";
        public const string BgId = "bg_id";
        public const string Users = "users";
        public const string EntryTile = "entrytile";
    }
}
