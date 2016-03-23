using System;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles api related packets.
    /// </summary>
    public class ApiPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public ApiPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles a api tunnel redirect packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void ApiTunnelRedirectPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in and not a guest.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Get the packet.
                ApiTunnelPacket PacketModel = JSON.DeSerialize<ApiTunnelPacket>(Params.PacketBody);

                // Redirect this to an event.
                this._ServerManager.EventsManager.CallEvent(EventType.ApiTunnelMessage, this, new ApiEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance, To = PacketModel.T, Data = PacketModel.M });
            }
        }
    }
}
