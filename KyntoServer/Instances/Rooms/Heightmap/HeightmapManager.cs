using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;

namespace KyntoServer.Instances.Rooms.Heightmap
{
    /// <summary>
    /// Manages the height mapping of this room.
    /// </summary>
    public class HeightmapManager : IHeightmapManager
    {
        /// <summary>
        /// Stores the parent room.
        /// </summary>
        private IRoom _ParentRoom;

        /// <summary>
        /// Stores this height maps holes mapping.
        /// </summary>
        private string[] _HolesMap;

        /// <summary>
        /// Gets the holes map for this height map.
        /// </summary>
        public string[] HolesMap
        {
            get
            {
                return this._HolesMap;
            }
        }

        /// <summary>
        /// Stores the height map string.
        /// </summary>
        private string _HeightmapString;

        /// <summary>
        /// Gets the height map string for this height map.
        /// </summary>
        public string HeightmapString
        {
            get
            {
                return this._HeightmapString;
            }
        }

        /// <summary>
        /// Stores the unit mapping for this room.
        /// </summary>
        private List<IAvatar>[,] _UnitMap;

        /// <summary>
        /// Gets the unit mapping for this room.
        /// </summary>
        public List<IAvatar>[,] UnitMap
        {
            get
            {
                return this._UnitMap;
            }
        }

        /// <summary>
        /// Stores the height mapping of this room.
        /// </summary>
        private int[,] _Heightmap;

        /// <summary>
        /// Gets the tile height mapping for this room.
        /// </summary>
        public int[,] HeightMap
        {
            get
            {
                return this._Heightmap;
            }
        }

        /// <summary>
        /// Stores the state map for this room.
        /// </summary>
        private TileState[,] _StateMap;

        /// <summary>
        /// Gets the tile state mapping.
        /// </summary>
        public TileState[,] StateMap
        {
            get
            {
                return this._StateMap;
            }
        }

        /// <summary>
        /// Stores the item stack map for this room.
        /// </summary>
        private List<IFurni>[,] _ItemStackMap;

        /// <summary>
        /// Gets the item stack mapping.
        /// </summary>
        public List<IFurni>[,] ItemStackMap
        {
            get
            {
                return this._ItemStackMap;
            }
        }

        /// <summary>
        /// Stores the reserved tiles map for this room.
        /// </summary>
        private bool[,] _ReservedMap;

        /// <summary>
        /// Gets the array of reserved tiles.
        /// </summary>
        public bool[,] ReservedMap
        {
            get
            {
                return this._ReservedMap;
            }
        }

        /// <summary>
        /// Stores the rooms cols.
        /// </summary>
        private int _RoomCols;

        /// <summary>
        /// Gets the cols of this room.
        /// </summary>
        public int Cols
        {
            get
            {
                return this._RoomCols;
            }
        }

        /// <summary>
        /// Stores the rooms rows.
        /// </summary>
        private int _RoomRows;

        /// <summary>
        /// Gets the rows of this room.
        /// </summary>
        public int Rows
        {
            get
            {
                return this._RoomRows;
            }
        }

        /// <summary>
        /// The nearest square number to the number of cols in the height map.
        /// </summary>
        private int _NearestSquareCols;

        /// <summary>
        /// Gets the nearest square number to the number of cols in the height map.
        /// </summary>
        public int NearestSquareCols
        {
            get
            {
                return this._NearestSquareCols;
            }
        }

        /// <summary>
        /// The nearest square number to the number of rows in the height map.
        /// </summary>
        private int _NearestSquareRows;

        /// <summary>
        /// Gets the nearest square number to the number of rows in the height map.
        /// </summary>
        public int NearestSquareRows
        {
            get
            {
                return this._NearestSquareRows;
            }
        }

        /// <summary>
        /// Initialises this height map manager.
        /// </summary>
        /// <param name="ParentRoom">The parent room.</param>
        public HeightmapManager(IRoom ParentRoom)
        {
            // Store the parent room.
            this._ParentRoom = ParentRoom;
        }

