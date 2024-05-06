using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Google.API.Translate;

namespace Inventory_Data.Ctrl
{
    public partial class UC_TestControls : UserControl
    {
        private static UC_TestControls _instance;
        public static UC_TestControls Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UC_TestControls();
                return _instance;
            }
        }

        public UC_TestControls()
        {
            InitializeComponent();
        }

        private void UC_TestControls_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            fnc_Google_Translate();
        }

        public void fnc_Google_Translate()
        {
            string txt = "";
        }
    }
}
