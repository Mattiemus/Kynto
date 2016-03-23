using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Instances.Rooms.Bots;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Instances.Rooms.Heightmap;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

using KyntoServer.Instances.Rooms.Furni;
using KyntoServer.Instances.Rooms.Heightmap;
using KyntoServer.Instances.Rooms.Pathfinding;

namespace KyntoServer.Instances.Rooms
{
    /// <summary>
    /// Handles the room room tasks, such as user and height map management.
    /// </summary>
    public abstract class Room : IRoom
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        protected IServerManager _ServerManager;

        /// <summary>
        /// Stores the height map manager.
        /// </summary>
        protected IHeightmapManager _HeightmapManager = null;

        /// <summary>
        /// Gets this rooms height map manager.
        /// </summary>
        public IHeightmapManager HeightmapManger
        {
            get
            {
                return this._HeightmapManager;
            }
        }

        /// <summary>
        /// Stores the furni manager.
        /// </summary>
        protected IFurniManager _FurniManager = null;

        /// <summary>
        /// Gets this rooms furni manager.
        /// </summary>
        public IFurniManager FurniManger
        {
            get
            {
                return this._FurniManager;
            }
        }

        /// <summary>
        /// Stores the list of bots in this room.
        /// </summary>
        private Dictionary<IAvatar, IBot> _BotsInRoom = new Dictionary<IAvatar, IBot>();

        /// <summary>
        /// Gets the lists of bots in this room.
        /// </summary>
        public List<IBot> BotsInRoom
        {
            get
            {
                // Convert to array.
                return new List<IBot>(this._BotsInRoom.Values.ToArray());
            }
        }

        /// <summary>
        /// Stores the list of users in this room.
        /// </summary>
        private Dictionary<IAvatar, object[]> _UsersInRoom = new Dictionary<IAvatar, object[]>();

        /// <summary>
        /// Gets the lists of users in this room.
        /// </summary>
        public List<IUser> UsersInRoom
        {
            get
            {
                // Convert to array.
                List<IUser> Buffer = new List<IUser>();
                // Propagate.
                KeyValuePair<IAvatar, object[]>[] Values = this._UsersInRoom.ToArray();
                for (int i = 0; i < Values.Length; i++)
                {
                    Buffer.Add((IUser)Values[i].Value[0]);
                }
                // Return.
                return Buffer;
            }
        }

        /// <summary>
        /// Stores the avatars in this room.
        /// </summary>
        private List<IAvatar> _AvatarsInRoom = new List<IAvatar>();

        /// <summary>
        /// Gets the lists of avatars in this room.
        /// </summary>
        public List<IAvatar> AvatarsInRoom
        {
            get
            {
                return this._AvatarsInRoom;
            }
        }

        /// <summary>
        /// Stores weather or not to keep this room active.
        /// </summary>
        private bool _RoomActive = true;

        /// <summary>
        /// Gets if this room is active.
        /// </summary>
        public bool RoomActive
        {
            get
            {
                return this._RoomActive;
            }
        }

        /// <summary>
        /// Gets this rooms type.
        /// </summary>
        public virtual RoomType RoomType
        {
            get
            {
                return RoomType.None;
            }
        }

        /// <summary>
        /// Gets this rooms id.
        /// </summary>
        public virtual int RoomId
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Initialises this room.
        /// </summary>
        public Room(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;

            // Initialise.
            this._HeightmapManager = new HeightmapManager(this);
            this._FurniManager = new FurniManager(this, ServerInstance);

            // Create the thread.
            this._ServerManager.ThreadService.CreateThread(RoomThread);
        }

