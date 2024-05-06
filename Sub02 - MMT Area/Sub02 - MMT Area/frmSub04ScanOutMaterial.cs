using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmSub04ScanOutMaterial : Form
    {
        frmSub02ScanInMaterial frm02ScanIn;

        public string _idx = "", _code = "", _date = "", _total = "", _maker = "", _purpose = "", _priority = "", _reason = "", _add = "";
        public string _approve = "", _approveDate = "", _response = "", _handoverDate = "";

        public string _outIDx = "", _outCode = "", _outDate = "", _outTotal = "", _outMaker = "";
        public string _outPurpose = "", _outPriority = "", _outReason = "", _outAdd = "";
        public string _outApprove = "", _outApproveDate = "", _outResponse = "", _outHandoverDate = "";
        public string _outMatIDx = "", _outMatName = "", _outMatCode = "", _outMatUnit = "", _outMatQty = "", _outMatHave = "";
        public string _lstMatIDx = "", _lstPacking = "", _lstMatName = "", _lstMatCode = "", _lstMatUnit = "", _lstMatQty = "";
        public string _bindOrderIDx = "";

        public MemoryStream _ms;

        public string _totalOrder = "0", _totalScan = "0";

        public string _rangeUnAprv = "4";

        public int _dgvOut_Request_Width;
        public int _dgvOut_Detail_Width;
        public int _dgvOut_List_Width;

        public int _dgvAprv_List_Width;
        public int _dgvAprv_Detail_Width;
        public int _dgvUnAprv_List_Width;

        public int _dgvBind_List_Width;
        public int _dgvBind_Detail_Width;

        DataTable table = new DataTable();
        DataColumn column;
        DataRow row;
        DataView view;

        public DateTime _dt;
        public DateTime _dateFilter;
        Timer timer = new Timer();

        public string _msgText = "";
        public int _msgType = 0;

        public frmSub04ScanOutMaterial()
        {
            InitializeComponent();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "IDx";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Pack";
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
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Unit";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Qty";
            table.Columns.Add(column);
        }

        private void frmSub04ScanOutMaterial_Load(object sender, EventArgs e)
        {
            _dgvOut_Request_Width = cls.fnGetDataGridWidth(dgvOut_Request);
            _dgvOut_Detail_Width = cls.fnGetDataGridWidth(dgvOut_Detail);
            _dgvOut_List_Width = cls.fnGetDataGridWidth(dgvOut_List);

            _dgvAprv_List_Width = cls.fnGetDataGridWidth(dgvAprv_List);
            _dgvAprv_Detail_Width = cls.fnGetDataGridWidth(dgvAprv_Detail);
            _dgvUnAprv_List_Width = cls.fnGetDataGridWidth(dgvUnAprv_List);

            _dgvBind_List_Width = cls.fnGetDataGridWidth(dgvOut_Bind_List);
            _dgvBind_Detail_Width = cls.fnGetDataGridWidth(dgvOut_Bind_Detail);

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

            initOut_Request();
            initBind();
        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initOut();
                    break;
                case 1:
                    MessageBox.Show("Vui lòng sử dụng chức năng 'Phiếu chưa duyệt' để theo dõi.");
                    tabControl1.SelectedIndex = 2;
                    //initAprv();
                    break;
                case 2:
                    initUnAprv();
                    break;
            }
            tssMessage.Text = "";
            //if(tabControl1.TabPages[2].Text=="")
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 5000;
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
            timer.Stop();
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nhậpKhoVậtTưToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frm02ScanIn == null)
            {
                frm02ScanIn= new frmSub02ScanInMaterial();
                frm02ScanIn.FormClosed += frm02ScanIn_FormClosed;
            }
            frm02ScanIn.WindowState = FormWindowState.Maximized;
            frm02ScanIn.Show(this);
            Close();
        }

        private void frm02ScanIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm02ScanIn = null;
            Show();
        }

        private void frmSub04ScanOutMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Có chắc là bạn muốn thoát?",
            //                   "Hệ thống quản lý hàng Spare Part",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Information) == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }


        #region SCAN OUT



        public void initOut()
        {
            initOut_Request();
        }

        public void initOut_Request()
        {
            string sql = "V2o1_BASE_SubMaterial04_Init_ScanOut_Request_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvOut_Request_Width = cls.fnGetDataGridWidth(dgvOut_Request);
            dgvOut_Request.DataSource = dt;

            //dgvOut_Request.Columns[0].Width = 20 * _dgvOut_Request_Width / 100;    // idx
            dgvOut_Request.Columns[1].Width = 50 * _dgvOut_Request_Width / 100;    // rsbmCode
            dgvOut_Request.Columns[2].Width = 30 * _dgvOut_Request_Width / 100;    // rsbmDate
            dgvOut_Request.Columns[3].Width = 20 * _dgvOut_Request_Width / 100;    // sum(matQty)
            //dgvOut_Request.Columns[4].Width = 30 * _dgvOut_Request_Width / 100;    // rsbmReceiver
            //dgvOut_Request.Columns[5].Width = 20 * _dgvOut_Request_Width / 100;    // rsbmPurpose
            //dgvOut_Request.Columns[6].Width = 20 * _dgvOut_Request_Width / 100;    // rsbmPriority
            //dgvOut_Request.Columns[7].Width = 20 * _dgvOut_Request_Width / 100;    // rsbmReason
            //dgvOut_Request.Columns[8].Width = 20 * _dgvOut_Request_Width / 100;    // rsbmAdd
            //dgvOut_Request.Columns[9].Width = 20 * _dgvOut_Request_Width / 100;    // mmtApprove
            //dgvOut_Request.Columns[10].Width = 20 * _dgvOut_Request_Width / 100;    // mmtApproveDate
            //dgvOut_Request.Columns[11].Width = 20 * _dgvOut_Request_Width / 100;    // mmtResponse
            //dgvOut_Request.Columns[12].Width = 20 * _dgvOut_Request_Width / 100;    // mmtDateHandOver

            dgvOut_Request.Columns[0].Visible = false;
            dgvOut_Request.Columns[1].Visible = true;
            dgvOut_Request.Columns[2].Visible = true;
            dgvOut_Request.Columns[3].Visible = true;
            dgvOut_Request.Columns[4].Visible = false;
            dgvOut_Request.Columns[5].Visible = false;
            dgvOut_Request.Columns[6].Visible = false;
            dgvOut_Request.Columns[7].Visible = false;
            dgvOut_Request.Columns[8].Visible = false;
            dgvOut_Request.Columns[9].Visible = false;
            dgvOut_Request.Columns[10].Visible = false;
            dgvOut_Request.Columns[11].Visible = false;
            dgvOut_Request.Columns[12].Visible = false;

            dgvOut_Request.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgvOut_Request, 11, 30);

        }

        public void initOut_Detail()
        {
            string outIDx = _outIDx;
            string sql = "V2o1_BASE_SubMaterial04_Init_ScanOut_Detail_SelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@rsbmIDx";
            sParams[0].Value = outIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvOut_Detail_Width = cls.fnGetDataGridWidth(dgvOut_Detail);
            dgvOut_Detail.DataSource = dt;

            //dgvOut_Detail.Columns[0].Width = 25 * _dgvOut_Detail_Width / 100;    // idx
            //dgvOut_Detail.Columns[1].Width = 25 * _dgvOut_Detail_Width / 100;    // matIDx
            dgvOut_Detail.Columns[2].Width = 70 * _dgvOut_Detail_Width / 100;    // matName
            //dgvOut_Detail.Columns[3].Width = 30 * _dgvOut_Detail_Width / 100;    // matCode
            dgvOut_Detail.Columns[4].Width = 10 * _dgvOut_Detail_Width / 100;    // matUnit
            dgvOut_Detail.Columns[5].Width = 20 * _dgvOut_Detail_Width / 100;    // matQty
            //dgvOut_Detail.Columns[6].Width = 15 * _dgvOut_Detail_Width / 100;    // sum(matQty)
            //dgvOut_Detail.Columns[7].Width = 15 * _dgvOut_Detail_Width / 100;    // ImageStatus
            //dgvOut_Detail.Columns[8].Width = 15 * _dgvOut_Detail_Width / 100;    // ImageView

            dgvOut_Detail.Columns[0].Visible = false;
            dgvOut_Detail.Columns[1].Visible = false;
            dgvOut_Detail.Columns[2].Visible = true;
            dgvOut_Detail.Columns[3].Visible = false;
            dgvOut_Detail.Columns[4].Visible = true;
            dgvOut_Detail.Columns[5].Visible = true;
            dgvOut_Detail.Columns[6].Visible = false;
            dgvOut_Detail.Columns[7].Visible = false;
            dgvOut_Detail.Columns[8].Visible = false;

            cls.fnFormatDatagridview(dgvOut_Detail, 11, 30);
            dgvOut_Detail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void dgvOut_Request_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOut_Request_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    dgvOut_Detail.DataSource = "";
                    dgvOut_Detail.Refresh();

                    _outMatIDx = "";
                    _outMatName = "";
                    _outMatCode = "";
                    _outMatUnit = "";
                    _outMatQty = "";
                    _outMatHave = "";

                    txtOut_Packing.Text = "";
                    txtOut_Packing.Enabled = false;
                    lblOut_Need.Text = "0";
                    lblOut_Need.Enabled = false;
                    lblOut_Have.Text = "0";
                    lblOut_Have.Enabled = false;
                    txtOut_Scan.Text = "0";
                    txtOut_Scan.Enabled = false;
                    lblOut_Remain.Text = "0";
                    lblOut_Remain.Enabled = false;

                    table.Rows.Clear();
                    dgvOut_List.DataSource = "";
                    dgvOut_List.Refresh();

                    //lblOut_Request_Purpose.Text = "[N/A]";
                    //lblOut_Request_Purpose.Enabled = false;
                    //lblOut_Reason.Text = "";
                    //lblOut_Reason.Enabled = false;



                    cls.fnDatagridClickCell(dgvOut_Request, e);
                    DataGridViewRow row = new DataGridViewRow();
                    row = dgvOut_Request.Rows[e.RowIndex];

                    string idx = row.Cells[0].Value.ToString();
                    string code = row.Cells[1].Value.ToString();
                    string date = row.Cells[2].Value.ToString();
                    string total = row.Cells[3].Value.ToString();
                    string maker = row.Cells[4].Value.ToString();
                    string purpose = row.Cells[5].Value.ToString();
                    string priority = row.Cells[6].Value.ToString();
                    string reason = row.Cells[7].Value.ToString();
                    string add = row.Cells[8].Value.ToString();
                    string approve = row.Cells[9].Value.ToString();
                    string approveDate = row.Cells[10].Value.ToString();
                    string response = row.Cells[11].Value.ToString();
                    string handoverDate = row.Cells[12].Value.ToString();

                    _outIDx = idx;
                    _outCode = code;
                    _outDate = date;
                    _outTotal = total;
                    _outMaker = maker;
                    _outPurpose = purpose;
                    _outPriority = priority;
                    _outReason = reason;
                    _outAdd = add;
                    _outApprove = approve;
                    _outApproveDate = approveDate;
                    _outResponse = response;
                    _outHandoverDate = handoverDate;

                    _totalOrder = total;

                    initOut_Detail();

                    txtOut_Packing.Text = "";
                    txtOut_Packing.Enabled = false;
                    lblOut_Pack_InDate.Text = "N/A";
                    lblOut_Pack_InDate.Enabled = false;
                    lblOut_Need.Text = "0";
                    lblOut_Need.Enabled = false;
                    lblOut_Have.Text = "0";
                    lblOut_Have.Enabled = false;
                    txtOut_Scan.Text = "0";
                    txtOut_Scan.Enabled = false;
                    lblOut_Remain.Text = "0";
                    lblOut_Remain.Enabled = false;


                    switch (priority)
                    {
                        case "1":
                            lblOut_Request_Purpose.Text = "Sửa chữa";
                            lblOut_Request_Purpose.ForeColor = Color.White;
                            lblOut_Request_Purpose.BackColor = Color.Red;
                            break;
                        case "2":
                            lblOut_Request_Purpose.Text = "Bảo trì";
                            lblOut_Request_Purpose.ForeColor = Color.White;
                            lblOut_Request_Purpose.BackColor = Color.Green;
                            break;
                        case "3":
                            lblOut_Request_Purpose.Text = "Cải tiến";
                            lblOut_Request_Purpose.ForeColor = Color.White;
                            lblOut_Request_Purpose.BackColor = Color.Blue;
                            break;
                        case "4":
                            lblOut_Request_Purpose.Text = "Hàng ngày";
                            lblOut_Request_Purpose.ForeColor = Color.White;
                            lblOut_Request_Purpose.BackColor = Color.Chocolate;
                            break;
                    }
                    lblOut_Request_Purpose.Enabled = true;
                    lblOut_Reason.Text = reason;
                    lblOut_Reason.Enabled = true;

                    btnOut_Save.Enabled = false;
                    btnOut_Done.Enabled = true;
                }
            }
        }

        private void dgvOut_Request_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvOut_Detail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOut_Detail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvOut_Detail, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvOut_Detail.Rows[e.RowIndex];

                string matIDx = row.Cells[1].Value.ToString();
                string matName = row.Cells[2].Value.ToString();
                string matCode = row.Cells[3].Value.ToString();
                string matUnit = row.Cells[4].Value.ToString();
                string matQty = row.Cells[5].Value.ToString();
                //string matHave = row.Cells[6].Value.ToString();
                string imagestatus = row.Cells[7].Value.ToString().ToLower();

                _outMatIDx = matIDx;
                _outMatName = matName;
                _outMatCode = matCode;
                _outMatUnit = matUnit;
                _outMatQty = matQty;
                //_outMatHave = matHave;

                if (dgvOut_List.Rows.Count > 0)
                {
                    int need = (matQty != "" && matQty != null) ? Convert.ToInt32(matQty) : 0;

                    int sum = 0;
                    for (int i = 0; i < dgvOut_List.Rows.Count; ++i)
                    {
                        if (dgvOut_List.Rows[i].Cells[0].Value.ToString() == matIDx)
                        {
                            sum += Convert.ToInt32(dgvOut_List.Rows[i].Cells[5].Value);
                        }
                    }
                    matQty = (need - sum).ToString();

                }

                if (imagestatus == "true")
                {
                    _ms = new MemoryStream((byte[])row.Cells[8].Value);
                    lnkOut_ViewImage.Enabled = true;
                }
                else
                {
                    _ms = null;
                    lnkOut_ViewImage.Enabled = false;
                }


                string needQty = lblOut_Need.Text;
                int _needQty = (needQty != "" && needQty != null) ? Convert.ToInt32(needQty) : 0;
                if (_needQty > 0)
                {
                    txtOut_Packing.Text = "";
                    txtOut_Packing.Enabled = true;
                    txtOut_Packing.Focus();
                    //lblOut_Have.Text = matHave;
                    //lblOut_Have.Enabled = true;
                    lblOut_Pack_InDate.Text = "N/A";
                    lblOut_Pack_InDate.Enabled = false;

                    lblOut_Need.Text = matQty;
                    lblOut_Need.Enabled = true;
                    lblOut_Have.Text = "0";
                    lblOut_Have.Enabled = false;
                    txtOut_Scan.Text = "0";
                    txtOut_Scan.Enabled = false;
                    lblOut_Remain.Text = "0";
                    lblOut_Remain.Enabled = false;
                }
                else
                {
                    txtOut_Packing.Text = "Đã đủ số lượng xuất";
                    txtOut_Packing.Enabled = false;
                    //lblOut_Have.Text = matHave;
                    //lblOut_Have.Enabled = true;
                    lblOut_Pack_InDate.Text = "N/A";
                    lblOut_Pack_InDate.Enabled = false;

                    lblOut_Need.Text = matQty;
                    lblOut_Need.Enabled = false;
                    lblOut_Have.Text = "0";
                    lblOut_Have.Enabled = false;
                    txtOut_Scan.Text = "0";
                    txtOut_Scan.Enabled = false;
                    lblOut_Remain.Text = "0";
                    lblOut_Remain.Enabled = false;
                }
            }
        }

        private void dgvOut_Detail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtOut_Packing_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string packing = txtOut_Packing.Text.Trim();
                    if (packing != "" && packing != null)
                    {
                        string packKind = packing.Substring(0, 3);
                        string packType = packing.Substring(4, 3);
                        string packCode = packing.Substring(8);

                        if (packKind.ToUpper() == "MMT")
                        {
                            if (packType.ToUpper() == "SBM")
                            {
                                if (packCode != "" && packCode != null)
                                {
                                    string sql = "V2o1_BASE_SubMaterial04_Init_ScanOut_GetPacking_SelItem_Addnew";
                                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                                    sParams[0] = new SqlParameter();
                                    sParams[0].SqlDbType = SqlDbType.VarChar;
                                    sParams[0].ParameterName = "@inCode";
                                    sParams[0].Value = packing;

                                    DataSet ds = new DataSet();
                                    ds = cls.ExecuteDataSet(sql, sParams);
                                    if (ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            string matCode = ds.Tables[0].Rows[0][2].ToString();
                                            string matInDate = ds.Tables[0].Rows[0][3].ToString();
                                            string matHave = ds.Tables[0].Rows[0][5].ToString();
                                            int _matNeed = 0, _matHave = 0, _matScan = 0, _matRemain = 0;
                                            if (matCode == _outMatCode)
                                            {
                                                _outMatHave = matHave;

                                                _matNeed = (_outMatQty != "" && _outMatQty != null) ? Convert.ToInt32(_outMatQty) : 0;
                                                _matHave = (matHave != "" && matHave != null) ? Convert.ToInt32(matHave) : 0;

                                                lblOut_Pack_InDate.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(matInDate));
                                                lblOut_Pack_InDate.Enabled = true;
                                                lblOut_Have.Text = matHave;
                                                lblOut_Have.Enabled = true;

                                                txtOut_Scan.Text = (_matNeed <= _matHave) ? _matNeed.ToString() : _matHave.ToString();
                                                txtOut_Scan.Enabled = true;
                                                txtOut_Scan.Focus();
                                                lblOut_Remain.Text = (_matNeed <= _matHave) ? (_matHave - _matNeed).ToString() : "0";
                                                lblOut_Remain.Enabled = true;

                                                lnkOut_Add.Enabled = true;
                                            }
                                            else
                                            {
                                                lblOut_Pack_InDate.Text = "N/A";
                                                lblOut_Have.Text = "0";
                                                lblOut_Have.Enabled = false;
                                                txtOut_Scan.Text = "0";
                                                txtOut_Scan.Enabled = false;
                                                lblOut_Remain.Text = "0";
                                                lblOut_Remain.Enabled = false;

                                                lnkOut_Add.Enabled = false;

                                                txtOut_Packing.Focus();
                                                txtOut_Packing.Text = "";

                                                _msgText = "Không đúng loại vật tư yêu cầu. Vui lòng kiểm tra lại.";
                                                _msgType = 2;
                                            }
                                        }
                                        else
                                        {
                                            lnkOut_Add.Enabled = false;
                                            txtOut_Packing.Focus();
                                            txtOut_Packing.Text = "";

                                            _msgText = "Không tìm thấy mã packing tương ứng. Vui lòng kiểm tra lại.";
                                            _msgType = 2;
                                        }
                                    }
                                    else
                                    {
                                        lnkOut_Add.Enabled = false;
                                        txtOut_Packing.Focus();
                                        txtOut_Packing.Text = "";

                                        _msgText = "Không tìm thấy mã packing tương ứng. Vui lòng kiểm tra lại.";
                                        _msgType = 2;
                                    }
                                }
                                else
                                {
                                    lnkOut_Add.Enabled = false;
                                    txtOut_Packing.Focus();
                                    txtOut_Packing.Text = "";

                                    _msgText = "Mã tem không hợp lệ, vui lòng kiểm tra lại.";
                                    _msgType = 2;
                                }
                            }
                            else
                            {
                                lnkOut_Add.Enabled = false;
                                txtOut_Packing.Focus();
                                txtOut_Packing.Text = "";

                                _msgText = "Tem không hợp lệ, bắt buộc phải chứa cụm 'SBM'. Vui lòng kiểm tra lại.";
                                _msgType = 2;
                            }
                        }
                        else
                        {
                            lnkOut_Add.Enabled = false;
                            txtOut_Packing.Focus();
                            txtOut_Packing.Text = "";

                            _msgText = "Tem không hợp lệ, luôn phải bắt đầu bằng 'MMT'. Vui lòng kiểm tra lại.";
                            _msgType = 2;
                        }
                    }
                }
            }
            catch
            {
                //lnkOut_Add.Enabled = false;
                txtOut_Packing.Focus();
                txtOut_Packing.Text = "";

                _msgText = "Tem không hợp lệ (độ dài chuỗi, định dạng). Vui lòng kiểm tra lại.";
                _msgType = 2;
            }
            finally
            {
                if (dgvOut_List.Rows.Count > 0)
                {
                    string packingList = "";
                    string packingCurr = txtOut_Packing.Text.Trim();
                    string _have = _outMatHave;

                    int have = (_have != "" && _have != null) ? Convert.ToInt32(_have) : 0;
                    //int sum = 0;
                    //for (int i = 0; i < dgvOut_List.Rows.Count; ++i)
                    //{
                    //    packingList = dgvOut_List.Rows[i].Cells[1].Value.ToString();
                    //    if (packingCurr == packingList)
                    //    {
                    //        if (dgvOut_List.Rows[i].Cells[0].Value.ToString() == _outMatIDx)
                    //        {
                    //            sum += Convert.ToInt32(dgvOut_List.Rows[i].Cells[5].Value);
                    //        }
                    //        else
                    //        {
                    //            sum = 0;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        sum = 0;
                    //    }
                    //    //if (dgvOut_List.Rows[i].Cells[0].Value.ToString() == _outMatIDx)
                    //    //{
                    //    //    sum += Convert.ToInt32(dgvOut_List.Rows[i].Cells[5].Value);
                    //    //}
                    //}
                    //_have = (have - sum).ToString();
                    lblOut_Have.Text = _have;

                    string matNeed = lblOut_Need.Text;
                    string matHave = lblOut_Have.Text;
                    string matScan = txtOut_Scan.Text.Trim();
                    string matRemain = lblOut_Remain.Text;

                    int _matNeed = 0, _matHave = 0, _matScan = 0, _matRemain = 0;
                    _matNeed = (matNeed != "" && matNeed != null) ? Convert.ToInt32(matNeed) : 0;
                    _matHave = (matHave != "" && matHave != null) ? Convert.ToInt32(matHave) : 0;
                    _matScan = (matScan != "" && matScan != null) ? Convert.ToInt32(matScan) : 0;
                    _matRemain = (matRemain != "" && matRemain != null) ? Convert.ToInt32(matRemain) : 0;

                    if (_matScan <= _matHave)
                    {
                        if (_matScan <= _matNeed)
                        {
                            lblOut_Remain.Text = (_matHave - _matScan).ToString();
                        }
                        else
                        {
                            txtOut_Scan.Text = _matNeed.ToString();
                        }
                    }
                    else
                    {
                        txtOut_Scan.Text = _matHave.ToString();
                    }
                }

                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }

        private void txtOut_Scan_TextChanged(object sender, EventArgs e)
        {
            //string matNeed = lblOut_Need.Text;
            //string matHave = lblOut_Have.Text;
            //string matScan = txtOut_Scan.Text.Trim();
            //string matRemain = lblOut_Remain.Text;

            //int _matNeed = 0, _matHave = 0, _matScan = 0, _matRemain = 0;
            //_matNeed = (matNeed != "" && matNeed != null) ? Convert.ToInt32(matNeed) : 0;
            //_matHave = (matHave != "" && matHave != null) ? Convert.ToInt32(matHave) : 0;
            //_matScan = (matScan != "" && matScan != null) ? Convert.ToInt32(matScan) : 0;
            //_matRemain = (matRemain != "" && matRemain != null) ? Convert.ToInt32(matRemain) : 0;

            //if (_matScan <= _matHave)
            //{
            //    if (_matScan <= _matNeed)
            //    {
            //        lblOut_Remain.Text = (_matHave - _matScan).ToString();
            //    }
            //    else
            //    {
            //        txtOut_Scan.Text = _matNeed.ToString();
            //    }
            //}
            //else
            //{
            //    txtOut_Scan.Text = _matHave.ToString();
            //}
        }

        private void txtOut_Scan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void lblOut_Need_TextChanged(object sender, EventArgs e)
        {
            string need = lblOut_Need.Text;
            int _need = (need != "" && need != null) ? Convert.ToInt32(need) : 0;
            if (_need == 0)
            {
                txtOut_Packing.Text = "Đã đủ số lượng xuất";
                txtOut_Packing.Enabled = false;
            }
            else
            {
                txtOut_Packing.Text = "";
                txtOut_Packing.Enabled = true;
                txtOut_Packing.Focus();
            }
        }

        private void lnkOut_Add_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pack = txtOut_Packing.Text.Trim();
            string scan = txtOut_Scan.Text.Trim();
            int _scan = (scan != "" && scan != null) ? Convert.ToInt32(scan) : 0;
            if (_scan > 0)
            {
                table.Rows.Add(_outMatIDx, pack, _outMatName, _outMatCode, _outMatUnit, _scan);
                view = new DataView(table);

                _dgvOut_List_Width = cls.fnGetDataGridWidth(dgvOut_List);
                dgvOut_List.DataSource = view;
                dgvOut_List.Refresh();

                //dgvOut_List.Columns[0].Width = 20 * _dgvOut_List_Width / 100;    // matId
                dgvOut_List.Columns[1].Width = 70 * _dgvOut_List_Width / 100;    // pack
                //dgvOut_List.Columns[2].Width = 20 * _dgvOut_List_Width / 100;    // matName
                //dgvOut_List.Columns[3].Width = 15 * _dgvOut_List_Width / 100;    // matCode
                dgvOut_List.Columns[4].Width = 10 * _dgvOut_List_Width / 100;    // matQty
                dgvOut_List.Columns[5].Width = 20 * _dgvOut_List_Width / 100;    // matUnit

                dgvOut_List.Columns[0].Visible = false;
                dgvOut_List.Columns[1].Visible = true;
                dgvOut_List.Columns[2].Visible = false;
                dgvOut_List.Columns[3].Visible = false;
                dgvOut_List.Columns[4].Visible = true;
                dgvOut_List.Columns[5].Visible = true;

                cls.fnFormatDatagridview(dgvOut_List, 10, 30);
                //dgvOut_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                if (dgvOut_List.Rows.Count > 0)
                {
                    string matQty = _outMatQty;
                    string _have = _outMatHave;

                    int have = (_have != "" && _have != null) ? Convert.ToInt32(_have) : 0;
                    int need = (matQty != "" && matQty != null) ? Convert.ToInt32(matQty) : 0;
                    int sum = 0;
                    int sumTotal = 0;
                    for (int i = 0; i < dgvOut_List.Rows.Count; ++i)
                    {
                        string lstIDx = dgvOut_List.Rows[i].Cells[0].Value.ToString();
                        string lstPacking = dgvOut_List.Rows[i].Cells[1].Value.ToString();
                        string scnPacking = txtOut_Packing.Text.Trim();
                        if (lstIDx == _outMatIDx && lstPacking == scnPacking)
                        {
                            sum += Convert.ToInt32(dgvOut_List.Rows[i].Cells[5].Value);
                        }
                        sumTotal += Convert.ToInt32(dgvOut_List.Rows[i].Cells[5].Value);
                    }
                    _totalScan = sumTotal.ToString();

                    _have = (have - sum).ToString();
                    matQty = (need - sum).ToString();

                    lblOut_Have.Text = _have;
                    lblOut_Need.Text = matQty;
                }

                lblOut_Have.Text = "0";
                lblOut_Have.Enabled = false;
                txtOut_Scan.Text = "0";
                txtOut_Scan.Enabled = false;
                lblOut_Remain.Text = "0";
                lblOut_Remain.Enabled = false;

                lnkOut_Add.Enabled = false;
                lnkOut_Del.Enabled = false;
            }
            else
            {
                _msgText = "Không thể xuất vật tư với số lượng là 0.";
                _msgType = 2;
            }

            btnOut_Save.Enabled = (_totalOrder == _totalScan) ? true : false;
            cls.fnMessage(tssMessage, _msgText, _msgType);
        }

        private void lnkOut_Del_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvOut_List.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dgvOut_List.Rows.Remove(row);
                }

                dgvOut_List.ClearSelection();
                dgvOut_List.Refresh();

                if (dgvOut_List.Rows.Count > 0)
                {
                    int totalOrder = Convert.ToInt32(_totalOrder);
                    int totalScan = Convert.ToInt32(_totalScan);
                    int qtyScan = Convert.ToInt32(_lstMatQty);
                    _totalScan = (totalScan - qtyScan).ToString();
                }
                else
                {
                    _totalScan = "0";
                }

                _lstMatIDx = "";
                _lstPacking = "";
                _lstMatName = "";
                _lstMatCode = "";
                _lstMatUnit = "";
                _lstMatQty = "";

                dgvOut_Detail.ClearSelection();
                _outMatIDx = "";
                _outMatName = "";
                _outMatCode = "";
                _outMatUnit = "";
                _outMatQty = "";

                txtOut_Packing.Text = "";
                txtOut_Packing.Enabled = false;
                lblOut_Pack_InDate.Text = "N/A";
                lblOut_Pack_InDate.Enabled = false;

                lblOut_Need.Text = "0";
                lblOut_Need.Enabled = false;
                lblOut_Have.Text = "0";
                lblOut_Have.Enabled = false;
                txtOut_Scan.Text = "0";
                txtOut_Scan.Enabled = false;
                lblOut_Remain.Text = "0";
                lblOut_Remain.Enabled = false;

                lnkOut_Add.Enabled = false;
                lnkOut_Del.Enabled = false;

                btnOut_Save.Enabled = false;
                btnOut_Done.Enabled = true;
            }
        }

        private void dgvOut_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOut_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvOut_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvOut_List.Rows[e.RowIndex];

                _lstMatIDx = row.Cells[0].Value.ToString();
                _lstPacking = row.Cells[1].Value.ToString();
                _lstMatName = row.Cells[2].Value.ToString();
                _lstMatCode = row.Cells[3].Value.ToString();
                _lstMatUnit = row.Cells[4].Value.ToString();
                _lstMatQty = row.Cells[5].Value.ToString();

                lnkOut_Add.Enabled = false;
                lnkOut_Del.Enabled = (dgvOut_List.Rows.Count > 0) ? true : false;
            }
        }

        private void dgvOut_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnOut_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string orderIDx = _outIDx;
                string orderCode = _outCode;
                string orderDate = _outDate;
                string orderTotal = _outTotal;
                string orderMaker = _outMaker;
                string orderPurpose = _outPurpose;
                string orderPriority = _outPriority;
                string orderReason = _outReason;
                string orderAdded = _outAdd;
                string orderApproved = _outApprove;
                string orderApprovedDate = _outApproveDate;
                string orderResponse = _outResponse;
                string orderHandoverDate = _outHandoverDate;

                string matIDx = _outMatIDx;
                string matName = _outMatName;
                string matCode = _outMatCode;
                string matUnit = _outMatUnit;
                string matQty = _outMatQty;

                //string msg = "";
                //msg += "orderIDx: " + orderIDx + "\r\n";
                //msg += "orderCode: " + orderCode + "\r\n";
                //msg += "orderDate: " + orderDate + "\r\n";
                //msg += "orderTotal: " + orderTotal + "\r\n";
                //msg += "orderMaker: " + orderMaker + "\r\n";
                //msg += "orderPurpose: " + orderPurpose + "\r\n";
                //msg += "orderPriority: " + orderPriority + "\r\n";
                //msg += "orderReason: " + orderReason + "\r\n";
                //msg += "orderAdded: " + orderAdded + "\r\n";
                //msg += "orderApproved: " + orderApproved + "\r\n";
                //msg += "orderApprovedDate: " + orderApprovedDate + "\r\n";
                //msg += "orderResponse: " + orderResponse + "\r\n";
                //msg += "orderHandoverDate: " + orderHandoverDate + "\r\n";
                //msg += "----------------------------------------: \r\n";
                //msg += "matIDx: " + matIDx + "\r\n";
                //msg += "matName: " + matName + "\r\n";
                //msg += "matCode: " + matCode + "\r\n";
                //msg += "matUnit: " + matUnit + "\r\n";
                //msg += "matQty: " + matQty + "\r\n";
                //msg += "----------------------------------------: \r\n";


                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    //MessageBox.Show(msg);
                    string idx = "", pack = "", name = "", code = "", unit = "", qty = "";
                    foreach (DataGridViewRow row in dgvOut_List.Rows)
                    {
                        idx = row.Cells[0].Value.ToString();
                        pack = row.Cells[1].Value.ToString();
                        name = row.Cells[2].Value.ToString();
                        code = row.Cells[3].Value.ToString();
                        unit = row.Cells[4].Value.ToString();
                        qty = row.Cells[5].Value.ToString();

                        //msg += "idx: " + idx + "\r\n";
                        //msg += "pack: " + pack + "\r\n";
                        //msg += "name " + name + "\r\n";
                        //msg += "code: " + code + "\r\n";
                        //msg += "unit: " + unit + "\r\n";
                        //msg += "qty: " + qty + "\r\n";
                        //msg += "----------------------------------------: \r\n";

                        string sql = "V2o1_BASE_SubMaterial04_Init_HandOver_List_AddItem_Addnew";

                        SqlParameter[] sParams = new SqlParameter[7]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@orderIDx";
                        sParams[0].Value = orderIDx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.VarChar;
                        sParams[1].ParameterName = "@packingCode";
                        sParams[1].Value = pack;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Int;
                        sParams[2].ParameterName = "@matIDx";
                        sParams[2].Value = idx;

                        sParams[3] = new SqlParameter();
                        sParams[3].SqlDbType = SqlDbType.NVarChar;
                        sParams[3].ParameterName = "@matName";
                        sParams[3].Value = name;

                        sParams[4] = new SqlParameter();
                        sParams[4].SqlDbType = SqlDbType.VarChar;
                        sParams[4].ParameterName = "@matCode";
                        sParams[4].Value = code;

                        sParams[5] = new SqlParameter();
                        sParams[5].SqlDbType = SqlDbType.VarChar;
                        sParams[5].ParameterName = "@matUnit";
                        sParams[5].Value = unit;

                        sParams[6] = new SqlParameter();
                        sParams[6].SqlDbType = SqlDbType.Int;
                        sParams[6].ParameterName = "@matQty";
                        sParams[6].Value = qty;

                        cls.fnUpdDel(sql, sParams);
                    }
                    //MessageBox.Show(msg);
                    _msgText = "Xuất kho cho phiếu " + orderCode + " thành công.";
                    _msgType = 1;
                }
            }
            catch(SqlException sqlEx)
            {
                _msgText = "Có lỗi dữ liệu phát sinh.";
                _msgType = 3;
                MessageBox.Show(sqlEx.ToString());
            }
            catch(Exception ex)
            {
                _msgText = "Có lỗi phát sinh.";
                _msgType = 2;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cls.fnMessage(tssMessage, _msgText, _msgType);
                fnOut_Done();
            }

        }

        private void lnkOut_ViewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSub04ScanOutMaterial_ViewImage frm04 = new frmSub04ScanOutMaterial_ViewImage(_ms);
            frm04.ShowDialog();
        }

        public void fnOut_Done()
        {
            _outIDx = "";
            _outCode = "";
            _outDate = "";
            _outTotal = "";
            _outMaker = "";
            _outPurpose = "";

            _outPriority = "";
            _outReason = "";
            _outAdd = "";

            _outApprove = "";
            _outApproveDate = "";
            _outResponse = "";
            _outHandoverDate = "";

            _outMatIDx = "";
            _outMatName = "";
            _outMatCode = "";
            _outMatUnit = "";
            _outMatQty = "";
            _outMatHave = "";

            _lstMatIDx = "";
            _lstPacking = "";
            _lstMatName = "";
            _lstMatCode = "";
            _lstMatUnit = "";
            _lstMatQty = "";

            _ms = null;

            dgvOut_Request.ClearSelection();
            dgvOut_Detail.DataSource = "";
            dgvOut_Detail.Refresh();
            lnkOut_ViewImage.Enabled = false;
            txtOut_Packing.Text = "";
            txtOut_Packing.Enabled = false;
            lblOut_Need.Text = "0";
            lblOut_Need.Enabled = false;
            lblOut_Have.Text = "0";
            lblOut_Have.Enabled = false;
            lblOut_Pack_InDate.Text = "N/A";
            lblOut_Pack_InDate.Enabled = false;
            txtOut_Scan.Text = "0";
            txtOut_Scan.Enabled = false;
            lblOut_Remain.Text = "0";
            lblOut_Remain.Enabled = false;
            lnkOut_Add.Enabled = false;
            lnkOut_Del.Enabled = false;
            dgvOut_List.DataSource = "";
            dgvOut_List.Refresh();
            lblOut_Request_Purpose.Text = "N/A";
            lblOut_Request_Purpose.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblOut_Reason.Text = "";
            lblOut_Reason.Enabled = false;

            btnOut_Save.Enabled = false;
            btnOut_Done.Enabled = false;
        }


        #endregion



        #region APPROVAL


        public void initAprv()
        {
            initAprv_List();
            initAprv_Approve();
            lblAprv_Date.Text = "N/A";
            lblAprv_Maker.Text = "N/A";
            lblAprv_Total.Text = "0";
            lblAprv_Purpose.Text = "";
            lblAprv_Reason.Text = "";

            lblAprv_Date.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Maker.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Total.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Purpose.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Reason.BackColor = Color.FromKnownColor(KnownColor.Control);

        }

        public void initAprv_List()
        {
            string sql = "V2o1_BASE_SubMaterial04_Init_Approval_List_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvAprv_List_Width = cls.fnGetDataGridWidth(dgvAprv_List);
            dgvAprv_List.DataSource = dt;

            //dgvAprv_List.Columns[0].Width = 20 * _dgvAprv_List_Width / 100;    // idx
            dgvAprv_List.Columns[1].Width = 50 * _dgvAprv_List_Width / 100;    // rsbmCode
            dgvAprv_List.Columns[2].Width = 30 * _dgvAprv_List_Width / 100;    // rsbmDate
            dgvAprv_List.Columns[3].Width = 20 * _dgvAprv_List_Width / 100;    // sum(matQty)
            //dgvAprv_List.Columns[4].Width = 30 * _dgvAprv_List_Width / 100;    // rsbmReceiver
            //dgvAprv_List.Columns[5].Width = 20 * _dgvAprv_List_Width / 100;    // rsbmPurpose
            //dgvAprv_List.Columns[6].Width = 20 * _dgvAprv_List_Width / 100;    // rsbmPriority
            //dgvAprv_List.Columns[7].Width = 20 * _dgvAprv_List_Width / 100;    // rsbmReason
            //dgvAprv_List.Columns[8].Width = 20 * _dgvAprv_List_Width / 100;    // rsbmAdd
            //dgvAprv_List.Columns[9].Width = 20 * _dgvAprv_List_Width / 100;    // mmtApprove
            //dgvAprv_List.Columns[10].Width = 20 * _dgvAprv_List_Width / 100;    // mmtApproveDate
            //dgvAprv_List.Columns[11].Width = 20 * _dgvAprv_List_Width / 100;    // mmtResponse
            //dgvAprv_List.Columns[12].Width = 20 * _dgvAprv_List_Width / 100;    // mmtDateHandOver

            dgvAprv_List.Columns[0].Visible = false;
            dgvAprv_List.Columns[1].Visible = true;
            dgvAprv_List.Columns[2].Visible = true;
            dgvAprv_List.Columns[3].Visible = true;
            dgvAprv_List.Columns[4].Visible = false;
            dgvAprv_List.Columns[5].Visible = false;
            dgvAprv_List.Columns[6].Visible = false;
            dgvAprv_List.Columns[7].Visible = false;
            dgvAprv_List.Columns[8].Visible = false;
            dgvAprv_List.Columns[9].Visible = false;
            dgvAprv_List.Columns[10].Visible = false;
            dgvAprv_List.Columns[11].Visible = false;
            dgvAprv_List.Columns[12].Visible = false;

            dgvAprv_List.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgvAprv_List, 11, 30);
        }

        public void initAprv_Detail()
        {
            string rsbmIDx = _idx;
            string sql = "V2o1_BASE_SubMaterial04_Init_Approval_Detail_SelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "rsbmIDx";
            sParams[0].Value = rsbmIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvAprv_Detail_Width = cls.fnGetDataGridWidth(dgvAprv_Detail);
            dgvAprv_Detail.DataSource = dt;

            //dgvAprv_Detail.Columns[0].Width = 25 * _dgvAprv_Detail_Width / 100;    // idx
            //dgvAprv_Detail.Columns[1].Width = 25 * _dgvAprv_Detail_Width / 100;    // matIDx
            dgvAprv_Detail.Columns[2].Width = 60 * _dgvAprv_Detail_Width / 100;    // matName
            //dgvAprv_Detail.Columns[3].Width = 30 * _dgvAprv_Detail_Width / 100;    // matCode
            dgvAprv_Detail.Columns[4].Width = 10 * _dgvAprv_Detail_Width / 100;    // matUnit
            dgvAprv_Detail.Columns[5].Width = 15 * _dgvAprv_Detail_Width / 100;    // matQty
            dgvAprv_Detail.Columns[6].Width = 15 * _dgvAprv_Detail_Width / 100;    // sum(matQty)

            dgvAprv_Detail.Columns[0].Visible = false;
            dgvAprv_Detail.Columns[1].Visible = false;
            dgvAprv_Detail.Columns[2].Visible = true;
            dgvAprv_Detail.Columns[3].Visible = false;
            dgvAprv_Detail.Columns[4].Visible = true;
            dgvAprv_Detail.Columns[5].Visible = true;
            dgvAprv_Detail.Columns[6].Visible = true;

            cls.fnFormatDatagridview(dgvAprv_Detail, 12, 30);
            dgvAprv_Detail.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        public void initAprv_Approve()
        {
            cbbAprv_Approve.Items.Clear();
            cbbAprv_Approve.Items.Add("Chấp nhận yêu cầu");
            cbbAprv_Approve.Items.Add("Lý do không hợp lý");
            cbbAprv_Approve.Items.Add("Vật tư không đủ xuất");
            cbbAprv_Approve.Items.Insert(0, "");
            cbbAprv_Approve.SelectedIndex = 0;
        }

        private void dgvAprv_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvAprv_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvAprv_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvAprv_List.Rows[e.RowIndex];

                string idx = row.Cells[0].Value.ToString();
                string code = row.Cells[1].Value.ToString();
                string date = row.Cells[2].Value.ToString();
                string total = row.Cells[3].Value.ToString();
                string maker = row.Cells[4].Value.ToString();
                string purpose = row.Cells[5].Value.ToString();
                string priority = row.Cells[6].Value.ToString();
                string reason = row.Cells[7].Value.ToString();
                string add = row.Cells[8].Value.ToString();
                string approve = row.Cells[9].Value.ToString();
                string approveDate = row.Cells[10].Value.ToString();
                string response = row.Cells[11].Value.ToString();
                string handoverDate = row.Cells[12].Value.ToString();

                _idx = idx;
                _code = code;
                _date = date;
                _total = total;
                _maker = maker;
                _purpose = purpose;
                _priority = priority;
                _reason = reason;
                _add = add;
                _approve = approve;
                _approveDate = approveDate;
                _response = response;
                _handoverDate = handoverDate;

                lblAprv_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(date));
                lblAprv_Maker.Text = maker;
                lblAprv_Total.Text = total;
                initAprv_Detail();
                dgvAprv_Detail.BackgroundColor = Color.White;
                lblAprv_Purpose.Text = purpose;
                switch (priority)
                {
                    case "1":
                        lblAprv_Purpose.Text = "Sửa chữa";
                        lblAprv_Purpose.BackColor = Color.Red;
                        break;
                    case "2":
                        lblAprv_Purpose.Text = "Bảo trì";
                        lblAprv_Purpose.BackColor = Color.Green;
                        break;
                    case "3":
                        lblAprv_Purpose.Text = "Cải tiến";
                        lblAprv_Purpose.BackColor = Color.Blue;
                        break;
                    case "4":
                        lblAprv_Purpose.Text = "Hàng ngày";
                        lblAprv_Purpose.BackColor = Color.Chocolate;
                        break;
                }
                lblAprv_Reason.Text = reason;

                cbbAprv_Approve.Enabled = true;
                btnAprv_Done.Enabled = true;
            }
        }

        private void dgvAprv_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvAprv_Detail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvAprv_Detail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dgvAprv_Detail.ClearSelection();
            }
        }

        private void dgvAprv_Detail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void cbbAprv_Approve_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int sel = cbbAprv_Approve.SelectedIndex;
            switch (sel)
            {
                case 0:
                    dtpAprv_HandoverDate.Enabled = false;
                    txtAprv_Response.Text = "";
                    txtAprv_Response.Enabled = false;
                    btnAprv_Save.Enabled = false;
                    break;
                case 1:
                    dtpAprv_HandoverDate.Enabled = true;
                    txtAprv_Response.Text = "Vui lòng liên hệ với phòng Vật tư để nhận hàng.";
                    txtAprv_Response.Enabled = true;
                    btnAprv_Save.Enabled = true;
                    break;
                case 2:
                    dtpAprv_HandoverDate.Enabled = false;
                    txtAprv_Response.Text = "Vui lòng tạo yêu cầu khác với lý do / số lượng hợp lý hơn.";
                    txtAprv_Response.Enabled = true;
                    btnAprv_Save.Enabled = true;
                    break;
                case 3:
                    dtpAprv_HandoverDate.Enabled = false;
                    txtAprv_Response.Text = "Vật tư hiện có không đủ cung cấp theo yêu cầu. Vui lòng tạo lại yêu cầu sau khoảng 1-2 ngày nữa.";
                    txtAprv_Response.Enabled = true;
                    btnAprv_Save.Enabled = true;
                    break;
                default:
                    dtpAprv_HandoverDate.Enabled = false;
                    txtAprv_Response.Text = "";
                    txtAprv_Response.Enabled = false;
                    btnAprv_Save.Enabled = false;
                    break;
            }
        }

        private void dtpAprv_HandoverDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime hoDate = dtpAprv_HandoverDate.Value;
            txtAprv_Response.Text = "Vui lòng nhận vật tư vào ngày " + String.Format("{0:dd/MM/yyyy}", hoDate);
        }

        private void btnAprv_Done_Click(object sender, EventArgs e)
        {
            fnAprv_Done();
        }

        public void fnAprv_Done()
        {
            _idx = "";
            _code = "";
            _date = "";
            _total = "";
            _maker = "";
            _purpose = "";
            _priority = "";
            _reason = "";
            _add = "";
            _approve = "";
            _approveDate = "";
            _response = "";
            _handoverDate = "";

            dgvAprv_List.ClearSelection();
            lblAprv_Date.Text = "N/A";
            lblAprv_Maker.Text = "N/A";
            lblAprv_Total.Text = "0";
            dgvAprv_Detail.DataSource = "";
            dgvAprv_Detail.Refresh();
            lblAprv_Purpose.Text = "";
            lblAprv_Reason.Text = "";

            lblAprv_Date.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Maker.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Total.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Purpose.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblAprv_Reason.BackColor = Color.FromKnownColor(KnownColor.Control);

            cbbAprv_Approve.SelectedIndex = 0;
            cbbAprv_Approve.Enabled = false;
            dtpAprv_HandoverDate.Value = DateTime.Now;
            dtpAprv_HandoverDate.Enabled = false;
            txtAprv_Response.Text = "";
            txtAprv_Response.Enabled = false;

            btnAprv_Save.Enabled = false;
            btnAprv_Done.Enabled = false;
        }

        private void btnAprv_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                fnAprv_Add();
            }
        }

        public void fnAprv_Add()
        {
            try
            {
                string aprvIDx = _idx;
                string approve = cbbAprv_Approve.SelectedIndex.ToString();
                DateTime hoDate = dtpAprv_HandoverDate.Value;
                string response = txtAprv_Response.Text.Trim();

                string sql = "V2o1_BASE_SubMaterial04_Init_Approval_Done_UpdItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@aprvIDx";
                sParams[0].Value = aprvIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.TinyInt;
                sParams[1].ParameterName = "@aprvNumber";
                sParams[1].Value = approve;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.DateTime;
                sParams[2].ParameterName = "@handoverDate";
                sParams[2].Value = hoDate;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.NVarChar;
                sParams[3].ParameterName = "@response";
                sParams[3].Value = response;

                cls.fnUpdDel(sql, sParams);

                initAprv_List();
                fnAprv_Done();

                _msgText = "Phê duyệt thành công";
                _msgType = 1;
            }
            catch
            {

            }
            finally
            {

            }

        }


        #endregion



        #region UN-APPROVAL


        public void initUnAprv()
        {
            initUnAprv_List();
            fnLinkColor();
        }

        public void initUnAprv_List()
        {
            string range = _rangeUnAprv;
            string sql = "V2o1_BASE_SubMaterial04_Init_UnApproval_SelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@rangeTime";
            sParams[0].Value = range;


            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvUnAprv_List_Width = cls.fnGetDataGridWidth(dgvUnAprv_List);
            dgvUnAprv_List.DataSource = dt;

            //dgvUnAprv_List.Columns[0].Width = 20 * _dgvUnAprv_List_Width / 100;    // idx
            dgvUnAprv_List.Columns[1].Width = 50 * _dgvUnAprv_List_Width / 100;    // rsbmCode
            dgvUnAprv_List.Columns[2].Width = 30 * _dgvUnAprv_List_Width / 100;    // rsbmDate
            dgvUnAprv_List.Columns[3].Width = 20 * _dgvUnAprv_List_Width / 100;    // sum(matQty)
            //dgvUnAprv_List.Columns[4].Width = 30 * _dgvUnAprv_List_Width / 100;    // rsbmReceiver
            //dgvUnAprv_List.Columns[5].Width = 20 * _dgvUnAprv_List_Width / 100;    // rsbmPurpose
            //dgvUnAprv_List.Columns[6].Width = 20 * _dgvUnAprv_List_Width / 100;    // rsbmPriority
            //dgvUnAprv_List.Columns[7].Width = 20 * _dgvUnAprv_List_Width / 100;    // rsbmReason
            //dgvUnAprv_List.Columns[8].Width = 20 * _dgvUnAprv_List_Width / 100;    // rsbmAdd
            //dgvUnAprv_List.Columns[9].Width = 20 * _dgvUnAprv_List_Width / 100;    // mmtApprove
            //dgvUnAprv_List.Columns[10].Width = 20 * _dgvUnAprv_List_Width / 100;    // mmtApproveDate
            //dgvUnAprv_List.Columns[11].Width = 20 * _dgvUnAprv_List_Width / 100;    // mmtResponse
            //dgvUnAprv_List.Columns[12].Width = 20 * _dgvUnAprv_List_Width / 100;    // mmtDateHandOver

            dgvUnAprv_List.Columns[0].Visible = false;
            dgvUnAprv_List.Columns[1].Visible = true;
            dgvUnAprv_List.Columns[2].Visible = true;
            dgvUnAprv_List.Columns[3].Visible = true;
            dgvUnAprv_List.Columns[4].Visible = false;
            dgvUnAprv_List.Columns[5].Visible = false;
            dgvUnAprv_List.Columns[6].Visible = false;
            dgvUnAprv_List.Columns[7].Visible = false;
            dgvUnAprv_List.Columns[8].Visible = false;
            dgvUnAprv_List.Columns[9].Visible = false;
            dgvUnAprv_List.Columns[10].Visible = false;
            dgvUnAprv_List.Columns[11].Visible = false;
            dgvUnAprv_List.Columns[12].Visible = false;

            dgvUnAprv_List.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgvUnAprv_List, 11, 30);
        }

        private void dgvUnAprv_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvUnAprv_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvUnAprv_List, e);
            }
        }

        private void dgvUnAprv_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void lnkUnAprv_Today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dgvUnAprv_List.DataSource = "";
            dgvUnAprv_List.Refresh();
            _rangeUnAprv = "1";
            initUnAprv_List();
            fnLinkColor();
        }

        private void lnkUnAprv_3Days_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dgvUnAprv_List.DataSource = "";
            dgvUnAprv_List.Refresh();
            _rangeUnAprv = "2";
            initUnAprv_List();
            fnLinkColor();
        }

        private void lnkUnAprv_1week_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dgvUnAprv_List.DataSource = "";
            dgvUnAprv_List.Refresh();
            _rangeUnAprv = "3";
            initUnAprv_List();
            fnLinkColor();
        }

        private void lnkUnAprv_All_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dgvUnAprv_List.DataSource = "";
            dgvUnAprv_List.Refresh();
            _rangeUnAprv = "4";
            initUnAprv_List();
            fnLinkColor();
        }

        public void fnLinkColor()
        {
            switch (_rangeUnAprv)
            {
                case "1":
                    lnkUnAprv_Today.LinkColor = Color.Red;
                    lnkUnAprv_3Days.LinkColor = Color.Blue;
                    lnkUnAprv_1week.LinkColor = Color.Blue;
                    lnkUnAprv_All.LinkColor = Color.Blue;
                    break;
                case "2":
                    lnkUnAprv_Today.LinkColor = Color.Blue;
                    lnkUnAprv_3Days.LinkColor = Color.Red;
                    lnkUnAprv_1week.LinkColor = Color.Blue;
                    lnkUnAprv_All.LinkColor = Color.Blue;
                    break;
                case "3":
                    lnkUnAprv_Today.LinkColor = Color.Blue;
                    lnkUnAprv_3Days.LinkColor = Color.Blue;
                    lnkUnAprv_1week.LinkColor = Color.Red;
                    lnkUnAprv_All.LinkColor = Color.Blue;
                    break;
                case "4":
                    lnkUnAprv_Today.LinkColor = Color.Blue;
                    lnkUnAprv_3Days.LinkColor = Color.Blue;
                    lnkUnAprv_1week.LinkColor = Color.Blue;
                    lnkUnAprv_All.LinkColor = Color.Red;
                    break;
            }
        }


        #endregion



        #region BINDING DATA


        public void initBind()
        {
            dtpOut_Bind_Filter.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            initBind_Order_List();
        }

        public void initBind_Order_List()
        {
            DateTime dateFilter = (_dateFilter > DateTime.MinValue) ? _dateFilter : new DateTime(_dt.Year, _dt.Month, _dt.Day);
            _dateFilter = dateFilter;

            string sql = "V2o1_BASE_SubMaterial04_Init_HandOver_Bind_List_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.DateTime;
            sParams[0].ParameterName = "@date";
            sParams[0].Value = dateFilter;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvBind_List_Width = cls.fnGetDataGridWidth(dgvOut_Bind_List);
            dgvOut_Bind_List.DataSource = dt;

            //dgvOut_Bind_List.Columns[0].Width = 20 * _dgvBind_List_Width / 100;    // [idx]
            dgvOut_Bind_List.Columns[1].Width = 50 * _dgvBind_List_Width / 100;    // [rsbmCode]
            dgvOut_Bind_List.Columns[2].Width = 25 * _dgvBind_List_Width / 100;    // [rsbmDate]
            //dgvOut_Bind_List.Columns[3].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmReceiveIDx]
            //dgvOut_Bind_List.Columns[4].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmReceiver]
            //dgvOut_Bind_List.Columns[5].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmPurpose]
            //dgvOut_Bind_List.Columns[6].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmPriority]
            //dgvOut_Bind_List.Columns[7].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmReason]
            //dgvOut_Bind_List.Columns[8].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmAdd]
            //dgvOut_Bind_List.Columns[9].Width = 20 * _dgvBind_List_Width / 100;    // [mmtApprove]
            //dgvOut_Bind_List.Columns[10].Width = 20 * _dgvBind_List_Width / 100;    // [mmtApproveDate]
            //dgvOut_Bind_List.Columns[11].Width = 20 * _dgvBind_List_Width / 100;    // [mmtResponse]
            //dgvOut_Bind_List.Columns[12].Width = 20 * _dgvBind_List_Width / 100;    // [mmtDateHandOver]
            dgvOut_Bind_List.Columns[13].Width = 25 * _dgvBind_List_Width / 100;    // [scanoutDate]
            //dgvOut_Bind_List.Columns[14].Width = 20 * _dgvBind_List_Width / 100;    // [rsbmFinish]

            dgvOut_Bind_List.Columns[0].Visible = false;
            dgvOut_Bind_List.Columns[1].Visible = true;
            dgvOut_Bind_List.Columns[2].Visible = true;
            dgvOut_Bind_List.Columns[3].Visible = false;
            dgvOut_Bind_List.Columns[4].Visible = false;
            dgvOut_Bind_List.Columns[5].Visible = false;
            dgvOut_Bind_List.Columns[6].Visible = false;
            dgvOut_Bind_List.Columns[7].Visible = false;
            dgvOut_Bind_List.Columns[8].Visible = false;
            dgvOut_Bind_List.Columns[9].Visible = false;
            dgvOut_Bind_List.Columns[10].Visible = false;
            dgvOut_Bind_List.Columns[11].Visible = false;
            dgvOut_Bind_List.Columns[12].Visible = false;
            dgvOut_Bind_List.Columns[13].Visible = true;
            dgvOut_Bind_List.Columns[14].Visible = false;

            dgvOut_Bind_List.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvOut_Bind_List.Columns[13].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgvOut_Bind_List, 11, 30);
        }

        private void dgvOut_Bind_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOut_Bind_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvOut_Bind_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvOut_Bind_List.Rows[e.RowIndex];

                string idx = row.Cells[0].Value.ToString();
                string rsbmCode = row.Cells[1].Value.ToString();
                string rsbmDate = row.Cells[2].Value.ToString();
                string rsbmReceiveIDx = row.Cells[3].Value.ToString();
                string rsbmReceiver = row.Cells[4].Value.ToString();
                string rsbmPurpose = row.Cells[5].Value.ToString();
                string rsbmPriority = row.Cells[6].Value.ToString();
                string rsbmReason = row.Cells[7].Value.ToString();
                string rsbmAdd = row.Cells[8].Value.ToString();
                string mmtApprove = row.Cells[9].Value.ToString();
                string mmtApproveDate = row.Cells[10].Value.ToString();
                string mmtResponse = row.Cells[11].Value.ToString();
                string mmtDateHandOver = row.Cells[12].Value.ToString();
                string scanoutDate = row.Cells[13].Value.ToString();
                string rsbmFinish = row.Cells[14].Value.ToString();

                _bindOrderIDx = idx;

                string purposeText = "";
                switch (rsbmPurpose.ToLower())
                {
                    case "repair":
                        purposeText = "SỬA CHỮA";
                        break;
                    case "maintain":
                        purposeText = "BẢO DƯỠNG";
                        break;
                    case "improve":
                        purposeText = "CẢI TIẾN";
                        break;
                    case "daily":
                        purposeText = "HÀNG NGÀY";
                        break;
                }
                string priorityText = "";
                switch (rsbmPriority)
                {
                    case "1":
                        priorityText = "MỨC CAO (1)";
                        break;
                    case "2":
                        priorityText = "MỨC TRUNG BÌNH (2)";
                        break;
                    case "3":
                        priorityText = "MỨC THÔNG THƯỜNG (3)";
                        break;
                }
                string approveText = "";
                switch (mmtApprove)
                {
                    case "0":
                        approveText = "ĐANG XỬ LÝ";
                        break;
                    case "1":
                        approveText = "CHẤP NHẬN YÊU CẦU";
                        break;
                    case "2":
                    case "3":
                        approveText = "YÊU CẦU BỊ TRẢ LẠI";
                        break;
                }

                grpOut_Bind_Maker.Enabled = true;
                grpOut_Bind_Maker.BackColor = Color.White;
                grpOut_Bind_Process.Enabled = true;
                grpOut_Bind_Process.BackColor = Color.White;

                lblOut_Bind_Maker_Name.BackColor = Color.White;
                lblOut_Bind_Maker_Name.Text = rsbmReceiver;
                lblOut_Bind_Maker_Date.BackColor = Color.White;
                lblOut_Bind_Maker_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(rsbmDate));
                lblOut_Bind_Maker_Priority.BackColor = Color.White;
                lblOut_Bind_Maker_Priority.Text = priorityText;
                lblOut_Bind_Maker_Purpose.BackColor = Color.White;
                lblOut_Bind_Maker_Purpose.Text = purposeText;
                lblOut_Bind_Maker_Reason.BackColor = Color.White;
                lblOut_Bind_Maker_Reason.Text = rsbmReason;
                lblOut_Bind_Process_Status.BackColor = Color.White;
                lblOut_Bind_Process_Status.Text = approveText;
                lblOut_Bind_Process_Date.BackColor = Color.White;
                lblOut_Bind_Process_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(mmtApproveDate));
                lblOut_Bind_Process_Response.BackColor = Color.White;
                lblOut_Bind_Process_Response.Text = mmtResponse;
                lblOut_Bind_Process_ScanDate.BackColor = Color.White;
                lblOut_Bind_Process_ScanDate.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(scanoutDate));
                lblOut_Bind_Process_ScanFinish.BackColor = Color.White;
                lblOut_Bind_Process_ScanFinish.Text = (rsbmFinish.ToLower() == "true") ? "Đã xong" : "Chưa xong";

                btnOut_Bind_Print_Delivery_Bill.Enabled = true;

                string sql = "V2o1_BASE_SubMaterial04_Init_HandOver_Bind_Scanout_List_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@orderIDx";
                sParams[0].Value = idx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvBind_Detail_Width = cls.fnGetDataGridWidth(dgvOut_Bind_Detail);
                dgvOut_Bind_Detail.DataSource = dt;

                //dgvOut_Bind_Detail.Columns[0].Width = 20 * _dgvBind_Detail_Width / 100;    // [idx]
                dgvOut_Bind_Detail.Columns[1].Width = 30 * _dgvBind_Detail_Width / 100;    // [packingCode]
                //dgvOut_Bind_Detail.Columns[2].Width = 20 * _dgvBind_Detail_Width / 100;    // [matIDx]
                dgvOut_Bind_Detail.Columns[3].Width = 25 * _dgvBind_Detail_Width / 100;    // [matName]
                dgvOut_Bind_Detail.Columns[4].Width = 20 * _dgvBind_Detail_Width / 100;    // [matCode]
                dgvOut_Bind_Detail.Columns[5].Width = 7 * _dgvBind_Detail_Width / 100;    // [matUnit]
                dgvOut_Bind_Detail.Columns[6].Width = 9 * _dgvBind_Detail_Width / 100;    // [matQty]
                dgvOut_Bind_Detail.Columns[7].Width = 9 * _dgvBind_Detail_Width / 100;    // [IN_Remain]

                dgvOut_Bind_Detail.Columns[0].Visible = false;
                dgvOut_Bind_Detail.Columns[1].Visible = true;
                dgvOut_Bind_Detail.Columns[2].Visible = false;
                dgvOut_Bind_Detail.Columns[3].Visible = true;
                dgvOut_Bind_Detail.Columns[4].Visible = true;
                dgvOut_Bind_Detail.Columns[5].Visible = true;
                dgvOut_Bind_Detail.Columns[6].Visible = true;
                dgvOut_Bind_Detail.Columns[7].Visible = true;

                cls.fnFormatDatagridview(dgvOut_Bind_Detail, 11, 30);
            }
        }

        private void dgvOut_Bind_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        public void initBind_Order_Detail()
        {

        }

        public void initBind_Order_Info()
        {

        }

        private void btnOut_Bind_View_Click(object sender, EventArgs e)
        {
            _dateFilter = dtpOut_Bind_Filter.Value;
            initBind_Order_List();
        }

        private void btnOut_Bind_Print_Delivery_Bill_Click(object sender, EventArgs e)
        {
            frmSub04ScanOutMaterial_PrintBill frm04Print = new frmSub04ScanOutMaterial_PrintBill(_bindOrderIDx);
            frm04Print.ShowDialog();
        }


        #endregion
    }
}
