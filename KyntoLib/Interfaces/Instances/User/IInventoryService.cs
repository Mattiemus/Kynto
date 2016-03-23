using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Instances.Rooms.Furni;

namespace KyntoLib.Interfaces.Instances.User
{
    /// <summary>
    /// Interfaces with a inventory service.
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// Gets the list of items in this inventory.
        /// </summary>
        List<IFurni> InventoryContents { get; }

        /// <summary>
        /// Initialises this inventory service.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Shuts down the inventory service.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Removes an item from this inventory.
        /// </summary>
        /// <param name="ToRemove">The item too remove.</param>
        void RemoveItem(IFurni ToRemove);

        /// <summary>
        /// Adds an item too this inventory.
        /// </summary>
        /// <param name="ToAdd">The item too add.</param>
        void AddItem(IFurni ToAdd);

        /// <summary>
        /// Gets an item from the inventory by id.
        /// </summary>
        /// <param name="Id">The id of the item.</param>
        /// <returns>The item with the specified id.</returns>
        IFurni GetItemById(int Id);

        /// <summary>
        /// Gets the array of furni in this users inventory.
        /// </summary>
        /// <returns>The array of furni in this users inventory.</returns>
        IFurni[] ToArray();
    }
}
