using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class SavedImsDatabaseTable : ISavedImsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this im.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this im.
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
        /// The member id who created this im.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id who created this im.
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
        /// The member id of who this im was sent too.
        /// </summary>
        private int _FriendId;

        /// <summary>
        /// The member id of who this im was sent too.
        /// </summary>
        public int FriendId
        {
            get
            {
                return this._FriendId;
            }
            set
            {
                this._FriendId = value;
            }
        }

        /// <summary>
        /// The timestamp when this im was created.
        /// </summary>
        private int _Created;

        /// <summary>
        /// The timestamp when this im was created.
        /// </summary>
        public int Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                this._Created = value;
            }
        }

        /// <summary>
        /// The message contained in this im.
        /// </summary>
        private string _Message;

        /// <summary>
        /// The message contained in this im.
        /// </summary>
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public SavedImsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public SavedImsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case SavedImsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case SavedImsTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case SavedImsTableFields.FriendId:
                        this._FriendId = (int)DataObject.Value;
                        break;

                    case SavedImsTableFields.Created:
                        this._Created = (int)DataObject.Value;
                        break;

                    case SavedImsTableFields.Message:
                        this._Message = (string)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.SavedIms)
                .Update()
                .Set(SavedImsTableFields.MemberId, this._MemberId)
                .Set(SavedImsTableFields.FriendId, this._FriendId)
                .Set(SavedImsTableFields.Created, this._Created)
                .Set(SavedImsTableFields.Message, this._Message)
                .Where(SavedImsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.SavedIms)
                .Insert()
                .Values(SavedImsTableFields.MemberId, this._MemberId)
                .Values(SavedImsTableFields.FriendId, this._FriendId)
                .Values(SavedImsTableFields.Created, this._Created)
                .Values(SavedImsTableFields.Message, this._Message);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.SavedIms)
                .Delete()
                .Where(SavedImsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
