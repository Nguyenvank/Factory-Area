Imports Microsoft.PointOfService
Imports System.Reflection
Imports System.IO

Public Class FrameStep4
    Inherits System.Windows.Forms.Form

    'CheckScanner object
    Private m_CheckScanner As CheckScanner = Nothing

    'Now Step
    Private m_strStep As String = "CheckSample_Step4"

    Private Const CROP_AREA_ID_ALL_AREA As Integer = CheckScanner.CropAreaEntireImage
    Private Const CROP_AREA_ID_PART_OF_CODE As Integer = 2
    Private Const CROP_AREA_ID_PART_OF_NUMBER As Integer = 3

    'CheckScaner CropAreID
    Private m_iCropAreaId As Integer = CROP_AREA_ID_ALL_AREA

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
        Me.btnRead.Size = New System.Drawing.Size(116, 28)
        Me.btnRead.TabIndex = 2
        Me.btnRead.Text = "Read"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(348, 252)
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
        Me.btnStore.Location = New System.Drawing.Point(20, 156)
        Me.btnStore.Name = "btnStore"
        Me.btnStore.Size = New System.Drawing.Size(116, 28)
        Me.btnStore.TabIndex = 6
        Me.btnStore.Text = "Store"
        '
        'btnCreateFile
        '
        Me.btnCreateFile.Enabled = False
        Me.btnCreateFile.Location = New System.Drawing.Point(20, 204)
        Me.btnCreateFile.Name = "btnCreateFile"
        Me.btnCreateFile.Size = New System.Drawing.Size(116, 28)
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
        Me.grpCroppingArea.Size = New System.Drawing.Size(272, 144)
        Me.grpCroppingArea.TabIndex = 8
        Me.grpCroppingArea.TabStop = False
        Me.grpCroppingArea.Text = "Cropping area"
        '
        'radPartNumber
        '
        Me.radPartNumber.Enabled = False
        Me.radPartNumber.Location = New System.Drawing.Point(16, 96)
        Me.radPartNumber.Name = "radPartNumber"
        Me.radPartNumber.Size = New System.Drawing.Size(236, 20)
        Me.radPartNumber.TabIndex = 2
        Me.radPartNumber.Text = "Part of number (5100,236,490,214)"
        '
        'radCropCode
        '
        Me.radCropCode.Enabled = False
        Me.radCropCode.Location = New System.Drawing.Point(16, 62)
        Me.radCropCode.Name = "radCropCode"
        Me.radCropCode.Size = New System.Drawing.Size(236, 20)
        Me.radCropCode.TabIndex = 1
        Me.radCropCode.Text = "Part of code (3500,2244,2000,456)"
        '
        'radCropAll
        '
        Me.radCropAll.Enabled = False
        Me.radCropAll.Location = New System.Drawing.Point(16, 28)
        Me.radCropAll.Name = "radCropAll"
        Me.radCropAll.Size = New System.Drawing.Size(236, 20)
        Me.radCropAll.TabIndex = 0
        Me.radCropAll.Text = "All area (0,0,5984,2756)"
        '
        'FrameStep4
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(472, 297)
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
        Me.Name = "FrameStep4"
        Me.Text = "Step 4 Specify reading area and cropping area."
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
            m_CheckScanner.FileId = "Epson CheckScanner SampleProgram Step4"

        Catch ex As PosControlException

        End Try

        Try

            'Set ImageTagData
            m_CheckScanner.ImageTagData = "Epson CheckScanner SampleProgram Step4 ImageTagData"


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

        strImageFilePath += "\\Step4." + strImageFormat

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
    Private Sub FrameStep4_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String
        Dim deviceInfo As DeviceInfo
        Dim posExplorer As PosExplorer

        strLogicalName = "CheckScanner"

        'Create PosExplorer
        posExplorer = New PosExplorer

        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.CheckScanner, strLogicalName)
            m_CheckScanner = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception

            ChangeButtonStatus()
            Return
        End Try

        Try

            'Register DataEventHandler.
            AddDataEvent(m_CheckScanner)

            'Register ErrorEventHandler.
            AddErrorEvent(m_CheckScanner)

            'Open the device
            m_CheckScanner.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_CheckScanner.Claim(1000)

            'Enable the device.
            m_CheckScanner.DeviceEnabled = True

            'Ready to fired event
            m_CheckScanner.DataEventEnabled = True

        Catch ex As PosControlException

            ChangeButtonStatus()

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

    End Sub
    ''' <summary>
    ''' When the method "closing" is called,
    ''' the following code is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep4_Closing(ByVal sender As Object _
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

        '<<<step1>>>--Start
        MessageBox.Show("CheckScanner Error" + vbCrLf + "ErrorCode =" + e.ErrorCode.ToString() _
        + vbCrLf + "ErrorCodeExtended =" + e.ErrorCodeExtended.ToString() _
        , m_strStep, MessageBoxButtons.OK)
        '<<<step1>>>--End

    End Sub

End Class
