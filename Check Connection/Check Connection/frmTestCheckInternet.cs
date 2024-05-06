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
    public partial class frmTestCheckInternet : Form
    {
        public frmTestCheckInternet()
        {
            InitializeComponent();
        }

        private void frmTestCheckInternet_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
            fnCheckInternet();
            
        }

        public void init()
        {
            fnGetDate();
        }

        public void fnGetDate()
        {
            lblDateTime.Text = cls.fnGetDate("S") + " " + cls.fnGetDate("dt");
        }

        public void fnCheckInternet()
        {
            string internetStatus = (check.IsConnectedToInternet()) ? "Good" : "Fail";
            string localhostStatus = (check.IsConnectedToLAN("192.168.1.1")) ? "Good" : "Fail";
            string computerName = System.Environment.MachineName;
            string computerIP = check.GetLocalIPAddress();


            lblInternetStatus.Text = internetStatus;
            lblLocalhostStatus.Text = localhostStatus;
            lblMachineName.Text = computerName;
            lblMachineIP.Text = computerIP;

            string sql = "";
            sql = "V2_BASE_TEST_CHECK_CONNECTION_ADDNEW";

            SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.VarChar;
            sParams[0].ParameterName = "@internetStatus";
            sParams[0].Value = internetStatus;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@localhostStatus";
            sParams[1].Value = localhostStatus;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.VarChar;
            sParams[2].ParameterName = "@computerIP";
            sParams[2].Value = computerIP;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.VarChar;
            sParams[3].ParameterName = "@computerName";
            sParams[3].Value = computerName;

            cls.fnUpdDel(sql, sParams);

        }

        private void frmTestCheckInternet_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    notifyIcon1.Visible = true;
            //    //notifyIcon1.ShowBalloonTip(3000);
            //    this.ShowInTaskbar = false;
            //}
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            //this.ShowInTaskbar = true;
            //notifyIcon1.Visible = false;
        }
    }
}
