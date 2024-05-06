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
    public partial class frmWarehouseInventoryGatheringData : Form
    {
        public string _appName = Application.ProductName;

        public frmWarehouseInventoryGatheringData()
        {
            InitializeComponent();
        }

        private void frmWarehouseInventoryGatheringData_Load(object sender, EventArgs e)
        {
            init();
            fnInventoryGatheringData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
            fnInventoryGatheringData();
            fnCheckTime();
        }

        public void init()
        {
            fnGetDate();
            lblMessage.Text = "";

            DateTime dt = DateTime.Now;
            DateTimePicker dtDay = new DateTimePicker();
            DateTimePicker dtNight = new DateTimePicker();

            dtpDay.Value = new DateTime(dt.Year, dt.Month, dt.Day, 19, 59, 0);
            dtpNight.Value = new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 0);

            dtDay.MinDate = new DateTime(dt.Year, dt.Month, dt.Day, 8, 0, 0);
            dtDay.MaxDate = new DateTime(dt.Year, dt.Month, dt.Day, 19, 59, 59);
            dtNight.MinDate = new DateTime(dt.Year, dt.Month, dt.Day, 20, 0, 0);
            dtNight.MaxDate = new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 59).AddDays(1);

        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
        }

        public void fnCheckTime()
        {
            string shift = cls.fnGetDate("s");
            DateTime dt = DateTime.Now;
            DateTime curTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            DateTime chkTime = (shift.ToUpper() == "DAY") ? dtpDay.Value : dtpNight.Value;

            if (curTime.TimeOfDay == chkTime.TimeOfDay)
            {
                string sql = "V2_BASE_INVENTORY_ENDOFSHIFT_ADDNEW";
                SqlParameter[] sParams = new SqlParameter[0]; // Parameter count

                cls.fnUpdDel(sql, sParams);
                lblMessage.Text = "Inventory of " + curTime.ToShortDateString() + " (" + shift + ") was gathered successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", curTime);
            }
        }

        public void fnInventoryGatheringData()
        {
            DateTime dt = DateTime.Now;
            string shift = cls.fnGetDate("s");
            DateTime curTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            DateTime chkTime = (shift.ToUpper() == "DAY") ? new DateTime(dt.Year, dt.Month, dt.Day, 19, 59, 0) : new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 0).AddDays(1);
            //DateTime chkTime = (shift.ToUpper() == "DAY") ? new DateTime(dt.Year, dt.Month, dt.Day, 19, 11, 00) : new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 59).AddDays(1);

            if (curTime.TimeOfDay == chkTime.TimeOfDay)
            {
                //MessageBox.Show("Execute Store Procedure");
                string sql = "V2_BASE_INVENTORY_ENDOFSHIFT_ADDNEW";
                SqlParameter[] sParams = new SqlParameter[0]; // Parameter count

                //sParams[0] = new SqlParameter();
                //sParams[0].SqlDbType = SqlDbType.DateTime;
                //sParams[0].ParameterName = "@inventDate";
                //sParams[0].Value = curTime.ToShortDateString();

                //sParams[1] = new SqlParameter();
                //sParams[1].SqlDbType = SqlDbType.VarChar;
                //sParams[1].ParameterName = "@inventShift";
                //sParams[1].Value = shift.ToUpper();

                cls.fnUpdDel(sql,sParams);
                //cls.AutoClosingMessageBox("INSERT INVENTORY OK", _appName, 2000);
                lblMessage.Text = "Inventory of " + curTime.ToShortDateString() + " (" + shift + ") was gathered successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", curTime);
            }
        }
    }
}
