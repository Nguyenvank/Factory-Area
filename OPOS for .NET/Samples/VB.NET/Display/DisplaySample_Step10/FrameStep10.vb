Imports Microsoft.PointOfService

Public Class FrameStep10
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
    Friend WithEvents btnExternalCharacter As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnExternalCharacter = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnExternalCharacter
        '
        Me.btnExternalCharacter.Location = New System.Drawing.Point(28, 56)
        Me.btnExternalCharacter.Name = "btnExternalCharacter"
        Me.btnExternalCharacter.Size = New System.Drawing.Size(152, 44)
        Me.btnExternalCharacter.TabIndex = 0
        Me.btnExternalCharacter.Text = "External Character Registration"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(204, 16)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'FrameStep10
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(292, 133)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExternalCharacter)
        Me.MaximizeBox = False
        Me.Name = "FrameStep10"
        Me.Text = "Step 10 External character registration"
        Me.ResumeLayout(False)

    End Sub

#End Region
    ''' <summary>
    ''' When the button "ExternalCharacter" is pushed, The method
    ''' "DefineGlypth" is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExternalCharacter_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnExternalCharacter.Click

        '<<<step10>>>--Start
        Dim strData As String = "1c070000000000"

        'sample2 Case DM-D500
        'strData = "ff9999ffff999999ff00000000000000"

        Dim abyGlyph(strData.Length / 2 - 1) As Byte


        For i As Integer = 0 To abyGlyph.Length - 1

            abyGlyph(i) = Convert.ToByte(strData.Substring(0, 2), 16)
            strData = strData.Substring(2, strData.Length - 2)

        Next

        Try

            m_Display.DefineGlyph(65, abyGlyph)
            m_Display.DisplayText("A", DisplayTextMode.Normal)

        Catch ex As PosControlException
            ChangeButtonStatus()
            Return
        End Try

        '<<<step10>>>--End

    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnExternalCharacter.Enabled = False

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
    Private Sub FrameStep10_Load(ByVal sender As System.Object _
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
    Private Sub FrameStep10_Closing(ByVal sender As Object _
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

End Class
