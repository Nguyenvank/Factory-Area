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
    public partial class frmPM_03RepairProcess : Form
    {
        public int _dgv_Repair_Assign_List_Width;
        public int _dgv_Work_Detail_List_Width;
        public int _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width;
        public int _dgv_Work_Detail_History_Machine_Replace_PartList_Width;
        public int _row_Order_Repair_Index = 0;

        public bool _login;
        public int _logIDx = 0;
        public int _logLevel = 0;
        public string _logName = "";

        public string _msgText;
        public int _msgType;

        public string _matImage = "";
        public bool _matNewImage;

        public int _rangeTime = 3;
        public string _woIDx = "", _manApprove = "";
        public string _picIDx = "", _picName = "", _picLevel = "";
        public string _equipIDx = "", _equipName = "", _machineIDx = "", _machineName = "", _machineDesc = "";
        public string _priority = "", _dateFinish = "", _orderNote = "", _added = "";
        public string _makerIDx = "", _makerName = "", _makerApprove = "", _makerApproveDate = "";
        public string _managerIDx = "", _managerName = "", _managerApprove = "", _managerApproveNote = "", _managerApproveDate = "";
        public string _repairIDx = "", _repairName = "", _repairApprove = "", _repairApproveNote = "", _repairApproveDate = "";
        public string _confirmIDx = "", _conFirmName = "", _confirmApprove = "", _confirmApproveNote = "", _confirmApproveDate = "";

        public DateTime _dt;
        Timer timer = new Timer();

        public frmPM_03RepairProcess(int logIDx, int logLevel, string logName)
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
                frmPM_00LoginSystem frmLogin = new frmPM_00LoginSystem(3);
                frmLogin.ShowDialog();
            }
        }

        private void frmPM_03RepairProcess_Load(object sender, EventArgs e)
        {
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

            init_Repair_Assign();
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
                    init_Repair_Assign();
                    fnLinkColor();
                    break;
                case 1:
                    init_Work_Detail();
                    //tabControl2.SelectedIndex = 0;
                    tabControl2.Enabled = false;
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
                    init_Work_Detail_History_Repair();
                    break;
                case 1:
                    init_Work_Detail_History_Replace();
                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 10000;
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
            tssMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
            tssMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            timer.Stop();
        }

        private void frmPM_03RepairProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }


        #region WORK ASSIGN


        public void init_Repair_Assign()
        {
            init_Repair_Assign_Order_List();
            init_Repair_Assign_PIC();

            lbl_Repair_Assign_Equipment.Text = "";
            lbl_Repair_Assign_Machine.Text = "";
            lbl_Repair_Assign_Machine_Desc.Text = "";
            lbl_Repair_Assign_Finish_Date.Text = "";
            lbl_Repair_Assign_Priority.Text = "";
            lbl_Repair_Assign_Date_Issue.Text = "";
            lbl_Repair_Assign_Date_Solve.Text = "";
            pnl_Repair_Assign_Image_Issue.BackgroundImage = null;
            pnl_Repair_Assign_Image_Solve.BackgroundImage = null;
            lbl_Repair_Assign_Order_Note.Text = "";
            cbb_Repair_Assign_PIC.SelectedIndex = 0;
            cbb_Repair_Assign_PIC.Enabled = false;
            txt_Repair_Assign_Qty_Job.Text = "";
            txt_Repair_Assign_Qty_Job.Enabled = false;
            txt_Repair_Assign_Note.Text = "";
            txt_Repair_Assign_Note.Enabled = false;
            btn_Repair_Assign_Save.Enabled = false;
            btn_Repair_Assign_Done.Enabled = false;


        }

        public void init_Repair_Assign_PIC()
        {
            string sql = "PMMS_03_Repair_Assign_PIC_SelItem_V1o0_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbb_Repair_Assign_PIC.DataSource = dt;
            cbb_Repair_Assign_PIC.DisplayMember = "Name";
            cbb_Repair_Assign_PIC.ValueMember = "idx";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbb_Repair_Assign_PIC.SelectedIndex = 0;
        }

        public void init_Repair_Assign_Order_List()
        {
            try
            {
                string sql = "PMMS_03_Repair_Assign_Order_List_SelItem_V1o0_Addnew";

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

                _dgv_Repair_Assign_List_Width = cls.fnGetDataGridWidth(dgv_Repair_Assign_List);
                dgv_Repair_Assign_List.DataSource = dt;

                dgv_Repair_Assign_List.Columns[0].Width = 5 * _dgv_Repair_Assign_List_Width / 100;    // STT
                //dgv_Repair_Assign_List.Columns[1].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // idx
                //dgv_Repair_Assign_List.Columns[2].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // picIDx
                //dgv_Repair_Assign_List.Columns[3].Width = 14 * _dgv_Repair_Assign_List_Width / 100;    // picName
                //dgv_Repair_Assign_List.Columns[4].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // picLevel
                //dgv_Repair_Assign_List.Columns[5].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // equipmentIDx
                dgv_Repair_Assign_List.Columns[6].Width = 12 * _dgv_Repair_Assign_List_Width / 100;    // equipmentName
                //dgv_Repair_Assign_List.Columns[7].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // machineIDx
                dgv_Repair_Assign_List.Columns[8].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // machineName
                dgv_Repair_Assign_List.Columns[9].Width = 15 * _dgv_Repair_Assign_List_Width / 100;    // machineDesc
                dgv_Repair_Assign_List.Columns[10].Width = 7 * _dgv_Repair_Assign_List_Width / 100;    // orderPriority
                dgv_Repair_Assign_List.Columns[11].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // dateFinish
                //dgv_Repair_Assign_List.Columns[12].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // orderNote
                //dgv_Repair_Assign_List.Columns[13].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // added
                //dgv_Repair_Assign_List.Columns[14].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // makerIDx
                //dgv_Repair_Assign_List.Columns[15].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // makerName
                //dgv_Repair_Assign_List.Columns[16].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // makerApprove
                //dgv_Repair_Assign_List.Columns[17].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // makerApproveDate
                //dgv_Repair_Assign_List.Columns[18].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // managerIDx
                //dgv_Repair_Assign_List.Columns[19].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // managerName
                //dgv_Repair_Assign_List.Columns[20].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // managerApprove
                //dgv_Repair_Assign_List.Columns[21].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // managerApproveNote
                dgv_Repair_Assign_List.Columns[22].Width = 12 * _dgv_Repair_Assign_List_Width / 100;    // managerApproveDate
                //dgv_Repair_Assign_List.Columns[23].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // pmIDx
                dgv_Repair_Assign_List.Columns[24].Width = 14 * _dgv_Repair_Assign_List_Width / 100;    // pmName
                //dgv_Repair_Assign_List.Columns[25].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // pmFinish
                //dgv_Repair_Assign_List.Columns[26].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // pmFinishNote
                //dgv_Repair_Assign_List.Columns[27].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // pmFinishDate
                //dgv_Repair_Assign_List.Columns[28].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // confirmIDx
                //dgv_Repair_Assign_List.Columns[29].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // confirmName
                //dgv_Repair_Assign_List.Columns[30].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // confirmClosed
                //dgv_Repair_Assign_List.Columns[31].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // confirmClosedNote
                //dgv_Repair_Assign_List.Columns[32].Width = 10 * _dgv_Repair_Assign_List_Width / 100;    // confirmClosedDate
                dgv_Repair_Assign_List.Columns[33].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // makerStatus
                dgv_Repair_Assign_List.Columns[34].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // managerStatus
                dgv_Repair_Assign_List.Columns[35].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // repairStatus
                dgv_Repair_Assign_List.Columns[36].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // confirmStatus
                dgv_Repair_Assign_List.Columns[37].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // orderClosed (blank)
                dgv_Repair_Assign_List.Columns[38].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // orderClosed (value)
                dgv_Repair_Assign_List.Columns[39].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // closedDate
                dgv_Repair_Assign_List.Columns[40].Width = 3 * _dgv_Repair_Assign_List_Width / 100;    // takeTime

                dgv_Repair_Assign_List.Columns[0].Visible = true;
                dgv_Repair_Assign_List.Columns[1].Visible = false;
                dgv_Repair_Assign_List.Columns[2].Visible = false;
                dgv_Repair_Assign_List.Columns[3].Visible = false;
                dgv_Repair_Assign_List.Columns[4].Visible = false;
                dgv_Repair_Assign_List.Columns[5].Visible = false;
                dgv_Repair_Assign_List.Columns[6].Visible = true;
                dgv_Repair_Assign_List.Columns[7].Visible = false;
                dgv_Repair_Assign_List.Columns[8].Visible = true;
                dgv_Repair_Assign_List.Columns[9].Visible = true;
                dgv_Repair_Assign_List.Columns[10].Visible = true;
                dgv_Repair_Assign_List.Columns[11].Visible = true;
                dgv_Repair_Assign_List.Columns[12].Visible = false;
                dgv_Repair_Assign_List.Columns[13].Visible = false;
                dgv_Repair_Assign_List.Columns[14].Visible = false;
                dgv_Repair_Assign_List.Columns[15].Visible = false;
                dgv_Repair_Assign_List.Columns[16].Visible = false;
                dgv_Repair_Assign_List.Columns[17].Visible = false;
                dgv_Repair_Assign_List.Columns[18].Visible = false;
                dgv_Repair_Assign_List.Columns[19].Visible = false;
                dgv_Repair_Assign_List.Columns[20].Visible = false;
                dgv_Repair_Assign_List.Columns[21].Visible = false;
                dgv_Repair_Assign_List.Columns[22].Visible = true;
                dgv_Repair_Assign_List.Columns[23].Visible = false;
                dgv_Repair_Assign_List.Columns[24].Visible = true;
                dgv_Repair_Assign_List.Columns[25].Visible = false;
                dgv_Repair_Assign_List.Columns[26].Visible = false;
                dgv_Repair_Assign_List.Columns[27].Visible = false;
                dgv_Repair_Assign_List.Columns[28].Visible = false;
                dgv_Repair_Assign_List.Columns[29].Visible = false;
                dgv_Repair_Assign_List.Columns[30].Visible = false;
                dgv_Repair_Assign_List.Columns[31].Visible = false;
                dgv_Repair_Assign_List.Columns[32].Visible = false;
                dgv_Repair_Assign_List.Columns[33].Visible = true;
                dgv_Repair_Assign_List.Columns[34].Visible = true;
                dgv_Repair_Assign_List.Columns[35].Visible = true;
                dgv_Repair_Assign_List.Columns[36].Visible = true;
                dgv_Repair_Assign_List.Columns[37].Visible = true;
                dgv_Repair_Assign_List.Columns[38].Visible = false;
                dgv_Repair_Assign_List.Columns[39].Visible = false;
                dgv_Repair_Assign_List.Columns[40].Visible = false;

                dgv_Repair_Assign_List.Columns[11].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgv_Repair_Assign_List.Columns[22].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                dgv_Repair_Assign_List.Columns[22].HeaderText = "WO được duyệt";
                dgv_Repair_Assign_List.Columns[33].HeaderText = "M";
                dgv_Repair_Assign_List.Columns[34].HeaderText = "A";
                dgv_Repair_Assign_List.Columns[35].HeaderText = "R";
                dgv_Repair_Assign_List.Columns[36].HeaderText = "C";
                dgv_Repair_Assign_List.Columns[37].HeaderText = "T";

                cls.fnFormatDatagridview(dgv_Repair_Assign_List, 11, 30);


                string makerName = "", makerApprove = "", makerApproveDate = "", orderClosed = "";
                string managerName = "", managerApprove = "", managerApproveNote = "", managerApproveDate = "";
                string repairName = "", repairApprove = "", repairApproveNote = "", repairApproveDate = "";
                string confirmName = "", confirmApprove = "", confirmApproveNote = "", confirmApproveDate = "";

                int _orderClosed = 0, _makerApprove = 0, _managerApprove = 0, _repairApprove = 0, _confirmApprove = 0;

                foreach (DataGridViewRow row in dgv_Repair_Assign_List.Rows)
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

                    row.Cells[24].Value = (_repairApprove == 3) ? "" : repairName;

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
            catch
            {

            }
            finally
            {

            }
        }

        public void init_Repair_Assign_Done()
        {
            _woIDx = ""; _manApprove = "";
            _picIDx = ""; _picName = ""; _picLevel = "";
            _equipIDx = ""; _equipName = ""; _machineIDx = ""; _machineName = ""; _machineDesc = "";
            _priority = ""; _dateFinish = ""; _orderNote = ""; _added = "";
            _makerIDx = ""; _makerName = ""; _makerApprove = ""; _makerApproveDate = "";
            _managerIDx = ""; _managerName = ""; _managerApprove = ""; _managerApproveNote = ""; _managerApproveDate = "";
            _repairIDx = ""; _repairName = ""; _repairApprove = ""; _repairApproveNote = ""; _repairApproveDate = "";
            _confirmIDx = ""; _conFirmName = ""; _confirmApprove = ""; _confirmApproveNote = ""; _confirmApproveDate = "";

            dgv_Repair_Assign_List.ClearSelection();

            lbl_Repair_Assign_Equipment.Text = "";
            lbl_Repair_Assign_Machine.Text = "";
            lbl_Repair_Assign_Machine_Desc.Text = "";
            lbl_Repair_Assign_Finish_Date.Text = "";
            lbl_Repair_Assign_Priority.Text = "";
            lbl_Repair_Assign_Date_Issue.Text = "";
            lbl_Repair_Assign_Date_Solve.Text = "";
            pnl_Repair_Assign_Image_Issue.BackgroundImage = null;
            pnl_Repair_Assign_Image_Solve.BackgroundImage = null;
            lbl_Repair_Assign_Order_Note.Text = "";
            cbb_Repair_Assign_PIC.SelectedIndex = 0;
            cbb_Repair_Assign_PIC.Enabled = false;
            txt_Repair_Assign_Qty_Job.Text = "";
            txt_Repair_Assign_Qty_Job.Enabled = false;
            lnk_Repair_Assign_Qty_Job_View.Enabled = false;
            txt_Repair_Assign_Note.Text = "";
            txt_Repair_Assign_Note.Enabled = false;
            btn_Repair_Assign_Save.Enabled = false;
            btn_Repair_Assign_Done.Enabled = false;
        }

        private void lnk_Order_Repair_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 1;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 2;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Week_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 3;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_10Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 4;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 5;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_2Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 6;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_3Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 7;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_6Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 8;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_9Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 9;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 10;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_Unssigned_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeTime = 11;
            dgv_Repair_Assign_List.DataSource = "";
            init_Repair_Assign_Order_List();
            fnLinkColor();
        }

        public void fnLinkColor()
        {
            init_Repair_Assign_Done();
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
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
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Blue;
                    break;
                case 11:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    lnk_Order_Repair_Unssigned.LinkColor = Color.Red;
                    break;
            }
        }

        private void dgv_Repair_Assign_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Repair_Assign_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //cls.fnDatagridClickCell(dgv_Order_Repair, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Repair_Assign_List.Rows[e.RowIndex];
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

                string manApprove = row.Cells[20].Value.ToString();

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
                            pnl_Repair_Assign_Image_Issue.BackgroundImage = bmpIssue;
                            pnl_Repair_Assign_Image_Issue.BackgroundImageLayout = ImageLayout.Stretch;

                            bmpIssue = null;
                            msIssue = null;

                            lbl_Repair_Assign_Date_Issue.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(imgIssueDate));
                        }
                        else
                        {
                            pnl_Repair_Assign_Image_Issue.BackgroundImage = null;
                            lbl_Repair_Assign_Date_Issue.Text = "";
                        }

                        if (imgSolveDate != "" && imgSolveDate != null)
                        {
                            Bitmap bmpSolve;
                            MemoryStream msSolve;
                            msSolve = new MemoryStream((byte[])ds.Tables[0].Rows[0][7]);
                            bmpSolve = new Bitmap(msSolve);
                            pnl_Repair_Assign_Image_Solve.BackgroundImage = bmpSolve;
                            pnl_Repair_Assign_Image_Solve.BackgroundImageLayout = ImageLayout.Stretch;

                            bmpSolve = null;
                            msSolve = null;

                            lbl_Repair_Assign_Date_Solve.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(imgSolveDate));
                        }
                        else
                        {
                            pnl_Repair_Assign_Image_Solve.BackgroundImage = null;
                            lbl_Repair_Assign_Date_Solve.Text = "";
                        }
                    }
                }



                //lbl_Approve_Maker_Name.Text = makerName.ToUpper();
                //lbl_Approve_Maker_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(added));
                lbl_Repair_Assign_Equipment.Text = equipName;
                lbl_Repair_Assign_Machine.Text = machineName;
                lbl_Repair_Assign_Machine_Desc.Text = machineDesc;
                lbl_Repair_Assign_Finish_Date.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dateFinish));
                lbl_Repair_Assign_Priority.Text = priority;
                lbl_Repair_Assign_Order_Note.Text = orderNote;
                cbb_Repair_Assign_PIC.SelectedIndex = 0;
                cbb_Repair_Assign_PIC.Enabled = true;
                txt_Repair_Assign_Qty_Job.Text = "0";
                txt_Repair_Assign_Qty_Job.ReadOnly = true;
                txt_Repair_Assign_Note.Text = "";
                txt_Repair_Assign_Note.Enabled = false;
                btn_Repair_Assign_Save.Enabled = false;
                btn_Repair_Assign_Done.Enabled = true;

            }
        }

        private void dgv_Repair_Assign_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cbb_Repair_Assign_PIC_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Repair_Assign_PIC.SelectedIndex > 0)
            {
                string workQty = "";
                string selPIC = cbb_Repair_Assign_PIC.SelectedValue.ToString();
                string sql = "PMMS_03_Repair_Assign_PIC_Work_Qty_SelItem_V1o0_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@picIDx";
                sParams[0].Value = selPIC;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        workQty = ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        workQty = "0";
                    }
                }
                else
                {
                    workQty = "0";
                }

                txt_Repair_Assign_Qty_Job.Text = workQty;
                lnk_Repair_Assign_Qty_Job_View.Enabled = (workQty == "0") ? false : true;
                txt_Repair_Assign_Note.Text = "";
                txt_Repair_Assign_Note.Enabled = true;

                btn_Repair_Assign_Save.Enabled = true;
            }
            else
            {
                txt_Repair_Assign_Qty_Job.Text = "0";
                lnk_Repair_Assign_Qty_Job_View.Enabled = false;
                txt_Repair_Assign_Note.Text = "";
                txt_Repair_Assign_Note.Enabled = false;

                btn_Repair_Assign_Save.Enabled = false;
            }
        }

        private void btn_Repair_Assign_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string woIDx = _woIDx;
                    string picIDx = cbb_Repair_Assign_PIC.SelectedValue.ToString();
                    string picName = cbb_Repair_Assign_PIC.Text;
                    string picNote = txt_Repair_Assign_Note.Text.Trim();

                    string sql = "PMMS_03_Repair_Assign_PIC_Work_UpdItem_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@woIDx";
                    sParams[0].Value = woIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@picIDx";
                    sParams[1].Value = picIDx;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.NVarChar;
                    sParams[2].ParameterName = "@picName";
                    sParams[2].Value = picName;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.NVarChar;
                    sParams[3].ParameterName = "@picNote";
                    sParams[3].Value = picNote;

                    cls.fnUpdDel(sql, sParams);

                    _msgText = "Phân công công việc thành công.";
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
                    init_Repair_Assign_Order_List();
                    init_Repair_Assign_Done();

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion


        #region WORK DETAIL


        public void init_Work_Detail()
        {
            init_Work_Detail_List();
            init_Work_Detail_Process_State();
            init_Work_Detail_Process_Status();

            lbl_Work_Detail_Info_Equipment.Text = "";
            lbl_Work_Detail_Info_Machine.Text = "";
            lbl_Work_Detail_Info_Priority.Text = "";
            lbl_Work_Detail_Info_DateCreate.Text = "";
            lbl_Work_Detail_Info_DateFinish.Text = "";
            lbl_Work_Detail_Info_Note.Text = "";
            lbl_Work_Detail_Info_Desc.Text = "";
            pnl_Work_Detail_Info_Picture.BackgroundImage = null;

            pnl_Work_Detail_Finish_Picture.BackgroundImage = null;
            lnk_Work_Detail_Finish_PicAdd.Enabled = false;
            lnk_Work_Detail_Finish_PicUpd.Enabled = false;
            lnk_Work_Detail_Finish_PicDel.Enabled = false;
            dtp_Work_Detail_Finish_Date.Enabled = false;
            txt_Work_Detail_Finish_Note.Text = "";
            btn_Work_Detail_Finish_Save.Enabled = false;
            btn_Work_Detail_Finish_Done.Enabled = false;

            dgv_Work_Detail_Part_Spare_List.DataSource = "";
            lbl_Work_Detail_Part_OrderNo.Text = "";
            txt_Work_Detail_Part_Filter.Text = "";
            lbl_Work_Detail_Part_Name.Text = "";
            lbl_Work_Detail_Part_Code.Text = "";
            txt_Work_Detail_Part_Qty.Text = "";
            lbl_Work_Detail_Part_Unit.Text = "";
            lnk_Work_Detail_Part_Picture.Enabled = false;
            btn_Work_Detail_Part_OrdAdd.Enabled = false;
            btn_Work_Detail_Part_OrdDel.Enabled = false;
            btn_Work_Detail_Part_OrdCreate.Enabled = false;
            dgv_Work_Detail_Part_Order_List.DataSource = "";

            lbl_Work_Detail_History_Machine_Name.Text = "";
            lbl_Work_Detail_History_Machine_Desc.Text = "";
            txt_Work_Detail_History_Machine_Repair_Manual.Text = "";
            btn_Work_Detail_History_Save.Enabled = false;
        }

        public void init_Work_Detail_List()
        {
            string logIDx = _logIDx.ToString();
            int list_filter = 0;
            if (rdb_Work_Detail_All.Checked)
            {
                list_filter = 0;
            }
            else if (rdb_Work_Detail_Finish.Checked)
            {
                list_filter = 1;
            }
            else if (rdb_Work_Detail_NotFinish.Checked)
            {
                list_filter = 4;
            }

            string sql = "PMMS_03_Work_Detail_List_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = logIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Work_Detail_List_Width = cls.fnGetDataGridWidth(dgv_Work_Detail_List);
            dgv_Work_Detail_List.DataSource = dt;

            dgv_Work_Detail_List.Columns[0].Width = 15 * _dgv_Work_Detail_List_Width / 100;    // STT
            //dgv_Work_Detail_List.Columns[1].Width = 10 * _dgv_Work_Detail_List_Width / 100;    // idx
            dgv_Work_Detail_List.Columns[2].Width = 55 * _dgv_Work_Detail_List_Width / 100;    // machineName
            dgv_Work_Detail_List.Columns[3].Width = 30 * _dgv_Work_Detail_List_Width / 100;    // dateFinish
            //dgv_Work_Detail_List.Columns[4].Width = 30 * _dgv_Work_Detail_List_Width / 100;    // pmFinishDate
            //dgv_Work_Detail_List.Columns[5].Width = 10 * _dgv_Work_Detail_List_Width / 100;    // pmFinish

            dgv_Work_Detail_List.Columns[0].Visible = true;
            dgv_Work_Detail_List.Columns[1].Visible = false;
            dgv_Work_Detail_List.Columns[2].Visible = true;
            dgv_Work_Detail_List.Columns[3].Visible = true;
            dgv_Work_Detail_List.Columns[4].Visible = false;
            dgv_Work_Detail_List.Columns[5].Visible = false;

            dgv_Work_Detail_List.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Work_Detail_List, 11, 30);


            string finish = "";
            int _finish = 0;
            foreach (DataGridViewRow row in dgv_Work_Detail_List.Rows)
            {
                finish = row.Cells[5].Value.ToString();
                _finish = (finish != "" && finish != null) ? Convert.ToInt32(finish) : 0;

                switch (_finish)
                {
                    case 0:
                        // NEW (not finish)
                        break;
                    case 1:
                        // CLOSED (finish)
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // REJECT (with some causes)
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                        break;
                    case 3:
                        // RECEIVE but not assign
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        break;
                    case 4:
                        // DOING (in processing)
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case 5:
                        // PENDING (shortage spare parts)
                        row.DefaultCellStyle.BackColor = Color.Orange;
                        break;
                }
            }
        }

        public void init_Work_Detail_History_Repair()
        {
            string machineIDx = _machineIDx;
            string sql = "PMMS_03_Work_Detail_History_Repair_List_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@machineIDx";
            sParams[0].Value = machineIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width = cls.fnGetDataGridWidth(dgv_Work_Detail_History_Machine_Repair_List);
            dgv_Work_Detail_History_Machine_Repair_List.DataSource = dt;

            //dgv_Work_Detail_History_Machine_Repair_List.Columns[0].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // idx
            dgv_Work_Detail_History_Machine_Repair_List.Columns[1].Width = 15 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // added
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[2].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // picIDx
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[3].Width = 18 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // picName
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[4].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // equipmentIDx
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[5].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // equipmentName
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[6].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // machineIDx
            dgv_Work_Detail_History_Machine_Repair_List.Columns[7].Width = 20 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // machineName
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[8].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // machineDesc
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[9].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // orderPriority
            dgv_Work_Detail_History_Machine_Repair_List.Columns[10].Width = 15 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // dateFinish
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[11].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // orderNote
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[12].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // orderClosed
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[13].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // closedDate
            dgv_Work_Detail_History_Machine_Repair_List.Columns[14].Width = 15 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // takeTime
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[15].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // idx
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[16].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // pmIDx
            dgv_Work_Detail_History_Machine_Repair_List.Columns[17].Width = 20 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // pmName
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[18].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // pmFinish
            //dgv_Work_Detail_History_Machine_Repair_List.Columns[19].Width = 10 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // pmFinishNote
            dgv_Work_Detail_History_Machine_Repair_List.Columns[20].Width = 15 * _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width / 100;    // pmFinishDate

            dgv_Work_Detail_History_Machine_Repair_List.Columns[0].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[1].Visible = true;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[2].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[3].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[4].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[5].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[6].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[7].Visible = true;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[8].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[9].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[10].Visible = true;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[11].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[12].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[13].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[14].Visible = true;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[15].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[16].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[17].Visible = true;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[18].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[19].Visible = false;
            dgv_Work_Detail_History_Machine_Repair_List.Columns[20].Visible = true;

            dgv_Work_Detail_History_Machine_Repair_List.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv_Work_Detail_History_Machine_Repair_List.Columns[10].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv_Work_Detail_History_Machine_Repair_List.Columns[20].DefaultCellStyle.Format = "dd/MM/yyyy";

            cls.fnFormatDatagridview(dgv_Work_Detail_History_Machine_Repair_List, 11, 30);
        }

        public void init_Work_Detail_History_Replace()
        {
            string machineIDx = _machineIDx;
            string sql = "PMMS_03_Work_Detail_History_Replace_List_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@machineIDx";
            sParams[0].Value = machineIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Work_Detail_History_Machine_Replace_PartList_Width = cls.fnGetDataGridWidth(dgv_Work_Detail_History_Machine_Replace_PartList);
            dgv_Work_Detail_History_Machine_Replace_PartList.DataSource = dt;

            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[0].Width =  8 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // STT
            //dgv_Work_Detail_History_Machine_Replace_PartList.Columns[1].Width = 10 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // [WoIDx]
            //dgv_Work_Detail_History_Machine_Replace_PartList.Columns[2].Width = 10 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // [RequestIDx]
            //dgv_Work_Detail_History_Machine_Replace_PartList.Columns[3].Width = 10 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // [SpareIDx]
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[4].Width = 21 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // worAdded
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[5].Width = 25 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // picName
            //dgv_Work_Detail_History_Machine_Replace_PartList.Columns[6].Width = 10 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // matIDx
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[7].Width = 30 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // matName
            //dgv_Work_Detail_History_Machine_Replace_PartList.Columns[8].Width = 18 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // matCode
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[9].Width = 8 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // matQty
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[10].Width = 8 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // Uom

            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[0].Visible = true;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[1].Visible = false;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[2].Visible = false;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[3].Visible = false;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[4].Visible = true;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[5].Visible = true;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[6].Visible = false;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[7].Visible = true;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[8].Visible = false;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[9].Visible = true;
            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[10].Visible = true;

            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Work_Detail_History_Machine_Replace_PartList, 11, 30);
        }

        public void init_Work_Detail_Process_State()
        {
            cbb_Work_Detail_Finish_Status.Items.Clear();
            cbb_Work_Detail_Finish_Status.Items.Add("Đã hoàn thành / Finish");
            cbb_Work_Detail_Finish_Status.Items.Add("Không chấp nhận / Reject");
            cbb_Work_Detail_Finish_Status.Items.Add("Chờ phân công / Waiting assigned");
            cbb_Work_Detail_Finish_Status.Items.Add("Đang sửa chữa / Repairing");
            cbb_Work_Detail_Finish_Status.Items.Add("Chờ vật tư / Waiting for spare parts");
            cbb_Work_Detail_Finish_Status.Items.Insert(0, "");
            cbb_Work_Detail_Finish_Status.SelectedIndex = 0;
        }

        public void init_Work_Detail_Process_Status()
        {
            if (_woIDx != "" && _woIDx != null)
            {

                DateTime makerApproveDate, repairApproveDate;
                makerApproveDate = (_makerApproveDate != "" && _makerApproveDate != null) ? Convert.ToDateTime(_makerApproveDate) : _dt;
                repairApproveDate = (_repairApprove == "1") ? Convert.ToDateTime(_repairApproveDate) : _dt;
                dtp_Work_Detail_Finish_Date.MinDate = new DateTime(makerApproveDate.Year, makerApproveDate.Month, makerApproveDate.Day);
                dtp_Work_Detail_Finish_Date.MaxDate = new DateTime(repairApproveDate.Year, repairApproveDate.Month, repairApproveDate.Day);

                string sql = "";

                lnk_Work_Detail_Finish_PicAdd.Enabled = (pnl_Work_Detail_Finish_Picture.BackgroundImage == null) ? true : false;
                string pmRepair = _repairApprove;
                int _pmRepair = (pmRepair != "" && pmRepair != null) ? Convert.ToInt32(pmRepair) : 0;
                cbb_Work_Detail_Finish_Status.SelectedIndex = _pmRepair;
                cbb_Work_Detail_Finish_Status.Enabled = true;
                //dtp_Work_Detail_Finish_Date.Enabled = true;
                btn_Work_Detail_Finish_Done.Enabled = true;
            }
            else
            {
                pnl_Work_Detail_Finish_Picture.BackgroundImage = null;
                lnk_Work_Detail_Finish_PicAdd.Enabled = false;
                lnk_Work_Detail_Finish_PicUpd.Enabled = false;
                lnk_Work_Detail_Finish_PicDel.Enabled = false;
                cbb_Work_Detail_Finish_Status.Enabled = false;
                dtp_Work_Detail_Finish_Date.Enabled = false;
                txt_Work_Detail_Finish_Note.Text = "";
                txt_Work_Detail_Finish_Note.Enabled = false;
                btn_Work_Detail_Finish_Save.Enabled = false;
                btn_Work_Detail_Finish_Done.Enabled = false;
            }
        }

        public void init_Work_Detail_Done()
        {

        }

        private void dgv_Work_Detail_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Work_Detail_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Work_Detail_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Work_Detail_List.Rows[e.RowIndex];

                string woIDx = row.Cells[1].Value.ToString();
                _woIDx = woIDx;

                string woPicIDx = "", woPicName = "", woPicLevel = "", woEquipmentIDx = "", woEquipmentName = "", woMachineIDx = "", woMachineName = "";
                string woMachineDesc = "", woOrderPriority = "", woDateFinish = "", woOrderNote = "", woOrderAdded = "", woOrderClosed = "", woClosedDate = "", woTakeTime = "";

                string woaIDx = "", woaWoIDx = "", woaMakerIDx = "", woaMakerName = "", woaMakerApprove = "", woaMakerApproveDate = "", woaManagerIDx = "", woaManagerName = "";
                string woaManagerApprove = "", woaManagerApproveNote = "", woaManagerApproveDate = "", woaRepairIDx = "", woaRepairName = "", woaRepairFinish = "", woaRepairFinishNote = "";
                string woaRepairFinishDate = "", woaConfirmIDx = "", woaConfirmName = "", woaConfirmClosed = "", woaConfirmClosedNote = "", woaConfirmCloseDate = "";

                string wopIDx = "", wopWoIDx = "", wopPicIDxBefore = "", wopPicNameBefore = "", wopImageBefore = "", wopImageBeforeDate = "";
                string wopPicIDxAfter = "", wopPicNameAfter = "", wopImageAfter = "", wopImageAfterDate = "";

                string worIDx = "", worCode = "", worDate = "", worPicIDx = "", worPicName = "", worPurpose = "", worPriority = "", worReason = "", worAdded = "";

                string sql = "PMMS_03_Work_Detail_Info_SelItem_V1o0_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@woIDx";
                sParams[0].Value = woIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //woIDx = ds.Tables[0].Rows[0][0].ToString();
                    woPicIDx = ds.Tables[0].Rows[0][1].ToString();
                    woPicName = ds.Tables[0].Rows[0][2].ToString();
                    woPicLevel = ds.Tables[0].Rows[0][3].ToString();
                    woEquipmentIDx = ds.Tables[0].Rows[0][4].ToString();
                    woEquipmentName = ds.Tables[0].Rows[0][5].ToString();
                    woMachineIDx = ds.Tables[0].Rows[0][6].ToString();
                    woMachineName = ds.Tables[0].Rows[0][7].ToString();
                    woMachineDesc = ds.Tables[0].Rows[0][8].ToString();
                    woOrderPriority = ds.Tables[0].Rows[0][9].ToString();
                    woDateFinish = ds.Tables[0].Rows[0][10].ToString();
                    woOrderNote = ds.Tables[0].Rows[0][11].ToString();
                    woOrderAdded = ds.Tables[0].Rows[0][12].ToString();
                    woOrderClosed = ds.Tables[0].Rows[0][13].ToString();
                    woClosedDate = ds.Tables[0].Rows[0][14].ToString();
                    woTakeTime = ds.Tables[0].Rows[0][15].ToString();

                    woaIDx = ds.Tables[0].Rows[0][16].ToString();
                    woaWoIDx = ds.Tables[0].Rows[0][17].ToString();
                    woaMakerIDx = ds.Tables[0].Rows[0][18].ToString();
                    woaMakerName = ds.Tables[0].Rows[0][19].ToString();
                    woaMakerApprove = ds.Tables[0].Rows[0][20].ToString();
                    woaMakerApproveDate = ds.Tables[0].Rows[0][21].ToString();
                    woaManagerIDx = ds.Tables[0].Rows[0][22].ToString();
                    woaManagerName = ds.Tables[0].Rows[0][23].ToString();
                    woaManagerApprove = ds.Tables[0].Rows[0][24].ToString();
                    woaManagerApproveNote = ds.Tables[0].Rows[0][25].ToString();
                    woaManagerApproveDate = ds.Tables[0].Rows[0][26].ToString();
                    woaRepairIDx = ds.Tables[0].Rows[0][27].ToString();
                    woaRepairName = ds.Tables[0].Rows[0][28].ToString();
                    woaRepairFinish = ds.Tables[0].Rows[0][29].ToString();
                    woaRepairFinishNote = ds.Tables[0].Rows[0][30].ToString();
                    woaRepairFinishDate = ds.Tables[0].Rows[0][31].ToString();
                    woaConfirmIDx = ds.Tables[0].Rows[0][31].ToString();
                    woaConfirmName = ds.Tables[0].Rows[0][33].ToString();
                    woaConfirmClosed = ds.Tables[0].Rows[0][34].ToString();
                    woaConfirmClosedNote = ds.Tables[0].Rows[0][35].ToString();
                    woaConfirmCloseDate = ds.Tables[0].Rows[0][36].ToString();

                    wopIDx = ds.Tables[0].Rows[0][37].ToString();
                    wopWoIDx = ds.Tables[0].Rows[0][38].ToString();
                    wopPicIDxBefore = ds.Tables[0].Rows[0][39].ToString();
                    wopPicNameBefore = ds.Tables[0].Rows[0][40].ToString();
                    wopImageBefore = ds.Tables[0].Rows[0][41].ToString();
                    wopImageBeforeDate = ds.Tables[0].Rows[0][42].ToString();
                    wopPicIDxAfter = ds.Tables[0].Rows[0][43].ToString();
                    wopPicNameAfter = ds.Tables[0].Rows[0][44].ToString();
                    wopImageAfter = ds.Tables[0].Rows[0][45].ToString();
                    wopImageAfterDate = ds.Tables[0].Rows[0][46].ToString();

                    worIDx = ds.Tables[0].Rows[0][47].ToString();
                    worCode = ds.Tables[0].Rows[0][48].ToString();
                    worDate = ds.Tables[0].Rows[0][49].ToString();
                    worPicIDx = ds.Tables[0].Rows[0][50].ToString();
                    worPicName = ds.Tables[0].Rows[0][51].ToString();
                    worPurpose = ds.Tables[0].Rows[0][52].ToString();
                    worPriority = ds.Tables[0].Rows[0][53].ToString();
                    worReason = ds.Tables[0].Rows[0][54].ToString();
                    worAdded = ds.Tables[0].Rows[0][55].ToString();

                    /*-----------------------------------------------------------------------------------------*/

                    //_woIDx = woIDx;
                    _manApprove = woaMakerApprove;
                    _picIDx = woaMakerIDx;
                    _picName = woaMakerName;
                    _picLevel = woPicLevel;
                    _equipIDx = woEquipmentIDx;
                    _equipName = woEquipmentName;
                    _machineIDx = woMachineIDx;
                    _machineName = woMachineName;
                    _machineDesc = woMachineDesc;
                    _priority = woOrderPriority;
                    _dateFinish = woDateFinish;
                    _orderNote = woOrderNote;
                    _added = woOrderAdded;
                    _makerIDx = woaMakerIDx;
                    _makerName = woaMakerName;
                    _makerApprove = woaMakerApprove;
                    _makerApproveDate = woaMakerApproveDate;
                    _managerIDx = woaManagerIDx;
                    _managerName = woaManagerName;
                    _managerApprove = woaManagerApprove;
                    _managerApproveNote = woaManagerApproveNote;
                    _managerApproveDate = woaManagerApproveDate;
                    _repairIDx = woaRepairIDx;
                    _repairName = woaRepairName;
                    _repairApprove = woaRepairFinish;
                    _repairApproveNote = woaRepairFinishNote;
                    _repairApproveDate = woaRepairFinishDate;
                    _confirmIDx = woaConfirmIDx;
                    _conFirmName = woaConfirmName;
                    _confirmApprove = woaConfirmClosed;
                    _confirmApproveNote = woaConfirmClosedNote;
                    _confirmApproveDate = woaConfirmCloseDate;


                    /*-----------------------------------------------------------------------------------------*/

                    lbl_Work_Detail_Info_Equipment.Text = woEquipmentName;
                    lbl_Work_Detail_Info_Machine.Text = woMachineName;
                    lbl_Work_Detail_Info_Priority.Text = (woOrderPriority == "1") ? "CAO" : (woOrderPriority == "2") ? "TB" : "THẤP";
                    lbl_Work_Detail_Info_DateCreate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(woOrderAdded));
                    lbl_Work_Detail_Info_DateFinish.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(woDateFinish));
                    lbl_Work_Detail_Info_Note.Text = woOrderNote;
                    lbl_Work_Detail_Info_Desc.Text = woMachineDesc;
                    lbl_Work_Detail_Info_Picture_Title.Text = woPicName.ToUpper() + " (" + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(woOrderAdded)) + ")";
                    if (wopImageBefore.Length > 0)
                    {
                        Bitmap bmpIssue;
                        MemoryStream msIssue;
                        msIssue = new MemoryStream((byte[])ds.Tables[0].Rows[0][41]);
                        bmpIssue = new Bitmap(msIssue);
                        pnl_Work_Detail_Info_Picture.BackgroundImage = bmpIssue;
                        pnl_Work_Detail_Info_Picture.BackgroundImageLayout = ImageLayout.Stretch;

                        bmpIssue = null;
                        msIssue = null;
                    }
                    else
                    {
                        pnl_Work_Detail_Info_Picture.BackgroundImage = null;
                    }

                    if (wopImageAfter.Length > 0)
                    {
                        Bitmap bmpSolve;
                        MemoryStream msSolve;
                        msSolve = new MemoryStream((byte[])ds.Tables[0].Rows[0][45]);
                        bmpSolve = new Bitmap(msSolve);
                        pnl_Work_Detail_Finish_Picture.BackgroundImage = bmpSolve;
                        pnl_Work_Detail_Finish_Picture.BackgroundImageLayout = ImageLayout.Stretch;

                        bmpSolve = null;
                        msSolve = null;
                    }
                    else
                    {
                        pnl_Work_Detail_Finish_Picture.BackgroundImage = null;
                    }

                    tabControl2.Enabled = true;
                    tabControl2.SelectedIndex = 0;

                    init_Work_Detail_Process_Status();
                    init_Work_Detail_History_Repair();
                }
                else
                {
                    tabControl2.Enabled = false;
                }
            }
        }

        private void dgv_Work_Detail_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cbb_Work_Detail_Finish_Status_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string statusMsg = "";
            int status = cbb_Work_Detail_Finish_Status.SelectedIndex;
            if (status != 0 && status != 3)
            {
                switch (status)
                {
                    case 1:
                        if (pnl_Work_Detail_Finish_Picture.BackgroundImage == null)
                        {
                            MessageBox.Show("Vui lòng thêm ảnh sau khi sửa chữa.");
                            btn_Work_Detail_Finish_Save.Enabled = false;
                        }
                        else
                        {
                            btn_Work_Detail_Finish_Save.Enabled = true;
                        }
                        statusMsg = "Đã hoàn thành việc sửa chữa cho máy " + _machineName;
                        dtp_Work_Detail_Finish_Date.Enabled = true;
                        txt_Work_Detail_Finish_Note.ReadOnly = true;
                        break;
                    case 2:
                        statusMsg = "Việc sửa chữa cho máy " + _machineName + " bị từ chối vì: ";
                        dtp_Work_Detail_Finish_Date.Enabled = false;
                        txt_Work_Detail_Finish_Note.ReadOnly = false;
                        txt_Work_Detail_Finish_Note.Focus();
                        btn_Work_Detail_Finish_Save.Enabled = true;
                        break;
                    case 4:
                        statusMsg = "Đang sửa chữa cho máy " + _machineName;
                        dtp_Work_Detail_Finish_Date.Enabled = false;
                        txt_Work_Detail_Finish_Note.ReadOnly = true;
                        btn_Work_Detail_Finish_Save.Enabled = true;
                        break;
                    case 5:
                        statusMsg = "Việc sửa chữa cho máy " + _machineName + " đang bị tạm dừng do: chờ mua vật tư thay thế";
                        dtp_Work_Detail_Finish_Date.Enabled = false;
                        txt_Work_Detail_Finish_Note.ReadOnly = false;
                        txt_Work_Detail_Finish_Note.Focus();
                        btn_Work_Detail_Finish_Save.Enabled = true;
                        break;
                }
                txt_Work_Detail_Finish_Note.Text = statusMsg;
                txt_Work_Detail_Finish_Note.Enabled = true;

                //btn_Work_Detail_Finish_Save.Enabled = (pnl_Work_Detail_Finish_Picture.BackgroundImage != null) ? true : false;
            }
            else
            {
                cbb_Work_Detail_Finish_Status.SelectedIndex = Convert.ToInt32(_repairApprove);
                txt_Work_Detail_Finish_Note.Text = "";
                txt_Work_Detail_Finish_Note.Enabled = false;
                btn_Work_Detail_Finish_Save.Enabled = false;
                statusMsg = "Vui lòng chọn lại tình trạng sửa chữa cho yêu cầu hiện tại.";
                MessageBox.Show(statusMsg);
            }
        }

        private void lnk_Work_Detail_Finish_PicAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //dialog.InitialDirectory = @"C:\";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.Title = "Chọn ảnh minh họa cho vật tư";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fleName = Path.GetFullPath(dialog.FileName);
                Image original = Image.FromFile(dialog.FileName);
                int BOXHEIGHT = 640;
                int BOXWIDTH = 640;
                float scaleHeight = (float)BOXHEIGHT / (float)original.Height;
                float scaleWidth = (float)BOXWIDTH / (float)original.Width;
                float scale = Math.Min(scaleHeight, scaleWidth);

                Bitmap resized = new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));

                //string resizedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Test\\";
                string resizedPath = cls.CreateFolderMissing(Application.StartupPath + "\\Temp\\");

                string _prefix = _dt.Year + "" + _dt.Month + "" + _dt.Day + "" + _dt.Hour + "" + _dt.Minute + "" + _dt.Second;
                //string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                string resizedName = cls.RemoveSpecialCharacters(Path.GetFileName(fleName.Replace(Path.GetExtension(fleName), null))) + "" + Path.GetExtension(fleName);

                string fullPath = resizedPath + "" + _prefix + "_" + resizedName.ToLower();
                _matImage = fullPath;

                pnl_Work_Detail_Finish_Picture.BackgroundImage = Image.FromFile(dialog.FileName);
                pnl_Work_Detail_Finish_Picture.BackgroundImageLayout = ((int)(original.Width) > (int)(panel1.Width) || (int)(original.Height) > (int)(panel1.Height)) ? ImageLayout.Stretch : ImageLayout.Center;

                resized.Save(fullPath);

                lnk_Work_Detail_Finish_PicAdd.Enabled = false;
                lnk_Work_Detail_Finish_PicUpd.Enabled = true;
                lnk_Work_Detail_Finish_PicDel.Enabled = true;
                _matNewImage = true;

                btn_Work_Detail_Finish_Save.Enabled = (cbb_Work_Detail_Finish_Status.SelectedIndex == 1) ? true : false;
            }
            else
            {
                lnk_Work_Detail_Finish_PicAdd.Enabled = true;
                lnk_Work_Detail_Finish_PicUpd.Enabled = false;
                lnk_Work_Detail_Finish_PicDel.Enabled = false;
                _matNewImage = false;

                btn_Work_Detail_Finish_Save.Enabled = false;
            }
        }

        private void btn_Work_Detail_Finish_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string woIDx = _woIDx;
                    string repairIDx = _repairIDx;
                    string repairName = _repairName;
                    string repairFinish = cbb_Work_Detail_Finish_Status.SelectedIndex.ToString();
                    DateTime repairDate = dtp_Work_Detail_Finish_Date.Value;
                    DateTime repairFinishDate = new DateTime(repairDate.Year, repairDate.Month, repairDate.Day, _dt.Hour, _dt.Minute, _dt.Second, _dt.Millisecond);
                    string repairFinishNote = txt_Work_Detail_Finish_Note.Text.Trim();

                    string imgPath = (repairFinish == "1") ? _matImage : Application.StartupPath + "\\No_Image_Available.jpg";
                    Bitmap bmp = new Bitmap(imgPath);
                    FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
                    byte[] bimage = new byte[fs.Length];
                    fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                    fs.Close();

                    string sql = "PMMS_03_Work_Detail_Status_UpdItem_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[7]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@woIDx";
                    sParams[0].Value = woIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.TinyInt;
                    sParams[1].ParameterName = "@repairFinish";
                    sParams[1].Value = repairFinish;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.NVarChar;
                    sParams[2].ParameterName = "@repairNote";
                    sParams[2].Value = repairFinishNote;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.DateTime;
                    sParams[3].ParameterName = "@repairDate";
                    sParams[3].Value = repairFinishDate;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.Int;
                    sParams[4].ParameterName = "@repairIDx";
                    sParams[4].Value = repairIDx;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.NVarChar;
                    sParams[5].ParameterName = "@repairName";
                    sParams[5].Value = repairName;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.Image;
                    sParams[6].ParameterName = "@image";
                    sParams[6].Value = bimage;

                    cls.fnUpdDel(sql, sParams);
                    bmp = null;

                    _msgText = "Cập nhật tình trạng sửa chữa thành công.";
                    _msgType = 1;
                }
                catch (SqlException sqlEx)
                {
                    _msgText = "Có lỗi dữ liệu phát sinh, vui lòng báo lại quản trị hệ thống.";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    _msgText = "Có lỗi phát sinh, vui lòng báo lại quản trị hệ thống.";
                    _msgType = 2;
                }
                finally
                {
                    init_Work_Detail_List();
                    init_Work_Detail_Done();

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }

            }
        }


        #endregion
    }
}
