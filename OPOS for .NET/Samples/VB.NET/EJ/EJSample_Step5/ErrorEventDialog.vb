Public Class ErrorEventDialog
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnResume = New System.Windows.Forms.Button
        Me.btnSuspend = New System.Windows.Forms.Button
        Me.lblErrorMsg = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.btnCancel.Location = New System.Drawing.Point(214, 116)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(94, 21)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel Process"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnResume
        '
        Me.btnResume.DialogResult = System.Windows.Forms.DialogResult.Retry
        Me.btnResume.Location = New System.Drawing.Point(114, 116)
        Me.btnResume.Name = "btnResume"
        Me.btnResume.Size = New System.Drawing.Size(94, 21)
        Me.btnResume.TabIndex = 6
        Me.btnResume.Text = "Retry Process"
        Me.btnResume.UseVisualStyleBackColor = True
        '
        'btnSuspend
        '
        Me.btnSuspend.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSuspend.Location = New System.Drawing.Point(6, 116)
        Me.btnSuspend.Name = "btnSuspend"
        Me.btnSuspend.Size = New System.Drawing.Size(102, 21)
        Me.btnSuspend.TabIndex = 5
        Me.btnSuspend.Text = "Suspend Process"
        Me.btnSuspend.UseVisualStyleBackColor = True
        '
        'lblErrorMsg
        '
        Me.lblErrorMsg.AutoSize = True
        Me.lblErrorMsg.Location = New System.Drawing.Point(12, 9)
        Me.lblErrorMsg.Name = "lblErrorMsg"
        Me.lblErrorMsg.Size = New System.Drawing.Size(79, 12)
        Me.lblErrorMsg.TabIndex = 4
        Me.lblErrorMsg.Text = "Error Message"
        '
        'ErrorEventDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(317, 145)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnResume)
        Me.Controls.Add(Me.btnSuspend)
        Me.Controls.Add(Me.lblErrorMsg)
        Me.Name = "ErrorEventDialog"
        Me.Text = "An Error has Occured."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents btnResume As System.Windows.Forms.Button
    Public WithEvents btnSuspend As System.Windows.Forms.Button
    Public WithEvents lblErrorMsg As System.Windows.Forms.Label

#End Region
End Class