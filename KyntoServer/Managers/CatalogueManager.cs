using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

namespace KyntoServer.Managers
{
    /// <summary>
    /// Manages the catalogue and its contents.
    /// </summary>
    public class CatalogueManager : ICatalogueManager
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Stores the list of all the furni templates.
        /// </summary>
        private Dictionary<string, IFurniDatabaseTable> _FurniTemplates = new Dictionary<string,IFurniDatabaseTable>();

        /// <summary>
        /// Gets the list of all the furni templates.
        /// </summary>
        public Dictionary<string, IFurniDatabaseTable> FurniTemplates
        {
            get
            {
                return this._FurniTemplates;
            }
        }

        /// <summary>
        /// Gets the list of all the furni data templates.
        /// </summary>
        private Dictionary<string, List<IFurniDataDatabaseTable>> _FurniDataTemplates = new Dictionary<string, List<IFurniDataDatabaseTable>>();

        /// <summary>
        /// Gets the list of all the furni data templates.
        /// </summary>
        public Dictionary<string, List<IFurniDataDatabaseTable>> FurniDataTemplates
        {
            get
            {
                return this._FurniDataTemplates;
            }
        }

        /// <summary>
        /// Stores the list of catalogue pages.
        /// </summary>
        private Dictionary<int, ICataloguePagesDatabaseTable> _CataloguePages = new Dictionary<int, ICataloguePagesDatabaseTable>();

        /// <summary>
        /// Gets the list of catalogue pages.
        /// </summary>
        public Dictionary<int, ICataloguePagesDatabaseTable> CataloguePages
        {
            get
            {
                return this._CataloguePages;
            }
        }

        /// <summary>
        /// Stores the list of catalogue items by catalogue page id.
        /// </summary>
        private Dictionary<int, List<ICatalogueItemsDatabaseTable>> _CatalogueItems = new Dictionary<int, List<ICatalogueItemsDatabaseTable>>();

        /// <summary>
        /// Gets the list of catalogue items by catalogue page id.
        /// </summary>
        public Dictionary<int, List<ICatalogueItemsDatabaseTable>> CatalogueItems
        {
            get
            {
                return this._CatalogueItems;
            }
        }

        /// <summary>
        /// Stores the list of clothes.
        /// </summary>
        private Dictionary<int, IClothesDatabaseTable> _Clothes = new Dictionary<int, IClothesDatabaseTable>();

        /// <summary>
        /// Gets the list of all the clothes.
        /// </summary>
        public Dictionary<int, IClothesDatabaseTable> Clothes
        {
            get
            {
                return this._Clothes;
            }
        }

        /// <summary>
        /// Initialises this catalogue manager and loads all catalogue items and pages.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;

            // Write that we have initialised.
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Catalogue manager initialised.", null);

