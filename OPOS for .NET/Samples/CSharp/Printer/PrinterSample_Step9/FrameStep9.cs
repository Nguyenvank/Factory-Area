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

namespace PrinterSample_Step9
{
	/// <summary>
	///  Description of FrameStep9.
	/// </summary>
	public class FrameStep9 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpReceipt;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnAsync;
		private System.Windows.Forms.Button btnReceipt;
		private System.Windows.Forms.GroupBox grpSlp;
		private System.Windows.Forms.Button btnSales;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep9()
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
            this.grpReceipt.SuspendLayout();
            this.grpSlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReceipt
            // 
            this.grpReceipt.Controls.Add(this.btnAsync);
            this.grpReceipt.Controls.Add(this.btnPrint);
            this.grpReceipt.Controls.Add(this.btnReceipt);
            this.grpReceipt.Location = new System.Drawing.Point(20, 22);
            this.grpReceipt.Name = "grpReceipt";
            this.grpReceipt.Size = new System.Drawing.Size(276, 156);
            this.grpReceipt.TabIndex = 0;
            this.grpReceipt.TabStop = false;
            this.grpReceipt.Text = "Receipt";
            // 
            // btnAsync
            // 
            this.btnAsync.Location = new System.Drawing.Point(68, 74);
            this.btnAsync.Name = "btnAsync";
            this.btnAsync.Size = new System.Drawing.Size(144, 26);
            this.btnAsync.TabIndex = 1;
            this.btnAsync.Text = "Asynchronous printing";
            this.btnAsync.Click += new System.EventHandler(this.btnAsync_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(68, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(144, 26);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReceipt
            // 
            this.btnReceipt.Location = new System.Drawing.Point(68, 113);
            this.btnReceipt.Name = "btnReceipt";
            this.btnReceipt.Size = new System.Drawing.Size(144, 26);
            this.btnReceipt.TabIndex = 2;
            this.btnReceipt.Text = "Print Receipt";
            this.btnReceipt.Click += new System.EventHandler(this.btnReceipt_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(184, 303);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpSlp
            // 
            this.grpSlp.Controls.Add(this.btnSales);
            this.grpSlp.Location = new System.Drawing.Point(20, 195);
            this.grpSlp.Name = "grpSlp";
            this.grpSlp.Size = new System.Drawing.Size(276, 91);
            this.grpSlp.TabIndex = 2;
            this.grpSlp.TabStop = false;
            this.grpSlp.Text = "Slp";
            // 
            // btnSales
            // 
            this.btnSales.Location = new System.Drawing.Point(66, 33);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(144, 26);
            this.btnSales.TabIndex = 1;
            this.btnSales.Text = "Print Sales Slip";
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // FrameStep9
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(316, 344);
            this.Controls.Add(this.grpSlp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpReceipt);
            this.MaximizeBox = false;
            this.Name = "FrameStep9";
            this.Text = "Step 9 Print on slips.";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep9_Closing);
            this.Load += new System.EventHandler(this.frmStep9_Load);
            this.grpReceipt.ResumeLayout(false);
            this.grpSlp.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep9());
		}

        // A maximum of 2 line widths will be considered
        const int MAX_LINE_WIDTHS = 2;

		/// <summary>
		/// PosPrinter object.
		/// </summary>
		PosPrinter m_Printer = null;

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
		
			//When outputting to a printer,a mouse cursor becomes like a hourglass.
			Cursor.Current = Cursors.WaitCursor;
			
			if(m_Printer.CapRecPresent == true)
			{
				try
				{
					//<<<step6>>>--Start
					//Batch processing mode
					m_Printer.TransactionPrint(PrinterStation.Receipt
						,PrinterTransactionControl.Transaction);

					//<<<step3>>>--Start
					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1B");
					//<<<step3>>>--End

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

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|fP");
					//<<<step2>>>--End
					
					//print all the buffer data. and exit the batch processing mode.
					m_Printer.TransactionPrint(PrinterStation.Receipt
						,PrinterTransactionControl.Normal);
					//<<<step6>>>--End
				}
				catch(PosControlException)
				{
				}
			}

			//<<<step6>>>--Start
			// When a cursor is back to its default shape, it means the process ends
			Cursor.Current = Cursors.Default;
			//<<<step6>>>--End
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
			//<<<step7>>>--Start
			try
			{
				m_Printer.AsyncMode = true;

				btnPrint_Click(sender,e);
				
				m_Printer.AsyncMode = false;
			}
			catch(PosControlException)
			{
			}
			//<<<step7>>>--End
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
			int iRecLineSpacing;
			int iRecLineHeight;
			bool bBuffering = true;
			Cursor.Current = Cursors.WaitCursor;

			iRecLineSpacing = m_Printer.RecLineSpacing;
			iRecLineHeight = m_Printer.RecLineHeight;

