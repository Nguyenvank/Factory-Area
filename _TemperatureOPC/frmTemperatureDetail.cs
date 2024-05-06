using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmTemperatureDetail : Form
    {


        public frmTemperatureDetail()
        {
            InitializeComponent();
        }

        private void frmTemperatureDetail_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            //if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts_Detail.Instance))
            //{
            //    pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts_Detail.Instance);
            //}
            //Ctrl.uc_Temperature_LiveCharts_Detail.Instance.Dock = DockStyle.Fill;
            //Ctrl.uc_Temperature_LiveCharts_Detail.Instance.BringToFront();

            if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts_ByDate.Instance))
            {
                pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts_ByDate.Instance);
            }
            Ctrl.uc_Temperature_LiveCharts_ByDate.Instance.Dock = DockStyle.Fill;
            Ctrl.uc_Temperature_LiveCharts_ByDate.Instance.BringToFront();

        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }
    }
}
