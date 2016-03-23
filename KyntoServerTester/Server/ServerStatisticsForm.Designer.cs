namespace KyntoServerTester.Server
{
    partial class ServerStatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series21 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerLaunchTime = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ServerDataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ServerCpuUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ServerRamUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerDataChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerCpuUsage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerRamUsage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ServerLaunchTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Statistics";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Launch Time:";
            // 
            // ServerLaunchTime
            // 
            this.ServerLaunchTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerLaunchTime.Enabled = false;
            this.ServerLaunchTime.Location = new System.Drawing.Point(84, 19);
            this.ServerLaunchTime.Name = "ServerLaunchTime";
            this.ServerLaunchTime.ReadOnly = true;
            this.ServerLaunchTime.Size = new System.Drawing.Size(409, 20);
            this.ServerLaunchTime.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ServerRamUsage);
            this.groupBox2.Controls.Add(this.ServerCpuUsage);
            this.groupBox2.Controls.Add(this.ServerDataChart);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 505);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Charts";
            // 
            // ServerDataChart
            // 
            chartArea7.Name = "ChartArea1";
            this.ServerDataChart.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.ServerDataChart.Legends.Add(legend7);
            this.ServerDataChart.Location = new System.Drawing.Point(6, 19);
            this.ServerDataChart.Name = "ServerDataChart";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Legend = "Legend1";
            series15.Name = "Active Connections";
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series16.Legend = "Legend1";
            series16.Name = "Users Online";
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series17.Legend = "Legend1";
            series17.Name = "Active Rooms";
            series18.ChartArea = "ChartArea1";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series18.Legend = "Legend1";
            series18.Name = "Active Public Rooms";
            series19.ChartArea = "ChartArea1";
            series19.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series19.Legend = "Legend1";
            series19.Name = "Active Private Rooms";
            this.ServerDataChart.Series.Add(series15);
            this.ServerDataChart.Series.Add(series16);
            this.ServerDataChart.Series.Add(series17);
            this.ServerDataChart.Series.Add(series18);
            this.ServerDataChart.Series.Add(series19);
            this.ServerDataChart.Size = new System.Drawing.Size(487, 155);
            this.ServerDataChart.TabIndex = 0;
            this.ServerDataChart.Text = "Server Statistics";
            // 
            // ServerCpuUsage
            // 
            chartArea8.Name = "ChartArea1";
            this.ServerCpuUsage.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.ServerCpuUsage.Legends.Add(legend8);
            this.ServerCpuUsage.Location = new System.Drawing.Point(6, 180);
            this.ServerCpuUsage.Name = "ServerCpuUsage";
            series20.ChartArea = "ChartArea1";
            series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series20.Legend = "Legend1";
            series20.Name = "Cpu Usage";
            this.ServerCpuUsage.Series.Add(series20);
            this.ServerCpuUsage.Size = new System.Drawing.Size(487, 155);
            this.ServerCpuUsage.TabIndex = 1;
            this.ServerCpuUsage.Text = "Server Hardware Usage";
            // 
            // ServerRamUsage
            // 
            chartArea9.Name = "ChartArea1";
            this.ServerRamUsage.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.ServerRamUsage.Legends.Add(legend9);
            this.ServerRamUsage.Location = new System.Drawing.Point(6, 341);
            this.ServerRamUsage.Name = "ServerRamUsage";
            series21.ChartArea = "ChartArea1";
            series21.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series21.Legend = "Legend1";
            series21.Name = "RamUsage";
            this.ServerRamUsage.Series.Add(series21);
            this.ServerRamUsage.Size = new System.Drawing.Size(487, 155);
            this.ServerRamUsage.TabIndex = 2;
            this.ServerRamUsage.Text = "Server Hardware Usage";
            // 
            // ServerStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 585);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServerStatisticsForm";
            this.Text = "Server Statistics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerStatisticsForm_FormClosing);
            this.Load += new System.EventHandler(this.ServerStatisticsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ServerDataChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerCpuUsage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerRamUsage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerLaunchTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ServerDataChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart ServerCpuUsage;
        private System.Windows.Forms.DataVisualization.Charting.Chart ServerRamUsage;
    }
}