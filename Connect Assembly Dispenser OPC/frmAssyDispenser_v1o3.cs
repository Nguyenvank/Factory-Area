﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPCAutomation;  // dùng thư viện OPCautomation
using Inventory_Data;
using System.Data.SqlClient;
using System.IO;


namespace Fan_1
{
    public partial class frmAssyDispenser_v1o3 : Form
    {
        public int run = 1;
        public static int item = 0;

        DateTime _dt;

        bool _connect;

        int _itemCount = 0;
        int _last_Dis01_NG = 0, _last_Dis02_NG = 0;
        bool _alarm_change_shift = false;
        int _value01_NG = 0, _value02_NG = 0;

        int _pre_SCY_01 = 0, _pre_SCN_01 = 0, _pre_SCY_02 = 0, _pre_SCN_02 = 0, _pre_num_SCN_01 = 0, _pre_num_SCN_02 = 0;
        int _cur_SCY_01 = 0, _cur_SCN_01 = 0, _cur_SCY_02 = 0, _cur_SCN_02 = 0, _cur_num_SCN_01 = 0, _cur_num_SCN_02 = 0;
        string _flePath = "", _fleName = "", _fleValue = "";
        bool _export = false, _first01 = true, _first02 = true, _timerAct = true, _controlAct = true;
        System.Windows.Forms.Timer timerMsg;


        // tạo sever và kết nối. Copy đoạn này cũng được rồi hiểu dần.
        public OPCAutomation.OPCServer AnOPCServer;
        public OPCAutomation.OPCServer ConnectedOPCServer;
        public OPCAutomation.OPCGroups ConnectedServerGroup;
        public OPCAutomation.OPCGroup ConnectedGroup;

        public string Groupname;

        int ItemCount;
        Array OPCItemIDs = Array.CreateInstance(typeof(string), 100);
        Array ItemServerHandles = Array.CreateInstance(typeof(Int32), 100);
        Array ItemServerErrors = Array.CreateInstance(typeof(Int32), 100);
        Array ClientHandles = Array.CreateInstance(typeof(Int32), 100);
        Array RequestedDataTypes = Array.CreateInstance(typeof(Int16), 100);
        Array AccessPaths = Array.CreateInstance(typeof(string), 100);
        Array WriteItems = Array.CreateInstance(typeof(string), 100);

        public object OpcSystemsData { get; private set; }
        public object ConnectedOPCGroup { get; private set; }
        public object Simulater { get; private set; }
        public object TextBox3 { get; private set; }

        public frmAssyDispenser_v1o3()
        {
            InitializeComponent();

            //txtDI01OK.Text = "0";
            //txtDI01NG.Text = "0";
            //txtDI02OK.Text = "0";
            //txtDI02NG.Text = "0";

            //Fnc_Refresh_Dispenser_Data();

            this.FormClosing += Form1_FormClosing;

            cls.SetDoubleBuffer(tableLayoutPanel1, true);
        }

        private void frmAssyDispenser_v1o3_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;

            //string value = "";
            //string filename = "";

            timerMsg = new System.Windows.Forms.Timer();


            //_R_Connect = (chk_R_Connect.Checked) ? true : false;
            //_P_Connect = (chk_P_Connect.Checked) ? true : false;
            //_A_Connect = (chk_A_Connect.Checked) ? true : false;

            //_itemCount = 69;
            _itemCount = 4;    // Only Dispenser Lines
            //if (_R_Connect == true) { _itemCount = 69; }
            //if (_P_Connect == true) { _itemCount = 105; }
            //if (_A_Connect == true) { _itemCount = 123; }

            Fnc_Prepair_Logging_Name();
            fnConnect();
            txtDI01SCY.Text = txtDI02SCY.Text = "True";
            //fnTemperatureThread();

            //Fnc_Update_Valve_Number(4, 0);
            //Fnc_Update_Valve_Number(5, 0);
        }