        /// <summary>
        /// Adds a bot too the room.
        /// </summary>
        /// <param name="Bot">The bot too add.</param>
        public void AddBot(IBot Bot)
        {
            // Check we don't already contain this bot.
            if (!this._BotsInRoom.ContainsKey(Bot.Avatar))
            {
                // Clean our the users avatar.
                Bot.Avatar.CurrentRoom = this;
                Bot.Avatar.LocationH = 1;
                Bot.Avatar.LocationSH = 0;

                // Get this rooms entry tile.
                int XCord = 0;
                int YCord = 0;
                try
                {
                    if (this.RoomType == RoomType.PrivateRoom)
                    {
                        XCord = int.Parse(((IPrivateRoom)this).DatabaseTable.EntryTile.Split('_')[0]);
                        YCord = int.Parse(((IPrivateRoom)this).DatabaseTable.EntryTile.Split('_')[1]);
                    }
                    else if (this.RoomType == RoomType.PublicRoom)
                    {
                        XCord = int.Parse(((IPublicRoom)this).DatabaseTable.EntryTile.Split('_')[0]);
                        YCord = int.Parse(((IPublicRoom)this).DatabaseTable.EntryTile.Split('_')[1]);
                    }
                }
                catch
                {
                }

                Bot.Avatar.TargetX = XCord;
                Bot.Avatar.TargetY = YCord;
                Bot.Avatar.LocationX = XCord;
                Bot.Avatar.LocationY = YCord;
                // Reset status.
                Bot.Avatar.Status = "idle";

                // Add this bot & avatar.
                this._BotsInRoom.Add(Bot.Avatar, Bot);
                this.AddAvatar(Bot.Avatar);
            }
        }

