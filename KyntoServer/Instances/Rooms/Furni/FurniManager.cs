using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;

namespace KyntoServer.Instances.Rooms.Furni
{
    /// <summary>
    /// Manages all the furniture in this room.
    /// </summary>
    public class FurniManager : IFurniManager
    {
        /// <summary>
        /// Stores the parent room.
        /// </summary>
        private IRoom _ParentRoom;

        /// <summary>
        /// Stores the server instance.
        /// </summary>
        private IServerManager _ServerInstance;

        /// <summary>
        /// Stores a list of all the items this furni manager manages.
        /// </summary>
        private List<IFurni> _Items = new List<IFurni>();

        /// <summary>
        /// Initialises this furni manager.
        /// </summary>
        /// <param name="ParentRoom">The parent room.</param>
        public FurniManager(IRoom ParentRoom, IServerManager ServerInstance)
        {
            // Store the parent room.
            this._ParentRoom = ParentRoom;
            this._ServerInstance = ServerInstance;
        }

        /// <summary>
        /// Initialises this furni manager, should load in all available furni.
        /// </summary>
        public void Initialise()
        {
            // Load the data based on current room type.
            switch (this._ParentRoom.RoomType)
            {
                case RoomType.PrivateRoom:
                    {
                        // Private room.
                        // Load the furni array.
                        List<IDatabaseTable> TableBuffer = this._ServerInstance.DatabaseService.Database.CreateQuery(DatabaseTables.Items)
                            .Select()
                            .Where(new string[] { ItemsTableFields.RoomId, ItemsTableFields.Tile }, new DatabaseComparison[] { DatabaseComparison.Equals, DatabaseComparison.NotEquals }, new object[] { ((IPrivateRoom)this._ParentRoom).DatabaseTable.Id, "inv" })
                            .OrderBy(ItemsTableFields.StackNr, DatabaseOrder.Ascending)
                            .ExecuteRead();

                        // Make sure we received data.
                        if (TableBuffer != null)
                        {
                            List<IItemsDatabaseTable> PrivateFurniArray = new List<IItemsDatabaseTable>();
                            for (int i = 0; i < TableBuffer.Count; i++)
                            {
                                PrivateFurniArray.Add((IItemsDatabaseTable)TableBuffer[i]);
                            }

                            // Add all the items too this room.
                            for (int i = 0; i < PrivateFurniArray.Count; i++)
                            {
                                // Get the items template.
                                IItemsDatabaseTable Item = PrivateFurniArray[i];
                                IFurniDatabaseTable ItemTemplate = this._ServerInstance.CatalogueManager.FurniTemplates[Item.Furni];
                                // TODO: List<IFurniDataDatabaseTable> FurniDataTemplate = this._ServerInstance.CatalogueManager.FurniDataTemplates[Item.Furni];

                                // Create the item.
                                BasicFurniItem FurniItem = new BasicFurniItem();
                                FurniItem.ItemData = Item;
                                FurniItem.ItemTemplate = ItemTemplate;
                                // TODO: FurniItem.FurniData = FurniDataTemplate;
                                        FurniItem.FurniData = new List<IFurniDataDatabaseTable>();
                                FurniItem.IsRoot = true;
                                
                                // Get the coordinates.
                                string[] Coords = Item.Tile.Split('_');
                                int X = int.Parse(Coords[0]);
                                int Y = int.Parse(Coords[1]);

                                // Attempt to add.
                                if (ItemWillFit(FurniItem, X, Y, Item.Rotation))
                                {
                                    PutItem(FurniItem, X, Y, Item.Rotation);
                                }
                                else
                                {
                                    // TODO: Return item to users inventory.
                                }
                            }
                        }
                        break;
                    }

                case RoomType.PublicRoom:
                    {
                        // Public room.
                        // Load the furni array.
                        List<IDatabaseTable> TableBuffer = this._ServerInstance.DatabaseService.Database.CreateQuery(DatabaseTables.PublicsItems)
                            .Select()
                            .Where(PublicsItemsTableFields.RoomId, DatabaseComparison.Equals, ((IPublicRoom)this._ParentRoom).DatabaseTable.Id)
                            .OrderBy(PublicsItemsTableFields.StackNr, DatabaseOrder.Ascending)
                            .ExecuteRead();

                        // Make sure we received data.
                        if (TableBuffer != null)
                        {
                            List<IPublicsItemsDatabaseTable> PublicFurniArray = new List<IPublicsItemsDatabaseTable>();
                            for (int i = 0; i < TableBuffer.Count; i++)
                            {
                                PublicFurniArray.Add((IPublicsItemsDatabaseTable)TableBuffer[i]);
                            }

                            // Add all the items too this room.
                            for (int i = 0; i < PublicFurniArray.Count; i++)
                            {
                                // Get the items template.
                                IItemsDatabaseTable Item = PublicFurniArray[i];
                                IFurniDatabaseTable ItemTemplate = this._ServerInstance.CatalogueManager.FurniTemplates[Item.Furni];

                                // Create the item.
                                BasicFurniItem FurniItem = new BasicFurniItem();
                                FurniItem.ItemData = Item;
                                FurniItem.ItemTemplate = ItemTemplate;
                                    FurniItem.FurniData = new List<IFurniDataDatabaseTable>();
                                FurniItem.IsRoot = true;

                                // Get the coordinates.
                                string[] Coords = Item.Tile.Split('_');
                                int X = int.Parse(Coords[0]);
                                int Y = int.Parse(Coords[1]);

                                // Attempt to add.
                                if (ItemWillFit(FurniItem, X, Y, Item.Rotation))
                                {
                                    PutItem(FurniItem, X, Y, Item.Rotation);
                                }
                                else
                                {
                                    // TODO: Public items should always fit, write error?
                                }
                            }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Deletes this furni manager.
        /// </summary>
        public void Delete()
        {
            _ParentRoom = null;
        }

        /// <summary>
        /// Moves an item.
        /// </summary>
        /// <param name="Item">The item to move.</param>
        /// <param name="NewX">The new x coordinate.</param>
        /// <param name="NewY">The new y coordinate.</param>
        /// <param name="NewRotation">The new rotation.</param>
        /// <returns>True if the item was moved.</returns>
        public bool MoveItem(IFurni Item, int NewX, int NewY, int NewRotation)
        {
            // Make sure the item fits at the new location.
            if (ItemWillFit(Item, NewX, NewY, NewRotation))
            {
                TakeItem(Item);

                Item.ItemData.Tile = NewX + "_" + NewY;
                Item.ItemData.Rotation = NewRotation;

                PutItem(Item, NewX, NewY, NewRotation);

                Item.ItemData.Update().Execute();

                this._ServerInstance.EventsManager.CallEvent(EventType.FurniMoved, this, new RoomItemEventArguments() { AvatarInstance = null, GameSocketConnection = null, UserInstance = null, Item = Item, RoomInstance = this._ParentRoom });

                return true;
            }

            // Item wont fit, error.
            return false;
        }

        /// <summary>
        /// Places an item in the room.
        /// </summary>
        /// <param name="Item">The item to add.</param>
        /// <param name="X">The x coordinate to place the item at.</param>
        /// <param name="Y">The y coordinate to place the item at.</param>
        /// <param name="Rotation">The rotation index of the item.</param>
        /// <returns>True if the item was added.</returns>
        public bool PlaceItem(IFurni Item, int X, int Y, int Rotation)
        {
            // Make sure the item fits at the location.
            if (ItemWillFit(Item, X, Y, Rotation))
            {
                Item.ItemData.StackNr = int.MaxValue;

                PutItem(Item, X, Y, Rotation);

                Item.ItemData.Tile = X + "_" + Y;
                Item.ItemData.Rotation = Rotation;
                Item.ItemData.RoomId = (this._ParentRoom.RoomType == RoomType.PrivateRoom) ? ((IPrivateRoom)this._ParentRoom).DatabaseTable.Id : ((IPublicRoom)this._ParentRoom).DatabaseTable.Id;
                Item.ItemData.Update().Execute();

                this._ServerInstance.EventsManager.CallEvent(EventType.FurniPlaced, this, new RoomItemEventArguments() { AvatarInstance = null, GameSocketConnection = null, UserInstance = null, Item = Item, RoomInstance = this._ParentRoom });

                return true;
            }

            // Item wont fit, error.
            return false;
        }

        /// <summary>
        /// Removes an item from the room.
        /// </summary>
        /// <param name="Item">The item to remove.</param>
        /// <returns>True if the item was removed.</returns>
        public bool RemoveItem(IFurni Item)
        {
            // Make sure the room contains the item.
            if (this._Items.Contains(Item))
            {
                TakeItem(Item);

                this._ServerInstance.EventsManager.CallEvent(EventType.FurniRemoved, this, new RoomItemEventArguments() { AvatarInstance = null, GameSocketConnection = null, UserInstance = null, Item = Item, RoomInstance = this._ParentRoom });
                
                return true;
            }

            // Item is not in room, error.
            return false;
        }

        /// <summary>
        /// Adds an item to the room, without checking it will fit first.
        /// </summary>
        /// <param name="Item">The item to add.</param>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <param name="Rotation">The rotation index of the item..</param>
        private void PutItem(IFurni Item, int X, int Y, int Rotation)
        {
            // Get how to iterate through based on rotation.
            int RotationX = 0;
            int RotationY = 0;
            int MaxX = 0;
            int MaxY = 0;
            GetRotationDirections(Rotation, Item.ItemTemplate.Rows, Item.ItemTemplate.Cols, ref RotationX, ref RotationY, ref MaxX, ref MaxY);

            // Make sure item goes on top.
            Item.ItemData.StackNr = int.MaxValue;

            // Loop through and add the item.
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    // Calculate the coordinates.
                    int XCoordinate = X + (x * RotationX);
                    int YCoordinate = Y + (y * RotationY);

                    // Check if this is root.
                    if (x != 0 || y != 0)
                    {
                        // Create the copy.
                        IFurni Furni = Item.Copy();
                        Furni.IsRoot = false;
                        // Add a copy.
                        this._ParentRoom.HeightmapManger.ItemStackMap[XCoordinate, YCoordinate].Add(Furni);
                    }
                    else
                    {
                        // Add the root!
                        IFurni Furni = Item.Copy();
                        Furni.IsRoot = true;
                        this._ParentRoom.HeightmapManger.ItemStackMap[XCoordinate, YCoordinate].Add(Furni);
                    }

                    // Now restack the tile.
                    RestackTile(XCoordinate, YCoordinate);
                }
            }

            // Finally add the item to the room list.
            this._Items.Add(Item);
        }

        /// <summary>
        /// Removes an item from the room.
        /// </summary>
        /// <param name="Item">The item to remove.</param>
        /// <returns>True if the item was removed.</returns>
        private bool TakeItem(IFurni Item)
        {
            // Get how to iterate through based on rotation.
            int RotationX = 0;
            int RotationY = 0;
            int MaxX = 0;
            int MaxY = 0;
            GetRotationDirections(Item.ItemData.Rotation, Item.ItemTemplate.Rows, Item.ItemTemplate.Cols, ref RotationX, ref RotationY, ref MaxX, ref MaxY);

            // Get where the item is.
            string[] Coords = Item.ItemData.Tile.Split('_');
            int X = int.Parse(Coords[0]);
            int Y = int.Parse(Coords[1]);

            // Go through every tile the item should take up, and remove it.
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    // Calculate the coordinates.
                    int XCoordinate = X + (x * RotationX);
                    int YCoordinate = Y + (y * RotationY);

                    // Remove
                    List<IFurni> Items = this._ParentRoom.HeightmapManger.ItemStackMap[XCoordinate, YCoordinate];
                    IFurni ToRemove = null;
                    for (int i = 0; i < Items.Count; i++)
                    {
                        if (Items[i].ItemData.Id == Item.ItemData.Id)
                        {
                            ToRemove = Items[i];
                            break;
                        }
                    }
                    this._ParentRoom.HeightmapManger.ItemStackMap[XCoordinate, YCoordinate].Remove(ToRemove);

                    // Now restack the tile.
                    RestackTile(XCoordinate, YCoordinate);
                }
            }
            this._Items.Remove(Item);

            // Success.
            return true;
        }

