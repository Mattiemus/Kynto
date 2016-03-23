using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class PublicsItemsDatabaseTable : IPublicsItemsDatabaseTable
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
        public int OwnerId
        {
            get
            {
                return -1;
            }
            set
            {
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
        /// The rotation of this item.
        /// </summary>
        private int _Rotation;

        /// <summary>
        /// The rotation of this item.
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
        public PublicsItemsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public PublicsItemsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case PublicsItemsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.Furni:
                        this._Furni = (string)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.RoomId:
                        this._RoomId = (int)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.Tile:
                        this._Tile = (string)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.Action:
                        this._Action = (bool)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.Height:
                        this._Height = (int)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.Rotation:
                        this._Rotation = (int)DataObject.Value;
                        break;

                    case PublicsItemsTableFields.StackNr:
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.PublicsItems)
                .Update()
                .Set(PublicsItemsTableFields.Furni, this._Furni)
                .Set(PublicsItemsTableFields.RoomId, this._RoomId)
                .Set(PublicsItemsTableFields.Tile, this._Tile)
                .Set(PublicsItemsTableFields.Action, this._Action)
                .Set(PublicsItemsTableFields.Height, this._Height)
                .Set(PublicsItemsTableFields.Rotation, this._Rotation)
                .Set(PublicsItemsTableFields.StackNr, this._StackNr)
                .Where(PublicsItemsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.PublicsItems)
                .Insert()
                .Values(PublicsItemsTableFields.Furni, this._Furni)
                .Values(PublicsItemsTableFields.RoomId, this._RoomId)
                .Values(PublicsItemsTableFields.Tile, this._Tile)
                .Values(PublicsItemsTableFields.Action, this._Action)
                .Values(PublicsItemsTableFields.Height, this._Height)
                .Values(PublicsItemsTableFields.Rotation, this._Rotation)
                .Values(PublicsItemsTableFields.StackNr, this._StackNr);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.PublicsItems)
                .Delete()
                .Where(PublicsItemsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
