using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rei;

namespace SachulGraph
{
    public partial class Form1 : Form
    {
        // DB 연결 관련
        private msServer sv = new msServer();
        private msServer sv2 = new msServer();

        // 사출기기 정보
        private static Dictionary<string, int> sachul_info = new Dictionary<string, int>();

        // INI 파일 관련
        Ini ini = new Ini(Application.StartupPath + "\\" + Application.ProductName+".ini");

        // 기본값
        public static string factory_name = "";
        public static string factory_code = "";
        public static string db_name = "";
        public static string db_name2 = "";
        public static string db_ip = "";
        public static int minute = 0;
        public int rtime = 20;
        public int mtime = 20;
        public int line_width = 1;
        public int[] gRow;
        public int timeLine = 9;
        public int iTime = 10;
        public int iShifts = 1;
        public DateTime real_time = DateTime.Now;
        public Boolean  bRealCheck = true;


        public Form1()
        {
            InitializeComponent();

            // ini 파일이 없으면,
            if (ini.IniExists() == false)
            {
                MessageBox.Show(Application.StartupPath + "\\" + Application.ProductName+".ini 파일이 없습니다. ini 파일을 설정, 생성해 주세요!");
                return;
            }
            else // ini 파일이 있으면 설정값을 가져온다.
            {
                factory_name = ini.GetIniValue("FACTORY", "NAME", "사출 모니터링").Trim();
                factory_code = ini.GetIniValue("FACTORY", "CODE", "F1").Trim();
                db_ip = ini.GetIniValue("DB", "IP", "").Trim();
                db_name = ini.GetIniValue("DB", "NAME", "INJREC_M").Trim();
                db_name2 = ini.GetIniValue("DB", "NAME2", "INJDB").Trim();
                minute = Convert.ToInt32("-" + ini.GetIniValue("SETTING", "MINUTES", "60").Trim());
                mtime = Convert.ToInt32(ini.GetIniValue("SETTING", "MTIME", "20").Trim());
                line_width = Convert.ToInt32(ini.GetIniValue("SETTING", "LINEWIDTH", "1").Trim());
                timeLine = Convert.ToInt32(ini.GetIniValue("SETTING", "TIMELINE", "9").Trim());
                iTime = Convert.ToInt32(ini.GetIniValue("SETTING", "ITIME", "10").Trim());
                bRealCheck= Convert.ToBoolean (Convert.ToInt32(ini.GetIniValue("SETTING", "REALCHECK", "1").Trim()));
                rtime = mtime;
               
            }
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMM dd, yyyy - dddd";
        }




        private void get_time()
        {          //DateTime current_time = DateTime.UtcNow.AddMinutes(0);//DateTime.Now; 

            // sv._query = "SELECT  DASYSTEM.DBO.get_stdtime('" + db_name2 + "')  datetime1 ";
            // if (sv.execQuery())
            // {
            //     real_time = Convert.ToDateTime(sv._ds.Tables[0].Rows[0][0].ToString());
            real_time = DateTime.Now;
            // }

            //MessageBox.Show(current_time.ToString());
            toolStripStatusLabel1.Text = real_time.ToString(); 
        }

