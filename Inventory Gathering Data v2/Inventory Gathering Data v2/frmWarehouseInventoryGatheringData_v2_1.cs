using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Inventory_Data;

namespace Inventory_Gathering_Data_v2
{
    public partial class frmWarehouseInventoryGatheringData_v2_1 : Form
    {
        public DateTime _dt, _startShift01, _endShift01, _startShift02, _endShift02;
        public string _shift;
        

        public frmWarehouseInventoryGatheringData_v2_1()
        {
            InitializeComponent();
        }

        private void frmWarehouseInventoryGatheringData_v2_1_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            DateTime dt = DateTime.Now;
            DateTime startShift01 = new DateTime(dt.Year, dt.Month, dt.Day, 8, 0, 0);
            DateTime endShift01 = new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 59);
            DateTime startShift02 = new DateTime(dt.Year, dt.Month, dt.Day, 20, 0, 0);
            DateTime endShift02 = new DateTime(dt.Year, dt.Month, dt.Day, 7, 59, 59).AddDays(1);
            string shift = cls.fnGetDate("S");

            _dt = dt;
            _startShift01 = startShift01;
            _endShift01 = endShift01;
            _startShift02 = startShift02;
            _endShift02 = endShift02;
            _shift = shift.ToUpper();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {

        }

        public void fnCheckEnableStatus(CheckBox chk, TextBox txt, DateTimePicker dtpIns, DateTimePicker dtpUpd, CheckBox chk60, CheckBox chk30)
        {
            if(chk.Checked)
            {
                txt.Enabled = true;
                dtpIns.Enabled = true;
                dtpUpd.Enabled = true;
                chk60.Enabled = true;
                chk30.Enabled = true;

                if (_shift == "DAY")
                {
                    dtpIns.Value = _startShift01;
                    if (chk60.Checked == false && chk30.Checked == false)
                    {
                        dtpUpd.Enabled = true;
                        dtpUpd.Value = _endShift01.AddMinutes(0);
                    }
                    else
                    {
                        dtpUpd.Enabled = false;
                    }
                }
                else
                {
                    dtpIns.Value = _startShift02;
                    if (chk60.Checked == false && chk30.Checked == false)
                    {
                        dtpUpd.Enabled = true;
                        dtpUpd.Value = _endShift02.AddMinutes(0);
                    }
                    else
                    {
                        dtpUpd.Enabled = false;
                    }
                }
            }
            else
            {
                txt.Enabled = false;
                dtpIns.Enabled = false;
                dtpUpd.Enabled = false;
                chk60.Enabled = false;
                chk30.Enabled = false;
            }
        }

        private void chkActive01_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive01, txtProc01, dtpIns01, dtpUpd01, chkHour01, chkMins01);
        }

        private void chkActive02_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive02, txtProc02, dtpIns02, dtpUpd02, chkHour02, chkMins02);
        }

        private void chkActive03_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive03, txtProc03, dtpIns03, dtpUpd03, chkHour03, chkMins03);
        }

        private void chkActive04_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive04, txtProc04, dtpIns04, dtpUpd04, chkHour04, chkMins04);
        }

        private void chkActive05_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive05, txtProc05, dtpIns05, dtpUpd05, chkHour05, chkMins05);
        }

        private void chkActive06_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive06, txtProc06, dtpIns06, dtpUpd06, chkHour06, chkMins06);
        }

        private void chkActive07_CheckedChanged(object sender, EventArgs e)
        {
            fnCheckEnableStatus(chkActive07, txtProc07, dtpIns07, dtpUpd07, chkHour07, chkMins07);
        }
    }
}
