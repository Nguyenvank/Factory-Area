using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace plc_sachul
{
    class ZebraPage
    {
        public string partno = string.Empty;
        public string lotno = string.Empty;

        public ZebraPage(string p_partno, string p_lotno)
        {
            this.partno = p_partno;
            this.lotno = p_lotno;
        }
    }

    class ZebraPrinter
    {
        private PrintDocument pDoc = new PrintDocument();

        public List<ZebraPage> listZebraPages = new List<ZebraPage>();

        private int pageIndex = 0;

        //생성자
        public ZebraPrinter()
        {

        }

        public void Print()
        {
            PageSettings ps = new PageSettings();
            ps.PaperSize = new PaperSize("", 200, 100);

            pDoc.DefaultPageSettings = ps;

            pDoc.PrintPage += new PrintPageEventHandler(printDocument);

            //PrintPreviewDialog pd = new PrintPreviewDialog();
            //pd.Document = pDoc;
            //pd.Show();
            pDoc.Print();
        }

        void printDocument(object sender, PrintPageEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);

            //e.Graphics.DrawRectangle(pen, 15, 15, 380, 280); //테두리

            Font fontTitle = new Font("굴림", 18, FontStyle.Bold);
            Font fontContent = new Font("굴림", 12, FontStyle.Regular);
            Font fontPart = new Font("굴림", 14, FontStyle.Regular);
            Font fontBarcode = new Font("3 of 9 barcode", 18, FontStyle.Regular);
            Font fontLotNo = new Font("굴림", 9, FontStyle.Regular);

            string temp = "";

            e.Graphics.DrawString(listZebraPages[pageIndex].partno, fontPart, Brushes.Black, 30, 30);

            temp = "*" + listZebraPages[pageIndex].lotno + "*";
            e.Graphics.DrawString(temp, fontBarcode, Brushes.Black, 3, 50);
            e.Graphics.DrawString(temp, fontBarcode, Brushes.Black, 3, 60);
            e.Graphics.DrawString(listZebraPages[pageIndex].lotno, fontLotNo, Brushes.Black, 50, 80);

            e.HasMorePages = (pageIndex++ < listZebraPages.Count - 1);
        }
    }
}
