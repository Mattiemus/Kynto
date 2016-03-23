using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{
    /// <summary>
    /// Represents the clothes database table.
    /// </summary>
    public class ClothesDatabaseTable : IClothesDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// Stores the id of this clothing item.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this clothing item.
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
        /// Stores the rank id that can by default use this item.
        /// </summary>
        private int _RankId;

        /// <summary>
        /// The rank id that can by default use this clothing item.
        /// </summary>
        public int RankId
        {
            get
            {
                return this._RankId;
            }
            set
            {
                this._RankId = value;
            }
        }

        /// <summary>
        /// Stores the sex that this clothing item belongs too.
        /// </summary>
        private string _Sex;

        /// <summary>
        /// The sex that this clothing item belongs too.
        /// </summary>
        public string Sex
        {
            get
            {
                return this._Sex;
            }
            set
            {
                this._Sex = value;
            }
        }

        /// <summary>
        /// Stores the type of this item.
        /// </summary>
        private string _Type;

        /// <summary>
        /// The type of this item.
        /// </summary>
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }

        /// <summary>
        /// Stores the id of this item in the filesystem.
        /// </summary>
        private int _ItemId;

        /// <summary>
        /// The item id in the filesystem.
        /// </summary>
        public int ItemId
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
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        public ClothesDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public ClothesDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case ClothesTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case ClothesTableFields.RankId:
                        this._RankId = (int)DataObject.Value;
                        break;

                    case ClothesTableFields.Sex:
                        this._Sex = (string)DataObject.Value;
                        break;

                    case ClothesTableFields.Type:
                        this._Type = (string)DataObject.Value;
                        break;

                    case ClothesTableFields.ItemId:
                        this._ItemId = (int)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Clothes)
                .Update()
                .Set(ClothesTableFields.RankId, this._RankId)
                .Set(ClothesTableFields.Sex, this._Sex)
                .Set(ClothesTableFields.Type, this._Type)
                .Set(ClothesTableFields.ItemId, this._ItemId)
                .Where(ClothesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table.
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Clothes)
                .Insert()
                .Values(ClothesTableFields.RankId, this._RankId)
                .Values(ClothesTableFields.Sex, this._Sex)
                .Values(ClothesTableFields.Type, this._Type)
                .Values(ClothesTableFields.ItemId, this._ItemId);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Clothes)
                .Delete()
                .Where(ClothesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
