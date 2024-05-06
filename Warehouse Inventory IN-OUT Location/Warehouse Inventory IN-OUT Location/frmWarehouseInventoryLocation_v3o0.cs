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
    public partial class frmWarehouseInventoryLocation_v3o0 : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __app = cls.appName();

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
            __rack_row = 3,
            __rack_a_col = 4,
            __rack_b_col = 8,
            __rack_c_col = 5;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public frmWarehouseInventoryLocation_v3o0()
        {
            InitializeComponent();

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;
        }

        private void frmWarehouseInventoryLocation_v3o0_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /***************************************/

        public void Fnc_Load_Controls()
        {
            Fnc_Load_Rack_Init("A");
            Fnc_Load_Rack_Init("B");
            Fnc_Load_Rack_Init("C");
        }

        public void Fnc_Load_Rack_Init(string rack_letter)
        {
            string
                rack_locate = "",
                data_locate = "";

            try
            {
                __sql = "V2o1_BASE_Warehouse_Rack_Locate_Item_SelItem_Addnew_v3o0";
                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;

                if (__tbl_cnt > 0)
                {
                    DataView
                        view_a = new DataView(__dt),
                        view_b = new DataView(__dt),
                        view_c = new DataView(__dt);

                    switch (rack_letter.ToUpper())
                    {
                        case "A":
                            view_a.RowFilter = "[rack]='A'";

                            foreach(DataRowView row_view in view_a)
                            {
                                for(int i_row = 0; i_row < view_a.Table.Columns.Count; i_row++)
                                {
                                    data_locate = row_view[10].ToString().ToUpper();

                                    for (int a_row = 1; a_row <= __rack_row; a_row++)
                                    {
                                        for (int a_col = 1; a_col <= __rack_a_col; a_col++)
                                        {
                                            rack_locate = String.Format("{0}-{1}{2}", rack_letter.ToUpper(), a_row, a_col).ToUpper();

                                            Label cell_a = (Label)cls.FindControlRecursive(pnl_main, "lbl_A_" + a_row + "_" + a_col);


                                            cell_a.Text = (data_locate == rack_locate) ? String.Format("{0}-{1}{2}", rack_letter.ToUpper(), a_row, a_col) : "";
                                            cell_a.BackColor = (cell_a.Text.Length > 0) ? Color.FromKnownColor(KnownColor.Control) : Color.Silver;
                                        }
                                    }
                                }
                            }

                            //for (int a_row = 1; a_row <= __rack_row; a_row++)
                            //{
                            //    for (int a_col = 1; a_col <= __rack_a_col; a_col++)
                            //    {
                            //        Label cell_a = (Label)cls.FindControlRecursive(pnl_main, "lbl_A_" + a_row + "_" + a_col);

                            //        cell_a.Text = String.Format("{0}-{1}{2}", rack_letter.ToUpper(), a_row, a_col);
                            //    }
                            //}
                            break;
                        case "B":
                            view_b.RowFilter = "[rack]='B'";

                            foreach (DataRowView row_view in view_b)
                            {
                                for (int i_row = 0; i_row < view_b.Table.Columns.Count; i_row++)
                                {
                                    data_locate = row_view[10].ToString().ToUpper();

                                    for (int b_row = 1; b_row <= __rack_row; b_row++)
                                    {
                                        for (int b_col = 1; b_col <= __rack_b_col; b_col++)
                                        {
                                            rack_locate = String.Format("{0}-{1}{2}", rack_letter.ToUpper(), b_row, b_col).ToUpper();

                                            Label cell_b = (Label)cls.FindControlRecursive(pnl_main, "lbl_B_" + b_row + "_" + b_col);


                                            cell_b.Text = (data_locate == rack_locate) ? String.Format("{0}-{1}{2}", rack_letter.ToUpper(), b_row, b_col) : "";
                                            cell_b.BackColor = (cell_b.Text.Length > 0) ? Color.FromKnownColor(KnownColor.Control) : Color.Silver;
                                        }
                                    }
                                }
                            }

                            //for (int b_row = 1; b_row <= __rack_row; b_row++)
                            //{
                            //    for (int b_col = 1; b_col <= __rack_b_col; b_col++)
                            //    {
                            //        Label cell_b = (Label)cls.FindControlRecursive(pnl_main, "lbl_B_" + b_row + "_" + b_col);

                            //        cell_b.Text = String.Format("{0}-{1}{2}", rack_letter.ToUpper(), b_row, b_col);
                            //    }
                            //}
                            break;
                        case "C":
                            view_c.RowFilter = "[rack]='C'";

                            foreach (DataRowView row_view in view_c)
                            {
                                for (int i_row = 0; i_row < view_c.Table.Columns.Count; i_row++)
                                {
                                    data_locate = row_view[10].ToString().ToUpper();

                                    for (int c_row = 1; c_row <= __rack_row; c_row++)
                                    {
                                        for (int c_col = 1; c_col <= __rack_c_col; c_col++)
                                        {
                                            rack_locate = String.Format("{0}-{1}{2}", rack_letter.ToUpper(), c_row, c_col).ToUpper();

                                            Label cell_c = (Label)cls.FindControlRecursive(pnl_main, "lbl_C_" + c_row + "_" + c_col);


                                            cell_c.Text = (data_locate == rack_locate) ? String.Format("{0}-{1}{2}", rack_letter.ToUpper(), c_row, c_col) : "";
                                            cell_c.BackColor = (cell_c.Text.Length > 0) ? Color.FromKnownColor(KnownColor.Control) : Color.Silver;
                                        }
                                    }
                                }
                            }

                            //for (int c_row = 1; c_row <= __rack_row; c_row++)
                            //{
                            //    for (int c_col = 1; c_col <= __rack_c_col; c_col++)
                            //    {
                            //        Label cell_c = (Label)cls.FindControlRecursive(pnl_main, "lbl_C_" + c_row + "_" + c_col);

                            //        cell_c.Text = String.Format("{0}-{1}{2}", rack_letter.ToUpper(), c_row, c_col);
                            //    }
                            //}
                            break;
                    }
                }
            }
            catch { }
            finally { }

        }

        /***************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            if (__now_sec == 0 || __now_sec % 10 == 0)
            {
                Fnc_Load_Rack_Init("A");
                Fnc_Load_Rack_Init("B");
                Fnc_Load_Rack_Init("C");
            }
        }
    }
}
