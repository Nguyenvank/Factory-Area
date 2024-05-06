using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_Data.Ctrl
{
    public partial class uc_MMS_WarehouseMaterialScanOut_v1o1 : UserControl
    {
        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        string _warning = "";

        string _orderIDx = "", _orderCode = "", _orderPIC = "", _orderDate = "", _orderStatus = "", _orderFinish = "";
        string _itemIDx = "", _itemMatIDx = "", _itemMatName = "", _itemMatCode = "", _itemUnit = "", _itemOrderedQty = "", _itemScannedQty = "", _itemEquivalentQty = "", _itemMatNote = "", _itemFinish = "";
        string _packing = "";

        string _msgText = "";
        int _msgType = 0;

        public uc_MMS_WarehouseMaterialScanOut_v1o1()
        {
            InitializeComponent();

            _warning= ini.GetIniValue("FIFO", "STOP", "0").Trim();

            //cls.SetDoubleBuffer(tableLayoutPanel1, true);
            cls.SetDoubleBuffer(pgr_Scanned, true);
            cls.SetDoubleBuffer(dgv_Ordered_List, true);
            cls.SetDoubleBuffer(dgv_Ordered_Items, true);
            cls.SetDoubleBuffer(dgv_Scanned_Items, true);
        }

        private void Uc_MMS_WarehouseMaterialScanOut_v1o1_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            init_Load_Controls();
        }

        public void init_Load_Controls()
        {
            Fnc_Load_Controls();
        }


        /****************************************************/

        public void Fnc_Load_Controls()
        {
            lbl_Ordered_Qty.Text = lbl_Scanned_Qty.Text = "0";
            lbl_Ordered_Pack.Text = lbl_Scanned_Pack.Text = "";
            pgr_Scanned.Minimum = pgr_Scanned.Maximum = pgr_Scanned.Value = 0;
            txt_Packcode.Text = txt_Ordered_Filter.Text = txt_Scanned_Filter.Text = "";
            txt_Packcode.Enabled = tbl_Packcode.Enabled = txt_Ordered_Filter.Enabled = txt_Scanned_Filter.Enabled = false;
            tbl_Packcode.BackColor = txt_Packcode.BackColor = Color.Gainsboro;
            dtp_Date.MaxDate = DateTime.Now;
            dgv_Ordered_List.DataSource = dgv_Ordered_Items.DataSource = dgv_Scanned_Items.DataSource = null;
            lbl_Seletected_Item.Text = "N/A";
        }

        public void Fnc_Load_Ordered_List()
        {
            DateTime date = dtp_Date.Value;
            string orderStatus = "";
            bool _orderStatus = false;
            int listCount = 0;
            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Orders_List_SelItem_V2o2_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Ordered_List_Width = cls.fnGetDataGridWidth(dgv_Ordered_List);
            dgv_Ordered_List.DataSource = dt;

            dgv_Ordered_List.Columns[0].Width = 10 * dgv_Ordered_List_Width / 100;    // No.
            //dgv_Ordered_List.Columns[1].Width = 5 * dgv_Ordered_List_Width / 100;    // idx.
            dgv_Ordered_List.Columns[2].Width = 25 * dgv_Ordered_List_Width / 100;    // orderCode.
            dgv_Ordered_List.Columns[3].Width = 35 * dgv_Ordered_List_Width / 100;    // PICname.
            dgv_Ordered_List.Columns[4].Width = 30 * dgv_Ordered_List_Width / 100;    // orderDate.
            //dgv_Ordered_List.Columns[5].Width = 5 * dgv_Ordered_List_Width / 100;    // orderStatus.
            //dgv_Ordered_List.Columns[6].Width = 6 * dgv_Ordered_List_Width / 100;    // finish.

            dgv_Ordered_List.Columns[0].Visible = true;
            dgv_Ordered_List.Columns[1].Visible = false;
            dgv_Ordered_List.Columns[2].Visible = true;
            dgv_Ordered_List.Columns[3].Visible = true;
            dgv_Ordered_List.Columns[4].Visible = true;
            dgv_Ordered_List.Columns[5].Visible = false;
            dgv_Ordered_List.Columns[6].Visible = false;

            dgv_Ordered_List.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            cls.fnFormatDatagridview(dgv_Ordered_List, 11, 30);

            txt_Ordered_Filter.Text = "";
            txt_Ordered_Filter.Enabled = (listCount > 0) ? true : false;

            foreach(DataGridViewRow row in dgv_Ordered_List.Rows)
            {
                orderStatus = row.Cells[5].Value.ToString();
                _orderStatus = (orderStatus.ToLower() == "1") ? true : false;

                if (_orderStatus == true)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        public void Fnc_Load_Ordered_Item()
        {
            string orderIDx = _orderIDx, orderedQty = "", scannedQty = "", finish = "";
            int _orderedQty = 0, _scannedQty = 0;
            bool _finish = false;
            int listCount = 0;
            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Orders_Items_SelItem_V2o2_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@orderIDx";
            sParams[0].Value = orderIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Ordered_Items_Width = cls.fnGetDataGridWidth(dgv_Ordered_Items);
            dgv_Ordered_Items.DataSource = dt;

            dgv_Ordered_Items.Columns[0].Width = 5 * dgv_Ordered_Items_Width / 100;    // No.
            //dgv_Ordered_Items.Columns[1].Width = 10 * dgv_Ordered_Items_Width / 100;    // idx.
            //dgv_Ordered_Items.Columns[2].Width = 10 * dgv_Ordered_Items_Width / 100;    // materialID.
            dgv_Ordered_Items.Columns[3].Width = 25 * dgv_Ordered_Items_Width / 100;    // materialName.
            dgv_Ordered_Items.Columns[4].Width = 20 * dgv_Ordered_Items_Width / 100;    // materialCode.
            dgv_Ordered_Items.Columns[5].Width = 5 * dgv_Ordered_Items_Width / 100;    // orderUnit.
            dgv_Ordered_Items.Columns[6].Width = 10 * dgv_Ordered_Items_Width / 100;    // orderQuantity.
            dgv_Ordered_Items.Columns[7].Width = 10 * dgv_Ordered_Items_Width / 100;    // Total.
            dgv_Ordered_Items.Columns[8].Width = 10 * dgv_Ordered_Items_Width / 100;    // equivalentQty.
            dgv_Ordered_Items.Columns[9].Width = 15 * dgv_Ordered_Items_Width / 100;    // materialNote.
            //dgv_Ordered_Items.Columns[10].Width = 20 * dgv_Ordered_Items_Width / 100;    // finish.

            dgv_Ordered_Items.Columns[0].Visible = true;
            dgv_Ordered_Items.Columns[1].Visible = false;
            dgv_Ordered_Items.Columns[2].Visible = false;
            dgv_Ordered_Items.Columns[3].Visible = true;
            dgv_Ordered_Items.Columns[4].Visible = true;
            dgv_Ordered_Items.Columns[5].Visible = true;
            dgv_Ordered_Items.Columns[6].Visible = true;
            dgv_Ordered_Items.Columns[7].Visible = true;
            dgv_Ordered_Items.Columns[8].Visible = true;
            dgv_Ordered_Items.Columns[9].Visible = true;
            dgv_Ordered_Items.Columns[10].Visible = false;

            cls.fnFormatDatagridview(dgv_Ordered_Items, 11, 30);

            foreach(DataGridViewRow row in dgv_Ordered_Items.Rows)
            {
                finish = row.Cells[10].Value.ToString();
                _finish = (finish == "1") ? true : false;

                if (_finish == true) { row.DefaultCellStyle.BackColor = Color.LightGreen; }

                //orderedQty = row.Cells[6].Value.ToString();
                //scannedQty = row.Cells[7].Value.ToString();

                //_orderedQty = (orderedQty != "" && orderedQty != null) ? Convert.ToInt32(orderedQty) : 0;
                //_scannedQty = (scannedQty != "" && scannedQty != null) ? Convert.ToInt32(scannedQty) : 0;

                //if (_scannedQty >= _orderedQty)
                //{
                //    row.DefaultCellStyle.BackColor = Color.LightGreen;
                //}
            }
        }

        public void Fnc_Load_Scanned_Item()
        {
            string orderIDx = _orderIDx, itemIDx = _itemMatIDx;
            int listCount = 0;
            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Scanned_Items_SelItem_V2o2_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@orderIDx";
            sParams[0].Value = orderIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "@itemIDx";
            sParams[1].Value = itemIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Scanned_Items_Width = cls.fnGetDataGridWidth(dgv_Scanned_Items);
            dgv_Scanned_Items.DataSource = dt;

            dgv_Scanned_Items.Columns[0].Width = 5 * dgv_Scanned_Items_Width / 100;    // No.
            //dgv_Scanned_Items.Columns[1].Width = 10 * dgv_Scanned_Items_Width / 100;    // inoutID.
            dgv_Scanned_Items.Columns[2].Width = 20 * dgv_Scanned_Items_Width / 100;    // packingCode.
            dgv_Scanned_Items.Columns[3].Width = 10 * dgv_Scanned_Items_Width / 100;    // importLOT.
            dgv_Scanned_Items.Columns[4].Width = 10 * dgv_Scanned_Items_Width / 100;    // transferSublocate.
            dgv_Scanned_Items.Columns[5].Width = 5 * dgv_Scanned_Items_Width / 100;    // uom.
            dgv_Scanned_Items.Columns[6].Width = 7 * dgv_Scanned_Items_Width / 100;    // packingQty.
            dgv_Scanned_Items.Columns[7].Width = 7 * dgv_Scanned_Items_Width / 100;    // packingUnit.
            dgv_Scanned_Items.Columns[8].Width = 18 * dgv_Scanned_Items_Width / 100;    // IN_Date.
            dgv_Scanned_Items.Columns[9].Width = 18 * dgv_Scanned_Items_Width / 100;    // OUT_Date.

            dgv_Scanned_Items.Columns[0].Visible = true;
            dgv_Scanned_Items.Columns[1].Visible = false;
            dgv_Scanned_Items.Columns[2].Visible = true;
            dgv_Scanned_Items.Columns[3].Visible = true;
            dgv_Scanned_Items.Columns[4].Visible = true;
            dgv_Scanned_Items.Columns[5].Visible = true;
            dgv_Scanned_Items.Columns[6].Visible = true;
            dgv_Scanned_Items.Columns[7].Visible = true;
            dgv_Scanned_Items.Columns[8].Visible = true;
            dgv_Scanned_Items.Columns[9].Visible = true;

            dgv_Scanned_Items.Columns[8].DefaultCellStyle.Format =
                dgv_Scanned_Items.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            cls.fnFormatDatagridview(dgv_Scanned_Items, 11, 30);

            txt_Scanned_Filter.Text = "";
            txt_Scanned_Filter.Enabled = (listCount > 0) ? true : false;
        }

        public void Fnc_Load_Scanned_Qty()
        {
            string orderIDx = _orderIDx, itemIDx = _itemMatIDx;
            string scannedQty = "", orderedQty = "", scannedPack = "", orderedPack = "", unitPack = "";
            int _scannedQty = 0, _orderedQty = 0, _scannedPack = 0, _orderedPack = 0, _rate = 0;
            bool _readyScan = false;
            int listCount = 0, rowCount = 0;
            string sql = "V2o1_BASE_Warehouse_Material_ScanOut_Scanned_Qty_SelItem_V2o2_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@orderIDx";
            sParams[0].Value = orderIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "@itemIDx";
            sParams[1].Value = itemIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            listCount = ds.Tables.Count;
            rowCount = ds.Tables[0].Rows.Count;

            if (listCount > 0 && rowCount > 0)
            {
                scannedQty = ds.Tables[0].Rows[0][1].ToString();
                orderedQty = ds.Tables[0].Rows[0][2].ToString();
                scannedPack = ds.Tables[0].Rows[0][3].ToString();
                orderedPack = ds.Tables[0].Rows[0][4].ToString();
                unitPack = ds.Tables[0].Rows[0][5].ToString();
            }
            else
            {
                scannedQty = orderedQty = scannedPack = orderedPack = "0";
                unitPack = "";
            }

            _scannedQty = (scannedQty != "" && scannedQty != null) ? Convert.ToInt32(scannedQty) : 0;
            _orderedQty = (orderedQty != "" && orderedQty != null) ? Convert.ToInt32(orderedQty) : 0;
            _scannedPack = (scannedPack != "" && scannedPack != null) ? Convert.ToInt32(scannedPack) : 0;
            _orderedPack = (orderedPack != "" && orderedPack != null) ? Convert.ToInt32(orderedPack) : 0;
            _rate = _scannedPack * 100 / _orderedPack;

            pgr_Scanned.Minimum = 0;
            //pgr_Scanned.Maximum = _orderedPack;
            //pgr_Scanned.Value = _scannedPack;
            pgr_Scanned.Maximum = _orderedQty;
            pgr_Scanned.Value = (_scannedQty <= _orderedQty) ? _scannedQty : _orderedQty;

            lbl_Scanned_Qty.Text = String.Format("{0:n0}", _scannedQty);
            lbl_Ordered_Qty.Text = String.Format("{0:n0}", _orderedQty);
            lbl_Scanned_Pack.Text = String.Format("{0:n0}", _scannedPack) + " " + unitPack;
            lbl_Scanned_Pack.Text += (_scannedPack > 0) ? " (" + _rate + "%)" : "";
            lbl_Ordered_Pack.Text = String.Format("{0:n0}", _orderedPack) + " " + unitPack + "";

            //_readyScan = (_scannedPack < _orderedPack) ? true : false;
            _readyScan = (_scannedQty < _orderedQty) ? true : false;
            tbl_Packcode.Enabled = txt_Packcode.Enabled = _readyScan;
            if (_readyScan == true)
            {
                tbl_Packcode.BackColor = txt_Packcode.BackColor = Color.White;
                txt_Packcode.Focus();
            }
            else
            {
                tbl_Packcode.BackColor = txt_Packcode.BackColor = Color.Gainsboro;
            }
        }

        public void Fnc_Save_Scanned_Item()
        {
            try
            {
                string orderIDx = "", itemIDx = "", packing = "";
                orderIDx = _orderIDx;
                itemIDx = _itemMatIDx;
                packing = _packing;

                string sql = "V2o1_BASE_Warehouse_Material_ScanOut_ScanPacking_Addnew";
                SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderID";
                sParams[0].Value = orderIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@itemID";
                sParams[1].Value = itemIDx;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.VarChar;
                sParams[2].ParameterName = "@packingCode";
                sParams[2].Value = packing;

                cls.fnUpdDel(sql, sParams);

                _msgText = "";
                _msgType = 0;

                Fnc_Load_Ordered_List();
                Fnc_Load_Ordered_Item();
                Fnc_Load_Scanned_Item();
                Fnc_Load_Scanned_Qty();
            }
            catch (SqlException sqlEx)
            {
                _msgText = "Có lỗi kết nối dữ liệu";
                _msgType = 2;
                MessageBox.Show(_msgText + "\r\n\r\n" + sqlEx.Message);
            }
            catch(Exception ex)
            {
                _msgText = "Có lỗi phát sinh";
                _msgType = 3;
                MessageBox.Show(_msgText + "\r\n\r\n" + ex.Message);
            }
            finally
            {

            }
        }

        public void Fnc_Test()
        {
            //string s = "3 box";
            //int idx = s.IndexOf(" ") + 1;
            //string s2 = s.Substring(idx);
            //MessageBox.Show("unit: " + s2);
        }


        /****************************************************/


        private void Btn_Load_Click(object sender, EventArgs e)
        {
            Fnc_Load_Controls();
            Fnc_Load_Ordered_List();
        }

        private void Txt_Ordered_Filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_Ordered_List, txt_Ordered_Filter, 3);
        }

        private void Dgv_Ordered_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string orderIDx = "", orderCode = "", orderPIC = "", orderDate = "", orderStatus = "", orderFinish = "";
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Ordered_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Ordered_List.Rows[e.RowIndex];

                _orderIDx = orderIDx = row.Cells[1].Value.ToString();
                _orderCode = orderCode = row.Cells[2].Value.ToString();
                _orderPIC = orderPIC = row.Cells[3].Value.ToString();
                _orderDate = orderDate = row.Cells[4].Value.ToString();
                _orderStatus = orderStatus = row.Cells[5].Value.ToString();
                _orderFinish = orderFinish = row.Cells[1].Value.ToString();

                tbl_Packcode.Enabled = txt_Packcode.Enabled = false;
                tbl_Packcode.BackColor = txt_Packcode.BackColor = Color.Gainsboro;
                pgr_Scanned.Minimum = pgr_Scanned.Maximum = pgr_Scanned.Value = 0;
                lbl_Ordered_Qty.Text = lbl_Scanned_Qty.Text = "0";
                lbl_Ordered_Pack.Text = lbl_Scanned_Pack.Text = "";
                lbl_Seletected_Item.Text = "N/A";
                dgv_Scanned_Items.DataSource = null;

                Fnc_Load_Ordered_Item();
            }
        }

        private void Txt_Scanned_Filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_Scanned_Items, txt_Scanned_Filter, 2);
        }

        private void Dgv_Ordered_Items_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string itemIDx = "", itemMatIDx = "", itemMatName = "", itemMatCode = "", itemUnit = "", itemOrderedQty = "", itemScannedQty = "", itemEquivalentQty = "", itemMatNote = "", itemFinish = "";
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Ordered_Items, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Ordered_Items.Rows[e.RowIndex];

                _itemIDx = itemIDx = row.Cells[1].Value.ToString();
                _itemMatIDx = itemMatIDx = row.Cells[2].Value.ToString();
                _itemMatName = itemMatName = row.Cells[3].Value.ToString();
                _itemMatCode = itemMatCode = row.Cells[4].Value.ToString();
                _itemUnit = itemUnit = row.Cells[5].Value.ToString();
                _itemOrderedQty = itemOrderedQty = row.Cells[6].Value.ToString();
                _itemScannedQty = itemScannedQty = row.Cells[7].Value.ToString();
                _itemEquivalentQty = itemEquivalentQty = row.Cells[8].Value.ToString();
                _itemMatNote = itemMatNote = row.Cells[9].Value.ToString();
                _itemFinish = itemFinish = row.Cells[10].Value.ToString();

                tbl_Packcode.Enabled = txt_Packcode.Enabled = false;
                tbl_Packcode.BackColor = txt_Packcode.BackColor = Color.Gainsboro;
                lbl_Seletected_Item.Text = itemMatName + " (" + itemMatCode + ")";


                Fnc_Load_Scanned_Qty();
                Fnc_Load_Scanned_Item();
            }
        }

        private void Dgv_Scanned_Items_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Scanned_Items, e);
            }
        }

        private void Txt_Packcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string itemIDx = "", itemMatIDx = "", itemMatName = "", itemMatCode = "", itemUnit = "", itemOrderedQty = "",
                    itemScannedQty = "", itemEquivalentQty = "", itemEquivalentUnit = "", itemMatNote = "", itemFinish = "",
                    equivUnit = "", packing = "", packKind = "", packType = "", packCode = "", sql = "", orderIDx = "",
                    chkPack = "", chkFIFO = "", chkPart = "", chkExist = "", chkQty = "", fifo_msg="";
                int itemEquivIndex = 0, listCount = 0, rowCount = 0, warning = 0;
                bool _chkPack = false, _chkFIFO = false, _chkPart = false, _chkExist = false, _chkQty = false;

                orderIDx = _orderIDx;
                itemIDx = _itemIDx;
                itemMatIDx = _itemMatIDx;
                itemMatName = _itemMatName;
                itemMatCode = _itemMatCode;
                itemUnit = _itemUnit;
                itemOrderedQty = _itemOrderedQty;
                itemScannedQty = _itemScannedQty;
                itemEquivalentQty = _itemEquivalentQty;
                itemEquivIndex = itemEquivalentQty.IndexOf(" ") + 1;
                itemEquivalentUnit = itemEquivalentQty.Substring(itemEquivIndex);
                itemMatNote = _itemMatNote;
                itemFinish = _itemFinish;
                warning = (_warning == "1") ? 1 : 0;

                switch (itemEquivalentUnit.ToLower().Substring(0, 3))
                {
                    case "pie":
                        equivUnit = "PCS";
                        break;
                    case "box":
                        equivUnit = "BOX";
                        break;
                    case "pac":
                        equivUnit = "PAK";
                        break;
                    case "pal":
                        equivUnit = "PAL";
                        break;
                }

                packing = txt_Packcode.Text.Trim();
                if (packing.Length > 0)
                {
                    packKind = packing.Substring(0, 3);
                    packType = packing.Substring(4, 3);
                    packCode = packing.Substring(5);

                    if (packKind.ToLower() == "mmt" && packType.ToLower() == equivUnit.ToLower() && packCode.Length >= 12)
                    {
                        _packing = packing;
                        sql = "V2o1_BASE_Warehouse_Material_ScanOut_Scanned_Items_ChkItem_V2o2_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@orderIDx";
                        sParams[0].Value = orderIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@itemIDx";
                        sParams[1].Value = itemMatIDx;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.VarChar;
                        sParams[2].ParameterName = "@packing";
                        sParams[2].Value = packing;

                        DataSet ds = new DataSet();
                        ds = cls.ExecuteDataSet(sql, sParams);
                        listCount = ds.Tables.Count;
                        rowCount = ds.Tables[0].Rows.Count;
                        //MessageBox.Show(orderIDx + " - " + itemMatIDx + " - " + packing + "\r\n\r\n" + listCount + " | " + rowCount);

                        if (listCount > 0 && rowCount > 0)
                        {
                            chkPack = ds.Tables[0].Rows[0][3].ToString();
                            chkFIFO = ds.Tables[0].Rows[0][4].ToString();
                            chkPart = ds.Tables[0].Rows[0][5].ToString();
                            chkExist = ds.Tables[0].Rows[0][6].ToString();
                            chkQty = ds.Tables[0].Rows[0][7].ToString();

                            _chkPack = (chkPack.ToLower() == "1") ? true : false;
                            _chkFIFO = (chkFIFO.ToLower() == "0") ? true : false;
                            _chkPart = (chkPart.ToLower() == "1") ? true : false;
                            _chkExist = (chkExist.ToLower() == "1") ? true : false;
                            _chkQty = (chkQty.ToLower() == "1") ? true : false;

                            if (_chkPack == true && _chkFIFO == true && _chkPart == true && _chkExist == true && _chkQty == true)
                            {
                                Fnc_Save_Scanned_Item();
                            }
                            else
                            {
                                if (_chkPack == false)
                                {
                                    _msgText = "Loại packing không đúng với loại được yêu cầu";
                                }
                                else if (_chkFIFO == false)
                                {
                                    string msg1 = "Hệ thống ghi nhận có 1 kiện (hoặc nhiều hơn) ngày nhập cũ hơn của kiện đang xuất";
                                    string msg2 = "\r\n\r\n";
                                    string msg3 = "Have more 1 packing scanned older date than current packing";
                                    string msg4 = "\r\n\r\n\r\n";
                                    string msg5 = "-------------------------------------------------------------";

                                    switch (warning)
                                    {
                                        case 0:
                                            //MessageBox.Show(msg1 + msg2 + msg3 + msg4 + msg5);
                                            _msgText = msg1 + msg2 + msg3 + msg4 + msg5;
                                            _msgType = 3;
                                            break;
                                        case 1:
                                            DialogResult dialog = MessageBox.Show(msg1 + msg2 + msg3 + msg4 + msg5 + "\r\nCó chắc là vẫn muốn xuất kiện này\r\nAre you sure to continue?", cls.appName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                            if (dialog == DialogResult.Yes)
                                            {
                                                Fnc_Save_Scanned_Item();
                                            }
                                            _msgType = 0;
                                            break;
                                    }
                                    //_msgText = "Kiện xuất không được FIFO";
                                }
                                else if (_chkPart == false)
                                {
                                    _msgText = "Không đúng loại hàng đang xuất hoặc tem này đã xuất rồi";
                                    _msgType = 3;
                                }
                                else if (_chkExist == false)
                                {
                                    _msgText = "Tem không được tìm thấy trên hệ thống";
                                    _msgType = 3;
                                }
                                else if (_chkQty == false)
                                {
                                    _msgText = "Số lượng xuất không thể lớn hơn số lượng yêu cầu";
                                    _msgType = 3;
                                }
                            }
                        }
                        else
                        {
                            _msgText = "Thông tin tem mã vạch đã nhập không được tìm thấy";
                            _msgType = 3;
                        }
                    }
                    else
                    {
                        if (packKind.ToLower() != "mmt")
                        {
                            _msgText = "Tem không đúng định dạng của MMT";
                            _msgType = 3;
                        }
                        else if (packType.ToLower() != equivUnit.ToLower())
                        {
                            _msgText = "Tem không đúng kiểu quy cách NVL (" + equivUnit + ")";
                            _msgType = 3;
                        }
                        else if (packCode.Length < 12)
                        {
                            _msgText = "Tem không đúng định dạng về độ dài ký tự (12)";
                            _msgType = 3;
                        }
                    }

                    switch (_msgType)
                    {
                        case 0:
                            break;
                        case 1:
                            MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 2:
                            MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 3:
                            MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                    }

                    _msgText = "";
                    _msgType = 0;
                }
                else
                {
                    _msgText = "";
                    _msgType = 0;
                }

                txt_Packcode.Text = "";
                txt_Packcode.Focus();

                //if (_msgType > 0)
                //{
                //    //switch (_msgType)
                //    //{
                //    //    case 0:
                //    //        break;
                //    //    case 1:
                //    //        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //        break;
                //    //    case 2:
                //    //        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //        break;
                //    //    case 3:
                //    //        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    //        break;
                //    //}

                //    //_msgText = "";
                //    //_msgType = 0;
                //}
            }
        }

    }
}
