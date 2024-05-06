Imports Microsoft.PointOfService
Imports System.Reflection

Public Class FrameStep2
    Inherits System.Windows.Forms.Form

    'Micr object
    Private m_Micr As Micr = Nothing

    'Now Step
    Private m_strStep As String = "MicrSample_Step2"

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
    Friend WithEvents grpInsert As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.grpInsert = New System.Windows.Forms.GroupBox
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnInsert = New System.Windows.Forms.Button
        Me.txtRawData = New System.Windows.Forms.TextBox
        Me.lblRawData = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.grpInsert.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpInsert
        '
        Me.grpInsert.Controls.Add(Me.btnRemove)
        Me.grpInsert.Controls.Add(Me.btnInsert)
        Me.grpInsert.Controls.Add(Me.txtRawData)
        Me.grpInsert.Controls.Add(Me.lblRawData)
        Me.grpInsert.Location = New System.Drawing.Point(28, 32)
        Me.grpInsert.Name = "grpInsert"
        Me.grpInsert.Size = New System.Drawing.Size(348, 112)
        Me.grpInsert.TabIndex = 0
        Me.grpInsert.TabStop = False
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(220, 64)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(100, 24)
        Me.btnRemove.TabIndex = 3
        Me.btnRemove.Text = "Remove"
        '
        'btnInsert
        '
        Me.btnInsert.Location = New System.Drawing.Point(96, 64)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(100, 24)
        Me.btnInsert.TabIndex = 2
        Me.btnInsert.Text = "Insert"
        '
        'txtRawData
        '
        Me.txtRawData.Location = New System.Drawing.Point(96, 28)
        Me.txtRawData.Name = "txtRawData"
        Me.txtRawData.Size = New System.Drawing.Size(224, 19)
        Me.txtRawData.TabIndex = 1
        Me.txtRawData.Text = ""
        '
        'lblRawData
        '
        Me.lblRawData.Location = New System.Drawing.Point(12, 28)
        Me.lblRawData.Name = "lblRawData"
        Me.lblRawData.Size = New System.Drawing.Size(72, 20)
        Me.lblRawData.TabIndex = 0
        Me.lblRawData.Text = "RawData"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(288, 160)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 24)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Exit"
        '
        'FrameStep2
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(408, 201)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grpInsert)
        Me.MaximizeBox = False
        Me.Name = "FrameStep2"
        Me.Text = "Step 2 Adding ""Time-out"" function."
        Me.grpInsert.ResumeLayout(False)
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
    ''' When the method "ChangeButtonStatus" was called,
    ''' all buttons other than a button "Close" become invalid.
    ''' </summary>
    Private Sub ChangeButtonStatus()

        btnInsert.Enabled = False
        btnRemove.Enabled = False
        txtRawData.Enabled = False

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
    Private Sub FrameStep2_Load(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles MyBase.Load

        '<<<step1>>>--Start
        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim strLogicalName As String
        Dim deviceInfo As DeviceInfo
        Dim posExplorer As PosExplorer

        strLogicalName = "Micr"

        'Create PosExplorer
        posExplorer = New PosExplorer

        Try

            deviceInfo = posExplorer.GetDevice(DeviceType.Micr, strLogicalName)
            m_Micr = posExplorer.CreateInstance(deviceInfo)

        Catch ex As Exception
            ChangeButtonStatus()
            Return
        End Try

        Try

            'Register DataEventHandler.
            AddDataEvent(m_Micr)

            'Open the device
            m_Micr.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_Micr.Claim(1000)

            'Enable the device.
            m_Micr.DeviceEnabled = True

            'In order to enable it to use the DataEvent.
            m_Micr.DataEventEnabled = True

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
    Private Sub FrameStep2_Closing(ByVal sender As Object _
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
    End Sub
    ''' <summary>
    ''' Add DataEventEventHandler.
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

        '<<<step1>>>--Start
        txtRawData.Text = m_Micr.RawData

        m_Micr.DataEventEnabled = True
        '<<<step1>>>--End

    End Sub

End Class
