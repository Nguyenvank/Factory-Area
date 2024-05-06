using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;

namespace DrawerSample_Step3
{
	/// <summary>
	/// Description of FrameStep3.
	/// </summary>
	public class FrameStep3 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep3()
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
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.grpCashDrawer = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblPower = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.txtPower = new System.Windows.Forms.TextBox();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.grpCashDrawer.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(40, 36);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(156, 28);
			this.btnOpen.TabIndex = 0;
			this.btnOpen.Text = "Open";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(200, 240);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 28);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// grpCashDrawer
			// 
			this.grpCashDrawer.Controls.Add(this.btnOpen);
			this.grpCashDrawer.Location = new System.Drawing.Point(48, 20);
			this.grpCashDrawer.Name = "grpCashDrawer";
			this.grpCashDrawer.Size = new System.Drawing.Size(240, 88);
			this.grpCashDrawer.TabIndex = 2;
			this.grpCashDrawer.TabStop = false;
			this.grpCashDrawer.Text = "CashDrawer";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblPower);
			this.groupBox1.Controls.Add(this.lblStatus);
			this.groupBox1.Controls.Add(this.txtPower);
			this.groupBox1.Controls.Add(this.txtStatus);
			this.groupBox1.Location = new System.Drawing.Point(48, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(240, 96);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Status Now";
			// 
			// lblPower
			// 
			this.lblPower.Location = new System.Drawing.Point(8, 64);
			this.lblPower.Name = "lblPower";
			this.lblPower.Size = new System.Drawing.Size(52, 16);
			this.lblPower.TabIndex = 3;
			this.lblPower.Text = "Power";
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(8, 32);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(52, 16);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "Status";
			// 
			// txtPower
			// 
			this.txtPower.Location = new System.Drawing.Point(68, 64);
			this.txtPower.Name = "txtPower";
			this.txtPower.ReadOnly = true;
			this.txtPower.Size = new System.Drawing.Size(128, 19);
			this.txtPower.TabIndex = 1;
			this.txtPower.Text = "";
			// 
			// txtStatus
			// 
			this.txtStatus.Location = new System.Drawing.Point(68, 28);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ReadOnly = true;
			this.txtStatus.Size = new System.Drawing.Size(128, 19);
			this.txtStatus.TabIndex = 0;
			this.txtStatus.Text = "";
			// 
			// frmStep3
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(336, 281);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpCashDrawer);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "frmStep3";
			this.Text = "Step 3 Adding error handlers.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep3_Closing);
			this.Load += new System.EventHandler(this.frmStep3_Load);
			this.grpCashDrawer.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep3());
		}

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox grpCashDrawer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.TextBox txtPower;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblPower;

		/// <summary>
		/// CashDrawer object.
		/// </summary>
		CashDrawer m_Drawer = null;

		/// <summary>
		/// When the button "Open" is pushed,
		/// a method "OpenDrawer" is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>--Start
			try
			{			
				m_Drawer.OpenDrawer();	
			}
			catch(PosControlException)
			{
			}
			
			//<<<step2>>>--End
		}

		/// <summary>
		/// When the method "ChangeButtonStatus" was called,
		/// all buttons other than a button "Close" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnOpen.Enabled = false;
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
		private void frmStep3_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "CashDrawer";
			
			//Create PosExplorer
			PosExplorer posExplorer = new PosExplorer();
	
			DeviceInfo deviceInfo = null;
		
			//<<<step3>>>--Start
			try
			{
				deviceInfo = posExplorer.GetDevice(DeviceType.CashDrawer,strLogicalName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.", "DrawerSample_Step3");
				//Disable button
				ChangeButtonStatus();
				return;
			}

			try
			{	
				m_Drawer =(CashDrawer)posExplorer.CreateInstance(deviceInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","DrawerSample_Step3"
					,MessageBoxButtons.OK);
			
				//Disable button
				ChangeButtonStatus();
				return;
			}
		
			//Add StatusUpdateEventHandler
			AddStatusUpdateEvent(m_Drawer);

			try
			{
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Drawer.Open();
			}
			catch(PosControlException)
			{

				MessageBox.Show("This device has not been registered, or cannot use."
					,"DrawerSample_Step3",MessageBoxButtons.OK);
				ChangeButtonStatus();
				return;
			}
		
			try
			{
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Drawer.Claim(1000);
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to get exclusive rights to the device."
					,"DrawerSample_Step3",MessageBoxButtons.OK);
				ChangeButtonStatus();
				return;
			}

			// Power reporting
			try
			{
				if(m_Drawer.CapPowerReporting != PowerReporting.None)
				{
					m_Drawer.PowerNotify = PowerNotification.Enabled;
				}
			}
			catch(PosControlException)
			{
			}

			try
			{
				//Enable the device.
				m_Drawer.DeviceEnabled = true;
			}
			catch(PosControlException)
			{

				MessageBox.Show("Now the device is disable to use.","DrawerSample_Step3"
					,MessageBoxButtons.OK);
			
				ChangeButtonStatus();
				return;
			}
			//<<<step3>>>--End

			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep3_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Drawer != null)
			{
				try
				{
					// Remove StatusUpdateEvent listener
					RemoveStatusUpdateEvent(m_Drawer);

					//Cancel the device
					m_Drawer.DeviceEnabled = false;

					//Release the device exclusive control right.
					m_Drawer.Release();

				}
				catch(PosControlException)
				{
				}
				finally
				{
					//Finish using the device.
					m_Drawer.Close();
				}
			}
			//<<<step1>>>--End
		}		

		/// <summary>
		/// Add StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddStatusUpdateEvent(object eventSource)
		{
			//<<<step2>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent" );
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.AddEventHandler(eventSource,
					new StatusUpdateEventHandler(OnStatusUpdateEvent));
			}
			//<<<step2>>>--End
		}		

		/// <summary>
		/// Remove StatusUpdateEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveStatusUpdateEvent(object eventSource)
		{
			//<<<step2>>>--Start
			EventInfo statusUpdateEvent = eventSource.GetType().GetEvent( "StatusUpdateEvent" );
			if(statusUpdateEvent != null)
			{
				statusUpdateEvent.RemoveEventHandler( eventSource, 
					new StatusUpdateEventHandler(OnStatusUpdateEvent) );
			}
			//<<<step2>>>--End
		}

		/// <summary>
		/// StatusUpdateEvnet
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
			
			//<<<step2>>>--Start
			switch(e.Status)
			{
					//Drawer is closed.
				case CashDrawer.StatusClosed:
					Cursor = Cursors.Default;
					txtStatus.Text = "Close";
					btnOpen.Enabled = true;					
					break;
					//Drawer is opened.
				case CashDrawer.StatusOpen:
					Cursor = Cursors.WaitCursor;
					txtStatus.Text = "Open";
					btnOpen.Enabled = false;
					break;

					//The Power Reporting Requirements fires the event when the device power status is changed.

					//The device is powered on.
				case PosCommon.StatusPowerOnline:
					txtPower.Text = "ready";
					break;
					//The device is powered off, or unconnected.
				case PosCommon.StatusPowerOff:
					txtPower.Text = "OFF";
					break;
					//The device is powered on, but disable to operate.
				case PosCommon.StatusPowerOffline:
					txtPower.Text = "not ready";
					break;
					//The device is powered off or off-line.
				case PosCommon.StatusPowerOffOffline:
					txtPower.Text = "Offline";
					break;
				default:
					break;
			}
			//<<<step2>>>--End
		}
	}
}
