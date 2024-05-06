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
    public partial class frmPM_02WorkApprove_ES_Details : Form
    {
        string _regIDx = "";
        string _aprIDx = "";
        string _aprTitleIDx = "";

        public frmPM_02WorkApprove_ES_Details()
        {
            InitializeComponent();
        }

        public frmPM_02WorkApprove_ES_Details(string regIDx, int aprIDx)
        {
            InitializeComponent();

            _regIDx = regIDx;
            _aprIDx = aprIDx.ToString();


        }

        private void frmPM_02WorkApprove_ES_Details_Load(object sender, EventArgs e)
        {
            fnc_Register_Load();
            fnc_Register_Delete();

            cbb_ES_Approve_Choice.Items.Clear();
            cbb_ES_Approve_Choice.Items.Add("");
            cbb_ES_Approve_Choice.Items.Add("Accept");
            cbb_ES_Approve_Choice.Items.Add("Deny");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnc_Register_Delete();
        }

        private void frmPM_02WorkApprove_ES_Details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void fnc_Register_Load()
        {
            string regIDx = _regIDx;
            //string aprIDx = _aprIDx;

            string makerNm = "", makerDeptIDx = "", makerDept = "", makerTitleIDx = "", makerTitle = "";
            string aprIDx = "", aprName = "", aprTitleIDx = "", aprTitle = "";
            string regCreate = "", regKind = "";
            string regFrom = "", regNm = "", regID = "", regQty = "", regPurpose = "";
            string regDate = "", regTime = "", regRemark = "", regTool = "", regToolImg = "";
            string managerApprove = "", managerApproveStatus = "";
            string directorApprove = "", directorApproveStatus = "";
            string hrApprove = "", hrApproveStatus = "";
            string securityControl = "", securityControlStatus = "";
            string regFinish = "", regFinishDate = "";

            string sql = "V2o1_ERP_Entrance_System_Reg_Detail_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@regIDx";
            sParams[0].Value = regIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                makerNm = ds.Tables[0].Rows[0][2].ToString();
                makerDept = ds.Tables[0].Rows[0][3].ToString();
                regCreate = ds.Tables[0].Rows[0][22].ToString();
                regKind = ds.Tables[0].Rows[0][4].ToString();
                regFrom = ds.Tables[0].Rows[0][5].ToString();
                regNm = ds.Tables[0].Rows[0][6].ToString();
                regID = ds.Tables[0].Rows[0][7].ToString();
                regQty = ds.Tables[0].Rows[0][8].ToString();
                regTool = ds.Tables[0].Rows[0][12].ToString().ToLower();
                regToolImg = ds.Tables[0].Rows[0][25].ToString();
                regPurpose = ds.Tables[0].Rows[0][9].ToString();
                regDate = ds.Tables[0].Rows[0][10].ToString();
                regTime = ds.Tables[0].Rows[0][11].ToString();
                regRemark = ds.Tables[0].Rows[0][13].ToString();
                managerApprove = ds.Tables[0].Rows[0][14].ToString();
                managerApproveStatus = ds.Tables[0].Rows[0][15].ToString();
                directorApprove = ds.Tables[0].Rows[0][16].ToString();
                directorApproveStatus = ds.Tables[0].Rows[0][17].ToString();
                hrApprove = ds.Tables[0].Rows[0][18].ToString();
                hrApproveStatus = ds.Tables[0].Rows[0][19].ToString();
                securityControl = ds.Tables[0].Rows[0][20].ToString();
                securityControlStatus = ds.Tables[0].Rows[0][21].ToString();
                regFinish = ds.Tables[0].Rows[0][23].ToString();
                regFinishDate = ds.Tables[0].Rows[0][24].ToString();
                //bmpTools = ds.Tables[0].Rows[0][25].ToString();
                aprIDx = ds.Tables[0].Rows[0][26].ToString();
                aprName = ds.Tables[0].Rows[0][27].ToString();
                aprTitleIDx = ds.Tables[0].Rows[0][28].ToString();
                aprTitle = ds.Tables[0].Rows[0][29].ToString();
                //makerDeptIDx = ds.Tables[0].Rows[0][25].ToString();
                //makerTitleIDx = ds.Tables[0].Rows[0][26].ToString();
                //makerTitle = ds.Tables[0].Rows[0][27].ToString();

                lbl_ES_Maker_Nm.Text = makerNm.ToUpper();
                lbl_ES_Maker_Dept.Text = makerDept.ToUpper();
                lbl_ES_Reg_Create.Text = regCreate;
                lbl_ES_Reg_Kind.Text = regKind;
                lbl_ES_Reg_From.Text = regFrom;
                lbl_ES_Reg_Nm.Text = regNm;
                lbl_ES_Reg_IDNo.Text = regID;
                lbl_ES_Reg_Total.Text = regQty;

                lbl_ES_Approve_Title.Text = aprName.ToUpper() + " (" + aprTitle + ")";
                //lbl_ES_Approve_Manager.Text = aprName.ToUpper() + "\r\n" + aprTitle;
                //btn_ES_Approve_Director.Text = "";
                //btn_ES_Approve_HR.Text = "";


                switch (aprTitleIDx)
                {
                    case "1":
                    case "2":
                    case "3":
                        lbl_ES_Approve_Manager.Enabled = false;
                        lbl_ES_Approve_Director.Enabled = true;
                        lbl_ES_Approve_HR.Enabled = false;
                        break;
                    case "4":
                        lbl_ES_Approve_Manager.Enabled = true;
                        lbl_ES_Approve_Director.Enabled = false;
                        lbl_ES_Approve_HR.Enabled = false;
                        break;
                    default:
                        lbl_ES_Approve_Manager.Enabled = false;
                        lbl_ES_Approve_Director.Enabled = false;
                        lbl_ES_Approve_HR.Enabled = false;
                        break;
                }

                if (regTool == "true")
                {
                    lbl_ES_Reg_Tools.Text = "YES";
                    pnl_ES_Reg_Tools.BackColor = Color.White;
                    if (regToolImg.Length > 0)
                    {
                        Bitmap bmpIssue;
                        MemoryStream msIssue;
                        msIssue = new MemoryStream((byte[])ds.Tables[0].Rows[0][25]);
                        bmpIssue = new Bitmap(msIssue);
                        pnl_ES_Reg_Tools.BackgroundImage = bmpIssue;
                        pnl_ES_Reg_Tools.BackgroundImageLayout = ImageLayout.Stretch;

                        bmpIssue = null;
                        msIssue = null;

                    }
                    else
                    {
                        pnl_ES_Reg_Tools.BackgroundImage = null;
                    }
                    pnl_ES_Reg_Tools_Border.BackColor = Color.White;
                }
                else
                {
                    lbl_ES_Reg_Tools.Text = "NO";
                    pnl_ES_Reg_Tools.BackColor = Color.LightGray;
                    pnl_ES_Reg_Tools.BackgroundImage = null;
                    pnl_ES_Reg_Tools_Border.BackColor = Color.LightGray;
                }

                lbl_ES_Reg_Date.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(regDate));
                lbl_ES_Reg_Date.Text += (regTime != "" && regTime != null) ? " " + String.Format("{0:HH:mm tt}", Convert.ToDateTime(regTime)) : "";
                lbl_ES_Reg_Purpose.Text = regPurpose;
                lbl_ES_Reg_Remark.Text = regRemark;

                switch (managerApprove)
                {
                    case "0":
                        // NOT APPROVE YET
                        lbl_ES_Approve_Manager.Text = "";
                        lbl_ES_Approve_Manager.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_Manager.BackColor = Color.LightGray;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        lbl_ES_Approve_Manager.Text = managerApproveStatus;
                        lbl_ES_Approve_Manager.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_Manager.BackColor = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        lbl_ES_Approve_Manager.Text = managerApproveStatus;
                        lbl_ES_Approve_Manager.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_Manager.BackColor = Color.LightPink;
                        break;
                }

                switch (directorApprove)
                {
                    case "0":
                        // NOT APPROVE YET
                        lbl_ES_Approve_Director.Text = "";
                        lbl_ES_Approve_Director.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_Director.BackColor = Color.LightGray;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        lbl_ES_Approve_Director.Text = directorApproveStatus;
                        lbl_ES_Approve_Director.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_Director.BackColor = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        lbl_ES_Approve_Director.Text = directorApproveStatus;
                        lbl_ES_Approve_Director.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_Director.BackColor = Color.LightPink;
                        break;
                }

                switch (hrApprove)
                {
                    case "0":
                        // NOT APPROVE YET
                        lbl_ES_Approve_HR.Text = "";
                        lbl_ES_Approve_HR.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_HR.BackColor = Color.LightGray;
                        break;
                    case "1":
                        // APPROVE ACCEPT
                        lbl_ES_Approve_HR.Text = hrApproveStatus;
                        lbl_ES_Approve_HR.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_HR.BackColor = Color.LightGreen;
                        break;
                    case "2":
                        // APPROVE DENY
                        lbl_ES_Approve_HR.Text = hrApproveStatus;
                        lbl_ES_Approve_HR.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                        lbl_ES_Approve_HR.BackColor = Color.LightPink;
                        break;
                }
            }
        }

        public void fnc_Register_Delete()
        {
            string regIDx = _regIDx;

            string manager = "", director = "", hr = "", security = "";
            string finish = "", finishDate = "";
            int _manager = 0, _director = 0, _hr = 0, _securiry = 0, _finish = 0;

            string sql = "V2o1_ERP_Entrance_System_Reg_Detail_Delete_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@regIDx";
            sParams[0].Value = regIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //manager = ds.Tables[0].Rows[0][0].ToString();
                director = ds.Tables[0].Rows[0][0].ToString();
                //hr = ds.Tables[0].Rows[0][2].ToString();
                //security = ds.Tables[0].Rows[0][3].ToString();
                //finish = ds.Tables[0].Rows[0][4].ToString();
                //finishDate = ds.Tables[0].Rows[0][5].ToString();

                //_manager = (manager != "" && manager != null) ? Convert.ToInt32(manager) : 0;
                _director = (director != "" && director != null) ? Convert.ToInt32(director) : 0;
                //_hr = (hr != "" && hr != null) ? Convert.ToInt32(hr) : 0;
                //_securiry = (security != "" && security != null) ? Convert.ToInt32(security) : 0;
                //_finish = (finish != "" && finish != null) ? Convert.ToInt32(finish) : 0;

                lnk_ES_Reg_Del.Visible = (_director == 0) ? true : false;
            }
        }

        private void lnk_ES_Reg_Del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string regIDx = _regIDx;
                    string sql = "V2o1_ERP_Entrance_System_Reg_Detail_DelItem_V1o1_Addnew";

                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@regIDx";
                    sParams[0].Value = regIDx;

                    cls.fnUpdDel(sql, sParams);

                    MessageBox.Show("Xóa đăng ký thành công!\r\nVui lòng refresh danh sách hiện có.");
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Có lỗi kết nối dữ liệu, vui lòng thử lại hoặc liên hệ quản trị hệ thống.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi phát sinh, vui lòng thử lại hoặc liên hệ quản trị hệ thống.");
                }
                finally
                {
                    this.Close();
                }

            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            //form.StartPosition = FormStartPosition.CenterScreen;
            form.StartPosition = FormStartPosition.Manual;
            //form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - form.Height) / 2);
            form.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - form.Width) / 2, 100);
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void cbb_ES_Approve_Choice_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int sel = cbb_ES_Approve_Choice.SelectedIndex;
            txt_ES_Approve_Reason.Text = "";
            switch (sel)
            {
                case 0:
                    lbl_ES_Approve_Manager.BackColor = Color.LightGray;
                    txt_ES_Approve_Reason.Enabled = false;
                    btn_ES_Approve_Save.Enabled = false;
                    break;
                case 1:
                    lbl_ES_Approve_Manager.BackColor = Color.LightGreen;
                    txt_ES_Approve_Reason.Enabled = false;
                    btn_ES_Approve_Save.Enabled = true;
                    break;
                case 2:
                    lbl_ES_Approve_Manager.BackColor = Color.LightPink;
                    txt_ES_Approve_Reason.Enabled = true;
                    txt_ES_Approve_Reason.Focus();
                    btn_ES_Approve_Save.Enabled = true;
                    break;
            }
        }

        private void txt_ES_Approve_Reason_TextChanged(object sender, EventArgs e)
        {
            int len = txt_ES_Approve_Reason.Text.Length;
            //btn_ES_Approve_Save.Enabled = (len > 0) ? true : false;
        }
    }
}
