using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmFinishGoodScanIn_v2o : Form
    {
        public int cate = 9;
        public string cateId;
        public string prodCate;
        public string prodId;
        public string prodName;
        public string prodCode;
        public string prodTitle;
        public string packing;
        public string prodQty;
        public string batchno;
        public string pcs, box, car, pal, sho;
        public string strBarcode;
        public string warehouse;
        public string location;

        private string factcd = "F1";
        private string shiftsno = "1";
        private string workdate = "";
        private string workdate_vn = "";
        private string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private DateTime sTime1 = new DateTime();
        private DateTime sTime2 = new DateTime();
        private string shiftsnm = "Night";

        private System.Windows.Forms.Timer timerCountdown;
        private System.Windows.Forms.Timer timerCountOut;
        private int counter = 15;
        private int counterDisplay, counterOut;
        public string machine;

        public int dgvListWidth;
        public int dgvScanOutWidth;

        BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
        //private PrintDocument printDocument1 = new PrintDocument();
        //private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

        public frmFinishGoodScanIn_v2o()
        {
            InitializeComponent();

            counterDisplay = counter;

            switch (cate)
            {
                case 1:
                    cateId = "101";
                    prodCate = "gas%";      // GASKET
                    prodTitle = "GASKET IN-STOCK SYSTEM";
                    break;
                case 2:
                    cateId = "101";
                    prodCate = "bel%";      // BELLOWS
                    prodTitle = "BELLOWS IN-STOCK SYSTEM";
                    break;
                case 3:
                    cateId = "104";
                    prodCate = "auto%";     // AUTO BALANCE
                    prodTitle = "AUTO BALANCE IN-STOCK SYSTEM";
                    break;
                case 4:
                    cateId = "104";
                    prodCate = "dis%";      // DISPENSER
                    prodTitle = "DISPENSER IN-STOCK SYSTEM";
                    break;
                case 5:
                    cateId = "104";
                    prodCate = "pump%";     // PUMP
                    prodTitle = "PUMP IN-STOCK SYSTEM";
                    break;
                case 6:
                    cateId = "104";
                    prodCate = "bal%";      // BALANCE WEIGHT
                    prodTitle = "WEIGHT BALANCE IN-STOCK SYSTEM";
                    break;
                case 7:
                    cateId = "101";
                    prodCate = "mix%";      // MIXING
                    prodTitle = "MIXING IN-STOCK SYSTEM";
                    break;
                case 8:
                    cateId = "103";
                    prodCate = "";         // SINGLE PART (WIP)
                    prodTitle = "W.I.P IN-STOCK SYSTEM";
                    break;
                case 9:
                    cateId = "106";
                    prodCate = "%vn%";
                    prodTitle = "F.M.B IN-STOCK SYSTEM";
                    break;
            }

            
        }

        private void frmFinishGoodScanIn_v2o_Load(object sender, EventArgs e)
        {
            dgvListWidth = cls.fnGetDataGridWidth(dgvList);
            dgvScanOutWidth = cls.fnGetDataGridWidth(dgvScanOut);

            init();

            //btnPrint.Click += new EventHandler(button1_Click);
            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
            fnWorkdate();
        }

        public void init()
        {
            fnGetdate();
            toolStripStatusLabel1.Text = prodTitle;
            //printDocument1.DefaultPageSettings.Landscape = true;


            //lblLabel_Shift.Visible = false;
            //lblLabel_Batchno.Visible = false;
            //lblLabel_DateProduce.Visible = false;
            //lblLabel_HourProduce.Visible = false;
            //lblLabel_Quantity.Visible = false;
            //lblLabel_Partname.Visible = false;
            //lblLabel_PIC.Visible = false;
            //lblLabel_LOT.Visible = false;
            //pictureBox1.Visible = false;

            lblPartcode.Text = "";
            lblDateProduce.Text = "";
            lblLOT.Text = "";
            lblWarehouse.Text = "";
            lblLocation.Text = "";

            fnLoadPIC();
            fnLoadPartname();
            fnLoadOUT();


            lblLabel_Shift.Text = "";
            lblLabel_Batchno.Text = "";
            lblLabel_DateProduce.Text = "";
            lblLabel_HourProduce.Text = "";
            lblLabel_Quantity.Text = "";
            lblLabel_Partname.Text = "";
            lblLabel_PIC.Text = "";
            lblLabel_LOT.Text = "";
            pictureBox1.Visible = false;

            lblCountOut.Text = "";
        }

        public void fnGetdate()
        {
            if(check.IsConnectedToInternet())
            {
                tssDatetime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                tssDatetime.ForeColor = Color.Black;

                if (cbbPartname.SelectedIndex > 0)
                {
                    lblDateProduce.Text = workdate_vn;
                    lblLOT.Text = workdate + "-" + shiftsno;
                }
            }
            else
            {
                tssDatetime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}",DateTime.Now);
                tssDatetime.ForeColor = Color.Red;
            }
        }

        public void fnLoadPIC()
        {
            try
            {
                string sql = "V2o1_BASE_Inventory_InStock_SelPIC_SelItem_Addnew";
                DataTable dtPIC = new DataTable();
                dtPIC = cls.fnSelect(sql);
                cbbPIC.DataSource = dtPIC;
                cbbPIC.DisplayMember = "picName";
                cbbPIC.ValueMember = "idx";
                dtPIC.Rows.InsertAt(dtPIC.NewRow(), 0);
                cbbPIC.SelectedIndex = 0;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadList()
        {
            try
            {
                if (prodId != "" && prodId != null)
                {
                    string sql = "V2o1_BASE_Inventory_InStock_SelList_SelItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@prodId";
                    sParams[0].Value = prodId;

                    DataTable dt = new DataTable();
                    dt = cls.ExecuteDataTable(sql, sParams);
                    dgvList.DataSource = dt;

                    dgvListWidth = cls.fnGetDataGridWidth(dgvList);

                    //dgvList.Columns[0].Width = 20 * dgvListWidth / 100;    // idx
                    dgvList.Columns[1].Width = 28 * dgvListWidth / 100;    // packingCode
                    //dgvList.Columns[2].Width = 20 * dgvListWidth / 100;    // partId
                    //dgvList.Columns[3].Width = 15 * dgvListWidth / 100;    // partName
                    dgvList.Columns[4].Width = 15 * dgvListWidth / 100;    // partCode
                    //dgvList.Columns[5].Width = 20 * dgvListWidth / 100;    // workShift
                    //dgvList.Columns[6].Width = 20 * dgvListWidth / 100;    // workDate
                    dgvList.Columns[7].Width = 8 * dgvListWidth / 100;    // workTime
                    dgvList.Columns[8].Width = 7 * dgvListWidth / 100;    // quantity
                    dgvList.Columns[9].Width = 5 * dgvListWidth / 100;    // unit
                    //dgvList.Columns[10].Width = 20 * dgvListWidth / 100;    // pic
                    dgvList.Columns[11].Width = 10 * dgvListWidth / 100;    // lotno
                    dgvList.Columns[12].Width = 10 * dgvListWidth / 100;    // locPos
                    dgvList.Columns[13].Width = 17 * dgvListWidth / 100;    // inDate

                    dgvList.Columns[0].Visible = false;
                    dgvList.Columns[1].Visible = true;
                    dgvList.Columns[2].Visible = false;
                    dgvList.Columns[3].Visible = false;
                    dgvList.Columns[4].Visible = true;
                    dgvList.Columns[5].Visible = false;
                    dgvList.Columns[6].Visible = false;
                    dgvList.Columns[7].Visible = true;
                    dgvList.Columns[8].Visible = true;
                    dgvList.Columns[9].Visible = true;
                    dgvList.Columns[10].Visible = false;
                    dgvList.Columns[11].Visible = true;
                    dgvList.Columns[12].Visible = true;
                    dgvList.Columns[13].Visible = true;

                    dgvList.Columns[7].DefaultCellStyle.Format = "HH:mm";
                    dgvList.Columns[13].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

                    cls.fnFormatDatagridview(dgvList, 11);
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadPartname()
        {
            try
            {
                string sql = "V2o1_BASE_Inventory_InStock_SelName_SelItem_Addnew";
                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.NVarChar;
                sParams[0].ParameterName = "@name";
                sParams[0].Value = prodCate;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@cate";
                sParams[1].Value = cateId;

                DataTable dtProdName = new DataTable();
                dtProdName = cls.ExecuteDataTable(sql, sParams);
                cbbPartname.DataSource = dtProdName;
                cbbPartname.DisplayMember = "Name";
                cbbPartname.ValueMember = "prodId";
                dtProdName.Rows.InsertAt(dtProdName.NewRow(), 0);
                cbbPartname.SelectedIndex = 0;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadOUT()
        {
            string sql = "V2o1_BASE_Inventory_OutStock_FMB_List_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            dgvScanOut.DataSource = dt;

            dgvScanOutWidth = cls.fnGetDataGridWidth(dgvScanOut);

            //dgvScanOut.Columns[0].Width = 20 * dgvScanOutWidth / 100;    // inIDx
            //dgvScanOut.Columns[1].Width = 20 * dgvScanOutWidth / 100;    // outIDX
            dgvScanOut.Columns[2].Width = 50 * dgvScanOutWidth / 100;    // Packing
            //dgvScanOut.Columns[3].Width = 20 * dgvScanOutWidth / 100;    // Partname
            dgvScanOut.Columns[4].Width = 25 * dgvScanOutWidth / 100;    // Partcode
            //dgvScanOut.Columns[5].Width = 20 * dgvScanOutWidth / 100;    // Outdate
            dgvScanOut.Columns[6].Width = 25 * dgvScanOutWidth / 100;    // FMB Time

            dgvScanOut.Columns[0].Visible = false;
            dgvScanOut.Columns[1].Visible = false;
            dgvScanOut.Columns[2].Visible = true;
            dgvScanOut.Columns[3].Visible = false;
            dgvScanOut.Columns[4].Visible = true;
            dgvScanOut.Columns[5].Visible = false;
            dgvScanOut.Columns[6].Visible = true;

            cls.fnFormatDatagridview(dgvScanOut, 11);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                string partID = prodId;
                string partName = prodName;
                string partCode = prodCode;
                string shift = lblLabel_Shift.Text;
                string batch = lblLabel_Batchno.Text;
                DateTime date = Convert.ToDateTime(lblLabel_DateProduce.Text);
                DateTime time = Convert.ToDateTime(lblLabel_HourProduce.Text);
                string quantity = lblLabel_Quantity.Text;
                string picID = cbbPIC.SelectedValue.ToString();
                string picName = lblLabel_PIC.Text;
                string lot = lblLabel_LOT.Text;
                string packing = strBarcode;
                string locWH = warehouse;
                string locPos = location;

                string sql = "V2o1_BASE_Inventory_InStock_AddItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[13]; // Parameter count
                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "@packingCode";
                sParams[0].Value = packing;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "@partId";
                sParams[1].Value = partID;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.NVarChar;
                sParams[2].ParameterName = "@partName";
                sParams[2].Value = partName;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.VarChar;
                sParams[3].ParameterName = "@partCode";
                sParams[3].Value = partCode;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.VarChar;
                sParams[4].ParameterName = "@workShift";
                sParams[4].Value = shift;

                sParams[5] = new SqlParameter();
                sParams[5].SqlDbType = SqlDbType.DateTime;
                sParams[5].ParameterName = "@workDate";
                sParams[5].Value = date;

                sParams[6] = new SqlParameter();
                sParams[6].SqlDbType = SqlDbType.DateTime;
                sParams[6].ParameterName = "@workTime";
                sParams[6].Value = time;

                sParams[7] = new SqlParameter();
                sParams[7].SqlDbType = SqlDbType.Int;
                sParams[7].ParameterName = "@batchno";
                sParams[7].Value = batch;

                sParams[8] = new SqlParameter();
                sParams[8].SqlDbType = SqlDbType.Int;
                sParams[8].ParameterName = "@quantity";
                sParams[8].Value = quantity;

                sParams[9] = new SqlParameter();
                sParams[9].SqlDbType = SqlDbType.NVarChar;
                sParams[9].ParameterName = "@pic";
                sParams[9].Value = picName;

                sParams[10] = new SqlParameter();
                sParams[10].SqlDbType = SqlDbType.VarChar;
                sParams[10].ParameterName = "@lotno";
                sParams[10].Value = lot;

                sParams[11] = new SqlParameter();
                sParams[11].SqlDbType = SqlDbType.VarChar;
                sParams[11].ParameterName = "@locWH";
                sParams[11].Value = locWH;

                sParams[12] = new SqlParameter();
                sParams[12].SqlDbType = SqlDbType.VarChar;
                sParams[12].ParameterName = "@locPos";
                sParams[12].Value = locPos;

                cls.fnUpdDel(sql, sParams);
                fnLoadList();


                //////////////////////////////////////////////////////////////////////////
                //string msg = "";                                                      //
                //msg += "partID: " + partID + "\r\n";                                  //
                //msg += "partName: " + partName + "\r\n";                              //
                //msg += "partCode: " + partCode + "\r\n";                              //
                //msg += "shift: " + shift + "\r\n";                                    //
                //msg += "batch: " + batch + "\r\n";                                    //
                //msg += "date: " + String.Format("{0:dd/MM/yyyy}", date) + "\r\n";     //
                //msg += "time: " + String.Format("{0:HH:mm}", time) + "\r\n";          //
                //msg += "quantity: " + quantity + "\r\n";                              //
                //msg += "picID: " + picID + "\r\n";                                    //
                //msg += "picName: " + picName + "\r\n";                                //
                //msg += "lot: " + lot + "\r\n";                                        //
                //msg += "packing: " + packing + "\r\n";                                //
                //msg += "locWH: " + locWH + "\r\n";                                    //
                //msg += "locPos: " + locPos + "\r\n";                                  //
                //MessageBox.Show(msg);                                                 //
                //return;                                                               //
                //////////////////////////////////////////////////////////////////////////


                //btnPrint.Enabled = false;

                PrinterSettings ps = new PrinterSettings();
                PrintDocument doc = new PrintDocument();
                Margins margins = new Margins(5, 45, 5, 5);

                ps.Copies = 1;
                ps.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
                doc.PrinterSettings = ps;

                IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA6 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A6); // setting paper size to A6 size
                doc.DefaultPageSettings.PaperSize = sizeA6;
                doc.DefaultPageSettings.Landscape = true;
                doc.DefaultPageSettings.Margins = margins;

                doc.PrintPage += this.Doc_PrintPage;
                
                doc.Print();

                //////////////////////////////////////////////////////
                //PrintDialog dlgSettings = new PrintDialog();      //
                //dlgSettings.Document = doc;                       //
                //if (dlgSettings.ShowDialog() == DialogResult.OK)  //
                //{                                                 //
                //    doc.Print();                                  //
                //}                                                 //
                //////////////////////////////////////////////////////

                timerCountdown.Stop();
                timerCountdown.Dispose();

                fnFinish();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(this.tableLayoutPanel4.Width, this.tableLayoutPanel4.Height);
                this.tableLayoutPanel4.DrawToBitmap(bmp, new Rectangle(0, 0, this.tableLayoutPanel4.Width, this.tableLayoutPanel4.Height));
                //float x = e.MarginBounds.Left;
                //float y = e.MarginBounds.Top;
                //e.Graphics.DrawImage((Image)bmp, x, y);

                Image i = bmp;
                Rectangle m = e.MarginBounds;
                if ((double)i.Width / (double)(i.Height) > (double)(m.Width) / (double)(m.Height)) // image is wider
                {
                    m.Height = (int)((double)(i.Height) / (double)(i.Width) * (double)(m.Width));
                }
                else
                {
                    m.Width = (int)((double)(i.Width) / (double)(i.Height) * (double)(m.Height));
                }
                e.Graphics.DrawImage((Image)bmp, m);
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void rdbPCS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                prodQty = txtPCS.Text;
                packing = "PCS";
                btnPreview.Enabled = (cbbPIC.SelectedIndex > 0) && (cbbPartname.SelectedIndex > 0) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void rdbBOX_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                prodQty = txtBOX.Text;
                packing = "BOX";
                btnPreview.Enabled = (cbbPIC.SelectedIndex > 0) && (cbbPartname.SelectedIndex > 0) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void rdbCAR_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                prodQty = txtCAR.Text;
                packing = "CAR";
                btnPreview.Enabled = (cbbPIC.SelectedIndex > 0) && (cbbPartname.SelectedIndex > 0) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void rdbPAL_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                prodQty = txtPAL.Text;
                packing = "PAL";
                btnPreview.Enabled = (cbbPIC.SelectedIndex > 0) && (cbbPartname.SelectedIndex > 0) ? true : false;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                fnLoadInfo();

                prodId = cbbPartname.SelectedValue.ToString();
                prodName = cbbPartname.Text;
                prodCode = lblPartcode.Text;
                warehouse = lblWarehouse.Text;
                location = lblLocation.Text;

                lblLabel_Shift.Text = shiftsnm.ToUpper();
                lblLabel_Batchno.Text = batchno;
                lblLabel_DateProduce.Text = workdate_vn;
                lblLabel_HourProduce.Text = String.Format("{0:HH:mm}", DateTime.Now);
                lblLabel_Quantity.Text = prodQty;
                lblLabel_Partname.Text = cbbPartname.Text.ToUpper();
                lblLabel_PIC.Text = cbbPIC.Text.ToUpper();
                lblLabel_LOT.Text = lblLOT.Text;
                fnLoadBarcode();
                pictureBox1.Visible = true;

                cbbPIC.Enabled = false;
                btnPrint.Enabled = true;

                timerCountdown = new System.Windows.Forms.Timer();
                counterDisplay = counter;
                timerCountdown.Stop();
                timerCountdown.Tick += new EventHandler(timerCountdown_Tick);
                timerCountdown.Interval = 1000; // 1 second
                timerCountdown.Start();
                lblCountDown.Visible = true;
                lblCountDown.Text = "Label will be auto erased in " + (counterDisplay) + " seconds";
                btnPreview.Enabled = false;

                cbbPartname.Enabled = false;
                rdbPCS.Enabled = false;
                rdbBOX.Enabled = false;
                rdbCAR.Enabled = false;
                rdbPAL.Enabled = false;
                rdbSHO.Enabled = false;

                //lblLabel_Shift.Visible = true;
                //lblLabel_Batchno.Visible = true;
                //lblLabel_DateProduce.Visible = true;
                //lblLabel_HourProduce.Visible = true;
                //lblLabel_Quantity.Visible = true;
                //lblLabel_Partname.Visible = true;
                //lblLabel_PIC.Visible = true;
                //lblLabel_LOT.Visible = true;
                //pictureBox1.Visible = true;
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void timerCountdown_Tick(object sender, EventArgs e)
        {
            try
            {
                counterDisplay--;
                if (counterDisplay == 0)
                {
                    timerCountdown.Stop();
                    fnFinish();
                    counterDisplay = counter;
                    cbbPIC.Enabled = true;
                }
                else
                {
                    cbbPIC.Enabled = false;
                    lblCountDown.Text = "Label will be auto erased in " + (counterDisplay) + " seconds";
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void timerCountOut_Tick(object sender, EventArgs e)
        {
            try
            {
                counterOut--;
                if (counterOut == 0)
                {
                    timerCountOut.Stop();
                    fnFinishOut();
                    counterOut = counter;
                    lblCountOut.Text = "";
                }
                else
                {
                    lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnFinish()
        {
            try
            {
                prodId = "";
                prodName = "";
                prodCode = "";
                prodTitle = "";
                packing = "";
                prodQty = "";
                batchno = "";
                pcs = "";
                box = "";
                car = "";
                pal = "";
                sho = "";
                strBarcode = "";
                warehouse = "";
                location = "";

                cbbPartname.Enabled = true;
                rdbPCS.Enabled = (txtPCS.Text != "0" && txtPCS.Text != "") ? true : false;
                rdbBOX.Enabled = (txtBOX.Text != "0" && txtBOX.Text != "") ? true : false;
                rdbCAR.Enabled = (txtCAR.Text != "0" && txtCAR.Text != "") ? true : false;
                rdbPAL.Enabled = (txtPAL.Text != "0" && txtPAL.Text != "") ? true : false;
                rdbSHO.Enabled = (txtSHO.Text != "0" && txtSHO.Text != "") ? true : false;

                //cbbPartname.SelectedIndex = 0;
                //lblPartcode.Text = "";
                rdbPCS.Checked = false;
                rdbBOX.Checked = false;
                rdbCAR.Checked = false;
                rdbPAL.Checked = false;
                rdbSHO.Checked = false;

                //rdbPCS.Enabled = false;
                //rdbBOX.Enabled = false;
                //rdbCAR.Enabled = false;
                //rdbPAL.Enabled = false;
                rdbSHO.Enabled = false;

                //txtPCS.Text = "";
                //txtBOX.Text = "";
                //txtCAR.Text = "";
                //txtPAL.Text = "";
                //txtSHO.Text = "";

                //txtPCS.Enabled = false;
                //txtBOX.Enabled = false;
                //txtCAR.Enabled = false;
                //txtPAL.Enabled = false;
                //txtSHO.Enabled = false;

                //txtPCS.BackColor = Color.FromKnownColor(KnownColor.Control);
                //txtBOX.BackColor = Color.FromKnownColor(KnownColor.Control);
                //txtCAR.BackColor = Color.FromKnownColor(KnownColor.Control);
                //txtPAL.BackColor = Color.FromKnownColor(KnownColor.Control);
                //txtSHO.BackColor = Color.FromKnownColor(KnownColor.Control);

                //lblDateProduce.Text = "";
                //lblLOT.Text = "";
                //lblWarehouse.Text = "";
                //lblLocation.Text = "";

                lblLabel_Shift.Text = "";
                lblLabel_Batchno.Text = "";
                lblLabel_DateProduce.Text = "";
                lblLabel_HourProduce.Text = "";
                lblLabel_Quantity.Text = "";
                lblLabel_Partname.Text = "";
                lblLabel_PIC.Text = "";
                lblLabel_LOT.Text = "";
                pictureBox1.Visible = false;

                lblCountDown.Visible = false;
                btnPreview.Enabled = false;
                btnPrint.Enabled = false;

                //////////////////////////////////////////////
                //////////////////////////////////////////////
                //////////////////////////////////////////////

                rdbRub01.Checked = false;
                rdbRub02.Checked = false;
                rdbRub03.Checked = false;
                rdbRub04.Checked = false;
                rdbRub05.Checked = false;
                rdbRub06.Checked = false;
                rdbRub07.Checked = false;
                rdbRub08.Checked = false;
                rdbRub09.Checked = false;
                rdbRub10.Checked = false;
                rdbRub11.Checked = false;
                rdbRub12.Checked = false;
                rdbRub13.Checked = false;
                rdbRub14.Checked = false;
                rdbRub15.Checked = false;
                rdbRub16.Checked = false;
                rdbRub17.Checked = false;
                rdbRub18.Checked = false;

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnFinishOut()
        {
            machine = "";
            rdbRub01.Checked = false;
            rdbRub02.Checked = false;
            rdbRub03.Checked = false;
            rdbRub04.Checked = false;
            rdbRub05.Checked = false;
            rdbRub06.Checked = false;
            rdbRub07.Checked = false;
            rdbRub08.Checked = false;
            rdbRub09.Checked = false;
            rdbRub10.Checked = false;
            rdbRub11.Checked = false;
            rdbRub12.Checked = false;
            rdbRub13.Checked = false;
            rdbRub14.Checked = false;
            rdbRub15.Checked = false;
            rdbRub16.Checked = false;
            rdbRub17.Checked = false;
            rdbRub18.Checked = false;
            txtScanOut.Text = "";
            txtScanOut.Enabled = false;
        }

        private void gASKETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void rdbRub01_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbRub01.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 01";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub02_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub02.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 02";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub03_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub03.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 03";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub04_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub04.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 04";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub05_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub05.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 05";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub06_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub06.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 06";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub07_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub07.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 07";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub08_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub08.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 08";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub09_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub09.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 09";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub10_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub10.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 10";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub11_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub11.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 11";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub12_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub12.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 12";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub13_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub13.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 13";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub14_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub14.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 14";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub15_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub15.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 15";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub16_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub16.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 16";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub17_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub17.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 17";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void rdbRub18_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRub18.Checked)
            {
                txtScanOut.Enabled = true;
                txtScanOut.BackColor = Color.DodgerBlue;
                txtScanOut.Focus();
                machine = "RUBBER 18";

                timerCountOut = new System.Windows.Forms.Timer();
                counterOut = counter;
                timerCountOut.Stop();
                timerCountOut.Tick += new EventHandler(timerCountOut_Tick);
                timerCountOut.Interval = 1000; // 1 second
                timerCountOut.Start();
                
                lblCountOut.Text = "Label will be auto erased in " + (counterOut) + " seconds";
            }
            else
            {
                timerCountOut.Stop();
                timerCountOut.Dispose();
                machine = "";

                txtScanOut.Enabled = false;
                txtScanOut.BackColor = Color.Silver;
            }
        }

        private void txtScanOut_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string packing = "";
                string _machine = "";
                string msgText = "";
                packing = txtScanOut.Text.Trim();
                _machine = machine;
                string inDate = "";
                DateTime _inDate,_dt;
                TimeSpan inventTime;
                double totalHour;
                _dt = DateTime.Now;
                if (e.KeyCode == Keys.Enter)
                {
                    if (packing != "" && packing != null)
                    {
                        string sqlChk = "V2o1_BASE_Inventory_OutStock_FMB_SelChkQty_SelItem_Addnew";
                        SqlParameter[] sParamsChkQty = new SqlParameter[1]; // Parameter count
                        sParamsChkQty[0] = new SqlParameter();
                        sParamsChkQty[0].SqlDbType = SqlDbType.VarChar;
                        sParamsChkQty[0].ParameterName = "@packing";
                        sParamsChkQty[0].Value = packing;

                        DataSet dsChkQty = new DataSet();
                        dsChkQty = cls.ExecuteDataSet(sqlChk, sParamsChkQty);
                        if (dsChkQty.Tables.Count > 0)
                        {
                            if(dsChkQty.Tables[0].Rows.Count>0)
                            {
                                inDate = dsChkQty.Tables[0].Rows[0][2].ToString();
                                _inDate = (inDate != "" && inDate != null) ? Convert.ToDateTime(inDate) : DateTime.Now;

                                inventTime = _dt - _inDate;
                                totalHour = inventTime.TotalHours;
                                if (totalHour >= 6)
                                {
                                    //////////////////////////////////////////////////////////////////////////////
                                    string sql = "V2o1_BASE_Inventory_OutStock_FMB_AddItem_Addnew";             //
                                    SqlParameter[] sParams = new SqlParameter[2]; // Parameter count            //
                                    sParams[0] = new SqlParameter();                                            //
                                    sParams[0].SqlDbType = SqlDbType.VarChar;                                   //
                                    sParams[0].ParameterName = "@packing";                                      //
                                    sParams[0].Value = packing;                                                 //
                                                                                                                //
                                    sParams[1] = new SqlParameter();                                            //
                                    sParams[1].SqlDbType = SqlDbType.VarChar;                                   //
                                    sParams[1].ParameterName = "@machine";                                      //
                                    sParams[1].Value = _machine;                                                //
                                                                                                                //
                                    cls.fnUpdDel(sql, sParams);                                                 //
                                                                                                                //
                                    msgText = "Out stock successful.";                                          //
                                                                                                                //
                                    fnLoadOUT();                                                                //
                                    fnFinishOut();                                                              //
                                    //////////////////////////////////////////////////////////////////////////////
                                }
                                else
                                {
                                    msgText = "Packing (" + packing + ") not enough time (6h) in FMB-ROOM, please re-check and scan-out again.";
                                }
                            }
                            else
                            {
                                msgText = "Packing (" + packing + ") cannot found on system, please re-check and scan-out again.";
                            }
                        }
                        else
                        {
                            msgText = "Packing (" + packing + ") cannot found on system, please re-check and scan-out again.";
                        }
                    }
                    else
                    {
                        msgText = "Invalid NULL packing code, please re-check and scan out again.";
                    }

                    MessageBox.Show(msgText);
                    txtScanOut.Text = "";
                    txtScanOut.Focus();
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadBarcode()
        {
            string barcode01 = "PRO";
            //string barcode02 = packing;
            string barcode02 = "BOX";
            string barcode03 = prodId + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            strBarcode = barcode01 + "-" + barcode02 + "-" + barcode03;

            errorProvider1.Clear();
            int width = 250;
            int height = 90;
            barcode.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            try
            {
                if (type != BarcodeLib.TYPE.UNSPECIFIED)
                {
                    barcode.IncludeLabel = true;
                    //barcode.RotateFlipType= (RotateFlipType)Enum.Parse(typeof(RotateFlipType), "Center", true);
                    pictureBox1.BackgroundImage = barcode.Encode(type, strBarcode, Color.Black, Color.White, width, height);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Center;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void cbbPartname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fnLoadInfo();
            fnLoadList();
        }

        public void fnLoadInfo()
        {
            try
            {
                prodName = "";
                prodCode = "";
                prodTitle = "";
                packing = "";
                prodQty = "";
                rdbPCS.Checked = false;
                rdbBOX.Checked = false;
                rdbCAR.Checked = false;
                rdbPAL.Checked = false;
                rdbSHO.Checked = false;

                lblLabel_Shift.Text = "";
                lblLabel_Batchno.Text = "";
                lblLabel_DateProduce.Text = "";
                lblLabel_HourProduce.Text = "";
                lblLabel_Quantity.Text = "";
                lblLabel_Partname.Text = "";
                lblLabel_PIC.Text = "";
                lblLabel_LOT.Text = "";
                pictureBox1.Visible = false;

                if (cbbPartname.SelectedIndex > 0)
                {
                    prodId = cbbPartname.SelectedValue.ToString();

                    string sql = "V2o1_BASE_Inventory_InStock_SelInfo_SelItem_Addnew";
                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.Int;
                    sParams[0].ParameterName = "@prodId";
                    sParams[0].Value = prodId;

                    DataSet ds = new DataSet();
                    ds = cls.ExecuteDataSet(sql, sParams);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblPartcode.Text = ds.Tables[0].Rows[0][0].ToString();
                            //txtPCS.Text = ds.Tables[0].Rows[0][1].ToString();
                            //txtBOX.Text = ds.Tables[0].Rows[0][2].ToString();
                            //txtCAR.Text = ds.Tables[0].Rows[0][3].ToString();
                            //txtPAL.Text = ds.Tables[0].Rows[0][4].ToString();
                            //txtSHO.Text = ds.Tables[0].Rows[0][5].ToString();
                            txtPCS.Text = "75";
                            txtBOX.Text = "150";
                            txtCAR.Text = "225";
                            txtPAL.Text = "300";
                            txtSHO.Text = "0";
                            lblDateProduce.Text = workdate_vn;
                            lblLOT.Text = workdate + "-" + shiftsno;
                            //lblWarehouse.Text = ds.Tables[0].Rows[0][6].ToString();
                            //lblLocation.Text = ds.Tables[0].Rows[0][7].ToString();
                            lblWarehouse.Text = "-";
                            lblLocation.Text = "FMB-R";
                            batchno = String.Format("{0:00000}", Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()));
                        }
                        else
                        {
                            lblPartcode.Text = "";
                            txtPCS.Text = "";
                            txtBOX.Text = "";
                            txtCAR.Text = "";
                            txtPAL.Text = "";
                            txtSHO.Text = "";
                            lblDateProduce.Text = "";
                            lblLOT.Text = "";
                            lblWarehouse.Text = "";
                            lblLocation.Text = "";
                        }
                    }
                    else
                    {
                        lblPartcode.Text = "";
                        txtPCS.Text = "";
                        txtBOX.Text = "";
                        txtCAR.Text = "";
                        txtPAL.Text = "";
                        txtSHO.Text = "";
                        lblDateProduce.Text = "";
                        lblLOT.Text = "";
                        lblWarehouse.Text = "";
                        lblLocation.Text = "";
                    }


                    if (txtPCS.Text != "0" && txtPCS != null)
                    {
                        txtPCS.Enabled = true;
                        txtPCS.ReadOnly = true;
                        txtPCS.BackColor = Color.PaleTurquoise;
                        rdbPCS.Enabled = true;
                    }
                    else
                    {
                        txtPCS.Enabled = false;
                        txtPCS.BackColor = Color.FromKnownColor(KnownColor.Control);
                        rdbPCS.Enabled = false;
                    }

                    if (txtBOX.Text != "0" && txtBOX != null)
                    {
                        txtBOX.Enabled = true;
                        txtBOX.ReadOnly = true;
                        txtBOX.BackColor = Color.PaleTurquoise;
                        rdbBOX.Enabled = true;
                    }
                    else
                    {
                        txtBOX.Enabled = false;
                        txtBOX.BackColor = Color.FromKnownColor(KnownColor.Control);
                        rdbBOX.Enabled = false;
                    }

                    if (txtCAR.Text != "0" && txtCAR != null)
                    {
                        txtCAR.Enabled = true;
                        txtCAR.ReadOnly = true;
                        txtCAR.BackColor = Color.PaleTurquoise;
                        rdbCAR.Enabled = true;
                    }
                    else
                    {
                        txtCAR.Enabled = false;
                        txtCAR.BackColor = Color.FromKnownColor(KnownColor.Control);
                        rdbCAR.Enabled = false;
                    }

                    if (txtPAL.Text != "0" && txtPAL != null)
                    {
                        txtPAL.Enabled = true;
                        txtPAL.ReadOnly = true;
                        txtPAL.BackColor = Color.PaleTurquoise;
                        rdbPAL.Enabled = true;
                    }
                    else
                    {
                        txtPAL.Enabled = false;
                        txtPAL.BackColor = Color.FromKnownColor(KnownColor.Control);
                        rdbPAL.Enabled = false;
                    }

                    if (txtSHO.Text != "0" && txtSHO != null)
                    {
                        txtSHO.Enabled = false;
                        txtSHO.ReadOnly = true;
                        txtSHO.BackColor = Color.PaleTurquoise;
                        rdbSHO.Enabled = false;
                    }
                    else
                    {
                        txtSHO.Enabled = false;
                        txtSHO.BackColor = Color.FromKnownColor(KnownColor.Control);
                        rdbSHO.Enabled = false;
                    }

                    btnPreview.Enabled = ((rdbBOX.Checked || rdbPCS.Checked || rdbCAR.Checked || rdbPAL.Checked) && cbbPIC.SelectedIndex > 0) ? true : false;

                    //rdbPCS.Enabled = (txtPCS.Text != "0" && txtPCS != null) ? true : false;
                    //rdbBOX.Enabled = (txtBOX.Text != "0" && txtBOX != null) ? true : false;
                    //rdbCAR.Enabled = (txtCAR.Text != "0" && txtCAR != null) ? true : false;
                    //rdbPAL.Enabled = (txtPAL.Text != "0" && txtPAL != null) ? true : false;
                    //rdbSHO.Enabled = (txtSHO.Text != "0" && txtSHO != null) ? true : false;

                }
                else
                {
                    prodId = "";
                    rdbPCS.Enabled = false;
                    rdbBOX.Enabled = false;
                    rdbCAR.Enabled = false;
                    rdbPAL.Enabled = false;
                    rdbSHO.Enabled = false;

                    lblPartcode.Text = "";
                    txtPCS.Text = "";
                    txtBOX.Text = "";
                    txtCAR.Text = "";
                    txtPAL.Text = "";
                    txtSHO.Text = "";

                    txtPCS.BackColor = Color.FromKnownColor(KnownColor.Control);
                    txtBOX.BackColor = Color.FromKnownColor(KnownColor.Control);
                    txtCAR.BackColor = Color.FromKnownColor(KnownColor.Control);
                    txtPAL.BackColor = Color.FromKnownColor(KnownColor.Control);
                    txtSHO.BackColor = Color.FromKnownColor(KnownColor.Control);

                    lblDateProduce.Text = "";
                    lblLOT.Text = "";
                    lblWarehouse.Text = "";
                    lblLocation.Text = "";

                    btnPreview.Enabled = false;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnWorkdate()
        {
            try
            {
                DateTime nNow = DateTime.Now;
                sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");

                if (DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00"))
                {
                    sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0).AddDays(-1);
                    sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                    shiftsnm = "Night";
                    shiftsno = "2";
                }
                else if (nNow.TimeOfDay >= TimeSpan.Parse("20:00:00"))
                {

                    sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0);
                    sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 00, 0).AddDays(1);
                    shiftsnm = "Night";
                    shiftsno = "2";
                }
                else
                {
                    sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                    sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0);
                    shiftsnm = "Day";
                    shiftsno = "1";
                }
                // sTime1 = sTime1.AddDays(-2);
                workdate = sTime1.ToString("yyyyMMdd");
                workdate_vn = sTime1.ToString("dd/MM/yyyy");
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
