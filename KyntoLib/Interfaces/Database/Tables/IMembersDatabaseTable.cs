using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents a members database table.
    /// </summary>
    public interface IMembersDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The id of this member.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// The username of this member.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// The password for this member.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// The email address for this member.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The timestamp when this member was created.
        /// </summary>
        int Created { get; set; }

        /// <summary>
        /// Weather this member is activated.
        /// </summary>
        bool Activated { get; set; }

        /// <summary>
        /// Weather we can email this member.
        /// </summary>
        bool Automail { get; set; }

        /// <summary>
        /// The rank id for this member.
        /// </summary>
        int RankId { get; set; }

        /// <summary>
        /// The ip address where this member last logged in at.
        /// </summary>
        string Ip { get; set; }

        /// <summary>
        /// The last timestamp when this member was online.
        /// </summary>
        int LastOnline { get; set; }

        /// <summary>
        /// Weather or not this member is banned.
        /// </summary>
        bool Ban { get; set; }

        /// <summary>
        /// The ban id that relates to this members ban.
        /// </summary>
        int BanId { get; set; }

        /// <summary>
        /// The clothes this member is wearing.
        /// </summary>
        string Clothes { get; set; }

        /// <summary>
        /// The sex of this member.
        /// </summary>
        string Sex { get; set; }

        /// <summary>
        /// The active badge this member is wearing.
        /// </summary>
        int ActiveBadge { get; set; }

        /// <summary>
        /// The motto of this member.
        /// </summary>
        string Motto { get; set; }

        /// <summary>
        /// The number of silver blocks this member has.
        /// </summary>
        int BlocksSilver { get; set; }
    }

    /// <summary>
    /// Represents the members table fields.
    /// </summary>
    public static class MembersTableFields
    {
        public const string Id = "id";
        public const string Username = "username";
        public const string Password = "password";
        public const string Email = "email";
        public const string Created = "created";
        public const string Activated = "activated";
        public const string Automail = "automail";
        public const string RankId = "rank_id";
        public const string Ip = "ip";
        public const string LastOnline = "last_online";
        public const string Ban = "ban";
        public const string BanId = "ban_id";
        public const string Clothes = "clothes";
        public const string Sex = "sex";
        public const string ActiveBadge = "active_badge";
        public const string Motto = "motto";
        public const string BlocksSilver = "blocks_silver";
    }
}
