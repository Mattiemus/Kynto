using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class MessagesDatabaseTable : IMessagesDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this message
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this message
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
        /// The member id this message was sent to.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id this message was sent to.
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
        /// The member id of the sender of this message
        /// </summary>
        private int _FriendId;

        /// <summary>
        /// The member id of the sender of this message
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
        /// The timestamp when this message was sent.
        /// </summary>
        private int _Created;

        /// <summary>
        /// The timestamp when this message was sent.
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
        /// The title of this message.
        /// </summary>
        private string _Title;

        /// <summary>
        /// The title of this message.
        /// </summary>
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                this._Title = value;
            }
        }

        /// <summary>
        /// The message body.
        /// </summary>
        private string _Message;

        /// <summary>
        /// The message body.
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
        public MessagesDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public MessagesDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case MessagesTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case MessagesTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case MessagesTableFields.FriendId:
                        this._FriendId = (int)DataObject.Value;
                        break;

                    case MessagesTableFields.Created:
                        this._Created = (int)DataObject.Value;
                        break;

                    case MessagesTableFields.Title:
                        this._Title = (string)DataObject.Value;
                        break;

                    case MessagesTableFields.Message:
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Messages)
                .Update()
                .Set(MessagesTableFields.MemberId, this._MemberId)
                .Set(MessagesTableFields.FriendId, this._FriendId)
                .Set(MessagesTableFields.Created, this._Created)
                .Set(MessagesTableFields.Title, this._Title)
                .Set(MessagesTableFields.Message, this._Message)
                .Where(MessagesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Messages)
                .Insert()
                .Values(MessagesTableFields.MemberId, this._MemberId)
                .Values(MessagesTableFields.FriendId, this._FriendId)
                .Values(MessagesTableFields.Created, this._Created)
                .Values(MessagesTableFields.Title, this._Title)
                .Values(MessagesTableFields.Message, this._Message);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Messages)
                .Delete()
                .Where(MessagesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
