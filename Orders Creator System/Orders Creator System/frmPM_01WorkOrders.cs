using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmPM_01WorkOrders : Form
    {
        public int _dgv_Order_Repair_Width;
        public int _dgv_Mat_Repair_Parts_Width;
        public int _dgv_Mat_Repair_Order_Width;

        public int _dgv_Mat_Utility_List_Width;
        public int _dgv_Mat_Utility_Order_Width;

        public int _dgv_Mat_Material_List_Width;
        public int _dgv_Mat_Material_Order_Width;
        public int _dgv_Mat_Request_List_Width;

        public int _dgv_Mat_Stationary_List_Width;
        public int _dgv_Mat_Stationary_Order_Width;

        public int _dgv_Order_Parts_Width;
        public int _row_Order_Repair_Index = 0;
        public int _row_Order_Parts_Index = 0;

        public bool _login = false;
        public int _logIDx = 0;
        public int _logLevel = 0;
        public string _logName = "";
        public int _logDept = 0;

        public string _msgText;
        public int _msgType;

        public string _matImage = "";
        public bool _matNewImage;

        public int _rangeWOTime = 3;
        public int _rangePOTime = 3;
        public string _woIDx = "", _manApprove = "";

        public string _matIDx = "", _matName = "", _matCode = "", _matQty = "", _matUnit = "";
        public string _matUtiIDx = "", _matUtiName = "", _matUtiCode = "", _matUtiQty = "", _matUtiUnit = "";
        public string _matNvlIDx = "", _matNvlName = "", _matNvlCode = "", _matNvlQty = "", _matNvlUnit = "", _matNvlPCS = "", _matNvlBOX = "", _matNvlPAK = "", _matNvlPAL = "", _matPackType = "", _matPackQty = "";
        public string _matStaIDx = "", _matStaName = "", _matStaCode = "", _matStaQty = "", _matStaUnit = "";

        //public string _prodIDx = "", _prodName = "", _prodCode = "", _prodQty = "", _prodUnit = "";
        DataTable table = new DataTable(), table2 = new DataTable();
        DataColumn column, column2;
        DataRow row, row2;
        DataView view, view2;

        public DateTime _dt;
        Timer timer = new Timer();

        public string _setMessage { get; set; }

        public frmPM_01WorkOrders(int logIDx, int logLevel, string logName,int logDept)
        {
            InitializeComponent();

            if (logIDx > 0 && logLevel > 0 && logName != "")
            {
                _login = true;
                _logIDx = logIDx;
                _logLevel = logLevel;
                _logName = logName;
                _logDept = logDept;
            }
            else
            {
                this.Close();
                frmPM_00LoginSystem frmLogin = new frmPM_00LoginSystem(1);
                frmLogin.ShowDialog();
            }

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IDx";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Code";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Qty";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Unit";
            table.Columns.Add(column);


            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "IDx";
            table2.Columns.Add(column2);

            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.String");
            column2.ColumnName = "Name";
            table2.Columns.Add(column2);

            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.String");
            column2.ColumnName = "Code";
            table2.Columns.Add(column2);

            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Decimal");
            column2.ColumnName = "Qty";
            table2.Columns.Add(column2);

            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.String");
            column2.ColumnName = "Unit";
            table2.Columns.Add(column2);

            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "QtyC";
            table2.Columns.Add(column2);

            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.String");
            column2.ColumnName = "UnitC";
            table2.Columns.Add(column2);
        }

        private void frmPM_01WorkOrders_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            if (_logIDx > 0 && _logName.Length > 0)
            {
                tssLogName.Text = "Logged in: " + _logName.ToUpper();
            }
            else
            {
                tssLogName.Text = "";
            }

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            init_Order_Repair();
            init_Order_List();
            fnLinkColor();
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    init_Order_Repair();
                    init_Order_List();
                    fnLinkColor();
                    break;
                case 1:
                    //init_Order_Repair_Done();

                    init_Order_Parts();
                    fnLinkColorParts();
                    break;
                case 2:
                    init_Order_Material();
                    init_Order_Material_List();
                    init_Order_Material_Done(0);
                    break;
                case 3:
                    if (_logDept == 1)
                    {
                        pnl_Main.Controls.Clear();
                        if (!pnl_Main.Controls.Contains(Ctrl.uc_EntranceSystem_Register.Instance))
                        {
                            pnl_Main.Controls.Add(Ctrl.uc_EntranceSystem_Register.Instance);
                            Ctrl.uc_EntranceSystem_Register.Instance.init_Reg();
                        }
                        Ctrl.uc_EntranceSystem_Register.Instance.Dock = DockStyle.Fill;
                        Ctrl.uc_EntranceSystem_Register.Instance.BringToFront();
                    }
                    else
                    {
                        string msg_vn = "Chức năng đang được phát triển, vui lòng thử lại sau.";
                        string msg_en = "Function is being developed, please try again later.";
                        MessageBox.Show(msg_vn + "\r\n" + msg_en);
                        tabControl1.SelectedIndex = 0;
                    }

                    break;
                case 4:
                    //if (_logDept == 0 || _logDept == 1 || _logDept == 6)
                    if (_logDept == 0 || _logDept == 1)
                    {
                        pnl_Temperature.Controls.Clear();
                        if (!pnl_Temperature.Controls.Contains(Ctrl.uc_Temperature_LimitRange.Instance))
                        {
                            pnl_Temperature.Controls.Add(Ctrl.uc_Temperature_LimitRange.Instance);
                            Ctrl.uc_Temperature_LimitRange.Instance.init_Reg();
                        }
                        Ctrl.uc_Temperature_LimitRange.Instance.Dock = DockStyle.Fill;
                        Ctrl.uc_Temperature_LimitRange.Instance.BringToFront();
                    }
                    else
                    {
                        //string msg_vn = "Chức năng này chỉ dành cho các giám sát viên sản xuất cài đặt dải nhiệt độ cần theo dõi cho máy sản xuất";
                        //string msg_en = "This function is only for supervisors who will set the temperature range to be monitored for the production machine";
                        string msg_vn = "Chức năng đang được phát triển, vui lòng thử lại sau.";
                        string msg_en = "Function is being developed, please try again later.";
                        MessageBox.Show(msg_vn + "\r\n" + msg_en);
                        tabControl1.SelectedIndex = 0;
                    }
                    break;
                case 5:

                    break;
            }
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            if (tssMessage.Text.Length > 0)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            tssMessage.Text = "";
            tssMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
            tssMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            timer.Stop();
        }

        public void init_LogOut()
        {
            if (_login == true)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi tài khoản hiện tại?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    _login = false;
                    _logIDx = 0;
                    _logLevel = 0;
                    _logName = "";

                    this.Hide();
                    frmPM_00LoginSystem frmLogin = new frmPM_00LoginSystem(1);
                    frmLogin.ShowDialog();
                }
            }
        }

        private void tssLogName_Click(object sender, EventArgs e)
        {
            init_LogOut();
        }

        private void thoátChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void thoátKhỏiTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            init_LogOut();
        }

        private void đổiMậtKhẩuTruyCậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logIDx = _logIDx.ToString();
            frmPM_01WorkOrders_PW_Change frmPW = new frmPM_01WorkOrders_PW_Change(logIDx);
            frmPW.ShowDialog();
        }

        private void chiTiếtNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmPM_01WorkOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }



        #region WORK ORDERS


        public void init_Order_Repair()
        {
            init_Order_Repair_Equipment();
            init_Order_Repair_Priority();

            //init_Order_Repair_Done();
        }

        public void init_Order_Repair_Done()
        {
            cbb_Order_Repair_Equipment.SelectedIndex = 0;
            cbb_Order_Repair_Equipment.Enabled = true;
            cbb_Order_Repair_Machine.SelectedIndex = 0;
            cbb_Order_Repair_Machine.Enabled = false;
            txt_Order_Repair_Desc.Text = "";
            txt_Order_Repair_Desc.Enabled = false;
            cbb_Order_Repair_Priority.SelectedIndex = 0;
            cbb_Order_Repair_Priority.Enabled = false;
            dtp_Order_Repair_Finish.Value = _dt;
            dtp_Order_Repair_Finish.Enabled = false;
            txt_Order_Repair_Note.Text = "";
            txt_Order_Repair_Note.Enabled = false;
            lnk_Order_Repair_Picture_Browse.Enabled = false;
            lnk_Order_Repair_Picture_Remove.Enabled = false;
            pnl_Order_Repair_Picture.BackgroundImage = null;
            btn_Order_Repair_Save.Enabled = false;
            btn_Order_Repair_Done.Enabled = false;
        }

        public void init_Order_Repair_Equipment()
        {
            string sql = "V2o1_BASE_PM_01_Create_Work_Order_Equipment_SelItem_V1o0_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbb_Order_Repair_Equipment.DataSource = dt;
            cbb_Order_Repair_Equipment.DisplayMember = "cateName";
            cbb_Order_Repair_Equipment.ValueMember = "idx";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbb_Order_Repair_Equipment.SelectedIndex = 0;

        }

        public void init_Order_Repair_Priority()
        {
            cbb_Order_Repair_Priority.Items.Clear();
            cbb_Order_Repair_Priority.Items.Add("Mức CAO");
            cbb_Order_Repair_Priority.Items.Add("Mức TB");
            cbb_Order_Repair_Priority.Items.Add("Mức THẤP");
            cbb_Order_Repair_Priority.Items.Insert(0, "");
            cbb_Order_Repair_Priority.SelectedIndex = 0;

        }

        private void cbb_Order_Repair_Equipment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Order_Repair_Equipment.SelectedIndex > 0)
            {
                cbb_Order_Repair_Machine.Enabled = true;

                string equipmentIDx = cbb_Order_Repair_Equipment.SelectedValue.ToString();
                string sql = "V2o1_BASE_PM_01_Create_Work_Order_Machine_SelItem_V1o0_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@equipmentIDx";
                sParams[0].Value = equipmentIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);
                cbb_Order_Repair_Machine.DataSource = dt;
                cbb_Order_Repair_Machine.DisplayMember = "machineName";
                cbb_Order_Repair_Machine.ValueMember = "idx";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbb_Order_Repair_Machine.SelectedIndex = 0;

                btn_Order_Repair_Done.Enabled = true;
            }
            else
            {
                init_Order_Repair_Done();
                //cbb_Order_Repair_Machine.SelectedIndex = 0;
                //cbb_Order_Repair_Machine.Enabled = false;

                //btn_Order_Repair_Done.Enabled = false;
            }
        }

        private void cbb_Order_Repair_Machine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Order_Repair_Machine.SelectedIndex > 0)
            {
                txt_Order_Repair_Desc.Enabled = true;
                txt_Order_Repair_Desc.Focus();
            }
            else
            {
                txt_Order_Repair_Desc.Text = "";
                txt_Order_Repair_Desc.Enabled = false;
            }
        }

        private void txt_Order_Repair_Desc_TextChanged(object sender, EventArgs e)
        {
            if (txt_Order_Repair_Desc.Text.Length > 0)
            {
                cbb_Order_Repair_Priority.Enabled = true;
            }
            else
            {
                cbb_Order_Repair_Priority.SelectedIndex = 0;
                cbb_Order_Repair_Priority.Enabled = false;
            }
        }

        private void cbb_Order_Repair_Priority_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Order_Repair_Priority.SelectedIndex > 0)
            {
                dtp_Order_Repair_Finish.Enabled = true;
                txt_Order_Repair_Note.Enabled = true;
                lnk_Order_Repair_Picture_Browse.Enabled = true;

                //btn_Order_Repair_Save.Enabled = true;
            }
            else
            {
                dtp_Order_Repair_Finish.Value = _dt;
                dtp_Order_Repair_Finish.Enabled = false;

                txt_Order_Repair_Note.Text = "";
                txt_Order_Repair_Note.Enabled = false;

                pnl_Order_Repair_Picture.BackgroundImage = null;
                lnk_Order_Repair_Picture_Browse.Enabled = false;

                btn_Order_Repair_Save.Enabled = false;
            }
        }

        private void btn_Order_Repair_Done_Click(object sender, EventArgs e)
        {
            if (_matImage != "" && _matImage != null)
            {
                string pic = _matImage;
                if (File.Exists(pic))
                {
                    File.Delete(pic);
                }

                _matImage = "";
                _matNewImage = false;
            }

            init_Order_Repair_Done();
        }

        private void lnk_Order_Repair_Picture_Browse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //dialog.InitialDirectory = @"C:\";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.Title = "Chọn ảnh minh họa cho vật tư";


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

                string _prefix = _dt.Year + "" + _dt.Month + "" + _dt.Day + "" + _dt.Hour + "" + _dt.Minute + "" + _dt.Second;
                //string resizedName = "resize_" + String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now) + "" + Path.GetExtension(fleName);
                string resizedName = cls.RemoveSpecialCharacters(Path.GetFileName(fleName.Replace(Path.GetExtension(fleName), null))) + "" + Path.GetExtension(fleName);

                string fullPath = resizedPath + "" + _prefix + "_" + resizedName.ToLower();
                _matImage = fullPath;

                pnl_Order_Repair_Picture.BackgroundImage = Image.FromFile(dialog.FileName);
                pnl_Order_Repair_Picture.BackgroundImageLayout = ((int)(original.Width) > (int)(pnl_Order_Repair_Picture.Width) || (int)(original.Height) > (int)(pnl_Order_Repair_Picture.Height)) ? ImageLayout.Stretch : ImageLayout.Center;

                resized.Save(fullPath);

                lnk_Order_Repair_Picture_Browse.Enabled = false;
                lnk_Order_Repair_Picture_Remove.Enabled = true;
                _matNewImage = true;

                btn_Order_Repair_Save.Enabled = true;
            }
            else
            {
                _matNewImage = false;

                btn_Order_Repair_Save.Enabled = false;
            }
        }

        private void lnk_Order_Repair_Picture_Remove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {

                string pic = _matImage;
                if (File.Exists(pic))
                {
                    File.Delete(pic);
                }

                _matImage = "";
                _matNewImage = false;

                pnl_Order_Repair_Picture.BackgroundImage = null;

                lnk_Order_Repair_Picture_Browse.Enabled = true;
                lnk_Order_Repair_Picture_Remove.Enabled = false;

                btn_Order_Repair_Save.Enabled = false;

                //if (_matIDx != "" && _matIDx != null)
                //{
                //    try
                //    {
                //        string matIDx = _matIDx;
                //        string sql = "V2o1_BASE_SubMaterial_Init_Material_Addnew_Image_DelItem_Addnew";

                //        SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                //        sParams[0] = new SqlParameter();
                //        sParams[0].SqlDbType = SqlDbType.Int;
                //        sParams[0].ParameterName = "@matIDx";
                //        sParams[0].Value = matIDx;

                //        cls.fnUpdDel(sql, sParams);

                //        _msgText = "Xóa ảnh thành công.";
                //        _msgType = 1;
                //    }
                //    catch (SqlException sqlEx)
                //    {
                //        _msgText = "Có lỗi dữ liệu phát sinh.";
                //        _msgType = 3;
                //        MessageBox.Show(sqlEx.ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        _msgText = "Có lỗi phát sinh.";
                //        _msgType = 2;
                //        MessageBox.Show(ex.ToString());
                //    }
                //    finally
                //    {
                //        cls.fnMessage(tssMsg, _msgText, _msgType);
                //        _ms = null;

                //        panel1.BackgroundImage = null;
                //        //if (picMat_Image.Image != null)
                //        //{
                //        //    picMat_Image.Image.Dispose();
                //        //    picMat_Image.Image = null;
                //        //}
                //        lnkMat_Clear.Enabled = false;
                //        fnBindData_All();
                //    }
                //}

            }
        }

        private void btn_Order_Repair_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    if (_login == true)
                    {
                        string logUserIDx = _logIDx.ToString();
                        string logUsername = _logName;
                        string logUserLevel = _logLevel.ToString();

                        string equipmentIDx = cbb_Order_Repair_Equipment.SelectedValue.ToString();
                        string equipmentName = cbb_Order_Repair_Equipment.Text;
                        string machineIDx = cbb_Order_Repair_Machine.SelectedValue.ToString();
                        string machineName = cbb_Order_Repair_Machine.Text;
                        string machineDesc = txt_Order_Repair_Desc.Text.Trim();
                        int priority = cbb_Order_Repair_Priority.SelectedIndex;
                        DateTime dateFinish = dtp_Order_Repair_Finish.Value;
                        string remark = txt_Order_Repair_Note.Text.Trim();

                        string imgPath = _matImage;
                        Bitmap bmp = new Bitmap(imgPath);
                        FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
                        byte[] bimage = new byte[fs.Length];
                        fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                        fs.Close();

                        string sql = "PMMS_01_Create_Repair_Work_Order_V1o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[12]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@picIDx";
                        sParams[0].Value = logUserIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.NVarChar;
                        sParams[1].ParameterName = "@picName";
                        sParams[1].Value = logUsername;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.TinyInt;
                        sParams[2].ParameterName = "@picLevel";
                        sParams[2].Value = logUserLevel;

                        sParams[3] = new SqlParameter();
                        sParams[3].SqlDbType = SqlDbType.Int;
                        sParams[3].ParameterName = "@equipmentIDx";
                        sParams[3].Value = equipmentIDx;

                        sParams[4] = new SqlParameter();
                        sParams[4].SqlDbType = SqlDbType.NVarChar;
                        sParams[4].ParameterName = "@equipmentName";
                        sParams[4].Value = equipmentName;

                        sParams[5] = new SqlParameter();
                        sParams[5].SqlDbType = SqlDbType.Int;
                        sParams[5].ParameterName = "@machineIDx";
                        sParams[5].Value = machineIDx;

                        sParams[6] = new SqlParameter();
                        sParams[6].SqlDbType = SqlDbType.NVarChar;
                        sParams[6].ParameterName = "@machineName";
                        sParams[6].Value = machineName;

                        sParams[7] = new SqlParameter();
                        sParams[7].SqlDbType = SqlDbType.NVarChar;
                        sParams[7].ParameterName = "@machineDesc";
                        sParams[7].Value = machineDesc;

                        sParams[8] = new SqlParameter();
                        sParams[8].SqlDbType = SqlDbType.TinyInt;
                        sParams[8].ParameterName = "@orderPriority";
                        sParams[8].Value = priority;

                        sParams[9] = new SqlParameter();
                        sParams[9].SqlDbType = SqlDbType.DateTime;
                        sParams[9].ParameterName = "@dateFinish";
                        sParams[9].Value = dateFinish;

                        sParams[10] = new SqlParameter();
                        sParams[10].SqlDbType = SqlDbType.NVarChar;
                        sParams[10].ParameterName = "@orderNote";
                        sParams[10].Value = remark;

                        sParams[11] = new SqlParameter();
                        sParams[11].SqlDbType = SqlDbType.Image;
                        sParams[11].ParameterName = "@image";
                        sParams[11].Value = bimage;

                        cls.fnUpdDel(sql, sParams);
                        bmp = null;

                        _msgText = "Thêm mới vật tư thành công.";
                        _msgType = 1;

                    }
                }
                catch (SqlException sqlEx)
                {
                    _msgText = "Có lỗi dữ liệu phát sinh, vui lòng báo lại quản trị hệ thống.";
                    _msgType = 3;
                }
                catch(Exception ex)
                {
                    _msgText = "Có lỗi phát sinh, vui lòng báo lại quản trị hệ thống.";
                    _msgType = 2;
                }
                finally
                {
                    init_Order_Repair_Done();
                    init_Order_List();

                    //if (_matImage != "" && _matImage != null)
                    //{
                    //    string pic = _matImage;
                    //    if (File.Exists(pic))
                    //    {
                    //        File.Delete(pic);
                    //    }

                    //    _matImage = "";
                    //    _matNewImage = false;
                    //}

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion



        #region ORDER LIST


        public void init_Order_List()
        {
            string sql = "PMMS_01_View_Repair_Work_Order_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = _logIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@rangeTime";
            sParams[1].Value = _rangeWOTime;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Order_Repair_Width = cls.fnGetDataGridWidth(dgv_Order_Repair);
            dgv_Order_Repair.DataSource = dt;

            dgv_Order_Repair.Columns[0].Width = 5 * _dgv_Order_Repair_Width / 100;    // STT
            //dgv_Order_Repair.Columns[1].Width = 10 * _dgv_Order_Repair_Width / 100;    // idx
            //dgv_Order_Repair.Columns[2].Width = 10 * _dgv_Order_Repair_Width / 100;    // picIDx
            dgv_Order_Repair.Columns[3].Width = 13 * _dgv_Order_Repair_Width / 100;    // picName
            //dgv_Order_Repair.Columns[4].Width = 10 * _dgv_Order_Repair_Width / 100;    // picLevel
            //dgv_Order_Repair.Columns[5].Width = 10 * _dgv_Order_Repair_Width / 100;    // equipmentIDx
            dgv_Order_Repair.Columns[6].Width = 12 * _dgv_Order_Repair_Width / 100;    // equipmentName
            //dgv_Order_Repair.Columns[7].Width = 10 * _dgv_Order_Repair_Width / 100;    // machineIDx
            dgv_Order_Repair.Columns[8].Width = 10 * _dgv_Order_Repair_Width / 100;    // machineName
            dgv_Order_Repair.Columns[9].Width = 16 * _dgv_Order_Repair_Width / 100;    // machineDesc
            dgv_Order_Repair.Columns[10].Width = 7 * _dgv_Order_Repair_Width / 100;    // orderPriority
            dgv_Order_Repair.Columns[11].Width = 10 * _dgv_Order_Repair_Width / 100;    // dateFinish
            //dgv_Order_Repair.Columns[12].Width = 10 * _dgv_Order_Repair_Width / 100;    // orderNote
            dgv_Order_Repair.Columns[13].Width = 12 * _dgv_Order_Repair_Width / 100;    // added
            //dgv_Order_Repair.Columns[14].Width = 10 * _dgv_Order_Repair_Width / 100;    // makerIDx
            //dgv_Order_Repair.Columns[15].Width = 10 * _dgv_Order_Repair_Width / 100;    // makerName
            //dgv_Order_Repair.Columns[16].Width = 10 * _dgv_Order_Repair_Width / 100;    // makerApprove
            //dgv_Order_Repair.Columns[17].Width = 10 * _dgv_Order_Repair_Width / 100;    // makerApproveDate
            //dgv_Order_Repair.Columns[18].Width = 10 * _dgv_Order_Repair_Width / 100;    // managerIDx
            //dgv_Order_Repair.Columns[19].Width = 10 * _dgv_Order_Repair_Width / 100;    // managerName
            //dgv_Order_Repair.Columns[20].Width = 10 * _dgv_Order_Repair_Width / 100;    // managerApprove
            //dgv_Order_Repair.Columns[21].Width = 10 * _dgv_Order_Repair_Width / 100;    // managerApproveNote
            //dgv_Order_Repair.Columns[22].Width = 10 * _dgv_Order_Repair_Width / 100;    // managerApproveDate
            //dgv_Order_Repair.Columns[23].Width = 10 * _dgv_Order_Repair_Width / 100;    // pmIDx
            //dgv_Order_Repair.Columns[24].Width = 10 * _dgv_Order_Repair_Width / 100;    // pmName
            //dgv_Order_Repair.Columns[25].Width = 10 * _dgv_Order_Repair_Width / 100;    // pmFinish
            //dgv_Order_Repair.Columns[26].Width = 10 * _dgv_Order_Repair_Width / 100;    // pmFinishNote
            //dgv_Order_Repair.Columns[27].Width = 10 * _dgv_Order_Repair_Width / 100;    // pmFinishDate
            //dgv_Order_Repair.Columns[28].Width = 10 * _dgv_Order_Repair_Width / 100;    // confirmIDx
            //dgv_Order_Repair.Columns[29].Width = 10 * _dgv_Order_Repair_Width / 100;    // confirmName
            //dgv_Order_Repair.Columns[30].Width = 10 * _dgv_Order_Repair_Width / 100;    // confirmClosed
            //dgv_Order_Repair.Columns[31].Width = 10 * _dgv_Order_Repair_Width / 100;    // confirmClosedNote
            //dgv_Order_Repair.Columns[32].Width = 10 * _dgv_Order_Repair_Width / 100;    // confirmClosedDate
            dgv_Order_Repair.Columns[33].Width = 3 * _dgv_Order_Repair_Width / 100;    // makerStatus
            dgv_Order_Repair.Columns[34].Width = 3 * _dgv_Order_Repair_Width / 100;    // managerStatus
            dgv_Order_Repair.Columns[35].Width = 3 * _dgv_Order_Repair_Width / 100;    // repairStatus
            dgv_Order_Repair.Columns[36].Width = 3 * _dgv_Order_Repair_Width / 100;    // confirmStatus
            dgv_Order_Repair.Columns[37].Width = 3 * _dgv_Order_Repair_Width / 100;    // orderClosed (blank)
            dgv_Order_Repair.Columns[38].Width = 3 * _dgv_Order_Repair_Width / 100;    // orderClosed (value)
            dgv_Order_Repair.Columns[39].Width = 3 * _dgv_Order_Repair_Width / 100;    // closedDate
            dgv_Order_Repair.Columns[40].Width = 3 * _dgv_Order_Repair_Width / 100;    // takeTime

            dgv_Order_Repair.Columns[0].Visible = true;
            dgv_Order_Repair.Columns[1].Visible = false;
            dgv_Order_Repair.Columns[2].Visible = false;
            dgv_Order_Repair.Columns[3].Visible = true;
            dgv_Order_Repair.Columns[4].Visible = false;
            dgv_Order_Repair.Columns[5].Visible = false;
            dgv_Order_Repair.Columns[6].Visible = true;
            dgv_Order_Repair.Columns[7].Visible = false;
            dgv_Order_Repair.Columns[8].Visible = true;
            dgv_Order_Repair.Columns[9].Visible = true;
            dgv_Order_Repair.Columns[10].Visible = true;
            dgv_Order_Repair.Columns[11].Visible = true;
            dgv_Order_Repair.Columns[12].Visible = false;
            dgv_Order_Repair.Columns[13].Visible = true;
            dgv_Order_Repair.Columns[14].Visible = false;
            dgv_Order_Repair.Columns[15].Visible = false;
            dgv_Order_Repair.Columns[16].Visible = false;
            dgv_Order_Repair.Columns[17].Visible = false;
            dgv_Order_Repair.Columns[18].Visible = false;
            dgv_Order_Repair.Columns[19].Visible = false;
            dgv_Order_Repair.Columns[20].Visible = false;
            dgv_Order_Repair.Columns[21].Visible = false;
            dgv_Order_Repair.Columns[22].Visible = false;
            dgv_Order_Repair.Columns[23].Visible = false;
            dgv_Order_Repair.Columns[24].Visible = false;
            dgv_Order_Repair.Columns[25].Visible = false;
            dgv_Order_Repair.Columns[26].Visible = false;
            dgv_Order_Repair.Columns[27].Visible = false;
            dgv_Order_Repair.Columns[28].Visible = false;
            dgv_Order_Repair.Columns[29].Visible = false;
            dgv_Order_Repair.Columns[30].Visible = false;
            dgv_Order_Repair.Columns[31].Visible = false;
            dgv_Order_Repair.Columns[32].Visible = false;
            dgv_Order_Repair.Columns[33].Visible = true;
            dgv_Order_Repair.Columns[34].Visible = true;
            dgv_Order_Repair.Columns[35].Visible = true;
            dgv_Order_Repair.Columns[36].Visible = true;
            dgv_Order_Repair.Columns[37].Visible = true;
            dgv_Order_Repair.Columns[38].Visible = false;
            dgv_Order_Repair.Columns[39].Visible = false;
            dgv_Order_Repair.Columns[40].Visible = false;


            dgv_Order_Repair.Columns[11].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv_Order_Repair.Columns[13].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            dgv_Order_Repair.Columns[33].HeaderText = "M";
            dgv_Order_Repair.Columns[34].HeaderText = "A";
            dgv_Order_Repair.Columns[35].HeaderText = "R";
            dgv_Order_Repair.Columns[36].HeaderText = "C";
            dgv_Order_Repair.Columns[37].HeaderText = "T";

            cls.fnFormatDatagridview(dgv_Order_Repair, 11, 30);


            string makerName = "", makerApprove = "", makerApproveDate = "", orderClosed = "";
            string managerName = "", managerApprove = "", managerApproveNote = "", managerApproveDate = "";
            string repairName = "", repairApprove = "", repairApproveNote = "", repairApproveDate = "";
            string confirmName = "", confirmApprove = "", confirmApproveNote = "", confirmApproveDate = "";

            int _orderClosed = 0, _makerApprove = 0, _managerApprove = 0, _repairApprove = 0, _confirmApprove = 0;

            foreach (DataGridViewRow row in dgv_Order_Repair.Rows)
            {
                makerName = row.Cells[15].Value.ToString();
                makerApprove = row.Cells[16].Value.ToString();
                makerApproveDate = row.Cells[17].Value.ToString();
                _makerApprove = (makerApprove != "" && makerApprove != null) ? Convert.ToInt32(makerApprove) : 0;

                managerName = row.Cells[19].Value.ToString();
                managerApprove = row.Cells[20].Value.ToString();
                managerApproveNote = row.Cells[21].Value.ToString();
                managerApproveDate = row.Cells[22].Value.ToString();
                _managerApprove = (managerApprove != "" && managerApprove != null) ? Convert.ToInt32(managerApprove) : 0;

                repairName = row.Cells[24].Value.ToString();
                repairApprove = row.Cells[25].Value.ToString();
                repairApproveNote = row.Cells[26].Value.ToString();
                repairApproveDate = row.Cells[27].Value.ToString();
                _repairApprove = (repairApprove != "" && repairApprove != null) ? Convert.ToInt32(repairApprove) : 0;

                confirmName = row.Cells[28].Value.ToString();
                confirmApprove = row.Cells[30].Value.ToString();
                confirmApproveNote = row.Cells[31].Value.ToString();
                confirmApproveDate = row.Cells[32].Value.ToString();
                _confirmApprove = (confirmApprove != "" && confirmApprove != null) ? Convert.ToInt32(confirmApprove) : 0;

                if (_makerApprove == 1 && _managerApprove == 1 && _repairApprove == 1 && _confirmApprove == 1)
                {
                    // APPROVED - FINISH
                    _orderClosed = 1;
                }
                else if (_makerApprove == 2 || _managerApprove == 2 || _repairApprove == 2 || _confirmApprove == 2)
                {
                    // REJECT
                    _orderClosed = 2;
                }
                else
                {
                    // DOING or NEARLY FINISH
                    orderClosed = row.Cells[38].Value.ToString();
                    _orderClosed = (orderClosed != "" && orderClosed != null) ? Convert.ToInt32(orderClosed) : 0;
                }

                //_orderClosed = (_makerApprove == 1 && _managerApprove == 1 && _repairApprove == 1 && _confirmApprove == 1) ? 1 : 2;

                row.Cells[33].Style.BackColor = (_makerApprove == 1) ? Color.LightGreen : Color.Gray;
                switch (_managerApprove)
                {
                    case 0:
                        // NEW
                        row.Cells[34].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[34].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // DOING
                        row.Cells[34].Style.BackColor = Color.OrangeRed;
                        break;
                    case 3:
                        // PENDING
                        row.Cells[34].Style.BackColor = Color.LemonChiffon;
                        break;
                }

                switch (_repairApprove)
                {
                    case 0:
                        // NEW
                        row.Cells[35].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[35].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // REJECT
                        row.Cells[35].Style.BackColor = Color.Red;
                        break;
                    case 3:
                        // NOT ASSIGN
                        row.Cells[35].Style.BackColor = Color.LemonChiffon;
                        break;
                    case 4:
                        // DOING
                        row.Cells[35].Style.BackColor = Color.Yellow;
                        break;
                    case 5:
                        // PENDING
                        row.Cells[35].Style.BackColor = Color.Orange;
                        break;
                }

                switch (_confirmApprove)
                {
                    case 0:
                        // NEW
                        row.Cells[36].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[36].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // REJECT
                        row.Cells[36].Style.BackColor = Color.Red;
                        break;
                    case 3:
                        // DONG
                        row.Cells[36].Style.BackColor = Color.YellowGreen;
                        break;
                }

                switch (_orderClosed)
                {
                    case 0:
                        // NEW
                        row.Cells[37].Style.BackColor = Color.Gray;
                        break;
                    case 1:
                        // FINISH
                        row.Cells[37].Style.BackColor = Color.LightGreen;
                        break;
                    case 2:
                        // DOING
                        row.Cells[37].Style.BackColor = Color.Red;
                        break;
                    case 3:
                        // PENDING
                        row.Cells[37].Style.BackColor = Color.Yellow;
                        break;
                    case 4:
                        // PENDING
                        row.Cells[37].Style.BackColor = Color.OliveDrab;
                        break;
                }

            }
        }

        private void lnk_Order_Repair_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 1;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 2;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Week_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 3;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_10Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 4;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 5;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_2Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 6;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_3Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 7;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_6Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 8;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_9Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 9;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        private void lnk_Order_Repair_1Year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeWOTime = 10;
            dgv_Order_Repair.DataSource = "";
            init_Order_List();
            fnLinkColor();
        }

        public void fnLinkColor()
        {
            switch (_rangeWOTime)
            {
                case 1:
                    lnk_Order_Repair_Today.LinkColor = Color.Red;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 2:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Red;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 3:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Red;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 4:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Red;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 5:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Red;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 6:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Red;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 7:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Red;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 8:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Red;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 9:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Red;
                    lnk_Order_Repair_1Year.LinkColor = Color.Blue;
                    break;
                case 10:
                    lnk_Order_Repair_Today.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Week.LinkColor = Color.Blue;
                    lnk_Order_Repair_10Days.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Month.LinkColor = Color.Blue;
                    lnk_Order_Repair_2Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_3Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_6Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_9Months.LinkColor = Color.Blue;
                    lnk_Order_Repair_1Year.LinkColor = Color.Red;
                    break;
            }
        }

        private void dgv_Order_Repair_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Order_Repair_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                //cls.fnDatagridClickCell(dgv_Order_Repair, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Order_Repair.Rows[e.RowIndex];
                for(int i = 0; i <= 31; i++)
                {
                    row.Cells[i].Style.SelectionBackColor = Color.LightSkyBlue;
                }
                row.Cells[33].Style.SelectionBackColor = row.Cells[33].Style.BackColor;
                row.Cells[34].Style.SelectionBackColor = row.Cells[34].Style.BackColor;
                row.Cells[35].Style.SelectionBackColor = row.Cells[35].Style.BackColor;
                row.Cells[36].Style.SelectionBackColor = row.Cells[36].Style.BackColor;
                row.Cells[37].Style.SelectionBackColor = row.Cells[37].Style.BackColor;
                row.Selected = true;

                string woIDx = row.Cells[1].Value.ToString();
                string manApprove = row.Cells[20].Value.ToString();

                _woIDx = woIDx;
                _manApprove = manApprove;
            }
        }

        private void dgv_Order_Repair_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgv_Order_Repair.Rows[e.RowIndex].Selected = true;
                _row_Order_Repair_Index = e.RowIndex;
                dgv_Order_Repair.CurrentCell = dgv_Order_Repair.Rows[e.RowIndex].Cells[0];
                cms_Order_Repair.Show(this.dgv_Order_Repair, e.Location);
                cms_Order_Repair.Show(Cursor.Position);
            }
        }

        private void tảiLạiDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            init_Order_List();
        }

        private void xemChiTiếtYêuCầuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string woIDx = _woIDx;

            frmPM_01WorkOrders_WO_Details frmWO_Detail = new frmPM_01WorkOrders_WO_Details(woIDx);
            frmWO_Detail.ShowDialog();
        }

        private void xóaWOĐangChọnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_manApprove != "1")
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        string woIDx = _woIDx;
                        string sql = "PMMS_01_Delete_Repair_Work_Order_V1o0_Addnew";
                        SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@woIDx";
                        sParams[0].Value = woIDx;

                        cls.fnUpdDel(sql, sParams);

                        _msgText = "Xóa yêu cầu sửa chữa thành công.";
                        _msgType = 1;
                    }
                    catch (SqlException sqlEx)
                    {
                        _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng báo lại quản trị hệ thống";
                        _msgType = 3;
                    }
                    catch (Exception ex)
                    {
                        _msgText = "Có lỗi phát sinh. Vui lòng báo lại quản trị hệ thống";
                        _msgType = 2;
                    }
                    finally
                    {
                        _woIDx = "";
                        init_Order_List();
                        cls.fnMessage(tssMessage, _msgText, _msgType);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa yêu cầu sửa chữa đã được phê duyệt.\r\nVui lòng liên hệ với cấp trên để xóa được WO này.");
            }
        }

        private void cms_Order_Repair_Opening(object sender, CancelEventArgs e)
        {
            string manApprove = _manApprove;
            cms_Order_Repair.Items[3].Enabled = (manApprove == "1") ? false : true;
        }


        #endregion



        #region ORDER PARTS


        public void init_Order_Parts()
        {
            init_Order_Parts_Equipment();
            init_Order_Parts_List();

            _matIDx = "";
            _matName = "";
            _matCode = "";
            _matQty = "";
            _matUnit = "";

            table.Rows.Clear();

            //cbb_Mat_Repair_Equipment.SelectedIndex = 0;
            cbb_Mat_Repair_Equipment.Enabled = true;
            //cbb_Mat_Repair_Machine.SelectedIndex = 0;
            cbb_Mat_Repair_Machine.Enabled = false;
            dgv_Mat_Repair_Parts.DataSource = "";
            txt_Mat_Repair_Qty_Need.Text = "";
            txt_Mat_Repair_Qty_Need.Enabled = false;
            txt_Mat_Repair_Filter.Text = "";
            txt_Mat_Repair_Filter.Enabled = false;
            lnk_Mat_Repair_Add.Enabled = false;
            lnk_Mat_Repair_Del.Enabled = false;
            dgv_Mat_Repair_Order.DataSource = "";
            txt_Mat_Repair_Reason.Text = "";
            txt_Mat_Repair_Reason.Enabled = false;
            btn_Mat_Repair_Save.Enabled = false;
            btn_Mat_Repair_Done.Enabled = false;

        }

        public void init_Order_Parts_Equipment()
        {
            string sql = "V2o1_BASE_PM_01_Create_Work_Order_Equipment_SelItem_V1o0_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbb_Mat_Repair_Equipment.DataSource = dt;
            cbb_Mat_Repair_Equipment.DisplayMember = "cateName";
            cbb_Mat_Repair_Equipment.ValueMember = "idx";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbb_Mat_Repair_Equipment.SelectedIndex = 0;
        }

        public void init_Order_Parts_Done()
        {
            _matIDx = "";
            _matName = "";
            _matCode = "";
            _matQty = "";
            _matUnit = "";

            table.Rows.Clear();

            cbb_Mat_Repair_Equipment.SelectedIndex = 0;
            cbb_Mat_Repair_Equipment.Enabled = true;
            cbb_Mat_Repair_Machine.SelectedIndex = 0;
            cbb_Mat_Repair_Machine.Enabled = false;
            dgv_Mat_Repair_Parts.DataSource = "";
            txt_Mat_Repair_Qty_Need.Text = "";
            txt_Mat_Repair_Qty_Need.Enabled = false;
            txt_Mat_Repair_Filter.Text = "";
            txt_Mat_Repair_Filter.Enabled = false;
            lnk_Mat_Repair_Add.Enabled = false;
            lnk_Mat_Repair_Del.Enabled = false;
            dgv_Mat_Repair_Order.DataSource = "";
            txt_Mat_Repair_Reason.Text = "";
            txt_Mat_Repair_Reason.Enabled = false;
            btn_Mat_Repair_Save.Enabled = false;
            btn_Mat_Repair_Done.Enabled = false;
        }

        public string initMR_Code()
        {
            string codeMR = "";
            string currCodeMR = "", nextCodeMR = "";
            int _currCodeMR = 0, _nextCodeMR = 0;

            string sql = "V2o1_BASE_SubMaterial03_Init_MakeRequest_Code_SelItem_Addnew";

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    currCodeMR = ds.Tables[0].Rows[0][0].ToString();
                    codeMR = cls.Right(currCodeMR, 4);
                    _currCodeMR = (codeMR != "" && codeMR != null) ? Convert.ToInt32(codeMR) : 0;
                    _nextCodeMR = _currCodeMR + 1;
                    nextCodeMR = "RSBM-00-" + String.Format("{0:yyyyMMdd}", _dt) + String.Format("{0:0000}", _nextCodeMR);
                }
                else
                {
                    nextCodeMR = "RSBM-00-" + String.Format("{0:yyyyMMdd}", _dt) + "0001";
                }
            }
            else
            {
                nextCodeMR = "RSBM-00-" + String.Format("{0:yyyyMMdd}", _dt) + "0001";
            }
            //try
            //{

            //}
            //catch
            //{

            //}
            //finally
            //{

            //}
            return nextCodeMR;
        }

        private void cbb_Mat_Repair_Equipment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Mat_Repair_Equipment.SelectedIndex > 0)
            {
                cbb_Mat_Repair_Machine.Enabled = true;

                string equipmentIDx = cbb_Mat_Repair_Equipment.SelectedValue.ToString();
                string sql = "V2o1_BASE_PM_01_Create_Work_Order_Machine_SelItem_V1o0_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@equipmentIDx";
                sParams[0].Value = equipmentIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);
                cbb_Mat_Repair_Machine.DataSource = dt;
                cbb_Mat_Repair_Machine.DisplayMember = "machineName";
                cbb_Mat_Repair_Machine.ValueMember = "idx";
                dt.Rows.InsertAt(dt.NewRow(), 0);
                cbb_Mat_Repair_Machine.SelectedIndex = 0;

                btn_Mat_Repair_Done.Enabled = true;
            }
            else
            {
                cbb_Mat_Repair_Machine.Enabled = false;
                //init_Order_Repair_Done();
                ////cbb_Order_Repair_Machine.SelectedIndex = 0;
                ////cbb_Order_Repair_Machine.Enabled = false;

                btn_Mat_Repair_Done.Enabled = false;
            }
                
                dgv_Mat_Repair_Parts.DataSource="";
                txt_Mat_Repair_Qty_Need.Text="";
                txt_Mat_Repair_Qty_Need.Enabled=false;
                lnk_Mat_Repair_Add.Enabled=false;
                lnk_Mat_Repair_Del.Enabled=false;
                txt_Mat_Repair_Filter.Text="";
                txt_Mat_Repair_Filter.Enabled=false;
                dgv_Mat_Repair_Order.DataSource="";
                txt_Mat_Repair_Reason.Text="";
                txt_Mat_Repair_Reason.Enabled=false;
                btn_Mat_Repair_Save.Enabled=false;
        }

        private void cbb_Mat_Repair_Machine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Mat_Repair_Machine.SelectedIndex > 0)
            {
                string matQty = "";
                int _matQty = 0;
                string sql = "PMMS_01_Order_Material_List_SelItem_V1o0_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                _dgv_Mat_Repair_Parts_Width = cls.fnGetDataGridWidth(dgv_Mat_Repair_Parts);
                dgv_Mat_Repair_Parts.DataSource = dt;

                dgv_Mat_Repair_Parts.Columns[0].Width = 15 * _dgv_Mat_Repair_Parts_Width / 100;    // STT
                //dgv_Mat_Repair_Parts.Columns[1].Width = 5 * _dgv_Mat_Repair_Parts_Width / 100;    // ProdId
                dgv_Mat_Repair_Parts.Columns[2].Width = 55 * _dgv_Mat_Repair_Parts_Width / 100;    // Name
                //dgv_Mat_Repair_Parts.Columns[3].Width = 5 * _dgv_Mat_Repair_Parts_Width / 100;    // BarCode
                dgv_Mat_Repair_Parts.Columns[4].Width = 15 * _dgv_Mat_Repair_Parts_Width / 100;    // Total
                dgv_Mat_Repair_Parts.Columns[5].Width = 15 * _dgv_Mat_Repair_Parts_Width / 100;    // Uom

                dgv_Mat_Repair_Parts.Columns[0].Visible = true;
                dgv_Mat_Repair_Parts.Columns[1].Visible = false;
                dgv_Mat_Repair_Parts.Columns[2].Visible = true;
                dgv_Mat_Repair_Parts.Columns[3].Visible = false;
                dgv_Mat_Repair_Parts.Columns[4].Visible = true;
                dgv_Mat_Repair_Parts.Columns[5].Visible = true;

                cls.fnFormatDatagridview(dgv_Mat_Repair_Parts, 11, 30);

                dgv_Mat_Repair_Parts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                foreach (DataGridViewRow row in dgv_Mat_Repair_Parts.Rows)
                {
                    matQty = row.Cells[4].Value.ToString();
                    _matQty = (matQty != "" && matQty != null) ? Convert.ToInt32(matQty) : 0;

                    if (_matQty == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Silver;
                    }
                }

                txt_Mat_Repair_Filter.Enabled = true;
                txt_Mat_Repair_Filter.Focus();
            }
            else
            {
                dgv_Mat_Repair_Parts.DataSource = "";

                txt_Mat_Repair_Filter.Text = "";
                txt_Mat_Repair_Filter.Enabled = false;
            }
        }

        private void dgv_Mat_Repair_Parts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Mat_Repair_Parts, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Mat_Repair_Parts.Rows[e.RowIndex];

                string matIDx = row.Cells[1].Value.ToString();
                string matName = row.Cells[2].Value.ToString();
                string matCode = row.Cells[3].Value.ToString();
                string matQty = row.Cells[4].Value.ToString();
                string matUnit = row.Cells[5].Value.ToString();

                _matIDx = matIDx;
                _matName = matName;
                _matCode = matCode;
                _matQty = matQty;
                _matUnit = matUnit;

                if (matQty != "0")
                {
                    txt_Mat_Repair_Qty_Need.Text = "1";
                    txt_Mat_Repair_Qty_Need.Enabled = true;
                    lnk_Mat_Repair_Add.Enabled = true;
                }
                else
                {
                    txt_Mat_Repair_Qty_Need.Text = "0";
                    txt_Mat_Repair_Qty_Need.Enabled = false;
                    lnk_Mat_Repair_Add.Enabled = false;
                }
            }
        }

        private void txt_Mat_Repair_Filter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cls.fnFilterDatagridRow(dgv_Mat_Repair_Parts, txt_Mat_Repair_Filter, 2);
            }
            catch
            {
                
            }
            finally
            {

            }
        }

        private void txt_Mat_Repair_Qty_Need_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_Mat_Repair_Qty_Need_Click(object sender, EventArgs e)
        {
            txt_Mat_Repair_Qty_Need.SelectAll();
        }

        private void txt_Mat_Repair_Qty_Need_TextChanged(object sender, EventArgs e)
        {
            string haveQty = _matQty;
            string needQty = txt_Mat_Repair_Qty_Need.Text.Trim();

            int _haveQty = 0, _needQty = 0;

            _haveQty = (haveQty != "" && haveQty != null) ? Convert.ToInt32(haveQty) : 0;
            _needQty = (needQty != "" && needQty != null) ? Convert.ToInt32(needQty) : 0;

            if (_needQty > _haveQty)
            {
                txt_Mat_Repair_Qty_Need.Text = _haveQty.ToString();
            }
            else
            {
                txt_Mat_Repair_Qty_Need.Text = (needQty == "" || needQty == null) ? "0" : _needQty.ToString();
            }

            lnk_Mat_Repair_Add.Enabled = (_needQty > 1 && _needQty <= _haveQty) ? true : false;
        }

        private void lnk_Mat_Repair_Add_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int prodIDx = (_matIDx != "" && _matIDx != null) ? Convert.ToInt32(_matIDx) : 0;
            string prodName = _matName;
            string prodCode = _matCode;
            //int prodQty = (_matQty != "" && _matQty != null) ? Convert.ToInt32(_matQty) : 0;
            int prodQty = (txt_Mat_Repair_Qty_Need.Text != "" && txt_Mat_Repair_Qty_Need.Text != null) ? Convert.ToInt32(txt_Mat_Repair_Qty_Need.Text) : 0;
            string prodUnit = _matUnit;

            if (prodIDx > 0 && prodQty > 0)
            {
                if (dgv_Mat_Repair_Order.Rows.Count > 0)
                {
                    string matOrdered = "";
                    foreach(DataGridViewRow row in dgv_Mat_Repair_Order.Rows)
                    {
                        matOrdered = row.Cells[0].Value.ToString();
                        if (matOrdered == _matIDx)
                        {
                            MessageBox.Show("MÃ VẬT TƯ ĐÃ CÓ TRONG DANH SÁCH YÊU CẦU NÀY.\r\nKhông thể yêu cầu nhiều lần cho 1 mã vật tư\r\n\r\nVui lòng xóa mã này (" + prodName + ") trước khi thêm mới.");
                            return;
                        }
                    }
                }

                table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
                view = new DataView(table);
                _dgv_Mat_Repair_Order_Width = cls.fnGetDataGridWidth(dgv_Mat_Repair_Order);

                dgv_Mat_Repair_Order.DataSource = view;
                //dgv_Mat_Repair_Order.Refresh();


                //dgv_Mat_Repair_Order.Columns[0].Width = 20 * _dgv_Mat_Repair_Order_Width / 100;    // matId
                dgv_Mat_Repair_Order.Columns[1].Width = 70 * _dgv_Mat_Repair_Order_Width / 100;    // matName
                //dgv_Mat_Repair_Order.Columns[2].Width = 20 * _dgv_Mat_Repair_Order_Width / 100;    // matCode
                dgv_Mat_Repair_Order.Columns[3].Width = 15 * _dgv_Mat_Repair_Order_Width / 100;    // matQty
                dgv_Mat_Repair_Order.Columns[4].Width = 15 * _dgv_Mat_Repair_Order_Width / 100;    // matUnit

                dgv_Mat_Repair_Order.Columns[0].Visible = false;
                dgv_Mat_Repair_Order.Columns[1].Visible = true;
                dgv_Mat_Repair_Order.Columns[2].Visible = false;
                dgv_Mat_Repair_Order.Columns[3].Visible = true;
                dgv_Mat_Repair_Order.Columns[4].Visible = true;

                cls.fnFormatDatagridview(dgv_Mat_Repair_Order, 10, 30);
                dgv_Mat_Repair_Order.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            else
            {
                MessageBox.Show("Không thể thêm khi số lượng bằng 0");
            }

            //if (prodIDx > 0 && prodQty > 0)
            //{
            //    table.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit);
            //    view = new DataView(table);
            //    _dgv_Mat_Repair_Order_Width = cls.fnGetDataGridWidth(dgv_Mat_Repair_Order);

            //    dgv_Mat_Repair_Order.DataSource = view;
            //    dgv_Mat_Repair_Order.Refresh();


            //    //dgv_Mat_Repair_Order.Columns[0].Width = 20 * _dgv_Mat_Repair_Order_Width / 100;    // matId
            //    dgv_Mat_Repair_Order.Columns[1].Width = 70 * _dgv_Mat_Repair_Order_Width / 100;    // matName
            //    //dgv_Mat_Repair_Order.Columns[2].Width = 20 * _dgv_Mat_Repair_Order_Width / 100;    // matCode
            //    dgv_Mat_Repair_Order.Columns[3].Width = 15 * _dgv_Mat_Repair_Order_Width / 100;    // matQty
            //    dgv_Mat_Repair_Order.Columns[4].Width = 15 * _dgv_Mat_Repair_Order_Width / 100;    // matUnit

            //    dgv_Mat_Repair_Order.Columns[0].Visible = false;
            //    dgv_Mat_Repair_Order.Columns[1].Visible = true;
            //    dgv_Mat_Repair_Order.Columns[2].Visible = false;
            //    dgv_Mat_Repair_Order.Columns[3].Visible = true;
            //    dgv_Mat_Repair_Order.Columns[4].Visible = true;

            //    cls.fnFormatDatagridview(dgv_Mat_Repair_Order, 10, 30);
            //    dgv_Mat_Repair_Order.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //}
            //else
            //{
            //    MessageBox.Show("Không thể thêm khi số lượng bằng 0");
            //}

            dgv_Mat_Repair_Parts.ClearSelection();
            _matIDx = "";
            _matName = "";
            _matCode = "";
            _matQty = "";
            _matUnit = "";

            txt_Mat_Repair_Qty_Need.Text = "0";
            txt_Mat_Repair_Qty_Need.Enabled = false;
            lnk_Mat_Repair_Add.Enabled = false;
            lnk_Mat_Repair_Del.Enabled = false;

            txt_Mat_Repair_Reason.Enabled = (dgv_Mat_Repair_Order.Rows.Count > 0) ? true : false;

            btn_Mat_Repair_Save.Enabled = false;

        }

        private void lnk_Mat_Repair_Del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgv_Mat_Repair_Order.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgv_Mat_Repair_Order.Rows.Remove(row);
                }

                dgv_Mat_Repair_Order.ClearSelection();
                _matIDx = "";
                _matName = "";
                _matCode = "";
                _matQty = "";
                _matUnit = "";

                lnk_Mat_Repair_Add.Enabled = false;
                lnk_Mat_Repair_Del.Enabled = false;
                btn_Mat_Repair_Save.Enabled = false;

                if (dgv_Mat_Repair_Order.Rows.Count == 0)
                {
                    lnk_Mat_Repair_Add.Enabled = false;
                    lnk_Mat_Repair_Del.Enabled = false;
                    txt_Mat_Repair_Reason.Text = "";
                    txt_Mat_Repair_Reason.Enabled = false;
                    //btn_Mat_Repair_Done.Enabled = false;
                }

                _msgText = "Xóa vật tư khỏi danh sách yêu cầu thành công.";
                _msgType = 0;

            }
            dgv_Mat_Repair_Order.ClearSelection();
        }

        private void dgv_Mat_Repair_Order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Mat_Repair_Order, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Mat_Repair_Order.Rows[e.RowIndex];

                string matIDx = row.Cells[0].Value.ToString();
                string matName = row.Cells[1].Value.ToString();
                string matCode = row.Cells[2].Value.ToString();
                string matQty = row.Cells[3].Value.ToString();
                string matUnit = row.Cells[4].Value.ToString();

                lnk_Mat_Repair_Add.Enabled = false;
                lnk_Mat_Repair_Del.Enabled = true;
            }
        }

        private void txt_Mat_Repair_Reason_TextChanged(object sender, EventArgs e)
        {
            string reason = txt_Mat_Repair_Reason.Text.Trim();
            btn_Mat_Repair_Save.Enabled = (reason.Length > 0) ? true : false;
        }

        private void btn_Mat_Repair_Done_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                init_Order_Parts_Done();
            }
        }

        private void btn_Mat_Repair_Save_Click(object sender, EventArgs e)
        {
            if (dgv_Mat_Repair_Order.Rows.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        string codeMR = "";
                        codeMR = initMR_Code();
                        string codeMaker = codeMR.Substring(4, 4);
                        codeMR = codeMR.Replace(codeMaker, "-" + String.Format("{0:00}", _logIDx) + "-");

                        string mrCode = codeMR;
                        string mrMakerIDx = _logIDx.ToString();
                        string mrMaker = _logName;
                        string mrPurpose;
                        if (txt_Mat_Repair_Reason.Text.ToLower().Contains("sửa") || txt_Mat_Repair_Reason.Text.ToLower().Contains("sửa chữa"))
                        {
                            mrPurpose = "Repair";
                        }
                        else if (txt_Mat_Repair_Reason.Text.ToLower().Contains("bảo dưỡng"))
                        {
                            mrPurpose = "Maintain";
                        }
                        else if (txt_Mat_Repair_Reason.Text.ToLower().Contains("cải tiến"))
                        {
                            mrPurpose = "Improve";
                        }
                        else
                        {
                            mrPurpose = "Repair";
                        }
                        string mrReason = txt_Mat_Repair_Reason.Text.Trim();
                        DateTime mrDate = _dt.AddMilliseconds(-_dt.Millisecond);
                        string mrPartIDx = "", mrPartName = "", mrPartCode = "", mrPartQty = "", mrPartUnit = "";
                        string equipmentIDx = cbb_Mat_Repair_Equipment.SelectedValue.ToString();
                        string machineIDx = cbb_Mat_Repair_Machine.SelectedValue.ToString();

                        string sqlRequest = "V2o1_BASE_SubMaterial03_Init_MakeRequest_Request_AddItem_V1o1_Addnew";

                        SqlParameter[] sParamsRequest = new SqlParameter[8]; // Parameter count

                        sParamsRequest[0] = new SqlParameter();
                        sParamsRequest[0].SqlDbType = SqlDbType.VarChar;
                        sParamsRequest[0].ParameterName = "@mrCode";
                        sParamsRequest[0].Value = mrCode;

                        sParamsRequest[1] = new SqlParameter();
                        sParamsRequest[1].SqlDbType = SqlDbType.DateTime;
                        sParamsRequest[1].ParameterName = "@mrDate";
                        sParamsRequest[1].Value = mrDate;

                        sParamsRequest[2] = new SqlParameter();
                        sParamsRequest[2].SqlDbType = SqlDbType.Int;
                        sParamsRequest[2].ParameterName = "@mrMakerIDx";
                        sParamsRequest[2].Value = mrMakerIDx;

                        sParamsRequest[3] = new SqlParameter();
                        sParamsRequest[3].SqlDbType = SqlDbType.NVarChar;
                        sParamsRequest[3].ParameterName = "@mrMaker";
                        sParamsRequest[3].Value = mrMaker;

                        sParamsRequest[4] = new SqlParameter();
                        sParamsRequest[4].SqlDbType = SqlDbType.NVarChar;
                        sParamsRequest[4].ParameterName = "@mrPurpose";
                        sParamsRequest[4].Value = mrPurpose;

                        sParamsRequest[5] = new SqlParameter();
                        sParamsRequest[5].SqlDbType = SqlDbType.NVarChar;
                        sParamsRequest[5].ParameterName = "@mrReason";
                        sParamsRequest[5].Value = mrReason;

                        sParamsRequest[6] = new SqlParameter();
                        sParamsRequest[6].SqlDbType = SqlDbType.Int;
                        sParamsRequest[6].ParameterName = "@equipmentIDx";
                        sParamsRequest[6].Value = equipmentIDx;

                        sParamsRequest[7] = new SqlParameter();
                        sParamsRequest[7].SqlDbType = SqlDbType.Int;
                        sParamsRequest[7].ParameterName = "@machineIDx";
                        sParamsRequest[7].Value = machineIDx;

                        cls.fnUpdDel(sqlRequest, sParamsRequest);

                        foreach (DataGridViewRow row in dgv_Mat_Repair_Order.Rows)
                        {
                            mrPartIDx = row.Cells[0].Value.ToString();
                            mrPartName = row.Cells[1].Value.ToString();
                            mrPartCode = row.Cells[2].Value.ToString();
                            mrPartQty = row.Cells[3].Value.ToString();
                            mrPartUnit = row.Cells[4].Value.ToString();

                            string sqlList = "V2o1_BASE_SubMaterial03_Init_MakeRequest_RequestList_AddItem_Addnew";

                            SqlParameter[] sParamsList = new SqlParameter[7]; // Parameter count

                            sParamsList[0] = new SqlParameter();
                            sParamsList[0].SqlDbType = SqlDbType.VarChar;
                            sParamsList[0].ParameterName = "@mrCode";
                            sParamsList[0].Value = mrCode;

                            sParamsList[1] = new SqlParameter();
                            sParamsList[1].SqlDbType = SqlDbType.DateTime;
                            sParamsList[1].ParameterName = "@mrDate";
                            sParamsList[1].Value = mrDate;

                            sParamsList[2] = new SqlParameter();
                            sParamsList[2].SqlDbType = SqlDbType.Int;
                            sParamsList[2].ParameterName = "@mrPartIdx";
                            sParamsList[2].Value = mrPartIDx;

                            sParamsList[3] = new SqlParameter();
                            sParamsList[3].SqlDbType = SqlDbType.NVarChar;
                            sParamsList[3].ParameterName = "@mrPartName";
                            sParamsList[3].Value = mrPartName;

                            sParamsList[4] = new SqlParameter();
                            sParamsList[4].SqlDbType = SqlDbType.VarChar;
                            sParamsList[4].ParameterName = "@mrPartCode";
                            sParamsList[4].Value = mrPartCode;

                            sParamsList[5] = new SqlParameter();
                            sParamsList[5].SqlDbType = SqlDbType.Int;
                            sParamsList[5].ParameterName = "@mrPartQty";
                            sParamsList[5].Value = mrPartQty;

                            sParamsList[6] = new SqlParameter();
                            sParamsList[6].SqlDbType = SqlDbType.VarChar;
                            sParamsList[6].ParameterName = "@mrPartUnit";
                            sParamsList[6].Value = mrPartUnit;

                            cls.fnUpdDel(sqlList, sParamsList);
                        }

                        _matIDx = "";
                        _matName = "";
                        _matCode = "";
                        _matQty = "";
                        _matUnit = "";
                        dgv_Mat_Repair_Order.DataSource = "";
                        table.Rows.Clear();

                        _msgText = "Tạo yêu cầu vật tư thành công. Vui lòng liên hệ kho Spare Part để nhận vật tư.";
                        _msgType = 1;


                    }
                    catch (SqlException sqlEx)
                    {
                        _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng báo lại quản trị hệ thống";
                        _msgType = 3;
                    }
                    catch (Exception ex)
                    {
                        _msgText = "Có lỗi phát sinh. Vui lòng báo lại quản trị hệ thống";
                        _msgType = 2;
                    }
                    finally
                    {
                        init_Order_Parts_List();
                        init_Order_Parts_Done();

                        cls.fnMessage(tssMessage, _msgText, _msgType);
                    }
                }
            }
        }


        #endregion



        #region ORDER PARTS LIST


        public void init_Order_Parts_List()
        {
            string sql = "PMMS_01_Order_Spare_Parts_List_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = _logIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@rangeTime";
            sParams[1].Value = _rangePOTime;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Order_Parts_Width = cls.fnGetDataGridWidth(dgv_Order_Parts);
            dgv_Order_Parts.DataSource = dt;

            dgv_Order_Parts.Columns[0].Width = 5 * _dgv_Order_Parts_Width / 100;    // STT
            dgv_Order_Parts.Columns[1].Width = 15 * _dgv_Order_Parts_Width / 100;    // cateName
            dgv_Order_Parts.Columns[2].Width = 15 * _dgv_Order_Parts_Width / 100;    // machineName
            //dgv_Order_Parts.Columns[3].Width = 5 * _dgv_Order_Parts_Width / 100;    // idx
            //dgv_Order_Parts.Columns[4].Width = 20 * _dgv_Order_Parts_Width / 100;    // rsbmCode
            //dgv_Order_Parts.Columns[5].Width = 5 * _dgv_Order_Parts_Width / 100;    // rsbmDate
            dgv_Order_Parts.Columns[6].Width = 5 * _dgv_Order_Parts_Width / 100;    // matQty
            //dgv_Order_Parts.Columns[7].Width = 5 * _dgv_Order_Parts_Width / 100;    // rsbmReceiver
            //dgv_Order_Parts.Columns[8].Width = 5 * _dgv_Order_Parts_Width / 100;    // rsbmPurpose
            //dgv_Order_Parts.Columns[9].Width = 5 * _dgv_Order_Parts_Width / 100;    // rsbmPriority
            dgv_Order_Parts.Columns[10].Width = 30 * _dgv_Order_Parts_Width / 100;    // rsbmReason
            dgv_Order_Parts.Columns[11].Width = 15 * _dgv_Order_Parts_Width / 100;    // rsbmAdd
            //dgv_Order_Parts.Columns[12].Width = 5 * _dgv_Order_Parts_Width / 100;    // rsbmFinish
            dgv_Order_Parts.Columns[13].Width = 15 * _dgv_Order_Parts_Width / 100;    // scanoutDate

            dgv_Order_Parts.Columns[0].Visible = true;
            dgv_Order_Parts.Columns[1].Visible = true;
            dgv_Order_Parts.Columns[2].Visible = true;
            dgv_Order_Parts.Columns[3].Visible = false;
            dgv_Order_Parts.Columns[4].Visible = false;
            dgv_Order_Parts.Columns[5].Visible = false;
            dgv_Order_Parts.Columns[6].Visible = true;
            dgv_Order_Parts.Columns[7].Visible = false;
            dgv_Order_Parts.Columns[8].Visible = false;
            dgv_Order_Parts.Columns[9].Visible = false;
            dgv_Order_Parts.Columns[10].Visible = true;
            dgv_Order_Parts.Columns[11].Visible = true;
            dgv_Order_Parts.Columns[12].Visible = false;
            dgv_Order_Parts.Columns[13].Visible = true;

            dgv_Order_Parts.Columns[11].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgv_Order_Parts.Columns[13].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            cls.fnFormatDatagridview(dgv_Order_Parts, 11, 30);
        }

        private void lnk_Order_Parts_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 1;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 2;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_1Week_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 3;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_10Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 4;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_1Month_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 5;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_2Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 6;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_3Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 7;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_6Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 8;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_9Months_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 9;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        private void lnk_Order_Parts_1Year_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangePOTime = 10;
            dgv_Order_Parts.DataSource = "";
            init_Order_Parts_List();
            fnLinkColorParts();
        }

        public void fnLinkColorParts()
        {
            switch (_rangePOTime)
            {
                case 1:
                    lnk_Order_Parts_Today.LinkColor = Color.Red;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 2:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Red;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 3:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Red;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 4:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Red;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 5:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Red;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 6:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Red;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 7:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Red;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 8:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Red;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 9:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Red;
                    lnk_Order_Parts_1Year.LinkColor = Color.Blue;
                    break;
                case 10:
                    lnk_Order_Parts_Today.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Week.LinkColor = Color.Blue;
                    lnk_Order_Parts_10Days.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Month.LinkColor = Color.Blue;
                    lnk_Order_Parts_2Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_3Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_6Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_9Months.LinkColor = Color.Blue;
                    lnk_Order_Parts_1Year.LinkColor = Color.Red;
                    break;
            }
        }

        private void dgv_Order_Parts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Order_Parts, e);
            }
        }

        private void dgv_Order_Parts_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgv_Order_Parts.Rows[e.RowIndex].Selected = true;
                _row_Order_Parts_Index = e.RowIndex;
                dgv_Order_Parts.CurrentCell = dgv_Order_Parts.Rows[e.RowIndex].Cells[0];
                cms_Order_Parts.Show(this.dgv_Order_Parts, e.Location);
                cms_Order_Parts.Show(Cursor.Position);
            }
        }


        #endregion



        #region ORDER MATERIAL


        public void init_Order_Material()
        {
            init_Order_Material_PIC();
            init_Order_Material_Warehouse();

            //MessageBox.Show(init_Order_Code());
        }

        public void init_Order_Material_PIC()
        {
            string sql = "PMMS_01_Order_Material_PIC_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = _logIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string logTitle = ds.Tables[0].Rows[0][1].ToString();
                string logDepart = ds.Tables[0].Rows[0][2].ToString();

                lbl_Mat_Material_Department.Text = logDepart;
                lbl_Mat_Material_PIC.Text = _logName.ToUpper();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin đăng nhập.\r\nVui lòng thử lại hoặc báo quản trị hệ thống");
                frmPM_00LoginSystem frmLogin = new frmPM_00LoginSystem(1);
                this.Close();
                frmLogin.Show();
            }
        }

        public void init_Order_Material_Warehouse()
        {
            string sql = "PMMS_01_Order_Material_Warehouse_SelItem_V1o0_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbb_Mat_Material_Warehouse.DataSource = dt;
            cbb_Mat_Material_Warehouse.DisplayMember = "location";
            cbb_Mat_Material_Warehouse.ValueMember = "locationId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbb_Mat_Material_Warehouse.SelectedIndex = 0;
        }

        public void init_Order_Material_Done(int type)
        {
            switch (type)
            {
                case 0:
                    _matNvlIDx = "";
                    _matNvlName = "";
                    _matNvlCode = "";
                    _matNvlQty = "";
                    _matNvlUnit = "";
                    _matNvlPCS = "";
                    _matNvlBOX = "";
                    _matNvlPAK = "";
                    _matNvlPAL = "";

                    table2.Rows.Clear();

                    cbb_Mat_Material_Warehouse.SelectedIndex = 0;
                    lbl_Mat_Material_Warehouse.Text = "";
                    dgv_Mat_Material_List.DataSource = "";

                    rdb_Mat_Material_PCS.Checked = false;
                    rdb_Mat_Material_PCS.Enabled = false;
                    rdb_Mat_Material_PCS.BackColor = Color.Silver;
                    lbl_Mat_Material_PCS.Text = "0";
                    lbl_Mat_Material_PCS.Enabled = false;
                    lbl_Mat_Material_PCS.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_BOX.Checked = false;
                    rdb_Mat_Material_BOX.Enabled = false;
                    rdb_Mat_Material_BOX.BackColor = Color.Silver;
                    lbl_Mat_Material_BOX.Text = "0";
                    lbl_Mat_Material_BOX.Enabled = false;
                    lbl_Mat_Material_BOX.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_PAK.Checked = false;
                    rdb_Mat_Material_PAK.Enabled = false;
                    rdb_Mat_Material_PAK.BackColor = Color.Silver;
                    lbl_Mat_Material_PAK.Text = "0";
                    lbl_Mat_Material_PAK.Enabled = false;
                    lbl_Mat_Material_PAK.BackColor = Color.Gainsboro;
                    rdb_Mat_Material_PAL.Checked = false;

                    rdb_Mat_Material_PAL.Enabled = false;
                    rdb_Mat_Material_PAL.BackColor = Color.Silver;
                    lbl_Mat_Material_PAL.Text = "0";
                    lbl_Mat_Material_PAL.Enabled = false;
                    lbl_Mat_Material_PAL.BackColor = Color.Gainsboro;

                    lbl_Mat_Material_TotalNeed.Text = "S.L cần";
                    txt_Mat_Material_Qty.Text = "";
                    txt_Mat_Material_Qty.Enabled = false;
                    lbl_Mat_Material_TotalQty.Text = "";
                    lbl_Mat_Material_Unit.Text = "";
                    txt_Mat_Material_Filter.Text = "";
                    txt_Mat_Material_Filter.Enabled = false;
                    lnk_Mat_Material_Add.Enabled = false;
                    lnk_Mat_Material_Del.Enabled = false;

                    dgv_Mat_Material_Order.DataSource = "";
                    txt_Mat_Material_Reason.Text = "";
                    txt_Mat_Material_Reason.Enabled = false;
                    btn_Mat_Material_Save.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                    break;
                case 1:
                    _matNvlIDx = "";
                    _matNvlName = "";
                    _matNvlCode = "";
                    _matNvlQty = "";
                    _matNvlUnit = "";
                    _matNvlPCS = "";
                    _matNvlBOX = "";
                    _matNvlPAK = "";
                    _matNvlPAL = "";

                    table.Rows.Clear();

                    dgv_Mat_Material_List.DataSource = "";

                    rdb_Mat_Material_PCS.Checked = false;
                    rdb_Mat_Material_PCS.Enabled = false;
                    rdb_Mat_Material_PCS.BackColor = Color.Silver;
                    lbl_Mat_Material_PCS.Text = "0";
                    lbl_Mat_Material_PCS.Enabled = false;

                    lbl_Mat_Material_PCS.BackColor = Color.Gainsboro;
                    rdb_Mat_Material_BOX.Checked = false;
                    rdb_Mat_Material_BOX.Enabled = false;
                    rdb_Mat_Material_BOX.BackColor = Color.Silver;
                    lbl_Mat_Material_BOX.Text = "0";
                    lbl_Mat_Material_BOX.Enabled = false;
                    lbl_Mat_Material_BOX.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_PAK.Checked = false;
                    rdb_Mat_Material_PAK.Enabled = false;
                    rdb_Mat_Material_PAK.BackColor = Color.Silver;
                    lbl_Mat_Material_PAK.Text = "0";
                    lbl_Mat_Material_PAK.Enabled = false;
                    lbl_Mat_Material_PAK.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_PAL.Checked = false;
                    rdb_Mat_Material_PAL.Enabled = false;
                    rdb_Mat_Material_PAL.BackColor = Color.Silver;
                    lbl_Mat_Material_PAL.Text = "0";
                    lbl_Mat_Material_PAL.Enabled = false;
                    lbl_Mat_Material_PAL.BackColor = Color.Gainsboro;

                    lbl_Mat_Material_TotalNeed.Text = "S.L cần";
                    txt_Mat_Material_Qty.Text = "";
                    txt_Mat_Material_Qty.Enabled = false;
                    lbl_Mat_Material_TotalQty.Text = "";
                    lbl_Mat_Material_Unit.Text = "";
                    txt_Mat_Material_Filter.Text = "";
                    txt_Mat_Material_Filter.Enabled = false;
                    lnk_Mat_Material_Add.Enabled = false;
                    lnk_Mat_Material_Del.Enabled = false;

                    dgv_Mat_Material_Order.DataSource = "";
                    txt_Mat_Material_Reason.Text = "";
                    txt_Mat_Material_Reason.Enabled = false;
                    btn_Mat_Material_Save.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                    break;
                case 2:
                    _matNvlIDx = "";
                    _matNvlName = "";
                    _matNvlCode = "";
                    _matNvlQty = "";
                    _matNvlUnit = "";
                    _matNvlPCS = "";
                    _matNvlBOX = "";
                    _matNvlPAK = "";
                    _matNvlPAL = "";

                    table.Rows.Clear();

                    lbl_Mat_Material_TotalNeed.Text = "S.L cần";
                    txt_Mat_Material_Qty.Text = "";
                    txt_Mat_Material_Qty.Enabled = false;
                    lbl_Mat_Material_TotalQty.Text = "";
                    lbl_Mat_Material_Unit.Text = "";
                    txt_Mat_Material_Filter.Text = "";
                    txt_Mat_Material_Filter.Enabled = false;
                    lnk_Mat_Material_Add.Enabled = false;
                    lnk_Mat_Material_Del.Enabled = false;

                    dgv_Mat_Material_Order.DataSource = "";
                    txt_Mat_Material_Reason.Text = "";
                    txt_Mat_Material_Reason.Enabled = false;
                    btn_Mat_Material_Save.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                    break;
                case 3:
                    _matNvlIDx = "";
                    _matNvlName = "";
                    _matNvlCode = "";
                    _matNvlQty = "";
                    _matNvlUnit = "";
                    _matNvlPCS = "";
                    _matNvlBOX = "";
                    _matNvlPAK = "";
                    _matNvlPAL = "";

                    table.Rows.Clear();

                    dgv_Mat_Material_Order.DataSource = "";
                    txt_Mat_Material_Reason.Text = "";
                    txt_Mat_Material_Reason.Enabled = false;
                    btn_Mat_Material_Save.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                    break;
                case 4:
                    _matNvlIDx = "";
                    _matNvlName = "";
                    _matNvlCode = "";
                    _matNvlQty = "";
                    _matNvlUnit = "";
                    _matNvlPCS = "";
                    _matNvlBOX = "";
                    _matNvlPAK = "";
                    _matNvlPAL = "";

                    table.Rows.Clear();

                    txt_Mat_Material_Reason.Text = "";
                    txt_Mat_Material_Reason.Enabled = false;
                    btn_Mat_Material_Save.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                    break;
                case 5:
                    _matNvlIDx = "";
                    _matNvlName = "";
                    _matNvlCode = "";
                    _matNvlQty = "";
                    _matNvlUnit = "";
                    _matNvlPCS = "";
                    _matNvlBOX = "";
                    _matNvlPAK = "";
                    _matNvlPAL = "";

                    table.Rows.Clear();

                    btn_Mat_Material_Save.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                    break;
            }
        }

        public void init_Order_Material_Type()
        {
            string pcs = "", box = "", pak = "", pal = "", qty = "";
            decimal _pcs = 0, _box = 0, _pak = 0, _pal = 0, _qty = 0;

            _matPackType = "";
            _matPackQty = "";

            pcs = lbl_Mat_Material_PCS.Text.Trim();
            box = lbl_Mat_Material_BOX.Text.Trim();
            pak = lbl_Mat_Material_PAK.Text.Trim();
            pal = lbl_Mat_Material_PAL.Text.Trim();

            if (rdb_Mat_Material_PCS.Checked)
            {
                qty = pcs;
                _matPackType = "PCS";
            }
            else if (rdb_Mat_Material_BOX.Checked)
            {
                qty = box;
                _matPackType = "BOX";
            }
            else if (rdb_Mat_Material_PAK.Checked)
            {
                qty = pak;
                _matPackType = "PAK";
            }
            else if (rdb_Mat_Material_PAL.Checked)
            {
                qty = pal;
                _matPackType = "PAL";
            }
            else
            {
                qty = "0";
                _matPackType = "";
            }

            _matPackQty = qty;
            _qty = Convert.ToDecimal(qty);
            if (_qty > 0)
            {
                lbl_Mat_Material_TotalNeed.Text = "S.L cần x " + _qty;
                txt_Mat_Material_Qty.Text = "1";
                txt_Mat_Material_Qty.Enabled = true;
                lbl_Mat_Material_TotalQty.Text = _qty.ToString();
                lbl_Mat_Material_Unit.Text = "(" + _matNvlUnit + ")";
                lnk_Mat_Material_Add.Enabled = true;
                lnk_Mat_Material_Del.Enabled = false;

                //MessageBox.Show(qty);
            }
            else
            {
                lbl_Mat_Material_TotalNeed.Text = "S.L cần";
                txt_Mat_Material_Qty.Text = "";
                txt_Mat_Material_Qty.Enabled = false;
                lbl_Mat_Material_TotalQty.Text = "";
                lbl_Mat_Material_Unit.Text = "";
                lnk_Mat_Material_Add.Enabled = false;
                lnk_Mat_Material_Del.Enabled = false;
            }
        }

        public void init_Order_Material_List()
        {
            string picIDx = _logIDx.ToString();
            string sql = "PMMS_01_Order_Material_Request_List_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@picIDx";
            sParams[0].Value = picIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Mat_Request_List_Width = cls.fnGetDataGridWidth(dgv_Mat_Request_List);
            dgv_Mat_Request_List.DataSource = dt;

            dgv_Mat_Request_List.Columns[0].Width = 5 * _dgv_Mat_Request_List_Width / 100;    // STT
            //dgv_Mat_Request_List.Columns[1].Width = 5 * _dgv_Mat_Request_List_Width / 100;    // idx
            //dgv_Mat_Request_List.Columns[2].Width = 5 * _dgv_Mat_Request_List_Width / 100;    // picIDx
            dgv_Mat_Request_List.Columns[3].Width = 10 * _dgv_Mat_Request_List_Width / 100;    // Dept
            dgv_Mat_Request_List.Columns[4].Width = 15 * _dgv_Mat_Request_List_Width / 100;    // Name
            dgv_Mat_Request_List.Columns[5].Width = 15 * _dgv_Mat_Request_List_Width / 100;    // orderCode
            dgv_Mat_Request_List.Columns[6].Width = 10 * _dgv_Mat_Request_List_Width / 100;    // Name
            dgv_Mat_Request_List.Columns[7].Width = 15 * _dgv_Mat_Request_List_Width / 100;    // orderMake
            dgv_Mat_Request_List.Columns[8].Width = 5 * _dgv_Mat_Request_List_Width / 100;    // matTotal
            dgv_Mat_Request_List.Columns[9].Width = 25 * _dgv_Mat_Request_List_Width / 100;    // orderNote
            //dgv_Mat_Request_List.Columns[10].Width = 5 * _dgv_Mat_Request_List_Width / 100;    // orderStatus

            dgv_Mat_Request_List.Columns[0].Visible = true;
            dgv_Mat_Request_List.Columns[1].Visible = false;
            dgv_Mat_Request_List.Columns[2].Visible = false;
            dgv_Mat_Request_List.Columns[3].Visible = true;
            dgv_Mat_Request_List.Columns[4].Visible = true;
            dgv_Mat_Request_List.Columns[5].Visible = true;
            dgv_Mat_Request_List.Columns[6].Visible = true;
            dgv_Mat_Request_List.Columns[7].Visible = true;
            dgv_Mat_Request_List.Columns[8].Visible = true;
            dgv_Mat_Request_List.Columns[9].Visible = true;
            dgv_Mat_Request_List.Columns[10].Visible = false;

            dgv_Mat_Request_List.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgv_Mat_Request_List.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cls.fnFormatDatagridview(dgv_Mat_Request_List, 11, 30);
        }

        public string init_Order_Code()
        {
            string picIDx = String.Format("{0:000}", _logIDx);
            string orderNo = "";
            try
            {
                string currOrderNo = "", nextOrderNo = "";
                int _currOrderNo = 0, _nextOrderNo = 0;
                string sql = "PMMS_01_Order_Material_Code_SelItem_V1o0_Addnew";
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@picIDx";
                sParams[0].Value = _logIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    currOrderNo = ds.Tables[0].Rows[0][0].ToString();
                    string orderCode = cls.Right(currOrderNo, 7);
                    _currOrderNo = (orderCode != "" && orderCode != null) ? Convert.ToInt32(orderCode) : 0;
                    _nextOrderNo = _currOrderNo + 1;
                    orderNo = "MR-" + picIDx + "-" + String.Format("{0:0000000}", _nextOrderNo);
                }
                else
                {
                    currOrderNo = "0";
                    nextOrderNo = "MR-" + picIDx + "-0000001";
                    orderNo = nextOrderNo;
                }
            }
            catch
            {

            }
            finally
            {

            }
            return orderNo;
        }

        private void cbb_Mat_Material_Warehouse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cbb_Mat_Material_Warehouse.SelectedIndex > 0)
                {
                    table2.Rows.Clear();
                    init_Order_Material_Done(1);

                    txt_Mat_Material_Filter.Text = "";
                    txt_Mat_Material_Filter.Enabled = true;
                    txt_Mat_Material_Filter.Focus();

                    string matQty = "";
                    decimal _matQty = 0;
                    string whIDx = cbb_Mat_Material_Warehouse.SelectedValue.ToString();
                    string sql = "PMMS_01_Order_Material_SelItem_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@whIDx";
                    sParams[0].Value = whIDx;

                    DataTable dt = new DataTable();
                    dt = cls.ExecuteDataTable(sql, sParams);

                    _dgv_Mat_Material_List_Width = cls.fnGetDataGridWidth(dgv_Mat_Material_List);
                    dgv_Mat_Material_List.DataSource = dt;

                    dgv_Mat_Material_List.Columns[0].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // STT
                    //dgv_Mat_Material_List.Columns[1].Width = 5 * _dgv_Mat_Material_List_Width / 100;    // ProdId
                    dgv_Mat_Material_List.Columns[2].Width = 55 * _dgv_Mat_Material_List_Width / 100;    // Name
                    //dgv_Mat_Material_List.Columns[3].Width = 5 * _dgv_Mat_Material_List_Width / 100;    // BarCode
                    dgv_Mat_Material_List.Columns[4].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // Total
                    dgv_Mat_Material_List.Columns[5].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // Uom
                    //dgv_Mat_Material_List.Columns[6].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // PCS - PackingOutStock
                    //dgv_Mat_Material_List.Columns[7].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // BOX - PackingBox
                    //dgv_Mat_Material_List.Columns[8].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // PAK - PackingCart
                    //dgv_Mat_Material_List.Columns[9].Width = 15 * _dgv_Mat_Material_List_Width / 100;    // PAL - PackingPallete

                    dgv_Mat_Material_List.Columns[0].Visible = true;
                    dgv_Mat_Material_List.Columns[1].Visible = false;
                    dgv_Mat_Material_List.Columns[2].Visible = true;
                    dgv_Mat_Material_List.Columns[3].Visible = false;
                    dgv_Mat_Material_List.Columns[4].Visible = true;
                    dgv_Mat_Material_List.Columns[5].Visible = true;
                    dgv_Mat_Material_List.Columns[6].Visible = false;
                    dgv_Mat_Material_List.Columns[7].Visible = false;
                    dgv_Mat_Material_List.Columns[8].Visible = false;
                    dgv_Mat_Material_List.Columns[9].Visible = false;

                    cls.fnFormatDatagridview(dgv_Mat_Material_List, 11, 30);

                    dgv_Mat_Material_List.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    foreach (DataGridViewRow row in dgv_Mat_Material_List.Rows)
                    {
                        matQty = row.Cells[4].Value.ToString();
                        _matQty = (matQty != "" && matQty != null) ? Convert.ToDecimal(matQty) : 0;

                        if (_matQty == 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.Silver;
                        }
                    }

                    string whName = "";
                    string whText = cbb_Mat_Material_Warehouse.Text.ToLower();
                    if (whText.Contains("wh01"))
                    {
                        whName = "RESIN REQUEST";
                    }
                    else if (whText.Contains("wh02"))
                    {
                        whName = "CKD REQUEST";
                    }
                    else if (whText.Contains("wh03"))
                    {
                        whName = "RUBBER REQUEST";
                    }
                    else if (whText.Contains("wh04"))
                    {
                        whName = "CHEMICAL REQUEST";
                    }
                    else if (whText.Contains("wh08"))
                    {
                        whName = "RECYCLE REQUEST";
                    }
                    else if (whText.Contains("wh09"))
                    {
                        whName = "SCRAP REQUEST";
                    }
                    else if (whText.Contains("wh10"))
                    {
                        whName = "GARBAGE REQUEST";
                    }
                    else if (whText.Contains("wh11"))
                    {
                        whName = "STATIONARY REQUEST";
                    }
                    else if (whText.Contains("wh12"))
                    {
                        whName = "UTILITY REQUEST";
                    }
                    else
                    {
                        whName = "WAREHOUSE REQUEST";
                    }
                    lbl_Mat_Material_Warehouse.Text = whName.ToUpper();
                }
                else
                {
                    dgv_Mat_Material_List.DataSource = "";

                    txt_Mat_Material_Filter.Text = "";
                    txt_Mat_Material_Filter.Enabled = false;

                    init_Order_Material_Done(0);
                    lbl_Mat_Material_Warehouse.Text = "";
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txt_Mat_Material_Filter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgv_Mat_Material_List.ClearSelection();

                rdb_Mat_Material_PCS.Checked = false;
                rdb_Mat_Material_PCS.Enabled = false;
                rdb_Mat_Material_PCS.BackColor = Color.Silver;
                lbl_Mat_Material_PCS.Text = "0";
                lbl_Mat_Material_PCS.Enabled = false;
                lbl_Mat_Material_PCS.BackColor = Color.Gainsboro;
                rdb_Mat_Material_BOX.Checked = false;
                rdb_Mat_Material_BOX.Enabled = false;
                rdb_Mat_Material_BOX.BackColor = Color.Silver;
                lbl_Mat_Material_BOX.Text = "0";
                lbl_Mat_Material_BOX.Enabled = false;
                lbl_Mat_Material_BOX.BackColor = Color.Gainsboro;
                rdb_Mat_Material_PAK.Checked = false;
                rdb_Mat_Material_PAK.Enabled = false;
                rdb_Mat_Material_PAK.BackColor = Color.Silver;
                lbl_Mat_Material_PAK.Text = "0";
                lbl_Mat_Material_PAK.Enabled = false;
                lbl_Mat_Material_PAK.BackColor = Color.Gainsboro;
                rdb_Mat_Material_PAL.Checked = false;
                rdb_Mat_Material_PAL.Enabled = false;
                rdb_Mat_Material_PAL.BackColor = Color.Silver;
                lbl_Mat_Material_PAL.Text = "0";
                lbl_Mat_Material_PAL.Enabled = false;
                lbl_Mat_Material_PAL.BackColor = Color.Gainsboro;

                lbl_Mat_Material_TotalNeed.Text = "S.L cần";
                txt_Mat_Material_Qty.Text = "";
                txt_Mat_Material_Qty.Enabled = false;
                lbl_Mat_Material_TotalQty.Text = "";
                lbl_Mat_Material_Unit.Text = "";
                lnk_Mat_Material_Add.Enabled = false;
                lnk_Mat_Material_Del.Enabled = false;

                cls.fnFilterDatagridRow(dgv_Mat_Material_List, txt_Mat_Material_Filter, 2);

                string matQty = "";
                decimal _matQty = 0;

                foreach (DataGridViewRow row in dgv_Mat_Material_List.Rows)
                {
                    matQty = row.Cells[4].Value.ToString();
                    _matQty = (matQty != "" && matQty != null) ? Convert.ToDecimal(matQty) : 0;

                    if (_matQty == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Silver;
                    }
                }

                dgv_Mat_Material_List.ClearSelection();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgv_Mat_Material_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Mat_Material_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Mat_Material_List.Rows[e.RowIndex];

                string matIDx = row.Cells[1].Value.ToString();
                string matName = row.Cells[2].Value.ToString();
                string matCode = row.Cells[3].Value.ToString();
                string matQty = row.Cells[4].Value.ToString();
                string matUnit = row.Cells[5].Value.ToString();
                string matPCS = row.Cells[6].Value.ToString();
                string matBOX = row.Cells[7].Value.ToString();
                string matPAK = row.Cells[8].Value.ToString();
                string matPAL = row.Cells[9].Value.ToString();

                _matNvlIDx = matIDx;
                _matNvlName = matName;
                _matNvlCode = matCode;
                _matNvlQty = matQty;
                _matNvlUnit = matUnit;
                _matNvlPCS = matPCS;
                _matNvlBOX = matBOX;
                _matNvlPAK = matPAK;
                _matNvlPAL = matPAL;

                _matPackType = "";
                _matPackQty = "";


                if (matQty != "0.0" && matQty != "0")
                {
                    decimal _matPCS, _matBOX, _matPAK, _matPAL;
                    _matPCS = ((matPCS != "0.0" || matPCS != "0") && matPCS != null) ? Convert.ToDecimal(matPCS) : 0;
                    _matBOX = ((matBOX != "0.0" || matBOX != "0") && matBOX != null) ? Convert.ToDecimal(matBOX) : 0;
                    _matPAK = ((matPAK != "0.0" || matPAK != "0") && matPAK != null) ? Convert.ToDecimal(matPAK) : 0;
                    _matPAL = ((matPAL != "0.0" || matPAL != "0") && matPAL != null) ? Convert.ToDecimal(matPAL) : 0;

                    rdb_Mat_Material_PCS.Checked = false;
                    rdb_Mat_Material_BOX.Checked = false;
                    rdb_Mat_Material_PAK.Checked = false;
                    rdb_Mat_Material_PAL.Checked = false;

                    if (_matPCS > 0)
                    {
                        rdb_Mat_Material_PCS.Enabled = true;
                        rdb_Mat_Material_PCS.BackColor = Color.LimeGreen;
                        lbl_Mat_Material_PCS.Text = matPCS;
                        lbl_Mat_Material_PCS.Enabled = true;
                        lbl_Mat_Material_PCS.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        rdb_Mat_Material_PCS.Enabled = false;
                        rdb_Mat_Material_PCS.BackColor = Color.Silver;
                        lbl_Mat_Material_PCS.Text = "0";
                        lbl_Mat_Material_PCS.Enabled = false;
                        lbl_Mat_Material_PCS.BackColor = Color.Gainsboro;
                    }

                    if (_matBOX > 0)
                    {
                        rdb_Mat_Material_BOX.Enabled = true;
                        rdb_Mat_Material_BOX.BackColor = Color.LimeGreen;
                        lbl_Mat_Material_BOX.Text = matBOX;
                        lbl_Mat_Material_BOX.Enabled = true;
                        lbl_Mat_Material_BOX.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        rdb_Mat_Material_BOX.Enabled = false;
                        rdb_Mat_Material_BOX.BackColor = Color.Silver;
                        lbl_Mat_Material_BOX.Text = "0";
                        lbl_Mat_Material_BOX.Enabled = false;
                        lbl_Mat_Material_BOX.BackColor = Color.Gainsboro;
                    }

                    if (_matPAK > 0)
                    {
                        rdb_Mat_Material_PAK.Enabled = true;
                        rdb_Mat_Material_PAK.BackColor = Color.LimeGreen;
                        lbl_Mat_Material_PAK.Text = matPAK;
                        lbl_Mat_Material_PAK.Enabled = true;
                        lbl_Mat_Material_PAK.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        rdb_Mat_Material_PAK.Enabled = false;
                        rdb_Mat_Material_PAK.BackColor = Color.Silver;
                        lbl_Mat_Material_PAK.Text = "0";
                        lbl_Mat_Material_PAK.Enabled = false;
                        lbl_Mat_Material_PAK.BackColor = Color.Gainsboro;
                    }

                    if (_matPAL > 0)
                    {
                        rdb_Mat_Material_PAL.Enabled = true;
                        rdb_Mat_Material_PAL.BackColor = Color.LimeGreen;
                        lbl_Mat_Material_PAL.Text = matPAL;
                        lbl_Mat_Material_PAL.Enabled = true;
                        lbl_Mat_Material_PAL.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        rdb_Mat_Material_PAL.Enabled = false;
                        rdb_Mat_Material_PAL.BackColor = Color.Silver;
                        lbl_Mat_Material_PAL.Text = "0";
                        lbl_Mat_Material_PAL.Enabled = false;
                        lbl_Mat_Material_PAL.BackColor = Color.Gainsboro;
                    }


                    //txt_Mat_Material_Qty.Text = "1";
                    //txt_Mat_Material_Qty.Enabled = true;
                    //lnk_Mat_Material_Add.Enabled = true;
                }
                else
                {
                    //txt_Mat_Material_Qty.Text = "0";
                    //txt_Mat_Material_Qty.Enabled = false;
                    //lnk_Mat_Material_Add.Enabled = false;

                    rdb_Mat_Material_PCS.Checked = false;
                    rdb_Mat_Material_PCS.Enabled = false;
                    rdb_Mat_Material_PCS.BackColor = Color.Silver;
                    lbl_Mat_Material_PCS.Text = "0";
                    lbl_Mat_Material_PCS.Enabled = false;
                    lbl_Mat_Material_PCS.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_BOX.Checked = false;
                    rdb_Mat_Material_BOX.Enabled = false;
                    rdb_Mat_Material_BOX.BackColor = Color.Silver;
                    lbl_Mat_Material_BOX.Text = "0";
                    lbl_Mat_Material_BOX.Enabled = false;
                    lbl_Mat_Material_BOX.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_PAK.Checked = false;
                    rdb_Mat_Material_PAK.Enabled = false;
                    rdb_Mat_Material_PAK.BackColor = Color.Silver;
                    lbl_Mat_Material_PAK.Text = "0";
                    lbl_Mat_Material_PAK.Enabled = false;
                    lbl_Mat_Material_PAK.BackColor = Color.Gainsboro;

                    rdb_Mat_Material_PAL.Checked = false;
                    rdb_Mat_Material_PAL.Enabled = false;
                    rdb_Mat_Material_PAL.BackColor = Color.Silver;
                    lbl_Mat_Material_PAL.Text = "0";
                    lbl_Mat_Material_PAL.Enabled = false;
                    lbl_Mat_Material_PAL.BackColor = Color.Gainsboro;

                    lbl_Mat_Material_TotalNeed.Text = "S.L cần";
                    txt_Mat_Material_Qty.Text = "";
                    txt_Mat_Material_Qty.Enabled = false;
                    lbl_Mat_Material_TotalQty.Text = "";
                    lbl_Mat_Material_Unit.Text = "";
                    lnk_Mat_Material_Add.Enabled = false;
                    lnk_Mat_Material_Del.Enabled = false;

                }
            }
        }

        private void txt_Mat_Material_Qty_Click(object sender, EventArgs e)
        {
            txt_Mat_Material_Qty.SelectAll();
        }

        private void rdb_Mat_Material_PCS_CheckedChanged(object sender, EventArgs e)
        {
            init_Order_Material_Type();
        }

        private void rdb_Mat_Material_BOX_CheckedChanged(object sender, EventArgs e)
        {
            init_Order_Material_Type();
        }

        private void rdb_Mat_Material_PAK_CheckedChanged(object sender, EventArgs e)
        {
            init_Order_Material_Type();
        }

        private void rdb_Mat_Material_PAL_CheckedChanged(object sender, EventArgs e)
        {
            init_Order_Material_Type();
        }

        private void txt_Mat_Material_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txt_Mat_Material_Qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string haveQty = _matNvlQty;
                string packQty = _matPackQty;
                string needQty = txt_Mat_Material_Qty.Text.Trim();

                decimal _haveQty = 0, _packQty = 0, _needQty = 0, _needTotal = 0;

                _haveQty = (haveQty != "0.0" && haveQty != "0" && haveQty != "" && haveQty != null) ? Convert.ToDecimal(haveQty) : 0;
                _packQty = (packQty != "0.0" && packQty != "0" && packQty != "" && packQty != null) ? Convert.ToDecimal(packQty) : 0;
                _needQty = (needQty != "0.0" && needQty != "0" && needQty != "" && needQty != null) ? Convert.ToDecimal(needQty) : 0;

                if (_needQty > 0)
                {
                    _needTotal = _needQty * _packQty;

                    if (_needTotal > _haveQty)
                    {
                        txt_Mat_Material_Qty.Text = "1";
                        lbl_Mat_Material_TotalQty.Text = _packQty.ToString();
                    }
                    else
                    {
                        lbl_Mat_Material_TotalQty.Text = _needTotal.ToString();
                    }
                }
                else
                {
                    txt_Mat_Material_Qty.Text = "1";
                    lbl_Mat_Material_TotalQty.Text = _packQty.ToString();
                }

                //if (_needQty > _haveQty)
                //{
                //    txt_Mat_Material_Qty.Text = _haveQty.ToString();
                //}
                //else
                //{
                //    txt_Mat_Material_Qty.Text = (needQty == "" || needQty == null) ? "1" : (_needQty * _packQty).ToString();
                //}

                lnk_Mat_Material_Add.Enabled = (_needQty > 1 && _needQty <= _haveQty) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void lnk_Mat_Material_Add_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int prodIDx = (_matNvlIDx != "" && _matNvlIDx != null) ? Convert.ToInt32(_matNvlIDx) : 0;
            string prodName = _matNvlName;
            string prodCode = _matNvlCode;
            string qty = lbl_Mat_Material_TotalQty.Text.Trim();
            decimal prodQty = (qty != "" && qty != null) ? Convert.ToDecimal(qty) : 0;
            string prodUnit = _matNvlUnit;
            string qtyC = txt_Mat_Material_Qty.Text.Trim();
            int prodQtyC = (qtyC != "" && qtyC != null) ? Convert.ToInt32(qtyC) : 0;
            string prodUnitC = _matPackType;
            if (prodIDx > 0 && prodQty > 0)
            {
                table2.Rows.Add(prodIDx, prodName, prodCode, prodQty, prodUnit, prodQtyC, prodUnitC);
                view2 = new DataView(table2);
                _dgv_Mat_Material_Order_Width = cls.fnGetDataGridWidth(dgv_Mat_Material_Order);

                dgv_Mat_Material_Order.DataSource = view2;
                dgv_Mat_Material_Order.Refresh();


                //dgv_Mat_Material_Order.Columns[0].Width = 20 * _dgv_Mat_Material_Order_Width / 100;    // matId
                dgv_Mat_Material_Order.Columns[1].Width = 52 * _dgv_Mat_Material_Order_Width / 100;    // matName
                //dgv_Mat_Material_Order.Columns[2].Width = 20 * _dgv_Mat_Material_Order_Width / 100;    // matCode
                dgv_Mat_Material_Order.Columns[3].Width = 12 * _dgv_Mat_Material_Order_Width / 100;    // matQty
                dgv_Mat_Material_Order.Columns[4].Width = 12 * _dgv_Mat_Material_Order_Width / 100;    // matUnit
                dgv_Mat_Material_Order.Columns[5].Width = 12 * _dgv_Mat_Material_Order_Width / 100;    // matQtyC
                dgv_Mat_Material_Order.Columns[6].Width = 12 * _dgv_Mat_Material_Order_Width / 100;    // matUnitC

                dgv_Mat_Material_Order.Columns[0].Visible = false;
                dgv_Mat_Material_Order.Columns[1].Visible = true;
                dgv_Mat_Material_Order.Columns[2].Visible = false;
                dgv_Mat_Material_Order.Columns[3].Visible = true;
                dgv_Mat_Material_Order.Columns[4].Visible = true;
                dgv_Mat_Material_Order.Columns[5].Visible = true;
                dgv_Mat_Material_Order.Columns[6].Visible = true;

                cls.fnFormatDatagridview(dgv_Mat_Material_Order, 10, 30);
                dgv_Mat_Material_Order.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            else
            {
                MessageBox.Show("Không thể thêm khi số lượng bằng 0");
            }

            _matNvlIDx = "";
            _matNvlName = "";
            _matNvlCode = "";
            _matNvlQty = "";
            _matNvlUnit = "";
            _matNvlPCS = "";
            _matNvlBOX = "";
            _matNvlPAK = "";
            _matNvlPAL = "";
            _matPackType = "";
            _matPackQty = "";

            dgv_Mat_Material_List.ClearSelection();
            rdb_Mat_Material_PCS.Checked = false;
            rdb_Mat_Material_PCS.Enabled = false;
            rdb_Mat_Material_PCS.BackColor = Color.Silver;
            lbl_Mat_Material_PCS.Text = "0";
            lbl_Mat_Material_PCS.Enabled = false;
            lbl_Mat_Material_PCS.BackColor = Color.Gainsboro;

            rdb_Mat_Material_BOX.Checked = false;
            rdb_Mat_Material_BOX.Enabled = false;
            rdb_Mat_Material_BOX.BackColor = Color.Silver;
            lbl_Mat_Material_BOX.Text = "0";
            lbl_Mat_Material_BOX.Enabled = false;
            lbl_Mat_Material_BOX.BackColor = Color.Gainsboro;

            rdb_Mat_Material_PAK.Checked = false;
            rdb_Mat_Material_PAK.Enabled = false;
            rdb_Mat_Material_PAK.BackColor = Color.Silver;
            lbl_Mat_Material_PAK.Text = "0";
            lbl_Mat_Material_PAK.Enabled = false;
            lbl_Mat_Material_PAK.BackColor = Color.Gainsboro;

            rdb_Mat_Material_PAL.Checked = false;
            rdb_Mat_Material_PAL.Enabled = false;
            rdb_Mat_Material_PAL.BackColor = Color.Silver;
            lbl_Mat_Material_PAL.Text = "0";
            lbl_Mat_Material_PAL.Enabled = false;
            lbl_Mat_Material_PAL.BackColor = Color.Gainsboro;

            txt_Mat_Material_Qty.Text = "0";
            txt_Mat_Material_Qty.Enabled = false;
            lnk_Mat_Material_Add.Enabled = false;
            lnk_Mat_Material_Del.Enabled = false;

            txt_Mat_Material_Reason.Enabled = (dgv_Mat_Material_Order.Rows.Count > 0) ? true : false;

            btn_Mat_Material_Save.Enabled = false;
            btn_Mat_Material_Done.Enabled = true;
        }

        private void lnk_Mat_Material_Del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgv_Mat_Material_Order.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgv_Mat_Material_Order.Rows.Remove(row);
                }

                dgv_Mat_Material_Order.ClearSelection();
                _matNvlIDx = "";
                _matNvlName = "";
                _matNvlCode = "";
                _matNvlQty = "";
                _matNvlUnit = "";
                _matNvlPCS = "";
                _matNvlBOX = "";
                _matNvlPAK = "";
                _matNvlPAL = "";
                _matPackType = "";
                _matPackQty = "";

                lnk_Mat_Material_Add.Enabled = false;
                lnk_Mat_Material_Del.Enabled = false;
                btn_Mat_Material_Save.Enabled = false;

                if (dgv_Mat_Material_Order.Rows.Count == 0)
                {
                    lnk_Mat_Material_Add.Enabled = false;
                    lnk_Mat_Material_Del.Enabled = false;
                    txt_Mat_Material_Reason.Text = "";
                    txt_Mat_Material_Reason.Enabled = false;
                    btn_Mat_Material_Done.Enabled = false;
                }

                _msgText = "Xóa vật tư khỏi danh sách yêu cầu thành công.";
                _msgType = 0;

            }
            dgv_Mat_Material_Order.ClearSelection();
        }

        private void dgv_Mat_Material_Order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Mat_Material_Order, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Mat_Material_Order.Rows[e.RowIndex];

                string matIDx = row.Cells[0].Value.ToString();
                string matName = row.Cells[1].Value.ToString();
                string matCode = row.Cells[2].Value.ToString();
                string matQty = row.Cells[3].Value.ToString();
                string matUnit = row.Cells[4].Value.ToString();
                string matQtyC = row.Cells[5].Value.ToString();
                string matUnitC = row.Cells[6].Value.ToString();

                lnk_Mat_Material_Add.Enabled = false;
                lnk_Mat_Material_Del.Enabled = true;
            }

        }

        private void txt_Mat_Material_Reason_TextChanged(object sender, EventArgs e)
        {
            string reason = txt_Mat_Material_Reason.Text.Trim();
            btn_Mat_Material_Save.Enabled = (reason.Length > 0) ? true : false;
        }

        private void btn_Mat_Material_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string picIDx = _logIDx.ToString();
                    string picName = _logName;
                    string orderCode = init_Order_Code();
                    DateTime orderDate = _dt;
                    string orderNote = txt_Mat_Material_Reason.Text.Trim();
                    string whIDx = cbb_Mat_Material_Warehouse.SelectedValue.ToString();

                    string matIDx = "", matName = "", matCode = "", orderQty = "", orderUnit = "", orderQtyC = "", orderUnitC = "";
                    foreach (DataGridViewRow row in dgv_Mat_Material_Order.Rows)
                    {
                        matIDx = row.Cells[0].Value.ToString();
                        matName = row.Cells[1].Value.ToString();
                        matCode = row.Cells[2].Value.ToString();
                        orderQty = row.Cells[3].Value.ToString();
                        orderUnit = row.Cells[4].Value.ToString();
                        orderQtyC = row.Cells[5].Value.ToString();
                        orderUnitC = row.Cells[6].Value.ToString();

                        string sql = "PMMS_01_Order_Material_AddItem_V1o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[12]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.VarChar;
                        sParams[0].ParameterName = "@orderCode";
                        sParams[0].Value = orderCode;

                        //sParams[1] = new SqlParameter();
                        //sParams[1].SqlDbType = SqlDbType.DateTime;
                        //sParams[1].ParameterName = "@orderDate";
                        //sParams[1].Value = orderDate;

                        //sParams[2] = new SqlParameter();
                        //sParams[2].SqlDbType = SqlDbType.VarChar;
                        //sParams[2].ParameterName = "@department";
                        //sParams[2].Value = "DONG-A VINA";

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.NVarChar;
                        sParams[1].ParameterName = "@picName";
                        sParams[1].Value = picName;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.NVarChar;
                        sParams[2].ParameterName = "@orderNote";
                        sParams[2].Value = orderNote;

                        sParams[3] = new SqlParameter();
                        sParams[3].SqlDbType = SqlDbType.Int;
                        sParams[3].ParameterName = "@matId";
                        sParams[3].Value = matIDx;

                        sParams[4] = new SqlParameter();
                        sParams[4].SqlDbType = SqlDbType.NVarChar;
                        sParams[4].ParameterName = "@matName";
                        sParams[4].Value = matName;

                        sParams[5] = new SqlParameter();
                        sParams[5].SqlDbType = SqlDbType.VarChar;
                        sParams[5].ParameterName = "@matCode";
                        sParams[5].Value = matCode;

                        sParams[6] = new SqlParameter();
                        sParams[6].SqlDbType = SqlDbType.SmallMoney;
                        sParams[6].ParameterName = "@orderQty";
                        sParams[6].Value = orderQty;

                        sParams[7] = new SqlParameter();
                        sParams[7].SqlDbType = SqlDbType.VarChar;
                        sParams[7].ParameterName = "@orderUnit";
                        sParams[7].Value = orderUnit;

                        sParams[8] = new SqlParameter();
                        sParams[8].SqlDbType = SqlDbType.SmallMoney;
                        sParams[8].ParameterName = "@orderQtyC";
                        sParams[8].Value = orderQtyC;

                        sParams[9] = new SqlParameter();
                        sParams[9].SqlDbType = SqlDbType.VarChar;
                        sParams[9].ParameterName = "@orderUnitC";
                        sParams[9].Value = orderUnitC;

                        //sParams[12] = new SqlParameter();
                        //sParams[12].SqlDbType = SqlDbType.NVarChar;
                        //sParams[12].ParameterName = "@orderReason";
                        //sParams[12].Value = "";

                        sParams[10] = new SqlParameter();
                        sParams[10].SqlDbType = SqlDbType.Int;
                        sParams[10].ParameterName = "@picIDx";
                        sParams[10].Value = picIDx;

                        sParams[11] = new SqlParameter();
                        sParams[11].SqlDbType = SqlDbType.Int;
                        sParams[11].ParameterName = "@whIDx";
                        sParams[11].Value = whIDx;

                        cls.fnUpdDel(sql, sParams);
                    }

                    _msgText = "Tạo yêu cầu thành công.";
                    _msgType = 1;
                }
                catch (SqlException sqlEx)
                {
                    _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng liên hệ quản trị hệ thống để được hỗ trợ.";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    _msgText = "Có lỗi phát sinh. Vui lòng liên hệ quản trị hệ thống để được hỗ trợ.";
                    _msgType = 2;
                }
                finally
                {
                    init_Order_Material_List();
                    init_Order_Material_Done(0);
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }

        private void btn_Mat_Material_Done_Click(object sender, EventArgs e)
        {
            init_Order_Material_Done(0);
        }


        #endregion

    }
}
