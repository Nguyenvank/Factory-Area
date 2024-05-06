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
//using Color = System.Drawing.Color;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_LiveCharts_Assembly : UserControl
    {
        private static uc_Temperature_LiveCharts_Assembly _instance;
        public static uc_Temperature_LiveCharts_Assembly Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_LiveCharts_Assembly();
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

        public System.Windows.Forms.Timer Timer { get; set; }

        DateTime _dt;

        string mc01u_sv, mc01l_sv, mc02u_sv, mc02l_sv, mc03u_sv, mc03l_sv, mc04u_sv, mc04l_sv, mc05_sv, mc06_sv;
        string mc01u_pv, mc01l_pv, mc02u_pv, mc02l_pv, mc03u_pv, mc03l_pv, mc04u_pv, mc04l_pv, mc05_pv, mc06_pv;
        string mc01u_al, mc01l_al, mc02u_al, mc02l_al, mc03u_al, mc03l_al, mc04u_al, mc04l_al, mc05_al, mc06_al;

        double _mc01u_sv = 0, _mc01l_sv = 0, _mc02u_sv = 0, _mc02l_sv = 0, _mc03u_sv = 0, _mc03l_sv = 0, _mc04u_sv = 0, _mc04l_sv = 0, _mc05_sv = 0, _mc06_sv = 0;
        double _mc01u_pv = 0, _mc01l_pv = 0, _mc02u_pv = 0, _mc02l_pv = 0, _mc03u_pv = 0, _mc03l_pv = 0, _mc04u_pv = 0, _mc04l_pv = 0, _mc05_pv = 0, _mc06_pv = 0;
        double _mc01u_al = 0, _mc01l_al = 0, _mc02u_al = 0, _mc02l_al = 0, _mc03u_al = 0, _mc03l_al = 0, _mc04u_al = 0, _mc04l_al = 0, _mc05_al = 0, _mc06_al = 0;
        int _line = 1;


        public uc_Temperature_LiveCharts_Assembly()
        {
            InitializeComponent();

            fnLoadData();

            //cls.SetDoubleBuffer(cartesianChart01, true);
            //cls.SetDoubleBuffer(cartesianChart02, true);
            //cls.SetDoubleBuffer(cartesianChart03, true);
            //cls.SetDoubleBuffer(cartesianChart04, true);
            //cls.SetDoubleBuffer(cartesianChart05, true);
            //cls.SetDoubleBuffer(cartesianChart06, true);
            //cls.SetDoubleBuffer(cartesianChart07, true);
            //cls.SetDoubleBuffer(cartesianChart08, true);
            //cls.SetDoubleBuffer(cartesianChart09, true);
            //cls.SetDoubleBuffer(cartesianChart10, true);
            //cls.SetDoubleBuffer(cartesianChart11, true);
            //cls.SetDoubleBuffer(cartesianChart12, true);
            //cls.SetDoubleBuffer(cartesianChart13, true);
            //cls.SetDoubleBuffer(cartesianChart14, true);
            //cls.SetDoubleBuffer(cartesianChart15, true);
            //cls.SetDoubleBuffer(cartesianChart16, true);

            //init_Data_InDay();
            init_Load_Data();

            init_Chart(cartesianChart01, ChartValues01, _mc01u_sv);
            init_Chart(cartesianChart02, ChartValues02, _mc01l_sv);
            init_Chart(cartesianChart03, ChartValues03, _mc02u_sv);
            init_Chart(cartesianChart04, ChartValues04, _mc02l_sv);
            init_Chart(cartesianChart05, ChartValues05, _mc03u_sv);
            init_Chart(cartesianChart06, ChartValues06, _mc03l_sv);
            init_Chart(cartesianChart07, ChartValues07, _mc04u_sv);
            init_Chart(cartesianChart08, ChartValues08, _mc04l_sv);
            init_Chart(cartesianChart09, ChartValues09, _mc05_sv);
            init_Chart(cartesianChart10, ChartValues10, _mc06_sv);
        }

        private void uc_Temperature_LiveCharts_Assembly_Load(object sender, EventArgs e)
        {
            //fnLoadData();
            //init_Load_Data();

            lbl_R01_SV.Text = "SV: " + _mc01u_sv + "" + (char)176 + "C";
            lbl_R01_PV.Text = "PV: " + _mc01u_pv + "" + (char)176 + "C";
            lbl_R02_SV.Text = "SV: " + _mc02u_sv + "" + (char)176 + "C";
            lbl_R02_PV.Text = "PV: " + _mc02u_pv + "" + (char)176 + "C";
            lbl_R03_SV.Text = "SV: " + _mc03u_sv + "" + (char)176 + "C";
            lbl_R03_PV.Text = "PV: " + _mc03u_pv + "" + (char)176 + "C";
            lbl_R04_SV.Text = "SV: " + _mc04u_sv + "" + (char)176 + "C";
            lbl_R04_PV.Text = "PV: " + _mc04u_pv + "" + (char)176 + "C";
            lbl_R05_SV.Text = "SV: " + _mc05_sv + "" + (char)176 + "C";
            lbl_R05_PV.Text = "PV: " + _mc05_pv + "" + (char)176 + "C";
            lbl_R06_SV.Text = "SV: " + _mc06_sv + "" + (char)176 + "C";
            lbl_R06_PV.Text = "PV: " + _mc06_pv + "" + (char)176 + "C";

            //string minmax = "SV " + (char)177 + " 15" + (char)176 + "C";
            //lbl_R01_MM.Text = lbl_R02_MM.Text = lbl_R03_MM.Text = lbl_R04_MM.Text = minmax;
            //lbl_R05_MM.Text = lbl_R06_MM.Text = lbl_R07_MM.Text = lbl_R08_MM.Text = minmax;
            //lbl_R09_MM.Text = lbl_R10_MM.Text = minmax;
            //lbl_R13_MM.Text = lbl_R14_MM.Text = lbl_R15_MM.Text = lbl_R16_MM.Text = lbl_R17_MM.Text = lbl_R18_MM.Text = minmax;
            //lbl_R13_MM.Text = lbl_R14_MM.Text = lbl_R15_MM.Text = lbl_R16_MM.Text = minmax;
            //init_Chart_Range(cartesianChart01, _mc01u_sv);
        }
        
        public void init_Chart_Range(LiveCharts.WinForms.CartesianChart chart, double sv)
        {
            int mcNum = Convert.ToInt32(cls.Right(chart.Name, 2));
            switch (mcNum) {
            	case 1:
                    lbl_R01_MM.Text = "SV " + (char)177 + " 15" + (char)176 + "C";
            		break;
            	case 2:
                    lbl_R02_MM.Text = "SV " + (char)177 + " 15" + (char)176 + "C";
                    break;
                case 3:
                    lbl_R03_MM.Text = "SV " + (char)177 + " 15" + (char)176 + "C";
                    break;
                case 4:
                    lbl_R04_MM.Text = "SV " + (char)177 + " 15" + (char)176 + "C";
                    break;
                case 5:
                    lbl_R05_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
            		break;
            	case 6:
                    lbl_R06_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
            		break;
            	case 7:
                    lbl_R07_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
            		break;
            	case 8:
                    lbl_R08_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
            		break;
            	case 9:
                    lbl_R09_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
            		break;
            	case 10:
                    lbl_R10_MM.Text = (sv < 180.0) ? "SV " + (char)177 + " 5" + (char)176 + "C" : "SV +2/-1" + (char)176 + "C";
            		break;
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
                        Value =  (Convert.ToInt32(cls.Right(chart.Name,2))<9)? (sv + 15.0) : (sv+2.0),
                        //Value = (Convert.ToInt32(cls.Right(chart.Name,2))>10)? (sv + 5.0) : (sv<180.0)?(sv + 5.0):(sv+2.0),
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
                        Value = (Convert.ToInt32(cls.Right(chart.Name,2))<9)? (sv - 15.1) : (sv-1.1),
                        //Value = (Convert.ToInt32(cls.Right(chart.Name,2))>10)? (sv - 5.1) : (sv<180.0)?(sv-5.1):(sv-1.1),
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
            //History data
            init_Data_InDay(chart, chartvalue);

            //switch (Convert.ToInt32(cls.Right(chart.Name, 2)))
            //{
            //    case 1:
            //    case 3:
            //    case 5:
            //    case 7:
            //        init_Data_MachineDay_Upper(chart, chartvalue);
            //        break;
            //    case 2:
            //    case 4:
            //    case 6:
            //    case 8:
            //        init_Data_MachineDay_Lower(chart, chartvalue);
            //        break;
            //    case 9:
            //    case 10:
            //        init_Data_MachineDay(chart, chartvalue);
            //        break;
            //}
            //init_Data_MachineDay(chart, chartvalue);

            //The next code simulates data changes every 500 ms
            //Current data
            Timer = new System.Windows.Forms.Timer
            {
                //Interval = 500
                Interval = 5000
            };
            Timer.Tick += (sender, eventArgs) => TimerOnTick(sender, eventArgs, chart, chartvalue, sv);
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

            double minSV = 0, maxSV = 0;

            int mc = Convert.ToInt32(cls.Right(chart.Name, 2));
            switch (mc)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    maxSV = (sv + 15.0);
                    minSV = (sv - 15.0);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    //maxSV = (Convert.ToInt32(cls.Right(chart.Name, 2)) > 10) ? (sv + 5.0) : (sv < 180.0) ? (sv + 5.0) : (sv + 2.0);
                    //minSV = (Convert.ToInt32(cls.Right(chart.Name, 2)) > 10) ? (sv - 5.0) : (sv < 180.0) ? (sv - 5.1) : (sv - 1.0);
                    break;
            }


            chart.AxisY[0].MaxValue = maxSV; // lets force the axis to be 100ms ahead
            chart.AxisY[0].MinValue = minSV; //we only care about the last 8 seconds
            
            init_Chart_Range(chart,sv);

        }

        private void TimerOnTick(object sender, EventArgs eventArgs, LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue,double sv)
        {
            //init_Load_Data();
            var now = System.DateTime.Now;

            //chartvalue.Add(new MeasureModel
            //{
            //    DateTime = now,
            //    Value = _mc01u_pv
            //});
            int mcNum = Convert.ToInt32(cls.Right(chart.Name, 2));
            switch (mcNum)
            {
                case 1:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc01u_pv
                    });
                    lbl_R01_SV.Text = "SV: " + _mc01u_sv + "" + (char)176 + "C";
                    lbl_R01_PV.Text = "PV: " + _mc01u_pv + "" + (char)176 + "C";
                    break;
                case 2:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc01l_pv
                    });
                    lbl_R02_SV.Text = "SV: " + _mc01l_sv + "" + (char)176 + "C";
                    lbl_R02_PV.Text = "PV: " + _mc01l_pv + "" + (char)176 + "C";
                    break;
                case 3:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc02u_pv
                    });
                    lbl_R03_SV.Text = "SV: " + _mc02u_sv + "" + (char)176 + "C";
                    lbl_R03_PV.Text = "PV: " + _mc02u_pv + "" + (char)176 + "C";
                    break;
                case 4:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc02l_pv
                    });
                    lbl_R04_SV.Text = "SV: " + _mc02l_sv + "" + (char)176 + "C";
                    lbl_R04_PV.Text = "PV: " + _mc02l_pv + "" + (char)176 + "C";
                    break;
                case 5:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc03u_pv
                    });
                    lbl_R05_SV.Text = "SV: " + _mc03u_sv + "" + (char)176 + "C";
                    lbl_R05_PV.Text = "PV: " + _mc03u_pv + "" + (char)176 + "C";
                    break;
                case 6:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc03l_pv
                    });
                    lbl_R06_SV.Text = "SV: " + _mc03l_sv + "" + (char)176 + "C";
                    lbl_R06_PV.Text = "PV: " + _mc03l_pv + "" + (char)176 + "C";
                    break;
                case 7:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc04u_pv
                    });
                    lbl_R07_SV.Text = "SV: " + _mc04u_sv + "" + (char)176 + "C";
                    lbl_R07_PV.Text = "PV: " + _mc04u_pv + "" + (char)176 + "C";
                    break;
                case 8:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc04l_pv
                    });
                    lbl_R08_SV.Text = "SV: " + _mc04l_sv + "" + (char)176 + "C";
                    lbl_R08_PV.Text = "PV: " + _mc04l_pv + "" + (char)176 + "C";
                    break;
                case 9:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc05_pv
                    });
                    lbl_R09_SV.Text = "SV: " + _mc05_sv + "" + (char)176 + "C";
                    lbl_R09_PV.Text = "PV: " + _mc05_pv + "" + (char)176 + "C";
                    break;
                case 10:
                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _mc06_pv
                    });
                    lbl_R10_SV.Text = "SV: " + _mc06_sv + "" + (char)176 + "C";
                    lbl_R10_PV.Text = "PV: " + _mc06_pv + "" + (char)176 + "C";
                    break;
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
            int col_SV_Num = 0, col_PV_Num = 0, col_AL_Num = 0, col_DT_Num = 0;
            switch (Convert.ToInt32(mcNum))
            {
                case 1:
                    // WELDING 01 UPPER
                    col_SV_Num = 1;
                    col_PV_Num = 2;
                    col_AL_Num = 3;
                    break;
                case 2:
                    // WELDING 01 LOWER
                    col_SV_Num = 4;
                    col_PV_Num = 5;
                    col_AL_Num = 6;
                    break;
                case 3:
                    // WELDING 02 UPPER
                    col_SV_Num = 7;
                    col_PV_Num = 8;
                    col_AL_Num = 9;
                    break;
                case 4:
                    // WELDING 02 LOWER
                    col_SV_Num = 10;
                    col_PV_Num = 11;
                    col_AL_Num = 12;
                    break;
                case 5:
                    // AUTOBALANCE 01 UPPER
                    col_SV_Num = 13;
                    col_PV_Num = 14;
                    col_AL_Num = 15;
                    break;
                case 6:
                    // AUTOBALANCE 01 LOWER
                    col_SV_Num = 16;
                    col_PV_Num = 17;
                    col_AL_Num = 18;
                    break;
                case 7:
                    // AUTOBALANCE 02 UPPER
                    col_SV_Num = 19;
                    col_PV_Num = 20;
                    col_AL_Num = 21;
                    break;
                case 8:
                    // AUTOBALANCE 02 LOWER
                    col_SV_Num = 22;
                    col_PV_Num = 23;
                    col_AL_Num = 24;
                    break;
                case 9:
                    // BLOWER 01
                    col_SV_Num = 25;
                    col_PV_Num = 26;
                    col_AL_Num = 27;
                    break;
                case 10:
                    // BLOWER 02
                    col_SV_Num = 28;
                    col_PV_Num = 29;
                    col_AL_Num = 30;
                    break;
            }
            col_DT_Num = 0;

            string col_SV_Data = "", col_PV_Data = "", col_AL_Data = "", col_DT_Data = "";
            double _col_SV_Data = 0, _col_PV_Data = 0;

            string sql = "V2o1_ERP_Temperature_Init_Data_Assembly_SelItem_V1o1_Addnew";

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
                    col_AL_Data = ds.Tables[0].Rows[i][col_AL_Num].ToString();
                    col_DT_Data = ds.Tables[0].Rows[i][col_DT_Num].ToString();

                    _col_SV_Data = (col_SV_Data != "" && col_SV_Data != null) ? Convert.ToDouble(col_SV_Data) : 0;
                    _col_PV_Data = (col_PV_Data != "" && col_PV_Data != null) ? Convert.ToDouble(col_PV_Data) : 0;
                    _col_DT_Data = (col_DT_Data != "" && col_DT_Data != null) ? Convert.ToDateTime(col_DT_Data) : DateTime.Now;
                    //_col_DT_Data = Convert.ToDateTime(col_DT_Data);

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

            string sql = "V2o1_ERP_Temperature_Init_Assembly_Machine_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@ln";
            sParams[0].Value = _line;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@mc";
            sParams[1].Value = mc;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if(ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0)
            {
                rows = ds.Tables[0].Rows.Count;
                for (i = 0; i < rows; i++)
                {
                    col_sv = ds.Tables[0].Rows[i][2].ToString();
                    col_pv = ds.Tables[0].Rows[i][3].ToString();
                    //col_al = ds.Tables[0].Rows[i][3].ToString();
                    col_dt = ds.Tables[0].Rows[i][5].ToString();

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

        public void init_Data_MachineDay_Upper(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue)
        {
            int mc = Convert.ToInt32(cls.Right(chart.Name, 2));
            string col_sv = "", col_pv = "", col_dt = "";
            double _col_sv = 0, _col_pv = 0;
            DateTime _col_dt = _dt;
            int rows = 0;
            int i;

            string sql = "V2o1_ERP_Temperature_Init_Assembly_Machine_Upper_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@ln";
            sParams[0].Value = _line;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@mc";
            sParams[1].Value = mc;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rows = ds.Tables[0].Rows.Count;
                for (i = 0; i < rows; i++)
                {
                    col_sv = ds.Tables[0].Rows[i][2].ToString();
                    col_pv = ds.Tables[0].Rows[i][3].ToString();
                    //col_al = ds.Tables[0].Rows[i][3].ToString();
                    col_dt = ds.Tables[0].Rows[i][5].ToString();

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

        public void init_Data_MachineDay_Lower(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue)
        {
            int mc = Convert.ToInt32(cls.Right(chart.Name, 2));
            string col_sv = "", col_pv = "", col_dt = "";
            double _col_sv = 0, _col_pv = 0;
            DateTime _col_dt = _dt;
            int rows = 0;
            int i;

            string sql = "V2o1_ERP_Temperature_Init_Assembly_Machine_Lower_SelItem_V1o1_Addnew";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.TinyInt;
            sParams[0].ParameterName = "@ln";
            sParams[0].Value = _line;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.TinyInt;
            sParams[1].ParameterName = "@mc";
            sParams[1].Value = mc;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rows = ds.Tables[0].Rows.Count;
                for (i = 0; i < rows; i++)
                {
                    col_sv = ds.Tables[0].Rows[i][2].ToString();
                    col_pv = ds.Tables[0].Rows[i][3].ToString();
                    //col_al = ds.Tables[0].Rows[i][3].ToString();
                    col_dt = ds.Tables[0].Rows[i][5].ToString();

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
                ds = cls.ExecuteDataSet(sql, sParams);

                int tbCount = ds.Tables.Count;
                int dsCount = ds.Tables[0].Rows.Count;
                //MessageBox.Show(tbCount + "\r\n" + dsCount);

                if (tbCount > 0 && dsCount > 0)
                {
                    string temp_SV = ds.Tables[0].Rows[0][0].ToString();
                    string temp_PV = ds.Tables[0].Rows[0][1].ToString();
                    string temp_AL = ds.Tables[0].Rows[0][2].ToString();
                    string temp_DT = ds.Tables[0].Rows[0][3].ToString();
                    //string temp_RS = ds.Tables[0].Rows[0][4].ToString();

                    //MessageBox.Show(temp_SV + "\r\n" + temp_SV + "\r\n" + temp_SV + "\r\n" + temp_SV);

                    //string mc01u_sv = "", mc02u_sv = "", mc03u_sv = "", mc04u_sv = "", mc05_sv = "", mc06_sv = "", mc07_sv = "", mc08_sv = "", mc09_sv = "", mc10_sv = "", mc11_sv = "", mc12_sv = "", mc13_sv = "", mc14_sv = "", mc15_sv = "", mc16_sv = "";
                    //string mc01u_pv = "", mc02u_pv = "", mc03u_pv = "", mc04u_pv = "", mc05_pv = "", mc06_pv = "", mc07_pv = "", mc08_pv = "", mc09_pv = "", mc10_pv = "", mc11_pv = "", mc12_pv = "", mc13_pv = "", mc14_pv = "", mc15_pv = "", mc16_pv = "";
                    //string mc01u_al = "", mc02u_al = "", mc03u_al = "", mc04u_al = "", mc05_al = "", mc06_al = "", mc07_al = "", mc08_al = "", mc09_al = "", mc10_al = "", mc11_al = "", mc12_al = "", mc13_al = "", mc14_al = "", mc15_al = "", mc16_al = "";

                    //double _mc01u_sv = 0, _mc02u_sv = 0, _mc03u_sv = 0, _mc04u_sv = 0, _mc05_sv = 0, _mc06_sv = 0, _mc07_sv = 0, _mc08_sv = 0, _mc09_sv = 0, _mc10_sv = 0, _mc11_sv = 0, _mc12_sv = 0, _mc13_sv = 0, _mc14_sv = 0, _mc15_sv = 0, _mc16_sv = 0;
                    //double _mc01u_pv = 0, _mc02u_pv = 0, _mc03u_pv = 0, _mc04u_pv = 0, _mc05_pv = 0, _mc06_pv = 0, _mc07_pv = 0, _mc08_pv = 0, _mc09_pv = 0, _mc10_pv = 0, _mc11_pv = 0, _mc12_pv = 0, _mc13_pv = 0, _mc14_pv = 0, _mc15_pv = 0, _mc16_pv = 0;
                    //double _mc01u_al = 0, _mc02u_al = 0, _mc03u_al = 0, _mc04u_al = 0, _mc05_al = 0, _mc06_al = 0, _mc07_al = 0, _mc08_al = 0, _mc09_al = 0, _mc10_al = 0, _mc11_al = 0, _mc12_al = 0, _mc13_al = 0, _mc14_al = 0, _mc15_al = 0, _mc16_al = 0;

                    mc01u_sv = temp_SV.Substring(112, 4);
                    mc01l_sv = temp_SV.Substring(116, 4);
                    mc02u_sv = temp_SV.Substring(120, 4);
                    mc02l_sv = temp_SV.Substring(124, 4);
                    mc03u_sv = temp_SV.Substring(128, 4);
                    mc03l_sv = temp_SV.Substring(132, 4);
                    mc04u_sv = temp_SV.Substring(136, 4);
                    mc04l_sv = temp_SV.Substring(140, 4);
                    mc05_sv = temp_SV.Substring(144, 4);
                    mc06_sv = temp_SV.Substring(148, 4);

                    _mc01u_sv = (mc01u_sv != "0" && mc01u_sv != null) ? Convert.ToDouble(mc01u_sv) : 0;
                    _mc01l_sv = (mc01l_sv != "0" && mc01l_sv != null) ? Convert.ToDouble(mc01l_sv) : 0;
                    _mc02u_sv = (mc02u_sv != "0" && mc02u_sv != null) ? Convert.ToDouble(mc02u_sv) : 0;
                    _mc02l_sv = (mc02l_sv != "0" && mc02l_sv != null) ? Convert.ToDouble(mc02l_sv) : 0;
                    _mc03u_sv = (mc03u_sv != "0" && mc03u_sv != null) ? Convert.ToDouble(mc03u_sv) : 0;
                    _mc03l_sv = (mc03l_sv != "0" && mc03l_sv != null) ? Convert.ToDouble(mc03l_sv) : 0;
                    _mc04u_sv = (mc04u_sv != "0" && mc04u_sv != null) ? Convert.ToDouble(mc04u_sv) : 0;
                    _mc04l_sv = (mc04l_sv != "0" && mc04l_sv != null) ? Convert.ToDouble(mc04l_sv) : 0;
                    _mc05_sv = (mc05_sv != "0" && mc05_sv != null) ? Convert.ToDouble(mc05_sv) : 0;
                    _mc06_sv = (mc06_sv != "0" && mc06_sv != null) ? Convert.ToDouble(mc06_sv) : 0;

                    //string msg = "";
                    //msg += "mc01u_sv: " + mc01u_sv + "\r\n";
                    //msg += "mc01l_sv: " + mc01l_sv + "\r\n";
                    //msg += "mc02u_sv: " + mc02u_sv + "\r\n";
                    //msg += "mc02l_sv: " + mc02l_sv + "\r\n";
                    //msg += "mc03u_sv: " + mc03u_sv + "\r\n";
                    //msg += "mc03l_sv: " + mc03l_sv + "\r\n";
                    //msg += "mc04u_sv: " + mc04u_sv + "\r\n";
                    //msg += "mc04l_sv: " + mc04l_sv + "\r\n";
                    //msg += "mc05_sv: " + mc05_sv + "\r\n";
                    //msg += "mc06_sv: " + mc06_sv + "\r\n";
                    //MessageBox.Show(msg);

                    /**************************************/


                    ////premc01u_pv = mc01u_pv;
                    ////premc02u_pv = mc02u_pv;
                    ////premc03u_pv = mc03u_pv;
                    ////premc04u_pv = mc04u_pv;
                    ////premc05_pv = mc05_pv;
                    ////premc06_pv = mc06_pv;
                    ////premc07_pv = mc07_pv;
                    ////premc08_pv = mc08_pv;
                    ////premc09_pv = mc09_pv;
                    ////premc10_pv = mc10_pv;
                    ////premc11_pv = mc11_pv;
                    ////premc12_pv = mc12_pv;
                    ////premc13_pv = mc13_pv;
                    ////premc14_pv = mc14_pv;
                    ////premc15_pv = mc15_pv;
                    ////premc16_pv = mc16_pv;

                    mc01u_pv = temp_PV.Substring(112, 4);
                    mc01l_pv = temp_PV.Substring(116, 4);
                    mc02u_pv = temp_PV.Substring(120, 4);
                    mc02l_pv = temp_PV.Substring(124, 4);
                    mc03u_pv = temp_PV.Substring(128, 4);
                    mc03l_pv = temp_PV.Substring(132, 4);
                    mc04u_pv = temp_PV.Substring(136, 4);
                    mc04l_pv = temp_PV.Substring(140, 4);
                    mc05_pv = temp_PV.Substring(144, 4);
                    mc06_pv = temp_PV.Substring(148, 4);

                    _mc01u_pv = (mc01u_pv != "0" && mc01u_pv != null) ? Convert.ToDouble(mc01u_pv) : 0;
                    _mc01l_pv = (mc01l_pv != "0" && mc01l_pv != null) ? Convert.ToDouble(mc01l_pv) : 0;
                    _mc02u_pv = (mc02u_pv != "0" && mc02u_pv != null) ? Convert.ToDouble(mc02u_pv) : 0;
                    _mc02l_pv = (mc02l_pv != "0" && mc02l_pv != null) ? Convert.ToDouble(mc02l_pv) : 0;
                    _mc03u_pv = (mc03u_pv != "0" && mc03u_pv != null) ? Convert.ToDouble(mc03u_pv) : 0;
                    _mc03l_pv = (mc03l_pv != "0" && mc03l_pv != null) ? Convert.ToDouble(mc03l_pv) : 0;
                    _mc04u_pv = (mc04u_pv != "0" && mc04u_pv != null) ? Convert.ToDouble(mc04u_pv) : 0;
                    _mc04l_pv = (mc04l_pv != "0" && mc04l_pv != null) ? Convert.ToDouble(mc04l_pv) : 0;
                    _mc05_pv = (mc05_pv != "0" && mc05_pv != null) ? Convert.ToDouble(mc05_pv) : 0;
                    _mc06_pv = (mc06_pv != "0" && mc06_pv != null) ? Convert.ToDouble(mc06_pv) : 0;


                    /**************************************/


                    mc01u_al = temp_AL.Substring(112, 4);
                    mc01l_al = temp_AL.Substring(116, 4);
                    mc02u_al = temp_AL.Substring(120, 4);
                    mc02l_al = temp_AL.Substring(124, 4);
                    mc03u_al = temp_AL.Substring(128, 4);
                    mc03l_al = temp_AL.Substring(132, 4);
                    mc04u_al = temp_AL.Substring(136, 4);
                    mc04l_al = temp_AL.Substring(140, 4);
                    mc05_al = temp_AL.Substring(144, 4);
                    mc06_al = temp_AL.Substring(148, 4);


                    _mc01u_al = (mc01u_al != "" && mc01u_al != null) ? Convert.ToDouble(mc01u_al) : 0;
                    _mc01l_al = (mc01l_al != "" && mc01l_al != null) ? Convert.ToDouble(mc01l_al) : 0;
                    _mc02u_al = (mc02u_al != "" && mc02u_al != null) ? Convert.ToDouble(mc02u_al) : 0;
                    _mc02l_al = (mc02l_al != "" && mc02l_al != null) ? Convert.ToDouble(mc02l_al) : 0;
                    _mc03u_al = (mc03u_al != "" && mc03u_al != null) ? Convert.ToDouble(mc03u_al) : 0;
                    _mc03l_al = (mc03l_al != "" && mc03l_al != null) ? Convert.ToDouble(mc03l_al) : 0;
                    _mc04u_al = (mc04u_al != "" && mc04u_al != null) ? Convert.ToDouble(mc04u_al) : 0;
                    _mc04l_al = (mc04l_al != "" && mc04l_al != null) ? Convert.ToDouble(mc04l_al) : 0;
                    _mc05_al = (mc05_al != "" && mc05_al != null) ? Convert.ToDouble(mc05_al) : 0;
                    _mc06_al = (mc06_al != "" && mc06_al != null) ? Convert.ToDouble(mc06_al) : 0;

                    //string msg = "";
                    //msg += "_mc01u_al: " + _mc01u_al + "\r\n";
                    //msg += "_mc01l_al: " + _mc01l_al + "\r\n";
                    //msg += "_mc02u_al: " + _mc02u_al + "\r\n";
                    //msg += "_mc02l_al: " + _mc02l_al + "\r\n";
                    //msg += "_mc03u_al: " + _mc03u_al + "\r\n";
                    //msg += "_mc03l_al: " + _mc03l_al + "\r\n";
                    //msg += "_mc04u_al: " + _mc04u_al + "\r\n";
                    //msg += "_mc04l_al: " + _mc04l_al + "\r\n";
                    //msg += "_mc05_al: " + _mc05_al + "\r\n";
                    //msg += "_mc06_al: " + _mc06_al + "\r\n";
                    //MessageBox.Show(msg);


                    lbl_A01_MC.BackColor = (_mc01u_al == 1 || _mc01l_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A01_UP.BackColor = (_mc01u_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A01_LO.BackColor = (_mc01l_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lbl_A02_MC.BackColor = (_mc02u_al == 1 || _mc02l_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A02_UP.BackColor = (_mc02u_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A02_LO.BackColor = (_mc02l_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lbl_A03_MC.BackColor = (_mc03u_al == 1 || _mc03l_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A03_UP.BackColor = (_mc03u_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A03_LO.BackColor = (_mc03l_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lbl_A04_MC.BackColor = (_mc04u_al == 1 || _mc04l_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A04_UP.BackColor = (_mc04u_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A04_LO.BackColor = (_mc04l_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lbl_A05_MC.BackColor = (_mc05_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A05_UP.BackColor = (_mc05_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    lbl_A06_MC.BackColor = (_mc06_al == 1) ? System.Drawing.Color.Red : System.Drawing.Color.FromKnownColor(KnownColor.Control);
                    lbl_A06_UP.BackColor = (_mc06_al == 1) ? System.Drawing.Color.Gold : System.Drawing.Color.FromKnownColor(KnownColor.Control);

                    //lbl_A01_UP.BackColor = System.Drawing.Color.Gold;

                    /**************************************/


                    //lbl_A01_MC.BackColor = (_mc01u_al == 1 || _mc01l_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //lbl_A02_MC.BackColor = (_mc02u_al == 1 || _mc02l_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //lbl_A03_MC.BackColor = (_mc03u_al == 1 || _mc03l_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //lbl_A04_MC.BackColor = (_mc04u_al == 1 || _mc04l_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //lbl_R05_MC.BackColor = (_mc05_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
                    //lbl_R06_MC.BackColor = (_mc06_al == 1) ? Color.Red : Color.FromKnownColor(KnownColor.Control);
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
                    //Thread.Sleep(1000);
                }
            })
            {
                IsBackground = true
            };
            loadData.Start();


            Thread resetUpdate = new Thread(() =>
            {
                while (true)
                {
                    init_Load_Update();
                    Thread.Sleep(5000);
                }
            })
            {
                IsBackground = true
            };
            resetUpdate.Start();


            Thread loadAlarm = new Thread(() =>
            {
                while (true)
                {

                    Thread.Sleep(20000);
                }
            })
            {
                IsBackground = true
            };
            loadAlarm.Start();
        }
    }
}
