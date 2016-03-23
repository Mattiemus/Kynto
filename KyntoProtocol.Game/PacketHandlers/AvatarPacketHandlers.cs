using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

using KyntoProtocol.Game.ChatCommands;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles connection related packets.
    /// </summary>
    public class AvatarPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the chat commands manager.
        /// </summary>
        private ChatCommandsManager _ChatCommandsManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public AvatarPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
            this._ChatCommandsManager = new ChatCommandsManager(ServerInstance);
        }

        /// <summary>
        /// Handles a avatar walk request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void RoomWalkRequestPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn)
            {
                // Make sure the user is in a room.
                if (Params.UserInstance.Avatar.CurrentRoom != null)
                {
                    // Get the packet.
                    MoveAvatarRequestPacket PacketModel = JSON.DeSerialize<MoveAvatarRequestPacket>(Params.PacketBody);

                    // Attempt to move the avatar.
                    Params.UserInstance.Avatar.TargetX = PacketModel.X;
                    Params.UserInstance.Avatar.TargetY = PacketModel.Y;
                }
            }
        }

        /// <summary>
        /// Handles a chat request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void SendChatRequestPacketHandler(object Sender, EventArgs Args)
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
                    SendMessagePacket PacketModel = JSON.DeSerialize<SendMessagePacket>(Params.PacketBody);

                    // Check the chat.
                    if (PacketModel.T == "R")
                    {
                        // Chat commands.
                        if (PacketModel.M.StartsWith("/") && _ChatCommandsManager.Execute(Params.UserInstance, PacketModel.M))
                        {
                            return;
                        }

                        // Cut the chat message down to length.
                        if (PacketModel.M.Length >= 65)
                        {
                            PacketModel.M = PacketModel.M.Substring(0, 65);
                        }
                        Params.UserInstance.Avatar.CurrentRoom.SendChat(Params.UserInstance.Avatar, PacketModel.M);
                    }
                }
            }
        }
    }
}
