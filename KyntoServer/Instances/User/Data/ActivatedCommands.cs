using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Instances.User.Data;

namespace KyntoServer.Instances.User.Data
{
    /// <summary>
    /// Represents all the possible activated commands of a user.
    /// </summary>
    public class ActivatedCommands : IActivatedCommands
    {
        /// <summary>
        /// Stores if the be right back command is active.
        /// </summary>
        private bool _Brb;

        /// <summary>
        /// Gets or sets if the be right back command is active.
        /// </summary>
        public bool Brb
        {
            get
            {
                return this._Brb;
            }
            set
            {
                this._Brb = value;
            }
        }
    }
}
