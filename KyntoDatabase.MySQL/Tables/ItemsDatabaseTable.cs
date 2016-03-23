using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class ItemsDatabaseTable : IItemsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this item.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this item.
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
        /// The furni type of this item.
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
        /// The owner of this item.
        /// </summary>
        private int _OwnerId;

        /// <summary>
        /// The owner of this item.
        /// </summary>
        public int OwnerId
        {
            get
            {
                return this._OwnerId;
            }
            set
            {
                this._OwnerId = value;
            }
        }

        /// <summary>
        /// The room id this item is in.
        /// </summary>
        private int _RoomId;

        /// <summary>
        /// The room id this item is in.
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
        /// The tile this item is on.
        /// </summary>
        private string _Tile;

        /// <summary>
        /// The tile this item is on.
        /// </summary>
        public string Tile
        {
            get
            {
                return this._Tile;
            }
            set
            {
                this._Tile = value;
            }
        }

        /// <summary>
        /// The action flag.
        /// </summary>
        private bool _Action;

        /// <summary>
        /// The action flag.
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
        /// The height this item is at.
        /// </summary>
        private int _Height;

        /// <summary>
        /// The height this item is at.
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
        /// The rotation this item is in.
        /// </summary>
        private int _Rotation;

        /// <summary>
        /// The rotation this item is in.
        /// </summary>
        public int Rotation
        {
            get
            {
                return this._Rotation;
            }
            set
            {
                this._Rotation = value;
            }
        }

        /// <summary>
        /// The stack number of this item.
        /// </summary>
        private int _StackNr;

        /// <summary>
        /// The stack number of this item.
        /// </summary>
        public int StackNr
        {
            get
            {
                return this._StackNr;
            }
            set
            {
                this._StackNr = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public ItemsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public ItemsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case ItemsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case ItemsTableFields.Furni:
                        this._Furni = (string)DataObject.Value;
                        break;

                    case ItemsTableFields.OwnerId:
                        this._OwnerId = (int)DataObject.Value;
                        break;

                    case ItemsTableFields.RoomId:
                        this._RoomId = (int)DataObject.Value;
                        break;

                    case ItemsTableFields.Tile:
                        this._Tile = (string)DataObject.Value;
                        break;

                    case ItemsTableFields.Action:
                        this._Action = (bool)DataObject.Value;
                        break;

                    case ItemsTableFields.Height:
                        this._Height = (int)DataObject.Value;
                        break;

                    case ItemsTableFields.Rotation:
                        this._Rotation = (int)DataObject.Value;
                        break;

                    case ItemsTableFields.StackNr:
                        this._StackNr = (int)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Items)
                .Update()
                .Set(ItemsTableFields.Furni, this._Furni)
                .Set(ItemsTableFields.OwnerId, this._OwnerId)
                .Set(ItemsTableFields.RoomId, this._RoomId)
                .Set(ItemsTableFields.Tile, this._Tile)
                .Set(ItemsTableFields.Action, this._Action)
                .Set(ItemsTableFields.Height, this._Height)
                .Set(ItemsTableFields.Rotation, this._Rotation)
                .Set(ItemsTableFields.StackNr, this._StackNr)
                .Where(ItemsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Items)
                .Insert()
                .Values(ItemsTableFields.Furni, this._Furni)
                .Values(ItemsTableFields.OwnerId, this._OwnerId)
                .Values(ItemsTableFields.RoomId, this._RoomId)
                .Values(ItemsTableFields.Tile, this._Tile)
                .Values(ItemsTableFields.Action, this._Action)
                .Values(ItemsTableFields.Height, this._Height)
                .Values(ItemsTableFields.Rotation, this._Rotation)
                .Values(ItemsTableFields.StackNr, this._StackNr);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Items)
                .Delete()
                .Where(ItemsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