        /// <summary>
        /// Should initialise this height map manager, loading the height map.
        /// </summary>
        public void Initialise()
        {
            // Initialise depending on the parent room type.
            switch (this._ParentRoom.RoomType)
            {
                case RoomType.PrivateRoom:
                    {
                        // Private room.
                        // Get the room.
                        IPrivateRoom PrivateRoom = (IPrivateRoom)this._ParentRoom;

                        // Create the height map.
                        this._RoomCols = PrivateRoom.DatabaseTable.Cols;
                        this._RoomRows = PrivateRoom.DatabaseTable.Rows;
                        // Create the mappings.
                        this._UnitMap = new List<IAvatar>[this._RoomRows, this._RoomCols];
                        this._Heightmap = new int[this._RoomRows, this._RoomCols];
                        this._StateMap = new TileState[this._RoomRows, this._RoomCols];
                        this._ItemStackMap = new List<IFurni>[this._RoomRows, this._RoomCols];
                        this._ReservedMap = new bool[this._RoomRows, this._RoomCols];
                        // Create the mapping instances.
                        for (int y = 0; y < this._RoomCols; y++)
                        {
                            for (int x = 0; x < this._RoomRows; x++)
                            {
                                // Initialise them.
                                this.UnitMap[x, y] = new List<IAvatar>();
                                this.HeightMap[x, y] = 1;
                                this.StateMap[x, y] = TileState.Open;
                                this.ItemStackMap[x, y] = new List<IFurni>();
                                this.ReservedMap[x, y] = false;
                            }
                        }
                        // Split out the holes map.
                        string[] HolesMapping = PrivateRoom.DatabaseTable.Holes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        // Create the holes mapping.
                        for (int i = 0; i < HolesMapping.Length; i++)
                        {
                            // Make sure the point contains data.
                            if (HolesMapping[i] != null && HolesMapping[i] != "")
                            {
                                // Get the points.
                                int X = int.Parse(HolesMapping[i].Split('_')[0]);
                                int Y = int.Parse(HolesMapping[i].Split('_')[1]);

                                // If the point is within the room, block it.
                                if (this.WithinRoom(X, Y))
                                {
                                    this.StateMap[X, Y] = TileState.Blocked;
                                    this.ItemStackMap[X, Y] = null;
                                }
                            }
                        }
                        // Store the holes mapping.
                        this._HolesMap = HolesMapping;

                        // Finish.
                        break;
                    }

                case RoomType.PublicRoom:
                    {
                        // Public room.
                        // Get the room.
                        IPublicRoom PublicRoom = (IPublicRoom)this._ParentRoom;

                        // Create the heightmap.
                        this._RoomCols = PublicRoom.DatabaseTable.Cols;
                        this._RoomRows = PublicRoom.DatabaseTable.Rows;
                        this._HeightmapString = PublicRoom.DatabaseTable.Heightmap;
                        // Create the mappings.
                        this._UnitMap = new List<IAvatar>[this._RoomRows, this._RoomCols];
                        this._Heightmap = new int[this._RoomRows, this._RoomCols];
                        this._StateMap = new TileState[this._RoomRows, this._RoomCols];
                        this._ItemStackMap = new List<IFurni>[this._RoomRows, this._RoomCols];
                        this._ReservedMap = new bool[this._RoomRows, this._RoomCols];

                        // Create the mapping instances.
                        for (int y = 0; y < Cols; y++)
                        {
                            for (int x = 0; x < Rows; x++)
                            {
                                // Initialise them.
                                this.UnitMap[x, y] = new List<IAvatar>();
                                this.HeightMap[x, y] = 1;
                                this.StateMap[x, y] = TileState.Open;
                                this.ItemStackMap[x, y] = new List<IFurni>();
                                this.ReservedMap[x, y] = false;
                            }
                        }

                        // Split out the heightmap.
                        string[] HeightmapRows = (this._HeightmapString.Split(new string[1] { Environment.NewLine }, StringSplitOptions.None));
                        // Create the mappings.
                        for (int y = 0; y < Cols; y++)
                        {
                            for (int x = 0; x < Rows; x++)
                            {
                                // If the heightmap data is set to 'X' set the tile to blocked.
                                if ((HeightmapRows[y])[x] == 'X')
                                {
                                    this.StateMap[x, y] = TileState.Blocked;
                                }
                            }
                        }

                        // Finish.
                        break;
                    }
            }

            // Setup the updated cols.
            this._NearestSquareCols = this._RoomCols;
            this._NearestSquareRows = this._RoomRows;

            // Make sure the rows & cols are a power of 2.
            double LogBase2OfCols = Math.Log(this._NearestSquareCols, 2);
            if (LogBase2OfCols != (int)LogBase2OfCols)
            {
                this._NearestSquareCols = (int)Math.Pow(2, Math.Ceiling(LogBase2OfCols));
            }
            double LogBase2OfRows = Math.Log(this._NearestSquareRows, 2);
            if (LogBase2OfRows != (int)LogBase2OfRows)
            {
                this._NearestSquareRows = (int)Math.Pow(2, Math.Ceiling(LogBase2OfRows));
            }
        }

        /// <summary>
        /// Deletes this height map manager.
        /// </summary>
        public void Delete()
        {
            _ParentRoom = null;
        }

        /// <summary>
        /// Finds if a point is within this room.
        /// </summary>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <returns>True if the point is within the room.</returns>
        public bool WithinRoom(int X, int Y)
        {
            // Return if it is.
            return (/* Check bigger than 0 */ (X >= 0 && Y >= 0) && /* Check within bounds */ (X <= this._RoomRows && Y <= this._RoomCols));
        }

        /// <summary>
        /// Creates a grid for the specified avatar.
        /// </summary>
        /// <param name="Avatar">The avatar to create the height map for.</param>
        /// <returns>The height map grid.</returns>
        public byte[,] GenerateGrid(IAvatar Avatar)
        {
            // Create the grid.
            for (int y = 0; y < this._RoomCols; y++)
            {
                for (int x = 0; x < this._RoomRows; x++)
                {
                    // Find out data about the tile.
                    bool UserTileOk = true;
                    for (int i = 0; i < this._UnitMap[x, y].Count; i++)
                    {
                        if (!this._UnitMap[x, y][i].IsGhost || (Avatar.TargetX == x && Avatar.TargetY == y))
                        {
                            UserTileOk = false;
                            break;
                        }
                    }
                    bool FurniTileOk = (this._StateMap[x, y] == TileState.Open || this._StateMap[x, y] == TileState.Seat);
                    bool FurniSeatTileOK = (this._StateMap[Avatar.TargetX, Avatar.TargetY] != TileState.Seat || (this._StateMap[x, y] == TileState.Open || (this._StateMap[x, y] == TileState.Seat && (Avatar.TargetX == x && Avatar.TargetY == y))));

                    // Find if the tile is free.
                    if (UserTileOk && FurniTileOk && FurniSeatTileOK)
                    {
                        // The tile is free.
                        Avatar.Heightmap[x, y] = 1;
                    }
                    else
                    {
                        // The tile is not free.
                        Avatar.Heightmap[x, y] = 0;
                    }
                }
            }

            // Return the generated grid.
            return Avatar.Heightmap;
        }
    }
}
