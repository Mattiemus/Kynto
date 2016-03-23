using System;

namespace KyntoLib.Interfaces.Instances.Rooms.Furni
{
    /// <summary>
    /// Interfaces with a furni manager.
    /// </summary>
    public interface IFurniManager
    {
        /// <summary>
        /// Initialises this furni manager, should load in all available furni.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Deletes this furni manager.
        /// </summary>
        void Delete();

        /// <summary>
        /// Moves an item.
        /// </summary>
        /// <param name="Item">The item to move.</param>
        /// <param name="NewX">The new x coordinate.</param>
        /// <param name="NewY">The new y coordinate.</param>
        /// <param name="NewRotation">The new rotation.</param>
        /// <returns>True if the item was moved.</returns>
        bool MoveItem(IFurni Item, int NewX, int NewY, int NewRotation);

        /// <summary>
        /// Places an item in the room.
        /// </summary>
        /// <param name="Item">The item to add.</param>
        /// <param name="X">The x coordinate to place the item at.</param>
        /// <param name="Y">The y coordinate to place the item at.</param>
        /// <param name="Rotation">The rotation index of the item.</param>
        /// <returns>True if the item was added.</returns>
        bool PlaceItem(IFurni Item, int X, int Y, int Rotation);

        /// <summary>
        /// Removes an item from the room.
        /// </summary>
        /// <param name="Item">The item to remove.</param>
        /// <returns>True if the item was removed.</returns>
        bool RemoveItem(IFurni Item);

        /// <summary>
        /// Checks if an item will fit in the room.
        /// </summary>
        /// <param name="Item">The item to add.</param>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <param name="Rotation">The rotation index of the item..</param>
        /// <returns>True if the item will fit at the specified location.</returns>
        bool ItemWillFit(IFurni Item, int X, int Y, int Rotation);

        /// <summary>
        /// Gets the items on the specified tile.
        /// </summary>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <returns>The items on the specified tile.</returns>
        IFurni[] GetItemsOnTile(int X, int Y);

        /// <summary>
        /// Gets the seat at a coordinate.
        /// </summary>
        /// <param name="X">The x coordinate.</param>
        /// <param name="Y">The y coordinate.</param>
        /// <returns>The seat at the coordinate.</returns>
        IFurni GetSeatAt(int X, int Y);

        /// <summary>
        /// Gets the item in the room with the specified id.
        /// </summary>
        /// <param name="Id">The items id to search for.</param>
        /// <returns>The items id.</returns>
        IFurni GetItem(int Id);

        /// <summary>
        /// Gets all the furni in this furni manager.
        /// </summary>
        /// <returns>The array containing all the furniture in this room.</returns>
        IFurni[] ToArray();
    }
}