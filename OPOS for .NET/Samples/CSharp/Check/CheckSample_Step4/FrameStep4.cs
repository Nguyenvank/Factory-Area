using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;
using System.IO;

namespace CheckSample_Step4
{
	/// <summary>
	/// Description of FrameStep4
	/// </summary>
	public class FrameStep4 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtDataSize;
		private System.Windows.Forms.Button btnRead;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblImageSize;
		private System.Windows.Forms.Label lblStore;
		private System.Windows.Forms.Button btnStore;
		private System.Windows.Forms.TextBox txtRestNumber;
		private System.Windows.Forms.Button btnCreateFile;
		private System.Windows.Forms.GroupBox grpCroppinArea;
		private System.Windows.Forms.RadioButton radPartNumber;
		private System.Windows.Forms.RadioButton radCropAll;
		private System.Windows.Forms.RadioButton radCropCode;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep4()
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
			this.grpCroppinArea = new System.Windows.Forms.GroupBox();
			this.radPartNumber = new System.Windows.Forms.RadioButton();
			this.radCropCode = new System.Windows.Forms.RadioButton();
			this.radCropAll = new System.Windows.Forms.RadioButton();
			this.grpCroppinArea.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtDataSize
			// 
			this.txtDataSize.Location = new System.Drawing.Point(184, 32);
			this.txtDataSize.Name = "txtDataSize";
			this.txtDataSize.ReadOnly = true;
			this.txtDataSize.Size = new System.Drawing.Size(276, 19);
			this.txtDataSize.TabIndex = 0;
			this.txtDataSize.Text = "";
			// 
			// lblImageSize
			// 
			this.lblImageSize.Location = new System.Drawing.Point(88, 32);
			this.lblImageSize.Name = "lblImageSize";
			this.lblImageSize.Size = new System.Drawing.Size(64, 20);
			this.lblImageSize.TabIndex = 1;
			this.lblImageSize.Text = "ImageSize :";
			// 
			// btnRead
			// 
			this.btnRead.Location = new System.Drawing.Point(20, 116);
			this.btnRead.Name = "btnRead";
			this.btnRead.Size = new System.Drawing.Size(144, 24);
			this.btnRead.TabIndex = 2;
			this.btnRead.Text = "Read";
			this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(368, 252);
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
			this.txtRestNumber.Size = new System.Drawing.Size(276, 19);
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
			this.btnStore.Location = new System.Drawing.Point(20, 164);
			this.btnStore.Name = "btnStore";
			this.btnStore.Size = new System.Drawing.Size(144, 24);
			this.btnStore.TabIndex = 6;
			this.btnStore.Text = "Store";
			this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
			// 
			// btnCreateFile
			// 
			this.btnCreateFile.Enabled = false;
			this.btnCreateFile.Location = new System.Drawing.Point(20, 212);
			this.btnCreateFile.Name = "btnCreateFile";
			this.btnCreateFile.Size = new System.Drawing.Size(144, 24);
			this.btnCreateFile.TabIndex = 7;
			this.btnCreateFile.Text = "CreateFile";
			this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
			// 
			// grpCroppinArea
			// 
			this.grpCroppinArea.Controls.Add(this.radPartNumber);
			this.grpCroppinArea.Controls.Add(this.radCropCode);
			this.grpCroppinArea.Controls.Add(this.radCropAll);
			this.grpCroppinArea.Location = new System.Drawing.Point(184, 108);
			this.grpCroppinArea.Name = "grpCroppinArea";
			this.grpCroppinArea.Size = new System.Drawing.Size(276, 132);
			this.grpCroppinArea.TabIndex = 8;
			this.grpCroppinArea.TabStop = false;
			this.grpCroppinArea.Text = "Cropping area";
			// 
			// radPartNumber
			// 
			this.radPartNumber.Enabled = false;
			this.radPartNumber.Location = new System.Drawing.Point(16, 96);
			this.radPartNumber.Name = "radPartNumber";
			this.radPartNumber.Size = new System.Drawing.Size(248, 20);
			this.radPartNumber.TabIndex = 2;
			this.radPartNumber.Tag = "Crop3";
			this.radPartNumber.Text = "Part of number (5100,236,490,214)";
			this.radPartNumber.CheckedChanged += new System.EventHandler(this.radPartNumber_CheckedChanged);
			// 
			// radCropCode
			// 
			this.radCropCode.Enabled = false;
			this.radCropCode.Location = new System.Drawing.Point(16, 64);
			this.radCropCode.Name = "radCropCode";
			this.radCropCode.Size = new System.Drawing.Size(248, 20);
			this.radCropCode.TabIndex = 1;
			this.radCropCode.Tag = "Crop2";
			this.radCropCode.Text = "Part of code (3500,2244,2000,456)";
			this.radCropCode.CheckedChanged += new System.EventHandler(this.radCropCode_CheckedChanged);
			// 
			// radCropAll
			// 
			this.radCropAll.Enabled = false;
			this.radCropAll.Location = new System.Drawing.Point(16, 28);
			this.radCropAll.Name = "radCropAll";
			this.radCropAll.Size = new System.Drawing.Size(248, 20);
			this.radCropAll.TabIndex = 0;
			this.radCropAll.Tag = "";
			this.radCropAll.Text = "All area (0,0,5984,2756)";
			this.radCropAll.CheckedChanged += new System.EventHandler(this.radCropAll_CheckedChanged);
			// 
			// FrameStep4
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(484, 289);
			this.Controls.Add(this.grpCroppinArea);
			this.Controls.Add(this.btnCreateFile);
			this.Controls.Add(this.btnStore);
			this.Controls.Add(this.lblStore);
			this.Controls.Add(this.txtRestNumber);
			this.Controls.Add(this.txtDataSize);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnRead);
			this.Controls.Add(this.lblImageSize);
			this.MaximizeBox = false;
			this.Name = "FrameStep4";
			this.Text = "Step 4 Specify reading area and cropping area.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep4_Closing);
			this.Load += new System.EventHandler(this.frmStep4_Load);
			this.grpCroppinArea.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep4());
		}

		const int CROP_AREA_ID_ALL_AREA = CheckScanner.CropAreaEntireImage;
		const int CROP_AREA_ID_PART_OF_CODE = 2;
		const int CROP_AREA_ID_PART_OF_NUMBER = 3;

		/// <summary>
		/// CheckScanner object
		/// </summary>
		CheckScanner m_CheckScanner = null;

		/// <summary>
		/// CheckScaner CropAreID
		/// </summary>
		private int m_iCropAreaId = CROP_AREA_ID_ALL_AREA;

		/// <summary>
		/// Read Check
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRead_Click(object sender, System.EventArgs e)
		{
			DialogResult dialogResult;

			//<<<step1>>>--Start
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
				radCropAll.Enabled = true;
				radCropCode.Enabled = true;
				radPartNumber.Enabled = true;
				radCropAll.Checked = true;
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
				m_CheckScanner.FileId = "Epson CheckScanner SampleProgram Step4";
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Set ImageTagData
				m_CheckScanner.ImageTagData = "Epson CheckScanner SampleProgram Step4 ImageTagData";
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
				m_CheckScanner.StoreImage(m_iCropAreaId);

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

			MessageBox.Show("A data was stored.","CheckSample_Step4",MessageBoxButtons.OK);		
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
					strImageFormat = "tiff";
				}
				else if(Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
				{
					strImageFormat = "bmp";
				}
				else if(Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
				{
					strImageFormat = "jpeg";
				}

				strImageFilePath += "\\Step4." + strImageFormat;

				Image.Save(strImageFilePath);

				btnCreateFile.Enabled = false;
				
				//Finish message
				MessageBox.Show("A file was created","CheckSample_Step4",MessageBoxButtons.OK);
			}
			//<<<step3>>>--End
		}

		/// <summary>
		/// Define crop area
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radCropAll_CheckedChanged(object sender, System.EventArgs e)
		{
			//<<<step4>>>--Start
			m_iCropAreaId = CROP_AREA_ID_ALL_AREA;

			try
			{
				m_CheckScanner.DataEventEnabled = true;
				m_CheckScanner.DefineCropArea(m_iCropAreaId,0,0
					,CheckScanner.CropAreaRight
					,CheckScanner.CropAreaBottom);

				m_CheckScanner.RetrieveImage(m_iCropAreaId);
			}
			catch(PosControlException)
			{
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// Define crop area
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radCropCode_CheckedChanged(object sender, System.EventArgs e)
		{
			//<<<step4>>>--Start
			m_iCropAreaId = CROP_AREA_ID_PART_OF_CODE;

			try
			{
				m_CheckScanner.DataEventEnabled = true;
				m_CheckScanner.DefineCropArea(m_iCropAreaId,3500, 2244, 2000, 456);
				m_CheckScanner.RetrieveImage(m_iCropAreaId);
			}
			catch(PosControlException)
			{
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// Define crop area
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radPartNumber_CheckedChanged(object sender, System.EventArgs e)
		{
			//<<<step4>>>--Start
			m_iCropAreaId = CROP_AREA_ID_PART_OF_NUMBER;

			try
			{
				m_CheckScanner.DataEventEnabled = true;
				m_CheckScanner.DefineCropArea(m_iCropAreaId,5100, 236, 490, 214);
				m_CheckScanner.RetrieveImage(m_iCropAreaId);
			}
			catch(PosControlException)
			{
			}
			//<<<step4>>>--End
		}

		/// <summary>
		/// When the method "ChangeButtonStatus" was called,
		/// all buttons other than a button "Exit" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnRead.Enabled = false;
			btnStore.Enabled = false;
			radCropAll.Enabled = false;
			radCropCode.Enabled = false;
			radPartNumber.Enabled = false;
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
		private void frmStep4_Load(object sender, System.EventArgs e)
		{
			//<<<step1>>>--Start
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalName = "CheckScanner";
			
			//Create PosExplorer
			PosExplorer posExplorer = new PosExplorer();

			DeviceInfo deviceInfo = null;

			//<<<step4>>>--Start
			try
			{
				deviceInfo = posExplorer.GetDevice("CheckScanner",strLogicalName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","CheckSample_Step4");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				
				m_CheckScanner =(CheckScanner)posExplorer.CreateInstance(deviceInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","CheckSample_Step4",MessageBoxButtons.OK);
				
				//Disable button
				ChangeButtonStatus();
				return;
			}
			
			//Register EventHandler
			AddDataEvent(m_CheckScanner);
			AddErrorEvent(m_CheckScanner);

			try
			{
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_CheckScanner.Open();
			}
			catch(PosControlException)
			{

				MessageBox.Show("This device has not been registered, or cannot use."
					,"CheckSample_Step4",MessageBoxButtons.OK);

				ChangeButtonStatus();
				return;
			}
			
			try
			{
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_CheckScanner.Claim(1000);
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to get exclusive rights to the device."
					,"CheckSample_Step4",MessageBoxButtons.OK);

				ChangeButtonStatus();
				return;
			}

			// Power reporting
			try
			{
				if(m_CheckScanner.CapPowerReporting != PowerReporting.None)
				{
					m_CheckScanner.PowerNotify = PowerNotification.Enabled;
				}
			}
			catch(PosControlException)
			{
			}

			try
			{
				//Enable the device.
				m_CheckScanner.DeviceEnabled = true;
			}
			catch(PosControlException)
			{

				MessageBox.Show("Now the device is disable to use.","CheckSample_Step4"
					,MessageBoxButtons.OK);
				
				ChangeButtonStatus();
				return;
			}

			try
			{
				//Ready to fired event
				m_CheckScanner.DataEventEnabled = true;
			}
			catch(PosControlException)
			{
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
			//<<<step4>>>--End
			//<<<step1>>>--End
		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep4_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//<<<step1>>>--Start
			if(m_CheckScanner != null)
			{
				try
				{
					// Remove DataEventHandler.
					RemoveDataEvent(m_CheckScanner);

					// Remove ErrorEventHandler.
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
		/// AddDataEventHadler
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
		/// AddErrorEventtHandler
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
		/// Remove DataEventHandler 
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
		/// Remove ErrorEventHandler
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
		/// Error Event
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
				,"CheckSample_Step4",MessageBoxButtons.OK);
			//<<<step1>>>--End
		}

		/// <summary>
		/// Data Event
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
