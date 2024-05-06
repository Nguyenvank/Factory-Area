Imports Microsoft.PointOfService
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Reflection
Public Class FrameStep4
    Inherits System.Windows.Forms.Form

#Region " Windows Forms Designer generated code. "
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private WithEvents btnBrowse As System.Windows.Forms.Button
    Private WithEvents chkStorageEnabled As System.Windows.Forms.CheckBox
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents txtMarker As System.Windows.Forms.TextBox
    Private WithEvents btnAddMarker As System.Windows.Forms.Button
    Private WithEvents lblEndMarker As System.Windows.Forms.Label
    Private WithEvents txtStartMarker As System.Windows.Forms.TextBox
    Private WithEvents lblStartMarker As System.Windows.Forms.Label
    Private WithEvents lblFile As System.Windows.Forms.Label
    Private WithEvents chAsync As System.Windows.Forms.CheckBox
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents txtPrintContent As System.Windows.Forms.TextBox
    Private WithEvents btnResume As System.Windows.Forms.Button
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnSuspend As System.Windows.Forms.Button
    Private WithEvents chDataEvent As System.Windows.Forms.CheckBox
    Private WithEvents btnPrintContent As System.Windows.Forms.Button
    Private WithEvents txtFileName As System.Windows.Forms.TextBox
    Private WithEvents gpBoxEJ As System.Windows.Forms.GroupBox
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents btnQueryContent As System.Windows.Forms.Button
    Private WithEvents txtEndMarker As System.Windows.Forms.TextBox
    Private WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Private WithEvents btnPrintReceipt As System.Windows.Forms.Button
    Private WithEvents grpBoxReceiptPrinter As System.Windows.Forms.GroupBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public Sub New()
        MyBase.New()

        ' The InitializeComponent() call is required for windows Forms designer support.
        InitializeComponent()

        ' TODO: Add counstructor code after the InitializeComponent() call.

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.chkStorageEnabled = New System.Windows.Forms.CheckBox
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.txtMarker = New System.Windows.Forms.TextBox
        Me.btnAddMarker = New System.Windows.Forms.Button
        Me.lblEndMarker = New System.Windows.Forms.Label
        Me.txtStartMarker = New System.Windows.Forms.TextBox
        Me.lblStartMarker = New System.Windows.Forms.Label
        Me.lblFile = New System.Windows.Forms.Label
        Me.chAsync = New System.Windows.Forms.CheckBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtPrintContent = New System.Windows.Forms.TextBox
        Me.btnResume = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSuspend = New System.Windows.Forms.Button
        Me.chDataEvent = New System.Windows.Forms.CheckBox
        Me.btnPrintContent = New System.Windows.Forms.Button
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.gpBoxEJ = New System.Windows.Forms.GroupBox
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.btnQueryContent = New System.Windows.Forms.Button
        Me.txtEndMarker = New System.Windows.Forms.TextBox
        Me.groupBox3 = New System.Windows.Forms.GroupBox
        Me.btnPrintReceipt = New System.Windows.Forms.Button
        Me.grpBoxReceiptPrinter = New System.Windows.Forms.GroupBox
        Me.groupBox1.SuspendLayout()
        Me.gpBoxEJ.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.grpBoxReceiptPrinter.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(202, 51)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(24, 21)
        Me.btnBrowse.TabIndex = 14
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'chkStorageEnabled
        '
        Me.chkStorageEnabled.AutoSize = True
        Me.chkStorageEnabled.Location = New System.Drawing.Point(23, 39)
        Me.chkStorageEnabled.Name = "chkStorageEnabled"
        Me.chkStorageEnabled.Size = New System.Drawing.Size(107, 16)
        Me.chkStorageEnabled.TabIndex = 1
        Me.chkStorageEnabled.Text = "Storage Enabled"
        Me.chkStorageEnabled.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.txtMarker)
        Me.groupBox1.Controls.Add(Me.btnAddMarker)
        Me.groupBox1.Location = New System.Drawing.Point(10, 71)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(244, 91)
        Me.groupBox1.TabIndex = 15
        Me.groupBox1.TabStop = False
        '
        'txtMarker
        '
        Me.txtMarker.Location = New System.Drawing.Point(13, 58)
        Me.txtMarker.Name = "txtMarker"
        Me.txtMarker.Size = New System.Drawing.Size(216, 19)
        Me.txtMarker.TabIndex = 2
        Me.txtMarker.Text = "MarkerByStep4"
        '
        'btnAddMarker
        '
        Me.btnAddMarker.Location = New System.Drawing.Point(60, 18)
        Me.btnAddMarker.Name = "btnAddMarker"
        Me.btnAddMarker.Size = New System.Drawing.Size(136, 21)
        Me.btnAddMarker.TabIndex = 0
        Me.btnAddMarker.Text = "Add Marker"
        Me.btnAddMarker.UseVisualStyleBackColor = True
        '
        'lblEndMarker
        '
        Me.lblEndMarker.AutoSize = True
        Me.lblEndMarker.Location = New System.Drawing.Point(19, 124)
        Me.lblEndMarker.Name = "lblEndMarker"
        Me.lblEndMarker.Size = New System.Drawing.Size(63, 12)
        Me.lblEndMarker.TabIndex = 11
        Me.lblEndMarker.Text = "End Marker"
        '
        'txtStartMarker
        '
        Me.txtStartMarker.Location = New System.Drawing.Point(21, 97)
        Me.txtStartMarker.Name = "txtStartMarker"
        Me.txtStartMarker.Size = New System.Drawing.Size(215, 19)
        Me.txtStartMarker.TabIndex = 7
        Me.txtStartMarker.Text = "MarkerByStep1"
        '
        'lblStartMarker
        '
        Me.lblStartMarker.AutoSize = True
        Me.lblStartMarker.Location = New System.Drawing.Point(19, 84)
        Me.lblStartMarker.Name = "lblStartMarker"
        Me.lblStartMarker.Size = New System.Drawing.Size(69, 12)
        Me.lblStartMarker.TabIndex = 10
        Me.lblStartMarker.Text = "Start Marker"
        '
        'lblFile
        '
        Me.lblFile.AutoSize = True
        Me.lblFile.Location = New System.Drawing.Point(19, 45)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(57, 12)
        Me.lblFile.TabIndex = 9
        Me.lblFile.Text = "File Name"
        '
        'chAsync
        '
        Me.chAsync.AutoSize = True
        Me.chAsync.Location = New System.Drawing.Point(269, 50)
        Me.chAsync.Name = "chAsync"
        Me.chAsync.Size = New System.Drawing.Size(87, 16)
        Me.chAsync.TabIndex = 3
        Me.chAsync.Text = "Async Mode"
        Me.chAsync.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(337, 269)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(136, 21)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "Cancel Print"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtPrintContent
        '
        Me.txtPrintContent.Location = New System.Drawing.Point(13, 53)
        Me.txtPrintContent.Name = "txtPrintContent"
        Me.txtPrintContent.Size = New System.Drawing.Size(183, 19)
        Me.txtPrintContent.TabIndex = 13
        Me.txtPrintContent.Text = "ElectronicJournalFile1.bin"
        '
        'btnResume
        '
        Me.btnResume.Enabled = False
        Me.btnResume.Location = New System.Drawing.Point(195, 269)
        Me.btnResume.Name = "btnResume"
        Me.btnResume.Size = New System.Drawing.Size(136, 21)
        Me.btnResume.TabIndex = 18
        Me.btnResume.Text = "Resume Print"
        Me.btnResume.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(415, 383)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(127, 21)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSuspend
        '
        Me.btnSuspend.Enabled = False
        Me.btnSuspend.Location = New System.Drawing.Point(53, 269)
        Me.btnSuspend.Name = "btnSuspend"
        Me.btnSuspend.Size = New System.Drawing.Size(136, 21)
        Me.btnSuspend.TabIndex = 15
        Me.btnSuspend.Text = "Suspend Print"
        Me.btnSuspend.UseVisualStyleBackColor = True
        '
        'chDataEvent
        '
        Me.chDataEvent.AutoSize = True
        Me.chDataEvent.Location = New System.Drawing.Point(269, 29)
        Me.chDataEvent.Name = "chDataEvent"
        Me.chDataEvent.Size = New System.Drawing.Size(121, 16)
        Me.chDataEvent.TabIndex = 4
        Me.chDataEvent.Text = "DataEvent Enabled"
        Me.chDataEvent.UseVisualStyleBackColor = True
        '
        'btnPrintContent
        '
        Me.btnPrintContent.Location = New System.Drawing.Point(60, 18)
        Me.btnPrintContent.Name = "btnPrintContent"
        Me.btnPrintContent.Size = New System.Drawing.Size(136, 21)
        Me.btnPrintContent.TabIndex = 12
        Me.btnPrintContent.Text = "Print File"
        Me.btnPrintContent.UseVisualStyleBackColor = True
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(21, 58)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(215, 19)
        Me.txtFileName.TabIndex = 6
        Me.txtFileName.Text = "ElectronicJournalFile1.bin"
        '
        'gpBoxEJ
        '
        Me.gpBoxEJ.Controls.Add(Me.btnCancel)
        Me.gpBoxEJ.Controls.Add(Me.btnResume)
        Me.gpBoxEJ.Controls.Add(Me.btnSuspend)
        Me.gpBoxEJ.Controls.Add(Me.chDataEvent)
        Me.gpBoxEJ.Controls.Add(Me.chAsync)
        Me.gpBoxEJ.Controls.Add(Me.chkStorageEnabled)
        Me.gpBoxEJ.Controls.Add(Me.groupBox1)
        Me.gpBoxEJ.Controls.Add(Me.groupBox2)
        Me.gpBoxEJ.Controls.Add(Me.groupBox3)
        Me.gpBoxEJ.Location = New System.Drawing.Point(12, 70)
        Me.gpBoxEJ.Name = "gpBoxEJ"
        Me.gpBoxEJ.Size = New System.Drawing.Size(530, 307)
        Me.gpBoxEJ.TabIndex = 4
        Me.gpBoxEJ.TabStop = False
        Me.gpBoxEJ.Text = "Electronic Journal"
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.btnQueryContent)
        Me.groupBox2.Controls.Add(Me.txtEndMarker)
        Me.groupBox2.Controls.Add(Me.txtFileName)
        Me.groupBox2.Controls.Add(Me.lblEndMarker)
        Me.groupBox2.Controls.Add(Me.txtStartMarker)
        Me.groupBox2.Controls.Add(Me.lblStartMarker)
        Me.groupBox2.Controls.Add(Me.lblFile)
        Me.groupBox2.Location = New System.Drawing.Point(260, 71)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(250, 186)
        Me.groupBox2.TabIndex = 16
        Me.groupBox2.TabStop = False
        '
        'btnQueryContent
        '
        Me.btnQueryContent.Location = New System.Drawing.Point(59, 18)
        Me.btnQueryContent.Name = "btnQueryContent"
        Me.btnQueryContent.Size = New System.Drawing.Size(136, 21)
        Me.btnQueryContent.TabIndex = 5
        Me.btnQueryContent.Text = "Query Content"
        Me.btnQueryContent.UseVisualStyleBackColor = True
        '
        'txtEndMarker
        '
        Me.txtEndMarker.Location = New System.Drawing.Point(21, 137)
        Me.txtEndMarker.Name = "txtEndMarker"
        Me.txtEndMarker.Size = New System.Drawing.Size(215, 19)
        Me.txtEndMarker.TabIndex = 8
        Me.txtEndMarker.Text = "MarkerByStep4"
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.btnBrowse)
        Me.groupBox3.Controls.Add(Me.btnPrintContent)
        Me.groupBox3.Controls.Add(Me.txtPrintContent)
        Me.groupBox3.Location = New System.Drawing.Point(10, 168)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(244, 89)
        Me.groupBox3.TabIndex = 17
        Me.groupBox3.TabStop = False
        '
        'btnPrintReceipt
        '
        Me.btnPrintReceipt.Location = New System.Drawing.Point(182, 18)
        Me.btnPrintReceipt.Name = "btnPrintReceipt"
        Me.btnPrintReceipt.Size = New System.Drawing.Size(149, 21)
        Me.btnPrintReceipt.TabIndex = 0
        Me.btnPrintReceipt.Text = "Print Receipt"
        Me.btnPrintReceipt.UseVisualStyleBackColor = True
        '
        'grpBoxReceiptPrinter
        '
        Me.grpBoxReceiptPrinter.Controls.Add(Me.btnPrintReceipt)
        Me.grpBoxReceiptPrinter.Location = New System.Drawing.Point(12, 12)
        Me.grpBoxReceiptPrinter.Name = "grpBoxReceiptPrinter"
        Me.grpBoxReceiptPrinter.Size = New System.Drawing.Size(530, 53)
        Me.grpBoxReceiptPrinter.TabIndex = 3
        Me.grpBoxReceiptPrinter.TabStop = False
        Me.grpBoxReceiptPrinter.Text = "Receipt Printer"
        '
        'FrameStep4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 431)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gpBoxEJ)
        Me.Controls.Add(Me.grpBoxReceiptPrinter)
        Me.Name = "FrameStep4"
        Me.Text = "Step 4 - Using Suspend Mode"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.gpBoxEJ.ResumeLayout(False)
        Me.gpBoxEJ.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.grpBoxReceiptPrinter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    ''' PosPrinter Service Object
    ''' </summary>
    Private m_Printer As PosPrinter = Nothing

    ''' <summary>
    ''' ElectronicJournal Service Object
    ''' </summary>
    Private m_ElectronicJournal As ElectronicJournal = Nothing

    ''' <summary>
    ''' Paper Near End
    ''' </summary>
    Private m_bPaperNearEnd As Boolean = False


    ''' <summary>
    '''  The method "Print Receipt" calls some other methods.
    '''  This includes methods for starting and ending the "rotation print mode", and for printing.
    ''' This method is very similar to Step 8 in the PosPrinter Sample Programs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPrintReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReceipt.Click
        'Initialization
        Dim nowDate As DateTime = DateTime.Now
        'System date
        Dim dateFormat As New DateTimeFormatInfo()
        'Date Format
        dateFormat.MonthDayPattern = "MMMM"
        Dim strDate As String = nowDate.ToString("MMMM,dd,yyyy  HH:mm", dateFormat)
        Dim iRecLineSpacing As Integer
        Dim iRecLineHeight As Integer
        Dim dialogResult As DialogResult
        Dim bBuffering As Boolean = True

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        iRecLineSpacing = m_Printer.RecLineSpacing
        iRecLineHeight = m_Printer.RecLineHeight

        If m_Printer.CapRecPresent = True Then
            While True

                Try

                    m_Printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Transaction)

                    m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Left90)

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|4C" + "" & Chr(27) & "|bC" + "   Receipt     ")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|3C" + "" & Chr(27) & "|2uC" + "       Mr. Brawn" & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|2uC" + "                                                  " & Chr(10) & "" & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|2uC" + "" & Chr(27) & "|3C" + "        Total payment              $" + "" & Chr(27) & "|4C" + "21.00  " & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|1C" & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, strDate + " Received" & Chr(10) & "" & Chr(10) & "")

                    m_Printer.RecLineHeight = 24
                    m_Printer.RecLineSpacing = m_Printer.RecLineHeight

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|uC" + " Details               " & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|1C" + "                          " + "" & Chr(27) & "|2C" + "OPOS Store" & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|uC" + " Tax excluded    $20.00" & Chr(10) & "")

                    m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|1C" + "                          " + "" & Chr(27) & "|bC" + "Zip code 999-9999" & Chr(10) & "")

                    'm_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|uC"
                    '   + " Tax(5%)        $1.00" + "\u001b|N" + "    Phone#(9999)99-9998\n");

                    Exit While

                Catch ex As PosControlException
                    If ex.ErrorCode.ToString() = ErrorCode.Illegal AndAlso ex.ErrorCodeExtended = 1004 Then
                        MessageBox.Show("Unable to print receipt." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                        m_Printer.ClearOutput()
                        bBuffering = False
                        Exit While
                    End If

                    ' When error occurs, display a message to ask the user whether retry or not.
                    dialogResult = MessageBox.Show("Fails to output to a printer." & Chr(10) & "" & Chr(10) & "Retry?", "EJSample_Step4", MessageBoxButtons.AbortRetryIgnore)

                    Try
                        ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                        m_Printer.ClearOutput()
                    Catch generatedExceptionName As PosControlException
                    End Try

                    If dialogResult = dialogResult.Abort OrElse dialogResult = dialogResult.Ignore Then
                        Exit While
                    End If

                    Continue While

                End Try
            End While
        Else
            MessageBox.Show("Cannot use a Receipt Stateion.", "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return
        End If

        m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Normal)

        If bBuffering = True Then
            m_Printer.PrintNormal(PrinterStation.Receipt, "" & Chr(27) & "|fP")
        End If

        While m_Printer.State <> ControlState.Idle
            Try
                System.Threading.Thread.Sleep(100)
            Catch generatedExceptionName As Exception
            End Try
        End While

        Try

            m_Printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal)
        Catch ex As PosControlException
            ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
            m_Printer.ClearOutput()

            MessageBox.Show("Unable to print receipt." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        System.Windows.Forms.Cursor.Current = Cursors.[Default]
        m_Printer.RecLineSpacing = iRecLineSpacing
        m_Printer.RecLineHeight = iRecLineHeight

    End Sub

    ''' <summary>
    ''' A method that sets the StorageEnabled property of ElectronicJournal.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub chkStorageEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStorageEnabled.CheckedChanged
        Try
            m_ElectronicJournal.StorageEnabled = chkStorageEnabled.Checked
        Catch ex As PosControlException
            MessageBox.Show("Fails to set StorageEnabled." & Chr(10) & "" + ex.ErrorCode.ToString() + "" & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ''' <summary>
    ''' A method that Adds a Marker to ElectronicJournal. The Marker text is as given in the text box.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAddMarker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMarker.Click
        Try
            m_ElectronicJournal.AddMarker(txtMarker.Text)
        Catch ex As PosControlException
            MessageBox.Show("Fails to Add Marker." & Chr(10) & "" + ex.ErrorCode.ToString() + "" & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub
    ''' <summary>
    ''' Loads the form. Open/Claim/Enable PosPrinter and ElectronicJournal. Sets StorageEnabled to true.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String = "PosPrinter"

        'Create PosExplorer
        Dim posExplorer As New PosExplorer()

        Dim deviceInfo As DeviceInfo = Nothing

        Try
            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to get printer device information.", "EJSample_Step4")
            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try
        Try
            m_Printer = DirectCast(posExplorer.CreateInstance(deviceInfo), PosPrinter)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to create PosPrinter instance.", "EJSample_Step4")
            'Nothing can be used.
            ChangePrintButtonStatus()

            GoTo LoadEJ
        End Try

        Try
            'Open the device
            m_Printer.Open()
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to open the PosPrinter device.", "EJSample_Step4")

            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try

        Try
            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to claim the POSprinter device.", "EJSample_Step4")

            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try

        Try
            'Enable the device.
            m_Printer.DeviceEnabled = True
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable to PosPrinter device.", "EJSample_Step4")
            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try
LoadEJ:

        'Initialize EJ
        Dim strEJLogicalName As String = "ElectronicJournal"

        Try
            deviceInfo = posExplorer.GetDevice(DeviceType.ElectronicJournal, strEJLogicalName)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to get Electronic Journal device information.", "EJSample_Step4")
            ChangeEJButtonStatus()
            Return
        End Try
        Try
            m_ElectronicJournal = DirectCast(posExplorer.CreateInstance(deviceInfo), ElectronicJournal)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to create ElectronicJournal instance.", "EJSample_Step4")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            'Open the device
            m_ElectronicJournal.Open()
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to open the ElectronicJournal Device.", "EJSample_Step4")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            m_ElectronicJournal.Claim(1000)
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to claim the ElectronicJournal device.", "EJSample_Step4")

            ChangeEJButtonStatus()
            Return
        End Try

        Try
            m_ElectronicJournal.DeviceEnabled = True
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable the ElectronicJournal device.", "EJSample_Step4")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            If chkStorageEnabled.Checked Then
                m_ElectronicJournal.StorageEnabled = True
            Else
                chkStorageEnabled.Checked = True
            End If
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable storage", "EJSample_Step4")
            ChangeEJButtonStatus()
            Return
        End Try

        '<<<step3>>>--Start	
        'Register OutputCompleteEvent
        AddOutputCompleteEvent(m_ElectronicJournal)

        'Register DataEvent
        AddDataEvent(m_ElectronicJournal)

        'Register ErrorEvent
        AddErrorEvent(m_ElectronicJournal)
        '<<<step3>>>--End	

        '<<<step4>>>--Start
        AddStatusUpdateEvent(m_ElectronicJournal)
        AddStatusUpdateEvent(m_Printer)
        btnSuspend.Enabled = False
        btnCancel.Enabled = False
        '<<<step4>>>--End	


    End Sub
    ''' <summary>
    ''' A method that disables the "Print Receipt" Button when called.
    ''' </summary>
    Private Sub ChangePrintButtonStatus()
        btnPrintReceipt.Enabled = False
    End Sub

    ''' <summary>
    ''' A method that disables all ElectronicJournal related controls when called.
    ''' </summary>
    Private Sub ChangeEJButtonStatus()
        btnAddMarker.Enabled = False
        txtMarker.Enabled = False
        chkStorageEnabled.Enabled = False
        '<<Step3>>--Start
        btnQueryContent.Enabled = False
        btnPrintContent.Enabled = False
        txtPrintContent.Enabled = False
        txtEndMarker.Enabled = False
        txtStartMarker.Enabled = False
        txtFileName.Enabled = False
        btnBrowse.Enabled = False
        '<<Step3>>--End
        '<<Step4>>--Start
        btnSuspend.Enabled = False
        btnResume.Enabled = False
        btnCancel.Enabled = False
        chAsync.Enabled = False
        chDataEvent.Enabled = False
        '<<Step4>>--End
    End Sub
    ''' <summary>
    ''' A method that is called when the form closes. Disable/Releases/Closes PosPrinter and ElectronicJournal.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FrameStep4_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If m_Printer IsNot Nothing Then
            Try
                'Cancel the device
                m_Printer.DeviceEnabled = False

                'Release the device exclusive control right.
                m_Printer.Release()
            Catch generatedExceptionName As PosControlException
            Finally
                'Finish using the device.
                m_Printer.Close()
            End Try
        End If

        If m_ElectronicJournal IsNot Nothing Then
            Try
                '<<<step3>>>--Start	
                'Unregister OutputCompleteEvent
                RemoveOutputCompleteEvent(m_ElectronicJournal)

                'Unregister DataEvent
                RemoveDataEvent(m_ElectronicJournal)

                'Unregister ErrorEvent
                RemoveErrorEvent(m_ElectronicJournal)
                '<<<step3>>>--End	

                '<<<step4>>>--Start
                'Unregister StatusUpdateEvent
                RemoveStatusUpdateEvent(m_ElectronicJournal)
                RemoveStatusUpdateEvent(m_Printer)
                '<<<step4>>>--End

                'Cancel the device
                m_ElectronicJournal.DeviceEnabled = False

                'Release the device exclusive control right.
                m_ElectronicJournal.Release()
            Catch generatedExceptionName As PosControlException
            Finally
                'Finish using the device.
                m_ElectronicJournal.Close()
            End Try
        End If
    End Sub
    '<<Step3>>--Start
    ''' <summary>
    ''' A method that sets the DataEventEnabled property
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub chDataEvent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chDataEvent.CheckedChanged
        Try
            m_ElectronicJournal.DataEventEnabled = chDataEvent.Checked
        Catch ex As PosControlException
            MessageBox.Show("Fails to change DataEventEnabled." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    ''' <summary>
    ''' A method that sets the Async Mode property
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub chAsync_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chAsync.CheckedChanged
        Try
            m_ElectronicJournal.AsyncMode = chAsync.Checked
        Catch ex As PosControlException
            MessageBox.Show("Fails to change Async Mode." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    ''' <summary>
    ''' A method that executes QueryContent based on the given filename, start and end markers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnQueryContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQueryContent.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try
            m_ElectronicJournal.QueryContent(txtFileName.Text, txtStartMarker.Text, txtEndMarker.Text)
        Catch ex As PosControlException
            MessageBox.Show("Fails to Query Content." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        System.Windows.Forms.Cursor.Current = Cursors.[Default]
    End Sub
    ''' <summary>
    ''' A method that executes print Content file based on the given filename
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPrintContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintContent.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        If m_bPaperNearEnd And chAsync.Checked Then
            btnSuspend.Enabled = True
        End If

        Try
            m_ElectronicJournal.PrintContentFile(txtPrintContent.Text)
        Catch ex As PosControlException
            MessageBox.Show("Fails to Print File." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        System.Windows.Forms.Cursor.Current = Cursors.[Default]
    End Sub
    ''' <summary>
    ''' A method that invokes a dialog box allowing the user to browse for the file for print Content file method.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim fileDialog As New OpenFileDialog()
        Dim result As DialogResult = fileDialog.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            txtPrintContent.Text = fileDialog.FileName
        End If
    End Sub
    ''' <summary>
    ''' Add OutputCompleteEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddOutputCompleteEvent(ByVal eventSource As Object)
        Dim outputCompleteEvent As EventInfo = eventSource.[GetType]().GetEvent("OutputCompleteEvent")
        If outputCompleteEvent IsNot Nothing Then
            outputCompleteEvent.AddEventHandler(eventSource, New OutputCompleteEventHandler(AddressOf OnOutputCompleteEvent))
        End If
    End Sub

    ''' <summary>
    ''' Add DataEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddDataEvent(ByVal eventSource As Object)
        Dim dataEvent As EventInfo = eventSource.[GetType]().GetEvent("DataEvent")
        If dataEvent IsNot Nothing Then
            dataEvent.AddEventHandler(eventSource, New DataEventHandler(AddressOf OnDataEvent))
        End If
    End Sub

    ''' <summary>
    ''' Add ErrorEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddErrorEvent(ByVal eventSource As Object)
        Dim errorEvent As EventInfo = eventSource.[GetType]().GetEvent("ErrorEvent")
        If errorEvent IsNot Nothing Then
            errorEvent.AddEventHandler(eventSource, New DeviceErrorEventHandler(AddressOf OnErrorEvent))
        End If
    End Sub
    '<<<Step4>>>--Start
    ''' <summary>
    ''' Add StateUpdateEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddStatusUpdateEvent(ByVal eventSource As Object)
        Dim statusUpdateEvent As EventInfo = eventSource.[GetType]().GetEvent("StatusUpdateEvent")
        If statusUpdateEvent IsNot Nothing Then
            statusUpdateEvent.AddEventHandler(eventSource, New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))
        End If

    End Sub
    '<<<Step4>>>--End

    ''' <summary>
    ''' Remove OutputCompleteEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveOutputCompleteEvent(ByVal eventSource As Object)
        Dim outputCompleteEvent As EventInfo = eventSource.[GetType]().GetEvent("OutputCompleteEvent")
        If outputCompleteEvent IsNot Nothing Then
            outputCompleteEvent.RemoveEventHandler(eventSource, New OutputCompleteEventHandler(AddressOf OnOutputCompleteEvent))
        End If
    End Sub


    ''' <summary>
    ''' Remove DataEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveDataEvent(ByVal eventSource As Object)
        Dim dataEvent As EventInfo = eventSource.[GetType]().GetEvent("DataEvent")
        If dataEvent IsNot Nothing Then
            dataEvent.RemoveEventHandler(eventSource, New DataEventHandler(AddressOf OnDataEvent))
        End If
    End Sub

    ''' <summary>
    ''' Remove ErrorEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveErrorEvent(ByVal eventSource As Object)
        Dim errorEvent As EventInfo = eventSource.[GetType]().GetEvent("ErrorEvent")
        If errorEvent IsNot Nothing Then
            errorEvent.RemoveEventHandler(eventSource, New DeviceErrorEventHandler(AddressOf OnErrorEvent))
        End If
    End Sub

    '<<<Step4>>>--Start
    ''' <summary>
    ''' Remove StatusUpdateEvent.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveStatusUpdateEvent(ByVal eventSource As Object)
        Dim statusUpdateEvent As EventInfo = eventSource.[GetType]().GetEvent("StatusUpdateEvent")
        If statusUpdateEvent IsNot Nothing Then
            statusUpdateEvent.RemoveEventHandler(eventSource, New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent))
        End If
    End Sub
    '<<<Step4>>>--End

    ''' <summary>
    ''' OutputComplete Event.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnOutputCompleteEvent(ByVal source As Object, ByVal e As OutputCompleteEventArgs)

        If InvokeRequired Then
            Invoke(New OutputCompleteEventHandler(AddressOf OnOutputCompleteEvent), New Object(1) {source, e})
            Return
        End If

        'Notify that EJ process is completed when it is asynchronous.
        MessageBox.Show("Complete Printing Content File: ID = " + e.OutputId.ToString(), "EJSample_Step4")

        If e.OutputId = m_ElectronicJournal.OutputId Then
            btnSuspend.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Data Event.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnDataEvent(ByVal source As Object, ByVal e As DataEventArgs)
        If InvokeRequired Then
            Invoke(New DataEventHandler(AddressOf OnDataEvent), New Object(1) {source, e})
            Return
        End If
        'Notify that EJ process is completed when it is asynchronous.
        MessageBox.Show("Complete Querying Content: Status = " + e.Status.ToString(), "EJSample_Step4")

        If Not m_ElectronicJournal.DataEventEnabled Then
            chDataEvent.Checked = False
        End If

    End Sub

    ''' <summary>
    ''' Error Event.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnErrorEvent(ByVal source As Object, ByVal e As DeviceErrorEventArgs)
        If InvokeRequired Then
            'Ensure calls to Windows Form Controls are from this application's thread
            Invoke(New DeviceErrorEventHandler(AddressOf OnErrorEvent), New Object(1) {source, e})
            Return
        End If

        Dim dialogResult As DialogResult
        Dim errorDialog As New ErrorEventDialog()


        Dim strCause As String = ""
        Select Case e.ErrorLocus
            Case ErrorLocus.Output
                strCause = "Output Error."
                errorDialog.btnSuspend.Text = "Suspend Process"
                Exit Select
            Case ErrorLocus.Input
                strCause = "Input Error."
                errorDialog.btnSuspend.Text = "Continue Input"
                Exit Select
            Case ErrorLocus.InputData
                strCause = "Input Data Error"
                errorDialog.btnSuspend.Text = "Continue Input"
                Exit Select
        End Select

        '<<Step4>>---Start
        Dim strMessage As String = "ErrorCode = " + e.ErrorCode.ToString() + "" & Chr(10) & "" + "ErrorCodeExtended = " + e.ErrorCodeExtended.ToString() + "" & Chr(10) & "" & Chr(10) & "" + "What would you like to do?"

        errorDialog.lblErrorMsg.Text = strMessage
        errorDialog.Text = strCause

        dialogResult = errorDialog.ShowDialog()

        If dialogResult = dialogResult.Abort Then
            btnSuspend.Enabled = False
            e.ErrorResponse = ErrorResponse.Clear
        ElseIf dialogResult = dialogResult.Retry Then
            If chAsync.Checked And m_bPaperNearEnd Then
                btnSuspend.Enabled = True
            End If
            e.ErrorResponse = ErrorResponse.Retry
        ElseIf dialogResult = dialogResult.OK Then
            Select Case e.ErrorLocus
                Case ErrorLocus.Output
                    Me.btnSuspend_Click(Me, e)
                    Exit Select
                Case ErrorLocus.InputData
                    e.ErrorResponse = ErrorResponse.ContinueInput
                    Exit Select
            End Select
        End If
        '<<Step4>>---End

    End Sub
    '<<Step3>>--End

    '<<Step4>>--Start
    ''' <summary>
    ''' StatusUpdate Event.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnStatusUpdateEvent(ByVal source As Object, ByVal e As StatusUpdateEventArgs)
        If InvokeRequired Then
            'Ensure calls to Windows Form Controls are from this application's thread
            Invoke(New StatusUpdateEventHandler(AddressOf OnStatusUpdateEvent), New Object(1) {source, e})
            Return
        End If

        Dim strMessage As String = ""
        Select Case e.Status
            Case PosPrinter.StatusReceiptPaperOK
                m_bPaperNearEnd = False
                Exit Select
            Case PosPrinter.StatusReceiptNearEmpty
                m_bPaperNearEnd = True
                strMessage = "Receipt Station Paper Near End"
                MessageBox.Show("State Event: " + strMessage, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Select
            Case ElectronicJournal.StatusMediumNearFull
                strMessage = "ElectronicJournal Medium Near Full"
                MessageBox.Show("State Event: " + strMessage, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Select
            Case ElectronicJournal.StatusMediumSuspended
                btnSuspend.Enabled = False
                btnResume.Enabled = True
                btnCancel.Enabled = True
                Exit Select
        End Select
    End Sub

    ''' <summary>
    ''' A method that suspends the currently running process.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSuspend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSuspend.Click
        Try
            m_ElectronicJournal.SuspendPrintContent()
            btnSuspend.Enabled = False
            btnResume.Enabled = True
            btnCancel.Enabled = True
        Catch ex As PosControlException
            MessageBox.Show("Fails to Suspend Process." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    ''' <summary>
    ''' A method that resumes the suspended process.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResume.Click
        Try
            m_ElectronicJournal.ResumePrintContent()
            btnResume.Enabled = False
            btnCancel.Enabled = False
            btnSuspend.Enabled = True
        Catch ex As PosControlException
            MessageBox.Show("Fails to Resume Process." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    ''' <summary>
    ''' A method that cancels the suspended process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            m_ElectronicJournal.CancelPrintContent()
            btnResume.Enabled = False
            btnCancel.Enabled = False
        Catch ex As PosControlException
            MessageBox.Show("Fails to Cancel Process." & Chr(10) & "" + ex.Message, "EJSample_Step4", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    '<<Step4>>--End
End Class
