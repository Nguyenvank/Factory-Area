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
    public partial class uc_Temperature_4_Square : UserControl
    {
        private static uc_Temperature_4_Square _instance;
        public static uc_Temperature_4_Square Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_4_Square();
                return _instance;
            }
        }

        public uc_Temperature_4_Square()
        {
            InitializeComponent();
        }
    }
}
