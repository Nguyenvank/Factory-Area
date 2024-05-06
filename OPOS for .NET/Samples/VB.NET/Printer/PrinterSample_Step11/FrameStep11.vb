Imports Microsoft.PointOfService
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports jp.co.epson.uposcommon

Public Class FrameStep11
    Inherits System.Windows.Forms.Form

    'Printer object.
    Private m_Printer As Microsoft.PointOfService.PosPrinter = Nothing

    'Now Step.
    Private m_strStep As String = "PrinterSample_Step11"

    'Printer cover status.
    Private m_btnStateByCover As Boolean = True

    'Printer paper status.
    Private m_btnStateByPaper As Boolean = True

    'Conversensor flag.
    Private m_bConverSensor As Boolean = False
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
    Friend WithEvents grpDirectIO As System.Windows.Forms.GroupBox
    Friend WithEvents btnSupportFunction As System.Windows.Forms.Button
    Friend WithEvents btnPrintCode128 As System.Windows.Forms.Button
    Friend WithEvents btnPanelSwitch As System.Windows.Forms.Button
    Friend WithEvents btnRecoverError As System.Windows.Forms.Button
    Friend WithEvents grpReCoverError As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnPrint = New System.Windows.Forms.Button
        Me.grpReceipt = New System.Windows.Forms.GroupBox
        Me.btnReceipt = New System.Windows.Forms.Button
        Me.btnAsync = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpSlp = New System.Windows.Forms.GroupBox
        Me.btnSales = New System.Windows.Forms.Button
        Me.grpDirectIO = New System.Windows.Forms.GroupBox
        Me.grpReCoverError = New System.Windows.Forms.GroupBox
        Me.btnRecoverError = New System.Windows.Forms.Button
        Me.btnPanelSwitch = New System.Windows.Forms.Button
        Me.btnPrintCode128 = New System.Windows.Forms.Button
        Me.btnSupportFunction = New System.Windows.Forms.Button
        Me.grpReceipt.SuspendLayout()
        Me.grpSlp.SuspendLayout()
        Me.grpDirectIO.SuspendLayout()
        Me.grpReCoverError.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(28, 32)
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
        Me.grpReceipt.Location = New System.Drawing.Point(24, 24)
        Me.grpReceipt.Name = "grpReceipt"
        Me.grpReceipt.Size = New System.Drawing.Size(196, 144)
        Me.grpReceipt.TabIndex = 1
        Me.grpReceipt.TabStop = False
        Me.grpReceipt.Text = "Receipt"
        '
        'btnReceipt
        '
        Me.btnReceipt.Location = New System.Drawing.Point(28, 104)
        Me.btnReceipt.Name = "btnReceipt"
        Me.btnReceipt.Size = New System.Drawing.Size(144, 24)
        Me.btnReceipt.TabIndex = 2
        Me.btnReceipt.Text = "Print Receipt"
        '
        'btnAsync
        '
        Me.btnAsync.Location = New System.Drawing.Point(28, 68)
        Me.btnAsync.Name = "btnAsync"
        Me.btnAsync.Size = New System.Drawing.Size(144, 24)
        Me.btnAsync.TabIndex = 1
        Me.btnAsync.Text = "Asynchronous printing"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(352, 272)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 28)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'grpSlp
        '
        Me.grpSlp.Controls.Add(Me.btnSales)
        Me.grpSlp.Location = New System.Drawing.Point(28, 184)
        Me.grpSlp.Name = "grpSlp"
        Me.grpSlp.Size = New System.Drawing.Size(192, 72)
        Me.grpSlp.TabIndex = 3
        Me.grpSlp.TabStop = False
        Me.grpSlp.Text = "Slp"
        '
        'btnSales
        '
        Me.btnSales.Location = New System.Drawing.Point(24, 28)
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Size = New System.Drawing.Size(144, 24)
        Me.btnSales.TabIndex = 3
        Me.btnSales.Text = "Print Sales Slp"
        '
        'grpDirectIO
        '
        Me.grpDirectIO.Controls.Add(Me.grpReCoverError)
        Me.grpDirectIO.Controls.Add(Me.btnPanelSwitch)
        Me.grpDirectIO.Controls.Add(Me.btnPrintCode128)
        Me.grpDirectIO.Controls.Add(Me.btnSupportFunction)
        Me.grpDirectIO.Location = New System.Drawing.Point(228, 24)
        Me.grpDirectIO.Name = "grpDirectIO"
        Me.grpDirectIO.Size = New System.Drawing.Size(216, 232)
        Me.grpDirectIO.TabIndex = 4
        Me.grpDirectIO.TabStop = False
        Me.grpDirectIO.Text = "DirectIO"
        '
        'grpReCoverError
        '
        Me.grpReCoverError.Controls.Add(Me.btnRecoverError)
        Me.grpReCoverError.Location = New System.Drawing.Point(16, 144)
        Me.grpReCoverError.Name = "grpReCoverError"
        Me.grpReCoverError.Size = New System.Drawing.Size(180, 72)
        Me.grpReCoverError.TabIndex = 4
        Me.grpReCoverError.TabStop = False
        Me.grpReCoverError.Text = "When The Error Occurred"
        '
        'btnRecoverError
        '
        Me.btnRecoverError.Location = New System.Drawing.Point(16, 28)
        Me.btnRecoverError.Name = "btnRecoverError"
        Me.btnRecoverError.Size = New System.Drawing.Size(144, 24)
        Me.btnRecoverError.TabIndex = 4
        Me.btnRecoverError.Text = "Recover Error"
        '
        'btnPanelSwitch
        '
        Me.btnPanelSwitch.Location = New System.Drawing.Point(32, 104)
        Me.btnPanelSwitch.Name = "btnPanelSwitch"
        Me.btnPanelSwitch.Size = New System.Drawing.Size(144, 24)
        Me.btnPanelSwitch.TabIndex = 3
        Me.btnPanelSwitch.Text = "Panel Switch"
        '
        'btnPrintCode128
        '
        Me.btnPrintCode128.Location = New System.Drawing.Point(32, 68)
        Me.btnPrintCode128.Name = "btnPrintCode128"
        Me.btnPrintCode128.Size = New System.Drawing.Size(144, 24)
        Me.btnPrintCode128.TabIndex = 2
        Me.btnPrintCode128.Text = "Print Code128 TypeB"
        '
        'btnSupportFunction
        '
        Me.btnSupportFunction.Location = New System.Drawing.Point(32, 32)
        Me.btnSupportFunction.Name = "btnSupportFunction"
        Me.btnSupportFunction.Size = New System.Drawing.Size(144, 24)
        Me.btnSupportFunction.TabIndex = 1
        Me.btnSupportFunction.Text = "SupportFunction"
        '
        'FrameStep11
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(468, 317)
        Me.Controls.Add(Me.grpDirectIO)
        Me.Controls.Add(Me.grpSlp)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpReceipt)
        Me.MaximizeBox = False
        Me.Name = "FrameStep11"
        Me.Text = "Step 11  Use the EPSON original methods"
        Me.grpReceipt.ResumeLayout(False)
        Me.grpSlp.ResumeLayout(False)
        Me.grpDirectIO.ResumeLayout(False)
        Me.grpReCoverError.ResumeLayout(False)
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
                ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                m_Printer.ClearOutput()
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
        btnPanelSwitch.Enabled = False
        btnPrintCode128.Enabled = False
        btnRecoverError.Enabled = False
        btnSupportFunction.Enabled = False

    End Sub
    ''' <summary>
    ''' The processing of the insertion of the slip paper and the
    ''' processing when an error occurs is described here.
    ''' </summary>
    Private Function WaitforInsertion() As Boolean

        '<<<Step10>>>--Start
        Dim dialogResult As DialogResult
        Dim bInsertion As Boolean = True

