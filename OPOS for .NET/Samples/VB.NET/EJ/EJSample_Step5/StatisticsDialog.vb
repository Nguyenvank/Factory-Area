Public Class StatisticsDialog
    Inherits System.Windows.Forms.Form

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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnOK = New System.Windows.Forms.Button
        Me.txtStats = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(196, 196)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(121, 21)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "Finish"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtStats
        '
        Me.txtStats.Location = New System.Drawing.Point(12, 12)
        Me.txtStats.Multiline = True
        Me.txtStats.Name = "txtStats"
        Me.txtStats.ReadOnly = True
        Me.txtStats.Size = New System.Drawing.Size(305, 178)
        Me.txtStats.TabIndex = 2
        Me.txtStats.Text = "Parsing Failed."
        '
        'StatisticsDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 226)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtStats)
        Me.Name = "StatisticsDialog"
        Me.Text = "StatisticsDialog"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnOK As System.Windows.Forms.Button
    Public WithEvents txtStats As System.Windows.Forms.TextBox
End Class