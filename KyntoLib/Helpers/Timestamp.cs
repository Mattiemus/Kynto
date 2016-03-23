using System;

namespace KyntoLib.Helpers
{
    /// <summary>
    /// Handles Unix timestamps.
    /// </summary>
    public static class Timestamp
    {
        /// <summary>
        /// Gets the current Unix timestamp.
        /// </summary>
        public static long Now
        {
            get
            {
                // Compares now with the Unix epoch.
                DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
                TimeSpan TimeSpan = (DateTime.Now.ToUniversalTime() - UnixEpoch);

                // Returns the number of seconds that have passed.
                return (long)TimeSpan.TotalSeconds;
            }
        }
    }
}
