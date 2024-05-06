using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data.Ctrl
{
    public partial class uc_Temperature_LiveData : UserControl
    {
        private static uc_Temperature_LiveData _instance;
        public static uc_Temperature_LiveData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Temperature_LiveData();
                return _instance;
            }
        }

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        DateTime _dt;

        Single _fontNm, _fontNo, _fontMc, _fontCu, _fontLo, _fontHi;
        int _tableW, _tableH, _tableP, _degree;
        int _legendVis_Heating, _legendVis_OutSpec, _legendVis_CurTemp, _legendVis_LowTemp, _legendVis_HigTemp;
        string _legendStr_Heating, _legendStr_OutSpec, _legendStr_CurTemp, _legendStr_LowTemp, _legendStr_HigTemp;

        private System.Windows.Forms.TableLayoutPanel tlp_MC_1;
        private System.Windows.Forms.Label lbl_MC_1_ProdNn;
        private System.Windows.Forms.Label lbl_MC_1_ProdNo;
        private System.Windows.Forms.Label lbl_MC_1_Name;
        private System.Windows.Forms.Label lbl_MC_1_Temp;
        private System.Windows.Forms.Label lbl_MC_1_Temp_Lo;
        private System.Windows.Forms.Label lbl_MC_1_Temp_Hi;

        public uc_Temperature_LiveData()
        {
            InitializeComponent();

            _fontNm = Convert.ToSingle(ini.GetIniValue("FONTSIZE", "NM", "12").Trim());
            _fontNo = Convert.ToSingle(ini.GetIniValue("FONTSIZE", "NO", "12").Trim());
            _fontMc = Convert.ToSingle(ini.GetIniValue("FONTSIZE", "MC", "20").Trim());
            _fontCu = Convert.ToSingle(ini.GetIniValue("FONTSIZE", "CU", "70").Trim());
            _fontLo = Convert.ToSingle(ini.GetIniValue("FONTSIZE", "LO", "20").Trim());
            _fontHi = Convert.ToSingle(ini.GetIniValue("FONTSIZE", "HI", "20").Trim());

            _tableW = Convert.ToInt32(ini.GetIniValue("TABLESIZE", "W", "400").Trim());
            _tableH = Convert.ToInt32(ini.GetIniValue("TABLESIZE", "H", "250").Trim());
            _tableP = Convert.ToInt32(ini.GetIniValue("TABLESIZE", "P", "10").Trim());
            _degree = Convert.ToInt32(ini.GetIniValue("SYMBOL", "DEG", "1").Trim());

            _legendVis_Heating = Convert.ToInt32(ini.GetIniValue("CAPTION_VISIBLE", "HEATING", "1").Trim());
            _legendVis_OutSpec = Convert.ToInt32(ini.GetIniValue("CAPTION_VISIBLE", "OUTSPEC", "1").Trim());
            _legendVis_CurTemp = Convert.ToInt32(ini.GetIniValue("CAPTION_VISIBLE", "CURTEMP", "1").Trim());
            _legendVis_LowTemp = Convert.ToInt32(ini.GetIniValue("CAPTION_VISIBLE", "LOWTEMP", "1").Trim());
            _legendVis_HigTemp = Convert.ToInt32(ini.GetIniValue("CAPTION_VISIBLE", "HIGTEMP", "1").Trim());

            _legendStr_Heating = ini.GetIniValue("CAPTION_STRING", "HEATING", "Temperature Heating").Trim();
            _legendStr_OutSpec = ini.GetIniValue("CAPTION_STRING", "OUTSPEC", "Temperature Our-Spec").Trim();
            _legendStr_CurTemp = ini.GetIniValue("CAPTION_STRING", "CURTEMP", "Temperature Currently").Trim();
            _legendStr_LowTemp = ini.GetIniValue("CAPTION_STRING", "LOWTEMP", "Low Temperature Limit").Trim();
            _legendStr_HigTemp = ini.GetIniValue("CAPTION_STRING", "HIGTEMP", "High Temperature Limit").Trim();
        }

        private void uc_Temperature_LiveData_Load(object sender, EventArgs e)
        {
            lbl_Cap_Heating_Color.Visible = lbl_Cap_Heating_String.Visible = (_legendVis_Heating == 1) ? true : false;
            lbl_Cap_OutSpec_Color.Visible = lbl_Cap_OutSpec_String.Visible = (_legendVis_OutSpec == 1) ? true : false;
            lbl_Cap_CurTemp_Color.Visible = lbl_Cap_CurTemp_String.Visible = (_legendVis_CurTemp == 1) ? true : false;
            lbl_Cap_LowTemp_Color.Visible = lbl_Cap_LowTemp_String.Visible = (_legendVis_LowTemp == 1) ? true : false;
            lbl_Cap_HigTemp_Color.Visible = lbl_Cap_HigTemp_String.Visible = (_legendVis_HigTemp == 1) ? true : false;

            lbl_Cap_Heating_String.Text = _legendStr_Heating;
            lbl_Cap_OutSpec_String.Text = _legendStr_OutSpec;
            lbl_Cap_CurTemp_String.Text = _legendStr_CurTemp;
            lbl_Cap_LowTemp_String.Text = _legendStr_LowTemp;
            lbl_Cap_HigTemp_String.Text = _legendStr_HigTemp;

            for (int i = 1; i <= 16; i++)
            {
                fnc_Create_MC(i, "", "", 0, 0, 0);
            }
        }

        public void fnc_Create_MC(int _mcNo, string _prodNm, string _prodNo,int _cuTemp, int _loTemp, int _hiTemp)
        {
            this.tlp_MC_1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_MC_1_ProdNn = new System.Windows.Forms.Label();
            this.lbl_MC_1_ProdNo = new System.Windows.Forms.Label();
            this.lbl_MC_1_Name = new System.Windows.Forms.Label();
            this.lbl_MC_1_Temp = new System.Windows.Forms.Label();
            this.lbl_MC_1_Temp_Lo = new System.Windows.Forms.Label();
            this.lbl_MC_1_Temp_Hi = new System.Windows.Forms.Label();

            this.tlp_MC_1.SuspendLayout();

            // 
            // tlp_MC_1
            // 
            this.tlp_MC_1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlp_MC_1.ColumnCount = 4;
            this.tlp_MC_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_MC_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_MC_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_MC_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_MC_1.Controls.Add(this.lbl_MC_1_ProdNn, 0, 0);
            this.tlp_MC_1.Controls.Add(this.lbl_MC_1_ProdNo, 0, 1);
            this.tlp_MC_1.Controls.Add(this.lbl_MC_1_Name, 3, 0);
            this.tlp_MC_1.Controls.Add(this.lbl_MC_1_Temp, 0, 2);
            this.tlp_MC_1.Controls.Add(this.lbl_MC_1_Temp_Lo, 0, 3);
            this.tlp_MC_1.Controls.Add(this.lbl_MC_1_Temp_Hi, 2, 3);
            this.tlp_MC_1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlp_MC_1.Location = new System.Drawing.Point(3, 3);
            this.tlp_MC_1.Name = "tlp_MC_" + _mcNo;
            this.tlp_MC_1.RowCount = 5;
            this.tlp_MC_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tlp_MC_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tlp_MC_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlp_MC_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlp_MC_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlp_MC_1.Size = new System.Drawing.Size(_tableW, _tableH);
            this.tlp_MC_1.TabIndex = 0;
            this.tlp_MC_1.Margin = new Padding(_tableP);
            // 
            // lbl_MC_1_ProdNn
            // 
            this.lbl_MC_1_ProdNn.AutoSize = true;
            this.tlp_MC_1.SetColumnSpan(this.lbl_MC_1_ProdNn, 3);
            this.lbl_MC_1_ProdNn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_MC_1.Font = new System.Drawing.Font("Times New Roman", _fontNm, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MC_1_ProdNn.Location = new System.Drawing.Point(1, 1);
            this.lbl_MC_1_ProdNn.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_MC_1_ProdNn.Name = "lbl_MC_"+ _mcNo + "_ProdNn";
            this.lbl_MC_1_ProdNn.Size = new System.Drawing.Size(200, 30);
            this.lbl_MC_1_ProdNn.TabIndex = 0;
            this.lbl_MC_1_ProdNn.Text = "label1";
            this.lbl_MC_1_ProdNn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MC_1_ProdNo
            // 
            this.lbl_MC_1_ProdNo.AutoSize = true;
            this.tlp_MC_1.SetColumnSpan(this.lbl_MC_1_ProdNo, 3);
            this.lbl_MC_1_ProdNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MC_1_Name.Font = new System.Drawing.Font("Times New Roman", _fontNo, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MC_1_ProdNo.Location = new System.Drawing.Point(1, 32);
            this.lbl_MC_1_ProdNo.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_MC_1_ProdNo.Name = "lbl_MC_" + _mcNo + "_ProdNo";
            this.lbl_MC_1_ProdNo.Size = new System.Drawing.Size(200, 30);
            this.lbl_MC_1_ProdNo.TabIndex = 0;
            this.lbl_MC_1_ProdNo.Text = "label1";
            this.lbl_MC_1_ProdNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MC_1_Name
            // 
            this.lbl_MC_1_Name.AutoSize = true;
            this.lbl_MC_1_Name.BackColor = System.Drawing.Color.LightGreen;
            this.lbl_MC_1_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MC_1_Name.Font = new System.Drawing.Font("Times New Roman", _fontMc, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MC_1_Name.Location = new System.Drawing.Point(202, 1);
            this.lbl_MC_1_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_MC_1_Name.Name = "lbl_MC_" + _mcNo + "_Name";
            this.tlp_MC_1.SetRowSpan(this.lbl_MC_1_Name, 2);
            this.lbl_MC_1_Name.Size = new System.Drawing.Size(67, 61);
            this.lbl_MC_1_Name.TabIndex = 0;
            this.lbl_MC_1_Name.Text = "M" + _mcNo;
            this.lbl_MC_1_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MC_1_Temp
            // 
            this.lbl_MC_1_Temp.AutoSize = true;
            this.tlp_MC_1.SetColumnSpan(this.lbl_MC_1_Temp, 4);
            this.lbl_MC_1_Temp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MC_1_Temp.Font = new System.Drawing.Font("Times New Roman", _fontCu, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MC_1_Temp.Location = new System.Drawing.Point(1, 63);
            this.lbl_MC_1_Temp.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_MC_1_Temp.Name = "lbl_MC_" + _mcNo + "_Temp";
            this.lbl_MC_1_Temp.Size = new System.Drawing.Size(268, 140);
            this.lbl_MC_1_Temp.TabIndex = 0;
            this.lbl_MC_1_Temp.Text = (_degree == 1) ? ((_mcNo + _cuTemp) * 12).ToString() + (char)186 : ((_mcNo + _cuTemp) * 12).ToString();
            this.lbl_MC_1_Temp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MC_1_Temp_Lo
            // 
            this.lbl_MC_1_Temp_Lo.AutoSize = true;
            this.tlp_MC_1.SetColumnSpan(this.lbl_MC_1_Temp_Lo, 2);
            this.lbl_MC_1_Temp_Lo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MC_1_Temp_Lo.Font = new System.Drawing.Font("Times New Roman", _fontLo, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MC_1_Temp_Lo.ForeColor = System.Drawing.Color.Firebrick;
            this.lbl_MC_1_Temp_Lo.Location = new System.Drawing.Point(1, 204);
            this.lbl_MC_1_Temp_Lo.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_MC_1_Temp_Lo.Name = "lbl_MC_" + _mcNo + "_Temp_Lo";
            this.tlp_MC_1.SetRowSpan(this.lbl_MC_1_Temp_Lo, 2);
            this.lbl_MC_1_Temp_Lo.Size = new System.Drawing.Size(133, 35);
            this.lbl_MC_1_Temp_Lo.TabIndex = 0;
            this.lbl_MC_1_Temp_Lo.Text = (_degree == 1) ? _loTemp.ToString() + (char)186 : _loTemp.ToString();
            this.lbl_MC_1_Temp_Lo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MC_1_Temp_Hi
            // 
            this.lbl_MC_1_Temp_Hi.AutoSize = true;
            this.tlp_MC_1.SetColumnSpan(this.lbl_MC_1_Temp_Hi, 2);
            this.lbl_MC_1_Temp_Hi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MC_1_Temp_Hi.Font = new System.Drawing.Font("Times New Roman", _fontHi, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MC_1_Temp_Hi.ForeColor = System.Drawing.Color.Green;
            this.lbl_MC_1_Temp_Hi.Location = new System.Drawing.Point(135, 204);
            this.lbl_MC_1_Temp_Hi.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_MC_1_Temp_Hi.Name = "lbl_MC_" + _mcNo + "_Temp_Hi";
            this.tlp_MC_1.SetRowSpan(this.lbl_MC_1_Temp_Hi, 2);
            this.lbl_MC_1_Temp_Hi.Size = new System.Drawing.Size(134, 35);
            this.lbl_MC_1_Temp_Hi.TabIndex = 0;
            this.lbl_MC_1_Temp_Hi.Text = (_degree == 1) ? _hiTemp.ToString() + (char)186 : _hiTemp.ToString();
            this.lbl_MC_1_Temp_Hi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.tlp_MC_1.ResumeLayout(false);
            this.tlp_MC_1.PerformLayout();
            this.flowLayoutPanel1.Controls.Add(this.tlp_MC_1);
        }

        
    }
}
