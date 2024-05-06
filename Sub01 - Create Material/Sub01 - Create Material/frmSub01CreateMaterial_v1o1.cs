using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmSub01CreateMaterial_v1o1 : Form
    {
        public DateTime _dt;

        public string _matIDx, _matImage, _matName, _matCode, _matCate;
        public string _matUnit, _mat1Day, _matSafety, _matStocked;
        public string _matVendor, _matLoc, _matStatus, _matRemark;
        public bool _matNewImage;
        public MemoryStream _ms;

        public int _dgvVendorWidth;
        public int _rowVendorListIndex = 0;
        public string _cateIDx;
        public string _vendorIDx;
        public string _msgText;
        public int _msgType;

        public int _dgvMaterial_All_Width;
        public int _dgvMaterial_Shortage_Width;
        public int _dgvMaterial_OutStock_Width;
        public int _dgvMaterial_LastUpdate_Width;

        public string _prodIDx = "", _prodName = "", _prodCode = "", _prodQty = "", _prodUnit = "";
        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;
        DataView view;

        public int _dgvHandOver_List_Width;
        public int _dgvHandOver_FullList_Width;
        public int _rowHandOver_Index = 0;
        public string _hoIDx = "", _hoCode = "", _hoDate = "", _finish = "";

        public string _letter = "";

        Timer timer = new Timer();


        public frmSub01CreateMaterial_v1o1()
        {
            InitializeComponent();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IDx";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Code";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Qty";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Unit";
            table.Columns.Add(column);

            cls.SetDoubleBuffer(dgvMaterial_All, true);
            cls.SetDoubleBuffer(dgvMaterial_Shortage, true);
            cls.SetDoubleBuffer(dgvMaterial_OutStock, true);
            cls.SetDoubleBuffer(dgvScanOut_Spare_List, true);
            cls.SetDoubleBuffer(dgvScanIn_Spare_List, true);

            cls.SetDoubleBuffer(dgvHandOver_FullList, true);
            cls.SetDoubleBuffer(trvCate_List, true);
            cls.SetDoubleBuffer(dgvVendor_List, true);
        }

        private void frmSub01CreateMaterial_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgvVendorWidth = cls.fnGetDataGridWidth(dgvVendor_List);
            _dgvMaterial_All_Width = cls.fnGetDataGridWidth(dgvMaterial_All);
            _dgvMaterial_Shortage_Width = cls.fnGetDataGridWidth(dgvMaterial_Shortage);
            _dgvMaterial_OutStock_Width = cls.fnGetDataGridWidth(dgvMaterial_OutStock);

            _dgvHandOver_List_Width = cls.fnGetDataGridWidth(dgvHandOver_List);
            _dgvHandOver_FullList_Width = cls.fnGetDataGridWidth(dgvHandOver_FullList);
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

            tabControl1.SelectedIndex = 1;
            init_Material();
            fnBindData_All();

            //fnBindData_All();

            //init_HandOver();
            //init_Material();
            //init_Category();
            //init_Vendor();
        }

        public void fnGetdate()
        {
            try
            {
                if(check.IsConnectedToInternet())
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
            int idx = tabControl1.SelectedIndex;
            switch (idx)
            {
                case 0:
                    init_HandOver();
                    break;
                case 1:
                    init_Material();
                    break;
                case 2:
                    init_Category();
                    break;
                case 3:
                    init_Vendor();
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab2 = tabControl2.SelectedIndex;
            switch (tab2)
            {
                case 0:
                    fnBindData_All();
                    break;
                case 1:
                    fnBindData_Shortage();
                    break;
                case 2:
                    fnBindData_OutStock();
                    break;
                case 3:
                    initScanOut_Spare_Status();
                    break;
                case 4:
                    initScanIn_Spare_Status();
                    break;
            }
        }

        public void onlynumwithsinglepoint(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'))
            { e.Handled = true; }
            TextBox txtDecimal = sender as TextBox;
            if (e.KeyChar == '.' && txtDecimal.Text.Contains("."))
            {
                e.Handled = true;
            }
        }


        #region HAND-OVER MATERIAL

        public void init_HandOver()
        {
            init_HandOver_List();
            init_HandOver_Name();

            lblHandOver_Code.Text = "N/A";
            txtHandOver_Qty.Text = "0";
            txtHandOver_Qty.Enabled = false;

            btnHandOver_Save.Enabled = false;
            btnHandOver_Finish.Enabled = false;
            //try
            //{

            //}
            //catch
            //{
                
            //}
            //finally
            //{

            //}
        }

        public void init_HandOver_List()
        {
            string sql = "V2o1_BASE_SubMaterial_Init_HandOver_List_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvHandOver_FullList_Width = cls.fnGetDataGridWidth(dgvHandOver_FullList);
            dgvHandOver_FullList.DataSource = dt;

            //dgvHandOver_FullList.Columns[0].Width = 20 * _dgvHandOver_FullList_Width / 100;    // idx
            dgvHandOver_FullList.Columns[1].Width = 45 * _dgvHandOver_FullList_Width / 100;    // hoCode
            dgvHandOver_FullList.Columns[2].Width = 40 * _dgvHandOver_FullList_Width / 100;    // hoDate
            dgvHandOver_FullList.Columns[3].Width = 15 * _dgvHandOver_FullList_Width / 100;    // matQty
            //dgvHandOver_FullList.Columns[4].Width = 20 * _dgvHandOver_FullList_Width / 100;    // hoNew
            //dgvHandOver_FullList.Columns[5].Width = 20 * _dgvHandOver_FullList_Width / 100;    // hoFinish
            //dgvHandOver_FullList.Columns[6].Width = 20 * _dgvHandOver_FullList_Width / 100;    // hoNote

            dgvHandOver_FullList.Columns[0].Visible = false;
            dgvHandOver_FullList.Columns[1].Visible = true;
            dgvHandOver_FullList.Columns[2].Visible = true;
            dgvHandOver_FullList.Columns[3].Visible = true;
            dgvHandOver_FullList.Columns[4].Visible = false;
            dgvHandOver_FullList.Columns[5].Visible = false;
            dgvHandOver_FullList.Columns[6].Visible = false;

            dgvHandOver_FullList.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridviewWhite(dgvHandOver_FullList, 11, 30);
        }

        public void init_HandOver_Name()
        {
            string sql = "V2o1_BASE_SubMaterial_Init_HandOver_Name_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbHandOver_Name.DataSource = dt;
            cbbHandOver_Name.DisplayMember = "Name";
            cbbHandOver_Name.ValueMember = "ProdId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbHandOver_Name.SelectedIndex = 0;
        }

        private void cbbHandOver_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string prodIDx = "", prodName = "", prodCode = "", prodUnit = "";
            if (cbbHandOver_Name.SelectedIndex > 0)
            {
                prodIDx = cbbHandOver_Name.SelectedValue.ToString();
                prodName = cbbHandOver_Name.Text;
                string sql = "V2o1_BASE_SubMaterial_Init_HandOver_List_Code_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodId";
                sParams[0].Value = prodIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    prodCode = ds.Tables[0].Rows[0][0].ToString();
                    prodUnit = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    prodCode = "N/A";
                    prodUnit = "";
                }

                txtHandOver_Qty.Enabled = true;
                txtHandOver_Qty.Focus();
                lnkHandOver_Add.Enabled = true;
                btnHandOver_Finish.Enabled = true;
            }
            else
            {
                prodIDx = "0";
                prodName = "";
                prodCode = "N/A";
                prodUnit = "";

                txtHandOver_Qty.Text = "0";
                txtHandOver_Qty.Enabled = false;
                lnkHandOver_Add.Enabled = false;
                btnHandOver_Finish.Enabled = false;
            }

            _prodIDx = prodIDx;
            _prodName = prodName;
            _prodCode = prodCode;
            _prodUnit = prodUnit;

            lblHandOver_Code.Text = prodCode;
            txtHandOver_Qty.Text = "0";

            dgvHandOver_List.ClearSelection();
            lnkHandOver_Del.Enabled = false;
        }

        private void txtHandOver_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtHandOver_Qty_TextChanged(object sender, EventArgs e)
        {
            if (txtHandOver_Qty.Text.Length == 0)
            {
                _prodQty = "0";
            }
            else
            {
                _prodQty = txtHandOver_Qty.Text;
            }
        }

        private void lnkHandOver_Add_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int prodIDx = (_prodIDx != "" && _prodIDx != null) ? Convert.ToInt32(_prodIDx) : 0;
            string prodName = _prodName;
            string prodCode = _prodCode;
            int prodQty = (_prodQty != "" && _prodQty != null) ? Convert.ToInt32(_prodQty) : 0;
            string prodUnit = _prodUnit;

            if (prodIDx > 0 && prodQty > 0)
            {
                table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
                view = new DataView(table);
                _dgvHandOver_List_Width = cls.fnGetDataGridWidth(dgvHandOver_List);

                dgvHandOver_List.DataSource = view;
                dgvHandOver_List.Refresh();


                //dgvHandOver_List.Columns[0].Width = 20 * _dgvHandOver_List_Width / 100;    // matId
                dgvHandOver_List.Columns[1].Width = 70 * _dgvHandOver_List_Width / 100;    // matName
                //dgvHandOver_List.Columns[2].Width = 20 * _dgvHandOver_List_Width / 100;    // matCode
                dgvHandOver_List.Columns[3].Width = 15 * _dgvHandOver_List_Width / 100;    // matQty
                dgvHandOver_List.Columns[4].Width = 15 * _dgvHandOver_List_Width / 100;    // matUnit

                dgvHandOver_List.Columns[0].Visible = false;
                dgvHandOver_List.Columns[1].Visible = true;
                dgvHandOver_List.Columns[2].Visible = false;
                dgvHandOver_List.Columns[3].Visible = true;
                dgvHandOver_List.Columns[4].Visible = true;

                cls.fnFormatDatagridview(dgvHandOver_List, 11, 30);
            }
            else
            {
                MessageBox.Show("Không thể thêm khi số lượng bằng 0");
            }

            cbbHandOver_Name.SelectedIndex = 0;
            lblHandOver_Code.Text = "N/A";
            txtHandOver_Qty.Text = "0";
            dgvHandOver_List.ClearSelection();
            lnkHandOver_Add.Enabled = false;
            lnkHandOver_Del.Enabled = false;
            btnHandOver_Save.Enabled = true;
            btnHandOver_Finish.Enabled = true;
        }

        private void lnkHandOver_Del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvHandOver_List.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgvHandOver_List.Rows.Remove(row);
                }

                cbbHandOver_Name.SelectedIndex = 0;
                lblHandOver_Code.Text = "N/A";
                txtHandOver_Qty.Text = "0";
                dgvHandOver_List.ClearSelection();
                lnkHandOver_Add.Enabled = false;
                lnkHandOver_Del.Enabled = false;
                btnHandOver_Save.Enabled = (dgvHandOver_List.Rows.Count > 0) ? true : false;
                btnHandOver_Finish.Enabled = (dgvHandOver_List.Rows.Count > 0) ? true : false;

                _msgText = "The item is removed successful.";
                _msgType = 0;

            }
            dgvHandOver_List.ClearSelection();
        }

        private void dgvHandOver_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvHandOver_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvHandOver_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvHandOver_List.Rows[e.RowIndex];

                string prodIDx = row.Cells[0].Value.ToString();
                string prodName = row.Cells[1].Value.ToString();
                string prodCode = row.Cells[2].Value.ToString();
                string prodQty = row.Cells[3].Value.ToString();
                string prodUnit = row.Cells[4].Value.ToString();

                lnkHandOver_Add.Enabled = false;
                lnkHandOver_Del.Enabled = true;
            }
        }

        private void dgvHandOver_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnHandOver_Save_Click(object sender, EventArgs e)
        {
            string listNote = txtHandOver_Note.Text.Trim();
            DateTime now = _dt.AddMilliseconds(-_dt.Millisecond);
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                //MessageBox.Show(dgvHandOver_List.Rows.Count.ToString());
                //string msg = "";
                foreach (DataGridViewRow row in dgvHandOver_List.Rows)
                {
                    string prodIDx = "", prodName = "", prodCode = "", prodQty = "", prodUnit = "";
                    prodIDx = row.Cells[0].Value.ToString();
                    prodName = row.Cells[1].Value.ToString();
                    prodCode = row.Cells[2].Value.ToString();
                    prodQty = row.Cells[3].Value.ToString();
                    prodUnit = row.Cells[4].Value.ToString();

                    //msg += "prodIDx: " + prodIDx + "\r\n";
                    //msg += "prodName: " + prodName + "\r\n";
                    //msg += "prodCode: " + prodCode + "\r\n";
                    //msg += "prodQty: " + prodQty + "\r\n";
                    //msg += "prodUnit: " + prodUnit + "\r\n";
                    //msg += "\r\n";

                    string sql = "V2o1_BASE_SubMaterial_Init_HandOver_Item_AddItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[7]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.DateTime;
                    sParams[0].ParameterName = "@handoverDate";
                    sParams[0].Value = now;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.NVarChar;
                    sParams[1].ParameterName = "@handoverNote";
                    sParams[1].Value = listNote;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.Int;
                    sParams[2].ParameterName = "@prodIDx";
                    sParams[2].Value = prodIDx;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.NVarChar;
                    sParams[3].ParameterName = "@prodName";
                    sParams[3].Value = prodName;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.VarChar;
                    sParams[4].ParameterName = "@prodCode";
                    sParams[4].Value = prodCode;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.Int;
                    sParams[5].ParameterName = "@prodQty";
                    sParams[5].Value = prodQty;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.VarChar;
                    sParams[6].ParameterName = "@prodUnit";
                    sParams[6].Value = prodUnit;

                    cls.fnUpdDel(sql, sParams);
                }

                //MessageBox.Show(msg);

                cbbHandOver_Name.SelectedIndex = 0;
                lblHandOver_Code.Text = "N/A";
                txtHandOver_Qty.Enabled = false;
                txtHandOver_Qty.Text = "0";
                dgvHandOver_List.DataSource = "";
                dgvHandOver_List.Refresh();
                lnkHandOver_Add.Enabled = false;
                lnkHandOver_Del.Enabled = false;
                txtHandOver_Note.Text = "";
                btnHandOver_Save.Enabled = false;
                btnHandOver_Finish.Enabled = false;

                _msgText = "Thêm danh mục bàn giao vật tư thành công.";
                _msgType = 0;

                init_HandOver_List();

                table.Rows.Clear();
            }
        }

        private void btnHandOver_Finish_Click(object sender, EventArgs e)
        {

        }

        private void dgvHandOver_FullList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvHandOver_FullList.ClearSelection();
            string hoIDx = "", hoCode = "", hoDate = "", hoQty = "", hoNew = "", hoFinish = "";
            bool _new = true, _finish = true;
            foreach (DataGridViewRow row in dgvHandOver_FullList.Rows)
            {
                hoIDx = row.Cells[0].Value.ToString();
                hoCode = row.Cells[1].Value.ToString();
                hoDate = row.Cells[2].Value.ToString();
                hoQty = row.Cells[3].Value.ToString();
                hoNew = row.Cells[4].Value.ToString();
                hoFinish = row.Cells[5].Value.ToString();

                _new = (hoNew.ToLower() == "true") ? true : false;
                _finish = (hoFinish.ToLower() == "true") ? true : false;

                if (_new == true)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = (_finish == true) ? Color.LightGreen : Color.White;
                }
            }
        }

        private void dgvHandOver_FullList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvHandOver_FullList, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvHandOver_FullList.Rows[e.RowIndex];

                string hoIDx = row.Cells[0].Value.ToString();
                string hoCode = row.Cells[1].Value.ToString();
                string hoDate = row.Cells[2].Value.ToString();
                string finish = row.Cells[5].Value.ToString();

                _hoIDx = hoIDx;
                _hoCode = hoCode;
                _hoDate = hoDate;
                _finish = finish;
            }
        }

        private void dgvHandOver_FullList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvHandOver_FullList.Rows[e.RowIndex].Selected = true;
                _rowHandOver_Index = e.RowIndex;
                dgvHandOver_FullList.CurrentCell = dgvHandOver_FullList.Rows[e.RowIndex].Cells[2];
                cmsHandOver_Menu.Show(this.dgvHandOver_FullList, e.Location);
                cmsHandOver_Menu.Show(Cursor.Position);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            init_HandOver_List();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string hoIDx = "", hoCode = "", hoDate = "", finish = "";
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                hoIDx = _hoIDx;
                hoCode = _hoCode;
                hoDate = _hoDate;
                finish = _finish;
                bool _finished = (finish.ToLower() == "true") ? true : false;
                if (_finished == false)
                {
                    string sql = "V2o1_BASE_SubMaterial_Init_HandOver_List_DelItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@hoIDx";
                    sParams[0].Value = hoIDx;

                    cls.fnUpdDel(sql, sParams);

                    _hoIDx = "";
                    _hoCode = "";
                    _hoDate = "";

                    init_HandOver_List();

                    _msgText = "Xóa thành công";
                    _msgType = 1;
                }
                else
                {
                    _msgText = "Mã này đã bàn giao xong nên không thể xóa";
                    _msgType = 2;
                }
                cls.fnMessage(tssMsg, _msgText, _msgType);
            }
        }

        private void printThisHDOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSub01CreateMaterial_HandOverPrint frmPrint_HandOver = new frmSub01CreateMaterial_HandOverPrint(_hoIDx.ToString());
            frmPrint_HandOver.ShowDialog();
        }


        #endregion


        #region MATERIAL FUNCTION

        public void init_Material()
        {
            try
            {
                _matNewImage = false;
                init_Material_List();
                init_Material_Code();
                init_Material_Cate();
                init_Material_Loc();
                init_Material_Unit();
                init_Material_Vendor();

                txtMat_Name.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtMat_Name.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtMat_Name.AutoCompleteCustomSource = LoadMaterialNames();

                fnMat_Finish();

                lnkMat_Change.Enabled = false;
                lnkMat_View.Enabled = false;
                lnkMat_Browse.Enabled = true;
                lnkMat_Clear.Enabled = false;

                txtMat_Name.Focus();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void init_Material_List()
        {
            try
            {
                //string sql = "V2o1_BASE_SubMaterial_Init_Material_List_SelItem_Addnew";
                //DataTable dt = new DataTable();
                //dt = cls.fnSelect(sql);
                //cbbMat_List.DataSource = dt;
                //cbbMat_List.DisplayMember = "Name";
                //cbbMat_List.ValueMember = "ProdId";
                //dt.Rows.InsertAt(dt.NewRow(), 0);
                //cbbMat_List.SelectedIndex = 0;
            }
            catch
            {
                
            }
            finally
            {

            }
        }

        public void init_Material_Code()
        {
            try
            {
                string orderNo = "";
                string currOrderNo = "", nextOrderNo = "";
                int _currOrderNo = 0, _nextOrderNo = 0;
                string sql = "V2o1_BASE_SubMaterial_Init_Material_Code_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currOrderNo = ds.Tables[0].Rows[0][0].ToString();
                        string orderCode = cls.Right(currOrderNo, 4);
                        _currOrderNo = (orderCode != "" && orderCode != null) ? Convert.ToInt32(orderCode) : 0;
                        _nextOrderNo = _currOrderNo + 1;
                        orderNo = "SBM-" + String.Format("{0:yyyyMMdd}", _dt) + "" + String.Format("{0:0000}", _nextOrderNo);
                    }
                    else
                    {
                        currOrderNo = "0";
                        nextOrderNo = "SBM-" + String.Format("{0:yyyyMMdd}", _dt) + "0001";
                        orderNo = nextOrderNo;
                    }
                }
                else
                {
                    currOrderNo = "0";
                    nextOrderNo = "SBM-" + String.Format("{0:yyyyMMdd}", _dt) + "0001";
                    orderNo = nextOrderNo;
                }
                txtMat_Code.Text = orderNo;
            }
            //catch (SqlException sqlEx)
            //{
            //    MessageBox.Show(sqlEx.ToString());
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void init_Material_Cate()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_Material_Cate_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                cbbMat_Cate.DataSource = dt;
                cbbMat_Cate.DisplayMember = "Parent_ID";
                cbbMat_Cate.ValueMember = "Tree_ID";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbbMat_Cate.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void init_Material_Loc()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_Material_Loc_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                cbbMat_Loc.DataSource = dt;
                cbbMat_Loc.DisplayMember = "Name";
                cbbMat_Loc.ValueMember = "LocationID";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbbMat_Loc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void init_Material_Unit()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_Material_Unit_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                cbbMat_Unit.DataSource = dt;
                cbbMat_Unit.DisplayMember = "uom";
                cbbMat_Unit.ValueMember = "uom";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbbMat_Unit.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void init_Material_Vendor()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_Material_Vendor_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                cbbMat_Vendor.DataSource = dt;
                cbbMat_Vendor.DisplayMember = "Name";
                cbbMat_Vendor.ValueMember = "VendorID";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbbMat_Vendor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void init_Material_AddChoice()
        {
            string mName = txtMat_Name.Text.Trim();
            int mList = cbbMat_List.SelectedIndex;
            int mCate = cbbMat_Cate.SelectedIndex;
            //int mLoca = cbbMat_Loc.SelectedIndex;
            int mLoca = 112;
            string m1Day = txtMat_1DayUsing.Text.Trim();
            string mSafety = txtMat_ReOrder.Text.Trim();
            int mUnit = cbbMat_Unit.SelectedIndex;
            int mVendor = cbbMat_Vendor.SelectedIndex;

            if (((mList > 0 || cbbMat_List.Text != "") || (mName != "" && mName != null))
                && mCate > 0
                && mLoca > 0
                && (m1Day != "" && m1Day != "0" && m1Day != null)
                && (mSafety != "" && mSafety != "0" && mSafety != null)
                && mUnit > 0
                && mVendor > 0)
            {
                rdbMat_Add.Enabled = true;
            }
            else
            {
                rdbMat_Add.Checked = false;
                rdbMat_Add.Enabled = false;
            }
        }

        public AutoCompleteStringCollection LoadMaterialNames()
        {
            AutoCompleteStringCollection TheNameList = new AutoCompleteStringCollection();
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_Material_List_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        TheNameList.Add(row[1].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
            return TheNameList;
        }

        public DataTable dtSource(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (sql != "")
                {
                    dt = cls.fnSelect(sql);
                }
            }
            catch
            {

            }
            finally
            {

            }
            return dt;
        }

        private void cbbMat_List_TextChanged(object sender, EventArgs e)
        {
            init_Material_AddChoice();
            //cbbMat_List.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cbbMat_List.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cbbMat_List_SelectionChangeCommitted(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void cbbMat_Cate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void cbbMat_Loc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void txtMat_1DayUsing_TextChanged(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void txtMat_ReOrder_TextChanged(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void txtMat_Price_TextChanged(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void cbbMat_Unit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void cbbMat_Vendor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            init_Material_AddChoice();
        }

        private void lnkMat_CateRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            init_Material_Cate();
        }

        private void lnkMat_VendorRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            init_Material_Vendor();
        }

        private void lnkMat_View_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmSub01CreateMaterial_ViewImage frmViewImage = new frmSub01CreateMaterial_ViewImage(_ms);
            frmViewImage.ShowDialog();
        }

        private void lnkMat_Browse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //dialog.InitialDirectory = @"C:\";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.Title = "Chọn ảnh minh họa cho vật tư";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fleName = Path.GetFullPath(dialog.FileName);
                Image original = Image.FromFile(dialog.FileName);
                int BOXHEIGHT = 640;
                int BOXWIDTH = 640;
                float scaleHeight = (float)BOXHEIGHT / (float)original.Height;
                float scaleWidth = (float)BOXWIDTH / (float)original.Width;
                float scale = Math.Min(scaleHeight, scaleWidth);

                Bitmap resized = new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));

                //string resizedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Test\\";
                string resizedPath = cls.CreateFolderMissing(Application.StartupPath + "\\Temp\\");

                //string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                string resizedName = cls.RemoveSpecialCharacters(Path.GetFileName(fleName.Replace(Path.GetExtension(fleName), null))) + "" + Path.GetExtension(fleName);

                string fullPath = resizedPath + "" + resizedName;
                _matImage = fullPath;

                panel1.BackgroundImage = Image.FromFile(dialog.FileName);
                panel1.BackgroundImageLayout = ((int)(original.Width) > (int)(panel1.Width) || (int)(original.Height) > (int)(panel1.Height)) ? ImageLayout.Stretch : ImageLayout.Center;

                resized.Save(fullPath);

                lnkMat_Clear.Enabled = true;
                _matNewImage = true;

                //OpenFileDialog dialog = new OpenFileDialog();
                //dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                ////dialog.InitialDirectory = @"C:\";
                //dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                //dialog.Title = "Chọn ảnh minh họa cho vật tư";


                //if (dialog.ShowDialog() == DialogResult.OK)
                //{
                //    //Encrypt the selected file. I'll do this later. :)
                //    //fileStream = dialog.OpenFile();
                //    //MessageBox.Show(dialog.FileName);

                //    string fleName = Path.GetFullPath(dialog.FileName);
                //    Image original = Image.FromFile(dialog.FileName);
                //    int BOXHEIGHT = picMat_Image.Height;
                //    int BOXWIDTH = picMat_Image.Width;
                //    float scaleHeight = (float)BOXHEIGHT / (float)original.Height;
                //    float scaleWidth = (float)BOXWIDTH / (float)original.Width;
                //    float scale = Math.Min(scaleHeight, scaleWidth);

                //    Bitmap resizedImage = new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));

                //    picMat_Image.SizeMode = PictureBoxSizeMode.CenterImage;
                //    picMat_Image.Image = resizedImage;
                //    picMat_Image.Tag = original; 

                //    Image resized = ResizedImage(298, 204, fleName);
                //    string resizedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Test\\";
                //    string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                //    resized.Save(resizedPath + resizedName);
                //}
            }
            else
            {
                _matNewImage = false;
            }
        }

        private void lnkMat_Clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (_matIDx != "" && _matIDx != null)
                {
                    try
                    {
                        string matIDx = _matIDx;
                        string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_Image_DelItem_Addnew";

                        SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@matIDx";
                        sParams[0].Value = matIDx;

                        cls.fnUpdDel(sql, sParams);

                        _msgText = "Xóa ảnh thành công.";
                        _msgType = 1;
                    }
                    catch (SqlException sqlEx)
                    {
                        _msgText = "Có lỗi dữ liệu phát sinh.";
                        _msgType = 3;
                        MessageBox.Show(sqlEx.ToString());
                    }
                    catch(Exception ex)
                    {
                        _msgText = "Có lỗi phát sinh.";
                        _msgType = 2;
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        cls.fnMessage(tssMsg, _msgText, _msgType);
                        _ms = null;

                        panel1.BackgroundImage = null;
                        //if (picMat_Image.Image != null)
                        //{
                        //    picMat_Image.Image.Dispose();
                        //    picMat_Image.Image = null;
                        //}
                        lnkMat_Clear.Enabled = false;
                        fnBindData_All();
                    }
                }

            }
        }

        private void lnkMat_Change_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                //dialog.InitialDirectory = @"C:\";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                dialog.Title = "Chọn ảnh minh họa cho vật tư";


                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DialogResult confirm = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        string fleName = Path.GetFullPath(dialog.FileName);
                        Image original = Image.FromFile(dialog.FileName);
                        int BOXHEIGHT = 640;
                        int BOXWIDTH = 640;
                        float scaleHeight = (float)BOXHEIGHT / (float)original.Height;
                        float scaleWidth = (float)BOXWIDTH / (float)original.Width;
                        float scale = Math.Min(scaleHeight, scaleWidth);

                        Bitmap resized = new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));

                        //string resizedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Test\\";
                        string resizedPath = cls.CreateFolderMissing(Application.StartupPath + "\\Temp\\");

                        //string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                        string resizedName = cls.RemoveSpecialCharacters(Path.GetFileName(fleName.Replace(Path.GetExtension(fleName), null))) + "" + Path.GetExtension(fleName);

                        string fullPath = resizedPath + "" + resizedName;

                        panel1.BackgroundImage = Image.FromFile(dialog.FileName);
                        panel1.BackgroundImageLayout = ((int)(original.Width) > (int)(panel1.Width) || (int)(original.Height) > (int)(panel1.Height)) ? ImageLayout.Stretch : ImageLayout.Center;

                        resized.Save(fullPath);

                        string matIDx = _matIDx;
                        string imgPath = fullPath;
                        Bitmap bmp = new Bitmap(imgPath);
                        FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
                        byte[] bimage = new byte[fs.Length];
                        fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                        fs.Close();

                        string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_Image_Only_UpdItem_Addnew";
                        SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@matIDx";
                        sParams[0].Value = matIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Image;
                        sParams[1].ParameterName = "@matImage";
                        sParams[1].Value = bimage;

                        cls.fnUpdDel(sql, sParams);

                        _msgText = "Thay đổi ảnh vật tư thành công.";
                        _msgType = 1;

                        lnkMat_Browse.Enabled = false;
                        lnkMat_View.Enabled = true;
                        lnkMat_Clear.Enabled = true;
                    }
                }
                _matNewImage = false;
            }
            catch (SqlException sqlEx)
            {
                _msgText = "Có lỗi dữ liệu phát sinh.";
                _msgType = 3;
                MessageBox.Show(sqlEx.ToString());
            }
            catch(Exception ex)
            {
                _msgText = "Có lỗi phát sinh.";
                _msgType = 2;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cls.fnMessage(tssMsg, _msgText, _msgType);
                fnBindData_All();
            }
        }

        private void rdbMat_ADD_CheckedChanged(object sender, EventArgs e)
        {
            btnMat_Save.Enabled = true;
        }

        private void rdbMat_UPD_CheckedChanged(object sender, EventArgs e)
        {
            btnMat_Save.Enabled = true;
        }

        private void rdbMat_DEL_CheckedChanged(object sender, EventArgs e)
        {
            btnMat_Save.Enabled = true;
        }

        private void txtMat_1DayUsing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMat_ReOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMat_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            cls.onlynumwithsinglepoint(sender, e);
        }

        public void fnMat_Finish()
        {
            _matIDx = "";
            _matImage = "";
            _matName = "";
            _matCode = "";
            _matCate = "";
            _matUnit = "";
            _mat1Day = "";
            _matSafety = "";
            _matStocked = "";
            _matVendor = "";
            _matLoc = "";
            _matStatus = "";
            _matRemark = "";
            _matNewImage = false;

            if (panel1.BackgroundImage != null)
            {
                //picMat_Image.Image.Dispose();
                //picMat_Image.Image = null;
                panel1.BackgroundImage = null;
                lnkMat_Clear.Enabled = false;
            }
            lnkMat_Change.Enabled = false;
            lnkMat_View.Enabled = false;
            lnkMat_Browse.Enabled = true;
            lnkMat_Clear.Enabled = false;

            //cbbMat_List.SelectedIndex = 0;
            cbbMat_List.Text = "";
            txtMat_Name.Text = "";
            txtMat_Code.Text = "";
            init_Material_Code();
            cbbMat_Cate.SelectedIndex = 0;
            cbbMat_Loc.SelectedIndex = 0;
            chkMat_Repair.Checked = false;
            chkMat_Maintain.Checked = false;
            chkMat_Improve.Checked = false;
            chkMat_Daily.Checked = false;
            txtMat_Lifetime.Text = "0";
            txtMat_1DayUsing.Text = "0";
            txtMat_ReOrder.Text = "0";
            txtMat_Price.Text = "0";
            cbbMat_Unit.SelectedIndex = 0;
            cbbMat_Vendor.SelectedIndex = 0;
            rdbMat_Enabled.Checked = true;
            rdbMat_Disabled.Checked = false;
            txtMat_Note.Text = "";

            dgvMaterial_All.ClearSelection();

            rdbMat_Add.Checked = false;
            rdbMat_Upd.Checked = false;
            rdbMat_Del.Checked = false;

            rdbMat_Add.Enabled = false;
            rdbMat_Upd.Enabled = false;
            rdbMat_Del.Enabled = false;
            btnMat_Save.Enabled = false;
            btnMat_Finish.Enabled = false;
        }

        public void fnMat_Add()
        {
            string matName = txtMat_Name.Text.Trim();
            string sql = "V2o1_BASE_SubMaterial_Init_Material_DuplicateName_ChkItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.NVarChar;
            sParams[0].ParameterName = "@matName";
            sParams[0].Value = matName;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                _msgText = "Trùng tên (" + matName + ") với vật tư đã có. Vui lòng chọn tên khác.";
                _msgType = 2;

                cls.fnMessage(tssMsg, _msgText, _msgType);
                tssMsg.ForeColor = Color.Red;
            }
            else
            {
                if (_matNewImage == false)
                {
                    fnMat_Add_NoImage();
                }
                else
                {
                    fnMat_Add_Image();
                    _matNewImage = false;
                }
            }
        }

        public void fnMat_Add_NoImage()
        {
            string matName = txtMat_Name.Text.Trim();
            string matCode = txtMat_Code.Text.Trim();
            int matCateIDx = Convert.ToInt32(cbbMat_Cate.SelectedValue.ToString());
            string matCateName = cbbMat_Cate.Text;
            //int matLocIDx = Convert.ToInt32(cbbMat_Loc.SelectedValue.ToString());
            int matLocIDx = 112;
            //string matLocName = cbbMat_Loc.Text;
            string matLocName = "MMT WH5";
            string mat1Day = txtMat_1DayUsing.Text.Trim();
            decimal matSafety = Convert.ToDecimal(txtMat_ReOrder.Text.Trim());
            string matUnit = cbbMat_Unit.Text;
            int matVendorIDx = Convert.ToInt32(cbbMat_Vendor.SelectedValue.ToString());
            string matVendorName = cbbMat_Vendor.Text;
            string matStatus = (rdbMat_Enabled.Checked) ? "True" : "False";
            string matNote = txtMat_Note.Text.Trim();
            string matLifetime = txtMat_Lifetime.Text.Trim();
            bool matTypeRepair = (chkMat_Repair.Checked) ? true : false;
            bool matTypeMaintain = (chkMat_Maintain.Checked) ? true : false;
            bool matTypeImprove = (chkMat_Improve.Checked) ? true : false;
            bool matTypeDaily = (chkMat_Daily.Checked) ? true : false;
            string price = txtMat_Price.Text.Trim();

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_AddItem_V2o1_Addnew";
            SqlParameter[] sParams = new SqlParameter[19]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.NVarChar;
            sParams[0].ParameterName = "@matName";
            sParams[0].Value = matName;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@matCode";
            sParams[1].Value = matCode;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "@cateIDx";
            sParams[2].Value = matCateIDx;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "@cateName";
            sParams[3].Value = matCateName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.Int;
            sParams[4].ParameterName = "@locaIDx";
            sParams[4].Value = matLocIDx;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.NVarChar;
            sParams[5].ParameterName = "@locaName";
            sParams[5].Value = matLocName;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "@mat1Day";
            sParams[6].Value = mat1Day;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Decimal;
            sParams[7].ParameterName = "@matSafety";
            sParams[7].Value = matSafety;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.VarChar;
            sParams[8].ParameterName = "@matUnit";
            sParams[8].Value = matUnit;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.Int;
            sParams[9].ParameterName = "@vendIDx";
            sParams[9].Value = matVendorIDx;

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.NVarChar;
            sParams[10].ParameterName = "@vendName";
            sParams[10].Value = matVendorName;

            sParams[11] = new SqlParameter();
            sParams[11].SqlDbType = SqlDbType.Bit;
            sParams[11].ParameterName = "@matStatus";
            sParams[11].Value = matStatus;

            sParams[12] = new SqlParameter();
            sParams[12].SqlDbType = SqlDbType.NVarChar;
            sParams[12].ParameterName = "@matNote";
            sParams[12].Value = matNote;

            sParams[13] = new SqlParameter();
            sParams[13].SqlDbType = SqlDbType.Int;
            sParams[13].ParameterName = "@matLifetime";
            sParams[13].Value = matLifetime;

            sParams[14] = new SqlParameter();
            sParams[14].SqlDbType = SqlDbType.Bit;
            sParams[14].ParameterName = "@matTypeRepair";
            sParams[14].Value = matTypeRepair;

            sParams[15] = new SqlParameter();
            sParams[15].SqlDbType = SqlDbType.Bit;
            sParams[15].ParameterName = "@matTypeMaintain";
            sParams[15].Value = matTypeMaintain;

            sParams[16] = new SqlParameter();
            sParams[16].SqlDbType = SqlDbType.Bit;
            sParams[16].ParameterName = "@matTypeImprove";
            sParams[16].Value = matTypeImprove;

            sParams[17] = new SqlParameter();
            sParams[17].SqlDbType = SqlDbType.Bit;
            sParams[17].ParameterName = "@matTypeDaily";
            sParams[17].Value = matTypeDaily;

            sParams[18] = new SqlParameter();
            sParams[18].SqlDbType = SqlDbType.Money;
            sParams[18].ParameterName = "@price";
            sParams[18].Value = price;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Thêm mới vật tư thành công.";
            _msgType = 0;
        }

        public void fnMat_Add_Image()
        {
            string matName = txtMat_Name.Text.Trim();
            string matCode = txtMat_Code.Text.Trim();
            int matCateIDx = Convert.ToInt32(cbbMat_Cate.SelectedValue.ToString());
            string matCateName = cbbMat_Cate.Text;
            //int matLocIDx = Convert.ToInt32(cbbMat_Loc.SelectedValue.ToString());
            int matLocIDx = 112;
            //string matLocName = cbbMat_Loc.Text;
            string matLocName = "MMT WH5";
            string mat1Day = txtMat_1DayUsing.Text.Trim();
            decimal matSafety = Convert.ToDecimal(txtMat_ReOrder.Text.Trim());
            string matUnit = cbbMat_Unit.Text;
            int matVendorIDx = Convert.ToInt32(cbbMat_Vendor.SelectedValue.ToString());
            string matVendorName = cbbMat_Vendor.Text;
            string matStatus = (rdbMat_Enabled.Checked) ? "True" : "False";
            string matNote = txtMat_Note.Text.Trim();
            string matLifetime = txtMat_Lifetime.Text.Trim();
            bool matTypeRepair = (chkMat_Repair.Checked) ? true : false;
            bool matTypeMaintain = (chkMat_Maintain.Checked) ? true : false;
            bool matTypeImprove = (chkMat_Improve.Checked) ? true : false;
            bool matTypeDaily = (chkMat_Daily.Checked) ? true : false;
            string price = txtMat_Price.Text.Trim();

            string imgPath = _matImage;
            Bitmap bmp = new Bitmap(imgPath);
            FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            byte[] bimage = new byte[fs.Length];
            fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_Image_AddItem_V2o1_Addnew";
            SqlParameter[] sParams = new SqlParameter[20]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.NVarChar;
            sParams[0].ParameterName = "@matName";
            sParams[0].Value = matName;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@matCode";
            sParams[1].Value = matCode;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "@cateIDx";
            sParams[2].Value = matCateIDx;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "@cateName";
            sParams[3].Value = matCateName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.Int;
            sParams[4].ParameterName = "@locaIDx";
            sParams[4].Value = matLocIDx;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.NVarChar;
            sParams[5].ParameterName = "@locaName";
            sParams[5].Value = matLocName;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "@mat1Day";
            sParams[6].Value = mat1Day;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Decimal;
            sParams[7].ParameterName = "@matSafety";
            sParams[7].Value = matSafety;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.VarChar;
            sParams[8].ParameterName = "@matUnit";
            sParams[8].Value = matUnit;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.Int;
            sParams[9].ParameterName = "@vendIDx";
            sParams[9].Value = matVendorIDx;

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.NVarChar;
            sParams[10].ParameterName = "@vendName";
            sParams[10].Value = matVendorName;

            sParams[11] = new SqlParameter();
            sParams[11].SqlDbType = SqlDbType.Bit;
            sParams[11].ParameterName = "@matStatus";
            sParams[11].Value = matStatus;

            sParams[12] = new SqlParameter();
            sParams[12].SqlDbType = SqlDbType.NVarChar;
            sParams[12].ParameterName = "@matNote";
            sParams[12].Value = matNote;

            sParams[13] = new SqlParameter();
            sParams[13].SqlDbType = SqlDbType.Int;
            sParams[13].ParameterName = "@matLifetime";
            sParams[13].Value = matLifetime;

            sParams[14] = new SqlParameter();
            sParams[14].SqlDbType = SqlDbType.Bit;
            sParams[14].ParameterName = "@matTypeRepair";
            sParams[14].Value = matTypeRepair;

            sParams[15] = new SqlParameter();
            sParams[15].SqlDbType = SqlDbType.Bit;
            sParams[15].ParameterName = "@matTypeMaintain";
            sParams[15].Value = matTypeMaintain;

            sParams[16] = new SqlParameter();
            sParams[16].SqlDbType = SqlDbType.Bit;
            sParams[16].ParameterName = "@matTypeImprove";
            sParams[16].Value = matTypeImprove;

            sParams[17] = new SqlParameter();
            sParams[17].SqlDbType = SqlDbType.Bit;
            sParams[17].ParameterName = "@matTypeDaily";
            sParams[17].Value = matTypeDaily;

            sParams[18] = new SqlParameter();
            sParams[18].SqlDbType = SqlDbType.Image;
            sParams[18].ParameterName = "@matImage";
            sParams[18].Value = bimage;

            sParams[19] = new SqlParameter();
            sParams[19].SqlDbType = SqlDbType.Money;
            sParams[19].ParameterName = "@price";
            sParams[19].Value = price;

            cls.fnUpdDel(sql, sParams);
            bmp = null;

            _msgText = "Thêm mới vật tư thành công.";
            _msgType = 0;
        }

        public void fnMat_Upd()
        {
            fnMat_Upd_NoImage();
            //if (_matNewImage == false)
            //{
            //    fnMat_Upd_NoImage();
            //}
            //else
            //{
            //    fnMat_Upd_Image();
            //    _matNewImage = false;
            //}

        }

        public void fnMat_Upd_NoImage()
        {
            string idx = _matIDx;
            string matName = txtMat_Name.Text.Trim();
            string matCode = txtMat_Code.Text.Trim();
            int matCateIDx = Convert.ToInt32(cbbMat_Cate.SelectedValue.ToString());
            string matCateName = cbbMat_Cate.Text;
            //int matLocIDx = Convert.ToInt32(cbbMat_Loc.SelectedValue.ToString());
            int matLocIDx = 112;
            //string matLocName = cbbMat_Loc.Text;
            string matLocName = "MMT WH5";
            string mat1Day = txtMat_1DayUsing.Text.Trim();
            decimal matSafety = Convert.ToDecimal(txtMat_ReOrder.Text.Trim());
            string matUnit = cbbMat_Unit.Text;
            int matVendorIDx = Convert.ToInt32(cbbMat_Vendor.SelectedValue.ToString());
            string matVendorName = cbbMat_Vendor.Text;
            string matStatus = (rdbMat_Enabled.Checked) ? "True" : "False";
            string matNote = txtMat_Note.Text.Trim();
            string matLifetime = txtMat_Lifetime.Text.Trim();
            bool matTypeRepair = (chkMat_Repair.Checked) ? true : false;
            bool matTypeMaintain = (chkMat_Maintain.Checked) ? true : false;
            bool matTypeImprove = (chkMat_Improve.Checked) ? true : false;
            bool matTypeDaily = (chkMat_Daily.Checked) ? true : false;
            string price = txtMat_Price.Text.Trim();

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_UpdItem_V2o1_Addnew";
            SqlParameter[] sParams = new SqlParameter[20]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.NVarChar;
            sParams[0].ParameterName = "@matName";
            sParams[0].Value = matName;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@matCode";
            sParams[1].Value = matCode;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "@cateIDx";
            sParams[2].Value = matCateIDx;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "@cateName";
            sParams[3].Value = matCateName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.Int;
            sParams[4].ParameterName = "@locaIDx";
            sParams[4].Value = matLocIDx;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.NVarChar;
            sParams[5].ParameterName = "@locaName";
            sParams[5].Value = matLocName;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "@mat1Day";
            sParams[6].Value = mat1Day;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Decimal;
            sParams[7].ParameterName = "@matSafety";
            sParams[7].Value = matSafety;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.VarChar;
            sParams[8].ParameterName = "@matUnit";
            sParams[8].Value = matUnit;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.Int;
            sParams[9].ParameterName = "@vendIDx";
            sParams[9].Value = matVendorIDx;

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.NVarChar;
            sParams[10].ParameterName = "@vendName";
            sParams[10].Value = matVendorName;

            sParams[11] = new SqlParameter();
            sParams[11].SqlDbType = SqlDbType.Bit;
            sParams[11].ParameterName = "@matStatus";
            sParams[11].Value = matStatus;

            sParams[12] = new SqlParameter();
            sParams[12].SqlDbType = SqlDbType.NVarChar;
            sParams[12].ParameterName = "@matNote";
            sParams[12].Value = matNote;

            sParams[13] = new SqlParameter();
            sParams[13].SqlDbType = SqlDbType.Int;
            sParams[13].ParameterName = "@matIDx";
            sParams[13].Value = idx;

            sParams[14] = new SqlParameter();
            sParams[14].SqlDbType = SqlDbType.Int;
            sParams[14].ParameterName = "@matLifetime";
            sParams[14].Value = matLifetime;

            sParams[15] = new SqlParameter();
            sParams[15].SqlDbType = SqlDbType.Bit;
            sParams[15].ParameterName = "@matTypeRepair";
            sParams[15].Value = matTypeRepair;

            sParams[16] = new SqlParameter();
            sParams[16].SqlDbType = SqlDbType.Bit;
            sParams[16].ParameterName = "@matTypeMaintain";
            sParams[16].Value = matTypeMaintain;

            sParams[17] = new SqlParameter();
            sParams[17].SqlDbType = SqlDbType.Bit;
            sParams[17].ParameterName = "@matTypeImprove";
            sParams[17].Value = matTypeImprove;

            sParams[18] = new SqlParameter();
            sParams[18].SqlDbType = SqlDbType.Bit;
            sParams[18].ParameterName = "@matTypeDaily";
            sParams[18].Value = matTypeDaily;

            sParams[19] = new SqlParameter();
            sParams[19].SqlDbType = SqlDbType.Money;
            sParams[19].ParameterName = "@price";
            sParams[19].Value = price;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Cập nhật vật tư thành công.";
            _msgType = 0;
        }

        public void fnMat_Upd_Image()
        {
            string idx = _matIDx;
            string matName = txtMat_Name.Text.Trim();
            string matCode = txtMat_Code.Text.Trim();
            int matCateIDx = Convert.ToInt32(cbbMat_Cate.SelectedValue.ToString());
            string matCateName = cbbMat_Cate.Text;
            //int matLocIDx = Convert.ToInt32(cbbMat_Loc.SelectedValue.ToString());
            int matLocIDx = 112;
            //string matLocName = cbbMat_Loc.Text;
            string matLocName = "MMT WH5";
            string mat1Day = txtMat_1DayUsing.Text.Trim();
            decimal matSafety = Convert.ToDecimal(txtMat_ReOrder.Text.Trim());
            string matUnit = cbbMat_Unit.Text;
            int matVendorIDx = Convert.ToInt32(cbbMat_Vendor.SelectedValue.ToString());
            string matVendorName = cbbMat_Vendor.Text;
            string matStatus = (rdbMat_Enabled.Checked) ? "True" : "False";
            string matNote = txtMat_Note.Text.Trim();
            string matLifetime = txtMat_Lifetime.Text.Trim();
            bool matTypeRepair = (chkMat_Repair.Checked) ? true : false;
            bool matTypeMaintain = (chkMat_Maintain.Checked) ? true : false;
            bool matTypeImprove = (chkMat_Improve.Checked) ? true : false;
            bool matTypeDaily = (chkMat_Daily.Checked) ? true : false;
            string price = txtMat_Price.Text.Trim();

            string imgPath = _matImage;
            Bitmap bmp = new Bitmap(imgPath);
            FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            byte[] bimage = new byte[fs.Length];
            fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
            fs.Close();


            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_Image_UpdItem_V2o1_Addnew";
            SqlParameter[] sParams = new SqlParameter[21]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.NVarChar;
            sParams[0].ParameterName = "@matName";
            sParams[0].Value = matName;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@matCode";
            sParams[1].Value = matCode;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "@cateIDx";
            sParams[2].Value = matCateIDx;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.NVarChar;
            sParams[3].ParameterName = "@cateName";
            sParams[3].Value = matCateName;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.Int;
            sParams[4].ParameterName = "@locaIDx";
            sParams[4].Value = matLocIDx;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.NVarChar;
            sParams[5].ParameterName = "@locaName";
            sParams[5].Value = matLocName;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "@mat1Day";
            sParams[6].Value = mat1Day;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Decimal;
            sParams[7].ParameterName = "@matSafety";
            sParams[7].Value = matSafety;

            sParams[8] = new SqlParameter();
            sParams[8].SqlDbType = SqlDbType.VarChar;
            sParams[8].ParameterName = "@matUnit";
            sParams[8].Value = matUnit;

            sParams[9] = new SqlParameter();
            sParams[9].SqlDbType = SqlDbType.Int;
            sParams[9].ParameterName = "@vendIDx";
            sParams[9].Value = matVendorIDx;

            sParams[10] = new SqlParameter();
            sParams[10].SqlDbType = SqlDbType.NVarChar;
            sParams[10].ParameterName = "@vendName";
            sParams[10].Value = matVendorName;

            sParams[11] = new SqlParameter();
            sParams[11].SqlDbType = SqlDbType.Bit;
            sParams[11].ParameterName = "@matStatus";
            sParams[11].Value = matStatus;

            sParams[12] = new SqlParameter();
            sParams[12].SqlDbType = SqlDbType.NVarChar;
            sParams[12].ParameterName = "@matNote";
            sParams[12].Value = matNote;

            sParams[13] = new SqlParameter();
            sParams[13].SqlDbType = SqlDbType.Int;
            sParams[13].ParameterName = "@matIDx";
            sParams[13].Value = idx;

            sParams[14] = new SqlParameter();
            sParams[14].SqlDbType = SqlDbType.Int;
            sParams[14].ParameterName = "@matLifetime";
            sParams[14].Value = matLifetime;

            sParams[15] = new SqlParameter();
            sParams[15].SqlDbType = SqlDbType.Bit;
            sParams[15].ParameterName = "@matTypeRepair";
            sParams[15].Value = matTypeRepair;

            sParams[16] = new SqlParameter();
            sParams[16].SqlDbType = SqlDbType.Bit;
            sParams[16].ParameterName = "@matTypeMaintain";
            sParams[16].Value = matTypeMaintain;

            sParams[17] = new SqlParameter();
            sParams[17].SqlDbType = SqlDbType.Bit;
            sParams[17].ParameterName = "@matTypeImprove";
            sParams[17].Value = matTypeImprove;

            sParams[18] = new SqlParameter();
            sParams[18].SqlDbType = SqlDbType.Bit;
            sParams[18].ParameterName = "@matTypeDaily";
            sParams[18].Value = matTypeDaily;

            sParams[19] = new SqlParameter();
            sParams[19].SqlDbType = SqlDbType.Image;
            sParams[19].ParameterName = "@matImage";
            sParams[19].Value = bimage;

            sParams[20] = new SqlParameter();
            sParams[20].SqlDbType = SqlDbType.Money;
            sParams[20].ParameterName = "@price";
            sParams[20].Value = price;

            cls.fnUpdDel(sql, sParams);
            bmp = null;

            _msgText = "Cập nhật vật tư thành công.";
            _msgType = 0;
        }

        public void fnMat_Del()
        {
            string idx = _matIDx;

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_DelItem_V2o1_Addnew";
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@matIDx";
            sParams[0].Value = idx;

            cls.fnUpdDel(sql, sParams);

            _msgText = "Xóa bỏ vật tư thành công.";
            _msgType = 0;
        }

        private void btnMat_Finish_Click(object sender, EventArgs e)
        {
            fnMat_Finish();
        }

        private void btnMat_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (rdbMat_Add.Checked)
                {
                    fnMat_Add();
                }
                else if (rdbMat_Upd.Checked)
                {
                    fnMat_Upd();
                }
                else if (rdbMat_Del.Checked)
                {
                    fnMat_Del();
                }

                fnBindData_All();
                fnMat_Finish();

                tssMsg.Text = _msgText;
                switch (_msgType)
                {
                    case 0:
                        //MESSAGE SUCCESSFUL;
                        tssMsg.ForeColor = Color.DarkGreen;
                        break;
                    case 1:
                        //MESSAGE ERRORS
                        tssMsg.ForeColor = Color.Red;
                        break;
                    case 2:
                        //MESSAGE WARNING
                        tssMsg.ForeColor = Color.Goldenrod;
                        break;
                    default:
                        tssMsg.ForeColor = Color.Black;
                        break;
                }

            }
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{

            //}
        }

        #endregion


        #region CATEGORY FUNCTION

        public void init_Category()
        {
            try
            {
                fnCate_CreateTree();

                txtCate_Name02.Enabled = false;
                rdbCate_Enabled.Enabled = false;
                rdbCate_Disabled.Enabled = false;
                rdbCate_Add.Enabled = false;
                rdbCate_Upd.Enabled = false;
                rdbCate_Del.Enabled = false;
                btnCate_Save.Enabled = false;
                btnCate_Finish.Enabled = false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnCate_CreateTree()
        {
            trvCate_List.Nodes.Clear();
            string sql = "V2o1_BASE_SubMaterial_Init_Category_Treeview_Parent_SelItem_Addnew";
            TreeNode parentNode;
            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                parentNode = new TreeNode();
                parentNode = trvCate_List.Nodes.Add(dr[1].ToString().ToUpper());
                PopulateTreeView(Convert.ToInt32(dr[0].ToString()), parentNode);
                parentNode.ToolTipText = dr[1].ToString();
                parentNode.Tag = dr[0];
                parentNode.Text = dr[1].ToString();
            }
            trvCate_List.ExpandAll();
            trvCate_List.Nodes[0].NodeFont = new Font(trvCate_List.Font, FontStyle.Bold);
            //trvCate_List.CollapseAll();
            //if (dt.Rows.Count > 0)
            //{
            //    trvCate_List.Nodes[0].Expand();
            //}
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            TreeNode childNode;
            string sql = "V2o1_BASE_SubMaterial_Init_Category_Treeview_Child_SelItem_Addnew";
            DataTable dtChild = new DataTable();

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@parentID";
            sParams[0].Value = parentId;

            dtChild = cls.ExecuteDataTable(sql, sParams);
            foreach (DataRow dr in dtChild.Rows)
            {
                childNode = new TreeNode();
                if (parentNode == null)
                {
                    childNode = trvCate_List.Nodes.Add(dr[1].ToString());
                    childNode.ToolTipText = dr[1].ToString();
                }
                else
                {
                    childNode = parentNode.Nodes.Add(dr[1].ToString());
                    PopulateTreeView(Convert.ToInt32(dr[0].ToString()), childNode);
                }
                childNode.Tag = dr[0];
                childNode.Text = dr[1].ToString();
                childNode.ToolTipText = dr[1].ToString();
            }
        }

        private void trvCate_List_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            //MessageBox.Show(node.Tag + "---------" + node.Text);
            _cateIDx = node.Tag.ToString();
            txtCate_Name01.Text = node.Text;
            //txtCate_Name02.Text = txtCate_Name01.Text;
            //txtCate_Name02.Focus();

            rdbCate_Enabled.Enabled = true;
            rdbCate_Disabled.Enabled = true;

            txtCate_Name02.Enabled = true;
            rdbCate_Add.Enabled = true;
            rdbCate_Upd.Enabled = true;
            rdbCate_Del.Enabled = (_cateIDx != "122") ? true : false;

            rdbCate_Add.Checked = false;
            rdbCate_Upd.Checked = false;
            rdbCate_Del.Checked = false;
            btnCate_Finish.Enabled = true;
        }

        private void rdbCate_Add_CheckedChanged(object sender, EventArgs e)
        {
            btnCate_Save.Enabled = true;
        }

        private void rdbCate_Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnCate_Save.Enabled = true;
        }

        private void rdbCate_Del_CheckedChanged(object sender, EventArgs e)
        {
            btnCate_Save.Enabled = true;
        }

        public void fnCate_Finish()
        {
            _cateIDx = "";
            fnCate_CreateTree();
            txtCate_Name01.Text = "";
            txtCate_Name02.Text = "";
            txtCate_Name02.Enabled = false;
            rdbCate_Enabled.Enabled = false;
            rdbCate_Disabled.Enabled = false;
            rdbCate_Add.Enabled = false;
            rdbCate_Upd.Enabled = false;
            rdbCate_Del.Enabled = false;
            btnCate_Save.Enabled = false;
            btnCate_Finish.Enabled = false;

        }

        public void fnCate_Add()
        {
            try
            {
                string cateName = txtCate_Name02.Text.Trim();
                string sql = "V2o1_BASE_SubMaterial_Init_Category_Addnew_AddItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@cateIDx";
                sParams[0].Value = _cateIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.NVarChar;
                sParams[1].ParameterName = "@cateName";
                sParams[1].Value = cateName;

                cls.fnUpdDel(sql, sParams);

                _msgText = "Thêm mới mục thành công.";
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnCate_Upd()
        {
            try
            {
                string cateName = txtCate_Name02.Text.Trim();
                if (cateName != "" && cateName != null)
                {
                    string cateStatus = (rdbCate_Enabled.Checked) ? "1" : "0";
                    string sql = "V2o1_BASE_SubMaterial_Init_Category_Update_UpdItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@cateIDx";
                    sParams[0].Value = _cateIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.NVarChar;
                    sParams[1].ParameterName = "@cateName";
                    sParams[1].Value = cateName;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.TinyInt;
                    sParams[2].ParameterName = "@cateStatus";
                    sParams[2].Value = cateStatus;

                    cls.fnUpdDel(sql, sParams);

                    _msgText = "Cập nhật danh mục thành công.";
                }
                else
                {
                    _msgText = "Vui lòng nhập tên danh mục để cập nhật.";
                }
            }
            catch
            {

            }
            finally
            {
                
            }
        }

        public void fnCate_Del()
        {
            try
            {
                string sqlChk = "V2o1_BASE_SubMaterial_Init_Category_Remove_ChkItem_Addnew";
                SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                sParamsChk[0] = new SqlParameter();
                sParamsChk[0].SqlDbType = SqlDbType.Int;
                sParamsChk[0].ParameterName = "@cateIDx";
                sParamsChk[0].Value = _cateIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sqlChk, sParamsChk);
                
                if (ds.Tables[0].Rows.Count == 0)
                {
                    string sql = "V2o1_BASE_SubMaterial_Init_Category_Remove_DelItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@cateIDx";
                    sParams[0].Value = _cateIDx;

                    cls.fnUpdDel(sql, sParams);

                    _msgText = "Xóa mục thành công.";
                }
                else
                {
                    _msgText = "Không thể xóa mục khi còn hàng hóa thuộc danh mục.";
                }
                
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnCate_Finish_Click(object sender, EventArgs e)
        {
            fnCate_Finish();
        }

        private void btnCate_Save_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (rdbCate_Add.Checked)
                    {
                        fnCate_Add();
                    }
                    else if (rdbCate_Upd.Checked)
                    {
                        fnCate_Upd();
                    }
                    else if (rdbCate_Del.Checked)
                    {
                        fnCate_Del();
                    }
                    tssMsg.Text = _msgText;
                    fnCate_Finish();
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        #endregion


        #region VENDOR FUNCTION

        public void init_Vendor()
        {
            try
            {
                init_Vendor_List();
                init_Vendor_Money();

                txtVendor_Name.Text = "";
                cbbVendor_Money.SelectedIndex = 107;
                txtVendor_Addr.Text = "";
                txtVendor_Tel.Text = "";
                txtVendor_Fax.Text = "";
                txtVendor_Email.Text = "";
                txtVendor_Web.Text = "";
                txtVendor_Note.Text = "";

                //rdbVendor_Add.Checked = true;
                rdbVendor_Add.Enabled = true;
                rdbVendor_Upd.Enabled = false;
                rdbVendor_Del.Enabled = false;

                btnVendor_Save.Enabled = false;
                btnVendor_Finish.Enabled = true;

                txtVendor_Name.Focus();
            }
            catch
            {
                
            }
            finally
            {

            }
        }

        public void init_Vendor_List()
        {
            string sql = "V2o1_BASE_SubMaterial_Init_Vendor_List_SelItem_Addnew";
            DataTable dt = new DataTable();

            SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
            dt = cls.ExecuteDataTable(sql, sParams);
            
            _dgvVendorWidth = cls.fnGetDataGridWidth(dgvVendor_List);
            dgvVendor_List.DataSource = dt;

            //dgvVendor_List.Columns[0].Width = 10 * _dgvVendorWidth / 100;    // idx
            dgvVendor_List.Columns[1].Width = 35 * _dgvVendorWidth / 100;    // idx
            dgvVendor_List.Columns[2].Width = 50 * _dgvVendorWidth / 100;    // idx
            dgvVendor_List.Columns[3].Width = 15 * _dgvVendorWidth / 100;    // idx

            dgvVendor_List.Columns[0].Visible = false;
            dgvVendor_List.Columns[1].Visible = true;
            dgvVendor_List.Columns[2].Visible = true;
            dgvVendor_List.Columns[3].Visible = true;

            cls.fnFormatDatagridview(dgvVendor_List, 11, 30);
        }

        public void init_Vendor_Money()
        {
            string sql = "V2o1_BASE_SubMaterial_Init_Vendor_Currency_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbVendor_Money.DataSource = dt;
            cbbVendor_Money.DisplayMember = "code";
            cbbVendor_Money.ValueMember = "currencyId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbVendor_Money.SelectedIndex = 0;
        }

        private void dgvVendor_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvVendor_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvVendor_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvVendor_List.Rows[e.RowIndex];

                string vendorIDx = row.Cells[0].Value.ToString();
                string vendorName = row.Cells[1].Value.ToString();
                string vendorAddr = row.Cells[2].Value.ToString();
                string vendorTel = row.Cells[4].Value.ToString();
                string vendorFax = row.Cells[5].Value.ToString();
                string vendorEmail = row.Cells[6].Value.ToString();
                string vendorWeb = row.Cells[7].Value.ToString();
                string vendorNote = row.Cells[8].Value.ToString();
                string vendorCurrency = row.Cells[9].Value.ToString();

                _vendorIDx = vendorIDx;

                txtVendor_Name.Text = vendorName;
                txtVendor_Addr.Text = vendorAddr;
                txtVendor_Tel.Text = vendorTel;
                txtVendor_Fax.Text = vendorFax;
                txtVendor_Email.Text = vendorEmail;
                txtVendor_Web.Text = vendorWeb;
                txtVendor_Note.Text = vendorNote;
                cbbVendor_Money.SelectedValue = vendorCurrency;

                rdbVendor_Add.Checked = false;
                rdbVendor_Add.Enabled = false;
                rdbVendor_Upd.Enabled = true;
                rdbVendor_Del.Enabled = true;
            }
        }

        private void dgvVendor_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvVendor_List.Rows[e.RowIndex].Selected = true;
                _rowVendorListIndex = e.RowIndex;
                dgvVendor_List.CurrentCell = dgvVendor_List.Rows[e.RowIndex].Cells[1];
                cmsVendor_Menu.Show(this.dgvVendor_List, e.Location);
                cmsVendor_Menu.Show(Cursor.Position);
            }
        }

        private void refeshThisListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvVendor_List.ClearSelection();
            init_Vendor_List();
        }

        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    fnVendor_Del();
                    init_Vendor_List();
                    fnVendor_Finish();
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void informationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string vendorIDx = _vendorIDx;
            frmSub01CreateMaterial_VendorDetails frm = new frmSub01CreateMaterial_VendorDetails(vendorIDx);
            frm.ShowDialog();
        }

        private void rdbVendor_Add_CheckedChanged(object sender, EventArgs e)
        {
            btnVendor_Save.Enabled = true;
        }

        private void rdbVendor_Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnVendor_Save.Enabled = true;
        }

        private void rdbVendor_Del_CheckedChanged(object sender, EventArgs e)
        {
            btnVendor_Save.Enabled = true;
        }

        public void fnVendor_Finish()
        {
            init_Vendor();
            _vendorIDx = "";

            rdbVendor_Add.Checked = false;
            rdbVendor_Upd.Checked = false;
            rdbVendor_Del.Checked = false;

            rdbVendor_Add.Enabled = true;
            rdbVendor_Upd.Enabled = false;
            rdbVendor_Del.Enabled = false;

            btnVendor_Save.Enabled = false;
            btnVendor_Finish.Enabled = true;
        }

        private void btnVendor_Finish_Click(object sender, EventArgs e)
        {
            fnVendor_Finish();
        }

        public void fnVendor_Add()
        {
            try
            {
                string vendorName = txtVendor_Name.Text.Trim();
                string vendorAddr = txtVendor_Addr.Text.Trim();
                string vendorTel = txtVendor_Tel.Text.Trim();
                string vendorFax = txtVendor_Fax.Text.Trim();
                string vendorEmail = txtVendor_Email.Text.Trim();
                string vendorWeb = txtVendor_Web.Text.Trim();
                string vendorNote = txtVendor_Note.Text.Trim();
                string currencyId = cbbVendor_Money.SelectedValue.ToString();

                if (vendorName != "" && vendorName != null)
                {
                    string sql = "V2o1_BASE_SubMaterial_Init_Vendor_Addnew_List_AddItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[8]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.NVarChar;
                    sParams[0].ParameterName = "@vName";
                    sParams[0].Value = vendorName;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.NVarChar;
                    sParams[1].ParameterName = "@vAddr";
                    sParams[1].Value = vendorAddr;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.VarChar;
                    sParams[2].ParameterName = "@vTel";
                    sParams[2].Value = vendorTel;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.VarChar;
                    sParams[3].ParameterName = "@vFax";
                    sParams[3].Value = vendorFax;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.VarChar;
                    sParams[4].ParameterName = "@vEmail";
                    sParams[4].Value = vendorEmail;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.VarChar;
                    sParams[5].ParameterName = "@vWeb";
                    sParams[5].Value = vendorWeb;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.NVarChar;
                    sParams[6].ParameterName = "@vNote";
                    sParams[6].Value = vendorNote;

                    sParams[7] = new SqlParameter();
                    sParams[7].SqlDbType = SqlDbType.Int;
                    sParams[7].ParameterName = "@vCurrency";
                    sParams[7].Value = currencyId;

                    cls.fnUpdDel(sql, sParams);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void fnVendor_Upd()
        {
            try
            {
                string vendorIDx = _vendorIDx;
                string vendorName = txtVendor_Name.Text.Trim();
                string vendorAddr = txtVendor_Addr.Text.Trim();
                string vendorTel = txtVendor_Tel.Text.Trim();
                string vendorFax = txtVendor_Fax.Text.Trim();
                string vendorEmail = txtVendor_Email.Text.Trim();
                string vendorWeb = txtVendor_Web.Text.Trim();
                string vendorNote = txtVendor_Note.Text.Trim();
                string currencyId = cbbVendor_Money.SelectedValue.ToString();

                if (vendorIDx != "" && vendorName != "")
                {
                    string sql = "V2o1_BASE_SubMaterial_Init_Vendor_Update_List_UpdItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[9]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@vIDx";
                    sParams[0].Value = vendorIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.NVarChar;
                    sParams[1].ParameterName = "@vName";
                    sParams[1].Value = vendorName;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.NVarChar;
                    sParams[2].ParameterName = "@vAddr";
                    sParams[2].Value = vendorAddr;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.VarChar;
                    sParams[3].ParameterName = "@vTel";
                    sParams[3].Value = vendorTel;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.VarChar;
                    sParams[4].ParameterName = "@vFax";
                    sParams[4].Value = vendorFax;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.VarChar;
                    sParams[5].ParameterName = "@vEmail";
                    sParams[5].Value = vendorEmail;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.VarChar;
                    sParams[6].ParameterName = "@vWeb";
                    sParams[6].Value = vendorWeb;

                    sParams[7] = new SqlParameter();
                    sParams[7].SqlDbType = SqlDbType.NVarChar;
                    sParams[7].ParameterName = "@vNote";
                    sParams[7].Value = vendorNote;

                    sParams[8] = new SqlParameter();
                    sParams[8].SqlDbType = SqlDbType.Int;
                    sParams[8].ParameterName = "@vCurrency";
                    sParams[8].Value = currencyId;

                    cls.fnUpdDel(sql, sParams);
                }
            }
            catch (SqlException sqlEx)
            {
                _msgText="Có lỗi khi đẩy dữ liệu về máy chủ";
                _msgType=3;
                MessageBox.Show(sqlEx.ToString());
            }
            catch (Exception ex)
            {
                _msgText="Có lỗi phát sinh.";
                _msgType=2;
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        public void fnVendor_Del()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_Vendor_Remove_List_UpdItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@vendorId";
                sParams[0].Value = _vendorIDx;

                cls.fnUpdDel(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnVendor_Save_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (rdbVendor_Add.Checked)
                    {
                        fnVendor_Add();
                    }
                    else if (rdbVendor_Upd.Checked)
                    {
                        fnVendor_Upd();
                    }
                    else if (rdbVendor_Del.Checked)
                    {
                        fnVendor_Del();
                    }

                    init_Vendor_List();
                    fnVendor_Finish();
                }
            }
            catch
            {
                
            }
            finally
            {

            }
        }

        #endregion


        #region BINDING DATA


        public void fnBindData_All()
        {
            try
            {
                //string sql = "V2o1_BASE_SubMaterial_Init_BindingData_All_SelItem_Addnew";
                string sql = "V2o1_BASE_SubMaterial_Init_BindingData_All_SelItem_V2o1_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "@letter";
                sParams[0].Value = _letter;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);
                //dt = cls.fnSelect(sql);

                _dgvMaterial_All_Width = cls.fnGetDataGridWidth(dgvMaterial_All);
                dgvMaterial_All.DataSource = dt;

                dgvMaterial_All.Columns[0].Width = 5 * _dgvMaterial_All_Width / 100;    // [STT]
                //dgvMaterial_All.Columns[1].Width = 5 * _dgvMaterial_All_Width / 100;    // [ProdId]
                dgvMaterial_All.Columns[2].Width = 23 * _dgvMaterial_All_Width / 100;    // [Name]
                dgvMaterial_All.Columns[3].Width = 18 * _dgvMaterial_All_Width / 100;    // [BarCode]
                dgvMaterial_All.Columns[4].Width = 13 * _dgvMaterial_All_Width / 100;    // [Name]
                dgvMaterial_All.Columns[5].Width = 5 * _dgvMaterial_All_Width / 100;    // [Uom]
                dgvMaterial_All.Columns[6].Width = 8 * _dgvMaterial_All_Width / 100;    // [Custom1]
                dgvMaterial_All.Columns[7].Width = 8 * _dgvMaterial_All_Width / 100;    // [ReorderQuantity]
                dgvMaterial_All.Columns[8].Width = 8 * _dgvMaterial_All_Width / 100;    // [Total]
                dgvMaterial_All.Columns[9].Width = 13 * _dgvMaterial_All_Width / 100;    // [Name]
                //dgvMaterial_All.Columns[10].Width = 5 * _dgvMaterial_All_Width / 100;    // [DefaultLocationId]
                //dgvMaterial_All.Columns[11].Width = 5 * _dgvMaterial_All_Width / 100;    // [IsActive]
                //dgvMaterial_All.Columns[12].Width = 5 * _dgvMaterial_All_Width / 100;    // [Note]
                //dgvMaterial_All.Columns[13].Width = 5 * _dgvMaterial_All_Width / 100;    // [CategoryId]
                //dgvMaterial_All.Columns[14].Width = 5 * _dgvMaterial_All_Width / 100;    // [LastVendorId]
                //dgvMaterial_All.Columns[15].Width = 5 * _dgvMaterial_All_Width / 100;    // [LifeCycle]
                //dgvMaterial_All.Columns[16].Width = 5 * _dgvMaterial_All_Width / 100;    // [Repair]
                //dgvMaterial_All.Columns[17].Width = 5 * _dgvMaterial_All_Width / 100;    // [Maintain]
                //dgvMaterial_All.Columns[18].Width = 5 * _dgvMaterial_All_Width / 100;    // [Improve]
                //dgvMaterial_All.Columns[19].Width = 5 * _dgvMaterial_All_Width / 100;    // [Daily]
                //dgvMaterial_All.Columns[20].Width = 5 * _dgvMaterial_All_Width / 100;    // [ImageStatus]
                //dgvMaterial_All.Columns[21].Width = 5 * _dgvMaterial_All_Width / 100;    // [price]

                dgvMaterial_All.Columns[0].Visible = true;
                dgvMaterial_All.Columns[1].Visible = false;
                dgvMaterial_All.Columns[2].Visible = true;
                dgvMaterial_All.Columns[3].Visible = true;
                dgvMaterial_All.Columns[4].Visible = true;
                dgvMaterial_All.Columns[5].Visible = true;
                dgvMaterial_All.Columns[6].Visible = true;
                dgvMaterial_All.Columns[7].Visible = true;
                dgvMaterial_All.Columns[8].Visible = true;
                dgvMaterial_All.Columns[9].Visible = true;
                dgvMaterial_All.Columns[10].Visible = false;
                dgvMaterial_All.Columns[11].Visible = false;
                dgvMaterial_All.Columns[12].Visible = false;
                dgvMaterial_All.Columns[13].Visible = false;
                dgvMaterial_All.Columns[14].Visible = false;
                dgvMaterial_All.Columns[15].Visible = false;
                dgvMaterial_All.Columns[16].Visible = false;
                dgvMaterial_All.Columns[17].Visible = false;
                dgvMaterial_All.Columns[18].Visible = false;
                dgvMaterial_All.Columns[19].Visible = false;
                dgvMaterial_All.Columns[20].Visible = false;
                dgvMaterial_All.Columns[21].Visible = false;



                ////dgvMaterial_All.Columns[1].Width = 10 * _dgvMaterial_All_Width / 100;    // ProdId
                //dgvMaterial_All.Columns[2].Width = 25 * _dgvMaterial_All_Width / 100;    // Name
                //dgvMaterial_All.Columns[3].Width = 20 * _dgvMaterial_All_Width / 100;    // BarCode
                //dgvMaterial_All.Columns[4].Width = 13 * _dgvMaterial_All_Width / 100;    // Category
                //dgvMaterial_All.Columns[5].Width = 5 * _dgvMaterial_All_Width / 100;    // Unit
                //dgvMaterial_All.Columns[6].Width = 8 * _dgvMaterial_All_Width / 100;    // 1-Day Using
                //dgvMaterial_All.Columns[7].Width = 8 * _dgvMaterial_All_Width / 100;    // Safety Stock
                //dgvMaterial_All.Columns[8].Width = 8 * _dgvMaterial_All_Width / 100;    // Current Stock
                //dgvMaterial_All.Columns[9].Width = 13 * _dgvMaterial_All_Width / 100;    // Vendor
                ////dgvMaterial_All.Columns[10].Width = 13 * _dgvMaterial_All_Width / 100;    // DefaultLocationId
                ////dgvMaterial_All.Columns[11].Width = 13 * _dgvMaterial_All_Width / 100;    // isActive
                ////dgvMaterial_All.Columns[12].Width = 13 * _dgvMaterial_All_Width / 100;    // Note
                ////dgvMaterial_All.Columns[13].Width = 13 * _dgvMaterial_All_Width / 100;    // cateIDx
                ////dgvMaterial_All.Columns[14].Width = 13 * _dgvMaterial_All_Width / 100;    // lastvendorID
                ////dgvMaterial_All.Columns[15].Width = 13 * _dgvMaterial_All_Width / 100;    // LifeCycle
                ////dgvMaterial_All.Columns[16].Width = 13 * _dgvMaterial_All_Width / 100;    // Repair
                ////dgvMaterial_All.Columns[17].Width = 13 * _dgvMaterial_All_Width / 100;    // Maintain
                ////dgvMaterial_All.Columns[18].Width = 13 * _dgvMaterial_All_Width / 100;    // Improve
                ////dgvMaterial_All.Columns[19].Width = 13 * _dgvMaterial_All_Width / 100;    // Daily
                ////dgvMaterial_All.Columns[20].Width = 13 * _dgvMaterial_All_Width / 100;    // ImageStatus
                ////dgvMaterial_All.Columns[21].Width = 13 * _dgvMaterial_All_Width / 100;    // MatImage
                ////dgvMaterial_All.Columns[22].Width = 13 * _dgvMaterial_All_Width / 100;    // Price

                //dgvMaterial_All.Columns[0].Visible = true;
                //dgvMaterial_All.Columns[1].Visible = false;
                //dgvMaterial_All.Columns[2].Visible = true;
                //dgvMaterial_All.Columns[3].Visible = true;
                //dgvMaterial_All.Columns[4].Visible = true;
                //dgvMaterial_All.Columns[5].Visible = true;
                //dgvMaterial_All.Columns[6].Visible = true;
                //dgvMaterial_All.Columns[7].Visible = true;
                //dgvMaterial_All.Columns[8].Visible = true;
                //dgvMaterial_All.Columns[9].Visible = true;
                //dgvMaterial_All.Columns[10].Visible = false;
                //dgvMaterial_All.Columns[11].Visible = false;
                //dgvMaterial_All.Columns[12].Visible = false;
                //dgvMaterial_All.Columns[13].Visible = false;
                //dgvMaterial_All.Columns[14].Visible = false;
                //dgvMaterial_All.Columns[15].Visible = false;
                //dgvMaterial_All.Columns[16].Visible = false;
                //dgvMaterial_All.Columns[17].Visible = false;
                //dgvMaterial_All.Columns[18].Visible = false;
                //dgvMaterial_All.Columns[19].Visible = false;
                //dgvMaterial_All.Columns[20].Visible = false;
                //dgvMaterial_All.Columns[21].Visible = false;
                //dgvMaterial_All.Columns[22].Visible = false;

                cls.fnFormatDatagridviewWhite(dgvMaterial_All, 12, 50);

                dgvMaterial_All.BackgroundColor = Color.White;
                fnBindData_All_Color();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnBindData_All_Color()
        {

            string using1day = "", safetystock = "", currentstock = "";
            int _using1day = 0;
            decimal _safetystock = 0, _currentstock = 0;
            foreach (DataGridViewRow row in dgvMaterial_All.Rows)
            {
                //idx = row.Cells[0].Value.ToString();
                //name = row.Cells[1].Value.ToString();
                //code = row.Cells[2].Value.ToString();
                //cateName = row.Cells[3].Value.ToString();
                //unit = row.Cells[4].Value.ToString();
                using1day = row.Cells[6].Value.ToString();
                safetystock = row.Cells[7].Value.ToString();
                currentstock = row.Cells[8].Value.ToString();
                //vendorName = row.Cells[8].Value.ToString();
                //location = row.Cells[9].Value.ToString();
                //status = row.Cells[10].Value.ToString();
                //remark = row.Cells[11].Value.ToString();
                //cateIDx = row.Cells[12].Value.ToString();
                //vendorIDx = row.Cells[13].Value.ToString();

                _using1day = (using1day != "" && using1day != null) ? Convert.ToInt32(using1day) : 0;
                _safetystock = (safetystock != "" && safetystock != null) ? Convert.ToDecimal(safetystock) : 0;
                _currentstock = (currentstock != "" && currentstock != null) ? Convert.ToDecimal(currentstock) : 0;

                if (_currentstock <= 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else if (_currentstock > 0 && _currentstock <= _safetystock)
                {
                    row.DefaultCellStyle.BackColor = Color.Khaki;
                }
                else if (_currentstock > _safetystock)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
            dgvMaterial_All.ClearSelection();
        }

        private void dgvMaterial_All_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            fnBindData_All_Color();

            //string using1day = "", safetystock = "", currentstock = "";
            //int _using1day = 0;
            //decimal _safetystock = 0, _currentstock = 0;
            //foreach (DataGridViewRow row in dgvMaterial_All.Rows)
            //{
            //    //idx = row.Cells[0].Value.ToString();
            //    //name = row.Cells[1].Value.ToString();
            //    //code = row.Cells[2].Value.ToString();
            //    //cateName = row.Cells[3].Value.ToString();
            //    //unit = row.Cells[4].Value.ToString();
            //    using1day = row.Cells[5].Value.ToString();
            //    safetystock = row.Cells[6].Value.ToString();
            //    currentstock = row.Cells[7].Value.ToString();
            //    //vendorName = row.Cells[8].Value.ToString();
            //    //location = row.Cells[9].Value.ToString();
            //    //status = row.Cells[10].Value.ToString();
            //    //remark = row.Cells[11].Value.ToString();
            //    //cateIDx = row.Cells[12].Value.ToString();
            //    //vendorIDx = row.Cells[13].Value.ToString();

            //    _using1day = (using1day != "" && using1day != null) ? Convert.ToInt32(using1day) : 0;
            //    _safetystock = (safetystock != "" && safetystock != null) ? Convert.ToDecimal(safetystock) : 0;
            //    _currentstock = (currentstock != "" && currentstock != null) ? Convert.ToDecimal(currentstock) : 0;

            //    if (_currentstock <= 0)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.MistyRose;
            //    }
            //    else if (_currentstock > 0 && _currentstock <= _safetystock)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Khaki;
            //    }
            //    else if (_currentstock > _safetystock)
            //    {
            //        row.DefaultCellStyle.BackColor = Color.White;
            //    }
            //}
        }

        private void dgvMaterial_All_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    cls.fnDatagridClickCell(dgvMaterial_All, e);
                    DataGridViewRow row = new DataGridViewRow();
                    row = dgvMaterial_All.Rows[e.RowIndex];

                    string idx = row.Cells[1].Value.ToString();
                    string name = row.Cells[2].Value.ToString();
                    string code = row.Cells[3].Value.ToString();
                    string cateName = row.Cells[4].Value.ToString();
                    string unit = row.Cells[5].Value.ToString();
                    string using1day = row.Cells[6].Value.ToString();
                    string safetystock = row.Cells[7].Value.ToString();
                    string currentstock = row.Cells[8].Value.ToString();
                    string vendorName = row.Cells[9].Value.ToString();
                    string location = row.Cells[10].Value.ToString();
                    string status = row.Cells[11].Value.ToString();
                    string remark = row.Cells[12].Value.ToString();
                    string cateIDx = row.Cells[13].Value.ToString();
                    string vendorIDx = row.Cells[14].Value.ToString();
                    string lifetime = row.Cells[15].Value.ToString();
                    string repair = row.Cells[16].Value.ToString().ToLower();
                    string maintain = row.Cells[17].Value.ToString().ToLower();
                    string improve = row.Cells[18].Value.ToString().ToLower();
                    string daily = row.Cells[19].Value.ToString().ToLower();
                    string imagestatus = row.Cells[20].Value.ToString().ToLower();
                    string price = row.Cells[21].Value.ToString();

                    _matIDx = idx;
                    _matName = name;
                    _matCode = code;
                    _matCate = cateIDx;
                    _matUnit = unit;
                    _mat1Day = using1day;
                    _matSafety = safetystock;
                    _matStocked = currentstock;
                    _matVendor = vendorIDx;
                    _matLoc = location;
                    _matStatus = status;
                    _matRemark = remark;

                    tabControl1.SelectedIndex = 1;

                    txtMat_Name.Text = _matName;
                    txtMat_Code.Text = _matCode;
                    cbbMat_Cate.SelectedValue = _matCate;
                    cbbMat_Loc.SelectedValue = _matLoc;
                    txtMat_1DayUsing.Text = _mat1Day;
                    txtMat_ReOrder.Text = _matSafety;
                    cbbMat_Unit.Text = _matUnit;
                    cbbMat_Vendor.SelectedValue = _matVendor;
                    rdbMat_Enabled.Checked = (status.ToLower() == "true") ? true : false;
                    rdbMat_Disabled.Checked = (status.ToLower() == "false") ? true : false;
                    txtMat_Note.Text = _matRemark;
                    chkMat_Repair.Checked = (repair == "true") ? true : false;
                    chkMat_Maintain.Checked = (maintain == "true") ? true : false;
                    chkMat_Improve.Checked = (improve == "true") ? true : false;
                    chkMat_Daily.Checked = (daily == "true") ? true : false;
                    txtMat_Lifetime.Text = (lifetime != "" && lifetime != null) ? lifetime : "0";
                    txtMat_Price.Text = (price != "" && price != null) ? price : "0";

                    rdbMat_Add.Checked = false;
                    rdbMat_Add.Enabled = false;
                    rdbMat_Upd.Enabled = true;
                    rdbMat_Del.Enabled = true;

                    btnMat_Save.Enabled = false;
                    btnMat_Finish.Enabled = true;

                    if (imagestatus == "true")
                    {
                        string sqlImage = "V2o1_BASE_SubMaterial_Init_BindingData_Image_SelItem_V1o1_Addnew";

                        SqlParameter[] sParamsImage = new SqlParameter[1]; // Parameter count
                        sParamsImage[0] = new SqlParameter();
                        sParamsImage[0].SqlDbType = SqlDbType.Int;
                        sParamsImage[0].ParameterName = "@matIDx";
                        sParamsImage[0].Value = _matIDx;

                        DataSet ds = new DataSet();
                        ds = cls.ExecuteDataSet(sqlImage, sParamsImage);

                        Bitmap bmp;

                        _ms = new MemoryStream((byte[])ds.Tables[0].Rows[0][0]);
                        bmp = new Bitmap(_ms);
                        panel1.BackgroundImage = bmp;
                        panel1.BackgroundImageLayout = ImageLayout.Stretch;

                        lnkMat_View.Enabled = true;
                        lnkMat_Clear.Enabled = true;
                    }
                    else
                    {
                        _ms = null;
                        panel1.BackgroundImage = null;

                        lnkMat_View.Enabled = false;
                        lnkMat_Clear.Enabled = false;
                    }
                    lnkMat_Change.Enabled = true;
                    lnkMat_Browse.Enabled = false;
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        private void dgvMaterial_All_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void fnBindData_Shortage()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_BindingData_Shortage_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgvMaterial_Shortage_Width = cls.fnGetDataGridWidth(dgvMaterial_Shortage);
                dgvMaterial_Shortage.DataSource = dt;

                //dgvMaterial_Shortage.Columns[0].Width = 10 * _dgvMaterial_Shortage_Width / 100;    // ProdId
                dgvMaterial_Shortage.Columns[1].Width = 25 * _dgvMaterial_Shortage_Width / 100;    // Name
                dgvMaterial_Shortage.Columns[2].Width = 20 * _dgvMaterial_Shortage_Width / 100;    // BarCode
                dgvMaterial_Shortage.Columns[3].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // Category
                dgvMaterial_Shortage.Columns[4].Width = 5 * _dgvMaterial_Shortage_Width / 100;    // Unit
                dgvMaterial_Shortage.Columns[5].Width = 8 * _dgvMaterial_Shortage_Width / 100;    // 1-Day Using
                dgvMaterial_Shortage.Columns[6].Width = 8 * _dgvMaterial_Shortage_Width / 100;    // Safety Stock
                dgvMaterial_Shortage.Columns[7].Width = 8 * _dgvMaterial_Shortage_Width / 100;    // Current Stock
                dgvMaterial_Shortage.Columns[8].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // Vendor
                //dgvMaterial_Shortage.Columns[9].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // DefaultLocationId
                //dgvMaterial_Shortage.Columns[10].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // isActive
                //dgvMaterial_Shortage.Columns[11].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // Note
                //dgvMaterial_Shortage.Columns[11].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // cateIDx
                //dgvMaterial_Shortage.Columns[12].Width = 13 * _dgvMaterial_Shortage_Width / 100;    // lastvendorID

                dgvMaterial_Shortage.Columns[0].Visible = false;
                dgvMaterial_Shortage.Columns[1].Visible = true;
                dgvMaterial_Shortage.Columns[2].Visible = true;
                dgvMaterial_Shortage.Columns[3].Visible = true;
                dgvMaterial_Shortage.Columns[4].Visible = true;
                dgvMaterial_Shortage.Columns[5].Visible = true;
                dgvMaterial_Shortage.Columns[6].Visible = true;
                dgvMaterial_Shortage.Columns[7].Visible = true;
                dgvMaterial_Shortage.Columns[8].Visible = true;
                dgvMaterial_Shortage.Columns[9].Visible = false;
                dgvMaterial_Shortage.Columns[10].Visible = false;
                dgvMaterial_Shortage.Columns[11].Visible = false;
                dgvMaterial_Shortage.Columns[12].Visible = false;
                dgvMaterial_Shortage.Columns[13].Visible = false;

                cls.fnFormatDatagridviewWhite(dgvMaterial_Shortage, 12, 50);

                foreach (DataGridViewRow row in dgvMaterial_Shortage.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Khaki;
                }

                dgvMaterial_Shortage.BackgroundColor = Color.Khaki;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvMaterial_Shortage_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMaterial_Shortage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvMaterial_Shortage, e);
            }
            else
            {
                foreach (DataGridViewRow row in dgvMaterial_Shortage.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Khaki;
                }

                dgvMaterial_Shortage.BackgroundColor = Color.Khaki;
            }
        }

        private void dgvMaterial_Shortage_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void fnBindData_OutStock()
        {
            try
            {
                string sql = "V2o1_BASE_SubMaterial_Init_BindingData_OutStock_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgvMaterial_OutStock_Width = cls.fnGetDataGridWidth(dgvMaterial_OutStock);
                dgvMaterial_OutStock.DataSource = dt;

                //dgvMaterial_OutStock.Columns[0].Width = 10 * _dgvMaterial_OutStock_Width / 100;    // ProdId
                dgvMaterial_OutStock.Columns[1].Width = 25 * _dgvMaterial_OutStock_Width / 100;    // Name
                dgvMaterial_OutStock.Columns[2].Width = 20 * _dgvMaterial_OutStock_Width / 100;    // BarCode
                dgvMaterial_OutStock.Columns[3].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // Category
                dgvMaterial_OutStock.Columns[4].Width = 5 * _dgvMaterial_OutStock_Width / 100;    // Unit
                dgvMaterial_OutStock.Columns[5].Width = 8 * _dgvMaterial_OutStock_Width / 100;    // 1-Day Using
                dgvMaterial_OutStock.Columns[6].Width = 8 * _dgvMaterial_OutStock_Width / 100;    // Safety Stock
                dgvMaterial_OutStock.Columns[7].Width = 8 * _dgvMaterial_OutStock_Width / 100;    // Current Stock
                dgvMaterial_OutStock.Columns[8].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // Vendor
                //dgvMaterial_OutStock.Columns[9].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // DefaultLocationId
                //dgvMaterial_OutStock.Columns[10].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // isActive
                //dgvMaterial_OutStock.Columns[11].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // Note
                //dgvMaterial_OutStock.Columns[11].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // cateIDx
                //dgvMaterial_OutStock.Columns[12].Width = 13 * _dgvMaterial_OutStock_Width / 100;    // lastvendorID

                dgvMaterial_OutStock.Columns[0].Visible = false;
                dgvMaterial_OutStock.Columns[1].Visible = true;
                dgvMaterial_OutStock.Columns[2].Visible = true;
                dgvMaterial_OutStock.Columns[3].Visible = true;
                dgvMaterial_OutStock.Columns[4].Visible = true;
                dgvMaterial_OutStock.Columns[5].Visible = true;
                dgvMaterial_OutStock.Columns[6].Visible = true;
                dgvMaterial_OutStock.Columns[7].Visible = true;
                dgvMaterial_OutStock.Columns[8].Visible = true;
                dgvMaterial_OutStock.Columns[9].Visible = false;
                dgvMaterial_OutStock.Columns[10].Visible = false;
                dgvMaterial_OutStock.Columns[11].Visible = false;
                dgvMaterial_OutStock.Columns[12].Visible = false;
                dgvMaterial_OutStock.Columns[13].Visible = false;

                cls.fnFormatDatagridviewWhite(dgvMaterial_OutStock, 12, 50);

                foreach(DataGridViewRow row in dgvMaterial_OutStock.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }

                dgvMaterial_OutStock.BackgroundColor = Color.MistyRose;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvMaterial_OutStock_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMaterial_OutStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvMaterial_OutStock, e);
            }
            else
            {
                foreach (DataGridViewRow row in dgvMaterial_OutStock.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }

                dgvMaterial_OutStock.BackgroundColor = Color.MistyRose;
            }

        }

        private void dgvMaterial_OutStock_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void lnkMaterial_All_Load_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fnBindData_All();
        }

        private void lnkMaterial_Hash_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "123";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_A_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "A";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_B_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "B";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_C_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "C";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_D_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "D";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_E_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "E";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_F_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "F";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_G_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "G";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_H_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "H";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_I_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "I";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_J_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "J";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_K_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "K";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_L_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "L";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_M_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "M";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_N_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "N";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_O_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "O";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_P_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "P";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_Q_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "Q";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_R_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "R";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_S_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "S";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_T_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "T";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_U_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "U";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_V_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "V";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_W_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "W";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_X_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "X";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_Y_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "Y";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_Z_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "Z";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        private void lnkMaterial_All_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _letter = "All";
            dgvMaterial_All.DataSource = "";
            fnBindData_All(); fnLinkColor();
        }

        public void fnLinkColor()
        {
            switch (_letter)
            {
                case "123":
                    lnkMaterial_Hash.LinkColor = Color.Red;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "A":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Red;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "B":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Red;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "C":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Red;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "D":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Red;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "E":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Red;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "F":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Red;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "G":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Red;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "H":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Red;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "I":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Red;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "J":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Red;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "K":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Red;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "L":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Red;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "M":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Red;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "N":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Red;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "O":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Red;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "P":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Red;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "Q":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Red;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "R":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Red;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "S":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Red;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "T":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Red;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "U":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Red;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "V":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Red;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "W":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Red;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "X":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Red;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "Y":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Red;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "Z":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Red;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
                case "All":
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Red;
                    break;
                default:
                    lnkMaterial_Hash.LinkColor = Color.Blue;
                    lnkMaterial_A.LinkColor = Color.Blue;
                    lnkMaterial_B.LinkColor = Color.Blue;
                    lnkMaterial_C.LinkColor = Color.Blue;
                    lnkMaterial_D.LinkColor = Color.Blue;
                    lnkMaterial_E.LinkColor = Color.Blue;
                    lnkMaterial_F.LinkColor = Color.Blue;
                    lnkMaterial_G.LinkColor = Color.Blue;
                    lnkMaterial_H.LinkColor = Color.Blue;
                    lnkMaterial_I.LinkColor = Color.Blue;
                    lnkMaterial_J.LinkColor = Color.Blue;
                    lnkMaterial_K.LinkColor = Color.Blue;
                    lnkMaterial_L.LinkColor = Color.Blue;
                    lnkMaterial_M.LinkColor = Color.Blue;
                    lnkMaterial_N.LinkColor = Color.Blue;
                    lnkMaterial_O.LinkColor = Color.Blue;
                    lnkMaterial_P.LinkColor = Color.Blue;
                    lnkMaterial_Q.LinkColor = Color.Blue;
                    lnkMaterial_R.LinkColor = Color.Blue;
                    lnkMaterial_S.LinkColor = Color.Blue;
                    lnkMaterial_T.LinkColor = Color.Blue;
                    lnkMaterial_U.LinkColor = Color.Blue;
                    lnkMaterial_V.LinkColor = Color.Blue;
                    lnkMaterial_W.LinkColor = Color.Blue;
                    lnkMaterial_X.LinkColor = Color.Blue;
                    lnkMaterial_Y.LinkColor = Color.Blue;
                    lnkMaterial_Z.LinkColor = Color.Blue;
                    lnkMaterial_All.LinkColor = Color.Blue;
                    break;
            }
        }

        private void txtMaterial_Filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgvMaterial_All, txtMaterial_Filter, 2);
            fnBindData_All_Color();
        }



        #endregion


        #region SCAN-OUT STATUS

        public void initScanOut_Spare_Status()
        {
            //initScanOut_Spare_Status_List();

            dtp_ScanOut_Spare_DateFr.MinDate = dtp_ScanOut_Spare_DateTo.MinDate = DateTime.Now.AddYears(-1);
            dtp_ScanOut_Spare_DateFr.MaxDate = dtp_ScanOut_Spare_DateTo.MaxDate = DateTime.Now;

            lbl_total_out.Text = "";

            txt_ScanOut_Spare_Filter.Text = "";
            txt_ScanOut_Spare_Filter.Enabled = false;

            dgvScanOut_Spare_List.DataSource = null;
        }

        public void initScanOut_Spare_Status_List()
        {
            int listCount = 0;
            DateTime dateFr = dtp_ScanOut_Spare_DateFr.Value;
            DateTime dateTo = dtp_ScanOut_Spare_DateTo.Value;

            string sql = "V2o1_BASE_SubMaterial_Init_BindingData_ScanOutStatus_SelItem_V2o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
            
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Date;
            sParams[0].ParameterName = "@dateFr";
            sParams[0].Value = dateFr;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Date;
            sParams[1].ParameterName = "@dateTo";
            sParams[1].Value = dateTo;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgvScanOut_Spare_List_Width = cls.fnGetDataGridWidth(dgvScanOut_Spare_List);
            dgvScanOut_Spare_List.DataSource = dt;

            dgvScanOut_Spare_List.Columns[0].Width = 5 * dgvScanOut_Spare_List_Width / 100;    // No.
            dgvScanOut_Spare_List.Columns[1].Width = 10 * dgvScanOut_Spare_List_Width / 100;    // rsbmDate.
            //dgvScanOut_Spare_List.Columns[2].Width = 5 * dgvScanOut_Spare_List_Width / 100;    // rsbmCode.
            //dgvScanOut_Spare_List.Columns[3].Width = 5 * dgvScanOut_Spare_List_Width / 100;    // rsbmReceiveIDx.
            dgvScanOut_Spare_List.Columns[4].Width = 15 * dgvScanOut_Spare_List_Width / 100;    // rsbmReceiver.
            //dgvScanOut_Spare_List.Columns[5].Width = 5 * dgvScanOut_Spare_List_Width / 100;    // rsbmPurpose.
            dgvScanOut_Spare_List.Columns[6].Width = 25 * dgvScanOut_Spare_List_Width / 100;    // matName.
            dgvScanOut_Spare_List.Columns[7].Width = 5 * dgvScanOut_Spare_List_Width / 100;    // matUnit.
            dgvScanOut_Spare_List.Columns[8].Width = 5 * dgvScanOut_Spare_List_Width / 100;    // matQty.
            dgvScanOut_Spare_List.Columns[9].Width = 20 * dgvScanOut_Spare_List_Width / 100;    // rsbmReason.
            dgvScanOut_Spare_List.Columns[10].Width = 15 * dgvScanOut_Spare_List_Width / 100;    // rsbmReason.

            dgvScanOut_Spare_List.Columns[0].Visible = true;
            dgvScanOut_Spare_List.Columns[1].Visible = true;
            dgvScanOut_Spare_List.Columns[2].Visible = false;
            dgvScanOut_Spare_List.Columns[3].Visible = false;
            dgvScanOut_Spare_List.Columns[4].Visible = true;
            dgvScanOut_Spare_List.Columns[5].Visible = false;
            dgvScanOut_Spare_List.Columns[6].Visible = true;
            dgvScanOut_Spare_List.Columns[7].Visible = true;
            dgvScanOut_Spare_List.Columns[8].Visible = true;
            dgvScanOut_Spare_List.Columns[9].Visible = true;
            dgvScanOut_Spare_List.Columns[10].Visible = true;

            dgvScanOut_Spare_List.Columns[0].HeaderText = "No.";
            dgvScanOut_Spare_List.Columns[1].HeaderText = "Yêu cầu";
            dgvScanOut_Spare_List.Columns[2].HeaderText = "Mã yêu cầu";
            dgvScanOut_Spare_List.Columns[3].HeaderText = "Mã người nhận";
            dgvScanOut_Spare_List.Columns[4].HeaderText = "Người nhận";
            dgvScanOut_Spare_List.Columns[5].HeaderText = "Mục đích";
            dgvScanOut_Spare_List.Columns[6].HeaderText = "Vật tư";
            dgvScanOut_Spare_List.Columns[7].HeaderText = "ĐVT";
            dgvScanOut_Spare_List.Columns[8].HeaderText = "SL";
            dgvScanOut_Spare_List.Columns[9].HeaderText = "Lý do";
            dgvScanOut_Spare_List.Columns[10].HeaderText = "Ngày xuất";

            dgvScanOut_Spare_List.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvScanOut_Spare_List.Columns[10].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgvScanOut_Spare_List, 11, 30);

            txt_ScanOut_Spare_Filter.Text = "";
            txt_ScanOut_Spare_Filter.Enabled = (listCount > 0) ? true : false;

            initScanOut_Spare_Status_List_Total();
        }

        public void initScanOut_Spare_Status_List_Total()
        {
            string
                qty = "";

            int
                _qty = 0,
                _total = 0;

            foreach(DataGridViewRow row in dgvScanOut_Spare_List.Rows)
            {
                qty = row.Cells[8].Value.ToString();
                _qty = (qty.Length > 0) ? Convert.ToInt32(qty) : 0;

                _total = _total + _qty;
                _qty = 0;
            }

            lbl_total_out.Text = String.Format("TỔNG HÀNG XUẤT: {0}", _total);
        }

        private void btn_ScanOut_Spare_View_Click(object sender, EventArgs e)
        {
            txt_ScanOut_Spare_Filter.Text = "";
            initScanOut_Spare_Status_List();
        }

        private void txt_ScanOut_Spare_Filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgvScanOut_Spare_List, txt_ScanOut_Spare_Filter, 6);
            initScanOut_Spare_Status_List_Total();
        }

        private void lnk_ScanOut_Spare_Copy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cls.fnCopyDatagridviewContent(dgvScanOut_Spare_List, tssMsg);
        }


        #endregion


        #region SCAN-IN STATUS

        public void initScanIn_Spare_Status()
        {
            dtp_ScanIn_Spare_DateFr.MinDate = dtp_ScanIn_Spare_DateTo.MinDate = DateTime.Now.AddYears(-1);
            dtp_ScanIn_Spare_DateFr.MaxDate = dtp_ScanIn_Spare_DateTo.MaxDate = DateTime.Now;

            lbl_total_in.Text = "";

            txt_ScanIn_Spare_Filter.Text = "";
            txt_ScanIn_Spare_Filter.Enabled = false;

            dgvScanIn_Spare_List.DataSource = null;
        }

        public void initScanIn_Spare_Status_List()
        {
            int listCount = 0;
            DateTime dateFr = dtp_ScanIn_Spare_DateFr.Value;
            DateTime dateTo = dtp_ScanIn_Spare_DateTo.Value;

            string sql = "V2o1_BASE_SubMaterial_Init_BindingData_ScanInStatus_SelItem_V2o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Date;
            sParams[0].ParameterName = "@dateFr";
            sParams[0].Value = dateFr;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Date;
            sParams[1].ParameterName = "@dateTo";
            sParams[1].Value = dateTo;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgvScanIn_Spare_List_Width = cls.fnGetDataGridWidth(dgvScanIn_Spare_List);
            dgvScanIn_Spare_List.DataSource = dt;

            dgvScanIn_Spare_List.Columns[0].Width = 5 * dgvScanIn_Spare_List_Width / 100;    // No.
            //dgvScanIn_Spare_List.Columns[1].Width = 10 * dgvScanIn_Spare_List_Width / 100;    // [IDx].
            //dgvScanIn_Spare_List.Columns[2].Width = 5 * dgvScanIn_Spare_List_Width / 100;    // [hoIDx].
            //dgvScanIn_Spare_List.Columns[3].Width = 5 * dgvScanIn_Spare_List_Width / 100;    // [matIDx].
            dgvScanIn_Spare_List.Columns[4].Width = 30 * dgvScanIn_Spare_List_Width / 100;    // [Materials].
            dgvScanIn_Spare_List.Columns[5].Width = 20 * dgvScanIn_Spare_List_Width / 100;    // [Code].
            dgvScanIn_Spare_List.Columns[6].Width = 5 * dgvScanIn_Spare_List_Width / 100;    // [Unit].
            dgvScanIn_Spare_List.Columns[7].Width = 5 * dgvScanIn_Spare_List_Width / 100;    // [Qty].
            dgvScanIn_Spare_List.Columns[8].Width = 20 * dgvScanIn_Spare_List_Width / 100;    // [Vendor].
            dgvScanIn_Spare_List.Columns[9].Width = 15 * dgvScanIn_Spare_List_Width / 100;    // [In stock].

            dgvScanIn_Spare_List.Columns[0].Visible = true;
            dgvScanIn_Spare_List.Columns[1].Visible = false;
            dgvScanIn_Spare_List.Columns[2].Visible = false;
            dgvScanIn_Spare_List.Columns[3].Visible = false;
            dgvScanIn_Spare_List.Columns[4].Visible = true;
            dgvScanIn_Spare_List.Columns[5].Visible = true;
            dgvScanIn_Spare_List.Columns[6].Visible = true;
            dgvScanIn_Spare_List.Columns[7].Visible = true;
            dgvScanIn_Spare_List.Columns[8].Visible = true;
            dgvScanIn_Spare_List.Columns[9].Visible = true;

            dgvScanIn_Spare_List.Columns[0].HeaderText = "No.";
            dgvScanIn_Spare_List.Columns[1].HeaderText = "IDx";
            dgvScanIn_Spare_List.Columns[2].HeaderText = "hoIDx";
            dgvScanIn_Spare_List.Columns[3].HeaderText = "matIDx";
            dgvScanIn_Spare_List.Columns[4].HeaderText = "Tên vật tư";
            dgvScanIn_Spare_List.Columns[5].HeaderText = "Mã vật tư";
            dgvScanIn_Spare_List.Columns[6].HeaderText = "ĐVT";
            dgvScanIn_Spare_List.Columns[7].HeaderText = "SL";
            dgvScanIn_Spare_List.Columns[8].HeaderText = "NCC";
            dgvScanIn_Spare_List.Columns[9].HeaderText = "Ngày nhập";

            dgvScanIn_Spare_List.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgvScanIn_Spare_List, 11, 30);

            txt_ScanIn_Spare_Filter.Text = "";
            txt_ScanIn_Spare_Filter.Enabled = (listCount > 0) ? true : false;

            initScanIn_Spare_Status_List_Total();
        }

        public void initScanIn_Spare_Status_List_Total()
        {
            string
                qty = "";

            int
                _qty = 0,
                _total = 0;

            foreach(DataGridViewRow row in dgvScanIn_Spare_List.Rows)
            {
                qty = row.Cells[7].Value.ToString();
                _qty = (qty.Length > 0) ? Convert.ToInt32(qty) : 0;

                _total = _total + _qty;

                _qty = 0;
            }

            lbl_total_in.Text = String.Format("TỔNG HÀNG NHẬP: {0}", _total);
        }

        private void btn_ScanIn_Spare_View_Click(object sender, EventArgs e)
        {
            txt_ScanIn_Spare_Filter.Text = "";
            initScanIn_Spare_Status_List();
        }

        private void txt_ScanIn_Spare_Filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgvScanIn_Spare_List, txt_ScanIn_Spare_Filter, 4);
            initScanIn_Spare_Status_List_Total();
        }

        private void lnk_ScanIn_Spare_Copy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cls.fnCopyDatagridviewContent(dgvScanIn_Spare_List, tssMsg);
        }


        #endregion


        private void tssMsg_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 5000;
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            if (tssMsg.Text.Length > 0)
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
            tssMsg.Text = "";
            timer.Stop();
        }

        public Image ResizedImage(int newWidth, int newHeight, string stPhotoPath)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();
            return bmPhoto;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            
        }
    }
}
