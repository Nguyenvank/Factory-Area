Imports Microsoft.PointOfService
Imports System.Globalization
Imports System.IO
Imports System.Reflection

Public Class FrameStep9
    Inherits System.Windows.Forms.Form

    'Printer object.
    Private m_Printer As Microsoft.PointOfService.PosPrinter = Nothing

    'Now Step.
    Private m_strStep As String = "PrinterSample_Step9"

#Region " Windows Forms Designer generated code."

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
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents grpReceipt As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAsync As System.Windows.Forms.Button
    Friend WithEvents btnReceipt As System.Windows.Forms.Button
    Friend WithEvents grpSlp As System.Windows.Forms.GroupBox
    Friend WithEvents btnSales As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnPrint = New System.Windows.Forms.Button
        Me.grpReceipt = New System.Windows.Forms.GroupBox
        Me.btnReceipt = New System.Windows.Forms.Button
        Me.btnAsync = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpSlp = New System.Windows.Forms.GroupBox
        Me.btnSales = New System.Windows.Forms.Button
        Me.grpReceipt.SuspendLayout()
        Me.grpSlp.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(56, 36)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(144, 24)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'grpReceipt
        '
        Me.grpReceipt.Controls.Add(Me.btnReceipt)
        Me.grpReceipt.Controls.Add(Me.btnAsync)
        Me.grpReceipt.Controls.Add(Me.btnPrint)
        Me.grpReceipt.Location = New System.Drawing.Point(40, 24)
        Me.grpReceipt.Name = "grpReceipt"
        Me.grpReceipt.Size = New System.Drawing.Size(248, 144)
        Me.grpReceipt.TabIndex = 1
        Me.grpReceipt.TabStop = False
        Me.grpReceipt.Text = "Receipt"
        '
        'btnReceipt
        '
        Me.btnReceipt.Location = New System.Drawing.Point(56, 104)
        Me.btnReceipt.Name = "btnReceipt"
        Me.btnReceipt.Size = New System.Drawing.Size(144, 24)
        Me.btnReceipt.TabIndex = 2
        Me.btnReceipt.Text = "Print Receipt"
        '
        'btnAsync
        '
        Me.btnAsync.Location = New System.Drawing.Point(56, 70)
        Me.btnAsync.Name = "btnAsync"
        Me.btnAsync.Size = New System.Drawing.Size(144, 24)
        Me.btnAsync.TabIndex = 1
        Me.btnAsync.Text = "Asynchronous printing"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(196, 272)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 28)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'grpSlp
        '
        Me.grpSlp.Controls.Add(Me.btnSales)
        Me.grpSlp.Location = New System.Drawing.Point(40, 184)
        Me.grpSlp.Name = "grpSlp"
        Me.grpSlp.Size = New System.Drawing.Size(248, 72)
        Me.grpSlp.TabIndex = 3
        Me.grpSlp.TabStop = False
        Me.grpSlp.Text = "Slp"
        '
        'btnSales
        '
        Me.btnSales.Location = New System.Drawing.Point(52, 32)
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Size = New System.Drawing.Size(144, 24)
        Me.btnSales.TabIndex = 3
        Me.btnSales.Text = "Print Sales Slp"
        '
        'FrameStep9
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(336, 317)
        Me.Controls.Add(Me.grpSlp)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpReceipt)
        Me.MaximizeBox = False
        Me.Name = "FrameStep9"
        Me.Text = "Step 9 Print on slips."
        Me.grpReceipt.ResumeLayout(False)
        Me.grpSlp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    ''' <summary>
    '''  A method "Print" calls some another method.
    '''  They are method for printing.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPrint_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnPrint.Click

        '<<<step2>>>--Start
        'Initialization

        Dim ESC As String
        Dim dateTime As DateTime = New DateTime
        Dim dateFormat As DateTimeFormatInfo = New DateTimeFormatInfo
        Dim strDate As String
        Dim strbcData As String
        Dim sRecLineChars() As String = {""}
        Dim lRecLineCharsCount As Long

        '<<<step4>>>--Start
        strbcData = "4902720005074"
        '<<<step4>>>--End

        Dim astrItem() As String = {"apples", "grapes", "bananas", "lemons", "oranges"}
        Dim astrPrice() As String = {"10.00", "20.00", "30.00", "40.00", "50.00"}

        'ESC command
        ESC = Chr(&H1B)

        'Get current date
        dateTime = System.DateTime.Now()

        dateFormat.MonthDayPattern = "MMMM"

        strDate = dateTime.ToString("MMMM,dd,yyyy,  HH:mm", dateFormat)

        '<<<step6>>>--Start
        'When outputting to a printer,a mouse cursor becomes like a hourglass.
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        '<<<step6>>>--End

        If m_Printer.CapRecPresent = True Then

            Try

                '<<<step6>>>--Start
                'Batch processing mode
                m_Printer.TransactionPrint(PrinterStation.Receipt _
                 , PrinterTransactionControl.Transaction)

                '<<<step3>>>--Start
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|1B")
                '<<<step3>>>--End

                'Print address
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|N" _
                + "123xxstreet,xxxcity,xxxxstate" + vbCrLf)

                'Print phone number
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|rA" _
                + "TEL 9999-99-9999   C#2" + vbCrLf)

                '<<<step5>>>--Start
                'Make 2mm speces
                'ESC|#uF = Line Feed
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|200uF")
                '<<<step5>>>--End

		        'Change the font size and print the date
	            'ESC|cA = Centering char
	            lRecLineCharsCount = GetRecLineChars(sRecLineChars)
				If lRecLineCharsCount >= 2 Then
					m_Printer.RecLineChars = sRecLineChars(1)
					m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|cA" + strDate + vbCrLf + vbCrLf)
					m_Printer.RecLineChars = sRecLineChars(0)
				Else
					m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|cA" + strDate + vbCrLf + vbCrLf)
				End If

                'Print buying goods
                Dim total As Double = 0.0
                Dim strPrintData As String = ""

                For i As Integer = 0 To astrItem.Length - 1

                    strPrintData = MakePrintString(m_Printer.RecLineChars, astrItem(i), "$" + astrPrice(i))

                    m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + vbCrLf)

                    total += Convert.ToString(astrPrice(i))

                Next

                'Make 2mm speces
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|200uF")

                'Print the total cost
                strPrintData = MakePrintString(m_Printer.RecLineChars, "Tax excluded." _
                , "$" + total.ToString("F"))

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|bC" + strPrintData + vbCrLf)

                strPrintData = MakePrintString(m_Printer.RecLineChars, "Tax 5.0%", "$" _
                + (total * 0.05).ToString("F"))

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|uC" + strPrintData + vbCrLf)

                strPrintData = MakePrintString(m_Printer.RecLineChars / 2, "Total", "$" _
                + (total * 1.05).ToString("F"))

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|2C" + strPrintData + vbCrLf)

                strPrintData = MakePrintString(m_Printer.RecLineChars, "Customer's payment" _
                , "$200.00")

                m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + vbCrLf)

                strPrintData = MakePrintString(m_Printer.RecLineChars, "Change", "$" _
                + (200.0 - (total * 1.05)).ToString("F"))

                m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + vbCrLf)

                'Make 5mm speces
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|500uF")

                '<<<step4>>>--Start
                If m_Printer.CapRecBarCode = True Then

                    'Bacode printing
                    m_Printer.PrintBarCode(PrinterStation.Receipt, strbcData, _
                    BarCodeSymbology.EanJan13, 1000, _
                    m_Printer.RecLineWidth, PosPrinter.PrinterBarCodeLeft, _
                    BarCodeTextPosition.Below)

                End If
                ''<<<step4>>>--End

                ''Feed the receipt to the cutter position automatically, and cut.
                ''ESC|#fP = Line Feed and Paper cut	
                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|fP")

                'print all the buffer data. and exit the batch processing mode.
                m_Printer.TransactionPrint(PrinterStation.Receipt _
                  , PrinterTransactionControl.Normal)
                '<<<step6>>>--End

            Catch ex As PosControlException

            End Try
        End If
        '//<<<step2>>>--End

    End Sub
	Private Function GetRecLineChars(ByRef sRecLineChars() As String) As Long
        Dim lCount As Long
        Dim i As Integer

        'Calculate the element count.
        lCount = m_Printer.RecLineCharsList.GetLength(0)

        If lCount = 0 Then
            GetRecLineChars = 0
        Else
            'Set the element to array.
            ReDim sRecLineChars(lCount)

            For i = 0 To (lCount - 1)
                sRecLineChars(i) = m_Printer.RecLineCharsList(i)
            Next

            GetRecLineChars = lCount
        End If
    End Function
    ''' <summary>
    ''' A method "Asynchronous Printing" calls some another method.
    ''' This includes methods for starting and ending "AsyncMode", and for printing.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnAsync_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnAsync.Click

        '<<<step7>>>--start
        Try

            m_Printer.AsyncMode = True

            btnPrint_Click(sender, e)

            m_Printer.AsyncMode = False

        Catch ex As PosControlException

        End Try
        '<<<step7>>>--end
    End Sub
    ''' <summary>
    ''' An appropriate interval is converted into the length of
    ''' the tab about two texts. And make a printing data.
    ''' </summary>
    ''' <param name="iRecLineChars">
    ''' The width of the territory which it prints on is converted into the number of
    ''' characters, and that value is specified.
    ''' </param>
    ''' <param name="strBuf">
    ''' It is necessary as an information for deciding the interval of the text.
    ''' </param>
    ''' <param name="strPrice">
    ''' It is necessary as an information for deciding the interval of the text, too.
    ''' </param>
    ''' <returns>printing data.
    ''' </returns>
    Private Function MakePrintString(ByVal iRecLineChars As Int32 _
    , ByVal strBuf As String, ByVal strPrice As String) As String

        '<<<step5>>>--Start
        Dim strValue As String
        Dim iSpace As Int32 = 0
        Dim tab As String = ""

        iSpace = iRecLineChars - (strBuf.Length + strPrice.Length)

        For i As Integer = 0 To iSpace - 1
            tab += " "
        Next

        strValue = strBuf + tab + strPrice

        MakePrintString = strValue

        '<<<step5>>>--End
    End Function
    ''' <summary>
    ''' When the method "changeButtonStatus" was called,
    ''' all buttons other than a button "closing" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        'Disable control.
        btnPrint.Enabled = False
        btnAsync.Enabled = False
        btnReceipt.Enabled = False
        btnSales.Enabled = False

    End Sub
    ''' <summary>
    ''' Form close.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(ByVal sender As System.Object, _
    ByVal e As System.EventArgs) Handles btnClose.Click

        '<<<step2>>>--Start
        Close()
        '<<<step2>>>--End

    End Sub

    ''' <summary>
    '''  The method "Print Receipt" calls some other methods.
    '''  This includes methods for starting and ending the "rotation print mode", and for printing.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnReceipt_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnReceipt.Click

        '<<<step8>>>--Start
        'Initialization
        Dim ESC As String
        Dim dateTime As DateTime = New DateTime
        Dim dateFormat As DateTimeFormatInfo = New DateTimeFormatInfo
        Dim strDate As String
        Dim iRecLineSpacing As Integer
        Dim iRecLineHeight As Integer
        Dim bBuffering As Boolean
        bBuffering = True

        'ESC command
        ESC = Chr(&H1B)

        'Get current date
        dateTime = System.DateTime.Now()

        dateFormat.MonthDayPattern = "MMMM"

        strDate = dateTime.ToString("MMMM,dd,yyyy,  HH:mm", dateFormat)

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        iRecLineSpacing = m_Printer.RecLineSpacing
        iRecLineHeight = m_Printer.RecLineHeight

        If m_Printer.CapRecPresent Then

            Try

                m_Printer.TransactionPrint(PrinterStation.Receipt _
               , PrinterTransactionControl.Transaction)

                m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Left90)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|4C" + ESC + "|bC" _
                 + "   Receipt     ")

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|3C" + ESC + "|2uC" _
                 + "       Mr. Brawn" + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|2uC" _
                 + "                                                  " + vbCrLf + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|2uC" + ESC + "|3C" _
                 + "        Total payment              $" + ESC + "|4C" + "21.00  " + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|1C" + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, strDate + " Received" + vbCrLf + vbCrLf)

                m_Printer.RecLineHeight = 24
                m_Printer.RecLineSpacing = m_Printer.RecLineHeight

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|uC" _
                 + " Details               " + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt _
                 , ESC + "|1C" + "                          " + ESC + "|2C" + "OPOS Store" + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|uC" _
                 + " Tax excluded    $20.00" + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|1C" + _
                "                          " + ESC + "|bC" + "Zip code 999-9999" + vbCrLf)

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|uC" _
                 + " Tax(5%)        $1.00" + ESC + "|N" + "    Phone#(9999)99-9998" + vbCrLf)

            Catch ex As PosControlException

                If ex.ErrorCode = ErrorCode.Illegal And ex.ErrorCodeExtended = 1004 Then

                    MessageBox.Show("Unable to print receipt." + vbCrLf _
                     , m_strStep, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                    m_Printer.ClearOutput()
                    bBuffering = False

                End If

            End Try

            Try

                m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Normal)

                If bBuffering = True Then

                    m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|fP")

                End If

                m_Printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal)

            Catch ex As PosControlException

                ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                m_Printer.ClearOutput()

            End Try

        End If

        m_Printer.RecLineSpacing = iRecLineSpacing
        m_Printer.RecLineHeight = iRecLineHeight

        System.Windows.Forms.Cursor.Current = Cursors.Default
        '<<<step8>>>--End

    End Sub
    ''' <summary>
    '''  A method "PrintSalesSlip" calls some another method.
    '''  They are method for printing and for inserting and
    '''  removing the slip paper.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSales_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnSales.Click

        '<<<Step9>>>--Start
        Dim ESC As String
        Dim nowDate As DateTime = DateTime.Now  'System date
        Dim dateFormat As DateTimeFormatInfo = New DateTimeFormatInfo    'Date Format
        dateFormat.MonthDayPattern = "MMMM"
        Dim strDate As String = nowDate.ToString("MMMM,dd,yyyy", dateFormat)
        Dim strRecno As String = "0001"         'Register No
        Dim strName As String = "ABCDEF"          'Casher No.
        Dim strSpace As String = ""
        Dim strPrintData As String
        Dim strTime As String = nowDate.ToString("HH:mm")
        Dim dialogResult As DialogResult

        'ESC command
        ESC = Chr(&H1B)

        Try
            Do

                'Request for Inserting s alip
                dialogResult = MessageBox.Show("Insert Slp", m_strStep _
                , MessageBoxButtons.OKCancel)

                If dialogResult = dialogResult.OK Then

                    Try
                        'wait time is 5 minute from set paper
                        m_Printer.BeginInsertion(5000)
                        m_Printer.EndInsertion()
                        Exit Do

                    Catch ex As PosControlException


                    End Try


                ElseIf dialogResult = dialogResult.Cancel Then
                    Return

                End If

            Loop

            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

            For i As Integer = 33 To m_Printer.SlpLineChars - 1

                strSpace += " "

            Next

            ' Print data
            strPrintData = vbCrLf + strSpace + "Print credit card sales slip" + vbCrLf
            strPrintData = strPrintData + ESC + "|1lF"
            strPrintData = strPrintData + strSpace + "        SEIKO EPSON Corp." + vbCrLf
            strPrintData = strPrintData + strSpace + "Thank you for coming to our shop!" + vbCrLf
            strPrintData = strPrintData + ESC + "|1lF"
            strPrintData = strPrintData + strSpace + "Date  " + strDate + vbCrLf
            strPrintData = strPrintData + strSpace + "Time      " + strTime + "Casher   " + strName + vbCrLf
            strPrintData = strPrintData + strSpace + "Number of the register  " + strRecno + vbCrLf
            strPrintData = strPrintData + ESC + "|N" + ESC + "|1lF"
            strPrintData = strPrintData + strSpace + "Details                      cost" + vbCrLf
            strPrintData = strPrintData + strSpace + "Cardigan                 $ 100.00" + vbCrLf
            strPrintData = strPrintData + strSpace + "Shoes                     $ 70.00" + vbCrLf
            strPrintData = strPrintData + strSpace + "Hat                       $ 30.00" + vbCrLf
            strPrintData = strPrintData + strSpace + "Bag                      $ 150.00" + vbCrLf
            strPrintData = strPrintData + strSpace + "        Excluded tax     $ 350.00" + vbCrLf
            strPrintData = strPrintData + strSpace + "        Tax(5%)           $ 17.50" + vbCrLf
            strPrintData = strPrintData + strSpace + "        -------------------------" + vbCrLf
            strPrintData = strPrintData + strSpace + ESC + "|2C     Total" + ESC + "|1C     $ 367.50" + vbCrLf
            strPrintData = strPrintData + ESC + "|1lF"
            strPrintData = strPrintData + strSpace + "Company name   EPSON-CARD" + vbCrLf
            strPrintData = strPrintData + strSpace + "Membership No. XXXXXXXXXXXXXXXX" + vbCrLf
            strPrintData = strPrintData + strSpace + "Valid date     12/05" + vbCrLf
            strPrintData = strPrintData + strSpace + "Handling No.   9999 - 999999" + vbCrLf
            strPrintData = strPrintData + strSpace + "Approval No.   99" + vbCrLf
            strPrintData = strPrintData + ESC + "|1lF"
            strPrintData = strPrintData + strSpace + "Signature" + vbCrLf
            'Printing process
            m_Printer.PrintNormal(PrinterStation.Slip, strPrintData)

            'Clean up
            System.Windows.Forms.Cursor.Current = Cursors.Default

            'Remove the slip at the slip station.
            m_Printer.BeginRemoval(10000)
            m_Printer.EndRemoval()

        Catch ex As PosControlException

        End Try
        '<<<Step9>>>--End

    End Sub
    ''' <summary>
    ''' The processing code required in order to enable to use of service is written here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep9_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String
        Dim deviceInfo As DeviceInfo
        Dim posExplorer As PosExplorer
        Dim strCurDir As String
        Dim strFilePath As String

        'Current Directory Path
        strCurDir = Directory.GetCurrentDirectory()

        strFilePath = strCurDir.Substring(0, strCurDir.IndexOf("Step9") + "Step9\".Length)


        strFilePath += "Logo.bmp"

        strLogicalName = "PosPrinter"

        'Create PosExplorer
        posExplorer = New PosExplorer

        m_Printer = Nothing

        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName)
            m_Printer = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception
            ChangeButtonStatus()
            Return
        End Try

        Try

            'Register OutputCompleteEventHandler.
            AddOutputCompleteEvent(m_Printer)

            'Open the device
            m_Printer.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)

            'Enable the device.
            m_Printer.DeviceEnabled = True

            '<<<step3>>>--Start

            'Output by the high quality mode
            m_Printer.RecLetterQuality = True
            Dim iRetryCount As Integer

            If m_Printer.CapRecBitmap Then
                Dim bSetBitmapSuccess As Boolean
                For iRetryCount = 0 To 5
                    Try
                        '<<<step5>>>--Start
                        m_Printer.SetBitmap(1, PrinterStation.Receipt, strFilePath, _
                        m_Printer.RecLineWidth / 2, PosPrinter.PrinterBitmapCenter)
                        '<<<step5>>>--End
                        bSetBitmapSuccess = True
                        Exit For
                    Catch pce As PosControlException
                        If pce.ErrorCode = ErrorCode.Failure And pce.ErrorCodeExtended = 0 And pce.Message = "It is not initialized." Then
                            System.Threading.Thread.Sleep(1000)
                        End If
                    End Try
                Next
                If Not bSetBitmapSuccess Then
                    MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep9" _
                            , MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
            '<<<step3>>>--End

            '<<<step5>>>--Start
            'Even if using any printers, 0.01mm unit makes it possible to print neatly.
            m_Printer.MapMode = MapMode.Metric
            '<<<step5>>>--End

            '<<<step8>>--start
            'Not function to [Print Receipt] button is disable
            If m_Printer.CapRecRight90 = False Or m_Printer.CapRecLeft90 = False Then

                btnReceipt.Enabled = False

            End If
            '<<<step8>>>--End

            '<<<step9>>>--Start
            If m_Printer.CapSlpPresent = False Or m_Printer.CapSlpFullSlip = False Then

                'Not function to [Print Sales slp] button is disable
                btnSales.Enabled = False

            End If
            '<<<step9>>>--End



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
    Private Sub FrameStep9_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step1>>>--Start
        If m_Printer Is Nothing Then
            Return
        End If

        Try
            '<<<step7>>>--Start
            'Remove OutputCompleteEvent.
            RemoveOutputCompleteEvent(m_Printer)
            '<<<step7>>>--End

            'Cancel the device
            m_Printer.DeviceEnabled = False

            'Release the device exclusive control right.
            m_Printer.Release()

        Catch ex As PosControlException

        Finally
            'Finish using the device.
            m_Printer.Close()

        End Try
        '<<<step1>>>--End

    End Sub

    ''' <summary>
    ''' Add OutputCompleteEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddOutputCompleteEvent(ByVal eventSource As Object)

        '<<<step7>>>--Start
        Dim outputCompleteEvent As EventInfo = Nothing

        outputCompleteEvent = eventSource.GetType().GetEvent("OutputCompleteEvent")

        If Not (outputCompleteEvent Is Nothing) Then
            outputCompleteEvent.AddEventHandler(eventSource _
            , New OutputCompleteEventHandler(AddressOf OnOutputCompleteEvent))

        End If

        '<<<step7>>>--End
    End Sub
    ''' <summary>
    ''' Remove OutputCompleteEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveOutputCompleteEvent(ByVal eventSource As Object)

        '<<<step7>>>--Start
        Dim outputCompleteEvent As EventInfo = Nothing

        outputCompleteEvent = eventSource.GetType().GetEvent("OutputCompleteEvent")

        If Not (outputCompleteEvent Is Nothing) Then
            outputCompleteEvent.RemoveEventHandler(eventSource _
            , New OutputCompleteEventHandler(AddressOf OnOutputCompleteEvent))

        End If

        '<<<step7>>>--End
    End Sub
    ''' <summary>
    ''' OutputComplete Event
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Protected Sub OnOutputCompleteEvent(ByVal source As Object, ByVal e As OutputCompleteEventArgs)

        '<<<step7>>>--Start
        'Notify that printing is completed when it is asnchronous.
        MessageBox.Show("Complete printing : ID = " + e.OutputId.ToString(), m_strStep)
        '<<<step7>>>--End

    End Sub

End Class
