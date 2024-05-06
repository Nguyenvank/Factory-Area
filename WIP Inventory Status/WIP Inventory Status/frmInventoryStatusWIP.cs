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
    public partial class frmInventoryStatusWIP : Form
    {

        public int _dgvList_Width, _dgvLocate_List_Width, _dgvPacking_List_Width;

        public string _msgText;
        public int _msgType;

        public DateTime _dt;

        public frmInventoryStatusWIP()
        {
            InitializeComponent();
        }

        private void frmInventoryStatusWIP_Load(object sender, EventArgs e)
        {
            _dgvList_Width = cls.fnGetDataGridWidth(dgvList);
            //_dgvLocate_List_Width = cls.fnGetDataGridWidth(dgvLocate_List);
            //_dgvPacking_List_Width = cls.fnGetDataGridWidth(dgvPacking_List);
            _dt = DateTime.Now;
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

            dtpDate.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        #region W.I.P LIST


        public void fnBindList()
        {
            DateTime date = dtpDate.Value;

            string sql = "V2o1_BASE_Inventory_WIP_Inventory_Status_v2o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvList_Width = cls.fnGetDataGridWidth(dgvList);
            dgvList.DataSource = dt;

            //dgvList.Columns[0].Width = 20 * _dgvList_Width / 100;    // ProdId
            dgvList.Columns[1].Width = 30 * _dgvList_Width / 100;    // Name
            dgvList.Columns[2].Width = 20 * _dgvList_Width / 100;    // BarCode
            dgvList.Columns[3].Width = 15 * _dgvList_Width / 100;    // Sublocation
            dgvList.Columns[4].Width = 5 * _dgvList_Width / 100;    // Uom
            dgvList.Columns[5].Width = 10 * _dgvList_Width / 100;    // scanIN
            dgvList.Columns[6].Width = 10 * _dgvList_Width / 100;    // scanOUT
            dgvList.Columns[7].Width = 10 * _dgvList_Width / 100;    // TOTAL

            dgvList.Columns[0].Visible = false;
            dgvList.Columns[1].Visible = true;
            dgvList.Columns[2].Visible = true;
            dgvList.Columns[3].Visible = true;
            dgvList.Columns[4].Visible = true;
            dgvList.Columns[5].Visible = true;
            dgvList.Columns[6].Visible = true;
            dgvList.Columns[7].Visible = true;

            cls.fnFormatDatagridview(dgvList, 12);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            fnBindList();
        }

        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string scanIn = "", scanOut = "";
            int _scanIn = 0, _scanOut = 0;
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                scanIn = row.Cells[5].Value.ToString();
                scanOut = row.Cells[6].Value.ToString();

                _scanIn = (scanIn != "" && scanIn != null) ? Convert.ToInt32(scanIn) : 0;
                _scanOut = (scanOut != "" && scanOut != null) ? Convert.ToInt32(scanOut) : 0;

                if (_scanIn == 0 && _scanOut == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                }
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvList, e);
            }
        }

        private void dgvList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        #endregion


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;

            switch (tab)
            {
                case 0:
                    dgvList.DataSource = "";
                    dgvList.Refresh();
                    break;
                case 1:
                    
                    break;
            }
        }
    }
}
