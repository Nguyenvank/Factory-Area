using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;

namespace DisplaySample_Step10
{
	/// <summary>
	/// Description of FrameStep10.
	/// </summary>
	public class FrameStep10 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnExternalCharacter;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep10()
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
			this.btnExternalCharacter = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnExternalCharacter
			// 
			this.btnExternalCharacter.Location = new System.Drawing.Point(24, 52);
			this.btnExternalCharacter.Name = "btnExternalCharacter";
			this.btnExternalCharacter.Size = new System.Drawing.Size(164, 48);
			this.btnExternalCharacter.TabIndex = 0;
			this.btnExternalCharacter.Text = "External Character Registration";
			this.btnExternalCharacter.Click += new System.EventHandler(this.btnExternalCharacter_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(224, 20);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(64, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmStep10
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(292, 125);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnExternalCharacter);
			this.MaximizeBox = false;
			this.Name = "frmStep10";
			this.Text = "Step 10 External character registration";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep10_Closing);
			this.Load += new System.EventHandler(this.frmStep10_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep10());
		}

		/// <summary>
		/// LineDisplay object.
		/// </summary>
		LineDisplay m_Display = null;

		/// <summary>
		/// When the button "ExternalCharacter" is pushed, The method
		/// "DefineGlypth" is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExternalCharacter_Click(object sender, System.EventArgs e)
		{
			//<<<step10>>>--Start
			//GlypthData
			string strData = "1c070000000000";
			
			//sample2 Case DM-D500
			//strData = "ff9999ffff999999ff00000000000000";

			byte[] byGlyph = new Byte[strData.Length/2];

			for( int i=0; i < byGlyph.Length; i++ )
			{
				byGlyph[i] = Convert.ToByte( strData.Substring(0, 2), 16 );
				strData = strData.Substring( 2, strData.Length - 2 );
			}
		
			try
			{
				m_Display.DefineGlyph(65,byGlyph);
                m_Display.DisplayText("A", DisplayTextMode.Normal);
			}
			catch(PosControlException)
			{
                ChangeButtonStatus(false);
                return;
			}
			
			//<<<step10>>>--End
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
		/// all buttons other than a button "Close" become valid or invalid based on bflag.
		/// </summary>
		private void ChangeButtonStatus(bool bflag)
		{
			btnExternalCharacter.Enabled = bflag;
		}

		/// <summary>
		/// The processing code required in order to enable to use of service is written here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep10_Load(object sender, System.EventArgs e)
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
					ChangeButtonStatus(false);
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
			catch(Exception)
			{
				ChangeButtonStatus(false);
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
		private void frmStep10_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
