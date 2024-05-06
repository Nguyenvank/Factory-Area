using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.IO;

namespace Inventory_Data.Ctrl
{
    public partial class uc_EntranceSystem_Register : UserControl
    {
        private static uc_EntranceSystem_Register _instance;
        public static uc_EntranceSystem_Register Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_EntranceSystem_Register();
                return _instance;
            }
        }

        int _regIDx = 0;

        int _makerIDx, _makerLevel;
        string _makerName;

        string _msgText = "";
        int _msgType = 0;
        string _matImage;
        bool _matNewImage;
        MemoryStream _ms;

        int _range = 3;
        int _dgv_ES_List_Width;

        //private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        //string _machine, _date;

        public uc_EntranceSystem_Register()
        {
            InitializeComponent();
        }

        private void uc_EntranceSystem_Register_Load(object sender, EventArgs e)
        {
            _dgv_ES_List_Width = cls.fnGetDataGridWidth(dgv_ES_List);
            init();
        }

        public void init()
        {
            init_Maker();

            dtp_ES_Reg_Date.MinDate = DateTime.Now;
            cbb_ES_Reg_Kind.Items.Clear();
            cbb_ES_Reg_Kind.Items.Add("");
            cbb_ES_Reg_Kind.Items.Add("Vào / IN");
            cbb_ES_Reg_Kind.Items.Add("Ra / OUT");

            cbb_ES_Reg_Tools.Items.Clear();
            cbb_ES_Reg_Tools.Items.Add("");
            cbb_ES_Reg_Tools.Items.Add("Không");
            cbb_ES_Reg_Tools.Items.Add("Có");

            init_Reg();
        }

        public void init_Maker()
        {
            System.Windows.Forms.Form frm = System.Windows.Forms.Application.OpenForms["frmPM_01WorkOrders"];

            int makerIDx = ((frmPM_01WorkOrders)frm)._logIDx;
            int makerLevel = ((frmPM_01WorkOrders)frm)._logLevel;
            string makerName = ((frmPM_01WorkOrders)frm)._logName;

            _makerIDx = makerIDx;
            _makerLevel = makerLevel;
            _makerName = makerName;

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

                lbl_ES_PIC_Name.Text = makerName.ToUpper();
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

        
        #region ENTRANCE SYSTEM - REGISTER IN


        public void init_Reg()
        {
            cbb_ES_Reg_Kind.SelectedIndex = 0;
            txt_ES_Reg_From.Text = "";
            txt_ES_Reg_Name.Text = "";
            txt_ES_Reg_IDNo.Text = "";
            txt_ES_Reg_Qty.Text = "";
            txt_ES_Reg_Purpose.Text = "";
            dtp_ES_Reg_Date.Value = DateTime.Now;
            chk_ES_Reg_Time.Checked = false;
            dtp_ES_Reg_Time.Value = DateTime.Now;
            dtp_ES_Reg_Time.Enabled = false;
            cbb_ES_Reg_Tools.SelectedIndex = 0;
            txt_ES_Reg_Note.Text = "";

            cbb_ES_Reg_Kind.Enabled = true;
            txt_ES_Reg_From.Enabled = false;
            txt_ES_Reg_Name.Enabled = false;
            txt_ES_Reg_IDNo.Enabled = false;
            txt_ES_Reg_Qty.Enabled = false;
            txt_ES_Reg_Purpose.Enabled = false;
            dtp_ES_Reg_Date.Enabled = false;
            chk_ES_Reg_Time.Enabled = false;
            dtp_ES_Reg_Time.Enabled = false;
            cbb_ES_Reg_Tools.Enabled = false;
            pnl_ES_Reg_Tool_Image_Border.Enabled = false;
            pnl_ES_Reg_Tool_Image.Enabled = false;
            pnl_ES_Reg_Tool_Image.BackgroundImage = global::Inventory_Data.Properties.Resources.no_image;
            pnl_ES_Reg_Tool_Image.BackgroundImageLayout = ImageLayout.Stretch;
            tlp_ES_Reg_Tool_Image.Enabled = false;
            txt_ES_Reg_Note.Enabled = false;
            rdb_ES_Reg_Add.Checked = false;
            rdb_ES_Reg_Upd.Checked = false;
            rdb_ES_Reg_Del.Checked = false;

            btn_ES_Reg_Save.Enabled = false;
            btn_ES_Reg_Done.Enabled = false;

            txt_ES_Reg_From.AutoCompleteMode = AutoCompleteMode.Suggest;
            txt_ES_Reg_From.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txt_ES_Reg_From.AutoCompleteCustomSource = LoadMaterialNames();

            init_Reg_List();
        }

        public void init_Reg_List()
        {
            string makerIDx = _makerIDx.ToString();
            string range = _range.ToString();
            string sql = "V2o1_ERP_Entrance_System_Reg_List_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = makerIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@rangeTime";
            sParams[1].Value = range;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            //foreach (DataRow dr in dt.Rows)
            //{

            //    string regDT = dr[12].ToString();
            //    string newRegDT = cls.Left(regDT, 10);
            //    dr[12] = newRegDT;
            //}

            _dgv_ES_List_Width = cls.fnGetDataGridWidth(dgv_ES_List);
            dgv_ES_List.DataSource = dt;

            dgv_ES_List.Columns[0].Width = 4 * _dgv_ES_List_Width / 100;    // STT
            //dgv_ES_List.Columns[1].Width = 5 * _dgv_ES_List_Width / 100;    // idx
            //dgv_ES_List.Columns[2].Width = 5 * _dgv_ES_List_Width / 100;    // picIDx
            //dgv_ES_List.Columns[3].Width = 15 * _dgv_ES_List_Width / 100;    // Name
            //dgv_ES_List.Columns[4].Width = 5 * _dgv_ES_List_Width / 100;    // Dept
            dgv_ES_List.Columns[5].Width = 4 * _dgv_ES_List_Width / 100;    // IN-OUT
            //dgv_ES_List.Columns[6].Width = 20 * _dgv_ES_List_Width / 100;    // inFrom
            //dgv_ES_List.Columns[7].Width = 15 * _dgv_ES_List_Width / 100;    // inRepNm
            //dgv_ES_List.Columns[8].Width = 7 * _dgv_ES_List_Width / 100;    // inRepID
            dgv_ES_List.Columns[9].Width = 18 * _dgv_ES_List_Width / 100;    // Representative
            //dgv_ES_List.Columns[10].Width = 4 * _dgv_ES_List_Width / 100;    // inQty
            dgv_ES_List.Columns[11].Width = 17 * _dgv_ES_List_Width / 100;    // inPurpose
            dgv_ES_List.Columns[12].Width = 7 * _dgv_ES_List_Width / 100;    // inRegDateTime
            dgv_ES_List.Columns[13].Width = 4 * _dgv_ES_List_Width / 100;    // inWithTools
            //dgv_ES_List.Columns[14].Width = 5 * _dgv_ES_List_Width / 100;    // inRemarks
            //dgv_ES_List.Columns[15].Width = 5 * _dgv_ES_List_Width / 100;    // managerApprove
            dgv_ES_List.Columns[16].Width = 10 * _dgv_ES_List_Width / 100;    // [Manager Approve]
            //dgv_ES_List.Columns[17].Width = 5 * _dgv_ES_List_Width / 100;    // directorApprove
            dgv_ES_List.Columns[18].Width = 10 * _dgv_ES_List_Width / 100;    // [Director Approve]
            //dgv_ES_List.Columns[19].Width = 5 * _dgv_ES_List_Width / 100;    // hrApprove
            dgv_ES_List.Columns[20].Width = 10 * _dgv_ES_List_Width / 100;    // [H.R Approve]
            //dgv_ES_List.Columns[21].Width = 5 * _dgv_ES_List_Width / 100;    // securityApprove
            dgv_ES_List.Columns[22].Width = 10 * _dgv_ES_List_Width / 100;    // [Security Control]
            dgv_ES_List.Columns[23].Width = 6 * _dgv_ES_List_Width / 100;    // regAdded
            //dgv_ES_List.Columns[24].Width = 5 * _dgv_ES_List_Width / 100;    // regFinish
            //dgv_ES_List.Columns[25].Width = 5 * _dgv_ES_List_Width / 100;    // regFinishDate

            dgv_ES_List.Columns[0].Visible = true;
            dgv_ES_List.Columns[1].Visible = false;
            dgv_ES_List.Columns[2].Visible = false;
            dgv_ES_List.Columns[3].Visible = false;
            dgv_ES_List.Columns[4].Visible = false;
            dgv_ES_List.Columns[5].Visible = true;
            dgv_ES_List.Columns[6].Visible = false;
            dgv_ES_List.Columns[7].Visible = false;
            dgv_ES_List.Columns[8].Visible = false;
            dgv_ES_List.Columns[9].Visible = true;
            dgv_ES_List.Columns[10].Visible = false;
            dgv_ES_List.Columns[11].Visible = true;
            dgv_ES_List.Columns[12].Visible = true;
            dgv_ES_List.Columns[13].Visible = true;
            dgv_ES_List.Columns[14].Visible = false;
            dgv_ES_List.Columns[15].Visible = false;
            dgv_ES_List.Columns[16].Visible = true;
            dgv_ES_List.Columns[17].Visible = false;
            dgv_ES_List.Columns[18].Visible = true;
            dgv_ES_List.Columns[19].Visible = false;
            dgv_ES_List.Columns[20].Visible = true;
            dgv_ES_List.Columns[21].Visible = false;
            dgv_ES_List.Columns[22].Visible = true;
            dgv_ES_List.Columns[23].Visible = true;
            dgv_ES_List.Columns[24].Visible = false;
            dgv_ES_List.Columns[25].Visible = false;

            dgv_ES_List.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv_ES_List.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv_ES_List.Columns[23].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm tt";
            cls.fnFormatDatagridviewWhite(dgv_ES_List, 11, 80);


            string regDT = "";
            string manager = "", director = "", hr = "", security = "";
            DateTime _regDT;
            int _manager = 0, _director = 0, _hr = 0, _security = 0;
            Color _bgManager = Color.Silver, _bgDirector = Color.Silver;
            Color _bgHR = Color.Silver, _bgSecurity = Color.Silver;
            foreach (DataGridViewRow row in dgv_ES_List.Rows)
            {
                regDT = row.Cells[12].Value.ToString().Replace("00:00 AM", "");

                //_regDT = Convert.ToDateTime(regDT.Replace("00:00 AM", ""));
                row.Cells[12].Value = regDT.ToString();

                manager = row.Cells[15].Value.ToString();
                director = row.Cells[17].Value.ToString();
                hr = row.Cells[19].Value.ToString();
                security = row.Cells[21].Value.ToString();

                _manager = (manager != "" && manager != null) ? Convert.ToInt32(manager) : 0;
                _director = (director != "" && director != null) ? Convert.ToInt32(director) : 0;
                _hr = (hr != "" && hr != null) ? Convert.ToInt32(hr) : 0;
                _security = (security != "" && security != null) ? Convert.ToInt32(security) : 0;

                switch (manager)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgManager = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgManager = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgManager = Color.LightPink;
                        break;
                }
                row.Cells[16].Style.BackColor = _bgManager;
                switch (director)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgDirector = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgDirector = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgDirector = Color.LightPink;
                        break;
                }
                row.Cells[18].Style.BackColor = _bgDirector;
                switch (hr)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgHR = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgHR = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgHR = Color.LightPink;
                        break;
                }
                row.Cells[20].Style.BackColor = _bgHR;
                switch (security)
                {
                    case "0":
                        // NOT APPROVE YET
                        _bgSecurity = Color.Silver;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        _bgSecurity = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        _bgSecurity = Color.LightPink;
                        break;
                }
                row.Cells[22].Style.BackColor = _bgSecurity;
            }
        }

        public void init_Reg_Done()
        {
            _matImage = "";
            _matNewImage = false;

            cbb_ES_Reg_Kind.SelectedIndex = 0;
            txt_ES_Reg_From.Text = "";
            txt_ES_Reg_Name.Text = "";
            txt_ES_Reg_IDNo.Text = "";
            txt_ES_Reg_Qty.Text = "";
            txt_ES_Reg_Purpose.Text = "";
            dtp_ES_Reg_Date.Value = DateTime.Now;
            chk_ES_Reg_Time.Checked = false;
            dtp_ES_Reg_Time.Value = DateTime.Now;
            dtp_ES_Reg_Time.Enabled = false;
            cbb_ES_Reg_Tools.SelectedIndex = 0;
            txt_ES_Reg_Note.Text = "";

            cbb_ES_Reg_Kind.Enabled = true;
            txt_ES_Reg_From.Enabled = false;
            txt_ES_Reg_Name.Enabled = false;
            txt_ES_Reg_IDNo.Enabled = false;
            txt_ES_Reg_Qty.Enabled = false;
            txt_ES_Reg_Purpose.Enabled = false;
            dtp_ES_Reg_Date.Enabled = false;
            chk_ES_Reg_Time.Enabled = false;
            dtp_ES_Reg_Time.Enabled = false;
            dtp_ES_Reg_Time.Enabled = false;
            cbb_ES_Reg_Tools.Enabled = false;
            pnl_ES_Reg_Tool_Image_Border.Enabled = false;
            pnl_ES_Reg_Tool_Image.Enabled = false;
            pnl_ES_Reg_Tool_Image.BackgroundImage = global::Inventory_Data.Properties.Resources.no_image;
            pnl_ES_Reg_Tool_Image.BackgroundImageLayout = ImageLayout.Stretch;
            tlp_ES_Reg_Tool_Image.Enabled = false;
            txt_ES_Reg_Note.Enabled = false;
            rdb_ES_Reg_Add.Checked = false;
            rdb_ES_Reg_Upd.Checked = false;
            rdb_ES_Reg_Del.Checked = false;

            btn_ES_Reg_Save.Enabled = false;
            btn_ES_Reg_Done.Enabled = false;

            txt_ES_Reg_From.Focus();
        }

        public AutoCompleteStringCollection LoadMaterialNames()
        {
            AutoCompleteStringCollection TheNameList = new AutoCompleteStringCollection();
            try
            {
                string sql = "V2o1_ERP_Entrance_System_Reg_AutoSelect_SelItem_V1o1_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TheNameList.Add(row[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
            return TheNameList;
        }

        private void cbb_ES_Reg_Kind_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txt_ES_Reg_From.Text = "";
            txt_ES_Reg_Name.Text = "";
            txt_ES_Reg_IDNo.Text = "";
            txt_ES_Reg_Qty.Text = "";
            txt_ES_Reg_Purpose.Text = "";
            dtp_ES_Reg_Date.Value = DateTime.Now;
            chk_ES_Reg_Time.Checked = false;
            dtp_ES_Reg_Time.Value = DateTime.Now;
            dtp_ES_Reg_Time.Enabled = false;
            cbb_ES_Reg_Tools.SelectedIndex = 0;
            txt_ES_Reg_Note.Text = "";
            pnl_ES_Reg_Tool_Image_Border.Enabled = false;
            pnl_ES_Reg_Tool_Image.Enabled = false;
            tlp_ES_Reg_Tool_Image.Enabled = false;
            rdb_ES_Reg_Add.Checked = false;
            rdb_ES_Reg_Upd.Checked = false;
            rdb_ES_Reg_Del.Checked = false;

            if (cbb_ES_Reg_Kind.SelectedIndex > 0)
            {
                txt_ES_Reg_From.Enabled = true;
                txt_ES_Reg_Name.Enabled = true;
                txt_ES_Reg_IDNo.Enabled = true;
                txt_ES_Reg_Qty.Enabled = true;
                txt_ES_Reg_Purpose.Enabled = true;
                dtp_ES_Reg_Date.Enabled = true;
                chk_ES_Reg_Time.Enabled = true;
                dtp_ES_Reg_Time.Enabled = false;
                cbb_ES_Reg_Tools.Enabled = true;
                txt_ES_Reg_Note.Enabled = true;
                rdb_ES_Reg_Add.Enabled = true;
                rdb_ES_Reg_Upd.Enabled = false;
                rdb_ES_Reg_Del.Enabled = false;

                //txt_ES_Reg_From.Enabled = (cbb_ES_Reg_Kind.SelectedIndex > 1) ? false : true;
                //txt_ES_Reg_From.Text = (cbb_ES_Reg_Kind.SelectedIndex > 1) ? "Dong-A Hwasung Vina" : "";
                lbl_ES_Reg_Date.Text = (cbb_ES_Reg_Kind.SelectedIndex > 1) ? "Ngày ra:" : "Ngày vào:";
                chk_ES_Reg_Time.Text = (cbb_ES_Reg_Kind.SelectedIndex > 1) ? "Giờ ra:" : "Giờ vào:";

                txt_ES_Reg_From.Focus();
            }
            else
            {
                txt_ES_Reg_From.Enabled = false;
                txt_ES_Reg_Name.Enabled = false;
                txt_ES_Reg_IDNo.Enabled = false;
                txt_ES_Reg_Qty.Enabled = false;
                txt_ES_Reg_Purpose.Enabled = false;
                dtp_ES_Reg_Date.Enabled = false;
                chk_ES_Reg_Time.Enabled = false;
                dtp_ES_Reg_Time.Enabled = false;
                cbb_ES_Reg_Tools.Enabled = false;
                txt_ES_Reg_Note.Enabled = false;
                rdb_ES_Reg_Add.Checked = false;
                rdb_ES_Reg_Upd.Checked = false;
                rdb_ES_Reg_Del.Checked = false;

                lbl_ES_Reg_Date.Text = "Ngày:";
                chk_ES_Reg_Time.Text = "Giờ:";
            }

            btn_ES_Reg_Save.Enabled = false;
            btn_ES_Reg_Done.Enabled = false;
        }

        private void txt_ES_Reg_From_TextChanged(object sender, EventArgs e)
        {
            if (txt_ES_Reg_From.Text.Length > 0)
            {
                if (txt_ES_Reg_Name.Text.Length > 0
                    && txt_ES_Reg_IDNo.Text.Length > 0
                    && txt_ES_Reg_Qty.Text.Length > 0
                    && txt_ES_Reg_Purpose.Text.Length > 0
                    && dtp_ES_Reg_Date.Value >= DateTime.Now
                    && cbb_ES_Reg_Tools.SelectedIndex > 0
                    && (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked))
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void txt_ES_Reg_Name_TextChanged(object sender, EventArgs e)
        {
            if (txt_ES_Reg_Name.Text.Length > 0)
            {
                if (txt_ES_Reg_IDNo.Text.Length > 0
                    && txt_ES_Reg_Qty.Text.Length > 0
                    && txt_ES_Reg_Purpose.Text.Length > 0
                    && dtp_ES_Reg_Date.Value >= DateTime.Now
                    && cbb_ES_Reg_Tools.SelectedIndex > 0
                    && (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked))
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void txt_ES_Reg_IDNo_TextChanged(object sender, EventArgs e)
        {
            if (txt_ES_Reg_IDNo.Text.Length > 0)
            {
                if (txt_ES_Reg_Qty.Text.Length > 0
                    && txt_ES_Reg_Purpose.Text.Length > 0
                    && dtp_ES_Reg_Date.Value >= DateTime.Now
                    && cbb_ES_Reg_Tools.SelectedIndex > 0
                    && (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked))
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void txt_ES_Reg_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_ES_Reg_Qty_TextChanged(object sender, EventArgs e)
        {
            if (txt_ES_Reg_Qty.Text.Length > 0)
            {
                if (txt_ES_Reg_Purpose.Text.Length > 0
                    && dtp_ES_Reg_Date.Value >= DateTime.Now
                    && cbb_ES_Reg_Tools.SelectedIndex > 0
                    && (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked))
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void txt_ES_Reg_Purpose_TextChanged(object sender, EventArgs e)
        {
            if (txt_ES_Reg_Purpose.Text.Length > 0)
            {
                if (dtp_ES_Reg_Date.Value >= DateTime.Now
                    && cbb_ES_Reg_Tools.SelectedIndex > 0
                    && (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked))
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void dtp_ES_Reg_Date_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_ES_Reg_Date.Value >= DateTime.Now)
            {
                if (cbb_ES_Reg_Tools.SelectedIndex > 0
                    && (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked))
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void cbb_ES_Reg_Tools_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_ES_Reg_Tools.SelectedIndex > 0)
            {
                if (rdb_ES_Reg_Add.Checked
                    || rdb_ES_Reg_Upd.Checked
                    || rdb_ES_Reg_Del.Checked)
                {
                    btn_ES_Reg_Save.Enabled = true;
                }
                else
                {
                    btn_ES_Reg_Save.Enabled = false;
                }

                pnl_ES_Reg_Tool_Image_Border.Enabled = (cbb_ES_Reg_Tools.SelectedIndex > 1) ? true : false;
                pnl_ES_Reg_Tool_Image.Enabled = false;
                tlp_ES_Reg_Tool_Image.Enabled = (cbb_ES_Reg_Tools.SelectedIndex > 1) ? true : false;

                lnk_ES_Reg_Tool_Image_Add.Enabled = (cbb_ES_Reg_Tools.SelectedIndex > 1 && _regIDx == 0) ? true : false;
                lnk_ES_Reg_Tool_Image_Upd.Enabled = (cbb_ES_Reg_Tools.SelectedIndex > 1 && _regIDx > 0) ? true : false;
                lnk_ES_Reg_Tool_Image_Del.Enabled = (cbb_ES_Reg_Tools.SelectedIndex > 1 && _regIDx > 0) ? true : false;

                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {

                pnl_ES_Reg_Tool_Image_Border.Enabled = false;
                pnl_ES_Reg_Tool_Image.Enabled = false;
                pnl_ES_Reg_Tool_Image.BackgroundImage = (Image)global::Inventory_Data.Properties.Resources.no_image;
                pnl_ES_Reg_Tool_Image.BackgroundImageLayout = ImageLayout.Stretch;
                tlp_ES_Reg_Tool_Image.Enabled = false;

                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void rdb_ES_Reg_Add_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_ES_Reg_Add.Checked)
            {
                btn_ES_Reg_Save.Enabled = true;
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void rdb_ES_Reg_Upd_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_ES_Reg_Upd.Checked)
            {
                btn_ES_Reg_Save.Enabled = true;
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void rdb_ES_Reg_Del_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_ES_Reg_Del.Checked)
            {
                btn_ES_Reg_Save.Enabled = true;
                btn_ES_Reg_Done.Enabled = true;
            }
            else
            {
                btn_ES_Reg_Save.Enabled = false;
                btn_ES_Reg_Done.Enabled = false;
            }
        }

        private void chk_ES_Reg_Time_CheckedChanged(object sender, EventArgs e)
        {
            dtp_ES_Reg_Time.Enabled = (chk_ES_Reg_Time.Checked) ? true : false;
        }

        private void btn_ES_Reg_Done_Click(object sender, EventArgs e)
        {
            init_Reg_Done();
        }

        private void btn_ES_Reg_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (rdb_ES_Reg_Add.Checked)
                {
                    fnc_ES_Reg_Add();
                }
                else if (rdb_ES_Reg_Upd.Checked)
                {
                    fnc_ES_Reg_Upd();
                }
                else if (rdb_ES_Reg_Del.Checked)
                {
                    fnc_ES_Reg_Del();
                }

                txt_ES_Reg_From.AutoCompleteCustomSource = LoadMaterialNames();
                init_Reg_List();
            }
        }

        public void fnc_ES_Reg_Add()
        {
            try
            {
                string inout = (cbb_ES_Reg_Kind.SelectedIndex > 1) ? "0" : "1";
                string makerIDx = _makerIDx.ToString();
                string regFrom = txt_ES_Reg_From.Text.Trim();
                string regName = txt_ES_Reg_Name.Text.Trim();
                string regIDNo = txt_ES_Reg_IDNo.Text.Trim();
                string regTotal = txt_ES_Reg_Qty.Text.Trim();
                string regPurpose = txt_ES_Reg_Purpose.Text.Trim();
                DateTime regDate = dtp_ES_Reg_Date.Value;
                bool chkTime = (chk_ES_Reg_Time.Checked) ? true : false;
                DateTime regTime = dtp_ES_Reg_Time.Value;
                int chkTool = (cbb_ES_Reg_Tools.SelectedIndex > 1) ? 1 : 0;
                string regNote = txt_ES_Reg_Note.Text.Trim();

                string imgPath = (_matImage != "" && _matImage != null) ? _matImage : fnc_ResourceFilePath("no-image.jpg");
                Bitmap bmp = new Bitmap(imgPath);
                FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
                byte[] bimage = new byte[fs.Length];
                fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                string sql = "V2o1_ERP_Entrance_System_Reg_In_AddItem_V1o1_Addnew";

                SqlParameter[] sParams = new SqlParameter[13]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@picIDx";
                sParams[0].Value = makerIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.TinyInt;
                sParams[1].ParameterName = "@inout";
                sParams[1].Value = inout;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.NVarChar;
                sParams[2].ParameterName = "@inFrom";
                sParams[2].Value = regFrom;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.NVarChar;
                sParams[3].ParameterName = "@inRepNm";
                sParams[3].Value = regName;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.VarChar;
                sParams[4].ParameterName = "@inRepID";
                sParams[4].Value = regIDNo;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.SmallInt;
                sParams[5].ParameterName = "@inQty";
                sParams[5].Value = regTotal;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.NVarChar;
                sParams[6].ParameterName = "@inPurpose";
                sParams[6].Value = regPurpose;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.DateTime;
                sParams[7].ParameterName = "@inRegDate";
                sParams[7].Value = regDate;

                sParams[8] = new SqlParameter();
                sParams[8].SqlDbType = SqlDbType.TinyInt;
                sParams[8].ParameterName = "@chkRegTime";
                sParams[8].Value = chkTime;

                sParams[9] = new SqlParameter();
                sParams[9].SqlDbType = SqlDbType.DateTime;
                sParams[9].ParameterName = "@inRegTime";
                sParams[9].Value = regTime;

                sParams[10] = new SqlParameter();
                sParams[10].SqlDbType = SqlDbType.TinyInt;
                sParams[10].ParameterName = "@inWithTools";
                sParams[10].Value = chkTool;

                sParams[11] = new SqlParameter();
                sParams[11].SqlDbType = SqlDbType.NVarChar;
                sParams[11].ParameterName = "@inRemarks";
                sParams[11].Value = regNote;

                sParams[12] = new SqlParameter();
                sParams[12].SqlDbType = SqlDbType.Image;
                sParams[12].ParameterName = "@inRegTool";
                sParams[12].Value = bimage;

                cls.fnUpdDel(sql, sParams);

                _msgText = "Tạo mới đăng ký thành công!";
                _msgType = 1;

                //bimage = null;
                //fs.Close();
                //fs.Dispose();
                //bmp = null;
                //bmp.Dispose();

                //GC.Collect();
            }
            catch(SqlException sqlEx)
            {
                _msgText = "Có lỗi kết nối dữ liệu, vui lòng kiểm tra lại tình trạng kết nối mạng của máy tính.";
                _msgType = 3;
            }
            catch(Exception ex)
            {
                _msgText = "Có lỗi phát sinh, vui lòng liên hệ quản trị hệ thống.";
                _msgType = 2;
            }
            finally
            {
                
                init_Reg_Done();
                switch (_msgType)
                {
                    case 1:
                        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        break;
                    case 2:
                        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        break;
                    case 3:
                        MessageBox.Show(_msgText, cls.appName(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        break;
                }
            }


        }

        public void fnc_ES_Reg_Upd()
        {

        }

        public void fnc_ES_Reg_Del()
        {

        }

        private void lnk_ES_Reg_Tool_Image_Add_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //dialog.InitialDirectory = @"C:\";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.Title = "CHOOSE IMAGE...";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fleName = Path.GetFullPath(dialog.FileName);
                Image original = Image.FromFile(dialog.FileName);
                int BOXHEIGHT = 640;
                int BOXWIDTH = 640;
                float scaleHeight = (float)BOXHEIGHT / (float)original.Height;
                float scaleWidth = (float)BOXWIDTH / (float)original.Width;
                float scale = Math.Min(scaleHeight, scaleWidth);

                Bitmap resized = new Bitmap(original, (int)(original.Width * scale), (int)(original.Height * scale));

                //string resizedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\Test\\";
                string resizedPath = cls.CreateFolderMissing(Application.StartupPath + "\\Temp\\");

                //string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                string resizedName = cls.RemoveSpecialCharacters(Path.GetFileName(fleName.Replace(Path.GetExtension(fleName), null))) + "" + Path.GetExtension(fleName);

                string fullPath = resizedPath + "" + resizedName;
                _matImage = fullPath;

                pnl_ES_Reg_Tool_Image.BackgroundImage = Image.FromFile(dialog.FileName);
                pnl_ES_Reg_Tool_Image.BackgroundImageLayout = ((int)(original.Width) > (int)(pnl_ES_Reg_Tool_Image.Width) || (int)(original.Height) > (int)(pnl_ES_Reg_Tool_Image.Height)) ? ImageLayout.Stretch : ImageLayout.Center;

                resized.Save(fullPath);

                lnk_ES_Reg_Tool_Image_Add.Enabled = false;
                lnk_ES_Reg_Tool_Image_Upd.Enabled = true;
                lnk_ES_Reg_Tool_Image_Del.Enabled = true;
                _matNewImage = true;

            }
            else
            {
                lnk_ES_Reg_Tool_Image_Add.Enabled = true;
                lnk_ES_Reg_Tool_Image_Upd.Enabled = false;
                lnk_ES_Reg_Tool_Image_Del.Enabled = false;
                _matNewImage = false;
            }
        }

        private void lnk_ES_Reg_Tool_Image_Upd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string oldImage = _matImage;
            lnk_ES_Reg_Tool_Image_Add_LinkClicked(sender, e);
            if (_matImage != oldImage &&File.Exists(oldImage)) { File.Delete(oldImage); }
        }

        private void lnk_ES_Reg_Tool_Image_Del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (File.Exists(_matImage)) { File.Delete(_matImage); }
                _matImage = "";
                _matNewImage = false;
                pnl_ES_Reg_Tool_Image.BackgroundImage = global::Inventory_Data.Properties.Resources.no_image;
                pnl_ES_Reg_Tool_Image.BackgroundImageLayout = ImageLayout.Stretch;

                lnk_ES_Reg_Tool_Image_Add.Enabled = true;
                lnk_ES_Reg_Tool_Image_Upd.Enabled = false;
                lnk_ES_Reg_Tool_Image_Del.Enabled = false;
            }
        }

        private void dgv_ES_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                init_Reg_List();
                //cls.fnDatagridClickCell(dgv_ES_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_ES_List.Rows[e.RowIndex];
                string regIDx = row.Cells[1].Value.ToString();

                for (int i = 0; i < 15; i++)
                {
                    row.Cells[i].Style.SelectionBackColor = Color.LightSkyBlue;
                    row.Selected = true;
                }
                row.Cells[16].Style.SelectionBackColor = row.Cells[16].Style.BackColor;
                row.Cells[18].Style.SelectionBackColor = row.Cells[16].Style.BackColor;
                row.Cells[20].Style.SelectionBackColor = row.Cells[16].Style.BackColor;
                row.Cells[22].Style.SelectionBackColor = row.Cells[16].Style.BackColor;

                _regIDx = Convert.ToInt32(regIDx);
            }
        }

        private void dgv_ES_List_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_ES_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_ES_List.Rows[e.RowIndex];
                string regIDx = row.Cells[1].Value.ToString();

                frmPM_01WorkOrders_ES_Details frm = new frmPM_01WorkOrders_ES_Details(regIDx);
                frm.ShowDialog();
            }
        }


        #endregion

    }
}
