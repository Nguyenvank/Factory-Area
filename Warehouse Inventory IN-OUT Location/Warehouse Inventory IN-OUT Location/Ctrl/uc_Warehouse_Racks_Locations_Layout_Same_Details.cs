using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventory_Data;

namespace Warehouse_Inventory_IN_OUT_Location.Ctrl
{
    public partial class uc_Warehouse_Racks_Locations_Layout_Same_Details : Form
    {
        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_idx = "",
            __rack_cd = "",
            __cell_no = "";

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public uc_Warehouse_Racks_Locations_Layout_Same_Details()
        {
            InitializeComponent();
        }

        public uc_Warehouse_Racks_Locations_Layout_Same_Details(string whs_idx, string rack_cd, string cell_no)
        {
            InitializeComponent();

            __whs_idx = whs_idx;
            __rack_cd = rack_cd;
            __cell_no = cell_no;

            this.Text = String.Format("Item(s) in the same location ({0}-{1})", __rack_cd, __cell_no);

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(dgv_list, true);
        }

        private void uc_Warehouse_Racks_Locations_Layout_Same_Details_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        private void uc_Warehouse_Racks_Locations_Layout_Same_Details_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /*************************************/

        public void Fnc_Load_Controls()
        {
            dgv_list.DataSource = null;

            Fnc_Load_Data_List();
        }

        public void Fnc_Load_Data_List()
        {
            string
                whs_idx = __whs_idx,
                rack_cd = __rack_cd,
                cell_no = __cell_no;

            try
            {
                //__sql = "V2o1_BASE_Warehouse_Material_ScanIn_Rack_Locate_Cell_Items_List_SelItem_Addnew_v1o0";
                __sql = "V2o1_BASE_Warehouse_Material_ScanIn_Rack_Locate_Cell_Items_List_SelItem_Addnew_v1o1";

                __sparam = new SqlParameter[3];

                __sparam[0] = new SqlParameter();
                __sparam[0].SqlDbType = SqlDbType.Int;
                __sparam[0].ParameterName = "@whs_idx";
                __sparam[0].Value = whs_idx;

                __sparam[1] = new SqlParameter();
                __sparam[1].SqlDbType = SqlDbType.VarChar;
                __sparam[1].ParameterName = "@rack_cd";
                __sparam[1].Value = rack_cd;

                __sparam[2] = new SqlParameter();
                __sparam[2].SqlDbType = SqlDbType.TinyInt;
                __sparam[2].ParameterName = "@cell_no";
                __sparam[2].Value = cell_no;

                __dt = cls.ExecuteDataTable(__sql, __sparam);
                __tbl_cnt = __dt.Rows.Count;
                dgv_list.DataSource = __dt;

                dgv_list.Columns[0].FillWeight = 10;    // No
                //dgv_list.Columns[1].FillWeight = 20;    // idx
                //dgv_list.Columns[2].FillWeight = 20;    // rack_idx
                dgv_list.Columns[3].FillWeight = 38;    // packingCode
                //dgv_list.Columns[4].FillWeight = 20;    // mat_nm
                dgv_list.Columns[5].FillWeight = 30;    // mat_cd
                dgv_list.Columns[6].FillWeight = 12;    // packing_qty
                dgv_list.Columns[7].FillWeight = 10;    // mat_uom
                //dgv_list.Columns[8].FillWeight = 20;    // importLOT
                //dgv_list.Columns[9].FillWeight = 20;    // pic_nm
                //dgv_list.Columns[10].FillWeight = 20;    // in_dt
                //dgv_list.Columns[11].FillWeight = 20;    // mat_1D
                //dgv_list.Columns[12].FillWeight = 20;    // mat_stock
                //dgv_list.Columns[13].FillWeight = 20;    // mat_safety
                //dgv_list.Columns[14].FillWeight = 20;    // mat_status

                dgv_list.Columns[0].Visible = true;
                dgv_list.Columns[1].Visible = false;
                dgv_list.Columns[2].Visible = false;
                dgv_list.Columns[3].Visible = true;
                dgv_list.Columns[4].Visible = false;
                dgv_list.Columns[5].Visible = true;
                dgv_list.Columns[6].Visible = true;
                dgv_list.Columns[7].Visible = true;
                dgv_list.Columns[8].Visible = false;
                dgv_list.Columns[9].Visible = false;
                dgv_list.Columns[10].Visible = false;
                dgv_list.Columns[11].Visible = false;
                dgv_list.Columns[12].Visible = false;
                dgv_list.Columns[13].Visible = false;
                dgv_list.Columns[14].Visible = false;

                dgv_list.Columns[10].DefaultCellStyle.Format = "dd/MM/yyyy MM:mm";

                cls.fnFormatDatagridview_FullWidth(dgv_list, 12, 30);

                dgv_list.BackgroundColor = Color.White;
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Data_List_Color()
        {
            string status = "";
            int _status = 0;
            Color color = Color.Transparent;

            foreach(DataGridViewRow row in dgv_list.Rows)
            {
                status = row.Cells[14].Value.ToString();
                _status = (status.Length > 0) ? Convert.ToInt32(status) : 0;

                switch (_status)
                {
                    case 0:
                        color = Color.LightGray;
                        break;
                    case 1:
                        color = Color.LightGreen;
                        break;
                    case 2:
                        color = Color.Gold;
                        break;
                    case 3:
                        color = Color.LightPink;
                        break;
                }

                row.DefaultCellStyle.BackColor = color;
            }

            dgv_list.ClearSelection();
        }

        /*************************************/

        private void dgv_list_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Fnc_Load_Data_List_Color();
        }

        private void dgv_list_Sorted(object sender, EventArgs e)
        {
            Fnc_Load_Data_List_Color();
        }
    }
}
