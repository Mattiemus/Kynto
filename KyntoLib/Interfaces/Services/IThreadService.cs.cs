using System;
using System.Threading;

using KyntoLib.Interfaces.Managers;

namespace KyntoLib.Interfaces.Services
{
    /// <summary>
    /// Handles all of the threads on the server.
    /// </summary>
    public interface IThreadService
    {
        /// <summary>
        /// Initialises the threading service.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Exits the thread service, stopping all threads.
        /// </summary>
        void Exit();

        /// <summary>
        /// Creates a new thread.
        /// </summary>
        /// <param name="ThreadMethod">The method for the new thread to run.</param>
        /// <returns>The thread instance.</returns>
        ThreadInstance CreateThread(ParameterizedThreadStart ThreadMethod);

        /// <summary>
        /// Stops a thread.
        /// </summary>
        /// <param name="Thread">The thread to stop.</param>
        void StopThread(ThreadInstance Thread);
    }

    /// <summary>
    /// Represents a single thread instance.
    /// </summary>
    public class ThreadInstance
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Gets the server manager.
        /// </summary>
        public IServerManager ServerManager
        {
            get
            {
                return this._ServerManager;
            }
        }

        /// <summary>
        /// Stores the thread id.
        /// </summary>
        private int _ThreadId;

        /// <summary>
        /// Gets this threads id.
        /// </summary>
        public int ThreadId
        {
            get
            {
                return this._ThreadId;
            }
        }

        /// <summary>
        /// Stores the thread instance.
        /// </summary>
        private Thread _Thread;

        /// <summary>
        /// Gets the thread.
        /// </summary>
        public Thread Thread
        {
            get
            {
                return this._Thread;
            }
        }

        /// <summary>
        /// Stores weather or not this thread should be exiting.
        /// </summary>
        private bool _Exiting = false;

        /// <summary>
        /// Gets or sets weather or not this thread should be exiting.
        /// </summary>
        public bool Exiting
        {
            get
            {
                return this._Exiting;
            }
            set
            {
                this._Exiting = value;
            }
        }

        /// <summary>
        /// Initialises this thread instance.
        /// </summary>
        /// <param name="ServerManager">The server manager instance.</param>
        /// <param name="Thread">The thread this instance contains.</param>
        /// <param name="ThreadId">The thread id.</param>
        public ThreadInstance(IServerManager ServerManager, Thread Thread, int ThreadId)
        {
            // Store.
            this._ServerManager = ServerManager;
            this._Thread = Thread;
            this._ThreadId = ThreadId;
        }

        /// <summary>
        /// Stops the thread.
        /// </summary>
        public void Stop()
        {
            // Stop this thread.
            this._Exiting = true;
        }
    }
}
