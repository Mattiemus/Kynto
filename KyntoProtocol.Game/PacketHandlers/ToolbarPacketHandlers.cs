using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles toolbar related packets.
    /// </summary>
    public class ToolbarPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public ToolbarPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles a request sit packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void AvatarSitRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Now check that the user is not already sitting or is currently walking.
                    if (Params.UserInstance.Avatar.Status != "sit" && Params.UserInstance.Avatar.Status != "walk")
                    {
                        // Make sure this tile isnt already a seat.
                        if (Params.UserInstance.Avatar.CurrentRoom.HeightmapManger.StateMap[Params.UserInstance.Avatar.LocationX, Params.UserInstance.Avatar.LocationY] != TileState.Seat)
                        {
                            // Store the new avatars status.
                            Params.UserInstance.Avatar.Status = "sit";
                            Params.UserInstance.Avatar.WalkStatus = "walk_new";

                            // Create the packet.
                            MoveAvatarPacket MoveAvatarPacket = new MoveAvatarPacket();
                            MoveAvatarPacket.I = Params.UserInstance.Avatar.AvatarId;
                            MoveAvatarPacket.X = Params.UserInstance.Avatar.LocationX;
                            MoveAvatarPacket.Y = Params.UserInstance.Avatar.LocationY;
                            MoveAvatarPacket.H = Params.UserInstance.Avatar.LocationH;
                            MoveAvatarPacket.SH = Params.UserInstance.Avatar.LocationSH;
                            MoveAvatarPacket.S = Params.UserInstance.Avatar.Status;
                            MoveAvatarPacket.W = Params.UserInstance.Avatar.WalkStatus;
                            // Convert to string.
                            string MovePacketString = JSON.Serializer<MoveAvatarPacket>(MoveAvatarPacket);
                            // Send data.
                            Params.UserInstance.Avatar.CurrentRoom.SendData(ProtocolInfo.ServerPackets.MoveAvatar + MovePacketString + ProtocolInfo.Terminator);
                        }
                    }
                    else if (Params.UserInstance.Avatar.Status != "walk")
                    {
                        // Make sure this tile isnt a seat.
                        if (Params.UserInstance.Avatar.CurrentRoom.HeightmapManger.StateMap[Params.UserInstance.Avatar.LocationX, Params.UserInstance.Avatar.LocationY] != TileState.Seat)
                        {
                            // Save the new status.
                            Params.UserInstance.Avatar.Status = "idle";
                            Params.UserInstance.Avatar.WalkStatus = "walk_new";

                            // Create the packet.
                            MoveAvatarPacket MoveAvatarPacket = new MoveAvatarPacket();
                            MoveAvatarPacket.I = Params.UserInstance.Avatar.AvatarId;
                            MoveAvatarPacket.X = Params.UserInstance.Avatar.LocationX;
                            MoveAvatarPacket.Y = Params.UserInstance.Avatar.LocationY;
                            MoveAvatarPacket.H = Params.UserInstance.Avatar.LocationH;
                            MoveAvatarPacket.SH = Params.UserInstance.Avatar.LocationSH;
                            MoveAvatarPacket.S = Params.UserInstance.Avatar.Status;
                            MoveAvatarPacket.W = Params.UserInstance.Avatar.WalkStatus;
                            // Convert to string.
                            string MovePacketString = JSON.Serializer<MoveAvatarPacket>(MoveAvatarPacket);
                            // Send the data.
                            Params.UserInstance.Avatar.CurrentRoom.SendData(ProtocolInfo.ServerPackets.MoveAvatar + MovePacketString + ProtocolInfo.Terminator);
                        }
                    }
                }
            }
        }
    }
}
