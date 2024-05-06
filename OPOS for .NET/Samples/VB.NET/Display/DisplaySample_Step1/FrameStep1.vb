Imports Microsoft.PointOfService

Public Class FrameStep1
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
    Friend WithEvents btnDisplayText As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnDisplayText = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnDisplayText
        '
        Me.btnDisplayText.Location = New System.Drawing.Point(52, 56)
        Me.btnDisplayText.Name = "btnDisplayText"
        Me.btnDisplayText.Size = New System.Drawing.Size(112, 28)
        Me.btnDisplayText.TabIndex = 0
        Me.btnDisplayText.Text = "Text Display"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(204, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'FrameStep1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(292, 133)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnDisplayText)
        Me.MaximizeBox = False
        Me.Name = "FrameStep1"
        Me.Text = "Step 1 Display characters."
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    '''  Display characters for LineDisplay.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDisplayText_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnDisplayText.Click

        Try
            m_Display.DisplayText("Hello OPOS for .NET", DisplayTextMode.Normal)

        Catch ex As PosControlException

        End Try
    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnDisplayText.Enabled = False

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
        '<<<step1>>>--End
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
        If m_Display Is Nothing Then
            Return
        End If

        Try
            m_Display.ClearText()

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
End Class
