namespace Inventory_Data
{
    partial class frmWarehouseInventoryGatheringData_v2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWarehouseInventoryGatheringData_v2));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblMatNextUpdate = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.chkMat60 = new System.Windows.Forms.CheckBox();
            this.dtpMatNight = new System.Windows.Forms.DateTimePicker();
            this.dtpMatDay = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblInvNextUpdate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.chkInv60 = new System.Windows.Forms.CheckBox();
            this.dtpInvNight = new System.Windows.Forms.DateTimePicker();
            this.dtpInvDay = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpMatInsert = new System.Windows.Forms.DateTimePicker();
            this.dtpInvInsert = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkInvActive = new System.Windows.Forms.CheckBox();
            this.chkMatActive = new System.Windows.Forms.CheckBox();
            this.chkMat30 = new System.Windows.Forms.CheckBox();
            this.chkInv30 = new System.Windows.Forms.CheckBox();
            this.lblTestTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Update Inventory Software";
            this.notifyIcon.BalloonTipTitle = "Update Inventory Software";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Update software still running on system";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // lblMatNextUpdate
            // 
            this.lblMatNextUpdate.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMatNextUpdate, 4);
            this.lblMatNextUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMatNextUpdate.Location = new System.Drawing.Point(435, 363);
            this.lblMatNextUpdate.Margin = new System.Windows.Forms.Padding(3);
            this.lblMatNextUpdate.Name = "lblMatNextUpdate";
            this.lblMatNextUpdate.Size = new System.Drawing.Size(210, 24);
            this.lblMatNextUpdate.TabIndex = 1;
            this.lblMatNextUpdate.Text = "By hours";
            this.lblMatNextUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label15, 4);
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(435, 333);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(210, 24);
            this.label15.TabIndex = 1;
            this.label15.Text = "Next time update";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkMat60
            // 
            this.chkMat60.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.chkMat60, 2);
            this.chkMat60.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMat60.Location = new System.Drawing.Point(435, 303);
            this.chkMat60.Name = "chkMat60";
            this.chkMat60.Size = new System.Drawing.Size(102, 24);
            this.chkMat60.TabIndex = 4;
            this.chkMat60.Text = "60 mins";
            this.chkMat60.UseVisualStyleBackColor = true;
            this.chkMat60.CheckedChanged += new System.EventHandler(this.chkMat60_CheckedChanged);
            // 
            // dtpMatNight
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtpMatNight, 2);
            this.dtpMatNight.CustomFormat = "HH:mm tt";
            this.dtpMatNight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpMatNight.Enabled = false;
            this.dtpMatNight.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMatNight.Location = new System.Drawing.Point(543, 273);
            this.dtpMatNight.Name = "dtpMatNight";
            this.dtpMatNight.ShowUpDown = true;
            this.dtpMatNight.Size = new System.Drawing.Size(102, 26);
            this.dtpMatNight.TabIndex = 3;
            this.dtpMatNight.Value = new System.DateTime(2018, 4, 17, 7, 59, 0, 0);
            // 
            // dtpMatDay
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtpMatDay, 2);
            this.dtpMatDay.CustomFormat = "HH:mm tt";
            this.dtpMatDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpMatDay.Enabled = false;
            this.dtpMatDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMatDay.Location = new System.Drawing.Point(435, 273);
            this.dtpMatDay.Name = "dtpMatDay";
            this.dtpMatDay.ShowUpDown = true;
            this.dtpMatDay.Size = new System.Drawing.Size(102, 26);
            this.dtpMatDay.TabIndex = 3;
            this.dtpMatDay.Value = new System.DateTime(2018, 4, 17, 19, 59, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label8, 2);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(543, 243);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 24);
            this.label8.TabIndex = 1;
            this.label8.Text = "Night update";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label9, 2);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(435, 243);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 24);
            this.label9.TabIndex = 1;
            this.label9.Text = "Day update";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label10, 4);
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(433, 121);
            this.label10.Margin = new System.Windows.Forms.Padding(1);
            this.label10.Name = "label10";
            this.tableLayoutPanel1.SetRowSpan(this.label10, 3);
            this.label10.Size = new System.Drawing.Size(214, 88);
            this.label10.TabIndex = 1;
            this.label10.Text = "MATERIAL";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInvNextUpdate
            // 
            this.lblInvNextUpdate.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblInvNextUpdate, 4);
            this.lblInvNextUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInvNextUpdate.Location = new System.Drawing.Point(57, 363);
            this.lblInvNextUpdate.Margin = new System.Windows.Forms.Padding(3);
            this.lblInvNextUpdate.Name = "lblInvNextUpdate";
            this.lblInvNextUpdate.Size = new System.Drawing.Size(210, 24);
            this.lblInvNextUpdate.TabIndex = 1;
            this.lblInvNextUpdate.Text = "By hours";
            this.lblInvNextUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label14, 4);
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(57, 333);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(210, 24);
            this.label14.TabIndex = 1;
            this.label14.Text = "Next time update";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkInv60
            // 
            this.chkInv60.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.chkInv60, 2);
            this.chkInv60.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkInv60.Location = new System.Drawing.Point(57, 303);
            this.chkInv60.Name = "chkInv60";
            this.chkInv60.Size = new System.Drawing.Size(102, 24);
            this.chkInv60.TabIndex = 4;
            this.chkInv60.Text = "60 mins";
            this.chkInv60.UseVisualStyleBackColor = true;
            this.chkInv60.CheckedChanged += new System.EventHandler(this.chkInv60_CheckedChanged);
            // 
            // dtpInvNight
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtpInvNight, 2);
            this.dtpInvNight.CustomFormat = "HH:mm tt";
            this.dtpInvNight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpInvNight.Enabled = false;
            this.dtpInvNight.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvNight.Location = new System.Drawing.Point(165, 273);
            this.dtpInvNight.Name = "dtpInvNight";
            this.dtpInvNight.ShowUpDown = true;
            this.dtpInvNight.Size = new System.Drawing.Size(102, 26);
            this.dtpInvNight.TabIndex = 3;
            this.dtpInvNight.Value = new System.DateTime(2018, 4, 17, 7, 58, 0, 0);
            // 
            // dtpInvDay
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtpInvDay, 2);
            this.dtpInvDay.CustomFormat = "HH:mm tt";
            this.dtpInvDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpInvDay.Enabled = false;
            this.dtpInvDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvDay.Location = new System.Drawing.Point(57, 273);
            this.dtpInvDay.Name = "dtpInvDay";
            this.dtpInvDay.ShowUpDown = true;
            this.dtpInvDay.Size = new System.Drawing.Size(102, 26);
            this.dtpInvDay.TabIndex = 3;
            this.dtpInvDay.Value = new System.DateTime(2018, 4, 17, 19, 58, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label4, 2);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(165, 243);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "Night update";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(57, 243);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Day update";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstLog
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lstLog, 7);
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 16;
            this.lstLog.Location = new System.Drawing.Point(705, 3);
            this.lstLog.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lstLog.Name = "lstLog";
            this.tableLayoutPanel1.SetRowSpan(this.lstLog, 15);
            this.lstLog.Size = new System.Drawing.Size(372, 452);
            this.lstLog.TabIndex = 5;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMessage, 13);
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 423);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(696, 29);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Program is auto runing by time...";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 4);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(55, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(1);
            this.label5.Name = "label5";
            this.tableLayoutPanel1.SetRowSpan(this.label5, 3);
            this.label5.Size = new System.Drawing.Size(214, 88);
            this.label5.TabIndex = 1;
            this.label5.Text = "INVENTORY";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTime, 6);
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(381, 63);
            this.lblTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(318, 24);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "label1";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblDate, 6);
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(3, 63);
            this.lblDate.Margin = new System.Windows.Forms.Padding(3);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(318, 24);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "label1";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 13);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 93);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(696, 24);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.OrangeRed;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.tableLayoutPanel2.SetRowSpan(this.label3, 2);
            this.label3.Size = new System.Drawing.Size(690, 2);
            this.label3.TabIndex = 1;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 13);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.tableLayoutPanel1.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(696, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "AUTO-UPDATING INVENTORY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 20;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTime, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.lstLog, 13, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 8, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 8, 8);
            this.tableLayoutPanel1.Controls.Add(this.label8, 10, 8);
            this.tableLayoutPanel1.Controls.Add(this.dtpMatDay, 8, 9);
            this.tableLayoutPanel1.Controls.Add(this.dtpMatNight, 10, 9);
            this.tableLayoutPanel1.Controls.Add(this.label15, 8, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblMatNextUpdate, 8, 12);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.dtpInvNight, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.dtpInvDay, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.chkInv60, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label14, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblInvNextUpdate, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.dtpMatInsert, 10, 7);
            this.tableLayoutPanel1.Controls.Add(this.dtpInvInsert, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.label6, 8, 7);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.chkInvActive, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.chkMatActive, 8, 13);
            this.tableLayoutPanel1.Controls.Add(this.chkMat60, 8, 10);
            this.tableLayoutPanel1.Controls.Add(this.chkMat30, 10, 10);
            this.tableLayoutPanel1.Controls.Add(this.chkInv30, 3, 10);
            this.tableLayoutPanel1.Controls.Add(this.lblTestTime, 5, 11);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1080, 455);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dtpMatInsert
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtpMatInsert, 2);
            this.dtpMatInsert.CustomFormat = "HH:mm tt";
            this.dtpMatInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpMatInsert.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMatInsert.Location = new System.Drawing.Point(543, 213);
            this.dtpMatInsert.Name = "dtpMatInsert";
            this.dtpMatInsert.ShowUpDown = true;
            this.dtpMatInsert.Size = new System.Drawing.Size(102, 26);
            this.dtpMatInsert.TabIndex = 3;
            this.dtpMatInsert.Value = new System.DateTime(2018, 4, 17, 19, 59, 0, 0);
            // 
            // dtpInvInsert
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.dtpInvInsert, 2);
            this.dtpInvInsert.CustomFormat = "HH:mm tt";
            this.dtpInvInsert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpInvInsert.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvInsert.Location = new System.Drawing.Point(165, 213);
            this.dtpInvInsert.Name = "dtpInvInsert";
            this.dtpInvInsert.ShowUpDown = true;
            this.dtpInvInsert.Size = new System.Drawing.Size(102, 26);
            this.dtpInvInsert.TabIndex = 3;
            this.dtpInvInsert.Value = new System.DateTime(2018, 4, 17, 19, 58, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(435, 213);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "Insert";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label7, 2);
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(57, 213);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 24);
            this.label7.TabIndex = 1;
            this.label7.Text = "Insert";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkInvActive
            // 
            this.chkInvActive.AutoSize = true;
            this.chkInvActive.Checked = true;
            this.chkInvActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel1.SetColumnSpan(this.chkInvActive, 4);
            this.chkInvActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkInvActive.Location = new System.Drawing.Point(57, 393);
            this.chkInvActive.Name = "chkInvActive";
            this.chkInvActive.Size = new System.Drawing.Size(210, 24);
            this.chkInvActive.TabIndex = 6;
            this.chkInvActive.Text = "Inventory Active";
            this.chkInvActive.UseVisualStyleBackColor = true;
            this.chkInvActive.CheckedChanged += new System.EventHandler(this.chkInvActive_CheckedChanged);
            // 
            // chkMatActive
            // 
            this.chkMatActive.AutoSize = true;
            this.chkMatActive.Checked = true;
            this.chkMatActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel1.SetColumnSpan(this.chkMatActive, 4);
            this.chkMatActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMatActive.Location = new System.Drawing.Point(435, 393);
            this.chkMatActive.Name = "chkMatActive";
            this.chkMatActive.Size = new System.Drawing.Size(210, 24);
            this.chkMatActive.TabIndex = 6;
            this.chkMatActive.Text = "Material Active";
            this.chkMatActive.UseVisualStyleBackColor = true;
            this.chkMatActive.CheckedChanged += new System.EventHandler(this.chkMatActive_CheckedChanged);
            // 
            // chkMat30
            // 
            this.chkMat30.AutoSize = true;
            this.chkMat30.Checked = true;
            this.chkMat30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel1.SetColumnSpan(this.chkMat30, 2);
            this.chkMat30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMat30.Location = new System.Drawing.Point(543, 303);
            this.chkMat30.Name = "chkMat30";
            this.chkMat30.Size = new System.Drawing.Size(102, 24);
            this.chkMat30.TabIndex = 4;
            this.chkMat30.Text = "30 mins";
            this.chkMat30.UseVisualStyleBackColor = true;
            this.chkMat30.CheckedChanged += new System.EventHandler(this.chkMat30_CheckedChanged);
            // 
            // chkInv30
            // 
            this.chkInv30.AutoSize = true;
            this.chkInv30.Checked = true;
            this.chkInv30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel1.SetColumnSpan(this.chkInv30, 2);
            this.chkInv30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkInv30.Location = new System.Drawing.Point(165, 303);
            this.chkInv30.Name = "chkInv30";
            this.chkInv30.Size = new System.Drawing.Size(102, 24);
            this.chkInv30.TabIndex = 4;
            this.chkInv30.Text = "30 mins";
            this.chkInv30.UseVisualStyleBackColor = true;
            this.chkInv30.CheckedChanged += new System.EventHandler(this.chkInv30_CheckedChanged);
            // 
            // lblTestTime
            // 
            this.lblTestTime.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTestTime, 3);
            this.lblTestTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTestTime.Location = new System.Drawing.Point(273, 333);
            this.lblTestTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblTestTime.Name = "lblTestTime";
            this.tableLayoutPanel1.SetRowSpan(this.lblTestTime, 3);
            this.lblTestTime.Size = new System.Drawing.Size(156, 84);
            this.lblTestTime.TabIndex = 7;
            this.lblTestTime.Text = "label11";
            this.lblTestTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmWarehouseInventoryGatheringData_v2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 455);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1096, 493);
            this.Name = "frmWarehouseInventoryGatheringData_v2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWarehouseInventoryGatheringData_v2";
            this.Load += new System.EventHandler(this.frmWarehouseInventoryGatheringData_v2_Load);
            this.Resize += new System.EventHandler(this.frmWarehouseInventoryGatheringData_v2_Resize);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lblMatNextUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpMatDay;
        private System.Windows.Forms.DateTimePicker dtpMatNight;
        private System.Windows.Forms.CheckBox chkMat60;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpInvNight;
        private System.Windows.Forms.DateTimePicker dtpInvDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkInv60;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblInvNextUpdate;
        private System.Windows.Forms.DateTimePicker dtpMatInsert;
        private System.Windows.Forms.DateTimePicker dtpInvInsert;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkInvActive;
        private System.Windows.Forms.CheckBox chkMatActive;
        private System.Windows.Forms.CheckBox chkMat30;
        private System.Windows.Forms.CheckBox chkInv30;
        private System.Windows.Forms.Label lblTestTime;
    }
}