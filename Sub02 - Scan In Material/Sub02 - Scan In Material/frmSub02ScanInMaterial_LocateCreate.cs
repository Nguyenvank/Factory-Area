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
    public partial class frmSub02ScanInMaterial_LocateCreate : Form
    {
        public frmSub02ScanInMaterial_LocateCreate()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(dgv_list, true);
        }

        private void frmSub02ScanInMaterial_LocateCreate_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        private void frmSub02ScanInMaterial_LocateCreate_KeyDown(object sender, KeyEventArgs e)
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

        /****************************************************/

        public void Fnc_Load_Controls()
        {
            dgv_list.DataSource = null;
            btn_load.Enabled = true;
            btn_save.Enabled = false;
            btn_close.Enabled = true;
            txt_filter.Text = "";
            txt_filter.Enabled = false;
        }

        public void Fnc_Load_Data_List()
        {
            string
                sql = "V2o1_BASE_SubMaterial02_Create_Locate_List_SelItem_Addnew";

            int
                tblCnt = 0,
                rowCnt = 0;

            DataTable
                dt = new DataTable();

            dt = dt = cls.ExecuteDataTable(sql);
            tblCnt = dt.Rows.Count;

            dgv_list.DataSource = dt;

            dgv_list.Columns[0].FillWeight = 10;      //[STT]
            //dgv_list.Columns[1].FillWeight = 10;      //ProdId
            dgv_list.Columns[2].FillWeight = 40;      //Name
            dgv_list.Columns[3].FillWeight = 20;      //BarCode
            dgv_list.Columns[4].FillWeight = 20;      //Sublocation
            //dgv_list.Columns[5].FillWeight = 20;      //Curr_Sublocation
            dgv_list.Columns[6].FillWeight = 10;      //Uom

            dgv_list.Columns[0].Visible = true;
            dgv_list.Columns[1].Visible = false;
            dgv_list.Columns[2].Visible = true;
            dgv_list.Columns[3].Visible = true;
            dgv_list.Columns[4].Visible = true;
            dgv_list.Columns[5].Visible = false;
            dgv_list.Columns[6].Visible = true;

            dgv_list.Columns[0].ReadOnly = true;
            dgv_list.Columns[1].ReadOnly = true;
            dgv_list.Columns[2].ReadOnly = true;
            dgv_list.Columns[3].ReadOnly = true;
            dgv_list.Columns[4].ReadOnly = false;
            dgv_list.Columns[5].ReadOnly = true;
            dgv_list.Columns[6].ReadOnly = true;

            cls.fnFormatDatagridview_FullWidth(dgv_list, 10, 30);

            txt_filter.Text = "";
            btn_save.Enabled = txt_filter.Enabled = (tblCnt > 0) ? true : false;

            Fnc_Load_Data_List_Color();
        }

        public void Fnc_Load_Data_List_Color()
        {
            string
                sublocate = "";

            int
                sublocate_len = 0;

            foreach(DataGridViewRow row in dgv_list.Rows)
            {
                sublocate = row.Cells[4].Value.ToString().Trim();
                sublocate_len = sublocate.Length;

                if (sublocate_len == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }

            dgv_list.ClearSelection();
        }

        public void Fnc_Load_Data_Click(DataGridViewCellEventArgs e)
        {
            string
                prod_idx = "",
                sublocate = "";

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_list, e);

                DataGridViewRow row = new DataGridViewRow();
                row = dgv_list.Rows[e.RowIndex];

                prod_idx = row.Cells[1].Value.ToString();
                sublocate = row.Cells[4].Value.ToString();
            }
        }

        public void Fnc_Load_Data_Save()
        {
            try
            {
                string
                    sql = "",
                    prod_idx = "",
                    new_sublocate = "",
                    cur_sublocate = "";

                foreach (DataGridViewRow row in dgv_list.Rows)
                {
                    prod_idx = row.Cells[1].Value.ToString();
                    new_sublocate = row.Cells[4].Value.ToString().Trim();
                    cur_sublocate = row.Cells[5].Value.ToString().Trim();

                    if (new_sublocate.Length > 0 && cur_sublocate.Length == 0)
                    {
                        sql = "V2o1_BASE_SubMaterial02_Update_Locate_Item_SelItem_Addnew";

                        SqlParameter[]
                            sParams = new SqlParameter[2]; // Parameter count

                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@prod_idx";
                        sParams[0].Value = prod_idx;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.VarChar;
                        sParams[1].ParameterName = "@sublocate";
                        sParams[1].Value = new_sublocate;

                        cls.fnUpdDel(sql, sParams);
                    }
                }
            }
            catch { }
            finally { }
        }

        /****************************************************/

        private void btn_load_Click(object sender, EventArgs e)
        {
            Fnc_Load_Data_List();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Cập nhật dữ liệu vị trí?", cls.appName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Fnc_Load_Data_Save();
                Fnc_Load_Data_List();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_list_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Fnc_Load_Data_Click(e);
        }

        private void dgv_list_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fnc_Load_Data_Click(e);
        }

        private void dgv_list_Sorted(object sender, EventArgs e)
        {
            Fnc_Load_Data_List_Color();
        }

        private void txt_filter_TextChanged(object sender, EventArgs e)
        {
            cls.fnFilterDatagridRow(dgv_list, txt_filter, 2);
            Fnc_Load_Data_List_Color();
        }

        private void dgv_list_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dgv_list.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    ((TextBox)(e.Control)).CharacterCasing = CharacterCasing.Upper;
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }

            //e.Control.KeyPress -= TextboxNumeric_KeyPress;
            //if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 4)
            //{
            //    if (e.Control is TextBox)
            //    {
            //        ((TextBox)(e.Control)).CharacterCasing = CharacterCasing.Upper;
            //    }

            //    e.Control.KeyPress += TextboxNumeric_KeyPress;
            //}
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != 95 && !(e.KeyChar >= 65 && e.KeyChar <= 90))
            {
                e.Handled = true;
            }
        }

        //private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    bool
        //        char_accept = true;

        //    if ((e.KeyChar < 48 && e.KeyChar > 57) || (e.KeyChar < 65 && e.KeyChar > 90) || e.KeyChar != 95)
        //    {
        //        char_accept = true;
        //    }

        //    e.Handled = (char_accept == true) ? true : false;

        //    //bool nonNumberEntered = true;

        //        //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
        //    //if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || e.KeyChar == 95)
        //    //{
        //    //    //nonNumberEntered = false;
        //    //    e.Handled = false;
        //    //}

        //    //if (nonNumberEntered)
        //    //{
        //    //    // Stop the character from being entered into the control since it is non-numerical.
        //    //    e.Handled = true;
        //    //}
        //    //else
        //    //{
        //    //    e.Handled = false;
        //    //}
        //}
    }
}
