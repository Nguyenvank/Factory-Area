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
    public partial class frmInventoryScanInWIP_V1o1 : Form
    {
        public string _inPartId = "";
        public string _inPartname = "";
        public string _inPartcode = "";
        public string _inWarehouse = "";
        public string _inLocation = "";
        public string _inPackPCS = "";
        public string _inPackBOX = "";
        public string _inPackCAR = "";
        public string _inPackPAL = "";

        public string _scanId = "";
        public string _scanCode = "";
        public string _scanUse = "";
        public string _scanLoc = "";
        public string _scanQty = "";
        public string _scanUom = "";

        public string _outId = "";
        public string _outCode = "";
        public string _outWH = "";
        public string _outLoc = "";
        public string _outLot = "";
        public string _outQty = "";
        public string _outUom = "";
        public string _outDate = "";
        public string _outPID = "";

        public int _dgvItemQtyWidth;
        public int _dgvItemScanInWidth;
        public int _dgvItemInventWidth;
        public int _dgvScanOutTodayWidth;
        public int _dgvScanOutAllWidth;

        public int rowItemQtyIndex = 0;
        public int rowScanInIndex = 0;
        public int rowScanOutIndex = 0;

        public frmInventoryScanInWIP_V1o1()
        {
            InitializeComponent();
        }

        private void frmInventoryScanInWIP_V1o1_Load(object sender, EventArgs e)
        {
            _dgvItemQtyWidth = cls.fnGetDataGridWidth(dgvItemQty);
            _dgvItemScanInWidth = cls.fnGetDataGridWidth(dgvItemScanIn);
            _dgvItemInventWidth = cls.fnGetDataGridWidth(dgvItemInvent);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            lblPartname.Text = "N/A";
            lblPartcode.Text = "N/A";
            lblLocation.Text = "N/A";
            lblPCS.Text = "0";
            lblBOX.Text = "0";
            lblCAR.Text = "0";
            lblPAL.Text = "0";

            txtPackcode.Enabled = false;
            txtScanOut.Enabled = false;
            btnExcel.Enabled = false;
            btnOutExcel.Enabled = false;

            fnBindScanInPartQty();
        }

        public void fnGetdate()
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

        public void fnBindScanInPartQty()
        {
            string sql = "V2o1_BASE_Inventory_WIP_ScanIn_PartQty_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            dgvItemQty.DataSource = dt;
            dgvItemQty.Refresh();

            _dgvItemQtyWidth = cls.fnGetDataGridWidth(dgvItemQty);

            //dgvItemQty.Columns[0].Width = 10 * _dgvItemQtyWidth / 100;    // ProdId
            dgvItemQty.Columns[1].Width = 55 * _dgvItemQtyWidth / 100;    // Name
            //dgvItemQty.Columns[2].Width = 55 * _dgvItemQtyWidth / 100;    // BarCode
            dgvItemQty.Columns[3].Width = 30 * _dgvItemQtyWidth / 100;    // quantity
            dgvItemQty.Columns[4].Width = 15 * _dgvItemQtyWidth / 100;    // Uom


            dgvItemQty.Columns[0].Visible = false;
            dgvItemQty.Columns[1].Visible = true;
            dgvItemQty.Columns[2].Visible = false;
            dgvItemQty.Columns[3].Visible = true;
            dgvItemQty.Columns[4].Visible = true;

            cls.fnFormatDatagridview(dgvItemQty, 12);
        }

        private void dgvItemQty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvItemQty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvItemQty, e);

            DataGridViewRow row = new DataGridViewRow();
            row = dgvItemQty.Rows[e.RowIndex];
            rowItemQtyIndex = e.RowIndex;
            string idx = row.Cells[0].Value.ToString();
            string name = row.Cells[1].Value.ToString();
            string code = row.Cells[2].Value.ToString();
            string invent = row.Cells[3].Value.ToString();
            string unit = row.Cells[4].Value.ToString();

            lblPartname.Text = name;
            lblPartcode.Text = code;

            string chkStatus = "";
            string location = "", pcs = "", box = "", car = "", pal = "", wh = "", msg = "";
            string sql = "V2o1_BASE_Inventory_WIP_ScanIn_PartQty_Packing_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@prodId";
            sParams[0].Value = idx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            if (dt.Rows.Count > 0)
            {
                location = dt.Rows[0][0].ToString();
                pcs = dt.Rows[0][1].ToString();
                box = dt.Rows[0][2].ToString();
                car = dt.Rows[0][3].ToString();
                pal = dt.Rows[0][4].ToString();
                wh = dt.Rows[0][5].ToString();

                if (location != "" && (pcs != "0" || box != "0" || car != "0" || pal != "0"))
                {
                    //msg = "Please scan packing code for in-stock.";
                    msg = "Quét mã packing để nhập kho.";

                    chkStatus = "1";
                }
                else
                {
                    if (location == "" && (pcs != "0" && box != "0" && car != "0" && pal != "0"))
                    {
                        //msg = "Please set LOCATION and PACKING quantity.";
                        msg = "Thiết lập VỊ TRÍ và SỐ LƯỢNG cho packing.";
                    }
                    else
                    {
                        if (location == "" && (pcs != "0" || box != "0" || car != "0" || pal != "0"))
                        {
                            //msg = "Please set LOCATION for this part.";
                            msg = "Thiết lập VỊ TRÍ cho loại này.";
                        }
                        else if (location != "" && (pcs == "0" && box == "0" && car == "0" && pal == "0"))
                        {
                            //msg = "Please set PACKING quantity for this part.";
                            msg = "Thiết lập SỐ LƯỢNG của packing cho loại này.";
                        }

                    }

                    chkStatus = "0";
                }
            }
            else
            {
                //msg = "Cannot find the product information.";
                msg = "Không tìm thấy thông tin sản phẩm.";

                location = "N/A";
                pcs = "0";
                box = "0";
                car = "0";
                pal = "0";

                chkStatus = "0";
            }

            _inPartId = idx;
            _inPartname = name;
            _inPartcode = code;
            _inWarehouse = wh;
            _inLocation = location;
            _inPackPCS = pcs;
            _inPackBOX = box;
            _inPackCAR = car;
            _inPackPAL = pal;

            lblLocation.Text = (location != "" && location != null) ? wh + " \\ " + location + " (" + invent + " " + unit + ")" : "N/A";
            lblPCS.Text = pcs;
            lblBOX.Text = box;
            lblCAR.Text = car;
            lblPAL.Text = pal;

            string confirm = "";
            //confirm = "Which one do you want to process: SCAN IN or SCAN OUT?\r\n\r\n- YES: if you want to scan IN\r\n- NO: if you want to scan OUT";
            confirm = "Bạn muốn làm việc với quy trình nào: NHẬP KHO hay XUẤT KHO?\r\n\r\n- Chọn YES: nếu bạn muốn NHẬP KHO\r\n- Chọn NO: nếu bạn muốn XUẤT KHO";
            DialogResult dialogResult = MessageBox.Show(confirm, cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (chkStatus == "1")
                {
                    txtPackcode.Text = "";
                    txtPackcode.Enabled = true;
                    txtPackcode.Focus();
                }
                else
                {
                    txtPackcode.Text = "";
                    txtPackcode.Enabled = false;
                }

                txtScanOut.Text = "";
                txtScanOut.Enabled = false;

                dgvScanOutToday.DataSource = "";
                dgvScanOutToday.Refresh();
                dgvScanOutAll.DataSource = "";
                dgvScanOutAll.Refresh();

                btnOutExcel.Enabled = false;
                btnExcel.Enabled = true;

                lblOutMessage.Text = "Message";
                lblMessage.Text = msg;

                fnBindScanInToday();
            }
            else if (dialogResult == DialogResult.No)
            {
                txtPackcode.Text = "";
                txtPackcode.Enabled = false;

                txtScanOut.Text = "";
                txtScanOut.Enabled = true;
                txtScanOut.Focus();

                dgvItemScanIn.DataSource = "";
                dgvItemScanIn.Refresh();
                dgvItemInvent.DataSource = "";
                dgvItemInvent.Refresh();

                btnOutExcel.Enabled = true;
                btnExcel.Enabled = false;

                //lblOutMessage.Text = "Please scan packing code for out-stock.";
                lblOutMessage.Text = "Quét mã packing để xuất kho.";
                lblMessage.Text = "Message";

                fnBindScanOutToday();
            }
            fnBindScanInAll();
            fnBindScanOutAll();
        }

        public void fnBindScanInToday()
        {
            try
            {
                string idx = _inPartId;
                string sqlToday = "V2o1_BASE_Inventory_WIP_ScanIn_PartQty_Packing_Today_SelItem_Addnew";

                SqlParameter[] sParamsToday = new SqlParameter[1]; // Parameter count
                sParamsToday[0] = new SqlParameter();
                sParamsToday[0].SqlDbType = SqlDbType.Int;
                sParamsToday[0].ParameterName = "@prodId";
                sParamsToday[0].Value = idx;

                DataTable dtToday = new DataTable();
                dtToday = cls.ExecuteDataTable(sqlToday, sParamsToday);

                dgvItemScanIn.DataSource = dtToday;
                dgvItemScanIn.Refresh();

                _dgvItemScanInWidth = cls.fnGetDataGridWidth(dgvItemScanIn);

                //dgvItemScanIn.Columns[0].FillWeight = 10;    // boxId
                dgvItemScanIn.Columns[1].FillWeight = 38;    // boxcode
                //dgvItemScanIn.Columns[2].FillWeight = 55;    // boxuse
                dgvItemScanIn.Columns[3].FillWeight = 30;    // boxsublocate
                dgvItemScanIn.Columns[4].FillWeight = 20;    // boxquantity
                dgvItemScanIn.Columns[5].FillWeight = 12;    // Uom

                ////dgvItemScanIn.Columns[0].Width = 10;    // boxId
                //dgvItemScanIn.Columns[1].Width = 38;    // boxcode
                ////dgvItemScanIn.Columns[2].Width = 55;    // boxuse
                //dgvItemScanIn.Columns[3].Width = 30;    // boxsublocate
                //dgvItemScanIn.Columns[4].Width = 20;    // boxquantity
                //dgvItemScanIn.Columns[5].Width = 12;    // Uom

                dgvItemScanIn.Columns[0].Visible = false;
                dgvItemScanIn.Columns[1].Visible = true;
                dgvItemScanIn.Columns[2].Visible = false;
                dgvItemScanIn.Columns[3].Visible = true;
                dgvItemScanIn.Columns[4].Visible = true;
                dgvItemScanIn.Columns[5].Visible = true;

                cls.fnFormatDatagridview_FullWidth(dgvItemScanIn, 12);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnBindScanInAll()
        {
            try
            {
                string idx = _inPartId;
                string sqlAll = "V2o1_BASE_Inventory_WIP_ScanIn_PartQty_Packing_All_SelItem_Addnew";

                SqlParameter[] sParamsAll = new SqlParameter[1]; // Parameter count
                sParamsAll[0] = new SqlParameter();
                sParamsAll[0].SqlDbType = SqlDbType.Int;
                sParamsAll[0].ParameterName = "@prodId";
                sParamsAll[0].Value = idx;

                DataTable dtToday = new DataTable();
                dtToday = cls.ExecuteDataTable(sqlAll, sParamsAll);

                dgvItemInvent.DataSource = dtToday;
                dgvItemInvent.Refresh();

                _dgvItemInventWidth = cls.fnGetDataGridWidth(dgvItemInvent);

                //dgvItemInvent.Columns[0].FillWeight = 10;    // boxId
                dgvItemInvent.Columns[1].FillWeight = 34;    // boxcode
                //dgvItemInvent.Columns[2].FillWeight = 55;    // boxuse
                //dgvItemInvent.Columns[3].FillWeight = 12;    // boxlocate
                dgvItemInvent.Columns[4].FillWeight = 15;    // boxsublocate
                dgvItemInvent.Columns[5].FillWeight = 15;    // boxLOT
                dgvItemInvent.Columns[6].FillWeight = 10;    // boxquantity
                dgvItemInvent.Columns[7].FillWeight = 8;    // Uom
                dgvItemInvent.Columns[8].FillWeight = 18;    // packingdate

                ////dgvItemInvent.Columns[0].Width = 10 * _dgvItemInventWidth / 100;    // boxId
                //dgvItemInvent.Columns[1].Width = 34 * _dgvItemInventWidth / 100;    // boxcode
                ////dgvItemInvent.Columns[2].Width = 55 * _dgvItemInventWidth / 100;    // boxuse
                ////dgvItemInvent.Columns[3].Width = 12 * _dgvItemInventWidth / 100;    // boxlocate
                //dgvItemInvent.Columns[4].Width = 15 * _dgvItemInventWidth / 100;    // boxsublocate
                //dgvItemInvent.Columns[5].Width = 15 * _dgvItemInventWidth / 100;    // boxLOT
                //dgvItemInvent.Columns[6].Width = 10 * _dgvItemInventWidth / 100;    // boxquantity
                //dgvItemInvent.Columns[7].Width = 8 * _dgvItemInventWidth / 100;    // Uom
                //dgvItemInvent.Columns[8].Width = 18 * _dgvItemInventWidth / 100;    // packingdate

                dgvItemInvent.Columns[0].Visible = false;
                dgvItemInvent.Columns[1].Visible = true;
                dgvItemInvent.Columns[2].Visible = false;
                dgvItemInvent.Columns[3].Visible = false;
                dgvItemInvent.Columns[4].Visible = true;
                dgvItemInvent.Columns[5].Visible = true;
                dgvItemInvent.Columns[6].Visible = true;
                dgvItemInvent.Columns[7].Visible = true;
                dgvItemInvent.Columns[8].Visible = true;

                dgvItemInvent.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                cls.fnFormatDatagridview_FullWidth(dgvItemInvent, 12);
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvItemQty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvItemQty.Rows[e.RowIndex].Selected = true;
                rowItemQtyIndex = e.RowIndex;
                dgvItemQty.CurrentCell = dgvItemQty.Rows[e.RowIndex].Cells[3];
                contextMenuStrip1.Show(this.dgvItemQty, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void refreshThisListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnBindScanInPartQty();
        }

        private void txtPackcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string msg = "";
                try
                {
                    string packing = txtPackcode.Text.Trim();
                    string packKind = cls.Left(packing, 3).ToUpper();
                    string packType = cls.Mid(packing, 4, 3).ToUpper();
                    string packCode = cls.Right(packing, 5);

                    //MessageBox.Show(packKind + "---" + packType + "---" + packCode);

                    if (packKind == "PRO")
                    {
                        if (packType == "PCS" || packType == "BOX" || packType == "CAR" || packType == "PAL")
                        {
                            string idx = "", use = "", act = "";
                            string sqlChk = "V2o1_BASE_Inventory_WIP_ScanIn_PartQty_Packing_ChkItem_Addnew";

                            SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count
                            sParamsChk[0] = new SqlParameter();
                            sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                            sParamsChk[0].ParameterName = "@packing";
                            sParamsChk[0].Value = packing;

                            DataTable dtChk = new DataTable();
                            dtChk = cls.ExecuteDataTable(sqlChk, sParamsChk);

                            if (dtChk.Rows.Count == 0)
                            {
                                string partId = _inPartId;
                                string partname = _inPartname;
                                string partcode = _inPartcode;
                                string warehouse = _inWarehouse;
                                string location = _inLocation;
                                string lot = cls.fnGetDate("ls");
                                string quantity = "0";
                                switch (packType)
                                {
                                    case "PCS":
                                        quantity = lblPCS.Text.Trim();
                                        break;
                                    case "BOX":
                                        quantity = lblBOX.Text.Trim();
                                        break;
                                    case "CAR":
                                        quantity = lblCAR.Text.Trim();
                                        break;
                                    case "PAL":
                                        quantity = lblPAL.Text.Trim();
                                        break;
                                }
                                if (quantity != "0")
                                {
                                    string sql = "V2o1_BASE_Inventory_WIP_ScanIn_AddItem_Addnew";

                                    SqlParameter[] sParams = new SqlParameter[8]; // Parameter count
                                    sParams[0] = new SqlParameter();
                                    sParams[0].SqlDbType = SqlDbType.VarChar;
                                    sParams[0].ParameterName = "@packing";
                                    sParams[0].Value = packing;

                                    sParams[1] = new SqlParameter();
                                    sParams[1].SqlDbType = SqlDbType.NVarChar;
                                    sParams[1].ParameterName = "@warehouse";
                                    sParams[1].Value = warehouse;

                                    sParams[2] = new SqlParameter();
                                    sParams[2].SqlDbType = SqlDbType.NVarChar;
                                    sParams[2].ParameterName = "@location";
                                    sParams[2].Value = location;

                                    sParams[3] = new SqlParameter();
                                    sParams[3].SqlDbType = SqlDbType.VarChar;
                                    sParams[3].ParameterName = "@lot";
                                    sParams[3].Value = lot;

                                    sParams[4] = new SqlParameter();
                                    sParams[4].SqlDbType = SqlDbType.Int;
                                    sParams[4].ParameterName = "@quantity";
                                    sParams[4].Value = quantity;

                                    sParams[5] = new SqlParameter();
                                    sParams[5].SqlDbType = SqlDbType.NVarChar;
                                    sParams[5].ParameterName = "@partname";
                                    sParams[5].Value = partname;

                                    sParams[6] = new SqlParameter();
                                    sParams[6].SqlDbType = SqlDbType.VarChar;
                                    sParams[6].ParameterName = "@partcode";
                                    sParams[6].Value = partcode;

                                    sParams[7] = new SqlParameter();
                                    sParams[7].SqlDbType = SqlDbType.Int;
                                    sParams[7].ParameterName = "@partId";
                                    sParams[7].Value = partId;

                                    cls.fnUpdDel(sql, sParams);

                                    fnBindScanInPartQty();
                                    fnBindScanInToday();
                                    fnBindScanInAll();

                                    DataGridViewRow row = new DataGridViewRow();
                                    row = dgvItemQty.Rows[rowItemQtyIndex];
                                    row.Selected = true;

                                    //msg = "In-stock '" + packing + "' successful.";
                                    msg = "Nhập kho '" + packing + "' thành công.";
                                }
                                else
                                {
                                    //msg = "Cannot in-stock with zero quantity. Please check packing standard quantity again.";
                                    msg = "Không thể nhập kho với số lượng 0. Vui lòng kiểm tra lại số lượng packing chuẩn.";
                                }
                                //idx = dtChk.Rows[0][0].ToString();
                                //use = dtChk.Rows[0][1].ToString();
                                //act = dtChk.Rows[0][2].ToString().ToUpper();

                                //if (use != "1" && act != "IN")
                                //{
                                //}
                                //else
                                //{
                                //    msg = "This packing still in-stock, please scan OUT before scan IN or choose another valid packing.";
                                //}
                            }
                            else
                            {
                                //msg = "This packing still in-stock, please scan OUT before scan IN or choose another valid packing.";
                                msg = "Packing này vẫn trong kho, vui lòng quét xuất kho trước khi nhập kho.";
                            }
                        }
                        else
                        {
                            //msg = "Please scan valid packing type, one of these: 'PCS', 'BOX', 'CAR', 'PAL'.";
                            msg = "Vui lòng quét đúng loại packing: 'PCS', 'BOX', 'CAR', 'PAL'.";
                        }
                    }
                    else
                    {
                        //msg = "Please scan valid packing for production, must start with 'PRO'.";
                        msg = "Vui lòng quét đúng loại packing cho sản xuất, phải bắt đầu bằng 'PRO'.";
                    }

                    txtPackcode.Text = "";
                    txtPackcode.Focus();
                }
                catch
                {

                }
                finally
                {

                }
                lblMessage.Text = msg;
                //dgvItemQty_CellClick(dgvItemQty, new DataGridViewCellEventArgs(1, rowItemQtyIndex));
            }
        }

        private void dgvItemScanIn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvItemScanIn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvItemScanIn, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvItemScanIn.Rows[e.RowIndex];
            rowScanInIndex = e.RowIndex;
            string scanId = row.Cells[0].Value.ToString();
            string scanCode = row.Cells[1].Value.ToString();
            string scanUse = row.Cells[2].Value.ToString();
            string scanLoc = row.Cells[3].Value.ToString();
            string scanQty = row.Cells[4].Value.ToString();
            string scanUom = row.Cells[5].Value.ToString();

            _scanId = scanId;
            _scanCode = scanCode;
            _scanUse = scanUse;
            _scanLoc = scanLoc;
            _scanQty = scanQty;
            _scanUom = scanUom;
        }

        private void dgvItemScanIn_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvItemScanIn.Rows[e.RowIndex].Selected = true;
                rowScanInIndex = e.RowIndex;
                dgvItemScanIn.CurrentCell = dgvItemScanIn.Rows[e.RowIndex].Cells[3];
                contextMenuStrip2.Show(this.dgvItemScanIn, e.Location);
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        private void refreshThisListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fnBindScanInToday();
            fnBindScanInAll();
        }

        private void deleteThisPackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            string scanId = _scanId;
            string scanCode = _scanCode;
            string scanUse = _scanUse;
            string scanLoc = _scanLoc;
            string scanQty = _scanQty;
            string scanUom = _scanUom;
            string partId = _inPartId;

            string confirm = "";
            //confirm = "Are you sure?";
            confirm = "Bạn có chắc không?";
            DialogResult dialogResultAdd = MessageBox.Show(confirm, cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                string sql = "V2o1_BASE_Inventory_WIP_ScanIn_DelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[5]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@packId";
                sParams[0].Value = scanId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "@packing";
                sParams[1].Value = scanCode;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.NVarChar;
                sParams[2].ParameterName = "@location";
                sParams[2].Value = scanLoc;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.Int;
                sParams[3].ParameterName = "@quantity";
                sParams[3].Value = scanQty;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.Int;
                sParams[4].ParameterName = "@partId";
                sParams[4].Value = partId;

                cls.fnUpdDel(sql, sParams);

                fnBindScanInPartQty();
                fnBindScanInToday();
                fnBindScanInAll();

                dgvItemQty_CellClick(dgvItemQty, new DataGridViewCellEventArgs(1, rowItemQtyIndex));
                DataGridViewRow row = new DataGridViewRow();
                row = dgvItemQty.Rows[rowItemQtyIndex];
                row.Selected = true;

                txtPackcode.Text = "";
                txtPackcode.Focus();

                //msg = "Packing '" + scanCode + "' deleted successful.";
                msg = "Xóa packing '" + scanCode + "' thành công.";
                lblMessage.Text = msg;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            dgvItemInvent.DataSource = "";
            dgvItemInvent.Refresh();

            //lblMessage.Text = "Please wait for generate data to export.";
            lblMessage.Text = "Vui lòng kiên nhẫn trong khi trích xuất dữ liệu từ hệ thống.";
            byte export = 0;

            string sql = "V2o1_BASE_Inventory_WIP_ScanIn_Excel_SelItem_Addnew";

            DataTable dtExcel = new DataTable();
            dtExcel = cls.fnSelect(sql);

            dgvItemInvent.DataSource = dtExcel;
            dgvItemInvent.Refresh();

            _dgvItemInventWidth = cls.fnGetDataGridWidth(dgvItemInvent);

            //dgvItemInvent.Columns[0].Width = 10 * _dgvItemInventWidth / 100;    // boxId
            dgvItemInvent.Columns[1].Width = 22 * _dgvItemInventWidth / 100;    // boxcode
            //dgvItemInvent.Columns[2].Width = 55 * _dgvItemInventWidth / 100;    // boxuse
            dgvItemInvent.Columns[3].Width = 12 * _dgvItemInventWidth / 100;    // boxlocate
            dgvItemInvent.Columns[4].Width = 15 * _dgvItemInventWidth / 100;    // boxsublocate
            dgvItemInvent.Columns[5].Width = 15 * _dgvItemInventWidth / 100;    // boxLOT
            dgvItemInvent.Columns[6].Width = 10 * _dgvItemInventWidth / 100;    // boxquantity
            dgvItemInvent.Columns[7].Width = 8 * _dgvItemInventWidth / 100;    // Uom
            dgvItemInvent.Columns[8].Width = 18 * _dgvItemInventWidth / 100;    // packingdate

            dgvItemInvent.Columns[0].Visible = false;
            dgvItemInvent.Columns[1].Visible = true;
            dgvItemInvent.Columns[2].Visible = false;
            dgvItemInvent.Columns[3].Visible = true;
            dgvItemInvent.Columns[4].Visible = true;
            dgvItemInvent.Columns[5].Visible = true;
            dgvItemInvent.Columns[6].Visible = true;
            dgvItemInvent.Columns[7].Visible = true;
            dgvItemInvent.Columns[8].Visible = true;

            dgvItemInvent.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgvItemInvent, 12);

            if (dgvItemInvent.Rows.Count > 0)
            {
                export = cls.ExportToExcel(dgvItemInvent, "WIP-IN " + DateTime.Now.Year);
                //lblMessage.Text = (export == 1) ? "Export to excel successful." : "Export to excel failure. Please try again.";
                lblMessage.Text = (export == 1) ? "Trích xuất sang excel thành công." : "Trích xuất sang excel bị lỗi. Vui lòng thử lại.";
                dgvItemInvent.DataSource = "";
                dgvItemInvent.Refresh();
                if (_inPartId != "" && _inPartId != null)
                {
                    fnBindScanInAll();
                }
            }
            else
            {
                //lblMessage.Text = "Have no data to export.";
                lblMessage.Text = "Không có dữ liệu để trích xuất.";
            }
        }

        public void fnBindScanOutToday()
        {
            string idx = _inPartId;
            string sql = "V2o1_BASE_Inventory_WIP_ScanOut_Packing_Today_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@prodId";
            sParams[0].Value = idx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            dgvScanOutToday.DataSource = dt;
            dgvScanOutToday.Refresh();

            _dgvScanOutTodayWidth = cls.fnGetDataGridWidth(dgvScanOutToday);

            //dgvScanOutToday.Columns[0].Width = 10 * _dgvScanOutTodayWidth / 100;    // outstockId
            dgvScanOutToday.Columns[1].Width = 22 * _dgvScanOutTodayWidth / 100;    // boxcode
            dgvScanOutToday.Columns[2].Width = 12 * _dgvScanOutTodayWidth / 100;    // boxlocate
            dgvScanOutToday.Columns[3].Width = 15 * _dgvScanOutTodayWidth / 100;    // boxsublocate
            dgvScanOutToday.Columns[4].Width = 15 * _dgvScanOutTodayWidth / 100;    // boxLOT
            dgvScanOutToday.Columns[5].Width = 10 * _dgvScanOutTodayWidth / 100;    // boxquantity
            dgvScanOutToday.Columns[6].Width = 8 * _dgvScanOutTodayWidth / 100;    // Uom
            dgvScanOutToday.Columns[7].Width = 18 * _dgvScanOutTodayWidth / 100;    // outstockfinishdate
            //dgvScanOutToday.Columns[8].Width = 10 * _dgvScanOutTodayWidth / 100;    // PartId

            dgvScanOutToday.Columns[0].Visible = false;
            dgvScanOutToday.Columns[1].Visible = true;
            dgvScanOutToday.Columns[2].Visible = true;
            dgvScanOutToday.Columns[3].Visible = true;
            dgvScanOutToday.Columns[4].Visible = true;
            dgvScanOutToday.Columns[5].Visible = true;
            dgvScanOutToday.Columns[6].Visible = true;
            dgvScanOutToday.Columns[7].Visible = true;
            dgvScanOutToday.Columns[8].Visible = false;

            dgvScanOutToday.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgvScanOutToday, 12);
        }

        public void fnBindScanOutAll()
        {
            string idx = _inPartId;
            string sql = "V2o1_BASE_Inventory_WIP_ScanOut_Packing_All_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@prodId";
            sParams[0].Value = idx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            dgvScanOutAll.DataSource = dt;
            dgvScanOutAll.Refresh();

            _dgvScanOutAllWidth = cls.fnGetDataGridWidth(dgvScanOutAll);

            //dgvScanOutAll.Columns[0].FillWeight = 10;    // outstockId
            dgvScanOutAll.Columns[1].FillWeight = 34;    // boxcode
            //dgvScanOutAll.Columns[2].FillWeight = 12;    // boxlocate
            dgvScanOutAll.Columns[3].FillWeight = 15;    // boxsublocate
            dgvScanOutAll.Columns[4].FillWeight = 15;    // boxLOT
            dgvScanOutAll.Columns[5].FillWeight = 10;    // boxquantity
            dgvScanOutAll.Columns[6].FillWeight = 8;    // Uom
            dgvScanOutAll.Columns[7].FillWeight = 18;    // outstockfinishdate

            ////dgvScanOutAll.Columns[0].Width = 10 * _dgvScanOutAllWidth / 100;    // outstockId
            //dgvScanOutAll.Columns[1].Width = 34 * _dgvScanOutAllWidth / 100;    // boxcode
            ////dgvScanOutAll.Columns[2].Width = 12 * _dgvScanOutAllWidth / 100;    // boxlocate
            //dgvScanOutAll.Columns[3].Width = 15 * _dgvScanOutAllWidth / 100;    // boxsublocate
            //dgvScanOutAll.Columns[4].Width = 15 * _dgvScanOutAllWidth / 100;    // boxLOT
            //dgvScanOutAll.Columns[5].Width = 10 * _dgvScanOutAllWidth / 100;    // boxquantity
            //dgvScanOutAll.Columns[6].Width = 8 * _dgvScanOutAllWidth / 100;    // Uom
            //dgvScanOutAll.Columns[7].Width = 18 * _dgvScanOutAllWidth / 100;    // outstockfinishdate

            dgvScanOutAll.Columns[0].Visible = false;
            dgvScanOutAll.Columns[1].Visible = true;
            dgvScanOutAll.Columns[2].Visible = false;
            dgvScanOutAll.Columns[3].Visible = true;
            dgvScanOutAll.Columns[4].Visible = true;
            dgvScanOutAll.Columns[5].Visible = true;
            dgvScanOutAll.Columns[6].Visible = true;
            dgvScanOutAll.Columns[7].Visible = true;

            dgvScanOutAll.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview_FullWidth(dgvScanOutAll, 12);
        }

        private void txtScanOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string msg = "";
                try
                {
                    string partId = _inPartId;
                    string partname = _inPartname;
                    string partcode = _inPartcode;
                    string warehouse = _inWarehouse;
                    string location = _inLocation;

                    string packing = txtScanOut.Text.Trim();
                    string packKind = cls.Left(packing, 3).ToUpper();
                    string packType = cls.Mid(packing, 4, 3).ToUpper();
                    string packCode = cls.Right(packing, 5);

                    //MessageBox.Show(packKind + "---" + packType + "---" + packCode);

                    if (packKind == "PRO")
                    {
                        if (packType == "PCS" || packType == "BOX" || packType == "CAR" || packType == "PAL")
                        {
                            string sqlChk = "V2o1_BASE_Inventory_WIP_ScanOut_PartQty_Packing_ChkItem_Addnew";

                            SqlParameter[] sParamsChk = new SqlParameter[3]; // Parameter count
                            sParamsChk[0] = new SqlParameter();
                            sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                            sParamsChk[0].ParameterName = "@packing";
                            sParamsChk[0].Value = packing;

                            sParamsChk[1] = new SqlParameter();
                            sParamsChk[1].SqlDbType = SqlDbType.Int;
                            sParamsChk[1].ParameterName = "@partId";
                            sParamsChk[1].Value = partId;

                            sParamsChk[2] = new SqlParameter();
                            sParamsChk[2].SqlDbType = SqlDbType.NVarChar;
                            sParamsChk[2].ParameterName = "@location";
                            sParamsChk[2].Value = location;

                            DataTable dtChk = new DataTable();
                            dtChk = cls.ExecuteDataTable(sqlChk, sParamsChk);

                            if (dtChk.Rows.Count > 0)
                            {
                                string quantity = dtChk.Rows[0][3].ToString();

                                if (quantity != "0")
                                {
                                    string sql = "V2o1_BASE_Inventory_WIP_ScanOut_AddItem_Addnew";

                                    SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                                    sParams[0] = new SqlParameter();
                                    sParams[0].SqlDbType = SqlDbType.VarChar;
                                    sParams[0].ParameterName = "@packing";
                                    sParams[0].Value = packing;

                                    sParams[1] = new SqlParameter();
                                    sParams[1].SqlDbType = SqlDbType.Int;
                                    sParams[1].ParameterName = "@partId";
                                    sParams[1].Value = partId;

                                    cls.fnUpdDel(sql, sParams);

                                    fnBindScanOutToday();
                                    fnBindScanOutAll();

                                    fnBindScanInToday();
                                    fnBindScanInAll();
                                    fnBindScanInPartQty();

                                    DataGridViewRow row = new DataGridViewRow();
                                    row = dgvItemQty.Rows[rowItemQtyIndex];
                                    row.Selected = true;

                                    //msg = "Out-stock '" + packing + "' successful.";
                                    msg = "Xuất kho '" + packing + "' thành công.";
                                }
                                else
                                {
                                    //msg = "Cannot out-stock with zero quantity. Please check Scan In Packing Quantity again.";
                                    msg = "Không thể xuất kho với số lượng 0. Vui lòng kiểm tra lại số lượng nhập kho của packing.";
                                }
                            }
                            else
                            {
                                //msg = "Cannot find this packing on in-stock system, please scan IN before scan OUT or choose another valid packing.";
                                msg = "Không thấy packing được nhập kho, vui lòng quét nhập kho trước khi xuất kho.";
                            }
                        }
                        else
                        {
                            //msg = "Please scan valid packing type, one of these: 'PCS', 'BOX', 'CAR', 'PAL'.";
                            msg = "Vui lòng quét đúng loại packing: 'PCS', 'BOX', 'CAR', 'PAL'.";
                        }
                    }
                    else
                    {
                        //msg = "Please scan valid packing for production, must start with 'PRO'.";
                        msg = "Vui lòng quét đúng loại packing cho sản xuất bắt đầu bằng 'PRO'.";
                    }

                    txtScanOut.Text = "";
                    txtScanOut.Focus();
                }
                catch
                {

                }
                finally
                {

                }
                lblOutMessage.Text = msg;
                //dgvItemQty_CellClick(dgvItemQty, new DataGridViewCellEventArgs(1, rowItemQtyIndex));
            }
        }

        private void dgvScanOutToday_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvScanOutToday_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvScanOutToday, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvScanOutToday.Rows[e.RowIndex];
            rowScanOutIndex = e.RowIndex;
            string outId = row.Cells[0].Value.ToString();
            string code = row.Cells[1].Value.ToString();
            string warehouse = row.Cells[2].Value.ToString();
            string locate = row.Cells[3].Value.ToString();
            string lot = row.Cells[4].Value.ToString();
            string quantity = row.Cells[5].Value.ToString();
            string uom = row.Cells[6].Value.ToString();
            string date = row.Cells[7].Value.ToString();
            string partId = row.Cells[8].Value.ToString();

            _outId = outId;
            _outCode = code;
            _outWH = warehouse;
            _outLoc = locate;
            _outLot = lot;
            _outQty = quantity;
            _outUom = uom;
            _outDate = date;
            _outPID = partId;
        }

        private void dgvScanOutToday_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvScanOutToday.Rows[e.RowIndex].Selected = true;
                rowScanOutIndex = e.RowIndex;
                dgvScanOutToday.CurrentCell = dgvScanOutToday.Rows[e.RowIndex].Cells[3];
                contextMenuStrip3.Show(this.dgvScanOutToday, e.Location);
                contextMenuStrip3.Show(Cursor.Position);
            }

        }

        private void refreshThisListToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            fnBindScanOutToday();
            fnBindScanOutAll();
        }

        private void deleteThisPackingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string msg = "";
            string outId = _outId;
            string code = _outCode;
            string warehouse = _outWH;
            string locate = _outLoc;
            string lot = _outLot;
            string quantity = _outQty;
            string uom = _outUom;
            string date = _outDate;
            string partId = _outPID;

            string sqlChk = "V2o1_BASE_Inventory_WIP_ScanOut_Delete_Packing_ChkItem_Addnew";

            SqlParameter[] sParamsChk = new SqlParameter[3]; // Parameter count
            sParamsChk[0] = new SqlParameter();
            sParamsChk[0].SqlDbType = SqlDbType.VarChar;
            sParamsChk[0].ParameterName = "@packing";
            sParamsChk[0].Value = code;

            sParamsChk[1] = new SqlParameter();
            sParamsChk[1].SqlDbType = SqlDbType.VarChar;
            sParamsChk[1].ParameterName = "@lot";
            sParamsChk[1].Value = lot;

            sParamsChk[2] = new SqlParameter();
            sParamsChk[2].SqlDbType = SqlDbType.Int;
            sParamsChk[2].ParameterName = "@partId";
            sParamsChk[2].Value = partId;

            DataTable dtChk = new DataTable();
            dtChk = cls.ExecuteDataTable(sqlChk, sParamsChk);

            if (dtChk.Rows.Count == 0)
            {
                string confirm = "";
                //confirm = "Are you sure?";
                confirm = "Bạn có chắc không?";
                DialogResult dialogResultAdd = MessageBox.Show(confirm, cls.appName(), MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    string sql = "V2o1_BASE_Inventory_WIP_ScanOut_DelItem_Addnew";

                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@outId";
                    sParams[0].Value = outId;

                    cls.fnUpdDel(sql, sParams);

                    fnBindScanOutToday();
                    fnBindScanOutAll();

                    fnBindScanInToday();
                    fnBindScanInAll();
                    fnBindScanInPartQty();

                    dgvItemQty_CellClick(dgvItemQty, new DataGridViewCellEventArgs(1, rowItemQtyIndex));
                    DataGridViewRow row = new DataGridViewRow();
                    row = dgvItemQty.Rows[rowItemQtyIndex];
                    row.Selected = true;

                    txtScanOut.Text = "";
                    txtScanOut.Focus();

                    //msg = "Packing '" + code + "' deleted successful.";
                    msg = "Xóa packing '" + code + "' thành công.";
                }
            }
            else
            {
                string curWarehose = dtChk.Rows[0][0].ToString();
                string curLocation = dtChk.Rows[0][1].ToString();
                string curInDate = dtChk.Rows[0][2].ToString();
                string curPartname = dtChk.Rows[0][3].ToString();
                string curPartcode = dtChk.Rows[0][4].ToString();

                //msg = "This packing cannot delete, it has in-stock with " + curPartname + " at " + String.Format("{0:dd/MM/yyyy HH:mm}", curInDate);
                msg = "Không thể xóa packing này, nó đã được nhập kho với tên hàng là " + curPartname + " lúc " + String.Format("{0:dd/MM/yyyy HH:mm}", curInDate);
            }

            lblOutMessage.Text = msg;
        }

        private void btnOutExcel_Click(object sender, EventArgs e)
        {
            dgvScanOutAll.DataSource = "";
            dgvScanOutAll.Refresh();

            //lblOutMessage.Text = "Please wait for generate data to export.";
            lblOutMessage.Text = "Vui lòng kiên nhẫn trong khi trích xuất dữ liệu từ hệ thống.";

            string sqlExcel = "V2o1_BASE_Inventory_WIP_ScanOut_Excel_SelItem_Addnew";

            DataTable dtExcel = new DataTable();
            dtExcel = cls.fnSelect(sqlExcel);
            dgvScanOutAll.DataSource = dtExcel;
            dgvScanOutAll.Refresh();

            _dgvScanOutAllWidth = cls.fnGetDataGridWidth(dgvScanOutAll);

            //dgvScanOutAll.Columns[0].Width = 10 * _dgvScanOutAllWidth / 100;    // outstockId
            dgvScanOutAll.Columns[1].Width = 22 * _dgvScanOutAllWidth / 100;    // boxcode
            dgvScanOutAll.Columns[2].Width = 12 * _dgvScanOutAllWidth / 100;    // boxlocate
            dgvScanOutAll.Columns[3].Width = 15 * _dgvScanOutAllWidth / 100;    // boxsublocate
            dgvScanOutAll.Columns[4].Width = 15 * _dgvScanOutAllWidth / 100;    // boxLOT
            dgvScanOutAll.Columns[5].Width = 10 * _dgvScanOutAllWidth / 100;    // boxquantity
            dgvScanOutAll.Columns[6].Width = 8 * _dgvScanOutAllWidth / 100;    // Uom
            dgvScanOutAll.Columns[7].Width = 18 * _dgvScanOutAllWidth / 100;    // outstockfinishdate

            dgvScanOutAll.Columns[0].Visible = false;
            dgvScanOutAll.Columns[1].Visible = true;
            dgvScanOutAll.Columns[2].Visible = true;
            dgvScanOutAll.Columns[3].Visible = true;
            dgvScanOutAll.Columns[4].Visible = true;
            dgvScanOutAll.Columns[5].Visible = true;
            dgvScanOutAll.Columns[6].Visible = true;
            dgvScanOutAll.Columns[7].Visible = true;

            dgvScanOutAll.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgvScanOutAll, 12);

            if (dgvScanOutAll.Rows.Count > 0)
            {
                byte export = cls.ExportToExcel(dgvScanOutAll, "WIP-OUT " + DateTime.Now.Year);
                //lblOutMessage.Text = (export == 1) ? "Export to excel successful." : "Export to excel failure. Please try again.";
                lblOutMessage.Text = (export == 1) ? "Trích xuất dữ liệu thành công." : "Lỗi trích xuất dữ liệu. Vui lòng thử lại.";
                dgvScanOutAll.DataSource = "";
                dgvScanOutAll.Refresh();
                if (_inPartId != "" && _inPartId != null)
                {
                    fnBindScanOutAll();
                }
            }
            else
            {
                //lblMessage.Text = "Have no data to export.";
                lblMessage.Text = "Không có dữ liệu để trích xuất.";
            }

            txtScanOut.Text = "";
            txtScanOut.Focus();
        }
    }
}