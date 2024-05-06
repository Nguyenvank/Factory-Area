using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmPM_02WorkApprove_Details : Form
    {
        public string _woIDx = "";

        public frmPM_02WorkApprove_Details(string woIDx)
        {
            InitializeComponent();

            _woIDx = woIDx;
        }

        private void frmPM_02WorkApprove_Details_Load(object sender, EventArgs e)
        {

        }
    }
}