        /// <summary>
        /// Restacks a tile, recalculates the tiles state, item heights and stack numbers.
        /// </summary>
        /// <param name="X">The x-coordinate of the tile to restack.</param>
        /// <param name="Y">The y-coordinate of the tile to restack.</param>
        private void RestackTile(int X, int Y)
        {
            // Get all the items on the tile.
            IFurni[] Items = GetItemsOnTile(X, Y);

            // Update the tiles state.
            {
                if (Items.Length == 0)
                {
                    // No items on tile, it must be free.
                    this._ParentRoom.HeightmapManger.StateMap[X, Y] = TileState.Open;
                }
                else if (Items.Length == 1)
                {
                    // Only one item on tile, it will be the state of whatever item type is on the tile.
                    switch (Items[0].ItemTemplate.Type)
                    {
                        case "rug":
                            this._ParentRoom.HeightmapManger.StateMap[X, Y] = TileState.Open;
                            break;

                        case "chair":
                            this._ParentRoom.HeightmapManger.StateMap[X, Y] = TileState.Seat;
                            break;

                        default:
                            this._ParentRoom.HeightmapManger.StateMap[X, Y] = TileState.Blocked;
                            break;
                    }
                }
                else
                {
                    // Multiple items, it must either be a seat or blocked.
                    bool ContainsSeat = false;

                    for (int i = 0; i < Items.Length; i++)
                    {
                        if (Items[i].ItemTemplate.Type == "chair")
                        {
                            ContainsSeat = true;
                            break;
                        }
                    }

                    if (ContainsSeat)
                    {
                        this._ParentRoom.HeightmapManger.StateMap[X, Y] = TileState.Seat;
                    }
                    else
                    {
                        this._ParentRoom.HeightmapManger.StateMap[X, Y] = TileState.Blocked;
                    }
                }
            }

            // TODO: Re-order stack numbers.
            {
                // ...
            }

            // TODO: Re-calculate stack heights of each item.
            {
                // ...
            }
        }

