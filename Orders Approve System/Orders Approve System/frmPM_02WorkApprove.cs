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
    public partial class frmPM_02WorkApprove : Form
    {
        public int _dgv_Approve_Repair_Width;
        public int _row_Approve_Repair_Index = 0;

        int _dgv_ES_List_Width;

        public bool _login = false;
        public int _logIDx = 0;
        public int _logLevel = 0;
        public string _logName = "";

        public string _msgText;
        public int _msgType;

        public string _matImage = "";
        public bool _matNewImage;

        public int _rangeTime = 3;
        public string _woIDx = "", _manApprove = "";
        public string _picIDx = "",_picName = "",_picLevel = "";
        public string _equipIDx = "",_equipName = "",_machineIDx = "",_machineName = "",_machineDesc = "";
        public string _priority = "",_dateFinish = "",_orderNote = "",_added = "";
        public string _makerIDx = "", _makerName = "", _makerApprove = "", _makerApproveDate = "";
        public string _managerIDx = "",_managerName = "",_managerApprove = "",_managerApproveNote = "",_managerApproveDate = "";
        public string _repairIDx = "",_repairName = "",_repairApprove = "",_repairApproveNote = "",_repairApproveDate = "";
        public string _confirmIDx = "",_conFirmName = "",_confirmApprove = "",_confirmApproveNote = "",_confirmApproveDate = "";

        int _regIDx;

        public DateTime _dt;

        Timer timer = new Timer();

        public frmPM_02WorkApprove(int logIDx, int logLevel, string logName)
        {
            InitializeComponent();

            if (logIDx > 0 && logLevel > 0 && logName != "")
            {
                _login = true;
                _logIDx = logIDx;
                _logLevel = logLevel;
                _logName = logName;
            }
            else
            {
                this.Close();
                frmPM_00LoginSystem frmLogin = new frmPM_00LoginSystem(2);
                frmLogin.ShowDialog();
            }
        }

        private void frmPM_02WorkApprove_Load(object sender, EventArgs e)
        {
            _dgv_Approve_Repair_Width = cls.fnGetDataGridWidth(dgv_Approve_Repair);
            _dgv_ES_List_Width = cls.fnGetDataGridWidth(dgv_ES_List);

            _dt = DateTime.Now;
            if (_logIDx > 0 && _logName.Length > 0)
            {
                tssLogName.Text = "Logged in: " + _logName.ToUpper();
            }
            else
            {
                tssLogName.Text = "";
            }
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            init_Approve_Orders();
            init_Approve_Choice();
            init_Approve_Auto();
            init_Approve_Orders_Done();
            fnLinkColor();
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    init_Approve_Orders();
                    init_Approve_Choice();
                    init_Approve_Auto();
                    init_Approve_Orders_Done();
                    fnLinkColor();
                    break;
                case 1:
                    init_Entrance_Register();
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private void frmPM_02WorkApprove_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }


        #region APPROVE REPAIR ORDERS


        public void init_Approve_Orders()
        {
            string sql = "PMMS_02_View_Repair_Work_Order_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = _logIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@rangeTime";
            sParams[1].Value = _rangeTime;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Approve_Repair_Width = cls.fnGetDataGridWidth(dgv_Approve_Repair);
            dgv_Approve_Repair.DataSource = dt;

            dgv_Approve_Repair.Columns[0].Width = 5 * _dgv_Approve_Repair_Width / 100;    // STT
            //dgv_Approve_Repair.Columns[1].Width = 10 * _dgv_Approve_Repair_Width / 100;    // idx
            //dgv_Approve_Repair.Columns[2].Width = 10 * _dgv_Approve_Repair_Width / 100;    // picIDx
            dgv_Approve_Repair.Columns[3].Width = 13 * _dgv_Approve_Repair_Width / 100;    // picName
            //dgv_Approve_Repair.Columns[4].Width = 10 * _dgv_Approve_Repair_Width / 100;    // picLevel
            //dgv_Approve_Repair.Columns[5].Width = 10 * _dgv_Approve_Repair_Width / 100;    // equipmentIDx
            dgv_Approve_Repair.Columns[6].Width = 12 * _dgv_Approve_Repair_Width / 100;    // equipmentName
            //dgv_Approve_Repair.Columns[7].Width = 10 * _dgv_Approve_Repair_Width / 100;    // machineIDx
            dgv_Approve_Repair.Columns[8].Width = 10 * _dgv_Approve_Repair_Width / 100;    // machineName
            dgv_Approve_Repair.Columns[9].Width = 16 * _dgv_Approve_Repair_Width / 100;    // machineDesc
            dgv_Approve_Repair.Columns[10].Width = 7 * _dgv_Approve_Repair_Width / 100;    // orderPriority
            dgv_Approve_Repair.Columns[11].Width = 10 * _dgv_Approve_Repair_Width / 100;    // dateFinish
            //dgv_Approve_Repair.Columns[12].Width = 10 * _dgv_Approve_Repair_Width / 100;    // orderNote
            dgv_Approve_Repair.Columns[13].Width = 12 * _dgv_Approve_Repair_Width / 100;    // added
            //dgv_Approve_Repair.Columns[14].Width = 10 * _dgv_Approve_Repair_Width / 100;    // makerIDx
            //dgv_Approve_Repair.Columns[15].Width = 10 * _dgv_Approve_Repair_Width / 100;    // makerName
            //dgv_Approve_Repair.Columns[16].Width = 10 * _dgv_Approve_Repair_Width / 100;    // makerApprove
            //dgv_Approve_Repair.Columns[17].Width = 10 * _dgv_Approve_Repair_Width / 100;    // makerApproveDate
            //dgv_Approve_Repair.Columns[18].Width = 10 * _dgv_Approve_Repair_Width / 100;    // managerIDx
            //dgv_Approve_Repair.Columns[19].Width = 10 * _dgv_Approve_Repair_Width / 100;    // managerName
            //dgv_Approve_Repair.Columns[20].Width = 10 * _dgv_Approve_Repair_Width / 100;    // managerApprove
            //dgv_Approve_Repair.Columns[21].Width = 10 * _dgv_Approve_Repair_Width / 100;    // managerApproveNote
            //dgv_Approve_Repair.Columns[22].Width = 10 * _dgv_Approve_Repair_Width / 100;    // managerApproveDate
            //dgv_Approve_Repair.Columns[23].Width = 10 * _dgv_Approve_Repair_Width / 100;    // pmIDx
            //dgv_Approve_Repair.Columns[24].Width = 10 * _dgv_Approve_Repair_Width / 100;    // pmName
            //dgv_Approve_Repair.Columns[25].Width = 10 * _dgv_Approve_Repair_Width / 100;    // pmFinish
            //dgv_Approve_Repair.Columns[26].Width = 10 * _dgv_Approve_Repair_Width / 100;    // pmFinishNote
            //dgv_Approve_Repair.Columns[27].Width = 10 * _dgv_Approve_Repair_Width / 100;    // pmFinishDate
            //dgv_Approve_Repair.Columns[28].Width = 10 * _dgv_Approve_Repair_Width / 100;    // confirmIDx
            //dgv_Approve_Repair.Columns[29].Width = 10 * _dgv_Approve_Repair_Width / 100;    // confirmName
            //dgv_Approve_Repair.Columns[30].Width = 10 * _dgv_Approve_Repair_Width / 100;    // confirmClosed
            //dgv_Approve_Repair.Columns[31].Width = 10 * _dgv_Approve_Repair_Width / 100;    // confirmClosedNote
            //dgv_Approve_Repair.Columns[32].Width = 10 * _dgv_Approve_Repair_Width / 100;    // confirmClosedDate
            dgv_Approve_Repair.Columns[33].Width = 3 * _dgv_Approve_Repair_Width / 100;    // makerStatus
            dgv_Approve_Repair.Columns[34].Width = 3 * _dgv_Approve_Repair_Width / 100;    // managerStatus
            dgv_Approve_Repair.Columns[35].Width = 3 * _dgv_Approve_Repair_Width / 100;    // repairStatus
            dgv_Approve_Repair.Columns[36].Width = 3 * _dgv_Approve_Repair_Width / 100;    // confirmStatus
            dgv_Approve_Repair.Columns[37].Width = 3 * _dgv_Approve_Repair_Width / 100;    // orderClosed (blank)
            dgv_Approve_Repair.Columns[38].Width = 3 * _dgv_Approve_Repair_Width / 100;    // orderClosed (value)
            dgv_Approve_Repair.Columns[39].Width = 3 * _dgv_Approve_Repair_Width / 100;    // closedDate
            dgv_Approve_Repair.Columns[40].Width = 3 * _dgv_Approve_Repair_Width / 100;    // takeTime

            dgv_Approve_Repair.Columns[0].Visible = true;
            dgv_Approve_Repair.Columns[1].Visible = false;
            dgv_Approve_Repair.Columns[2].Visible = false;
            dgv_Approve_Repair.Columns[3].Visible = true;
            dgv_Approve_Repair.Columns[4].Visible = false;
            dgv_Approve_Repair.Columns[5].Visible = false;
            dgv_Approve_Repair.Columns[6].Visible = true;
            dgv_Approve_Repair.Columns[7].Visible = false;
            dgv_Approve_Repair.Columns[8].Visible = true;
            dgv_Approve_Repair.Columns[9].Visible = true;
            dgv_Approve_Repair.Columns[10].Visible = true;
            dgv_Approve_Repair.Columns[11].Visible = true;
            dgv_Approve_Repair.Columns[12].Visible = false;
            dgv_Approve_Repair.Columns[13].Visible = true;
            dgv_Approve_Repair.Columns[14].Visible = false;
            dgv_Approve_Repair.Columns[15].Visible = false;
            dgv_Approve_Repair.Columns[16].Visible = false;
            dgv_Approve_Repair.Columns[17].Visible = false;
            dgv_Approve_Repair.Columns[18].Visible = false;
            dgv_Approve_Repair.Columns[19].Visible = false;
            dgv_Approve_Repair.Columns[20].Visible = false;
            dgv_Approve_Repair.Columns[21].Visible = false;
            dgv_Approve_Repair.Columns[22].Visible = false;
            dgv_Approve_Repair.Columns[23].Visible = false;
            dgv_Approve_Repair.Columns[24].Visible = false;
            dgv_Approve_Repair.Columns[25].Visible = false;
            dgv_Approve_Repair.Columns[26].Visible = false;
            dgv_Approve_Repair.Columns[27].Visible = false;
            dgv_Approve_Repair.Columns[28].Visible = false;
            dgv_Approve_Repair.Columns[29].Visible = false;
            dgv_Approve_Repair.Columns[30].Visible = false;
            dgv_Approve_Repair.Columns[31].Visible = false;
            dgv_Approve_Repair.Columns[32].Visible = false;
            dgv_Approve_Repair.Columns[33].Visible = true;
            dgv_Approve_Repair.Columns[34].Visible = true;
            dgv_Approve_Repair.Columns[35].Visible = true;
            dgv_Approve_Repair.Columns[36].Visible = true;
            dgv_Approve_Repair.Columns[37].Visible = true;
            dgv_Approve_Repair.Columns[38].Visible = false;
            dgv_Approve_Repair.Columns[39].Visible = false;
            dgv_Approve_Repair.Columns[40].Visible = false;

            dgv_Approve_Repair.Columns[11].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv_Approve_Repair.Columns[13].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            dgv_Approve_Repair.Columns[33].HeaderText = "M";
            dgv_Approve_Repair.Columns[34].HeaderText = "A";
            dgv_Approve_Repair.Columns[35].HeaderText = "R";
            dgv_Approve_Repair.Columns[36].HeaderText = "C";
            dgv_Approve_Repair.Columns[37].HeaderText = "T";

            cls.fnFormatDatagridview(dgv_Approve_Repair, 11, 30);


            string makerName = "", makerApprove = "", makerApproveDate = "", orderClosed = "";
            string managerName = "", managerApprove = "", managerApproveNote = "", managerApproveDate = "";
            string repairName = "", repairApprove = "", repairApproveNote = "", repairApproveDate = "";
            string confirmName = "", confirmApprove = "", confirmApproveNote = "", confirmApproveDate = "";

            int _orderClosed = 0, _makerApprove = 0, _managerApprove = 0, _repairApprove = 0, _confirmApprove = 0;

            foreach (DataGridViewRow row in dgv_Approve_Repair.Rows)
            {
                makerName = row.Cells[15].Value.ToString();
                makerApprove = row.Cells[16].Value.ToString();
                makerApproveDate = row.Cells[17].Value.ToString();
                _makerApprove = (makerApprove != "" && makerApprove != null) ? Convert.ToInt32(makerApprove) : 0;

                managerName = row.Cells[19].Value.ToString();
                managerApprove = row.Cells[20].Value.ToString();
                managerApproveNote = row.Cells[21].Value.ToString();
                managerApproveDate = row.Cells[22].Value.ToString();
                _managerApprove = (managerApprove != "" && managerApprove != null) ? Convert.ToInt32(managerApprove) : 0;

                repairName = row.Cells[24].Value.ToString();
                repairApprove = row.Cells[25].Value.ToString();
                repairApproveNote = row.Cells[26].Value.ToString();
                repairApproveDate = row.Cells[27].Value.ToString();
                _repairApprove = (repairApprove != "" && repairApprove != null) ? Convert.ToInt32(repairApprove) : 0;

                confirmName = row.Cells[29].Value.ToString();
                confirmApprove = row.Cells[30].Value.ToString();
                confirmApproveNote = row.Cells[31].Value.ToString();
                confirmApproveDate = row.Cells[32].Value.ToString();
                _confirmApprove = (confirmApprove != "" && confirmApprove != null) ? Convert.ToInt32(confirmApprove) : 0;

                if (_makerApprove == 1 && _managerApprove == 1 && _repairApprove == 1 && _confirmApprove == 1)
                {
                    // APPROVED - FINISH
                    _orderClosed = 1;
                }
                else if (_makerApprove == 2 || _managerApprove == 2 || _repairApprove == 2 || _confirmApprove == 2)
                {
                    // REJECT
                    _orderClosed = 2;
                }
                else
                {
                    // DOING or NEARLY FINISH
                    orderClosed = row.Cells[38].Value.ToString();
                    _orderClosed = (orderClosed != "" && orderClosed != null) ? Convert.ToInt32(orderClosed) : 0;
                }

                //_orderClosed = (_makerApprove == 1 && _managerApprove == 1 && _repairApprove == 1 && _confirmApprove == 1) ? 1 : 2;

                row.Cells[33].Style.BackColor = (_makerApprove == 1) ? Color.LightGreen : Color.Gray;
                switch (_managerApprove)
                {
                    case 0:
                        // NEW
                        row.Cells[34].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[34].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // DOING
                        row.Cells[34].Style.BackColor = Color.OrangeRed;
                        break;
                    case 3:
                        // PENDING
                        row.Cells[34].Style.BackColor = Color.LemonChiffon;
                        break;
                }

                switch (_repairApprove)
                {
                    case 0:
                        // NEW
                        row.Cells[35].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[35].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // REJECT
                        row.Cells[35].Style.BackColor = Color.Red;
                        break;
                    case 3:
                        // NOT ASSIGN
                        row.Cells[35].Style.BackColor = Color.LemonChiffon;
                        break;
                    case 4:
                        // DOING
                        row.Cells[35].Style.BackColor = Color.Yellow;
                        break;
                    case 5:
                        // PENDING
                        row.Cells[35].Style.BackColor = Color.Orange;
                        break;
                }

                switch (_confirmApprove)
                {
                    case 0:
                        // NEW
                        row.Cells[36].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[36].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // REJECT
                        row.Cells[36].Style.BackColor = Color.Red;
                        break;
                    case 3:
                        // DONG
                        row.Cells[36].Style.BackColor = Color.YellowGreen;
                        break;
                }

                switch (_orderClosed)
                {
                    case 0:
                        // NEW
                        row.Cells[37].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[37].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // DOING
                        row.Cells[37].Style.BackColor = Color.Red;
                        break;
                    case 3:
                        // PENDING
                        row.Cells[37].Style.BackColor = Color.Yellow;
                        break;
                    case 4:
                        // PENDING
                        row.Cells[37].Style.BackColor = Color.OliveDrab;
                        break;
                }


            }
        }

        public void init_Approve_Choice()
        {
            cbb_Approve_Choice.Items.Clear();
            cbb_Approve_Choice.Items.Add("Đồng ý / Approved");
            cbb_Approve_Choice.Items.Add("Từ chối / Rejected");
            cbb_Approve_Choice.Items.Insert(0, "");
            cbb_Approve_Choice.SelectedIndex = 0;
        }

        public void init_Approve_Auto()
        {
            string logIDx = _logIDx.ToString();
            string sql = "PMMS_02_Select_Repair_Work_Order_Auto_Approve_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = logIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string auto = ds.Tables[0].Rows[0][0].ToString().ToLower();
                    chk_Approve_Auto.Checked = (auto == "true") ? true : false;
                }
            }
        }

        public void init_Approve_Orders_Done()
        {
            _woIDx = ""; _manApprove = "";
            _picIDx = ""; _picName = ""; _picLevel = "";
            _equipIDx = ""; _equipName = ""; _machineIDx = ""; _machineName = ""; _machineDesc = "";
            _priority = ""; _dateFinish = ""; _orderNote = ""; _added = "";
            _makerIDx = ""; _makerName = ""; _makerApprove = ""; _makerApproveDate = "";
            _managerIDx = ""; _managerName = ""; _managerApprove = ""; _managerApproveNote = ""; _managerApproveDate = "";
            _repairIDx = ""; _repairName = ""; _repairApprove = ""; _repairApproveNote = ""; _repairApproveDate = "";
            _confirmIDx = ""; _conFirmName = ""; _confirmApprove = ""; _confirmApproveNote = ""; _confirmApproveDate = "";

            dgv_Approve_Repair.ClearSelection();

            lbl_Approve_Maker_Name.Text = "";
            lbl_Approve_Maker_Date.Text = "";
            lbl_Approve_Equipment.Text = "";
            lbl_Approve_Machine.Text = "";
            lbl_Approve_Desc_2.Text = "";
            lbl_Approve_Date_Finish.Text = "";
            lbl_Approve_Priority.Text = "";
            lbl_Approve_Note_2.Text = "";

            lbl_Approve_Issue_Date.Text = "";
            lbl_Approve_Solve_Date.Text = "";
            pnl_Approve_Issue.BackgroundImage = null;
            pnl_Approve_Solve.BackgroundImage = null;

            cbb_Approve_Choice.SelectedIndex = 0;
            cbb_Approve_Choice.Enabled = false;
            txt_Approve_Reason.Text = "";
            txt_Approve_Reason.Enabled = false;

            btn_Approve_Save.Enabled = false;
            btn_Approve_Done.Enabled = false;
        }

        private void lnk_Order_Repair_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 1;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 2;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Week_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 3;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_10Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 4;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 5;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_2Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 6;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_3Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 7;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_6Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 8;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_9Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 9;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 10;
            dgv_Approve_Repair.DataSource = "";
            init_Approve_Orders();
            fnLinkColor();
        }

        private void cbb_Approve_Choice_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Approve_Choice.SelectedIndex > 0)
            {
                txt_Approve_Reason.Enabled = true;

                if (cbb_Approve_Choice.SelectedIndex == 1)
                {
                    txt_Approve_Reason.Text = "Đồng ý phê duyệt theo yêu cầu này";
                }
                else
                {
                    txt_Approve_Reason.Text = "Từ chối yêu cầu này với lý do sau: ";
                    txt_Approve_Reason.Focus();
                }

                btn_Approve_Save.Enabled = true;
            }
            else
            {
                txt_Approve_Reason.Text = "";
                txt_Approve_Reason.Enabled = false;

                btn_Approve_Save.Enabled = false;
            }
        }

        private void dgv_Approve_Repair_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Approve_Repair_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //cls.fnDatagridClickCell(dgv_Order_Repair, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Approve_Repair.Rows[e.RowIndex];
                for (int i = 0; i <= 31; i++)
                {
                    row.Cells[i].Style.SelectionBackColor = Color.LightSkyBlue;
                }
                row.Cells[33].Style.SelectionBackColor = row.Cells[33].Style.BackColor;
                row.Cells[34].Style.SelectionBackColor = row.Cells[34].Style.BackColor;
                row.Cells[35].Style.SelectionBackColor = row.Cells[35].Style.BackColor;
                row.Cells[36].Style.SelectionBackColor = row.Cells[36].Style.BackColor;
                row.Cells[37].Style.SelectionBackColor = row.Cells[37].Style.BackColor;
                row.Selected = true;

                string woIDx = row.Cells[1].Value.ToString();

                string picIDx = row.Cells[2].Value.ToString();
                string picName = row.Cells[3].Value.ToString();
                string picLevel = row.Cells[4].Value.ToString();

                string equipIDx = row.Cells[5].Value.ToString();
                string equipName = row.Cells[6].Value.ToString();
                string machineIDx = row.Cells[7].Value.ToString();
                string machineName = row.Cells[8].Value.ToString();
                string machineDesc = row.Cells[9].Value.ToString();

                string priority = row.Cells[10].Value.ToString();
                string dateFinish = row.Cells[11].Value.ToString();
                string orderNote = row.Cells[12].Value.ToString();
                string added = row.Cells[13].Value.ToString();

                string makerIDx = row.Cells[14].Value.ToString();
                string makerName = row.Cells[15].Value.ToString();
                string makerApprove = row.Cells[16].Value.ToString();
                string makerApproveDate = row.Cells[17].Value.ToString();

                string managerIDx = row.Cells[18].Value.ToString();
                string managerName = row.Cells[19].Value.ToString();
                string managerApprove = row.Cells[20].Value.ToString();
                string managerApproveNote = row.Cells[21].Value.ToString();
                string managerApproveDate = row.Cells[22].Value.ToString();

                string repairIDx = row.Cells[23].Value.ToString();
                string repairName = row.Cells[24].Value.ToString();
                string repairApprove = row.Cells[25].Value.ToString();
                string repairApproveNote = row.Cells[26].Value.ToString();
                string repairApproveDate = row.Cells[27].Value.ToString();

                string confirmIDx = row.Cells[28].Value.ToString();
                string conFirmName = row.Cells[29].Value.ToString();
                string confirmApprove = row.Cells[30].Value.ToString();
                string confirmApproveNote = row.Cells[31].Value.ToString();
                string confirmApproveDate = row.Cells[32].Value.ToString();

                _picIDx = picIDx;
                _picName = picName;
                _picLevel = picLevel;

                _equipIDx = equipIDx;
                _equipName = equipName;
                _machineIDx = machineIDx;
                _machineName = machineName;
                _machineDesc = machineDesc;

                _priority = priority;
                _dateFinish = dateFinish;
                _orderNote = orderNote;
                _added = added;

                _makerIDx = makerIDx;
                _makerName = makerName;
                _makerApprove = makerApprove;
                _makerApproveDate = makerApproveDate;

                _managerIDx = managerIDx;
                _managerName = managerName;
                _managerApprove = managerApprove;
                _managerApproveNote = managerApproveNote;
                _managerApproveDate = managerApproveDate;

                _repairIDx = repairIDx;
                _repairName = repairName;
                _repairApprove = repairApprove;
                _repairApproveNote = repairApproveNote;
                _repairApproveDate = repairApproveDate;

                _confirmIDx = confirmIDx;
                _conFirmName = conFirmName;
                _confirmApprove = confirmApprove;
                _confirmApproveNote = confirmApproveNote;
                _confirmApproveDate = confirmApproveDate;

                string manApprove = row.Cells[19].Value.ToString();

                _woIDx = woIDx;
                _manApprove = manApprove;

                string sqlImage = "PMMS_02_View_Repair_Work_Order_Pictures_V1o0_Addnew";

                SqlParameter[] sParamsImage = new SqlParameter[1]; // Parameter count

                sParamsImage[0] = new SqlParameter();
                sParamsImage[0].SqlDbType = SqlDbType.Int;
                sParamsImage[0].ParameterName = "@woIDx";
                sParamsImage[0].Value = woIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sqlImage, sParamsImage);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string imgIssueName = "", imgIssueDate = "", imgSolveName = "", imgSolveDate = "";

                        imgIssueName = ds.Tables[0].Rows[0][2].ToString();
                        imgIssueDate = ds.Tables[0].Rows[0][4].ToString();
                        imgSolveName = ds.Tables[0].Rows[0][6].ToString();
                        imgSolveDate = ds.Tables[0].Rows[0][8].ToString();

                        if (imgIssueDate != "" && imgIssueDate != null)
                        {
                            Bitmap bmpIssue;
                            MemoryStream msIssue;
                            msIssue = new MemoryStream((byte[])ds.Tables[0].Rows[0][3]);
                            bmpIssue = new Bitmap(msIssue);
                            pnl_Approve_Issue.BackgroundImage = bmpIssue;
                            pnl_Approve_Issue.BackgroundImageLayout = ImageLayout.Stretch;

                            bmpIssue = null;
                            msIssue = null;

                            lbl_Approve_Issue_Date.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(imgIssueDate));
                        }
                        else
                        {
                            pnl_Approve_Issue.BackgroundImage = null;
                            lbl_Approve_Issue_Date.Text = "";
                        }

                        if (imgSolveDate != "" && imgSolveDate != null)
                        {
                            Bitmap bmpSolve;
                            MemoryStream msSolve;
                            msSolve = new MemoryStream((byte[])ds.Tables[0].Rows[0][7]);
                            bmpSolve = new Bitmap(msSolve);
                            pnl_Approve_Solve.BackgroundImage = bmpSolve;
                            pnl_Approve_Solve.BackgroundImageLayout = ImageLayout.Stretch;

                            bmpSolve = null;
                            msSolve = null;

                            lbl_Approve_Solve_Date.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(imgSolveDate));
                        }
                        else
                        {
                            pnl_Approve_Solve.BackgroundImage = null;
                            lbl_Approve_Solve_Date.Text = "";
                        }
                    }
                }



                lbl_Approve_Maker_Name.Text = makerName.ToUpper();
                lbl_Approve_Maker_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(added));
                lbl_Approve_Equipment.Text = equipName;
                lbl_Approve_Machine.Text = machineName;
                lbl_Approve_Desc.Text = machineDesc;
                lbl_Approve_Date_Finish.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dateFinish));
                lbl_Approve_Priority.Text = priority;
                lbl_Approve_Note.Text = orderNote;
                cbb_Approve_Choice.SelectedIndex = 0;
                cbb_Approve_Choice.Enabled = true;
                txt_Approve_Reason.Text = "";
                btn_Approve_Save.Enabled = false;
                btn_Approve_Done.Enabled = true;

            }
        }

        private void dgv_Approve_Repair_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgv_Approve_Repair.Rows[e.RowIndex].Selected = true;
                _row_Approve_Repair_Index = e.RowIndex;
                dgv_Approve_Repair.CurrentCell = dgv_Approve_Repair.Rows[e.RowIndex].Cells[0];
                cms_Approve_Orders.Show(this.dgv_Approve_Repair, e.Location);
                cms_Approve_Orders.Show(Cursor.Position);
            }
        }

        private void tảiLạiDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            init_Approve_Orders();
            init_Approve_Orders_Done();
        }

        private void xemChiTiếtYêuCầuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string woIDx = _woIDx;
            frmPM_02WorkApprove_Details frmDetail = new frmPM_02WorkApprove_Details(woIDx);
            frmDetail.ShowDialog();
        }

        private void xóaYêuCầuĐangChọnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_repairApprove == "0" || _repairApprove == "3")
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        string woIDx = _woIDx;
                        string sql = "PMMS_02_Delete_Repair_Work_Order_V1o0_Addnew";
                        SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@woIDx";
                        sParams[0].Value = woIDx;

                        cls.fnUpdDel(sql, sParams);

                        _msgText = "Xóa yêu cầu sửa chữa thành công.";
                        _msgType = 1;
                    }
                    catch (SqlException sqlEx)
                    {
                        _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng báo lại quản trị hệ thống";
                        _msgType = 3;
                    }
                    catch (Exception ex)
                    {
                        _msgText = "Có lỗi phát sinh. Vui lòng báo lại quản trị hệ thống";
                        _msgType = 2;
                    }
                    finally
                    {
                        _woIDx = "";
                        init_Approve_Orders();
                        init_Approve_Orders_Done();
                        cls.fnMessage(tssMessage, _msgText, _msgType);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa WO khi bên sửa chữa đã tiếp nhận yêu cầu.\r\nVui lòng liên hệ với bên sửa chữa để dừng công việc và xóa WO này.");
            }

        }

        public void fnLinkColor()
        {
            init_Approve_Orders_Done();
            switch (_rangeTime)
            {
                case 1:
                    lnk_Order_Repair_Today.LinkColor = Color.Red;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 2:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Red;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 3:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Red;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 4:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Red;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 5:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Red;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 6:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Red;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 7:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Red;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 8:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Red;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 9:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Red;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 10:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Red;
                    break;
            }
        }

        private void btn_Approve_Done_Click(object sender, EventArgs e)
        {
            init_Approve_Orders_Done();
        }

        private void btn_Approve_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string woIDx = _woIDx;
                    string choiceIDx = cbb_Approve_Choice.SelectedIndex.ToString();
                    string choice = cbb_Approve_Choice.Text;
                    string reason = txt_Approve_Reason.Text.Trim();

                    string sql = "PMMS_02_Approve_Repair_Work_Order_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@woIDx";
                    sParams[0].Value = woIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.TinyInt;
                    sParams[1].ParameterName = "@approve";
                    sParams[1].Value = choiceIDx;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.NVarChar;
                    sParams[2].ParameterName = "@approveNote";
                    sParams[2].Value = reason;

                    cls.fnUpdDel(sql, sParams);

                    _msgText = "Phê duyệt yêu cầu thành công.";
                    _msgType = 1;
                }
                catch (SqlException sqlEx)
                {
                    _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng báo lại quản trị hệ thống";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    _msgText = "Có lỗi phát sinh. Vui lòng báo lại quản trị hệ thống";
                    _msgType = 2;
                }
                finally
                {
                    init_Approve_Orders();
                    init_Approve_Orders_Done();

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }

        private void chk_Approve_Auto_Click(object sender, EventArgs e)
        {
            if (chk_Approve_Auto.Checked)
            {
                MessageBox.Show("LƯU Ý TRƯỚC KHI SỬ DỤNG!!!\r\n\r\nChức năng này không tự động tắt,\r\nngười dùng có thể sẽ KHÔNG KIỂM SOÁT được phê duyệt của mình.\r\nDo vậy, chỉ sử dụng chức năng này khi không có giải pháp khác thay thế");
            }
            else
            {
                MessageBox.Show("Chức năng phê duyệt tự động đã tắt bởi người dùng");
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string logIDx = _logIDx.ToString();
                    bool auto = (chk_Approve_Auto.Checked) ? true : false;
                    string sql = "PMMS_02_Set_Repair_Work_Order_Auto_Approve_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@picIDx";
                    sParams[0].Value = logIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Bit;
                    sParams[1].ParameterName = "@auto";
                    sParams[1].Value = auto;

                    cls.fnUpdDel(sql, sParams);

                    _msgText = "Cập nhật chức năng phê duyệt tự động thành công";
                    _msgType = 1;

                }
                catch (SqlException sqlEx)
                {
                    _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng báo lại quản trị hệ thống";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    _msgText = "Có lỗi phát sinh. Vui lòng báo lại quản trị hệ thống";
                    _msgType = 2;
                }
                finally
                {
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion


        #region APPROVE ENTRANCE REGISTER


        public void init_Entrance_Register()
        {



            init_Entrance_Register_List();
        }

        public void init_Entrance_Register_List()
        {
            string makerIDx = _logIDx.ToString();
            string range = _rangeTime.ToString();
            string sql = "V2o1_ERP_Entrance_System_Manager_Approve_List_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = makerIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@rangeTime";
            sParams[1].Value = range;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            //foreach (DataRow dr in dt.Rows)
            //{

            //    string regDT = dr[12].ToString();
            //    string newRegDT = cls.Left(regDT, 10);
            //    dr[12] = newRegDT;
            //}

            _dgv_ES_List_Width = cls.fnGetDataGridWidth(dgv_ES_List);
            dgv_ES_List.DataSource = dt;

            dgv_ES_List.Columns[0].Width = 3 * _dgv_ES_List_Width / 100;    // STT
            //dgv_ES_List.Columns[1].Width = 5 * _dgv_ES_List_Width / 100;    // idx
            //dgv_ES_List.Columns[2].Width = 5 * _dgv_ES_List_Width / 100;    // picIDx
            dgv_ES_List.Columns[3].Width = 10 * _dgv_ES_List_Width / 100;    // Name
            //dgv_ES_List.Columns[4].Width = 5 * _dgv_ES_List_Width / 100;    // Dept
            dgv_ES_List.Columns[5].Width = 3 * _dgv_ES_List_Width / 100;    // IN-OUT
            //dgv_ES_List.Columns[6].Width = 20 * _dgv_ES_List_Width / 100;    // inFrom
            //dgv_ES_List.Columns[7].Width = 15 * _dgv_ES_List_Width / 100;    // inRepNm
            //dgv_ES_List.Columns[8].Width = 7 * _dgv_ES_List_Width / 100;    // inRepID
            dgv_ES_List.Columns[9].Width = 15 * _dgv_ES_List_Width / 100;    // Representative
            //dgv_ES_List.Columns[10].Width = 4 * _dgv_ES_List_Width / 100;    // inQty
            dgv_ES_List.Columns[11].Width = 14 * _dgv_ES_List_Width / 100;    // inPurpose
            dgv_ES_List.Columns[12].Width = 6 * _dgv_ES_List_Width / 100;    // inRegDateTime
            dgv_ES_List.Columns[13].Width = 3 * _dgv_ES_List_Width / 100;    // inWithTools
            //dgv_ES_List.Columns[14].Width = 5 * _dgv_ES_List_Width / 100;    // inRemarks
            //dgv_ES_List.Columns[15].Width = 5 * _dgv_ES_List_Width / 100;    // managerApprove
            dgv_ES_List.Columns[16].Width = 10 * _dgv_ES_List_Width / 100;    // [Manager Approve]
            //dgv_ES_List.Columns[17].Width = 5 * _dgv_ES_List_Width / 100;    // directorApprove
            dgv_ES_List.Columns[18].Width = 10 * _dgv_ES_List_Width / 100;    // [Director Approve]
            //dgv_ES_List.Columns[19].Width = 5 * _dgv_ES_List_Width / 100;    // hrApprove
            dgv_ES_List.Columns[20].Width = 10 * _dgv_ES_List_Width / 100;    // [H.R Approve]
            //dgv_ES_List.Columns[21].Width = 5 * _dgv_ES_List_Width / 100;    // securityApprove
            dgv_ES_List.Columns[22].Width = 10 * _dgv_ES_List_Width / 100;    // [Security Control]
            dgv_ES_List.Columns[23].Width = 6 * _dgv_ES_List_Width / 100;    // regAdded
            //dgv_ES_List.Columns[24].Width = 5 * _dgv_ES_List_Width / 100;    // regFinish
            //dgv_ES_List.Columns[25].Width = 5 * _dgv_ES_List_Width / 100;    // regFinishDate

            dgv_ES_List.Columns[0].Visible = true;
            dgv_ES_List.Columns[1].Visible = false;
            dgv_ES_List.Columns[2].Visible = false;
            dgv_ES_List.Columns[3].Visible = true;
            dgv_ES_List.Columns[4].Visible = false;
            dgv_ES_List.Columns[5].Visible = true;
            dgv_ES_List.Columns[6].Visible = false;
            dgv_ES_List.Columns[7].Visible = false;
            dgv_ES_List.Columns[8].Visible = false;
            dgv_ES_List.Columns[9].Visible = true;
            dgv_ES_List.Columns[10].Visible = false;
            dgv_ES_List.Columns[11].Visible = true;
            dgv_ES_List.Columns[12].Visible = true;
            dgv_ES_List.Columns[13].Visible = true;
            dgv_ES_List.Columns[14].Visible = false;
            dgv_ES_List.Columns[15].Visible = false;
            dgv_ES_List.Columns[16].Visible = true;
            dgv_ES_List.Columns[17].Visible = false;
            dgv_ES_List.Columns[18].Visible = true;
            dgv_ES_List.Columns[19].Visible = false;
            dgv_ES_List.Columns[20].Visible = true;
            dgv_ES_List.Columns[21].Visible = false;
            dgv_ES_List.Columns[22].Visible = true;
            dgv_ES_List.Columns[23].Visible = true;
            dgv_ES_List.Columns[24].Visible = false;
            dgv_ES_List.Columns[25].Visible = false;

            dgv_ES_List.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_ES_List.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_ES_List.Columns[23].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm tt";
            cls.fnFormatDatagridviewWhite(dgv_ES_List, 11, 80);


            string regDT = "";
            string manager = "", director = "", hr = "", security = "";
            DateTime _regDT;
            int _manager = 0, _director = 0, _hr = 0, _security = 0;
            Color _bgManager = Color.Silver, _bgDirector = Color.Silver;
            Color _bgHR = Color.Silver, _bgSecurity = Color.Silver;
            foreach (DataGridViewRow row in dgv_ES_List.Rows)
            {
                regDT = row.Cells[12].Value.ToString().Replace("00:00 AM", "");

                //_regDT = Convert.ToDateTime(regDT.Replace("00:00 AM", ""));
                row.Cells[12].Value = regDT.ToString();

                manager = row.Cells[15].Value.ToString();
                director = row.Cells[17].Value.ToString();
                hr = row.Cells[19].Value.ToString();
                security = row.Cells[21].Value.ToString();

                _manager = (manager != "" && manager != null) ? Convert.ToInt32(manager) : 0;
                _director = (director != "" && director != null) ? Convert.ToInt32(director) : 0;
                _hr = (hr != "" && hr != null) ? Convert.ToInt32(hr) : 0;
                _security = (security != "" && security != null) ? Convert.ToInt32(security) : 0;

                switch (manager)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgManager = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgManager = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgManager = Color.LightPink;
                        break;
                }
                row.Cells[16].Style.BackColor = _bgManager;
                switch (director)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgDirector = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgDirector = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgDirector = Color.LightPink;
                        break;
                }
                row.Cells[18].Style.BackColor = _bgDirector;
                switch (hr)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgHR = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgHR = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgHR = Color.LightPink;
                        break;
                }
                row.Cells[20].Style.BackColor = _bgHR;
                switch (security)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgSecurity = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgSecurity = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgSecurity = Color.LightPink;
                        break;
                }
                row.Cells[22].Style.BackColor = _bgSecurity;
            }

        }

        private void dgv_ES_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                init_Entrance_Register_List();
                //cls.fnDatagridClickCell(dgv_ES_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_ES_List.Rows[e.RowIndex];
                string regIDx = row.Cells[1].Value.ToString();

                for (int i = 0; i < 15; i++)
                {
                    row.Cells[i].Style.SelectionBackColor = Color.LightSkyBlue;
                    row.Selected = true;
                }
                row.Cells[16].Style.SelectionBackColor = row.Cells[16].Style.BackColor;
                row.Cells[18].Style.SelectionBackColor = row.Cells[16].Style.BackColor;
                row.Cells[20].Style.SelectionBackColor = row.Cells[16].Style.BackColor;
                row.Cells[22].Style.SelectionBackColor = row.Cells[16].Style.BackColor;

                _regIDx = Convert.ToInt32(regIDx);
            }
        }

        private void dgv_ES_List_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_ES_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_ES_List.Rows[e.RowIndex];
                string regIDx = row.Cells[1].Value.ToString();

                frmPM_02WorkApprove_ES_Details frm = new frmPM_02WorkApprove_ES_Details(regIDx, _logIDx);
                frm.ShowDialog();
            }
        }


        #endregion

    }
}
