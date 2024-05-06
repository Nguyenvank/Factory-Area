using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmWarehouseDefines : Form
    {
        public string _locId = "";
        public string _locSub = "";
        public string _locProd = "";
        public string _oldSub = "";

        public string _WH6ProdId = "";

        public int _dgvWH_LocWidth;
        public int _dgvWH_PakWidth;
        public int _dgvWH_WH6Width;
        public int _dgvWH_FGIWidth;

        public bool connectOK = false;

        public frmWarehouseDefines()
        {
            InitializeComponent();
        }

        private void frmWarehouseDefines_Load(object sender, EventArgs e)
        {
            _dgvWH_LocWidth = cls.fnGetDataGridWidth(dgvWH_Loc) - 20;
            _dgvWH_PakWidth = cls.fnGetDataGridWidth(dgvWH_Pak) - 20;
            _dgvWH_WH6Width = cls.fnGetDataGridWidth(dgvWH_WH6) - 20;
            _dgvWH_FGIWidth = cls.fnGetDataGridWidth(dgvWH_FGI) - 20;

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvWH_LocWidth = cls.fnGetDataGridWidth(dgvWH_Loc) - 20;
            _dgvWH_PakWidth = cls.fnGetDataGridWidth(dgvWH_Pak) - 20;
            _dgvWH_WH6Width = cls.fnGetDataGridWidth(dgvWH_WH6) - 20;
            _dgvWH_FGIWidth = cls.fnGetDataGridWidth(dgvWH_FGI) - 20;

            connectOK = (check.IsConnectedToInternet() == true) ? true : false;

            if(connectOK==true)
            {
                fnGetdate();
            }
        }

        public void init()
        {
            fnGetdate();
            initWH_Loc();
            initWH_Pak();
            initWH_WH6();
            initWH_FGI();
        }

        public void fnGetdate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");

            if (connectOK == true)
            {
                lblDate.ForeColor = Color.Black;
                lblTime.ForeColor = Color.Black;
            }
            else
            {
                lblDate.ForeColor = Color.Red;
                lblTime.ForeColor = Color.Red;
            }
        }

        public void initWH_Loc()
        {
            // WAREHOUSE NAME
            string sqlLoc = "";
            sqlLoc = "V2o1_BASE_Warehouse_Defines_Location_SelItem_Addnew";
            DataTable dtLoc = new DataTable();

            SqlParameter[] sParamsLoc = new SqlParameter[0]; // Parameter count
            //sParamsLine[0] = new SqlParameter();
            //sParamsLine[0].SqlDbType = SqlDbType.VarChar;
            //sParamsLine[0].ParameterName = "lineCode";
            //sParamsLine[0].Value = "";

            dtLoc = cls.ExecuteDataTable(sqlLoc, sParamsLoc);
            cbbLoc_Name.DataSource = dtLoc;
            cbbLoc_Name.DisplayMember = "Name";
            cbbLoc_Name.ValueMember = "LocationId";
            dtLoc.Rows.InsertAt(dtLoc.NewRow(), 0);
            cbbLoc_Name.SelectedIndex = 0;

            // PRODUCT NAME
            string sqlProd = "";
            sqlProd = "V2o1_BASE_Warehouse_Defines_Location_SelProd_Addnew";
            DataTable dtProd = new DataTable();

            SqlParameter[] sParamsProd = new SqlParameter[0]; // Parameter count
            //sParamsLine[0] = new SqlParameter();
            //sParamsLine[0].SqlDbType = SqlDbType.VarChar;
            //sParamsLine[0].ParameterName = "lineCode";
            //sParamsLine[0].Value = "";

            dtProd = cls.ExecuteDataTable(sqlProd, sParamsLoc);
            cbbLoc_Prod.DataSource = dtProd;
            cbbLoc_Prod.DisplayMember = "Name";
            cbbLoc_Prod.ValueMember = "prodId";
            dtProd.Rows.InsertAt(dtProd.NewRow(), 0);
            cbbLoc_Prod.SelectedIndex = 0;

            cbbLoc_Name.Enabled = true;
            txtLoc_Sub.Enabled = false;
            txtLoc_Sub.Text = "";
            cbbLoc_Prod.Enabled = false;

            rdbLocAdd.Enabled = false;
            rdbLocUpd.Enabled = false;
            rdbLocDel.Enabled = false;
            rdbLocAdd.Checked = false;
            rdbLocUpd.Checked = false;
            rdbLocDel.Checked = false;

            dgvWH_Loc.Enabled = false;
            dgvWH_Loc.DataSource = "";

            btnLocSave.Enabled = false;
            btnLocFinish.Enabled = false;
        }

        private void cbbLoc_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string locId = cbbLoc_Name.SelectedValue.ToString();
            _locSub = "";
            _locProd = "";
            cbbLoc_Prod.SelectedIndex = 0;
            dgvWH_Loc.DataSource = "";
            dgvWH_Loc.Refresh();

            if (cbbLoc_Name.SelectedIndex > 0)
            {
                cbbLoc_Name.Enabled = true;

                _locId = locId;

                //if (_locId != "" && _locSub != "")
                //{
                //    rdbLocAdd.Enabled = false;
                //    rdbLocAdd.Checked = false;
                //    rdbLocUpd.Enabled = true;
                //    rdbLocDel.Enabled = false;
                //    rdbLocUpd.Checked = true;
                //    rdbLocDel.Checked = false;

                //    btnLocSave.Enabled = false;
                //    btnLocFinish.Enabled = true;
                //}
                //else
                //{
                //    rdbLocAdd.Enabled = true;
                //    rdbLocAdd.Checked = true;
                //    rdbLocUpd.Enabled = false;
                //    rdbLocDel.Enabled = false;
                //    rdbLocUpd.Checked = false;
                //    rdbLocDel.Checked = false;

                //    btnLocSave.Enabled = true;
                //    btnLocFinish.Enabled = true;
                //}

                cbbLoc_Prod.Enabled = true;
            }
            else
            {
                cbbLoc_Name.Enabled = true;
                txtLoc_Sub.Enabled = false;
                txtLoc_Sub.Text = "";
                cbbLoc_Prod.Enabled = false;

                rdbLocAdd.Enabled = false;
                rdbLocUpd.Enabled = false;
                rdbLocDel.Enabled = false;
                rdbLocAdd.Checked = false;
                rdbLocUpd.Checked = false;
                rdbLocDel.Checked = false;

                dgvWH_Loc.Enabled = false;
                dgvWH_Loc.DataSource = "";

                btnLocSave.Enabled = false;
                btnLocFinish.Enabled = false;
            }
        }

        private void cbbLoc_Prod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string locSub = cbbLoc_Name.SelectedValue.ToString();
            string locProd = cbbLoc_Prod.SelectedValue.ToString();


            if (cbbLoc_Prod.SelectedIndex > 0)
            {
                _locProd = locProd;

                if (_locId != "" && _locSub != "" && _locProd != "")
                {
                    rdbLocAdd.Enabled = false;
                    rdbLocAdd.Checked = false;
                    rdbLocUpd.Enabled = true;
                    rdbLocDel.Enabled = false;
                    rdbLocUpd.Checked = true;
                    rdbLocDel.Checked = false;

                    btnLocSave.Enabled = false;
                    btnLocFinish.Enabled = true;
                }
                else
                {
                    rdbLocAdd.Enabled = true;
                    rdbLocAdd.Checked = true;
                    rdbLocUpd.Enabled = false;
                    rdbLocDel.Enabled = false;
                    rdbLocUpd.Checked = false;
                    rdbLocDel.Checked = false;

                    btnLocSave.Enabled = true;
                    btnLocFinish.Enabled = true;
                }
                txtLoc_Sub.Enabled = true;
                txtLoc_Sub.Text = "";

                fnBindLoc();
                dgvWH_Loc.Enabled = true;
            }
            else
            {
                _locProd = "";

                cbbLoc_Name.Enabled = true;
                txtLoc_Sub.Enabled = false;
                txtLoc_Sub.Text = "";

                rdbLocAdd.Enabled = false;
                rdbLocUpd.Enabled = false;
                rdbLocDel.Enabled = false;
                rdbLocAdd.Checked = false;
                rdbLocUpd.Checked = false;
                rdbLocDel.Checked = false;

                dgvWH_Loc.Enabled = false;
                dgvWH_Loc.DataSource = "";

                btnLocSave.Enabled = false;
                btnLocFinish.Enabled = false;
            }
        }

        public void fnBindLoc()
        {
            //string locId = cbbLoc_Name.SelectedValue.ToString();
            //string locProd = cbbLoc_Prod.SelectedValue.ToString();
            string locId = _locId;
            string locProd = _locProd;
            string sql = "V2o1_BASE_Warehouse_Defines_Location_SelSubLocation_Addnew";
            DataTable dtLoc = new DataTable();

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "whId";
            sParams[0].Value = locId;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.Int;
            //sParams[1].ParameterName = "prodId";
            //sParams[1].Value = locProd;

            dtLoc = cls.ExecuteDataTable(sql, sParams);

            dgvWH_Loc.DataSource = dtLoc;
            dgvWH_Loc.Refresh();

            dgvWH_Loc.Columns[0].Width = 50 * _dgvWH_LocWidth / 100;    // partcode
            dgvWH_Loc.Columns[1].Width = 30 * _dgvWH_LocWidth / 100;    // location
            dgvWH_Loc.Columns[2].Width = 20 * _dgvWH_LocWidth / 100;    // quantity
            //dgvWH_Loc.Columns[3].Width = 10 * _dgvWH_LocWidth / 100;  // locId
            //dgvWH_Loc.Columns[4].Width = 8 * _dgvWH_LocWidth / 100;   // prodId


            dgvWH_Loc.Columns[0].Visible = true;
            dgvWH_Loc.Columns[1].Visible = true;
            dgvWH_Loc.Columns[2].Visible = true;
            dgvWH_Loc.Columns[3].Visible = false;
            dgvWH_Loc.Columns[4].Visible = false;

            cls.fnFormatDatagridview(dgvWH_Loc, 12);
        }

        private void dgvWH_Loc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvWH_Loc, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvWH_Loc.Rows[e.RowIndex];
            string locId = row.Cells[3].Value.ToString();
            string locSub = row.Cells[1].Value.ToString();
            _locId = locId;
            _locSub = locSub;

            cbbLoc_Name.Enabled = false;
            cbbLoc_Prod.Enabled = false;
            txtLoc_Sub.Enabled = true;
            txtLoc_Sub.Text = locSub;

            rdbLocAdd.Enabled = false;
            rdbLocAdd.Checked = false;
            rdbLocUpd.Enabled = true;
            rdbLocUpd.Checked = false;
            rdbLocDel.Enabled = true;
            rdbLocDel.Checked = false;

            btnLocSave.Enabled = false;
            btnLocFinish.Enabled = true;

            _oldSub = locSub;
        }

        private void rdbLocUpd_CheckedChanged(object sender, EventArgs e)
        {
            btnLocSave.Enabled = true;
        }

        private void rdbLocDel_CheckedChanged(object sender, EventArgs e)
        {
            btnLocSave.Enabled = true;
        }

        private void btnLocFinish_Click(object sender, EventArgs e)
        {
            _locId = "";

            cbbLoc_Name.Enabled = true;
            cbbLoc_Name.SelectedIndex = 0;
            cbbLoc_Prod.SelectedIndex = 0;
            cbbLoc_Prod.Enabled = false;
            txtLoc_Sub.Enabled = false;
            txtLoc_Sub.Text = "";

            rdbLocAdd.Enabled = false;
            rdbLocUpd.Enabled = false;
            rdbLocDel.Enabled = false;
            rdbLocAdd.Checked = false;
            rdbLocUpd.Checked = false;
            rdbLocDel.Checked = false;

            dgvWH_Loc.Enabled = false;
            dgvWH_Loc.DataSource = "";

            btnLocSave.Enabled = false;
            btnLocFinish.Enabled = false;
        }

        private void btnLocSave_Click(object sender, EventArgs e)
        {
            string locId = _locId;
            string locProd = _locProd;
            string locSub = txtLoc_Sub.Text.Trim();
            if (locId != "" && locProd != "" && locSub != "")
            {
                DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    if (rdbLocAdd.Checked)
                    {
                        fnLocAdd();
                    }
                    else if (rdbLocUpd.Checked)
                    {
                        fnLocUpd();
                    }
                    else if (rdbLocDel.Checked)
                    {
                        fnLocDel();
                    }
                    fnBindLoc();
                }
            }
            else
            {
                MessageBox.Show("Please input/choose the location name");
                txtLoc_Sub.Text = "";
                txtLoc_Sub.Focus();
            }
        }

        public void fnLocAdd()
        {
            string sql = "";
            string locId = _locId;
            string locProd = _locProd;
            string locSub = txtLoc_Sub.Text.Trim();

            sql = "V2o1_BASE_Warehouse_Defines_Location_AddItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "locId";
            sParams[0].Value = locId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "locSub";
            sParams[1].Value = locSub;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "locProd";
            sParams[2].Value = locProd;

            cls.fnUpdDel(sql, sParams);

            txtLoc_Sub.Text = "";
            txtLoc_Sub.Focus();
        }

        public void fnLocUpd()
        {
            string sql = "";
            string locId = _locId;
            string locProd = _locProd;
            string locSub = txtLoc_Sub.Text.Trim();
            string oldSub = _oldSub;

            sql = "V2o1_BASE_Warehouse_Defines_Location_UpdItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "locId";
            sParams[0].Value = locId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "locSub";
            sParams[1].Value = locSub;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.Int;
            sParams[2].ParameterName = "locProd";
            sParams[2].Value = locProd;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.VarChar;
            sParams[3].ParameterName = "oldSub";
            sParams[3].Value = oldSub;

            cls.fnUpdDel(sql, sParams);

            _oldSub = "";
            txtLoc_Sub.Text = "";
            txtLoc_Sub.Focus();
        }

        public void fnLocDel()
        {
            string sql = "";
            string locId = _locId;
            string locProd = _locProd;
            string locSub = txtLoc_Sub.Text.Trim();

            sql = "V2o1_BASE_Warehouse_Defines_Location_DelItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "locId";
            sParams[0].Value = locId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "locSub";
            sParams[1].Value = locSub;

            //sParams[2] = new SqlParameter();
            //sParams[2].SqlDbType = SqlDbType.Int;
            //sParams[2].ParameterName = "locProd";
            //sParams[2].Value = locProd;

            cls.fnUpdDel(sql, sParams);

            cbbLoc_Name.Enabled = true;
            cbbLoc_Prod.Enabled = true;
            
            txtLoc_Sub.Text = "";
            txtLoc_Sub.Focus();

            rdbLocAdd.Enabled = true;
            rdbLocAdd.Checked = true;
            rdbLocUpd.Enabled = false;
            rdbLocUpd.Checked = false;
            rdbLocDel.Enabled = false;
            rdbLocDel.Checked = false;

            btnLocSave.Enabled = true;
            btnLocFinish.Enabled = true;
        }

        public void initWH_Pak()
        {
            dgvWH_Pak.Enabled = false;
            btnPakSave.Enabled = false;
            btnPakOff.Enabled = false;
        }

        public void initWH_WH6()
        {
            cbbWH6_Warehouse.Items.Add("WH6 - Finish Good");
            cbbWH6_Warehouse.Items.Add("WH7 - Working In Process");
            cbbWH6_Warehouse.Items.Add("WH8 - Resin Recycle");
            cbbWH6_Warehouse.Items.Add("WH9 - Scrap");
            cbbWH6_Warehouse.Items.Add("WH10 - Garbage");
            cbbWH6_Warehouse.Items.Add("WH11 - Stationary");
            cbbWH6_Warehouse.Items.Insert(0, "");
            cbbWH6_Warehouse.SelectedIndex = 0;

            btnWH6ON.Enabled = false;

            lblWH6_Partname.Text = "N/A";
            lblWH6_Partcode.Text = "N/A";
            lblWH6_CurrPos.Text = "N/A";
            txtWH6_NewPos.Enabled = false;
            txtWH6_NewPos.Text = "";

            dgvWH_WH6.Enabled = false;

            rdbWH6Add.Enabled = false;
            rdbWH6Upd.Enabled = false;
            rdbWH6Del.Enabled = false;

            btnWH6Save.Enabled = false;
            btnWH6Finish.Enabled = false;
        }

        public void initWH_FGI()
        {
            dtpFGIDate.Value = DateTime.Now;
            cbbFGIShift.Items.Add("DAY");
            cbbFGIShift.Items.Add("NIGHT");
            cbbFGIShift.Items.Insert(0, "");
            cbbFGIShift.SelectedIndex = 0;

            dgvWH_FGI.Enabled = false;

            btnFGISave.Enabled = false;
            btnFGIFinish.Enabled = false;
        }

        private void btnPakOn_Click(object sender, EventArgs e)
        {
            dgvWH_Pak.Enabled = true;
            btnPakOn.Enabled = false;
            btnPakSave.Enabled = true;
            btnPakOff.Enabled = true;
            fnBindPak();
        }

        private void btnPakOff_Click(object sender, EventArgs e)
        {
            dgvWH_Pak.DataSource = "";
            dgvWH_Pak.Refresh();
            dgvWH_Pak.Enabled = false;
            btnPakOn.Enabled = true;
            btnPakSave.Enabled = false;
            btnPakOff.Enabled = false;
        }

        private void btnPakSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                string sql = "", prodId = "", pcs = "", box = "", pak = "", pal = "";
                foreach (DataGridViewRow row in dgvWH_Pak.Rows)
                {
                    prodId = row.Cells[0].Value.ToString();
                    pcs = row.Cells[4].Value.ToString();
                    box = row.Cells[5].Value.ToString();
                    pak = row.Cells[6].Value.ToString();
                    pal = row.Cells[7].Value.ToString();

                    sql = "V2o1_BASE_Warehouse_Defines_Packing_UpdItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "prodId";
                    sParams[0].Value = prodId;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.SmallMoney;
                    sParams[1].ParameterName = "pcs";
                    sParams[1].Value = pcs;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.SmallMoney;
                    sParams[2].ParameterName = "box";
                    sParams[2].Value = box;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.SmallMoney;
                    sParams[3].ParameterName = "pak";
                    sParams[3].Value = pak;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.SmallMoney;
                    sParams[4].ParameterName = "pal";
                    sParams[4].Value = pal;

                    cls.fnUpdDel(sql, sParams);
                }
                fnBindPak();
                MessageBox.Show("Update Packing Standard successful", cls.appName());
            }
        }

        public void fnBindPak()
        {
            string sql = "V2o1_BASE_Warehouse_Defines_Packing_SelItem_Addnew";
            DataTable dtPak = new DataTable();

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "cateId";
            sParams[0].Value = 105;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.Int;
            //sParams[1].ParameterName = "prodId";
            //sParams[1].Value = locProd;

            dtPak = cls.ExecuteDataTable(sql, sParams);

            dgvWH_Pak.DataSource = dtPak;
            dgvWH_Pak.Refresh();

            //dgvWH_Pak.Columns[0].Width = 50 * _dgvWH_PakWidth / 100;  // prodId
            dgvWH_Pak.Columns[1].Width = 20 * _dgvWH_PakWidth / 100;  // category
            dgvWH_Pak.Columns[2].Width = 30 * _dgvWH_PakWidth / 100;  // material
            //dgvWH_Pak.Columns[3].Width = 10 * _dgvWH_PakWidth / 100;  // code
            dgvWH_Pak.Columns[4].Width = 10 * _dgvWH_PakWidth / 100;   // pcs
            dgvWH_Pak.Columns[5].Width = 10 * _dgvWH_PakWidth / 100;   // box
            dgvWH_Pak.Columns[6].Width = 10 * _dgvWH_PakWidth / 100;   // pak
            dgvWH_Pak.Columns[7].Width = 10 * _dgvWH_PakWidth / 100;   // pal
            dgvWH_Pak.Columns[8].Width = 10 * _dgvWH_PakWidth / 100;   // unit


            dgvWH_Pak.Columns[0].Visible = false;
            dgvWH_Pak.Columns[1].Visible = true;
            dgvWH_Pak.Columns[2].Visible = true;
            dgvWH_Pak.Columns[3].Visible = false;
            dgvWH_Pak.Columns[4].Visible = true;
            dgvWH_Pak.Columns[5].Visible = true;
            dgvWH_Pak.Columns[6].Visible = true;
            dgvWH_Pak.Columns[7].Visible = true;
            dgvWH_Pak.Columns[8].Visible = true;

            dgvWH_Pak.Columns[0].ReadOnly = true;
            dgvWH_Pak.Columns[1].ReadOnly = true;
            dgvWH_Pak.Columns[2].ReadOnly = true;
            dgvWH_Pak.Columns[3].ReadOnly = true;
            dgvWH_Pak.Columns[4].ReadOnly = false;
            dgvWH_Pak.Columns[5].ReadOnly = false;
            dgvWH_Pak.Columns[6].ReadOnly = false;
            dgvWH_Pak.Columns[7].ReadOnly = false;
            dgvWH_Pak.Columns[8].ReadOnly = true;

            cls.fnFormatDatagridview(dgvWH_Pak, 11);
        }

        private void dgvWH_Pak_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvWH_Pak, e);
        }

        private void dgvWH_Pak_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string pcs = "", box = "", pak = "", pal = "";
            int _pcs = 0, _box = 0, _pak = 0, _pal = 0;
            foreach (DataGridViewRow row in dgvWH_Pak.Rows)
            {
                pcs = row.Cells[4].Value.ToString();
                box = row.Cells[5].Value.ToString();
                pak = row.Cells[6].Value.ToString();
                pal = row.Cells[7].Value.ToString();

                _pcs = (pcs != "" && pcs != null) ? Convert.ToInt32(pcs) : 0;
                _box = (box != "" && box != null) ? Convert.ToInt32(box) : 0;
                _pak = (pak != "" && pak != null) ? Convert.ToInt32(pak) : 0;
                _pal = (pal != "" && pal != null) ? Convert.ToInt32(pal) : 0;

                if (_pcs == 0 && _box == 0 && _pak == 0 && _pal == 0)
                {
                    row.Cells[4].Style.BackColor = Color.Yellow;
                    row.Cells[5].Style.BackColor = Color.Yellow;
                    row.Cells[6].Style.BackColor = Color.Yellow;
                    row.Cells[7].Style.BackColor = Color.Yellow;
                }
                else
                {
                    row.Cells[4].Style.BackColor = (_pcs == 0) ? Color.LightYellow : Color.LightSkyBlue;
                    row.Cells[5].Style.BackColor = (_box == 0) ? Color.LightYellow : Color.LightSkyBlue;
                    row.Cells[6].Style.BackColor = (_pak == 0) ? Color.LightYellow : Color.LightSkyBlue;
                    row.Cells[7].Style.BackColor = (_pal == 0) ? Color.LightYellow : Color.LightSkyBlue;
                }
            }
        }

        private void btnWH6ON_Click(object sender, EventArgs e)
        {
            btnWH6ON.Enabled = false;

            fnBindWH6();

            lblWH6_Partname.Text = "N/A";
            lblWH6_Partcode.Text = "N/A";
            lblWH6_CurrPos.Text = "N/A";
            txtWH6_NewPos.Enabled = false;
            txtWH6_NewPos.Text = "";

            dgvWH_WH6.Enabled = true;

            rdbWH6Add.Enabled = false;
            rdbWH6Upd.Enabled = false;
            rdbWH6Del.Enabled = false;

            btnWH6Save.Enabled = false;
            btnWH6Finish.Enabled = true;

        }

        public void fnBindWH6()
        {
            string whId = "";
            if (cbbWH6_Warehouse.SelectedIndex == 1)        //WH6 - Finish Good
                whId = "100";
            else if (cbbWH6_Warehouse.SelectedIndex == 2)   //WH7 - Working In Process
                whId = "114";
            else if (cbbWH6_Warehouse.SelectedIndex == 3)   //WH8 - Recycle
                whId = "115";
            else if (cbbWH6_Warehouse.SelectedIndex == 4)   //WH9 - Scrap
                whId = "116";
            else if (cbbWH6_Warehouse.SelectedIndex == 5)   //WH10 - Garbage
                whId = "117";
            else if (cbbWH6_Warehouse.SelectedIndex == 6)   //WH11 - Stationary
                whId = "118";
            string sql = "V2o1_BASE_Warehouse_Defines_WH6_SelItem_Addnew";
            DataTable dtPak = new DataTable();

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@whId";
            sParams[0].Value = whId;

            dtPak = cls.ExecuteDataTable(sql, sParams);

            dgvWH_WH6.DataSource = dtPak;
            dgvWH_WH6.Refresh();

            //dgvWH_WH6.Columns[0].Width = 50 * _dgvWH_WH6Width / 100;  // prodId
            dgvWH_WH6.Columns[1].Width = 50 * _dgvWH_WH6Width / 100;  // category
            dgvWH_WH6.Columns[2].Width = 30 * _dgvWH_WH6Width / 100;  // material
            dgvWH_WH6.Columns[3].Width = 20 * _dgvWH_WH6Width / 100;  // code


            dgvWH_WH6.Columns[0].Visible = false;
            dgvWH_WH6.Columns[1].Visible = true;
            dgvWH_WH6.Columns[2].Visible = true;
            dgvWH_WH6.Columns[3].Visible = true;

            cls.fnFormatDatagridview(dgvWH_WH6, 11);

        }

        private void btnWH6Finish_Click(object sender, EventArgs e)
        {
            cbbWH6_Warehouse.SelectedIndex = 0;
            btnWH6ON.Enabled = false;

            lblWH6_Partname.Text = "N/A";
            lblWH6_Partcode.Text = "N/A";
            lblWH6_CurrPos.Text = "N/A";
            txtWH6_NewPos.Enabled = false;
            txtWH6_NewPos.Text = "";

            dgvWH_WH6.DataSource = "";
            dgvWH_WH6.Refresh();
            dgvWH_WH6.Enabled = false;

            rdbWH6Add.Enabled = false;
            rdbWH6Add.Checked = false;
            rdbWH6Upd.Enabled = false;
            rdbWH6Upd.Checked = false;
            rdbWH6Del.Enabled = false;
            rdbWH6Del.Checked = false;

            btnWH6Save.Enabled = false;
            btnWH6Finish.Enabled = false;
        }

        private void dgvWH_WH6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvWH_WH6, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvWH_WH6.Rows[e.RowIndex];
            string prodId = row.Cells[0].Value.ToString();
            string prodName = row.Cells[1].Value.ToString();
            string prodCode = row.Cells[2].Value.ToString();
            string prodSub = row.Cells[3].Value.ToString();

            _WH6ProdId = prodId;

            lblWH6_Partname.Text = prodName;
            lblWH6_Partcode.Text = prodCode;
            if(prodSub != "" && prodSub != null)
            {
                lblWH6_CurrPos.Text = prodSub;
                lblWH6_CurrPos.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            else
            {
                lblWH6_CurrPos.Text = "N/A";
                lblWH6_CurrPos.BackColor = Color.Yellow;
            }

            txtWH6_NewPos.Enabled = true;
            txtWH6_NewPos.Focus();

            rdbWH6Add.Enabled = false;
            rdbWH6Add.Checked = false;
            rdbWH6Upd.Enabled = false;
            rdbWH6Upd.Checked = false;
            rdbWH6Del.Enabled = true;
            rdbWH6Del.Checked = false;

            btnWH6Save.Enabled = false;
            btnWH6Finish.Enabled = true;
        }

        private void dgvWH_WH6_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string pos = "";
            foreach (DataGridViewRow row in dgvWH_WH6.Rows)
            {
                pos = row.Cells[3].Value.ToString();
                if(pos==""||pos==null)
                {
                    row.Cells[3].Style.BackColor = Color.Yellow;
                }
            }
        }

        private void btnWH6Save_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                if (rdbWH6Add.Checked)
                {
                    fnWH6Add();
                }
                else if (rdbWH6Upd.Checked)
                {
                    fnWH6Upd();
                }
                else if (rdbWH6Del.Checked)
                {
                    fnWH6Del();
                }
                fnBindWH6();

                lblWH6_Partname.Text = "N/A";
                lblWH6_Partcode.Text = "N/A";
                lblWH6_CurrPos.Text = "N/A";
                txtWH6_NewPos.Enabled = false;
                txtWH6_NewPos.Text = "";

                rdbWH6Add.Enabled = false;
                rdbWH6Add.Checked = false;
                rdbWH6Upd.Enabled = false;
                rdbWH6Upd.Checked = false;
                rdbWH6Del.Enabled = false;
                rdbWH6Del.Checked = false;

                btnWH6Save.Enabled = false;
                btnWH6Finish.Enabled = true;

            }
        }

        public void fnWH6Add()
        {

        }

        public void fnWH6Upd()
        {
            string prodId = _WH6ProdId;
            string sublocate = txtWH6_NewPos.Text.Trim();
            if(prodId!="")
            {
                string sql = "V2o1_BASE_Warehouse_Defines_WH6_UpdItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "prodId";
                sParams[0].Value = prodId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "sublocate";
                sParams[1].Value = sublocate;

                cls.fnUpdDel(sql, sParams);

                _WH6ProdId = "";
                txtWH6_NewPos.Text = "";
                txtWH6_NewPos.Focus();
            }
        }

        public void fnWH6Del()
        {
            string prodId = _WH6ProdId;
            string sublocate = lblWH6_CurrPos.Text;
            if (prodId != "")
            {
                string sql = "V2o1_BASE_Warehouse_Defines_WH6_DelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "prodId";
                sParams[0].Value = prodId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "sublocate";
                sParams[1].Value = sublocate;

                cls.fnUpdDel(sql, sParams);

                _WH6ProdId = "";
                txtWH6_NewPos.Text = "";
                txtWH6_NewPos.Focus();
            }
        }

        private void txtWH6_NewPos_TextChanged(object sender, EventArgs e)
        {
            if(txtWH6_NewPos.Text.Length>0)
            {
                rdbWH6Upd.Enabled = true;
                rdbWH6Del.Enabled = true;
            }
            else
            {
                rdbWH6Upd.Enabled = false;
                rdbWH6Del.Enabled = true;

                btnWH6Save.Enabled = false;
            }
        }

        private void rdbWH6Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnWH6Save.Enabled = true;
        }

        private void rdbWH6Del_CheckedChanged(object sender, EventArgs e)
        {
            btnWH6Save.Enabled = true;
        }

        private void dtpFGIDate_ValueChanged(object sender, EventArgs e)
        {
            fnBindFGI();
        }

        private void cbbFGIShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnBindFGI();
        }

        public void fnBindFGI()
        {
            if(cbbFGIShift.SelectedIndex>0)
            {
                DateTime inventDate = dtpFGIDate.Value;
                string inventShift = cbbFGIShift.Text;
                string sql = "V2_BASE_FINISHGOOD_COUNTING_SELECT_ADDNEW";
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.DateTime;
                sParams[0].ParameterName = "date";
                sParams[0].Value = inventDate;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "shift";
                sParams[1].Value = inventShift;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);

                dgvWH_FGI.DataSource = dt;
                dgvWH_FGI.Refresh();

                cls.fnFormatDatagridview(dgvWH_FGI, 11);

                dgvWH_FGI.Columns[0].Visible = true;
                dgvWH_FGI.Columns[1].Visible = true;
                dgvWH_FGI.Columns[2].Visible = true;
                dgvWH_FGI.Columns[3].Visible = false;

                dgvWH_FGI.Columns[0].Width = 60 * _dgvWH_FGIWidth / 100;
                dgvWH_FGI.Columns[1].Width = 25 * _dgvWH_FGIWidth / 100;
                dgvWH_FGI.Columns[2].Width = 15 * _dgvWH_FGIWidth / 100;

                dgvWH_FGI.Columns[0].ReadOnly = true;
                dgvWH_FGI.Columns[1].ReadOnly = false;
                dgvWH_FGI.Columns[2].ReadOnly = true;

                dgvWH_FGI.Enabled = true;
                btnFGISave.Enabled = true;
                btnFGIFinish.Enabled = true;
            }
            else
            {
                dgvWH_FGI.DataSource = "";
                dgvWH_FGI.Refresh();
                dgvWH_FGI.Enabled = false;
                btnFGISave.Enabled = false;
                btnFGIFinish.Enabled = false;
            }
        }

        private void btnFGIFinish_Click(object sender, EventArgs e)
        {
            dtpFGIDate.Value = DateTime.Now;
            cbbFGIShift.SelectedIndex = 0;
            dgvWH_FGI.DataSource = "";
            dgvWH_FGI.Refresh();
            btnFGISave.Enabled = false;
            btnFGIFinish.Enabled = false;
        }

        private void btnFGISave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvWH_FGI.Rows)
                {
                    DateTime date = dtpFGIDate.Value;
                    string shift = cbbFGIShift.Text;
                    string invent = row.Cells[1].Value.ToString();
                    string prodId = row.Cells[3].Value.ToString();

                    string sql = "V2_BASE_FINISHGOOD_COUNTING_INSUPD_ADDNEW";
                    SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.DateTime;
                    sParams[0].ParameterName = "@date";
                    sParams[0].Value = date.ToShortDateString();

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.VarChar;
                    sParams[1].ParameterName = "@shift";
                    sParams[1].Value = shift;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.VarChar;
                    sParams[2].ParameterName = "@invent";
                    sParams[2].Value = invent;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.VarChar;
                    sParams[3].ParameterName = "@prodId";
                    sParams[3].Value = prodId;

                    cls.fnUpdDel(sql, sParams);
                }
                fnBindFGI();
            }
        }

        private void dgvWH_FGI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvWH_FGI, e);
        }

        private void dgvWH_FGI_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string qty = "";
            int _qty = 0;
            foreach (DataGridViewRow row in dgvWH_FGI.Rows)
            {
                qty = row.Cells[1].Value.ToString();
                _qty = (qty != "" && qty != null) ? Convert.ToInt32(qty) : 0;
                if(_qty==0)
                {
                    row.Cells[1].Style.BackColor = Color.Yellow;
                }
            }
        }

        private void cbbWH6_Warehouse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbWH6_Warehouse.SelectedIndex > 0)
            {
                btnWH6ON.Enabled = true;
            }
            else
            {
                btnWH6ON.Enabled = false;
            }
        }
    }
}
