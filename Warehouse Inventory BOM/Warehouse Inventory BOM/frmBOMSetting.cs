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
    public partial class frmBOMSetting : Form
    {
        public string _prodId;
        public string _prodName;
        public string _prodCode;
        public string _prodCate;
        public string _prodLoc;
        public string _prodInvent;

        public string _itemID;
        public string _childID;
        public string _childName;
        public string _childCode;
        public decimal _childQty;
        public string _childUnit;

        public int _dgvProductsWidth;
        public int _dgvLocWidth;
        public int _dgvBOMWidth;

        public byte _save;

        private int rowListIndex = 0;

        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;
        DataView view;

        public frmBOMSetting()
        {
            InitializeComponent();
            _save = 0;
        }

        private void frmBOMSetting_Load(object sender, EventArgs e)
        {
            init();
            fnInitBOMList();
        }

        public void init()
        {
            fnLoadList();
            lblTitle.Text = "BOM SETTING";
            lblBarcode.Text = "";
            lblCate.Text = "N/A";
            lblWH.Text = "N/A";
            lblInventory.Text = "0";
            dgvLoc.DataSource = "";
            dgvBOM.DataSource = "";
            cbbBOM_Item.Enabled = false;
            lblBOM_Code.Text = "N/A";
            btnBOM_Minus.Enabled = false;
            lblBOM_Qty.Text = "0.0000";
            btnBOM_Plus.Enabled = false;
            btnBOM_Add.Enabled = false;
            btnBOM_Remove.Enabled = false;
            lblInfo.Text = "Message";
            btnSave.Enabled = false;
            btnFinish.Enabled = false;
            lblMatBOM_title.Text = "MATERIAL OF BOM";
        }

        public void fnInitBOMList()
        {
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "itemId";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "parentId";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "childId";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Material";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Code";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Qty";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Unit";
            table.Columns.Add(column);
        }

        public void fnLoadList()
        {
            try
            {
                string sql = "V2o1_BASE_BOM_Setting_Product_List_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                dgvProducts.DataSource = dt;

                _dgvProductsWidth = cls.fnGetDataGridWidth(dgvProducts);

                //dgvProducts.Columns[0].Width = 20 * _dgvProductsWidth / 100;    // idx
                dgvProducts.Columns[1].Width = 20 * _dgvProductsWidth / 100;    // category
                dgvProducts.Columns[2].Width = 45 * _dgvProductsWidth / 100;    // name
                dgvProducts.Columns[3].Width = 35 * _dgvProductsWidth / 100;    // code
                //dgvProducts.Columns[4].Width = 20 * _dgvProductsWidth / 100;    // warehouse
                //dgvProducts.Columns[5].Width = 15 * _dgvProductsWidth / 100;    // inventory
                //dgvProducts.Columns[6].Width = 15 * _dgvProductsWidth / 100;    // BOM

                dgvProducts.Columns[0].Visible = false;
                dgvProducts.Columns[1].Visible = true;
                dgvProducts.Columns[2].Visible = true;
                dgvProducts.Columns[3].Visible = true;
                dgvProducts.Columns[4].Visible = false;
                dgvProducts.Columns[5].Visible = false;
                dgvProducts.Columns[6].Visible = false;

                cls.fnFormatDatagridview(dgvProducts, 11);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadLocation()
        {
            try
            {
                string sqlQty = "V2o1_BASE_BOM_Setting_Product_Inventory_SelItem_Addnew";
                DataTable dtQty = new DataTable();

                SqlParameter[] sParamsQty = new SqlParameter[1]; // Parameter count
                sParamsQty[0] = new SqlParameter();
                sParamsQty[0].SqlDbType = SqlDbType.Int;
                sParamsQty[0].ParameterName = "@prodId";
                sParamsQty[0].Value = _prodId;

                dtQty = cls.ExecuteDataTable(sqlQty, sParamsQty);
                dgvLoc.DataSource = dtQty;

                _dgvLocWidth = cls.fnGetDataGridWidth(dgvLoc);

                //dgvLoc.Columns[0].Width = 20 * _dgvLocWidth / 100;    // inoutID
                dgvLoc.Columns[1].Width = 30 * _dgvLocWidth / 100;    // packingID
                dgvLoc.Columns[2].Width = 30 * _dgvLocWidth / 100;    // packingID
                dgvLoc.Columns[3].Width = 25 * _dgvLocWidth / 100;    // packingCode
                dgvLoc.Columns[4].Width = 15 * _dgvLocWidth / 100;    // packingCode

                dgvLoc.Columns[0].Visible = false;
                dgvLoc.Columns[1].Visible = true;
                dgvLoc.Columns[2].Visible = true;
                dgvLoc.Columns[3].Visible = true;
                dgvLoc.Columns[4].Visible = true;

                cls.fnFormatDatagridview(dgvLoc, 15);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadMatItem()
        {
            string prodId = _prodId;
            try
            {
                string sql = "V2o1_BASE_BOM_Setting_MatItem_List_SelItem_Addnew";
                DataTable dt = new DataTable();

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodId";
                sParams[0].Value = prodId;

                dt = cls.ExecuteDataTable(sql, sParams);
                cbbBOM_Item.DataSource = dt;
                cbbBOM_Item.DisplayMember = "Name";
                cbbBOM_Item.ValueMember = "ProdId";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbbBOM_Item.SelectedIndex = 0;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadItemBOM()
        {
            try
            {
                table.Rows.Clear();
                string parentID = _prodId;
                string itemID = "", childID = "", childName = "", childCode = "", childQty = "0.0000", childUnit = "";
                string sql = "V2o1_BASE_BOM_Setting_BOMItem_List_SelItem_Addnew";
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@parentID";
                sParams[0].Value = parentID;

                //dt = cls.ExecuteDataTable(sql, sParams);
                ds = cls.ExecuteDataSet(sql, sParams);
                int dsCount = ds.Tables[0].Rows.Count;
                if (dsCount > 0)
                {
                    for (int i = 0; i < dsCount; i++)
                    {
                        itemID = ds.Tables[0].Rows[i][0].ToString();
                        parentID = ds.Tables[0].Rows[i][1].ToString();
                        childID = ds.Tables[0].Rows[i][2].ToString();
                        childName = ds.Tables[0].Rows[i][3].ToString();
                        childCode = ds.Tables[0].Rows[i][4].ToString();
                        childQty = ds.Tables[0].Rows[i][5].ToString();
                        childUnit = ds.Tables[0].Rows[i][6].ToString();
                        table.Rows.Add(itemID, parentID, childID, childName, childCode, childQty, childUnit);
                    }
                    DataView dsView = new DataView(table);
                    //dt = ds.Tables[0];
                    dgvBOM.DataSource = dsView;

                    _dgvBOMWidth = cls.fnGetDataGridWidth(dgvBOM);

                    //dgvBOM.Columns[0].Width = 20 * _dgvBOMWidth / 100;    // itemID
                    //dgvBOM.Columns[1].Width = 15 * _dgvBOMWidth / 100;    // parentID
                    //dgvBOM.Columns[2].Width = 45 * _dgvBOMWidth / 100;    // childID
                    dgvBOM.Columns[3].Width = 45 * _dgvBOMWidth / 100;    // childName
                    dgvBOM.Columns[4].Width = 25 * _dgvBOMWidth / 100;    // childCode
                    dgvBOM.Columns[5].Width = 15 * _dgvBOMWidth / 100;    // childQty
                    dgvBOM.Columns[6].Width = 15 * _dgvBOMWidth / 100;    // childUnit

                    dgvBOM.Columns[0].Visible = false;
                    dgvBOM.Columns[1].Visible = false;
                    dgvBOM.Columns[2].Visible = false;
                    dgvBOM.Columns[3].Visible = true;
                    dgvBOM.Columns[4].Visible = true;
                    dgvBOM.Columns[5].Visible = true;
                    dgvBOM.Columns[6].Visible = true;


                    cls.fnFormatDatagridview(dgvBOM, 15);
                }
                btnSave.Enabled = (dgvBOM.Rows.Count > 0) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            (dgvProducts.DataSource as DataTable).DefaultView.RowFilter = string.Format("Name LIKE '%{0}%'", txtFilter.Text);
        }

        private void dgvProducts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string bom = "";
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    bom = row.Cells[6].Value.ToString();
                    if (bom == "1")
                    {
                        row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                    }
                    else
                    {

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

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //fnLoadList();
                cls.fnDatagridClickCell(dgvProducts, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvProducts.Rows[e.RowIndex];

                _prodId = row.Cells[0].Value.ToString();
                _prodCate = row.Cells[1].Value.ToString();
                _prodName = row.Cells[2].Value.ToString();
                _prodCode = row.Cells[3].Value.ToString();
                _prodLoc = row.Cells[4].Value.ToString();
                _prodInvent = row.Cells[5].Value.ToString();

                if (_save == 0)
                {
                    fn_dgvProducts_CellClick();
                }
                else
                {
                    DialogResult dialogResultAdd = MessageBox.Show("Your action not save yet\r\nDo you want to continue without saving your changes?", cls.appName(), MessageBoxButtons.YesNo);
                    if (dialogResultAdd == DialogResult.Yes)
                    {
                        //table.Rows.Add("", "", "", "", "", "");
                        table.Rows.Clear();
                        btnSave.Enabled = false;
                        //fnInitBOMList();

                        fn_dgvProducts_CellClick();
                        lblMatBOM_title.Text = "MATERIAL OF BOM";
                        _save = 0;
                    }
                }
                fnLoadItemBOM();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fn_dgvProducts_CellClick()
        {
            lblTitle.Text = _prodName;
            lblBarcode.Text = (_prodCode.Length <= 17) ? _prodCode : cls.Left(_prodCode, 17) + "...";
            lblCate.Text = _prodCate;
            lblWH.Text = _prodLoc;
            lblInventory.Text = _prodInvent;

            cls.showTooltip(lblTitle, "Material/Product name", _prodName);
            cls.showTooltip(lblBarcode, "Material/Product code", _prodCode);

            fnLoadLocation();

            cbbBOM_Item.Enabled = true;
            fnLoadMatItem();

            cbbBOM_Item.SelectedIndex = 0;
            lblBOM_Code.Text = "N/A";
            lblBOM_Qty.Text = "0.0000";
            btnBOM_Minus.Enabled = false;
            btnBOM_Plus.Enabled = false;
            btnBOM_Add.Enabled = false;
            btnBOM_Remove.Enabled = false;

            _childID = "";
            _childName = "";
            _childCode = "";
            _childQty = 0;
            _childUnit = "";

            lblInfo.Text = "Message";

            dgvBOM.DataSource = "";
            btnFinish.Enabled = true;
        }

        private void dgvProducts_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvProducts.Rows[e.RowIndex].Selected = true;
                rowListIndex = e.RowIndex;
                dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[2];
                cmsList.Show(this.dgvProducts, e.Location);
                cmsList.Show(Cursor.Position);
            }
        }

        private void refreshTheListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnLoadList();
        }

        private void cbbBOM_Item_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbBOM_Item.SelectedIndex > 0)
            {
                try
                {
                    string selCode = cbbBOM_Item.SelectedValue.ToString();
                    string disName = "";
                    string disCode = "";
                    string disUnit = "";
                    string disQtyTitle = "";
                    string sql = "V2o1_BASE_BOM_Setting_MatItem_List_Code_SelItem_Addnew";
                    DataSet ds = new DataSet();

                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@prodId";
                    sParams[0].Value = selCode;

                    ds = cls.ExecuteDataSet(sql, sParams);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        disCode = ds.Tables[0].Rows[0][3].ToString();
                        disUnit = ds.Tables[0].Rows[0][5].ToString();
                        disQtyTitle = "Quantity:\r\n(" + disUnit + ")";

                        lblBOM_Qty.Text = "1.0000";
                        btnBOM_Minus.Enabled = true;
                        btnBOM_Plus.Enabled = true;
                        btnBOM_Add.Enabled = true;

                        _childID = selCode;
                        _childName = cbbBOM_Item.Text;
                        _childCode = disCode;
                        _childQty = (lblBOM_Qty.Text != "" && lblBOM_Qty.Text != null) ? Convert.ToDecimal(lblBOM_Qty.Text) : 0;
                        _childUnit = disUnit;
                    }
                    else
                    {
                        disCode = "N/A";
                        disQtyTitle = "Quantity:";

                        lblBOM_Qty.Text = "0.0000";
                        btnBOM_Minus.Enabled = false;
                        btnBOM_Plus.Enabled = false;
                        btnBOM_Add.Enabled = false;
                    }

                    lblBOM_Code.Text = disCode;
                    lblBOM_Qty_Title.Text = disQtyTitle;
                }
                catch
                {

                }
                finally
                {

                }
            }
            else
            {
                lblBOM_Qty.Text = "0";
                btnBOM_Minus.Enabled = false;
                btnBOM_Plus.Enabled = false;
                btnBOM_Add.Enabled = false;
            }
        }

        private void btnBOM_Minus_Click(object sender, EventArgs e)
        {
            try
            {
                string qty = lblBOM_Qty.Text;
                decimal _qty = Convert.ToDecimal(qty);
                decimal _tmp = 0;
                _tmp = (_qty > 0) ? (_qty - 1) : 0;
                lblBOM_Qty.Text = _tmp.ToString();
                btnBOM_Add.Enabled = (_tmp > 0) ? true : false;
                _childQty = (lblBOM_Qty.Text != "" && lblBOM_Qty.Text != null) ? Convert.ToDecimal(lblBOM_Qty.Text) : 0;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnBOM_Plus_Click(object sender, EventArgs e)
        {
            try
            {
                string qty = lblBOM_Qty.Text;
                decimal _qty = Convert.ToDecimal(qty);
                decimal _tmp = 0;
                _tmp = (_qty + 1);
                lblBOM_Qty.Text = _tmp.ToString();
                btnBOM_Add.Enabled = (_tmp > 0) ? true : false;
                _childQty = (lblBOM_Qty.Text != "" && lblBOM_Qty.Text != null) ? Convert.ToDecimal(lblBOM_Qty.Text) : 0;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnBOM_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string parentID = "";
                string childID = "";
                string childName = "";
                string childCode = "";
                decimal childQty = 0;
                string childUnit = "";

                parentID = _prodId;
                childID = _childID;
                childName = _childName;
                childCode = _childCode;
                childQty = _childQty;
                childUnit = _childUnit;

                table.Rows.Add(0, parentID, childID, childName, childCode, childQty, childUnit);
                view = new DataView(table);

                dgvBOM.DataSource = view;

                _dgvBOMWidth = cls.fnGetDataGridWidth(dgvBOM);

                //dgvBOM.Columns[0].Width = 20 * _dgvBOMWidth / 100;    // itemId
                //dgvBOM.Columns[1].Width = 20 * _dgvBOMWidth / 100;    // parentId
                //dgvBOM.Columns[2].Width = 15 * _dgvBOMWidth / 100;    // childID
                dgvBOM.Columns[3].Width = 45 * _dgvBOMWidth / 100;    // childName
                dgvBOM.Columns[4].Width = 25 * _dgvBOMWidth / 100;    // childCode
                dgvBOM.Columns[5].Width = 15 * _dgvBOMWidth / 100;    // childQty
                dgvBOM.Columns[6].Width = 15 * _dgvBOMWidth / 100;    // childUnit

                dgvBOM.Columns[0].Visible = false;
                dgvBOM.Columns[1].Visible = false;
                dgvBOM.Columns[2].Visible = false;
                dgvBOM.Columns[3].Visible = true;
                dgvBOM.Columns[4].Visible = true;
                dgvBOM.Columns[5].Visible = true;
                dgvBOM.Columns[6].Visible = true;


                cls.fnFormatDatagridview(dgvBOM, 15);

                lblInfo.Text = "Add new material item to BOM successful.";

                dgvBOM.Enabled = true;
                cbbBOM_Item.SelectedIndex = 0;
                lblBOM_Code.Text = "N/A";
                lblBOM_Qty.Text = "0";
                btnBOM_Minus.Enabled = false;
                btnBOM_Plus.Enabled = false;
                btnBOM_Add.Enabled = false;
                btnBOM_Remove.Enabled = false;

                _save = 1;
                lblMatBOM_title.Text = "MATERIAL OF BOM *";
                btnSave.Enabled = (dgvBOM.Rows.Count > 0) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvBOM_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvBOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnBOM_Remove.Enabled = true;

                cls.fnDatagridClickCell(dgvBOM, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvBOM.Rows[e.RowIndex];

                string parentID = row.Cells[0].Value.ToString();
                string childID = row.Cells[1].Value.ToString();
                string childName = row.Cells[2].Value.ToString();
                string childCode = row.Cells[3].Value.ToString();
                string childQty = row.Cells[4].Value.ToString();
                string childUnit = row.Cells[5].Value.ToString();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvBOM_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnBOM_Remove_Click(object sender, EventArgs e)
        {
            string itemID = "", parentID = "", childID = "";
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvBOM.SelectedRows)
                {
                    itemID = row.Cells[0].Value.ToString();
                    parentID = row.Cells[1].Value.ToString();
                    childID = row.Cells[2].Value.ToString();

                    if (itemID != "0")
                    {
                        string sql = "V2o1_BASE_BOM_Setting_MatItem_To_BOM_DelItem_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count
                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@itemID";
                        sParams[0].Value = itemID;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@parentID";
                        sParams[1].Value = parentID;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Int;
                        sParams[2].ParameterName = "@childID";
                        sParams[2].Value = childID;

                        cls.fnUpdDel(sql, sParams);
                    }

                    if (!row.IsNewRow)
                        dgvBOM.Rows.Remove(row);
                        
                }
                lblInfo.Text = "The item is removed successful.";
            }
            dgvBOM.ClearSelection();
            btnBOM_Remove.Enabled = false;
            _save = 1;
            lblMatBOM_title.Text = "MATERIAL OF BOM *";
            btnSave.Enabled = (dgvBOM.Rows.Count > 0) ? true : false;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                fnFinish();
            }
        }

        public void fnFinish()
        {
            _prodId = "";
            _prodName = "";
            _prodCode = "";
            _prodCate = "";
            _prodLoc = "";
            _prodInvent = "";

            _childID = "";
            _childName = "";
            _childCode = "";
            _childQty = 0;
            _childUnit = "";

            _save = 0;

            cbbBOM_Item.SelectedIndex = 0;
            lblBOM_Code.Text = "N/A";
            lblBOM_Qty.Text = "0";

            dgvProducts.ClearSelection();
            dgvLoc.ClearSelection();
            dgvBOM.ClearSelection();

            init();
            table.Rows.Clear();
            //fnInitBOMList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                fnAdd();
            }
        }

        public void fnAdd()
        {
            try
            {
                string itemID = "", parentID = "", childID = "", childName = "", childCode = "", childQty = "", childUnit = "";
                string msg = "";
                string sql = "";
                foreach (DataGridViewRow row in dgvBOM.Rows)
                {
                    itemID = row.Cells[0].Value.ToString();
                    parentID = row.Cells[1].Value.ToString();
                    childID = row.Cells[2].Value.ToString();
                    childName = row.Cells[3].Value.ToString();
                    childCode = row.Cells[4].Value.ToString();
                    childQty = row.Cells[5].Value.ToString();
                    childUnit = row.Cells[6].Value.ToString();

                    msg += "parentID: " + parentID + "\r\n";
                    msg += "childID: " + childID + "\r\n";
                    msg += "childName: " + childName + "\r\n";
                    msg += "childCode: " + childCode + "\r\n";
                    msg += "childQty: " + childQty + "\r\n";
                    msg += "childUnit: " + childUnit + "\r\n";
                    msg += "------------------------------\r\n";

                    sql = "V2o1_BASE_BOM_Setting_MatItem_To_BOM_AddItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[7]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@itemID";
                    sParams[0].Value = itemID;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@parentID";
                    sParams[1].Value = parentID;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.Int;
                    sParams[2].ParameterName = "@childID";
                    sParams[2].Value = childID;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.NVarChar;
                    sParams[3].ParameterName = "@childName";
                    sParams[3].Value = childName;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.VarChar;
                    sParams[4].ParameterName = "@childCode";
                    sParams[4].Value = childCode;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.Decimal;
                    sParams[5].ParameterName = "@childQty";
                    sParams[5].Value = childQty;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.VarChar;
                    sParams[6].ParameterName = "@childUnit";
                    sParams[6].Value = childUnit;

                    cls.fnUpdDel(sql, sParams);
                }

                dgvBOM.DataSource = "";
                table.Rows.Clear();
                fnFinish();
                lblInfo.Text = "Material list add to BOM successful.";

                //MessageBox.Show(msg);
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void lblBOM_Qty_TextChanged(object sender, EventArgs e)
        {
            string qty = lblBOM_Qty.Text.Trim();
            if (qty != "" && qty != "0.0000" && qty != null)
            {
                _childQty = Convert.ToDecimal(qty);
            }
            else
            {
                _childQty = 0;
                lblBOM_Qty.Text = "0.0000";
            }
        }
    }
}