        /// <summary>
        /// 그래프 그리기
        /// </summary>
        private void DrawChartGraph()
        {
            //DateTime current_time = DateTime.UtcNow.AddMinutes(0);//DateTime.Now; 
            DateTime current_time = dateTimePicker1.Value.Date.AddHours(iShifts * 12 + 8);


            DateTime base_time = current_time.AddHours(-12);

            int iRow = 0;
            string sql = "";
            int mLine = 0;
            foreach (KeyValuePair<string, int> each in sachul_info)
            {
                string s_name = each.Key;
                int s_no = each.Value;
                int asciiCode = (int)Convert.ToChar (s_name);
                string s_name2 = Convert.ToChar(asciiCode+1).ToString();
                //
                //앞 라인 MAX           

                sql = "select MAX(eqmno)  from  " + db_name2 + ".dbo.bm_eqm where factcd = '" + factory_code + "' and used = 1 AND line='" + s_name.Substring(0, 1) + "'";
                sv._query = sql;
                if (sv.execQuery())
                {
                    mLine = Convert.ToInt16(sv._ds.Tables[0].Rows[0][0]);
                }


                //
                string chart_name = "chart_" + s_name;
                Control[] con = tableLayoutPanel1.Controls.Find(chart_name, false);

                foreach (System.Windows.Forms.DataVisualization.Charting.Chart chart in con)
                {
                    int y_value = 15;//////////////16
                    int y_value2 = 14;//////////////15
                    int j = 0;
                    string sLineNo = "";
                    y_value = gRow[iRow] * 2 ;
                    y_value2 = gRow[iRow] * 2-1;
                    iRow ++;


                    chart.ChartAreas["ChartArea1"].AxisX.Minimum = base_time.ToOADate();
                    chart.ChartAreas["ChartArea1"].AxisX.Maximum = current_time.ToOADate() + 0.001;

                    for (int i = 0; i < s_no; i++)
                    {
                        j = i + 1;
                        if (j<=mLine)
                        {
                            sLineNo = s_name.Substring(0, 1) + Right("0" + j, 2);
                        }
                        else
                        {
                            sLineNo = s_name2.Substring(0, 1) + Right("0" + (j- mLine), 2);
                        }

                        sql = "select pk_datetime, onoff from " + db_name + ".dbo.H_" + sLineNo
                                + " where pk_datetime >= convert(Datetime, '" + base_time.ToString("yyyy-MM-dd HH:mm:ss") + "', 20) "
                                + " and  pk_datetime < convert(Datetime, '" + current_time.ToString("yyyy-MM-dd HH:mm:ss") + "', 20) "
                                + " order by pk_datetime asc";
                        sv._query = sql;
 

                        if (sv.execQuery())
                        {

                            chart.ResetAutoValues();

                            // 그래프가 이미 존재하면 삭제
                            if (chart.Series[i].Points.Count > 0)
                            {
                                chart.Series[i].Points.Clear();
                            }

                            for (int k = 0; k < sv._ds.Tables[0].Rows.Count; k++)
                            {
                                DateTime time = Convert.ToDateTime(sv._ds.Tables[0].Rows[k][0]);

                                bool state = Convert.ToBoolean(sv._ds.Tables[0].Rows[k][1].ToString());

                                if (state == false)
                                {
                                    chart.Series[i].Points.AddXY(time.ToOADate(), y_value2);
                                }
                                else
                                {
                                    chart.Series[i].Points.AddXY(time.ToOADate(), y_value);
                                }

                                chart.Invalidate();
                            }
                        }
                        else
                        {
                            chart.Series[i].Points.AddXY(current_time.ToOADate(), y_value2);
                            chart.Series[i].Points.AddXY(base_time.ToOADate(), y_value2);
                            chart.Invalidate();
                        }
                        y_value = y_value - 2;
                        y_value2 = y_value2 - 2;
                    }
                }
            }
        }

