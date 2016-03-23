using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class MembersBanDatabaseTable : IMembersBanDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this ban.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this ban.
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
        /// The id of the member being banned.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The id of the member being banned.
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
        /// The timestamp when this ban was created.
        /// </summary>
        private int _Created;

        /// <summary>
        /// The timestamp when this ban was created.
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
        /// The id of the member who created this ban.
        /// </summary>
        private int _ById;

        /// <summary>
        /// The id of the member who created this ban.
        /// </summary>
        public int ById
        {
            get
            {
                return this._ById;
            }
            set
            {
                this._ById = value;
            }
        }

        /// <summary>
        /// The reason this ban was made.
        /// </summary>
        private string _Reason;

        /// <summary>
        /// The reason this ban was made.
        /// </summary>
        public string Reason
        {
            get
            {
                return this._Reason;
            }
            set
            {
                this._Reason = value;
            }
        }

        /// <summary>
        /// The length of time this ban will last for.
        /// </summary>
        private int _Length;

        /// <summary>
        /// The length of time this ban will last for.
        /// </summary>
        public int Length
        {
            get
            {
                return this._Length;
            }
            set
            {
                this._Length = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public MembersBanDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public MembersBanDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case MembersBanTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case MembersBanTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case MembersBanTableFields.Created:
                        this._Created = (int)DataObject.Value;
                        break;

                    case MembersBanTableFields.ById:
                        this._ById = (int)DataObject.Value;
                        break;

                    case MembersBanTableFields.Reason:
                        this._Reason = (string)DataObject.Value;
                        break;

                    case MembersBanTableFields.Length:
                        this._Length = (int)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MemberBan)
                .Update()
                .Set(MembersBanTableFields.MemberId, this._MemberId)
                .Set(MembersBanTableFields.Created, this._Created)
                .Set(MembersBanTableFields.ById, this._ById)
                .Set(MembersBanTableFields.Reason, this._Reason)
                .Set(MembersBanTableFields.Length, this._Length)
                .Where(MembersBanTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MemberBan)
                .Insert()
                .Values(MembersBanTableFields.MemberId, this._Created)
                .Values(MembersBanTableFields.Created, this._Created)
                .Values(MembersBanTableFields.ById, this._ById)
                .Values(MembersBanTableFields.Reason, this._Reason)
                .Values(MembersBanTableFields.Length, this._Length);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MemberBan)
                .Delete()
                .Where(MembersBanTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
