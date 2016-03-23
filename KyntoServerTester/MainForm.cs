using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KyntoServerTester.DebugClient;
using KyntoServerTester.Server;
using KyntoServerTester.Server.Monitors;
using KyntoServerTester.Server.Monitors.Managers;
using KyntoServerTester.Server.Monitors.Services;

namespace KyntoServerTester
{
    /// <summary>
    /// The main server tester form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Stores the server statistics form.
        /// </summary>
        public ServerStatisticsForm ServerStatistics;

        /// <summary>
        /// Stores the catalogue monitor.
        /// </summary>
        public CatalogueMonitorForm CatalogueMonitor;

        /// <summary>
        /// Stores the controllers monitor.
        /// </summary>
        public ControllersMonitorForm ControllersMonitor;

        /// <summary>
        /// Stores the events monitor.
        /// </summary>
        public EventsMonitorForm EventsMonitor;

        /// <summary>
        /// Stores the plugins monitor.
        /// </summary>
        public PluginsMonitorForm PluginsMonitor;

        /// <summary>
        /// Stores the rooms monitor.
        /// </summary>
        public RoomsMonitorForm RoomsMonitor;

        /// <summary>
        /// Stores the users monitor.
        /// </summary>
        public UsersMonitorForm UsersMonitor;

        /// <summary>
        /// Stores the backend sockets monitor.
        /// </summary>
        public BackendSocketsMonitorForm BackendSocketsMonitor;

        /// <summary>
        /// Stores the database monitor.
        /// </summary>
        public DatabaseMonitorForm DatabaseMonitor;

        /// <summary>
        /// Stores the game sockets monitor.
        /// </summary>
        public GameSocketsMonitorForm GameSocketsMonitor;

        /// <summary>
        /// Stores the logging monitor.
        /// </summary>
        public LoggingMonitorForm LoggingMonitor;

        /// <summary>
        /// Stores the settings monitor.
        /// </summary>
        public SettingsMonitorForm SettingsMonitor;

        /// <summary>
        /// Stores the thread monitor.
        /// </summary>
        public ThreadMonitorForm ThreadMonitor;

        /// <summary>
        /// Stores the list of debug clients.
        /// </summary>
        public List<DebugClientForm> DebugClients = new List<DebugClientForm>();

        /// <summary>
        /// Initialises this form.
        /// </summary>
        public MainForm()
        {
            // Create the gui.
            InitializeComponent();
        }

        /// <summary>
        /// The onload event.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void MainForm_Load(object Sender, EventArgs EventArgs)
        {

        }

