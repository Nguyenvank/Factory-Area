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
    public partial class frmWarehouseMaterialScanOut_v1o2_Message_v1o0 : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __issue_msg = "",
            __issue_cd = "",
            __app = cls.appName();

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __whs_idx = 0,
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

        public frmWarehouseMaterialScanOut_v1o2_Message_v1o0(string err_msg, string err_cd,int whs_idx)
        {
            InitializeComponent();

            try
            {
                __issue_msg = err_msg;
                __issue_cd = err_cd;
                __whs_idx = whs_idx;

                __err_cd = (__issue_cd.Length > 0) ? Convert.ToInt32(__issue_cd) : 0;
            }
            catch { }
            finally { }

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(this, true);
            cls.SetDoubleBuffer(tlp_main, true);
        }

        private void frmWarehouseMaterialScanOut_v1o1_Message_v1o2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmWarehouseMaterialScanOut_v1o1_Message_v1o2_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Get_Warning_From_Logging();

            Fnc_Load_Controls();
        }

        /****************************************/

        public void Fnc_Load_Controls()
        {
            string msg = "";
            msg = "Hãy liên hệ với cấp quản lý để xác nhận lại trên hệ thống\r\n";
            msg += "Please contact the management to confirm on the system\r\n";
            msg += "Error code:  {0:000}";

            lbl_top.Text = __app;
            lbl_mid.Text = __issue_msg;

            lbl_bot.Text = String.Format(msg, __err_cd);
        }

        public void Fnc_Load_Get_Warning_From_Logging()
        {
            string issue_msg = __issue_msg;

            int
                whs_idx = __whs_idx,
                issue_cnt = 0,
                issue_cd = Convert.ToInt32(__issue_cd);

            try
            {
                __sql = "V2o1_SUMMARY_MMT_Warehouse_Scan_Out_Issue_SelItem_Addnew_V1o0";

                __sparam = new SqlParameter[1];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@whs_idx";
                __sparam[0].Value = __whs_idx;

                __ds = cls.ExecuteDataSet(__sql, __sparam);
                __tbl_cnt = __ds.Tables.Count;
                __row_cnt = __ds.Tables[0].Rows.Count;

                if (__tbl_cnt > 0 && __row_cnt > 0)
                {
                    issue_cnt = int.Parse(__ds.Tables[0].Rows[0][0].ToString());

                    if (issue_cnt == 0)
                    {
                        this.Close();
                    }
                }
            }
            catch { }
            finally { }
        }

        /****************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            tlp_main.BackColor = (__now_sec % 2 == 0) ? Color.Firebrick : Color.IndianRed;

            Fnc_Load_Get_Warning_From_Logging();

            if (__err_cd == 1000 && __display <= 15)
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
