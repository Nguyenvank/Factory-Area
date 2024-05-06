using System;
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
// using System.Collections.Generic;


namespace Fan_1
{
    public partial class frmAssyFromPLC_v2o0 : Form
    {
        public int run = 1;
        public static int item = 0;
        //int underTemp = 177, upperTemp = 183;
        DateTime _dt;
        bool _start = false, _R_start = false, _P_start = false, _A_start = false;
        bool _R_upd = false, _P_upd = false, _A_upd = false;

        string _R01_SV, _R02_SV, _R03_SV, _R04_SV, _R05_SV, _R06_SV, _R07_SV, _R08_SV, _R09_SV, _R10_SV, _R11_SV, _R12_SV, _R13_SV, _R14_SV, _R15_SV, _R16_SV;
        string _R01_PV, _R02_PV, _R03_PV, _R04_PV, _R05_PV, _R06_PV, _R07_PV, _R08_PV, _R09_PV, _R10_PV, _R11_PV, _R12_PV, _R13_PV, _R14_PV, _R15_PV, _R16_PV;

        string _P01_SV, _P02_SV, _P03_SV, _P04_SV, _P05_SV, _P06_SV, _P07_SV, _P08_SV, _P09_SV, _P10_SV, _P11_SV, _P12_SV;
        string _P01_PV, _P02_PV, _P03_PV, _P04_PV, _P05_PV, _P06_PV, _P07_PV, _P08_PV, _P09_PV, _P10_PV, _P11_PV, _P12_PV;

        string _A01_SV, _A02_SV, _A03_SV, _A04_SV, _A05_SV, _A06_SV;
        string _A01_PV, _A02_PV, _A03_PV, _A04_PV, _A05_PV, _A06_PV;

        public static string shiftsno = "1";
        public static string shiftsnm = "Night";
        public static string workdate = "";
        public static string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        public static DateTime sTime1 = new DateTime();
        public static DateTime sTime2 = new DateTime();

        bool _connect, _R_Connect, _P_Connect, _A_Connect;
        int _itemCount = 0;
        int _last_Dis01_NG = 0, _last_Dis02_NG = 0;
        bool _alarm_change_shift = false;
        int _value01_NG = 0, _value02_NG = 0;


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

        public frmAssyFromPLC_v2o0()
        {
            InitializeComponent();

            txtAB01OK.Text = "0";
            txtAB01NG.Text = "0";
            txtAB02OK.Text = "0";
            txtAB02NG.Text = "0";
            txtAB03OK.Text = "0";
            txtAB03NG.Text = "0";
            txtBL01OK.Text = "0";
            txtBL02OK.Text = "0";
            txtPU01OK.Text = "0";
            txtPU01NG.Text = "0";
            txtWB01OK.Text = "0";
            txtWB02OK.Text = "0";
            txtWB03OK.Text = "0";
            txtWD01OK.Text = "0";
            txtWD02OK.Text = "0";
            //txtDI01OK.Text = "0";
            //txtDI01NG.Text = "0";
            //txtDI02OK.Text = "0";
            //txtDI02NG.Text = "0";
            txtMX01OK.Text = "0";

            //Fnc_Refresh_Dispenser_Data();
        }

        private void frmAssyFromPLC_v2o0_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;


            //_R_Connect = (chk_R_Connect.Checked) ? true : false;
            //_P_Connect = (chk_P_Connect.Checked) ? true : false;
            //_A_Connect = (chk_A_Connect.Checked) ? true : false;

            //_itemCount = 69;
            _itemCount = 21;    // Rubber + Plastic01 + Plastic03
            //if (_R_Connect == true) { _itemCount = 69; }
            if (_P_Connect == true) { _itemCount = 105; }
            if (_A_Connect == true) { _itemCount = 123; }

            fnConnect();
            //fnTemperatureThread();
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

                OPCItemIDs.SetValue("Channel1.Device1.AutoBalance01_OK", 1);
                ClientHandles.SetValue(1, 1);

                OPCItemIDs.SetValue("Channel1.Device1.AutoBalance01_NG", 2);
                ClientHandles.SetValue(2, 2);

