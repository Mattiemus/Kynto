using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Instances.Connections;
using KyntoLib.Interfaces.Instances.User.Data;

using KyntoServer.Instances.User.Data;

namespace KyntoServer.Managers
{
    /// <summary>
    /// Manages all users.
    /// </summary>
    public class UsersManager : IUsersManager
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the list of users.
        /// </summary>
        private List<IUser> _Users = new List<IUser>();

        /// <summary>
        /// Gets the list of users currently logged in.
        /// </summary>
        public List<IUser> Users
        {
            get
            {
                return this._Users;
            }
        }

        /// <summary>
        /// Stores the list of all rank's rights.
        /// </summary>
        private Dictionary<int, IRightsData> _Rights = new Dictionary<int,IRightsData>();

        /// <summary>
        /// Gets the list of all rank's rights.
        /// </summary>
        public Dictionary<int, IRightsData> Rights
        {
            get
            {
                return this._Rights;
            }
        }

        /// <summary>
        /// Initialises this users manager.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;

            // Write that we have initialised.
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Users manager initialised.", null);
            
            // Initialise users manager stuff.
            InitialiseRanks();
        }

        /// <summary>
        /// Initialises all the ranks.
        /// </summary>
        public void InitialiseRanks()
        {
            // Firstly clear the ranks.
            this._Rights.Clear();

            // Load all the rights of this rank.
            List<IDatabaseTable> Rights = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Ranks)
                .Select()
                .ExecuteRead();

            // Make sure we got some ranks.
            if (Rights == null || Rights.Count == 0)
            {
                // Write that we have failed to initialised.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "  Failed to gather rank rights.", null);
                this._ServerManager.Exit(null, "Failed to load ranks.");
            }
            else
            {
                // Process each rank.
                for (int i = 0; i < Rights.Count; i++)
                {
                    // Add to the list.
                    IRanksDatabaseTable Rank = (IRanksDatabaseTable)Rights[i];
                    this._Rights.Add(Rank.Id, new RightsData(Rank));
                }

                // Write that we loaded ranks.
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded " + Rights.Count + " ranks.", null);
            }
        }

        /// <summary>
        /// Exits the users manager, disconnecting all users.
        /// </summary>
        public void Exit()
        {
            // Delete all user connections.
            for (int i = 0; i < this._Users.Count; i++)
            {
                ((IGameSocketConnection)this._Users[i]).Disconnect();
            }

            // Kill all users.
            this._Users.Clear();

            // Write that we have initialised.
            this._ServerManager.LoggingService.Write(LogImportance.Normal, "Users manager unloaded all users.", null);
        }

        /// <summary>
        /// Adds a user to this user manager.
        /// </summary>
        /// <param name="UserInstance">The user too add.</param>
        public void AddUser(IUser UserInstance)
        {
            // This user is already logged in - remove the old instance then add.
            for (int i = 0; i < this._Users.Count; i++)
            {
                // Check if this is the user.
                if (this._Users[i].UserData.UserInfo.Id == UserInstance.UserData.UserInfo.Id)
                {
                    // Kick this user.
                    ((IGameSocketConnection)this._Users[i]).Disconnect();
                }
            }

            // Add this user.
            this._Users.Add(UserInstance);
        }

        /// <summary>
        /// Removes a user from this user manager.
        /// </summary>
        /// <param name="UserInstance">The user too remove.</param>
        public void RemoveUser(IUser UserInstance)
        {
            // Remove this user if it is contained within this users manager.
            if (this._Users.Contains(UserInstance))
            {
                // Remove the user.
                this._Users.Remove(UserInstance);
            }
        }

        /// <summary>
        /// Gets a user based on id.
        /// </summary>
        /// <param name="Id">The id of the user.</param>
        /// <returns>The user with the specified id.</returns>
        public IUser GetUserById(int Id)
        {
            for (int i = 0; i < this._Users.Count; i++)
            {
                if (this._Users[i].UserData != null && this._Users[i].UserData.UserInfo.Id == Id)
                {
                    return this._Users[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a user based on username.
        /// </summary>
        /// <param name="Username">The username to search for.</param>
        /// <returns>The user with the specified username.</returns>
        public IUser GetUserByUsername(string Username)
        {
            for (int i = 0; i < this._Users.Count; i++)
            {
                if (this._Users[i].UserData != null && this._Users[i].UserData.UserInfo.Username == Username)
                {
                    return this._Users[i];
                }
            }

            return null;
        }
    }
}
