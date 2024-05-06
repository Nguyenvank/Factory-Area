using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Rei;

namespace Trimming
{

    public partial class Trimming : Form
    {
        // INI 파일 관련
        Ini ini = new Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        public Trimming()
        {
            InitializeComponent();
        }


        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;


       /* private void button1_Click(object sender, EventArgs e)
        {

            string filename = "";//(*.bmp, *.jpg)|*.bmp;*.jpg

            openFileDialog1.Filter = "excel Files(*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.Title = "excel 파일을 선택하세요.";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                fpSpread1.OpenExcel(filename, FarPoint.Excel.ExcelOpenFlags.DoNotRecalculateAfterLoad);
            }
        }
        */
    }
}
