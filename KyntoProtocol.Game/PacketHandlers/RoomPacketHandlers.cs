using System;

using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles room related packets.
    /// </summary>
    public class RoomPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public RoomPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles a room entry request.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomEntryRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn)
            {
                // Retrieve the login request.
                RoomChangePacket PacketModel = JSON.DeSerialize<RoomChangePacket>(Params.PacketBody);

                // Make sure the packet is correctly filled in!
                if (PacketModel != null)
                {
                    // If the user is in a room, leave it.
                    if (Params.UserInstance.Avatar.CurrentRoom != null)
                    {
                        Params.UserInstance.Avatar.CurrentRoom.RemoveAvatar(Params.UserInstance.Avatar);
                    }

                    // Attempt to join the room.
                    IRoom JoinedRoom = this._ServerManager.RoomsManager.JoinRoom(Params.UserInstance, Params.GameSocketConnection, PacketModel.X + "_" + PacketModel.Y, PacketModel.RID, PacketModel.S);

                    // Check if we joined.
                    if (JoinedRoom == null)
                    {
                        // Room join failed!
                        Params.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.ChangeFailed + ProtocolInfo.Terminator, true);
                    }
                }
            }
        }
    }
}
