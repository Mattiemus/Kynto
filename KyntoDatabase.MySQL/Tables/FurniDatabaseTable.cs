using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class FurniDatabaseTable : IFurniDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this furni item.
        /// </summary>
        private string _Furni;

        /// <summary>
        /// The id of this furni item.
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
        /// The name of this item.
        /// </summary>
        private string _Name;

        /// <summary>
        /// The name of this item.
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        /// <summary>
        /// The description of this item.
        /// </summary>
        private string _Description;

        /// <summary>
        /// The description of this item.
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
        /// The type of this item.
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
        /// If this item has an action.
        /// </summary>
        private bool _Action;

        /// <summary>
        /// If this item has an action.
        /// </summary>
        public bool Action
        {
            get
            {
                return this._Action;
            }
            set
            {
                this._Action = value;
            }
        }

        /// <summary>
        /// If this item can be stacked.
        /// </summary>
        private bool _Stacking;

        /// <summary>
        /// If this item can be stacked.
        /// </summary>
        public bool Stacking
        {
            get
            {
                return this._Stacking;
            }
            set
            {
                this._Stacking = value;
            }
        }

        /// <summary>
        /// The rows this item takes up.
        /// </summary>
        private int _Rows;

        /// <summary>
        /// The rows this item takes up.
        /// </summary>
        public int Rows
        {
            get
            {
                return this._Rows;
            }
            set
            {
                this._Rows = value;
            }
        }

        /// <summary>
        /// The cols this item takes up.
        /// </summary>
        private int _Cols;

        /// <summary>
        /// The cols this item takes up.
        /// </summary>
        public int Cols
        {
            get
            {
                return this._Cols;
            }
            set
            {
                this._Cols = value;
            }
        }

        /// <summary>
        /// The height of the item.
        /// </summary>
        private int _Height;

        /// <summary>
        /// The height of the item.
        /// </summary>
        public int Height
        {
            get
            {
                return this._Height;
            }
            set
            {
                this._Height = value;
            }
        }

        /// <summary>
        /// The class this item uses for scriptable actions.
        /// </summary>
        private string _Class;

        /// <summary>
        /// The class this item uses for scriptable actions.
        /// </summary>
        public string Class
        {
            get
            {
                return this._Class;
            }
            set
            {
                this._Class = value;
            }
        }

        /// <summary>
        /// The xml data for this item.
        /// </summary>
        private string _XmlData;

        /// <summary>
        /// The xml data for this item.
        /// </summary>
        public string XmlData
        {
            get
            {
                return this._XmlData;
            }
            set
            {
                this._XmlData = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public FurniDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public FurniDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case FurniTableFields.Furni:
                        this._Furni = (string)DataObject.Value;
                        break;

                    case FurniTableFields.Name:
                        this._Name = (string)DataObject.Value;
                        break;

                    case FurniTableFields.Description:
                        this._Description = (string)DataObject.Value;
                        break;

                    case FurniTableFields.Type:
                        this._Type = (string)DataObject.Value;
                        break;

                    case FurniTableFields.Action:
                        this._Action = (bool)DataObject.Value;
                        break;

                    case FurniTableFields.Stacking:
                        this._Stacking = (bool)DataObject.Value;
                        break;

                    case FurniTableFields.Rows:
                        this._Rows = (int)DataObject.Value;
                        break;

                    case FurniTableFields.Cols:
                        this._Cols = (int)DataObject.Value;
                        break;

                    case FurniTableFields.Height:
                        this._Height = (int)DataObject.Value;
                        break;

                    case FurniTableFields.Class:
                        this._Class = (string)DataObject.Value;
                        break;

                    case FurniTableFields.XmlData:
                        this._XmlData = (string)DataObject.Value;
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
            if (String.IsNullOrEmpty(this._Furni))
            {
                throw new Exception("This row has not been set an id, an update query cannot be generated.");
            }

            // Create the update query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Furni)
                .Update()
                .Set(FurniTableFields.Name, this._Name)
                .Set(FurniTableFields.Description, this._Description)
                .Set(FurniTableFields.Type, this._Type)
                .Set(FurniTableFields.Action, this._Action)
                .Set(FurniTableFields.Stacking, this._Stacking)
                .Set(FurniTableFields.Rows, this._Rows)
                .Set(FurniTableFields.Cols, this._Cols)
                .Set(FurniTableFields.Height, this._Height)
                .Set(FurniTableFields.Class, this._Class)
                .Set(FurniTableFields.XmlData, this._XmlData)
                .Where(FurniTableFields.Furni, DatabaseComparison.Equals, this._Furni);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Furni)
                .Insert()
                .Values(FurniTableFields.Furni, this._Furni)
                .Values(FurniTableFields.Name, this._Name)
                .Values(FurniTableFields.Description, this._Description)
                .Values(FurniTableFields.Type, this._Type)
                .Values(FurniTableFields.Action, this._Action)
                .Values(FurniTableFields.Stacking, this._Stacking)
                .Values(FurniTableFields.Rows, this._Rows)
                .Values(FurniTableFields.Cols, this._Cols)
                .Values(FurniTableFields.Height, this._Height)
                .Values(FurniTableFields.Class, this._Class)
                .Values(FurniTableFields.XmlData, this._XmlData);
        }

        /// <summary>
        /// Creates a query that will delete this table.
        /// </summary>
        public IDatabaseQuery Delete()
        {
            if (String.IsNullOrEmpty(this._Furni))
            {
                throw new Exception("This row has not been set an id, a delete query cannot be generated.");
            }

            // Create the delete query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Furni)
                .Delete()
                .Where(FurniTableFields.Furni, DatabaseComparison.Equals, this._Furni);
        }
    }
}
