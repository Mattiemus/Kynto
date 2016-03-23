using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// Stores the protocol information.
    /// </summary>
    public static class ProtocolInfo
    {
        /// <summary>
        /// Stores the header length.
        /// </summary>
        public static int HeaderLength = 2;

        /// <summary>
        /// Stores the terminator that marks the end of a client-sent packet.
        /// </summary>
        public static string Terminator = "#";

        /// <summary>
        /// Stores all off the packets that the client would send.
        /// </summary>
        public static class ClientPackets
        {
            /// <summary>
            /// A standard login request.
            /// </summary>
            public const string LoginRequest = "Li";

            /// <summary>
            /// Login complete.
            /// </summary>
            public const string LoginCompleated = "LC";

            /// <summary>
            /// Purchase an item from the catalogue.
            /// </summary>
            public const string CatalogePurchaseItem = "XP";

            /// <summary>
            /// Request catalogue page data.
            /// </summary>
            public const string CatalogeRequestPageData = "XR";

            /// <summary>
            /// Request catalogue global data.
            /// </summary>
            public const string CatalogeRequestCatalogueData = "XC";

            /// <summary>
            /// Request the navigator packet.
            /// </summary>
            public const string RequestNavigator = "RN";

            /// <summary>
            /// The request create room packet.
            /// </summary>
            public const string CreateRoom = "RQ";

            /// <summary>
            /// The editor room info packet.
            /// </summary>
            public const string EditRoomInfoRequest = "IG";

            /// <summary>
            /// The room save info packets.
            /// </summary>
            public const string EditRoomSaveEdits = "SX";

            /// <summary>
            /// Open changer window.
            /// </summary>
            public const string OpenAvatarChangerWindow = "OC";

            /// <summary>
            /// Save the new avatar data.
            /// </summary>
            public const string SaveAvatarData = "SC";

            /// <summary>
            /// Request friendship.
            /// </summary>
            public const string FriendRequest = "Pf";

            /// <summary>
            /// Accept a friend request.
            /// </summary>
            public const string AcceptFriendRequest = "Pa";

            /// <summary>
            /// Deny a friend request.
            /// </summary>
            public const string DenyFriendRequest = "Pd";

            /// <summary>
            /// Room chat send request packet.
            /// </summary>
            public const string SendChat = "SM";

            /// <summary>
            /// Move avatar request packet.
            /// </summary>
            public const string MoveAvatarRequest = "mA";

            /// <summary>
            /// Avatar sit request packet.
            /// </summary>
            public const string RequestAvatarSit = "AS";

            /// <summary>
            /// Move furni request packet.
            /// </summary>
            public const string MoveFurnictureRequest = "Fm";

            /// <summary>
            /// Pick up furni request packet.
            /// </summary>
            public const string PickFurnictureRequest = "Fg";

            /// Return furni request packet.
            /// </summary>
            public const string ReturnFurnictureRequest = "Fq";

            /// Delete furni request packet.
            /// </summary>
            public const string DeleteFurnictureRequest = "F~";

            /// <summary>
            /// Drop furni request packet.
            /// </summary>
            public const string DropFurnictureRequest = "Fd";

            /// <summary>
            /// Activate furni request packet.
            /// </summary>
            public const string ActivateFurnictureRequest = "Fc";

            /// <summary>
            /// Change room request packet.
            /// </summary>
            public const string RoomChangeRequest = "RC";

            /// <summary>
            /// Leave room request packet.
            /// </summary>
            public const string RoomLeaveRequest = "RL";

            /// <summary>
            /// Send api tunnel message.
            /// </summary>
            public const string ApiTunnel = "AT";
        }

        /// <summary>
        /// Stores all of the packets that the server would send.
        /// </summary>
        public static class ServerPackets
        {
            /// <summary>
            /// The connection was setup ok.
            /// </summary>
            public const string ConnectionOK = "C!";

            /// <summary>
            /// There was an error with the connection
            /// </summary>
            public const string ConnectionError = "CE";

            /// <summary>
            /// The server version information.
            /// </summary>
            public const string ServerVersion = "SV";

            /// <summary>
            /// Login accepted response.
            /// </summary>
            public const string LoginAccepted = "LA";

            /// <summary>
            /// Login failed response.
            /// </summary>
            public const string LoginFailed = "LF";

            /// <summary>
            /// User data response (login accepted).
            /// </summary>
            public const string UserData = "UD";

            /// <summary>
            /// Contains all the catalogue data.
            /// </summary>
            public const string CatalogueData = "cD";

            /// <summary>
            /// Create a message box.
            /// </summary>
            public const string MessageBox = "Mg";

            /// <summary>
            /// The room creation was ok packet.
            /// </summary>
            public const string RoomCreationOk = "QZ";

            /// <summary>
            /// The room edit ok packet.
            /// </summary>
            public const string RoomEditOK = "FZ";

            /// <summary>
            /// The room edit response packet.
            /// </summary>
            public const string RoomEditResponse = "ET";

            /// <summary>
            /// The navigator data.
            /// </summary>
            public const string NavigatorData = "ND";

            /// <summary>
            /// Update avatar details.
            /// </summary>
            public const string AvatarChangerDetails = "AC";

            /// <summary>
            /// The send friend data packet.
            /// </summary>
            public const string SendFriendData = "PF";

            /// <summary>
            /// The update friend data packet.
            /// </summary>
            public const string UpdateFriendData = "PU";

            /// <summary>
            /// The friend requested packet.
            /// </summary>
            public const string FriendRequestRecived = "PR";

            /// <summary>
            /// The remove item from inventory packet.
            /// </summary>
            public const string RemoveItem = "Hr";

            /// <summary>
            /// The add item too inventory packet.
            /// </summary>
            public const string AddItem = "Ha";

            /// <summary>
            /// Spam initiated packet.
            /// </summary>
            public const string SpamInitiated = "Sp";

            /// <summary>
            /// Send message packet.
            /// </summary>
            public const string SendMessage = "Sm";

            /// <summary>
            /// Add avatar packet.
            /// </summary>
            public const string AddAvatar = "AA";

            /// <summary>
            /// Move avatar packet.
            /// </summary>
            public const string MoveAvatar = "MA";

            /// <summary>
            /// Update avatar status packet.
            /// </summary>
            public const string UpdateAvatarStatus = "Us";

            /// <summary>
            /// Remove avatar packet.
            /// </summary>
            public const string RemoveAvatar = "RA";

            /// <summary>
            /// Move furni packet.
            /// </summary>
            public const string MoveFurni = "FM";

            /// <summary>
            /// Remove furni packet.
            /// </summary>
            public const string RemoveFurni = "Fk";

            /// <summary>
            /// Add furni packet.
            /// </summary>
            public const string AddFurni = "FY";

            /// <summary>
            /// Update furni packet.
            /// </summary>
            public const string UpdateFurni = "FU";

            /// <summary>
            /// Room data packet.
            /// </summary>
            public const string RoomData = "Rd";

            /// <summary>
            /// Change failed packet.
            /// </summary>
            public const string ChangeFailed = "RF";

            /// <summary>
            /// Change successful packet.
            /// </summary>
            public const string ChangeSuccessful = "Rc";

            /// <summary>
            /// Kicked from room packet.
            /// </summary>
            public const string Kicked = "RK";

            /// <summary>
            /// Api tunnel message.
            /// </summary>
            public const string ApiTunnelMessage = "At";
        }

        /// <summary>
        /// Stores all of the packets that both the client and server send too each other.
        /// </summary>
        public static class SharedPackets
        {
            /// <summary>
            /// The connection was accepted.
            /// </summary>
            public const string ConnectionAccepted = "C!";

            /// <summary>
            /// The connection was closed.
            /// </summary>
            public const string ConnectionClosed = "DC";
        }
    }
}
