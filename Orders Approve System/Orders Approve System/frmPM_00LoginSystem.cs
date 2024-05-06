using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmPM_00LoginSystem : Form
    {

        public int _loginForm=0;

        public frmPM_00LoginSystem(int logForm)
        {
            InitializeComponent();

            _loginForm = logForm;
        }

        private void frmPM_00LoginSystem_Load(object sender, EventArgs e)
        {
            txtUsn.Text = "nguyen thi phuong";
            txtPwd.Text = "d1800s";
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string usn = txtUsn.Text.Trim();
                string pwd = txtPwd.Text.Trim();

                if (usn.Length > 0 && pwd.Length > 0)
                {
                    string logIDx = "", logLevel = "", logName = "", logDept = "";
                    int _logIDx = 0, _logLevel = 0, _logDept = 0;
                    string sql = "PMMS_00_Login_To_System_SelItem_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.VarChar;
                    sParams[0].ParameterName = "@usn";
                    sParams[0].Value = usn;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.VarChar;
                    sParams[1].ParameterName = "@pwd";
                    sParams[1].Value = pwd;

                    DataSet ds = new DataSet();
                    ds = cls.ExecuteDataSet(sql, sParams);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            logIDx = ds.Tables[0].Rows[0][0].ToString();
                            logName = ds.Tables[0].Rows[0][1].ToString();
                            logLevel = ds.Tables[0].Rows[0][2].ToString();
                            logDept = ds.Tables[0].Rows[0][3].ToString();

                            _logIDx = (logIDx != "" && logIDx != null) ? Convert.ToInt32(logIDx) : 0;
                            _logLevel = (logLevel != "" && logLevel != null) ? Convert.ToInt32(logLevel) : 0;
                            _logDept = (logDept != "" && logDept != null) ? Convert.ToInt32(logDept) : 0;

                            switch (_loginForm)
                            {
                                case 1:
                                    //frmPM_01WorkOrders frmWorkOrder = new frmPM_01WorkOrders(_logIDx, _logLevel, logName);
                                    //frmWorkOrder.WindowState = FormWindowState.Maximized;
                                    ////frmWorkOrder.MdiParent = this.MdiParent;
                                    //frmWorkOrder.Show();
                                    break;
                                case 2:
                                    if (_logLevel <= 4)
                                    {
                                        frmPM_02WorkApprove frmWorkApprove = new frmPM_02WorkApprove(_logIDx, _logLevel, logName);
                                        frmWorkApprove.WindowState = FormWindowState.Maximized;
                                        //frmWorkApprove.MdiParent = this.MdiParent;
                                        frmWorkApprove.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Rất tiếc, bạn không có đủ quyền để sử dụng chức năng này.\r\nVui lòng thông báo với quản trị hệ thống nếu lời nhắn này không đúng.");
                                    }
                                    break;
                                case 3:
                                    //if (_logDept == 0 || _logDept == 8)
                                    //{
                                    //    frmPM_03RepairProcess frmRepairProcess = new frmPM_03RepairProcess(_logIDx, _logLevel, logName);
                                    //    frmRepairProcess.WindowState = FormWindowState.Maximized;
                                    //    frmRepairProcess.MdiParent = this.MdiParent;
                                    //    frmRepairProcess.Show();
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("Rất tiếc, bạn không thuộc nhân sự của chức năng này.\r\nVui lòng thông báo với quản trị hệ thống nếu lời nhắn này không đúng.");
                                    //}
                                    break;
                                case 4:
                                    break;
                            }
                            this.Hide();
                        }
                        else
                        {
                            logIDx = "0";
                            logLevel = "0";
                            logName = "";

                            _logIDx = 0;
                            _logLevel = 0;

                            txtUsn.Text = "";
                            txtUsn.Focus();
                            txtPwd.Text = "";

                            lblMsg.Text = "Khong dung ten truy cap hoac mat khau. Hay thu lai..";
                        }
                    }
                    else
                    {
                        logIDx = "0";
                        logLevel = "0";
                        logName = "";

                        _logIDx = 0;
                        _logLevel = 0;

                        txtUsn.Text = "";
                        txtUsn.Focus();
                        txtPwd.Text = "";

                        lblMsg.Text = "Khong dung ten truy cap hoac mat khau. Hay thu lai..";
                    }
                }
                else
                {
                    txtUsn.Text = "";
                    txtUsn.Focus();
                    txtPwd.Text = "";

                    lblMsg.Text = "Vui long nhap ten truy cap va mat khau..";
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txtUsn_TextChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            lblMsg.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }

        private void txtUsn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPwd.Focus();
            }
        }
    }
}
