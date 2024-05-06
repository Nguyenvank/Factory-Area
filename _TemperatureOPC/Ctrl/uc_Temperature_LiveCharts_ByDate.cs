using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_LiveCharts_ByDate : UserControl
    {
        private static uc_Temperature_LiveCharts_ByDate _instance;
        public static uc_Temperature_LiveCharts_ByDate Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_LiveCharts_ByDate();
                return _instance;
            }
        }

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        DateTime _dt;

        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        public ChartValues<MeasureModel> ChartValues { get; set; }

        public uc_Temperature_LiveCharts_ByDate()
        {
            InitializeComponent();
        }

        private void uc_Temperature_LiveCharts_ByDate_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            //string str01 = "06/02/2019 00:12:00 0 0";
            //string str02 = "10/02/2019 00:34:01 180 182";
            //string str03 = "11/02/2019 11:56:01 0180 0182";

            //string str0101 = str01.Substring(0,19);
            //string str0102 = str01.Substring(20, 1);
            //string str0103 = str01.Substring(22, 1);

            //string str0201 = str02.Substring(0, 19);
            //string str0202 = str02.Substring(20, 3);
            //string str0203 = str02.Substring(24, 3);

            //string str0301 = str03.Substring(0, 19);
            //string str0302 = str03.Substring(20, 4);
            //string str0303 = str03.Substring(25, 4);

            //MessageBox.Show(str01 + ": " + str01.Length + "\r\n" + str02 + ": " + str02.Length + "\r\n" + str03 + ": " + str03.Length);
            //MessageBox.Show(str01 + "\r\n" + str0101 + "\r\n" + str0102 + "\r\n" + str0103);
            //MessageBox.Show(str02 + "\r\n" + str0201 + "\r\n" + str0202 + "\r\n" + str0203);
            //MessageBox.Show(str03 + "\r\n" + str0301 + "\r\n" + str0302 + "\r\n" + str0303);

            dtp_Date.MinDate = new DateTime(2019, 1, 30, 8, 0, 0);
            //dtp_Date.MaxDate = (_dt.Hour < 12) ? _dt.AddDays(-1) : _dt.AddDays(0);
            dtp_Date.MaxDate = DateTime.Now.AddDays(0);
        }

        private void cbb_Machine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_Machine.SelectedIndex > 0)
            {
                dtp_Date.Enabled = true;
                btn_Load.Enabled = true;
            }
            else
            {
                dtp_Date.Enabled = false;
                btn_Load.Enabled = false;
            }
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            cartesianChart01.Visible = true;
            init_Chart(cartesianChart01, ChartValues, init_Data_SV());
        }

        private void SetAxisLimits(System.DateTime now, LiveCharts.WinForms.CartesianChart chart, double sv)
        {
            //cartesianChart1.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 100ms ahead
            //cartesianChart1.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds

            chart.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(0).Ticks; // lets force the axis to be 100ms ahead
            chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromHours(24).Ticks; //we only care about the last 8 seconds

            //chart.AxisY[0].MaxValue = 190; // lets force the axis to be 100ms ahead
            //chart.AxisY[0].MinValue = 155; //we only care about the last 8 seconds

            double maxSV = (sv + 8.0);
            double minSV = (sv - 8.0);

            chart.AxisY[0].MaxValue = maxSV; // lets force the axis to be 100ms ahead
            chart.AxisY[0].MinValue = minSV; //we only care about the last 8 seconds

        }

        public void init_Chart(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue, double sv)
        {
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
                LabelFormatter = value => new System.DateTime((long)value).ToString("dd/MM/yy\r\nHH:mm"),
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
                        Value = (sv + 5),
                        SectionWidth = 0.05,
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
                        SectionWidth = 0.05,
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
                        Value = (sv-5),
                        SectionWidth = 0.05,
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
            //chart.DataTooltip = null;

            //chart.Zoom = ZoomingOptions.X;

            SetAxisLimits(System.DateTime.Now, chart, sv);

            //Initialize data at beginning open program
            init_Data_MachineDay(chart, chartvalue);
        }

        public void init_Data_MachineDay(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue)
        {
            //while (chartvalue.Count > 0) { chartvalue.RemoveAt(0); }
            //chart.AxisX[0].MinValue = double.NaN;
            //chart.AxisX[0].MaxValue = double.NaN;
            //chart.AxisY[0].MinValue = double.NaN;
            //chart.AxisY[0].MaxValue = double.NaN;

            string dataFile = "RUBBER" + String.Format("{0:00}", cbb_Machine.SelectedIndex) + ".txt";
            DateTime selDate = Convert.ToDateTime(dtp_Date.Value);
            int selYear = Convert.ToInt32(selDate.Year);
            int selMonth = Convert.ToInt32(selDate.Month);
            int selDay = Convert.ToInt32(selDate.Day);

            string dataPath = @"\\\\192.168.1.2\\Temperatures Monitoring\\";
            dataPath += selYear + @"\\";
            dataPath += selYear + "" + String.Format("{0:00}", selMonth) + @"\\";
            dataPath += selYear + "" + String.Format("{0:00}", selMonth) + "" + String.Format("{0:00}", selDay) + @"\\";
            dataPath += dataFile;

            string msg = "";
            string line = "";
            string dates = "", sv = "", pv = "";
            DateTime _dates;
            double _sv, _pv;

            if (File.Exists(dataPath))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(dataPath);
                while ((line = file.ReadLine()) != null)
                {
                    dates = line.Substring(0, 19).Trim();
                    switch (line.Length)
                    {
                        case 23:
                            sv = line.Substring(20, 1).Trim();
                            pv = line.Substring(22, 1).Trim();
                            break;
                        case 27:
                            sv = line.Substring(20, 3).Trim();
                            pv = line.Substring(24, 3).Trim();
                            break;
                        case 29:
                            sv = line.Substring(20, 4).Trim();
                            pv = line.Substring(25, 4).Trim();
                            break;
                    }

                    _dates = Convert.ToDateTime(dates);
                    _sv = Convert.ToDouble(sv);
                    _pv = Convert.ToDouble(pv);

                    chartvalue.Add(new MeasureModel
                    {
                        DateTime = _dates,
                        Value = _pv
                    });
                    SetAxisLimits(_dates, chart, _sv);
                    //MessageBox.Show(dates + " - " + value);
                    //break;
                }
                file.Close();
            }
            else
            {
                cartesianChart01.Visible = false;
                msg = "Cannot open data file (" + dataFile + "). Please re-check and try again.";
                MessageBox.Show(msg);
            }

            //FileInfo myFile = new FileInfo(dataPath);
            //bool fleExist = myFile.Exists;
            //if (fleExist)
            //{
            //    msg = "OK";
            //}
            //else
            //{
            //    msg = "NG";
            //}
        }

        public double init_Data_SV()
        {
            string dataFile = "RUBBER" + String.Format("{0:00}", cbb_Machine.SelectedIndex) + ".txt";
            DateTime selDate = Convert.ToDateTime(dtp_Date.Value);
            int selYear = Convert.ToInt32(selDate.Year);
            int selMonth = Convert.ToInt32(selDate.Month);
            int selDay = Convert.ToInt32(selDate.Day);

            string dataPath = @"\\\\192.168.1.2\\Temperatures Monitoring\\";
            dataPath += selYear + @"\\";
            dataPath += selYear + "" + String.Format("{0:00}", selMonth) + @"\\";
            dataPath += selYear + "" + String.Format("{0:00}", selMonth) + "" + String.Format("{0:00}", selDay) + @"\\";
            dataPath += dataFile;

            string msg = "";
            string line = "";
            string sv = "";
            double _sv = 0;

            if (File.Exists(dataPath))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(dataPath);
                while ((line = file.ReadLine()) != null)
                {
                    switch (line.Length)
                    {
                        case 23:
                            sv = line.Substring(20, 1).Trim();
                            break;
                        case 27:
                            sv = line.Substring(20, 3).Trim();
                            break;
                        case 29:
                            sv = line.Substring(20, 4).Trim();
                            break;
                    }
                    //sv = line.Substring(20, 3).Trim();
                    _sv = Convert.ToDouble(sv);
                    break;
                }
                file.Close();
            }
            else
            {
                msg = "Cannot open data file (" + dataFile + "). Please re-check and try again......";
            }
            return _sv;
        }
    }
}
