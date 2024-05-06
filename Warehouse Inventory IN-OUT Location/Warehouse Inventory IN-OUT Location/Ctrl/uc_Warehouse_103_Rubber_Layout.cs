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

namespace Warehouse_Inventory_IN_OUT_Location.Ctrl
{
    public partial class uc_Warehouse_103_Rubber_Layout : UserControl
    {
        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            __whs_idx = "103";


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

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public uc_Warehouse_103_Rubber_Layout()
        {
            InitializeComponent();
        }

        private void uc_Warehouse_103_Rubber_Layout_Load(object sender, EventArgs e)
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

        }

        private void Fnc_Load_Rack_Details(object sender, EventArgs e)
        {
            Label
                lbl_click = (Label)sender;

            string
                whs_idx = __whs_idx,
                rack_cd = lbl_click.Text.Trim().Replace("RACK ", "");

            Ctrl.uc_Warehouse_Racks_Locations_Layout_Full_Windows details = new Ctrl.uc_Warehouse_Racks_Locations_Layout_Full_Windows(whs_idx, rack_cd);
            details.ShowDialog();
        }

        /***********************************/
    }
}
