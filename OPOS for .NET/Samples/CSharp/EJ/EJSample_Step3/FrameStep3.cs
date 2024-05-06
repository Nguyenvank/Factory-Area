using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.PointOfService;
using System.Globalization;
using System.Reflection;

namespace EJSample_Step3
{
    public class FrameStep3 : Form
    {
        //<<Step1>>--Start
        /// <summary>
        /// PosPrinter Service Object
        /// </summary>
        PosPrinter m_Printer = null;

        /// <summary>
        /// ElectronicJournal Service Object
        /// </summary>
        ElectronicJournal m_ElectronicJournal = null;

        private CheckBox chDataEvent;
        private CheckBox chAsync;
        private Label lblEndMarker;
        private Label lblStartMarker;
        private Label lblFile;
        private TextBox txtEndMarker;
        private TextBox txtStartMarker;
        private TextBox txtFileName;
        private Button btnQueryContent;
        private TextBox txtPrintContent;
        private Button btnPrintContent;
        private Button btnBrowse;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrameStep3());
        }

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
            this.grpBoxReceiptPrinter = new System.Windows.Forms.GroupBox();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.gpBoxEJ = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPrintContent = new System.Windows.Forms.TextBox();
            this.btnPrintContent = new System.Windows.Forms.Button();
            this.lblEndMarker = new System.Windows.Forms.Label();
            this.lblStartMarker = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtEndMarker = new System.Windows.Forms.TextBox();
            this.txtStartMarker = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnQueryContent = new System.Windows.Forms.Button();
            this.chDataEvent = new System.Windows.Forms.CheckBox();
            this.chAsync = new System.Windows.Forms.CheckBox();
            this.txtMarker = new System.Windows.Forms.TextBox();
            this.chStorageEnabled = new System.Windows.Forms.CheckBox();
            this.btnAddMarker = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpBoxReceiptPrinter.SuspendLayout();
            this.gpBoxEJ.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxReceiptPrinter
            // 
            this.grpBoxReceiptPrinter.Controls.Add(this.btnPrintReceipt);
            this.grpBoxReceiptPrinter.Location = new System.Drawing.Point(13, 12);
            this.grpBoxReceiptPrinter.Name = "grpBoxReceiptPrinter";
            this.grpBoxReceiptPrinter.Size = new System.Drawing.Size(405, 57);
            this.grpBoxReceiptPrinter.TabIndex = 0;
            this.grpBoxReceiptPrinter.TabStop = false;
            this.grpBoxReceiptPrinter.Text = "Receipt Printer";
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.Location = new System.Drawing.Point(111, 20);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(149, 23);
            this.btnPrintReceipt.TabIndex = 0;
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = true;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // gpBoxEJ
            // 
            this.gpBoxEJ.Controls.Add(this.btnBrowse);
            this.gpBoxEJ.Controls.Add(this.txtPrintContent);
            this.gpBoxEJ.Controls.Add(this.btnPrintContent);
            this.gpBoxEJ.Controls.Add(this.lblEndMarker);
            this.gpBoxEJ.Controls.Add(this.lblStartMarker);
            this.gpBoxEJ.Controls.Add(this.lblFile);
            this.gpBoxEJ.Controls.Add(this.txtEndMarker);
            this.gpBoxEJ.Controls.Add(this.txtStartMarker);
            this.gpBoxEJ.Controls.Add(this.txtFileName);
            this.gpBoxEJ.Controls.Add(this.btnQueryContent);
            this.gpBoxEJ.Controls.Add(this.chDataEvent);
            this.gpBoxEJ.Controls.Add(this.chAsync);
            this.gpBoxEJ.Controls.Add(this.txtMarker);
            this.gpBoxEJ.Controls.Add(this.chStorageEnabled);
            this.gpBoxEJ.Controls.Add(this.btnAddMarker);
            this.gpBoxEJ.Controls.Add(this.groupBox1);
            this.gpBoxEJ.Controls.Add(this.groupBox2);
            this.gpBoxEJ.Controls.Add(this.groupBox3);
            this.gpBoxEJ.Location = new System.Drawing.Point(13, 75);
            this.gpBoxEJ.Name = "gpBoxEJ";
            this.gpBoxEJ.Size = new System.Drawing.Size(405, 333);
            this.gpBoxEJ.TabIndex = 1;
            this.gpBoxEJ.TabStop = false;
            this.gpBoxEJ.Text = "Electronic Journal";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(364, 283);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 14;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtPrintContent
            // 
            this.txtPrintContent.Location = new System.Drawing.Point(175, 285);
            this.txtPrintContent.Name = "txtPrintContent";
            this.txtPrintContent.Size = new System.Drawing.Size(183, 20);
            this.txtPrintContent.TabIndex = 13;
            this.txtPrintContent.Text = "ElectronicJournalFile1.bin";
            // 
            // btnPrintContent
            // 
            this.btnPrintContent.Location = new System.Drawing.Point(23, 283);
            this.btnPrintContent.Name = "btnPrintContent";
            this.btnPrintContent.Size = new System.Drawing.Size(136, 23);
            this.btnPrintContent.TabIndex = 12;
            this.btnPrintContent.Text = "Print File";
            this.btnPrintContent.UseVisualStyleBackColor = true;
            this.btnPrintContent.Click += new System.EventHandler(this.btnPrintContent_Click);
            // 
            // lblEndMarker
            // 
            this.lblEndMarker.AutoSize = true;
            this.lblEndMarker.Location = new System.Drawing.Point(172, 216);
            this.lblEndMarker.Name = "lblEndMarker";
            this.lblEndMarker.Size = new System.Drawing.Size(62, 13);
            this.lblEndMarker.TabIndex = 11;
            this.lblEndMarker.Text = "End Marker";
            // 
            // lblStartMarker
            // 
            this.lblStartMarker.AutoSize = true;
            this.lblStartMarker.Location = new System.Drawing.Point(172, 173);
            this.lblStartMarker.Name = "lblStartMarker";
            this.lblStartMarker.Size = new System.Drawing.Size(65, 13);
            this.lblStartMarker.TabIndex = 10;
            this.lblStartMarker.Text = "Start Marker";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(172, 131);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(54, 13);
            this.lblFile.TabIndex = 9;
            this.lblFile.Text = "File Name";
            // 
            // txtEndMarker
            // 
            this.txtEndMarker.Location = new System.Drawing.Point(175, 232);
            this.txtEndMarker.Name = "txtEndMarker";
            this.txtEndMarker.Size = new System.Drawing.Size(183, 20);
            this.txtEndMarker.TabIndex = 8;
            this.txtEndMarker.Text = "MarkerByStep3";
            // 
            // txtStartMarker
            // 
            this.txtStartMarker.Location = new System.Drawing.Point(175, 189);
            this.txtStartMarker.Name = "txtStartMarker";
            this.txtStartMarker.Size = new System.Drawing.Size(183, 20);
            this.txtStartMarker.TabIndex = 7;
            this.txtStartMarker.Text = "MarkerByStep1";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(175, 147);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(183, 20);
            this.txtFileName.TabIndex = 6;
            this.txtFileName.Text = "ElectronicJournalFile1.bin";
            // 
            // btnQueryContent
            // 
            this.btnQueryContent.Location = new System.Drawing.Point(23, 186);
            this.btnQueryContent.Name = "btnQueryContent";
            this.btnQueryContent.Size = new System.Drawing.Size(136, 23);
            this.btnQueryContent.TabIndex = 5;
            this.btnQueryContent.Text = "Query Content";
            this.btnQueryContent.UseVisualStyleBackColor = true;
            this.btnQueryContent.Click += new System.EventHandler(this.btnQueryContent_Click);
            // 
            // chDataEvent
            // 
            this.chDataEvent.AutoSize = true;
            this.chDataEvent.Location = new System.Drawing.Point(175, 31);
            this.chDataEvent.Name = "chDataEvent";
            this.chDataEvent.Size = new System.Drawing.Size(119, 17);
            this.chDataEvent.TabIndex = 4;
            this.chDataEvent.Text = "DataEvent Enabled";
            this.chDataEvent.UseVisualStyleBackColor = true;
            this.chDataEvent.CheckedChanged += new System.EventHandler(this.chDataEvent_CheckedChanged);
            // 
            // chAsync
            // 
            this.chAsync.AutoSize = true;
            this.chAsync.Location = new System.Drawing.Point(175, 54);
            this.chAsync.Name = "chAsync";
            this.chAsync.Size = new System.Drawing.Size(85, 17);
            this.chAsync.TabIndex = 3;
            this.chAsync.Text = "Async Mode";
            this.chAsync.UseVisualStyleBackColor = true;
            this.chAsync.CheckedChanged += new System.EventHandler(this.chAsync_CheckedChanged);
            // 
            // txtMarker
            // 
            this.txtMarker.Location = new System.Drawing.Point(175, 89);
            this.txtMarker.Name = "txtMarker";
            this.txtMarker.Size = new System.Drawing.Size(183, 20);
            this.txtMarker.TabIndex = 2;
            this.txtMarker.Text = "MarkerByStep3";
            // 
            // chStorageEnabled
            // 
            this.chStorageEnabled.AutoSize = true;
            this.chStorageEnabled.Checked = true;
            this.chStorageEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chStorageEnabled.Location = new System.Drawing.Point(23, 31);
            this.chStorageEnabled.Name = "chStorageEnabled";
            this.chStorageEnabled.Size = new System.Drawing.Size(105, 17);
            this.chStorageEnabled.TabIndex = 1;
            this.chStorageEnabled.Text = "Storage Enabled";
            this.chStorageEnabled.UseVisualStyleBackColor = true;
            this.chStorageEnabled.CheckedChanged += new System.EventHandler(this.chStorageEnabled_CheckedChanged);
            // 
            // btnAddMarker
            // 
            this.btnAddMarker.Location = new System.Drawing.Point(23, 87);
            this.btnAddMarker.Name = "btnAddMarker";
            this.btnAddMarker.Size = new System.Drawing.Size(136, 23);
            this.btnAddMarker.TabIndex = 0;
            this.btnAddMarker.Text = "Add Marker";
            this.btnAddMarker.UseVisualStyleBackColor = true;
            this.btnAddMarker.Click += new System.EventHandler(this.btnAddMarker_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(10, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 46);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(10, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 139);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(10, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 50);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(260, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrameStep3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 457);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gpBoxEJ);
            this.Controls.Add(this.grpBoxReceiptPrinter);
            this.Name = "FrameStep3";
            this.Text = "Step 3 - Extract Content to a Separate file and print from file. ";
            this.Load += new System.EventHandler(this.FrameStep3_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrameStep3_Closing);
            this.grpBoxReceiptPrinter.ResumeLayout(false);
            this.gpBoxEJ.ResumeLayout(false);
            this.gpBoxEJ.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxReceiptPrinter;
        private System.Windows.Forms.Button btnPrintReceipt;
        private System.Windows.Forms.GroupBox gpBoxEJ;
        private System.Windows.Forms.Button btnAddMarker;
        private System.Windows.Forms.TextBox txtMarker;
        private System.Windows.Forms.CheckBox chStorageEnabled;
        private System.Windows.Forms.Button btnClose;
        public FrameStep3()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  The method "Print Receipt" calls some other methods.
        ///  This includes methods for starting and ending the "rotation print mode", and for printing.
        /// This method is very similar to Step 8 in the PosPrinter Sample Programs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {

            //Initialization
            DateTime nowDate = DateTime.Now;							//System date
            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
            dateFormat.MonthDayPattern = "MMMM";
            string strDate = nowDate.ToString("MMMM,dd,yyyy  HH:mm", dateFormat);
            int iRecLineSpacing;
            int iRecLineHeight;
            DialogResult dialogResult;
            bool bBuffering = true;

            Cursor.Current = Cursors.WaitCursor;

            iRecLineSpacing = m_Printer.RecLineSpacing;
            iRecLineHeight = m_Printer.RecLineHeight;

            if (m_Printer.CapRecPresent == true)
            {
                while (true)
                {

                    try
                    {

                        m_Printer.TransactionPrint(PrinterStation.Receipt
                            , PrinterTransactionControl.Transaction);

                        m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Left90);

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|4C" + "\u001b|bC"
                            + "   Receipt     ");

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|3C" + "\u001b|2uC"
                            + "       Mr. Brawn\n");

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|2uC"
                            + "                                                  \n\n");

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|2uC" + "\u001b|3C"
                            + "        Total payment              $" + "\u001b|4C" + "21.00  \n");

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1C\n");

                        m_Printer.PrintNormal(PrinterStation.Receipt, strDate + " Received\n\n");

                        m_Printer.RecLineHeight = 24;
                        m_Printer.RecLineSpacing = m_Printer.RecLineHeight;

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                            + " Details               \n");

                        m_Printer.PrintNormal(PrinterStation.Receipt
                            , "\u001b|1C" + "                          " + "\u001b|2C" + "OPOS Store\n");

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                            + " Tax excluded    $20.00\n");

                        m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|1C"
                            + "                          " + "\u001b|bC" + "Zip code 999-9999\n");

                        //m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                        //    + " Tax(5%)        $1.00" + "\u001b|N" + "    Phone#(9999)99-9998\n");

                        break;
                    }
                    catch (PosControlException ex)
                    {
                        if (ex.ErrorCode == ErrorCode.Illegal && ex.ErrorCodeExtended == 1004)
                        {
                            MessageBox.Show("Unable to print receipt.\n" + ex.Message
                                , "EJSample_Step3", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Clear the buffered data since the buffer retains print data when an error occurs during printing.
                            m_Printer.ClearOutput();
                            bBuffering = false;
                            break;
                        }

                        // When error occurs, display a message to ask the user whether retry or not.
                        dialogResult = MessageBox.Show("Fails to output to a printer.\n\nRetry?"
                            , "EJSample_Step3", MessageBoxButtons.AbortRetryIgnore);

                        try
                        {
                            // Clear the buffered data since the buffer retains print data when an error occurs during printing.
                            m_Printer.ClearOutput();
                        }
                        catch (PosControlException)
                        {
                        }

                        if (dialogResult == DialogResult.Abort || dialogResult == DialogResult.Ignore)
                        {
                            break;
                        }

                        continue;
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot use a Receipt Stateion.", "EJSample_Step3"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Normal);

            if (bBuffering == true)
            {
                m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
            }

            while (m_Printer.State != ControlState.Idle)
            {
                try
                {
                    System.Threading.Thread.Sleep(100);
                }
                catch (Exception)
                {
                }
            }

            try
            {
                m_Printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal);
            }
            catch (PosControlException ex)
            {
                // Clear the buffered data since the buffer retains print data when an error occurs during printing.
                m_Printer.ClearOutput();
                MessageBox.Show("Unable to print receipt.\n" + ex.Message, "EJSample_Step3",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            Cursor.Current = Cursors.Default;
            m_Printer.RecLineSpacing = iRecLineSpacing;
            m_Printer.RecLineHeight = iRecLineHeight;
        }

        /// <summary>
        /// A method that sets the StorageEnabled property of ElectronicJournal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chStorageEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_ElectronicJournal.StorageEnabled = chStorageEnabled.Checked;
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to set StorageEnabled.\n" + ex.ErrorCode + "\n" + ex.Message
                    , "EJSample_Step3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// A method that Adds a Marker to ElectronicJournal. The Marker text is as given in the text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            try
            {
                m_ElectronicJournal.AddMarker(txtMarker.Text);
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to Add Marker.\n" + ex.ErrorCode + "\n" + ex.Message
                    , "EJSample_Step3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Loads the form. Open/Claim/Enable PosPrinter and ElectronicJournal. Sets StorageEnabled to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameStep3_Load(object sender, EventArgs e)
        {
            //<<<step1>>>--Start
            //Use a Logical Device Name which has been set on the SetupPOS.
            string strLogicalName = "PosPrinter";

            //Create PosExplorer
            PosExplorer posExplorer = new PosExplorer();

            DeviceInfo deviceInfo = null;

            try
            {
                deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName);
            }
            catch (Exception)
            {
                MessageBox.Show("Fails to get printer device information.", "EJSample_Step3");
                //Nothing can be used.
                ChangePrintButtonStatus();
                goto LoadEJ;
            }
            try
            {
                m_Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
            }
            catch (Exception)
            {
                MessageBox.Show("Fails to create PosPrinter instance.", "EJSample_Step3");
                //Nothing can be used.
                ChangePrintButtonStatus();
                goto LoadEJ;

            }

            try
            {
                //Open the device
                m_Printer.Open();
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to open the PosPrinter device.", "EJSample_Step3");

                //Nothing can be used.
                ChangePrintButtonStatus();
                goto LoadEJ;
            }

            try
            {
                //Get the exclusive control right for the opened device.
                //Then the device is disable from other application.
                m_Printer.Claim(1000);
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to claim the POSprinter device.", "EJSample_Step3");

                //Nothing can be used.
                ChangePrintButtonStatus();
                goto LoadEJ;
            }

            try
            {
                //Enable the device.
                m_Printer.DeviceEnabled = true;
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to enable to PosPrinter device.", "EJSample_Step3");
                //Nothing can be used.
                ChangePrintButtonStatus();
                goto LoadEJ;
            }

            LoadEJ:

            //Initialize EJ
            string strEJLogicalName = "ElectronicJournal";

            try
            {
                deviceInfo = posExplorer.GetDevice(DeviceType.ElectronicJournal, strEJLogicalName);
            }
            catch (Exception)
            {
                MessageBox.Show("Fails to get Electronic Journal device information.", "EJSample_Step3");
                ChangeEJButtonStatus();
                return;
            }
            try
            {
                m_ElectronicJournal = (ElectronicJournal)posExplorer.CreateInstance(deviceInfo);
            }
            catch (Exception)
            {
                MessageBox.Show("Fails to create ElectronicJournal instance.", "EJSample_Step3");
                ChangeEJButtonStatus();
                return;
            }

            try
            {
                //Open the device
                m_ElectronicJournal.Open();
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to open the ElectronicJournal Device.", "EJSample_Step3");
                ChangeEJButtonStatus();
                return;
            }

            try
            {
                m_ElectronicJournal.Claim(1000);
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to claim the ElectronicJournal device.", "EJSample_Step3");

                ChangeEJButtonStatus();
                return;
            }

            try
            {
                m_ElectronicJournal.DeviceEnabled = true;
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to enable the ElectronicJournal device.", "EJSample_Step3");
                ChangeEJButtonStatus();
                return;
            }

            try
            {
                if (chStorageEnabled.Checked)
                {
                    m_ElectronicJournal.StorageEnabled = true;
                }
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to enable storage", "EJSample_Step3");
                ChangeEJButtonStatus();
                return;
            }

            //<<<step3>>>--Start	
            //Register OutputCompleteEvent
            AddOutputCompleteEvent(m_ElectronicJournal);

            //Register DataEvent
            AddDataEvent(m_ElectronicJournal);

            //Register ErrorEvent
            AddErrorEvent(m_ElectronicJournal);
            //<<<step3>>>--End	

        }

        /// <summary>
        /// A method that disables the "Print Receipt" Button when called.
        /// </summary>
        private void ChangePrintButtonStatus()
        {
            btnPrintReceipt.Enabled = false;
        }

        /// <summary>
        /// A method that disables all ElectronicJournal related controls when called.
        /// </summary>
        private void ChangeEJButtonStatus()
        {
            btnAddMarker.Enabled = false;
            txtMarker.Enabled = false;
            chStorageEnabled.Enabled = false;
            //<<Step3>>--Start
            btnQueryContent.Enabled = false;
            btnPrintContent.Enabled = false;
            txtEndMarker.Enabled = false;
            txtStartMarker.Enabled = false;
            txtFileName.Enabled = false;
            txtPrintContent.Enabled = false;
            btnBrowse.Enabled = false;
            chDataEvent.Enabled = false;
            chAsync.Enabled = false;
            //<<Step3>>--End
        }

         /// <summary>
        /// A method that is called when the form closes. Disable/Releases/Closes PosPrinter and ElectronicJournal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameStep3_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {


            if (m_ElectronicJournal != null)
            {

                //<<<step3>>>--Start	
                //Unregister OutputCompleteEvent
                RemoveOutputCompleteEvent(m_ElectronicJournal);

                //Unregister DataEvent
                RemoveDataEvent(m_ElectronicJournal);

                //Unregister ErrorEvent
                RemoveErrorEvent(m_ElectronicJournal);
                //<<<step3>>>--End	

                try
                {
                    //Cancel the device
                    m_ElectronicJournal.DeviceEnabled = false;

                    //Release the device exclusive control right.
                    m_ElectronicJournal.Release();
                }
                catch (PosControlException)
                {
                }
                finally
                {
                    //Finish using the device.
                    m_ElectronicJournal.Close();
                }
            }

            if (m_Printer != null)
            {
                try
                {
                    //Cancel the device
                    m_Printer.DeviceEnabled = false;

                    //Release the device exclusive control right.
                    m_Printer.Release();
                }
                catch (PosControlException)
                {
                }
                finally
                {
                    //Finish using the device.
                    m_Printer.Close();
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        //<<Step1>>--End


        //<<Step3>>--Start
        /// <summary>
        /// A method that sets the DataEventEnabled property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chDataEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_ElectronicJournal.DataEventEnabled = chDataEvent.Checked;
            }
            catch(PosControlException ex)
            {
               MessageBox.Show("Fails to change DataEventEnabled.\n" + ex.Message, "EJSample_Step3",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// A method that sets the Async Mode property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chAsync_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                m_ElectronicJournal.AsyncMode = chAsync.Checked;
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to change Async Mode.\n" + ex.Message, "EJSample_Step3",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// A method that executes QueryContent based on the given filename, start and end markers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryContent_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                m_ElectronicJournal.QueryContent(txtFileName.Text, txtStartMarker.Text, txtEndMarker.Text);
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to Query Content.\n" + ex.Message, "EJSample_Step3",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Cursor.Current = Cursors.Default;

        }
        /// <summary>
        /// A method that executes print Content file based on the given filename
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintContent_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                m_ElectronicJournal.PrintContentFile(txtPrintContent.Text);
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to Print File.\n" + ex.Message, "EJSample_Step3",
                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Cursor.Current = Cursors.Default;

        }
        /// <summary>
        /// A method that invokes a dialog box allowing the user to browse for the file for print Content file method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPrintContent.Text = fileDialog.FileName;
            }
        }

        /// <summary>
        /// Add OutputCompleteEventHandler.
        /// </summary>
        /// <param name="eventSource"></param>
        protected void AddOutputCompleteEvent(object eventSource)
        {
            EventInfo outputCompleteEvent = eventSource.GetType().GetEvent("OutputCompleteEvent");
            if (outputCompleteEvent != null)
            {
                outputCompleteEvent.AddEventHandler(eventSource,
                    new OutputCompleteEventHandler(OnOutputCompleteEvent));
            }
        }

        /// <summary>
        /// Add DataEventHandler.
        /// </summary>
        /// <param name="eventSource"></param>
        protected void AddDataEvent(object eventSource)
        {
            EventInfo dataEvent = eventSource.GetType().GetEvent("DataEvent");
            if (dataEvent != null)
            {
                dataEvent.AddEventHandler(eventSource, new DataEventHandler(OnDataEvent));
            }
        }

        /// <summary>
        /// Add ErrorEventHandler.
        /// </summary>
        /// <param name="eventSource"></param>
        protected void AddErrorEvent(object eventSource)
        {
            EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
            if (errorEvent != null)
            {
                errorEvent.AddEventHandler(eventSource,
                    new DeviceErrorEventHandler(OnErrorEvent));
            }
        }


        /// <summary>
        /// Remove OutputCompleteEventHandler.
        /// </summary>
        /// <param name="eventSource"></param>
        protected void RemoveOutputCompleteEvent(object eventSource)
        {
            EventInfo outputCompleteEvent = eventSource.GetType().GetEvent("OutputCompleteEvent");
            if (outputCompleteEvent != null)
            {
                outputCompleteEvent.RemoveEventHandler(eventSource,
                    new OutputCompleteEventHandler(OnOutputCompleteEvent));
            }
        }


        /// <summary>
        /// Remove DataEventHandler.
        /// </summary>
        /// <param name="eventSource"></param>
        protected void RemoveDataEvent(object eventSource)
        {
            EventInfo dataEvent = eventSource.GetType().GetEvent("DataEvent");
            if (dataEvent != null)
            {
                dataEvent.RemoveEventHandler(eventSource,
                    new DataEventHandler(OnDataEvent));
            }
        }

        /// <summary>
        /// Remove ErrorEventHandler.
        /// </summary>
        /// <param name="eventSource"></param>
        protected void RemoveErrorEvent(object eventSource)
        {
            EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
            if (errorEvent != null)
            {
                errorEvent.RemoveEventHandler(eventSource,
                    new DeviceErrorEventHandler(OnErrorEvent));
            }
        }



        /// <summary>
        /// OutputComplete Event.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void OnOutputCompleteEvent(object source, OutputCompleteEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new OutputCompleteEventHandler(OnOutputCompleteEvent), new object[2] { source, e });
                return;
            }

            //Notify that EJ process is completed when it is asynchronous.
            MessageBox.Show("Complete Printing Content File: ID = " + e.OutputId.ToString()
                , "EJSample_Step3");
        }

        /// <summary>
        /// Data Event.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void OnDataEvent(object source, DataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DataEventHandler(OnDataEvent), new object[2] { source, e });
                return;
            }

            //Notify that EJ process is completed when it is asynchronous.
            MessageBox.Show("Complete Querying Content: Status = " + e.Status.ToString()
    , "EJSample_Step3");

            if (!m_ElectronicJournal.DataEventEnabled)
            {
                chDataEvent.Checked = false;
            }

        }

        /// <summary>
        /// Error Event.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void OnErrorEvent(object source, DeviceErrorEventArgs e)
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DeviceErrorEventHandler(OnErrorEvent), new object[2] { source, e });
                return;
            }

            DialogResult dialogResult;

            string strCause = "";
            if (e.ErrorLocus == ErrorLocus.Output)
            {
                strCause = "Output Error";
            }
            else if (e.ErrorLocus == ErrorLocus.Input)
            {
                strCause = "Input Error";
            }
            else if (e.ErrorLocus == ErrorLocus.InputData)
            {
                strCause = "Input Error";
            }
        
            string strMessage = strCause + "\n\n" + "ErrorCode = " + e.ErrorCode.ToString()
                + "\n" + "ErrorCodeExtended = " + e.ErrorCodeExtended.ToString() + "\n\n"
                + "What would you like to do?";

            dialogResult = MessageBox.Show(strMessage, "EJSample_Step3"
                , MessageBoxButtons.RetryCancel);

            if (dialogResult == DialogResult.Cancel)
            {
                e.ErrorResponse = ErrorResponse.Clear;
            }
            else if (dialogResult == DialogResult.Retry)
            {
                e.ErrorResponse = ErrorResponse.Retry;
            }

        }
        //<<Step3>>--End


    }
}