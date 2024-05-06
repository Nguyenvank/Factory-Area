using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;

namespace DisplaySample_Step5
{
	/// <summary>
	/// Description of FrameStep5.
	/// </summary>
	public class FrameStep5 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnSpecifyPosition;
		private System.Windows.Forms.Button btnBlinkCharacter;
		private System.Windows.Forms.Button btnTeletypeCharacters;
		private System.Windows.Forms.GroupBox grpScroll;
		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Button btnRight;
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
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnSpecifyPosition = new System.Windows.Forms.Button();
			this.btnBlinkCharacter = new System.Windows.Forms.Button();
			this.btnTeletypeCharacters = new System.Windows.Forms.Button();
			this.grpScroll = new System.Windows.Forms.GroupBox();
			this.btnRight = new System.Windows.Forms.Button();
			this.btnLeft = new System.Windows.Forms.Button();
			this.grpScroll.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(344, 140);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 28);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(28, 20);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(188, 24);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnSpecifyPosition
			// 
			this.btnSpecifyPosition.Location = new System.Drawing.Point(28, 60);
			this.btnSpecifyPosition.Name = "btnSpecifyPosition";
			this.btnSpecifyPosition.Size = new System.Drawing.Size(188, 24);
			this.btnSpecifyPosition.TabIndex = 3;
			this.btnSpecifyPosition.Text = "Specify Position and Display";
			this.btnSpecifyPosition.Click += new System.EventHandler(this.btnSpecifyPosition_Click);
			// 
			// btnBlinkCharacter
			// 
			this.btnBlinkCharacter.Location = new System.Drawing.Point(28, 100);
			this.btnBlinkCharacter.Name = "btnBlinkCharacter";
			this.btnBlinkCharacter.Size = new System.Drawing.Size(188, 24);
			this.btnBlinkCharacter.TabIndex = 4;
			this.btnBlinkCharacter.Text = "Blink Characters";
			this.btnBlinkCharacter.Click += new System.EventHandler(this.btnBlinkCharacter_Click);
			// 
			// btnTeletypeCharacters
			// 
			this.btnTeletypeCharacters.Location = new System.Drawing.Point(28, 140);
			this.btnTeletypeCharacters.Name = "btnTeletypeCharacters";
			this.btnTeletypeCharacters.Size = new System.Drawing.Size(188, 24);
			this.btnTeletypeCharacters.TabIndex = 5;
			this.btnTeletypeCharacters.Text = "Teletype Characters";
			this.btnTeletypeCharacters.Click += new System.EventHandler(this.btnTeletypeCharacters_Click);
			this.btnTeletypeCharacters.Leave += new System.EventHandler(this.btnTeletypeCharacters_Leave);
			// 
			// grpScroll
			// 
			this.grpScroll.Controls.Add(this.btnRight);
			this.grpScroll.Controls.Add(this.btnLeft);
			this.grpScroll.Location = new System.Drawing.Point(244, 20);
			this.grpScroll.Name = "grpScroll";
			this.grpScroll.Size = new System.Drawing.Size(204, 96);
			this.grpScroll.TabIndex = 6;
			this.grpScroll.TabStop = false;
			this.grpScroll.Text = "Scroll";
			// 
			// btnRight
			// 
			this.btnRight.Location = new System.Drawing.Point(120, 36);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(72, 24);
			this.btnRight.TabIndex = 1;
			this.btnRight.Text = "Right";
			this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
			// 
			// btnLeft
			// 
			this.btnLeft.Location = new System.Drawing.Point(16, 36);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(72, 24);
			this.btnLeft.TabIndex = 0;
			this.btnLeft.Text = "Left";
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// frmStep5
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(464, 173);
			this.Controls.Add(this.grpScroll);
			this.Controls.Add(this.btnTeletypeCharacters);
			this.Controls.Add(this.btnBlinkCharacter);
			this.Controls.Add(this.btnSpecifyPosition);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "frmStep5";
			this.Text = "Step 5 Scroll displayed characters.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep5_Closing);
			this.Load += new System.EventHandler(this.frmStep5_Load);
			this.grpScroll.ResumeLayout(false);
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

		/// <summary>
		/// LineDisplay object.
		/// </summary>
		LineDisplay m_Display = null;

		/// <summary>
		/// When the button "specifyPosition" is pushed,
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
		/// When the button "blinkCharactersn" is pushed,
		/// the method "displayText" is run.
		/// "Blink" is run in the case shown below.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBlinkCharacter_Click(object sender, System.EventArgs e)
		{
			//<<<step3>>>--Start
			try
			{
				m_Display.DisplayText("Blink",DisplayTextMode.Blink);
			}
			catch(PosControlException)
			{
			}
			//<<<step3>>>--End
		}

		/// <summary>
		/// When the button "teletypeCharacters" is pushed,
		/// the method "displayText" is run.
		/// in the case shown below.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTeletypeCharacters_Click(object sender, System.EventArgs e)
		{
			//<<<step4>>>--Start
			try
			{
				//wait 1.0 second for the next character displaied.
				m_Display.InterCharacterWait = 1000;
				m_Display.DisplayText("Teletype",DisplayTextMode.Normal);
			}
			catch(PosControlException)
			{
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// When the button "btnTeletypeCharacters" is leave forcus,
		/// "InterCharacterWait" reset.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTeletypeCharacters_Leave(object sender, System.EventArgs e)
		{
			//<<<step4>>>--Start
            try
            {
                m_Display.InterCharacterWait = 0;
            }
            catch (PosControlException)
            {
            }
			//<<<step4>>>--End

		}

		/// <summary>
		/// When the button "left" is pushed,
		/// a method "scrollText" is run.
		/// A first paramete orders the scrolling in the left direction.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLeft_Click(object sender, System.EventArgs e)
		{
			//<<<step5>>>--Start
			try
			{
				//scrollText(int direction, int units)
				//Move the character to the left side for a column.
				m_Display.ScrollText(DisplayScrollText.Left, 1);
			}
			catch(PosControlException)
			{
			}
			//<<<step5>>>--End
		}

		/// <summary>
		/// When the button "right" is pushed,
		/// a method "scrollText" is run.
		/// A first paramete orders the scrolling in the right direction.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRight_Click(object sender, System.EventArgs e)
		{
			//<<<step5>>>--Start
			try
			{
				//scrollText(int direction, int units)
				//Move the character to the right side for a column.
				m_Display.ScrollText(DisplayScrollText.Right, 1);
			}
			catch(PosControlException)
			{
			}		
			//<<<step5>>>--End
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
			btnBlinkCharacter.Enabled = false;
			btnTeletypeCharacters.Enabled = false;
			btnLeft.Enabled = false;
			btnRight.Enabled = false;
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
		private void frmStep5_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
