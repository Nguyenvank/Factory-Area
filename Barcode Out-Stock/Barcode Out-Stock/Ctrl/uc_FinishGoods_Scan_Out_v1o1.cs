using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_Data.Ctrl
{
    public partial class uc_FinishGoods_Scan_Out_v1o1 : UserControl
    {

        string _doIDx = "", _doCode = "", _doDate = "", _doStatus = "", _doFinish = "";
        string _itemIDx = "", _partIDx = "", _partName = "", _partCode = "", _shipTo = "", _itemUnit = "", _itemQty = "", _finishNote = "", _itemStatus = "";

        int __orderedQty = 0, __scannedQty = 0, __gapQty = 0;

        string _msgText = "";
        int _msgType = 0;

        public uc_FinishGoods_Scan_Out_v1o1()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(dgv_Orders, true);
            cls.SetDoubleBuffer(dgv_Scanned, true);
        }

        private void uc_FinishGoods_Scan_Out_v1o1_Load(object sender, EventArgs e)
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


        /*****************************************************/


        public void Fnc_Load_Controls()
        {
            dtp_Date.MaxDate = DateTime.Now;
            btn_Load.Enabled = true;
            txt_Filter_Product.Enabled =
                txt_Filter_Packing.Enabled =
                txt_Barcode.Enabled =
                pgr_Scanned.Enabled = false;
            dgv_Orders.DataSource = 
                dgv_Scanned.DataSource = null;
            lbl_Product.Text =
                lbl_Order.Text =
                lbl_Scanned.Text =
                txt_Filter_Product.Text =
                txt_Filter_Packing.Text =
                txt_Barcode.Text =
                lbl_Product_Title.Text = "";
            progressBar1.Minimum =
                progressBar1.Maximum =
                progressBar1.Value = 0;
        }

        public void Fnc_Load_Orders_List()
        {
            DateTime date = dtp_Date.Value;
            string finish = "", scannedQty = "";
            bool _finish = false;
            int listCount = 0, _scannedQty = 0;
            string sql = "V2o1_BASE_FinishGoods_OUT_Orders_List_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Orders_Width = cls.fnGetDataGridWidth(dgv_Orders);
            dgv_Orders.DataSource = dt;

            dgv_Orders.Columns[0].Width = 10 * dgv_Orders_Width / 100;    // No.
            //dgv_Orders.Columns[1].Width = 5 * dgv_Orders_Width / 100;    // doID.
            //dgv_Orders.Columns[2].Width = 15 * dgv_Orders_Width / 100;    // doCode.
            //dgv_Orders.Columns[3].Width = 5 * dgv_Orders_Width / 100;    // doDate.
            //dgv_Orders.Columns[4].Width = 5 * dgv_Orders_Width / 100;    // doStatus.
            //dgv_Orders.Columns[5].Width = 5 * dgv_Orders_Width / 100;    // idx.
            //dgv_Orders.Columns[6].Width = 5 * dgv_Orders_Width / 100;    // partID.
            dgv_Orders.Columns[7].Width = 35 * dgv_Orders_Width / 100;    // partname.
            dgv_Orders.Columns[8].Width = 20 * dgv_Orders_Width / 100;    // partcode.
            dgv_Orders.Columns[9].Width = 15 * dgv_Orders_Width / 100;    // shipTo.
            dgv_Orders.Columns[10].Width = 10 * dgv_Orders_Width / 100;    // doUnit.
            dgv_Orders.Columns[11].Width = 10 * dgv_Orders_Width / 100;    // doQuantity.
            //dgv_Orders.Columns[12].Width = 10 * dgv_Orders_Width / 100;    // doFinish.
            //dgv_Orders.Columns[13].Width = 5 * dgv_Orders_Width / 100;    // finishNote.
            //dgv_Orders.Columns[14].Width = 5 * dgv_Orders_Width / 100;    // itemStatus.
            //dgv_Orders.Columns[15].Width = 5 * dgv_Orders_Width / 100;    // scannedTotal.

            dgv_Orders.Columns[0].Visible = true;
            dgv_Orders.Columns[1].Visible = false;
            dgv_Orders.Columns[2].Visible = false;
            dgv_Orders.Columns[3].Visible = false;
            dgv_Orders.Columns[4].Visible = false;
            dgv_Orders.Columns[5].Visible = false;
            dgv_Orders.Columns[6].Visible = false;
            dgv_Orders.Columns[7].Visible = true;
            dgv_Orders.Columns[8].Visible = true;
            dgv_Orders.Columns[9].Visible = true;
            dgv_Orders.Columns[10].Visible = true;
            dgv_Orders.Columns[11].Visible = true;
            dgv_Orders.Columns[12].Visible = false;
            dgv_Orders.Columns[13].Visible = false;
            dgv_Orders.Columns[14].Visible = false;
            dgv_Orders.Columns[15].Visible = false;

            cls.fnFormatDatagridview(dgv_Orders, 11, 30);

            txt_Filter_Product.Text = "";
            txt_Filter_Product.Enabled = (listCount > 0) ? true : false;

            foreach(DataGridViewRow row in dgv_Orders.Rows)
            {
                finish = row.Cells[12].Value.ToString();
                _finish = (finish == "1") ? true : false;

                scannedQty = row.Cells[15].Value.ToString();
                _scannedQty = (scannedQty != "" && scannedQty != null) ? Convert.ToInt32(scannedQty) : 0;

                if (_finish == true) { row.DefaultCellStyle.BackColor = Color.LightGreen; }
                if (_scannedQty == 0) { row.DefaultCellStyle.BackColor = Color.Gold; }

            }
        }

        public void Fnc_Load_Scanned_List()
        {
            string doIDx = _doIDx;
            string partIDx = _partIDx;
            int listCount = 0, rowCount = 0;
            string sql = "V2o1_BASE_FinishGoods_OUT_Scanned_List_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@doIDx";
            sParams[0].Value = doIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "@partIDx";
            sParams[1].Value = partIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Scanned_Width = cls.fnGetDataGridWidth(dgv_Scanned);
            dgv_Scanned.DataSource = dt;

            dgv_Scanned.Columns[0].Width = 5 * dgv_Scanned_Width / 100;    // No.
            //dgv_Scanned.Columns[1].Width = 5 * dgv_Scanned_Width / 100;    // boxId.
            dgv_Scanned.Columns[2].Width = 30 * dgv_Scanned_Width / 100;    // boxcode.
            //dgv_Scanned.Columns[3].Width = 10 * dgv_Scanned_Width / 100;    // boxsublocate.
            //dgv_Scanned.Columns[4].Width = 10 * dgv_Scanned_Width / 100;    // boxLOT.
            //dgv_Scanned.Columns[5].Width = 5 * dgv_Scanned_Width / 100;    // prodId.
            //dgv_Scanned.Columns[6].Width = 5 * dgv_Scanned_Width / 100;    // boxpartname.
            dgv_Scanned.Columns[7].Width = 20 * dgv_Scanned_Width / 100;    // boxpartno.
            dgv_Scanned.Columns[8].Width = 10 * dgv_Scanned_Width / 100;    // unit.
            dgv_Scanned.Columns[9].Width = 10 * dgv_Scanned_Width / 100;    // boxquantity.
            dgv_Scanned.Columns[10].Width = 10 * dgv_Scanned_Width / 100;    // receiver.
            dgv_Scanned.Columns[11].Width = 15 * dgv_Scanned_Width / 100;    // IN_Date.
            //dgv_Scanned.Columns[12].Width = 15 * dgv_Scanned_Width / 100;    // OUT_Date.

            dgv_Scanned.Columns[0].Visible = true;
            dgv_Scanned.Columns[1].Visible = false;
            dgv_Scanned.Columns[2].Visible = true;
            dgv_Scanned.Columns[3].Visible = false;
            dgv_Scanned.Columns[4].Visible = false;
            dgv_Scanned.Columns[5].Visible = false;
            dgv_Scanned.Columns[6].Visible = false;
            dgv_Scanned.Columns[7].Visible = true;
            dgv_Scanned.Columns[8].Visible = true;
            dgv_Scanned.Columns[9].Visible = true;
            dgv_Scanned.Columns[10].Visible = true;
            dgv_Scanned.Columns[11].Visible = true;
            dgv_Scanned.Columns[12].Visible = false;

            dgv_Scanned.Columns[11].DefaultCellStyle.Format =
                dgv_Scanned.Columns[12].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; 

            cls.fnFormatDatagridview(dgv_Scanned, 11, 30);

            txt_Filter_Packing.Text = "";
            txt_Filter_Packing.Enabled = (listCount > 0) ? true : false;
        }

        public void Fnc_Load_Orders_Scanned_Qty()
        {
            DateTime date = dtp_Date.Value;
            string doIDx = _doIDx;
            string itemIDx = _itemIDx;
            string partIDx = _partIDx;
            string orderedQty = "", scannedQty = "";
            int listCount = 0, rowCount = 0;
            int _orderedQty = 0, _scannedQty = 0, _percentQty = 0;
            string sql = "V2o1_BASE_FinishGoods_OUT_Orders_Scanned_Qty_SelItem_Addnew_v1o1";

            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@doIDx";
            sParams[0].Value = doIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "@itemIDx";
            sParams[1].Value = itemIDx;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "@partIDx";
            sParams[2].Value = partIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            listCount = ds.Tables.Count;
            rowCount = ds.Tables[0].Rows.Count;

            if (listCount > 0 && rowCount > 0)
            {
                orderedQty = ds.Tables[0].Rows[0][0].ToString();
                scannedQty = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                orderedQty = "0";
                scannedQty = "0";
            }

            __orderedQty = _orderedQty = (orderedQty != "" && orderedQty != null) ? Convert.ToInt32(orderedQty) : 0;
            __scannedQty = _scannedQty = (scannedQty != "" && scannedQty != null) ? Convert.ToInt32(scannedQty) : 0;
            _percentQty = _scannedQty * 100 / _orderedQty;
            __gapQty = _orderedQty - _scannedQty;

            txt_Barcode.Enabled = tbl_Barcode.Enabled = (_scannedQty < _orderedQty) ? true : false;
            if (txt_Barcode.Enabled == true) { txt_Barcode.Focus(); }
            tbl_Barcode.BackColor = txt_Barcode.BackColor = (_scannedQty < _orderedQty) ? Color.White : Color.Gainsboro;

            lbl_Order.Text = String.Format("{0:n0}", _orderedQty);
            lbl_Scanned.Text = String.Format("{0:n0}", _scannedQty);

            progressBar1.Enabled = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = _orderedQty;
            progressBar1.Value = _scannedQty;
            //progressBar1.CreateGraphics().DrawString(_percentQty.ToString() + "%", new Font("Times New Roman",
            //                              (float)10.25, FontStyle.Bold),
            //                              Brushes.Red, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));

        }

        public void Fnc_Save_Scan_Packing(string _boxID, string _doIDx, string _proIDx, string _packing,string _quantity)
        {
            try
            {
                DateTime date = DateTime.Now;
                string boxId = "", boxcode = "", boxuse = "", boxlocate = "",
                    boxsublocate = "", packingdate = "", boxLOT = "", boxquantity = "",
                    boxpartname = "", boxpartno = "", boxactivity = "", prodId = "",
                    scanIN = "", IN_Stock = "", IN_Date = "", OUT_Stock = "",
                    orderIDx = "", receiver = "", OUT_Date = "";
                string sql = "";
                SqlParameter[] sParams;

                if (_boxID != "0")
                {
                    boxId = _boxID;
                    //boxcode = _packing;
                    //boxquantity = _quantity;
                    //prodId = _proIDx;
                    orderIDx = _doIDx;
                    receiver = _shipTo;

                    sql = "V2o1_BASE_FinishGoods_OUT_Packing_BoxIDx_UpdItem_Addnew";

                    sParams = new SqlParameter[3]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@boxIDx";
                    sParams[0].Value = boxId;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@orderIDx";
                    sParams[1].Value = orderIDx;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.VarChar;
                    sParams[2].ParameterName = "@shipTo";
                    sParams[2].Value = receiver;

                    

                    cls.fnUpdDel(sql, sParams);
                }
                else
                {
                    boxcode = _packing;
                    boxquantity = _quantity;
                    boxpartname = _partName;
                    boxpartno = _partCode;
                    prodId = _proIDx;
                    orderIDx = _doIDx;
                    receiver = _shipTo;

                    /*
                    @boxcode varchar(50)
                    ,@boxquantity smallint
                    ,@boxpartname varchar(200)
                    ,@boxpartno varchar(100)
                    ,@prodId int
                    ,@orderIDx int
                    ,@receiver nvarchar(100)

                     */

                    sql = "V2o1_BASE_FinishGoods_OUT_Packing_No_BoxIDx_UpdItem_Addnew";

                    sParams = new SqlParameter[6]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.VarChar;
                    sParams[0].ParameterName = "@boxcode";
                    sParams[0].Value = boxcode;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.VarChar;
                    sParams[1].ParameterName = "@boxpartname";
                    sParams[1].Value = boxpartname;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.VarChar;
                    sParams[2].ParameterName = "@boxpartno";
                    sParams[2].Value = boxpartno;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.Int;
                    sParams[3].ParameterName = "@prodId";
                    sParams[3].Value = prodId;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.Int;
                    sParams[4].ParameterName = "@orderIDx";
                    sParams[4].Value = orderIDx;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.NVarChar;
                    sParams[5].ParameterName = "@receiver";
                    sParams[5].Value = receiver;

                    cls.fnUpdDel(sql, sParams);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }


        /*****************************************************/


        private void Btn_Load_Click(object sender, EventArgs e)
        {
            Fnc_Load_Controls();

            Fnc_Load_Orders_List();
        }

        private void Txt_Filter_Product_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_Orders, txt_Filter_Product, 7);
        }

        private void Dgv_Orders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string doIDx = "", doCode = "", doDate = "", doStatus = "", doFinish = "";
            string itemIDx = "", partIDx = "", partName = "", partCode = "", shipTo = "", itemUnit = "", itemQty = "", finishNote = "", itemStatus = "";

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Orders, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Orders.Rows[e.RowIndex];

                _doIDx = doIDx = row.Cells[1].Value.ToString();
                _doCode = doCode = row.Cells[2].Value.ToString();
                _doDate = doDate = row.Cells[3].Value.ToString();
                _doStatus = doStatus = row.Cells[4].Value.ToString();
                _itemIDx = itemIDx = row.Cells[5].Value.ToString();
                _partIDx = partIDx = row.Cells[6].Value.ToString();
                _partName = partName = row.Cells[7].Value.ToString();
                _partCode = partCode = row.Cells[8].Value.ToString();
                _shipTo = shipTo = row.Cells[9].Value.ToString();
                _itemUnit = itemUnit = row.Cells[10].Value.ToString();
                _itemQty = itemQty = row.Cells[11].Value.ToString();
                _doFinish = doFinish = row.Cells[12].Value.ToString();
                _finishNote = finishNote = row.Cells[13].Value.ToString();
                _itemStatus = itemStatus = row.Cells[14].Value.ToString();

                lbl_Product_Title.Text = partName.ToUpper() + " (" + partCode + ")";

                Fnc_Load_Scanned_List();
                Fnc_Load_Orders_Scanned_Qty();
            }
        }

        private void Txt_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            string doIDx = "", proIDx = "", pack = "", kind = "", type = "";
            string sqlCheck = "";
            DataSet ds;
            SqlParameter[] sParams;
            int listCountCheck = 0, rowCountCheck = 0, chkLen = 0;
            string boxID = "", found = "", match = "", quantity = "";
            bool _found = false, _match = false;
            int _boxID = 0, _quantity = 0;

            if (e.KeyCode == Keys.Enter)
            {
                doIDx = _doIDx;
                proIDx = _partIDx;
                pack = txt_Barcode.Text.Trim();
                if (pack.Length > 0)
                {
                    kind = pack.Substring(0, 3);
                    type = pack.Substring(4, 3);

                    //MessageBox.Show(pack + "\r\n" + kind + "\r\n" + type);
                    //return;
                    chkLen = pack.Length;

                    if (pack != "" && pack != null)
                    {
                        if (kind.ToLower() == "pro")
                        {
                            sqlCheck = "V2o1_BASE_FinishGoods_OUT_Packing_ChkItem_Addnew";

                            sParams = new SqlParameter[3]; // Parameter count
                            sParams[0] = new SqlParameter();
                            sParams[0].SqlDbType = SqlDbType.VarChar;
                            sParams[0].ParameterName = "@packing";
                            sParams[0].Value = pack;

                            sParams[1] = new SqlParameter();
                            sParams[1].SqlDbType = SqlDbType.Int;
                            sParams[1].ParameterName = "@proIDx";
                            sParams[1].Value = proIDx;

                            sParams[2] = new SqlParameter();
                            sParams[2].SqlDbType = SqlDbType.TinyInt;
                            sParams[2].ParameterName = "@chkLen";
                            sParams[2].Value = chkLen;

                            ds = new DataSet();
                            ds = cls.ExecuteDataSet(sqlCheck, sParams);
                            listCountCheck = ds.Tables.Count;
                            rowCountCheck = ds.Tables[0].Rows.Count;

                            if (listCountCheck > 0 && rowCountCheck > 0)
                            {
                                boxID = ds.Tables[0].Rows[0][0].ToString();
                                found = ds.Tables[0].Rows[0][2].ToString();
                                match = ds.Tables[0].Rows[0][3].ToString();
                                quantity = ds.Tables[0].Rows[0][4].ToString();
                            }
                            else
                            {
                                boxID = "0";
                                found = "0";
                                match = "0";
                                quantity = "0";
                            }

                            _boxID = (boxID != "" && boxID != null) ? Convert.ToInt32(boxID) : 0;
                            _found = (found.ToLower() == "1") ? true : false;
                            _match = (match.ToLower() == "1") ? true : false;
                            _quantity = (quantity != "" && quantity != null) ? Convert.ToInt32(quantity) : 0;

                            if (_found == true)
                            {
                                if (_match == true)
                                {
                                    if (_quantity > 0)
                                    {
                                        if (_quantity <= __gapQty)
                                        {
                                            Fnc_Save_Scan_Packing(boxID, doIDx, proIDx, pack, quantity);

                                            //Fnc_Load_Orders_List();
                                            Fnc_Load_Scanned_List();
                                            Fnc_Load_Orders_Scanned_Qty();
                                        }
                                        else
                                        {
                                            _msgText = "Số lượng kiện/thùng hàng không được vượt số lượng còn lại của yêu cầu";
                                            _msgType = 3;

                                            MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        _msgText = "Không thể xuất kho mà không có số lượng (QTY=0)";
                                        _msgType = 3;

                                        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    _msgText = "Hàng đang xuất không đúng loại hàng đang chọn, hãy kiểm tra lại";
                                    _msgType = 3;

                                    MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                _msgText = "Kiện/thùng hàng này chưa nhập kho, hãy nhập kho trước khi xuất hàng";
                                _msgType = 3;

                                MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            _msgText = "Kiện/thùng hàng không đúng loại của sản xuất (PRO), hãy kiểm tra lại";
                            _msgType = 3;

                            MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        _msgText = "Quét mã kiện/thùng để bắt đầu xuất hàng";
                        _msgType = 3;

                        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                txt_Barcode.Text = "";
                txt_Barcode.Focus();
            }
        }

        private void Txt_Filter_Packing_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_Scanned, txt_Filter_Packing, 2);
        }

        private void Dgv_Scanned_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Scanned, e);
            }
        }
    }
}
