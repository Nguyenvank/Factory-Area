using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmWarehouseStatusMonitoring : Form
    {
        public int _dgvWHStatusWidth;

        public frmWarehouseStatusMonitoring()
        {
            InitializeComponent();
        }

        private void frmWarehouseStatusMonitoring_Load(object sender, EventArgs e)
        {

            init();
            if(check.IsConnectedToInternet())
            {
                _dgvWHStatusWidth = cls.fnGetDataGridWidth(dgvWHStatus);
                fnBindDataWarehouseStatus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvWHStatusWidth = cls.fnGetDataGridWidth(dgvWHStatus);
            fnGetDate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                fnBindDataWarehouseStatus();
            }
        }

        public void init()
        {
            fnGetDate();
        }

        public void fnGetDate()
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

        public void fnBindDataWarehouseStatus()
        {
            string sql = "";
            sql = "V2_BASE_WAREHOUSE_STATUS_MONITORING_ADDNEW";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            ////dt.Select("Sublocation IS NOT NULL");
            //DataRow[] dtr = dt.Select("sublocation<>''");
            //DataTable dt2 = dt.Clone();
            //foreach (DataRow d in dtr)
            //{
            //    dt2.ImportRow(d);
            //}

            ////string cate = "", partname = "", partcode = "";
            //string _inventory8 = "", _instock = "", _outstock = "", _inventory = "";
            //int inventory8 = 0, instock = 0, outstock = 0, inventory = 0;
            ////string unit = "", remark = "";

            //////DataTable dgvDT = dt;
            //foreach (DataRow row in dt.Rows)
            //{
            //    _inventory8 = row[3].ToString();
            //    _instock = row[4].ToString();
            //    _outstock = row[5].ToString();
            //    _inventory = row[6].ToString();



            //    if (_instock == null || _instock == "")
            //        _instock = "0";
            //    if (_outstock == null || _outstock == "")
            //        _outstock = "0";
            //    if (_inventory == null || _inventory == "")
            //        _inventory = "0";

            //    instock = (_instock != "0") ? Convert.ToInt32(_instock) : 0;
            //    outstock = (_outstock != "0") ? Convert.ToInt32(_outstock) : 0;
            //    inventory = (_inventory != "0") ? Convert.ToInt32(_inventory) : 0;
            //    inventory8 = (outstock != 0) ? inventory + outstock : inventory;

            //    //    row[3] = inventory8.ToString();
            //    //    row[4] = instock.ToString();
            //    //    row[5] = outstock.ToString();
            //    //    row[6] = inventory.ToString();
            //}
            dgvWHStatus.DataSource = dt;
            dgvWHStatus.Refresh();

            //dgvWHStatus.DataSource = dt;
            //dgvWHStatus.Refresh();

            //for (int i = 0; i < dgvWHStatus.Rows.Count; i++)
            //{
            //    dgvWHStatus.Rows[i].Height = (dgvWHStatus.Height - dgvWHStatus.Columns[0].HeaderCell.Size.Height) / dgvWHStatus.Rows.Count;
            //}

            dgvWHStatus.Columns[0].Width = 8 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[1].Width = 25 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[2].Width = 15 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[3].Width = 8 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[4].Width = 8 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[5].Width = 8 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[6].Width = 8 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[7].Width = 5 * _dgvWHStatusWidth / 100;
            dgvWHStatus.Columns[8].Width = 15 * _dgvWHStatusWidth / 100;

            dgvWHStatus.Columns[0].Visible = true;
            dgvWHStatus.Columns[1].Visible = true;
            dgvWHStatus.Columns[2].Visible = true;
            dgvWHStatus.Columns[3].Visible = true;
            dgvWHStatus.Columns[4].Visible = true;
            dgvWHStatus.Columns[5].Visible = true;
            dgvWHStatus.Columns[6].Visible = true;
            dgvWHStatus.Columns[7].Visible = true;
            dgvWHStatus.Columns[8].Visible = true;

            cls.fnFormatDatagridview(dgvWHStatus, 12);

            dgvWHStatus.RowTemplate.Height = 28;

            dgvWHStatus.Columns[3].DefaultCellStyle.Format = "#,0.###";
            dgvWHStatus.Columns[4].DefaultCellStyle.Format = "#,0.###";
            dgvWHStatus.Columns[5].DefaultCellStyle.Format = "#,0.###";
            dgvWHStatus.Columns[6].DefaultCellStyle.Format = "#,0.###";

            //dgvWHStatus.Columns[3].HeaderText = "Inventory (" + cls.fnGetDate("tsp") + ")";
            //dgvWHStatus.Columns[6].HeaderText = "Inventory (" + cls.fnGetDate("tsn") + ")";

        }

        private void dgvWHStatus_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            fnBindDataWarehouseStatus();
        }

        private void dgvWHStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                fnBindDataWarehouseStatus();
            }
            //cls.fnDatagridClickCell(dgvWHStatus, e);
        }

        private void dgvWHStatus_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //string cate = "", partname = "", partcode = "";
            string _inventory8 = "", _instock = "", _outstock = "", _inventory = "";
            int inventory8 = 0, instock = 0, outstock = 0, inventory = 0;
            //string unit = "", remark = "";

            foreach (DataGridViewRow row in dgvWHStatus.Rows)
            {
                _inventory8 = row.Cells[3].Value.ToString();
                _instock = row.Cells[4].Value.ToString();
                _outstock = row.Cells[5].Value.ToString();
                _inventory = row.Cells[6].Value.ToString();



                if (_instock == null || _instock == "")
                    _instock = "0";
                if (_outstock == null || _outstock == "")
                    _outstock = "0";
                if (_inventory == null || _inventory == "")
                    _inventory = "0";

                instock = (_instock != "0") ? Convert.ToInt32(_instock) : 0;
                outstock = (_outstock != "0") ? Convert.ToInt32(_outstock) : 0;
                inventory = (_inventory != "0") ? Convert.ToInt32(_inventory) : 0;
                inventory8 = (outstock != 0) ? inventory + outstock : inventory;

                row.Cells[6].Style.BackColor = Color.LightSkyBlue;

                if (inventory8 <= 0)
                {
                    row.Cells[3].Style.BackColor = Color.Yellow;
                }
                if (inventory <= 0)
                {
                    row.Cells[6].Style.BackColor = Color.Yellow;
                }
            }
        }
    }
}
