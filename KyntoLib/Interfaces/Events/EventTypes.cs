using System;

namespace KyntoLib.Interfaces.Events
{
    /// <summary>
    /// All possible event types.
    /// </summary>
    public enum EventType
    {
        ServerStarted,
        ServerExiting,

        GameNewConnection,
        GameLostConnection,
        GameRecivedPacket,

        BackendNewConnection,
        BackendLostConnection,
        BackendRecivedPacket,

        ApiTunnelMessage,

        UserLoginAccepted,
        UserLoggedOut,
        UserLoginSuccessful,
        GuestLoginAccepted,
        GuestLoggedOut,
        GuestLoginSuccessful,
        LoginFailed,

        UserDataUpdated,

        AvatarAdded,
        AvatarRemoved,
        AvatarWalk,
        AvatarTalked,
        AvatarUpdated,

        RoomInitialised,
        RoomShutdown,
        RoomJoined,
        RoomKick,

        FurniActivated,
        FurniMoved,
        FurniRemoved,
        FurniPlaced,
        
        InventoryAddItem,
        InventoryRemoveItem,

        PdaLoaded,
        PdaFriendLoggedIn,
        PdaFriendLoggedOut,

        CommandToggleBrb
    }
}
