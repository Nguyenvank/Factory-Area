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
    public partial class frmWarehouseMaterialRequest : Form
    {
        public string _orderCode, _orderCode2;
        public DateTime _orderDate, _orderDate2;
        public string _department, _department2;
        public string _picName, _picName2;
        public string _picId, _picId2;
        public string _orderNote, _orderNote2;

        public string _matId;
        public string _matName;
        public string _matCode;
        public string _orderQty;
        public string _orderUnit;
        public string _equivQty;
        public string _equivUnit;
        public string _orderReason;

        public int _dgvMaterialWidth;
        public int _dgvOrderWidth;
        public int _dgvLocationWidth;

        private int rowMaterialIndex = 0;
        private int rowOrderIndex = 0;
        private int rowLocationIndex = 0;

        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;
        DataView view;



        public frmWarehouseMaterialRequest()
        {
            InitializeComponent();
        }

        private void frmWarehouseMaterialRequest_Load(object sender, EventArgs e)
        {
            _dgvMaterialWidth = cls.fnGetDataGridWidth(dgvMaterial) - 20;
            _dgvOrderWidth = cls.fnGetDataGridWidth(dgvOrder);
            _dgvLocationWidth = cls.fnGetDataGridWidth(dgvLocation) - 20;

            init();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvMaterialWidth = cls.fnGetDataGridWidth(dgvMaterial) - 20;
            _dgvOrderWidth = cls.fnGetDataGridWidth(dgvOrder);
            _dgvLocationWidth = cls.fnGetDataGridWidth(dgvLocation) - 20;

            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
            fnFinish();
            fnBindOrderList();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "matId";
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

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Qty(C)";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Unit(C)";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Reason";
            table.Columns.Add(column);

        }

        public void fnFinish()
        {
            _orderCode = "";
            _orderDate = DateTime.Now;
            _department = "";
            _picName = "";
            _orderNote = "";

            _orderCode2 = "";
            _orderDate2 = DateTime.Now;
            _department2 = "";
            _picName2 = "";
            _orderNote2 = "";

            _matId = "";
            _matName = "";
            _matCode = "";
            _orderQty = "";
            _orderUnit = "";
            _equivQty = "";
            _equivUnit = "";
            _orderReason = "";

            dgvMaterial.ClearSelection();
            dgvOrder.Enabled = true;
            dgvOrder.DataSource = "";
            dgvOrder.Refresh();

            lblOrderNo.Text = fnOrderNo();
            dtpOrderDate.Value = DateTime.Now;
            dtpOrderDate.Enabled = false;
            cbbPICname.Enabled = false;
            lblMatName.Text = "Part name: N/A";
            lblMatCode.Text = "Part code: N/A";
            lblPackStd.Text = "0";
            txtOrderQty.Text = "0";
            txtOrderQty.Enabled = false;
            lblTotalOrder.Text = "0";

            rdbPCS.Checked = false;
            rdbPCS.Enabled = false;
            rdbBOX.Checked = false;
            rdbBOX.Enabled = false;
            rdbPAK.Checked = false;
            rdbPAK.Enabled = false;
            rdbPAL.Checked = false;
            rdbPAL.Enabled = false;

            lblMessage.Text = "Message";
            btnOrderAdd.Enabled = false;
            btnOrderAdd.BackColor = Color.Gray;
            btnOrderDel.Enabled = false;
            btnOrderDel.BackColor = Color.Gray;
            txtReason.Text = "";
            txtReason.Enabled = false;
            txtNotes.Text = "";
            txtNotes.Enabled = false;
            lblItems.Text = "0";
            rdbAdd.Checked = false;
            rdbAdd.Enabled = false;
            rdbUpd.Checked = false;
            rdbUpd.Enabled = false;
            rdbDel.Checked = false;
            rdbDel.Enabled = false;
            btnSave.Enabled = false;
            btnFinish.Enabled = true;

            fnBindMaterial();
        }

        public void fnGetdate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");

            if(check.IsConnectedToInternet())
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

        public void fnBindMaterial()
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_Request_Material_SelItem_Addnew";
                DataTable dt = new DataTable();

                //SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
                //sParams[0] = new SqlParameter();
                //sParams[0].SqlDbType = SqlDbType.Int;
                //sParams[0].ParameterName = "@orderId";
                //sParams[0].Value = orderIdx;

                //dt = cls.ExecuteDataTable(sql, sParams);
                dt = cls.fnSelect(sql);
                dgvMaterial.DataSource = dt;
                dgvMaterial.Refresh();

                dgvMaterial.Columns[0].Width = 30 * _dgvMaterialWidth / 100;    // [Warehouse]
                dgvMaterial.Columns[1].Width = 70 * _dgvMaterialWidth / 100;    // [Part name]
                //dgvMaterial.Columns[2].Width = 30 * _dgvMaterialWidth / 100;    // [Code]
                //dgvMaterial.Columns[3].Width = 25 * _dgvMaterialWidth / 100;    // [idx]

                dgvMaterial.Columns[0].Visible = true;
                dgvMaterial.Columns[1].Visible = true;
                dgvMaterial.Columns[2].Visible = false;
                dgvMaterial.Columns[3].Visible = false;

                cls.fnFormatDatagridview(dgvMaterial, 11);
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

        private void dgvMaterial_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMaterial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //_orderCode = "";
            //_orderDate = DateTime.Now;
            //_department = "";
            //_picName = "";
            //_orderNote = "";
            _matId = "";
            _matName = "";
            _matCode = "";
            _orderQty = "";
            _orderUnit = "";
            _equivQty = "";
            _equivUnit = "";
            _orderReason = "";


            cls.fnDatagridClickCell(dgvMaterial, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvMaterial.Rows[e.RowIndex];
            string matId = row.Cells[3].Value.ToString();
            string matName = row.Cells[1].Value.ToString();
            string matCode = row.Cells[2].Value.ToString();

            fnBindPIC();

            lblMatName.Text = matName;
            lblMatCode.Text = matCode;

            lblPackStd.Text = "0";
            lblPackStdUnit.Text = "(-)";
            lblTotalOrderUnit.Text = "(-)";
            rdbPCS.Checked = false;
            rdbPCS.Enabled = false;
            rdbBOX.Checked = false;
            rdbBOX.Enabled = false;
            rdbPAK.Checked = false;
            rdbPAK.Enabled = false;
            rdbPAL.Checked = false;
            rdbPAL.Enabled = false;
            txtOrderQty.Text = "0";
            txtOrderQty.Enabled = false;

            _matId = matId;
            _matName = matName;
            _matCode = matCode;
            _orderCode = lblOrderNo.Text;
            _orderCode2 = _orderCode;
            //_picName2 = _picName;

            lblOrderNo.Text = (_orderCode2 == ""||_orderCode2=="N/A") ? fnOrderNo() : _orderCode2;
            dtpOrderDate.Value = (_picName2 == "") ? DateTime.Now : _orderDate2;
            dtpOrderDate.Enabled = (_picName2 == "") ? true : false;
            txtNotes.Text = (_picName2 == "") ? "" : _orderNote2;
            txtNotes.Enabled = (_picName2 == "") ? true : false;
            cbbPICname.Enabled = (_picName2 == "") ? true : false;
            cbbPICname.SelectedValue = (_picId2 == "") ? 0 : Convert.ToInt32(_picId2);
            if (_picName2 != "")
            {
                fnPackStd();
            }
        }

        public void fnFillMatData()
        {
            if (_matId != "")
            {

            }
        }

        public void fnBindPIC()
        {
            string sql = "V2o1_BASE_Warehouse_Material_Request_PIC_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbPICname.DataSource = dt;
            cbbPICname.DisplayMember = "picName";
            cbbPICname.ValueMember = "idx";
            dt.Rows.InsertAt(dt.NewRow(), 0);
        }
        
        private void dgvMaterial_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvMaterial.ClearSelection();
                dgvMaterial.Rows[e.RowIndex].Selected = true;
                rowMaterialIndex = e.RowIndex;
                dgvMaterial.CurrentCell = dgvMaterial.Rows[e.RowIndex].Cells[1];
                contextMenuStrip1.Show(this.dgvMaterial, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }

        }

        private void refreshTheseItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnBindMaterial();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            fnFindMaterial();
        }

        public void fnFindMaterial()
        {
            fnFinish();

            BindingSource bs = new BindingSource();
            bs.DataSource = dgvMaterial.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvMaterial.Columns[2].DataPropertyName + ", System.String) like '%" + txtFilter.Text.Replace("'", "''") + "%'");
            dgvMaterial.DataSource = bs;
        }

        public string fnOrderNo()
        {
            string orderNo = "";
            try
            {
                string currOrderNo = "", nextOrderNo = "";
                int _currOrderNo = 0, _nextOrderNo = 0;
                string sql = "V2o1_BASE_Warehouse_Material_Request_OrderNo_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currOrderNo = ds.Tables[0].Rows[0][0].ToString();
                        string orderCode = cls.Right(currOrderNo, 7);
                        _currOrderNo = (orderCode != "" && orderCode != null) ? Convert.ToInt32(orderCode) : 0;
                        _nextOrderNo = _currOrderNo + 1;
                        orderNo = "MR-" + String.Format("{0:0000000}", _nextOrderNo);
                    }
                    else
                    {
                        currOrderNo = "0";
                        nextOrderNo = "MR-0000001";
                        orderNo = nextOrderNo;
                    }
                }
                else
                {
                    currOrderNo = "0";
                    nextOrderNo = "MR-0000001";
                    orderNo = nextOrderNo;
                }
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
            return orderNo;
        }

        private void cbbPICname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbPICname.SelectedIndex > 0)
            {
                _picName = cbbPICname.Text.ToString();
                _picName2 = cbbPICname.Text.ToString();
                _picId = cbbPICname.SelectedValue.ToString();
                _picId2 = cbbPICname.SelectedValue.ToString();

                txtOrderQty.Text = "0";
                //txtOrderQty.Enabled = true;

                rdbPCS.Checked = false;
                rdbPCS.Enabled = true;
                rdbBOX.Checked = false;
                rdbBOX.Enabled = true;
                rdbPAK.Checked = false;
                rdbPAK.Enabled = true;
                rdbPAL.Checked = false;
                rdbPAL.Enabled = true;


                fnPackStd();

            }
            else
            {
                _picName = "";
                _picName2 = "";
                _picId = "";
                _picId2 = "";

                txtOrderQty.Text = "";
                txtOrderQty.Enabled = false;
                //cbbPackType.SelectedIndex = 0;
            }
        }

        private void txtOrderQty_TextChanged(object sender, EventArgs e)
        {
            if (cls.IsNumeric(txtOrderQty.Text.Trim()))
            {
                fnTotalCalulate();
            }
            else
            {
                txtOrderQty.Text = "0";
            }
        }

        public void fnTotalCalulate()
        {
            string orderQty = txtOrderQty.Text.Trim();
            int __orderQty = 0;

            if (orderQty != "" && orderQty != null && cls.IsNumeric(orderQty) == true)
            {
                __orderQty = Convert.ToInt32(orderQty);
            }
            else
            {
                __orderQty = 0;
            }

            string packStd = lblPackStd.Text;
            int _packStd = (packStd != "" && packStd != null) ? Convert.ToInt32(packStd) : 0;
            int orderTotal = 0;
            if (_packStd > 0 && __orderQty > 0)
            {
                orderTotal = (_packStd * __orderQty);
                lblTotalOrder.Text = orderTotal.ToString();

                btnOrderAdd.Enabled = true;
                btnOrderDel.Enabled = true;

                _orderQty = lblTotalOrder.Text;
                _orderUnit = lblTotalOrderUnit.Text;
                _equivQty = txtOrderQty.Text.Trim();

            }
            else
            {
                lblTotalOrder.Text = "0";

                btnOrderAdd.Enabled = false;
                btnOrderDel.Enabled = false;
            }
        }

        public void fnPackStd()
        {
            string packQty = "", packType = "";
            int _packType = 0;
            string pcs = "", box = "", pak = "", pal = "", unit = "";
            string sql = "V2o1_BASE_Warehouse_Material_Request_PackStd_SelItem_Addnew";
            DataSet ds = new DataSet();
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@matId";
            sParams[0].Value = _matId;

            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pcs = ds.Tables[0].Rows[0][0].ToString();
                    box = ds.Tables[0].Rows[0][1].ToString();
                    pak = ds.Tables[0].Rows[0][2].ToString();
                    pal = ds.Tables[0].Rows[0][3].ToString();
                    unit = ds.Tables[0].Rows[0][4].ToString();
                }
                else
                {
                    pcs = "0";
                    box = "0";
                    pak = "0";
                    pal = "0";
                    unit = "(-)";
                }
            }
            else
            {
                pcs = "0";
                box = "0";
                pak = "0";
                pal = "0";
                unit = "(-)";
            }

            lblPCS.Text = pcs;
            lblBOX.Text = box;
            lblPAK.Text = pak;
            lblPAL.Text = pal;

            rdbPCS.Enabled = (pcs != "0") ? true : false;
            rdbBOX.Enabled = (box != "0") ? true : false;
            rdbPAK.Enabled = (pak != "0") ? true : false;
            rdbPAL.Enabled = (pal != "0") ? true : false;

            lblPCS.Enabled = (pcs != "0") ? true : false;
            lblBOX.Enabled = (box != "0") ? true : false;
            lblPAK.Enabled = (pak != "0") ? true : false;
            lblPAL.Enabled = (pal != "0") ? true : false;

            lblPackStdUnit.Text = "(" + unit.ToLower() + ")";
            lblTotalOrderUnit.Text = "(" + unit.ToLower() + ")";

            if (rdbPCS.Checked)
            {
                _packType = 1;
                packQty = pcs;
                packType = "piece";
            }
            else if (rdbBOX.Checked)
            {
                _packType = 2;
                packQty = box;
                packType = "box";
            }
            else if (rdbPAK.Checked)
            {
                _packType = 3;
                packQty = pak;
                packType = "pack";
            }
            else if (rdbPAL.Checked)
            {
                _packType = 4;
                packQty = pal;
                packType = "pallete";
            }


            switch (_packType)
            {
                case 1:
                    lblPackStd.Text = pcs;
                    break;
                case 2:
                    lblPackStd.Text = box;
                    break;
                case 3:
                    lblPackStd.Text = pak;
                    break;
                case 4:
                    lblPackStd.Text = pal;
                    break;
            }

            //_equivQty = packQty;
            //_equivUnit = packType;
            _equivUnit = packType;
            
            txtOrderQty.Enabled = true;
            fnTotalCalulate();
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                fnFindMaterial();
                txtFilter.Focus();
            }
        }

        private void rdbPCS_CheckedChanged(object sender, EventArgs e)
        {
            fnPackStd();
        }

        private void rdbBOX_CheckedChanged(object sender, EventArgs e)
        {
            fnPackStd();
        }

        private void rdbPAK_CheckedChanged(object sender, EventArgs e)
        {
            fnPackStd();
        }

        private void rdbPAL_CheckedChanged(object sender, EventArgs e)
        {
            fnPackStd();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            fnFinish();
        }

        private void lblTotalOrder_TextChanged(object sender, EventArgs e)
        {
            if (lblTotalOrder.Text != "0")
            {
                txtReason.Text = "";
                txtReason.Enabled = true;

                txtNotes.Text = (_orderNote2 == "") ? "" : _orderNote2;
                txtNotes.Enabled = (_orderNote2 == "") ? true : false;

            }
            else
            {
                txtReason.Text = "";
                txtReason.Enabled = false;

                txtNotes.Text = "";
                txtNotes.Enabled = false;

            }
        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {
            if (txtReason.Text != "" && txtReason.Text != null)
            {
                btnOrderAdd.Enabled = true;
                btnOrderDel.Enabled = false;

                btnOrderAdd.BackColor = Color.DodgerBlue;
                _orderReason = txtReason.Text.Trim();
                btnOrderDel.BackColor = Color.Gray;
            }
            else
            {
                btnOrderAdd.Enabled = false;
                btnOrderDel.Enabled = false;

                btnOrderAdd.BackColor = Color.Gray;
                btnOrderDel.BackColor = Color.Gray;
            }
        }

        private void dgvOrder_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvOrder, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvOrder.Rows[e.RowIndex];
            string idx = row.Cells[0].Value.ToString();
            string matName = row.Cells[1].Value.ToString();
            string matCode = row.Cells[2].Value.ToString();
            string orderQty = row.Cells[3].Value.ToString();
            string orderUnit = row.Cells[4].Value.ToString();
            string equivQty = row.Cells[5].Value.ToString();
            string equivUnit = row.Cells[6].Value.ToString();
            string reason = row.Cells[7].Value.ToString();

            _matId = idx;
            fnPackStd();

            if(_picName2!="")
            {
                lblOrderNo.Text = _orderCode2;
                dtpOrderDate.Value = _orderDate2;
                cbbPICname.SelectedText = _picName2;
            }
            lblMatName.Text = matName;
            lblMatCode.Text = matCode;

            switch (equivUnit.ToLower())
            {
                case "piece":
                    rdbPCS.Checked = true;
                    //rdbPCS.Enabled = true;
                    //lblPCS.Enabled = true;
                    break;
                case "box":
                    rdbBOX.Checked = true;
                    //rdbBOX.Enabled = true;
                    //lblBOX.Enabled = true;
                    break;
                case "pack":
                    rdbPAK.Checked = true;
                    //rdbPAK.Enabled = true;
                    //lblPAK.Enabled = true;
                    break;
                case "pallete":
                    rdbPAL.Checked = true;
                    //rdbPAL.Enabled = true;
                    //lblPAL.Enabled = true;
                    break;
            }
            rdbPCS.Enabled = false;
            lblPCS.Enabled = false;
            rdbBOX.Enabled = false;
            lblBOX.Enabled = false;
            rdbPAK.Enabled = false;
            lblPAK.Enabled = false;
            rdbPAL.Enabled = false;
            lblPAL.Enabled = false;

            lblPackStd.Text = orderQty;
            lblPackStdUnit.Text = orderUnit;
            txtOrderQty.Text = equivQty;
            txtOrderQty.Enabled = false;
            lblTotalOrderUnit.Text = orderUnit;
            txtReason.Text = reason;
            txtReason.Enabled = false;
            txtNotes.Text = _orderNote2;
            txtNotes.Enabled = false;
            btnOrderAdd.Enabled = false;
            btnOrderAdd.BackColor = Color.Gray;
            btnOrderDel.Enabled = true;
            btnOrderDel.BackColor = Color.LightCoral;
        }

        private void dgvOrder_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnOrderDel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvOrder.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgvOrder.Rows.Remove(row);
                }
                lblItems.Text = dgvOrder.Rows.Count.ToString();
                lblMessage.Text = "The item is removed successful.";
            }
            dgvOrder.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                fnAdd();
            }
            fnBindOrderList();
            System.Windows.Forms.Application.Restart();
        }

        private void lblItems_TextChanged(object sender, EventArgs e)
        {
            if(lblItems.Text!="0")
            {
                rdbAdd.Enabled = true;
                rdbAdd.Checked = true;
                rdbUpd.Enabled = false;
                rdbUpd.Checked = false;
                rdbDel.Enabled = false;
                rdbDel.Checked = false;

                btnSave.Enabled = true;
            }
            else
            {
                rdbAdd.Enabled = false;
                rdbAdd.Checked = false;
                rdbUpd.Enabled = false;
                rdbUpd.Checked = false;
                rdbDel.Enabled = false;
                rdbDel.Checked = false;

                btnSave.Enabled = false;
            }
        }

        private void dgvLocation_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvLocation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvLocation, e);
            //DataGridViewRow row = new DataGridViewRow();
            //row = dgvLocation.Rows[e.RowIndex];
            //string orderId = row.Cells[0].Value.ToString();
            //string orderCode = row.Cells[1].Value.ToString();
            //string sql = "V2o1_BASE_Warehouse_Material_Request_OrderDetail_SelItem_Addnew";
            //DataTable dt = new DataTable();
            //SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@orderId";
            //sParams[0].Value = orderId;

            //dt = cls.ExecuteDataTable(sql, sParams);
            //dgvOrder.DataSource = dt;
            //dgvOrder.Refresh();

            ////dgvOrder.Columns[0].Width = 20 * _dgvOrderWidth / 100;    // matId
            //dgvOrder.Columns[1].Width = 25 * _dgvOrderWidth / 100;    // matName
            //dgvOrder.Columns[2].Width = 20 * _dgvOrderWidth / 100;    // matCode
            //dgvOrder.Columns[3].Width = 10 * _dgvOrderWidth / 100;    // orderQty
            //dgvOrder.Columns[4].Width = 10 * _dgvOrderWidth / 100;    // orderUnit
            //dgvOrder.Columns[5].Width = 10 * _dgvOrderWidth / 100;    // equivQty
            //dgvOrder.Columns[6].Width = 10 * _dgvOrderWidth / 100;    // equivUnit
            //dgvOrder.Columns[7].Width = 15 * _dgvOrderWidth / 100;    // orderReason

            //dgvOrder.Columns[0].Visible = false;
            //dgvOrder.Columns[1].Visible = true;
            //dgvOrder.Columns[2].Visible = true;
            //dgvOrder.Columns[3].Visible = true;
            //dgvOrder.Columns[4].Visible = true;
            //dgvOrder.Columns[5].Visible = true;
            //dgvOrder.Columns[6].Visible = true;
            //dgvOrder.Columns[7].Visible = true;

            //dgvOrder.Enabled = false;

            //cls.fnFormatDatagridview(dgvOrder, 11, 30);

        }

        private void dgvLocation_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvLocation.ClearSelection();
                dgvLocation.Rows[e.RowIndex].Selected = true;
                rowLocationIndex = e.RowIndex;
                dgvLocation.CurrentCell = dgvLocation.Rows[e.RowIndex].Cells[1];
                contextMenuStrip3.Show(this.dgvLocation, e.Location);
                contextMenuStrip3.Show(Cursor.Position);
            }
        }

        private void refreshTheseItemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fnBindOrderList();
        }

        private void dtpOrderDate_ValueChanged(object sender, EventArgs e)
        {
            _orderDate = dtpOrderDate.Value;
        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            _orderNote = (txtNotes.Text != "" && txtNotes.Text != null) ? txtNotes.Text.Trim() : "";
        }

        private void btnOrderAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string orderCode = _orderCode;
                DateTime orderDate = _orderDate;
                string department = _department;
                string picName = _picName;
                string orderNote = _orderNote;

                string matId = _matId;
                string matName = _matName;
                string matCode = _matCode;
                string orderQty = _orderQty;
                string orderUnit = _orderUnit.Replace("(", "").Replace(")", "");
                string equivQty = _equivQty;
                string equivUnit = _equivUnit;
                string orderReason = _orderReason;


                table.Rows.Add(matId, matName, matCode, orderQty, orderUnit, equivQty, equivUnit, orderReason);
                view = new DataView(table);

                dgvOrder.DataSource = view;
                dgvOrder.Refresh();

                //dgvOrder.Columns[0].Width = 20 * _dgvOrderWidth / 100;    // matId
                dgvOrder.Columns[1].Width = 25 * _dgvOrderWidth / 100;    // matName
                dgvOrder.Columns[2].Width = 20 * _dgvOrderWidth / 100;    // matCode
                dgvOrder.Columns[3].Width = 10 * _dgvOrderWidth / 100;    // orderQty
                dgvOrder.Columns[4].Width = 10 * _dgvOrderWidth / 100;    // orderUnit
                dgvOrder.Columns[5].Width = 10 * _dgvOrderWidth / 100;    // equivQty
                dgvOrder.Columns[6].Width = 10 * _dgvOrderWidth / 100;    // equivUnit
                dgvOrder.Columns[7].Width = 15 * _dgvOrderWidth / 100;    // orderReason

                dgvOrder.Columns[0].Visible = false;
                dgvOrder.Columns[1].Visible = true;
                dgvOrder.Columns[2].Visible = true;
                dgvOrder.Columns[3].Visible = true;
                dgvOrder.Columns[4].Visible = true;
                dgvOrder.Columns[5].Visible = true;
                dgvOrder.Columns[6].Visible = true;
                dgvOrder.Columns[7].Visible = true;


                cls.fnFormatDatagridview(dgvOrder, 11, 30);

                _orderCode2 = _orderCode;
                _orderDate2 = _orderDate;
                _department2 = _department;
                _picName2 = _picName;
                _orderNote2 = _orderNote;

                _matId = "";
                _matName = "";
                _matCode = "";
                _orderQty = "";
                _orderUnit = "";
                _equivQty = "";
                _equivUnit = "";
                _orderReason = "";

                dgvMaterial.ClearSelection();

                cbbPICname.Enabled = false;
                lblMatName.Text = "Part name: N/A";
                lblMatCode.Text = "Part code: N/A";

                rdbPCS.Checked = false;
                rdbPCS.Enabled = false;
                rdbBOX.Checked = false;
                rdbBOX.Enabled = false;
                rdbPAK.Checked = false;
                rdbPAK.Enabled = false;
                rdbPAL.Checked = false;
                rdbPAL.Enabled = false;

                lblPCS.Text = "0";
                lblPCS.Enabled = false;
                lblBOX.Text = "0";
                lblBOX.Enabled = false;
                lblPAK.Text = "0";
                lblPAK.Enabled = false;
                lblPAL.Text = "0";
                lblPAL.Enabled = false;

                lblPackStd.Text = "0";
                lblPackStdUnit.Text = "(-)";
                txtOrderQty.Text = "0";
                txtOrderQty.Enabled = false;
                lblTotalOrder.Text = "0";
                lblTotalOrderUnit.Text = "(-)";
                txtReason.Text = "";
                txtReason.Enabled = false;
                txtNotes.Text = _orderNote2;
                txtNotes.Enabled = false;

                btnOrderAdd.Enabled = false;
                btnOrderAdd.BackColor = Color.Gray;
                btnOrderDel.Enabled = false;
                btnOrderDel.BackColor = Color.Gray;

                lblItems.Text = dgvOrder.Rows.Count.ToString();
                lblMessage.Text = "The item " + matName + " has added succcessful.";
            }
            catch (SqlException sql)
            {

            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
            //dgvOrder.Rows.Add(matId, matName, matCode, orderQty, orderUnit, equivQty, equivUnit, orderReason);
        }

        public void fnAdd()
        {
            string orderCode = _orderCode2;
            DateTime orderDate = _orderDate2;
            string department = _department2;
            string picName = _picName2;
            string orderNote = _orderNote2;

            string __matId = "", __matName = "", __matCode = "", __orderQty = "", __orderUnit = "", __equivQty = "", __equivUnit = "", __orderReason = "";
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                __matId = row.Cells[0].Value.ToString();
                __matName = row.Cells[1].Value.ToString();
                __matCode = row.Cells[2].Value.ToString();
                __orderQty = row.Cells[3].Value.ToString();
                __orderUnit = row.Cells[4].Value.ToString();
                __equivQty = row.Cells[5].Value.ToString();
                __equivUnit = row.Cells[6].Value.ToString();
                __orderReason = row.Cells[7].Value.ToString();

                //string msg = "";
                //msg += "orderCode: " + orderCode + "\r\n";
                //msg += "orderDate: " + orderDate + "\r\n";
                //msg += "department: " + department + "\r\n";
                //msg += "picName: " + picName + "\r\n";
                //msg += "orderNote: " + orderNote + "\r\n";
                //msg += "----------------------------------\r\n";
                //msg += "matId: " + __matId + "\r\n";
                //msg += "matName: " + __matName + "\r\n";
                //msg += "matCode: " + __matCode + "\r\n";
                //msg += "orderQty: " + __orderQty + "\r\n";
                //msg += "orderUnit: " + __orderUnit + "\r\n";
                //msg += "equivQty: " + __equivQty + "\r\n";
                //msg += "equivUnit: " + __equivUnit + "\r\n";
                //msg += "orderReason: " + __orderReason + "\r\n";
                //MessageBox.Show(msg);

                string sql = "V2o1_BASE_Warehouse_Material_Request_AddItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[13]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "@orderCode";
                sParams[0].Value = orderCode;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.DateTime;
                sParams[1].ParameterName = "@orderDate";
                sParams[1].Value = orderDate;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.VarChar;
                sParams[2].ParameterName = "@department";
                sParams[2].Value = "DONG-A VINA";

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.NVarChar;
                sParams[3].ParameterName = "@picName";
                sParams[3].Value = picName;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.NVarChar;
                sParams[4].ParameterName = "@orderNote";
                sParams[4].Value = orderNote;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.Int;
                sParams[5].ParameterName = "@matId";
                sParams[5].Value = __matId;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.NVarChar;
                sParams[6].ParameterName = "@matName";
                sParams[6].Value = __matName;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.VarChar;
                sParams[7].ParameterName = "@matCode";
                sParams[7].Value = __matCode;

                sParams[8] = new SqlParameter();
                sParams[8].SqlDbType = SqlDbType.SmallMoney;
                sParams[8].ParameterName = "@orderQty";
                sParams[8].Value = __orderQty;

                sParams[9] = new SqlParameter();
                sParams[9].SqlDbType = SqlDbType.VarChar;
                sParams[9].ParameterName = "@orderUnit";
                sParams[9].Value = __orderUnit;

                sParams[10] = new SqlParameter();
                sParams[10].SqlDbType = SqlDbType.SmallMoney;
                sParams[10].ParameterName = "@equivQty";
                sParams[10].Value = __equivQty;

                sParams[11] = new SqlParameter();
                sParams[11].SqlDbType = SqlDbType.VarChar;
                sParams[11].ParameterName = "@equivUnit";
                sParams[11].Value = __equivUnit;

                sParams[12] = new SqlParameter();
                sParams[12].SqlDbType = SqlDbType.NVarChar;
                sParams[12].ParameterName = "@orderReason";
                sParams[12].Value = __orderReason;

                cls.fnUpdDel(sql, sParams);
            }
            dgvOrder.DataSource = "";
            dgvOrder.Refresh();
            fnFinish();
            dtpOrderDate.Value = DateTime.Now;
            cbbPICname.SelectedIndex = 0;

            lblMessage.Text = "Order " + orderCode + " saved at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " successful;";
        }

        public void fnBindOrderList()
        {
            string sql = "V2o1_BASE_Warehouse_Material_Request_OrderList_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            dgvLocation.DataSource = dt;
            dgvLocation.Refresh();

            //dgvLocation.Columns[0].Width = 20 * _dgvLocationWidth / 100;    // idx
            dgvLocation.Columns[1].Width = 35 * _dgvLocationWidth / 100;    // orderCode
            //dgvLocation.Columns[2].Width = 20 * _dgvLocationWidth / 100;    // department
            dgvLocation.Columns[3].Width = 25 * _dgvLocationWidth / 100;    // orderDate
            //dgvLocation.Columns[4].Width = 10 * _dgvLocationWidth / 100;    // orderTime
            //dgvLocation.Columns[5].Width = 10 * _dgvLocationWidth / 100;    // orderNote
            //dgvLocation.Columns[6].Width = 10 * _dgvLocationWidth / 100;    // orderMake
            //dgvLocation.Columns[7].Width = 15 * _dgvLocationWidth / 100;    // orderStatus
            //dgvLocation.Columns[8].Width = 15 * _dgvLocationWidth / 100;    // finish
            //dgvLocation.Columns[9].Width = 15 * _dgvLocationWidth / 100;    // statusNote
            dgvLocation.Columns[10].Width = 40 * _dgvLocationWidth / 100;    // picName
            //dgvLocation.Columns[11].Width = 15 * _dgvLocationWidth / 100;    // picTitle

            dgvLocation.Columns[0].Visible = false;
            dgvLocation.Columns[1].Visible = true;
            dgvLocation.Columns[2].Visible = false;
            dgvLocation.Columns[3].Visible = true;
            dgvLocation.Columns[4].Visible = false;
            dgvLocation.Columns[5].Visible = false;
            dgvLocation.Columns[6].Visible = false;
            dgvLocation.Columns[7].Visible = false;
            dgvLocation.Columns[8].Visible = false;
            dgvLocation.Columns[9].Visible = false;
            dgvLocation.Columns[10].Visible = true;
            dgvLocation.Columns[11].Visible = false;


            cls.fnFormatDatagridview(dgvLocation, 11);

        }
    }
}
