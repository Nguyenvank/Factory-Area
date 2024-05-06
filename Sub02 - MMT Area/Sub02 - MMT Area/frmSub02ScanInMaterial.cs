using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmSub02ScanInMaterial : Form
    {
        frmSub04ScanOutMaterial frm04ScanOut;

        public int _dgvScanIn_List_Width;
        public int _dgvHandOver_List_Width;
        public int _dgvHandOver_Detail_Width;
        public int _dgvLocate_List_Width;
        //public int _dgvPacking_List_Width;
        public int _dgvTransfer_List_Width;

        public MemoryStream _ms;

        public string _scanIDx = "", _scanHoIDx = "", _scanMatIDx = "", _scanMatName = "", _scanMatCode = "", _scanMatQty = "", _scanMatUnit = "";
        public string _scanInStock = "", _scanInDate = "", _scanLocate = "", _scanPCS = "", _scanBOX = "", _scanPAK = "", _scanLEN = "", _scanHoDate = "";
        public string _transferPartIDx = "";

        public string _hoIDx;

        public string _msgText;
        public int _msgType;

        public string _binding_scanIn_range = "1";
        public string _binding_invent_range = "1";
        public string _binding_scanOut_range = "1";

        public int _dgvBinding_ScanIn_Width;
        public int _dgvBinding_Invent_Width;
        public int _dgvBinding_ScanOut_Width;

        public DateTime _dt;
        Timer timer = new Timer();

        public frmSub02ScanInMaterial()
        {
            InitializeComponent();
        }

        private void frmSub02ScanInMaterial_Load(object sender, EventArgs e)
        {
            _dgvScanIn_List_Width = cls.fnGetDataGridWidth(dgvScanIn_List);
            _dgvHandOver_List_Width = cls.fnGetDataGridWidth(dgvHandOver_List);
            _dgvHandOver_Detail_Width = cls.fnGetDataGridWidth(dgvHandOver_Detail);
            _dgvLocate_List_Width = cls.fnGetDataGridWidth(dgvLocate_List);
            //_dgvPacking_List_Width = cls.fnGetDataGridWidth(dgvPacking_List);
            _dgvTransfer_List_Width = cls.fnGetDataGridWidth(dgvTransfer_List);

            _dgvBinding_ScanIn_Width = cls.fnGetDataGridWidth(dgvBinding_ScanIn);
            _dgvBinding_Invent_Width = cls.fnGetDataGridWidth(dgvBinding_Inventory);
            _dgvBinding_ScanOut_Width = cls.fnGetDataGridWidth(dgvBinding_ScanOut);

            

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            initScanIN();
            initBinding_ScanIn();
        }

        public void fnGetdate()
        {
            if (check.IsConnectedToInternet())
            {
                tssDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                tssDateTime.ForeColor = Color.Black;
            }
            else
            {
                tssDateTime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt);
                tssDateTime.ForeColor = Color.Red;
            }
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 5000;
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
            timer.Stop();
        }

        private void xuấtKhoVậtTưToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm04ScanOut == null)
            {
                frm04ScanOut = new frmSub04ScanOutMaterial();
                frm04ScanOut.FormClosed += frm04ScanOut_FormClosed;
            }
            frm04ScanOut.WindowState = FormWindowState.Maximized;
            frm04ScanOut.Show(this);
            Close();
        }

        private void frm04ScanOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm04ScanOut = null;
            Show();
        }

        private void thoátKhỏiỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmSub02ScanInMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Có chắc là bạn muốn thoát?",
            //                   "Hệ thống quản lý hàng Spare Part",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Information) == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }


        #region BINDING DATA


        public void initBinding()
        {
            initBinding_ScanIn();
        }

        public void initBinding_ScanIn()
        {
            try
            {
                string rangeTime = _binding_scanIn_range;
                string sql = "V2o1_BASE_SubMaterial02_Init_Binding_ScanIn_List_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@rangeTime";
                sParams[0].Value = rangeTime;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvBinding_ScanIn_Width = cls.fnGetDataGridWidth(dgvBinding_ScanIn);
                dgvBinding_ScanIn.DataSource = dt;

                //dgvBinding_ScanIn.Columns[0].Width = 25 * _dgvBinding_ScanIn_Width / 100;    // idx
                //dgvBinding_ScanIn.Columns[1].Width = 25 * _dgvBinding_ScanIn_Width / 100;    // hoIDx
                dgvBinding_ScanIn.Columns[2].Width = 23 * _dgvBinding_ScanIn_Width / 100;    // matName
                dgvBinding_ScanIn.Columns[3].Width = 15 * _dgvBinding_ScanIn_Width / 100;    // matCode
                dgvBinding_ScanIn.Columns[4].Width = 15 * _dgvBinding_ScanIn_Width / 100;    // hoDate
                dgvBinding_ScanIn.Columns[5].Width = 20 * _dgvBinding_ScanIn_Width / 100;    // IN_Code
                //dgvBinding_ScanIn.Columns[6].Width = 25 * _dgvBinding_ScanIn_Width / 100;    // matIDx
                dgvBinding_ScanIn.Columns[7].Width = 5 * _dgvBinding_ScanIn_Width / 100;    // matUnit
                dgvBinding_ScanIn.Columns[8].Width = 7 * _dgvBinding_ScanIn_Width / 100;    // matQty
                dgvBinding_ScanIn.Columns[9].Width = 15 * _dgvBinding_ScanIn_Width / 100;    // IN_Date

                dgvBinding_ScanIn.Columns[0].Visible = false;
                dgvBinding_ScanIn.Columns[1].Visible = false;
                dgvBinding_ScanIn.Columns[2].Visible = true;
                dgvBinding_ScanIn.Columns[3].Visible = true;
                dgvBinding_ScanIn.Columns[4].Visible = true;
                dgvBinding_ScanIn.Columns[5].Visible = true;
                dgvBinding_ScanIn.Columns[6].Visible = false;
                dgvBinding_ScanIn.Columns[7].Visible = true;
                dgvBinding_ScanIn.Columns[8].Visible = true;
                dgvBinding_ScanIn.Columns[9].Visible = true;

                dgvBinding_ScanIn.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvBinding_ScanIn.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                cls.fnFormatDatagridview(dgvBinding_ScanIn, 12, 30);

                dgvBinding_ScanIn.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvBinding_ScanIn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvBinding_ScanIn, e);
            }
        }

        private void lnkBinding_ScanIn_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _binding_scanIn_range = "1";
            dgvBinding_ScanIn.DataSource = "";
            dgvBinding_ScanIn.Refresh();
            initBinding_ScanIn();
        }

        private void lnkBinding_ScanIn_2Weeks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _binding_scanIn_range = "2";
            dgvBinding_ScanIn.DataSource = "";
            dgvBinding_ScanIn.Refresh();
            initBinding_ScanIn();
        }

        private void lnkBinding_ScanIn_1Month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _binding_scanIn_range = "3";
            dgvBinding_ScanIn.DataSource = "";
            dgvBinding_ScanIn.Refresh();
            initBinding_ScanIn();
        }

        private void lnkBinding_ScanIn_3Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _binding_scanIn_range = "4";
            dgvBinding_ScanIn.DataSource = "";
            dgvBinding_ScanIn.Refresh();
            initBinding_ScanIn();
        }

        private void lnkBinding_ScanIn_6Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _binding_scanIn_range = "5";
            dgvBinding_ScanIn.DataSource = "";
            dgvBinding_ScanIn.Refresh();
            initBinding_ScanIn();
        }

        private void lnkBinding_ScanIn_1Year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _binding_scanIn_range = "6";
            dgvBinding_ScanIn.DataSource = "";
            dgvBinding_ScanIn.Refresh();
            initBinding_ScanIn();
        }

        public void initBinding_Inventory()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial02_Init_Binding_Inventory_List_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgvBinding_Invent_Width = cls.fnGetDataGridWidth(dgvBinding_Inventory);
                dgvBinding_Inventory.DataSource = dt;

                //dgvBinding_Inventory.Columns[0].Width = 25 * _dgvBinding_Invent_Width / 100;    // ProdId
                dgvBinding_Inventory.Columns[1].Width = 30 * _dgvBinding_Invent_Width / 100;    // Name
                dgvBinding_Inventory.Columns[2].Width = 26 * _dgvBinding_Invent_Width / 100;    // BarCode
                dgvBinding_Inventory.Columns[3].Width = 5 * _dgvBinding_Invent_Width / 100;    // Uom
                dgvBinding_Inventory.Columns[4].Width = 8 * _dgvBinding_Invent_Width / 100;    // 1-Day Using
                dgvBinding_Inventory.Columns[5].Width = 8 * _dgvBinding_Invent_Width / 100;    // Safety Stock
                dgvBinding_Inventory.Columns[6].Width = 8 * _dgvBinding_Invent_Width / 100;    // Total
                dgvBinding_Inventory.Columns[7].Width = 15 * _dgvBinding_Invent_Width / 100;    // IN_Date
                //dgvBinding_Inventory.Columns[8].Width = 15 * _dgvBinding_Invent_Width / 100;    // ImageStatus
                //dgvBinding_Inventory.Columns[9].Width = 15 * _dgvBinding_Invent_Width / 100;    // Picture

                dgvBinding_Inventory.Columns[0].Visible = false;
                dgvBinding_Inventory.Columns[1].Visible = true;
                dgvBinding_Inventory.Columns[2].Visible = true;
                dgvBinding_Inventory.Columns[3].Visible = true;
                dgvBinding_Inventory.Columns[4].Visible = true;
                dgvBinding_Inventory.Columns[5].Visible = true;
                dgvBinding_Inventory.Columns[6].Visible = true;
                dgvBinding_Inventory.Columns[7].Visible = true;
                dgvBinding_Inventory.Columns[7].Visible = false;
                dgvBinding_Inventory.Columns[8].Visible = false;
                dgvBinding_Inventory.Columns[9].Visible = false;

                dgvBinding_Inventory.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                cls.fnFormatDatagridview(dgvBinding_Inventory, 12, 30);

                dgvBinding_Inventory.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvBinding_Inventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvBinding_Inventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvBinding_Inventory, e);
            }
        }

        private void dgvBinding_Inventory_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void initBinding_ScanOut()
        {

        }

        private void lnkBinding_ScanOut_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_binding_scanIn_range = "1";
            //dgvBinding_ScanIn.DataSource = "";
            //dgvBinding_ScanIn.Refresh();
            //initBinding_ScanIn();
        }

        private void lnkBinding_ScanOut_2Weeks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_binding_scanIn_range = "2";
            //dgvBinding_ScanIn.DataSource = "";
            //dgvBinding_ScanIn.Refresh();
            //initBinding_ScanIn();
        }

        private void lnkBinding_ScanOut_1Month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_binding_scanIn_range = "3";
            //dgvBinding_ScanIn.DataSource = "";
            //dgvBinding_ScanIn.Refresh();
            //initBinding_ScanIn();
        }

        private void lnkBinding_ScanOut_3Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_binding_scanIn_range = "4";
            //dgvBinding_ScanIn.DataSource = "";
            //dgvBinding_ScanIn.Refresh();
            //initBinding_ScanIn();
        }

        private void lnkBinding_ScanOut_6Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_binding_scanIn_range = "5";
            //dgvBinding_ScanIn.DataSource = "";
            //dgvBinding_ScanIn.Refresh();
            //initBinding_ScanIn();
        }

        private void lnkBinding_ScanOut_1Year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //_binding_scanIn_range = "6";
            //dgvBinding_ScanIn.DataSource = "";
            //dgvBinding_ScanIn.Refresh();
            //initBinding_ScanIn();
        }


        #endregion



        #region SCAN-IN MATERIAL


        public void initScanIN()
        {
            initScanIn_List();
            txtScanIn_Packing.Visible = false;
            lblScanIn_Name.Text = "";
            lblScanIn_Code.Text = "";
            lblScanIn_HoDate.Text = "";
            lblScanIn_Warehouse.Text = "";
            lblScanIn_Locate.Text = "";
            lblScanIn_Sum.Text = "0";
            lblScanIn_Total.Text = "0";
            lblScanIn_Unit.Text = "";

            txtScanIn_PCS.Enabled = false;
            txtScanIn_BOX.Enabled = false;
            txtScanIn_PAK.Enabled = false;
            txtScanIn_LEN.Enabled = false;

            txtScanIn_PCS.Text = "0";
            txtScanIn_BOX.Text = "0";
            txtScanIn_PAK.Text = "0";
            txtScanIn_LEN.Text = "0";

            lblScanIn_PCS.BackColor = Color.Silver;
            lblScanIn_BOX.BackColor = Color.Silver;
            lblScanIn_PAK.BackColor = Color.Silver;
            lblScanIn_LEN.BackColor = Color.Silver;

            btnScanIn_Save.Enabled = false;
            btnScanIn_Done.Enabled = false;
        }

        public void initScanIn_List()
        {
            string sql = "V2o1_BASE_SubMaterial02_Init_ScanIn_List_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvScanIn_List_Width = cls.fnGetDataGridWidth(dgvScanIn_List);
            dgvScanIn_List.DataSource = dt;

            //dgvScanIn_List.Columns[0].Width = 10 * _dgvScanIn_List_Width / 100;    // idx
            //dgvScanIn_List.Columns[1].Width = 10 * _dgvScanIn_List_Width / 100;    // hoIDx
            //dgvScanIn_List.Columns[2].Width = 10 * _dgvScanIn_List_Width / 100;    // matIDx
            dgvScanIn_List.Columns[3].Width = 65 * _dgvScanIn_List_Width / 100;    // matName
            //dgvScanIn_List.Columns[4].Width = 10 * _dgvScanIn_List_Width / 100;    // matCode
            dgvScanIn_List.Columns[5].Width = 20 * _dgvScanIn_List_Width / 100;    // matQty
            dgvScanIn_List.Columns[6].Width = 15 * _dgvScanIn_List_Width / 100;    // matUnit
            //dgvScanIn_List.Columns[7].Width = 15 * _dgvScanIn_List_Width / 100;    // IN_Stock
            //dgvScanIn_List.Columns[8].Width = 15 * _dgvScanIn_List_Width / 100;    // IN_Date
            //dgvScanIn_List.Columns[9].Width = 15 * _dgvScanIn_List_Width / 100;    // Sublocation
            //dgvScanIn_List.Columns[10].Width = 15 * _dgvScanIn_List_Width / 100;    // PCS
            //dgvScanIn_List.Columns[11].Width = 15 * _dgvScanIn_List_Width / 100;    // BOX
            //dgvScanIn_List.Columns[12].Width = 15 * _dgvScanIn_List_Width / 100;    // PAK
            //dgvScanIn_List.Columns[13].Width = 15 * _dgvScanIn_List_Width / 100;    // LEN
            //dgvScanIn_List.Columns[14].Width = 15 * _dgvScanIn_List_Width / 100;    // hoDate
            //dgvScanIn_List.Columns[14].Width = 16 * _dgvScanIn_List_Width / 100;    // ImageStatus
            //dgvScanIn_List.Columns[14].Width = 17 * _dgvScanIn_List_Width / 100;    // ViewImage

            dgvScanIn_List.Columns[0].Visible = false;
            dgvScanIn_List.Columns[1].Visible = false;
            dgvScanIn_List.Columns[2].Visible = false;
            dgvScanIn_List.Columns[3].Visible = true;
            dgvScanIn_List.Columns[4].Visible = false;
            dgvScanIn_List.Columns[5].Visible = true;
            dgvScanIn_List.Columns[6].Visible = true;
            dgvScanIn_List.Columns[7].Visible = false;
            dgvScanIn_List.Columns[8].Visible = false;
            dgvScanIn_List.Columns[9].Visible = false;
            dgvScanIn_List.Columns[10].Visible = false;
            dgvScanIn_List.Columns[11].Visible = false;
            dgvScanIn_List.Columns[12].Visible = false;
            dgvScanIn_List.Columns[13].Visible = false;
            dgvScanIn_List.Columns[14].Visible = false;
            dgvScanIn_List.Columns[15].Visible = false;
            dgvScanIn_List.Columns[16].Visible = false;

            cls.fnFormatDatagridview(dgvScanIn_List, 11, 30);

            dgvScanIn_List.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvScanIn_List.BackgroundColor = Color.White;

        }

        private void dgvScanIn_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvScanIn_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvScanIn_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvScanIn_List.Rows[e.RowIndex];

                string scanIDx = row.Cells[0].Value.ToString();
                string hoIDx = row.Cells[1].Value.ToString();
                string matIDx = row.Cells[2].Value.ToString();
                string matName = row.Cells[3].Value.ToString();
                string matCode = row.Cells[4].Value.ToString();
                string matQty = row.Cells[5].Value.ToString();
                string matUnit = row.Cells[6].Value.ToString();
                string IN_Stock = row.Cells[7].Value.ToString();
                string IN_Date = row.Cells[8].Value.ToString();
                string locate = row.Cells[9].Value.ToString();
                string matPCS = row.Cells[10].Value.ToString();
                string matBOX = row.Cells[11].Value.ToString();
                string matPAK = row.Cells[12].Value.ToString();
                string matLEN = row.Cells[13].Value.ToString();
                string hoDate = row.Cells[14].Value.ToString();
                string imagestatus = row.Cells[15].Value.ToString().ToLower();

                decimal _matPCS = (matPCS != "" && matPCS != null) ? Convert.ToDecimal(matPCS) : 0;
                decimal _matBOX = (matBOX != "" && matBOX != null) ? Convert.ToDecimal(matBOX) : 0;
                decimal _matPAK = (matPAK != "" && matPAK != null) ? Convert.ToDecimal(matPAK) : 0;
                decimal _matLEN = (matLEN != "" && matLEN != null) ? Convert.ToDecimal(matLEN) : 0;
                DateTime _hoDate = (hoDate != "" && hoDate != null) ? Convert.ToDateTime(hoDate) : _dt;

                _scanIDx = scanIDx;
                _scanHoIDx = hoIDx;
                _scanMatIDx = matIDx;
                _scanMatName = matName;
                _scanMatCode = matCode;
                _scanMatQty = matQty;
                _scanMatUnit = matUnit;
                _scanInStock = IN_Stock;
                _scanInDate = IN_Date;
                _scanLocate = locate;
                _scanPCS = matPCS;
                _scanBOX = matBOX;
                _scanPAK = matPAK;
                _scanLEN = matLEN;
                _scanHoDate = hoDate;

                lblScanIn_Name.Text = matName;
                lblScanIn_Code.Text = matCode;
                lblScanIn_HoDate.Text = String.Format("{0:dd/MM/yyyy HH:mm}", _hoDate);
                lblScanIn_Warehouse.Text = "MMT WH5";
                lblScanIn_Locate.Text = locate;
                lblScanIn_Sum.Text = matQty;
                lblScanIn_Unit.Text = matUnit;

                txtScanIn_PCS.Text = matPCS;
                txtScanIn_BOX.Text = matBOX;
                txtScanIn_PAK.Text = matPAK;
                txtScanIn_LEN.Text = matLEN;

                txtScanIn_PCS.Enabled = (matPCS != "" && matPCS != null && matPCS != "0") ? true : false;
                txtScanIn_BOX.Enabled = (matBOX != "" && matBOX != null && matBOX != "0") ? true : false;
                txtScanIn_PAK.Enabled = (matPAK != "" && matPAK != null && matPAK != "0") ? true : false;
                txtScanIn_LEN.Enabled = (matLEN != "" && matLEN != null && matLEN != "0") ? true : false;

                lblScanIn_PCS.BackColor = (matPCS != "" && matPCS != null && matPCS != "0") ? Color.DeepSkyBlue : Color.Silver;
                lblScanIn_BOX.BackColor = (matBOX != "" && matBOX != null && matBOX != "0") ? Color.DeepSkyBlue : Color.Silver;
                lblScanIn_PAK.BackColor = (matPAK != "" && matPAK != null && matPAK != "0") ? Color.DeepSkyBlue : Color.Silver;
                lblScanIn_LEN.BackColor = (matLEN != "" && matLEN != null && matLEN != "0") ? Color.DeepSkyBlue : Color.Silver;

                //if (_matPCS == 0 && _matBOX == 0 && _matPAK == 0 && _matLEN == 0)
                //{
                //    txtScanIn_Packing.Text = "";
                //    txtScanIn_Packing.Visible = false;
                //}
                //else
                //{
                //    txtScanIn_Packing.Text = "";
                //    txtScanIn_Packing.Visible = true;
                //    txtScanIn_Packing.Focus();
                //}
                txtScanIn_Packing.Text = "";
                txtScanIn_Packing.Visible = true;
                txtScanIn_Packing.Focus();

                if (imagestatus == "true")
                {
                    _ms = new MemoryStream((byte[])row.Cells[16].Value);
                    lnkScanIn_ViewImage.Enabled = true;
                }
                else
                {
                    _ms = null;
                    lnkScanIn_ViewImage.Enabled = false;
                }

                btnScanIn_Save.Enabled = (txtScanIn_Packing.Text.Trim().Length > 0) ? true : false;
                btnScanIn_Done.Enabled = true;
            }
        }

        private void dgvScanIn_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtScanIn_Packing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fnScanIn_Data();

                fnScanIn_Done();
            }
        }

        private void txtScanIn_Packing_TextChanged(object sender, EventArgs e)
        {
            string packing = txtScanIn_Packing.Text.Trim();
            btnScanIn_Save.Enabled = (packing.Length > 0) ? true : false;
        }

        public void fnScanIn_Data()
        {
            try
            {
                string scanIDx = _scanIDx;
                string hoIDx = _scanHoIDx;
                string matIDx = _scanMatIDx;
                string matName = _scanMatName;
                string matCode = _scanMatCode;
                string matQty = _scanMatQty;
                string matUnit = _scanMatUnit;
                string IN_Stock = _scanInStock;
                string IN_Date = _scanInDate;
                string locate = _scanLocate;
                string matPCS = _scanPCS;
                string matBOX = _scanBOX;
                string matPAK = _scanPAK;
                string matLEN = _scanLEN;

                string packing = txtScanIn_Packing.Text.Trim();
                string packType = (packing != "" && packing != null) ? packing.Substring(0, 3) : "";
                string packKind = (packing != "" && packing != null) ? packing.Substring(4, 3) : "";
                string packCode = (packing != "" && packing != null) ? packing.Substring(8) : "";

                if (packing != "" && packing != null)
                {
                    if (packType.ToUpper() == "MMT")
                    {
                        if (packKind.ToUpper() == "SBM")
                        {
                            if (locate.Length > 0)
                            {
                                if (packCode.Length >= 5 && packCode.Length <= 15)
                                {
                                    string sqlChk = "V2o1_BASE_SubMaterial02_Init_ScanIn_Item_ChkItem_Addnew";
                                    SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                                    sParamsChk[0] = new SqlParameter();
                                    sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                                    sParamsChk[0].ParameterName = "@scanPacking";
                                    sParamsChk[0].Value = packing;

                                    DataSet ds = new DataSet();
                                    ds = cls.ExecuteDataSet(sqlChk, sParamsChk);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        //string msg = "";
                                        //msg += "scanIDx: " + scanIDx + "\r\n";
                                        //msg += "hoIDx: " + hoIDx + "\r\n";
                                        //msg += "matIDx: " + matIDx + "\r\n";
                                        //msg += "packing: " + packing + "\r\n";
                                        //MessageBox.Show(msg);

                                        string sql = "V2o1_BASE_SubMaterial02_Init_ScanIn_Item_AddItem_Addnew";
                                        SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                                        sParams[0] = new SqlParameter();
                                        sParams[0].SqlDbType = SqlDbType.Int;
                                        sParams[0].ParameterName = "@scanIDx";
                                        sParams[0].Value = scanIDx;

                                        sParams[1] = new SqlParameter();
                                        sParams[1].SqlDbType = SqlDbType.Int;
                                        sParams[1].ParameterName = "@scanHoIDx";
                                        sParams[1].Value = hoIDx;

                                        sParams[2] = new SqlParameter();
                                        sParams[2].SqlDbType = SqlDbType.Int;
                                        sParams[2].ParameterName = "@scanMatIDx";
                                        sParams[2].Value = matIDx;

                                        sParams[3] = new SqlParameter();
                                        sParams[3].SqlDbType = SqlDbType.VarChar;
                                        sParams[3].ParameterName = "@scanPacking";
                                        sParams[3].Value = packing;

                                        cls.fnUpdDel(sql, sParams);

                                        int tab2 = tabControl2.SelectedIndex;
                                        switch (tab2)
                                        {
                                            case 0:
                                                initBinding_ScanIn();
                                                break;
                                            case 1:
                                                initBinding_Inventory();
                                                break;
                                            case 2:
                                                tabControl2.SelectedIndex = 1;
                                                initBinding_ScanIn();
                                                break;
                                        }
                                        initScanIn_List();
                                        fnScanIn_Done();

                                        _msgText = "Nhập kho thành công.";
                                        _msgType = 1;
                                    }
                                    else
                                    {
                                        string chkCode = ds.Tables[0].Rows[0][1].ToString();
                                        string chkDate = ds.Tables[0].Rows[0][2].ToString();
                                        _msgText = "Mã tem này đã từng scan trên hệ thống cho mã '" + chkCode + "' ngày " + chkDate;
                                        _msgType = 2;
                                    }
                                }
                                else
                                {
                                    _msgText = "Mã tem không hợp lệ, phải lớn hơn bằng 5 và nhỏ hơn bằng 15 ký tự.";
                                    _msgType = 2;
                                }
                            }
                            else
                            {
                                _msgText = "Chưa có vị trí vật tư. Vui lòng kiểm tra lại.";
                                _msgType = 2;
                            }
                        }
                        else
                        {
                            _msgText = "Kiểu tem không đúng, bắt buộc phải có 'SBM'. Vui lòng kiểm tra lại.";
                            _msgType = 2;
                        }
                    }
                    else
                    {
                        _msgText = "Loại tem không đúng, phải bắt đầu bằng 'MMT'. Vui lòng kiểm tra lại.";
                        _msgType = 2;
                    }
                }
                else
                {
                    _msgText = "Không nhận được mã tem. Vui lòng kiểm tra lại.";
                    _msgType = 2;
                }

                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        private void btnScanIn_Done_Click(object sender, EventArgs e)
        {
            fnScanIn_Done();
        }

        public void fnScanIn_Done()
        {
            dgvScanIn_List.ClearSelection();
            txtScanIn_Packing.Text = "";
            txtScanIn_Packing.Visible = false;
            lblScanIn_Name.Text = "";
            lblScanIn_Code.Text = "";
            lblScanIn_HoDate.Text = "";
            lblScanIn_Warehouse.Text = "";
            lblScanIn_Locate.Text = "";
            lblScanIn_Sum.Text = "0";
            lblScanIn_Total.Text = "0";
            lblScanIn_Unit.Text = "";
            btnScanIn_Save.Enabled = false;
            btnScanIn_Done.Enabled = false;
        }

        private void btnScanIn_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                fnScanIn_Data();

                fnScanIn_Done();
            }
        }

        private void lnkScanIn_ViewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSub02ScanInMaterial_ViewImage frm02 = new frmSub02ScanInMaterial_ViewImage(_ms);
            frm02.ShowDialog();
        }



        #endregion



        #region HAND-OVER MATERIAL


        public void initHandOver()
        {
            initHandOver_List();
            lblHandOver_Code.Text = "";
            lblHandOver_Date.Text = "";
            lblHandOver_Total.Text = "0";
            dgvHandOver_Detail.DataSource = "";
            dgvHandOver_Detail.Refresh();
            lblHandOver_Note.Text = "";
            chkHandOver_Done.Checked = false;
            chkHandOver_Done.Enabled = false;
            btnHandOver_Save.Enabled = false;
            btnHandOver_Done.Enabled = false;
        }

        public void initHandOver_List()
        {
            string sql = "V2o1_BASE_SubMaterial02_Init_HandOver_List_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvHandOver_List_Width = cls.fnGetDataGridWidth(dgvHandOver_List);
            dgvHandOver_List.DataSource = dt;

            //dgvHandOver_List.Columns[0].Width = 10 * _dgvHandOver_List_Width / 100;    // IDx
            dgvHandOver_List.Columns[1].Width = 45 * _dgvHandOver_List_Width / 100;    // hoCode
            dgvHandOver_List.Columns[2].Width = 40 * _dgvHandOver_List_Width / 100;    // hoDate
            dgvHandOver_List.Columns[3].Width = 15 * _dgvHandOver_List_Width / 100;    // hoSum
            //dgvHandOver_List.Columns[4].Width = 10 * _dgvHandOver_List_Width / 100;    // hoNew
            //dgvHandOver_List.Columns[5].Width = 10 * _dgvHandOver_List_Width / 100;    // hoDone
            //dgvHandOver_List.Columns[6].Width = 10 * _dgvHandOver_List_Width / 100;    // hoNote

            dgvHandOver_List.Columns[0].Visible = false;
            dgvHandOver_List.Columns[1].Visible = true;
            dgvHandOver_List.Columns[2].Visible = true;
            dgvHandOver_List.Columns[3].Visible = true;
            dgvHandOver_List.Columns[4].Visible = false;
            dgvHandOver_List.Columns[5].Visible = false;
            dgvHandOver_List.Columns[6].Visible = false;

            dgvHandOver_List.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridviewWhite(dgvHandOver_List, 11, 30);

            dgvHandOver_List.BackgroundColor = Color.White;


            string hoNew = "", hoDone = "";
            bool _hoNew = true, _hoDone = true;
            foreach (DataGridViewRow row in dgvHandOver_List.Rows)
            {
                hoNew = row.Cells[4].Value.ToString();
                hoDone = row.Cells[5].Value.ToString();

                _hoNew = (hoNew.ToLower() == "true") ? true : false;
                _hoDone = (hoDone.ToLower() == "true") ? true : false;

                row.DefaultCellStyle.BackColor = (_hoNew == true) ? Color.LightYellow : (_hoDone == true) ? Color.LightGreen : Color.White;
            }
        }

        private void dgvHandOver_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvHandOver_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                _hoIDx = "";
                cls.fnDatagridClickCell(dgvHandOver_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvHandOver_List.Rows[e.RowIndex];

                string hoIDx = row.Cells[0].Value.ToString();
                string hoCode = row.Cells[1].Value.ToString();
                string hoDate = row.Cells[2].Value.ToString();
                string hoSum = row.Cells[3].Value.ToString();
                string hoNew = row.Cells[4].Value.ToString();
                string hoDone = row.Cells[5].Value.ToString();
                string hoNote = row.Cells[6].Value.ToString();

                lblHandOver_Code.Text = hoCode;
                lblHandOver_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(hoDate));
                lblHandOver_Total.Text = String.Format("{0:N0}", (hoSum != "" && hoSum != null) ? Convert.ToInt32(hoSum) : 0);
                lblHandOver_Note.Text = hoNote;

                _hoIDx = hoIDx;

                string sql = "V2o1_BASE_SubMaterial02_Init_HandOver_Detail_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@hoIDx";
                sParams[0].Value = hoIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvHandOver_Detail_Width = cls.fnGetDataGridWidth(dgvHandOver_Detail);
                dgvHandOver_Detail.DataSource = dt;

                //dgvHandOver_Detail.Columns[0].Width = 10 * _dgvHandOver_Detail_Width / 100;    // idx
                //dgvHandOver_Detail.Columns[1].Width = 10 * _dgvHandOver_Detail_Width / 100;    // hoIDx
                //dgvHandOver_Detail.Columns[2].Width = 10 * _dgvHandOver_Detail_Width / 100;    // matIDx
                dgvHandOver_Detail.Columns[3].Width = 70 * _dgvHandOver_Detail_Width / 100;    // matName
                                                                                               //dgvHandOver_Detail.Columns[4].Width = 10 * _dgvHandOver_Detail_Width / 100;    // matCode
                dgvHandOver_Detail.Columns[5].Width = 15 * _dgvHandOver_Detail_Width / 100;    // matQty
                dgvHandOver_Detail.Columns[6].Width = 15 * _dgvHandOver_Detail_Width / 100;    // matUnit

                dgvHandOver_Detail.Columns[0].Visible = false;
                dgvHandOver_Detail.Columns[1].Visible = false;
                dgvHandOver_Detail.Columns[2].Visible = false;
                dgvHandOver_Detail.Columns[3].Visible = true;
                dgvHandOver_Detail.Columns[4].Visible = false;
                dgvHandOver_Detail.Columns[5].Visible = true;
                dgvHandOver_Detail.Columns[6].Visible = true;

                cls.fnFormatDatagridview(dgvHandOver_Detail, 11, 30);

                dgvHandOver_Detail.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvHandOver_List.Refresh();

                chkHandOver_Done.Enabled = (hoDone.ToLower() == "true") ? false : true;
                chkHandOver_Done.Checked = (hoDone.ToLower() == "true") ? true : false;

                btnHandOver_Save.Enabled = (hoDone.ToLower() == "true") ? false : true;
                btnHandOver_Done.Enabled = true;
            }
        }

        private void dgvHandOver_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnHandOver_Done_Click(object sender, EventArgs e)
        {
            fnDone();
        }

        public void fnDone()
        {
            _hoIDx = "";
            _msgText = "";
            _msgType = 0;

            dgvHandOver_List.ClearSelection();
            lblHandOver_Code.Text = "";
            lblHandOver_Date.Text = "";
            lblHandOver_Total.Text = "0";
            dgvHandOver_Detail.DataSource = "";
            dgvHandOver_Detail.Refresh();
            lblHandOver_Note.Text = "";
            chkHandOver_Done.Checked = false;
            chkHandOver_Done.Enabled = false;

            btnHandOver_Save.Enabled = false;
            btnHandOver_Done.Enabled = false;
        }

        private void btnHandOver_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string hoIDx = _hoIDx;
                string sql = "V2o1_BASE_SubMaterial02_Init_HandOver_Done_UpdItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@hoIDx";
                sParams[0].Value = hoIDx;

                cls.fnUpdDel(sql, sParams);

                _msgText = "Cập nhật thành công.";
                _msgType = 1;
                cls.fnMessage(tssMessage, _msgText, _msgType);

                initHandOver_List();
                fnDone();
            }
        }


        #endregion



        #region SET LOCATION


        public void initLocate()
        {
            initLocate_List();
        }

        public void initLocate_List()
        {
            string sql = "V2o1_BASE_SubMaterial02_Init_Location_List_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvLocate_List_Width = cls.fnGetDataGridWidth(dgvLocate_List);
            dgvLocate_List.DataSource = dt;

            //dgvLocate_List.Columns[0].Width = 10 * _dgvLocate_List_Width / 100;    // ProdId
            dgvLocate_List.Columns[1].Width = 75 * _dgvLocate_List_Width / 100;    // Name
            //dgvLocate_List.Columns[2].Width = 10 * _dgvLocate_List_Width / 100;    // Barcode
            dgvLocate_List.Columns[3].Width = 25 * _dgvLocate_List_Width / 100;    // Sublocation

            dgvLocate_List.Columns[0].Visible = false;
            dgvLocate_List.Columns[1].Visible = true;
            dgvLocate_List.Columns[2].Visible = false;
            dgvLocate_List.Columns[3].Visible = true;

            dgvLocate_List.Columns[0].ReadOnly = true;
            dgvLocate_List.Columns[1].ReadOnly = true;
            dgvLocate_List.Columns[2].ReadOnly = true;
            dgvLocate_List.Columns[3].ReadOnly = false;

            cls.fnFormatDatagridview(dgvLocate_List, 11, 30);

            dgvLocate_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvLocate_List.BackgroundColor = Color.White;
        }

        private void dgvLocate_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string loc = "";
            foreach (DataGridViewRow row in dgvLocate_List.Rows)
            {
                loc = row.Cells[3].Value.ToString();
                if (loc != "")
                {
                    row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    row.ReadOnly = true;
                }
            }
        }

        private void dgvLocate_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvLocate_List, e);
            }
        }

        private void dgvLocate_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvLocate_List_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (dgvLocate_List.CurrentCell.ColumnIndex == 0 || dgvLocate_List.CurrentCell.ColumnIndex == 2)
            if (dgvLocate_List.CurrentCell.ColumnIndex == 3)
            {
                if (e.Control is TextBox)
                {
                    ((TextBox)(e.Control)).CharacterCasing = CharacterCasing.Upper;
                }
            }
        }

        private void btnLocate_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string matIDx = "", matName = "", matCode = "", matLoc = "";
                foreach (DataGridViewRow row in dgvLocate_List.Rows)
                {
                    matIDx = row.Cells[0].Value.ToString();
                    matName = row.Cells[1].Value.ToString();
                    matCode = row.Cells[2].Value.ToString();
                    matLoc = row.Cells[3].Value.ToString();

                    string sql = "V2o1_BASE_SubMaterial02_Init_Location_List_UpdItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@matIDx";
                    sParams[0].Value = matIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.VarChar;
                    sParams[1].ParameterName = "@matLoc";
                    sParams[1].Value = matLoc;

                    cls.fnUpdDel(sql, sParams);
                }

                initLocate_List();

                _msgText = "Cập nhật vị trí thành công.";
                _msgType = 1;
                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }

        private void btnLocate_Done_Click(object sender, EventArgs e)
        {

        }


        #endregion



        //#region SET PACKING STANDARD


        //public void initPacking()
        //{
        //    initPacking_List();
        //}

        //public void initPacking_List()
        //{
        //    string sql = "V2o1_BASE_SubMaterial02_Init_Packing_List_SelItem_Addnew";
        //    DataTable dt = new DataTable();
        //    dt = cls.fnSelect(sql);

        //    _dgvPacking_List_Width = cls.fnGetDataGridWidth(dgvPacking_List);
        //    dgvPacking_List.DataSource = dt;

        //    //dgvPacking_List.Columns[0].Width = 10 * _dgvPacking_List_Width / 100;    // ProdId
        //    dgvPacking_List.Columns[1].Width = 52 * _dgvPacking_List_Width / 100;    // Name
        //    //dgvPacking_List.Columns[2].Width = 10 * _dgvPacking_List_Width / 100;    // Barcode
        //    dgvPacking_List.Columns[3].Width = 12 * _dgvPacking_List_Width / 100;    // PCS
        //    dgvPacking_List.Columns[4].Width = 12 * _dgvPacking_List_Width / 100;    // BOX
        //    dgvPacking_List.Columns[5].Width = 12 * _dgvPacking_List_Width / 100;    // PAK
        //    dgvPacking_List.Columns[6].Width = 12 * _dgvPacking_List_Width / 100;    // LEN

        //    dgvPacking_List.Columns[0].Visible = false;
        //    dgvPacking_List.Columns[1].Visible = true;
        //    dgvPacking_List.Columns[2].Visible = false;
        //    dgvPacking_List.Columns[3].Visible = true;
        //    dgvPacking_List.Columns[4].Visible = true;
        //    dgvPacking_List.Columns[5].Visible = true;
        //    dgvPacking_List.Columns[6].Visible = true;

        //    dgvPacking_List.Columns[0].ReadOnly = true;
        //    dgvPacking_List.Columns[1].ReadOnly = true;
        //    dgvPacking_List.Columns[2].ReadOnly = true;
        //    dgvPacking_List.Columns[3].ReadOnly = false;
        //    dgvPacking_List.Columns[4].ReadOnly = false;
        //    dgvPacking_List.Columns[5].ReadOnly = false;
        //    dgvPacking_List.Columns[6].ReadOnly = false;

        //    cls.fnFormatDatagridviewWhite(dgvPacking_List, 11, 30);
        //    dgvPacking_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    dgvPacking_List.BackgroundColor = Color.White;


        //    string matPCS = "", matBOX = "", matPAK = "", matLEN = "";
        //    decimal _matPCS = 0, _matBOX = 0, _matPAK = 0, _matLEN = 0;
        //    foreach (DataGridViewRow row in dgvPacking_List.Rows)
        //    {
        //        matPCS = row.Cells[3].Value.ToString();
        //        matBOX = row.Cells[4].Value.ToString();
        //        matPAK = row.Cells[5].Value.ToString();
        //        matLEN = row.Cells[6].Value.ToString();

        //        _matPCS = (matPCS != "" && matPCS != null) ? Convert.ToDecimal(matPCS) : 0;
        //        _matBOX = (matBOX != "" && matBOX != null) ? Convert.ToDecimal(matBOX) : 0;
        //        _matPAK = (matPAK != "" && matPAK != null) ? Convert.ToDecimal(matPAK) : 0;
        //        _matLEN = (matLEN != "" && matLEN != null) ? Convert.ToDecimal(matLEN) : 0;

        //        if (_matPCS == 0 && _matBOX == 0 && _matPAK == 0 && _matLEN == 0)
        //        {
        //            row.DefaultCellStyle.BackColor = Color.Khaki;
        //        }
        //        else
        //        {
        //            row.DefaultCellStyle.BackColor = Color.White;
        //        }
        //    }
        //}

        //private void dgvPacking_List_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    cls.fnDatagridClickCell(dgvPacking_List, e);
        //}

        //private void dgvPacking_List_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
        //    if (dgvPacking_List.CurrentCell.ColumnIndex == 3
        //        || dgvPacking_List.CurrentCell.ColumnIndex == 4
        //        || dgvPacking_List.CurrentCell.ColumnIndex == 5
        //        || dgvPacking_List.CurrentCell.ColumnIndex == 6) //Desired Column
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
        //        }
        //    }
        //}

        //private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void btnPacking_Done_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnPacking_Save_Click(object sender, EventArgs e)
        //{
        //    DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
        //    if (dialog == DialogResult.Yes)
        //    {
        //        string matIDx = "", matName = "", matCode = "", matPCS = "", matBOX = "", matPAK = "", matLEN = "";
        //        foreach (DataGridViewRow row in dgvPacking_List.Rows)
        //        {
        //            matIDx = row.Cells[0].Value.ToString();
        //            matName = row.Cells[1].Value.ToString();
        //            matCode = row.Cells[2].Value.ToString();
        //            matPCS = row.Cells[3].Value.ToString();
        //            matBOX = row.Cells[4].Value.ToString();
        //            matPAK = row.Cells[5].Value.ToString();
        //            matLEN = row.Cells[6].Value.ToString();

        //            string sql = "V2o1_BASE_SubMaterial02_Init_Packing_List_UpdItem_Addnew";
        //            SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

        //            sParams[0] = new SqlParameter();
        //            sParams[0].SqlDbType = SqlDbType.Int;
        //            sParams[0].ParameterName = "@matIDx";
        //            sParams[0].Value = matIDx;

        //            sParams[1] = new SqlParameter();
        //            sParams[1].SqlDbType = SqlDbType.SmallMoney;
        //            sParams[1].ParameterName = "@matPCS";
        //            sParams[1].Value = matPCS;

        //            sParams[2] = new SqlParameter();
        //            sParams[2].SqlDbType = SqlDbType.SmallMoney;
        //            sParams[2].ParameterName = "@matBOX";
        //            sParams[2].Value = matBOX;

        //            sParams[3] = new SqlParameter();
        //            sParams[3].SqlDbType = SqlDbType.SmallMoney;
        //            sParams[3].ParameterName = "@matPAK";
        //            sParams[3].Value = matPAK;

        //            sParams[4] = new SqlParameter();
        //            sParams[4].SqlDbType = SqlDbType.SmallMoney;
        //            sParams[4].ParameterName = "@matLEN";
        //            sParams[4].Value = matLEN;

        //            cls.fnUpdDel(sql, sParams);
        //        }

        //        initPacking_List();

        //        _msgText = "Cập nhật số lượng cho packing thành công.";
        //        _msgType = 1;
        //        cls.fnMessage(tssMessage, _msgText, _msgType);
        //    }
        //}


        //#endregion

        #region RE-INSTOCK MATERIAL

        public void initReIn()
        {
            initReIn_List();
            initReIn_Done();
        }

        public void initReIn_List()
        {
            string sql = "";
        }

        public void initReIn_Done()
        {
            dgvReIn_List.ClearSelection();
            txtReIn_Filter.Text = "";
            lblReIn_Code.Text = "N/A";
            lblReIn_Code.Enabled = false;
            lblReIn_Code.BackColor = Color.WhiteSmoke;
            lblReIn_Name.Text = "N/A";
            lblReIn_Name.Enabled = false;
            lblReIn_Name.BackColor = Color.WhiteSmoke;
            tlpReIn_Packing.Enabled = false;
            tlpReIn_Packing.BackColor = Color.WhiteSmoke;
            txtReIn_Packing.Text = "";
            txtReIn_Packing.Enabled = false;
            txtReIn_Packing.BackColor = Color.WhiteSmoke;
            txtReIn_Qty.Text = "0";
            txtReIn_Qty.Enabled = false;
            txtReIn_Qty.BackColor = Color.WhiteSmoke;
            txtReIn_Remark.Text = "";
            txtReIn_Remark.Enabled = false;
            txtReIn_Remark.BackColor = Color.WhiteSmoke;

            btnReIn_Save.Enabled = false;
            btnReIn_Done.Enabled = false;
        }

        #endregion


        #region TRANSFER LOCATE


        public void initTransfer()
        {
            initTransfer_List();

            //txtTransfer_Filter.Text = "";
            //txtTransfer_Filter.Enabled = true;
            //lblTransfer_PCode.Text = "N/A";
            //lblTransfer_PCode.Enabled = false;
            //lblTransfer_PName.Text = "N/A";
            //lblTransfer_PName.Enabled = false;
            //lblTransfer_CurPos.Text = "N/A";
            //lblTransfer_CurPos.Enabled = false;
            //txtTransfer_NewPos.Text = "";
            //txtTransfer_NewPos.Enabled = false;

            //btnTransfer_Save.Enabled = false;
            //btnTransfer_Done.Enabled = false;

            fnTransfer_Done();
        }

        public void initTransfer_List()
        {
            string sql = "V2o1_BASE_SubMaterial02_Init_Transfer_List_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvTransfer_List_Width = cls.fnGetDataGridWidth(dgvTransfer_List);
            dgvTransfer_List.DataSource = dt;

            //dgvTransfer_List.Columns[0].Width = 10 * _dgvTransfer_List_Width / 100;    // ProdId
            dgvTransfer_List.Columns[1].Width = 75 * _dgvTransfer_List_Width / 100;    // Name
            //dgvTransfer_List.Columns[2].Width = 10 * _dgvTransfer_List_Width / 100;    // Barcode
            dgvTransfer_List.Columns[3].Width = 25 * _dgvTransfer_List_Width / 100;    // Sublocation

            dgvTransfer_List.Columns[0].Visible = false;
            dgvTransfer_List.Columns[1].Visible = true;
            dgvTransfer_List.Columns[2].Visible = false;
            dgvTransfer_List.Columns[3].Visible = true;

            dgvTransfer_List.Columns[0].ReadOnly = true;
            dgvTransfer_List.Columns[1].ReadOnly = true;
            dgvTransfer_List.Columns[2].ReadOnly = true;
            dgvTransfer_List.Columns[3].ReadOnly = false;

            cls.fnFormatDatagridview(dgvTransfer_List, 11, 30);

            dgvTransfer_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvTransfer_List.BackgroundColor = Color.White;
        }

        private void dgvTransfer_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvTransfer_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvTransfer_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvTransfer_List.Rows[e.RowIndex];

                string partIDx = row.Cells[0].Value.ToString();
                string partName = row.Cells[1].Value.ToString();
                string partCode = row.Cells[2].Value.ToString();
                string curLocate = row.Cells[3].Value.ToString();

                _transferPartIDx = partIDx;

                lblTransfer_PCode.Text = partCode;
                lblTransfer_PCode.Enabled = true;
                lblTransfer_PName.Text = partName;
                lblTransfer_PName.Enabled = true;
                lblTransfer_CurPos.Text = curLocate;
                lblTransfer_CurPos.Enabled = true;
                txtTransfer_NewPos.Text = curLocate;
                txtTransfer_NewPos.Enabled = true;
                txtTransfer_NewPos.Focus();
                txtTransfer_NewPos.BackColor = Color.LightGreen;

                btnTransfer_Save.Enabled = false;
                btnTransfer_Done.Enabled = true;
            }
        }

        private void dgvTransfer_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtTransfer_NewPos_TextChanged(object sender, EventArgs e)
        {
            string newPos = txtTransfer_NewPos.Text.Trim();
            if (newPos.Length > 0)
            {
                btnTransfer_Save.Enabled = true;
            }
            else
            {
                btnTransfer_Save.Enabled = false;
            }
        }

        private void btnTransfer_Done_Click(object sender, EventArgs e)
        {
            fnTransfer_Done();
        }

        private void btnTransfer_Save_Click(object sender, EventArgs e)
        {
            fnTransfer_Save();
        }

        public void fnTransfer_Done()
        {
            _transferPartIDx = "";
            dgvTransfer_List.ClearSelection();

            txtTransfer_Filter.Text = "";
            txtTransfer_Filter.Enabled = true;
            lblTransfer_PCode.Text = "N/A";
            lblTransfer_PCode.Enabled = false;
            lblTransfer_PName.Text = "N/A";
            lblTransfer_PName.Enabled = false;
            lblTransfer_CurPos.Text = "N/A";
            lblTransfer_CurPos.Enabled = false;
            txtTransfer_NewPos.Text = "";
            txtTransfer_NewPos.Enabled = false;
            txtTransfer_NewPos.BackColor = Color.Silver;

            btnTransfer_Save.Enabled = false;
            btnTransfer_Done.Enabled = false;
        }

        public void fnTransfer_Save()
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string partIDx = _transferPartIDx;
                string newPos = txtTransfer_NewPos.Text.Trim();
                if (partIDx != "" && newPos != "")
                {
                    string sqlChk = "V2o1_BASE_SubMaterial02_Init_Transfer_Possition_ChkItem_Addnew";

                    SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                    sParamsChk[0] = new SqlParameter();
                    sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                    sParamsChk[0].ParameterName = "@newPos";
                    sParamsChk[0].Value = newPos;

                    DataSet ds = new DataSet();
                    ds = cls.ExecuteDataSet(sqlChk, sParamsChk);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        string sql = "V2o1_BASE_SubMaterial02_Init_Transfer_Possition_UpdItem_Addnew";

                        SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@matIDx";
                        sParams[0].Value = partIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.VarChar;
                        sParams[1].ParameterName = "@newPos";
                        sParams[1].Value = newPos;

                        cls.fnUpdDel(sql, sParams);

                        initTransfer_List();
                        fnTransfer_Done();

                        _msgText = "Transer thành công.";
                        _msgType = 1;
                    }
                    else
                    {
                        string partName = ds.Tables[0].Rows[0][1].ToString();
                        string partCode = ds.Tables[0].Rows[0][2].ToString();
                        _msgText = "Vị trí này đã gán cho một vật tư khác (" + partName + " [" + partCode + "]). Vui lòng kiểm tra lại.";
                        _msgType = 2;
                    }
                }
                else
                {
                    _msgText = "Không tìm thấy mã vật tư hoặc/và chưa có vị trí mới. Vui lòng kiểm tra lại.";
                    _msgType = 2;
                }

                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }


        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fnTab = tabControl1.SelectedIndex;
            switch (fnTab)
            {
                case 0:
                    initScanIN();
                    break;
                case 1:
                    initHandOver();
                    break;
                case 2:
                    initLocate();
                    break;
                case 3:
                    //initPacking();
                    MessageBox.Show("Chức năng đang phát triển, vui lòng thử lại sau khi hoàn thành.", cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 1;
                    //initReIn();
                    break;
                case 4:
                    initTransfer();
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fnTab = tabControl2.SelectedIndex;
            switch (fnTab)
            {
                case 0:
                    initBinding_ScanIn();
                    break;
                case 1:
                    initBinding_Inventory();
                    break;
                case 2:
                    initBinding_ScanOut();
                    break;
            }
        }

    }
}
