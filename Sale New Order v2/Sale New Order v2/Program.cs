﻿using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sale_New_Order_v2
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
            Application.Run(new frmSaleNewOrder2());
        }
    }
}