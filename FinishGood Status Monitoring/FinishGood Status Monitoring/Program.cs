using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinishGood_Status_Monitoring
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
            //Application.Run(new frmFinishGoodInventoryStatus());
            Application.Run(new frmFinishGoodMonitoring_v2o0());
            //Application.Run(new frmFinishGoodStatus_v2o1());
            
        }
    }
}
