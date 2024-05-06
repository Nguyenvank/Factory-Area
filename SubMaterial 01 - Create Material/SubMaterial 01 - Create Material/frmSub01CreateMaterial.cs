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
    public partial class frmSub01CreateMaterial : Form
    {
        public DateTime _dt;

        public string _matIDx, _matName, _matCode, _matCate;
        public string _matUnit, _mat1Day, _matSafety, _matStocked;
        public string _matVendor, _matLoc, _matStatus, _matRemark;

        public int _dgvVendorWidth;
        public int _rowVendorListIndex = 0;
        public string _cateIDx;
        public string _vendorIDx;
        public string _msgText;
        public int _msgType;

        public int _dgvMaterial_All_Width;
        public int _dgvMaterial_Shortage_Width;
        public int _dgvMaterial_OutStock_Width;

        public string _prodIDx = "", _prodName = "", _prodCode = "", _prodQty = "", _prodUnit = "";
        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;
        DataView view;

        public int _dgvHandOver_List_Width;
        public int _dgvHandOver_FullList_Width;
        public int _rowHandOver_Index = 0;
        public string _hoIDx = "", _hoCode = "", _hoDate = "", _hoDone = "";


        public frmSub01CreateMaterial()
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

            init_HandOver();

            fnBindData_All();

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


        #region HAND-OVER MATERIAL

        public void init_HandOver()
        {
            init_HandOver_List();
            init_HandOver_Name();

            lblHandOver_Code.Text = "N/A";
            txtHandOver_Qty.Text = "0";
            txtHandOver_Qty.Enabled = false;
            txtHandOver_Note.Enabled = false;

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

            string hoNew = "", hoDone = "";
            bool _hoNew = true, _hoDone = true;
            foreach (DataGridViewRow row in dgvHandOver_FullList.Rows)
            {
                hoNew = row.Cells[4].Value.ToString();
                hoDone = row.Cells[5].Value.ToString();

                _hoNew = (hoNew.ToLower() == "true") ? true : false;
                _hoDone = (hoDone.ToLower() == "true") ? true : false;

                row.DefaultCellStyle.BackColor = (_hoNew == true) ? Color.LightYellow : (_hoDone == true) ? Color.LightGreen : Color.White;
            }

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
                txtHandOver_Note.Enabled = true;
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
                txtHandOver_Note.Enabled = false;
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
            try
            {
                if (prodIDx > 0 && prodQty > 0)
                {
                    if (dgvHandOver_List.Rows.Count > 0)
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            string idx = dr[0].ToString();
                            if (idx == _prodIDx)
                            {
                                int curQty = Convert.ToInt32(dr[3].ToString());
                                int newQty = prodQty + curQty;
                                dr[3] = newQty.ToString();
                                dgvHandOver_List.Refresh();
                                break;
                            }
                            else
                            {
                                table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
                            }
                        }

                        //foreach (DataGridViewRow row in dgvHandOver_List.Rows)
                        //{
                        //    string idx = row.Cells[0].Value.ToString();
                        //    if (idx == _prodIDx)
                        //    {
                        //        int _rowIDx = dgvHandOver_List.CurrentCell.RowIndex;
                        //        int curQty = Convert.ToInt32(row.Cells[3].Value.ToString());
                        //        int newQty = prodQty + curQty;
                        //        row.Cells[3].Value = newQty.ToString();
                        //        dgvHandOver_List.Refresh();
                        //    }
                        //    else
                        //    {
                        //        table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
                        //    }
                        //}
                    }
                    else
                    {
                        table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
                    }
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

            }
            catch
            {

            }
            finally
            {

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
            }
        }

        private void btnHandOver_Finish_Click(object sender, EventArgs e)
        {
            fnHandOver_Done();
        }

        public void fnHandOver_Done()
        {
            dgvHandOver_FullList.ClearSelection();
            dgvHandOver_FullList.Refresh();
            _hoIDx = "";
            _hoCode = "";
            _hoDate = "";
            _hoDone = "";
            cbbHandOver_Name.SelectedIndex = 0;
            lblHandOver_Code.Text = "N/A";
            txtHandOver_Qty.Text = "0";
            txtHandOver_Qty.Enabled = false;
            dgvHandOver_List.DataSource = "";
            dgvHandOver_List.Refresh();
            lnkHandOver_Add.Enabled = false;
            lnkHandOver_Del.Enabled = false;
            txtHandOver_Note.Text = "";
            txtHandOver_Note.Enabled = false;
            btnHandOver_Save.Enabled = false;
            btnHandOver_Finish.Enabled = false;
        }

        private void dgvHandOver_FullList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string hoIDx = "", hoCode = "", hoDate = "", hoQty = "", hoNew = "", hoFinish = "";
            bool _new = true;
            foreach (DataGridViewRow row in dgvHandOver_FullList.Rows)
            {
                hoIDx = row.Cells[0].Value.ToString();
                hoCode = row.Cells[1].Value.ToString();
                hoDate = row.Cells[2].Value.ToString();
                hoQty = row.Cells[3].Value.ToString();
                hoNew = row.Cells[4].Value.ToString();
                hoFinish = row.Cells[5].Value.ToString();

                _new = (hoNew.ToLower() == "true") ? true : false;
                if (_new == true)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void dgvHandOver_FullList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvHandOver_FullList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvHandOver_FullList.Rows[e.RowIndex];

            string hoIDx = row.Cells[0].Value.ToString();
            string hoCode = row.Cells[1].Value.ToString();
            string hoDate = row.Cells[2].Value.ToString();
            string hoDone = row.Cells[5].Value.ToString();

            _hoIDx = hoIDx;
            _hoCode = hoCode;
            _hoDate = hoDate;
            _hoDone = hoDone;
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
            string hoIDx = "", hoCode = "", hoDate = "", hoDone = "";
            hoDone = _hoDone;
            if (hoDone.ToLower() != "true")
            {
                DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    hoIDx = _hoIDx;
                    hoCode = _hoCode;
                    hoDate = _hoDate;

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
                }
            }
            else
            {
                MessageBox.Show("Không xóa được do mục này đã bàn giao thành công.");
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

                lnkMat_Browse.Enabled = false;
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
            int mLoca = cbbMat_Loc.SelectedIndex;
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

        private void lnkMat_Browse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            //dialog.InitialDirectory = @"C:\";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.Title = "Chọn ảnh minh họa cho vật tư";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Encrypt the selected file. I'll do this later. :)
                //fileStream = dialog.OpenFile();
                //MessageBox.Show(dialog.FileName);

                string fleName = Path.GetFullPath(dialog.FileName);
                Image original = Image.FromFile(dialog.FileName);
                int BOXHEIGHT = picMat_Image.Height;
                int BOXWIDTH = picMat_Image.Width;
                float scaleHeight = (float)BOXHEIGHT / (float)original.Height;
                float scaleWidth = (float)BOXWIDTH / (float)original.Width;
                float scale = Math.Min(scaleHeight, scaleWidth);

                Bitmap resizedImage = new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));

                picMat_Image.SizeMode = PictureBoxSizeMode.CenterImage;
                picMat_Image.Image = resizedImage;
                picMat_Image.Tag = original; 

                Image resized = ResizedImage(298, 204, fleName);
                string resizedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Test\\";
                string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                resized.Save(resizedPath + resizedName);
            }
        }

        private void lnkMat_Clear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (picMat_Image.Image != null)
                {
                    picMat_Image.Image.Dispose();
                    picMat_Image.Image = null;
                }
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

        public void fnMat_Finish()
        {
            if (picMat_Image.Image != null)
            {
                picMat_Image.Image.Dispose();
                picMat_Image.Image = null;
            }

            //cbbMat_List.SelectedIndex = 0;
            cbbMat_List.Text = "";
            txtMat_Name.Text = "";
            txtMat_Code.Text = "";
            init_Material_Code();
            cbbMat_Cate.SelectedIndex = 0;
            cbbMat_Loc.SelectedIndex = 0;
            txtMat_1DayUsing.Text = "0";
            txtMat_ReOrder.Text = "0";
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
            btnMat_Finish.Enabled = true;
        }

        public void fnMat_Add()
        {
            string matName = txtMat_Name.Text.Trim();
            string matCode = txtMat_Code.Text.Trim();
            int matCateIDx = Convert.ToInt32(cbbMat_Cate.SelectedValue.ToString());
            string matCateName = cbbMat_Cate.Text;
            int matLocIDx = Convert.ToInt32(cbbMat_Loc.SelectedValue.ToString());
            string matLocName = cbbMat_Loc.Text;
            string mat1Day = txtMat_1DayUsing.Text.Trim();
            decimal matSafety = Convert.ToDecimal(txtMat_ReOrder.Text.Trim());
            string matUnit = cbbMat_Unit.Text;
            int matVendorIDx = Convert.ToInt32(cbbMat_Vendor.SelectedValue.ToString());
            string matVendorName = cbbMat_Vendor.Text;
            string matStatus = (rdbMat_Enabled.Checked) ? "True" : "False";
            string matNote = txtMat_Note.Text.Trim();

            //string msg = "";
            //msg += "matName: " + matName + "\r\n";
            //msg += "matCode: " + matCode + "\r\n";
            //msg += "matCateIDx: " + matCateIDx + "\r\n";
            //msg += "matCateName: " + matCateName + "\r\n";
            //msg += "matLocIDx: " + matLocIDx + "\r\n";
            //msg += "matLocName: " + matLocName + "\r\n";
            //msg += "mat1Day: " + mat1Day + "\r\n";
            //msg += "matSafety: " + matSafety + "\r\n";
            //msg += "matUnit: " + matUnit + "\r\n";
            //msg += "matVendorIDx: " + matVendorIDx + "\r\n";
            //msg += "matVendorName: " + matVendorName + "\r\n";
            //msg += "matStatus: " + matStatus + "\r\n";
            //msg += "matNote: " + matNote + "\r\n";
            //MessageBox.Show(msg);

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_AddItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[13]; // Parameter count

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

            cls.fnUpdDel(sql, sParams);

            _msgText = "Thêm mới vật tư thành công.";
            _msgType = 0;

            try
            {

                //string sqlCheck = "V2o1_BASE_SubMaterial_Init_Material_ListCheck_SelItem_Addnew";
                //SqlParameter[] sParamsCheck = new SqlParameter[1]; // Parameter count

                //sParamsCheck[0] = new SqlParameter();
                //sParamsCheck[0].SqlDbType = SqlDbType.NVarChar;
                //sParamsCheck[0].ParameterName = "@matName";
                //sParamsCheck[0].Value = matName;

                //DataSet ds = new DataSet();
                //ds = cls.ExecuteDataSet(sqlCheck, sParamsCheck);
                //MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
                //if (ds.Tables[0].Rows.Count <= 0)
                //{

                //}
                //else
                //{
                //    _msgText = "Đã có '" + matName + "' trên hệ thống, hãy chọn một tên khác cho vật tư mới.";
                //    _msgType = 1;
                //}
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.ToString());
                _msgText = sqlEx.Source.ToString();
                _msgType = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                _msgText = ex.Source.ToString();
                _msgType = 1;
            }
            finally
            {

            }

            
        }

        public void fnMat_Upd()
        {
            string idx=_matIDx;
            string matName = txtMat_Name.Text.Trim();
            string matCode = txtMat_Code.Text.Trim();
            int matCateIDx = Convert.ToInt32(cbbMat_Cate.SelectedValue.ToString());
            string matCateName = cbbMat_Cate.Text;
            int matLocIDx = Convert.ToInt32(cbbMat_Loc.SelectedValue.ToString());
            string matLocName = cbbMat_Loc.Text;
            string mat1Day = txtMat_1DayUsing.Text.Trim();
            decimal matSafety = Convert.ToDecimal(txtMat_ReOrder.Text.Trim());
            string matUnit = cbbMat_Unit.Text;
            int matVendorIDx = Convert.ToInt32(cbbMat_Vendor.SelectedValue.ToString());
            string matVendorName = cbbMat_Vendor.Text;
            string matStatus = (rdbMat_Enabled.Checked) ? "True" : "False";
            string matNote = txtMat_Note.Text.Trim();

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_UpdItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[14]; // Parameter count

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

            cls.fnUpdDel(sql, sParams);

            _msgText = "Cập nhật vật tư thành công.";
            _msgType = 0;
        }

        public void fnMat_Del()
        {
            string idx = _matIDx;

            string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_DelItem_Addnew";
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
            catch
            {

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
                string sql = "V2o1_BASE_SubMaterial_Init_BindingData_All_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgvMaterial_All_Width = cls.fnGetDataGridWidth(dgvMaterial_All);
                dgvMaterial_All.DataSource = dt;

                //dgvMaterial_All.Columns[0].Width = 10 * _dgvMaterial_All_Width / 100;    // ProdId
                dgvMaterial_All.Columns[1].Width = 25 * _dgvMaterial_All_Width / 100;    // Name
                dgvMaterial_All.Columns[2].Width = 20 * _dgvMaterial_All_Width / 100;    // BarCode
                dgvMaterial_All.Columns[3].Width = 13 * _dgvMaterial_All_Width / 100;    // Category
                dgvMaterial_All.Columns[4].Width = 5 * _dgvMaterial_All_Width / 100;    // Unit
                dgvMaterial_All.Columns[5].Width = 8 * _dgvMaterial_All_Width / 100;    // 1-Day Using
                dgvMaterial_All.Columns[6].Width = 8 * _dgvMaterial_All_Width / 100;    // Safety Stock
                dgvMaterial_All.Columns[7].Width = 8 * _dgvMaterial_All_Width / 100;    // Current Stock
                dgvMaterial_All.Columns[8].Width = 13 * _dgvMaterial_All_Width / 100;    // Vendor
                //dgvMaterial_All.Columns[9].Width = 13 * _dgvMaterial_All_Width / 100;    // DefaultLocationId
                //dgvMaterial_All.Columns[10].Width = 13 * _dgvMaterial_All_Width / 100;    // isActive
                //dgvMaterial_All.Columns[11].Width = 13 * _dgvMaterial_All_Width / 100;    // Note
                //dgvMaterial_All.Columns[11].Width = 13 * _dgvMaterial_All_Width / 100;    // cateIDx
                //dgvMaterial_All.Columns[12].Width = 13 * _dgvMaterial_All_Width / 100;    // lastvendorID

                dgvMaterial_All.Columns[0].Visible = false;
                dgvMaterial_All.Columns[1].Visible = true;
                dgvMaterial_All.Columns[2].Visible = true;
                dgvMaterial_All.Columns[3].Visible = true;
                dgvMaterial_All.Columns[4].Visible = true;
                dgvMaterial_All.Columns[5].Visible = true;
                dgvMaterial_All.Columns[6].Visible = true;
                dgvMaterial_All.Columns[7].Visible = true;
                dgvMaterial_All.Columns[8].Visible = true;
                dgvMaterial_All.Columns[9].Visible = false;
                dgvMaterial_All.Columns[10].Visible = false;
                dgvMaterial_All.Columns[11].Visible = false;
                dgvMaterial_All.Columns[12].Visible = false;
                dgvMaterial_All.Columns[13].Visible = false;

                cls.fnFormatDatagridviewWhite(dgvMaterial_All, 12, 50);

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
                    using1day = row.Cells[5].Value.ToString();
                    safetystock = row.Cells[6].Value.ToString();
                    currentstock = row.Cells[7].Value.ToString();
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

                dgvMaterial_All.BackgroundColor = Color.White;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvMaterial_All_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

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
            cls.fnDatagridClickCell(dgvMaterial_All, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvMaterial_All.Rows[e.RowIndex];

            string idx = row.Cells[0].Value.ToString();
            string name = row.Cells[1].Value.ToString();
            string code = row.Cells[2].Value.ToString();
            string cateName = row.Cells[3].Value.ToString();
            string unit = row.Cells[4].Value.ToString();
            string using1day = row.Cells[5].Value.ToString();
            string safetystock = row.Cells[6].Value.ToString();
            string currentstock = row.Cells[7].Value.ToString();
            string vendorName = row.Cells[8].Value.ToString();
            string location = row.Cells[9].Value.ToString();
            string status = row.Cells[10].Value.ToString();
            string remark = row.Cells[11].Value.ToString();
            string cateIDx = row.Cells[12].Value.ToString();
            string vendorIDx = row.Cells[13].Value.ToString();

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

            rdbMat_Add.Checked = false;
            rdbMat_Add.Enabled = false;
            rdbMat_Upd.Enabled = true;
            rdbMat_Del.Enabled = true;

            btnMat_Save.Enabled = false;
            btnMat_Finish.Enabled = true;
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
            }
        }

        private void lnkMaterial_All_Load_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fnBindData_All();
        }


        #endregion


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
