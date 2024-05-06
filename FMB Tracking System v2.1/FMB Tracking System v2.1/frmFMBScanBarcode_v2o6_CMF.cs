using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Inventory_Data;
using System.Data.SqlClient;

namespace FMB_Tracking_System_v2._1
{
    public partial class frmFMBScanBarcode_v2o6_CMF : Form
    {
        string _packing = "", _matLot = "", _pakWeight = "", _matIDx = "";
        int ___pakWeight = 0;

        System.Windows.Forms.Form frm = System.Windows.Forms.Application.OpenForms["frmFMBScanBarcode_v2o6"];

        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer _remain = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer _closed = new System.Windows.Forms.Timer();

        DateTime _dt = DateTime.Now;

        int _time_start = 0, _time_remain = 23, _time_closed = 25, _code_qty = 0;

        public frmFMBScanBarcode_v2o6_CMF()
        {
            InitializeComponent();
        }

        public frmFMBScanBarcode_v2o6_CMF(string packing, string matLot, string pakWeight, string matIDx)
        {
            InitializeComponent();

            _packing = packing;
            _matLot = matLot;
            _pakWeight = pakWeight;
            _matIDx = matIDx;

            ___pakWeight = (_pakWeight.Length > 0) ? Convert.ToInt32(_pakWeight) : 0;
            _code_qty = (___pakWeight / 75);

            lbl_Count.Text = _code_qty.ToString();
        }

        private void frmFMBScanBarcode_v2o6_CMF_Load(object sender, EventArgs e)
        {
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(this._timer_Tick);
            _timer.Enabled = true;

            _remain.Interval = 1000;
            _remain.Tick += new EventHandler(this._remain_Tick);
            _remain.Enabled = true;

            _closed.Interval = 1000;
            _closed.Tick += new EventHandler(this._closed_Tick);
            _closed.Enabled = true;

            Fnc_Load_Init();
        }

        private void frmFMBScanBarcode_v2o6_CMF_KeyDown(object sender, KeyEventArgs e)
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

        /**********************************************/

        public void Fnc_Load_Controls()
        {
            txt_Code.Text = "";
            txt_Code.Focus();

            lst_List.Items.Clear();

            lbl_Sec.Text = String.Format("{0:00}", _time_remain);

            //string s1 = "PRO-CMF-202009090 001\r\n";
            //string s2 = "PRO-CMF-202009090001";
            //bool comp = cls.Fnc_Compare_String_OrdinalIgnoreCase(s1, s2);
            //MessageBox.Show(comp.ToString());
        }

        public bool Fnc_String_Compare(string s1, string s2)
        {
            bool result = false;

            string normalized1 = Regex.Replace(s1, @"\s", "");
            string normalized2 = Regex.Replace(s2, @"\s", "");

            result = String.Equals(normalized1,normalized2,StringComparison.OrdinalIgnoreCase);


            return result;
        }

        public bool Fnc_Check_Code_Scan_CMB(string code)
        {
            bool chk = false;
            string chkCnt = "";
            int tblCnt = 0, rowCnt = 0, _chkCnt = 0;

            try
            {
                string sql = "FMB_Material_IN_Exist_ChkItem_CMF_V2o2_Addnew";

                SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                sParamsChk[0] = new SqlParameter();
                sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                sParamsChk[0].ParameterName = "@cmf_code";
                sParamsChk[0].Value = code;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParamsChk);

                tblCnt = ds.Tables.Count;
                rowCnt = ds.Tables[0].Rows.Count;

                if (tblCnt > 0 && rowCnt > 0)
                {
                    chkCnt = ds.Tables[0].Rows[0][0].ToString();
                    _chkCnt = (chkCnt.Length > 0) ? Convert.ToInt32(chkCnt) : 0;

                    chk = (_chkCnt == 1) ? true : false;
                }
            }
            catch { }
            finally { }

            return chk;
        }