        /// <summary>
        /// 차트 컨트롤을 생성함
        /// </summary>
        private void AddChartControl()
        {
            string sql = "select min( line) line, COUNT(eqmno) as line_no , (rtrim(substring(eqmnm,1,charindex(' ',eqmnm) ))) GRPNM,(ASCII(min(LINE)))/4  ROWID   from  " + db_name2 + ".dbo.bm_eqm where factcd = '" + factory_code + "' and used = 1 and line not in ('P','Q','R','V') group by rtrim(substring(eqmnm,1,charindex(' ',eqmnm) ))  ";
            sv._query = sql;
            //sv._query = "select 'A1',5 union all select 'A2',5 union all select 'A3',5 union all select 'A4',5 union all select 'A5',2 ";

            if (sv.execQuery()) // 정보가 있을 경우
            {
                int sachul_number = 0;
                int gCount = sv._ds.Tables[0].Rows.Count;

                gRow = new int[gCount+1];
                if (gCount==1)
                {
                    tableLayoutPanel1.ColumnCount = 1;
                    tableLayoutPanel1.RowCount = 1;
                   gRow[0]= Convert.ToInt32(sv._ds.Tables[0].Rows[0][1].ToString());
                }
                else
                {
                    tableLayoutPanel1.ColumnCount = 2;
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    tableLayoutPanel1.RowCount = 1;
                    sv2._query = " SELECT ROWID, MAX(LINE_NO) LINE_NO  FROM(" + sql +" ) A GROUP BY ROWID order by 1 ";
                    if (sv2.execQuery()) // 정보가 있을 경우
                    {
                        int Gcount2 = sv2._ds.Tables[0].Rows.Count;
                        tableLayoutPanel1.RowCount = Gcount2;
                        int iCnt = 0;
                        int gCnt = 0;
                        for (int i = 0; i < Gcount2; i++)
                        {
                            gRow[i*2] =  Convert.ToInt32(sv2._ds.Tables[0].Rows[i][1].ToString());
                            gRow[i * 2 + 1] =   Convert.ToInt32(sv2._ds.Tables[0].Rows[i][1].ToString());
                            iCnt = iCnt +3+gRow[i*2];
                        }
                        for (int i = 0; i < Gcount2; i++)
                        {
                            gCnt = Convert.ToInt32((gRow[i * 2]+3) * 100/ iCnt);
                            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, gCnt));
                        }
                    }
                }



