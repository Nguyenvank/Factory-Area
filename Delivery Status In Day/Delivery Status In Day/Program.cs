using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery_Status_In_Day
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
            //Application.Run(new frmDeliveryStatus_v2());
            //Application.Run(new frmDeliveryStatus_v3o1());
            Application.Run(new frmDeliveryStatus_v3o2_AutoRefresh());
        }
    }
}
