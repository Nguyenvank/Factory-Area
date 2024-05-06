using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxSerial;

namespace QueryDevice
{
    public partial class QueryDevice : Form
    {
        private ComPort _objComport = new ComPort();
        private System.Boolean _bPortOpened = false;

        public QueryDevice()
        {
            InitializeComponent();
        }

        private void QueryDevice_Load(object sender, EventArgs e)
        {
            int i;

            comboDevice.Items.Clear();
            for (i = 0; i < _objComport.GetDeviceCount(); i++)
                comboDevice.Items.Add(_objComport.GetDeviceName(i));
            for (i = 0; i < _objComport.GetPortCount(); i++)
                comboDevice.Items.Add(_objComport.GetPortName(i));

            comboDevice.SelectedIndex = 0;

            comboSpeed.Items.Clear();
            comboSpeed.Items.Add("Default");
            comboSpeed.Items.Add("110");
            comboSpeed.Items.Add("300");
            comboSpeed.Items.Add("600");
            comboSpeed.Items.Add("1200");
            comboSpeed.Items.Add("2400");
            comboSpeed.Items.Add("4800");
            comboSpeed.Items.Add("9600");
            comboSpeed.Items.Add("14400");
            comboSpeed.Items.Add("19200");
            comboSpeed.Items.Add("28800");
            comboSpeed.Items.Add("38400");
            comboSpeed.Items.Add("56000");
            comboSpeed.Items.Add("57600");
            comboSpeed.Items.Add("115200");
            comboSpeed.Items.Add("128000");
            comboSpeed.Items.Add("230400");
            comboSpeed.Items.Add("256000");
            comboSpeed.Items.Add("460800");
            comboSpeed.Items.Add("921600");
            comboSpeed.SelectedIndex = 7;		// 9600

            comboSWFlow.Items.Clear();
            comboSWFlow.Items.Add("Default");
            comboSWFlow.Items.Add("Disabled");
            comboSWFlow.Items.Add("Enabled");
            comboSWFlow.SelectedIndex = 0;

            comboHWFlow.Items.Clear();
            comboHWFlow.Items.Add("Default");
            comboHWFlow.Items.Add("Disabled");
            comboHWFlow.Items.Add("Enabled");
            comboHWFlow.SelectedIndex = 0;

            comboFormat.Items.Clear();
            comboFormat.Items.Add("Default");
            comboFormat.Items.Add("n,8,1");
            comboFormat.Items.Add("e,7,1");
            comboFormat.SelectedIndex = 0;

            textLogfile.Text = System.IO.Path.GetTempPath() + "SerialPort.log";

            textComponentInfo.Text = String.Format("ActiveXperts Serial Port Component {0}; Build {1}; Module {2})", _objComport.Version, _objComport.Build, _objComport.Module );
            textLicenseInfo.Text = String.Format("{0} ({1})", _objComport.LicenseStatus, _objComport.LicenseKey == "" ? "no license key" : _objComport.LicenseKey);

            UpdateControls();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_bPortOpened)
            {
                string strReceived = "";
				do
				{
                    strReceived = _objComport.ReadString();

					checkCTS.Checked = _objComport.QueryCTS();
					checkDSR.Checked = _objComport.QueryDSR();
					checkDCD.Checked = _objComport.QueryDCD();
					checkRI.Checked = _objComport.QueryRI();

                    if (strReceived != "" && _objComport.LastError == 0)
                        listReceived.Items.Add(strReceived);
				}
                while (strReceived != "");
            }
        }

