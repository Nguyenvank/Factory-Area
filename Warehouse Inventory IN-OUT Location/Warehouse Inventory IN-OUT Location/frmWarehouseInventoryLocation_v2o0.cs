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
    public partial class frmWarehouseInventoryLocation_v2o0 : Form
    {
        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_cd = "",
            __whs_nm = "";

        string[] 
            __rack_letter = new string[] { };

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0,
            __row_num = 3,
            __col_num = 5,
            __curr_letter_idx = 0,
            __next_letter_idx = 0;

        cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) },
            __color_cell = { Color.LightGreen, Color.Gold, Color.LightPink, Color.Silver};

        public frmWarehouseInventoryLocation_v2o0()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_top, true);
            cls.SetDoubleBuffer(tlp_top_btn, true);
            cls.SetDoubleBuffer(tlp_legend, true);
            cls.SetDoubleBuffer(tlp_letter, true);
        }

        private void frmWarehouseInventoryLocation_v2o0_Load(object sender, EventArgs e)
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
            chk_autorun.Checked = false;

            Fnc_Load_Locate_Init();

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
            for(int row = 1; row <= __row_num; row++)
            {
                for(int col = 1; col <= __col_num; col++)
                {
                    TableLayoutPanel
                        tlp_loc = (TableLayoutPanel)cls.FindControlRecursive(pnl_main, "tlp_" + row + "_" + col);

                    Label
                        lbl_title = (Label)cls.FindControlRecursive(pnl_main, "lbl_title_" + row + "_" + col),
                        lbl_name_scan = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_scan_" + row + "_" + col),
                        lbl_value_scan = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_scan_" + row + "_" + col),
                        lbl_name_expired = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_expired_" + row + "_" + col),
                        lbl_value_expired = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_expired_" + row + "_" + col),
                        lbl_name_qty = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_qty_" + row + "_" + col),
                        lbl_value_qty = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_qty_" + row + "_" + col),
                        lbl_name_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_name_loc_" + row + "_" + col),
                        lbl_value_loc = (Label)cls.FindControlRecursive(pnl_main, "lbl_value_loc_" + row + "_" + col);

                    cls.SetDoubleBuffer(tlp_loc, true);
                    tlp_loc.BackColor = Color.Gainsboro;

                    lbl_title.Text = "";

                    lbl_value_scan.Text =
                        lbl_value_expired.Text =
                        lbl_value_qty.Text = "";

                    lbl_value_loc.Text = String.Format("{0}-{1}-{2}", "A", row, col);

                    //lbl_title.Text =
                    //    lbl_name_scan.Text = lbl_value_scan.Text =
                    //    lbl_name_expired.Text = lbl_value_expired.Text =
                    //    lbl_name_qty.Text = lbl_value_qty.Text =
                    //    lbl_name_loc.Text = lbl_value_loc.Text = "";
                }
            }
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
                        lbl_value_rack.Text = locateChar[0].ToString();
                        __curr_letter_idx = 0;
                    }
                }
            }
            catch { }
            finally { }
        }

        /****************************************/
    }
}
