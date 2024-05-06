using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Warehouse_Inventory_IN_OUT_Location
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
            //Application.Run(new frmWarehouseInventoryLocation_v1o0());
            //Application.Run(new frmWarehouseInventoryLocation_v1o1());
            //Application.Run(new frmWarehouseInventoryLocation_v1o2());
            Application.Run(new frmWarehouseInventoryLocation_v1o3());      // update new color theme for CKD+Resin
            //Application.Run(new frmWarehouseInventoryLocation_v2o0());
            //Application.Run(new frmWarehouseInventoryLocation_v2o1());
            ////Application.Run(new frm_Warehouse_Location_v4o0());
            //Application.Run(new Ctrl.uc_Warehouse_Racks_Locations_Layout());

            ///
            //Application.Run(new frmWarehouseInventoryLocation_v3o0());
            //Application.Run(new Form1());
        }
    }
}
