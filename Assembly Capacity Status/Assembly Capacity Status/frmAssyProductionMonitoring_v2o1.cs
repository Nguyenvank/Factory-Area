using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmAssyProductionMonitoring_v2o1 : Form
    {
        public int _dgvAssyMonitorWidth;
        private DateTime _dt = DateTime.Now;

        public frmAssyProductionMonitoring_v2o1()
        {
            InitializeComponent();
        }

        private void frmAssyProductionMonitoring_v2o1_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet() == true)
            {
                _dgvAssyMonitorWidth = cls.fnGetDataGridWidth(dgvAssyMonitor);
            }

            fnGetDate();
            dgvAssyMonitor.ClearSelection();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet() == true)
            {
                fnBindAssy();
                fnStatus();
            }
        }

        public void init()
        {
            _dgvAssyMonitorWidth = cls.fnGetDataGridWidth(dgvAssyMonitor);

            fnGetDate();
            fnBindAssy();

            dgvAssyMonitor.BackgroundColor = Color.LightYellow;
        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
            if (check.IsConnectedToInternet() == false)
            {
                lblDate.ForeColor = Color.Red;
                lblTime.ForeColor = Color.Red;
            }
            else
            {
                lblDate.ForeColor = Color.Black;
                lblTime.ForeColor = Color.Black;
            }
        }

        public void fnBindAssy()
        {
            try
            {
                string sql = "";
                sql = "V2o1_BASE_Capacity_Work_Order_Monitor_Addnew";

                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    string order = dr[15].ToString();
                    string OK = dr[16].ToString();
                    string NG = dr[17].ToString();
                    string total = dr[18].ToString();

                    dr[15] = (order != null && order != "") ? Convert.ToInt32(order) : 0;
                    dr[16] = (OK != null && OK != "") ? Convert.ToInt32(OK) : 0;
                    dr[17] = (NG != null && NG != "") ? Convert.ToInt32(NG) : 0;
                    dr[18] = (total != null && total != "") ? Convert.ToInt32(total) : 0;

                    string rate = dr[19].ToString();
                    dr[19] = (rate != null && rate != "" && rate != "0") ? rate + "%" : "0%";

                    string stop = dr[20].ToString();
                    dr[26] = (stop.ToLower() == "true") ? dr[24].ToString() : dr[26].ToString();
                }

                dgvAssyMonitor.DataSource = dt;
                dgvAssyMonitor.Refresh();


                //dgvAssyMonitor.Columns[0].Width = 10 * _dgvAssyMonitorWidth / 100;    // idx
                //dgvAssyMonitor.Columns[1].Width = 10 * _dgvAssyMonitorWidth / 100;    // Machine
                dgvAssyMonitor.Columns[2].Width = 13 * _dgvAssyMonitorWidth / 100;      // Line
                //dgvAssyMonitor.Columns[3].Width = 10 * _dgvAssyMonitorWidth / 100;    // orderID
                //dgvAssyMonitor.Columns[4].Width = 10 * _dgvAssyMonitorWidth / 100;    // date
                //dgvAssyMonitor.Columns[4].Width = 10 * _dgvAssyMonitorWidth / 100;    // shift
                //dgvAssyMonitor.Columns[5].Width = 10 * _dgvAssyMonitorWidth / 100;    // prodID
                dgvAssyMonitor.Columns[7].Width = 20 * _dgvAssyMonitorWidth / 100;      // Current product
                dgvAssyMonitor.Columns[8].Width = 12 * _dgvAssyMonitorWidth / 100;      // Code
                //dgvAssyMonitor.Columns[9].Width = 10 * _dgvAssyMonitorWidth / 100;    // from
                //dgvAssyMonitor.Columns[10].Width = 10 * _dgvAssyMonitorWidth / 100;   // to
                //dgvAssyMonitor.Columns[11].Width = 10 * _dgvAssyMonitorWidth / 100;   // clock
                //dgvAssyMonitor.Columns[12].Width = 10 * _dgvAssyMonitorWidth / 100;   // span
                //dgvAssyMonitor.Columns[13].Width = 10 * _dgvAssyMonitorWidth / 100;   // uph
                //dgvAssyMonitor.Columns[14].Width = 10 * _dgvAssyMonitorWidth / 100;   // seq
                dgvAssyMonitor.Columns[15].Width = 7 * _dgvAssyMonitorWidth / 100;     // order
                dgvAssyMonitor.Columns[16].Width = 7 * _dgvAssyMonitorWidth / 100;     // OK
                dgvAssyMonitor.Columns[17].Width = 7 * _dgvAssyMonitorWidth / 100;     // NG
                dgvAssyMonitor.Columns[18].Width = 7 * _dgvAssyMonitorWidth / 100;     // total
                dgvAssyMonitor.Columns[19].Width = 7 * _dgvAssyMonitorWidth / 100;     // rate
                //dgvAssyMonitor.Columns[20].Width = 10 * _dgvAssyMonitorWidth / 100;   // stop
                //dgvAssyMonitor.Columns[21].Width = 10 * _dgvAssyMonitorWidth / 100;   // dateadd
                //dgvAssyMonitor.Columns[22].Width = 10 * _dgvAssyMonitorWidth / 100;   // active
                //dgvAssyMonitor.Columns[23].Width = 10 * _dgvAssyMonitorWidth / 100;   // last update
                //dgvAssyMonitor.Columns[24].Width = 10 * _dgvAssyMonitorWidth / 100;   // note
                //dgvAssyMonitor.Columns[25].Width = 10 * _dgvAssyMonitorWidth / 100;   // picId
                dgvAssyMonitor.Columns[26].Width = 20 * _dgvAssyMonitorWidth / 100;   // picName
                //dgvAssyMonitor.Columns[27].Width = 3 * _dgvAssyMonitorWidth / 100;   // active

                dgvAssyMonitor.Columns[0].Visible = false;
                dgvAssyMonitor.Columns[1].Visible = false;
                dgvAssyMonitor.Columns[2].Visible = true;
                dgvAssyMonitor.Columns[3].Visible = false;
                dgvAssyMonitor.Columns[4].Visible = false;
                dgvAssyMonitor.Columns[5].Visible = false;
                dgvAssyMonitor.Columns[6].Visible = false;
                dgvAssyMonitor.Columns[7].Visible = true;
                dgvAssyMonitor.Columns[8].Visible = true;
                dgvAssyMonitor.Columns[9].Visible = false;
                dgvAssyMonitor.Columns[10].Visible = false;
                dgvAssyMonitor.Columns[11].Visible = false;
                dgvAssyMonitor.Columns[12].Visible = false;
                dgvAssyMonitor.Columns[13].Visible = false;
                dgvAssyMonitor.Columns[14].Visible = false;
                dgvAssyMonitor.Columns[15].Visible = true;
                dgvAssyMonitor.Columns[16].Visible = true;
                dgvAssyMonitor.Columns[17].Visible = true;
                dgvAssyMonitor.Columns[18].Visible = true;
                dgvAssyMonitor.Columns[19].Visible = true;
                dgvAssyMonitor.Columns[20].Visible = false;
                dgvAssyMonitor.Columns[21].Visible = false;
                dgvAssyMonitor.Columns[22].Visible = false;
                dgvAssyMonitor.Columns[23].Visible = false;
                dgvAssyMonitor.Columns[24].Visible = false;
                dgvAssyMonitor.Columns[25].Visible = false;
                dgvAssyMonitor.Columns[26].Visible = true;
                dgvAssyMonitor.Columns[27].Visible = false;

                cls.fnFormatDatagridviewWhite(dgvAssyMonitor, 18, 50);
            }
            catch
            {

            }
            finally
            {

            }

        }

        private void dgvAssyMonitor_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvAssyMonitor.ClearSelection();

            string idx = "", prodId = "", stop = "", stopNote = "", picId = "", picName = "";
            byte _stop = 0;
            string lastUpdate = "", orderDate = "", _orderShift = "", _rate = "", rates = "";
            double _rates = 0;
            int rate = 0, _lineId = 0;
            DateTime _lastUpdate, nowUpdate;
            string orderId = "", orderShift = "", idleNote = "", lineId = "", lineName = "";
            string partId = "", partName = "", partCode = "", active = "", timeTo = "";
            DateTime _orderDate, _idleStop;
            DateTime dt, _timeTo;
            DateTime _idleLunchStart, _idleLunchStop, _idleDinnerStart, _idleDinnerStop;
            DateTime _idleNightmealStart, _idleNightmealStop;
            DateTime _idleBreakfastStart, _idleBreakfastStop;
            DateTime _shiftEnd;
            TimeSpan ts;
            dt = DateTime.Now;
            _idleLunchStart = new DateTime(dt.Year, dt.Month, dt.Day, 11, 50, 0);
            _idleLunchStop = new DateTime(dt.Year, dt.Month, dt.Day, 12, 59, 59);
            _idleDinnerStart = new DateTime(dt.Year, dt.Month, dt.Day, 17, 0, 0);
            _idleDinnerStop = new DateTime(dt.Year, dt.Month, dt.Day, 17, 40, 59);
            _idleNightmealStart = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
            _idleNightmealStop = new DateTime(dt.Year, dt.Month, dt.Day, 0, 59, 59);
            _idleBreakfastStart = new DateTime(dt.Year, dt.Month, dt.Day, 5, 0, 0);
            _idleBreakfastStop = new DateTime(dt.Year, dt.Month, dt.Day, 5, 40, 59);
            _shiftEnd = (cls.fnGetDate("S").ToUpper() == "DAY") ? new DateTime(dt.Year, dt.Month, dt.Day, 19, 59, 59) : new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 59);


            foreach (DataGridViewRow row in dgvAssyMonitor.Rows)
            {
                orderId = row.Cells[3].Value.ToString();
                orderDate = row.Cells[4].Value.ToString();
                orderShift = row.Cells[5].Value.ToString();
                lineId = row.Cells[0].Value.ToString();
                lineName = row.Cells[2].Value.ToString();
                partId = row.Cells[6].Value.ToString();
                partName = row.Cells[7].Value.ToString();
                partCode = row.Cells[8].Value.ToString();
                picId = row.Cells[25].Value.ToString();
                picName = row.Cells[26].Value.ToString();
                stop = row.Cells[20].Value.ToString();
                stopNote = row.Cells[24].Value.ToString();
                rates = row.Cells[19].Value.ToString().Replace("%", "");
                lastUpdate = row.Cells[23].Value.ToString();
                active = row.Cells[27].Value.ToString();
                timeTo = row.Cells[10].Value.ToString();

                //row.Cells[27].Value = (stop != "" && stop != "1") ? stopNote : picName;
                _rates = (rates != null && rates != "") ? Convert.ToDouble(rates) : 0;
                _lineId = (lineId != null && lineId != "") ? Convert.ToInt32(lineId) : 0;
                _timeTo = (timeTo != null && timeTo != "") ? Convert.ToDateTime(timeTo) : dt;

                _orderDate = (orderDate != null && orderDate != "") ? Convert.ToDateTime(orderDate) : _dt;
                _orderShift = orderShift.ToUpper();
                _lastUpdate = (lastUpdate != null && lastUpdate != "") ? Convert.ToDateTime(lastUpdate) : _dt;

                ////////////////////////////////////////////

                _orderDate = (orderDate != null && orderDate != "") ? Convert.ToDateTime(orderDate) : _dt;
                _orderShift = orderShift.ToUpper();
                _lastUpdate = (lastUpdate != null && lastUpdate != "") ? Convert.ToDateTime(lastUpdate) : _dt;

                if (_lastUpdate < _dt && _orderShift == cls.fnGetDate("S").ToUpper())
                {
                    TimeSpan idleTime = _dt - _lastUpdate;
                    double totalMinutes = idleTime.Minutes;
                    if (totalMinutes >= 5)
                    {
                        //if (
                        //    (cls.isTimeBetween(dt, _idleLunchStart, _idleLunchStop) == true)
                        //    || (cls.isTimeBetween(dt, _idleDinnerStart, _idleDinnerStop) == true)
                        //    || (cls.isTimeBetween(dt, _idleNightmealStart, _idleNightmealStop) == true)
                        //    || (cls.isTimeBetween(dt, _idleBreakfastStart, _idleBreakfastStop) == true)
                        //    )
                        //{
                        //    row.DefaultCellStyle.BackColor = Color.White;
                        //    fnIdleTime("stop", orderId, _orderDate, orderShift, partId, partName, partCode, stopNote);
                        //}
                        //else
                        //{
                        //    //if (_timeTo.Subtract(_shiftEnd).TotalMinutes >= 0)
                        //    //{
                        //    //    row.DefaultCellStyle.BackColor = Color.Yellow;
                        //    //}
                        //    //else
                        //    //{
                        //    //    row.DefaultCellStyle.BackColor = Color.Firebrick;
                        //    //}
                        //    row.DefaultCellStyle.BackColor = Color.Firebrick;
                        //}
                        row.DefaultCellStyle.BackColor = Color.Firebrick;
                    }
                    //else
                    //{
                    //    fnIdleTime("start", orderId, _orderDate, orderShift, partId, partName, partCode, stopNote);
                    //}
                }

                //if (stop.ToLower() == "true")
                //{
                //    row.DefaultCellStyle.BackColor = Color.Firebrick;
                //    row.DefaultCellStyle.ForeColor = Color.Black;
                //    fnIdleTime("stop", orderId, _orderDate, orderShift, partId, partName, partCode, stopNote);
                //}
                //else
                //{

                //}

                if (
                    (cls.isTimeBetween(dt, _idleLunchStart, _idleLunchStop) == true)
                    || (cls.isTimeBetween(dt, _idleDinnerStart, _idleDinnerStop) == true)
                    || (cls.isTimeBetween(dt, _idleNightmealStart, _idleNightmealStop) == true)
                    || (cls.isTimeBetween(dt, _idleBreakfastStart, _idleBreakfastStop) == true)
                    )
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    //fnIdleTime("stop", orderId, _orderDate, orderShift, partId, partName, partCode, stopNote);
                }


                //if (_lineId > 6)
                if (active.ToLower() == "false")
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }

                if (_rates >= 100)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }




                //if (_rates >= 100)
                //{
                //    row.Cells[17].Style.ForeColor = (row.DefaultCellStyle.BackColor == Color.Firebrick) ? Color.Black : Color.Firebrick;
                //    row.Cells[19].Style.BackColor = Color.YellowGreen;
                //}
                //else
                //{

                //}
                row.Cells[17].Style.ForeColor = (row.DefaultCellStyle.BackColor == Color.Firebrick) ? Color.Black : Color.Firebrick;
                row.Cells[19].Style.BackColor = Color.YellowGreen;
            }
            dgvAssyMonitor.Refresh();
        }

        private void dgvAssyMonitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAssyMonitor.Refresh();
            if (check.IsConnectedToInternet() == true)
            {
                dgvAssyMonitor.ClearSelection();
                fnBindAssy();
            }
        }
        
        public void fnStatus()
        {
            try
            {

                //string sql1 = "V2o1_BASE_Capacity_Work_Order_Monitor_Status_Addnew";
                //DataSet dsBlue = new DataSet();
                //dsBlue = cls.ExecuteDataSet(sql1, CommandType.StoredProcedure);
                //string idxBlue = dsBlue.Tables[0].Rows[0][0].ToString();
                //string assyLineBlue = dsBlue.Tables[0].Rows[0][1].ToString();
                //string lastUpdateBlue = String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Convert.ToDateTime(dsBlue.Tables[0].Rows[0][2].ToString()));

                //tssStatus.Text = "";
                //if (dsBlue.Tables[0].Rows.Count > 0)
                //{
                //    tssStatus.Text = "Line " + assyLineBlue + " is updated at " + lastUpdateBlue + " successful.";
                //    tssStatus.ForeColor = Color.Blue;
                //}
                //else
                //{
                //    tssStatus.Text = "Gathering data...";
                //}

                //string sql2 = "V2o1_BASE_Capacity_Work_Order_Monitor_RedSignal_Addnew";
                //DataSet dsRed = new DataSet();
                //dsRed = cls.ExecuteDataSet(sql2, CommandType.StoredProcedure);
                //string idxRed = dsRed.Tables[0].Rows[0][0].ToString();
                //string assyLineRed = dsRed.Tables[0].Rows[0][1].ToString();
                //string lastUpdateRed = String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", Convert.ToDateTime(dsRed.Tables[0].Rows[0][2].ToString()));

                //tssStatusRed.Text = "";
                //if (dsRed.Tables[0].Rows.Count > 0)
                //{
                //    tssStatusRed.Text = "The last update is " + assyLineRed + " at " + lastUpdateRed + ".";
                //    tssStatusRed.ForeColor = Color.Firebrick;
                //}
                //else
                //{
                //    tssStatusRed.Text = "Gathering data...";
                //}
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnIdleTime(string idleType, string orderId, DateTime assyDate, string assyShift, string prodId, string prodName, string prodCode, string stopNote)
        {
            string sql = "V2o1_BASE_Capacity_Work_Order_IdleTime_Addnew";
            SqlParameter[] sParams = new SqlParameter[8]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.VarChar;
            sParams[0].ParameterName = "idleType";
            sParams[0].Value = idleType;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "orderID";
            sParams[1].Value = orderId;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.DateTime;
            sParams[2].ParameterName = "assyDate";
            sParams[2].Value = assyDate;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.VarChar;
            sParams[3].ParameterName = "assyShift";
            sParams[3].Value = assyShift;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.Int;
            sParams[4].ParameterName = "prodId";
            sParams[4].Value = prodId;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.NVarChar;
            sParams[5].ParameterName = "prodName";
            sParams[5].Value = prodName;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "prodCode";
            sParams[6].Value = prodCode;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.NVarChar;
            sParams[7].ParameterName = "stopNote";
            sParams[7].Value = stopNote;

            cls.fnUpdDel(sql, sParams);
        }
    }
}
