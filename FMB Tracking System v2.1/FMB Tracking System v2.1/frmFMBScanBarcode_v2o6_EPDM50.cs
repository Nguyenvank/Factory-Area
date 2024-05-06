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

namespace FMB_Tracking_System_v2._1
{
    public partial class frmFMBScanBarcode_v2o6_EPDM50 : Form
    {
        Timer
            __timer_main = new Timer(),
            __timer_cls = new Timer(),
            __timer_cnt = new Timer();

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
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0,
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __counter = 100,
            __pre_close = 20,
            __close = 60;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        bool
            __is_long_cd = false,
            __is_short_cd = false,
            __is_mmt_cd = false,
            __is_pro_cd = false;

        public frmFMBScanBarcode_v2o6_EPDM50()
        {
            InitializeComponent();

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            __timer_cnt.Interval = 50;
            __timer_cnt.Enabled = false;
            __timer_cnt.Tick += __timer_cnt_Tick;

            __timer_cls.Interval = 1000;
            __timer_cls.Enabled = false;
            __timer_cls.Tick += __timer_cls_Tick;

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_left, true);
            cls.SetDoubleBuffer(tlp_need, true);
            cls.SetDoubleBuffer(tlp_code, true);
            cls.SetDoubleBuffer(dgv_list, true);
            cls.SetDoubleBuffer(pgr_counter, true);
        }

        private void frmFMBScanBarcode_v2o6_EPDM50_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        private void frmFMBScanBarcode_v2o6_EPDM50_KeyDown(object sender, KeyEventArgs e)
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

        /***********************************/

        public void Fnc_Load_Controls()
        {
            lbl_need_1.BackColor = lbl_need_2.BackColor = lbl_need_3.BackColor = lbl_need_4.BackColor = Color.Yellow;
            tlp_code.BackColor = txt_code.BackColor = Color.White;

            txt_long_cd.Text = 
                txt_fmb_nm.Text = 
                txt_short_cd.Text = 
                txt_lot_no.Text = 
                txt_exp_dt.Text = 
                txt_batch_no.Text = 
                txt_weight_gs.Text = 
                txt_mmt_cd.Text = 
                txt_pro_cd.Text = "";

            pgr_counter.Minimum = 0;
            pgr_counter.Maximum = pgr_counter.Value = __counter;
            pgr_counter.Enabled = false;

            dgv_list.DataSource = null;

            lbl_auto_close.Visible = false;

            Fnc_Load_Scan_Status();
            Fnc_Load_Data_List();

            txt_code.Text = "";
            txt_code.Focus();

        }

