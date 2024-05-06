using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmTemperatureScreenUpdate : Form
    {
        string _line = "";

        public frmTemperatureScreenUpdate()
        {
            InitializeComponent();
        }

        private void frmTemperatureScreenUpdate_Load(object sender, EventArgs e)
        {
            btn_Update.Enabled = false;
            rdb_R_Upd.Checked = false;
            rdb_P_Upd.Checked = false;
            rdb_A_Upd.Checked = false;
            rdb_R_Upd.Enabled = true;
            rdb_P_Upd.Enabled = true;
            rdb_A_Upd.Enabled = true;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string line;
                line = _line;

                if (line != "")
                {
                    string sql = "V2o1_ERP_Temperature_Garthering_Set_Update_UpdItem_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.TinyInt;
                    sParams[0].ParameterName = "@line";
                    sParams[0].Value = line;

                    cls.fnUpdDel(sql,sParams);

                    rdb_R_Upd.Checked = false;
                    rdb_P_Upd.Checked = false;
                    rdb_A_Upd.Checked = false;
                    btn_Update.BackColor = Color.LightGreen;
                    btn_Update.Enabled = false;
                    _line = "";

                    cls.AutoClosingMessageBox.Show("Update successful", "Update Temperature Screen", 1500);
                }
                else
                {
                    MessageBox.Show("Please choose line (Rubber/Injection/Assembly) to refresh first");
                }
            }
        }

        private void rdb_R_Upd_MouseClick(object sender, MouseEventArgs e)
        {
            _line = "1";
            btn_Update.Enabled = true;
            btn_Update.BackColor = Color.LightYellow;
        }

        private void rdb_P_Upd_MouseClick(object sender, MouseEventArgs e)
        {
            _line = "2";
            btn_Update.Enabled = true;
            btn_Update.BackColor = Color.LightYellow;
        }

        private void rdb_A_Upd_MouseClick(object sender, MouseEventArgs e)
        {
            _line = "3";
            btn_Update.Enabled = true;
            btn_Update.BackColor = Color.LightYellow;
        }
    }
}
