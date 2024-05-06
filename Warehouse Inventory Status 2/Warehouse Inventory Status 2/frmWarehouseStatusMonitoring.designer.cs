namespace Inventory_Data
{
    partial class frmWarehouseStatusMonitoring
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWarehouseStatusMonitoring));
        	this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        	this.dgvWHStatus = new System.Windows.Forms.DataGridView();
        	this.lblDate = new System.Windows.Forms.Label();
        	this.lblTitle = new System.Windows.Forms.Label();
        	this.lblTime = new System.Windows.Forms.Label();
        	this.cbbWarehouse = new System.Windows.Forms.ComboBox();
        	this.dtpDataDate = new System.Windows.Forms.DateTimePicker();
        	this.btnDisplay = new System.Windows.Forms.Button();
        	this.btnExcel = new System.Windows.Forms.Button();
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.timer2 = new System.Windows.Forms.Timer(this.components);
        	this.tableLayoutPanel1.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.dgvWHStatus)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// tableLayoutPanel1
        	// 
        	this.tableLayoutPanel1.ColumnCount = 25;
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.Controls.Add(this.dgvWHStatus, 0, 2);
        	this.tableLayoutPanel1.Controls.Add(this.lblDate, 0, 0);
        	this.tableLayoutPanel1.Controls.Add(this.lblTitle, 5, 0);
        	this.tableLayoutPanel1.Controls.Add(this.lblTime, 0, 1);
        	this.tableLayoutPanel1.Controls.Add(this.cbbWarehouse, 22, 0);
        	this.tableLayoutPanel1.Controls.Add(this.dtpDataDate, 20, 0);
        	this.tableLayoutPanel1.Controls.Add(this.btnDisplay, 22, 1);
        	this.tableLayoutPanel1.Controls.Add(this.btnExcel, 20, 1);
        	this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        	this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
        	this.tableLayoutPanel1.Name = "tableLayoutPanel1";
        	this.tableLayoutPanel1.RowCount = 25;
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
        	this.tableLayoutPanel1.Size = new System.Drawing.Size(1350, 729);
        	this.tableLayoutPanel1.TabIndex = 0;
        	// 
        	// dgvWHStatus
        	// 
        	this.dgvWHStatus.AllowUserToAddRows = false;
        	this.dgvWHStatus.AllowUserToDeleteRows = false;
        	this.dgvWHStatus.AllowUserToResizeColumns = false;
        	this.dgvWHStatus.AllowUserToResizeRows = false;
        	this.dgvWHStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        	this.tableLayoutPanel1.SetColumnSpan(this.dgvWHStatus, 25);
        	this.dgvWHStatus.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.dgvWHStatus.Location = new System.Drawing.Point(3, 61);
        	this.dgvWHStatus.Name = "dgvWHStatus";
        	this.dgvWHStatus.ReadOnly = true;
        	this.dgvWHStatus.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        	this.tableLayoutPanel1.SetRowSpan(this.dgvWHStatus, 23);
        	this.dgvWHStatus.RowTemplate.Height = 25;
        	this.dgvWHStatus.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        	this.dgvWHStatus.Size = new System.Drawing.Size(1344, 665);
        	this.dgvWHStatus.TabIndex = 1;
        	this.dgvWHStatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWHStatus_CellClick);
        	this.dgvWHStatus.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWHStatus_CellValueChanged);
        	this.dgvWHStatus.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvWHStatus_DataBindingComplete);
        	// 
        	// lblDate
        	// 
        	this.lblDate.AutoSize = true;
        	this.tableLayoutPanel1.SetColumnSpan(this.lblDate, 5);
        	this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.lblDate.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lblDate.Location = new System.Drawing.Point(3, 3);
        	this.lblDate.Margin = new System.Windows.Forms.Padding(3);
        	this.lblDate.Name = "lblDate";
        	this.lblDate.Size = new System.Drawing.Size(264, 23);
        	this.lblDate.TabIndex = 2;
        	this.lblDate.Text = "label1";
        	this.lblDate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        	// 
        	// lblTitle
        	// 
        	this.lblTitle.AutoSize = true;
        	this.tableLayoutPanel1.SetColumnSpan(this.lblTitle, 15);
        	this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lblTitle.Location = new System.Drawing.Point(273, 3);
        	this.lblTitle.Margin = new System.Windows.Forms.Padding(3);
        	this.lblTitle.Name = "lblTitle";
        	this.tableLayoutPanel1.SetRowSpan(this.lblTitle, 2);
        	this.lblTitle.Size = new System.Drawing.Size(804, 52);
        	this.lblTitle.TabIndex = 2;
        	this.lblTitle.Text = "MATERIAL INVENTORY STATUS";
        	this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// lblTime
        	// 
        	this.lblTime.AutoSize = true;
        	this.tableLayoutPanel1.SetColumnSpan(this.lblTime, 5);
        	this.lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.lblTime.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lblTime.Location = new System.Drawing.Point(3, 32);
        	this.lblTime.Margin = new System.Windows.Forms.Padding(3);
        	this.lblTime.Name = "lblTime";
        	this.lblTime.Size = new System.Drawing.Size(264, 23);
        	this.lblTime.TabIndex = 2;
        	this.lblTime.Text = "label1";
        	this.lblTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        	// 
        	// cbbWarehouse
        	// 
        	this.tableLayoutPanel1.SetColumnSpan(this.cbbWarehouse, 3);
        	this.cbbWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.cbbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.cbbWarehouse.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.cbbWarehouse.FormattingEnabled = true;
        	this.cbbWarehouse.Location = new System.Drawing.Point(1191, 3);
        	this.cbbWarehouse.Name = "cbbWarehouse";
        	this.cbbWarehouse.Size = new System.Drawing.Size(156, 27);
        	this.cbbWarehouse.TabIndex = 3;
        	this.cbbWarehouse.SelectionChangeCommitted += new System.EventHandler(this.cbbWarehouse_SelectionChangeCommitted);
        	// 
        	// dtpDataDate
        	// 
        	this.tableLayoutPanel1.SetColumnSpan(this.dtpDataDate, 2);
        	this.dtpDataDate.CustomFormat = "dd/MM/yyyy";
        	this.dtpDataDate.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.dtpDataDate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.dtpDataDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        	this.dtpDataDate.Location = new System.Drawing.Point(1083, 3);
        	this.dtpDataDate.Name = "dtpDataDate";
        	this.dtpDataDate.Size = new System.Drawing.Size(102, 26);
        	this.dtpDataDate.TabIndex = 6;
        	this.dtpDataDate.Visible = false;
        	// 
        	// btnDisplay
        	// 
        	this.btnDisplay.BackColor = System.Drawing.Color.LightSeaGreen;
        	this.tableLayoutPanel1.SetColumnSpan(this.btnDisplay, 3);
        	this.btnDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.btnDisplay.FlatStyle = System.Windows.Forms.FlatStyle.System;
        	this.btnDisplay.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.btnDisplay.Location = new System.Drawing.Point(1190, 31);
        	this.btnDisplay.Margin = new System.Windows.Forms.Padding(2);
        	this.btnDisplay.Name = "btnDisplay";
        	this.btnDisplay.Size = new System.Drawing.Size(158, 25);
        	this.btnDisplay.TabIndex = 5;
        	this.btnDisplay.Text = "Display Data";
        	this.btnDisplay.UseVisualStyleBackColor = false;
        	this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
        	// 
        	// btnExcel
        	// 
        	this.btnExcel.BackColor = System.Drawing.Color.LightSeaGreen;
        	this.tableLayoutPanel1.SetColumnSpan(this.btnExcel, 2);
        	this.btnExcel.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.System;
        	this.btnExcel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.btnExcel.Location = new System.Drawing.Point(1082, 31);
        	this.btnExcel.Margin = new System.Windows.Forms.Padding(2);
        	this.btnExcel.Name = "btnExcel";
        	this.btnExcel.Size = new System.Drawing.Size(104, 25);
        	this.btnExcel.TabIndex = 5;
        	this.btnExcel.Text = "EXCEL";
        	this.btnExcel.UseVisualStyleBackColor = false;
        	this.btnExcel.Visible = false;
        	// 
        	// timer1
        	// 
        	this.timer1.Enabled = true;
        	this.timer1.Interval = 1000;
        	this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        	// 
        	// timer2
        	// 
        	this.timer2.Enabled = true;
        	this.timer2.Interval = 5000;
        	this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
        	// 
        	// frmWarehouseStatusMonitoring
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(1350, 729);
        	this.Controls.Add(this.tableLayoutPanel1);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MinimumSize = new System.Drawing.Size(1366, 768);
        	this.Name = "frmWarehouseStatusMonitoring";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "MATERIAL INVENTORY STATUS 2";
        	this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        	this.Load += new System.EventHandler(this.frmWarehouseStatusMonitoring_Load);
        	this.tableLayoutPanel1.ResumeLayout(false);
        	this.tableLayoutPanel1.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.dgvWHStatus)).EndInit();
        	this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvWHStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ComboBox cbbWarehouse;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DateTimePicker dtpDataDate;
        private System.Windows.Forms.Button btnDisplay;
    }
}