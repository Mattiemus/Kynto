using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database.Query;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Interfaces with a database table.
    /// </summary>
    public interface IDatabaseTable
    {
        /// <summary>
        /// Creates a query that will update this table.
        /// </summary>
        IDatabaseQuery Update();

        /// <summary>
        /// Creates a query that will save this table.
        /// </summary>
        IDatabaseQuery Save();

        /// <summary>
        /// Creates a query that will delete this table.
        /// </summary>
        IDatabaseQuery Delete();

        /// <summary>
        /// Fills this table with data from a dictionary.
        /// </summary>
        /// <param name="Data">The dictionary containing the data for this table.</param>
        void FromDictionary(Dictionary<string, object> Data);
    }
}
