Imports Microsoft.PointOfService
Imports System.Reflection
Imports System.IO
Imports jp.co.epson.uposcommon
Imports System.Diagnostics


Public Class FrameStep7
    Inherits System.Windows.Forms.Form

    'CheckScanner object
    Private m_CheckScanner As CheckScanner = Nothing

    'Now Step
    Private m_strStep As String = "CheckSample_Step7"

    Private Const CROP_AREA_ID_ALL_AREA As Integer = CheckScanner.CropAreaEntireImage
    Private Const CROP_AREA_ID_PART_OF_CODE As Integer = 2
    Private Const CROP_AREA_ID_PART_OF_NUMBER As Integer = 3

    'CheckScaner CropAreID
    Private m_iCropAreaId As Integer = CROP_AREA_ID_ALL_AREA

    'Micr object
    Private m_Micr As Micr = Nothing

    'Micr RawData
    Private m_strMicrRawData As String = ""

#Region " Windows Forms Designer generated code. "

    Public Sub New()
        MyBase.New()

        ' The InitializeComponent() call is required for windows Forms designer support.
        InitializeComponent()

        ' TODO: Add counstructor code after the InitializeComponent() call.

    End Sub

    ' Rear treatment is carried out in the resource being used.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Design  variable.
    Private components As System.ComponentModel.IContainer

    ' This method is required for Windows Forms designer support.
    'Do not change the method contents inside the source code editor.   
    ' The Forms designer might not be able to load this method if it was changed manually.
    Friend WithEvents lblImageSize As System.Windows.Forms.Label
    Friend WithEvents txtDataSize As System.Windows.Forms.TextBox
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblStore As System.Windows.Forms.Label
    Friend WithEvents btnStore As System.Windows.Forms.Button
    Friend WithEvents txtRestNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateFile As System.Windows.Forms.Button
    Friend WithEvents grpCroppingArea As System.Windows.Forms.GroupBox
    Friend WithEvents radCropAll As System.Windows.Forms.RadioButton
    Friend WithEvents radCropCode As System.Windows.Forms.RadioButton
    Friend WithEvents radPartNumber As System.Windows.Forms.RadioButton
    Friend WithEvents btnAttachMICR As System.Windows.Forms.Button
    Friend WithEvents btnReadMemory As System.Windows.Forms.Button
    Friend WithEvents cmbMemoyData As System.Windows.Forms.ComboBox
    Friend WithEvents lblCurrentMode As System.Windows.Forms.Label
    Friend WithEvents cmbCurrentMode As System.Windows.Forms.ComboBox
    Friend WithEvents btnRetrieveStatistics As System.Windows.Forms.Button
    Friend WithEvents txtStatistics As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblImageSize = New System.Windows.Forms.Label
        Me.txtDataSize = New System.Windows.Forms.TextBox
        Me.btnRead = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblStore = New System.Windows.Forms.Label
        Me.txtRestNumber = New System.Windows.Forms.TextBox
        Me.btnStore = New System.Windows.Forms.Button
        Me.btnCreateFile = New System.Windows.Forms.Button
        Me.grpCroppingArea = New System.Windows.Forms.GroupBox
        Me.radPartNumber = New System.Windows.Forms.RadioButton
        Me.radCropCode = New System.Windows.Forms.RadioButton
        Me.radCropAll = New System.Windows.Forms.RadioButton
        Me.btnAttachMICR = New System.Windows.Forms.Button
        Me.btnReadMemory = New System.Windows.Forms.Button
        Me.cmbMemoyData = New System.Windows.Forms.ComboBox
        Me.cmbCurrentMode = New System.Windows.Forms.ComboBox
        Me.lblCurrentMode = New System.Windows.Forms.Label
        Me.btnRetrieveStatistics = New System.Windows.Forms.Button
        Me.txtStatistics = New System.Windows.Forms.TextBox
        Me.grpCroppingArea.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblImageSize
        '
        Me.lblImageSize.Location = New System.Drawing.Point(80, 32)
        Me.lblImageSize.Name = "lblImageSize"
        Me.lblImageSize.Size = New System.Drawing.Size(68, 24)
        Me.lblImageSize.TabIndex = 0
        Me.lblImageSize.Text = "ImageSize :"
        '
        'txtDataSize
        '
        Me.txtDataSize.Location = New System.Drawing.Point(168, 32)
        Me.txtDataSize.Name = "txtDataSize"
        Me.txtDataSize.ReadOnly = True
        Me.txtDataSize.Size = New System.Drawing.Size(268, 19)
        Me.txtDataSize.TabIndex = 1
        Me.txtDataSize.Text = ""
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(20, 108)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(124, 28)
        Me.btnRead.TabIndex = 2
        Me.btnRead.Text = "Read"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(352, 380)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 28)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        '
        'lblStore
        '
        Me.lblStore.Location = New System.Drawing.Point(16, 64)
        Me.lblStore.Name = "lblStore"
        Me.lblStore.Size = New System.Drawing.Size(136, 24)
        Me.lblStore.TabIndex = 4
        Me.lblStore.Text = "Rest number of ""Store"" :"
        '
        'txtRestNumber
        '
        Me.txtRestNumber.Location = New System.Drawing.Point(168, 67)
        Me.txtRestNumber.Name = "txtRestNumber"
        Me.txtRestNumber.ReadOnly = True
        Me.txtRestNumber.Size = New System.Drawing.Size(268, 19)
        Me.txtRestNumber.TabIndex = 5
        Me.txtRestNumber.Text = ""
        '
        'btnStore
        '
        Me.btnStore.Enabled = False
        Me.btnStore.Location = New System.Drawing.Point(20, 182)
        Me.btnStore.Name = "btnStore"
        Me.btnStore.Size = New System.Drawing.Size(124, 28)
        Me.btnStore.TabIndex = 6
        Me.btnStore.Text = "Store"
        '
        'btnCreateFile
        '
        Me.btnCreateFile.Enabled = False
        Me.btnCreateFile.Location = New System.Drawing.Point(20, 219)
        Me.btnCreateFile.Name = "btnCreateFile"
        Me.btnCreateFile.Size = New System.Drawing.Size(124, 28)
        Me.btnCreateFile.TabIndex = 7
        Me.btnCreateFile.Text = "CreateFile"
        '
        'grpCroppingArea
        '
        Me.grpCroppingArea.Controls.Add(Me.radPartNumber)
        Me.grpCroppingArea.Controls.Add(Me.radCropCode)
        Me.grpCroppingArea.Controls.Add(Me.radCropAll)
        Me.grpCroppingArea.Location = New System.Drawing.Point(168, 96)
        Me.grpCroppingArea.Name = "grpCroppingArea"
        Me.grpCroppingArea.Size = New System.Drawing.Size(272, 152)
        Me.grpCroppingArea.TabIndex = 8
        Me.grpCroppingArea.TabStop = False
        Me.grpCroppingArea.Text = "Cropping area"
        '
        'radPartNumber
        '
        Me.radPartNumber.Enabled = False
        Me.radPartNumber.Location = New System.Drawing.Point(16, 116)
        Me.radPartNumber.Name = "radPartNumber"
        Me.radPartNumber.Size = New System.Drawing.Size(236, 20)
        Me.radPartNumber.TabIndex = 2
        Me.radPartNumber.Text = "Part of number (5100,236,490,214)"
        '
        'radCropCode
        '
        Me.radCropCode.Enabled = False
        Me.radCropCode.Location = New System.Drawing.Point(16, 74)
        Me.radCropCode.Name = "radCropCode"
        Me.radCropCode.Size = New System.Drawing.Size(236, 20)
        Me.radCropCode.TabIndex = 1
        Me.radCropCode.Text = "Part of code (3500,2244,2000,456)"
        '
        'radCropAll
        '
        Me.radCropAll.Enabled = False
        Me.radCropAll.Location = New System.Drawing.Point(16, 32)
        Me.radCropAll.Name = "radCropAll"
        Me.radCropAll.Size = New System.Drawing.Size(236, 20)
        Me.radCropAll.TabIndex = 0
        Me.radCropAll.Text = "All area (0,0,5984,2756)"
        '
        'btnAttachMICR
        '
        Me.btnAttachMICR.Location = New System.Drawing.Point(20, 145)
        Me.btnAttachMICR.Name = "btnAttachMICR"
        Me.btnAttachMICR.Size = New System.Drawing.Size(124, 28)
        Me.btnAttachMICR.TabIndex = 9
        Me.btnAttachMICR.Text = "Attached MICR data"
        '
        'btnReadMemory
        '
        Me.btnReadMemory.Location = New System.Drawing.Point(20, 256)
        Me.btnReadMemory.Name = "btnReadMemory"
        Me.btnReadMemory.Size = New System.Drawing.Size(124, 28)
        Me.btnReadMemory.TabIndex = 10
        Me.btnReadMemory.Text = "Read memory data"
        '
        'cmbMemoyData
        '
        Me.cmbMemoyData.Location = New System.Drawing.Point(168, 260)
        Me.cmbMemoyData.Name = "cmbMemoyData"
        Me.cmbMemoyData.Size = New System.Drawing.Size(272, 20)
        Me.cmbMemoyData.TabIndex = 11
        '
        'cmbCurrentMode
        '
        Me.cmbCurrentMode.Location = New System.Drawing.Point(168, 300)
        Me.cmbCurrentMode.Name = "cmbCurrentMode"
        Me.cmbCurrentMode.Size = New System.Drawing.Size(272, 20)
        Me.cmbCurrentMode.TabIndex = 12
        '
        'lblCurrentMode
        '
        Me.lblCurrentMode.Location = New System.Drawing.Point(72, 300)
        Me.lblCurrentMode.Name = "lblCurrentMode"
        Me.lblCurrentMode.Size = New System.Drawing.Size(72, 24)
        Me.lblCurrentMode.TabIndex = 13
        Me.lblCurrentMode.Text = "CurrentMode"
        '
        'btnRetrieveStatistics
        '
        Me.btnRetrieveStatistics.Location = New System.Drawing.Point(20, 336)
        Me.btnRetrieveStatistics.Name = "btnRetrieveStatistics"
        Me.btnRetrieveStatistics.Size = New System.Drawing.Size(124, 28)
        Me.btnRetrieveStatistics.TabIndex = 14
        Me.btnRetrieveStatistics.Text = "RetrieveStatistics"
        '
        'txtStatistics
        '
        Me.txtStatistics.Location = New System.Drawing.Point(168, 340)
        Me.txtStatistics.Name = "txtStatistics"
        Me.txtStatistics.Size = New System.Drawing.Size(272, 19)
        Me.txtStatistics.TabIndex = 15
        Me.txtStatistics.Text = "ModelName,HoursPoweredCount,GoodReadCount"
        '
        'FrameStep7
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(484, 417)
        Me.Controls.Add(Me.txtStatistics)
        Me.Controls.Add(Me.btnRetrieveStatistics)
        Me.Controls.Add(Me.lblCurrentMode)
        Me.Controls.Add(Me.cmbCurrentMode)
        Me.Controls.Add(Me.cmbMemoyData)
        Me.Controls.Add(Me.btnReadMemory)
        Me.Controls.Add(Me.btnAttachMICR)
        Me.Controls.Add(Me.grpCroppingArea)
        Me.Controls.Add(Me.btnCreateFile)
        Me.Controls.Add(Me.btnStore)
        Me.Controls.Add(Me.txtRestNumber)
        Me.Controls.Add(Me.txtDataSize)
        Me.Controls.Add(Me.lblStore)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnRead)
        Me.Controls.Add(Me.lblImageSize)
        Me.MaximizeBox = False
        Me.Name = "FrameStep7"
        Me.Text = "Step 7 Obtains the statistics of the device."
        Me.grpCroppingArea.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    ''' <summary>
    ''' Read check.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRead_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnRead.Click

        '<<<step1>>>--Start
        Dim dialogResult As DialogResult
        Dim bBeginInsertion As Boolean = False

        Try

            'EventClear
            m_CheckScanner.ClearInput()

        Catch ex As PosControlException

        End Try

        Try

            m_CheckScanner.DataEventEnabled = True

        Catch ex As PosControlException

        End Try

        Do

            Try

                m_CheckScanner.BeginInsertion(3000)
                bBeginInsertion = True

            Catch ex As PosControlException

                If ex.ErrorCode = ErrorCode.Timeout Then

                    dialogResult = MessageBox.Show("Please insert a check." _
                    , "beginInsertion timeout", MessageBoxButtons.YesNo)

                    If dialogResult = dialogResult.No Then

                        Exit Sub

                    End If

                    bBeginInsertion = False

                Else

                    Return

                End If

            End Try

        Loop Until bBeginInsertion = True

        Try

            ' Set paper & Scanning
            m_CheckScanner.EndInsertion()

        Catch ex As PosControlException

        End Try

        Try

            'Call to retrieve an image to the ImageData proparty
            m_CheckScanner.RetrieveImage(CheckScanner.CropAreaEntireImage)

            '<<<step4>>>--Start
            radCropAll.Enabled = True
            radCropCode.Enabled = True
            radPartNumber.Enabled = True
            radCropAll.Checked = True
            '<<<step4>>>--End

        Catch ex As PosControlException

        End Try
        '<<<step1>>>--End

    End Sub
    ''' <summary>
    ''' Image file store use fileId and ImageTagdata
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnStore_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnStore.Click

        '<<<step2>>>--Start
        Try
            'Set FileID
            m_CheckScanner.FileId = "Epson CheckScanner SampleProgram Step7"

        Catch ex As PosControlException

        End Try

        Try

            '<<<step5>>>--Start
            'Set ImageTagData
            m_CheckScanner.ImageTagData = "Epson CheckScanner SampleProgram Step7 ImageTagData" _
            + m_strMicrRawData
            '<<<step5>>>--End

        Catch ex As PosControlException

        End Try

        Try

            'StoreImage Data File
            m_CheckScanner.StoreImage(m_iCropAreaId)


        Catch ex As PosControlException

        End Try

        Try

            'Displayed "rest number"
            txtRestNumber.Text = m_CheckScanner.RemainingImagesEstimate.ToString()

        Catch ex As PosControlException

        End Try

        MessageBox.Show("A data was stored.", m_strStep, MessageBoxButtons.OK)
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' Create image file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCreateFile_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnCreateFile.Click

        '<<<step3>>>--Start
        Dim Image As Bitmap = Nothing
        Dim strImageFormat As String = ""
        Dim strImageFilePath As String

        strImageFilePath = Directory.GetCurrentDirectory()

        Image = m_CheckScanner.ImageData

        If Not Image Is Nothing Then

            If Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff) Then

                strImageFormat = "tif"

            ElseIf Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp) Then

                strImageFormat = "bmp"

            ElseIf Image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg) Then

                strImageFormat = "jpg"

            End If

        End If

        strImageFilePath += "\\Step7." + strImageFormat

        Image.Save(strImageFilePath)

        btnCreateFile.Enabled = False

        'Finish message
        MessageBox.Show("A file was created", m_strStep, MessageBoxButtons.OK)
        '<<<step3>>>--End

    End Sub
    ''' <summary>
    ''' Define crop area
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub radCropAll_CheckedChanged(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles radCropAll.CheckedChanged

        '<<<step4>>>--Start
        m_iCropAreaId = CROP_AREA_ID_ALL_AREA

        Try

            m_CheckScanner.DataEventEnabled = True

            m_CheckScanner.DefineCropArea(m_iCropAreaId, 0, 0 _
             , CheckScanner.CropAreaRight _
             , CheckScanner.CropAreaBottom)

            m_CheckScanner.RetrieveImage(m_iCropAreaId)


        Catch ex As PosControlException

        End Try
        '<<<step4>>>--End

    End Sub
    ''' <summary>
    ''' Define crop area
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub radCropCode_CheckedChanged(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles radCropCode.CheckedChanged

        '<<<step4>>>--Start
        m_iCropAreaId = CROP_AREA_ID_PART_OF_CODE

        Try

            m_CheckScanner.DataEventEnabled = True

            m_CheckScanner.DefineCropArea(m_iCropAreaId, 3500, 2244, 2000, 456)

            m_CheckScanner.RetrieveImage(m_iCropAreaId)

        Catch ex As PosControlException

        End Try
        '<<<step4>>>--End

    End Sub
    ''' <summary>
    ''' Define crop area
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub radPartNumber_CheckedChanged(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles radPartNumber.CheckedChanged

        '<<<step4>>>--Start
        m_iCropAreaId = CROP_AREA_ID_PART_OF_NUMBER

        Try

            m_CheckScanner.DataEventEnabled = True

            m_CheckScanner.DefineCropArea(m_iCropAreaId, 5100, 236, 490, 214)

            m_CheckScanner.RetrieveImage(m_iCropAreaId)


        Catch ex As PosControlException

        End Try
        '<<<step4>>>--End

    End Sub
    ''' <summary>
    ''' CheckScanner read memory and get image.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnReadMemory_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnReadMemory.Click

        '<<<step5>>>--Start
        Dim strStep As String = "Epson CheckScanner SampleProgram "
        strStep += cmbMemoyData.Text

        Try

            'Ready to fired event
            m_CheckScanner.DataEventEnabled = True

        Catch ex As PosControlException

        End Try

        Try

            m_CheckScanner.FileId = strStep


        Catch ex As PosControlException

        End Try

        Try

            'Speciffic "FileID"
            m_CheckScanner.RetrieveMemory(CheckImageLocate.FileId)
            m_CheckScanner.ClearImage(CheckImageClear.FileId)

        Catch ex As PosControlException

            ' Error MessageBox
            MessageBox.Show("No data!", "CheckScanner Error", MessageBoxButtons.OK)

        End Try

        Try

            ' Displayed "rest number"
            txtRestNumber.Text = m_CheckScanner.RemainingImagesEstimate.ToString()

        Catch ex As PosControlException

        End Try

        'Disable "CroppingArea" Group
        radCropAll.Enabled = False
        radCropCode.Enabled = False
        radPartNumber.Enabled = False
        '<<<step5>>>--End

    End Sub
    ''' <summary>
    ''' Checkscanner attach micr.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAttachMICR_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnAttachMICR.Click

        '<<<step5>>>--Start
        ' MICR Section >>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' Ready to fired event
        Dim bBeginInsertion As Boolean = False
        Dim dialogResult As DialogResult

        Try

            m_Micr.DataEventEnabled = True

        Catch ex As PosControlException

        End Try

        Do
            Try

                m_Micr.BeginInsertion(3000)
                bBeginInsertion = True

            Catch ex As PosControlException

                If ex.ErrorCode = ErrorCode.Timeout Then

                    dialogResult = MessageBox.Show("Please insert a check.", _
                        "beginInsertion timeout", MessageBoxButtons.YesNo)

                    If dialogResult = dialogResult.No Then

                        Exit Sub

                    End If

                    bBeginInsertion = False

                Else

                    Return

                End If

            End Try

        Loop Until bBeginInsertion = True

        Try
            ' Set paper & Scanning
            m_Micr.EndInsertion()

        Catch ex As PosControlException

        End Try

        ' CheckScanner Section >>>>>>>>>>>>>>>>>>>>>>>>>
        Try

            m_CheckScanner.DocumentHeight = 2756
            m_CheckScanner.DocumentWidth = 5984

        Catch ex As PosControlException

        End Try

        'DirectIO read range
        Dim iData As Integer
        Dim abyte() As Byte = {0}
        Dim strReadRange As String = "197,0,5984,2756"
        Dim strReset As String = ""

        Try

            m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_READ_AREA, iData, strReadRange)
            m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_PRESCAN, iData, abyte)

        Catch ex As PosControlException

        End Try

        'Ready to fired event
        Try

            m_CheckScanner.DataEventEnabled = True

        Catch ex As PosControlException


        End Try

        'Read TIFF file
        Try

            m_CheckScanner.ImageFormat = CheckImageFormats.Tiff

        Catch ex As PosControlException

        End Try

        ' Timeout function.
        bBeginInsertion = False

        Do

            Try

                m_CheckScanner.BeginInsertion(3000)
                bBeginInsertion = True

            Catch ex As PosControlException

                If ex.ErrorCode = ErrorCode.Timeout Then

                    dialogResult = MessageBox.Show("Please insert a check.", _
                        "beginInsertion timeout", MessageBoxButtons.YesNo)

                    If dialogResult = dialogResult.No Then

                        Exit Sub

                    End If

                End If

                bBeginInsertion = False

            End Try

        Loop Until bBeginInsertion = True

        ' Set paper & Scanning
        Try

            m_CheckScanner.EndInsertion()

        Catch ex As PosControlException

        End Try

        Try

            ' Call to retrieve an image to the ImageData property
            m_CheckScanner.RetrieveImage(CheckScanner.CropAreaEntireImage)
            radCropAll.Enabled = True
            radCropCode.Enabled = True
            radPartNumber.Enabled = True
            radCropAll.Checked = True
            m_iCropAreaId = CROP_AREA_ID_ALL_AREA

        Catch ex As PosControlException

        End Try

        'Clear DirectIO setting area
        Try

            m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_READ_AREA, iData, strReset)

        Catch ex As PosControlException

        End Try
        '<<<step5>>>--End

    End Sub
    ''' <summary>
    ''' Change Scanne mode (checkscanner or cardscanner).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmbCurrentMode_SelectedIndexChanged(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles cmbCurrentMode.SelectedIndexChanged

        '<<<step6>>>--Start
        Dim iData As Integer = 0
        Dim abyte() As Byte = {0}

        'Action when item changed
        If cmbCurrentMode.Text.Equals("CheckScanner") Then

            iData = EpsonCheckScannerConst.CHK_DI_MODE_CHECKSCANNER
            btnAttachMICR.Enabled = True

        ElseIf cmbCurrentMode.Text.Equals("CardScanner") Then

            iData = EpsonCheckScannerConst.CHK_DI_MODE_CARDSCANNER
            btnAttachMICR.Enabled = False

        Else

            Return

        End If

        'Change mode
        Try

            m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_CHANGE_MODE, iData, abyte)

        Catch ex As PosControlException

            MessageBox.Show("An error occurred in the direct IO CHANGE_MODE method. ", _
            "directIO Error", MessageBoxButtons.OK)

            Return

        End Try
        '<<<step6>>>--End

    End Sub
    ''' <summary>
    '''  A method "RetrieveStatistics" calls RetrieveStatistics method.
    '''  This method obtains the statistics of device.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRetrieveStatistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetrieveStatistics.Click

        '<<<step7>>>--Start
        Dim astrStatistics As String() = Nothing
        Dim strReturn As String = ""
        Dim strSaveXmlPath As String = ""

        'Current Directory Path
        Dim strCurDir As String = Directory.GetCurrentDirectory()

        strSaveXmlPath += strCurDir + "\\" + "Sample.xml"

        astrStatistics = txtStatistics.Text.Split(",")

        Try
            strReturn = m_CheckScanner.RetrieveStatistics(astrStatistics)

            If File.Exists(strSaveXmlPath) Then

                File.Delete(strSaveXmlPath)

            End If

            Try
                Dim sw As StreamWriter = File.CreateText(strSaveXmlPath)

                sw.Write(strReturn)

                sw.Close()

                'show xml file
                Process.Start(strSaveXmlPath)

            Catch ex As IOException

            End Try

        Catch ex As Exception

        End Try
        '<<<step7>>>--End

    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnRead.Enabled = False
        btnStore.Enabled = False
        btnCreateFile.Enabled = False
        radCropAll.Enabled = False
        radCropCode.Enabled = False
        radPartNumber.Enabled = False
        btnReadMemory.Enabled = False
        btnAttachMICR.Enabled = False
        cmbMemoyData.Enabled = False
        cmbCurrentMode.Enabled = False
        btnRetrieveStatistics.Enabled = False
        txtStatistics.Enabled = False

    End Sub
    ''' <summary>
    ''' Form close.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExit_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnExit.Click

        Close()

    End Sub
    ''' <summary>
    ''' The processing code required in order to enable to use of service is written here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep7_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strCScanLogicalName As String
        Dim strMicrLogicalName As String
        Dim deviceCScanInfo As DeviceInfo = Nothing
        Dim deviceMicrInfo As DeviceInfo = Nothing

        Dim posExplorer As PosExplorer

        strCScanLogicalName = "CheckScanner"
        strMicrLogicalName = "Micr"

        'Create PosExplorer
        posExplorer = New PosExplorer

        '<<<step5>>>--Start
        Try

            deviceCScanInfo = posExplorer.GetDevice(DeviceType.CheckScanner, strCScanLogicalName)

        Catch ex As Exception

            MessageBox.Show("Fails to get device information.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            m_CheckScanner = posExplorer.CreateInstance(deviceCScanInfo)

        Catch ex As Exception

            'Fails CreateInstance
            MessageBox.Show("Fails to create instance", m_strStep _
             , MessageBoxButtons.OK)

            'Disable button
            ChangeButtonStatus()
            Return

        End Try

        'Register DataEventHandler.
        AddDataEvent(m_CheckScanner)

        'Register ErrorEventHandler.
        AddErrorEvent(m_CheckScanner)

        Try

            'Open the device
            m_CheckScanner.Open()

        Catch ex As PosControlException

            MessageBox.Show("Fails to open the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()

        End Try

        Try

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_CheckScanner.Claim(1000)

        Catch ex As PosControlException

            MessageBox.Show("Fails to claim the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        ' Power reporting
        Try

            If Not m_CheckScanner.CapPowerReporting = PowerReporting.None Then

                m_CheckScanner.PowerNotify = PowerNotification.Enabled

            End If

        Catch ex As PosControlException

        End Try

        Try

            'Enable the device.
            m_CheckScanner.DeviceEnabled = True

        Catch ex As PosControlException

            MessageBox.Show("Disable to use the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            'Ready to fired event
            m_CheckScanner.DataEventEnabled = True

        Catch ex As PosControlException

        End Try

        Try
            'Read TIFF file format
            m_CheckScanner.ImageFormat = CheckImageFormats.Tiff

            'Read BMP file format
            'm_CheckScanner.ImageFormat = CheckImageFormats.Bmp

            'Read JPEG file format
            'm_CheckScanner.ImageFormat = CheckImageFormats.Jpeg

        Catch ex As PosControlException

        End Try

        '<<<step6>>>--Start
        ' directIO 2nd Param
        Dim iData As Integer = 0
        Dim abyte() As Byte = {0}
        Dim directIOReturn As DirectIOData

        Try

            ' Get support function
            directIOReturn = m_CheckScanner.DirectIO(EpsonCheckScannerConst.CHK_DI_GET_SUPPORT_FUNCTION, _
            iData, abyte)

        Catch ex As PosControlException

            MessageBox.Show("An error occurred in the direct IO GET_SUPPORT_FUNCTION method." _
            , "directIO Error", MessageBoxButtons.OK)
            Return

        End Try

        If Not directIOReturn.Data = 0 Then

            If Not (directIOReturn.Data And EpsonCheckScannerConst.CHK_DI_CHECKSCANNER) = 0 Then

                cmbCurrentMode.Items.Add("CheckScanner")

            End If

            If Not (directIOReturn.Data And EpsonCheckScannerConst.CHK_DI_CARDSCANNER) = 0 Then

                cmbCurrentMode.Items.Add("CardScanner")

            End If

            cmbCurrentMode.SelectedIndex = 0
            '<<<step6>>>--End

        End If

        ''Micr Open
        Try

            deviceMicrInfo = posExplorer.GetDevice(DeviceType.Micr, strMicrLogicalName)

        Catch ex As Exception

            MessageBox.Show("Fails to get device information.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            m_Micr = posExplorer.CreateInstance(deviceMicrInfo)

        Catch ex As Exception

            'Fails CreateInstance
            MessageBox.Show("Fails to create instance", m_strStep _
                , MessageBoxButtons.OK)

            'Disable button
            ChangeButtonStatus()
            Return

        End Try

        'Register DataEventHandler.
        AddDataEvent(m_Micr)

        'Register ErrorEventHandler.
        AddErrorEvent(m_Micr)

        Try

            'Open the device
            m_Micr.Open()

        Catch ex As PosControlException

            MessageBox.Show("Fails to open the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()

        End Try

        Try

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Micr.Claim(1000)

        Catch ex As PosControlException

            MessageBox.Show("Fails to claim the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        ' Power reporting
        Try

            If Not m_Micr.CapPowerReporting = PowerReporting.None Then

                m_Micr.PowerNotify = PowerNotification.Enabled

            End If

        Catch ex As PosControlException

        End Try


        Try

            'Enable the device.
            m_Micr.DeviceEnabled = True

        Catch ex As PosControlException

            MessageBox.Show("Disable to use the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            'Ready to fired event
            m_Micr.DataEventEnabled = True

        Catch ex As PosControlException

        End Try

        'Set ComboBox Item
        cmbMemoyData.Items.Add("Step2")
        cmbMemoyData.Items.Add("Step3")
        cmbMemoyData.Items.Add("Step4")
        cmbMemoyData.Items.Add("Step5")
        cmbMemoyData.Items.Add("Step6")
        cmbMemoyData.Items.Add("Step7")

        cmbMemoyData.SelectedIndex = 0
        '<<<step5>>>--End

        '<<<step7>>>--Start
        If m_CheckScanner.CapStatisticsReporting = False Then

            btnRetrieveStatistics.Enabled = False
            txtStatistics.Enabled = False

        End If
        '<<<step7>>>--End
    End Sub
    ''' <summary>
    ''' When the method "closing" is called,
    ''' the following code is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep7_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step1>>>--Start
        If m_CheckScanner Is Nothing Then
            Return
        End If

        Try
            'Remove DataEventHandler.
            RemoveDataEvent(m_CheckScanner)

            'Remove ErrorEventHandler.
            RemoveErrorEvent(m_CheckScanner)

            'Cancel the device
            m_CheckScanner.DeviceEnabled = False

            'Release the device exclusive control right.
            m_CheckScanner.Release()

        Catch ex As PosControlException

        Finally

            'Finish using the device.
            m_CheckScanner.Close()

        End Try
        '<<<step1>>>--End

        '<<<step5>>>--Start
        If m_Micr Is Nothing Then
            Return
        End If

        Try

            'Remove DataEventHandler.
            RemoveDataEvent(m_Micr)

            'Remove ErrorEventHandler.
            RemoveErrorEvent(m_Micr)

            'Cancel the device
            m_Micr.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Micr.Release()

        Catch ex As PosControlException

        Finally

            'Finish using the device.
            m_Micr.Close()

        End Try
        '<<<step5>>>--End

    End Sub
    ''' <summary>
    ''' Add DataEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddDataEvent(ByVal eventSource As Object)

        '<<<step1>>>--Start
        Dim dataEvent As EventInfo = Nothing

        dataEvent = eventSource.GetType().GetEvent("DataEvent")

        If Not (dataEvent Is Nothing) Then
            dataEvent.AddEventHandler(eventSource _
            , New DataEventHandler(AddressOf OnDataEvent))

        End If
        '<<<step1>>>--End

    End Sub

    ''' <summary>
    ''' Add ErrorEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddErrorEvent(ByVal eventSource As Object)

        '<<<step4>>>--Start
        Dim errorEvent As EventInfo = Nothing

        errorEvent = eventSource.GetType().GetEvent("ErrorEvent")

        If Not (errorEvent Is Nothing) Then
            errorEvent.AddEventHandler(eventSource _
            , New DeviceErrorEventHandler(AddressOf OnErrorEvent))

        End If
        '<<<step4>>>--End

    End Sub
    ''' <summary>
    ''' Remove DataEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveDataEvent(ByVal eventSource As Object)

        '<<<step1>>>--Start
        Dim dataEvent As EventInfo = Nothing

        dataEvent = eventSource.GetType().GetEvent("DataEvent")

        If Not (dataEvent Is Nothing) Then
            dataEvent.RemoveEventHandler(eventSource _
            , New DataEventHandler(AddressOf OnDataEvent))

        End If
        '<<<step1>>>--End

    End Sub
    ''' <summary>
    ''' Remove ErrorEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveErrorEvent(ByVal eventSource As Object)

        '<<<step1>>>--Start
        Dim errorEvent As EventInfo = Nothing

        errorEvent = eventSource.GetType().GetEvent("ErrorEvent")

        If Not (errorEvent Is Nothing) Then
            errorEvent.RemoveEventHandler(eventSource _
            , New DeviceErrorEventHandler(AddressOf OnErrorEvent))

        End If
        '<<<step1>>>--End

    End Sub
    ''' <summary>
    ''' Data Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnDataEvent(ByVal source As Object, ByVal e As DataEventArgs)

        If InvokeRequired Then
            'Ensure calls to Windows Form Controls are from this application's thread
            Invoke(New DataEventHandler(AddressOf OnDataEvent), New Object() {source, e})
            Return
        End If

        '<<<step1>>>--Start
        'Bitmap object
        Dim Image As Bitmap = Nothing

        '<<<step5>>>--Start
        If source Is m_CheckScanner Then

            Try

                'Get Image Data
                Image = m_CheckScanner.ImageData

                txtDataSize.Text = "Width:" + Image.Width.ToString() _
                + " Height:" + Image.Height.ToString()

                m_CheckScanner.DataEventEnabled = True

            Catch ex As PosControlException

            End Try
            '<<<step1>>>--End

            '<<<step2>>>--Start
            btnStore.Enabled = True
            '<<<step2>>>--End

            '<<<step3>>>--Start
            btnCreateFile.Enabled = True
            '<<<step3>>>--End

        ElseIf source Is m_Micr Then

            Try

                m_strMicrRawData = m_Micr.RawData

            Catch ex As PosControlException

            End Try

            Try

                m_Micr.DataEventEnabled = True

            Catch ex As PosControlException

            End Try

        End If
        '<<<step5>>>--ENd

    End Sub
    ''' <summary>
    ''' Error Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnErrorEvent(ByVal source As Object, ByVal e As DeviceErrorEventArgs)

        If InvokeRequired Then
            'Ensure calls to Windows Form Controls are from this application's thread
            Invoke(New DeviceErrorEventHandler(AddressOf OnErrorEvent), New Object() {source, e})
            Return
        End If

        '<<<step5>>>--Start
        If source Is m_CheckScanner Then

            Try

                MessageBox.Show("CheckScanner Error" + vbCrLf + "ErrorCode =" + e.ErrorCode.ToString() _
                + vbCrLf + "ErrorCodeExtended =" + e.ErrorCodeExtended.ToString() _
                , m_strStep, MessageBoxButtons.OK)

            Catch ex As PosControlException


            End Try

        ElseIf source Is m_Micr Then

            MessageBox.Show("Micr Error" + vbCrLf + "ErrorCode =" + e.ErrorCode.ToString() _
                + vbCrLf + "ErrorCodeExtended =" + e.ErrorCodeExtended.ToString() _
                , m_strStep, MessageBoxButtons.OK)

            m_strMicrRawData = "ErrorData"

        End If

    End Sub

End Class
