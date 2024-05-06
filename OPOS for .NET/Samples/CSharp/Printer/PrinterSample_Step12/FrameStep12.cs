using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.IO;
using System.Reflection;
using System.Globalization;
using jp.co.epson.uposcommon;


namespace PrinterSample_Step12
{
	/// <summary>
	///  Description of FrameStep12.
	/// </summary>
	public class FrameStep12 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpReceipt;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnAsync;
		private System.Windows.Forms.Button btnReceipt;
		private System.Windows.Forms.GroupBox grpSlp;
		private System.Windows.Forms.Button btnSales;
		private System.Windows.Forms.Button btnRecoverError;
		private System.Windows.Forms.GroupBox grpDirectIO;
		private System.Windows.Forms.Button btnSupportFunction;
		private System.Windows.Forms.Button btnPanelSwitch;
		private System.Windows.Forms.Button btnPrintCode128;
		private System.Windows.Forms.GroupBox grpReCoverError;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep12()
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
			this.grpReceipt = new System.Windows.Forms.GroupBox();
			this.btnAsync = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnReceipt = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.grpSlp = new System.Windows.Forms.GroupBox();
			this.btnSales = new System.Windows.Forms.Button();
			this.grpDirectIO = new System.Windows.Forms.GroupBox();
			this.btnPanelSwitch = new System.Windows.Forms.Button();
			this.btnPrintCode128 = new System.Windows.Forms.Button();
			this.btnSupportFunction = new System.Windows.Forms.Button();
			this.grpReCoverError = new System.Windows.Forms.GroupBox();
			this.btnRecoverError = new System.Windows.Forms.Button();
			this.grpReceipt.SuspendLayout();
			this.grpSlp.SuspendLayout();
			this.grpDirectIO.SuspendLayout();
			this.grpReCoverError.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpReceipt
			// 
			this.grpReceipt.Controls.Add(this.btnAsync);
			this.grpReceipt.Controls.Add(this.btnPrint);
			this.grpReceipt.Controls.Add(this.btnReceipt);
			this.grpReceipt.Location = new System.Drawing.Point(20, 20);
			this.grpReceipt.Name = "grpReceipt";
			this.grpReceipt.Size = new System.Drawing.Size(196, 144);
			this.grpReceipt.TabIndex = 0;
			this.grpReceipt.TabStop = false;
			this.grpReceipt.Text = "Receipt";
			// 
			// btnAsync
			// 
			this.btnAsync.Location = new System.Drawing.Point(20, 68);
			this.btnAsync.Name = "btnAsync";
			this.btnAsync.Size = new System.Drawing.Size(144, 24);
			this.btnAsync.TabIndex = 1;
			this.btnAsync.Text = "Asynchronous printing";
			this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(20, 28);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(144, 24);
			this.btnPrint.TabIndex = 0;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnReceipt
			// 
			this.btnReceipt.Location = new System.Drawing.Point(20, 104);
			this.btnReceipt.Name = "btnReceipt";
			this.btnReceipt.Size = new System.Drawing.Size(144, 24);
			this.btnReceipt.TabIndex = 2;
			this.btnReceipt.Text = "Print Receipt";
			this.btnReceipt.Click += new System.EventHandler(this.btnReceipt_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(308, 280);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(112, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// grpSlp
			// 
			this.grpSlp.Controls.Add(this.btnSales);
			this.grpSlp.Location = new System.Drawing.Point(20, 180);
			this.grpSlp.Name = "grpSlp";
			this.grpSlp.Size = new System.Drawing.Size(196, 84);
			this.grpSlp.TabIndex = 2;
			this.grpSlp.TabStop = false;
			this.grpSlp.Text = "Slp";
			// 
			// btnSales
			// 
			this.btnSales.Location = new System.Drawing.Point(20, 28);
			this.btnSales.Name = "btnSales";
			this.btnSales.Size = new System.Drawing.Size(144, 24);
			this.btnSales.TabIndex = 1;
			this.btnSales.Text = "Print Sales Slip";
			this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
			// 
			// grpDirectIO
			// 
			this.grpDirectIO.Controls.Add(this.btnPanelSwitch);
			this.grpDirectIO.Controls.Add(this.btnPrintCode128);
			this.grpDirectIO.Controls.Add(this.btnSupportFunction);
			this.grpDirectIO.Controls.Add(this.grpReCoverError);
			this.grpDirectIO.Location = new System.Drawing.Point(228, 20);
			this.grpDirectIO.Name = "grpDirectIO";
			this.grpDirectIO.Size = new System.Drawing.Size(220, 244);
			this.grpDirectIO.TabIndex = 3;
			this.grpDirectIO.TabStop = false;
			this.grpDirectIO.Text = "DirectIO";
			// 
			// btnPanelSwitch
			// 
			this.btnPanelSwitch.Location = new System.Drawing.Point(38, 104);
			this.btnPanelSwitch.Name = "btnPanelSwitch";
			this.btnPanelSwitch.Size = new System.Drawing.Size(144, 24);
			this.btnPanelSwitch.TabIndex = 5;
			this.btnPanelSwitch.Text = "Panel Switch";
			this.btnPanelSwitch.Click += new System.EventHandler(this.btnPanelSwitch_Click);
			// 
			// btnPrintCode128
			// 
			this.btnPrintCode128.Location = new System.Drawing.Point(36, 64);
			this.btnPrintCode128.Name = "btnPrintCode128";
			this.btnPrintCode128.Size = new System.Drawing.Size(144, 24);
			this.btnPrintCode128.TabIndex = 4;
			this.btnPrintCode128.Text = "Print Code128 TypeB";
			this.btnPrintCode128.Click += new System.EventHandler(this.btnPrintCode128_Click);
			// 
			// btnSupportFunction
			// 
			this.btnSupportFunction.Location = new System.Drawing.Point(36, 28);
			this.btnSupportFunction.Name = "btnSupportFunction";
			this.btnSupportFunction.Size = new System.Drawing.Size(144, 24);
			this.btnSupportFunction.TabIndex = 3;
			this.btnSupportFunction.Text = "SupportFunction";
			this.btnSupportFunction.Click += new System.EventHandler(this.btnSupportFunction_Click);
			// 
			// grpReCoverError
			// 
			this.grpReCoverError.Controls.Add(this.btnRecoverError);
			this.grpReCoverError.Location = new System.Drawing.Point(16, 140);
			this.grpReCoverError.Name = "grpReCoverError";
			this.grpReCoverError.Size = new System.Drawing.Size(188, 80);
			this.grpReCoverError.TabIndex = 0;
			this.grpReCoverError.TabStop = false;
			this.grpReCoverError.Text = "When The Error Occurred";
			// 
			// btnRecoverError
			// 
			this.btnRecoverError.Location = new System.Drawing.Point(20, 32);
			this.btnRecoverError.Name = "btnRecoverError";
			this.btnRecoverError.Size = new System.Drawing.Size(144, 24);
			this.btnRecoverError.TabIndex = 2;
			this.btnRecoverError.Text = "Recover Error";
			this.btnRecoverError.Click += new System.EventHandler(this.btnRecoverError_Click);
			// 
			// FrameStep12
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(456, 317);
			this.Controls.Add(this.grpDirectIO);
			this.Controls.Add(this.grpSlp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.grpReceipt);
			this.MaximizeBox = false;
			this.Name = "FrameStep12";
			this.Text = "Step 12 Rotated-Prints Bitmaps and Barcodes";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep12_Closing);
			this.Load += new System.EventHandler(this.frmStep12_Load);
			this.grpReceipt.ResumeLayout(false);
			this.grpSlp.ResumeLayout(false);
			this.grpDirectIO.ResumeLayout(false);
			this.grpReCoverError.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep12());
		}

        // A maximum of 2 line widths will be considered
        const int MAX_LINE_WIDTHS = 2;

		/// <summary>
		/// PosPrinter object.
		/// </summary>
		PosPrinter m_Printer = null;

		/// <summary>
		/// Printer cover status flag.
		/// </summary>
		bool m_btnStateByCover = true;

		/// <summary>
		/// Printer paper status.
		/// </summary>
		bool m_btnStateByPaper = true;

		/// <summary>
		/// Conversensor flag.
		/// </summary>
		bool m_bConverSensor = false;
		
		/// <summary>
		/// An appropriate interval is converted into the length of
		/// the tab about two texts. And make a printing data.
		/// </summary>
		/// <param name="iLineChars">
		/// The width of the territory which it prints on is converted into the number of
		/// characters, and that value is specified.
		/// </param>
		/// <param name="strBuf">
		/// It is necessary as an information for deciding the interval of the text.
		/// </param>
		/// <param name="strPrice">
		/// It is necessary as an information for deciding the interval of the text, too.
		/// </param>
		/// <returns>printing data.
		/// </returns>
		public String MakePrintString(int iLineChars,String strBuf,String strPrice)
		{
			int iSpaces = 0;
			String tab = "";
			try
			{
				iSpaces = iLineChars - (strBuf.Length + strPrice.Length);
				for (int j = 0 ; j < iSpaces ; j++)
				{
					tab += " ";
				}
			}
			catch(Exception)
			{
			}
			return strBuf + tab + strPrice;
		}

		/// <summary>
		/// When the method "changeButtonStatus" was called,
		/// all buttons other than a button "closing" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnAsync.Enabled = false;
			btnPrint.Enabled = false;
			btnReceipt.Enabled = false;
			btnSales.Enabled = false;
			btnSupportFunction.Enabled = false;
			btnPrintCode128.Enabled = false;
			btnPanelSwitch.Enabled = false;
			btnRecoverError.Enabled = false;
		}

		/// The information related to the error from the parameter
		/// "ex" is received as a type of "int".
		/// Information by the sentence corresponding to the received
		/// information is returned as "strErrorCodeEx".
		/// </summary>
		/// <param name="ex"></param>
		/// <returns>
		/// "int" type information is changed into the information
		/// by the sentence, and is returned as a "String" type.
		/// "strErrorCodeEx" holds the information on this "int" type.
		/// </returns>
		private string GetErrorCode(PosControlException ex)
		{
			//<<<step10>>>--Start
			string strErrorCodeEx = "";
			
			switch(ex.ErrorCodeExtended)
			{
				case PosPrinter.ExtendedErrorCoverOpen:
				case PosPrinter.ExtendedErrorJournalEmpty:
				case PosPrinter.ExtendedErrorReceiptEmpty:
				case PosPrinter.ExtendedErrorSlipEmpty:
					strErrorCodeEx = ex.Message;
					break;
				default:
					string strEC = ex.ErrorCode.ToString();
					string strECE = ex.ErrorCodeExtended.ToString();
					strErrorCodeEx = "ErrorCode =" + strEC + "\nErrorCodeExtended =" + strECE + "\n" 
						+ ex.Message;
					break;
			}
			return strErrorCodeEx;

			//<<<step10>>>--End
		}

		/// <summary>
		///  A method "Print" calls some another method.
		///  They are method for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			DateTime nowDate = DateTime.Now;							//System date
			DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
			dateFormat.MonthDayPattern = "MMMM";
			string strDate = nowDate.ToString("MMMM,dd,yyyy  HH:mm",dateFormat);
			string strbcData = "4902720005074";
            int[] RecLineChars = new int[MAX_LINE_WIDTHS] { 0, 0 };
            long lRecLineCharsCount;

			String[] astritem = {"apples", "grapes", "bananas", "lemons", "oranges"};
			String[] astrprice = {"10.00", "20.00", "30.00", "40.00", "50.00"};
			DialogResult dialogResult;
			
			//When outputting to a printer,a mouse cursor becomes like a hourglass.
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				if (m_Printer.CapRecPresent == true)
				{
							
					while (true)
					{			
						//<<<step10>>>--Start
						try
						{
							//<<<step6>>>--Start
							//Batch processing mode
							m_Printer.TransactionPrint(PrinterStation.Receipt,
								PrinterTransactionControl.Transaction);
						}
						catch(PosControlException)
						{
							MessageBox.Show("Cannot use a POS Printer.","Printer_SampleStep12"
								,MessageBoxButtons.OK,MessageBoxIcon.Warning);

							ChangeButtonStatus();
							return;
						}
						try
						{
							if (m_Printer.CapRecBitmap == true)
							{
								//<<<step3>>>--Start
								//Print a registered bitmap.
								m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1B");
								//<<<step3>>>--End
							}

							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|N" 
								+ "123xxstreet,xxxcity,xxxxstate\n");

							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|rA" 
								+"TEL 9999-99-9999   C#2\n");
			
							//<<<step5>>--Start
							//Make 2mm speces
							//ESC|#uF = Line Feed
							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|200uF");
							//<<<step5>>>-End

			                lRecLineCharsCount = GetRecLineChars(ref RecLineChars);
							if (lRecLineCharsCount >= 2) {
			                    m_Printer.RecLineChars = RecLineChars[1];
								m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|cA" + strDate + "\n");
			                    m_Printer.RecLineChars = RecLineChars[0];
							}
							else {
								m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|cA" + strDate + "\n");
							}

							//<<<step5>>>--Start
							//Make 5mm speces
							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|500uF");

							//Print buying goods
							double total = 0.0;
							string strPrintData = "";
							for (int i = 0; i < astritem.Length; i++)
							{
								strPrintData = MakePrintString(m_Printer.RecLineChars, astritem[i], "$"
									+ astrprice[i]);

								m_Printer.PrintNormal(PrinterStation.Receipt,strPrintData + "\n");

								total += Convert.ToDouble(astrprice[i]);

							}

							//Make 2mm speces
							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|200uF");

							//Print the total cost
							strPrintData = MakePrintString(m_Printer.RecLineChars,"Tax excluded."
								, "$" + total.ToString("F"));

							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|bC" + strPrintData + "\n");

							strPrintData = MakePrintString(m_Printer.RecLineChars,"Tax 5.0%","$" 
								+ (total * 0.05).ToString("F"));

							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC" +  strPrintData + "\n");

							strPrintData = MakePrintString(m_Printer.RecLineChars /2 ,"Total","$" 
								+ (total * 1.05).ToString("F"));

							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|bC" + "\u001b|2C" 
								+ strPrintData + "\n");

							strPrintData = MakePrintString(m_Printer.RecLineChars,"Customer's payment"
								, "$200.00");

							m_Printer.PrintNormal(PrinterStation.Receipt
								,strPrintData + "\n");

							strPrintData = MakePrintString(m_Printer.RecLineChars,"Change","$" 
								+ (200.00 - (total * 1.05)).ToString("F"));

							m_Printer.PrintNormal(PrinterStation.Receipt,strPrintData + "\n");
		
							//Make 5mm speces
							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|500uF");
				
							//<<<step4>>>--Start
							if(m_Printer.CapRecBarCode == true)
							{
								//Barcode printing
								m_Printer.PrintBarCode(PrinterStation.Receipt,strbcData,
									BarCodeSymbology.EanJan13,1000,
									m_Printer.RecLineWidth,PosPrinter.PrinterBarCodeLeft,
									BarCodeTextPosition.Below);
							}
							//<<<step4>>>--End
							//<<<step5>>>--End

							m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|"
								+ m_Printer.RecLinesToPaperCut + "lF");
				
							if(m_Printer.CapRecPaperCut == true)
							{
								m_Printer.CutPaper(100);
							}
							break;
						}
						catch(PosControlException)
						{
							// When error occurs, display a message to ask the user whether retry or not.
							dialogResult = MessageBox.Show("Failed to output to printer.\n\nRetry?"
								,"Printer_SampleStep12",MessageBoxButtons.AbortRetryIgnore);

							if (dialogResult == DialogResult.Abort)
							{
								try
								{
									// Clear the buffered data since the buffer retains print data when an error occurs during printing.
									m_Printer.ClearOutput();
								}
								catch(PosControlException)
								{
								}
								return;
							}
							else if (dialogResult ==DialogResult.Ignore)
							{
								break;
							}
							try
							{
								// Clear the buffered data since the buffer retains print data when an error occurs during printing.
								m_Printer.ClearOutput();
							}
							catch(PosControlException)
							{
							}
							continue;
						}
						//<<<step10>>>--End
					
					}
					
					//<<<step10>>>--Start
					//print all the buffer data. and exit the batch processing mode.
					while(m_Printer.State != ControlState.Idle)
					{
						try
						{
							System.Threading.Thread.Sleep(100);
						}
						catch(Exception)
						{
						}
					}
					//<<<step10>>>--End

					m_Printer.TransactionPrint(PrinterStation.Receipt
						,PrinterTransactionControl.Normal);
					//<<<step6>>>--End

				}
				else
				{
					MessageBox.Show("Cannot use a Receipt Stateion.","Printer_SampleStep12"
						,MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return;
				}
			}
			catch(PosControlException)
			{
				// Clear the buffered data since the buffer retains print data when an error occurs during printing.
                m_Printer.ClearOutput();
			}

			// When a cursor is back to its default shape, it means the process ends
			Cursor.Current = Cursors.Default;
		}

        private long GetRecLineChars(ref int[] RecLineChars)
		{
            long lRecLineChars = 0;
	        long lCount;
	        int i;
	
	        // Calculate the element count.
	        lCount = m_Printer.RecLineCharsList.GetLength(0);
	
	        if(lCount == 0) {
                lRecLineChars = 0;
	        } 
	        else {
                if (lCount > MAX_LINE_WIDTHS)
                {
                    lCount = MAX_LINE_WIDTHS;
                }
	
				for( i = 0; i < lCount; i++) {
                    RecLineChars[i] = m_Printer.RecLineCharsList[i];
	         	}

                lRecLineChars = lCount;
	     	}

            return lRecLineChars;
		}
		
		/// <summary>
		/// A method "Asynchronous Printing" calls some another method.
		/// This includes methods for starting and ending "AsyncMode", and for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAsync_Click(object sender, System.EventArgs e)
		{
			try
			{
				m_Printer.AsyncMode = true;

				btnPrint_Click(sender,e);
				
				m_Printer.AsyncMode = false;
			}
			catch(PosControlException)
			{
			}
		}

		/// <summary>
		///  The method "Print Receipt" calls some other methods.
		///  This includes methods for starting and ending the "rotation print mode", and for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReceipt_Click(object sender, System.EventArgs e)
		{
			//<<<step8>>>--Start
			//Initialization
			DateTime nowDate = DateTime.Now;							//System date
			DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
			dateFormat.MonthDayPattern = "MMMM";
			string strDate = nowDate.ToString("MMMM,dd,yyyy  HH:mm",dateFormat);
			string strbcData = "4902720005074";
			int iRecLineSpacing;
			int iRecLineHeight;
			bool bBitmapPrint = false;
			bool bBarcodePrint = false;
			DialogResult dialogResult;
			bool bBuffering = true;
			int iPrintRotation = 0;
			string strCurDir = Directory.GetCurrentDirectory();

			string strFilePath = strCurDir.Substring(0,strCurDir.LastIndexOf("Step12") + "Step12\\".Length);

			strFilePath += "Logo.bmp";
	
			Cursor.Current = Cursors.WaitCursor;

			//<<<step12>>>--Start
			Rotation[] arBitmapRotationList = m_Printer.RecBitmapRotationList;
			Rotation[] arBarcodeRotationList = m_Printer.RecBarCodeRotationList;
		
			//Check rotate bitmap
			for(int i = 0; i < arBitmapRotationList.Length;i++)
			{
				if(arBitmapRotationList[i].Equals(Rotation.Left90))
				{
					bBitmapPrint = true;
					iPrintRotation = (iPrintRotation | (int)PrintRotation.Left90 )
						| ((int)PrintRotation.Bitmap);
				}
			}

			//Check rotate barcode
			for(int i = 0; i < arBarcodeRotationList.Length;i++)
			{
				if(arBarcodeRotationList[i].Equals(Rotation.Left90))
				{
					bBarcodePrint = true;
					iPrintRotation = (iPrintRotation | (int)PrintRotation.Barcode) 
						| ((int)PrintRotation.Left90);
				}
			}
			//<<<step12>>>--End

			iRecLineSpacing = m_Printer.RecLineSpacing;
			iRecLineHeight = m_Printer.RecLineHeight;

			if(m_Printer.CapRecPresent == true)
			{
				//<<<step10>>>--Start
				while(true)
				{
					
					try
					{
						
						m_Printer.TransactionPrint(PrinterStation.Receipt
							,PrinterTransactionControl.Transaction);
						
						m_Printer.RotatePrint(PrinterStation.Receipt,
							(PrintRotation)iPrintRotation);

						//<<<step12>>>--Start
						if(bBitmapPrint == true)
						{
						
							m_Printer.PrintBitmap(PrinterStation.Receipt,strFilePath,
								m_Printer.RecLineWidth / 6
								,PosPrinter.PrinterBitmapCenter);
						
						}
						//<<<step12>>>-End

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|4C" + "\u001b|bC"
							+ "   Receipt     ");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|3C" + "\u001b|2uC"
							+ "       Mr. Brawn\n");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|2uC"
							+ "                                                  \n");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|2uC" + "\u001b|3C" 
							+ "        Total payment              $" + "\u001b|4C" + "21.00  ");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1C\n" );

						m_Printer.PrintNormal(PrinterStation.Receipt,strDate + " Received\n\n");

						m_Printer.RecLineHeight = 24;
						m_Printer.RecLineSpacing = m_Printer.RecLineHeight;
		
						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC" 
							+ " Details               \n");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1C"
							+ "                          " + "\u001b|2C" + "OPOS Store\n");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC"
							+ " Tax excluded    $20.00\n");

						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1C"
							+ "                          " + "\u001b|bC" + "Zip code 999-9999\n");
						
						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC"
							+ " Tax(5%)        $1.00" + "\u001b|N" + "    Phone#(9999)99-9998\n\n");

						//<<<step12>>>--Start
						if(bBarcodePrint == true)
						{
					
							m_Printer.PrintBarCode(PrinterStation.Receipt,
								strbcData,BarCodeSymbology.EanJan13,
								500,m_Printer.RecLineWidth /2 ,1,
								BarCodeTextPosition.Below);	
						}
						//<<<step12>>>--End
						break;
					}
					catch(PosControlException ex)
					{
						if(ex.ErrorCode == ErrorCode.Illegal && ex.ErrorCodeExtended == 1004)
						{
							MessageBox.Show("Unable to print receipt.\n" + GetErrorCode(ex)
								,"Printer_SampleStep12",MessageBoxButtons.OK,MessageBoxIcon.Warning);

							// Clear the buffered data since the buffer retains print data when an error occurs during printing.
							m_Printer.ClearOutput();
							bBuffering = false;
							break;
						}
						
						// When error occurs, display a message to ask the user whether retry or not.
						dialogResult = MessageBox.Show("Failed to output to printer.\n\nRetry?"
							,"Printer_SampleStep12",MessageBoxButtons.AbortRetryIgnore);

						try
						{
							// Clear the buffered data since the buffer retains print data when an error occurs during printing.
							m_Printer.ClearOutput();
						}
						catch(PosControlException)
						{
						}

						if (dialogResult == DialogResult.Abort || dialogResult == DialogResult.Ignore)
						{
							break;
						}

						continue;
					}
				}
				//<<<step10>>>--End
			}
			else
			{
				MessageBox.Show("Cannot use a Receipt Stateion.","Printer_SampleStep12"
					,MessageBoxButtons.OK,MessageBoxIcon.Warning);

				return;
			}

			try
			{
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

				m_Printer.TransactionPrint(PrinterStation.Receipt,PrinterTransactionControl.Normal);
			}
			catch(PosControlException ex )
			{
				// Clear the buffered data since the buffer retains print data when an error occurs during printing.
				m_Printer.ClearOutput();
				MessageBox.Show("Unable to print receipt.\n" + GetErrorCode(ex),"Printer_SampleStep12",
					MessageBoxButtons.OK,MessageBoxIcon.Warning);

			}

			Cursor.Current = Cursors.Default;
			m_Printer.RecLineSpacing = iRecLineSpacing;
			m_Printer.RecLineHeight = iRecLineHeight;
			//<<<step8>>>--Start
		}

		/// <summary>
		///  A method "PrintSalesSlip" calls some another method.
		///  They are method for printing and for inserting and
		///  removing the slip paper.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSales_Click(object sender, System.EventArgs e)
		{
			//<<<Step9>>>--Start
			//Initialization
			DateTime nowDate = DateTime.Now;							//System date
			DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
			dateFormat.MonthDayPattern = "MMMM";
			string strDate = nowDate.ToString("MMMM,dd,yyyy",dateFormat);
			string strRecno = "0001";									//Register No.
			string strName = "ABCDEF";									 //Casher No.
			string strSpace = "";
			string strPrintData;
			string strTime = nowDate.ToString("HH:mm");
		
			Cursor.Current = Cursors.WaitCursor;

			//Request for inserting a slip	
			if(WaitForInsertion() ==false)
			{
				return;
			}

			try
			{
				for (int i = 33; i < m_Printer.SlpLineChars; i++)
				{
					strSpace += " ";
				}

				// Print data
				strPrintData = "\n" + strSpace + "Print credit card sales slip\n";
				strPrintData = strPrintData + "\u001b|1lF";
				strPrintData = strPrintData + strSpace + "        SEIKO EPSON Corp.\n";
				strPrintData = strPrintData + strSpace + "Thank you for coming to our shop!\n";
				strPrintData = strPrintData + "\u001b|1lF";
				strPrintData = strPrintData + strSpace + "Date  " + strDate + "\n";
				strPrintData = strPrintData + strSpace + "Time      " + strTime + "Casher   " + strName + "\n";
				strPrintData = strPrintData + strSpace + "Number of the register  " + strRecno + "\n";
				strPrintData = strPrintData + "\u001b|N" + "\u001b|1lF";
				strPrintData = strPrintData + strSpace + "Details                      cost\n";
				strPrintData = strPrintData + strSpace + "Cardigan                 $ 100.00\n";
				strPrintData = strPrintData + strSpace + "Shoes                     $ 70.00\n";
				strPrintData = strPrintData + strSpace + "Hat                       $ 30.00\n";
				strPrintData = strPrintData + strSpace + "Bag                      $ 150.00\n";
				strPrintData = strPrintData + strSpace + "        Excluded tax     $ 350.00\n";
				strPrintData = strPrintData + strSpace + "        Tax(5%)           $ 17.50\n";
				strPrintData = strPrintData + strSpace + "        -------------------------\n";
				strPrintData = strPrintData + strSpace + "\u001b|2C     Total" +"\u001b|1C     $ 367.50\n";
				strPrintData = strPrintData + "\u001b|1lF";
				strPrintData = strPrintData + strSpace + "Company name   EPSON-CARD\n";
				strPrintData = strPrintData + strSpace + "Membership No. XXXXXXXXXXXXXXXX\n";
				strPrintData = strPrintData + strSpace + "Valid date     12/05\n";
				strPrintData = strPrintData + strSpace + "Handling No.   9999 - 999999\n";
				strPrintData = strPrintData + strSpace + "Approval No.   99\n";
				strPrintData = strPrintData + "\u001b|1lF";
				strPrintData = strPrintData + strSpace + "Signature\n";
				//Printing process
				m_Printer.PrintNormal(PrinterStation.Slip,strPrintData);

				//Clean up
				Cursor.Current = Cursors.Default;

				//Remove the slip at the slip station.
				WaitForRemoval();
			}
			catch(PosControlException)
			{
			}
			//<<<Step9>>>--End
		}

		/// <summary>
		/// When the button "SupportFunction" is pushed, the method
		/// "directIO" is run. "directIO" decides that function by the
		/// parameter. "GetSupportFunction" is ordered here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSupportFunction_Click(object sender, System.EventArgs e)
		{

			//<<<step11>>--Start
			int iData = 0;
			byte[] abyte = new byte[0];
			DirectIOData  directIOReturn;
			string strMessage = "";

			try
			{
				// directIO(int command, int data, Object)
				directIOReturn =  m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_GET_SUPPORT_FUNCTION
					,iData ,abyte );

				if(Convert.ToBoolean(directIOReturn.Data & 
					(int)EpsonPOSPrinterConst.PTR_DI_LABEL))
				{
					strMessage = "Lable function supported.";
				}
				else if(Convert.ToBoolean(directIOReturn.Data & 
					(int)EpsonPOSPrinterConst.PTR_DI_BLACK_MARK))
				{
					strMessage = "BlackMark function supported.";
																				
				}
				else if(Convert.ToBoolean(directIOReturn.Data & 
					(int)EpsonPOSPrinterConst.PTR_DI_GB18030))
				{
					strMessage = "GB18030 Simplified Chinese can be printed.";
				}
				else if(Convert.ToBoolean(directIOReturn.Data & 
					(int)EpsonPOSPrinterConst.PTR_DI_BATTERY))
				{
					strMessage = "Battery function supported.";
				}
				else
				{
					strMessage = "Return:" + directIOReturn.Data.ToString();
				}

				MessageBox.Show(strMessage,"DirectIOData");

			}
			catch(PosControlException)
			{
				MessageBox.Show("This function is disable to use.","PrinterSample_Step12");
			}
			//<<<step11>>--End
		}

		/// <summary>
		/// When the button "Panel Switch" is pushed, the method
		/// "directIO" is run. "directIO" decides that function by the
		/// parameter."Panel Switch" is ordered here.
		///  printer panel button is disabled.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPanelSwitch_Click(object sender, System.EventArgs e)
		{
			//<<<step11>>--Start
			int iData = 0;
			byte[] abyte = new byte[0];
			DirectIOData  directIOReturn;

			try
			{
				// directIO(int command, int data, Object)
				directIOReturn =  m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_PANEL_SWITCH
					,iData,abyte);
			}

			catch(PosControlException)
			{
				MessageBox.Show("This function is disable to use.","PrinterSample_Step12");	
			}
			//<<<step11>>--End
		}

		/// <summary>
		/// When the button "Panel Switch" is pushed, the method
		/// "directIO" is run. "directIO" decides that function by the
		/// parameter. "PrintBarCode Code128 TypeB" is ordered here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrintCode128_Click(object sender, System.EventArgs e)
		{
			//<<<step11>>--Start
			int iData = EpsonPOSPrinterConst.PTR_DI_CODE_B;
			byte[] abyte = new byte[0];
			DirectIOData  directIOReturn;
			string strbcData = "OPOS for .NET";

			try
			{
				// directIO(int command, int data, Object)
				directIOReturn =  m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_CODE128_TYPE
					,iData ,abyte );
				
				m_Printer.PrintBarCode(PrinterStation.Receipt,
					strbcData,BarCodeSymbology.Code128,
					1000,m_Printer.RecLineWidth,
					PosPrinter.PrinterBarCodeCenter,
					BarCodeTextPosition.Below);

			}

			catch(PosControlException)
			{
				MessageBox.Show("DirectIO Code128 failed.","PrinterSample_Step12");
				
			}
			//<<<step11>>--End
		}

		/// When the button "Recover Error" is pushed, the method
		/// "directIO" is run. "directIO" decides that function by the
		/// parameter. "RecoverError" is ordered.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRecoverError_Click(object sender, System.EventArgs e)
		{
			//<<<Step11>>>--Start
			int iData = 0;
			byte[] abyte = new byte[0];

			try
			{					
				// directIO(int command, int data, Object)
				m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_RECOVER_ERROR ,iData ,abyte );
			}
			catch(PosControlException)
			{
				MessageBox.Show("This function is disable to use.","Printer_SampleStep12");
			}
			//<<<Step11>>>--End

		}
		/// <summary>
		/// The processing of the insertion of the slip paper and the
		/// processing when an error occurs is described here.
		/// </summary>
		private bool WaitForInsertion()
		{
			//<<<Step10>>>
			DialogResult dialogResult;
			bool bInsertion = true;

			while(true)
			{
				try
				{
					m_Printer.BeginInsertion(500);
				}
				catch(PosControlException pex)
				{

					if(pex.ErrorCode == ErrorCode.Timeout)
					{
						dialogResult =  MessageBox.Show("Please insert a form.","PrinterSample_Step12"
							,MessageBoxButtons.YesNo);

						if(dialogResult == DialogResult.No)
						{
							bInsertion = false;
							try
							{
								m_Printer.EndInsertion();
								m_Printer.BeginRemoval(5000);
							}
							catch(PosControlException)
							{
							}
							return bInsertion;
						}		
					}
					else
					{
						string strMessage = GetErrorCode(pex);
						MessageBox.Show(strMessage,"PrinterSample_Step12");
						bInsertion = false;
					}
				}

				try
				{
					m_Printer.EndInsertion();
					bInsertion = true;
				}
				catch(PosControlException pex)
				{
					string strMessage = "";
					
					if(pex.ErrorCodeExtended == PosPrinter.ExtendedErrorSlipEmpty)
					{

						strMessage = "Please insert a form.";
					}
					
					else
					{
						if(m_Printer.SlpEmpty == false)
						{
							strMessage = "Please remove a form."; 
						}
						else
						{
							strMessage = GetErrorCode(pex);
						}
					}

					dialogResult = MessageBox.Show(strMessage + "\n\n Do you continue?",
						"Print credit sales slip",MessageBoxButtons.YesNo);

					if(dialogResult == DialogResult.No)
					{
						bInsertion = false;
						return bInsertion;
					}

					try
					{
						m_Printer.BeginRemoval(5000);
					}
					catch(PosControlException)
					{
					}
					continue;
				}
				break;

			}
			return bInsertion;
		}

		/// <summary>
		/// Discharge processing of a slip paper and processing when
		/// an error occurs are described here.
		/// </summary>
		/// <returns></returns>
		private void WaitForRemoval()
		{
			//Discharge operation of a paper is started.
			try
			{
				m_Printer.BeginRemoval(10000);
			}
			catch(PosControlException pex)
			{
				string strMessage = "";

				if(pex.ErrorCode == ErrorCode.Timeout)
				{
					strMessage = "Please remove a form.";
				}
				else
				{
					strMessage = GetErrorCode(pex);
				}

				MessageBox.Show(strMessage,"Print credit sales slip",
					MessageBoxButtons.OK,MessageBoxIcon.Warning);

				return;
			}

			try
			{
				m_Printer.EndRemoval();
			}

			catch(PosControlException)
			{
			}
		}

		/// <summary>
		/// Close form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// The processing code required in order to enable to use of service is written here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep12_Load(object sender, System.EventArgs e)
		{
			
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "PosPrinter";
						
			//Current Directory Path
			string strCurDir = Directory.GetCurrentDirectory();

			string strFilePath = strCurDir.Substring(0,strCurDir.LastIndexOf("bin"));

			strFilePath += "Logo.bmp";
	
			//Create PosExplorer
			PosExplorer posExplorer = new PosExplorer();
		
			DeviceInfo deviceInfo = null;

			//<<<step10>>>--Start
			try
			{
				deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter,strLogicalName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","Printer_SampleStep12");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}
			try
			{
				m_Printer =(PosPrinter)posExplorer.CreateInstance(deviceInfo);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to create instance.","Printer_SampleStep12");
				//Nothing can be used.
				ChangeButtonStatus();
				return;

			}

			//<<<step9>>>--Start	
			//Register OutputCompleteEventHandler.
			AddOutputCompleteEvent(m_Printer);
			//<<<step9>>>--End

			//<<<step10>>>--Start	
			//Register OutputCompleteEventHandler.
			AddErrorEvent(m_Printer);

			//Register OutputCompleteEventHandler.
			AddStatusUpdateEvent(m_Printer);
			

			try
			{
				//Open the device
				m_Printer.Open();
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to open the device.","Printer_SampleStep12");

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
				MessageBox.Show("Failed to claim the device.","Printer_SampleStep12");

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
				MessageBox.Show("Disable to use the device.","Printer_SampleStep12");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}
		
			try
			{
				//<<<step3>>>--Start
				//Output by the high quality mode
				m_Printer.RecLetterQuality = true;
	
				// Even if using any printers, 0.01mm unit makes it possible to print neatly.
				m_Printer.MapMode = MapMode.Metric;
			}
			catch(PosControlException)
			{
			}

            if (m_Printer.CapRecBitmap == true)
            {

                bool bSetBitmapSuccess = false;
                for (int iRetryCount = 0; iRetryCount < 5; iRetryCount++)
                {
                    try
                    {
                        //<<<step5>>>--Start
                        //Register a bitmap
                        m_Printer.SetBitmap(1, PrinterStation.Receipt,
                            strFilePath, m_Printer.RecLineWidth / 2,
                            PosPrinter.PrinterBitmapCenter);
                        //<<<step5>>>--End
                        bSetBitmapSuccess = true;
                        break;
                    }
                    catch (PosControlException pce)
                    {
                        if (pce.ErrorCode == ErrorCode.Failure && pce.ErrorCodeExtended == 0 && pce.Message == "It is not initialized.")
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
                if (!bSetBitmapSuccess)
                {
                    MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep12"
                            , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
			//<<<step3>>>--End

			//<<<Step8>>>--Start
			//Check on rotation print function			
			if((m_Printer.CapRecRight90 == false) | (m_Printer.CapRecLeft90 == false))
			{
				//Not function to [Print Receipt] button is disable
				btnReceipt.Enabled = false;
			}
			//<<<step8>>>--End

			//<<<step9>>>--Start
			//Check on slp print function		
			if((m_Printer.CapSlpPresent == false) | (m_Printer.CapSlpFullSlip == false))
			{
				btnSales.Enabled = false;
			}
			//<<<step9>>>--End

			//<<<Step10>>>--Start
			m_bConverSensor = m_Printer.CapCoverSensor;
			//<<<Step10>>>--End

			
			//<<<step10>>>--End
			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep12_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Printer != null)
			{
				try
				{

					//<<<step9>>>--Start
					// Remove OutputCompleteEvent listener
					RemoveOutputCompleteEvent(m_Printer);
					//<<<step9>>>--End

					//<<<step10>>>--Start
					//Remove ErrorEvent listener
					RemoveErrorEvent(m_Printer);

					// Remove StatusUpdateEvent listener
					RemoveStatusUpdateEvent(m_Printer);
					//<<<step10>>>--End

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
                    try
                    {
                        m_Printer.Close();
                    }
                    catch (PosControlException)
                    {
                    }
				}
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Add OutputCompleteEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddOutputCompleteEvent(object eventSource)
		{
			//<<<step7>>>--Start
			EventInfo outputCompleteEvent = eventSource.GetType().GetEvent( "OutputCompleteEvent");
			if( outputCompleteEvent != null )
			{
				outputCompleteEvent.AddEventHandler( eventSource,
					new OutputCompleteEventHandler(OnOutputCompleteEvent));
			}
			//<<<step7>>>--End
		}

		
		/// <summary>
		/// Add ErrorEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddErrorEvent(object eventSource)
		{
			//<<<step10>>>--Start
			EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
			if(errorEvent != null)
			{
				errorEvent.AddEventHandler(eventSource,
					new DeviceErrorEventHandler(OnErrorEvent));
			}
			//<<<step10>>>--End
		}

		/// <summary>
		/// Add StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddStatusUpdateEvent(object eventSource)
		{
			//<<<step10>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent");
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.AddEventHandler(eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent));
			}
			//<<<step10>>>--End
		}

		/// <summary>
		/// Remove OutputCompeleteEvenHandlert.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveOutputCompleteEvent(object eventSource)
		{
			//<<<step7>>>--Start
			EventInfo outputCompleteEvent = eventSource.GetType().GetEvent( "OutputCompleteEvent");
			if( outputCompleteEvent != null )
			{
				outputCompleteEvent.RemoveEventHandler( eventSource,
					new OutputCompleteEventHandler(OnOutputCompleteEvent));
			}
			//<<<step7>>>--End
		}

		/// <summary>
		/// Remove ErrorEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveErrorEvent(object eventSource)
		{
			//<<<step10>>>--Start
			EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
			if(errorEvent != null)
			{
				errorEvent.RemoveEventHandler( eventSource,
					new DeviceErrorEventHandler(OnErrorEvent));
			}
			//<<<step10>>>--End
		}

		/// <summary>
		/// Remove StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveStatusUpdateEvent(object eventSource)
		{
			//<<<step10>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent");
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.RemoveEventHandler( eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent));
			}
			//<<<step10>>>--End
		}

		/// <summary>
		/// OutputComplete Event.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected  void OnOutputCompleteEvent(object source, OutputCompleteEventArgs e)
		{
			//<<<step7>>>--Start
			//Notify that printing is completed when it is asnchronous.
			MessageBox.Show("Complete printing : ID = " + e.OutputId.ToString()
				,"Printer_SampleStep12");
			//<<<step7>>>--End
		}

		/// <summary>
		/// Error Event.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected  void OnErrorEvent(object source, DeviceErrorEventArgs e )
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DeviceErrorEventHandler(OnErrorEvent), new object[2] { source, e });
                return;
            }

			//<<<step10>>>--Start
			DialogResult dialogResult;

			string strMessage = "Printer Error\n\n" + "ErrorCode = " + e.ErrorCode.ToString() 
				+ "\n" + "ErrorCodeExtended = " + e.ErrorCodeExtended.ToString();
		
			dialogResult =  MessageBox.Show(strMessage,"Printer_SampleStep12"
				,MessageBoxButtons.RetryCancel);

			if(dialogResult == DialogResult.Cancel)
			{
				e.ErrorResponse = ErrorResponse.Clear;
			}
			else if(dialogResult == DialogResult.Retry)
			{
				e.ErrorResponse = ErrorResponse.Retry;
			}
			//<<<step10>>>--End

		}

		/// <summary>
		/// StatusUpdateEvnet.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected  void OnStatusUpdateEvent(object source, StatusUpdateEventArgs e)
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new StatusUpdateEventHandler(OnStatusUpdateEvent), new object[2] { source, e });
                return;
            }

			//When there is a change of the status on the printer, the event is fired.
			switch(e.Status)
			{
					//Printer cover is open.
				case PosPrinter.StatusCoverOpen:
					m_btnStateByCover = false;
					break;
					//No receipt paper.
				case PosPrinter.StatusReceiptEmpty:
					m_btnStateByPaper = false;
					break;
					//'Printer cover is close.
				case PosPrinter.StatusCoverOK:
					m_btnStateByCover = true;
					break;
					//'Receipt paper is ok.
				case PosPrinter.StatusReceiptPaperOK:
				case PosPrinter.StatusReceiptNearEmpty:
					m_btnStateByPaper = true;
					break;
			}

			if((m_btnStateByPaper == true) && ((m_btnStateByCover == true) || !m_bConverSensor))
			{
				btnPrint.Enabled = true;
				btnAsync.Enabled = true;
				btnSales.Enabled = true;
				btnPanelSwitch.Enabled = true;
				btnRecoverError.Enabled = true;
				btnPrintCode128.Enabled = true;
				btnSupportFunction.Enabled = true;

				try
				{
					if((m_Printer.CapRecLeft90 == false) || (m_Printer.CapRecRight90 == false))
					{
						btnReceipt.Enabled = false;
					}
					else
					{
						btnReceipt.Enabled = true;
					}

					if((m_Printer.CapSlpPresent == false) || (m_Printer.CapSlpFullSlip == false))
					{
						btnSales.Enabled = false;
					}
				}
				catch(PosControlException)
				{
				}
			}
			else
			{
				ChangeButtonStatus();
			}												  
		}
	}
}
