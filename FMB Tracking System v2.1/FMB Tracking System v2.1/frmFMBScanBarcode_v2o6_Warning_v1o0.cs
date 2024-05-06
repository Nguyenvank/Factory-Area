using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventory_Data;

namespace FMB_Tracking_System_v2._1
{
    public partial class frmFMBScanBarcode_v2o6_Warning_v1o0 : Form
    {
        Timer
            __timer_main = new Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __err_nm = "",
            __err_msg = "",
            __pack_cd = "";

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __err_cd = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0,
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0;

        bool
            __accept = false;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public frmFMBScanBarcode_v2o6_Warning_v1o0()
        {
            InitializeComponent();
        }

        public frmFMBScanBarcode_v2o6_Warning_v1o0(int err_cd, string err_msg, string pack_cd)
        {
            InitializeComponent();

            __err_cd = err_cd;
            __err_msg = err_msg;
            __pack_cd = pack_cd;

            __timer_main.Interval = 1000;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(this, true);
            cls.SetDoubleBuffer(tlp_main, true);
        }

        private void frmFMBScanBarcode_v2o6_Warning_v1o0_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /*****************************************/

        public void Fnc_Load_Controls()
        {
            lbl_top.Text = "";
            lbl_bottom.Text = "HÃY THÔNG BÁO CHO QUẢN LÝ ĐỂ XÁC NHẬN VẤN ĐỀ NÀY\r\nPLEASE NOTICE SUPERVISOR TO CONFIRM THIS ISSUE";

            Fnc_Load_Message();
            Fnc_Load_Confirm();
        }

        public void Fnc_Load_Message()
        {
            switch (__err_cd)
            {
                case 1:
                    __err_nm = "No FIFO";
                    break;
                case 2:
                    __err_nm = "Not enough cooling time";
                    break;
                case 3:
                    __err_nm = "Invalid packing code";
                    break;
                case 4:
                    __err_nm = "Invalid packing code for FMB";
                    break;
                case 5:
                    __err_nm = "Invalid packing code for FMB";
                    break;
                case 6:
                    __err_nm = "Invalid packing code";
                    break;
                case 7:
                    __err_nm = "Not scan mixxing yet";
                    break;
                case 8:
                    __err_nm = "Chemical expired";
                    break;
                case 9:
                    break;
            }

            lbl_top.Text = __err_msg;

            try
            {
                __sql = "FMB_Material_Packing_OUT_Confirm_AddItem_V1o0_Addnew";

                __sparam = new SqlParameter[4];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@err_cd";
                __sparam[0].Value = __err_cd;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.NVarChar;
                __sparam[1].ParameterName = "@err_nm";
                __sparam[1].Value = __err_nm;

                __sparam[2] = new SqlParameter();
                __sparam[2].SqlDbType = SqlDbType.NVarChar;
                __sparam[2].ParameterName = "@err_msg";
                __sparam[2].Value = __err_msg;

                __sparam[3] = new SqlParameter();
                __sparam[3].SqlDbType = SqlDbType.VarChar;
                __sparam[3].ParameterName = "@fmb_cd";
                __sparam[3].Value = __pack_cd;

                cls.fnUpdDel(__sql, __sparam);
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Confirm()
        {
            string accept = "";

            try
            {
                __sql = "FMB_Material_Packing_OUT_Confirm_SelItem_V1o0_Addnew";

                __ds = cls.ExecuteDataSet(__sql);
                __tbl_cnt = __ds.Tables.Count;
                __row_cnt = __ds.Tables[0].Rows.Count;

                if (__tbl_cnt > 0 && __row_cnt > 0)
                {
                    accept = __ds.Tables[0].Rows[0][7].ToString();
                    __accept = (accept.ToLower() == "true") ? true : false;

                    if (__accept)
                    {
                        this.Close();
                    }
                }
            }
            catch { }
            finally { }
        }

        /*****************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            this.BackColor = (__now_sec % 2 == 0) ? Color.OrangeRed : Color.Coral;

            Fnc_Load_Confirm();
        }
    }
}
