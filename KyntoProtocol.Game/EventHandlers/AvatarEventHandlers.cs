using System;
using System.Text;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.EventHandlers
{
    /// <summary>
    /// Handles avatar related events.
    /// </summary>
    public class AvatarEventHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public AvatarEventHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the avatar added to room event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomAvatarAddedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomEventArguments EventArguments = (RoomEventArguments)Args;

            // Send users in room the new avatar.
            // Create the packet.
            AddAvatarPacket AvatarsData = new AddAvatarPacket();
            AvatarsData.I = EventArguments.AvatarInstance.AvatarId;
            AvatarsData.UI = EventArguments.AvatarInstance.UserId;
            AvatarsData.U = EventArguments.AvatarInstance.Username;
            AvatarsData.M = EventArguments.AvatarInstance.Motto;
            AvatarsData.C = EventArguments.AvatarInstance.Clothes;
            AvatarsData.S = EventArguments.AvatarInstance.Sex;
            AvatarsData.B = EventArguments.AvatarInstance.Badge;
            AvatarsData.X = EventArguments.AvatarInstance.LocationX;
            AvatarsData.Y = EventArguments.AvatarInstance.LocationY;
            AvatarsData.H = (EventArguments.AvatarInstance.LocationSH != 0) ? EventArguments.AvatarInstance.LocationSH : EventArguments.AvatarInstance.LocationH;
            AvatarsData.St = EventArguments.AvatarInstance.Status;
            AvatarsData.Sta = (EventArguments.AvatarInstance.IsGhost) ? "brb" : "online";
            AvatarsData.T = EventArguments.AvatarInstance.Type;
            // Convert to string.
            string AvatarsDataString = JSON.Serializer<AddAvatarPacket>(AvatarsData);
            // Send avatar to everyone in the room..
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.AddAvatar + AvatarsDataString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the avatar removed from room event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomAvatarRemovedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomEventArguments EventArguments = (RoomEventArguments)Args;

            // Send the removed avatar packet.
            RemoveAvatarPacket RemoveAvatarPacket = new RemoveAvatarPacket();
            RemoveAvatarPacket.I = EventArguments.AvatarInstance.AvatarId;
            // To string.
            string PacketDataString = JSON.Serializer<RemoveAvatarPacket>(RemoveAvatarPacket);
            // Send
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.RemoveAvatar + PacketDataString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the avatar walked event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomAvatarWalkedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomEventArguments EventArguments = (RoomEventArguments)Args;

            // Create the packet.
            MoveAvatarPacket PacketBody = new MoveAvatarPacket();
            PacketBody.I = EventArguments.AvatarInstance.AvatarId;
            PacketBody.X = EventArguments.AvatarInstance.LocationX;
            PacketBody.Y = EventArguments.AvatarInstance.LocationY;
            PacketBody.H = EventArguments.AvatarInstance.LocationH;
            PacketBody.SH = EventArguments.AvatarInstance.LocationSH;
            PacketBody.W = EventArguments.AvatarInstance.WalkStatus;
            PacketBody.SI = (EventArguments.AvatarInstance.Status == "sit") ? EventArguments.RoomInstance.FurniManger.GetSeatAt(EventArguments.AvatarInstance.LocationX, EventArguments.AvatarInstance.LocationY).ItemData.Id : 0;
            PacketBody.S = EventArguments.AvatarInstance.Status;
            // Convert too string.
            string MovePacketString = JSON.Serializer<MoveAvatarPacket>(PacketBody);
            // Send the data.
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.MoveAvatar + MovePacketString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the send chat event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomChatSentEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            RoomChatEventArguments EventArguments = (RoomChatEventArguments)Args;

            // Create the packet.
            SendMessageReplyPacket ToSendChatMessage = new SendMessageReplyPacket();
            ToSendChatMessage.U = EventArguments.AvatarInstance.Username;
            ToSendChatMessage.I = EventArguments.AvatarInstance.AvatarId;
            ToSendChatMessage.M = EventArguments.ChatMessage;
            // Convert to string.
            string ToSendChatMessageString = JSON.Serializer<SendMessageReplyPacket>(ToSendChatMessage);
            // Send data.
            EventArguments.RoomInstance.SendData(ProtocolInfo.ServerPackets.SendMessage + ToSendChatMessageString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the avatar updated event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void AvatarUpdatedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GameSocketsEventArguments EventArguments = (GameSocketsEventArguments)Args;

            // Create the packet.
            AvatarUpdatePacket AvatarNewData = new AvatarUpdatePacket();
            AvatarNewData.I = EventArguments.UserInstance.Avatar.AvatarId;
            AvatarNewData.S = (EventArguments.UserInstance.UserData.ActivatedCommands.Brb) ? "brb" : "online";
            AvatarNewData.Si = (EventArguments.UserInstance.Avatar.Status == "sit");
            AvatarNewData.C = EventArguments.UserInstance.Avatar.Clothes;
            // Convert to string.
            string AvatarNewDataString = JSON.Serializer<AvatarUpdatePacket>(AvatarNewData);
            // Send data.
            EventArguments.UserInstance.Avatar.CurrentRoom.SendData(ProtocolInfo.ServerPackets.UpdateAvatarStatus + AvatarNewDataString + ProtocolInfo.Terminator);
        }

        /// <summary>
        /// Handles the toggle brb event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void AvatarToggledBrbEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GameSocketsEventArguments EventArguments = (GameSocketsEventArguments)Args;

            // Check user is in a room.
            if (EventArguments.UserInstance.Avatar.CurrentRoom != null)
            {
                // Toggle brb status.
                EventArguments.UserInstance.UserData.ActivatedCommands.Brb = !EventArguments.UserInstance.UserData.ActivatedCommands.Brb;
                EventArguments.UserInstance.Avatar.IsGhost = EventArguments.UserInstance.UserData.ActivatedCommands.Brb;

                // Fire the avatar updated event.
                this._ServerManager.EventsManager.CallEvent(EventType.AvatarUpdated, this, new GameSocketsEventArguments() { GameSocketConnection = EventArguments.GameSocketConnection, UserInstance = EventArguments.UserInstance });
            }
        }
    }
}
