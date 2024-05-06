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
    public partial class frmWarehouseTotalQuantityCheck : Form
    {
        public frmWarehouseTotalQuantityCheck()
        {
            InitializeComponent();
        }

        private void frmWarehouseTotalQuantityCheck_Load(object sender, EventArgs e)
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
        }

        public void fnGetDate()
        {
            try
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
                    lblDate.Text = String.Format("{0:dd/MM/yyyy}",DateTime.Now);
                    lblTime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);

                    lblDate.ForeColor = Color.Red;
                    lblTime.ForeColor = Color.Red;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnDataLoad()
        {

        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Please wait for generate data from system...";
            byte export;

            //DataGridView dgvs = new DataGridView();
            //dgvs.Visible = false;
            DateTime dtNow = DateTime.Now;

            string sql = "V2o1_BASE_Warehouse_Report_CountAll_SelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[0];
            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            if (dt.Rows.Count > 0)
            {
                dgvWHTotal.DataSource = dt;
                dgvWHTotal.Refresh();
                export=cls.ExportToExcel(dgvWHTotal, "Warehouse " + String.Format("{0:dd.MM.yyyy HH_mm_ss}", dtNow));

                if(export==1)
                {
                    lblMessage.Text = "Export warehouse data successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dtNow);
                    lblMessage.ForeColor = Color.Blue;
                }
                else
                {
                    lblMessage.Text = "Has some issues while generate data, please try again..";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
    }
}
