using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.EventHandlers
{
    /// <summary>
    /// Handles furni related events.
    /// </summary>
    public class FurniEventHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public FurniEventHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the furni moved event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void FurniMovedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomItemEventArguments EventArguments = (RoomItemEventArguments)Args;

            // Send to everyone in room the new furni locations.
            // Create the packet.
            MoveFurniResponsePacket MoveFurniClientPacket = new MoveFurniResponsePacket();
            MoveFurniClientPacket.I = EventArguments.Item.ItemData.Id;
            MoveFurniClientPacket.S = EventArguments.Item.ItemData.StackNr;
            MoveFurniClientPacket.T = EventArguments.Item.ItemData.Tile;
            // Convert to string.
            string MoveFurniClientPacketString = JSON.Serializer<MoveFurniResponsePacket>(MoveFurniClientPacket);
            // Send too the room.
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.MoveFurni + MoveFurniClientPacketString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the furni removed event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void FurniRemovedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomItemEventArguments EventArguments = (RoomItemEventArguments)Args;

            // Tell all users in the room that the item was removed.
            RemoveItemPacket RemoveItemPacket = new RemoveItemPacket();
            RemoveItemPacket.I = EventArguments.Item.ItemData.Id;
            // Convert to string.
            string RemoveItemPacketString = JSON.Serializer<RemoveItemPacket>(RemoveItemPacket);
            // Reply.
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.RemoveFurni + RemoveItemPacketString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the furni placed event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void FurniPlacedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomItemEventArguments EventArguments = (RoomItemEventArguments)Args;

            // Send too room that it was added.
            FurnictureArrayPacket[] ItemData = FurnictureArrayPacket.FromArray(new IFurni[] { EventArguments.Item });

            // Convert to string.
            string ItemDataString = JSON.Serializer<FurnictureArrayPacket>(ItemData[0]);
            // Send data.
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.AddFurni + ItemDataString + ProtocolInfo.Terminator);
        }
    }
}
