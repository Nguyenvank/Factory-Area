using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmAssyProductionMonitoring_v2 : Form
    {
        public int _dgvAssyMonitorWidth;


        public frmAssyProductionMonitoring_v2()
        {
            InitializeComponent();
        }

        private void frmAssyProductionMonitoring_v2_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvAssyMonitorWidth = cls.fnGetDataGridWidth(dgvAssyMonitor);

            fnGetDate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            fnBindAssy();
        }

        public void init()
        {
            _dgvAssyMonitorWidth = cls.fnGetDataGridWidth(dgvAssyMonitor);

            fnGetDate();
            fnBindAssy();
        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
        }

        public void fnBindAssy()
        {
            string sql = "";
            sql = "V2_BASE_CAPACITY_WORK_ORDER_MONITOR_2_ADDNEW";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            dgvAssyMonitor.DataSource = dt;
            dgvAssyMonitor.Refresh();

            //for (int i = 0; i < dgvAssyMonitor.Rows.Count; i++)
            //{
            //    dgvAssyMonitor.Rows[i].Height = (dgvAssyMonitor.Height - dgvAssyMonitor.Columns[0].HeaderCell.Size.Height) / dgvAssyMonitor.Rows.Count;
            //}

            //dgvAssyMonitor.Columns[0].Width = 10 * _dgvAssyMonitorWidth / 100;  // idx
            //dgvAssyMonitor.Columns[1].Width = 15 * _dgvAssyMonitorWidth / 100;  // assyDate
            //dgvAssyMonitor.Columns[2].Width = 10 * _dgvAssyMonitorWidth / 100;  // assyShift
            //dgvAssyMonitor.Columns[3].Width = 50 * _dgvAssyMonitorWidth / 100;  // lineId
            dgvAssyMonitor.Columns[4].Width = 15 * _dgvAssyMonitorWidth / 100;    // assyLine
            //dgvAssyMonitor.Columns[5].Width = 8 * _dgvAssyMonitorWidth / 100;   // prodId
            dgvAssyMonitor.Columns[6].Width = 20 * _dgvAssyMonitorWidth / 100;     // current product
            dgvAssyMonitor.Columns[7].Width = 8 * _dgvAssyMonitorWidth / 100;     // Total order
            dgvAssyMonitor.Columns[8].Width = 8 * _dgvAssyMonitorWidth / 100;     // OK
            dgvAssyMonitor.Columns[9].Width = 8 * _dgvAssyMonitorWidth / 100;    // NG
            dgvAssyMonitor.Columns[10].Width = 8 * _dgvAssyMonitorWidth / 100;    // Achieve
            dgvAssyMonitor.Columns[11].Width = 8 * _dgvAssyMonitorWidth / 100;    // rate
            dgvAssyMonitor.Columns[12].Width = 25 * _dgvAssyMonitorWidth / 100;    // PIC
            //dgvAssyMonitor.Columns[13].Width = 25 * _dgvAssyMonitorWidth / 100;    // stop
            //dgvAssyMonitor.Columns[14].Width = 25 * _dgvAssyMonitorWidth / 100;    // last update
            //dgvAssyMonitor.Columns[15].Width = 25 * _dgvAssyMonitorWidth / 100;    // status

            dgvAssyMonitor.Columns[0].Visible = false;
            dgvAssyMonitor.Columns[1].Visible = false;
            dgvAssyMonitor.Columns[2].Visible = false;
            dgvAssyMonitor.Columns[3].Visible = false;
            dgvAssyMonitor.Columns[4].Visible = true;
            dgvAssyMonitor.Columns[5].Visible = false;
            dgvAssyMonitor.Columns[6].Visible = true;
            dgvAssyMonitor.Columns[7].Visible = true;
            dgvAssyMonitor.Columns[8].Visible = true;
            dgvAssyMonitor.Columns[9].Visible = true;
            dgvAssyMonitor.Columns[10].Visible = true;
            dgvAssyMonitor.Columns[11].Visible = true;
            dgvAssyMonitor.Columns[12].Visible = true;
            dgvAssyMonitor.Columns[13].Visible = false;
            dgvAssyMonitor.Columns[14].Visible = false;
            dgvAssyMonitor.Columns[15].Visible = false;

            cls.fnFormatDatagridview(dgvAssyMonitor, 20,70);
        }

        private void dgvAssyMonitor_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvAssyMonitor.ClearSelection();

            string idx = "", prodId = "", stop = "";
            byte _stop = 0;
            string _lastUpdate = "", orderDate = "", ordershift = "", _rate = "";
            int rate = 0;
            DateTime lastUpdate, nowUpdate;
            string orderId = "", orderShift = "", idleNote = "";
            string partId = "", partName = "", partCode = "";
            DateTime _orderDate, _idleStop;
            DateTime dt;
            DateTime _idleLunchStart, _idleLunchStop, _idleDinnerStart, _idleDinnerStop;
            DateTime _idleNightmealStart, _idleNightmealStop;
            DateTime _idleBreakfastStart, _idleBreakfastStop;
            dt = DateTime.Now;
            _idleLunchStart = new DateTime(dt.Year, dt.Month, dt.Day, 11, 50, 0);
            _idleLunchStop = new DateTime(dt.Year, dt.Month, dt.Day, 12, 59, 59);
            _idleDinnerStart = new DateTime(dt.Year, dt.Month, dt.Day, 17, 0, 0);
            _idleDinnerStop = new DateTime(dt.Year, dt.Month, dt.Day, 17, 40, 59);
            _idleNightmealStart = new DateTime(dt.Year, dt.Month, dt.Day, 23, 50, 0);
            _idleNightmealStop = new DateTime(dt.Year, dt.Month, dt.Day, 0, 59, 59);
            _idleBreakfastStart = new DateTime(dt.Year, dt.Month, dt.Day, 5, 0, 0);
            _idleBreakfastStop = new DateTime(dt.Year, dt.Month, dt.Day, 5, 40, 59);

            foreach (DataGridViewRow row in dgvAssyMonitor.Rows)
            {
                orderId = row.Cells[0].Value.ToString();
                orderDate = row.Cells[1].Value.ToString();
                _orderDate = (orderDate != "N/A") ? Convert.ToDateTime(orderDate) : DateTime.Now;
                ordershift = row.Cells[2].Value.ToString();
                partId = row.Cells[5].Value.ToString();
                partName = row.Cells[6].Value.ToString();
                partCode = "";
                _idleStop = (row.Cells[14].Value.ToString() != "N/A") ? Convert.ToDateTime(row.Cells[14].Value.ToString()) : DateTime.Now;
                idleNote = row.Cells[12].Value.ToString();

                _rate = row.Cells[11].Value.ToString().Replace("%", "");
                rate = (_rate != "" && _rate != null) ? Convert.ToInt32(_rate) : 0;
                if (rate >= 100)
                {
                    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                    //row.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    if (orderDate == "N/A" && Convert.ToInt32(row.Cells[3].Value.ToString()) > 6)
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        stop = row.Cells[13].Value.ToString().ToLower();
                        if (stop == "true")
                        {
                            row.DefaultCellStyle.BackColor = Color.Tomato;

                            //fnIdleStop(orderId, _orderDate, ordershift, partId, partName, _idleStop, idleNote);
                        }
                        else
                        {
                            orderDate = (row.Cells[1].Value.ToString() != "N/A") ? String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells[1].Value.ToString())) : "";
                            ordershift = row.Cells[2].Value.ToString();
                            _lastUpdate = row.Cells[14].Value.ToString();

                            if (_lastUpdate != "" && _lastUpdate != null && ordershift == cls.fnGetDate("S"))
                            {
                                lastUpdate = (_lastUpdate != "" && _lastUpdate != null) ? Convert.ToDateTime(row.Cells[14].Value.ToString()) : DateTime.Now;
                                nowUpdate = DateTime.Now;
                                TimeSpan idleTime = nowUpdate - lastUpdate;
                                double totalMinutes = idleTime.Minutes;
                                if (totalMinutes >= 5)
                                {
                                    if (
                                        (cls.isTimeBetween(dt, _idleLunchStart, _idleLunchStop) == true) ||
                                        (cls.isTimeBetween(dt, _idleDinnerStart, _idleDinnerStop) == true) ||
                                        (cls.isTimeBetween(dt, _idleNightmealStart, _idleNightmealStop) == true) ||
                                        (cls.isTimeBetween(dt, _idleBreakfastStart, _idleBreakfastStop) == true))
                                    {
                                        row.DefaultCellStyle.BackColor = Color.White;
                                    }
                                    else
                                    {
                                        //row.DefaultCellStyle.BackColor = Color.IndianRed;
                                        row.DefaultCellStyle.BackColor = Color.Firebrick;
                                        //row.DefaultCellStyle.ForeColor = Color.White;

                                        //fnIdleStop(orderId, _orderDate, ordershift, partId, partName, _idleStop, "Temporary stop");
                                    }
                                }
                                else
                                {
                                    //row.Cells[7].Style.BackColor = Color.Gold;
                                    //row.Cells[8].Style.BackColor = Color.LightSkyBlue;
                                    //row.Cells[9].Style.BackColor = Color.LightYellow;
                                    row.Cells[9].Style.ForeColor = Color.Firebrick;
                                    row.Cells[10].Style.BackColor = Color.DeepSkyBlue;
                                    row.Cells[11].Style.BackColor = Color.YellowGreen;
                                }
                            }
                            else
                            {
                                row.Cells[9].Style.ForeColor = Color.Firebrick;
                                row.Cells[10].Style.BackColor = Color.DeepSkyBlue;
                                row.Cells[11].Style.BackColor = Color.YellowGreen;
                            }
                        }
                    }


                }
                //orderDate = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells[1].Value.ToString()));
                //ordershift = row.Cells[2].Value.ToString();
                //_rate = row.Cells[11].Value.ToString();
                //rate = (_rate != "" && _rate != null) ? Convert.ToInt32(_rate) : 0;
                //_lastUpdate = row.Cells[14].Value.ToString();

                //if (_lastUpdate != "" && _lastUpdate != null && ordershift == cls.fnGetDate("S"))
                //{
                //    lastUpdate = (_lastUpdate != "" && _lastUpdate != null) ? Convert.ToDateTime(row.Cells[14].Value.ToString()) : DateTime.Now;
                //    nowUpdate = DateTime.Now;
                //    TimeSpan idleTime = nowUpdate - lastUpdate;
                //    double totalMinutes = idleTime.Minutes;
                //    if (totalMinutes >= 5)
                //    {
                //        row.DefaultCellStyle.BackColor = Color.Goldenrod;
                //    }

                //}

                //stop = row.Cells[13].Value.ToString().ToLower();
                //if (stop == "true")
                //{
                //    row.DefaultCellStyle.BackColor = Color.Tomato;
                //}

                //if (rate >= 100)
                //{
                //    row.DefaultCellStyle.BackColor = Color.LawnGreen;
                //    //row.DefaultCellStyle.ForeColor = Color.White;
                //}
            }
            //dgvAssyMonitor.ClearSelection();

            //string idx = "", prodId = "", stop = "";
            //byte _stop = 0;
            //string _lastUpdate = "", orderDate = "", ordershift = "", _rate = "";
            //int rate = 0, __rate = 0;
            //DateTime lastUpdate, nowUpdate;
            //foreach (DataGridViewRow row in dgvAssyMonitor.Rows)
            //{
            //    _rate = row.Cells[11].Value.ToString().Replace("%", "");
            //    rate = (_rate != "" && _rate != null) ? Convert.ToInt32(_rate) : 0;
            //    if (rate >= 100)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.LawnGreen;
            //        //row.DefaultCellStyle.ForeColor = Color.White;
            //    }
            //    else
            //    {
            //        stop = row.Cells[13].Value.ToString().ToLower();
            //        if (stop == "true")
            //        {
            //            row.DefaultCellStyle.BackColor = Color.Tomato;
            //        }
            //        else
            //        {
            //            orderDate = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row.Cells[1].Value.ToString()));
            //            ordershift = row.Cells[2].Value.ToString();
            //            _lastUpdate = row.Cells[14].Value.ToString();

            //            if (_lastUpdate != "" && _lastUpdate != null && ordershift == cls.fnGetDate("S"))
            //            {
            //                lastUpdate = (_lastUpdate != "" && _lastUpdate != null) ? Convert.ToDateTime(row.Cells[14].Value.ToString()) : DateTime.Now;
            //                nowUpdate = DateTime.Now;
            //                TimeSpan idleTime = nowUpdate - lastUpdate;
            //                double totalMinutes = idleTime.Minutes;
            //                if (totalMinutes >= 5)
            //                {
            //                    //row.DefaultCellStyle.BackColor = Color.IndianRed;
            //                    row.DefaultCellStyle.BackColor = Color.Firebrick;
            //                    //row.DefaultCellStyle.ForeColor = Color.White;
            //                }
            //                else
            //                {
            //                    //row.Cells[7].Style.BackColor = Color.Gold;
            //                    //row.Cells[8].Style.BackColor = Color.LightSkyBlue;
            //                    //row.Cells[9].Style.BackColor = Color.LightYellow;
            //                    row.Cells[9].Style.ForeColor = Color.Firebrick;
            //                    row.Cells[10].Style.BackColor = Color.DeepSkyBlue;
            //                    row.Cells[11].Style.BackColor = Color.YellowGreen;
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void dgvAssyMonitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAssyMonitor.ClearSelection();
            fnBindAssy();
        }
    }
}
