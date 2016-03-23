using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Events;

namespace KyntoServer.Instances.User
{
    /// <summary>
    /// Handles the pda.
    /// </summary>
    public class PdaService : IPdaService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the user instance parent.
        /// </summary>
        private IUser _UserInstance;

        /// <summary>
        /// Stores the parent users connection.
        /// </summary>
        private IGameSocketConnection _UserConnection;

        /// <summary>
        /// Stores the list of offline friends.
        /// </summary>
        private List<int> _OfflineFriends = new List<int>();

        /// <summary>
        /// Gets the list of offline friends.
        /// </summary>
        public List<int> OfflineFriends
        {
            get
            {
                return this._OfflineFriends;
            }
        }

        /// <summary>
        /// Stores the list of online friends.
        /// </summary>
        private List<IUser> _OnlineFriends = new List<IUser>();

        /// <summary>
        /// Gets the list of online friends.
        /// </summary>
        public List<IUser> OnlineFriends
        {
            get
            {
                return this._OnlineFriends;
            }
        }

        /// <summary>
        /// Stores the list of friend requests.
        /// </summary>
        private List<int> _FriendRequests = new List<int>();

        /// <summary>
        /// Gets the list of active friend requests.
        /// </summary>
        public List<int> FriendRequests
        {
            get
            {
                return this._FriendRequests;
            }
        }

        /// <summary>
        /// Initialises this pda service.
        /// </summary>
        /// <param name="ServerManager">The server manager isntance.</param>
        /// <param name="ParentUser">The parent user.</param>
        /// <param name="ParentSocket">The parent socket.</param>
        public PdaService(IServerManager ServerManager, IUser ParentUser, IGameSocketConnection ParentSocket)
        {
            // Store these values.
            this._ServerManager = ServerManager;
            this._UserInstance = ParentUser;
            this._UserConnection = ParentSocket;
        }

        /// <summary>
        /// Called when the owner of this pda service logs in.
        /// </summary>
        public void Initialise()
        {
            // Firstly get the users friends.
            List<IDatabaseTable> OwnedFriends = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.MembersFriends)
                .Select()
                .Where(MembersFriendsTableFields.MemberId, DatabaseComparison.Equals, this._UserInstance.UserData.UserInfo.Id)
                .ExecuteRead();

            // Make sure we got our friends.
            if (OwnedFriends != null)
            {
                // Add the friends to our current arrays.
                for (int i = 0; i < OwnedFriends.Count; i++)
                {
                    // Get the fields as the correct type.
                    IMembersFriendsDatabaseTable FriendsEntry = ((IMembersFriendsDatabaseTable)OwnedFriends[i]);

                    // Find out if the friendship is active.
                    if (FriendsEntry.Active)
                    {
                        // Friendship is active - find if user is online.
                        IUser UserInstance = this._ServerManager.UsersManager.GetUserById(FriendsEntry.FriendId);

                        // If we found the user add it to online list.
                        if (UserInstance != null)
                        {
                            this._OnlineFriends.Add(UserInstance);
                        }
                        else
                        {
                            // User is offline - only add id.
                            this._OfflineFriends.Add(FriendsEntry.FriendId);
                        }
                    }
                    else
                    {
                        // Friendship is inactive - add it too requests.
                        this._FriendRequests.Add(FriendsEntry.FriendId);
                    }
                }
            }

            // Call pda loaded event.
            // TODO: re-add PDA events.
            //// Create params.
            //Dictionary<string, object> Params = new Dictionary<string, object>();
            //Params.Add("GameSocketConnection", this._UserConnection);
            //Params.Add("OnlineFriends", this._OnlineFriends);
            //Params.Add("OfflineFriends", this._OfflineFriends);
            //Params.Add("FriendRequests", this._FriendRequests);
            //// Fire event.
            //this._ServerManager.EventsManager.CallEvent(EventType.PDA.Loaded, Params);

            // Tell all our friends that we logged in.
            for (int i = 0; i < this._OnlineFriends.Count; i++)
            {
                this._OnlineFriends[i].PdaService.FriendLoggedIn(this._UserInstance);
            }
        }

        /// <summary>
        /// Shuts down this pda service.
        /// </summary>
        public void Shutdown()
        {
            // Tell all our friends that we logged out.
            for (int i = 0; i < this._OnlineFriends.Count; i++)
            {
                this._OnlineFriends[i].PdaService.FriendLoggedOff(this._UserInstance);
            }

            // Forget who owns this so we will be garbage collected.
            this._UserInstance = null;
            this._UserConnection = null;
        }

        /// <summary>
        /// Called when a user logs in.
        /// </summary>
        /// <param name="User">The user who logged in.</param>
        public void FriendLoggedIn(IUser User)
        {
            // Firstly, make sure we know this user! - just to double check.
            if (!this._OfflineFriends.Contains(User.UserData.UserInfo.Id))
            {
                this._OfflineFriends.Add(User.UserData.UserInfo.Id);
            }

            // A friend logged in - remove from the offline users list.
            this._OfflineFriends.Remove(User.UserData.UserInfo.Id);
            // Add to the online friends list.
            this._OnlineFriends.Add(User);

            // Fire pda friend logged in event.
            // TODO: re-add PDA events.
            //// Create params.
            //Dictionary<string, object> Params = new Dictionary<string, object>();
            //Params.Add("GameSocketConnection", this._UserConnection);
            //Params.Add("User", this._UserInstance);
            //Params.Add("Friend", User);
            //// Fire event.
            //this._ServerManager.EventsManager.CallEvent(EventType.PDA.FriendLoggedIn, Params);
        }

        /// <summary>
        /// Called when a user logs off.
        /// </summary>
        /// <param name="User">The user who logged off.</param>
        public void FriendLoggedOff(IUser User)
        {
            // Firstly make sure this user is in our online friends list.
            if (!this._OnlineFriends.Contains(User))
            {
                // Add friend to online friends.
                this._OnlineFriends.Add(User);
            }

            // Remove the user from our online friends.
            this._OnlineFriends.Remove(User);
            // Add to our offline friends.
            this._OfflineFriends.Add(User.UserData.UserInfo.Id);

            // Fire pda friend logged out event.
            // TODO: re-add PDA events.
            //// Create params.
            //Dictionary<string, object> Params = new Dictionary<string, object>();
            //Params.Add("GameSocketConnection", this._UserConnection);
            //Params.Add("User", this._UserInstance);
            //Params.Add("Friend", User);
            //// Fire event.
            //this._ServerManager.EventsManager.CallEvent(EventType.PDA.FriendLoggedOut, Params);
        }
    }
}
