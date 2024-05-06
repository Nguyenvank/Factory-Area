using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DX___MMT_System
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            int tab = tabPane1.SelectedPageIndex;
            switch (tab)
            {
                case 0:

                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    Panel pnl = new Panel();
                    pnl.Name = "pnl_Invent_Sub";
                    ucInventSub uc = new ucInventSub();
                    showControl(pnl, uc);
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }
        }

        public void showControl(System.Windows.Forms.Panel pnl, System.Windows.Forms.Control obj)
        {
            pnl.Controls.Clear();
            obj.Dock = DockStyle.Fill;
            pnl.Controls.Add(obj);
        }
    }
}