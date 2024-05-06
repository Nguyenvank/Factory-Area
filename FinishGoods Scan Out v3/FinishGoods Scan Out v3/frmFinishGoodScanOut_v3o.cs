using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmFinishGoodScanOut_v3o : Form
    {
        public int dgvItemOrderWidth;
        public int dgvItemScanWidth;

        private System.Windows.Forms.Timer timerCountdown;
        private int counter = 8;

        public int msgFlag = 0;
        public string msgText;


        public frmFinishGoodScanOut_v3o()
        {
            InitializeComponent();
            
        }

        private void frmFinishGoodScanOut_v3o_Load(object sender, EventArgs e)
        {
            txtPacking.Focus();
            dgvItemOrderWidth = cls.fnGetDataGridWidth(dgvItemOrder);
            dgvItemScanWidth = cls.fnGetDataGridWidth(dgvItemScan);
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            fnLoadOrder();      // 5000 ms
            //fnLoadScanList();
            fnTotal();

        }

        public void init()
        {
            fnGetdate();
            fnLoadOrder();
            fnLoadScanList();
            fnTotal();
        }

        public void fnGetdate()
        {
            try
            {
                if (check.IsConnectedToInternet())
                {
                    tssDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                    tssDateTime.ForeColor = Color.Black;
                }
                else
                {
                    tssDateTime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
                    tssDateTime.ForeColor = Color.Red;
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadOrder()
        {
            try
            {

                string sql = "V2o1_BASE_Inventory_OutStock_SelOrder_SelItem_Addnew";
                DataTable dt = new DataTable();
                dt = cls.fnSelect(sql);

                dgvItemOrder.DataSource = dt;

                dgvItemOrderWidth = cls.fnGetDataGridWidth(dgvItemOrder);

                //dgvItemOrder.Columns[0].Width = 20 * dgvItemOrderWidth / 100;    // idx
                //dgvItemOrder.Columns[1].Width = 20 * dgvItemOrderWidth / 100;    // doID
                //dgvItemOrder.Columns[2].Width = 20 * dgvItemOrderWidth / 100;    // partID
                dgvItemOrder.Columns[3].Width = 20 * dgvItemOrderWidth / 100;    // partname
                dgvItemOrder.Columns[4].Width = 17 * dgvItemOrderWidth / 100;    // partcode
                dgvItemOrder.Columns[5].Width = 9 * dgvItemOrderWidth / 100;    // doQuantity
                dgvItemOrder.Columns[6].Width = 9 * dgvItemOrderWidth / 100;    // shipTo
                dgvItemOrder.Columns[7].Width = 10 * dgvItemOrderWidth / 100;    // location
                dgvItemOrder.Columns[8].Width = 10 * dgvItemOrderWidth / 100;    // Out Stock
                dgvItemOrder.Columns[9].Width = 10 * dgvItemOrderWidth / 100;    // Remain
                dgvItemOrder.Columns[10].Width = 10 * dgvItemOrderWidth / 100;    // DA Stock
                dgvItemOrder.Columns[11].Width = 5 * dgvItemOrderWidth / 100;    // doUnit

                dgvItemOrder.Columns[0].Visible = false;
                dgvItemOrder.Columns[1].Visible = false;
                dgvItemOrder.Columns[2].Visible = false;
                dgvItemOrder.Columns[3].Visible = true;
                dgvItemOrder.Columns[4].Visible = true;
                dgvItemOrder.Columns[5].Visible = true;
                dgvItemOrder.Columns[6].Visible = true;
                dgvItemOrder.Columns[7].Visible = true;
                dgvItemOrder.Columns[8].Visible = true;
                dgvItemOrder.Columns[9].Visible = true;
                dgvItemOrder.Columns[10].Visible = true;
                dgvItemOrder.Columns[11].Visible = true;

                cls.fnFormatDatagridview(dgvItemOrder, 13, 40);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadScanList()
        {
            string sql = "V2o1_BASE_Inventory_OutStock_SelOut_SelItem_Addnew";
            DataTable dt = new DataTable();
            dt = cls.ExecuteDataTable(sql);
            dgvItemScan.DataSource = dt;

            dgvItemScanWidth = cls.fnGetDataGridWidth(dgvItemScan);

            //dgvItemScan.Columns[0].Width = 20 * dgvItemScanWidth / 100;    // idx
            dgvItemScan.Columns[1].Width = 50 * dgvItemScanWidth / 100;    // packingCode
            //dgvItemScan.Columns[2].Width = 20 * dgvItemScanWidth / 100;    // partId
            //dgvItemScan.Columns[3].Width = 20 * dgvItemScanWidth / 100;    // partName
            dgvItemScan.Columns[4].Width = 25 * dgvItemScanWidth / 100;    // partCode
            dgvItemScan.Columns[5].Width = 10 * dgvItemScanWidth / 100;    // quantity
            dgvItemScan.Columns[6].Width = 15 * dgvItemScanWidth / 100;    // outDate

            dgvItemScan.Columns[0].Visible = false;
            dgvItemScan.Columns[1].Visible = true;
            dgvItemScan.Columns[2].Visible = false;
            dgvItemScan.Columns[3].Visible = false;
            dgvItemScan.Columns[4].Visible = true;
            dgvItemScan.Columns[5].Visible = true;
            dgvItemScan.Columns[6].Visible = true;

            dgvItemScan.Columns[6].DefaultCellStyle.Format = "HH:mm";

            cls.fnFormatDatagridview(dgvItemScan, 11, 40);
        }

        private void txtPacking_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string packing = "", packing01 = "", packing02 = "", packing03 = "";
                string partPlanQty = "", partPlanRemain = "", partScanQty = "";
                int _partPlanQty = 0, _partPlanRemain = 0, _partScanQty = 0;

                if (e.KeyCode == Keys.Enter)
                {
                    packing = txtPacking.Text.Trim();
                    if (packing != "" && packing != null)
                    {
                        packing01 = packing.Substring(0, 3);
                        packing02 = packing.Substring(4, 3);
                        packing03 = packing.Substring(8);

                        if(packing.Length>=24)
                        {
                            if (packing01 == "PRO")
                            {
                                if (packing02 == "PCS" || packing02 == "BOX" || packing02 == "CAR" || packing02 == "PAL" || packing02 == "SHO")
                                {
                                    if (packing03.Length >= 16)
                                    {
                                        string sqlChkQty = "V2o1_BASE_Inventory_OutStock_SelChkQty_SelItem_Addnew";
                                        SqlParameter[] sParamsChkQty = new SqlParameter[1]; // Parameter count
                                        sParamsChkQty[0] = new SqlParameter();
                                        sParamsChkQty[0].SqlDbType = SqlDbType.VarChar;
                                        sParamsChkQty[0].ParameterName = "@packing";
                                        sParamsChkQty[0].Value = packing;

                                        DataSet dsChkQty = new DataSet();
                                        dsChkQty = cls.ExecuteDataSet(sqlChkQty, sParamsChkQty);
                                        if (dsChkQty.Tables.Count > 0)
                                        {
                                            if (dsChkQty.Tables[0].Rows.Count > 0)
                                            {
                                                partPlanQty = dsChkQty.Tables[0].Rows[0][1].ToString();
                                                partPlanRemain = dsChkQty.Tables[0].Rows[0][2].ToString();
                                                partScanQty = dsChkQty.Tables[0].Rows[0][3].ToString();

                                                _partPlanQty = (partPlanQty != "" && partPlanQty != null) ? Convert.ToInt32(partPlanQty) : 0;
                                                _partPlanRemain = (partPlanRemain != "" && partPlanRemain != null) ? Convert.ToInt32(partPlanRemain) : 0;
                                                _partScanQty = (partScanQty != "" && partScanQty != null) ? Convert.ToInt32(partScanQty) : 0;

                                                if(_partScanQty <= _partPlanRemain)
                                                {
                                                    if(_partScanQty <= _partPlanQty)
                                                    {
                                                        //////////////////////////////////////////////////////////////////////////////
                                                        string sql = "V2o1_BASE_Inventory_OutStock_AddItem_Addnew";                 //
                                                        SqlParameter[] sParams = new SqlParameter[1]; // Parameter count            //
                                                        sParams[0] = new SqlParameter();                                            //
                                                        sParams[0].SqlDbType = SqlDbType.VarChar;                                   //
                                                        sParams[0].ParameterName = "@packing";                                      //
                                                        sParams[0].Value = packing;                                                 //
                                                                                                                                    //
                                                        cls.fnUpdDel(sql, sParams);                                                 //
                                                                                                                                    //
                                                        msgFlag = 1;                                                                //
                                                        msgText = "Out stock successful.";                                          //
                                                                                                                                    //
                                                        fnLoadOrder();                                                              //
                                                        fnLoadScanList();                                                           //
                                                        fnTotal();                                                                  //
                                                        //////////////////////////////////////////////////////////////////////////////
                                                    }
                                                    else
                                                    {
                                                        msgFlag = -1;
                                                        msgText = "Cannot scan out (" + _partScanQty + ") bigger than order (" + _partPlanQty + "), please re-check and try again.";
                                                    }
                                                }
                                                else
                                                {
                                                    msgFlag = -1;
                                                    msgText = "Cannot scan out (" + _partScanQty + ") bigger than remain (" + _partPlanRemain + "), please re-check and try again.";
                                                }
                                            }
                                            else
                                            {
                                                msgFlag = -1;
                                                msgText = "Packing (" + packing + ") cannot found on system, please re-check and try again.";
                                            }
                                        }
                                        else
                                        {
                                            msgFlag = -1;
                                            msgText = "Packing (" + packing + ") cannot found on system, please re-check and try again.";
                                        }
                                    }
                                    else
                                    {
                                        msgFlag = -1;
                                        msgText = "Packing number (" + packing03 + ") is invalid, please re-check and try again.";
                                    }
                                }
                                else
                                {
                                    msgFlag = -1;
                                    msgText = "Packing type (" + packing02 + ") is invalid, please re-check and try again.";
                                }
                            }
                            else
                            {
                                msgFlag = -1;
                                msgText = "Packing kind (" + packing01 + ") is invalid, please re-check and try again.";
                            }
                        }
                        else
                        {
                            msgFlag = -1;
                            msgText = "Packing (" + packing + ") is invalid, please re-check and try again.";
                        }
                    }
                    else
                    {
                        msgFlag = -1;
                        msgText = "Packing NULL is invalid, please re-check and try again.";
                    }

                    //timerCountdown = new System.Windows.Forms.Timer();
                    //timerCountdown.Tick += new EventHandler(timerCountdown_Tick);
                    //timerCountdown.Interval = 1000; // 1 second
                    //timerCountdown.Start();

                    txtPacking.Text = "";
                    txtPacking.Focus();
                    lblMessage.Text = msgText;
                    
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void timerCountdown_Tick(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                timerCountdown.Stop();
                timerCountdown.Dispose();
            }
            else
            {
                if (msgFlag != 0)
                {
                    lblMessage.Text = msgText;
                    switch (msgFlag)
                    {
                        case -1:
                            lblMessage.BackColor = Color.Red;
                            break;
                        case 1:
                            lblMessage.BackColor = Color.DodgerBlue;
                            break;
                    }
                    msgFlag = 0;
                    msgText = "";
                }
                else
                {
                    lblMessage.Text = "";
                    lblMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
            counter--;
        }

        public void fnTotal()
        {
            lblItemTotal.Text = String.Format("{0:n0}", fnTotalOut());
            lblOrderTotal.Text = String.Format("{0:n0}", fnTotalQty());
            lblScanTotal.Text = String.Format("{0:n0}", fnTotalRate()) + "%";
        }

        public int fnTotalQty()
        {
            int total = 0;
            string _total = "";

            string sql = "V2o1_BASE_Inventory_OutStock_SelTotalQty_SelItem_Addnew";
            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql);
            if (ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    _total = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    _total = "0";
                }
            }
            else
            {
                _total = "0";
            }

            total = (_total != "" && _total != null) ? Convert.ToInt32(_total) : 0;

            return total;
        }

        public int fnTotalOut()
        {
            int total = 0;
            string _total = "";

            string sql = "V2o1_BASE_Inventory_OutStock_SelTotalOut_SelItem_Addnew";
            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _total = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    _total = "0";
                }
            }
            else
            {
                _total = "0";
            }

            total = (_total != "" && _total != null) ? Convert.ToInt32(_total) : 0;

            return total;
        }

        public int fnTotalRate()
        {
            int total = 0;

            total = (int)(fnTotalOut() * 100) / fnTotalQty();

            return total;
        }
    }
}
