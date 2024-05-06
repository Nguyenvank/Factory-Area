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
    public partial class frmWareshouseInventoryCounting : Form
    {
        public int _dgvCountingItemwidth;
        public int _dgvFGStatusWidth;

        public string _appName = Application.ProductName;


        public frmWareshouseInventoryCounting()
        {
            InitializeComponent();
        }

        private void frmWareshouseInventoryCounting_Load(object sender, EventArgs e)
        {
            _dgvCountingItemwidth = cls.fnGetDataGridWidth(dgvCountingItem) - 20;
            _dgvFGStatusWidth = cls.fnGetDataGridWidth(dgvFGStatus)-20;

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvCountingItemwidth = cls.fnGetDataGridWidth(dgvCountingItem) - 20;
            _dgvFGStatusWidth = cls.fnGetDataGridWidth(dgvFGStatus)-20;

            fnGetDate();
            fnBindDataInventoryStatus();
        }

        public void init()
        {
            fnGetDate();

            cbbShift.Items.Add("DAY");
            cbbShift.Items.Add("NIGHT");
            cbbShift.Items.Insert(0, "");
            cbbShift.SelectedIndex = 0;

            fnBindDataInventoryStatus();
            dgvFGStatus.Columns[11].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            dgvFGStatus.Columns[11].HeaderCell.Style.BackColor = Color.LightSkyBlue;

        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
        }

        public void fnGetInventory()
        {
            DateTime date = dtpCountingDate.Value;
            string shift = cbbShift.Text.ToString();

            string sql = "";
            sql = "V2_BASE_FINISHGOOD_COUNTING_SELECT_ADDNEW";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = date;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@shift";
            sParams[1].Value = shift;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            dgvCountingItem.DataSource = dt;
            dgvCountingItem.Refresh();

            cls.fnFormatDatagridview(dgvCountingItem, 11);

            dgvCountingItem.Columns[0].Visible = true;
            dgvCountingItem.Columns[1].Visible = true;
            dgvCountingItem.Columns[2].Visible = true;
            dgvCountingItem.Columns[3].Visible = false;

            dgvCountingItem.Columns[0].Width = 60 * _dgvCountingItemwidth / 100;
            dgvCountingItem.Columns[1].Width = 25 * _dgvCountingItemwidth / 100;
            dgvCountingItem.Columns[2].Width = 15 * _dgvCountingItemwidth / 100;

            dgvCountingItem.Columns[0].ReadOnly = true;
            dgvCountingItem.Columns[1].ReadOnly = false;
            dgvCountingItem.Columns[2].ReadOnly = true;

        }

        private void cbbShift_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbShift.SelectedIndex > 0)
            {
                fnGetInventory();
            }
            else
            {
                dgvCountingItem.DataSource = "";
                dgvCountingItem.Refresh();
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            cbbShift.SelectedIndex = 0;

            //fnGetInventory();
            dgvCountingItem.DataSource = "";
            dgvCountingItem.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvCountingItem.Rows)
                {
                    DateTime date = dtpCountingDate.Value;
                    string shift = cbbShift.Text;
                    string invent = row.Cells[1].Value.ToString();
                    string prodId = row.Cells[3].Value.ToString();
                    DateTime dateInsert = (shift.ToLower() == "day") ? new DateTime(date.Year, date.Month, date.Day, 19, 59, 59) : new DateTime(date.Year, date.Month, date.Day, 7, 59, 59).AddDays(1);

                    string sql = "V2_BASE_FINISHGOOD_COUNTING_INSUPD_ADDNEW";
                    SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.DateTime;
                    sParams[0].ParameterName = "@date";
                    sParams[0].Value = date.ToShortDateString();

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.VarChar;
                    sParams[1].ParameterName = "@shift";
                    sParams[1].Value = shift;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.VarChar;
                    sParams[2].ParameterName = "@invent";
                    sParams[2].Value = invent;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.VarChar;
                    sParams[3].ParameterName = "@prodId";
                    sParams[3].Value = prodId;

                    cls.fnUpdDel(sql, sParams);
                }
                fnGetInventory();
                fnBindDataInventoryStatus();
            }

        }

        private void dtpCountingDate_ValueChanged(object sender, EventArgs e)
        {
            if (cbbShift.SelectedIndex > 0)
            {
                fnGetInventory();
            }
            else
            {
                dgvCountingItem.DataSource = "";
                dgvCountingItem.Refresh();
            }
        }

        private void dgvCountingItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgvCountingItem.Rows[e.RowIndex];
            dgvCountingItem.DefaultCellStyle.SelectionBackColor = Color.DodgerBlue;
            row.Selected = true;
        }

        public void fnBindDataInventoryStatus()
        {
            string sql = "";
            sql = "V2_BASE_FINISHGOOD_STATUS_MONITORING_ADDNEW";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            dgvFGStatus.DataSource = dt;
            dgvFGStatus.Refresh();

            //dgvFGStatus.DataSource = dt;
            //dgvFGStatus.Refresh();

            //for (int i = 0; i < dgvFGStatus.Rows.Count; i++)
            //{
            //    dgvFGStatus.Rows[i].Height = (dgvFGStatus.Height - dgvFGStatus.Columns[0].HeaderCell.Size.Height) / dgvFGStatus.Rows.Count;
            //}

            dgvFGStatus.Columns[0].Width = 6 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[1].Width = 17 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[2].Width = 10 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[3].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[4].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[5].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[6].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[7].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[8].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[9].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[10].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[11].Width = 7 * _dgvFGStatusWidth / 100;
            dgvFGStatus.Columns[12].Width = 4 * _dgvFGStatusWidth / 100;

            dgvFGStatus.Columns[0].Visible = true;
            dgvFGStatus.Columns[1].Visible = true;
            dgvFGStatus.Columns[2].Visible = true;
            dgvFGStatus.Columns[3].Visible = true;
            dgvFGStatus.Columns[4].Visible = true;
            dgvFGStatus.Columns[5].Visible = true;
            dgvFGStatus.Columns[6].Visible = true;
            dgvFGStatus.Columns[7].Visible = true;
            dgvFGStatus.Columns[8].Visible = true;
            dgvFGStatus.Columns[9].Visible = true;
            dgvFGStatus.Columns[10].Visible = true;
            dgvFGStatus.Columns[12].Visible = true;

            cls.fnFormatDatagridview(dgvFGStatus, 11);

            //dgvFGStatus.RowTemplate.Height = 40;

            dgvFGStatus.Columns[4].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[5].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[6].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[7].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[8].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[9].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[10].DefaultCellStyle.Format = "#,0.###";
            dgvFGStatus.Columns[11].DefaultCellStyle.Format = "#,0.###";


        }

        private void dgvFGStatus_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string partname = "";
            foreach (DataGridViewRow row in dgvFGStatus.Rows)
            {
                partname = row.Cells[3].Value.ToString().ToLower();
                if (partname.Contains("bellows") || partname.Contains("gasket") || partname.Contains("pump"))
                {
                    row.Cells[9].Value = "N/A";
                }

                row.Cells[11].Style.BackColor = Color.LightSkyBlue;
            }
        }
    }
}
