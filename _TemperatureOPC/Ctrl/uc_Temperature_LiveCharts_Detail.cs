using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using System.Windows.Media;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_LiveCharts_Detail : UserControl
    {
        private static uc_Temperature_LiveCharts_Detail _instance;
        public static uc_Temperature_LiveCharts_Detail Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_LiveCharts_Detail();
                return _instance;
            }
        }

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        DateTime _dt;

        int _machine = 1;

        public ChartValues<MeasureModel> ChartValues { get; set; }
        public ChartValues<MeasureModel> ChartValues01 { get; set; }
        public ChartValues<MeasureModel> ChartValues02 { get; set; }
        public ChartValues<MeasureModel> ChartValues03 { get; set; }
        public ChartValues<MeasureModel> ChartValues04 { get; set; }
        public ChartValues<MeasureModel> ChartValues05 { get; set; }
        public ChartValues<MeasureModel> ChartValues06 { get; set; }
        public ChartValues<MeasureModel> ChartValues07 { get; set; }
        public ChartValues<MeasureModel> ChartValues08 { get; set; }
        public ChartValues<MeasureModel> ChartValues09 { get; set; }
        public ChartValues<MeasureModel> ChartValues10 { get; set; }
        public ChartValues<MeasureModel> ChartValues11 { get; set; }
        public ChartValues<MeasureModel> ChartValues12 { get; set; }
        public ChartValues<MeasureModel> ChartValues13 { get; set; }
        public ChartValues<MeasureModel> ChartValues14 { get; set; }
        public ChartValues<MeasureModel> ChartValues15 { get; set; }
        public ChartValues<MeasureModel> ChartValues16 { get; set; }
        public System.Windows.Forms.Timer Timer { get; set; }

        string mc01_sv = "", mc02_sv = "", mc03_sv = "", mc04_sv = "", mc05_sv = "", mc06_sv = "", mc07_sv = "", mc08_sv = "", mc09_sv = "", mc10_sv = "", mc11_sv = "", mc12_sv = "", mc13_sv = "", mc14_sv = "", mc15_sv = "", mc16_sv = "";

        string mc01_pv = "", mc02_pv = "", mc03_pv = "", mc04_pv = "", mc05_pv = "", mc06_pv = "", mc07_pv = "", mc08_pv = "", mc09_pv = "", mc10_pv = "", mc11_pv = "", mc12_pv = "", mc13_pv = "", mc14_pv = "", mc15_pv = "", mc16_pv = "";
        string mc01_al = "", mc02_al = "", mc03_al = "", mc04_al = "", mc05_al = "", mc06_al = "", mc07_al = "", mc08_al = "", mc09_al = "", mc10_al = "", mc11_al = "", mc12_al = "", mc13_al = "", mc14_al = "", mc15_al = "", mc16_al = "";
        string premc01_pv = "", premc02_pv = "", premc03_pv = "", premc04_pv = "", premc05_pv = "", premc06_pv = "", premc07_pv = "", premc08_pv = "", premc09_pv = "", premc10_pv = "", premc11_pv = "", premc12_pv = "", premc13_pv = "", premc14_pv = "", premc15_pv = "", premc16_pv = "";

        double _mc01_sv = 0, _mc02_sv = 0, _mc03_sv = 0, _mc04_sv = 0, _mc05_sv = 0, _mc06_sv = 0, _mc07_sv = 0, _mc08_sv = 0, _mc09_sv = 0, _mc10_sv = 0, _mc11_sv = 0, _mc12_sv = 0, _mc13_sv = 0, _mc14_sv = 0, _mc15_sv = 0, _mc16_sv = 0;
        double _mc01_pv = 0, _mc02_pv = 0, _mc03_pv = 0, _mc04_pv = 0, _mc05_pv = 0, _mc06_pv = 0, _mc07_pv = 0, _mc08_pv = 0, _mc09_pv = 0, _mc10_pv = 0, _mc11_pv = 0, _mc12_pv = 0, _mc13_pv = 0, _mc14_pv = 0, _mc15_pv = 0, _mc16_pv = 0;
        double _mc01_al = 0, _mc02_al = 0, _mc03_al = 0, _mc04_al = 0, _mc05_al = 0, _mc06_al = 0, _mc07_al = 0, _mc08_al = 0, _mc09_al = 0, _mc10_al = 0, _mc11_al = 0, _mc12_al = 0, _mc13_al = 0, _mc14_al = 0, _mc15_al = 0, _mc16_al = 0;

        public uc_Temperature_LiveCharts_Detail()
        {
            InitializeComponent();

            fnLoadData();
            init_Load_Chart(_machine);
        }

        private void uc_Temperature_LiveCharts_Detail_Load(object sender, EventArgs e)
        {
            
        }

        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
        }


        #region MACHINE TEMPERATUTE DETAIL

        public void init_Load_Last_Data()
        {
            string sv = "", pv = "";
            string R01_SV = "", R02_SV = "", R03_SV = "", R04_SV = "", R05_SV = "", R06_SV = "", R07_SV = "", R08_SV = "", R09_SV = "", R10_SV = "", R11_SV = "", R12_SV = "", R13_SV = "", R14_SV = "", R15_SV = "", R16_SV = "";
            string R01_PV = "", R02_PV = "", R03_PV = "", R04_PV = "", R05_PV = "", R06_PV = "", R07_PV = "", R08_PV = "", R09_PV = "", R10_PV = "", R11_PV = "", R12_PV = "", R13_PV = "", R14_PV = "", R15_PV = "", R16_PV = "";
            double _R01_SV = 0, _R02_SV = 0, _R03_SV = 0, _R04_SV = 0, _R05_SV = 0, _R06_SV = 0, _R07_SV = 0, _R08_SV = 0, _R09_SV = 0, _R10_SV = 0, _R11_SV = 0, _R12_SV = 0, _R13_SV = 0, _R14_SV = 0, _R15_SV = 0, _R16_SV = 0;
            double _R01_PV = 0, _R02_PV = 0, _R03_PV = 0, _R04_PV = 0, _R05_PV = 0, _R06_PV = 0, _R07_PV = 0, _R08_PV = 0, _R09_PV = 0, _R10_PV = 0, _R11_PV = 0, _R12_PV = 0, _R13_PV = 0, _R14_PV = 0, _R15_PV = 0, _R16_PV = 0;

            string sql = "V2o1_ERP_Temperature_Init_Machine_Last_Data_SelItem_V1o0_Addnew";

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                sv = ds.Tables[0].Rows[0][0].ToString();
                pv = ds.Tables[0].Rows[0][1].ToString();

                R01_SV = sv.Substring(0, 4); R02_SV = sv.Substring(4, 4); R03_SV = sv.Substring(8, 4); R04_SV = sv.Substring(12, 4);
                R05_SV = sv.Substring(16, 4); R06_SV = sv.Substring(20, 4); R07_SV = sv.Substring(24, 4); R08_SV = sv.Substring(28, 4);
                R09_SV = sv.Substring(32, 4); R10_SV = sv.Substring(36, 4); R11_SV = sv.Substring(40, 4); R12_SV = sv.Substring(44, 4);
                R13_SV = sv.Substring(48, 4); R14_SV = sv.Substring(52, 4); R15_SV = sv.Substring(56, 4); R16_SV = sv.Substring(60, 4);

                R01_PV = pv.Substring(0, 4); R02_PV = pv.Substring(4, 4); R03_PV = pv.Substring(8, 4); R04_PV = pv.Substring(12, 4);
                R05_PV = pv.Substring(16, 4); R06_PV = pv.Substring(20, 4); R07_PV = pv.Substring(24, 4); R08_PV = pv.Substring(28, 4);
                R09_PV = pv.Substring(32, 4); R10_PV = pv.Substring(36, 4); R11_PV = pv.Substring(40, 4); R12_PV = pv.Substring(44, 4);
                R13_PV = pv.Substring(48, 4); R14_PV = pv.Substring(52, 4); R15_PV = pv.Substring(56, 4); R16_PV = pv.Substring(60, 4);

                //string msg = "";
                //msg += "R01_SV: " + R01_SV + "\r\n"; msg += "R02_SV: " + R02_SV + "\r\n"; msg += "R03_SV: " + R03_SV + "\r\n"; msg += "R04_SV: " + R04_SV + "\r\n";
                //msg += "R05_SV: " + R05_SV + "\r\n"; msg += "R06_SV: " + R06_SV + "\r\n"; msg += "R07_SV: " + R07_SV + "\r\n"; msg += "R08_SV: " + R08_SV + "\r\n";
                //msg += "R09_SV: " + R09_SV + "\r\n"; msg += "R10_SV: " + R10_SV + "\r\n"; msg += "R11_SV: " + R11_SV + "\r\n"; msg += "R12_SV: " + R12_SV + "\r\n";
                //msg += "R13_SV: " + R13_SV + "\r\n"; msg += "R14_SV: " + R14_SV + "\r\n"; msg += "R15_SV: " + R15_SV + "\r\n"; msg += "R16_SV: " + R16_SV + "\r\n";

                //msg += "-------------------------- \r\n";

                //msg += "R01_PV: " + R01_PV + "\r\n"; msg += "R02_PV: " + R02_PV + "\r\n"; msg += "R03_PV: " + R03_PV + "\r\n"; msg += "R04_PV: " + R04_PV + "\r\n";
                //msg += "R05_PV: " + R05_PV + "\r\n"; msg += "R06_PV: " + R06_PV + "\r\n"; msg += "R07_PV: " + R07_PV + "\r\n"; msg += "R08_PV: " + R08_PV + "\r\n";
                //msg += "R09_PV: " + R09_PV + "\r\n"; msg += "R10_PV: " + R10_PV + "\r\n"; msg += "R11_PV: " + R11_PV + "\r\n"; msg += "R12_PV: " + R12_PV + "\r\n";
                //msg += "R13_PV: " + R13_PV + "\r\n"; msg += "R14_PV: " + R14_PV + "\r\n"; msg += "R15_PV: " + R15_PV + "\r\n"; msg += "R16_PV: " + R16_PV + "\r\n";
                //MessageBox.Show(msg);
            }

            //_R01_SV = _mc01_sv = (R01_SV != "" && R01_SV != null) ? Convert.ToDouble(R01_SV) : 0; _R02_SV = _mc02_sv = (R02_SV != "" && R02_SV != null) ? Convert.ToDouble(R02_SV) : 0;
            //_R03_SV = _mc03_sv = (R03_SV != "" && R03_SV != null) ? Convert.ToDouble(R03_SV) : 0; _R04_SV = _mc04_sv = (R04_SV != "" && R04_SV != null) ? Convert.ToDouble(R04_SV) : 0;
            //_R05_SV = _mc05_sv = (R05_SV != "" && R05_SV != null) ? Convert.ToDouble(R05_SV) : 0; _R06_SV = _mc06_sv = (R06_SV != "" && R06_SV != null) ? Convert.ToDouble(R06_SV) : 0;
            //_R07_SV = _mc07_sv = (R07_SV != "" && R07_SV != null) ? Convert.ToDouble(R07_SV) : 0; _R08_SV = _mc08_sv = (R08_SV != "" && R08_SV != null) ? Convert.ToDouble(R08_SV) : 0;
            //_R09_SV = _mc09_sv = (R09_SV != "" && R09_SV != null) ? Convert.ToDouble(R09_SV) : 0; _R10_SV = _mc10_sv = (R10_SV != "" && R10_SV != null) ? Convert.ToDouble(R10_SV) : 0;
            //_R11_SV = _mc11_sv = (R11_SV != "" && R11_SV != null) ? Convert.ToDouble(R11_SV) : 0; _R12_SV = _mc12_sv = (R12_SV != "" && R12_SV != null) ? Convert.ToDouble(R12_SV) : 0;
            //_R13_SV = _mc13_sv = (R13_SV != "" && R13_SV != null) ? Convert.ToDouble(R13_SV) : 0; _R14_SV = _mc14_sv = (R14_SV != "" && R14_SV != null) ? Convert.ToDouble(R14_SV) : 0;
            //_R15_SV = _mc15_sv = (R15_SV != "" && R15_SV != null) ? Convert.ToDouble(R15_SV) : 0; _R16_SV = _mc16_sv = (R16_SV != "" && R16_SV != null) ? Convert.ToDouble(R16_SV) : 0;

            //_R01_PV = _mc01_pv = (R01_PV != "" && R01_PV != null) ? Convert.ToDouble(R01_PV) : 0; _R02_PV = _mc02_pv = (R02_PV != "" && R02_PV != null) ? Convert.ToDouble(R02_PV) : 0;
            //_R03_PV = _mc03_pv = (R03_PV != "" && R03_PV != null) ? Convert.ToDouble(R03_PV) : 0; _R04_PV = _mc04_pv = (R04_PV != "" && R04_PV != null) ? Convert.ToDouble(R04_PV) : 0;
            //_R05_PV = _mc05_pv = (R05_PV != "" && R05_PV != null) ? Convert.ToDouble(R05_PV) : 0; _R06_PV = _mc06_pv = (R06_PV != "" && R06_PV != null) ? Convert.ToDouble(R06_PV) : 0;
            //_R07_PV = _mc07_pv = (R07_PV != "" && R07_PV != null) ? Convert.ToDouble(R07_PV) : 0; _R08_PV = _mc08_pv = (R08_PV != "" && R08_PV != null) ? Convert.ToDouble(R08_PV) : 0;
            //_R09_PV = _mc09_pv = (R09_PV != "" && R09_PV != null) ? Convert.ToDouble(R09_PV) : 0; _R10_PV = _mc10_pv = (R10_PV != "" && R10_PV != null) ? Convert.ToDouble(R10_PV) : 0;
            //_R11_PV = _mc11_pv = (R11_PV != "" && R11_PV != null) ? Convert.ToDouble(R11_PV) : 0; _R12_PV = _mc12_pv = (R12_PV != "" && R12_PV != null) ? Convert.ToDouble(R12_PV) : 0;
            //_R13_PV = _mc13_pv = (R13_PV != "" && R13_PV != null) ? Convert.ToDouble(R13_PV) : 0; _R14_PV = _mc14_pv = (R14_PV != "" && R14_PV != null) ? Convert.ToDouble(R14_PV) : 0;
            //_R15_PV = _mc15_pv = (R15_PV != "" && R15_PV != null) ? Convert.ToDouble(R15_PV) : 0; _R16_PV = _mc16_pv = (R16_PV != "" && R16_PV != null) ? Convert.ToDouble(R16_PV) : 0;

            _R01_SV = (R01_SV != "" && R01_SV != null) ? Convert.ToDouble(R01_SV) : 0; _R02_SV = (R02_SV != "" && R02_SV != null) ? Convert.ToDouble(R02_SV) : 0;
            _R03_SV = (R03_SV != "" && R03_SV != null) ? Convert.ToDouble(R03_SV) : 0; _R04_SV = (R04_SV != "" && R04_SV != null) ? Convert.ToDouble(R04_SV) : 0;
            _R05_SV = (R05_SV != "" && R05_SV != null) ? Convert.ToDouble(R05_SV) : 0; _R06_SV = (R06_SV != "" && R06_SV != null) ? Convert.ToDouble(R06_SV) : 0;
            _R07_SV = (R07_SV != "" && R07_SV != null) ? Convert.ToDouble(R07_SV) : 0; _R08_SV = (R08_SV != "" && R08_SV != null) ? Convert.ToDouble(R08_SV) : 0;
            _R09_SV = (R09_SV != "" && R09_SV != null) ? Convert.ToDouble(R09_SV) : 0; _R10_SV = (R10_SV != "" && R10_SV != null) ? Convert.ToDouble(R10_SV) : 0;
            _R11_SV = (R11_SV != "" && R11_SV != null) ? Convert.ToDouble(R11_SV) : 0; _R12_SV = (R12_SV != "" && R12_SV != null) ? Convert.ToDouble(R12_SV) : 0;
            _R13_SV = (R13_SV != "" && R13_SV != null) ? Convert.ToDouble(R13_SV) : 0; _R14_SV = (R14_SV != "" && R14_SV != null) ? Convert.ToDouble(R14_SV) : 0;
            _R15_SV = (R15_SV != "" && R15_SV != null) ? Convert.ToDouble(R15_SV) : 0; _R16_SV = (R16_SV != "" && R16_SV != null) ? Convert.ToDouble(R16_SV) : 0;

            _R01_PV = (R01_PV != "" && R01_PV != null) ? Convert.ToDouble(R01_PV) : 0; _R02_PV = (R02_PV != "" && R02_PV != null) ? Convert.ToDouble(R02_PV) : 0;
            _R03_PV = (R03_PV != "" && R03_PV != null) ? Convert.ToDouble(R03_PV) : 0; _R04_PV = (R04_PV != "" && R04_PV != null) ? Convert.ToDouble(R04_PV) : 0;
            _R05_PV = (R05_PV != "" && R05_PV != null) ? Convert.ToDouble(R05_PV) : 0; _R06_PV = (R06_PV != "" && R06_PV != null) ? Convert.ToDouble(R06_PV) : 0;
            _R07_PV = (R07_PV != "" && R07_PV != null) ? Convert.ToDouble(R07_PV) : 0; _R08_PV = (R08_PV != "" && R08_PV != null) ? Convert.ToDouble(R08_PV) : 0;
            _R09_PV = (R09_PV != "" && R09_PV != null) ? Convert.ToDouble(R09_PV) : 0; _R10_PV = (R10_PV != "" && R10_PV != null) ? Convert.ToDouble(R10_PV) : 0;
            _R11_PV = (R11_PV != "" && R11_PV != null) ? Convert.ToDouble(R11_PV) : 0; _R12_PV = (R12_PV != "" && R12_PV != null) ? Convert.ToDouble(R12_PV) : 0;
            _R13_PV = (R13_PV != "" && R13_PV != null) ? Convert.ToDouble(R13_PV) : 0; _R14_PV = (R14_PV != "" && R14_PV != null) ? Convert.ToDouble(R14_PV) : 0;
            _R15_PV = (R15_PV != "" && R15_PV != null) ? Convert.ToDouble(R15_PV) : 0; _R16_PV = (R16_PV != "" && R16_PV != null) ? Convert.ToDouble(R16_PV) : 0;

            lbl_R01_SV.Text = _R01_SV.ToString() + (char)176 + "C"; lbl_R02_SV.Text = _R02_SV.ToString() + (char)176 + "C";
            lbl_R03_SV.Text = _R03_SV.ToString() + (char)176 + "C"; lbl_R04_SV.Text = _R04_SV.ToString() + (char)176 + "C";
            lbl_R05_SV.Text = _R05_SV.ToString() + (char)176 + "C"; lbl_R06_SV.Text = _R06_SV.ToString() + (char)176 + "C";
            lbl_R07_SV.Text = _R07_SV.ToString() + (char)176 + "C"; lbl_R08_SV.Text = _R08_SV.ToString() + (char)176 + "C";
            lbl_R09_SV.Text = _R09_SV.ToString() + (char)176 + "C"; lbl_R10_SV.Text = _R10_SV.ToString() + (char)176 + "C";
            lbl_R11_SV.Text = _R11_SV.ToString() + (char)176 + "C"; lbl_R12_SV.Text = _R12_SV.ToString() + (char)176 + "C";
            lbl_R13_SV.Text = _R13_SV.ToString() + (char)176 + "C"; lbl_R14_SV.Text = _R14_SV.ToString() + (char)176 + "C";
            lbl_R15_SV.Text = _R15_SV.ToString() + (char)176 + "C"; lbl_R16_SV.Text = _R16_SV.ToString() + (char)176 + "C";

            lbl_R01_PV.Text = _R01_PV.ToString() + (char)176 + "C"; lbl_R02_PV.Text = _R02_PV.ToString() + (char)176 + "C";
            lbl_R03_PV.Text = _R03_PV.ToString() + (char)176 + "C"; lbl_R04_PV.Text = _R04_PV.ToString() + (char)176 + "C";
            lbl_R05_PV.Text = _R05_PV.ToString() + (char)176 + "C"; lbl_R06_PV.Text = _R06_PV.ToString() + (char)176 + "C";
            lbl_R07_PV.Text = _R07_PV.ToString() + (char)176 + "C"; lbl_R08_PV.Text = _R08_PV.ToString() + (char)176 + "C";
            lbl_R09_PV.Text = _R09_PV.ToString() + (char)176 + "C"; lbl_R10_PV.Text = _R10_PV.ToString() + (char)176 + "C";
            lbl_R11_PV.Text = _R11_PV.ToString() + (char)176 + "C"; lbl_R12_PV.Text = _R12_PV.ToString() + (char)176 + "C";
            lbl_R13_PV.Text = _R13_PV.ToString() + (char)176 + "C"; lbl_R14_PV.Text = _R14_PV.ToString() + (char)176 + "C";
            lbl_R15_PV.Text = _R15_PV.ToString() + (char)176 + "C"; lbl_R16_PV.Text = _R16_PV.ToString() + (char)176 + "C";
        }

        public void init_Load_Chart(int machine)
        {

            double sv = 0;
            switch (machine)
            {
                case 1:
                    sv = _mc01_sv;
                    break;
                case 2:
                    sv = _mc02_sv;
                    break;
                case 3:
                    sv = _mc03_sv;
                    break;
                case 4:
                    sv = _mc04_sv;
                    break;
                case 5:
                    sv = _mc05_sv;
                    break;
                case 6:
                    sv = _mc06_sv;
                    break;
                case 7:
                    sv = _mc07_sv;
                    break;
                case 8:
                    sv = _mc08_sv;
                    break;
                case 9:
                    sv = _mc09_sv;
                    break;
                case 10:
                    sv = _mc10_sv;
                    break;
                case 11:
                    sv = _mc11_sv;
                    break;
                case 12:
                    sv = _mc12_sv;
                    break;
                case 13:
                    sv = _mc13_sv;
                    break;
                case 14:
                    sv = _mc14_sv;
                    break;
                case 15:
                    sv = _mc15_sv;
                    break;
                case 16:
                    sv = _mc16_sv;
                    break;
            }

            //To handle live data easily, in this case we built a specialized type
            //the MeasureModel class, it only contains 2 properties
            //DateTime and Value
            //We need to configure LiveCharts to handle MeasureModel class
            //The next code configures MEasureModel  globally, this means
            //that livecharts learns to plot MeasureModel and will use this config every time
            //a ChartValues instance uses this type.
            //this code ideally should only run once, when application starts is reccomended.
            //you can configure series in many ways, learn more at http://lvcharts.net/App/examples/v1/wpf/Types%20and%20Configuration

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the ChartValues property will store our values array
            ChartValues = new ChartValues<MeasureModel>();
            chr_Temperature.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = ChartValues,
                    //PointGeometrySize = 18,
                    //StrokeThickness = 4
                    PointGeometrySize = 2,
                    StrokeThickness = 2
                }
            };
            chr_Temperature.AxisX.Add(new Axis
            {
                DisableAnimations = true,
                //LabelFormatter = value => new System.DateTime((long)value).ToString("mm:ss"),
                LabelFormatter = value => new System.DateTime((long)value).ToString("HH:mm"),
                Separator = new Separator
                {
                    //Step = TimeSpan.FromSeconds(1).Ticks
                    Step = TimeSpan.FromHours(1).Ticks
                }
            });
            chr_Temperature.AxisY.Add(new Axis
            {
                Sections = new SectionsCollection
                {
                    //new AxisSection
                    //{
                    //    //Value = 8.5,
                    //    //Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(248, 213, 72))
                    //    Label = "NG",
                    //    Value = 3000,
                    //    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(254,132,132))
                    //},
                    new AxisSection
                    {
                        //Label = "OK",
                        //Value = 182,
                        Value = (sv + 5),
                        SectionWidth = 0.1,
                        Fill = new SolidColorBrush
                        {
                            //Color = System.Windows.Media.Color.FromRgb(204,204,204),
                            Color = System.Windows.Media.Color.FromRgb(255,0,0),
                            Opacity = .4
                        }
                    },
                    new AxisSection
                    {
                        //Label = "OK",
                        //Value = 182,
                        Value = sv,
                        SectionWidth = 0.1,
                        Fill = new SolidColorBrush
                        {
                            //Color = System.Windows.Media.Color.FromRgb(204,204,204),
                            Color = System.Windows.Media.Color.FromRgb(51, 204, 255),
                            Opacity = .4
                        }
                    },
                    new AxisSection
                    {
                        //Label = "NG",
                        //Value = 178.9,
                        Value = (sv-5.1),
                        SectionWidth = 0.1,
                        Fill = new SolidColorBrush
                        {
                            //Color = System.Windows.Media.Color.FromRgb(254,132,132),
                            Color = System.Windows.Media.Color.FromRgb(255,0,0),
                            Opacity = .4
                        }
                    }
                }
            });

            SetAxisLimits(System.DateTime.Now, sv);

            //Initialize data at beginning open program
            //init_Data_InDay(chart, chartvalue);

            //ChartValues.Clear();
            
            init_Data_MachineDay(_machine);

            //The next code simulates data changes every 500 ms
            Timer = new System.Windows.Forms.Timer
            {
                //Interval = 500
                Interval = 5000
            };
            Timer.Tick += (sender, eventArgs) => TimerOnTick(sender, eventArgs, sv, _machine);
            //Rand = new Random();
            Timer.Start();
        }

        private void SetAxisLimits(System.DateTime now, double sv)
        {
            //cartesianChart1.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 100ms ahead
            //cartesianChart1.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds

            chr_Temperature.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromHours(1).Ticks; // lets force the axis to be 100ms ahead
            chr_Temperature.AxisX[0].MinValue = now.Ticks - TimeSpan.FromHours(24).Ticks; //we only care about the last 8 seconds

            //chart.AxisY[0].MaxValue = 182; // lets force the axis to be 100ms ahead
            //chart.AxisY[0].MinValue = 179; //we only care about the last 8 seconds

            double maxSV = (sv + 10.0);
            double minSV = (sv - 10.0);

            chr_Temperature.AxisY[0].MaxValue = maxSV; // lets force the axis to be 100ms ahead
            chr_Temperature.AxisY[0].MinValue = minSV; //we only care about the last 8 seconds

        }

        private void TimerOnTick(object sender, EventArgs eventArgs, double sv, int machine)
        {
            //init_Load_Data();
            var now = System.DateTime.Now;


            switch (machine)
            {
                case 1:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc01_pv
                    });
                    lbl_R01_SV.Text = _mc01_sv + "" + (char)176 + "C";
                    lbl_R01_PV.Text = _mc01_pv + "" + (char)176 + "C";
                    break;
                case 2:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc02_pv
                    });
                    lbl_R02_SV.Text = _mc02_sv + "" + (char)176 + "C";
                    lbl_R02_PV.Text = _mc02_pv + "" + (char)176 + "C";
                    break;
                case 3:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc03_pv
                    });
                    lbl_R03_SV.Text = _mc03_sv + "" + (char)176 + "C";
                    lbl_R03_PV.Text = _mc03_pv + "" + (char)176 + "C";
                    break;
                case 4:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc04_pv
                    });
                    lbl_R04_SV.Text = _mc04_sv + "" + (char)176 + "C";
                    lbl_R04_PV.Text = _mc04_pv + "" + (char)176 + "C";
                    break;
                case 5:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc05_pv
                    });
                    lbl_R05_SV.Text = _mc05_sv + "" + (char)176 + "C";
                    lbl_R05_PV.Text = _mc05_pv + "" + (char)176 + "C";
                    break;
                case 6:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc06_pv
                    });
                    lbl_R06_SV.Text = _mc06_sv + "" + (char)176 + "C";
                    lbl_R06_PV.Text = _mc06_pv + "" + (char)176 + "C";
                    break;
                case 7:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc07_pv
                    });
                    lbl_R07_SV.Text = _mc07_sv + "" + (char)176 + "C";
                    lbl_R07_PV.Text = _mc07_pv + "" + (char)176 + "C";
                    break;
                case 8:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc08_pv
                    });
                    lbl_R08_SV.Text = _mc08_sv + "" + (char)176 + "C";
                    lbl_R08_PV.Text = _mc08_pv + "" + (char)176 + "C";
                    break;
                case 9:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc09_pv
                    });
                    lbl_R09_SV.Text = _mc09_sv + "" + (char)176 + "C";
                    lbl_R09_PV.Text = _mc09_pv + "" + (char)176 + "C";
                    break;
                case 10:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc10_pv
                    });
                    lbl_R10_SV.Text = _mc10_sv + "" + (char)176 + "C";
                    lbl_R10_PV.Text = _mc10_pv + "" + (char)176 + "C";
                    break;
                case 11:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc11_pv
                    });
                    lbl_R11_SV.Text = _mc11_sv + "" + (char)176 + "C";
                    lbl_R11_PV.Text = _mc11_pv + "" + (char)176 + "C";
                    break;
                case 12:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc12_pv
                    });
                    lbl_R12_SV.Text = _mc12_sv + "" + (char)176 + "C";
                    lbl_R12_PV.Text = _mc12_pv + "" + (char)176 + "C";
                    break;
                case 13:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc13_pv
                    });
                    lbl_R13_SV.Text = _mc13_sv + "" + (char)176 + "C";
                    lbl_R13_PV.Text = _mc13_pv + "" + (char)176 + "C";
                    break;
                case 14:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc14_pv
                    });
                    lbl_R14_SV.Text = _mc14_sv + "" + (char)176 + "C";
                    lbl_R14_PV.Text = _mc14_pv + "" + (char)176 + "C";
                    break;
                case 15:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc15_pv
                    });
                    lbl_R15_SV.Text = _mc15_sv + "" + (char)176 + "C";
                    lbl_R15_PV.Text = _mc15_pv + "" + (char)176 + "C";
                    break;
                case 16:
                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc16_pv
                    });
                    lbl_R16_SV.Text = _mc16_sv + "" + (char)176 + "C";
                    lbl_R16_PV.Text = _mc16_pv + "" + (char)176 + "C";
                    break;
            }

            SetAxisLimits(now, sv);

            //lets only use the last 30 values
            //if (chartvalue.Count > 182) chartvalue.RemoveAt(0);
        }

        public void init_Data_MachineDay(int machine)
        {
            //int mc = Convert.ToInt32(cls.Right(chart.Name, 2));
            string col_sv = "", col_pv = "", col_dt = "";
            double _col_sv = 0, _col_pv = 0;
            DateTime _col_dt = _dt;
            int rows = 0;
            int i;

            string sql = "V2o1_ERP_Temperature_Init_Machine_SelItem_V1o0_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@mc";
            sParams[0].Value = machine;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rows = ds.Tables[0].Rows.Count;
                for (i = 0; i < rows; i++)
                {
                    col_sv = ds.Tables[0].Rows[i][1].ToString();
                    col_pv = ds.Tables[0].Rows[i][2].ToString();
                    //col_al = ds.Tables[0].Rows[i][3].ToString();
                    col_dt = ds.Tables[0].Rows[i][4].ToString();

                    _col_sv = (col_sv != "" && col_sv != null) ? Convert.ToDouble(col_sv) : 0;
                    _col_pv = (col_pv != "" && col_pv != null) ? Convert.ToDouble(col_pv) : 0;
                    //_col_al = (col_al != "" && col_al != null) ? Convert.ToBoolean(col_al) : false;
                    _col_dt = (col_dt != "" && col_dt != null) ? Convert.ToDateTime(col_dt) : _dt;

                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = _col_dt,
                        Value = _col_pv
                    });
                    SetAxisLimits(_col_dt, _col_sv);
                }
            }
        }

        public void init_Load_Data()
        {
            try
            {
                string sql = "V2o1_ERP_Temperature_Garthering_SelItem_V1o0_Addnew";
                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string temp_SV = ds.Tables[0].Rows[0][0].ToString();
                    string temp_PV = ds.Tables[0].Rows[0][1].ToString();
                    string temp_AL = ds.Tables[0].Rows[0][2].ToString();
                    string temp_DT = ds.Tables[0].Rows[0][3].ToString();

                    //string mc01_sv = "", mc02_sv = "", mc03_sv = "", mc04_sv = "", mc05_sv = "", mc06_sv = "", mc07_sv = "", mc08_sv = "", mc09_sv = "", mc10_sv = "", mc11_sv = "", mc12_sv = "", mc13_sv = "", mc14_sv = "", mc15_sv = "", mc16_sv = "";
                    //string mc01_pv = "", mc02_pv = "", mc03_pv = "", mc04_pv = "", mc05_pv = "", mc06_pv = "", mc07_pv = "", mc08_pv = "", mc09_pv = "", mc10_pv = "", mc11_pv = "", mc12_pv = "", mc13_pv = "", mc14_pv = "", mc15_pv = "", mc16_pv = "";
                    //string mc01_al = "", mc02_al = "", mc03_al = "", mc04_al = "", mc05_al = "", mc06_al = "", mc07_al = "", mc08_al = "", mc09_al = "", mc10_al = "", mc11_al = "", mc12_al = "", mc13_al = "", mc14_al = "", mc15_al = "", mc16_al = "";

                    //double _mc01_sv = 0, _mc02_sv = 0, _mc03_sv = 0, _mc04_sv = 0, _mc05_sv = 0, _mc06_sv = 0, _mc07_sv = 0, _mc08_sv = 0, _mc09_sv = 0, _mc10_sv = 0, _mc11_sv = 0, _mc12_sv = 0, _mc13_sv = 0, _mc14_sv = 0, _mc15_sv = 0, _mc16_sv = 0;
                    //double _mc01_pv = 0, _mc02_pv = 0, _mc03_pv = 0, _mc04_pv = 0, _mc05_pv = 0, _mc06_pv = 0, _mc07_pv = 0, _mc08_pv = 0, _mc09_pv = 0, _mc10_pv = 0, _mc11_pv = 0, _mc12_pv = 0, _mc13_pv = 0, _mc14_pv = 0, _mc15_pv = 0, _mc16_pv = 0;
                    //double _mc01_al = 0, _mc02_al = 0, _mc03_al = 0, _mc04_al = 0, _mc05_al = 0, _mc06_al = 0, _mc07_al = 0, _mc08_al = 0, _mc09_al = 0, _mc10_al = 0, _mc11_al = 0, _mc12_al = 0, _mc13_al = 0, _mc14_al = 0, _mc15_al = 0, _mc16_al = 0;

                    mc01_sv = temp_SV.Substring(0, 4);
                    mc02_sv = temp_SV.Substring(4, 4);
                    mc03_sv = temp_SV.Substring(8, 4);
                    mc04_sv = temp_SV.Substring(12, 4);
                    mc05_sv = temp_SV.Substring(16, 4);
                    mc06_sv = temp_SV.Substring(20, 4);
                    mc07_sv = temp_SV.Substring(24, 4);
                    mc08_sv = temp_SV.Substring(28, 4);
                    mc09_sv = temp_SV.Substring(32, 4);
                    mc10_sv = temp_SV.Substring(36, 4);
                    mc11_sv = temp_SV.Substring(40, 4);
                    mc12_sv = temp_SV.Substring(44, 4);
                    mc13_sv = temp_SV.Substring(48, 4);
                    mc14_sv = temp_SV.Substring(52, 4);
                    mc15_sv = temp_SV.Substring(56, 4);
                    mc16_sv = temp_SV.Substring(60, 4);

                    _mc01_sv = (mc01_sv != "0" && mc01_sv != null) ? Convert.ToDouble(mc01_sv) : 0;
                    _mc02_sv = (mc02_sv != "0" && mc02_sv != null) ? Convert.ToDouble(mc02_sv) : 0;
                    _mc03_sv = (mc03_sv != "0" && mc03_sv != null) ? Convert.ToDouble(mc03_sv) : 0;
                    _mc04_sv = (mc04_sv != "0" && mc04_sv != null) ? Convert.ToDouble(mc04_sv) : 0;
                    _mc05_sv = (mc05_sv != "0" && mc05_sv != null) ? Convert.ToDouble(mc05_sv) : 0;
                    _mc06_sv = (mc06_sv != "0" && mc06_sv != null) ? Convert.ToDouble(mc06_sv) : 0;
                    _mc07_sv = (mc07_sv != "0" && mc07_sv != null) ? Convert.ToDouble(mc07_sv) : 0;
                    _mc08_sv = (mc08_sv != "0" && mc08_sv != null) ? Convert.ToDouble(mc08_sv) : 0;
                    _mc09_sv = (mc09_sv != "0" && mc09_sv != null) ? Convert.ToDouble(mc09_sv) : 0;
                    _mc10_sv = (mc10_sv != "0" && mc10_sv != null) ? Convert.ToDouble(mc10_sv) : 0;
                    _mc11_sv = (mc11_sv != "0" && mc11_sv != null) ? Convert.ToDouble(mc11_sv) : 0;
                    _mc12_sv = (mc12_sv != "0" && mc12_sv != null) ? Convert.ToDouble(mc12_sv) : 0;
                    _mc13_sv = (mc13_sv != "0" && mc13_sv != null) ? Convert.ToDouble(mc13_sv) : 0;
                    _mc14_sv = (mc14_sv != "0" && mc14_sv != null) ? Convert.ToDouble(mc14_sv) : 0;
                    _mc15_sv = (mc15_sv != "0" && mc15_sv != null) ? Convert.ToDouble(mc15_sv) : 0;
                    _mc16_sv = (mc16_sv != "0" && mc16_sv != null) ? Convert.ToDouble(mc16_sv) : 0;

                    //R01_SV.Text = "SV: " + _mc01_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R02_SV.Text = "SV: " + _mc02_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R03_SV.Text = "SV: " + _mc03_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R04_SV.Text = "SV: " + _mc04_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R05_SV.Text = "SV: " + _mc05_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R06_SV.Text = "SV: " + _mc06_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R07_SV.Text = "SV: " + _mc07_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R08_SV.Text = "SV: " + _mc08_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R09_SV.Text = "SV: " + _mc09_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R10_SV.Text = "SV: " + _mc10_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R11_SV.Text = "SV: " + _mc11_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R12_SV.Text = "SV: " + _mc12_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R13_SV.Text = "SV: " + _mc13_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R14_SV.Text = "SV: " + _mc14_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R15_SV.Text = "SV: " + _mc15_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";
                    //R16_SV.Text = "SV: " + _mc16_sv.ToString() + "" + (char)176 + "C " + (char)177 + " 5";

                    //R01_SV.Text = "SV: " + _mc01_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R02_SV.Text = "SV: " + _mc02_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R03_SV.Text = "SV: " + _mc03_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R04_SV.Text = "SV: " + _mc04_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R05_SV.Text = "SV: " + _mc05_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R06_SV.Text = "SV: " + _mc06_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R07_SV.Text = "SV: " + _mc07_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R08_SV.Text = "SV: " + _mc08_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R09_SV.Text = "SV: " + _mc09_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R10_SV.Text = "SV: " + _mc10_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R11_SV.Text = "SV: " + _mc11_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R12_SV.Text = "SV: " + _mc12_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R13_SV.Text = "SV: " + _mc13_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R14_SV.Text = "SV: " + _mc14_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R15_SV.Text = "SV: " + _mc15_sv.ToString() + "" + (char)176 + "C +2/-1";
                    //R16_SV.Text = "SV: " + _mc16_sv.ToString() + "" + (char)176 + "C +2/-1";

                    /**************************************/

                    premc01_pv = mc01_pv;
                    premc02_pv = mc02_pv;
                    premc03_pv = mc03_pv;
                    premc04_pv = mc04_pv;
                    premc05_pv = mc05_pv;
                    premc06_pv = mc06_pv;
                    premc07_pv = mc07_pv;
                    premc08_pv = mc08_pv;
                    premc09_pv = mc09_pv;
                    premc10_pv = mc10_pv;
                    premc11_pv = mc11_pv;
                    premc12_pv = mc12_pv;
                    premc13_pv = mc13_pv;
                    premc14_pv = mc14_pv;
                    premc15_pv = mc15_pv;
                    premc16_pv = mc16_pv;

                    mc01_pv = temp_PV.Substring(0, 4);
                    mc02_pv = temp_PV.Substring(4, 4);
                    mc03_pv = temp_PV.Substring(8, 4);
                    mc04_pv = temp_PV.Substring(12, 4);
                    mc05_pv = temp_PV.Substring(16, 4);
                    mc06_pv = temp_PV.Substring(20, 4);
                    mc07_pv = temp_PV.Substring(24, 4);
                    mc08_pv = temp_PV.Substring(28, 4);
                    mc09_pv = temp_PV.Substring(32, 4);
                    mc10_pv = temp_PV.Substring(36, 4);
                    mc11_pv = temp_PV.Substring(40, 4);
                    mc12_pv = temp_PV.Substring(44, 4);
                    mc13_pv = temp_PV.Substring(48, 4);
                    mc14_pv = temp_PV.Substring(52, 4);
                    mc15_pv = temp_PV.Substring(56, 4);
                    mc16_pv = temp_PV.Substring(60, 4);

                    //if (mc01_pv != "0" && mc01_pv != null) { _mc01_pv = Convert.ToInt32(mc01_pv); }
                    //if (mc02_pv != "0" && mc02_pv != null) { _mc02_pv = Convert.ToInt32(mc02_pv); }
                    //if (mc03_pv != "0" && mc03_pv != null) { _mc03_pv = Convert.ToInt32(mc03_pv); }
                    //if (mc04_pv != "0" && mc04_pv != null) { _mc04_pv = Convert.ToInt32(mc04_pv); }
                    //if (mc05_pv != "0" && mc05_pv != null) { _mc05_pv = Convert.ToInt32(mc05_pv); }
                    //if (mc06_pv != "0" && mc06_pv != null) { _mc06_pv = Convert.ToInt32(mc06_pv); }
                    //if (mc07_pv != "0" && mc07_pv != null) { _mc07_pv = Convert.ToInt32(mc07_pv); }
                    //if (mc08_pv != "0" && mc08_pv != null) { _mc08_pv = Convert.ToInt32(mc08_pv); }
                    //if (mc09_pv != "0" && mc09_pv != null) { _mc09_pv = Convert.ToInt32(mc09_pv); }
                    //if (mc10_pv != "0" && mc10_pv != null) { _mc10_pv = Convert.ToInt32(mc10_pv); }
                    //if (mc11_pv != "0" && mc11_pv != null) { _mc11_pv = Convert.ToInt32(mc11_pv); }
                    //if (mc12_pv != "0" && mc12_pv != null) { _mc12_pv = Convert.ToInt32(mc12_pv); }
                    //if (mc13_pv != "0" && mc13_pv != null) { _mc13_pv = Convert.ToInt32(mc13_pv); }
                    //if (mc14_pv != "0" && mc14_pv != null) { _mc14_pv = Convert.ToInt32(mc14_pv); }
                    //if (mc15_pv != "0" && mc15_pv != null) { _mc15_pv = Convert.ToInt32(mc15_pv); }
                    //if (mc16_pv != "0" && mc16_pv != null) { _mc16_pv = Convert.ToInt32(mc16_pv); }

                    _mc01_pv = (mc01_pv != "0" && mc01_pv != null) ? Convert.ToDouble(mc01_pv) : Convert.ToDouble(premc01_pv);
                    _mc02_pv = (mc02_pv != "0" && mc02_pv != null) ? Convert.ToDouble(mc02_pv) : Convert.ToDouble(premc02_pv);
                    _mc03_pv = (mc03_pv != "0" && mc03_pv != null) ? Convert.ToDouble(mc03_pv) : Convert.ToDouble(premc03_pv);
                    _mc04_pv = (mc04_pv != "0" && mc04_pv != null) ? Convert.ToDouble(mc04_pv) : Convert.ToDouble(premc04_pv);
                    _mc05_pv = (mc05_pv != "0" && mc05_pv != null) ? Convert.ToDouble(mc05_pv) : Convert.ToDouble(premc05_pv);
                    _mc06_pv = (mc06_pv != "0" && mc06_pv != null) ? Convert.ToDouble(mc06_pv) : Convert.ToDouble(premc06_pv);
                    _mc07_pv = (mc07_pv != "0" && mc07_pv != null) ? Convert.ToDouble(mc07_pv) : Convert.ToDouble(premc07_pv);
                    _mc08_pv = (mc08_pv != "0" && mc08_pv != null) ? Convert.ToDouble(mc08_pv) : Convert.ToDouble(premc08_pv);
                    _mc09_pv = (mc09_pv != "0" && mc09_pv != null) ? Convert.ToDouble(mc09_pv) : Convert.ToDouble(premc09_pv);
                    _mc10_pv = (mc10_pv != "0" && mc10_pv != null) ? Convert.ToDouble(mc10_pv) : Convert.ToDouble(premc10_pv);
                    _mc11_pv = (mc11_pv != "0" && mc11_pv != null) ? Convert.ToDouble(mc11_pv) : Convert.ToDouble(premc11_pv);
                    _mc12_pv = (mc12_pv != "0" && mc12_pv != null) ? Convert.ToDouble(mc12_pv) : Convert.ToDouble(premc12_pv);
                    _mc13_pv = (mc13_pv != "0" && mc13_pv != null) ? Convert.ToDouble(mc13_pv) : Convert.ToDouble(premc13_pv);
                    _mc14_pv = (mc14_pv != "0" && mc14_pv != null) ? Convert.ToDouble(mc14_pv) : Convert.ToDouble(premc14_pv);
                    _mc15_pv = (mc15_pv != "0" && mc15_pv != null) ? Convert.ToDouble(mc15_pv) : Convert.ToDouble(premc15_pv);
                    _mc16_pv = (mc16_pv != "0" && mc16_pv != null) ? Convert.ToDouble(mc16_pv) : Convert.ToDouble(premc16_pv);

                    //R01_PV.Text = "PV: " + _mc01_pv.ToString() + "" + (char)176 + "C";
                    //R02_PV.Text = "PV: " + _mc02_pv.ToString() + "" + (char)176 + "C";
                    //R03_PV.Text = "PV: " + _mc03_pv.ToString() + "" + (char)176 + "C";
                    //R04_PV.Text = "PV: " + _mc04_pv.ToString() + "" + (char)176 + "C";
                    //R05_PV.Text = "PV: " + _mc05_pv.ToString() + "" + (char)176 + "C";
                    //R06_PV.Text = "PV: " + _mc06_pv.ToString() + "" + (char)176 + "C";
                    //R07_PV.Text = "PV: " + _mc07_pv.ToString() + "" + (char)176 + "C";
                    //R08_PV.Text = "PV: " + _mc08_pv.ToString() + "" + (char)176 + "C";
                    //R09_PV.Text = "PV: " + _mc09_pv.ToString() + "" + (char)176 + "C";
                    //R10_PV.Text = "PV: " + _mc10_pv.ToString() + "" + (char)176 + "C";
                    //R11_PV.Text = "PV: " + _mc11_pv.ToString() + "" + (char)176 + "C";
                    //R12_PV.Text = "PV: " + _mc12_pv.ToString() + "" + (char)176 + "C";
                    //R13_PV.Text = "PV: " + _mc13_pv.ToString() + "" + (char)176 + "C";
                    //R14_PV.Text = "PV: " + _mc14_pv.ToString() + "" + (char)176 + "C";
                    //R15_PV.Text = "PV: " + _mc15_pv.ToString() + "" + (char)176 + "C";
                    //R16_PV.Text = "PV: " + _mc16_pv.ToString() + "" + (char)176 + "C";

                    /**************************************/

                    mc01_al = temp_AL.Substring(0, 4);
                    mc02_al = temp_AL.Substring(4, 4);
                    mc03_al = temp_AL.Substring(8, 4);
                    mc04_al = temp_AL.Substring(12, 4);
                    mc05_al = temp_AL.Substring(16, 4);
                    mc06_al = temp_AL.Substring(20, 4);
                    mc07_al = temp_AL.Substring(24, 4);
                    mc08_al = temp_AL.Substring(28, 4);
                    mc09_al = temp_AL.Substring(32, 4);
                    mc10_al = temp_AL.Substring(36, 4);
                    mc11_al = temp_AL.Substring(40, 4);
                    mc12_al = temp_AL.Substring(44, 4);
                    mc13_al = temp_AL.Substring(48, 4);
                    mc14_al = temp_AL.Substring(52, 4);
                    mc15_al = temp_AL.Substring(56, 4);
                    mc16_al = temp_AL.Substring(60, 4);

                    _mc01_al = (mc01_al != "0" && mc01_al != null) ? Convert.ToDouble(mc01_al) : 0;
                    _mc02_al = (mc02_al != "0" && mc02_al != null) ? Convert.ToDouble(mc02_al) : 0;
                    _mc03_al = (mc03_al != "0" && mc03_al != null) ? Convert.ToDouble(mc03_al) : 0;
                    _mc04_al = (mc04_al != "0" && mc04_al != null) ? Convert.ToDouble(mc04_al) : 0;
                    _mc05_al = (mc05_al != "0" && mc05_al != null) ? Convert.ToDouble(mc05_al) : 0;
                    _mc06_al = (mc06_al != "0" && mc06_al != null) ? Convert.ToDouble(mc06_al) : 0;
                    _mc07_al = (mc07_al != "0" && mc07_al != null) ? Convert.ToDouble(mc07_al) : 0;
                    _mc08_al = (mc08_al != "0" && mc08_al != null) ? Convert.ToDouble(mc08_al) : 0;
                    _mc09_al = (mc09_al != "0" && mc09_al != null) ? Convert.ToDouble(mc09_al) : 0;
                    _mc10_al = (mc10_al != "0" && mc10_al != null) ? Convert.ToDouble(mc10_al) : 0;
                    _mc11_al = (mc11_al != "0" && mc11_al != null) ? Convert.ToDouble(mc11_al) : 0;
                    _mc12_al = (mc12_al != "0" && mc12_al != null) ? Convert.ToDouble(mc12_al) : 0;
                    _mc13_al = (mc13_al != "0" && mc13_al != null) ? Convert.ToDouble(mc13_al) : 0;
                    _mc14_al = (mc14_al != "0" && mc14_al != null) ? Convert.ToDouble(mc14_al) : 0;
                    _mc15_al = (mc15_al != "0" && mc15_al != null) ? Convert.ToDouble(mc15_al) : 0;
                    _mc16_al = (mc16_al != "0" && mc16_al != null) ? Convert.ToDouble(mc16_al) : 0;

                    //lbl_R01_MC.BackColor = lbl_R01_SV.BackColor = lbl_R01_PV.BackColor = (_mc01_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R02_MC.BackColor = lbl_R02_SV.BackColor = lbl_R02_PV.BackColor = (_mc02_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R03_MC.BackColor = lbl_R03_SV.BackColor = lbl_R03_PV.BackColor = (_mc03_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R04_MC.BackColor = lbl_R04_SV.BackColor = lbl_R04_PV.BackColor = (_mc04_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R05_MC.BackColor = lbl_R05_SV.BackColor = lbl_R05_PV.BackColor = (_mc05_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R06_MC.BackColor = lbl_R06_SV.BackColor = lbl_R06_PV.BackColor = (_mc06_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R07_MC.BackColor = lbl_R07_SV.BackColor = lbl_R07_PV.BackColor = (_mc07_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R08_MC.BackColor = lbl_R08_SV.BackColor = lbl_R08_PV.BackColor = (_mc08_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R09_MC.BackColor = lbl_R09_SV.BackColor = lbl_R09_PV.BackColor = (_mc09_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R10_MC.BackColor = lbl_R10_SV.BackColor = lbl_R10_PV.BackColor = (_mc10_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R11_MC.BackColor = lbl_R11_SV.BackColor = lbl_R11_PV.BackColor = (_mc11_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R12_MC.BackColor = lbl_R12_SV.BackColor = lbl_R12_PV.BackColor = (_mc12_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R13_MC.BackColor = lbl_R13_SV.BackColor = lbl_R13_PV.BackColor = (_mc13_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R14_MC.BackColor = lbl_R14_SV.BackColor = lbl_R14_PV.BackColor = (_mc14_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R15_MC.BackColor = lbl_R15_SV.BackColor = lbl_R15_PV.BackColor = (_mc15_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_R16_MC.BackColor = lbl_R16_SV.BackColor = lbl_R16_PV.BackColor = (_mc16_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lnk_R01_NM.BackColor = (_mc01_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R02_NM.BackColor = (_mc02_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R03_NM.BackColor = (_mc03_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R04_NM.BackColor = (_mc04_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R05_NM.BackColor = (_mc05_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R06_NM.BackColor = (_mc06_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R07_NM.BackColor = (_mc07_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R08_NM.BackColor = (_mc08_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R09_NM.BackColor = (_mc09_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R10_NM.BackColor = (_mc10_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R11_NM.BackColor = (_mc11_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R12_NM.BackColor = (_mc12_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R13_NM.BackColor = (_mc13_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R14_NM.BackColor = (_mc14_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R15_NM.BackColor = (_mc15_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lnk_R16_NM.BackColor = (_mc16_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                }
            }
            catch (SqlException sqlEx)
            {
                //MessageBox.Show("SQL Error:\r\n" + sqlEx.ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:\r\n" + ex.ToString());
            }
            finally
            {

            }
        }

        private void lnk_R01_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 1;
            init_Load_Chart(_machine);
        }

        private void lnk_R02_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 2;
            init_Load_Chart(_machine);
        }

        private void lnk_R03_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 3;
            init_Load_Chart(_machine);
        }

        private void lnk_R04_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 4;
            init_Load_Chart(_machine);
        }

        private void lnk_R05_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 5;
            init_Load_Chart(_machine);
        }

        private void lnk_R06_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 6;
            init_Load_Chart(_machine);
        }

        private void lnk_R07_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 7;
            init_Load_Chart(_machine);
        }

        private void lnk_R08_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 8;
            init_Load_Chart(_machine);
        }

        private void lnk_R09_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 9;
            init_Load_Chart(_machine);
        }

        private void lnk_R10_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 10;
            init_Load_Chart(_machine);
        }

        private void lnk_R11_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 11;
            init_Load_Chart(_machine);
        }

        private void lnk_R12_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 12;
            init_Load_Chart(_machine);
        }

        private void lnk_R13_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 13;
            init_Load_Chart(_machine);
        }

        private void lnk_R14_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 14;
            init_Load_Chart(_machine);
        }

        private void lnk_R15_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 15;
            init_Load_Chart(_machine);
        }

        private void lnk_R16_NM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _machine = 16;
            init_Load_Chart(_machine);
        }


        #endregion

        public void fnLoadData()
        {
            Thread loadNow = new Thread(() =>
            {
                while (true)
                {
                    _dt = DateTime.Now;
                    Thread.Sleep(1000);
                }
            });
            loadNow.IsBackground = true;
            loadNow.Start();

            //Thread loadData = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        init_Load_Data();
            //        Thread.Sleep(90000);
            //    }
            //});
            //loadData.IsBackground = true;
            //loadData.Start();

            Thread loadLastData = new Thread(() =>
            {
                while (true)
                {
                    init_Load_Data();
                    init_Load_Last_Data();
                    Thread.Sleep(90000);
                }
            });
            loadLastData.IsBackground = true;
            loadLastData.Start();

            Thread loadAlarm = new Thread(() =>
            {
                while (true)
                {

                    Thread.Sleep(20000);
                }
            });
            loadAlarm.IsBackground = true;
            loadAlarm.Start();

        }


    }
}
