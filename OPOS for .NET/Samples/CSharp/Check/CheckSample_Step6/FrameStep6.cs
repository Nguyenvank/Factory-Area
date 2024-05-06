using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.PointOfService;
using System.Reflection;
using System.IO;
using jp.co.epson.uposcommon;

namespace CheckSample_Step6
{
	/// <summary>
	/// Description of FrameStep6.
	/// </summary>
	public class FrameStep6 : System.Windows.Forms.Form
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
		private System.Windows.Forms.Button btnAttachMICR;
		private System.Windows.Forms.ComboBox cmbMemoyData;
		private System.Windows.Forms.Button btnReadMemory;
		private System.Windows.Forms.Label lblCurrentMode;
		private System.Windows.Forms.ComboBox cmbCurrentMode;
		/// <summary>
		/// Design  variable. 
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameStep6()
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
			this.btnAttachMICR = new System.Windows.Forms.Button();
			this.btnReadMemory = new System.Windows.Forms.Button();
			this.cmbMemoyData = new System.Windows.Forms.ComboBox();
			this.cmbCurrentMode = new System.Windows.Forms.ComboBox();
			this.lblCurrentMode = new System.Windows.Forms.Label();
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
			this.btnExit.Location = new System.Drawing.Point(380, 320);
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
			this.btnStore.Location = new System.Drawing.Point(20, 182);
			this.btnStore.Name = "btnStore";
			this.btnStore.Size = new System.Drawing.Size(144, 24);
			this.btnStore.TabIndex = 6;
			this.btnStore.Text = "Store";
			this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
			// 
			// btnCreateFile
			// 
			this.btnCreateFile.Enabled = false;
			this.btnCreateFile.Location = new System.Drawing.Point(20, 215);
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
			// btnAttachMICR
			// 
			this.btnAttachMICR.Location = new System.Drawing.Point(20, 149);
			this.btnAttachMICR.Name = "btnAttachMICR";
			this.btnAttachMICR.Size = new System.Drawing.Size(144, 24);
			this.btnAttachMICR.TabIndex = 9;
			this.btnAttachMICR.Text = "Attached MICR data";
			this.btnAttachMICR.Click += new System.EventHandler(this.btnAttachMICR_Click);
			// 
			// btnReadMemory
			// 
			this.btnReadMemory.Location = new System.Drawing.Point(20, 248);
			this.btnReadMemory.Name = "btnReadMemory";
			this.btnReadMemory.Size = new System.Drawing.Size(144, 24);
			this.btnReadMemory.TabIndex = 10;
			this.btnReadMemory.Text = "Read memory data";
			this.btnReadMemory.Click += new System.EventHandler(this.btnReadMemory_Click);
			// 
			// cmbMemoyData
			// 
			this.cmbMemoyData.Location = new System.Drawing.Point(188, 252);
			this.cmbMemoyData.Name = "cmbMemoyData";
			this.cmbMemoyData.Size = new System.Drawing.Size(272, 20);
			this.cmbMemoyData.TabIndex = 11;
			// 
			// cmbCurrentMode
			// 
			this.cmbCurrentMode.Location = new System.Drawing.Point(188, 288);
			this.cmbCurrentMode.Name = "cmbCurrentMode";
			this.cmbCurrentMode.Size = new System.Drawing.Size(272, 20);
			this.cmbCurrentMode.TabIndex = 12;
			this.cmbCurrentMode.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentMode_SelectedIndexChanged);
			// 
			// lblCurrentMode
			// 
			this.lblCurrentMode.Location = new System.Drawing.Point(76, 288);
			this.lblCurrentMode.Name = "lblCurrentMode";
			this.lblCurrentMode.Size = new System.Drawing.Size(88, 20);
			this.lblCurrentMode.TabIndex = 13;
			this.lblCurrentMode.Text = "CurrentMode : ";
			// 
			// FrameStep6
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(480, 353);
			this.Controls.Add(this.lblCurrentMode);
			this.Controls.Add(this.cmbCurrentMode);
			this.Controls.Add(this.cmbMemoyData);
			this.Controls.Add(this.btnReadMemory);
			this.Controls.Add(this.btnAttachMICR);
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
			this.Name = "FrameStep6";
			this.Text = "Step 6 Changing reading mode.";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmStep6_Closing);
			this.Load += new System.EventHandler(this.frmStep6_Load);
			this.grpCroppinArea.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Main 
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrameStep6());
		}

		const int CROP_AREA_ID_ALL_AREA = CheckScanner.CropAreaEntireImage;
		const int CROP_AREA_ID_PART_OF_CODE = 2;
		const int CROP_AREA_ID_PART_OF_NUMBER = 3;

		/// <summary>
		/// CheckScanner object.
		/// </summary>
		CheckScanner m_CheckScanner = null;

		/// <summary>
		/// Micr object.
		/// </summary>
		Micr m_Micr = null;

		/// <summary>
		/// CheckScaner CropAreID.
		/// </summary>
		int m_iCropAreaId = CROP_AREA_ID_ALL_AREA;

		/// <summary>
		/// Micr RawData.
		/// </summary>
		string m_strMicrRawData = "";

		/// <summary>
		/// Read check.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRead_Click(object sender, System.EventArgs e)
		{
			//<<<step1>>>
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
					bBeginInsertion = true;
					m_CheckScanner.BeginInsertion(3000);
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

			m_strMicrRawData = "";
		}

		/// <summary>
		/// Image file store use fileId and ImageTagdata.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnStore_Click(object sender, System.EventArgs e)
		{
			//<<<step2>>>
			try
			{
				// Set FileID
				m_CheckScanner.FileId = "Epson CheckScanner SampleProgram Step6";
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Set ImageTagData
				m_CheckScanner.ImageTagData = "Epson CheckScanner SampleProgram Step6 ImageTagData"
					+ m_strMicrRawData;
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

			MessageBox.Show("A data was stored.","CheckSample_Step6",MessageBoxButtons.OK);		
		}

		/// <summary>
		/// Create image file.
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

				strImageFilePath += "\\Step6." + strImageFormat;

				Image.Save(strImageFilePath);

				btnCreateFile.Enabled = false;

				//Finish message
				MessageBox.Show("A file was created","CheckSample_Step6",MessageBoxButtons.OK);
				//<<<step3>>>--End
			}
		}

		/// <summary>
		/// Define crop area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radCropAll_CheckedChanged(object sender, System.EventArgs e)
		{
			//<<<step4>>>
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
		}

		/// <summary>
		/// Define crop area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radCropCode_CheckedChanged(object sender, System.EventArgs e)
		{
			//<<<step4>>>
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
		}

		/// <summary>
		/// Define crop area.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radPartNumber_CheckedChanged(object sender, System.EventArgs e)
		{
			//<<<step4>>>
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
		}

		/// <summary>
		/// CheckScanner read memory and get image.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReadMemory_Click(object sender, System.EventArgs e)
		{

			//<<<step5>>>
			string strStep = "Epson CheckScanner SampleProgram ";
			strStep += cmbMemoyData.Text;
			
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
				m_CheckScanner.FileId = strStep;				
			}
			catch(PosControlException)
			{
			}

			try
			{
				// Speciffic "FileID"
				m_CheckScanner.RetrieveMemory(CheckImageLocate.FileId);
				m_CheckScanner.ClearImage(CheckImageClear.FileId);
			}
			catch(PosControlException)
			{
				// Error MessageBox
				MessageBox.Show("No data!","CheckScanner Error",MessageBoxButtons.OK);
			}

			try
			{
				// Displayed "rest number"
				txtRestNumber.Text = m_CheckScanner.RemainingImagesEstimate.ToString();

			}
			catch(PosControlException)
			{
			}

			// Disable "CroppingArea" Group
			radCropAll.Enabled = false;
			radCropCode.Enabled = false;
			radPartNumber.Enabled = false;
		}

		/// <summary>
		/// Checkscanner attach micr.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAttachMICR_Click(object sender, System.EventArgs e)
		{
			//<<<step5>>>--Start
			// MICR Section >>>>>>>>>>>>>>>>>>>>>>>>>>>>
			// Ready to fired event
			DialogResult dialogResult;
			try
			{
				m_Micr.DataEventEnabled = true;
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

			// Set paper & Scanning
			try
			{
				m_Micr.EndInsertion();
			}
			catch(PosControlException)
			{
			}

			// CheckScanner Section >>>>>>>>>>>>>>>>>>>>>>>>>
			try
			{
				m_CheckScanner.DocumentHeight = 2756;
				m_CheckScanner.DocumentWidth = 5984;
			}
			catch(PosControlException)
			{
			}

			// DirectIO read range
			int iData = 0;
			Byte[] abyte = new Byte[0];
	
			string strReadRange = "197,0,5984,2756";
			string strReset = "";

			try
			{
				m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_READ_AREA,iData,strReadRange);
				m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_PRESCAN,iData,abyte);
				
			}
			catch(PosControlException)
			{
			}

			// Ready to fired event
			try
			{
				m_CheckScanner.DataEventEnabled = true;;
			}
			catch(PosControlException)
			{
			}

			// Read TIFF file
			try
			{
				m_CheckScanner.ImageFormat = CheckImageFormats.Tiff;
			}
			catch(PosControlException)
			{
			}

			// Timeout function.
			bBeginInsertion = false;

			do
			{
				try
				{
					bBeginInsertion = true;
					m_CheckScanner.BeginInsertion(3000);
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
					}
					bBeginInsertion = false;
				}
			}while(!bBeginInsertion);

			// Set paper & Scanning
			try
			{
				m_CheckScanner.EndInsertion();
			}
			catch(PosControlException)
			{
			}

			// Call to retrieve an image to the ImageData property
			try
			{
				m_CheckScanner.RetrieveImage(CheckScanner.CropAreaEntireImage);
				radCropAll.Enabled = true;
				radCropCode.Enabled = true;
				radPartNumber.Enabled = true;
				radCropAll.Checked = true;
				m_iCropAreaId = CROP_AREA_ID_ALL_AREA;
			}
			catch(PosControlException)
			{
			}

			// Clear DirectIO setting area
			try
			{
				m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_READ_AREA,iData,strReset);
			
			}
			catch(PosControlException)
			{
			}
			//<<<step5>>>--End

		}

		/// <summary>
		/// When the method "ChangeButtonStatus" was called,
		/// all buttons other than a button "Exit" become invalid.
		/// </summary>
		private void ChangeButtonStatus()
		{
			btnRead.Enabled = false;
			btnStore.Enabled = false;
			btnCreateFile.Enabled = false;
			radCropAll.Enabled = false;
			radCropCode.Enabled = false;
			radPartNumber.Enabled = false;
			btnAttachMICR.Enabled = false;
			btnReadMemory.Enabled = false;
			cmbMemoyData.Enabled = false;
			cmbCurrentMode.Enabled = false;
		}

		/// <summary>
		/// Change Scanne mode (checkscanner or cardscanner).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbCurrentMode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//<<<step6>>>--Start
			int iData = 0;
			byte[] abyte = new Byte[0];
			
			// Action when item changed
			if(cmbCurrentMode.Text.Equals("CheckScanner"))
			{
				iData = EpsonCheckScannerConst.CHK_DI_MODE_CHECKSCANNER;
				btnAttachMICR.Enabled = true;
			}
			else if(cmbCurrentMode.Text.Equals("CardScanner"))
			{
				iData = EpsonCheckScannerConst.CHK_DI_MODE_CARDSCANNER;
				btnAttachMICR.Enabled = false;
			}
			else
			{
				return;
			}

			// Change mode
			try
			{
				m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_CHANGE_MODE, iData, abyte);
			}
			catch (PosControlException) 
			{
				MessageBox.Show("An error occurred in the direct IO CHANGE_MODE method. ",
					"directIO Error", MessageBoxButtons.OK);

				return;
			}
			//<<<step6>>>--End
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
		private void frmStep6_Load(object sender, System.EventArgs e)
		{
			//Use a Logical Device Name which has been set on the SetupPOS.
			string strLogicalCSanName = "CheckScanner";
			string strLogicalMicrName = "Micr";

			//Create PosExplorer
			PosExplorer posExplorer = new PosExplorer();

			DeviceInfo deviceCScanInfo = null;
			DeviceInfo deviceMicrInfo = null;

			//<<<step5>>>--Start
			try
			{
				deviceCScanInfo = posExplorer.GetDevice(DeviceType.CheckScanner,strLogicalCSanName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","CheckSample_Step6");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				m_CheckScanner =(CheckScanner)posExplorer.CreateInstance(deviceCScanInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","CheckSample_Step6",MessageBoxButtons.OK);
				
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
					,"CheckSample_Step6",MessageBoxButtons.OK);

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
				MessageBox.Show("Failed to get exclusive rights to the device.","CheckSample_Step6"
					,MessageBoxButtons.OK);

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

				MessageBox.Show("Now the device is disable to use.","CheckSample_Step6",MessageBoxButtons.OK);
				
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

			//<<<step6>>>--Start
			// directIO 2nd Param
			int iData = 0;
			Byte[] abyte = new Byte[0];
			DirectIOData directIOReturn;
			
			try 
			{
				// Get support function
				directIOReturn = m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_GET_SUPPORT_FUNCTION
					, iData, abyte);
			}
			catch (PosControlException) 
			{
				MessageBox.Show("An error occurred in the direct IO GET_SUPPORT_FUNCTION method."
					,"directIO Error",MessageBoxButtons.OK);
				return;
			}

			if(directIOReturn.Data != 0)
			{
				if((directIOReturn.Data & EpsonCheckScannerConst.CHK_DI_CHECKSCANNER) != 0)
				{
					cmbCurrentMode.Items.Add("CheckScanner");
				}

				if((directIOReturn.Data & EpsonCheckScannerConst.CHK_DI_CARDSCANNER) != 0)
				{
					cmbCurrentMode.Items.Add("CardScanner");
				}

				cmbCurrentMode.SelectedIndex = 0;
			}

			//<<<step6>>>--End
			
			//<<<step5>>>--Start
			try
			{
				
				deviceMicrInfo = posExplorer.GetDevice(DeviceType.Micr,strLogicalMicrName);
			}
			catch(Exception)
			{
				MessageBox.Show("Failed to get device information.","CheckSample_Step6");
				//Nothing can be used.
				ChangeButtonStatus();
				return;
			}

			try
			{
				m_Micr =(Micr)posExplorer.CreateInstance(deviceMicrInfo);
			}
			catch(Exception)
			{
				//Failed CreateInstance
				MessageBox.Show("Failed to create instance","CheckSample_Step6"
					,MessageBoxButtons.OK);
				
				//Disable button
				ChangeButtonStatus();
				return;
			}
			
			//Register EventHandler
			AddDataEvent(m_Micr);
			AddErrorEvent(m_Micr);

			try
			{
				//Open the device
				//Use a Logical Device Name which has been set on the SetupPOS.
				m_Micr.Open();
			}
			catch(PosControlException)
			{

				MessageBox.Show("This device has not been registered, or cannot use."
					,"CheckSample_Step6",MessageBoxButtons.OK);

				ChangeButtonStatus();
				return;
			}
			
			try
			{
				//Get the exclusive control right for the opened device.
				//Then the device is disable from other application.
				m_Micr.Claim(1000);
			}
			catch(PosControlException)
			{
				MessageBox.Show("Failed to get exclusive rights to the device."
					,"CheckSample_Step6",MessageBoxButtons.OK);

				ChangeButtonStatus();
				return;
			}

			// Power reporting
			try
			{
				if(m_Micr.CapPowerReporting != PowerReporting.None)
				{
					m_Micr.PowerNotify = PowerNotification.Enabled;
				}
			}
			catch(PosControlException)
			{
			}

			try
			{
				//Enable the device.
				m_Micr.DeviceEnabled = true;
			}
			catch(PosControlException)
			{

				MessageBox.Show("Now the device is disable to use.","CheckSample_Step6"
					,MessageBoxButtons.OK);
				
				ChangeButtonStatus();
				return;
			}

			try
			{
				//Ready to fired event
				m_Micr.DataEventEnabled = true;
			}
			catch(PosControlException)
			{
			}
	
			//Set ComboBox Item
			cmbMemoyData.Items.Add("Step2");
			cmbMemoyData.Items.Add("Step3");
			cmbMemoyData.Items.Add("Step4");
			cmbMemoyData.Items.Add("Step5");
			cmbMemoyData.Items.Add("Step6");
			
			cmbMemoyData.SelectedIndex = 0;
			//<<<step5>>>--End

		}

		/// <summary>
		/// When the method "closing" is called,
		/// the following code is run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmStep6_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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


			//<<<step5>>>--Start
			if(m_Micr != null)
			{
				try
				{
					// Remove DataEventHandler
					RemoveDataEvent(m_Micr);

					// Remove ErrorEventHandler
					RemoveErrorEvent(m_Micr);

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
			//<<<step5>>>--End
		}

		/// <summary>
		/// AddDataEventHadler.
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

			//<<<step5>>>--Start
			if(source is CheckScanner)
			{
				MessageBox.Show("CheckScanner Error\n" + "ErrorCode =" + e.ErrorCode.ToString() + "\n"
					+ "ErrorCodeExtended =" +e.ErrorCodeExtended.ToString() ,"CheckSample_Step6"
					,MessageBoxButtons.OK);
			}

			
			else if(source is Micr)
			{
				MessageBox.Show("Micr Error\n" + "ErrorCode =" + e.ErrorCode.ToString() + "\n"
					+ "ErrorCodeExtended =" +e.ErrorCodeExtended.ToString() ,"CheckSample_Step6"
					,MessageBoxButtons.OK);

				m_strMicrRawData = "ErrorData";
			}
			//<<<step5>>>--End
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

			//<<<step5>>>--Start
			if(source is CheckScanner)
			{
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

				btnStore.Enabled = true;
				btnCreateFile.Enabled = true;
			}

			
			else if(source is Micr)
			{
				try
				{
					m_strMicrRawData = m_Micr.RawData;
				}
				catch(PosControlException)
				{
				}

				try
				{
					m_Micr.DataEventEnabled = true;
				}
				catch(PosControlException)
				{
				}
			}

			//<<<step5>>>--End
		}		
	}
}
