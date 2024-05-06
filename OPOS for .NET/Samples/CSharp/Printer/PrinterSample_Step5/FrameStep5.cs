using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.IO;
using System.Globalization;

namespace PrinterSample_Step5
{
	/// <summary>
	/// Description of FrameStep5.
	/// </summary>
	public class FrameStep5 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpReceipt;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Design  variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep5()
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
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.grpReceipt.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpReceipt
			// 
			this.grpReceipt.Controls.Add(this.btnPrint);
			this.grpReceipt.Location = new System.Drawing.Point(60, 40);
			this.grpReceipt.Name = "grpReceipt";
			this.grpReceipt.Size = new System.Drawing.Size(320, 116);
			this.grpReceipt.TabIndex = 0;
			this.grpReceipt.TabStop = false;
			this.grpReceipt.Text = "Receipt";
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(92, 48);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(112, 24);
			this.btnPrint.TabIndex = 0;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(264, 172);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(112, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmStep5
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(436, 213);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.grpReceipt);
			this.MaximizeBox = false;
			this.Name = "frmStep5";
			this.Text = "Step 5  Writing a module that is independent of device types";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep5_Closing);
			this.Load += new System.EventHandler(this.frmStep5_Load);
			this.grpReceipt.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep5());
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
		///  A method "Print" calls some another method.
		///  They are method for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>--Start
			//Initialization
			DateTime nowDate = DateTime.Now;							//System date
			DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
			dateFormat.MonthDayPattern = "MMMM";
			string strDate = nowDate.ToString("MMMM,dd,yyyy  HH:mm",dateFormat);
			string strbcData = "4902720005074";
            int[] RecLineChars = new int[MAX_LINE_WIDTHS] { 0, 0 };
            long lRecLineCharsCount;

			String[] astritem = {"apples", "grapes", "bananas", "lemons", "oranges"};
			String[] astrprice = {"10.00", "20.00", "30.00", "40.00", "50.00"};
		
			try
			{
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
			}
			catch(PosControlException)
			{
			}
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
		/// When the method "changeButtonStatus" was called,
		/// all buttons other than a button "closing" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnPrint.Enabled = false;
		}

		// <summary>
		/// Form close
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
		private void frmStep5_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "PosPrinter";
						
			//Current Directory Path
			string strCurDir = Directory.GetCurrentDirectory();

			string strFilePath = strCurDir.Substring(0,strCurDir.LastIndexOf("Step5") + "Step5\\".Length);

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

				//Open the device
				m_Printer.Open();

				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Printer.Claim(1000);

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
                        MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep5"
                                , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
				//<<<step3>>>--End

				//<<<step5>>>--Start
				// Even if using any printers, 0.01mm unit makes it possible to print neatly.
				m_Printer.MapMode = MapMode.Metric;
				//<<<step5>>>--End
			}
			catch(PosControlException)
			{
				ChangeButtonStatus();
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep5_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
	}
}
