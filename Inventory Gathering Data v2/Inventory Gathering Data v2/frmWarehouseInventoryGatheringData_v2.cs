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
    public partial class frmWarehouseInventoryGatheringData_v2 : Form
    {

        public DateTime prevInvUpdate, prevMatUpdate;
        public DateTime currInvUpdate, currMatUpdate;
        public DateTime nextInvUpdate, nextMatUpdate;
        public DateTime dt;

        public frmWarehouseInventoryGatheringData_v2()
        {
            InitializeComponent();
        }

        private void frmWarehouseInventoryGatheringData_v2_Load(object sender, EventArgs e)
        {
            init();
            lstLog.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            fnGetdate();
            fnGetNextUpdate();
            fnUpdate();
        }

        public void init()
        {
            dt = DateTime.Now;
            fnGetdate();
            fnUpdate();
            fnGetNextUpdate();
            fnUpdate();
        }

        public void fnGetdate()
        {
            try
            {
                if(check.IsConnectedToLAN("192.168.1.1"))
                {
                    lblDate.Text = cls.fnGetDate("SD");
                    lblDate.ForeColor = Color.Black;
                    lblTime.Text = cls.fnGetDate("CT");
                    lblTime.ForeColor = Color.Black;

                    label1.BackColor = Color.DodgerBlue;
                }
                else
                {
                    lblDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    lblDate.ForeColor = Color.Red;
                    lblTime.Text = String.Format("{0:HH:mm:ss tt}", DateTime.Now);
                    lblTime.ForeColor = Color.Red;

                    label1.BackColor = Color.Firebrick;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        public void fnGetNextUpdate()
        {
            if (chkInv60.Checked)
            {
                prevInvUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextInvUpdate = prevInvUpdate.AddHours(1);
            }
            else if (chkInv30.Checked)
            {
                prevInvUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextInvUpdate = (dt.Minute <= 30) ? prevInvUpdate.AddMinutes(30) : prevInvUpdate.AddHours(1);
            }
            else
            {
                if (cls.fnGetDate("S") == "DAY")
                {
                    prevInvUpdate = dtpInvNight.Value.AddDays(-1);
                    nextInvUpdate = dtpInvDay.Value;
                }
                else
                {
                    prevInvUpdate = dtpInvDay.Value.AddDays(-1);
                    nextInvUpdate = dtpInvNight.Value;
                }
            }

            lblInvNextUpdate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", nextInvUpdate);


            if (chkMat60.Checked)
            {
                prevMatUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextMatUpdate = prevMatUpdate.AddHours(1);
            }
            else if (chkMat30.Checked)
            {
                prevMatUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextMatUpdate = (dt.Minute <= 30) ? prevMatUpdate.AddMinutes(30) : prevMatUpdate.AddHours(1);
            }
            else
            {
                if (cls.fnGetDate("S") == "DAY")
                {
                    prevMatUpdate = dtpMatNight.Value.AddDays(-1);
                    nextMatUpdate = dtpMatDay.Value;
                }
                else
                {
                    prevMatUpdate = dtpMatDay.Value.AddDays(-1);
                    nextMatUpdate = dtpMatNight.Value;
                }
            }

            lblMatNextUpdate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", nextMatUpdate);
        }

        private void frmWarehouseInventoryGatheringData_v2_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
            }
        }

        private void chkInvActive_CheckedChanged(object sender, EventArgs e)
        {
            if(chkInvActive.Checked)
            {
                dtpInvInsert.Enabled = true;
                chkInv60.Enabled = true;
                chkInv30.Enabled = true;
                dtpInvDay.Enabled = (chkInv60.Checked || chkInv30.Checked) ? false : true;
                dtpInvNight.Enabled = (chkInv60.Checked || chkInv30.Checked) ? false : true;

                label14.Visible = true;
                lblInvNextUpdate.Visible = true;
            }
            else
            {
                dtpInvInsert.Enabled = false;
                chkInv60.Enabled = false;
                chkInv30.Enabled = false;
                dtpInvDay.Enabled = false;
                dtpInvNight.Enabled = false;

                label14.Visible = false;
                lblInvNextUpdate.Visible = false;
            }
        }

        private void chkMatActive_CheckedChanged(object sender, EventArgs e)
        {
            if(chkMatActive.Checked)
            {
                dtpMatInsert.Enabled = true;
                chkMat60.Enabled = true;
                chkMat30.Enabled = true;
                dtpMatDay.Enabled = (chkMat60.Checked || chkMat30.Checked) ? false : true;
                dtpMatNight.Enabled = (chkMat60.Checked || chkMat30.Checked) ? false : true;

                label15.Visible = true;
                lblMatNextUpdate.Visible = true;
            }
            else
            {
                dtpMatInsert.Enabled = false;
                chkMat60.Enabled = false;
                chkMat30.Enabled = false;
                dtpMatDay.Enabled = false;
                dtpMatNight.Enabled = false;

                label15.Visible = false;
                lblMatNextUpdate.Visible = false;
            }
        }

        private void chkInv30_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInv30.Checked)
            {
                chkInv60.Checked = false;

                dtpInvDay.Enabled = false;
                dtpInvNight.Enabled = false;

                prevInvUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextInvUpdate = (dt.Minute < 30) ? prevInvUpdate.AddMinutes(30) : prevInvUpdate.AddHours(1);
            }
            else
            {
                dtpInvDay.Enabled = true;
                dtpInvNight.Enabled = true;

                if (cls.fnGetDate("S") == "DAY")
                {
                    prevInvUpdate = dtpInvNight.Value.AddDays(-1);
                    nextInvUpdate = dtpInvDay.Value;
                }
                else
                {
                    prevInvUpdate = dtpInvDay.Value.AddDays(-1);
                    nextInvUpdate = dtpInvNight.Value;
                }
            }
            lblInvNextUpdate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", nextInvUpdate);
        }

        private void chkInv60_CheckedChanged(object sender, EventArgs e)
        {
            if(chkInv60.Checked)
            {
                chkInv30.Checked = false;

                dtpInvDay.Enabled = false;
                dtpInvNight.Enabled = false;

                prevInvUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextInvUpdate = prevInvUpdate.AddHours(1);
            }
            else
            {
                dtpInvDay.Enabled = true;
                dtpInvNight.Enabled = true;

                if (cls.fnGetDate("S") == "DAY")
                {
                    prevInvUpdate = dtpInvNight.Value.AddDays(-1);
                    nextInvUpdate = dtpInvDay.Value;
                }
                else
                {
                    prevInvUpdate = dtpInvDay.Value.AddDays(-1);
                    nextInvUpdate = dtpInvNight.Value;
                }
            }
            lblInvNextUpdate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", nextInvUpdate);
        }

        private void chkMat30_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMat30.Checked)
            {
                chkMat60.Checked = false;

                dtpMatDay.Enabled = false;
                dtpMatNight.Enabled = false;

                prevMatUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextMatUpdate = (dt.Minute < 30) ? prevMatUpdate.AddMinutes(30) : prevMatUpdate.AddHours(1);
            }
            else
            {
                dtpMatDay.Enabled = true;
                dtpMatNight.Enabled = true;

                if (cls.fnGetDate("S") == "DAY")
                {
                    prevMatUpdate = dtpMatNight.Value.AddDays(-1);
                    nextMatUpdate = dtpMatDay.Value;
                }
                else
                {
                    prevMatUpdate = dtpMatDay.Value.AddDays(-1);
                    nextMatUpdate = dtpMatNight.Value;
                }
            }
            lblMatNextUpdate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", nextMatUpdate);
        }

        private void chkMat60_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMat60.Checked)
            {
                chkMat30.Checked = false;

                dtpMatDay.Enabled = false;
                dtpMatNight.Enabled = false;

                prevMatUpdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                nextMatUpdate = prevMatUpdate.AddHours(1);
            }
            else
            {
                dtpMatDay.Enabled = true;
                dtpMatNight.Enabled = true;

                if (cls.fnGetDate("S") == "DAY")
                {
                    prevMatUpdate = dtpMatNight.Value.AddDays(-1);
                    nextMatUpdate = dtpMatDay.Value;
                }
                else
                {
                    prevMatUpdate = dtpMatDay.Value.AddDays(-1);
                    nextMatUpdate = dtpMatNight.Value;
                }
            }
            lblMatNextUpdate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", nextMatUpdate);
        }

        public void fnUpdate()
        {
            //lblTestTime.Text = String.Format("{0:HH:mm:ss}", point);
            //lblTestTime.Text = (DateTime.Compare(dt, nextMatUpdate)).ToString();
            

            DateTime invIns = dtpInvInsert.Value;
            DateTime dtInvIns = new DateTime(dt.Year, dt.Month, dt.Day, invIns.Hour, invIns.Minute, 0);
            DateTime invUpd = nextInvUpdate;
            DateTime dtInvUpd = new DateTime(dt.Year, dt.Month, dt.Day, invUpd.Hour, invUpd.Minute, 0);

            DateTime matIns = dtpMatInsert.Value;
            DateTime dtMatIns = new DateTime(dt.Year, dt.Month, dt.Day, matIns.Hour, matIns.Minute, 0);
            DateTime matUpd = nextMatUpdate;
            DateTime dtMatUpd = new DateTime(dt.Year, dt.Month, dt.Day, matUpd.Hour, matUpd.Minute, 0);

            if (chkInvActive.Checked)
            {
                if (dt.TimeOfDay == dtInvIns.TimeOfDay || dt.TimeOfDay == dtInvUpd.TimeOfDay)
                {
                    string sqlInv = "V2o1_BASE_Inventory_EndofDate_Production_Addnew";
                    DataSet dsInv = new DataSet();
                    dsInv = cls.ExecuteDataSet(sqlInv);
                    //lblMessage.Text = "Inventory updated successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
                    if (dt.TimeOfDay == dtInvIns.TimeOfDay)
                    {
                        lblMessage.Text = "Production inventory inserted successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt);
                    }
                    if (dt.TimeOfDay == dtInvUpd.TimeOfDay)
                    {
                        lblMessage.Text = "Production inventory updated successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", dt);
                    }
                    lstLog.Items.Add(lblMessage.Text);
                }
            }

            lblTestTime.Text = dtMatUpd.ToString();
            //lblTestTime.Text = String.Format("{0:HH:mm:ss}", nextMatUpdate) + "\r\n" + String.Format("{0:HH:mm:ss}", dt) + "\r\n" + DateTime.Compare(dt,nextMatUpdate);
            if (chkMatActive.Checked)
            {
                //if (point.TimeOfDay == dtMatIns.TimeOfDay || point.TimeOfDay == dtMatUpd.TimeOfDay)
                //{
                //    string sqlMat = "V2o1_BASE_Inventory_EndofDate_Material_Addnew";
                //    DataSet dsMat = new DataSet();
                //    dsMat = cls.ExecuteDataSet(sqlMat);
                //    cls.fnUpdDel(sqlMat);
                //    if (point.TimeOfDay == dtMatIns.TimeOfDay)
                //    {
                //        lblMessage.Text = "Material inventory inserted successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
                //    }
                //    if (point.TimeOfDay == dtMatUpd.TimeOfDay)
                //    {
                //        lblMessage.Text = "Material inventory updated successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
                //    }
                //    lstLog.Items.Add(lblMessage.Text);
                //}
                if (DateTime.Compare(dt,dtMatUpd)==0)
                {
                    MessageBox.Show("OK");
                }
            }
        }
    }
}
