using System;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

using MySql.Data.MySqlClient;

using KyntoDatabase.MySQL.Query;
using KyntoDatabase.MySQL.Tables;

namespace KyntoDatabase.MySQL
{
    /// <summary>
    /// Represents a database.
    /// </summary>
    public class MySQLDatabase : IDatabaseInterface
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Gets the server manager.
        /// </summary>
        internal IServerManager ServerManager
        {
            get
            {
                return this._ServerManager;
            }
        }

        /// <summary>
        /// Stores the mysql connection.
        /// </summary>
        private MySqlConnection _Connection;

        /// <summary>
        /// Gets the MySql server connection.
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                return this._Connection;
            }
        }

        /// <summary>
        /// Gets the database type this handler handles.
        /// </summary>
        public string DatabaseType
        {
            get
            {
                return "MySQL";
            }
        }

        /// <summary>
        /// Gets the last insert id.
        /// </summary>
        public long LastInsertId
        {
            get
            {
                MySqlCommand Command = this._Connection.CreateCommand();
                Command.CommandText = "SELECT LAST_INSERT_ID()";
                long LastId = (long)Command.ExecuteScalar();

                return LastId;
            }
        }

        /// <summary>
        /// Initialises this database handler.
        /// </summary>
        /// <param name="ServerInstance">The server manager.</param>
        public MySQLDatabase(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Connect to the database.
        /// </summary>
        /// <returns>True for connection success, false for connection failure.</returns>
        public bool Connect()
        {            
            // Create our connection string.
            string ConnectionString = "Server=" + this._ServerManager.SettingsService.GetValue("Database.Connector.Host") + ";"
                + "Database=" + this._ServerManager.SettingsService.GetValue("Database.Connector.Database") + ";"
                + "User ID=" + this._ServerManager.SettingsService.GetValue("Database.Connector.Username") + ";"
                + "Password=" + this._ServerManager.SettingsService.GetValue("Database.Connector.Password");

            // Attempt to connect.
            try
            {
                // Connect.
                this._Connection = new MySqlConnection();
                this._Connection.ConnectionString = ConnectionString;
                this._Connection.Open();

                return true;
            }
            catch (Exception Ex)
            {
                // Write that we got an error.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "Error connecting to database.", Ex);

                return false;
            }
        }

        /// <summary>
        /// Disconnects the database.
        /// </summary>
        /// <returns>True for success.</returns>
        public bool Disconnect()
        {
            // Disconnect the database.
            if (this._Connection != null)
            {
                this._Connection.Close();
            }

            return true;
        }

        /// <summary>
        /// Creates a database query linked to a table.
        /// </summary>
        /// <param name="Table">The table this query will execute on.</param>
        /// <returns>A new database query.</returns>
        public IDatabaseQuery CreateQuery(string Table)
        {
            // Create the query.
            return new MySQLDatabaseQuery(this, Table);
        }

        /// <summary>
        /// Creates a blank table of the specified type.
        /// </summary>
        /// <param name="Table">The table type.</param>
        /// <returns>A blank table of the specified type.</returns>
        public IDatabaseTable CreateBlankTable(string Table)
        {
            switch (Table)
            {
                case DatabaseTables.Members:
                    return new MembersDatabaseTable(this);
                case DatabaseTables.MembersBadges:
                    return new MembersBadgesDatabaseTable(this);
                case DatabaseTables.MemberBan:
                    return new MembersBanDatabaseTable(this);
                case DatabaseTables.MembersClothes:
                    return new MembersClothesDatabaseTable(this);
                case DatabaseTables.MembersFriends:
                    return new MembersFriendsDatabaseTable(this);
                case DatabaseTables.MembersCommands:
                    return new MembersCommandsDatabaseTable(this);
        
                case DatabaseTables.Rooms:
                    return new RoomsDatabaseTable(this);
                case DatabaseTables.RoomsRights:
                    return new RoomsRightsDatabaseTable(this);

                case DatabaseTables.Publics:
                    return new PublicsDatabaseTable(this);
                case DatabaseTables.PublicsItems:
                    return new PublicsItemsDatabaseTable(this);

                case DatabaseTables.CataloguePages:
                    return new CataloguePagesDatabaseTable(this);
                case DatabaseTables.CatalogueItems:
                    return new CatalogueItemsDatabaseTable(this);

                case DatabaseTables.Clothes:
                    return new ClothesDatabaseTable(this);
                case DatabaseTables.Ranks:
                    return new RanksDatabaseTable(this);
                case DatabaseTables.Furni:
                    return new FurniDatabaseTable(this);
                case DatabaseTables.FurniData:
                    return new FurniDataDatabaseTable(this);
                case DatabaseTables.Messages:
                    return new MessagesDatabaseTable(this);
                case DatabaseTables.SavedIms:
                    return new SavedImsDatabaseTable(this);
                case DatabaseTables.Items:
                    return new ItemsDatabaseTable(this);
                case DatabaseTables.SsoTickets:
                    return new SsoTicketsDatabaseTable(this);

                default:
                    throw new Exception("Unknown database table \"" + Table + "\"");
            }
        }
    }
}
