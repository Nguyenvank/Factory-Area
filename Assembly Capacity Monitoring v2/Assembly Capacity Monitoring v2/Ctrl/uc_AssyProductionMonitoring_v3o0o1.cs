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
    public partial class uc_AssyProductionMonitoring_v3o0o1 : UserControl
    {

        int min = 1, max = 15;
        DateTime _dt;
        string fontTitle = "Arial Black", fontLine = "Arial", fontProd = "Arial", fontValue = "Times New Roman";

        public uc_AssyProductionMonitoring_v3o0o1()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(tableLayoutPanel1, true);
            cls.SetDoubleBuffer(tbl_Line_1, true);
            cls.SetDoubleBuffer(tbl_Line_2, true);
            cls.SetDoubleBuffer(tbl_Line_4, true);
            cls.SetDoubleBuffer(tbl_Line_5, true);
            cls.SetDoubleBuffer(tbl_Line_6, true);
            cls.SetDoubleBuffer(tbl_Line_7, true);
            cls.SetDoubleBuffer(tbl_Line_8, true);
            cls.SetDoubleBuffer(tbl_Line_9, true);
            cls.SetDoubleBuffer(tbl_Line_10, true);
            cls.SetDoubleBuffer(tbl_Line_11, true);
            cls.SetDoubleBuffer(tbl_Line_12, true);
            cls.SetDoubleBuffer(tbl_Line_13, true);
            cls.SetDoubleBuffer(tbl_Line_14, true);
            cls.SetDoubleBuffer(tbl_Line_15, true);
        }

        private void Uc_AssyProductionMonitoring_v3o0o1_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            _dt = DateTime.Now;

            init_Load_Titles();
            init_Load_Controls();
            Fnc_Load_Data();

            Fnc_Load_Data_Refresh();
        }

        public void init_Load_Titles()
        {
            for (int i = 1; i <= 10; i++)
            {
                Label lbl_Title = (Label)cls.FindControlRecursive(panel1, "label" + i);
                lbl_Title.Font = new Font(fontTitle, 11, FontStyle.Bold);
                lbl_Title.ForeColor = Color.Silver;
            }
        }

        public void init_Load_Controls()
        {
            //try
            //{

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{

            //}
            string lbl_Name = "";

            for (int i = min; i <= max; i++)
            {
                switch (i)
                {
                    case 1:
                        lbl_Name = "Auto Balance 01";
                        break;
                    case 2:
                        lbl_Name = "Auto Balance 02";
                        break;
                    case 3:
                        lbl_Name = "";
                        break;
                    case 4:
                        lbl_Name = "Dispenser 01";
                        break;
                    case 5:
                        lbl_Name = "Dispenser 02";
                        break;
                    case 6:
                        lbl_Name = "Pump";
                        break;
                    case 7:
                        lbl_Name = "Weight Balance 01";
                        break;
                    case 8:
                        lbl_Name = "Weight Balance 02";
                        break;
                    case 9:
                        lbl_Name = "Weight Balance 03";
                        break;
                    case 10:
                        lbl_Name = "Weight Balance 04";
                        break;
                    case 11:
                        lbl_Name = "Blower 01";
                        break;
                    case 12:
                        lbl_Name = "Blower 02";
                        break;
                    case 13:
                        lbl_Name = "Welding 01";
                        break;
                    case 14:
                        lbl_Name = "Welding 02";
                        break;
                    case 15:
                        lbl_Name = "Mixing";
                        break;
                }
                if (i != 3)
                {
                    Label lbl_Line = (Label)cls.FindControlRecursive(panel1, "lbl_Line_" + i);
                    Label lbl_Prod = (Label)cls.FindControlRecursive(panel1, "lbl_Prod_" + i);
                    Label lbl_Plan = (Label)cls.FindControlRecursive(panel1, "lbl_Plan_" + i);
                    Label lbl_Target = (Label)cls.FindControlRecursive(panel1, "lbl_Target_" + i);
                    Label lbl_Achieve = (Label)cls.FindControlRecursive(panel1, "lbl_Achieve_" + i);
                    Label lbl_Rate = (Label)cls.FindControlRecursive(panel1, "lbl_Rate_" + i);

                    lbl_Line.Text = lbl_Name.ToUpper();
                    //lbl_Line.Font = new Font("Times New Roman", 18, FontStyle.Bold);
                    lbl_Line.Font = new Font(fontLine, 18, FontStyle.Bold);
                    lbl_Line.ForeColor = Color.YellowGreen;

                    lbl_Prod.Text = "";
                    //lbl_Prod.Font = new Font("Times New Roman", 12, FontStyle.Bold);
                    lbl_Prod.Font = new Font(fontProd, 12, FontStyle.Bold);
                    lbl_Prod.ForeColor = Color.Goldenrod;

                    lbl_Plan.Text = lbl_Target.Text = lbl_Achieve.Text = lbl_Rate.Text = "0";
                    lbl_Plan.Font =
                        lbl_Target.Font =
                        lbl_Achieve.Font =
                        lbl_Rate.Font = new Font(fontValue, 45, FontStyle.Bold);

                    lbl_Plan.ForeColor =
                        lbl_Target.ForeColor =
                        lbl_Achieve.ForeColor =
                        lbl_Rate.ForeColor = Color.Gold;

                    lbl_Line.BackColor =
                        lbl_Prod.BackColor =
                        lbl_Plan.BackColor =
                        lbl_Target.BackColor =
                        lbl_Achieve.BackColor =
                        lbl_Rate.BackColor = Color.Black;
                }
            }
        }

        public Boolean Fnc_Check_Network_Connection()
        {
            Boolean chk = false;

            chk = (check.IsConnectedToInternet() == true) ? true : false;

            return chk;
        }

        public void Fnc_Load_Data()
        {
            try
            {
                int secTimer = _dt.Second;
                int listCount = 0, rowCount = 0, colCount = 0;
                string lineIDx = "", lineName = "", orderIDx = "", prodIDx = "", prodName = "", prodCode = "", timeFr = "", timeTo = "";
                string plan = "", target = "", achieved = "", rate = "", lastUpdate = "", timeout = "";
                int _lineIDx = 0, _plan = 0, _target = 0, _achieved = 0, _rate = 0;
                bool _timeout = false, _timeRest = false;
                Color bgColor=Color.Firebrick;
                DateTime _lastUpdate;
                DateTime date = (_dt.Hour >= 8) ? _dt : _dt.AddDays(-1);
                DateTime rest01_fr, rest01_to, rest02_fr, rest02_to, rest03_fr, rest03_to, rest04_fr, rest04_to;
                rest01_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 11, 50, 00);
                rest01_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 12, 59, 59);
                rest02_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 00, 00);
                rest02_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 40, 00);
                rest03_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 23, 50, 00);
                rest03_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 0, 59, 59);
                rest04_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 00, 00);
                rest04_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 40, 00);
                _timeRest = (cls.isTimeBetween(_dt, rest01_fr, rest01_to) == true ||
                    cls.isTimeBetween(_dt, rest02_fr, rest02_to) == true ||
                    cls.isTimeBetween(_dt, rest03_fr, rest03_to) == true ||
                    cls.isTimeBetween(_dt, rest04_fr, rest04_to) == true) ? true : false;
                string shift = cls.fnGetDate("s");
                string sql = "V2o1_BASE_Capacity_Work_Order_Monitor_3o0o1_Addnew";

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.DateTime;
                sParams[0].ParameterName = "@date";
                sParams[0].Value = date;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "@shift";
                sParams[1].Value = shift;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);

                listCount = ds.Tables.Count;
                rowCount = ds.Tables[0].Rows.Count;
                colCount = ds.Tables[0].Columns.Count;

                if (listCount > 0 && rowCount > 0)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        lineIDx = ds.Tables[0].Rows[i][0].ToString();
                        lineName = ds.Tables[0].Rows[i][1].ToString();
                        orderIDx = ds.Tables[0].Rows[i][2].ToString();
                        prodIDx = ds.Tables[0].Rows[i][3].ToString();
                        prodName = ds.Tables[0].Rows[i][4].ToString();
                        prodCode = ds.Tables[0].Rows[i][5].ToString();
                        timeFr = ds.Tables[0].Rows[i][6].ToString();
                        timeTo = ds.Tables[0].Rows[i][7].ToString();
                        plan = ds.Tables[0].Rows[i][8].ToString().Replace(".0", "");
                        target = ds.Tables[0].Rows[i][9].ToString().Replace(".0", "");
                        achieved = ds.Tables[0].Rows[i][10].ToString().Replace(".0", "");
                        rate = ds.Tables[0].Rows[i][11].ToString().Replace(".0", "");
                        lastUpdate = ds.Tables[0].Rows[i][12].ToString();
                        timeout = ds.Tables[0].Rows[i][13].ToString();

                        _lineIDx = (lineIDx != "" && lineIDx != null) ? Convert.ToInt32(lineIDx) : 0;
                        _plan = (plan != "" && plan != null) ? Convert.ToInt32(plan) : 0;
                        _target = (target != "" && target != null) ? Convert.ToInt32(target) : 0;
                        _achieved = (achieved != "" && achieved != null) ? Convert.ToInt32(achieved) : 0;
                        _rate = (rate != "" && rate != null) ? Convert.ToInt32(rate) : 0;
                        _lastUpdate = (lastUpdate != "" && lastUpdate != null) ? Convert.ToDateTime(lastUpdate) : DateTime.Now;
                        _timeout = (timeout == "1") ? true : false;

                        Label lbl_Line = (Label)cls.FindControlRecursive(panel1, "lbl_Line_" + (lineIDx));
                        Label lbl_Prod = (Label)cls.FindControlRecursive(panel1, "lbl_Prod_" + (lineIDx));
                        Label lbl_Plan = (Label)cls.FindControlRecursive(panel1, "lbl_Plan_" + (lineIDx));
                        Label lbl_Target = (Label)cls.FindControlRecursive(panel1, "lbl_Target_" + (lineIDx));
                        Label lbl_Achieve = (Label)cls.FindControlRecursive(panel1, "lbl_Achieve_" + (lineIDx));
                        Label lbl_Rate = (Label)cls.FindControlRecursive(panel1, "lbl_Rate_" + (lineIDx));

                        //lbl_Line.Font = new Font("Times New Roman", 18, FontStyle.Bold);
                        lbl_Line.Font = new Font(fontLine, 18, FontStyle.Bold);
                        lbl_Line.ForeColor = (orderIDx != "") ? Color.YellowGreen : Color.DimGray;

                        lbl_Prod.Font = new Font(fontProd, 12, FontStyle.Bold);
                        lbl_Prod.ForeColor = (orderIDx != "") ? Color.Goldenrod : Color.DimGray;

                        lbl_Plan.Font =
                            lbl_Target.Font =
                            lbl_Achieve.Font =
                            lbl_Rate.Font = new Font(fontValue, 45, FontStyle.Bold);

                        lbl_Plan.ForeColor =
                            lbl_Target.ForeColor =
                            lbl_Achieve.ForeColor =
                            lbl_Rate.ForeColor = Color.Gold;
                        lbl_Target.ForeColor = Color.Blue;
                        lbl_Achieve.ForeColor = Color.Green;
                        lbl_Rate.ForeColor = (_rate < 95) ? Color.Red : ((_rate >= 95 && _rate <= 105) ? Color.LightGreen : Color.DodgerBlue);

                        lbl_Line.BackColor =
                            lbl_Prod.BackColor =
                            lbl_Plan.BackColor =
                            lbl_Target.BackColor =
                            lbl_Achieve.BackColor =
                            lbl_Rate.BackColor = ((_timeRest == false) && (_timeout == true)) ? ((secTimer % 2 == 0) ? Color.IndianRed : Color.Firebrick) : Color.Black;

                        //Fnc_Load_Data_Alarm(Convert.ToInt32(lineIDx), _timeout);

                        lbl_Prod.Text = (orderIDx != "") ? prodCode : "HAVE NO W.O";
                        lbl_Plan.Text = (orderIDx != "") ? _plan.ToString() : "";
                        lbl_Target.Text = (orderIDx != "") ? _target.ToString() : "";
                        lbl_Achieve.Text = (orderIDx != "") ? _achieved.ToString() : "";
                        lbl_Rate.Text = (orderIDx != "") ? _rate.ToString() + "%" : "";

                        //if (_lineIDx == max) { lbl_Rate.Text = String.Format("{0:00}", secTimer); }

                    }
                }

            }
            catch(SqlException sqlEx)
            {
                //MessageBox.Show(sqlEx.Message);
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        public void Fnc_Load_Data_Alarm(int lineIDx, bool _timeout = false)
        {
            DateTime rest01_fr, rest01_to, rest02_fr, rest02_to, rest03_fr, rest03_to, rest04_fr, rest04_to;
            rest01_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 11, 50, 0);
            rest01_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 12, 59, 59);
            rest02_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 0, 0);
            rest02_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 40, 0);
            rest03_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 23, 50, 0);
            rest03_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 0, 59, 59);
            rest04_fr = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 0, 0);
            rest04_to = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 40, 0);
            int secTimer = _dt.Second;
            bool _timeRest = (cls.isTimeBetween(_dt, rest01_fr, rest01_to) == true ||
                    cls.isTimeBetween(_dt, rest02_fr, rest02_to) == true ||
                    cls.isTimeBetween(_dt, rest03_fr, rest03_to) == true ||
                    cls.isTimeBetween(_dt, rest04_fr, rest04_to) == true) ? true : false;

            //for(int i = min; i <= max; i++)
            //{
            //}

            Label lbl_Line = (Label)cls.FindControlRecursive(panel1, "lbl_Line_" + (lineIDx));
            Label lbl_Prod = (Label)cls.FindControlRecursive(panel1, "lbl_Prod_" + (lineIDx));
            Label lbl_Plan = (Label)cls.FindControlRecursive(panel1, "lbl_Plan_" + (lineIDx));
            Label lbl_Target = (Label)cls.FindControlRecursive(panel1, "lbl_Target_" + (lineIDx));
            Label lbl_Achieve = (Label)cls.FindControlRecursive(panel1, "lbl_Achieve_" + (lineIDx));
            Label lbl_Rate = (Label)cls.FindControlRecursive(panel1, "lbl_Rate_" + (lineIDx));

            //lbl_Line.BackColor =
            //    lbl_Prod.BackColor =
            //    lbl_Plan.BackColor =
            //    lbl_Target.BackColor =
            //    lbl_Achieve.BackColor =
            //    lbl_Rate.BackColor = ((_timeRest == false) && (_timeout == true || lineIDx == 7) && (secTimer % 2 == 0)) ? (BackColor == Color.Firebrick) ? Color.Black : Color.Firebrick : Color.Black;

            if ((_timeRest == false) && (_timeout == true || lineIDx == 7))
            {
                lbl_Line.BackColor =
                    lbl_Prod.BackColor =
                    lbl_Plan.BackColor =
                    lbl_Target.BackColor =
                    lbl_Achieve.BackColor =
                    lbl_Rate.BackColor = (BackColor == Color.DarkRed) ? Color.Firebrick : Color.DarkRed;
            }
            else
            {
                lbl_Line.BackColor =
                    lbl_Prod.BackColor =
                    lbl_Plan.BackColor =
                    lbl_Target.BackColor =
                    lbl_Achieve.BackColor =
                    lbl_Rate.BackColor = Color.Black;
            }
        }

        public void Fnc_Load_Data_Refresh()
        {
            Thread loadDate = new Thread(() =>
            {
                while (true)
                {
                    _dt = DateTime.Now;
                    Thread.Sleep(1000);
                }
            });
            loadDate.IsBackground = true;
            loadDate.Start();

            Thread loadData = new Thread(() =>
            {
                while (true)
                {
                    if (Fnc_Check_Network_Connection() == true)
                    {
                        Fnc_Load_Data();

                        label1.ForeColor = label2.ForeColor = label3.ForeColor = label4.ForeColor = label5.ForeColor = label6.ForeColor = label7.ForeColor = label8.ForeColor = label9.ForeColor = label10.ForeColor = Color.Silver;
                    }
                    else
                    {
                        label1.ForeColor = label2.ForeColor = label3.ForeColor = label4.ForeColor = label5.ForeColor = label6.ForeColor = label7.ForeColor = label8.ForeColor = label9.ForeColor = label10.ForeColor = (_dt.Second % 2 == 0) ? Color.Red : Color.DarkOrange;
                    }
                    Thread.Sleep(1000);
                }
            });
            loadData.IsBackground = true;
            loadData.Start();
        }
    }
}
