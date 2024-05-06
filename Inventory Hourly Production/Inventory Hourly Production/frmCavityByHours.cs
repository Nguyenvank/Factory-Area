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
    public partial class frmCavityByHours : Form
    {

        public DateTime _dt;
        public int _dgvDataWidth;

        public frmCavityByHours()
        {
            InitializeComponent();
        }

        private void frmCavityByHours_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgvDataWidth = cls.fnGetDataGridWidth(dgvData);

            init();

            FreezeBand(dgvData.Columns[0]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 1000ms
            _dt = DateTime.Now;

            fnGetdate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // 5000ms
            //fnLoadData();
        }

        public void init()
        {
            fnGetdate();
            fnLoadData();
        }

        public void fnGetdate()
        {
            try
            {
                if(check.IsConnectedToInternet())
                {
                    tssDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                    tssDateTime.ForeColor = Color.Black;
                }
                else
                {
                    tssDateTime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt);
                    tssDateTime.ForeColor = Color.Red;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadData()
        {
            try
            {
                string sql = "V2o1_BASE_Inventory_Hourly_Production_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[0]; // Parameter count

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvDataWidth = cls.fnGetDataGridWidth(dgvData);
                dgvData.DataSource = dt;

                dgvData.Columns[0].Width = 15 * _dgvDataWidth / 100;    // idx
                for(int i = 1; i < dgvData.Columns.Count; i++)
                {
                    dgvData.Columns[i].Width = 150;    // name
                    dgvData.Columns[i].Visible = true;
                }
                dgvData.Columns[0].Visible = true;

                cls.fnFormatDatagridview(dgvData, 15, "horizontal");
            }
            catch
            {
                
            }
            finally
            {

            }
        }

        private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fnLoadData();
        }

        private static void FreezeBand(DataGridViewBand band)
        {
            band.Frozen = true;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.WhiteSmoke;
            band.DefaultCellStyle = style;
        }
    }
}
