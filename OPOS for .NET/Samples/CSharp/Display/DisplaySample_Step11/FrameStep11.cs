using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;
using jp.co.epson.uposcommon;
using System.IO;
using System.Diagnostics;


namespace DisplaySample_Step11
{
	/// <summary>
	/// Description of FrameStep11.
	/// </summary>
	public class FrameStep11 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnSpecifyPosition;
		private System.Windows.Forms.Button btnBlinkCharacter;
		private System.Windows.Forms.Button btnTeletypeCharacters;
		private System.Windows.Forms.GroupBox grpScroll;
		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Button btnRight;
		private System.Windows.Forms.Button btnWindowControl;
		private System.Windows.Forms.GroupBox grpGraphicDisplay;
		private System.Windows.Forms.Button btnSetChracterOrnaments;
		private System.Windows.Forms.Button btnDisplayBitmap;
		private System.Windows.Forms.GroupBox grpStatistics;
		private System.Windows.Forms.Button btnRetrieveStatistics;
		private System.Windows.Forms.TextBox txtStatistics;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

        private int iPreviousStatus = -1;

		public FrameStep11()
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
			this.btnWindowControl = new System.Windows.Forms.Button();
			this.grpGraphicDisplay = new System.Windows.Forms.GroupBox();
			this.btnDisplayBitmap = new System.Windows.Forms.Button();
			this.btnSetChracterOrnaments = new System.Windows.Forms.Button();
			this.grpStatistics = new System.Windows.Forms.GroupBox();
			this.txtStatistics = new System.Windows.Forms.TextBox();
			this.btnRetrieveStatistics = new System.Windows.Forms.Button();
			this.grpScroll.SuspendLayout();
			this.grpGraphicDisplay.SuspendLayout();
			this.grpStatistics.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(384, 328);
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
			this.grpScroll.Location = new System.Drawing.Point(248, 20);
			this.grpScroll.Name = "grpScroll";
			this.grpScroll.Size = new System.Drawing.Size(244, 76);
			this.grpScroll.TabIndex = 6;
			this.grpScroll.TabStop = false;
			this.grpScroll.Text = "Scroll";
			// 
			// btnRight
			// 
			this.btnRight.Location = new System.Drawing.Point(144, 36);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(72, 24);
			this.btnRight.TabIndex = 1;
			this.btnRight.Text = "Right";
			this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
			// 
			// btnLeft
			// 
			this.btnLeft.Location = new System.Drawing.Point(28, 36);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(72, 24);
			this.btnLeft.TabIndex = 0;
			this.btnLeft.Text = "Left";
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// btnWindowControl
			// 
			this.btnWindowControl.Location = new System.Drawing.Point(28, 176);
			this.btnWindowControl.Name = "btnWindowControl";
			this.btnWindowControl.Size = new System.Drawing.Size(188, 24);
			this.btnWindowControl.TabIndex = 7;
			this.btnWindowControl.Text = "Windows Control";
			this.btnWindowControl.Click += new System.EventHandler(this.btnWindowControl_Click);
			// 
			// grpGraphicDisplay
			// 
			this.grpGraphicDisplay.Controls.Add(this.btnDisplayBitmap);
			this.grpGraphicDisplay.Controls.Add(this.btnSetChracterOrnaments);
			this.grpGraphicDisplay.Location = new System.Drawing.Point(248, 108);
			this.grpGraphicDisplay.Name = "grpGraphicDisplay";
			this.grpGraphicDisplay.Size = new System.Drawing.Size(244, 96);
			this.grpGraphicDisplay.TabIndex = 8;
			this.grpGraphicDisplay.TabStop = false;
			this.grpGraphicDisplay.Text = "GraphicDisplay only";
			// 
			// btnDisplayBitmap
			// 
			this.btnDisplayBitmap.Location = new System.Drawing.Point(32, 60);
			this.btnDisplayBitmap.Name = "btnDisplayBitmap";
			this.btnDisplayBitmap.Size = new System.Drawing.Size(188, 24);
			this.btnDisplayBitmap.TabIndex = 7;
			this.btnDisplayBitmap.Text = "DisplayBitmap";
			this.btnDisplayBitmap.Click += new System.EventHandler(this.btnDisplayBitmap_Click);
			// 
			// btnSetChracterOrnaments
			// 
			this.btnSetChracterOrnaments.Location = new System.Drawing.Point(32, 24);
			this.btnSetChracterOrnaments.Name = "btnSetChracterOrnaments";
			this.btnSetChracterOrnaments.Size = new System.Drawing.Size(188, 24);
			this.btnSetChracterOrnaments.TabIndex = 6;
			this.btnSetChracterOrnaments.Text = "Set Character Ornaments";
			this.btnSetChracterOrnaments.Click += new System.EventHandler(this.btnSetChracterOrnaments_Click);
			// 
			// grpStatistics
			// 
			this.grpStatistics.Controls.Add(this.txtStatistics);
			this.grpStatistics.Controls.Add(this.btnRetrieveStatistics);
			this.grpStatistics.Location = new System.Drawing.Point(248, 220);
			this.grpStatistics.Name = "grpStatistics";
			this.grpStatistics.Size = new System.Drawing.Size(244, 96);
			this.grpStatistics.TabIndex = 9;
			this.grpStatistics.TabStop = false;
			this.grpStatistics.Text = "Device Statistics";
			// 
			// txtStatistics
			// 
			this.txtStatistics.Location = new System.Drawing.Point(32, 20);
			this.txtStatistics.Name = "txtStatistics";
			this.txtStatistics.Size = new System.Drawing.Size(188, 19);
			this.txtStatistics.TabIndex = 8;
			this.txtStatistics.Text = "ModelName,HoursPoweredCount,OnlineTransitionCount";
			// 
			// btnRetrieveStatistics
			// 
			this.btnRetrieveStatistics.Location = new System.Drawing.Point(32, 60);
			this.btnRetrieveStatistics.Name = "btnRetrieveStatistics";
			this.btnRetrieveStatistics.Size = new System.Drawing.Size(188, 24);
			this.btnRetrieveStatistics.TabIndex = 7;
			this.btnRetrieveStatistics.Text = "Retrieve Statistics";
			this.btnRetrieveStatistics.Click += new System.EventHandler(this.btnRetrieveStatistics_Click);
			// 
			// frmStep11
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(508, 365);
			this.Controls.Add(this.grpStatistics);
			this.Controls.Add(this.grpGraphicDisplay);
			this.Controls.Add(this.btnWindowControl);
			this.Controls.Add(this.grpScroll);
			this.Controls.Add(this.btnTeletypeCharacters);
			this.Controls.Add(this.btnBlinkCharacter);
			this.Controls.Add(this.btnSpecifyPosition);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "frmStep11";
			this.Text = "Step 11 Obtains the statistics of the device.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep11_Closing);
			this.Load += new System.EventHandler(this.frmStep11_Load);
			this.grpScroll.ResumeLayout(false);
			this.grpGraphicDisplay.ResumeLayout(false);
			this.grpStatistics.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep11());
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
		/// When the button "windowsControl" is pushed,
		/// a method is run, and a property is set.
		/// These codes realize "Marquee".
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnWindowControl_Click(object sender, System.EventArgs e)
		{
			//<<<step6>>>--Start
			try
			{
				//createWindow(int viewportRow, int viewportColumn, int viewportHeight,
				//int viewportWidth, int windowHeight, int windowWidth);
				m_Display.CreateWindow(1,10,1,10,1,34);

				//When "MarqueeFormat" is "DisplayMarqueeFormat.Walk",
				//put a character on the display one by one from the reverse of the side that
				//you selected as The direction of "Marquee".
				//The direction of "Marquee" is that you selected as "MarqueeType".
				m_Display.MarqueeFormat = DisplayMarqueeFormat.Walk;

				//When the "MarqueeType" is "DisplayMarqueeType.Init",
				//  The change of the setting from "DISP_MT_INIT" permits that the setting of "String data" and
				//  "MarqueeFormat" becomes effective.
				m_Display.MarqueeType = DisplayMarqueeType.Init;

				//It is 1.0 second that the next head of "String data" starts
				//  after the end of "String data" was displayed.
				m_Display.MarqueeRepeatWait = 1000;

				//It takes 0.1 second that the next moving satarts
				//  after the end of the one moving of unit.
				m_Display.MarqueeUnitWait = 100;

				m_Display.DisplayText("Sale! 50%-20% OFF!",DisplayTextMode.Normal);

				//For set the direction as "MarqueeType". For example, the left and the right.
				//disp.setMarqueeType(LineDisplayConst.DISP_MT_LEFT);
				m_Display.MarqueeType = DisplayMarqueeType.Left;

				MessageBox.Show("When pressing OK, it ends.","DisplaySample_Step11",MessageBoxButtons.OK);

				m_Display.MarqueeType = DisplayMarqueeType.None;
				m_Display.DestroyWindow();
			}
			catch(PosControlException)
			{
			}
			//<<<step6>>>--End
		}

		/// <summary>
		/// When the button "setcaracterOrnaments" is pushed,
		/// a method "displayTextAt" is run.
		/// These codes realize that the various kinds of
		/// characters is displayed by using "ESC".
		///  "ESC" means Escape sequence code.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSetChracterOrnaments_Click(object sender, System.EventArgs e)
		{
			//<<<step8>>>--Start
			try
			{
				//For clear the text on the window be in current use.
				m_Display.ClearText();
				
				//It prints by "Bold" using an escape sequence code.
				m_Display.DisplayTextAt(0,0,"Normal" + "\u001b|bCBold",DisplayTextMode.Normal);

				//"\u001b|rvC" is the command which reverses a character. 
				m_Display.DisplayTextAt(1,0,"\u001b|rvCReverse" + "\u001b|bCBold&Reverse"
					,DisplayTextMode.Normal);

				m_Display.DisplayText("\u001b|bCBold",DisplayTextMode.Normal);
			}
			catch(PosControlException)
			{
			}
			//<<<step8>>>--End
		}

		/// <summary>
		/// When the button "displayBitmap" is pushed,
		/// a method "directIO" is run.
		/// These codes realize that a bitmap is displayed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDisplayBitmap_Click(object sender, System.EventArgs e)
		{
			//<<<step9>>>--Start
			//Current Directory Path
			string strCurDir = Directory.GetCurrentDirectory();

			string strFilePath = strCurDir.Substring(0,strCurDir.LastIndexOf("bin"));

			strFilePath += "Epson.bmp";
			
			try
			{
				int iData = 0;
				byte[] abyte = new Byte[1];
				//The command "DISP_DI_GRAPHIC".
				m_Display.DirectIO(EpsonLineDisplayConst.DISP_DI_GRAPHIC,iData,abyte);
			
				m_Display.CreateWindow(0,0,64,256,64,256);

				m_Display.DisplayBitmap(strFilePath,64,LineDisplay.DisplayBitmapLeft
					,LineDisplay.DisplayBitmapTop);

				MessageBox.Show("When pressing OK, delete the window."
					,"CheckSample_Step9",MessageBoxButtons.OK);

				m_Display.DestroyWindow();
			
			}
			catch(PosControlException)
			{
			}
			//<<<step9>>>--End
		}

		/// <summary>
		/// When the button "RetrieveStatistics" is pushed,
		/// a method "RetrieveStatistics" is run.
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
				strReturn = m_Display.RetrieveStatistics(astrStatistics);
			
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
		/// Form Close
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
			btnClear.Enabled = bflag;
			btnSpecifyPosition.Enabled = bflag;
			btnBlinkCharacter.Enabled = bflag;
			btnTeletypeCharacters.Enabled = bflag;
			btnLeft.Enabled = bflag;
			btnRight.Enabled = bflag;
			btnWindowControl.Enabled = bflag;
			btnRetrieveStatistics.Enabled = bflag;
			txtStatistics.Enabled = bflag;

			//<<<step8>>>--Start
			//Supports only for the Graphic Display(EPSON DM-D500 series)
			if(m_Display.DeviceName.IndexOf("DM-D5") >= 0)
			{
				btnSetChracterOrnaments.Enabled = bflag;
				btnDisplayBitmap.Enabled = bflag;
			}
            else
            {
                btnSetChracterOrnaments.Enabled = false;
                btnDisplayBitmap.Enabled = false;
            }
			//<<<step8>>>--End
		}

		/// <summary>
		/// The processing code required in order to enable to use of service is written here.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep11_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "LineDisplay";
			
			//Create PosExplorer
			PosExplorer posExplorer = new PosExplorer();

			DeviceInfo deviceInfo = null;

			//<<<step7>>>--Start
			try
			{
				deviceInfo = posExplorer.GetDevice(DeviceType.LineDisplay,strLogicalName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","DisplaySample_Step11");
				//Nothing can be used.
				ChangeButtonStatus(false);
				return;
			}
			
			try
			{
				m_Display =(LineDisplay)posExplorer.CreateInstance(deviceInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","DisplaySample_Step11"
					,MessageBoxButtons.OK);
				
				//Disable button
				ChangeButtonStatus(false);
				return;
			}
			
			//Add StatusUpdateEventHandler
			AddStatusUpdateEvent(m_Display);

			try
			{
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Display.Open();
			}
			catch(PosControlException)
			{

				MessageBox.Show("This device has not been registered, or cannot use."
					,"DisplaySample_Step11",MessageBoxButtons.OK);
				ChangeButtonStatus(false);
				return;
			}
			
			try
			{
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Display.Claim(1000);
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to get exclusive rights to the device."
					,"DisplaySample_Step11",MessageBoxButtons.OK);

				ChangeButtonStatus(false);
				return;
			}

			// Power reporting
			try
			{
				if(m_Display.CapPowerReporting != PowerReporting.None)
				{
					m_Display.PowerNotify = PowerNotification.Enabled;
				}
			}
			catch(PosControlException)
			{
			}

			try
			{
				//Enable the device.
				m_Display.DeviceEnabled = true;
			}
			catch(PosControlException)
			{

				MessageBox.Show("Now the device is disable to use.","DisplaySample_Step11",
					MessageBoxButtons.OK);	

				ChangeButtonStatus(false);
				return;
			}

			//<<<step7>>>--End
			//<<<step1>>>--End

			//<<<step8>>>--Start
			//Supports only for the Graphic Display(EPSON DM-D500 series)
			if(m_Display.DeviceName.IndexOf("DM-D5") < 0)
			{
				btnSetChracterOrnaments.Enabled = false;
				btnDisplayBitmap.Enabled = false;
			}
			//<<<step8>>>--End

			//<<<steP11>>>--Start
			if(m_Display.CapStatisticsReporting == false)
			{
				btnRetrieveStatistics.Enabled = false;
				txtStatistics.Enabled = false;
			}
			//<<<steP11>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep11_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Display != null)
			{
                try
                {
                    //For clear the text on the window be in current use.
                    m_Display.ClearText();
                }
                catch (PosControlException)
                {
                }
                try
                {
                    // Remove StatusUpdateEventHandler.
                    RemoveStatusUpdateEvent(m_Display);

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
		/// Add StatusUpdateEventHandler
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddStatusUpdateEvent(object eventSource)
		{
			//<<<step7>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent");
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.AddEventHandler(eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent));
			}
			//<<<step7>>>--End
		}

		/// <summary>
		/// Remove StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveStatusUpdateEvent(object eventSource)
		{
			//<<<step7>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent");
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.RemoveEventHandler( eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent));
			}
			//<<<step7>>>--End
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

            if (iPreviousStatus == e.Status)
            {
                return;
            }

            iPreviousStatus = e.Status;

			//<<<step7>>>--Start
			//'The Power Reporting Requirements fires the event when the device power status is changed.
			switch(e.Status)
			{
					//The device is powered on.
				case PosCommon.StatusPowerOnline:
					MessageBox.Show("The device is powered on.","DisplaySample_Step11"
						,MessageBoxButtons.OK);
					ChangeButtonStatus(true);
					break;
					//The device is powered off, or unconnected.
				case PosCommon.StatusPowerOff:
					MessageBox.Show("The device is powered off","DisplaySample_Step11"
						,MessageBoxButtons.OK);
					ChangeButtonStatus(false);
					break;
					//The device is powered on, but disable to operate.
				case PosCommon.StatusPowerOffline:
					MessageBox.Show("The device is powered on, but disable to operate."
						,"DisplaySample_Step11",MessageBoxButtons.OK);
					ChangeButtonStatus(false);
					break;
					//The device is powered off or off-line.
				case PosCommon.StatusPowerOffOffline:
					MessageBox.Show("The device is powered off or off-line.","DisplaySample_Step11"
						,MessageBoxButtons.OK);
					ChangeButtonStatus(false);
					break;
				default:
					break;
			}
			//<<<step7>>>--End
		}
	}
}
