namespace QueryDevice
{
    partial class QueryDevice
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
            this.buttonView = new System.Windows.Forms.Button();
            this.textLogfile = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textResult = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkRI = new System.Windows.Forms.CheckBox();
            this.checkDCD = new System.Windows.Forms.CheckBox();
            this.checkDSR = new System.Windows.Forms.CheckBox();
            this.checkCTS = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.checkRTS = new System.Windows.Forms.CheckBox();
            this.checkDTR = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listReceived = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboSWFlow = new System.Windows.Forms.ComboBox();
            this.comboFormat = new System.Windows.Forms.ComboBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboHWFlow = new System.Windows.Forms.ComboBox();
            this.comboSpeed = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textData = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textComponentInfo = new System.Windows.Forms.Label();
            this.textLicenseInfo = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonView
            // 
            this.buttonView.Location = new System.Drawing.Point(472, 42);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(80, 21);
            this.buttonView.TabIndex = 4;
            this.buttonView.Text = "&View";
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // textLogfile
            // 
            this.textLogfile.Location = new System.Drawing.Point(95, 42);
            this.textLogfile.Name = "textLogfile";
            this.textLogfile.Size = new System.Drawing.Size(371, 20);
            this.textLogfile.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonView);
            this.groupBox3.Controls.Add(this.textLogfile);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textResult);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(12, 379);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(560, 74);
            this.groupBox3.TabIndex = 46;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Result and Logging";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "&Logfile:";
            // 
            // textResult
            // 
            this.textResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textResult.ForeColor = System.Drawing.Color.Black;
            this.textResult.Location = new System.Drawing.Point(95, 19);
            this.textResult.Name = "textResult";
            this.textResult.Size = new System.Drawing.Size(457, 20);
            this.textResult.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(15, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Res&ult:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkRI);
            this.groupBox5.Controls.Add(this.checkDCD);
            this.groupBox5.Controls.Add(this.checkDSR);
            this.groupBox5.Controls.Add(this.checkCTS);
            this.groupBox5.Location = new System.Drawing.Point(12, 327);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(560, 50);
            this.groupBox5.TabIndex = 48;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Hardware (Flow) Control Lines";
            // 
            // checkRI
            // 
            this.checkRI.Enabled = false;
            this.checkRI.Location = new System.Drawing.Point(460, 16);
            this.checkRI.Name = "checkRI";
            this.checkRI.Size = new System.Drawing.Size(66, 24);
            this.checkRI.TabIndex = 3;
            this.checkRI.Text = "RI";
            // 
            // checkDCD
            // 
            this.checkDCD.Enabled = false;
            this.checkDCD.Location = new System.Drawing.Point(338, 16);
            this.checkDCD.Name = "checkDCD";
            this.checkDCD.Size = new System.Drawing.Size(104, 24);
            this.checkDCD.TabIndex = 2;
            this.checkDCD.Text = "DCD";
            // 
            // checkDSR
            // 
            this.checkDSR.Enabled = false;
            this.checkDSR.Location = new System.Drawing.Point(216, 16);
            this.checkDSR.Name = "checkDSR";
            this.checkDSR.Size = new System.Drawing.Size(104, 24);
            this.checkDSR.TabIndex = 1;
            this.checkDSR.Text = "DSR";
            // 
            // checkCTS
            // 
            this.checkCTS.Enabled = false;
            this.checkCTS.Location = new System.Drawing.Point(94, 16);
            this.checkCTS.Name = "checkCTS";
            this.checkCTS.Size = new System.Drawing.Size(104, 24);
            this.checkCTS.TabIndex = 0;
            this.checkCTS.Text = "CTS";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(220, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(331, 30);
            this.label11.TabIndex = 6;
            this.label11.Text = "(signals can be used to set a flow control line, independent from flow control. S" +
                "ignals are for advanced use only)";
            // 
            // checkRTS
            // 
            this.checkRTS.Location = new System.Drawing.Point(148, 41);
            this.checkRTS.Name = "checkRTS";
            this.checkRTS.Size = new System.Drawing.Size(104, 24);
            this.checkRTS.TabIndex = 5;
            this.checkRTS.Text = "RTS";
            this.checkRTS.CheckedChanged += new System.EventHandler(this.checkRTS_CheckedChanged);
            // 
            // checkDTR
            // 
            this.checkDTR.Location = new System.Drawing.Point(95, 41);
            this.checkDTR.Name = "checkDTR";
            this.checkDTR.Size = new System.Drawing.Size(104, 24);
            this.checkDTR.TabIndex = 4;
            this.checkDTR.Text = "DTR";
            this.checkDTR.CheckedChanged += new System.EventHandler(this.checkDTR_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.listReceived);
            this.groupBox4.Location = new System.Drawing.Point(12, 192);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(560, 133);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Receive";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "Received:";
            // 
            // listReceived
            // 
            this.listReceived.Location = new System.Drawing.Point(95, 20);
            this.listReceived.Name = "listReceived";
            this.listReceived.Size = new System.Drawing.Size(456, 95);
            this.listReceived.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboSWFlow);
            this.groupBox1.Controls.Add(this.comboFormat);
            this.groupBox1.Controls.Add(this.buttonUpdate);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.buttonClose);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboHWFlow);
            this.groupBox1.Controls.Add(this.comboSpeed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboDevice);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 98);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device";
            // 
            // comboSWFlow
            // 
            this.comboSWFlow.Location = new System.Drawing.Point(338, 64);
            this.comboSWFlow.Name = "comboSWFlow";
            this.comboSWFlow.Size = new System.Drawing.Size(128, 21);
            this.comboSWFlow.TabIndex = 12;
            // 
            // comboFormat
            // 
            this.comboFormat.Location = new System.Drawing.Point(338, 40);
            this.comboFormat.Name = "comboFormat";
            this.comboFormat.Size = new System.Drawing.Size(128, 21);
            this.comboFormat.TabIndex = 11;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(473, 64);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(80, 21);
            this.buttonUpdate.TabIndex = 10;
            this.buttonUpdate.Text = "&Update";
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(249, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "&SW FlowCtrl:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(250, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Data &Format:";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(473, 40);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(80, 21);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "&Close";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(473, 16);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(80, 21);
            this.buttonOpen.TabIndex = 6;
            this.buttonOpen.Text = "&Open";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "&HW FlowCtrl:";
            // 
            // comboHWFlow
            // 
            this.comboHWFlow.Location = new System.Drawing.Point(95, 64);
            this.comboHWFlow.Name = "comboHWFlow";
            this.comboHWFlow.Size = new System.Drawing.Size(128, 21);
            this.comboHWFlow.TabIndex = 4;
            // 
            // comboSpeed
            // 
            this.comboSpeed.Location = new System.Drawing.Point(95, 40);
            this.comboSpeed.Name = "comboSpeed";
            this.comboSpeed.Size = new System.Drawing.Size(128, 21);
            this.comboSpeed.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "S&peed:";
            // 
            // comboDevice
            // 
            this.comboDevice.DisplayMember = "scsd";
            this.comboDevice.Location = new System.Drawing.Point(95, 16);
            this.comboDevice.Name = "comboDevice";
            this.comboDevice.Size = new System.Drawing.Size(371, 21);
            this.comboDevice.TabIndex = 1;
            this.comboDevice.ValueMember = "scsd";
            this.comboDevice.SelectionChangeCommitted += new System.EventHandler(this.comboDevice_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Name:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 23);
            this.label5.TabIndex = 2;
            this.label5.Text = "D&ata:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(13, 495);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(559, 14);
            this.label12.TabIndex = 49;
            this.label12.Text = "Source code for this application can be found in the \'Samples\' folder in the inst" +
                "allation directory.";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(15, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 3;
            this.label10.Text = "Si&gnals:";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(473, 17);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(80, 21);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "&Submit";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textData
            // 
            this.textData.Location = new System.Drawing.Point(95, 17);
            this.textData.Name = "textData";
            this.textData.Size = new System.Drawing.Size(371, 20);
            this.textData.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.checkRTS);
            this.groupBox2.Controls.Add(this.checkDTR);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonSend);
            this.groupBox2.Controls.Add(this.textData);
            this.groupBox2.Location = new System.Drawing.Point(12, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 79);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send";
            // 
            // textComponentInfo
            // 
            this.textComponentInfo.Location = new System.Drawing.Point(13, 463);
            this.textComponentInfo.Name = "textComponentInfo";
            this.textComponentInfo.Size = new System.Drawing.Size(559, 14);
            this.textComponentInfo.TabIndex = 50;
            this.textComponentInfo.Text = "ComponentInfo";
            // 
            // textLicenseInfo
            // 
            this.textLicenseInfo.Location = new System.Drawing.Point(13, 479);
            this.textLicenseInfo.Name = "textLicenseInfo";
            this.textLicenseInfo.Size = new System.Drawing.Size(559, 14);
            this.textLicenseInfo.TabIndex = 51;
            this.textLicenseInfo.Text = "LicenseInfo";
            // 
            // QueryDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 518);
            this.Controls.Add(this.textLicenseInfo);
            this.Controls.Add(this.textComponentInfo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryDevice";
            this.Text = "Query Device - Based on ActiveXperts Serial Port Component";
            this.Load += new System.EventHandler(this.QueryDevice_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.TextBox textLogfile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label textResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkRI;
        private System.Windows.Forms.CheckBox checkDCD;
        private System.Windows.Forms.CheckBox checkDSR;
        private System.Windows.Forms.CheckBox checkCTS;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkRTS;
        private System.Windows.Forms.CheckBox checkDTR;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listReceived;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboSWFlow;
        private System.Windows.Forms.ComboBox comboFormat;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboHWFlow;
        private System.Windows.Forms.ComboBox comboSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label textComponentInfo;
        private System.Windows.Forms.Label textLicenseInfo;
    }
}

