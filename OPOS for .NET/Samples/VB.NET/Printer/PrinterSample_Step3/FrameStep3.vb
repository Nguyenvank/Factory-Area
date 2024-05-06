Imports Microsoft.PointOfService
Imports System.Globalization
Imports System.IO


Public Class FrameStep3
    Inherits System.Windows.Forms.Form

    Private m_Printer As Microsoft.PointOfService.PosPrinter = Nothing

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnPrint = New System.Windows.Forms.Button
        Me.grpReceipt = New System.Windows.Forms.GroupBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpReceipt.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(48, 40)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(136, 32)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'grpReceipt
        '
        Me.grpReceipt.Controls.Add(Me.btnPrint)
        Me.grpReceipt.Location = New System.Drawing.Point(24, 28)
        Me.grpReceipt.Name = "grpReceipt"
        Me.grpReceipt.Size = New System.Drawing.Size(228, 96)
        Me.grpReceipt.TabIndex = 1
        Me.grpReceipt.TabStop = False
        Me.grpReceipt.Text = "Receipt"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(156, 140)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(92, 28)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'FrameStep3
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(292, 189)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpReceipt)
        Me.MaximizeBox = False
        Me.Name = "FrameStep3"
        Me.Text = "Step 3  Print Bitmaps"
        Me.grpReceipt.ResumeLayout(False)
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

        'ESC command
        ESC = Chr(&H1B)

        'Get current date
        dateTime = System.DateTime.Now()

        dateFormat.MonthDayPattern = "MMMM"

        strDate = dateTime.ToString("MMMM,dd,yyyy,  HH:mm", dateFormat)

        Try

            '<<<step3>>>--Start
            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|1B")
            '<<<step3>>>--End

            'Print address
            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|N" _
            + "123xxstreet,xxxcity,xxxxstate" + vbCrLf)

            'Print phone number
            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|rA" _
            + "TEL 9999-99-9999   C#2" + vbCrLf)

            'Print date
            'ESC|cA = Centaring char
            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|cA" + strDate + vbCrLf + vbCrLf)
            'Print buying goods
            m_Printer.PrintNormal(PrinterStation.Receipt, "apples                  $20.00" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, "grapes                  $30.00" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, "bananas                 $40.00" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, "lemons                  $50.00" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, "oranges                 $60.00" + vbCrLf + vbCrLf)

            '//Print the total cost
            '//ESC|bC = Bold
            '//ESC|uC = Underline
            '//ESC|2C = Wide charcter
            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|bC" _
            + "Tax excluded.          $200.00" + ESC + "|N" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|uC" _
            + "Tax  5.0%               $10.00" + ESC + "|N" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|bC" + ESC + "|2C" _
            + "Total   $210.00" + ESC + "|N" + vbCrLf)

            m_Printer.PrintNormal(PrinterStation.Receipt, "Customer's payment     $250.00" + vbCrLf)
            m_Printer.PrintNormal(PrinterStation.Receipt, "Change                  $40.00" + vbCrLf + vbCrLf)

            'Feed the receipt to the cutter position automatically, and cut.
            'ESC|#fP = Line Feed and Paper cut	
            m_Printer.PrintNormal(PrinterStation.Receipt, ESC + "|fP")

        Catch ex As Exception

        End Try
        '//<<<step2>>>--End

    End Sub
    ''' <summary>
    ''' When the method "changeButtonStatus" was called,
    ''' all buttons other than a button "closing" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        'Disable control.
        btnPrint.Enabled = False
    End Sub
    ''' <summary>
    ''' Close form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnClose_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnClose.Click

        '<<<step2>>> Start
        Close()
        '<<<step2>>> End

    End Sub
    ''' <summary>
    ''' The processing code required in order to enable to use of service is written here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrameStep3_Load(ByVal sender As System.Object _
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

        strFilePath = strCurDir.Substring(0, strCurDir.IndexOf("Step3") + "Step3\".Length)

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

            'Open the device
            m_Printer.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Printer.Claim(1000)

            'Enable the device.
            m_Printer.DeviceEnabled = True

            '<<<step3>>> Start

            'Output by the high quality mode
            m_Printer.RecLetterQuality = True

            Dim iRetryCount As Integer

            If m_Printer.CapRecBitmap Then
                Dim bSetBitmapSuccess As Boolean
                For iRetryCount = 0 To 5
                    Try
                        m_Printer.SetBitmap(1, PrinterStation.Receipt, strFilePath, _
                        PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter)
                        bSetBitmapSuccess = True
                        Exit For
                    Catch pce As PosControlException
                        If pce.ErrorCode = ErrorCode.Failure And pce.ErrorCodeExtended = 0 And pce.Message = "It is not initialized." Then
                            System.Threading.Thread.Sleep(1000)
                        End If
                    End Try
                Next
                If Not bSetBitmapSuccess Then
                    MessageBox.Show("Failed to set bitmap.", "Printer_SampleStep3" _
                            , MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
            '<<<step3>>> End

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
    Private Sub FrameStep3_Closing(ByVal sender As Object _
    , ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        '<<<step1>>>--Start
        If m_Printer Is Nothing Then
            Return
        End If

        Try
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

End Class
