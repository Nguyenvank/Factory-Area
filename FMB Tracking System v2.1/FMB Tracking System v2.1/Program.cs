using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FMB_Tracking_System_v2._1
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
            //Application.Run(new frmFMBScanBarcode_v2o3());
            //Application.Run(new frmFMBScanBarcode_v2o4());

            //Application.Run(new frmFMBScanBarcode_v2o5());      // version OK
            Application.Run(new frmFMBScanBarcode_v2o6());      // Append imported FMB scan barcode box
        }
    }
}
