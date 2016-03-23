using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Instances.User;
using KyntoLib.Interfaces.Instances.User.Data;

namespace KyntoLib.Interfaces.Managers
{
    /// <summary>
    /// Manages all users.
    /// </summary>
    public interface IUsersManager
    {
        /// <summary>
        /// Gets the list of users currently logged in.
        /// </summary>
        List<IUser> Users { get; }

        /// <summary>
        /// Gets the list of all rank's rights.
        /// </summary>
        Dictionary<int, IRightsData> Rights { get; }

        /// <summary>
        /// Initialises this users manager.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Initialises all the ranks.
        /// </summary>
        void InitialiseRanks();

        /// <summary>
        /// Exits the users manager, disconnecting all users.
        /// </summary>
        void Exit();

        /// <summary>
        /// Adds a user to this user manager.
        /// </summary>
        /// <param name="UserInstance">The user too add.</param>
        void AddUser(IUser UserInstance);

        /// <summary>
        /// Removes a user from this user manager.
        /// </summary>
        /// <param name="UserInstance">The user too remove.</param>
        void RemoveUser(IUser UserInstance);

        /// <summary>
        /// Gets a user based on id.
        /// </summary>
        /// <param name="Id">The id of the user.</param>
        /// <returns>The user with the specified id.</returns>
        IUser GetUserById(int Id);

        /// <summary>
        /// Gets a user based on username.
        /// </summary>
        /// <param name="Username">The username to search for.</param>
        /// <returns>The user with the specified username.</returns>
        IUser GetUserByUsername(string Username);
    }
}
