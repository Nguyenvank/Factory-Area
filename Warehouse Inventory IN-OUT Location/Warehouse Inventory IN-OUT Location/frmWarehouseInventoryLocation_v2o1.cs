using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventory_Data;

namespace Warehouse_Inventory_IN_OUT_Location
{
    public partial class frmWarehouseInventoryLocation_v2o1 : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_cd = "",
            __whs_nm = "",
            __curr_rack = "";

        string[]
            __rack_letter = new string[] { },
            __rack_letter_backward = new string[] { };

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0,
            __row_num = 3,
            __col_num = 5,
            __rack_next = 31,
            __curr_letter_idx = 0,
            __next_letter_idx = 0;

        cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) },
            __color_cell = { Color.LightGreen, Color.Gold, Color.LightPink, Color.Silver};

        public frmWarehouseInventoryLocation_v2o1()
        {
            InitializeComponent();

            Fnc_Load_Config();

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_top, true);
            cls.SetDoubleBuffer(tlp_top_btn, true);
            cls.SetDoubleBuffer(tlp_legend, true);
            cls.SetDoubleBuffer(tlp_letter, true);
            cls.SetDoubleBuffer(tlp_cover, true);
        }

        private void frmWarehouseInventoryLocation_v2o1_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /****************************************/

        public void Fnc_Load_Controls()
        {
            lbl_value_rack.Text = "";
            chk_autorun.Checked = true;
            btn_prev.Enabled = btn_next.Enabled = false;

            Fnc_Load_Rack_Letter();
            //Fnc_Load_Locate_Init();
            Fnc_Load_Locate_Init_02();

            lbl_legend_normal_1.BackColor = lbl_legend_normal_2.BackColor = __color_cell[0];
            lbl_legend_warning_1.BackColor = lbl_legend_warning_2.BackColor = __color_cell[1];
            lbl_legend_shortage_1.BackColor = lbl_legend_shortage_2.BackColor = __color_cell[2];
            lbl_legend_noplan_1.BackColor = lbl_legend_noplan_2.BackColor = __color_cell[3];
        }

        public void Fnc_Load_Config()
        {
            __whs_cd = ini.GetIniValue("WAREHOUSE", "ID", "102").Trim();
            __whs_nm = ini.GetIniValue("WAREHOUSE", "NM", "CKD INVENTORY LOCATION").Trim();
        }

        public void Fnc_Load_Locate_Init()
        {
            int i = 1;
            try
            {
                for (int col = 1; col <= __col_num; col++)
                {
                    for (int row = 1; row <= __row_num; row++)
                    {
                        TableLayoutPanel
                            tlp_loc = (TableLayoutPanel)cls.FindControlRecursive(pnl_main, "tlp_" + col + "_" + row);

                        Label
                            lbl_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_loc_" + col + "_" + row);
                        /*****************************************/
                        //lbl_title = (Label)cls.FindControlRecursive(pnl_main, "lbl_title_" + row + "_" + col),
                        //lbl_name_scan = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_scan_" + row + "_" + col),
                        //lbl_value_scan = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_scan_" + row + "_" + col),
                        //lbl_name_expired = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_expired_" + row + "_" + col),
                        //lbl_value_expired = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_expired_" + row + "_" + col),
                        //lbl_name_qty = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_qty_" + row + "_" + col),
                        //lbl_value_qty = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_qty_" + row + "_" + col),
                        //lbl_name_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_loc_" + row + "_" + col),
                        //lbl_value_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_loc_" + row + "_" + col);

                        cls.SetDoubleBuffer(tlp_loc, true);
                        tlp_loc.BackColor = Color.FromKnownColor(KnownColor.Control);

                        tlp_loc.Visible = (col <= 4) ? true : false;

                        lbl_loc.Text = String.Format("{0}{1}", "A", i);
                        i++;

                        //lbl_title.Text = "";

                        //lbl_value_scan.Text =
                        //    lbl_value_expired.Text =
                        //    lbl_value_qty.Text = "";

                        //lbl_value_loc.Text = String.Format("{0}-{1}-{2}", "A", row, col);

                        //lbl_title.Text =
                        //    lbl_name_scan.Text = lbl_value_scan.Text =
                        //    lbl_name_expired.Text = lbl_value_expired.Text =
                        //    lbl_name_qty.Text = lbl_value_qty.Text =
                        //    lbl_name_loc.Text = lbl_value_loc.Text = "";
                    }
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Locate_Init_02()
        {
            try
            {
                for(int i = 1; i <= 15; i++)
                {
                    TableLayoutPanel
                        tlp_loc = (TableLayoutPanel)cls.FindControlRecursive(pnl_main, "tlp_" + i);

                    Label
                        lbl_title_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_title_loc_" + i),
                        lbl_item = (Label)cls.FindControlRecursive(pnl_main, "lbl_item_" + i),
                        lbl_cell_1 = (Label)cls.FindControlRecursive(pnl_main, "lbl_cell_" + i + "_1"),
                        lbl_cell_2 = (Label)cls.FindControlRecursive(pnl_main, "lbl_cell_" + i + "_2"),
                        lbl_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_loc_" + i);

                    cls.SetDoubleBuffer(tlp_loc, true);

                    lbl_title_loc.Text = lbl_loc.Text = __curr_rack + "-" + i;
                    lbl_item.Text = "0";
                    lbl_cell_1.Text = lbl_cell_2.Text = "";
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Rack_Letter()
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SubLocate_Letter_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@locIDx";
                sParams[0].Value = __whs_cd;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<string> locateChar = new List<string>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            locateChar.Add(row[0].ToString());
                        }
                        __rack_letter = locateChar.ToArray();
                        lbl_value_rack.Text = __curr_rack = locateChar[0].ToString().Substring(0, 1);
                        __curr_letter_idx = 0;
                    }
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Rack_Letter_Prev()
        {
            string rack_letter = "";

            try
            {
                if (__rack_letter.Length > 0)
                {
                    int nLetter = __rack_letter.Length;

                    __curr_letter_idx -= 1;

                    if (__curr_letter_idx < 0)
                    {
                        __curr_letter_idx = nLetter - 1;
                    }

                    rack_letter = __rack_letter[__curr_letter_idx];
                    lbl_value_rack.Text = __curr_rack = rack_letter.Substring(0, 1);
}
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Rack_Letter_Next()
        {
            string rack_letter = "";

            try
            {
                if (__rack_letter.Length > 0)
                {
                    int nLetter = __rack_letter.Length;

                    __curr_letter_idx += 1;

                    if (__curr_letter_idx >= nLetter)
                    {
                        __curr_letter_idx = 0;
                    }

                    rack_letter = __rack_letter[__curr_letter_idx];
                    lbl_value_rack.Text = __curr_rack = rack_letter.Substring(0, 1);
                }
            }
            catch { }
            finally { }
        }

        /****************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            if (__rack_next >= 0)
            {
                __rack_next--;

                chk_autorun.Text = String.Format("Auto-change in {0:00}s", __rack_next);

                if (__rack_next == 0)
                {
                    Fnc_Load_Rack_Letter_Next();
                    Fnc_Load_Locate_Init_02();

                    __rack_next = 31;
                }
            }
        }

        private void chk_autorun_CheckedChanged(object sender, EventArgs e)
        {
            bool check = (chk_autorun.Checked) ? true : false;

            btn_prev.Enabled = btn_next.Enabled = (check) ? false : true;
            __timer_main.Enabled = (check) ? true : false;
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            Fnc_Load_Rack_Letter_Prev();
            Fnc_Load_Locate_Init_02();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            Fnc_Load_Rack_Letter_Next();
            Fnc_Load_Locate_Init_02();
        }
    }
}
