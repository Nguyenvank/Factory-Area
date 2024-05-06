using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Inventory_Data.Ctrl
{
    public partial class uc_PMS_Temperature_Injection_Digits_06 : UserControl
    {
        string _msg = "", _msg_en = "", _msg_vi = "";
        int _msg_no = 0;

        int _mcRUB_Min = 1, _mcRUB_Max = 20;
        int _mcASY_Min = 1, _mcASY_Max = 4;

        int _ln = 0, _mc = 0;

        DataTable _tempLH;
        DateTime _now;
        int _nowSec, _nowMiSec;


        public uc_PMS_Temperature_Injection_Digits_06()
        {
            InitializeComponent();


            cls.SetDoubleBuffer(pnl_0, true);
            cls.SetDoubleBuffer(tbl_0, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_1, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_2, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_3, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_4, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_5, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_6, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_7, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_8, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_9, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_10, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_11, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_12, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_13, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_14, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_15, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_16, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_17, true);
            //cls.SetDoubleBuffer(tbl_MC_RUB_18, true);
            //cls.SetDoubleBuffer(tbl_MC_ASY_1, true);
            //cls.SetDoubleBuffer(tbl_MC_ASY_2, true);
            //cls.SetDoubleBuffer(tbl_MC_ASY_3, true);
            //cls.SetDoubleBuffer(tbl_MC_ASY_4, true);
        }

        private void uc_PMS_Temperature_Injection_Digits_06_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            init_Load_Controls();
        }

        public void init_Load_Controls()
        {
            Fnc_Load_Temp_LLHH(0);
            Fnc_Load_Controls();
            Fnc_Load_Data();
            //Fnc_Load_DateTime();

            //Fnc_Load_Temp_RUB();
            //Fnc_Load_Temp_ASY();
        }


        /**********************************************************/

        public void Fnc_Load_Controls()
        {
            _now = DateTime.Now;
            _nowSec = _now.Second;
            for (int i = _mcRUB_Min; i <= _mcRUB_Max; i++)
            {
                Label lbl_RUB_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_NM_" + i);
                lbl_RUB_NM.Text = "RUBBER " + String.Format("{0:00}", i);
                lbl_RUB_NM.Font = new Font("Times New Roman", 15, FontStyle.Bold);

                Label lbl_RUB_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_PV_" + i);
                //lbl_RUB_PV.Text = "180" + (char)176;
                //lbl_RUB_PV.Cursor = Cursors.Hand;
                //lbl_RUB_PV.Click += new EventHandler(set_temper_range);
                lbl_RUB_PV.Text = "180";
                lbl_RUB_PV.Font = new Font("Times New Roman", 90, FontStyle.Bold);

                Label lbl_RUB_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_SV_" + i);
                lbl_RUB_SV.Text = "0";
                lbl_RUB_SV.TextAlign = ContentAlignment.MiddleCenter;
                lbl_RUB_SV.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_RUB_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_HH_" + i);
                lbl_RUB_HH.Text = "0";
                lbl_RUB_HH.TextAlign = ContentAlignment.MiddleCenter;
                lbl_RUB_HH.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_RUB_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_LL_" + i);
                lbl_RUB_LL.Text = "0";
                lbl_RUB_LL.TextAlign = ContentAlignment.MiddleCenter;
                lbl_RUB_LL.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            }

            //Fnc_Load_Products_RUB();

            string prefix_mc = "";
            string prefix_nm = "";
            int prefix_no = 0;

            for(int a = _mcASY_Min; a <= _mcASY_Max; a++)
            {
                prefix_mc = (a == 1 || a == 2 || a == 3 || a == 4) ? "WELDING" : "AUTOBALANCE";
                prefix_nm = (a % 2 != 0) ? "(Upper)" : "(Lower)";
                prefix_no = (a == 1 || a == 2 || a == 5 || a == 6) ? 1 : 2;

                Label lbl_ASY_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_NM_" + a);
                lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm;
                lbl_ASY_NM.Font = new Font("Times New Roman", 15, FontStyle.Bold);

                Label lbl_ASY_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_PV_" + a);
                //lbl_ASY_PV.Text = "180" + (char)176;
                lbl_ASY_PV.Text = "180";
                lbl_ASY_PV.Font = new Font("Times New Roman", 90, FontStyle.Bold);

                Label lbl_ASY_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_SV_" + a);
                lbl_ASY_SV.Text = "0";
                lbl_ASY_SV.TextAlign = ContentAlignment.MiddleCenter;
                lbl_ASY_SV.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_ASY_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_HH_" + a);
                lbl_ASY_HH.Text = "0";
                lbl_ASY_HH.TextAlign = ContentAlignment.MiddleCenter;
                lbl_ASY_HH.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_ASY_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_LL_" + a);
                lbl_ASY_LL.Text = "0";
                lbl_ASY_LL.TextAlign = ContentAlignment.MiddleCenter;
                lbl_ASY_LL.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            }

            //Fnc_Load_Products_ASY();
        }

        public void Fnc_Load_Data()
        {
            //Thread loadData = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Fnc_Load_Temp_LLHH(0);

            //        Fnc_Load_Temp_RUB();
            //        //Fnc_Load_Products_RUB();

            //        Fnc_Load_Temp_ASY();
            //        //Fnc_Load_Products_ASY();

            //        Thread.Sleep(10000);
            //    }
            //});
            //loadData.IsBackground = true;
            //loadData.Start();

            //Thread loadDate = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        //Fnc_Load_DateTime();
            //        _now = DateTime.Now;

            //        Fnc_Load_Temp_RUB();
            //        Fnc_Load_Temp_ASY();

            //        Thread.Sleep(1000);
            //    }
            //});
            //loadDate.IsBackground = true;
            //loadDate.Start();

            //Thread loadDateMil = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        //Fnc_Load_DateTime();
            //        _now = DateTime.Now;
            //        _nowSec = _now.Second;
            //        _nowMiSec = _now.Millisecond;
            //        //label5.Text = (_nowSec % 2 == 0) ? "On" : "Off";
            //        //lbl_MC_RUB_SV_19.Text = (_nowMiSec <= 500) ? "On" : "Off";
            //        //lbl_MC_RUB_SV_19.Text = String.Format("{0:000}", _nowMiSec);

            //        Thread.Sleep(1);
            //    }
            //});
            //loadDateMil.IsBackground = true;
            //loadDateMil.Start();
        }

        public void Fnc_Load_Temp_RUB()
        {
            try
            {
                int listCount = 0, tblCount = 0, rowCount = 0, step01 = 0, step02 = 4;
                string temp_RUB_SV = "", temp_RUB_PV = "", temp_RUB_AL = "", temp_SPEC = "";
                string val_RUB_SV = "", val_RUB_PV = "", val_RUB_AL = "";
                int _val_RUB_SV = 0, _val_RUB_PV = 0, _val_RUB_AL = 0, _val_RUB_HH = 0, _val_RUB_LL = 0;
                int _val_HH_G = 0, _val_HH_Y = 0, _val_HH_R = 0;
                int _val_LL_G = 0, _val_LL_Y = 0, _val_LL_R = 0;
                string sql = "V2o1_ERP_Temperature_Digits_Init_Machine_SelItem_V1o1_Addnew";
                Color bgNG = Color.Transparent, frNG = Color.Transparent, bgMN = Color.Transparent;

                string tempLL = "", tempHH = "";
                int _tempLL = 0, _tempHH = 0;
                DataRow[] tempLH;
                string msg = "";

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                tblCount = ds.Tables.Count;
                rowCount = ds.Tables[0].Rows.Count;

                if (tblCount > 0 && rowCount > 0)
                {
                    temp_RUB_SV = ds.Tables[0].Rows[0][0].ToString();
                    temp_RUB_PV = ds.Tables[0].Rows[0][1].ToString();
                    temp_RUB_AL = ds.Tables[0].Rows[0][2].ToString();

                    for (int i = _mcRUB_Min; i <= _mcRUB_Max; i++)
                    {
                        tempLH = _tempLH.Select("mcLineIDx=1 and mcNameIDx=" + i);
                        if (tempLH != null)
                        {
                            foreach (DataRow row in tempLH)
                            {
                                tempLL = row[4].ToString();
                                tempHH = row[5].ToString();

                                _tempLL = (tempLL != "" && tempLL != null) ? Convert.ToInt32(tempLL) : 3;
                                _tempHH = (tempHH != "" && tempHH != null) ? Convert.ToInt32(tempHH) : 3;
                            }

                            msg += "LL: " + _tempLL + "          HH: " + _tempHH + "\r\n";
                        }
                        else
                        {
                            msg = "Nothing";
                        }

                        TableLayoutPanel tbl_RUB_MC = (TableLayoutPanel)cls.FindControlRecursive(pnl_0, "tbl_MC_RUB_" + i);
                        Label lbl_RUB_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_NM_" + i);
                        Label lbl_RUB_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_PV_" + i);
                        Label lbl_RUB_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_SV_" + i);
                        Label lbl_RUB_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_HH_" + i);
                        Label lbl_RUB_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_LL_" + i);

                        val_RUB_SV = temp_RUB_SV.Substring(step01, step02);
                        val_RUB_PV = temp_RUB_PV.Substring(step01, step02);
                        val_RUB_AL = temp_RUB_AL.Substring(step01, step02);

                        _val_RUB_SV = Convert.ToInt32(val_RUB_SV);
                        _val_RUB_PV = Convert.ToInt32(val_RUB_PV);
                        _val_RUB_AL = Convert.ToInt32(val_RUB_AL);

                        //_val_RUB_HH = (i <= 10) ? (_val_RUB_SV + 2) : (_val_RUB_SV + 5);
                        //_val_RUB_LL = (i <= 10) ? (_val_RUB_SV - 1) : (_val_RUB_SV - 5);

                        //_val_HH_G = _val_RUB_SV + 3; _val_HH_Y = _val_RUB_SV + 5;
                        //_val_LL_G = _val_RUB_SV - 3; _val_LL_Y = _val_RUB_SV - 5;

                        _val_HH_G = _val_RUB_SV + _tempHH; _val_HH_Y = _val_RUB_SV + (_tempHH + 2);
                        _val_LL_G = _val_RUB_SV - _tempLL; _val_LL_Y = _val_RUB_SV - (_tempLL + 2);

                        if (_val_RUB_SV > 0)
                        {
                            lbl_RUB_NM.Text = "RUBBER " + String.Format("{0:00}", i);

                            switch (i)
                            {
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 10:
                                case 11:
                                case 12:
                                    if (_val_RUB_PV >= _val_LL_G && _val_RUB_PV <= _val_HH_G)
                                    {
                                        bgMN = Color.DodgerBlue;
                                        bgNG = Color.White;
                                        frNG = Color.DodgerBlue;
                                    }
                                    else if ((_val_RUB_PV >= _val_LL_Y && _val_RUB_PV < _val_LL_G) || (_val_RUB_PV > _val_HH_G && _val_RUB_PV <= _val_HH_Y))
                                    {
                                        bgMN = Color.Chocolate;
                                        //bgNG = Color.Wheat;
                                        //frNG = Color.DarkOrange;
                                        bgNG = (_nowSec % 2 == 0) ? Color.Wheat : Color.DarkOrange;
                                        frNG = (_nowSec % 2 == 0) ? Color.DarkOrange : Color.Wheat;
                                    }
                                    else if (_val_RUB_AL < _val_LL_Y || _val_RUB_PV > _val_HH_Y)
                                    {
                                        bgMN = Color.Firebrick;
                                        //bgNG = Color.LightPink;
                                        //frNG = Color.Red;
                                        bgNG = (_nowSec % 2 == 0) ? Color.LightPink : Color.Red;
                                        frNG = (_nowSec % 2 == 0) ? Color.Red : Color.LightPink;
                                    }

                                    _val_RUB_HH = _val_HH_G;
                                    _val_RUB_LL = _val_LL_G;
                                    temp_SPEC = "(+" + tempHH + "/-" + tempLL + ")";
                                    break;
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                    if (_val_RUB_PV >= _val_LL_Y && _val_RUB_PV <= _val_HH_Y)
                                    {
                                        bgMN = Color.DodgerBlue;
                                        bgNG = Color.White;
                                        frNG = Color.DodgerBlue;
                                    }
                                    else if (_val_RUB_PV < _val_LL_Y || _val_RUB_PV > _val_HH_Y)
                                    {
                                        bgMN = Color.Firebrick;
                                        //bgNG = Color.LightPink;
                                        //frNG = Color.Red;
                                        bgNG = (_nowSec % 2 == 0) ? Color.LightPink : Color.Red;
                                        frNG = (_nowSec % 2 == 0) ? Color.Red : Color.LightPink;
                                    }

                                    _val_RUB_HH = _val_HH_Y;
                                    _val_RUB_LL = _val_LL_Y;
                                    temp_SPEC = "(+" + tempHH + "/-" + tempLL + ")";
                                    break;
                            }

                            //if (i <= 10)
                            //{
                            //    if (_val_RUB_PV >= _val_LL_G && _val_RUB_PV <= _val_HH_G)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;
                            //    }
                            //    else if ((_val_RUB_PV >= _val_LL_Y && _val_RUB_PV < _val_LL_G) || (_val_RUB_PV > _val_HH_G && _val_RUB_PV <= _val_HH_Y))
                            //    {
                            //        bgNG = Color.Wheat;
                            //        frNG = Color.DarkOrange;
                            //    }
                            //    else if (_val_RUB_AL < _val_LL_Y || _val_RUB_PV > _val_HH_Y)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;
                            //    }

                            //    _val_RUB_HH = _val_HH_G;
                            //    _val_RUB_LL = _val_LL_G;
                            //}
                            //else
                            //{
                            //    if (_val_RUB_PV >= _val_LL_Y && _val_RUB_PV <= _val_HH_Y)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;
                            //    }
                            //    else if (_val_RUB_PV < _val_LL_Y || _val_RUB_PV > _val_HH_Y)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;
                            //    }

                            //    _val_RUB_HH = _val_HH_Y;
                            //    _val_RUB_LL = _val_LL_Y;
                            //}

                            tbl_RUB_MC.BackColor = bgNG;
                            //lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = frNG;
                            lbl_RUB_PV.ForeColor = frNG;
                            lbl_RUB_NM.BackColor = bgMN;




                            //if (i <= 10)
                            //{
                            //    if (_val_RUB_PV >= _val_RUB_SV - 3 && _val_RUB_PV <= _val_RUB_SV + 3)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;

                            //        tbl_RUB_MC.BackColor = Color.White;
                            //        lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = Color.DodgerBlue;
                            //    }
                            //    else if ((_val_RUB_PV >= _val_RUB_SV - 5 && _val_RUB_PV < _val_RUB_SV - 3) || (_val_RUB_PV > _val_RUB_SV + 3 && _val_RUB_PV <= _val_RUB_SV + 5))
                            //    {
                            //        bgNG = Color.Orange;
                            //        frNG = Color.DarkOrange;

                            //        tbl_RUB_MC.BackColor = Color.Orange;
                            //        lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = Color.DarkOrange;
                            //    }
                            //    else if (_val_RUB_AL < _val_RUB_SV - 5 || _val_RUB_PV > _val_RUB_SV + 5)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;

                            //        tbl_RUB_MC.BackColor = Color.LightPink;
                            //        lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = Color.Red;
                            //    }
                            //}
                            //else
                            //{
                            //    if (_val_RUB_PV >= _val_RUB_SV - 5 && _val_RUB_PV <= _val_RUB_SV + 5)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;

                            //        tbl_RUB_MC.BackColor = Color.White;
                            //        lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = Color.DodgerBlue;
                            //    }
                            //    else if (_val_RUB_PV < _val_RUB_SV - 5 || _val_RUB_PV > _val_RUB_SV + 5)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;

                            //        tbl_RUB_MC.BackColor = Color.LightPink;
                            //        lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = Color.Red;
                            //    }
                            //}

                            //tbl_RUB_MC.BackColor = (_val_RUB_AL == 0) ? Color.White : Color.LightPink;
                            //lbl_RUB_NM.BackColor = (_val_RUB_AL == 0) ? Color.LightGreen : Color.Red;
                            ////tbl_RUB_MC.BackColor = (_val_RUB_PV >= _val_RUB_LL && _val_RUB_PV <= _val_RUB_HH) ? Color.White : Color.LightPink;
                            //tbl_RUB_MC.BackColor = bgNG;

                            ////lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = (_val_RUB_PV >= _val_RUB_LL && _val_RUB_PV <= _val_RUB_HH) ? Color.DodgerBlue : Color.Red;
                            //lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = frNG;
                            //lbl_RUB_PV.ForeColor =
                            //    lbl_RUB_SV.ForeColor =
                            //    lbl_RUB_HH.ForeColor =
                            //    lbl_RUB_LL.ForeColor = (tbl_RUB_MC.BackColor == Color.White) ? Color.DodgerBlue : Color.Red;
                            //lbl_RUB_PV.ForeColor = (lbl_RUB_NM.BackColor == Color.DodgerBlue) ? Color.Blue : Color.Red;

                            ////lbl_RUB_SV.Text = (i <= 10) ? "S: " + _val_RUB_SV.ToString() + "\r\n(+3/-3)" : "S: " + _val_RUB_SV.ToString() + "\r\n(+5/-5)";
                            lbl_RUB_SV.Text = "S: " + _val_RUB_SV.ToString() + "\r\n" + temp_SPEC;
                            lbl_RUB_PV.Text = _val_RUB_PV.ToString();
                            lbl_RUB_HH.Text = "H: " + _val_RUB_HH.ToString();
                            lbl_RUB_LL.Text = "L: " + _val_RUB_LL.ToString();
                        }
                        else
                        {
                            tbl_RUB_MC.BackColor = Color.Gainsboro;
                            lbl_RUB_NM.BackColor = Color.Gray;
                            lbl_RUB_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n(No work)";
                            lbl_RUB_SV.Text = "";
                            lbl_RUB_PV.Text = "";
                            lbl_RUB_HH.Text = "";
                            lbl_RUB_LL.Text = "";
                        }

                        step01 = step01 + step02;
                    }
                    //MessageBox.Show(msg);
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Load_Temp_ASY()
        {
            try
            {

                string prefix_mc = "";
                string prefix_nm = "";
                int prefix_no = 0, pre_SV = 0, pre_HH = 0, pre_LL = 0;

                int listCount = 0, tblCount = 0, rowCount = 0, step01 = 128, step02 = 4;
                string temp_SV = "", temp_PV = "", temp_AL = "";
                string val_SV = "", val_PV = "", val_AL = "";
                int _val_SV = 0, _val_PV = 0, _val_AL = 0, _val_HH = 0, _val_LL = 0;

                Color bgMN = Color.Transparent, bgNG = Color.Transparent, frNG = Color.Transparent;

                string tempLL = "", tempHH = "";
                int _tempLL = 0, _tempHH = 0;
                DataRow[] tempLH;
                string msg = "";

                string sql = "V2o1_ERP_Temperature_Digits_Init_Machine_SelItem_V1o1_Addnew";

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                tblCount = ds.Tables.Count;
                rowCount = ds.Tables[0].Rows.Count;

                if (tblCount > 0 && rowCount > 0)
                {
                    temp_SV = ds.Tables[0].Rows[0][0].ToString();
                    temp_PV = ds.Tables[0].Rows[0][1].ToString();
                    temp_AL = ds.Tables[0].Rows[0][2].ToString();

                    for (int i = _mcASY_Min; i <= _mcASY_Max; i++)
                    {
                        tempLH = _tempLH.Select("mcLineIDx=3 and mcNameIDx=" + i);
                        if (tempLH != null)
                        {
                            foreach (DataRow row in tempLH)
                            {
                                tempLL = row[4].ToString();
                                tempHH = row[5].ToString();

                                _tempLL = (tempLL != "" && tempLL != null) ? Convert.ToInt32(tempLL) : 3;
                                _tempHH = (tempHH != "" && tempHH != null) ? Convert.ToInt32(tempHH) : 3;
                            }

                            msg += "LL: " + _tempLL + "          HH: " + _tempHH + "\r\n";
                        }
                        else
                        {
                            msg = "Nothing";
                        }

                        TableLayoutPanel tbl_ASY_MC = (TableLayoutPanel)cls.FindControlRecursive(pnl_0, "tbl_MC_ASY_" + i);
                        Label lbl_ASY_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_NM_" + i);
                        Label lbl_ASY_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_PV_" + i);
                        Label lbl_ASY_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_SV_" + i);
                        Label lbl_ASY_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_HH_" + i);
                        Label lbl_ASY_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_LL_" + i);

                        prefix_mc = (i == 1 || i == 2 || i == 3 || i == 4) ? "WELDING" : "AUTOBALANCE";
                        prefix_nm = (i % 2 != 0) ? "(Upper)" : "(Lower)";
                        prefix_no = (i == 1 || i == 2 || i == 5 || i == 6) ? 1 : 2;

                        val_SV = temp_SV.Substring(step01, step02);
                        val_PV = temp_PV.Substring(step01, step02);
                        val_AL = temp_AL.Substring(step01, step02);

                        _val_SV = Convert.ToInt32(val_SV);
                        _val_PV = Convert.ToInt32(val_PV);
                        _val_AL = Convert.ToInt32(val_AL);
                        _val_HH = (_val_SV + _tempHH);
                        _val_LL = (_val_SV - _tempLL);

                        if (_val_SV > 0)
                        {
                            if (_val_PV >= _val_LL && _val_PV <= _val_HH)
                            {
                                bgMN = Color.DodgerBlue;
                                bgNG = Color.White;
                                frNG = Color.DodgerBlue;
                            }
                            else if (_val_PV < _val_LL || _val_PV > _val_HH)
                            {
                                bgMN = Color.Firebrick;
                                //bgNG = Color.LightPink;
                                //frNG = Color.Red;
                                bgNG = (_nowSec % 2 == 0) ? Color.LightPink : Color.Red;
                                frNG = (_nowSec % 2 == 0) ? Color.Red : Color.LightPink;
                            }

                            //tbl_RUB_MC.BackColor = (_val_AL == 0) ? Color.White : Color.LightPink;
                            //lbl_RUB_NM.BackColor = (_val_AL == 0) ? Color.LightGreen : Color.Red;
                            ////tbl_ASY_MC.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.White : Color.LightPink;

                            lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm;
                            ////lbl_ASY_PV.ForeColor = lbl_ASY_NM.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.DodgerBlue : Color.Red;

                            tbl_ASY_MC.BackColor = bgNG;
                            //lbl_RUB_PV.ForeColor = lbl_RUB_NM.BackColor = frNG;
                            lbl_ASY_PV.ForeColor = frNG;
                            lbl_ASY_NM.BackColor = bgMN;


                            //lbl_ASY_PV.ForeColor = lbl_ASY_NM.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.ForestGreen : Color.Red;
                            //lbl_RUB_PV.ForeColor =
                            //    lbl_RUB_SV.ForeColor =
                            //    lbl_RUB_HH.ForeColor =
                            //    lbl_RUB_LL.ForeColor = (tbl_RUB_MC.BackColor == Color.White) ? Color.DodgerBlue : Color.Red;
                            //lbl_RUB_PV.ForeColor = (lbl_RUB_NM.BackColor == Color.DodgerBlue) ? Color.Blue : Color.Red;
                            lbl_ASY_SV.Text = (_tempLL == _tempHH) ? "S: " + _val_SV.ToString() + "\r\n(" + (char)177 + "" + _tempLL + ")" : "S: " + _val_SV.ToString() + "\r\n(+" + _tempHH + " /-" + _tempLL + ")";
                            lbl_ASY_PV.Text = _val_PV.ToString();
                            lbl_ASY_HH.Text = "H: " + _val_HH.ToString();
                            lbl_ASY_LL.Text = "L: " + _val_LL.ToString();
                        }
                        else
                        {
                            tbl_ASY_MC.BackColor = Color.Gainsboro;
                            lbl_ASY_NM.BackColor = Color.Gray;

                            lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm + "\r\n(No work)";
                            //lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm;
                            lbl_ASY_SV.Text = "";
                            lbl_ASY_PV.Text = "";
                            lbl_ASY_HH.Text = "";
                            lbl_ASY_LL.Text = "";
                        }

                        step01 = step01 + step02;
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Load_Temp_LLHH(int _lnIDx)
        {
            try
            {
                string sql = "V2o1_SUMMARY_PMS_Temperature_Get_Range_List_V1o1_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@lnIDx";
                sParams[0].Value = _lnIDx;

                _tempLH = cls.ExecuteDataTable(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Update_Temper_Range(int _lnIDx, int _mcIDx, int _mcSV, int _mcLL, int _mcHH)
        {
            try
            {
                string sql = "V2o1_SUMMARY_PMS_Temperature_Set_Range_List_V1o1_UpdItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@lnIDx";
                sParams[0].Value = _lnIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@mcIDx";
                sParams[1].Value = _mcIDx;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.Int;
                sParams[2].ParameterName = "@mcSV";
                sParams[2].Value = _mcSV;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.Int;
                sParams[3].ParameterName = "@mcLL";
                sParams[3].Value = _mcLL;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.Int;
                sParams[4].ParameterName = "@mcHH";
                sParams[4].Value = _mcHH;

                cls.fnUpdDel(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Load_Products_RUB()
        {
            DateTime date = DateTime.Now;
            DateTime dayBegin = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            DateTime dayEnd = new DateTime(date.Year, date.Month, date.Day, 20, 59, 59);
            DateTime nightBegin = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0);
            DateTime nightEnd = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0).AddDays(1);
            //string mcRUB_Name = "";
            //string factory = "F1";
            int shift = (cls.isTimeBetween(date, dayBegin, dayEnd) == true) ? 1 : 2;
            int listCount = 0, rows = 0, cols = 0;
            string val_PV = "";
            int _val_PV = 0;

            try
            {
                string sql = "GET_PRODUCTMONITORING6_VNUSER_SHIFT_WORK_ORDER_LIST";

                SqlParameter[] sParam = new SqlParameter[2]; // Parameter count

                sParam[0] = new SqlParameter();
                sParam[0].SqlDbType = SqlDbType.DateTime;
                sParam[0].ParameterName = "@date";
                sParam[0].Value = date;

                sParam[1] = new SqlParameter();
                sParam[1].SqlDbType = SqlDbType.TinyInt;
                sParam[1].ParameterName = "@shifts";
                sParam[1].Value = shift;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, CommandType.StoredProcedure, "connVMERP", sParam);
                listCount = ds.Tables.Count;
                rows = ds.Tables[0].Rows.Count;

                if (listCount > 0 && rows > 0)
                {
                    for (int i = _mcRUB_Min; i <= _mcRUB_Max; i++)
                    {
                        Label lbl_RUB_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_NM_" + i);
                        lbl_RUB_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n" + ds.Tables[0].Rows[i][4].ToString();

                        //Label lbl_RUB_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_RUB_SV_" + i);
                        //mcRUB_Name = lbl_RUB_NM.Text;
                        //lbl_RUB_NM.Text = "";
                        //lbl_RUB_NM.Text = mcRUB_Name + "\r\n" + ds.Tables[0].Rows[i][4].ToString();

                        //val_PV = lbl_RUB_PV.Text;
                        //_val_PV = (val_PV != "" && val_PV != null) ? Convert.ToInt32(val_PV) : 0;

                        //if (_val_PV > 0)
                        //{
                        //    lbl_RUB_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n" + ds.Tables[0].Rows[i][4].ToString();
                        //}
                        //else
                        //{
                        //    lbl_RUB_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n(No work)";
                        //}
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Load_Products_ASY()
        {
            string prefix_mc = "";
            string prefix_nm = "";
            int prefix_no = 0, pre_SV = 0, pre_HH = 0, pre_LL = 0, pre_col = 0;

            DateTime date = DateTime.Now;
            DateTime dayBegin = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            DateTime dayEnd = new DateTime(date.Year, date.Month, date.Day, 20, 59, 59);
            DateTime nightBegin = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0);
            DateTime nightEnd = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0).AddDays(1);
            //string mcASY_Name = "";
            int shift = (cls.isTimeBetween(date, dayBegin, dayEnd) == true) ? 1 : 2;
            int listCount = 0, rows = 0, cols = 0;
            string val_PV = "";
            int _val_PV = 0;

            try
            {
                string sql = "V2o1_ERP_Temperature_Digits_Init_Product_SelItem_V1o1_Addnew";

                SqlParameter[] sParam = new SqlParameter[2]; // Parameter count

                sParam[0] = new SqlParameter();
                sParam[0].SqlDbType = SqlDbType.DateTime;
                sParam[0].ParameterName = "@date";
                sParam[0].Value = date;

                sParam[1] = new SqlParameter();
                sParam[1].SqlDbType = SqlDbType.TinyInt;
                sParam[1].ParameterName = "@shifts";
                sParam[1].Value = shift;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParam);
                listCount = ds.Tables.Count;
                rows = ds.Tables[0].Rows.Count;

                if (listCount > 0 && rows > 0)
                {
                    for (int i = _mcASY_Min; i <= _mcASY_Max; i++)
                    {
                        prefix_mc = (i == 1 || i == 2 || i == 3 || i == 4) ? "WELDING" : "AUTOBALANCE";
                        prefix_nm = (i % 2 != 0) ? "(Upper)" : "(Lower)";
                        prefix_no = (i == 1 || i == 2 || i == 5 || i == 6) ? 1 : 2;
                        pre_col = (i == 1 || i == 2) ? 0 : 1;

                        Label lbl_ASY_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_NM_" + i);
                        lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm + "\r\n" + ds.Tables[0].Rows[pre_col][2].ToString();

                        //Label lbl_ASY_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_ASY_SV_" + i);
                        //lbl_ASY_NM.Text = "\r\n" + ds.Tables[0].Rows[pre_col][2].ToString();

                        //val_PV = lbl_ASY_PV.Text;
                        //_val_PV = (val_PV != "" && val_PV != null) ? Convert.ToInt32(val_PV) : 0;

                        //if (_val_PV > 0)
                        //{
                        //    lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm + "\r\n" + ds.Tables[0].Rows[pre_col][2].ToString();
                        //}
                        //else
                        //{
                        //    lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm + "\r\n(No work)";
                        //}
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        /**********************************************************/

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Interval=10.000

            Fnc_Load_Temp_LLHH(0);

            Fnc_Load_Temp_RUB();

            Fnc_Load_Temp_ASY();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Interval=1.000

            _now = DateTime.Now;

            Fnc_Load_Temp_RUB();
            Fnc_Load_Temp_ASY();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            // Interval=1

            _now = DateTime.Now;
            _nowSec = _now.Second;
            _nowMiSec = _now.Millisecond;

            //lbl_MC_RUB_SV_19.Text = String.Format("{0:000}", _nowMiSec);
        }




    }
}
