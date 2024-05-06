using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_ERP
{
    public partial class employeeForm : Form
    {
        string
            __sql = "",
            __msg = "";

        SqlParameter[]
            __sparam = null;

        DataTable
            __dt = null,
            __dt_mat = null;

        DataSet
            __ds = null;

        int
            __tbl_cnt = 0,
            __row_cnt = 0,
            __col_cnt = 0;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          // try
        //    {
              //  string connectionString = "localhost"; // Replace with your database connection string

              //  using (SqlConnection connection = new SqlConnection(connectionString))
              //  {
                 //   connection.Open();

               //     string sqlQuery = "SELECT * FROM da_employee";

                   // using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                  //  {
                  //      using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                   //     {
                 //           DataTable dataTable = new DataTable();
                 //           adapter.Fill(dataTable);

             //           }
              //      }
          //      }
          //  }
            //catch (Exception ex)
           // {
               // Console.WriteLine("An error occurred: " + ex.Message);
          //  }
           // finally
           // {
                // You can put cleanup code here if needed.
           // }
        }

        Color[]
            __color = { Color.White, Color.LightGreen, Color.LightPink, Color.Gainsboro, Color.Yellow, Color.Gold, Color.FromKnownColor(KnownColor.Control) };

        public employeeForm()
        {
            InitializeComponent();
        }
    }
}