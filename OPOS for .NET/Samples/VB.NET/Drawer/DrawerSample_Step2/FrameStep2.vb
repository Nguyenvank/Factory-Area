Imports Microsoft.PointOfService
Imports System.Reflection

Public Class FrameStep2
    Inherits System.Windows.Forms.Form

    'CashDrawer object
    Private m_Drawer As CashDrawer = Nothing

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
    Friend WithEvents grpDrawer As System.Windows.Forms.GroupBox
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grpStatus As System.Windows.Forms.GroupBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblPower As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPower As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grpDrawer = New System.Windows.Forms.GroupBox
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpStatus = New System.Windows.Forms.GroupBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lblPower = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtPower = New System.Windows.Forms.TextBox
        Me.grpDrawer.SuspendLayout()
        Me.grpStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpDrawer
        '
        Me.grpDrawer.Controls.Add(Me.btnOpen)
        Me.grpDrawer.Location = New System.Drawing.Point(36, 16)
        Me.grpDrawer.Name = "grpDrawer"
        Me.grpDrawer.Size = New System.Drawing.Size(256, 104)
        Me.grpDrawer.TabIndex = 0
        Me.grpDrawer.TabStop = False
        Me.grpDrawer.Text = "CashDrawer"
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(56, 44)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(144, 32)
        Me.btnOpen.TabIndex = 0
        Me.btnOpen.Text = "Open"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(208, 244)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(84, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'grpStatus
        '
        Me.grpStatus.Controls.Add(Me.txtPower)
        Me.grpStatus.Controls.Add(Me.txtStatus)
        Me.grpStatus.Controls.Add(Me.lblPower)
        Me.grpStatus.Controls.Add(Me.lblStatus)
        Me.grpStatus.Location = New System.Drawing.Point(36, 132)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Size = New System.Drawing.Size(256, 100)
        Me.grpStatus.TabIndex = 2
        Me.grpStatus.TabStop = False
        Me.grpStatus.Text = "Status Now"
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(24, 28)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(60, 20)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Status"
        '
        'lblPower
        '
        Me.lblPower.Location = New System.Drawing.Point(24, 60)
        Me.lblPower.Name = "lblPower"
        Me.lblPower.Size = New System.Drawing.Size(60, 20)
        Me.lblPower.TabIndex = 1
        Me.lblPower.Text = "Power"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(92, 24)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(108, 19)
        Me.txtStatus.TabIndex = 2
        Me.txtStatus.Text = ""
        '
        'txtPower
        '
        Me.txtPower.Location = New System.Drawing.Point(92, 60)
        Me.txtPower.Name = "txtPower"
        Me.txtPower.ReadOnly = True
        Me.txtPower.Size = New System.Drawing.Size(108, 19)
        Me.txtPower.TabIndex = 3
        Me.txtPower.Text = ""
        '
        'FrameStep2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(332, 289)
        Me.Controls.Add(Me.grpStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpDrawer)
        Me.MaximizeBox = False
        Me.Name = "FrameStep2"
        Me.Text = "Step 2 Check the status of the cash drawer."
        Me.grpDrawer.ResumeLayout(False)
        Me.grpStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    ''' When the button "Open" is pushed,
    ''' a method "OpenDrawer" is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOpen_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnOpen.Click

        '<<<step2>>>--Start
        Try

            m_Drawer.OpenDrawer()

        Catch ex As PosControlException

        End Try
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnOpen.Enabled = False

    End Sub
    ''' <summary>
    ''' Form close.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnClose.Click

        Close()

    End Sub
    ''' <summary>
    ''' The processing code required in order to enable to use of service is written here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep2_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String
        Dim deviceInfo As DeviceInfo
        Dim posExplorer As PosExplorer

        strLogicalName = "CashDrawer"

        'Create PosExplorer
        posExplorer = New PosExplorer

        m_Drawer = Nothing

        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.CashDrawer, strLogicalName)
            m_Drawer = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception
            ChangeButtonStatus()
            Return
        End Try

        Try

            '<<<step2>>>--Start
            'Add StatusUpdateEventHandler
            AddStatusUpdateEvent(m_Drawer)
            '<<<step2>>--End

            'Open the device
            m_Drawer.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Drawer.Claim(1000)

            'If support the CapPowerReporting, enable the Power Reporting Requirements.
            If Not m_Drawer.CapPowerReporting = PowerReporting.None Then

                m_Drawer.PowerNotify = PowerNotification.Enabled
            End If

            'Enable the device.
            m_Drawer.DeviceEnabled = True

        Catch ex As PosControlException

            ChangeButtonStatus()

        End Try
        '<<<step1>>>--End
    End Sub
    ''' <summary>
    ''' When the method "closing" is called,
    ''' the following code is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep2_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step2>>>--Start
        If m_Drawer Is Nothing Then
            Return
        End If

        Try

            ' Remove StatusUpdateEvent listener
            RemoveStatusUpdateEvent(m_Drawer)

            'Cancel the device
            m_Drawer.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Drawer.Release()

        Catch ex As Exception

        Finally
            'Finish using the device.
            m_Drawer.Close()

        End Try
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' Add StatusUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddStatusUpdateEvent(ByVal eventSource As Object)

        '<<<step2>>>--Start
        Dim statusUpdateEvent As EventInfo = Nothing

        statusUpdateEvent = eventSource.GetType().GetEvent("StatusUpdateEvent")

        If Not (statusUpdateEvent Is Nothing) Then
            statusUpdateEvent.AddEventHandler(eventSource _
            , New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))

        End If

        '<<<step2>>>--End
    End Sub

    ''' <summary>
    ''' Remove StatusUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveStatusUpdateEvent(ByVal eventSource As Object)

        '<<<step2>>>--Start
        Dim statusupdateEvent As EventInfo = Nothing

        statusupdateEvent = eventSource.GetType().GetEvent("StatusUpdateEvent")

        If Not (statusupdateEvent Is Nothing) Then
            statusupdateEvent.RemoveEventHandler(eventSource _
            , New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))

        End If

        '<<<step2>>>--End
    End Sub
    ''' <summary>
    ''' StatusUpdate Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnStatusUpdateEvent(ByVal source As Object, ByVal e As StatusUpdateEventArgs)

        If InvokeRequired Then
            'Ensure calls to Windows Form Controls are from this application's thread
            Invoke(New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent), New Object() {source, e})
            Return
        End If

        '<<<step2>>>--Start
        'When there is a change of the status on the printer, the event is fired.
        Select Case e.Status

            'Drawer is closed.
        Case CashDrawer.StatusClosed
                Cursor = Cursors.Default
                txtStatus.Text = "Close"
                btnOpen.Enabled = True

                'Drawer is opened.
            Case CashDrawer.StatusOpen
                Cursor = Cursors.WaitCursor
                txtStatus.Text = "Open"
                btnOpen.Enabled = False

                'The Power Reporting Requirements fires the event when the device power status is changed.

                'The device is powered on.
            Case PosCommon.StatusPowerOnline
                txtPower.Text = "ready"

                'The device is powered off, or unconnected.
            Case PosCommon.StatusPowerOff
                txtPower.Text = "OFF"

                'The device is powered on, but disable to operate.
            Case PosCommon.StatusPowerOffline
                txtPower.Text = "not ready"

                'The device is powered off or off-line.
            Case PosCommon.StatusPowerOffOffline
                txtPower.Text = "Offline"

            Case Else

        End Select
        '<<<step2>>>--End

    End Sub
End Class
