Imports Microsoft.PointOfService

Public Class FrameStep1
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grpDrawer = New System.Windows.Forms.GroupBox
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpDrawer.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpDrawer
        '
        Me.grpDrawer.Controls.Add(Me.btnOpen)
        Me.grpDrawer.Location = New System.Drawing.Point(36, 32)
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
        Me.btnClose.Location = New System.Drawing.Point(208, 152)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'FrameStep1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(332, 193)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpDrawer)
        Me.MaximizeBox = False
        Me.Name = "FrameStep1"
        Me.Text = "Step 1 Open cash drawer."
        Me.grpDrawer.ResumeLayout(False)
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

        '<<<step1>>>--Start
        'When outputting to a printer,a mouse cursor becomes like a hourglass.
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor


        Try
            'Open the drawer by using the "OpenDrawer" method.
            m_Drawer.OpenDrawer()
            btnOpen.Enabled = False
            ' Wait during opend drawer.
            Do
                System.Threading.Thread.Sleep(100)

            Loop Until m_Drawer.DrawerOpened = True

            'When the drawer is not closed in ten seconds after opening, beep until it is closed.
            'If  that method is executed, the value is not returned until the drawer is closed.
            m_Drawer.WaitForDrawerClose(10000, 2000, 100, 1000)

            btnOpen.Enabled = True

            System.Windows.Forms.Cursor.Current = Cursors.Default

        Catch ex As Exception

        End Try
        '<<<step1>>>--End
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
    Private Sub FrameStep1_Load(ByVal sender As System.Object _
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

            'Open the device
            m_Drawer.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Drawer.Claim(1000)

            'Enable the device.
            m_Drawer.DeviceEnabled = True

        Catch ex As PosControlException

            ChangeButtonStatus()

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
        If m_Drawer Is Nothing Then
            Return
        End If

        Try
            'Cancel the device
            m_Drawer.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Drawer.Release()

        Catch ex As Exception

        Finally
            'Finish using the device.
            m_Drawer.Close()

        End Try
        '<<<step1>>>--End

    End Sub
End Class
