using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Data
{
    public static class DBConnect
    {
        public static SqlConnection myCon = null;

        public static void CreateConnection()
        {
            myCon = new SqlConnection(cls.getConnectionString());
            myCon.Open();

        }
    }

    public static class cls
    {
        public static BindingSource bindingSource0 = new BindingSource();
        public static BindingSource bindingSource1 = new BindingSource();
        public static BindingSource bindingSource2 = new BindingSource();
        public static BindingSource bindingSource3 = new BindingSource();
        public static BindingSource bindingSource4 = new BindingSource();
        public static SqlDataAdapter dataAdapter0 = new SqlDataAdapter();
        public static SqlDataAdapter dataAdapter1 = new SqlDataAdapter();
        public static SqlDataAdapter dataAdapter2 = new SqlDataAdapter();
        public static SqlDataAdapter dataAdapter3 = new SqlDataAdapter();
        public static SqlDataAdapter dataAdapter4 = new SqlDataAdapter();

        public static string factcd = "F1";
        public static string factnm = "본사";
        public static string shiftsno = "1";
        public static string shiftsnm = "Night";
        public static string workdate = "";
        public static string sNow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        public static DateTime sTime1 = new DateTime();
        public static DateTime sTime2 = new DateTime();


        public static string fnGetDate(string format)
        {
            string s = "";

            DateTime nNow = DateTime.Now;
            //sNow = nNow.ToString("yyyy-MM-dd HH:mm:ss");

            //button2.Text = sNow;//DateTime.Now.TimeOfDay.ToString();

            if (DateTime.Now.TimeOfDay < TimeSpan.Parse("08:00:00"))
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0).AddDays(-1);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else if (nNow.TimeOfDay >= TimeSpan.Parse("20:00:00"))
            {

                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0).AddDays(1);
                shiftsnm = "Night";
                shiftsno = "2";
            }
            else
            {
                sTime1 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 8, 0, 0);
                sTime2 = new DateTime(nNow.Year, nNow.Month, nNow.Day, 20, 0, 0);
                shiftsnm = "Day";
                shiftsno = "1";
            }
            // sTime1 = sTime1.AddDays(-2);
            //workdate = sTime1.ToString("yyyyMMdd");
            //button1.Text = sTime1.ToString("yyyy/MM/dd") + " " + shiftsnm;
            switch (format)
            {
                case "d":   //Date: 09/10/2017
                    s = nNow.ToString("dd/MM/yyyy");
                    break;
                case "dt":  //Date time: 09/10/2017 19:36:22
                    s = nNow.ToString("dd/MM/yyyy HH:mm:ss");
                    break;
                case "t":   //Time: 19:36:22
                    s = nNow.ToString("HH:mm:ss");
                    break;
                case "sd":  //Shift date: Day(Night) 09/10/2017
                    s = shiftsnm + " " + nNow.ToString("dd/MM/yyyy");
                    break;
                case "SD":  // Shift date: DAY(NIGHT) 09/10/2017
                    s = shiftsnm.ToUpper() + " " + nNow.ToString("dd/MM/yyyy");
                    break;
                case "ct":  //Country time: Vina 19:36:22
                    s = "Vina " + nNow.ToString("HH:mm:ss");
                    break;
                case "CT":  // Country time: VINA 19:36:22
                    s = "VINA " + nNow.ToString("HH:mm:ss");
                    break;
                case "s":   // Shift: Day/Night
                    s = shiftsnm;
                    break;
                case "S":   // Shift (capital): DAY/NIGHT
                    s = shiftsnm.ToUpper();
                    break;
                case "sn":  // Shift number: 1-Day; 2-Night
                    s = shiftsno;
                    break;
                case "lot":
                    s = nNow.ToString("yyyyMMdd");
                    break;
            }

            return s;
        }

        public static DataSet ExecuteDataSet(string sql, CommandType cmdType)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //log to a file or Throw a message ex.Message;
                }
                return ds;
            }
        }

        public static DataSet ExecuteDataSet(string sql, CommandType cmdType, string conName)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings[conName].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //log to a file or Throw a message ex.Message;
                }
                return ds;
            }
        }

        public static DataSet ExecuteDataSet(string sql, CommandType cmdType, string conName, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings[conName].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //log to a file or Throw a message ex.Message;
                }
                return ds;
            }
            //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@IMPORTID";
            //sParams[0].Value = SelectedListID;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@PREFIX";
            //sParams[1].Value = selectedPrefix;
        }

        public static DataSet ExecuteDataSet(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //log to a file or Throw a message ex.Message;
                }
                return ds;
            }
            //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@IMPORTID";
            //sParams[0].Value = SelectedListID;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@PREFIX";
            //sParams[1].Value = selectedPrefix;
        }

        public static DataSet ExecuteDataSet(string sql, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //log to a file or Throw a message ex.Message;
                }
                return ds;
            }
            //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@IMPORTID";
            //sParams[0].Value = SelectedListID;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@PREFIX";
            //sParams[1].Value = selectedPrefix;
        }

        public static DataTable ExecuteDataTable(string sql, CommandType cmdType)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //Show a message or log a message on ex.Message
                }
                return ds.Tables[0];
            }
        }

        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, string conName)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings[conName].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //Show a message or log a message on ex.Message
                }
                return ds.Tables[0];
            }
        }

        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, string conName, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings[conName].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //Show a message or log a message on ex.Message
                }
                return ds.Tables[0];
            }
            //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@IMPORTID";
            //sParams[0].Value = SelectedListID;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@PREFIX";
            //sParams[1].Value = selectedPrefix;
        }

        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = cmdType;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //Show a message or log a message on ex.Message
                }
                return ds.Tables[0];
            }
            //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@IMPORTID";
            //sParams[0].Value = SelectedListID;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@PREFIX";
            //sParams[1].Value = selectedPrefix;
        }

        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (DataSet ds = new DataSet())
            using (SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.Connection.Open();
                    new SqlDataAdapter(cmd).Fill(ds);
                }
                //catch (SqlException ex)
                catch
                {
                    //Show a message or log a message on ex.Message
                }
                return ds.Tables[0];
            }
            //SqlParameter[] sParams = new SqlParameter[2]; // Parameter count

            //sParams[0] = new SqlParameter();
            //sParams[0].SqlDbType = SqlDbType.Int;
            //sParams[0].ParameterName = "@IMPORTID";
            //sParams[0].Value = SelectedListID;

            //sParams[1] = new SqlParameter();
            //sParams[1].SqlDbType = SqlDbType.VarChar;
            //sParams[1].ParameterName = "@PREFIX";
            //sParams[1].Value = selectedPrefix;
        }

        public static string fnGetRow(string sql)
        {
            string rowValue = "";
            DataSet ds = new DataSet();
            ds = ExecuteDataSet(sql, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rowValue = ds.Tables[0].Rows[0][0].ToString();
            }
            return rowValue;
        }

        public static void RemoveSelection(Object obj)
        {
            TextBox textbox = obj as TextBox;
            if (textbox != null)
            {
                textbox.SelectionLength = 0;
            }
        }

        public static int fnGetCount(string sql)
        {
            int count = 0;
            DataSet ds = new DataSet();
            ds = ExecuteDataSet(sql, CommandType.StoredProcedure);
            count = ds.Tables[0].Rows.Count;
            return count;
        }
            

        public static string getConnectionString()
        {
            string strConn = "";
            strConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            //strConn = ConfigurationSettings.AppSettings["conn"];
            return strConn;
        }

        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        public static DataTable fnSelect(string procedure)
        {
            using (DataTable dt = new DataTable())
            {
                string connString = getConnectionString();
                string sql = procedure;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            da.SelectCommand = new SqlCommand(sql, conn);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;

                            //DataSet ds = new DataSet();
                            //da.Fill(ds, "result_name");

                            //dt = ds.Tables["result_name"];
                            da.Fill(dt);
                            //foreach (DataRow row in dt.Rows)
                            //{
                            //    //manipulate your data
                            //}
                            //dtData = dt;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
                return dt;
            }
        }

        public static DataTable fnSelect(string procedure,string connName)
        {
            using (DataTable dt = new DataTable())
            {
                string connString = GetConnectionStringByName(connName);
                string sql = procedure;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            da.SelectCommand = new SqlCommand(sql, conn);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;

                            //DataSet ds = new DataSet();
                            //da.Fill(ds, "result_name");

                            //dt = ds.Tables["result_name"];
                            da.Fill(dt);
                            //foreach (DataRow row in dt.Rows)
                            //{
                            //    //manipulate your data
                            //}
                            //dtData = dt;
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Error: " + ex.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
                return dt;
            }
        }

        public static void fnUpdDel(string procedure,string connName, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(GetConnectionStringByName(connName)))
                using (SqlCommand _cmdDelete = new SqlCommand())
                {
                    _cmdDelete.CommandType = CommandType.StoredProcedure;
                    _cmdDelete.CommandText = procedure;
                    _cmdDelete.Connection = _con;

                    // add parameter
                    foreach (var item in parameters)
                    {
                        _cmdDelete.Parameters.Add(item);
                    }

                    // open connection, execute command, close connection
                    _con.Open();
                    _cmdDelete.ExecuteNonQuery();
                    _con.Close();
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public static void fnUpdDel(string procedure, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(GetConnectionStringByName("conn")))
                using (SqlCommand _cmdDelete = new SqlCommand())
                {
                    _cmdDelete.CommandType = CommandType.StoredProcedure;
                    _cmdDelete.CommandText = procedure;
                    _cmdDelete.Connection = _con;

                    // add parameter
                    foreach (var item in parameters)
                    {
                        _cmdDelete.Parameters.Add(item);
                    }

                    // open connection, execute command, close connection
                    _con.Open();
                    _cmdDelete.ExecuteNonQuery();
                    _con.Close();
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public static bool IsFormOpen(Type t)
        {
            if (!t.IsSubclassOf(typeof(Form)) && !(t == typeof(Form)))
                throw new ArgumentException("Type is not a form", "t");
            try
            {
                for (int i1 = 0; i1 < Application.OpenForms.Count; i1++)
                {
                    Form f = Application.OpenForms[i1];
                    if (t.IsInstanceOfType(f))
                        return true;
                }
            }
            catch (IndexOutOfRangeException)
            {
                //This can change if they close/open a form while code is running. Just throw it away
            }
            return false;
        }

        public static void GetDataProcedure(string selectProcedure,DataGridView dgv,string strCon)
        {

        }

        public static void fnDatagridClickCell(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgv.Rows[e.RowIndex];
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            row.Selected = true;
        }

        public static void fnFormatDatagridview(DataGridView dgv, byte fontsize)
        {
            // Hide row header
            dgv.RowHeadersVisible = false;

            // Hide horizontal scrollbar
            dgv.ScrollBars = ScrollBars.Vertical;

            // Align content to center of cell/column
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // Set the selection background color for all the cells.
            dgv.DefaultCellStyle.SelectionBackColor = Color.White;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            ////dgv.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            ////dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;

            // Set the row and column header styles.
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.Black;

            // Clear selection
            dgv.ClearSelection();

            // Set font and fontsize
            dgv.DefaultCellStyle.Font = new Font("Times New Roman", fontsize);

            //using (Font font = new Font(dgv.DefaultCellStyle.Font.FontFamily, fontsize, FontStyle.Regular))
            //{
            //    //dgvCodeDefine.Columns["Rating"].DefaultCellStyle.Font = font;
            //    dgv.DefaultCellStyle.Font = font;
            //}

        }

        public class MyDataGrid : DataGrid
        {

            public MyDataGrid()
            {
                //make scrollbar visible & hook up handler
                this.VertScrollBar.Visible = true;
                this.VertScrollBar.VisibleChanged += new EventHandler(ShowScrollBars);
            }

            private int CAPTIONHEIGHT = 21;
            private int BORDERWIDTH = 2;

            private void ShowScrollBars(object sender, EventArgs e)
            {
                if (!this.VertScrollBar.Visible)
                {
                    int width = this.VertScrollBar.Width;
                    this.VertScrollBar.Location = new Point(this.ClientRectangle.Width - width - BORDERWIDTH, CAPTIONHEIGHT);
                    this.VertScrollBar.Size = new Size(width, this.ClientRectangle.Height - CAPTIONHEIGHT - BORDERWIDTH);
                    this.VertScrollBar.Show();
                }
            }
        }

        public static int fnGetDataGridWidth(DataGridView dgv)
        {
            int dgvWidth = 0;
            dgvWidth = ((dgv.ScrollBars & ScrollBars.Vertical) != ScrollBars.None) ? dgv.Width : dgv.Width - 20;
            return dgvWidth;
        }

        public static void GetData(string selectCommand, DataGridView dgv, BindingSource bindingSource, SqlDataAdapter dataAdapter)
        {
            try
            {
                // Specify a connection string. Replace the given value with a 
                // valid connection string for a Northwind SQL Server sample
                // database accessible to your system.
                ////String connectionString = GetConnectionStringByName("conn");
                String connectionString = getConnectionString();

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. These are used to
                // update the database.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Please check connection string again.");
            }
        }

        /// <summary>
        /// creates and open a sqlconnection
        /// </summary>
        /// <param name="connectionString">
        /// A <see cref="System.String"/> that contains the sql connectin parameters
        /// </param>
        /// <returns>
        /// A <see cref="SqlConnection"/> 
        /// </returns>
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            //catch (SqlException ex)
            catch
            {
                //ex should be written into a error log

                // dispose of the connection to avoid connections leak
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
            return connection;
        }

        /// <summary>
        /// Creates a sqlcommand
        /// </summary>
        /// <param name="connection">
        /// A <see cref="SqlConnection"/>
        /// </param>
        /// <param name="commandText">
        /// A <see cref="System.String"/> of the sql query.
        /// </param>
        /// <param name="commandType">
        /// A <see cref="CommandType"/> of the query type.
        /// </param>
        /// <returns>
        /// A <see cref="SqlCommand"/>
        /// </returns>
        public static SqlCommand GetCommand(this SqlConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandTimeout = connection.ConnectionTimeout;
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        /// <summary>
        /// Adds a parameter to the command parameter array.
        /// </summary>
        /// <param name="command">
        /// A <see cref="SqlCommand"/> 
        /// </param>
        /// <param name="parameterName">
        /// A <see cref="System.String"/> of the named parameter in the sql query.
        /// </param>
        /// <param name="parameterValue">
        /// A <see cref="System.Object"/> of the parameter value.
        /// </param>
        /// <param name="parameterSqlType">
        /// A <see cref="SqlDbType"/>
        /// </param>
        public static void AddParameter(this SqlCommand command, string parameterName, object parameterValue, SqlDbType parameterSqlType)
        {
            if (!parameterName.StartsWith("@"))
            {
                parameterName = "@" + parameterName;
            }
            command.Parameters.Add(parameterName, parameterSqlType);
            command.Parameters[parameterName].Value = parameterValue;
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "abc!123123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "abc!123123";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string Left(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth)
            {
                TextLenth = Text.Length;
            }
            ConvertText = Text.Substring(0, TextLenth);
            return ConvertText;
        }

        public static string Right(string Text, int TextLenth)
        {
            string ConvertText;
            if (Text.Length < TextLenth)
            {
                TextLenth = Text.Length;
            }
            ConvertText = Text.Substring(Text.Length - TextLenth, TextLenth);
            return ConvertText;
        }

        public static string Mid(string Text, int Startint, int Endint)
        {
            string ConvertText;
            if (Startint < Text.Length || Endint < Text.Length)
            {
                ConvertText = Text.Substring(Startint, Endint);
                return ConvertText;
            }
            else
                return Text;
        }

        public static void SerialPortOpen(SerialPort srp, string portname)
        {
            srp.Close();
            srp.PortName = portname;
            srp.BaudRate = 38400;
            srp.DataBits = 8;
            srp.Parity = Parity.None;
            srp.StopBits = StopBits.One;
            srp.Open();
        }

        public static bool IsNumeric(this string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        public static string appName()
        {
            string _appname = "";
            return _appname = Application.ProductName;
        }
    }

    public static class Helper
    {
        //public virtual void Button1_Click(object sender, EventArgs args)
        //{
        //    // get the connection
        //    using (SqlConnection connection = Helper.GetConnection("Pooling=true;Min Pool Size=5;Max Pool Size=40;Connect Timeout=10;server=server\instance;database=mydatabase;Integrated Security=false;User Id=username;Password=password;"))
        //    {
        //        //create the command	
        //        using (SqlCommand command = connection.GetCommand("SELECT textBox1 = @textBox1 FROM dbo.table1 WHERE textBox2 = @textBox2", CommandType.Text))
        //        {
        //            // add the parameter
        //            command.AddParameter("@textBox1", TextBox1.Text, SqlDbType.VarChar);
        //            command.AddParameter("@textBox2", TextBox2.Text, SqlDbType.VarChar);

        //            // initialize the reader and execute the command 
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {

        //                if (!reader.HasRows)
        //                {
        //                    reader.Close();
        //                    command.CommandText = "INSERT INTO dbo.table1 (textBox1, textBox2) VALUES (@textBox1, @textBox2)";
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }

        //        //create the command
        //        using (SqlCommand command = connection.GetCommand("SELECT * FROM dbo.table1 WHERE textBox1 = @textBox1", CommandType.Text))
        //        {
        //            //add the parameters
        //            command.AddParameter("@textBox1", TextBox1.Text, SqlDbType.VarChar);

        //            // initialize the reader and execute the command 
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                Label1.Text = Convert.ToString(reader["textBox2"]);
        //            }
        //        }
        //    }
        //}
    }

    //public static class Ini
    //{
    //    //ini 파일 위치
    //    public static string iniPath;

    //    static bool factory_index;

    //    /// <summary>
    //    /// Ini 클래스의 생성자
    //    /// </summary>
    //    /// <param name="path">ini 파일의 위치</param>
    //    public static Ini(string path)
    //    {
    //        // TODO: Complete member initialization
    //        iniPath = path;
    //    }

    //    [DllImport("kernel32.dll")]
    //    //ini 파일 읽기
    //    private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filepath);
    //    [DllImport("kernel32.dll")]
    //    //ini 파일 쓰기
    //    private static extern int WritePrivateProfileString(String section, String key, String val, String filepath);

    //    //ini 파일 유무
    //    /// <summary>
    //    /// ini 파일의 유무를 check
    //    /// </summary>
    //    /// <returns>존재 시 true, 없을 경우 false</returns>
    //    public static bool IniExists()
    //    {
    //        factory_index = File.Exists(iniPath);

    //        return factory_index;
    //    }

    //    /// <summary>
    //    /// ini 파일 생성
    //    /// </summary>
    //    public static void CreateIni()
    //    {
    //        File.Create(iniPath).Close();
    //    }

    //    /// <summary>
    //    /// ini 파일에서 값을 읽어옴
    //    /// </summary>
    //    /// <param name="section">ini 파일의 섹션</param>
    //    /// <param name="key">ini 파일의 키</param>
    //    /// <returns>해당 [섹션, 키]의 값을 반환</returns>
    //    public static string GetIniValue(string section, string key, string msg = "")
    //    {
    //        StringBuilder result = new StringBuilder(255);
    //        int i = GetPrivateProfileString(section, key, msg, result, 255, iniPath);
    //        return result.ToString();
    //    }

    //    /// <summary>
    //    /// ini 파일에 값을 씀
    //    /// </summary>
    //    /// <param name="section">섹션</param>
    //    /// <param name="key">키</param>
    //    /// <param name="val">값</param>
    //    public static void SetIniValue(string section, string key, string val)
    //    {
    //        WritePrivateProfileString(section, key, val, iniPath);
    //    }
    //}

    public static class check
    {
        //Creating the extern function...
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);

        }

        public static bool IsConnectedToLAN(string ip)
        {
            Ping x = new Ping();
            PingReply reply = x.Send(IPAddress.Parse(ip));

            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
