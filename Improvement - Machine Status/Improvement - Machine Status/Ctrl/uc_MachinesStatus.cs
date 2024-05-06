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
    public partial class uc_MachinesStatus : UserControl
    {

        int _rubber = 20, _plastic = 12, _assemby = 12;

        public uc_MachinesStatus()
        {
            InitializeComponent();

            //ImageList imageList = new ImageList();
            //imageList.ImageSize = new Size(40, 48);
            //imageList.ColorDepth = ColorDepth.Depth32Bit;
            //Image image = new Bitmap(Properties.Resources.monitor_big);
            //imageList.Images.Add(image);

            //lst_Machines.View = View.LargeIcon;
            //lst_Machines.LargeImageList = imageList;

            ListViewGroup Rubber = new ListViewGroup("RUBBER MACHINES");
            ListViewGroup Plastic = new ListViewGroup("PLASTIC MACHINES");
            ListViewGroup Assembly = new ListViewGroup("ASSEMBLY MACHINES");

            lst_Machines.Groups.Add(Rubber);
            lst_Machines.Groups.Add(Plastic);
            lst_Machines.Groups.Add(Assembly);
        }

        private void uc_MachinesStatus_Load(object sender, EventArgs e)
        {
            

        }
    }
}
