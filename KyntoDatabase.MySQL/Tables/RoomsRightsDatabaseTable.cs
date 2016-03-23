using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class RoomsRightsDatabaseTable : IRoomsRightsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this room right.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this room right.
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
        /// The room id this rights set belongs too.
        /// </summary>
        private int _RoomId;

        /// <summary>
        /// The room id this rights set belongs too.
        /// </summary>
        public int RoomId
        {
            get
            {
                return this._RoomId;
            }
            set
            {
                this._RoomId = value;
            }
        }

        /// <summary>
        /// The member id this set of rights belongs to.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id this set of rights belongs to.
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
        /// The rights this member has.
        /// </summary>
        private string _Rights;

        /// <summary>
        /// The rights this member has.
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
        public RoomsRightsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public RoomsRightsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case RoomsRightsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case RoomsRightsTableFields.RoomId:
                        this._RoomId = (int)DataObject.Value;
                        break;

                    case RoomsRightsTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case RoomsRightsTableFields.Rights:
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.RoomsRights)
                .Update()
                .Set(RoomsRightsTableFields.RoomId, this._RoomId)
                .Set(RoomsRightsTableFields.MemberId, this._MemberId)
                .Set(RoomsRightsTableFields.Rights, this._Rights)
                .Where(RoomsRightsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.RoomsRights)
                .Insert()
                .Values(RoomsRightsTableFields.RoomId, this._RoomId)
                .Values(RoomsRightsTableFields.MemberId, this._MemberId)
                .Values(RoomsRightsTableFields.Rights, this._Rights);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.RoomsRights)
                .Delete()
                .Where(RoomsRightsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