InsertStart:

        Do
            Try

                m_Printer.BeginInsertion(500)

            Catch ex As PosControlException

                If ex.ErrorCode = ErrorCode.Timeout Then

                    dialogResult = MessageBox.Show("Please insert a form.", m_strStep _
                    , MessageBoxButtons.YesNo)

                    If dialogResult = dialogResult.No Then

                        bInsertion = False

                        Try
                            m_Printer.EndInsertion()
                            m_Printer.BeginRemoval(5000)

                        Catch ex2 As PosControlException

                        End Try

                        Return bInsertion

                    End If

                Else
                    Dim strMessage As String
                    strMessage = GetErrorCode(ex)
                    MessageBox.Show(strMessage, m_strStep)
                    bInsertion = False

                End If

            End Try

            Try
                m_Printer.EndInsertion()
                bInsertion = True
               
            Catch ex As PosControlException
                Dim strMessage As String = ""
                If ex.ErrorCodeExtended = PosPrinter.ExtendedErrorSlipEmpty Then

                    strMessage = "Please insert a form."

                Else
                    If m_Printer.SlpEmpty = False Then

                        strMessage = "Please remove a form."

                    Else

                        strMessage = GetErrorCode(ex)

                    End If
                End If


                dialogResult = MessageBox.Show(strMessage + vbCrLf + vbCrLf + "Do you continue?" _
                , "Print credit sales slip", MessageBoxButtons.YesNo)

                If dialogResult = dialogResult.No Then
                    bInsertion = False
                    Return bInsertion
                End If

                Try
                    m_Printer.BeginRemoval(5000)


                Catch ex3 As PosControlException


                End Try

                GoTo InsertStart

            End Try

            Exit Do

        Loop

        Return bInsertion
        '<<<Step10>>>--End

    End Function

    ''' <summary>
    ''' Discharge processing of a slip paper and processing when
    ''' an error occurs are described here.
    ''' </summary>
    Private Sub WaitforRemoval()

        'Discharge operation of a paper is started.
        '<<<Step10>>>--Start
        Try
            m_Printer.BeginRemoval(10000)

        Catch ex As PosControlException

            Dim strMessage As String = ""

            If ex.ErrorCode = ErrorCode.Timeout Then

                strMessage = "Please remove a form."
            Else
                strMessage = GetErrorCode(ex)
            End If

            MessageBox.Show(strMessage, "Print credit sales slip" _
            , MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return

        End Try

        Try
            m_Printer.EndRemoval()

        Catch ex As Exception

        End Try
        '<<<Step10>>>--End

    End Sub
    ''' <summary>
    ''' The information related to the error from the parameter
    ''' "ex" is received as a type of "int".
    ''' Information by the sentence corresponding to the received
    ''' information is returned as "strErrorCodeEx".
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <returns>
    ''' "int" type information is changed into the information
    ''' by the sentence, and is returned as a "String" type.
    ''' "strErrorCodeEx" holds the information on this "int" type.
    ''' </returns>
    Private Function GetErrorCode(ByVal ex As PosControlException) As String

        '<<<step10>>>--Start
        Dim strErrorCodeEx As String = ""
        Dim strEC As String = ""
        Dim strECE As String = ""

        Select Case ex.ErrorCodeExtended

            Case PosPrinter.ExtendedErrorCoverOpen
                strErrorCodeEx = ex.Message
            Case PosPrinter.ExtendedErrorJournalEmpty
                strErrorCodeEx = ex.Message
            Case PosPrinter.ExtendedErrorReceiptEmpty
                strErrorCodeEx = ex.Message
            Case PosPrinter.ExtendedErrorSlipEmpty
                strErrorCodeEx = ex.Message
            Case Else
                strEC = ex.ErrorCode.ToString()
                strECE = ex.ErrorCodeExtended.ToString()
                strErrorCodeEx = "ErrorCode =" + strEC + vbCrLf + "ErrorCodeExtended =" + strECE + vbCrLf _
                + ex.Message

        End Select

        GetErrorCode = strErrorCodeEx

        '<<<step10>>>--End
    End Function
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

            '<<<step10>>>--Start
            Do

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
                    Exit Do

                Catch ex As PosControlException

                    If ex.ErrorCode = ErrorCode.Illegal And ex.ErrorCodeExtended = 1004 Then

                        MessageBox.Show("Unable to print receipt." + vbCrLf + GetErrorCode(ex) _
                         , m_strStep, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                        m_Printer.ClearOutput()
                        bBuffering = False
                        Exit Do

                    End If

                    'When error occurs, display a message to ask the user whether retry or not.
                    DialogResult = MessageBox.Show("Fails to output to a printer." + vbCrLf + vbCrLf + "Retry?" _
                     , m_strStep, MessageBoxButtons.AbortRetryIgnore)

                    Try
                        ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
                        m_Printer.ClearOutput()

                    Catch ex2 As PosControlException

                    End Try

                    If DialogResult = System.Windows.Forms.DialogResult.Abort Or DialogResult = System.Windows.Forms.DialogResult.Ignore Then

                        Exit Do

                    End If

                End Try

            Loop

        Else

            MessageBox.Show("Cannot use a Receipt Stateion.", m_strStep _
            , MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Return

        End If

        Try
            m_Printer.RotatePrint(PrinterStation.Receipt, PrintRotation.Normal)

            If bBuffering = True Then

                m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|fP")

            End If


            Do While m_Printer.State = Not ControlState.Idle

                Try

                    System.Threading.Thread.Sleep(100)

                Catch ex As PosControlException

                End Try

            Loop

            m_Printer.TransactionPrint(PrinterStation.Receipt, PrinterTransactionControl.Normal)

        Catch ex As Exception

            ' Clear the buffered data since the buffer retains print data when an error occurs during printing.
            m_Printer.ClearOutput()
            MessageBox.Show("Unable to print receipt." + vbCrLf + GetErrorCode(ex), m_strStep, _
             MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try
        '<<<step10>>>--End

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

        'ESC command
        ESC = Chr(&H1B)

        '<<<step10>>>--Start
        If WaitforInsertion() = False Then
            Return
        End If
        '<<<step10>>>--End

        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try

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

            '<<<step10>>>--Start
            'Remove the slip at the slip station.
            WaitforRemoval()
            '<<<step10>>>--End

        Catch ex As Exception

        End Try
        ''<<<Step9>>>--End

    End Sub
    ''' <summary>
    ''' When the button "SupportFunction" is pushed, the method
    ''' "directIO" is run. "directIO" decides that function by the
    ''' parameter. "SupportFunction" is ordered here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSupportFunction_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnSupportFunction.Click

        '<<<step11>>--Start
        Dim iData As Integer
        Dim abyte() As Byte = {0}
        Dim directIOReturn As DirectIOData
        Dim strMessage As String = ""

        Try

            'directIO(int commnad,int data,object)
            directIOReturn = m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_GET_SUPPORT_FUNCTION _
            , iData, abyte)

            If Convert.ToBoolean(directIOReturn.Data And _
                EpsonPOSPrinterConst.PTR_DI_LABEL) Then

                strMessage = "Lable function supported."

            ElseIf Convert.ToBoolean(directIOReturn.Data And _
                EpsonPOSPrinterConst.PTR_DI_BLACK_MARK) Then

                strMessage = "BlackMark function supported."

            ElseIf Convert.ToBoolean(directIOReturn.Data And _
                EpsonPOSPrinterConst.PTR_DI_GB18030) Then

                strMessage = "GB18030 Simplified Chinese can be printed."

            ElseIf Convert.ToBoolean(directIOReturn.Data And _
                EpsonPOSPrinterConst.PTR_DI_BATTERY) Then

                strMessage = "Battery function supported."

            Else

                strMessage = "Return:" + directIOReturn.Data.ToString()

            End If

            MessageBox.Show(strMessage, "DirectIOData")

        Catch ex As PosControlException

            MessageBox.Show("This function is disable to use.", m_strStep)

        End Try
        '<<<step11>>--End

    End Sub
    ''' <summary>
    ''' When the button "PrintCode128" is pushed, the method
    ''' "directIO" is run. "directIO" decides that function by the
    ''' parameter."Code128 typeB" is ordered here.
    '''  printer panel button is disabled.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPrintCode128_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnPrintCode128.Click

        '<<<step11>>--Start
        Dim iData As Integer = EpsonPOSPrinterConst.PTR_DI_CODE_B
        Dim abyte() As Byte = {0}
        Dim directIOReturn As DirectIOData
        Dim strbcData As String = "OPOS for .NET"

        Try

            'directIO(int commnad,int data,object)
            directIOReturn = m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_CODE128_TYPE _
            , iData, abyte)

            m_Printer.PrintBarCode(PrinterStation.Receipt, _
            strbcData, BarCodeSymbology.Code128, _
            1000, m_Printer.RecLineWidth, _
            PosPrinter.PrinterBarCodeCenter, _
            BarCodeTextPosition.Below)

        Catch ex As PosControlException

            MessageBox.Show("DirectIO Code128 failed.", m_strStep)

        End Try
        '<<<step11>>--End
    End Sub
    ''' <summary>
    ''' When the button "Panel Switch" is pushed, the method
    ''' "directIO" is run. "directIO" decides that function by the
    ''' parameter."Panel Switch" is ordered here.
    '''  printer panel button is disabled.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnPanelSwitch_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnPanelSwitch.Click

        '<<<step11>>--Start
        Dim iData As Integer = 0
        Dim abyte() As Byte = {0}
        Dim directIOReturn As DirectIOData

        Try

            'directIO(int commnad,int data,object)
            directIOReturn = m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_PANEL_SWITCH _
            , iData, abyte)

        Catch ex As PosControlException

            MessageBox.Show("This function is disable to use.", m_strStep)

        End Try
        '<<<step11>>--End

    End Sub
    '''<summary>
    ''' When the button "Recover Error" is pushed, the method
    ''' "directIO" is run. "directIO" decides that function by the
    ''' parameter. "RecoverError" is ordered.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRecoverError_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnRecoverError.Click

        '<<<step11>>--Start
        Dim iData As Integer = 0
        Dim abyte() As Byte = {0}
        Dim directIOReturn As DirectIOData

        Try

            'directIO(int commnad,int data,object)
            directIOReturn = m_Printer.DirectIO(EpsonPOSPrinterConst.PTR_DI_RECOVER_ERROR _
            , iData, abyte)

        Catch ex As PosControlException

            MessageBox.Show("This function is disable to use.", m_strStep)

        End Try
        '<<<step11>>--End

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
        Dim strCurDir As String
        Dim strFilePath As String

        'Current Directory Path
        strCurDir = Directory.GetCurrentDirectory()

        strFilePath = strCurDir.Substring(0, strCurDir.IndexOf("Step11") + "Step11\".Length)

        strFilePath += "Logo.bmp"

        strLogicalName = "PosPrinter"

        'Create PosExplorer
        posExplorer = New PosExplorer

        m_Printer = Nothing

        '<<<step10>>>--Start
        Try
            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName)

        Catch ex As Exception

            MessageBox.Show("Fails to get device information.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try
            m_Printer = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception
            MessageBox.Show("Fails to create device instance.", m_strStep)
            ChangeButtonStatus()
            Return
        End Try

        'Register OutputCompleteEventHandler.
        AddOutputCompleteEvent(m_Printer)

        '<<<step10>>--Start
        'Register ErrorEventHandler.
        AddErrorEvent(m_Printer)

        'Add StatusUpdateEventHandler
        AddStatusUpdateEvent(m_Printer)
        '<<<step10>>--End

        Try
            'Open the device
            m_Printer.Open()

        Catch ex As PosControlException

            MessageBox.Show("Fails to open the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try


        Try
            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)

        Catch ex As PosControlException

            MessageBox.Show("Fails to claim the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()
            Return

        End Try

        Try

            'Enable the device.
            m_Printer.DeviceEnabled = True

        Catch ex As PosControlException

            MessageBox.Show("Disable to use the device.", m_strStep)
            'Nothing can be used.
            ChangeButtonStatus()

        End Try


        '<<<step3>>>--Start
        Try
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
                    MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep11" _
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

        Catch ex As PosControlException

        End Try
        '<<<step9>>>--End

        '<<<step10>>>--Start
        m_bConverSensor = m_Printer.CapCoverSensor
        '<<<step10>>>--End

        '<<<step1>>>--End
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
        If m_Printer Is Nothing Then
            Return
        End If

        Try
            '<<<step7>>>--Start
            'Remove OutputCompleteEventHandler.
            RemoveOutputCompleteEvent(m_Printer)
            '<<<step7>>>--End

            '<<<step10>>>--Start
            'Remove ErrorEventHandler.
            RemoveErrorEvent(m_Printer)
            '<<<step10>>>--End

            '<<<step10>>>--Start
            'Remove StatusUpdateEventHandler.
            RemoveStatusUpdateEvent(m_Printer)
            '<<<step10>>>--End

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
    ''' Add ErrorEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub AddErrorEvent(ByVal eventSource As Object)

        '<<<step10>>>--Start
        Dim errorEvent As EventInfo = Nothing

        errorEvent = eventSource.GetType().GetEvent("ErrorEvent")

        If Not (errorEvent Is Nothing) Then
            errorEvent.AddEventHandler(eventSource _
            , New DeviceErrorEventHandler(AddressOf OnErrorEvent))

        End If

        '<<<step10>>>--End
    End Sub

    ''' <summary>
    ''' Remove OutputCompleteEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveOutputCompleteEvent(ByVal eventSource As Object)

        '<<<step10>>>--Start

        Dim outputCompleteEvent As EventInfo = Nothing

        outputCompleteEvent = eventSource.GetType().GetEvent("OutputCompleteEvent")

        If Not (outputCompleteEvent Is Nothing) Then
            outputCompleteEvent.RemoveEventHandler(eventSource _
            , New OutputCompleteEventHandler(AddressOf OnOutputCompleteEvent))

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
    ''' Remove ErrorEventHandler.
    ''' </summary>
    ''' <param name="eventSource"></param>
    Protected Sub RemoveErrorEvent(ByVal eventSource As Object)

        '<<<step7>>>--Start

        Dim errorEvent As EventInfo = Nothing

        errorEvent = eventSource.GetType().GetEvent("ErrorEvent")

        If Not (errorEvent Is Nothing) Then
            errorEvent.RemoveEventHandler(eventSource _
            , New DeviceErrorEventHandler(AddressOf OnErrorEvent))

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

        '<<<step10>>>--Start
        Dim dialogResult As DialogResult

        Dim strMessage As String

        strMessage = "Printer Error" + vbCrLf + vbCrLf + "ErrorCode = " + e.ErrorCode.ToString() _
        + vbCrLf + "ErrorCodeExtended = " + e.ErrorCodeExtended.ToString()

        dialogResult = MessageBox.Show(strMessage, m_strStep, MessageBoxButtons.RetryCancel)

        If dialogResult = dialogResult.Cancel Then

            e.ErrorResponse = ErrorResponse.Clear

        ElseIf dialogResult = dialogResult.Retry Then

            e.ErrorResponse = ErrorResponse.Retry

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

        '<<<step10>>>--Start
        'When there is a change of the status on the printer, the event is fired.
        Select Case e.Status

            'Printer cover is open.
        Case PosPrinter.StatusCoverOpen
                m_btnStateByCover = False

            Case PosPrinter.StatusReceiptEmpty
                m_btnStateByPaper = False
                'Printer cover is close.
            Case PosPrinter.StatusCoverOK
                m_btnStateByCover = True
                'Receipt paper is ok.		
            Case PosPrinter.StatusReceiptPaperOK
                m_btnStateByPaper = True
            Case PosPrinter.StatusReceiptNearEmpty
                m_btnStateByPaper = True
        End Select

        If (m_btnStateByPaper = True) And ((m_btnStateByCover = True Or Not (m_bConverSensor))) Then

            btnPrint.Enabled = True
            btnAsync.Enabled = True
            btnSales.Enabled = True
            btnPanelSwitch.Enabled = True
            btnPrintCode128.Enabled = True
            btnRecoverError.Enabled = True
            btnSupportFunction.Enabled = True

            Try
                If m_Printer.CapRecLeft90 = False Or m_Printer.CapRecRight90 = False Then
                    btnReceipt.Enabled = False
                Else
                    btnReceipt.Enabled = True

                End If

                If m_Printer.CapSlpPresent = False Or m_Printer.CapSlpFullSlip = False Then

                    btnSales.Enabled = False

                End If

            Catch ex As PosControlException

            End Try
        Else

            ChangeButtonStatus()

        End If
        '<<<step10>>>--End

    End Sub

End Class
