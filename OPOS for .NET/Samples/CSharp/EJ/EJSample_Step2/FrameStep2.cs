using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.PointOfService;
using System.Globalization;

namespace EJSample_Step2
{
    public class FrameStep2 : Form
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
        private TextBox txtMarker2;
        private TextBox txtMarker1;
        private Button btnPrintContent;
        private Button btnErase;
        private Label lblStartMarker;
        private Label lblEndMarker;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        

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
            Application.Run(new FrameStep2());
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
            this.lblEndMarker = new System.Windows.Forms.Label();
            this.lblStartMarker = new System.Windows.Forms.Label();
            this.btnErase = new System.Windows.Forms.Button();
            this.txtMarker2 = new System.Windows.Forms.TextBox();
            this.txtMarker1 = new System.Windows.Forms.TextBox();
            this.btnPrintContent = new System.Windows.Forms.Button();
            this.txtMarker = new System.Windows.Forms.TextBox();
            this.chStorageEnabled = new System.Windows.Forms.CheckBox();
            this.btnAddMarker = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpBoxReceiptPrinter.SuspendLayout();
            this.gpBoxEJ.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxReceiptPrinter
            // 
            this.grpBoxReceiptPrinter.Controls.Add(this.btnPrintReceipt);
            this.grpBoxReceiptPrinter.Location = new System.Drawing.Point(13, 11);
            this.grpBoxReceiptPrinter.Name = "grpBoxReceiptPrinter";
            this.grpBoxReceiptPrinter.Size = new System.Drawing.Size(326, 71);
            this.grpBoxReceiptPrinter.TabIndex = 0;
            this.grpBoxReceiptPrinter.TabStop = false;
            this.grpBoxReceiptPrinter.Text = "Receipt Printer";
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.Location = new System.Drawing.Point(89, 27);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(149, 21);
            this.btnPrintReceipt.TabIndex = 0;
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = true;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // gpBoxEJ
            // 
            this.gpBoxEJ.Controls.Add(this.lblEndMarker);
            this.gpBoxEJ.Controls.Add(this.lblStartMarker);
            this.gpBoxEJ.Controls.Add(this.btnErase);
            this.gpBoxEJ.Controls.Add(this.txtMarker2);
            this.gpBoxEJ.Controls.Add(this.txtMarker1);
            this.gpBoxEJ.Controls.Add(this.btnPrintContent);
            this.gpBoxEJ.Controls.Add(this.txtMarker);
            this.gpBoxEJ.Controls.Add(this.chStorageEnabled);
            this.gpBoxEJ.Controls.Add(this.btnAddMarker);
            this.gpBoxEJ.Controls.Add(this.groupBox1);
            this.gpBoxEJ.Controls.Add(this.groupBox2);
            this.gpBoxEJ.Location = new System.Drawing.Point(13, 89);
            this.gpBoxEJ.Name = "gpBoxEJ";
            this.gpBoxEJ.Size = new System.Drawing.Size(326, 222);
            this.gpBoxEJ.TabIndex = 1;
            this.gpBoxEJ.TabStop = false;
            this.gpBoxEJ.Text = "Electronic Journal";
            // 
            // lblEndMarker
            // 
            this.lblEndMarker.AutoSize = true;
            this.lblEndMarker.Location = new System.Drawing.Point(149, 132);
            this.lblEndMarker.Name = "lblEndMarker";
            this.lblEndMarker.Size = new System.Drawing.Size(63, 12);
            this.lblEndMarker.TabIndex = 8;
            this.lblEndMarker.Text = "End Marker";
            // 
            // lblStartMarker
            // 
            this.lblStartMarker.AutoSize = true;
            this.lblStartMarker.Location = new System.Drawing.Point(149, 91);
            this.lblStartMarker.Name = "lblStartMarker";
            this.lblStartMarker.Size = new System.Drawing.Size(69, 12);
            this.lblStartMarker.TabIndex = 7;
            this.lblStartMarker.Text = "Start Marker";
            // 
            // btnErase
            // 
            this.btnErase.Location = new System.Drawing.Point(21, 186);
            this.btnErase.Name = "btnErase";
            this.btnErase.Size = new System.Drawing.Size(122, 21);
            this.btnErase.TabIndex = 6;
            this.btnErase.Text = "Erase Medium";
            this.btnErase.UseVisualStyleBackColor = true;
            this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
            // 
            // txtMarker2
            // 
            this.txtMarker2.Location = new System.Drawing.Point(149, 147);
            this.txtMarker2.Name = "txtMarker2";
            this.txtMarker2.Size = new System.Drawing.Size(160, 19);
            this.txtMarker2.TabIndex = 5;
            this.txtMarker2.Text = "MarkerByStep2";
            // 
            // txtMarker1
            // 
            this.txtMarker1.Location = new System.Drawing.Point(149, 106);
            this.txtMarker1.Name = "txtMarker1";
            this.txtMarker1.Size = new System.Drawing.Size(160, 19);
            this.txtMarker1.TabIndex = 4;
            this.txtMarker1.Text = "MarkerByStep1";
            // 
            // btnPrintContent
            // 
            this.btnPrintContent.Location = new System.Drawing.Point(21, 123);
            this.btnPrintContent.Name = "btnPrintContent";
            this.btnPrintContent.Size = new System.Drawing.Size(122, 21);
            this.btnPrintContent.TabIndex = 3;
            this.btnPrintContent.Text = "Print File";
            this.btnPrintContent.UseVisualStyleBackColor = true;
            this.btnPrintContent.Click += new System.EventHandler(this.btnPrintContent_Click);
            // 
            // txtMarker
            // 
            this.txtMarker.Location = new System.Drawing.Point(148, 53);
            this.txtMarker.Name = "txtMarker";
            this.txtMarker.Size = new System.Drawing.Size(159, 19);
            this.txtMarker.TabIndex = 2;
            this.txtMarker.Text = "MarkerByStep2";
            // 
            // chStorageEnabled
            // 
            this.chStorageEnabled.AutoSize = true;
            this.chStorageEnabled.Checked = true;
            this.chStorageEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chStorageEnabled.Location = new System.Drawing.Point(148, 18);
            this.chStorageEnabled.Name = "chStorageEnabled";
            this.chStorageEnabled.Size = new System.Drawing.Size(107, 16);
            this.chStorageEnabled.TabIndex = 1;
            this.chStorageEnabled.Text = "Storage Enabled";
            this.chStorageEnabled.UseVisualStyleBackColor = true;
            this.chStorageEnabled.CheckedChanged += new System.EventHandler(this.chStorageEnabled_CheckedChanged);
            // 
            // btnAddMarker
            // 
            this.btnAddMarker.Location = new System.Drawing.Point(20, 50);
            this.btnAddMarker.Name = "btnAddMarker";
            this.btnAddMarker.Size = new System.Drawing.Size(122, 21);
            this.btnAddMarker.TabIndex = 0;
            this.btnAddMarker.Text = "Add Marker";
            this.btnAddMarker.UseVisualStyleBackColor = true;
            this.btnAddMarker.Click += new System.EventHandler(this.btnAddMarker_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(7, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 39);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(7, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(313, 91);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(212, 316);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 21);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrameStep2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 348);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gpBoxEJ);
            this.Controls.Add(this.grpBoxReceiptPrinter);
            this.KeyPreview = true;
            this.Name = "FrameStep2";
            this.Text = "Step 2 - Printing the data saved in storage medium.";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrameStep2_Closing);
            this.Load += new System.EventHandler(this.FrameStep2_Load);
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
        public FrameStep2()
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
                         //   + " Tax(5%)        $1.00" + "\u001b|N" + "    Phone#(9999)99-9998\n");

                        break;
                    }
                    catch (PosControlException ex)
                    {
                        if (ex.ErrorCode == ErrorCode.Illegal && ex.ErrorCodeExtended == 1004)
                        {
                            MessageBox.Show("Unable to print receipt.\n" + ex.Message
                                , "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Clear the buffered data since the buffer retains print data when an error occurs during printing.
                            m_Printer.ClearOutput();
                            bBuffering = false;
                            break;
                        }

                         //When error occurs, display a message to ask the user whether retry or not.
                        dialogResult = MessageBox.Show("Fails to output to a printer.\n\nRetry?"
                            , "EJSample_Step2", MessageBoxButtons.AbortRetryIgnore);

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
                MessageBox.Show("Cannot use a Receipt Stateion.", "EJSample_Step2"
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
                MessageBox.Show("Unable to print receipt.\n" + ex.Message, "EJSample_Step2",
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
                    , "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    , "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Loads the form. Open/Claim/Enable PosPrinter and ElectronicJournal. Sets StorageEnabled to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameStep2_Load(object sender, EventArgs e)
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
                MessageBox.Show("Fails to get printer device information.", "EJSample_Step2");
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
                MessageBox.Show("Fails to create PosPrinter instance.", "EJSample_Step2");
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
                MessageBox.Show("Fails to open the PosPrinter device.", "EJSample_Step2");

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
                MessageBox.Show("Fails to claim the POSprinter device.", "EJSample_Step2");

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
                MessageBox.Show("Fails to enable to PosPrinter device.", "EJSample_Step2");
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
                MessageBox.Show("Fails to get Electronic Journal device information.", "EJSample_Step2");
                ChangeEJButtonStatus();
                return;
            }
            try
            {
                m_ElectronicJournal = (ElectronicJournal)posExplorer.CreateInstance(deviceInfo);
            }
            catch (Exception)
            {
                MessageBox.Show("Fails to create ElectronicJournal instance.", "EJSample_Step2");
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
                MessageBox.Show("Fails to open the ElectronicJournal Device.", "EJSample_Step2");
                ChangeEJButtonStatus();
                return;
            }

            try
            {
                m_ElectronicJournal.Claim(1000);
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to claim the ElectronicJournal device.", "EJSample_Step2");

                ChangeEJButtonStatus();
                return;
            }

            try
            {
                m_ElectronicJournal.DeviceEnabled = true;
            }
            catch (PosControlException)
            {
                MessageBox.Show("Fails to enable the ElectronicJournal device.", "EJSample_Step2");
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
                MessageBox.Show("Fails to enable storage", "EJSample_Step2");
                ChangeEJButtonStatus();
                return;
            }



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
            //<<Step2>>--Start
            btnPrintContent.Enabled = false;
            btnErase.Enabled = false;
            txtMarker1.Enabled = false;
            txtMarker2.Enabled = false;
            //<<Step2>>--End
        }

          /// <summary>
        /// A method that is called when the form closes. Disable/Releases/Closes PosPrinter and ElectronicJournal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameStep2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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

            if (m_ElectronicJournal != null)
            {
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        //<<Step1>>--End


        //<<Step2>>--Start
        /// <summary>
        /// A method that is called to print the content of an ElectronicJournal specified between the start and end markers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintContent_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                m_ElectronicJournal.PrintContent(txtMarker1.Text, txtMarker2.Text);
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to Print Content.\n" + ex.Message, "EJSample_Step2");
            }

            Cursor.Current = Cursors.Default;


        }

        /// <summary>
        /// A method that is called to Erase the medium. All log image and tag files for this electronic Journal will be erased.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErase_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to erase medium?\n", "EJSample_Step2", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    m_ElectronicJournal.EraseMedium();
                }
            }
            catch (PosControlException ex)
            {
                MessageBox.Show("Fails to Erase Medium.\n" + ex.Message, "EJSample_Step2");
            }
            Cursor.Current = Cursors.Default;

            //<<Step2>>--End

        }

    }
}