using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;

namespace DisplaySample_Step2
{
	/// <summary>
	/// Description of FrameStep2.
	/// </summary>
	public class FrameStep2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnSpecifyPosition;
		private System.Windows.Forms.Button btnAcquisition;
		private System.Windows.Forms.TextBox txtAcquisition;
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
		/// The method is required ofr Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. 
		/// The Forms designer might not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnSpecifyPosition = new System.Windows.Forms.Button();
			this.btnAcquisition = new System.Windows.Forms.Button();
			this.txtAcquisition = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(296, 20);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(32, 20);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(188, 24);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnSpecifyPosition
			// 
			this.btnSpecifyPosition.Location = new System.Drawing.Point(32, 60);
			this.btnSpecifyPosition.Name = "btnSpecifyPosition";
			this.btnSpecifyPosition.Size = new System.Drawing.Size(188, 24);
			this.btnSpecifyPosition.TabIndex = 3;
			this.btnSpecifyPosition.Text = "Specify Position and Display";
			this.btnSpecifyPosition.Click += new System.EventHandler(this.btnSpecifyPosition_Click);
			// 
			// btnAcquisition
			// 
			this.btnAcquisition.CausesValidation = false;
			this.btnAcquisition.Location = new System.Drawing.Point(32, 96);
			this.btnAcquisition.Name = "btnAcquisition";
			this.btnAcquisition.Size = new System.Drawing.Size(188, 32);
			this.btnAcquisition.TabIndex = 4;
			this.btnAcquisition.Text = "Specify Position and Acquire Character";
			this.btnAcquisition.Click += new System.EventHandler(this.btnAcquisition_Click);
			// 
			// txtAcquisition
			// 
			this.txtAcquisition.BackColor = System.Drawing.SystemColors.Window;
			this.txtAcquisition.Location = new System.Drawing.Point(232, 104);
			this.txtAcquisition.Name = "txtAcquisition";
			this.txtAcquisition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAcquisition.Size = new System.Drawing.Size(50, 19);
			this.txtAcquisition.TabIndex = 5;
			this.txtAcquisition.Tag = "(Hex)";
			this.txtAcquisition.Text = "";
			// 
			// FrameStep2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(408, 145);
			this.Controls.Add(this.txtAcquisition);
			this.Controls.Add(this.btnAcquisition);
			this.Controls.Add(this.btnSpecifyPosition);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "FrameStep2";
			this.Text = "Step 2 Display and acquire characters at the specified position.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep2_Closing);
			this.Load += new System.EventHandler(this.frmStep2_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep2());
		}
		
		/// <summary>
		/// LineDisplay object.
		/// </summary>
		LineDisplay m_Display = null;

		/// <summary>
		/// When the button "SpecifyPosition" is pushed,
		/// the method "displayTextAt" is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSpecifyPosition_Click(object sender, System.EventArgs e)
		{
			
			//<<<step2>>>--Start
			try
			{
				//For display the text that specified.
				//displayTextAt(int row, int column, string data)
				m_Display.DisplayTextAt(1,5,"Hello OPOS for .NET",DisplayTextMode.Normal);
			}
			catch(PosControlException)
			{
			}
			//<<<step2>>>--End
		}

		/// <summary>
		/// When the button "clear" is pushed, The method
		/// "clearText" is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>--Start
			try
			{
				m_Display.ClearText();
			}
			catch(PosControlException)
			{
			}
			//<<<step2>>>--End
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
			btnClear.Enabled = false;
			btnSpecifyPosition.Enabled = false;
			btnAcquisition.Enabled = false;
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
		private void frmStep2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

		/// <summary>
		/// When the method "Specify Position and Acquisition" button is clicked,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAcquisition_Click(object sender, System.EventArgs e)
		{
			try
			{
				//The present cursor position is set up.
				m_Display.CursorRow = 1;
				m_Display.CursorColumn = 5;
				//The character of the present cursor position is acquired.
				int iDisplayChar = m_Display.ReadCharacterAtCursor();
				txtAcquisition.Text = iDisplayChar.ToString();
			}
			catch
			{
				MessageBox.Show("Acquisition Failure");
			}
												  
	
		}

	}
}
