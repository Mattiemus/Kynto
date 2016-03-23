using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class MembersFriendsDatabaseTable : IMembersFriendsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id for this array of friends.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id for this array of friends.
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
        /// The member id.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id.
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
        /// The friend id.
        /// </summary>
        private int _FriendId;

        /// <summary>
        /// The friend id.
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
        /// The timestamp that this friendship was created.
        /// </summary>
        private int _Created;

        /// <summary>
        /// The timestamp that this friendship was created.
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
        /// Weather or not this friendship is active.
        /// </summary>
        private bool _Active;

        /// <summary>
        /// Weather or not this friendship is active.
        /// </summary>
        public bool Active
        {
            get
            {
                return this._Active;
            }
            set
            {
                this._Active = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public MembersFriendsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public MembersFriendsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case MembersFriendsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case MembersFriendsTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case MembersFriendsTableFields.FriendId:
                        this._FriendId = (int)DataObject.Value;
                        break;

                    case MembersFriendsTableFields.Created:
                        this._Created = (int)DataObject.Value;
                        break;

                    case MembersFriendsTableFields.Active:
                        this._Active = (bool)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersFriends)
                .Update()
                .Set(MembersFriendsTableFields.MemberId, this._MemberId)
                .Set(MembersFriendsTableFields.FriendId, this._FriendId)
                .Set(MembersFriendsTableFields.Created, this._Created)
                .Set(MembersFriendsTableFields.Active, this._Active)
                .Where(MembersFriendsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersFriends)
                .Insert()
                .Values(MembersFriendsTableFields.MemberId, this._MemberId)
                .Values(MembersFriendsTableFields.FriendId, this._FriendId)
                .Values(MembersFriendsTableFields.Created, this._Created)
                .Values(MembersFriendsTableFields.Active, this._Active);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersFriends)
                .Delete()
                .Where(MembersFriendsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
