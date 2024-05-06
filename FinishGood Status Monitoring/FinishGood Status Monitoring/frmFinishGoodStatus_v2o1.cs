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
    public partial class frmFinishGoodStatus_v2o1 : Form
    {
        public int _dgvFG_List_Width;

        public DateTime _dt;

        public frmFinishGoodStatus_v2o1()
        {
            InitializeComponent();
        }

        private void frmFinishGoodStatus_v2o1_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgvFG_List_Width = cls.fnGetDataGridWidth(dgvFG_List);
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            dtpDate.MinDate = new DateTime(2018, 10, 3);
            dtpDate.MaxDate = (_dt.Hour >= 8 && _dt.Hour < 20) ? new DateTime(_dt.Year, _dt.Month, _dt.Day) : new DateTime(_dt.Year, _dt.Month, _dt.Day).AddDays(1);
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            fnFG_Bind();
        }

        public void fnFG_Bind()
        {
            try
            {
                DateTime date = dtpDate.Value;

                string sql = "V2o1_BASE_Inventory_FinishGood_EndofDate_List_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.DateTime;
                sParams[0].ParameterName = "@date";
                sParams[0].Value = date;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvFG_List_Width = cls.fnGetDataGridWidth(dgvFG_List);
                dgvFG_List.DataSource = dt;

                //dgvFG_List.Columns[0].Width = 20 * _dgvFG_List_Width / 100;    // idx
                dgvFG_List.Columns[1].Width = 10 * _dgvFG_List_Width / 100;    // inventDate
                //dgvFG_List.Columns[2].Width = 20 * _dgvFG_List_Width / 100;    // prodId
                dgvFG_List.Columns[3].Width = 20 * _dgvFG_List_Width / 100;    // partname
                dgvFG_List.Columns[4].Width = 13 * _dgvFG_List_Width / 100;    // partcode
                dgvFG_List.Columns[5].Width = 8 * _dgvFG_List_Width / 100;    // location
                dgvFG_List.Columns[6].Width = 7 * _dgvFG_List_Width / 100;    // uom
                dgvFG_List.Columns[7].Width = 7 * _dgvFG_List_Width / 100;    // inventoryBeginOfDay
                dgvFG_List.Columns[8].Width = 7 * _dgvFG_List_Width / 100;    // deliveryPlan
                dgvFG_List.Columns[9].Width = 7 * _dgvFG_List_Width / 100;    // produceScanIn
                dgvFG_List.Columns[10].Width = 7 * _dgvFG_List_Width / 100;    // deliveryScanOut
                dgvFG_List.Columns[11].Width = 7 * _dgvFG_List_Width / 100;    // returnOK
                //dgvFG_List.Columns[12].Width = 20 * _dgvFG_List_Width / 100;    // returnNG
                dgvFG_List.Columns[13].Width = 7 * _dgvFG_List_Width / 100;    // inventoryEndOfDay
                //dgvFG_List.Columns[14].Width = 20 * _dgvFG_List_Width / 100;    // [dateadd]

                dgvFG_List.Columns[0].Visible = false;
                dgvFG_List.Columns[1].Visible = true; 
                dgvFG_List.Columns[2].Visible = false;
                dgvFG_List.Columns[3].Visible = true;
                dgvFG_List.Columns[4].Visible = true;
                dgvFG_List.Columns[5].Visible = true;
                dgvFG_List.Columns[6].Visible = true;
                dgvFG_List.Columns[7].Visible = true;
                dgvFG_List.Columns[8].Visible = true;
                dgvFG_List.Columns[9].Visible = true;
                dgvFG_List.Columns[10].Visible = true;
                dgvFG_List.Columns[11].Visible = true;
                dgvFG_List.Columns[12].Visible = false;
                dgvFG_List.Columns[13].Visible = true;
                dgvFG_List.Columns[14].Visible = false;

                dgvFG_List.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
                cls.fnFormatDatagridview(dgvFG_List, 13);
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvFG_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string invent = "";
            int _invent = 0;
            foreach (DataGridViewRow row in dgvFG_List.Rows)
            {
                invent = row.Cells[13].Value.ToString();
                _invent = (invent != "" && invent != null) ? Convert.ToInt32(invent) : 0;

                //row.Cells[13].Style.BackColor = (_invent > 0) ? Color.DodgerBlue : Color.Yellow;
                //row.Cells[13].Style.ForeColor = (_invent > 0) ? Color.White : Color.Black;

                if (_invent > 0)
                {
                    row.Cells[13].Style.BackColor = Color.DodgerBlue;
                    row.Cells[13].Style.ForeColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void dgvFG_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvFG_List.ClearSelection();
                fnFG_Bind();
            }
        }

        private void dgvFG_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
