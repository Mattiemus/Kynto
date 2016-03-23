using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class FurniDataDatabaseTable : IFurniDataDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of the specific item data field.
        /// </summary>
        private int _ItemDataId = -1;

        /// <summary>
        /// The id of the specific item data field.
        /// </summary>
        public int ItemDataId
        {
            get
            {
                return this._ItemDataId;
            }
            set
            {
                this._ItemDataId = value;
            }
        }

        /// <summary>
        /// The id of the furni that this corresponds too.
        /// </summary>
        private string _ItemId;

        /// <summary>
        /// The id of the furni that this corresponds too.
        /// </summary>
        public string ItemId
        {
            get
            {
                return this._ItemId;
            }
            set
            {
                this._ItemId = value;
            }
        }

        /// <summary>
        /// The key of the data value.
        /// </summary>
        private string _Key;

        /// <summary>
        /// The key of the data value.
        /// </summary>
        public string Key
        {
            get
            {
                return this._Key;
            }
            set
            {
                this._Key = value;
            }
        }

        /// <summary>
        /// The value of the data item.
        /// </summary>
        private string _Value;

        /// <summary>
        /// The value of the data item.
        /// </summary>
        public string Value
        {
            get
            {
                return this._Value;
            }
            set
            {
                this._Value = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public FurniDataDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public FurniDataDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case FurniDataTableFields.ItemDataId:
                        this._ItemDataId = (int)DataObject.Value;
                        break;

                    case FurniDataTableFields.ItemId:
                        this._ItemId = (string)DataObject.Value;
                        break;

                    case FurniDataTableFields.Key:
                        this._Key = (string)DataObject.Value;
                        break;

                    case FurniDataTableFields.Value:
                        this._Value = (string)DataObject.Value;
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
            if (this._ItemDataId == -1)
            {
                throw new Exception("This row has not been set an id, an update query cannot be generated.");
            }

            // Create the update query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.FurniData)
                .Update()
                .Set(FurniDataTableFields.ItemId, this._ItemId)
                .Set(FurniDataTableFields.Key, this._Key)
                .Set(FurniDataTableFields.Value, this._Value)
                .Where(FurniDataTableFields.ItemDataId, DatabaseComparison.Equals, this._ItemDataId);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.FurniData)
                .Insert()
                .Values(FurniDataTableFields.ItemDataId, this._ItemDataId)
                .Values(FurniDataTableFields.ItemId, this._ItemId)
                .Values(FurniDataTableFields.Key, this._Key)
                .Values(FurniDataTableFields.Value, this._Value);
        }

        /// <summary>
        /// Creates a query that will delete this table.
        /// </summary>
        public IDatabaseQuery Delete()
        {
            if (this._ItemDataId == -1)
            {
                throw new Exception("This row has not been set an id, a delete query cannot be generated.");
            }

            // Create the delete query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.FurniData)
                .Delete()
                .Where(FurniDataTableFields.ItemDataId, DatabaseComparison.Equals, this._ItemDataId);
        }
    }
}
