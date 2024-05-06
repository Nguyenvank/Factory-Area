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

            //Application.Run(new frmAssyDispenser_v1o0());      // ONLY DISPENSER LINES FUCNTION
            //Application.Run(new frmAssyDispenser_v1o1());      // ONLY DISPENSER LINES FUCNTION
            //Application.Run(new frmAssyDispenser_v1o2());      // ONLY DISPENSER LINES FUCNTION
            //Application.Run(new frmAssyDispenser_v1o3());      // ONLY DISPENSER LINES FUCNTION
            //Application.Run(new frmAssyDispenser_v1o4());      // ONLY DISPENSER LINES FUCNTION
            //Application.Run(new frmAssyDispenser_v1o5());       // Force double signal to PLC
            Application.Run(new frmAssyDispenser_v1o6());

        }
    }
}
