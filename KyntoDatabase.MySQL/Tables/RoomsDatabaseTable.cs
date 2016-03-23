using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class RoomsDatabaseTable : IRoomsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this room.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this room.
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
        /// The name of this room.
        /// </summary>
        private string _Name;

        /// <summary>
        /// The name of this room.
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
        /// The description of this room.
        /// </summary>
        private string _Description;

        /// <summary>
        /// The description of this room.
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
        /// The number of views this room has had.
        /// </summary>
        private int _Views;

        /// <summary>
        /// The number of views this room has had.
        /// </summary>
        public int Views
        {
            get
            {
                return this._Views;
            }
            set
            {
                this._Views = value;
            }
        }

        /// <summary>
        /// The owner id of this room.
        /// </summary>
        private int _OwnerId;

        /// <summary>
        /// The owner id of this room.
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
        /// The id of the floor of this room.
        /// </summary>
        private int _FloorId;

        /// <summary>
        /// The id of the floor of this room.
        /// </summary>
        public int FloorId
        {
            get
            {
                return this._FloorId;
            }
            set
            {
                this._FloorId = value;
            }
        }

        /// <summary>
        /// The id of the walls this room has.
        /// </summary>
        private int _WallId;

        /// <summary>
        /// The id of the walls this room has.
        /// </summary>
        public int WallId
        {
            get
            {
                return this._WallId;
            }
            set
            {
                this._WallId = value;
            }
        }

        /// <summary>
        /// The number of cols in this room.
        /// </summary>
        private int _Cols;

        /// <summary>
        /// The number of cols in this room.
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
        /// The number of rows in this room.
        /// </summary>
        private int _Rows;

        /// <summary>
        /// The number of rows in this room.
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
        /// The locations of the floor tile holes.
        /// </summary>
        private string _Holes;

        /// <summary>
        /// The locations of the floor tile holes.
        /// </summary>
        public string Holes
        {
            get
            {
                return this._Holes;
            }
            set
            {
                this._Holes = value;
            }
        }

        /// <summary>
        /// The background id.
        /// </summary>
        private int _BgId;

        /// <summary>
        /// The background id.
        /// </summary>
        public int BgId
        {
            get
            {
                return this._BgId;
            }
            set
            {
                this._BgId = value;
            }
        }

        /// <summary>
        /// The number of users currently in this room.
        /// </summary>
        private int _Users;

        /// <summary>
        /// The number of users currently in this room.
        /// </summary>
        public int Users
        {
            get
            {
                return this._Users;
            }
            set
            {
                this._Users = value;
            }
        }

        /// <summary>
        /// The entry tile location.
        /// </summary>
        private string _EntryTile;

        /// <summary>
        /// The entry tile location.
        /// </summary>
        public string EntryTile
        {
            get
            {
                return this._EntryTile;
            }
            set
            {
                this._EntryTile = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public RoomsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public RoomsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case RoomsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.Name:
                        this._Name = (string)DataObject.Value;
                        break;

                    case RoomsTableFields.Description:
                        this._Description = (string)DataObject.Value;
                        break;

                    case RoomsTableFields.Views:
                        this._Views = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.OwnerId:
                        this._OwnerId = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.FloorId:
                        this._FloorId = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.WallId:
                        this._WallId = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.Cols:
                        this._Cols = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.Rows:
                        this._Rows = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.Holes:
                        this._Holes = (string)DataObject.Value;
                        break;

                    case RoomsTableFields.BgId:
                        this._BgId = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.Users:
                        this._Users = (int)DataObject.Value;
                        break;

                    case RoomsTableFields.EntryTile:
                        this._EntryTile = (string)DataObject.Value;
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Rooms)
                .Update()
                .Set(RoomsTableFields.Name, this._Name)
                .Set(RoomsTableFields.Description, this._Description)
                .Set(RoomsTableFields.Views, this._Views)
                .Set(RoomsTableFields.OwnerId, this._OwnerId)
                .Set(RoomsTableFields.FloorId, this._FloorId)
                .Set(RoomsTableFields.WallId, this._WallId)
                .Set(RoomsTableFields.Cols, this._Cols)
                .Set(RoomsTableFields.Rows, this._Rows)
                .Set(RoomsTableFields.Holes, this._Holes)
                .Set(RoomsTableFields.BgId, this._BgId)
                .Set(RoomsTableFields.Users, this._Users)
                .Set(RoomsTableFields.EntryTile, this._EntryTile)
                .Where(RoomsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Rooms)
                .Insert()
                .Values(RoomsTableFields.Name, this._Name)
                .Values(RoomsTableFields.Description, this._Description)
                .Values(RoomsTableFields.Views, this._Views)
                .Values(RoomsTableFields.OwnerId, this._OwnerId)
                .Values(RoomsTableFields.FloorId, this._FloorId)
                .Values(RoomsTableFields.WallId, this._WallId)
                .Values(RoomsTableFields.Cols, this._Cols)
                .Values(RoomsTableFields.Rows, this._Rows)
                .Values(RoomsTableFields.Holes, this._Holes)
                .Values(RoomsTableFields.BgId, this._BgId)
                .Values(RoomsTableFields.Users, this._Users)
                .Values(RoomsTableFields.EntryTile, this._EntryTile);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Rooms)
                .Delete()
                .Where(RoomsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
