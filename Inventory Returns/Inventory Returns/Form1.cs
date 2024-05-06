using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Returns
{
    public partial class Form1 : Form
    {
        public string partID;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
            fnGetDate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
        }

        public void init()
        {
            dtpDateReturn.Value = DateTime.Now;
            lblReturnOK.Text = "OK: 0";
            lblReturnNG.Text = "NG: 0";
            txtReturnOK.Text = "0";
            txtReturnNG.Text = "0";

            dtpDateReturn.Enabled = false;
            txtReturnOK.Enabled = false;
            txtReturnNG.Enabled = false;
            rdbAdd.Enabled = false;
            rdbUpd.Enabled = false;
            rdbDel.Enabled = false;
            btnSave.Enabled = false;
            btnFinish.Enabled = false;

        }

        public void fnGetDate()
        {

        }

    }
}
