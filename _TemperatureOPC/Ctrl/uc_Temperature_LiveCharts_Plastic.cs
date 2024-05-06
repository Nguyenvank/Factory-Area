using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Threading;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_LiveCharts_Plastic : UserControl
    {
        private static uc_Temperature_LiveCharts_Plastic _instance;
        public static uc_Temperature_LiveCharts_Plastic Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_LiveCharts_Plastic();
                return _instance;
            }
        }

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

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
        //public ChartValues<MeasureModel> ChartValues13 { get; set; }
        //public ChartValues<MeasureModel> ChartValues14 { get; set; }
        //public ChartValues<MeasureModel> ChartValues15 { get; set; }
        //public ChartValues<MeasureModel> ChartValues16 { get; set; }
        public System.Windows.Forms.Timer Timer { get; set; }
        //public Random Rand { get; set; }

        DateTime _dt;

        string mc01_sv = "", mc02_sv = "", mc03_sv = "", mc04_sv = "", mc05_sv = "", mc06_sv = "", mc07_sv = "", mc08_sv = "", mc09_sv = "", mc10_sv = "", mc11_sv = "", mc12_sv = "", mc13_sv = "", mc14_sv = "", mc15_sv = "", mc16_sv = "";
        string mc01_pv = "", mc02_pv = "", mc03_pv = "", mc04_pv = "", mc05_pv = "", mc06_pv = "", mc07_pv = "", mc08_pv = "", mc09_pv = "", mc10_pv = "", mc11_pv = "", mc12_pv = "", mc13_pv = "", mc14_pv = "", mc15_pv = "", mc16_pv = "";
        string mc01_al = "", mc02_al = "", mc03_al = "", mc04_al = "", mc05_al = "", mc06_al = "", mc07_al = "", mc08_al = "", mc09_al = "", mc10_al = "", mc11_al = "", mc12_al = "", mc13_al = "", mc14_al = "", mc15_al = "", mc16_al = "";
        string premc01_pv = "", premc02_pv = "", premc03_pv = "", premc04_pv = "", premc05_pv = "", premc06_pv = "", premc07_pv = "", premc08_pv = "", premc09_pv = "", premc10_pv = "", premc11_pv = "", premc12_pv = "", premc13_pv = "", premc14_pv = "", premc15_pv = "", premc16_pv = "";

        double _mc01_sv = 0, _mc02_sv = 0, _mc03_sv = 0, _mc04_sv = 0, _mc05_sv = 0, _mc06_sv = 0, _mc07_sv = 0, _mc08_sv = 0, _mc09_sv = 0, _mc10_sv = 0, _mc11_sv = 0, _mc12_sv = 0, _mc13_sv = 0, _mc14_sv = 0, _mc15_sv = 0, _mc16_sv = 0;
        double _mc01_pv = 0, _mc02_pv = 0, _mc03_pv = 0, _mc04_pv = 0, _mc05_pv = 0, _mc06_pv = 0, _mc07_pv = 0, _mc08_pv = 0, _mc09_pv = 0, _mc10_pv = 0, _mc11_pv = 0, _mc12_pv = 0, _mc13_pv = 0, _mc14_pv = 0, _mc15_pv = 0, _mc16_pv = 0;
        double _mc01_al = 0, _mc02_al = 0, _mc03_al = 0, _mc04_al = 0, _mc05_al = 0, _mc06_al = 0, _mc07_al = 0, _mc08_al = 0, _mc09_al = 0, _mc10_al = 0, _mc11_al = 0, _mc12_al = 0, _mc13_al = 0, _mc14_al = 0, _mc15_al = 0, _mc16_al = 0;

        int _line = 2;

        public uc_Temperature_LiveCharts_Plastic()
        {
            InitializeComponent();

            init_Load_Data();

            init_Chart(cartesianChart01, ChartValues01, _mc01_sv);
            init_Chart(cartesianChart02, ChartValues02, _mc02_sv);
            init_Chart(cartesianChart03, ChartValues03, _mc03_sv);
            init_Chart(cartesianChart04, ChartValues04, _mc04_sv);
            init_Chart(cartesianChart05, ChartValues05, _mc05_sv);
            init_Chart(cartesianChart06, ChartValues06, _mc06_sv);
            init_Chart(cartesianChart07, ChartValues07, _mc07_sv);
            init_Chart(cartesianChart08, ChartValues08, _mc08_sv);
            init_Chart(cartesianChart09, ChartValues09, _mc09_sv);
            init_Chart(cartesianChart10, ChartValues10, _mc10_sv);
            init_Chart(cartesianChart11, ChartValues11, _mc11_sv);
            init_Chart(cartesianChart12, ChartValues12, _mc12_sv);
            //init_Chart(cartesianChart13, ChartValues13, _mc13_sv);
            //init_Chart(cartesianChart14, ChartValues14, _mc14_sv);
            //init_Chart(cartesianChart15, ChartValues15, _mc15_sv);
            //init_Chart(cartesianChart16, ChartValues16, _mc16_sv);
        }

        private void uc_Temperature_LiveCharts_Plastic_Load(object sender, EventArgs e)
        {
            fnLoadData();

            lbl_P01_SV.Text = "SV: " + _mc01_sv + "" + (char)176 + "C";
            lbl_P01_PV.Text = "PV: " + _mc01_pv + "" + (char)176 + "C";
            lbl_P02_SV.Text = "SV: " + _mc02_sv + "" + (char)176 + "C";
            lbl_P02_PV.Text = "PV: " + _mc02_pv + "" + (char)176 + "C";
            lbl_P03_SV.Text = "SV: " + _mc03_sv + "" + (char)176 + "C";
            lbl_P03_PV.Text = "PV: " + _mc03_pv + "" + (char)176 + "C";
            lbl_P04_SV.Text = "SV: " + _mc04_sv + "" + (char)176 + "C";
            lbl_P04_PV.Text = "PV: " + _mc04_pv + "" + (char)176 + "C";
            lbl_P05_SV.Text = "SV: " + _mc05_sv + "" + (char)176 + "C";
            lbl_P05_PV.Text = "PV: " + _mc05_pv + "" + (char)176 + "C";
            lbl_P06_SV.Text = "SV: " + _mc06_sv + "" + (char)176 + "C";
            lbl_P06_PV.Text = "PV: " + _mc06_pv + "" + (char)176 + "C";
            lbl_P07_SV.Text = "SV: " + _mc07_sv + "" + (char)176 + "C";
            lbl_P07_PV.Text = "PV: " + _mc07_pv + "" + (char)176 + "C";
            lbl_P08_SV.Text = "SV: " + _mc08_sv + "" + (char)176 + "C";
            lbl_P08_PV.Text = "PV: " + _mc08_pv + "" + (char)176 + "C";
            lbl_P09_SV.Text = "SV: " + _mc09_sv + "" + (char)176 + "C";
            lbl_P09_PV.Text = "PV: " + _mc09_pv + "" + (char)176 + "C";
            lbl_P10_SV.Text = "SV: " + _mc10_sv + "" + (char)176 + "C";
            lbl_P10_PV.Text = "PV: " + _mc10_pv + "" + (char)176 + "C";
            lbl_P11_SV.Text = "SV: " + _mc11_sv + "" + (char)176 + "C";
            lbl_P11_PV.Text = "PV: " + _mc11_pv + "" + (char)176 + "C";
            lbl_P12_SV.Text = "SV: " + _mc12_sv + "" + (char)176 + "C";
            lbl_P12_PV.Text = "PV: " + _mc12_pv + "" + (char)176 + "C";
            //lbl_P13_SV.Text = "SV: " + _mc13_sv + "" + (char)176 + "C";
            //lbl_P13_PV.Text = "PV: " + _mc13_pv + "" + (char)176 + "C";
            //lbl_P14_SV.Text = "SV: " + _mc14_sv + "" + (char)176 + "C";
            //lbl_P14_PV.Text = "PV: " + _mc14_pv + "" + (char)176 + "C";
            //lbl_P15_SV.Text = "SV: " + _mc15_sv + "" + (char)176 + "C";
            //lbl_P15_PV.Text = "PV: " + _mc15_pv + "" + (char)176 + "C";
            //lbl_P16_SV.Text = "SV: " + _mc16_sv + "" + (char)176 + "C";
            //lbl_P16_PV.Text = "PV: " + _mc16_pv + "" + (char)176 + "C";

            string minmax = "SV " + (char)177 + " 5" + (char)176 + "C";
            lbl_P01_MM.Text = lbl_P02_MM.Text = lbl_P03_MM.Text = lbl_P04_MM.Text = minmax;
            lbl_P05_MM.Text = lbl_P06_MM.Text = lbl_P07_MM.Text = lbl_P08_MM.Text = minmax;
            lbl_P09_MM.Text = lbl_P10_MM.Text = lbl_P11_MM.Text = lbl_P12_MM.Text = minmax;
            //lbl_P13_MM.Text = lbl_P14_MM.Text = lbl_P15_MM.Text = lbl_P16_MM.Text = minmax;
            //init_Chart_Range(cartesianChart01, _mc01_sv);
        }

        public void init_Chart_Range(LiveCharts.WinForms.CartesianChart chart, double sv)
        {
            switch (chart.Name)
            {
                case "cartesianChart01":
                    lbl_P01_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart02":
                    lbl_P02_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart03":
                    lbl_P03_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart04":
                    lbl_P04_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart05":
                    lbl_P05_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart06":
                    lbl_P06_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart07":
                    lbl_P07_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart08":
                    lbl_P08_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart09":
                    lbl_P09_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart10":
                    lbl_P10_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
                    break;
                case "cartesianChart11":
                    //lbl_P11_MM.Text=(sv<180.0)?"SV " + (char)177 + " 5" + (char)176 + "C":"SV +2/-1"+(char)176+"C";
                    lbl_P11_MM.Text = "SV " + (char)177 + " 5" + (char)176 + "C";
                    break;
                case "cartesianChart12":
                    //lbl_P12_MM.Text=(sv<180.0)?"SV " + (char)177 + " 5" + (char)176 + "C":"SV +2/-1"+(char)176+"C";
                    lbl_P12_MM.Text = "SV " + (char)177 + " 5" + (char)176 + "C";
                    break;
                //case "cartesianChart13":
                //    //lbl_P13_MM.Text=(sv<180.0)?"SV " + (char)177 + " 5" + (char)176 + "C":"SV +2/-1"+(char)176+"C";
                //    lbl_P13_MM.Text = "SV " + (char)177 + " 5" + (char)176 + "C";
                //    break;
                //case "cartesianChart14":
                //    //lbl_P14_MM.Text=(sv<180.0)?"SV " + (char)177 + " 5" + (char)176 + "C":"SV +2/-1"+(char)176+"C";
                //    lbl_P14_MM.Text = "SV " + (char)177 + " 5" + (char)176 + "C";
                //    break;
                //case "cartesianChart15":
                //    //lbl_P15_MM.Text=(sv<180.0)?"SV " + (char)177 + " 5" + (char)176 + "C":"SV +2/-1"+(char)176+"C";
                //    lbl_P15_MM.Text = "SV " + (char)177 + " 5" + (char)176 + "C";
                //    break;
                //case "cartesianChart16":
                //    //lbl_P16_MM.Text=(sv<180.0)?"SV " + (char)177 + " 5" + (char)176 + "C":"SV +2/-1"+(char)176+"C";
                //    lbl_P16_MM.Text = "SV " + (char)177 + " 5" + (char)176 + "C";
                //    break;
            }
        }

        public void init_Chart(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue, double sv)
        {
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
            chartvalue = new ChartValues<MeasureModel>();
            chart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = chartvalue,
                    //PointGeometrySize = 18,
                    //StrokeThickness = 4
                    PointGeometrySize = 2,
                    StrokeThickness = 1
                }
            };
            chart.AxisX.Add(new Axis
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
            chart.AxisY.Add(new Axis
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
                        Value =  (Convert.ToInt32(cls.Right(chart.Name,2))>10)? (sv + 5.0) : (sv<180.0)?(sv + 5.0):(sv+2.0),
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
                        Value = (Convert.ToInt32(cls.Right(chart.Name,2))>10)? (sv - 5.1) : (sv<180.0)?(sv-5.1):(sv-1.1),
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

            chart.DisableAnimations = true;
            chart.Hoverable = false;
            chart.DataTooltip = null;

            SetAxisLimits(System.DateTime.Now, chart, sv);

            //Initialize data at beginning open program
            //init_Data_InDay(chart, chartvalue);
            init_Data_MachineDay(chart, chartvalue);

            //The next code simulates data changes every 500 ms
            Timer = new System.Windows.Forms.Timer
            {
                //Interval = 500
                Interval = 5000
            };
            Timer.Tick += (sender, eventArgs) => TimerOnTick(sender, eventArgs, chart, chartvalue, sv);
            //Rand = new Random();
            Timer.Start();
        }

        private void SetAxisLimits(System.DateTime now, LiveCharts.WinForms.CartesianChart chart, double sv)
        {
            //cartesianChart1.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 100ms ahead
            //cartesianChart1.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds

            chart.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(10).Ticks; // lets force the axis to be 100ms ahead
            chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromHours(12).Ticks; //we only care about the last 8 seconds

            //chart.AxisY[0].MaxValue = 182; // lets force the axis to be 100ms ahead
            //chart.AxisY[0].MinValue = 179; //we only care about the last 8 seconds

            //double maxSV = (sv<180.0)?(sv + 5.0):(sv+2.0);
            //double minSV = (sv<180.0)?(sv - 5.1):(sv-1.1);

            double maxSV = (Convert.ToInt32(cls.Right(chart.Name, 2)) > 10) ? (sv + 5.0) : (sv < 180.0) ? (sv + 5.0) : (sv + 2.0);
            double minSV = (Convert.ToInt32(cls.Right(chart.Name, 2)) > 10) ? (sv - 5.0) : (sv < 180.0) ? (sv - 5.1) : (sv - 1.0);

            chart.AxisY[0].MaxValue = maxSV; // lets force the axis to be 100ms ahead
            chart.AxisY[0].MinValue = minSV; //we only care about the last 8 seconds

            init_Chart_Range(chart, sv);

        }

        private void TimerOnTick(object sender, EventArgs eventArgs, LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue, double sv)
        {
            //init_Load_Data();
            var now = System.DateTime.Now;

            //chartvalue.Add(new MeasureModel
            //{
            //    DateTime = now,
            //    Value = _mc01_pv
            //});

            switch (chart.Name)
            {
                case "cartesianChart01":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc01_pv
                    });
                    lbl_P01_SV.Text = "SV: " + _mc01_sv + "" + (char)176 + "C";
                    lbl_P01_PV.Text = "PV: " + _mc01_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart02":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc02_pv
                    });
                    lbl_P02_SV.Text = "SV: " + _mc02_sv + "" + (char)176 + "C";
                    lbl_P02_PV.Text = "PV: " + _mc02_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart03":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc03_pv
                    });
                    lbl_P03_SV.Text = "SV: " + _mc03_sv + "" + (char)176 + "C";
                    lbl_P03_PV.Text = "PV: " + _mc03_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart04":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc04_pv
                    });
                    lbl_P04_SV.Text = "SV: " + _mc04_sv + "" + (char)176 + "C";
                    lbl_P04_PV.Text = "PV: " + _mc04_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart05":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc05_pv
                    });
                    lbl_P05_SV.Text = "SV: " + _mc05_sv + "" + (char)176 + "C";
                    lbl_P05_PV.Text = "PV: " + _mc05_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart06":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc06_pv
                    });
                    lbl_P06_SV.Text = "SV: " + _mc06_sv + "" + (char)176 + "C";
                    lbl_P06_PV.Text = "PV: " + _mc06_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart07":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc07_pv
                    });
                    lbl_P07_SV.Text = "SV: " + _mc07_sv + "" + (char)176 + "C";
                    lbl_P07_PV.Text = "PV: " + _mc07_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart08":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc08_pv
                    });
                    lbl_P08_SV.Text = "SV: " + _mc08_sv + "" + (char)176 + "C";
                    lbl_P08_PV.Text = "PV: " + _mc08_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart09":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc09_pv
                    });
                    lbl_P09_SV.Text = "SV: " + _mc09_sv + "" + (char)176 + "C";
                    lbl_P09_PV.Text = "PV: " + _mc09_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart10":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc10_pv
                    });
                    lbl_P10_SV.Text = "SV: " + _mc10_sv + "" + (char)176 + "C";
                    lbl_P10_PV.Text = "PV: " + _mc10_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart11":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc11_pv
                    });
                    lbl_P11_SV.Text = "SV: " + _mc11_sv + "" + (char)176 + "C";
                    lbl_P11_PV.Text = "PV: " + _mc11_pv + "" + (char)176 + "C";
                    break;
                case "cartesianChart12":
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc12_pv
                    });
                    lbl_P12_SV.Text = "SV: " + _mc12_sv + "" + (char)176 + "C";
                    lbl_P12_PV.Text = "PV: " + _mc12_pv + "" + (char)176 + "C";
                    break;
                //case "cartesianChart13":
                //    chartvalue.Add(new MeasureModel
                //    {
                //        DateTime = now,
                //        Value = _mc13_pv
                //    });
                //    lbl_P13_SV.Text = "SV: " + _mc13_sv + "" + (char)176 + "C";
                //    lbl_P13_PV.Text = "PV: " + _mc13_pv + "" + (char)176 + "C";
                //    break;
                //case "cartesianChart14":
                //    chartvalue.Add(new MeasureModel
                //    {
                //        DateTime = now,
                //        Value = _mc14_pv
                //    });
                //    lbl_P14_SV.Text = "SV: " + _mc14_sv + "" + (char)176 + "C";
                //    lbl_P14_PV.Text = "PV: " + _mc14_pv + "" + (char)176 + "C";
                //    break;
                //case "cartesianChart15":
                //    chartvalue.Add(new MeasureModel
                //    {
                //        DateTime = now,
                //        Value = _mc15_pv
                //    });
                //    lbl_P15_SV.Text = "SV: " + _mc15_sv + "" + (char)176 + "C";
                //    lbl_P15_PV.Text = "PV: " + _mc15_pv + "" + (char)176 + "C";
                //    break;
                //case "cartesianChart16":
                //    chartvalue.Add(new MeasureModel
                //    {
                //        DateTime = now,
                //        Value = _mc16_pv
                //    });
                //    lbl_P16_SV.Text = "SV: " + _mc16_sv + "" + (char)176 + "C";
                //    lbl_P16_PV.Text = "PV: " + _mc16_pv + "" + (char)176 + "C";
                //    break;
            }
            //chartvalue.Add(new MeasureModel
            //{
            //    DateTime = now,
            //    Value = Rand.Next(179, 182)
            //});

            SetAxisLimits(now, chart, sv);

            //lets only use the last 30 values
            //if (chartvalue.Count > 182) chartvalue.RemoveAt(0);
        }

        public void init_Data_InDay(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue)
        {
            //try
            //{

            //}
            //catch (SqlException sqlEx)
            //{
            //    MessageBox.Show(sqlEx.ToString());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{

            //}

            string mcNum = cls.Right(chart.Name, 2);
            int col_SV_Num = 0, col_PV_Num = 0, col_AL_Num, col_DT_Num = 0;
            switch (Convert.ToInt32(mcNum))
            {
                case 1:
                    col_SV_Num = 0;
                    col_PV_Num = 1;
                    col_AL_Num = 2;
                    break;
                case 2:
                    col_SV_Num = 3;
                    col_PV_Num = 4;
                    col_AL_Num = 5;
                    break;
                case 3:
                    col_SV_Num = 6;
                    col_PV_Num = 7;
                    col_AL_Num = 8;
                    break;
                case 4:
                    col_SV_Num = 9;
                    col_PV_Num = 10;
                    col_AL_Num = 11;
                    break;
                case 5:
                    col_SV_Num = 12;
                    col_PV_Num = 13;
                    col_AL_Num = 14;
                    break;
                case 6:
                    col_SV_Num = 15;
                    col_PV_Num = 16;
                    col_AL_Num = 17;
                    break;
                case 7:
                    col_SV_Num = 18;
                    col_PV_Num = 19;
                    col_AL_Num = 20;
                    break;
                case 8:
                    col_SV_Num = 21;
                    col_PV_Num = 22;
                    col_AL_Num = 23;
                    break;
                case 9:
                    col_SV_Num = 24;
                    col_PV_Num = 25;
                    col_AL_Num = 26;
                    break;
                case 10:
                    col_SV_Num = 27;
                    col_PV_Num = 28;
                    col_AL_Num = 29;
                    break;
                case 11:
                    col_SV_Num = 30;
                    col_PV_Num = 31;
                    col_AL_Num = 32;
                    break;
                case 12:
                    col_SV_Num = 33;
                    col_PV_Num = 34;
                    col_AL_Num = 35;
                    break;
                case 13:
                    col_SV_Num = 36;
                    col_PV_Num = 37;
                    col_AL_Num = 38;
                    break;
                case 14:
                    col_SV_Num = 39;
                    col_PV_Num = 40;
                    col_AL_Num = 41;
                    break;
                case 15:
                    col_SV_Num = 42;
                    col_PV_Num = 43;
                    col_AL_Num = 44;
                    break;
                case 16:
                    col_SV_Num = 45;
                    col_PV_Num = 46;
                    col_AL_Num = 47;
                    break;
            }
            col_DT_Num = 48;

            string col_SV_Data = "", col_PV_Data = "", col_DT_Data = "";
            double _col_SV_Data = 0, _col_PV_Data = 0;

            string sql = "V2o1_ERP_Temperature_Init_Data_SelItem_V1o0_Addnew";

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                int rows = ds.Tables[0].Rows.Count;
                DateTime _col_DT_Data;

                for (int i = 0; i < rows; i++)
                {
                    col_SV_Data = ds.Tables[0].Rows[i][col_SV_Num].ToString();
                    col_PV_Data = ds.Tables[0].Rows[i][col_PV_Num].ToString();
                    col_DT_Data = ds.Tables[0].Rows[i][col_DT_Num].ToString();

                    _col_SV_Data = (col_SV_Data != "" && col_SV_Data != null) ? Convert.ToDouble(col_SV_Data) : 0;
                    _col_PV_Data = (col_PV_Data != "" && col_PV_Data != null) ? Convert.ToDouble(col_PV_Data) : 0;
                    _col_DT_Data = (col_DT_Data != "" && col_DT_Data != null) ? Convert.ToDateTime(col_DT_Data) : DateTime.Now;

                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = _col_DT_Data,
                        Value = _col_PV_Data
                    });
                    SetAxisLimits(_col_DT_Data, chart, _col_SV_Data);
                }
            }
        }

        public void init_Data_MachineDay(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue)
        {
            int mc = Convert.ToInt32(cls.Right(chart.Name, 2));
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
            sParams[0].Value = mc;

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

                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = _col_dt,
                        Value = _col_pv
                    });
                    SetAxisLimits(_col_dt, chart, _col_sv);
                }
            }
        }

        public void init_Load_Data()
        {
            try
            {
                string sql = "V2o1_ERP_Temperature_Garthering_SelItem_V1o2_Addnew";

                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.TinyInt;
                sParams[0].ParameterName = "@line";
                sParams[0].Value = _line;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql,sParams);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string temp_SV = ds.Tables[0].Rows[0][0].ToString();
                    string temp_PV = ds.Tables[0].Rows[0][1].ToString();
                    string temp_AL = ds.Tables[0].Rows[0][2].ToString();
                    string temp_DT = ds.Tables[0].Rows[0][3].ToString();
                    //string temp_RS = ds.Tables[0].Rows[0][4].ToString();

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

                    //lbl_P01_MC.BackColor = lbl_P01_SV.BackColor = lbl_P01_PV.BackColor = (_mc01_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P02_MC.BackColor = lbl_P02_SV.BackColor = lbl_P02_PV.BackColor = (_mc02_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P03_MC.BackColor = lbl_P03_SV.BackColor = lbl_P03_PV.BackColor = (_mc03_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P04_MC.BackColor = lbl_P04_SV.BackColor = lbl_P04_PV.BackColor = (_mc04_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P05_MC.BackColor = lbl_P05_SV.BackColor = lbl_P05_PV.BackColor = (_mc05_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P06_MC.BackColor = lbl_P06_SV.BackColor = lbl_P06_PV.BackColor = (_mc06_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P07_MC.BackColor = lbl_P07_SV.BackColor = lbl_P07_PV.BackColor = (_mc07_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P08_MC.BackColor = lbl_P08_SV.BackColor = lbl_P08_PV.BackColor = (_mc08_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P09_MC.BackColor = lbl_P09_SV.BackColor = lbl_P09_PV.BackColor = (_mc09_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P10_MC.BackColor = lbl_P10_SV.BackColor = lbl_P10_PV.BackColor = (_mc10_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P11_MC.BackColor = lbl_P11_SV.BackColor = lbl_P11_PV.BackColor = (_mc11_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P12_MC.BackColor = lbl_P12_SV.BackColor = lbl_P12_PV.BackColor = (_mc12_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P13_MC.BackColor = lbl_P13_SV.BackColor = lbl_P13_PV.BackColor = (_mc13_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P14_MC.BackColor = lbl_P14_SV.BackColor = lbl_P14_PV.BackColor = (_mc14_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P15_MC.BackColor = lbl_P15_SV.BackColor = lbl_P15_PV.BackColor = (_mc15_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P16_MC.BackColor = lbl_P16_SV.BackColor = lbl_P16_PV.BackColor = (_mc16_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lbl_P01_MC.BackColor = (_mc01_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P02_MC.BackColor = (_mc02_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P03_MC.BackColor = (_mc03_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P04_MC.BackColor = (_mc04_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P05_MC.BackColor = (_mc05_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P06_MC.BackColor = (_mc06_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P07_MC.BackColor = (_mc07_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P08_MC.BackColor = (_mc08_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P09_MC.BackColor = (_mc09_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P10_MC.BackColor = (_mc10_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P11_MC.BackColor = (_mc11_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_P12_MC.BackColor = (_mc12_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P13_MC.BackColor = (_mc13_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P14_MC.BackColor = (_mc14_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P15_MC.BackColor = (_mc15_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    //lbl_P16_MC.BackColor = (_mc16_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    //R01_AL.Text = _mc01_al.ToString();
                    //R02_AL.Text = _mc02_al.ToString();
                    //R03_AL.Text = _mc03_al.ToString();
                    //R04_AL.Text = _mc04_al.ToString();
                    //R05_AL.Text = _mc05_al.ToString();
                    //R06_AL.Text = _mc06_al.ToString();
                    //R07_AL.Text = _mc07_al.ToString();
                    //R08_AL.Text = _mc08_al.ToString();
                    //R09_AL.Text = _mc09_al.ToString();
                    //R10_AL.Text = _mc10_al.ToString();
                    //R11_AL.Text = _mc11_al.ToString();
                    //R12_AL.Text = _mc12_al.ToString();
                    //R13_AL.Text = _mc13_al.ToString();
                    //R14_AL.Text = _mc14_al.ToString();
                    //R15_AL.Text = _mc15_al.ToString();
                    //R16_AL.Text = _mc16_al.ToString();

                    //R01_AL.BackColor = R01_AL.ForeColor = (_mc01_sv == 0) ? Color.Gray : (_mc01_al == 1) ? Color.Red : Color.Green;
                    //R02_AL.BackColor = R02_AL.ForeColor = (_mc02_sv == 0) ? Color.Gray : (_mc02_al == 1) ? Color.Red : Color.Green;
                    //R03_AL.BackColor = R03_AL.ForeColor = (_mc03_sv == 0) ? Color.Gray : (_mc03_al == 1) ? Color.Red : Color.Green;
                    //R04_AL.BackColor = R04_AL.ForeColor = (_mc04_sv == 0) ? Color.Gray : (_mc04_al == 1) ? Color.Red : Color.Green;
                    //R05_AL.BackColor = R05_AL.ForeColor = (_mc05_sv == 0) ? Color.Gray : (_mc05_al == 1) ? Color.Red : Color.Green;
                    //R06_AL.BackColor = R06_AL.ForeColor = (_mc06_sv == 0) ? Color.Gray : (_mc06_al == 1) ? Color.Red : Color.Green;
                    //R07_AL.BackColor = R07_AL.ForeColor = (_mc07_sv == 0) ? Color.Gray : (_mc07_al == 1) ? Color.Red : Color.Green;
                    //R08_AL.BackColor = R08_AL.ForeColor = (_mc08_sv == 0) ? Color.Gray : (_mc08_al == 1) ? Color.Red : Color.Green;
                    //R09_AL.BackColor = R09_AL.ForeColor = (_mc09_sv == 0) ? Color.Gray : (_mc09_al == 1) ? Color.Red : Color.Green;
                    //R10_AL.BackColor = R10_AL.ForeColor = (_mc10_sv == 0) ? Color.Gray : (_mc10_al == 1) ? Color.Red : Color.Green;
                    //R11_AL.BackColor = R11_AL.ForeColor = (_mc11_sv == 0) ? Color.Gray : (_mc11_al == 1) ? Color.Red : Color.Green;
                    //R12_AL.BackColor = R12_AL.ForeColor = (_mc12_sv == 0) ? Color.Gray : (_mc12_al == 1) ? Color.Red : Color.Green;
                    //R13_AL.BackColor = R13_AL.ForeColor = (_mc13_sv == 0) ? Color.Gray : (_mc13_al == 1) ? Color.Red : Color.Green;
                    //R14_AL.BackColor = R14_AL.ForeColor = (_mc14_sv == 0) ? Color.Gray : (_mc14_al == 1) ? Color.Red : Color.Green;
                    //R15_AL.BackColor = R15_AL.ForeColor = (_mc15_sv == 0) ? Color.Gray : (_mc15_al == 1) ? Color.Red : Color.Green;
                    //R16_AL.BackColor = R16_AL.ForeColor = (_mc16_sv == 0) ? Color.Gray : (_mc16_al == 1) ? Color.Red : Color.Green;

                    //tlp_R01.BackColor = (_mc01_sv == 0) ? Color.Gray : (_mc01_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R02.BackColor = (_mc02_sv == 0) ? Color.Gray : (_mc02_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R03.BackColor = (_mc03_sv == 0) ? Color.Gray : (_mc03_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R04.BackColor = (_mc04_sv == 0) ? Color.Gray : (_mc04_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R05.BackColor = (_mc05_sv == 0) ? Color.Gray : (_mc05_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R06.BackColor = (_mc06_sv == 0) ? Color.Gray : (_mc06_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R07.BackColor = (_mc07_sv == 0) ? Color.Gray : (_mc07_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R08.BackColor = (_mc08_sv == 0) ? Color.Gray : (_mc08_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R09.BackColor = (_mc09_sv == 0) ? Color.Gray : (_mc09_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R10.BackColor = (_mc10_sv == 0) ? Color.Gray : (_mc10_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R11.BackColor = (_mc11_sv == 0) ? Color.Gray : (_mc11_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R12.BackColor = (_mc12_sv == 0) ? Color.Gray : (_mc12_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R13.BackColor = (_mc13_sv == 0) ? Color.Gray : (_mc13_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R14.BackColor = (_mc14_sv == 0) ? Color.Gray : (_mc14_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R15.BackColor = (_mc15_sv == 0) ? Color.Gray : (_mc15_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //tlp_R16.BackColor = (_mc16_sv == 0) ? Color.Gray : (_mc16_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
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

        public void init_Load_Update()
        {
            string sqlSel = "V2o1_ERP_Temperature_Garthering_Select_Update_UpdItem_V1o2_Addnew";

            SqlParameter[] sParamsSel = new SqlParameter[1]; // Parameter count

            sParamsSel[0] = new SqlParameter();
            sParamsSel[0].SqlDbType = SqlDbType.TinyInt;
            sParamsSel[0].ParameterName = "@line";
            sParamsSel[0].Value = _line;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sqlSel, sParamsSel);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string temp_RS = ds.Tables[0].Rows[0][0].ToString();
                if (temp_RS == "1")
                {
                    string sqlUpd = "V2o1_ERP_Temperature_Garthering_Reset_Update_UpdItem_V1o2_Addnew";

                    SqlParameter[] sParamsUpd = new SqlParameter[1]; // Parameter count

                    sParamsUpd[0] = new SqlParameter();
                    sParamsUpd[0].SqlDbType = SqlDbType.TinyInt;
                    sParamsUpd[0].ParameterName = "@line";
                    sParamsUpd[0].Value = _line;

                    cls.fnUpdDel(sqlUpd, sParamsUpd);

                    System.Windows.Forms.Application.Restart();
                    //System.Windows.Forms.Application.Exit();
                }
            }
        }

        public void init_Load_Alarm()
        {

        }

        public void fnLoadData()
        {
            Thread loadData = new Thread(() =>
            {
                while (true)
                {
                    init_Load_Data();
                    Thread.Sleep(150000);
                }
            });
            loadData.IsBackground = true;
            loadData.Start();

            Thread resetUpdate = new Thread(() =>
            {
                while (true)
                {
                    init_Load_Update();
                    Thread.Sleep(5000);
                }
            });
            resetUpdate.IsBackground = true;
            resetUpdate.Start();


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
