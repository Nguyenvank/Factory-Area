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

namespace Warehouse_Inventory_IN_OUT_Location.Ctrl
{
    public partial class uc_Warehouse_Racks_Locations_Layout : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_idx = "",
            __rack_cd = "",
            __rack_char = "",
            __curr_rack = "";

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
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0,
            __rack_next = 31,
            __curr_letter_idx = 0;

        cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public uc_Warehouse_Racks_Locations_Layout()
        {
            InitializeComponent();

            Fnc_Set_Config_Warehouse();
            Fnc_Load_Rack_Letter();

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

        public uc_Warehouse_Racks_Locations_Layout(string whs_idx, string rack_cd)
        {
            InitializeComponent();

            __whs_idx = whs_idx;
            __rack_cd = rack_cd;

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_top, true);
            cls.SetDoubleBuffer(tlp_top_btn, true);
            cls.SetDoubleBuffer(tlp_legend, true);
            cls.SetDoubleBuffer(tlp_letter, true);
            cls.SetDoubleBuffer(tlp_cover, true);
        }

        private void uc_Warehouse_Racks_Locations_Layout_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        private void uc_Warehouse_Racks_Locations_Layout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /**************************************/

        public void Fnc_Load_Controls()
        {
            __rack_char = __rack_cd.Substring(0, 1);
            lbl_value_rack.Text = __rack_char;

            Fnc_Load_Rack_Locate_Init();

            chk_autorun.Checked = true;
            btn_prev.Enabled = btn_next.Enabled = false;
        }

        public void Fnc_Load_Rack_Letter()
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_Inventory_Rack_Location_Layout_Letter_SelItem_Addnew_v1o0";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@whs_idx";
                sParams[0].Value = __whs_idx;

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
                        lbl_value_rack.Text = __curr_rack = __rack_cd = locateChar[0].ToString();
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
                    __curr_rack = __rack_cd = rack_letter;
                    lbl_value_rack.Text = __rack_cd.Substring(0, 1);
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
                    __curr_rack = __rack_cd = rack_letter;
                    lbl_value_rack.Text = __rack_cd.Substring(0, 1);
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Set_Config_Warehouse()
        {
            __whs_idx = ini.GetIniValue("WHS", "ID", "101");
            //__whs_nm = ini.GetIniValue("WHS", "NM", "RESIN");
        }

        public void Fnc_Load_Rack_Locate_Init()
        {
            string
                whs_idx = __whs_idx,
                rack_cd = __rack_cd;

            try
            {
                for (int i = 1; i <= 30; i++)
                {
                    Label
                        lbl_title = (Label)cls.FindControlRecursive(pnl_rack, "lbl_title_" + i),
                        lbl_cell = (Label)cls.FindControlRecursive(pnl_rack, "lbl_cell_" + i),
                        lbl_loc = (Label)cls.FindControlRecursive(pnl_rack, "lbl_loc_" + i);

                    lbl_title.Text = "";

                    lbl_cell.Text = "";
                    lbl_cell.Enabled = false;

                    lbl_loc.Text = "";

                    lbl_title.BackColor=lbl_cell.BackColor = lbl_loc.BackColor = Color.DarkGray;

                    //lbl_title_loc.Text = lbl_loc.Text = __curr_rack + "-" + i;
                    //lbl_item.Text = "0";
                    //lbl_cell_1.Text = lbl_cell_2.Text = "";
                }

                Fnc_Load_Rack_Locate_Cell_Numbers(whs_idx, rack_cd);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Rack_Locate_Cell_Numbers(string whs_idx, string rack_cd)
        {
            string
                cell_used = "",
                cell_free = "",
                cell_item = "";

            int
                _cell_item = 0;

            bool
                _cell_used = false,
                _cell_free = false;

            try
            {
                __sql = "V2o1_BASE_Warehouse_Material_ScanIn_Rack_Locate_Cell_SelItem_Addnew_v1o0";

                __sparam = new SqlParameter[2];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@whs_idx";
                __sparam[0].Value = whs_idx;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.VarChar;
                __sparam[1].ParameterName = "@rack_cd";
                __sparam[1].Value = rack_cd;

                __ds = cls.ExecuteDataSet(__sql, __sparam);
                __tbl_cnt = __ds.Tables.Count;
                __row_cnt = __ds.Tables[0].Rows.Count;

                if (__tbl_cnt > 0 && __row_cnt > 0)
                {
                    for (int i = 1; i <= __row_cnt; i++)
                    {
                        cell_used = __ds.Tables[0].Rows[i - 1][8].ToString();
                        cell_free = __ds.Tables[0].Rows[i - 1][11].ToString();
                        cell_item = __ds.Tables[0].Rows[i - 1][14].ToString();

                        _cell_used = (cell_used.ToLower() == "true") ? true : false;
                        _cell_free = (cell_free.ToLower() == "true") ? true : false;
                        _cell_item = (cell_item.Length > 0) ? Convert.ToInt32(cell_item) : 0;

                        Label
                            lbl_title = (Label)cls.FindControlRecursive(pnl_rack, "lbl_title_" + i),
                            lbl_cell = (Label)cls.FindControlRecursive(pnl_rack, "lbl_cell_" + i),
                            lbl_loc = (Label)cls.FindControlRecursive(pnl_rack, "lbl_loc_" + i);

                        lbl_title.Text = String.Format("{0}-{1}", __rack_cd, i);

                        //lbl_cell.Text = String.Format("{0}\r\n({1})", i, _cell_item);
                        lbl_cell.Text = String.Format("{0}", _cell_item);
                        lbl_cell.Enabled = true;

                        lbl_title.BackColor = Color.Gainsboro;
                        lbl_cell.BackColor = (!_cell_used) ? Color.Gainsboro : Color.LightYellow;
                        lbl_loc.BackColor = Color.Silver;

                        //lbl_cell.BackColor = Color.Gainsboro;
                    }
                }
            }
            catch { }
            finally { }
        }

        private void Fnc_Load_Cell_Details(object sender, EventArgs e)
        {
            string whs_idx = __whs_idx;
            Label lbl_click = (Label)sender;
            string lbl_nm = lbl_click.Name.Replace("_cell_", "_title_");
            Label lbl_rack_cell = (Label)cls.FindControlRecursive(pnl_rack, lbl_nm);
            string rack_cell = lbl_rack_cell.Text;
            string rack_cd = __rack_cd;
            string cell_no = rack_cell.Replace(rack_cd + "-", "");

            //MessageBox.Show(rack_cd + " | " + cell_no);

            __timer_main.Stop();

            uc_Warehouse_Racks_Locations_Layout_Same_Details same_cell = new uc_Warehouse_Racks_Locations_Layout_Same_Details(whs_idx, rack_cd, cell_no);
            same_cell.ShowDialog();

            if (chk_autorun.Checked)
            {
                __timer_main.Start();
            }
        }

        /**************************************/

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
                    Fnc_Load_Rack_Locate_Init();

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
            Fnc_Load_Rack_Locate_Init();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            Fnc_Load_Rack_Letter_Next();
            Fnc_Load_Rack_Locate_Init();
        }
    }
}
