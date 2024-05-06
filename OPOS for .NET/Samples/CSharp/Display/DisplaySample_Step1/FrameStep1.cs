using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;

namespace DisplaySample_Step1
{
	/// <summary>
	/// Description of frmStep1.
	/// </summary>
	public class FrameStep1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnDisplayText;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep1()
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
			this.btnDisplayText = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnDisplayText
			// 
			this.btnDisplayText.Location = new System.Drawing.Point(40, 52);
			this.btnDisplayText.Name = "btnDisplayText";
			this.btnDisplayText.Size = new System.Drawing.Size(116, 28);
			this.btnDisplayText.TabIndex = 0;
			this.btnDisplayText.Text = "Text Display";
			this.btnDisplayText.Click += new System.EventHandler(this.btnDisplayText_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(208, 16);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(76, 28);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmStep1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(292, 121);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDisplayText);
			this.MaximizeBox = false;
			this.Name = "frmStep1";
			this.Text = "Step 1 Display characters.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep1_Closing);
			this.Load += new System.EventHandler(this.frmStep1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep1());
		}

		/// <summary>
		/// LineDisplay object.
		/// </summary>
		LineDisplay m_Display = null;

		/// <summary>
		///  Display characters for LineDisplay.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDisplayText_Click(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			try
			{
				m_Display.DisplayText("Hello OPOS for .NET",DisplayTextMode.Normal);
			}
			catch(PosControlException)
			{
			}
			//<<<step1>>>--End
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
		/// When the method "ChangeButtonStatus" was called,
		/// all buttons other than a button "Close" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnDisplayText.Enabled = false;
		}

		/// <summary>
		/// The processing code required in order to enable to use of service is written here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep1_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "LineDisplay";
			
			try
			{
				//Create PosExplorer
				PosExplorer posExplorer = new PosExplorer();
	
				DeviceInfo deviceInfo = null;

				try
				{
					deviceInfo = posExplorer.GetDevice(DeviceType.LineDisplay,strLogicalName);
					m_Display =(LineDisplay)posExplorer.CreateInstance(deviceInfo);
				}
				catch(Exception)
				{
					ChangeButtonStatus();
					return;
				}

				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Display.Open();

				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Display.Claim(1000);
		
				//If support the CapPowerReporting, enable the Power Reporting Requirements.
				if(m_Display.CapPowerReporting != PowerReporting.None)
				{
					m_Display.PowerNotify = PowerNotification.Enabled;
				}

				//Enable the device.
				m_Display.DeviceEnabled = true;
				
			}
			catch(PosControlException)
			{
				ChangeButtonStatus();
				return;
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Display != null)
			{
				try
				{
					//For clear the text on the window be in current use.
					m_Display.ClearText();

					//Cancel the device
					m_Display.DeviceEnabled = false;

					//Release the device exclusive control right.
					m_Display.Release();

				}
				catch(PosControlException)
				{
				}
				finally
				{
					//Finish using the device.
					m_Display.Close();
				}
			}
			//<<<step1>>>--End
		}
	}
}
