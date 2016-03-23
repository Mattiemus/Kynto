using System;

using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Database
{
    /// <summary>
    /// Represents a database.
    /// </summary>
    public interface IDatabaseInterface
    {
        /// <summary>
        /// Gets the database type this handler handles.
        /// </summary>
        string DatabaseType { get; }

        /// <summary>
        /// Gets the last insert id.
        /// </summary>
        long LastInsertId { get; }

        /// <summary>
        /// Connect to the database.
        /// </summary>
        /// <returns>True for connection success, false for connection failure.</returns>
        bool Connect();

        /// <summary>
        /// Disconnects the database.
        /// </summary>
        /// <returns>True for success.</returns>
        bool Disconnect();

        /// <summary>
        /// Creates a database query linked to a table.
        /// </summary>
        /// <param name="Table">The table this query will execute on.</param>
        /// <returns>A new database query.</returns>
        IDatabaseQuery CreateQuery(string Table);

        /// <summary>
        /// Creates a blank table of the specified type.
        /// </summary>
        /// <param name="Table">The table type.</param>
        /// <returns>A blank table of the specified type.</returns>
        IDatabaseTable CreateBlankTable(string Table);
    }

    /// <summary>
    /// The comparison to do between database fields and values
    /// </summary>
    public enum DatabaseComparison
    {
        /// <summary>
        /// Check if the two values are equal.
        /// </summary>
        Equals,

        /// <summary>
        /// Checks if the two values are not equal.
        /// </summary>
        NotEquals,

        /// <summary>
        /// Equal to or less that.
        /// </summary>
        EqualOrLessThan,

        /// <summary>
        /// Like.
        /// </summary>
        Like,
    }

    /// <summary>
    /// The order in which to return a query.
    /// </summary>
    public enum DatabaseOrder
    {
        /// <summary>
        /// Sort lowest to highest.
        /// </summary>
        Ascending,

        /// <summary>
        /// Sort highest to lowest.
        /// </summary>
        Decending
    }

    /// <summary>
    /// Represents the different possible database tables.
    /// </summary>
    public static class DatabaseTables
    {
        public const string Members = "members";
        public const string MembersBadges = "members_badges";
        public const string MemberBan = "members_ban";
        public const string MembersClothes = "members_clothes";
        public const string MembersFriends = "members_friends";
        public const string MembersCommands = "members_commands";
        
        public const string Rooms = "rooms";
        public const string RoomsRights = "rooms_rights";

        public const string Publics = "publics";
        public const string PublicsItems = "publics_items";

        public const string CataloguePages = "catalogue_pages";
        public const string CatalogueItems = "catalogue_items";

        public const string Clothes = "clothes";
        public const string Ranks = "ranks";
        public const string Furni = "furni";
        public const string FurniData = "furni_data";
        public const string Messages = "messages";
        public const string SavedIms = "saved_ims";
        public const string Items = "items";
        public const string SsoTickets = "sso_tickets";
    }
}
