using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data.Ctrl
{
    public partial class uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0 : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __warning_msg = "";

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __warning_cd = 0,
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(this, true);
            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_content, true);
        }

        public uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0(string warning_msg, int warning_cd)
        {
            InitializeComponent();

            __warning_msg = warning_msg;
            __warning_cd = warning_cd;

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(this, true);
            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_content, true);
        }

        private void uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        private void uc_FinishGoods_Scan_Out_v2o0_Warning_v1o0_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /**************************************/

        public void Fnc_Load_Controls()
        {
            lbl_header.Text = "DELIVERY SCAN OUT ISSUE";
            lbl_main.Text = __warning_msg;
            lbl_footer.Text = "HÃY THÔNG BÁO TỚI NGƯỜI QUẢN LÝ ĐỂ XÁC NHẬN THÔNG BÁO NÀY\r\nPLEASE NOTICE TO THE MANAGER TO CONFIRM THIS NOTICE";

            lbl_main.BackColor = Color.Tomato;
        }

        /**************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            lbl_main.BackColor = (__now_sec % 2 == 0) ? Color.Salmon : Color.Tomato;
        }
    }
}
