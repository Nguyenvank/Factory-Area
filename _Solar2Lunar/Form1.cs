using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solar2Lunar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] arr =  vcal.convertSolar2Lunar(dateTimePicker1.Value.Day, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year, 7);
            textBox1.Text = new DateTime(arr[2], arr[1], arr[0]).ToString("dd/MM/yyyy");
        }
        clsSolar2Lunar vcal = new clsSolar2Lunar();
    }
}
