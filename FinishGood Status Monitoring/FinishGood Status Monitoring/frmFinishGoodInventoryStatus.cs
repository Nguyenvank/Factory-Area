using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmFinishGoodInventoryStatus : Form
    {
        public int _dgvFGStatusWidth;

        public frmFinishGoodInventoryStatus()
        {
            InitializeComponent();
        }

        private void frmFinishGoodInventoryStatus_Load(object sender, EventArgs e)
        {
            _dgvFGStatusWidth = cls.fnGetDataGridWidth(dgvFGStatus) - 20;

            init();
            if (check.IsConnectedToInternet())
            {
                fnBindDataInventoryStatus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvFGStatusWidth = cls.fnGetDataGridWidth(dgvFGStatus) - 20;

            fnGetDate();
            //_Autoscroll();
            //EnsureVisibleRow(dgvFGStatus, 20);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                fnBindDataInventoryStatus();
            }
        }

        public void init()
        {
            fnGetDate();
        }

        public void fnGetDate()
        {
            try
            {
                if (!check.IsConnectedToInternet())
                {
                    lblDate.Text = String.Format("{0:dd/MM/yyyy}",DateTime.Now);
                    lblTime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
                    lblDate.ForeColor = Color.Red;
                    lblTime.ForeColor = Color.Red;
                }
                else
                {
                    lblDate.Text = cls.fnGetDate("SD");
                    lblTime.Text = cls.fnGetDate("CT");
                    lblDate.ForeColor = Color.Black;
                    lblTime.ForeColor = Color.Black;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnBindDataInventoryStatus()
        {
            try
            {
                string sql = "";
                sql = "V2_BASE_FINISHGOOD_STATUS_MONITORING_ADDNEW";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    string prevDay = dr[4].ToString();
                    string currDay = dr[11].ToString();

                    int _prevDay = (prevDay != "" && prevDay != null) ? Convert.ToInt32(prevDay) : 0;
                    int _currDay = (currDay != "" && currDay != null) ? Convert.ToInt32(currDay) : 0;

                    if (_prevDay < 0)
                    {
                        dr[4] = 0;
                    }

                    if (_currDay < 0)
                    {
                        dr[11] = 0;
                    }
                }


                dgvFGStatus.DataSource = dt;
                dgvFGStatus.Refresh();

                //dgvFGStatus.DataSource = dt;
                //dgvFGStatus.Refresh();

                //for (int i = 0; i < dgvFGStatus.Rows.Count; i++)
                //{
                //    dgvFGStatus.Rows[i].Height = (dgvFGStatus.Height - dgvFGStatus.Columns[0].HeaderCell.Size.Height) / dgvFGStatus.Rows.Count;
                //}

                dgvFGStatus.Columns[0].Width = 6 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[1].Width = 16 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[2].Width = 12 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[3].Width = 6 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[4].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[5].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[6].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[7].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[8].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[9].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[10].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[11].Width = 7 * _dgvFGStatusWidth / 100;
                dgvFGStatus.Columns[12].Width = 5 * _dgvFGStatusWidth / 100;

                dgvFGStatus.Columns[0].Visible = true;
                dgvFGStatus.Columns[1].Visible = true;
                dgvFGStatus.Columns[2].Visible = true;
                dgvFGStatus.Columns[3].Visible = true;
                dgvFGStatus.Columns[4].Visible = true;
                dgvFGStatus.Columns[5].Visible = true;
                dgvFGStatus.Columns[6].Visible = true;
                dgvFGStatus.Columns[7].Visible = true;
                dgvFGStatus.Columns[8].Visible = true;
                dgvFGStatus.Columns[9].Visible = true;
                dgvFGStatus.Columns[10].Visible = true;
                dgvFGStatus.Columns[12].Visible = true;

                cls.fnFormatDatagridview(dgvFGStatus, 11);

                //dgvFGStatus.RowTemplate.Height = 40;

                dgvFGStatus.Columns[4].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[5].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[6].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[7].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[8].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[9].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[10].DefaultCellStyle.Format = "#,0.###";
                dgvFGStatus.Columns[11].DefaultCellStyle.Format = "#,0.###";

                dgvFGStatus.Columns[11].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void _Autoscroll()
        {
            try
            {
                bool upward = false;
                DataGridView dgv = dgvFGStatus;
                int firstRowIndex = dgv.FirstDisplayedScrollingRowIndex;
                int nrowdisplayed = dgv.DisplayedRowCount(false);

                if (upward)
                {
                    if (dgv.FirstDisplayedScrollingRowIndex - dgv.DisplayedRowCount(false) > 0)
                    {
                        dgv.FirstDisplayedScrollingRowIndex -= dgv.DisplayedRowCount(false);
                    }
                    else
                    {
                        dgv.FirstDisplayedScrollingRowIndex = 0;
                    }
                    if (dgv.FirstDisplayedScrollingRowIndex == 0) upward = false;
                }
                else
                {
                    if (dgv.FirstDisplayedScrollingRowIndex + dgv.DisplayedRowCount(false) < dgv.Rows.Count - 1)
                    {
                        dgv.FirstDisplayedScrollingRowIndex += dgv.DisplayedRowCount(false);
                    }
                    else
                    {
                        dgv.FirstDisplayedScrollingRowIndex = dgv.Rows.Count - dgv.DisplayedRowCount(false);
                    }
                    if (dgv.FirstDisplayedScrollingRowIndex == dgv.Rows.Count - dgv.DisplayedRowCount(false)) upward = true;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private static void EnsureVisibleRow(DataGridView view, int rowToShow)
        {
            try
            {
                if (rowToShow >= 0 && rowToShow < view.RowCount)
                {
                    var countVisible = view.DisplayedRowCount(false);
                    var firstVisible = view.FirstDisplayedScrollingRowIndex;
                    if (rowToShow < firstVisible)
                    {
                        view.FirstDisplayedScrollingRowIndex = rowToShow;
                    }
                    else if (rowToShow >= firstVisible + countVisible)
                    {
                        view.FirstDisplayedScrollingRowIndex = rowToShow - countVisible + 1;
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvFGStatus_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string partname = "", inventory = "", remain = "";
                int _inventory = 0, _remain = 0;
                foreach (DataGridViewRow row in dgvFGStatus.Rows)
                {
                    remain = row.Cells[8].Value.ToString();
                    _remain = (remain != null && remain != "") ? Convert.ToInt32(remain) : 0;
                    if (_remain != 0)
                    {
                        if (_remain > 0)
                        {
                            row.Cells[8].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            row.Cells[8].Style.BackColor = Color.Goldenrod;
                            //row.Cells[8].Value = Math.Abs(_remain).ToString();
                        }
                    }
                    

                    row.Cells[11].Style.BackColor = Color.LightSkyBlue;
                    inventory = row.Cells[11].Value.ToString();
                    _inventory = (inventory != null && inventory != "0") ? Convert.ToInt32(inventory) : 0;
                    if (_inventory <= 0)
                    {
                        row.Cells[11].Style.BackColor = Color.Yellow;
                    }


                    partname = row.Cells[3].Value.ToString().ToLower();
                    if (partname.Contains("bellows") || partname.Contains("gasket") || partname.Contains("pump"))
                    {
                        row.Cells[9].Value = "N/A";
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dgvFGStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (check.IsConnectedToInternet())
                {
                    fnBindDataInventoryStatus();
                }
            }
            catch
            {

            }
            finally
            {

            }
        }
    }
}
