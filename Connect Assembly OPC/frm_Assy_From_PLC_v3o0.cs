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
using Inventory_Data;
using S7.Net;

namespace Fan_1
{
    public partial class frm_Assy_From_PLC_v3o0 : Form
    {
        Plc __plc;
        CpuType __cpu = CpuType.S7200;
        string __ip = "192.168.0.16";
        short __rack = 0;
        short __slot = 1;
        int __cycle = 1000;

        System.Windows.Forms.Timer
            __timer_main = new System.Windows.Forms.Timer();

        DateTime
            __now = DateTime.Now;

        string
            __sql = "",
            __msg = "",
            __app = cls.appName(),
            /********************/
            __addr_line_1_ok = "", __addr_line_1_ng = "",
            __addr_line_2_ok = "", __addr_line_2_ng = "",
            __addr_line_3_ok = "", __addr_line_3_ng = "",
            __addr_line_4_ok = "", __addr_line_4_ng = "",
            __addr_line_5_ok = "", __addr_line_5_ng = "",
            __addr_line_6_ok = "", __addr_line_6_ng = "",
            __addr_line_7_ok = "", __addr_line_7_ng = "",
            __addr_line_8_ok = "", __addr_line_8_ng = "",
            __addr_line_9_ok = "", __addr_line_9_ng = "",
            __addr_line_10_ok = "", __addr_line_10_ng = "",
            __addr_line_11_ok = "", __addr_line_11_ng = "",
            __addr_line_12_ok = "", __addr_line_12_ng = "",
            __addr_line_13_ok = "", __addr_line_13_ng = "",
            __addr_line_14_ok = "", __addr_line_14_ng = "",
            /********************/
            __val_ab_1_ok = "", __val_ab_1_ng = "",
            __val_ab_2_ok = "", __val_ab_2_ng = "",
            __val_ab_3_ok = "", __val_ab_3_ng = "",
            __val_dis_1_ok = "", __val_dis_1_ng = "",
            __val_dis_2_ok = "", __val_dis_2_ng = "",
            __val_pump_ok = "", __val_pump_ng = "",
            __val_wb_1_ok = "", __val_wb_1_ng = "",
            __val_wb_2_ok = "", __val_wb_2_ng = "",
            __val_wb_3_ok = "", __val_wb_3_ng = "",
            __val_wb_4_ok = "", __val_wb_4_ng = "",
            __val_bl_1_ok = "", __val_bl_1_ng = "",
            __val_bl_2_ok = "", __val_bl_2_ng = "",
            __val_wld_1_ok = "", __val_wld_1_ng = "",
            __val_wld_2_ok = "", __val_wld_2_ng = "";

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __now_hrs = 0,
            __now_min = 0,
            __now_sec = 0,
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        ushort
            __ab_1_ok = 0, __ab_1_ng = 0,
            __ab_2_ok = 0, __ab_2_ng = 0,
            __ab_3_ok = 0, __ab_3_ng = 0,
            __dis_1_ok = 0, __dis_1_ng = 0,
            __dis_2_ok = 0, __dis_2_ng = 0,
            __pump_ok = 0, __pump_ng = 0,
            __wb_1_ok = 0, __wb_1_ng = 0,
            __wb_2_ok = 0, __wb_2_ng = 0,
            __wb_3_ok = 0, __wb_3_ng = 0,
            __wb_4_ok = 0, __wb_4_ng = 0,
            __bl_1_ok = 0, __bl_1_ng = 0,
            __bl_2_ok = 0, __bl_2_ng = 0,
            __wld_1_ok = 0, __wld_1_ng = 0,
            __wld_2_ok = 0, __wld_2_ng = 0;


        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        cls.Ini
            ini_plc = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName.Trim().Replace(" ", "_") + "_PLC.ini"),
            ini_addr = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName.Trim().Replace(" ", "_") + "_ADDR.ini");

        public frm_Assy_From_PLC_v3o0()
        {
            InitializeComponent();

            Fnc_Load_Config_PLC();
            Fnc_Load_Config_Addr();

            __timer_main.Interval = __cycle;
            __timer_main.Enabled = true;
            __timer_main.Tick += __timer_main_Tick;

            cls.SetDoubleBuffer(tlp_main, true);
            cls.SetDoubleBuffer(tlp_top, true);
            cls.SetDoubleBuffer(pnl_value, true);
            cls.SetDoubleBuffer(tlp_value, true);
        }

        private void frm_Assy_From_PLC_v3o0_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chk_plc.Checked)
            {
                DialogResult result = MessageBox.Show("Do you still want to close this PLC connection ?", __app, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Fnc_Load_DisConnect_PLC();
                }
            }
            else
            {
                Fnc_Load_DisConnect_PLC();
            }
        }

        private void frm_Assy_From_PLC_v3o0_Load(object sender, EventArgs e)
        {
            Fnc_Load_Init();
        }

        public void Fnc_Load_Init()
        {
            Fnc_Load_Controls();
        }

        /*************************************/

        public void Fnc_Load_Controls()
        {
            Fnc_Load_Init_Controls();

            chk_plc.Enabled = true;
            chk_plc.Checked = false;

            lbl_status.Text = "Not connect to PLC yet";
            tlp_top.BackColor = Color.Gold;

            chk_list.Enabled = chk_list.Checked = false;
            dgv_list.DataSource = null;
        }

        public void Fnc_Load_Init_Controls()
        {
            try
            {
                for(int i = 1; i <= 16; i++)
                {
                    Label
                        lbl_hd = (Label)cls.FindControlRecursive(pnl_value, "lbl_assy_line_" + i + "_title"),
                        lbl_tt = (Label)cls.FindControlRecursive(pnl_value, "lbl_assy_line_" + i + "_tt"),
                        lbl_ok = (Label)cls.FindControlRecursive(pnl_value, "lbl_assy_line_" + i + "_ok"),
                        lbl_ng = (Label)cls.FindControlRecursive(pnl_value, "lbl_assy_line_" + i + "_ng");

                    Fnc_Load_Header_Text(i, lbl_hd);

                    lbl_tt.Text = "0";
                    lbl_ok.Text = "OK: 0";
                    lbl_ng.Text = "NG: 0";

                    if (i == 3 || i == 15 || i == 16)
                    {
                        lbl_hd.Text = lbl_tt.Text = lbl_ok.Text = lbl_ng.Text = "";
                    }
                }

            }
            catch { }
            finally { }
        }

        public string Fnc_Load_Header_Text(int line, Label lbl)
        {
            string header = "";
            try
            {
                if (line > 0)
                {
                    switch (line)
                    {
                        case 1:
                            header = "Auto Balance 01";
                            break;
                        case 2:
                            header = "Auto Balance 02";
                            break;
                        case 3:
                            header = "Auto Balance 03";
                            break;
                        case 4:
                            header = "Dispenser 01";
                            break;
                        case 5:
                            header = "Dispenser 02";
                            break;
                        case 6:
                            header = "Pump line";
                            break;
                        case 7:
                            header = "Weight Balance 01";
                            break;
                        case 8:
                            header = "Weight Balance 02";
                            break;
                        case 9:
                            header = "Weight Balance 03";
                            break;
                        case 10:
                            header = "Weight Balance 04";
                            break;
                        case 11:
                            header = "Blower machine 01";
                            break;
                        case 12:
                            header = "Blower machine 02";
                            break;
                        case 13:
                            header = "Welding machine 01";
                            break;
                        case 14:
                            header = "Welding machine 02";
                            break;
                        case 15:
                            header = "";
                            break;
                        case 16:
                            header = "";
                            break;
                    }

                    lbl.Text = header;
                }
            }
            catch { }
            finally { }

            return header;
        }

        public void Fnc_Load_Config_PLC()
        {
            string
                plc_type = ini_plc.GetIniValue("PLC", "Type", "200").Trim(),
                ip_addr = ini_plc.GetIniValue("PLC", "IP", "192.168.0.16").Trim(),
                rack_no = ini_plc.GetIniValue("PLC", "Rack", "0").Trim(),
                slot_no = ini_plc.GetIniValue("PLC", "Slot", "1").Trim(),
                cycle = ini_plc.GetIniValue("PLC", "Cycle", "1000").Trim();

            short
                _rack_no = Convert.ToInt16(rack_no),
                _slot_no = Convert.ToInt16(slot_no);

            int
                _cycle = Convert.ToInt32(cycle);

            switch (plc_type)
            {
                case "200":
                    __cpu = CpuType.S7200;
                    break;
                case "300":
                    __cpu = CpuType.S7300;
                    break;
                case "400":
                    __cpu = CpuType.S7400;
                    break;
                case "1200":
                    __cpu = CpuType.S71200;
                    break;
            }

            __ip = ip_addr;
            __rack = _rack_no;
            __slot = _slot_no;
            __cycle = _cycle;

            __plc = new Plc(__cpu, __ip, __rack, __slot);
        }

        public void Fnc_Load_Config_Addr()
        {
            __addr_line_1_ok = ini_addr.GetIniValue("ADDR", "AUTOBALANCE_01_OK", "C1"); __addr_line_1_ng = ini_addr.GetIniValue("ADDR", "AUTOBALANCE_01_NG", "C2");
            __addr_line_2_ok = ini_addr.GetIniValue("ADDR", "AUTOBALANCE_02_OK", "C3"); __addr_line_2_ng = ini_addr.GetIniValue("ADDR", "AUTOBALANCE_02_NG", "C4");
            __addr_line_3_ok = ini_addr.GetIniValue("ADDR", "AUTOBALANCE_03_OK", "C5"); __addr_line_3_ng = ini_addr.GetIniValue("ADDR", "AUTOBALANCE_03_NG", "C6");

            __addr_line_4_ok = ini_addr.GetIniValue("ADDR", "DISPENSER_01_OK", "VW10"); __addr_line_4_ng = ini_addr.GetIniValue("ADDR", "DISPENSER_01_NG", "VW12");
            __addr_line_5_ok = ini_addr.GetIniValue("ADDR", "DISPENSER_02_OK", "VW14"); __addr_line_5_ng = ini_addr.GetIniValue("ADDR", "DISPENSER_02_NG", "C26");

            __addr_line_6_ok = ini_addr.GetIniValue("ADDR", "PUMP_LINE_OK", "C33"); __addr_line_6_ng = ini_addr.GetIniValue("ADDR", "PUMP_LINE_NG", "VW6");

            __addr_line_7_ok = ini_addr.GetIniValue("ADDR", "W_BALANCE_01_OK", "C11"); __addr_line_7_ng = ini_addr.GetIniValue("ADDR", "W_BALANCE_01_NG", "VW6");
            __addr_line_8_ok = ini_addr.GetIniValue("ADDR", "W_BALANCE_02_OK", "C12"); __addr_line_8_ng = ini_addr.GetIniValue("ADDR", "W_BALANCE_01_NG", "VW6");
            __addr_line_9_ok = ini_addr.GetIniValue("ADDR", "W_BALANCE_03_OK", "C13"); __addr_line_9_ng = ini_addr.GetIniValue("ADDR", "W_BALANCE_01_NG", "VW6");
            __addr_line_10_ok = ini_addr.GetIniValue("ADDR", "W_BALANCE_04_OK", "C27"); __addr_line_10_ng = ini_addr.GetIniValue("ADDR", "W_BALANCE_01_NG", "VW6");

            __addr_line_11_ok = ini_addr.GetIniValue("ADDR", "BLOWER_01_OK", "C9"); __addr_line_11_ng = ini_addr.GetIniValue("ADDR", "BLOWER_01_NG", "VW6");
            __addr_line_12_ok = ini_addr.GetIniValue("ADDR", "BLOWER_02_OK", "C10"); __addr_line_12_ng = ini_addr.GetIniValue("ADDR", "BLOWER_01_NG", "VW6");

            __addr_line_13_ok = ini_addr.GetIniValue("ADDR", "WELDING_01_OK", "C7"); __addr_line_13_ng = ini_addr.GetIniValue("ADDR", "WELDING_01_NG", "VW6");
            __addr_line_14_ok = ini_addr.GetIniValue("ADDR", "WELDING_02_OK", "C8"); __addr_line_14_ng = ini_addr.GetIniValue("ADDR", "WELDING_01_NG", "VW6");
        }

        public void Fnc_Load_Connect_PLC()
        {
            try
            {
                if (chk_plc.Checked)
                {

                    if (__plc.IsAvailable || __plc.IsConnected)
                    {
                        __plc.Close();
                    }

                    __plc.Open();

                    Fnc_Load_Read_PLC();
                }
                else
                {
                    if (__plc.IsAvailable || __plc.IsConnected)
                    {
                        __plc.Close();
                    }
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_DisConnect_PLC()
        {
            try
            {
                if (__plc.IsAvailable || __plc.IsConnected)
                {
                    __plc.Close();
                }
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Read_PLC()
        {
            try
            {
                if (__plc.IsAvailable && __plc.IsConnected)
                {
                    __ab_1_ok = (ushort)__plc.Read(__addr_line_1_ok); __ab_1_ng = (ushort)__plc.Read(__addr_line_1_ng);
                    __ab_2_ok = (ushort)__plc.Read(__addr_line_2_ok); __ab_2_ng = (ushort)__plc.Read(__addr_line_2_ng);
                    __ab_3_ok = (ushort)__plc.Read(__addr_line_3_ok); __ab_3_ng = (ushort)__plc.Read(__addr_line_3_ng);

                    __dis_1_ok = (ushort)__plc.Read(__addr_line_4_ok); __dis_1_ng = (ushort)__plc.Read(__addr_line_4_ng);
                    __dis_2_ok = (ushort)__plc.Read(__addr_line_5_ok); __dis_2_ng = (ushort)__plc.Read(__addr_line_5_ng);

                    __pump_ok = (ushort)__plc.Read(__addr_line_6_ok); __pump_ng = (ushort)__plc.Read(__addr_line_6_ng);

                    __wb_1_ok = (ushort)__plc.Read(__addr_line_7_ok); __wb_1_ng = (ushort)__plc.Read(__addr_line_7_ng);
                    __wb_2_ok = (ushort)__plc.Read(__addr_line_8_ok); __wb_2_ng = (ushort)__plc.Read(__addr_line_8_ng);
                    __wb_3_ok = (ushort)__plc.Read(__addr_line_9_ok); __wb_3_ng = (ushort)__plc.Read(__addr_line_9_ng);
                    __wb_4_ok = (ushort)__plc.Read(__addr_line_10_ok); __wb_4_ng = (ushort)__plc.Read(__addr_line_10_ng);

                    __bl_1_ok = (ushort)__plc.Read(__addr_line_11_ok); __bl_1_ng = (ushort)__plc.Read(__addr_line_11_ng);
                    __bl_2_ok = (ushort)__plc.Read(__addr_line_12_ok); __bl_2_ng = (ushort)__plc.Read(__addr_line_12_ng);

                    __wld_1_ok = (ushort)__plc.Read(__addr_line_13_ok); __wld_1_ng = (ushort)__plc.Read(__addr_line_13_ng);
                    __wld_2_ok = (ushort)__plc.Read(__addr_line_14_ok); __wld_2_ng = (ushort)__plc.Read(__addr_line_14_ng);

                    lbl_assy_line_1_ok.Text = String.Format("OK: {0}", __ab_1_ok); lbl_assy_line_1_ng.Text = String.Format("NG: {0}", __ab_1_ng);
                    lbl_assy_line_2_ok.Text = String.Format("OK: {0}", __ab_2_ok); lbl_assy_line_2_ng.Text = String.Format("NG: {0}", __ab_2_ng);
                    lbl_assy_line_3_ok.Text = String.Format("OK: {0}", __ab_3_ok); lbl_assy_line_3_ng.Text = String.Format("NG: {0}", __ab_3_ng);

                    lbl_assy_line_4_ok.Text = String.Format("OK: {0}", __dis_1_ok); lbl_assy_line_4_ng.Text = String.Format("NG: {0}", __dis_1_ng);
                    lbl_assy_line_5_ok.Text = String.Format("OK: {0}", __dis_2_ok); lbl_assy_line_5_ng.Text = String.Format("NG: {0}", __dis_2_ng);

                    lbl_assy_line_6_ok.Text = String.Format("OK: {0}", __pump_ok); lbl_assy_line_6_ng.Text = String.Format("NG: {0}", __pump_ng);

                    lbl_assy_line_7_ok.Text = String.Format("OK: {0}", __wb_1_ok); lbl_assy_line_7_ng.Text = String.Format("NG: {0}", __wb_1_ng);
                    lbl_assy_line_8_ok.Text = String.Format("OK: {0}", __wb_2_ok); lbl_assy_line_8_ng.Text = String.Format("NG: {0}", __wb_2_ng);
                    lbl_assy_line_9_ok.Text = String.Format("OK: {0}", __wb_3_ok); lbl_assy_line_9_ng.Text = String.Format("NG: {0}", __wb_3_ng);
                    lbl_assy_line_10_ok.Text = String.Format("OK: {0}", __wb_4_ok); lbl_assy_line_10_ng.Text = String.Format("NG: {0}", __wb_4_ng);

                    lbl_assy_line_11_ok.Text = String.Format("OK: {0}", __bl_1_ok); lbl_assy_line_11_ng.Text = String.Format("NG: {0}", __bl_1_ng);
                    lbl_assy_line_12_ok.Text = String.Format("OK: {0}", __bl_2_ok); lbl_assy_line_12_ng.Text = String.Format("NG: {0}", __bl_2_ng);

                    lbl_assy_line_13_ok.Text = String.Format("OK: {0}", __wld_1_ok); lbl_assy_line_13_ng.Text = String.Format("NG: {0}", __wld_1_ng);
                    lbl_assy_line_14_ok.Text = String.Format("OK: {0}", __wld_2_ok); lbl_assy_line_14_ng.Text = String.Format("NG: {0}", __wld_2_ng);
                }
                else
                {
                    lbl_assy_line_1_ok.Text =
                        lbl_assy_line_2_ok.Text =
                        lbl_assy_line_3_ok.Text =
                        lbl_assy_line_4_ok.Text =
                        lbl_assy_line_5_ok.Text =
                        lbl_assy_line_6_ok.Text =
                        lbl_assy_line_7_ok.Text =
                        lbl_assy_line_8_ok.Text =
                        lbl_assy_line_9_ok.Text =
                        lbl_assy_line_10_ok.Text =
                        lbl_assy_line_11_ok.Text =
                        lbl_assy_line_12_ok.Text =
                        lbl_assy_line_13_ok.Text =
                        lbl_assy_line_14_ok.Text = "OK: 0";

                    lbl_assy_line_1_ng.Text =
                        lbl_assy_line_2_ng.Text =
                        lbl_assy_line_3_ng.Text =
                        lbl_assy_line_4_ng.Text =
                        lbl_assy_line_5_ng.Text =
                        lbl_assy_line_6_ng.Text =
                        lbl_assy_line_7_ng.Text =
                        lbl_assy_line_8_ng.Text =
                        lbl_assy_line_9_ng.Text =
                        lbl_assy_line_10_ng.Text =
                        lbl_assy_line_11_ng.Text =
                        lbl_assy_line_12_ng.Text =
                        lbl_assy_line_13_ng.Text =
                        lbl_assy_line_14_ng.Text = "NG: 0";
                }

                lbl_assy_line_3_title.Text = lbl_assy_line_3_tt.Text = lbl_assy_line_3_ok.Text = lbl_assy_line_3_ng.Text =
                    lbl_assy_line_15_title.Text = lbl_assy_line_15_tt.Text = lbl_assy_line_15_ok.Text = lbl_assy_line_15_ng.Text =
                    lbl_assy_line_16_title.Text = lbl_assy_line_16_tt.Text = lbl_assy_line_16_ok.Text = lbl_assy_line_16_ng.Text = "";
            }
            catch { }
            finally { }
        }

        static async Task Main()
        {
            Task<DateTime> line01_ok = Fnc_AB_01_OK(); Task<DateTime> line01_ng = Fnc_AB_01_NG();
            Task<DateTime> line02_ok = Fnc_AB_02_OK(); Task<DateTime> line02_ng = Fnc_AB_02_NG();
            Task<DateTime> line03_ok = Fnc_AB_03_OK(); Task<DateTime> line03_ng = Fnc_AB_03_NG();

            Task<DateTime> line04_ok = Fnc_DIS_01_OK(); Task<DateTime> line04_ng = Fnc_DIS_01_NG();
            Task<DateTime> line05_ok = Fnc_DIS_02_OK(); Task<DateTime> line05_ng = Fnc_DIS_02_NG();

            Task<DateTime> line06_ok = Fnc_Pump_OK(); Task<DateTime> line06_ng = Fnc_Pump_NG();

            Task<DateTime> line07_ok = Fnc_WB_01_OK(); Task<DateTime> line07_ng = Fnc_WB_01_NG();
            Task<DateTime> line08_ok = Fnc_WB_02_OK(); Task<DateTime> line08_ng = Fnc_WB_02_NG();
            Task<DateTime> line09_ok = Fnc_WB_03_OK(); Task<DateTime> line09_ng = Fnc_WB_03_NG();
            Task<DateTime> line10_ok = Fnc_WB_04_OK(); Task<DateTime> line10_ng = Fnc_WB_04_NG();

            Task<DateTime> line11_ok = Fnc_BL_01_OK(); Task<DateTime> line11_ng = Fnc_BL_01_NG();
            Task<DateTime> line12_ok = Fnc_BL_02_OK(); Task<DateTime> line12_ng = Fnc_BL_02_NG();

            Task<DateTime> line13_ok = Fnc_WLD_01_OK(); Task<DateTime> line13_ng = Fnc_WLD_01_NG();
            Task<DateTime> line14_ok = Fnc_WLD_02_OK(); Task<DateTime> line14_ng = Fnc_WLD_02_NG();
        }

        static async Task<DateTime> Fnc_AB_01_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_AB_01_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_AB_02_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_AB_02_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_AB_03_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_AB_03_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_DIS_01_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_DIS_01_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_DIS_02_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_DIS_02_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_Pump_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_Pump_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_01_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_01_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_02_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_02_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_03_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_03_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_04_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WB_04_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_BL_01_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_BL_01_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_BL_02_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_BL_02_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WLD_01_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WLD_01_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WLD_02_OK()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        static async Task<DateTime> Fnc_WLD_02_NG()
        {
            DateTime result = DateTime.Now;

            return result;
        }

        /*************************************/

        private void __timer_main_Tick(object sender, EventArgs e)
        {
            __now = DateTime.Now;

            __now_hrs = __now.Hour;
            __now_min = __now.Minute;
            __now_sec = __now.Second;

            try
            {
                if (chk_plc.Checked)
                {
                    Fnc_Load_Connect_PLC();

                    if (__plc.IsConnected)
                    {
                        tlp_top.BackColor = (__now_sec % 2 == 0) ? Color.DodgerBlue : Color.LightSkyBlue;
                        lbl_status.Text = "PLC connected successful";

                        Fnc_Load_Read_PLC();
                    }
                    else
                    {
                        tlp_top.BackColor = (__now_sec % 2 == 0) ? Color.OrangeRed : Color.LightPink;
                        lbl_status.Text = "PLC 192.168.0.16 connected failed";
                    }
                }
                else
                {
                    tlp_top.BackColor = Color.Gold;
                    lbl_status.Text = "Not connect to PLC yet";
                }
            }
            catch { }
            finally { }
        }

        private void chk_plc_Click(object sender, EventArgs e)
        {
            Fnc_Load_Connect_PLC();
        }
    }
}
