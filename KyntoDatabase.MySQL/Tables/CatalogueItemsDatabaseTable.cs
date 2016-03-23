using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{
    /// <summary>
    /// Represents the catalogue items database table.
    /// </summary>
    public class CatalogueItemsDatabaseTable : ICatalogueItemsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// Stores the catalogue item's id.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The catalogue item's id.
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
        /// Stores the page id this item is in.
        /// </summary>
        private int _PageId;

        /// <summary>
        /// The page id that this item is in.
        /// </summary>
        public int PageId
        {
            get
            {
                return this._PageId;
            }
            set
            {
                this._PageId = value;
            }
        }

        /// <summary>
        /// Stores the furni type of this item.
        /// </summary>
        private string _Furni;

        /// <summary>
        /// The furni type of this item.
        /// </summary>
        public string Furni
        {
            get
            {
                return this._Furni;
            }
            set
            {
                this._Furni = value;
            }
        }

        /// <summary>
        /// Stores the cost of this item in silver.
        /// </summary>
        private int _CostSilver;

        /// <summary>
        /// The cost of this item in silver.
        /// </summary>
        public int CostSilver
        {
            get
            {
                return this._CostSilver;
            }
            set
            {
                this._CostSilver = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        public CatalogueItemsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public CatalogueItemsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case CatalogueItemsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case CatalogueItemsTableFields.PageId:
                        this._PageId = (int)DataObject.Value;
                        break;

                    case CatalogueItemsTableFields.Furni:
                        this._Furni = (string)DataObject.Value;
                        break;

                    case CatalogueItemsTableFields.CostSilver:
                        this._CostSilver = (int)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.CatalogueItems)
                .Update()
                .Set(CatalogueItemsTableFields.PageId, this._PageId)
                .Set(CatalogueItemsTableFields.Furni, this._Furni)
                .Set(CatalogueItemsTableFields.CostSilver, this._CostSilver)
                .Where(CatalogueItemsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table.
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.CatalogueItems)
                .Insert()
                .Values(CatalogueItemsTableFields.PageId, this._PageId)
                .Values(CatalogueItemsTableFields.Furni, this._Furni)
                .Values(CatalogueItemsTableFields.CostSilver, this._CostSilver);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.CatalogueItems)
                .Delete()
                .Where(CatalogueItemsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
