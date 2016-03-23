using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Helpers;
using KyntoLib.Encryption;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles incoming packets.
    /// </summary>
    public class PacketHandler
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the dictionary of all packet handlers.
        /// </summary>
        private Dictionary<string, EventHandler> _PacketHandlers = new Dictionary<string, EventHandler>();
        
        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public PacketHandler(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;

            // Add all packet handlers.
            // Connection packets.
            ConnectionPacketHandlers ConnectionPacketHandlers = new ConnectionPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.SharedPackets.ConnectionClosed, ConnectionPacketHandlers.DisconnectRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.LoginRequest, ConnectionPacketHandlers.LoginRequestPacketHandler);
            // Room packets.
            RoomPacketHandlers RoomPacketHandlers = new RoomPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.RoomChangeRequest, RoomPacketHandlers.RoomEntryRequestPacketHandler);
            // Avatar packets.
            AvatarPacketHandlers AvatarPacketHandlers = new AvatarPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.MoveAvatarRequest, AvatarPacketHandlers.RoomWalkRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.SendChat, AvatarPacketHandlers.SendChatRequestPacketHandler);
            // Furni packets.
            FurniPacketHandlers FurniPacketHandlers = new FurniPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.ActivateFurnictureRequest, FurniPacketHandlers.ActivateFurniRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.DeleteFurnictureRequest, FurniPacketHandlers.DeleteFurniRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.DropFurnictureRequest, FurniPacketHandlers.DropFurniRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.MoveFurnictureRequest, FurniPacketHandlers.MoveFurniRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.PickFurnictureRequest, FurniPacketHandlers.PickUpFurniRequestPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.ReturnFurnictureRequest, FurniPacketHandlers.ReturnFurniRequestPacketHandler);
            // Api packets.
            ApiPacketHandlers ApiPacketHandlers = new ApiPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.ApiTunnel, ApiPacketHandlers.ApiTunnelRedirectPacketHandler);
            // Catalogue packets.
            CataloguePacketHandlers CataloguePacketHandlers = new CataloguePacketHandlers(ServerInstance);
            // Clothing changer packets.
            ClothesChangerPacketHandlers ClothesChangerPacketHandlers = new ClothesChangerPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.OpenAvatarChangerWindow, ClothesChangerPacketHandlers.AvatarChangerRequestDataPacketHandler);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.SaveAvatarData, ClothesChangerPacketHandlers.AvatarChangerSaveDataPacketHandler);
            // Inventory packets.
            // Pda packets.
            PdaPacketHandlers PdaPacketHandlers = new PdaPacketHandlers(ServerInstance);
            // Room editor packets.
            // Toolbar packets.
            ToolbarPacketHandlers ToolbarPacketHandlers = new ToolbarPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.RequestAvatarSit, ToolbarPacketHandlers.AvatarSitRequestPacketHandler);
            // Trading packets.
            TradingPacketHandlers TradingPacketHandlers = new TradingPacketHandlers(ServerInstance);
            // Navigator packets.
            NavigatorPacketHandlers NavigatorPacketHandlers = new NavigatorPacketHandlers(ServerInstance);
            this._PacketHandlers.Add(ProtocolInfo.ClientPackets.RequestNavigator, NavigatorPacketHandlers.RequestNavigatorPacketHandler);
        }

        /// <summary>
        /// Handles the packet received event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void PacketRecivedEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            GameSocketsReceivedEventArguments EventArguments = (GameSocketsReceivedEventArguments)Args;

            // If we are in production mode check for policy file request.
            if (!bool.Parse(this._ServerManager.SettingsService.GetValue("DevMode")))
            {
                if (EventArguments.RecivedData.StartsWith("<policy"))
                {
                    // Send the policy file and remember the request.
                    EventArguments.GameSocketConnection.SendData("<?xml version=\"1.0\"?><cross-domain-policy><allow-access-from domain=\"*\" to-ports=\"*\" secure=\"false\" /><site-control permitted-cross-domain-policies=\"master-only\" /></cross-domain-policy>", false);
                    this._ServerManager.GameSocketsService.RecentPolicyRequests.Add(new RecentPolicyRequest() { IP = EventArguments.GameSocketConnection.SocketInstance.RemoteEndPoint.ToString().Split(':')[0], Timestamp = DateTime.Now });
                        //EventArguments.GameSocketConnection.Disconnect();

                    // Finish.
                    return;
                }
            }

            // Get the decrypted data.
            try
            {
                EventArguments.RecivedData = RC4Encryption.Decrypt(EventArguments.RecivedData, this._ServerManager.SettingsService.GetValue("Network.Game.EncryptionKey"));
            }
            catch (Exception ex)
            {
                this._ServerManager.LoggingService.Write(LogImportance.Debug, "Dropped packet", ex);
            }

            // Retrieve the data from the packet.
            string PacketHeader = EventArguments.RecivedData.Substring(0, ProtocolInfo.HeaderLength);
            string PacketBody = EventArguments.RecivedData.Substring(ProtocolInfo.HeaderLength);

            // Write that we received data.
            this._ServerManager.LoggingService.WritePacket(EventArguments.GameSocketConnection.SocketId, PacketDirection.In, PacketType.Game, EventArguments.RecivedData);

            // Search for the packet handler.
            if (this._PacketHandlers.ContainsKey(PacketHeader))
            {
                // Execute the handler.
                _PacketHandlers[PacketHeader](this, new GameSocketsPacketReceivedEventArguments() { GameSocketConnection = EventArguments.GameSocketConnection, UserInstance = EventArguments.UserInstance, RecivedData = EventArguments.RecivedData, PacketHeader = PacketHeader, PacketBody = PacketBody });
                return;
            }

            // No packet handler found!
            if (PacketHeader != null)
            {
                this._ServerManager.LoggingService.Write(LogImportance.Normal, "[Game][" + EventArguments.GameSocketConnection.SocketId + "] Game packet handler for: \"" + PacketHeader + "\" Not found...", null);
            }
            else
            {
                this._ServerManager.LoggingService.Write(LogImportance.Normal, "[Game][" + EventArguments.GameSocketConnection.SocketId + "] Game packet handler not found!", null);
            }
        }
    }
}
