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
    public partial class uc_PMS_Temperature_Injection_Digits_03 : UserControl
    {

        int _mcINJ_Min = 1, _mcINJ_Max = 18;
        int _mcASY_Min = 1, _mcASY_Max = 4;


        public uc_PMS_Temperature_Injection_Digits_03()
        {
            InitializeComponent();


            //cls.SetDoubleBuffer(pnl_0, true);
            cls.SetDoubleBuffer(tbl_0, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_1, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_2, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_3, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_4, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_5, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_6, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_7, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_8, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_9, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_10, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_11, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_12, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_13, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_14, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_15, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_16, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_17, true);
            //cls.SetDoubleBuffer(tbl_MC_INJ_18, true);
        }

        private void uc_PMS_Temperature_Injection_Digits_03_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            init_Load_Controls();
        }

        public void init_Load_Controls()
        {
            Fnc_Load_Controls();
            Fnc_Load_Data();
            Fnc_Load_DateTime();
        }


        /**********************************************************/

        public void Fnc_Load_Controls()
        {
            for(int i = _mcINJ_Min; i <= _mcINJ_Max; i++)
            {
                Label lbl_INJ_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_NM_" + i);
                lbl_INJ_NM.Text = "RUBBER " + String.Format("{0:00}", i);
                lbl_INJ_NM.Font = new Font("Times New Roman", 15, FontStyle.Bold);

                Label lbl_INJ_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_PV_" + i);
                //lbl_INJ_PV.Text = "180" + (char)176;
                lbl_INJ_PV.Text = "180";
                lbl_INJ_PV.Font = new Font("Times New Roman", 90, FontStyle.Bold);

                Label lbl_INJ_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_SV_" + i);
                lbl_INJ_SV.Text = "0";
                lbl_INJ_SV.TextAlign = ContentAlignment.MiddleCenter;
                lbl_INJ_SV.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_INJ_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_HH_" + i);
                lbl_INJ_HH.Text = "0";
                lbl_INJ_HH.TextAlign = ContentAlignment.MiddleCenter;
                lbl_INJ_HH.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_INJ_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_LL_" + i);
                lbl_INJ_LL.Text = "0";
                lbl_INJ_LL.TextAlign = ContentAlignment.MiddleCenter;
                lbl_INJ_LL.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            }

            //Fnc_Load_Products_INJ();

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
            Thread loadData = new Thread(() =>
            {
                while (true)
                {
                    Fnc_Load_Temp_INJ();
                    //Fnc_Load_Products_INJ();

                    Fnc_Load_Temp_ASY();
                    //Fnc_Load_Products_ASY();

                    Thread.Sleep(10000);
                }
            });
            loadData.IsBackground = true;
            loadData.Start();

            Thread loadDate = new Thread(() =>
            {
                while (true)
                {
                    //Fnc_Load_DateTime();

                    Thread.Sleep(1000);
                }
            });
            loadDate.IsBackground = true;
            loadDate.Start();
        }

        public void Fnc_Load_Temp_INJ()
        {
            try
            {
                int listCount = 0, tblCount = 0, rowCount = 0, step01 = 0, step02 = 4;
                string temp_INJ_SV = "", temp_INJ_PV = "", temp_INJ_AL = "", temp_SPEC = "";
                string val_INJ_SV = "", val_INJ_PV = "", val_INJ_AL = "";
                int _val_INJ_SV = 0, _val_INJ_PV = 0, _val_INJ_AL = 0, _val_INJ_HH = 0, _val_INJ_LL = 0;
                int _val_HH_G = 0, _val_HH_Y = 0, _val_HH_R = 0;
                int _val_LL_G = 0, _val_LL_Y = 0, _val_LL_R = 0;
                string sql = "V2o1_ERP_Temperature_Digits_Init_Machine_SelItem_V1o1_Addnew";
                Color bgNG = Color.Transparent, frNG = Color.Transparent;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                tblCount = ds.Tables.Count;
                rowCount = ds.Tables[0].Rows.Count;

                if (tblCount > 0 && rowCount > 0)
                {
                    temp_INJ_SV = ds.Tables[0].Rows[0][0].ToString();
                    temp_INJ_PV = ds.Tables[0].Rows[0][1].ToString();
                    temp_INJ_AL = ds.Tables[0].Rows[0][2].ToString();

                    for (int i = _mcINJ_Min; i <= _mcINJ_Max; i++)
                    {
                        TableLayoutPanel tbl_INJ_MC = (TableLayoutPanel)cls.FindControlRecursive(pnl_0, "tbl_MC_INJ_" + i);
                        Label lbl_INJ_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_NM_" + i);
                        Label lbl_INJ_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_PV_" + i);
                        Label lbl_INJ_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_SV_" + i);
                        Label lbl_INJ_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_HH_" + i);
                        Label lbl_INJ_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_LL_" + i);

                        val_INJ_SV = temp_INJ_SV.Substring(step01, step02);
                        val_INJ_PV = temp_INJ_PV.Substring(step01, step02);
                        val_INJ_AL = temp_INJ_AL.Substring(step01, step02);

                        _val_INJ_SV = Convert.ToInt32(val_INJ_SV);
                        _val_INJ_PV = Convert.ToInt32(val_INJ_PV);
                        _val_INJ_AL = Convert.ToInt32(val_INJ_AL);

                        //_val_INJ_HH = (i <= 10) ? (_val_INJ_SV + 2) : (_val_INJ_SV + 5);
                        //_val_INJ_LL = (i <= 10) ? (_val_INJ_SV - 1) : (_val_INJ_SV - 5);

                        _val_HH_G = _val_INJ_SV + 3; _val_HH_Y = _val_INJ_SV + 5;
                        _val_LL_G = _val_INJ_SV - 3; _val_LL_Y = _val_INJ_SV - 5;

                        if (_val_INJ_SV > 0)
                        {
                            lbl_INJ_NM.Text = "RUBBER " + String.Format("{0:00}", i);

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
                                //case 11:
                                case 12:
                                    if (_val_INJ_PV >= _val_LL_G && _val_INJ_PV <= _val_HH_G)
                                    {
                                        bgNG = Color.White;
                                        frNG = Color.DodgerBlue;
                                    }
                                    else if ((_val_INJ_PV >= _val_LL_Y && _val_INJ_PV < _val_LL_G) || (_val_INJ_PV > _val_HH_G && _val_INJ_PV <= _val_HH_Y))
                                    {
                                        bgNG = Color.Wheat;
                                        frNG = Color.DarkOrange;
                                    }
                                    else if (_val_INJ_AL < _val_LL_Y || _val_INJ_PV > _val_HH_Y)
                                    {
                                        bgNG = Color.LightPink;
                                        frNG = Color.Red;
                                    }

                                    _val_INJ_HH = _val_HH_G;
                                    _val_INJ_LL = _val_LL_G;
                                    temp_SPEC = "(+3/-3)";
                                    break;
                                case 11:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                    if (_val_INJ_PV >= _val_LL_Y && _val_INJ_PV <= _val_HH_Y)
                                    {
                                        bgNG = Color.White;
                                        frNG = Color.DodgerBlue;
                                    }
                                    else if (_val_INJ_PV < _val_LL_Y || _val_INJ_PV > _val_HH_Y)
                                    {
                                        bgNG = Color.LightPink;
                                        frNG = Color.Red;
                                    }

                                    _val_INJ_HH = _val_HH_Y;
                                    _val_INJ_LL = _val_LL_Y;
                                    temp_SPEC = "(+5/-5)";
                                    break;
                            }

                            //if (i <= 10)
                            //{
                            //    if (_val_INJ_PV >= _val_LL_G && _val_INJ_PV <= _val_HH_G)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;
                            //    }
                            //    else if ((_val_INJ_PV >= _val_LL_Y && _val_INJ_PV < _val_LL_G) || (_val_INJ_PV > _val_HH_G && _val_INJ_PV <= _val_HH_Y))
                            //    {
                            //        bgNG = Color.Wheat;
                            //        frNG = Color.DarkOrange;
                            //    }
                            //    else if (_val_INJ_AL < _val_LL_Y || _val_INJ_PV > _val_HH_Y)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;
                            //    }

                            //    _val_INJ_HH = _val_HH_G;
                            //    _val_INJ_LL = _val_LL_G;
                            //}
                            //else
                            //{
                            //    if (_val_INJ_PV >= _val_LL_Y && _val_INJ_PV <= _val_HH_Y)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;
                            //    }
                            //    else if (_val_INJ_PV < _val_LL_Y || _val_INJ_PV > _val_HH_Y)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;
                            //    }

                            //    _val_INJ_HH = _val_HH_Y;
                            //    _val_INJ_LL = _val_LL_Y;
                            //}

                            tbl_INJ_MC.BackColor = bgNG;
                            lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = frNG;


                            //if (i <= 10)
                            //{
                            //    if (_val_INJ_PV >= _val_INJ_SV - 3 && _val_INJ_PV <= _val_INJ_SV + 3)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;

                            //        tbl_INJ_MC.BackColor = Color.White;
                            //        lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = Color.DodgerBlue;
                            //    }
                            //    else if ((_val_INJ_PV >= _val_INJ_SV - 5 && _val_INJ_PV < _val_INJ_SV - 3) || (_val_INJ_PV > _val_INJ_SV + 3 && _val_INJ_PV <= _val_INJ_SV + 5))
                            //    {
                            //        bgNG = Color.Orange;
                            //        frNG = Color.DarkOrange;

                            //        tbl_INJ_MC.BackColor = Color.Orange;
                            //        lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = Color.DarkOrange;
                            //    }
                            //    else if (_val_INJ_AL < _val_INJ_SV - 5 || _val_INJ_PV > _val_INJ_SV + 5)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;

                            //        tbl_INJ_MC.BackColor = Color.LightPink;
                            //        lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = Color.Red;
                            //    }
                            //}
                            //else
                            //{
                            //    if (_val_INJ_PV >= _val_INJ_SV - 5 && _val_INJ_PV <= _val_INJ_SV + 5)
                            //    {
                            //        bgNG = Color.White;
                            //        frNG = Color.DodgerBlue;

                            //        tbl_INJ_MC.BackColor = Color.White;
                            //        lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = Color.DodgerBlue;
                            //    }
                            //    else if (_val_INJ_PV < _val_INJ_SV - 5 || _val_INJ_PV > _val_INJ_SV + 5)
                            //    {
                            //        bgNG = Color.LightPink;
                            //        frNG = Color.Red;

                            //        tbl_INJ_MC.BackColor = Color.LightPink;
                            //        lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = Color.Red;
                            //    }
                            //}

                            //tbl_INJ_MC.BackColor = (_val_INJ_AL == 0) ? Color.White : Color.LightPink;
                            //lbl_INJ_NM.BackColor = (_val_INJ_AL == 0) ? Color.LightGreen : Color.Red;
                            ////tbl_INJ_MC.BackColor = (_val_INJ_PV >= _val_INJ_LL && _val_INJ_PV <= _val_INJ_HH) ? Color.White : Color.LightPink;
                            //tbl_INJ_MC.BackColor = bgNG;

                            ////lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = (_val_INJ_PV >= _val_INJ_LL && _val_INJ_PV <= _val_INJ_HH) ? Color.DodgerBlue : Color.Red;
                            //lbl_INJ_PV.ForeColor = lbl_INJ_NM.BackColor = frNG;
                            //lbl_INJ_PV.ForeColor =
                            //    lbl_INJ_SV.ForeColor =
                            //    lbl_INJ_HH.ForeColor =
                            //    lbl_INJ_LL.ForeColor = (tbl_INJ_MC.BackColor == Color.White) ? Color.DodgerBlue : Color.Red;
                            //lbl_INJ_PV.ForeColor = (lbl_INJ_NM.BackColor == Color.DodgerBlue) ? Color.Blue : Color.Red;

                            ////lbl_INJ_SV.Text = (i <= 10) ? "S: " + _val_INJ_SV.ToString() + "\r\n(+3/-3)" : "S: " + _val_INJ_SV.ToString() + "\r\n(+5/-5)";
                            lbl_INJ_SV.Text = "S: " + _val_INJ_SV.ToString() + "\r\n" + temp_SPEC;
                            lbl_INJ_PV.Text = _val_INJ_PV.ToString();
                            lbl_INJ_HH.Text = "H: " + _val_INJ_HH.ToString();
                            lbl_INJ_LL.Text = "L: " + _val_INJ_LL.ToString();
                        }
                        else
                        {
                            tbl_INJ_MC.BackColor = Color.Gainsboro;
                            lbl_INJ_NM.BackColor = Color.Gray;
                            lbl_INJ_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n(No work)";
                            lbl_INJ_SV.Text = "";
                            lbl_INJ_PV.Text = "";
                            lbl_INJ_HH.Text = "";
                            lbl_INJ_LL.Text = "";
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

        public void Fnc_Load_Temp_ASY()
        {
            try
            {

                string prefix_mc = "";
                string prefix_nm = "";
                int prefix_no = 0, pre_SV = 0, pre_HH = 0, pre_LL = 0;

                int listCount = 0, tblCount = 0, rowCount = 0, step01 = 120, step02 = 4;
                string temp_SV = "", temp_PV = "", temp_AL = "";
                string val_SV = "", val_PV = "", val_AL = "";
                int _val_SV = 0, _val_PV = 0, _val_AL = 0, _val_HH = 0, _val_LL = 0;
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
                        _val_HH = (_val_SV + 15);
                        _val_LL = (_val_SV - 15);

                        if (_val_SV > 0)
                        {
                            //tbl_INJ_MC.BackColor = (_val_AL == 0) ? Color.White : Color.LightPink;
                            //lbl_INJ_NM.BackColor = (_val_AL == 0) ? Color.LightGreen : Color.Red;
                            tbl_ASY_MC.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.White : Color.LightPink;

                            lbl_ASY_NM.Text = prefix_mc + " " + String.Format("{0:00}", prefix_no) + " " + prefix_nm;
                            lbl_ASY_PV.ForeColor = lbl_ASY_NM.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.DodgerBlue : Color.Red;
                            //lbl_ASY_PV.ForeColor = lbl_ASY_NM.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.ForestGreen : Color.Red;
                            //lbl_INJ_PV.ForeColor =
                            //    lbl_INJ_SV.ForeColor =
                            //    lbl_INJ_HH.ForeColor =
                            //    lbl_INJ_LL.ForeColor = (tbl_INJ_MC.BackColor == Color.White) ? Color.DodgerBlue : Color.Red;
                            //lbl_INJ_PV.ForeColor = (lbl_INJ_NM.BackColor == Color.DodgerBlue) ? Color.Blue : Color.Red;
                            lbl_ASY_SV.Text = "S: " + _val_SV.ToString() + "\r\n(" + (char)177 + "15)";
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

        public void Fnc_Load_DateTime()
        {
            DateTime dt = DateTime.Now;
            string dtTimer;
            int sec = dt.Second;

            lbl_Hour.Text = String.Format("{0:HH}", dt);
            lbl_Minute.Text = String.Format("{0:mm}", dt);
            //lbl_Date.Text = String.Format("{0:dd/MM/yyyy}", dt);
            lbl_Tick.Visible = (sec % 2 == 0) ? true : false;

            //dtTimer = String.Format("{0:HH:mm:ss}", dt);
            //dtTimer = String.Format("{0:HH:mm:ss}", dt);
            //lbl_Timer.Text = (sec % 2 == 0) ? dtTimer : dtTimer.Replace(":", "");
        }

        public void Fnc_Load_Products_INJ()
        {
            DateTime date = DateTime.Now;
            DateTime dayBegin = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            DateTime dayEnd = new DateTime(date.Year, date.Month, date.Day, 20, 59, 59);
            DateTime nightBegin = new DateTime(date.Year, date.Month, date.Day, 20, 0, 0);
            DateTime nightEnd = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0).AddDays(1);
            //string mcINJ_Name = "";
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
                    for (int i = _mcINJ_Min; i <= _mcINJ_Max; i++)
                    {
                        Label lbl_INJ_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_NM_" + i);
                        lbl_INJ_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n" + ds.Tables[0].Rows[i][4].ToString();

                        //Label lbl_INJ_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_INJ_SV_" + i);
                        //mcINJ_Name = lbl_INJ_NM.Text;
                        //lbl_INJ_NM.Text = "";
                        //lbl_INJ_NM.Text = mcINJ_Name + "\r\n" + ds.Tables[0].Rows[i][4].ToString();

                        //val_PV = lbl_INJ_PV.Text;
                        //_val_PV = (val_PV != "" && val_PV != null) ? Convert.ToInt32(val_PV) : 0;

                        //if (_val_PV > 0)
                        //{
                        //    lbl_INJ_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n" + ds.Tables[0].Rows[i][4].ToString();
                        //}
                        //else
                        //{
                        //    lbl_INJ_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n(No work)";
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



    }
}