                for (int i = 0; i < gCount; i++)
                {
                    string line_name = sv._ds.Tables[0].Rows[i][0].ToString();
                    string line_no = sv._ds.Tables[0].Rows[i][1].ToString();
                    string group_name = sv._ds.Tables[0].Rows[i][2].ToString();
                     
                    // 사출기 정보 관련
                    sachul_info.Add(line_name, Convert.ToInt32(line_no));

                    // Chart 추가 (속성 설정)
                    System.Windows.Forms.DataVisualization.Charting.Chart chartCon = new System.Windows.Forms.DataVisualization.Charting.Chart();

                    chartCon.BackColor = Color.Black; //색상
                    chartCon.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
                    chartCon.PaletteCustomColors = new Color[] {
                                                Color.Red,
                                                Color.Yellow,
                                                Color.White,
                                                Color.Fuchsia,
                                                Color.Aqua,
                                                Color.Lime,
                                                Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))),
                                                Color.MediumSpringGreen,
                                                Color.Khaki,
                                                Color.RoyalBlue,
                                                Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
                                                Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128))))),
                                                Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
                                                Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
                                                Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
                                                Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255))))),
                                                Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))))};
                    chartCon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; // Anchor
                    chartCon.Name = "chart_" + line_name; // Chart Control 이름 chart_A 
                    // ChartArea 추가 및 속성
                    chartCon.ChartAreas.Add("ChartArea1");
                    chartCon.ChartAreas["ChartArea1"].BackColor = Color.Black;
                   //chartCon.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                   //chartCon.ChartAreas["ChartArea1"].AxisX.Maximum = 3700;
                   chartCon.ChartAreas["ChartArea1"].AxisY.Minimum = 0;

                    chartCon.ChartAreas["ChartArea1"].AxisY.Maximum = gRow[i]*2+1 ;

                    chartCon.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
                    chartCon.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;

                    //chartCon.ChartAreas["ChartArea1"].AxisX.Interval = 600;

                    chartCon.ChartAreas["ChartArea1"].AxisX.LineColor = Color.LightGray;
                    chartCon.ChartAreas["ChartArea1"].AxisY.LineColor = Color.LightGray;
                    chartCon.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.DimGray;
                    chartCon.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = 60* iTime;
                    chartCon.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 60 * iTime;
                    chartCon.ChartAreas["ChartArea1"].AxisX.LabelStyle.ForeColor = Color.White;
                    chartCon.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "HH:mm";
                   

                    chartCon.Legends.Add("Legend1"); 
                    chartCon.Legends["Legend1"].Title = group_name;// line_name + "_GROUP";
                    chartCon.Legends["Legend1"].TitleForeColor = Color.White;
                    chartCon.Legends["Legend1"].BackColor = System.Drawing.Color.Black;
                    chartCon.Legends["Legend1"].ForeColor = System.Drawing.Color.White;
                    chartCon.Legends["Legend1"].BorderColor = Color.White;

                    // Serises 추가 및 속성
                    for (int j = 0; j < Convert.ToInt32(line_no); j++)
                    {
                        sachul_number++;

                        string serise_name = "Series" + (j + 1);

                        chartCon.Series.Add(serise_name);


                        // Chart Type
                        chartCon.Series[j].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
                        chartCon.Series[j].LegendText = sachul_number.ToString() ; 


                        sv2._query = "SELECT eqmno,replace(eqmnm, substring(eqmnm, 1, charindex(' ', eqmnm)), '')   FROM  " + db_name2 + ".dbo.bm_eqm where factcd = '" + factory_code + "'  and used = 1 and eqmno1=" + sachul_number.ToString();
                        if (sv2.execQuery())
                        {
                            chartCon.Series[j].LegendText = sv2._ds.Tables[0].Rows[0][1].ToString();
                        } 
                        chartCon.Series[j].BorderWidth = line_width;
                        //chartCon.Legends.Add(serise_name);
                        chartCon.ChartAreas[0].AxisY.StripLines.Add(
                        new System.Windows.Forms.DataVisualization.Charting.StripLine()
                        {
                            BorderColor = Color.White,
                            IntervalOffset = chartCon.ChartAreas["ChartArea1"].AxisY.Maximum  -(j * 2  +2 ),
                            Text = chartCon.Series[j].LegendText,
                            TextAlignment = StringAlignment.Near,
                            ForeColor = Color.White,
                            Font = new Font("Microsoft Sans Serif", 8F,FontStyle.Bold ),
                            BorderWidth = 0

                        });
                        
                    }



                    this.tableLayoutPanel1.Controls.Add(chartCon);
                }
            }
        }

        /// <summary>
        /// 폼 종료 시,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            sv.Close();
            sv2.Close();
}

/// <summary>
/// 시간마다 그래프를 다시 그림
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            get_time();

            if (rtime >= mtime)
            {
                if (checkBox1.Checked )
                {
                    get_RealTime();
                }
                DrawChartGraph();
                rtime = 0;
            }
            else
            {
                rtime = rtime + 1;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text =   factory_name  ;
            checkBox1.Checked = bRealCheck;

            // DB 연결 정보 세팅
            sv.SetDBInfo(db_ip, db_name);
            sv.Open();
            sv2.SetDBInfo(db_ip, db_name);
            sv2.Open();


            get_RealTime();

            //타이머 설정 및 시작
            timer1.Interval = 1000;
            timer1.Start();

            // 차트 생성
            AddChartControl();

            // 차트 그리기
            DrawChartGraph();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            rtime = mtime;
            //timer1_Tick(sender,e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtime = mtime;
            iShifts = comboBox1.SelectedIndex+1; 
            //timer1_Tick(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                get_RealTime();
            }

        }

        private void get_RealTime()
        {
            

            if (real_time.Hour >= 8 && real_time.Hour <= 20)
            {
                iShifts = 1;
                dateTimePicker1.Value = real_time;
            }
            else
            {
                iShifts = 2;
                dateTimePicker1.Value = real_time.AddDays(-1);
            }

            comboBox1.SelectedIndex = iShifts - 1;
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



    }
}

