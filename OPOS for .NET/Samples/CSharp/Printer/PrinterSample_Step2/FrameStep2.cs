using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Globalization;

namespace PrinterSample_Step2
{
	/// <summary>
	/// Description of FrameStep2.
	/// </summary>
	public class FrameStep2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Design  variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep2()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnPrint);
			this.groupBox1.Location = new System.Drawing.Point(24, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(248, 72);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Receipt";
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(68, 28);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(116, 28);
			this.btnPrint.TabIndex = 0;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(152, 120);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(116, 28);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// FrameStep2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(308, 165);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.groupBox1);
			this.MaximizeBox = false;
			this.Name = "FrameStep2";
			this.Text = "Step 2  Print various types of characters.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep2_Closing);
			this.Load += new System.EventHandler(this.frmStep2_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep2());
		}

		/// <summary>
		/// PosPrinter object
		/// </summary>
		PosPrinter m_Printer = null;

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
			string strDate = nowDate.ToString("MMMM,dd,yyyy,  HH:mm",dateFormat);
			
			try
			{
				//Print address
				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|N"
					+ "123xxstreet,xxxcity,xxxxstate\n");

				//Print phone number
				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|rA"
					+"TEL 9999-99-9999   C#2\n");
				//Print date
				//   \u001b|cA = Centaring char
				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|cA" + strDate + "\n\n");
				//Print buying goods
				m_Printer.PrintNormal(PrinterStation.Receipt, "apples                  $20.00\n");

				m_Printer.PrintNormal(PrinterStation.Receipt, "grapes                  $30.00\n");

				m_Printer.PrintNormal(PrinterStation.Receipt, "bananas                 $40.00\n");

				m_Printer.PrintNormal(PrinterStation.Receipt, "lemons                  $50.00\n");

				m_Printer.PrintNormal(PrinterStation.Receipt, "oranges                 $60.00\n\n");

				//Print the total cost
				//\u001b|bC = Bold
				//\u001b|uC = Underline
				//\u001b|2C = Wide charcter
				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|bC" 
					+ "Tax excluded.          $200.00" +  "\u001b|N\n");

				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|uC"
					+ "Tax  5.0%               $10.00" +  "\u001b|N\n");

				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|bC" +  "\u001b|2C" 
					+ "Total   $210.00" + "\u001b|N\n");

				m_Printer.PrintNormal(PrinterStation.Receipt,"Customer's payment     $250.00\n");
				m_Printer.PrintNormal(PrinterStation.Receipt,"Change                  $40.00\n\n");

				//Feed the receipt to the cutter position automatically, and cut.
				//   \u001b|#fP = Line Feed and Paper cut	
				m_Printer.PrintNormal(PrinterStation.Receipt,"\u001b|fP");
			}
			catch(PosControlException)
			{
			}
			//<<<step2>>>--End
		}

		/// <summary>
		/// When the method "changeButtonStatus" was called,
		/// all buttons other than a button "closing" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnPrint.Enabled = false;
		}

		/// <summary>
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
		private void frmStep2_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "PosPrinter";
			
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
		private void frmStep2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
