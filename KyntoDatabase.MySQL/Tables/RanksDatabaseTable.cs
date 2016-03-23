using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class RanksDatabaseTable : IRanksDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this rank.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this rank.
        /// </summary>
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        /// <summary>
        /// The rights this rank has.
        /// </summary>
        private string _Rights;

        /// <summary>
        /// The rights this rank has.
        /// </summary>
        public string Rights
        {
            get
            {
                return this._Rights;
            }
            set
            {
                this._Rights = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public RanksDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public RanksDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
            FromDictionary(Data);
        }

        /// <summary>
        /// Fills this table with data from a dictionary.
        /// </summary>
        /// <param name="Data">The dictionary containing the data for this table.</param>
        public void FromDictionary(Dictionary<string, object> Data)
        {
            // Process the data.
            foreach (KeyValuePair<string, object> DataObject in Data)
            {
                switch (DataObject.Key)
                {
                    case RanksTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case RanksTableFields.Rights:
                        this._Rights = (string)DataObject.Value;
                        break;

                    default:
                        throw new Exception("Unknown database column \"" + DataObject.Key + "\"");
                }
            }
        }

        /// <summary>
        /// Creates a query that will update this table.
        /// </summary>
        public IDatabaseQuery Update()
        {
            if (this._Id == -1)
            {
                throw new Exception("This row has not been set an id, an update query cannot be generated.");
            }

            // Create the update query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Ranks)
                .Update()
                .Set(RanksTableFields.Rights, this._Rights)
                .Where(RanksTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Ranks)
                .Insert()
                .Values(RanksTableFields.Rights, this._Rights);
        }

        /// <summary>
        /// Creates a query that will delete this table.
        /// </summary>
        public IDatabaseQuery Delete()
        {
            if (this._Id == -1)
            {
                throw new Exception("This row has not been set an id, a delete query cannot be generated.");
            }

            // Create the delete query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Ranks)
                .Delete()
                .Where(RanksTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
