using Inventory_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fan_1
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
            //Application.Run(new frmRS485Communication());
            //Application.Run(new frmAssyFromPLC());
            //Application.Run(new frmRS485CommunicationGraph());
            //Application.Run(new frmTemperatureDatabase());
            //Application.Run(new frmTemperatureNumbers());

            Application.Run(new frmTemperatureSeperate());
            //Application.Run(new frmTemperatureDetail());

            //Application.Run(new frmTemperatureByDate_v2());

            //Application.Run(new frmTemperatureScreenUpdate());

            //Application.Run(new Form1());
        }
    }
}