        public void Fnc_Load_Data_List()
        {
            try
            {
                __sql = "FMB_Material_Packing_EPDM50_List_IN_SelItem_V1o0_Addnew";

                DataTable dt = new DataTable();
                dt = cls.fnSelect(__sql);
                dgv_list.DataSource = dt;

                dgv_list.Columns[0].FillWeight = 10;    // STT
                //dgv_list.Columns[1].FillWeight = 5;    // boxId
                dgv_list.Columns[2].FillWeight = 30;    // boxcode
                //dgv_list.Columns[3].FillWeight = 5;    // boxLOT
                //dgv_list.Columns[4].FillWeight = 15;    // boxpartname
                dgv_list.Columns[5].FillWeight = 20;    // boxpartno
                dgv_list.Columns[6].FillWeight = 15;    // boxquantity
                dgv_list.Columns[7].FillWeight = 25;    // packingdate
                //dgv_list.Columns[8].FillWeight = 25;    // rank
                //dgv_list.Columns[9].FillWeight = 25;    // cooling
                //dgv_list.Columns[10].FillWeight = 25;    // box_long_cd
                //dgv_list.Columns[11].FillWeight = 25;    // box_short_cd
                //dgv_list.Columns[12].FillWeight = 25;    // mmt_cd
                //dgv_list.Columns[13].FillWeight = 25;    // pro_cd

                //dgv_list.Columns[0].Width = 10 * _dgv_list_Width / 100;    // STT
                ////dgv_list.Columns[1].Width = 5 * _dgv_list_Width / 100;    // boxId
                //dgv_list.Columns[2].Width = 30 * _dgv_list_Width / 100;    // boxcode
                ////dgv_list.Columns[3].Width = 5 * _dgv_list_Width / 100;    // boxLOT
                ////dgv_list.Columns[4].Width = 15 * _dgv_list_Width / 100;    // boxpartname
                //dgv_list.Columns[5].Width = 20 * _dgv_list_Width / 100;    // boxpartno
                //dgv_list.Columns[6].Width = 15 * _dgv_list_Width / 100;    // boxquantity
                //dgv_list.Columns[7].Width = 25 * _dgv_list_Width / 100;    // packingdate

                dgv_list.Columns[0].Visible = true;
                dgv_list.Columns[1].Visible = false;
                dgv_list.Columns[2].Visible = true;
                dgv_list.Columns[3].Visible = false;
                dgv_list.Columns[4].Visible = false;
                dgv_list.Columns[5].Visible = true;
                dgv_list.Columns[6].Visible = true;
                dgv_list.Columns[7].Visible = true;
                dgv_list.Columns[8].Visible = false;
                dgv_list.Columns[9].Visible = false;
                dgv_list.Columns[10].Visible = false;
                dgv_list.Columns[11].Visible = false;
                dgv_list.Columns[12].Visible = false;
                dgv_list.Columns[13].Visible = false;

                dgv_list.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                cls.fnFormatDatagridview_FullWidth(dgv_list, 11, 30);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Scan_Status()
        {
            switch (__is_long_cd)
            {
                case true:
                    lbl_need_1.BackColor = (__now_sec % 2 == 0) ? Color.LightGreen : Color.LimeGreen;
                    break;
                case false:
                    lbl_need_1.BackColor = (__now_sec % 2 == 0) ? Color.Yellow : Color.Gold;
                    break;
            }

            switch (__is_short_cd)
            {
                case true:
                    lbl_need_2.BackColor = (__now_sec % 2 == 0) ? Color.LightGreen : Color.LimeGreen;
                    break;
                case false:
                    lbl_need_2.BackColor = (__now_sec % 2 == 0) ? Color.Yellow : Color.Gold;
                    break;
            }

            switch (__is_mmt_cd)
            {
                case true:
                    lbl_need_3.BackColor = (__now_sec % 2 == 0) ? Color.LightGreen : Color.LimeGreen;
                    break;
                case false:
                    lbl_need_3.BackColor = (__now_sec % 2 == 0) ? Color.Yellow : Color.Gold;
                    break;
            }

            switch (__is_pro_cd)
            {
                case true:
                    lbl_need_4.BackColor = (__now_sec % 2 == 0) ? Color.LightGreen : Color.LimeGreen;
                    break;
                case false:
                    lbl_need_4.BackColor = (__now_sec % 2 == 0) ? Color.Yellow : Color.Gold;
                    break;
            }

            __timer_cnt.Enabled = pgr_counter.Enabled = (__is_long_cd && __is_short_cd && __is_mmt_cd && __is_pro_cd) ? true : false;
        }

        public void Fnc_Load_Process_Code(string code)
        {
            string
                code_fmb_nm = "",
                code_fmb_lot = "",
                code_fmb_lot_yr = "", code_fmb_lot_mn = "", code_fmb_lot_dt = "",
                code_fmb_exp = "",
                code_fmb_batch = "",
                code_fmb_weight = "",
                same_msg = "";

            int
                lot_yrs = 0,
                lot_mon = 0,
                lot_day = 0,
                same_len = 0;

            DateTime
                _code_fmb_lot_dt = DateTime.Now,
                _code_fmb_exp_dt = DateTime.Now;

            switch (code.Substring(0, 3).ToUpper())
            {
                case "PRO":
                    same_msg = Fnc_Load_Check_Same_Code(code, 13);
                    same_len = same_msg.Length;
                    if (same_len == 0)
                    {
                        txt_pro_cd.Text = code.ToUpper();
                    }
                    else
                    {
                        __timer_cls.Stop();
                        MessageBox.Show(same_msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __timer_cls.Start();
                    }
                    break;
                case "MMT":
                    same_msg = Fnc_Load_Check_Same_Code(code, 12);
                    same_len = same_msg.Length;
                    txt_mmt_cd.Text = code.ToUpper();
                    //if (same_len == 0)
                    //{
                    //    txt_mmt_cd.Text = code.ToUpper();
                    //}
                    //else
                    //{
                    //    MessageBox.Show(same_msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    break;
                case "EPD":
                    same_msg = Fnc_Load_Check_Same_Code(code, 10);
                    same_len = same_msg.Length;
                    if (same_len == 0)
                    {
                        _code_fmb_lot_dt = _code_fmb_exp_dt = DateTime.Now;
                        txt_long_cd.Text = code;
                        code_fmb_nm = code.Substring(0, 7);
                        code_fmb_lot = code.Substring(7, 6);
                        code_fmb_lot_yr = code_fmb_lot.Substring(0, 2); lot_yrs = (code_fmb_lot_yr.Length > 0) ? Convert.ToInt32(code_fmb_lot_yr) + 2000 : 0;
                        code_fmb_lot_mn = code_fmb_lot.Substring(2, 2); lot_mon = (code_fmb_lot_mn.Length > 0) ? Convert.ToInt32(code_fmb_lot_mn) : 0;
                        code_fmb_lot_dt = code_fmb_lot.Substring(4); lot_day = (code_fmb_lot_dt.Length > 0) ? Convert.ToInt32(code_fmb_lot_dt) : 0;
                        if (lot_yrs > 0 && lot_mon > 0 && lot_day > 0)
                        {
                            _code_fmb_lot_dt = new DateTime(lot_yrs, lot_mon, lot_day, 0, 0, 0);
                            _code_fmb_exp_dt = _code_fmb_lot_dt.AddDays(15);
                        }
                        code_fmb_batch = code.Substring(18, 3);
                        code_fmb_weight = code.Substring(23);

                        txt_fmb_nm.Text = code_fmb_nm.ToUpper();
                        txt_lot_no.Text = code_fmb_lot;
                        txt_exp_dt.Text = (_code_fmb_exp_dt != DateTime.Now) ? String.Format("{0:yyMMdd}", _code_fmb_exp_dt) : "";
                        txt_batch_no.Text = code_fmb_batch.Substring(0, 1) + "-" + code_fmb_batch.Substring(1);
                        txt_weight_gs.Text = code_fmb_weight;
                    }
                    else
                    {
                        __timer_cls.Stop();
                        MessageBox.Show(same_msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __timer_cls.Start();
                    }
                    break;
                default:
                    same_msg = Fnc_Load_Check_Same_Code(code, 11);
                    same_len = same_msg.Length;
                    if (same_len == 0)
                    {
                        txt_short_cd.Text = code;
                    }
                    else
                    {
                        __timer_cls.Stop();
                        MessageBox.Show(same_msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __timer_cls.Start();
                    }
                    break;
            }

            txt_code.Text = "";
            txt_code.Focus();
        }

        public string Fnc_Load_Check_Same_Code(string scan_val, int col)
        {
            string
                msg = "",
                code_type = (scan_val.Length > 3) ? scan_val.Substring(0, 3) : "",
                same_col = "";
            int
                lst_count = dgv_list.Rows.Count,
                same_cnt = 0;

            if (lst_count > 0)
            {
                foreach(DataGridViewRow row in dgv_list.Rows)
                {
                    same_col = row.Cells[col].Value.ToString();

                    if (scan_val.ToLower() == same_col.ToLower()) { same_cnt += 1; }
                }

                if (same_cnt > 0)
                {
                    msg = "Mã này đã được quét trước đó.\r\nKiểm tra và quét lại.";
                }
            }

            if (code_type.ToUpper() == "MMT")
            {
                Fnc_Load_Save_To_History(scan_val, "OK", true);
            }
            else
            {
                Fnc_Load_Save_To_History(scan_val, (msg.Length > 0) ? msg : "OK", (msg.Length > 0) ? false : true);
            }

            return msg;
        }

        public void Fnc_Load_Save_To_History(string code, string msg, bool is_ok)
        {
            try
            {
                __sql = "FMB_Material_Packing_EPDM50_IN_History_Activities_AddItem_V1o0_Addnew";

                __sparam = new SqlParameter[3];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.VarChar;
                __sparam[0].ParameterName = "@code";
                __sparam[0].Value = code;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.NVarChar;
                __sparam[1].ParameterName = "@msg";
                __sparam[1].Value = msg;

                __sparam[2] = new SqlParameter();
                __sparam[2].SqlDbType = SqlDbType.Bit;
                __sparam[2].ParameterName = "@is_ok";
                __sparam[2].Value = is_ok;

                cls.fnUpdDel(__sql, __sparam);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Save_Data()
        {
            string
                long_cd = txt_long_cd.Text.Trim(),
                short_cd = txt_short_cd.Text.Trim(),
                mmt_cd = txt_mmt_cd.Text.Trim(),
                pro_cd = txt_pro_cd.Text.Trim(),
                fmb_nm = txt_fmb_nm.Text.Trim(),
                lot_no = txt_lot_no.Text.Trim(),
                exp_dt = txt_exp_dt.Text.Trim(),
                batch_no = txt_batch_no.Text.Trim(),
                weight = txt_weight_gs.Text.Trim();

            try
            {
                __sql = "FMB_Material_Packing_EPDM50_IN_AddItem_V1o0_Addnew";

                __sparam = new SqlParameter[9];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.VarChar;
                __sparam[0].ParameterName = "@long_cd";
                __sparam[0].Value = long_cd;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.VarChar;
                __sparam[1].ParameterName = "@short_cd";
                __sparam[1].Value = short_cd;

                __sparam[2] = new SqlParameter();
                __sparam[2].SqlDbType = SqlDbType.VarChar;
                __sparam[2].ParameterName = "@mmt_cd";
                __sparam[2].Value = mmt_cd;

                __sparam[3] = new SqlParameter();
                __sparam[3].SqlDbType = SqlDbType.VarChar;
                __sparam[3].ParameterName = "@pro_cd";
                __sparam[3].Value = pro_cd;

                __sparam[4] = new SqlParameter();
                __sparam[4].SqlDbType = SqlDbType.NVarChar;
                __sparam[4].ParameterName = "@fmb_nm";
                __sparam[4].Value = fmb_nm;

                __sparam[5] = new SqlParameter();
                __sparam[5].SqlDbType = SqlDbType.VarChar;
                __sparam[5].ParameterName = "@lot_no";
                __sparam[5].Value = lot_no;

                __sparam[6] = new SqlParameter();
                __sparam[6].SqlDbType = SqlDbType.VarChar;
                __sparam[6].ParameterName = "@exp_dt";
                __sparam[6].Value = exp_dt;

                __sparam[7] = new SqlParameter();
                __sparam[7].SqlDbType = SqlDbType.VarChar;
                __sparam[7].ParameterName = "@batch_no";
                __sparam[7].Value = batch_no;

                __sparam[8] = new SqlParameter();
                __sparam[8].SqlDbType = SqlDbType.Decimal;
                __sparam[8].ParameterName = "@weight";
                __sparam[8].Value = weight;

                cls.fnUpdDel(__sql, __sparam);

                Fnc_Load_Save_To_History(pro_cd, "Quét mã EPDM-50 nhập vào phòng ủ FMB thành công", true);

                Fnc_Load_Data_List();
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

            if (__pre_close >= 0)
            {
                __pre_close -= 1;
                //__timer_cls.Enabled = false;
                //lbl_auto_close.Visible = false;

                if (__pre_close == 0)
                {
                    __timer_cls.Enabled = true;
                    //__pre_close = 20;
                    lbl_auto_close.Visible = true;
                }
            }

            Fnc_Load_Scan_Status();
        }

        private void __timer_cnt_Tick(object sender, EventArgs e)
        {
            if (__counter >= 0)
            {
                tlp_code.Enabled = txt_code.Enabled = false;
                tlp_code.BackColor = txt_code.BackColor = Color.Gainsboro;

                __counter -= 1;

                pgr_counter.Value = (__counter > 0) ? __counter : 0;

                if (__counter == 0)
                {
                    //MessageBox.Show("Add new FMB");
                    Fnc_Load_Save_Data();

                    lbl_need_1.BackColor = lbl_need_2.BackColor = lbl_need_3.BackColor = lbl_need_4.BackColor = Color.Yellow;
                    txt_long_cd.Text =
                        txt_fmb_nm.Text =
                        txt_short_cd.Text =
                        txt_lot_no.Text =
                        txt_exp_dt.Text =
                        txt_batch_no.Text =
                        txt_weight_gs.Text =
                        txt_mmt_cd.Text =
                        txt_pro_cd.Text = "";
                    __is_long_cd = __is_short_cd = __is_mmt_cd = __is_pro_cd = false;

                    __counter = 100;
                    __timer_cnt.Enabled = false;

                    pgr_counter.Minimum = 0;
                    pgr_counter.Maximum = pgr_counter.Value = __counter;
                    pgr_counter.Enabled = false;

                    tlp_code.Enabled = txt_code.Enabled = true;
                    tlp_code.BackColor = txt_code.BackColor = Color.White;
                    txt_code.Focus();
                }
            }
        }

        private void __timer_cls_Tick(object sender, EventArgs e)
        {
            if (__close >= 0)
            {
                __close -= 1;

                lbl_auto_close.Text = String.Format("Nếu không có hoạt động, form này sẽ tự động đóng sau {0:00} giây nữa", __close);

                if (__close == 0)
                {
                    this.Close();
                }
            }
        }

        private void txt_code_KeyDown(object sender, KeyEventArgs e)
        {
            string
                code = "",
                code_kind = "",
                code_type = "";
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                {
                    code = txt_code.Text.Trim();
                    if (code.Length > 4)
                    {
                        code_kind = code.Substring(0, 3);

                        if (code_kind == "EPD" || code_kind == "MMT" || code_kind == "PRO")
                        {
                            Fnc_Load_Process_Code(code);
                        }
                        else
                        {
                            if (code.Length == 5)
                            {
                                Fnc_Load_Process_Code(code);
                            }
                            else
                            {
                                __is_short_cd = false;
                                txt_short_cd.Text = "";

                                __msg = "Mã ngắn không đúng định dạng\r\nHãy tìm và quét lại mã ngắn trên hộp đựng.";
                                Fnc_Load_Save_To_History(code, __msg, false);
                                MessageBox.Show(__msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                __msg = "";
                            }
                        }
                    }
                    else
                    {
                        __msg = "Mã quá ngắn (<=4 ký tự) và không đúng định dạng.\r\nHãy tìm và quét lại mã đúng trên hộp đựng.";
                        Fnc_Load_Save_To_History(code, __msg, false);
                        MessageBox.Show(__msg, __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        __msg = "";
                    }

                    __timer_cls.Enabled = false;
                    __pre_close = 20;
                    __close = 60;
                    lbl_auto_close.Text = String.Format("Nếu không có hoạt động, form này sẽ tự động đóng sau {0:00} giây nữa", __close - 1);
                    lbl_auto_close.Visible = false;

                    txt_code.Text = "";
                    txt_code.Focus();
                }
            }
            catch { }
            finally { }
        }

        private void txt_long_cd_TextChanged(object sender, EventArgs e)
        {
            string cd = txt_long_cd.Text.Trim();
            int cd_len = cd.Length;

            __is_long_cd = (cd_len > 0) ? true : false;
        }

        private void txt_short_cd_TextChanged(object sender, EventArgs e)
        {
            string cd = txt_short_cd.Text.Trim();
            int cd_len = cd.Length;

            __is_short_cd = (cd_len > 0 && cd_len <= 5) ? true : false;

            //if (!__is_short_cd)
            //{
            //    MessageBox.Show("Không đúng định dạng mã vạch ngắn\r\nHãy tìm và quét lại đúng mã.", __app, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txt_mmt_cd_TextChanged(object sender, EventArgs e)
        {
            string cd = txt_mmt_cd.Text.Trim();
            int cd_len = cd.Length;

            __is_mmt_cd = (cd_len > 0) ? true : false;
        }

        private void txt_pro_cd_TextChanged(object sender, EventArgs e)
        {
            string cd = txt_pro_cd.Text.Trim();
            int cd_len = cd.Length;

            __is_pro_cd = (cd_len > 0) ? true : false;
        }
    }
}
