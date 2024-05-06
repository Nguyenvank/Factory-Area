using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            init_Load_Controls();
        }

        public void init_Load_Controls()
        {
            Fnc_GetDate();
            Fnc_Load_Controls();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Fnc_GetDate();
        }


        /*********************************************************/


        public void Fnc_GetDate()
        {
            cls.fnSetDateTime(tssDatetime);
        }

        public void Fnc_Load_Controls()
        {
            cls.showUC(new Ctrl.uc_MMS_WarehouseMaterialScanOut_v1o1(), panel1);
        }


        /*********************************************************/



    }
}
