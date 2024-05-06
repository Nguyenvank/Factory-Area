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
    public partial class frmBOMCalculate : Form
    {
        public int _dgvBOMWidth;

        private int rowListIndex = 0;


        public frmBOMCalculate()
        {
            InitializeComponent();
        }

        private void frmBOMCalculate_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        public void init()
        {
            fnCreateTree();
            fnLoadInventoryBOM();
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
                //parentNode.NodeFont = new Font(treeView1.Font, FontStyle.Bold);
            }
            //treeView1.ExpandAll();
            treeView1.CollapseAll();
            //if (dt.Rows.Count > 0)
            //{
            //    //treeView1.Nodes[0].Expand();
            //}
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

        public void fnLoadInventoryBOM()
        {
            try
            {
                dgvBOM.ClearSelection();
                string header = "";
                DateTime _header;

                string sql = "V2o1_BASE_BOM_Calculate_Inventory_Quantity_SelItem_Addnew";
                DataTable dt = new DataTable();

                dt = cls.fnSelect(sql);
                _dgvBOMWidth = cls.fnGetDataGridWidth(dgvBOM);

                dgvBOM.DataSource = dt;

                //dgvBOM.Columns[0].Width = 20 * _dgvBOMWidth / 100;    // idx
                //dgvBOM.Columns[1].Width = 15 * _dgvBOMWidth / 100;    // category
                //dgvBOM.Columns[2].Width = 10 * _dgvBOMWidth / 100;    // name
                dgvBOM.Columns[1].Width = 150;    // category
                dgvBOM.Columns[2].Width = 110;    // name
                dgvBOM.Columns[3].Width = 60;    // inventory

                for (int i = 4; i < dgvBOM.Columns.Count; i++)
                {
                    header = dgvBOM.Columns[i].HeaderCell.Value.ToString();
                    _header = Convert.ToDateTime(header);
                    //dgvBOM.Columns[i].Width = 10 * _dgvBOMWidth / 100;    // name
                    dgvBOM.Columns[i].Width = 50;    // name
                    dgvBOM.Columns[i].HeaderCell.Value = String.Format("{0:dd/MM}", _header);
                    dgvBOM.Columns[i].Visible = true;
                    //dgvBOM.Columns[i].Frozen = false;
                }

                dgvBOM.Columns[0].Visible = false;
                dgvBOM.Columns[1].Visible = true;
                dgvBOM.Columns[2].Visible = true;
                dgvBOM.Columns[3].Visible = true;

                //dgvBOM.Columns[3].Frozen = true;

                cls.fnFormatDatagridview(dgvBOM, 11, 32);

            }
            catch
            {

            }
            finally
            {

            }

        }

        public void fnUpdateInventoryBOM()
        {
            string sql = "";
        }

        private void dgvBOM_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int cols = dgvBOM.Columns.Count;
            string invent = "";
            int _invent = 0;
            foreach (DataGridViewRow row in dgvBOM.Rows)
            {
                for (int i = 4; i < cols; i++)
                {
                    invent = row.Cells[i].Value.ToString();
                    _invent = (invent != "" && invent != null) ? Convert.ToInt32(invent) : 0;
                    row.Cells[i].Style.BackColor = (_invent > 0) ? Color.DeepSkyBlue : Color.Red;
                    row.Cells[i].Style.ForeColor = (_invent > 0) ? Color.Black : Color.White;
                }
            }
        }

        private void dgvBOM_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cls.fnDatagridClickCell(dgvBOM, e);
        }

        private void dgvBOM_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvBOM.Rows[e.RowIndex].Selected = true;
                rowListIndex = e.RowIndex;
                dgvBOM.CurrentCell = dgvBOM.Rows[e.RowIndex].Cells[2];
                cmsBOM.Show(this.dgvBOM, e.Location);
                cmsBOM.Show(Cursor.Position);
            }

        }

        private void refreshThisTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnLoadInventoryBOM();
            dgvBOM.ClearSelection();
        }

        private void updateNewInventoryToBOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnUpdateInventoryBOM();
        }
    }
}
