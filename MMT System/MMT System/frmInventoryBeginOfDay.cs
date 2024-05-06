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
    public partial class frmInventoryBeginOfDay : Form
    {

        public int whFG = 100;          // WH6
        public int whResin = 101;       // WH1
        public int whCKD = 102;         // WH2
        public int whRubber = 103;      // WH3
        public int whChemical = 111;    // WH4
        public int whRecycle = 115;     // WH8
        public int whGabbage = 116;     // WH9
        public int whScrap = 117;       // WH10
        public int whStationary = 118;  // WH11

        public int _dgv_Invent_Resin_Qty_Width, _dgv_Invent_Resin_Lst_Width;
        public int _dgv_Invent_CKD_Qty_Width, _dgv_Invent_CKD_Lst_Width;
        public int _dgv_Invent_Rubber_Qty_Width, _dgv_Invent_Rubber_Lst_Width;
        public int _dgv_Invent_Chemical_Qty_Width, _dgv_Invent_Chemical_Lst_Width;
        public int _dgv_Invent_FG_Qty_Width, _dgv_Invent_FG_Lst_Width;

        string _idx = "";

        public string _msgText;
        public int _msgType;

        public DateTime _dt;
        Timer timer = new Timer();


        public frmInventoryBeginOfDay()
        {
            InitializeComponent();
        }

        private void frmInventoryBeginOfDay_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            _dgv_Invent_Resin_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_Resin_Qty);
            _dgv_Invent_Resin_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_Resin_Lst);
            _dgv_Invent_CKD_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_CKD_Qty);
            _dgv_Invent_CKD_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_CKD_Lst);
            _dgv_Invent_Rubber_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_Rubber_Qty);
            _dgv_Invent_Rubber_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_Rubber_Lst);
            _dgv_Invent_Chemical_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_Chemical_Qty);
            _dgv_Invent_Chemical_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_Chemical_Lst);
            _dgv_Invent_FG_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_FG_Qty);
            _dgv_Invent_FG_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_FG_Lst);

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

            init_Invent_Resin();
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            init_Invent();
            switch (tab)
            {
                case 0:
                    init_Invent_Resin();
                    break;
                case 1:
                    init_Invent_CKD();
                    break;
                case 2:
                    init_Invent_Rubber();
                    break;
                case 3:
                    init_Invent_Chemical();
                    break;
                case 4:
                    init_Invent_FG();
                    break;
                case 5:
                    break;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl2.SelectedIndex;
            switch (tab)
            {
                case 0:
                    init_Invent();
                    break;
                case 1:
                    pnl_Locate.Controls.Clear();
                    if (!pnl_Locate.Controls.Contains(Ctrl.uc_Warehouse_Change_Sublocate.Instance))
                    {
                        pnl_Locate.Controls.Add(Ctrl.uc_Warehouse_Change_Sublocate.Instance);
                    }
                    Ctrl.uc_Warehouse_Change_Sublocate.Instance.Dock = DockStyle.Fill;
                    Ctrl.uc_Warehouse_Change_Sublocate.Instance.BringToFront();
                    break;
                case 2:
                    init_Params();
                    MessageBox.Show("Chức năng đang được phát triển, vui lòng thử lại sau.");
                    tabControl2.SelectedIndex = 0;
                    break;
            }
        }

        public void init_Invent()
        {
            dgv_Invent_Resin_Qty.DataSource = "";
            dgv_Invent_Resin_Lst.DataSource = "";
            dgv_Invent_CKD_Qty.DataSource = "";
            dgv_Invent_CKD_Lst.DataSource = "";
            dgv_Invent_Rubber_Qty.DataSource = "";
            dgv_Invent_Rubber_Lst.DataSource = "";
            dgv_Invent_Chemical_Qty.DataSource = "";
            dgv_Invent_Chemical_Lst.DataSource = "";
            dgv_Invent_FG_Qty.DataSource = "";
            dgv_Invent_FG_Lst.DataSource = "";

            tlp_TermStatus.Enabled = false;
            rdb_Invent_FG_LongTerm.Checked = false;
            rdb_Invent_FG_ShortTerm.Checked = false;
        }

        public void init_Params()
        {

        }


        #region INVENTTORY RESIN

        public void init_Invent_Resin()
        {
            dtp_Invent_Resin_Date.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            btn_Invent_Resin_Save.Enabled = false;
        }

        public void init_Invent_Resin_Qty()
        {
            int whId = whResin;
            DateTime date = dtp_Invent_Resin_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_Resin_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_Resin_Qty);
            dgv_Invent_Resin_Qty.DataSource = dt;

            //dgv_Invent_Resin_Qty.Columns[0].Width = 25 * _dgv_Invent_Resin_Qty_Width / 100;    // idx
            //dgv_Invent_Resin_Qty.Columns[1].Width = 25 * _dgv_Invent_Resin_Qty_Width / 100;    // inventDate
            //dgv_Invent_Resin_Qty.Columns[2].Width = 25 * _dgv_Invent_Resin_Qty_Width / 100;    // matIDx
            dgv_Invent_Resin_Qty.Columns[3].Width = 70 * _dgv_Invent_Resin_Qty_Width / 100;    // matName
            dgv_Invent_Resin_Qty.Columns[4].Width = 30 * _dgv_Invent_Resin_Qty_Width / 100;    // yesterday

            dgv_Invent_Resin_Qty.Columns[0].Visible = false;
            dgv_Invent_Resin_Qty.Columns[1].Visible = false;
            dgv_Invent_Resin_Qty.Columns[2].Visible = false;
            dgv_Invent_Resin_Qty.Columns[3].Visible = true;
            dgv_Invent_Resin_Qty.Columns[4].Visible = true;

            dgv_Invent_Resin_Qty.Columns[0].ReadOnly = true;
            dgv_Invent_Resin_Qty.Columns[1].ReadOnly = true;
            dgv_Invent_Resin_Qty.Columns[2].ReadOnly = true;
            dgv_Invent_Resin_Qty.Columns[3].ReadOnly = true;
            dgv_Invent_Resin_Qty.Columns[4].ReadOnly = false;

            cls.fnFormatDatagridview(dgv_Invent_Resin_Qty, 12, 30);

            this.dgv_Invent_Resin_Qty.Scroll += new ScrollEventHandler(dgv_Invent_Resin_Qty_Scroll);

            //dt.Dispose();
            //GC.Collect();
        }

        public void init_Invent_Resin_List()
        {
            int whId = whResin;
            DateTime date = dtp_Invent_Resin_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_List_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_Resin_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_Resin_Lst);
            dgv_Invent_Resin_Lst.DataSource = dt;

            //dgv_Invent_Resin_Lst.Columns[0].Width = 25 * _dgv_Invent_Resin_Lst_Width / 100;    // idx           // idx
            dgv_Invent_Resin_Lst.Columns[1].Width = 10 * _dgv_Invent_Resin_Lst_Width / 100;    // inventDate    // inventDate
            //dgv_Invent_Resin_Lst.Columns[2].Width = 25 * _dgv_Invent_Resin_Lst_Width / 100;    // matIDx        // prodId
            dgv_Invent_Resin_Lst.Columns[3].Width = 25 * _dgv_Invent_Resin_Lst_Width / 100;    // matName       // partname
            dgv_Invent_Resin_Lst.Columns[4].Width = 17 * _dgv_Invent_Resin_Lst_Width / 100;    // matCode       // partcode
            dgv_Invent_Resin_Lst.Columns[5].Width = 8 * _dgv_Invent_Resin_Lst_Width / 100;    // matUnit       // uom
            dgv_Invent_Resin_Lst.Columns[6].Width = 8 * _dgv_Invent_Resin_Lst_Width / 100;    // yesterday     // inventoryBeginOfDay
            dgv_Invent_Resin_Lst.Columns[7].Width = 8 * _dgv_Invent_Resin_Lst_Width / 100;    // sumIN         // produceScanIn
            dgv_Invent_Resin_Lst.Columns[8].Width = 8 * _dgv_Invent_Resin_Lst_Width / 100;    // sumReturn     // returnOK
            dgv_Invent_Resin_Lst.Columns[9].Width = 8 * _dgv_Invent_Resin_Lst_Width / 100;    // sumOut        // deliveryScanOut
            dgv_Invent_Resin_Lst.Columns[10].Width = 8 * _dgv_Invent_Resin_Lst_Width / 100;    // inventory     // inventoryEndOfDay
            //dgv_Invent_Resin_Lst.Columns[11].Width = 25 * _dgv_Invent_Resin_Lst_Width / 100;    // lastUpdate    // dateadd

            dgv_Invent_Resin_Lst.Columns[0].Visible = false;
            dgv_Invent_Resin_Lst.Columns[1].Visible = true;
            dgv_Invent_Resin_Lst.Columns[2].Visible = false;
            dgv_Invent_Resin_Lst.Columns[3].Visible = true;
            dgv_Invent_Resin_Lst.Columns[4].Visible = true;
            dgv_Invent_Resin_Lst.Columns[5].Visible = true;
            dgv_Invent_Resin_Lst.Columns[6].Visible = true;
            dgv_Invent_Resin_Lst.Columns[7].Visible = true;
            dgv_Invent_Resin_Lst.Columns[8].Visible = true;
            dgv_Invent_Resin_Lst.Columns[9].Visible = true;
            dgv_Invent_Resin_Lst.Columns[10].Visible = true;
            dgv_Invent_Resin_Lst.Columns[11].Visible = false;

            dgv_Invent_Resin_Lst.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Invent_Resin_Lst, 12, 30);
            dgv_Invent_Resin_Lst.ScrollBars = ScrollBars.None;

            //dt.Dispose();
            //GC.Collect();
        }

        private void btn_Invent_Resin_Load_Click(object sender, EventArgs e)
        {
            init_Invent_Resin_Qty();
            init_Invent_Resin_List();

            int row_Qty = dgv_Invent_Resin_Qty.Rows.Count;
            int row_Lst = dgv_Invent_Resin_Lst.Rows.Count;

            btn_Invent_Resin_Save.Enabled = (row_Qty > 0 && row_Lst > 0 && dtp_Invent_Resin_Date.Value.Date == _dt.Date) ? true : false;
        }

        private void dgv_Invent_Resin_Qty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Invent_Resin_Qty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Invent_Resin_Qty, e);
            }
        }

        private void dgv_Invent_Resin_Qty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_Resin_Lst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string inventQty = "";
            decimal _inventQty = 0;
            foreach (DataGridViewRow row in dgv_Invent_Resin_Lst.Rows)
            {
                inventQty = row.Cells[6].Value.ToString();
                _inventQty = (inventQty != "" && inventQty != null) ? Convert.ToDecimal(inventQty) : 0;

                if (_inventQty == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[6].Style.BackColor = Color.DodgerBlue;
                    row.Cells[6].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgv_Invent_Resin_Lst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv_Invent_Resin_Lst.ClearSelection();
            }
        }

        private void dgv_Invent_Resin_Lst_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_Resin_Qty_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnQty_Resin_KeyPress);
            if (dgv_Invent_Resin_Qty.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnQty_Resin_KeyPress);
                }
            }
        }

        private void ColumnQty_Resin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void dgv_Invent_Resin_Qty_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dgv_Invent_Resin_Lst.FirstDisplayedScrollingRowIndex = dgv_Invent_Resin_Qty.FirstDisplayedScrollingRowIndex;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btn_Invent_Resin_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    int whId = whResin;
                    string idx = "", newQty = "";
                    foreach (DataGridViewRow row in dgv_Invent_Resin_Qty.Rows)
                    {
                        idx = row.Cells[0].Value.ToString();
                        newQty = row.Cells[4].Value.ToString();

                        string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_UpdItem_V2o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@whId";
                        sParams[0].Value = whId;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@idx";
                        sParams[1].Value = idx;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Decimal;
                        sParams[2].ParameterName = "@newQty";
                        sParams[2].Value = newQty;

                        cls.fnUpdDel(sql, sParams);
                    }

                    init_Invent_Resin_List();
                    init_Invent_Resin_Qty();

                    _msgText = "Update Resin inventory successful.";
                    _msgType = 1;
                }
                catch (Exception ex)
                {
                    _msgText = "Has problem occured, please try/check again inputed data";
                    _msgType = 2;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion


        #region INVENTTORY CKD


        public void init_Invent_CKD()
        {
            dtp_Invent_CKD_Date.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            btn_Invent_CKD_Save.Enabled = false;
        }

        public void init_Invent_CKD_Qty()
        {
            int whId = whCKD;
            DateTime date = dtp_Invent_CKD_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_CKD_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_CKD_Qty);
            dgv_Invent_CKD_Qty.DataSource = dt;

            //dgv_Invent_CKD_Qty.Columns[0].Width = 25 * _dgv_Invent_CKD_Qty_Width / 100;    // idx
            //dgv_Invent_CKD_Qty.Columns[1].Width = 25 * _dgv_Invent_CKD_Qty_Width / 100;    // inventDate
            //dgv_Invent_CKD_Qty.Columns[2].Width = 25 * _dgv_Invent_CKD_Qty_Width / 100;    // matIDx
            dgv_Invent_CKD_Qty.Columns[3].Width = 70 * _dgv_Invent_CKD_Qty_Width / 100;    // matName
            dgv_Invent_CKD_Qty.Columns[4].Width = 30 * _dgv_Invent_CKD_Qty_Width / 100;    // yesterday

            dgv_Invent_CKD_Qty.Columns[0].Visible = false;
            dgv_Invent_CKD_Qty.Columns[1].Visible = false;
            dgv_Invent_CKD_Qty.Columns[2].Visible = false;
            dgv_Invent_CKD_Qty.Columns[3].Visible = true;
            dgv_Invent_CKD_Qty.Columns[4].Visible = true;

            dgv_Invent_CKD_Qty.Columns[0].ReadOnly = true;
            dgv_Invent_CKD_Qty.Columns[1].ReadOnly = true;
            dgv_Invent_CKD_Qty.Columns[2].ReadOnly = true;
            dgv_Invent_CKD_Qty.Columns[3].ReadOnly = true;
            dgv_Invent_CKD_Qty.Columns[4].ReadOnly = false;

            cls.fnFormatDatagridview(dgv_Invent_CKD_Qty, 12, 30);

            this.dgv_Invent_CKD_Qty.Scroll += new ScrollEventHandler(dgv_Invent_CKD_Qty_Scroll);

            //dt.Dispose();
            //GC.Collect();
        }

        public void init_Invent_CKD_List()
        {
            int whId = whCKD;
            DateTime date = dtp_Invent_CKD_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_List_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_CKD_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_CKD_Lst);
            dgv_Invent_CKD_Lst.DataSource = dt;

            //dgv_Invent_CKD_Lst.Columns[0].Width = 25 * _dgv_Invent_CKD_Lst_Width / 100;    // idx           // idx
            dgv_Invent_CKD_Lst.Columns[1].Width = 10 * _dgv_Invent_CKD_Lst_Width / 100;    // inventDate    // inventDate
            //dgv_Invent_CKD_Lst.Columns[2].Width = 25 * _dgv_Invent_CKD_Lst_Width / 100;    // matIDx        // prodId
            dgv_Invent_CKD_Lst.Columns[3].Width = 25 * _dgv_Invent_CKD_Lst_Width / 100;    // matName       // partname
            dgv_Invent_CKD_Lst.Columns[4].Width = 17 * _dgv_Invent_CKD_Lst_Width / 100;    // matCode       // partcode
            dgv_Invent_CKD_Lst.Columns[5].Width = 8 * _dgv_Invent_CKD_Lst_Width / 100;    // matUnit       // uom
            dgv_Invent_CKD_Lst.Columns[6].Width = 8 * _dgv_Invent_CKD_Lst_Width / 100;    // yesterday     // inventoryBeginOfDay
            dgv_Invent_CKD_Lst.Columns[7].Width = 8 * _dgv_Invent_CKD_Lst_Width / 100;    // sumIN         // produceScanIn
            dgv_Invent_CKD_Lst.Columns[8].Width = 8 * _dgv_Invent_CKD_Lst_Width / 100;    // sumReturn     // returnOK
            dgv_Invent_CKD_Lst.Columns[9].Width = 8 * _dgv_Invent_CKD_Lst_Width / 100;    // sumOut        // deliveryScanOut
            dgv_Invent_CKD_Lst.Columns[10].Width = 8 * _dgv_Invent_CKD_Lst_Width / 100;    // inventory     // inventoryEndOfDay
            //dgv_Invent_CKD_Lst.Columns[11].Width = 25 * _dgv_Invent_CKD_Lst_Width / 100;    // lastUpdate    // dateadd

            dgv_Invent_CKD_Lst.Columns[0].Visible = false;
            dgv_Invent_CKD_Lst.Columns[1].Visible = true;
            dgv_Invent_CKD_Lst.Columns[2].Visible = false;
            dgv_Invent_CKD_Lst.Columns[3].Visible = true;
            dgv_Invent_CKD_Lst.Columns[4].Visible = true;
            dgv_Invent_CKD_Lst.Columns[5].Visible = true;
            dgv_Invent_CKD_Lst.Columns[6].Visible = true;
            dgv_Invent_CKD_Lst.Columns[7].Visible = true;
            dgv_Invent_CKD_Lst.Columns[8].Visible = true;
            dgv_Invent_CKD_Lst.Columns[9].Visible = true;
            dgv_Invent_CKD_Lst.Columns[10].Visible = true;
            dgv_Invent_CKD_Lst.Columns[11].Visible = false;

            dgv_Invent_CKD_Lst.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Invent_CKD_Lst, 12, 30);
            dgv_Invent_CKD_Lst.ScrollBars = ScrollBars.None;

            //dt.Dispose();
            //GC.Collect();
        }

        private void btn_Invent_CKD_Load_Click(object sender, EventArgs e)
        {
            init_Invent_CKD_Qty();
            init_Invent_CKD_List();

            int row_Qty = dgv_Invent_CKD_Qty.Rows.Count;
            int row_Lst = dgv_Invent_CKD_Lst.Rows.Count;

            btn_Invent_CKD_Save.Enabled = (row_Qty > 0 && row_Lst > 0 && dtp_Invent_CKD_Date.Value.Date == _dt.Date) ? true : false;
        }

        private void dgv_Invent_CKD_Qty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Invent_CKD_Qty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Invent_CKD_Qty, e);
            }
        }

        private void dgv_Invent_CKD_Qty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_CKD_Lst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string inventQty = "";
            decimal _inventQty = 0;
            foreach (DataGridViewRow row in dgv_Invent_CKD_Lst.Rows)
            {
                inventQty = row.Cells[6].Value.ToString();
                _inventQty = (inventQty != "" && inventQty != null) ? Convert.ToDecimal(inventQty) : 0;

                if (_inventQty == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[6].Style.BackColor = Color.DodgerBlue;
                    row.Cells[6].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgv_Invent_CKD_Lst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv_Invent_CKD_Lst.ClearSelection();
            }
        }

        private void dgv_Invent_CKD_Lst_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_CKD_Qty_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnQty_CKD_KeyPress);
            if (dgv_Invent_CKD_Qty.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnQty_CKD_KeyPress);
                }
            }
        }

        private void ColumnQty_CKD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void dgv_Invent_CKD_Qty_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dgv_Invent_CKD_Lst.FirstDisplayedScrollingRowIndex = dgv_Invent_CKD_Qty.FirstDisplayedScrollingRowIndex;
            }
            catch
            {

            }
            finally
            {

            }
            
        }

        private void btn_Invent_CKD_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    int whId = whCKD;
                    string idx = "", newQty = "";
                    foreach (DataGridViewRow row in dgv_Invent_CKD_Qty.Rows)
                    {
                        idx = row.Cells[0].Value.ToString();
                        newQty = row.Cells[4].Value.ToString();

                        string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_UpdItem_V2o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@whId";
                        sParams[0].Value = whId;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@idx";
                        sParams[1].Value = idx;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Decimal;
                        sParams[2].ParameterName = "@newQty";
                        sParams[2].Value = newQty;

                        cls.fnUpdDel(sql, sParams);
                    }

                    init_Invent_CKD_List();
                    init_Invent_CKD_Qty();

                    _msgText = "Update CKD inventory successful.";
                    _msgType = 1;
                }
                catch (Exception ex)
                {
                    _msgText = "Has problem occured, please try/check again inputed data";
                    _msgType = 2;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion


        #region INVENTTORY RUBBER

        public void init_Invent_Rubber()
        {
            dtp_Invent_Rubber_Date.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            btn_Invent_Rubber_Save.Enabled = false;
        }

        public void init_Invent_Rubber_Qty()
        {
            int whId = whRubber;
            DateTime date = dtp_Invent_Rubber_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_Rubber_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_Rubber_Qty);
            dgv_Invent_Rubber_Qty.DataSource = dt;

            //dgv_Invent_Rubber_Qty.Columns[0].Width = 25 * _dgv_Invent_Rubber_Qty_Width / 100;    // idx
            //dgv_Invent_Rubber_Qty.Columns[1].Width = 25 * _dgv_Invent_Rubber_Qty_Width / 100;    // inventDate
            //dgv_Invent_Rubber_Qty.Columns[2].Width = 25 * _dgv_Invent_Rubber_Qty_Width / 100;    // matIDx
            dgv_Invent_Rubber_Qty.Columns[3].Width = 70 * _dgv_Invent_Rubber_Qty_Width / 100;    // matName
            dgv_Invent_Rubber_Qty.Columns[4].Width = 30 * _dgv_Invent_Rubber_Qty_Width / 100;    // yesterday

            dgv_Invent_Rubber_Qty.Columns[0].Visible = false;
            dgv_Invent_Rubber_Qty.Columns[1].Visible = false;
            dgv_Invent_Rubber_Qty.Columns[2].Visible = false;
            dgv_Invent_Rubber_Qty.Columns[3].Visible = true;
            dgv_Invent_Rubber_Qty.Columns[4].Visible = true;

            dgv_Invent_Rubber_Qty.Columns[0].ReadOnly = true;
            dgv_Invent_Rubber_Qty.Columns[1].ReadOnly = true;
            dgv_Invent_Rubber_Qty.Columns[2].ReadOnly = true;
            dgv_Invent_Rubber_Qty.Columns[3].ReadOnly = true;
            dgv_Invent_Rubber_Qty.Columns[4].ReadOnly = false;

            cls.fnFormatDatagridview(dgv_Invent_Rubber_Qty, 12, 30);

            this.dgv_Invent_Rubber_Qty.Scroll += new ScrollEventHandler(dgv_Invent_Rubber_Qty_Scroll);

            //dt.Dispose();
            //GC.Collect();
        }

        public void init_Invent_Rubber_List()
        {
            int whId = whRubber;
            DateTime date = dtp_Invent_Rubber_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_List_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_Rubber_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_Rubber_Lst);
            dgv_Invent_Rubber_Lst.DataSource = dt;

            //dgv_Invent_Rubber_Lst.Columns[0].Width = 25 * _dgv_Invent_Rubber_Lst_Width / 100;    // idx           // idx
            dgv_Invent_Rubber_Lst.Columns[1].Width = 10 * _dgv_Invent_Rubber_Lst_Width / 100;    // inventDate    // inventDate
            //dgv_Invent_Rubber_Lst.Columns[2].Width = 25 * _dgv_Invent_Rubber_Lst_Width / 100;    // matIDx        // prodId
            dgv_Invent_Rubber_Lst.Columns[3].Width = 25 * _dgv_Invent_Rubber_Lst_Width / 100;    // matName       // partname
            dgv_Invent_Rubber_Lst.Columns[4].Width = 17 * _dgv_Invent_Rubber_Lst_Width / 100;    // matCode       // partcode
            dgv_Invent_Rubber_Lst.Columns[5].Width = 8 * _dgv_Invent_Rubber_Lst_Width / 100;    // matUnit       // uom
            dgv_Invent_Rubber_Lst.Columns[6].Width = 8 * _dgv_Invent_Rubber_Lst_Width / 100;    // yesterday     // inventoryBeginOfDay
            dgv_Invent_Rubber_Lst.Columns[7].Width = 8 * _dgv_Invent_Rubber_Lst_Width / 100;    // sumIN         // produceScanIn
            dgv_Invent_Rubber_Lst.Columns[8].Width = 8 * _dgv_Invent_Rubber_Lst_Width / 100;    // sumReturn     // returnOK
            dgv_Invent_Rubber_Lst.Columns[9].Width = 8 * _dgv_Invent_Rubber_Lst_Width / 100;    // sumOut        // deliveryScanOut
            dgv_Invent_Rubber_Lst.Columns[10].Width = 8 * _dgv_Invent_Rubber_Lst_Width / 100;    // inventory     // inventoryEndOfDay
            //dgv_Invent_Rubber_Lst.Columns[11].Width = 25 * _dgv_Invent_Rubber_Lst_Width / 100;    // lastUpdate    // dateadd

            dgv_Invent_Rubber_Lst.Columns[0].Visible = false;
            dgv_Invent_Rubber_Lst.Columns[1].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[2].Visible = false;
            dgv_Invent_Rubber_Lst.Columns[3].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[4].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[5].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[6].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[7].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[8].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[9].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[10].Visible = true;
            dgv_Invent_Rubber_Lst.Columns[11].Visible = false;

            dgv_Invent_Rubber_Lst.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Invent_Rubber_Lst, 12, 30);
            dgv_Invent_Rubber_Lst.ScrollBars = ScrollBars.None;

            //dt.Dispose();
            //GC.Collect();
        }

        private void btn_Invent_Rubber_Load_Click(object sender, EventArgs e)
        {
            init_Invent_Rubber_Qty();
            init_Invent_Rubber_List();

            int row_Qty = dgv_Invent_Rubber_Qty.Rows.Count;
            int row_Lst = dgv_Invent_Rubber_Lst.Rows.Count;

            btn_Invent_Rubber_Save.Enabled = (row_Qty > 0 && row_Lst > 0 && dtp_Invent_Rubber_Date.Value.Date == _dt.Date) ? true : false;
        }

        private void dgv_Invent_Rubber_Qty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Invent_Rubber_Qty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Invent_Rubber_Qty, e);
            }
        }

        private void dgv_Invent_Rubber_Qty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_Rubber_Lst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string inventQty = "";
            decimal _inventQty = 0;
            foreach (DataGridViewRow row in dgv_Invent_Rubber_Lst.Rows)
            {
                inventQty = row.Cells[6].Value.ToString();
                _inventQty = (inventQty != "" && inventQty != null) ? Convert.ToDecimal(inventQty) : 0;

                if (_inventQty == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[6].Style.BackColor = Color.DodgerBlue;
                    row.Cells[6].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgv_Invent_Rubber_Lst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv_Invent_Rubber_Lst.ClearSelection();
            }
        }

        private void dgv_Invent_Rubber_Lst_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_Rubber_Qty_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnQty_Rubber_KeyPress);
            if (dgv_Invent_Rubber_Qty.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnQty_Rubber_KeyPress);
                }
            }
        }

        private void ColumnQty_Rubber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void dgv_Invent_Rubber_Qty_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dgv_Invent_Rubber_Lst.FirstDisplayedScrollingRowIndex = dgv_Invent_Rubber_Qty.FirstDisplayedScrollingRowIndex;
            }
            catch
            {

            }
            finally
            {

            }

        }

        private void btn_Invent_Rubber_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    int whId = whRubber;
                    string idx = "", newQty = "";
                    foreach (DataGridViewRow row in dgv_Invent_Rubber_Qty.Rows)
                    {
                        idx = row.Cells[0].Value.ToString();
                        newQty = row.Cells[4].Value.ToString();

                        string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_UpdItem_V2o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@whId";
                        sParams[0].Value = whId;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@idx";
                        sParams[1].Value = idx;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Decimal;
                        sParams[2].ParameterName = "@newQty";
                        sParams[2].Value = newQty;

                        cls.fnUpdDel(sql, sParams);
                    }

                    init_Invent_Rubber_List();
                    init_Invent_Rubber_Qty();

                    _msgText = "Update Rubber inventory successful.";
                    _msgType = 1;
                }
                catch (Exception ex)
                {
                    _msgText = "Has problem occured, please try/check again inputed data";
                    _msgType = 2;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion


        #region INVENTTORY CHEMICAL

        public void init_Invent_Chemical()
        {
            dtp_Invent_Chemical_Date.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            btn_Invent_Chemical_Save.Enabled = false;
        }

        public void init_Invent_Chemical_Qty()
        {
            int whId = whChemical;
            DateTime date = dtp_Invent_Chemical_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_Chemical_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_Chemical_Qty);
            dgv_Invent_Chemical_Qty.DataSource = dt;

            //dgv_Invent_Chemical_Qty.Columns[0].Width = 25 * _dgv_Invent_Chemical_Qty_Width / 100;    // idx
            //dgv_Invent_Chemical_Qty.Columns[1].Width = 25 * _dgv_Invent_Chemical_Qty_Width / 100;    // inventDate
            //dgv_Invent_Chemical_Qty.Columns[2].Width = 25 * _dgv_Invent_Chemical_Qty_Width / 100;    // matIDx
            dgv_Invent_Chemical_Qty.Columns[3].Width = 70 * _dgv_Invent_Chemical_Qty_Width / 100;    // matName
            dgv_Invent_Chemical_Qty.Columns[4].Width = 30 * _dgv_Invent_Chemical_Qty_Width / 100;    // yesterday

            dgv_Invent_Chemical_Qty.Columns[0].Visible = false;
            dgv_Invent_Chemical_Qty.Columns[1].Visible = false;
            dgv_Invent_Chemical_Qty.Columns[2].Visible = false;
            dgv_Invent_Chemical_Qty.Columns[3].Visible = true;
            dgv_Invent_Chemical_Qty.Columns[4].Visible = true;

            dgv_Invent_Chemical_Qty.Columns[0].ReadOnly = true;
            dgv_Invent_Chemical_Qty.Columns[1].ReadOnly = true;
            dgv_Invent_Chemical_Qty.Columns[2].ReadOnly = true;
            dgv_Invent_Chemical_Qty.Columns[3].ReadOnly = true;
            dgv_Invent_Chemical_Qty.Columns[4].ReadOnly = false;

            cls.fnFormatDatagridview(dgv_Invent_Chemical_Qty, 12, 30);

            this.dgv_Invent_Chemical_Qty.Scroll += new ScrollEventHandler(dgv_Invent_Chemical_Qty_Scroll);

            //dt.Dispose();
            //GC.Collect();
        }

        public void init_Invent_Chemical_List()
        {
            int whId = whChemical;
            DateTime date = dtp_Invent_Chemical_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_List_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_Chemical_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_Chemical_Lst);
            dgv_Invent_Chemical_Lst.DataSource = dt;

            //dgv_Invent_Chemical_Lst.Columns[0].Width = 25 * _dgv_Invent_Chemical_Lst_Width / 100;    // idx           // idx
            dgv_Invent_Chemical_Lst.Columns[1].Width = 10 * _dgv_Invent_Chemical_Lst_Width / 100;    // inventDate    // inventDate
            //dgv_Invent_Chemical_Lst.Columns[2].Width = 25 * _dgv_Invent_Chemical_Lst_Width / 100;    // matIDx        // prodId
            dgv_Invent_Chemical_Lst.Columns[3].Width = 25 * _dgv_Invent_Chemical_Lst_Width / 100;    // matName       // partname
            dgv_Invent_Chemical_Lst.Columns[4].Width = 17 * _dgv_Invent_Chemical_Lst_Width / 100;    // matCode       // partcode
            dgv_Invent_Chemical_Lst.Columns[5].Width = 8 * _dgv_Invent_Chemical_Lst_Width / 100;    // matUnit       // uom
            dgv_Invent_Chemical_Lst.Columns[6].Width = 8 * _dgv_Invent_Chemical_Lst_Width / 100;    // yesterday     // inventoryBeginOfDay
            dgv_Invent_Chemical_Lst.Columns[7].Width = 8 * _dgv_Invent_Chemical_Lst_Width / 100;    // sumIN         // produceScanIn
            dgv_Invent_Chemical_Lst.Columns[8].Width = 8 * _dgv_Invent_Chemical_Lst_Width / 100;    // sumReturn     // returnOK
            dgv_Invent_Chemical_Lst.Columns[9].Width = 8 * _dgv_Invent_Chemical_Lst_Width / 100;    // sumOut        // deliveryScanOut
            dgv_Invent_Chemical_Lst.Columns[10].Width = 8 * _dgv_Invent_Chemical_Lst_Width / 100;    // inventory     // inventoryEndOfDay
            //dgv_Invent_Chemical_Lst.Columns[11].Width = 25 * _dgv_Invent_Chemical_Lst_Width / 100;    // lastUpdate    // dateadd

            dgv_Invent_Chemical_Lst.Columns[0].Visible = false;
            dgv_Invent_Chemical_Lst.Columns[1].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[2].Visible = false;
            dgv_Invent_Chemical_Lst.Columns[3].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[4].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[5].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[6].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[7].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[8].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[9].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[10].Visible = true;
            dgv_Invent_Chemical_Lst.Columns[11].Visible = false;

            dgv_Invent_Chemical_Lst.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Invent_Chemical_Lst, 12, 30);
            dgv_Invent_Chemical_Lst.ScrollBars = ScrollBars.None;

            //dt.Dispose();
            //GC.Collect();
        }

        private void btn_Invent_Chemical_Load_Click(object sender, EventArgs e)
        {
            init_Invent_Chemical_Qty();
            init_Invent_Chemical_List();

            int row_Qty = dgv_Invent_Chemical_Qty.Rows.Count;
            int row_Lst = dgv_Invent_Chemical_Lst.Rows.Count;

            btn_Invent_Chemical_Save.Enabled = (row_Qty > 0 && row_Lst > 0 && dtp_Invent_Chemical_Date.Value.Date == _dt.Date) ? true : false;
        }

        private void dgv_Invent_Chemical_Qty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Invent_Chemical_Qty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Invent_Chemical_Qty, e);
            }
        }

        private void dgv_Invent_Chemical_Qty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_Chemical_Lst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string inventQty = "";
            decimal _inventQty = 0;
            foreach (DataGridViewRow row in dgv_Invent_Chemical_Lst.Rows)
            {
                inventQty = row.Cells[6].Value.ToString();
                _inventQty = (inventQty != "" && inventQty != null) ? Convert.ToDecimal(inventQty) : 0;

                if (_inventQty == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[6].Style.BackColor = Color.DodgerBlue;
                    row.Cells[6].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgv_Invent_Chemical_Lst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv_Invent_Chemical_Lst.ClearSelection();
            }
        }

        private void dgv_Invent_Chemical_Lst_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_Chemical_Qty_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnQty_Chemical_KeyPress);
            if (dgv_Invent_Chemical_Qty.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnQty_Chemical_KeyPress);
                }
            }
        }

        private void ColumnQty_Chemical_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void dgv_Invent_Chemical_Qty_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dgv_Invent_Chemical_Lst.FirstDisplayedScrollingRowIndex = dgv_Invent_Chemical_Qty.FirstDisplayedScrollingRowIndex;
            }
            catch
            {

            }
            finally
            {

            }

        }

        private void btn_Invent_Chemical_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    int whId = whChemical;
                    string idx = "", newQty = "";
                    foreach (DataGridViewRow row in dgv_Invent_Chemical_Qty.Rows)
                    {
                        idx = row.Cells[0].Value.ToString();
                        newQty = row.Cells[4].Value.ToString();

                        string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_UpdItem_V2o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@whId";
                        sParams[0].Value = whId;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@idx";
                        sParams[1].Value = idx;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Decimal;
                        sParams[2].ParameterName = "@newQty";
                        sParams[2].Value = newQty;

                        cls.fnUpdDel(sql, sParams);
                    }

                    init_Invent_Chemical_List();
                    init_Invent_Chemical_Qty();

                    _msgText = "Update Chemical inventory successful.";
                    _msgType = 1;
                }
                catch (Exception ex)
                {
                    _msgText = "Has problem occured, please try/check again inputed data";
                    _msgType = 2;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #endregion


        #region INVENTTORY FINISH GOODS

        public void init_Invent_FG()
        {
            dtp_Invent_FG_Date.MaxDate = new DateTime(_dt.Year, _dt.Month, _dt.Day);
            btn_Invent_FG_Save.Enabled = false;
        }

        public void init_Invent_FG_Qty()
        {
            int whId = whFG;
            DateTime date = dtp_Invent_FG_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_FG_Qty_Width = cls.fnGetDataGridWidth(dgv_Invent_FG_Qty);
            dgv_Invent_FG_Qty.DataSource = dt;

            //dgv_Invent_FG_Qty.Columns[0].Width = 25 * _dgv_Invent_FG_Qty_Width / 100;    // idx
            //dgv_Invent_FG_Qty.Columns[1].Width = 25 * _dgv_Invent_FG_Qty_Width / 100;    // inventDate
            //dgv_Invent_FG_Qty.Columns[2].Width = 25 * _dgv_Invent_FG_Qty_Width / 100;    // matIDx
            dgv_Invent_FG_Qty.Columns[3].Width = 70 * _dgv_Invent_FG_Qty_Width / 100;    // matName
            dgv_Invent_FG_Qty.Columns[4].Width = 30 * _dgv_Invent_FG_Qty_Width / 100;    // yesterday
            //dgv_Invent_FG_Qty.Columns[5].Width = 30 * _dgv_Invent_FG_Qty_Width / 100;    // LongTerm

            dgv_Invent_FG_Qty.Columns[0].Visible = false;
            dgv_Invent_FG_Qty.Columns[1].Visible = false;
            dgv_Invent_FG_Qty.Columns[2].Visible = false;
            dgv_Invent_FG_Qty.Columns[3].Visible = true;
            dgv_Invent_FG_Qty.Columns[4].Visible = true;
            dgv_Invent_FG_Qty.Columns[5].Visible = false;

            dgv_Invent_FG_Qty.Columns[0].ReadOnly = true;
            dgv_Invent_FG_Qty.Columns[1].ReadOnly = true;
            dgv_Invent_FG_Qty.Columns[2].ReadOnly = true;
            dgv_Invent_FG_Qty.Columns[3].ReadOnly = true;
            dgv_Invent_FG_Qty.Columns[4].ReadOnly = false;
            dgv_Invent_FG_Qty.Columns[3].ReadOnly = true;

            cls.fnFormatDatagridview(dgv_Invent_FG_Qty, 12, 30);

            this.dgv_Invent_FG_Qty.Scroll += new ScrollEventHandler(dgv_Invent_FG_Qty_Scroll);

            //dt.Dispose();
            //GC.Collect();
        }

        public void init_Invent_FG_List()
        {
            int whId = whFG;
            DateTime date = dtp_Invent_FG_Date.Value;

            string sql = "V2o1_BASE_Inventory_BeginOfDate_List_SelItem_V2o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.DateTime;
            sParams[1].ParameterName = "@date";
            sParams[1].Value = date;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Invent_FG_Lst_Width = cls.fnGetDataGridWidth(dgv_Invent_FG_Lst);
            dgv_Invent_FG_Lst.DataSource = dt;

            //dgv_Invent_FG_Lst.Columns[0].Width = 25 * _dgv_Invent_FG_Lst_Width / 100;    // idx           // idx
            dgv_Invent_FG_Lst.Columns[1].Width = 10 * _dgv_Invent_FG_Lst_Width / 100;    // inventDate    // inventDate
            //dgv_Invent_FG_Lst.Columns[2].Width = 25 * _dgv_Invent_FG_Lst_Width / 100;    // matIDx        // prodId
            dgv_Invent_FG_Lst.Columns[3].Width = 25 * _dgv_Invent_FG_Lst_Width / 100;    // matName       // partname
            dgv_Invent_FG_Lst.Columns[4].Width = 17 * _dgv_Invent_FG_Lst_Width / 100;    // matCode       // partcode
            dgv_Invent_FG_Lst.Columns[5].Width = 8 * _dgv_Invent_FG_Lst_Width / 100;    // matUnit       // uom
            dgv_Invent_FG_Lst.Columns[6].Width = 8 * _dgv_Invent_FG_Lst_Width / 100;    // yesterday     // inventoryBeginOfDay
            dgv_Invent_FG_Lst.Columns[7].Width = 8 * _dgv_Invent_FG_Lst_Width / 100;    // sumIN         // produceScanIn
            dgv_Invent_FG_Lst.Columns[8].Width = 8 * _dgv_Invent_FG_Lst_Width / 100;    // sumReturn     // returnOK
            dgv_Invent_FG_Lst.Columns[9].Width = 8 * _dgv_Invent_FG_Lst_Width / 100;    // sumOut        // deliveryScanOut
            dgv_Invent_FG_Lst.Columns[10].Width = 8 * _dgv_Invent_FG_Lst_Width / 100;    // inventory     // inventoryEndOfDay
            //dgv_Invent_FG_Lst.Columns[11].Width = 25 * _dgv_Invent_FG_Lst_Width / 100;    // lastUpdate    // dateadd
            //dgv_Invent_FG_Lst.Columns[12].Width = 25 * _dgv_Invent_FG_Lst_Width / 100;    // lastUpdate    // LongTerm

            dgv_Invent_FG_Lst.Columns[0].Visible = false;
            dgv_Invent_FG_Lst.Columns[1].Visible = true;
            dgv_Invent_FG_Lst.Columns[2].Visible = false;
            dgv_Invent_FG_Lst.Columns[3].Visible = true;
            dgv_Invent_FG_Lst.Columns[4].Visible = true;
            dgv_Invent_FG_Lst.Columns[5].Visible = true;
            dgv_Invent_FG_Lst.Columns[6].Visible = true;
            dgv_Invent_FG_Lst.Columns[7].Visible = true;
            dgv_Invent_FG_Lst.Columns[8].Visible = true;
            dgv_Invent_FG_Lst.Columns[9].Visible = true;
            dgv_Invent_FG_Lst.Columns[10].Visible = true;
            dgv_Invent_FG_Lst.Columns[11].Visible = false;
            dgv_Invent_FG_Lst.Columns[12].Visible = false;

            dgv_Invent_FG_Lst.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            cls.fnFormatDatagridview(dgv_Invent_FG_Lst, 12, 30);
            dgv_Invent_FG_Lst.ScrollBars = ScrollBars.None;

            //dt.Dispose();
            //GC.Collect();
        }

        private void btn_Invent_FG_Load_Click(object sender, EventArgs e)
        {
            init_Invent_FG_Qty();
            init_Invent_FG_List();

            int row_Qty = dgv_Invent_FG_Qty.Rows.Count;
            int row_Lst = dgv_Invent_FG_Lst.Rows.Count;

            btn_Invent_FG_Save.Enabled = (row_Qty > 0 && row_Lst > 0 && dtp_Invent_FG_Date.Value.Date == _dt.Date) ? true : false;
        }

        private void dgv_Invent_FG_Qty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Invent_FG_Qty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Invent_FG_Qty, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Invent_FG_Qty.Rows[e.RowIndex];
                string idx = row.Cells[2].Value.ToString();
                string longterm = row.Cells[5].Value.ToString();
                _idx = idx;
                bool _longterm = Convert.ToBoolean(longterm);
                if (_longterm)
                {
                    rdb_Invent_FG_LongTerm.Checked = true;
                }
                else
                {
                    rdb_Invent_FG_ShortTerm.Checked = true;
                }

                tlp_TermStatus.Enabled = true;
            }
        }

        private void dgv_Invent_FG_Qty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_FG_Lst_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string inventQty = "", longterm = "";
            decimal _inventQty = 0;
            bool _longterm = false;
            foreach (DataGridViewRow row in dgv_Invent_FG_Lst.Rows)
            {
                inventQty = row.Cells[6].Value.ToString();
                _inventQty = (inventQty != "" && inventQty != null) ? Convert.ToDecimal(inventQty) : 0;

                longterm = row.Cells[12].Value.ToString();
                _longterm = (longterm.ToLower() == "true") ? true : false;

                if (_inventQty == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {

                    row.Cells[6].Style.BackColor = (_longterm == true) ? Color.Orange : Color.DodgerBlue;
                    row.Cells[6].Style.ForeColor = Color.White;
                }
            }
        }

        private void dgv_Invent_FG_Lst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgv_Invent_FG_Lst.ClearSelection();
            }
        }

        private void dgv_Invent_FG_Lst_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Invent_FG_Qty_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnQty_FG_KeyPress);
            if (dgv_Invent_FG_Qty.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnQty_FG_KeyPress);
                }
            }
        }

        private void ColumnQty_FG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void dgv_Invent_FG_Qty_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dgv_Invent_FG_Lst.FirstDisplayedScrollingRowIndex = dgv_Invent_FG_Qty.FirstDisplayedScrollingRowIndex;
            }
            catch
            {

            }
            finally
            {

            }

        }

        private void btn_Invent_FG_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    int whId = whFG;
                    string idx = "", newQty = "";
                    foreach (DataGridViewRow row in dgv_Invent_FG_Qty.Rows)
                    {
                        idx = row.Cells[0].Value.ToString();
                        newQty = row.Cells[4].Value.ToString();

                        string sql = "V2o1_BASE_Inventory_BeginOfDate_Quantity_UpdItem_V2o0_Addnew";

                        SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@whId";
                        sParams[0].Value = whId;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.Int;
                        sParams[1].ParameterName = "@idx";
                        sParams[1].Value = idx;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Decimal;
                        sParams[2].ParameterName = "@newQty";
                        sParams[2].Value = newQty;

                        cls.fnUpdDel(sql, sParams);
                    }

                    init_Invent_FG_List();
                    init_Invent_FG_Qty();

                    _msgText = "Update FG inventory successful.";
                    _msgType = 1;
                }
                catch (Exception ex)
                {
                    _msgText = "Has problem occured, please try/check again inputed data";
                    _msgType = 2;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }

        private void btn_Invent_FG_TermSet_Click(object sender, EventArgs e)
        {
            try
            {
                string idx = _idx;
                bool longterm = (rdb_Invent_FG_LongTerm.Checked) ? true : false;
                string sql = "V2o1_BASE_Inventory_BeginOfDate_LongTerm_UpdItem_V2o0_Addnew";

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodIDx";
                sParams[0].Value = idx;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Bit;
                sParams[1].ParameterName = "@longterm";
                sParams[1].Value = longterm;

                cls.fnUpdDel(sql, sParams);

                init_Invent_FG_List();
                init_Invent_FG_Qty();

                _msgText = "Update LongTerm status successful.";
                _msgType = 1;
            }
            catch(Exception ex)
            {
                _msgText = "Has problem occured, please try/check again inputed data";
                _msgType = 2;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cls.fnMessage(tssMessage, _msgText, _msgType);
            }

        }


        #endregion

        private void TssMessage_TextChanged(object sender, EventArgs e)
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

        public void fnc_Set_Status_Message(string _msgText, int _msgCode)
        {
            Color _msgColor = Color.FromKnownColor(KnownColor.Control);
            switch (_msgCode)
            {
                case 0:
                    _msgColor = Color.FromKnownColor(KnownColor.Control);
                    break;
                case 1:
                    _msgColor = Color.LightGreen;
                    break;
                case 2:
                    _msgColor = Color.LightPink;
                    break;
                case 3:
                    _msgColor = Color.Goldenrod;
                    break;
            }

            tssMessage.Text = _msgText;
            tssMessage.BackColor = _msgColor;
        }

    }
}
