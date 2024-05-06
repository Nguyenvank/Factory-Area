Imports Microsoft.PointOfService
Imports System.Reflection
Imports System.Globalization
Imports System.Text


Public Class FrameStep5
    Inherits System.Windows.Forms.Form

    'Micr object
    Private m_Micr As Micr = Nothing

    '<<<step5>>>--Start
    'Printer object
    Private m_Printer As PosPrinter = Nothing
    '<<<step5>>>--End

    'Now Step
    Private m_strStep As String = "MicrSample_Step5"

#Region " Windows Forms Designer generated code.  "

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
    Friend WithEvents lblRawData As System.Windows.Forms.Label
    Friend WithEvents btnInsert As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtRawData As System.Windows.Forms.TextBox
    Friend WithEvents grpCheckData As System.Windows.Forms.GroupBox
    Friend WithEvents lblAccountNumber As System.Windows.Forms.Label
    Friend WithEvents txtAccountNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblBankNumber As System.Windows.Forms.Label
    Friend WithEvents txtBanknumber As System.Windows.Forms.TextBox
    Friend WithEvents lblCheckType As System.Windows.Forms.Label
    Friend WithEvents txtCheckType As System.Windows.Forms.TextBox
    Friend WithEvents lblCountryCode As System.Windows.Forms.Label
    Friend WithEvents txtCountryCode As System.Windows.Forms.TextBox
    Friend WithEvents lblEPC As System.Windows.Forms.Label
    Friend WithEvents txtEPC As System.Windows.Forms.TextBox
    Friend WithEvents lblSerialNumber As System.Windows.Forms.Label
    Friend WithEvents txtSerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblTransitNumber As System.Windows.Forms.Label
    Friend WithEvents txtTransitNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grpCheckData = New System.Windows.Forms.GroupBox
        Me.txtTransitNumber = New System.Windows.Forms.TextBox
        Me.lblTransitNumber = New System.Windows.Forms.Label
        Me.txtSerialNumber = New System.Windows.Forms.TextBox
        Me.lblSerialNumber = New System.Windows.Forms.Label
        Me.txtEPC = New System.Windows.Forms.TextBox
        Me.lblEPC = New System.Windows.Forms.Label
        Me.txtCountryCode = New System.Windows.Forms.TextBox
        Me.lblCountryCode = New System.Windows.Forms.Label
        Me.txtCheckType = New System.Windows.Forms.TextBox
        Me.lblCheckType = New System.Windows.Forms.Label
        Me.txtBanknumber = New System.Windows.Forms.TextBox
        Me.lblBankNumber = New System.Windows.Forms.Label
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.lblAmount = New System.Windows.Forms.Label
        Me.txtAccountNumber = New System.Windows.Forms.TextBox
        Me.lblAccountNumber = New System.Windows.Forms.Label
        Me.txtRawData = New System.Windows.Forms.TextBox
        Me.lblRawData = New System.Windows.Forms.Label
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnInsert = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.grpCheckData.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpCheckData
        '
        Me.grpCheckData.Controls.Add(Me.txtTransitNumber)
        Me.grpCheckData.Controls.Add(Me.lblTransitNumber)
        Me.grpCheckData.Controls.Add(Me.txtSerialNumber)
        Me.grpCheckData.Controls.Add(Me.lblSerialNumber)
        Me.grpCheckData.Controls.Add(Me.txtEPC)
        Me.grpCheckData.Controls.Add(Me.lblEPC)
        Me.grpCheckData.Controls.Add(Me.txtCountryCode)
        Me.grpCheckData.Controls.Add(Me.lblCountryCode)
        Me.grpCheckData.Controls.Add(Me.txtCheckType)
        Me.grpCheckData.Controls.Add(Me.lblCheckType)
        Me.grpCheckData.Controls.Add(Me.txtBanknumber)
        Me.grpCheckData.Controls.Add(Me.lblBankNumber)
        Me.grpCheckData.Controls.Add(Me.txtAmount)
        Me.grpCheckData.Controls.Add(Me.lblAmount)
        Me.grpCheckData.Controls.Add(Me.txtAccountNumber)
        Me.grpCheckData.Controls.Add(Me.lblAccountNumber)
        Me.grpCheckData.Controls.Add(Me.txtRawData)
        Me.grpCheckData.Controls.Add(Me.lblRawData)
        Me.grpCheckData.Location = New System.Drawing.Point(28, 32)
        Me.grpCheckData.Name = "grpCheckData"
        Me.grpCheckData.Size = New System.Drawing.Size(380, 300)
        Me.grpCheckData.TabIndex = 0
        Me.grpCheckData.TabStop = False
        Me.grpCheckData.Text = "The data of check"
        '
        'txtTransitNumber
        '
        Me.txtTransitNumber.Location = New System.Drawing.Point(120, 256)
        Me.txtTransitNumber.Name = "txtTransitNumber"
        Me.txtTransitNumber.Size = New System.Drawing.Size(224, 19)
        Me.txtTransitNumber.TabIndex = 17
        Me.txtTransitNumber.Text = ""
        '
        'lblTransitNumber
        '
        Me.lblTransitNumber.Location = New System.Drawing.Point(12, 256)
        Me.lblTransitNumber.Name = "lblTransitNumber"
        Me.lblTransitNumber.Size = New System.Drawing.Size(92, 20)
        Me.lblTransitNumber.TabIndex = 16
        Me.lblTransitNumber.Text = "TransitNumber"
        '
        'txtSerialNumber
        '
        Me.txtSerialNumber.Location = New System.Drawing.Point(120, 228)
        Me.txtSerialNumber.Name = "txtSerialNumber"
        Me.txtSerialNumber.Size = New System.Drawing.Size(224, 19)
        Me.txtSerialNumber.TabIndex = 15
        Me.txtSerialNumber.Text = ""
        '
        'lblSerialNumber
        '
        Me.lblSerialNumber.Location = New System.Drawing.Point(12, 224)
        Me.lblSerialNumber.Name = "lblSerialNumber"
        Me.lblSerialNumber.Size = New System.Drawing.Size(92, 20)
        Me.lblSerialNumber.TabIndex = 14
        Me.lblSerialNumber.Text = "SerialNumber"
        '
        'txtEPC
        '
        Me.txtEPC.Location = New System.Drawing.Point(120, 196)
        Me.txtEPC.Name = "txtEPC"
        Me.txtEPC.Size = New System.Drawing.Size(224, 19)
        Me.txtEPC.TabIndex = 13
        Me.txtEPC.Text = ""
        '
        'lblEPC
        '
        Me.lblEPC.Location = New System.Drawing.Point(12, 196)
        Me.lblEPC.Name = "lblEPC"
        Me.lblEPC.Size = New System.Drawing.Size(92, 20)
        Me.lblEPC.TabIndex = 12
        Me.lblEPC.Text = "EPC"
        '
        'txtCountryCode
        '
        Me.txtCountryCode.Location = New System.Drawing.Point(120, 168)
        Me.txtCountryCode.Name = "txtCountryCode"
        Me.txtCountryCode.Size = New System.Drawing.Size(224, 19)
        Me.txtCountryCode.TabIndex = 11
        Me.txtCountryCode.Text = ""
        '
        'lblCountryCode
        '
        Me.lblCountryCode.Location = New System.Drawing.Point(12, 168)
        Me.lblCountryCode.Name = "lblCountryCode"
        Me.lblCountryCode.Size = New System.Drawing.Size(92, 20)
        Me.lblCountryCode.TabIndex = 10
        Me.lblCountryCode.Text = "CountryCode"
        '
        'txtCheckType
        '
        Me.txtCheckType.Location = New System.Drawing.Point(120, 140)
        Me.txtCheckType.Name = "txtCheckType"
        Me.txtCheckType.Size = New System.Drawing.Size(224, 19)
        Me.txtCheckType.TabIndex = 9
        Me.txtCheckType.Text = ""
        '
        'lblCheckType
        '
        Me.lblCheckType.Location = New System.Drawing.Point(12, 140)
        Me.lblCheckType.Name = "lblCheckType"
        Me.lblCheckType.Size = New System.Drawing.Size(92, 20)
        Me.lblCheckType.TabIndex = 8
        Me.lblCheckType.Text = "CheckType"
        '
        'txtBanknumber
        '
        Me.txtBanknumber.Location = New System.Drawing.Point(120, 112)
        Me.txtBanknumber.Name = "txtBanknumber"
        Me.txtBanknumber.Size = New System.Drawing.Size(224, 19)
        Me.txtBanknumber.TabIndex = 7
        Me.txtBanknumber.Text = ""
        '
        'lblBankNumber
        '
        Me.lblBankNumber.Location = New System.Drawing.Point(12, 112)
        Me.lblBankNumber.Name = "lblBankNumber"
        Me.lblBankNumber.Size = New System.Drawing.Size(92, 20)
        Me.lblBankNumber.TabIndex = 6
        Me.lblBankNumber.Text = "BankNumber"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(120, 84)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(224, 19)
        Me.txtAmount.TabIndex = 5
        Me.txtAmount.Text = ""
        '
        'lblAmount
        '
        Me.lblAmount.Location = New System.Drawing.Point(12, 84)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(92, 20)
        Me.lblAmount.TabIndex = 4
        Me.lblAmount.Text = "Amount"
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.Location = New System.Drawing.Point(120, 56)
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Size = New System.Drawing.Size(224, 19)
        Me.txtAccountNumber.TabIndex = 3
        Me.txtAccountNumber.Text = ""
        '
        'lblAccountNumber
        '
        Me.lblAccountNumber.Location = New System.Drawing.Point(12, 56)
        Me.lblAccountNumber.Name = "lblAccountNumber"
        Me.lblAccountNumber.Size = New System.Drawing.Size(92, 20)
        Me.lblAccountNumber.TabIndex = 2
        Me.lblAccountNumber.Text = "AccountNumber"
        '
        'txtRawData
        '
        Me.txtRawData.Location = New System.Drawing.Point(120, 28)
        Me.txtRawData.Name = "txtRawData"
        Me.txtRawData.Size = New System.Drawing.Size(224, 19)
        Me.txtRawData.TabIndex = 1
        Me.txtRawData.Text = ""
        '
        'lblRawData
        '
        Me.lblRawData.Location = New System.Drawing.Point(12, 28)
        Me.lblRawData.Name = "lblRawData"
        Me.lblRawData.Size = New System.Drawing.Size(88, 20)
        Me.lblRawData.TabIndex = 0
        Me.lblRawData.Text = "RawData"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(116, 344)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(86, 24)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "Remove"
        '
        'btnInsert
        '
        Me.btnInsert.Location = New System.Drawing.Point(32, 344)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(84, 24)
        Me.btnInsert.TabIndex = 2
        Me.btnInsert.Text = "Insert"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(316, 344)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 24)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Exit"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(204, 344)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(86, 24)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "Print"
        '
        'FrameStep5
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(432, 397)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grpCheckData)
        Me.Controls.Add(Me.btnInsert)
        Me.Controls.Add(Me.btnRemove)
        Me.MaximizeBox = False
        Me.Name = "FrameStep5"
        Me.Text = "Step 5  Adding a function printing on the other side of the slip."
        Me.grpCheckData.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    ''' The code for inserting a check are described.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnInsert_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnInsert.Click

        '<<<step2>>>--Start
        Dim dialogResult As DialogResult

        Do

            Try

                'Insertion preparations of a check are made.
                m_Micr.BeginInsertion(1000)
                Exit Do

            Catch ex As PosControlException

                If ex.ErrorCode = ErrorCode.Timeout Then

                    dialogResult = MessageBox.Show("Please insert a check." _
                    , m_strStep, MessageBoxButtons.YesNo)

                    If dialogResult = dialogResult.No Then

                        Try

                            m_Micr.EndInsertion()
                            m_Micr.BeginRemoval(10000)

                        Catch ex2 As PosControlException

                        End Try

                        Exit Sub

                    End If

                ElseIf ex.ErrorCode = ErrorCode.Illegal And ex.ErrorCodeExtended = jp.co.epson.uposcommon.EpsonUPOSConst.UPOS_EX_INVALID_MODE Then

                    dialogResult = MessageBox.Show("Insert error.", "Insert error.", MessageBoxButtons.OK)

                    Exit Sub

                Else

                    Exit Sub

                End If

            End Try
        Loop

        Try

            'Insertion operation of a check is started.
            m_Micr.EndInsertion()

        Catch ex As PosControlException

        End Try
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' The code for removing a check are described.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRemove_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnRemove.Click

        '<<<step2>>>--Start
        Try

            m_Micr.BeginRemoval(3000)

        Catch ex As PosControlException

            If ex.ErrorCode = ErrorCode.Timeout Then

                MessageBox.Show("Please remove a check.", m_strStep)

            End If

        End Try
        '<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' The code for printing on a Check sheet are described.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPrint_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnPrint.Click

        '<<<step5>>>--Start
        Dim nowDate As DateTime
        Dim dateFormat As DateTimeFormatInfo = New DateTimeFormatInfo
        Dim strPrintData As StringBuilder = New StringBuilder
        Dim strDate As String
        Dim ESC As String

        'ESC command
        ESC = Chr(&H1B)

        nowDate = DateTime.Now              'System date
        dateFormat.MonthDayPattern = "MMMM" '//Date Format
        strDate = nowDate.ToString("MMMM,dd,yyyy  HH:mm", dateFormat)

        strPrintData.Append(ESC + "|rA" + strDate + vbCrLf)
        strPrintData.Append(ESC + "|rA" + "SEIKO EPSON CORPORATION" + vbCrLf)

        Try

            m_Printer.ChangePrintSide(PrinterSide.Side2)

        Catch ex As PosControlException

        End Try

        Try

            m_Printer.PrintNormal(PrinterStation.Slip, strPrintData.ToString())

        Catch ex As PosControlException

        End Try
        '<<<step5>>>--End

    End Sub
    ''' <summary>
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnInsert.Enabled = False
        btnRemove.Enabled = False
        txtRawData.Enabled = False
        txtAccountNumber.Enabled = False
        txtAmount.Enabled = False
        txtBanknumber.Enabled = False
        txtCheckType.Enabled = False
        txtCountryCode.Enabled = False
        txtEPC.Enabled = False
        txtSerialNumber.Enabled = False
        txtTransitNumber.Enabled = False
        btnPrint.Enabled = False

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
    Private Sub FrameStep5_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strMicrLogicalName As String
        Dim strLogicalName As String
        Dim deviceInfo As DeviceInfo

        Dim posExplorer As PosExplorer

        deviceInfo = Nothing

        strMicrLogicalName = "Micr"
        strLogicalName = "PosPrinter"

        'Create PosExplorer
        posExplorer = New PosExplorer

        '<<<step5>>>--Start
        '<<<step4>>>--Start
        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.Micr, strMicrLogicalName)

        Catch ex As Exception

            MessageBox.Show("Fails to get device information.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            m_Micr = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception

            'Fails CreateInstance
            MessageBox.Show("Fails to create instance", m_strStep, MessageBoxButtons.OK)
            ChangeButtonStatus()
            Return

        End Try

        'Register DataEventHandler.
        AddDataEvent(m_Micr)

        '<<<step4>>>--Start
        'Register ErrorEvent
        AddErrorEvent(m_Micr)

        'Register StatusUpdateEvent
        AddStatusUpdateEvent(m_Micr)
        '<<<step4>>>--End

        Try

            'Open the device
            m_Micr.Open()

            'In order to enable it to use the DataEvent.
            m_Micr.DataEventEnabled = True

        Catch ex As PosControlException

            MessageBox.Show("This device has not been registered, or cannot use." _
            , m_strStep, MessageBoxButtons.OK)

            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Micr.Claim(1000)

        Catch ex As PosControlException

            MessageBox.Show("Fails to get the exclusive access for the device." _
            , m_strStep, MessageBoxButtons.OK)

            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

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

            MessageBox.Show("Now the device is disable to use.", m_strStep _
            , MessageBoxButtons.OK)

            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try
        '<<<step4>>>--End

        '<<<step5>>--Start
        deviceInfo = Nothing

        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName)

        Catch ex As Exception

            MessageBox.Show("Fails to get device information.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        '<<<step4>>>--Start
        Try
            m_Printer = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception
            'Fails CreateInstance
            MessageBox.Show("Fails to create instance", m_strStep, MessageBoxButtons.OK)
            ChangeButtonStatus()
            Return
        End Try

        Try

            'Open the device
            m_Printer.Open()

        Catch ex As PosControlException

            MessageBox.Show("This device has not been registered, or cannot use." _
            , m_strStep, MessageBoxButtons.OK)

            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)

        Catch ex As PosControlException

            MessageBox.Show("Fails to get the exclusive access for the device." _
            , m_strStep, MessageBoxButtons.OK)

            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            'Enable the device.
            m_Printer.DeviceEnabled = True

        Catch ex As PosControlException

            MessageBox.Show("Now the device is disable to use.", m_strStep _
            , MessageBoxButtons.OK)

            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        If m_Micr.CapValidationDevice = False Then

            MessageBox.Show("The device does not support for printing both sides. Fails to operate the step." _
            , m_strStep, MessageBoxButtons.OK)

            btnPrint.Enabled = False

        End If

        '<<<step5>>>--End
        '<<<step1>>>--End

    End Sub
    ''' <summary>
    ''' When the method "closing" is called,
    ''' the following code is run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep5_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step1>>>--Start
        If m_Micr Is Nothing Then

            Return

        End If

        Try

            'Remove ErrorEventHandler.
            RemoveDataEvent(m_Micr)

            'Cancel the device
            m_Micr.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Micr.Release()

        Catch ex As Exception

        Finally
            'Finish using the device.
            m_Micr.Close()

        End Try
        '<<<step1>>>--End

        '<<<step5>>>--Start
        If m_Printer Is Nothing Then

            Return

        End If

        Try

            'Cancel the device
            m_Printer.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Printer.Release()

        Catch ex As Exception

        Finally
            'Finish using the device.
            m_Printer.Close()

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
    ''' Add StatusUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddStatusUpdateEvent(ByVal eventSource As Object)

        '<<<step4>>>--Start
        Dim statusUpdateEvent As EventInfo = Nothing

        statusUpdateEvent = eventSource.GetType().GetEvent("StatusUpdateEvent")

        If Not (statusUpdateEvent Is Nothing) Then
            statusUpdateEvent.AddEventHandler(eventSource _
            , New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))

        End If
        '<<<step4>>>--End

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
    ''' Remove StatusUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveStatusUpdateEvent(ByVal eventSource As Object)

        '<<<step4>>>--Start
        Dim statusupdateEvent As EventInfo = Nothing

        statusupdateEvent = eventSource.GetType().GetEvent("StatusUpdateEvent")

        If Not (statusupdateEvent Is Nothing) Then
            statusupdateEvent.RemoveEventHandler(eventSource _
            , New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))

        End If
        '<<<step4>>>--End

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
    '<<<step1>>>--End
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

        '<<<step3>>>--Start
        txtRawData.Text = m_Micr.RawData
        txtAccountNumber.Text = m_Micr.AccountNumber
        txtAmount.Text = m_Micr.Amount
        txtBanknumber.Text = m_Micr.BankNumber
        txtEPC.Text = m_Micr.Epc
        txtSerialNumber.Text = m_Micr.SerialNumber
        txtTransitNumber.Text = m_Micr.TransitNumber

        Select Case m_Micr.CheckType

            Case CheckType.Business
                txtCheckType.Text = "Business"
            Case CheckType.Personal
                txtCheckType.Text = "Personal"
            Case CheckType.Unknown
                txtCheckType.Text = "Unknown"

        End Select

        Select Case m_Micr.CountryCode
            Case CheckCountryCode.Canada
                txtCountryCode.Text = "Canada"
            Case CheckCountryCode.Mexico
                txtCountryCode.Text = "Mexico"
            Case CheckCountryCode.Usa
                txtCountryCode.Text = "Usa"
            Case CheckCountryCode.Unknown
                txtCountryCode.Text = "Unknown"

        End Select

        m_Micr.DataEventEnabled = True
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

        '<<<step4>>>--Start
        MessageBox.Show("Micr Error" + vbCrLf + "ErrorCode =" + e.ErrorCode.ToString() _
        + vbCrLf + "ErrorCodeExtended =" + e.ErrorCodeExtended.ToString(), m_strStep _
         , MessageBoxButtons.OK)
        '<<<step4>>>--End

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

        '<<<step4>>>--Start
        ''The Power Reporting Requirements fires the event when the device power status is changed.

        Select Case e.Status

            'The device is powered on.
            Case PosCommon.StatusPowerOnline
                MessageBox.Show("The device is powered on.", m_strStep, MessageBoxButtons.OK)

                'The device is powered off, or unconnected.
            Case PosCommon.StatusPowerOff
                MessageBox.Show("The device is powered off", m_strStep, MessageBoxButtons.OK)

                'The device is powered on, but disable to operate.
            Case PosCommon.StatusPowerOffline
                MessageBox.Show("The device is powered on, but disable to operate." _
                , m_strStep, MessageBoxButtons.OK)

                'The device is powered off or off-line.
            Case PosCommon.StatusPowerOffOffline
                MessageBox.Show("The device is powered on or off-line." _
                , m_strStep, MessageBoxButtons.OK)
            Case Else

        End Select
        '<<<step4>>>--End

    End Sub

End Class
