using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmSub03MakeRequest : Form
    {
        public int _dgvMR_List_Width;
        public int _dgvMR_Request_Width;
        public int _dgvMR_OrderedList_Width;
        public int _dgvMR_OrderedDetail_Width;
        public MemoryStream _ms;

        public string _prodIDx = "", _prodName = "", _prodCode = "", _prodQty = "", _prodUnit = "";
        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;
        DataView view;

        public DateTime _dt;
        Timer timer = new Timer();
        public string _msgText;
        public int _msgType;

        public string _act = "add";
        public string _mrIDx;

        public string _range = "1";
        public string _rsbmIDx;


        public frmSub03MakeRequest()
        {
            InitializeComponent();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IDx";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Code";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Qty";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Unit";
            table.Columns.Add(column);

        }

        private void frmSub03MakeRequest_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            _dgvMR_List_Width = cls.fnGetDataGridWidth(dgvMR_List);
            _dgvMR_Request_Width = cls.fnGetDataGridWidth(dgvMR_Request);
            _dgvMR_OrderedList_Width = cls.fnGetDataGridWidth(dgvMR_OrderedList);
            _dgvMR_OrderedDetail_Width = cls.fnGetDataGridWidth(dgvMR_OrderedDetail);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
            initMR();
            initBinding();
            fnLinkColor();
        }

        public void fnGetdate()
        {
            if (check.IsConnectedToInternet())
            {
                tssDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                tssDateTime.ForeColor = Color.Black;
            }
            else
            {
                tssDateTime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt);
                tssDateTime.ForeColor = Color.Red;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initMR();
                    break;
                case 1:
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initBinding();
                    break;
                case 1:
                    break;
            }
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 5000;
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            if (tssMessage.Text.Length > 0)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            tssMessage.Text = "";
            timer.Stop();
        }



        #region BINDING DATA


        public void initBinding()
        {
            initBinding_Ordered();
        }

        public void initBinding_Ordered()
        {
            string sql = "V2o1_BASE_SubMaterial03_Init_Binding_OrderedList_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@rangeTime";
            sParams[0].Value = _range;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql,sParams);

            _dgvMR_OrderedList_Width = cls.fnGetDataGridWidth(dgvMR_OrderedList);
            dgvMR_OrderedList.DataSource = dt;

            //dgvMR_OrderedList.Columns[0].Width = 25 * _dgvMR_OrderedList_Width / 100;    // idx
            dgvMR_OrderedList.Columns[1].Width = 45 * _dgvMR_OrderedList_Width / 100;    // rsbmCode
            dgvMR_OrderedList.Columns[2].Width = 25 * _dgvMR_OrderedList_Width / 100;    // rsbmDate
            dgvMR_OrderedList.Columns[3].Width = 10 * _dgvMR_OrderedList_Width / 100;    // matQty
            dgvMR_OrderedList.Columns[4].Width = 20 * _dgvMR_OrderedList_Width / 100;    // rsbmReceiver
            //dgvMR_OrderedList.Columns[5].Width = 25 * _dgvMR_OrderedList_Width / 100;    // rsbmPurpose
            //dgvMR_OrderedList.Columns[6].Width = 25 * _dgvMR_OrderedList_Width / 100;    // rsbmPriority
            //dgvMR_OrderedList.Columns[7].Width = 25 * _dgvMR_OrderedList_Width / 100;    // rsbmReason
            //dgvMR_OrderedList.Columns[8].Width = 25 * _dgvMR_OrderedList_Width / 100;    // rsbmAdd
            //dgvMR_OrderedList.Columns[9].Width = 25 * _dgvMR_OrderedList_Width / 100;    // mmtApprove
            //dgvMR_OrderedList.Columns[10].Width = 25 * _dgvMR_OrderedList_Width / 100;    // mmtApproveDate
            //dgvMR_OrderedList.Columns[11].Width = 25 * _dgvMR_OrderedList_Width / 100;    // mmtResponse
            //dgvMR_OrderedList.Columns[12].Width = 25 * _dgvMR_OrderedList_Width / 100;    // mmtDateHandOver

            dgvMR_OrderedList.Columns[0].Visible = false;
            dgvMR_OrderedList.Columns[1].Visible = true;
            dgvMR_OrderedList.Columns[2].Visible = true;
            dgvMR_OrderedList.Columns[3].Visible = true;
            dgvMR_OrderedList.Columns[4].Visible = true;
            dgvMR_OrderedList.Columns[5].Visible = false;
            dgvMR_OrderedList.Columns[6].Visible = false;
            dgvMR_OrderedList.Columns[7].Visible = false;
            dgvMR_OrderedList.Columns[8].Visible = false;
            dgvMR_OrderedList.Columns[9].Visible = false;
            dgvMR_OrderedList.Columns[10].Visible = false;
            dgvMR_OrderedList.Columns[11].Visible = false;
            dgvMR_OrderedList.Columns[12].Visible = false;

            dgvMR_OrderedList.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgvMR_OrderedList, 12, 30);
            dgvMR_OrderedList.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            string priority = "";
            int _priority = 0;
            foreach (DataGridViewRow row in dgvMR_OrderedList.Rows)
            {
                priority = row.Cells[6].Value.ToString();
                _priority = (priority != "" && priority != null) ? Convert.ToInt32(priority) : 0;
                switch (_priority)
                {
                    case 1:
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        break;
                    case 2:
                        row.DefaultCellStyle.ForeColor = Color.Green;
                        break;
                    case 3:
                        row.DefaultCellStyle.ForeColor = Color.Blue;
                        break;
                    case 4:
                        row.DefaultCellStyle.ForeColor = Color.Chocolate;
                        break;
                }
            }
        }

        public void initBinding_Ordered_Detail()
        {
            string rsbmIDx = _rsbmIDx;
            string sql = "V2o1_BASE_SubMaterial03_Init_Binding_OrderedDetail_SelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "rsbmIDx";
            sParams[0].Value = rsbmIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvMR_OrderedDetail_Width = cls.fnGetDataGridWidth(dgvMR_OrderedDetail);
            dgvMR_OrderedDetail.DataSource = dt;

            //dgvMR_OrderedDetail.Columns[0].Width = 25 * _dgvMR_OrderedDetail_Width / 100;    // idx
            //dgvMR_OrderedDetail.Columns[1].Width = 25 * _dgvMR_OrderedDetail_Width / 100;    // matIDx
            dgvMR_OrderedDetail.Columns[2].Width = 45 * _dgvMR_OrderedDetail_Width / 100;    // matName
            dgvMR_OrderedDetail.Columns[3].Width = 30 * _dgvMR_OrderedDetail_Width / 100;    // matCode
            dgvMR_OrderedDetail.Columns[4].Width = 10 * _dgvMR_OrderedDetail_Width / 100;    // matUnit
            dgvMR_OrderedDetail.Columns[5].Width = 15 * _dgvMR_OrderedDetail_Width / 100;    // matQty

            dgvMR_OrderedDetail.Columns[0].Visible = false;
            dgvMR_OrderedDetail.Columns[1].Visible = false;
            dgvMR_OrderedDetail.Columns[2].Visible = true;
            dgvMR_OrderedDetail.Columns[3].Visible = true;
            dgvMR_OrderedDetail.Columns[4].Visible = true;
            dgvMR_OrderedDetail.Columns[5].Visible = true;

            cls.fnFormatDatagridview(dgvMR_OrderedDetail, 12, 30);
            dgvMR_OrderedDetail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void dgvMR_OrderedList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMR_OrderedList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvMR_OrderedList, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvMR_OrderedList.Rows[e.RowIndex];

                string idx = "", code = "", date = "", receiver = "", purpose = "", priority = "", reason = "";
                string mmtApproval = "", mmtResponse = "", mmtDateResponse = "", mmtHandoverDate = "";
                idx = row.Cells[0].Value.ToString();
                code = row.Cells[1].Value.ToString();
                date = row.Cells[2].Value.ToString();
                receiver = row.Cells[4].Value.ToString();
                purpose = row.Cells[5].Value.ToString();
                priority = row.Cells[6].Value.ToString();
                reason = row.Cells[7].Value.ToString();

                mmtApproval = row.Cells[9].Value.ToString();
                mmtDateResponse = row.Cells[10].Value.ToString();
                mmtResponse = row.Cells[11].Value.ToString();
                mmtHandoverDate = row.Cells[12].Value.ToString();

                _rsbmIDx = idx;

                lblMR_OrderedList_Code.Text = code;
                lblMR_OrderedList_Receiver.Text = "Ngày tạo: " + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(date));
                lblMR_OrderedList_Date.Text = "Người tạo: " + receiver;
                lblMR_OrderedDetail_Reason.Text = reason;

                switch (priority)
                {
                    case "1":
                        purpose = "Sửa chữa";
                        lblMR_OrderedDetail_Purpose.ForeColor = Color.Red;
                        break;
                    case "2":
                        purpose = "Bảo trì";
                        lblMR_OrderedDetail_Purpose.ForeColor = Color.Green;
                        break;
                    case "3":
                        purpose = "Cải tiến";
                        lblMR_OrderedDetail_Purpose.ForeColor = Color.Blue;
                        break;
                    case "4":
                        purpose = "Hàng ngày";
                        lblMR_OrderedDetail_Purpose.ForeColor = Color.Chocolate;
                        break;
                }

                lblMR_OrderedDetail_Purpose.Text = purpose.ToUpper();

                switch (mmtApproval)
                {
                    case "0":
                        // YEU CAU MOI VA DANG DUOC XEM XET
                        lblMR_OrderedDetail_MMT_Approval.ForeColor = Color.DodgerBlue;
                        lblMR_OrderedDetail_MMT_Approval.Text = "ĐANG XỬ LÝ...";
                        lblMR_OrderedDetail_MMT_Response.Text = (mmtResponse != "" && mmtResponse != null) ? mmtResponse : "Vui lòng chờ trong khi chúng tôi đang xử lý yêu cầu của bạn.";
                        lblMR_OrderedDetail_MMT_DateResponse.Text = "";
                        break;
                    case "1":
                        // TINH TRANG OK CO THE NHAN VAT TU
                        lblMR_OrderedDetail_MMT_Approval.ForeColor = Color.DeepSkyBlue;
                        lblMR_OrderedDetail_MMT_Approval.Text = "CHẤP NHẬN YÊU CẦU";
                        lblMR_OrderedDetail_MMT_Response.Text = (mmtResponse != "" && mmtResponse != null) ? mmtResponse : "Vui lòng liên hệ với phòng Vật tư để nhận hàng.";
                        lblMR_OrderedDetail_MMT_DateResponse.Text = "[" + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(mmtDateResponse)) + "]";
                        break;
                    case "2":
                        // LY DO KHONG THOA DANG NEN KHONG PHAT HANG
                        lblMR_OrderedDetail_MMT_Approval.ForeColor = Color.Tomato;
                        lblMR_OrderedDetail_MMT_Approval.Text = "YÊU CẦU BỊ TRẢ LẠI";
                        lblMR_OrderedDetail_MMT_Response.Text = (mmtResponse != "" && mmtResponse != null) ? mmtResponse : "Vui lòng tạo yêu cầu khác với lý do hợp lý hơn.";
                        lblMR_OrderedDetail_MMT_DateResponse.Text = "[" + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(mmtDateResponse)) + "]";
                        break;
                    case "3":
                        // VAT TU KHONG DU DE PHAT THEO YEU CAU
                        lblMR_OrderedDetail_MMT_Approval.ForeColor = Color.Tomato;
                        lblMR_OrderedDetail_MMT_Approval.Text = "YÊU CẦU BỊ TRẢ LẠI";
                        lblMR_OrderedDetail_MMT_Response.Text = (mmtResponse != "" && mmtResponse != null) ? mmtResponse : "Rất tiếc, vật tư hiện có không đủ cung cấp theo yêu cầu.";
                        lblMR_OrderedDetail_MMT_DateResponse.Text = "[" + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(mmtDateResponse)) + "]";
                        break;
                }

                initBinding_Ordered_Detail();

                panel1.Visible = true;
            }
        }

        private void dgvMR_OrderedList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void lnkMR_OrderedList_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "1";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "2";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_2weeks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "3";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_1month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "4";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_3months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "5";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_6months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "6";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_9months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "7";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        private void lnkMR_OrderedList_1year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "8";
            dgvMR_OrderedList.DataSource = "";
            dgvMR_OrderedList.Refresh();
            panel1.Visible = false;
            initBinding_Ordered();
            fnLinkColor();
        }

        public void fnLinkColor()
        {
            switch (_range)
            {
                case "1":
                    lnkMR_OrderedList_Today.LinkColor = Color.Red;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "2":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Red;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "3":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Red;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "4":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Red;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "5":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Red;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "6":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Red;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "7":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Red;
                    lnkMR_OrderedList_1year.LinkColor = Color.Blue;
                    break;
                case "8":
                    lnkMR_OrderedList_Today.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3Days.LinkColor = Color.Blue;
                    lnkMR_OrderedList_2weeks.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1month.LinkColor = Color.Blue;
                    lnkMR_OrderedList_3months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_6months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_9months.LinkColor = Color.Blue;
                    lnkMR_OrderedList_1year.LinkColor = Color.Red;
                    break;
            }
        }

        #endregion



        #region MAKE REQUEST


        public void initMR()
        {
            initMR_Filter();
            lblMR_Code.Text = "";
            initMR_List();
            initMR_Maker();

            lblMR_PartName.Text = "N/A";
            lblMR_PartCode.Text = "N/A";
            txtMR_PartQty.Text = "0";
        }

        public void initMR_Filter()
        {
            txtMR_Name.Text = "";
            btnMR_Filter.Enabled = true;
        }

        public string initMR_Code()
        {
            string codeMR = "", picIDx = cbbMR_Maker.SelectedValue.ToString();
            string currCodeMR = "", nextCodeMR = "";
            int _currCodeMR = 0, _nextCodeMR = 0;

            string sql = "V2o1_BASE_SubMaterial03_Init_MakeRequest_Code_SelItem_V1o2_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = picIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    currCodeMR = ds.Tables[0].Rows[0][0].ToString();
                    codeMR = cls.Right(currCodeMR, 4);
                    _currCodeMR = (codeMR != "" && codeMR != null) ? Convert.ToInt32(codeMR) : 0;
                    _nextCodeMR = _currCodeMR + 1;
                    nextCodeMR = "RSBM-00-" + String.Format("{0:yyyyMMdd}", _dt) + String.Format("{0:0000}", _nextCodeMR);
                }
                else
                {
                    nextCodeMR = "RSBM-00-" + String.Format("{0:yyyyMMdd}", _dt) + "0001";
                }
            }
            else
            {
                nextCodeMR = "RSBM-00-" + String.Format("{0:yyyyMMdd}", _dt) + "0001";
            }
            //try
            //{

            //}
            //catch
            //{

            //}
            //finally
            //{

            //}
            return nextCodeMR;
        }

        public void initMR_List()
        {
            string sql = "V2o1_BASE_SubMaterial03_Init_MakeRequest_List_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvMR_List_Width = cls.fnGetDataGridWidth(dgvMR_List);
            dgvMR_List.DataSource = dt;

            //dgvMR_List.Columns[0].Width = 25 * _dgvMR_List_Width / 100;    // ProdId
            dgvMR_List.Columns[1].Width = 65 * _dgvMR_List_Width / 100;    // Name
            //dgvMR_List.Columns[2].Width = 26 * _dgvMR_List_Width / 100;    // BarCode
            dgvMR_List.Columns[3].Width = 15 * _dgvMR_List_Width / 100;    // Uom
            //dgvMR_List.Columns[4].Width = 8 * _dgvMR_List_Width / 100;    // 1-Day Using
            //dgvMR_List.Columns[5].Width = 8 * _dgvMR_List_Width / 100;    // Safety Stock
            dgvMR_List.Columns[6].Width = 20 * _dgvMR_List_Width / 100;    // Total
            //dgvMR_List.Columns[7].Width = 15 * _dgvMR_List_Width / 100;    // IN_Date
            //dgvMR_List.Columns[8].Width = 15 * _dgvMR_List_Width / 100;    // ImageStatus
            //dgvMR_List.Columns[9].Width = 15 * _dgvMR_List_Width / 100;    // Picture

            dgvMR_List.Columns[0].Visible = false;
            dgvMR_List.Columns[1].Visible = true;
            dgvMR_List.Columns[2].Visible = false;
            dgvMR_List.Columns[3].Visible = true;
            dgvMR_List.Columns[4].Visible = false;
            dgvMR_List.Columns[5].Visible = false;
            dgvMR_List.Columns[6].Visible = true;
            dgvMR_List.Columns[7].Visible = false;
            dgvMR_List.Columns[8].Visible = false;
            //dgvMR_List.Columns[9].Visible = false;

            dgvMR_List.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgvMR_List, 12, 30);

            dgvMR_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        public void initMR_Maker()
        {
            string sql = "V2o1_BASE_SubMaterial03_Init_MakeRequest_Maker_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbMR_Maker.DataSource = dt;
            cbbMR_Maker.DisplayMember = "picName";
            cbbMR_Maker.ValueMember = "idx";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbMR_Maker.SelectedIndex = 0;

        }

        private void txtMR_Name_TextChanged(object sender, EventArgs e)
        {
            //string sFilter = txtMR_Name.Text.Trim();
            //btnMR_Filter.Enabled = (sFilter.Length > 0) ? true : false;
        }

        private void dgvMR_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMR_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvMR_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgvMR_List.Rows[e.RowIndex];

                string matIDx = row.Cells[0].Value.ToString();
                string matName = row.Cells[1].Value.ToString();
                string matCode = row.Cells[2].Value.ToString();
                string matUnit = row.Cells[3].Value.ToString();
                string mat1Day = row.Cells[4].Value.ToString();
                string matSafety = row.Cells[5].Value.ToString();
                string matInvent = row.Cells[6].Value.ToString();
                string matLastIn = row.Cells[7].Value.ToString();
                string imagestatus = row.Cells[8].Value.ToString().ToLower();

                _prodIDx = matIDx;
                _prodName = matName;
                _prodCode = matCode;
                _prodUnit = matUnit;
                _prodQty = matInvent;

                lblMR_PartName.Text = matName;
                lblMR_PartCode.Text = matCode;
                lblMR_PartUnit.Text = "S.L (" + matUnit + ")";

                //lnkMR_ViewImage.Enabled = (imagestatus == "true") ? true : false;
                if (imagestatus == "true")
                {
                    string sqlImage = "V2o1_BASE_SubMaterial_Init_BindingData_Image_SelItem_V1o1_Addnew";

                    SqlParameter[] sParamsImage = new SqlParameter[1]; // Parameter count
                    sParamsImage[0] = new SqlParameter();
                    sParamsImage[0].SqlDbType = SqlDbType.Int;
                    sParamsImage[0].ParameterName = "@matIDx";
                    sParamsImage[0].Value = matIDx;

                    DataSet ds = new DataSet();
                    ds = cls.ExecuteDataSet(sqlImage, sParamsImage);

                    _ms = new MemoryStream((byte[])ds.Tables[0].Rows[0][0]);
                    lnkMR_ViewImage.Enabled = true;
                }
                else
                {
                    _ms = null;
                    lnkMR_ViewImage.Enabled = false;
                }

                lblMR_Code.Enabled = true;
                cbbMR_Maker.Enabled = true;
            }
        }

        private void dgvMR_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cbbMR_Maker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string codeMR = "";
            if (cbbMR_Maker.SelectedIndex > 0)
            {
                codeMR = initMR_Code();
                string codeMaker = codeMR.Substring(4, 4);
                codeMR = codeMR.Replace(codeMaker, "-" + String.Format("{0:00}", Convert.ToInt32(cbbMR_Maker.SelectedValue)) + "-");

                txtMR_PartQty.Enabled = true;
                txtMR_PartQty.Focus();

                if(dgvMR_Request.Rows.Count>0
                    &&(rdbMR_Repair.Checked||rdbMR_Maintain.Checked||rdbMR_Improve.Checked || rdbMR_Daily.Checked)
                    && txtMR_Reason.Text.Length > 0)
                {
                    btnMR_Save.Enabled = true;
                }
                else
                {
                    btnMR_Save.Enabled = false;
                }
                btnMR_Done.Enabled = true;
            }
            else
            {
                codeMR = "";
                txtMR_PartQty.Text = "0";
                txtMR_PartQty.Enabled = false;

                btnMR_Save.Enabled = false;
                btnMR_Done.Enabled = false;
            }

            lblMR_Code.Text = codeMR;

            try
            {

            }
            catch
            {

            }
            finally
            {

            }

        }

        private void txtMR_PartQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMR_PartQty_TextChanged(object sender, EventArgs e)
        {
            string partQty = txtMR_PartQty.Text.Trim();
            int prodInvent = (_prodQty != "" && _prodQty != null) ? Convert.ToInt32(_prodQty) : 0;
            int _partQty = (partQty != "" && partQty != null) ? Convert.ToInt32(partQty) : 0;

            //if (_partQty <= prodInvent && _partQty > 0)
            //{
            //    lnkMR_Addnew.Enabled = true;
            //}
            //else
            //{
            //    lnkMR_Addnew.Enabled = false;
            //}

            if (prodInvent > 0 && prodInvent >= _partQty)
            {
                lnkMR_Addnew.Enabled = true;
            }
            else
            {
                lnkMR_Addnew.Enabled = false;
            }

            //if (partQty.Length > 0 && partQty != "0")
            //{
            //    _prodQty = partQty;
            //    lnkMR_Addnew.Enabled = true;
            //}
            //else
            //{
            //    _prodQty = "0";
            //    lnkMR_Addnew.Enabled = false;
            //}
        }

        private void dgvMR_Request_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMR_Request_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvMR_Request, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvMR_Request.Rows[e.RowIndex];

            string prodIDx = row.Cells[0].Value.ToString();
            string prodName = row.Cells[1].Value.ToString();
            string prodCode = row.Cells[2].Value.ToString();
            string prodQty = row.Cells[3].Value.ToString();
            string prodUnit = row.Cells[4].Value.ToString();

            lnkMR_Addnew.Enabled = false;
            lnkMR_Remove.Enabled = true;

        }

        private void dgvMR_Request_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void lnkMR_Addnew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int prodIDx = (_prodIDx != "" && _prodIDx != null) ? Convert.ToInt32(_prodIDx) : 0;
            string prodName = _prodName;
            string prodCode = _prodCode;
            //int prodQty = (_prodQty != "" && _prodQty != null) ? Convert.ToInt32(_prodQty) : 0;
            int prodQty = (txtMR_PartQty.Text != "" && txtMR_PartQty.Text != null) ? Convert.ToInt32(txtMR_PartQty.Text) : 0;
            string prodUnit = _prodUnit;
            if (prodIDx > 0 && prodQty > 0)
            {
                table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
                view = new DataView(table);
                _dgvMR_Request_Width = cls.fnGetDataGridWidth(dgvMR_Request);

                dgvMR_Request.DataSource = view;
                dgvMR_Request.Refresh();


                //dgvMR_Request.Columns[0].Width = 20 * _dgvMR_Request_Width / 100;    // matId
                dgvMR_Request.Columns[1].Width = 70 * _dgvMR_Request_Width / 100;    // matName
                //dgvMR_Request.Columns[2].Width = 20 * _dgvMR_Request_Width / 100;    // matCode
                dgvMR_Request.Columns[3].Width = 15 * _dgvMR_Request_Width / 100;    // matQty
                dgvMR_Request.Columns[4].Width = 15 * _dgvMR_Request_Width / 100;    // matUnit

                dgvMR_Request.Columns[0].Visible = false;
                dgvMR_Request.Columns[1].Visible = true;
                dgvMR_Request.Columns[2].Visible = false;
                dgvMR_Request.Columns[3].Visible = true;
                dgvMR_Request.Columns[4].Visible = true;

                cls.fnFormatDatagridview(dgvMR_Request, 10, 30);
                dgvMR_Request.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            else
            {
                MessageBox.Show("Không thể thêm khi số lượng bằng 0");
            }

            dgvMR_List.ClearSelection();
            _prodIDx = "";
            _prodName = "";
            _prodCode = "";
            _prodQty = "";
            _prodUnit = "";

            lblMR_PartName.Text = "N/A";
            lblMR_PartCode.Text = "N/A";
            txtMR_PartQty.Text = "0";
            //txtMR_PartQty.Enabled = false;
            lnkMR_Addnew.Enabled = false;
            lnkMR_Remove.Enabled = false;

            if (dgvMR_Request.Rows.Count > 0)
            {
                rdbMR_Repair.Enabled = true;
                rdbMR_Maintain.Enabled = true;
                rdbMR_Improve.Enabled = true;
                rdbMR_Daily.Enabled = true;
            }
            else
            {
                rdbMR_Repair.Enabled = false;
                rdbMR_Maintain.Enabled = false;
                rdbMR_Improve.Enabled = false;
                rdbMR_Daily.Enabled = false;
            }
            rdbMR_Repair.Checked = false;
            rdbMR_Maintain.Checked = false;
            rdbMR_Improve.Checked = false;
            rdbMR_Daily.Checked = false;

            txtMR_Reason.Enabled = (txtMR_Reason.Text.Length > 0) ? true : false;

            btnMR_Save.Enabled = false;
            //btnMR_Done.Enabled = false;
        }

        private void lnkMR_Remove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvMR_Request.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgvMR_Request.Rows.Remove(row);
                }

                dgvMR_List.ClearSelection();
                _prodIDx = "";
                _prodName = "";
                _prodCode = "";
                _prodQty = "";
                _prodUnit = "";

                lblMR_PartName.Text = "N/A";
                lblMR_PartCode.Text = "N/A";
                txtMR_PartQty.Text = "0";
                //txtMR_PartQty.Enabled = false;
                lnkMR_Addnew.Enabled = false;
                lnkMR_Remove.Enabled = false;
                btnMR_Save.Enabled = false;
                //btnMR_Done.Enabled = false;

                if (dgvMR_Request.Rows.Count > 0)
                {
                    rdbMR_Repair.Enabled = true;
                    rdbMR_Maintain.Enabled = true;
                    rdbMR_Improve.Enabled = true;
                    rdbMR_Daily.Enabled = true;
                }
                else
                {
                    rdbMR_Repair.Enabled = false;
                    rdbMR_Maintain.Enabled = false;
                    rdbMR_Improve.Enabled = false;
                    rdbMR_Daily.Enabled = false;
                }
                rdbMR_Repair.Checked = false;
                rdbMR_Maintain.Checked = false;
                rdbMR_Improve.Checked = false;
                rdbMR_Daily.Checked = true;

                if (dgvMR_Request.Rows.Count == 0)
                {
                    lblMR_Code.Enabled = false;
                    cbbMR_Maker.SelectedIndex = 0;
                    cbbMR_Maker.Enabled = false;
                    //lblMR_PartName.Text = "N/A";
                    //lblMR_PartCode.Text = "N/A";
                    //txtMR_PartQty.Text = "0";
                    txtMR_PartQty.Enabled = false;
                    lnkMR_Addnew.Enabled = false;
                    lnkMR_Remove.Enabled = false;
                    txtMR_Reason.Text = "";
                    txtMR_Reason.Enabled = false;
                    btnMR_Done.Enabled = false;
                }
                //cbbHandOver_Name.SelectedIndex = 0;
                //lblHandOver_Code.Text = "N/A";
                //txtHandOver_Qty.Text = "0";
                //dgvHandOver_List.ClearSelection();
                //lnkHandOver_Add.Enabled = false;
                //lnkHandOver_Del.Enabled = false;
                //btnHandOver_Save.Enabled = (dgvHandOver_List.Rows.Count > 0) ? true : false;
                //btnHandOver_Finish.Enabled = (dgvHandOver_List.Rows.Count > 0) ? true : false;

                _msgText = "Xóa vật tư khỏi danh sách yêu cầu thành công.";
                _msgType = 0;

            }
            dgvMR_Request.ClearSelection();
        }

        private void rdbMR_Improve_CheckedChanged(object sender, EventArgs e)
        {
            txtMR_Reason.Enabled = true;
            txtMR_Reason.Text = "";
            txtMR_Reason.Focus();
        }

        private void rdbMR_Repair_CheckedChanged(object sender, EventArgs e)
        {
            txtMR_Reason.Enabled = true;
            txtMR_Reason.Text = "";
            txtMR_Reason.Focus();
        }

        private void rdbMR_Maintain_CheckedChanged(object sender, EventArgs e)
        {
            txtMR_Reason.Enabled = true;
            txtMR_Reason.Text = "";
            txtMR_Reason.Focus();
        }

        private void rdbMR_Daily_CheckedChanged(object sender, EventArgs e)
        {
            txtMR_Reason.Enabled = true;
            txtMR_Reason.Text = "";
            txtMR_Reason.Focus();
        }

        private void txtMR_Reason_TextChanged(object sender, EventArgs e)
        {
            string reason = txtMR_Reason.Text.Trim();
            if (reason.Length > 0)
            {
                int _reason = Convert.ToInt32(reason.Length);
                lblMR_ReasonCount.Text = "(" + (Convert.ToInt32(2000) - _reason) + ")";
                btnMR_Save.Enabled = true;
            }
            else
            {
                lblMR_ReasonCount.Text = "(2000)";
                btnMR_Save.Enabled = false;
            }
        }

        private void btnMR_Done_Click(object sender, EventArgs e)
        {
            fnMR_Done();
        }

        public void fnMR_Done()
        {
            dgvMR_List.ClearSelection();
            _prodIDx = "";
            _prodName = "";
            _prodCode = "";
            _prodQty = "";
            _prodUnit = "";

            lblMR_Code.Text = "";
            cbbMR_Maker.SelectedIndex = 0;
            cbbMR_Maker.Enabled = false;
            lblMR_PartName.Text = "N/A";
            lblMR_PartCode.Text = "N/A";
            txtMR_PartQty.Text = "0";
            txtMR_PartQty.Enabled = false;
            dgvMR_Request.DataSource = "";
            dgvMR_Request.Refresh();
            lnkMR_Addnew.Enabled = false;
            lnkMR_Remove.Enabled = false;
            rdbMR_Repair.Checked = false;
            rdbMR_Repair.Enabled = false;
            rdbMR_Maintain.Checked = false;
            rdbMR_Maintain.Enabled = false;
            rdbMR_Improve.Checked = false;
            rdbMR_Improve.Enabled = false;
            rdbMR_Daily.Checked = false;
            rdbMR_Daily.Enabled = false;
            txtMR_Reason.Text = "";
            txtMR_Reason.Enabled = false;
            btnMR_Save.Enabled = false;
            btnMR_Done.Enabled = false;
        }

        private void btnMR_Filter_Click(object sender, EventArgs e)
        {
            try
            {
                //BindingSource bd = (BindingSource)dgvOrder_List.DataSource;
                //DataTable dt = (DataTable)bd.DataSource;
                //dt.DefaultView.RowFilter = string.Format("boxcode like '%{0}%'", txtOrder_Filter.Text.Trim().Replace("'", "''"));
                //dgvOrder_List.Refresh();

                BindingSource bs = new BindingSource();
                bs.DataSource = dgvMR_List.DataSource;
                bs.Filter = string.Format("CONVERT([" + dgvMR_List.Columns[1].DataPropertyName + "], System.String) like '%" + txtMR_Name.Text.Replace("'", "''") + "%'");
                dgvMR_List.DataSource = bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        private void btnMR_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                switch (_act)
                {
                    case "add":
                        fnMR_Add();
                        break;
                    case "upd":
                        fnMR_Upd();
                        break;
                    case "del":
                        fnMR_Del();
                        break;
                }

                initBinding_Ordered();
                fnMR_Done();

                cls.fnMessage(tssMessage, _msgText, _msgType);
                _act = "add";
            }
        }

        public void fnMR_Add()
        {
            if (dgvMR_Request.Rows.Count > 0)
            {
                string mrCode = lblMR_Code.Text;
                string mrMakerIDx = cbbMR_Maker.SelectedValue.ToString();
                string mrMaker = cbbMR_Maker.Text;
                string mrPurpose = (rdbMR_Repair.Checked) ? "Repair" : (rdbMR_Maintain.Checked) ? "Maintain" : (rdbMR_Improve.Checked) ? "Improve" : "Daily";
                string mrReason = txtMR_Reason.Text.Trim();
                DateTime mrDate = _dt.AddMilliseconds(-_dt.Millisecond);
                string mrPartIDx = "", mrPartName = "", mrPartCode = "", mrPartQty = "", mrPartUnit = "";

                string sqlRequest = "V2o1_BASE_SubMaterial03_Init_MakeRequest_Request_AddItem_Addnew";

                SqlParameter[] sParamsRequest = new SqlParameter[6]; // Parameter count

                sParamsRequest[0] = new SqlParameter();
                sParamsRequest[0].SqlDbType = SqlDbType.VarChar;
                sParamsRequest[0].ParameterName = "@mrCode";
                sParamsRequest[0].Value = mrCode;

                sParamsRequest[1] = new SqlParameter();
                sParamsRequest[1].SqlDbType = SqlDbType.DateTime;
                sParamsRequest[1].ParameterName = "@mrDate";
                sParamsRequest[1].Value = mrDate;

                sParamsRequest[2] = new SqlParameter();
                sParamsRequest[2].SqlDbType = SqlDbType.Int;
                sParamsRequest[2].ParameterName = "@mrMakerIDx";
                sParamsRequest[2].Value = mrMakerIDx;

                sParamsRequest[3] = new SqlParameter();
                sParamsRequest[3].SqlDbType = SqlDbType.NVarChar;
                sParamsRequest[3].ParameterName = "@mrMaker";
                sParamsRequest[3].Value = mrMaker;

                sParamsRequest[4] = new SqlParameter();
                sParamsRequest[4].SqlDbType = SqlDbType.NVarChar;
                sParamsRequest[4].ParameterName = "@mrPurpose";
                sParamsRequest[4].Value = mrPurpose;

                sParamsRequest[5] = new SqlParameter();
                sParamsRequest[5].SqlDbType = SqlDbType.NVarChar;
                sParamsRequest[5].ParameterName = "@mrReason";
                sParamsRequest[5].Value = mrReason;

                cls.fnUpdDel(sqlRequest, sParamsRequest);

                foreach (DataGridViewRow row in dgvMR_Request.Rows)
                {
                    mrPartIDx = row.Cells[0].Value.ToString();
                    mrPartName = row.Cells[1].Value.ToString();
                    mrPartCode = row.Cells[2].Value.ToString();
                    mrPartQty = row.Cells[3].Value.ToString();
                    mrPartUnit = row.Cells[4].Value.ToString();

                    string sqlList = "V2o1_BASE_SubMaterial03_Init_MakeRequest_RequestList_AddItem_Addnew";

                    SqlParameter[] sParamsList = new SqlParameter[7]; // Parameter count

                    sParamsList[0] = new SqlParameter();
                    sParamsList[0].SqlDbType = SqlDbType.VarChar;
                    sParamsList[0].ParameterName = "@mrCode";
                    sParamsList[0].Value = mrCode;

                    sParamsList[1] = new SqlParameter();
                    sParamsList[1].SqlDbType = SqlDbType.DateTime;
                    sParamsList[1].ParameterName = "@mrDate";
                    sParamsList[1].Value = mrDate;

                    sParamsList[2] = new SqlParameter();
                    sParamsList[2].SqlDbType = SqlDbType.Int;
                    sParamsList[2].ParameterName = "@mrPartIdx";
                    sParamsList[2].Value = mrPartIDx;

                    sParamsList[3] = new SqlParameter();
                    sParamsList[3].SqlDbType = SqlDbType.NVarChar;
                    sParamsList[3].ParameterName = "@mrPartName";
                    sParamsList[3].Value = mrPartName;

                    sParamsList[4] = new SqlParameter();
                    sParamsList[4].SqlDbType = SqlDbType.VarChar;
                    sParamsList[4].ParameterName = "@mrPartCode";
                    sParamsList[4].Value = mrPartCode;

                    sParamsList[5] = new SqlParameter();
                    sParamsList[5].SqlDbType = SqlDbType.Int;
                    sParamsList[5].ParameterName = "@mrPartQty";
                    sParamsList[5].Value = mrPartQty;

                    sParamsList[6] = new SqlParameter();
                    sParamsList[6].SqlDbType = SqlDbType.VarChar;
                    sParamsList[6].ParameterName = "@mrPartUnit";
                    sParamsList[6].Value = mrPartUnit;

                    cls.fnUpdDel(sqlList, sParamsList);
                }

                _prodIDx = "";
                _prodName = "";
                _prodCode = "";
                _prodQty = "";
                _prodUnit = "";
                dgvMR_Request.DataSource = "";
                table.Rows.Clear();

                _msgText = "Tạo yêu cầu vật tư thành công. Vui lòng đợi phản hồi từ phòng Vật tư.";
                _msgType = 1;
            }
            else
            {
                _msgText = "Không thể tạo yêu cầu khi không có danh sách vật tư trống. Vui lòng thử lại";
                _msgType = 2;
            }
        }

        public void fnMR_Upd()
        {

        }

        public void fnMR_Del()
        {

        }

        private void lblMR_ViewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSub03MakeRequest_ViewImage frm03 = new frmSub03MakeRequest_ViewImage(_ms);
            frm03.ShowDialog();
        }


        #endregion
    }
}
