namespace FMB_Tracking_System_v2._1
{
    partial class frmFMBScanBarcode_v2o5_CMF
    {
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Code = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_Sec = new System.Windows.Forms.Label();
            this.lbl_Count = new System.Windows.Forms.Label();
            this.lbl_Comp = new System.Windows.Forms.Label();
            this.lst_List = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "MÃ C.M.F:";
            // 
            // txt_Code
            // 
            this.txt_Code.Location = new System.Drawing.Point(19, 32);
            this.txt_Code.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(308, 26);
            this.txt_Code.TabIndex = 1;
            this.txt_Code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Code_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "MỤC NHẬP C.M.F:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 301);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Thời gian còn lại lưu tự động:";
            // 
            // lbl_Sec
            // 
            this.lbl_Sec.AutoSize = true;
            this.lbl_Sec.Location = new System.Drawing.Point(199, 301);
            this.lbl_Sec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Sec.Name = "lbl_Sec";
            this.lbl_Sec.Size = new System.Drawing.Size(25, 19);
            this.lbl_Sec.TabIndex = 0;
            this.lbl_Sec.Text = "10";
            // 
            // lbl_Count
            // 
            this.lbl_Count.AutoSize = true;
            this.lbl_Count.Location = new System.Drawing.Point(310, 84);
            this.lbl_Count.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Count.Name = "lbl_Count";
            this.lbl_Count.Size = new System.Drawing.Size(17, 19);
            this.lbl_Count.TabIndex = 0;
            this.lbl_Count.Text = "0";
            // 
            // lbl_Comp
            // 
            this.lbl_Comp.AutoSize = true;
            this.lbl_Comp.Location = new System.Drawing.Point(291, 9);
            this.lbl_Comp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Comp.Name = "lbl_Comp";
            this.lbl_Comp.Size = new System.Drawing.Size(36, 19);
            this.lbl_Comp.TabIndex = 0;
            this.lbl_Comp.Text = "false";
            this.lbl_Comp.Visible = false;
            // 
            // lst_List
            // 
            this.lst_List.FormattingEnabled = true;
            this.lst_List.ItemHeight = 19;
            this.lst_List.Location = new System.Drawing.Point(19, 106);
            this.lst_List.Name = "lst_List";
            this.lst_List.Size = new System.Drawing.Size(308, 175);
            this.lst_List.TabIndex = 4;
            // 
            // frmFMBScanBarcode_v2o5_CMF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 343);
            this.Controls.Add(this.lst_List);
            this.Controls.Add(this.txt_Code);
            this.Controls.Add(this.lbl_Count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_Sec);
            this.Controls.Add(this.lbl_Comp);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFMBScanBarcode_v2o5_CMF";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C.M.F CODE SCAN IN";
            this.Load += new System.EventHandler(this.frmFMBScanBarcode_v2o5_CMF_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFMBScanBarcode_v2o5_CMF_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Sec;
        private System.Windows.Forms.Label lbl_Count;
        private System.Windows.Forms.Label lbl_Comp;
        private System.Windows.Forms.ListBox lst_List;
    }
}