                OPCItemIDs.SetValue("Channel1.Device1.AutoBalance02_OK", 3);
                ClientHandles.SetValue(3, 3);

                OPCItemIDs.SetValue("Channel1.Device1.AutoBalance02_NG", 4);
                ClientHandles.SetValue(4, 4);

                OPCItemIDs.SetValue("Channel1.Device1.AutoBalance03_OK", 5);
                ClientHandles.SetValue(5, 5);

                OPCItemIDs.SetValue("Channel1.Device1.AutoBalance03_NG", 6);
                ClientHandles.SetValue(6, 6);

                OPCItemIDs.SetValue("Channel1.Device1.Blower01_OK", 7);
                ClientHandles.SetValue(7, 7);

                OPCItemIDs.SetValue("Channel1.Device1.Blower02_OK", 8);
                ClientHandles.SetValue(8, 8);

                OPCItemIDs.SetValue("Channel1.Device1.Pump01_OK", 9);
                ClientHandles.SetValue(9, 9);

                OPCItemIDs.SetValue("Channel1.Device1.Pump01_NG", 10);
                ClientHandles.SetValue(10, 10);

                OPCItemIDs.SetValue("Channel1.Device1.WeightBalance01_OK", 11);
                ClientHandles.SetValue(11, 11);

                OPCItemIDs.SetValue("Channel1.Device1.WeightBalance02_OK", 12);
                ClientHandles.SetValue(12, 12);

                OPCItemIDs.SetValue("Channel1.Device1.WeightBalance03_OK", 13);
                ClientHandles.SetValue(13, 13);

                OPCItemIDs.SetValue("Channel1.Device1.Welding01_OK", 14);
                ClientHandles.SetValue(14, 14);

                OPCItemIDs.SetValue("Channel1.Device1.Welding02_OK", 15);
                ClientHandles.SetValue(15, 15);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser01_OK", 16); //Dispenser01_OK    Dispenser01_OK
                ClientHandles.SetValue(16, 16);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser01_NG", 17);
                ClientHandles.SetValue(17, 17);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser02_OK", 18);
                ClientHandles.SetValue(18, 18);

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser02_NG", 19);
                ClientHandles.SetValue(19, 19);

                OPCItemIDs.SetValue("Channel1.Device1.Mixing01_OK", 20);
                ClientHandles.SetValue(20, 20);

                OPCItemIDs.SetValue("Channel1.Device1.WeightBalance04_OK", 21);
                ClientHandles.SetValue(21, 21);

                //'Set the desire active state for the Items " đây là đoạn kết nối mình cứ thế thêm vào thôi"
                ConnectedGroup.OPCItems.DefaultIsActive = true;
                ConnectedGroup.OPCItems.AddItems(ItemCount, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
                chkConnect.Checked = true;

                lblStatus.Text = "OPC Connected";
                lblStatus.BackColor = Color.DodgerBlue;

                Fnc_Refresh_Dispenser_Data();
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

            txtAB01OK.Text = "0";
            txtAB01NG.Text = "0";
            txtAB02OK.Text = "0";
            txtAB02NG.Text = "0";
            txtAB03OK.Text = "0";
            txtAB03NG.Text = "0";
            //txtDI01OK.Text = "0";
            //txtDI01NG.Text = "0";
            //txtDI02OK.Text = "0";
            //txtDI02NG.Text = "0";
            txtPU01OK.Text = "0";
            txtPU01NG.Text = "0";
            txtWB01OK.Text = "0";
            txtWB02OK.Text = "0";
            txtWB03OK.Text = "0";
            txtBL01OK.Text = "0";
            txtBL02OK.Text = "0";
            txtMX01OK.Text = "0";
            txtWD01OK.Text = "0";
            txtWD02OK.Text = "0";

        }

