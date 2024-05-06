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
    public partial class frmDeliveryStatus : Form
    {
        public string _idx = "", _code = "", _date = "";

        public int _dgvOrdersWidth;
        public int _dgvDeliveryWidth;

        private int rowOrdersIndex = 0;

        public frmDeliveryStatus()
        {
            InitializeComponent();
        }

        private void frmDeliveryStatus_Load(object sender, EventArgs e)
        {
            _dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders);
            _dgvDeliveryWidth = cls.fnGetDataGridWidth(dgvDelivery);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
            fnBindOrders();

        }

        public void fnGetdate()
        {
            if(check.IsConnectedToInternet())
            {
                lblDate.Text = cls.fnGetDate("SD");
                lblTime.Text = cls.fnGetDate("CT");

                lblDate.ForeColor = Color.Black;
                lblTime.ForeColor = Color.Black;
            }
            else
            {
                lblDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                lblTime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);

                lblDate.ForeColor = Color.Red;
                lblTime.ForeColor = Color.Red;
            }
        }

        public void fnBindOrders()
        {
            string sql = "V2o1_BASE_Delivery_Status_Order_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            dgvOrders.DataSource = dt;
            dgvOrders.Refresh();

            _dgvOrdersWidth = cls.fnGetDataGridWidth(dgvOrders);

            dgvOrders.Columns[0].Width = 20 * _dgvOrdersWidth / 100;    // doId
            dgvOrders.Columns[1].Width = 50 * _dgvOrdersWidth / 100;    // doCode
            dgvOrders.Columns[2].Width = 30 * _dgvOrdersWidth / 100;    // doDate

            dgvOrders.Columns[0].Visible = true;
            dgvOrders.Columns[1].Visible = true;
            dgvOrders.Columns[2].Visible = true;

            cls.fnFormatDatagridview(dgvOrders, 12);

        }

        public void fnBindDelivery()
        {
            string orderId = _idx;

            string sql = "V2o1_BASE_Delivery_Status_Order_Detail_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@orderId";
            sParams[0].Value = orderId;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            foreach (DataRow dr in dt.Rows)
            {
                string remain = dr[12].ToString();
                dr[13] = (remain == "0") ? true : false;
            }

            dgvDelivery.DataSource = dt;
            dgvDelivery.Refresh();

            _dgvDeliveryWidth = cls.fnGetDataGridWidth(dgvDelivery);

            //dgvDelivery.Columns[0].Width = 20 * _dgvDeliveryWidth / 100;    // ID
            //dgvDelivery.Columns[1].Width = 50 * _dgvDeliveryWidth / 100;    // PID
            dgvDelivery.Columns[2].Width = 20 * _dgvDeliveryWidth / 100;    // Name
            dgvDelivery.Columns[3].Width = 10 * _dgvDeliveryWidth / 100;    // Code
            dgvDelivery.Columns[4].Width = 5 * _dgvDeliveryWidth / 100;    // To
            dgvDelivery.Columns[5].Width = 6 * _dgvDeliveryWidth / 100;    // Request
            dgvDelivery.Columns[6].Width = 5 * _dgvDeliveryWidth / 100;    // Unit
            dgvDelivery.Columns[7].Width = 6 * _dgvDeliveryWidth / 100;    // Scan out
            dgvDelivery.Columns[8].Width = 6 * _dgvDeliveryWidth / 100;    // Stock LG
            dgvDelivery.Columns[9].Width = 6 * _dgvDeliveryWidth / 100;    // Inventory
            dgvDelivery.Columns[10].Width = 6 * _dgvDeliveryWidth / 100;    // Plan
            dgvDelivery.Columns[11].Width = 6 * _dgvDeliveryWidth / 100;    // Return
            dgvDelivery.Columns[12].Width = 6 * _dgvDeliveryWidth / 100;    // Remain
            dgvDelivery.Columns[13].Width = 5 * _dgvDeliveryWidth / 100;    // Finish
            dgvDelivery.Columns[14].Width = 13 * _dgvDeliveryWidth / 100;    // Note

            dgvDelivery.Columns[0].Visible = false;
            dgvDelivery.Columns[1].Visible = false;
            dgvDelivery.Columns[2].Visible = true;
            dgvDelivery.Columns[3].Visible = true;
            dgvDelivery.Columns[4].Visible = true;
            dgvDelivery.Columns[5].Visible = true;
            dgvDelivery.Columns[6].Visible = true;
            dgvDelivery.Columns[7].Visible = true;
            dgvDelivery.Columns[8].Visible = true;
            dgvDelivery.Columns[9].Visible = true;
            dgvDelivery.Columns[10].Visible = true;
            dgvDelivery.Columns[11].Visible = true;
            dgvDelivery.Columns[12].Visible = true;
            dgvDelivery.Columns[13].Visible = true;
            dgvDelivery.Columns[14].Visible = true;

            cls.fnFormatDatagridview(dgvDelivery, 12);

        }

        private void dgvOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvOrders, e);

            DataGridViewRow row = new DataGridViewRow();
            row = dgvOrders.Rows[e.RowIndex];

            string idx = row.Cells[0].Value.ToString();
            string code = row.Cells[1].Value.ToString();
            string date = row.Cells[2].Value.ToString();

            _idx = idx;
            _code = code;
            _date = date;

            fnBindDelivery();
        }

        private void dgvOrders_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvOrders.Rows[e.RowIndex].Selected = true;
                rowOrdersIndex = e.RowIndex;
                dgvOrders.CurrentCell = dgvOrders.Rows[e.RowIndex].Cells[1];
                contextMenuStrip1.Show(this.dgvOrders, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void refreshThisOrderListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnBindOrders();
        }

        private void dgvDelivery_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string remain = "";
            int _remain = 0;
            foreach (DataGridViewRow row in dgvDelivery.Rows)
            {
                remain = row.Cells[12].Value.ToString();
                _remain = (remain != "" && remain != null) ? Convert.ToInt32(remain) : 0;

                //finish = row.Cells[13].Value.ToString();
                //row.Cells[12].Style.BackColor = (_remain > 0) ? Color.Yellow : Color.LightSkyBlue;
                row.DefaultCellStyle.BackColor = (_remain > 0) ? Color.Yellow : Color.DodgerBlue;
            }
        }

        private void dgvDelivery_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDelivery_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