            // Initialise catalogue manager stuff.
            InitialiseFurni();
            InitialiseCataloguePages();
            InitialiseCatalogueItems();
            InitialiseClothes();
        }

        /// <summary>
        /// Initialises all the furni templates.
        /// </summary>
        public void InitialiseFurni()
        {
            // Templates
            {
                // Firstly clear all the furni templates.
                this._FurniTemplates.Clear();

                // Load all the templates.
                List<IDatabaseTable> Furni = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Furni)
                    .Select()
                    .ExecuteRead();

                // Make sure we got some templates.
                if (Furni == null || Furni.Count == 0)
                {
                    // Write that we have failed to initialised.
                    this._ServerManager.LoggingService.Write(LogImportance.Error, "  Failed to gather template data.", null);
                    this._ServerManager.Exit(null, "Failed to load templates.");
                }
                else
                {
                    // Process each template.
                    for (int i = 0; i < Furni.Count; i++)
                    {
                        // Add to the list.
                        IFurniDatabaseTable FurniTemplate = (IFurniDatabaseTable)Furni[i];
                        this._FurniTemplates.Add(FurniTemplate.Furni, FurniTemplate);
                    }

                    // Write that we loaded ranks.
                    this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded " + Furni.Count + " furni templates.", null);
                }
            }

            // Data
            {
                // Firstly clear all furni data templates.
                this._FurniDataTemplates.Clear();

                // Load all the data templates.
                List<IDatabaseTable> DataTemplates = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.FurniData)
                    .Select()
                    .ExecuteRead();

                // Make sure we got some data.
                if (DataTemplates == null || DataTemplates.Count == 0)
                {
                    // Write that we have failed to initialised.
                    this._ServerManager.LoggingService.Write(LogImportance.Error, "  Failed to gather fruni data template data.", null);
                    this._ServerManager.Exit(null, "Failed to load furni data templates.");
                }
                else
                {
                    // Process each template.
                    for (int i = 0; i < DataTemplates.Count; i++)
                    {
                        // Add to the list.
                        IFurniDataDatabaseTable FurniDataTemplate = (IFurniDataDatabaseTable)DataTemplates[i];
                        if (!this._FurniDataTemplates.ContainsKey(FurniDataTemplate.ItemId))
                        {
                            this._FurniDataTemplates.Add(FurniDataTemplate.ItemId, new List<IFurniDataDatabaseTable>());
                        }
                        this._FurniDataTemplates[FurniDataTemplate.ItemId].Add(FurniDataTemplate);
                    }

                    // Write that we loaded ranks.
                    this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded " + this._FurniDataTemplates.Count + " furni data templates.", null);
                }
            }
        }

        /// <summary>
        /// Initialises all the catalogue pages.
        /// </summary>
        public void InitialiseCataloguePages()
        {
            // Firstly clear the catalogue pages.
            this._CataloguePages.Clear();

            // Load all the pages.
            List<IDatabaseTable> CataloguePages = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.CataloguePages)
                .Select()
                .ExecuteRead();

            // Make sure we got some pages.
            if (CataloguePages == null)
            {
                // Write that we have failed to initialised.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "  Failed to gather catalogue page data.", null);
                this._ServerManager.Exit(null, "Failed to load catalogue pages.");
            }
            else
            {
                // Process each page.
                for (int i = 0; i < CataloguePages.Count; i++)
                {
                    // Add to the list.
                    ICataloguePagesDatabaseTable CataloguePage = (ICataloguePagesDatabaseTable)CataloguePages[i];
                    this._CataloguePages.Add(CataloguePage.Id, CataloguePage);
                }

                // Write that we loaded the pages.
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded " + CataloguePages.Count + " catalogue pages.", null);
            }
        }

        /// <summary>
        /// Initialises all the catalogue items.
        /// </summary>
        public void InitialiseCatalogueItems()
        {
            // Firstly clear the catalogue items.
            this._CataloguePages.Clear();

            // Load all the items.
            List<IDatabaseTable> CatalogueItems = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.CatalogueItems)
                .Select()
                .ExecuteRead();

            // Make sure we got some items.
            if (CatalogueItems == null)
            {
                // Write that we have failed to initialised.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "  Failed to gather catalogue items data.", null);
                this._ServerManager.Exit(null, "Failed to load catalogue items.");
            }
            else
            {
                // Process each page.
                for (int i = 0; i < CatalogueItems.Count; i++)
                {
                    // Add to the list.
                    ICatalogueItemsDatabaseTable CatalogueItem = (ICatalogueItemsDatabaseTable)CatalogueItems[i];

                    // Add to the list.
                    if (!this._CatalogueItems.ContainsKey(CatalogueItem.PageId))
                    {
                        this._CatalogueItems.Add(CatalogueItem.PageId, new List<ICatalogueItemsDatabaseTable>());
                    }
                    this._CatalogueItems[CatalogueItem.PageId].Add(CatalogueItem);
                }

                // Write that we loaded the pages.
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded " + CatalogueItems.Count + " catalogue items.", null);
            }
        }

        /// <summary>
        /// Initialises all the clothes.
        /// </summary>
        public void InitialiseClothes()
        {
            // Firstly clear the clothes.
            this._Clothes.Clear();

            // Load all the clothes data.
            List<IDatabaseTable> Clothes = this._ServerManager.DatabaseService.Database.CreateQuery(DatabaseTables.Clothes)
                .Select()
                .ExecuteRead();

            // Make sure we got some clothes.
            if (Clothes == null || Clothes.Count == 0)
            {
                // Write that we have failed to initialised.
                this._ServerManager.LoggingService.Write(LogImportance.Error, "  Failed to gather clothes.", null);
                this._ServerManager.Exit(null, "Failed to load clothes.");
            }
            else
            {
                // Process each item of clothing..
                for (int i = 0; i < Clothes.Count; i++)
                {
                    // Add to the list.
                    IClothesDatabaseTable Clothing = (IClothesDatabaseTable)Clothes[i];
                    this._Clothes.Add(Clothing.Id, Clothing);
                }

                // Write that we loaded clothes.
                this._ServerManager.LoggingService.Write(LogImportance.Initialise, "  Loaded " + Clothes.Count + " clothes.", null);
            }
        }

        /// <summary>
        /// Gets the list of clothes available to a specific rank.
        /// </summary>
        /// <param name="RankId">The rank the clothes should be available too.</param>
        /// <returns>The clothes available to the specified rank.</returns>
        public List<IClothesDatabaseTable> GetClothesByRank(int RankId)
        {
            List<IClothesDatabaseTable> Clothes = new List<IClothesDatabaseTable>();

            foreach (int i in this._Clothes.Keys)
            {
                if (this._Clothes[i].RankId <= RankId)
                {
                    Clothes.Add(this._Clothes[i]);
                }
            }

            return Clothes;
        }

        /// <summary>
        /// Exits the catalogue manager.
        /// </summary>
        public void Exit()
        {
        }
    }
}
