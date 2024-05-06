using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventory_Data;

namespace FMB_Tracking_System_v2._1
{
    public partial class frmFMBScanBarcode_v2o6_NGCart : Form
    {
        public frmFMBScanBarcode_v2o6_NGCart()
        {
            InitializeComponent();
        }

        private void frmFMBScanBarcode_v2o6_NGCart_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        private void frmFMBScanBarcode_v2o6_NGCart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /******************************************************/

        public void Fnc_Load_Controls()
        {
            txt_code.Text =
                txt_filter.Text =
                lbl_IN_FMB.Text =
                lbl_Mixing_Date.Text =
                lbl_Chemical_Bag.Text =
                lbl_Weighing_Date.Text = "";
            txt_code.BackColor = Color.Red;
            txt_code.ForeColor = Color.White;
            txt_filter.BackColor =
                lbl_IN_FMB.BackColor =
                lbl_Mixing_Date.BackColor =
                lbl_Weighing_Date.BackColor =
                lbl_Chemical_Bag.BackColor = Color.Gainsboro;
            txt_filter.Enabled = false;
            btn_Save.Enabled = btn_Done.Enabled = false;
            btn_Save.BackColor = btn_Done.BackColor = Color.FromKnownColor(KnownColor.ControlDark);

            dgv_list.DataSource = null;

        }

        /******************************************************/

    }
}
