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
    public partial class frmAutoSwitchTab : Form
    {

        public DateTime _dt;
        //public int btnWidth = 194;
        //public int btnHeight = 130;
        public int btnWidth = 110;
        public int btnHeight = 55;

        public frmAutoSwitchTab()
        {
            InitializeComponent();
        }

        private void frmAutoSwitchTab_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            init();

            fnAutoSwitch();
            fnTab01();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
        }

        public void fnGetdate()
        {
            if (check.IsConnectedToInternet())
            {
                tssDateTime.Text = cls.fnGetDate("SD") + " - " + cls.fnGetDate("CT");
                tssDateTime.ForeColor = Color.Black;
            }
            else
            {
                tssDateTime.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", _dt);
                tssDateTime.ForeColor = Color.Red;
            }
        }

        public void fnAutoSwitch()
        {
            Timer mytimer = new Timer();
            mytimer.Tick += mytimer_Tick;
            mytimer.Interval = 30000;
            mytimer.Start();
        }

        public void mytimer_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectedIndex = 1;
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                tabControl1.SelectedIndex = 2;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                tabControl1.SelectedIndex = 3;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                tabControl1.SelectedIndex = 4;
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                tabControl1.SelectedIndex = 0;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl1.SelectedIndex;
            switch (tab)
            {
                case 0:
                    fnTab01();
                    break;
                case 1:
                    fnTab02();
                    break;
                case 2:
                    fnTab03();
                    break;
                case 3:
                    fnTab04();
                    break;
                case 4:
                    fnTab05();
                    break;
            }
        }

        public void fnTabs(string locIDx,int tab)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
            flowLayoutPanel5.Controls.Clear();

            string sql = "V2o1_BASE_Warehouse_Inventory_InOut_Status_Location_SelItem_Addnew";
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

                    TableLayoutPanel tlp_1 = new TableLayoutPanel();
                    TableLayoutPanel tlp_1_1 = new TableLayoutPanel();
                    Label lbl_1_header_1 = new Label();
                    Label lbl_1_quantity_1 = new Label();
                    Label lbl_1_code_1 = new Label();
                    Label lbl_1_in_1 = new Label();
                    Label lbl_1_out_1 = new Label();

                    tlp_1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
                    //tlp_1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.tlp_background));
                    tlp_1.ColumnCount = 1;
                    tlp_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    tlp_1.Controls.Add(lbl_1_header_1, 0, 0);
                    tlp_1.Controls.Add(lbl_1_quantity_1, 0, 1);
                    tlp_1.Controls.Add(lbl_1_code_1, 0, 2);
                    //tlp_1.Controls.Add(tlp_1_1, 0, 2);
                    tlp_1.Location = new System.Drawing.Point(3, 3);
                    tlp_1.Name = "tlp_1_" + tab + "_" + i;
                    tlp_1.RowCount = 3;
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                    tlp_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    //tlp_1.Size = new System.Drawing.Size(120, 80);
                    tlp_1.Size = new System.Drawing.Size(btnWidth, btnHeight);
                    tlp_1.TabIndex = i + 100;

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
                    lbl_1_header_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_header_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_header_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_header_1.Name = "lbl_1_code_" + tab + "_" + i;
                    //lbl_1_header_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_header_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(25 * 100 / btnHeight));
                    lbl_1_header_1.TabIndex = 1;
                    lbl_1_header_1.Text = btnCode;
                    lbl_1_header_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_code_1.AutoSize = true;
                    lbl_1_code_1.BackColor = System.Drawing.Color.LightCyan;
                    lbl_1_code_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_code_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_code_1.Location = new System.Drawing.Point(0, 0);
                    lbl_1_code_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_code_1.Name = "lbl_1_header_" + tab + "_" + i;
                    //lbl_1_code_1.Size = new System.Drawing.Size(120, 24);
                    lbl_1_code_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(25 * 100 / btnHeight));
                    lbl_1_code_1.TabIndex = 1;
                    lbl_1_code_1.Text = btnHeader;
                    lbl_1_code_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    lbl_1_quantity_1.AutoSize = true;
                    lbl_1_quantity_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    lbl_1_quantity_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_1_quantity_1.Location = new System.Drawing.Point(0, 24);
                    lbl_1_quantity_1.Margin = new System.Windows.Forms.Padding(0);
                    lbl_1_quantity_1.Name = "lbl_1_quantity_" + tab + "_" + i;
                    //lbl_1_quantity_1.Size = new System.Drawing.Size(120, 32);
                    lbl_1_quantity_1.Size = new System.Drawing.Size(btnWidth, Convert.ToInt32(50 * 100 / btnHeight));
                    lbl_1_quantity_1.TabIndex = 1;
                    lbl_1_quantity_1.Text = btnQuantity;
                    lbl_1_quantity_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    //lbl_1_in_1.AutoSize = true;
                    //lbl_1_in_1.BackColor = System.Drawing.Color.LightCyan;
                    //lbl_1_in_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    //lbl_1_in_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_in_1.Location = new System.Drawing.Point(0, 0);
                    //lbl_1_in_1.Margin = new System.Windows.Forms.Padding(0);
                    //lbl_1_in_1.Name = "lbl_1_in_" + tab + "_" + i;
                    ////lbl_1_in_1.Size = new System.Drawing.Size(60, 24);
                    //lbl_1_in_1.Size = new System.Drawing.Size(Convert.ToInt32(50 * 100 / btnWidth), Convert.ToInt32(30 * 100 / btnHeight));
                    //lbl_1_in_1.TabIndex = 1;
                    //lbl_1_in_1.Text = btnIN;
                    //lbl_1_in_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    //lbl_1_out_1.AutoSize = true;
                    //lbl_1_out_1.BackColor = System.Drawing.Color.Linen;
                    //lbl_1_out_1.Dock = System.Windows.Forms.DockStyle.Fill;
                    //lbl_1_out_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //lbl_1_out_1.Location = new System.Drawing.Point(60, 0);
                    //lbl_1_out_1.Margin = new System.Windows.Forms.Padding(0);
                    //lbl_1_out_1.Name = "lbl_1_out_" + tab + "_" + i;
                    ////lbl_1_out_1.Size = new System.Drawing.Size(60, 24);
                    //lbl_1_out_1.Size = new System.Drawing.Size(Convert.ToInt32(50 * 100 / btnWidth), Convert.ToInt32(30 * 100 / btnHeight));
                    //lbl_1_out_1.TabIndex = 1;
                    //lbl_1_out_1.Text = btnOUT;
                    //lbl_1_out_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


                    //Button btn = new Button();
                    ////btn.BackColor = System.Drawing.Color.DodgerBlue;
                    //btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    ////btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //btn.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //btn.ForeColor = System.Drawing.Color.Black;
                    //btn.Name = "btn_" + locIDx + "_" + i;
                    //btn.Size = new System.Drawing.Size(btnWidth, btnHeight);
                    //btn.TabIndex = i + 100;
                    //btnText = btnHeader + "\r\n" + btnQuantity + "\r\n" + btnIN + "            " + btnOUT;
                    //btn.Text = btnText;

                    //btn.BackColor = (btnQuantity == "0") ? Color.DarkGray : Color.DodgerBlue;
                    ////btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.btnBackground_2));
                    
                    //btn.UseVisualStyleBackColor = false;

                    switch (tab)
                    {
                        case 1:
                            //flowLayoutPanel1.Controls.Add(btn);
                            flowLayoutPanel1.Controls.Add(tlp_1);
                            break;
                        case 2:
                            //flowLayoutPanel2.Controls.Add(btn);
                            flowLayoutPanel2.Controls.Add(tlp_1);
                            break;
                        case 3:
                            //flowLayoutPanel3.Controls.Add(btn);
                            flowLayoutPanel3.Controls.Add(tlp_1);
                            break;
                        case 4:
                            //flowLayoutPanel4.Controls.Add(btn);
                            flowLayoutPanel4.Controls.Add(tlp_1);
                            break;
                        case 5:
                            //flowLayoutPanel5.Controls.Add(btn);
                            flowLayoutPanel5.Controls.Add(tlp_1);
                            break;
                    }

                }
            }
        }

        public void fnTab01()
        {
            fnTabs("101", 1);
        }

        public void fnTab02()
        {
            fnTabs("102", 2);
        }

        public void fnTab03()
        {
            fnTabs("103", 3);
        }

        public void fnTab04()
        {
            fnTabs("111", 4);
        }

        public void fnTab05()
        {
            fnTabs("118", 5);
        }

    }
}
