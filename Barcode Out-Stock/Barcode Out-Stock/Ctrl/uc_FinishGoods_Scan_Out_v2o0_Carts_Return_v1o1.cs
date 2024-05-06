using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data.Ctrl
{
    public partial class uc_FinishGoods_Scan_Out_v2o0_Carts_Return_v1o1 : Form
    {
        string
            __sql = "",
            __msg = "";

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

        public uc_FinishGoods_Scan_Out_v2o0_Carts_Return_v1o1()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(dgv_return_today, true);
            cls.SetDoubleBuffer(dgv_all_available, true);
            cls.SetDoubleBuffer(dgv_too_long, true);
        }

        private void uc_FinishGoods_Scan_Out_v2o0_Carts_Return_v1o1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void uc_FinishGoods_Scan_Out_v2o0_Carts_Return_v1o1_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /**************************************/

        public void Fnc_Load_Controls()
        {
            txt_code.Text = txt_filter_01.Text = txt_filter_02.Text = "";
            txt_code.Enabled = true;
            txt_filter_01.Enabled = txt_filter_02.Enabled = false;
            dgv_return_today.DataSource = dgv_all_available.DataSource = dgv_too_long.DataSource = null;

            Fnc_Load_Carts_Return_Today();
            Fnc_Load_Carts_Available();
            Fnc_Load_Cart_Not_Come_Back_Yet();
            Fnc_Load_Carts_Not_Return_Over_5_Days();

            txt_code.Focus();
        }

        public void Fnc_Load_Carts_Return_Today()
        {
            try
            {
                __sql = "ERP_Delivery_Order_Carts_Return_Today_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_return_today.DataSource = __dt;

                dgv_return_today.Columns[0].FillWeight = 20;      //No
                //dgv_return_today.Columns[1].FillWeight = 10;      //cart_idx
                dgv_return_today.Columns[2].FillWeight = 80;      //cart_cd
                //dgv_return_today.Columns[3].FillWeight = 10;      //cart_grade
                //dgv_return_today.Columns[4].FillWeight = 10;      //cart_hist_idx
                //dgv_return_today.Columns[5].FillWeight = 10;      //cart_act
                //dgv_return_today.Columns[6].FillWeight = 10;      //cart_act_dt

                dgv_return_today.Columns[0].Visible = true;
                dgv_return_today.Columns[1].Visible = false;
                dgv_return_today.Columns[2].Visible = true;
                dgv_return_today.Columns[3].Visible = false;
                dgv_return_today.Columns[4].Visible = false;
                dgv_return_today.Columns[5].Visible = false;
                dgv_return_today.Columns[6].Visible = false;

                dgv_return_today.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                cls.fnFormatDatagridview_FullWidth(dgv_return_today, 12, 30);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Carts_Available()
        {
            try
            {
                __sql = "ERP_Delivery_Order_Carts_Return_Available_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_all_available.DataSource = __dt;

                dgv_all_available.Columns[0].FillWeight = 15;      //No
                //dgv_all_available.Columns[1].FillWeight = 15;      //idx
                dgv_all_available.Columns[2].FillWeight = 70;      //cart_cd
                dgv_all_available.Columns[3].FillWeight = 15;      //cart_grade

                dgv_all_available.Columns[0].Visible = true;
                dgv_all_available.Columns[1].Visible = false;
                dgv_all_available.Columns[2].Visible = true;
                dgv_all_available.Columns[3].Visible = true;

                cls.fnFormatDatagridview_FullWidth(dgv_all_available, 12, 30);

                txt_filter_01.Text = "";
                txt_filter_01.Enabled = (__tbl_cnt > 0) ? true : false;
                lbl_title_cart_avail.Text = String.Format("Xe có sẵn trên hệ thống ({0})", __tbl_cnt);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Carts_Available_Color()
        {
            string grade = "";

            Color bg_color = Color.Transparent;

            try
            {
                foreach(DataGridViewRow row in dgv_all_available.Rows)
                {
                    grade = row.Cells[3].Value.ToString();

                    switch (grade)
                    {
                        case "A":
                            bg_color = Color.LightGreen;
                            break;
                        case "B":
                            bg_color = Color.Yellow;
                            break;
                        case "C":
                            bg_color = Color.Gold;
                            break;
                        case "D":
                            bg_color = Color.LightPink;
                            break;
                        case "Broken":
                            bg_color = Color.Tomato;
                            break;
                    }

                    row.DefaultCellStyle.BackColor = bg_color;
                }

                dgv_all_available.ClearSelection();
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Cart_Not_Come_Back_Yet()
        {
            try
            {
                __sql = "ERP_Delivery_Order_Carts_Return_Not_Back_Yet_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_cart_out.DataSource = __dt;

                dgv_cart_out.Columns[0].FillWeight = 10;      //No
                dgv_cart_out.Columns[1].FillWeight = 45;      //cart_cd
                dgv_cart_out.Columns[2].FillWeight = 25;      //cart_act_dt
                //dgv_cart_out.Columns[3].FillWeight = 20;      //Out days
                dgv_cart_out.Columns[4].FillWeight = 20;      //delivery_to

                dgv_cart_out.Columns[0].Visible = true;
                dgv_cart_out.Columns[1].Visible = true;
                dgv_cart_out.Columns[2].Visible = true;
                dgv_cart_out.Columns[3].Visible = false;
                dgv_cart_out.Columns[4].Visible = true;

                dgv_cart_out.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
                cls.fnFormatDatagridview_FullWidth(dgv_cart_out, 12, 30);

                txt_filter_03.Text = "";
                txt_filter_03.Enabled = (__tbl_cnt > 0) ? true : false;
                lbl_title_cart_out.Text = String.Format("Xe giao hàng đi chưa về ({0})", __tbl_cnt);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Carts_Not_Return_Over_5_Days()
        {
            try
            {
                __sql = "ERP_Delivery_Order_Carts_Return_Not_In_5_Days_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_too_long.DataSource = __dt;

                dgv_too_long.Columns[0].FillWeight = 10;      //No
                dgv_too_long.Columns[1].FillWeight = 50;      //cart_cd
                //dgv_too_long.Columns[2].FillWeight = 30;      //cart_act_dt
                dgv_too_long.Columns[3].FillWeight = 20;      //Out days
                dgv_too_long.Columns[4].FillWeight = 20;      //delivery_to

                dgv_too_long.Columns[0].Visible = true;
                dgv_too_long.Columns[1].Visible = true;
                dgv_too_long.Columns[2].Visible = false;
                dgv_too_long.Columns[3].Visible = true;
                dgv_too_long.Columns[4].Visible = true;

                cls.fnFormatDatagridview_FullWidth(dgv_too_long, 12, 30);

                txt_filter_02.Text = "";
                txt_filter_02.Enabled = (__tbl_cnt > 0) ? true : false;
                lbl_title_too_long.Text = String.Format("Xe đi quá 5 ngày chưa về ({0})", __tbl_cnt);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Cart_Return_Code(KeyEventArgs e)
        {
            string
                full_code = "",
                type = "",
                kind = "",
                code = "";

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    full_code = txt_code.Text.Trim();

                    if (full_code.Length >= 20)
                    {
                        type = full_code.Substring(0, 3);
                        kind = full_code.Substring(4, 3);
                        code = full_code.Substring(8);

                        if (type == "DAV" && kind == "CAR")
                        {
                            __sql = "ERP_Delivery_Order_Carts_Return_SelItem_V1o0_Addnew";

                            __sparam = new SqlParameter[1];

                            __sparam[0] = new SqlParameter();
                            __sparam[0].SqlDbType = SqlDbType.NVarChar;
                            __sparam[0].ParameterName = "@cart_cd";
                            __sparam[0].Value = full_code;

                            cls.fnUpdDel(__sql, __sparam);

                            Fnc_Load_Carts_Return_Today();
                            Fnc_Load_Carts_Available();
                            Fnc_Load_Cart_Not_Come_Back_Yet();
                            Fnc_Load_Carts_Not_Return_Over_5_Days();
                        }

                        txt_code.Text = "";
                        txt_code.Focus();
                    }
                }
            }
            catch { }
            finally { }
        }

        /**************************************/

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            Fnc_Load_Scan_Cart_Return_Code(e);
        }

        private void txt_filter_01_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_all_available, txt_filter_01, 2);
        }

        private void txt_filter_02_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_too_long, txt_filter_02, 1);
        }

        private void dgv_all_available_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Fnc_Load_Carts_Available_Color();
        }

        private void dgv_all_available_Sorted(object sender, EventArgs e)
        {
            Fnc_Load_Carts_Available_Color();
        }
    }
}
