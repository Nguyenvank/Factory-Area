using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dbconnect;
using FarPoint.Win.Spread.CellType;
using Rei;

namespace ProductMonitoring
{
    public partial class Form1 : Form
    {

        private msServer sv = new msServer();
        private string factcd = "F1";
        private string factnm = "본사";
        private string shiftsno = "1";
        private string shiftsnm = "Night";
        private string workdate = "";
        private string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private DateTime sTime1 = new DateTime();
        private DateTime sTime2 = new DateTime();
        private string factory_name = "";
        private string db_ip = "";
        private string db_name = "";


        // INI 파일 관련
        Ini ini = new Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");




        public Form1()
        {
            InitializeComponent();

            factory_name = ini.GetIniValue("FACTORY", "NAME", "Production Performance Monitoring").Trim();
            factcd = ini.GetIniValue("FACTORY", "CODE", "F1").Trim();
            db_ip = ini.GetIniValue("DB", "IP", "210.219.199.8,5001").Trim();
            db_name = ini.GetIniValue("DB", "NAME", "VMERP").Trim();

            this.Text = factory_name;
            sv.SetDBInfo(db_ip, db_name );
            sv.Open();

            shiftsnm = "Night";
            fpSpread1.ActiveSheet.RowCount = 0;
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                string factString = comboBox1.Items[i].ToString();
                if (factString.Substring(factString.Length - 2, 2) == factcd)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            timer1_Tick(sender, e); 
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            DateTime nNow = DateTime.Now;
            sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
            
            button2.Text = sNow;//DateTime.Now.TimeOfDay.ToString();

            if (DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00"))
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day - 1, 18, 30, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else if (nNow.TimeOfDay >= TimeSpan.Parse("20:00:00"))
            {

                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day + 1, 8, 00, 0);
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
            button1.Text = sTime1.ToString("yyyy/MM/dd") + " " + shiftsnm ;

            get_data_list();
            Form1_Resize(sender, e);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int SpreadWidth = fpSpread1.Width;
            int i = 0;

            //fpSpread1.ActiveSheet.ColumnHeader.Width = 4 * SpreadWidth / 100;
              fpSpread1.ActiveSheet.Columns[i].Width = 9 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 12 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 10 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 5 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 5 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 5 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 6 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 5 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 5 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 6 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 7 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 15 * SpreadWidth / 100;
            i = i + 1; fpSpread1.ActiveSheet.Columns[i].Width = 5 * SpreadWidth / 100;

            int SpreadHeight = fpSpread1.Height -60;

            for (int iRow = 0; iRow < fpSpread1.ActiveSheet.RowCount ; iRow++)
            {
                if (SpreadHeight > 100) { fpSpread1.ActiveSheet.Rows.Get(iRow).Height = SpreadHeight / fpSpread1.ActiveSheet.RowCount; }
            }

            fpSpread1.VerticalScrollBar.Visible = false;
            fpSpread1.HorizontalScrollBar.Visible = false;


        }

        private void get_data_list()
        {

            string sql = "";
            string sProd = "0";
            int ImageCol = 0;
           
            FarPoint.Win.Spread.SheetView sSpread = fpSpread1.ActiveSheet;
            Image ImageOn = imageList1.Images[2];// Image.FromFile( Application.StartupPath+"\\on34.gif");
            Image ImageOff = imageList1.Images[3];// Image.FromFile( Application.StartupPath + "\\off34.gif");

            Color bColor = Color.Firebrick;


            // 이미지 셀 인스턴스를 생성한다.
            FarPoint.Win.Spread.CellType.ImageCellType ImageCell = new FarPoint.Win.Spread.CellType.ImageCellType();

            sql = "GET_PRODUCTMONITORING '" + factcd + "', '" + shiftsno + "','" + workdate + "'   ";
            sv._query = sql;
            if (sv.execQuery())
            {
                sSpread.RowCount = sv._ds.Tables[0].Rows.Count;
                ImageCol = sSpread.ColumnCount - 1;
                factnm = sv._ds.Tables[0].Rows[0]["FACTNM"].ToString();

                for (int iRow = 0; iRow < sv._ds.Tables[0].Rows.Count; iRow++)
                {

                    sProd = sv._ds.Tables[0].Rows[iRow]["onoff"].ToString();

                    if (sProd == "0")
                    {
                        sSpread.Cells[iRow, ImageCol].CellType = ImageCell;
                        ImageCell.Style = FarPoint.Win.RenderStyle.StretchAndScale       ;
                        sSpread.Cells[iRow, ImageCol].Value = ImageOn;
                        bColor = Color.White;

                    }
                    else
                    {
                        sSpread.Cells[iRow, ImageCol].CellType = ImageCell;
                        ImageCell.Style = FarPoint.Win.RenderStyle.StretchAndScale;
                        sSpread.Cells[iRow, ImageCol].Value = ImageOff;
                        bColor = Color.Firebrick;

                    }
                    sSpread.Cells[iRow, ImageCol].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    sSpread.Cells[iRow, ImageCol].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    sSpread.Cells[iRow, ImageCol].BackColor = bColor;
                    for (int iCol = 0; iCol < sSpread.ColumnCount-1; iCol++)
                    {
                        sSpread.Cells[iRow, iCol].Value = sv._ds.Tables[0].Rows[iRow][iCol].ToString();
                        sSpread.Cells[iRow, iCol].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        sSpread.Cells[iRow, iCol].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        sSpread.Cells[iRow, iCol].BackColor = bColor;

                    }
                }
            }
            else
            {
                sSpread.RowCount = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string factString = comboBox1.SelectedItem.ToString();
            factcd = factString.Substring(factString.Length - 2, 2);
            ini.SetIniValue("FACTORY", "CODE", factcd);
            timer1_Tick(sender, e);
        }
    }
}
