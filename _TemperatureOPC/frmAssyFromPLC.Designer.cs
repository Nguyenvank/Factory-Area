﻿namespace Fan_1
{
    partial class frmAssyFromPLC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssyFromPLC));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAB02OK = new System.Windows.Forms.TextBox();
            this.txtAB03OK = new System.Windows.Forms.TextBox();
            this.txtDI01OK = new System.Windows.Forms.TextBox();
            this.txtDI02OK = new System.Windows.Forms.TextBox();
            this.txtPU01OK = new System.Windows.Forms.TextBox();
            this.txtWB01OK = new System.Windows.Forms.TextBox();
            this.txtBL01OK = new System.Windows.Forms.TextBox();
            this.txtWB02OK = new System.Windows.Forms.TextBox();
            this.txtBL02OK = new System.Windows.Forms.TextBox();
            this.txtWB03OK = new System.Windows.Forms.TextBox();
            this.txtMX01OK = new System.Windows.Forms.TextBox();
            this.txtWD01OK = new System.Windows.Forms.TextBox();
            this.txtWD02OK = new System.Windows.Forms.TextBox();
            this.txtPU01NG = new System.Windows.Forms.TextBox();
            this.txtDI02NG = new System.Windows.Forms.TextBox();
            this.txtDI01NG = new System.Windows.Forms.TextBox();
            this.txtAB03NG = new System.Windows.Forms.TextBox();
            this.txtAB02NG = new System.Windows.Forms.TextBox();
            this.txtAB01NG = new System.Windows.Forms.TextBox();
            this.txtAB01OK = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkConnect = new System.Windows.Forms.CheckBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.chkOperate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWB04OK = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label14, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.label15, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.label8, 6, 12);
            this.tableLayoutPanel1.Controls.Add(this.label9, 6, 13);
            this.tableLayoutPanel1.Controls.Add(this.label12, 6, 14);
            this.tableLayoutPanel1.Controls.Add(this.label13, 1, 17);
            this.tableLayoutPanel1.Controls.Add(this.label16, 1, 18);
            this.tableLayoutPanel1.Controls.Add(this.txtAB02OK, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtAB03OK, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtDI01OK, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtDI02OK, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtPU01OK, 3, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtWB01OK, 3, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtBL01OK, 8, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtWB02OK, 3, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtBL02OK, 8, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtWB03OK, 3, 14);
            this.tableLayoutPanel1.Controls.Add(this.txtMX01OK, 8, 14);
            this.tableLayoutPanel1.Controls.Add(this.txtWD01OK, 3, 17);
            this.tableLayoutPanel1.Controls.Add(this.txtWD02OK, 3, 18);
            this.tableLayoutPanel1.Controls.Add(this.txtPU01NG, 8, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtDI02NG, 8, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtDI01NG, 8, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtAB03NG, 8, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtAB02NG, 8, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtAB01NG, 8, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtAB01OK, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 6, 16);
            this.tableLayoutPanel1.Controls.Add(this.chkConnect, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMessage, 0, 19);
            this.tableLayoutPanel1.Controls.Add(this.chkOperate, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 6, 18);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.txtWB04OK, 3, 15);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 20;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(461, 548);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 10);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.tableLayoutPanel1.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(455, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASSEMBLY DATA FROM OPC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label4, 2);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(49, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "A/Balance 01:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(49, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "A/Balance 02:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(49, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "A/Balance 03:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label10, 2);
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(49, 192);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Dispenser 01:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label11, 2);
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(49, 219);
            this.label11.Margin = new System.Windows.Forms.Padding(3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Dispenser 02:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label14, 2);
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(49, 273);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 21);
            this.label14.TabIndex = 0;
            this.label14.Text = "Pump 01:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label17, 2);
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(49, 381);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 21);
            this.label17.TabIndex = 0;
            this.label17.Text = "W/B 03:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label15, 2);
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(49, 354);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 21);
            this.label15.TabIndex = 0;
            this.label15.Text = "W/B 02:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label7, 2);
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 327);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "W/B 01:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label8, 2);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(279, 327);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "Blower 01:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label9, 2);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(279, 354);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "Blower 02:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label12, 2);
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(279, 381);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Mixing:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label13, 2);
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(49, 462);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "Welding 01:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label16, 2);
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(49, 489);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "Welding 02:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAB02OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAB02OK, 2);
            this.txtAB02OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB02OK.Location = new System.Drawing.Point(141, 111);
            this.txtAB02OK.Name = "txtAB02OK";
            this.txtAB02OK.ReadOnly = true;
            this.txtAB02OK.Size = new System.Drawing.Size(86, 26);
            this.txtAB02OK.TabIndex = 4;
            this.txtAB02OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAB02OK.TextChanged += new System.EventHandler(this.txtAB02OK_TextChanged);
            // 
            // txtAB03OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAB03OK, 2);
            this.txtAB03OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB03OK.Location = new System.Drawing.Point(141, 138);
            this.txtAB03OK.Name = "txtAB03OK";
            this.txtAB03OK.ReadOnly = true;
            this.txtAB03OK.Size = new System.Drawing.Size(86, 26);
            this.txtAB03OK.TabIndex = 6;
            this.txtAB03OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAB03OK.TextChanged += new System.EventHandler(this.txtAB03OK_TextChanged);
            // 
            // txtDI01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtDI01OK, 2);
            this.txtDI01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDI01OK.Location = new System.Drawing.Point(141, 192);
            this.txtDI01OK.Name = "txtDI01OK";
            this.txtDI01OK.ReadOnly = true;
            this.txtDI01OK.Size = new System.Drawing.Size(86, 26);
            this.txtDI01OK.TabIndex = 8;
            this.txtDI01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDI01OK.TextChanged += new System.EventHandler(this.txtDI01OK_TextChanged);
            // 
            // txtDI02OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtDI02OK, 2);
            this.txtDI02OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDI02OK.Location = new System.Drawing.Point(141, 219);
            this.txtDI02OK.Name = "txtDI02OK";
            this.txtDI02OK.ReadOnly = true;
            this.txtDI02OK.Size = new System.Drawing.Size(86, 26);
            this.txtDI02OK.TabIndex = 10;
            this.txtDI02OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDI02OK.TextChanged += new System.EventHandler(this.txtDI02OK_TextChanged);
            // 
            // txtPU01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtPU01OK, 2);
            this.txtPU01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPU01OK.Location = new System.Drawing.Point(141, 273);
            this.txtPU01OK.Name = "txtPU01OK";
            this.txtPU01OK.ReadOnly = true;
            this.txtPU01OK.Size = new System.Drawing.Size(86, 26);
            this.txtPU01OK.TabIndex = 12;
            this.txtPU01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPU01OK.TextChanged += new System.EventHandler(this.txtPU01OK_TextChanged);
            // 
            // txtWB01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtWB01OK, 2);
            this.txtWB01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWB01OK.Location = new System.Drawing.Point(141, 327);
            this.txtWB01OK.Name = "txtWB01OK";
            this.txtWB01OK.ReadOnly = true;
            this.txtWB01OK.Size = new System.Drawing.Size(86, 26);
            this.txtWB01OK.TabIndex = 14;
            this.txtWB01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWB01OK.TextChanged += new System.EventHandler(this.txtWB01OK_TextChanged);
            // 
            // txtBL01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBL01OK, 2);
            this.txtBL01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBL01OK.Location = new System.Drawing.Point(371, 327);
            this.txtBL01OK.Name = "txtBL01OK";
            this.txtBL01OK.ReadOnly = true;
            this.txtBL01OK.Size = new System.Drawing.Size(87, 26);
            this.txtBL01OK.TabIndex = 17;
            this.txtBL01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBL01OK.TextChanged += new System.EventHandler(this.txtBL01OK_TextChanged);
            // 
            // txtWB02OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtWB02OK, 2);
            this.txtWB02OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWB02OK.Location = new System.Drawing.Point(141, 354);
            this.txtWB02OK.Name = "txtWB02OK";
            this.txtWB02OK.ReadOnly = true;
            this.txtWB02OK.Size = new System.Drawing.Size(86, 26);
            this.txtWB02OK.TabIndex = 15;
            this.txtWB02OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWB02OK.TextChanged += new System.EventHandler(this.txtWB02OK_TextChanged);
            // 
            // txtBL02OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBL02OK, 2);
            this.txtBL02OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBL02OK.Location = new System.Drawing.Point(371, 354);
            this.txtBL02OK.Name = "txtBL02OK";
            this.txtBL02OK.ReadOnly = true;
            this.txtBL02OK.Size = new System.Drawing.Size(87, 26);
            this.txtBL02OK.TabIndex = 18;
            this.txtBL02OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBL02OK.TextChanged += new System.EventHandler(this.txtBL02OK_TextChanged);
            // 
            // txtWB03OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtWB03OK, 2);
            this.txtWB03OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWB03OK.Location = new System.Drawing.Point(141, 381);
            this.txtWB03OK.Name = "txtWB03OK";
            this.txtWB03OK.ReadOnly = true;
            this.txtWB03OK.Size = new System.Drawing.Size(86, 26);
            this.txtWB03OK.TabIndex = 16;
            this.txtWB03OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWB03OK.TextChanged += new System.EventHandler(this.txtWB03OK_TextChanged);
            // 
            // txtMX01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtMX01OK, 2);
            this.txtMX01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMX01OK.Location = new System.Drawing.Point(371, 381);
            this.txtMX01OK.Name = "txtMX01OK";
            this.txtMX01OK.ReadOnly = true;
            this.txtMX01OK.Size = new System.Drawing.Size(87, 26);
            this.txtMX01OK.TabIndex = 19;
            this.txtMX01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMX01OK.TextChanged += new System.EventHandler(this.txtMX01OK_TextChanged);
            // 
            // txtWD01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtWD01OK, 2);
            this.txtWD01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWD01OK.Location = new System.Drawing.Point(141, 462);
            this.txtWD01OK.Name = "txtWD01OK";
            this.txtWD01OK.ReadOnly = true;
            this.txtWD01OK.Size = new System.Drawing.Size(86, 26);
            this.txtWD01OK.TabIndex = 20;
            this.txtWD01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWD01OK.TextChanged += new System.EventHandler(this.txtWD01OK_TextChanged);
            // 
            // txtWD02OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtWD02OK, 2);
            this.txtWD02OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWD02OK.Location = new System.Drawing.Point(141, 489);
            this.txtWD02OK.Name = "txtWD02OK";
            this.txtWD02OK.ReadOnly = true;
            this.txtWD02OK.Size = new System.Drawing.Size(86, 26);
            this.txtWD02OK.TabIndex = 21;
            this.txtWD02OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWD02OK.TextChanged += new System.EventHandler(this.txtWD02OK_TextChanged);
            // 
            // txtPU01NG
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtPU01NG, 2);
            this.txtPU01NG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPU01NG.Location = new System.Drawing.Point(371, 273);
            this.txtPU01NG.Name = "txtPU01NG";
            this.txtPU01NG.ReadOnly = true;
            this.txtPU01NG.Size = new System.Drawing.Size(87, 26);
            this.txtPU01NG.TabIndex = 13;
            this.txtPU01NG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPU01NG.TextChanged += new System.EventHandler(this.txtPU01NG_TextChanged);
            // 
            // txtDI02NG
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtDI02NG, 2);
            this.txtDI02NG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDI02NG.Location = new System.Drawing.Point(371, 219);
            this.txtDI02NG.Name = "txtDI02NG";
            this.txtDI02NG.ReadOnly = true;
            this.txtDI02NG.Size = new System.Drawing.Size(87, 26);
            this.txtDI02NG.TabIndex = 11;
            this.txtDI02NG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDI02NG.TextChanged += new System.EventHandler(this.txtDI02NG_TextChanged);
            // 
            // txtDI01NG
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtDI01NG, 2);
            this.txtDI01NG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDI01NG.Location = new System.Drawing.Point(371, 192);
            this.txtDI01NG.Name = "txtDI01NG";
            this.txtDI01NG.ReadOnly = true;
            this.txtDI01NG.Size = new System.Drawing.Size(87, 26);
            this.txtDI01NG.TabIndex = 9;
            this.txtDI01NG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDI01NG.TextChanged += new System.EventHandler(this.txtDI01NG_TextChanged);
            // 
            // txtAB03NG
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAB03NG, 2);
            this.txtAB03NG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB03NG.Location = new System.Drawing.Point(371, 138);
            this.txtAB03NG.Name = "txtAB03NG";
            this.txtAB03NG.ReadOnly = true;
            this.txtAB03NG.Size = new System.Drawing.Size(87, 26);
            this.txtAB03NG.TabIndex = 7;
            this.txtAB03NG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAB03NG.TextChanged += new System.EventHandler(this.txtAB03NG_TextChanged);
            // 
            // txtAB02NG
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAB02NG, 2);
            this.txtAB02NG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB02NG.Location = new System.Drawing.Point(371, 111);
            this.txtAB02NG.Name = "txtAB02NG";
            this.txtAB02NG.ReadOnly = true;
            this.txtAB02NG.Size = new System.Drawing.Size(87, 26);
            this.txtAB02NG.TabIndex = 5;
            this.txtAB02NG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAB02NG.TextChanged += new System.EventHandler(this.txtAB02NG_TextChanged);
            // 
            // txtAB01NG
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAB01NG, 2);
            this.txtAB01NG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB01NG.Location = new System.Drawing.Point(371, 84);
            this.txtAB01NG.Name = "txtAB01NG";
            this.txtAB01NG.ReadOnly = true;
            this.txtAB01NG.Size = new System.Drawing.Size(87, 26);
            this.txtAB01NG.TabIndex = 3;
            this.txtAB01NG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAB01NG.TextChanged += new System.EventHandler(this.txtAB01NG_TextChanged);
            // 
            // txtAB01OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAB01OK, 2);
            this.txtAB01OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAB01OK.Location = new System.Drawing.Point(141, 84);
            this.txtAB01OK.Name = "txtAB01OK";
            this.txtAB01OK.ReadOnly = true;
            this.txtAB01OK.Size = new System.Drawing.Size(86, 26);
            this.txtAB01OK.TabIndex = 2;
            this.txtAB01OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAB01OK.TextChanged += new System.EventHandler(this.txtAB01OK_TextChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblStatus, 4);
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(279, 435);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatus.Name = "lblStatus";
            this.tableLayoutPanel1.SetRowSpan(this.lblStatus, 2);
            this.lblStatus.Size = new System.Drawing.Size(179, 48);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Disconnected";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkConnect
            // 
            this.chkConnect.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.chkConnect, 4);
            this.chkConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkConnect.Location = new System.Drawing.Point(49, 57);
            this.chkConnect.Name = "chkConnect";
            this.chkConnect.Size = new System.Drawing.Size(178, 21);
            this.chkConnect.TabIndex = 1;
            this.chkConnect.Text = "Connect to OPC";
            this.chkConnect.UseVisualStyleBackColor = true;
            this.chkConnect.CheckedChanged += new System.EventHandler(this.chkConnect_CheckedChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblMessage, 10);
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 516);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(455, 29);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkOperate
            // 
            this.chkOperate.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.chkOperate, 4);
            this.chkOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOperate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOperate.Location = new System.Drawing.Point(279, 57);
            this.chkOperate.Name = "chkOperate";
            this.chkOperate.Size = new System.Drawing.Size(179, 21);
            this.chkOperate.TabIndex = 22;
            this.chkOperate.Text = "MONITORING";
            this.chkOperate.UseVisualStyleBackColor = true;
            this.chkOperate.Visible = false;
            this.chkOperate.CheckedChanged += new System.EventHandler(this.chkOperate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 489);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Disconnected";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 408);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "W/B 04:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWB04OK
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtWB04OK, 2);
            this.txtWB04OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWB04OK.Location = new System.Drawing.Point(141, 408);
            this.txtWB04OK.Name = "txtWB04OK";
            this.txtWB04OK.ReadOnly = true;
            this.txtWB04OK.Size = new System.Drawing.Size(86, 26);
            this.txtWB04OK.TabIndex = 16;
            this.txtWB04OK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtWB04OK.TextChanged += new System.EventHandler(this.txtWB04OK_TextChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Gathering data still running";
            this.notifyIcon.BalloonTipTitle = "Assembly Data from OPC";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Assembly Data from OPC";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // frmAssyFromPLC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 548);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmAssyFromPLC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Garthering Data From OPC";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAssyFromPLC_Load);
            this.Resize += new System.EventHandler(this.frmAssyFromPLC_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAB02OK;
        private System.Windows.Forms.TextBox txtAB03OK;
        private System.Windows.Forms.TextBox txtDI01OK;
        private System.Windows.Forms.TextBox txtDI02OK;
        private System.Windows.Forms.TextBox txtPU01OK;
        private System.Windows.Forms.TextBox txtWB01OK;
        private System.Windows.Forms.TextBox txtBL01OK;
        private System.Windows.Forms.TextBox txtWB02OK;
        private System.Windows.Forms.TextBox txtBL02OK;
        private System.Windows.Forms.TextBox txtWB03OK;
        private System.Windows.Forms.TextBox txtMX01OK;
        private System.Windows.Forms.TextBox txtWD01OK;
        private System.Windows.Forms.TextBox txtWD02OK;
        private System.Windows.Forms.TextBox txtPU01NG;
        private System.Windows.Forms.TextBox txtDI02NG;
        private System.Windows.Forms.TextBox txtDI01NG;
        private System.Windows.Forms.TextBox txtAB03NG;
        private System.Windows.Forms.TextBox txtAB02NG;
        private System.Windows.Forms.TextBox txtAB01NG;
        private System.Windows.Forms.TextBox txtAB01OK;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkConnect;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox chkOperate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWB04OK;
    }
}