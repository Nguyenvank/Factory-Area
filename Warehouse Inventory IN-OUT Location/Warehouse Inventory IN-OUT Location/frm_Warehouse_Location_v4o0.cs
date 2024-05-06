using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventory_Data;

namespace Warehouse_Inventory_IN_OUT_Location
{
    public partial class frm_Warehouse_Location_v4o0 : Form
    {
        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_id = "",
            __whs_nm = "";

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public frm_Warehouse_Location_v4o0()
        {
            InitializeComponent();

            Fnc_Set_Config_Warehouse();

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(pnl_main, true);
        }

        private void frm_Warehouse_Location_v4o0_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /***********************************/

        public void Fnc_Load_Controls()
        {
            Fnc_Load_View_Warehouse();
        }

        public void Fnc_Set_Config_Warehouse()
        {
            __whs_id = ini.GetIniValue("WHS", "ID", "101");
            __whs_nm = ini.GetIniValue("WHS", "NM", "RESIN");
        }

        public void Fnc_Load_View_Warehouse()
        {
            string
                whs_id = __whs_id,
                whs_nm = __whs_nm,
                title = "";

            //__whs_id = whs_id;

            int _whs_id = (__whs_id.Length > 0) ? Convert.ToInt32(__whs_id) : 101;

            switch (_whs_id)
            {
                case 101:
                    cls.showUC(new Ctrl.uc_Warehouse_101_Resin_Layout(), pnl_main);
                    break;
                case 102:
                    cls.showUC(new Ctrl.uc_Warehouse_102_CKD_Layout(), pnl_main);
                    break;
                case 103:
                    cls.showUC(new Ctrl.uc_Warehouse_103_Rubber_Layout(), pnl_main);
                    break;
            }

            title = "Warehouse Location v4.0 - " + whs_nm.ToUpper() + " WAREHOUSE";
            this.Text = title;
        }

        /***********************************/

        private void resinWarehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WHS", "ID", "101");
            ini.SetIniValue("WHS", "NM", "RESIN");

            __whs_id = ini.GetIniValue("WHS", "ID", "101");
            __whs_nm = ini.GetIniValue("WHS", "NM", "RESIN");

            Fnc_Load_View_Warehouse();
        }

        private void cKDWarehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WHS", "ID", "102");
            ini.SetIniValue("WHS", "NM", "CKD");

            __whs_id = ini.GetIniValue("WHS", "ID", "102");
            __whs_nm = ini.GetIniValue("WHS", "NM", "CKD");

            Fnc_Load_View_Warehouse();
        }

        private void rubberWarehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WHS", "ID", "103");
            ini.SetIniValue("WHS", "NM", "RUBBER");

            __whs_id = ini.GetIniValue("WHS", "ID", "103");
            __whs_nm = ini.GetIniValue("WHS", "NM", "RUBBER");

            Fnc_Load_View_Warehouse();
        }

        private void openAutochangeRacksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
