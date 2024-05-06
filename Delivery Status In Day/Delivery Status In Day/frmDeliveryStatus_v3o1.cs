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
    public partial class frmDeliveryStatus_v3o1 : Form
    {
        public int _dgvDelivery_List_Width;

        public DateTime _dt;

        public frmDeliveryStatus_v3o1()
        {
            InitializeComponent();
        }

        private void frmDeliveryStatus_v3o1_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgvDelivery_List_Width = cls.fnGetDataGridWidth(dgvDelivery_List);
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

            dtpDelivery_Date.MinDate = new DateTime(2018, 9, 22);
            dtpDelivery_Date.MaxDate = (_dt.Hour >= 8 && _dt.Hour < 20) ? new DateTime(_dt.Year, _dt.Month, _dt.Day) : new DateTime(_dt.Year, _dt.Month, _dt.Day).AddDays(1);
        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void btnDelivery_Load_Click(object sender, EventArgs e)
        {
            DateTime deliveryDate = dtpDelivery_Date.Value;

            string sql = "V2o1_BASE_Delivery_Status_Order_Detail_V3o1_List_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = deliveryDate;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvDelivery_List_Width = cls.fnGetDataGridWidth(dgvDelivery_List);
            dgvDelivery_List.DataSource = dt;

            //dgvDelivery_List.Columns[0].Width = 20 * _dgvDelivery_List_Width / 100;    // idx
            //dgvDelivery_List.Columns[1].Width = 20 * _dgvDelivery_List_Width / 100;    // deliveryDate
            //dgvDelivery_List.Columns[2].Width = 20 * _dgvDelivery_List_Width / 100;    // orderIDx
            //dgvDelivery_List.Columns[3].Width = 20 * _dgvDelivery_List_Width / 100;    // partIDx
            dgvDelivery_List.Columns[4].Width = 15 * _dgvDelivery_List_Width / 100;    // partName
            dgvDelivery_List.Columns[5].Width = 10 * _dgvDelivery_List_Width / 100;    // partCode
            dgvDelivery_List.Columns[6].Width = 7 * _dgvDelivery_List_Width / 100;    // shipto
            //dgvDelivery_List.Columns[7].Width = 4 * _dgvDelivery_List_Width / 100;    // partUnit
            dgvDelivery_List.Columns[8].Width = 7 * _dgvDelivery_List_Width / 100;    // orderQty
            dgvDelivery_List.Columns[9].Width = 7 * _dgvDelivery_List_Width / 100;    // scanOut
            dgvDelivery_List.Columns[10].Width = 7 * _dgvDelivery_List_Width / 100;    // stockCustomer
            dgvDelivery_List.Columns[11].Width = 7 * _dgvDelivery_List_Width / 100;    // stockDA
            //dgvDelivery_List.Columns[12].Width = 5 * _dgvDelivery_List_Width / 100;    // deliveryPlan
            dgvDelivery_List.Columns[13].Width = 7 * _dgvDelivery_List_Width / 100;    // deliveryRemain
            dgvDelivery_List.Columns[14].Width = 4 * _dgvDelivery_List_Width / 100;    // finish
            dgvDelivery_List.Columns[15].Width = 13 * _dgvDelivery_List_Width / 100;    // lastScan
            dgvDelivery_List.Columns[16].Width = 16 * _dgvDelivery_List_Width / 100;    // remark

            dgvDelivery_List.Columns[0].Visible = false;
            dgvDelivery_List.Columns[1].Visible = false;
            dgvDelivery_List.Columns[2].Visible = false;
            dgvDelivery_List.Columns[3].Visible = false;
            dgvDelivery_List.Columns[4].Visible = true;
            dgvDelivery_List.Columns[5].Visible = true;
            dgvDelivery_List.Columns[6].Visible = true;
            dgvDelivery_List.Columns[7].Visible = false;
            dgvDelivery_List.Columns[8].Visible = true;
            dgvDelivery_List.Columns[9].Visible = true;
            dgvDelivery_List.Columns[10].Visible = true;
            dgvDelivery_List.Columns[11].Visible = true;
            dgvDelivery_List.Columns[12].Visible = false;
            dgvDelivery_List.Columns[13].Visible = true;
            dgvDelivery_List.Columns[14].Visible = true;
            dgvDelivery_List.Columns[15].Visible = true;
            dgvDelivery_List.Columns[16].Visible = true;

            dgvDelivery_List.Columns[15].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridviewWhite(dgvDelivery_List, 12,60);
        }

        private void dgvDelivery_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDelivery_List.Rows)
            {
                string idx = "", date = "", dId = "", pId = "";
                string name = "", code = "", ship = "";
                string doQty = "", doUnit = "", scanOut = "", outstock = "";
                string stockLG = "", stockDA = "", planToday = "", planTomorrow = "";
                string doRemain = "", lastOut = "", doFinish = "", finishNote = "";

                int _doQty = 0, _scanOut = 0, _outstock = 0, _stockLG = 0, _stockDA = 0;
                int _planToday = 0, _planTomorrow = 0, _doRemain = 0;

                idx = row.Cells[0].Value.ToString();
                date = row.Cells[1].Value.ToString();
                dId = row.Cells[2].Value.ToString();
                pId = row.Cells[3].Value.ToString();
                name = row.Cells[4].Value.ToString();
                code = row.Cells[5].Value.ToString();
                ship = row.Cells[6].Value.ToString();
                doUnit = row.Cells[7].Value.ToString();
                doQty = row.Cells[8].Value.ToString();
                scanOut = row.Cells[9].Value.ToString();
                stockLG = row.Cells[10].Value.ToString();
                stockDA = row.Cells[11].Value.ToString();
                planToday = row.Cells[12].Value.ToString();
                //planTomorrow = row.Cells[12].Value.ToString();
                doRemain = row.Cells[13].Value.ToString();
                doFinish = row.Cells[14].Value.ToString();
                lastOut = row.Cells[15].Value.ToString();
                finishNote = row.Cells[16].Value.ToString();

                _scanOut = (scanOut != "" && scanOut != null) ? Convert.ToInt32(scanOut) : 0;
                _doQty = (doQty != "" && doQty != null) ? Convert.ToInt32(doQty) : 0;
                _planToday = (planToday != "" && planToday != null) ? Convert.ToInt32(planToday) : 0;
                _doRemain = (doRemain != "" && doRemain != null) ? Convert.ToInt32(doRemain) : 0;

                //row.Cells[8].Value = _scanOut;
                //row.Cells[13].Value = (_doQty - _scanOut);

                if (doFinish.ToLower() == "true")
                {
                    row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (scanOut == "" || scanOut == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (scanOut == doQty|| _doRemain == 0)
                {
                    row.Cells[14].Value = "True";
                    row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void dgvDelivery_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvDelivery_List.ClearSelection();
            }
        }

        private void dgvDelivery_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
