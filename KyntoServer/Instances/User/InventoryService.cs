using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Events;

using KyntoServer.Instances.Rooms.Furni;

namespace KyntoServer.Instances.User
{
    /// <summary>
    /// Handles the inventory of a user.
    /// </summary>
    public class InventoryService : IInventoryService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the user instance parent.
        /// </summary>
        private IUser _UserInstance;

        /// <summary>
        /// Stores the parent users connection.
        /// </summary>
        private IGameSocketConnection _UserConnection;

        /// <summary>
        /// Stores the list of items in this inventory.
        /// </summary>
        private List<IFurni> _InventoryContents = new List<IFurni>();

        /// <summary>
        /// Gets the list of items in this inventory.
        /// </summary>
        public List<IFurni> InventoryContents
        {
            get
            {
                return this._InventoryContents;
            }
        }

        /// <summary>
        /// Initialises this inventory service.
        /// </summary>
        /// <param name="ServerManager">The server manager isntance.</param>
        /// <param name="ParentUser">The parent user.</param>
        /// <param name="ParentSocket">The parent socket.</param>
        public InventoryService(IServerManager ServerManager, IUser ParentUser, IGameSocketConnection ParentSocket)
        {
            // Store these values.
            this._ServerManager = ServerManager;
            this._UserInstance = ParentUser;
            this._UserConnection = ParentSocket;
        }

        /// <summary>
        /// Called when the owner of this pda service logs in.
        /// </summary>
        public void Initialise()
        {
            // Get the list of database items.
            List<IDatabaseTable> FurniDatabase = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Items)
                .Select()
                .Where(new string[] { ItemsTableFields.OwnerId, ItemsTableFields.Tile }, new DatabaseComparison[] { DatabaseComparison.Equals, DatabaseComparison.Equals }, new string[] { this._UserInstance.UserData.UserInfo.Id.ToString(), "inv" })
                .ExecuteRead();

            // Make sure we have data.
            if (FurniDatabase != null)
            {
                // Add the items.
                for (int i = 0; i < FurniDatabase.Count; i++)
                {
                    // Create the furni.
                    BasicFurniItem Item = new BasicFurniItem();
                    Item.ItemData = ((IItemsDatabaseTable)FurniDatabase[i]);
                    Item.ItemTemplate = this._ServerManager.CatalogueManager.FurniTemplates[Item.ItemData.Furni];

                    // Add it.
                    this._InventoryContents.Add(Item);
                }
            }
        }

        /// <summary>
        /// Shuts down the inventory service.
        /// </summary>
        public void Shutdown()
        {
            // Forget the owner so we can be garbage collected.
            this._UserConnection = null;
            this._UserInstance = null;
        }

        /// <summary>
        /// Removes an item from this inventory.
        /// </summary>
        /// <param name="ToRemove">The item too remove.</param>
        public void RemoveItem(IFurni ToRemove)
        {
            // Remove the item.
            if (this._InventoryContents.Contains(ToRemove))
            {
                // Remove!
                this._InventoryContents.Remove(ToRemove);

                // Fire the item added to inventory event.
                this._ServerManager.EventsManager.CallEvent(EventType.InventoryRemoveItem, this, new ItemEventArguments() { GameSocketConnection = this._UserConnection, UserInstance = this._UserInstance, Item = ToRemove });
            }
        }

        /// <summary>
        /// Adds an item too this inventory.
        /// </summary>
        /// <param name="ToAdd">The item too add.</param>
        public void AddItem(IFurni ToAdd)
        {
            // Add the item.
            this._InventoryContents.Add(ToAdd);

            // Fire the item removed from inventory event.
            this._ServerManager.EventsManager.CallEvent(EventType.InventoryAddItem, this, new ItemEventArguments() { GameSocketConnection = this._UserConnection, UserInstance = this._UserInstance, Item = ToAdd });
        }

        /// <summary>
        /// Gets an item from the inventory by id.
        /// </summary>
        /// <param name="Id">The id of the item.</param>
        /// <returns>The item with the specified id.</returns>
        public IFurni GetItemById(int Id)
        {
            for (int i = 0; i < this._InventoryContents.Count; i++)
            {
                if (this._InventoryContents[i].ItemData.Id == Id)
                {
                    return this._InventoryContents[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the array of furni in this users inventory.
        /// </summary>
        /// <returns>The array of furni in this users inventory.</returns>
        public IFurni[] ToArray()
        {
            return this._InventoryContents.ToArray();
        }
    }
}
