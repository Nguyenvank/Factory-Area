using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_1_Detail : UserControl
    {
        private static uc_Temperature_1_Detail _instance;
        public static uc_Temperature_1_Detail Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_1_Detail();
                return _instance;
            }
        }

        public uc_Temperature_1_Detail()
        {
            InitializeComponent();
        }
    }
}
