using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Rei;
using System.IO.Ports;

using DbCheck;

namespace plc_sachul
{
 
    public partial class Form1 : Form
    {
        const char STX = (char)0x02;
        const char ETX = (char)0x03; //End Text [응답용Asc]
        const char EOT = (char)0x04; //End of Text[요구용 Asc]
        const char ENQ = (char)0x05; //Enquire[프레임시작코드]
        const char ACK = (char)0x06; //Acknowledge[응답 시작]
        const char NAK = (char)0x15; //Not Acknoledge[에러응답시작]

        // INI 파일 관련
        //Ini ini = new Ini("C:\\plc_sachul.ini");

        // DB 관련
        private msServer sv = new msServer();

        public string getStrData = "";
        public int l_cnt = 0;
        public static string plc_code = "";
        public static string app_name = "";

        // INI 파일 관련
        Ini ini = new Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        
        public static string db_name = "";
        public static string db_ip = "";
        public string preStrData = "";
        public int plcCount = 20;

        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false; // 크로스 스레드 관련
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // ini 파일이 없으면,
            if (ini.IniExists() == false)
            {
                MessageBox.Show(Application.StartupPath + "\\" + Application.ProductName + ".ini 파일이 없습니다. ini 파일을 설정, 생성해 주세요!");
                return;
            }
            else // ini 파일이 있으면 설정값을 가져온다.
            {
                app_name = ini.GetIniValue("SETTING", "NAME", "PLC GATHERING").Trim();
                db_ip = ini.GetIniValue("DB", "IP", "210.219.199.8,5001").Trim();
                db_name = ini.GetIniValue("DB", "NAME", "INJREC_VM").Trim();
                plc_code = ini.GetIniValue("SETTING", "PLC_CODE", "MW0001").Trim();
            }
            
            this.Text = app_name; // "Machine Work";
            sv.SetDBInfo(db_ip, db_name);
            sv.Open();
            SerialPortOpen();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // serial 포트 종료
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                serialPort1.Dispose();
            }

            // DB 연결 종료
            sv.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (check.IsConnectedToInternet() == true)//(sv.State().ToString() == "YES")
            {
                l_state.Text = "YES";
                l_state.BackColor = Color.Blue;
            }
            else
            {
                l_state.Text = "NO";
                l_state.BackColor = Color.Red;
            }

            plc(plc_code);
            System.Threading.Thread.Sleep(50);
        }

        private void plc(string temp)
        {
            int iLen;
            string iHex = "";
            string SendString = "";

            iLen = Convert.ToInt32(2) * 4 + 11;  //var2 * 4 + 11
            iHex = Right("0" + Convert.ToString((Convert.ToInt32(Convert.ToInt32(26))), 16).ToUpper().ToString(), 2); //Right("0" & Hex(Val(var2)), 2)


            SendString = ENQ + "00RSB07%" + temp + iHex + EOT; //SendString = ENQ & "00RSB07%" & var1 & iHex & EOT

            if (temp ==  "MW0001" || temp == "MW0011") //(temp == plc_code)// "MW0001" || temp == "DW0501")'
            {
                serialPort1.Write(SendString);
                do { } while (serialPort1.WriteBufferSize == 0);
                PlcDataRead(temp);
                System.Threading.Thread.Sleep(50);//0.1초마다 한번씩읽기
                //Application.DoEvents();
            }

        }

        private void PlcDataRead(string plc_temp)
         {

            string read_buffer = "";

            System.Threading.Thread.Sleep(1000);

            if (plc_temp == "MW0001" || plc_temp == "MW0011")
            {
                read_buffer = serialPort1.ReadExisting();
            }

            DateTime now = DateTime.Now; //DateTime.UtcNow.AddMinutes(0);//DateTime.Now;

            l_time.Text = "Time : " + now.ToString("yyyy-MM-dd HH:mm:ss");

            string time = now.ToString("yyyy-MM-dd HH:mm:ss");

            getStrData = read_buffer;

            if (getStrData.Length.Equals(Convert.ToInt32(Convert.ToInt32(26)) * 4 + 11))
            {
                if (Left(getStrData, 1) == ACK.ToString())
                {
                    getStrData = Mid(getStrData, 10, Convert.ToInt32(Convert.ToInt32(26)) * 4);
                }
                else
                {
                    getStrData = "";
                }

                if (l_cnt <= 10)
                {
                    l_data.Text = plc_temp + "::" + getStrData + "(" + plc_code + ")" + "\r" + "\n" + l_data.Text;
                }
                else
                {
                    l_data.Text = plc_temp + "::" + getStrData;
                    l_cnt = 0;
                }


                if (sv.State() == "YES")
                {
                    //if (preStrData != getStrData)  
                    //{
                        sv._query = "update TEMPT3 set temp1 = '" + getStrData + "',sysdate = '" + time + "' where temp = '" + plc_temp + "'";
                        sv.execNonQuery();
                   //}
                    //preStrData = getStrData + "'";
                }
                else { sv.Open(); } 


                l_cnt = l_cnt + 1;
                //l_data.Text = temp + "::" + getStrData + "\r" + "\n" + l_data.Text;
            }

           // sv.Close();
        }

        private void SerialPortOpen()
        {
            serialPort1.Close();
            serialPort1.PortName = "COM1";
            serialPort1.BaudRate = 38400;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Open();
        }

        public string Left(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth)
            {
                TextLenth = Text.Length;
            }
            ConvertText = Text.Substring(0, TextLenth);
            return ConvertText;
        }

        public string Right(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth)
            {
                TextLenth = Text.Length;
            }
            ConvertText = Text.Substring(Text.Length - TextLenth, TextLenth);
            return ConvertText;
        }

        public string Mid(string Text, int Startint, int Endint)
        {
            string ConvertText;
            if (Startint < Text.Length || Endint < Text.Length)
            {
                ConvertText = Text.Substring(Startint, Endint);
                return ConvertText;
            }
            else
                return Text;
        }
    }
}
