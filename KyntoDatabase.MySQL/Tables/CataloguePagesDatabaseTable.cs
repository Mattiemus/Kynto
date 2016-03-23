using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{
    /// <summary>
    /// Represents the catalogue pages database table.
    /// </summary>
    public class CataloguePagesDatabaseTable : ICataloguePagesDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// Stores the catalogue page id.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The catalogue page id.
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
        /// Stores the title of this page.
        /// </summary>
        private string _Title;

        /// <summary>
        /// The title of this page.
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
        /// Stores the description of this page.
        /// </summary>
        private string _Description;

        /// <summary>
        /// The description of this page.
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        public CataloguePagesDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public CataloguePagesDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case CataloguePagesTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case CataloguePagesTableFields.Title:
                        this._Title = (string)DataObject.Value;
                        break;

                    case CataloguePagesTableFields.Description:
                        this._Description = (string)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.CataloguePages)
                .Update()
                .Set(CataloguePagesTableFields.Title, this._Title)
                .Set(CataloguePagesTableFields.Description, this._Description)
                .Where(CataloguePagesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table.
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.CataloguePages)
                .Insert()
                .Values(CataloguePagesTableFields.Title, this._Title)
                .Values(CataloguePagesTableFields.Description, this._Description);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.CataloguePages)
                .Delete()
                .Where(CataloguePagesTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