        private void ObjOPCGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
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
                        txtAB01OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 2:
                        txtAB01NG.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 3:
                        txtAB02OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 4:
                        txtAB02NG.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 5:
                        txtAB03OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 6:
                        txtAB03NG.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 7:
                        txtBL01OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 8:
                        txtBL02OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 9:
                        txtPU01OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 10:
                        txtPU01NG.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 11:
                        txtWB01OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 12:
                        txtWB02OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 13:
                        txtWB03OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 14:
                        txtWD01OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 15:
                        txtWD02OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                        //Fnc_Load_Dispenser_Data();
                        break;
                    case 20:
                        txtMX01OK.Text = ItemValues.GetValue(i).ToString();
                        break;
                    case 21:
                        txtWB04OK.Text = ItemValues.GetValue(i).ToString();
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

        public void fnUpdateData(string lineID, string line, string type, string valueOK, string valueNG)
        {
            string shift = cls.fnGetDate("s").ToUpper();
            int chk = line.IndexOf("-");
            string name = line.Substring(0, chk);
            string pos = line.Substring(chk + 1);
            //string lineId = "0";
            string tagname = "";
            switch (name)
            {
                //case "Auto Balance-1":
                //    //tagname = "Channel1.Device1.autobalance1";
                //    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.AutoBalance01_OK" : "Channel1.Device1.AutoBalance01_NG";
                //    tagname = "Channel1.Device1.AutoBalance01_NG";
                //    break;
                //case "Auto Balance-2":
                //    //tagname = "Channel1.Device1.autobalance2";
                //    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.AutoBalance02_OK" : "Channel1.Device1.AutoBalance02_NG";
                //    tagname = "Channel1.Device1.AutoBalance02_NG";
                //    break;
                //case "Auto Balance-3":
                //    //tagname = "Channel1.Device1.autobalance3";
                //    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.AutoBalance03_OK" : "Channel1.Device1.AutoBalance03_NG";
                //    tagname = "Channel1.Device1.AutoBalance03_NG";
                //    break;
                case "Dispenser-1":
                    //tagname = "Channel1.Device1.dispenser1";
                    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.Dispenser01_OK" : "Channel1.Device1.Dispenser01_NG";
                    //tagname = "Channel1.Device1.Dispenser01_NG";
                    tagname = "Channel1.Device1.Dispenser01_AL";
                    break;
                case "Dispenser-2":
                    //tagname = "Channel1.Device1.dispenser2";
                    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.Dispenser02_OK" : "Channel1.Device1.Dispenser02_NG";
                    //tagname = "Channel1.Device1.Dispenser02_NG";
                    tagname = "Channel1.Device1.Dispenser02_AL";
                    break;
                //case "Weight Balance-1":
                //    //tagname = "Channel1.Device1.weightbalance1_OK";
                //    tagname = "Channel1.Device1.weightbalance1_NG";
                //    break;
                //case "Weight Balance-2":
                //    //tagname = "Channel1.Device1.weightbalance2_OK";
                //    tagname = "Channel1.Device1.weightbalance2_NG";
                //    break;
                //case "Weight Balance-3":
                //    //tagname = "Channel1.Device1.weightbalance3_OK";
                //    tagname = "Channel1.Device1.weightbalance3_NG";
                //    break;
                //case "Pump-1":
                //    //tagname = "Channel1.Device1.pump1";
                //    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.Pump01_OK" : "Channel1.Device1.Pump01_NG";
                //    tagname = "Channel1.Device1.Pump01_NG";
                //    break;
                //case "Pump-2":
                //    //tagname = "Channel1.Device1.pump2";
                //    tagname = "";
                //    break;
                //case "Welding-1":
                //    //tagname = "Channel1.Device1.Welding1_OK";
                //    tagname = "Channel1.Device1.Welding1_NG";
                //    break;
                //case "Welding-2":
                //    //tagname = "Channel1.Device1.Welding2_OK";
                //    tagname = "Channel1.Device1.Welding2_NG";
                //    break;
                //case "Mixing-1":
                //    //tagname = "Channel1.Device1.Mixing1_OK";
                //    tagname = "Channel1.Device1.Mixing1_NG";
                //    break;
                //case "Blower-1":
                //    //tagname = "Channel1.Device1.Blower1_OK";
                //    tagname = "Channel1.Device1.Blower1_NG";
                //    break;
                //case "Blower-2":
                //    //tagname = "Channel1.Device1.Blower2_OK";
                //    tagname = "Channel1.Device1.Blower2_NG";
                //    break;
            }

            if (lineID != "4" && lineID != "5")
            {
                string sql = "";
                sql = "V2_BASE_CAPACITY_GET_INS_PLC_DATA_ADDNEW";
                SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.VarChar;
                sParams[0].ParameterName = "assyShift";
                sParams[0].Value = shift;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.Int;
                sParams[1].ParameterName = "lineId";
                sParams[1].Value = lineID;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.VarChar;
                sParams[2].ParameterName = "assyLine";
                sParams[2].Value = name + " 0" + pos;

                sParams[3] = new SqlParameter();
                sParams[3].SqlDbType = SqlDbType.Int;
                sParams[3].ParameterName = "valueOK";
                sParams[3].Value = valueOK;

                sParams[4] = new SqlParameter();
                sParams[4].SqlDbType = SqlDbType.Int;
                sParams[4].ParameterName = "valueNG";
                sParams[4].Value = valueNG;

                cls.fnUpdDel(sql, sParams);
            }

            //if (_alarm_change_shift == true)
            //{
            //    //fnConnect();
            //    fnWarningStart(tagname);
            //    //fnWarningStop(tagname);
            //    //fnDisconnect();
            //    _alarm_change_shift = false;
            //}


            //if (type == "OK")
            //{
            //    lblMessage.ForeColor = Color.Blue;
            //}
            //else
            //{
            //    lblMessage.ForeColor = Color.Red;
            //}

            lblMessage.ForeColor = (type == "OK") ? Color.Blue : Color.Red;
            lblMessage.Text = "Update for " + name + " 0" + pos + " successfull at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now) + ".";
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

        private void txtAB01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("1", "Auto Balance-1", "OK", txtAB01OK.Text.Trim(), txtAB01NG.Text.Trim());
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
            //lblMessage.Text = "Update Auto Balance 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtAB01NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("1", "Auto Balance-1", "NG", txtAB01OK.Text.Trim(), txtAB01NG.Text.Trim());
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
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Text = "Update Auto Balance 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtAB02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("2", "Auto Balance-2", "OK", txtAB02OK.Text.Trim(), txtAB02NG.Text.Trim());
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
            //lblMessage.Text = "Update Auto Balance 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtAB02NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("2", "Auto Balance-2", "NG", txtAB02OK.Text.Trim(), txtAB02NG.Text.Trim());
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
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Text = "Update Auto Balance 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtAB03OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("3", "Auto Balance-3", "OK", txtAB03OK.Text.Trim(), txtAB03NG.Text.Trim());
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
            //lblMessage.Text = "Update Auto Balance 03 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtAB03NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("3", "Auto Balance-3", "NG", txtAB03OK.Text.Trim(), txtAB03NG.Text.Trim());
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
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Text = "Update Auto Balance 03 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //fnUpdateData("4", "Dispenser-1", "OK", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
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
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txtDI02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //fnUpdateData("5", "Dispenser-2", "OK", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
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
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        private void txtPU01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("6", "Pump-1", "OK", txtPU01OK.Text.Trim(), txtPU01NG.Text.Trim());
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
            //lblMessage.Text = "Update Pump 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtPU01NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("6", "Pump-1", "NG", txtPU01OK.Text.Trim(), txtPU01NG.Text.Trim());
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
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Text = "Update Pump 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtWB01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("7", "Weight Balance-1", "OK", txtWB01OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Weight Balance 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtWB02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("8", "Weight Balance-2", "OK", txtWB02OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Weight Balance 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtWB03OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("9", "Weight Balance-3", "OK", txtWB03OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Weight Balance 03 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtWB04OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("10", "Weight Balance-4", "OK", txtWB04OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Weight Balance 04 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtBL01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("11", "Blower-1", "OK", txtBL01OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Blower 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtBL02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("12", "Blower-2", "OK", txtBL02OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Blower 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtMX01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("15", "Mixing-1", "OK", txtMX01OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Mixing successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtWD01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("13", "Welding-1", "OK", txtWD01OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Welding 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtWD02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("14", "Welding-2", "OK", txtWD02OK.Text.Trim(), "0");
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
            //lblMessage.Text = "Update Welding 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void frmAssyFromPLC_v2o0_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
            }
        }

