using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Managers;

using KyntoServer.Managers;

namespace KyntoServer
{
    /// <summary>
    /// The main entrypoint of the server.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Stores the base directory of the server.
        /// </summary>
        private static string _BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Gets the base directory of the server.
        /// </summary>
        public static string BaseDirectory
        {
            get
            {
                return _BaseDirectory;
            }
            set
            {
                _BaseDirectory = value;
            }
        }

        /// <summary>
        /// Stores the launch time of the server.
        /// </summary>
        private static DateTime _LaunchTime;

        /// <summary>
        /// Gets the time at which the server was launched.
        /// </summary>
        public static DateTime LaunchTime
        {
            get
            {
                return _LaunchTime;
            }
        }

        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private static IServerManager _ServerManager;

        /// <summary>
        /// Gets the server manager.
        /// </summary>
        public static IServerManager ServerManager
        {
            get
            {
                return _ServerManager;
            }
        }

        /// <summary>
        /// The entry point of the server.
        /// </summary>
        /// <param name="Arguments">The arguments passed to the server.</param>
        public static void Main(string[] Arguments)
        {
            // Store server values.
            _LaunchTime = DateTime.Now;

            // Initialise our server.
            _ServerManager = new ServerManager();
            _ServerManager.Initialise(Arguments);

            // Commands loop.
            while (true)
            {
                // Read command.
                string Command = Console.ReadLine();

                // Make sure command is correct.
                // Split command and parameters.
                string[] CommandSegments = Command.Split(' ');

                // Find command meaning.
                switch (CommandSegments[0])
                {
                    case "exit":
                        {
                            _ServerManager.Exit(null, "Exited via console.");
                            break;
                        }

                    case "bandwidth":
                        {
                            Console.WriteLine("Uploaded: " + _ServerManager.GameSocketsService.UploadedBytes + "bytes");
                            Console.WriteLine("Downloaded: " + _ServerManager.GameSocketsService.DownloadedBytes + "bytes");
                            break;
                        }

                    case "reload":
                        {
                            // Make sure we have something to reload.
                            if (CommandSegments.Length == 1)
                            {
                                Console.WriteLine("The \"reload\" command requires something to reload (reload [ranks/clothes/furni/catalogue]).");
                            }
                            else
                            {
                                switch (CommandSegments[1])
                                {
                                    case "ranks":
                                        _ServerManager.LoggingService.Write(KyntoLib.Interfaces.Services.LogImportance.Normal, "Reloading \"" + CommandSegments[1] + "\".", null);
                                        _ServerManager.UsersManager.InitialiseRanks();
                                        break;

                                    case "clothes":
                                        _ServerManager.LoggingService.Write(KyntoLib.Interfaces.Services.LogImportance.Normal, "Reloading \"" + CommandSegments[1] + "\".", null);
                                        _ServerManager.CatalogueManager.InitialiseClothes();
                                        break;

                                    case "furni":
                                        _ServerManager.LoggingService.Write(KyntoLib.Interfaces.Services.LogImportance.Normal, "Reloading \"" + CommandSegments[1] + "\".", null);
                                        _ServerManager.CatalogueManager.InitialiseFurni();
                                        break;

                                    case "catalogue":
                                        _ServerManager.LoggingService.Write(KyntoLib.Interfaces.Services.LogImportance.Normal, "Reloading \"" + CommandSegments[1] + "\".", null);
                                        _ServerManager.CatalogueManager.InitialiseCataloguePages();
                                        _ServerManager.CatalogueManager.InitialiseCatalogueItems();
                                        break;

                                    default:
                                        Console.WriteLine("Failed to reload \"" + CommandSegments[1] + "\".");
                                        break;
                                }
                            }

                            break;
                        }

                    default:
                        {
                            Console.WriteLine("\"" + CommandSegments[0] + "\" is an unknown command.");
                            break;
                        }
                }
            }
        }
    }
}
