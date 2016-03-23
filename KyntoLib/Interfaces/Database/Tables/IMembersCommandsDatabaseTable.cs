using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the members commands database table.
    /// </summary>
    public interface IMembersCommandsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this set of avaliable commands.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The member id who owns these commands.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The array of avaliable commands.
        /// </summary>
        string Commands { get; set; }
    }

    /// <summary>
    /// Represents the members commands table fields.
    /// </summary>
    public static class MembersCommandsTableFields
    {
        public const string Id = "id";
        public const string MemberId = "member_id";
        public const string Commands = "commands";
    }
}
