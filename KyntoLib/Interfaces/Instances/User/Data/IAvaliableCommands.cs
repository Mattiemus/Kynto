using System;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Instances.User.Data
{
    /// <summary>
    /// Represents all possible available commands.
    /// </summary>
    public interface IAvaliableCommands
    {
        /// <summary>
        /// Parses the table, reading the available commands data.
        /// </summary>
        /// <param name="Table">The table to parse.</param>
        void Parse(IMembersCommandsDatabaseTable Table);
    }
}
