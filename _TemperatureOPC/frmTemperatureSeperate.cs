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
    public partial class frmTemperatureSeperate : Form
    {
        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        public string temperLoad = "";

        int _load = 1;
        bool _fullscreen = false;

        public frmTemperatureSeperate()
        {
            InitializeComponent();

            temperLoad = ini.GetIniValue("TEMPER", "ID", "1").Trim();
            _load = Convert.ToInt32(temperLoad);

            Fnc_Load_Menu_Context();
        }

        private void frmTemperatureSeperate_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
            init_Default();
        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        public void init_Default()
        {
            switch (_load)
            {
                case 1:
                    if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts.Instance))
                    {
                        pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts.Instance);
                    }
                    Ctrl.uc_Temperature_LiveCharts.Instance.Dock = DockStyle.Fill;
                    Ctrl.uc_Temperature_LiveCharts.Instance.BringToFront();
                    break;
                case 2:
                    if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts_Plastic.Instance))
                    {
                        pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts_Plastic.Instance);
                    }
                    Ctrl.uc_Temperature_LiveCharts_Plastic.Instance.Dock = DockStyle.Fill;
                    Ctrl.uc_Temperature_LiveCharts_Plastic.Instance.BringToFront();
                    break;
                case 3:
                    if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveData.Instance))
                    {
                        pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveData.Instance);
                    }
                    Ctrl.uc_Temperature_LiveData.Instance.Dock = DockStyle.Fill;
                    Ctrl.uc_Temperature_LiveData.Instance.BringToFront();
                    break;
                case 4:
                    if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts_Welding.Instance))
                    {
                        pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts_Welding.Instance);
                    }
                    Ctrl.uc_Temperature_LiveCharts_Welding.Instance.Dock = DockStyle.Fill;
                    Ctrl.uc_Temperature_LiveCharts_Welding.Instance.BringToFront();
                    break;
                case 5:
                    if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts_Assembly.Instance))
                    {
                        pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts_Assembly.Instance);
                    }
                    Ctrl.uc_Temperature_LiveCharts_Assembly.Instance.Dock = DockStyle.Fill;
                    Ctrl.uc_Temperature_LiveCharts_Assembly.Instance.BringToFront();
                    break;
                case 102:
                    //cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits(), pnl_Main);
                    cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_02(), pnl_Main);
                    break;
                case 103:
                    //cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits(), pnl_Main);
                    //cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_03(), pnl_Main);      /* Fix LL and HH temperature limit */
                    //cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_04(), pnl_Main);      /* Default temperature monitor screen */
                    cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_05(), pnl_Main);        /* Blinking NG temperature cell */
                    break;
                case 104:
                    //cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_04(), pnl_Main);
                    cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_05(), pnl_Main);
                    break;
                case 106:
                    cls.showUC(new Ctrl.uc_PMS_Temperature_Injection_Digits_06(), pnl_Main);
                    break;
            }
        }

        private void temperatureAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_All.Instance))
            //{
            //    pnl_Main.Controls.Add(Ctrl.uc_Temperature_All.Instance);
            //}
            //Ctrl.uc_Temperature_All.Instance.Dock = DockStyle.Fill;
            //Ctrl.uc_Temperature_All.Instance.BringToFront();
        }

        private void temperature1DetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_1_Detail.Instance))
            {
                pnl_Main.Controls.Add(Ctrl.uc_Temperature_1_Detail.Instance);
            }
            Ctrl.uc_Temperature_1_Detail.Instance.Dock = DockStyle.Fill;
            Ctrl.uc_Temperature_1_Detail.Instance.BringToFront();
        }

        private void temperature4HorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_4_Horizontal.Instance))
            //{
            //    pnl_Main.Controls.Add(Ctrl.uc_Temperature_4_Horizontal.Instance);
            //}
            //Ctrl.uc_Temperature_4_Horizontal.Instance.Dock = DockStyle.Fill;
            //Ctrl.uc_Temperature_4_Horizontal.Instance.BringToFront();
        }

        private void temperature4SquareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_4_Square.Instance))
            {
                pnl_Main.Controls.Add(Ctrl.uc_Temperature_4_Square.Instance);
            }
            Ctrl.uc_Temperature_4_Square.Instance.Dock = DockStyle.Fill;
            Ctrl.uc_Temperature_4_Square.Instance.BringToFront();
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void temeratureMSChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_MS_Chart.Instance))
            //{
            //    pnl_Main.Controls.Add(Ctrl.uc_Temperature_MS_Chart.Instance);
            //}
            //Ctrl.uc_Temperature_MS_Chart.Instance.Dock = DockStyle.Fill;
            //Ctrl.uc_Temperature_MS_Chart.Instance.BringToFront();
        }

        private void temperatureLiveChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!pnl_Main.Controls.Contains(Ctrl.uc_Temperature_LiveCharts.Instance))
            {
                pnl_Main.Controls.Add(Ctrl.uc_Temperature_LiveCharts.Instance);
            }
            Ctrl.uc_Temperature_LiveCharts.Instance.Dock = DockStyle.Fill;
            Ctrl.uc_Temperature_LiveCharts.Instance.BringToFront();
        }

        public void Fnc_Load_Menu_Context()
        {
            string sFullscreen = (_fullscreen == true) ? "Disable Fullcreen" : "Enable Fullscreen";
            ContextMenu mnuPanel = new ContextMenu();
            mnuPanel.MenuItems.Add(new MenuItem("Injection Temperature (Graph)", Fnc_Injection_Temper_Graph_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Injection Temperature (Digits BR)", Fnc_Injection_Temper_Digits_BlRd_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Injection Temperature (Digits BYR)", Fnc_Injection_Temper_Digits_BlYlRd_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Injection Temperature (20 MC)", Fnc_Injection_Temper_Digits_20MC_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Assembly Temperature", Fnc_Assembly_Temper_Click));
            mnuPanel.MenuItems.Add("-");
            mnuPanel.MenuItems.Add(new MenuItem(sFullscreen, Fnc_Fullscreen_Click));
            mnuPanel.MenuItems.Add("-");
            mnuPanel.MenuItems.Add(new MenuItem("Exit application", Fnc_Form_Close_Click));
            pnl_Main.ContextMenu = mnuPanel;
        }

        public void Fnc_Load_Fullscreen()
        {
            bool _newStatus = (_fullscreen == true) ? false : true;
            this.FormBorderStyle = (_newStatus == true) ? FormBorderStyle.None : FormBorderStyle.FixedSingle;

            _fullscreen = _newStatus;
            Fnc_Load_Menu_Context();
        }

        public void Fnc_Load_unFullscreen()
        {
            if (_fullscreen == true)
            {
                _fullscreen = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Fnc_Load_Menu_Context();
            }
        }

        private void Fnc_Injection_Temper_Graph_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("TEMPER", "ID", "1");
            System.Windows.Forms.Application.Restart();
        }

        private void Fnc_Injection_Temper_Digits_BlRd_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("TEMPER", "ID", "102");
            System.Windows.Forms.Application.Restart();
        }

        private void Fnc_Injection_Temper_Digits_BlYlRd_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("TEMPER", "ID", "103");
            //ini.SetIniValue("TEMPER", "ID", "104");
            System.Windows.Forms.Application.Restart();
        }

        private void Fnc_Injection_Temper_Digits_20MC_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("TEMPER", "ID", "106");
            //ini.SetIniValue("TEMPER", "ID", "104");
            System.Windows.Forms.Application.Restart();
        }

        private void Fnc_Assembly_Temper_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("TEMPER", "ID", "5");
            System.Windows.Forms.Application.Restart();
        }

        private void Fnc_Form_Close_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        public void Fnc_Fullscreen_Click(object sender, EventArgs e)
        {
            Fnc_Load_Fullscreen();
        }

        private void frmTemperatureSeperate_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F:
                    if (e.Modifiers == Keys.Control)
                    {
                        Fnc_Load_Fullscreen();
                    }
                    break;
                case Keys.Escape:
                    Fnc_Load_unFullscreen();
                    break;
            }
            //if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            //{
            //    Fnc_Load_Fullscreen();
            //}
        }
    }
}
