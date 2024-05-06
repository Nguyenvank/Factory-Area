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
    public partial class frmTemperatureByDate_v2 : Form
    {
        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        public frmTemperatureByDate_v2()
        {
            InitializeComponent();
        }

        public frmTemperatureByDate_v2(frmTemperatureByDateSelected_v2 frmView)
        {
            InitializeComponent();

            frmView.Close();
        }

        private void frmTemperatureByDate_v2_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            cbb_Line.Items.Clear();
            cbb_Line.Items.Add("RUBBER");
            cbb_Line.Items.Add("PLASTIC");
            cbb_Line.Items.Add("ASSEMBLY");
            cbb_Line.Items.Insert(0, "");
            cbb_Line.SelectedIndex = 0;


            dtp_Date.MinDate = new DateTime(2019, 1, 30, 8, 0, 0);
            dtp_Date.MaxDate = DateTime.Now.AddDays(0);

        }

        private void cbb_Line_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int ln = cbb_Line.SelectedIndex;
            if (ln > 0)
            {
                int mc = 0;
                int n = 0;

                cbb_Machine.Items.Clear();
                switch (ln)
                {
                    case 1:
                        mc = 20;
                        for (n = 1; n <= mc; n++) { cbb_Machine.Items.Add("RUBBER " + String.Format("{0:00}", n)); }
                        dtp_Date.MinDate = new DateTime(2019, 1, 30, 8, 0, 0);
                        break;
                    case 2:
                        mc = 12;
                        for (n = 1; n <= mc; n++) { cbb_Machine.Items.Add("PLASTIC " + String.Format("{0:00}", n)); }
                        dtp_Date.MinDate = new DateTime(2019, 4, 1, 8, 0, 0);
                        break;
                    case 3:
                        mc = 6;
                        //for (n = 1; n <= mc; n++) { cbb_Machine.Items.Add("ASSEMBLY " + String.Format("{0:00}", n)); }
                        cbb_Machine.Items.Add("WELDING 01");
                        cbb_Machine.Items.Add("WELDING 02");
                        cbb_Machine.Items.Add("A.BALANCE 01");
                        cbb_Machine.Items.Add("A.BALANCE 02");
                        cbb_Machine.Items.Add("BLOWER 01");
                        cbb_Machine.Items.Add("BLOWER 02");
                        dtp_Date.MinDate = new DateTime(2019, 4, 1, 8, 0, 0);
                        break;
                }
                cbb_Machine.Items.Insert(0, "");
                cbb_Machine.SelectedIndex = 0;
                cbb_Machine.Enabled = true;
                dtp_Date.Enabled = true;
            }
            else
            {
                cbb_Machine.SelectedIndex = 0;
                cbb_Machine.Enabled = false;
                dtp_Date.Enabled = false;
                btn_Load.Enabled = false;
            }
        }

        private void cbb_Machine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dtp_Date.Enabled = btn_Load.Enabled = (cbb_Machine.SelectedIndex > 0) ? true : false;
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            string line = String.Format("{0:00}", cbb_Line.SelectedIndex);
            string machine = String.Format("{0:00}", cbb_Machine.SelectedIndex);
            string date = String.Format("{0:dd/MM/yyyy}", dtp_Date.Value);

            ini.SetIniValue("TEMPERATURE", "MACHINE", line + "-" + machine);
            ini.SetIniValue("TEMPERATURE", "DATE", date);

            frmTemperatureByDateSelected_v2 frmView = new frmTemperatureByDateSelected_v2(this);
            frmView.Show();
            this.Visible = false;
        }
    }
}
