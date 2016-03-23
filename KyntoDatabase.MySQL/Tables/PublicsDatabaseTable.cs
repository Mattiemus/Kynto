using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class PublicsDatabaseTable : IPublicsDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this public room.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this public room.
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
        /// The name of this public room.
        /// </summary>
        private string _Name;

        /// <summary>
        /// The name of this public room.
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
        /// The description of this public room.
        /// </summary>
        private string _Description;

        /// <summary>
        /// The description of this public room.
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
        /// The rooms heightmap string.
        /// </summary>
        private string _Heightmap;

        /// <summary>
        /// The rooms heightmap string.
        /// </summary>
        public string Heightmap
        {
            get
            {
                return this._Heightmap;
            }
            set
            {
                this._Heightmap = value;
            }
        }

        /// <summary>
        /// The number of cols this public room takes up.
        /// </summary>
        private int _Cols;

        /// <summary>
        /// The number of cols this public room takes up.
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
        /// The number of rows this public room takes up.
        /// </summary>
        private int _Rows;

        /// <summary>
        /// The number of rows this public room takes up.
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
        /// The entry tile coordinate.
        /// </summary>
        private string _EntryTile;

        /// <summary>
        /// The entry tile coordinate.
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
        /// The offset x.
        /// </summary>
        private int _OffsetX;

        /// <summary>
        /// The offset x.
        /// </summary>
        public int OffsetX
        {
            get
            {
                return this._OffsetX;
            }
            set
            {
                this._OffsetX = value;
            }
        }

        /// <summary>
        /// The offset y.
        /// </summary>
        private int _OffsetY;

        /// <summary>
        /// The offset y.
        /// </summary>
        public int OffsetY
        {
            get
            {
                return this._OffsetY;
            }
            set
            {
                this._OffsetY = value;
            }
        }

        /// <summary>
        /// The background layer id.
        /// </summary>
        private int _BgLayer;

        /// <summary>
        /// The background layer id.
        /// </summary>
        public int BgLayer
        {
            get
            {
                return this._BgLayer;
            }
            set
            {
                this._BgLayer = value;
            }
        }

        /// <summary>
        /// The background image id.
        /// </summary>
        private int _BgImage;

        /// <summary>
        /// The background image id.
        /// </summary>
        public int BgImage
        {
            get
            {
                return this._BgImage;
            }
            set
            {
                this._BgImage = value;
            }
        }

        /// <summary>
        /// The xml data for this room.
        /// </summary>
        private string _XmlData;

        /// <summary>
        /// The xml data for this room.
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
        public PublicsDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public PublicsDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
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
                    case PublicsTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.Name:
                        this._Name = (string)DataObject.Value;
                        break;

                    case PublicsTableFields.Description:
                        this._Description = (string)DataObject.Value;
                        break;

                    case PublicsTableFields.Heightmap:
                        this._Heightmap = (string)DataObject.Value;
                        break;

                    case PublicsTableFields.Cols:
                        this._Cols = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.Rows:
                        this._Rows = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.EntryTile:
                        this._EntryTile = (string)DataObject.Value;
                        break;

                    case PublicsTableFields.OffsetX:
                        this._OffsetX = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.OffsetY:
                        this._OffsetY = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.BgLayer:
                        this._BgLayer = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.BgImage:
                        this._BgImage = (int)DataObject.Value;
                        break;

                    case PublicsTableFields.XmlData:
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
            if (this._Id == -1)
            {
                throw new Exception("This row has not been set an id, an update query cannot be generated.");
            }

            // Create the update query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Publics)
                .Update()
                .Set(PublicsTableFields.Name, this._Name)
                .Set(PublicsTableFields.Description, this._Description)
                .Set(PublicsTableFields.Heightmap, this._Heightmap)
                .Set(PublicsTableFields.Cols, this._Cols)
                .Set(PublicsTableFields.Rows, this._Rows)
                .Set(PublicsTableFields.EntryTile, this._EntryTile)
                .Set(PublicsTableFields.OffsetX, this._OffsetX)
                .Set(PublicsTableFields.OffsetY, this._OffsetY)
                .Set(PublicsTableFields.BgLayer, this._BgLayer)
                .Set(PublicsTableFields.BgImage, this._BgImage)
                .Set(PublicsTableFields.XmlData, this._XmlData)
                .Where(PublicsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Publics)
                .Insert()
                .Values(PublicsTableFields.Name, this._Name)
                .Values(PublicsTableFields.Description, this._Description)
                .Values(PublicsTableFields.Heightmap, this._Heightmap)
                .Values(PublicsTableFields.Cols, this._Cols)
                .Values(PublicsTableFields.Rows, this._Rows)
                .Values(PublicsTableFields.EntryTile, this._EntryTile)
                .Values(PublicsTableFields.OffsetX, this._OffsetX)
                .Values(PublicsTableFields.OffsetY, this._OffsetY)
                .Values(PublicsTableFields.BgLayer, this._BgLayer)
                .Values(PublicsTableFields.BgImage, this._BgImage)
                .Values(PublicsTableFields.XmlData, this._XmlData);
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
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Publics)
                .Delete()
                .Where(PublicsTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
