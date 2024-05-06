Imports Microsoft.PointOfService
Imports System.Globalization
Imports System.Windows.Forms
Public Class FrameStep1
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
        Me.txtMarker = New System.Windows.Forms.TextBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnPrintReceipt = New System.Windows.Forms.Button
        Me.gpBoxEJ = New System.Windows.Forms.GroupBox
        Me.chkStorageEnabled = New System.Windows.Forms.CheckBox
        Me.btnAddMarker = New System.Windows.Forms.Button
        Me.grpBoxReceiptPrinter = New System.Windows.Forms.GroupBox
        Me.gpBoxEJ.SuspendLayout()
        Me.grpBoxReceiptPrinter.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMarker
        '
        Me.txtMarker.Location = New System.Drawing.Point(89, 77)
        Me.txtMarker.Name = "txtMarker"
        Me.txtMarker.Size = New System.Drawing.Size(149, 19)
        Me.txtMarker.TabIndex = 2
        Me.txtMarker.Text = "MarkerByStep1"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(211, 214)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(127, 21)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnPrintReceipt
        '
        Me.btnPrintReceipt.Location = New System.Drawing.Point(89, 27)
        Me.btnPrintReceipt.Name = "btnPrintReceipt"
        Me.btnPrintReceipt.Size = New System.Drawing.Size(149, 21)
        Me.btnPrintReceipt.TabIndex = 0
        Me.btnPrintReceipt.Text = "Print Receipt"
        Me.btnPrintReceipt.UseVisualStyleBackColor = True
        '
        'gpBoxEJ
        '
        Me.gpBoxEJ.Controls.Add(Me.chkStorageEnabled)
        Me.gpBoxEJ.Controls.Add(Me.txtMarker)
        Me.gpBoxEJ.Controls.Add(Me.btnAddMarker)
        Me.gpBoxEJ.Location = New System.Drawing.Point(12, 90)
        Me.gpBoxEJ.Name = "gpBoxEJ"
        Me.gpBoxEJ.Size = New System.Drawing.Size(326, 116)
        Me.gpBoxEJ.TabIndex = 4
        Me.gpBoxEJ.TabStop = False
        Me.gpBoxEJ.Text = "Electronic Journal"
        '
        'chkStorageEnabled
        '
        Me.chkStorageEnabled.AutoSize = True
        Me.chkStorageEnabled.Location = New System.Drawing.Point(89, 28)
        Me.chkStorageEnabled.Name = "chkStorageEnabled"
        Me.chkStorageEnabled.Size = New System.Drawing.Size(103, 16)
        Me.chkStorageEnabled.TabIndex = 3
        Me.chkStorageEnabled.Text = "StorageEnabled"
        Me.chkStorageEnabled.UseVisualStyleBackColor = True
        '
        'btnAddMarker
        '
        Me.btnAddMarker.Location = New System.Drawing.Point(89, 50)
        Me.btnAddMarker.Name = "btnAddMarker"
        Me.btnAddMarker.Size = New System.Drawing.Size(149, 21)
        Me.btnAddMarker.TabIndex = 0
        Me.btnAddMarker.Text = "Add Marker"
        Me.btnAddMarker.UseVisualStyleBackColor = True
        '
        'grpBoxReceiptPrinter
        '
        Me.grpBoxReceiptPrinter.Controls.Add(Me.btnPrintReceipt)
        Me.grpBoxReceiptPrinter.Location = New System.Drawing.Point(12, 12)
        Me.grpBoxReceiptPrinter.Name = "grpBoxReceiptPrinter"
        Me.grpBoxReceiptPrinter.Size = New System.Drawing.Size(326, 71)
        Me.grpBoxReceiptPrinter.TabIndex = 3
        Me.grpBoxReceiptPrinter.TabStop = False
        Me.grpBoxReceiptPrinter.Text = "Receipt Printer"
        '
        'FrameStep1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 247)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gpBoxEJ)
        Me.Controls.Add(Me.grpBoxReceiptPrinter)
        Me.Name = "FrameStep1"
        Me.Text = "Step 1 - Storing PosPrinter Output"
        Me.gpBoxEJ.ResumeLayout(False)
        Me.gpBoxEJ.PerformLayout()
        Me.grpBoxReceiptPrinter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents txtMarker As System.Windows.Forms.TextBox
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents btnPrintReceipt As System.Windows.Forms.Button
    Private WithEvents gpBoxEJ As System.Windows.Forms.GroupBox
    Private WithEvents btnAddMarker As System.Windows.Forms.Button
    Private WithEvents grpBoxReceiptPrinter As System.Windows.Forms.GroupBox
    Friend WithEvents chkStorageEnabled As System.Windows.Forms.CheckBox

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
                        MessageBox.Show("Unable to print receipt." & Chr(10) & "" + ex.Message, "EJSample_Step1", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                        m_Printer.ClearOutput()
                        bBuffering = False
                        Exit While
                    End If

                    ' When error occurs, display a message to ask the user whether retry or not.
                    dialogResult = MessageBox.Show("Fails to output to a printer." & Chr(10) & "" & Chr(10) & "Retry?", "EJSample_Step1", MessageBoxButtons.AbortRetryIgnore)

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
            MessageBox.Show("Cannot use a Receipt Stateion.", "EJSample_Step1", MessageBoxButtons.OK, MessageBoxIcon.Warning)

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

            MessageBox.Show("Unable to print receipt." & Chr(10) & "" + ex.Message, "EJSample_Step1", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show("Fails to set StorageEnabled." & Chr(10) & "" + ex.ErrorCode.ToString() + "" & Chr(10) & "" + ex.Message, "EJSample_Step1", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show("Fails to Add Marker." & Chr(10) & "" + ex.ErrorCode.ToString() + "" & Chr(10) & "" + ex.Message, "EJSample_Step1", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub
    ''' <summary>
    ''' Loads the form. Open/Claim/Enable PosPrinter and ElectronicJournal. Sets StorageEnabled to true.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String = "PosPrinter"

        'Create PosExplorer
        Dim posExplorer As New PosExplorer()

        Dim deviceInfo As DeviceInfo = Nothing

        Try
            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to get printer device information.", "EJSample_Step1")
            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try
        Try
            m_Printer = DirectCast(posExplorer.CreateInstance(deviceInfo), PosPrinter)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to create PosPrinter instance.", "EJSample_Step1")
            'Nothing can be used.
            ChangePrintButtonStatus()

            GoTo LoadEJ
        End Try

        Try
            'Open the device
            m_Printer.Open()
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to open the PosPrinter device.", "EJSample_Step1")

            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try

        Try
            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to claim the POSprinter device.", "EJSample_Step1")

            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try

        Try
            'Enable the device.
            m_Printer.DeviceEnabled = True
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable to PosPrinter device.", "EJSample_Step1")
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
            MessageBox.Show("Fails to get Electronic Journal device information.", "EJSample_Step1")
            ChangeEJButtonStatus()
            Return
        End Try
        Try
            m_ElectronicJournal = DirectCast(posExplorer.CreateInstance(deviceInfo), ElectronicJournal)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to create ElectronicJournal instance.", "EJSample_Step1")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            'Open the device
            m_ElectronicJournal.Open()
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to open the ElectronicJournal Device.", "EJSample_Step1")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            m_ElectronicJournal.Claim(1000)
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to claim the ElectronicJournal device.", "EJSample_Step1")

            ChangeEJButtonStatus()
            Return
        End Try

        Try
            m_ElectronicJournal.DeviceEnabled = True
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable the ElectronicJournal device.", "EJSample_Step1")
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
            MessageBox.Show("Fails to enable storage", "EJSample_Step1")
            ChangeEJButtonStatus()
            Return
        End Try

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
    End Sub
    ''' <summary>
    ''' A method that is called when the form closes. Disable/Releases/Closes PosPrinter and ElectronicJournal.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FrameStep1_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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


End Class
