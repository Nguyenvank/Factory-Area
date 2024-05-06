using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Inventory_Data.Ctrl
{
    public partial class uc_PMS_Temperature_Injection_Digits : UserControl
    {

        int _mcMin = 1, _mcMax = 18;


        public uc_PMS_Temperature_Injection_Digits()
        {
            InitializeComponent();


            //cls.SetDoubleBuffer(pnl_0, true);
            cls.SetDoubleBuffer(tbl_0, true);
            //cls.SetDoubleBuffer(tbl_MC_1, true);
            //cls.SetDoubleBuffer(tbl_MC_2, true);
            //cls.SetDoubleBuffer(tbl_MC_3, true);
            //cls.SetDoubleBuffer(tbl_MC_4, true);
            //cls.SetDoubleBuffer(tbl_MC_5, true);
            //cls.SetDoubleBuffer(tbl_MC_6, true);
            //cls.SetDoubleBuffer(tbl_MC_7, true);
            //cls.SetDoubleBuffer(tbl_MC_8, true);
            //cls.SetDoubleBuffer(tbl_MC_9, true);
            //cls.SetDoubleBuffer(tbl_MC_10, true);
            //cls.SetDoubleBuffer(tbl_MC_11, true);
            //cls.SetDoubleBuffer(tbl_MC_12, true);
            //cls.SetDoubleBuffer(tbl_MC_13, true);
            //cls.SetDoubleBuffer(tbl_MC_14, true);
            //cls.SetDoubleBuffer(tbl_MC_15, true);
            //cls.SetDoubleBuffer(tbl_MC_16, true);
            //cls.SetDoubleBuffer(tbl_MC_17, true);
            //cls.SetDoubleBuffer(tbl_MC_18, true);
        }

        private void Uc_PMS_Temperature_Injection_Digits_Load(object sender, EventArgs e)
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
        }


        /**********************************************************/

        public void Fnc_Load_Controls()
        {
            for(int i = _mcMin; i <= _mcMax; i++)
            {
                Label lbl_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_NM_" + i);
                lbl_NM.Text = "RUBBER " + String.Format("{0:00}", i);
                lbl_NM.Font = new Font("Times New Roman", 20, FontStyle.Bold);

                Label lbl_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_PV_" + i);
                //lbl_PV.Text = "180" + (char)176;
                lbl_PV.Text = "180";
                lbl_PV.Font = new Font("Times New Roman", 90, FontStyle.Bold);

                Label lbl_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_SV_" + i);
                lbl_SV.Text = "0";
                lbl_SV.TextAlign = ContentAlignment.MiddleCenter;
                lbl_SV.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_HH_" + i);
                lbl_HH.Text = "0";
                lbl_HH.TextAlign = ContentAlignment.MiddleCenter;
                lbl_HH.Font = new Font("Times New Roman", 11, FontStyle.Bold);

                Label lbl_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_LL_" + i);
                lbl_LL.Text = "0";
                lbl_LL.TextAlign = ContentAlignment.MiddleCenter;
                lbl_LL.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            }
        }

        public void Fnc_Load_Data()
        {
            Thread loadData = new Thread(() =>
            {
                while (true)
                {
                    Fnc_Load_Temp();
                    Thread.Sleep(10000);
                }
            });
            loadData.IsBackground = true;
            loadData.Start();
        }

        public void Fnc_Load_Temp()
        {
            try
            {
                int listCount = 0, tblCount = 0, rowCount = 0, step01 = 0, step02 = 4;
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

                    for (int i = _mcMin; i <= _mcMax; i++)
                    {
                        TableLayoutPanel tbl_MC = (TableLayoutPanel)cls.FindControlRecursive(pnl_0, "tbl_MC_" + i);
                        Label lbl_NM = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_NM_" + i);
                        Label lbl_PV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_PV_" + i);
                        Label lbl_SV = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_SV_" + i);
                        Label lbl_HH = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_HH_" + i);
                        Label lbl_LL = (Label)cls.FindControlRecursive(pnl_0, "lbl_MC_LL_" + i);

                        val_SV = temp_SV.Substring(step01, step02);
                        val_PV = temp_PV.Substring(step01, step02);
                        val_AL = temp_AL.Substring(step01, step02);

                        _val_SV = Convert.ToInt32(val_SV);
                        _val_PV = Convert.ToInt32(val_PV);
                        _val_AL = Convert.ToInt32(val_AL);
                        _val_HH = (i <= 10) ? (_val_SV + 2) : (_val_SV + 5);
                        _val_LL = (i <= 10) ? (_val_SV - 1) : (_val_SV - 5);

                        if (_val_SV > 0)
                        {
                            //tbl_MC.BackColor = (_val_AL == 0) ? Color.White : Color.LightPink;
                            //lbl_NM.BackColor = (_val_AL == 0) ? Color.LightGreen : Color.Red;
                            tbl_MC.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.White : Color.LightPink;
                            lbl_PV.ForeColor = lbl_NM.BackColor = (_val_PV >= _val_LL && _val_PV <= _val_HH) ? Color.DodgerBlue : Color.Red;
                            //lbl_PV.ForeColor =
                            //    lbl_SV.ForeColor =
                            //    lbl_HH.ForeColor =
                            //    lbl_LL.ForeColor = (tbl_MC.BackColor == Color.White) ? Color.DodgerBlue : Color.Red;
                            //lbl_PV.ForeColor = (lbl_NM.BackColor == Color.DodgerBlue) ? Color.Blue : Color.Red;
                            lbl_SV.Text = (i <= 10) ? "S: " + _val_SV.ToString() + "\r\n(+2/-1)" : "S: " + _val_SV.ToString() + "\r\n(+5/-5)";
                            lbl_PV.Text = _val_PV.ToString();
                            lbl_HH.Text = "H: " + _val_HH.ToString();
                            lbl_LL.Text = "L: " + _val_LL.ToString();
                        }
                        else
                        {
                            tbl_MC.BackColor = Color.Gainsboro;
                            lbl_NM.BackColor = Color.Gray;
                            lbl_NM.Text = "RUBBER " + String.Format("{0:00}", i) + "\r\n(No work)";
                            lbl_SV.Text = "";
                            lbl_PV.Text = "";
                            lbl_HH.Text = "";
                            lbl_LL.Text = "";
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

        /**********************************************************/



    }
}
