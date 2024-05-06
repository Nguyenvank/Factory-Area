using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EJSample_Step5
{
    public class ErrorEventDialog : Form
    {
        public ErrorEventDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.Location = new System.Drawing.Point(30, 13);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(79, 12);
            this.lblErrorMsg.TabIndex = 0;
            this.lblErrorMsg.Text = "Error Message";
            // 
            // btnSuspend
            // 
            this.btnSuspend.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSuspend.Location = new System.Drawing.Point(25, 109);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(102, 21);
            this.btnSuspend.TabIndex = 1;
            this.btnSuspend.Text = "Suspend Process";
            this.btnSuspend.UseVisualStyleBackColor = true;
            // 
            // btnResume
            // 
            this.btnResume.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnResume.Location = new System.Drawing.Point(133, 109);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(94, 21);
            this.btnResume.TabIndex = 2;
            this.btnResume.Text = "Retry Process";
            this.btnResume.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancel.Location = new System.Drawing.Point(233, 109);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel Process";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ErrorEventDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 160);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnSuspend);
            this.Controls.Add(this.lblErrorMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorEventDialog";
            this.Text = "An Error has Occurred.";
            this.Load += new System.EventHandler(this.ErrorEventDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblErrorMsg;
        public System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnCancel;

        private void ErrorEventDialog_Load(object sender, EventArgs e)
        {

        }
    }
}