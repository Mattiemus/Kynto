using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User.Data;

namespace KyntoServer.Instances.User.Data
{
    /// <summary>
    /// Represents all possible available commands.
    /// </summary>
    public class AvaliableCommands : IAvaliableCommands
    {
        /// <summary>
        /// Parses the table, reading the available commands data.
        /// </summary>
        /// <param name="Table">The table to parse.</param>
        public void Parse(IMembersCommandsDatabaseTable Table)
        {
        }
    }
}
