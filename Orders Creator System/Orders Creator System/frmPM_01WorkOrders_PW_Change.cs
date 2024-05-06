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
    public partial class frmPM_01WorkOrders_PW_Change : Form
    {
        string _logIDx = "";
        string _oldPwd = "";


        public frmPM_01WorkOrders_PW_Change(string logIDx)
        {
            InitializeComponent();

            _logIDx = logIDx;
        }

        private void frmPM_01WorkOrders_PW_Change_Load(object sender, EventArgs e)
        {
            string logIDx = _logIDx;
            string logName = "", logTitle = "", logUsn = "", logPwd = "";
            string sql = "PMMS_01_Change_Password_User_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@logIDx";
            sParams[0].Value = logIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                logName = ds.Tables[0].Rows[0][1].ToString();
                logTitle = ds.Tables[0].Rows[0][2].ToString();
                logUsn = ds.Tables[0].Rows[0][3].ToString();
                logPwd = ds.Tables[0].Rows[0][4].ToString();

                _oldPwd = logPwd;

                lbl_User_Name.Text = logName;
                lbl_User_Title.Text = logTitle;
                lbl_User_Username.Text = "Username: " + logUsn;

                txt_PW_current.Enabled = true;
                txt_PW_current.Text = "";
                txt_PW_current.Focus();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin của người dùng (" + logIDx + ").\r\nVui lòng liên hệ quản trị hệ thống để được hỗ trợ.");
                this.Close();
            }
            lbl_PW_Status.Text = "Mật khẩu phải có độ dài ít nhất 8 ký tự";
        }

        private void txt_PW_current_TextChanged(object sender, EventArgs e)
        {
            string curPwd = txt_PW_current.Text.Trim();
            if (curPwd.Length > 0)
            {
                txt_PW_new.Enabled = true;
                txt_PW_new.Text = "";
            }
            else
            {
                txt_PW_new.Text = "";
                txt_PW_new.Enabled = false;

                txt_PW_confirm.Text = "";
                txt_PW_confirm.Enabled = false;

                btn_Save.Enabled = false;
            }
        }

        private void txt_PW_new_TextChanged(object sender, EventArgs e)
        {
            string newPwd = txt_PW_new.Text.Trim();
            int lenNewPwd = newPwd.Length;
            if (lenNewPwd >= 8)
            {
                txt_PW_confirm.Enabled = true;
                txt_PW_confirm.Text = "";
            }
            else
            {
                txt_PW_confirm.Text = "";
                txt_PW_confirm.Enabled = false;

                btn_Save.Enabled = false;
            }
        }

        private void txt_PW_confirm_TextChanged(object sender, EventArgs e)
        {
            string newPwd = txt_PW_new.Text.Trim();
            int lenNewPwd = newPwd.Length;
            string cfmPwd = txt_PW_confirm.Text.Trim();
            if (cfmPwd.Length >= 8)
            {
                btn_Save.Enabled = (cfmPwd == newPwd) ? true : false;
            }
            else
            {
                btn_Save.Enabled = false;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string logIDx = _logIDx;
                string curPwd = txt_PW_current.Text.Trim();
                string newPwd = txt_PW_new.Text.Trim();
                string oldPwd = _oldPwd;

                if (curPwd == oldPwd)
                {
                    if (newPwd.Length >= 8)
                    {
                        string sql = "PMMS_01_Change_Password_User_UpdItem_V1o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@logIDx";
                        sParams[0].Value = logIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.VarChar;
                        sParams[1].ParameterName = "@newPwd";
                        sParams[1].Value = newPwd;

                        cls.fnUpdDel(sql, sParams);

                        this.Close();

                        MessageBox.Show("Thay đổi mật khẩu thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu phải có độ dài ít nhất 8 ký tự.\r\nVui lòng nhập lại.");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu ở ô hiện tại không khớp với mật khẩu đang dùng.\r\nVui lòng nhập lại.");
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi phát sinh.\r\nVui lòng liên hệ quản trị hệ thống để được hỗ trợ");
            }
            finally
            {
                
            }

        }
    }
}
