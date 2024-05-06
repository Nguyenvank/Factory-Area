using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_LimitRange : UserControl
    {
        private static uc_Temperature_LimitRange _instance;
        public static uc_Temperature_LimitRange Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_LimitRange();
                return _instance;
            }
        }


        int _regIDx = 0;

        int _makerIDx, _makerLevel, _makerDept;
        string _makerName;
        int _dgv_ES_List_Width;

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        DateTime _dt;

        public uc_Temperature_LimitRange()
        {
            InitializeComponent();
        }

        private void uc_Temperature_LimitRange_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            init_Maker();

            cbb_ES_Reg_Kind.Items.Clear();
            cbb_ES_Reg_Kind.Items.Add("");
            cbb_ES_Reg_Kind.Items.Add("Rubber / Cao su");
            cbb_ES_Reg_Kind.Items.Add("Plastic / Ép nhựa");
            cbb_ES_Reg_Kind.Items.Add("Assembly / Lắp ráp");


            init_Reg();
        }

        public void init_Maker()
        {
            System.Windows.Forms.Form frm = System.Windows.Forms.Application.OpenForms["frmPM_01WorkOrders"];

            _makerIDx = ((frmPM_01WorkOrders)frm)._logIDx;
            _makerLevel = ((frmPM_01WorkOrders)frm)._logLevel;
            _makerName = ((frmPM_01WorkOrders)frm)._logName;
            _makerDept = ((frmPM_01WorkOrders)frm)._logDept;
            _dt = ((frmPM_01WorkOrders)frm)._dt;

            /*********************************************/
            string sql = "PMMS_01_Order_Entrance_Reg_PIC_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = _makerIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string logTitle = ds.Tables[0].Rows[0][1].ToString();
                string logDepart = ds.Tables[0].Rows[0][2].ToString();

                lbl_ES_PIC_Name.Text = _makerName.ToUpper();
                lbl_ES_PIC_Dept.Text = logDepart;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin đăng nhập.\r\nVui lòng thử lại hoặc báo quản trị hệ thống");
                frmPM_00LoginSystem frmLogin = new frmPM_00LoginSystem(1);
                this.Hide();
                frmLogin.Show();
            }
            /**********************************************/
        }

        public string fnc_ResourceFilePath(string file)
        {
            string flePath = "";
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            flePath = string.Format("{0}Resources\\" + file, Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            return flePath;
        }

        public void init_Reg()
        {
            cbb_ES_Reg_Kind.SelectedIndex = 0;

            cbb_ES_Reg_Kind.Enabled = true;

            btn_ES_Reg_Save.Enabled = false;
            btn_ES_Reg_Done.Enabled = false;

            lbl_Addnew.Enabled = (_makerDept == 1) ? true : false;
            lbl_Remove.Enabled = (_makerDept == 1) ? true : false;
            lbl_Refresh.Enabled = true;

            lbl_Addnew.BackColor = (_makerDept == 1) ? Color.Green : Color.Silver;
            lbl_Remove.BackColor = (_makerDept == 1) ? Color.LightPink : Color.Silver;
            lbl_Refresh.BackColor = Color.Gold;

            lbl_Addnew.Text = (char)43 + "";
            lbl_Remove.Text = (char)8211 + "";
            lbl_Refresh.Text = (char)191 + "?";

            lbl_Temp_Standard.Text = "Nhiệt độ tiêu chuẩn (" + (char)176 + "C)";
            lbl_Temp_High.Text = "Giới hạn nhiệt độ cao (" + (char)176 + "C)";
            lbl_Temp_Low.Text = "Giới hạn nhiệt độ thấp (" + (char)176 + "C)";

            //txt_ES_Reg_From.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txt_ES_Reg_From.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txt_ES_Reg_From.AutoCompleteCustomSource = LoadMaterialNames();

            //init_Reg_List();
        }

    }
}
