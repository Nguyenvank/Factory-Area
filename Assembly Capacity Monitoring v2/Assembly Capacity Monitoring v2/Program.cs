using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Assembly_Capacity_Monitoring_v2
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

            Application.Run(new frm_Main());
            //Application.Run(new frmAssyProductionMonitoring_v3o0());
            //Application.Run(new frmAssyProductionMonitoring_v2o8());
            ////Application.Run(new frmAssyProductionMonitoring_v2o7());
            //Application.Run(new frmAssyProductionMonitoring_v2o6());
            //Application.Run(new frmAssyProductionMonitoring());
        }
    }
}
