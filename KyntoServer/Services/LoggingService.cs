using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Rooms;
using KyntoLib.Helpers;

namespace KyntoServer.Services
{
    /// <summary>
    /// Handles logging of debug data and other information.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the server log file stream.
        /// </summary>
        private string _ServerLogFile;

        /// <summary>
        /// Stores the database logs folder.
        /// </summary>
        private string _DatabaseLogFile;

        /// <summary>
        /// Stores the members logs folder.
        /// </summary>
        private string _MembersLogsFolder;

        /// <summary>
        /// Stores the publics logs folder.
        /// </summary>
        private string _PublicRoomsLogsFolder;

        /// <summary>
        /// Stores the privates logs folder.
        /// </summary>
        private string _PrivateRoomsLogsFolder;

        /// <summary>
        /// Stores the queue of data that should be written to the log.
        /// </summary>
        private Queue<object[]> _ServerLogWriteQueue = new Queue<object[]>();

        /// <summary>
        /// Stores the queue of data that should be written to the database log.
        /// </summary>
        private Queue<object[]> _DatabaseLogWriteQueue = new Queue<object[]>();

        /// <summary>
        /// Stores the queue of data that should be written to the members log.
        /// </summary>
        private Queue<object[]> _MembersLogWriteQueue = new Queue<object[]>();

        /// <summary>
        /// Stores the queue of data that should be written to the rooms log.
        /// </summary>
        private Queue<object[]> _RoomsLogWriteQueue = new Queue<object[]>();

        /// <summary>
        /// Initialises the logging service.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server.
            this._ServerManager = ServerInstance;

            // Store the log file name.
            this._ServerLogFile = Program.BaseDirectory + "Logs/Server/" + Program.LaunchTime.ToString("MM-dd-yyyy_hh-mm-sstt") + ".log";
            this._DatabaseLogFile = Program.BaseDirectory + "Logs/Database/" + Program.LaunchTime.ToString("MM-dd-yyyy_hh-mm-sstt") + ".log";
            this._MembersLogsFolder = Program.BaseDirectory + "Logs/Members/";
            this._PrivateRoomsLogsFolder = Program.BaseDirectory + "Logs/Rooms/Private/";
            this._PublicRoomsLogsFolder = Program.BaseDirectory + "Logs/Rooms/Public/";

