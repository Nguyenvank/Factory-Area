using Inventory_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Warehouse_Inventory_BOM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bOMAssignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmBOMSetting
            if (!cls.IsFormOpen(typeof(frmBOMSetting)))
            {
                foreach (Form childForm in MdiChildren)
                {
                    childForm.Close();
                }

                frmBOMSetting frm = new frmBOMSetting();
                frm.MdiParent = this;
                frm.BringToFront();
                frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void bOMPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmBOMPlan
            if (!cls.IsFormOpen(typeof(frmBOMPlan)))
            {
                foreach (Form childForm in MdiChildren)
                {
                    childForm.Close();
                }

                frmBOMPlan frm = new frmBOMPlan();
                frm.MdiParent = this;
                frm.BringToFront();
                frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void bOMCalculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmBOMCalculate
            if (!cls.IsFormOpen(typeof(frmBOMCalculate)))
            {
                foreach (Form childForm in MdiChildren)
                {
                    childForm.Close();
                }

                frmBOMCalculate frm = new frmBOMCalculate();
                frm.MdiParent = this;
                frm.BringToFront();
                frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.Show();

            }
        }
    }
}
