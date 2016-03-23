using System;

using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Events
{
    /// <summary>
    /// The event arguments for a backend socket event.
    /// </summary>
    public class BackendSocketsEventArguments : EventArgs
    {
        public IBackendSocketConnection BackendSocketConnection;
    }

    /// <summary>
    /// The event arguments for the backend sockets packet received event.
    /// </summary>
    public class BackendPacketReceivedEventArguments : BackendSocketsEventArguments
    {
        public string RecivedData;
    }


    /// <summary>
    /// The event arguments for a game socket event.
    /// </summary>
    public class GameSocketsEventArguments : EventArgs
    {
        public IUser UserInstance;
        public IGameSocketConnection GameSocketConnection;
    }

    /// <summary>
    /// The event arguments for the game sockets packet received event.
    /// </summary>
    public class GameSocketsReceivedEventArguments : GameSocketsEventArguments
    {
        public string RecivedData;
    }

    /// <summary>
    /// The event arguments for the packet received event.
    /// </summary>
    public class GameSocketsPacketReceivedEventArguments : GameSocketsReceivedEventArguments
    {
        public string PacketHeader;
        public string PacketBody;
    }

    /// <summary>
    /// The event arguments for a user login accepted event.
    /// </summary>
    public class UserLoginAcceptedEventArguments : GameSocketsEventArguments
    {
        public IMembersDatabaseTable UsersInfo;
    }

    /// <summary>
    /// The event arguments for a guest login accepted event.
    /// </summary>
    public class GuestLoginAcceptedEventArguments : GameSocketsEventArguments
    {
    }

    /// <summary>
    /// The event arguments for a login failed event.
    /// </summary>
    public class LoginFailedEventArguments : GameSocketsEventArguments
    {
        public string FailureReason;
        public string BanReason;
        public long BanEndTimestamp;
    }

    /// <summary>
    /// The event arguments for a room event.
    /// </summary>
    public class RoomEventArguments : GameSocketsEventArguments
    {
        public IRoom RoomInstance;
        public IAvatar AvatarInstance;
    }

    /// <summary>
    /// The event arguments for an room item event.
    /// </summary>
    public class RoomItemEventArguments : RoomEventArguments
    {
        public IFurni Item;
    }

    /// <summary>
    /// The event arguments for a room chat event.
    /// </summary>
    public class RoomChatEventArguments : RoomEventArguments
    {
        public string ChatMessage;
    }

    /// <summary>
    /// The event arguments for an item event.
    /// </summary>
    public class ItemEventArguments : GameSocketsEventArguments
    {
        public IFurni Item;
    }

    /// <summary>
    /// The event arguments for an api event.
    /// </summary>
    public class ApiEventArguments : GameSocketsEventArguments
    {
        public string[] To;
        public string Data;
    }
}
