using System;

namespace KyntoLib.Interfaces.Database.Tables
{
    /// <summary>
    /// Represents the sso tickets database table.
    /// </summary>
    public interface ISsoTicketsDatabaseTable : IDatabaseTable
    {
        /// <summary>
        /// The sso ticket id.
        /// </summary>
        string Ticket { get; set; }

        /// <summary>
        /// The member id this ticket belongs too.
        /// </summary>
        int MemberId { get; set; }

        /// <summary>
        /// The timestamp when this ticket was created.
        /// </summary>
        int Created { get; set; }
    }

    /// <summary>
    /// Represents the sso tickets table fields.
    /// </summary>
    public static class SsoTicketsTableFields
    {
        public const string Ticket = "ticket";
        public const string MemberId = "member_id";
        public const string Created = "created";
    }
}
