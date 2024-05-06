namespace Inventory_Data.Ctrl
{
    partial class uc_MachinesStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lst_Machines = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lst_Machines
            // 
            this.lst_Machines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_Machines.HideSelection = false;
            this.lst_Machines.Location = new System.Drawing.Point(0, 0);
            this.lst_Machines.Name = "lst_Machines";
            this.lst_Machines.Size = new System.Drawing.Size(1366, 768);
            this.lst_Machines.TabIndex = 0;
            this.lst_Machines.UseCompatibleStateImageBehavior = false;
            // 
            // uc_MachinesStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lst_Machines);
            this.Name = "uc_MachinesStatus";
            this.Size = new System.Drawing.Size(1366, 768);
            this.Load += new System.EventHandler(this.uc_MachinesStatus_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lst_Machines;
    }
}
