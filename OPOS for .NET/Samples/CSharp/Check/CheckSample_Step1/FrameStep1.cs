using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;

namespace CheckSample_Step1
{
	/// <summary>
	/// Description of FrameStep1.
	/// </summary>
	public class FrameStep1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtDataSize;
		private System.Windows.Forms.Button btnRead;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblImageSize;
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
			this.txtDataSize = new System.Windows.Forms.TextBox();
			this.lblImageSize = new System.Windows.Forms.Label();
			this.btnRead = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtDataSize
			// 
			this.txtDataSize.Location = new System.Drawing.Point(84, 44);
			this.txtDataSize.Name = "txtDataSize";
			this.txtDataSize.ReadOnly = true;
			this.txtDataSize.Size = new System.Drawing.Size(216, 19);
			this.txtDataSize.TabIndex = 0;
			this.txtDataSize.Text = "";
			// 
			// lblImageSize
			// 
			this.lblImageSize.Location = new System.Drawing.Point(12, 44);
			this.lblImageSize.Name = "lblImageSize";
			this.lblImageSize.Size = new System.Drawing.Size(64, 20);
			this.lblImageSize.TabIndex = 1;
			this.lblImageSize.Text = "ImageSize :";
			// 
			// btnRead
			// 
			this.btnRead.Location = new System.Drawing.Point(80, 84);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new System.Drawing.Size(88, 24);
			this.btnRead.TabIndex = 2;
			this.btnRead.Text = "Read";
			this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(212, 84);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 24);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// FrameStep1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(344, 141);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnRead);
			this.Controls.Add(this.lblImageSize);
			this.Controls.Add(this.txtDataSize);
			this.MaximizeBox = false;
			this.Name = "FrameStep1";
			this.Text = "Step 1 Normal operation:Read data.";
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
		/// CheckScanner object.
		/// </summary>
		CheckScanner m_CheckScanner = null;

		/// <summary>
		/// Read check.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRead_Click(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			DialogResult dialogResult;

			try
			{
				// EventClear
				m_CheckScanner.ClearInput();
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Ready to fired event
				m_CheckScanner.DataEventEnabled = true;
			}
			catch(PosControlException)
			{
			}

			// Timeout function.
			bool bBeginInsertion = false;

			do
			{
				try
				{
					m_CheckScanner.BeginInsertion(3000);
					bBeginInsertion = true;
				}
				catch(PosControlException pex)
				{
					
					if(pex.ErrorCode == ErrorCode.Timeout)
					{
						dialogResult = MessageBox.Show("Please insert a check.",
							"beginInsertion timeout",MessageBoxButtons.YesNo);

						if(dialogResult == DialogResult.No)
						{
							return;
						}
						bBeginInsertion = false;
					}
					else
					{
						return;
					}
					
				}

			}while(!bBeginInsertion);
			
			try
			{
				// Set paper & Scanning
				m_CheckScanner.EndInsertion();
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Call to retrieve an image to the ImageData proparty
				m_CheckScanner.RetrieveImage(CheckScanner.CropAreaEntireImage);
			}
			catch(PosControlException)
			{
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "ChangeButtonStatus" was called,
		/// all buttons other than a button "Exit" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnRead.Enabled = false;
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
			string strLogicalName = "CheckScanner";
			
			try
			{
				//Create PosExplorer
				PosExplorer posExplorer = new PosExplorer();
	
				DeviceInfo deviceInfo = null;

				try
				{
					deviceInfo = posExplorer.GetDevice(DeviceType.CheckScanner,strLogicalName);
					m_CheckScanner =(CheckScanner)posExplorer.CreateInstance(deviceInfo);
				}
				catch(Exception)
				{
					ChangeButtonStatus();
					return;
				}
			
				//Register EventHandler
				AddDataEvent(m_CheckScanner);
				AddErrorEvent(m_CheckScanner);

				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_CheckScanner.Open();

				//In order to enable it to use the DataEvent.
				m_CheckScanner.DataEventEnabled = true;
		
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_CheckScanner.Claim(1000);
		
				//Enable the device.
				m_CheckScanner.DeviceEnabled = true;

				//Ready to fired event
				m_CheckScanner.DataEventEnabled = true;

			}
			catch(Exception)
			{
				ChangeButtonStatus();
				return;
			}

			try
			{ 
				//Read TIFF file format
				m_CheckScanner.ImageFormat = CheckImageFormats.Tiff;

				//Read BMP file format
				//m_CheckScanner.ImageFormat = CheckImageFormats.Bmp;

				//Read JPEG file format
				//m_CheckScanner.ImageFormat = CheckImageFormats.Jpeg;

			}
			catch(PosControlException)
			{
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
			if(m_CheckScanner != null)
			{
				try
				{
					// Remove DataEvent listener
					RemoveDataEvent(m_CheckScanner);

					// Remove ErrorEvent listener
					RemoveErrorEvent(m_CheckScanner);

					//Cancel the device
					m_CheckScanner.DeviceEnabled = false;

					//Release the device exclusive control right.
					m_CheckScanner.Release();
				}
				catch(PosControlException)
				{
				}
				finally
				{
					//Finish using the device.
					m_CheckScanner.Close();
				}
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Add DataEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddDataEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent" );
			
			if( dataEvent != null )
			{
				dataEvent.AddEventHandler( eventSource,
					new DataEventHandler(OnDataEvent) );
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Add ErrorEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void AddErrorEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
			if(errorEvent != null)
			{
				errorEvent.AddEventHandler(eventSource,
					new DeviceErrorEventHandler(OnErrorEvent));
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Remove DataEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveDataEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent" );
			
			if( dataEvent != null )
			{
				dataEvent.RemoveEventHandler( eventSource,
					new DataEventHandler(OnDataEvent) );
			}
			//<<<step1>>>--End
		}

		/// <summary>
		/// Remove ErrorEventHandler.
		/// </summary>
		/// <param name="eventSource"></param>
		protected void RemoveErrorEvent(object eventSource)
		{
			//<<<step1>>>--Start
			EventInfo errorEvent = eventSource.GetType().GetEvent("ErrorEvent");
			if(errorEvent != null)
			{
				errorEvent.RemoveEventHandler( eventSource,
					new DeviceErrorEventHandler(OnErrorEvent));
			}
			//<<<step1>>>--End
		}
		
		/// <summary>
		/// Error Event.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected  void OnErrorEvent(object source, DeviceErrorEventArgs e )
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DeviceErrorEventHandler(OnErrorEvent), new object[2] { source, e });
                return;
            }

			//<<<step1>>>--Start
			MessageBox.Show("CheckScanner Error\n" + "ErrorCode =" + e.ErrorCode.ToString() 
				+ "\n" + "ErrorCodeExtended =" + e.ErrorCodeExtended.ToString() 
				,"CheckSample_Step1",MessageBoxButtons.OK);
			//<<<step1>>>--End
		}

		/// <summary>
		/// Data Event.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected  void OnDataEvent(object source, DataEventArgs e)
        {
            if (InvokeRequired)
            {
                //Ensure calls to Windows Form Controls are from this application's thread
                Invoke(new DataEventHandler(OnDataEvent), new object[2] { source, e });
                return;
            }
	
			//<<<step1>>>--Start
			//Bitmap object
			Bitmap Image;

			try
			{
				//Get Image Data 
				Image =  m_CheckScanner.ImageData;
				
				txtDataSize.Text = "Width:" + Image.Width.ToString() 
					+ " Height:" + Image.Height.ToString();

				m_CheckScanner.DataEventEnabled = true;
			}
			catch(PosControlException)
			{
			}
		}
		//<<<step1>>>--End
	}
}
