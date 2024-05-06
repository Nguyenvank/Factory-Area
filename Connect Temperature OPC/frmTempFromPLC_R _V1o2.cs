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
    public partial class frmTempFromPLC_R_V1o2 : Form
    {
        public int run = 1;
        public static int item = 0;
        //int underTemp = 177, upperTemp = 183;
        DateTime _dt;
        bool _start = false, _R_start = false, _P_start = false, _A_start = false;
        bool _R_upd = false, _P_upd = false, _A_upd = false;

        string _R01_SV, _R02_SV, _R03_SV, _R04_SV, _R05_SV, _R06_SV, _R07_SV, _R08_SV, _R09_SV, _R10_SV, _R11_SV, _R12_SV, _R13_SV, _R14_SV, _R15_SV, _R16_SV, _R17_SV, _R18_SV;
        string _R01_PV, _R02_PV, _R03_PV, _R04_PV, _R05_PV, _R06_PV, _R07_PV, _R08_PV, _R09_PV, _R10_PV, _R11_PV, _R12_PV, _R13_PV, _R14_PV, _R15_PV, _R16_PV, _R17_PV, _R18_PV;

        string _P01_SV, _P02_SV, _P03_SV, _P04_SV, _P05_SV, _P06_SV, _P07_SV, _P08_SV, _P09_SV, _P10_SV, _P11_SV, _P12_SV;

        string _P01_PV, _P02_PV, _P03_PV, _P04_PV, _P05_PV, _P06_PV, _P07_PV, _P08_PV, _P09_PV, _P10_PV, _P11_PV, _P12_PV;

        string _A01U_SV, _A01L_SV, _A02U_SV, _A02L_SV, _A03U_SV, _A03L_SV, _A04U_SV, _A04L_SV, _A05_SV, _A06_SV;
        string _A01U_PV, _A01L_PV, _A02U_PV, _A02L_PV, _A03U_PV, _A03L_PV, _A04U_PV, _A04L_PV, _A05_PV, _A06_PV;

        public static string shiftsno = "1";
        public static string shiftsnm = "Night";
        public static string workdate = "";
        public static string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        public static DateTime sTime1 = new DateTime();
        public static DateTime sTime2 = new DateTime();

        bool _connect, _R_Connect, _P_Connect, _A_Connect;
        int _itemCount = 66;


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

        public frmTempFromPLC_R_V1o2()
        {
            InitializeComponent();
        }

        private void frmTempFromPLC_R_V1o2_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;


            //_R_Connect = (chk_R_Connect.Checked) ? true : false;
            //_P_Connect = (chk_P_Connect.Checked) ? true : false;
            //_A_Connect = (chk_A_Connect.Checked) ? true : false;

            //_itemCount = 69;
            //_itemCount = 60;    // Rubber + Plastic01 + Plastic03
            //if (_R_Connect == true) { _itemCount = 69; }
            if (_P_Connect == true) { _itemCount = 105; }
            if (_A_Connect == true) { _itemCount = 123; }

            fnConnect();

            fnTemperatureThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //_dt = DateTime.Now;
            ////lbl_Start.Text = (_R_start == true) ? "True" : "False";
            //lbl_DT.Text = String.Format("{0:ss}", _dt);
            //int sec = Convert.ToInt32(_dt.Second);

            //if (sec == 0 && _R_start == true)
            //{
            //    lbl_Start.BackColor = Color.DodgerBlue;
            //    fnSaveRubberDataFile(_R01, _R02, _R03, _R04, _R05, _R06, _R07, _R08, _R09, _R10, _R11, _R12, _R13, _R14, _R15, _R16);
            //}
            //else
            //{
            //    lbl_Start.BackColor = Color.FromKnownColor(KnownColor.Control);
            //}
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

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 1", 1);
                ClientHandles.SetValue(1, 1);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 1", 2);
                ClientHandles.SetValue(2, 2);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 1", 3);
                ClientHandles.SetValue(3, 3);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 2", 4);
                ClientHandles.SetValue(4, 4);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 2", 5);
                ClientHandles.SetValue(5, 5);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 2", 6);
                ClientHandles.SetValue(6, 6);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 3", 7);
                ClientHandles.SetValue(7, 7);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 3", 8);
                ClientHandles.SetValue(8, 8);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 3", 9);
                ClientHandles.SetValue(9, 9);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 4", 10);
                ClientHandles.SetValue(10, 10);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 4", 11);
                ClientHandles.SetValue(11, 11);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 4", 12);
                ClientHandles.SetValue(12, 12);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 5", 13);
                ClientHandles.SetValue(13, 13);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 5", 14);
                ClientHandles.SetValue(14, 14);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 5", 15);
                ClientHandles.SetValue(15, 15);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 6", 16);
                ClientHandles.SetValue(16, 16);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 6", 17);
                ClientHandles.SetValue(17, 17);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 6", 18);
                ClientHandles.SetValue(18, 18);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 7", 19);
                ClientHandles.SetValue(19, 19);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 7", 20);
                ClientHandles.SetValue(20, 20);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 7", 21);
                ClientHandles.SetValue(21, 21);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 8", 22);
                ClientHandles.SetValue(22, 22);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 8", 23);
                ClientHandles.SetValue(23, 23);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 8", 24);
                ClientHandles.SetValue(24, 24);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 9", 25);
                ClientHandles.SetValue(25, 25);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 9", 26);
                ClientHandles.SetValue(26, 26);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 9", 27);
                ClientHandles.SetValue(27, 27);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 10", 28);
                ClientHandles.SetValue(28, 28);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 10", 29);
                ClientHandles.SetValue(29, 29);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 10", 30);
                ClientHandles.SetValue(30, 30);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 11", 31);
                ClientHandles.SetValue(31, 31);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 11", 32);
                ClientHandles.SetValue(32, 32);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 11", 33);
                ClientHandles.SetValue(33, 33);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 12", 34);
                ClientHandles.SetValue(34, 34);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 12", 35);
                ClientHandles.SetValue(35, 35);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 12", 36);
                ClientHandles.SetValue(36, 36);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 13", 37);
                ClientHandles.SetValue(37, 37);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 13", 38);
                ClientHandles.SetValue(38, 38);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 13", 39);
                ClientHandles.SetValue(39, 39);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 14", 40);
                ClientHandles.SetValue(40, 40);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 14", 41);
                ClientHandles.SetValue(41, 41);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 14", 42);
                ClientHandles.SetValue(42, 42);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 15", 43);
                ClientHandles.SetValue(43, 43);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 15", 44);
                ClientHandles.SetValue(44, 44);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 15", 45);
                ClientHandles.SetValue(45, 45);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 16", 46);
                ClientHandles.SetValue(46, 46);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 16", 47);
                ClientHandles.SetValue(47, 47);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 16", 48);
                ClientHandles.SetValue(48, 48);

                ///*********************************/
                ///*********************************/
                ///*********************************/

                //OPCItemIDs.SetValue("Channel1.Device2.PLASTIC 01/SV", 49);
                //ClientHandles.SetValue(49, 49);
                //OPCItemIDs.SetValue("Channel1.Device2.PLASTIC 01/PV", 50);
                //ClientHandles.SetValue(50, 50);
                //OPCItemIDs.SetValue("Channel1.Device2.PLASTIC 01/AL", 51);
                //ClientHandles.SetValue(51, 51);

                //OPCItemIDs.SetValue("Channel1.Device2.PLASTIC 03/SV", 52);
                //ClientHandles.SetValue(52, 52);
                //OPCItemIDs.SetValue("Channel1.Device2.PLASTIC 03/PV", 53);
                //ClientHandles.SetValue(53, 53);
                //OPCItemIDs.SetValue("Channel1.Device2.PLASTIC 03/AL", 54);
                //ClientHandles.SetValue(54, 54);

                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING01_UPPER/SV", 49);
                ClientHandles.SetValue(49, 49);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING01_UPPER/PV", 50);
                ClientHandles.SetValue(50, 50);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING01_UPPER/AL", 51);
                ClientHandles.SetValue(51, 51);

                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING01_LOWER/SV", 52);
                ClientHandles.SetValue(52, 52);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING01_LOWER/PV", 53);
                ClientHandles.SetValue(53, 53);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING01_LOWER/AL", 54);
                ClientHandles.SetValue(54, 54);

                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING02_UPPER/SV", 55);
                ClientHandles.SetValue(55, 55);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING02_UPPER/PV", 56);
                ClientHandles.SetValue(56, 56);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING02_UPPER/AL", 57);
                ClientHandles.SetValue(57, 57);

                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING02_LOWER/SV", 58);
                ClientHandles.SetValue(58, 58);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING02_LOWER/PV", 59);
                ClientHandles.SetValue(59, 59);
                OPCItemIDs.SetValue("Channel1.Device2.ASSEMBLY_WELDING02_LOWER/AL", 60);
                ClientHandles.SetValue(60, 60);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 17", 61);
                ClientHandles.SetValue(61, 61);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 17", 62);
                ClientHandles.SetValue(62, 62);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 17", 63);
                ClientHandles.SetValue(63, 63);

                OPCItemIDs.SetValue("Channel1.Device2.SV/RUBBER 18", 64);
                ClientHandles.SetValue(64, 64);
                OPCItemIDs.SetValue("Channel1.Device2.PV/RUBBER 18", 65);
                ClientHandles.SetValue(65, 65);
                OPCItemIDs.SetValue("Channel1.Device2.ALRM/RUBBER 18", 66);
                ClientHandles.SetValue(66, 66);


                //if (_P_Connect == true)
                //{
                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 1", 70);
                //    ClientHandles.SetValue(70, 70);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 1", 71);
                //    ClientHandles.SetValue(71, 71);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 1", 72);
                //    ClientHandles.SetValue(72, 72);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 2", 73);
                //    ClientHandles.SetValue(73, 73);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 2", 74);
                //    ClientHandles.SetValue(74, 74);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 2", 75);
                //    ClientHandles.SetValue(75, 75);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 3", 76);
                //    ClientHandles.SetValue(76, 76);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 3", 77);
                //    ClientHandles.SetValue(77, 77);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 3", 78);
                //    ClientHandles.SetValue(78, 78);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 4", 79);
                //    ClientHandles.SetValue(79, 79);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 4", 80);
                //    ClientHandles.SetValue(80, 80);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 4", 81);
                //    ClientHandles.SetValue(81, 81);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 5", 82);
                //    ClientHandles.SetValue(82, 82);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 5", 83);
                //    ClientHandles.SetValue(83, 83);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 5", 84);
                //    ClientHandles.SetValue(84, 84);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 6", 85);
                //    ClientHandles.SetValue(85, 85);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 6", 86);
                //    ClientHandles.SetValue(86, 86);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 6", 87);
                //    ClientHandles.SetValue(87, 87);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 7", 88);
                //    ClientHandles.SetValue(88, 88);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 7", 89);
                //    ClientHandles.SetValue(89, 89);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 7", 90);
                //    ClientHandles.SetValue(90, 90);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 8", 91);
                //    ClientHandles.SetValue(91, 91);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 8", 92);
                //    ClientHandles.SetValue(92, 92);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 8", 93);
                //    ClientHandles.SetValue(93, 93);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 9", 94);
                //    ClientHandles.SetValue(94, 94);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 9", 95);
                //    ClientHandles.SetValue(95, 95);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 9", 96);
                //    ClientHandles.SetValue(96, 96);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 10", 97);
                //    ClientHandles.SetValue(97, 97);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 10", 98);
                //    ClientHandles.SetValue(98, 98);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 10", 99);
                //    ClientHandles.SetValue(99, 99);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 11", 100);
                //    ClientHandles.SetValue(100, 100);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 11", 101);
                //    ClientHandles.SetValue(101, 101);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 11", 102);
                //    ClientHandles.SetValue(102, 102);

                //    OPCItemIDs.SetValue("Channel1.Device3.SV/PLASTIC 12", 103);
                //    ClientHandles.SetValue(103, 103);
                //    OPCItemIDs.SetValue("Channel1.Device3.PV/PLASTIC 12", 104);
                //    ClientHandles.SetValue(104, 104);
                //    OPCItemIDs.SetValue("Channel1.Device3.ALRM/PLASTIC 12", 105);
                //    ClientHandles.SetValue(105, 105);
                //}

                /////*********************************/
                /////*********************************/
                /////*********************************/

                //if (_A_Connect == true)
                //{
                //    OPCItemIDs.SetValue("Channel1.Device4.SV/WELDING 1", 106);
                //    ClientHandles.SetValue(106, 106);
                //    OPCItemIDs.SetValue("Channel1.Device4.PV/WELDING 1", 107);
                //    ClientHandles.SetValue(107, 107);
                //    OPCItemIDs.SetValue("Channel1.Device4.ALRM/WELDING 1", 108);
                //    ClientHandles.SetValue(108, 108);

                //    OPCItemIDs.SetValue("Channel1.Device4.SV/WELDING 2", 109);
                //    ClientHandles.SetValue(109, 109);
                //    OPCItemIDs.SetValue("Channel1.Device4.PV/WELDING 2", 110);
                //    ClientHandles.SetValue(110, 110);
                //    OPCItemIDs.SetValue("Channel1.Device4.ALRM/WELDING 2", 111);
                //    ClientHandles.SetValue(111, 111);

                //    OPCItemIDs.SetValue("Channel1.Device4.SV/ABALANCE 1", 112);
                //    ClientHandles.SetValue(112, 112);
                //    OPCItemIDs.SetValue("Channel1.Device4.PV/ABALANCE 1", 113);
                //    ClientHandles.SetValue(113, 113);
                //    OPCItemIDs.SetValue("Channel1.Device4.ALRM/ABALANCE 1", 114);
                //    ClientHandles.SetValue(114, 114);

                //    OPCItemIDs.SetValue("Channel1.Device4.SV/ABALANCE 2", 115);
                //    ClientHandles.SetValue(115, 115);
                //    OPCItemIDs.SetValue("Channel1.Device4.PV/ABALANCE 2", 116);
                //    ClientHandles.SetValue(116, 116);
                //    OPCItemIDs.SetValue("Channel1.Device4.ALRM/ABALANCE 2", 117);
                //    ClientHandles.SetValue(117, 117);

                //    OPCItemIDs.SetValue("Channel1.Device4.SV/BLOWER 1", 118);
                //    ClientHandles.SetValue(118, 118);
                //    OPCItemIDs.SetValue("Channel1.Device4.PV/BLOWER 1", 119);
                //    ClientHandles.SetValue(119, 119);
                //    OPCItemIDs.SetValue("Channel1.Device4.ALRM/BLOWER 1", 120);
                //    ClientHandles.SetValue(120, 120);

                //    OPCItemIDs.SetValue("Channel1.Device4.SV/BLOWER 2", 121);
                //    ClientHandles.SetValue(121, 121);
                //    OPCItemIDs.SetValue("Channel1.Device4.PV/BLOWER 2", 122);
                //    ClientHandles.SetValue(122, 122);
                //    OPCItemIDs.SetValue("Channel1.Device4.ALRM/BLOWER 2", 123);
                //    ClientHandles.SetValue(123, 123);
                //}

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
        }

        private void ObjOPCGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 1) { lbl_R01_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 2) { lbl_R01_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 3) { lbl_R01_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R01 = Convert.ToInt32(lbl_R01_AL.Text); lbl_R01_AL.BackColor = (tempPV_R01 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 4) { lbl_R02_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 5) { lbl_R02_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 6) { lbl_R02_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R02 = Convert.ToInt32(lbl_R02_AL.Text); lbl_R02_AL.BackColor = (tempPV_R02 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 7) { lbl_R03_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 8) { lbl_R03_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 9) { lbl_R03_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R03 = Convert.ToInt32(lbl_R03_AL.Text); lbl_R03_AL.BackColor = (tempPV_R03 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 10) { lbl_R04_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 11) { lbl_R04_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 12) { lbl_R04_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R04 = Convert.ToInt32(lbl_R04_AL.Text); lbl_R04_AL.BackColor = (tempPV_R04 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 13) { lbl_R05_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 14) { lbl_R05_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 15) { lbl_R05_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R05 = Convert.ToInt32(lbl_R05_AL.Text); lbl_R05_AL.BackColor = (tempPV_R05 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 16) { lbl_R06_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 17) { lbl_R06_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 18) { lbl_R06_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R06 = Convert.ToInt32(lbl_R06_AL.Text); lbl_R06_AL.BackColor = (tempPV_R06 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 19) { lbl_R07_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 20) { lbl_R07_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 21) { lbl_R07_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R07 = Convert.ToInt32(lbl_R07_AL.Text); lbl_R07_AL.BackColor = (tempPV_R07 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 22) { lbl_R08_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 23) { lbl_R08_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 24) { lbl_R08_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R08 = Convert.ToInt32(lbl_R08_AL.Text); lbl_R08_AL.BackColor = (tempPV_R08 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 25) { lbl_R09_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 26) { lbl_R09_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 27) { lbl_R09_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R09 = Convert.ToInt32(lbl_R09_AL.Text); lbl_R09_AL.BackColor = (tempPV_R09 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 28) { lbl_R10_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 29) { lbl_R10_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 30) { lbl_R10_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R10 = Convert.ToInt32(lbl_R10_AL.Text); lbl_R10_AL.BackColor = (tempPV_R10 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 31) { lbl_R11_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 32) { lbl_R11_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 33) { lbl_R11_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R11 = Convert.ToInt32(lbl_R11_AL.Text); lbl_R11_AL.BackColor = (tempPV_R11 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 34) { lbl_R12_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 35) { lbl_R12_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 36) { lbl_R12_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R12 = Convert.ToInt32(lbl_R12_AL.Text); lbl_R12_AL.BackColor = (tempPV_R12 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 37) { lbl_R13_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 38) { lbl_R13_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 39) { lbl_R13_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R13 = Convert.ToInt32(lbl_R13_AL.Text); lbl_R13_AL.BackColor = (tempPV_R13 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 40) { lbl_R14_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 41) { lbl_R14_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 42) { lbl_R14_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R14 = Convert.ToInt32(lbl_R14_AL.Text); lbl_R14_AL.BackColor = (tempPV_R14 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 43) { lbl_R15_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 44) { lbl_R15_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 45) { lbl_R15_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R15 = Convert.ToInt32(lbl_R15_AL.Text); lbl_R15_AL.BackColor = (tempPV_R15 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 46) { lbl_R16_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 47) { lbl_R16_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 48) { lbl_R16_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R16 = Convert.ToInt32(lbl_R16_AL.Text); lbl_R16_AL.BackColor = (tempPV_R16 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                /*************************************/
                /*************************************/
                /*************************************/

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 49) { lbl_A01U_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 50) { lbl_A01U_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 51) { lbl_A01U_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_P01U = Convert.ToInt32(lbl_A01U_AL.Text); lbl_A01U_AL.BackColor = (tempPV_P01U == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 52) { lbl_A01L_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 53) { lbl_A01L_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 54) { lbl_A01L_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_P01L = Convert.ToInt32(lbl_A01L_AL.Text); lbl_A01L_AL.BackColor = (tempPV_P01L == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 55) { lbl_A02U_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 56) { lbl_A02U_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 57) { lbl_A02U_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_P02U = Convert.ToInt32(lbl_A02U_AL.Text); lbl_A02U_AL.BackColor = (tempPV_P02U == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 58) { lbl_A02L_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 59) { lbl_A02L_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 60) { lbl_A02L_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_P02L = Convert.ToInt32(lbl_A02L_AL.Text); lbl_A02L_AL.BackColor = (tempPV_P02L == 0) ? Color.LightGreen : Color.Pink; }  //AL

                /*************************************/
                /*************************************/
                /*************************************/

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 61) { lbl_R17_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 62) { lbl_R17_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 63) { lbl_R17_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R17 = Convert.ToInt32(lbl_R17_AL.Text); lbl_R17_AL.BackColor = (tempPV_R17 == 0) ? Color.LightGreen : Color.Pink; }  //AL

                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 64) { lbl_R18_SV.Text = ItemValues.GetValue(i).ToString(); }      //SV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 65) { lbl_R18_PV.Text = ItemValues.GetValue(i).ToString(); }      //PV
                if (Convert.ToInt32(ClientHandles.GetValue(i)) == 66) { lbl_R18_AL.Text = (Convert.ToBoolean(ItemValues.GetValue(i)) == true) ? "1" : "0"; int tempPV_R18 = Convert.ToInt32(lbl_R18_AL.Text); lbl_R18_AL.BackColor = (tempPV_R18 == 0) ? Color.LightGreen : Color.Pink; }  //AL

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

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void frmTempFromPLC_R_V1o2_Resize(object sender, EventArgs e)
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            //fnUpdateRubberTemp();
        }

        public void fnUpdateTemp()
        {
            string R01_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R01_SV.Text));
            string R01_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R01_PV.Text));
            string R01_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R01_AL.Text));

            string R02_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R02_SV.Text));
            string R02_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R02_PV.Text));
            string R02_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R02_AL.Text));

            string R03_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R03_SV.Text));
            string R03_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R03_PV.Text));
            string R03_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R03_AL.Text));

            string R04_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R04_SV.Text));
            string R04_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R04_PV.Text));
            string R04_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R04_AL.Text));

            string R05_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R05_SV.Text));
            string R05_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R05_PV.Text));
            string R05_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R05_AL.Text));

            string R06_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R06_SV.Text));
            string R06_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R06_PV.Text));
            string R06_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R06_AL.Text));

            string R07_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R07_SV.Text));
            string R07_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R07_PV.Text));
            string R07_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R07_AL.Text));

            string R08_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R08_SV.Text));
            string R08_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R08_PV.Text));
            string R08_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R08_AL.Text));

            string R09_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R09_SV.Text));
            string R09_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R09_PV.Text));
            string R09_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R09_AL.Text));

            string R10_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R10_SV.Text));
            string R10_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R10_PV.Text));
            string R10_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R10_AL.Text));

            string R11_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R11_SV.Text));
            string R11_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R11_PV.Text));
            string R11_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R11_AL.Text));

            string R12_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R12_SV.Text));
            string R12_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R12_PV.Text));
            string R12_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R12_AL.Text));

            string R13_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R13_SV.Text));
            string R13_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R13_PV.Text));
            string R13_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R13_AL.Text));

            string R14_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R14_SV.Text));
            string R14_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R14_PV.Text));
            string R14_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R14_AL.Text));

            string R15_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R15_SV.Text));
            string R15_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R15_PV.Text));
            string R15_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R15_AL.Text));

            string R16_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R16_SV.Text));
            string R16_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R16_PV.Text));
            string R16_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R16_AL.Text));

            string R17_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R17_SV.Text));
            string R17_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R17_PV.Text));
            string R17_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R17_AL.Text));

            string R18_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_R18_SV.Text));
            string R18_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_R18_PV.Text));
            string R18_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_R18_AL.Text));

            string P01_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P01_SV.Text));
            string P01_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P01_PV.Text));
            string P01_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P01_AL.Text));

            string P02_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P02_SV.Text));
            string P02_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P02_PV.Text));
            string P02_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P02_AL.Text));

            string P03_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P03_SV.Text));
            string P03_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P03_PV.Text));
            string P03_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P03_AL.Text));

            string P04_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P04_SV.Text));
            string P04_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P04_PV.Text));
            string P04_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P04_AL.Text));

            string P05_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P05_SV.Text));
            string P05_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P05_PV.Text));
            string P05_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P05_AL.Text));

            string P06_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P06_SV.Text));
            string P06_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P06_PV.Text));
            string P06_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P06_AL.Text));

            string P07_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P07_SV.Text));
            string P07_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P07_PV.Text));
            string P07_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P07_AL.Text));

            string P08_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P08_SV.Text));
            string P08_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P08_PV.Text));
            string P08_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P08_AL.Text));

            string P09_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P09_SV.Text));
            string P09_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P09_PV.Text));
            string P09_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P09_AL.Text));

            string P10_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P10_SV.Text));
            string P10_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P10_PV.Text));
            string P10_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P10_AL.Text));

            string P11_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P11_SV.Text));
            string P11_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P11_PV.Text));
            string P11_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P11_AL.Text));

            string P12_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_P12_SV.Text));
            string P12_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_P12_PV.Text));
            string P12_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_P12_AL.Text));

            string A01U_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A01U_SV.Text));
            string A01U_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A01U_PV.Text));
            string A01U_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A01U_AL.Text));

            string A01L_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A01L_SV.Text));
            string A01L_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A01L_PV.Text));
            string A01L_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A01L_AL.Text));

            string A02U_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A02U_SV.Text));
            string A02U_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A02U_PV.Text));
            string A02U_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A02U_AL.Text));

            string A02L_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A02L_SV.Text));
            string A02L_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A02L_PV.Text));
            string A02L_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A02L_AL.Text));

            string A03U_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A03U_SV.Text));
            string A03U_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A03U_PV.Text));
            string A03U_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A03U_AL.Text));

            string A03L_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A03L_SV.Text));
            string A03L_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A03L_PV.Text));
            string A03L_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A03L_AL.Text));

            string A04U_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A04U_SV.Text));
            string A04U_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A04U_PV.Text));
            string A04U_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A04U_AL.Text));

            string A04L_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A04L_SV.Text));
            string A04L_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A04L_PV.Text));
            string A04L_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A04L_AL.Text));

            string A05_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A05_SV.Text));
            string A05_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A05_PV.Text));
            string A05_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A05_AL.Text));

            string A06_SV = String.Format("{0:0000}", Convert.ToInt32(lbl_A06_SV.Text));
            string A06_PV = String.Format("{0:0000}", Convert.ToInt32(lbl_A06_PV.Text));
            string A06_AL = String.Format("{0:0000}", Convert.ToInt32(lbl_A06_AL.Text));

            string temp_SV = R01_SV + R02_SV + R03_SV + R04_SV + R05_SV + R06_SV + R07_SV + R08_SV + R09_SV + R10_SV + R11_SV + R12_SV + R13_SV + R14_SV + R15_SV + R16_SV + R17_SV + R18_SV
                            + P01_SV + P02_SV + P03_SV + P04_SV + P05_SV + P06_SV + P07_SV + P08_SV + P09_SV + P10_SV + P11_SV + P12_SV
                            + A01U_SV + A01L_SV + A02U_SV + A02L_SV + A03U_SV + A03L_SV + A04U_SV + A04L_SV + A05_SV + A06_SV;

            string temp_PV = R01_PV + R02_PV + R03_PV + R04_PV + R05_PV + R06_PV + R07_PV + R08_PV + R09_PV + R10_PV + R11_PV + R12_PV + R13_PV + R14_PV + R15_PV + R16_PV + R17_PV + R18_PV
                            + P01_PV + P02_PV + P03_PV + P04_PV + P05_PV + P06_PV + P07_PV + P08_PV + P09_PV + P10_PV + P11_PV + P12_PV
                            + A01U_PV + A01L_PV + A02U_PV + A02L_PV + A03U_PV + A03L_PV + A04U_PV + A04L_PV + A05_PV + A06_PV;

            string temp_AL = R01_AL + R02_AL + R03_AL + R04_AL + R05_AL + R06_AL + R07_AL + R08_AL + R09_AL + R10_AL + R11_AL + R12_AL + R13_AL + R14_AL + R15_AL + R16_AL + R17_AL + R18_AL
                            + P01_AL + P02_AL + P03_AL + P04_AL + P05_AL + P06_AL + P07_AL + P08_AL + P09_AL + P10_AL + P11_AL + P12_AL
                            + A01U_AL + A01L_AL + A02U_AL + A02L_AL + A03U_AL + A03L_AL + A04U_AL + A04L_AL + A05_AL + A06_AL;

            string sql = "V2o1_ERP_Temperature_Garthering_UpdItem_V1o1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.VarChar;
            sParams[0].ParameterName = "@sv";
            sParams[0].Value = temp_SV;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@pv";
            sParams[1].Value = temp_PV;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.VarChar;
            sParams[2].ParameterName = "@al";
            sParams[2].Value = temp_AL;

            //sParams[3] = new SqlParameter();
            //sParams[3].SqlDbType = SqlDbType.TinyInt;
            //sParams[3].ParameterName = "@ln";
            //sParams[3].Value = 1;

            cls.fnUpdDel(sql, sParams);

            _R01_SV = R01_SV; _R02_SV = R02_SV; _R03_SV = R03_SV; _R04_SV = R04_SV; _R05_SV = R05_SV; _R06_SV = R06_SV; _R07_SV = R07_SV; _R08_SV = R08_SV; _R09_SV = R09_SV; _R10_SV = R10_SV; _R11_SV = R11_SV; _R12_SV = R12_SV; _R13_SV = R13_SV; _R14_SV = R14_SV; _R15_SV = R15_SV; _R16_SV = R16_SV; _R17_SV = R17_SV; _R18_SV = R18_SV;
            _R01_PV = R01_PV; _R02_PV = R02_PV; _R03_PV = R03_PV; _R04_PV = R04_PV; _R05_PV = R05_PV; _R06_PV = R06_PV; _R07_PV = R07_PV; _R08_PV = R08_PV; _R09_PV = R09_PV; _R10_PV = R10_PV; _R11_PV = R11_PV; _R12_PV = R12_PV; _R13_PV = R13_PV; _R14_PV = R14_PV; _R15_PV = R15_PV; _R16_PV = R16_PV; _R17_PV = R17_PV; _R18_PV = R18_PV;

            _P01_SV = P01_SV; _P02_SV = P02_SV; _P03_SV = P03_SV; _P04_SV = P04_SV; _P05_SV = P05_SV; _P06_SV = P06_SV; _P07_SV = P07_SV; _P08_SV = P08_SV; _P09_SV = P09_SV; _P10_SV = P10_SV; _P11_SV = P11_SV; _P12_SV = P12_SV;
            _P01_PV = P01_PV; _P02_PV = P02_PV; _P03_PV = P03_PV; _P04_PV = P04_PV; _P05_PV = P05_PV; _P06_PV = P06_PV; _P07_PV = P07_PV; _P08_PV = P08_PV; _P09_PV = P09_PV; _P10_PV = P10_PV; _P11_PV = P11_PV; _P12_PV = P12_PV;

            _A01U_SV = A01U_SV; _A01L_SV = A01L_SV; _A02U_SV = A02U_SV; _A02L_SV = A02L_SV; _A03U_SV = A03U_SV; _A03L_SV = A03L_SV; _A04U_SV = A04U_SV; _A04L_SV = A04L_SV; _A05_SV = A05_SV; _A06_SV = A06_SV;
            _A01U_PV = A01U_PV; _A01L_PV = A01L_PV; _A02U_PV = A02U_PV; _A02L_PV = A02L_PV; _A03U_PV = A03U_PV; _A03L_PV = A03L_PV; _A04U_PV = A04U_PV; _A04L_PV = A04L_PV; _A05_PV = A05_PV; _A06_PV = A06_PV;

            _start = true;

            //fnSaveRubberDataFile(R01_PV, R02_PV, R03_PV, R04_PV, R05_PV, R06_PV, R07_PV, R08_PV, R09_PV, R10_PV, R11_PV, R12_PV, R13_PV, R14_PV, R15_PV, R16_PV);
        }

        public void fnSaveDataFile()
        {
            if (_connect == true)
            {
                int R01SV, R02SV, R03SV, R04SV, R05SV, R06SV, R07SV, R08SV, R09SV, R10SV, R11SV, R12SV, R13SV, R14SV, R15SV, R16SV, R17SV, R18SV;
                int R01PV, R02PV, R03PV, R04PV, R05PV, R06PV, R07PV, R08PV, R09PV, R10PV, R11PV, R12PV, R13PV, R14PV, R15PV, R16PV, R17PV, R18PV;

                int P01SV, P02SV, P03SV, P04SV, P05SV, P06SV, P07SV, P08SV, P09SV, P10SV, P11SV, P12SV;
                int P01PV, P02PV, P03PV, P04PV, P05PV, P06PV, P07PV, P08PV, P09PV, P10PV, P11PV, P12PV;

                int A01USV, A01LSV, A02USV, A02LSV, A03USV, A03LSV, A04USV, A04LSV, A05SV, A06SV;
                int A01UPV, A01LPV, A02UPV, A02LPV, A03UPV, A03LPV, A04UPV, A04LPV, A05PV, A06PV;

                DateTime now = _dt;
                string value = "";
                string filename = "";
                string prefix_Year = now.Year.ToString();
                string prefix_Month = String.Format("{0:00}", now.Month);
                string prefix_Day = String.Format("{0:00}", now.Day);

                string path = @"F:\SQLExpress\Temperatures_Monitoring\" + prefix_Year + @"\" + prefix_Year + prefix_Month + @"\" + prefix_Year + prefix_Month + prefix_Day + @"\";

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                if (_R_upd == true)
                {
                    R01SV = (_R01_SV.Length > 0) ? Convert.ToInt32(_R01_SV) : 0;
                    R02SV = (_R02_SV.Length > 0) ? Convert.ToInt32(_R02_SV) : 0;
                    R03SV = (_R03_SV.Length > 0) ? Convert.ToInt32(_R03_SV) : 0;
                    R04SV = (_R04_SV.Length > 0) ? Convert.ToInt32(_R04_SV) : 0;
                    R05SV = (_R05_SV.Length > 0) ? Convert.ToInt32(_R05_SV) : 0;
                    R06SV = (_R06_SV.Length > 0) ? Convert.ToInt32(_R06_SV) : 0;
                    R07SV = (_R07_SV.Length > 0) ? Convert.ToInt32(_R07_SV) : 0;
                    R08SV = (_R08_SV.Length > 0) ? Convert.ToInt32(_R08_SV) : 0;
                    R09SV = (_R09_SV.Length > 0) ? Convert.ToInt32(_R09_SV) : 0;
                    R10SV = (_R10_SV.Length > 0) ? Convert.ToInt32(_R10_SV) : 0;
                    R11SV = (_R11_SV.Length > 0) ? Convert.ToInt32(_R11_SV) : 0;
                    R12SV = (_R12_SV.Length > 0) ? Convert.ToInt32(_R12_SV) : 0;
                    R13SV = (_R13_SV.Length > 0) ? Convert.ToInt32(_R13_SV) : 0;
                    R14SV = (_R14_SV.Length > 0) ? Convert.ToInt32(_R14_SV) : 0;
                    R15SV = (_R15_SV.Length > 0) ? Convert.ToInt32(_R15_SV) : 0;
                    R16SV = (_R16_SV.Length > 0) ? Convert.ToInt32(_R16_SV) : 0;
                    R17SV = (_R17_SV.Length > 0) ? Convert.ToInt32(_R17_SV) : 0;
                    R18SV = (_R18_SV.Length > 0) ? Convert.ToInt32(_R18_SV) : 0;

                    R01PV = (_R01_PV.Length > 0) ? Convert.ToInt32(_R01_PV) : 0;
                    R02PV = (_R02_PV.Length > 0) ? Convert.ToInt32(_R02_PV) : 0;
                    R03PV = (_R03_PV.Length > 0) ? Convert.ToInt32(_R03_PV) : 0;
                    R04PV = (_R04_PV.Length > 0) ? Convert.ToInt32(_R04_PV) : 0;
                    R05PV = (_R05_PV.Length > 0) ? Convert.ToInt32(_R05_PV) : 0;
                    R06PV = (_R06_PV.Length > 0) ? Convert.ToInt32(_R06_PV) : 0;
                    R07PV = (_R07_PV.Length > 0) ? Convert.ToInt32(_R07_PV) : 0;
                    R08PV = (_R08_PV.Length > 0) ? Convert.ToInt32(_R08_PV) : 0;
                    R09PV = (_R09_PV.Length > 0) ? Convert.ToInt32(_R09_PV) : 0;
                    R10PV = (_R10_PV.Length > 0) ? Convert.ToInt32(_R10_PV) : 0;
                    R11PV = (_R11_PV.Length > 0) ? Convert.ToInt32(_R11_PV) : 0;
                    R12PV = (_R12_PV.Length > 0) ? Convert.ToInt32(_R12_PV) : 0;
                    R13PV = (_R13_PV.Length > 0) ? Convert.ToInt32(_R13_PV) : 0;
                    R14PV = (_R14_PV.Length > 0) ? Convert.ToInt32(_R14_PV) : 0;
                    R15PV = (_R15_PV.Length > 0) ? Convert.ToInt32(_R15_PV) : 0;
                    R16PV = (_R16_PV.Length > 0) ? Convert.ToInt32(_R16_PV) : 0;
                    R17PV = (_R17_PV.Length > 0) ? Convert.ToInt32(_R17_PV) : 0;
                    R18PV = (_R18_PV.Length > 0) ? Convert.ToInt32(_R18_PV) : 0;

                    for (int r = 1; r <= 18; r++)
                    {
                        switch (r)
                        {
                            case 1:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R01SV) + " " + String.Format("{0:0000}", R01PV);
                                break;
                            case 2:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R02SV) + " " + String.Format("{0:0000}", R02PV);
                                break;
                            case 3:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R03SV) + " " + String.Format("{0:0000}", R03PV);
                                break;
                            case 4:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R04SV) + " " + String.Format("{0:0000}", R04PV);
                                break;
                            case 5:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R05SV) + " " + String.Format("{0:0000}", R05PV);
                                break;
                            case 6:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R06SV) + " " + String.Format("{0:0000}", R06PV);
                                break;
                            case 7:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R07SV) + " " + String.Format("{0:0000}", R07PV);
                                break;
                            case 8:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R08SV) + " " + String.Format("{0:0000}", R08PV);
                                break;
                            case 9:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R09SV) + " " + String.Format("{0:0000}", R09PV);
                                break;
                            case 10:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R10SV) + " " + String.Format("{0:0000}", R10PV);
                                break;
                            case 11:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R11SV) + " " + String.Format("{0:0000}", R11PV);
                                break;
                            case 12:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R12SV) + " " + String.Format("{0:0000}", R12PV);
                                break;
                            case 13:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R13SV) + " " + String.Format("{0:0000}", R13PV);
                                break;
                            case 14:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R14SV) + " " + String.Format("{0:0000}", R14PV);
                                break;
                            case 15:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R15SV) + " " + String.Format("{0:0000}", R15PV);
                                break;
                            case 16:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R16SV) + " " + String.Format("{0:0000}", R16PV);
                                break;
                            case 17:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R17SV) + " " + String.Format("{0:0000}", R17PV);
                                break;
                            case 18:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", R18SV) + " " + String.Format("{0:0000}", R18PV);
                                break;
                        }
                        filename = "RUBBER" + String.Format("{0:00}", r) + ".txt";
                        Fnc_WriteData(path, filename, value);
                    }

                    //_R_upd = false;
                }

                if (_P_upd == true)
                {
                    P01SV = (_P01_SV.Length > 0) ? Convert.ToInt32(_P01_SV) : 0;
                    P02SV = (_P02_SV.Length > 0) ? Convert.ToInt32(_P02_SV) : 0;
                    P03SV = (_P03_SV.Length > 0) ? Convert.ToInt32(_P03_SV) : 0;
                    P04SV = (_P04_SV.Length > 0) ? Convert.ToInt32(_P04_SV) : 0;
                    P05SV = (_P05_SV.Length > 0) ? Convert.ToInt32(_P05_SV) : 0;
                    P06SV = (_P06_SV.Length > 0) ? Convert.ToInt32(_P06_SV) : 0;
                    P07SV = (_P07_SV.Length > 0) ? Convert.ToInt32(_P07_SV) : 0;
                    P08SV = (_P08_SV.Length > 0) ? Convert.ToInt32(_P08_SV) : 0;
                    P09SV = (_P09_SV.Length > 0) ? Convert.ToInt32(_P09_SV) : 0;
                    P10SV = (_P10_SV.Length > 0) ? Convert.ToInt32(_P10_SV) : 0;
                    P11SV = (_P11_SV.Length > 0) ? Convert.ToInt32(_P11_SV) : 0;
                    P12SV = (_P12_SV.Length > 0) ? Convert.ToInt32(_P12_SV) : 0;

                    P01PV = (_P01_PV.Length > 0) ? Convert.ToInt32(_P01_PV) : 0;
                    P02PV = (_P02_PV.Length > 0) ? Convert.ToInt32(_P02_PV) : 0;
                    P03PV = (_P03_PV.Length > 0) ? Convert.ToInt32(_P03_PV) : 0;
                    P04PV = (_P04_PV.Length > 0) ? Convert.ToInt32(_P04_PV) : 0;
                    P05PV = (_P05_PV.Length > 0) ? Convert.ToInt32(_P05_PV) : 0;
                    P06PV = (_P06_PV.Length > 0) ? Convert.ToInt32(_P06_PV) : 0;
                    P07PV = (_P07_PV.Length > 0) ? Convert.ToInt32(_P07_PV) : 0;
                    P08PV = (_P08_PV.Length > 0) ? Convert.ToInt32(_P08_PV) : 0;
                    P09PV = (_P09_PV.Length > 0) ? Convert.ToInt32(_P09_PV) : 0;
                    P10PV = (_P10_PV.Length > 0) ? Convert.ToInt32(_P10_PV) : 0;
                    P11PV = (_P11_PV.Length > 0) ? Convert.ToInt32(_P11_PV) : 0;
                    P12PV = (_P12_PV.Length > 0) ? Convert.ToInt32(_P12_PV) : 0;

                    for (int p = 1; p <= 12; p++)
                    {
                        switch (p)
                        {
                            case 1:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P01SV) + " " + String.Format("{0:0000}", P01PV);
                                break;
                            case 2:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P02SV) + " " + String.Format("{0:0000}", P02PV);
                                break;
                            case 3:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P03SV) + " " + String.Format("{0:0000}", P03PV);
                                break;
                            case 4:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P04SV) + " " + String.Format("{0:0000}", P04PV);
                                break;
                            case 5:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P05SV) + " " + String.Format("{0:0000}", P05PV);
                                break;
                            case 6:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P06SV) + " " + String.Format("{0:0000}", P06PV);
                                break;
                            case 7:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P07SV) + " " + String.Format("{0:0000}", P07PV);
                                break;
                            case 8:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P08SV) + " " + String.Format("{0:0000}", P08PV);
                                break;
                            case 9:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P09SV) + " " + String.Format("{0:0000}", P09PV);
                                break;
                            case 10:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P10SV) + " " + String.Format("{0:0000}", P10PV);
                                break;
                            case 11:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P11SV) + " " + String.Format("{0:0000}", P11PV);
                                break;
                            case 12:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", P12SV) + " " + String.Format("{0:0000}", P12PV);
                                break;
                        }
                        filename = "PLASTIC" + String.Format("{0:00}", p) + ".txt";
                        Fnc_WriteData(path, filename, value);
                    }

                    //_P_upd = false;
                }

                if (_A_upd == true)
                {

                    A01USV = (_A01U_SV.Length > 0) ? Convert.ToInt32(_A01U_SV) : 0;
                    A01LSV = (_A01L_SV.Length > 0) ? Convert.ToInt32(_A01L_SV) : 0;
                    A02USV = (_A02U_SV.Length > 0) ? Convert.ToInt32(_A02U_SV) : 0;
                    A02LSV = (_A02L_SV.Length > 0) ? Convert.ToInt32(_A02L_SV) : 0;
                    A03USV = (_A03U_SV.Length > 0) ? Convert.ToInt32(_A03U_SV) : 0;
                    A03LSV = (_A03L_SV.Length > 0) ? Convert.ToInt32(_A03L_SV) : 0;
                    A04USV = (_A04U_SV.Length > 0) ? Convert.ToInt32(_A04U_SV) : 0;
                    A04LSV = (_A04L_SV.Length > 0) ? Convert.ToInt32(_A04L_SV) : 0;
                    A05SV = (_A03U_SV.Length > 0) ? Convert.ToInt32(_A03U_SV) : 0;
                    A06SV = (_A03L_SV.Length > 0) ? Convert.ToInt32(_A03L_SV) : 0;

                    A01UPV = (_A01U_PV.Length > 0) ? Convert.ToInt32(_A01U_PV) : 0;
                    A01LPV = (_A01L_PV.Length > 0) ? Convert.ToInt32(_A01L_PV) : 0;
                    A02UPV = (_A02U_PV.Length > 0) ? Convert.ToInt32(_A02U_PV) : 0;
                    A02LPV = (_A02L_PV.Length > 0) ? Convert.ToInt32(_A02L_PV) : 0;
                    A03UPV = (_A03U_PV.Length > 0) ? Convert.ToInt32(_A03U_PV) : 0;
                    A03LPV = (_A03L_PV.Length > 0) ? Convert.ToInt32(_A03L_PV) : 0;
                    A04UPV = (_A04U_PV.Length > 0) ? Convert.ToInt32(_A04U_PV) : 0;
                    A04LPV = (_A04L_PV.Length > 0) ? Convert.ToInt32(_A04L_PV) : 0;
                    A05PV = (_A03U_PV.Length > 0) ? Convert.ToInt32(_A03U_PV) : 0;
                    A06PV = (_A03L_PV.Length > 0) ? Convert.ToInt32(_A03L_PV) : 0;

                    for (int a = 1; a <= 10; a++)
                    {
                        switch (a)
                        {
                            case 1:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A01USV) + " " + String.Format("{0:0000}", A01UPV);
                                break;
                            case 2:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A01LSV) + " " + String.Format("{0:0000}", A01LPV);
                                break;
                            case 3:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A02USV) + " " + String.Format("{0:0000}", A02UPV);
                                break;
                            case 4:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A02LSV) + " " + String.Format("{0:0000}", A02LPV);
                                break;
                            case 5:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A03USV) + " " + String.Format("{0:0000}", A03UPV);
                                break;
                            case 6:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A03LSV) + " " + String.Format("{0:0000}", A03LPV);
                                break;
                            case 7:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A04USV) + " " + String.Format("{0:0000}", A04UPV);
                                break;
                            case 8:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A04LSV) + " " + String.Format("{0:0000}", A04LPV);
                                break;
                            case 9:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A05SV) + " " + String.Format("{0:0000}", A05PV);
                                break;
                            case 10:
                                value = now.ToString("dd/MM/yyyy HH:mm:ss") + " " + String.Format("{0:0000}", A06SV) + " " + String.Format("{0:0000}", A06PV);
                                break;
                        }
                        filename = "ASSEMBLY" + String.Format("{0:00}", a) + ".txt";
                        Fnc_WriteData(path, filename, value);
                    }

                    //_A_upd = false;
                }
            }
        }

        public void Fnc_WriteData(string path,string filename,string value)
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

        public static string fnGetDate(string format)
        {
            string s = "";

            DateTime nNow = DateTime.Now;
            //sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");

            //button2.Text = sNow;//DateTime.Now.TimeOfDay.ToString();

            if (DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00"))
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0).AddDays(-1);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else if (nNow.TimeOfDay >= TimeSpan.Parse("20:00:00"))
            {

                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0).AddDays(1);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
                shiftsnm = "Day";
                shiftsno = "1";
            }
            // sTime1 = sTime1.AddDays(-2);
            //workdate = sTime1.ToString("yyyyMMdd");
            //button1.Text = sTime1.ToString("yyyy/MM/dd") + " " + shiftsnm;
            switch (format)
            {
                case "d":   //Date: 09/10/2017
                    s = nNow.ToString("dd/MM/yyyy");
                    break;
                case "dt":  //Date time: 09/10/2017 19:36:22
                    s = nNow.ToString("dd/MM/yyyy HH:mm:ss");
                    break;
                case "t":   //Time: 19:36:22
                    s = nNow.ToString("HH:mm:ss");
                    break;
                case "sd":  //Shift date: Day(Night) 09/10/2017
                    s = (shiftsno == "1") ? (shiftsnm + " " + nNow.ToString("dd/MM/yyyy")) : (shiftsnm + " " + nNow.AddDays(-1).ToString("dd/MM/yyyy"));
                    break;
                case "SD":  // Shift date: DAY(NIGHT) 09/10/2017
                    s = (shiftsno == "1") ? (shiftsnm.ToUpper() + " " + nNow.ToString("dd/MM/yyyy")) : (shiftsnm.ToUpper() + " " + nNow.AddDays(-1).ToString("dd/MM/yyyy"));
                    break;
                case "ct":  //Country time: Vina 19:36:22
                    s = "Vina " + nNow.ToString("HH:mm:ss");
                    break;
                case "CT":  // Country time: VINA 19:36:22
                    s = "VINA " + nNow.ToString("HH:mm:ss");
                    break;
                case "s":   // Shift: Day/Night
                    s = shiftsnm;
                    break;
                case "S":   // Shift (capital): DAY/NIGHT
                    s = shiftsnm.ToUpper();
                    break;
                case "sn":  // Shift number: 1-Day; 2-Night
                    s = shiftsno;
                    break;
                case "lot": // LOT date: 20171009
                    s = (shiftsno == "1") ? nNow.ToString("yyyyMMdd") : nNow.AddDays(-1).ToString("yyyyMMdd");
                    break;
                case "ls":  // LOT number: 20171009-1 (Day); 20171009-2 (Night)
                    s = (shiftsno == "1") ? nNow.ToString("yyyyMMdd") + "-1" : nNow.AddDays(-1).ToString("yyyyMMdd") + "-2";
                    break;
            }

            return s;
        }

        public void fnTemperatureThread()
        {
            Thread getDateTime = new Thread(() =>
            {
                while (true)
                {
                    _dt = DateTime.Now;
                    Thread.Sleep(1000);
                }
            });
            getDateTime.IsBackground = true;
            getDateTime.Start();

            /****************************/
            /****************************/
            /****************************/

            Thread temperCollect = new Thread(() =>
            {
                while (true)
                {
                    fnUpdateTemp();
                    //Thread.Sleep(100);
                    Thread.Sleep(90000);
                }
            });
            temperCollect.IsBackground = true;
            temperCollect.Start();

            Thread temperWrite = new Thread(() =>
            {
                while (true)
                {
                    int sec = Convert.ToInt32(_dt.Second);
                    lbl_R_DT.Text = lbl_P_DT.Text = lbl_A_DT.Text = String.Format("{0:ss}", _dt);

                    switch (sec)
                    {
                        case 0:
                            _R_upd = true;
                            _P_upd = false;
                            _A_upd = false;

                            fnSaveDataFile();
                            break;
                        case 20:
                            _R_upd = false;
                            _P_upd = true;
                            _A_upd = false;

                            fnSaveDataFile();
                            break;
                        case 40:
                            _R_upd = false;
                            _P_upd = false;
                            _A_upd = true;

                            fnSaveDataFile();
                            break;
                    }
                    //cls.AutoClosingMessageBox.Show("_R_upd: " + _R_upd + "\r\n_P_upd: " + _P_upd + "\r\n_A_upd: " + _A_upd, "", 1500);


                    lbl_R_DT.BackColor = (_R_upd == true) ? Color.DodgerBlue : Color.FromKnownColor(KnownColor.Control);
                    lbl_P_DT.BackColor = (_P_upd == true) ? Color.DodgerBlue : Color.FromKnownColor(KnownColor.Control);
                    lbl_A_DT.BackColor = (_A_upd == true) ? Color.DodgerBlue : Color.FromKnownColor(KnownColor.Control);

                    _R_upd = false;
                    _P_upd = false;
                    _A_upd = false;

                    Thread.Sleep(1000);
                }
            });
            temperWrite.IsBackground = true;
            temperWrite.Start();
        }

    }
}
