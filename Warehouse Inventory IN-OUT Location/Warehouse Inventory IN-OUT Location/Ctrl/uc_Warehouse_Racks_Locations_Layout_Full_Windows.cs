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
    public partial class uc_Warehouse_Racks_Locations_Layout_Full_Windows : Form
    {
        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_idx = "",
            __rack_cd = "",
            __rack_char = "";

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
            __col_cnt = 0;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public uc_Warehouse_Racks_Locations_Layout_Full_Windows()
        {
            InitializeComponent();
        }

        public uc_Warehouse_Racks_Locations_Layout_Full_Windows(string whs_idx, string rack_cd)
        {
            InitializeComponent();

            __whs_idx = whs_idx;
            __rack_cd = rack_cd;

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_cover, true);
        }

        private void uc_Warehouse_Racks_Locations_Layout_Full_Windows_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        private void uc_Warehouse_Racks_Locations_Layout_Full_Windows_KeyDown(object sender, KeyEventArgs e)
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

            Fnc_Load_Rack_Locate_Init();

            //for (int i = 1; i <= 30; i++)
            //{
            //    Label lbl_cell = (Label)cls.FindControlRecursive(pnl_rack, "lbl_cell_" + i);

            //    //lbl_cell.Text = String.Format("{0}", i);
            //    lbl_cell.Text = "";
            //    lbl_cell.BackColor = Color.LightGray;

            //    if (i <= 15)
            //    {
            //        Label lbl_loc = (Label)cls.FindControlRecursive(pnl_rack, "lbl_loc_" + i);

            //        //lbl_loc.Text = String.Format("L{0}", i);
            //        lbl_loc.Text = "";
            //        lbl_loc.BackColor = Color.Silver;
            //    }
            //}

        }

        public void Fnc_Load_Rack_Locate_Init()
        {
            string
                whs_idx = __whs_idx,
                rack_cd = __rack_cd;

            try
            {
                //for (int i = 1; i <= 30; i++)
                //{
                //    Label lbl_cell = (Label)cls.FindControlRecursive(pnl_rack, "lbl_cell_" + i);
                //    lbl_cell.Text = "";
                //    lbl_cell.Enabled = false;
                //    lbl_cell.BackColor = Color.FromKnownColor(KnownColor.Control);

                //    if (i <= 15)
                //    {
                //        Label lbl_loc = (Label)cls.FindControlRecursive(pnl_rack, "lbl_loc_" + i);
                //        lbl_loc.Text = "";
                //        lbl_loc.BackColor = Color.Gainsboro;
                //    }
                //}

                for (int i = 1; i <= 30; i++)
                {
                    Label
                        lbl_title = (Label)cls.FindControlRecursive(pnl_rack, "lbl_title_" + i),
                        lbl_cell = (Label)cls.FindControlRecursive(pnl_rack, "lbl_cell_" + i),
                        lbl_loc = (Label)cls.FindControlRecursive(pnl_rack, "lbl_loc_" + i);

                    lbl_title.Text = "";

                    lbl_cell.Text = "";
                    lbl_cell.Enabled = false;

                    //lbl_loc.Text = String.Format("{0}-{1}", __rack_cd, i);
                    lbl_loc.Text = "";
                    lbl_title.BackColor = lbl_cell.BackColor = lbl_loc.BackColor = Color.DarkGray;

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

                        lbl_title.Text = String.Format("{0}-{1}", rack_cd, i);
                        lbl_title.BackColor = Color.Gainsboro;

                        //lbl_cell.Text = String.Format("{0}\r\n({1})", i, _cell_item);
                        lbl_cell.Text = String.Format("{0} item(s)", _cell_item);
                        lbl_cell.Enabled = true;
                        lbl_title.BackColor = (!_cell_used) ? Color.Gainsboro : Color.LemonChiffon;
                        lbl_cell.BackColor = (!_cell_used) ? Color.Gainsboro : Color.LightYellow;

                        lbl_loc.BackColor = Color.Gray;

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

            uc_Warehouse_Racks_Locations_Layout_Same_Details same_cell = new uc_Warehouse_Racks_Locations_Layout_Same_Details(whs_idx, rack_cd, cell_no);
            same_cell.ShowDialog();
        }

        /**************************************/
    }
}
