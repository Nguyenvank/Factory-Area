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
    public partial class frmFinishGoodReturn : Form
    {
        public DateTime _dt;
        public string _msgText;
        public byte _msgType;
        public int _returnIDx = 0;

        public int _dgvReturnWidth;

        public string _printRange;
        public int _dgvPrintListWidth;
        public int rowIndexPrintList = 0;
        public string _prtOutName = "";
        public string _prtOutCode = "";
        public string _prtOutType = "";
        public DateTime _prtOutDate;


        public frmFinishGoodReturn()
        {
            InitializeComponent();
        }

        private void frmFinishGoodReturn_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgvReturnWidth = cls.fnGetDataGridWidth(dgvReturn);
            _dgvPrintListWidth = cls.fnGetDataGridWidth(dgvPrintList);
            _printRange = "1";

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            fnGetdate();
            //dtpRIN_Date.Refresh();
            //dtpROUT_Date.Refresh();
        }

        public void init()
        {
            fnGetdate();

            initReturn_DATA();
            initReturn_STAT();
            initReturn_IN();
            initReturn_OUT();
        }

        public void fnGetdate()
        {
            try
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
            catch
            {

            }
            finally
            {

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            initReturn_DATA();
            fnReturn_IN_Finish();
        }


        #region LOAD DATA

        public void initReturn_IMPORT()
        {
            try
            {
                string sql = "V2o1_BASE_Inventory_Return_TabIN_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgvReturnWidth = cls.fnGetDataGridWidth(dgvReturn);
                dgvReturn.DataSource = dt;

                //dgvReturn.Columns[0].Width = 10 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[1].Width = 25 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[2].Width = 20 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[3].Width = 15 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[4].Width = 10 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[5].Width = 10 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[6].Width = 20 * _dgvReturnWidth / 100;    // idx
                //dgvReturn.Columns[7].Width = 10 * _dgvReturnWidth / 100;    // idx
                //dgvReturn.Columns[8].Width = 10 * _dgvReturnWidth / 100;    // idx
                dgvReturn.Columns[9].Width = 10 * _dgvReturnWidth / 100;    // idx

                dgvReturn.Columns[0].Visible = false;
                dgvReturn.Columns[1].Visible = true;
                dgvReturn.Columns[2].Visible = true;
                dgvReturn.Columns[3].Visible = true;
                dgvReturn.Columns[4].Visible = true;
                dgvReturn.Columns[5].Visible = true;
                dgvReturn.Columns[6].Visible = true;
                dgvReturn.Columns[7].Visible = false;
                dgvReturn.Columns[8].Visible = false;
                dgvReturn.Columns[9].Visible = true;

                dgvReturn.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                cls.fnFormatDatagridview(dgvReturn, 12, 30);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void initReturn_EXPORT()
        {
            try
            {
                string sql = "V2o1_BASE_Inventory_Return_TabOUT_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgvReturnWidth = cls.fnGetDataGridWidth(dgvReturn);
                dgvReturn.DataSource = dt;

                //dgvReturn.Columns[0].Width = 10 * _dgvReturnWidth / 100;    // partId
                dgvReturn.Columns[1].Width = 40 * _dgvReturnWidth / 100;    // partname
                dgvReturn.Columns[2].Width = 25 * _dgvReturnWidth / 100;    // partcode
                dgvReturn.Columns[3].Width = 10 * _dgvReturnWidth / 100;    // returnKind
                dgvReturn.Columns[4].Width = 15 * _dgvReturnWidth / 100;    // returnOK
                dgvReturn.Columns[5].Width = 10 * _dgvReturnWidth / 100;    // uom

                dgvReturn.Columns[0].Visible = false;
                dgvReturn.Columns[1].Visible = true;
                dgvReturn.Columns[2].Visible = true;
                dgvReturn.Columns[3].Visible = true;
                dgvReturn.Columns[4].Visible = true;
                dgvReturn.Columns[5].Visible = true;

                cls.fnFormatDatagridview(dgvReturn, 12, 30);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void initReturn_DATA()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                initReturn_IMPORT();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                initReturn_EXPORT();
            }
            else
            {
                initPrint_List();

                dgvReturn.DataSource = "";
                dgvReturn.Refresh();
            }
        }

        private void dgvReturn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvReturn, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvReturn.Rows[e.RowIndex];

            if (tabControl1.SelectedIndex == 0)
            {
                string partId = row.Cells[0].Value.ToString();
                string partname = row.Cells[1].Value.ToString();
                string partcode = row.Cells[2].Value.ToString();
                DateTime returnDate = Convert.ToDateTime(row.Cells[3].Value.ToString());
                string returnKind = row.Cells[4].Value.ToString();
                string returnOK = row.Cells[5].Value.ToString();
                string returnFrom = row.Cells[6].Value.ToString();
                string returnReason = row.Cells[7].Value.ToString();
                string locId = row.Cells[8].Value.ToString();
                string locName = row.Cells[9].Value.ToString();
                string idx = row.Cells[10].Value.ToString();

                _returnIDx = Convert.ToInt32(idx);

                dtpRIN_Date.Value = returnDate;
                cbbRIN_Name.SelectedValue = partId;
                lblRIN_Code.Text = partcode;
                cbbRIN_Type.Text = returnKind;
                txtRIN_Qty.Text = returnOK;
                cbbRIN_From.Text = returnFrom;
                txtRIN_Reason.Text = returnReason;
                cbbRIN_Warehouse.SelectedValue = locId;

                txtRIN_Qty.BackColor = (returnKind == "OK") ? Color.LightGreen : Color.LightPink;

                //txtReturnNG.Text = returnNG;
                //lblSubOK.Text = (returnOK != "0") ? subOK : "N/A";
                //lblSubNG.Text = (returnNG != "0") ? "Garbage" : "N/A";
                //hdfLocId.Text = locId;

                dtpRIN_Date.Enabled = false;
                cbbRIN_Name.Enabled = false;
                cbbRIN_Type.Enabled = true;
                txtRIN_Qty.Enabled = true;
                cbbRIN_From.Enabled = true;
                txtRIN_Reason.Enabled = true;
                cbbRIN_Warehouse.Enabled = true;

                //txtRIN_Qty.Enabled = true;
                //txtReturnNG.Enabled = true;

                rdbRIN_Add.Checked = false;
                rdbRIN_Add.Enabled = false;
                rdbRIN_Upd.Checked = false;
                rdbRIN_Upd.Enabled = true;
                rdbRIN_Del.Checked = false;
                rdbRIN_Del.Enabled = true;

                btnRIN_Save.Enabled = false;
                btnRIN_Finish.Enabled = true;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                string partID = row.Cells[0].Value.ToString();
                string partname = row.Cells[1].Value.ToString();
                string partcode = row.Cells[2].Value.ToString();
                string returnKind = row.Cells[3].Value.ToString();
                string returnQty = row.Cells[4].Value.ToString();
                string partunit = row.Cells[5].Value.ToString();

                cbbROUT_Name.Text = partname;
                lblROUT_Code.Text = partcode;
                cbbROUT_Type.Text = returnKind;
                txtROUT_Qty.Text = returnQty;
                lblROUT_Unit.Text = partunit;

                cbbROUT_Name.Enabled = false;
                lblROUT_Code.Enabled = false;
                cbbROUT_Type.Enabled = false;
                txtROUT_Qty.Enabled = false;
                lblROUT_Unit.Enabled = false;
                txtROUT_Reason.Text = "";
                txtROUT_Reason.Enabled = true;
                cbbROUT_Receiver.SelectedIndex = 0;
                cbbROUT_Receiver.Enabled = false;

                rdbROUT_Add.Enabled = false;
                rdbROUT_Upd.Enabled = false;
                rdbROUT_Del.Enabled = false;

                rdbROUT_Add.Checked = false;
                rdbROUT_Upd.Checked = false;
                rdbROUT_Del.Checked = false;

                btnROUT_Save.Enabled = false;
                btnROUT_Finish.Enabled = true;
            }
        }

        private void dgvReturn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvReturn_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        #endregion


        #region STATISTIC

        public void initReturn_STAT()
        {
            string totalOK = "", totalNG = "", todayIN_OK = "", todayIN_NG = "", todayOUT_OK = "", todayOUT_NG = "";
            string sql = "V2o1_BASE_Inventory_Return_STAT_Counting_SelItem_Addnew";
            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql);
            if (ds.Tables.Count > 0)
            {
                totalOK = ds.Tables[0].Rows[0][0].ToString();
                totalNG = ds.Tables[0].Rows[0][1].ToString();
                todayIN_OK = ds.Tables[0].Rows[0][2].ToString();
                todayIN_NG = ds.Tables[0].Rows[0][3].ToString();
                todayOUT_OK = ds.Tables[0].Rows[0][4].ToString();
                todayOUT_NG = ds.Tables[0].Rows[0][5].ToString();
            }
            else
            {
                totalOK = "0";
                totalNG = "0";
                todayIN_OK = "0";
                todayIN_NG = "0";
                todayOUT_OK = "0";
                todayOUT_NG = "0";
            }

            lblRSTA_Today.Text = String.Format("{0:dd/MM/yyyy}", _dt);
            lblRSTA_Inv_IN.Text = totalOK;
            lblRSTA_Inv_OUT.Text = totalNG;
            lblRSTA_Today_IN_OK.Text = todayIN_OK;
            lblRSTA_Today_IN_NG.Text = todayIN_NG;
            lblRSTA_Today_OUT_OK.Text = todayOUT_OK;
            lblRSTA_Today_OUT_NG.Text = todayOUT_NG;
        }


        #endregion


        #region RETURN IN


        public void initReturn_IN()
        {
            try
            {
                dtpRIN_Date.MaxDate = _dt;
                initReturn_Parts(cbbRIN_Name, "V2o1_BASE_Inventory_Return_IN_Parts_SelItem_Addnew");
                lblRIN_Code.Text = "N/A";
                initReturn_Type(cbbRIN_Type);
                txtRIN_Qty.Text = "0";
                lblRIN_Unit.Text = "";
                initReturn_IN_From();
                initReturn_IN_Warehouse();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void initReturn_IN_From()
        {
            string sql = "V2o1_BASE_Inventory_Return_IN_From_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbRIN_From.DataSource = dt;
            cbbRIN_From.DisplayMember = "Name";
            cbbRIN_From.ValueMember = "CustomerId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbRIN_From.SelectedIndex = 0;
        }

        public void initReturn_IN_Warehouse()
        {
            string sql = "V2o1_BASE_Inventory_Return_IN_Warehouse_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbRIN_Warehouse.DataSource = dt;
            cbbRIN_Warehouse.DisplayMember = "locName";
            cbbRIN_Warehouse.ValueMember = "locID";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbRIN_Warehouse.SelectedIndex = 0;
        }

        private void cbbRIN_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbRIN_Name.SelectedIndex > 0)
            {
                string partIDx = cbbRIN_Name.SelectedValue.ToString();
                string partCode = "", partUnit = "";
                string sql = "V2o1_BASE_Inventory_Return_IN_Name_Selected_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodId";
                sParams[0].Value = partIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    partCode = ds.Tables[0].Rows[0][0].ToString();
                    partUnit = "(" + ds.Tables[0].Rows[0][1].ToString() + ")";
                }
                else
                {
                    partCode = "N/A";
                    partUnit = "";
                }
                lblRIN_Code.Text = partCode;
                lblRIN_Unit.Text = partUnit;
                cbbRIN_Type.Enabled = true;
                btnRIN_Finish.Enabled = true;
            }
            else
            {
                lblRIN_Code.Text = "N/A";
                lblRIN_Unit.Text = "";
                cbbRIN_Type.SelectedIndex = 0;
                cbbRIN_Type.Enabled = false;
                btnRIN_Finish.Enabled = false;
            }
        }

        private void cbbRIN_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbRIN_Type.SelectedIndex >0 )
            {
                txtRIN_Qty.Enabled = true;
                switch(cbbRIN_Type.SelectedIndex)
                {
                    case 1:
                        txtRIN_Qty.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        txtRIN_Qty.BackColor = Color.LightPink;
                        break;
                    default:
                        txtRIN_Qty.BackColor = Color.White;
                        break;
                }
                txtRIN_Qty.Focus();
                cbbRIN_From.Enabled = true;
            }
            else
            {
                txtRIN_Qty.Text = "0";
                txtRIN_Qty.Enabled = false;
                txtRIN_Qty.BackColor = Color.FromKnownColor(KnownColor.Control);
                cbbRIN_From.SelectedIndex = 0;
                cbbRIN_From.Enabled = false;
            }
        }

        private void txtRIN_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cbbRIN_From_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbRIN_From.SelectedIndex > 0)
            {
                txtRIN_Reason.Enabled = true;
            }
            else
            {
                txtRIN_Reason.Text = "";
                txtRIN_Reason.Enabled = false;
            }
        }

        private void txtRIN_Reason_TextChanged(object sender, EventArgs e)
        {
            if (txtRIN_Reason.Text.Length > 0)
            {
                cbbRIN_Warehouse.Enabled = true;
            }
            else
            {
                cbbRIN_Warehouse.SelectedIndex = 0;
                cbbRIN_Warehouse.Enabled = false;
            }
        }

        private void cbbRIN_Warehouse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbRIN_Warehouse.SelectedIndex > 0)
            {
                if (_returnIDx == 0)
                {
                    rdbRIN_Add.Enabled = true;
                }
                else
                {
                    rdbRIN_Add.Enabled = false;
                }
            }
            else
            {
                rdbRIN_Add.Enabled = false;
            }
        }

        private void rdbRIN_Add_CheckedChanged(object sender, EventArgs e)
        {
            btnRIN_Save.Enabled = true;
        }

        private void rdbRIN_Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnRIN_Save.Enabled = true;
        }

        private void rdbRIN_Del_CheckedChanged(object sender, EventArgs e)
        {
            btnRIN_Save.Enabled = true;
        }

        private void btnRIN_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    if (rdbRIN_Add.Checked)
                    {
                        fnReturn_IN_Add();
                    }
                    else if (rdbRIN_Upd.Checked)
                    {
                        fnReturn_IN_Upd();
                    }
                    else if (rdbRIN_Del.Checked)
                    {
                        fnReturn_IN_Del();
                    }

                    initReturn_DATA();
                    initReturn_STAT();
                    fnReturn_IN_Finish();

                    tssMsg.Text = _msgText;
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        private void btnRIN_Finish_Click(object sender, EventArgs e)
        {
            fnReturn_IN_Finish();
        }

        public void fnReturn_IN_Add()
        {
            DateTime inDate = dtpRIN_Date.Value;
            string inPartIDx = cbbRIN_Name.SelectedValue.ToString();
            string inPartName = cbbRIN_Name.Text;
            string inPartCode = lblRIN_Code.Text;
            string inType = (cbbRIN_Type.SelectedIndex > 1) ? "NG" : "OK";
            string inQty = txtRIN_Qty.Text.Trim();
            string inUnit = lblRIN_Unit.Text.Replace("(", "").Replace(")", "");
            string inFrom = cbbRIN_From.Text;
            string inReason = txtRIN_Reason.Text.Trim();
            string inLocIDx = cbbRIN_Warehouse.SelectedValue.ToString();
            string inLocName = cbbRIN_Warehouse.Text.Replace("WH0", "WH");

            string sql = "V2o1_BASE_Inventory_Return_IN_AddItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[10]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@inDate";
            sParams[0].Value = inDate;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "@inPartIDx";
            sParams[1].Value = inPartIDx;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.NVarChar;
            sParams[2].ParameterName = "@inPartName";
            sParams[2].Value = inPartName;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.VarChar;
            sParams[3].ParameterName = "@inPartCode";
            sParams[3].Value = inPartCode;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.VarChar;
            sParams[4].ParameterName = "@inType";
            sParams[4].Value = inType;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.Int;
            sParams[5].ParameterName = "@inQty";
            sParams[5].Value = inQty;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.NVarChar;
            sParams[6].ParameterName = "@inFrom";
            sParams[6].Value = inFrom;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.NVarChar;
            sParams[7].ParameterName = "@inReason";
            sParams[7].Value = inReason;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.Int;
            sParams[8].ParameterName = "@inLocIDx";
            sParams[8].Value = inLocIDx;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.NVarChar;
            sParams[9].ParameterName = "@inLocName";
            sParams[9].Value = inLocName;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Thêm mới thành công";
            _msgType = 1;
        }

        public void fnReturn_IN_Upd()
        {
            string inIDx = _returnIDx.ToString();
            DateTime inDate = dtpRIN_Date.Value;
            string inPartIDx = cbbRIN_Name.SelectedValue.ToString();
            string inPartName = cbbRIN_Name.Text;
            string inPartCode = lblRIN_Code.Text;
            string inType = (cbbRIN_Type.SelectedIndex > 1) ? "NG" : "OK";
            string inQty = txtRIN_Qty.Text.Trim();
            string inUnit = lblRIN_Unit.Text.Replace("(", "").Replace(")", "");
            string inFrom = cbbRIN_From.Text;
            string inReason = txtRIN_Reason.Text.Trim();
            string inLocIDx = cbbRIN_Warehouse.SelectedValue.ToString();
            string inLocName = cbbRIN_Warehouse.Text.Replace("WH0", "WH");

            string sql = "V2o1_BASE_Inventory_Return_IN_UpdItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[11]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@inIDx";
            sParams[0].Value = inIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@inDate";
            sParams[1].Value = inDate;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "@inPartIDx";
            sParams[2].Value = inPartIDx;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "@inPartName";
            sParams[3].Value = inPartName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.VarChar;
            sParams[4].ParameterName = "@inPartCode";
            sParams[4].Value = inPartCode;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.VarChar;
            sParams[5].ParameterName = "@inType";
            sParams[5].Value = inType;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.Int;
            sParams[6].ParameterName = "@inQty";
            sParams[6].Value = inQty;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.NVarChar;
            sParams[7].ParameterName = "@inFrom";
            sParams[7].Value = inFrom;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.NVarChar;
            sParams[8].ParameterName = "@inReason";
            sParams[8].Value = inReason;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.Int;
            sParams[9].ParameterName = "@inLocIDx";
            sParams[9].Value = inLocIDx;

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.NVarChar;
            sParams[10].ParameterName = "@inLocName";
            sParams[10].Value = inLocName;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Cập nhật thành công";
            _msgType = 1;
        }

        public void fnReturn_IN_Del()
        {
            string inIDx = _returnIDx.ToString();
            string sql = "V2o1_BASE_Inventory_Return_IN_DelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@inIDx";
            sParams[0].Value = inIDx;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Xóa thành công";
            _msgType = 1;
        }

        public void fnReturn_IN_Finish()
        {
            dtpRIN_Date.Value = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            cbbRIN_Name.SelectedIndex = 0;
            lblRIN_Code.Text = "N/A";
            cbbRIN_Type.SelectedIndex = 0;
            txtRIN_Qty.Text = "0";
            txtRIN_Qty.BackColor = Color.FromKnownColor(KnownColor.Control);
            cbbRIN_From.SelectedIndex = 0;
            txtRIN_Reason.Text = "";
            cbbRIN_Warehouse.SelectedIndex = 0;

            dtpRIN_Date.Enabled = true;
            cbbRIN_Name.Enabled = true;
            cbbRIN_Type.Enabled = false;
            txtRIN_Qty.Enabled = false;
            cbbRIN_From.Enabled = false;
            txtRIN_Reason.Enabled = false;
            cbbRIN_Warehouse.Enabled = false;

            rdbRIN_Add.Enabled = false;
            rdbRIN_Add.Checked = false;
            rdbRIN_Upd.Enabled = false;
            rdbRIN_Upd.Checked = false;
            rdbRIN_Del.Enabled = false;
            rdbRIN_Del.Checked = false;
            btnRIN_Save.Enabled = false;
            btnRIN_Finish.Enabled = false;

            dgvReturn.ClearSelection();
            dgvReturn.Refresh();
        }


        #endregion


        #region RETURN OUT


        public void initReturn_OUT()
        {
            try
            {
                dtpROUT_Date.Enabled = false;
                initReturn_Parts(cbbROUT_Name, "V2o1_BASE_Inventory_Return_OUT_Parts_SelItem_Addnew");
                lblROUT_Code.Text = "N/A";
                initReturn_Type(cbbROUT_Type);
                initReturn_OUT_Receiver();

                rdbROUT_Add.Enabled = false;
                rdbROUT_Upd.Enabled = false;
                rdbROUT_Del.Enabled = false;

                btnROUT_Save.Enabled = false;
                btnROUT_Finish.Enabled = false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void initReturn_OUT_Receiver()
        {
            string sql = "V2o1_BASE_Inventory_Return_OUT_Receiver_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbROUT_Receiver.DataSource = dt;
            cbbROUT_Receiver.DisplayMember = "picName";
            cbbROUT_Receiver.ValueMember = "idx";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbROUT_Receiver.SelectedIndex = 0;

        }

        private void cbbROUT_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (cbbROUT_Name.SelectedIndex > 0)
            //{
            //    cbbROUT_Type.Enabled = true;
            //}
            //else
            //{
            //    cbbROUT_Type.SelectedIndex = 0;
            //    cbbROUT_Type.Enabled = false;
            //}
        }

        private void cbbROUT_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (cbbROUT_Type.SelectedIndex > 0)
            //{
            //    txtROUT_Reason.Enabled = true;
            //}
            //else
            //{
            //    txtROUT_Reason.Text = "";
            //    txtROUT_Reason.Enabled = false;
            //}
        }

        private void txtROUT_Reason_TextChanged(object sender, EventArgs e)
        {
            if (txtROUT_Reason.Text.Length > 0)
            {
                cbbROUT_Receiver.Enabled = true;
            }
            else
            {
                cbbROUT_Receiver.SelectedIndex = 0;
                cbbROUT_Receiver.Enabled = false;
            }
        }

        private void cbbROUT_Receiver_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbROUT_Receiver.SelectedIndex > 0)
            {
                rdbROUT_Add.Enabled = true;
            }
            else
            {
                rdbROUT_Add.Enabled = false;
            }
            rdbROUT_Add.Checked = false;
            rdbROUT_Upd.Checked = false;
            rdbROUT_Del.Checked = false;

            rdbROUT_Upd.Enabled = false;
            rdbROUT_Del.Enabled = false;
        }

        private void rdbROUT_Add_CheckedChanged(object sender, EventArgs e)
        {
            btnROUT_Save.Enabled = true;
        }

        private void rdbROUT_Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnROUT_Save.Enabled = true;
        }

        private void rdbROUT_Del_CheckedChanged(object sender, EventArgs e)
        {
            btnROUT_Save.Enabled = true;
        }

        private void btnROUT_Finish_Click(object sender, EventArgs e)
        {
            fnReturn_OUT_Finish();
        }

        private void btnROUT_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (rdbROUT_Add.Checked)
                {
                    fnReturn_OUT_Add();
                }
                else if (rdbROUT_Upd.Checked)
                {
                    fnReturn_OUT_Upd();
                }
                else if (rdbROUT_Del.Checked)
                {
                    fnReturn_OUT_Del();
                }

                initReturn_DATA();
                initReturn_STAT();
                fnReturn_OUT_Finish();

                tssMsg.Text = _msgText;
            }
        }

        public void fnReturn_OUT_Add()
        {
            try
            {
                DateTime outDate = dtpROUT_Date.Value;
                string outPartIDx = cbbROUT_Name.SelectedValue.ToString();
                string outPartName = cbbROUT_Name.Text;
                string outPartCode = lblROUT_Code.Text;
                string outType = cbbROUT_Type.Text;
                string outQty = txtROUT_Qty.Text;
                string outReason = txtROUT_Reason.Text.Trim();
                string outReceiver = cbbROUT_Receiver.Text;

                string sql = "V2o1_BASE_Inventory_Return_OUT_AddItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[8]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.DateTime;
                sParams[0].ParameterName = "@outDate";
                sParams[0].Value = outDate;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@outPartIDx";
                sParams[1].Value = outPartIDx;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.NVarChar;
                sParams[2].ParameterName = "@outPartName";
                sParams[2].Value = outPartName;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.VarChar;
                sParams[3].ParameterName = "@outPartCode";
                sParams[3].Value = outPartCode;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.VarChar;
                sParams[4].ParameterName = "@outType";
                sParams[4].Value = outType;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.Int;
                sParams[5].ParameterName = "@outQty";
                sParams[5].Value = outQty;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.NVarChar;
                sParams[6].ParameterName = "@outReason";
                sParams[6].Value = outReason;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.NVarChar;
                sParams[7].ParameterName = "@outReceiver";
                sParams[7].Value = outReceiver;

                cls.fnUpdDel(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnReturn_OUT_Upd()
        {

        }

        public void fnReturn_OUT_Del()
        {

        }

        public void fnReturn_OUT_Finish()
        {
            cbbROUT_Name.SelectedIndex = 0;
            lblROUT_Code.Text = "N/A";
            cbbROUT_Type.SelectedIndex = 0;
            txtROUT_Qty.Text = "0";
            txtROUT_Reason.Text = "";
            cbbROUT_Receiver.SelectedIndex = 0;

            cbbROUT_Name.Enabled = false;
            lblROUT_Code.Enabled = false;
            cbbROUT_Type.Enabled = false;
            txtROUT_Qty.Enabled = false;
            txtROUT_Reason.Enabled = false;
            cbbROUT_Receiver.Enabled = false;

            rdbROUT_Add.Enabled = false;
            rdbROUT_Upd.Enabled = false;
            rdbROUT_Del.Enabled = false;

            rdbROUT_Add.Checked = false;
            rdbROUT_Upd.Checked = false;
            rdbROUT_Del.Checked = false;

            btnROUT_Save.Enabled = false;
            btnROUT_Finish.Enabled = false;

            dgvReturn.ClearSelection();
            dgvReturn.Refresh();
        }




        #endregion


        #region PRINT LIST


        public void initPrint_List()
        {
            try
            {
                string rangeTime = _printRange;
                string sql = "V2o1_BASE_Inventory_Return_OUT_Print_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@rangeTime";
                sParams[0].Value = rangeTime;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvPrintListWidth = cls.fnGetDataGridWidth(dgvPrintList);
                dgvPrintList.DataSource = dt;

                dgvPrintList.Columns[0].Width = 45 * _dgvPrintListWidth / 100;    // partname
                //dgvPrintList.Columns[1].Width = 25 * _dgvPrintListWidth / 100;    // partcode
                dgvPrintList.Columns[2].Width = 15 * _dgvPrintListWidth / 100;    // returnKind
                dgvPrintList.Columns[3].Width = 40 * _dgvPrintListWidth / 100;    // out_date

                dgvPrintList.Columns[0].Visible = true;
                dgvPrintList.Columns[1].Visible = false;
                dgvPrintList.Columns[2].Visible = true;
                dgvPrintList.Columns[3].Visible = true;

                dgvPrintList.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                cls.fnFormatDatagridview(dgvPrintList, 10, 30);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void initPrint_List_Detail()
        {
            string outName = _prtOutName;
            string outCode = _prtOutCode;
            string outType = _prtOutType;
            DateTime outDate = _prtOutDate;
            string sql = "V2o1_BASE_Inventory_Return_OUT_Print_Detail_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[4]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.NVarChar;
            sParams[0].ParameterName = "@outName";
            sParams[0].Value = outName;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@outCode";
            sParams[1].Value = outCode;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.VarChar;
            sParams[2].ParameterName = "@outType";
            sParams[2].Value = outType;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.DateTime;
            sParams[3].ParameterName = "@outDate";
            sParams[3].Value = outDate;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvReturnWidth = cls.fnGetDataGridWidth(dgvReturn);
            dgvReturn.DataSource = dt;

            ////dgvReturn.Columns[0].Width = 10 * _dgvReturnWidth / 100;    // partId
            //dgvReturn.Columns[1].Width = 25 * _dgvReturnWidth / 100;    // partname
            //dgvReturn.Columns[2].Width = 20 * _dgvReturnWidth / 100;    // partcode
            //dgvReturn.Columns[3].Width = 15 * _dgvReturnWidth / 100;    // returnDate
            //dgvReturn.Columns[4].Width = 10 * _dgvReturnWidth / 100;    // returnKind
            //dgvReturn.Columns[5].Width = 10 * _dgvReturnWidth / 100;    // returnOK
            //dgvReturn.Columns[6].Width = 20 * _dgvReturnWidth / 100;    // returnFrom
            ////dgvReturn.Columns[7].Width = 10 * _dgvReturnWidth / 100;    // returnReason
            ////dgvReturn.Columns[8].Width = 10 * _dgvReturnWidth / 100;    // locId
            //dgvReturn.Columns[9].Width = 10 * _dgvReturnWidth / 100;    // locName

            //dgvReturn.Columns[0].Visible = false;
            //dgvReturn.Columns[1].Visible = true;
            //dgvReturn.Columns[2].Visible = true;
            //dgvReturn.Columns[3].Visible = true;
            //dgvReturn.Columns[4].Visible = true;
            //dgvReturn.Columns[5].Visible = true;
            //dgvReturn.Columns[6].Visible = true;
            //dgvReturn.Columns[7].Visible = false;
            //dgvReturn.Columns[8].Visible = false;
            //dgvReturn.Columns[9].Visible = true;

            dgvReturn.Columns[0].Width = 25 * _dgvReturnWidth / 100;    // partname
            dgvReturn.Columns[1].Width = 20 * _dgvReturnWidth / 100;    // partcode
            dgvReturn.Columns[2].Width = 15 * _dgvReturnWidth / 100;    // returnDate
            dgvReturn.Columns[3].Width = 10 * _dgvReturnWidth / 100;    // returnKind
            dgvReturn.Columns[4].Width = 10 * _dgvReturnWidth / 100;    // returnOK
            dgvReturn.Columns[5].Width = 20 * _dgvReturnWidth / 100;    // returnFrom
            dgvReturn.Columns[6].Width = 10 * _dgvReturnWidth / 100;    // returnReason
            dgvReturn.Columns[7].Width = 10 * _dgvReturnWidth / 100;    // locName
            dgvReturn.Columns[8].Width = 10 * _dgvReturnWidth / 100;    // receiver
            dgvReturn.Columns[9].Width = 10 * _dgvReturnWidth / 100;    // sign

            dgvReturn.Columns[0].Visible = true;
            dgvReturn.Columns[1].Visible = true;
            dgvReturn.Columns[2].Visible = true;
            dgvReturn.Columns[3].Visible = true;
            dgvReturn.Columns[4].Visible = true;
            dgvReturn.Columns[5].Visible = true;
            dgvReturn.Columns[6].Visible = true;
            dgvReturn.Columns[7].Visible = true;
            dgvReturn.Columns[8].Visible = true;
            dgvReturn.Columns[9].Visible = true;

            dgvReturn.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgvReturn, 12, 30);

        }

        private void dgvPrintList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvPrintList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvPrintList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvPrintList.Rows[e.RowIndex];

            string partName = row.Cells[0].Value.ToString();
            string partCode = row.Cells[1].Value.ToString();
            string returnType = row.Cells[2].Value.ToString();
            DateTime returnDate = Convert.ToDateTime(row.Cells[3].Value.ToString());

            _prtOutName = partName;
            _prtOutCode = partCode;
            _prtOutType = returnType;
            _prtOutDate = returnDate;

            //string msg = "";
            //msg += "partName: " + partName + "\r\n";
            //msg += "partCode: " + partCode + "\r\n";
            //msg += "returnType: " + returnType + "\r\n";
            //msg += "returnDate: " + returnDate + "\r\n";
            //MessageBox.Show(msg);

            initPrint_List_Detail();
        }

        private void dgvPrintList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvPrintList.Rows[e.RowIndex].Selected = true;
                rowIndexPrintList = e.RowIndex;
                dgvPrintList.CurrentCell = dgvPrintList.Rows[e.RowIndex].Cells[2];
                contextMenuStrip1.Show(this.dgvPrintList, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void lnkPrint_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _printRange = "1";
            dgvReturn.DataSource = "";
            dgvReturn.Refresh();
            initPrint_List();
        }

        private void lnkPrint_3day_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _printRange = "2";
            dgvReturn.DataSource = "";
            dgvReturn.Refresh();
            initPrint_List();
        }

        private void lnkPrint_2week_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _printRange = "3";
            dgvReturn.DataSource = "";
            dgvReturn.Refresh();
            initPrint_List();
        }

        private void lnkPrint_1month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _printRange = "4";
            dgvReturn.DataSource = "";
            dgvReturn.Refresh();
            initPrint_List();
        }

        private void lnkPrint_all_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _printRange = "5";
            dgvReturn.DataSource = "";
            dgvReturn.Refresh();
            initPrint_List();
        }

        private void refreshThisListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initPrint_List();
        }

        private void printThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsPrint _ClsPrint = new ClsPrint(dgvReturn, "BIÊN BẢN GIAO HÀNG");
            _ClsPrint.PrintForm();
        }


        #endregion


        #region RETURN COMMON


        public void initReturn_Parts(ComboBox cbb, string sqlParts)
        {
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sqlParts);
            cbb.DataSource = dt;
            cbb.DisplayMember = "Name";
            cbb.ValueMember = "prodId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbb.SelectedIndex = 0;
        }

        public void initReturn_Type(ComboBox cbb)
        {
            cbb.Items.Add("OK");
            cbb.Items.Add("NG");
            cbb.Items.Insert(0, "");
            cbb.SelectedIndex = 0;
        }



        #endregion


        public void fnBindReturn()
        {
            try
            {
                //DateTime fr = dtpReturnDateFr.Value;
                //DateTime to = dtpReturnDateTo.Value;
                //string sql = "V2o1_BASE_Warehouse_FinishGood_Return_SelItem_Addnew";
                //DataTable dt = new DataTable();
                //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                //sParams[0] = new SqlParameter();
                //sParams[0].SqlDbType = SqlDbType.DateTime;
                //sParams[0].ParameterName = "@fr";
                //sParams[0].Value = fr;

                //sParams[1] = new SqlParameter();
                //sParams[1].SqlDbType = SqlDbType.DateTime;
                //sParams[1].ParameterName = "@to";
                //sParams[1].Value = to;

                //dt = cls.ExecuteDataTable(sql, sParams);

                ////foreach (DataRow dr in dt.Rows)
                ////{
                ////    string qty = dr[2].ToString();
                ////    if (qty == "0")
                ////    {
                ////        dr[3] = "";
                ////    }
                ////}

                //dgvReturn.DataSource = dt;
                //dgvReturn.Refresh();


                ////dgvReturn.Columns[0].Width = 20 * _dgvReturnWidth / 100;    // idx
                ////dgvReturn.Columns[1].Width = 40 * _dgvReturnWidth / 100;    // partId
                //dgvReturn.Columns[2].Width = 22 * _dgvReturnWidth / 100;    // partname
                //dgvReturn.Columns[3].Width = 15 * _dgvReturnWidth / 100;    // partcode
                //dgvReturn.Columns[4].Width = 15 * _dgvReturnWidth / 100;    // date return
                //dgvReturn.Columns[5].Width = 10 * _dgvReturnWidth / 100;    // ok
                //dgvReturn.Columns[6].Width = 10 * _dgvReturnWidth / 100;    // ng
                //dgvReturn.Columns[7].Width = 6 * _dgvReturnWidth / 100;    // unit
                ////dgvReturn.Columns[8].Width = 10 * _dgvReturnWidth / 100;    // locId
                //dgvReturn.Columns[9].Width = 10 * _dgvReturnWidth / 100;    // locName
                //dgvReturn.Columns[10].Width = 12 * _dgvReturnWidth / 100;    // subOK

                //dgvReturn.Columns[0].Visible = false;
                //dgvReturn.Columns[1].Visible = false;
                //dgvReturn.Columns[2].Visible = true;
                //dgvReturn.Columns[3].Visible = true;
                //dgvReturn.Columns[4].Visible = true;
                //dgvReturn.Columns[5].Visible = true;
                //dgvReturn.Columns[6].Visible = true;
                //dgvReturn.Columns[7].Visible = true;
                //dgvReturn.Columns[8].Visible = false;
                //dgvReturn.Columns[9].Visible = true;
                //dgvReturn.Columns[10].Visible = true;

                //cls.fnFormatDatagridview(dgvReturn, 11);
            }
            catch
            {

            }
            finally
            {

            }


        }

        public void fnBindCbbPartname()
        {
            //string sql = "V2o1_BASE_Warehouse_FinishGood_Partname_SelItem_Addnew";
            //DataTable dt = new DataTable();
            //dt = cls.fnSelect(sql);
            //cbbReturnName.DataSource = dt;
            //cbbReturnName.DisplayMember = "Name";
            //cbbReturnName.ValueMember = "prodId";
            //dt.Rows.InsertAt(dt.NewRow(), 0);
            //cbbReturnName.SelectedIndex = 0;
        }

        private void dtpReturnDateFr_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ////dtpReturnDateTo.Value = dtpReturnDateFr.Value;
                //dtpReturnDateTo.MinDate = dtpReturnDateFr.Value;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dtpReturnDateTo_ValueChanged(object sender, EventArgs e)
        {
            fnBindReturn();
        }

        private void btnReturnSave_Click(object sender, EventArgs e)
        {
            //DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            //if (dialogResultAdd == DialogResult.Yes)
            //{
            //    if(rdbReturnAdd.Checked)
            //    {
            //        fnReturnAdd();
            //    }
            //    else if(rdbReturnUpd.Checked)
            //    {
            //        fnReturnUpd();
            //    }
            //    else if(rdbReturnDel.Checked)
            //    {
            //        fnReturnDel();
            //    }
            //}
            //dtpReturnDateFr.Value = dtpReturnDate.Value;
            //dtpReturnDateTo.Value = dtpReturnDate.Value;
            //fnBindReturn();

            //cbbReturnName.SelectedIndex = 0;
            //lblReturnCode.Text = "";
            //txtReturnOK.Text = "";
            //txtReturnNG.Text = "";
            //lblSubOK.Text = "N/A";
            //lblSubNG.Text = "N/A";
            //hdfLocId.Text = "";
            //rdbReturnAdd.Checked = false;
            //rdbReturnAdd.Enabled = false;
            //rdbReturnUpd.Checked = false;
            //rdbReturnUpd.Enabled = false;
            //rdbReturnDel.Checked = false;
            //rdbReturnDel.Enabled = false;
            //btnReturnSave.Enabled = false;
        }

        public void fnReturnAdd()
        {
            //DateTime date = dtpReturnDate.Value;
            //string partId = cbbReturnName.SelectedValue.ToString();
            //string partname = cbbReturnName.Text;
            //string partcode = lblReturnCode.Text;
            //string returnOK = (txtReturnOK.Text.Trim() != "") ? txtReturnOK.Text.Trim() : "0";
            //string returnNG = (txtReturnNG.Text.Trim() != "") ? txtReturnNG.Text.Trim() : "0";
            //string locId = hdfLocId.Text.Trim();
            //string subId = lblSubOK.Text;

            //string sql = "V2o1_BASE_Warehouse_FinishGood_Return_AddItem_Addnew";
            //SqlParameter[] sParams = new SqlParameter[8]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.DateTime;
            //sParams[0].ParameterName = "@date";
            //sParams[0].Value = date;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.Int;
            //sParams[1].ParameterName = "@partId";
            //sParams[1].Value = partId;

            //sParams[2] = new SqlParameter();
            //sParams[2].SqlDbType = SqlDbType.NVarChar;
            //sParams[2].ParameterName = "@partname";
            //sParams[2].Value = partname;

            //sParams[3] = new SqlParameter();
            //sParams[3].SqlDbType = SqlDbType.VarChar;
            //sParams[3].ParameterName = "@partcode";
            //sParams[3].Value = partcode;

            //sParams[4] = new SqlParameter();
            //sParams[4].SqlDbType = SqlDbType.Int;
            //sParams[4].ParameterName = "@returnOK";
            //sParams[4].Value = returnOK;

            //sParams[5] = new SqlParameter();
            //sParams[5].SqlDbType = SqlDbType.Int;
            //sParams[5].ParameterName = "@returnNG";
            //sParams[5].Value = returnNG;

            //sParams[6] = new SqlParameter();
            //sParams[6].SqlDbType = SqlDbType.Int;
            //sParams[6].ParameterName = "@locId";
            //sParams[6].Value = locId;

            //sParams[7] = new SqlParameter();
            //sParams[7].SqlDbType = SqlDbType.VarChar;
            //sParams[7].ParameterName = "@subOK";
            //sParams[7].Value = subId;

            //cls.fnUpdDel(sql, sParams);
        }

        public void fnReturnUpd()
        {
            //string idx = _returnId;
            //DateTime date = dtpReturnDate.Value;
            //string partId = cbbReturnName.SelectedValue.ToString();
            //string partname = cbbReturnName.Text;
            //string partcode = lblReturnCode.Text;
            //string returnOK = (txtReturnOK.Text.Trim() != "") ? txtReturnOK.Text.Trim() : "0";
            //string returnNG = (txtReturnNG.Text.Trim() != "") ? txtReturnNG.Text.Trim() : "0";
            //string locId = hdfLocId.Text.Trim();
            //string subId = lblSubOK.Text;

            //string sql = "V2o1_BASE_Warehouse_FinishGood_Return_UpdItem_Addnew";
            //SqlParameter[] sParams = new SqlParameter[9]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.DateTime;
            //sParams[0].ParameterName = "@date";
            //sParams[0].Value = date;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.Int;
            //sParams[1].ParameterName = "@partId";
            //sParams[1].Value = partId;

            //sParams[2] = new SqlParameter();
            //sParams[2].SqlDbType = SqlDbType.NVarChar;
            //sParams[2].ParameterName = "@partname";
            //sParams[2].Value = partname;

            //sParams[3] = new SqlParameter();
            //sParams[3].SqlDbType = SqlDbType.VarChar;
            //sParams[3].ParameterName = "@partcode";
            //sParams[3].Value = partcode;

            //sParams[4] = new SqlParameter();
            //sParams[4].SqlDbType = SqlDbType.Int;
            //sParams[4].ParameterName = "@returnOK";
            //sParams[4].Value = returnOK;

            //sParams[5] = new SqlParameter();
            //sParams[5].SqlDbType = SqlDbType.Int;
            //sParams[5].ParameterName = "@returnNG";
            //sParams[5].Value = returnNG;

            //sParams[6] = new SqlParameter();
            //sParams[6].SqlDbType = SqlDbType.Int;
            //sParams[6].ParameterName = "@locId";
            //sParams[6].Value = locId;

            //sParams[7] = new SqlParameter();
            //sParams[7].SqlDbType = SqlDbType.VarChar;
            //sParams[7].ParameterName = "@subOK";
            //sParams[7].Value = subId;

            //sParams[8] = new SqlParameter();
            //sParams[8].SqlDbType = SqlDbType.Int;
            //sParams[8].ParameterName = "@idx";
            //sParams[8].Value = idx;

            //cls.fnUpdDel(sql, sParams);
        }

        public void fnReturnDel()
        {
            //string idx = _returnId;

            //string sql = "V2o1_BASE_Warehouse_FinishGood_Return_DelItem_Addnew";
            //SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@idx";
            //sParams[0].Value = idx;

            //cls.fnUpdDel(sql, sParams);

            //dtpReturnDate.Value = DateTime.Now;
            //dtpReturnDate.Enabled = true;
            //cbbReturnName.Enabled = true;
        }

        private void cbbReturnName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //string partId = cbbReturnName.SelectedValue.ToString();
            //string sql = "V2o1_BASE_Warehouse_FinishGood_Partcode_SelItem_Addnew";
            //SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "prodId";
            //sParams[0].Value = partId;

            //DataSet ds = new DataSet();
            //ds = cls.ExecuteDataSet(sql, sParams);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lblReturnCode.Text = ds.Tables[0].Rows[0][0].ToString();
            //}
            //else
            //{
            //    lblReturnCode.Text = "N/A";
            //}

            //txtReturnOK.Text = "0";
            //txtReturnOK.Enabled = true;
            //txtReturnNG.Text = "0";
            //txtReturnNG.Enabled = true;
            //fnBindLocSub();
        }

        public void fnBindLocSub()
        {
            //string partId = cbbReturnName.SelectedValue.ToString();
            //string sql = "V2o1_BASE_Warehouse_FinishGood_LocSub_SelItem_Addnew";
            //SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@prodId";
            //sParams[0].Value = partId;

            //string locSub = "";
            //DataSet ds = new DataSet();
            //ds = cls.ExecuteDataSet(sql, sParams);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    hdfLocId.Text = ds.Tables[0].Rows[0][0].ToString();
            //    locSub = ds.Tables[0].Rows[0][1].ToString();

            //    lblSubOK.Text = (txtReturnOK.Text != "0") ? locSub : "N/A";
            //    lblSubNG.Text = (txtReturnNG.Text != "0") ? "Garbage" : "N/A";
            //}
            //else
            //{
            //    lblSubOK.Text = "N/A";
            //    lblSubNG.Text = "N/A";
            //}
        }

        private void txtReturnOK_TextChanged(object sender, EventArgs e)
        {
            //if(txtReturnOK.Text=="")
            //{
            //    txtReturnOK.Text = "0";

            //    rdbReturnAdd.Checked = false;
            //    rdbReturnAdd.Enabled = false;
            //    rdbReturnUpd.Checked = false;
            //    rdbReturnUpd.Enabled = false;
            //    rdbReturnDel.Checked = false;
            //    rdbReturnDel.Enabled = false;
            //    btnReturnSave.Enabled = false;
            //}

            //if (txtReturnOK.Text!="0")
            //{
            //    if (_returnId != "0")
            //    {
            //        rdbReturnAdd.Checked = false;
            //        rdbReturnAdd.Enabled = false;
            //        rdbReturnUpd.Checked = false;
            //        rdbReturnUpd.Enabled = true;
            //        rdbReturnDel.Checked = false;
            //        rdbReturnDel.Enabled = true;
            //        btnReturnSave.Enabled = false;
            //    }
            //    else
            //    {
            //        rdbReturnAdd.Checked = true;
            //        rdbReturnAdd.Enabled = true;
            //        rdbReturnUpd.Checked = false;
            //        rdbReturnUpd.Enabled = false;
            //        rdbReturnDel.Checked = false;
            //        rdbReturnDel.Enabled = false;
            //        btnReturnSave.Enabled = true;
            //    }
            //    fnBindLocSub();
            //}
            //else
            //{
            //    rdbReturnAdd.Checked = false;
            //    rdbReturnAdd.Enabled = false;
            //    rdbReturnUpd.Checked = false;
            //    rdbReturnUpd.Enabled = false;
            //    rdbReturnDel.Checked = false;
            //    rdbReturnDel.Enabled = false;
            //    btnReturnSave.Enabled = false;
            //}
        }

        private void txtReturnNG_TextChanged(object sender, EventArgs e)
        {
            //if (txtReturnNG.Text == "")
            //{
            //    txtReturnNG.Text = "0";

            //    rdbReturnAdd.Checked = false;
            //    rdbReturnAdd.Enabled = false;
            //    rdbReturnUpd.Checked = false;
            //    rdbReturnUpd.Enabled = false;
            //    rdbReturnDel.Checked = false;
            //    rdbReturnDel.Enabled = false;
            //    btnReturnSave.Enabled = false;
            //}

            //if (txtReturnNG.Text != "0")
            //{
            //    if (_returnId != "0")
            //    {
            //        rdbReturnAdd.Checked = false;
            //        rdbReturnAdd.Enabled = false;
            //        rdbReturnUpd.Checked = false;
            //        rdbReturnUpd.Enabled = true;
            //        rdbReturnDel.Checked = false;
            //        rdbReturnDel.Enabled = true;
            //        btnReturnSave.Enabled = false;
            //    }
            //    else
            //    {
            //        rdbReturnAdd.Checked = true;
            //        rdbReturnAdd.Enabled = true;
            //        rdbReturnUpd.Checked = false;
            //        rdbReturnUpd.Enabled = false;
            //        rdbReturnDel.Checked = false;
            //        rdbReturnDel.Enabled = false;
            //        btnReturnSave.Enabled = true;
            //    }
            //    fnBindLocSub();
            //}
            //else
            //{
            //    rdbReturnAdd.Checked = false;
            //    rdbReturnAdd.Enabled = false;
            //    rdbReturnUpd.Checked = false;
            //    rdbReturnUpd.Enabled = false;
            //    rdbReturnDel.Checked = false;
            //    rdbReturnDel.Enabled = false;
            //    btnReturnSave.Enabled = false;
            //}
        }

        private void rdbReturnUpd_CheckedChanged(object sender, EventArgs e)
        {
            //btnReturnSave.Enabled = true;
        }

        private void rdbReturnDel_CheckedChanged(object sender, EventArgs e)
        {
            //btnReturnSave.Enabled = true;
        }

        private void btnReturnExcel_Click(object sender, EventArgs e)
        {
            //fnBindReturn();
            //if (dgvReturn.Rows.Count>0)
            //{
            //    byte export = 0;
            //    export = cls.ExportToExcel(dgvReturn, "FinishGoodReturn");
            //    if (export == 1)
            //    {
            //        MessageBox.Show("Export successful.");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Export failure. Please try again.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Have no data to export.\r\nPlease choose another range of date and try again.");
            //}
        }

        public void initFGNG()
        {
            //dtpNG_Fr.CustomFormat = "dd/MM/yyyy";
            //dtpNG_To.CustomFormat = "dd/MM/yyyy";

            //fnBindFGNG();

            //txtNG_Packcode.Text = "";
            //txtNG_Packcode.Enabled = true;
            //txtNG_Packcode.Focus();
            //btnNG_Check.Enabled = false;
            //lblNG_Partname.Text = "Partname: N/A";
            //lblNG_Partcode.Text = "Partcode: N/A";
            //txtNG_CurrQty.Text = "";
            //txtNG_CurrQty.ReadOnly = true;
            //txtNG_Std.Text = "";
            //txtNG_Std.ReadOnly = true;
            //txtNG_NGQty.Text = "";
            //txtNG_NGQty.Enabled = false;
            //txtNG_Note.Text = "";
            //txtNG_Note.Enabled = false;
            //rdbNG_Add.Checked = false;
            //rdbNG_Add.Enabled = false;
            //rdbNG_Upd.Checked = false;
            //rdbNG_Upd.Enabled = false;
            //rdbNG_Del.Checked = false;
            //rdbNG_Del.Enabled = false;
            //btnNG_Save.Enabled = false;
        }

        public void fnBindFGNG()
        {

        }

        private void txtNG_Packcode_TextChanged(object sender, EventArgs e)
        {
            //if(txtNG_Packcode.Text!="")
            //{
            //    btnNG_Check.Enabled = true;
            //}
            //else
            //{
            //    btnNG_Check.Enabled = false;
            //}
        }

        private void btnNG_Check_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string packCode = txtNG_Packcode.Text.Trim();
            //    string sql = "V2o1_BASE_Warehouse_FinishGoodNG_CheckCode_SelItem_Addnew";
            //    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            //    sParams[0] = new SqlParameter();
            //    sParams[0].SqlDbType = SqlDbType.VarChar;
            //    sParams[0].ParameterName = "@packCode";
            //    sParams[0].Value = packCode;

            //    DataSet dsCheck = new DataSet();
            //    dsCheck = cls.ExecuteDataSet(sql, sParams);
            //    if(dsCheck.Tables.Count>0)
            //    {
            //        if (dsCheck.Tables[0].Rows.Count > 0)
            //        {
            //            string msg = "";
            //            string packId = dsCheck.Tables[0].Rows[0][0].ToString();
            //            string packcode = dsCheck.Tables[0].Rows[0][1].ToString();
            //            string packUse = dsCheck.Tables[0].Rows[0][2].ToString();
            //            string location = dsCheck.Tables[0].Rows[0][3].ToString();
            //            string sublocate = dsCheck.Tables[0].Rows[0][4].ToString();
            //            string packdate = dsCheck.Tables[0].Rows[0][5].ToString();
            //            string packLOT = dsCheck.Tables[0].Rows[0][6].ToString();
            //            string packQty = dsCheck.Tables[0].Rows[0][7].ToString();
            //            string partname = dsCheck.Tables[0].Rows[0][8].ToString();
            //            string partcode = dsCheck.Tables[0].Rows[0][9].ToString();
            //            string packActivity = dsCheck.Tables[0].Rows[0][10].ToString();
            //            string orderNumber = dsCheck.Tables[0].Rows[0][11].ToString();
            //            string outDate = dsCheck.Tables[0].Rows[0][12].ToString();
            //            string packStd = dsCheck.Tables[0].Rows[0][13].ToString();
            //            string shiftnm = (cls.Right(packLOT, 1) == "1") ? "day" : "night";

            //            if (orderNumber != "" || packActivity.ToLower() != "in")
            //            {
            //                msg = "THIS PACKING WAS DELIVERIED\r\n";
            //                msg += "\r\n";

            //                msg += packcode.ToUpper() + "\r\n";
            //                msg += "The last scan in date: " + packdate + ", in " + shiftnm.ToUpper() + " shift (LOT: " + packLOT + ").\r\n";
            //                msg += (orderNumber != "") ? "Under order number: " + orderNumber + ", dated " + outDate + "\r\n" : "";
            //                msg += "Please try with another pack code again.\r\n";

            //                msg += "\r\n";
            //                msg += "If wrong, please contact with your Supervisor about this.";
            //                MessageBox.Show(msg, cls.appName());
            //                txtNG_Packcode.Text = "";
            //                txtNG_Packcode.Focus();
            //            }
            //            else
            //            {
            //                msg = "PLEASE CHECK PACKING INFORMATION BELOW\r\n";
            //                msg += "\r\n";

            //                msg += "Packing code: " + packcode + "\r\n";
            //                msg += "Location: " + location + "\r\n";
            //                msg += "Sub-locate: " + sublocate + "\r\n";
            //                msg += "Packing date: " + packdate + "\r\n";
            //                msg += "LOT number: " + packLOT + "\r\n";
            //                msg += "Packing Qty: " + packQty + "\r\n";
            //                msg += "Part name: " + partname + "\r\n";
            //                msg += "Part code: " + partcode + "\r\n";
            //                msg += "Pack Std: " + packStd + "\r\n";

            //                msg += "\r\n";
            //                msg += "If this packing is right infor, \r\nplease click YES button if not click NO button.";


            //                DialogResult dialogResultAdd = MessageBox.Show(msg, cls.appName(), MessageBoxButtons.YesNo);
            //                if (dialogResultAdd == DialogResult.Yes)
            //                {
            //                    _packId = packId;
            //                    _packcode = packcode;
            //                    _packUse = packUse;
            //                    _location = location;
            //                    _sublocate = sublocate;
            //                    _packdate = packdate;
            //                    _packLOT = packLOT;
            //                    _packQty = packQty;
            //                    _partname = partname;
            //                    _partcode = partcode;
            //                    _orderNumber = orderNumber;
            //                    _outDate = outDate;
            //                    _packStd = packStd;

            //                    txtNG_Packcode.Enabled = false;
            //                    btnNG_Check.Enabled = false;
            //                    lblNG_Partname.Text = partname;
            //                    lblNG_Partcode.Text = partcode;
            //                    txtNG_Std.Text = packStd;
            //                    txtNG_Std.ReadOnly = true;
            //                    txtNG_CurrQty.Text = packQty;
            //                    txtNG_CurrQty.ReadOnly = true;
            //                    txtNG_NGQty.Text = "0";
            //                    txtNG_NGQty.Enabled = true;
            //                    txtNG_Note.Text = "";
            //                    txtNG_Note.Enabled = false;
            //                    rdbNG_Add.Checked = false;
            //                    rdbNG_Add.Enabled = false;
            //                    rdbNG_Upd.Checked = false;
            //                    rdbNG_Upd.Enabled = false;
            //                    rdbNG_Del.Checked = false;
            //                    rdbNG_Del.Enabled = false;
            //                    btnNG_Save.Enabled = false;
            //                    btnNG_Finish.Enabled = true;
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Please try again with another packing code.", cls.appName());
            //                    txtNG_Packcode.Text = "";
            //                    txtNG_Packcode.Focus();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("This packing code cannot be found on system.\r\nPlease input another to try again.", cls.appName());
            //            txtNG_Packcode.Text = "";
            //            txtNG_Packcode.Focus();
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please input valid packing code", cls.appName());
            //        txtNG_Packcode.Text = "";
            //        txtNG_Packcode.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    txtNG_Packcode.Text = "";
            //    txtNG_Packcode.Focus();
            //    //MessageBox.Show("Please input valid packing code", cls.appName());
            //    MessageBox.Show(ex.Message + "\r\n" + ex.ToString());
            //}
            //finally
            //{

            //}
        }

        private void rdbNG_Upd_CheckedChanged(object sender, EventArgs e)
        {
            //btnNG_Save.Enabled = true;
        }

        private void rdbNG_Del_CheckedChanged(object sender, EventArgs e)
        {
            //btnNG_Save.Enabled = true;
        }

        private void btnNG_Save_Click(object sender, EventArgs e)
        {
            //DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            //if (dialogResultAdd == DialogResult.Yes)
            //{
            //    if(rdbNG_Add.Checked)
            //    {
            //        fnNG_Add();
            //    }
            //    else if(rdbNG_Upd.Checked)
            //    {
            //        fnNG_Upd();
            //    }
            //    else if(rdbNG_Del.Checked)
            //    {
            //        fnNG_Del();
            //    }
            //    fnNG_Finish();
            //}
        }

        public void fnNG_Add()
        {
            //string packId = _packId;
            //string packcode = _packcode;
            //string packUse = _packUse;
            //string location = _location;
            //string sublocate = _sublocate;
            //string packdate = _packdate;
            //string packLOT = _packLOT;
            //string packQty = _packQty;
            //string partname = _partname;
            //string partcode = _partcode;
            //string qtyNG = txtNG_NGQty.Text.Trim();
            //string noteNG = txtNG_Note.Text.Trim();
            //string orderNumber = _orderNumber;
            //string outDate = _outDate;
            //string packStd = _packStd;

            //string msg = "";
            //msg += "packId: " + packId + "\r\n";
            //msg += "packcode: " + packcode + "\r\n";
            //msg += "packLOT: " + packLOT + "\r\n";
            //msg += "partname: " + partname + "\r\n";
            //msg += "partcode: " + partcode + "\r\n";
            //msg += "packQty: " + packQty + "\r\n";
            //msg += "packdate: " + packdate + "\r\n";
            //msg += "location: " + location + "\r\n";
            //msg += "sublocate: " + sublocate + "\r\n";
            //msg += "dateNG: " + DateTime.Now + "\r\n";
            //msg += "qtyNG: " + qtyNG + "\r\n";
            //msg += "noteNG: " + noteNG + "\r\n";
            //msg += "packStd: " + packStd + "\r\n";
            //MessageBox.Show(msg, cls.appName());

            //string sql = "V2o1_BASE_Warehouse_FinishGoodNG_AddItem_Addnew";
            //SqlParameter[] sParams = new SqlParameter[12]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@packId";
            //sParams[0].Value = packId;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@packCode";
            //sParams[1].Value = packcode;

            //sParams[2] = new SqlParameter();
            //sParams[2].SqlDbType = SqlDbType.VarChar;
            //sParams[2].ParameterName = "@packLOT";
            //sParams[2].Value = packLOT;

            //sParams[3] = new SqlParameter();
            //sParams[3].SqlDbType = SqlDbType.NVarChar;
            //sParams[3].ParameterName = "@partName";
            //sParams[3].Value = partname;

            //sParams[4] = new SqlParameter();
            //sParams[4].SqlDbType = SqlDbType.VarChar;
            //sParams[4].ParameterName = "@partCode";
            //sParams[4].Value = partcode;

            //sParams[5] = new SqlParameter();
            //sParams[5].SqlDbType = SqlDbType.Int;
            //sParams[5].ParameterName = "@packQuantity";
            //sParams[5].Value = packQty;

            //sParams[6] = new SqlParameter();
            //sParams[6].SqlDbType = SqlDbType.DateTime;
            //sParams[6].ParameterName = "@packInstock";
            //sParams[6].Value = packdate;

            //sParams[7] = new SqlParameter();
            //sParams[7].SqlDbType = SqlDbType.NVarChar;
            //sParams[7].ParameterName = "@packLoc";
            //sParams[7].Value = location;

            //sParams[8] = new SqlParameter();
            //sParams[8].SqlDbType = SqlDbType.NVarChar;
            //sParams[8].ParameterName = "@packSub";
            //sParams[8].Value = sublocate;

            //sParams[9] = new SqlParameter();
            //sParams[9].SqlDbType = SqlDbType.DateTime;
            //sParams[9].ParameterName = "@dateNG";
            //sParams[9].Value = DateTime.Now;

            //sParams[10] = new SqlParameter();
            //sParams[10].SqlDbType = SqlDbType.Int;
            //sParams[10].ParameterName = "@quantityNG";
            //sParams[10].Value = qtyNG;

            //sParams[11] = new SqlParameter();
            //sParams[11].SqlDbType = SqlDbType.NVarChar;
            //sParams[11].ParameterName = "@noteNG";
            //sParams[11].Value = noteNG;

            //cls.fnUpdDel(sql, sParams);
        }

        public void fnNG_Upd()
        {

        }

        public void fnNG_Del()
        {

        }

        private void btnNG_Finish_Click(object sender, EventArgs e)
        {
            //fnNG_Finish();
        }

        public void fnNG_Finish()
        {
            //_packId = "";
            //_packcode = "";
            //_packUse = "";
            //_location = "";
            //_sublocate = "";
            //_packdate = "";
            //_packLOT = "";
            //_packQty = "";
            //_partname = "";
            //_partcode = "";
            //_orderNumber = "";
            //_outDate = "";

            //dtpNG_Fr.Value = DateTime.Now;
            //dtpNG_To.Value = DateTime.Now;

            //fnBindFGNG();

            //txtNG_Packcode.Text = "";
            //txtNG_Packcode.Enabled = true;
            //txtNG_Packcode.Focus();
            //btnNG_Check.Enabled = false;
            //lblNG_Partname.Text = "Partname: N/A";
            //lblNG_Partcode.Text = "Partcode: N/A";
            //txtNG_Std.Text = "";
            //txtNG_Std.Enabled = false;
            //txtNG_CurrQty.Text = "";
            //txtNG_CurrQty.Enabled = false;
            //txtNG_NGQty.Text = "";
            //txtNG_NGQty.Enabled = false;
            //txtNG_Note.Text = "";
            //txtNG_Note.Enabled = false;
            //rdbNG_Add.Checked = false;
            //rdbNG_Add.Enabled = false;
            //rdbNG_Upd.Checked = false;
            //rdbNG_Upd.Enabled = false;
            //rdbNG_Del.Checked = false;
            //rdbNG_Del.Enabled = false;
            //btnNG_Save.Enabled = false;

        }

        private void txtNG_NGQty_TextChanged(object sender, EventArgs e)
        {
            //string cuQty = txtNG_CurrQty.Text.Trim();
            //string ngQty = txtNG_NGQty.Text.Trim();
            
            //if(cls.IsNumeric(ngQty))
            //{
            //    int _cuQty = (cuQty != "" && cuQty != null) ? Convert.ToInt32(cuQty) : 0;
            //    int _ngQty = (ngQty != "" && ngQty != null) ? Convert.ToInt32(ngQty) : 0;
            //    if (_ngQty < 0)
            //    {
            //        _ngQty = 0;
            //    }
            //    if (_ngQty > _cuQty)
            //    {
            //        _ngQty = _cuQty;
            //    }
            //    txtNG_NGQty.Text = _ngQty.ToString();
            //}
            //else
            //{
            //    txtNG_NGQty.Text = "0";
            //}

            //if (txtNG_NGQty.Text != "0")
            //{
            //    txtNG_Note.Enabled = true;
            //}
            //else
            //{
            //    txtNG_Note.Text = "";
            //    txtNG_Note.Enabled = false;
            //    rdbNG_Add.Enabled = false;
            //    rdbNG_Add.Checked = false;
            //    rdbNG_Upd.Enabled = false;
            //    rdbNG_Upd.Checked = false;
            //    rdbNG_Del.Enabled = false;
            //    rdbNG_Del.Checked = false;
            //    btnNG_Save.Enabled = false;
            //    btnNG_Finish.Enabled = true;
            //}
        }

        private void txtNG_Note_TextChanged(object sender, EventArgs e)
        {
            //string note = txtNG_Note.Text.Trim();
            //if (note != "" && note != null)
            //{
            //    rdbNG_Add.Enabled = true;
            //    rdbNG_Add.Checked = true;
            //    rdbNG_Upd.Enabled = false;
            //    rdbNG_Upd.Checked = false;
            //    rdbNG_Del.Enabled = false;
            //    rdbNG_Del.Checked = false;
            //    btnNG_Save.Enabled = true;
            //}
            //else
            //{
            //    rdbNG_Add.Enabled = false;
            //    rdbNG_Add.Checked = false;
            //    rdbNG_Upd.Enabled = false;
            //    rdbNG_Upd.Checked = false;
            //    rdbNG_Del.Enabled = false;
            //    rdbNG_Del.Checked = false;
            //    btnNG_Save.Enabled = false;
            //}
            //btnNG_Finish.Enabled = true;
        }
    }
}
