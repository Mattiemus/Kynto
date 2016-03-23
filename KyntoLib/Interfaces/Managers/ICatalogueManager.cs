using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Managers
{
    /// <summary>
    /// Manages the catalogue and its contents.
    /// </summary>
    public interface ICatalogueManager
    {
        /// <summary>
        /// Gets the list of all the furni templates.
        /// </summary>
        Dictionary<string, IFurniDatabaseTable> FurniTemplates { get; }

        /// <summary>
        /// Gets the list of all the furni data templates.
        /// </summary>
        Dictionary<string, List<IFurniDataDatabaseTable>> FurniDataTemplates { get; }

        /// <summary>
        /// Gets the list of catalogue pages.
        /// </summary>
        Dictionary<int, ICataloguePagesDatabaseTable> CataloguePages { get; }

        /// <summary>
        /// Gets the list of catalogue items by catalogue page id.
        /// </summary>
        Dictionary<int, List<ICatalogueItemsDatabaseTable>> CatalogueItems { get; }

        /// <summary>
        /// Gets the list of all the clothes.
        /// </summary>
        Dictionary<int, IClothesDatabaseTable> Clothes { get; }

        /// <summary>
        /// Initialises this catalogue manager and loads all catalogue items and pages.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Initialises all the furni templates.
        /// </summary>
        void InitialiseFurni();

        /// <summary>
        /// Initialises all the catalogue pages.
        /// </summary>
        void InitialiseCataloguePages();

        /// <summary>
        /// Initialises all the catalogue items.
        /// </summary>
        void InitialiseCatalogueItems();

        /// <summary>
        /// Initialises all the clothes.
        /// </summary>
        void InitialiseClothes();

        /// <summary>
        /// Gets the list of clothes available to a specific rank.
        /// </summary>
        /// <param name="RankId">The rank the clothes should be available too.</param>
        /// <returns>The clothes available to the specified rank.</returns>
        List<IClothesDatabaseTable> GetClothesByRank(int RankId);

        /// <summary>
        /// Exits the catalogue manager.
        /// </summary>
        void Exit();
    }
}
