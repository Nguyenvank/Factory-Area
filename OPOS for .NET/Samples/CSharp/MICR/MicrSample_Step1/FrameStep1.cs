using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;

namespace MicrSample_Step1
{
	/// <summary>
	/// Description of FrameStep1.
	/// </summary>
	public class FrameStep1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblRawData;
		private System.Windows.Forms.TextBox txtRawData;
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
			this.lblRawData = new System.Windows.Forms.Label();
			this.txtRawData = new System.Windows.Forms.TextBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblRawData
			// 
			this.lblRawData.Location = new System.Drawing.Point(16, 24);
			this.lblRawData.Name = "lblRawData";
			this.lblRawData.Size = new System.Drawing.Size(60, 20);
			this.lblRawData.TabIndex = 0;
			this.lblRawData.Text = "RawData";
			// 
			// txtRawData
			// 
			this.txtRawData.Location = new System.Drawing.Point(84, 24);
			this.txtRawData.Name = "txtRawData";
			this.txtRawData.Size = new System.Drawing.Size(272, 19);
			this.txtRawData.TabIndex = 1;
			this.txtRawData.Text = "";
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(84, 52);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(88, 24);
			this.btnInsert.TabIndex = 2;
			this.btnInsert.Text = "Insert";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(212, 52);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(88, 24);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(316, 136);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 24);
			this.btnExit.TabIndex = 4;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnInsert);
			this.groupBox1.Controls.Add(this.btnRemove);
			this.groupBox1.Controls.Add(this.lblRawData);
			this.groupBox1.Controls.Add(this.txtRawData);
			this.groupBox1.Location = new System.Drawing.Point(28, 24);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 96);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			// 
			// FrameStep1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(428, 173);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnExit);
			this.MaximizeBox = false;
			this.Name = "FrameStep1";
			this.Text = "Step 1 Normal operation: Display the read data.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep1_Closing);
			this.Load += new System.EventHandler(this.frmStep1_Load);
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
			Application.Run(new FrameStep1());
		}

		/// <summary>
		/// Micr object.
		/// </summary>
		Micr m_Micr = null;

		/// <summary>
		/// The code for inserting a check are described.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnInsert_Click(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			try
			{
				//Insertion operation of a check is started.
				m_Micr.BeginInsertion(0);
				m_Micr.EndInsertion();
			}
			catch(PosControlException)
			{
			}		
			//<<<step1>>>--End
		}

		/// <summary>
		/// The code for removing a check are described.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			try
			{
				m_Micr.BeginRemoval(0);
			}
			catch(PosControlException)
			{
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "changeButtonStatus" was called,
		/// all buttons other than a button "closing" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnInsert.Enabled = false;
			btnRemove.Enabled = false;
			txtRawData.Enabled = false;
		}

		/// <summary>
		/// Form close.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExit_Click(object sender, System.EventArgs e)
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
			string strLogicalName = "Micr";
			
			try
			{
				//Create PosExplorer
				PosExplorer posExplorer = new PosExplorer();
		
				DeviceInfo deviceInfo = null;
			
				try
				{
					deviceInfo = posExplorer.GetDevice(DeviceType.Micr,strLogicalName);
					m_Micr =(Micr)posExplorer.CreateInstance(deviceInfo);
				}
				catch(Exception)
				{
					ChangeButtonStatus();
					return;
				}

				//Register EventHandler
				AddDataEvent(m_Micr);

				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Micr.Open();

				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Micr.Claim(1000);

				//Enable the device.
				m_Micr.DeviceEnabled = true;

				m_Micr.DataEventEnabled = true;
		
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
		private void frmStep1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_Micr != null)
			{
				try
				{
					// Remove DataEventHandler
					RemoveDataEvent(m_Micr);

					//Cancel the device
					m_Micr.DeviceEnabled = false;

					//Release the device exclusive control right.
					m_Micr.Release();

				}
				catch(PosControlException)
				{
				}
				finally
				{
					//Finish using the device.
					m_Micr.Close();
				}
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Add ErrorEventHandler
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddDataEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent");
			
			if( dataEvent != null )
			{
				dataEvent.AddEventHandler( eventSource,
					new DataEventHandler(OnDataEvent) );
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Remove OutputCompleteEventHandler
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveDataEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent");
			
			if( dataEvent != null )
			{
				dataEvent.RemoveEventHandler( eventSource,
					new DataEventHandler(OnDataEvent) );
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// OutputComplete Event
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void OnDataEvent(object source, DataEventArgs e)
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DataEventHandler(OnDataEvent), new object[2] { source, e });
                return;
            }
	
			//<<<step1>>>--Start
			txtRawData.Text = m_Micr.RawData;
			
			m_Micr.DataEventEnabled = true;
			//<<<step1>>>--End
		}
	}
}
