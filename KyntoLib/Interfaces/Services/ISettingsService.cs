using System;

using KyntoLib.Interfaces.Managers;

namespace KyntoLib.Interfaces.Services
{
    /// <summary>
    /// Stores the settings for the server.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Loads in the settings including passed command line arguments.
        /// </summary>
        /// <param name="CommandLineArguments">The command line arguments passed too us.</param>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(string[] CommandLineArguments, IServerManager ServerInstance);

        /// <summary>
        /// Exits the settings service, closing all settings files.
        /// </summary>
        void Exit();

        /// <summary>
        /// Gets a value of a setting item by key.
        /// </summary>
        /// <param name="Key">The key to get the value of.</param>
        /// <returns>The value of the setting.</returns>
        string GetValue(string Key);
    }
}
