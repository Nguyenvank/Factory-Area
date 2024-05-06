using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;
using System.IO;

namespace CheckSample_Step3
{
	/// <summary>
	/// Description of FrameStep3
	/// </summary>
	public class FrameStep3 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtDataSize;
		private System.Windows.Forms.Button btnRead;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblImageSize;
		private System.Windows.Forms.Label lblStore;
		private System.Windows.Forms.Button btnStore;
		private System.Windows.Forms.TextBox txtRestNumber;
		private System.Windows.Forms.Button btnCreateFile;
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
			this.txtDataSize = new System.Windows.Forms.TextBox();
			this.lblImageSize = new System.Windows.Forms.Label();
			this.btnRead = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.txtRestNumber = new System.Windows.Forms.TextBox();
			this.lblStore = new System.Windows.Forms.Label();
			this.btnStore = new System.Windows.Forms.Button();
			this.btnCreateFile = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtDataSize
			// 
			this.txtDataSize.Location = new System.Drawing.Point(184, 32);
			this.txtDataSize.Name = "txtDataSize";
			this.txtDataSize.ReadOnly = true;
			this.txtDataSize.Size = new System.Drawing.Size(216, 19);
			this.txtDataSize.TabIndex = 0;
			this.txtDataSize.Text = "";
			// 
			// lblImageSize
			// 
			this.lblImageSize.Location = new System.Drawing.Point(92, 32);
			this.lblImageSize.Name = "lblImageSize";
			this.lblImageSize.Size = new System.Drawing.Size(64, 20);
			this.lblImageSize.TabIndex = 1;
			this.lblImageSize.Text = "ImageSize :";
			// 
			// btnRead
			// 
			this.btnRead.Location = new System.Drawing.Point(180, 108);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new System.Drawing.Size(88, 24);
			this.btnRead.TabIndex = 2;
			this.btnRead.Text = "Read";
			this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(316, 188);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 24);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// txtRestNumber
			// 
			this.txtRestNumber.Location = new System.Drawing.Point(184, 72);
			this.txtRestNumber.Name = "txtRestNumber";
			this.txtRestNumber.ReadOnly = true;
			this.txtRestNumber.Size = new System.Drawing.Size(216, 19);
			this.txtRestNumber.TabIndex = 4;
			this.txtRestNumber.Text = "";
			// 
			// lblStore
			// 
			this.lblStore.Location = new System.Drawing.Point(16, 72);
			this.lblStore.Name = "lblStore";
			this.lblStore.Size = new System.Drawing.Size(140, 16);
			this.lblStore.TabIndex = 5;
			this.lblStore.Text = "Rest number of \"Store\" :";
			// 
			// btnStore
			// 
			this.btnStore.Enabled = false;
			this.btnStore.Location = new System.Drawing.Point(180, 148);
			this.btnStore.Name = "btnStore";
			this.btnStore.Size = new System.Drawing.Size(88, 24);
			this.btnStore.TabIndex = 6;
			this.btnStore.Text = "Store";
			this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
			// 
			// btnCreateFile
			// 
			this.btnCreateFile.Enabled = false;
			this.btnCreateFile.Location = new System.Drawing.Point(180, 188);
			this.btnCreateFile.Name = "btnCreateFile";
			this.btnCreateFile.Size = new System.Drawing.Size(88, 24);
			this.btnCreateFile.TabIndex = 7;
			this.btnCreateFile.Text = "CreateFile";
			this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
			// 
			// frmStep3
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(420, 233);
			this.Controls.Add(this.btnCreateFile);
			this.Controls.Add(this.btnStore);
			this.Controls.Add(this.lblStore);
			this.Controls.Add(this.txtRestNumber);
			this.Controls.Add(this.txtDataSize);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnRead);
			this.Controls.Add(this.lblImageSize);
			this.MaximizeBox = false;
			this.Name = "frmStep3";
			this.Text = "Step3 Read data stored in the file.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep3_Closing);
			this.Load += new System.EventHandler(this.frmStep3_Load);
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

		/// <summary>
		/// CheckScanner object.
		/// </summary>
		CheckScanner m_CheckScanner = null;

		/// <summary>
		/// Read Check
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
		/// Image file store use fileId and ImageTagdata
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStore_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>--Start
			try
			{
				// Set FileID
				m_CheckScanner.FileId = "Epson CheckScanner SampleProgram Step3";
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Set ImageTagData
				m_CheckScanner.ImageTagData = "Epson CheckScanner SampleProgram Step3 ImageTagData";
			}
			catch(PosControlException)
			{
			}
			
			try
			{
				// Clear Image Data File
				m_CheckScanner.ClearImage(CheckImageClear.FileId);
			}
			catch(PosControlException)
			{
			}
			
			try
			{
				// StoreImage Data File
				m_CheckScanner.StoreImage(CheckScanner.CropAreaEntireImage);
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Displayed "rest number"
				txtRestNumber.Text = m_CheckScanner.RemainingImagesEstimate.ToString();
			}
			catch(PosControlException)
			{
			}

			MessageBox.Show("A data was stored.","CheckSample_Step3",MessageBoxButtons.OK);		
			//<<<step2>>>--End
		}

		/// <summary>
		/// Create image file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCreateFile_Click(object sender, System.EventArgs e)
		{
			//<<<step3>>>--Start
			Bitmap Image;
			string strImageFormat = "";
			string strImageFilePath;

			strImageFilePath =  Directory.GetCurrentDirectory();

			Image = m_CheckScanner.ImageData;

			if(Image != null)
			{
				if(Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
				{
					strImageFormat = "tif";
				}
				else if(Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
				{
					strImageFormat = "bmp";
				}
				else if(Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
				{
					strImageFormat = "jpg";
				}

				strImageFilePath += "\\Step3." + strImageFormat;

				Image.Save(strImageFilePath);

				btnCreateFile.Enabled = false;

				//Finish message
				MessageBox.Show("A file was created","CheckSample_Step3",MessageBoxButtons.OK);
			}		
			//<<<step3>>>--End
		}

		/// <summary>
		/// When the method "ChangeButtonStatus" was called,
		/// all buttons other than a button "Exit" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnRead.Enabled = false;
			btnStore.Enabled = false;
		}

		/// <summary>
		/// Form Close
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
		private void frmStep3_Load(object sender, System.EventArgs e)
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
		private void frmStep3_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_CheckScanner != null)
			{
				try
				{
					// Remove DataEventHandler
					RemoveDataEvent(m_CheckScanner);

					// Remove ErrorEventHandler
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
		/// Add DataEventHadler.
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
		/// AddErrorEventtHandler.
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
			EventInfo dataEvent = eventSource.GetType().GetEvent( "DataEvent");
			
			if( dataEvent != null )
			{
				dataEvent.RemoveEventHandler( eventSource,
					new DataEventHandler(OnDataEvent));
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
		protected void OnErrorEvent(object source, DeviceErrorEventArgs e )
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
				,"CheckSample_Step3",MessageBoxButtons.OK);
			//<<<step1>>>--End
		}

		/// <summary>
		/// Data Event.
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
			//<<<step1>>>--End

			//<<<step2>>>--Start
			btnStore.Enabled = true;
			//<<<step2>>>--End

			//<<<step3>>>--Start
			btnCreateFile.Enabled = true;
			//<<<step3>>>--ENd
		}	
	}
}
