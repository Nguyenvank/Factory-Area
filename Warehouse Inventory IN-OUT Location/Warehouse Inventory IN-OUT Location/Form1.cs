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
    public partial class Form1 : Form
    {
        public DateTime _dt;

        public int btnWidth = 85;
        public int btnHeight = 70;
        public int btnW01,btnH01,btnW02,btnH02;

        private cls.Ini ini = new cls.Ini(Application.StartupPath + "\\" + Application.ProductName + ".ini");
        public string warehouseIDx01 = "";
        public string warehouseName01 = "";
        public int _warehouseIDx01 = 0;

        public string warehouseIDx02 = "";
        public string warehouseName02 = "";
        public int _warehouseIDx02 = 0;

        public int itemPadding = 0;

        public Form1()
        {
            InitializeComponent();

            warehouseIDx01 = ini.GetIniValue("WAREHOUSE01", "ID", "103").Trim();
            warehouseName01 = ini.GetIniValue("WAREHOUSE01", "NM", "RUBBER INVENTORY LOCATION").Trim();
            btnW01=Convert.ToInt32(ini.GetIniValue("WAREHOUSE01", "W", "85").Trim());
            btnH01=Convert.ToInt32(ini.GetIniValue("WAREHOUSE01", "H", "70").Trim());
            _warehouseIDx01 = Convert.ToInt32(warehouseIDx01);

            warehouseIDx02 = ini.GetIniValue("WAREHOUSE02", "ID", "111").Trim();
            warehouseName02 = ini.GetIniValue("WAREHOUSE02", "NM", "CHEMICAL INVENTORY LOCATION").Trim();
            btnW02=Convert.ToInt32(ini.GetIniValue("WAREHOUSE02", "W", "85").Trim());
            btnH02=Convert.ToInt32(ini.GetIniValue("WAREHOUSE02", "H", "70").Trim());
            _warehouseIDx02 = Convert.ToInt32(warehouseIDx02);

            itemPadding = Convert.ToInt32(ini.GetIniValue("WAREHOUSE", "PD", "8").Trim());
        }

        public void Form1Load(object sender, EventArgs e)
		{
            _dt = DateTime.Now;
            init();
        }

        public void Timer1Tick(object sender, EventArgs e)
		{
            _dt = DateTime.Now;
            fnGetdate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            fnTabs01(warehouseIDx01, _warehouseIDx01);
            fnTabs02(warehouseIDx02, _warehouseIDx02);
        }

        public void init()
		{
            fnGetdate();

            lblTitle01.Text = warehouseName01;
            lblTitle02.Text = warehouseName02;
            //tssAppName.Text = warehouseName;
            fnTabs01(warehouseIDx01, _warehouseIDx01);
            fnTabs02(warehouseIDx02, _warehouseIDx02);
        }

        public void fnGetdate()
		{
            cls.fnSetDateTime(tssDateTime);
        }

        public void fnTabs01(string locIDx, int tab)
        {
            flowLayoutPanel1.Controls.Clear();

            //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
            string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SelItem_Addnew";

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
                    tlp_1.Location = new System.Drawing.Point(3, 3);
                    tlp_1.Name = "tlp_1_" + tab + "_" + i;
                    tlp_1.RowCount = 4;
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    //tlp_1.Size = new System.Drawing.Size(120, 80);
                    tlp_1.Size = new System.Drawing.Size(btnW01, btnH01);
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
                    //tlp_1_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(30 * 100 / btnH01));
                    //tlp_1_1.TabIndex = 0;

                    lbl_1_header_1.AutoSize = true;
                    lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
                    lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
                    //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_header_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(25 * 100 / btnH01));
                    lbl_1_header_1.TabIndex = 1;
                    lbl_1_header_1.Text = btnCode;
                    lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_code_1.AutoSize = true;
                    lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
                    lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
                    //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_code_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(25 * 100 / btnH01));
                    lbl_1_code_1.TabIndex = 1;
                    lbl_1_code_1.Text = btnHeader;
                    lbl_1_code_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_quantity_1.AutoSize = true;
                    lbl_1_quantity_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_quantity_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_quantity_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_quantity_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_quantity_1.Name = "lbl_1_quantity_" + tab + "_" + i;
                    //lbl_1_quantity_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_quantity_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(50 * 100 / btnH01));
                    lbl_1_quantity_1.TabIndex = 1;
                    lbl_1_quantity_1.Text = btnQuantity;
                    lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_date_1.AutoSize = true;
                    lbl_1_date_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_date_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_date_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_date_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_date_1.Name = "lbl_1_date_" + tab + "_" + i;
                    //lbl_1_date_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_date_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(50 * 100 / btnH01));
                    lbl_1_date_1.TabIndex = 1;
                    lbl_1_date_1.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(btnDate));
                    lbl_1_date_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    flowLayoutPanel1.Controls.Add(tlp_1);
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
            //        tlp_1.Size = new System.Drawing.Size(btnW01, btnH01);
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
            //        //tlp_1_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(30 * 100 / btnH01));
            //        //tlp_1_1.TabIndex = 0;

            //        lbl_1_header_1.AutoSize = true;
            //        lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
            //        lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
            //        lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
            //        lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
            //        lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
            //        //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
            //        lbl_1_header_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(25 * 100 / btnH01));
            //        lbl_1_header_1.TabIndex = 1;
            //        lbl_1_header_1.Text = btnCode;
            //        lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //        lbl_1_code_1.AutoSize = true;
            //        lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
            //        lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
            //        lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
            //        lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
            //        lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
            //        //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
            //        lbl_1_code_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(25 * 100 / btnH01));
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
            //        lbl_1_quantity_1.Size = new System.Drawing.Size(btnW01, Convert.ToInt32(50 * 100 / btnH01));
            //        lbl_1_quantity_1.TabIndex = 1;
            //        lbl_1_quantity_1.Text = btnQuantity;
            //        lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //        flowLayoutPanel1.Controls.Add(tlp_1);
            //    }
            //}
        }

        public void fnTabs02(string locIDx, int tab)
        {
            flowLayoutPanel2.Controls.Clear();

            //string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
            string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_Test_SelItem_Addnew";

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

                    TableLayoutPanel tlp_2 = new TableLayoutPanel();
                    TableLayoutPanel tlp_2_2 = new TableLayoutPanel();
                    Label lbl_2_header_2 = new Label();
                    Label lbl_2_quantity_2 = new Label();
                    Label lbl_2_date_2 = new Label();
                    Label lbl_2_code_2 = new Label();
                    Label lbl_2_in_2 = new Label();
                    Label lbl_2_out_2 = new Label();

                    tlp_2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                    //tlp_2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tlp_background));
                    tlp_2.ColumnCount = 1;
                    tlp_2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    tlp_2.Controls.Add(lbl_2_header_2, 0, 0);
                    tlp_2.Controls.Add(lbl_2_date_2, 0, 1);
                    tlp_2.Controls.Add(lbl_2_quantity_2, 0, 2);
                    tlp_2.Controls.Add(lbl_2_code_2, 0, 3);
                    //tlp_2.Controls.Add(tlp_2_2, 0, 2);
                    tlp_2.Location = new System.Drawing.Point(3, 3);
                    tlp_2.Name = "tlp_2_" + tab + "_" + i;
                    tlp_2.RowCount = 4;
                    tlp_2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                    tlp_2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    //tlp_2.Size = new System.Drawing.Size(120, 80);
                    tlp_2.Size = new System.Drawing.Size(btnW02, btnH02);
                    tlp_2.TabIndex = i + 100;
                    tlp_2.Margin = new Padding(itemPadding);

                    //tlp_2_2.ColumnCount = 2;
                    //tlp_2_2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                    //tlp_2_2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
                    //tlp_2_2.Controls.Add(lbl_2_in_2, 0, 0);
                    //tlp_2_2.Controls.Add(lbl_2_out_2, 1, 0);
                    //tlp_2_2.Location = new System.Drawing.Point(0, 56);
                    //tlp_2_2.Margin = new System.Windows.Forms.Padding(0);
                    //tlp_2_2.Name = "tlp_2_2_" + tab + "_" + i;
                    //tlp_2_2.RowCount = 1;
                    //tlp_2_2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    //tlp_2_2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                    ////tlp_2_2.Size = new System.Drawing.Size(120, 24);
                    //tlp_2_2.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(30 * 100 / btnW02));
                    //tlp_2_2.TabIndex = 0;

                    lbl_2_header_2.AutoSize = true;
                    lbl_2_header_2.BackColor = System.Drawing.Color.DeepSkyBlue;
                    lbl_2_header_2.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_2_header_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_2_header_2.Location = new System.Drawing.Point(0, 0);
                    lbl_2_header_2.Margin = new System.Windows.Forms.Padding(0);
                    lbl_2_header_2.Name = "lbl_2_code_" + tab + "_" + i;
                    //lbl_2_header_2.Size = new System.Drawing.Size(120, 24);
                    lbl_2_header_2.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(25 * 100 / btnW02));
                    lbl_2_header_2.TabIndex = 1;
                    lbl_2_header_2.Text = btnCode;
                    lbl_2_header_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_2_code_2.AutoSize = true;
                    lbl_2_code_2.BackColor = System.Drawing.Color.LightCyan;
                    lbl_2_code_2.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_2_code_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_2_code_2.Location = new System.Drawing.Point(0, 0);
                    lbl_2_code_2.Margin = new System.Windows.Forms.Padding(0);
                    lbl_2_code_2.Name = "lbl_2_header_" + tab + "_" + i;
                    //lbl_2_code_2.Size = new System.Drawing.Size(120, 24);
                    lbl_2_code_2.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(25 * 100 / btnW02));
                    lbl_2_code_2.TabIndex = 1;
                    lbl_2_code_2.Text = btnHeader;
                    lbl_2_code_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_2_quantity_2.AutoSize = true;
                    lbl_2_quantity_2.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_2_quantity_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_2_quantity_2.Location = new System.Drawing.Point(0, 24);
                    lbl_2_quantity_2.Margin = new System.Windows.Forms.Padding(0);
                    lbl_2_quantity_2.Name = "lbl_2_quantity_" + tab + "_" + i;
                    //lbl_2_quantity_2.Size = new System.Drawing.Size(120, 32);
                    lbl_2_quantity_2.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(25 * 100 / btnW02));
                    lbl_2_quantity_2.TabIndex = 1;
                    lbl_2_quantity_2.Text = btnQuantity;
                    lbl_2_quantity_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_2_date_2.AutoSize = true;
                    lbl_2_date_2.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_2_date_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_2_date_2.Location = new System.Drawing.Point(0, 24);
                    lbl_2_date_2.Margin = new System.Windows.Forms.Padding(0);
                    lbl_2_date_2.Name = "lbl_2_date_" + tab + "_" + i;
                    //lbl_2_date_2.Size = new System.Drawing.Size(120, 32);
                    lbl_2_date_2.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(25 * 100 / btnW02));
                    lbl_2_date_2.TabIndex = 1;
                    lbl_2_date_2.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(btnDate));
                    lbl_2_date_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    flowLayoutPanel2.Controls.Add(tlp_2);
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
            //        tlp_1.Size = new System.Drawing.Size(btnH02, btnW02);
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
            //        //tlp_1_1.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(30 * 100 / btnW02));
            //        //tlp_1_1.TabIndex = 0;

            //        lbl_1_header_1.AutoSize = true;
            //        lbl_1_header_1.BackColor = System.Drawing.Color.DeepSkyBlue;
            //        lbl_1_header_1.Dock = System.Windows.Forms.DockStyle.Fill;
            //        lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
            //        lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
            //        lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
            //        //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
            //        lbl_1_header_1.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(25 * 100 / btnW02));
            //        lbl_1_header_1.TabIndex = 1;
            //        lbl_1_header_1.Text = btnCode;
            //        lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //        lbl_1_code_1.AutoSize = true;
            //        lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
            //        lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
            //        lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
            //        lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
            //        lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
            //        //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
            //        lbl_1_code_1.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(25 * 100 / btnW02));
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
            //        lbl_1_quantity_1.Size = new System.Drawing.Size(btnH02, Convert.ToInt32(50 * 100 / btnW02));
            //        lbl_1_quantity_1.TabIndex = 1;
            //        lbl_1_quantity_1.Text = btnQuantity;
            //        lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //        flowLayoutPanel1.Controls.Add(tlp_1);
            //    }
            //}
        }

    }
}
