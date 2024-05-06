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
    public partial class frmInventorySafetyUsing : Form
    {
        public DateTime _dt;
        public int _dgvMaterialWidth;


        public frmInventorySafetyUsing()
        {
            InitializeComponent();
        }

        private void frmInventorySafetyUsing_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgvMaterialWidth = cls.fnGetDataGridWidth(dgvMaterial);
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
            init_Warehouse();
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

        public void init_Warehouse()
        {
            try
            {
                string sql = "V2o1_BASE_Inventory_Safety_Using_Warehouse_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                cbbWarehouse.DataSource = dt;
                cbbWarehouse.DisplayMember = "Name";
                cbbWarehouse.ValueMember = "LocationId";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbbWarehouse.SelectedIndex = 0;

            }
            catch
            {

            }
            finally
            {

            }
        }

        private void cbbWarehouse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbWarehouse.SelectedIndex > 0)
            {
                btnLoad.Enabled = true;
            }
            else
            {
                btnLoad.Enabled = false;
                btnSave.Enabled = false;
                btnFinish.Enabled = false;
            }
        }

        public void fnLoadData()
        {
            string whIDx = cbbWarehouse.SelectedValue.ToString();
            string sql = "V2o1_BASE_Inventory_Safety_Using_Material_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whIDx";
            sParams[0].Value = whIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            if (dt.Rows.Count > 0)
            {
                btnSave.Enabled = true;
                btnFinish.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnFinish.Enabled = false;
            }

            dgvMaterial.DataSource = dt;
            _dgvMaterialWidth = cls.fnGetDataGridWidth(dgvMaterial);

            //dgvMaterial.Columns[0].Width = 10 * _dgvMaterialWidth / 100;    // ProdId
            dgvMaterial.Columns[1].Width = 20 * _dgvMaterialWidth / 100;    // Name
            dgvMaterial.Columns[2].Width = 15 * _dgvMaterialWidth / 100;    // Barcode
            dgvMaterial.Columns[3].Width = 5 * _dgvMaterialWidth / 100;    // Uom
            dgvMaterial.Columns[4].Width = 10 * _dgvMaterialWidth / 100;    // Custom1
            dgvMaterial.Columns[5].Width = 10 * _dgvMaterialWidth / 100;    // Custom2
            dgvMaterial.Columns[6].Width = 10 * _dgvMaterialWidth / 100;    // Custom3
            dgvMaterial.Columns[7].Width = 30 * _dgvMaterialWidth / 100;    // Custom4

            dgvMaterial.Columns[0].Visible = false;
            dgvMaterial.Columns[1].Visible = true;
            dgvMaterial.Columns[2].Visible = true;
            dgvMaterial.Columns[3].Visible = true;
            dgvMaterial.Columns[4].Visible = true;
            dgvMaterial.Columns[5].Visible = true;
            dgvMaterial.Columns[6].Visible = true;
            dgvMaterial.Columns[7].Visible = true;

            dgvMaterial.Columns[0].ReadOnly = true;
            dgvMaterial.Columns[1].ReadOnly = true;
            dgvMaterial.Columns[2].ReadOnly = true;
            dgvMaterial.Columns[3].ReadOnly = true;
            dgvMaterial.Columns[4].ReadOnly = false;
            dgvMaterial.Columns[5].ReadOnly = false;
            dgvMaterial.Columns[6].ReadOnly = false;
            dgvMaterial.Columns[7].ReadOnly = false;

            cls.fnFormatDatagridview(dgvMaterial, 11, 45);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            fnLoadData();
        }

        private void dgvMaterial_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMaterial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvMaterial, e);
        }

        private void dgvMaterial_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void dgvMaterial_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvMaterial.CurrentCell.ColumnIndex == 7)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(CheckKey);
            }
            else
            {
                e.Control.KeyPress += new KeyPressEventHandler(CheckKey);
            }
        }

        public void CheckKey(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            cbbWarehouse.SelectedIndex = 0;
            btnLoad.Enabled = false;
            btnSave.Enabled = false;
            btnFinish.Enabled = false;
            dgvMaterial.DataSource = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string matIDx = "", matName = "", matCode = "", matUnit = "", using1Day = "", safety3Days = "", safety14Days = "", usingRemark = "";
                    foreach (DataGridViewRow row in dgvMaterial.Rows)
                    {
                        matIDx = row.Cells[0].Value.ToString();
                        matName = row.Cells[1].Value.ToString();
                        matCode = row.Cells[2].Value.ToString();
                        matUnit = row.Cells[3].Value.ToString();
                        using1Day = row.Cells[4].Value.ToString();
                        safety3Days = row.Cells[5].Value.ToString();
                        safety14Days = row.Cells[6].Value.ToString();
                        usingRemark = row.Cells[7].Value.ToString();

                        string sql = "V2o1_BASE_Inventory_Safety_Using_Warehouse_UpdItem_Addnew";
                        SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@matIDx";
                        sParams[0].Value = matIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.NVarChar;
                        sParams[1].ParameterName = "@using1Day";
                        sParams[1].Value = using1Day;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.NVarChar;
                        sParams[2].ParameterName = "@safety3Days";
                        sParams[2].Value = safety3Days;

                        sParams[3] = new SqlParameter();
                        sParams[3].SqlDbType = SqlDbType.NVarChar;
                        sParams[3].ParameterName = "@safety14Days";
                        sParams[3].Value = safety14Days;

                        sParams[4] = new SqlParameter();
                        sParams[4].SqlDbType = SqlDbType.NVarChar;
                        sParams[4].ParameterName = "@usingRemark";
                        sParams[4].Value = usingRemark;

                        cls.fnUpdDel(sql, sParams);

                        tssMsg.Text = "Cập nhật thành công.";
                    }
                }
                catch
                {
                    tssMsg.Text = "Có lỗi khi cập nhật";
                }
                finally
                {

                }

                fnLoadData();
            }
        }
    }
}
