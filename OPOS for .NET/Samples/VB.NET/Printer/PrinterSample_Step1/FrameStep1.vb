Imports Microsoft.PointOfService


Public Class FrameStep1
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnPrint = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(84, 40)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(120, 32)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print"
        '
        'FrameStep1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(280, 125)
        Me.Controls.Add(Me.btnPrint)
        Me.MaximizeBox = False
        Me.Name = "FrameStep1"
        Me.Text = "Step1 Print ""Hello OPOS for .Net"""
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

        '<<<step1>>> Start
        Try
            'As using the PrintNormal method, send strings to a printer, and print it
            '[vbCrLf] is the standard code for starting a new line.
            m_Printer.PrintNormal(PrinterStation.Receipt, "Hello OPOS for .Net" + vbCrLf)

        Catch ex As PosControlException

        End Try
        '<<<step2>>> End

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
        '<<<step1>>>--End

    End Sub
End Class