        public void fnConnect()
        {
            try
            {
                string IOServer = "Kepware.KEPServerEX.V4";  // phiên bản opc kepwware cần kết nối.
                string IOGroup = "OPCGroup1";
                //Create a new OPC Server object
                ConnectedOPCServer = new OPCAutomation.OPCServer();      //
                                                                         //Attempt to connect with the server
                                                                         // ConnectedOPCServer.Connect(labelOPC.Text);
                ConnectedOPCServer.Connect(IOServer, "");
                ConnectedGroup = ConnectedOPCServer.OPCGroups.Add(IOGroup);
                ConnectedGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(ObjOPCGroup_DataChange);
                ConnectedGroup.UpdateRate = 10;
                ConnectedGroup.IsSubscribed = ConnectedGroup.IsActive;


                //pictureBox1.Visible = true;   // hiển thị ảnh động cơ khi ấn nút connect
                // lấy giá trị Tag từ OPC lên sever để tạo giao diện
                //ItemCount = 21;
                ItemCount = _itemCount;

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser01_SCY", 1);
                ClientHandles.SetValue(1, 1);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser01_SCN", 2);
                ClientHandles.SetValue(2, 2);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser02_SCY", 3);
                ClientHandles.SetValue(3, 3);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser02_SCN", 4);
                ClientHandles.SetValue(4, 4);

                //OPCItemIDs.SetValue("Channel1.Device1.Dispenser01_VAL", 5);
                //ClientHandles.SetValue(5, 5);

                //OPCItemIDs.SetValue("Channel1.Device1.Dispenser02_VAL", 6);
                //ClientHandles.SetValue(6, 6);

                //'Set the desire active state for the Items " đây là đoạn kết nối mình cứ thế thêm vào thôi"
                ConnectedGroup.OPCItems.DefaultIsActive = true;
                ConnectedGroup.OPCItems.AddItems(ItemCount, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
                chkConnect.Checked = true;

                lblStatus.Text = "Connected";
                lblStatus.BackColor = Color.DodgerBlue;

                Fnc_Refresh_Dispenser_Data();

                Fnc_Update_Valve_Number(4, 0);
                Fnc_Update_Valve_Number(5, 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Code: " + ex.Source + "\r\nMessage: " + ex.Message);
            }
            finally
            {

            }
        }

        public void fnDisconnect()
        {
            ConnectedOPCServer.Disconnect();  // đây là disconnect sevr khi mình ấn nút Disconnect
                                              //pictureBox1.Visible = false;     // khi ấn disconnect thì bức ảnh động cơ sẽ k dc hiển thị
            lblStatus.Text = "OPC Disconnected";
            lblStatus.BackColor = Color.Tomato;

            //txtAB01OK.Text = "0";
            //txtAB01NG.Text = "0";
            //txtAB02OK.Text = "0";
            //txtAB02NG.Text = "0";
            //txtAB03OK.Text = "0";
            //txtAB03NG.Text = "0";
            ////txtDI01OK.Text = "0";
            ////txtDI01NG.Text = "0";
            ////txtDI02OK.Text = "0";
            ////txtDI02NG.Text = "0";
            //txtPU01OK.Text = "0";
            //txtPU01NG.Text = "0";
            //txtWB01OK.Text = "0";
            //txtWB02OK.Text = "0";
            //txtWB03OK.Text = "0";
            //txtBL01OK.Text = "0";
            //txtBL02OK.Text = "0";
            //txtMX01OK.Text = "0";
            //txtWD01OK.Text = "0";
            //txtWD02OK.Text = "0";

            txtDI01SCY.Text =
                txtDI01SCN.Text =
                txtDI02SCY.Text =
                txtDI02SCN.Text =
                lblDI01SCN_Num.Text =
                lblDI02SCN_Num.Text = "0";

            lblNGLog.Text = "";

            txtDI01SCY.ReadOnly =
                txtDI01SCN.ReadOnly =
                txtDI02SCY.ReadOnly =
                txtDI02SCN.ReadOnly = true;

        }

        private void ObjOPCGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 1) { txtDI01SCY.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 2) { txtDI01SCN.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 3) { txtDI02SCY.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 4) { txtDI02SCN.Text = ItemValues.GetValue(i).ToString(); }

                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 1) { txtAB01OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 2) { txtAB01NG.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 3) { txtAB02OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 4) { txtAB02NG.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 5) { txtAB03OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 6) { txtAB03NG.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 7) { txtBL01OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 8) { txtBL02OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 9) { txtPU01OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 10) { txtPU01NG.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 11) { txtWB01OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 12) { txtWB02OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 13) { txtWB03OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 14) { txtWD01OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 15) { txtWD02OK.Text = ItemValues.GetValue(i).ToString(); }
                ////if (Convert.ToInt32(ClientHandles.GetValue(i)) == 16) { txtDI01OK.Text = ItemValues.GetValue(i).ToString(); }
                ////if (Convert.ToInt32(ClientHandles.GetValue(i)) == 17) { txtDI01NG.Text = ItemValues.GetValue(i).ToString(); }
                ////if (Convert.ToInt32(ClientHandles.GetValue(i)) == 18) { txtDI02OK.Text = ItemValues.GetValue(i).ToString(); }
                ////if (Convert.ToInt32(ClientHandles.GetValue(i)) == 19) { txtDI02NG.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 20) { txtMX01OK.Text = ItemValues.GetValue(i).ToString(); }
                //if (Convert.ToInt32(ClientHandles.GetValue(i)) == 21) { txtWB04OK.Text = ItemValues.GetValue(i).ToString(); }

