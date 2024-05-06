using Barcode_FMB_Mixing.Properties;
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

namespace Barcode_FMB_Mixing
{
	public class Form1 : Form
	{
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
				new Form1.AutoClosingMessageBox(text, caption, timeout);
			}

			private void OnTimerElapsed(object state)
			{
				IntPtr mbWnd = Form1.AutoClosingMessageBox.FindWindow("#32770", this._caption);
				bool flag = mbWnd != IntPtr.Zero;
				if (flag)
				{
					Form1.AutoClosingMessageBox.SendMessage(mbWnd, 16u, IntPtr.Zero, IntPtr.Zero);
				}
				this._timeoutTimer.Dispose();
			}

			[DllImport("user32.dll", SetLastError = true)]
			private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		}

		private string factcd = "F1";

		private string factnm = "본사";

		private string shiftsno = "1";

		private string shiftsnm = "Night";

		private string workdate = "";

		private string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

		private DateTime sTime1 = default(DateTime);

		private DateTime sTime2 = default(DateTime);

		private BindingSource bindingSource0 = new BindingSource();

		private SqlDataAdapter dataAdapter0 = new SqlDataAdapter();

		private BindingSource bindingSource1 = new BindingSource();

		private SqlDataAdapter dataAdapter1 = new SqlDataAdapter();

		private BindingSource bindingSource2 = new BindingSource();

		private SqlDataAdapter dataAdapter2 = new SqlDataAdapter();

		private BindingSource bindingSource3 = new BindingSource();

		private SqlDataAdapter dataAdapter3 = new SqlDataAdapter();

		private IContainer components = null;

		private PictureBox pictureBox2;

		private Label label1;

		private TextBox txtStatus;

		private System.Windows.Forms.Timer timer1;

		private Label label2;

		private DataGridView dgvInStatusAll;

		private Label label3;

		private TextBox txtInputBox;

		private Label label4;

		private TextBox txtFunction;

		private Label lblWeightUnit;

		private TextBox txtWeight;

		private Label label6;

		private TextBox txtLOTnumber;

		private Label label7;

		private TextBox txtPackingDate;

		private Label label8;

		private TextBox txtMachineID;

		private DataGridView dgvOutStatusAll;

		private Label label9;

		private TextBox txtCartID;

		private Label lblOutDetail;

		private Label lblInDetail;

		private TextBox txtTotalCartOut;

		private Label lblTotalCartOut;

		private Label lblTotalKgOut;

		private TextBox txtTotalKgOut;

		private Label lblTotalCartIn;

		private Label lblTotalKgIn;

		private TextBox txtTotalCartIn;

		private TextBox txtTotalKgIn;

		private Label label5;

		private TextBox txtPartname;

		private TextBox txtPartnumber;

		private TextBox txtPackingdateOut;

		private TextBox txtLotnumberOut;

		private TextBox hdfCurrDate;

		private TextBox textBox1;

		public Form1()
		{
			this.InitializeComponent();
			this.getProductInfo();
			this.txtInputBox.Focus();
			this.chkLabelVisible();
			this.fnDataViewInStock();
			this.fnDataViewOutStock();
			this.fnDataViewTotal();
			base.TopMost = true;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.getProductInfo();
			this.txtInputBox.Focus();
			this.txtWeight.Text = "0";
			this.chkLabelVisible();
			this.fnDataViewInStock();
			this.fnDataViewOutStock();
			this.fnDataViewTotal();
		}

		private void getProductInfo()
		{
			DateTime nNow = DateTime.Now;
			this.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
			this.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			this.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				this.sTime1 = this.sTime1.AddDays(-1.0);
				this.sTime2 = this.sTime2.AddDays(0.0);
				this.shiftsnm = "Night";
				this.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					this.sTime1 = this.sTime1.AddDays(0.0);
					this.sTime2 = this.sTime2.AddDays(1.0);
					this.shiftsnm = "Night";
					this.shiftsno = "2";
				}
				else
				{
					this.shiftsnm = "Day";
					this.shiftsno = "1";
				}
			}
			this.txtStatus.Text = nNow.ToString("dd/MM/yyyy HH:mm:ss");
			this.txtLOTnumber.Text = this.sTime1.ToString("yyyyMMdd") + "-" + this.shiftsno;
			this.txtPackingDate.Text = this.sTime1.ToString("yyyyMMdd");
			this.hdfCurrDate.Text = this.sTime1.ToString("yyyy-MM-dd");
		}

		public int getCount(string sql)
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

		public string getValue(string sql)
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
			}
			finally
			{
				con.Close();
			}
			return found;
		}

		private void GetData(string selectCommand, DataGridView dgvName, BindingSource bindingsource, SqlDataAdapter sqldataadapter)
		{
			try
			{
				string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
				sqldataadapter = new SqlDataAdapter(selectCommand, connectionString);
				SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqldataadapter);
				DataTable table = new DataTable();
				table.Locale = CultureInfo.InvariantCulture;
				sqldataadapter.Fill(table);
				bindingsource.DataSource = table;
				dgvName.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(), "System Notice");
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.getProductInfo();
			this.chkInputBox();
			this.txtInputBox.Focus();
		}

		private void txtInputBox_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == Keys.Return;
			if (flag)
			{
				string inputbox = this.txtInputBox.Text.Trim().ToUpper();
				bool flag2 = inputbox != "";
				if (flag2)
				{
					string comm0 = inputbox;
					string comm = comm0.Substring(0, 3);
					string comm2 = comm0.Substring(4);
					string a = comm;
					if (!(a == "COD"))
					{
						if (!(a == "LOC"))
						{
							if (!(a == "WGH"))
							{
								if (!(a == "MAC"))
								{
									if (!(a == "BOX"))
									{
										if (!(a == "TYP"))
										{
											Form1.AutoClosingMessageBox.Show("Vui lòng quét mã cần thực hiện", "System Message", 1000);
										}
										else
										{
											int seperate = comm2.IndexOf("|");
											this.txtPartname.Text = comm2.Substring(0, seperate);
											this.txtPartnumber.Text = comm2.Substring(seperate + 1);
										}
									}
									else
									{
										this.txtCartID.Text = comm0;
									}
								}
								else
								{
									this.txtMachineID.Text = comm0.Substring(8);
								}
							}
							else
							{
								this.txtWeight.Text = comm2;
							}
						}
						else
						{
							Form1.AutoClosingMessageBox.Show("SET LOCATE", "System Message", 1000);
						}
					}
					else
					{
						string a2 = comm2;
						if (!(a2 == "RESTART"))
						{
							if (!(a2 == "INN"))
							{
								if (a2 == "OUT")
								{
									this.txtFunction.Text = "OUT-STOCK";
									this.fnProcess();
								}
							}
							else
							{
								this.txtFunction.Text = "IN-STOCK";
								this.fnProcess();
							}
						}
						else
						{
							this.txtFunction.Text = null;
							this.txtCartID.Text = null;
							this.txtPartname.Text = null;
							this.txtPartnumber.Text = null;
							this.txtWeight.Text = "0";
							this.txtMachineID.Text = null;
						}
					}
					this.chkInputBox();
					this.chkLabelVisible();
					this.fnProcess();
					this.txtInputBox.Text = null;
					this.txtInputBox.Focus();
					this.fnDataViewInStock();
					this.fnDataViewOutStock();
					this.fnDataViewTotal();
				}
			}
		}

		public void chkInputBox()
		{
			string func = this.txtFunction.Text.Trim();
			string activity = this.txtFunction.Text.Trim();
			string cartID = this.txtCartID.Text.Trim();
			string partname = this.txtPartname.Text.Trim();
			string partnumber = this.txtPartnumber.Text.Trim();
			string weight = this.txtWeight.Text.Trim();
			string machineID = this.txtMachineID.Text.Trim();
			bool flag = cartID != "" && partname != "" && partnumber != "" && weight != "0" && machineID != "";
			if (flag)
			{
				this.txtFunction.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.txtCartID.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.txtPartname.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.txtPartnumber.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.txtWeight.BackColor = Color.FromKnownColor(KnownColor.Control);
				this.lblWeightUnit.BackColor = Color.FromKnownColor(KnownColor.Control);
			}
			else
			{
				this.txtFunction.BackColor = Color.Yellow;
				this.txtCartID.BackColor = Color.Yellow;
				this.txtPartname.BackColor = Color.Yellow;
				this.txtPartnumber.BackColor = Color.Yellow;
				this.txtWeight.BackColor = ((weight != "0") ? Color.FromKnownColor(KnownColor.Control) : Color.Yellow);
				this.lblWeightUnit.BackColor = ((weight != "0") ? Color.FromKnownColor(KnownColor.Control) : Color.Yellow);
			}
			bool flag2 = func != "";
			if (flag2)
			{
				bool flag3 = func.Substring(0, 1) != "I";
				if (flag3)
				{
					this.txtMachineID.BackColor = Color.FromKnownColor(KnownColor.Control);
				}
				else
				{
					this.txtMachineID.BackColor = Color.Yellow;
				}
			}
			else
			{
				this.txtMachineID.BackColor = Color.Yellow;
			}
		}

		public void chkLabelVisible()
		{
			string func = this.txtFunction.Text.Trim();
			this.lblInDetail.Visible = true;
			this.txtTotalCartIn.Visible = true;
			this.lblTotalCartIn.Visible = true;
			this.txtTotalKgIn.Visible = true;
			this.lblTotalKgIn.Visible = true;
			this.lblOutDetail.Visible = true;
			this.txtTotalCartOut.Visible = true;
			this.lblTotalCartOut.Visible = true;
			this.txtTotalKgOut.Visible = true;
			this.lblTotalKgOut.Visible = true;
			this.txtLOTnumber.Visible = true;
			this.txtPackingDate.Visible = true;
			this.txtLotnumberOut.Visible = false;
			this.txtPackingdateOut.Visible = false;
		}

		public void chkDataProcessing()
		{
			string func = this.txtFunction.Text.Trim();
			string cartID = this.txtCartID.Text.Trim();
			string weight = this.txtWeight.Text.Trim();
			string machineID = this.txtMachineID.Text.Trim();
			bool flag = cartID != "";
			if (flag)
			{
				string sqlCheckInOut = "select mixId from Base_BoxesMixingInOutActivities where cartcode='" + cartID + "' and cartuse=0";
				int chkInOutStatus = this.getCount(sqlCheckInOut);
				bool flag2 = chkInOutStatus > 0;
				if (flag2)
				{
					this.txtFunction.Text = "OUT-STOCK";
				}
				else
				{
					this.txtFunction.Text = "IN-STOCK";
				}
			}
		}

		public void fnProcess()
		{
			string func = this.txtFunction.Text.Trim();
			string cartID = this.txtCartID.Text.Trim();
			string partname = this.txtPartname.Text.Trim();
			string partnumber = this.txtPartnumber.Text.Trim();
			string lot = this.txtLOTnumber.Text.Trim();
			string pack = this.txtPackingDate.Text.Trim();
			string weight = this.txtWeight.Text.Trim();
			string machine = this.txtMachineID.Text.Trim();
			bool flag = func != "";
			if (flag)
			{
				string a = func.Substring(0, 1);
				if (!(a == "I"))
				{
					if (!(a == "O"))
					{
						Form1.AutoClosingMessageBox.Show("DEFAULT", "System Message", 1000);
					}
					else
					{
						bool flag2 = cartID != "";
						if (flag2)
						{
							string sqlCartID = "select boxcode from BASE_BoxesManagement where boxcode='" + cartID + "' and boxuse=1";
							int chkCartID = this.getCount(sqlCartID);
							bool flag3 = chkCartID > 0;
							if (flag3)
							{
								string chkCartPartname = this.getValue("select boxpartname from BASE_BoxesManagement where boxcode='" + cartID + "' and boxuse=1");
								string chkCartPartnumber = this.getValue("select boxpartno from BASE_BoxesManagement where boxcode='" + cartID + "' and boxuse=1");
								string chkCartLOTnumber = this.getValue("select boxLOT from BASE_BoxesManagement where boxcode='" + cartID + "' and boxuse=1");
								string chkCartPackingDate = this.getValue("select packingdate from BASE_BoxesManagement where boxcode='" + cartID + "' and boxuse=1");
								string chkCartWeight = this.getValue("select boxquantity from BASE_BoxesManagement where boxcode='" + cartID + "' and boxuse=1");
								this.txtLOTnumber.Visible = false;
								this.txtPackingDate.Visible = false;
								this.txtLotnumberOut.Visible = true;
								this.txtPackingdateOut.Visible = true;
								this.txtPartname.Text = chkCartPartname;
								this.txtPartnumber.Text = chkCartPartnumber;
								this.txtLotnumberOut.Text = chkCartLOTnumber;
								this.txtPackingdateOut.Text = Convert.ToDateTime(chkCartPackingDate).ToString("yyyyMMdd");
								this.txtWeight.Text = chkCartWeight;
							}
							else
							{
								Form1.AutoClosingMessageBox.Show("Không tìm thấy, vui lòng kiểm tra lại mã thùng.", "System Message", 1000);
							}
						}
						bool flag4 = cartID != "" && partname != "" && partnumber != "" && weight != "0" && machine != "";
						if (flag4)
						{
							string msg = "";
							int seperate = machine.IndexOf(" ");
							string mcName = machine.Substring(0, seperate);
							string mcNumber = machine.Substring(seperate + 1);
							string mcDate = this.hdfCurrDate.Text.Trim();
							string chkBoxcodeExist = "select boxcode from BASE_BoxesManagement where boxcode='" + cartID + "'";
							int boxcodeExist = this.getCount(chkBoxcodeExist);
							bool flag5 = boxcodeExist > 0;
							if (flag5)
							{
								string chkBoxcodeValid = chkBoxcodeExist + " and boxuse=1";
								int boxcodeValid = this.getCount(chkBoxcodeValid);
								bool flag6 = boxcodeValid > 0;
								if (flag6)
								{
									string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
									SqlConnection con = new SqlConnection(sConn);
									con.Open();
									SqlCommand cmd = new SqlCommand();
									cmd.CommandType = CommandType.StoredProcedure;
									cmd.CommandText = "BASE_Mixing_Transfer_Addnew";
									cmd.Parameters.Add("boxcode", SqlDbType.VarChar).Value = cartID;
									cmd.Parameters.Add("locate", SqlDbType.NVarChar).Value = mcName;
									cmd.Parameters.Add("sublocate", SqlDbType.NVarChar).Value = mcNumber;
									cmd.Parameters.Add("lotnumber", SqlDbType.VarChar).Value = this.txtLotnumberOut.Text.Trim();
									cmd.Connection = con;
									try
									{
										cmd.ExecuteNonQuery();
										msg = "Xuất kho thành công.";
										this.fnRestart();
									}
									catch (Exception ex)
									{
										MessageBox.Show("Có lỗi: " + ex.ToString());
									}
									finally
									{
										con.Close();
										con.Dispose();
									}
								}
								else
								{
									this.txtInputBox.Focus();
									this.txtCartID.Text = null;
									msg = "Mã thùng này không khả dụng.";
								}
							}
							else
							{
								this.txtInputBox.Focus();
								this.txtCartID.Text = null;
								msg = "Mã thùng này không đúng.";
							}
							Form1.AutoClosingMessageBox.Show(msg, "System Message", 1000);
						}
					}
				}
				else
				{
					this.txtLOTnumber.Visible = true;
					this.txtPackingDate.Visible = true;
					this.txtLotnumberOut.Visible = false;
					this.txtPackingdateOut.Visible = false;
					bool flag7 = cartID != "" && partname != "" && partnumber != "" && weight != "0";
					if (flag7)
					{
						string msg2 = "";
						string chkBoxcodeExist2 = "select boxcode from BASE_BoxesManagement where boxcode='" + cartID + "'";
						int boxcodeExist2 = this.getCount(chkBoxcodeExist2);
						bool flag8 = boxcodeExist2 > 0;
						if (flag8)
						{
							string chkBoxcodeValid2 = chkBoxcodeExist2 + " and boxuse=1";
							int boxcodeValid2 = this.getCount(chkBoxcodeValid2);
							bool flag9 = boxcodeValid2 == 0;
							if (flag9)
							{
								string sConn2 = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
								SqlConnection con2 = new SqlConnection(sConn2);
								con2.Open();
								SqlCommand cmd2 = new SqlCommand();
								cmd2.CommandType = CommandType.StoredProcedure;
								cmd2.CommandText = "BASE_Product_InStock_Addnew";
								cmd2.Parameters.Add("@boxcode", SqlDbType.VarChar).Value = cartID;
								cmd2.Parameters.Add("@boxuse", SqlDbType.Bit).Value = 1;
								cmd2.Parameters.Add("@boxlocate", SqlDbType.VarChar).Value = "PRODUCTION WH";
								cmd2.Parameters.Add("@boxsublocate", SqlDbType.VarChar).Value = pack;
								cmd2.Parameters.Add("@packingdate", SqlDbType.DateTime).Value = DateTime.ParseExact(pack, "yyyyMMdd", CultureInfo.InvariantCulture);
								cmd2.Parameters.Add("@boxLOT", SqlDbType.VarChar).Value = lot;
								cmd2.Parameters.Add("@boxquantity", SqlDbType.SmallInt).Value = weight;
								cmd2.Parameters.Add("@boxpartname", SqlDbType.VarChar).Value = partname;
								cmd2.Parameters.Add("@boxpartno", SqlDbType.VarChar).Value = partnumber;
								cmd2.Connection = con2;
								try
								{
									cmd2.ExecuteNonQuery();
									msg2 = "Nhập kho thành công.";
									this.fnRestart();
								}
								catch (Exception ex2)
								{
									MessageBox.Show("Có lỗi: " + ex2.ToString());
								}
								finally
								{
									con2.Close();
									con2.Dispose();
								}
							}
							else
							{
								this.txtInputBox.Focus();
								this.txtCartID.Text = null;
								msg2 = "Mã thùng này không khả dụng.";
							}
						}
						else
						{
							this.txtInputBox.Focus();
							this.txtCartID.Text = null;
							msg2 = "Mã thùng này không đúng.";
						}
						Form1.AutoClosingMessageBox.Show(msg2, "System Message", 1000);
					}
				}
			}
		}

		public void fnRestart()
		{
			this.txtFunction.Text = null;
			this.txtCartID.Text = null;
			this.txtPartname.Text = null;
			this.txtPartnumber.Text = null;
			this.txtWeight.Text = "0";
			this.txtMachineID.Text = null;
		}

		public void fnDataViewInStock()
		{
			string func = this.txtFunction.Text.Trim();
			string cartID = this.txtCartID.Text.Trim();
			string partname = this.txtPartname.Text.Trim();
			string partnumber = this.txtPartnumber.Text.Trim();
			string lot = this.txtLOTnumber.Text.Trim();
			string pack = this.txtPackingDate.Text.Trim();
			string weight = this.txtWeight.Text.Trim();
			string machine = this.txtMachineID.Text.Trim();
			string sql = "SELECT boxcode as [Cart ID], boxLOT as [LOT], packingdate as [Time in], boxpartno as [P/N], boxquantity as [W (kg)] ";
			sql += "FROM dbo.BASE_BoxesManagement ";
			sql += "WHERE(boxuse = 1) AND (boxcode LIKE 'BOX-069%') ";
			sql += "ORDER BY packingdate asc";
			this.dgvInStatusAll.DataSource = this.bindingSource0;
			this.GetData(sql, this.dgvInStatusAll, this.bindingSource0, this.dataAdapter0);
			int dgvWidth = this.dgvInStatusAll.Width - SystemInformation.VerticalScrollBarWidth;
			DataGridViewColumn dgvInStatusAll_column = this.dgvInStatusAll.Columns[0];
			DataGridViewColumn dgvInStatusAll_column2 = this.dgvInStatusAll.Columns[1];
			DataGridViewColumn dgvInStatusAll_column3 = this.dgvInStatusAll.Columns[2];
			DataGridViewColumn dgvInStatusAll_column4 = this.dgvInStatusAll.Columns[3];
			DataGridViewColumn dgvInStatusAll_column5 = this.dgvInStatusAll.Columns[4];
			dgvInStatusAll_column.Width = 20 * dgvWidth / 100;
			dgvInStatusAll_column2.Width = 20 * dgvWidth / 100;
			dgvInStatusAll_column3.Width = 25 * dgvWidth / 100;
			dgvInStatusAll_column4.Width = 20 * dgvWidth / 100;
			dgvInStatusAll_column5.Width = 15 * dgvWidth / 100;
			this.dgvInStatusAll.DefaultCellStyle.Font = new Font("Tahoma", 9f);
			this.dgvInStatusAll.RowHeadersVisible = false;
			this.dgvInStatusAll.Columns[0].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvInStatusAll.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvInStatusAll.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[2].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvInStatusAll.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[3].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvInStatusAll.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[4].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvInStatusAll.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvInStatusAll.Columns[2].DefaultCellStyle.Format = "dd/MM HH:mm tt";
		}

		public void fnDataViewOutStock()
		{
			string func = this.txtFunction.Text.Trim();
			string cartID = this.txtCartID.Text.Trim();
			string partname = this.txtPartname.Text.Trim();
			string partnumber = this.txtPartnumber.Text.Trim();
			string lot = this.txtLOTnumber.Text.Trim();
			string pack = this.txtPackingDate.Text.Trim();
			string weight = this.txtWeight.Text.Trim();
			string machine = this.txtMachineID.Text.Trim();
			string sql = "SELECT boxcode AS [Cart ID], boxLOT AS [LOT], boxpartname AS [Part name], boxpartno AS [Part no], transferlocate + ' ' + transfersublocate AS [Machine], boxquantity AS [(kg)], packingdate as [In-Stock], transferdate as [Out-Stock], dbo.getTimeSpanFromSeconds(DATEDIFF(second, packingdate, transferdate)) AS [Cooling Time] ";
			sql += "FROM dbo.BASE_BoxesInOutActivities ";
			sql = sql + "WHERE (boxuse = 0) AND (transferdate IS NOT NULL) AND (boxcode LIKE 'BOX-069%') AND (DATEDIFF(day, transferdate, '" + this.hdfCurrDate.Text.Trim() + "') = 0) ";
			sql += "ORDER BY transferdate desc";
			this.dgvOutStatusAll.DataSource = this.bindingSource1;
			this.GetData(sql, this.dgvOutStatusAll, this.bindingSource1, this.dataAdapter1);
			int dgvWidth = this.dgvOutStatusAll.Width;
			DataGridViewColumn dgvOutStatusAll_column = this.dgvOutStatusAll.Columns[0];
			DataGridViewColumn dgvOutStatusAll_column2 = this.dgvOutStatusAll.Columns[1];
			DataGridViewColumn dgvOutStatusAll_column3 = this.dgvOutStatusAll.Columns[2];
			DataGridViewColumn dgvOutStatusAll_column4 = this.dgvOutStatusAll.Columns[3];
			DataGridViewColumn dgvOutStatusAll_column5 = this.dgvOutStatusAll.Columns[4];
			DataGridViewColumn dgvOutStatusAll_column6 = this.dgvOutStatusAll.Columns[5];
			DataGridViewColumn dgvOutStatusAll_column7 = this.dgvOutStatusAll.Columns[6];
			DataGridViewColumn dgvOutStatusAll_column8 = this.dgvOutStatusAll.Columns[7];
			DataGridViewColumn dgvOutStatusAll_column9 = this.dgvOutStatusAll.Columns[8];
			dgvOutStatusAll_column.Width = 15 * dgvWidth / 100;
			dgvOutStatusAll_column2.Width = 10 * dgvWidth / 100;
			dgvOutStatusAll_column3.Width = 10 * dgvWidth / 100;
			dgvOutStatusAll_column4.Width = 10 * dgvWidth / 100;
			dgvOutStatusAll_column5.Width = 10 * dgvWidth / 100;
			dgvOutStatusAll_column6.Width = 5 * dgvWidth / 100;
			dgvOutStatusAll_column7.Width = 14 * dgvWidth / 100;
			dgvOutStatusAll_column8.Width = 14 * dgvWidth / 100;
			dgvOutStatusAll_column9.Width = 12 * dgvWidth / 100;
			this.dgvOutStatusAll.DefaultCellStyle.Font = new Font("Tahoma", 9f);
			this.dgvOutStatusAll.RowHeadersVisible = false;
			this.dgvOutStatusAll.Columns[0].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[2].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[3].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[4].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[5].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[6].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[7].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[8].HeaderCell.Style.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			this.dgvOutStatusAll.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvOutStatusAll.Columns[6].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
			this.dgvOutStatusAll.Columns[7].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
			this.fnDataViewTotal();
		}

		public void fnDataViewTotal()
		{
			string sqlTotalCartIn = "SELECT count(boxcode) FROM BASE_BoxesManagement WHERE (boxuse = 1) AND (boxcode LIKE 'BOX-069%') ";
			string sqlTotalCartInKg = "SELECT SUM(boxquantity) FROM BASE_BoxesManagement WHERE (boxuse = 1) AND (boxcode LIKE 'BOX-069%') ";
			string sqlTotalCartOut = "SELECT count(boxcode) FROM BASE_BoxesInOutActivities WHERE (boxuse = 0) AND (transferdate IS NOT NULL) AND (boxcode LIKE 'BOX-069%') AND (DATEDIFF(day, transferdate, '" + this.hdfCurrDate.Text.Trim() + "') = 0) ";
			string sqlTotalCartOutKg = "SELECT SUM(boxquantity) FROM BASE_BoxesInOutActivities WHERE (boxuse = 0) AND (transferdate IS NOT NULL) AND (boxcode LIKE 'BOX-069%') AND (DATEDIFF(day, transferdate, '" + this.hdfCurrDate.Text.Trim() + "') = 0) ";
			this.txtTotalCartIn.Text = this.getValue(sqlTotalCartIn);
			this.txtTotalKgIn.Text = this.getValue(sqlTotalCartInKg);
			this.txtTotalCartOut.Text = this.getValue(sqlTotalCartOut);
			this.txtTotalKgOut.Text = this.getValue(sqlTotalCartOutKg);
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
			this.components = new Container();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
			this.pictureBox2 = new PictureBox();
			this.label1 = new Label();
			this.txtStatus = new TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.label2 = new Label();
			this.dgvInStatusAll = new DataGridView();
			this.label3 = new Label();
			this.txtInputBox = new TextBox();
			this.label4 = new Label();
			this.txtFunction = new TextBox();
			this.lblWeightUnit = new Label();
			this.txtWeight = new TextBox();
			this.label6 = new Label();
			this.txtLOTnumber = new TextBox();
			this.label7 = new Label();
			this.txtPackingDate = new TextBox();
			this.label8 = new Label();
			this.txtMachineID = new TextBox();
			this.dgvOutStatusAll = new DataGridView();
			this.label9 = new Label();
			this.txtCartID = new TextBox();
			this.lblOutDetail = new Label();
			this.lblInDetail = new Label();
			this.txtTotalCartOut = new TextBox();
			this.lblTotalCartOut = new Label();
			this.lblTotalKgOut = new Label();
			this.txtTotalKgOut = new TextBox();
			this.lblTotalCartIn = new Label();
			this.lblTotalKgIn = new Label();
			this.txtTotalCartIn = new TextBox();
			this.txtTotalKgIn = new TextBox();
			this.label5 = new Label();
			this.txtPartname = new TextBox();
			this.txtPartnumber = new TextBox();
			this.txtPackingdateOut = new TextBox();
			this.txtLotnumberOut = new TextBox();
			this.hdfCurrDate = new TextBox();
			this.textBox1 = new TextBox();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.dgvInStatusAll).BeginInit();
			((ISupportInitialize)this.dgvOutStatusAll).BeginInit();
			base.SuspendLayout();
			this.pictureBox2.Image = Resources.logo3;
			this.pictureBox2.Location = new Point(376, 10);
			this.pictureBox2.Margin = new Padding(2, 3, 2, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new Size(113, 131);
			this.pictureBox2.TabIndex = 21;
			this.pictureBox2.TabStop = false;
			this.label1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 30f, FontStyle.Bold);
			this.label1.Location = new Point(516, 30);
			this.label1.Margin = new Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(514, 46);
			this.label1.TabIndex = 20;
			this.label1.Text = "FMB TRACKING SYSTEM";
			this.txtStatus.BackColor = SystemColors.Control;
			this.txtStatus.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtStatus.Location = new Point(12, 42);
			this.txtStatus.Margin = new Padding(2, 3, 2, 3);
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ReadOnly = true;
			this.txtStatus.Size = new Size(293, 35);
			this.txtStatus.TabIndex = 22;
			this.txtStatus.TextAlign = HorizontalAlignment.Center;
			this.txtStatus.Visible = false;
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(18, 9);
			this.label2.Margin = new Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(280, 26);
			this.label2.TabIndex = 23;
			this.label2.Text = "IN/OUT MIXING STATUS";
			this.label2.Visible = false;
			this.dgvInStatusAll.AllowUserToAddRows = false;
			this.dgvInStatusAll.AllowUserToDeleteRows = false;
			this.dgvInStatusAll.AllowUserToResizeColumns = false;
			this.dgvInStatusAll.AllowUserToResizeRows = false;
			this.dgvInStatusAll.BackgroundColor = Color.White;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = SystemColors.Control;
			dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
			this.dgvInStatusAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.dgvInStatusAll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvInStatusAll.Location = new Point(211, 179);
			this.dgvInStatusAll.Margin = new Padding(2, 3, 2, 3);
			this.dgvInStatusAll.MultiSelect = false;
			this.dgvInStatusAll.Name = "dgvInStatusAll";
			this.dgvInStatusAll.ReadOnly = true;
			this.dgvInStatusAll.RowTemplate.DefaultCellStyle.BackColor = Color.White;
			this.dgvInStatusAll.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
			this.dgvInStatusAll.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.White;
			this.dgvInStatusAll.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
			this.dgvInStatusAll.RowTemplate.ReadOnly = true;
			this.dgvInStatusAll.RowTemplate.Resizable = DataGridViewTriState.False;
			this.dgvInStatusAll.ScrollBars = ScrollBars.Vertical;
			this.dgvInStatusAll.Size = new Size(512, 211);
			this.dgvInStatusAll.TabIndex = 24;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(445, 105);
			this.label3.Margin = new Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new Size(146, 26);
			this.label3.TabIndex = 23;
			this.label3.Text = "INPUT BOX:";
			this.label3.Visible = false;
			this.txtInputBox.CharacterCasing = CharacterCasing.Upper;
			this.txtInputBox.Font = new Font("Microsoft Sans Serif", 25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtInputBox.Location = new Point(578, 95);
			this.txtInputBox.Margin = new Padding(2, 3, 2, 3);
			this.txtInputBox.Name = "txtInputBox";
			this.txtInputBox.Size = new Size(383, 45);
			this.txtInputBox.TabIndex = 1;
			this.txtInputBox.TextAlign = HorizontalAlignment.Center;
			this.txtInputBox.KeyDown += new KeyEventHandler(this.txtInputBox_KeyDown);
			this.label4.AutoSize = true;
			this.label4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label4.Location = new Point(750, 182);
			this.label4.Margin = new Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new Size(84, 20);
			this.label4.TabIndex = 23;
			this.label4.Text = "Function:";
			this.txtFunction.CharacterCasing = CharacterCasing.Upper;
			this.txtFunction.Enabled = false;
			this.txtFunction.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtFunction.Location = new Point(880, 178);
			this.txtFunction.Margin = new Padding(2, 3, 2, 3);
			this.txtFunction.Name = "txtFunction";
			this.txtFunction.ReadOnly = true;
			this.txtFunction.Size = new Size(311, 30);
			this.txtFunction.TabIndex = 1;
			this.txtFunction.TextAlign = HorizontalAlignment.Center;
			this.lblWeightUnit.AutoSize = true;
			this.lblWeightUnit.Font = new Font("Microsoft Sans Serif", 13f, FontStyle.Bold, GraphicsUnit.Pixel, 0);
			this.lblWeightUnit.ForeColor = SystemColors.GrayText;
			this.lblWeightUnit.Location = new Point(1164, 358);
			this.lblWeightUnit.Margin = new Padding(2, 0, 2, 0);
			this.lblWeightUnit.Name = "lblWeightUnit";
			this.lblWeightUnit.Size = new Size(25, 16);
			this.lblWeightUnit.TabIndex = 23;
			this.lblWeightUnit.Text = "kg";
			this.txtWeight.CharacterCasing = CharacterCasing.Upper;
			this.txtWeight.Enabled = false;
			this.txtWeight.Font = new Font("Microsoft Sans Serif", 45f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtWeight.Location = new Point(1045, 301);
			this.txtWeight.Margin = new Padding(2, 3, 2, 3);
			this.txtWeight.Name = "txtWeight";
			this.txtWeight.ReadOnly = true;
			this.txtWeight.Size = new Size(146, 75);
			this.txtWeight.TabIndex = 1;
			this.txtWeight.Text = "300";
			this.txtWeight.TextAlign = HorizontalAlignment.Center;
			this.label6.AutoSize = true;
			this.label6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label6.Location = new Point(750, 308);
			this.label6.Margin = new Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new Size(112, 20);
			this.label6.TabIndex = 23;
			this.label6.Text = "LOT number:";
			this.txtLOTnumber.CharacterCasing = CharacterCasing.Upper;
			this.txtLOTnumber.Enabled = false;
			this.txtLOTnumber.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtLOTnumber.Location = new Point(880, 304);
			this.txtLOTnumber.Margin = new Padding(2, 3, 2, 3);
			this.txtLOTnumber.Name = "txtLOTnumber";
			this.txtLOTnumber.ReadOnly = true;
			this.txtLOTnumber.Size = new Size(149, 30);
			this.txtLOTnumber.TabIndex = 1;
			this.txtLOTnumber.TextAlign = HorizontalAlignment.Center;
			this.label7.AutoSize = true;
			this.label7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label7.Location = new Point(750, 350);
			this.label7.Margin = new Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new Size(121, 20);
			this.label7.TabIndex = 23;
			this.label7.Text = "Packing Date:";
			this.txtPackingDate.CharacterCasing = CharacterCasing.Upper;
			this.txtPackingDate.Enabled = false;
			this.txtPackingDate.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtPackingDate.Location = new Point(880, 346);
			this.txtPackingDate.Margin = new Padding(2, 3, 2, 3);
			this.txtPackingDate.Name = "txtPackingDate";
			this.txtPackingDate.ReadOnly = true;
			this.txtPackingDate.Size = new Size(149, 30);
			this.txtPackingDate.TabIndex = 1;
			this.txtPackingDate.TextAlign = HorizontalAlignment.Center;
			this.label8.AutoSize = true;
			this.label8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label8.Location = new Point(750, 392);
			this.label8.Margin = new Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new Size(105, 20);
			this.label8.TabIndex = 23;
			this.label8.Text = "Machine ID:";
			this.txtMachineID.CharacterCasing = CharacterCasing.Upper;
			this.txtMachineID.Enabled = false;
			this.txtMachineID.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtMachineID.Location = new Point(880, 388);
			this.txtMachineID.Margin = new Padding(2, 3, 2, 3);
			this.txtMachineID.Name = "txtMachineID";
			this.txtMachineID.ReadOnly = true;
			this.txtMachineID.Size = new Size(311, 30);
			this.txtMachineID.TabIndex = 1;
			this.txtMachineID.TextAlign = HorizontalAlignment.Center;
			this.dgvOutStatusAll.AllowUserToAddRows = false;
			this.dgvOutStatusAll.AllowUserToDeleteRows = false;
			this.dgvOutStatusAll.AllowUserToResizeColumns = false;
			this.dgvOutStatusAll.AllowUserToResizeRows = false;
			this.dgvOutStatusAll.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
			this.dgvOutStatusAll.BackgroundColor = Color.White;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = SystemColors.Control;
			dataGridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
			this.dgvOutStatusAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvOutStatusAll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvOutStatusAll.Location = new Point(211, 429);
			this.dgvOutStatusAll.Margin = new Padding(2, 3, 2, 3);
			this.dgvOutStatusAll.MultiSelect = false;
			this.dgvOutStatusAll.Name = "dgvOutStatusAll";
			this.dgvOutStatusAll.ReadOnly = true;
			this.dgvOutStatusAll.RowTemplate.DefaultCellStyle.BackColor = Color.White;
			this.dgvOutStatusAll.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
			this.dgvOutStatusAll.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.White;
			this.dgvOutStatusAll.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
			this.dgvOutStatusAll.RowTemplate.ReadOnly = true;
			this.dgvOutStatusAll.RowTemplate.Resizable = DataGridViewTriState.False;
			this.dgvOutStatusAll.ScrollBars = ScrollBars.Vertical;
			this.dgvOutStatusAll.Size = new Size(980, 263);
			this.dgvOutStatusAll.TabIndex = 24;
			this.label9.AutoSize = true;
			this.label9.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label9.Location = new Point(750, 224);
			this.label9.Margin = new Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new Size(72, 20);
			this.label9.TabIndex = 23;
			this.label9.Text = "Cart ID:";
			this.txtCartID.CharacterCasing = CharacterCasing.Upper;
			this.txtCartID.Enabled = false;
			this.txtCartID.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtCartID.Location = new Point(880, 220);
			this.txtCartID.Margin = new Padding(2, 3, 2, 3);
			this.txtCartID.Name = "txtCartID";
			this.txtCartID.ReadOnly = true;
			this.txtCartID.Size = new Size(311, 30);
			this.txtCartID.TabIndex = 1;
			this.txtCartID.TextAlign = HorizontalAlignment.Center;
			this.lblOutDetail.AutoSize = true;
			this.lblOutDetail.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblOutDetail.Location = new Point(206, 406);
			this.lblOutDetail.Margin = new Padding(2, 0, 2, 0);
			this.lblOutDetail.Name = "lblOutDetail";
			this.lblOutDetail.Size = new Size(163, 20);
			this.lblOutDetail.TabIndex = 23;
			this.lblOutDetail.Text = "OUT-Stock Details:";
			this.lblInDetail.AutoSize = true;
			this.lblInDetail.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblInDetail.Location = new Point(206, 156);
			this.lblInDetail.Margin = new Padding(2, 0, 2, 0);
			this.lblInDetail.Name = "lblInDetail";
			this.lblInDetail.Size = new Size(145, 20);
			this.lblInDetail.TabIndex = 23;
			this.lblInDetail.Text = "IN-Stock Details:";
			this.txtTotalCartOut.BorderStyle = BorderStyle.None;
			this.txtTotalCartOut.CharacterCasing = CharacterCasing.Upper;
			this.txtTotalCartOut.Enabled = false;
			this.txtTotalCartOut.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtTotalCartOut.Location = new Point(373, 403);
			this.txtTotalCartOut.Margin = new Padding(2, 3, 2, 3);
			this.txtTotalCartOut.Name = "txtTotalCartOut";
			this.txtTotalCartOut.ReadOnly = true;
			this.txtTotalCartOut.Size = new Size(132, 23);
			this.txtTotalCartOut.TabIndex = 1;
			this.txtTotalCartOut.Text = "0";
			this.txtTotalCartOut.TextAlign = HorizontalAlignment.Right;
			this.lblTotalCartOut.AutoSize = true;
			this.lblTotalCartOut.Enabled = false;
			this.lblTotalCartOut.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblTotalCartOut.Location = new Point(505, 407);
			this.lblTotalCartOut.Margin = new Padding(2, 0, 2, 0);
			this.lblTotalCartOut.Name = "lblTotalCartOut";
			this.lblTotalCartOut.Size = new Size(50, 20);
			this.lblTotalCartOut.TabIndex = 23;
			this.lblTotalCartOut.Text = "cart /";
			this.lblTotalKgOut.AutoSize = true;
			this.lblTotalKgOut.Enabled = false;
			this.lblTotalKgOut.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblTotalKgOut.Location = new Point(692, 406);
			this.lblTotalKgOut.Margin = new Padding(2, 0, 2, 0);
			this.lblTotalKgOut.Name = "lblTotalKgOut";
			this.lblTotalKgOut.Size = new Size(28, 20);
			this.lblTotalKgOut.TabIndex = 23;
			this.lblTotalKgOut.Text = "kg";
			this.txtTotalKgOut.BorderStyle = BorderStyle.None;
			this.txtTotalKgOut.CharacterCasing = CharacterCasing.Upper;
			this.txtTotalKgOut.Enabled = false;
			this.txtTotalKgOut.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtTotalKgOut.Location = new Point(560, 403);
			this.txtTotalKgOut.Margin = new Padding(2, 3, 2, 3);
			this.txtTotalKgOut.Name = "txtTotalKgOut";
			this.txtTotalKgOut.ReadOnly = true;
			this.txtTotalKgOut.Size = new Size(132, 23);
			this.txtTotalKgOut.TabIndex = 1;
			this.txtTotalKgOut.Text = "0";
			this.txtTotalKgOut.TextAlign = HorizontalAlignment.Right;
			this.lblTotalCartIn.AutoSize = true;
			this.lblTotalCartIn.Enabled = false;
			this.lblTotalCartIn.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblTotalCartIn.Location = new Point(505, 157);
			this.lblTotalCartIn.Margin = new Padding(2, 0, 2, 0);
			this.lblTotalCartIn.Name = "lblTotalCartIn";
			this.lblTotalCartIn.Size = new Size(50, 20);
			this.lblTotalCartIn.TabIndex = 23;
			this.lblTotalCartIn.Text = "cart /";
			this.lblTotalKgIn.AutoSize = true;
			this.lblTotalKgIn.Enabled = false;
			this.lblTotalKgIn.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblTotalKgIn.Location = new Point(692, 157);
			this.lblTotalKgIn.Margin = new Padding(2, 0, 2, 0);
			this.lblTotalKgIn.Name = "lblTotalKgIn";
			this.lblTotalKgIn.Size = new Size(28, 20);
			this.lblTotalKgIn.TabIndex = 23;
			this.lblTotalKgIn.Text = "kg";
			this.txtTotalCartIn.BorderStyle = BorderStyle.None;
			this.txtTotalCartIn.CharacterCasing = CharacterCasing.Upper;
			this.txtTotalCartIn.Enabled = false;
			this.txtTotalCartIn.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtTotalCartIn.Location = new Point(373, 153);
			this.txtTotalCartIn.Margin = new Padding(2, 3, 2, 3);
			this.txtTotalCartIn.Name = "txtTotalCartIn";
			this.txtTotalCartIn.ReadOnly = true;
			this.txtTotalCartIn.Size = new Size(132, 23);
			this.txtTotalCartIn.TabIndex = 1;
			this.txtTotalCartIn.Text = "0";
			this.txtTotalCartIn.TextAlign = HorizontalAlignment.Right;
			this.txtTotalKgIn.BorderStyle = BorderStyle.None;
			this.txtTotalKgIn.CharacterCasing = CharacterCasing.Upper;
			this.txtTotalKgIn.Enabled = false;
			this.txtTotalKgIn.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtTotalKgIn.Location = new Point(560, 153);
			this.txtTotalKgIn.Margin = new Padding(2, 3, 2, 3);
			this.txtTotalKgIn.Name = "txtTotalKgIn";
			this.txtTotalKgIn.ReadOnly = true;
			this.txtTotalKgIn.Size = new Size(132, 23);
			this.txtTotalKgIn.TabIndex = 1;
			this.txtTotalKgIn.Text = "0";
			this.txtTotalKgIn.TextAlign = HorizontalAlignment.Right;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(750, 266);
			this.label5.Margin = new Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new Size(82, 20);
			this.label5.TabIndex = 23;
			this.label5.Text = "Part info:";
			this.txtPartname.CharacterCasing = CharacterCasing.Upper;
			this.txtPartname.Enabled = false;
			this.txtPartname.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtPartname.Location = new Point(880, 262);
			this.txtPartname.Margin = new Padding(2, 3, 2, 3);
			this.txtPartname.Name = "txtPartname";
			this.txtPartname.ReadOnly = true;
			this.txtPartname.Size = new Size(149, 30);
			this.txtPartname.TabIndex = 1;
			this.txtPartname.TextAlign = HorizontalAlignment.Center;
			this.txtPartnumber.CharacterCasing = CharacterCasing.Upper;
			this.txtPartnumber.Enabled = false;
			this.txtPartnumber.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtPartnumber.Location = new Point(1045, 262);
			this.txtPartnumber.Margin = new Padding(2, 3, 2, 3);
			this.txtPartnumber.Name = "txtPartnumber";
			this.txtPartnumber.ReadOnly = true;
			this.txtPartnumber.Size = new Size(146, 30);
			this.txtPartnumber.TabIndex = 1;
			this.txtPartnumber.TextAlign = HorizontalAlignment.Center;
			this.txtPackingdateOut.CharacterCasing = CharacterCasing.Upper;
			this.txtPackingdateOut.Enabled = false;
			this.txtPackingdateOut.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtPackingdateOut.Location = new Point(880, 346);
			this.txtPackingdateOut.Margin = new Padding(2, 3, 2, 3);
			this.txtPackingdateOut.Name = "txtPackingdateOut";
			this.txtPackingdateOut.ReadOnly = true;
			this.txtPackingdateOut.Size = new Size(149, 30);
			this.txtPackingdateOut.TabIndex = 25;
			this.txtPackingdateOut.TextAlign = HorizontalAlignment.Center;
			this.txtLotnumberOut.CharacterCasing = CharacterCasing.Upper;
			this.txtLotnumberOut.Enabled = false;
			this.txtLotnumberOut.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.txtLotnumberOut.Location = new Point(880, 304);
			this.txtLotnumberOut.Margin = new Padding(2, 3, 2, 3);
			this.txtLotnumberOut.Name = "txtLotnumberOut";
			this.txtLotnumberOut.ReadOnly = true;
			this.txtLotnumberOut.Size = new Size(149, 30);
			this.txtLotnumberOut.TabIndex = 26;
			this.txtLotnumberOut.TextAlign = HorizontalAlignment.Center;
			this.hdfCurrDate.Location = new Point(1125, 152);
			this.hdfCurrDate.Name = "hdfCurrDate";
			this.hdfCurrDate.Size = new Size(64, 20);
			this.hdfCurrDate.TabIndex = 27;
			this.hdfCurrDate.Text = "hdfCurrDate";
			this.hdfCurrDate.Visible = false;
			this.textBox1.Location = new Point(1055, 152);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(64, 20);
			this.textBox1.TabIndex = 27;
			this.textBox1.Text = "hdfCurrLOT";
			this.textBox1.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(1350, 729);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.hdfCurrDate);
			base.Controls.Add(this.txtPackingdateOut);
			base.Controls.Add(this.txtLotnumberOut);
			base.Controls.Add(this.lblWeightUnit);
			base.Controls.Add(this.txtTotalKgIn);
			base.Controls.Add(this.txtTotalCartIn);
			base.Controls.Add(this.lblTotalKgIn);
			base.Controls.Add(this.lblTotalCartIn);
			base.Controls.Add(this.txtMachineID);
			base.Controls.Add(this.txtPackingDate);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.txtLOTnumber);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.txtPartnumber);
			base.Controls.Add(this.txtPartname);
			base.Controls.Add(this.txtCartID);
			base.Controls.Add(this.txtWeight);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.txtTotalKgOut);
			base.Controls.Add(this.txtTotalCartOut);
			base.Controls.Add(this.txtFunction);
			base.Controls.Add(this.txtInputBox);
			base.Controls.Add(this.lblInDetail);
			base.Controls.Add(this.lblOutDetail);
			base.Controls.Add(this.lblTotalKgOut);
			base.Controls.Add(this.lblTotalCartOut);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.dgvOutStatusAll);
			base.Controls.Add(this.dgvInStatusAll);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtStatus);
			base.Controls.Add(this.pictureBox2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.label3);
			this.ForeColor = SystemColors.ControlDarkDark;
			base.Icon = (Icon)resources.GetObject("$this.Icon");
			base.Margin = new Padding(2, 3, 2, 3);
			this.MinimumSize = new Size(1366, 768);
			base.Name = "Form1";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Inventory Mixing";
			base.WindowState = FormWindowState.Maximized;
			base.Load += new EventHandler(this.Form1_Load);
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.dgvInStatusAll).EndInit();
			((ISupportInitialize)this.dgvOutStatusAll).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