            // Write a pretty header for our logs!
            Console.ForegroundColor = ConsoleColor.White;
            this.Write(LogImportance.Server, "################################################################", null);
            this.Write(LogImportance.Server, "#                                                              #", null);
            this.Write(LogImportance.Server, "#                          Kynto Server                        #", null);
            this.Write(LogImportance.Server, "#                                                              #", null);
            this.Write(LogImportance.Server, "# Version: v1.00BETA                                           #", null);
            this.Write(LogImportance.Server, "# Time: " + DateTime.Now.ToString("MM-dd-yyyy hh:mm:sstt") + "                                  #", null);
            this.Write(LogImportance.Server, "#                                                              #", null);
            this.Write(LogImportance.Server, "################################################################", null);
            this.Write(LogImportance.Server, "", null);
            this.Write(LogImportance.Initialise, "Initialising...", null);
            this.Write(LogImportance.Initialise, " Logging service initialised.", null);
            this.Write(LogImportance.Initialise, "  Writing to server log file: \"" + this._ServerLogFile + "\".", null);
            this.Write(LogImportance.Initialise, "  Writing to database log file: \"" + this._DatabaseLogFile + "\".", null);
        }

        /// <summary>
        /// Called after the thread service has initialised, tells the logging service to start the write thread.
        /// </summary>
        public void InitialiseThread()
        {
            // Start the thread.
            this._ServerManager.ThreadService.CreateThread(LoggingThread);
        }

        /// <summary>
        /// Handles logging of data to the various log files.
        /// </summary>
        /// <param name="Instance">The thread instance.</param>
        private void LoggingThread(object Instance)
        {
            // Get the thread instance.
            ThreadInstance ThreadInstance = (ThreadInstance)Instance;

            // Loop.
            while (!ThreadInstance.Exiting)
            {
                // Flush and sleep.
                FlushQueueToLog();
                Thread.Sleep(1000);
            }
            
            // Flush the queue.
            FlushQueueToLog();
        }

        /// <summary>
        /// Writes all the queue data to the log.
        /// </summary>
        private void FlushQueueToLog()
        {
            // Write all the log file data.
            if (this._ServerLogWriteQueue.Count != 0)
            {
                StreamWriter LogFileWriter = File.AppendText(this._ServerLogFile);
                while (this._ServerLogWriteQueue.Count != 0)
                {
                    // Write
                    object[] DataToWrite = this._ServerLogWriteQueue.Dequeue();
                    long LogTime = (long)DataToWrite[0];
                    string Text = (string)DataToWrite[1];
                    string StringToWrite = LogTime + "|" + Text;
                    try { LogFileWriter.WriteLine(StringToWrite); }
                    catch { }
                }
                LogFileWriter.Flush();
                LogFileWriter.Close();
                LogFileWriter.Dispose();
            }

            // Write all the database log file data.
            if (this._DatabaseLogWriteQueue.Count != 0)
            {
                StreamWriter DatabaseLogFileWriter = File.AppendText(this._DatabaseLogFile);
                while (this._DatabaseLogWriteQueue.Count != 0)
                {
                    // Write
                    object[] DataToWrite = this._DatabaseLogWriteQueue.Dequeue();
                    long LogTime = (long)DataToWrite[0];
                    string Text = (string)DataToWrite[1];
                    string StringToWrite = LogTime + "|" + Text;
                    try { DatabaseLogFileWriter.WriteLine(StringToWrite); }
                    catch { }
                }
                DatabaseLogFileWriter.Flush();
                DatabaseLogFileWriter.Close();
                DatabaseLogFileWriter.Dispose();
            }

            // Write all the members log file data.
            while (this._MembersLogWriteQueue.Count != 0)
            {
                object[] DataToWrite = this._MembersLogWriteQueue.Dequeue();
                long LogTime = (long)DataToWrite[0];
                int MemberId = (int)DataToWrite[1];
                string Text = (string)DataToWrite[2];
                StreamWriter MembersLogFileWriter = File.AppendText(this._MembersLogsFolder + MemberId + ".log");
                string StringToWrite = LogTime + "|" + Text;
                try { MembersLogFileWriter.WriteLine(StringToWrite); }
                catch { }
                MembersLogFileWriter.Flush();
                MembersLogFileWriter.Close();
                MembersLogFileWriter.Dispose();
            }

            // Write all the rooms log file data.
            while (this._RoomsLogWriteQueue.Count != 0)
            {
                object[] DataToWrite = this._RoomsLogWriteQueue.Dequeue();
                long LogTime = (long)DataToWrite[0];
                int RoomId = (int)DataToWrite[1];
                RoomType RoomType = (RoomType)DataToWrite[2];
                string Text = (string)DataToWrite[3];
                StreamWriter RoomsLogFileWriter = null;
                if (RoomType == RoomType.PrivateRoom)
                {
                    RoomsLogFileWriter = File.AppendText(this._PrivateRoomsLogsFolder + RoomId + ".log");
                }
                else
                {
                    RoomsLogFileWriter = File.AppendText(this._PublicRoomsLogsFolder + RoomId + ".log");
                }
                string StringToWrite = LogTime + "|" + Text;
                try { RoomsLogFileWriter.WriteLine(StringToWrite); }
                catch { }
                RoomsLogFileWriter.Flush();
                RoomsLogFileWriter.Close();
                RoomsLogFileWriter.Dispose();
            }
        }

        /// <summary>
        /// Exits the logging service, closing and finalising all logs.
        /// </summary>
        public void Exit()
        {
            // Write success.
            this.Write(LogImportance.Normal, "Logging service shut down.", null);
        }

        /// <summary>
        /// Writes a line too the server log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        public void Write(LogImportance Importance, string Message, Exception ExceptionThrown)
        {
            // Compile the message.
            string CompiledMessage = "";
            switch (Importance)
            {
                case LogImportance.Normal:
                    {
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Debug:
                    {
                        if (bool.Parse(this._ServerManager.SettingsService.GetValue("Server.Debug")))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * " + Message;
                            Console.WriteLine(CompiledMessage);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        break;
                    }

                case LogImportance.Initialise:
                    {
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * " + Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Server:
                    {
                        CompiledMessage = Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
            }

            // Write to log.
            this._ServerLogWriteQueue.Enqueue(new object[] { Timestamp.Now, Message });
            if (ExceptionThrown != null)
            {
                this._ServerLogWriteQueue.Enqueue(new object[] { Timestamp.Now, ExceptionThrown.ToString() });
            }
        }

        /// <summary>
        /// Writes a line too the database log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        public void WriteDatabase(LogImportance Importance, string Message, Exception ExceptionThrown)
        {
            // Compile the message.
            string CompiledMessage = "";
            switch (Importance)
            {
                case LogImportance.Normal:
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [DB] " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [DB] " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [DB] " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [DB] " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Debug:
                    {
                        if (bool.Parse(this._ServerManager.SettingsService.GetValue("Server.Debug")))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [DB] " + Message;
                            Console.WriteLine(CompiledMessage);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        break;
                    }

                case LogImportance.Initialise:
                    {
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [DB] " + Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Server:
                    {
                        CompiledMessage = Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
            }

            // Write to log.
            this._DatabaseLogWriteQueue.Enqueue(new object[] { Timestamp.Now, Message });
            if (ExceptionThrown != null)
            {
                this._DatabaseLogWriteQueue.Enqueue(new object[] { Timestamp.Now, ExceptionThrown.ToString() });
            }
        }

        /// <summary>
        /// Writes a line too the room log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="RoomId">The id of the room.</param>
        /// <param name="RoomType">The rooms type.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        public void WriteRoom(LogImportance Importance, int RoomId, RoomType RoomType, string Message, Exception ExceptionThrown)
        {
            // Compile the message.
            string CompiledMessage = "";
            switch (Importance)
            {
                case LogImportance.Normal:
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [RM] " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [RM] " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [RM] " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [RM] " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Debug:
                    {
                        if (bool.Parse(this._ServerManager.SettingsService.GetValue("Server.Debug")))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [RM] " + Message;
                            Console.WriteLine(CompiledMessage);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        break;
                    }

                case LogImportance.Initialise:
                    {
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [RM] " + Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Server:
                    {
                        CompiledMessage = Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
            }

            // Write to log.
            this._RoomsLogWriteQueue.Enqueue(new object[] { Timestamp.Now, RoomId, RoomType, Message });
            if (ExceptionThrown != null)
            {
                this._RoomsLogWriteQueue.Enqueue(new object[] { Timestamp.Now, RoomId, RoomType, ExceptionThrown.ToString() });
            }
        }

        /// <summary>
        /// Writes a line to the users log.
        /// </summary>
        /// <param name="Importance">The importance of the log message.</param>
        /// <param name="UserId">The users id.</param>
        /// <param name="Message">The message to write.</param>
        /// <param name="ExceptionThrown">The exception thrown.</param>
        public void WriteMember(LogImportance Importance, int UserId, string Message, Exception ExceptionThrown)
        {
            // Compile the message.
            string CompiledMessage = "";
            switch (Importance)
            {
                case LogImportance.Normal:
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [MR] " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [MR] " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [MR] " + Message;
                        if (ExceptionThrown != null)
                        {
                            CompiledMessage += Environment.NewLine + "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [MR] " + ExceptionThrown.ToString();
                        }
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Debug:
                    {
                        if (bool.Parse(this._ServerManager.SettingsService.GetValue("Server.Debug")))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [MR] " + Message;
                            Console.WriteLine(CompiledMessage);
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        break;
                    }

                case LogImportance.Initialise:
                    {
                        CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * [MR] " + Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }

                case LogImportance.Server:
                    {
                        CompiledMessage = Message;
                        Console.WriteLine(CompiledMessage);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
            }

            // Write to log.
            this._MembersLogWriteQueue.Enqueue(new object[] { Timestamp.Now, UserId, Message });
            if (ExceptionThrown != null)
            {
                this._MembersLogWriteQueue.Enqueue(new object[] { Timestamp.Now, UserId, ExceptionThrown.ToString() });
            }
        }

        /// <summary>
        /// Writes a line to the packets log.
        /// </summary>
        /// <param name="ConnectionId">The connection id.</param>
        /// <param name="Direction">The packet direction.</param>
        /// <param name="PacketType">The type of packet.</param>
        /// <param name="Packet">The packet.</param>
        public void WritePacket(int ConnectionId, PacketDirection Direction, PacketType PacketType, string Packet)
        {
            if (bool.Parse(_ServerManager.SettingsService.GetValue("Server.Debug")))
            {
                // Compile the message.
                string CompiledMessage = "[" + DateTime.Now.ToString("MM-dd-yyyy hh:mmtt") + "] * ";

                if (PacketType == PacketType.Game)
                {
                    CompiledMessage += "[GAME][" + ConnectionId + "] ";
                }
                else if (PacketType == PacketType.Backend)
                {
                    CompiledMessage += "[BACKEND][" + ConnectionId + "] ";
                }

                if (Direction == PacketDirection.In)
                {
                    CompiledMessage += "<< ";
                }
                else if (Direction == PacketDirection.Out)
                {
                    CompiledMessage += ">> ";
                }

                CompiledMessage += Packet;

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(CompiledMessage);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
