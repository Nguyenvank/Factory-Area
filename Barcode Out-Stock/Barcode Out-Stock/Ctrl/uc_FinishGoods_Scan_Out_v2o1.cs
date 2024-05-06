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
    public partial class uc_FinishGoods_Scan_Out_v2o1 : UserControl
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __prod_nm = "",
            __prod_cd = "",
            __delivery_to = "";


        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null;

        DataSet
            __ds = null;

        int
            __do_idx = 0,
            __do_item_idx = 0,
            __prod_idx = 0,
            __box_idx = 0,
            __cart_idx = 0,
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __clear_goods_cd_cnt = 0,
            __clear_carts_cd_cnt = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        double
            __total_qty = 0,
            __total_out = 0,
            __total_rate = 0;

        bool
            __goods_cd_ok = false,
            __carts_cd_ok = false,
            __clear_goods_cd = false,
            __clear_carts_cd = false,
            __display_sub_form = false;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public uc_FinishGoods_Scan_Out_v2o1()
        {
            InitializeComponent();

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_code, true);
            cls.SetDoubleBuffer(tlp_total, true);
            cls.SetDoubleBuffer(tlp_header, true);
            cls.SetDoubleBuffer(dgv_order, true);
            cls.SetDoubleBuffer(dgv_scan, true);
            cls.SetDoubleBuffer(dgv_cart_out, true);
            cls.SetDoubleBuffer(dgv_cart_return, true);
        }

        private void uc_FinishGoods_Scan_Out_v2o0_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /***********************************/

        public void Fnc_Load_Controls()
        {
            dgv_order.DataSource =
                dgv_scan.DataSource =
                dgv_cart_out.DataSource =
                dgv_cart_return.DataSource = null;

            lbl_title_cart_cd.Text = "MÃ XE GIAO HÀNG";
            lbl_title_good_cd.Text = "TEM HÀNG THÀNH PHẨM";
            lbl_title_cart_out.Text = "XE ĐÃ XUẤT HÀNG (0)";
            lbl_title_cart_return.Text = "XE ĐÃ QUAY VỀ (0)";
            lbl_cart_cd.Text = lbl_good_cd.Text = "-";
            lbl_cart_cd.BackColor = lbl_good_cd.BackColor = Color.Gainsboro;
            txt_code.Text = "";
            tlp_code.Enabled = txt_code.Enabled = false;
            tlp_code.BackColor = txt_code.BackColor = Color.Gainsboro;
            lbl_item_nm.Text = lbl_item_cd.Text = "";
            lbl_qty_order.Text = lbl_qty_out.Text = "0 ea";
            lbl_qty_out_percent.Text = "0.0%";

            Fnc_Load_Delivery_Order_List();
            Fnc_Load_Scan_Item_List();
            Fnc_Load_Carts_OUT_List();
            Fnc_Load_Carts_IN_List();
        }

        public void Fnc_Load_Delivery_Order_List()
        {
            try
            {
                __sql = "ERP_Delivery_Order_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_order.DataSource = __dt;

                dgv_order.Columns[0].FillWeight = 10;      //No
                //dgv_order.Columns[1].FillWeight = 5;      //do_item_idx
                //dgv_order.Columns[2].FillWeight = 5;      //do_idx
                dgv_order.Columns[3].FillWeight = 45;      //goods_idx
                dgv_order.Columns[4].FillWeight = 45;      //goods_nm
                //dgv_order.Columns[5].FillWeight = 5;      //goods_cd
                dgv_order.Columns[6].FillWeight = 15;      //do_qty
                dgv_order.Columns[7].FillWeight = 10;      //do_unit
                dgv_order.Columns[8].FillWeight = 20;      //ship_to
                //dgv_order.Columns[9].FillWeight = 5;      //do_remain
                //dgv_order.Columns[10].FillWeight = 5;      //do_finish
                //dgv_order.Columns[11].FillWeight = 5;      //do_note
                //dgv_order.Columns[12].FillWeight = 5;      //do_dt
                //dgv_order.Columns[13].FillWeight = 5;      //scan_out
                //dgv_order.Columns[14].FillWeight = 5;      //rate_out

                dgv_order.Columns[0].Visible = true;
                dgv_order.Columns[1].Visible = false;
                dgv_order.Columns[2].Visible = false;
                dgv_order.Columns[3].Visible = false;
                dgv_order.Columns[4].Visible = true;
                dgv_order.Columns[5].Visible = false;
                dgv_order.Columns[6].Visible = true;
                dgv_order.Columns[7].Visible = true;
                dgv_order.Columns[8].Visible = true;
                dgv_order.Columns[9].Visible = false;
                dgv_order.Columns[10].Visible = false;
                dgv_order.Columns[11].Visible = false;
                dgv_order.Columns[12].Visible = false;
                dgv_order.Columns[13].Visible = false;
                dgv_order.Columns[14].Visible = false;

                dgv_order.Columns[12].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                cls.fnFormatDatagridviewColor_FullWidth(dgv_order, 15, 35, Color.White, BorderStyle.None);
                dgv_order.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Delivery_Order_List_Color()
        {
            int
                scan_out = 0,
                dgv_rows = dgv_order.Rows.Count;

            Color
                bg_color = Color.Transparent;

            try
            {
                if (dgv_rows > 0)
                {
                    foreach(DataGridViewRow row in dgv_order.Rows)
                    {
                        scan_out = int.Parse(row.Cells[13].Value.ToString());

                        bg_color = (scan_out == 0) ? Color.White : Color.LightYellow;

                        row.Cells[0].Style.BackColor = bg_color;
                    }

                    dgv_order.ClearSelection();
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Delivery_Order_List_Click(DataGridViewCellEventArgs e, byte click)
        {
            try
            {
                __total_qty = __total_out = __total_rate = 0;

                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    cls.fnDatagridClickCell(dgv_order, e);

                    DataGridViewRow row = new DataGridViewRow();
                    row = dgv_order.Rows[e.RowIndex];

                    __do_item_idx = int.Parse(row.Cells[1].Value.ToString());
                    __do_idx = int.Parse(row.Cells[2].Value.ToString());
                    __prod_idx = int.Parse(row.Cells[3].Value.ToString());
                    __prod_nm = row.Cells[4].Value.ToString();
                    __prod_cd = row.Cells[5].Value.ToString();
                    __total_qty = double.Parse(row.Cells[6].Value.ToString());
                    __delivery_to = row.Cells[8].Value.ToString();

                    lbl_item_nm.Text = String.Format("{0}", __prod_nm.ToUpper());
                    lbl_item_cd.Text = String.Format("{0}", __prod_cd);
                    lbl_qty_order.Text = String.Format("{0} ea", __total_qty);

                    Fnc_Load_Scan_Item_List();
                    Fnc_Load_Scan_Item_Sum();

                    if (__total_out < __total_qty)
                    {
                        lbl_cart_cd.Text = lbl_good_cd.Text = "-";
                        lbl_cart_cd.BackColor = lbl_good_cd.BackColor = Color.Gainsboro;
                        txt_code.Text = "";
                        tlp_code.Enabled = txt_code.Enabled = true;
                        tlp_code.BackColor = txt_code.BackColor = Color.White;
                        txt_code.Focus();
                    }
                    else
                    {
                        lbl_cart_cd.Text = lbl_good_cd.Text = "-";
                        txt_code.Text = "";
                        tlp_code.Enabled = txt_code.Enabled = false;
                        tlp_code.BackColor = txt_code.BackColor = Color.Gainsboro;
                    }

                    if (click == 2)
                    {
                        Fnc_Load_Delivery_Order_List();
                    }
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Item_Sum()
        {
            try
            {
                if (dgv_scan.Rows.Count > 0)
                {
                    foreach(DataGridViewRow row in dgv_scan.Rows)
                    {
                        __total_out += double.Parse(row.Cells[4].Value.ToString());
                    }

                    __total_rate = __total_out / __total_qty * 100;
                }
                else
                {
                    __total_out = __total_rate = 0;
                }

                lbl_qty_out.Text = String.Format("{0} ea", __total_out);
                lbl_qty_out_percent.Text = String.Format("{0:0.0}%", __total_rate);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Item_List()
        {
            int
                do_idx = __do_idx,
                do_item_idx = __do_item_idx,
                prod_idx = __prod_idx;

            try
            {
                __sql = "ERP_Delivery_Order_Scan_Out_List_SelItem_V1o0_Addnew";

                __sparam = new SqlParameter[2];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@do_idx";
                __sparam[0].Value = do_idx;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.Int;
                __sparam[1].ParameterName = "@prod_idx";
                __sparam[1].Value = prod_idx;

                __dt = cls.ExecuteDataTable(__sql, __sparam);
                __tbl_cnt = __dt.Rows.Count;
                dgv_scan.DataSource = __dt;

                dgv_scan.Columns[0].FillWeight = 5;      //No
                //dgv_scan.Columns[1].FillWeight = 10;      //box_idx
                dgv_scan.Columns[2].FillWeight = 23;      //Packing
                //dgv_scan.Columns[3].FillWeight = 8;      //LOT
                dgv_scan.Columns[4].FillWeight = 10;      //Q'ty
                //dgv_scan.Columns[5].FillWeight = 10;      //prod_idx
                //dgv_scan.Columns[6].FillWeight = 10;      //prod_nm
                //dgv_scan.Columns[7].FillWeight = 10;      //prod_cd
                //dgv_scan.Columns[8].FillWeight = 14;      //IN_Date
                dgv_scan.Columns[9].FillWeight = 22;      //OUT_Date
                //dgv_scan.Columns[10].FillWeight = 10;      //orderIDx
                dgv_scan.Columns[11].FillWeight = 17;      //shipTo
                dgv_scan.Columns[12].FillWeight = 23;      //cart_cd

                dgv_scan.Columns[0].Visible = true;
                dgv_scan.Columns[1].Visible = false;
                dgv_scan.Columns[2].Visible = true;
                dgv_scan.Columns[3].Visible = false;
                dgv_scan.Columns[4].Visible = true;
                dgv_scan.Columns[5].Visible = false;
                dgv_scan.Columns[6].Visible = false;
                dgv_scan.Columns[7].Visible = false;
                dgv_scan.Columns[8].Visible = false;
                dgv_scan.Columns[9].Visible = true;
                dgv_scan.Columns[10].Visible = false;
                dgv_scan.Columns[11].Visible = true;
                dgv_scan.Columns[12].Visible = true;

                dgv_scan.Columns[8].DefaultCellStyle.Format = dgv_scan.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                cls.fnFormatDatagridviewColor_FullWidth(dgv_scan, 13, 35, Color.White, BorderStyle.None);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Carts_OUT_List()
        {
            try
            {
                __sql = "ERP_Delivery_Order_Carts_Out_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_cart_out.DataSource = __dt;

                dgv_cart_out.Columns[0].FillWeight = 5;      //No
                dgv_cart_out.Columns[1].FillWeight = 30;      //cart_cd
                dgv_cart_out.Columns[2].FillWeight = 10;      //cart_grade
                //dgv_cart_out.Columns[3].FillWeight = 30;      //prod_nm
                //dgv_cart_out.Columns[4].FillWeight = 5;      //prod_cd
                dgv_cart_out.Columns[5].FillWeight = 15;      //delivery_to
                dgv_cart_out.Columns[6].FillWeight = 25;      //cart_act_dt

                dgv_cart_out.Columns[0].Visible = true;
                dgv_cart_out.Columns[1].Visible = true;
                dgv_cart_out.Columns[2].Visible = true;
                dgv_cart_out.Columns[3].Visible = false;
                dgv_cart_out.Columns[4].Visible = false;
                dgv_cart_out.Columns[5].Visible = true;
                dgv_cart_out.Columns[6].Visible = true;

                dgv_cart_out.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                cls.fnFormatDatagridviewColor_FullWidth(dgv_cart_out, 10, 30, Color.WhiteSmoke, BorderStyle.None);

                lbl_title_cart_out.Text = String.Format("XE ĐÃ XUẤT HÀNG ({0})", __tbl_cnt);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Carts_IN()
        {
            try
            {
                __display_sub_form = true;
                uc_FinishGoods_Scan_Out_v2o0_Carts_Return_v1o1 cart_return = new uc_FinishGoods_Scan_Out_v2o0_Carts_Return_v1o1();
                cart_return.ShowDialog();
                __display_sub_form = false;

                Fnc_Load_Carts_OUT_List();
                Fnc_Load_Carts_IN_List();
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Carts_IN_List()
        {
            try
            {
                __sql = "ERP_Delivery_Order_Carts_Return_Today_List_SelItem_V1o0_Addnew";

                __dt = cls.ExecuteDataTable(__sql);
                __tbl_cnt = __dt.Rows.Count;
                dgv_cart_return.DataSource = __dt;

                dgv_cart_return.Columns[0].FillWeight = 20;      //No
                //dgv_cart_return.Columns[1].FillWeight = 10;      //cart_idx
                dgv_cart_return.Columns[2].FillWeight = 80;      //cart_cd
                //dgv_cart_return.Columns[3].FillWeight = 10;      //cart_grade
                //dgv_cart_return.Columns[4].FillWeight = 10;      //cart_hist_idx
                //dgv_cart_return.Columns[5].FillWeight = 10;      //cart_act
                //dgv_cart_return.Columns[6].FillWeight = 10;      //cart_act_dt

                dgv_cart_return.Columns[0].Visible = true;
                dgv_cart_return.Columns[1].Visible = false;
                dgv_cart_return.Columns[2].Visible = true;
                dgv_cart_return.Columns[3].Visible = false;
                dgv_cart_return.Columns[4].Visible = false;
                dgv_cart_return.Columns[5].Visible = false;
                dgv_cart_return.Columns[6].Visible = false;

                dgv_cart_return.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                cls.fnFormatDatagridviewColor_FullWidth(dgv_cart_return, 10, 30, Color.White, BorderStyle.None);

                lbl_title_cart_return.Text = String.Format("XE ĐÃ QUAY VỀ ({0})", __tbl_cnt);
            }
            catch { }
            finally { }

        }

        public void Fnc_Load_Scan_Code(KeyEventArgs e)
        {
            string
                full_code = "",
                type = "",
                kind = "",
                code = "";

            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    full_code = txt_code.Text.Trim();
                    type = full_code.Substring(0, 3);
                    kind = full_code.Substring(4, 3);
                    code = full_code.Substring(8);

                    if (full_code.Length >= 20)
                    {
                        switch (type.ToLower())
                        {
                            case "pro":
                                Fnc_Load_Scan_Code_PRO(__prod_idx, full_code);
                                break;
                            case "dav":
                                Fnc_Load_Scan_Code_DAV(full_code);
                                break;
                        }

                        if (__goods_cd_ok && __carts_cd_ok)
                        {
                            Fnc_Load_Scan_Code_Save();

                            __goods_cd_ok = __carts_cd_ok = false;
                            lbl_cart_cd.Text = lbl_good_cd.Text = "-";
                            lbl_cart_cd.BackColor = lbl_good_cd.BackColor = Color.Gainsboro;

                            Fnc_Load_Scan_Item_List();
                            Fnc_Load_Carts_OUT_List();
                        }
                    }
                    else
                    {
                        __msg = "";
                    }

                    txt_code.Text = "";
                    txt_code.Focus();
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Code_PRO(int prod_idx, string goods_cd)
        {
            string
                right_nm = "";

            bool
                box_exist = false,
                right_idx = false,
                box_stock = false;

            DateTime
                out_stock = DateTime.Now;

            Color bg_color = Color.Transparent;

            int 
                warning_cd = 0;

            try
            {
                __msg = "";
                __box_idx = 0;
                __goods_cd_ok = false;

                __sql = "ERP_Delivery_Order_Scan_Goods_Code_SelItem_V1o0_Addnew";

                __sparam = new SqlParameter[2];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@prod_idx";
                __sparam[0].Value = prod_idx;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.VarChar;
                __sparam[1].ParameterName = "@goods_cd";
                __sparam[1].Value = goods_cd;

                __ds = cls.ExecuteDataSet(__sql, __sparam);
                __tbl_cnt = __ds.Tables.Count;
                __row_cnt = __ds.Tables[0].Rows.Count;

                if (__tbl_cnt > 0 && __row_cnt > 0)
                {
                    __box_idx = int.Parse(__ds.Tables[0].Rows[0][0].ToString());
                    box_exist = bool.Parse(__ds.Tables[0].Rows[0][3].ToString());
                    right_idx = bool.Parse(__ds.Tables[0].Rows[0][4].ToString());
                    right_nm = __ds.Tables[0].Rows[0][5].ToString();
                    box_stock = bool.Parse(__ds.Tables[0].Rows[0][6].ToString());
                    out_stock = (!box_stock) ? DateTime.Parse(__ds.Tables[0].Rows[0][7].ToString()) : DateTime.Now;

                    if (box_exist && right_idx && box_stock)
                    {
                        bg_color = Color.LightGreen;

                        __goods_cd_ok = true;
                    }
                    else
                    {
                        bg_color = Color.LightPink;

                        if (!box_exist) { __msg += "- Mã này không tồn tai trên hệ thống\r\n"; }
                        if (!right_idx) { __msg += String.Format("- Mã vừa quét {0} là của hàng {1}\r\n", goods_cd, right_nm); }
                        if (!box_stock) { __msg += String.Format("- Mã này đã được xuất đi ngày {0:dd/MM/yyyy HH:mm:ss}\r\n", out_stock); }

                        warning_cd = 1;
                    }
                }
                else
                {
                    bg_color = Color.LightPink;

                    __msg += "- Mã này không tồn tại trên hệ thống\r\n";

                    warning_cd = 2;
                }

                lbl_good_cd.Text = goods_cd;
                lbl_good_cd.BackColor = bg_color;

                if (__msg.Length > 0)
                {
                    tlp_code.BackColor = txt_code.BackColor = bg_color;
                    //MessageBox.Show(__msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0 warning = new uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0(__msg, warning_cd);
                    warning.ShowDialog();

                    tlp_code.BackColor = txt_code.BackColor = Color.White;

                    __clear_goods_cd = true;
                    __msg = "";
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Code_DAV(string cart_cd)
        {
            string
                cart_grade = "",
                delivery_to = "";

            bool
                cart_broken = false,
                cart_delivery = false,
                cart_active = false,
                cart_enable = false,
                total_ok = false;

            int
                warning_cd = 0;

            Color
                bg_color = Color.Transparent;

            try
            {
                __msg = "";
                __carts_cd_ok = false;

                __sql = "ERP_Delivery_Order_Scan_Carts_Code_SelItem_V1o0_Addnew";

                __sparam = new SqlParameter[1];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.VarChar;
                __sparam[0].ParameterName = "@cart_cd";
                __sparam[0].Value = cart_cd;

                __ds = cls.ExecuteDataSet(__sql, __sparam);
                __tbl_cnt = __ds.Tables.Count;
                __row_cnt = __ds.Tables[0].Rows.Count;

                if (__tbl_cnt > 0 && __row_cnt > 0)
                {
                    __cart_idx = int.Parse(__ds.Tables[0].Rows[0][0].ToString());
                    cart_grade = __ds.Tables[0].Rows[0][2].ToString();
                    cart_broken = bool.Parse(__ds.Tables[0].Rows[0][3].ToString());
                    cart_delivery = bool.Parse(__ds.Tables[0].Rows[0][4].ToString());
                    delivery_to = __ds.Tables[0].Rows[0][5].ToString();
                    cart_active = bool.Parse(__ds.Tables[0].Rows[0][6].ToString());
                    cart_enable = bool.Parse(__ds.Tables[0].Rows[0][7].ToString());

                    total_ok = (cart_grade.ToUpper() != "D" && !cart_broken && !cart_delivery && cart_active && cart_enable) ? true : false;

                    if (total_ok)
                    {
                        bg_color = Color.LightGreen;

                        __carts_cd_ok = true;
                    }
                    else
                    {
                        bg_color = Color.LightPink;

                        if (cart_grade.ToUpper() == "D") { __msg += String.Format("- Mã xe {0} không đạt tiêu chuẩn sử dụng (đang ở mức D)\r\n", cart_cd); }
                        if (cart_broken) { __msg += "- Xe này đang bị hỏng, hãy cập nhật lại tình trạng xe\r\n"; }
                        if (cart_delivery) { __msg += String.Format("- Xe này đã giao hàng ({0}) và chưa quay về\r\n", delivery_to); }
                        if (!cart_active || !cart_enable) { __msg += "- Mã xe đang bị tắt kích hoạt, hãy cập nhật lại để sử dụng\r\n"; }

                        warning_cd = 3;
                    }
                }
                else
                {
                    __msg = String.Format("- Mã xe {0} không tồn tại trên hệ thống", cart_cd);

                    warning_cd = 4;
                }

                lbl_cart_cd.Text = cart_cd;
                lbl_cart_cd.BackColor = bg_color;

                if (__msg.Length > 0)
                {
                    tlp_code.BackColor = txt_code.BackColor = bg_color;
                    //MessageBox.Show(__msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0 warning = new uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0(__msg, warning_cd);
                    warning.ShowDialog();

                    tlp_code.BackColor = txt_code.BackColor = Color.White;

                    __clear_carts_cd = true;
                    __msg = "";
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Code_Save()
        {
            string
                goods_cd = lbl_good_cd.Text,
                carts_cd = lbl_cart_cd.Text,
                delivery_to = __delivery_to;

            int
                order_idx = __do_idx,
                prod_idx = __prod_idx,
                box_idx = __box_idx,
                carts_idx = __cart_idx;

            try
            {
                __sql = "ERP_Delivery_Order_Scan_Save_Code_SelItem_V1o0_Addnew";

                __sparam = new SqlParameter[6];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@order_idx";
                __sparam[0].Value = order_idx;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.Int;
                __sparam[1].ParameterName = "@prod_idx";
                __sparam[1].Value = prod_idx;

                __sparam[2] = new SqlParameter();
                __sparam[2].SqlDbType = SqlDbType.Int;
                __sparam[2].ParameterName = "@box_idx";
                __sparam[2].Value = box_idx;

                __sparam[3] = new SqlParameter();
                __sparam[3].SqlDbType = SqlDbType.Int;
                __sparam[3].ParameterName = "@carts_idx";
                __sparam[3].Value = carts_idx;

                __sparam[4] = new SqlParameter();
                __sparam[4].SqlDbType = SqlDbType.VarChar;
                __sparam[4].ParameterName = "@carts_cd";
                __sparam[4].Value = carts_cd;

                __sparam[5] = new SqlParameter();
                __sparam[5].SqlDbType = SqlDbType.NVarChar;
                __sparam[5].ParameterName = "@delivery_to";
                __sparam[5].Value = delivery_to;

                cls.fnUpdDel(__sql, __sparam);
            }
            catch { }
            finally { }
        }

        /***********************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            txt_code.Focus();

            if (__clear_goods_cd)
            {
                if (__clear_goods_cd_cnt <= 4)
                {
                    __clear_goods_cd_cnt += 1;

                    if (__clear_goods_cd_cnt == 4)
                    {
                        lbl_good_cd.Text = "-";
                        lbl_good_cd.BackColor = Color.Gainsboro;

                        __clear_goods_cd = false;
                        __clear_goods_cd_cnt = 0;
                    }
                }
            }

            if (__clear_carts_cd)
            {
                if (__clear_carts_cd_cnt <= 4)
                {
                    __clear_carts_cd_cnt += 1;

                    if (__clear_carts_cd_cnt == 4)
                    {
                        lbl_cart_cd.Text = "-";
                        lbl_cart_cd.BackColor = Color.Gainsboro;

                        __clear_carts_cd = false;
                        __clear_carts_cd_cnt = 0;
                    }
                }
            }

            if (!__display_sub_form && __now_sec % 30 == 0)
            {
                Fnc_Load_Carts_OUT_List();
                Fnc_Load_Carts_IN_List();
            }
        }

        private void dgv_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Fnc_Load_Delivery_Order_List_Click(e, 1);
        }

        private void dgv_order_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fnc_Load_Delivery_Order_List_Click(e, 2);
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            Fnc_Load_Scan_Code(e);
        }

        private void dgv_order_Sorted(object sender, EventArgs e)
        {
            Fnc_Load_Delivery_Order_List_Color();
        }

        private void dgv_order_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Fnc_Load_Delivery_Order_List_Color();
        }

        private void lbl_title_cart_return_DoubleClick(object sender, EventArgs e)
        {
            Fnc_Load_Carts_IN();
        }
    }
}
