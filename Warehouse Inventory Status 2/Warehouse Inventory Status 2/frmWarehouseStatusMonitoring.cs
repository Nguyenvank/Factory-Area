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
    public partial class frmWarehouseStatusMonitoring : Form
    {
        public int _dgvWHStatusWidth;
        public DateTime _datadate;

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

        }

        public void init()
        {
            fnGetDate();

            //cbbWarehouse.Items.Add("WH1 - Resin");
            //cbbWarehouse.Items.Add("WH2 - CKD");
            //cbbWarehouse.Items.Add("WH3 - Rubber");
            //cbbWarehouse.Items.Add("WH4 - Chemical");
            //cbbWarehouse.Items.Add("WH7 - W.I.P");
            cbbWarehouse.Items.Add("WH8 - Recycle");
            //cbbWarehouse.Items.Add("WH9 - Scrap");
            //cbbWarehouse.Items.Add("WH10 - Garbage");
            //cbbWarehouse.Items.Add("WH11 - Stationary");
            cbbWarehouse.Items.Insert(0, "");
            cbbWarehouse.SelectedIndex = 0;
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
            if(cbbWarehouse.SelectedIndex>0)
            {
                string title = "";
                DateTime datadate = _datadate;
                string cateId = cbbWarehouse.SelectedIndex.ToString();
                switch (cbbWarehouse.SelectedIndex)
                {
                    case 1:
                        cateId = "149";     //WH8
                        title = "RECYCLE";
                        break;
                    case 2:
                        cateId = "150";
                        title = "SCRAP";   //WH9
                        break;
                    case 3:
                        cateId = "151";
                        title = "GARBAGE";   //WH10 - Nhap vao voi WH9
                        break;
                        //case 6:
                        //    cateId = "122";
                        //    break;
                        //case 7:
                        //    cateId = "147";
                        //    break;
                }

                lblTitle.Text = title + " INVENTORY STATUS";

                string sql = "";
                sql = "V2_BASE_WAREHOUSE_STATUS_MONITORING2_ADDNEW";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "@cateId";
                sParams[0].Value = cateId;

                //sParams[1] = new SqlParameter();
                //sParams[1].SqlDbType = SqlDbType.DateTime;
                //sParams[1].ParameterName = "@currdate";
                //sParams[1].Value = datadate;


                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);
                //dt = cls.fnSelect(sql);
                ////dt.Select("Sublocation IS NOT NULL");
                //DataRow[] dtr = dt.Select("sublocation<>''");
                //DataTable dt2 = dt.Clone();
                //foreach (DataRow d in dtr)
                //{
                //    dt2.ImportRow(d);
                //}


                dgvWHStatus.DataSource = dt;
                dgvWHStatus.Refresh();

                dgvWHStatus.Columns[0].Width = 10 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[1].Width = 27 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[2].Width = 19 * _dgvWHStatusWidth / 100;
                //dgvWHStatus.Columns[3].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[4].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[5].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[6].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[7].Width = 5 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[8].Width = 15 * _dgvWHStatusWidth / 100;

                dgvWHStatus.Columns[0].Visible = true;
                dgvWHStatus.Columns[1].Visible = true;
                dgvWHStatus.Columns[2].Visible = true;
                dgvWHStatus.Columns[3].Visible = false;
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

                row.Cells[6].Style.BackColor = Color.LightCyan;

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

        private void cbbWarehouse_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                DateTime _date = dtpDataDate.Value;
                _datadate = _date;
                if (cbbWarehouse.SelectedIndex != 6 && cbbWarehouse.SelectedIndex != 7)
                {
                    fnBindDataWarehouseStatus();
                }
                else
                {
                    string promt = Prompt.ShowDialog("Authorizied Permission", "Password", "1");
                    if (promt.ToUpper() == "mmt".ToUpper())
                    {
                        fnBindDataWarehouseStatus();
                    }
                    else
                    {
                        return;
                    }
                    //DialogResult dialogResultAdd = MessageBox.Show("Are you an authorized person?", cls.appName(), MessageBoxButtons.YesNo);
                    //if (dialogResultAdd == DialogResult.Yes)
                    //{
                    //    fnBindDataWarehouseStatus();
                    //}
                    //else
                    //{
                    //    return;
                    //}
                }
            }
        }
    }
}
