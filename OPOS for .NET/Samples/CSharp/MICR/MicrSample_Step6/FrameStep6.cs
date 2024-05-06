using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.IO;
using System.Diagnostics;
using EpsonUPOSConst = jp.co.epson.uposcommon.EpsonUPOSConst;

namespace MicrSample_Step6
{
	/// <summary>
	/// Description of FrameStep6.
	/// </summary>
	public class FrameStep6 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.GroupBox grpCheckData;
		private System.Windows.Forms.TextBox txtAccountNumber;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.TextBox txtBanknumber;
		private System.Windows.Forms.TextBox txtCheckType;
		private System.Windows.Forms.TextBox txtCountryCode;
		private System.Windows.Forms.TextBox txtEPC;
		private System.Windows.Forms.TextBox txtSerialNumber;
		private System.Windows.Forms.TextBox txtTransitNumber;
		private System.Windows.Forms.Label lblAmount;
		private System.Windows.Forms.Label lblBankNumber;
		private System.Windows.Forms.Label lblCheckType;
		private System.Windows.Forms.Label lblCountryCode;
		private System.Windows.Forms.Label lblEPC;
		private System.Windows.Forms.Label lblSerialNumber;
		private System.Windows.Forms.Label lblTransitNumber;
		private System.Windows.Forms.Label lblAccountNumber;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.GroupBox grpDeviceStatistics;
		private System.Windows.Forms.Button btnRetrieveStatistics;
		private System.Windows.Forms.TextBox txtStatistics;
		private System.Windows.Forms.Label lblRawData;
		private System.Windows.Forms.TextBox txtRawData;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep6()
		{
			//
			// The InitializeComponent() call is required for windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add counstructor code after the InitializeComponent() call.
			//
		}

