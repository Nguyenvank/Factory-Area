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
    public partial class frmPM_01WorkOrders_WO_Details : Form
    {
        public int _dgv_dgv_Work_Detail_History_Machine_Repair_List_Width;
        public int _dgv_Work_Detail_History_Machine_Replace_PartList_Width;

        public string _woIDx = "", _manApprove = "";
        public string _picIDx = "", _picName = "", _picLevel = "";
        public string _equipIDx = "", _equipName = "", _machineIDx = "", _machineName = "", _machineDesc = "";
        public string _priority = "", _dateFinish = "", _orderNote = "", _added = "";
        public string _makerIDx = "", _makerName = "", _makerApprove = "", _makerApproveDate = "";
        public string _managerIDx = "", _managerName = "", _managerApprove = "", _managerApproveNote = "", _managerApproveDate = "";
        public string _repairIDx = "", _repairName = "", _repairApprove = "", _repairApproveNote = "", _repairApproveDate = "";
        public string _confirmIDx = "", _conFirmName = "", _confirmApprove = "", _confirmApproveNote = "", _confirmApproveDate = "";


        public frmPM_01WorkOrders_WO_Details(string woIDx)
        {
            InitializeComponent();

            _woIDx = woIDx;
            init();
        }

        private void frmPM_01WorkOrders_WO_Details_Load(object sender, EventArgs e)
        {
            
        }

        public void init()
        {
            init_Work_Detail();
            init_Work_Detail_History_Repair();
            init_Work_Detail_History_Replace();
        }

        public void init_Work_Detail()
        {
            string woIDx = _woIDx;
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

                lbl_WO_Detail_Equipment.Text = woEquipmentName;
                lbl_WO_Detail_Machine.Text = woMachineName;
                lbl_WO_Detail_Priority.Text = (woOrderPriority == "1") ? "CAO" : (woOrderPriority == "2") ? "TB" : "THẤP";
                lbl_WO_Detail_Date_Finish.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(woDateFinish));
                lbl_WO_Detail_Desc.Text = woMachineDesc;
                lbl_WO_Detail_Issue.Text = "Phát hành bởi " + woPicName.ToUpper() + " (" + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(woOrderAdded)) + ")";
                if (wopImageBefore.Length > 0)
                {
                    Bitmap bmpIssue;
                    MemoryStream msIssue;
                    msIssue = new MemoryStream((byte[])ds.Tables[0].Rows[0][41]);
                    bmpIssue = new Bitmap(msIssue);
                    pnl_WO_Detail_Issue.BackgroundImage = bmpIssue;
                    pnl_WO_Detail_Issue.BackgroundImageLayout = ImageLayout.Stretch;

                    bmpIssue = null;
                    msIssue = null;
                }
                else
                {
                    pnl_WO_Detail_Issue.BackgroundImage = null;
                }

                if (woaRepairFinish == "1")
                {
                    btn_WO_Detail_Closed_Issue.BackColor = Color.LightGreen;
                    btn_WO_Detail_Closed_Issue.Enabled = true;
                    btn_WO_Detail_Closed_Issue.Text = "XÁC NHẬN ĐÓNG YÊU CẦU SỬA CHỮA";
                }
                else
                {
                    btn_WO_Detail_Closed_Issue.BackColor = Color.LightGray;
                    btn_WO_Detail_Closed_Issue.Enabled = false;
                    btn_WO_Detail_Closed_Issue.Text = "CHƯA ĐÓNG ĐƯỢC YÊU CẦU DO TRẠNG THÁI CHƯA HOÀN THÀNH";
                }


                string labelSolve = "";
                switch (woaRepairFinish)
                {
                    case "0":
                        labelSolve = "";
                        break;
                    case "1":
                        labelSolve = "Hoàn thành bởi " + woaRepairName.ToUpper() + " (" + String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(woaRepairFinishDate)) + ")";
                        break;
                    case "2":
                        labelSolve = "Đã bị từ chối sửa chữa";
                        break;
                    case "3":
                        labelSolve = "Yêu cầu đang chờ phân công công việc";
                        break;
                    case "4":
                        labelSolve = "Yêu cầu đang được tiến hành sửa chữa";
                        break;
                    case "5":
                        labelSolve = "Công việc đang tạm dừng để chờ mua vật tư thay thế";
                        break;
                }
                lbl_WO_Detail_Solve.Text = labelSolve;

                if (wopImageAfter.Length > 0)
                {
                    Bitmap bmpSolve;
                    MemoryStream msSolve;
                    msSolve = new MemoryStream((byte[])ds.Tables[0].Rows[0][45]);
                    bmpSolve = new Bitmap(msSolve);
                    pnl_WO_Detail_Solve.BackgroundImage = bmpSolve;
                    pnl_WO_Detail_Solve.BackgroundImageLayout = ImageLayout.Stretch;

                    bmpSolve = null;
                    msSolve = null;
                }
                else
                {
                    pnl_WO_Detail_Solve.BackgroundImage = null;
                }

                tabControl1.Enabled = true;
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                tabControl1.Enabled = false;
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

            dgv_Work_Detail_History_Machine_Replace_PartList.Columns[0].Width = 8 * _dgv_Work_Detail_History_Machine_Replace_PartList_Width / 100;    // STT
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

    }
}
