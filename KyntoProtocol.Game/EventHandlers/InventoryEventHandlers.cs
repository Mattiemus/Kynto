using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Data;

namespace KyntoProtocol.Game.EventHandlers
{
    /// <summary>
    /// Handles inventory related events.
    /// </summary>
    public class InventoryEventHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public InventoryEventHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the add item to inventory event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void AddInventoryItemEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            ItemEventArguments EventArguments = (ItemEventArguments)Args;

            // Convert to packet using our to array method.
            FurnictureArrayPacket[] ItemData = FurnictureArrayPacket.FromArray(new IFurni[] { EventArguments.Item });
            // Make sure we got data.
            if (ItemData != null && ItemData.Length == 1)
            {
                // Convert to string.
                string ItemDataString = JSON.Serializer<FurnictureArrayPacket>(ItemData[0]);
                // Send data.
                EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.AddItem + ItemDataString + ProtocolInfo.Terminator, true);
            }
        }

        /// <summary>
        /// Handles the remove item from inventory event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RemoveInventoryItemEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            ItemEventArguments EventArguments = (ItemEventArguments)Args;

            // Create the packet.
            RemoveFromInventoryPacket RemoveFromInventoryPacket = new RemoveFromInventoryPacket();
            RemoveFromInventoryPacket.I = EventArguments.Item.ItemData.Id;
            // Convert to string.
            string RemoveFromInventoryPacketString = JSON.Serializer<RemoveFromInventoryPacket>(RemoveFromInventoryPacket);
            // Send data.
            EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.RemoveItem + RemoveFromInventoryPacketString + ProtocolInfo.Terminator, true);
        }
    }
}
