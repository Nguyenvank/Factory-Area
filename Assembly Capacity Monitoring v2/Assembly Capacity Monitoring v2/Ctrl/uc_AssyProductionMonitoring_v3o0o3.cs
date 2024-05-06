using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Inventory_Data.Ctrl
{
    public partial class uc_AssyProductionMonitoring_v3o0o3 : UserControl
    {
        public Thread threadUpd;

        int _dgv_Main_Width, _dgv_Main_Heght;

        DateTime _dt;
        Image _imgR, _imgS;

        DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

        int i = 0;

        public uc_AssyProductionMonitoring_v3o0o3()
        {
            InitializeComponent();

            imageCol.Name = "Status";
            imageCol.HeaderText = "Status";
            dgv_Main.CellFormatting += dgv_Main_CellFormatting;

            cls.SetDoubleBuffer(dgv_Main, true);
        }

        private void uc_AssyProductionMonitoring_v3o0o3_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            //init();
            init_Main();
            frmKoreanOperatorEfficiencyMonitoring_Resize(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetDate();

            init_Main();

            frmKoreanOperatorEfficiencyMonitoring_Resize(sender, e);

            if (!check.IsConnectedToInternet())
            {
                dgv_Main.DataSource = null;
                dgv_Main.Columns.Clear();
                dgv_Main.Refresh();
            }
            else
            {
                //i++;
                //if (i < 2)
                //{
                //    //init_Main();
                //    if (dgv_Main.ColumnCount >= 36)
                //    {
                //        dgv_Main.Columns.RemoveAt(0);
                //        //dgv_Main.Columns.Remove(imageCol);
                //    }
                //    init_Main();
                //    //dgv_Main.Columns.Insert(32, imageCol);
                //    //imageCol.Width = 3 * _dgv_Main_Width / 100;                 //Machine status image
                //}

                
            }
            //i++;
            //if (check.IsConnectedToInternet() && (i < 2))
            //{
            //    dgv_Main.Columns.Clear();
            //    init_Main();
            //}
        }

        public void init()
        {
            fnGetDate();

            //init_Main();
            threadUpd = new Thread(new ThreadStart(this.fnUpdateMain));
            threadUpd.IsBackground = true;
            threadUpd.Start();
        }

        public void fnGetDate()
        {
            lbl_Title.Text = "ASSEMBLY ONLINE OPERATOR EFFICIENCY MONITORING SYSTEM       " + cls.fnGetDate("dt");
        }

        public void fnUpdateMain()
        {
            while (true)
            {
                this.Invoke((MethodInvoker)delegate { init_Main(); });
                Thread.Sleep(1000);
            }

            //Thread loadMain = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        init_Main();
            //        Thread.Sleep(1000);
            //    }
            //});

            //loadMain.IsBackground = true;
            //loadMain.Start();
        }

        public void init_Main()
        {
            try
            {
                string sql = "V2o1_BASE_Capacity_Work_Order_Monitor_2o3o6_o2_Addnew";

                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                //foreach (DataRow dr in dt.Rows)
                //{
                //    string stop = dr[22].ToString().ToLower();
                //    string note = dr[21].ToString();
                //    string rate = dr[26].ToString().Replace("%", "");
                //    if (stop == "true")
                //    {
                //        dr[27] = note;
                //    }
                //}

                dgv_Main.DataSource = dt;
                //_dgv_Main_Width = cls.fnGetDataGridWidth(dgv_Main);
                _dgv_Main_Width = cls.fnGetDataGridWidth(dgv_Main);

                dgv_Main.Columns[0].Width = 3 * _dgv_Main_Width / 100;      //STT
                //dgv_Main.Columns[1].Width = 3 * _dgv_Main_Width / 100;    //idx
                dgv_Main.Columns[2].Width = 12 * _dgv_Main_Width / 100;      //lineName
                //dgv_Main.Columns[3].Width = 3 * _dgv_Main_Width / 100;    //isActive
                //dgv_Main.Columns[4].Width = 3 * _dgv_Main_Width / 100;    //seq
                //dgv_Main.Columns[5].Width = 3 * _dgv_Main_Width / 100;    //IDx
                //dgv_Main.Columns[6].Width = 3 * _dgv_Main_Width / 100;    //AssyDate
                //dgv_Main.Columns[7].Width = 3 * _dgv_Main_Width / 100;    //AssyShift
                //dgv_Main.Columns[8].Width = 3 * _dgv_Main_Width / 100;    //LineID
                //dgv_Main.Columns[9].Width = 10 * _dgv_Main_Width / 100;      //LineName
                //dgv_Main.Columns[10].Width = 3 * _dgv_Main_Width / 100;   //PartID
                dgv_Main.Columns[11].Width = 12 * _dgv_Main_Width / 100;     //PartName
                dgv_Main.Columns[12].Width = 9 * _dgv_Main_Width / 100;     //PartCode
                //dgv_Main.Columns[13].Width = 3 * _dgv_Main_Width / 100;   //[From]
                //dgv_Main.Columns[14].Width = 3 * _dgv_Main_Width / 100;   //[To]
                //dgv_Main.Columns[15].Width = 3 * _dgv_Main_Width / 100;   //[Clock]
                //dgv_Main.Columns[16].Width = 3 * _dgv_Main_Width / 100;   //[Span]
                //dgv_Main.Columns[17].Width = 3 * _dgv_Main_Width / 100;   //[UPH]
                //dgv_Main.Columns[18].Width = 3 * _dgv_Main_Width / 100;   //[Seq]
                //dgv_Main.Columns[19].Width = 3 * _dgv_Main_Width / 100;   //[Order]
                //dgv_Main.Columns[20].Width = 3 * _dgv_Main_Width / 100;   //[Total]
                //dgv_Main.Columns[21].Width = 3 * _dgv_Main_Width / 100;   //[Note]
                //dgv_Main.Columns[22].Width = 3 * _dgv_Main_Width / 100;   //[Stop]
                dgv_Main.Columns[23].Width = 7 * _dgv_Main_Width / 100;     //[Plan]
                dgv_Main.Columns[24].Width = 7 * _dgv_Main_Width / 100;     //[Goal]
                dgv_Main.Columns[25].Width = 7 * _dgv_Main_Width / 100;     //[Result]
                dgv_Main.Columns[26].Width = 7 * _dgv_Main_Width / 100;     //[Rate]
                dgv_Main.Columns[27].Width = 14 * _dgv_Main_Width / 100;     //[Reason]
                //dgv_Main.Columns[28].Width = 3 * _dgv_Main_Width / 100;   //[PicID]
                dgv_Main.Columns[29].Width = 10 * _dgv_Main_Width / 100;     //[PIC]

                //dgv_Main.Columns[30].Width = 3 * _dgv_Main_Width / 100;   //[Active]
                //dgv_Main.Columns[31].Width = 3 * _dgv_Main_Width / 100;   //[Last Update]

                if (dgv_Main.ColumnCount > 36) { dgv_Main.Columns.Remove(imageCol); }
                _dgv_Main_Width = cls.fnGetDataGridWidth(dgv_Main);
                dgv_Main.Columns.Insert(32, imageCol);
                imageCol.Width = 3 * _dgv_Main_Width / 100;                 //Machine status image

                dgv_Main.Columns[33].Width = 3 * _dgv_Main_Width / 100;     //[F]
                dgv_Main.Columns[34].Width = 3 * _dgv_Main_Width / 100;     //[M]
                dgv_Main.Columns[35].Width = 3 * _dgv_Main_Width / 100;     //[L]
                //dgv_Main.Columns[36].Width = 3 * _dgv_Main_Width / 100;   //[TimeOut]

                //dgv_Main.Columns[0].Width = 3 * _dgv_Main_Width / 100;      //STT
                //dgv_Main.Columns[1].Width = 10 * _dgv_Main_Width / 100;     //Machine No.
                //dgv_Main.Columns[2].Width = 10 * _dgv_Main_Width / 100;     //Part No.
                //dgv_Main.Columns[3].Width = 10 * _dgv_Main_Width / 100;     //Material
                //dgv_Main.Columns[4].Width = 6 * _dgv_Main_Width / 100;      //Shift plan
                //dgv_Main.Columns[5].Width = 6 * _dgv_Main_Width / 100;      //Target current
                //dgv_Main.Columns[6].Width = 6 * _dgv_Main_Width / 100;      //Achievement current
                //dgv_Main.Columns[7].Width = 6 * _dgv_Main_Width / 100;      //Achievement current rate
                //dgv_Main.Columns[8].Width = 16 * _dgv_Main_Width / 100;     //Remark
                //dgv_Main.Columns[9].Width = 15 * _dgv_Main_Width / 100;     //Worker
                //dgv_Main.Columns[10].Width = 3 * _dgv_Main_Width / 100;     //Machine status

                //dgv_Main.Columns.Insert(11, imageCol);
                //imageCol.Width = 3 * _dgv_Main_Width / 100;                 //Machine status image

                //dgv_Main.Columns[12].Width = 3 * _dgv_Main_Width / 100;     //@global_QA_F
                //dgv_Main.Columns[13].Width = 3 * _dgv_Main_Width / 100;     //@global_QA_M
                //dgv_Main.Columns[14].Width = 3 * _dgv_Main_Width / 100;     //@global_QA_L

                //dgv_Main.Columns[0].Visible = true;
                //dgv_Main.Columns[1].Visible = false;
                //dgv_Main.Columns[2].Visible = true;
                //dgv_Main.Columns[3].Visible = false;
                //dgv_Main.Columns[4].Visible = false;
                //dgv_Main.Columns[5].Visible = false;
                //dgv_Main.Columns[6].Visible = false;
                //dgv_Main.Columns[7].Visible = false;
                //dgv_Main.Columns[8].Visible = false;
                //dgv_Main.Columns[9].Visible = false;
                //dgv_Main.Columns[10].Visible = false;
                //dgv_Main.Columns[11].Visible = true;
                //dgv_Main.Columns[12].Visible = true;
                //dgv_Main.Columns[13].Visible = false;
                //dgv_Main.Columns[14].Visible = false;
                //dgv_Main.Columns[15].Visible = false;
                //dgv_Main.Columns[16].Visible = false;
                //dgv_Main.Columns[17].Visible = false;
                //dgv_Main.Columns[18].Visible = false;
                //dgv_Main.Columns[19].Visible = false;
                //dgv_Main.Columns[20].Visible = false;
                //dgv_Main.Columns[21].Visible = false;
                //dgv_Main.Columns[22].Visible = false;
                //dgv_Main.Columns[23].Visible = true;
                //dgv_Main.Columns[24].Visible = true;
                //dgv_Main.Columns[25].Visible = true;
                //dgv_Main.Columns[26].Visible = true;
                //dgv_Main.Columns[27].Visible = true;
                //dgv_Main.Columns[28].Visible = false;
                //dgv_Main.Columns[29].Visible = true;
                //dgv_Main.Columns[30].Visible = false;
                //dgv_Main.Columns[31].Visible = false;
                //dgv_Main.Columns[32].Visible = true;
                //dgv_Main.Columns[33].Visible = true;
                //dgv_Main.Columns[34].Visible = true;
                //dgv_Main.Columns[35].Visible = true;
                //dgv_Main.Columns[36].Visible = false;

                cls.fnFormatDatagridviewWhite(dgv_Main, 11, 60);

                dgv_Main.DefaultCellStyle.Font = new Font("Tahoma", 12, FontStyle.Bold);

                fnCheckStatus();
                fnc_dgvMain_Columns_Order();

                //string achieved_shift = "", achieved_current = "", achieved_rate = "", bad = "", state = "", stateStop = "", stateTime = "";
                //int _bad = 0;
                //double _achieved_shift, _achieved_current, _achieved_rate;
                //bool _stateStop = false, _stateTime = false;

                //foreach (DataGridViewRow row in dgv_Main.Rows)
                //{
                //    string sChk = row.Cells[6].Value.ToString();
                //    if (sChk != "" && sChk != null)
                //    {
                //        dgv_Main.Columns[24].DefaultCellStyle.ForeColor = Color.Blue;        // Target current
                //        dgv_Main.Columns[25].DefaultCellStyle.ForeColor = Color.Green;   // Achievement current
                //        //dgv_Main.Columns[7].DefaultCellStyle.ForeColor = Color.DarkGreen;   // Achievement current rate

                //        //achieved_shift = row.Cells[5].Value.ToString();
                //        //_achieved_shift = (achieved_shift != "" && achieved_shift != null) ? Convert.ToDouble(achieved_shift) : 0;

                //        //achieved_current = row.Cells[6].Value.ToString();
                //        //_achieved_current = (achieved_current != "" && achieved_current != null) ? Convert.ToDouble(achieved_current) : 0;

                //        achieved_rate = row.Cells[26].Value.ToString().Replace("%", "");
                //        _achieved_rate = (achieved_rate != "" && achieved_rate != null) ? Convert.ToDouble(achieved_rate) : 0;

                //        //row.Cells[7].Style.BackColor = (_achieved_current >= 100) ? Color.Green : Color.White;

                //        //bad = row.Cells[11].Value.ToString();
                //        //_bad = (bad != "" && bad != null) ? Convert.ToInt32(bad) : 0;
                //        //row.Cells[11].Style.BackColor = (_bad > 0) ? Color.Gold : Color.White;

                //        state = row.Cells[22].Value.ToString().ToLower();
                //        _stateStop = (state == "true") ? true : false;
                //        //row.Cells[12].Style.BackColor = (state == "R") ? Color.Green : Color.Gold;
                //        //row.DefaultCellStyle.BackColor = (state == "R") ? Color.White : Color.Gold;

                //        if (state == "false")
                //        {
                //            if (_achieved_rate >= 0 && _achieved_rate < 95)
                //            {
                //                row.Cells[26].Style.ForeColor = Color.Firebrick;
                //            }
                //            else if (_achieved_rate >= 95 && _achieved_rate < 105)
                //            {
                //                row.Cells[26].Style.ForeColor = Color.DarkGreen;
                //            }
                //            else
                //            {
                //                row.Cells[26].Style.ForeColor = Color.DarkBlue;
                //            }

                //            row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_2;
                //        }
                //        else
                //        {
                //            for (int i = 0; i < dgv_Main.Columns.Count; i++)
                //            {
                //                row.Cells[i].Style.BackColor = Color.Firebrick;
                //                row.Cells[i].Style.ForeColor = Color.White;
                //            }

                //            row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_1;
                //        }
                //    }
                //    else
                //    {
                //        row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_0;
                //    }
                //}
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnc_dgvMain_Columns_Order()
        {
            dgv_Main.Columns[0].Visible = true;
            dgv_Main.Columns[1].Visible = false;
            dgv_Main.Columns[2].Visible = true;
            dgv_Main.Columns[3].Visible = false;
            dgv_Main.Columns[4].Visible = false;
            dgv_Main.Columns[5].Visible = false;
            dgv_Main.Columns[6].Visible = false;
            dgv_Main.Columns[7].Visible = false;
            dgv_Main.Columns[8].Visible = false;
            dgv_Main.Columns[9].Visible = false;
            dgv_Main.Columns[10].Visible = false;
            dgv_Main.Columns[11].Visible = true;
            dgv_Main.Columns[12].Visible = true;
            dgv_Main.Columns[13].Visible = false;
            dgv_Main.Columns[14].Visible = false;
            dgv_Main.Columns[15].Visible = false;
            dgv_Main.Columns[16].Visible = false;
            dgv_Main.Columns[17].Visible = false;
            dgv_Main.Columns[18].Visible = false;
            dgv_Main.Columns[19].Visible = false;
            dgv_Main.Columns[20].Visible = false;
            dgv_Main.Columns[21].Visible = false;
            dgv_Main.Columns[22].Visible = false;
            dgv_Main.Columns[23].Visible = true;
            dgv_Main.Columns[24].Visible = true;
            dgv_Main.Columns[25].Visible = true;
            dgv_Main.Columns[26].Visible = true;
            dgv_Main.Columns[27].Visible = true;
            dgv_Main.Columns[28].Visible = false;
            dgv_Main.Columns[29].Visible = true;
            dgv_Main.Columns[30].Visible = false;
            dgv_Main.Columns[31].Visible = false;
            dgv_Main.Columns[32].Visible = true;
            dgv_Main.Columns[33].Visible = true;
            dgv_Main.Columns[34].Visible = true;
            dgv_Main.Columns[35].Visible = true;
            dgv_Main.Columns[36].Visible = false;

            dgv_Main.Columns[0].DisplayIndex = 0;
            dgv_Main.Columns[2].DisplayIndex = 1;
            dgv_Main.Columns[11].DisplayIndex = 2;
            dgv_Main.Columns[12].DisplayIndex = 3;
            dgv_Main.Columns[23].DisplayIndex = 4;
            dgv_Main.Columns[24].DisplayIndex = 5;
            dgv_Main.Columns[25].DisplayIndex = 6;
            dgv_Main.Columns[26].DisplayIndex = 7;
            dgv_Main.Columns[27].DisplayIndex = 8;
            dgv_Main.Columns[29].DisplayIndex = 9;
            dgv_Main.Columns[32].DisplayIndex = 10;
            dgv_Main.Columns[33].DisplayIndex = 11;
            dgv_Main.Columns[34].DisplayIndex = 12;
            dgv_Main.Columns[35].DisplayIndex = 13;

        }

        private void dgv_Main_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            //if (dgv_Main.Columns[e.ColumnIndex].HeaderText.ToLower() == "status")
            //{
            //    //DataRowView drv = dgv_Main.Rows[e.RowIndex].DataBoundItem as DataRowView;
            //    //string name = drv.Row["Name"].ToString();
            //    e.Value = (System.Drawing.Image)_imgR;
            //}
        }

        private void dgv_Main_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dgv_Main.ColumnCount > 36) { dgv_Main.Columns.Remove(imageCol); }
            _dgv_Main_Width = cls.fnGetDataGridWidth(dgv_Main);
            dgv_Main.Columns.Insert(32, imageCol);
            imageCol.Width = 3 * _dgv_Main_Width / 100;                 //Machine status image


            int height = 0;
            foreach (DataGridViewRow row in dgv_Main.Rows)
            {
                height += row.Height;
            }
            height += dgv_Main.ColumnHeadersHeight;

            int width = 0;
            foreach (DataGridViewColumn col in dgv_Main.Columns)
            {
                width += col.Width;
            }
            width += dgv_Main.RowHeadersWidth;

            dgv_Main.ClientSize = new Size(width + 2, height + 2);
        }

        private void frmKoreanOperatorEfficiencyMonitoring_Resize(object sender, EventArgs e)
        {
            int dgvHH = 60;
            int dgvSW = 0;//SystemInformation.HorizontalScrollBarHeight - 5;
            int dgvH = dgv_Main.Height - (dgvHH + dgvSW);
            for (int iRow = 0; iRow < dgv_Main.RowCount; iRow++)
            {
                if (dgvH > 100)
                {
                    dgv_Main.RowTemplate.Height = (dgvH / dgv_Main.RowCount);
                }
            }
            dgv_Main.ScrollBars = ScrollBars.None;
        }

        private void dgv_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                init_Main();
                dgv_Main.ClearSelection();
            }
        }

        public void fnCheckStatus()
        {
            try
            {
                string stop = "", stopNote = "", picId = "", picName = "";
                string lastUpdate = "", orderDate = "", _orderShift = "", rates = "";
                string orderId = "", orderShift = "", lineId = "", lineName = "";
                string partId = "", partName = "", partCode = "", active = "", timeTo = "", timeOut = "";

                string msg = "";

                decimal _rates = 0;
                int _lineId = 0;
                bool _timeOut = false;

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

                foreach (DataGridViewRow row in dgv_Main.Rows)
                {
                    row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_a;
                    string sChk = row.Cells[6].Value.ToString();
                    if (sChk != "" && sChk != null)
                    {
                        orderId = row.Cells[5].Value.ToString();
                        orderDate = row.Cells[6].Value.ToString();
                        orderShift = row.Cells[7].Value.ToString();
                        lineId = row.Cells[8].Value.ToString();
                        lineName = row.Cells[9].Value.ToString();
                        partId = row.Cells[10].Value.ToString();
                        partName = row.Cells[11].Value.ToString();
                        partCode = row.Cells[12].Value.ToString();
                        timeTo = row.Cells[14].Value.ToString();
                        stopNote = row.Cells[21].Value.ToString();
                        stop = row.Cells[22].Value.ToString();
                        rates = row.Cells[26].Value.ToString().Replace("%", "");
                        picId = row.Cells[28].Value.ToString();
                        picName = row.Cells[29].Value.ToString();
                        active = row.Cells[30].Value.ToString();
                        lastUpdate = row.Cells[31].Value.ToString();
                        timeOut = row.Cells[36].Value.ToString();

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
                        _timeOut = (timeOut != "1") ? true : false;



                        row.Cells[24].Style.ForeColor = Color.Blue;        // Target current
                        row.Cells[25].Style.ForeColor = Color.Green;   // Achievement current

                        //if (_rates < 95)
                        //    //row.Cells[28].Style.ForeColor = Color.Firebrick;
                        //    row.Cells[26].Style.ForeColor = Color.Firebrick;
                        //else if (_rates >= 95 && _rates < 105)
                        //    //row.Cells[28].Style.ForeColor = Color.Green;
                        //    row.Cells[26].Style.ForeColor = Color.DarkGreen;
                        //else if (_rates >= 105)
                        //    //row.Cells[28].Style.ForeColor = Color.DarkBlue;
                        //    row.Cells[26].Style.ForeColor = Color.DarkBlue;

                        //row.Cells[25].Style.ForeColor = Color.Firebrick;
                        //row.Cells[26].Style.ForeColor = Color.DarkBlue;
                        //row.Cells[27].Style.ForeColor = Color.Green;
                        //row.Cells[29].Style.ForeColor = Color.Black;

                        if (_rates < 95)
                        {
                            row.Cells[26].Style.ForeColor = Color.Firebrick;
                        }
                        else if (_rates >= 95 && _rates < 105)
                        {
                            row.Cells[26].Style.ForeColor = Color.DarkGreen;
                        }
                        else if (_rates >= 105)
                        {
                            row.Cells[26].Style.ForeColor = Color.DarkBlue;
                        }

                        //row.Cells[31].Value = (active == "1") ? Properties.Resources.switch2_on : Properties.Resources.switch2_off;

                        if (stop.ToLower() == "true")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightPink;
                            //row.DefaultCellStyle.ForeColor = Color.White;

                            //row.Cells[24].Style.ForeColor = Color.White;
                            //row.Cells[25].Style.ForeColor = Color.White;
                            //row.Cells[26].Style.ForeColor = Color.White;

                            row.Cells[27].Value = stopNote;
                            row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_1;
                        }
                        else
                        {
                            //_orderDate = (orderDate != null && orderDate != "") ? Convert.ToDateTime(orderDate) : _dt;
                            _orderShift = orderShift.ToUpper();
                            _lastUpdate = (lastUpdate != null && lastUpdate != "") ? Convert.ToDateTime(lastUpdate) : _dt;
                            if (_lastUpdate < _dt && _orderShift == cls.fnGetDate("S").ToUpper())
                            {
                                if (_timeOut == false && active == "1")
                                {
                                    if ((cls.isTimeBetween(_dt, _idleLunchStart, _idleLunchStop) == true)
                                        || (cls.isTimeBetween(_dt, _idleDinnerStart, _idleDinnerStop) == true)
                                        || (cls.isTimeBetween(_dt, _idleNightmealStart, _idleNightmealStop) == true)
                                        || (cls.isTimeBetween(_dt, _idleBreakfastStart, _idleBreakfastStop) == true))
                                    {

                                        row.Cells[24].Style.ForeColor = Color.Blue;        // Target current
                                        row.Cells[25].Style.ForeColor = Color.Green;   // Achievement current

                                        //row.DefaultCellStyle.BackColor = Color.White;
                                        //fnIdleTimeStop(orderId);
                                        row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_2;
                                    }
                                    else
                                    {
                                        row.DefaultCellStyle.BackColor = Color.LightPink;
                                        //row.DefaultCellStyle.ForeColor = Color.White;

                                        //row.Cells[24].Style.ForeColor = Color.White;
                                        //row.Cells[25].Style.ForeColor = Color.White;
                                        //row.Cells[26].Style.ForeColor = Color.White;

                                        row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_1;
                                        //row.Cells[18].Style.BackColor = Color.LightPink;

                                        //MessageBox.Show(orderId + "\r\n" + orderDate + "\r\n" + orderShift + "\r\n" + partId + "\r\n" + partName + "\r\n" + partCode + "\r\n" + lineId + "\r\n" + lineName);
                                        //fnIdleTimeStart(orderId, orderDate, orderShift, partId, partName, partCode, lineId, lineName);
                                    }
                                }
                                else
                                {
                                    //fnIdleTimeStop(orderId);
                                    //row.DefaultCellStyle.BackColor = Color.White;
                                    //row.DefaultCellStyle.ForeColor = Color.Black;

                                    //row.Cells[24].Style.ForeColor = Color.Blue;        // Target current
                                    //row.Cells[25].Style.ForeColor = Color.Green;   // Achievement current


                                    //row.Cells[32].Value = (active == "1") ? (System.Drawing.Image)Properties.Resources.switch2_2 : (System.Drawing.Image)Properties.Resources.switch2_w;
                                    //row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_2;
                                    if (_dt.AddMilliseconds(-_dt.Millisecond) >= _timeTo.AddMilliseconds(-_timeTo.Millisecond))
                                    {
                                        row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_2;
                                    }
                                    else
                                    {
                                        row.Cells[32].Value = (active == "1") ? (System.Drawing.Image)Properties.Resources.switch2_2 : (System.Drawing.Image)Properties.Resources.switch2_w;

                                    }

                                }
                                //    TimeSpan idleTime = _dt - _lastUpdate;
                                //    double totalMinutes = idleTime.Minutes;
                                //    //if (totalMinutes >= 5 && active == "1" && lineId != "15")
                                //    //if (totalMinutes >= 5 && active == "1" && lineId != "0")
                                //    if (totalMinutes >= 5 && active == "1")
                                //    {
                                //        if ((cls.isTimeBetween(_dt, _idleLunchStart, _idleLunchStop) == true)
                                //            || (cls.isTimeBetween(_dt, _idleDinnerStart, _idleDinnerStop) == true)
                                //            || (cls.isTimeBetween(_dt, _idleNightmealStart, _idleNightmealStop) == true)
                                //            || (cls.isTimeBetween(_dt, _idleBreakfastStart, _idleBreakfastStop) == true))
                                //        {

                                //            row.Cells[24].Style.ForeColor = Color.Blue;        // Target current
                                //            row.Cells[25].Style.ForeColor = Color.Green;   // Achievement current

                                //            //row.DefaultCellStyle.BackColor = Color.White;
                                //            //fnIdleTimeStop(orderId);
                                //            row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_2;
                                //        }
                                //        else
                                //        {
                                //            row.DefaultCellStyle.BackColor = Color.LightPink;
                                //            //row.DefaultCellStyle.ForeColor = Color.White;

                                //            //row.Cells[24].Style.ForeColor = Color.White;
                                //            //row.Cells[25].Style.ForeColor = Color.White;
                                //            //row.Cells[26].Style.ForeColor = Color.White;

                                //            row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_1;
                                //            //row.Cells[18].Style.BackColor = Color.LightPink;

                                //            //MessageBox.Show(orderId + "\r\n" + orderDate + "\r\n" + orderShift + "\r\n" + partId + "\r\n" + partName + "\r\n" + partCode + "\r\n" + lineId + "\r\n" + lineName);
                                //            //fnIdleTimeStart(orderId, orderDate, orderShift, partId, partName, partCode, lineId, lineName);
                                //        }
                                //    }
                                //    else
                                //    {
                                //        //fnIdleTimeStop(orderId);
                                //        row.Cells[32].Value = (active == "1") ? (System.Drawing.Image)Properties.Resources.switch2_2 : (System.Drawing.Image)Properties.Resources.switch2_b;

                                //    }
                            }
                        }
                    }
                    else
                    {
                        //row.Cells[32].Value = (System.Drawing.Image)Properties.Resources.switch2_0;
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


    }
}
