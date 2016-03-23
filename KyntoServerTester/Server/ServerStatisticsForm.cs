using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace KyntoServerTester.Server
{
    /// <summary>
    /// The form used to display key server statistics.
    /// </summary>
    public partial class ServerStatisticsForm : Form
    {
        /// <summary>
        /// Stores if the form has closed or not.
        /// </summary>
        private bool _HasClosed = false;

        /// <summary>
        /// Stores the thread used to update the charts.
        /// </summary>
        private Thread _UpdateChartsThread;

        /// <summary>
        /// Initialises this form.
        /// </summary>
        public ServerStatisticsForm()
        {
            // Initialises the gui.
            InitializeComponent();
        }

        /// <summary>
        /// The form load event handler.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void ServerStatisticsForm_Load(object Sender, EventArgs EventArgs)
        {
            // Global stats.
            this.ServerLaunchTime.Text = KyntoServer.Program.LaunchTime.ToString();

            // Create thread to update charts.
            this._UpdateChartsThread = new Thread(new ThreadStart(UpdateCharts));
            this._UpdateChartsThread.Start();
        }

        /// <summary>
        /// Updates the charts.
        /// </summary>
        private void UpdateCharts()
        {
            // Create performance monitors.
            PerformanceCounter CpuCounter;
            CpuCounter = new PerformanceCounter();
            CpuCounter.CategoryName = "Processor";
            CpuCounter.CounterName = "% Processor Time";
            CpuCounter.InstanceName = "_Total";

            // Loop while open.
            while (!this._HasClosed)
            {
                // Precompute for reuse.
                TimeSpan CurrentTimespan = (DateTime.Now - KyntoServer.Program.LaunchTime);
                string Time = Math.Floor(CurrentTimespan.TotalHours) + ":" + CurrentTimespan.Minutes + ":" + CurrentTimespan.Seconds;

                // Update the chartz!
                this.ServerDataChart.Invoke(new MethodInvoker(delegate
                {
                    // Remove points to the left if theres more than 8 items.
                    if (this.ServerDataChart.Series[0].Points.Count >= 8)
                    {
                        this.ServerDataChart.Series[0].Points.RemoveAt(0);
                    }

                    if (this.ServerDataChart.Series[1].Points.Count >= 8)
                    {
                        this.ServerDataChart.Series[1].Points.RemoveAt(0);
                    }

                    if (this.ServerDataChart.Series[2].Points.Count >= 8)
                    {
                        this.ServerDataChart.Series[2].Points.RemoveAt(0);
                    }

                    if (this.ServerDataChart.Series[3].Points.Count >= 8)
                    {
                        this.ServerDataChart.Series[3].Points.RemoveAt(0);
                    }

                    if (this.ServerDataChart.Series[4].Points.Count >= 8)
                    {
                        this.ServerDataChart.Series[4].Points.RemoveAt(0);
                    }

                    try
                    {
                        // Active connections.
                        this.ServerDataChart.Series[0].Points.AddXY(Time, KyntoServer.Program.ServerManager.GameSocketsService.ActiveConnections);

                        // Users online.
                        this.ServerDataChart.Series[1].Points.AddXY(Time, KyntoServer.Program.ServerManager.UsersManager.Users.Count);

                        // Active rooms.
                        this.ServerDataChart.Series[2].Points.AddXY(Time, KyntoServer.Program.ServerManager.RoomsManager.PrivateRooms.Count + KyntoServer.Program.ServerManager.RoomsManager.PublicRooms.Count);

                        // Active public rooms.
                        this.ServerDataChart.Series[3].Points.AddXY(Time, KyntoServer.Program.ServerManager.RoomsManager.PublicRooms.Count);

                        // Active private rooms.
                        this.ServerDataChart.Series[4].Points.AddXY(Time, KyntoServer.Program.ServerManager.RoomsManager.PrivateRooms.Count);
                    }
                    catch
                    {
                    }
                }));
                this.ServerCpuUsage.Invoke(new MethodInvoker(delegate
                {
                    // Remove points to the left if theres more than 9 items.
                    if (this.ServerCpuUsage.Series[0].Points.Count >= 8)
                    {
                        this.ServerCpuUsage.Series[0].Points.RemoveAt(0);
                    }

                    try
                    {
                        // Cpu usage.
                        this.ServerCpuUsage.Series[0].Points.AddXY(Time, CpuCounter.NextValue());
                    }
                    catch
                    {
                    }
                }));
                this.ServerRamUsage.Invoke(new MethodInvoker(delegate
                {
                    // Remove points to the left if theres more than 9 items.
                    if (this.ServerRamUsage.Series[0].Points.Count >= 8)
                    {
                        this.ServerRamUsage.Series[0].Points.RemoveAt(0);
                    }

                    try
                    {
                        // Memory usage.
                        this.ServerRamUsage.Series[0].Points.AddXY(Time, ((double)(GC.GetTotalMemory(false)) / 1000000));
                    }
                    catch
                    {
                    }
                }));

                // Sleep for 1 second.
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// The form closed event handler.
        /// </summary>
        /// <param name="Sender">The sender object.</param>
        /// <param name="EventArgs">The event arguments.</param>
        private void ServerStatisticsForm_FormClosing(object Sender, FormClosingEventArgs EventArgs)
        {
            this._HasClosed = true;
        }
    }
}
