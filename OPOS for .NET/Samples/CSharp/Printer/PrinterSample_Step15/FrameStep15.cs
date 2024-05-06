using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.IO;

namespace PrinterSample_Step15
{
	/// <summary>
	/// Description of FrameStep15.
	/// </summary>
	public class FrameStep15 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnPrint;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep15()
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
			this.btnPrint = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(80, 48);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(108, 36);
			this.btnPrint.TabIndex = 0;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// frmStep15
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(272, 133);
			this.Controls.Add(this.btnPrint);
			this.MaximizeBox = false;
			this.Name = "frmStep15";
			this.Text = "Step15 Print Memory Bitmap";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep15_Closing);
			this.Load += new System.EventHandler(this.frmStep15_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep15());
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
			//<<<step1>>> Start
			try
			{
                // Get the file path for the bitmap (which is located in the current directory)
                string strCurDir = Directory.GetCurrentDirectory();
                string strFilePath = strCurDir.Substring(0, strCurDir.LastIndexOf("Step15") + "Step15\\".Length);

                // Append the bitmap file name
                strFilePath += "Logo.bmp";

                // Create the bitmap object from the file
                System.Drawing.Bitmap bitmap = null;
                bitmap = new System.Drawing.Bitmap(strFilePath, true);

                // Call the Print Memory Bitmap function
                m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, bitmap, m_Printer.RecLineWidth, PosPrinter.PrinterBitmapCenter);
			}
			catch(PosControlException)
			{
			}
			//<<<step1>>> End
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
		/// The processing code required in order to enable to use of service is written here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep15_Load(object sender, System.EventArgs e)
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
		private void frmStep15_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