		/// <summary>
		/// Rear treatment is carried out in the resource being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Forms Designer generated code.
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. 
		/// The Forms designer might not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblRawData = new System.Windows.Forms.Label();
			this.txtRawData = new System.Windows.Forms.TextBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.grpCheckData = new System.Windows.Forms.GroupBox();
			this.lblTransitNumber = new System.Windows.Forms.Label();
			this.lblSerialNumber = new System.Windows.Forms.Label();
			this.lblEPC = new System.Windows.Forms.Label();
			this.lblCountryCode = new System.Windows.Forms.Label();
			this.lblCheckType = new System.Windows.Forms.Label();
			this.lblBankNumber = new System.Windows.Forms.Label();
			this.lblAmount = new System.Windows.Forms.Label();
			this.txtTransitNumber = new System.Windows.Forms.TextBox();
			this.txtSerialNumber = new System.Windows.Forms.TextBox();
			this.txtEPC = new System.Windows.Forms.TextBox();
			this.txtCountryCode = new System.Windows.Forms.TextBox();
			this.txtCheckType = new System.Windows.Forms.TextBox();
			this.txtBanknumber = new System.Windows.Forms.TextBox();
			this.txtAmount = new System.Windows.Forms.TextBox();
			this.txtAccountNumber = new System.Windows.Forms.TextBox();
			this.lblAccountNumber = new System.Windows.Forms.Label();
			this.btnPrint = new System.Windows.Forms.Button();
			this.grpDeviceStatistics = new System.Windows.Forms.GroupBox();
			this.btnRetrieveStatistics = new System.Windows.Forms.Button();
			this.txtStatistics = new System.Windows.Forms.TextBox();
			this.grpCheckData.SuspendLayout();
			this.grpDeviceStatistics.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblRawData
			// 
			this.lblRawData.Location = new System.Drawing.Point(8, 24);
			this.lblRawData.Name = "lblRawData";
			this.lblRawData.Size = new System.Drawing.Size(92, 20);
			this.lblRawData.TabIndex = 0;
			this.lblRawData.Text = "RawData";
			// 
			// txtRawData
			// 
			this.txtRawData.Location = new System.Drawing.Point(124, 24);
			this.txtRawData.Name = "txtRawData";
			this.txtRawData.Size = new System.Drawing.Size(228, 19);
			this.txtRawData.TabIndex = 1;
			this.txtRawData.Text = "";
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(28, 320);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(88, 24);
			this.btnInsert.TabIndex = 2;
			this.btnInsert.Text = "Insert";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(116, 320);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(88, 24);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(312, 320);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 24);
			this.btnExit.TabIndex = 4;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// grpCheckData
			// 
			this.grpCheckData.Controls.Add(this.lblTransitNumber);
			this.grpCheckData.Controls.Add(this.lblSerialNumber);
			this.grpCheckData.Controls.Add(this.lblEPC);
			this.grpCheckData.Controls.Add(this.lblCountryCode);
			this.grpCheckData.Controls.Add(this.lblCheckType);
			this.grpCheckData.Controls.Add(this.lblBankNumber);
			this.grpCheckData.Controls.Add(this.lblAmount);
			this.grpCheckData.Controls.Add(this.txtTransitNumber);
			this.grpCheckData.Controls.Add(this.txtSerialNumber);
			this.grpCheckData.Controls.Add(this.txtEPC);
			this.grpCheckData.Controls.Add(this.txtCountryCode);
			this.grpCheckData.Controls.Add(this.txtCheckType);
			this.grpCheckData.Controls.Add(this.txtBanknumber);
			this.grpCheckData.Controls.Add(this.txtAmount);
			this.grpCheckData.Controls.Add(this.txtAccountNumber);
			this.grpCheckData.Controls.Add(this.lblRawData);
			this.grpCheckData.Controls.Add(this.txtRawData);
			this.grpCheckData.Controls.Add(this.lblAccountNumber);
			this.grpCheckData.Location = new System.Drawing.Point(20, 24);
			this.grpCheckData.Name = "grpCheckData";
			this.grpCheckData.Size = new System.Drawing.Size(384, 284);
			this.grpCheckData.TabIndex = 5;
			this.grpCheckData.TabStop = false;
			this.grpCheckData.Text = "The data of check";
			// 
			// lblTransitNumber
			// 
			this.lblTransitNumber.Location = new System.Drawing.Point(8, 236);
			this.lblTransitNumber.Name = "lblTransitNumber";
			this.lblTransitNumber.Size = new System.Drawing.Size(92, 20);
			this.lblTransitNumber.TabIndex = 16;
			this.lblTransitNumber.Text = "TransitNumber";
			// 
			// lblSerialNumber
			// 
			this.lblSerialNumber.Location = new System.Drawing.Point(8, 204);
			this.lblSerialNumber.Name = "lblSerialNumber";
			this.lblSerialNumber.Size = new System.Drawing.Size(92, 20);
			this.lblSerialNumber.TabIndex = 15;
			this.lblSerialNumber.Text = "SerialNumber";
			// 
			// lblEPC
			// 
			this.lblEPC.Location = new System.Drawing.Point(8, 180);
			this.lblEPC.Name = "lblEPC";
			this.lblEPC.Size = new System.Drawing.Size(92, 20);
			this.lblEPC.TabIndex = 14;
			this.lblEPC.Text = "EPC";
			// 
			// lblCountryCode
			// 
			this.lblCountryCode.Location = new System.Drawing.Point(8, 152);
			this.lblCountryCode.Name = "lblCountryCode";
			this.lblCountryCode.Size = new System.Drawing.Size(92, 20);
			this.lblCountryCode.TabIndex = 13;
			this.lblCountryCode.Text = "CountryCode";
			// 
			// lblCheckType
			// 
			this.lblCheckType.Location = new System.Drawing.Point(8, 128);
			this.lblCheckType.Name = "lblCheckType";
			this.lblCheckType.Size = new System.Drawing.Size(92, 20);
			this.lblCheckType.TabIndex = 12;
			this.lblCheckType.Text = "CheckType";
			// 
			// lblBankNumber
			// 
			this.lblBankNumber.Location = new System.Drawing.Point(8, 104);
			this.lblBankNumber.Name = "lblBankNumber";
			this.lblBankNumber.Size = new System.Drawing.Size(92, 20);
			this.lblBankNumber.TabIndex = 11;
			this.lblBankNumber.Text = "BankNumber";
			// 
			// lblAmount
			// 
			this.lblAmount.Location = new System.Drawing.Point(8, 76);
			this.lblAmount.Name = "lblAmount";
			this.lblAmount.Size = new System.Drawing.Size(92, 20);
			this.lblAmount.TabIndex = 10;
			this.lblAmount.Text = "Amount";
			// 
			// txtTransitNumber
			// 
			this.txtTransitNumber.Location = new System.Drawing.Point(124, 236);
			this.txtTransitNumber.Name = "txtTransitNumber";
			this.txtTransitNumber.Size = new System.Drawing.Size(228, 19);
			this.txtTransitNumber.TabIndex = 9;
			this.txtTransitNumber.Text = "";
			// 
			// txtSerialNumber
			// 
			this.txtSerialNumber.Location = new System.Drawing.Point(124, 208);
			this.txtSerialNumber.Name = "txtSerialNumber";
			this.txtSerialNumber.Size = new System.Drawing.Size(228, 19);
			this.txtSerialNumber.TabIndex = 8;
			this.txtSerialNumber.Text = "";
			// 
			// txtEPC
			// 
			this.txtEPC.Location = new System.Drawing.Point(124, 180);
			this.txtEPC.Name = "txtEPC";
			this.txtEPC.Size = new System.Drawing.Size(228, 19);
			this.txtEPC.TabIndex = 7;
			this.txtEPC.Text = "";
			// 
			// txtCountryCode
			// 
			this.txtCountryCode.Location = new System.Drawing.Point(124, 148);
			this.txtCountryCode.Name = "txtCountryCode";
			this.txtCountryCode.Size = new System.Drawing.Size(228, 19);
			this.txtCountryCode.TabIndex = 6;
			this.txtCountryCode.Text = "";
			// 
			// txtCheckType
			// 
			this.txtCheckType.Location = new System.Drawing.Point(124, 120);
			this.txtCheckType.Name = "txtCheckType";
			this.txtCheckType.Size = new System.Drawing.Size(228, 19);
			this.txtCheckType.TabIndex = 5;
			this.txtCheckType.Text = "";
			// 
			// txtBanknumber
			// 
			this.txtBanknumber.Location = new System.Drawing.Point(124, 96);
			this.txtBanknumber.Name = "txtBanknumber";
			this.txtBanknumber.Size = new System.Drawing.Size(228, 19);
			this.txtBanknumber.TabIndex = 4;
			this.txtBanknumber.Text = "";
			// 
			// txtAmount
			// 
			this.txtAmount.Location = new System.Drawing.Point(124, 72);
			this.txtAmount.Name = "txtAmount";
			this.txtAmount.Size = new System.Drawing.Size(228, 19);
			this.txtAmount.TabIndex = 3;
			this.txtAmount.Text = "";
			// 
			// txtAccountNumber
			// 
			this.txtAccountNumber.Location = new System.Drawing.Point(124, 48);
			this.txtAccountNumber.Name = "txtAccountNumber";
			this.txtAccountNumber.Size = new System.Drawing.Size(228, 19);
			this.txtAccountNumber.TabIndex = 2;
			this.txtAccountNumber.Text = "";
			// 
			// lblAccountNumber
			// 
			this.lblAccountNumber.Location = new System.Drawing.Point(8, 48);
			this.lblAccountNumber.Name = "lblAccountNumber";
			this.lblAccountNumber.Size = new System.Drawing.Size(92, 20);
			this.lblAccountNumber.TabIndex = 17;
			this.lblAccountNumber.Text = "AccountNumber";
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(204, 320);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(88, 24);
			this.btnPrint.TabIndex = 6;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// grpDeviceStatistics
			// 
			this.grpDeviceStatistics.Controls.Add(this.btnRetrieveStatistics);
			this.grpDeviceStatistics.Controls.Add(this.txtStatistics);
			this.grpDeviceStatistics.Location = new System.Drawing.Point(24, 364);
			this.grpDeviceStatistics.Name = "grpDeviceStatistics";
			this.grpDeviceStatistics.Size = new System.Drawing.Size(376, 88);
			this.grpDeviceStatistics.TabIndex = 7;
			this.grpDeviceStatistics.TabStop = false;
			this.grpDeviceStatistics.Text = "Device Statistics";
			// 
			// btnRetrieveStatistics
			// 
			this.btnRetrieveStatistics.Location = new System.Drawing.Point(8, 32);
			this.btnRetrieveStatistics.Name = "btnRetrieveStatistics";
			this.btnRetrieveStatistics.Size = new System.Drawing.Size(108, 24);
			this.btnRetrieveStatistics.TabIndex = 11;
			this.btnRetrieveStatistics.Text = "RetrieveStatistics";
			this.btnRetrieveStatistics.Click += new System.EventHandler(this.btnRetrieveStatistics_Click);
			// 
			// txtStatistics
			// 
			this.txtStatistics.Location = new System.Drawing.Point(124, 35);
			this.txtStatistics.Name = "txtStatistics";
			this.txtStatistics.Size = new System.Drawing.Size(228, 19);
			this.txtStatistics.TabIndex = 10;
			this.txtStatistics.Text = "ModelName,HoursPoweredCount,GoodReadCount";
			// 
			// FrameStep6
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(428, 473);
			this.Controls.Add(this.grpDeviceStatistics);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.grpCheckData);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnInsert);
			this.Controls.Add(this.btnRemove);
			this.MaximizeBox = false;
			this.Name = "FrameStep6";
			this.Text = "Step 6 Obtains the statistics of the device.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep6_Closing);
			this.Load += new System.EventHandler(this.frmStep6_Load);
			this.grpCheckData.ResumeLayout(false);
			this.grpDeviceStatistics.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep6());
		}

		/// <summary>
		/// Micr object.
		/// </summary>
		Micr m_Micr = null;

		/// <summary>
		/// Printer object.
		/// </summary>
		PosPrinter m_Printer = null;

		/// <summary>
		/// The code for inserting a check are described.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>--Start
			DialogResult dialogResult;

			while(true)
			{
				try
				{
					//Insertion preparations of a check are made.
					m_Micr.BeginInsertion(1000);
					break;
				}
				catch(PosControlException pex)
				{
					//If Timeoust begininsertion
					if (pex.ErrorCode == ErrorCode.Timeout) 
					{

						dialogResult = MessageBox.Show("Please insert a check."
							,"MicrSample_Step6",MessageBoxButtons.YesNo);

						if(dialogResult == DialogResult.No)
						{

							try
							{
								m_Micr.EndInsertion();
								m_Micr.BeginRemoval(10000);
							}
							catch(PosControlException)
							{
							}
							
							return;
						}
					}	
                    else if (pex.ErrorCode == ErrorCode.Illegal && pex.ErrorCodeExtended == EpsonUPOSConst.UPOS_EX_INVALID_MODE)
                    {
                        dialogResult = MessageBox.Show("Insert error.", "Insert error.", MessageBoxButtons.OK);
                        return;
					}	
					else
					{
						return;
					}
				}
			}
		
			try
			{
				//Insertion operation of a check is started.
				m_Micr.EndInsertion();
			}
			catch(PosControlException)
			{
			}
			//<<<step2>>>--End
		}

		/// <summary>
		/// The code for removing a check are described.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>--Start
			try
			{
				m_Micr.BeginRemoval(3000);
			}
			catch(PosControlException pex)
			{
				//If Timeoust BeginRemoval
				if (pex.ErrorCode == ErrorCode.Timeout) 
				{
					MessageBox.Show("Please remove a check.","MicrSample_Step6");
				}
			}
			//<<<step2>>>--End
		}

		/// <summary>
		/// The code for printing on a Check sheet are described.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			//<<<step5>>>--Start
			DateTime nowDate = DateTime.Now;							//System date
			DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
			dateFormat.MonthDayPattern = "MMMM";
			string strDate = nowDate.ToString("MMMM,dd,yyyy  HH:mm",dateFormat);
			
			StringBuilder strPrintData = new StringBuilder();

			strPrintData.Append("\u001b|rA" + strDate + "\n");
			strPrintData.Append("\u001b|rA" + "SEIKO EPSON CORPORATION\n");
			
			try
			{
				m_Printer.ChangePrintSide(PrinterSide.Side2);
			}
			catch(PosControlException)
			{
			}

			try
			{
				m_Printer.PrintNormal(PrinterStation.Slip,strPrintData.ToString());
					
			}
			catch(PosControlException)
			{
			}
			//<<<step5>>>--End
		}

		/// <summary>
		///  A method "RetrieveStatistics" calls RetrieveStatistics method.
		///  This method obtains the statistics of device
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRetrieveStatistics_Click(object sender, System.EventArgs e)
		{
			//<<<step6>>>--Start
			string[] astrStatistics = null;
			string strReturn = "";
			string strSaveXmlPath = "";
		
			//Current Directory Path
			string strCurDir = Directory.GetCurrentDirectory();
		
			strSaveXmlPath +=strCurDir +  "\\" + "Sample.xml";
		
			astrStatistics = txtStatistics.Text.Split(',');

			try
			{
				strReturn = m_Micr.RetrieveStatistics(astrStatistics);
			
				if(File.Exists(strSaveXmlPath))
				{
					File.Delete(strSaveXmlPath);
				}

				try
				{
					StreamWriter sw = File.CreateText(strSaveXmlPath);
					sw.Write(strReturn);
					sw.Close();

					//Show xml file
					Process.Start(strSaveXmlPath);
				}
				catch(IOException)
				{
				}
			}
			catch(PosControlException)
			{
			}
			//<<<step6>>>--End
		}

		/// <summary>
		///  When the method "changeButtonStatus" was called,
		///  All buttons other than a button "Exit" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnInsert.Enabled = false;
			btnRemove.Enabled = false;
			txtAccountNumber.Enabled = false;
			txtAmount.Enabled = false;
			txtBanknumber.Enabled = false;
			txtCheckType.Enabled = false;
			txtCountryCode.Enabled = false;
			txtEPC.Enabled = false;
			txtRawData.Enabled = false;
			txtSerialNumber.Enabled = false;
			txtTransitNumber.Enabled = false;
			btnPrint.Enabled = false;
			txtStatistics.Enabled = false;
			btnRetrieveStatistics.Enabled = false;
		}

		/// <summary>
		/// From Close
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// The processing code required in order to enable to use of service is written here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep6_Load(object sender, System.EventArgs e)
		{
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strMicrLogicalName = "Micr";
			string strPrinterLogicalName = "PosPrinter";
			
			//Create PosExplorer
			PosExplorer posExplorer = new PosExplorer();
	
			DeviceInfo deviceMicrInfo = null;

			DeviceInfo devicePrinterInfo = null;
		
			//<<<step5>>>--Start
			try
			{
				deviceMicrInfo = posExplorer.GetDevice(DeviceType.Micr,strMicrLogicalName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","MicrSample_Step6");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			//<<<step4>>>--Start
			try
			{
				m_Micr =(Micr)posExplorer.CreateInstance(deviceMicrInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","MicrSample_Step6",MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			//Register DataEvent
			AddDataEvent(m_Micr);

			//<<<step4>>>--Start
			//Register ErrorEvent
			AddErrorEvent(m_Micr);

			//Register StatusUpdateEvent
			AddStatusUpdateEvent(m_Micr);
			//<<<step4>>>--End

			try
			{
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Micr.Open();

				//In order to enable it to use the DataEvent.
				m_Micr.DataEventEnabled = true;

			}
			catch(PosControlException)
			{
				MessageBox.Show("This device has not been registered, or cannot use."
					,"MicrSample_Step6",MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Micr.Claim(1000);
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to get the exclusive access for the device."
					,"MicrSample_Step6",MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				if(m_Micr.CapPowerReporting != PowerReporting.None)
				{
					m_Micr.PowerNotify = PowerNotification.Enabled;
				}
			}
			catch(PosControlException)
			{
			}

			try
			{
				//Enable the device.
				m_Micr.DeviceEnabled = true;
			}
			catch(PosControlException)
			{
				MessageBox.Show("Now the device is disable to use."
					,"MicrSample_Step6",MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}
			//<<<step4>>>--End
			//<<<step1>>>--End
			
			try
			{
				devicePrinterInfo = posExplorer.GetDevice(DeviceType.PosPrinter,strPrinterLogicalName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","MicrSample_Step6");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				m_Printer =(PosPrinter)posExplorer.CreateInstance(devicePrinterInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","MicrSample_Step6"
					,MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Printer.Open();

			}
			catch(PosControlException)
			{
				MessageBox.Show("This device has not been registered, or cannot use."
					,"MicrSample_Step6",MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Printer.Claim(1000);
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to get the exclusive access for the device."
					,"MicrSample_Step6",MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}
			
			try
			{
				//Enable the device.
				m_Printer.DeviceEnabled = true;
			}
			catch(PosControlException)
			{
				MessageBox.Show("Now the device is disable to use.","MicrSample_Step6"
					,MessageBoxButtons.OK);

				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			if(m_Micr.CapValidationDevice == false)
			{
				MessageBox.Show("The device does not support for printing both sides. Failed to operate the step."
					,"MicrSample_Step6",MessageBoxButtons.OK);

				btnPrint.Enabled = false;
			}
			//<<<step5>>>--End

			//<<<step6>>>--Start
			if(m_Micr.CapStatisticsReporting == false)
			{
				btnRetrieveStatistics.Enabled = false;
				txtStatistics.Enabled = false;
			}
			//<<<step6>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep6_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Micr != null)
			{
				try
				{
					// Remove DataEvent listener
					RemoveDataEvent(m_Micr);

					//<<<step4>>>--Start
					// Remove ErrorEvent listener
					RemoveErrorEvent(m_Micr);

					// Remove StatusUpdateEvent listener
					RemoveStatusUpdateEvent(m_Micr);
					//<<<step4>>>--End

					//Cancel the device
					m_Micr.DeviceEnabled = false;

					//Release the device exclusive control right.
					m_Micr.Release();
				}
				catch(PosControlException)
				{
				}
				finally
				{
					//Finish using the device.
					m_Micr.Close();
				}
			}
			////<<<step1>>>--End

			//<<<step5>>>--Start
			if(m_Printer != null)
			{
				try
				{
					//Cancel the device
					m_Printer.DeviceEnabled = false;

					//Release the device exclusive control right.
					m_Printer.Release();

				}
				catch(PosControlException)
				{
				}
				finally
				{
					//Finish using the device.
					m_Printer.Close();
				}
			}
			//<<<step5>>>--End			
		}

		/// <summary>
		/// Add DataEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddDataEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent" );
			
			if( dataEvent != null )
			{
				dataEvent.AddEventHandler( eventSource,
					new DataEventHandler(OnDataEvent) );
			}
			//<<<step1>>>--End
		}
		
		/// <summary>
		/// Add ErrorEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddErrorEvent(object eventSource)
		{
			//<<<step4>>>--Start
			EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
			if(errorEvent != null)
			{
				errorEvent.AddEventHandler(eventSource,
					new DeviceErrorEventHandler(OnErrorEvent));
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// Add StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddStatusUpdateEvent(object eventSource)
		{
			//<<<step4>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent" );
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.AddEventHandler(eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent));
			}
			//<<<step4>>>--End
		}
	
		/// <summary>
		/// Remove DataEventHandlerr.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveDataEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent" );
			
			if( dataEvent != null )
			{
				dataEvent.RemoveEventHandler( eventSource,
					new DataEventHandler(OnDataEvent) );
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Remove ErrorEvenHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveErrorEvent(object eventSource)
		{
			//<<<step4>>>--Start
			EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
			if(errorEvent != null)
			{
				errorEvent.RemoveEventHandler( eventSource,
					new DeviceErrorEventHandler(OnErrorEvent) );
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// Remove StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveStatusUpdateEvent(object eventSource)
		{
			//<<<step4>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent" );
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.RemoveEventHandler( eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent) );
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// OutputComplete Event.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void OnErrorEvent(object source, DeviceErrorEventArgs e )
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DeviceErrorEventHandler(OnErrorEvent), new object[2] { source, e });
                return;
            }

			//<<<step4>>>--Start
			MessageBox.Show("Micr Error\n" + "ErrorCode =" + e.ErrorCode.ToString() + "\n"
				+ "ErrorCodeExtended =" +e.ErrorCodeExtended.ToString() ,"MicrSample_Step6"
				,MessageBoxButtons.OK);
			//<<<step4>>>--End
		}

		/// <summary>
		/// StatusUpdateEvnet.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void OnStatusUpdateEvent(object source, StatusUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new StatusUpdateEventHandler(OnStatusUpdateEvent), new object[2] { source, e });
                return;
            }

			//<<<step4>>>--Start
			//'The Power Reporting Requirements fires the event when the device power status is changed.
			switch(e.Status)
			{
					//The device is powered on.
				case PosCommon.StatusPowerOnline:
					MessageBox.Show("The device is powered on.","MicrSample_Step6"
						,MessageBoxButtons.OK);
					break;
					//The device is powered off, or unconnected.
				case PosCommon.StatusPowerOff:
					MessageBox.Show("The device is powered off","MicrSample_Step6"
						,MessageBoxButtons.OK);
					break;
					//The device is powered on, but disable to operate.
				case PosCommon.StatusPowerOffline:
					MessageBox.Show("The device is powered on, but disable to operate."
						,"MicrSample_Step6",MessageBoxButtons.OK);
					break;
					//The device is powered off or off-line.
				case PosCommon.StatusPowerOffOffline:
					MessageBox.Show("The device is powered on or off-line."
						,"MicrSample_Step6",MessageBoxButtons.OK);
					break;
				default:
					break;
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// OutputComplete Event.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void OnDataEvent(object source, DataEventArgs e)
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DataEventHandler(OnDataEvent), new object[2] { source, e });
                return;
            }
	
			//<<<step3>>>--Start
			txtRawData.Text = m_Micr.RawData;
			txtAccountNumber.Text = m_Micr.AccountNumber;
			txtAmount.Text = m_Micr.Amount;
			txtBanknumber.Text = m_Micr.BankNumber;
			txtEPC.Text = m_Micr.Epc;
			txtSerialNumber.Text = m_Micr.SerialNumber;
			txtTransitNumber.Text = m_Micr.TransitNumber;
			
			switch(m_Micr.CheckType)
			{
				case CheckType.Business:
					txtCheckType.Text = "Business";
					break;
				case CheckType.Personal:
					txtCheckType.Text = "Personal";
					break;
				case CheckType.Unknown:
					txtCheckType.Text = "Unknown";
					break;
			}

			switch(m_Micr.CountryCode)
			{
				case CheckCountryCode.Canada:
					txtCountryCode.Text = "Canada";
					break;
				case CheckCountryCode.Mexico:
					txtCountryCode.Text = "Mexico";
					break;
				case CheckCountryCode.Usa:
					txtCountryCode.Text = "Usa";
					break;
				case CheckCountryCode.Unknown:
					txtCountryCode.Text = "Unknown";
					break;
			}

			m_Micr.DataEventEnabled = true;
			//<<<step3>>>--End
		}
	}
}
