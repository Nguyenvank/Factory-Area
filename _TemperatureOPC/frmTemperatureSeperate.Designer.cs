namespace Inventory_Data
{
    partial class frmTemperatureSeperate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemperatureSeperate));
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.temperatureAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temperature1DetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temperature4HorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temperature4SquareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temeratureMSChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.temperatureLiveChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerMain
            // 
            this.timerMain.Enabled = true;
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripStatusLabel1,
            this.tssDateTime,
            this.tssMessage,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 744);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1366, 24);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.temperatureAllToolStripMenuItem,
            this.temperature1DetailToolStripMenuItem,
            this.temperature4HorizontalToolStripMenuItem,
            this.temperature4SquareToolStripMenuItem,
            this.temeratureMSChartToolStripMenuItem,
            this.temperatureLiveChartToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitApplicationToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // temperatureAllToolStripMenuItem
            // 
            this.temperatureAllToolStripMenuItem.Name = "temperatureAllToolStripMenuItem";
            this.temperatureAllToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.temperatureAllToolStripMenuItem.Text = "Temperature All";
            this.temperatureAllToolStripMenuItem.Click += new System.EventHandler(this.temperatureAllToolStripMenuItem_Click);
            // 
            // temperature1DetailToolStripMenuItem
            // 
            this.temperature1DetailToolStripMenuItem.Name = "temperature1DetailToolStripMenuItem";
            this.temperature1DetailToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.temperature1DetailToolStripMenuItem.Text = "Temperature 1 Detail";
            this.temperature1DetailToolStripMenuItem.Click += new System.EventHandler(this.temperature1DetailToolStripMenuItem_Click);
            // 
            // temperature4HorizontalToolStripMenuItem
            // 
            this.temperature4HorizontalToolStripMenuItem.Name = "temperature4HorizontalToolStripMenuItem";
            this.temperature4HorizontalToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.temperature4HorizontalToolStripMenuItem.Text = "Temperature 4 Horizontal";
            this.temperature4HorizontalToolStripMenuItem.Click += new System.EventHandler(this.temperature4HorizontalToolStripMenuItem_Click);
            // 
            // temperature4SquareToolStripMenuItem
            // 
            this.temperature4SquareToolStripMenuItem.Name = "temperature4SquareToolStripMenuItem";
            this.temperature4SquareToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.temperature4SquareToolStripMenuItem.Text = "Temperature 4 Square";
            this.temperature4SquareToolStripMenuItem.Click += new System.EventHandler(this.temperature4SquareToolStripMenuItem_Click);
            // 
            // temeratureMSChartToolStripMenuItem
            // 
            this.temeratureMSChartToolStripMenuItem.Name = "temeratureMSChartToolStripMenuItem";
            this.temeratureMSChartToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.temeratureMSChartToolStripMenuItem.Text = "Temerature MS Chart";
            this.temeratureMSChartToolStripMenuItem.Click += new System.EventHandler(this.temeratureMSChartToolStripMenuItem_Click);
            // 
            // temperatureLiveChartToolStripMenuItem
            // 
            this.temperatureLiveChartToolStripMenuItem.Name = "temperatureLiveChartToolStripMenuItem";
            this.temperatureLiveChartToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.temperatureLiveChartToolStripMenuItem.Text = "Temperature LiveChart";
            this.temperatureLiveChartToolStripMenuItem.Click += new System.EventHandler(this.temperatureLiveChartToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitApplicationToolStripMenuItem.Text = "Exit Application";
            this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(293, 19);
            this.toolStripStatusLabel1.Text = "RUBBER MACHINES TEMPERATURE STATUS";
            // 
            // tssDateTime
            // 
            this.tssDateTime.Name = "tssDateTime";
            this.tssDateTime.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.tssDateTime.Size = new System.Drawing.Size(158, 19);
            this.tssDateTime.Text = "toolStripStatusLabel2";
            // 
            // tssMessage
            // 
            this.tssMessage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tssMessage.Name = "tssMessage";
            this.tssMessage.Size = new System.Drawing.Size(677, 19);
            this.tssMessage.Spring = true;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(194, 19);
            this.toolStripStatusLabel4.Text = "DONG-A HWASUNG VINA";
            // 
            // pnl_Main
            // 
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 0);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(1366, 768);
            this.pnl_Main.TabIndex = 1;
            // 
            // frmTemperatureSeperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "frmTemperatureSeperate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TEMPERATURE MONITORING";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTemperatureSeperate_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTemperatureSeperate_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssDateTime;
        private System.Windows.Forms.ToolStripStatusLabel tssMessage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.ToolStripMenuItem temperatureAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temperature1DetailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temperature4HorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temperature4SquareToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temeratureMSChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem temperatureLiveChartToolStripMenuItem;
    }
}