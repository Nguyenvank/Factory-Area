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
    public partial class frmInternalProductionOrder : Form
    {
        public string _boxIDx = "", _boxCode = "", _boxPack = "", _boxLot = "", _boxQty = "";
        public string _mergeIDx = "", _mergeProdIDx = "", _mergeNewPacking = "", _mergeNewQty = "";
        public string _splitIDx = "", _splitProdIDx = "", _splitNewPacking01 = "", _splitNewQty01 = "", _splitNewPacking02 = "", _splitNewQty02 = "";
        public int _dgvOrder_List_Width;
        public int _dgvBind_List_Width;
        public int _dgvMerge_List_Width;
        public int _dgvSplit_List_Width;
        public int _dgvStatus_List_Width;

        public DateTime _dt;
        public string _range = "1";
        public string _rangeStatus = "3";

        public string _msgText = "";
        public int _msgType = 0;

        public frmInternalProductionOrder()
        {
            InitializeComponent();
        }

        private void frmInternalProductionOrder_Load(object sender, EventArgs e)
        {
            _dgvOrder_List_Width = cls.fnGetDataGridWidth(dgvOrder_List);
            _dgvBind_List_Width = cls.fnGetDataGridWidth(dgvBind_List);
            _dgvMerge_List_Width = cls.fnGetDataGridWidth(dgvMerge_List);
            _dgvSplit_List_Width = cls.fnGetDataGridWidth(dgvSplit_List);
            _dgvStatus_List_Width = cls.fnGetDataGridWidth(dgvStatus_List);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();

            initOrder();
            initBind();
        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnOrder_Done();
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initOrder();
                    break;
                case 1:
                    initMerge();
                    break;
                case 2:
                    initSplit();
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initBind();
                    break;
                case 1:
                    initStatus();
                    break;
                case 2:
                    break;
            }
        }


        #region REQUEST PACKING

        public void initOrder()
        {
            initOrder_PartName();
            lblOrder_List.Text = "Danh sách kiện:";
        }

        public void initOrder_PartName()
        {
            
            string sql = "V2o1_BASE_Internal_Production_Order_PartName_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbOrder_Name.DataSource = dt;
            cbbOrder_Name.DisplayMember = "Name";
            cbbOrder_Name.ValueMember = "prodID";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbbOrder_Name.SelectedIndex = 0;
        }

        public void initOrder_PartCode()
        {
            string prodId = cbbOrder_Name.SelectedValue.ToString();
            string prodCode = "", prodLocate = "";
            string sql = "V2o1_BASE_Internal_Production_Order_PartCode_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@prodId";
            sParams[0].Value = prodId;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                prodCode = ds.Tables[0].Rows[0][0].ToString();
                prodLocate = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                prodCode = "N/A";
                prodLocate = "N/A";
            }
            lblOrder_Code.Text = prodCode;
            lblOrder_Locate.Text = prodLocate;
        }

        public int initOrder_Total()
        {
            int total = 0;
            try
            {
                string prodId = cbbOrder_Name.SelectedValue.ToString();
                string type = "";
                if (rdbOrder_Merge.Checked)
                {
                    type = "m";
                }
                else if (rdbOrder_Split.Checked)
                {
                    type = "s";
                }

                string sql = "V2o1_BASE_Internal_Production_Order_PackingTotal_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodId";
                sParams[0].Value = prodId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "@type";
                sParams[1].Value = type;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    total = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    total = 0;
                }

            }
            catch
            {

            }
            finally
            {

            }
            return total;
        }

        public void initOrder_List(string sql)
        {
            if (cbbOrder_Name.SelectedIndex > 0)
            {
                string prodId = cbbOrder_Name.SelectedValue.ToString();

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodId";
                sParams[0].Value = prodId;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                _dgvOrder_List_Width = cls.fnGetDataGridWidth(dgvOrder_List);
                dgvOrder_List.DataSource = dt;

                //dgvOrder_List.Columns[0].Width = 10 * _dgvOrder_List_Width / 100;    // boxId
                dgvOrder_List.Columns[1].Width = 65 * _dgvOrder_List_Width / 100;    // boxcode
                                                                                     //dgvOrder_List.Columns[2].Width = 10 * _dgvOrder_List_Width / 100;    // packingdate
                dgvOrder_List.Columns[3].Width = 25 * _dgvOrder_List_Width / 100;    // boxLOT
                dgvOrder_List.Columns[4].Width = 10 * _dgvOrder_List_Width / 100;    // boxquantity

                dgvOrder_List.Columns[0].Visible = false;
                dgvOrder_List.Columns[1].Visible = true;
                dgvOrder_List.Columns[2].Visible = false;
                dgvOrder_List.Columns[3].Visible = true;
                dgvOrder_List.Columns[4].Visible = true;

                cls.fnFormatDatagridview(dgvOrder_List, 11, 30);


                _boxIDx = "";
                _boxCode = "";
                _boxPack = "";
                _boxLot = "";
                _boxQty = "";

                lblOrder_BoxCode.Text = "N/A";
                lblOrder_BoxCode.Enabled = false;
                lblOrder_BoxLOT.Text = "N/A";
                lblOrder_BoxLOT.Enabled = false;
                lblOrder_BoxPack.Text = "N/A";
                lblOrder_BoxPack.Enabled = false;
                lblOrder_BoxQty.Text = "0";
                lblOrder_BoxQty.Enabled = false;

                txtOrder_Reason.Text = "";
                txtOrder_Reason.Enabled = false;

                lblOrder_Count.Text = "(1000)";

                btnOrder_Save.Enabled = false;
            }
        }

        private void cbbOrder_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbOrder_Name.SelectedIndex > 0)
            {
                initOrder_PartCode();
                lblOrder_Code.Enabled = true;
                lblOrder_Locate.Enabled = true;

                rdbOrder_Merge.Enabled = true;
                rdbOrder_Split.Enabled = true;

                btnOrder_Save.Enabled = false;
                btnOrder_Done.Enabled = true;
            }
            else
            {
                lblOrder_Code.Text = "N/A";
                lblOrder_Code.Enabled = false;

                lblOrder_Locate.Text = "N/A";
                lblOrder_Locate.Enabled = false;

                rdbOrder_Merge.Enabled = false;
                rdbOrder_Split.Enabled = false;

                btnOrder_Save.Enabled = false;
                btnOrder_Done.Enabled = false;
            }
            rdbOrder_Merge.Checked = false;
            rdbOrder_Split.Checked = false;

            lblOrder_Total.Text = "0";
            dgvOrder_List.DataSource = "";
            dgvOrder_List.Refresh();
            txtOrder_Filter.Text = "";
            txtOrder_Filter.Enabled = false;

            lblOrder_BoxCode.Text = "N/A";
            lblOrder_BoxCode.Enabled = false;
            lblOrder_BoxLOT.Text = "N/A";
            lblOrder_BoxLOT.Enabled = false;
            lblOrder_BoxPack.Text = "N/A";
            lblOrder_BoxPack.Enabled = false;
            lblOrder_BoxQty.Text = "0";
            lblOrder_BoxQty.Enabled = false;

            lblOrder_Count.Text = "(1000)";
            txtOrder_Reason.Text = "";
            txtOrder_Reason.Enabled = false;

            rdbOrder_Add.Checked = false;
            rdbOrder_Add.Enabled = false;
            rdbOrder_Upd.Checked = false;
            rdbOrder_Upd.Enabled = false;
            rdbOrder_Del.Checked = false;
            rdbOrder_Del.Enabled = false;

        }

        private void rdbOrder_Merge_CheckedChanged(object sender, EventArgs e)
        {
            lblOrder_List.Text = "Danh sách kiện lẻ:";
            lblOrder_Total.Text = initOrder_Total().ToString();

            string sql = "V2o1_BASE_Internal_Production_Order_PackingNotFull_SelItem_Addnew";

            initOrder_List(sql);
            txtOrder_Filter.Enabled = true;
        }

        private void rdbOrder_Split_CheckedChanged(object sender, EventArgs e)
        {
            lblOrder_List.Text = "Danh sách kiện đủ:";
            lblOrder_Total.Text = initOrder_Total().ToString();

            string sql = "V2o1_BASE_Internal_Production_Order_PackingFull_SelItem_Addnew";

            initOrder_List(sql);
            txtOrder_Filter.Enabled = true;
        }

        private void dgvOrder_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvOrder_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvOrder_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgvOrder_List.Rows[e.RowIndex];

                string boxIDx = row.Cells[0].Value.ToString();
                string boxCode = row.Cells[1].Value.ToString();
                string boxPack = row.Cells[2].Value.ToString();
                string boxLot = row.Cells[3].Value.ToString();
                string boxQty = row.Cells[4].Value.ToString();

                _boxIDx = boxIDx;
                _boxCode = boxCode;
                _boxPack = boxPack;
                _boxLot = boxLot;
                _boxQty = boxQty;

                lblOrder_BoxCode.Text = boxCode;
                lblOrder_BoxLOT.Text = boxLot;
                lblOrder_BoxPack.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(boxPack));
                lblOrder_BoxQty.Text = boxQty;

                lblOrder_BoxCode.Enabled = true;
                lblOrder_BoxLOT.Enabled = true;
                lblOrder_BoxPack.Enabled = true;
                lblOrder_BoxQty.Enabled = true;

                string reason = "";
                if (rdbOrder_Merge.Checked || rdbOrder_Split.Checked)
                {
                    if (rdbOrder_Merge.Checked)
                    {
                        reason = "Gộp kiện lẻ " + boxCode + " để thành một kiện đủ";
                    }
                    if (rdbOrder_Split.Checked)
                    {
                        reason = "Tách kiện đủ " + boxCode + " để thành hai kiện lẻ";
                    }
                }
                else
                {
                    reason = "";
                }

                txtOrder_Reason.Text = reason;
                txtOrder_Reason.Enabled = true;
                //txtOrder_Reason.Focus();
            }
        }

        private void dgvOrder_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void rdbOrder_Add_CheckedChanged(object sender, EventArgs e)
        {
            btnOrder_Save.Enabled = true;
        }

        private void rdbOrder_Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnOrder_Save.Enabled = true;
        }

        private void rdbOrder_Del_CheckedChanged(object sender, EventArgs e)
        {
            btnOrder_Save.Enabled = true;
        }

        private void btnOrder_Done_Click(object sender, EventArgs e)
        {
            fnOrder_Done();
        }

        private void btnOrder_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (rdbOrder_Add.Checked)
                {
                    fnOrder_Add();
                }
                else if (rdbOrder_Upd.Checked)
                {
                    fnOrder_Upd();
                }
                else if (rdbOrder_Del.Checked)
                {
                    fnOrder_Del();
                }
                //initBind_List();
            }
        }

        public void fnOrder_Done()
        {
            _boxIDx = "";
            _boxCode = "";
            _boxPack = "";
            _boxLot = "";
            _boxQty = "";

            cbbOrder_Name.SelectedIndex = 0;
            cbbOrder_Name.Enabled = true;
            lblOrder_Code.Text = "N/A";
            lblOrder_Code.Enabled = false;
            rdbOrder_Merge.Checked = false;
            rdbOrder_Merge.Enabled = false;
            rdbOrder_Split.Checked = false;
            rdbOrder_Split.Enabled = false;
            lblOrder_Locate.Text = "N/A";
            lblOrder_Locate.Enabled = false;
            lblOrder_List.Text = "Danh sách kiện:";
            lblOrder_Total.Text = "0";
            dgvOrder_List.DataSource = "";
            dgvOrder_List.Refresh();
            txtOrder_Filter.Text = "";
            txtOrder_Filter.Enabled = false;
            lblOrder_BoxCode.Text = "N/A";
            lblOrder_BoxCode.Enabled = false;
            lblOrder_BoxLOT.Text = "N/A";
            lblOrder_BoxLOT.Enabled = false;
            lblOrder_BoxPack.Text = "N/A";
            lblOrder_BoxPack.Enabled = false;
            lblOrder_BoxQty.Text = "0";
            txtOrder_Reason.Text = "";
            txtOrder_Reason.Enabled = false;
            lblOrder_Count.Text = "(1000)";

            rdbOrder_Add.Checked = false;
            rdbOrder_Add.Enabled = false;
            rdbOrder_Upd.Checked = false;
            rdbOrder_Upd.Enabled = false;
            rdbOrder_Del.Checked = false;
            rdbOrder_Del.Enabled = false;

            btnOrder_Save.Enabled = false;
            btnOrder_Done.Enabled = false;

            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initBind();
                    break;
                case 1:
                    initStatus();
                    break;
                case 2:
                    break;
            }
        }

        public void fnOrder_Add()
        {
            try
            {
                string prodIDx = cbbOrder_Name.SelectedValue.ToString();
                string prodName = cbbOrder_Name.Text;
                string prodCode = lblOrder_Code.Text;
                string prodLocate = lblOrder_Locate.Text;
                string boxIDx = _boxIDx;
                string boxCode = _boxCode;
                string boxLOT = _boxLot;
                string boxInDate = _boxPack;
                string boxQty = _boxQty;
                string actType = (rdbOrder_Merge.Checked) ? "True" : "False";      // True: Merge box; False: Split box
                string actReason = txtOrder_Reason.Text.Trim();

                string sql = "V2o1_BASE_Internal_Production_Order_AddItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[11]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodIDx";
                sParams[0].Value = prodIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.NVarChar;
                sParams[1].ParameterName = "@prodName";
                sParams[1].Value = prodName;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.VarChar;
                sParams[2].ParameterName = "@prodCode";
                sParams[2].Value = prodCode;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.VarChar;
                sParams[3].ParameterName = "@prodLocate";
                sParams[3].Value = prodLocate;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.Int;
                sParams[4].ParameterName = "@boxIDx";
                sParams[4].Value = boxIDx;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.VarChar;
                sParams[5].ParameterName = "@boxCode";
                sParams[5].Value = boxCode;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.VarChar;
                sParams[6].ParameterName = "@boxLOT";
                sParams[6].Value = boxLOT;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.DateTime;
                sParams[7].ParameterName = "@boxInDate";
                sParams[7].Value = boxInDate;

                sParams[8] = new SqlParameter();
                sParams[8].SqlDbType = SqlDbType.Int;
                sParams[8].ParameterName = "@boxQty";
                sParams[8].Value = boxQty;

                sParams[9] = new SqlParameter();
                sParams[9].SqlDbType = SqlDbType.Bit;
                sParams[9].ParameterName = "@actType";
                sParams[9].Value = actType;

                sParams[10] = new SqlParameter();
                sParams[10].SqlDbType = SqlDbType.NVarChar;
                sParams[10].ParameterName = "@actReason";
                sParams[10].Value = actReason;

                cls.fnUpdDel(sql, sParams);

                _msgText = "Đã tạo yêu cầu thành công.";
                _msgType = 1;
            }
            catch (SqlException exSql)
            {
                _msgText = "Lỗi đẩy dữ liệu lên máy chủ, vui lòng kiểm tra lại";
                _msgType = 3;
                MessageBox.Show(exSql.ToString());
            }
            catch (Exception ex)
            {
                _msgText = "Có lỗi phát sinh";
                _msgType = 2;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                fnOrder_Done();

                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }

        public void fnOrder_Upd()
        {

        }

        public void fnOrder_Del()
        {

        }

        private void txtOrder_Reason_TextChanged(object sender, EventArgs e)
        {
            int len = txtOrder_Reason.Text.Length;
            lblOrder_Count.Text = "(" + (1000 - len) + ")";

            rdbOrder_Add.Enabled = (len > 0) ? true : false;
        }

        private void txtOrder_Filter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //BindingSource bd = (BindingSource)dgvOrder_List.DataSource;
                //DataTable dt = (DataTable)bd.DataSource;
                //dt.DefaultView.RowFilter = string.Format("boxcode like '%{0}%'", txtOrder_Filter.Text.Trim().Replace("'", "''"));
                //dgvOrder_List.Refresh();

                BindingSource bs = new BindingSource();
                bs.DataSource = dgvOrder_List.DataSource;
                bs.Filter = string.Format("CONVERT(" + dgvOrder_List.Columns[1].DataPropertyName + ", System.String) like '%" + txtOrder_Filter.Text.Replace("'", "''") + "%'");
                dgvOrder_List.DataSource = bs;
            }
            catch
            {

            }
            finally
            {

            }
        }



        #endregion


        #region BINDING DATA


        public void initBind()
        {
            initBind_List();
            fnLinkColor();
        }

        public void initBind_List()
        {
            string range = _range;
            string sql = "V2o1_BASE_Internal_Production_Order_BindList_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@range";
            sParams[0].Value = range;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvBind_List_Width = cls.fnGetDataGridWidth(dgvBind_List);
            dgvBind_List.DataSource = dt;

            //dgvBind_List.Columns[0].Width = 10 * _dgvBind_List_Width / 100;    // [idx]
            //dgvBind_List.Columns[1].Width = 10 * _dgvBind_List_Width / 100;    // [prodIDx]
            dgvBind_List.Columns[2].Width = 20 * _dgvBind_List_Width / 100;    // [boxCode]
            dgvBind_List.Columns[3].Width = 10 * _dgvBind_List_Width / 100;    // [boxMergeSplit]
            dgvBind_List.Columns[4].Width = 20 * _dgvBind_List_Width / 100;    // [prodName]
            dgvBind_List.Columns[5].Width = 12 * _dgvBind_List_Width / 100;    // [prodCode]
            //dgvBind_List.Columns[6].Width = 10 * _dgvBind_List_Width / 100;    // [boxIDx]
            dgvBind_List.Columns[7].Width = 10 * _dgvBind_List_Width / 100;    // [boxLocate]
            dgvBind_List.Columns[8].Width = 10 * _dgvBind_List_Width / 100;    // [boxLOT]
            dgvBind_List.Columns[9].Width = 5 * _dgvBind_List_Width / 100;    // [boxQty]
            dgvBind_List.Columns[10].Width = 15 * _dgvBind_List_Width / 100;    // [boxInDate]
            //dgvBind_List.Columns[11].Width = 10 * _dgvBind_List_Width / 100;    // [boxReason]
            //dgvBind_List.Columns[12].Width = 10 * _dgvBind_List_Width / 100;    // [requestDate]

            dgvBind_List.Columns[0].Visible = false;
            dgvBind_List.Columns[1].Visible = false;
            dgvBind_List.Columns[2].Visible = true;
            dgvBind_List.Columns[3].Visible = true;
            dgvBind_List.Columns[4].Visible = true;
            dgvBind_List.Columns[5].Visible = true;
            dgvBind_List.Columns[6].Visible = false;
            dgvBind_List.Columns[7].Visible = true;
            dgvBind_List.Columns[8].Visible = true;
            dgvBind_List.Columns[9].Visible = true;
            dgvBind_List.Columns[10].Visible = true;
            dgvBind_List.Columns[11].Visible = false;
            dgvBind_List.Columns[12].Visible = false;

            dgvBind_List.Columns[10].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgvBind_List, 11, 30);
        }

        public void fnLinkColor()
        {
            switch (_range)
            {
                case "1":
                    lnkOrder_01.LinkColor = Color.Red;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "2":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Red;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "3":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Red;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "4":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Red;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "5":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Red;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "6":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Red;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "7":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Red;
                    lnkOrder_08.LinkColor = Color.Blue;
                    break;
                case "8":
                    lnkOrder_01.LinkColor = Color.Blue;
                    lnkOrder_02.LinkColor = Color.Blue;
                    lnkOrder_03.LinkColor = Color.Blue;
                    lnkOrder_04.LinkColor = Color.Blue;
                    lnkOrder_05.LinkColor = Color.Blue;
                    lnkOrder_06.LinkColor = Color.Blue;
                    lnkOrder_07.LinkColor = Color.Blue;
                    lnkOrder_08.LinkColor = Color.Red;
                    break;
            }
        }

        private void lnkOrder_01_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "1";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_02_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "2";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_03_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "3";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_04_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "4";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_05_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "5";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_06_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "6";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_07_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "7";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }

        private void lnkOrder_08_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _range = "8";
            dgvBind_List.DataSource = "";
            dgvBind_List.Refresh();
            initBind_List();
            fnLinkColor();
        }




        #endregion


        #region MERGE PACKING


        public void initMerge()
        {
            initMerge_List();
            fnMerge_Done();
        }

        public void initMerge_List()
        {
            string sql = "V2o1_BASE_Internal_Production_Merge_List_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvMerge_List_Width = cls.fnGetDataGridWidth(dgvMerge_List);
            dgvMerge_List.DataSource = dt;

            //dgvMerge_List.Columns[0].Width = 10 * _dgvMerge_List_Width / 100;    // idx
            dgvMerge_List.Columns[1].Width = 60 * _dgvMerge_List_Width / 100;    // boxCode
            dgvMerge_List.Columns[2].Width = 15 * _dgvMerge_List_Width / 100;    // boxQty
            dgvMerge_List.Columns[3].Width = 25 * _dgvMerge_List_Width / 100;    // mmtScanDate
            //dgvMerge_List.Columns[4].Width = 25 * _dgvMerge_List_Width / 100;    // prodIdx

            dgvMerge_List.Columns[0].Visible = false;
            dgvMerge_List.Columns[1].Visible = true;
            dgvMerge_List.Columns[2].Visible = true;
            dgvMerge_List.Columns[3].Visible = true;
            dgvMerge_List.Columns[4].Visible = false;

            dgvMerge_List.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgvMerge_List, 11, 30);
        }

        public void initMerge_Detail()
        {
            string mergeIDx = _mergeIDx;
            string merPacking = "", merProdIDx = "", merProdName = "", merProdCode = "", merInDate = "", merQty = "", merLOT = "", merLoc = "";
            string mmtOutDate = "", mmtOutStatus = "", mmtOutRemark = "";
            string sql = "V2o1_BASE_Internal_Production_Merge_List_Detail_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@mergeIDx";
            sParams[0].Value = mergeIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                merPacking = ds.Tables[0].Rows[0][1].ToString();
                merProdIDx = ds.Tables[0].Rows[0][2].ToString();
                merProdName = ds.Tables[0].Rows[0][3].ToString();
                merProdCode = ds.Tables[0].Rows[0][4].ToString();
                merInDate = ds.Tables[0].Rows[0][5].ToString();
                merQty = ds.Tables[0].Rows[0][6].ToString();
                merLOT = ds.Tables[0].Rows[0][7].ToString();
                merLoc = ds.Tables[0].Rows[0][8].ToString();
                mmtOutDate = ds.Tables[0].Rows[0][9].ToString();
                mmtOutStatus = ds.Tables[0].Rows[0][10].ToString();
                mmtOutRemark = ds.Tables[0].Rows[0][11].ToString();

                tlpMerge_NewPacking.BackColor = Color.LightGreen;
                txtMerge_NewPacking.BackColor = Color.LightGreen;
                txtMerge_NewPacking.Enabled = true;
                txtMerge_NewPacking.Focus();
            }
            else
            {
                merPacking = "";
                merProdIDx = "N/A";
                merProdName = "N/A";
                merProdCode = "N/A";
                merInDate = "N/A";
                merQty = "0";
                merLOT = "N/A";
                merLoc = "N/A";
                mmtOutDate = "N/A";
                mmtOutStatus = "N/A";
                mmtOutRemark = "N/A";

                tlpMerge_NewPacking.BackColor = Color.Silver;
                txtMerge_NewPacking.BackColor = Color.Silver;
                txtMerge_NewPacking.Enabled = false;
            }

            lblMerge_Packing.Text = merPacking;
            lblMerge_Name.Text = merProdName;
            lblMerge_Code.Text = merProdCode;
            lblMerge_InDate.Text = (merInDate != "N/A") ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(merInDate)) : merInDate;
            lblMerge_Qty.Text = merQty;
            lblMerge_Lot.Text = merLOT;
            lblMerge_Locate.Text = merLoc;
            lblMerge_OutDate.Text = (mmtOutDate != "N/A") ? String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(mmtOutDate)) : mmtOutDate;
            lblMerge_OutStatus.Text = mmtOutStatus;
            lblMerge_OutRemark.Text = mmtOutRemark;
        }

        public void fnMerge_Done()
        {
            _mergeIDx = "";

            dgvMerge_List.ClearSelection();
            lblMerge_Packing.Text = "";
            lblMerge_Name.Text = "N/A";
            lblMerge_Code.Text = "N/A";
            lblMerge_InDate.Text = "N/A";
            lblMerge_Qty.Text = "0";
            lblMerge_Lot.Text = "N/A";
            lblMerge_Locate.Text = "N/A";
            lblMerge_OutDate.Text = "N/A";
            lblMerge_OutStatus.Text = "N/A";
            lblMerge_OutRemark.Text = "N/A";
            lblMerge_NewQty.Text = "0";
            txtMerge_NewPacking.Text = "";
            txtMerge_NewPacking.Enabled = false;
            txtMerge_NewPacking.BackColor = Color.Silver;
            tlpMerge_NewPacking.BackColor = Color.Silver;
            btnMerge_Save.Enabled = false;
            btnMerge_Done.Enabled = false;

            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
                    initBind();
                    break;
                case 1:
                    initStatus();
                    break;
                case 2:
                    break;
            }
        }

        private void dgvMerge_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvMerge_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvMerge_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgvMerge_List.Rows[e.RowIndex];

                string mergeIDx = row.Cells[0].Value.ToString();
                string mergeBoxCode = row.Cells[1].Value.ToString();
                string mergeBoxQty = row.Cells[2].Value.ToString();
                string mergeScanOut = row.Cells[3].Value.ToString();
                string mergeProdIDx = row.Cells[4].Value.ToString();

                _mergeIDx = mergeIDx;
                _mergeProdIDx = mergeProdIDx;
                initMerge_Detail();

                btnMerge_Done.Enabled = true;
            }
        }

        private void dgvMerge_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtMerge_NewPacking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string packing = txtMerge_NewPacking.Text.Trim();
                if (packing != "" && packing != null)
                {
                    if (packing.Length > 13)
                    {
                        string packType = packing.Substring(0, 3).ToUpper();
                        string packKind = packing.Substring(4, 3).ToUpper();
                        string packCode = packing.Substring(8);

                        if (packType == "PRO")
                        {
                            if (packKind == "PCS" || packKind == "BOX" || packKind == "CAR" || packKind == "PAL")
                            {
                                string prodIDx = _mergeProdIDx;
                                string stdQty = "";
                                string sql = "V2o1_BASE_Internal_Production_Merge_Standard_Qty_SelItem_Addnew";

                                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                                sParams[0] = new SqlParameter();
                                sParams[0].SqlDbType = SqlDbType.Int;
                                sParams[0].ParameterName = "@prodIDx";
                                sParams[0].Value = prodIDx;

                                sParams[1] = new SqlParameter();
                                sParams[1].SqlDbType = SqlDbType.VarChar;
                                sParams[1].ParameterName = "@packKind";
                                sParams[1].Value = packKind;

                                DataSet ds = new DataSet();
                                ds = cls.ExecuteDataSet(sql, sParams);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    stdQty = ds.Tables[0].Rows[0][0].ToString();
                                }
                                else
                                {
                                    stdQty = "0";
                                }

                                lblMerge_NewQty.Text = stdQty;

                                _mergeNewPacking = (stdQty != "0") ? packing : "";
                                _mergeNewQty = (stdQty != "0") ? stdQty : "";

                                lblMerge_NewQty.BackColor = (stdQty != "0") ? Color.LightGreen : Color.Red;
                                btnMerge_Save.Enabled = (stdQty != "0") ? true : false;
                                _msgText = (stdQty != "0") ? "Kiện mới " + packing + " đã sẵn sàng để gộp kiện." : "Kiểu kiện mới chưa có số lượng chuẩn, vui lòng kiểm tra lại";
                                _msgType = (stdQty != "0") ? 1 : 2;
                            }
                            else
                            {
                                _msgText = "Định dạng kiểu kiện hàng không đúng, chỉ có thể là PCS / BOX / CAR / PAL";
                                _msgType = 2;
                            }
                        }
                        else
                        {
                            _msgText = "Định dạng mã kiện không đúng, bắt buộc phải bắt đầu bằng PRO (dành cho tem sản xuất).";
                            _msgType = 2;
                        }
                    }
                    else
                    {
                        _msgText = "Độ dài mã kiện mới không đúng, vui lòng kiểm tra lại.";
                        _msgType = 2;
                    }
                }
                else
                {
                    _msgText = "Vui lòng nhập mã kiện mới.";
                    _msgType = 2;
                }

                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }

        private void btnMerge_Done_Click(object sender, EventArgs e)
        {
            fnMerge_Done();
        }

        private void btnMerge_Save_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string mergeIDx = _mergeIDx;
                    string mergePacking = lblMerge_Packing.Text;
                    string prodIDx = _mergeProdIDx;
                    string prodName = lblMerge_Name.Text;
                    string prodCode = lblMerge_Code.Text;
                    string prodInDate = lblMerge_InDate.Text;
                    string prodLOT = lblMerge_Lot.Text;
                    string prodLocate = lblMerge_Locate.Text;
                    string mergeNewPacking = _mergeNewPacking;
                    string mergeNewQty = _mergeNewQty;
                    string mergeNewLOT = cls.fnGetDate("ls");

                    string sql = "V2o1_BASE_Internal_Production_Merge_New_Packing_AddItem_Addnew";

                    SqlParameter[] sParams = new SqlParameter[11]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@mergeIDx";
                    sParams[0].Value = mergeIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@prodIDx";
                    sParams[1].Value = prodIDx;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.VarChar;
                    sParams[2].ParameterName = "@mergeNewPacking";
                    sParams[2].Value = mergeNewPacking;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.Int;
                    sParams[3].ParameterName = "@mergeNewQty";
                    sParams[3].Value = mergeNewQty;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.VarChar;
                    sParams[4].ParameterName = "@mergeNewLOT";
                    sParams[4].Value = mergeNewLOT;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.VarChar;
                    sParams[5].ParameterName = "@mergePacking";
                    sParams[5].Value = mergePacking;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.NVarChar;
                    sParams[6].ParameterName = "@prodName";
                    sParams[6].Value = prodName;

                    sParams[7] = new SqlParameter();
                    sParams[7].SqlDbType = SqlDbType.VarChar;
                    sParams[7].ParameterName = "@prodCode";
                    sParams[7].Value = prodCode;

                    sParams[8] = new SqlParameter();
                    sParams[8].SqlDbType = SqlDbType.DateTime;
                    sParams[8].ParameterName = "@prodInDate";
                    sParams[8].Value = prodInDate;

                    sParams[9] = new SqlParameter();
                    sParams[9].SqlDbType = SqlDbType.VarChar;
                    sParams[9].ParameterName = "@prodLOT";
                    sParams[9].Value = prodLOT;

                    sParams[10] = new SqlParameter();
                    sParams[10].SqlDbType = SqlDbType.VarChar;
                    sParams[10].ParameterName = "@prodLocate";
                    sParams[10].Value = prodLocate;

                    cls.fnUpdDel(sql, sParams);

                    initMerge_List();
                    fnMerge_Done();

                    _msgText = "Thiết lập gộp kiện thành công";
                    _msgType = 1;
                }
            }
            catch (SqlException sqlEx)
            {
                _msgText = "Đẩy dữ liệu phát sinh lỗi, vui lòng kiểm tra lại";
                _msgType = 2;

                MessageBox.Show(sqlEx.ToString());
            }
            catch (Exception ex)
            {
                _msgText = "Có lỗi phát sinh, vui lòng kiểm tra lại";
                _msgType = 2;

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }


        #endregion


        #region SPLIT PACKING


        public void initSplit()
        {
            initSplit_List();

            lblSplit_Packing.Text = "N/A";
            lblSplit_Name.Text = "N/A";
            lblSplit_Code.Text = "N/A";
            lblSplit_InDate.Text = "N/A";
            lblSplit_Qty.Text = "0";
            lblSplit_Lot.Text = "N/A";
            lblSplit_Locate.Text = "N/A";

            lblSplit_OutDate.Text = "N/A";
            lblSplit_OutStatus.Text = "N/A";
            lblSplit_OutRemark.Text = "N/A";

            txtSplit_NewQty01.Text = "0";
            txtSplit_NewQty01.Enabled = false;
            txtSplit_NewQty01.BackColor = Color.Silver;
            txtSplit_NewPacking01.Text = "N/A";
            txtSplit_NewPacking01.Enabled = false;
            txtSplit_NewPacking01.BackColor = Color.Silver;
            tlpSplit_NewPacking01.BackColor = Color.Silver;

            txtSplit_NewQty02.Text = "0";
            txtSplit_NewQty02.Enabled = false;
            txtSplit_NewQty02.BackColor = Color.Silver;
            txtSplit_NewPacking02.Text = "N/A";
            txtSplit_NewPacking02.Enabled = false;
            txtSplit_NewPacking02.BackColor = Color.Silver;
            tlpSplit_NewPacking02.BackColor = Color.Silver;

            btnSplit_Save.Enabled = false;
            btnSplit_Done.Enabled = false;
        }

        public void initSplit_List()
        {
            string sql = "V2o1_BASE_Internal_Production_Split_List_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgvSplit_List_Width = cls.fnGetDataGridWidth(dgvSplit_List);
            dgvSplit_List.DataSource = dt;

            //dgvSplit_List.Columns[0].Width = 10 * _dgvSplit_List_Width / 100;    // idx
            dgvSplit_List.Columns[1].Width = 60 * _dgvSplit_List_Width / 100;    // boxCode
            dgvSplit_List.Columns[2].Width = 15 * _dgvSplit_List_Width / 100;    // boxQty
            dgvSplit_List.Columns[3].Width = 25 * _dgvSplit_List_Width / 100;    // mmtScanDate
            //dgvSplit_List.Columns[4].Width = 25 * _dgvSplit_List_Width / 100;    // prodIdx

            dgvSplit_List.Columns[0].Visible = false;
            dgvSplit_List.Columns[1].Visible = true;
            dgvSplit_List.Columns[2].Visible = true;
            dgvSplit_List.Columns[3].Visible = true;
            dgvSplit_List.Columns[4].Visible = false;

            dgvSplit_List.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgvSplit_List, 11, 30);

        }

        public void initSplit_Detail()
        {
            string splitIDx = _splitIDx;
            string splPacking = "", splProdIDx = "", splProdName = "", splProdCode = "", splInDate = "", splQty = "", splLOT = "", splLoc = "";
            string mmtOutDate = "", mmtOutStatus = "", mmtOutRemark = "";
            string sql = "V2o1_BASE_Internal_Production_Split_List_Detail_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@splitIDx";
            sParams[0].Value = splitIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                splPacking = ds.Tables[0].Rows[0][1].ToString();
                splProdIDx = ds.Tables[0].Rows[0][2].ToString();
                splProdName = ds.Tables[0].Rows[0][3].ToString();
                splProdCode = ds.Tables[0].Rows[0][4].ToString();
                splInDate = ds.Tables[0].Rows[0][5].ToString();
                splQty = ds.Tables[0].Rows[0][6].ToString();
                splLOT = ds.Tables[0].Rows[0][7].ToString();
                splLoc = ds.Tables[0].Rows[0][8].ToString();
                mmtOutDate = ds.Tables[0].Rows[0][9].ToString();
                mmtOutStatus = ds.Tables[0].Rows[0][10].ToString();
                mmtOutRemark = ds.Tables[0].Rows[0][11].ToString();

                txtSplit_NewQty01.Text = "0";
                txtSplit_NewQty01.Enabled = false;
                txtSplit_NewQty01.BackColor = Color.Silver;
                txtSplit_NewPacking01.Text = "";
                txtSplit_NewPacking01.Enabled = true;
                txtSplit_NewPacking01.Focus();
                txtSplit_NewPacking01.BackColor = Color.LightGreen;
                tlpSplit_NewPacking01.BackColor = Color.LightGreen;

                txtSplit_NewQty02.Text = "0";
                txtSplit_NewQty02.Enabled = false;
                txtSplit_NewQty02.BackColor = Color.Silver;
                txtSplit_NewPacking02.Text = "N/A";
                txtSplit_NewPacking02.Enabled = false;
                txtSplit_NewPacking02.BackColor = Color.Silver;
                tlpSplit_NewPacking02.BackColor = Color.Silver;
            }
            else
            {
                splPacking = "";
                splProdIDx = "N/A";
                splProdName = "N/A";
                splProdCode = "N/A";
                splInDate = "N/A";
                splQty = "0";
                splLOT = "N/A";
                splLoc = "N/A";
                mmtOutDate = "N/A";
                mmtOutStatus = "N/A";
                mmtOutRemark = "N/A";

                txtSplit_NewQty01.Text = "0";
                txtSplit_NewQty01.Enabled = false;
                txtSplit_NewQty01.BackColor = Color.Silver;
                txtSplit_NewPacking01.Text = "N/A";
                txtSplit_NewPacking01.Enabled = false;
                txtSplit_NewPacking01.BackColor = Color.Silver;
                tlpSplit_NewPacking01.BackColor = Color.Silver;

                txtSplit_NewQty02.Text = "0";
                txtSplit_NewQty02.Enabled = false;
                txtSplit_NewQty02.BackColor = Color.Silver;
                txtSplit_NewPacking02.Text = "N/A";
                txtSplit_NewPacking02.Enabled = false;
                txtSplit_NewPacking02.BackColor = Color.Silver;
                tlpSplit_NewPacking02.BackColor = Color.Silver;
            }

            lblSplit_Packing.Text = splPacking;
            lblSplit_Name.Text = splProdName;
            lblSplit_Code.Text = splProdCode;
            lblSplit_InDate.Text = (splInDate != "N/A") ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(splInDate)) : splInDate;
            lblSplit_Qty.Text = splQty;
            lblSplit_Lot.Text = splLOT;
            lblSplit_Locate.Text = splLoc;
            lblSplit_OutDate.Text = (mmtOutDate != "N/A") ? String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(mmtOutDate)) : mmtOutDate;
            lblSplit_OutStatus.Text = mmtOutStatus;
            lblSplit_OutRemark.Text = mmtOutRemark;
        }

        private void dgvSplit_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvSplit_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvSplit_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgvSplit_List.Rows[e.RowIndex];

                string splitIDx = row.Cells[0].Value.ToString();
                string splitBoxCode = row.Cells[1].Value.ToString();
                string splitBoxQty = row.Cells[2].Value.ToString();
                string splitScanOut = row.Cells[3].Value.ToString();
                string splitProdIDx = row.Cells[4].Value.ToString();

                _splitIDx = splitIDx;
                _splitProdIDx = splitProdIDx;
                initSplit_Detail();

                btnSplit_Done.Enabled = true;
            }
        }

        private void dgvSplit_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void txtSplit_NewPacking01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string packing = txtSplit_NewPacking01.Text.Trim();
                if (packing != "" && packing != null)
                {
                    if (packing.Length > 13)
                    {
                        string packType = packing.Substring(0, 3).ToUpper();
                        string packKind = packing.Substring(4, 3).ToUpper();
                        string packCode = packing.Substring(8);

                        if (packType == "PRO")
                        {
                            if (packKind == "PCS" || packKind == "BOX" || packKind == "CAR" || packKind == "PAL")
                            {
                                string prodIDx = _splitProdIDx;
                                string stdQty = "";
                                string sql = "V2o1_BASE_Internal_Production_Split_Standard_Qty_SelItem_Addnew";

                                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                                sParams[0] = new SqlParameter();
                                sParams[0].SqlDbType = SqlDbType.Int;
                                sParams[0].ParameterName = "@prodIDx";
                                sParams[0].Value = prodIDx;

                                sParams[1] = new SqlParameter();
                                sParams[1].SqlDbType = SqlDbType.VarChar;
                                sParams[1].ParameterName = "@packing";
                                sParams[1].Value = packing;

                                DataSet ds = new DataSet();
                                ds = cls.ExecuteDataSet(sql, sParams);
                                int n = ds.Tables[0].Rows.Count;

                                _splitNewPacking01 = (n == 0) ? packing : "";

                                txtSplit_NewQty01.Enabled = (n == 0) ? true : false;
                                txtSplit_NewQty01.BackColor = (n == 0) ? Color.LightGreen : Color.Red;
                                txtSplit_NewQty01.Text = (n == 0) ? lblSplit_Qty.Text : "0";
                                if (n == 0) { txtSplit_NewQty01.Focus(); }

                                _msgText = (n == 0) ? "Kiện " + packing + " đã sẵn sàng để tách kiện." : "Kiện mới đã tồn tại trên hệ thống và chưa được xuất ra, vui lòng kiểm tra lại";
                                _msgType = (n == 0) ? 1 : 2;


                                txtSplit_NewPacking02.Enabled = (n == 0) ? true : false;
                                txtSplit_NewPacking02.BackColor = (n == 0) ? Color.LightYellow : Color.Silver;
                                tlpSplit_NewPacking02.BackColor = (n == 0) ? Color.LightYellow : Color.Silver;

                                txtSplit_NewQty02.Enabled = (n == 0) ? true : false;
                                txtSplit_NewQty02.BackColor = (n == 0) ? Color.LightYellow : Color.Silver;
                            }
                            else
                            {
                                _msgText = "Định dạng kiểu kiện hàng không đúng, chỉ có thể là PCS / BOX / CAR / PAL";
                                _msgType = 2;
                            }
                        }
                        else
                        {
                            _msgText = "Định dạng mã kiện không đúng, bắt buộc phải bắt đầu bằng PRO (dành cho tem sản xuất).";
                            _msgType = 2;
                        }
                    }
                    else
                    {
                        _msgText = "Độ dài mã kiện mới không đúng, vui lòng kiểm tra lại.";
                        _msgType = 2;
                    }
                }
                else
                {
                    _msgText = "Vui lòng nhập mã kiện mới.";
                    _msgType = 2;
                }

                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }

        private void txtSplit_NewQty01_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSplit_NewQty01_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string val = txtSplit_NewQty01.Text.Trim();
                int _val = Convert.ToInt32(val);
                int _std = Convert.ToInt32(lblSplit_Qty.Text);

                if (val == "0" || val == "" || val == null || _val > _std)
                {
                    txtSplit_NewQty01.Text = _std.ToString();
                    txtSplit_NewQty02.Text = "0";

                    tlpSplit_NewPacking02.BackColor = Color.Silver;
                    txtSplit_NewPacking02.BackColor = Color.Silver;
                    txtSplit_NewPacking02.Enabled = false;
                    txtSplit_NewQty02.BackColor = Color.Silver;
                    txtSplit_NewQty02.Enabled = false;
                }
                else
                {
                    int _rem = _std - _val;
                    txtSplit_NewQty02.Text = _rem.ToString();

                    txtSplit_NewPacking02.Text = (_val != _std) ? "" : "N/A";
                    txtSplit_NewPacking02.Enabled = (_val != _std) ? true : false;
                    txtSplit_NewPacking02.BackColor = (_val != _std) ? Color.LightYellow : Color.Silver;
                    tlpSplit_NewPacking02.BackColor = (_val != _std) ? Color.LightYellow : Color.Silver;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txtSplit_NewQty01_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSplit_NewPacking02.Focus();
            }
        }

        private void txtSplit_NewPacking02_TextChanged(object sender, EventArgs e)
        {
            string val01 = txtSplit_NewPacking01.Text.Trim();
            string val02 = txtSplit_NewPacking02.Text.Trim();
            if (val02.Length > 13 && val01 != val02)
            {
                btnSplit_Save.Enabled = true;
            }
            else
            {
                btnSplit_Save.Enabled = false;
            }
        }

        private void btnSplit_Done_Click(object sender, EventArgs e)
        {
            fnSplit_Done();
        }

        public void fnSplit_Done()
        {
            _splitIDx = "";
            _splitProdIDx = "";
            _splitNewPacking01 = "";
            _splitNewQty01 = "";
            _splitNewPacking02 = "";
            _splitNewQty02 = "";

            dgvSplit_List.ClearSelection();

            lblSplit_Packing.Text = "N/A";
            lblSplit_Name.Text = "N/A";
            lblSplit_Code.Text = "N/A";
            lblSplit_InDate.Text = "N/A";
            lblSplit_Qty.Text = "0";
            lblSplit_Lot.Text = "N/A";
            lblSplit_Locate.Text = "N/A";

            lblSplit_OutDate.Text = "N/A";
            lblSplit_OutStatus.Text = "N/A";
            lblSplit_OutRemark.Text = "N/A";

            txtSplit_NewQty01.Text = "0";
            txtSplit_NewQty01.Enabled = false;
            txtSplit_NewQty01.BackColor = Color.Silver;
            txtSplit_NewPacking01.Text = "N/A";
            txtSplit_NewPacking01.Enabled = false;
            txtSplit_NewPacking01.BackColor = Color.Silver;
            tlpSplit_NewPacking01.BackColor = Color.Silver;

            txtSplit_NewQty02.Text = "0";
            txtSplit_NewQty02.Enabled = false;
            txtSplit_NewQty02.BackColor = Color.Silver;
            txtSplit_NewPacking02.Text = "N/A";
            txtSplit_NewPacking02.Enabled = false;
            txtSplit_NewPacking02.BackColor = Color.Silver;
            tlpSplit_NewPacking02.BackColor = Color.Silver;

            btnSplit_Save.Enabled = false;
            btnSplit_Done.Enabled = false;
        }

        private void btnSplit_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                fnSplit_Add();
                
                initSplit_List();
            }
        }

        public void fnSplit_Add()
        {
            try
            {
                string splitIDx = _splitIDx;
                string splitPacking = lblSplit_Packing.Text;
                string packingQty = lblSplit_Qty.Text;
                string prodIDx = _splitProdIDx;
                string prodName = lblSplit_Name.Text;
                string prodCode = lblSplit_Code.Text;
                string prodInDate = lblSplit_InDate.Text;
                string prodLOT = lblSplit_Lot.Text;
                string prodLocate = lblSplit_Locate.Text;
                string newSplitPacking01 = txtSplit_NewPacking01.Text.Trim();
                string newPackingQty01 = txtSplit_NewQty01.Text.Trim();
                string newSplitPacking02 = txtSplit_NewPacking02.Text.Trim();
                string newPackingQty02 = txtSplit_NewQty02.Text.Trim();

                string sql = "V2o1_BASE_Internal_Production_Split_New_Packing_AddItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[13]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@splitIDx";
                sParams[0].Value = splitIDx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "@splitPacking";
                sParams[1].Value = splitPacking;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.Int;
                sParams[2].ParameterName = "@packingQty";
                sParams[2].Value = packingQty;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.Int;
                sParams[3].ParameterName = "@prodIDx";
                sParams[3].Value = prodIDx;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.NVarChar;
                sParams[4].ParameterName = "@prodName";
                sParams[4].Value = prodName;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.VarChar;
                sParams[5].ParameterName = "@prodCode";
                sParams[5].Value = prodCode;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.DateTime;
                sParams[6].ParameterName = "@prodInDate";
                sParams[6].Value = prodInDate;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.VarChar;
                sParams[7].ParameterName = "@prodLOT";
                sParams[7].Value = prodLOT;

                sParams[8] = new SqlParameter();
                sParams[8].SqlDbType = SqlDbType.VarChar;
                sParams[8].ParameterName = "@prodLocate";
                sParams[8].Value = prodLocate;

                sParams[9] = new SqlParameter();
                sParams[9].SqlDbType = SqlDbType.VarChar;
                sParams[9].ParameterName = "@newSplitPacking01";
                sParams[9].Value = newSplitPacking01;

                sParams[10] = new SqlParameter();
                sParams[10].SqlDbType = SqlDbType.Int;
                sParams[10].ParameterName = "@newPackingQty01";
                sParams[10].Value = newPackingQty01;

                sParams[11] = new SqlParameter();
                sParams[11].SqlDbType = SqlDbType.VarChar;
                sParams[11].ParameterName = "@newSplitPacking02";
                sParams[11].Value = newSplitPacking02;

                sParams[12] = new SqlParameter();
                sParams[12].SqlDbType = SqlDbType.Int;
                sParams[12].ParameterName = "@newPackingQty02";
                sParams[12].Value = newPackingQty02;

                cls.fnUpdDel(sql, sParams);

                fnSplit_Done();

                _msgText = "Đã tách kiện thành công.";
                _msgType = 1;
            }
            catch(SqlException sqlEx)
            {
                _msgText = "Có lỗi đẩy dữ liệu, vui lòng kiểm tra lại";
                _msgType = 3;
                MessageBox.Show(sqlEx.ToString());
            }
            catch (Exception ex)
            {
                _msgText = "Có lỗi phát sinh, vui lòng kiểm tra lại";
                _msgType = 2;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cls.fnMessage(tssMessage, _msgText, _msgType);
            }
        }



        #endregion


        #region REQUEST STATUS

        public void initStatus()
        {
            initStatus_List();
            fnStatus_LinkColor();
        }

        public void initStatus_List()
        {
            string range = _rangeStatus;
            string sql = "V2o1_BASE_Internal_Warehouse_Responsive_BindList_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@range";
            sParams[0].Value = range;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgvStatus_List_Width = cls.fnGetDataGridWidth(dgvStatus_List);
            dgvStatus_List.DataSource = dt;

            //dgvStatus_List.Columns[0].Width = 10 * _dgvStatus_List_Width / 100;    // [idx]
            //dgvStatus_List.Columns[1].Width = 10 * _dgvStatus_List_Width / 100;    // [prodIDx]
            dgvStatus_List.Columns[2].Width = 20 * _dgvStatus_List_Width / 100;    // [boxCode]
            dgvStatus_List.Columns[3].Width = 15 * _dgvStatus_List_Width / 100;    // [prodName]
            dgvStatus_List.Columns[4].Width = 14 * _dgvStatus_List_Width / 100;    // [prodCode]
            //dgvStatus_List.Columns[5].Width = 10 * _dgvStatus_List_Width / 100;    // [boxIDx]
            dgvStatus_List.Columns[6].Width = 8 * _dgvStatus_List_Width / 100;    // [boxLocate]
            dgvStatus_List.Columns[7].Width = 8 * _dgvStatus_List_Width / 100;    // [boxLOT]
            dgvStatus_List.Columns[8].Width = 5 * _dgvStatus_List_Width / 100;    // [boxQty]
            dgvStatus_List.Columns[9].Width = 12 * _dgvStatus_List_Width / 100;    // [boxInDate]
            dgvStatus_List.Columns[10].Width = 10 * _dgvStatus_List_Width / 100;    // [boxMergeSplit]
            //dgvStatus_List.Columns[11].Width = 10 * _dgvStatus_List_Width / 100;    // [boxReason]
            //dgvStatus_List.Columns[12].Width = 10 * _dgvStatus_List_Width / 100;    // [requestDate]
            //dgvStatus_List.Columns[13].Width = 10 * _dgvStatus_List_Width / 100;    // [boxStatus]
            dgvStatus_List.Columns[14].Width = 8 * _dgvStatus_List_Width / 100;    // [boxDone]

            dgvStatus_List.Columns[0].Visible = false;
            dgvStatus_List.Columns[1].Visible = false;
            dgvStatus_List.Columns[2].Visible = true;
            dgvStatus_List.Columns[3].Visible = true;
            dgvStatus_List.Columns[4].Visible = true;
            dgvStatus_List.Columns[5].Visible = false;
            dgvStatus_List.Columns[6].Visible = true;
            dgvStatus_List.Columns[7].Visible = true;
            dgvStatus_List.Columns[8].Visible = true;
            dgvStatus_List.Columns[9].Visible = true;
            dgvStatus_List.Columns[10].Visible = true;
            dgvStatus_List.Columns[11].Visible = false;
            dgvStatus_List.Columns[12].Visible = false;
            dgvStatus_List.Columns[13].Visible = false;
            dgvStatus_List.Columns[14].Visible = true;

            dgvStatus_List.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvStatus_List.Columns[14].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridviewWhite(dgvStatus_List, 11, 30);
        }

        public void fnStatus_LinkColor()
        {
            switch (_rangeStatus)
            {
                case "1":
                    lnkStatus_01.LinkColor = Color.Red;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "2":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Red;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "3":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Red;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "4":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Red;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "5":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Red;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "6":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Red;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "7":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Red;
                    lnkStatus_08.LinkColor = Color.Blue;
                    break;
                case "8":
                    lnkStatus_01.LinkColor = Color.Blue;
                    lnkStatus_02.LinkColor = Color.Blue;
                    lnkStatus_03.LinkColor = Color.Blue;
                    lnkStatus_04.LinkColor = Color.Blue;
                    lnkStatus_05.LinkColor = Color.Blue;
                    lnkStatus_06.LinkColor = Color.Blue;
                    lnkStatus_07.LinkColor = Color.Blue;
                    lnkStatus_08.LinkColor = Color.Red;
                    break;
            }
        }

        private void lnkStatus_01_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "1";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_02_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "2";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_03_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "3";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_04_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "4";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_05_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "5";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_06_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "6";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_07_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "7";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void lnkStatus_08_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _rangeStatus = "8";
            dgvStatus_List.DataSource = "";
            dgvStatus_List.Refresh();
            initStatus_List();
            fnStatus_LinkColor();
        }

        private void dgvStatus_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvStatus_List.ClearSelection();
            foreach (DataGridViewRow row in dgvStatus_List.Rows)
            {
                string status = row.Cells[13].Value.ToString();
                if (status.ToLower() == "true")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }

        private void dgvStatus_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvStatus_List.ClearSelection();
                cls.fnDatagridClickCell(dgvStatus_List, e);
            }
        }

        private void dgvStatus_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        #endregion
    }
}