        /// <summary>
        /// Adds a user to this room.
        /// </summary>
        /// <param name="UserInstance">The user instance.</param>
        /// <param name="UserConnection">The users connection.</param>
        /// <param name="Entrytile">The requested entry tile.</param>
        public void AddUser(IUser UserInstance, IGameSocketConnection UserConnection, string Entrytile)
        {
            // Check we don't already contain this user.
            if (!this._UsersInRoom.ContainsKey(UserInstance.Avatar))
            {
                // Clean our the users avatar.
                UserInstance.Avatar.CurrentRoom = this;
                UserInstance.Avatar.LocationH = 1;
                UserInstance.Avatar.LocationSH = 0;

                // Get this rooms entry tile.
                int XCord = 0;
                int YCord = 0;
                try
                {
                    if (this.RoomType == RoomType.PrivateRoom)
                    {
                        XCord = int.Parse(((IPrivateRoom)this).DatabaseTable.EntryTile.Split('_')[0]);
                        YCord = int.Parse(((IPrivateRoom)this).DatabaseTable.EntryTile.Split('_')[1]);
                    }
                    else if (this.RoomType == RoomType.PublicRoom)
                    {
                        if (Entrytile == null || Entrytile == "")
                        {
                            XCord = int.Parse(((IPublicRoom)this).DatabaseTable.EntryTile.Split('_')[0]);
                            YCord = int.Parse(((IPublicRoom)this).DatabaseTable.EntryTile.Split('_')[1]);
                        }
                        else
                        {
                            XCord = int.Parse(Entrytile.Split('_')[0]);
                            YCord = int.Parse(Entrytile.Split('_')[1]);
                        }
                    }
                }
                catch
                {
                }

                UserInstance.Avatar.TargetX = XCord;
                UserInstance.Avatar.TargetY = YCord;
                UserInstance.Avatar.LocationX = XCord;
                UserInstance.Avatar.LocationY = YCord;
                // Reset status.
                UserInstance.Avatar.Status = "idle";

                // Call the room loaded event.
                this._ServerManager.EventsManager.CallEvent(EventType.RoomJoined, this, new RoomEventArguments() { GameSocketConnection = UserConnection, UserInstance = UserInstance, RoomInstance = this, AvatarInstance = UserInstance.Avatar });

                // Add this user & avatar.
                this._UsersInRoom.Add(UserInstance.Avatar, new object[] { UserInstance, UserConnection });
                this.AddAvatar(UserInstance.Avatar);

                // Log the user joining.
                if (UserInstance.UserData.IsLoggedIn && !UserInstance.UserData.IsGuest)
                {
                    if (this.RoomType == RoomType.PrivateRoom)
                    {
                        this._ServerManager.LoggingService.WriteMember(LogImportance.Normal, UserInstance.UserData.UserInfo.Id, "Joined private room: " + this.RoomId, null);
                    }
                    else
                    {
                        this._ServerManager.LoggingService.WriteMember(LogImportance.Normal, UserInstance.UserData.UserInfo.Id, "Joined public room: " + this.RoomId, null);
                    }
                    this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, this.RoomId, this.RoomType, "User joined: " + UserInstance.UserData.UserInfo.Id, null);
                }

                // Update views.
                if (this.RoomType == RoomType.PrivateRoom)
                {
                    IRoomsDatabaseTable Table = ((IPrivateRoom)this).DatabaseTable;
                    Table.Views++;
                    Table.Update().Execute();
                }
            }
        }

        /// <summary>
        /// Adds an avatar too this room.
        /// </summary>
        /// <param name="Avatar">The avatar too add.</param>
        public void AddAvatar(IAvatar Avatar)
        {
            // Check we don't already contain this avatar.
            if (!this._AvatarsInRoom.Contains(Avatar))
            {
                // Create the avatars height map.
                Avatar.Heightmap = new byte[this._HeightmapManager.NearestSquareRows, this._HeightmapManager.NearestSquareCols];

                // Create the avatars id as blank.
                Avatar.AvatarId = 1;

                // If there's more than 1 avatar in this room, find a new id.
                if (this._AvatarsInRoom.Count != 0)
                {
                    // Store the list of used ids.
                    List<int> UsedIds = new List<int>();

                    // Propagate our list.
                    for (int i = 0; i < this._AvatarsInRoom.Count; i++)
                    {
                        // Add too the in use list.
                        UsedIds.Add(this._AvatarsInRoom[i].AvatarId);
                    }

                    // Find a free id.
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        // If this number isn't used, use it.
                        if (!UsedIds.Contains(i))
                        {
                            // Set the id and finish.
                            Avatar.AvatarId = i;
                            break;
                        }
                    }
                }

                // Add the avatar.
                this._AvatarsInRoom.Add(Avatar);

                // Add the avatar too the tile.
                if (this._HeightmapManager.UnitMap[Avatar.LocationX, Avatar.LocationY] != null)
                {
                    this._HeightmapManager.UnitMap[Avatar.LocationX, Avatar.LocationY].Add(Avatar);
                }

                // Call the add avatar room event.
                if (this._UsersInRoom.ContainsKey(Avatar))
                {
                    this._ServerManager.EventsManager.CallEvent(EventType.AvatarAdded, this, new RoomEventArguments() { UserInstance = (IUser)this._UsersInRoom[Avatar][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[Avatar][1], RoomInstance = this, AvatarInstance = Avatar });
                }
                else
                {
                    this._ServerManager.EventsManager.CallEvent(EventType.AvatarAdded, this, new RoomEventArguments() { RoomInstance = this, AvatarInstance = Avatar });
                }
            }
        }

        /// <summary>
        /// Removes an avatar from this room.
        /// </summary>
        /// <param name="Avatar">The avatar too remove.</param>
        public void RemoveAvatar(IAvatar Avatar)
        {
            // Check we contain this avatar.
            if (this._AvatarsInRoom.Contains(Avatar))
            {
                // Remove the avatar.
                this._AvatarsInRoom.Remove(Avatar);

                // Call the removed avatar event.
                if (this._UsersInRoom.ContainsKey(Avatar))
                {
                    this._ServerManager.EventsManager.CallEvent(EventType.AvatarRemoved, this, new RoomEventArguments() { UserInstance = (IUser)this._UsersInRoom[Avatar][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[Avatar][1], RoomInstance = this, AvatarInstance = Avatar });
                }
                else
                {
                    this._ServerManager.EventsManager.CallEvent(EventType.AvatarRemoved, this, new RoomEventArguments() { RoomInstance = this, AvatarInstance = Avatar });
                }

                // Log the user leaving.
                if (((IUser)this._UsersInRoom[Avatar][0]).UserData.IsLoggedIn && !((IUser)this._UsersInRoom[Avatar][0]).UserData.IsGuest)
                {
                    if (this.RoomType == RoomType.PrivateRoom)
                    {
                        this._ServerManager.LoggingService.WriteMember(LogImportance.Normal, ((IUser)this._UsersInRoom[Avatar][0]).UserData.UserInfo.Id, "Left private room: " + this.RoomId, null);
                    }
                    else
                    {
                        this._ServerManager.LoggingService.WriteMember(LogImportance.Normal, ((IUser)this._UsersInRoom[Avatar][0]).UserData.UserInfo.Id, "Left public room: " + this.RoomId, null);
                    }
                    this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, this.RoomId, this.RoomType, "User left: " + ((IUser)this._UsersInRoom[Avatar][0]).UserData.UserInfo.Id, null);
                }
            }

            // Remove from users.
            if (this._UsersInRoom.ContainsKey(Avatar))
            {
                // Remove the avatar.
                this._UsersInRoom.Remove(Avatar);
            }

            // Remove from bots.
            if (this._BotsInRoom.ContainsKey(Avatar))
            {
                // Remove the bot.
                this._BotsInRoom.Remove(Avatar);
            }

            // Remove from unit map.
            bool FoundAvatar = false;
            for (int x = 0; x < this._HeightmapManager.Rows; x++)
            {
                for (int y = 0; y < this._HeightmapManager.Cols; y++)
                {
                    // If the users match, remove.
                    if (this._HeightmapManager.UnitMap[x, y].Contains(Avatar))
                    {
                        // Remove.
                        this._HeightmapManager.UnitMap[x, y].Remove(Avatar);
                        FoundAvatar = true;
                        break;
                    }
                }

                if (FoundAvatar)
                {
                    break;
                }
            }

            // Clear the avatars room.
            Avatar.CurrentRoom = null;

            // Update database.
            if (this.RoomType == RoomType.PrivateRoom)
            {
                ((IPrivateRoom)this).DatabaseTable.Users = this._UsersInRoom.Count;
            }

            // Finally check if we are empty.
            if (this._AvatarsInRoom.Count == 0)
            {
                // Destroy the room.
                this._RoomActive = false;
                this._ServerManager.RoomsManager.RemoveRoom(this, (RoomType)this.RoomType);

                // Write about us shutting down.
                if (this.RoomType == RoomType.PrivateRoom)
                {
                    this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, ((PrivateRoom)this).DatabaseTable.Id, this.RoomType, "Room shutdown, no more users in room.", null);
                }
                else if (this.RoomType == RoomType.PublicRoom)
                {
                    this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, ((PublicRoom)this).DatabaseTable.Id, this.RoomType, "Room shutdown, no more users in room.", null);
                }

                // Fire shutdown event.
                this._ServerManager.EventsManager.CallEvent(EventType.RoomShutdown, this, new RoomEventArguments() { GameSocketConnection = null, UserInstance = null, RoomInstance = this, AvatarInstance = null });

                // Destroy any circular dependencies.
                _FurniManager.Delete();
                _HeightmapManager.Delete();
            }
        }

        /// <summary>
        /// Kicks a user from this room.
        /// </summary>
        /// <param name="User">The user to kick.</param>
        public void KickUser(IUser User)
        {
            // Make sure we contain this user.
            if (this._UsersInRoom.ContainsKey(User.Avatar))
            {
                // Remove.
                RemoveAvatar(User.Avatar);

                // Fire kicked event.
                this._ServerManager.EventsManager.CallEvent(EventType.RoomKick, this, new RoomEventArguments() { RoomInstance = this, AvatarInstance = User.Avatar, UserInstance = User, GameSocketConnection = (IGameSocketConnection)User });
            }
        }

        /// <summary>
        /// Sends a chat message too the room.
        /// </summary>
        /// <param name="Avatar">The avatar sending the chat message.</param>
        /// <param name="ChatMessage">The chat message being sent.</param>
        public void SendChat(IAvatar Avatar, string ChatMessage)
        {
            // Call the send chat event.
            if (this._UsersInRoom.ContainsKey(Avatar))
            {
                this._ServerManager.EventsManager.CallEvent(EventType.AvatarTalked, this, new RoomChatEventArguments() { ChatMessage = ChatMessage, UserInstance = (IUser)this._UsersInRoom[Avatar][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[Avatar][1], RoomInstance = this, AvatarInstance = Avatar });
            }
            else
            {
                this._ServerManager.EventsManager.CallEvent(EventType.AvatarTalked, this, new RoomChatEventArguments() { ChatMessage = ChatMessage, RoomInstance = this, AvatarInstance = Avatar });
            }

            // Un-brb a user.
            if (this._UsersInRoom.ContainsKey(Avatar))
            {
                // If the user is away.
                if (((IUser)this._UsersInRoom[Avatar][0]).UserData.ActivatedCommands.Brb)
                {
                    // Call toggle brb event.
                    this._ServerManager.EventsManager.CallEvent(EventType.CommandToggleBrb, this, new GameSocketsEventArguments() { UserInstance = (IUser)this._UsersInRoom[Avatar][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[Avatar][0] });
                }
            }

            // Log the chat.
            if (((IUser)this._UsersInRoom[Avatar][0]) != null)
            {
                this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, this.RoomId, this.RoomType, "(" + ((IUser)this._UsersInRoom[Avatar][0]).UserData.UserInfo.Id + ") " + Avatar.Username + ": " + ChatMessage, null);
            }
            else
            {
                this._ServerManager.LoggingService.WriteRoom(LogImportance.Normal, this.RoomId, this.RoomType, Avatar.Username + ": " + ChatMessage, null);
            }
        }

        /// <summary>
        /// Sends all the users in the room a piece of data.
        /// </summary>
        /// <param name="Data">The data too send</param>
        public void SendData(string Data)
        {
            // Firstly copy our directory.
            KeyValuePair<IAvatar, object[]>[] AvatarArray = this._UsersInRoom.ToArray();

            // Send data to every user in the room.
            for (int i = 0; i < AvatarArray.Length; i++)
            {
                // Get the values.
                IUser UserInstance = (IUser)AvatarArray[i].Value[0];
                IGameSocketConnection UserConnection = (IGameSocketConnection)AvatarArray[i].Value[1];

                // Send the data.
                UserConnection.SendData(Data, true);
            }
        }

        /// <summary>
        /// Sends all the users in the room, except the socket not too send to, the piece of data.
        /// </summary>
        /// <param name="Data">The data to send.</param>
        /// <param name="NotToSendToo">Who not to send the data too.</param>
        public void SendData(string Data, IGameSocketConnection NotToSendToo)
        {
            // Firstly copy our directory.
            KeyValuePair<IAvatar, object[]>[] AvatarArray = this._UsersInRoom.ToArray();

            // Send data to every user in the room apart from the specified user..
            for (int i = 0; i < AvatarArray.Length; i++)
            {
                // Get the values.
                IUser UserInstance = (IUser)AvatarArray[i].Value[0];
                IGameSocketConnection UserConnection = (IGameSocketConnection)AvatarArray[i].Value[1];

                // Make sure this is not the specified user.
                if (UserConnection != NotToSendToo)
                {
                    // Send the data.
                    UserConnection.SendData(Data, true);
                }
            }
        }

        /// <summary>
        /// Handles this rooms thread.
        /// </summary>
        private void RoomThread(object Instance)
        {
            // Get the thread instance.
            ThreadInstance ThreadInstance = (ThreadInstance)Instance;

            // Store the pathfinder.
            PathFinderFast Pathfinder = new PathFinderFast();

            // Loop while active.
            while (this._RoomActive && !ThreadInstance.Exiting)
            {
                // Loop through each avatar, handling it.
                for (int i = 0; i < this._AvatarsInRoom.Count; i++)
                {
                    // Get the avatar.
                    IAvatar AvatarInstance = this._AvatarsInRoom[i];

                    // We cannot afford to break here - try/catch to make sure.
                    try
                    {
                        // If the avatars target isn't there current position, start walking!
                        if (AvatarInstance.TargetX != AvatarInstance.LocationX || AvatarInstance.TargetY != AvatarInstance.LocationY)
                        {
                            // Walk dependent on movement method.
                            switch (AvatarInstance.MovementMethod)
                            {
                                default:
                                case MovementMethod.Walk:
                                    {
                                        // Standard walking.
                                        // Create our pathfinder.
                                        Pathfinder.SetGrid(this._HeightmapManager.GenerateGrid(AvatarInstance));
                                        List<PathFinderNode> Path = Pathfinder.FindPath(new Point(AvatarInstance.LocationX, AvatarInstance.LocationY), new Point(AvatarInstance.TargetX, AvatarInstance.TargetY));

                                        // Check that we have a path.
                                        if (Path == null || Path.Count == 0)
                                        {
                                            // No path found...
                                            // Un-brb a user.
                                            if (this._UsersInRoom.ContainsKey(AvatarInstance))
                                            {
                                                // If the user is away.
                                                if (((IUser)this._UsersInRoom[AvatarInstance][0]).UserData.ActivatedCommands.Brb)
                                                {
                                                    // Call toggle brb event.
                                                    this._ServerManager.EventsManager.CallEvent(EventType.CommandToggleBrb, this, new GameSocketsEventArguments() { UserInstance = (IUser)this._UsersInRoom[AvatarInstance][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[AvatarInstance][0] });
                                                }
                                            }

                                            // Reset the users target position.
                                            AvatarInstance.TargetX = AvatarInstance.LocationX;
                                            AvatarInstance.TargetY = AvatarInstance.LocationY;

                                            // Reset the avatars walk status.
                                            if (this._HeightmapManager.StateMap[AvatarInstance.LocationX, AvatarInstance.LocationY] == TileState.Seat)
                                            {
                                                // Get sit data.
                                                AvatarInstance.LocationSH = this._FurniManager.GetSeatAt(AvatarInstance.LocationX, AvatarInstance.LocationY).ItemData.Height;
                                                AvatarInstance.Status = "sit";
                                            }
                                            else
                                            {
                                                // Reset to idle.
                                                AvatarInstance.Status = "idle";
                                            }

                                            // Set the walk status to ended.
                                            AvatarInstance.WalkStatus = "walk_end";
                                        }
                                        else
                                        {
                                            // A path was found!
                                            // Get the new position.
                                            PathFinderNode NextPosition = Path[Path.Count - 2];

                                            // Un-brb a user.
                                            if (this._UsersInRoom.ContainsKey(AvatarInstance))
                                            {
                                                // If the user is away.
                                                if (((IUser)this._UsersInRoom[AvatarInstance][0]).UserData.ActivatedCommands.Brb)
                                                {
                                                    // Call toggle brb event.
                                                    this._ServerManager.EventsManager.CallEvent(EventType.CommandToggleBrb, this, new GameSocketsEventArguments() { UserInstance = (IUser)this._UsersInRoom[AvatarInstance][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[AvatarInstance][0] });
                                                }
                                            }

                                            // Switch over the unit mapping.
                                            this._HeightmapManager.UnitMap[AvatarInstance.LocationX, AvatarInstance.LocationY].Remove(AvatarInstance);
                                            this._HeightmapManager.UnitMap[NextPosition.X, NextPosition.Y].Add(AvatarInstance);

                                            // Store the old status for later.
                                            string OldStatus = AvatarInstance.Status;

                                            // Calculate the avatars rotation.
                                            AvatarInstance.LocationH = RotationalCalculator.CalculateRotation(NextPosition.X, NextPosition.Y, AvatarInstance.LocationX, AvatarInstance.LocationY);

                                            // Set avatars walk status.
                                            if (this._HeightmapManager.StateMap[NextPosition.X, NextPosition.Y] == TileState.Seat)
                                            {
                                                // Get sit data.
                                                AvatarInstance.LocationSH = this._FurniManager.GetSeatAt(NextPosition.X, NextPosition.Y).ItemData.Height;
                                                AvatarInstance.Status = "sit";
                                            }
                                            else if (NextPosition.X == AvatarInstance.TargetX && NextPosition.Y == AvatarInstance.TargetY)
                                            {
                                                // We have finished walking, set to idle.
                                                AvatarInstance.Status = "idle";
                                            }
                                            else
                                            {
                                                // We are still walking, set to walk.
                                                AvatarInstance.Status = "walk";
                                            }

                                            // Set the avatars position to the next coordinate.
                                            AvatarInstance.LocationX = NextPosition.X;
                                            AvatarInstance.LocationY = NextPosition.Y;

                                            // Find out the avatars walk status.
                                            if (OldStatus == "idle" || OldStatus == "sit")
                                            {
                                                // Start of walking.
                                                AvatarInstance.WalkStatus = "walk_new";
                                            }
                                            else if (NextPosition.X == AvatarInstance.TargetX && NextPosition.Y == AvatarInstance.TargetY)
                                            {
                                                // End of walking.
                                                AvatarInstance.WalkStatus = "walk_end";
                                            }
                                            else
                                            {
                                                // Middle of walking.
                                                AvatarInstance.WalkStatus = "walk_mid";
                                            }
                                        }

                                        // Call the avatar walked event.
                                        if (this._UsersInRoom.ContainsKey(AvatarInstance))
                                        {
                                            this._ServerManager.EventsManager.CallEvent(EventType.AvatarWalk, this, new RoomEventArguments() { UserInstance = (IUser)this._UsersInRoom[AvatarInstance][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[AvatarInstance][1], RoomInstance = this, AvatarInstance = AvatarInstance });
                                        }
                                        else
                                        {
                                            this._ServerManager.EventsManager.CallEvent(EventType.AvatarWalk, this, new RoomEventArguments() { RoomInstance = this, AvatarInstance = AvatarInstance });
                                        }

                                        // Finish.
                                        break;
                                    }

                                case MovementMethod.NoCheckWarp:
                                    {
                                        // Admin-Style warp.
                                        PathFinderNode NextPosition = new PathFinderNode() { X = AvatarInstance.TargetX, Y = AvatarInstance.TargetY };

                                        // Un-brb a user.
                                        if (this._UsersInRoom.ContainsKey(AvatarInstance))
                                        {
                                            // If the user is away.
                                            if (((IUser)this._UsersInRoom[AvatarInstance][0]).UserData.ActivatedCommands.Brb)
                                            {
                                                // Call toggle brb event.
                                                this._ServerManager.EventsManager.CallEvent(EventType.CommandToggleBrb, this, new GameSocketsEventArguments() { UserInstance = (IUser)this._UsersInRoom[AvatarInstance][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[AvatarInstance][0] });
                                            }
                                        }

                                        // Switch over the unit mapping.
                                        this._HeightmapManager.UnitMap[AvatarInstance.LocationX, AvatarInstance.LocationY].Remove(AvatarInstance);
                                        this._HeightmapManager.UnitMap[NextPosition.X, NextPosition.Y].Add(AvatarInstance);

                                        // Calculate the avatars rotation.
                                        AvatarInstance.LocationH = RotationalCalculator.CalculateRotation(NextPosition.X, NextPosition.Y, AvatarInstance.LocationX, AvatarInstance.LocationY);

                                        // Set the status to either sit or warp, depending on next tile.
                                        if (this._HeightmapManager.StateMap[NextPosition.X, NextPosition.Y] == TileState.Seat)
                                        {
                                            AvatarInstance.LocationSH = this._FurniManager.GetSeatAt(NextPosition.X, NextPosition.Y).ItemData.Height;
                                            AvatarInstance.Status = "sit";
                                        }
                                        else
                                        {
                                            AvatarInstance.Status = "warp";
                                        }

                                        // Set the avatars position to the next coordinate.
                                        AvatarInstance.LocationX = NextPosition.X;
                                        AvatarInstance.LocationY = NextPosition.Y;

                                        // Call the avatar walked event.
                                        if (this._UsersInRoom.ContainsKey(AvatarInstance))
                                        {
                                            this._ServerManager.EventsManager.CallEvent(EventType.AvatarWalk, this, new RoomEventArguments() { UserInstance = (IUser)this._UsersInRoom[AvatarInstance][0], GameSocketConnection = (IGameSocketConnection)this._UsersInRoom[AvatarInstance][1], RoomInstance = this, AvatarInstance = AvatarInstance });
                                        }
                                        else
                                        {
                                            this._ServerManager.EventsManager.CallEvent(EventType.AvatarWalk, this, new RoomEventArguments() { RoomInstance = this, AvatarInstance = AvatarInstance });
                                        }

                                        // Finish.
                                        break;
                                    }
                            }
                        }
                    }
                    catch
                    {
                        // Reset the avatars walk target - this is probably what is causing the error.
                        AvatarInstance.TargetX = AvatarInstance.LocationX;
                        AvatarInstance.TargetY = AvatarInstance.LocationY;
                    }
                }

                // Sleep.
                Thread.Sleep(430);
            }

            this._ServerManager.ThreadService.StopThread(ThreadInstance);
        }
    }
}
