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
    public partial class frmPM_01WorkOrders_ES_Details : Form
    {
        string _regIDx = "";

        public frmPM_01WorkOrders_ES_Details()
        {
            InitializeComponent();
        }

        public frmPM_01WorkOrders_ES_Details(string regIDx)
        {
            InitializeComponent();

            _regIDx = regIDx;
        }

        private void frmPM_01WorkOrders_ES_Details_Load(object sender, EventArgs e)
        {
            fnc_Register_Load();
            fnc_Register_Delete();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnc_Register_Delete();
        }

        private void frmPM_01WorkOrders_ES_Details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void fnc_Register_Load()
        {
            string regIDx = _regIDx;

            string makerNm = "", makerDept = "";
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

                lbl_ES_Maker_Nm.Text = makerNm.ToUpper();
                lbl_ES_Maker_Dept.Text = makerDept.ToUpper();
                lbl_ES_Reg_Create.Text = regCreate;
                lbl_ES_Reg_Kind.Text = regKind;
                lbl_ES_Reg_From.Text = regFrom;
                lbl_ES_Reg_Nm.Text = regNm;
                lbl_ES_Reg_IDNo.Text = regID;
                lbl_ES_Reg_Total.Text = regQty;
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
                        lbl_ES_Approve_Manager.BackColor = Color.Silver;
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
                        lbl_ES_Approve_Director.BackColor = Color.Silver;
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
                        lbl_ES_Approve_HR.BackColor = Color.Silver;
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
                catch(SqlException sqlEx)
                {
                    MessageBox.Show("Có lỗi kết nối dữ liệu, vui lòng thử lại hoặc liên hệ quản trị hệ thống.");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Có lỗi phát sinh, vui lòng thử lại hoặc liên hệ quản trị hệ thống.");
                }
                finally
                {
                    this.Close();
                }
                
            }
        }
    }
}
