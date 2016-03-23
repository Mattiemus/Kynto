using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using KyntoLib.Interfaces.Services;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Helpers;
using KyntoLib.Data;

namespace KyntoServer.Services
{
    /// <summary>
    /// Stores the settings for the server.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Store the settings xml structure.
        /// </summary>
        private SettingsXMLFileStructure _SettingsData;

        /// <summary>
        /// Stores the settings dictionary.
        /// </summary>
        private Dictionary<string, string> _Settings = new Dictionary<string, string>();

        /// <summary>
        /// Loads in the settings including passed command line arguments.
        /// </summary>
        /// <param name="CommandLineArguments">The command line arguments passed too us.</param>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(string[] CommandLineArguments, IServerManager ServerInstance)
        {
            // Store the server.
            this._ServerManager = ServerInstance;

            // Used later...
            FileStream SettingsFile = null;

            // Find the location of the settings file.
            if (File.Exists(Program.BaseDirectory + "settings.xml"))
            {
                SettingsFile = File.OpenRead(Program.BaseDirectory + "settings.xml");
            }
            else
            {
                // Error - could not open settings file.
                this._ServerManager.LoggingService.Write(LogImportance.Error, " Settings file not found!", null);
                this._ServerManager.Exit(null, "Settings file not found.");
            }

            // Read the settings file.
            int FileLength = (int)SettingsFile.Length;
            byte[] FileBuffer = new byte[FileLength];
            int ActualBytesRead;
            int SumBytesRead = 0;
            while ((ActualBytesRead = SettingsFile.Read(FileBuffer, SumBytesRead, FileLength - SumBytesRead)) > 0)
            {
                SumBytesRead += ActualBytesRead;
            }
            string FileContents = Encoding.ASCII.GetString(FileBuffer);

            // Read out the settings.
            try
            {
                this._SettingsData = XML.DeSerialize<SettingsXMLFileStructure>(FileContents);
            }
            catch
            {
                // Error - could not read settings file.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "Settings file was corrupt.", null);
                this._ServerManager.Exit(null, "Settings was corrupt.");
            }

            // Add some extra settings...
            // Dev mode.
            if (CommandLineArguments.Length == 1)
            {
                this._SettingsData.Settings = ArrayHelper.AddToArray<SettingsXMLFileStructure.SettingsXMLItemStructure>(_SettingsData.Settings, new SettingsXMLFileStructure.SettingsXMLItemStructure() { Name = "DevMode", Value = "True" });
            }
            else
            {
                this._SettingsData.Settings = ArrayHelper.AddToArray<SettingsXMLFileStructure.SettingsXMLItemStructure>(_SettingsData.Settings, new SettingsXMLFileStructure.SettingsXMLItemStructure() { Name = "DevMode", Value = "False" });
            }

            // Close the settings file.
            SettingsFile.Close();
            SettingsFile.Dispose();

            // Buffer the settings.
            for (int i = 0; i < this._SettingsData.Settings.Length; i++)
            {
                this._Settings.Add(this._SettingsData.Settings[i].Name, this._SettingsData.Settings[i].Value);
            }

            // Write success...
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Settings file loaded.", null);
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded: \"" + this._SettingsData.Settings.Length + "\" settings.", null);
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Dev mode: \"" + this.GetValue("DevMode") + "\".", null);
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Debug mode: \"" + this.GetValue("Server.Debug") + "\".", null);
        }

        /// <summary>
        /// Exits the settings service, closing all settings files.
        /// </summary>
        public void Exit()
        {
            // Nothing to do really...
            // Write success.
            this._ServerManager.LoggingService.Write(LogImportance.Normal, "Settings service shutdown.", null);
        }

        /// <summary>
        /// Gets a value of a setting item by key.
        /// </summary>
        /// <param name="Key">The key to get the value of.</param>
        /// <returns>The value of the setting.</returns>
        public string GetValue(string Key)
        {
            // Make sure the setting exists.
            if (this._Settings.ContainsKey(Key))
            {
                return this._Settings[Key];
            }

            // The setting doesn't exist, return null.
            return null;
        }
    }

    /// <summary>
    /// Stores the structure for the settings XML.
    /// </summary>
    public class SettingsXMLFileStructure
    {
        /// <summary>
        /// Stores the structure for a single settings item.
        /// </summary>
        public class SettingsXMLItemStructure
        {
            /// <summary>
            /// Store the setting item name.
            /// </summary>
            public string Name;

            /// <summary>
            /// Store the setting item value.
            /// </summary>
            public string Value;
        }

        /// <summary>
        /// Stores the settings.
        /// </summary>
        public SettingsXMLItemStructure[] Settings;
    }
}