        private void chkOperate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOperate.Checked)
            {
                chkOperate.Text = "GARTHERING";
                chkOperate.ForeColor = Color.Blue;
                run = 1;
            }
            else
            {
                chkOperate.Text = "MONITORING";
                chkOperate.ForeColor = Color.Firebrick;
                run = 0;
            }
        }

        public void Fnc_Load_Dispenser_Data()
        {
            try
            {
                int lstCount = 0, rowCount = 0;
                string line = "", dis01_ln = "", dis01_ok = "", dis01_ng = "", dis01_upd = "", dis02_ln = "", dis02_ok = "", dis02_ng = "", dis02_upd = "";
                int _line = 0, _dis01_ln = 0, _dis01_ok = 0, _dis01_ng = 0, _dis02_ln = 0, _dis02_ok = 0, _dis02_ng = 0;
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
                                    break;
                                case 5:
                                    dis02_ok = ds.Tables[0].Rows[0][2].ToString();
                                    dis02_ng = ds.Tables[0].Rows[0][3].ToString();
                                    dis02_upd = ds.Tables[0].Rows[0][5].ToString();
                                    break;
                            }

                            break;
                        case 2:
                            dis01_ok = ds.Tables[0].Rows[0][2].ToString();
                            dis01_ng = ds.Tables[0].Rows[0][3].ToString();
                            dis01_upd = ds.Tables[0].Rows[0][5].ToString();

                            dis02_ok = ds.Tables[0].Rows[1][2].ToString();
                            dis02_ng = ds.Tables[0].Rows[1][3].ToString();
                            dis02_upd = ds.Tables[0].Rows[1][5].ToString();
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

                    dis02_ok = dis02_ng = "0";
                    dis02_upd = "";
                }

                _dis01_ok = (dis01_ok != "" && dis01_ok != null) ? Convert.ToInt32(dis01_ok) : 0;
                _value01_NG = _dis01_ng = (dis01_ng != "" && dis01_ng != null) ? Convert.ToInt32(dis01_ng) : 0;
                _dis01_upd = (dis01_upd != "" && dis01_upd != null) ? Convert.ToDateTime(dis01_upd) : DateTime.Now;

                _dis02_ok = (dis02_ok != "" && dis02_ok != null) ? Convert.ToInt32(dis02_ok) : 0;
                _value02_NG = _dis02_ng = (dis02_ng != "" && dis02_ng != null) ? Convert.ToInt32(dis02_ng) : 0;
                _dis02_upd = (dis02_upd != "" && dis02_upd != null) ? Convert.ToDateTime(dis02_upd) : DateTime.Now;

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
                txtDI02OK.Text = dis02_ok;
                txtDI02NG.Text = dis02_ng;

                _last_Dis01_NG = (_dis01_ng == 0) ? 0 : _last_Dis01_NG;
                _last_Dis02_NG = (_dis02_ng == 0) ? 0 : _last_Dis02_NG;

                lbl_last_Dis01_NG.Text = _last_Dis01_NG.ToString();
                lbl_last_Dis02_NG.Text = _last_Dis02_NG.ToString();
            }
            catch
            {

            }
            finally
            {

            }
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

        public void Fnc_Refresh_Dispenser_Data()
        {
            Thread loadData = new Thread(() =>
            {
                while (true)
                {
                    Fnc_Load_Dispenser_Data();

                    Thread.Sleep(1000);
                }
            });
            loadData.IsBackground = true;
            loadData.Start();

            //Thread loadDate = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        //Fnc_Load_DateTime();

            //        Thread.Sleep(1000);
            //    }
            //});
            //loadDate.IsBackground = true;
            //loadDate.Start();
        }


    }
}
