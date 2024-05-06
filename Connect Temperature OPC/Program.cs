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
            //Application.Run(new Form1());
            //Application.Run(new frmTempFromPLC_R());      // TEMPERATURE DATA
            //Application.Run(new frmTempFromPLC_R_V1o2());      // TEMPERATURE DATA (18 RUBBER MCs)
            //Application.Run(new frmTempFromPLC_R_V1o3());      // TEMPERATURE DATA (20 RUBBER MCs)
            Application.Run(new frmTempFromPLC_R_V1o4());      // TEMPERATURE DATA (Optimize code for huge PV over 9999 - 4 characters value)
        }
    }
}
