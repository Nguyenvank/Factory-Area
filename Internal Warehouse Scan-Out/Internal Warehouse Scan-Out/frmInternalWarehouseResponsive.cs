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
    public partial class frmInternalWarehouseResponsive : Form
    {
        public int _dgvInl_List_Width;
        public int _dgvBind_List_Width;

        public string _idx = "", _prodIDx = "", _prodName = "", _prodCode = "", _boxIDx = "", _boxCode = "", _boxLocate = "";
        public string _boxLOT = "", _boxQty = "", _boxInDate = "", _boxMergeSplit = "", _purpose = "", _boxReason = "", _requestDate = "";
        public string _act = "add";
        public string _range = "2";

        public DateTime _dt;

        public string _msgText = "";
        public int _msgType = 0;

        public frmInternalWarehouseResponsive()
        {
            InitializeComponent();
        }

        private void frmInternalWarehouseResponsive_Load(object sender, EventArgs e)
        {
            _dgvInl_List_Width = cls.fnGetDataGridWidth(dgvInl_List);
            _dgvBind_List_Width = cls.fnGetDataGridWidth(dgvBind_List);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            initInl();
            initBind();
        }

        public void fnGetdate()
        {
            _dt = DateTime.Now;
            cls.fnSetDateTime(tssDateTime);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initInl();
                    break;
                case 1:
                    break;
            }
        }

        #region INTERNAL REQUEST


        public void initInl()
        {
            initInl_List();

            lblInl_Packing.Text = "N/A";
            lblInl_Name.Text = "N/A";
            lblInl_Code.Text = "N/A";
            lblInl_InDate.Text = "N/A";
            lblInl_Qty.Text = "0";
            lblInl_LOT.Text = "N/A";
            lblInl_Locate.Text = "N/A";
            lblInl_Reason.Text = "";

            initInl_Response();
            lblInl_Status.Text = "N/A";
            txtInl_Remark.Text = "";
        }

        public void initInl_List()
        {
            string sql = "V2o1_BASE_Internal_Warehouse_Responsive_List_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvInl_List_Width = cls.fnGetDataGridWidth(dgvInl_List);
            dgvInl_List.DataSource = dt;

            //dgvInl_List.Columns[0].Width = 25 * _dgvInl_List_Width / 100;    // idx
            //dgvInl_List.Columns[1].Width = 25 * _dgvInl_List_Width / 100;    // prodIDx
            //dgvInl_List.Columns[2].Width = 25 * _dgvInl_List_Width / 100;    // prodName
            //dgvInl_List.Columns[3].Width = 25 * _dgvInl_List_Width / 100;    // prodCode
            //dgvInl_List.Columns[4].Width = 25 * _dgvInl_List_Width / 100;    // boxIDx
            dgvInl_List.Columns[5].Width = 65 * _dgvInl_List_Width / 100;    // boxCode
            //dgvInl_List.Columns[6].Width = 25 * _dgvInl_List_Width / 100;    // boxLocate
            //dgvInl_List.Columns[7].Width = 25 * _dgvInl_List_Width / 100;    // boxLOT
            dgvInl_List.Columns[8].Width = 12 * _dgvInl_List_Width / 100;    // boxQty
            //dgvInl_List.Columns[9].Width = 25 * _dgvInl_List_Width / 100;    // boxInDate
            //dgvInl_List.Columns[10].Width = 25 * _dgvInl_List_Width / 100;    // boxMergeSplit
            dgvInl_List.Columns[11].Width = 23 * _dgvInl_List_Width / 100;    // boxMergeSplit
            //dgvInl_List.Columns[12].Width = 25 * _dgvInl_List_Width / 100;    // boxReason
            //dgvInl_List.Columns[13].Width = 25 * _dgvInl_List_Width / 100;    // requestDate

            dgvInl_List.Columns[0].Visible = false;
            dgvInl_List.Columns[1].Visible = false;
            dgvInl_List.Columns[2].Visible = false;
            dgvInl_List.Columns[3].Visible = false;
            dgvInl_List.Columns[4].Visible = false;
            dgvInl_List.Columns[5].Visible = true;
            dgvInl_List.Columns[6].Visible = false;
            dgvInl_List.Columns[7].Visible = false;
            dgvInl_List.Columns[8].Visible = true;
            dgvInl_List.Columns[9].Visible = false;
            dgvInl_List.Columns[10].Visible = false;
            dgvInl_List.Columns[11].Visible = true;
            dgvInl_List.Columns[12].Visible = false;
            dgvInl_List.Columns[13].Visible = false;

            cls.fnFormatDatagridview(dgvInl_List, 12, 30);
        }

        public void initInl_Response()
        {
            cbbInl_Response.Items.Clear();
            cbbInl_Response.Items.Add("Đồng ý xuất kho");
            cbbInl_Response.Items.Insert(0, "");
            cbbInl_Response.SelectedIndex = 0;
        }

        private void dgvInl_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvInl_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvInl_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgvInl_List.Rows[e.RowIndex];

                string idx = row.Cells[0].Value.ToString();
                string prodIDx = row.Cells[1].Value.ToString();
                string prodName = row.Cells[2].Value.ToString();
                string prodCode = row.Cells[3].Value.ToString();
                string boxIDx = row.Cells[4].Value.ToString();
                string boxCode = row.Cells[5].Value.ToString();
                string boxLocate = row.Cells[6].Value.ToString();
                string boxLOT = row.Cells[7].Value.ToString();
                string boxQty = row.Cells[8].Value.ToString();
                string boxInDate = row.Cells[9].Value.ToString();
                string boxMergeSplit = row.Cells[10].Value.ToString();
                string purpose = row.Cells[11].Value.ToString();
                string boxReason = row.Cells[12].Value.ToString();
                string requestDate = row.Cells[13].Value.ToString();

                _idx = idx;
                _prodIDx = prodIDx;
                _prodName = prodName;
                _prodCode = prodCode;
                _boxIDx = boxIDx;
                _boxCode = boxCode;
                _boxLocate = boxLocate;
                _boxLOT = boxLOT;
                _boxQty = boxQty;
                _boxInDate = boxInDate;
                _boxMergeSplit = boxMergeSplit;
                _purpose = purpose;
                _boxReason = boxReason;
                _requestDate = requestDate;

                lblInl_Packing.Text = boxCode;
                lblInl_Name.Text = prodName;
                lblInl_Code.Text = prodCode;
                lblInl_InDate.Text = boxInDate;
                lblInl_Qty.Text = boxQty;
                lblInl_LOT.Text = boxLOT;
                lblInl_Locate.Text = boxLocate;
                lblInl_Reason.Text = purpose.ToUpper() + ": " + boxReason;

                cbbInl_Response.Enabled = true;
                btnInl_Done.Enabled = true;
            }
        }

        private void dgvInl_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cbbInl_Response_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbInl_Response.SelectedIndex > 0)
            {
                lblInl_Status.Text = "Đã xuất kho";
                txtInl_Remark.Enabled = true;
                txtInl_Remark.Text = "Đã xuất kho theo yêu cầu";
            }
            else
            {
                lblInl_Status.Text = "N/A";
                txtInl_Remark.Enabled = false;
                txtInl_Remark.Text = "";
            }
        }

        private void txtInl_Remark_TextChanged(object sender, EventArgs e)
        {
            btnInl_Save.Enabled = (txtInl_Remark.Text.Length > 0) ? true : false;
        }

        private void btnInl_Done_Click(object sender, EventArgs e)
        {
            fnInl_Done();
        }

        public void fnInl_Done()
        {
            dgvInl_List.ClearSelection();

            _idx = "";
            _prodIDx = "";
            _prodName = "";
            _prodCode = "";
            _boxIDx = "";
            _boxCode = "";
            _boxLocate = "";
            _boxLOT = "";
            _boxQty = "";
            _boxInDate = "";
            _boxMergeSplit = "";
            _purpose = "";
            _boxReason = "";
            _requestDate = "";

            lblInl_Packing.Text = "N/A";
            lblInl_Name.Text = "N/A";
            lblInl_Code.Text = "N/A";
            lblInl_InDate.Text = "N/A";
            lblInl_Qty.Text = "0";
            lblInl_LOT.Text = "N/A";
            lblInl_Locate.Text = "N/A";
            lblInl_Reason.Text = "";

            initInl_Response();
            lblInl_Status.Text = "N/A";
            txtInl_Remark.Text = "";

            btnInl_Save.Enabled = false;
            btnInl_Done.Enabled = false;
        }

        private void btnInl_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                switch (_act.ToLower())
                {
                    case "add":
                        fnInl_Add();
                        break;
                    case "upd":
                        break;
                    case "del":
                        break;
                }
                initInl_List();
                fnInl_Done();
                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }

        public void fnInl_Add()
        {
            string idx = _idx;
            string prodIDx = _prodIDx;
            string prodName = _prodName;
            string prodCode = _prodCode;
            string boxIDx = _boxIDx;
            string boxCode = _boxCode;
            string boxLocate = _boxLocate;
            string boxLOT = _boxLOT;
            string boxQty = _boxQty;
            string boxInDate = _boxInDate;
            string boxMergeSplit = _boxMergeSplit;
            string purpose = _purpose;
            string boxReason = _boxReason;
            string requestDate = _requestDate;

            string mmtResponse = (cbbInl_Response.SelectedIndex == 1) ? "True" : "False";
            string mmtRemark = txtInl_Remark.Text.Trim();

            string sql = "V2o1_BASE_Internal_Warehouse_Responsive_AddItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@idx";
            sParams[0].Value = idx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Bit;
            sParams[1].ParameterName = "@mmtScanOut";
            sParams[1].Value = mmtResponse;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.NVarChar;
            sParams[2].ParameterName = "@mmtRemark";
            sParams[2].Value = mmtRemark;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Phản hồi thành công";
            _msgType = 1;
        }



        #endregion


        #region BINDING DATA

        public void initBind()
        {
            initBind_List();
            fnLinkColor();
        }

        public void initBind_List()
        {
            string range = _range;
            string sql = "V2o1_BASE_Internal_Warehouse_Responsive_BindList_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@range";
            sParams[0].Value = range;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvBind_List_Width = cls.fnGetDataGridWidth(dgvBind_List);
            dgvBind_List.DataSource = dt;

            //dgvBind_List.Columns[0].Width = 10 * _dgvBind_List_Width / 100;    // [idx]
            //dgvBind_List.Columns[1].Width = 10 * _dgvBind_List_Width / 100;    // [prodIDx]
            dgvBind_List.Columns[2].Width = 20 * _dgvBind_List_Width / 100;    // [boxCode]
            dgvBind_List.Columns[3].Width = 15 * _dgvBind_List_Width / 100;    // [prodName]
            dgvBind_List.Columns[4].Width = 14 * _dgvBind_List_Width / 100;    // [prodCode]
            //dgvBind_List.Columns[5].Width = 10 * _dgvBind_List_Width / 100;    // [boxIDx]
            dgvBind_List.Columns[6].Width = 8 * _dgvBind_List_Width / 100;    // [boxLocate]
            dgvBind_List.Columns[7].Width = 8 * _dgvBind_List_Width / 100;    // [boxLOT]
            dgvBind_List.Columns[8].Width = 5 * _dgvBind_List_Width / 100;    // [boxQty]
            dgvBind_List.Columns[9].Width = 12 * _dgvBind_List_Width / 100;    // [boxInDate]
            dgvBind_List.Columns[10].Width = 10 * _dgvBind_List_Width / 100;    // [boxMergeSplit]
            //dgvBind_List.Columns[11].Width = 10 * _dgvBind_List_Width / 100;    // [boxReason]
            //dgvBind_List.Columns[12].Width = 10 * _dgvBind_List_Width / 100;    // [requestDate]
            //dgvBind_List.Columns[13].Width = 10 * _dgvBind_List_Width / 100;    // [boxStatus]
            dgvBind_List.Columns[14].Width = 8 * _dgvBind_List_Width / 100;    // [boxDone]

            dgvBind_List.Columns[0].Visible = false;
            dgvBind_List.Columns[1].Visible = false;
            dgvBind_List.Columns[2].Visible = true;
            dgvBind_List.Columns[3].Visible = true;
            dgvBind_List.Columns[4].Visible = true;
            dgvBind_List.Columns[5].Visible = false;
            dgvBind_List.Columns[6].Visible = true;
            dgvBind_List.Columns[7].Visible = true;
            dgvBind_List.Columns[8].Visible = true;
            dgvBind_List.Columns[9].Visible = true;
            dgvBind_List.Columns[10].Visible = true;
            dgvBind_List.Columns[11].Visible = false;
            dgvBind_List.Columns[12].Visible = false;
            dgvBind_List.Columns[13].Visible = false;
            dgvBind_List.Columns[14].Visible = true;

            dgvBind_List.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvBind_List.Columns[14].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridviewWhite(dgvBind_List, 11, 30);
        }

        public void fnLinkColor()
        {
            switch (_range)
            {
                case "1":
                    lnkBind_01.LinkColor = Color.Red;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "2":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Red;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "3":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Red;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "4":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Red;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "5":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Red;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "6":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Red;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "7":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Red;
                    lnkBind_08.LinkColor = Color.Blue;
                    break;
                case "8":
                    lnkBind_01.LinkColor = Color.Blue;
                    lnkBind_02.LinkColor = Color.Blue;
                    lnkBind_03.LinkColor = Color.Blue;
                    lnkBind_04.LinkColor = Color.Blue;
                    lnkBind_05.LinkColor = Color.Blue;
                    lnkBind_06.LinkColor = Color.Blue;
                    lnkBind_07.LinkColor = Color.Blue;
                    lnkBind_08.LinkColor = Color.Red;
                    break;
            }
        }

        private void lnkBind_01_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "1";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_02_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "2";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_03_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "3";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_04_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "4";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_05_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "5";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_06_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "6";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_07_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "7";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkBind_08_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "8";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void dgvBind_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvBind_List.ClearSelection();
            foreach (DataGridViewRow row in dgvBind_List.Rows)
            {
                string status = row.Cells[13].Value.ToString();
                if (status.ToLower() == "true")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }

        private void dgvBind_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvBind_List.ClearSelection();
                cls.fnDatagridClickCell(dgvBind_List, e);
            }
        }

        private void dgvBind_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }



        #endregion
    }
}
