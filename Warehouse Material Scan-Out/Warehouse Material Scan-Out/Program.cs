using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmWarehouseMaterialScanOut());
            //Application.Run(new frmWarehouseMaterialScanOut_v1o1());    // Warning form
            Application.Run(new frmWarehouseMaterialScanOut_v1o2());    // Block system
            //Application.Run(new frm_Main());
        }
    }
}
