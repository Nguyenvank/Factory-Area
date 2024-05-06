using Barcode_In_Stock;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Inventory_Data
{
	public class frmInventoryInStock : Form
	{
        public decimal _stdPCS = 0, _stdBOX = 0, _stdCAR = 0, _stdPAL = 0;

        public bool _sample01 = false, _sample02 = false, _sample03 = false;
        public string _chkProdIDx = "", _chkSubcodeNo = "";
        public string _chkSubcode01 = "", _chkSubcode02 = "", _chkSubcode03 = "";
        public string _packCode = "", _packLocation = "", _packManufacturedDate = "", _packLOT = "", _packQuantity = "", _packPartname = "", _packPartnumber = "";

        System.Windows.Forms.Timer timerStart = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timerStop = new System.Windows.Forms.Timer();


        public class AutoClosingMessageBox
		{
			private System.Threading.Timer _timeoutTimer;

			private string _caption;

			private const int WM_CLOSE = 16;

			private AutoClosingMessageBox(string text, string caption, int timeout)
			{
				this._caption = caption;
				this._timeoutTimer = new System.Threading.Timer(new TimerCallback(this.OnTimerElapsed), null, timeout, -1);
				using (this._timeoutTimer)
				{
					MessageBox.Show(text, caption);
				}
			}

			public static void Show(string text, string caption, int timeout)
			{
				new frmInventoryInStock.AutoClosingMessageBox(text, caption, timeout);
			}

			private void OnTimerElapsed(object state)
			{
				IntPtr mbWnd = frmInventoryInStock.AutoClosingMessageBox.FindWindow("#32770", this._caption);
				bool flag = mbWnd != IntPtr.Zero;
				if (flag)
				{
					frmInventoryInStock.AutoClosingMessageBox.SendMessage(mbWnd, 16u, IntPtr.Zero, IntPtr.Zero);
				}
				this._timeoutTimer.Dispose();
			}

			[DllImport("user32.dll", SetLastError = true)]
			private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		}

		public string _prodID;

		public string _prodCode;

		public int packingQty;

		public string _packingType;

		public decimal _packingPCS;

		public decimal _packingBOX;

		public decimal _packingCAR;

		public decimal _packingPAL;

		public byte _chkNetwork;

		private IContainer components = null;

		private Label label1;

		private Label label2;

		private TextBox txtPackingID;

		private Label label3;

		private TextBox txtPartname;

		private Label label4;

		private TextBox txtPartnumber;

		private Label label5;

		private TextBox txtPackingQty;

		private Label label6;

		private TextBox txtLOTnumber;

		private Label label7;

		private TextBox txtManufacturedDate;

		private Label label8;

		private TextBox txtLocation;

		private Label label9;

		private Label label10;

		private Label label11;

		private Label label12;

		private DataGridView dataGridView1;

		private System.Windows.Forms.Timer timer1;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel tssStatus;

		private ToolStripStatusLabel tssTimer;

		private Label lblTotalBox;

		private Label lblTotalItems;

		private Label lblStatus;

		private Label lblProductType;

		private TextBox txtProductType;

		private PictureBox pictureBox1;

		private PictureBox pictureBox2;

		private TextBox txtPCS;

		private TextBox txtBOX;

		private TextBox txtCAR;

		private TextBox txtPAL;

		private Label label13;

		private Label label14;

		private Label label16;

		private Label label17;

		private Label lblConnection;

		private Label label15;

		private TextBox txtTotalDay;

		private Label label18;

		private Label label19;

		private TextBox txtTotalNight;

		private Label label20;
        private Button btnSample01;
        private Button btnSample02;
        private Button btnSample03;
        private Panel pnlDispenser;
        private Label label21;
        private TextBox textBox1;

		public frmInventoryInStock()
		{
			this.InitializeComponent();

            this.txtPackingID.Text = "";
			this.txtPartname.Text = "";
			this.txtPartnumber.Text = "";
			this.txtPackingQty.Text = "";
			this.lblProductType.Visible = false;
			this.txtProductType.Visible = false;
			this.label2.Visible = true;
			this.txtPackingID.Visible = true;
			this.bindData();
			this.initTotal();
		}

		private void getProductInfo()
		{
			bool flag = !cls.CheckForInternetConnection();
			if (flag)
			{
				this.lblConnection.Text = "OFFLINE";
				this.lblConnection.BackColor = Color.Red;
				this._chkNetwork = 0;
			}
			else
			{
				this.lblConnection.Text = "ONLINE";
				this.lblConnection.BackColor = Color.FromKnownColor(KnownColor.Highlight);
				this._chkNetwork = 1;
			}
			this.txtLOTnumber.Text = cls.getDateShift("y");
			this.txtManufacturedDate.Text = cls.getProductInfo("yyyyMMdd");
			this.txtLocation.Text = "Production WH";
			this.tssTimer.Text = cls.getProductInfo("dd/MM/yyyy HH:mm:ss");
			this.tssStatus.Text = "INVENTORY IN-STOCK SYSTEM";
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.getProductInfo();
			bool flag = !this.WithErrors();
			if (flag)
			{
				this.txtPartname.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.txtPartname.ReadOnly = true;
				this.txtPartnumber.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.txtPartnumber.ReadOnly = true;
			}
			//this.txtPackingID.Focus();
			this.getTotal();
		}

		public void getTotal()
		{
			string lotDay = cls.getDateShift("y").Substring(0, 9) + "1";
			string lotNight = cls.getDateShift("y").Substring(0, 9) + "2";
			string sql = "select sum(boxquantity) from BASE_BoxesInOutActivities where boxactivity='IN' and boxLOT='[X]' and boxpartno='" + this._prodCode + "' group by boxpartno";
			string qtyDay = cls.getValue(sql.Replace("[X]", lotDay));
			string qtyNight = cls.getValue(sql.Replace("[X]", lotNight));
			this.txtTotalDay.Text = ((qtyDay != "" && qtyDay != null) ? qtyDay : "0");
			this.txtTotalNight.Text = ((qtyNight != "" && qtyNight != null) ? qtyNight : "0");
		}

		private void txtPackingID_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				string boxcode = this.txtPackingID.Text.Trim();
				bool flag2 = boxcode != "";
				if (flag2)
				{
					string comm = boxcode.Substring(0, 3);
					string comm2 = boxcode.Substring(4);
					string a = comm;
					if (!(a == "COD"))
					{
						if (!(a == "TYP"))
						{
							if (!(a == "QTY"))
							{
								if (!(a == "PRO"))
								{
									this.txtPackingID.Text = null;
									frmInventoryInStock.AutoClosingMessageBox.Show("Vui lòng quét mã cần thực hiện", "System Message", 1000);
									this.txtPackingID.Focus();
								}
								else
								{
                                    
									this.insertDB();
								}
							}
							else
							{
								this.txtPackingQty.Text = comm2;
								this.txtPackingID.Text = null;
								this.txtPackingID.Focus();
							}
						}
						else
						{
							this.txtPackingID.Text = null;
							bool flag3 = comm2.Substring(0, 3) == "FMB";
							if (flag3)
							{
								comm2 += "/0";
							}
							int str = comm2.IndexOf("|");
							int str2 = comm2.IndexOf("/");
							string ss = comm2.Substring(0, str).ToString();
							string ss2 = comm2.Substring(str + 1, str2 - (str + 1)).ToString();
							string ss3 = comm2.Substring(str2 + 1).ToString();
							this.txtPartname.Text = ss;
							this.txtPartnumber.Text = ss2;
							string prodID = cls.getValue("select prodId from base_product where barcode='" + ss2 + "'");
							this._prodID = ((prodID != "0" && prodID != null) ? prodID : "0");
							this._prodCode = ss2;
							this.txtPackingQty.Text = "";
							string sqlPacking = string.Concat(new string[]
							{
								"select cast([X] as numeric(10,0)) from base_Product where name='",
								ss,
								"' or barcode='",
								ss2,
								"'"
							});

                            if (ss.ToLower().Contains("dispenser"))
                            {
                                _chkProdIDx = prodID;
                            }

                            decimal pcs = (cls.getValue(sqlPacking.Replace("[X]", "PackingOutStock")) != "") ? Convert.ToDecimal(cls.getValue(sqlPacking.Replace("[X]", "PackingOutStock"))) : decimal.Zero;
							decimal box = (cls.getValue(sqlPacking.Replace("[X]", "PackingBox")) != "") ? Convert.ToDecimal(cls.getValue(sqlPacking.Replace("[X]", "PackingBox"))) : decimal.Zero;
							decimal car = (cls.getValue(sqlPacking.Replace("[X]", "PackingCart")) != "") ? Convert.ToDecimal(cls.getValue(sqlPacking.Replace("[X]", "PackingCart"))) : decimal.Zero;
							decimal pal = (cls.getValue(sqlPacking.Replace("[X]", "PackingPallete")) != "") ? Convert.ToDecimal(cls.getValue(sqlPacking.Replace("[X]", "PackingPallete"))) : decimal.Zero;
							this.txtPCS.Text = pcs.ToString();
							this.txtBOX.Text = box.ToString();
							this.txtCAR.Text = car.ToString();
							this.txtPAL.Text = pal.ToString();
							this._packingPCS = pcs;
							this._packingBOX = box;
							this._packingCAR = car;
							this._packingPAL = pal;

                            _stdPCS = pcs;
                            _stdBOX = box;
                            _stdCAR = car;
                            _stdPAL = pal;

							bool flag4 = pcs > decimal.Zero || box > decimal.Zero || car > decimal.Zero || pal > decimal.Zero;
							if (flag4)
							{
								this.txtPCS.BackColor = ((this.txtPCS.Text.Trim() == "0") ? Color.FromKnownColor(KnownColor.Control) : (this.txtPCS.BackColor = Color.LightGreen));
								this.txtBOX.BackColor = ((this.txtBOX.Text.Trim() == "0") ? Color.FromKnownColor(KnownColor.Control) : (this.txtBOX.BackColor = Color.LightGreen));
								this.txtCAR.BackColor = ((this.txtCAR.Text.Trim() == "0") ? Color.FromKnownColor(KnownColor.Control) : (this.txtCAR.BackColor = Color.LightGreen));
								this.txtPAL.BackColor = ((this.txtPAL.Text.Trim() == "0") ? Color.FromKnownColor(KnownColor.Control) : (this.txtPAL.BackColor = Color.LightGreen));

                                this.txtPCS.ReadOnly = (this.txtPCS.BackColor == Color.LightGreen) ? false : true;
                                this.txtBOX.ReadOnly = (this.txtBOX.BackColor == Color.LightGreen) ? false : true;
                                this.txtCAR.ReadOnly = (this.txtCAR.BackColor == Color.LightGreen) ? false : true;
                                this.txtPAL.ReadOnly = (this.txtPAL.BackColor == Color.LightGreen) ? false : true;
                            }
							else
							{
								this.txtPCS.BackColor = Color.Red;
								this.txtBOX.BackColor = Color.Red;
								this.txtCAR.BackColor = Color.Red;
								this.txtPAL.BackColor = Color.Red;
								MessageBox.Show("Vui lòng báo văn phòng sản xuất cập nhật số lượng\ncho packing của mã sản phẩm này", "System message");
							}
							this.txtPackingID.Focus();
						}
					}
					else
					{
						string a2 = comm2;
						if (!(a2 == "RESTART"))
						{
							this.txtPackingID.Text = null;
							frmInventoryInStock.AutoClosingMessageBox.Show("Vui lòng quét mã cần thực hiện", "System Message", 1000);
							this.txtPackingID.Focus();
						}
						else
						{
							this.txtPackingID.Text = null;
							this.txtPartname.Text = null;
							this.txtPartnumber.Text = null;
							this.txtPackingQty.Text = null;
							this.txtPackingID.Focus();
						}
					}
					this.bindData();
					this.initTotal();
					this.getTotal();
				}
			}
		}

		public void insertDB2()
		{
			string sPartname = this.txtPartname.Text.Trim();
			string sPartnumber = this.txtPartnumber.Text.Trim();
			string sPackingCode = this.txtPackingID.Text.Trim();
			string sPackingType = sPackingCode.Substring(4, 3);
			string a = sPackingType;
			if (!(a == "PCS"))
			{
				if (!(a == "BOX"))
				{
					if (!(a == "CAR"))
					{
						if (!(a == "PAL"))
						{
							decimal sQuantity2 = 0m;
						}
						else
						{
							decimal sQuantity2 = this._packingPAL;
						}
					}
					else
					{
						decimal sQuantity2 = this._packingCAR;
					}
				}
				else
				{
					decimal sQuantity2 = this._packingBOX;
				}
			}
			else
			{
				decimal sQuantity2 = this._packingPCS;
			}
		}

		private void insertDB()
		{
			string sPartname = this.txtPartname.Text.Trim();
			string sPartnumber = this.txtPartnumber.Text.Trim();
			string sPackingCode = this.txtPackingID.Text.Trim();
			string sPackingType = sPackingCode.Substring(4, 3);
			string a = sPackingType;
			decimal sQuantity;
			if (!(a == "PCS"))
			{
				if (!(a == "BOX"))
				{
					if (!(a == "CAR"))
					{
						if (!(a == "PAL"))
						{
							sQuantity = 0m;
						}
						else
						{
							sQuantity = this._packingPAL;
						}
					}
					else
					{
						sQuantity = this._packingCAR;
					}
				}
				else
				{
					sQuantity = this._packingBOX;
				}
			}
			else
			{
				sQuantity = this._packingPCS;
			}

            _packCode = sPackingCode;
            _packLocation = txtLocation.Text.Trim();
            _packManufacturedDate = txtManufacturedDate.Text.Trim();
            _packLOT = txtLOTnumber.Text.Trim();
            _packQuantity = sQuantity.ToString();
            _packPartname = sPartname;
            _packPartnumber = sPartnumber;



            bool flag = this.WithErrors();
			if (flag)
			{
				this.txtPackingID.Text = "";
				this.txtPackingID.Focus();
				frmInventoryInStock.AutoClosingMessageBox.Show("Nhập dữ liệu trước..", "System Message", 1000);
			}
			else
			{
				bool flag2 = sQuantity > decimal.Zero;
				if (flag2)
				{
					string chkBoxType = this.txtPackingID.Text.Substring(0, 3);
					string sql = "SELECT boxcode FROM dbo.BASE_BoxesManagement WHERE (boxcode='" + this.txtPackingID.Text.Trim().ToString() + "')";
					int exist = (chkBoxType != "EXP") ? this.chkDuplicate(sql) : 1;
					this.lblTotalItems.Text = exist.ToString();
					bool flag3 = this.txtPackingID.Text.Trim() != "";
					if (flag3)
					{
                        string ss = txtPartname.Text.Trim();
                        if (ss.ToLower().Contains("dispenser"))
                        {
                            //string subcode01 = _chkSubcode01;
                            //string subcode02 = _chkSubcode02;
                            //string subcode03 = _chkSubcode03;

                            //btnSample01.BackColor = Color.FromKnownColor(KnownColor.Control);
                            //btnSample02.BackColor = Color.FromKnownColor(KnownColor.Control);
                            //btnSample03.BackColor = Color.FromKnownColor(KnownColor.Control);
                            this.Opacity = 50;
                            frmCheckProof frm = new frmCheckProof(_chkProdIDx, _packPartname, _packPartnumber, _packCode, _packLocation, _packManufacturedDate, _packLOT, _packQuantity);
                            frm.ShowDialog();

                        }

                        ////////string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                        ////////SqlConnection con = new SqlConnection(sConn);
                        ////////con.Open();
                        ////////SqlCommand cmd = new SqlCommand();
                        ////////cmd.CommandType = CommandType.StoredProcedure;
                        ////////cmd.CommandText = "BASE_Product_InStock_Addnew";
                        ////////cmd.Parameters.Add("@boxcode", SqlDbType.VarChar).Value = sPackingCode;
                        ////////cmd.Parameters.Add("@boxuse", SqlDbType.Bit).Value = 1;
                        ////////cmd.Parameters.Add("@boxlocate", SqlDbType.VarChar).Value = this.txtLocation.Text.ToString();
                        ////////cmd.Parameters.Add("@boxsublocate", SqlDbType.VarChar).Value = this.txtManufacturedDate.Text.ToString();
                        ////////cmd.Parameters.Add("@packingdate", SqlDbType.DateTime).Value = DateTime.ParseExact(this.txtManufacturedDate.Text, "yyyyMMdd", CultureInfo.InvariantCulture);
                        ////////cmd.Parameters.Add("@boxLOT", SqlDbType.VarChar).Value = this.txtLOTnumber.Text.ToString();
                        ////////cmd.Parameters.Add("@boxquantity", SqlDbType.SmallInt).Value = sQuantity;
                        ////////cmd.Parameters.Add("@boxpartname", SqlDbType.VarChar).Value = sPartname;
                        ////////cmd.Parameters.Add("@boxpartno", SqlDbType.VarChar).Value = sPartnumber;
                        ////////cmd.Connection = con;
                        ////////try
                        ////////{
                        ////////    cmd.ExecuteNonQuery();
                        ////////}
                        ////////catch
                        ////////{
                        ////////}
                        ////////finally
                        ////////{
                        ////////    con.Close();
                        ////////    con.Dispose();
                        ////////}
                        //bool flag4 = exist > 0;
                        //if (flag4)
                        //{
                        //	string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                        //	SqlConnection con = new SqlConnection(sConn);
                        //	con.Open();
                        //	SqlCommand cmd = new SqlCommand();
                        //	cmd.CommandType = CommandType.StoredProcedure;
                        //	cmd.CommandText = "BASE_Product_InStock_Addnew";
                        //	cmd.Parameters.Add("@boxcode", SqlDbType.VarChar).Value = sPackingCode;
                        //	cmd.Parameters.Add("@boxuse", SqlDbType.Bit).Value = 1;
                        //	cmd.Parameters.Add("@boxlocate", SqlDbType.VarChar).Value = this.txtLocation.Text.ToString();
                        //	cmd.Parameters.Add("@boxsublocate", SqlDbType.VarChar).Value = this.txtManufacturedDate.Text.ToString();
                        //	cmd.Parameters.Add("@packingdate", SqlDbType.DateTime).Value = DateTime.ParseExact(this.txtManufacturedDate.Text, "yyyyMMdd", CultureInfo.InvariantCulture);
                        //	cmd.Parameters.Add("@boxLOT", SqlDbType.VarChar).Value = this.txtLOTnumber.Text.ToString();
                        //	cmd.Parameters.Add("@boxquantity", SqlDbType.SmallInt).Value = sQuantity;
                        //	cmd.Parameters.Add("@boxpartname", SqlDbType.VarChar).Value = sPartname;
                        //	cmd.Parameters.Add("@boxpartno", SqlDbType.VarChar).Value = sPartnumber;
                        //	cmd.Connection = con;
                        //	try
                        //	{
                        //		cmd.ExecuteNonQuery();
                        //	}
                        //	catch
                        //	{
                        //	}
                        //	finally
                        //	{
                        //		con.Close();
                        //		con.Dispose();
                        //	}
                        //}
                        //else
                        //{
                        //	this.txtPackingID.Text = "";
                        //	this.txtPackingID.Focus();
                        //	frmInventoryInStock.AutoClosingMessageBox.Show("Mã thùng này không đúng..", "System Message", 1000);
                        //}
                    }
                    else
					{
						this.txtPackingID.Text = "";
						this.txtPackingID.Focus();
						frmInventoryInStock.AutoClosingMessageBox.Show("Vui lòng nhập mã thùng..", "System Message", 1000);
					}
				}
				else
				{
					this.txtPackingID.Text = "";
					this.txtPackingID.Focus();
					MessageBox.Show("Vui lòng báo văn phòng sản xuất cài đặt số lượng\ncho loại packing " + sPackingType + " này..", "System Message");
				}
			}
			this.txtPackingID.Text = "";
		}

        public void timerStart_Tick(object sender, EventArgs e)
        {
            txtPackingID.BackColor = Color.FromKnownColor(KnownColor.Control);
            timerStart.Stop();
        }

        public void fnGetBackValue(bool status, string sPartname, string sPartnumber, string sPackingCode, string sPackingLoc, string sPackingProduce, string sPackingLOT, string sPackingQty)
        {
            //MessageBox.Show(status.ToString());
            return;
            if (status == true)
            {
                string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection con = new SqlConnection(sConn);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BASE_Product_InStock_Addnew";
                cmd.Parameters.Add("@boxcode", SqlDbType.VarChar).Value = sPackingCode;
                cmd.Parameters.Add("@boxuse", SqlDbType.Bit).Value = 1;
                //cmd.Parameters.Add("@boxlocate", SqlDbType.VarChar).Value = this.txtLocation.Text.ToString();
                cmd.Parameters.Add("@boxlocate", SqlDbType.VarChar).Value = sPackingLoc;
                //cmd.Parameters.Add("@boxsublocate", SqlDbType.VarChar).Value = this.txtManufacturedDate.Text.ToString();
                cmd.Parameters.Add("@boxsublocate", SqlDbType.VarChar).Value = sPackingProduce;
                //cmd.Parameters.Add("@packingdate", SqlDbType.DateTime).Value = DateTime.ParseExact(this.txtManufacturedDate.Text, "yyyyMMdd", CultureInfo.InvariantCulture);
                cmd.Parameters.Add("@packingdate", SqlDbType.DateTime).Value = DateTime.ParseExact(sPackingProduce, "yyyyMMdd", CultureInfo.InvariantCulture);
                //cmd.Parameters.Add("@boxLOT", SqlDbType.VarChar).Value = this.txtLOTnumber.Text.ToString();
                cmd.Parameters.Add("@boxLOT", SqlDbType.VarChar).Value = sPackingLOT;
                //cmd.Parameters.Add("@boxquantity", SqlDbType.SmallInt).Value = sQuantity;
                cmd.Parameters.Add("@boxquantity", SqlDbType.SmallInt).Value = sPackingQty;
                cmd.Parameters.Add("@boxpartname", SqlDbType.VarChar).Value = sPartname;
                cmd.Parameters.Add("@boxpartno", SqlDbType.VarChar).Value = sPartnumber;
                cmd.Connection = con;
                try
                {
                    cmd.ExecuteNonQuery();
                    bindData();
                }
                catch
                {
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            //else
            //{
            //    txtPackingID.BackColor = Color.Red;

            //    timerStart.Enabled = true;
            //    timerStart.Interval = 5000;
            //    timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            //    timerStart.Start();
            //}
        }

		private bool WithErrors()
		{
			bool flag = this.txtPartname.Text.Trim() == string.Empty;
			bool result;
			if (flag)
			{
				this.txtPartname.BackColor = Color.Yellow;
				result = true;
			}
			else
			{
				bool flag2 = this.txtPartnumber.Text.Trim() == string.Empty;
				if (flag2)
				{
					this.txtPartnumber.BackColor = Color.Yellow;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public int chkDuplicate(string sql)
		{
			int found = 0;
			string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
			SqlConnection con = new SqlConnection(sConn);
			con.Open();
			SqlCommand _comm = new SqlCommand();
			_comm.CommandText = sql;
			_comm.Connection = con;
			try
			{
				_comm.ExecuteNonQuery();
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(_comm);
				da.Fill(ds, "PackingID");
				found = ds.Tables["PackingID"].Rows.Count;
			}
			catch
			{
				throw;
			}
			finally
			{
				con.Close();
			}
			return found;
		}

		public string chkSum(string sql)
		{
			string found = "";
			string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
			SqlConnection con = new SqlConnection(sConn);
			con.Open();
			SqlCommand _comm = new SqlCommand();
			_comm.CommandText = sql;
			_comm.Connection = con;
			try
			{
				_comm.ExecuteNonQuery();
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(_comm);
				da.Fill(ds, "Sum");
				found = ds.Tables["Sum"].Rows[0][0].ToString();
			}
			catch
			{
				throw;
			}
			finally
			{
				con.Close();
			}
			return found;
		}

		public void initTotal()
		{
			string sqlTotalBox = string.Concat(new string[]
			{
				"select * from BASE_BoxesInOutActivities where (boxuse=1) and (boxLOT='",
				this.txtLOTnumber.Text.Trim().ToString(),
				"') and (boxpartno='",
				this.txtPartnumber.Text.Trim(),
				"') and (boxactivity='IN')"
			});
			string sqlTotalItems = string.Concat(new string[]
			{
				"select sum(boxquantity) as total from BASE_BoxesInOutActivities where (boxuse=1) and (boxLOT='",
				this.txtLOTnumber.Text.Trim().ToString(),
				"') and (boxpartno='",
				this.txtPartnumber.Text.Trim(),
				"') and boxactivity='IN'"
			});
			this.lblTotalBox.Text = this.chkDuplicate(sqlTotalBox).ToString();
			this.lblTotalItems.Text = ((this.chkSum(sqlTotalItems) != "") ? this.chkSum(sqlTotalItems).ToString() : "0");
		}

		private void bindData()
		{
			this.getProductInfo();
			string sql = "SELECT TOP (100) PERCENT boxpartname as [Part Name], boxpartno as [P/N], boxcode as [Packing ID], boxquantity as [Q'ty] ";
			sql += "FROM dbo.BASE_BoxesInOutActivities ";
			sql = sql + "WHERE (boxLOT = '" + this.txtLOTnumber.Text.Trim() + "') ";
			sql = sql + "AND (boxpartno='" + this.txtPartnumber.Text.Trim() + "') ";
            sql = sql + "AND (boxactivity='IN') ";
            sql += "ORDER BY boxId DESC";
			this.dataGridView1.DataSource = cls.bindingSource0;
			cls.GetData(sql, this.dataGridView1, cls.bindingSource0, cls.dataAdapter0);
			this.dataGridView1.RowHeadersVisible = false;
			DataGridViewColumn dataGridView1_column = this.dataGridView1.Columns[0];
			DataGridViewColumn dataGridView1_column2 = this.dataGridView1.Columns[1];
			DataGridViewColumn dataGridView1_column3 = this.dataGridView1.Columns[2];
			DataGridViewColumn dataGridView1_column4 = this.dataGridView1.Columns[3];
			dataGridView1_column.Width = 127;
			dataGridView1_column2.Width = 127;
			dataGridView1_column3.Width = 127;
			dataGridView1_column4.Width = 57;
			this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9f);
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Columns[0].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[2].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[3].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		}

		private void frmInventoryInStock_Load(object sender, EventArgs e)
		{
			this.bindData();
			bool flag = this.txtPartname.Text == null || this.txtPartnumber.Text == null;
			if (flag)
			{
				this.txtPartname.Focus();
			}
			this.txtPCS.ReadOnly = true;
			this.txtBOX.ReadOnly = true;
			this.txtCAR.ReadOnly = true;
			this.txtPAL.ReadOnly = true;
			this.txtTotalDay.Text = "0";
			this.txtTotalNight.Text = "0";
			this.getTotal();
		}

		private void txtPartnumber_Leave(object sender, EventArgs e)
		{
			this.bindData();
			this.initTotal();
		}

		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventoryInStock));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPackingID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPartname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPartnumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPackingQty = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLOTnumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtManufacturedDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalBox = new System.Windows.Forms.Label();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblProductType = new System.Windows.Forms.Label();
            this.txtProductType = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtPCS = new System.Windows.Forms.TextBox();
            this.txtBOX = new System.Windows.Forms.TextBox();
            this.txtCAR = new System.Windows.Forms.TextBox();
            this.txtPAL = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblConnection = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTotalDay = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTotalNight = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSample01 = new System.Windows.Forms.Button();
            this.btnSample02 = new System.Windows.Forms.Button();
            this.btnSample03 = new System.Windows.Forms.Button();
            this.pnlDispenser = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlDispenser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(401, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(652, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "INVENTORY IN-STOCK SYSTEM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(461, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Packing ID:";
            // 
            // txtPackingID
            // 
            this.txtPackingID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPackingID.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackingID.Location = new System.Drawing.Point(632, 129);
            this.txtPackingID.Name = "txtPackingID";
            this.txtPackingID.Size = new System.Drawing.Size(337, 53);
            this.txtPackingID.TabIndex = 1;
            this.txtPackingID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPackingID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPackingID_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Part name:";
            // 
            // txtPartname
            // 
            this.txtPartname.BackColor = System.Drawing.SystemColors.Control;
            this.txtPartname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartname.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartname.Location = new System.Drawing.Point(442, 274);
            this.txtPartname.Name = "txtPartname";
            this.txtPartname.ReadOnly = true;
            this.txtPartname.Size = new System.Drawing.Size(310, 38);
            this.txtPartname.TabIndex = 2;
            this.txtPartname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 342);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "Part number:";
            // 
            // txtPartnumber
            // 
            this.txtPartnumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPartnumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPartnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartnumber.Location = new System.Drawing.Point(442, 338);
            this.txtPartnumber.Name = "txtPartnumber";
            this.txtPartnumber.ReadOnly = true;
            this.txtPartnumber.Size = new System.Drawing.Size(310, 38);
            this.txtPartnumber.TabIndex = 3;
            this.txtPartnumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPartnumber.Leave += new System.EventHandler(this.txtPartnumber_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(237, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 31);
            this.label5.TabIndex = 0;
            this.label5.Text = "Packing Q\'ty:";
            // 
            // txtPackingQty
            // 
            this.txtPackingQty.BackColor = System.Drawing.SystemColors.Control;
            this.txtPackingQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPackingQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPackingQty.Location = new System.Drawing.Point(27, 161);
            this.txtPackingQty.Name = "txtPackingQty";
            this.txtPackingQty.ReadOnly = true;
            this.txtPackingQty.Size = new System.Drawing.Size(310, 38);
            this.txtPackingQty.TabIndex = 4;
            this.txtPackingQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPackingQty.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(237, 471);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 31);
            this.label6.TabIndex = 0;
            this.label6.Text = "LOT number:";
            // 
            // txtLOTnumber
            // 
            this.txtLOTnumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLOTnumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLOTnumber.Location = new System.Drawing.Point(442, 467);
            this.txtLOTnumber.Name = "txtLOTnumber";
            this.txtLOTnumber.ReadOnly = true;
            this.txtLOTnumber.Size = new System.Drawing.Size(310, 38);
            this.txtLOTnumber.TabIndex = 5;
            this.txtLOTnumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(237, 536);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(201, 31);
            this.label7.TabIndex = 0;
            this.label7.Text = "Manufactured:";
            // 
            // txtManufacturedDate
            // 
            this.txtManufacturedDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtManufacturedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManufacturedDate.Location = new System.Drawing.Point(442, 532);
            this.txtManufacturedDate.Name = "txtManufacturedDate";
            this.txtManufacturedDate.ReadOnly = true;
            this.txtManufacturedDate.Size = new System.Drawing.Size(310, 38);
            this.txtManufacturedDate.TabIndex = 6;
            this.txtManufacturedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(237, 601);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 31);
            this.label8.TabIndex = 0;
            this.label8.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(442, 597);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(310, 38);
            this.txtLocation.TabIndex = 7;
            this.txtLocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(364, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 31);
            this.label9.TabIndex = 0;
            this.label9.Text = "Product Info";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(898, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(203, 31);
            this.label10.TabIndex = 0;
            this.label10.Text = "In-Stock Items";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(859, 246);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Total Qty Packing";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1047, 246);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "Total Qty Items";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(823, 308);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(440, 327);
            this.dataGridView1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStatus,
            this.tssTimer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1350, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(1257, 17);
            this.tssStatus.Spring = true;
            this.tssStatus.Text = "INVENTORY IN-STOCK SYSTEM";
            this.tssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tssTimer
            // 
            this.tssTimer.Name = "tssTimer";
            this.tssTimer.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.tssTimer.Size = new System.Drawing.Size(78, 17);
            this.tssTimer.Text = "Timer";
            // 
            // lblTotalBox
            // 
            this.lblTotalBox.AutoSize = true;
            this.lblTotalBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBox.Location = new System.Drawing.Point(888, 267);
            this.lblTotalBox.Name = "lblTotalBox";
            this.lblTotalBox.Size = new System.Drawing.Size(62, 31);
            this.lblTotalBox.TabIndex = 8;
            this.lblTotalBox.Text = "120";
            this.lblTotalBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItems.Location = new System.Drawing.Point(1068, 267);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(62, 31);
            this.lblTotalItems.TabIndex = 8;
            this.lblTotalItems.Text = "120";
            this.lblTotalItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(999, 143);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 31);
            this.lblStatus.TabIndex = 0;
            // 
            // lblProductType
            // 
            this.lblProductType.AutoSize = true;
            this.lblProductType.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductType.Location = new System.Drawing.Point(438, 143);
            this.lblProductType.Name = "lblProductType";
            this.lblProductType.Size = new System.Drawing.Size(188, 31);
            this.lblProductType.TabIndex = 9;
            this.lblProductType.Text = "Product type:";
            // 
            // txtProductType
            // 
            this.txtProductType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProductType.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductType.Location = new System.Drawing.Point(632, 129);
            this.txtProductType.Name = "txtProductType";
            this.txtProductType.Size = new System.Drawing.Size(337, 53);
            this.txtProductType.TabIndex = 10;
            this.txtProductType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProductType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPackingID_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1107, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(235, 267);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(282, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(113, 132);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // txtPCS
            // 
            this.txtPCS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPCS.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPCS.Location = new System.Drawing.Point(442, 412);
            this.txtPCS.Name = "txtPCS";
            this.txtPCS.Size = new System.Drawing.Size(66, 30);
            this.txtPCS.TabIndex = 12;
            this.txtPCS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPCS.TextChanged += new System.EventHandler(this.txtPCS_TextChanged);
            this.txtPCS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPCS_KeyPress);
            // 
            // txtBOX
            // 
            this.txtBOX.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBOX.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBOX.Location = new System.Drawing.Point(523, 412);
            this.txtBOX.Name = "txtBOX";
            this.txtBOX.Size = new System.Drawing.Size(66, 30);
            this.txtBOX.TabIndex = 12;
            this.txtBOX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBOX.TextChanged += new System.EventHandler(this.txtBOX_TextChanged);
            this.txtBOX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBOX_KeyPress);
            // 
            // txtCAR
            // 
            this.txtCAR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCAR.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCAR.Location = new System.Drawing.Point(604, 412);
            this.txtCAR.Name = "txtCAR";
            this.txtCAR.Size = new System.Drawing.Size(66, 30);
            this.txtCAR.TabIndex = 12;
            this.txtCAR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCAR.TextChanged += new System.EventHandler(this.txtCAR_TextChanged);
            this.txtCAR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCAR_KeyPress);
            // 
            // txtPAL
            // 
            this.txtPAL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPAL.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAL.Location = new System.Drawing.Point(685, 412);
            this.txtPAL.Name = "txtPAL";
            this.txtPAL.Size = new System.Drawing.Size(66, 30);
            this.txtPAL.TabIndex = 12;
            this.txtPAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPAL.TextChanged += new System.EventHandler(this.txtPAL_TextChanged);
            this.txtPAL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPAL_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(451, 388);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 17);
            this.label13.TabIndex = 13;
            this.label13.Text = "PIECE";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(537, 388);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 17);
            this.label14.TabIndex = 13;
            this.label14.Text = "BOX";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(614, 388);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 17);
            this.label16.TabIndex = 13;
            this.label16.Text = "CART";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(689, 388);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 17);
            this.label17.TabIndex = 13;
            this.label17.Text = "PALLET";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.BackColor = System.Drawing.Color.Red;
            this.lblConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnection.ForeColor = System.Drawing.SystemColors.Window;
            this.lblConnection.Location = new System.Drawing.Point(999, 143);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(137, 31);
            this.lblConnection.TabIndex = 9;
            this.lblConnection.Text = "OFFLINE";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(597, 674);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 31);
            this.label15.TabIndex = 0;
            this.label15.Text = "Qty in day:";
            // 
            // txtTotalDay
            // 
            this.txtTotalDay.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotalDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDay.Location = new System.Drawing.Point(887, 670);
            this.txtTotalDay.Name = "txtTotalDay";
            this.txtTotalDay.Size = new System.Drawing.Size(120, 38);
            this.txtTotalDay.TabIndex = 7;
            this.txtTotalDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(817, 674);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 31);
            this.label18.TabIndex = 0;
            this.label18.Text = "Day";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(1056, 674);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 31);
            this.label19.TabIndex = 0;
            this.label19.Text = "Night";
            // 
            // txtTotalNight
            // 
            this.txtTotalNight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotalNight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalNight.Location = new System.Drawing.Point(1143, 670);
            this.txtTotalNight.Name = "txtTotalNight";
            this.txtTotalNight.Size = new System.Drawing.Size(120, 38);
            this.txtTotalNight.TabIndex = 7;
            this.txtTotalNight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(799, 278);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 17);
            this.label20.TabIndex = 13;
            this.label20.Text = "PALLET";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label20.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(182, 237);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(155, 38);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.Visible = false;
            // 
            // btnSample01
            // 
            this.btnSample01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSample01.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSample01.Location = new System.Drawing.Point(20, 9);
            this.btnSample01.Name = "btnSample01";
            this.btnSample01.Size = new System.Drawing.Size(38, 31);
            this.btnSample01.TabIndex = 14;
            this.btnSample01.Text = "1";
            this.btnSample01.UseVisualStyleBackColor = true;
            // 
            // btnSample02
            // 
            this.btnSample02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSample02.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSample02.Location = new System.Drawing.Point(81, 9);
            this.btnSample02.Name = "btnSample02";
            this.btnSample02.Size = new System.Drawing.Size(38, 31);
            this.btnSample02.TabIndex = 14;
            this.btnSample02.Text = "2";
            this.btnSample02.UseVisualStyleBackColor = true;
            // 
            // btnSample03
            // 
            this.btnSample03.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSample03.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSample03.Location = new System.Drawing.Point(142, 9);
            this.btnSample03.Name = "btnSample03";
            this.btnSample03.Size = new System.Drawing.Size(38, 31);
            this.btnSample03.TabIndex = 14;
            this.btnSample03.Text = "3";
            this.btnSample03.UseVisualStyleBackColor = true;
            // 
            // pnlDispenser
            // 
            this.pnlDispenser.Controls.Add(this.label21);
            this.pnlDispenser.Controls.Add(this.btnSample01);
            this.pnlDispenser.Controls.Add(this.btnSample02);
            this.pnlDispenser.Controls.Add(this.btnSample03);
            this.pnlDispenser.Location = new System.Drawing.Point(617, 188);
            this.pnlDispenser.Name = "pnlDispenser";
            this.pnlDispenser.Size = new System.Drawing.Size(200, 67);
            this.pnlDispenser.TabIndex = 16;
            this.pnlDispenser.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(52, 49);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 13);
            this.label21.TabIndex = 15;
            this.label21.Text = "CHECK SAMPLES";
            // 
            // frmInventoryInStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.pnlDispenser);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPAL);
            this.Controls.Add(this.txtCAR);
            this.Controls.Add(this.txtBOX);
            this.Controls.Add(this.txtPCS);
            this.Controls.Add(this.txtProductType);
            this.Controls.Add(this.lblConnection);
            this.Controls.Add(this.lblProductType);
            this.Controls.Add(this.lblTotalItems);
            this.Controls.Add(this.lblTotalBox);
            this.Controls.Add(this.txtTotalNight);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtTotalDay);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtManufacturedDate);
            this.Controls.Add(this.txtLOTnumber);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtPackingQty);
            this.Controls.Add(this.txtPartnumber);
            this.Controls.Add(this.txtPartname);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtPackingID);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "frmInventoryInStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INVENTORY IN-STOCK SYSTEM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInventoryInStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlDispenser.ResumeLayout(false);
            this.pnlDispenser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void txtPCS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtBOX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtCAR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPAL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtPCS_TextChanged(object sender, EventArgs e)
        {
            if (txtPCS.ReadOnly == false)
            {
                string val = txtPCS.Text.Trim();
                decimal _val = (val != "" && val != null) ? Convert.ToDecimal(val) : 0;
                txtPCS.Text = (_val > _stdPCS) ? _stdPCS.ToString() : _val.ToString();
            }
        }

        private void txtBOX_TextChanged(object sender, EventArgs e)
        {
            if (txtBOX.ReadOnly == false)
            {
                string val = txtBOX.Text.Trim();
                decimal _val = (val != "" && val != null) ? Convert.ToDecimal(val) : 0;
                txtBOX.Text = (_val > _stdPCS) ? _stdBOX.ToString() : _val.ToString();
            }
        }

        private void txtCAR_TextChanged(object sender, EventArgs e)
        {
            if (txtCAR.ReadOnly == false)
            {
                string val = txtCAR.Text.Trim();
                decimal _val = (val != "" && val != null) ? Convert.ToDecimal(val) : 0;
                txtCAR.Text = (_val > _stdPCS) ? _stdCAR.ToString() : _val.ToString();
            }
        }

        private void txtPAL_TextChanged(object sender, EventArgs e)
        {
            if (txtPAL.ReadOnly == false)
            {
                string val = txtPAL.Text.Trim();
                decimal _val = (val != "" && val != null) ? Convert.ToDecimal(val) : 0;
                txtPAL.Text = (_val > _stdPCS) ? _stdPAL.ToString() : _val.ToString();
            }
        }
    }
}
