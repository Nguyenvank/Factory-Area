using System;
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
    public partial class frmSub04ScanOutMaterial_PrintBill : Form
    {
        public string _orderIDx = "";
        public int _dgv_Print_ScanOut_List_Width;

        public frmSub04ScanOutMaterial_PrintBill(string orderIDx)
        {
            InitializeComponent();
            _orderIDx = orderIDx;

            pnl_Print_Logo.BackgroundImage = Properties.Resources.logo_small_2;
            pnl_Print_Logo.BackgroundImageLayout = ImageLayout.Center;

            ContextMenu mnuPanel = new ContextMenu();
            mnuPanel.MenuItems.Add(new MenuItem("In phiếu", fn_Print_Bill_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Đóng", fn_Print_Close_Click));
            panel1.ContextMenu = mnuPanel;
        }

        private void frmSub04ScanOutMaterial_PrintBill_Load(object sender, EventArgs e)
        {
            string orderIDx = _orderIDx;

            string rsbmCode = "", rsbmDate = "", rsbmReceiveIDx = "", rsbmReceiver = "", rsbmPurpose = "", rsbmPriority = "", rsbmReason = "", rsbmAdd = "";
            string mmtApprove = "", mmtApproveDate = "", mmtResponse = "", mmtDateHandOver = "", scanoutDate = "", rsbmFinish = "";

            string sqlDetail = "";
            string sql = "V2o1_BASE_SubMaterial04_Init_HandOver_Bind_List_Print_Bill_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@orderIDx";
            sParams[0].Value = orderIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables[0].Rows.Count > 0)
            {
                rsbmCode = ds.Tables[0].Rows[0][1].ToString();
                rsbmDate = ds.Tables[0].Rows[0][2].ToString();
                //rsbmReceiveIDx = ds.Tables[0].Rows[0][3].ToString();
                rsbmReceiver = ds.Tables[0].Rows[0][4].ToString();
                rsbmPurpose = ds.Tables[0].Rows[0][5].ToString();
                rsbmPriority = ds.Tables[0].Rows[0][6].ToString();
                rsbmReason = ds.Tables[0].Rows[0][7].ToString();
                //rsbmAdd = ds.Tables[0].Rows[0][8].ToString();
                //mmtApprove = ds.Tables[0].Rows[0][9].ToString();
                //mmtApproveDate = ds.Tables[0].Rows[0][10].ToString();
                //mmtResponse = ds.Tables[0].Rows[0][11].ToString();
                //mmtDateHandOver = ds.Tables[0].Rows[0][12].ToString();
                scanoutDate = ds.Tables[0].Rows[0][13].ToString();
                //rsbmFinish = ds.Tables[0].Rows[0][14].ToString();

                string purposeText = "";
                switch (rsbmPurpose.ToLower())
                {
                    case "repair":
                        purposeText = "Sửa chữa";
                        break;
                    case "maintain":
                        purposeText = "Bảo dưỡng";
                        break;
                    case "improve":
                        purposeText = "Cải tiến";
                        break;
                    case "daily":
                        purposeText = "Hàng ngày";
                        break;
                }
                string priorityText = "";
                switch (rsbmPriority)
                {
                    case "1":
                        priorityText = "Mức cao";
                        break;
                    case "2":
                        priorityText = "Mức TB";
                        break;
                    case "3":
                        priorityText = "Mức thường";
                        break;
                }

                lbl_Print_Order_Code.Text = rsbmCode;
                lbl_Print_Scan_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Convert.ToDateTime(scanoutDate));
                lbl_Print_Header_Priority.Text = priorityText;
                lbl_Print_Header_Purpose.Text = purposeText;
                lbl_Print_Purpose.Text = purposeText;
                lbl_Print_Maker_Name.Text = rsbmReceiver;
                lbl_Print_Order_Create_Date.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(rsbmDate));
                lbl_Print_Order_Priority.Text = priorityText;
                lbl_Print_Order_Reason.Text = rsbmReason;

                sqlDetail = "V2o1_BASE_SubMaterial04_Init_HandOver_Bind_List_Print_Detail_SelItem_Addnew";

                SqlParameter[] sParamsDetail = new SqlParameter[1]; // Parameter count

                sParamsDetail[0] = new SqlParameter();
                sParamsDetail[0].SqlDbType = SqlDbType.Int;
                sParamsDetail[0].ParameterName = "@orderIDx";
                sParamsDetail[0].Value = orderIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sqlDetail, sParamsDetail);

                _dgv_Print_ScanOut_List_Width = cls.fnGetDataGridWidth(dgv_Print_ScanOut_List);
                dgv_Print_ScanOut_List.DataSource = dt;

                //dgv_Print_ScanOut_List.Columns[0].Width = 20 * _dgv_Print_ScanOut_List_Width / 100;    // [idx]
                //dgv_Print_ScanOut_List.Columns[1].Width = 20 * _dgv_Print_ScanOut_List_Width / 100;    // [matIDx]
                dgv_Print_ScanOut_List.Columns[2].Width = 25 * _dgv_Print_ScanOut_List_Width / 100;    // [matName]
                dgv_Print_ScanOut_List.Columns[3].Width = 20 * _dgv_Print_ScanOut_List_Width / 100;    // [matCode]
                dgv_Print_ScanOut_List.Columns[4].Width = 30 * _dgv_Print_ScanOut_List_Width / 100;    // [packingCode]
                dgv_Print_ScanOut_List.Columns[5].Width = 7 * _dgv_Print_ScanOut_List_Width / 100;    // [matUnit]
                dgv_Print_ScanOut_List.Columns[6].Width = 9 * _dgv_Print_ScanOut_List_Width / 100;    // [matQty]
                dgv_Print_ScanOut_List.Columns[7].Width = 9 * _dgv_Print_ScanOut_List_Width / 100;    // [matQty]

                dgv_Print_ScanOut_List.Columns[0].Visible = false;
                dgv_Print_ScanOut_List.Columns[1].Visible = false;
                dgv_Print_ScanOut_List.Columns[2].Visible = true;
                dgv_Print_ScanOut_List.Columns[3].Visible = true;
                dgv_Print_ScanOut_List.Columns[4].Visible = true;
                dgv_Print_ScanOut_List.Columns[5].Visible = true;
                dgv_Print_ScanOut_List.Columns[6].Visible = true;
                dgv_Print_ScanOut_List.Columns[7].Visible = true;

                cls.fnFormatDatagridview(dgv_Print_ScanOut_List, 11, 40);
            }
        }

        private void fn_Print_Bill_Click(object sender, EventArgs e)
        {
            fnPrint();
        }

        private void fn_Print_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void fnPrint()
        {

            PrinterSettings ps = new PrinterSettings();
            PrintDocument doc = new PrintDocument();
            Margins margins = new Margins(5, 45, 15, 5);

            ps.Copies = 1;
            ps.DefaultPageSettings.Margins = new Margins(5, 5, 15, 5);
            doc.PrinterSettings = ps;

            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A6 size
            doc.DefaultPageSettings.PaperSize = sizeA4;
            doc.DefaultPageSettings.Landscape = false;
            doc.DefaultPageSettings.Margins = margins;

            doc.PrintPage += this.Doc_PrintPage;

            doc.Print();

            this.Close();

        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(this.tableLayoutPanel1.Width, this.tableLayoutPanel1.Height);
                this.tableLayoutPanel1.DrawToBitmap(bmp, new Rectangle(0, 0, this.tableLayoutPanel1.Width, this.tableLayoutPanel1.Height));
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

        private void FrmSub04ScanOutMaterial_PrintBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn?", cls.appName(), MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }
    }
}
