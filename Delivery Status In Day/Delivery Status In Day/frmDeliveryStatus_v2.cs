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
    public partial class frmDeliveryStatus_v2 : Form
    {
        public string _orderID = "";
        public string _orderCode = "";
        public string _orderDate = "";
        public string _prodID = "";
        public string _prodCode = "";
        public string _prodName = "";
        public int _dgvOrderListRowSelected = 0;
        public int _nOrder;
        public int _dgvOrderListWidth;
        public int _dgvOrderDetailWidth;

        private int rowOrdersIndex = 0;


        public frmDeliveryStatus_v2()
        {
            InitializeComponent();
        }

        private void frmDeliveryStatus_v2_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            _dgvOrderListWidth = cls.fnGetDataGridWidth(dgvOrderList);
            _dgvOrderDetailWidth = cls.fnGetDataGridWidth(dgvOrderDetail);
            fnBindOrderList();

        }

        public void fnGetdate()
        {
            lblDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
            lblDateTime.ForeColor = (check.IsConnectedToInternet()) ? Color.Black : Color.Red;
        }

        public void fnBindOrderList()
        {
            string sql = "V2o1_BASE_Delivery_Status_Order_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            dgvOrderList.DataSource = dt;
            dgvOrderList.Refresh();

            _dgvOrderListWidth = cls.fnGetDataGridWidth(dgvOrderList);

            dgvOrderList.Columns[0].Width = 20 * _dgvOrderListWidth / 100;    // doId
            dgvOrderList.Columns[1].Width = 50 * _dgvOrderListWidth / 100;    // doCode
            dgvOrderList.Columns[2].Width = 30 * _dgvOrderListWidth / 100;    // doDate

            dgvOrderList.Columns[0].Visible = true;
            dgvOrderList.Columns[1].Visible = true;
            dgvOrderList.Columns[2].Visible = true;

            cls.fnFormatDatagridview(dgvOrderList, 12);
        }

        private void dgvOrderList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvOrderList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvOrderList.Rows[e.RowIndex];

            string idx = row.Cells[0].Value.ToString();
            string code = row.Cells[1].Value.ToString();
            string date = row.Cells[2].Value.ToString();

            _orderID = idx;
            _orderCode = code;
            _orderDate = date;

            fnBindOrderDetail();
        }

        private void dgvOrderList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvOrderList.Rows[e.RowIndex].Selected = true;
                rowOrdersIndex = e.RowIndex;
                dgvOrderList.CurrentCell = dgvOrderList.Rows[e.RowIndex].Cells[1];
                contextMenuStrip1.Show(this.dgvOrderList, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void refreshThisListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnBindOrderList();
        }

        public void fnBindOrderDetail()
        {
            string orderId = _orderID;

            string sql = "V2o1_BASE_Delivery_Status_Order_Detail2_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@orderId";
            sParams[0].Value = orderId;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    string remain = dr[12].ToString();
            //    dr[13] = (remain == "0") ? true : false;
            //}

            dgvOrderDetail.DataSource = dt;
            dgvOrderDetail.Refresh();

            _dgvOrderDetailWidth = cls.fnGetDataGridWidth(dgvOrderDetail);

            //dgvOrderDetail.Columns[0].Width = 20 * _dgvOrderDetailWidth / 100;    // idx
            //dgvOrderDetail.Columns[1].Width = 50 * _dgvOrderDetailWidth / 100;    // doID
            //dgvOrderDetail.Columns[2].Width = 20 * _dgvOrderDetailWidth / 100;    // partID
            dgvOrderDetail.Columns[3].Width = 20 * _dgvOrderDetailWidth / 100;    // partname
            dgvOrderDetail.Columns[4].Width = 10 * _dgvOrderDetailWidth / 100;    // partcode
            dgvOrderDetail.Columns[5].Width = 5 * _dgvOrderDetailWidth / 100;    // shipTo
            dgvOrderDetail.Columns[6].Width = 5 * _dgvOrderDetailWidth / 100;    // doQuantity
            dgvOrderDetail.Columns[7].Width = 5 * _dgvOrderDetailWidth / 100;    // doUnit
            dgvOrderDetail.Columns[8].Width = 5 * _dgvOrderDetailWidth / 100;    // boxquantity/BASE_BoxesOutStockManagement
            dgvOrderDetail.Columns[9].Width = 5 * _dgvOrderDetailWidth / 100;    // doStockLG
            dgvOrderDetail.Columns[10].Width = 5 * _dgvOrderDetailWidth / 100;    // quantity/BASE_Inventory
            dgvOrderDetail.Columns[11].Width = 5 * _dgvOrderDetailWidth / 100;    // doPlanToday
            //dgvOrderDetail.Columns[12].Width = 5 * _dgvOrderDetailWidth / 100;    // doPlanTomorrow
            dgvOrderDetail.Columns[13].Width = 5 * _dgvOrderDetailWidth / 100;    // [Remain]
            dgvOrderDetail.Columns[14].Width = 10 * _dgvOrderDetailWidth / 100;    // outstockfinishdate
            dgvOrderDetail.Columns[15].Width = 5 * _dgvOrderDetailWidth / 100;    // doFinish
            dgvOrderDetail.Columns[16].Width = 15 * _dgvOrderDetailWidth / 100;    // finishNote

            dgvOrderDetail.Columns[0].Visible = false;
            dgvOrderDetail.Columns[1].Visible = false;
            dgvOrderDetail.Columns[2].Visible = false;
            dgvOrderDetail.Columns[3].Visible = true;
            dgvOrderDetail.Columns[4].Visible = true;
            dgvOrderDetail.Columns[5].Visible = true;
            dgvOrderDetail.Columns[6].Visible = true;
            dgvOrderDetail.Columns[7].Visible = true;
            dgvOrderDetail.Columns[8].Visible = true;
            dgvOrderDetail.Columns[9].Visible = true;
            dgvOrderDetail.Columns[10].Visible = true;
            dgvOrderDetail.Columns[11].Visible = true;
            dgvOrderDetail.Columns[12].Visible = false;
            dgvOrderDetail.Columns[13].Visible = true;
            dgvOrderDetail.Columns[14].Visible = true;
            dgvOrderDetail.Columns[15].Visible = true;
            dgvOrderDetail.Columns[16].Visible = true;

            dgvOrderDetail.Columns[14].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgvOrderDetail, 12);

            foreach (DataGridViewRow row in dgvOrderDetail.Rows)
            {
                string idx = "", dId = "", pId = "";
                string name = "", code = "", ship = "";
                string doQty = "", doUnit = "", scanOut = "", outstock = "";
                string stockLG = "", stockDA = "", planToday = "", planTomorrow = "";
                string doRemain = "", lastOut = "", doFinish = "", finishNote = "";

                int _doQty = 0, _scanOut = 0, _outstock = 0, _stockLG = 0, _stockDA = 0;
                int _planToday = 0, _planTomorrow = 0, _doRemain = 0;

                idx = row.Cells[0].Value.ToString();
                dId = row.Cells[1].Value.ToString();
                pId = row.Cells[2].Value.ToString();
                name = row.Cells[3].Value.ToString();
                code = row.Cells[4].Value.ToString();
                ship = row.Cells[5].Value.ToString();
                doQty = row.Cells[6].Value.ToString();
                doUnit = row.Cells[7].Value.ToString();
                scanOut = row.Cells[8].Value.ToString();
                stockLG = row.Cells[9].Value.ToString();
                stockDA = row.Cells[10].Value.ToString();
                planToday = row.Cells[11].Value.ToString();
                planTomorrow = row.Cells[12].Value.ToString();
                doRemain = row.Cells[13].Value.ToString();
                lastOut = row.Cells[14].Value.ToString();
                doFinish = row.Cells[15].Value.ToString();
                finishNote = row.Cells[16].Value.ToString();

                _scanOut = (scanOut != "") ? Convert.ToInt32(scanOut) : 0;
                _doQty = (doQty != "") ? Convert.ToInt32(doQty) : 0;
                _planToday = (planToday != "") ? Convert.ToInt32(planToday) : 0;

                //row.Cells[8].Value = _scanOut;
                //row.Cells[13].Value = (_doQty - _scanOut);

                if (doFinish.ToLower() == "true")
                {
                    row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (scanOut == "" || scanOut == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (scanOut == doQty)
                {
                    row.Cells[15].Value = "True";
                    row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void dgvOrderDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //foreach (DataGridViewRow row in dgvOrderDetail.Rows)
            //{
            //    string idx = "", dId = "", pId = "";
            //    string name = "", code = "", ship = "";
            //    string doQty = "", doUnit = "", scanOut = "", outstock = "";
            //    string stockLG = "", stockDA = "", planToday = "", planTomorrow = "";
            //    string doRemain = "", lastOut = "", doFinish = "", finishNote = "";

            //    int _doQty = 0, _scanOut = 0, _outstock = 0, _stockLG = 0, _stockDA = 0;
            //    int _planToday = 0, _planTomorrow = 0, _doRemain = 0;

            //    idx = row.Cells[0].Value.ToString();
            //    dId = row.Cells[1].Value.ToString();
            //    pId = row.Cells[2].Value.ToString();
            //    name = row.Cells[3].Value.ToString();
            //    code = row.Cells[4].Value.ToString();
            //    ship = row.Cells[5].Value.ToString();
            //    doQty = row.Cells[6].Value.ToString();
            //    doUnit = row.Cells[7].Value.ToString();
            //    scanOut = row.Cells[8].Value.ToString();
            //    stockLG = row.Cells[9].Value.ToString();
            //    stockDA = row.Cells[10].Value.ToString();
            //    planToday = row.Cells[11].Value.ToString();
            //    planTomorrow = row.Cells[12].Value.ToString();
            //    doRemain = row.Cells[13].Value.ToString();
            //    lastOut = row.Cells[14].Value.ToString();
            //    doFinish = row.Cells[15].Value.ToString();
            //    finishNote = row.Cells[16].Value.ToString();

            //    _scanOut = (scanOut != "") ? Convert.ToInt32(scanOut) : 0;
            //    _doQty = (doQty != "") ? Convert.ToInt32(doQty) : 0;
            //    _planToday = (planToday != "") ? Convert.ToInt32(planToday) : 0;

            //    //row.Cells[8].Value = _scanOut;
            //    //row.Cells[13].Value = (_doQty - _scanOut);

            //    if (doFinish.ToLower() == "true")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.DodgerBlue;
            //        row.DefaultCellStyle.ForeColor = Color.White;
            //    }
            //    else if (scanOut == "" || scanOut == "0")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Gold;
            //        row.DefaultCellStyle.ForeColor = Color.Black;
            //    }
            //    else if(scanOut == doQty)
            //    {
            //        row.Cells[15].Value = "True";
            //        row.DefaultCellStyle.BackColor = Color.DodgerBlue;
            //        row.DefaultCellStyle.ForeColor = Color.White;
            //    }
            //}
        }

        private void dgvOrderDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvOrderDetail, e);
        }

        private void dgvOrderDetail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
