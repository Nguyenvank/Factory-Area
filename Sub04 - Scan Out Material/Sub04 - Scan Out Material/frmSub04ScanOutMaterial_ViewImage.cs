using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmSub04ScanOutMaterial_ViewImage : Form
    {
        MemoryStream _ms;

        public frmSub04ScanOutMaterial_ViewImage(MemoryStream ms)
        {
            InitializeComponent();
            _ms = ms;
        }

        private void frmSub04ScanOutMaterial_ViewImage_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = false;

            Bitmap bmp = new Bitmap(_ms);

            //int imgW = (int)bmp.Width;
            //int imgH = (int)bmp.Height;

            //MessageBox.Show(imgW + "x" + imgH);

            panel1.BackgroundImage = bmp;
            panel1.BackgroundImageLayout = ImageLayout.Center;
            toolTip1.SetToolTip(panel1, "Click here to close image.");

            _ms = null;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
