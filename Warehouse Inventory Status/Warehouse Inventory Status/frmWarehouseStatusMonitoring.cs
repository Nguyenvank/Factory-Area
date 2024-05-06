using System;
using System.Collections;
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
    public partial class frmWarehouseStatusMonitoring : Form
    {
        public int _dgvWHStatusWidth;
        public DateTime _datadate;

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 10; //Used for the header height


        public frmWarehouseStatusMonitoring()
        {
            InitializeComponent();
        }

        private void frmWarehouseStatusMonitoring_Load(object sender, EventArgs e)
        {

            init();
            if(check.IsConnectedToInternet())
            {
                _dgvWHStatusWidth = cls.fnGetDataGridWidth(dgvWHStatus);
                fnBindDataWarehouseStatus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dgvWHStatusWidth = cls.fnGetDataGridWidth(dgvWHStatus);
            fnGetDate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        public void init()
        {
            fnGetDate();

            cbbWarehouse.Items.Add("WH1 - Resin");
            cbbWarehouse.Items.Add("WH2 - CKD");
            cbbWarehouse.Items.Add("WH3 - Rubber");
            cbbWarehouse.Items.Add("WH4 - Chemical");
            cbbWarehouse.Items.Add("WH7 - W.I.P");
            //cbbWarehouse.Items.Add("WH8 - Recycle");
            cbbWarehouse.Items.Add("WH9 - Scrap");
            //cbbWarehouse.Items.Add("WH10 - Garbage");
            cbbWarehouse.Items.Add("WH11 - Stationary");
            cbbWarehouse.Items.Insert(0, "");
            cbbWarehouse.SelectedIndex = 0;

            btnPrint.Enabled = false;
        }

        public void fnGetDate()
        {
            lblDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
            if(check.IsConnectedToInternet())
            {
                lblDateTime.ForeColor = Color.Black;
            }
            else
            {
                lblDateTime.ForeColor = Color.Red;
            }
        }

        public void fnBindDataWarehouseStatus()
        {
            if(cbbWarehouse.SelectedIndex>0)
            {
                string title = "";
                //DateTime datadate = _datadate;
                DateTime frDate = dtpFrDate.Value;
                DateTime toDate = dtpToDate.Value;
                string cateId = cbbWarehouse.SelectedIndex.ToString();
                switch (cbbWarehouse.SelectedIndex)
                {
                    case 1:
                        cateId = "107";     //WH1
                        title = "RESIN";
                        break;
                    case 2:
                        cateId = "108";     //WH2
                        title = "C.K.D";
                        break;
                    case 3:
                        cateId = "106";     //WH3
                        title = "RUBBER";
                        break;
                    case 4:
                        cateId = "121";     //WH4
                        title = "CHEMICAL";
                        break;
                    case 5:
                        cateId = "103";     //WH7
                        title = "W.I.P";
                        break;
                    case 6:
                        cateId = "150";
                        title = "SCRAP";   //WH9 + WH10
                        break;
                    case 7:
                        cateId = "152";
                        title = "STATIONARY";   //WH11
                        break;
                        //case 6:
                        //    cateId = "122";
                        //    break;
                        //case 7:
                        //    cateId = "147";
                        //    break;
                }

                lblTitle.Text = title + " INVENTORY STATUS";

                string sql = "";
                sql = "V2_BASE_WAREHOUSE_STATUS_MONITORING3_ADDNEW";
                SqlParameter[] sParams = new SqlParameter[3]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "@cateId";
                sParams[0].Value = cateId;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.DateTime;
                sParams[1].ParameterName = "@fromdate";
                sParams[1].Value = frDate;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.DateTime;
                sParams[2].ParameterName = "@todate";
                sParams[2].Value = toDate;


                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sql, sParams);
                //dt = cls.fnSelect(sql);
                ////dt.Select("Sublocation IS NOT NULL");
                //DataRow[] dtr = dt.Select("sublocation<>''");
                //DataTable dt2 = dt.Clone();
                //foreach (DataRow d in dtr)
                //{
                //    dt2.ImportRow(d);
                //}


                dgvWHStatus.DataSource = dt;
                dgvWHStatus.Refresh();

                dgvWHStatus.Columns[0].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[1].Width = 25 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[2].Width = 15 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[3].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[4].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[5].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[6].Width = 8 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[7].Width = 5 * _dgvWHStatusWidth / 100;
                dgvWHStatus.Columns[8].Width = 15 * _dgvWHStatusWidth / 100;

                dgvWHStatus.Columns[0].Visible = true;
                dgvWHStatus.Columns[1].Visible = true;
                dgvWHStatus.Columns[2].Visible = true;
                dgvWHStatus.Columns[3].Visible = true;
                dgvWHStatus.Columns[4].Visible = true;
                dgvWHStatus.Columns[5].Visible = true;
                dgvWHStatus.Columns[6].Visible = true;
                dgvWHStatus.Columns[7].Visible = true;
                dgvWHStatus.Columns[8].Visible = true;

                cls.fnFormatDatagridview(dgvWHStatus, 12);

                dgvWHStatus.RowTemplate.Height = 28;

                dgvWHStatus.Columns[3].DefaultCellStyle.Format = "#,0.###";
                dgvWHStatus.Columns[4].DefaultCellStyle.Format = "#,0.###";
                dgvWHStatus.Columns[5].DefaultCellStyle.Format = "#,0.###";
                dgvWHStatus.Columns[6].DefaultCellStyle.Format = "#,0.###";

                //dgvWHStatus.Columns[3].HeaderText = "Inventory (" + cls.fnGetDate("tsp") + ")";
                //dgvWHStatus.Columns[6].HeaderText = "Inventory (" + cls.fnGetDate("tsn") + ")";
            }
        }

        private void dgvWHStatus_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            fnBindDataWarehouseStatus();
        }

        private void dgvWHStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                fnBindDataWarehouseStatus();
            }
            //cls.fnDatagridClickCell(dgvWHStatus, e);
        }

        private void dgvWHStatus_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //string cate = "", partname = "", partcode = "";
            string _inventory8 = "", _instock = "", _outstock = "", _inventory = "";
            int inventory8 = 0, instock = 0, outstock = 0, inventory = 0;
            //string unit = "", remark = "";

            foreach (DataGridViewRow row in dgvWHStatus.Rows)
            {
                _inventory8 = row.Cells[3].Value.ToString();
                _instock = row.Cells[4].Value.ToString();
                _outstock = row.Cells[5].Value.ToString();
                _inventory = row.Cells[6].Value.ToString();



                if (_instock == null || _instock == "")
                    _instock = "0";
                if (_outstock == null || _outstock == "")
                    _outstock = "0";
                if (_inventory == null || _inventory == "")
                    _inventory = "0";

                instock = (_instock != "0") ? Convert.ToInt32(_instock) : 0;
                outstock = (_outstock != "0") ? Convert.ToInt32(_outstock) : 0;
                inventory = (_inventory != "0") ? Convert.ToInt32(_inventory) : 0;
                inventory8 = (outstock != 0) ? inventory + outstock : inventory;

                row.Cells[6].Style.BackColor = Color.LightCyan;

                if (inventory8 <= 0)
                {
                    row.Cells[3].Style.BackColor = Color.Yellow;
                }
                if (inventory <= 0)
                {
                    row.Cells[6].Style.BackColor = Color.Yellow;
                }
            }
        }

        private void cbbWarehouse_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet())
            {
                //DateTime _date = dtpDataDate.Value;
                //_datadate = _date;
                //if (cbbWarehouse.SelectedIndex != 6 && cbbWarehouse.SelectedIndex != 7)
                //{
                //    fnBindDataWarehouseStatus();
                //}
                //else
                //{
                //    string promt = Prompt.ShowDialog("Authorizied Permission", "Password", "1");
                //    if (promt.ToUpper() == "mmt".ToUpper())
                //    {
                //        fnBindDataWarehouseStatus();
                //    }
                //    else
                //    {
                //        return;
                //    }
                //    DialogResult dialogResultAdd = MessageBox.Show("Are you an authorized person?", cls.appName(), MessageBoxButtons.YesNo);
                //    if (dialogResultAdd == DialogResult.Yes)
                //    {
                //        fnBindDataWarehouseStatus();
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}
                fnBindDataWarehouseStatus();

                btnPrint.Enabled = (dgvWHStatus.Rows.Count > 0) ? true : false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;

            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Inventory Status";
                printDocument1.Print();
            }

            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvWHStatus.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dgvWHStatus.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgvWHStatus.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvWHStatus.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Inventory Status", new Font(dgvWHStatus.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Inventory Status", new Font(dgvWHStatus.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dgvWHStatus.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dgvWHStatus.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Inventory Status", new Font(new Font(dgvWHStatus.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgvWHStatus.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