        /// <summary>
        /// Checks if an item will fit in the room.
        /// </summary>
        /// <param name="Item">The item to add.</param>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</para.m>
        /// <param name="Rotation">The rotation index of the item..</param>
        /// <returns>True if the item will fit at the specified location.</returns>
        public bool ItemWillFit(IFurni Item, int X, int Y, int Rotation)
        {
            // Get how to iterate through based on rotation.
            int RotationX = 0;
            int RotationY = 0;
            int MaxX = 0;
            int MaxY = 0;
            GetRotationDirections(Rotation, Item.ItemTemplate.Rows, Item.ItemTemplate.Cols, ref RotationX, ref RotationY, ref MaxX, ref MaxY);

            // Check that each tile the item takes up is free and exists.
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    // Calculate the coordinates.
                    int XCoordinate = X + (x * RotationX);
                    int YCoordinate = Y + (y * RotationY);

                    // Check within room boundaries.
                    if (!this._ParentRoom.HeightmapManger.WithinRoom(XCoordinate, YCoordinate))
                    {
                        return false;
                    }
                }
            }

            // Success.
            return true;
        }

        /// <summary>
        /// Gets the direction of rotation for an item.
        /// </summary>
        /// <param name="Rotation">The rotation.</param>
        /// <param name="Rows">The number of rows the item takes up.</param>
        /// <param name="Cols">The number of cols the item takes up.</param>
        /// <param name="RotationX">The rotation x incremental value.</param>
        /// <param name="RotationY">The rotation y incremental value.</param>
        /// <param name="MaxX">The maximum x value to increment to.</param>
        /// <param name="MaxY">The maximum y value to increment to.</param>
        private void GetRotationDirections(int Rotation, int Rows, int Cols, ref int RotationX, ref int RotationY, ref int MaxX, ref int MaxY)
        {
            // Find out the direction.
            switch (Rotation)
            {
                default:
                case 0:
                    RotationX = 1;
                    RotationY = 1;
                    MaxX = Rows;
                    MaxY = Cols;
                    break;

                case 1:
                    RotationX = 1;
                    RotationY = 1;
                    MaxX = Rows;
                    MaxY = Cols;
                    break;

                case 2:
                    RotationX = 1;
                    RotationY = 1;
                    MaxX = Rows;
                    MaxY = Cols;
                    break;

                case 3:
                    RotationX = 1;
                    RotationY = 1;
                    MaxX = Rows;
                    MaxY = Cols;
                    break;
            }
        }

        /// <summary>
        /// Gets the items on the specified tile.
        /// </summary>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <returns>The items on the specified tile.</returns>
        public IFurni[] GetItemsOnTile(int X, int Y)
        {
            // Create a buffer.
            List<IFurni> FurniBuffer = new List<IFurni>();

            // Loop through the furni data at the coordinate.
            for (int i = 0; i < this._ParentRoom.HeightmapManger.ItemStackMap[X, Y].Count; i++)
            {
                FurniBuffer.Add(this._ParentRoom.HeightmapManger.ItemStackMap[X, Y][i]);
            }

            // Convert to array and return.
            return FurniBuffer.ToArray();
        }

        /// <summary>
        /// Gets the seat at a coordinate.
        /// </summary>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <returns>The seat at the coordinate.</returns>
        public IFurni GetSeatAt(int X, int Y)
        {
            // Loop through the furni data at the coordinate.
            for (int i = 0; i < this._ParentRoom.HeightmapManger.ItemStackMap[X, Y].Count; i++)
            {
                // Check if the item is a chair.
                if (this._ParentRoom.HeightmapManger.ItemStackMap[X, Y][i].ItemTemplate.Type == "chair")
                {
                    return this._ParentRoom.HeightmapManger.ItemStackMap[X, Y][i];
                }
            }

            // No seat found, return null.
            return null;
        }

        /// <summary>
        /// Gets the item in the room with the specified id.
        /// </summary>
        /// <param name="Id">The items id to search for.</param>
        /// <returns>The items id.</returns>
        public IFurni GetItem(int Id)
        {
            // Search for the item.
            for (int i = 0; i < this._Items.Count; i++)
            {
                if (this._Items[i].ItemData.Id == Id)
                {
                    return this._Items[i];
                }
            }

            // Not found.
            return null;
        }

        /// <summary>
        /// Gets all the furni in this furni manager.
        /// </summary>
        /// <returns>The array containing all the furniture in this room.</returns>
        public IFurni[] ToArray()
        {
            return this._Items.ToArray();
        }
    }
}
