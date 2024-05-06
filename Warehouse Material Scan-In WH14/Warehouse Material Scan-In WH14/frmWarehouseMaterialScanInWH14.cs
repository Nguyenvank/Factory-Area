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
    public partial class frmWarehouseMaterialScanInWH14 : Form
    {
        public string _locId = "120";     //warehouse ID
        public string _subId;           //sub-locate
        public string _matId;           //material ID

        public DataGridViewRow _rowMat;
        public DataGridViewRow _rowSub;
        public DataGridViewRow _rowLst;

        public int _dgvMatListWidth;
        public int _dgvScanListWidth;
        public int _dgvLocListWidth;

        private int rowIndex = 0;

        public frmWarehouseMaterialScanInWH14()
        {
            InitializeComponent();
        }

        private void frmWarehouseMaterialScanInWH14_Load(object sender, EventArgs e)
        {
            _dgvMatListWidth = cls.fnGetDataGridWidth(dgvMatList);
            _dgvScanListWidth = cls.fnGetDataGridWidth(dgvScanList);
            _dgvLocListWidth = cls.fnGetDataGridWidth(dgvLocList);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvMatListWidth = cls.fnGetDataGridWidth(dgvMatList);
            _dgvScanListWidth = cls.fnGetDataGridWidth(dgvScanList);
            _dgvLocListWidth = cls.fnGetDataGridWidth(dgvLocList);

            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
            initMatList();
        }

        public void fnGetdate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
            if (check.IsConnectedToInternet() == true)
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

        public void initMatList()
        {
            lblPartName.Text = "N/A";
            lblPartCode.Text = "N/A";
            lblLocation.Text = "N/A";
            txtLotNumber.Text = cls.fnGetDate("lot") + "-" + cls.fnGetDate("sn");
            //txtLotNumber.Text = cls.fnGetDate("lot");
            txtLotNumber.Enabled = false;
            dtpInStockDate.Value = DateTime.Now;
            dtpInStockDate.Enabled = false;

            lblPCS.Text = "0";
            lblBOX.Text = "0";
            lblPAK.Text = "0";
            lblPAL.Text = "0";

            lblPCS.Enabled = false;
            lblBOX.Enabled = false;
            lblPAK.Enabled = false;
            lblPAL.Enabled = false;
            tableLayoutPanel3.Enabled = false;

            cbbCountBy.Enabled = false;
            cbbCountBy.Items.Add("Today");
            cbbCountBy.Items.Add("This week");
            cbbCountBy.Items.Add("This month");
            cbbCountBy.Items.Insert(0, "");
            cbbCountBy.SelectedIndex = 0;
            lblScanInQty.Text = "0";
            lblScanOutQty.Text = "0";
            lblReturnQty.Text = "0";
            lblRemainTotal.Text = "0";
            groupBox1.Enabled = false;

            txtPackCode.Text = "";
            txtPackCode.Enabled = false;

            fnBindMatList(_locId);

            lblNoteFor.Text = "N/A";
            btnNoteSave.Enabled = false;
            txtNoteContent.Enabled = false;
        }

        public void fnBindMatList(string _locId)
        {
            try
            {
                string sqlMat = "V2o1_BASE_Warehouse_Material_ScanIn_SelItem_Quantity_Addnew";
                DataTable dtMat = new DataTable();
                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "whId";
                sParams[0].Value = _locId;

                dtMat = cls.ExecuteDataTable(sqlMat, sParams);
                dgvMatList.DataSource = dtMat;
                dgvMatList.Refresh();


                //dgvWH_Quantity.Columns[0].Width = 20 * _dgvWH_QuantityWidth / 100;    // idx
                dgvMatList.Columns[1].Width = 70 * _dgvMatListWidth / 100;    // Machine
                dgvMatList.Columns[2].Width = 30 * _dgvMatListWidth / 100;    // Line

                dgvMatList.Columns[0].Visible = false;
                dgvMatList.Columns[1].Visible = true;
                dgvMatList.Columns[2].Visible = true;

                cls.fnFormatDatagridview(dgvMatList, 12);

                if(_rowMat!=null)
                {
                    _rowMat.Selected = true;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvMatList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvMatList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvMatList.Rows[e.RowIndex];
            _rowMat = row;

            string idx = row.Cells[0].Value.ToString();
            string code = row.Cells[1].Value.ToString();


            _matId = idx;
            _subId = "";
            _rowSub = null;
            _rowLst = null;
            lblLocation.Text = "N/A";
            txtLotNumber.Text = cls.fnGetDate("lot") + "-" + cls.fnGetDate("sn");
            txtLotNumber.Enabled = false;
            dtpInStockDate.Value = DateTime.Now;
            dtpInStockDate.Enabled = false;
            txtPackCode.Text = "";
            txtPackCode.Enabled = false;

            fnGetQty(_matId);
            fnBindWH_Packing(_locId, _matId);
            fnBindWH_Location(_locId, _matId);

            lblNoteFor.Text = "Note for " + code + ":";
            btnNoteSave.Enabled = true;
            txtNoteContent.Enabled = true;
            string sqlNote = "V2o1_BASE_Warehouse_Material_ScanIn_Note_SelItem_Addnew";

            SqlParameter[] sParamsNote = new SqlParameter[1]; // Parameter count
            sParamsNote[0] = new SqlParameter();
            sParamsNote[0].SqlDbType = SqlDbType.Int;
            sParamsNote[0].ParameterName = "@matId";
            sParamsNote[0].Value = idx;

            DataTable dtNote = new DataTable();
            dtNote = cls.ExecuteDataTable(sqlNote, sParamsNote);

            txtNoteContent.Text = (dtNote.Rows.Count > 0) ? dtNote.Rows[0][0].ToString() : "";
        }

        private void dgvMatList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        public void fnBindWH_Location(string whId, string invId)
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanIn_SelItem_Location_Addnew";
                DataTable dt = new DataTable();
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "whId";
                sParams[0].Value = whId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "prodId";
                sParams[1].Value = invId;

                dt = cls.ExecuteDataTable(sql, sParams);

                //foreach (DataRow dr in dt.Rows)
                //{
                //    string qty = dr[2].ToString();
                //    if (qty == "0")
                //    {
                //        dr[3] = "";
                //    }
                //}

                dgvLocList.DataSource = dt;
                dgvLocList.Refresh();


                //dgvLocList.Columns[0].Width = 20 * _dgvLocListWidth / 100;    // idx
                dgvLocList.Columns[1].Width = 40 * _dgvLocListWidth / 100;    // Location
                dgvLocList.Columns[2].Width = 20 * _dgvLocListWidth / 100;    // Quantity
                dgvLocList.Columns[3].Width = 40 * _dgvLocListWidth / 100;    // IN Date
                                                                              //dgvLocList.Columns[4].Width = 30 * _dgvLocListWidth / 100;    // LocId

                dgvLocList.Columns[0].Visible = false;
                dgvLocList.Columns[1].Visible = true;
                dgvLocList.Columns[2].Visible = true;
                dgvLocList.Columns[3].Visible = true;
                dgvLocList.Columns[4].Visible = false;

                dgvLocList.Columns[0].ReadOnly = true;
                dgvLocList.Columns[1].ReadOnly = true;
                dgvLocList.Columns[2].ReadOnly = true;
                dgvLocList.Columns[3].ReadOnly = true;
                dgvLocList.Columns[4].ReadOnly = true;

                cls.fnFormatDatagridview(dgvLocList, 12);

                _rowSub.Selected = (_rowSub != null) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnGetQty(string prodId)
        {
            try
            {
                string time = "";
                switch (cbbCountBy.SelectedIndex)
                {
                    case 1:
                        time = "day";
                        break;
                    case 2:
                        time = "week";
                        break;
                    case 3:
                        time = "month";
                        break;
                }
                string sql = "V2o1_BASE_Warehouse_Material_ScanIn_CurrItem_Quantity_Addnew";
                DataSet ds = new DataSet();

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodId";
                sParams[0].Value = prodId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.VarChar;
                sParams[1].ParameterName = "@time";
                sParams[1].Value = time;

                ds = cls.ExecuteDataSet(sql, sParams);

                string pcs = "", box = "", pak = "", pal = "";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    cbbCountBy.Enabled = true;
                    //cbbCountBy.SelectedIndex = 1;
                    lblScanInQty.Text = ds.Tables[0].Rows[0][0].ToString();
                    lblScanOutQty.Text = ds.Tables[0].Rows[0][1].ToString();
                    lblReturnQty.Text = ds.Tables[0].Rows[0][2].ToString();
                    lblRemainTotal.Text = ds.Tables[0].Rows[0][3].ToString();

                    pcs = ds.Tables[0].Rows[0][4].ToString();
                    box = ds.Tables[0].Rows[0][5].ToString();
                    pak = ds.Tables[0].Rows[0][6].ToString();
                    pal = ds.Tables[0].Rows[0][7].ToString();

                    lblPartName.Text = ds.Tables[0].Rows[0][8].ToString();
                    lblPartCode.Text = ds.Tables[0].Rows[0][9].ToString();

                    tableLayoutPanel3.Enabled = true;
                    groupBox1.Enabled = true;
                }
                else
                {
                    lblScanInQty.Text = "0";
                    lblScanOutQty.Text = "0";
                    lblReturnQty.Text = "0";
                    lblRemainTotal.Text = "0";

                    pcs = "0";
                    box = "0";
                    pak = "0";
                    pal = "0";

                    lblPartName.Text = "N/A";
                    lblPartCode.Text = "N/A";

                    tableLayoutPanel3.Enabled = false;
                    groupBox1.Enabled = false;
                }
                lblPCS.Text = pcs;
                lblBOX.Text = box;
                lblPAK.Text = pak;
                lblPAL.Text = pal;

                lblPCS.Enabled = (lblPCS.Text == "0") ? false : false;
                lblBOX.Enabled = (lblBOX.Text == "0") ? false : false;
                lblPAK.Enabled = (lblPAK.Text == "0") ? false : false;
                lblPAL.Enabled = (lblPAL.Text == "0") ? false : false;

                lblLocation.BackColor = Color.Yellow;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void cbbCountBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCountBy.SelectedIndex > 0)
            {
                lblScanInBy.Text = "Scan IN in " + cbbCountBy.Text.ToLower();
                lblScanOutBy.Text = "Scan OUT in " + cbbCountBy.Text.ToLower();
                lblReturnBy.Text = "Returned in " + cbbCountBy.Text.ToLower();

                fnGetQty(_matId);
            }
            else
            {
                cbbCountBy.SelectedIndex = 1;
            }
        }

        private void dgvLocList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvLocList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvLocList.Rows[e.RowIndex];
            _rowSub = row;
            _rowLst = null;
            string idx = row.Cells[0].Value.ToString();
            string sublocate = row.Cells[1].Value.ToString();
            string quantity = row.Cells[2].Value.ToString();
            string lot = txtLotNumber.Text.Trim();

            if (quantity != "0")
            {
                DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    _subId = sublocate;

                    lblLocation.Text = sublocate;
                    lblLocation.BackColor = Color.FromKnownColor(KnownColor.Control);
                    dtpInStockDate.Enabled = true;

                    txtLotNumber.Enabled = true;
                    if (lot != "" && lot != null)
                    {
                        txtPackCode.Enabled = true;
                        txtPackCode.Focus();
                    }
                    else
                    {
                        txtPackCode.Enabled = false;
                        txtLotNumber.Focus();
                    }
                }
                else
                {
                    _subId = "";
                    lblLocation.Text = "N/A";
                    lblLocation.BackColor = Color.Yellow;
                    dtpInStockDate.Value = DateTime.Now;
                    dtpInStockDate.Enabled = false;
                    txtLotNumber.Text = cls.fnGetDate("lot") + "-" + cls.fnGetDate("sn");
                    txtLotNumber.Enabled = false;
                    txtPackCode.Text = "";
                    txtPackCode.Enabled = false;
                }
            }
            else
            {
                _subId = sublocate;

                lblLocation.Text = sublocate;
                lblLocation.BackColor = Color.FromKnownColor(KnownColor.Control);
                dtpInStockDate.Enabled = true;

                txtLotNumber.Enabled = true;
                if (lot != "" && lot != null)
                {
                    txtPackCode.Enabled = true;
                    txtPackCode.Focus();
                }
                else
                {
                    txtPackCode.Enabled = false;
                    txtLotNumber.Focus();
                }
            }
        }

        private void dgvLocList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void txtLotNumber_TextChanged(object sender, EventArgs e)
        {
            string lot = txtLotNumber.Text.Trim();
            if (lot != "" && lot != null)
            {
                txtPackCode.Enabled = true;
            }
            else
            {
                txtPackCode.Enabled = false;
            }
        }

        private void txtPackCode_KeyDown(object sender, KeyEventArgs e)
        {
            string locId = _locId;  //warehouse
            string matId = _matId;  //material
            string subId = _subId;  //location
            string packCode = txtPackCode.Text.Trim();
            string codeCheck = "", codeType = "", codeID = "";
            string matName = lblPartName.Text;
            string matCode = lblPartCode.Text;
            string lotNumber = txtLotNumber.Text.Trim();
            decimal pcs = Convert.ToDecimal(lblPCS.Text);
            decimal box = Convert.ToDecimal(lblBOX.Text);
            decimal pak = Convert.ToDecimal(lblPAK.Text);
            decimal pal = Convert.ToDecimal(lblPAL.Text);
            decimal packQty = 0;
            string packUnit = "";
            DateTime insDate = dtpInStockDate.Value;

            if (e.KeyCode == Keys.Enter)
            {
                if (packCode != "" && packCode != null)
                {
                    if (packCode.IndexOf("-") > 0)
                    {
                        codeCheck = cls.Left(packCode, 3);
                        codeType = cls.Mid(packCode, 4, 3);
                        codeID = cls.Right(packCode, 5);
                        switch (codeType.ToUpper())
                        {
                            case "PCS":
                                packQty = pcs;
                                packUnit = "piece";
                                break;
                            case "BOX":
                                packQty = box;
                                packUnit = "box";
                                break;
                            case "PAK":
                                packQty = pak;
                                packUnit = "pack";
                                break;
                            case "PAL":
                                packQty = pal;
                                packUnit = "pallete";
                                break;
                        }

                        if (cls.fnCheckPackCode(packCode))
                        {
                            if (codeCheck.ToUpper() != "PRO")
                            {
                                if (packQty > 0)
                                {
                                    string sqlChk = "V2o1_BASE_Warehouse_Material_ScanIn_ChkItem_Addnew";
                                    SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                                    sParamsChk[0] = new SqlParameter();
                                    sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                                    sParamsChk[0].ParameterName = "@packCode";
                                    sParamsChk[0].Value = packCode;

                                    DataTable dtChk = new DataTable();
                                    dtChk = cls.ExecuteDataTable(sqlChk, sParamsChk);
                                    if (dtChk.Rows.Count > 0)
                                    {
                                    	string sqlChkExist="V2o1_BASE_Warehouse_Material_ScanIn_ChkBarcode_Exist_Addnew";
	                                    SqlParameter[] sParamsChkExist = new SqlParameter[1]; // Parameter count
	
	                                    sParamsChkExist[0] = new SqlParameter();
	                                    sParamsChkExist[0].SqlDbType = SqlDbType.VarChar;
	                                    sParamsChkExist[0].ParameterName = "@packCode";
	                                    sParamsChkExist[0].Value = packCode;
                                    	
	                                    DataSet dsExist=new DataSet();
	                                    dsExist=cls.ExecuteDataSet(sqlChkExist,sParamsChkExist);
	                                    if(dsExist.Tables.Count<=0 || dsExist.Tables[0].Rows.Count<=0)
	                                    {
	                                        //lblMessage.Text = "Right type of packing code.";
	                                        string sql = "V2o1_BASE_Warehouse_Material_ScanIn_AddItem_Addnew";
	                                        SqlParameter[] sParams = new SqlParameter[9]; // Parameter count
	
	                                        sParams[0] = new SqlParameter();
	                                        sParams[0].SqlDbType = SqlDbType.Int;
	                                        sParams[0].ParameterName = "@locId";
	                                        sParams[0].Value = locId;
	
	                                        sParams[1] = new SqlParameter();
	                                        sParams[1].SqlDbType = SqlDbType.VarChar;
	                                        sParams[1].ParameterName = "@subId";
	                                        sParams[1].Value = subId;
	
	                                        sParams[2] = new SqlParameter();
	                                        sParams[2].SqlDbType = SqlDbType.Int;
	                                        sParams[2].ParameterName = "@matId";
	                                        sParams[2].Value = matId;
	
	                                        sParams[3] = new SqlParameter();
	                                        sParams[3].SqlDbType = SqlDbType.NVarChar;
	                                        sParams[3].ParameterName = "@matName";
	                                        sParams[3].Value = matName;
	
	                                        sParams[4] = new SqlParameter();
	                                        sParams[4].SqlDbType = SqlDbType.VarChar;
	                                        sParams[4].ParameterName = "@matCode";
	                                        sParams[4].Value = matCode;
	
	                                        sParams[5] = new SqlParameter();
	                                        sParams[5].SqlDbType = SqlDbType.VarChar;
	                                        sParams[5].ParameterName = "@packCode";
	                                        sParams[5].Value = packCode;
	
	                                        sParams[6] = new SqlParameter();
	                                        sParams[6].SqlDbType = SqlDbType.SmallMoney;
	                                        sParams[6].ParameterName = "@packQty";
	                                        sParams[6].Value = packQty;
	
	                                        sParams[7] = new SqlParameter();
	                                        sParams[7].SqlDbType = SqlDbType.VarChar;
	                                        sParams[7].ParameterName = "@lotNumber";
	                                        sParams[7].Value = lotNumber;
	
	                                        sParams[8] = new SqlParameter();
	                                        sParams[8].SqlDbType = SqlDbType.DateTime;
	                                        sParams[8].ParameterName = "@insDate";
	                                        sParams[8].Value = insDate;
	
	                                        cls.fnUpdDel(sql, sParams);
	
	                                        fnGetQty(matId);
	                                        fnBindMatList(locId);
	                                        fnBindWH_Location(locId, matId);
	                                        fnBindWH_Packing(locId, matId);
	
	                                        _locId = locId;
	                                        _subId = subId;
	                                        _matId = matId;
	
	                                        txtPackCode.Text = "";
	                                        txtPackCode.Focus();
	
	                                        //lblMessage.Text = "In-stock " + packCode.ToUpper() + " successful at " + DateTime.Now;
	                                        lblMessage.Text = "Nhập kho " + packCode.ToUpper() + " thành công lúc " + DateTime.Now;
	                                    }
	                                    else
	                                    {
	                                    	lblMessage.Text = "Không thể nhập kho cho " + codeType.ToUpper() + " vì đã có mã tem này trên hệ thống.";
	                                    }
                                    }
                                    else
                                    {
                                        //lblMessage.Text = "Cannot in-stock for " + codeType.ToUpper() + " because it still in-stock already";
                                        lblMessage.Text = "Không thể nhập kho cho " + codeType.ToUpper() + " khi tình trạng vẫn 'trong kho'";
                                    }
                                }
                                else
                                {
                                    //lblMessage.Text = "Please SET packing quantity for " + codeType.ToUpper() + " before scan in";
                                    lblMessage.Text = "Vui lòng thiết lập số lượng cho " + codeType.ToUpper() + " trước khi nhập kho";
                                }
                            }
                            else
                            {
                                //lblMessage.Text = "Please scan/input packing for warehouse code.\r\nValid barcode for warehouse's packing must be start with MMT";
                                lblMessage.Text = "Vui lòng quét/nhập mã packing.\r\nMã vạch đúng cho warehouse packing phải bắt đầu bằng 'MMT'";
                            }
                        }
                        else
                        {
                            //lblMessage.Text = "Invalid packing code. Please try again.";
                            lblMessage.Text = "Sai mã packing. Vui lòng thử lại";
                        }
                    }
                    else
                    {
                        //lblMessage.Text = "Invalid packing code. Please try again.";
                        lblMessage.Text = "Không đúng kiểu cho mã packing. Vui lòng thử lại.";
                    }
                }
                else
                {
                    //lblMessage.Text = "Please scan/input a valid packing code.";
                    lblMessage.Text = "Vui lòng quét/nhập mã đúng cho packing.";
                }
                txtPackCode.Text = "";
                txtPackCode.Focus();
            }
        }

        public void fnBindWH_Packing(string _locId, string _matId)
        {
            try
            {
                string sql = "V2o1_BASE_Warehouse_Material_ScanIn_SelItem_Packing_Addnew";
                DataTable dt = new DataTable();
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@locId";
                sParams[0].Value = _locId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@matId";
                sParams[1].Value = _matId;

                dt = cls.ExecuteDataTable(sql, sParams);

                //foreach (DataRow dr in dt.Rows)
                //{
                //    string qty = dr[2].ToString();
                //    if (qty == "0")
                //    {
                //        dr[3] = "";
                //    }
                //}

                dgvScanList.DataSource = dt;
                dgvScanList.Refresh();


                //dgvScanList.Columns[0].Width = 20 * _dgvScanListWidth / 100;    // inoutID
                //dgvScanList.Columns[1].Width = 40 * _dgvScanListWidth / 100;    // packingID
                dgvScanList.Columns[2].Width = 20 * _dgvScanListWidth / 100;    // packingCode
                //dgvScanList.Columns[3].Width = 40 * _dgvScanListWidth / 100;    // partId
                dgvScanList.Columns[4].Width = 20 * _dgvScanListWidth / 100;    // partName
                dgvScanList.Columns[5].Width = 15 * _dgvScanListWidth / 100;    // partNumber
                dgvScanList.Columns[6].Width = 8 * _dgvScanListWidth / 100;    // packingQty
                dgvScanList.Columns[7].Width = 5 * _dgvScanListWidth / 100;    // uom
                dgvScanList.Columns[8].Width = 12 * _dgvScanListWidth / 100;    // importLOT
                dgvScanList.Columns[9].Width = 10 * _dgvScanListWidth / 100;    // receiveDate
                //dgvScanList.Columns[10].Width = 30 * _dgvScanListWidth / 100;    // transferLocationID
                //dgvScanList.Columns[11].Width = 30 * _dgvScanListWidth / 100;    // transferLocation
                dgvScanList.Columns[12].Width = 10 * _dgvScanListWidth / 100;    // transferSublocate

                dgvScanList.Columns[0].Visible = false;
                dgvScanList.Columns[1].Visible = false;
                dgvScanList.Columns[2].Visible = true;
                dgvScanList.Columns[3].Visible = false;
                dgvScanList.Columns[4].Visible = true;
                dgvScanList.Columns[5].Visible = true;
                dgvScanList.Columns[6].Visible = true;
                dgvScanList.Columns[7].Visible = true;
                dgvScanList.Columns[8].Visible = true;
                dgvScanList.Columns[9].Visible = true;
                dgvScanList.Columns[10].Visible = false;
                dgvScanList.Columns[11].Visible = false;
                dgvScanList.Columns[12].Visible = true;

                cls.fnFormatDatagridview(dgvScanList, 11);

                _rowLst.Selected = (_rowLst != null) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvScanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvScanList, e);
            DataGridViewRow row = new DataGridViewRow();
            row = dgvScanList.Rows[e.RowIndex];
            _rowLst = row;
        }

        private void dgvScanList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvScanList.Rows[e.RowIndex].Selected = true;
                rowIndex = e.RowIndex;
                dgvScanList.CurrentCell = dgvScanList.Rows[e.RowIndex].Cells[2];
                contextMenuStrip1.Show(this.dgvScanList, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void btnExport2Excel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Please wait for generate data to export.";
            byte export = 0;
            try
            {
                fnBindWH_Packing(_locId, "0");
                export = cls.ExportToExcel(dgvScanList, "Rubber " + String.Format("{0:dd-MM-yyyy}", DateTime.Now));
                lblMessage.Text = (export == 1) ? "Export to excel successful." : "Export to excel failure. Please try again.";
                fnBindWH_Packing(_locId, _matId);
                dgvScanList.Refresh();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void deleteThisPackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dgvScanList.Rows[rowIndex].IsNewRow)
            {
                //dgvScanList.Rows.RemoveAt(rowIndex);
                string idx = dgvScanList.Rows[rowIndex].Cells[0].Value.ToString();
                string packId = dgvScanList.Rows[rowIndex].Cells[1].Value.ToString();
                string matId = dgvScanList.Rows[rowIndex].Cells[3].Value.ToString();
                string subId = dgvScanList.Rows[rowIndex].Cells[12].Value.ToString();
                string locId = dgvScanList.Rows[rowIndex].Cells[10].Value.ToString();
                string insDate = dgvScanList.Rows[rowIndex].Cells[9].Value.ToString();

                string msg = "";
                msg += "idx: " + idx + "\r\n";
                msg += "packId: " + packId + "\r\n";
                msg += "locId: " + locId + "\r\n";
                msg += "subId: " + subId + "\r\n";
                msg += "matId: " + matId + "\r\n";
                msg += "locId: " + locId + "\r\n";
                MessageBox.Show(msg);

                string packCode = dgvScanList.Rows[rowIndex].Cells[2].Value.ToString();
                DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialogResultAdd == DialogResult.Yes)
                {
                    string codeCheck = "", codeType = "", codeID = "";
                    string matName = lblPartName.Text;
                    string matCode = lblPartCode.Text;
                    string lotNumber = txtLotNumber.Text.Trim();
                    decimal pcs = Convert.ToDecimal(lblPCS.Text);
                    decimal box = Convert.ToDecimal(lblBOX.Text);
                    decimal pak = Convert.ToDecimal(lblPAK.Text);
                    decimal pal = Convert.ToDecimal(lblPAL.Text);
                    decimal packQty = 0;

                    string sql = "V2o1_BASE_Warehouse_Material_ScanIn_DelItem2_Addnew";
                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@idx";
                    sParams[0].Value = idx;

                    cls.fnUpdDel(sql, sParams);

                    lblMessage.Text = "Delete " + packCode + " successful";

                    _rowLst = null;

                    fnGetQty(matId);
                    fnBindMatList(locId);
                    fnBindWH_Location(locId, matId);
                    fnBindWH_Packing(locId, matId);
                }
            }
        }

        private void transferToAnotherLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lblPCS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = lblPCS.Text.Trim();
                int _qty = (qty != "" && qty != null && cls.Left(qty, 1) != "-") ? Convert.ToInt32(qty) : 0;
                lblPCS.Text = _qty.ToString();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void lblBOX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = lblBOX.Text.Trim();
                int _qty = (qty != "" && qty != null && cls.Left(qty, 1) != "-") ? Convert.ToInt32(qty) : 0;
                lblBOX.Text = _qty.ToString();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void lblPAK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = lblPAK.Text.Trim();
                int _qty = (qty != "" && qty != null && cls.Left(qty, 1) != "-") ? Convert.ToInt32(qty) : 0;
                lblPAK.Text = _qty.ToString();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void lblPAL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string qty = lblPAL.Text.Trim();
                int _qty = (qty != "" && qty != null && cls.Left(qty, 1) != "-") ? Convert.ToInt32(qty) : 0;
                lblPAL.Text = _qty.ToString();
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnNoteSave_Click(object sender, EventArgs e)
        {
            string note = txtNoteContent.Text.Trim();
            string sql = "V2o1_BASE_Warehouse_Material_ScanIn_Note_AddItem_Addnew";
            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@matId";
            sParams[0].Value = _matId;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.NVarChar;
            sParams[1].ParameterName = "@note";
            sParams[1].Value = note;

            cls.fnUpdDel(sql, sParams);
            lblMessage.Text = "Update note successful.";
        }
    }
}
