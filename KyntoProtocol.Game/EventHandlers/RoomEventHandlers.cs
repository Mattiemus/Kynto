using System;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.User.Data;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.EventHandlers
{
    /// <summary>
    /// Handles room related events.
    /// </summary>
    public class RoomEventHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public RoomEventHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the room joined event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomJoinedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomEventArguments EventArguments = (RoomEventArguments)Args;

            // Send the room data.
            switch (EventArguments.RoomInstance.RoomType)
            {
                case RoomType.PrivateRoom:
                    {
                        // Private room.
                        // Get the room.
                        IPrivateRoom PrivateRoomInstance = (IPrivateRoom)EventArguments.RoomInstance;

                        // Get the users rights (we will need them a lot)!
                        IRoomRightsData Rights = ((IPrivateRoom)EventArguments.UserInstance.Avatar.CurrentRoom).GetRightsForUser(EventArguments.UserInstance);

                        // Create the packet.
                        PrivateRoomLoadDataPacket RoomDataPacket = new PrivateRoomLoadDataPacket();
                        RoomDataPacket.I = PrivateRoomInstance.DatabaseTable.Id;
                        RoomDataPacket.T = (int)RoomType.PrivateRoom;
                        RoomDataPacket.V = PrivateRoomInstance.DatabaseTable.Views;
                        RoomDataPacket.N = PrivateRoomInstance.DatabaseTable.Name;
                        RoomDataPacket.D = PrivateRoomInstance.DatabaseTable.Description;
                        RoomDataPacket.O = PrivateRoomInstance.DatabaseTable.OwnerId;
                        RoomDataPacket.Fl = PrivateRoomInstance.DatabaseTable.FloorId;
                        RoomDataPacket.Wl = PrivateRoomInstance.DatabaseTable.WallId;
                        RoomDataPacket.X = PrivateRoomInstance.DatabaseTable.Rows;
                        RoomDataPacket.Y = PrivateRoomInstance.DatabaseTable.Cols;
                        RoomDataPacket.Bg = PrivateRoomInstance.DatabaseTable.BgId;
                        RoomDataPacket.U = PrivateRoomInstance.UsersInRoom.Count;
                        RoomDataPacket.H = PrivateRoomInstance.HeightmapManger.HolesMap;
                        RoomDataPacket.F = FurnictureArrayPacket.FromArray(PrivateRoomInstance.FurniManger.ToArray());
                        RoomDataPacket.R = new RoomRightsPacket()
                            {
                                MF = Rights.CanMoveFurni || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanOverrideMoveFurniInPrivateRooms),
                                PF = Rights.CanPickupFurniToOwnInventory || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanOverridePickupFurniToOwnInPrivateRooms),
                                RF = Rights.CanPickupFurniToOwnersInventory || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanOverridePickupFurniToOwnerInPrivateRooms),
                                DF = Rights.CanDropFurni || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanOverrideDropFurniInPrivateRooms),
                                AF = Rights.CanActivateFurni || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanOverrideActivateFurniInPrivateRooms),
                                K = Rights.CanKickUser || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanKickUserFromPrivateRoom),
                                B = Rights.CanBanUser || (!EventArguments.UserInstance.UserData.IsGuest && EventArguments.UserInstance.UserData.UsersRank.CanPrivateRoomBanUser)
                            };
                        // Convert to string.
                        string RoomDataString = JSON.Serializer<PrivateRoomLoadDataPacket>(RoomDataPacket);
                        // Send.
                        EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.RoomData + RoomDataString + ProtocolInfo.Terminator, true);

                        break;
                    }

                case RoomType.PublicRoom:
                    {
                        // Public room.
                        // Get the room.
                        IPublicRoom PublicRoomInstance = (IPublicRoom)EventArguments.RoomInstance;

                        // Create the packet.
                        PublicRoomLoadDataPacket RoomDataPacket = new PublicRoomLoadDataPacket();
                        RoomDataPacket.I = PublicRoomInstance.DatabaseTable.Id;
                        RoomDataPacket.T = (int)RoomType.PublicRoom;
                        RoomDataPacket.N = PublicRoomInstance.DatabaseTable.Name;
                        RoomDataPacket.D = PublicRoomInstance.DatabaseTable.Description;
                        RoomDataPacket.Da = Convert.ToBase64String(Encoding.ASCII.GetBytes(PublicRoomInstance.DatabaseTable.XmlData));
                        RoomDataPacket.H = PublicRoomInstance.HeightmapManger.HeightmapString;
                        RoomDataPacket.X = PublicRoomInstance.DatabaseTable.Rows;
                        RoomDataPacket.Y = PublicRoomInstance.DatabaseTable.Cols;
                        RoomDataPacket.OX = PublicRoomInstance.DatabaseTable.OffsetX;
                        RoomDataPacket.OY = PublicRoomInstance.DatabaseTable.OffsetY;
                        RoomDataPacket.BL = PublicRoomInstance.DatabaseTable.BgLayer;
                        RoomDataPacket.BI = PublicRoomInstance.DatabaseTable.BgImage;
                        RoomDataPacket.F = FurnictureArrayPacket.FromArray(PublicRoomInstance.FurniManger.ToArray());
                        RoomDataPacket.E = PublicRoomInstance.DatabaseTable.EntryTile;
                        // Convert to string.
                        string RoomDataString = JSON.Serializer<PublicRoomLoadDataPacket>(RoomDataPacket);
                        // Send.
                        EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.RoomData + RoomDataString + ProtocolInfo.Terminator, true);

                        break;
                    }
            }

            // Send user avatars in room (apart from his/her own).
            for (int i = 0; i < EventArguments.RoomInstance.AvatarsInRoom.Count; i++)
            {
                if (EventArguments.RoomInstance.AvatarsInRoom[i] != EventArguments.AvatarInstance)
                {
                    // Create the packet.
                    AddAvatarPacket Avatars = new AddAvatarPacket();
                    Avatars.I = EventArguments.RoomInstance.AvatarsInRoom[i].AvatarId;
                    Avatars.UI = EventArguments.RoomInstance.AvatarsInRoom[i].UserId;
                    Avatars.U = EventArguments.RoomInstance.AvatarsInRoom[i].Username;
                    Avatars.M = EventArguments.RoomInstance.AvatarsInRoom[i].Motto;
                    Avatars.C = EventArguments.RoomInstance.AvatarsInRoom[i].Clothes;
                    Avatars.S = EventArguments.RoomInstance.AvatarsInRoom[i].Sex;
                    Avatars.B = EventArguments.RoomInstance.AvatarsInRoom[i].Badge;
                    Avatars.X = EventArguments.RoomInstance.AvatarsInRoom[i].LocationX;
                    Avatars.Y = EventArguments.RoomInstance.AvatarsInRoom[i].LocationY;
                    if (EventArguments.RoomInstance.AvatarsInRoom[i].LocationSH != 0)
                    {
                        Avatars.H = EventArguments.RoomInstance.AvatarsInRoom[i].LocationSH;
                    }
                    else
                    {
                        Avatars.H = EventArguments.RoomInstance.AvatarsInRoom[i].LocationH;
                    }
                    Avatars.St = EventArguments.RoomInstance.AvatarsInRoom[i].Status;
                    if (EventArguments.RoomInstance.AvatarsInRoom[i].IsGhost)
                    {
                        Avatars.Sta = "brb";
                    }
                    else
                    {
                        Avatars.Sta = "online";
                    }
                    Avatars.T = EventArguments.RoomInstance.AvatarsInRoom[i].Type;
                    // Convert to string.
                    string AvatarsString = JSON.Serializer<AddAvatarPacket>(Avatars);
                    // Send.
                    EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.AddAvatar + AvatarsString + ProtocolInfo.Terminator, true);
                }
            }
        }

        /// <summary>
        /// Handles the room joined event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomKickedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomEventArguments EventArguments = (RoomEventArguments)Args;

            // Send the kicked user away.
            EventArguments.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.Kicked + ProtocolInfo.Terminator, true);
        }
    }
}
