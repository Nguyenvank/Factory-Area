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
    public partial class frmTestFlowControls : Form
    {
        public int nRubber = 16;
        public int nInjection = 12;
        public int nAssembly = 15;
        public int nABalance = 3;
        public int nDispenser = 2;
        public int nPump = 1;
        public int nWelding = 2;
        public int nWBalance = 4;
        public int nBlower = 2;
        public int nMixing = 1;

        public int btnWidth = 194;
        public int btnHeight = 130;     //125

        private string factcd = "F1";
        private string shiftsno = "1";
        private string workdate = "";
        private string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        private DateTime sTime1 = new DateTime();
        private DateTime sTime2 = new DateTime();
        private string shiftsnm = "Night";

        //countRub
        public string _nameRub = "";
        public string _rubber = "";
        public string _statusRub = "0";
        public string _activeRub = "";
        public int countRub = 0;

        //countRub
        public string _nameInj = "";
        public string _injection = "";
        public string _statusInj = "0";
        public string _activeInj = "";
        public int countInj = 0;

        public string _name = "";
        public string _stop = "0";
        public string lastUpdate = "";
        public DateTime _dt = DateTime.Now;
        public DateTime _lastUpdate;
        public string _outTime = "0";
        public string _status = "0";
        public string _active = "";
        public TimeSpan idleTime;
        public double totalMinutes;
        public int countAsy = 0;

        public DateTime _startLunch;
        public DateTime _endLunch;
        public DateTime _startDinner;
        public DateTime _endDinner;
        public DateTime _startMidnight;
        public DateTime _endMidnight;
        public DateTime _startBreakfast;
        public DateTime _endBreakfast;

        public frmTestFlowControls()
        {
            InitializeComponent();
        }

        private void frmTestFlowControls_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(cls.fnGetDate("sn") + "\r\n" + cls.fnGetDate("lot").Substring(0, 8));
            init();
            //ProcessControlColor(flpRub, "btnRub10", "1");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
            fnWorkdate();
            fnRestTime();
            fnLoadRubberStatus();
            fnLoadPlasticStatus();
            fnLoadAssemblyStatus();
        }

        public void init()
        {
            fnGetdate();
            fnWorkdate();
            fnRestTime();
            fnLoadRubber();
            fnLoadInjection();
            fnLoadAssembly();

            fnLoadRubberStatus();
            fnLoadPlasticStatus();
            fnLoadAssemblyStatus();
        }

        public void fnGetdate()
        {
            if(check.IsConnectedToInternet())
            {
                toolStripStatusLabel2.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                toolStripStatusLabel2.ForeColor = Color.Black;
            }
            else
            {
                toolStripStatusLabel2.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss tt}", DateTime.Now);
                toolStripStatusLabel2.ForeColor = Color.Red;
            }
        }

        private void ProcessControlsName(Control ctrlContainer, string name, string text)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl.GetType() == typeof(Button) && ctrl.Name == name)
                {
                    // Do whatever to the TextBox 
                    ctrl.Text = text;
                }
            }
        }

        public void ProcessControlColor(Control ctrlContainer, string name, string status)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl.GetType() == typeof(Button) && ctrl.Name == name)
                {
                    // Do whatever to the TextBox 
                    ctrl.BackColor = (Convert.ToInt32(status) == 0) ? Color.DodgerBlue : Color.IndianRed;
                }
            }
        }

        public void ProcessControlDisabled(Control ctrlContainer, string name)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl.GetType() == typeof(Button) && ctrl.Name == name)
                {
                    // Do whatever to the TextBox 
                    ctrl.BackColor = Color.Silver;
                    ctrl.ForeColor = Color.LightGray;
                    //ctrl.Enabled = false;
                }
            }
        }

        public void fnLoadRubber()
        {
            try
            {
                if(nRubber>0)
                {

                    for (int iRub = 1; iRub <= nRubber; iRub++)
                    {
                        Button btn = new Button();
                        btn.BackColor = System.Drawing.Color.DodgerBlue;
                        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.Name = "btnRub" + iRub;
                        btn.Size = new System.Drawing.Size(btnWidth, btnHeight);
                        btn.TabIndex = 0;
                        btn.Text = (iRub < 10) ? "RUBBER 0" + iRub : "RUBBER " + iRub;
                        btn.UseVisualStyleBackColor = false;

                        flpRub.Controls.Add(btn);
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadInjection()
        {
            try
            {
                if (nInjection > 0)
                {

                    for (int iInj = 1; iInj <= nInjection; iInj++)
                    {
                        Button btn = new Button();
                        btn.BackColor = System.Drawing.Color.DodgerBlue;
                        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.Name = "btnInj" + iInj;
                        btn.Size = new System.Drawing.Size(btnWidth, btnHeight);
                        btn.TabIndex = 0;
                        btn.Text = (iInj < 10) ? "INJECTION 0" + iInj : "INJECTION " + iInj;
                        btn.UseVisualStyleBackColor = false;

                        flpInj.Controls.Add(btn);
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadAssembly()
        {
            try
            {
                if (nAssembly > 0)
                {

                    for (int iAsy = 1; iAsy <= nAssembly; iAsy++)
                    {
                        Button btn = new Button();
                        btn.BackColor = System.Drawing.Color.DodgerBlue;
                        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        //btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.Name = "btnAsy" + iAsy;
                        btn.Size = new System.Drawing.Size(btnWidth, btnHeight);
                        btn.TabIndex = 0;
                        btn.Text = (iAsy < 10) ? "INJECTION 0" + iAsy : "INJECTION " + iAsy;
                        btn.UseVisualStyleBackColor = false;

                        flpAsy.Controls.Add(btn);
                    }
                    ProcessControlsName(flpAsy, "btnAsy1", "AUTO\r\nBALANCE 01");
                    ProcessControlsName(flpAsy, "btnAsy2", "AUTO\r\nBALANCE 02");
                    ProcessControlsName(flpAsy, "btnAsy3", "AUTO\r\nBALANCE 03");

                    ProcessControlsName(flpAsy, "btnAsy4", "DISPENSER 01");
                    ProcessControlsName(flpAsy, "btnAsy5", "DISPENSER 02");

                    ProcessControlsName(flpAsy, "btnAsy6", "PUMP");

                    ProcessControlsName(flpAsy, "btnAsy7", "WELDING 01");
                    ProcessControlsName(flpAsy, "btnAsy8", "WELDING 02");

                    ProcessControlsName(flpAsy, "btnAsy9", "WEIGHT\r\nBALANCE 01");
                    ProcessControlsName(flpAsy, "btnAsy10", "WEIGHT\r\nBALANCE 02");
                    ProcessControlsName(flpAsy, "btnAsy11", "WEIGHT\r\nBALANCE 03");
                    ProcessControlsName(flpAsy, "btnAsy12", "WEIGHT\r\nBALANCE 04");

                    ProcessControlsName(flpAsy, "btnAsy13", "BLOWER 01");
                    ProcessControlsName(flpAsy, "btnAsy14", "BLOWER 02");

                    ProcessControlsName(flpAsy, "btnAsy15", "MIXING");
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void fnLoadAssemblyStatus()
        {
            try
            {
                string sql = "V2o1_BASE_Capacity_Work_Order_Monitor_2o3o7_Machine_Status_Addnew";
                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                countAsy = ds.Tables[0].Rows.Count;
                if (countAsy > 0)
                {
                    foreach (Control ctrl in flpAsy.Controls)
                    {
                        ctrl.BackColor = Color.Silver;
                        ctrl.ForeColor = Color.LightGray;
                        //ctrl.Enabled = false;
                    }

                    for (int i = 0; i < countAsy; i++)
                    {
                        _name = ds.Tables[0].Rows[i][12].ToString();
                        if (_name != "")
                        {
                            _stop = ds.Tables[0].Rows[i][9].ToString();
                            lastUpdate = ds.Tables[0].Rows[i][10].ToString();
                            _lastUpdate = (lastUpdate != "" && lastUpdate != null) ? Convert.ToDateTime(ds.Tables[0].Rows[i][10].ToString()) : _dt;
                            idleTime = DateTime.Now - _lastUpdate;
                            totalMinutes = idleTime.Minutes;
                            _status = ds.Tables[0].Rows[i][11].ToString();
                            _active = (_status == "1") ? "0" : "1";

                            foreach (Control ctrl in flpAsy.Controls)
                            {
                                if (ctrl.Name == _name)
                                {
                                    if(_stop.ToLower() == "true" || totalMinutes >= 5)
                                    {
                                        if(cls.isTimeBetween(DateTime.Now,_startLunch,_endLunch)
                                            || cls.isTimeBetween(DateTime.Now, _startDinner, _endDinner)
                                            || cls.isTimeBetween(DateTime.Now, _startMidnight, _endMidnight)
                                            || cls.isTimeBetween(DateTime.Now, _startBreakfast, _endBreakfast))
                                        {
                                            ctrl.BackColor = Color.DodgerBlue;
                                        }
                                        else
                                        {
                                            ctrl.BackColor = Color.IndianRed;
                                        }
                                    }
                                    else if(_status == "0")
                                    {
                                        ctrl.BackColor = Color.DodgerBlue;
                                    }
                                    else
                                    {
                                        ctrl.BackColor = Color.IndianRed;
                                    }
                                    //ctrl.BackColor = (_status == "0") ? ((_stop == "1" || totalMinutes >= 5) ? Color.IndianRed : Color.DodgerBlue) : Color.IndianRed;
                                    ctrl.ForeColor = Color.White;
                                    //ctrl.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {

            }

        }

        public void fnLoadRubberStatus()
        {
            try
            {
                string sql = "GET_PRODUCTMONITORING3_VNUSER_RUBBER_MACHINE_STATUS";
                SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Char;
                sParams[0].ParameterName = "@FACTCD";
                sParams[0].Value = factcd;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.SmallInt;
                sParams[1].ParameterName = "@SHIFT";
                sParams[1].Value = shiftsno;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.Char;
                sParams[2].ParameterName = "@WDATE";
                sParams[2].Value = workdate;


                DataSet dsRub = new DataSet();
                dsRub = cls.ExecuteDataSet(sql, CommandType.StoredProcedure, "connVMERP", sParams);
                if(dsRub.Tables.Count>0)
                {
                    countRub = dsRub.Tables[0].Rows.Count;
                    //MessageBox.Show("factcd: " + factcd + "\r\n" + "shiftsno: " + shiftsno + "\r\n" + "workdate: " + workdate + "\r\n" + countRub.ToString());
                    if (countRub > 0)
                    {
                        foreach (Control ctrlRub in flpRub.Controls)
                        {
                            ctrlRub.BackColor = Color.Silver;
                            ctrlRub.ForeColor = Color.LightGray;
                            //ctrlRub.Enabled = false;
                        }

                        for (int i = 0; i < countRub; i++)
                        {
                            _nameRub = dsRub.Tables[0].Rows[i][0].ToString();
                            //_stopRub = ds.Tables[0].Rows[i][9].ToString();
                            //lastUpdateRub = ds.Tables[0].Rows[i][10].ToString();
                            //_lastUpdateRub = (lastUpdate != "" && lastUpdate != null) ? Convert.ToDateTime(ds.Tables[0].Rows[i][10].ToString()) : _dt;
                            //idleTimeRub = DateTime.Now - _lastUpdate;
                            //totalMinutesRub = idleTime.Minutes;
                            _statusRub = dsRub.Tables[0].Rows[i][16].ToString();
                            //_activeRub = (_status == "1") ? "1" : "0";

                            switch (_nameRub.ToLower())
                            {
                                case "rubber 1":
                                    _rubber = "btnRub1";
                                    break;
                                case "rubber 2":
                                    _rubber = "btnRub2";
                                    break;
                                case "rubber 3":
                                    _rubber = "btnRub3";
                                    break;
                                case "rubber 4":
                                    _rubber = "btnRub4";
                                    break;
                                case "rubber 5":
                                    _rubber = "btnRub5";
                                    break;
                                case "rubber 6":
                                    _rubber = "btnRub6";
                                    break;
                                case "rubber 7":
                                    _rubber = "btnRub7";
                                    break;
                                case "rubber 8":
                                    _rubber = "btnRub8";
                                    break;
                                case "rubber 9":
                                    _rubber = "btnRub9";
                                    break;
                                case "rubber 10":
                                    _rubber = "btnRub10";
                                    break;
                                case "rubber 11":
                                    _rubber = "btnRub11";
                                    break;
                                case "rubber 12":
                                    _rubber = "btnRub12";
                                    break;
                                case "rubber 13":
                                    _rubber = "btnRub13";
                                    break;
                                case "rubber 14":
                                    _rubber = "btnRub14";
                                    break;
                                case "rubber 15":
                                    _rubber = "btnRub15";
                                    break;
                                case "rubber 16":
                                    _rubber = "btnRub16";
                                    break;
                                case "rubber 17":
                                    _rubber = "btnRub17";
                                    break;
                                case "rubber 18":
                                    _rubber = "btnRub18";
                                    break;
                                case "rubber 19":
                                    _rubber = "btnRub19";
                                    break;
                                case "rubber 20":
                                    _rubber = "btnRub20";
                                    break;
                            }
                            foreach (Control ctrlRubber in flpRub.Controls)
                            {
                                if (ctrlRubber.Name == _rubber)
                                {
                                    if (_statusRub == "0")
                                    {
                                        ctrlRubber.BackColor = Color.DodgerBlue;
                                    }
                                    else
                                    {
                                        ctrlRubber.BackColor = Color.IndianRed;
                                    }
                                    //ctrl.BackColor = (_status == "0") ? ((_stop == "1" || totalMinutes >= 5) ? Color.IndianRed : Color.DodgerBlue) : Color.IndianRed;
                                    ctrlRubber.ForeColor = Color.White;
                                    //ctrl.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {

            }

        }

        public void fnLoadPlasticStatus()
        {
            try
            {
                string sql = "GET_PRODUCTMONITORING3_VNUSER_PLASTIC_MACHINE_STATUS";
                SqlParameter[] sParams = new SqlParameter[3]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Char;
                sParams[0].ParameterName = "@FACTCD";
                sParams[0].Value = factcd;

                sParams[1] = new SqlParameter();
                sParams[1].SqlDbType = SqlDbType.SmallInt;
                sParams[1].ParameterName = "@SHIFT";
                sParams[1].Value = shiftsno;

                sParams[2] = new SqlParameter();
                sParams[2].SqlDbType = SqlDbType.Char;
                sParams[2].ParameterName = "@WDATE";
                sParams[2].Value = workdate;


                DataSet dsInj = new DataSet();
                dsInj = cls.ExecuteDataSet(sql, CommandType.StoredProcedure, "connVMERP", sParams);
                if(dsInj.Tables.Count>0)
                {
                    countInj = dsInj.Tables[0].Rows.Count;
                    if (countInj > 0)
                    {
                        foreach (Control ctrlInj in flpInj.Controls)
                        {
                            ctrlInj.BackColor = Color.Silver;
                            ctrlInj.ForeColor = Color.LightGray;
                            //ctrlInj.Enabled = false;
                        }

                        for (int i = 0; i < countInj; i++)
                        {
                            _nameInj = dsInj.Tables[0].Rows[i][0].ToString();
                            _statusInj = dsInj.Tables[0].Rows[i][16].ToString();

                            switch (_nameInj.ToLower())
                            {
                                case "plastic 1":
                                    _rubber = "btnInj1";
                                    break;
                                case "plastic 2":
                                    _rubber = "btnInj2";
                                    break;
                                case "plastic 3":
                                    _rubber = "btnInj3";
                                    break;
                                case "plastic 4":
                                    _rubber = "btnInj4";
                                    break;
                                case "plastic 5":
                                    _rubber = "btnInj5";
                                    break;
                                case "plastic 6":
                                    _rubber = "btnInj6";
                                    break;
                                case "plastic 7":
                                    _rubber = "btnInj7";
                                    break;
                                case "plastic 8":
                                    _rubber = "btnInj8";
                                    break;
                                case "plastic 9":
                                    _rubber = "btnInj9";
                                    break;
                                case "plastic 10":
                                    _rubber = "btnInj10";
                                    break;
                                case "plastic 11":
                                    _rubber = "btnInj11";
                                    break;
                                case "plastic 12":
                                    _rubber = "btnInj12";
                                    break;
                                case "plastic 13":
                                    _rubber = "btnInj13";
                                    break;
                                case "plastic 14":
                                    _rubber = "btnInj14";
                                    break;
                                case "plastic 15":
                                    _rubber = "btnInj15";
                                    break;
                                case "plastic 16":
                                    _rubber = "btnInj16";
                                    break;
                                case "plastic 17":
                                    _rubber = "btnInj17";
                                    break;
                                case "plastic 18":
                                    _rubber = "btnInj18";
                                    break;
                                case "plastic 19":
                                    _rubber = "btnInj19";
                                    break;
                                case "plastic 20":
                                    _rubber = "btnInj20";
                                    break;
                            }
                            foreach (Control ctrlInjection in flpInj.Controls)
                            {
                                if (ctrlInjection.Name == _rubber)
                                {
                                    if (_statusInj == "0")
                                    {
                                        ctrlInjection.BackColor = Color.DodgerBlue;
                                    }
                                    else
                                    {
                                        ctrlInjection.BackColor = Color.IndianRed;
                                    }
                                    //ctrl.BackColor = (_status == "0") ? ((_stop == "1" || totalMinutes >= 5) ? Color.IndianRed : Color.DodgerBlue) : Color.IndianRed;
                                    ctrlInjection.ForeColor = Color.White;
                                    //ctrl.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {

            }

        }

        public void fnWorkdate()
        {
            DateTime nNow = DateTime.Now;
            sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");

            if (DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00"))
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0).AddDays(-1);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else if (nNow.TimeOfDay >= TimeSpan.Parse("20:00:00"))
            {

                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 00, 0).AddDays(1);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 18, 30, 0);
                shiftsnm = "Day";
                shiftsno = "1";
            }
            // sTime1 = sTime1.AddDays(-2);
            workdate = sTime1.ToString("yyyyMMdd");
        }

        public void fnRestTime()
        {
            _startLunch = new DateTime(_dt.Year, _dt.Month, _dt.Day, 11, 50, 0);
            _endLunch = new DateTime(_dt.Year, _dt.Month, _dt.Day, 12, 59, 59);
            _startDinner = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 0, 0);
            _endDinner = new DateTime(_dt.Year, _dt.Month, _dt.Day, 17, 39, 59);
            _startMidnight = new DateTime(_dt.Year, _dt.Month, _dt.Day, 0, 0, 0);
            _endMidnight = new DateTime(_dt.Year, _dt.Month, _dt.Day, 0, 59, 59);
            _startBreakfast = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 0, 0);
            _endBreakfast = new DateTime(_dt.Year, _dt.Month, _dt.Day, 5, 39, 59);
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fnExit();
        }

        public void fnExit()
        {
            DialogResult dialogResultAdd = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialogResultAdd == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