			if(m_Printer.CapRecPresent == true)
			{
				try
				{

					m_Printer.TransactionPrint(PrinterStation.Receipt
						,PrinterTransactionControl.Transaction);
			
					m_Printer.RotatePrint(PrinterStation.Receipt,PrintRotation.Left90);

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|4C" + "\u001b|bC"
						+ "   Receipt     ");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|3C" + "\u001b|2uC"
						+ "       Mr. Brawn\n");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|2uC"
						+ "                                                  \n\n");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|2uC" + "\u001b|3C"
						+ "        Total payment              $" +"\u001b|4C" + "21.00  \n");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1C\n" );

					m_Printer.PrintNormal(PrinterStation.Receipt,strDate + " Received\n\n");

					m_Printer.RecLineHeight = 24;
					m_Printer.RecLineSpacing = m_Printer.RecLineHeight;
			
					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC"
						+ " Details               \n");

					m_Printer.PrintNormal(PrinterStation.Receipt
						,"\u001b|1C" + "                          " + "\u001b|2C" + "OPOS Store\n");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC" 
						+ " Tax excluded    $20.00\n");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|1C"
						+ "                          " + "\u001b|bC" + "Zip code 999-9999\n");

					m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC" 
						+ " Tax(5%)        $1.00" + "\u001b|N" + "    Phone#(9999)99-9998\n");

				}
				catch(PosControlException ex)
				{
					if(ex.ErrorCode == ErrorCode.Illegal && ex.ErrorCodeExtended == 1004)
					{
						MessageBox.Show("Unable to print receipt.\n"
							,"Printer_SampleStep9",MessageBoxButtons.OK,MessageBoxIcon.Warning);

						// Clear the buffered data since the buffer retains print data when an error occurs during printing.
						m_Printer.ClearOutput();
						bBuffering = false;
					}
				}

				try
				{
					m_Printer.RotatePrint(PrinterStation.Receipt,PrintRotation.Normal);

					if(bBuffering == true)
					{
						m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|fP");
					}

					m_Printer.TransactionPrint(PrinterStation.Receipt,PrinterTransactionControl.Normal);
				}
				catch(PosControlException)
				{
					// Clear the buffered data since the buffer retains print data when an error occurs during printing.
					m_Printer.ClearOutput();
				}
				
			}

			m_Printer.RecLineSpacing = iRecLineSpacing;
			m_Printer.RecLineHeight = iRecLineHeight;
			Cursor.Current = Cursors.Default;
			//<<<step8>>>--End
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
			DialogResult dialogResult;

			try
			{
				//Request for inserting a slip
				while(true)
				{
					dialogResult = MessageBox.Show("Insert Slp","PrinterSample_Step9"
						,MessageBoxButtons.OKCancel);

					if(dialogResult == DialogResult.OK)
					{
						try
						{
							//wait time is 5 minute from set paper
							m_Printer.BeginInsertion(5000);
							m_Printer.EndInsertion();
							break;
						}
						catch(PosControlException)
						{
							continue;
						}
					}
					else if(dialogResult == DialogResult.Cancel)
					{
						return;
					}
				}
			
				Cursor.Current = Cursors.WaitCursor;

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
				strPrintData = strPrintData + strSpace + "\u001b|2C     Total" + "\u001b|1C     $ 367.50\n";
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
				m_Printer.BeginRemoval(10000);
				m_Printer.EndRemoval();
			}
			catch(PosControlException)
			{
			}
			//<<<Step9>>>--End
		}

		/// <summary>
		/// Form close.
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
		private void frmStep9_Load(object sender, System.EventArgs e)
		{
			
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "PosPrinter";
						
			//Current Directory Path
			string strCurDir = Directory.GetCurrentDirectory();

			string strFilePath = strCurDir.Substring(0,strCurDir.LastIndexOf("Step9") + "Step9\\".Length);

			strFilePath += "Logo.bmp";

			try
			{
				//Create PosExplorer
				PosExplorer posExplorer = new PosExplorer();
			
				DeviceInfo deviceInfo = null;
				
				try
				{
					deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter,strLogicalName);
					m_Printer =(PosPrinter)posExplorer.CreateInstance(deviceInfo);
				}
				catch(Exception)
				{
					ChangeButtonStatus();
					return;
				}

				//Register OutputCompleteEventHandler.
				AddOutputComplete(m_Printer);
			
				//Open the device
				m_Printer.Open();

				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Printer.Claim(2000);

				//Enable the device.
				m_Printer.DeviceEnabled = true;

				//<<<step3>>>--Start
				//Output by the high quality mode
				m_Printer.RecLetterQuality = true;

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
                        MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep9"
                                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
				//<<<step3>>>--End

				//<<<step5>>>--Start
				// Even if using any printers, 0.01mm unit makes it possible to print neatly.
				m_Printer.MapMode = MapMode.Metric;
				//<<<step5>>>--End

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
					//Not function to [Print Sales slp] button is disable
					btnSales.Enabled = false;
				}
				//<<<step9>>>--End

			}
			catch(Exception ex)
			{
				ChangeButtonStatus();
			}
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep9_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
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
			//<<<step1>>>--End
		}

		/// <summary>
		/// Add OutputCompleteEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddOutputComplete(object eventSource)
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
		/// Remove OutputCompleteEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveOutputComplete(object eventSource)
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
		/// OutputComplete Event
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected  void OnOutputCompleteEvent(object source, OutputCompleteEventArgs e)
		{
			//<<<step7>>>--Start
			//Notify that printing is completed when it is asnchronous.
			MessageBox.Show("Complete printing : ID = " + e.OutputId.ToString()
				,"PrinterSample_Step9");
			//<<<step7>>>--End
		}
	}
}
