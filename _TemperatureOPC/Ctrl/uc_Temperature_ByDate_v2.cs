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
using System.Windows.Media;
using System.IO;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_ByDate_v2 : UserControl
    {
        private static uc_Temperature_ByDate_v2 _instance;
        public static uc_Temperature_ByDate_v2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_ByDate_v2();
                return _instance;
            }
        }

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");

        string _line, _machine, _date;
        string _machineName;

        public uc_Temperature_ByDate_v2()
        {
            InitializeComponent();

            _line = ini.GetIniValue("TEMPERATURE", "MACHINE").Substring(0, 2);
            _machine = ini.GetIniValue("TEMPERATURE", "MACHINE").Substring(3, 2);
            _date = ini.GetIniValue("TEMPERATURE", "DATE");

            int ln = (_line != "" && _line != null) ? Convert.ToInt32(_line) : 0;
            int mc = (_machine != "" && _machine != null) ? Convert.ToInt32(_machine) : 0;
            string nm = "";

            switch (ln)
            {
                case 0:
                    break;
                case 1:
                    nm = "RUBBER " + String.Format("{0:00}", Convert.ToInt32(mc));
                    _machineName = nm.Replace(" ", "") + ".txt";
                    break;
                case 2:
                    nm = "PLASTIC "+ String.Format("{0:00}", Convert.ToInt32(mc));
                    _machineName = nm.Replace(" ", "") + ".txt";
                    break;
                case 3:
                    switch (mc)
                    {
                        case 1:
                            nm = "WELDING 01";
                            break;
                        case 2:
                            nm = "WELDING 02";
                            break;
                        case 3:
                            nm = "A.BALANCE 01";
                            break;
                        case 4:
                            nm = "A.BALANCE 02";
                            break;
                        case 5:
                            nm = "BLOWER 01";
                            break;
                        case 6:
                            nm = "BLOWER 02";
                            break;
                    }
                    _machineName = ("ASSEMBLY " + String.Format("{0:00}", Convert.ToInt32(mc))).Replace(" ", "") + ".txt";
                    break;
            }


            //lbl_Temp_Machine.Text = "RUBBER " + String.Format("{0:00}", Convert.ToInt32(_machine));
            lbl_Temp_Machine.Text = nm;
            lbl_Temp_Date.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(_date));

            init_Chart(cartesianChart01, ChartValues, init_Data_SV());
        }

        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        public ChartValues<MeasureModel> ChartValues { get; set; }

        private void uc_Temperature_ByDate_v2_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(_line.ToString());
        }

        private void SetAxisLimits(System.DateTime now, LiveCharts.WinForms.CartesianChart chart, double sv)
        {
            int ln = Convert.ToInt32(_line);
            int mc = Convert.ToInt32(_machine);
            //cartesianChart1.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 100ms ahead
            //cartesianChart1.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds

            chart.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(0).Ticks; // lets force the axis to be 100ms ahead
            chart.AxisX[0].MinValue = now.Ticks - TimeSpan.FromHours(24).Ticks; //we only care about the last 8 seconds

            //chart.AxisY[0].MaxValue = 190; // lets force the axis to be 100ms ahead
            //chart.AxisY[0].MinValue = 155; //we only care about the last 8 seconds

            double maxSV = 0, minSV = 0;

            switch (ln)
            {
                case 1:
                    maxSV = (sv + 8.0);
                    minSV = (sv - 8.0);
                    break;
                case 2:
                    maxSV = (230.0 + 10.0);
                    minSV = (190.0 - 15.0);
                    break;
                case 3:
                    maxSV = (sv + 8.0);
                    minSV = (sv - 8.0);
                    break;
            }

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
                        //Value = (sv + 5),
                        Value = (_line == "01") ? (sv + 5.0):(_line == "02") ? 230.0 : (sv + 5.0),
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
                        //Value = (sv-5),
                        Value = (_line == "01") ? (sv - 5.0):(_line == "02") ? 190.0 : (sv - 5.0),
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

        private void lbl_Temp_Date_DoubleClick(object sender, EventArgs e)
        {
            //DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            //if (dialog == DialogResult.Yes)
            //{
            //    System.Windows.Forms.Application.Exit();
            //    frmTemperatureByDate_v2 frm = new frmTemperatureByDate_v2();
            //    frm.Show();
            //}
        }

        private void lbl_Temp_Date_MouseHover(object sender, EventArgs e)
        {
            lbl_Temp_Date.BackColor = System.Drawing.Color.LightGreen;
            lbl_Temp_Date.Cursor = Cursors.Hand;
        }

        private void lbl_Temp_Date_MouseLeave(object sender, EventArgs e)
        {
            lbl_Temp_Date.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.Control);
            lbl_Temp_Date.Cursor = Cursors.Default;
        }

        public void init_Data_MachineDay(LiveCharts.WinForms.CartesianChart chart, ChartValues<MeasureModel> chartvalue)
        {
            //while (chartvalue.Count > 0) { chartvalue.RemoveAt(0); }
            //chart.AxisX[0].MinValue = double.NaN;
            //chart.AxisX[0].MaxValue = double.NaN;
            //chart.AxisY[0].MinValue = double.NaN;
            //chart.AxisY[0].MaxValue = double.NaN;

            //string dataFile = "RUBBER" + String.Format("{0:00}", Convert.ToInt32(_machine)) + ".txt";
            string dataFile = _machineName;
            DateTime selDate = Convert.ToDateTime(_date);
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
            //string dataFile = "RUBBER" + String.Format("{0:00}", Convert.ToInt32(_machine)) + ".txt";
            string dataFile = _machineName;
            DateTime selDate = Convert.ToDateTime(_date);
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
