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
    public partial class frmWarehouseInventoryLocation_v1o2 : Form
    {
        public DateTime _dt;

        public int tlp_width = 85;
        public int tlp_height = 90;

        cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        public string warehouseIDx = "";
        public string warehouseName = "";
        public int itemPadding = 0;
        public int _warehouseIDx = 0;
        public string[] _locateChar = new string[] { };
        public int _locateTime = 300;       // seconds
        public double _fontSize = 7;

        Timer _timerLocate = new Timer();
        public string _currLetter = "";
        public int _n = 0;
        public int _currLetterIDx = 0, _nextLetterIDx = 0;


        public frmWarehouseInventoryLocation_v1o2()
        {
            InitializeComponent();

            warehouseIDx = ini.GetIniValue("WAREHOUSE", "ID", "102").Trim();
            warehouseName = ini.GetIniValue("WAREHOUSE", "NM", "CKD INVENTORY LOCATION").Trim();
            tlp_width=Convert.ToInt32(ini.GetIniValue("WAREHOUSE", "W", "85").Trim());
            tlp_height=Convert.ToInt32(ini.GetIniValue("WAREHOUSE", "H", "90").Trim());
            itemPadding = Convert.ToInt32(ini.GetIniValue("WAREHOUSE", "PD", "8").Trim());
            _warehouseIDx = Convert.ToInt32(warehouseIDx);
            _locateTime = Convert.ToInt32(ini.GetIniValue("WAREHOUSE", "INTERVAL", "300").Trim());
            _fontSize = Convert.ToDouble(ini.GetIniValue("WAREHOUSE", "FONT", "7").Trim());


            ContextMenu mnuPanel = new ContextMenu();
            mnuPanel.MenuItems.Add(new MenuItem("RESIN Warehouse", fn_Resin_Click));
            mnuPanel.MenuItems.Add(new MenuItem("CKD Warehouse", fn_CKD_Click));
            mnuPanel.MenuItems.Add(new MenuItem("RUBBER Warehouse", fn_Rubber_Click));
            //mnuPanel.MenuItems.Add(new MenuItem("CHEMICAL Warehouse", fn_Chemical_Click));
            mnuPanel.MenuItems.Add("-");
            mnuPanel.MenuItems.Add(new MenuItem("Interval time 10 sec", fn_Interval10_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Interval time 20 sec", fn_Interval20_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Interval time 30 sec", fn_Interval30_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Interval time 40 sec", fn_Interval40_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Interval time 50 sec", fn_Interval50_Click));
            mnuPanel.MenuItems.Add(new MenuItem("Interval time 60 sec", fn_Interval60_Click));
            mnuPanel.MenuItems.Add("-");
            mnuPanel.MenuItems.Add(new MenuItem("Exit application", fn_Close_Click));
            flp_main.ContextMenu = mnuPanel;

            cls.SetDoubleBuffer(flp_main, true);
        }

        private void frmWarehouseInventoryLocation_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnLocateAutoChange();
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetdate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            fnTabs(warehouseIDx, _warehouseIDx);
        }

        public void init()
        {
            fnGetdate();

            chkAutoRun.Checked = true;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;

            lblTitle.Text = warehouseName;
            tssAppName.Text = warehouseName;
            //fnTabs(warehouseIDx, _warehouseIDx);
            //fnTabsRack_Expire(warehouseIDx, _warehouseIDx, _locateChar[0]);
            Fnc_Load_Tabs_Rack();
        }

        public void fnGetdate()
        {
            cls.fnSetDateTime(tssDateTime);
        }

        public void fnLocateChar(string locIDx)
        {
            string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SubLocate_Letter_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@locIDx";
            sParams[0].Value = locIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<string> locateChar = new List<string>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        locateChar.Add(row[0].ToString());
                    }
                    _locateChar = locateChar.ToArray();
                    lblLetter.Text = locateChar[0].ToString();
                    _currLetterIDx = 0;
                }
            }

        }

        public void fnLocateAutoChange()
        {
            fnLocateChar(warehouseIDx);

            _timerLocate.Enabled = true;
            _timerLocate.Interval = _locateTime * 1000;
            _timerLocate.Tick += new System.EventHandler(this.timerLocate_Tick);
            _timerLocate.Start();
        }

        public void timerLocate_Tick(object sender, EventArgs e)
        {
            try
            {
                Fnc_Load_Tabs_Rack();

                //if (_locateChar.Length > 0)
                //{
                //    int nLetter = _locateChar.Length;
                //    //MessageBox.Show(nLetter.ToString());

                //    _currLetterIDx += 1;

                //    if (_currLetterIDx >= nLetter)
                //    {
                //        _currLetterIDx = 0;
                //    }

                //    lblLetter.Text = (_currLetterIDx == 0) ? _locateChar[0] : _locateChar[_currLetterIDx];
                //    fnTabsRack_Expire(warehouseIDx, _warehouseIDx, _locateChar[_currLetterIDx]);
                //}
                //else
                //{
                //    //MessageBox.Show("Nothing to display..");
                //}
            }
            catch
            {
                //tssMsg.Text = "Counting has problem, check again.";
            }
            finally
            {

            }
        }

        public void fnTabs(string locIDx, int tab)
        {
            try
            {
                flp_main.Controls.Clear();

                //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
                //string sql="V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SelItem_Addnew";
                string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SelItem_Addnew_v1o1";


                SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

                sParams[0] = new SqlParameter();
                sParams[0].SqlDbType = SqlDbType.Int;
                sParams[0].ParameterName = "@locIDx";
                sParams[0].Value = locIDx;

                DataSet ds = new DataSet();
                ds = cls.ExecuteDataSet(sql, sParams);
                if (ds.Tables.Count > 0)
                {
                    string btnText = "";
                    string btnHeader = "";
                    string btnQuantity = "";
                    string btnIN = "", btnOUT = "";
                    string btnCode = "";
                    string btnDate = "";
                    string btnExp = "";

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        btnText = "";
                        btnHeader = "";
                        btnQuantity = "";

                        btnHeader = ds.Tables[0].Rows[i][0].ToString().ToUpper();
                        btnQuantity = ds.Tables[0].Rows[i][1].ToString();
                        btnIN = ds.Tables[0].Rows[i][2].ToString();
                        btnOUT = ds.Tables[0].Rows[i][3].ToString();
                        btnCode = ds.Tables[0].Rows[i][4].ToString().ToUpper();
                        btnDate = ds.Tables[0].Rows[i][5].ToString().ToUpper();
                        btnExp = ds.Tables[0].Rows[i][6].ToString().ToUpper();

                        TableLayoutPanel tlp_1 = new TableLayoutPanel();
                        TableLayoutPanel tlp_1_1 = new TableLayoutPanel();
                        Label lbl_1_header_1 = new Label();
                        Label lbl_1_quantity_1 = new Label();
                        Label lbl_1_date_1 = new Label();
                        Label lbl_1_exp_1 = new Label();
                        Label lbl_1_code_1 = new Label();
                        Label lbl_1_in_1 = new Label();
                        Label lbl_1_out_1 = new Label();

                        tlp_1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                        //tlp_1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tlp_background));
                        tlp_1.ColumnCount = 1;
                        tlp_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                        tlp_1.Controls.Add(lbl_1_header_1, 0, 0);
                        tlp_1.Controls.Add(lbl_1_date_1, 0, 1);
                        tlp_1.Controls.Add(lbl_1_exp_1, 0, 2);
                        tlp_1.Controls.Add(lbl_1_quantity_1, 0, 3);
                        tlp_1.Controls.Add(lbl_1_code_1, 0, 4);
                        //tlp_1.Controls.Add(tlp_1_1, 0, 2);
                        tlp_1.Location = new System.Drawing.Point(0, 0);
                        tlp_1.Name = "tlp_1_" + tab + "_" + i;
                        tlp_1.RowCount = 5;
                        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                        //tlp_1.Size = new System.Drawing.Size(120, 80);
                        tlp_1.Size = new System.Drawing.Size(tlp_width, tlp_height);
                        tlp_1.TabIndex = i + 100;
                        tlp_1.Margin = new Padding(itemPadding);

                        //tlp_1_1.ColumnCount = 2;
                        //tlp_1_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        //tlp_1_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                        //tlp_1_1.Controls.Add(lbl_1_in_1, 0, 0);
                        //tlp_1_1.Controls.Add(lbl_1_out_1, 1, 0);
                        //tlp_1_1.Location = new System.Drawing.Point(0, 56);
                        //tlp_1_1.Margin = new System.Windows.Forms.Padding(0);
                        //tlp_1_1.Name = "tlp_1_1_" + tab + "_" + i;
                        //tlp_1_1.RowCount = 1;
                        //tlp_1_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                        //tlp_1_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                        ////tlp_1_1.Size = new System.Drawing.Size(120, 24);
                        //tlp_1_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(30 * 100 / btnHeight));
                        //tlp_1_1.TabIndex = 0;

                        lbl_1_header_1.AutoSize = true;
                        lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
                        lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
                        //lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_header_1.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
                        lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
                        lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
                        //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
                        lbl_1_header_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                        lbl_1_header_1.TabIndex = 1;
                        lbl_1_header_1.Text = btnCode;
                        lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        lbl_1_code_1.AutoSize = true;
                        lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
                        lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
                        //lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_code_1.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
                        lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
                        lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
                        //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
                        lbl_1_code_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                        lbl_1_code_1.TabIndex = 1;
                        lbl_1_code_1.Text = btnHeader;
                        lbl_1_code_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        lbl_1_quantity_1.AutoSize = true;
                        lbl_1_quantity_1.Dock = System.Windows.Forms.DockStyle.Fill;
                        //lbl_1_quantity_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_quantity_1.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_quantity_1.Location = new System.Drawing.Point(0, 24);
                        lbl_1_quantity_1.Margin = new System.Windows.Forms.Padding(0);
                        lbl_1_quantity_1.Name = "lbl_1_quantity_" + tab + "_" + i;
                        //lbl_1_quantity_1.Size = new System.Drawing.Size(120, 32);
                        lbl_1_quantity_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(40 * 100 / tlp_height));
                        lbl_1_quantity_1.TabIndex = 1;
                        lbl_1_quantity_1.Text = btnQuantity;
                        lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        lbl_1_date_1.AutoSize = true;
                        lbl_1_date_1.Dock = System.Windows.Forms.DockStyle.Fill;
                        //lbl_1_date_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_date_1.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_date_1.Location = new System.Drawing.Point(0, 24);
                        lbl_1_date_1.Margin = new System.Windows.Forms.Padding(0);
                        lbl_1_date_1.Name = "lbl_1_date_" + tab + "_" + i;
                        //lbl_1_date_1.Size = new System.Drawing.Size(120, 32);
                        lbl_1_date_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                        lbl_1_date_1.TabIndex = 1;
                        lbl_1_date_1.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(btnDate));
                        lbl_1_date_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        lbl_1_exp_1.AutoSize = true;
                        lbl_1_exp_1.Dock = System.Windows.Forms.DockStyle.Fill;
                        //lbl_1_exp_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_exp_1.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        lbl_1_exp_1.Location = new System.Drawing.Point(0, 24);
                        lbl_1_exp_1.Margin = new System.Windows.Forms.Padding(0);
                        lbl_1_exp_1.Name = "lbl_1_exp_" + tab + "_" + i;
                        //lbl_1_exp_1.Size = new System.Drawing.Size(120, 32);
                        lbl_1_exp_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                        lbl_1_exp_1.TabIndex = 1;
                        lbl_1_exp_1.Text = String.Format("EXP: {0:dd/MM/yyyy}", Convert.ToDateTime(btnDate));
                        lbl_1_exp_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        flp_main.Controls.Add(tlp_1);
                        cls.SetDoubleBuffer(tlp_1, true);
                    }
                }

                /****************************************/
                /****************************************/
                /****************************************/

                //flowLayoutPanel1.Controls.Clear();

                //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
                //SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                //sParams[0] = new SqlParameter();
                //sParams[0].SqlDbType = SqlDbType.Int;
                //sParams[0].ParameterName = "@locIDx";
                //sParams[0].Value = locIDx;

                //DataSet ds = new DataSet();
                //ds = cls.ExecuteDataSet(sql, sParams);
                //if (ds.Tables.Count > 0)
                //{
                //    string btnText = "";
                //    string btnHeader = "";
                //    string btnQuantity = "";
                //    string btnIN = "", btnOUT = "";
                //    string btnCode = "";
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        btnText = "";
                //        btnHeader = "";
                //        btnQuantity = "";

                //        btnHeader = ds.Tables[0].Rows[i][0].ToString().ToUpper();
                //        btnQuantity = ds.Tables[0].Rows[i][1].ToString();
                //        btnIN = ds.Tables[0].Rows[i][2].ToString();
                //        btnOUT = ds.Tables[0].Rows[i][3].ToString();
                //        btnCode = ds.Tables[0].Rows[i][4].ToString().ToUpper();

                //        TableLayoutPanel tlp_1 = new TableLayoutPanel();
                //        TableLayoutPanel tlp_1_1 = new TableLayoutPanel();
                //        Label lbl_1_header_1 = new Label();
                //        Label lbl_1_quantity_1 = new Label();
                //        Label lbl_1_code_1 = new Label();
                //        Label lbl_1_in_1 = new Label();
                //        Label lbl_1_out_1 = new Label();

                //        tlp_1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                //        //tlp_1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tlp_background));
                //        tlp_1.ColumnCount = 1;
                //        tlp_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                //        tlp_1.Controls.Add(lbl_1_header_1, 0, 0);
                //        tlp_1.Controls.Add(lbl_1_quantity_1, 0, 1);
                //        tlp_1.Controls.Add(lbl_1_code_1, 0, 2);
                //        //tlp_1.Controls.Add(tlp_1_1, 0, 2);
                //        tlp_1.Location = new System.Drawing.Point(3, 3);
                //        tlp_1.Name = "tlp_1_" + tab + "_" + i;
                //        tlp_1.RowCount = 3;
                //        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                //        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                //        tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                //        //tlp_1.Size = new System.Drawing.Size(120, 80);
                //        tlp_1.Size = new System.Drawing.Size(btnWidth, btnHeight);
                //        tlp_1.TabIndex = i + 100;
                //        tlp_1.Margin = new Padding(itemPadding);

                //        //tlp_1_1.ColumnCount = 2;
                //        //tlp_1_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                //        //tlp_1_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                //        //tlp_1_1.Controls.Add(lbl_1_in_1, 0, 0);
                //        //tlp_1_1.Controls.Add(lbl_1_out_1, 1, 0);
                //        //tlp_1_1.Location = new System.Drawing.Point(0, 56);
                //        //tlp_1_1.Margin = new System.Windows.Forms.Padding(0);
                //        //tlp_1_1.Name = "tlp_1_1_" + tab + "_" + i;
                //        //tlp_1_1.RowCount = 1;
                //        //tlp_1_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                //        //tlp_1_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                //        ////tlp_1_1.Size = new System.Drawing.Size(120, 24);
                //        //tlp_1_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(30 * 100 / btnHeight));
                //        //tlp_1_1.TabIndex = 0;

                //        lbl_1_header_1.AutoSize = true;
                //        lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
                //        lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
                //        lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //        lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
                //        lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
                //        lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
                //        //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
                //        lbl_1_header_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(25 * 100 / btnHeight));
                //        lbl_1_header_1.TabIndex = 1;
                //        lbl_1_header_1.Text = btnCode;
                //        lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                //        lbl_1_code_1.AutoSize = true;
                //        lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
                //        lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
                //        lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //        lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
                //        lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
                //        lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
                //        //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
                //        lbl_1_code_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(25 * 100 / btnHeight));
                //        lbl_1_code_1.TabIndex = 1;
                //        lbl_1_code_1.Text = btnHeader;
                //        lbl_1_code_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                //        lbl_1_quantity_1.AutoSize = true;
                //        lbl_1_quantity_1.Dock = System.Windows.Forms.DockStyle.Fill;
                //        lbl_1_quantity_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //        lbl_1_quantity_1.Location = new System.Drawing.Point(0, 24);
                //        lbl_1_quantity_1.Margin = new System.Windows.Forms.Padding(0);
                //        lbl_1_quantity_1.Name = "lbl_1_quantity_" + tab + "_" + i;
                //        //lbl_1_quantity_1.Size = new System.Drawing.Size(120, 32);
                //        lbl_1_quantity_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(50 * 100 / btnHeight));
                //        lbl_1_quantity_1.TabIndex = 1;
                //        lbl_1_quantity_1.Text = btnQuantity;
                //        lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                //        flowLayoutPanel1.Controls.Add(tlp_1);
                //    }
                //}
            }
            catch { }
            finally { }
        }

        public void Fnc_Load_Tabs_Rack()
        {
            if (_locateChar.Length > 0)
            {
                int nLetter = _locateChar.Length;
                //MessageBox.Show(nLetter.ToString());

                _currLetterIDx += 1;

                if (_currLetterIDx >= nLetter)
                {
                    _currLetterIDx = 0;
                }

                lblLetter.Text = (_currLetterIDx == 0) ? _locateChar[0] : _locateChar[_currLetterIDx];
            }
            else
            {
                //MessageBox.Show("Nothing to display..");
            }

            switch (_warehouseIDx)
            {
                case 101:
                case 102:
                    fnTabsRack_No_Expire(warehouseIDx, _warehouseIDx, _locateChar[_currLetterIDx]);
                    break;
                case 103:
                    fnTabsRack_Expire(warehouseIDx, _warehouseIDx, _locateChar[_currLetterIDx]);
                    break;
            }
        }

        public void fnTabsRack_No_Expire(string locIDx, int tab, string rackLetter)
        {

            //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
            //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SubLocate_Rack_Letter_SelItem_Addnew";
            string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SubLocate_Rack_Letter_SelItem_Addnew_v1o0";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@locIDx";
            sParams[0].Value = locIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@letter";
            sParams[1].Value = rackLetter;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0)
            {
                string btnText = "";
                string btnHeader = "";
                string btnQuantity = "";
                string btnIN = "", btnOUT = "";
                string btnCode = "";
                string btnDate = "";
                string use = "", safety = "", stock = "";
                decimal _use = 0, _safety = 0, _stock = 0;
                bool _color = false, _green = false, _yellow = false, _red = false, _grey = false;
                DateTime _btnExp = DateTime.Now;

                flp_main.Controls.Clear();
                
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    btnText = "";
                    btnHeader = "";
                    btnQuantity = "";

                    btnHeader = ds.Tables[0].Rows[i][0].ToString().ToUpper();
                    btnQuantity = ds.Tables[0].Rows[i][1].ToString();
                    btnIN = ds.Tables[0].Rows[i][2].ToString();
                    btnOUT = ds.Tables[0].Rows[i][3].ToString();
                    btnCode = ds.Tables[0].Rows[i][4].ToString().ToUpper();
                    btnDate = ds.Tables[0].Rows[i][5].ToString().ToUpper();
                    use = ds.Tables[0].Rows[i][6].ToString().ToUpper();
                    stock = ds.Tables[0].Rows[i][7].ToString().ToUpper();
                    safety = ds.Tables[0].Rows[i][8].ToString().ToUpper();

                    _use = (use != "" && use != null) ? Convert.ToDecimal(use) : 0;
                    _stock = (stock != "0.00" && stock != null) ? Convert.ToDecimal(stock) : 0;
                    _safety = (safety != "0.00" && safety != null) ? Convert.ToDecimal(safety) : 1;
                    _green = (_stock >= _safety) ? true : false;
                    _yellow = ((_stock < _safety) && _stock >= (30 * 100 / _safety)) ? true : false;
                    _red = ((_stock>0) && (_stock < (30 * 100 / _safety))) ? true : false;
                    _grey = (_use > 0) ? false : true;

                    TableLayoutPanel tlp_1 = new TableLayoutPanel();
                    TableLayoutPanel tlp_1_1 = new TableLayoutPanel();
                    Label lbl_1_header_1 = new Label();
                    Label lbl_1_quantity_1 = new Label();
                    Label lbl_1_date_1 = new Label();
                    Label lbl_1_code_1 = new Label();
                    Label lbl_1_in_1 = new Label();
                    Label lbl_1_out_1 = new Label();

                    tlp_1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                    //tlp_1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tlp_background));
                    tlp_1.ColumnCount = 1;
                    tlp_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    tlp_1.Controls.Add(lbl_1_header_1, 0, 0);
                    tlp_1.Controls.Add(lbl_1_date_1, 0, 1);
                    tlp_1.Controls.Add(lbl_1_quantity_1, 0, 2);
                    tlp_1.Controls.Add(lbl_1_code_1, 0, 3);
                    //tlp_1.Controls.Add(tlp_1_1, 0, 2);
                    tlp_1.Location = new System.Drawing.Point(0, 0);
                    tlp_1.Name = "tlp_1_" + tab + "_" + i;
                    tlp_1.RowCount = 4;
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    //tlp_1.Size = new System.Drawing.Size(120, 80);
                    tlp_1.Size = new System.Drawing.Size(tlp_width, tlp_height);
                    tlp_1.TabIndex = i + 100;
                    tlp_1.Margin = new Padding(itemPadding);

                    lbl_1_header_1.AutoSize = true;
                    lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
                    lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_header_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
                    //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_header_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_header_1.TabIndex = 1;
                    lbl_1_header_1.Text = btnCode;
                    lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_code_1.AutoSize = true;
                    lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
                    lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_code_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
                    //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_code_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_code_1.TabIndex = 1;
                    lbl_1_code_1.Text = btnHeader;
                    lbl_1_code_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_quantity_1.AutoSize = true;
                    lbl_1_quantity_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_quantity_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_quantity_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_quantity_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_quantity_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_quantity_1.Name = "lbl_1_quantity_" + tab + "_" + i;
                    //lbl_1_quantity_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_quantity_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(40 * 100 / tlp_height));
                    lbl_1_quantity_1.TabIndex = 1;
                    lbl_1_quantity_1.Text = "QTY: " + btnQuantity;
                    lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_date_1.AutoSize = true;
                    lbl_1_date_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_date_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_date_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_date_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_date_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_date_1.Name = "lbl_1_date_" + tab + "_" + i;
                    //lbl_1_date_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_date_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_date_1.TabIndex = 1;
                    lbl_1_date_1.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(btnDate));
                    lbl_1_date_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    if (lblLetter.Text.ToUpper() != "T")
                    {
                        if (_green == true)
                        {
                            lbl_1_header_1.BackColor = Color.DeepSkyBlue;
                            lbl_1_quantity_1.BackColor = Color.White;
                            lbl_1_date_1.BackColor = Color.White;
                            lbl_1_code_1.BackColor = Color.LightCyan;
                            lbl_1_in_1.BackColor = Color.White;
                            lbl_1_out_1.BackColor = Color.White;
                        }

                        if (_yellow == true)
                        {
                            lbl_1_header_1.BackColor = Color.Gold;
                            lbl_1_quantity_1.BackColor = Color.Yellow;
                            lbl_1_date_1.BackColor = Color.Yellow;
                            lbl_1_code_1.BackColor = Color.Yellow;
                            lbl_1_in_1.BackColor = Color.Yellow;
                            lbl_1_out_1.BackColor = Color.Yellow;
                        }

                        if (_red == true)
                        {
                            lbl_1_header_1.BackColor = Color.Tomato;
                            lbl_1_quantity_1.BackColor = Color.LightSalmon;
                            lbl_1_date_1.BackColor = Color.LightSalmon;
                            lbl_1_code_1.BackColor = Color.LightSalmon;
                            lbl_1_in_1.BackColor = Color.LightSalmon;
                            lbl_1_out_1.BackColor = Color.LightSalmon;
                        }

                        if (_grey == true)
                        {
                            lbl_1_header_1.BackColor = Color.DarkGray;
                            lbl_1_quantity_1.BackColor = Color.Silver;
                            lbl_1_date_1.BackColor = Color.Silver;
                            lbl_1_code_1.BackColor = Color.Silver;
                            lbl_1_in_1.BackColor = Color.Silver;
                            lbl_1_out_1.BackColor = Color.Silver;
                        }
                    }

                    //if ((_stock < _safety) && (lblLetter.Text.ToUpper() != "T"))
                    //{
                    //    lbl_1_header_1.BackColor = Color.Gold;
                    //    lbl_1_quantity_1.BackColor = Color.Yellow;
                    //    lbl_1_date_1.BackColor = Color.Yellow;
                    //    lbl_1_code_1.BackColor = Color.Yellow;
                    //    lbl_1_in_1.BackColor = Color.Yellow;
                    //    lbl_1_out_1.BackColor = Color.Yellow;
                    //}

                    flp_main.Controls.Add(tlp_1);
                    cls.SetDoubleBuffer(tlp_1, true);
                }
            }
        }

        public void fnTabsRack_Expire(string locIDx, int tab, string rackLetter)
        {

            //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
            //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SubLocate_Rack_Letter_SelItem_Addnew";
            string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SubLocate_Rack_Letter_SelItem_Addnew_v1o0";

            SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@locIDx";
            sParams[0].Value = locIDx;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.VarChar;
            sParams[1].ParameterName = "@letter";
            sParams[1].Value = rackLetter;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);
            if (ds.Tables.Count > 0)
            {
                string btnText = "";
                string btnHeader = "";
                string btnQuantity = "";
                string btnIN = "", btnOUT = "";
                string btnCode = "";
                string btnDate = "", btnExp = "", strExp = "";
                string use = "", safety = "", stock = "";
                decimal _use = 0, _safety = 0, _stock = 0;
                bool _color = false, _green = false, _yellow = false, _red = false, _grey = false;
                DateTime _btnExp = DateTime.Now;

                flp_main.Controls.Clear();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    btnText = "";
                    btnHeader = "";
                    btnQuantity = "";

                    btnHeader = ds.Tables[0].Rows[i][0].ToString().ToUpper();
                    btnQuantity = ds.Tables[0].Rows[i][1].ToString();
                    btnIN = ds.Tables[0].Rows[i][2].ToString();
                    btnOUT = ds.Tables[0].Rows[i][3].ToString();
                    btnCode = ds.Tables[0].Rows[i][4].ToString().ToUpper();
                    btnDate = ds.Tables[0].Rows[i][5].ToString().ToUpper();
                    use = ds.Tables[0].Rows[i][6].ToString().ToUpper();
                    stock = ds.Tables[0].Rows[i][7].ToString().ToUpper();
                    safety = ds.Tables[0].Rows[i][8].ToString().ToUpper();
                    btnExp = ds.Tables[0].Rows[i][9].ToString().ToUpper();

                    _use = (use != "" && use != null) ? Convert.ToDecimal(use) : 0;
                    _stock = (stock != "0.00" && stock != null) ? Convert.ToDecimal(stock) : 0;
                    _safety = (safety != "0.00" && safety != null) ? Convert.ToDecimal(safety) : 1;
                    _green = (_stock >= _safety) ? true : false;
                    _yellow = ((_stock < _safety) && _stock >= (30 * 100 / _safety)) ? true : false;
                    _red = ((_stock > 0) && (_stock < (30 * 100 / _safety))) ? true : false;
                    _grey = (_use > 0) ? false : true;
                    _btnExp = (btnExp.Length > 0) ? Convert.ToDateTime(btnExp) : DateTime.Now;
                    strExp = (btnExp.Length > 0) ? String.Format("EXP: {0:dd/MM/yyyy}", _btnExp) : "";

                    TableLayoutPanel 
                        tlp_1 = new TableLayoutPanel(),
                        tlp_1_1 = new TableLayoutPanel();

                    Label 
                        lbl_1_header_1 = new Label(),
                        lbl_1_quantity_1 = new Label(),
                        lbl_1_date_1 = new Label(),
                        lbl_1_exp_1 = new Label(),
                        lbl_1_code_1 = new Label(),
                        lbl_1_in_1 = new Label(),
                        lbl_1_out_1 = new Label();

                    tlp_1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                    //tlp_1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tlp_background));
                    tlp_1.ColumnCount = 1;
                    tlp_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    tlp_1.Controls.Add(lbl_1_header_1, 0, 0);
                    tlp_1.Controls.Add(lbl_1_date_1, 0, 1);
                    tlp_1.Controls.Add(lbl_1_exp_1, 0, 2);
                    tlp_1.Controls.Add(lbl_1_quantity_1, 0, 3);
                    tlp_1.Controls.Add(lbl_1_code_1, 0, 4);
                    //tlp_1.Controls.Add(tlp_1_1, 0, 2);
                    tlp_1.Location = new System.Drawing.Point(0, 0);
                    tlp_1.Name = "tlp_1_" + tab + "_" + i;
                    tlp_1.RowCount = 5;
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    //tlp_1.Size = new System.Drawing.Size(120, 80);
                    tlp_1.Size = new System.Drawing.Size(tlp_width, tlp_height);
                    tlp_1.TabIndex = i + 100;
                    tlp_1.Margin = new Padding(itemPadding);

                    lbl_1_header_1.AutoSize = true;
                    lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
                    lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_header_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
                    //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_header_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_header_1.TabIndex = 1;
                    lbl_1_header_1.Text = btnCode;
                    lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_code_1.AutoSize = true;
                    lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
                    lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_code_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
                    //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_code_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_code_1.TabIndex = 1;
                    lbl_1_code_1.Text = btnHeader;
                    lbl_1_code_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_quantity_1.AutoSize = true;
                    lbl_1_quantity_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_quantity_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_quantity_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_quantity_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_quantity_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_quantity_1.Name = "lbl_1_quantity_" + tab + "_" + i;
                    //lbl_1_quantity_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_quantity_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(40 * 100 / tlp_height));
                    lbl_1_quantity_1.TabIndex = 1;
                    lbl_1_quantity_1.Text = "QTY: " + btnQuantity;
                    lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_date_1.AutoSize = true;
                    lbl_1_date_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_date_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_date_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_date_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_date_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_date_1.Name = "lbl_1_date_" + tab + "_" + i;
                    //lbl_1_date_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_date_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_date_1.TabIndex = 1;
                    lbl_1_date_1.Text = String.Format("WHS: {0:dd/MM/yyyy}", Convert.ToDateTime(btnDate));
                    lbl_1_date_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                    lbl_1_exp_1.AutoSize = true;
                    lbl_1_exp_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_exp_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_exp_1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_exp_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_exp_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_exp_1.Name = "lbl_1_exp_" + tab + "_" + i;
                    //lbl_1_exp_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_exp_1.Size = new System.Drawing.Size(tlp_width, Convert.ToInt32(20 * 100 / tlp_height));
                    lbl_1_exp_1.TabIndex = 1;
                    lbl_1_exp_1.Text = strExp;
                    lbl_1_exp_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                    if (_green == true)
                    {
                        //lbl_1_header_1.BackColor = Color.DeepSkyBlue;
                        //lbl_1_quantity_1.BackColor = Color.White;
                        //lbl_1_date_1.BackColor = Color.White;
                        //lbl_1_exp_1.BackColor = Color.White;
                        //lbl_1_code_1.BackColor = Color.LightCyan;
                        //lbl_1_in_1.BackColor = Color.White;
                        //lbl_1_out_1.BackColor = Color.White;

                        lbl_1_header_1.BackColor =
                        lbl_1_quantity_1.BackColor =
                        lbl_1_date_1.BackColor =
                        lbl_1_exp_1.BackColor =
                        lbl_1_code_1.BackColor =
                        lbl_1_in_1.BackColor =
                        lbl_1_out_1.BackColor = Color.LightGreen;
                    }

                    if (_yellow == true)
                    {
                        //lbl_1_header_1.BackColor = Color.Gold;
                        //lbl_1_quantity_1.BackColor = Color.Yellow;
                        //lbl_1_date_1.BackColor = Color.Yellow;
                        //lbl_1_exp_1.BackColor = Color.Yellow;
                        //lbl_1_code_1.BackColor = Color.Yellow;
                        //lbl_1_in_1.BackColor = Color.Yellow;
                        //lbl_1_out_1.BackColor = Color.Yellow;

                        lbl_1_header_1.BackColor =
                        lbl_1_quantity_1.BackColor =
                        lbl_1_date_1.BackColor =
                        lbl_1_exp_1.BackColor =
                        lbl_1_code_1.BackColor =
                        lbl_1_in_1.BackColor =
                        lbl_1_out_1.BackColor = Color.Gold;
                    }

                    if (_red == true)
                    {
                        //lbl_1_header_1.BackColor = Color.Tomato;
                        //lbl_1_quantity_1.BackColor = Color.LightSalmon;
                        //lbl_1_date_1.BackColor = Color.LightSalmon;
                        //lbl_1_exp_1.BackColor = Color.LightSalmon;
                        //lbl_1_code_1.BackColor = Color.LightSalmon;
                        //lbl_1_in_1.BackColor = Color.LightSalmon;
                        //lbl_1_out_1.BackColor = Color.LightSalmon;

                        lbl_1_header_1.BackColor =
                        lbl_1_quantity_1.BackColor =
                        lbl_1_date_1.BackColor =
                        lbl_1_exp_1.BackColor =
                        lbl_1_code_1.BackColor =
                        lbl_1_in_1.BackColor =
                        lbl_1_out_1.BackColor = Color.LightPink;
                    }

                    if (_grey == true)
                    {
                        //lbl_1_header_1.BackColor = Color.DarkGray;
                        //lbl_1_quantity_1.BackColor = Color.Silver;
                        //lbl_1_date_1.BackColor = Color.Silver;
                        //lbl_1_exp_1.BackColor = Color.Silver;
                        //lbl_1_code_1.BackColor = Color.Silver;
                        //lbl_1_in_1.BackColor = Color.Silver;
                        //lbl_1_out_1.BackColor = Color.Silver;

                        lbl_1_header_1.BackColor =
                        lbl_1_quantity_1.BackColor =
                        lbl_1_date_1.BackColor =
                        lbl_1_exp_1.BackColor =
                        lbl_1_code_1.BackColor =
                        lbl_1_in_1.BackColor =
                        lbl_1_out_1.BackColor = Color.LightGray;
                    }


                    ////if (lblLetter.Text.ToUpper() != "T")
                    ////{
                    ////    if (_green == true)
                    ////    {
                    ////        //lbl_1_header_1.BackColor = Color.DeepSkyBlue;
                    ////        //lbl_1_quantity_1.BackColor = Color.White;
                    ////        //lbl_1_date_1.BackColor = Color.White;
                    ////        //lbl_1_exp_1.BackColor = Color.White;
                    ////        //lbl_1_code_1.BackColor = Color.LightCyan;
                    ////        //lbl_1_in_1.BackColor = Color.White;
                    ////        //lbl_1_out_1.BackColor = Color.White;

                    ////        lbl_1_header_1.BackColor = 
                    ////        lbl_1_quantity_1.BackColor = 
                    ////        lbl_1_date_1.BackColor = 
                    ////        lbl_1_exp_1.BackColor = 
                    ////        lbl_1_code_1.BackColor = 
                    ////        lbl_1_in_1.BackColor = 
                    ////        lbl_1_out_1.BackColor = Color.LightGreen;
                    ////    }

                    ////    if (_yellow == true)
                    ////    {
                    ////        //lbl_1_header_1.BackColor = Color.Gold;
                    ////        //lbl_1_quantity_1.BackColor = Color.Yellow;
                    ////        //lbl_1_date_1.BackColor = Color.Yellow;
                    ////        //lbl_1_exp_1.BackColor = Color.Yellow;
                    ////        //lbl_1_code_1.BackColor = Color.Yellow;
                    ////        //lbl_1_in_1.BackColor = Color.Yellow;
                    ////        //lbl_1_out_1.BackColor = Color.Yellow;

                    ////        lbl_1_header_1.BackColor = 
                    ////        lbl_1_quantity_1.BackColor = 
                    ////        lbl_1_date_1.BackColor = 
                    ////        lbl_1_exp_1.BackColor = 
                    ////        lbl_1_code_1.BackColor = 
                    ////        lbl_1_in_1.BackColor = 
                    ////        lbl_1_out_1.BackColor = Color.Gold;
                    ////    }

                    ////    if (_red == true)
                    ////    {
                    ////        //lbl_1_header_1.BackColor = Color.Tomato;
                    ////        //lbl_1_quantity_1.BackColor = Color.LightSalmon;
                    ////        //lbl_1_date_1.BackColor = Color.LightSalmon;
                    ////        //lbl_1_exp_1.BackColor = Color.LightSalmon;
                    ////        //lbl_1_code_1.BackColor = Color.LightSalmon;
                    ////        //lbl_1_in_1.BackColor = Color.LightSalmon;
                    ////        //lbl_1_out_1.BackColor = Color.LightSalmon;

                    ////        lbl_1_header_1.BackColor = 
                    ////        lbl_1_quantity_1.BackColor = 
                    ////        lbl_1_date_1.BackColor = 
                    ////        lbl_1_exp_1.BackColor = 
                    ////        lbl_1_code_1.BackColor = 
                    ////        lbl_1_in_1.BackColor = 
                    ////        lbl_1_out_1.BackColor = Color.LightPink;
                    ////    }

                    ////    if (_grey == true)
                    ////    {
                    ////        //lbl_1_header_1.BackColor = Color.DarkGray;
                    ////        //lbl_1_quantity_1.BackColor = Color.Silver;
                    ////        //lbl_1_date_1.BackColor = Color.Silver;
                    ////        //lbl_1_exp_1.BackColor = Color.Silver;
                    ////        //lbl_1_code_1.BackColor = Color.Silver;
                    ////        //lbl_1_in_1.BackColor = Color.Silver;
                    ////        //lbl_1_out_1.BackColor = Color.Silver;

                    ////        lbl_1_header_1.BackColor = 
                    ////        lbl_1_quantity_1.BackColor = 
                    ////        lbl_1_date_1.BackColor = 
                    ////        lbl_1_exp_1.BackColor = 
                    ////        lbl_1_code_1.BackColor = 
                    ////        lbl_1_in_1.BackColor = 
                    ////        lbl_1_out_1.BackColor = Color.LightGray;
                    ////    }
                    ////}

                    //if ((_stock < _safety) && (lblLetter.Text.ToUpper() != "T"))
                    //{
                    //    lbl_1_header_1.BackColor = Color.Gold;
                    //    lbl_1_quantity_1.BackColor = Color.Yellow;
                    //    lbl_1_date_1.BackColor = Color.Yellow;
                    //    lbl_1_code_1.BackColor = Color.Yellow;
                    //    lbl_1_in_1.BackColor = Color.Yellow;
                    //    lbl_1_out_1.BackColor = Color.Yellow;
                    //}

                    flp_main.Controls.Add(tlp_1);
                    cls.SetDoubleBuffer(tlp_1, true);
                }
            }
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void fn_Resin_Click(object sender, EventArgs e)
        {
            string whID = "101";
            string whNM = "RESIN INVENTORY LOCATION";

            ini.SetIniValue("WAREHOUSE", "ID", whID);
            ini.SetIniValue("WAREHOUSE", "NM", whNM);

            System.Windows.Forms.Application.Restart();
        }

        private void fn_CKD_Click(object sender, EventArgs e)
        {
            string whID = "102";
            string whNM = "CKD INVENTORY LOCATION";

            ini.SetIniValue("WAREHOUSE", "ID", whID);
            ini.SetIniValue("WAREHOUSE", "NM", whNM);

            System.Windows.Forms.Application.Restart();
        }

        private void fn_Rubber_Click(object sender, EventArgs e)
        {
            string whID = "103";
            string whNM = "RUBBER INVENTORY LOCATION";

            ini.SetIniValue("WAREHOUSE", "ID", whID);
            ini.SetIniValue("WAREHOUSE", "NM", whNM);

            System.Windows.Forms.Application.Restart();
        }

        private void fn_Chemical_Click(object sender, EventArgs e)
        {
            string whID = "111";
            string whNM = "CHEMICAL INVENTORY LOCATION";

            ini.SetIniValue("WAREHOUSE", "ID", whID);
            ini.SetIniValue("WAREHOUSE", "NM", whNM);

            System.Windows.Forms.Application.Restart();
        }

        private void fn_Interval10_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WAREHOUSE", "INTERVAL", "10");
            System.Windows.Forms.Application.Restart();
        }

        private void fn_Interval20_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WAREHOUSE", "INTERVAL", "20");
            System.Windows.Forms.Application.Restart();
        }

        private void fn_Interval30_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WAREHOUSE", "INTERVAL", "30");
            System.Windows.Forms.Application.Restart();
        }

        private void fn_Interval40_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WAREHOUSE", "INTERVAL", "40");
            System.Windows.Forms.Application.Restart();
        }

        private void fn_Interval50_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WAREHOUSE", "INTERVAL", "50");
            System.Windows.Forms.Application.Restart();
        }

        private void fn_Interval60_Click(object sender, EventArgs e)
        {
            ini.SetIniValue("WAREHOUSE", "INTERVAL", "60");
            System.Windows.Forms.Application.Restart();
        }

        private void chkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRun.Checked)
            {
                _timerLocate.Start();

                btnPrev.Enabled = false;
                btnNext.Enabled = false;
            }
            else
            {
                _timerLocate.Stop();

                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                Fnc_Load_Tabs_Rack();

                //if (_locateChar.Length > 0)
                //{
                //    int nLetter = _locateChar.Length;
                //    //MessageBox.Show(nLetter.ToString());

                //    _currLetterIDx -= 1;

                //    if (_currLetterIDx < 0)
                //    {
                //        _currLetterIDx = nLetter;
                //    }

                //    lblLetter.Text = (_currLetterIDx < 0) ? _locateChar[nLetter] : _locateChar[_currLetterIDx];
                //    fnTabsRack_Expire(warehouseIDx, _warehouseIDx, _locateChar[_currLetterIDx]);
                //}
                //else
                //{
                //    MessageBox.Show("Nothing to display..");
                //}
            }
            catch
            {
                //tssMsg.Text = "Counting has problem, check again.";
            }
            finally
            {

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Fnc_Load_Tabs_Rack();

                //if (_locateChar.Length > 0)
                //{
                //    int nLetter = _locateChar.Length;
                //    //MessageBox.Show(nLetter.ToString());

                //    _currLetterIDx += 1;

                //    if (_currLetterIDx >= nLetter)
                //    {
                //        _currLetterIDx = 0;
                //    }

                //    lblLetter.Text = (_currLetterIDx == 0) ? _locateChar[0] : _locateChar[_currLetterIDx];
                //    fnTabsRack_Expire(warehouseIDx, _warehouseIDx, _locateChar[_currLetterIDx]);
                //}
                //else
                //{
                //    MessageBox.Show("Nothing to display..");
                //}
            }
            catch
            {
                //tssMsg.Text = "Counting has problem, check again.";
            }
            finally
            {

            }
        }

        private void fn_Close_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure?", cls.appName(), MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

    }
}
