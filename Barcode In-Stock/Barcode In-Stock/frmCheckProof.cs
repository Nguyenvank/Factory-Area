using Inventory_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Barcode_In_Stock
{
    public partial class frmCheckProof : Form
    {
        public string _chkProdIDx = "", _chkSubcodeNo = "";
        public string _chkSubcode01 = "", _chkSubcode02 = "", _chkSubcode03 = "";
        public bool _chkFinal = true, _chkMaster = true, _chkSlave01 = true, _chkSlave02 = true, _chkSlave03 = true;
        public string _packCode = "", _packLocation = "", _packManufacturedDate = "", _packLOT = "", _packQuantity = "", _packPartname = "", _packPartnumber = "";

        private static int VALIDATION_DELAY = 500;
        private System.Threading.Timer timer = (System.Threading.Timer)null;

        public System.Windows.Forms.Timer timerClose = new System.Windows.Forms.Timer();

        public frmCheckProof(string prodIDx, string sPartname, string sPartnumber, string sPackingCode, string sPackingLoc, string sPackingProduce, string sPackingLOT, string sPackingQty)
        {
            InitializeComponent();

            _packPartname = sPartname;
            _packPartnumber = sPartnumber;
            _packCode = sPackingCode;
            _packLocation = sPackingLoc;
            _packManufacturedDate = sPackingProduce;
            _packLOT = sPackingLOT;
            _packQuantity = sPackingQty;

            init(prodIDx);
        }

        private void frmCheckProof_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(_chkSubcode01 + "\r\n" + _chkSubcode02 + "\r\n" + _chkSubcode03);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtSubcode.Focus();
        }

        private void txtSubcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender as TextBox).ContainsFocus)
                    return;
                DisposeTimer();
                timer = new System.Threading.Timer(new TimerCallback(TimerElapsed), (object)null, frmCheckProof.VALIDATION_DELAY, frmCheckProof.VALIDATION_DELAY);
            }
            catch
            {

            }
            finally
            {

            }
        }
        public void TimerElapsed(object obj)
        {
            try
            {
                CheckSyntaxAndReport();
                DisposeTimer();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void DisposeTimer()
        {
            try
            {
                if (timer == null)
                    return;
                timer.Dispose();
                timer = (System.Threading.Timer)null;
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                frmInventoryInStock frm = new frmInventoryInStock();
                frm.fnGetBackValue(_chkFinal, _packPartname, _packPartnumber, _packCode, _packLocation, _packManufacturedDate, _packLOT, _packQuantity);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void CheckSyntaxAndReport()
        {
            try
            {

                string chkCode = txtSubcode.Text.Trim().ToLower();
                string subCode01 = _chkSubcode01.ToLower();
                string subCode02 = _chkSubcode02.ToLower();
                string subCode03 = _chkSubcode03.ToLower();
                string subCodeNo = _chkSubcodeNo;

                int _subCodeNum = 0;
                _subCodeNum = (subCode01 != null && subCode01 != "") ? 1 : 0;
                _subCodeNum = (subCode02 != null && subCode02 != "") ? _subCodeNum + 1 : _subCodeNum;
                _subCodeNum = (subCode03 != null && subCode03 != "") ? _subCodeNum + 1 : _subCodeNum;
                int _subCodeNo = (subCodeNo != "" && subCodeNo != null) ? Convert.ToInt32(subCodeNo) : 0;

                bool chkFinal = true, chkMaster = true, chkSlave01 = true, chkSlave02 = true, chkSlave03 = true;

                chkSlave01 = (chkCode.Contains(subCode01)) ? true : false;
                chkSlave02 = (chkCode.Contains(subCode02)) ? true : false;
                chkSlave03 = (chkCode.Contains(subCode03)) ? true : false;

                chkMaster = (chkSlave01 == true && chkSlave02 == true && chkSlave03 == true) ? true : false;
                

                Invoke(new Action(() =>
                {
                    if (_subCodeNo < 4)
                    {

                        _subCodeNo = _subCodeNo + 1;
                        _chkSubcodeNo = _subCodeNo.ToString();

                        switch (_subCodeNo)
                        {
                            case 1:
                                lblStatus01.Text = (chkMaster == true) ? "OK" : "NG";
                                lblStatus01.BackColor = (chkMaster == true) ? Color.DodgerBlue : Color.Red;
                                lblStatus01.ForeColor = Color.White;
                                break;
                            case 2:
                                lblStatus02.Text = (chkMaster == true) ? "OK" : "NG";
                                lblStatus02.BackColor = (chkMaster == true) ? Color.DodgerBlue : Color.Red;
                                lblStatus02.ForeColor = Color.White;
                                break;
                            case 3:
                                lblStatus03.Text = (chkMaster == true) ? "OK" : "NG";
                                lblStatus03.BackColor = (chkMaster == true) ? Color.DodgerBlue : Color.Red;
                                lblStatus03.ForeColor = Color.White;

                                /////////////////////////////////////////////////////////////

                                txtSubcode.Enabled = false;
                                txtSubcode.BackColor = Color.Silver;
                                tableLayoutPanel3.BackColor = Color.Silver;

                                timerClose.Interval = 1000;
                                timerClose.Enabled = true;
                                timerClose.Tick += new System.EventHandler(this.timerClose_Tick);

                                timerClose.Start();

                                break;
                        }
                    }
                    //else
                    //{
                    //    //txtSubcode.Enabled = false;
                    //    //timerClose.Interval = 1500;
                    //    //timerClose.Enabled = true;
                    //    //timerClose.Tick += new System.EventHandler(this.timerClose_Tick);

                    //    //timerClose.Start();
                    //    //frmInventoryInStock frm = new frmInventoryInStock();
                    //    //frm.fnGetBackValue(_chkFinal);
                    //    //this.Close();
                    //}

                    if (_subCodeNo == 3)
                    {
                         
                    }

                    txtSubcode.Text = "";
                    txtSubcode.Focus();
                }));
            }
            catch
            {

            }
            finally
            {
                //if (Convert.ToInt32(_chkSubcodeNo) == 3)
                //{
                //    frmInventoryInStock frm = new frmInventoryInStock();
                //    frm.fnGetBackValue(_chkFinal);
                //    this.Close();
                //}
            }
        }

        public void init(string idx)
        {
            if (idx != "" && idx != null)
            {
                string prodIDx = "", prodName = "", prodCode = "";
                string subcode01 = "", subcode02 = "", subcode03 = "";
                int subcodeNo = 0;
                string sql = "BASE_Product_InStock_Dispenser_Definition_SelItem_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@prodID";
                sParams[0].Value = idx;

                DataSet ds = new DataSet();
                ds = clsData.ExecuteDataSet(sql, sParams);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    prodName = ds.Tables[0].Rows[0][2].ToString();
                    prodCode = ds.Tables[0].Rows[0][3].ToString();
                    subcode01 = ds.Tables[0].Rows[0][4].ToString();
                    subcode02 = ds.Tables[0].Rows[0][5].ToString();
                    subcode03 = ds.Tables[0].Rows[0][6].ToString();
                }
                else
                {
                    prodName = "";
                    prodCode = "";
                    subcode01 = "";
                    subcode02 = "";
                    subcode03 = "";
                }
                _chkProdIDx = idx;
                _chkSubcode01 = subcode01;
                _chkSubcode02 = subcode02;
                _chkSubcode03 = subcode03;
                subcodeNo = (subcode01 != "" && subcode01 != null) ? subcodeNo + 1 : subcodeNo;
                subcodeNo = (subcode02 != "" && subcode02 != null) ? subcodeNo + 1 : subcodeNo;
                subcodeNo = (subcode03 != "" && subcode03 != null) ? subcodeNo + 1 : subcodeNo;

                lblPartName.Text = prodName + "\r\n" + prodCode;
            }
            else
            {
                MessageBox.Show("Không nhận được mã hàng. Không thể nhập kho kiện hàng này");
                
            }

            txtSubcode.Focus();
        }

        private void txtSubcode_KeyDown(object sender, KeyEventArgs e)
        {
            //string codeText = txtSubcode.Text.Trim();
            //int codeLen = codeText.Length;

            //if (codeText != "" && codeLen > 0)
            //{
            //    MessageBox.Show("OK");
            //    txtSubcode.Text = "";
            //}
            //else
            //{
            //    MessageBox.Show("NG");
            //}
        }


    }
}
