using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OPCAutomation;

namespace Inventory_Data
{
    public partial class frmCapacityWarning : Form
    {
        public static int item = 0;

        // tạo sever và kết nối. Copy đoạn này cũng được rồi hiểu dần.
        public OPCAutomation.OPCServer AnOPCServer;
        public OPCAutomation.OPCServer ConnectedOPCServer;
        public OPCAutomation.OPCGroups ConnectedServerGroup;
        public OPCAutomation.OPCGroup ConnectedGroup;

        public string Groupname;

        int ItemCount;
        Array OPCItemIDs = Array.CreateInstance(typeof(string), 10);
        Array ItemServerHandles = Array.CreateInstance(typeof(Int32), 10);
        Array ItemServerErrors = Array.CreateInstance(typeof(Int32), 10);
        Array ClientHandles = Array.CreateInstance(typeof(Int32), 10);
        Array RequestedDataTypes = Array.CreateInstance(typeof(Int16), 10);
        Array AccessPaths = Array.CreateInstance(typeof(string), 10);
        Array WriteItems = Array.CreateInstance(typeof(string), 10);

        public object OpcSystemsData { get; private set; }
        public object ConnectedOPCGroup { get; private set; }
        public object Simulater { get; private set; }
        public object TextBox3 { get; private set; }



        public frmCapacityWarning()
        {
            InitializeComponent();
        }

        private void frmCapacityWarning_Load(object sender, EventArgs e)
        {
            fnBindNG();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnBindNG();
        }

        public void fnBindNG()
        {
            string sql = "V2o1_BASE_Capacity_NG_Signal_AddItem_Addnew";
            string ab01 = "", ab02 = "", ab03 = "", di01 = "", di02 = "";
            string wb01 = "", wb02 = "", wb03 = "", pu01 = "", pu02 = "";
            string wd01 = "", wd02 = "", mx01 = "", bl01 = "", bl02 = "";
            SqlParameter[] sParams = new SqlParameter[0]; // Parameter count
            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql,sParams);
            if(ds.Tables.Count>0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    ab01 = ds.Tables[0].Rows[0][0].ToString();
                    ab02 = ds.Tables[0].Rows[0][1].ToString();
                    ab03 = ds.Tables[0].Rows[0][2].ToString();
                    di01 = ds.Tables[0].Rows[0][3].ToString();
                    di02 = ds.Tables[0].Rows[0][4].ToString();

                    wb01 = ds.Tables[0].Rows[0][5].ToString();
                    wb02 = ds.Tables[0].Rows[0][6].ToString();
                    wb03 = ds.Tables[0].Rows[0][7].ToString();
                    pu01 = ds.Tables[0].Rows[0][8].ToString();
                    pu02 = ds.Tables[0].Rows[0][9].ToString();

                    wd01 = ds.Tables[0].Rows[0][10].ToString();
                    wd02 = ds.Tables[0].Rows[0][11].ToString();
                    mx01 = ds.Tables[0].Rows[0][12].ToString();
                    bl01 = ds.Tables[0].Rows[0][13].ToString();
                    bl02 = ds.Tables[0].Rows[0][14].ToString();
                }
                else
                {
                    ab01 = "0";
                    ab02 = "0";
                    ab03 = "0";
                    di01 = "0";
                    di02 = "0";

                    wb01 = "0";
                    wb02 = "0";
                    wb03 = "0";
                    pu01 = "0";
                    pu02 = "0";

                    wd01 = "0";
                    wd02 = "0";
                    mx01 = "0";
                    bl01 = "0";
                    bl02 = "0";
                }
            }
            else
            {
                ab01 = "0";
                ab02 = "0";
                ab03 = "0";
                di01 = "0";
                di02 = "0";

                wb01 = "0";
                wb02 = "0";
                wb03 = "0";
                pu01 = "0";
                pu02 = "0";

                wd01 = "0";
                wd02 = "0";
                mx01 = "0";
                bl01 = "0";
                bl02 = "0";
            }
            txtAB01NG.Text = ab01;
            txtAB02NG.Text = ab02;
            txtAB03NG.Text = ab03;
            txtDI01NG.Text = di01;
            txtDI02NG.Text = di02;

            txtWB01NG.Text = wb01;
            txtWB02NG.Text = wb02;
            txtWB03NG.Text = wb03;
            txtPU01NG.Text = pu01;
            txtPU02NG.Text = pu02;

