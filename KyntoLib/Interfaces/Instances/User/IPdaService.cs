using System;
using System.Collections.Generic;

namespace KyntoLib.Interfaces.Instances.User
{
    /// <summary>
    /// Interfaces with a Pda service.
    /// </summary>
    public interface IPdaService
    {
        /// <summary>
        /// Gets the list of offline friends.
        /// </summary>
        List<int> OfflineFriends { get; }

        /// <summary>
        /// Gets the list of online friends.
        /// </summary>
        List<IUser> OnlineFriends { get; }

        /// <summary>
        /// Gets the list of active friend requests.
        /// </summary>
        List<int> FriendRequests { get; }

        /// <summary>
        /// Called when the owner of this pda service logs in.
        /// </summary>
        void Initialise();

        /// <summary>
        /// Shuts down this pda service.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Called when a user logs in.
        /// </summary>
        /// <param name="User">The user who logged in.</param>
        void FriendLoggedIn(IUser User);

        /// <summary>
        /// Called when a user logs off.
        /// </summary>
        /// <param name="User">The user who logged off.</param>
        void FriendLoggedOff(IUser User);
    }
}
