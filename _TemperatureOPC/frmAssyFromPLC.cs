using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPCAutomation;  // dùng thư viện OPCautomation
using Inventory_Data;
using System.Data.SqlClient;
// using System.Collections.Generic;


namespace Fan_1
{
    public partial class frmAssyFromPLC : Form
    {
        public int run = 1;
        public static int item = 0;

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

        public frmAssyFromPLC()
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
            txtDI01OK.Text = "0";
            txtDI01NG.Text = "0";
            txtDI02OK.Text = "0";
            txtDI02NG.Text = "0";
            txtMX01OK.Text = "0";
        }

        private void frmAssyFromPLC_Load(object sender, EventArgs e)
        {
            fnConnect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
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
                ItemCount = 21;
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

                OPCItemIDs.SetValue("Channel1.Device1.Dispenser01_OK", 16);
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
            txtDI01OK.Text = "0";
            txtDI01NG.Text = "0";
            txtDI02OK.Text = "0";
            txtDI02NG.Text = "0";
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
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 1) { txtAB01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 2) { txtAB01NG.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 3) { txtAB02OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 4) { txtAB02NG.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 5) { txtAB03OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 6) { txtAB03NG.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 7) { txtBL01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 8) { txtBL02OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 9) { txtPU01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 10) { txtPU01NG.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 11) { txtWB01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 12) { txtWB02OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 13) { txtWB03OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 14) { txtWD01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 15) { txtWD02OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 16) { txtDI01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 17) { txtDI01NG.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 18) { txtDI02OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 19) { txtDI02NG.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 20) { txtMX01OK.Text = ItemValues.GetValue(i).ToString(); }
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 21) { txtWB04OK.Text = ItemValues.GetValue(i).ToString(); }
            }
        }

        private void chkConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConnect.Checked)
            {
                fnConnect();
            }
            else
            {
                fnDisconnect();
            }
            //chkOperate.Checked = (chkOperate.Checked) ? false : false;
        }

        public void fnUpdateData(string lineID, string line, string type, string valueOK, string valueNG)
        {
            string shift = cls.fnGetDate("s").ToUpper();
            int chk = line.IndexOf("-");
            string name = line.Substring(0, chk);
            string pos = line.Substring(chk + 1);
            string lineId = "0";
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
                    tagname = "Channel1.Device1.Dispenser01_NG";
                    break;
                case "Dispenser-2":
                    //tagname = "Channel1.Device1.dispenser2";
                    //tagname = (type.ToLower() == "ok") ? "Channel1.Device1.Dispenser02_OK" : "Channel1.Device1.Dispenser02_NG";
                    tagname = "Channel1.Device1.Dispenser02_NG";
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

            if (type == "OK")
            {
                lblMessage.ForeColor = Color.Blue;
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                //fnConnect();
                fnWarningStart(tagname);
                fnWarningStop(tagname);
                //fnDisconnect();
            }

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
                    MessageBox.Show("No Connect");
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
                    MessageBox.Show("No Connect");
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Auto Balance 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Update Auto Balance 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Auto Balance 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Update Auto Balance 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Auto Balance 03 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Update Auto Balance 03 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI01OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("4", "Dispenser-1", "OK", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
            }
            catch
            {

            }
            finally
            {

            }
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Dispenser 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI01NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("4", "Dispenser-1", "NG", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
            }
            catch
            {

            }
            finally
            {

            }
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Update Dispenser 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI02OK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("5", "Dispenser-2", "OK", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
            }
            catch
            {

            }
            finally
            {

            }
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Dispenser 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void txtDI02NG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                fnUpdateData("5", "Dispenser-2", "NG", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
            }
            catch
            {

            }
            finally
            {

            }
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Update Dispenser 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Pump 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Update Pump 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Weight Balance 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Weight Balance 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Weight Balance 03 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Weight Balance 04 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Blower 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Blower 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Mixing successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Welding 01 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
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
            if (run == 0)
            {
            }
            else
            {
            }
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Text = "Update Welding 02 successful at " + String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void frmAssyFromPLC_Resize(object sender, EventArgs e)
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
    }
}
