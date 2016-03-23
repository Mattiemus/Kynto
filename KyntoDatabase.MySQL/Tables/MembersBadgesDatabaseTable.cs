using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class MembersBadgesDatabaseTable : IMembersBadgesDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this set of badges.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this set of badges.
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
        /// The member id of the owner of these badges.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id of the owner of these badges.
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
        /// The badges.
        /// </summary>
        private string _Badges;

        /// <summary>
        /// The badges.
        /// </summary>
        public string Badges
        {
            get
            {
                return this._Badges;
            }
            set
            {
                this._Badges = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public MembersBadgesDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public MembersBadgesDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case MembersBadgesTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case MembersBadgesTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case MembersBadgesTableFields.Badges:
                        this._Badges = (string)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersBadges)
                .Update()
                .Set(MembersBadgesTableFields.MemberId, this._MemberId)
                .Set(MembersBadgesTableFields.Badges, this._Badges)
                .Where(MembersBadgesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersBadges)
                .Insert()
                .Values(MembersBadgesTableFields.MemberId, this._MemberId)
                .Values(MembersBadgesTableFields.Badges, this._Badges);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.MembersBadges)
                .Delete()
                .Where(MembersBadgesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