                switch (Convert.ToInt32(ClientHandles.GetValue(i)))
                {
                    case 1:
                        txtDI01SCY.Text = ItemValues.GetValue(i).ToString();
                        //txtDI01SCY.BackColor = (txtDI01SCY.Text.ToLower() == "true") ? Color.LightGreen : Color.FromKnownColor(KnownColor.Control);
                        break;
                    case 2:
                        txtDI01SCN.Text = ItemValues.GetValue(i).ToString();
                        //txtDI01SCN.BackColor = (txtDI01SCN.Text.ToLower() == "true") ? Color.LightPink : Color.FromKnownColor(KnownColor.Control);
                        if (_first01 == true && txtDI01SCN.Text.ToLower() == "true")
                        {
                            Fnc_Update_Valve_Number(4, 0);
                            _first01 = false;
                        }
                        break;
                    case 3:
                        txtDI02SCY.Text = ItemValues.GetValue(i).ToString();
                        //txtDI02SCY.BackColor = (txtDI02SCY.Text.ToLower() == "true") ? Color.LightGreen : Color.FromKnownColor(KnownColor.Control);
                        break;
                    case 4:
                        txtDI02SCN.Text = ItemValues.GetValue(i).ToString();
                        //txtDI02SCN.BackColor = (txtDI02SCN.Text.ToLower() == "true") ? Color.LightPink : Color.FromKnownColor(KnownColor.Control);
                        if (_first02 == true && txtDI02SCN.Text.ToLower() == "true")
                        {
                            Fnc_Update_Valve_Number(5, 0);
                            _first02 = false;
                        }
                        break;
                }
            }
        }

        private void chkConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConnect.Checked)
            {
                fnConnect();
                _connect = true;
            }
            else
            {
                fnDisconnect();
                _connect = false;
            }
            //chkOperate.Checked = (chkOperate.Checked) ? false : false;
        }

        public void fnWarningStart(string tagname)
        {
            if (tagname != "" && tagname != null)
            {
                try
                {
                    OPCItem newOPC = ConnectedGroup.OPCItems.AddItem(tagname, 1);
                    newOPC.Write(Convert.ToBoolean(1));
                }
                catch
                {
                    //MessageBox.Show("No Connect");
                }
            }
        }

        public void fnWarningStop(string tagname)
        {
            if (tagname != "" && tagname != null)
            {
                try
                {
                    OPCItem newOPC = ConnectedGroup.OPCItems.AddItem(tagname, 1);
                    newOPC.Write(Convert.ToBoolean(0));
                }
                catch
                {
                    //MessageBox.Show("No Connect");
                }
            }
        }

        public void fnReadCodeDone(string tagname)
        {
            if (tagname != "" && tagname != null)
            {
                try
                {
                    OPCItem newOPC = ConnectedGroup.OPCItems.AddItem(tagname, 1);
                    newOPC.Write(Convert.ToBoolean(1));
                }
                catch
                {
                    //MessageBox.Show("No Connect");
                }
            }
        }

        public void fnUpdateValveNumber(string tagname,int value)
        {
            if (tagname != "" && tagname != null)
            {
                try
                {
                    OPCItem newOPC;
                    switch (value)
                    {
                        case 0:
                            newOPC = ConnectedGroup.OPCItems.AddItem(tagname, 0);
                            newOPC.Write(Convert.ToBoolean(0));
                            break;
                        case 1:
                            newOPC = ConnectedGroup.OPCItems.AddItem(tagname, 1);
                            newOPC.Write(Convert.ToBoolean(1));
                            break;
                    }
                }
                catch
                {
                    //MessageBox.Show("No Connect");
                }
            }
        }

        private void txtDI01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //fnUpdateData("4", "Dispenser-1", "OK", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
                //Fnc_Read_Code(4);
                //Fnc_Set_Message("Line 01 read barcode", 1);
            }
            catch
            {

            }
            finally
            {

            }
            //if (run == 0)
            //{
            //}
            //else
            //{
            //}
            //lblMessage.ForeColor = Color.Blue;
            //lblMessage.Text = "Update Dispenser 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //fnUpdateData("5", "Dispenser-2", "OK", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
                //Fnc_Read_Code(5);
                //Fnc_Set_Message("Line 02 read barcode", 1);
            }
            catch
            {

            }
            finally
            {

            }
            //if (run == 0)
            //{
            //}
            //else
            //{
            //}
            //lblMessage.ForeColor = Color.Blue;
            //lblMessage.Text = "Update Dispenser 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI01NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string value_dis01_NG = txtDI01NG.Text.Trim();
                string value_dis01_NG = _value01_NG.ToString();
                int _value_dis01_NG = (value_dis01_NG != "" && value_dis01_NG != null) ? Convert.ToInt32(value_dis01_NG) : 0;
                if (_value_dis01_NG > _last_Dis01_NG)
                {
                    _alarm_change_shift = true;
                    Fnc_Alarm_Test(4);
                    //_last_Dis01_NG = (txtDI01NG.Text != "0") ? _value_dis01_NG : 0;
                    _last_Dis01_NG = _value_dis01_NG;

                    //fnUpdateData("4", "Dispenser-1", "NG", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
                    Fnc_Set_Message("Line 01 NG detected", 2);
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txtDI02NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string value_dis02_NG = txtDI02NG.Text.Trim();
                string value_dis02_NG = _value02_NG.ToString();
                int _value_dis02_NG = (value_dis02_NG != "" && value_dis02_NG != null) ? Convert.ToInt32(value_dis02_NG) : 0;
                if (_value_dis02_NG > _last_Dis02_NG)
                {
                    _alarm_change_shift = true;
                    Fnc_Alarm_Test(5);
                    //_last_Dis02_NG = (txtDI02NG.Text != "0") ? _value_dis02_NG : 0;
                    _last_Dis02_NG = _value_dis02_NG;

                    //fnUpdateData("5", "Dispenser-2", "NG", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
                    Fnc_Set_Message("Line 02 NG detected", 2);
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void TxtDI01SCY_TextChanged(object sender, EventArgs e)
        {
            _timerAct = false;
            _controlAct = (txtDI01SCY.Text.ToLower() == "true") ? true : false; /* TRUE: DISPLAY; FALSE: DISAPPEAR */

            txtDI01SCY.BackColor = txtDI01SCY.ForeColor = lblMessage.BackColor = (txtDI01SCY.Text.ToLower() == "true") ? Color.LightGreen : Color.FromKnownColor(KnownColor.Control);
            //txtDI02SCY.BackColor = txtDI02SCY.ForeColor = (txtDI02SCY.Text.ToLower() == "true") ? txtDI02SCY.BackColor : Color.FromKnownColor(KnownColor.Control);
            txtDI02SCY.BackColor = txtDI02SCY.ForeColor = Color.FromKnownColor(KnownColor.Control);
            lblMessage.Text = (txtDI01SCY.BackColor == Color.LightGreen) ? "Line 01 barcode detected" : "";
            //lblMessage.Text = (lblMessage.Text.Length > 0) ? lblMessage.Text + "\r\n" + "Line 01 barcode detected" : "Line 01 barcode detected";
            //Fnc_Set_Message("Line 01 barcode detected", 1);
        }

        private void TxtDI02SCY_TextChanged(object sender, EventArgs e)
        {
            _timerAct = false;
            _controlAct = (txtDI02SCY.Text.ToLower() == "true") ? true : false; /* TRUE: DISPLAY; FALSE: DISAPPEAR */

            //txtDI01SCY.BackColor = txtDI01SCY.ForeColor = (txtDI01SCY.Text.ToLower() == "true") ? txtDI01SCY.BackColor : Color.FromKnownColor(KnownColor.Control);
            txtDI01SCY.BackColor = txtDI01SCY.ForeColor = Color.FromKnownColor(KnownColor.Control);
            txtDI02SCY.BackColor = txtDI02SCY.ForeColor = lblMessage.BackColor = (txtDI02SCY.Text.ToLower() == "true") ? Color.LightGreen : Color.FromKnownColor(KnownColor.Control);
            lblMessage.Text = (txtDI02SCY.BackColor == Color.LightGreen) ? "Line 02 barcode detected" : "";
            //lblMessage.Text = (lblMessage.Text.Length > 0) ? lblMessage.Text + "\r\n" + "Line 02 barcode detected" : "Line 02 barcode detected";
            //Fnc_Set_Message("Line 02 barcode detected", 1);
        }

        private void TxtDI01SCN_TextChanged(object sender, EventArgs e)
        {
            string lstBox01 = "";
            string cur_SCN_01 = txtDI01SCN.Text.Trim().ToLower();
            _cur_SCN_01 = (cur_SCN_01 == "true") ? 1 : 0;

            _timerAct = false;
            _controlAct = (txtDI01SCN.Text.ToLower() == "true") ? true : false; /* TRUE: DISPLAY | FALSE: DISAPPEAR */

            if (txtDI01SCN.Text.ToLower() == "true")
            {
                _cur_num_SCN_01 += 1;

                lstBox01 += String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "\r\n";
                //txtNGLog01.Text += lstBox01;
                txtNGLog01.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "\r\n");
                txtNGLog01.SelectionStart = txtNGLog01.Text.Length;
                txtNGLog01.ScrollToCaret();
                txtNGLog01.Refresh();

                lblNGLog.Text += "Line 01, " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "\r\n";
                //lstNGLog01.Items.Add(lstBox01);

                //txtDI01SCN.BackColor = Color.LightPink;
                txtDI01SCN.BackColor = txtDI01SCN.ForeColor = lblMessage.BackColor = Color.LightPink;
                //lblMessage.Text = (lblMessage.Text.Length > 0) ? lblMessage.Text + "\r\n" + "Line 01 no barcode detected" : "Line 01 no barcode detected";
                lblMessage.Text = "Line 01 no barcode detected";
                //Fnc_Set_Message("Line 01 no barcode detected", 2);
            }
            else
            {
                txtDI01SCN.BackColor = txtDI01SCN.ForeColor = lblMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                lblMessage.Text = "";
            }
            //txtDI01SCN.BackColor = txtDI01SCN.ForeColor = lblMessage.BackColor = (txtDI01SCN.Text.ToLower() == "true") ? Color.LightPink : Color.FromKnownColor(KnownColor.Control);
            lblDI01SCN_Num.Text = _cur_num_SCN_01.ToString();
        }

        private void TxtDI02SCN_TextChanged(object sender, EventArgs e)
        {
            string lstBox02 = "";
            string cur_SCN_02 = txtDI02SCN.Text.Trim().ToLower();
            _cur_SCN_02 = (cur_SCN_02 == "true") ? 1 : 0;

            _timerAct = false;
            _controlAct = (txtDI02SCN.Text.ToLower() == "true") ? true : false; /* TRUE: DISPLAY | FALSE: DISAPPEAR */

            if (txtDI02SCN.Text.ToLower() == "true")
            {
                _cur_num_SCN_02 += 1;

                lstBox02 += String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "\r\n";
                //txtNGLog02.Text += lstBox02;
                txtNGLog02.AppendText(String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "\r\n");
                txtNGLog02.SelectionStart = txtNGLog02.Text.Length;
                txtNGLog02.ScrollToCaret();
                txtNGLog02.Refresh();

                lblNGLog.Text += "Line 02, " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt) + "\r\n";
                //lstNGLog02.Items.Add(lstBox02);

                //txtDI02SCN.BackColor = Color.LightPink;
                txtDI02SCN.BackColor = txtDI02SCN.ForeColor = lblMessage.BackColor = Color.LightPink;
                //lblMessage.Text = (lblMessage.Text.Length > 0) ? lblMessage.Text + "\r\n" + "Line 02 no barcode detected" : "Line 02 no barcode detected";
                lblMessage.Text = "Line 02 no barcode detected";
                //Fnc_Set_Message("Line 02 no barcode detected", 2);
            }
            else
            {
                txtDI02SCN.BackColor = txtDI02SCN.ForeColor = lblMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                lblMessage.Text = "";
            }
            //txtDI02SCN.BackColor = txtDI02SCN.ForeColor = lblMessage.BackColor = (txtDI02SCN.Text.ToLower() == "true") ? Color.LightPink : Color.FromKnownColor(KnownColor.Control);
            lblDI02SCN_Num.Text = _cur_num_SCN_02.ToString();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void frmAssyDispenser_v1o3_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
            }
        }

        public void Fnc_Load_Dispenser_Data()
        {
            try
            {
                int lstCount = 0, rowCount = 0;
                string line = "", dis01_ln = "", dis01_ok = "", dis01_ng = "", dis01_upd = "", dis02_ln = "", dis02_ok = "", dis02_ng = "", dis02_upd = "", dis01_val = "", dis02_val = "";
                int _line = 0, _dis01_ln = 0, _dis01_ok = 0, _dis01_ng = 0, _dis02_ln = 0, _dis02_ok = 0, _dis02_ng = 0, _dis01_val = 0, _dis02_val = 0;
                DateTime _dis01_upd, _dis02_upd;

                string sql = "V2_BASE_CAPACITY_GET_DISPENSER_DATA_ADDNEW";

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                lstCount = ds.Tables.Count;
                rowCount = ds.Tables[0].Rows.Count;

                if (lstCount > 0 && rowCount > 0)
                {
                    //for(int i = 0; i < rowCount; i++)
                    //{
                    //    dis01_ok = ds.Tables[0].Rows[i][2].ToString();
                    //    dis01_ng = ds.Tables[0].Rows[i][3].ToString();
                    //    dis01_upd = ds.Tables[0].Rows[i][5].ToString();

                    //    dis02_ok = ds.Tables[0].Rows[i][2].ToString();
                    //    dis02_ng = ds.Tables[0].Rows[i][3].ToString();
                    //    dis02_upd = ds.Tables[0].Rows[i][5].ToString();
                    //}

                    switch (rowCount)
                    {
                        case 1:
                            line = ds.Tables[0].Rows[0][1].ToString();
                            _line = (line != "" && line != null) ? Convert.ToInt32(line) : 0;
                            switch (_line)
                            {
                                case 4:
                                    dis01_ok = ds.Tables[0].Rows[0][2].ToString();
                                    dis01_ng = ds.Tables[0].Rows[0][3].ToString();
                                    dis01_upd = ds.Tables[0].Rows[0][5].ToString();
                                    dis01_val = ds.Tables[0].Rows[0][6].ToString();
                                    break;
                                case 5:
                                    dis02_ok = ds.Tables[0].Rows[0][2].ToString();
                                    dis02_ng = ds.Tables[0].Rows[0][3].ToString();
                                    dis02_upd = ds.Tables[0].Rows[0][5].ToString();
                                    dis02_val = ds.Tables[0].Rows[0][6].ToString();
                                    break;
                            }

                            break;
                        case 2:
                            dis01_ok = ds.Tables[0].Rows[0][2].ToString();
                            dis01_ng = ds.Tables[0].Rows[0][3].ToString();
                            dis01_upd = ds.Tables[0].Rows[0][5].ToString();
                            dis01_val = ds.Tables[0].Rows[0][6].ToString();

                            dis02_ok = ds.Tables[0].Rows[1][2].ToString();
                            dis02_ng = ds.Tables[0].Rows[1][3].ToString();
                            dis02_upd = ds.Tables[0].Rows[1][5].ToString();
                            dis02_val = ds.Tables[0].Rows[1][6].ToString();
                            break;
                    }

                    //if (rowCount == 2)
                    //{
                    //    dis01_ok = ds.Tables[0].Rows[0][2].ToString();
                    //    dis01_ng = ds.Tables[0].Rows[0][3].ToString();
                    //    dis01_upd = ds.Tables[0].Rows[0][5].ToString();

                    //    dis02_ok = ds.Tables[0].Rows[1][2].ToString();
                    //    dis02_ng = ds.Tables[0].Rows[1][3].ToString();
                    //    dis02_upd = ds.Tables[0].Rows[1][5].ToString();
                    //}


                }
                else
                {
                    dis01_ok = dis01_ng = "0";
                    dis01_upd = "";
                    dis01_val = "";

                    dis02_ok = dis02_ng = "0";
                    dis02_upd = "";
                    dis02_val = "";
                }

                _dis01_ok = (dis01_ok != "" && dis01_ok != null) ? Convert.ToInt32(dis01_ok) : 0;
                _value01_NG = _dis01_ng = (dis01_ng != "" && dis01_ng != null) ? Convert.ToInt32(dis01_ng) : 0;
                _dis01_upd = (dis01_upd != "" && dis01_upd != null) ? Convert.ToDateTime(dis01_upd) : DateTime.Now;
                _dis01_val = (dis01_val != "" && dis01_val != null) ? Convert.ToInt32(dis01_val) : 0;

                _dis02_ok = (dis02_ok != "" && dis02_ok != null) ? Convert.ToInt32(dis02_ok) : 0;
                _value02_NG = _dis02_ng = (dis02_ng != "" && dis02_ng != null) ? Convert.ToInt32(dis02_ng) : 0;
                _dis02_upd = (dis02_upd != "" && dis02_upd != null) ? Convert.ToDateTime(dis02_upd) : DateTime.Now;
                _dis02_val = (dis02_val != "" && dis02_val != null) ? Convert.ToInt32(dis02_val) : 0;

                //if (_dis01_ng > _last_Dis01_NG)
                //{
                //    _last_Dis01_NG = _dis01_ng;
                //    _alarm_change_shift = false;
                //}
                //else
                //{
                //    _alarm_change_shift = true;
                //}

                //if (_dis02_ng > _last_Dis02_NG)
                //{
                //    _last_Dis02_NG = _dis02_ng;
                //    _alarm_change_shift = false;
                //}
                //else
                //{
                //    _alarm_change_shift = true;
                //}

                //if (_dis01_ng > _last_Dis01_NG || _dis02_ng > _last_Dis02_NG)
                //{
                //    _alarm_change_shift = true;

                //    if(_dis01_ng > _last_Dis01_NG) { _last_Dis01_NG = _dis01_ng; lbl_alarm_change_shift_01.Text = _alarm_change_shift.ToString(); }
                //    if(_dis02_ng > _last_Dis02_NG) { _last_Dis02_NG = _dis02_ng; lbl_alarm_change_shift_02.Text = _alarm_change_shift.ToString(); }
                //}
                //else
                //{
                //    _alarm_change_shift = false;
                //    lbl_alarm_change_shift_01.Text = lbl_alarm_change_shift_02.Text = _alarm_change_shift.ToString();
                //}


                //txtDI01OK.Text = _dis01_ok.ToString();
                //txtDI01NG.Text = _dis01_ng.ToString();
                //txtDI02OK.Text = _dis02_ok.ToString();
                //txtDI02NG.Text = _dis02_ng.ToString();

                txtDI01OK.Text = dis01_ok;
                txtDI01NG.Text = dis01_ng;
                txtDI01VAL.Text = _dis01_val.ToString();

                txtDI02OK.Text = dis02_ok;
                txtDI02NG.Text = dis02_ng;
                txtDI02VAL.Text = _dis02_val.ToString();

                _last_Dis01_NG = (_dis01_ng == 0) ? 0 : _last_Dis01_NG;
                _last_Dis02_NG = (_dis02_ng == 0) ? 0 : _last_Dis02_NG;

                //lbl_last_Dis01_NG.Text = _last_Dis01_NG.ToString();
                //lbl_last_Dis02_NG.Text = _last_Dis02_NG.ToString();
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Upate_Scan_No(int line, int valSCN)
        {
            try
            {
                string sql = "V2_BASE_CAPACITY_GET_DISPENSER_SCAN_NO_UPDITEM_ADDNEW";

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@line";
                sParams[0].Value = line;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Bit;
                sParams[1].ParameterName = "@scn";
                sParams[1].Value = valSCN;

                cls.fnUpdDel(sql, sParams);
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void Fnc_Select_Scan_Readable(int line)
        {
            try
            {
                int listCount = 0, rowCount = 0;
                bool _readable = false;
                string codeReadable = "";
                bool _codeReadable = false;
                string sql = "V2_BASE_CAPACITY_GET_DISPENSER_SCAN_READABLE_SELITEM_ADDNEW";

                SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@line";
                sParams[0].Value = line;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Bit;
                sParams[1].ParameterName = "@readable";
                sParams[1].Value = _readable;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                listCount = ds.Tables.Count;
                rowCount = ds.Tables[0].Rows.Count;

                if (listCount > 0 && rowCount > 0)
                {
                    codeReadable = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    codeReadable = "False";
                }
                _codeReadable = Convert.ToBoolean(codeReadable);

                if (_codeReadable == true)
                {
                    Fnc_Read_Code(line);
                }
            }
            catch
            {
                
            }
            finally
            {

            }
        }

        public void Fnc_Select_Scan_No(int line)
        {
            string sql = "V2_BASE_CAPACITY_GET_DISPENSER_SCAN_NO_SELITEM_ADDNEW";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@line";
            sParams[0].Value = line;
        }

        private void Btn_alarm_test_01_Click(object sender, EventArgs e)
        {
            _alarm_change_shift = true;
            Fnc_Alarm_Test(4);
        }

        private void Btn_alarm_test_02_Click(object sender, EventArgs e)
        {
            _alarm_change_shift = true;
            Fnc_Alarm_Test(5);
        }

        public void Fnc_Alarm_Test(int line)
        {
            string tagname = "";
            //_alarm_change_shift = true;

            if (_alarm_change_shift == true)
            {
                switch (line)
                {
                    case 4:
                        lbl_alarm_change_shift_01.Text = _alarm_change_shift.ToString();
                        tagname = "Channel1.Device1.Dispenser01_AL";
                        break;
                    case 5:
                        lbl_alarm_change_shift_02.Text = _alarm_change_shift.ToString();
                        tagname = "Channel1.Device1.Dispenser02_AL";
                        break;
                }

                fnWarningStart(tagname);
                //fnWarningStop(tagname);

                _alarm_change_shift = false;
            }
        }

        public void Fnc_Read_Code(int line)
        {
            string tagname = "";
            switch (line)
            {
                case 4:
                    tagname = "Channel1.Device1.Dispenser01_SCY";
                    break;
                case 5:
                    tagname = "Channel1.Device1.Dispenser02_SCY";
                    break;
            }

            fnReadCodeDone(tagname);
        }

        public void Fnc_Prepair_Logging_Name()
        {
            string prefix_Year = "", prefix_Month = "", prefix_Day = "", prefix_Shift = "";

            if (_dt.Day == 1 && _dt.Hour == 7)
            {
                prefix_Year = _dt.Year.ToString();
                prefix_Month = String.Format("{0:00}", _dt.AddMonths(-1));
                prefix_Day = String.Format("{0:00}", _dt.AddDays(-1));
            }
            else
            {
                prefix_Year = _dt.Year.ToString();
                prefix_Month = String.Format("{0:00}", _dt.Month);
                prefix_Day = String.Format("{0:00}", _dt.Day);
            }

            switch (_dt.Hour)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 20:
                case 21:
                case 22:
                case 23:
                    prefix_Shift = "02";
                    break;
                default:
                    prefix_Shift = "01";
                    break;
            }

            //prefix_Year = _dt.Year.ToString();
            //prefix_Month = String.Format("{0:00}", _dt.Month);
            //prefix_Day = (_dt.Hour >= 8) ? String.Format("{0:00}", _dt.Day) : String.Format("{0:00}", _dt.Day - 1);
            //_flePath = @"F:\SQLExpress\Dispenser_Monitoring\" + prefix_Year + @"\" + prefix_Year + prefix_Month + @"\" + prefix_Year + prefix_Month + prefix_Day + @"\";

            _flePath = @"F:\SQLExpress\Dispenser_Monitoring\" + prefix_Year + @"\" + prefix_Year + prefix_Month + @"\";
            //_fleName = "DISPENSER_D" + prefix_Day + "_S[S].txt";
            _fleName = "DISPENSER_D" + prefix_Day + "_S" + prefix_Shift + ".txt";
        }

        public void Fnc_Prepair_NG_Log()
        {
            if(_export == true)
            {
                string fName = "";

                //fName = (_dt.Hour == 7) ? _fleName.Replace("[S]", "2") : _fleName.Replace("[S]", "1");
                if (!Directory.Exists(_flePath)) { Directory.CreateDirectory(_flePath); }
                _fleValue = lblNGLog.Text;
                //Fnc_WriteData(_flePath, fName, _fleValue);
                Fnc_WriteData(_flePath, _fleName, _fleValue);

                lblNGLog.Text = "";
                //lstNGLog01.Items.Clear();
                //lstNGLog02.Items.Clear();
                txtNGLog01.Text = txtNGLog02.Text = "";
                _pre_num_SCN_01 = _pre_num_SCN_02 = 0;
                _cur_num_SCN_01 = _cur_num_SCN_02 = 0;
                lblDI01SCN_Num.Text = lblDI02SCN_Num.Text = "0";

                _export = false;
            }

            //if (((_dt.Hour == 19) || (_dt.Hour == 7)) && _dt.Minute == 59 && _dt.Second == 0)
            //{
            //}
        }

        public void Fnc_Refresh_Dispenser_Data()
        {

            //Thread loadTime = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        _dt = DateTime.Now;

            //        Thread.Sleep(1000);
            //    }
            //});
            //loadTime.IsBackground = true;
            //loadTime.Start();

            //Thread loadData = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        Fnc_Load_Dispenser_Data();
            //        Fnc_Select_Scan_Readable(4);
            //        Fnc_Select_Scan_Readable(5);
            //        Fnc_Upate_Scan_No(4, _cur_SCN_01);
            //        Fnc_Upate_Scan_No(5, _cur_SCN_02); 

            //        Thread.Sleep(500);
            //    }
            //});
            //loadData.IsBackground = true;
            //loadData.Start();

            //Thread loadLogs = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        //if (((_dt.Hour == 19) || (_dt.Hour == 7)) && _dt.Minute == 59 && _dt.Second == 50)
            //        if (_dt.Hour == 12 && _dt.Minute == 30 && _dt.Second == 50)
            //        {
            //            _export = true;
            //            Fnc_Prepair_NG_Log();
            //        }

            //        Thread.Sleep(1000);
            //    }
            //});
            //loadLogs.IsBackground = true;
            //loadLogs.Start();

            ////Thread loadLog = new Thread(() =>
            ////{
            ////    while (true)
            ////    {
            ////        //Fnc_Load_DateTime();

            ////        Thread.Sleep(1000);
            ////    }
            ////});
            ////loadLog.IsBackground = true;
            ////loadLog.Start();
        }

        public void Fnc_Update_Valve_Number(int line, int value)
        {
            string tagname = "";
            switch (line)
            {
                case 4:
                    tagname = "Channel1.Device1.Dispenser01_VAL";
                    break;
                case 5:
                    tagname = "Channel1.Device1.Dispenser02_VAL";
                    break;
            }

            fnUpdateValveNumber(tagname, value);
        }

        public void Fnc_WriteData(string path, string filename, string value)
        {
            string newPath = path + filename;
            int line = 0;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, filename), true))
            {
                if (line == 0)
                {
                    outputFile.WriteLine(value);
                    outputFile.Close();

                    line += 1;
                }
            }
        }

        public void Fnc_Set_Message(string msg,byte type)
        {
            switch (type)
            {
                case 1:
                    lblMessage.BackColor = Color.LightGreen;
                    break;
                case 2:
                    lblMessage.BackColor = Color.LightPink;
                    break;
            }

            lblMessage.Text = msg;
        }

        private void TxtDI01VAL_TextChanged(object sender, EventArgs e)
        {
            string curVal01 = txtDI01VAL.Text.Trim();
            int _curVal01 = (curVal01 != "" && curVal01 != null) ? Convert.ToInt32(curVal01) : 0;

            Fnc_Update_Valve_Number(4, _curVal01);
            //if (curVal != _prev_Val_01)
            //{
            //    Fnc_Update_Valve_Number(4, _curVal);
            //    _prev_Val_01 = curVal;
            //}
        }

        private void TxtDI02VAL_TextChanged(object sender, EventArgs e)
        {
            string curVal02 = txtDI02VAL.Text.Trim();
            int _curVal02 = (curVal02 != "" && curVal02 != null) ? Convert.ToInt32(curVal02) : 0;

            Fnc_Update_Valve_Number(5, _curVal02);
            //if (curVal != _prev_Val_02)
            //{
            //    Fnc_Update_Valve_Number(5, _curVal);
            //    _prev_Val_02 = curVal;
            //}
        }

        private void lnkSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _export = true;
            Fnc_Prepair_NG_Log();

            //MessageBox.Show("Data is saved!!!\r\n\r\nCheck data at:\r\n" + _flePath);

            _timerAct = true;

            Fnc_Set_Message("Data is saved!", 1);
        }

        private void lnkCopy01_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string logNG = "";
            string logDT = String.Format("{0:dd/MM/yyyy}", _dt);
            //if (lstNGLog01.Items.Count > 0)
            //{
            //    foreach (object item in lstNGLog01.Items)
            //    {
            //        logNG += "Line01, " + item.ToString() + "\r\n";
            //    }
            //    Clipboard.SetText(logNG);
            //    MessageBox.Show("NG Log at Line 01 copied!\r\n(Paste to any editable program that you need)");
            //}

            _timerAct = true;

            if (txtNGLog01.Text.Length > 0)
            {
                logNG = txtNGLog01.Text.Replace(logDT, "Line 01, " + logDT);
                Clipboard.SetText(logNG);
                //MessageBox.Show("NG Log at Line 01 copied!\r\n(Paste to any editable program that you need)");
                Fnc_Set_Message("NG Log at Line 01 copied!", 1);
            }
            else
            {
                //MessageBox.Show("Nothing to copied!");
                Fnc_Set_Message("Nothing to copied!", 2);
            }
        }

        private void lnkCopy02_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string logNG = "";
            string logDT = String.Format("{0:dd/MM/yyyy}", _dt);
            //if (lstNGLog02.Items.Count > 0)
            //{
            //    foreach (object item in lstNGLog02.Items)
            //    {
            //        logNG += "Line02, " + item.ToString() + "\r\n";
            //    }
            //    Clipboard.SetText(logNG);
            //    MessageBox.Show("NG Log at Line 02 copied!\r\n(Paste to any editable program that you need)");
            //}

            _timerAct = true;

            if (txtNGLog02.Text.Length > 0)
            {
                logNG = txtNGLog02.Text.Replace(logDT, "Line 02, " + logDT);
                Clipboard.SetText(logNG);
                Fnc_Set_Message("NG Log at Line 02 copied!", 1);
            }
            else
            {
                //MessageBox.Show("Nothing to copied!");
                Fnc_Set_Message("Nothing to copied!", 2);
            }
        }

        private void lnkCopyAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string logNG = "";
            if (lblNGLog.Text.Length > 0)
            {
                logNG = lblNGLog.Text;
                Clipboard.SetText(logNG);
                //MessageBox.Show("NG Log all lines copied!\r\n(Paste to any editable program that you need)");
                Fnc_Set_Message("All NG Logging is copied!", 1);
            }
            else
            {
                //MessageBox.Show("Nothing to copied!");
                Fnc_Set_Message("Nothing to copied!", 2);
            }
        }

        private void btn_alarm_disable_01_Click(object sender, EventArgs e)
        {
            //fnUpdateValveNumber("Channel1.Device1.Dispenser01_AL", 0);
            Fnc_Update_Valve_Number(4, 0);
        }

        private void btn_alarm_disable_02_Click(object sender, EventArgs e)
        {
            //fnUpdateValveNumber("Channel1.Device1.Dispenser02_AL", 0);
            Fnc_Update_Valve_Number(5, 0);
        }

        private void btn_alarm_disable_Click(object sender, EventArgs e)
        {
            fnUpdateValveNumber("Channel1.Device1.Dispenser01_AL", 0);
            fnUpdateValveNumber("Channel1.Device1.Dispenser02_AL", 0);
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            if (_timerAct == true)
            {
                timerMsg.Interval = 2000;
                timerMsg.Enabled = true;
                timerMsg.Tick += new EventHandler(timerMsg_Tick);
                if (lblMessage.Text.Length > 0)
                {
                    timerMsg.Start();
                }
                else
                {
                    timerMsg.Stop();
                }
            }
            else
            {
                timerMsg.Enabled = false;
                timerMsg.Stop();

                if (_controlAct == true)
                {
                    // DISPLAY MESSAGE

                }
                else
                {
                    // DISAPPEAR MESSAGE

                    lblMessage.Text = "";
                    lblMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
                    lblMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                }

                _timerAct = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _export = true;
            Fnc_Prepair_NG_Log();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            lblDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Fnc_Load_Dispenser_Data();
            Fnc_Select_Scan_Readable(4);
            Fnc_Select_Scan_Readable(5);
            Fnc_Upate_Scan_No(4, _cur_SCN_01);
            Fnc_Upate_Scan_No(5, _cur_SCN_02);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Fnc_Prepair_Logging_Name();

            if (((_dt.Hour == 19) || (_dt.Hour == 7)) && _dt.Minute == 59 && _dt.Second == 0)
            //if (_dt.Hour == 11 && _dt.Minute == 57 && _dt.Second == 0)
            {
                _export = true;
                Fnc_Prepair_NG_Log();
            }
        }

        public void timerMsg_Tick(object sender, EventArgs e)
        {
            timerMsg.Stop();

            lblMessage.Text = "";
            lblMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
            lblMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
        }
    }
}
