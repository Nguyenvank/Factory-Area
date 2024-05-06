using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Warehouse_Change_Sublocate : UserControl
    {
        private static uc_Warehouse_Change_Sublocate _instance;
        public static uc_Warehouse_Change_Sublocate Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Warehouse_Change_Sublocate();
                return _instance;
            }
        }

        string _msg = "", _msg_en = "", _msg_vi = "";
        int _msg_no = 0;

        int _makerIDx, _makerLevel, _makerDept;

        string _makerName;

        System.Windows.Forms.Form frm = System.Windows.Forms.Application.OpenForms["frmInventoryBeginOfDay"];
        DateTime _date;

        /******************************************/

        string _locIDx = "";
        string _matIDx = "", _matName = "", _matCode = "", _matUnit = "", _matPCS = "", _matBOX = "", _matPAK = "", _matPAL = "", _matSUM = "";
        string _inoutIDx = "", _packingCode = "", _importLOT = "", _sublocate = "", _unit = "", _quantity = "", _inDate = "";

        /******************************************/

        public uc_Warehouse_Change_Sublocate()
        {
            InitializeComponent();
        }

        private void Uc_Warehouse_Change_Sublocate_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            Init_Locate_Name();
            Init_Locate_Done();
        }

        public void Init_Locate_Name()
        {
            //BASE_Warehouse_Materials_Location_Name_SelItem_V1o0_Addnew
            string sql = "V2o1_BASE_Warehouse_Materials_Location_Name_SelItem_V1o0_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            cbb_Loc_Name.DataSource = dt;
            cbb_Loc_Name.DisplayMember = "location";
            cbb_Loc_Name.ValueMember = "locationId";
            dt.Rows.InsertAt(dt.NewRow(), 0);
            cbb_Loc_Name.SelectedIndex = 0;

        }

        public void Init_Locate_Done()
        {
            _matIDx = _matName = _matCode = _matUnit = _matPCS = _matBOX = _matPAK = _matPAL = _matSUM = "";
            _inoutIDx = _packingCode = _importLOT = _sublocate = _unit = _quantity = _inDate = "";

            cbb_Loc_Name.SelectedIndex = 0;
            btn_Loc_Load.Enabled = false;
            dgv_Loc_Items.DataSource = "";

            lbl_Item_Selected_Name.Text = "";
            lbl_Item_Selected_Total.Text = "";
            lbl_Item_Selected_Unit.Text = "";
            dgv_Loc_List.DataSource = "";
            txt_Item_Loc_Filter.Text = "";
            txt_Item_Loc_Filter.Enabled = false;
            lbl_Item_Packing.Text = "";
            lbl_Item_LOT.Text = "";
            lbl_Item_Name.Text = "";
            lbl_Item_Code.Text = "";
            lbl_Item_Loc.Text = "";
            lbl_Item_Loc_Cur.Text = "";
            txt_Item_Loc_New.Text = "";
            txt_Item_Loc_New.Enabled = false;
            lbl_Item_Loc_PCS.Text = "";
            lbl_Item_Loc_BOX.Text = "";
            lbl_Item_Loc_PAK.Text = "";
            lbl_Item_Loc_PAL.Text = "";
            btn_Item_Save.Enabled = false;
            btn_Item_Done.Enabled = false;

            txt_Item_Filter.Text = "";
            txt_Item_Filter.Enabled = false;
            //dgv_Item_List.DataSource = "";
        }

        public void Fnc_Loc_Items_Load()
        {
            string locIDx = _locIDx;
            int listCount = 0;
            //MessageBox.Show(locIDx);
            string sql = "V2o1_BASE_Warehouse_Materials_Location_Info_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@locIDx";
            sParams[0].Value = locIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Loc_Items_Width = cls.fnGetDataGridWidth(dgv_Loc_Items);
            dgv_Loc_Items.DataSource = dt;

            dgv_Loc_Items.Columns[0].Width = 10 * dgv_Loc_Items_Width / 100;    // No.
            //dgv_Loc_Items.Columns[1].Width = 7 * dgv_Loc_Items_Width / 100;    // ProdId.
            dgv_Loc_Items.Columns[2].Width = 50 * dgv_Loc_Items_Width / 100;    // Name.
            dgv_Loc_Items.Columns[3].Width = 40 * dgv_Loc_Items_Width / 100;    // BarCode.
            //dgv_Loc_Items.Columns[4].Width = 7 * dgv_Loc_Items_Width / 100;    // Unit.
            //dgv_Loc_Items.Columns[5].Width = 7 * dgv_Loc_Items_Width / 100;    // PCS.
            //dgv_Loc_Items.Columns[6].Width = 7 * dgv_Loc_Items_Width / 100;    // BOX.
            //dgv_Loc_Items.Columns[7].Width = 30 * dgv_Loc_Items_Width / 100;    // PAK.
            //dgv_Loc_Items.Columns[8].Width = 25 * dgv_Loc_Items_Width / 100;    // PAL.
            //dgv_Loc_Items.Columns[9].Width = 7 * dgv_Loc_Items_Width / 100;    // Total.

            dgv_Loc_Items.Columns[0].Visible = true;
            dgv_Loc_Items.Columns[1].Visible = false;
            dgv_Loc_Items.Columns[2].Visible = true;
            dgv_Loc_Items.Columns[3].Visible = true;
            dgv_Loc_Items.Columns[4].Visible = false;
            dgv_Loc_Items.Columns[5].Visible = false;
            dgv_Loc_Items.Columns[6].Visible = false;
            dgv_Loc_Items.Columns[7].Visible = false;
            dgv_Loc_Items.Columns[8].Visible = false;
            dgv_Loc_Items.Columns[9].Visible = false;

            cls.fnFormatDatagridview(dgv_Loc_Items, 11, 30);
        }

        public void Fnc_Loc_List_Load()
        {
            string locIDx = _locIDx;
            string matIDx = _matIDx;
            int listCount = 0;

            string sql = "V2o1_BASE_Warehouse_Materials_Item_Info_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@locIDx";
            sParams[0].Value = locIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "@matIDx";
            sParams[1].Value = matIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);
            listCount = dt.Rows.Count;

            int dgv_Loc_List_Width = cls.fnGetDataGridWidth(dgv_Loc_List);
            dgv_Loc_List.DataSource = dt;

            dgv_Loc_List.Columns[0].Width = 5 * dgv_Loc_List_Width / 100;    // No.
            //dgv_Loc_List.Columns[1].Width = 7 * dgv_Loc_List_Width / 100;    // inoutID.
            dgv_Loc_List.Columns[2].Width = 30 * dgv_Loc_List_Width / 100;    // packingCode.
            dgv_Loc_List.Columns[3].Width = 15 * dgv_Loc_List_Width / 100;    // importLOT.
            dgv_Loc_List.Columns[4].Width = 15 * dgv_Loc_List_Width / 100;    // defaultSublocate.
            dgv_Loc_List.Columns[5].Width = 5 * dgv_Loc_List_Width / 100;    // uom.
            dgv_Loc_List.Columns[6].Width = 10 * dgv_Loc_List_Width / 100;    // packingQty.
            dgv_Loc_List.Columns[7].Width = 20 * dgv_Loc_List_Width / 100;    // IN_Date.

            dgv_Loc_List.Columns[0].Visible = true;
            dgv_Loc_List.Columns[1].Visible = false;
            dgv_Loc_List.Columns[2].Visible = true;
            dgv_Loc_List.Columns[3].Visible = true;
            dgv_Loc_List.Columns[4].Visible = true;
            dgv_Loc_List.Columns[5].Visible = true;
            dgv_Loc_List.Columns[6].Visible = true;
            dgv_Loc_List.Columns[7].Visible = true;

            dgv_Loc_List.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            cls.fnFormatDatagridview(dgv_Loc_List, 11, 30);

        }

        private void Cbb_Loc_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int sel = cbb_Loc_Name.SelectedIndex;
            if (sel > 0)
            {
                btn_Loc_Load.Enabled = true;
                _locIDx = cbb_Loc_Name.SelectedValue.ToString();
            }
            else
            {
                _locIDx = "";
                Init_Locate_Done();
            }
        }

        private void Btn_Loc_Load_Click(object sender, EventArgs e)
        {
            _matIDx = _matName = _matCode = _matUnit = _matPCS = _matBOX = _matPAK = _matPAL = _matSUM = "";
            _inoutIDx = _packingCode = _importLOT = _sublocate = _unit = _quantity = _inDate = "";

            lbl_Item_Selected_Name.Text = "";
            lbl_Item_Selected_Total.Text = "";
            lbl_Item_Selected_Unit.Text = "";
            dgv_Loc_List.DataSource = "";
            txt_Item_Loc_Filter.Text = "";
            txt_Item_Loc_Filter.Enabled = false;
            lbl_Item_Packing.Text = "";
            lbl_Item_LOT.Text = "";
            lbl_Item_Name.Text = "";
            lbl_Item_Code.Text = "";
            lbl_Item_Loc.Text = "";
            lbl_Item_Loc_Cur.Text = "";
            txt_Item_Loc_New.Text = "";
            txt_Item_Loc_New.Enabled = false;
            lbl_Item_Loc_PCS.Text = "";
            lbl_Item_Loc_BOX.Text = "";
            lbl_Item_Loc_PAK.Text = "";
            lbl_Item_Loc_PAL.Text = "";
            btn_Item_Save.Enabled = false;
            btn_Item_Done.Enabled = false;

            txt_Item_Filter.Text = "";
            txt_Item_Filter.Enabled = false;


            Fnc_Loc_Items_Load();
        }

        private void Dgv_Loc_Items_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string matIDx = "", matName = "", matCode = "", matUnit = "", matPCS = "", matBOX = "", matPAK = "", matPAL = "", matSUM = "";
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Loc_Items, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Loc_Items.Rows[e.RowIndex];

                _matIDx = matIDx = row.Cells[1].Value.ToString();
                _matName = matName = row.Cells[2].Value.ToString();
                _matCode = matCode = row.Cells[3].Value.ToString();
                _matUnit = matUnit = row.Cells[4].Value.ToString();
                _matPCS = matPCS = row.Cells[5].Value.ToString();
                _matBOX = matBOX = row.Cells[6].Value.ToString();
                _matPAK = matPAK = row.Cells[7].Value.ToString();
                _matPAL = matPAL = row.Cells[8].Value.ToString();
                _matSUM = matSUM = row.Cells[9].Value.ToString();

                lbl_Item_Selected_Name.Text = matName + " (" + matCode + ")";
                lbl_Item_Selected_Total.Text = "Stock: " + matSUM;
                lbl_Item_Selected_Unit.Text = matUnit;
                txt_Item_Loc_Filter.Enabled = true;
                txt_Item_Loc_Filter.Focus();

                lbl_Item_Packing.Text = "";
                lbl_Item_LOT.Text = "";
                lbl_Item_Name.Text = "";
                lbl_Item_Code.Text = "";
                lbl_Item_Loc.Text = "";
                lbl_Item_Loc_Cur.Text = "";
                txt_Item_Loc_New.Enabled = false;
                txt_Item_Loc_New.Text = "";
                lbl_Item_Loc_PCS.Text = "";
                lbl_Item_Loc_BOX.Text = "";
                lbl_Item_Loc_PAK.Text = "";
                lbl_Item_Loc_PAL.Text = "";

                btn_Item_Save.Enabled = false;
                btn_Item_Done.Enabled = false;


                Fnc_Loc_List_Load();
            }
        }

        private void Dgv_Loc_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string inoutIDx = "", packingCode = "", importLOT = "", sublocate = "", unit = "", quantity = "", inDate = "";
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Loc_List, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Loc_List.Rows[e.RowIndex];

                _inoutIDx = inoutIDx = row.Cells[1].Value.ToString();
                _packingCode = packingCode = row.Cells[2].Value.ToString();
                _importLOT = importLOT = row.Cells[3].Value.ToString();
                _sublocate = sublocate = row.Cells[4].Value.ToString();
                _unit = unit = row.Cells[5].Value.ToString();
                _quantity = quantity = row.Cells[6].Value.ToString();
                _inDate = inDate = row.Cells[7].Value.ToString();

                lbl_Item_Packing.Text = _packingCode;
                lbl_Item_LOT.Text = _importLOT;
                lbl_Item_Name.Text = _matName;
                lbl_Item_Code.Text = _matCode;
                lbl_Item_Loc.Text = cbb_Loc_Name.Text;
                lbl_Item_Loc_Cur.Text = _sublocate;
                txt_Item_Loc_New.Enabled = true;
                txt_Item_Loc_New.Text = _sublocate;
                lbl_Item_Loc_PCS.Text = _matPCS;
                lbl_Item_Loc_BOX.Text = _matBOX;
                lbl_Item_Loc_PAK.Text = _matPAK;
                lbl_Item_Loc_PAL.Text = _matPAL;

                btn_Item_Save.Enabled = true;
                btn_Item_Done.Enabled = true;
            }
        }

        private void Txt_Item_Loc_New_TextChanged(object sender, EventArgs e)
        {
            int len = txt_Item_Loc_New.Text.Length;
            btn_Item_Save.Enabled = (len > 0) ? true : false;
        }

        private void Txt_Item_Loc_Filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_Loc_List, txt_Item_Loc_Filter, 2);
        }

        private void Btn_Item_Save_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    string locIDx = _locIDx;
                    string inoutIDx = _inoutIDx;
                    string matIDx = _matIDx;
                    string packCode = _packingCode;
                    string oldLoc = _sublocate;
                    string newLoc = txt_Item_Loc_New.Text.Trim();
                    string packQty = _quantity;

                    string sql = "V2o1_BASE_Warehouse_Materials_Item_Sublocate_UpdItem_V1o0_Addnew";

                    SqlParameter[] sParams = new SqlParameter[7]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@locIDx";
                    sParams[0].Value = locIDx;

                    sParams[1] = new SqlParameter();
                    sParams[1].SqlDbType = SqlDbType.Int;
                    sParams[1].ParameterName = "@inoutIDx";
                    sParams[1].Value = inoutIDx;

                    sParams[2] = new SqlParameter();
                    sParams[2].SqlDbType = SqlDbType.Int;
                    sParams[2].ParameterName = "@matIDx";
                    sParams[2].Value = matIDx;

                    sParams[3] = new SqlParameter();
                    sParams[3].SqlDbType = SqlDbType.VarChar;
                    sParams[3].ParameterName = "@packCode";
                    sParams[3].Value = packCode;

                    sParams[4] = new SqlParameter();
                    sParams[4].SqlDbType = SqlDbType.NVarChar;
                    sParams[4].ParameterName = "@oldLoc";
                    sParams[4].Value = oldLoc;

                    sParams[5] = new SqlParameter();
                    sParams[5].SqlDbType = SqlDbType.NVarChar;
                    sParams[5].ParameterName = "@newLoc";
                    sParams[5].Value = newLoc;

                    sParams[6] = new SqlParameter();
                    sParams[6].SqlDbType = SqlDbType.Int;
                    sParams[6].ParameterName = "@curQty";
                    sParams[6].Value = packQty;

                    cls.fnUpdDel(sql, sParams);

                    _msg_en = "Change sublocate successful.";
                    _msg_no = 1;
                }
                catch (SqlException sqlEx)
                {
                    _msg_en = "Program has connect database error occured.";
                    _msg_no = 2;
                }
                catch (Exception ex)
                {
                    _msg_en = "Program has common error occured.";
                    _msg_no = 2;
                }
                finally
                {

                    _inoutIDx = _packingCode = _importLOT = _sublocate = _unit = _quantity = _inDate = "";
                    lbl_Item_Packing.Text = "";
                    lbl_Item_LOT.Text = "";
                    lbl_Item_Name.Text = "";
                    lbl_Item_Code.Text = "";
                    lbl_Item_Loc.Text = "";
                    lbl_Item_Loc_Cur.Text = "";
                    txt_Item_Loc_New.Text = "";
                    txt_Item_Loc_New.Enabled = false;
                    lbl_Item_Loc_PCS.Text = "";
                    lbl_Item_Loc_BOX.Text = "";
                    lbl_Item_Loc_PAK.Text = "";
                    lbl_Item_Loc_PAL.Text = "";
                    btn_Item_Save.Enabled = false;
                    btn_Item_Done.Enabled = false;

                    //Init_Locate_Done();
                    Fnc_Loc_List_Load();

                    ((frmInventoryBeginOfDay)frm).fnc_Set_Status_Message(_msg_en, _msg_no);
                }
            }
        }

        private void Btn_Item_Done_Click(object sender, EventArgs e)
        {
            Init_Locate_Done();
        }

    }
}
