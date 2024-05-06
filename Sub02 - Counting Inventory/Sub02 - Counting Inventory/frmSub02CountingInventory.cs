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
    public partial class frmSub02CountingInventory : Form
    {

        public int _dgv_Sub_List_Width;
        public int _dgv_Sub_Packing_Width;

        public string _prodIDx = "";

        public string _msgText;
        public int _msgType;

        public DateTime _dt;
        Timer timer = new Timer();


        public frmSub02CountingInventory()
        {
            InitializeComponent();

            init_Sub_Menu();
        }

        private void frmSub02CountingInventory_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();

            init_Sub_List();
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        public void init_Sub_Menu()
        {
            ContextMenu mnuSubList = new ContextMenu();
            mnuSubList.MenuItems.Add(new MenuItem("Tải lại danh mục vật tư", fn_Sub_List_Refresh));
            if (_prodIDx != "" && _prodIDx != null)
            {
                mnuSubList.MenuItems.Add(new MenuItem("Tải lại danh mục nhập kho", fn_Sub_Packing_Refresh));
                mnuSubList.MenuItems.Add(new MenuItem("-"));
                mnuSubList.MenuItems.Add(new MenuItem("Lưu lại số lượng nhập kho", fn_Sub_Packing_Save));
            }
            tableLayoutPanel1.ContextMenu = mnuSubList;
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
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

        private void fn_Sub_List_Refresh(object sender, EventArgs e)
        {
            _prodIDx = "";

            init_Sub_List();
            init_Sub_Menu();

            dgv_Sub_Packing.DataSource = "";
            dgv_Sub_List.ClearSelection();
        }

        private void fn_Sub_Packing_Refresh(object sender, EventArgs e)
        {
            init_Sub_Packing();

            dgv_Sub_Packing.ClearSelection();
        }

        private void fn_Sub_Packing_Save(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tiếp tục?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow rows in dgv_Sub_Packing.Rows)
                    {
                        string idx = rows.Cells[1].Value.ToString();
                        string matIDx = rows.Cells[3].Value.ToString();
                        string qtyRemain = rows.Cells[9].Value.ToString();

                        if (qtyRemain != "" && qtyRemain != null)
                        {
                            string sql = "V2o1_BASE_SubMaterial02_Init_Counting_Inventory_Packing_UpdItem_Addnew";

                            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                            sParams[0] = new SqlParameter();
                            sParams[0].SqlDbType = SqlDbType.Int;
                            sParams[0].ParameterName = "@idx";
                            sParams[0].Value = idx;

                            sParams[1] = new SqlParameter();
                            sParams[1].SqlDbType = SqlDbType.Int;
                            sParams[1].ParameterName = "@matIDx";
                            sParams[1].Value = matIDx;

                            sParams[2] = new SqlParameter();
                            sParams[2].SqlDbType = SqlDbType.Int;
                            sParams[2].ParameterName = "@qtyRemain";
                            sParams[2].Value = qtyRemain;

                            cls.fnUpdDel(sql, sParams);
                        }
                    }

                    _msgText = "Cập nhật số lượng tồn kho thành công.";
                    _msgType = 1;
                }
                catch (SqlException sqlEx)
                {
                    _msgText = "Có lỗi dữ liệu phát sinh.\r\nVui lòng chụp lại lỗi và liên hệ quản trị hệ thống.";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    _msgText = "Có lỗi phát sinh.\r\nVui lòng chụp lại lỗi và liên hệ quản trị hệ thống.";
                    _msgType = 2;
                }
                finally
                {
                    init_Sub_List();
                    init_Sub_Packing();
                    init_Sub_Menu();

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }


        #region SUB-MATERIAL LIST


        public void init_Sub_List()
        {
            string sql = "V2o1_BASE_SubMaterial02_Init_Counting_Inventory_List_SelItem_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgv_Sub_List_Width = cls.fnGetDataGridWidth(dgv_Sub_List);
            dgv_Sub_List.DataSource = dt;

            dgv_Sub_List.Columns[0].Width = 10 * _dgv_Sub_List_Width / 100;    // STT
            //dgv_Sub_List.Columns[1].Width = 15 * _dgv_Sub_List_Width / 100;    // ProdId
            dgv_Sub_List.Columns[2].Width = 50 * _dgv_Sub_List_Width / 100;    // Name
            //dgv_Sub_List.Columns[3].Width = 15 * _dgv_Sub_List_Width / 100;    // BarCode
            dgv_Sub_List.Columns[4].Width = 15 * _dgv_Sub_List_Width / 100;    // Total IN
            dgv_Sub_List.Columns[5].Width = 15 * _dgv_Sub_List_Width / 100;    // Total Remain
            dgv_Sub_List.Columns[6].Width = 10 * _dgv_Sub_List_Width / 100;    // UOM

            dgv_Sub_List.Columns[0].Visible = true;
            dgv_Sub_List.Columns[1].Visible = false;
            dgv_Sub_List.Columns[2].Visible = true;
            dgv_Sub_List.Columns[3].Visible = false;
            dgv_Sub_List.Columns[4].Visible = true;
            dgv_Sub_List.Columns[5].Visible = true;
            dgv_Sub_List.Columns[6].Visible = true;

            cls.fnFormatDatagridview(dgv_Sub_List, 11, 50);
        }

        private void dgv_Sub_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Sub_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Sub_List, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Sub_List.Rows[e.RowIndex];

                string prodIDx = row.Cells[1].Value.ToString();
                _prodIDx = prodIDx;

                init_Sub_Packing();
                init_Sub_Menu();
            }
        }

        private void dgv_Sub_List_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        #endregion


        #region SUB-MATERIAL PACKING


        public void init_Sub_Packing()
        {
            string matIDx = _prodIDx;
            string sql = "V2o1_BASE_SubMaterial02_Init_Counting_Inventory_Packing_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@matIDx";
            sParams[0].Value = matIDx;

            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql, sParams);

            _dgv_Sub_Packing_Width = cls.fnGetDataGridWidth(dgv_Sub_Packing);
            dgv_Sub_Packing.DataSource = dt;

            dgv_Sub_Packing.Columns[0].Width = 6 * _dgv_Sub_Packing_Width / 100;    // STT
            //dgv_Sub_Packing.Columns[1].Width = 15 * _dgv_Sub_Packing_Width / 100;    // idx
            //dgv_Sub_Packing.Columns[2].Width = 15 * _dgv_Sub_Packing_Width / 100;    // hoIDx
            //dgv_Sub_Packing.Columns[3].Width = 15 * _dgv_Sub_Packing_Width / 100;    // matIDx
            dgv_Sub_Packing.Columns[4].Width = 20 * _dgv_Sub_Packing_Width / 100;    // matName
            dgv_Sub_Packing.Columns[5].Width = 19 * _dgv_Sub_Packing_Width / 100;    // matCode
            dgv_Sub_Packing.Columns[6].Width = 6 * _dgv_Sub_Packing_Width / 100;    // matQty
            dgv_Sub_Packing.Columns[7].Width = 14 * _dgv_Sub_Packing_Width / 100;    // IN_Date
            dgv_Sub_Packing.Columns[8].Width = 23 * _dgv_Sub_Packing_Width / 100;    // IN_Code
            dgv_Sub_Packing.Columns[9].Width = 6 * _dgv_Sub_Packing_Width / 100;    // IN_Remain
            dgv_Sub_Packing.Columns[10].Width = 6 * _dgv_Sub_Packing_Width / 100;    // matUnit

            dgv_Sub_Packing.Columns[0].Visible = true;
            dgv_Sub_Packing.Columns[1].Visible = false;
            dgv_Sub_Packing.Columns[2].Visible = false;
            dgv_Sub_Packing.Columns[3].Visible = false;
            dgv_Sub_Packing.Columns[4].Visible = true;
            dgv_Sub_Packing.Columns[5].Visible = true;
            dgv_Sub_Packing.Columns[6].Visible = true;
            dgv_Sub_Packing.Columns[7].Visible = true;
            dgv_Sub_Packing.Columns[8].Visible = true;
            dgv_Sub_Packing.Columns[9].Visible = true;
            dgv_Sub_Packing.Columns[10].Visible = true;

            dgv_Sub_Packing.Columns[0].ReadOnly = true;
            dgv_Sub_Packing.Columns[1].ReadOnly = true;
            dgv_Sub_Packing.Columns[2].ReadOnly = true;
            dgv_Sub_Packing.Columns[3].ReadOnly = true;
            dgv_Sub_Packing.Columns[4].ReadOnly = true;
            dgv_Sub_Packing.Columns[5].ReadOnly = true;
            dgv_Sub_Packing.Columns[6].ReadOnly = true;
            dgv_Sub_Packing.Columns[7].ReadOnly = true;
            dgv_Sub_Packing.Columns[8].ReadOnly = true;
            dgv_Sub_Packing.Columns[9].ReadOnly = false;
            dgv_Sub_Packing.Columns[10].ReadOnly = true;


            dgv_Sub_Packing.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgv_Sub_Packing, 14, 50);

            foreach (DataGridViewRow rows in dgv_Sub_Packing.Rows)
            {
                rows.Cells[9].Style.BackColor = Color.DeepSkyBlue;
            }

        }

        private void dgv_Sub_Packing_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgv_Sub_Packing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgv_Sub_Packing, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgv_Sub_Packing.Rows[e.RowIndex];
            }
        }

        private void dgv_Sub_Packing_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Sub_Packing_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv_Sub_Packing.CurrentCell.ColumnIndex == 7)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(CheckKey);
            }
            else
            {
                e.Control.KeyPress += new KeyPressEventHandler(CheckKey);
            }
        }

        public void CheckKey(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }


        #endregion

    }
}