        public string Fnc_Check_Code_Scan_CMB_Valid_Date(string code)
        {
            string msg_error = "";

            string
                chkCnt = "",
                expire_dt = "";

            DateTime
                _current = DateTime.Now,
                _expire_dt = DateTime.Now;
            
            int 
                tblCnt = 0, 
                rowCnt = 0, 
                _chkCnt = 0;

            try
            {
                string sql = "FMB_Material_IN_Exist_ChkItem_CMF_V2o3_Addnew";

                SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                sParamsChk[0] = new SqlParameter();
                sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                sParamsChk[0].ParameterName = "@cmf_code";
                sParamsChk[0].Value = code;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParamsChk);

                tblCnt = ds.Tables.Count;
                rowCnt = ds.Tables[0].Rows.Count;

                if (tblCnt > 0 && rowCnt > 0)
                {
                    chkCnt = ds.Tables[0].Rows[0][0].ToString();
                    expire_dt = ds.Tables[0].Rows[0][1].ToString();

                    _chkCnt = (chkCnt.Length > 0) ? Convert.ToInt32(chkCnt) : 0;
                    _expire_dt = (expire_dt.Length > 0) ? Convert.ToDateTime(expire_dt) : DateTime.Now;

                    if (_chkCnt > 0)
                    {
                        if (_current > _expire_dt)
                        {
                            //msg_error = "HÃY KIỂM TRA LẠI !!!\r\n\r\n\r\n";
                            msg_error += "MÃ " + code + " ĐÃ HẾT HẠN SỬ DỤNG (" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", _expire_dt) + ")\r\n";
                            msg_error += "FMB " + code + " EXPIRED (" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", _expire_dt) + ")\r\n\r\n";
                            msg_error += "Mã FMB phải được quét trước thời điểm\r\nhết hạn in trên tem\r\n\r\n\r\n";
                            msg_error += "CHÚ Ý: " + code + " chưa được\r\nghi nhận vào hệ thống vì lý do trên\r\n\r\n";
                            msg_error += "Error code: 008";
                        }
                        else
                        {
                            msg_error = "";
                        }
                    }
                    else
                    {
                        //msg_error = "HÃY KIỂM TRA LẠI !!!\r\n\r\n\r\n";
                        msg_error += "MÃ " + code + " CHƯA ĐƯỢC QUÉT KẾT HỢP VỚI NGUYÊN LIỆU CMB\r\n";
                        msg_error += "CODE " + code + " UNSCANED PRO-CMF COMBINED WITH CMB MATERIALS\r\n\r\n";
                        msg_error += "Mã CMF phải được quét kết hợp với CMB trước khi\r\nđược nhập vào phòng làm mát cao su\r\n\r\n\r\n";
                        msg_error += "CHÚ Ý: " + code + " chưa được\r\nghi nhận vào hệ thống vì lý do trên\r\n\r\n";
                        msg_error += "Error code: 007";
                    }
                }
            }
            catch { }
            finally { }

            return msg_error;
        }

        public void Fnc_Add_CMF_Code()
        {
            string code = txt_Code.Text.Trim();

            if (code.Length > 0)
            {

                lst_List.Items.Add(code);

                _time_remain = 15;
                _time_closed = 20;

                //for(int i = 0; i < txt_List.Lines.Length; i++)
                //{
                //    MessageBox.Show(txt_List.Lines[i]);
                //}
            }

            txt_Code.Text = "";
            txt_Code.Focus();
        }

        public void Fnc_Add_CMF_Code(string code)
        {
            string
                packing=code,
                msg = "",
                err_cd = "";

            int
                _err_cd = 0;

            if (code.Length > 0)
            {
                //bool chk_scan = Fnc_Check_Code_Scan_CMB(code);
                msg = Fnc_Check_Code_Scan_CMB_Valid_Date(code);

                //if (chk_scan == true)
                if (msg.Length == 0)
                {
                    bool _chk = false;

                    for (int i = 0; i < lst_List.Items.Count; i++)
                    {
                        string chk = lst_List.Items[i].ToString();

                        if (chk.Trim().Length > 0)
                        {
                            if (cls.Fnc_Compare_String_OrdinalIgnoreCase(chk, code) == true)
                            {
                                _chk = true;
                            }
                        }
                    }

                    if (_chk == false)
                    {
                        lst_List.Items.Add(code);

                        _code_qty -= 1;
                    }

                    _time_remain = 23;
                    _time_closed = 25;

                    _remain.Start();
                    _closed.Start();
                }
                else
                {
                    _remain.Stop();
                    _closed.Stop();

                    err_cd = cls.Right(msg, 3);
                    _err_cd = (err_cd.Length > 0) ? Convert.ToInt32(err_cd) : 0;

                    //string msg = "HÃY KIỂM TRA LẠI !!!\r\n\r\n\r\n";
                    //msg += "MÃ " + code + " CHƯA ĐƯỢC QUÉT\r\nKẾT HỢP VỚI NGUYÊN LIỆU CMB\r\n\r\n";
                    //msg += "Mã CMF phải được quét kết hợp với CMB trước khi\r\nđược nhập vào phòng làm mát cao su\r\n\r\n\r\n";
                    //msg += "CHÚ Ý: " + code + " chưa được\r\nghi nhận vào hệ thống vì lý do trên";
                    //MessageBox.Show(msg, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);


                    frmFMBScanBarcode_v2o6_Warning_v1o0 form = new frmFMBScanBarcode_v2o6_Warning_v1o0(_err_cd, msg, packing);
                    form.ShowDialog();

                    _remain.Start();
                    _closed.Start();
                }

                lbl_Count.Text = _code_qty.ToString();
            }

            txt_Code.Text = "";
            txt_Code.Focus();
        }

        public void Fnc_Save_CMF_Code()
        {
            int code_qty = _code_qty;

            if (code_qty == 0)
            {
                int code_items = lst_List.Items.Count;
                string packing = _packing, matLot = _matLot, pakWeight = _pakWeight, matIDx = _matIDx;
                string sql = "FMB_Material_IN_Exist_SaveItem_CMF_V2o2_Addnew";
                try
                {
                    for (int i = 0; i < code_items; i++)
                    {
                        SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.VarChar;
                        sParams[0].ParameterName = "@cmf_code";
                        sParams[0].Value = lst_List.Items[i].ToString();

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.VarChar;
                        sParams[1].ParameterName = "@car_code";
                        sParams[1].Value = packing;

                        cls.fnUpdDel(sql, sParams);
                    }

                    if (code_items > 0)
                    {
                        ((frmFMBScanBarcode_v2o6)frm).Fnc_Load_Save_Scan_In(packing, matLot, pakWeight, matIDx);
                    }
                    else
                    {
                        ((frmFMBScanBarcode_v2o6)frm).Fnc_Load_Not_Scan_In_Not_Save();
                    }
                }
                catch { }
                finally { }
            }
            else
            {
                ((frmFMBScanBarcode_v2o6)frm).Fnc_Load_Not_Scan_In_Not_Save();
            }
        }

        public void Fnc_Save_CMF_Code_01()
        {
            string chk = "", cmf = "", kind = "", type = "", code = "";
            int line_count = 0, len = 0;
            bool str_comp = false;

            cmf = txt_Code.Text.Trim();
            if (cmf.Length > 0)
            {
                kind = cmf.Substring(0, 3);
                type = cmf.Substring(4, 3);
                code = cmf.Substring(8);
                len = cmf.Length;

                lbl_Comp.Text = "";
                lbl_Count.Text = "0";

                if (kind.ToLower() == "pro" && type.ToLower() == "cmf")
                {
                    line_count = lst_List.Items.Count;

                    if (line_count < 1)
                    {
                        Fnc_Add_CMF_Code(cmf);
                    }
                    else
                    {
                        for (int i = 0; i < lst_List.Items.Count; i++)
                        {
                            chk = lst_List.Items[i].ToString();

                            if (chk.Length > 0)
                            {
                                str_comp = cls.Fnc_Compare_String_OrdinalIgnoreCase(chk, cmf);

                                if (str_comp == false) { Fnc_Add_CMF_Code(cmf); }
                            }
                        }

                        //chk = txt_List.Text.Trim();
                        //string[] lines = chk.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                        ////string[] list = chk.Split(new String[] { "\r\n" }, StringSplitOptions.None);
                        //foreach (string line in lines)
                        //{
                        //    if (line.Length > 0 && cls.Fnc_Compare_String_OrdinalIgnoreCase(line, cmf) == false)
                        //    {
                        //        Fnc_Add_CMF_Code(cmf);
                        //    }
                        //}


                        //for (int i = 0; i < line_count; i++)
                        //{
                        //    chk = txt_List.Lines[i];
                        //    if (chk.Length > 5)
                        //    {
                        //        str_comp = cls.Fnc_Compare_String_OrdinalIgnoreCase(chk, cmf);
                        //        if (str_comp == false) { Fnc_Add_CMF_Code(cmf); }
                        //    }
                        //}

                        //foreach (string line in txt_List.Lines)
                        //{
                        //    str_comp = cls.Fnc_Compare_String_OrdinalIgnoreCase(cmf, line);
                        //    if (str_comp == false)
                        //    {
                        //        Fnc_Add_CMF_Code(cmf);
                        //    }
                        //}
                    }
                }

                //if (line_count > 1)
                //{
                //    for (int i = 0; i < line_count; i++)
                //    {
                //        chk = txt_List.Lines[i];

                //        str_comp = cls.Fnc_Compare_String_OrdinalIgnoreCase(chk, cmf);
                //        lbl_Comp.Text = str_comp.ToString();

                //        if (str_comp == false)
                //        {
                //            Fnc_Add_CMF_Code();
                //        }
                //    }
                //}
                //else
                //{
                //    Fnc_Add_CMF_Code();
                //}

                line_count = lst_List.Items.Count;
                lbl_Count.Text = line_count.ToString();
            }

            txt_Code.Text = "";
            txt_Code.Focus();
        }

        public void Fnc_Save_CMF_Code_02()
        {
            try
            {
                string chk = "", cmf = "", kind = "", type = "", code = "";
                int line_count = 0, leng = 0;
                bool str_comp = false;

                cmf = txt_Code.Text.Trim();

                if (cmf.Length > 0)
                {
                    leng = cmf.Length;
                    kind = cmf.Substring(0, 3);
                    type = cmf.Substring(4, 3);
                    code = cmf.Substring(8);

                    if (kind.ToLower() == "pro" && type.ToLower() == "cmf")
                    {
                        Fnc_Add_CMF_Code(cmf);
                    }

                    txt_Code.Text = "";
                    txt_Code.Focus();
                }
            }
            catch { }
            finally { }
        }

        /**********************************************/

        private void _timer_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
        }

        private void _closed_Tick(object sender, EventArgs e)
        {
            if (_time_closed > 0)
            {
                this.Text = "NHẬP MÃ VẠCH C.M.F TỰ ĐỘNG ĐÓNG SAU " + String.Format("{0:00}", _time_closed) + " GIÂY NỮA";

                _time_closed -= 1;
            }
            else
            {
                _closed.Enabled = false;
                this.Close();
            }
        }

        private void _remain_Tick(object sender, EventArgs e)
        {
            if (_time_remain > 0)
            {
                txt_Code.Enabled = true;
                txt_Code.Focus();
                lbl_Sec.Text = (_time_remain > 0) ? String.Format("{0:00}", _time_remain) + " giây" : "0 giây";

                _time_remain -= 1;
            }
            else
            {
                Fnc_Save_CMF_Code();

                txt_Code.Text = "";
                txt_Code.Enabled = false;
                lbl_Sec.Text = "0";

                _remain.Enabled = false;
            }
        }

        private void txt_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Fnc_Save_CMF_Code_01();
                Fnc_Save_CMF_Code_02();
            }
        }
    }
}
