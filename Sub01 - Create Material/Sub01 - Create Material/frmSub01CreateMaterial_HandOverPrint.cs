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
    public partial class frmSub01CreateMaterial_HandOverPrint : Form
    {
        public int _dgvPrint_List_Width;

        public frmSub01CreateMaterial_HandOverPrint(string hoIDx)
        {
            InitializeComponent();

            //label1.Text = "BIÊN BẢN\r\nBÀN GIAO VẬT TƯ";

            _dgvPrint_List_Width = cls.fnGetDataGridWidth(dgvPrint_List);

            init(hoIDx);

        }

        public void init(string hoIDx)
        {
            string hoCode = "", hoDate = "", hoNew = "", hoFinish = "", hoNote = "", hoTotal = "";
            string sqlHead = "V2o1_BASE_SubMaterial_Init_HandOver_Print_Head_SelItem_Addnew";
            SqlParameter[] sParamsHead = new SqlParameter[1]; // Parameter count

            sParamsHead[0] = new SqlParameter();
            sParamsHead[0].SqlDbType = SqlDbType.Int;
            sParamsHead[0].ParameterName = "@hoIDx";
            sParamsHead[0].Value = hoIDx;

            DataSet dsHead = new DataSet();
            dsHead = cls.ExecuteDataSet(sqlHead, sParamsHead);
            if (dsHead.Tables.Count > 0)
            {
                hoCode = dsHead.Tables[0].Rows[0][1].ToString();
                hoDate = dsHead.Tables[0].Rows[0][2].ToString();
                hoNew = dsHead.Tables[0].Rows[0][3].ToString();
                hoFinish = dsHead.Tables[0].Rows[0][4].ToString();
                hoNote = dsHead.Tables[0].Rows[0][5].ToString();
                hoTotal = dsHead.Tables[0].Rows[0][6].ToString();

                this.Text = hoCode + " Printing..";
                lblPrint_Code.Text = hoCode;
                lblPrint_Date.Text = hoDate;
                lblPrint_Note.Text = hoNote;
                lblPrint_Total.Text = hoTotal;

                //string sqlList = "V2o1_BASE_SubMaterial_Init_HandOver_Print_List_SelItem_Addnew";
                string sqlList = "V2o1_BASE_SubMaterial_Init_HandOver_Print_List_SelItem_V2o1_Addnew";
                
                SqlParameter[] sParamsList = new SqlParameter[1]; // Parameter count

                sParamsList[0] = new SqlParameter();
                sParamsList[0].SqlDbType = SqlDbType.Int;
                sParamsList[0].ParameterName = "@hoIDx";
                sParamsList[0].Value = hoIDx;

                DataTable dt = new DataTable();
                dt = cls.ExecuteDataTable(sqlList, sParamsList);

                _dgvPrint_List_Width = cls.fnGetDataGridWidth(dgvPrint_List);
                dgvPrint_List.DataSource = dt;

                dgvPrint_List.Columns[0].Width = 10 * _dgvPrint_List_Width / 100;    // matName
                dgvPrint_List.Columns[1].Width = 55 * _dgvPrint_List_Width / 100;    // matName
                dgvPrint_List.Columns[2].Width = 15 * _dgvPrint_List_Width / 100;    // matCode
                dgvPrint_List.Columns[3].Width = 10 * _dgvPrint_List_Width / 100;    // matQty
                dgvPrint_List.Columns[4].Width = 10 * _dgvPrint_List_Width / 100;    // matUnit

                dgvPrint_List.Columns[0].Visible = true;
                dgvPrint_List.Columns[1].Visible = true;
                dgvPrint_List.Columns[2].Visible = true;
                dgvPrint_List.Columns[3].Visible = true;
                dgvPrint_List.Columns[4].Visible = true;

                dgvPrint_List.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                cls.fnFormatDatagridviewWhite(dgvPrint_List, 10, 30);

                dgvPrint_List.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvPrint_List.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            }
            else
            {
                hoCode = "";
                hoDate = "";
                hoNew = "";
                hoFinish = "";
                hoNote = "";
                hoTotal = "";

                MessageBox.Show("Không tìm thấy mã HDO tương ứng.\r\nVui lòng thử lại.");
                this.Close();
            }
        }

        public void fnPrint()
        {

            PrinterSettings ps = new PrinterSettings();
            PrintDocument doc = new PrintDocument();
            Margins margins = new Margins(5, 45, 5, 5);

            ps.Copies = 1;
            ps.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
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

        private void label1_Click(object sender, EventArgs e)
        {
            fnPrint();
        }
    }
}
