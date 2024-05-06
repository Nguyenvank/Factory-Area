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
    public partial class frmCapacityWorkOrders_v2 : Form
    {
        public string _idxOrder;
        public string _appName = cls.appName();
        public int _dgvListOrderWidth;

        public static DateTime _dt = DateTime.Now;
        public DateTime _dtLunchStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 11, 50, 0);
        public DateTime _dtLunchStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 13, 0, 0);
        public DateTime _dtDinnerStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 0, 0);
        public DateTime _dtDinnerStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 40, 0);
        public DateTime _dtNightmealStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 23, 50, 0);
        public DateTime _dtNightmealStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 1, 0, 0).AddDays(1);
        public DateTime _dtBreakfastStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 0, 0).AddDays(1);
        public DateTime _dtBreakfastStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 40, 0).AddDays(1);

        public DateTime _dtDayStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 8, 0, 0);
        public DateTime _dtDayStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 19, 59, 59);
        public DateTime _dtNightStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 20, 0, 0);
        public DateTime _dtPrevDayStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 23, 59, 59);
        public DateTime _dtNextDayStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, 0, 0, 0).AddDays(1);
        public DateTime _dtNightStop = new DateTime(_dt.Year, _dt.Month, _dt.Day, 7, 59, 59).AddDays(1);


        public frmCapacityWorkOrders_v2()
        {
            InitializeComponent();
        }

        private void frmCapacityWorkOrders_v2_Load(object sender, EventArgs e)
        {
            _dgvListOrderWidth = cls.fnGetDataGridWidth(dgvListOrder);

            init();
            _idxOrder = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvListOrderWidth = cls.fnGetDataGridWidth(dgvListOrder);

            fnGetDate();
            //fnBindData();
        }

        public void init()
        {
            fnGetDate();
            fnInitData();
            fnInitSearch();
            fnBindData();
            fnShowTooltip();
        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
        }

        public void fnBindData()
        {
            string sql = "";
            //sql = "V2o1_BASE_Capacity_Work_Order_SelItem_Addnew";
            sql = "V2o1_BASE_Capacity_Work_Order_SelItem_V1o2_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            foreach (DataRow dr in dt.Rows)
            {
                string clock = dr[12].ToString();
                if(clock.ToLower()=="false")
                {
                    dr[10] = new TimeSpan();
                    dr[11] = new TimeSpan();
                }
            }

            dgvListOrder.DataSource = dt;
            dgvListOrder.Refresh();

            //dgvListOrder.Columns[0].Width = 10 * _dgvListOrderWidth / 100;    // idx
            dgvListOrder.Columns[1].Width = 7 * _dgvListOrderWidth / 100;    // date
            dgvListOrder.Columns[2].Width = 5 * _dgvListOrderWidth / 100;    // shift
            //dgvListOrder.Columns[3].Width = 10 * _dgvListOrderWidth / 100;    // lineId
            dgvListOrder.Columns[4].Width = 8 * _dgvListOrderWidth / 100;    // lineName
            //dgvListOrder.Columns[5].Width = 10 * _dgvListOrderWidth / 100;    // prodId
            dgvListOrder.Columns[6].Width = 12 * _dgvListOrderWidth / 100;    // prodName
            dgvListOrder.Columns[7].Width = 8 * _dgvListOrderWidth / 100;    // prodCode
            //dgvListOrder.Columns[8].Width = 10 * _dgvListOrderWidth / 100;    // picId
            dgvListOrder.Columns[9].Width = 10 * _dgvListOrderWidth / 100;    // picName
            dgvListOrder.Columns[10].Width = 5 * _dgvListOrderWidth / 100;    // timeFrom
            dgvListOrder.Columns[11].Width = 5 * _dgvListOrderWidth / 100;    // timeTo
            //dgvListOrder.Columns[12].Width = 10 * _dgvListOrderWidth / 100;    // useClock
            dgvListOrder.Columns[13].Width = 5 * _dgvListOrderWidth / 100;    // timeSpan
            dgvListOrder.Columns[14].Width = 5 * _dgvListOrderWidth / 100;    // UPH
            dgvListOrder.Columns[15].Width = 5 * _dgvListOrderWidth / 100;    // Seq
            dgvListOrder.Columns[16].Width = 5 * _dgvListOrderWidth / 100;    // totalOrder
            //dgvListOrder.Columns[17].Width = 10 * _dgvListOrderWidth / 100;    // note
            dgvListOrder.Columns[18].Width = 5 * _dgvListOrderWidth / 100;    // OK
            dgvListOrder.Columns[19].Width = 5 * _dgvListOrderWidth / 100;    // NG
            dgvListOrder.Columns[20].Width = 5 * _dgvListOrderWidth / 100;    // Total
            dgvListOrder.Columns[21].Width = 5 * _dgvListOrderWidth / 100;    // Rate
            //dgvListOrder.Columns[22].Width = 10 * _dgvListOrderWidth / 100;    // Stop
            //dgvListOrder.Columns[23].Width = 10 * _dgvListOrderWidth / 100;    // DateAdd
            //dgvListOrder.Columns[24].Width = 10 * _dgvListOrderWidth / 100;    // Active
            //dgvListOrder.Columns[25].Width = 10 * _dgvListOrderWidth / 100;    // clockFrom
            //dgvListOrder.Columns[26].Width = 10 * _dgvListOrderWidth / 100;    // clockTo
            //dgvListOrder.Columns[27].Width = 10 * _dgvListOrderWidth / 100;    // reason
            //dgvListOrder.Columns[28].Width = 10 * _dgvListOrderWidth / 100;    // action


            dgvListOrder.Columns[0].Visible = false;
            dgvListOrder.Columns[1].Visible = true;
            dgvListOrder.Columns[2].Visible = true;
            dgvListOrder.Columns[3].Visible = false;
            dgvListOrder.Columns[4].Visible = true;
            dgvListOrder.Columns[5].Visible = false;
            dgvListOrder.Columns[6].Visible = true;
            dgvListOrder.Columns[7].Visible = true;
            dgvListOrder.Columns[8].Visible = false;
            dgvListOrder.Columns[9].Visible = true;
            dgvListOrder.Columns[10].Visible = true;
            dgvListOrder.Columns[11].Visible = true;
            dgvListOrder.Columns[12].Visible = false;
            dgvListOrder.Columns[13].Visible = true;
            dgvListOrder.Columns[14].Visible = true;
            dgvListOrder.Columns[15].Visible = true;
            dgvListOrder.Columns[16].Visible = true;
            dgvListOrder.Columns[17].Visible = false;
            dgvListOrder.Columns[18].Visible = true;
            dgvListOrder.Columns[19].Visible = true;
            dgvListOrder.Columns[20].Visible = true;
            dgvListOrder.Columns[21].Visible = true;
            dgvListOrder.Columns[22].Visible = false;
            dgvListOrder.Columns[23].Visible = false;
            dgvListOrder.Columns[24].Visible = false;
            dgvListOrder.Columns[25].Visible = false;
            dgvListOrder.Columns[26].Visible = false;
            dgvListOrder.Columns[27].Visible = false;
            dgvListOrder.Columns[28].Visible = false;

            cls.fnFormatDatagridview(dgvListOrder, 11, 40);

        }

        public void fnBindSearchDate(DateTime assyDate)
        {
            dgvListOrder.DataSource = "";
            dgvListOrder.Refresh();
            string sql = "";
            //sql = "V2o1_BASE_Capacity_Work_Order_Search_Date_Addnew";
            sql = "V2o1_BASE_Capacity_Work_Order_Search_Date_V1o1_Addnew";
            DataTable dtSearch = new DataTable();

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "searchDate";
            sParams[0].Value = assyDate;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "searchAll";
            sParams[1].Value = "Date";

            dtSearch = cls.ExecuteDataTable(sql, sParams);

            foreach (DataRow dr in dtSearch.Rows)
            {
                string clock = dr[12].ToString();
                if (clock.ToLower() == "false")
                {
                    dr[10] = new TimeSpan();
                    dr[11] = new TimeSpan();
                }
            }

            dgvListOrder.DataSource = dtSearch;
            dgvListOrder.Refresh();

            //dgvListOrder.Columns[0].Width = 10 * _dgvListOrderWidth / 100;    // idx
            dgvListOrder.Columns[1].Width = 7 * _dgvListOrderWidth / 100;    // date
            dgvListOrder.Columns[2].Width = 5 * _dgvListOrderWidth / 100;    // shift
            //dgvListOrder.Columns[3].Width = 10 * _dgvListOrderWidth / 100;    // lineId
            dgvListOrder.Columns[4].Width = 8 * _dgvListOrderWidth / 100;    // lineName
            //dgvListOrder.Columns[5].Width = 10 * _dgvListOrderWidth / 100;    // prodId
            dgvListOrder.Columns[6].Width = 12 * _dgvListOrderWidth / 100;    // prodName
            dgvListOrder.Columns[7].Width = 8 * _dgvListOrderWidth / 100;    // prodCode
            //dgvListOrder.Columns[8].Width = 10 * _dgvListOrderWidth / 100;    // picId
            dgvListOrder.Columns[9].Width = 10 * _dgvListOrderWidth / 100;    // picName
            dgvListOrder.Columns[10].Width = 5 * _dgvListOrderWidth / 100;    // timeFrom
            dgvListOrder.Columns[11].Width = 5 * _dgvListOrderWidth / 100;    // timeTo
            //dgvListOrder.Columns[12].Width = 10 * _dgvListOrderWidth / 100;    // useClock
            dgvListOrder.Columns[13].Width = 5 * _dgvListOrderWidth / 100;    // timeSpan
            dgvListOrder.Columns[14].Width = 5 * _dgvListOrderWidth / 100;    // UPH
            dgvListOrder.Columns[15].Width = 5 * _dgvListOrderWidth / 100;    // Seq
            dgvListOrder.Columns[16].Width = 5 * _dgvListOrderWidth / 100;    // totalOrder
            //dgvListOrder.Columns[17].Width = 10 * _dgvListOrderWidth / 100;    // note
            dgvListOrder.Columns[18].Width = 5 * _dgvListOrderWidth / 100;    // OK
            dgvListOrder.Columns[19].Width = 5 * _dgvListOrderWidth / 100;    // NG
            dgvListOrder.Columns[20].Width = 5 * _dgvListOrderWidth / 100;    // Total
            dgvListOrder.Columns[21].Width = 5 * _dgvListOrderWidth / 100;    // Rate
            //dgvListOrder.Columns[22].Width = 10 * _dgvListOrderWidth / 100;    // Stop
            //dgvListOrder.Columns[23].Width = 10 * _dgvListOrderWidth / 100;    // DateAdd
            //dgvListOrder.Columns[24].Width = 10 * _dgvListOrderWidth / 100;    // Active
            //dgvListOrder.Columns[25].Width = 10 * _dgvListOrderWidth / 100;    // clockFrom
            //dgvListOrder.Columns[26].Width = 10 * _dgvListOrderWidth / 100;    // clockTo
            //dgvListOrder.Columns[27].Width = 10 * _dgvListOrderWidth / 100;    // reason


            dgvListOrder.Columns[0].Visible = false;
            dgvListOrder.Columns[1].Visible = true;
            dgvListOrder.Columns[2].Visible = true;
            dgvListOrder.Columns[3].Visible = false;
            dgvListOrder.Columns[4].Visible = true;
            dgvListOrder.Columns[5].Visible = false;
            dgvListOrder.Columns[6].Visible = true;
            dgvListOrder.Columns[7].Visible = true;
            dgvListOrder.Columns[8].Visible = false;
            dgvListOrder.Columns[9].Visible = true;
            dgvListOrder.Columns[10].Visible = true;
            dgvListOrder.Columns[11].Visible = true;
            dgvListOrder.Columns[12].Visible = false;
            dgvListOrder.Columns[13].Visible = true;
            dgvListOrder.Columns[14].Visible = true;
            dgvListOrder.Columns[15].Visible = true;
            dgvListOrder.Columns[16].Visible = true;
            dgvListOrder.Columns[17].Visible = false;
            dgvListOrder.Columns[18].Visible = true;
            dgvListOrder.Columns[19].Visible = true;
            dgvListOrder.Columns[20].Visible = true;
            dgvListOrder.Columns[21].Visible = true;
            dgvListOrder.Columns[22].Visible = false;
            dgvListOrder.Columns[23].Visible = false;
            dgvListOrder.Columns[24].Visible = false;
            dgvListOrder.Columns[25].Visible = false;
            dgvListOrder.Columns[26].Visible = false;
            dgvListOrder.Columns[27].Visible = false;

            cls.fnFormatDatagridview(dgvListOrder, 11, 40);

        }

        public void fnInitData()
        {
            // SHIFT INFO
            cbbShift.Items.Add("DAY");
            cbbShift.Items.Add("NIGHT");
            cbbShift.Items.Insert(0, "");
            cbbShift.SelectedIndex = 0;

            // PRODUCT NAME
            string sqlProdName = "";
            sqlProdName = "V2_BASE_CAPACITY_WORK_ORDER_SELECT_PRODUCTION_ADDNEW";
            DataTable dtProdName = new DataTable();
            dtProdName = cls.fnSelect(sqlProdName);
            cbbProdName.DataSource = dtProdName;
            cbbProdName.DisplayMember = "Name";
            cbbProdName.ValueMember = "prodId";
            dtProdName.Rows.InsertAt(dtProdName.NewRow(), 0);
            cbbProdName.SelectedIndex = 0;

            // LINE NAME
            string sqlLine = "";
            sqlLine = "V2_BASE_CAPACITY_WORK_ORDER_SELECT_LINENAME_ADDNEW";
            DataTable dtLine = new DataTable();

            SqlParameter[] sParamsLine = new SqlParameter[1]; // Parameter count
            sParamsLine[0] = new SqlParameter();
            sParamsLine[0].SqlDbType = SqlDbType.VarChar;
            sParamsLine[0].ParameterName = "lineCode";
            sParamsLine[0].Value = "";

            dtLine = cls.ExecuteDataTable(sqlLine, sParamsLine);
            cbbLine.DataSource = dtLine;
            cbbLine.DisplayMember = "lineName";
            cbbLine.ValueMember = "idx";
            dtLine.Rows.InsertAt(dtLine.NewRow(), 0);
            cbbLine.SelectedIndex = 0;

            // PIC NAME
            string sqlPic = "";
            sqlPic = "V2_BASE_CAPACITY_WORK_ORDER_SELPIC_ADDNEW";
            DataTable dtPic = new DataTable();
            dtPic = cls.fnSelect(sqlPic);
            cbbPIC.DataSource = dtPic;
            cbbPIC.DisplayMember = "picName";
            cbbPIC.ValueMember = "picIdx";
            dtPic.Rows.InsertAt(dtPic.NewRow(), 0);
            cbbPIC.SelectedIndex = 0;

            // SEQUENCE
            for (int i = 1; i <= 10; i++)
            {
                cbbSequence.Items.Add("Seq #" + i);
            }
            cbbSequence.Items.Insert(0, "");
            cbbSequence.SelectedIndex = 0;
        }

        public void fnInitSearch()
        {
            // SHIFT INFO
            cbbSearchShift.Items.Add("DAY");
            cbbSearchShift.Items.Add("NIGHT");
            cbbSearchShift.Items.Insert(0, "");
            cbbSearchShift.SelectedIndex = 0;

            // LINE NAME
            string sqlSearchLine = "";
            sqlSearchLine = "V2_BASE_CAPACITY_WORK_ORDER_SELECT_LINENAME_ADDNEW";
            DataTable dtSearchLine = new DataTable();

            SqlParameter[] sParamsSearchLine = new SqlParameter[1]; // Parameter count
            sParamsSearchLine[0] = new SqlParameter();
            sParamsSearchLine[0].SqlDbType = SqlDbType.VarChar;
            sParamsSearchLine[0].ParameterName = "lineCode";
            sParamsSearchLine[0].Value = "";

            dtSearchLine = cls.ExecuteDataTable(sqlSearchLine, sParamsSearchLine);
            cbbSearchLine.DataSource = dtSearchLine;
            cbbSearchLine.DisplayMember = "lineName";
            cbbSearchLine.ValueMember = "idx";
            dtSearchLine.Rows.InsertAt(dtSearchLine.NewRow(), 0);
            cbbSearchLine.SelectedIndex = 0;

            // PIC NAME
            string sqlSearchPic = "";
            sqlSearchPic = "V2_BASE_CAPACITY_WORK_ORDER_SELPIC_ADDNEW";
            DataTable dtSearchPic = new DataTable();
            dtSearchPic = cls.fnSelect(sqlSearchPic);
            foreach (DataRow dr in dtSearchPic.Rows)
            {
                string picName = cls.IndexOf(dr[1].ToString(),". ", 1);
                dr[1] = picName;
            }
            cbbSearchPIC.DataSource = dtSearchPic;
            cbbSearchPIC.DisplayMember = "picName";
            cbbSearchPIC.ValueMember = "picIdx";
            dtSearchPic.Rows.InsertAt(dtSearchPic.NewRow(), 0);
            cbbSearchPIC.SelectedIndex = 0;

            // STATUS INFO
            cbbSearchStatus.Items.Add("RUN");
            cbbSearchStatus.Items.Add("STOP");
            cbbSearchStatus.Items.Insert(0, "");
            cbbSearchStatus.SelectedIndex = 0;
        }

        public void fnShowTooltip()
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.chkOT01, "70 min (11:50:00 ~ 13:00:00)");
            toolTip1.SetToolTip(this.chkOT02, "40 min (17:00:00 ~ 17:40:00)");
            toolTip1.SetToolTip(this.chkOT03, "N/A");
        }

        private void cbbShift_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cbbShift.SelectedIndex>0)
            {
                btnFinish.Enabled = true;
                cbbProdName.Enabled = true;
                txtTimeSpan.Text = "0";
                if(cbbShift.Text=="DAY")
                {
                    dtpTimeFrom.Value = new DateTime(_dt.Year, _dt.Month, _dt.Day, 8, 0, 0);
                }
                else if(cbbShift.Text=="NIGHT")
                {
                    dtpTimeFrom.Value = new DateTime(_dt.Year, _dt.Month, _dt.Day, 20, 0, 0);
                }
            }
            else
            {
                cbbProdName.SelectedIndex = 0;
                cbbProdName.Enabled = false;
            }
            
        }

        private void cbbProdName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbProdName.SelectedIndex > 0)
            {
                cbbLine.Enabled = true;

                string prodId = cbbProdName.SelectedValue.ToString();
                string sqlUPH = "";
                sqlUPH = "V2_BASE_CAPACITY_WORK_ORDER_SELECT_UPH_ADDNEW";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "prodId";
                sParams[0].Value = prodId;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sqlUPH, sParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtUPH.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtProdCode.Text = ds.Tables[0].Rows[0][1].ToString();
                    hdfProdCode.Text = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    txtUPH.Text = "";
                    txtProdCode.Text = "";
                    hdfProdCode.Text = "";
                }

            }
            else
            {
                cbbLine.SelectedIndex = 0;
                cbbLine.Enabled = false;

                txtProdCode.Text = "N/A";
                hdfProdCode.Text = "N/A";
            }
        }

        private void cbbLine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbLine.SelectedIndex > 0)
            {
                cbbPIC.Enabled = true;
            }
            else
            {
                cbbPIC.SelectedIndex = 0;
                cbbPIC.Enabled = true;
            }
        }

        private void cbbPIC_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbPIC.SelectedIndex > 0)
            {
                if(_idxOrder=="")
                {
                    // ADDNEW
                    chkUseClockTime.Enabled = true;
                    chkUseClockTime.Checked = true;
                    txtTimeSpan.ReadOnly = true;

                    txtTimeSpan.Text = "0.0";
                }
                else
                {
                    // UPDATE
                }
            }
            else
            {
                chkUseClockTime.Enabled = false;
                chkUseClockTime.Checked = false;
                txtTimeSpan.ReadOnly = true;

                txtTimeSpan.Text = "";

                cbbPIC.Focus();
            }
        }

        private void chkUseClockTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseClockTime.Checked)
            {
                dtpTimeFrom.Enabled = true;
                dtpTimeTo.Enabled = true;
                //dtpTimeFrom.Value = DateTime.Now;
                //dtpTimeTo.Value = DateTime.Now;
                txtTimeSpan.ReadOnly = true;
                txtTimeSpan.Text = "0.0";
            }
            else
            {
                dtpTimeFrom.Enabled = false;
                dtpTimeTo.Enabled = false;
                txtTimeSpan.Enabled = true;
                txtTimeSpan.ReadOnly = false;
                txtTimeSpan.Focus();
            }
        }

        private void dtpTimeTo_ValueChanged(object sender, EventArgs e)
        {
            DateTime timeFr = dtpTimeFrom.Value;
            DateTime timeTo = dtpTimeTo.Value;
            DateTime _timeFr = new DateTime(timeFr.Year, timeFr.Month, timeFr.Day, timeFr.Hour, timeFr.Minute, 0);
            DateTime _timeTo = new DateTime(timeTo.Year, timeTo.Month, timeTo.Day, timeTo.Hour, timeTo.Minute, 0);
            double _timeMinus = 0;
            TimeSpan span=new TimeSpan();
            double second=0;

            if (cbbShift.Text == "DAY")
            {
                if (cls.isTimeBetween(_timeTo, _timeFr, _dtLunchStart) == true)
                {
                    _timeMinus = 0;
                }
                else if (cls.isTimeBetween(_timeTo, _timeFr, _dtDinnerStart) == true)
                {
                    _timeMinus = 1;
                }
                else if (cls.isTimeBetween(_timeTo, _timeFr, _dtDayStop) == true)
                {
                    _timeMinus = 1.5;
                }
                else if (cls.isTimeBetween(_timeTo, _dtLunchStart, _dtDinnerStart) == true)
                {
                    _timeMinus = 1;
                }
                else if (cls.isTimeBetween(_timeTo, _dtLunchStart, _dtDayStop) == true)
                {
                    _timeMinus = 1.5;
                }
                else if (cls.isTimeBetween(_timeTo, _dtDinnerStart, _dtDayStop) == true)
                {
                    _timeMinus = 0.5;
                }
            }
            else
            {
                if (cls.isTimeBetween(_timeTo, _timeFr, _dtNightmealStart) == true)
                {
                    _timeMinus = 0;
                }
                else if (cls.isTimeBetween(_timeTo, _timeFr, _dtBreakfastStart) == true)
                {
                    _timeMinus = 1;
                }
                else if (cls.isTimeBetween(_timeTo, _timeFr, _dtDayStart) == true)
                {
                    _timeMinus = 2;
                }
                else if (cls.isTimeBetween(_timeTo, _dtNightmealStart, _dtBreakfastStart) == true)
                {
                    _timeMinus = 1;
                }
                else if (cls.isTimeBetween(_timeTo, _dtNightmealStart, _dtDayStart) == true)
                {
                    _timeMinus = 2;
                }
                else
                {
                    _timeMinus = 1;
                }
            }

            if (_timeTo >= _timeFr)
            {
                span = _timeTo - _timeFr;
            }
            else
            {
                span = _timeTo.AddDays(1) - _timeFr;
            }

            second = ((span.TotalSeconds) / 3600) - _timeMinus;
            txtTimeSpan.Text = String.Format("{0:0.0}", second);
        }

        private void txtTimeSpan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double _total = 0, _span = 0, _uph = 0;
                string span = txtTimeSpan.Text.Trim();
                string uph = txtUPH.Text.Trim();

                if (txtTimeSpan.Text == "")
                {
                    txtTimeSpan.Text = "0.0";
                }

                if (span != "" && uph != "")
                {
                    _span = Convert.ToDouble(span);
                    _uph = Convert.ToDouble(uph);
                    _total = _span * _uph;
                    //DateTime _from = dtpTimeFrom.Value;
                    //dtpTimeTo.Value = _from.AddHours(_span);
                }
                else
                {
                    _total = 0;
                }

                hdfTotal.Text = ((int)_total).ToString();
                lblTotal.Text = String.Format("{0:n0}", _total);
                if (_total > 0)
                {
                    cbbSequence.Enabled = true;
                    rdbAdd.Enabled = true;
                    btnSave.Enabled = true;

                }
                else
                {
                    cbbSequence.SelectedIndex = 0;
                    cbbSequence.Enabled = false;
                    rdbAdd.Checked = false;
                    rdbAdd.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
            catch
            {

            }


        }

        private void cbbSequence_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbSequence.SelectedIndex > 0)
            {
                if (_idxOrder == "")
                {
                    rdbAdd.Enabled = true;
                    rdbAdd.Checked = true;
                    rdbUpd.Enabled = false;
                    rdbDel.Enabled = false;

                    btnSave.Enabled = true;
                    btnFinish.Enabled = true;
                }
                else
                {
                    rdbAdd.Enabled = false;
                    rdbAdd.Checked = false;
                    rdbUpd.Enabled = true;
                    rdbDel.Enabled = true;

                    btnSave.Enabled = false;
                    btnFinish.Enabled = true;
                }
            }
            else
            {
                rdbAdd.Enabled = false;
                rdbUpd.Enabled = false;
                rdbDel.Enabled = false;

                rdbAdd.Checked = false;
                rdbUpd.Checked = false;
                rdbDel.Checked = false;

                btnSave.Enabled = false;
                btnFinish.Enabled = false;
            }
        }

        private void rdbUpd_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void rdbDel_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            fnFinish();
        }

        public void fnFinish()
        {
            //fnBindData();
            _idxOrder = "";
            hdfProdCode.Text = "";
            hdfTotal.Text = "";

            dtpDate.Enabled = true;
            cbbShift.Enabled = true;
            cbbShift.SelectedIndex = 0;
            cbbProdName.SelectedIndex = 0;
            cbbProdName.Enabled = false;
            txtProdCode.Enabled = true;
            txtProdCode.ReadOnly = true;
            txtProdCode.Text = "";
            cbbLine.SelectedIndex = 0;
            cbbLine.Enabled = false;
            cbbPIC.SelectedIndex = 0;
            cbbPIC.Enabled = false;

            dtpTimeFrom.Value = DateTime.Now;
            dtpTimeFrom.Enabled = false;
            dtpTimeTo.Value = DateTime.Now;
            dtpTimeTo.Enabled = false;
            chkUseClockTime.Checked = false;
            chkUseClockTime.Enabled = false;
            txtTimeSpan.Text = "";
            txtTimeSpan.Enabled = true;
            txtTimeSpan.ReadOnly = true;
            txtUPH.Text = "";
            txtUPH.Enabled = true;
            txtUPH.ReadOnly = true;
            cbbSequence.SelectedIndex = 0;
            cbbSequence.Enabled = false;
            lblTotal.Text = "0";

            chkStop.Enabled = false;
            chkStop.Checked = false;
            txtStopNote.Enabled = false;
            txtReason.Enabled = false;
            txtAction.Enabled = false;
            lblMessage.Text = "";
            rdbAdd.Checked = false;
            rdbUpd.Checked = false;
            rdbDel.Checked = false;
            rdbAdd.Enabled = false;
            rdbUpd.Enabled = false;
            rdbDel.Enabled = false;
            btnSave.Enabled = false;
            btnFinish.Enabled = false;

            dgvListOrder.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime _assyDate = dtpDate.Value;
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                if (rdbAdd.Checked)
                {
                    fnAdd();
                }
                else if (rdbUpd.Checked)
                {
                    fnUpd();
                }
                else if (rdbDel.Checked)
                {
                    fnDel();
                }
                fnBindSearchDate(_assyDate);
                fnFinish();
            }
        }

        public void fnAdd()
        {
            DateTime assyDate = dtpDate.Value;
            string assyShift = cbbShift.Text;
            string prodId = cbbProdName.SelectedValue.ToString();
            string prodName = cbbProdName.Text;
            string prodCode = hdfProdCode.Text;
            string lineId = cbbLine.SelectedValue.ToString();
            string lineName = cbbLine.Text;
            string picId = cbbPIC.SelectedValue.ToString();
            string picName = cls.IndexOf(cbbPIC.Text, 1);
            DateTime _from = dtpTimeFrom.Value;
            DateTime _to = dtpTimeTo.Value;
            DateTime __to = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0);
            DateTime from;
            //if (chkUseClockTime.Checked && cbbSequence.Text == "Seq #1")
            if (!chkUseClockTime.Checked && cbbSequence.SelectedIndex > 0)
            {
                if (assyShift == "DAY")
                {
                    from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 8, 0, 0);
                }
                else
                {
                    from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 20, 0, 0);
                }
            }
            else
            {
                from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _from.Hour, _from.Minute, 0);
            }
            //DateTime from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _from.Hour, _from.Minute, 0);
            DateTime _nightStart = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 20, 0, 0);
            DateTime _endDay = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 23, 59, 59);
            DateTime to = (cls.isTimeBetween(__to, _nightStart, _endDay) == true) ? new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0) : new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0).AddDays(1);
            byte useClock = Convert.ToByte((chkUseClockTime.Checked) ? 1 : 0);
            string span = txtTimeSpan.Text.Trim();
            string uph = txtUPH.Text.Trim();
            string seq = cbbSequence.Text.Replace("Seq #", "");
            string total = hdfTotal.Text.Trim();
            //string stop = (chkStop.Checked) ? "1" : "0";
            //string note = txtStopNote.Text.Trim();

            if(useClock==0)
            {
                double _span = Convert.ToDouble(span);
                to = to.AddHours(_span);
                DateTime toAppend = to;
                if (assyShift == "DAY")
                {


                    if (cls.isTimeBetween(to, _dtDayStart, _dtLunchStart) == true)
                    {
                        toAppend = to.AddHours(0);
                    }
                    else if (cls.isTimeBetween(to, _dtDayStart, _dtDinnerStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtDayStart, _dtDayStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtLunchStart, _dtDinnerStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtLunchStart, _dtDayStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtDinnerStart, _dtDayStop) == true)
                    {
                        toAppend = to.AddHours(0.5);
                    }
                }
                else if (assyShift == "NIGHT")
                {
                    if (cls.isTimeBetween(to, _dtNightStart, _dtNightmealStart) == true)
                    {
                        toAppend = to.AddHours(0);
                    }
                    else if (cls.isTimeBetween(to, _dtNightStart, _dtBreakfastStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtNightStart, _dtNightStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtNightmealStart, _dtBreakfastStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtNightmealStart, _dtNightStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtBreakfastStart, _dtNightStop) == true)
                    {
                        toAppend = to.AddHours(0.5);
                    }
                }

                //MessageBox.Show(to.ToString());
            }

            

            string msg = "";
            msg += "date: " + assyDate.ToShortDateString() + "\r\n";
            msg += "shift: " + assyShift + "\r\n";
            msg += "prodId: " + prodId + "\r\n";
            msg += "prodname: " + prodName + "\r\n";
            msg += "prodcode: " + prodCode + "\r\n";
            msg += "lineId: " + lineId + "\r\n";
            msg += "linename: " + lineName + "\r\n";
            msg += "picId: " + picId + "\r\n";
            msg += "picname: " + picName + "\r\n";
            msg += "timefrom: " + from + "\r\n";
            msg += "timeto: " + to + "\r\n";
            msg += "useclock: " + useClock + "\r\n";
            msg += "span: " + span + "\r\n";
            msg += "uph: " + uph + "\r\n";
            msg += "seq: " + seq + "\r\n";
            msg += "total: " + total + "\r\n";
            //msg += "stop: " + stop + "\r\n";
            //msg += "note: " + note + "\r\n";
            //MessageBox.Show(msg);

            string sql = "";
            sql = "V2o1_BASE_Capacity_Work_Order_AddItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[16]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "assyDate";
            sParams[0].Value = assyDate;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "assyShift";
            sParams[1].Value = assyShift;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "prodId";
            sParams[2].Value = prodId;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "prodName";
            sParams[3].Value = prodName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.VarChar;
            sParams[4].ParameterName = "prodCode";
            sParams[4].Value = prodCode;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.Int;
            sParams[5].ParameterName = "lineId";
            sParams[5].Value = lineId;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "lineName";
            sParams[6].Value = lineName;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Int;
            sParams[7].ParameterName = "picId";
            sParams[7].Value = picId;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.NVarChar;
            sParams[8].ParameterName = "picName";
            sParams[8].Value = picName;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.DateTime;
            sParams[9].ParameterName = "timeFrom";
            sParams[9].Value = Convert.ToDateTime(from);

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.DateTime;
            sParams[10].ParameterName = "timeTo";
            sParams[10].Value = Convert.ToDateTime(to);

            sParams[11] = new SqlParameter();
            sParams[11].SqlDbType = SqlDbType.Bit;
            sParams[11].ParameterName = "useClock";
            sParams[11].Value = useClock;

            sParams[12] = new SqlParameter();
            sParams[12].SqlDbType = SqlDbType.Decimal;
            sParams[12].ParameterName = "timeSpan";
            sParams[12].Value = Convert.ToDecimal(span);

            sParams[13] = new SqlParameter();
            sParams[13].SqlDbType = SqlDbType.SmallInt;
            sParams[13].ParameterName = "prodUPH";
            sParams[13].Value = Convert.ToInt16(uph);

            sParams[14] = new SqlParameter();
            sParams[14].SqlDbType = SqlDbType.TinyInt;
            sParams[14].ParameterName = "assySeq";
            sParams[14].Value = Convert.ToInt16(seq);

            sParams[15] = new SqlParameter();
            sParams[15].SqlDbType = SqlDbType.Int;
            sParams[15].ParameterName = "orderTotal";
            sParams[15].Value = Convert.ToInt32(total);

            //sParams[16] = new SqlParameter();
            //sParams[16].SqlDbType = SqlDbType.NVarChar;
            //sParams[16].ParameterName = "assyNote";
            //sParams[16].Value = note;

            //sParams[17] = new SqlParameter();
            //sParams[17].SqlDbType = SqlDbType.Bit;
            //sParams[17].ParameterName = "assyStop";
            //sParams[17].Value = stop;

            cls.fnUpdDel(sql, sParams);
        }

        public void fnUpd()
        {
            string idx = _idxOrder;
            DateTime assyDate = dtpDate.Value;
            string assyShift = cbbShift.Text;
            string prodId = cbbProdName.SelectedValue.ToString();
            string prodName = cbbProdName.Text;
            string prodCode = hdfProdCode.Text;
            string lineId = cbbLine.SelectedValue.ToString();
            string lineName = cbbLine.Text;
            string picId = cbbPIC.SelectedValue.ToString();
            string picName = cls.IndexOf(cbbPIC.Text, 1);
            DateTime _from = dtpTimeFrom.Value;
            DateTime _to = dtpTimeTo.Value;
            DateTime __to = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0);
            DateTime from;

            //if (chkUseClockTime.Checked && cbbSequence.SelectedIndex > 0)
            //{
            //    if (assyShift == "DAY")
            //    {
            //        from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 8, 0, 0);
            //    }
            //    else
            //    {
            //        from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 20, 0, 0);
            //    }
            //}
            //else
            //{
            //    from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _from.Hour, _from.Minute, 0);
            //}
            from = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _from.Hour, _from.Minute, 0);
            
            //DateTime from = (chkUseClockTime.Checked&&cbbSequence.Text=="Seq #1")?new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _from.Hour, _from.Minute, 0);
            //DateTime to = (cls.isTimeBetween(_to, _dtNightStart, _dtPrevDayStop) == true) ? new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0) : new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0).AddDays(1);
            DateTime _nightStart = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 20, 0, 0);
            DateTime _endDay = new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, 23, 59, 59);
            DateTime to = (cls.isTimeBetween(__to, _nightStart, _endDay) == true) ? new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0) : new DateTime(assyDate.Year, assyDate.Month, assyDate.Day, _to.Hour, _to.Minute, 0).AddDays(1);
            byte useClock = Convert.ToByte((chkUseClockTime.Checked) ? 1 : 0);
            string span = txtTimeSpan.Text.Trim();
            string uph = txtUPH.Text.Trim();
            string seq = cbbSequence.Text.Replace("Seq #", "");
            string total = hdfTotal.Text.Trim();
            string note = (txtStopNote.Enabled == true) ? txtStopNote.Text.Trim() : "";
            byte stop = Convert.ToByte((chkStop.Checked && chkStop.Enabled == true) ? 1 : 0);
            string reason = txtReason.Text.Trim();

            if (useClock == 0)
            {
                double _span = Convert.ToDouble(span);
                to = to.AddHours(_span);
                DateTime toAppend = to;
                if (assyShift == "DAY")
                {
                    if (cls.isTimeBetween(to, _dtDayStart, _dtLunchStart) == true)
                    {
                        toAppend = to.AddHours(0);
                    }
                    else if (cls.isTimeBetween(to, _dtDayStart, _dtDinnerStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtDayStart, _dtDayStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtLunchStart, _dtDinnerStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtLunchStart, _dtDayStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtDinnerStart, _dtDayStop) == true)
                    {
                        toAppend = to.AddHours(0.5);
                    }
                }
                else if (assyShift == "NIGHT")
                {
                    if (cls.isTimeBetween(to, _dtNightStart, _dtNightmealStart) == true)
                    {
                        toAppend = to.AddHours(0);
                    }
                    else if (cls.isTimeBetween(to, _dtNightStart, _dtBreakfastStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtNightStart, _dtNightStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtNightmealStart, _dtBreakfastStart) == true)
                    {
                        toAppend = to.AddHours(1);
                    }
                    else if (cls.isTimeBetween(to, _dtNightmealStart, _dtNightStop) == true)
                    {
                        toAppend = to.AddHours(1.5);
                    }
                    else if (cls.isTimeBetween(to, _dtBreakfastStart, _dtNightStop) == true)
                    {
                        toAppend = to.AddHours(0.5);
                    }
                }
            }



            //string msg = "";
            //msg += "idx: " + idx + "\r\n";
            //msg += "date: " + assyDate.ToShortDateString() + "\r\n";
            //msg += "shift: " + assyShift + "\r\n";
            //msg += "prodId: " + prodId + "\r\n";
            //msg += "prodname: " + prodName + "\r\n";
            //msg += "prodcode: " + prodCode + "\r\n";
            //msg += "lineId: " + lineId + "\r\n";
            //msg += "linename: " + lineName + "\r\n";
            //msg += "picId: " + picId + "\r\n";
            //msg += "picname: " + picName + "\r\n";
            //msg += "timefrom: " + from + "\r\n";
            //msg += "timeto: " + to + "\r\n";
            //msg += "useclock: " + useClock + "\r\n";
            //msg += "span: " + span + "\r\n";
            //msg += "uph: " + uph + "\r\n";
            //msg += "seq: " + seq + "\r\n";
            //msg += "total: " + total + "\r\n";
            //msg += "note: " + note + "\r\n";
            //msg += "stop: " + stop + "\r\n";
            //MessageBox.Show(msg);

            string sql = "";
            sql = "V2o1_BASE_Capacity_Work_Order_UpdItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[20]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "assyDate";
            sParams[0].Value = assyDate;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "assyShift";
            sParams[1].Value = assyShift;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "prodId";
            sParams[2].Value = prodId;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "prodName";
            sParams[3].Value = prodName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.VarChar;
            sParams[4].ParameterName = "prodCode";
            sParams[4].Value = prodCode;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.Int;
            sParams[5].ParameterName = "lineId";
            sParams[5].Value = lineId;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "lineName";
            sParams[6].Value = lineName;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Int;
            sParams[7].ParameterName = "picId";
            sParams[7].Value = picId;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.NVarChar;
            sParams[8].ParameterName = "picName";
            sParams[8].Value = picName;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.DateTime;
            sParams[9].ParameterName = "timeFrom";
            sParams[9].Value = Convert.ToDateTime(from);

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.DateTime;
            sParams[10].ParameterName = "timeTo";
            sParams[10].Value = Convert.ToDateTime(to);

            sParams[11] = new SqlParameter();
            sParams[11].SqlDbType = SqlDbType.Bit;
            sParams[11].ParameterName = "useClock";
            sParams[11].Value = useClock;

            sParams[12] = new SqlParameter();
            sParams[12].SqlDbType = SqlDbType.Decimal;
            sParams[12].ParameterName = "timeSpan";
            sParams[12].Value = Convert.ToDecimal(span);

            sParams[13] = new SqlParameter();
            sParams[13].SqlDbType = SqlDbType.SmallInt;
            sParams[13].ParameterName = "prodUPH";
            sParams[13].Value = Convert.ToInt16(uph);

            sParams[14] = new SqlParameter();
            sParams[14].SqlDbType = SqlDbType.TinyInt;
            sParams[14].ParameterName = "assySeq";
            sParams[14].Value = Convert.ToInt16(seq);

            sParams[15] = new SqlParameter();
            sParams[15].SqlDbType = SqlDbType.Int;
            sParams[15].ParameterName = "orderTotal";
            sParams[15].Value = Convert.ToInt32(total);

            sParams[16] = new SqlParameter();
            sParams[16].SqlDbType = SqlDbType.NVarChar;
            sParams[16].ParameterName = "assyNote";
            sParams[16].Value = note;

            sParams[17] = new SqlParameter();
            sParams[17].SqlDbType = SqlDbType.Bit;
            sParams[17].ParameterName = "assyStop";
            sParams[17].Value = stop;

            sParams[18] = new SqlParameter();
            sParams[18].SqlDbType = SqlDbType.Int;
            sParams[18].ParameterName = "idx";
            sParams[18].Value = idx;

            sParams[19] = new SqlParameter();
            sParams[19].SqlDbType = SqlDbType.NVarChar;
            sParams[19].ParameterName = "reason";
            sParams[19].Value = reason;

            cls.fnUpdDel(sql, sParams);
        }

        public void fnUpd_V2o1()
        {
            DateTime timeDt = dtpDate.Value;
            int shift = (cbbShift.SelectedIndex > 1) ? 2 : 1;
            string picIDx = cbbPIC.SelectedValue.ToString();
            string picNm = cls.IndexOf(cbbPIC.Text, 1);
            DateTime timeFr = dtpTimeFrom.Value;
            DateTime timeTo = dtpTimeTo.Value;
            string timeSpan = txtTimeSpan.Text.Trim();
            string seq = cbbSequence.Text.Replace("Seq #", "");
            string total = lblTotal.Text.Trim();
            string note = (txtStopNote.Enabled == true) ? txtStopNote.Text.Trim() : "";
            byte stop = Convert.ToByte((chkStop.Checked && chkStop.Enabled == true) ? 1 : 0);
            string reason = txtReason.Text.Trim();

            DateTime dateFR, dateTO;
            DateTime shiftDB, shiftDE, shiftNB, shiftNE;
            DateTime restLB, restLE, restDB, restDE, restMB, restME, restBB, restBE;
            int restTM = 0;

            if (timeFr < timeTo)
            {
                shiftDB = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 8, 0, 0);
                shiftDE = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 19, 59, 59);
                shiftNB = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 20, 0, 0);
                shiftNE = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 8, 0, 0).AddDays(1);

                restLB = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 11, 50, 0);
                restLE = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 13, 0, 0);
                restDB = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 17, 0, 0);
                restDE = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 17, 40, 0);
                restMB = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 23, 50, 0);
                restME = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 1, 0, 0).AddDays(1);
                restBB = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 5, 0, 0).AddDays(1);
                restBE = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, 5, 40, 0).AddDays(1);

                dateFR = new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, timeFr.Hour, timeFr.Minute, 0);
                dateTO = (shift == 1) ? new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, timeTo.Hour, timeTo.Minute, 0) : new DateTime(timeDt.Year, timeDt.Month, timeDt.Day, timeTo.Hour, timeTo.Minute, 0).AddDays(1);

                if ((dateFR >= shiftDB && dateFR < restLB) || (dateFR >= shiftNB && dateFR < restMB))
                {
                    /* 08:00 < FROM < 11:50 || 20:00 < FROM < 23:50 */

                    if((dateTO >= shiftDB && dateTO < restLB) || (dateTO >= shiftNB && dateTO < restMB)) { restTM = 0; }
                    else if ((dateTO >= restLB && dateTO < restDB) || dateTO >= restMB && dateTO < restBB) { restTM = 70; }
                    else if ((dateTO >= restDB) || dateTO >= restBB) { restTM = 110; }
                }
                else if ((dateFR >= restLB && dateFR < restDB) || dateFR >= restMB && dateFR < restBB)
                {
                    /* 11:50 < FROM < 17:00 || 23:50 < FROM < 05:00 */

                    if ((dateTO >= shiftDB && dateTO < restLB) || (dateTO >= shiftNB && dateTO < restMB)) { restTM = 0; }
                    else if ((dateTO >= restLB && dateTO < restDB) || dateTO >= restMB && dateTO < restBB) { restTM = 0; }
                    else if ((dateTO >= restDB) || dateTO >= restBB) { restTM = 40; }
                }
                //else if ((dateFR >= restDB && dateFR < shiftNB) || dateFR >= restBB && dateFR < shiftDB.AddDays(1))
                else if ((dateFR >= restDB) || dateFR >= restBB)
                {
                    /* 17:00 < FROM < 20:00 || 05:00 < FROM < 08:00 */

                    if ((dateTO >= shiftDB && dateTO < restLB) || (dateTO >= shiftNB && dateTO < restMB)) { restTM = 0; }
                    else if ((dateTO >= restLB && dateTO < restDB) || dateTO >= restMB && dateTO < restBB) { restTM = 0; }
                    else if ((dateTO >= restDB) || dateTO >= restBB) { restTM = 0; }
                }


            }
            else
            {
                MessageBox.Show("Time From cannot over than time To. Please check again.");
            }
        }

        public void fnDel()
        {
            DateTime assyDate = dtpDate.Value;
            string assyShift = cbbShift.Text;
            string idx = _idxOrder;
            string prodId = cbbProdName.SelectedValue.ToString();
            string prodName = cbbProdName.Text;
            string prodCode = hdfProdCode.Text;
            string lineId = cbbLine.SelectedValue.ToString();
            string lineName = cbbLine.Text;

            string sql = "";
            sql = "V2o1_BASE_Capacity_Work_Order_DelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[8]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "assyDate";
            sParams[0].Value = assyDate;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "assyShift";
            sParams[1].Value = assyShift;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "prodId";
            sParams[2].Value = prodId;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "prodName";
            sParams[3].Value = prodName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.VarChar;
            sParams[4].ParameterName = "prodCode";
            sParams[4].Value = prodCode;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.Int;
            sParams[5].ParameterName = "lineId";
            sParams[5].Value = lineId;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "lineName";
            sParams[6].Value = lineName;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Int;
            sParams[7].ParameterName = "idx";
            sParams[7].Value = idx;

            cls.fnUpdDel(sql, sParams);
        }

        private void dtpTimeFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpTimeTo.Value = dtpTimeFrom.Value;
        }

        private void dgvListOrder_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string useClock = "";
            string active = "";
            foreach (DataGridViewRow row in dgvListOrder.Rows)
            {
                active = row.Cells[22].Value.ToString();
                if(active.ToLower()=="true")
                {
                    row.DefaultCellStyle.BackColor = Color.Tomato;
                }

                useClock = row.Cells[12].Value.ToString();
                if (useClock.ToLower() == "false")
                {
                    row.Cells[10].Style.BackColor = Color.LightGray;
                    row.Cells[11].Style.BackColor = Color.LightGray;
                }
            }
        }

        private void dgvListOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //fnBindData();
            cls.fnDatagridClickCell(dgvListOrder, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvListOrder.Rows[e.RowIndex];
            string idx = row.Cells[0].Value.ToString();
            DateTime date = Convert.ToDateTime(row.Cells[1].Value.ToString());
            string shift = row.Cells[2].Value.ToString();
            string lineId = row.Cells[3].Value.ToString();
            string line = row.Cells[4].Value.ToString();
            string prodId = row.Cells[5].Value.ToString();
            string prod = row.Cells[6].Value.ToString();
            string code = row.Cells[7].Value.ToString();
            string picId = row.Cells[8].Value.ToString();
            string pic = row.Cells[9].Value.ToString();
            string clock = row.Cells[12].Value.ToString();
            DateTime from = (clock.ToLower() == "true") ? Convert.ToDateTime(row.Cells[10].Value.ToString()) : Convert.ToDateTime(row.Cells[25].Value.ToString());
            DateTime to = (clock.ToLower() == "true") ? Convert.ToDateTime(row.Cells[11].Value.ToString()) : Convert.ToDateTime(row.Cells[26].Value.ToString());
            string span = row.Cells[13].Value.ToString();
            string uph = row.Cells[14].Value.ToString();
            string seq = row.Cells[15].Value.ToString();
            string order = row.Cells[16].Value.ToString();
            string note = row.Cells[17].Value.ToString();
            string ok = row.Cells[18].Value.ToString();
            string ng = row.Cells[19].Value.ToString();
            string total = row.Cells[20].Value.ToString();
            string rate = row.Cells[21].Value.ToString();
            string stop = row.Cells[22].Value.ToString();
            string dateadd = row.Cells[23].Value.ToString();
            string active = row.Cells[24].Value.ToString();
            string reason = row.Cells[27].Value.ToString();
            string measure = row.Cells[28].Value.ToString();

            _idxOrder = idx;

            dtpDate.Value = date;
            cbbShift.Text = shift;
            cbbLine.SelectedValue = lineId;
            cbbProdName.SelectedValue = prodId;
            txtProdCode.Text = code;
            hdfProdCode.Text = code;
            cbbPIC.SelectedValue = picId;
            if(clock.ToLower()=="false")
            {
                dtpTimeFrom.Value = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                dtpTimeTo.Value = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                chkUseClockTime.Checked = false;

                dtpTimeFrom.Enabled = false;
                dtpTimeTo.Enabled = false;
                txtTimeSpan.Enabled = true;
                txtTimeSpan.ReadOnly = false;
            }
            else
            {
                dtpTimeFrom.Value = from;
                dtpTimeTo.Value = to;
                chkUseClockTime.Checked = true;

                dtpTimeFrom.Enabled = true;
                dtpTimeTo.Enabled = true;
                txtTimeSpan.Enabled = false;
                txtTimeSpan.ReadOnly = true;
            }
            txtTimeSpan.Text = span;
            txtUPH.Text = uph;
            cbbSequence.Text = "Seq #" + seq;
            lblTotal.Text = order;
            hdfTotal.Text = order;
            if(stop.ToLower()=="true")
            {
                chkStop.Checked = true;
                chkStop.Enabled = true;
                txtStopNote.Text = note;
                txtStopNote.Enabled = true;
            }
            else
            {
                chkStop.Checked = false;
                chkStop.Enabled = true;
                txtStopNote.Text = "";
                txtStopNote.Enabled = false;
            }

            dtpDate.Enabled = false;
            cbbShift.Enabled = false;
            cbbProdName.Enabled = false;
            txtProdCode.Enabled = false;
            cbbLine.Enabled = false;
            cbbPIC.Enabled = true;
            chkUseClockTime.Enabled = true;
            txtUPH.Enabled = false;
            cbbSequence.Enabled = true;
            txtReason.Enabled = true;
            txtReason.Text = reason;
            txtAction.Enabled = (txtReason.Enabled == true && reason != "" && reason != null) ? true : false;
            txtAction.Text = measure;
            //chkStop.Enabled = true;
            //txtStopNote.Enabled = false;
            rdbAdd.Enabled = false;
            rdbAdd.Checked = false;
            rdbUpd.Enabled = true;
            rdbDel.Enabled = true;
            btnSave.Enabled = false;
            btnFinish.Enabled = true;
        }

        private void chkStop_CheckedChanged(object sender, EventArgs e)
        {
            if(chkStop.Checked)
            {
                txtStopNote.Enabled = true;
                txtStopNote.Focus();
            }
            else
            {
                txtStopNote.Text = "";
                txtStopNote.Enabled = false;
            }
        }

        private void chkSearchAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSearchAll.Checked)
            {
                dtpSearchDate.Enabled = false;
                cbbSearchShift.Enabled = false;
                cbbSearchLine.Enabled = false;
                cbbSearchPIC.Enabled = false;
                cbbSearchStatus.Enabled = false;
            }
            else
            {
                dtpSearchDate.Enabled = true;
                cbbSearchShift.Enabled = true;
                cbbSearchLine.Enabled = true;
                cbbSearchPIC.Enabled = true;
                cbbSearchStatus.Enabled = true;
            }

            dtpSearchDate.Value = _dt;
            cbbSearchShift.SelectedIndex = 0;
            cbbSearchLine.SelectedIndex = 0;
            cbbSearchPIC.SelectedIndex = 0;
            cbbSearchStatus.SelectedIndex = 0;
        }

        private void btnSearchToday_Click(object sender, EventArgs e)
        {
            if(chkSearchAll.Checked)
            {
                chkSearchAll.Checked = false;
            }

            dtpSearchDate.Value = _dt;
            cbbSearchShift.SelectedIndex = 0;
            cbbSearchLine.SelectedIndex = 0;
            cbbSearchPIC.SelectedIndex = 0;
            cbbSearchStatus.SelectedIndex = 0;

            fnBindData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvListOrder.DataSource = "";
            dgvListOrder.Refresh();

            DateTime searchDate = dtpSearchDate.Value;
            string searchAll = (chkSearchAll.Checked) ? "ALL" : "DATE";
            //string _searchShift = cbbSearchShift.Text;
            //string _searchLineId = cbbSearchLine.SelectedValue.ToString();
            //string _searchLinename = cbbSearchLine.Text;
            //string _searchPicId = cbbSearchPIC.SelectedValue.ToString();
            //string _searchPicname = cbbSearchPIC.Text;
            //string _searchStatus = cbbSearchStatus.Text;

            //string searchShift = (_searchShift != "" && _searchShift != null) ? cbbSearchShift.Text : "";
            //string searchLineId = (cbbSearchLine.SelectedIndex > 0) ? cbbSearchLine.SelectedValue.ToString() : "";
            //string searchLinename = (cbbSearchLine.SelectedIndex > 0) ? cbbSearchLine.Text : "";
            //string searchPicId = (cbbSearchPIC.SelectedIndex > 0) ? cbbSearchPIC.SelectedValue.ToString() : "";
            //string searchPicname = (cbbSearchPIC.SelectedIndex > 0) ? cbbSearchPIC.Text : "";
            //string searchStatus = (cbbSearchStatus.SelectedIndex > 0) ? (cbbSearchStatus.SelectedIndex == 1) ? "1" : "0" : "0";

            //string msg = "";
            //msg += "searchDate: " + searchDate + "\r\n";
            //msg += "searchShift: " + searchShift + "\r\n";
            //msg += "searchLineId: " + searchLineId + "\r\n";
            //msg += "searchLinename: " + searchLinename + "\r\n";
            //msg += "searchPicId: " + searchPicId + "\r\n";
            //msg += "searchPicname: " + searchPicname + "\r\n";
            //msg += "searchStatus: " + searchStatus + "\r\n";

            //MessageBox.Show(msg);

            //string sql = "V2o1_BASE_Capacity_Work_Order_Search_Addnew";
            string sql = "V2o1_BASE_Capacity_Work_Order_Search_Date_V1o2_Addnew";
            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "searchDate";
            sParams[0].Value = searchDate;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "searchAll";
            sParams[1].Value = searchAll;

            //sParams[2] = new SqlParameter();
            //sParams[2].SqlDbType = SqlDbType.Int;
            //sParams[2].ParameterName = "searchLineId";
            //sParams[2].Value = searchLineId;

            //sParams[3] = new SqlParameter();
            //sParams[3].SqlDbType = SqlDbType.VarChar;
            //sParams[3].ParameterName = "searchLinename";
            //sParams[3].Value = searchLinename;

            //sParams[4] = new SqlParameter();
            //sParams[4].SqlDbType = SqlDbType.Int;
            //sParams[4].ParameterName = "searchPicId";
            //sParams[4].Value = searchPicId;

            //sParams[5] = new SqlParameter();
            //sParams[5].SqlDbType = SqlDbType.NVarChar;
            //sParams[5].ParameterName = "searchPicname";
            //sParams[5].Value = searchPicname;

            //sParams[6] = new SqlParameter();
            //sParams[6].SqlDbType = SqlDbType.Bit;
            //sParams[6].ParameterName = "searchStatus";
            //sParams[6].Value = searchStatus;

            DataTable dtSearch = new DataTable();
            dtSearch = cls.ExecuteDataTable(sql, CommandType.StoredProcedure, "conn", sParams);

            foreach (DataRow dr in dtSearch.Rows)
            {
                string clock = dr[12].ToString();
                if (clock.ToLower() == "false")
                {
                    dr[10] = new TimeSpan();
                    dr[11] = new TimeSpan();
                }
            }

            dgvListOrder.DataSource = dtSearch;
            dgvListOrder.Refresh();

            //dgvListOrder.Columns[0].Width = 10 * _dgvListOrderWidth / 100;    // idx
            dgvListOrder.Columns[1].Width = 7 * _dgvListOrderWidth / 100;    // date
            dgvListOrder.Columns[2].Width = 5 * _dgvListOrderWidth / 100;    // shift
            //dgvListOrder.Columns[3].Width = 10 * _dgvListOrderWidth / 100;    // lineId
            dgvListOrder.Columns[4].Width = 8 * _dgvListOrderWidth / 100;    // lineName
            //dgvListOrder.Columns[5].Width = 10 * _dgvListOrderWidth / 100;    // prodId
            dgvListOrder.Columns[6].Width = 12 * _dgvListOrderWidth / 100;    // prodName
            dgvListOrder.Columns[7].Width = 8 * _dgvListOrderWidth / 100;    // prodCode
            //dgvListOrder.Columns[8].Width = 10 * _dgvListOrderWidth / 100;    // picId
            dgvListOrder.Columns[9].Width = 10 * _dgvListOrderWidth / 100;    // picName
            dgvListOrder.Columns[10].Width = 5 * _dgvListOrderWidth / 100;    // timeFrom
            dgvListOrder.Columns[11].Width = 5 * _dgvListOrderWidth / 100;    // timeTo
            //dgvListOrder.Columns[12].Width = 10 * _dgvListOrderWidth / 100;    // useClock
            dgvListOrder.Columns[13].Width = 5 * _dgvListOrderWidth / 100;    // timeSpan
            dgvListOrder.Columns[14].Width = 5 * _dgvListOrderWidth / 100;    // UPH
            dgvListOrder.Columns[15].Width = 5 * _dgvListOrderWidth / 100;    // Seq
            dgvListOrder.Columns[16].Width = 5 * _dgvListOrderWidth / 100;    // totalOrder
            //dgvListOrder.Columns[17].Width = 10 * _dgvListOrderWidth / 100;    // note
            dgvListOrder.Columns[18].Width = 5 * _dgvListOrderWidth / 100;    // OK
            dgvListOrder.Columns[19].Width = 5 * _dgvListOrderWidth / 100;    // NG
            dgvListOrder.Columns[20].Width = 5 * _dgvListOrderWidth / 100;    // Total
            dgvListOrder.Columns[21].Width = 5 * _dgvListOrderWidth / 100;    // Rate
            //dgvListOrder.Columns[22].Width = 10 * _dgvListOrderWidth / 100;    // Stop
            //dgvListOrder.Columns[23].Width = 10 * _dgvListOrderWidth / 100;    // DateAdd
            //dgvListOrder.Columns[24].Width = 10 * _dgvListOrderWidth / 100;    // Active
            //dgvListOrder.Columns[25].Width = 10 * _dgvListOrderWidth / 100;    // Active
            //dgvListOrder.Columns[26].Width = 10 * _dgvListOrderWidth / 100;    // Active
            //dgvListOrder.Columns[27].Width = 10 * _dgvListOrderWidth / 100;    // Active
            //dgvListOrder.Columns[28].Width = 10 * _dgvListOrderWidth / 100;    // Action


            dgvListOrder.Columns[0].Visible = false;
            dgvListOrder.Columns[1].Visible = true;
            dgvListOrder.Columns[2].Visible = true;
            dgvListOrder.Columns[3].Visible = false;
            dgvListOrder.Columns[4].Visible = true;
            dgvListOrder.Columns[5].Visible = false;
            dgvListOrder.Columns[6].Visible = true;
            dgvListOrder.Columns[7].Visible = true;
            dgvListOrder.Columns[8].Visible = false;
            dgvListOrder.Columns[9].Visible = true;
            dgvListOrder.Columns[10].Visible = true;
            dgvListOrder.Columns[11].Visible = true;
            dgvListOrder.Columns[12].Visible = false;
            dgvListOrder.Columns[13].Visible = true;
            dgvListOrder.Columns[14].Visible = true;
            dgvListOrder.Columns[15].Visible = true;
            dgvListOrder.Columns[16].Visible = true;
            dgvListOrder.Columns[17].Visible = false;
            dgvListOrder.Columns[18].Visible = true;
            dgvListOrder.Columns[19].Visible = true;
            dgvListOrder.Columns[20].Visible = true;
            dgvListOrder.Columns[21].Visible = true;
            dgvListOrder.Columns[22].Visible = false;
            dgvListOrder.Columns[23].Visible = false;
            dgvListOrder.Columns[24].Visible = false;
            dgvListOrder.Columns[25].Visible = false;
            dgvListOrder.Columns[26].Visible = false;
            dgvListOrder.Columns[27].Visible = false;
            dgvListOrder.Columns[28].Visible = false;

            cls.fnFormatDatagridview(dgvListOrder, 11, 40);

        }

        private void btnReason_Click(object sender, EventArgs e)
        {

            string idx = _idxOrder;
            DateTime _assyDate = dtpDate.Value;
            string reason = txtReason.Text.Trim();
            string action = txtAction.Text.Trim();

            string sql = "V2o1_BASE_Capacity_Work_Order_Reason_UpdItem_2o2_Addnew";

            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@idx";
            sParams[0].Value = idx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.NVarChar;
            sParams[1].ParameterName = "@reason";
            sParams[1].Value = reason;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.NVarChar;
            sParams[2].ParameterName = "@action";
            sParams[2].Value = action;

            cls.fnUpdDel(sql, sParams);

            fnBindSearchDate(_assyDate);
            fnFinish();

            MessageBox.Show("Update reason successful.");
        }

        private void TxtReason_TextChanged(object sender, EventArgs e)
        {
            txtAction.Enabled = (txtReason.Text != "" && txtReason != null) ? true : false;
            txtAction.Text = (txtReason.Text.Length > 0) ? txtAction.Text : "";
        }
    }
}
