using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Plugins;

using KyntoProtocol.Game.EventHandlers;
using KyntoProtocol.Game.PacketHandlers;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The game library plugin.
    /// </summary>
    public class GameProtocolPlugin : IPlugin
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Gets the plugin's name.
        /// </summary>
        public string PluginName
        {
            get
            {
                return "GameProtocol";
            }
        }

        /// <summary>
        /// Initialises the plugin.
        /// </summary>
        /// <param name="ServerManager">The parent server manager.</param>
        public void Initialise(IServerManager ServerManager)
        {
            // Store.
            this._ServerManager = ServerManager;


            // Create event handlers.
            // Connection events.
            ConnectionEventHandlers ConnectionEventHandlers = new ConnectionEventHandlers(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.GameNewConnection, ConnectionEventHandlers.UserConnectedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.GameLostConnection, ConnectionEventHandlers.UserDisconnectedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.GuestLoginAccepted, ConnectionEventHandlers.GuestLoginAcceptedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.UserLoginAccepted, ConnectionEventHandlers.UserLoginAcceptedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.UserLoginSuccessful, ConnectionEventHandlers.LoginSuccessfulEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.GuestLoginSuccessful, ConnectionEventHandlers.LoginSuccessfulEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.LoginFailed, ConnectionEventHandlers.LoginFailedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.UserDataUpdated, ConnectionEventHandlers.UserDataUpdatedEventHandler);
            // Room events.
            RoomEventHandlers RoomEventHandlers = new RoomEventHandlers(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.RoomJoined, RoomEventHandlers.RoomJoinedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.RoomKick, RoomEventHandlers.RoomKickedEventHandler);
            // Avatar events.
            AvatarEventHandlers AvatarEventHandlers = new AvatarEventHandlers(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.AvatarAdded, AvatarEventHandlers.RoomAvatarAddedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.AvatarRemoved, AvatarEventHandlers.RoomAvatarRemovedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.AvatarWalk, AvatarEventHandlers.RoomAvatarWalkedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.AvatarTalked, AvatarEventHandlers.RoomChatSentEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.AvatarUpdated, AvatarEventHandlers.AvatarUpdatedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.CommandToggleBrb, AvatarEventHandlers.AvatarToggledBrbEventHandler);
            // Furni events.
            FurniEventHandlers FurniEventHandlers = new FurniEventHandlers(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.FurniMoved, FurniEventHandlers.FurniMovedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.FurniRemoved, FurniEventHandlers.FurniRemovedEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.FurniPlaced, FurniEventHandlers.FurniPlacedEventHandler);
            // Api.
            ApiEventHandlers ApiEventHandlers = new ApiEventHandlers(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.ApiTunnelMessage, ApiEventHandlers.ApiSendTunneledDataEventHandler);
            // Catalogue events.
            // Clothing changer events.
            // Inventory events.
            InventoryEventHandlers InventoryEventHandlers = new InventoryEventHandlers(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.InventoryAddItem, InventoryEventHandlers.AddInventoryItemEventHandler);
            ServerManager.EventsManager.HookEvent(EventType.InventoryRemoveItem, InventoryEventHandlers.RemoveInventoryItemEventHandler);
            // Pda events.
            // Room editor events.
            // Toolbar events.
            // Trading events.
            // Navigator events.

            // Initialise packet handlers.
            PacketHandler PacketHandler = new PacketHandler(ServerManager);
            ServerManager.EventsManager.HookEvent(EventType.GameRecivedPacket, PacketHandler.PacketRecivedEventHandler);
        }

        /// <summary>
        /// Shuts down the plugin.
        /// </summary>
        public void Exit()
        {
        }
    }
}
