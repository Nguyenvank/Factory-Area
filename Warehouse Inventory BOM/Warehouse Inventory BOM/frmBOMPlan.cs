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
    public partial class frmBOMPlan : Form
    {

        private int rowProdPlanListIndex = 0;
        private int rowProdPlanQtyIndex = 0;

        public int _dgvProdPlanListWidth;
        public int _dgvProdPlanQtyWidth;

        public frmBOMPlan()
        {
            InitializeComponent();

            //dgvProdPlanQty.Scroll += new System.Windows.Forms.ScrollEventHandler(dgvProdPlanQty_Scroll);
        }

        private void frmBOMPlan_Load(object sender, EventArgs e)
        {
            init();

            dtpProdPlanDate.MinDate = DateTime.Now;
        }

        public void init()
        {
            fnCreateTree();

            

            fnLoadProdPlanList();
            fnLoadProdPlanQty();
        }

        public void fnLoadProdPlanList()
        {
            try
            {
                string header = "";
                DateTime _header;
                string sql = "V2o1_BASE_BOM_Plan_Production_List_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql);

                _dgvProdPlanListWidth = cls.fnGetDataGridWidth(dgvProdPlanList);

                dgvProdPlanList.DataSource = dt;

                //dgvProdPlanList.Columns[0].Width = 20 * _dgvProdPlanListWidth / 100;    // idx
                //dgvProdPlanList.Columns[1].Width = 15 * _dgvProdPlanListWidth / 100;    // category
                //dgvProdPlanList.Columns[2].Width = 10 * _dgvProdPlanListWidth / 100;    // name
                dgvProdPlanList.Columns[1].Width = 150;    // category
                dgvProdPlanList.Columns[2].Width = 110;    // name

                for (int i = 3; i < dgvProdPlanList.Columns.Count; i++)
                {
                    header = dgvProdPlanList.Columns[i].HeaderCell.Value.ToString();
                    _header = Convert.ToDateTime(header);
                    //dgvProdPlanList.Columns[i].Width = 10 * _dgvProdPlanListWidth / 100;    // name
                    dgvProdPlanList.Columns[i].Width = 50;    // name
                    dgvProdPlanList.Columns[i].HeaderCell.Value = String.Format("{0:dd/MM}", _header);
                    dgvProdPlanList.Columns[i].Visible = true;
                }

                dgvProdPlanList.Columns[0].Visible = false;
                dgvProdPlanList.Columns[1].Visible = true;
                dgvProdPlanList.Columns[2].Visible = true;

                cls.fnFormatDatagridview(dgvProdPlanList, 11, 32);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadProdPlanQty()
        {
            try
            {
                DateTime planDate = dtpProdPlanDate.Value;
                string sql = "V2o1_BASE_BOM_Plan_Production_Qty_SelItem_Addnew";
                DataTable dt = new DataTable();

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.DateTime;
                sParams[0].ParameterName = "@planDate";
                sParams[0].Value = planDate;

                dt = cls.ExecuteDataTable(sql,sParams);

                _dgvProdPlanQtyWidth = cls.fnGetDataGridWidth(dgvProdPlanQty);
                dgvProdPlanQty.DataSource = dt;

                //dgvProdPlanQty.Columns[0].Width = 20 * _dgvProdPlanQtyWidth / 100;    // idx
                dgvProdPlanQty.Columns[1].Width = 75 * _dgvProdPlanQtyWidth / 100;    // category
                dgvProdPlanQty.Columns[2].Width = 25 * _dgvProdPlanQtyWidth / 100;    // name

                dgvProdPlanQty.Columns[0].Visible = false;
                dgvProdPlanQty.Columns[1].Visible = true;
                dgvProdPlanQty.Columns[2].Visible = true;

                dgvProdPlanQty.Columns[0].ReadOnly = true;
                dgvProdPlanQty.Columns[1].ReadOnly = true;
                dgvProdPlanQty.Columns[2].ReadOnly = false;

                cls.fnFormatDatagridview(dgvProdPlanQty, 11);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnCreateTree()
        {
            treeView1.Nodes.Clear();
            string sql = "V2o1_BASE_BOM_Plan_Create_Tree_Parent_SelItem_Addnew";
            TreeNode parentNode;
            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                parentNode = treeView1.Nodes.Add(dr[1].ToString());
                PopulateTreeView(Convert.ToInt32(dr[0].ToString()), parentNode);
                //parentNode.ToolTipText = dr[1].ToString();
            }
            //treeView1.ExpandAll();
            treeView1.CollapseAll();
            if (dt.Rows.Count > 0)
            {
                //treeView1.Nodes[0].Expand();
            }
        }

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {
            TreeNode childNode;
            string sql = "V2o1_BASE_BOM_Plan_Create_Tree_Child_SelItem_Addnew";
            DataTable dtChild = new DataTable();

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@parentID";
            sParams[0].Value = parentId;

            dtChild = cls.ExecuteDataTable(sql, sParams);
            foreach (DataRow dr in dtChild.Rows)
            {
                if (parentNode == null)
                {
                    childNode = treeView1.Nodes.Add(dr[1].ToString());
                    childNode.ToolTipText = dr[1].ToString();
                }
                else
                {
                    childNode = parentNode.Nodes.Add(dr[1].ToString());
                    PopulateTreeView(Convert.ToInt32(dr[0].ToString()), childNode);
                }
                childNode.ToolTipText = dr[1].ToString();
            }
        }

        private void dgvProdPlanQty_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvProdPlanQty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvProdPlanQty, e);
        }

        private void dgvProdPlanQty_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvProdPlanQty.Rows[e.RowIndex].Selected = true;
                rowProdPlanQtyIndex = e.RowIndex;
                dgvProdPlanQty.CurrentCell = dgvProdPlanQty.Rows[e.RowIndex].Cells[2];
                cmsProdPlanQty.Show(this.dgvProdPlanQty, e.Location);
                cmsProdPlanQty.Show(Cursor.Position);
            }
        }

        private void refreshThisListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnLoadProdPlanQty();
            lblProdPlanInfo.Text = "Refresh production plan quantity successful.";
        }

        private void btnProdPlanSave_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    DateTime planDate = dtpProdPlanDate.Value;
                    string prodID = "", prodName = "", planQty = "";
                    foreach (DataGridViewRow row in dgvProdPlanQty.Rows)
                    {
                        prodID = row.Cells[0].Value.ToString();
                        prodName = row.Cells[1].Value.ToString();
                        planQty = row.Cells[2].Value.ToString();

                        string sql = "V2o1_BASE_BOM_Plan_Production_Qty_AddItem_Addnew";
                        DataTable dt = new DataTable();

                        SqlParameter[] sParams = new SqlParameter[4]; // Parameter count
                        sParams[0] = new SqlParameter();
                        sParams[0].SqlDbType = SqlDbType.Int;
                        sParams[0].ParameterName = "@prodID";
                        sParams[0].Value = prodID;

                        sParams[1] = new SqlParameter();
                        sParams[1].SqlDbType = SqlDbType.NVarChar;
                        sParams[1].ParameterName = "@prodName";
                        sParams[1].Value = prodName;

                        sParams[2] = new SqlParameter();
                        sParams[2].SqlDbType = SqlDbType.Int;
                        sParams[2].ParameterName = "@planQty";
                        sParams[2].Value = planQty;

                        sParams[3] = new SqlParameter();
                        sParams[3].SqlDbType = SqlDbType.Date;
                        sParams[3].ParameterName = "@planDate";
                        sParams[3].Value = planDate;

                        cls.fnUpdDel(sql, sParams);
                    }

                    fnLoadProdPlanList();
                    lblProdPlanInfo.Text = "Save production plan quantity successful.";
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        private void dgvProdPlanList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void dgvProdPlanList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvProdPlanList, e);
        }

        private void dgvProdPlanList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvProdPlanList.Rows[e.RowIndex].Selected = true;
                rowProdPlanListIndex = e.RowIndex;
                dgvProdPlanList.CurrentCell = dgvProdPlanList.Rows[e.RowIndex].Cells[2];
                cmsProdPlanList.Show(this.dgvProdPlanList, e.Location);
                cmsProdPlanList.Show(Cursor.Position);
            }
        }

        private void refreshThisListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fnLoadProdPlanList();
            lblProdPlanInfo.Text = "Refresh production plan list successful.";
        }

        private void btnProdPlanLoad_Click(object sender, EventArgs e)
        {

        }

        private void dgvProdPlanList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                DataGridViewCell cell = this.dgvProdPlanList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = cell.Value.ToString();
            }
        }

        private void dtpProdPlanDate_ValueChanged(object sender, EventArgs e)
        {
            fnLoadProdPlanQty();
        }
    }
}
