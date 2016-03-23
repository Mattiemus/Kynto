using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the ranks database table.
    /// </summary>
    public interface IRanksDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this rank.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The rights this rank has.
        /// </summary>
        string Rights { get; set; }
    }

    /// <summary>
    /// Represents the ranks table fields.
    /// </summary>
    public static class RanksTableFields
    {
        public const string Id = "id";
        public const string Rights = "rights";
    }
}
