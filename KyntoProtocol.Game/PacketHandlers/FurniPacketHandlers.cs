using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles furni related packets.
    /// </summary>
    public class FurniPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public FurniPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles a activate furni request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void ActivateFurniRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    ActivateFurniRequestPacket PacketModel = JSON.DeSerialize<ActivateFurniRequestPacket>(Params.PacketBody);

                    // Make sure we received data.
                    if (PacketModel != null)
                    {
                        // Make sure the user has room rights.
                        if (Params.UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (((IPrivateRoom)Params.UserInstance.Avatar.CurrentRoom).GetRightsForUser(Params.UserInstance).CanActivateFurni || Params.UserInstance.UserData.UsersRank.CanOverrideActivateFurniInPrivateRooms))
                        {
                            // Private room.
                            // Get the item.
                            IFurni Item = Params.UserInstance.Avatar.CurrentRoom.FurniManger.GetItem(PacketModel.I);

                            // Make sure we got an item.
                            if (Item != null)
                            {
                                // Check the item has an action state.
                                if (Item.ItemTemplate.Action)
                                {
                                    // Toggle the items action.
                                    Item.ItemData.Action = (!Item.ItemData.Action);

                                    // Create the packet.
                                    ActivateFurniResponsePacket ActivateFurniResponsePacket = new ActivateFurniResponsePacket();
                                    ActivateFurniResponsePacket.I = Item.ItemData.Id;
                                    ActivateFurniResponsePacket.A = Item.ItemData.Action;
                                    // Convert to string.
                                    string ActivateFurniResponsePacketString = JSON.Serializer<ActivateFurniResponsePacket>(ActivateFurniResponsePacket);
                                    // Send to room.
                                    Params.UserInstance.Avatar.CurrentRoom.SendData(ProtocolInfo.ServerPackets.UpdateFurni + ActivateFurniResponsePacketString + ProtocolInfo.Terminator);

                                    // Save the furni!
                                    Item.ItemData.Update().Execute();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles a delete furni request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void DeleteFurniRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    DeleteFurniRequestPacket PacketModel = JSON.DeSerialize<DeleteFurniRequestPacket>(Params.PacketBody);

                    // Get the users rights (we will need them a lot)!
                    if (Params.UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (((IPrivateRoom)Params.UserInstance.Avatar.CurrentRoom).GetRightsForUser(Params.UserInstance).CanDeleteFurni || Params.UserInstance.UserData.UsersRank.CanOverrideDeleteFurniInPrivateRooms))
                    {
                        // Get the furni.
                        IFurni Item = Params.UserInstance.Avatar.CurrentRoom.FurniManger.GetItem(PacketModel.I);

                        // Attempt to move the item.
                        bool Success = Params.UserInstance.Avatar.CurrentRoom.FurniManger.RemoveItem(Item);

                        // Find out if it was ok.
                        if (Success)
                        {
                            // Delete the item.
                            Item.ItemData.Delete().Execute();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles a drop furni request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void DropFurniRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    DropFurniRequestPacket PacketModel = JSON.DeSerialize<DropFurniRequestPacket>(Params.PacketBody);

                    // Make sure the user has room rights.
                    if (Params.UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (((IPrivateRoom)Params.UserInstance.Avatar.CurrentRoom).GetRightsForUser(Params.UserInstance).CanDropFurni || Params.UserInstance.UserData.UsersRank.CanOverrideDropFurniInPrivateRooms))
                    {
                        // Get the furni.
                        IFurni Item = Params.UserInstance.InventoryService.GetItemById(PacketModel.I);

                        // Make sure we have this item.
                        if (Item != null)
                        {
                            // Set the items action to false!
                            Item.ItemData.Action = false;

                            // Split the coordinates.
                            string[] Coords = PacketModel.T.Split('_');
                            int X = int.Parse(Coords[0]);
                            int Y = int.Parse(Coords[1]);

                            // Attempt to move the item.
                            bool Success = Params.UserInstance.Avatar.CurrentRoom.FurniManger.PlaceItem(Item, X, Y, Item.ItemData.Rotation);

                            // Find out if it was ok.
                            if (Success)
                            {
                                // Remove from inventory.
                                Params.UserInstance.InventoryService.RemoveItem(Item);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles a move furni request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void MoveFurniRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    MoveFurniPacket PacketModel = JSON.DeSerialize<MoveFurniPacket>(Params.PacketBody);

                    // Make sure the user has room rights.
                    if ((Params.UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (((IPrivateRoom)Params.UserInstance.Avatar.CurrentRoom).GetRightsForUser(Params.UserInstance).CanMoveFurni || Params.UserInstance.UserData.UsersRank.CanOverrideMoveFurniInPrivateRooms)))
                    {
                        // Get the item to move.
                        IFurni Item = Params.UserInstance.Avatar.CurrentRoom.FurniManger.GetItem(PacketModel.I);

                        // Move it, if it exists.
                        if (Item != null)
                        {
                            // Split the coordinates.
                            string[] Coords = PacketModel.T.Split('_');
                            int X = int.Parse(Coords[0]);
                            int Y = int.Parse(Coords[1]);

                            // TODO: add rotation to move packet.

                            // Move the item.
                            Params.UserInstance.Avatar.CurrentRoom.FurniManger.MoveItem(Item, X, Y, Item.ItemData.Rotation);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles a pickup furni request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void PickUpFurniRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    PickFurniRequestPacket PacketModel = JSON.DeSerialize<PickFurniRequestPacket>(Params.PacketBody);

                    // Get the users rights (we will need them a lot)!
                    if (Params.UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (((IPrivateRoom)Params.UserInstance.Avatar.CurrentRoom).GetRightsForUser(Params.UserInstance).CanPickupFurniToOwnInventory || Params.UserInstance.UserData.UsersRank.CanOverridePickupFurniToOwnInPrivateRooms))
                    {
                        // Get the furni.
                        IFurni Item = Params.UserInstance.Avatar.CurrentRoom.FurniManger.GetItem(PacketModel.I);

                        // Attempt to move the item.
                        bool Success = Params.UserInstance.Avatar.CurrentRoom.FurniManger.RemoveItem(Item);

                        // Find out if it was ok.
                        if (Success)
                        {
                            // Update the item.
                            Item.ItemData.Tile = "inv";

                            // Make sure the user now owns the item.
                            Item.ItemData.OwnerId = Params.UserInstance.UserData.UserInfo.Id;
                            Params.UserInstance.InventoryService.AddItem(Item);

                            // Save the furni!
                            Item.ItemData.Update().Execute();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles a return furni request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void ReturnFurniRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    ReturnFurniRequestPacket PacketModel = JSON.DeSerialize<ReturnFurniRequestPacket>(Params.PacketBody);

                    // Get the users rights (we will need them a lot)!
                    if (Params.UserInstance.Avatar.CurrentRoom.RoomType == RoomType.PrivateRoom && (((IPrivateRoom)Params.UserInstance.Avatar.CurrentRoom).GetRightsForUser(Params.UserInstance).CanPickupFurniToOwnersInventory || Params.UserInstance.UserData.UsersRank.CanOverridePickupFurniToOwnerInPrivateRooms))
                    {
                        // Get the furni.
                        IFurni Item = Params.UserInstance.Avatar.CurrentRoom.FurniManger.GetItem(PacketModel.I);

                        // Attempt to move the item.
                        bool Success = Params.UserInstance.Avatar.CurrentRoom.FurniManger.RemoveItem(Item);

                        // Find out if it was ok.
                        if (Success)
                        {
                            // Update the item.
                            Item.ItemData.Tile = "inv";

                            // Make sure the user now owns the item.
                            IUser ItemOwner = this._ServerManager.UsersManager.GetUserById(Item.ItemData.OwnerId);
                            if (ItemOwner != null)
                            {
                                ItemOwner.InventoryService.AddItem(Item);
                            }

                            // Save the furni!
                            Item.ItemData.Update().Execute();
                        }
                    }
                }
            }
        }
    }
}
