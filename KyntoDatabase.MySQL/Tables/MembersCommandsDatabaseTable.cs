using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class MembersCommandsDatabaseTable : IMembersCommandsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this set of avaliable commands.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this set of avaliable commands.
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
        /// The member id who owns these commands.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id who owns these commands.
        /// </summary>
        public int MemberId
        {
            get
            {
                return this._MemberId;
            }
            set
            {
                this._MemberId = value;
            }
        }

        /// <summary>
        /// The array of avaliable commands.
        /// </summary>
        private string _Commands;

        /// <summary>
        /// The array of avaliable commands.
        /// </summary>
        public string Commands
        {
            get
            {
                return this._Commands;
            }
            set
            {
                this._Commands = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public MembersCommandsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public MembersCommandsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case MembersCommandsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case MembersCommandsTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case MembersCommandsTableFields.Commands:
                        this._Commands = (string)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersCommands)
                .Update()
                .Set(MembersCommandsTableFields.MemberId, this._MemberId)
                .Set(MembersCommandsTableFields.Commands, this._Commands)
                .Where(MembersCommandsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersCommands)
                .Insert()
                .Values(MembersCommandsTableFields.MemberId, this._MemberId)
                .Values(MembersCommandsTableFields.Commands, this._Commands);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersCommands)
                .Delete()
                .Where(MembersCommandsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
