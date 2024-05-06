Imports Microsoft.PointOfService

Public Class FrameStep2
    Inherits System.Windows.Forms.Form

    'LineDisplay object
    Private m_Display As LineDisplay = Nothing
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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnSpecifyPosition As System.Windows.Forms.Button
    Friend WithEvents btnAcquisition As System.Windows.Forms.Button
    Friend WithEvents txtAcquisition As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSpecifyPosition = New System.Windows.Forms.Button
        Me.btnAcquisition = New System.Windows.Forms.Button
        Me.txtAcquisition = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(52, 20)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(184, 28)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "Clear"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(288, 20)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnSpecifyPosition
        '
        Me.btnSpecifyPosition.Location = New System.Drawing.Point(52, 68)
        Me.btnSpecifyPosition.Name = "btnSpecifyPosition"
        Me.btnSpecifyPosition.Size = New System.Drawing.Size(184, 28)
        Me.btnSpecifyPosition.TabIndex = 2
        Me.btnSpecifyPosition.Text = "Specify Position and Display"
        '
        'btnAcquisition
        '
        Me.btnAcquisition.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAcquisition.Location = New System.Drawing.Point(52, 112)
        Me.btnAcquisition.Name = "btnAcquisition"
        Me.btnAcquisition.Size = New System.Drawing.Size(184, 32)
        Me.btnAcquisition.TabIndex = 3
        Me.btnAcquisition.Text = "Specify Position and Acquire Character"
        '
        'txtAcquisition
        '
        Me.txtAcquisition.Location = New System.Drawing.Point(240, 124)
        Me.txtAcquisition.Name = "txtAcquisition"
        Me.txtAcquisition.Size = New System.Drawing.Size(44, 19)
        Me.txtAcquisition.TabIndex = 4
        Me.txtAcquisition.Text = ""
        '
        'FrameStep2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(396, 161)
        Me.Controls.Add(Me.txtAcquisition)
        Me.Controls.Add(Me.btnAcquisition)
        Me.Controls.Add(Me.btnSpecifyPosition)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.MaximizeBox = False
        Me.Name = "FrameStep2"
        Me.Text = "Step 2 Display characters at the specified position."
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    ''' When the button "clear" is pushed, The method
    ''' "clearText" is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClear_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnClear.Click

        '<<<step2>>>--Start
        Try

            m_Display.ClearText()

        Catch ex As PosControlException

        End Try
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' When the button "specifyPosition" is pushed,
    ''' the method "displayTextAt" is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSpecifyPosition_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnSpecifyPosition.Click

        '<<<step2>>>--Start
        Try
            'For display the text that specified.
            'displayTextAt(int row, int column, string data)
            m_Display.DisplayTextAt(1, 5, "Hello OPOS for .NET", DisplayTextMode.Normal)

        Catch ex As PosControlException

        End Try
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnClear.Enabled = False
        btnSpecifyPosition.Enabled = False
        btnAcquisition.Enabled = False

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

        strLogicalName = "LineDisplay"

        'Create PosExplorer
        posExplorer = New PosExplorer

        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.LineDisplay, strLogicalName)
            m_Display = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception
            ChangeButtonStatus()
            Return
        End Try

        Try
            'Open the device
            m_Display.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Display.Claim(1000)

            'If support the CapPowerReporting, enable the Power Reporting Requirements.
            If Not m_Display.CapPowerReporting = PowerReporting.None Then

                m_Display.PowerNotify = PowerNotification.Enabled

            End If

            'Enable the device.
            m_Display.DeviceEnabled = True

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
    Private Sub FrameStep2_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step1>>>--Start
        If m_Display Is Nothing Then
            Return
        End If

        Try
            'Cancel the device
            m_Display.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Display.Release()

        Catch ex As PosControlException

        Finally
            'Finish using the device.
            m_Display.Close()

        End Try
        '<<<step1>>>--End

    End Sub

    Private Sub btnAcquisition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcquisition.Click
        Try

            'The present cursor position is set up.
            m_Display.CursorRow = 1
            m_Display.CursorColumn = 5

            'The character of the present cursor position is acquired.
            Dim iDisplayChar As Integer = m_Display.ReadCharacterAtCursor()
            txtAcquisition.Text = iDisplayChar.ToString()

        Catch ex As PosControlException
            MessageBox.Show("Acquisition Failure")
        End Try


    End Sub
End Class
