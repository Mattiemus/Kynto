using System;

using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.EventHandlers
{
    /// <summary>
    /// Handles api related events.
    /// </summary>
    public class ApiEventHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of event handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public ApiEventHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles the send api tunnelled data event.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void ApiSendTunneledDataEventHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments
            ApiEventArguments EventArguments = (ApiEventArguments)Args;

            // Create the packet.
            ApiTunnelPacketResponse ApiTunnelPacketResponse = new ApiTunnelPacketResponse();
            ApiTunnelPacketResponse.F = EventArguments.UserInstance.Avatar.Username;
            ApiTunnelPacketResponse.M = EventArguments.Data;
            // Convert to string.
            string ApiTunnelPacketResponseString = JSON.Serializer<ApiTunnelPacketResponse>(ApiTunnelPacketResponse);

            // Send to all targets.
            for (int i = 0; i < EventArguments.To.Length; i++)
            {
                // Get the user.
                IGameSocketConnection TargetUser = (IGameSocketConnection)this._ServerManager.UsersManager.GetUserByUsername(EventArguments.To[i]);
                if (TargetUser != null)
                {
                    TargetUser.SendData(ProtocolInfo.ServerPackets.ApiTunnelMessage + ApiTunnelPacketResponseString + ProtocolInfo.Terminator, true);
                }
            }
        }
    }
}
