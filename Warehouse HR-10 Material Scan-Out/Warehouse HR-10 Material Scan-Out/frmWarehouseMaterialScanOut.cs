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
    public partial class frmWarehouseMaterialScanOut : Form
    {
        public string _orderIdx = "0";
        public string _matIdx = "0";
        public string _packIdx = "0";
        public string _inventoryIdx = "0";

        public string _orderQty = "";
        public string _orderUnit = "";
        public string _equivQty = "";
        public string _equivUnit = "";

        public string _partcode = "";
        public string _quantity = "";
        public string _locateId = "";
        public string _location = "";


        public int _dgvOrdersWidth;
        public int _dgvListWidth;
        public int _dgvScanWidth;
        public int _dgvInventoryWidth;

        private int rowOrderIndex = 0;
        private int rowListIndex = 0;
        private int rowScanIndex = 0;
        private int rowInventoryIndex = 0;

        public frmWarehouseMaterialScanOut()
        {
            InitializeComponent();
        }

        private void frmWarehouseMaterialScanOut_Load(object sender, EventArgs e)
        {
            _dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders) - 20;
            _dgvListWidth = cls.fnGetDataGridWidth(dgvList);
            _dgvScanWidth = cls.fnGetDataGridWidth(dgvScan);
            _dgvInventoryWidth = cls.fnGetDataGridWidth(dgvInventory);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //_dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders) - 20;
            //_dgvListWidth = cls.fnGetDataGridWidth(dgvList);
            //_dgvScanWidth = cls.fnGetDataGridWidth(dgvScan);
            //_dgvInventoryWidth = cls.fnGetDataGridWidth(dgvInventory);

            fnGetdate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //fnBindOrder();
        }

        public void init()
        {
            fnGetdate();

            //dgvOrders.DataSource = "";
            dgvInventory.DataSource = "";
            dgvList.DataSource = "";
            dgvScan.DataSource = "";

            txtPackcode.Text = "";
            txtPackcode.Enabled = false;
            lblMessage.Text = "Message";

            lblItemTotal.Text = "0";
            lblOrderTotal.Text = "0";
            lblScanPercent.Text = "0%";
            lblItemPercent.Text = "0%";
            lblScanTotal.Text = "0";

            lblPartCode.Text = "-N/A-";
            lblPartInvent.Text = "-";
            lblPartUnit.Text = "-";

            fnBindOrder();
            dgvOrders.ClearSelection();
        }

        public void fnGetdate()
        {
            try
            {
                lblDate.Text = cls.fnGetDate("SD");
                lblTime.Text = cls.fnGetDate("CT");

                if (check.IsConnectedToInternet())
                {
                    lblDate.ForeColor = Color.Black;
                    lblTime.ForeColor = Color.Black;
                }
                else
                {
                    lblDate.ForeColor = Color.Red;
                    lblTime.ForeColor = Color.Red;
                }
            }
            catch(SqlException sqlEx)
            {

            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        public void fnBindOrder()
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_HR_10_Material_ScanOut_Orders_SelItem_Addnew";
                DataTable dt = new DataTable();

                //SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
                //sParams[0] = new SqlParameter();
                //sParams[0].SqlDbType = SqlDbType.Int;
                //sParams[0].ParameterName = "whId";
                //sParams[0].Value = _locId;

                dt = cls.fnSelect(sql);
                dgvOrders.DataSource = dt;
                dgvOrders.Refresh();

                _dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders);

                //dgvOrders.Columns[0].Width = 20 * _dgvOrdersWidth / 100;    // idx
                dgvOrders.Columns[1].Width = 33 * _dgvOrdersWidth / 100;    // orderCode
                dgvOrders.Columns[2].Width = 40 * _dgvOrdersWidth / 100;    // department
                dgvOrders.Columns[3].Width = 27 * _dgvOrdersWidth / 100;    // orderDate
                //dgvOrders.Columns[4].Width = 30 * _dgvOrdersWidth / 100;    // orderStatus
                //dgvOrders.Columns[5].Width = 30 * _dgvOrdersWidth / 100;    // finish

                dgvOrders.Columns[0].Visible = false;
                dgvOrders.Columns[1].Visible = true;
                dgvOrders.Columns[2].Visible = true;
                dgvOrders.Columns[3].Visible = true;
                dgvOrders.Columns[4].Visible = false;
                dgvOrders.Columns[5].Visible = false;

                cls.fnFormatDatagridview(dgvOrders, 11);
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvOrders, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvOrders.Rows[e.RowIndex];

            string idx = row.Cells[0].Value.ToString();
            string code = row.Cells[1].Value.ToString();
            string dept = row.Cells[2].Value.ToString();
            string date = row.Cells[3].Value.ToString();
            string status = row.Cells[4].Value.ToString();
            string finish = row.Cells[5].Value.ToString();

            _orderIdx = idx;
            fnBindList();


            _matIdx = "0";
            fnTotalQty();
            dgvScan.DataSource = "";
            dgvInventory.DataSource = "";

            lblMessage.Text = "Vui lòng chọn mục cần scan-out";
            lblMessage.BackColor = Color.FromKnownColor(KnownColor.Info);
        }

        private void dgvOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string idx = "", code = "", dept = "", date = "", status = "", finish = "";
            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                idx = row.Cells[0].Value.ToString();
                code = row.Cells[1].Value.ToString();
                dept = row.Cells[2].Value.ToString();
                date = row.Cells[3].Value.ToString();
                status = row.Cells[4].Value.ToString();
                finish = row.Cells[5].Value.ToString();

                switch (status)
                {
                    case "0":
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case "1":
                        break;
                    case "2":
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        break;
                }
            }
        }

        private void dgvOrders_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvOrders.ClearSelection();
                dgvOrders.Rows[e.RowIndex].Selected = true;
                rowOrderIndex = e.RowIndex;
                dgvOrders.CurrentCell = dgvOrders.Rows[e.RowIndex].Cells[2];
                contextMenuStrip1.Show(this.dgvOrders, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void refreshOrdersListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnBindOrder();
        }

        public void fnBindList()
        {
            string orderIdx = _orderIdx;
            try
            {
                string sql = "V2o1_BASE_Warehouse_HR_Material_ScanOut_List_SelItem_Addnew";
                DataTable dt = new DataTable();

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderId";
                sParams[0].Value = orderIdx;

                dt = cls.ExecuteDataTable(sql,sParams);
                dgvList.DataSource = dt;
                dgvList.Refresh();

                _dgvListWidth = cls.fnGetDataGridWidth(dgvList);

                //dgvList.Columns[0].Width = 20 * _dgvListWidth / 100;    // idx
                //dgvList.Columns[1].Width = 40 * _dgvListWidth / 100;    // orderID
                //dgvList.Columns[2].Width = 30 * _dgvListWidth / 100;    // materialID
                dgvList.Columns[3].Width = 25 * _dgvListWidth / 100;    // materialName
                dgvList.Columns[4].Width = 15 * _dgvListWidth / 100;    // materialCode
                dgvList.Columns[5].Width = 10 * _dgvListWidth / 100;    // orderQuantity
                dgvList.Columns[6].Width = 10 * _dgvListWidth / 100;    // orderUnit
                dgvList.Columns[7].Width = 10 * _dgvListWidth / 100;    // equivalentQty
                dgvList.Columns[8].Width = 10 * _dgvListWidth / 100;    // equivalentUnit
                //dgvList.Columns[9].Width = 30 * _dgvListWidth / 100;    // finish
                dgvList.Columns[10].Width = 20 * _dgvListWidth / 100;    // materialNote

                dgvList.Columns[0].Visible = false;
                dgvList.Columns[1].Visible = false;
                dgvList.Columns[2].Visible = false;
                dgvList.Columns[3].Visible = true;
                dgvList.Columns[4].Visible = true;
                dgvList.Columns[5].Visible = true;
                dgvList.Columns[6].Visible = true;
                dgvList.Columns[7].Visible = true;
                dgvList.Columns[8].Visible = true;
                dgvList.Columns[9].Visible = false;
                dgvList.Columns[10].Visible = true;

                cls.fnFormatDatagridview(dgvList, 11);
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvList.Rows[e.RowIndex];

            string idx = row.Cells[0].Value.ToString();
            string orderID = _orderIdx;
            string matId = row.Cells[2].Value.ToString();
            string matName = row.Cells[3].Value.ToString();
            string matCode = row.Cells[4].Value.ToString();
            string orderQty = row.Cells[5].Value.ToString();
            string orderUnit = row.Cells[6].Value.ToString();
            string equivQty = row.Cells[7].Value.ToString();
            string equivUnit = row.Cells[8].Value.ToString();
            string finish = row.Cells[9].Value.ToString();
            string matNote = row.Cells[10].Value.ToString();

            _matIdx = matId;
            _orderQty = orderQty;
            _orderUnit = orderUnit;
            _equivQty = equivQty;
            _equivUnit = equivUnit;

            fnTotalQty();
            fnBindPacking();
            fnBindInvent();
        }

        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void fnTotalQty()
        {
            string orderId = _orderIdx;
            string matId = _matIdx;
            string packId = _packIdx;
            string inventoryId = _inventoryIdx;
            string itemTotal = "", orderTotal = "", scanTotal = "", scanPercent = "";
            string itemPercent = "", itemCode = "", itemInvent = "", itemUnit = "";
            string orderItems = "", scanOut = "";
            int _orderItems = 0, _scanOut = 0;
            double _itemTotal = 0, _orderTotal = 0, _scanTotal = 0, _itemInvent = 0;
            double _scanPercent = 0, _itemPercent = 0;

            if (orderId != "0")
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_TotalQty_Calculate_Addnew";

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderId";
                sParams[0].Value = orderId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@matId";
                sParams[1].Value = matId;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        itemTotal = ds.Tables[0].Rows[0][0].ToString();
                        orderTotal = ds.Tables[0].Rows[0][1].ToString();
                        scanTotal = ds.Tables[0].Rows[0][2].ToString();
                        scanPercent = ds.Tables[0].Rows[0][3].ToString();
                        itemPercent = ds.Tables[0].Rows[0][4].ToString();
                        itemCode = ds.Tables[0].Rows[0][5].ToString();
                        itemInvent = ds.Tables[0].Rows[0][6].ToString();
                        itemUnit = ds.Tables[0].Rows[0][7].ToString();
                        orderItems = ds.Tables[0].Rows[0][8].ToString();
                        scanOut = ds.Tables[0].Rows[0][9].ToString();
                    }
                    else
                    {
                        itemTotal = "0";
                        orderTotal = "0";
                        scanTotal = "0";
                        scanPercent = "0";
                        itemPercent = "0";
                        itemCode = "-N/A-";
                        itemInvent = "0";
                        itemUnit = "-";
                        orderItems = "0";
                        scanOut = "0";
                    }
                }
                else
                {
                    itemTotal = "0";
                    orderTotal = "0";
                    scanTotal = "0";
                    scanPercent = "0";
                    itemPercent = "0";
                    itemCode = "-N/A-";
                    itemInvent = "0";
                    itemUnit = "-";
                    orderItems = "0";
                    scanOut = "0";
                }
            }
            else
            {
                itemTotal = "0";
                orderTotal = "0";
                scanTotal = "0";
                scanPercent = "0";
                itemPercent = "0";
                itemCode = "-N/A-";
                itemInvent = "0";
                itemUnit = "-";
                orderItems = "0";
                scanOut = "0";
            }

            _orderItems = (orderItems != "" && orderItems != null) ? Convert.ToInt32(orderItems) : 0;
            _scanOut = (scanOut != "" && scanOut != null) ? Convert.ToInt32(scanOut) : 0;
            
            _itemTotal = (itemTotal != "" && itemTotal != null) ? Convert.ToDouble(itemTotal) : 0;
            _orderTotal = (orderTotal != "" && orderTotal != null) ? Convert.ToDouble(orderTotal) : 0;
            _scanTotal = (scanTotal != "" && scanTotal != null) ? Convert.ToDouble(scanTotal) : 0;
            _scanPercent = (scanPercent != "" && scanPercent != null) ? Convert.ToDouble(scanPercent) : 0;
            _itemPercent = (itemPercent != "" && itemPercent != null) ? Convert.ToDouble(itemPercent) : 0;
            _itemInvent = (itemInvent != "" && itemInvent != null) ? Convert.ToDouble(itemInvent) : 0;

            lblItemTotal.Text = String.Format("{0:0}", _itemTotal);
            lblOrderTotal.Text = String.Format("{0:0}", _orderTotal);
            lblScanPercent.Text = String.Format("{0:0.0}", _scanPercent) + "%";
            lblItemPercent.Text = String.Format("{0:0.0}", _itemPercent) + "%";
            lblScanTotal.Text = String.Format("{0:0}", _scanTotal);

            lblPartCode.Text = itemCode;
            lblPartInvent.Text = String.Format("{0:0}", _itemInvent);
            lblPartUnit.Text = itemUnit;

            if (_scanPercent >= 99 || _orderItems == _scanOut)
            {
                txtPackcode.Text = "";
                txtPackcode.Enabled = false;
                lblMessage.Text = "Mục này đã xuất kho đủ số lượng như yêu cầu.";
                lblMessage.BackColor = Color.LightGreen;
            }
            else
            {
                txtPackcode.Text = "";
                txtPackcode.Enabled = true;
                txtPackcode.Focus();
                lblMessage.Text = "";
                lblMessage.Text = "Chọn mục hàng và bắt đầu nhập mã barcode.";
                lblMessage.BackColor = Color.FromKnownColor(KnownColor.Info);
            }
        }

        public void fnBindInvent()
        {
            string matId = _matIdx;
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Invent_SelItem_Addnew";
                DataTable dt = new DataTable();

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@matId";
                sParams[0].Value = matId;

                dt = cls.ExecuteDataTable(sql, sParams);
                dgvInventory.DataSource = dt;
                dgvInventory.Refresh();

                _dgvInventoryWidth = cls.fnGetDataGridWidth(dgvInventory);

                //dgvInventory.Columns[0].Width = 20 * _dgvInventoryWidth / 100;    // ProdId
                dgvInventory.Columns[1].Width = 30 * _dgvInventoryWidth / 100;    // WH.Name
                dgvInventory.Columns[2].Width = 25 * _dgvInventoryWidth / 100;    // Sublocation
                dgvInventory.Columns[3].Width = 20 * _dgvInventoryWidth / 100;    // Quantity
                dgvInventory.Columns[4].Width = 25 * _dgvInventoryWidth / 100;    // receiveDate
                //dgvInventory.Columns[5].Width = 10 * _dgvInventoryWidth / 100;    // LocationId

                dgvInventory.Columns[0].Visible = false;
                dgvInventory.Columns[1].Visible = true;
                dgvInventory.Columns[2].Visible = true;
                dgvInventory.Columns[3].Visible = true;
                dgvInventory.Columns[4].Visible = true;
                dgvInventory.Columns[5].Visible = false;

                cls.fnFormatDatagridview(dgvInventory, 11);


            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public void fnBindPacking()
        {
            string orderId = _orderIdx;
            string matId = _matIdx;
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Scan_SelItem_Addnew";
                DataTable dt = new DataTable();

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderId";
                sParams[0].Value = orderId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@matId";
                sParams[1].Value = matId;

                dt = cls.ExecuteDataTable(sql, sParams);
                dgvScan.DataSource = dt;
                dgvScan.Refresh();

                _dgvScanWidth = cls.fnGetDataGridWidth(dgvScan);

                //dgvScan.Columns[0].Width = 20 * _dgvScanWidth / 100;    // outId
                //dgvScan.Columns[1].Width = 30 * _dgvScanWidth / 100;    // orderID
                //dgvScan.Columns[2].Width = 25 * _dgvScanWidth / 100;    // partID
                //dgvScan.Columns[3].Width = 10 * _dgvScanWidth / 100;    // packingID
                dgvScan.Columns[4].Width = 15 * _dgvScanWidth / 100;    // packingCode
                dgvScan.Columns[5].Width = 10 * _dgvScanWidth / 100;    // outSublocate
                dgvScan.Columns[6].Width = 17 * _dgvScanWidth / 100;    // partName
                dgvScan.Columns[7].Width = 15 * _dgvScanWidth / 100;    // partCode
                dgvScan.Columns[8].Width = 10 * _dgvScanWidth / 100;    // packingQty
                dgvScan.Columns[9].Width = 5 * _dgvScanWidth / 100;    // orderQty
                dgvScan.Columns[10].Width = 14 * _dgvScanWidth / 100;    // packingStockDate
                dgvScan.Columns[11].Width = 14 * _dgvScanWidth / 100;    // outStockDate
                //dgvScan.Columns[12].Width = 14 * _dgvScanWidth / 100;    // outLocateId

                dgvScan.Columns[0].Visible = false;
                dgvScan.Columns[1].Visible = false;
                dgvScan.Columns[2].Visible = false;
                dgvScan.Columns[3].Visible = false;
                dgvScan.Columns[4].Visible = true;
                dgvScan.Columns[5].Visible = true;
                dgvScan.Columns[6].Visible = true;
                dgvScan.Columns[7].Visible = true;
                dgvScan.Columns[8].Visible = true;
                dgvScan.Columns[9].Visible = true;
                dgvScan.Columns[10].Visible = true;
                dgvScan.Columns[11].Visible = true;
                dgvScan.Columns[12].Visible = false;

                dgvScan.Columns[10].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvScan.Columns[11].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                cls.fnFormatDatagridview(dgvScan, 11);
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void dgvScan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string orderId = _orderIdx;
            string materialId = _matIdx;

            cls.fnDatagridClickCell(dgvScan, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvScan.Rows[e.RowIndex];
            string packingId = row.Cells[3].Value.ToString();
            string partcode = row.Cells[7].Value.ToString();
            string quantity = row.Cells[8].Value.ToString();
            string locateId = row.Cells[12].Value.ToString();
            string location = row.Cells[5].Value.ToString();

            _partcode = partcode;
            _quantity = quantity;
            _locateId = locateId;
            _location = location;

        }

        private void dgvScan_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvScan_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvScan.ClearSelection();
                dgvScan.Rows[e.RowIndex].Selected = true;
                rowScanIndex = e.RowIndex;
                dgvScan.CurrentCell = dgvScan.Rows[e.RowIndex].Cells[4];
                contextMenuStrip2.Show(this.dgvScan, e.Location);
                contextMenuStrip2.Show(Cursor.Position);
            }

        }

        private void dgvInventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvInventory, e);
        }

        private void dgvInventory_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvInventory.ClearSelection();
                dgvInventory.Rows[e.RowIndex].Selected = true;
                rowInventoryIndex = e.RowIndex;
                dgvInventory.CurrentCell = dgvInventory.Rows[e.RowIndex].Cells[1];
                contextMenuStrip3.Show(this.dgvInventory, e.Location);
                contextMenuStrip3.Show(Cursor.Position);
            }
        }

        private void refreshMaterialsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnBindInvent();
        }

        private void txtPackcode_KeyDown(object sender, KeyEventArgs e)
        {
            string orderId = _orderIdx;
            string matId = _matIdx;
            string orderQty = _orderQty;
            string orderUnit = _orderUnit;
            string equivQty = _equivQty;
            string equivUnit = _equivUnit;
            string compUnit = "";
            switch(equivUnit)
            {
                case "piece":
                    compUnit = "PCS";
                    break;
                case "box":
                    compUnit = "BOX";
                    break;
                case "pack":
                    compUnit = "PAK";
                    break;
                case "pallete":
                    compUnit = "PAL";
                    break;
            }

            string packing = txtPackcode.Text.Trim();
            string sqlPackID = "V2o1_BASE_Warehouse_Material_ScanOut_PackingID_SelItem_Addnew";
            string packID = "";

            SqlParameter[] sParamsPackID = new SqlParameter[1]; // Parameter count
            sParamsPackID[0] = new SqlParameter();
            sParamsPackID[0].SqlDbType = SqlDbType.Int;
            sParamsPackID[0].ParameterName = "@packing";
            sParamsPackID[0].Value = packing;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sqlPackID, sParamsPackID);
            if(ds.Tables.Count>0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    packID = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    packID = "0";
                }
            }
            else
            {
                packID = "0";
            }

            if (e.KeyCode==Keys.Enter)
            {
                string packType = cls.Left(packing, 3);
                string codeType = cls.Mid(packing, 4, 3);
                string packCode = cls.Right(packing, packing.Length - 7);

                if (packType == "MMT")
                {
                    string packCurMatID = "", packCurMatName = "";
                    string sqlCurMaterial = "V2o1_BASE_Warehouse_HR_Material_ScanOut_CheckCurPackMat_SelItem_Addnew";
                    SqlParameter[] sParamsCurMat = new SqlParameter[2]; // Parameter count

                    sParamsCurMat[0] = new SqlParameter();
                    sParamsCurMat[0].SqlDbType = SqlDbType.VarChar;
                    sParamsCurMat[0].ParameterName = "@packing";
                    sParamsCurMat[0].Value = packing;

                    sParamsCurMat[1] = new SqlParameter();
                    sParamsCurMat[1].SqlDbType = SqlDbType.Int;
                    sParamsCurMat[1].ParameterName = "@partId";
                    sParamsCurMat[1].Value = matId;

                    DataTable dtCurMat = new DataTable();
                    dtCurMat = cls.ExecuteDataTable(sqlCurMaterial, sParamsCurMat);
                    if (dtCurMat.Rows.Count > 0)
                    {
                        if (codeType.ToUpper() == compUnit.ToUpper())
                        {
                            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_ScanPacking_Addnew";
                            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                            sParams[0] = new SqlParameter();
                            sParams[0].SqlDbType = SqlDbType.Int;
                            sParams[0].ParameterName = "@orderID";
                            sParams[0].Value = orderId;

                            sParams[1] = new SqlParameter();
                            sParams[1].SqlDbType = SqlDbType.Int;
                            sParams[1].ParameterName = "@itemID";
                            sParams[1].Value = matId;

                            sParams[2] = new SqlParameter();
                            sParams[2].SqlDbType = SqlDbType.VarChar;
                            sParams[2].ParameterName = "@packingCode";
                            sParams[2].Value = packing;

                            cls.fnUpdDel(sql, sParams);

                            lblMessage.Text = "Xuất kho thành công.";
                            lblMessage.BackColor = Color.LightSeaGreen;

                            fnBindPacking();
                            fnTotalQty();
                            fnBindInvent();
                        }
                        else
                        {
                            lblMessage.Text = "Sai mã đóng gói.\r\nVui lòng chọn đúng loại '" + codeType.ToUpper() + "' để xuất hàng.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Sai loại hàng xuất kho.\r\nVui lòng kiểm tra lại loại hàng và/hoặc tem mã vạch.";
                    }
                }
                else
                {
                    lblMessage.Text = "Sai nhãn tem mã vạch.\r\nPhải bắt đầu bằng 'MMT'.";
                }
                //MessageBox.Show(orderId + "---" + matId + "---" + packing + "---" + packType + "---" + codeType + "---" + packCode);
                lblMessage.BackColor = Color.Gold;

                txtPackcode.Text = "";
                txtPackcode.Focus();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Vui lòng chờ trong khi hệ thống trích xuất dữ liệu.";

            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Excel_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            dgvExcel.DataSource = dt;
            dgvExcel.Refresh();

            byte export = 0;
            export = cls.ExportToExcel(dgvExcel, "Material Out " + String.Format("{0:MM-yyyy}", DateTime.Now));
            lblMessage.Text = (export == 1) ? "Xuất ra excel thành công." : "Có lỗi khi xuất dữ liệu.\r\nVui lòng thử lại.";
            dgvExcel.DataSource = "";
            dgvExcel.Refresh();
        }

        private void deleteThisPackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string orderId = _orderIdx;
            string materialId = _matIdx;
            string packingId = _packIdx;
            string partcode = _partcode;
            string quantity = _quantity;
            string locateId = _locateId;
            string location = _location;

            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Scan_DelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[7]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@materialId";
                sParams[1].Value = materialId;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.Int;
                sParams[2].ParameterName = "@packingId";
                sParams[2].Value = packingId;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.VarChar;
                sParams[3].ParameterName = "@partcode";
                sParams[3].Value = partcode;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.SmallMoney;
                sParams[4].ParameterName = "@quantity";
                sParams[4].Value = quantity;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.Int;
                sParams[5].ParameterName = "@locateId";
                sParams[5].Value = locateId;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.VarChar;
                sParams[6].ParameterName = "@location";
                sParams[6].Value = location;

                cls.fnUpdDel(sql, sParams);
            }
            fnBindPacking();
            fnTotalQty();
            fnBindInvent();

            txtPackcode.Text = "";
            txtPackcode.Focus();
        }
    }
}
