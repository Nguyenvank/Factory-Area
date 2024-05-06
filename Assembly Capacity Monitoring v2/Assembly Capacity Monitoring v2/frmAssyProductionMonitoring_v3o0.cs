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
    public partial class frmAssyProductionMonitoring_v3o0 : Form
    {
        int _view = 1;

        public frmAssyProductionMonitoring_v3o0()
        {
            InitializeComponent();

            ContextMenu mnuPanel = new ContextMenu();
            mnuPanel.MenuItems.Add(new MenuItem("Table view", fn_Table_View_Click));
            mnuPanel.MenuItems.Add(new MenuItem("List view", fn_List_View_Click));
            mnuPanel.MenuItems.Add("-");
            mnuPanel.MenuItems.Add(new MenuItem("Exit application", fn_Close_Click));
            panel1.ContextMenu = mnuPanel;

        }

        private void FrmAssyProductionMonitoring_v3o0_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            Fnc_Load_Controls();
            Fnc_Load_Data();
        }

        public void Fnc_Load_Controls()
        {

        }

        public void Fnc_Load_Data()
        {
            switch (_view)
            {
                case 1:
                    cls.showUC(new Ctrl.uc_AssyProductionMonitoring_v3o0o1(), panel1);
                    break;
                case 2:
                    cls.showUC(new Ctrl.uc_AssyProductionMonitoring_v3o0o2(), panel1);
                    break;
            }
        }

        private void fn_Table_View_Click(object sender, EventArgs e)
        {
            _view = 1;
            panel1.Controls.Clear();
            Fnc_Load_Data();
        }

        private void fn_List_View_Click(object sender, EventArgs e)
        {
            _view = 2;
            panel1.Controls.Clear();
            Fnc_Load_Data();
        }

        private void fn_Close_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }



    }
}