            txtWD01NG.Text = wd01;
            txtWD02NG.Text = wd02;
            txtMX01NG.Text = mx01;
            txtBL01NG.Text = bl01;
            txtBL02NG.Text = bl02;
        }

        public void fnConnectOPC(string tagname)
        {
            try
            {
                string IOServer = "Kepware.KEPServerEX.V4";  // phiên bản opc kepwware cần kết nối.
                string IOGroup = "OPCGroup1";
                //Create a new OPC Server object
                ConnectedOPCServer = new OPCAutomation.OPCServer();
                //Attempt to connect with the server
                // ConnectedOPCServer.Connect(labelOPC.Text);
                ConnectedOPCServer.Connect(IOServer, "192.168.0.97");
                ConnectedGroup = ConnectedOPCServer.OPCGroups.Add(IOGroup);
                ConnectedGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(ObjOPCGroup_DataChange);
                ConnectedGroup.UpdateRate = 10;
                ConnectedGroup.IsSubscribed = ConnectedGroup.IsActive;

                //pictureBox1.Visible = true;   // hiển thị ảnh động cơ khi ấn nút connect
                // lấy giá trị Tag từ OPC lên sever để tạo giao diện
                ItemCount = 1;
                OPCItemIDs.SetValue(tagname, 1);
                ClientHandles.SetValue(1, 1);
                //'Set the desire active state for the Items " đây là đoạn kết nối mình cứ thế thêm vào thôi"
                ConnectedGroup.OPCItems.DefaultIsActive = true;
                ConnectedGroup.OPCItems.AddItems(ItemCount, ref OPCItemIDs, ref ClientHandles, out ItemServerHandles, out ItemServerErrors, RequestedDataTypes, AccessPaths);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void fnDisConnectOPC()
        {
            ConnectedOPCServer.Disconnect();
        }

        private void ObjOPCGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            try
            {
                for (int i = 1; i <= NumItems; i++)
                {
                    if (Convert.ToInt32(ClientHandles.GetValue(i)) == 1)
                    {
                        textBox1.Text = ItemValues.GetValue(i).ToString();
                        textBox1.Text = ItemValues.GetValue(i).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        public void fnWarning(string name)
        {
            string tagname = "";
            switch (name)
            {
                case "Auto Balance-1":
                    //tagname = "Channel1.Device1.autobalance1";
                    tagname = "";
                    break;
                case "Auto Balance-2":
                    //tagname = "Channel1.Device1.autobalance2";
                    tagname = "";
                    break;
                case "Auto Balance-3":
                    //tagname = "Channel1.Device1.autobalance3";
                    tagname = "";
                    break;
                case "Dispenser-1":
                    tagname = "Channel1.Device1.dispenser1";
                    break;
                case "Dispenser-2":
                    tagname = "Channel1.Device1.dispenser2";
                    break;
                case "Weight Balance-1":
                    //tagname = "Channel1.Device1.weightbalance1";
                    tagname = "";
                    break;
                case "Weight Balance-2":
                    //tagname = "Channel1.Device1.weightbalance2";
                    tagname = "";
                    break;
                case "Weight Balance-3":
                    //tagname = "Channel1.Device1.weightbalance3";
                    tagname = "";
                    break;
                case "Pump-1":
                    //tagname = "Channel1.Device1.pump1";
                    tagname = "";
                    break;
                case "Pump-2":
                    //tagname = "Channel1.Device1.pump2";
                    tagname = "";
                    break;
                case "Welding-1":
                    //tagname = "Channel1.Device1.welding1";
                    tagname = "";
                    break;
                case "Welding-2":
                    //tagname = "Channel1.Device1.welding2";
                    tagname = "";
                    break;
                case "Mixing-1":
                    //tagname = "Channel1.Device1.mixing1";
                    tagname = "";
                    break;
                case "Blower-1":
                    //tagname = "Channel1.Device1.blower1";
                    tagname = "";
                    break;
                case "Blower-2":
                    //tagname = "Channel1.Device1.blower2";
                    tagname = "";
                    break;
            }
            fnConnectOPC(tagname);
            fnWarningStart(tagname);
            fnWarningStop(tagname);
            fnDisConnectOPC();
        }


        private void txtAB01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Auto Balance-1");
        }

        private void txtAB02NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Auto Balance-2");
        }

        private void txtAB03NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Auto Balance-3");
        }

        private void txtDI01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Dispenser-1");
        }

        private void txtDI02NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Dispenser-2");
        }

        private void txtWB01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Weight Balance-1");
        }

        private void txtWB02NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Weight Balance-2");
        }

        private void txtWB03NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Weight Balance-3");
        }

        private void txtPU01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Pump-1");
        }

        private void txtPU02NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Pump-2");
        }

        private void txtWD01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Welding-1");
        }

        private void txtWD02NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Welding-2");
        }

        private void txtMX01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Mixing-1");
        }

        private void txtBL01NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Blower-1");
        }

        private void txtBL02NG_TextChanged(object sender, EventArgs e)
        {
            fnWarning("Blower-2");
        }
    }
}
