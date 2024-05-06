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

    public partial class frmCapacityCodeTimeDefine : Form
    {
        public string _idx;
        public string _partId;
        public string _partCode;
        public string _idxPIC;

        public string _idxLine;

        public int _dgvCodeDefineWidth;
        public int _dgvRestingTimeWidth;
        public int _dgvAssyLineWidth;
        public int _dgvPICWidth;

        //public string _appName = System.AppDomain.CurrentDomain.FriendlyName;
        public string _appName = Application.ProductName;

        public frmCapacityCodeTimeDefine()
        {
            InitializeComponent();
        }

        private void frmCapacityCodeTimeDefine_Load(object sender, EventArgs e)
        {
            _dgvCodeDefineWidth = cls.fnGetDataGridWidth(dgvCodeDefine) - 20;
            _dgvRestingTimeWidth = ((dgvCodeDefine.ScrollBars & ScrollBars.Vertical) != ScrollBars.None) ? this.dgvPIC.Width : this.dgvPIC.Width - 20;
            _dgvAssyLineWidth = cls.fnGetDataGridWidth(dgvAssyLine);
            _dgvPICWidth = cls.fnGetDataGridWidth(dgvPIC);

            init();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvCodeDefineWidth = cls.fnGetDataGridWidth(dgvCodeDefine) - 20;
            _dgvRestingTimeWidth = ((dgvCodeDefine.ScrollBars & ScrollBars.Vertical) != ScrollBars.None) ? this.dgvPIC.Width : this.dgvPIC.Width - 20;
            _dgvAssyLineWidth = cls.fnGetDataGridWidth(dgvAssyLine);
            _dgvPICWidth = cls.fnGetDataGridWidth(dgvPIC);

            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            fnBindCodePartName();
            fnBindCodeDefine();
            fnBindRestingTime();
            fnBindAssyLine();
            fnBindAssyLineName();
            fnBindPicTitle();
            fnBindPicLine();

            initCodeDefine();
            initRestingTime();
            initAssyLine();
            initPIC();
        }

        public void fnGetDate()
        {

        }

        public void fnBindCodePartName()
        {
            string sql = "";
            sql = "V2_BASE_CAPACITY_CODE_DEFINE_SELECT_PARTNAME_ADDNEW";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbbCodePartName.DataSource = dt;
            cbbCodePartName.DisplayMember = "Name";
            cbbCodePartName.ValueMember = "prodId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            this.cbbCodePartName.SelectedIndex = 0;
        }

        public void fnBindCodeDefine()
        {
            string sql = "";
            sql = "V2_BASE_CAPACITY_CODE_DEFINE_ADDNEW";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            ////dt.Select("Sublocation IS NOT NULL");
            //DataRow[] dtr = dt.Select("sublocation<>''");
            //DataTable dt2 = dt.Clone();
            //foreach (DataRow d in dtr)
            //{
            //    dt2.ImportRow(d);
            //}

            dgvCodeDefine.DataSource = dt;
            dgvCodeDefine.Refresh();

            dgvCodeDefine.Columns[2].Width = 20 * _dgvCodeDefineWidth / 100;
            dgvCodeDefine.Columns[3].Width = 20 * _dgvCodeDefineWidth / 100;
            dgvCodeDefine.Columns[4].Width = 15 * _dgvCodeDefineWidth / 100;
            dgvCodeDefine.Columns[5].Width = 15 * _dgvCodeDefineWidth / 100;
            dgvCodeDefine.Columns[6].Width = 15 * _dgvCodeDefineWidth / 100;
            dgvCodeDefine.Columns[7].Width = 10 * _dgvCodeDefineWidth / 100;
            dgvCodeDefine.Columns[8].Width = 5 * _dgvCodeDefineWidth / 100;

            dgvCodeDefine.Columns[0].Visible = false;
            dgvCodeDefine.Columns[1].Visible = false;
            dgvCodeDefine.Columns[2].Visible = true;
            dgvCodeDefine.Columns[3].Visible = true;
            dgvCodeDefine.Columns[4].Visible = true;
            dgvCodeDefine.Columns[5].Visible = true;
            dgvCodeDefine.Columns[6].Visible = true;
            dgvCodeDefine.Columns[7].Visible = true;
            dgvCodeDefine.Columns[8].Visible = true;

            //dgvCodeDefine.Columns[4].DefaultCellStyle.Format = @"dd\/MM\/yyyy";

            // Set the selection background color for all the cells.
            dgvCodeDefine.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvCodeDefine.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            dgvCodeDefine.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvCodeDefine.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Set the row and column header styles.
            dgvCodeDefine.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCodeDefine.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvCodeDefine.RowHeadersDefaultCellStyle.BackColor = Color.Black;

            using (Font font = new Font(dgvCodeDefine.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular))
            {
                //dgvCodeDefine.Columns["Rating"].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[0].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[1].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[2].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[3].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[4].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[5].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[6].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[7].DefaultCellStyle.Font = font;
                dgvCodeDefine.Columns[8].DefaultCellStyle.Font = font;
            }

            // Attach a handler to the CellFormatting event.
            //dgvCodeDefine.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvCodeDefine_CellFormatting);
        }

        public void fnBindRestingTime()
        {

        }

        public void fnBindAssyLine()
        {
            string sql = "";
            sql = "V2_BASE_CAPACITY_LINE_DEFINE_ADDNEW";
            DataTable dtLine = new DataTable();
            dtLine = cls.fnSelect(sql);
            dgvAssyLine.DataSource = dtLine;
            dgvAssyLine.Refresh();

            dgvAssyLine.Columns[0].Width = 10 * _dgvAssyLineWidth / 100;
            dgvAssyLine.Columns[1].Width = 35 * _dgvAssyLineWidth / 100;
            dgvAssyLine.Columns[2].Width = 45 * _dgvAssyLineWidth / 100;
            dgvAssyLine.Columns[3].Width = 10 * _dgvAssyLineWidth / 100;

            dgvAssyLine.Columns[0].Visible = true;
            dgvAssyLine.Columns[1].Visible = true;
            dgvAssyLine.Columns[2].Visible = true;
            dgvAssyLine.Columns[3].Visible = true;

            cls.fnFormatDatagridview(dgvAssyLine, 11);
        }

        public void fnBindAssyLineName()
        {
            cbbAssyMachine.Items.Add("Auto Balance");
            cbbAssyMachine.Items.Add("Blower");
            cbbAssyMachine.Items.Add("Dispenser");
            cbbAssyMachine.Items.Add("Mixing");
            cbbAssyMachine.Items.Add("Pump");
            cbbAssyMachine.Items.Add("Weight Balance");
            cbbAssyMachine.Items.Add("Welding");
            cbbAssyMachine.Items.Insert(0, "");
            cbbAssyMachine.SelectedIndex = 0;
        }

        public void fnBindPicTitle()
        {
            cbbPIC_Title.Items.Add("Supervisor.");
            cbbPIC_Title.Items.Add("Sub-Sup.");
            cbbPIC_Title.Items.Add("Worker.");
            cbbPIC_Title.Items.Insert(0, "");
            cbbPIC_Title.SelectedIndex = 0;
        }

        public void fnBindPicLine()
        {
            string sqlPic = "";
            sqlPic = "V2_BASE_CAPACITY_LINE_DEFINE_PIC_SELITEM_ADDNEW";
            DataTable dtPic = new DataTable();
            dtPic = cls.fnSelect(sqlPic);
            dgvPIC.DataSource = dtPic;
            dgvPIC.Refresh();

            dgvPIC.Columns[0].Width = 10 * _dgvPICWidth / 100;
            dgvPIC.Columns[1].Width = 40 * _dgvPICWidth / 100;
            dgvPIC.Columns[2].Width = 50 * _dgvPICWidth / 100;
            //dgvPIC.Columns[3].Width = 50 * _dgvPICWidth / 100;
            //dgvPIC.Columns[4].Width = 50 * _dgvPICWidth / 100;

            dgvPIC.Columns[0].Visible = true;
            dgvPIC.Columns[1].Visible = true;
            dgvPIC.Columns[2].Visible = true;
            dgvPIC.Columns[3].Visible = false;
            dgvPIC.Columns[4].Visible = false;

            cls.fnFormatDatagridview(dgvPIC, 11);

        }



        public void initCodeDefine()
        {
            cbbCodePartName.Enabled = true;
            txtCodePartNo.Enabled = false;
            txtCodeUPH.Enabled = false;
            txtCode01.Enabled = false;
            txtCode02.Enabled = false;
            txtCode03.Enabled = false;
            rdbCodeActive.Enabled = false;
            rdbCodeDeactive.Enabled = false;
            lblCodeMessage.Enabled = false;
            rdbCodeAdd.Enabled = true;
            rdbCodeUpd.Enabled = false;
            rdbCodeDel.Enabled = false;
            btnCodeSave.Enabled = true;
            btnCodeFinish.Enabled = true;

            cbbCodePartName.SelectedIndex = 0;
            txtCodePartNo.Text = "";
            txtCodeUPH.Text = "";
            txtCode01.Text = "";
            txtCode02.Text = "";
            txtCode03.Text = "";
            rdbCodeActive.Checked = false;
            rdbCodeDeactive.Checked = false;
            lblCodeMessage.Text = "";
            rdbCodeAdd.Checked = true;
            rdbCodeUpd.Checked = false;
            rdbCodeDel.Checked = false;

        }

        public void initRestingTime()
        {

        }

        public void initAssyLine()
        {
            fnLineFinish();
        }

        public void initPIC()
        {
            txtPIC_Name.Enabled = false;
            rdbPIC_Add.Enabled = false;
            rdbPIC_Upd.Enabled = false;
            rdbPIC_Del.Enabled = false;
            btnPIC_Save.Enabled = false;
            btnPIC_Finish.Enabled = false;
        }

        private void dgvCodeDefine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCodeDefine.Rows.Count > 0)
            {
                DataGridViewRow row = new DataGridViewRow();
                row = dgvCodeDefine.Rows[e.RowIndex];
                row.Selected = true;
                string idx = row.Cells[0].Value.ToString();
                string partID = row.Cells[1].Value.ToString();
                string partName = row.Cells[2].Value.ToString();
                string partCode = row.Cells[3].Value.ToString();
                string code01 = row.Cells[4].Value.ToString();
                string code02 = row.Cells[5].Value.ToString();
                string code03 = row.Cells[6].Value.ToString();
                string UPH = row.Cells[7].Value.ToString();
                string active = row.Cells[8].Value.ToString();

                _idx = idx;
                _partId = partID;
                _partCode = partCode;

                cbbCodePartName.Enabled = false;
                txtCodePartNo.Enabled = false;
                txtCodeUPH.Enabled = true;
                txtCode01.Enabled = true;
                txtCode02.Enabled = true;
                txtCode03.Enabled = true;
                rdbCodeActive.Enabled = true;
                rdbCodeActive.Enabled = true;
                rdbCodeAdd.Enabled = false;
                rdbCodeUpd.Enabled = true;
                rdbCodeDel.Enabled = true;

                rdbCodeAdd.Checked = false;
                rdbCodeUpd.Checked = false;
                rdbCodeDel.Checked = false;

                btnCodeSave.Enabled = false;
                btnCodeFinish.Enabled = true;

                //txtCodePartName.Text = partName;
                cbbCodePartName.SelectedValue = partID;
                txtCodePartNo.Text = partCode;
                txtCodeUPH.Text = UPH;
                txtCode01.Text = code01;
                txtCode02.Text = code02;
                txtCode03.Text = code03;
                if (active == "True")
                {
                    rdbCodeActive.Checked = true;
                }
                else
                {
                    rdbCodeDeactive.Checked = true;
                }
            }
        }

        private void rdbCodeUpd_CheckedChanged(object sender, EventArgs e)
        {
            btnCodeSave.Enabled = true;
        }

        private void rdbCodeDel_CheckedChanged(object sender, EventArgs e)
        {
            btnCodeSave.Enabled = true;
        }

        private void cbbCodePartName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbCodePartName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbCodePartName.SelectedIndex > 0)
            {
                string prodID = cbbCodePartName.SelectedValue.ToString();
                string sql = "V2_BASE_CAPACITY_CODE_DEFINE_SELECT_PARTCODE_ADDNEW";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodID";
                sParams[0].Value = prodID;

                DataTable dta = new DataTable();
                dta = cls.ExecuteDataTable(sql, CommandType.StoredProcedure, sParams);
                txtCodePartNo.Text = (dta.Rows.Count > 0) ? dta.Rows[0][0].ToString() : "";

                txtCodePartNo.Enabled = false;
                txtCodeUPH.Enabled = true;
                txtCode01.Enabled = true;
                txtCode02.Enabled = true;
                txtCode03.Enabled = true;
                rdbCodeActive.Enabled = true;
                rdbCodeDeactive.Enabled = true;
                rdbCodeAdd.Enabled = true;
                rdbCodeUpd.Enabled = false;
                rdbCodeDel.Enabled = false;
                btnCodeSave.Enabled = true;
                btnCodeFinish.Enabled = true;

                rdbCodeActive.Checked = true;
                rdbCodeAdd.Checked = true;
            }
            else
            {
                txtCodePartNo.Text = "";

                txtCodePartNo.Enabled = false;
                txtCodeUPH.Enabled = false;
                txtCode01.Enabled = false;
                txtCode02.Enabled = false;
                txtCode03.Enabled = false;
                rdbCodeActive.Enabled = false;
                rdbCodeDeactive.Enabled = false;
                rdbCodeAdd.Enabled = false;
                rdbCodeUpd.Enabled = false;
                rdbCodeDel.Enabled = false;
                btnCodeSave.Enabled = false;
                btnCodeFinish.Enabled = true;

                rdbCodeActive.Checked = false;
                rdbCodeAdd.Checked = false;
            }
        }

        private void btnCodeFinish_Click(object sender, EventArgs e)
        {
            cbbCodePartName.SelectedIndex = 0;
            cbbCodePartName.Enabled = true;
            txtCodePartNo.Enabled = false;
            txtCodeUPH.Enabled = false;
            txtCode01.Enabled = false;
            txtCode02.Enabled = false;
            txtCode03.Enabled = false;
            rdbCodeActive.Enabled = false;
            rdbCodeDeactive.Enabled = false;
            rdbCodeAdd.Enabled = true;
            rdbCodeUpd.Enabled = false;
            rdbCodeDel.Enabled = false;

            txtCodePartNo.Text = "";
            txtCodeUPH.Text = "";
            txtCode01.Text = "";
            txtCode02.Text = "";
            txtCode03.Text = "";
            rdbCodeActive.Checked = false;
            rdbCodeDeactive.Checked = false;
            rdbCodeAdd.Checked = true;
            rdbCodeUpd.Enabled = false;
            rdbCodeDel.Enabled = false;

            btnCodeSave.Enabled = true;
            btnCodeFinish.Enabled = true;

            dgvCodeDefine.ClearSelection();
        }

        private void btnCodeSave_Click(object sender, EventArgs e)
        {
            byte act = 0;
            if (rdbCodeAdd.Checked)
            {
                act = 1;
                DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    fnSaveAdd();
                }
                //else if (dialogResultAdd == DialogResult.No)
                //{
                //    //do something else
                //}
            }
            else if(rdbCodeUpd.Checked)
            {
                act = 2;
                DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    fnSaveUpd();
                }
            }
            else if(rdbCodeDel.Checked)
            {
                act = 3;
                DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    fnSaveDel();
                }
            }

        }

        public void fnSaveAdd()
        {
            string partId = cbbCodePartName.SelectedValue.ToString();
            string partName = cbbCodePartName.Text;
            string partCode = txtCodePartNo.Text;
            string uph = txtCodeUPH.Text.Trim();
            string code01 = txtCode01.Text.Trim();
            string code02 = txtCode02.Text.Trim();
            string code03 = txtCode03.Text.Trim();
            string status = (rdbCodeActive.Checked) ? "True" : "False";

            string sql = "";
            sql = "V2_BASE_CAPACITY_CODE_DEFINE_ADDITEM_ADDNEW";

            SqlParameter[] sParams = new SqlParameter[8]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@partID";
            sParams[0].Value = partId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.NVarChar;
            sParams[1].ParameterName = "@partname";
            sParams[1].Value = partName;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.VarChar;
            sParams[2].ParameterName = "@partnumber";
            sParams[2].Value = partCode;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.SmallInt;
            sParams[3].ParameterName = "@uph";
            sParams[3].Value = uph;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.VarChar;
            sParams[4].ParameterName = "@code01";
            sParams[4].Value = code01;

            sParams[5] = new SqlParameter();
            sParams[5].SqlDbType = SqlDbType.VarChar;
            sParams[5].ParameterName = "@code02";
            sParams[5].Value = code02;

            sParams[6] = new SqlParameter();
            sParams[6].SqlDbType = SqlDbType.VarChar;
            sParams[6].ParameterName = "@code03";
            sParams[6].Value = code03;

            sParams[7] = new SqlParameter();
            sParams[7].SqlDbType = SqlDbType.Bit;
            sParams[7].ParameterName = "@status";
            sParams[7].Value = status;

            cls.fnUpdDel(sql, sParams);

            fnBindCodeDefine();
            initCodeDefine();
        }

        public void fnSaveUpd()
        {
            fnSaveAdd();
        }

        public void fnSaveDel()
        {
            string idx = _idx;

            string sql = "";
            sql = "V2_BASE_CAPACITY_CODE_DEFINE_DELITEM_ADDNEW";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@idx";
            sParams[0].Value = idx;

            cls.fnUpdDel(sql, sParams);

            fnBindCodeDefine();
            initCodeDefine();
        }

        private void cbbAssyMachine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cbbAssyMachine.SelectedIndex>0)
            {
                txtAssyLine.Enabled = true;
                rdbLineAdd.Enabled = true;
                rdbLineAdd.Checked = true;
                btnLineSave.Enabled = true;
            }
            else
            {
                txtAssyLine.Enabled = false;
                rdbLineAdd.Enabled = false;
                rdbLineAdd.Checked = false;
                btnLineSave.Enabled = false;
            }
        }

        private void btnLineSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                if (rdbLineAdd.Checked)
                {
                    fnLineAdd();
                }
                else if (rdbLineUpd.Checked)
                {
                    fnLineUpd();
                    fnLineFinish();
                }
                else if (rdbLineDel.Checked)
                {
                    fnLineDel();
                    fnLineFinish();
                }
                fnBindAssyLine();
            }
        }
        
        public void fnLineAdd()
        {
            string production = cbbAssyMachine.Text;
            string line = txtAssyLine.Text.Trim();
            byte active = Convert.ToByte((chkLineActive.Checked) ? 1 : 0);

            if(line!="")
            {
                string sqlLine = "";
                sqlLine = "V2_BASE_CAPACITY_LINE_DEFINE_ADDITEM_ADDNEW";
                SqlParameter[] sParamsLine = new SqlParameter[2]; // Parameter count

                sParamsLine[0] = new SqlParameter();
                sParamsLine[0].SqlDbType = SqlDbType.VarChar;
                sParamsLine[0].ParameterName = "@production";
                sParamsLine[0].Value = production;

                sParamsLine[1] = new SqlParameter();
                sParamsLine[1].SqlDbType = SqlDbType.NVarChar;
                sParamsLine[1].ParameterName = "@line";
                sParamsLine[1].Value = line;

                sParamsLine[2] = new SqlParameter();
                sParamsLine[2].SqlDbType = SqlDbType.Bit;
                sParamsLine[2].ParameterName = "@active";
                sParamsLine[2].Value = active;

                cls.fnUpdDel(sqlLine, sParamsLine);
            }
        }

        public void fnLineUpd()
        {
            string idx = _idxLine;
            string line = txtAssyLine.Text.Trim();
            byte active = Convert.ToByte((chkLineActive.Checked) ? 1 : 0);

            if (line!="")
            {
                string sqlLine = "";
                sqlLine = "V2_BASE_CAPACITY_LINE_DEFINE_UPDITEM_ADDNEW";
                SqlParameter[] sParamsLine = new SqlParameter[3]; // Parameter count

                sParamsLine[0] = new SqlParameter();
                sParamsLine[0].SqlDbType = SqlDbType.Int;
                sParamsLine[0].ParameterName = "@idx";
                sParamsLine[0].Value = idx;

                sParamsLine[1] = new SqlParameter();
                sParamsLine[1].SqlDbType = SqlDbType.NVarChar;
                sParamsLine[1].ParameterName = "@line";
                sParamsLine[1].Value = line;

                sParamsLine[2] = new SqlParameter();
                sParamsLine[2].SqlDbType = SqlDbType.Bit;
                sParamsLine[2].ParameterName = "@active";
                sParamsLine[2].Value = active;

                cls.fnUpdDel(sqlLine, sParamsLine);
            }
        }

        public void fnLineDel()
        {
            string idx = _idxLine;

            string sqlLine = "";
            sqlLine = "V2_BASE_CAPACITY_LINE_DEFINE_DELITEM_ADDNEW";
            SqlParameter[] sParamsLine = new SqlParameter[1]; // Parameter count

            sParamsLine[0] = new SqlParameter();
            sParamsLine[0].SqlDbType = SqlDbType.Int;
            sParamsLine[0].ParameterName = "@idx";
            sParamsLine[0].Value = idx;

            cls.fnUpdDel(sqlLine, sParamsLine);
        }

        private void dgvAssyLine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvAssyLine, e);
            DataGridViewRow rowLine = new DataGridViewRow();
            rowLine = dgvAssyLine.Rows[e.RowIndex];
            string idx = rowLine.Cells[0].Value.ToString();
            string production = rowLine.Cells[1].Value.ToString().Trim();
            string line = rowLine.Cells[2].Value.ToString();
            string active= rowLine.Cells[3].Value.ToString();

            _idxLine = idx;

            cbbAssyMachine.SelectedItem = production;
            txtAssyLine.Text = line;
            chkLineActive.Checked = (active.ToLower() == "true") ? true : false;

            cbbAssyMachine.Enabled = false;
            txtAssyLine.Enabled = true;

            rdbLineAdd.Enabled = false;
            rdbLineAdd.Checked = false;
            rdbLineUpd.Enabled = true;
            rdbLineDel.Enabled = true;
            btnLineSave.Enabled = false;
        }

        private void rdbLineUpd_CheckedChanged(object sender, EventArgs e)
        {
            btnLineSave.Enabled = true;
        }

        private void rdbLineDel_CheckedChanged(object sender, EventArgs e)
        {
            btnLineSave.Enabled = true;
        }

        public void fnLineFinish()
        {
            cbbAssyMachine.SelectedIndex = 0;
            cbbAssyMachine.Enabled = true;
            txtAssyLine.Enabled = false;
            txtAssyLine.Text = "";
            rdbLineAdd.Enabled = false;
            rdbLineUpd.Enabled = false;
            rdbLineDel.Enabled = false;
            btnLineSave.Enabled = false;
        }

        private void cbbPIC_Title_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbbPIC_Title.SelectedIndex > 0)
            {
                txtPIC_Name.Enabled = true;
                txtPIC_Name.Focus();
            }
            else
            {
                txtPIC_Name.Enabled = false;
            }
        }

        private void txtPIC_Name_TextChanged(object sender, EventArgs e)
        {
            if (txtPIC_Name.Text != "")
            {
                if (_idxPIC != "")
                {
                    // Update
                    rdbPIC_Add.Enabled = false;
                    rdbPIC_Add.Checked = false;
                    rdbPIC_Upd.Enabled = true;
                    rdbPIC_Del.Enabled = true;
                    //btnPIC_Save.Enabled = false;
                    btnPIC_Finish.Enabled = true;
                }
                else
                {
                    // Addnew
                    rdbPIC_Add.Enabled = true;
                    rdbPIC_Add.Checked = true;
                    rdbPIC_Upd.Enabled = false;
                    rdbPIC_Del.Enabled = false;
                    //btnPIC_Save.Enabled = true;
                    btnPIC_Finish.Enabled = true;
                }
                if(rdbPIC_Add.Checked)
                {
                    btnPIC_Save.Enabled = true;
                }
                else
                {
                    btnPIC_Save.Enabled = false;
                }
            }
            else
            {
                rdbPIC_Add.Enabled = false;
                rdbPIC_Add.Checked = false;
                rdbPIC_Upd.Enabled = false;
                rdbPIC_Del.Enabled = false;
                btnPIC_Save.Enabled = false;
                btnPIC_Finish.Enabled = false;
            }
        }

        private void btnPIC_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", _appName, MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                if (rdbPIC_Add.Checked)
                {
                    fnPIC_Add();
                }
                else if (rdbPIC_Upd.Checked)
                {
                    fnPIC_Upd();
                }
                else if (rdbPIC_Del.Checked)
                {
                    fnPIC_Del();
                }
                fnBindPicLine();
                fnPIC_Finish();
            }
        }

        public void fnPIC_Add()
        {
            string picTitle = cbbPIC_Title.Text;
            string picName = txtPIC_Name.Text.Trim();

            if (picName != "")
            {
                string sql = "";
                sql = "V2_BASE_CAPACITY_LINE_DEFINE_PIC_ADDITEM_ADDNEW";
                SqlParameter[] sParamsLine = new SqlParameter[2]; // Parameter count

                sParamsLine[0] = new SqlParameter();
                sParamsLine[0].SqlDbType = SqlDbType.NVarChar;
                sParamsLine[0].ParameterName = "@picTitle";
                sParamsLine[0].Value = picTitle;

                sParamsLine[1] = new SqlParameter();
                sParamsLine[1].SqlDbType = SqlDbType.NVarChar;
                sParamsLine[1].ParameterName = "@picName";
                sParamsLine[1].Value = picName;

                cls.fnUpdDel(sql, sParamsLine);
            }

        }

        public void fnPIC_Upd()
        {
            string picIdx = _idxPIC;
            string picTitle = cbbPIC_Title.Text;
            string picName = txtPIC_Name.Text.Trim();

            if (picIdx != "")
            {
                string sql = "";
                sql = "V2_BASE_CAPACITY_LINE_DEFINE_PIC_UPDITEM_ADDNEW";
                SqlParameter[] sParamsPIC = new SqlParameter[3]; // Parameter count

                sParamsPIC[0] = new SqlParameter();
                sParamsPIC[0].SqlDbType = SqlDbType.Int;
                sParamsPIC[0].ParameterName = "@picIdx";
                sParamsPIC[0].Value = picIdx;

                sParamsPIC[1] = new SqlParameter();
                sParamsPIC[1].SqlDbType = SqlDbType.NVarChar;
                sParamsPIC[1].ParameterName = "@picTitle";
                sParamsPIC[1].Value = picTitle;

                sParamsPIC[2] = new SqlParameter();
                sParamsPIC[2].SqlDbType = SqlDbType.NVarChar;
                sParamsPIC[2].ParameterName = "@picName";
                sParamsPIC[2].Value = picName;

                cls.fnUpdDel(sql, sParamsPIC);
            }
        }

        public void fnPIC_Del()
        {
            string picIdx = _idxPIC;

            if (picIdx != "")
            {
                string sql = "";
                sql = "V2_BASE_CAPACITY_LINE_DEFINE_PIC_DELITEM_ADDNEW";
                SqlParameter[] sParamsPIC = new SqlParameter[3]; // Parameter count

                sParamsPIC[0] = new SqlParameter();
                sParamsPIC[0].SqlDbType = SqlDbType.Int;
                sParamsPIC[0].ParameterName = "@picIdx";
                sParamsPIC[0].Value = picIdx;

                cls.fnUpdDel(sql, sParamsPIC);
            }
        }

        private void btnPIC_Finish_Click(object sender, EventArgs e)
        {
            fnPIC_Finish();
        }

        public void fnPIC_Finish()
        {
            _idxPIC = "";

            cbbPIC_Title.SelectedIndex = 0;

            txtPIC_Name.Enabled = false;
            txtPIC_Name.Text = "";

            rdbPIC_Add.Enabled = false;
            rdbPIC_Upd.Enabled = false;
            rdbPIC_Del.Enabled = false;

            rdbPIC_Add.Checked = false;
            rdbPIC_Upd.Checked = false;
            rdbPIC_Del.Checked = false;

            btnPIC_Save.Enabled = false;
            btnPIC_Finish.Enabled = false;
        }

        private void dgvIdleTime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvPIC, e);
            DataGridViewRow rowLine = new DataGridViewRow();
            rowLine = dgvPIC.Rows[e.RowIndex];
            string idx = rowLine.Cells[0].Value.ToString();
            string picTitle = rowLine.Cells[1].Value.ToString();
            string picName = rowLine.Cells[2].Value.ToString();

            _idxPIC = idx;

            cbbPIC_Title.Text = picTitle;
            txtPIC_Name.Text = picName;
            txtPIC_Name.Focus();

            cbbPIC_Title.Enabled = true;
            txtPIC_Name.Enabled = true;

            rdbPIC_Add.Enabled = false;
            rdbPIC_Add.Checked = false;
            rdbPIC_Upd.Enabled = true;
            rdbPIC_Del.Enabled = true;
            btnPIC_Save.Enabled = false;
            btnPIC_Finish.Enabled = true;
        }

        private void rdbPIC_Del_CheckedChanged(object sender, EventArgs e)
        {
            btnPIC_Save.Enabled = true;
        }

        private void rdbPIC_Upd_CheckedChanged(object sender, EventArgs e)
        {
            btnPIC_Save.Enabled = true;
        }
    }
}

