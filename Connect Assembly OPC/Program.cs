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
            //Application.Run(new frmAssyFromPLC_v2o0());      // ASSEMBLY DATA

            //Application.Run(new frmAssyFromPLC_v2o1());      // APPEND CHECK NO BARCODE LABEL FUCNTION
            //Application.Run(new frmAssyFromPLC_v2o3());      // ONLY DISPENSER LINES FUCNTION
            Application.Run(new frm_Assy_From_PLC_v3o0());      // GET DATA DIRECTLY FROM PLC BY S7 FUNCTION

            //Application.Run(new frmAssyFromPLC_v2o2());      // APPEND CHECK NO BARCODE LABEL FUCNTION WITH APPEND VALVE NUMBERS
            //Application.Run(new frmAssyFromPLC());      // ASSEMBLY DATA
            //Application.Run(new frmTempFromPLC());      // TEMPERATURE DATA
        }
    }
}
