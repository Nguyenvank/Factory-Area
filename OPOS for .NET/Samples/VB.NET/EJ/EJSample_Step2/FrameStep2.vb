Imports Microsoft.PointOfService
Imports System.Globalization
Imports System.Windows.Forms
Public Class FrameStep2
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
    Private WithEvents lblEndMarker As System.Windows.Forms.Label
    Private WithEvents lblStartMarker As System.Windows.Forms.Label
    Private WithEvents btnErase As System.Windows.Forms.Button
    Private WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents txtMarker2 As System.Windows.Forms.TextBox
    Private WithEvents txtMarker1 As System.Windows.Forms.TextBox
    Private WithEvents chkStorageEnabled As System.Windows.Forms.CheckBox
    Private WithEvents btnAddMarker As System.Windows.Forms.Button
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents gpBoxEJ As System.Windows.Forms.GroupBox
    Private WithEvents btnPrintContent As System.Windows.Forms.Button
    Private WithEvents txtMarker As System.Windows.Forms.TextBox
    Private WithEvents grpBoxReceiptPrinter As System.Windows.Forms.GroupBox
    Private WithEvents btnPrintReceipt As System.Windows.Forms.Button

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
        Me.lblEndMarker = New System.Windows.Forms.Label
        Me.lblStartMarker = New System.Windows.Forms.Label
        Me.btnErase = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtMarker2 = New System.Windows.Forms.TextBox
        Me.txtMarker1 = New System.Windows.Forms.TextBox
        Me.chkStorageEnabled = New System.Windows.Forms.CheckBox
        Me.btnAddMarker = New System.Windows.Forms.Button
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.gpBoxEJ = New System.Windows.Forms.GroupBox
        Me.btnPrintContent = New System.Windows.Forms.Button
        Me.txtMarker = New System.Windows.Forms.TextBox
        Me.grpBoxReceiptPrinter = New System.Windows.Forms.GroupBox
        Me.btnPrintReceipt = New System.Windows.Forms.Button
        Me.groupBox2.SuspendLayout()
        Me.gpBoxEJ.SuspendLayout()
        Me.grpBoxReceiptPrinter.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblEndMarker
        '
        Me.lblEndMarker.AutoSize = True
        Me.lblEndMarker.Location = New System.Drawing.Point(142, 70)
        Me.lblEndMarker.Name = "lblEndMarker"
        Me.lblEndMarker.Size = New System.Drawing.Size(63, 12)
        Me.lblEndMarker.TabIndex = 8
        Me.lblEndMarker.Text = "End Marker"
        '
        'lblStartMarker
        '
        Me.lblStartMarker.AutoSize = True
        Me.lblStartMarker.Location = New System.Drawing.Point(149, 99)
        Me.lblStartMarker.Name = "lblStartMarker"
        Me.lblStartMarker.Size = New System.Drawing.Size(69, 12)
        Me.lblStartMarker.TabIndex = 7
        Me.lblStartMarker.Text = "Start Marker"
        '
        'btnErase
        '
        Me.btnErase.Location = New System.Drawing.Point(20, 205)
        Me.btnErase.Name = "btnErase"
        Me.btnErase.Size = New System.Drawing.Size(122, 21)
        Me.btnErase.TabIndex = 6
        Me.btnErase.Text = "Erase Medium"
        Me.btnErase.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(210, 332)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(127, 21)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtMarker2
        '
        Me.txtMarker2.Location = New System.Drawing.Point(149, 169)
        Me.txtMarker2.Name = "txtMarker2"
        Me.txtMarker2.Size = New System.Drawing.Size(160, 19)
        Me.txtMarker2.TabIndex = 5
        Me.txtMarker2.Text = "MarkerByStep2"
        '
        'txtMarker1
        '
        Me.txtMarker1.Location = New System.Drawing.Point(149, 115)
        Me.txtMarker1.Name = "txtMarker1"
        Me.txtMarker1.Size = New System.Drawing.Size(160, 19)
        Me.txtMarker1.TabIndex = 4
        Me.txtMarker1.Text = "MarkerByStep1"
        '
        'chkStorageEnabled
        '
        Me.chkStorageEnabled.AutoSize = True
        Me.chkStorageEnabled.Location = New System.Drawing.Point(148, 18)
        Me.chkStorageEnabled.Name = "chkStorageEnabled"
        Me.chkStorageEnabled.Size = New System.Drawing.Size(107, 16)
        Me.chkStorageEnabled.TabIndex = 1
        Me.chkStorageEnabled.Text = "Storage Enabled"
        Me.chkStorageEnabled.UseVisualStyleBackColor = True
        '
        'btnAddMarker
        '
        Me.btnAddMarker.Location = New System.Drawing.Point(20, 50)
        Me.btnAddMarker.Name = "btnAddMarker"
        Me.btnAddMarker.Size = New System.Drawing.Size(122, 21)
        Me.btnAddMarker.TabIndex = 0
        Me.btnAddMarker.Text = "Add Marker"
        Me.btnAddMarker.UseVisualStyleBackColor = True
        '
        'groupBox1
        '
        Me.groupBox1.Location = New System.Drawing.Point(7, 39)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(313, 39)
        Me.groupBox1.TabIndex = 9
        Me.groupBox1.TabStop = False
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.lblEndMarker)
        Me.groupBox2.Location = New System.Drawing.Point(7, 83)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(313, 116)
        Me.groupBox2.TabIndex = 10
        Me.groupBox2.TabStop = False
        '
        'gpBoxEJ
        '
        Me.gpBoxEJ.Controls.Add(Me.lblStartMarker)
        Me.gpBoxEJ.Controls.Add(Me.btnErase)
        Me.gpBoxEJ.Controls.Add(Me.txtMarker2)
        Me.gpBoxEJ.Controls.Add(Me.txtMarker1)
        Me.gpBoxEJ.Controls.Add(Me.btnPrintContent)
        Me.gpBoxEJ.Controls.Add(Me.txtMarker)
        Me.gpBoxEJ.Controls.Add(Me.chkStorageEnabled)
        Me.gpBoxEJ.Controls.Add(Me.btnAddMarker)
        Me.gpBoxEJ.Controls.Add(Me.groupBox1)
        Me.gpBoxEJ.Controls.Add(Me.groupBox2)
        Me.gpBoxEJ.Location = New System.Drawing.Point(11, 90)
        Me.gpBoxEJ.Name = "gpBoxEJ"
        Me.gpBoxEJ.Size = New System.Drawing.Size(326, 236)
        Me.gpBoxEJ.TabIndex = 4
        Me.gpBoxEJ.TabStop = False
        Me.gpBoxEJ.Text = "Electronic Journal"
        '
        'btnPrintContent
        '
        Me.btnPrintContent.Location = New System.Drawing.Point(21, 129)
        Me.btnPrintContent.Name = "btnPrintContent"
        Me.btnPrintContent.Size = New System.Drawing.Size(122, 21)
        Me.btnPrintContent.TabIndex = 3
        Me.btnPrintContent.Text = "Print File"
        Me.btnPrintContent.UseVisualStyleBackColor = True
        '
        'txtMarker
        '
        Me.txtMarker.Location = New System.Drawing.Point(148, 53)
        Me.txtMarker.Name = "txtMarker"
        Me.txtMarker.Size = New System.Drawing.Size(159, 19)
        Me.txtMarker.TabIndex = 2
        Me.txtMarker.Text = "MarkerByStep2"
        '
        'grpBoxReceiptPrinter
        '
        Me.grpBoxReceiptPrinter.Controls.Add(Me.btnPrintReceipt)
        Me.grpBoxReceiptPrinter.Location = New System.Drawing.Point(11, 12)
        Me.grpBoxReceiptPrinter.Name = "grpBoxReceiptPrinter"
        Me.grpBoxReceiptPrinter.Size = New System.Drawing.Size(326, 71)
        Me.grpBoxReceiptPrinter.TabIndex = 3
        Me.grpBoxReceiptPrinter.TabStop = False
        Me.grpBoxReceiptPrinter.Text = "Receipt Printer"
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
        'FrameStep2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 365)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.gpBoxEJ)
        Me.Controls.Add(Me.grpBoxReceiptPrinter)
        Me.Name = "FrameStep2"
        Me.Text = "Step 2 - Printing the data stored in storage medium"
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.gpBoxEJ.ResumeLayout(False)
        Me.gpBoxEJ.PerformLayout()
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
                        MessageBox.Show("Unable to print receipt." & Chr(10) & "" + ex.Message, "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                        m_Printer.ClearOutput()
                        bBuffering = False
                        Exit While
                    End If

                    ' When error occurs, display a message to ask the user whether retry or not.
                    dialogResult = MessageBox.Show("Fails to output to a printer." & Chr(10) & "" & Chr(10) & "Retry?", "EJSample_Step2", MessageBoxButtons.AbortRetryIgnore)

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
            MessageBox.Show("Cannot use a Receipt Stateion.", "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning)

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

            MessageBox.Show("Unable to print receipt." & Chr(10) & "" + ex.Message, "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show("Fails to set StorageEnabled." & Chr(10) & "" + ex.ErrorCode.ToString() + "" & Chr(10) & "" + ex.Message, "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show("Fails to Add Marker." & Chr(10) & "" + ex.ErrorCode.ToString() + "" & Chr(10) & "" + ex.Message, "EJSample_Step2", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub
    ''' <summary>
    ''' Loads the form. Open/Claim/Enable PosPrinter and ElectronicJournal. Sets StorageEnabled to true.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String = "PosPrinter"

        'Create PosExplorer
        Dim posExplorer As New PosExplorer()

        Dim deviceInfo As DeviceInfo = Nothing

        Try
            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to get printer device information.", "EJSample_Step2")
            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try
        Try
            m_Printer = DirectCast(posExplorer.CreateInstance(deviceInfo), PosPrinter)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to create PosPrinter instance.", "EJSample_Step2")
            'Nothing can be used.
            ChangePrintButtonStatus()

            GoTo LoadEJ
        End Try

        Try
            'Open the device
            m_Printer.Open()
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to open the PosPrinter device.", "EJSample_Step2")

            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try

        Try
            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to claim the POSprinter device.", "EJSample_Step2")

            'Nothing can be used.
            ChangePrintButtonStatus()
            GoTo LoadEJ
        End Try

        Try
            'Enable the device.
            m_Printer.DeviceEnabled = True
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable to PosPrinter device.", "EJSample_Step2")
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
            MessageBox.Show("Fails to get Electronic Journal device information.", "EJSample_Step2")
            ChangeEJButtonStatus()
            Return
        End Try
        Try
            m_ElectronicJournal = DirectCast(posExplorer.CreateInstance(deviceInfo), ElectronicJournal)
        Catch generatedExceptionName As Exception
            MessageBox.Show("Fails to create ElectronicJournal instance.", "EJSample_Step2")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            'Open the device
            m_ElectronicJournal.Open()
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to open the ElectronicJournal Device.", "EJSample_Step2")
            ChangeEJButtonStatus()
            Return
        End Try

        Try
            m_ElectronicJournal.Claim(1000)
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to claim the ElectronicJournal device.", "EJSample_Step2")

            ChangeEJButtonStatus()
            Return
        End Try

        Try
            m_ElectronicJournal.DeviceEnabled = True
        Catch generatedExceptionName As PosControlException
            MessageBox.Show("Fails to enable the ElectronicJournal device.", "EJSample_Step2")
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
            MessageBox.Show("Fails to enable storage", "EJSample_Step2")
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
        '<<Step2>>--Start
        btnPrintContent.Enabled = False
        btnErase.Enabled = False
        txtMarker1.Enabled = False
        txtMarker2.Enabled = False
        '<<Step2>>--End
    End Sub
    ''' <summary>
    ''' A method that is called when the form closes. Disable/Releases/Closes PosPrinter and ElectronicJournal.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub FrameStep2_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
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

    '<<Step2>>--Start
    ''' <summary>
    ''' A method that is called to print the content of an ElectronicJournal specified between the start and end markers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPrintContent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintContent.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try
            m_ElectronicJournal.PrintContent(txtMarker1.Text, txtMarker2.Text)
        Catch ex As PosControlException
            MessageBox.Show("Fails to Print Content." & Chr(10) & "" + ex.Message, "EJSample_Step2")
        End Try

        System.Windows.Forms.Cursor.Current = Cursors.[Default]
    End Sub
    ''' <summary>
    ''' A method that is called to Erase the medium. All log image and tag files for this electronic Journal will be erased.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnErase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErase.Click
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to erase medium?" & Chr(10) & "", "EJSample_Step2", MessageBoxButtons.OKCancel)
            If result = System.Windows.Forms.DialogResult.OK Then
                m_ElectronicJournal.EraseMedium()
            End If
        Catch ex As PosControlException
            MessageBox.Show("Fails to Erase Medium." & Chr(10) & "" + ex.Message, "EJSample_Step2")
        End Try
        System.Windows.Forms.Cursor.Current = Cursors.[Default]

        '<<Step2>>--End
    End Sub
End Class
