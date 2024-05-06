using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Inventory_Data
{
	public static class cls
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
				new cls.AutoClosingMessageBox(text, caption, timeout);
			}

			private void OnTimerElapsed(object state)
			{
				IntPtr mbWnd = cls.AutoClosingMessageBox.FindWindow("#32770", this._caption);
				bool flag = mbWnd != IntPtr.Zero;
				if (flag)
				{
					cls.AutoClosingMessageBox.SendMessage(mbWnd, 16u, IntPtr.Zero, IntPtr.Zero);
				}
				this._timeoutTimer.Dispose();
			}

			[DllImport("user32.dll", SetLastError = true)]
			private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		}

		public static string factcd = "F1";

		public static string factnm = "본사";

		public static string shiftsno = "1";

		public static string shiftsnm = "Night";

		public static string workdate = "";

		public static string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

		public static DateTime sTime1 = default(DateTime);

		public static DateTime sTime2 = default(DateTime);

		public static BindingSource bindingSource0 = new BindingSource();

		public static SqlDataAdapter dataAdapter0 = new SqlDataAdapter();

		public static BindingSource bindingSource1 = new BindingSource();

		public static SqlDataAdapter dataAdapter1 = new SqlDataAdapter();

		public static BindingSource bindingSource2 = new BindingSource();

		public static SqlDataAdapter dataAdapter2 = new SqlDataAdapter();

		public static BindingSource bindingSource3 = new BindingSource();

		public static SqlDataAdapter dataAdapter3 = new SqlDataAdapter();

		public static BindingSource bindingSource4 = new BindingSource();

		public static SqlDataAdapter dataAdapter4 = new SqlDataAdapter();

		public static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(cls.NumericCheck);

		public static KeyPressEventHandler NumericCheckHandlerDecimal = new KeyPressEventHandler(cls.NumericCheckDecimal);

		public static void getProductInfo()
		{
			DateTime nNow = DateTime.Now;
			cls.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
			cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				cls.sTime1 = cls.sTime1.AddDays(-1.0);
				cls.sTime2 = cls.sTime2.AddDays(0.0);
				cls.shiftsnm = "Night";
				cls.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					cls.sTime1 = cls.sTime1.AddDays(0.0);
					cls.sTime2 = cls.sTime2.AddDays(1.0);
					cls.shiftsnm = "Night";
					cls.shiftsno = "2";
				}
				else
				{
					cls.shiftsnm = "Day";
					cls.shiftsno = "1";
				}
			}
		}

		public static string getProductInfo(string datetimeformat)
		{
			DateTime nNow = DateTime.Now;
			cls.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
			cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				cls.sTime1 = cls.sTime1.AddDays(-1.0);
				cls.sTime2 = cls.sTime2.AddDays(0.0);
				cls.shiftsnm = "Night";
				cls.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					cls.sTime1 = cls.sTime1.AddDays(0.0);
					cls.sTime2 = cls.sTime2.AddDays(1.0);
					cls.shiftsnm = "Night";
					cls.shiftsno = "2";
				}
				else
				{
					cls.shiftsnm = "Day";
					cls.shiftsno = "1";
				}
			}
			return nNow.ToString(datetimeformat);
		}

		public static string getProductInfo(string datetimeformat, string shift)
		{
			string sDateTime = "";
			DateTime nNow = DateTime.Now;
			cls.sNow = nNow.ToString(datetimeformat);
			cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				cls.sTime1 = cls.sTime1.AddDays(-1.0);
				cls.sTime2 = cls.sTime2.AddDays(0.0);
				cls.shiftsnm = "Night";
				cls.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					cls.sTime1 = cls.sTime1.AddDays(0.0);
					cls.sTime2 = cls.sTime2.AddDays(1.0);
					cls.shiftsnm = "Night";
					cls.shiftsno = "2";
				}
				else
				{
					cls.shiftsnm = "Day";
					cls.shiftsno = "1";
				}
			}
			bool flag3 = shift != "";
			if (flag3)
			{
				bool flag4 = shift == "1";
				if (flag4)
				{
					sDateTime = cls.shiftsno + " - " + cls.sTime1.ToString(datetimeformat);
				}
				else
				{
					bool flag5 = shift == "d";
					if (flag5)
					{
						sDateTime = cls.shiftsnm + " - " + cls.sTime1.ToString(datetimeformat);
					}
				}
			}
			else
			{
				sDateTime = cls.sTime1.ToString(datetimeformat);
			}
			return sDateTime;
		}

		public static string getShiftInfo()
		{
			DateTime nNow = DateTime.Now;
			cls.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
			cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				cls.sTime1 = cls.sTime1.AddDays(-1.0);
				cls.sTime2 = cls.sTime2.AddDays(0.0);
				cls.shiftsnm = "Night";
				cls.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					cls.sTime1 = cls.sTime1.AddDays(0.0);
					cls.sTime2 = cls.sTime2.AddDays(1.0);
					cls.shiftsnm = "Night";
					cls.shiftsno = "2";
				}
				else
				{
					cls.shiftsnm = "Day";
					cls.shiftsno = "1";
				}
			}
			return cls.shiftsnm;
		}

		public static string getShiftName()
		{
			DateTime nNow = DateTime.Now;
			cls.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
			cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				cls.sTime1 = cls.sTime1.AddDays(-1.0);
				cls.sTime2 = cls.sTime2.AddDays(0.0);
				cls.shiftsnm = "Night";
				cls.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					cls.sTime1 = cls.sTime1.AddDays(0.0);
					cls.sTime2 = cls.sTime2.AddDays(1.0);
					cls.shiftsnm = "Night";
					cls.shiftsno = "2";
				}
				else
				{
					cls.shiftsnm = "Day";
					cls.shiftsno = "1";
				}
			}
			return cls.shiftsnm;
		}

		public static string getShiftNo()
		{
			DateTime nNow = DateTime.Now;
			cls.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
			cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
			cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
			DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
			DateTime currDate = nNow.Date;
			bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
			if (flag)
			{
				cls.sTime1 = cls.sTime1.AddDays(-1.0);
				cls.sTime2 = cls.sTime2.AddDays(0.0);
				cls.shiftsnm = "Night";
				cls.shiftsno = "2";
			}
			else
			{
				bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
				if (flag2)
				{
					cls.sTime1 = cls.sTime1.AddDays(0.0);
					cls.sTime2 = cls.sTime2.AddDays(1.0);
					cls.shiftsnm = "Night";
					cls.shiftsno = "2";
				}
				else
				{
					cls.shiftsnm = "Day";
					cls.shiftsno = "1";
				}
			}
			return cls.shiftsno;
		}

        public static string getDateShift(string format)
        {
            string dateshift = "";
            DateTime nNow = DateTime.Now;
            cls.sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");
            cls.sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
            cls.sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
            DateTime lastOfThisMonth = new DateTime(nNow.Year, nNow.Month, 1).AddMonths(1).AddDays(-1.0);
            DateTime currDate = nNow.Date;
            bool flag = DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00");
            if (flag)
            {
                cls.sTime1 = cls.sTime1.AddDays(-1.0);
                cls.sTime2 = cls.sTime2.AddDays(0.0);
                cls.shiftsnm = "Night";
                cls.shiftsno = "2";
            }
            else
            {
                bool flag2 = nNow.TimeOfDay >= TimeSpan.Parse("20:00:00");
                if (flag2)
                {
                    cls.sTime1 = cls.sTime1.AddDays(0.0);
                    cls.sTime2 = cls.sTime2.AddDays(1.0);
                    cls.shiftsnm = "Night";
                    cls.shiftsno = "2";
                }
                else
                {
                    cls.shiftsnm = "Day";
                    cls.shiftsno = "1";
                }
            }

            if (format == "dt")
            {
                dateshift = cls.sTime1.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else if (format == "DN")
            {
                dateshift = cls.shiftsnm.ToUpper() + " " + cls.sTime1.ToString("dd/MM/yyyy");
            }
            else if (format == "sh")
            {
                dateshift = cls.shiftsnm;
            }
            else if (format == "1t")
            {
                dateshift = cls.sTime1.ToString("HH:mm:ss");
            }
            else if (format == "fs")
            {
                dateshift = cls.shiftsnm + " " + cls.sTime1.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else if (format == "dn")
            {
                dateshift = cls.shiftsnm + " " + cls.sTime1.ToString("dd/MM/yyyy");
            }
            else if (format == "SH")
            {
                dateshift = cls.shiftsnm.ToUpper();
            }
            else if (format == "FS")
            {
                dateshift = cls.shiftsnm.ToUpper() + " " + cls.sTime1.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else if (format == "1d")
            {
                dateshift = cls.sTime1.ToString("dd/MM/yyyy");
            }
            else if (format == "d/")
            {
                dateshift = cls.sTime1.ToString("dd/MM/yyyy") + "-" + cls.shiftsno;
            }
            else if (format == "y-")
            {
                dateshift = cls.sTime1.ToString("yyyy-MM-dd") + "-" + cls.shiftsno;
            }
            else if (format == "d-")
            {
                dateshift = cls.sTime1.ToString("dd-MM-yyyy") + "-" + cls.shiftsno;
            }
            else if (format == "y/")
            {
                dateshift = cls.sTime1.ToString("yyyy/MM/dd") + "-" + cls.shiftsno;
            }
            else if (format == "y")
            {
                dateshift = cls.sTime1.ToString("yyyyMMdd") + "-" + cls.shiftsno;
            }
            else if (format == "d")
            {
                dateshift = cls.sTime1.ToString("ddMMyyyy") + "-" + cls.shiftsno;
            }


            //uint num = < PrivateImplementationDetails >.ComputeStringHash(format);
            //if (num <= 1662835111u)
            //{
            //    if (num <= 1293727493u)
            //    {
            //        if (num != 1062342494u)
            //        {
            //            if (num != 1120831591u)
            //            {
            //                if (num == 1293727493u)
            //                {
            //                    if (format == "dt")
            //                    {
            //                        dateshift = cls.sTime1.ToString("dd/MM/yyyy HH:mm:ss");
            //                    }
            //                }
            //            }
            //            else if (format == "DN")
            //            {
            //                dateshift = cls.shiftsnm.ToUpper() + " " + cls.sTime1.ToString("dd/MM/yyyy");
            //            }
            //        }
            //        else if (format == "sh")
            //        {
            //            dateshift = cls.shiftsnm;
            //        }
            //    }
            //    else if (num <= 1475053752u)
            //    {
            //        if (num != 1445123422u)
            //        {
            //            if (num == 1475053752u)
            //            {
            //                if (format == "1t")
            //                {
            //                    dateshift = cls.sTime1.ToString("HH:mm:ss");
            //                }
            //            }
            //        }
            //        else if (format == "fs")
            //        {
            //            dateshift = cls.shiftsnm + " " + cls.sTime1.ToString("dd/MM/yyyy HH:mm:ss");
            //        }
            //    }
            //    else if (num != 1594003422u)
            //    {
            //        if (num == 1662835111u)
            //        {
            //            if (format == "dn")
            //            {
            //                dateshift = cls.shiftsnm + " " + cls.sTime1.ToString("dd/MM/yyyy");
            //            }
            //        }
            //    }
            //    else if (format == "SH")
            //    {
            //        dateshift = cls.shiftsnm.ToUpper();
            //    }
            //}
            //else if (num <= 2719825108u)
            //{
            //    if (num <= 1976784350u)
            //    {
            //        if (num != 1743495656u)
            //        {
            //            if (num == 1976784350u)
            //            {
            //                if (format == "FS")
            //                {
            //                    dateshift = cls.shiftsnm.ToUpper() + " " + cls.sTime1.ToString("dd/MM/yyyy HH:mm:ss");
            //                }
            //            }
            //        }
            //        else if (format == "1d")
            //        {
            //            dateshift = cls.sTime1.ToString("dd/MM/yyyy");
            //        }
            //    }
            //    else if (num != 2690654107u)
            //    {
            //        if (num == 2719825108u)
            //        {
            //            if (format == "d/")
            //            {
            //                dateshift = cls.sTime1.ToString("dd/MM/yyyy") + "-" + cls.shiftsno;
            //            }
            //        }
            //    }
            //    else if (format == "y-")
            //    {
            //        dateshift = cls.sTime1.ToString("yyyy-MM-dd") + "-" + cls.shiftsno;
            //    }
            //}
            //else if (num <= 2753380346u)
            //{
            //    if (num != 2724209345u)
            //    {
            //        if (num == 2753380346u)
            //        {
            //            if (format == "d-")
            //            {
            //                dateshift = cls.sTime1.ToString("dd-MM-yyyy") + "-" + cls.shiftsno;
            //            }
            //        }
            //    }
            //    else if (format == "y/")
            //    {
            //        dateshift = cls.sTime1.ToString("yyyy/MM/dd") + "-" + cls.shiftsno;
            //    }
            //}
            //else if (num != 3775669363u)
            //{
            //    if (num == 4228665076u)
            //    {
            //        if (format == "y")
            //        {
            //            dateshift = cls.sTime1.ToString("yyyyMMdd") + "-" + cls.shiftsno;
            //        }
            //    }
            //}
            //else if (format == "d")
            //{
            //    dateshift = cls.sTime1.ToString("ddMMyyyy") + "-" + cls.shiftsno;
            //}
            return dateshift;
        }

        public static int getCount(string sql)
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

		public static int getCount(string sql, string connect)
		{
			int found = 0;
			string sConn = ConfigurationManager.ConnectionStrings[connect].ConnectionString;
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

		public static string getValue(string sql)
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

		public static DataTable getTable(string sql)
		{
			DataTable tbl = new DataTable();
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
				da.Fill(tbl);
			}
			catch
			{
			}
			finally
			{
				con.Close();
			}
			return tbl;
		}

		public static string getValue(string sql, string connect)
		{
			string found = "";
			string sConn = ConfigurationManager.ConnectionStrings[connect].ConnectionString;
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

		public static int getWidth(DataGridView dgv)
		{
			return dgv.Width;
		}

		public static int getHeght(DataGridView dgv)
		{
			return dgv.Height;
		}

		public static void GetData(string selectCommand, DataGridView dgvName, BindingSource bindingsource, SqlDataAdapter sqldataadapter)
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

		public static void GetData(string strConnect, string selectCommand, DataGridView dgvName, BindingSource bindingsource, SqlDataAdapter sqldataadapter)
		{
			try
			{
				string connectionString = ConfigurationManager.ConnectionStrings[strConnect].ConnectionString;
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

		public static void BindDataGrid(string CommandText, DataGridView GridView, BindingSource BindingSource)
		{
			DataTable dt = new DataTable();
			string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
			SqlConnection con = new SqlConnection(sConn);
			con.Open();
			SqlDataAdapter da = new SqlDataAdapter(new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = CommandText,
				Connection = con
			});
			da.Fill(dt);
			BindingSource.DataSource = dt;
			GridView.DataSource = BindingSource;
		}

		public static void BindDataGrid(string strConnect, string CommandText, DataGridView GridView, BindingSource BindingSource)
		{
			DataTable dt = new DataTable();
			string sConn = ConfigurationManager.ConnectionStrings[strConnect].ConnectionString;
			SqlConnection con = new SqlConnection(sConn);
			con.Open();
			SqlDataAdapter da = new SqlDataAdapter(new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = CommandText,
				Connection = con
			});
			da.Fill(dt);
			BindingSource.DataSource = dt;
			GridView.DataSource = BindingSource;
		}

		public static void fnClearSelectColor(DataGridView dgv)
		{
			dgv.ClearSelection();
			dgv.CurrentCell = null;
		}

		public static void fnResetTimer(System.Windows.Forms.Timer timer)
		{
			timer.Stop();
			timer.Start();
		}

		public static void fnSetDatagridRowColor(DataGridView dgv)
		{
			foreach (DataGridViewRow row in ((IEnumerable)dgv.Rows))
			{
				bool flag = row.Index % 2 != 0;
				if (flag)
				{
					row.DefaultCellStyle.BackColor = Color.LightCyan;
				}
				else
				{
					row.DefaultCellStyle.BackColor = Color.White;
				}
			}
		}

		public static void NumericCheck(object sender, KeyPressEventArgs e)
		{
			DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
			bool flag = s != null && e.KeyChar == ',';
			if (flag)
			{
				e.KeyChar = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
				e.Handled = s.Text.Contains(e.KeyChar);
			}
			else
			{
				e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
			}
		}

		public static void NumericCheckDecimal(object sender, KeyPressEventArgs e)
		{
			DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
			bool flag = s != null && (e.KeyChar == '.' || e.KeyChar == ',');
			if (flag)
			{
				e.KeyChar = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
				e.Handled = s.Text.Contains(e.KeyChar);
			}
			else
			{
				e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
			}
		}

		public static string EncryptString(string Message, string Passphrase)
		{
			UTF8Encoding UTF8 = new UTF8Encoding();
			MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
			TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
			TDESAlgorithm.Key = TDESKey;
			TDESAlgorithm.Mode = CipherMode.ECB;
			TDESAlgorithm.Padding = PaddingMode.PKCS7;
			byte[] DataToEncrypt = UTF8.GetBytes(Message);
			byte[] Results;
			try
			{
				ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
				Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
			}
			finally
			{
				TDESAlgorithm.Clear();
				HashProvider.Clear();
			}
			return Convert.ToBase64String(Results);
		}

		public static string DecryptString(string Message, string Passphrase)
		{
			UTF8Encoding UTF8 = new UTF8Encoding();
			MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
			TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
			TDESAlgorithm.Key = TDESKey;
			TDESAlgorithm.Mode = CipherMode.ECB;
			TDESAlgorithm.Padding = PaddingMode.PKCS7;
			byte[] DataToDecrypt = Convert.FromBase64String(Message);
			byte[] Results;
			try
			{
				ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
				Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
			}
			finally
			{
				TDESAlgorithm.Clear();
				HashProvider.Clear();
			}
			return UTF8.GetString(Results);
		}

		public static string RightString(this string str, int length)
		{
			return str.Substring(str.Length - length, length);
		}

		public static void FreezeBand(DataGridViewBand band)
		{
			band.Frozen = true;
			band.DefaultCellStyle = new DataGridViewCellStyle
			{
				BackColor = Color.WhiteSmoke
			};
		}

		public static int fnGetScreenWidth()
		{
			return SystemInformation.VirtualScreen.Width;
		}

		public static int fnGetScreenHeight()
		{
			return SystemInformation.VirtualScreen.Height;
		}

		public static Control GetControlByName(Control ParentCntl, string NameToSearch)
		{
			bool flag = ParentCntl.Name == NameToSearch;
			Control result;
			if (flag)
			{
				result = ParentCntl;
			}
			else
			{
				foreach (Control ChildCntl in ParentCntl.Controls)
				{
					Control ResultCntl = cls.GetControlByName(ChildCntl, NameToSearch);
					bool flag2 = ResultCntl != null;
					if (flag2)
					{
						result = ResultCntl;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public static void status(Label label, string message, int interval)
		{
			label.Text = message;
			bool flag = message.ToUpper() == "OK";
			if (flag)
			{
				label.ForeColor = Color.Green;
			}
			else
			{
				label.ForeColor = Color.Red;
			}
			System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
			t.Interval = interval;
			t.Tick += delegate(object s, EventArgs e)
			{
				label.Hide();
				t.Stop();
			};
			t.Start();
		}

		public static string ShowDialog(string text, string caption)
		{
			Form prompt = new Form
			{
				Width = 500,
				Height = 200,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				Text = caption,
				StartPosition = FormStartPosition.CenterScreen
			};
			Label textLabel = new Label
			{
				Left = 50,
				Top = 10,
				Text = text,
				Width = 450
			};
			TextBox textBox = new TextBox
			{
				Left = 50,
				Top = 40,
				Width = 400,
				Height = 80,
				Multiline = true
			};
			Button confirmation = new Button
			{
				Text = "XÁC NHẬN",
				Left = 350,
				Width = 100,
				Top = 130,
				DialogResult = DialogResult.OK
			};
			confirmation.Click += delegate(object sender, EventArgs e)
			{
				prompt.Close();
			};
			prompt.Controls.Add(textBox);
			prompt.Controls.Add(confirmation);
			prompt.Controls.Add(textLabel);
			prompt.AcceptButton = confirmation;
			return (prompt.ShowDialog() == DialogResult.OK) ? textBox.Text : "";
		}

		public static bool CheckForInternetConnection()
		{
			bool result;
			try
			{
				using (WebClient client = new WebClient())
				{
					using (client.OpenRead("http://clients3.google.com/generate_204"))
					{
						result = true;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
