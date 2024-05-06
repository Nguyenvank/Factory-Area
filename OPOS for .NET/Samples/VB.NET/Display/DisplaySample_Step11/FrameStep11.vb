Imports Microsoft.PointOfService
Imports System.Reflection
Imports System.IO
Imports jp.co.epson.uposcommon
Imports System.Diagnostics

Public Class FrameStep11
    Inherits System.Windows.Forms.Form

    'LineDisplay object
    Private m_Display As LineDisplay = Nothing

    'Remembers previous status
    Private iPreviousStatus As Integer

    'Now Step
    Private m_strStep As String = "DisplaySample_Step11"
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
    Friend WithEvents btnBlinkCharacter As System.Windows.Forms.Button
    Friend WithEvents btnTeletypeCharacters As System.Windows.Forms.Button
    Friend WithEvents grpScroll As System.Windows.Forms.GroupBox
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents btnWindowControl As System.Windows.Forms.Button
    Friend WithEvents grpGraphicDisplay As System.Windows.Forms.GroupBox
    Friend WithEvents btnSetChracterOrnaments As System.Windows.Forms.Button
    Friend WithEvents btnDisplayBitmap As System.Windows.Forms.Button
    Friend WithEvents txtStatistics As System.Windows.Forms.TextBox
    Friend WithEvents btnRetrieveStatistics As System.Windows.Forms.Button
    Friend WithEvents grpStatistics As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSpecifyPosition = New System.Windows.Forms.Button
        Me.btnBlinkCharacter = New System.Windows.Forms.Button
        Me.btnTeletypeCharacters = New System.Windows.Forms.Button
        Me.grpScroll = New System.Windows.Forms.GroupBox
        Me.btnRight = New System.Windows.Forms.Button
        Me.btnLeft = New System.Windows.Forms.Button
        Me.btnWindowControl = New System.Windows.Forms.Button
        Me.grpGraphicDisplay = New System.Windows.Forms.GroupBox
        Me.btnDisplayBitmap = New System.Windows.Forms.Button
        Me.btnSetChracterOrnaments = New System.Windows.Forms.Button
        Me.grpStatistics = New System.Windows.Forms.GroupBox
        Me.btnRetrieveStatistics = New System.Windows.Forms.Button
        Me.txtStatistics = New System.Windows.Forms.TextBox
        Me.grpScroll.SuspendLayout()
        Me.grpGraphicDisplay.SuspendLayout()
        Me.grpStatistics.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(20, 24)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(184, 24)
        Me.btnClear.TabIndex = 0
        Me.btnClear.Text = "Clear"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(356, 324)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 28)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnSpecifyPosition
        '
        Me.btnSpecifyPosition.Location = New System.Drawing.Point(20, 61)
        Me.btnSpecifyPosition.Name = "btnSpecifyPosition"
        Me.btnSpecifyPosition.Size = New System.Drawing.Size(184, 24)
        Me.btnSpecifyPosition.TabIndex = 2
        Me.btnSpecifyPosition.Text = "Specify Position and Display"
        '
        'btnBlinkCharacter
        '
        Me.btnBlinkCharacter.Location = New System.Drawing.Point(20, 98)
        Me.btnBlinkCharacter.Name = "btnBlinkCharacter"
        Me.btnBlinkCharacter.Size = New System.Drawing.Size(184, 24)
        Me.btnBlinkCharacter.TabIndex = 3
        Me.btnBlinkCharacter.Text = "Blink Characters"
        '
        'btnTeletypeCharacters
        '
        Me.btnTeletypeCharacters.Location = New System.Drawing.Point(20, 135)
        Me.btnTeletypeCharacters.Name = "btnTeletypeCharacters"
        Me.btnTeletypeCharacters.Size = New System.Drawing.Size(184, 24)
        Me.btnTeletypeCharacters.TabIndex = 4
        Me.btnTeletypeCharacters.Text = "Teletype Characters"
        '
        'grpScroll
        '
        Me.grpScroll.Controls.Add(Me.btnRight)
        Me.grpScroll.Controls.Add(Me.btnLeft)
        Me.grpScroll.Location = New System.Drawing.Point(224, 16)
        Me.grpScroll.Name = "grpScroll"
        Me.grpScroll.Size = New System.Drawing.Size(224, 68)
        Me.grpScroll.TabIndex = 5
        Me.grpScroll.TabStop = False
        Me.grpScroll.Text = "Scroll"
        '
        'btnRight
        '
        Me.btnRight.Location = New System.Drawing.Point(136, 32)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(68, 24)
        Me.btnRight.TabIndex = 1
        Me.btnRight.Text = "Right"
        '
        'btnLeft
        '
        Me.btnLeft.Location = New System.Drawing.Point(20, 32)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(68, 24)
        Me.btnLeft.TabIndex = 0
        Me.btnLeft.Text = "Left"
        '
        'btnWindowControl
        '
        Me.btnWindowControl.Location = New System.Drawing.Point(20, 172)
        Me.btnWindowControl.Name = "btnWindowControl"
        Me.btnWindowControl.Size = New System.Drawing.Size(184, 24)
        Me.btnWindowControl.TabIndex = 6
        Me.btnWindowControl.Text = "Windows Control"
        '
        'grpGraphicDisplay
        '
        Me.grpGraphicDisplay.Controls.Add(Me.btnDisplayBitmap)
        Me.grpGraphicDisplay.Controls.Add(Me.btnSetChracterOrnaments)
        Me.grpGraphicDisplay.Location = New System.Drawing.Point(224, 88)
        Me.grpGraphicDisplay.Name = "grpGraphicDisplay"
        Me.grpGraphicDisplay.Size = New System.Drawing.Size(224, 108)
        Me.grpGraphicDisplay.TabIndex = 7
        Me.grpGraphicDisplay.TabStop = False
        Me.grpGraphicDisplay.Text = "GraphicDisplay only"
        '
        'btnDisplayBitmap
        '
        Me.btnDisplayBitmap.Location = New System.Drawing.Point(20, 68)
        Me.btnDisplayBitmap.Name = "btnDisplayBitmap"
        Me.btnDisplayBitmap.Size = New System.Drawing.Size(184, 24)
        Me.btnDisplayBitmap.TabIndex = 8
        Me.btnDisplayBitmap.Text = "DisplayBitmap"
        '
        'btnSetChracterOrnaments
        '
        Me.btnSetChracterOrnaments.Location = New System.Drawing.Point(20, 28)
        Me.btnSetChracterOrnaments.Name = "btnSetChracterOrnaments"
        Me.btnSetChracterOrnaments.Size = New System.Drawing.Size(184, 24)
        Me.btnSetChracterOrnaments.TabIndex = 7
        Me.btnSetChracterOrnaments.Text = "Set Character Ornaments"
        '
        'grpStatistics
        '
        Me.grpStatistics.Controls.Add(Me.btnRetrieveStatistics)
        Me.grpStatistics.Controls.Add(Me.txtStatistics)
        Me.grpStatistics.Location = New System.Drawing.Point(224, 204)
        Me.grpStatistics.Name = "grpStatistics"
        Me.grpStatistics.Size = New System.Drawing.Size(224, 108)
        Me.grpStatistics.TabIndex = 8
        Me.grpStatistics.TabStop = False
        Me.grpStatistics.Text = "Device Statistics"
        '
        'btnRetrieveStatistics
        '
        Me.btnRetrieveStatistics.Location = New System.Drawing.Point(20, 64)
        Me.btnRetrieveStatistics.Name = "btnRetrieveStatistics"
        Me.btnRetrieveStatistics.Size = New System.Drawing.Size(184, 24)
        Me.btnRetrieveStatistics.TabIndex = 9
        Me.btnRetrieveStatistics.Text = "Retrieve Statistics"
        '
        'txtStatistics
        '
        Me.txtStatistics.Location = New System.Drawing.Point(20, 28)
        Me.txtStatistics.Name = "txtStatistics"
        Me.txtStatistics.Size = New System.Drawing.Size(180, 19)
        Me.txtStatistics.TabIndex = 0
        Me.txtStatistics.Text = "ModelName,HoursPoweredCount,OnlineTransitionCount"
        '
        'FrameStep11
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(468, 369)
        Me.Controls.Add(Me.grpStatistics)
        Me.Controls.Add(Me.grpGraphicDisplay)
        Me.Controls.Add(Me.btnWindowControl)
        Me.Controls.Add(Me.grpScroll)
        Me.Controls.Add(Me.btnTeletypeCharacters)
        Me.Controls.Add(Me.btnBlinkCharacter)
        Me.Controls.Add(Me.btnSpecifyPosition)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.MaximizeBox = False
        Me.Name = "FrameStep11"
        Me.Text = "Step 11 Obtains the statistics of the device."
        Me.grpScroll.ResumeLayout(False)
        Me.grpGraphicDisplay.ResumeLayout(False)
        Me.grpStatistics.ResumeLayout(False)
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
    ''' When the button "blinkCharactersn" is pushed,
    ''' the method "displayText" is run.
    ''' "Blink" is run in the case shown below.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnBlinkCharacter_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnBlinkCharacter.Click

        '<<<step3>>>--Start
        Try

            m_Display.DisplayText("Blink", DisplayTextMode.Blink)

        Catch ex As Exception

        End Try
        '<<<step3>>>--End

    End Sub
    ''' <summary>
    ''' When the button "teletypeCharacters" is pushed,
    ''' the method "displayText" is run.
    ''' in the case shown below.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnTeletypeCharacters_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnTeletypeCharacters.Click

        '<<<step4>>>--Start
        Try
            'wait 1.0 second for the next character displaied.
            m_Display.InterCharacterWait = 1000
            m_Display.DisplayText("Teletype", DisplayTextMode.Normal)

        Catch ex As Exception

        End Try
        '<<<step4>>>--End

    End Sub
    ''' <summary>
    ''' When the button "btnTeletypeCharacters" is leave forcus,
    ''' "InterCharacterWait" reset.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnTeletypeCharacters_Leave(ByVal sender As Object _
    , ByVal e As System.EventArgs) Handles btnTeletypeCharacters.Leave

        '<<<step4>>>--Start
        Try
            m_Display.InterCharacterWait = 0
        Catch ex As PosControlException

        End Try
        '<<<step4>>>--End

    End Sub
    ''' <summary>
    ''' When the button "left" is pushed,
    ''' a method "scrollText" is run.
    ''' A first paramete orders the scrolling in the left direction.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnLeft_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnLeft.Click

        '<<<step5>>>--Start
        Try

            'scrollText(int direction, int units)
            'Move the character to the left side for a column.
            m_Display.ScrollText(DisplayScrollText.Left, 1)

        Catch ex As PosControlException

        End Try
        '<<<step5>>>--End

    End Sub
    ''' <summary>
    ''' When the button "right" is pushed,
    ''' a method "scrollText" is run.
    ''' A first paramete orders the scrolling in the right direction.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRight_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnRight.Click

        '<<<step5>>>--Start
        Try

            'scrollText(int direction, int units)
            'Move the character to the right side for a column.
            m_Display.ScrollText(DisplayScrollText.Right, 1)

        Catch ex As PosControlException

        End Try
        '<<<step5>>>--End

    End Sub
    ''' <summary>
    ''' When the button "windowsControl" is pushed,
    ''' a method is run, and a property is set.
    ''' These codes realize "Marquee".
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWindowControl_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnWindowControl.Click

        '<<<step6>>>--Start
        Try
            'createWindow(int viewportRow, int viewportColumn, int viewportHeight,
            'int viewportWidth, int windowHeight, int windowWidth);
            m_Display.CreateWindow(1, 10, 1, 10, 1, 34)

            'When "MarqueeFormat" is "DisplayMarqueeFormat.Walk",
            'put a character on the display one by one from the reverse of the side that
            'you selected as The direction of "Marquee".
            'The direction of "Marquee" is that you selected as "MarqueeType".
            m_Display.MarqueeFormat = DisplayMarqueeFormat.Walk

            'When the "MarqueeType" is "DisplayMarqueeType.Init",
            '  The change of the setting from "DisplayMarqueeType.Init" permits that the setting of "String data" and
            '"MarqueeFormat" becomes effective.
            m_Display.MarqueeType = DisplayMarqueeType.Init

            'It takes 0.1 second that the next moving satarts
            '  after the end of the one moving of unit.
            m_Display.MarqueeUnitWait = 100

            m_Display.DisplayText("Sale! 50%-20% OFF!", DisplayTextMode.Normal)

            'For set the direction as "MarqueeType". For example, the left and the right, the dawn and so on.
            'disp.setMarqueeType(LineDisplayConst.DISP_MT_LEFT);
            m_Display.MarqueeType = DisplayMarqueeType.Left

            MessageBox.Show("When pressing OK, it ends.", m_strStep _
            , MessageBoxButtons.OK)

            m_Display.MarqueeType = DisplayMarqueeType.None
            m_Display.DestroyWindow()

        Catch ex As PosControlException

        End Try
        '<<<step6>>>--End

    End Sub
    ''' <summary>
    ''' When the button "setcaracterOrnaments" is pushed,
    ''' a method "displayTextAt" is run.
    ''' These codes realize that the various kinds of
    ''' characters is displayed by using "ESC".
    '''  "ESC" means Escape sequence code.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSetChracterOrnaments_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnSetChracterOrnaments.Click

        '<<<step8>>>--Start
        Dim ESC As String
        'ESC command
        ESC = Chr(&H1B)

        Try
            'For clear the text on the window be in current use.
            m_Display.ClearText()

            'It prints by "Bold" using an escape sequence code.
            m_Display.DisplayTextAt(0, 0, "Normal" + ESC + "|bCBold", DisplayTextMode.Normal)

            '"\u001b|rvC" is the command which reverses a character. 
            m_Display.DisplayTextAt(1, 0, ESC + "|rvCReverse" + ESC + "|bCBold&Reverse" _
            , DisplayTextMode.Normal)

            m_Display.DisplayText(ESC + "|bCBold", DisplayTextMode.Normal)

        Catch ex As PosControlException

        End Try
        '<<<step8>>>--End

    End Sub
    ''' <summary>
    ''' When the button "displayBitmap" is pushed,
    ''' a method "directIO" is run.
    ''' These codes realize that a bitmap is displayed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnDisplayBitmap_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnDisplayBitmap.Click

        '<<<step9>>>--Start
        Dim strCurDir As String
        Dim strFilePath As String
        Dim iData As Integer = 0
        Dim abyte() As Byte = {0}

        'Current Directory Path
        strCurDir = Directory.GetCurrentDirectory()
        strFilePath = strCurDir.Substring(0, strCurDir.LastIndexOf("bin"))

        strFilePath += "EPSON.BMP"

        Try
            'The command "DISP_DI_GRAPHIC".
            m_Display.DirectIO(EpsonLineDisplayConst.DISP_DI_GRAPHIC, iData, abyte)

            m_Display.CreateWindow(0, 0, 64, 256, 64, 256)

            m_Display.DisplayBitmap(strFilePath, 64, LineDisplay.DisplayBitmapLeft _
            , LineDisplay.DisplayBitmapTop)

            MessageBox.Show("When pressing OK, delete the window." _
            , m_strStep, MessageBoxButtons.OK)

            m_Display.DestroyWindow()

        Catch ex As PosControlException

        End Try
        '<<<step9>>>--End

    End Sub
    ''' <summary>
    '''  A method "RetrieveStatistics" calls RetrieveStatistics method.
    '''  This method obtains the statistics of device
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRetrieveStatistics_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnRetrieveStatistics.Click

        '<<<step11>>>--Start
        Dim astrStatistics As String() = Nothing
        Dim strReturn As String = ""
        Dim strSaveXmlPath As String = ""

        'Current Directory Path
        Dim strCurDir As String = Directory.GetCurrentDirectory()

        strSaveXmlPath += strCurDir + "\\" + "Sample.xml"

        astrStatistics = txtStatistics.Text.Split(",")

        Try
            strReturn = m_Display.RetrieveStatistics(astrStatistics)

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
        '<<<step11>>>--End
    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become valid or invalid based on bFlag.
    ''' </summary>
    Private Sub ChangeButtonStatus(ByVal bFlag As Boolean)

        btnClear.Enabled = bFlag
        btnSpecifyPosition.Enabled = bFlag
        btnBlinkCharacter.Enabled = bFlag
        btnTeletypeCharacters.Enabled = bFlag
        btnLeft.Enabled = bFlag
        btnRight.Enabled = bFlag
        btnWindowControl.Enabled = bFlag
        btnRetrieveStatistics.Enabled = bFlag
        txtStatistics.Enabled = bFlag

        '<<<step8>>>--Start
        'Supports only for the Graphic Display(EPSON DM-D500 series)
        If m_Display.DeviceName.IndexOf("DM-D5") >= 0 Then

            btnSetChracterOrnaments.Enabled = bFlag
            btnDisplayBitmap.Enabled = bFlag
        Else
            btnSetChracterOrnaments.Enabled = False
            btnDisplayBitmap.Enabled = False
        End If
        '<<<step8>>>--End

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
    Private Sub FrameStep11_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String
        Dim deviceInfo As DeviceInfo
        Dim posExplorer As PosExplorer

        strLogicalName = "LineDisplay"

        'Create PosExplorer
        posExplorer = New PosExplorer

        '<<<step7>>>--Start
        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.LineDisplay, strLogicalName)

        Catch ex As Exception

            MessageBox.Show("Fails to get device information.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus(False)
            Return

        End Try

        Try
            m_Display = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception

            'Fails CreateInstance
            MessageBox.Show("Fails to create instance", m_strStep _
             , MessageBoxButtons.OK)

            'Disable button
            ChangeButtonStatus(False)
            Return
        End Try

        'Register EventHandler
        AddStatusUpdateEvent(m_Display)

        Try

            'Open the device
            m_Display.Open()

        Catch ex As PosControlException

            MessageBox.Show("This device has not been registered, or cannot use." _
            , m_strStep, MessageBoxButtons.OK)

            ChangeButtonStatus(False)
            Return
        End Try

        Try

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Display.Claim(1000)

        Catch ex As PosControlException

            MessageBox.Show("Fails to get the exclusive right for the device." _
            , m_strStep, MessageBoxButtons.OK)

            ChangeButtonStatus(False)
            Return

        End Try

        'If support the CapPowerReporting, enable the Power Reporting Requirements.
        If Not m_Display.CapPowerReporting = PowerReporting.None Then

            m_Display.PowerNotify = PowerNotification.Enabled

        End If

        Try
            'Enable the device.
            m_Display.DeviceEnabled = True

        Catch ex As PosControlException

            ChangeButtonStatus(False)

        End Try
        '<<<step7>>>--End
        '<<<step1>>>--End

        '<<<step8>>>--Start
        'Supports only for the Graphic Display(EPSON DM-D500 series)
        If m_Display.DeviceName.IndexOf("DM-D5") < 0 Then

            btnSetChracterOrnaments.Enabled = False
            btnDisplayBitmap.Enabled = False

        End If
        '<<<step8>>>--End

        '<<<step11>>>--Start
        If m_Display.CapStatisticsReporting = False Then

            btnRetrieveStatistics.Enabled = False
            txtStatistics.Enabled = False

        End If
        '<<<step11>>>--End

    End Sub
    ''' <summary>
    ''' When the method "closing" is called,
    ''' the following code is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep11_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step1>>>--Start
        If m_Display Is Nothing Then
            Return
        End If

        Try
            '<<<step7>>>--Start
            'Remove StatusUpdateEventHandler.
            RemoveStatusUpdateEvent(m_Display)
            '<<<step7>>>--End

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
    ''' <summary>
    ''' Add StatusUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddStatusUpdateEvent(ByVal eventSource As Object)

        '<<<step10>>>--Start
        Dim statusUpdateEvent As EventInfo = Nothing

        statusUpdateEvent = eventSource.GetType().GetEvent("StatusUpdateEvent")

        If Not (statusUpdateEvent Is Nothing) Then
            statusUpdateEvent.AddEventHandler(eventSource _
            , New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))

        End If
        '<<<step10>>>--End
    End Sub
    ''' <summary>
    ''' Remove StatusUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveStatusUpdateEvent(ByVal eventSource As Object)

        '<<<step10>>>--Start
        Dim statusupdateEvent As EventInfo = Nothing

        statusupdateEvent = eventSource.GetType().GetEvent("StatusUpdateEvent")

        If Not (statusupdateEvent Is Nothing) Then
            statusupdateEvent.RemoveEventHandler(eventSource _
            , New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))

        End If
        '<<<step10>>>--End
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

        If iPreviousStatus = e.Status Then
            Return
        End If

        iPreviousStatus = e.Status

        '<<<step7>>>--Start
        ''The Power Reporting Requirements fires the event when the device power status is changed.
        Select Case e.Status

            'The device is powered on.
        Case PosCommon.StatusPowerOnline
                MessageBox.Show("The device is powered on.", m_strStep _
                , MessageBoxButtons.OK)
                ChangeButtonStatus(True)
                'The device is powered off, or unconnected.
            Case PosCommon.StatusPowerOff
                MessageBox.Show("The device is powered off", m_strStep _
                , MessageBoxButtons.OK)
                ChangeButtonStatus(False)
                'The device is powered on, but disable to operate.
            Case PosCommon.StatusPowerOffline
                MessageBox.Show("The device is powered on, but disable to operate." _
                , m_strStep, MessageBoxButtons.OK)
                ChangeButtonStatus(False)
                'PosCommon.StatusPowerOffOffline:
            Case PosCommon.StatusPowerOffOffline
                MessageBox.Show("The device is powered off or off-line.", m_strStep _
                 , MessageBoxButtons.OK)
                ChangeButtonStatus(False)
            Case Else

        End Select
        '<<<step7>>>--End

    End Sub

End Class
