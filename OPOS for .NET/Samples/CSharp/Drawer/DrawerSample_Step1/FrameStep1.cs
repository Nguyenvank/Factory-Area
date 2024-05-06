using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;

namespace DrawerSample_Step1
{
	/// <summary>
	/// Description of FrameStep1.
	/// </summary>
	public class FrameStep1 : System.Windows.Forms.Form
	{
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
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.grpCashDrawer = new System.Windows.Forms.GroupBox();
			this.grpCashDrawer.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(40, 36);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(172, 28);
			this.btnOpen.TabIndex = 0;
			this.btnOpen.Text = "Open";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(188, 120);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 28);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// grpCashDrawer
			// 
			this.grpCashDrawer.Controls.Add(this.btnOpen);
			this.grpCashDrawer.Location = new System.Drawing.Point(20, 20);
			this.grpCashDrawer.Name = "grpCashDrawer";
			this.grpCashDrawer.Size = new System.Drawing.Size(260, 88);
			this.grpCashDrawer.TabIndex = 2;
			this.grpCashDrawer.TabStop = false;
			this.grpCashDrawer.Text = "CashDrawer";
			// 
			// frmStep1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(292, 161);
			this.Controls.Add(this.grpCashDrawer);
			this.Controls.Add(this.btnClose);
			this.MaximizeBox = false;
			this.Name = "frmStep1";
			this.Text = "Step 1 Open cash drawer.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep1_Closing);
			this.Load += new System.EventHandler(this.frmStep1_Load);
			this.grpCashDrawer.ResumeLayout(false);
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

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox grpCashDrawer;

		/// <summary>
		/// CashDrawer object
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
			//<<<step1>>>--Start
			//When outputting to a printer,a mouse cursor becomes like a hourglass.
			Cursor.Current = Cursors.WaitCursor;
			btnOpen.Enabled = false;

			try
			{
				//Open the drawer by using the "OpenDrawer" method.
				m_Drawer.OpenDrawer();

				// Wait during opend drawer.
				while(m_Drawer.DrawerOpened == false)
				{
					System.Threading.Thread.Sleep(100);
				}

				//When the drawer is not closed in ten seconds after opening, beep until it is closed.
				//If  that method is executed, the value is not returned until the drawer is closed.
				m_Drawer.WaitForDrawerClose(10000, 2000, 100, 1000);

				btnOpen.Enabled = true;

				Cursor.Current = Cursors.Default;
			}
			catch(PosControlException)
			{
			}		
			//<<<step1>>>--End
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
		private void frmStep1_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "CashDrawer";
			
			try
			{
				//Create PosExplorer
				PosExplorer posExplorer = new PosExplorer();
		
				DeviceInfo deviceInfo = null;
			
				try
				{
					deviceInfo = posExplorer.GetDevice(DeviceType.CashDrawer,strLogicalName);
					m_Drawer =(CashDrawer)posExplorer.CreateInstance(deviceInfo);
				}
				catch(Exception)
				{
					//Nothing can be used.
					ChangeButtonStatus();
					return;
				}
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Drawer.Open();

				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Drawer.Claim(1000);

				//Enable the device.
				m_Drawer.DeviceEnabled = true;

			}
			catch(PosControlException)
			{
				//Nothing can be used.
				//Nothing can be used.
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
		private void frmStep1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Drawer != null)
			{
				try
				{
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
	}
}
