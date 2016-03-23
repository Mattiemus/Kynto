using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class SsoTicketsDatabaseTable : ISsoTicketsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The sso ticket id.
        /// </summary>
        private string _Ticket;

        /// <summary>
        /// The sso ticket id.
        /// </summary>
        public string Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                this._Ticket = value;
            }
        }

        /// <summary>
        /// The member id this ticket belongs too.
        /// </summary>
        private int _MemberId;

        /// <summary>
        /// The member id this ticket belongs too.
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
        /// The timestamp when this ticket was created.
        /// </summary>
        private int _Created;

        /// <summary>
        /// The timestamp when this ticket was created.
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
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public SsoTicketsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public SsoTicketsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case SsoTicketsTableFields.Ticket:
                        this._Ticket = (string)DataObject.Value;
                        break;

                    case SsoTicketsTableFields.MemberId:
                        this._MemberId = (int)DataObject.Value;
                        break;

                    case SsoTicketsTableFields.Created:
                        this._Created = (int)DataObject.Value;
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
            if (String.IsNullOrEmpty(this._Ticket))
            {
                throw new Exception("This row has not been set an id, an update query cannot be generated.");
            }

            // Create the update query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.SsoTickets)
                .Update()
                .Set(SsoTicketsTableFields.MemberId, this._MemberId)
                .Set(SsoTicketsTableFields.Created, this._Created)
                .Where(SsoTicketsTableFields.Ticket, DatabaseComparison.Equals, this._Ticket);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.SsoTickets)
                .Insert()
                .Values(SsoTicketsTableFields.Ticket, this._Ticket)
                .Values(SsoTicketsTableFields.MemberId, this._MemberId)
                .Values(SsoTicketsTableFields.Created, this._Created);
        }

        /// <summary>
        /// Creates a query that will delete this table.
        /// </summary>
        public IDatabaseQuery Delete()
        {
            if (String.IsNullOrEmpty(this._Ticket))
            {
                throw new Exception("This row has not been set an id, a delete query cannot be generated.");
            }

            // Create the delete query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.SsoTickets)
                .Delete()
                .Where(SsoTicketsTableFields.Ticket, DatabaseComparison.Equals, this._Ticket);
        }
    }
}
