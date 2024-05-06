using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmTemperatureByDateSelected_v2 : Form
    {
        private readonly frmTemperatureByDate_v2 frmFrom;

        public frmTemperatureByDateSelected_v2()
        {
            InitializeComponent();
        }

        public frmTemperatureByDateSelected_v2(frmTemperatureByDate_v2 frm)
        {
            InitializeComponent();

            frmFrom = frm;
        }

        private void frmTemperatureByDateSelected_v2_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmFrom.Close();
        }

        private void frmTemperatureByDateSelected_v2_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();
            init_Default();
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        public void init_Default()
        {
            if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_ByDate_v2.Instance))
            {
                pnl_Main.Controls.Add(Ctrl.uc_Temperature_ByDate_v2.Instance);
            }
            Ctrl.uc_Temperature_ByDate_v2.Instance.Dock = DockStyle.Fill;
            Ctrl.uc_Temperature_ByDate_v2.Instance.BringToFront();
        }

        private void viewAnotherDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
