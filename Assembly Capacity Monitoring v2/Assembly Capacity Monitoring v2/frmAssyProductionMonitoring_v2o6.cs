using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmAssyProductionMonitoring_v2o6 : Form
    {
        public int _dgvAssyMonitorWidth;

        public frmAssyProductionMonitoring_v2o6()
        {
            InitializeComponent();
        }

        private void frmAssyProductionMonitoring_v2o6_Load(object sender, EventArgs e)
        {
            _dgvAssyMonitorWidth = cls.fnGetDataGridWidth(dgvAssyMonitor);

            fnGetdate();
            fnDataload();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                fnDataload();
                fnCheckStatus();
            }
            fnGetdate();
        }

        public void fnGetdate()
        {
            try
            {
                if (check.IsConnectedToInternet())
                {
                    lblDate.Text = cls.fnGetDate("SD");
                    lblTime.Text = cls.fnGetDate("CT");

                    lblDate.ForeColor = Color.Black;
                    lblTime.ForeColor = Color.Black;
                }
                else
                {
                    lblDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    lblTime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);

                    lblDate.ForeColor = Color.Red;
                    lblTime.ForeColor = Color.Red;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnDataload()
        {
            try
            {
                string sql = "V2o1_BASE_Capacity_Work_Order_Monitor_2o3o6_Addnew";

                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    string stop = dr[24].ToString().ToLower();
                    string note = dr[23].ToString();
                    string rate = dr[28].ToString().Replace("%", "");
                    if(stop=="true")
                    {
                        dr[29] = note;
                    }
                    //int _rate = (rate != "" && rate != null) ? Convert.ToInt32(rate) : 0;
                    //dr[28] = (rate != "" && rate != null) ? ((_rate > 0) ? ((_rate > 110) ? 110 : _rate) + "%" : "0") : "";
                    //string order = dr[15].ToString();
                    //string OK = dr[16].ToString();
                    //string NG = dr[17].ToString();
                    //string total = dr[18].ToString();

                    ////string totalOrder = dr[19].ToString();
                    ////string totalArchieve = dr[20].ToString();

                    ////int _totalOrder = 0;
                    ////int _totalArchieve = 0;
                    ////int _totalRate = 0;

                    ////_totalOrder = (totalOrder != null && totalOrder != "") ? Convert.ToInt32(totalOrder) : 0;
                    ////_totalArchieve = (totalArchieve != null && totalArchieve != "") ? Convert.ToInt32(totalArchieve) : 0;
                    ////_totalRate = (_totalArchieve * 100) / _totalOrder;

                    //dr[15] = (order != null && order != "") ? Convert.ToInt32(order) : 0;
                    //dr[16] = (OK != null && OK != "") ? Convert.ToInt32(OK) : 0;
                    //dr[17] = (NG != null && NG != "") ? Convert.ToInt32(NG) : 0;
                    //dr[18] = (total != null && total != "") ? Convert.ToInt32(total) : 0;
                    ////dr[19] = _totalOrder;
                    ////dr[20] = _totalArchieve;

                    //string rate = dr[21].ToString();
                    //dr[21] = (rate != null && rate != "" && rate != "0") ? rate + "%" : "0%";
                    ////dr[21] = (rate != null && rate != "" && rate != "0") ? _totalRate + "%" : "0%";

                    //string stop = dr[22].ToString();
                    //dr[28] = (stop.ToLower() == "true") ? dr[26].ToString() : dr[28].ToString();

                    //if (dr[7].ToString() != null && dr[7].ToString() != "")
                    //{
                    //    string order = dr[21].ToString();
                    //    string OK = dr[22].ToString();
                    //    string NG = dr[23].ToString();
                    //    string total = dr[24].ToString();

                    //    dr[21] = (order != null && order != "") ? Convert.ToInt32(order) : 0;
                    //    dr[22] = (OK != null && OK != "") ? Convert.ToInt32(OK) : 0;
                    //    dr[23] = (NG != null && NG != "") ? Convert.ToInt32(NG) : 0;
                    //    dr[24] = (total != null && total != "") ? Convert.ToInt32(total) : 0;

                    //    string rate = dr[25].ToString();
                    //    dr[25] = (rate != null && rate != "") ? rate : "0";

                    //    string stop = dr[29].ToString();
                    //    dr[27] = (stop.ToLower() == "true") ? dr[28].ToString() : dr[27].ToString();
                    //}
                    //else
                    //{
                    //    //dr[11] = "N/A";
                    //    //dr[12] = "N/A";
                    //    //dr[20] = "0";
                    //    //dr[21] = "0";
                    //    //dr[22] = "0";
                    //    //dr[23] = "0";
                    //    //dr[24] = "0";
                    //    //dr[25] = "0";
                    //    //dr[27] = "N/A";
                    //}

                    //string act = dr[30].ToString();
                    //dr[31] = (act == "1") ? Properties.Resources.switch2_on : Properties.Resources.switch2_off;
                }

                dgvAssyMonitor.DataSource = dt;
                //dgvAssyMonitor.Refresh();

                _dgvAssyMonitorWidth = cls.fnGetDataGridWidth(dgvAssyMonitor);
                dgvAssyMonitor.Columns[3].HeaderText = "Line";
                dgvAssyMonitor.Columns[11].HeaderText = "Current Product";
                dgvAssyMonitor.Columns[12].HeaderText = "Code";

                //dgvAssyMonitor.Columns[0].Width = 10 * _dgvAssyMonitorWidth / 100;    // [LID]
                //dgvAssyMonitor.Columns[1].Width = 10 * _dgvAssyMonitorWidth / 100;    // [MachineName]
                //dgvAssyMonitor.Columns[2].Width = 12 * _dgvAssyMonitorWidth / 100;    // [MachineCode]
                dgvAssyMonitor.Columns[3].Width = 15 * _dgvAssyMonitorWidth / 100;    // [Line name]
                //dgvAssyMonitor.Columns[4].Width = 10 * _dgvAssyMonitorWidth / 100;    // [LineActive]
                //dgvAssyMonitor.Columns[5].Width = 10 * _dgvAssyMonitorWidth / 100;    // [idleCounting]
                //dgvAssyMonitor.Columns[6].Width = 10 * _dgvAssyMonitorWidth / 100;    // [useBarcode]
                //dgvAssyMonitor.Columns[7].Width = 10 * _dgvAssyMonitorWidth / 100;    // [IDx]
                //dgvAssyMonitor.Columns[8].Width = 10 * _dgvAssyMonitorWidth / 100;    // [AssyDate]
                //dgvAssyMonitor.Columns[9].Width = 10 * _dgvAssyMonitorWidth / 100;    // [AssyShift]
                //dgvAssyMonitor.Columns[10].Width = 18 * _dgvAssyMonitorWidth / 100;    // [PartID]
                dgvAssyMonitor.Columns[11].Width = 18 * _dgvAssyMonitorWidth / 100;     // [PartName]
                dgvAssyMonitor.Columns[12].Width = 13 * _dgvAssyMonitorWidth / 100;    // [PartCode]
                //dgvAssyMonitor.Columns[13].Width = 10 * _dgvAssyMonitorWidth / 100;   // [LineID]
                //dgvAssyMonitor.Columns[14].Width = 10 * _dgvAssyMonitorWidth / 100;   // [LineName]
                //dgvAssyMonitor.Columns[15].Width = 10 * _dgvAssyMonitorWidth / 100;   // [From]
                //dgvAssyMonitor.Columns[16].Width = 10 * _dgvAssyMonitorWidth / 100;   // [To]
                //dgvAssyMonitor.Columns[17].Width = 10 * _dgvAssyMonitorWidth / 100;   // [Clock]
                //dgvAssyMonitor.Columns[18].Width = 5 * _dgvAssyMonitorWidth / 100;    // [Span]
                //dgvAssyMonitor.Columns[19].Width = 5 * _dgvAssyMonitorWidth / 100;    // [UPH]
                //dgvAssyMonitor.Columns[20].Width = 5 * _dgvAssyMonitorWidth / 100;    // [Seq]
                //dgvAssyMonitor.Columns[21].Width = 5 * _dgvAssyMonitorWidth / 100;    // [Order]
                //dgvAssyMonitor.Columns[22].Width = 5 * _dgvAssyMonitorWidth / 100;    // [OK]
                //dgvAssyMonitor.Columns[23].Width = 5 * _dgvAssyMonitorWidth / 100;    // [Rate %]
                //dgvAssyMonitor.Columns[24].Width = 7 * _dgvAssyMonitorWidth / 100;    // [Goal]
                dgvAssyMonitor.Columns[25].Width = 6 * _dgvAssyMonitorWidth / 100;    // [Result]
                dgvAssyMonitor.Columns[26].Width = 6 * _dgvAssyMonitorWidth / 100;    // [Rate %]
                dgvAssyMonitor.Columns[27].Width = 6 * _dgvAssyMonitorWidth / 100;   // [PicID]
                dgvAssyMonitor.Columns[28].Width = 6 * _dgvAssyMonitorWidth / 100;   // [PicName]
                dgvAssyMonitor.Columns[29].Width = 15 * _dgvAssyMonitorWidth / 100;    // [NG]
                //dgvAssyMonitor.Columns[30].Width = 5 * _dgvAssyMonitorWidth / 100;    // [Total]
                dgvAssyMonitor.Columns[31].Width = 15 * _dgvAssyMonitorWidth / 100;   // [Note]
                //dgvAssyMonitor.Columns[32].Width = 10 * _dgvAssyMonitorWidth / 100;   // [Stop]
                //dgvAssyMonitor.Columns[33].Width = 10 * _dgvAssyMonitorWidth / 100;   // [Stop]

                dgvAssyMonitor.Columns[0].Visible = false;
                dgvAssyMonitor.Columns[1].Visible = false;
                dgvAssyMonitor.Columns[2].Visible = false;
                dgvAssyMonitor.Columns[3].Visible = true;
                dgvAssyMonitor.Columns[4].Visible = false;
                dgvAssyMonitor.Columns[5].Visible = false;
                dgvAssyMonitor.Columns[6].Visible = false;
                dgvAssyMonitor.Columns[7].Visible = false;
                dgvAssyMonitor.Columns[8].Visible = false;
                dgvAssyMonitor.Columns[9].Visible = false;
                dgvAssyMonitor.Columns[10].Visible = false;
                dgvAssyMonitor.Columns[11].Visible = true;
                dgvAssyMonitor.Columns[12].Visible = true;
                dgvAssyMonitor.Columns[13].Visible = false;
                dgvAssyMonitor.Columns[14].Visible = false;
                dgvAssyMonitor.Columns[15].Visible = false;
                dgvAssyMonitor.Columns[16].Visible = false;
                dgvAssyMonitor.Columns[17].Visible = false;
                dgvAssyMonitor.Columns[18].Visible = false;
                dgvAssyMonitor.Columns[19].Visible = false;
                dgvAssyMonitor.Columns[20].Visible = false;
                dgvAssyMonitor.Columns[21].Visible = false;
                dgvAssyMonitor.Columns[22].Visible = false;
                dgvAssyMonitor.Columns[23].Visible = false;
                dgvAssyMonitor.Columns[24].Visible = false;
                dgvAssyMonitor.Columns[25].Visible = true;
                dgvAssyMonitor.Columns[26].Visible = true;
                dgvAssyMonitor.Columns[27].Visible = true;
                dgvAssyMonitor.Columns[28].Visible = true;
                dgvAssyMonitor.Columns[29].Visible = true;
                dgvAssyMonitor.Columns[30].Visible = false;
                dgvAssyMonitor.Columns[31].Visible = true;
                dgvAssyMonitor.Columns[32].Visible = false;
                dgvAssyMonitor.Columns[33].Visible = false;

                cls.fnFormatDatagridviewWhite(dgvAssyMonitor, 16, 50);

                //dgvAssyMonitor.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 18, FontStyle.Bold);
                dgvAssyMonitor.DefaultCellStyle.Font = new Font("Arial", 16, FontStyle.Bold);

                //fnCheckStatus();
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
            try
            {
                foreach (DataGridViewRow row in dgvAssyMonitor.Rows)
                {
                    row.Cells[25].Style.BackColor = Color.LightSkyBlue;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvAssyMonitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                fnDataload();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnCheckStatus()
        {
            try
            {
                string stop = "", stopNote = "", picId = "", picName = "";
                string lastUpdate = "", orderDate = "", _orderShift = "", rates = "";
                string orderId = "", orderShift = "", lineId = "", lineName = "";
                string partId = "", partName = "", partCode = "", active = "", timeTo = "";

                string msg = "";

                int _rates = 0;
                int _lineId = 0;

                DateTime _lastUpdate, _orderDate;
                DateTime _dt, _timeTo;
                DateTime _idleLunchStart, _idleLunchStop, _idleDinnerStart, _idleDinnerStop;
                DateTime _idleNightmealStart, _idleNightmealStop;
                DateTime _idleBreakfastStart, _idleBreakfastStop;
                DateTime _shiftEnd;

                _dt = DateTime.Now;
                _idleLunchStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 11, 50, 0);
                _idleLunchStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 12, 59, 59);
                _idleDinnerStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 0, 0);
                _idleDinnerStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 40, 59);
                _idleNightmealStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 23, 50, 0);
                _idleNightmealStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 0, 59, 59);
                _idleBreakfastStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 0, 0);
                _idleBreakfastStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 40, 59);
                _shiftEnd = (cls.fnGetDate("S").ToUpper() == "DAY") ? new DateTime(_dt.Year, _dt.Month, _dt.Day, 19, 59, 59) : new DateTime(_dt.Year, _dt.Month, _dt.Day, 7, 59, 59);

                foreach (DataGridViewRow row in dgvAssyMonitor.Rows)
                {
                    if (row.Cells[7].Value.ToString() != null && row.Cells[7].Value.ToString() != "")
                    {
                        orderId = row.Cells[7].Value.ToString();
                        orderDate = row.Cells[8].Value.ToString();
                        orderShift = row.Cells[9].Value.ToString();
                        lineId = row.Cells[0].Value.ToString();
                        lineName = row.Cells[3].Value.ToString();
                        partId = row.Cells[10].Value.ToString();
                        partName = row.Cells[11].Value.ToString();
                        partCode = row.Cells[12].Value.ToString();
                        picId = row.Cells[30].Value.ToString();
                        picName = row.Cells[31].Value.ToString();
                        stop = row.Cells[24].Value.ToString();
                        stopNote = row.Cells[23].Value.ToString();
                        rates = row.Cells[28].Value.ToString().Replace("%", "");
                        lastUpdate = row.Cells[33].Value.ToString();
                        active = row.Cells[32].Value.ToString();
                        timeTo = row.Cells[16].Value.ToString();

                        //msg = "orderDate: " + orderDate + "\r\n";
                        //msg += "orderShift: " + orderShift + "\r\n";
                        //msg += "lineId: " + lineId + "\r\n";
                        //msg += "lineName: " + lineName + "\r\n";
                        //msg += "partId: " + partId + "\r\n";
                        //msg += "partName: " + partName + "\r\n";
                        //msg += "partCode: " + partCode + "\r\n";
                        //msg += "picId: " + picId + "\r\n";
                        //msg += "picName: " + picName + "\r\n";
                        //msg += "stop: " + stop + "\r\n";
                        //msg += "stopNote: " + stopNote + "\r\n";
                        //msg += "rates: " + rates + "\r\n";
                        //msg += "lastUpdate: " + lastUpdate + "\r\n";
                        //msg += "active: " + active + "\r\n";
                        //msg += "timeTo: " + timeTo + "\r\n";
                        //MessageBox.Show(msg);

                        //row.Cells[27].Value = (stop != "" && stop != "1") ? stopNote : picName;
                        _rates = (rates != null && rates != "") ? Convert.ToInt32(rates) : 0;
                        _lineId = (lineId != null && lineId != "") ? Convert.ToInt32(lineId) : 0;
                        _timeTo = (timeTo != null && timeTo != "") ? Convert.ToDateTime(timeTo) : _dt;

                        _orderDate = (orderDate != null && orderDate != "") ? Convert.ToDateTime(orderDate) : _dt;
                        _orderShift = orderShift.ToUpper();
                        _lastUpdate = (lastUpdate != null && lastUpdate != "") ? Convert.ToDateTime(lastUpdate) : _dt;

                        //switch(_rates)
                        //{
                        //    case 0 - 97:
                        //        row.Cells[28].Style.ForeColor = Color.Firebrick;
                        //        break;
                        //    case 98 - 104:
                        //        row.Cells[28].Style.ForeColor = Color.Green;
                        //        break;
                        //    case 105 - 1000:
                        //        row.Cells[28].Style.ForeColor = Color.DarkBlue;
                        //        break;
                        //}

                        if (_rates < 95)
                            row.Cells[28].Style.ForeColor = Color.Firebrick;
                        else if (_rates >= 95 && _rates < 105)
                            row.Cells[28].Style.ForeColor = Color.Green;
                        else if (_rates >= 105)
                            row.Cells[28].Style.ForeColor = Color.DarkBlue;

                        row.Cells[25].Style.ForeColor = Color.Firebrick;
                        row.Cells[26].Style.ForeColor = Color.DarkBlue;
                        row.Cells[27].Style.ForeColor = Color.Green;
                        row.Cells[29].Style.ForeColor = Color.Black;

                        //row.Cells[31].Value = (active == "1") ? Properties.Resources.switch2_on : Properties.Resources.switch2_off;

                        if (stop.ToLower() == "true")
                        {
                            row.Cells[25].Style.ForeColor = Color.Firebrick;
                            row.Cells[26].Style.ForeColor = Color.DarkBlue;
                            row.Cells[27].Style.ForeColor = Color.Green;
                            row.Cells[29].Style.ForeColor = Color.Black;

                            row.DefaultCellStyle.BackColor = Color.LightPink;
                            //row.DefaultCellStyle.ForeColor = Color.White;
                            //row.Cells[18].Style.BackColor = Color.LightPink;
                        }
                        else
                        {
                            _orderDate = (orderDate != null && orderDate != "") ? Convert.ToDateTime(orderDate) : _dt;
                            _orderShift = orderShift.ToUpper();
                            _lastUpdate = (lastUpdate != null && lastUpdate != "") ? Convert.ToDateTime(lastUpdate) : _dt;
                            if (_lastUpdate < _dt && _orderShift == cls.fnGetDate("S").ToUpper())
                            {
                                TimeSpan idleTime = _dt - _lastUpdate;
                                double totalMinutes = idleTime.Minutes;
                                //if (totalMinutes >= 5 && active == "1" && lineId != "15")
                                if (totalMinutes >= 5 && active == "1" && lineId != "0")
                                {
                                    if (
                                        (cls.isTimeBetween(_dt, _idleLunchStart, _idleLunchStop) == true)
                                        || (cls.isTimeBetween(_dt, _idleDinnerStart, _idleDinnerStop) == true)
                                        || (cls.isTimeBetween(_dt, _idleNightmealStart, _idleNightmealStop) == true)
                                        || (cls.isTimeBetween(_dt, _idleBreakfastStart, _idleBreakfastStop) == true)
                                        )
                                    {
                                        row.Cells[25].Style.ForeColor = Color.Firebrick;
                                        row.Cells[26].Style.ForeColor = Color.DarkBlue;
                                        row.Cells[27].Style.ForeColor = Color.Green;
                                        row.Cells[29].Style.ForeColor = Color.Black;

                                        //row.DefaultCellStyle.BackColor = Color.White;
                                        fnIdleTimeStop(orderId);
                                    }
                                    else
                                    {
                                        row.Cells[25].Style.ForeColor = Color.Firebrick;
                                        row.Cells[26].Style.ForeColor = Color.DarkBlue;
                                        row.Cells[27].Style.ForeColor = Color.Green;
                                        row.Cells[29].Style.ForeColor = Color.Black;

                                        row.DefaultCellStyle.BackColor = Color.LightPink;
                                        //row.Cells[18].Style.BackColor = Color.LightPink;

                                        //MessageBox.Show(orderId + "\r\n" + orderDate + "\r\n" + orderShift + "\r\n" + partId + "\r\n" + partName + "\r\n" + partCode + "\r\n" + lineId + "\r\n" + lineName);
                                        fnIdleTimeStart(orderId, orderDate, orderShift, partId, partName, partCode, lineId, lineName);
                                    }
                                }
                                else
                                {
                                    fnIdleTimeStop(orderId);
                                }
                            }
                        }
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

        public void fnIdleTimeStart (string orderID, string orderDate, string orderShift, string partID, string partName, string partCode, string lineID, string lineName)
        {
            try
            {
                DateTime idleStart = DateTime.Now;
                string sql = "V2o1_BASE_Capacity_Work_Order_Monitor_IdleTimeStart_AddItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[8]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderID;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.DateTime;
                sParams[1].ParameterName = "@orderDate";
                sParams[1].Value = orderDate;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.VarChar;
                sParams[2].ParameterName = "@orderShift";
                sParams[2].Value = orderShift;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.Int;
                sParams[3].ParameterName = "@partID";
                sParams[3].Value = partID;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.NVarChar;
                sParams[4].ParameterName = "@partName";
                sParams[4].Value = partName;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.VarChar;
                sParams[5].ParameterName = "@partCode";
                sParams[5].Value = partCode;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.Int;
                sParams[6].ParameterName = "@lineID";
                sParams[6].Value = lineID;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.NVarChar;
                sParams[7].ParameterName = "@lineName";
                sParams[7].Value = lineName;

                cls.fnUpdDel(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }

        }

        public void fnIdleTimeStop (string orderID)
        {
            try
            {
                DateTime idleStop = DateTime.Now;
                string sql = "V2o1_BASE_Capacity_Work_Order_Monitor_IdleTimeStop_AddItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderID;

                //sParams[1] = new SqlParameter();
                //sParams[1].SqlDbType = SqlDbType.DateTime;
                //sParams[1].ParameterName = "@orderDate";
                //sParams[1].Value = orderDate;

                //sParams[2] = new SqlParameter();
                //sParams[2].SqlDbType = SqlDbType.VarChar;
                //sParams[2].ParameterName = "@orderShift";
                //sParams[2].Value = orderShift;

                //sParams[3] = new SqlParameter();
                //sParams[3].SqlDbType = SqlDbType.DateTime;
                //sParams[3].ParameterName = "@orderAddDate";
                //sParams[3].Value = DateTime.Now;

                //sParams[4] = new SqlParameter();
                //sParams[4].SqlDbType = SqlDbType.Int;
                //sParams[4].ParameterName = "@partID";
                //sParams[4].Value = partID;

                //sParams[5] = new SqlParameter();
                //sParams[5].SqlDbType = SqlDbType.NVarChar;
                //sParams[5].ParameterName = "@partName";
                //sParams[5].Value = partName;

                //sParams[6] = new SqlParameter();
                //sParams[6].SqlDbType = SqlDbType.VarChar;
                //sParams[6].ParameterName = "@partCode";
                //sParams[6].Value = partCode;

                //sParams[7] = new SqlParameter();
                //sParams[7].SqlDbType = SqlDbType.Int;
                //sParams[7].ParameterName = "@lineID";
                //sParams[7].Value = lineID;

                //sParams[8] = new SqlParameter();
                //sParams[8].SqlDbType = SqlDbType.NVarChar;
                //sParams[8].ParameterName = "@lineName";
                //sParams[8].Value = lineName;

                cls.fnUpdDel(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }

        }
    }
}
