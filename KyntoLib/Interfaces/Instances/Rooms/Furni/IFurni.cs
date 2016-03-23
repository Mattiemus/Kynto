using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Instances.Rooms.Furni
{
    /// <summary>
    /// Interfaces with a furni item.
    /// </summary>
    public interface IFurni
    {
        /// <summary>
        /// Gets or sets if this furni item is the root item.
        /// </summary>
        bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets this furni items data.
        /// </summary>
        IItemsDatabaseTable ItemData { get; set; }

        /// <summary>
        /// Gets or sets this furnis template data.
        /// </summary>
        IFurniDatabaseTable ItemTemplate { get; set; }

        /// <summary>
        /// Gets or sets this furnis data.
        /// </summary>
        List<IFurniDataDatabaseTable> FurniData { get; set; }

        /// <summary>
        /// Copies this furni instance.
        /// </summary>
        /// <returns>A copy of this furni instance.</returns>
        IFurni Copy();
    }
}
