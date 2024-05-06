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
    public partial class frmInventoryGatheringMaterial : Form
    {
        public DateTime _dt;
        public DateTime _matDayStart;
        public DateTime _matDatEnd;
        public DateTime _proDayStart;
        public DateTime _proDayEnd;

        public frmInventoryGatheringMaterial()
        {
            InitializeComponent();
        }

        private void frmInventoryGatheringMaterial_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetdate();
            fnSetDateTime();

            fnMatExecute();
            fnProExecute();
        }

        public void init()
        {
            fnGetdate();
            fnSetDateTime();

            fnMatExecute();
            fnProExecute();
        }

        public void fnGetdate()
        {
            if (check.IsConnectedToInternet())
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

        public void fnMatExecute()
        {
            DateTime matStart = dtpMatStartDay.Value;
            DateTime timeMatStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, matStart.Hour, matStart.Minute, matStart.Second);
            DateTime checkMatStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, _dt.Hour, _dt.Minute, _dt.Second);
            if (checkMatStart.TimeOfDay == timeMatStart.TimeOfDay)
            {
                string sql = "V2o1_BASE_Inventory_EndofDate_V2o1_Addnew";
                SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
                cls.fnUpdDel(sql, sParams);
                lblMatMsg.Text = "Material inventory of " + _dt.ToShortDateString() + " was gathered successfull.";
                lstHistory.Items.Add("[" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "] - " + lblMatMsg.Text);
            }
            lblMatRemain.Text = "Remain time: " + String.Format("{0:HH:mm:ss}", timeMatStart);
        }

        public void fnProExecute()
        {
            DateTime proStart = dtpProStartDay.Value;
            DateTime timeProStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, proStart.Hour, proStart.Minute, proStart.Second);
            DateTime checkProStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, _dt.Hour, _dt.Minute, _dt.Second);
            if (checkProStart.TimeOfDay == timeProStart.TimeOfDay)
            {
                //string sql = "V2o1_BASE_Inventory_EndofDate_V2o1_Addnew";
                //SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
                //cls.fnUpdDel(sql, sParams);
                lblProMsg.Text = "Finish goods inventory of " + _dt.ToShortDateString() + " was gathered successfull.";
                lstHistory.Items.Add("[" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "] - " + lblProMsg.Text);
            }
            lblProRemain.Text = "Remain time: " + String.Format("{0:HH:mm:ss}", timeProStart);
        }

        public void fnSetDateTime()
        {
            DateTime matTimeStart = dtpMatStartDay.Value;
            DateTime matTimeEnd = dtpMatEndDay.Value;
            DateTime proTimeStart = dtpProStartDay.Value;
            DateTime proTimeEnd = dtpProEndDay.Value;

            DateTime matDayStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, matTimeStart.Hour, matTimeStart.Minute, matTimeStart.Second).AddDays(-1);
            DateTime matDatEnd = new DateTime(_dt.Year, _dt.Month, _dt.Day, matTimeEnd.Hour, matTimeEnd.Minute, matTimeEnd.Second);

            DateTime proDayStart = new DateTime(_dt.Year, _dt.Month, _dt.Day, proTimeStart.Hour, proTimeStart.Minute, proTimeStart.Second).AddDays(-1);
            DateTime proDayEnd = new DateTime(_dt.Year, _dt.Month, _dt.Day, proTimeEnd.Hour, proTimeEnd.Minute, proTimeEnd.Second);

            _matDayStart = matDayStart;
            _matDatEnd = matDatEnd;
            _proDayStart = proDayStart;
            _proDayEnd = proDayEnd;
        }

        private void dtpMatStartDay_ValueChanged(object sender, EventArgs e)
        {
            fnSetDateTime();
        }

        private void dtpMatEndDay_ValueChanged(object sender, EventArgs e)
        {
            fnSetDateTime();
        }

        private void dtpProStartDay_ValueChanged(object sender, EventArgs e)
        {
            fnSetDateTime();
        }

        private void dtpProEndDay_ValueChanged(object sender, EventArgs e)
        {
            fnSetDateTime();
        }

        private void frmInventoryGatheringMaterial_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}
