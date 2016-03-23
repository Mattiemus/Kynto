using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

namespace KyntoServer.Services
{
    public class ThreadService : IThreadService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the list of active threads.
        /// </summary>
        private List<ThreadInstance> _ActiveThreads = new List<ThreadInstance>();

        /// <summary>
        /// Initialises the threading service.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server.
            this._ServerManager = ServerInstance;

            // Not very much we can do to initialise... oh well, lets just say it was successful...
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Threads service initialised.", null);
            this._ServerManager.LoggingService.InitialiseThread();
        }

        /// <summary>
        /// Exits the thread service, stopping all threads.
        /// </summary>
        public void Exit()
        {
            // Write success.
            this._ServerManager.LoggingService.Write(LogImportance.Normal, "Thread service shutdown, closing \"" + this._ActiveThreads.Count + "\" threads.", null);

            // Lets exit all of our threads.
            for (int i = 0; i < this._ActiveThreads.Count; i++)
            {
                this._ActiveThreads[i].Stop();
            }

            // Clear all threads.
            this._ActiveThreads.Clear();
        }

        /// <summary>
        /// Creates a new thread.
        /// </summary>
        /// <param name="ThreadMethod">The method for the new thread to run.</param>
        /// <returns>The thread instance.</returns>
        public ThreadInstance CreateThread(ParameterizedThreadStart ThreadMethod)
        {
            // Create the avatars id as blank.
            int ThreadId = 1;

            // If threes more than 1 thread, create a new thread id.
            if (this._ActiveThreads.Count != 0)
            {
                // Store the list of used ids.
                List<int> UsedIds = new List<int>();

                // Propagate our list.
                for (int i = 0; i < this._ActiveThreads.Count; i++)
                {
                    // Add too the in use list.
                    UsedIds.Add(this._ActiveThreads[i].ThreadId);
                }

                // Find a free id.
                for (int i = 1; i < int.MaxValue; i++)
                {
                    // If this number isn't used, use it.
                    if (!UsedIds.Contains(i))
                    {
                        // Set the id and finish.
                        ThreadId = i;
                        break;
                    }
                }
            }

            // Create our thread instance.
            ThreadInstance ThreadInstance = new ThreadInstance(this._ServerManager, new Thread(ThreadMethod), ThreadId);
            ThreadInstance.Thread.Start(ThreadInstance);

            // Write to log.
            this._ServerManager.LoggingService.Write(LogImportance.Normal, "Created thread id \"" + ThreadId + "\".", null);

            // Return the thread instance we created.
            return ThreadInstance;
        }

        /// <summary>
        /// Stops a thread.
        /// </summary>
        /// <param name="Thread">The thread to stop.</param>
        public void StopThread(ThreadInstance Thread)
        {
            // Close this thread.
            Thread.Stop();

            // Destroy the thread.
            if (this._ActiveThreads.Contains(Thread))
            {
                this._ActiveThreads.Remove(Thread);
            }

            // Write to log.
            this._ServerManager.LoggingService.Write(LogImportance.Normal, "Destroyed thread id \"" + Thread.ThreadId + "\".", null);
        }
    }
}