        /// <summary>
        /// The onclosed event.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void MainForm_FormClosed(object Sender, FormClosedEventArgs EventArgs)
        {
            // Close the kynto server as well..
            KyntoServer.Program.ServerManager.Exit(null, "Server Tester Was Closed.");
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void statisticsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.ServerStatistics == null)
            {
                this.ServerStatistics = new ServerStatisticsForm();
                this.ServerStatistics.MdiParent = this;
                this.ServerStatistics.Show();
            }
            else
            {
                this.ServerStatistics.Show();
                this.ServerStatistics.WindowState = FormWindowState.Normal;
                this.ServerStatistics.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void catalogueToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.CatalogueMonitor == null)
            {
                this.CatalogueMonitor = new CatalogueMonitorForm();
                this.CatalogueMonitor.MdiParent = this;
                this.CatalogueMonitor.Show();
            }
            else
            {
                this.CatalogueMonitor.Show();
                this.CatalogueMonitor.WindowState = FormWindowState.Normal;
                this.CatalogueMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void controllersToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.ControllersMonitor == null)
            {
                this.ControllersMonitor = new ControllersMonitorForm();
                this.ControllersMonitor.MdiParent = this;
                this.ControllersMonitor.Show();
            }
            else
            {
                this.ControllersMonitor.Show();
                this.ControllersMonitor.WindowState = FormWindowState.Normal;
                this.ControllersMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void eventsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.EventsMonitor == null)
            {
                this.EventsMonitor = new EventsMonitorForm();
                this.EventsMonitor.MdiParent = this;
                this.EventsMonitor.Show();
            }
            else
            {
                this.EventsMonitor.Show();
                this.EventsMonitor.WindowState = FormWindowState.Normal;
                this.EventsMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void pluginsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.PluginsMonitor == null)
            {
                this.PluginsMonitor = new PluginsMonitorForm();
                this.PluginsMonitor.MdiParent = this;
                this.PluginsMonitor.Show();
            }
            else
            {
                this.PluginsMonitor.Show();
                this.PluginsMonitor.WindowState = FormWindowState.Normal;
                this.PluginsMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void roomsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.RoomsMonitor == null)
            {
                this.RoomsMonitor = new RoomsMonitorForm();
                this.RoomsMonitor.MdiParent = this;
                this.RoomsMonitor.Show();
            }
            else
            {
                this.RoomsMonitor.Show();
                this.RoomsMonitor.WindowState = FormWindowState.Normal;
                this.RoomsMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void usersToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.UsersMonitor == null)
            {
                this.UsersMonitor = new UsersMonitorForm();
                this.UsersMonitor.MdiParent = this;
                this.UsersMonitor.Show();
            }
            else
            {
                this.UsersMonitor.Show();
                this.UsersMonitor.WindowState = FormWindowState.Normal;
                this.UsersMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void backendSocketsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.BackendSocketsMonitor == null)
            {
                this.BackendSocketsMonitor = new BackendSocketsMonitorForm();
                this.BackendSocketsMonitor.MdiParent = this;
                this.BackendSocketsMonitor.Show();
            }
            else
            {
                this.BackendSocketsMonitor.Show();
                this.BackendSocketsMonitor.WindowState = FormWindowState.Normal;
                this.BackendSocketsMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void gameSocketsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.GameSocketsMonitor == null)
            {
                this.GameSocketsMonitor = new GameSocketsMonitorForm();
                this.GameSocketsMonitor.MdiParent = this;
                this.GameSocketsMonitor.Show();
            }
            else
            {
                this.GameSocketsMonitor.Show();
                this.GameSocketsMonitor.WindowState = FormWindowState.Normal;
                this.GameSocketsMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void databaseToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.DatabaseMonitor == null)
            {
                this.DatabaseMonitor = new DatabaseMonitorForm();
                this.DatabaseMonitor.MdiParent = this;
                this.DatabaseMonitor.Show();
            }
            else
            {
                this.DatabaseMonitor.Show();
                this.DatabaseMonitor.WindowState = FormWindowState.Normal;
                this.DatabaseMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void loggingToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.LoggingMonitor == null)
            {
                this.LoggingMonitor = new LoggingMonitorForm();
                this.LoggingMonitor.MdiParent = this;
                this.LoggingMonitor.Show();
            }
            else
            {
                this.LoggingMonitor.Show();
                this.LoggingMonitor.WindowState = FormWindowState.Normal;
                this.LoggingMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void settingsToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.SettingsMonitor == null)
            {
                this.SettingsMonitor = new SettingsMonitorForm();
                this.SettingsMonitor.MdiParent = this;
                this.SettingsMonitor.Show();
            }
            else
            {
                this.SettingsMonitor.Show();
                this.SettingsMonitor.WindowState = FormWindowState.Normal;
                this.SettingsMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void threadToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            if (this.ThreadMonitor == null)
            {
                this.ThreadMonitor = new ThreadMonitorForm();
                this.ThreadMonitor.MdiParent = this;
                this.ThreadMonitor.Show();
            }
            else
            {
                this.ThreadMonitor.Show();
                this.ThreadMonitor.WindowState = FormWindowState.Normal;
                this.ThreadMonitor.Focus();
            }
        }

        /// <summary>
        /// Creates a new form for this item.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void createDebugClientToolStripMenuItem_Click(object Sender, EventArgs EventArgs)
        {
            DebugClientForm DebugClient = new DebugClientForm();
            DebugClient.MdiParent = this;
            DebugClient.Show();

            this.activeDebugClientsToolStripMenuItem.DropDownItems.Add("[" + this.DebugClients.Count + "] [Not Logged In] Debug Client");

            this.DebugClients.Add(DebugClient);
        }
    }
}
