using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Inventory_Data
{
    public partial class frmWarehouseReportFGInOut : Form
    {
        public int _dgvListwidth;

        public frmWarehouseReportFGInOut()
        {
            InitializeComponent();
        }

        private void frmWarehouseReportFGInOut_Load(object sender, EventArgs e)
        {
            _dgvListwidth = cls.fnGetDataGridWidth(dgvList) - 20;

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvListwidth = cls.fnGetDataGridWidth(dgvList) - 20;

            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            chkToDate.Checked = false;
            dtpToDate.Enabled = false;
        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
        }

        private void chkToDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpToDate.Enabled = (chkToDate.Checked) ? true : false;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime dateFr = dtpFromDate.Value;
            DateTime dateTo = dtpToDate.Value;
            string dateRange = (chkToDate.Checked) ? "1" : "0";
            string shiftDay = (rdbShiftDay.Checked) ? "1" : "0";
            string shiftNight = (rdbShiftNight.Checked) ? "1" : "0";
            string scanIn = (chkScanIn.Checked) ? "1" : "0";
            string scanOut = (chkScanOut.Checked) ? "1" : "0";

            string shift = (shiftDay == "1") ? "DAY" : "NIGHT";

            string sql = "V2o1_BASE_Warehouse_Report_FinishGood_ScanInOut_SelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "inventDate";
            sParams[0].Value = dateFr;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "inventShift";
            sParams[1].Value = shift;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            if (DateTime.Now.Subtract(dateFr).TotalDays >= 0 && dt.Rows.Count>0)
            {
                dgvList.DataSource = dt;
                dgvList.Refresh();

                dgvList.Columns[0].Width = 6 * _dgvListwidth / 100;
                dgvList.Columns[1].Width = 16 * _dgvListwidth / 100;
                dgvList.Columns[2].Width = 12 * _dgvListwidth / 100;
                dgvList.Columns[3].Width = 6 * _dgvListwidth / 100;
                dgvList.Columns[4].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[5].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[6].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[7].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[8].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[9].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[10].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[11].Width = 7 * _dgvListwidth / 100;
                dgvList.Columns[12].Width = 5 * _dgvListwidth / 100;

                dgvList.Columns[0].Visible = true;
                dgvList.Columns[1].Visible = true;
                dgvList.Columns[2].Visible = true;
                dgvList.Columns[3].Visible = true;
                dgvList.Columns[4].Visible = true;
                dgvList.Columns[5].Visible = true;
                dgvList.Columns[6].Visible = true;
                dgvList.Columns[7].Visible = true;
                dgvList.Columns[8].Visible = true;
                dgvList.Columns[9].Visible = true;
                dgvList.Columns[10].Visible = true;
                dgvList.Columns[12].Visible = true;

                cls.fnFormatDatagridview(dgvList, 11);

                dgvList.Columns[4].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[5].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[6].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[7].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[8].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[9].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[10].DefaultCellStyle.Format = "#,0.###";
                dgvList.Columns[11].DefaultCellStyle.Format = "#,0.###";

                dgvList.Columns[11].DefaultCellStyle.BackColor = Color.LightSkyBlue;

                btnExcel.Enabled = true;
                //PopulateRows(dgvList);
            }
            else
            {
                dgvList.DataSource = "";
                dgvList.Refresh();

                btnExcel.Enabled = false;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DateTime dateFr = dtpFromDate.Value;
            string shiftDay = (rdbShiftDay.Checked) ? "1" : "0";
            string shiftNight = (rdbShiftNight.Checked) ? "1" : "0";
            string shift = (shiftDay == "1") ? "DAY" : "NIGHT";

            ExportToExcel();
        }

        public void ExportToExcel()
        {
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {

                worksheet = workbook.ActiveSheet;

                worksheet.Name = "ExportedFromDatGrid";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                //Loop through each row and read value from each column. 
                for (int i = 0; i < dgvList.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvList.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvList.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvList.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }
    }
}
