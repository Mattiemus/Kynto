using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoServer.Instances.Rooms.Furni
{
    /// <summary>
    /// Represents a single furni item.
    /// </summary>
    public class BasicFurniItem : IFurni
    {
        /// <summary>
        /// Stores if this is the root node of an item.
        /// </summary>
        private bool _IsRoot = false;

        /// <summary>
        /// Gets or sets if this furni item is the root item.
        /// </summary>
        public bool IsRoot
        {
            get
            {
                return this._IsRoot;
            }
            set
            {
                this._IsRoot = value;
            }
        }

        /// <summary>
        /// Stores this furnis item data.
        /// </summary>
        private IItemsDatabaseTable _ItemData;

        /// <summary>
        /// Gets or sets this furni items data.
        /// </summary>
        public IItemsDatabaseTable ItemData
        {
            get
            {
                return this._ItemData;
            }
            set
            {
                this._ItemData = value;
            }
        }

        /// <summary>
        /// Stores this furnis item template.
        /// </summary>
        private IFurniDatabaseTable _ItemTemplate;

        /// Gets or sets this furnis template data.
        /// </summary>
        public IFurniDatabaseTable ItemTemplate
        {
            get
            {
                return this._ItemTemplate;
            }
            set
            {
                this._ItemTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets this furnis data.
        /// </summary>
        private List<IFurniDataDatabaseTable> _FurniData;

        /// <summary>
        /// Gets or sets this furnis data.
        /// </summary>
        public List<IFurniDataDatabaseTable> FurniData
        {
            get
            {
                return this._FurniData;
            }
            set
            {
                this._FurniData = value;
            }
        }

        /// <summary>
        /// Copies this furni instance.
        /// </summary>
        /// <returns>A copy of this furni instance.</returns>
        public IFurni Copy()
        {
            // Return a copy.
            return new BasicFurniItem() { _IsRoot = this._IsRoot, _ItemData = this._ItemData, _ItemTemplate = this._ItemTemplate, _FurniData = this._FurniData };
        }
    }
}
