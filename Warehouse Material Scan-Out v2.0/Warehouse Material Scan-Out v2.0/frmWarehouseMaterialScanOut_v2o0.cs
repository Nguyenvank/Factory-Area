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
    public partial class frmWarehouseMaterialScanOut_v2o0 : Form
    {
        public int _dgvOrderListWidth;
        public int _dgvItemListWidth;
        public int _dgvScanListWidth;

        public string _orderIDx;
        public string _orderCode;
        public string _orderPIC;
        public string _orderDate;
        public string _orderStatus;

        public int _chkOrder01;
        public int _chkOrder02;

        public string _partIDx;
        public string _partName;
        public string _partCode;
        public string _partUnit;

        public DateTime _dt = DateTime.Now;

        public frmWarehouseMaterialScanOut_v2o0()
        {
            InitializeComponent();
        }

        private void frmWarehouseMaterialScanOut_v2o0_Load(object sender, EventArgs e)
        {
            try
            {
                init();
            }
            catch
            {

            }
            finally
            {

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                fnGetdate();

                _chkOrder02 = fnLoadNewOrder();
                if (_chkOrder02 > _chkOrder01)
                {
                    initLoadOrder();
                    _chkOrder01 = _chkOrder02;
                }
            }
            catch
            {

            }
            finally
            {

            }

        }

        public void init()
        {
            try
            {
                _chkOrder01 = fnLoadNewOrder();

                _dgvOrderListWidth = cls.fnGetDataGridWidth(dgvOrderList);
                _dgvItemListWidth = cls.fnGetDataGridWidth(dgvItemList);
                _dgvScanListWidth = cls.fnGetDataGridWidth(dgvScanList);

                fnGetdate();

                initTotalPanel();
                initLoadOrder();
                dgvOrderList.ClearSelection();
            }
            catch
            {

            }
            finally
            {

            }

        }

        public void fnGetdate()
        {
            try
            {
                cls.fnDateTime(tssDateTime, 3);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void initTotalPanel()
        {
            lblItem.Text = "0";
            lblTotal.Text = "0";
            lblScan.Text = "0%";

            lblQ_Item.Text = "(0)";
            lblQ_Total.Text = "(0)";
            //lblQ_Scan.Text = "(0)";

            txtPacking.Enabled = false;
        }

        public void initLoadOrder()
        {
            try
            {
                dgvOrderList.ClearSelection();
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_v2_OrderList_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                dgvOrderList.DataSource = dt;
                _dgvOrderListWidth = cls.fnGetDataGridWidth(dgvOrderList);

                //dgvOrderList.Columns[0].Width = 15 * _dgvOrderListWidth / 100;    //IDx
                dgvOrderList.Columns[1].Width = 30 * _dgvOrderListWidth / 100;    //Code
                dgvOrderList.Columns[2].Width = 40 * _dgvOrderListWidth / 100;    //Receiver
                dgvOrderList.Columns[3].Width = 30 * _dgvOrderListWidth / 100;    //Make
                //dgvOrderList.Columns[4].Width = 30 * _dgvOrderListWidth / 100;    //Status

                dgvOrderList.Columns[0].Visible = false;
                dgvOrderList.Columns[1].Visible = true;
                dgvOrderList.Columns[2].Visible = true;
                dgvOrderList.Columns[3].Visible = true;
                dgvOrderList.Columns[4].Visible = false;

                //dgvOrderList.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvOrderList.Columns[3].DefaultCellStyle.Format = "dd/MM/yy HH:mm";

                cls.fnFormatDatagridview(dgvOrderList, 10, 30);
            }
            catch
            {

            }
            finally
            {

            }

        }

        public void fnLoadItems()
        {
            try
            {
                string orderIDx = _orderIDx;
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_v2_ItemList_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                dgvItemList.DataSource = dt;
                _dgvItemListWidth = cls.fnGetDataGridWidth(dgvItemList);

                //dgvItemList.Columns[0].Width = 15 * _dgvItemListWidth / 100;    //IDx
                //dgvItemList.Columns[1].Width = 15 * _dgvItemListWidth / 100;    //partIDx
                dgvItemList.Columns[2].Width = 25 * _dgvItemListWidth / 100;    //Name
                dgvItemList.Columns[3].Width = 20 * _dgvItemListWidth / 100;    //Code
                dgvItemList.Columns[4].Width = 5 * _dgvItemListWidth / 100;    //Unit
                dgvItemList.Columns[5].Width = 15 * _dgvItemListWidth / 100;    //Qty
                dgvItemList.Columns[6].Width = 10 * _dgvItemListWidth / 100;    //Approx.
                //dgvItemList.Columns[7].Width = 15 * _dgvItemListWidth / 100;    //Approx. Qty
                //dgvItemList.Columns[8].Width = 15 * _dgvItemListWidth / 100;    //Approx. Unit
                dgvItemList.Columns[9].Width = 15 * _dgvItemListWidth / 100;    //Scan-out
                dgvItemList.Columns[10].Width = 10 * _dgvItemListWidth / 100;    //Approx.
                //dgvItemList.Columns[11].Width = 15 * _dgvItemListWidth / 100;    //Approx. Out
                //dgvItemList.Columns[12].Width = 15 * _dgvItemListWidth / 100;    //Approx. Unit

                dgvItemList.Columns[0].Visible = false;
                dgvItemList.Columns[1].Visible = false;
                dgvItemList.Columns[2].Visible = true;
                dgvItemList.Columns[3].Visible = true;
                dgvItemList.Columns[4].Visible = true;
                dgvItemList.Columns[5].Visible = true;
                dgvItemList.Columns[6].Visible = true;
                dgvItemList.Columns[7].Visible = false;
                dgvItemList.Columns[8].Visible = false;
                dgvItemList.Columns[9].Visible = true;
                dgvItemList.Columns[10].Visible = true;
                dgvItemList.Columns[11].Visible = false;
                dgvItemList.Columns[12].Visible = false;

                cls.fnFormatDatagridview(dgvItemList, 12, 30);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadScans()
        {
            try
            {
                string orderIDx = _orderIDx;
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_v2_ScanList_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                dgvScanList.DataSource = dt;
                _dgvScanListWidth = cls.fnGetDataGridWidth(dgvScanList);

                //dgvScanList.Columns[0].Width = 15 * _dgvScanListWidth / 100;    //outID
                dgvScanList.Columns[1].Width = 20 * _dgvScanListWidth / 100;    //packingCode
                //dgvScanList.Columns[2].Width = 15 * _dgvScanListWidth / 100;    //partID
                dgvScanList.Columns[3].Width = 17 * _dgvScanListWidth / 100;    //partName
                dgvScanList.Columns[4].Width = 12 * _dgvScanListWidth / 100;    //partCode
                dgvScanList.Columns[5].Width = 5 * _dgvScanListWidth / 100;    //packingUnit
                dgvScanList.Columns[6].Width = 8 * _dgvScanListWidth / 100;    //packingQty
                dgvScanList.Columns[7].Width = 14 * _dgvScanListWidth / 100;    //packingStockDate
                //dgvScanList.Columns[8].Width = 15 * _dgvScanListWidth / 100;    //packingLocation
                dgvScanList.Columns[9].Width = 10 * _dgvScanListWidth / 100;    //packingSublocate
                dgvScanList.Columns[10].Width = 14 * _dgvScanListWidth / 100;    //outStockDate

                dgvScanList.Columns[0].Visible = false;
                dgvScanList.Columns[1].Visible = true;
                dgvScanList.Columns[2].Visible = false;
                dgvScanList.Columns[3].Visible = true;
                dgvScanList.Columns[4].Visible = true;
                dgvScanList.Columns[5].Visible = true;
                dgvScanList.Columns[6].Visible = true;
                dgvScanList.Columns[7].Visible = true;
                dgvScanList.Columns[8].Visible = false;
                dgvScanList.Columns[9].Visible = true;
                dgvScanList.Columns[10].Visible = true;

                dgvScanList.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvScanList.Columns[10].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                cls.fnFormatDatagridview(dgvScanList, 12, 30);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public int fnLoadNewOrder()
        {
            string nOrder = "";
            int _nOrder = 0;
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_v2_NewOrder_ChkItem_Addnew";
                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                if(ds.Tables.Count>0)
                {
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        nOrder = ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        nOrder = "0";
                    }
                }
                else
                {
                    nOrder = "0";
                }
                _nOrder = (nOrder != "" && nOrder != null) ? Convert.ToInt32(nOrder) : 0;
            }
            catch
            {

            }
            finally
            {

            }
            return _nOrder;
        }

        private void dgvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _orderIDx = "";
            _orderCode = "";
            _orderPIC = "";
            _orderDate = "";
            _orderStatus = "";

            cls.fnDatagridClickCell(dgvOrderList, e);

            DataGridViewRow row = new DataGridViewRow();
            row = dgvOrderList.Rows[e.RowIndex];

            _orderIDx = row.Cells[0].Value.ToString();
            _orderCode = row.Cells[1].Value.ToString();
            _orderPIC = row.Cells[2].Value.ToString();
            _orderDate = row.Cells[3].Value.ToString();
            _orderStatus = row.Cells[4].Value.ToString();

            fnLoadItems();
            fnLoadScans();
            fnLoadTotal();
            //txtPacking.Enabled = true;
            //txtPacking.Focus();
        }

        private void dgvOrderList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string finish = "";
                dgvOrderList.ClearSelection();
                foreach (DataGridViewRow row in dgvOrderList.Rows)
                {

                    finish = row.Cells[4].Value.ToString();
                    if (finish == "1")
                    {
                        row.DefaultCellStyle.BackColor = Color.Turquoise;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
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

        private void dgvOrderList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void fnLoadTotal()
        {
            string orderIDx, partIDx;
            orderIDx = (_orderIDx == "" || _orderIDx == null) ? "0" : _orderIDx;
            partIDx = (_partIDx == "" || _partIDx == null) ? "0" : _partIDx;

            string item = "", item_Q = "", total = "", total_Q = "", scan = "", scan_Q = "";
            int _item = 0, _item_Q = 0, _total = 0, _total_Q = 0, _scan = 0, _scan_Q = 0, _rate = 0;

            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_v2_TotalPanel_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@partID";
                sParams[1].Value = partIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if(ds.Tables.Count>0)
                {
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        item = ds.Tables[0].Rows[0][0].ToString();
                        item_Q = ds.Tables[0].Rows[0][1].ToString();
                        total = ds.Tables[0].Rows[0][2].ToString();
                        total_Q = ds.Tables[0].Rows[0][3].ToString();
                        scan = ds.Tables[0].Rows[0][4].ToString();
                        scan_Q = ds.Tables[0].Rows[0][5].ToString();

                        _item = (item != "" && item != null) ? Convert.ToInt32(item) : 0;
                        _item_Q = (item_Q != "" && item_Q != null) ? Convert.ToInt32(item_Q) : 0;
                        _total = (total != "" && total != null) ? Convert.ToInt32(total) : 0;
                        _total_Q = (total_Q != "" && total_Q != null) ? Convert.ToInt32(total_Q) : 0;
                        _scan = (scan != "" && scan != null) ? Convert.ToInt32(scan) : 0;
                        _scan_Q = (scan_Q != "" && scan_Q != null) ? Convert.ToInt32(scan_Q) : 0;

                        _rate = _item_Q * 100 / _total_Q;
                    }
                    else
                    {
                        item = "0";
                        item_Q = "0";
                        total = "0";
                        total_Q = "0";
                        scan = "0";
                        scan_Q = "0";
                    }
                }
                else
                {
                    item = "0";
                    item_Q = "0";
                    total = "0";
                    total_Q = "0";
                    scan = "0";
                    scan_Q = "0";
                }

                progressBar1.Minimum = 0;
                progressBar1.Maximum = _total_Q;
                progressBar1.Value = _item_Q;

                lblItem.Text = String.Format("{0:N0}", Convert.ToInt32(item));
                lblQ_Item.Text = String.Format("({0:N0})", Convert.ToInt32(item_Q));
                lblTotal.Text = String.Format("{0:N0}", Convert.ToInt32(total));
                lblQ_Total.Text = String.Format("({0:N0})", Convert.ToInt32(total_Q));
                //lblScan.Text = String.Format("{0:N0}", Convert.ToInt32(scan));
                lblScan.Text = String.Format("{0:N0}", Convert.ToInt32(_rate)) + "%";
                //lblQ_Scan.Text = String.Format("({0:N0})", Convert.ToInt32(scan_Q));

                string qTotal = "", qItems = "", qScans = "";
                qTotal = lblQ_Total.Text.Trim().Replace("(", "").Replace(")", "");
                qItems = lblQ_Item.Text.Trim().Replace("(", "").Replace(")", "");
                //qScans = lblQ_Scan.Text.Trim().Replace("(", "").Replace(")", "");
                //if (qTotal == qItems && qItems == qScans)
                if (qTotal == qItems)
                {
                    txtPacking.Enabled = false;
                    txtPacking.Text = "Full Order";
                }
                else
                {
                    txtPacking.Enabled = true;
                    txtPacking.Text = "";
                    txtPacking.Focus();
                }

            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvItemList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string q_order = "", q_scan = "";
                dgvItemList.ClearSelection();
                foreach (DataGridViewRow row in dgvItemList.Rows)
                {
                    q_order = row.Cells[7].Value.ToString();
                    q_scan = row.Cells[12].Value.ToString();

                    if (q_order == q_scan)
                    {
                        row.DefaultCellStyle.BackColor = Color.Turquoise;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
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

        private void dgvItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvItemList.ClearSelection();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvItemList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
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

        private void dgvScanList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
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

        private void dgvScanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void dgvScanList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
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

        private void txtPacking_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                tssMessage.Text = "";
                tssMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                txtPacking.BackColor = Color.FromKnownColor(KnownColor.Control);
                txtPacking.ForeColor = Color.Black;

                string orderIDx = _orderIDx;
                string msgText = "";
                int msgType = 0;    //  -1: error; 0: normal; 1: success
                string packing = txtPacking.Text.Trim();
                string packType = "", packKind = "", packCode = "";
                string qItem = "", qTotal = "";
                int _qItem = 0, _qTotal = 0;
                if (e.KeyCode == Keys.Enter)
                {
                    if (packing != "" && packing != null)
                    {
                        qItem = lblQ_Item.Text.Trim().Replace("(", "").Replace(")", "");
                        qTotal = lblQ_Total.Text.Trim().Replace("(", "").Replace(")", "");
                        _qItem = (qItem != "" && qItem != null) ? Convert.ToInt32(qItem) : 0;
                        _qTotal = (qTotal != "" && qTotal != null) ? Convert.ToInt32(qTotal) : 0;
                        if (packing.Length >= 12)
                        {
                            packType = packing.Substring(0, 3);
                            packKind = packing.Substring(4, 3);
                            packCode = packing.Substring(8);

                            msgText = packType + " - " + packKind + " - " + packCode;
                            msgType = 0;
                            //if ((packType == "MMT") && (packKind == "PCS" || packKind == "BOX" || packKind == "PAK" || packKind == "PAL") && (packCode.Length >= 5))
                            if ((packType == "MMT") && (packKind == "PCS" || packKind == "BOX" || packKind == "PAK" || packKind == "PAL"))
                            {
                                // CHECK TYPE OF MATERIAL INCLUDE CURRENT ORDER ITEMS
                                string chkSQL01 = "V2o1_BASE_Warehouse_Material_ScanOut_v2_PackingInOrder_ChkItem_Addnew";
                                SqlParameter[] sParamsChk01 = new SqlParameter[2]; // Parameter count
                                sParamsChk01[0] = new SqlParameter();
                                sParamsChk01[0].SqlDbType = SqlDbType.Int;
                                sParamsChk01[0].ParameterName = "@orderID";
                                sParamsChk01[0].Value = orderIDx;

                                sParamsChk01[1] = new SqlParameter();
                                sParamsChk01[1].SqlDbType = SqlDbType.VarChar;
                                sParamsChk01[1].ParameterName = "@packing";
                                sParamsChk01[1].Value = packing;

                                DataSet dsChk01 = new DataSet();
                                dsChk01 = cls.ExecuteDataSet(chkSQL01, sParamsChk01);
                                if (dsChk01.Tables.Count > 0)
                                {
                                    if (dsChk01.Tables[0].Rows.Count > 0)
                                    {
                                        // CHECK QUANTITY OF PACKING WITH CURRENT ORDER ITEM
                                        if (_qItem < _qTotal)
                                        {
                                            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_v2_ScanOut_AddItem_Addnew";
                                            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                                            sParams[0] = new SqlParameter();
                                            sParams[0].SqlDbType = SqlDbType.Int;
                                            sParams[0].ParameterName = "@orderID";
                                            sParams[0].Value = orderIDx;

                                            sParams[1] = new SqlParameter();
                                            sParams[1].SqlDbType = SqlDbType.VarChar;
                                            sParams[1].ParameterName = "@packing";
                                            sParams[1].Value = packing;

                                            cls.fnUpdDel(sql, sParams);

                                            fnLoadItems();
                                            fnLoadScans();
                                            fnLoadTotal();

                                            msgText = "[0006]-Scan out " + packing + " successful.";
                                            msgType = 1;
                                        }
                                        else
                                        {
                                            msgText = "[0005]-Cannot scan out more because it's FULL ORDER.";
                                            msgType = -1;
                                        }
                                    }
                                    //else
                                    //{
                                    //    msgText = "Cannot found kind of material (" + packing + ") in current request. Please re-check and try again.";
                                    //    msgType = -1;
                                    //}
                                }
                                else
                                {
                                    msgText = "[0004]-Cannot found kind of material (" + packing + ") in current request. Please re-check and try again.";
                                    msgType = -1;
                                }
                            }
                            else
                            {
                                //msgText = "[0003]-Invalid packing code.\r\n\r\n- Packing type: must be start with 'MMT'.\r\n- Packing kind: must be start with PCS/BOX/PAK/PAL.\r\n- Packing code: must be more than 5 characters.\r\n\r\nPlease re-check and try again.";
                                msgText = "[0003]-Invalid packing code. Packing must be in format: MMT-PCS/BOX/PAK/PAL-XXXXXXXXXXXXXXXX. Please re-check and try again.";
                                msgType = -1;
                            }
                        }
                        else
                        {
                            msgText = "[0002]-Invalid packing code length, please re-check and try again.";
                            msgType = -1;
                        }
                    }
                    else
                    {
                        msgText = "[0001]-Invalid packing code, please re-check and try again.";
                        msgType = -1;
                    }

                    switch (msgType)
                    {
                        case -1:
                            tssMessage.BackColor = Color.Red;
                            tssMessage.ForeColor = Color.White;

                            txtPacking.BackColor = Color.Red;
                            txtPacking.ForeColor = Color.White;
                            //MessageBox.Show(msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 0:
                            tssMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                            tssMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);

                            txtPacking.BackColor = Color.FromKnownColor(KnownColor.Control);
                            txtPacking.ForeColor = Color.Black;
                            //MessageBox.Show(msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1:
                            tssMessage.BackColor = Color.Turquoise;
                            tssMessage.ForeColor = Color.Black;

                            txtPacking.BackColor = Color.Turquoise;
                            txtPacking.ForeColor = Color.Black;
                            //MessageBox.Show(msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                    }
                    tssMessage.Text = msgText;

                    txtPacking.Text = "";
                    txtPacking.Focus();
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
