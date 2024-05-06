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
    public partial class frmWarehouseMaterialScanOut_v1o1_Message : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __cde = "",
            __app = cls.appName();

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0,
            __err_cd = 0,
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __display = 0;

        bool
            __flag_dis = false;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public frmWarehouseMaterialScanOut_v1o1_Message(string err_msg, string err_cd)
        {
            InitializeComponent();

            __msg = err_msg;
            __cde = err_cd;

            __err_cd = (__cde.Length > 0) ? Convert.ToInt32(__cde) : 0;

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(this, true);
            cls.SetDoubleBuffer(tlp_main, true);
        }

        private void frmWarehouseMaterialScanOut_v1o1_Message_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmWarehouseMaterialScanOut_v1o1_Message_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /****************************************/

        public void Fnc_Load_Controls()
        {
            lbl_top.Text = __app;
            lbl_mid.Text = __msg;
            lbl_bot.Text = String.Format("DO NOT TRY TO SCAN THIS PACKING !!!\r\nPlease contact to your manager to solve it\r\nError code: {0:000}", __err_cd);
        }

        /****************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            if (__display <= 15)
            {
                __display++;

                tlp_main.BackColor = (__now_sec % 2 == 0) ? Color.Firebrick : Color.IndianRed;

                if (__display == 15)
                {
                    this.Close();
                }
            }
        }
    }
}