        private void AssignPortSettings()
        {
            // ComPort: Clear (good practise)
            _objComport.Clear();   

            // Use SetLicense with the license key to unlock this component. SetLicense will need to be called 
            // everytime a new instance of this component is created. Alternatively, use SaveLicense to store the
            // license key in the registry. You only need to call SaveLicense once.
            // SetLicense is the recommended method when distributing this component with your own software.
            //
            // _objComport.SetLicense( "XXXXX-XXXXX-XXXXX" )  
          
            // Retrieve PortID
            _objComport.Device = comboDevice.SelectedItem.ToString();

            // Retrieve LogFile
            _objComport.LogFile = textLogfile.Text;

            // Retrieve Baudrate
            if ( comboSpeed.SelectedIndex == 0 )
                _objComport.BaudRate = 0;
            else 
                _objComport.BaudRate = System.Int32.Parse(comboSpeed.SelectedItem.ToString());

            // Retrieve Flow Control
            _objComport.HardwareFlowControl = comboHWFlow.SelectedIndex;
            _objComport.SoftwareFlowControl = comboSWFlow.SelectedIndex;

            // Retrieve Device Settings
            if (comboFormat.Text == "Default")
            {
                _objComport.DataBits = (short)_objComport.asDATABITS_DEFAULT;
                _objComport.StopBits = (short)_objComport.asSTOPBITS_DEFAULT;
                _objComport.Parity = (short)_objComport.asPARITY_DEFAULT;
            }

            if (comboFormat.Text == "n,8,1")
            {
                _objComport.DataBits = (short)_objComport.asDATABITS_DEFAULT;
                _objComport.StopBits = (short)_objComport.asSTOPBITS_DEFAULT;
                _objComport.Parity = (short)_objComport.asPARITY_DEFAULT;
            }

            if (comboFormat.Text == "e,7,1")
            {
                _objComport.DataBits = (short)_objComport.asDATABITS_7;
                _objComport.StopBits = (short)_objComport.asSTOPBITS_1;
                _objComport.Parity = (short)_objComport.asPARITY_EVEN;
            }

            // Set Comm Timeout ( 50 ms )
            _objComport.ComTimeout = 50;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            AssignPortSettings();

            _objComport.Open();

            if (GetResult() == 0)
                _bPortOpened = true;
            else
                _bPortOpened = false;

            listReceived.Items.Clear();

            UpdateControls();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            listReceived.Items.Clear();

            _objComport.Close();

            GetResult();

            _bPortOpened = false;

            UpdateControls();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            AssignPortSettings();

            _objComport.UpdateCom();

            GetResult();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            listReceived.Items.Clear();

            _objComport.WriteString(textData.Text);

            GetResult();	

        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(textLogfile.Text))
                System.Diagnostics.Process.Start(textLogfile.Text);

        }

        private void UpdateControls()
        {
            string strDevice = "";
            bool bIsDirectCom = false;

            strDevice = comboDevice.SelectedItem.ToString();
            if (string.Compare(strDevice, 0, "COM", 0, 3, true) == 0)
                bIsDirectCom = true;
            else
                bIsDirectCom = false;

            listReceived.Enabled = _objComport.IsOpened;
            textData.Enabled = _objComport.IsOpened;

            buttonSend.Enabled = _objComport.IsOpened;
            buttonClose.Enabled = _objComport.IsOpened;
            buttonUpdate.Enabled = _objComport.IsOpened && bIsDirectCom;
            buttonOpen.Enabled = ! _objComport.IsOpened;

            comboSpeed.Enabled = bIsDirectCom;
            comboFormat.Enabled = bIsDirectCom;
            comboHWFlow.Enabled = bIsDirectCom;
            comboSWFlow.Enabled = bIsDirectCom;
            checkDTR.Enabled = _objComport.IsOpened;
            checkRTS.Enabled = _objComport.IsOpened;

            checkDTR.Checked = true;
            checkRTS.Checked = true;
        }

        private long GetResult()
        {
            long lResult = _objComport.LastError;

            textResult.Text = lResult.ToString() + ": " + _objComport.GetErrorDescription(_objComport.LastError);

            return lResult;
        }

        private void checkDTR_CheckedChanged(object sender, EventArgs e)
        {
            if (_objComport.IsOpened)
            {
                if (checkDTR.Checked)
                    _objComport.RaiseDTR(true);
                else
                    _objComport.RaiseDTR(false);

                GetResult();
            }
        }

        private void checkRTS_CheckedChanged(object sender, EventArgs e)
        {
            if (_objComport.IsOpened )
            {
                if (checkRTS.Checked)
                    _objComport.RaiseRTS(true);
                else
                    _objComport.RaiseRTS(false);

                GetResult();
            }
        }

        private void comboDevice_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateControls();
        }
    }
}