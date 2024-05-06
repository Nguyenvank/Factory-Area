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
    public partial class frmWarehouseMaterialScanOut_v1o2 : Form
    {
        string __msg = "", __sql = "";
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

        string
            __issue_msg = "";


        SqlParameter[]
            __sparam = null;

        DataSet
            __ds = null;

        int
            __issue_cd = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        public int _dgvOrdersWidth;
        public int _dgvListWidth;
        public int _dgvScanWidth;
        public int _dgvInventoryWidth;

        private int rowOrderIndex = 0;
        private int rowListIndex = 0;
        private int rowScanIndex = 0;
        private int rowInventoryIndex = 0;

        public DateTime _dt;
        Timer timer = new Timer();

        cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        string
            __msg_type = "";

        int
            __msg_type_cd = 0,
            __whs_idx = 0;

        public frmWarehouseMaterialScanOut_v1o2()
        {
            InitializeComponent();

            __msg_type = ini.GetIniValue("MSG", "TYPE", "1").Trim();    /* 1- MessageBox with OK; 2- MessageBox with YESNO */
            __whs_idx = int.Parse(ini.GetIniValue("WHS", "ID", "102"));

            __msg_type_cd = (__msg_type.Length > 0) ? Convert.ToInt32(__msg_type) : 1;
            try
            {
            }
            catch { }
            finally { }

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_summary, true);
            cls.SetDoubleBuffer(tlp_inventory, true);
            cls.SetDoubleBuffer(dgvList, true);
            cls.SetDoubleBuffer(dgvOrders, true);
            cls.SetDoubleBuffer(dgvScan, true);
            cls.SetDoubleBuffer(dgvInventory, true);
        }

        private void frmWarehouseMaterialScanOut_v1o2_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            _dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders) - 20;
            _dgvListWidth = cls.fnGetDataGridWidth(dgvList);
            _dgvScanWidth = cls.fnGetDataGridWidth(dgvScan);
            _dgvInventoryWidth = cls.fnGetDataGridWidth(dgvInventory);

            init();

            Fnc_Load_Get_Warning_From_Logging();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            //_dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders) - 20;
            //_dgvListWidth = cls.fnGetDataGridWidth(dgvList);
            //_dgvScanWidth = cls.fnGetDataGridWidth(dgvScan);
            //_dgvInventoryWidth = cls.fnGetDataGridWidth(dgvInventory);

            fnGetdate();

            Fnc_Load_Get_Warning_From_Logging();
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
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Orders_SelItem_V2o1_Addnew";
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
                dgvOrders.Columns[1].Width = 40 * _dgvOrdersWidth / 100;    // orderCode
                dgvOrders.Columns[2].Width = 33 * _dgvOrdersWidth / 100;    // department
                dgvOrders.Columns[3].Width = 27 * _dgvOrdersWidth / 100;    // orderDate
                //dgvOrders.Columns[4].Width = 30 * _dgvOrdersWidth / 100;    // orderStatus
                //dgvOrders.Columns[5].Width = 30 * _dgvOrdersWidth / 100;    // finish

                dgvOrders.Columns[0].Visible = false;
                dgvOrders.Columns[1].Visible = true;
                dgvOrders.Columns[2].Visible = true;
                dgvOrders.Columns[3].Visible = true;
                dgvOrders.Columns[4].Visible = false;
                dgvOrders.Columns[5].Visible = false;

                cls.fnFormatDatagridview(dgvOrders, 11, 30);
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
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
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
            }
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
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
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
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_List_SelItem_Addnew";
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
            catch { }
            finally { }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
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

                //MessageBox.Show("_matIdx: " + _matIdx + "\r\n_orderQty: " + _orderQty + "\r\n_orderUnit: " + _orderUnit + "\r\n_equivQty: " + _equivQty + "\r\n_equivUnit: " + _equivUnit);

                fnTotalQty();
                fnBindPacking();
                fnBindInvent();
            }
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
                try
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
                catch { }
                finally { }
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

            //if (_scanPercent >= 96 || _orderItems == _scanOut)
            if (_orderItems == _scanOut)
            {
                txtPackcode.Text = "";
                txtPackcode.Enabled = false;
                lblMessage.Text = "This item has been out-stocked enough as request";
            }
            else
            {
                txtPackcode.Text = "";
                txtPackcode.Enabled = true;
                txtPackcode.Focus();
                lblMessage.Text = "";
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
            catch { }
            finally { }
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
                dgvScan.Columns[4].Width = 30 * _dgvScanWidth / 100;    // packingCode
                dgvScan.Columns[5].Width = 10 * _dgvScanWidth / 100;    // outSublocate
                //dgvScan.Columns[6].Width = 17 * _dgvScanWidth / 100;    // partName
                dgvScan.Columns[7].Width = 18 * _dgvScanWidth / 100;    // partCode
                dgvScan.Columns[8].Width = 5 * _dgvScanWidth / 100;    // packingQty
                dgvScan.Columns[9].Width = 7 * _dgvScanWidth / 100;    // orderQty
                dgvScan.Columns[10].Width = 15 * _dgvScanWidth / 100;    // packingStockDate
                dgvScan.Columns[11].Width = 15 * _dgvScanWidth / 100;    // outStockDate
                //dgvScan.Columns[12].Width = 14 * _dgvScanWidth / 100;    // outLocateId

                dgvScan.Columns[0].Visible = false;
                dgvScan.Columns[1].Visible = false;
                dgvScan.Columns[2].Visible = false;
                dgvScan.Columns[3].Visible = false;
                dgvScan.Columns[4].Visible = true;
                dgvScan.Columns[5].Visible = true;
                dgvScan.Columns[6].Visible = false;
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
            catch { }
            finally { }
        }

        private void dgvScan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvInventory, e);
            }
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
            if (e.KeyCode == Keys.Enter)
            {
	            string orderId = _orderIdx;
	            string matId = _matIdx;
	            string orderQty = _orderQty;
	            string orderUnit = _orderUnit;
	            string equivQty = _equivQty;
	            string equivUnit = _equivUnit;
	            string compUnit = "";
                string split = "", kindOut = "";
                int _split = 0, _kindOut = 0, whs_idx = __whs_idx;
	            switch(equivUnit.ToLower())
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

                try
                {
                }
                catch { }
                finally { }

                string packing = txtPackcode.Text.Trim();
                //string sqlPackID = "V2o1_BASE_Warehouse_Material_ScanOut_PackingID_SelItem_Addnew";
                string sqlPackID = "V2o1_BASE_Warehouse_Material_ScanOut_PackingID_SelItem_V2o1_Addnew";
                string packID = "", packInDate = "", packFIFO = "", locateFIFO = "", dateFIFO = "";
                int _packFIFO = 0;
                DateTime _packInDate, _dateFIFO;

                SqlParameter[] sParamsPackID = new SqlParameter[1]; // Parameter count
                sParamsPackID[0] = new SqlParameter();
                sParamsPackID[0].SqlDbType = SqlDbType.VarChar;
                sParamsPackID[0].ParameterName = "@packing";
                sParamsPackID[0].Value = packing;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sqlPackID, sParamsPackID);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    packID = ds.Tables[0].Rows[0][0].ToString();
                    packInDate = ds.Tables[0].Rows[0][5].ToString();
                    packFIFO = ds.Tables[0].Rows[0][6].ToString();
                    locateFIFO = ds.Tables[0].Rows[0][7].ToString();
                    dateFIFO = ds.Tables[0].Rows[0][8].ToString();

                    _packFIFO = (packFIFO != "" && packFIFO != null) ? Convert.ToInt32(packFIFO) : 0;
                    _dateFIFO = (_packFIFO == 1) ? Convert.ToDateTime(dateFIFO) : Convert.ToDateTime(packInDate);
                    _packInDate = Convert.ToDateTime(packInDate);

                    fnTotalQty();
                    string packType = cls.Left(packing, 3);
                    string codeType = cls.Mid(packing, 4, 3);
                    string packCode = cls.Right(packing, 5);

                    if (packType == "MMT")
                    {
                        string sqlExist = "V2o1_BASE_Warehouse_Material_ScanOut_CheckPacking_V2o1_Addnew";
                        SqlParameter[] sParamsExist = new SqlParameter[2]; // Parameter count

                        sParamsExist[0] = new SqlParameter();
                        sParamsExist[0].SqlDbType = SqlDbType.Int;
                        sParamsExist[0].ParameterName = "@partID";
                        sParamsExist[0].Value = matId;

                        sParamsExist[1] = new SqlParameter();
                        sParamsExist[1].SqlDbType = SqlDbType.VarChar;
                        sParamsExist[1].ParameterName = "@packingCode";
                        sParamsExist[1].Value = packing;

                        DataSet dsExist = new DataSet();
                        dsExist = cls.ExecuteDataSet(sqlExist, sParamsExist);

                        if (dsExist.Tables.Count > 0 && dsExist.Tables[0].Rows.Count > 0)
                        {
                            //split = ds.Tables[0].Rows[0][8].ToString();
                            //kindOut = ds.Tables[0].Rows[0][9].ToString();

                            split = "";
                            kindOut = "";

                            _split = (split != "" && split != null) ? Convert.ToInt32(split) : 0;
                            _kindOut = (kindOut != "" && kindOut != null) ? Convert.ToInt32(kindOut) : 0;

                            //if (_split == 0 || _kindOut == 0)
                            //{

                            //}
                            //else
                            //{
                            //    MessageBox.Show("XUẤT VẬT TƯ KHÔNG THÀNH CÔNG!!!\r\n\r\nKhông thể xuất vật tư cho mục này do đã tách NVL xong\r\nVui lòng tạo order mới để có thể xuất kho.");
                            //}

                            if (_packFIFO == 0)
                            {
                                if (codeType.ToUpper() == compUnit.ToUpper())
                                {
                                    fnScanOut_Material(orderId, matId, packing);

                                    //string sql = "V2o1_BASE_Warehouse_Material_ScanOut_ScanPacking_Addnew";
                                    //SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                                    //sParams[0] = new SqlParameter();
                                    //sParams[0].SqlDbType = SqlDbType.Int;
                                    //sParams[0].ParameterName = "@orderID";
                                    //sParams[0].Value = orderId;

                                    //sParams[1] = new SqlParameter();
                                    //sParams[1].SqlDbType = SqlDbType.Int;
                                    //sParams[1].ParameterName = "@itemID";
                                    //sParams[1].Value = matId;

                                    //sParams[2] = new SqlParameter();
                                    //sParams[2].SqlDbType = SqlDbType.VarChar;
                                    //sParams[2].ParameterName = "@packingCode";
                                    //sParams[2].Value = packing;

                                    //cls.fnUpdDel(sql, sParams);
                                }
                                else
                                {
                                    //lblMessage.Text = "Wrong packing type. Please choose '" + codeType.ToUpper() + "' another to scan.";
                                    lblMessage.Text = "Sai loại packing. Vui lòng chọn loại '" + codeType.ToUpper() + "' khác để scan.";
                                }
                            }
                            else
                            {
                                int msgType = __msg_type_cd;    /* 1- MessageBox with OK; 2- MessageBox with YESNO */

                                string msg1 = "Hệ thống ghi nhận có 1 kiện (hoặc nhiều hơn) ngày nhập (" + _dateFIFO.ToString("dd/MM/yyyy") + ") cũ hơn của kiện đang xuất (" + _packInDate.ToString("dd/MM/yyyy") + ")";
                                string msg2 = "\r\n\r\n";
                                string msg3 = "Have more 1 packing scan in early date (" + _dateFIFO.ToString("dd/MM/yyyy") + ") than current packing (" + _packInDate.ToString("dd/MM/yyyy") + ")";
                                string msg4 = "\r\n\r\n\r\n";
                                string msg5 = "-------------------------------------------------------------";

                                __msg = msg1 + msg2 + msg3 + msg4;

                                lblMessage.Text = msg1;
                                lblMessage.BackColor = Color.Tomato;
                                lblMessage.ForeColor = Color.White;
                                if (msgType == 1)
                                {
                                    __issue_msg = msg1 + msg2 + msg3 + msg4;
                                    __issue_cd = 1;

                                    Fnc_Load_Save_Warning_To_Block(whs_idx, __issue_msg, __issue_cd, packing);

                                    //MessageBox.Show(msg1 + msg2 + msg3 + msg4 + msg5);
                                    frmWarehouseMaterialScanOut_v1o2_Message_v1o0 msg = new frmWarehouseMaterialScanOut_v1o2_Message_v1o0(msg1 + msg2 + msg3 + msg4, msgType.ToString(), whs_idx);
                                    msg.ShowDialog();
                                }
                                else
                                {
                                    DialogResult dialog = MessageBox.Show(msg1 + msg2 + msg3 + msg4 + msg5 + "\r\nCó chắc là vẫn muốn xuất kiện này\r\nAre you sure to continue?", cls.appName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        fnScanOut_Material(orderId, matId, packing);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string msg_02 = "Sai vật tư như yêu cầu. Vui lòng kiểm tra vật tư đang được chọn và scan lại.";
                            //lblMessage.Text = "Wrong material as order. Please check the selected material and choose again.";
                            lblMessage.Text = msg_02;

                            __issue_msg = msg_02;
                            __issue_cd = 2;

                            Fnc_Load_Save_Warning_To_Block(whs_idx, __issue_msg, __issue_cd, packing);

                            frmWarehouseMaterialScanOut_v1o2_Message_v1o0 msg = new frmWarehouseMaterialScanOut_v1o2_Message_v1o0(msg_02, "1", whs_idx);
                            msg.ShowDialog();
                        }

                        __issue_msg = "";
                        __issue_cd = 0;
                    }
                    else
                    {
                        //lblMessage.Text = "Invalid barcode for warehouse label.\r\nIt must begin with 'MMT'.";
                        lblMessage.Text = "Sai loại mã vạch cho tem MMT.\r\nBắt buộc phải bắt đầu bằng 'MMT'.";
                    }
                    //MessageBox.Show(orderId + "---" + matId + "---" + packing + "---" + packType + "---" + codeType + "---" + packCode);
                    fnBindPacking();
                    fnTotalQty();
                    fnBindInvent();

                    txtPackcode.Text = "";
                    txtPackcode.Focus();

                }
                else
                {
                    packID = "0";
                    //lblMessage.Text = "Cannot found packing code as input. Please check and try again.";
                    lblMessage.Text = "Không tìm thấy mã kiện tương ứng như vừa scan. Vui lòng kiểm tra và scan lại.";
                }
            }
        }

        public void fnScanOut_Material(string _orderID, string _matID, string _packing)
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_ScanPacking_Addnew";
                SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = _orderID;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@itemID";
                sParams[1].Value = _matID;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.VarChar;
                sParams[2].ParameterName = "@packingCode";
                sParams[2].Value = _packing;

                cls.fnUpdDel(sql, sParams);
            }
            catch { }
            finally { }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "Please wait for generate data to export.";

                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Excel_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                dgvExcel.DataSource = dt;
                dgvExcel.Refresh();

                byte export = 0;
                export = cls.ExportToExcel(dgvExcel, "Material Out " + String.Format("{0:MM-yyyy}", DateTime.Now));
                lblMessage.Text = (export == 1) ? "Export to excel successful." : "Export to excel failure. Please try again.";
                dgvExcel.DataSource = "";
                dgvExcel.Refresh();
            }
            catch { }
            finally { }
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
                try
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
                catch { }
                finally { }
            }
            fnBindPacking();
            fnTotalQty();
            fnBindInvent();

            txtPackcode.Text = "";
            txtPackcode.Focus();
        }

        private void tssMsg_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 7000;
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(this.timer_Tick);

            if (lblMessage.Text.Length > 0)
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
            lblMessage.Text = "";
            lblMessage.BackColor = Color.FromKnownColor(KnownColor.Info);
            lblMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            timer.Stop();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string orderIDx = _orderIdx;
            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Orders_Status_SelItem_V2o1_Addnew";

            try
            {
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderIDx";
                sParams[0].Value = orderIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                }
            }
            catch { }
            finally { }
        }

        private void PrintThisOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lblMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int whs_idx = __whs_idx;
            string mess = "", packing = "MMT-PAK-202306300001";
            string msg1 = "Hệ thống ghi nhận có 1 kiện (hoặc nhiều hơn) ngày nhập (" + DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy") + ") cũ hơn của kiện đang xuất (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
            string msg2 = "\r\n\r\n";
            string msg3 = "Have more 1 packing scan in early date (" + DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy") + ") than current packing (" + DateTime.Now.ToString("dd/MM/yyyy") + ")";
            string msg4 = "\r\n\r\n";
            mess = msg1 + msg2 + msg3 + msg4;

            __issue_msg = msg1 + msg2 + msg3 + msg4;
            __issue_cd = 1000;

            //MessageBox.Show(__issue_msg);
            Fnc_Load_Save_Warning_To_Block(whs_idx, __issue_msg, __issue_cd, packing);

            frmWarehouseMaterialScanOut_v1o2_Message_v1o0 msg = new frmWarehouseMaterialScanOut_v1o2_Message_v1o0(mess, "1000", whs_idx);
            msg.ShowDialog();

            __issue_msg = "";
            __issue_cd = 0;
        }

        public void Fnc_Load_Save_Warning_To_Block(int whs_idx, string issue_msg, int issue_cd, string label_cd)
        {
            //MessageBox.Show(whs_idx + "\r\n" + issue_msg + "\r\n" + issue_cd + "\r\n" + label_cd);


            try
            {
                __sql = "V2o1_SUMMARY_MMT_Warehouse_Scan_Out_Issue_AddItem_Addnew_V1o0";

                __sparam = new SqlParameter[4];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@whs_idx";
                __sparam[0].Value = whs_idx;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.NVarChar;
                __sparam[1].ParameterName = "@issue_msg";
                __sparam[1].Value = issue_msg;

                __sparam[2] = new SqlParameter();
                __sparam[2].SqlDbType = SqlDbType.TinyInt;
                __sparam[2].ParameterName = "@issue_cd";
                __sparam[2].Value = issue_cd;

                __sparam[3] = new SqlParameter();
                __sparam[3].SqlDbType = SqlDbType.VarChar;
                __sparam[3].ParameterName = "@label_cd";
                __sparam[3].Value = label_cd;

                cls.fnUpdDel(__sql, __sparam);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Get_Warning_From_Logging()
        {
            string issue_msg = __issue_msg;

            int
                whs_idx = __whs_idx,
                issue_cnt = 0,
                issue_cd = __issue_cd;

            try
            {
                __sql = "V2o1_SUMMARY_MMT_Warehouse_Scan_Out_Issue_SelItem_Addnew_V1o0";

                __sparam = new SqlParameter[1];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@whs_idx";
                __sparam[0].Value = __whs_idx;

                __ds = cls.ExecuteDataSet(__sql, __sparam);
                __tbl_cnt = __ds.Tables.Count;
                __row_cnt = __ds.Tables[0].Rows.Count;

                if (__tbl_cnt > 0 && __row_cnt > 0)
                {
                    issue_cnt = int.Parse(__ds.Tables[0].Rows[0][0].ToString());

                    if (issue_cnt > 0)
                    {
                        frmWarehouseMaterialScanOut_v1o2_Message_v1o0 msg = new frmWarehouseMaterialScanOut_v1o2_Message_v1o0(__issue_msg, __issue_cd.ToString(), whs_idx);
                        msg.ShowDialog();
                    }
                }
            }
            catch { }
            finally { }
        }
    }
}
