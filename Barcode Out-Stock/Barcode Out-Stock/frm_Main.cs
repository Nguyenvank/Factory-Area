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
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            Fnc_Get_Datetime();
            init_Load_Controls();
        }

        public void init_Load_Controls()
        {
            Fnc_Load_Controls();
        }


        /****************************************************/

        public void Fnc_Get_Datetime()
        {
            cls.fnSetDateTime(tss_Datetime);
        }

        public void Fnc_Load_Controls()
        {
            //cls.showUC(new Ctrl.uc_FinishGoods_Scan_Out_v1o0(), panel1);
            //cls.showUC(new Ctrl.uc_FinishGoods_Scan_Out_v2o0(), panel1);
            cls.showUC(new Ctrl.uc_FinishGoods_Scan_Out_v2o1(), panel1);

            this.Text = "FINISH GOODS SCAN OUT v2.0";
        }

        /****************************************************/


        private void Timer1_Tick(object sender, EventArgs e)
        {
            Fnc_Get_Datetime();
        }
    }
}
