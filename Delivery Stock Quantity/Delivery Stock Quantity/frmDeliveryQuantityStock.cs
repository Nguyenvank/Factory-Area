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
    public partial class frmDeliveryQuantityStock : Form
    {
        public int _dgvStockWidth;
        public string _appName = Application.ProductName;

        public frmDeliveryQuantityStock()
        {
            InitializeComponent();
        }

        private void frmDeliveryQuantityStock_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            fnBindData();

            lblMessage.Text = "";
        }

        public void fnGetDate()
        {
            lblDateTime.Text = cls.fnGetDate("S") + "\r\n" + cls.fnGetDate("d");

            _dgvStockWidth = cls.fnGetDataGridWidth(dgvStock) - 20;
        }

        public void fnBindData()
        {
            string sql = "";
            sql = "V2_BASE_DELIVERY_STOCK_QUANTITY_ADDNEW";

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql);

            dgvStock.DataSource = dt;
            dgvStock.Refresh();

            cls.fnFormatDatagridview(dgvStock, 11);

            dgvStock.Columns[0].Visible = true;
            dgvStock.Columns[1].Visible = true;
            dgvStock.Columns[2].Visible = true;
            dgvStock.Columns[3].Visible = false;

            dgvStock.Columns[0].Width = 60 * _dgvStockWidth / 100;
            dgvStock.Columns[1].Width = 25 * _dgvStockWidth / 100;
            dgvStock.Columns[2].Width = 15 * _dgvStockWidth / 100;

            dgvStock.Columns[0].ReadOnly = true;
            dgvStock.Columns[1].ReadOnly = true;
            dgvStock.Columns[2].ReadOnly = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvStock.Rows)
                {
                    string prodID= row.Cells[3].Value.ToString();
                    string stock = row.Cells[2].Value.ToString();

                    string sql = "V2_BASE_DELIVERY_STOCK_UPD_QUANTITY_ADDNEW";
                    SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@prodId";
                    sParams[0].Value = prodID;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@stock";
                    sParams[1].Value = stock;

                    cls.fnUpdDel(sql, sParams);

                    lblMessage.Text = "Update success..";
                }
                fnBindData();
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            fnBindData();
            lblMessage.Text = "";
        }

        private void dgvStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgvStock.Rows[e.RowIndex];
            dgvStock.DefaultCellStyle.SelectionBackColor = Color.DodgerBlue;
            row.Selected = true;
        }
    }
}
