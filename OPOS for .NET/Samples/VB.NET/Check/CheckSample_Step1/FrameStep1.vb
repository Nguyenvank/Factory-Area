Imports Microsoft.PointOfService
Imports System.Reflection

Public Class FrameStep1
    Inherits System.Windows.Forms.Form

    'CheckScanner object
    Private m_CheckScanner As CheckScanner = Nothing

    'Now Step
    Private m_strStep As String = "CheckSample_Step1"

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblImageSize = New System.Windows.Forms.Label
        Me.txtDataSize = New System.Windows.Forms.TextBox
        Me.btnRead = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblImageSize
        '
        Me.lblImageSize.Location = New System.Drawing.Point(16, 48)
        Me.lblImageSize.Name = "lblImageSize"
        Me.lblImageSize.Size = New System.Drawing.Size(68, 24)
        Me.lblImageSize.TabIndex = 0
        Me.lblImageSize.Text = "ImageSize :"
        '
        'txtDataSize
        '
        Me.txtDataSize.Location = New System.Drawing.Point(100, 48)
        Me.txtDataSize.Name = "txtDataSize"
        Me.txtDataSize.ReadOnly = True
        Me.txtDataSize.Size = New System.Drawing.Size(216, 19)
        Me.txtDataSize.TabIndex = 1
        Me.txtDataSize.Text = ""
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(100, 80)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(80, 28)
        Me.btnRead.TabIndex = 2
        Me.btnRead.Text = "Read"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(236, 80)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 28)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        '
        'FrameStep1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(356, 137)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnRead)
        Me.Controls.Add(Me.txtDataSize)
        Me.Controls.Add(Me.lblImageSize)
        Me.MaximizeBox = False
        Me.Name = "FrameStep1"
        Me.Text = "Step 1 Normal operation:Read data."
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

        Catch ex As PosControlException

        End Try

    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnRead.Enabled = False

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
    Private Sub FrameStep1_Load(ByVal sender As System.Object _
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
    Private Sub FrameStep1_Closing(ByVal sender As Object _
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
    ''' Remove StatusUpdateEventHandler.
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
    ''' StatusUpdate Event
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
