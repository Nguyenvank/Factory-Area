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
    public partial class frmFinishGoodDataLabel_v1o1 : Form
    {

        public int _dgvMake_List_Width;
        public int _dgvView_List_Width;

        public string labelFor = "MMT";

        public DateTime _dt;

        public frmFinishGoodDataLabel_v1o1()
        {
            InitializeComponent();
        }

        private void frmFinishGoodDataLabel_v1o1_Load(object sender, EventArgs e)
        {
            _dgvMake_List_Width = cls.fnGetDataGridWidth(dgvMake_List);
            _dgvView_List_Width = cls.fnGetDataGridWidth(dgvView_List);

            _dt = DateTime.Now;

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            initMake();
        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        #region MAKE DATA

        public void initMake()
        {
            initMake_Type();
            initMake_Step();
            initMake_Start();
            initMake_Final();
            initMake_Preview();
            initMake_List();
        }

        public void initMake_Type()
        {
            cbbMake_Type.Items.Clear();
            if (labelFor.ToUpper() == "PRO")
            {
                cbbMake_Type.Items.Add("PCS");
                cbbMake_Type.Items.Add("BOX");
                cbbMake_Type.Items.Add("CAR");
                cbbMake_Type.Items.Add("PAL");
                cbbMake_Type.Items.Add("069");
            }
            else if (labelFor.ToUpper() == "MMT")
            {
                cbbMake_Type.Items.Add("PCS");
                cbbMake_Type.Items.Add("BOX");
                cbbMake_Type.Items.Add("PAK");
                cbbMake_Type.Items.Add("PAL");
                cbbMake_Type.Items.Add("SBM");
            }
            cbbMake_Type.Items.Insert(0, "");
            cbbMake_Type.SelectedIndex = 0;
        }

        public void initMake_Step()
        {

        }

        public void initMake_Start()
        {

        }

        public void initMake_Final()
        {

        }

        public void initMake_Preview()
        {
            string type = cbbMake_Type.Text;
            string step = txtMake_Step.Text.Trim();
            string start = txtMake_Start.Text.Trim();
            string final = txtMake_Final.Text.Trim();
            string preview = "";
            int _step = 0, _start = 0, _final = 0;
            _step = (step != "" && step != null) ? Convert.ToInt32(step) : 0;
            _start = (start != "" && start != null) ? Convert.ToInt32(start) : 0;
            _final = (final != "" && final != null) ? Convert.ToInt32(final) : 0;
            string prefix = cls.fnGetDate("lot");

            if (type == "" || step == "" || start == "" || final == "")
            {
                dgvMake_List.DataSource = "";
                dgvMake_List.Refresh();

                lblMake_Preview.Text = "N/A";
                btnMake_Save.Enabled = false;

            }
            else
            {
                switch (labelFor.ToUpper())
                {
                    case "PRO":
                        preview = "PRO-" + type + "-" + prefix + "" + String.Format("{0:0000}", _start);
                        break;
                    case "MMT":
                        preview = "MMT-" + type + "-" + prefix + "" + String.Format("{0:0000}", _start);
                        break;
                }
                lblMake_Preview.Text = preview;

                initMake_List();
                btnMake_Save.Enabled = true;
            }
        }

        public void initMake_List()
        {
            string type = cbbMake_Type.Text;
            //string sql = "V2o1_BASE_Inventory_Barcode_Label_SelItem_Addnew";
            string sql = "V2o1_BASE_Inventory_Barcode_Label_SelItem_V2o1_Addnew"; 
            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.VarChar;
            sParams[0].ParameterName = "@type";
            sParams[0].Value = type;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@kind";
            sParams[1].Value = labelFor.ToLower();

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvMake_List_Width = cls.fnGetDataGridWidth(dgvMake_List);
            dgvMake_List.DataSource = dt;

            //dgvMake_List.Columns[0].Width = 25 * _dgvMake_List_Width / 100;    // boxId
            dgvMake_List.Columns[1].Width = 80 * _dgvMake_List_Width / 100;    // boxcode
            //dgvMake_List.Columns[2].Width = 40 * _dgvMake_List_Width / 100;    // boxpartname
            dgvMake_List.Columns[3].Width = 20 * _dgvMake_List_Width / 100;    // boxquantity

            dgvMake_List.Columns[0].Visible = false;
            dgvMake_List.Columns[1].Visible = true;
            dgvMake_List.Columns[2].Visible = false;
            dgvMake_List.Columns[3].Visible = true;

            cls.fnFormatDatagridview(dgvMake_List, 12, 30);
            dgvMake_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        private void txtMake_Step_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMake_Start_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMake_Final_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cbbMake_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbMake_Type.SelectedIndex > 0)
            {
                txtMake_Step.Enabled = true;
                txtMake_Step.Text = "1";
                txtMake_Step.Focus();
                initMake_Preview();
                initMake_List();

                btnMake_Done.Enabled = true;
            }
            else
            {
                dgvMake_List.DataSource = "";
                dgvMake_List.Refresh();

                txtMake_Step.Text = "";
                txtMake_Step.Enabled = false;

                btnMake_Done.Enabled = false;
            }
        }

        private void txtMake_Step_TextChanged(object sender, EventArgs e)
        {
            if (txtMake_Step.Text.Length > 0 && txtMake_Step.Text != "0")
            {
                txtMake_Start.Enabled = true;
                txtMake_Start.Text = "1";
            }
            else
            {
                txtMake_Start.Text = "";
                txtMake_Start.Enabled = false;
            }
        }

        private void txtMake_Start_TextChanged(object sender, EventArgs e)
        {
            if (txtMake_Start.Text.Length > 0 && txtMake_Start.Text != "0")
            {
                txtMake_Final.Enabled = true;
                txtMake_Final.Text = "1000";
            }
            else
            {
                txtMake_Final.Text = "";
                txtMake_Final.Enabled = false;
            }
        }

        private void txtMake_Final_TextChanged(object sender, EventArgs e)
        {
            if (txtMake_Final.Text.Length > 0 && txtMake_Final.Text != "0")
            {
                initMake_Preview();
                btnMake_Save.Enabled = true;
            }
            else
            {
                lblMake_Preview.Text = "N/A";
                btnMake_Save.Enabled = false;
            }
        }

        private void btnMake_Done_Click(object sender, EventArgs e)
        {
            fnMake_Done();
        }

        public void fnMake_Done()
        {
            cbbMake_Type.SelectedIndex = 0;
            cbbMake_Type.Enabled = false;
            txtMake_Step.Text = "";
            txtMake_Step.Enabled = false;
            txtMake_Start.Text = "";
            txtMake_Start.Enabled = false;
            txtMake_Final.Text = "";
            txtMake_Final.Enabled = false;

            lblMake_Preview.Text = "N/A";

            dgvMake_List.DataSource = "";
            dgvMake_List.Refresh();

            btnMake_Save.Enabled = false;
            btnMake_Done.Enabled = false;
        }

        private void btnMake_Save_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string type = cbbMake_Type.Text;
                    string step = txtMake_Step.Text.Trim();
                    string start = txtMake_Start.Text.Trim();
                    string final = txtMake_Final.Text.Trim();
                    string kind = labelFor.ToUpper();

                    //string sql = "V2o1_BASE_Inventory_Barcode_Label_AddItem_Addnew";
                    string sql = "V2o1_BASE_Inventory_Barcode_Label_AddItem_V2o1_Addnew";
                    
                    SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.VarChar;
                    sParams[0].ParameterName = "@type";
                    sParams[0].Value = type;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@step";
                    sParams[1].Value = step;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.Int;
                    sParams[2].ParameterName = "@start";
                    sParams[2].Value = start;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.Int;
                    sParams[3].ParameterName = "@final";
                    sParams[3].Value = final;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.VarChar;
                    sParams[4].ParameterName = "@kind";
                    sParams[4].Value = kind;

                    cls.fnUpdDel(sql, sParams);
                    initMake_List();

                    tssMsg.Text = "Tạo mới thành công.";
                    tssMsg.ForeColor = Color.Green;
                }
            }
            catch
            {
                tssMsg.Text = "Có lỗi khi tạo.";
                tssMsg.ForeColor = Color.Red;
            }
            finally
            {

            }
        }

        private void lblMake_Preview_TextChanged(object sender, EventArgs e)
        {
            if (lblMake_Preview.Text != "N/A")
            {
                btnMake_Save.Enabled = true;
            }
            else
            {
                btnMake_Save.Enabled = false;
            }
        }



        #endregion


        #region VIEW DATA

        public void initView()
        {
            initView_List();
        }

        public void initView_List()
        {
            DateTime date = dtpView_Date.Value;
            //string sql = "V2o1_BASE_Inventory_Barcode_Label_ViewItem_Addnew";
            string sql = "V2o1_BASE_Inventory_Barcode_Label_ViewItem_V2o1_Addnew";
            
            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = date;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@kind";
            sParams[1].Value = labelFor;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvView_List_Width = cls.fnGetDataGridWidth(dgvView_List);
            dgvView_List.DataSource = dt;

            //dgvView_List.Columns[0].Width = 25 * _dgvView_List_Width / 100;    // boxId
            dgvView_List.Columns[1].Width = 80 * _dgvView_List_Width / 100;    // boxcode
            //dgvView_List.Columns[2].Width = 40 * _dgvView_List_Width / 100;    // boxpartname
            dgvView_List.Columns[3].Width = 20 * _dgvView_List_Width / 100;    // boxquantity

            dgvView_List.Columns[0].Visible = false;
            dgvView_List.Columns[1].Visible = true;
            dgvView_List.Columns[2].Visible = false;
            dgvView_List.Columns[3].Visible = true;

            cls.fnFormatDatagridview(dgvView_List, 12, 30);
            dgvView_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        }

        private void btnView_List_Click(object sender, EventArgs e)
        {
            initView_List();
        }


        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initMake();
                    break;
                case 1:
                    initView();
                    break;
            }
        }

        private void thoátChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void temPROToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFor = "PRO";
            initMake();
        }

        private void temMMTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFor = "MMT";
            initMake();
        }
    }
}
