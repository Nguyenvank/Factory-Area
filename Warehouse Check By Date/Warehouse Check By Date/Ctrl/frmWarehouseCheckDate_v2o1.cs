using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data.Ctrl
{
    public partial class frmWarehouseCheckDate_v2o1 : UserControl
    {
        public int _dgvDataWidth;

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 1;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height


        public frmWarehouseCheckDate_v2o1()
        {
            InitializeComponent();

            cls.SetDoubleBuffer(dgvData, true);
        }

        private void frmWarehouseCheckDate_v2o1_Load(object sender, EventArgs e)
        {
            _dgvDataWidth = cls.fnGetDataGridWidth(dgvData);

            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
            //if (check.IsConnectedToInternet() == true && cbbWH.SelectedIndex > 0)
            //{
            //    fnLoadData();
            //}
        }

        public void init()
        {
            fnGetdate();

            dtpDateFr.MinDate = new DateTime(2018, 6, 4);
            dtpDateFr.MaxDate = DateTime.Now;
            dtpDateTo.MaxDate = DateTime.Now;

            cbbWH.Items.Add("RESIN");
            cbbWH.Items.Add("CKD");
            cbbWH.Items.Add("RUBBER");
            cbbWH.Items.Add("CHEMICAL");
            cbbWH.Items.Add("FINISH GOODS");
            cbbWH.Items.Add("SCRAP");
            cbbWH.Items.Add("STATIONARY");
            //cbbWH.Items.Add("SPARE PARTS");
            cbbWH.Items.Add("RECYCLES");
            //cbbWH.Items.Add("W.I.P");
            cbbWH.Items.Insert(0, "SELECT...");
            cbbWH.SelectedIndex = 0;

            //lblShortage.Text = "Shortage";
            //lblWarning.Text = "Warning";
            //lblNormal.Text = "Normal";
        }

        public void fnGetdate()
        {
            try
            {
                if (check.IsConnectedToInternet())
                {
                    lblDate.Text = cls.fnGetDate("SD");
                    lblTime.Text = cls.fnGetDate("CT");

                    lblDate.ForeColor = Color.White;
                    lblTime.ForeColor = Color.White;
                }
                else
                {
                    lblDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    lblTime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);

                    lblDate.ForeColor = Color.Red;
                    lblTime.ForeColor = Color.Red;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void dtpDateFr_ValueChanged(object sender, EventArgs e)
        {
            dtpDateTo.MinDate = dtpDateFr.Value;
            dtpDateTo.Value = dtpDateFr.Value;
        }

        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            //dtpDateFr.MaxDate = dtpDateTo.Value;
        }

        public string fnWarehouseID()
        {
            string whID = "";

            switch (cbbWH.SelectedIndex)
            {
                case 1:
                    whID = "101";   //RESIN
                    break;
                case 2:
                    whID = "102";   //CKD
                    break;
                case 3:
                    whID = "103";   //RUBBER
                    break;
                case 4:
                    whID = "111";   //CHEMICAL
                    break;
                case 5:
                    whID = "100";   //FINISH GOODS
                    break;
                case 6:
                    whID = "116";   //SCRAP + GARBAGE
                    break;
                case 7:
                    whID = "118";   //STATIONARY
                    break;
                case 8:
                    whID = "115";   //RECYCLE
                    break;
                case 9:
                    whID = "112";   //SUB-MATERIAL (SPARE PARTS)
                    break;
                //case 8:
                //    whID = "114";   //W.I.P : 114
                //    break;
                default:
                    whID = "0";     //ALL
                    break;
            }

            return whID;
        }

        public void fnLoadData()
        {
            try
            {
                DateTime selDateFr = dtpDateFr.Value;
                DateTime selDateTo = dtpDateTo.Value;
                string selWH = fnWarehouseID();


                //string sql = "V2o1_BASE_Warehouse_Inventory_By_Date_v2_SelItem_Addnew";
                string sql = "V2o1_BASE_Warehouse_Inventory_By_Date_v2o2_SelItem_Addnew";
                DataTable dt = new DataTable();

                SqlParameter[] sParams = new SqlParameter[3]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.DateTime;
                sParams[0].ParameterName = "@selDateFr";
                sParams[0].Value = selDateFr;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.DateTime;
                sParams[1].ParameterName = "@selDateTo";
                sParams[1].Value = selDateTo;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.Int;
                sParams[2].ParameterName = "@selWH";
                sParams[2].Value = selWH;

                dt = cls.ExecuteDataTable(sql, sParams);
                dgvData.DataSource = dt;

                _dgvDataWidth = cls.fnGetDataGridWidth(dgvData);

                ////dgvData.Columns[0].Width = 130;    // idx
                //dgvData.Columns[1].Width = 10 * _dgvDataWidth / 100;    // inventDate
                //dgvData.Columns[2].Width = 18 * _dgvDataWidth / 100;    // matIDx
                //dgvData.Columns[3].Width = 15 * _dgvDataWidth / 100;    // matName
                //dgvData.Columns[4].Width = 6 * _dgvDataWidth / 100;    // matCode
                //dgvData.Columns[5].Width = 6 * _dgvDataWidth / 100;    // matUnit
                //dgvData.Columns[6].Width = 6 * _dgvDataWidth / 100;    // using1Day
                //dgvData.Columns[7].Width = 6 * _dgvDataWidth / 100;    // [Safety Stock]
                //dgvData.Columns[8].Width = 6 * _dgvDataWidth / 100;    // yesterday
                //dgvData.Columns[9].Width = 6 * _dgvDataWidth / 100;    // sumIN
                //dgvData.Columns[10].Width = 6 * _dgvDataWidth / 100;    // sumReturn
                //dgvData.Columns[11].Width = 5 * _dgvDataWidth / 100;    // sumOut
                //dgvData.Columns[12].Width = 10 * _dgvDataWidth / 100;    // inventory

                //dgvData.Columns[0].Visible = false;
                //dgvData.Columns[1].Visible = true;
                //dgvData.Columns[2].Visible = true;
                //dgvData.Columns[3].Visible = true;
                //dgvData.Columns[4].Visible = true;
                //dgvData.Columns[5].Visible = true;
                //dgvData.Columns[6].Visible = true;
                //dgvData.Columns[7].Visible = true;
                //dgvData.Columns[8].Visible = true;
                //dgvData.Columns[9].Visible = true;
                //dgvData.Columns[10].Visible = true;
                //dgvData.Columns[11].Visible = true;
                //dgvData.Columns[12].Visible = true;

                //dgvData.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";


                if (selWH != "100" && selWH != "114" && selWH != "115" && selWH != "116" && selWH != "118")
                //if (selWH != "100" && selWH != "114" && selWH != "116" && selWH != "118")
                {
                    //dgvData.Columns[0].Width = 10 * _dgvDataWidth / 100;    // idx
                    dgvData.Columns[1].Width = 10 * _dgvDataWidth / 100;    // inventDate
                    //dgvData.Columns[2].Width = 10 * _dgvDataWidth / 100;    // matIDx
                    dgvData.Columns[3].Width = 17 * _dgvDataWidth / 100;    // matName
                    dgvData.Columns[4].Width = 13 * _dgvDataWidth / 100;    // matCode
                    dgvData.Columns[5].Width = 4 * _dgvDataWidth / 100;    // matUnit
                    //dgvData.Columns[6].Width = 6 * _dgvDataWidth / 100;    // using1Day
                    //dgvData.Columns[7].Width = 6 * _dgvDataWidth / 100;    // stock3Days
                    //dgvData.Columns[7].Width = 6 * _dgvDataWidth / 100;    // stock14Days
                    dgvData.Columns[7].Width = 8 * _dgvDataWidth / 100;    // safetyStock
                    //dgvData.Columns[8].Width = 6 * _dgvDataWidth / 100;    // yesterday
                    dgvData.Columns[9].Width = 8 * _dgvDataWidth / 100;    // sumIN
                    dgvData.Columns[10].Width = 8 * _dgvDataWidth / 100;    // sumReturn
                    dgvData.Columns[11].Width = 8 * _dgvDataWidth / 100;    // sumOut
                    dgvData.Columns[12].Width = 8 * _dgvDataWidth / 100;    // inventory
                    dgvData.Columns[13].Width = 18 * _dgvDataWidth / 100;    // remark
                    //dgvData.Columns[14].Width = 18 * _dgvDataWidth / 100;    // longterm

                    dgvData.Columns[0].Visible = false;
                    dgvData.Columns[1].Visible = true;
                    dgvData.Columns[2].Visible = false;
                    dgvData.Columns[3].Visible = true;
                    dgvData.Columns[4].Visible = true;
                    dgvData.Columns[5].Visible = true;
                    dgvData.Columns[6].Visible = false;
                    //dgvData.Columns[7].Visible = true;
                    dgvData.Columns[7].Visible = true;
                    dgvData.Columns[8].Visible = false;
                    dgvData.Columns[9].Visible = true;
                    dgvData.Columns[10].Visible = true;
                    dgvData.Columns[11].Visible = true;
                    dgvData.Columns[12].Visible = true;
                    dgvData.Columns[13].Visible = true;
                    dgvData.Columns[14].Visible = false;
                }
                else
                {
                    //dgvData.Columns[0].Width = 10 * _dgvDataWidth / 100;    // idx
                    dgvData.Columns[1].Width = 10 * _dgvDataWidth / 100;    // inventDate
                    //dgvData.Columns[2].Width = 10 * _dgvDataWidth / 100;    // matIDx
                    dgvData.Columns[3].Width = 20 * _dgvDataWidth / 100;    // matName
                    dgvData.Columns[4].Width = 14 * _dgvDataWidth / 100;    // matCode
                    dgvData.Columns[5].Width = 4 * _dgvDataWidth / 100;    // matUnit
                    //dgvData.Columns[6].Width = 6 * _dgvDataWidth / 100;    // using1Day
                    ////dgvData.Columns[7].Width = 6 * _dgvDataWidth / 100;    // stock3Days
                    //dgvData.Columns[7].Width = 6 * _dgvDataWidth / 100;    // stock14Days
                    //dgvData.Columns[7].Width = 6 * _dgvDataWidth / 100;    // safetyStock
                    //dgvData.Columns[8].Width = 6 * _dgvDataWidth / 100;    // yesterday
                    dgvData.Columns[9].Width = 8 * _dgvDataWidth / 100;    // sumIN
                    dgvData.Columns[10].Width = 8 * _dgvDataWidth / 100;    // sumReturn
                    dgvData.Columns[11].Width = 8 * _dgvDataWidth / 100;    // sumOut
                    dgvData.Columns[12].Width = 8 * _dgvDataWidth / 100;    // inventory
                    dgvData.Columns[13].Width = 20 * _dgvDataWidth / 100;    // remark
                    //dgvData.Columns[14].Width = 20 * _dgvDataWidth / 100;    // longterm

                    dgvData.Columns[0].Visible = false;
                    dgvData.Columns[1].Visible = true;
                    dgvData.Columns[2].Visible = false;
                    dgvData.Columns[3].Visible = true;
                    dgvData.Columns[4].Visible = true;
                    dgvData.Columns[5].Visible = true;
                    dgvData.Columns[6].Visible = false;
                    //dgvData.Columns[7].Visible = false;
                    dgvData.Columns[7].Visible = false;
                    dgvData.Columns[8].Visible = false;
                    dgvData.Columns[9].Visible = true;
                    dgvData.Columns[10].Visible = true;
                    dgvData.Columns[11].Visible = true;
                    dgvData.Columns[12].Visible = true;
                    dgvData.Columns[13].Visible = true;
                    dgvData.Columns[14].Visible = false;
                }

                dgvData.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
                cls.fnFormatDatagridview(dgvData, 12, 70);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            fnLoadData();
            btnPrint.Enabled = (dgvData.Rows.Count > 0) ? true : false;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string selWH = fnWarehouseID();
            string longterm = "";
            bool _longterm = false;
            if (selWH != "100" && selWH != "114" && selWH != "115" && selWH != "116" && selWH != "118")       //RESIN; CKD; RUBBER; CHEMICAL
            //if (selWH != "100" && selWH != "114" && selWH != "116" && selWH != "118")       //RESIN; CKD; RUBBER; CHEMICAL
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    //string partID = row.Cells[0].Value.ToString();
                    //string invent = row.Cells[10].Value.ToString();
                    //string reorder = row.Cells[4].Value.ToString();
                    //string stock03 = row.Cells[5].Value.ToString();
                    //string stock14 = row.Cells[6].Value.ToString();

                    string partID = row.Cells[2].Value.ToString();
                    string invent = row.Cells[12].Value.ToString();
                    string reorder = row.Cells[6].Value.ToString();
                    string safety = row.Cells[7].Value.ToString();
                    //string stock03 = row.Cells[7].Value.ToString();
                    //string stock14 = row.Cells[8].Value.ToString();
                    longterm = row.Cells[14].Value.ToString();
                    _longterm = (longterm.ToLower() == "true") ? true : false;

                    decimal _invent = 0, _reorder = 0, _stock03 = 0, _stock14 = 0, _safety = 0, stockChk = 0, _reorder10 = 0;

                    //row.Cells[4].Style.BackColor = Color.LemonChiffon;
                    row.Cells[6].Style.BackColor = Color.LemonChiffon;

                    _invent = (invent != "" && invent != null) ? Convert.ToDecimal(invent) : 0;
                    _reorder = (reorder != "" && reorder != null) ? Convert.ToDecimal(reorder) : 0;
                    //_stock03 = (stock03 != "" && stock03 != null) ? Convert.ToDecimal(stock03) : 0;
                    //_stock14 = (stock14 != "" && stock14 != null) ? Convert.ToDecimal(stock14) : 0;
                    _safety = (safety != "" && safety != null) ? Convert.ToDecimal(safety) : 0;

                    //stockChk = (_stock03 > 0) ? _stock03 : _stock14;
                    stockChk = _safety;
                    //_reorder10 = (_reorder > 0) ? Convert.ToInt32(_reorder * 1.1) : 0;

                    row.Cells[7].Style.BackColor = Color.LimeGreen;

                    if (_reorder > 0)
                    {
                        if (_invent > stockChk)
                        {
                            //row.Cells[10].Style.BackColor = Color.DeepSkyBlue;
                            row.Cells[12].Style.BackColor = (_longterm == true) ? Color.Orange : Color.DeepSkyBlue;
                        }
                        else if (_invent <= stockChk && _invent > 0)
                        {
                            //row.Cells[10].Style.BackColor = Color.Gold;
                            row.Cells[12].Style.BackColor = Color.Gold;
                        }
                        else if (_invent == 0)
                        {
                            //row.Cells[10].Style.BackColor = Color.IndianRed;
                            row.Cells[12].Style.BackColor = Color.IndianRed;
                        }
                    }
                    else
                    {
                        //row.Cells[4].Style.BackColor = Color.Gray;
                        ////row.Cells[5].Style.BackColor = Color.Gray;
                        ////row.Cells[6].Style.BackColor = Color.Gray;
                        ////row.Cells[7].Style.BackColor = Color.Gray;
                        //row.Cells[10].Style.BackColor = Color.Gray;

                        row.Cells[6].Style.BackColor = Color.Gray;
                        //row.Cells[7].Style.BackColor = Color.Gray;
                        //row.Cells[8].Style.BackColor = Color.Gray;
                        //row.Cells[9].Style.BackColor = Color.Gray;
                        row.Cells[12].Style.BackColor = Color.Gray;

                    }
                }
            }
            else
            {
                string invent = "";
                decimal _invent = 0;
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    invent = row.Cells[12].Value.ToString();
                    _invent = (invent != "" && invent != null) ? Convert.ToDecimal(invent) : 0;

                    longterm = row.Cells[14].Value.ToString();
                    _longterm = (longterm.ToLower() == "true") ? true : false;

                    if (_invent == 0)
                    {
                        row.Cells[12].Style.BackColor = Color.Gold;
                    }
                    else
                    {
                        row.Cells[12].Style.BackColor = (_longterm == true) ? Color.Orange : Color.DeepSkyBlue;
                    }
                }
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                cls.fnDatagridClickCell(dgvData, e);
                DataGridViewRow row = new DataGridViewRow();
                row = dgvData.Rows[e.RowIndex];
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime selDateFr = dtpDateFr.Value;
            string headerPrint = String.Format("{0:dd/MM/yyyy}", selDateFr);
            //ClsPrint _ClsPrint = new ClsPrint(dgvData, "Inventory " + headerPrint);
            //_ClsPrint.PrintForm();

            //PrintDialog printDialog = new PrintDialog();
            //printDialog.Document = printDocument1;
            //printDialog.UseEXDialog = true;
            ////Get the document
            //if (DialogResult.OK == printDialog.ShowDialog())
            //{
            //    printDocument1.DocumentName = "Inventory " + headerPrint;
            //    printDocument1.Print();
            //}
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //try
            //{
            //    strFormat = new StringFormat();
            //    strFormat.Alignment = StringAlignment.Near;
            //    strFormat.LineAlignment = StringAlignment.Center;
            //    strFormat.Trimming = StringTrimming.EllipsisCharacter;

            //    arrColumnLefts.Clear();
            //    arrColumnWidths.Clear();
            //    iCellHeight = 0;
            //    iRow = 0;
            //    bFirstPage = true;
            //    bNewPage = true;

            //    // Calculating Total Widths
            //    iTotalWidth = 0;
            //    foreach (DataGridViewColumn dgvGridCol in dgvData.Columns)
            //    {
            //        iTotalWidth += dgvGridCol.Width;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap bm = new Bitmap(this.dgvData.Width, this.dgvData.Height);
            //dgvData.DrawToBitmap(bm, new Rectangle(0, 0, this.dgvData.Width, this.dgvData.Height));
            //e.Graphics.DrawImage(bm, 0, 0);

            //DateTime selDateFr = dtpDateFr.Value;
            //string headerPrint = String.Format("{0:dd/MM/yyyy}", selDateFr);
            //try
            //{
            //    //Set the left margin
            //    int iLeftMargin = e.MarginBounds.Left;
            //    //Set the top margin
            //    int iTopMargin = e.MarginBounds.Top;
            //    //Whether more pages have to print or not
            //    bool bMorePagesToPrint = false;
            //    int iTmpWidth = 0;

            //    //For the first page to print set the cell width and header height
            //    if (bFirstPage)
            //    {
            //        foreach (DataGridViewColumn GridCol in dgvData.Columns)
            //        {
            //            iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
            //                (double)iTotalWidth * (double)iTotalWidth *
            //                ((double)e.MarginBounds.Width / (double)iTotalWidth))));

            //            iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
            //                GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

            //            // Save width and height of headers
            //            arrColumnLefts.Add(iLeftMargin);
            //            arrColumnWidths.Add(iTmpWidth);
            //            iLeftMargin += iTmpWidth;
            //        }
            //    }
            //    //Loop till all the grid rows not get printed
            //    while (iRow <= dgvData.Rows.Count - 1)
            //    {
            //        DataGridViewRow GridRow = dgvData.Rows[iRow];
            //        //Set the cell height
            //        iCellHeight = GridRow.Height + 5;
            //        int iCount = 0;
            //        //Check whether the current page settings allows more rows to print
            //        if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
            //        {
            //            bNewPage = true;
            //            bFirstPage = false;
            //            bMorePagesToPrint = true;
            //            break;
            //        }
            //        else
            //        {
            //            if (bNewPage)
            //            {
            //                //Draw Header
            //                e.Graphics.DrawString("Inventory "+ headerPrint,
            //                    new Font(dgvData.Font, FontStyle.Bold),
            //                    Brushes.Black, e.MarginBounds.Left,
            //                    e.MarginBounds.Top - e.Graphics.MeasureString("Inventory "+ headerPrint,
            //                    new Font(dgvData.Font, FontStyle.Bold),
            //                    e.MarginBounds.Width).Height - 13);

            //                String strDate = DateTime.Now.ToLongDateString() + " " +
            //                    DateTime.Now.ToShortTimeString();
            //                //Draw Date
            //                e.Graphics.DrawString(strDate,
            //                    new Font(dgvData.Font, FontStyle.Bold), Brushes.Black,
            //                    e.MarginBounds.Left +
            //                    (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
            //                    new Font(dgvData.Font, FontStyle.Bold),
            //                    e.MarginBounds.Width).Width),
            //                    e.MarginBounds.Top - e.Graphics.MeasureString("Inventory "+ headerPrint,
            //                    new Font(new Font(dgvData.Font, FontStyle.Bold),
            //                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

            //                //Draw Columns                 
            //                iTopMargin = e.MarginBounds.Top;
            //                foreach (DataGridViewColumn GridCol in dgvData.Columns)
            //                {
            //                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
            //                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
            //                        (int)arrColumnWidths[iCount], iHeaderHeight));

            //                    e.Graphics.DrawRectangle(Pens.Black,
            //                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
            //                        (int)arrColumnWidths[iCount], iHeaderHeight));

            //                    e.Graphics.DrawString(GridCol.HeaderText,
            //                        GridCol.InheritedStyle.Font,
            //                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
            //                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
            //                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
            //                    iCount++;
            //                }
            //                bNewPage = false;
            //                iTopMargin += iHeaderHeight;
            //            }
            //            iCount = 0;
            //            //Draw Columns Contents                
            //            foreach (DataGridViewCell Cel in GridRow.Cells)
            //            {
            //                if (Cel.Value != null)
            //                {
            //                    e.Graphics.DrawString(Cel.Value.ToString(),
            //                        Cel.InheritedStyle.Font,
            //                        new SolidBrush(Cel.InheritedStyle.ForeColor),
            //                        new RectangleF((int)arrColumnLefts[iCount],
            //                        (float)iTopMargin,
            //                        (int)arrColumnWidths[iCount], (float)iCellHeight),
            //                        strFormat);
            //                }
            //                //Drawing Cells Borders 
            //                e.Graphics.DrawRectangle(Pens.Black,
            //                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
            //                    (int)arrColumnWidths[iCount], iCellHeight));
            //                iCount++;
            //            }
            //        }
            //        iRow++;
            //        iTopMargin += iCellHeight;
            //    }
            //    //If more lines exist, print another page.
            //    if (bMorePagesToPrint)
            //        e.HasMorePages = true;
            //    else
            //        e.HasMorePages = false;
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
            //       MessageBoxIcon.Error);
            //}
        }

        private void cbbWH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //btnFilter.Enabled = (cbbWH.SelectedIndex > 0) ? true : false;
            if (cbbWH.SelectedIndex == 0)
            {
                btnFilter.Enabled = false;
                btnPrint.Enabled = false;
            }
            else
            {
                btnFilter.Enabled = true;
            }
        }
    }
}
