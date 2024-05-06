Imports Microsoft.PointOfService

Public Class FrameFirstStep
    Inherits System.Windows.Forms.Form

#Region " Windows Forms Designer generated code. "

    Public Sub New()
        MyBase.New()

        ' The InitializeComponent() call is required for windows Forms designer support.
        InitializeComponent()

        ' TODO: Add counstructor code after the InitializeComponent() call.

    End Sub

    ' Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Rear treatment is carried out in the resource being used.
    Private components As System.ComponentModel.IContainer

    ' This method is required for Windows Forms designer support.
    'Do not change the method contents inside the source code editor.   
    ' The Forms designer might not be able to load this method if it was changed manually.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExample1 As System.Windows.Forms.Button
    Friend WithEvents btnExample2 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnExample2 = New System.Windows.Forms.Button
        Me.btnExample1 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExample2)
        Me.GroupBox1.Controls.Add(Me.btnExample1)
        Me.GroupBox1.Location = New System.Drawing.Point(36, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 132)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Exsample"
        '
        'btnExample2
        '
        Me.btnExample2.Location = New System.Drawing.Point(52, 80)
        Me.btnExample2.Name = "btnExample2"
        Me.btnExample2.Size = New System.Drawing.Size(116, 28)
        Me.btnExample2.TabIndex = 1
        Me.btnExample2.Text = "Example2"
        '
        'btnExample1
        '
        Me.btnExample1.Location = New System.Drawing.Point(52, 28)
        Me.btnExample1.Name = "btnExample1"
        Me.btnExample1.Size = New System.Drawing.Size(116, 28)
        Me.btnExample1.TabIndex = 0
        Me.btnExample1.Text = "Example1"
        '
        'FrameFirstStep
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(300, 193)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "FrameFirstStep"
        Me.Text = "First Step ""How to use PosExplorer"""
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'PosCommon object
    Private m_PosCommon As PosCommon = Nothing

    'Logicalname
    Private m_strLogicalName As String = "PosPrinter"
    ''' <summary>
    ''' How to use "PosExplorer" sample1.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExample1_Click(ByVal sender As System.Object _
    , ByVal e As System.EventArgs) Handles btnExample1.Click

        'Use a Logical Device Name which has been set on the SetupPOS.
        Dim posExplorer As PosExplorer = New PosExplorer
        Dim deviceInfo As DeviceInfo = Nothing

        Try

            'Get DeviceInfo use devicecategory and logicalname.
            deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, m_strLogicalName)

            m_PosCommon = posExplorer.CreateInstance(deviceInfo)

            'Open the device.
            m_PosCommon.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_PosCommon.Claim(1000)

            'Enable the device.
            m_PosCommon.DeviceEnabled = True

            'CheckHealth.
            m_PosCommon.CheckHealth(Microsoft.PointOfService.HealthCheckLevel.Interactive)

            'Close device
            m_PosCommon.Close()

        Catch ex As Exception

        End Try

    End Sub
    ''' <summary>
    ''' How to use "PosExplorer" sample2.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExample2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExample2.Click

        Dim posExplorer As PosExplorer = New PosExplorer
        Dim deviceInfo As DeviceInfo = Nothing
        Dim devInfo As DeviceInfo = Nothing
        Dim deviceCollection As DeviceCollection
        Dim astrLogicalNames() As String
        Dim hashDevice As Hashtable = New Hashtable
        Dim iCount As Integer = 0

        Try

            deviceCollection = posExplorer.GetDevices()

            For Each devInfo In deviceCollection

                'if the device name is registered. 
                If devInfo.LogicalNames.Length > 0 Then

                    astrLogicalNames = devInfo.LogicalNames

                    For j As Integer = 0 To astrLogicalNames.Length - 1

                        If Not hashDevice.ContainsKey(astrLogicalNames(j)) Then

                            m_PosCommon = posExplorer.CreateInstance(devInfo)

                            'Use Legacy Opos
                            'If m_PosCommon.Compatibility.Equals(DeviceCompatibilities.Opos) Then
                            'Use Opos for .Net

                            If m_PosCommon.Compatibility.Equals(DeviceCompatibilities.CompatibilityLevel1) Then

                                Try

                                    'Register hashtable key:LogicalName,Value:DeviceInfo
                                    hashDevice.Add(astrLogicalNames(j), devInfo)

                                Catch ex As Exception

                                End Try

                            End If

                        End If

                    Next

                End If

            Next devInfo

            deviceInfo = hashDevice(m_strLogicalName)

            m_PosCommon = posExplorer.CreateInstance(deviceInfo)

            'Open the device
            m_PosCommon.Open()

            'Get the exclusive control right for the opened device.
            'Then the device is disable from other application.
            m_PosCommon.Claim(1000)

            'Enable the device.
            m_PosCommon.DeviceEnabled = True

            'CheckHealth.
            m_PosCommon.CheckHealth(Microsoft.PointOfService.HealthCheckLevel.Interactive)

            'Close device
            m_PosCommon.Close()

        Catch ex As Exception

        End Try

    End Sub
End Class
