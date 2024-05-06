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
    public partial class frmSub01CreateMaterial_VendorDetails : Form
    {
        public string _vendorIDx = "";

        public frmSub01CreateMaterial_VendorDetails(string vendorIDx)
        {
            InitializeComponent();
            _vendorIDx = vendorIDx;
        }

        private void frmSub01CreateMaterial_VendorDetails_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetdate();
        }

        public void init()
        {
            fnGetdate();
            initVendor_Info();
        }

        public void fnGetdate()
        {

        }

        public void initVendor_Info()
        {
            string vendorIDx = _vendorIDx;
            string vendorName = "", vendorAdd = "", vendorPhone = "", vendorFax = "", vendorEmail = "", vendorWebsite = "", vendorRemark = "";
            string sql = "V2o1_BASE_SubMaterial_Init_Vendor_Detail_Info_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@vendorIDx";
            sParams[0].Value = vendorIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables[0].Rows.Count > 0)
            {
                vendorName = ds.Tables[0].Rows[0][0].ToString();
                vendorAdd = ds.Tables[0].Rows[0][1].ToString();
                vendorPhone = ds.Tables[0].Rows[0][2].ToString();
                vendorFax = ds.Tables[0].Rows[0][3].ToString();
                vendorEmail = ds.Tables[0].Rows[0][4].ToString();
                vendorWebsite = ds.Tables[0].Rows[0][5].ToString();
                vendorRemark = ds.Tables[0].Rows[0][6].ToString();
            }
            else
            {
                vendorName = "";
                vendorAdd = "";
                vendorPhone = "";
                vendorFax = "";
                vendorEmail = "";
                vendorWebsite = "";
                vendorRemark = "";
            }

            lblVendor_Name.Text = (vendorName != "" && vendorName != null) ? vendorName : "N/A";
            lblVendor_Add.Text = (vendorAdd != "" && vendorAdd != null) ? vendorAdd : "N/A";
            lblVendor_Phone.Text = (vendorPhone != "" && vendorPhone != null) ? vendorPhone : "N/A";
            lblVendor_Fax.Text = (vendorFax != "" && vendorFax != null) ? vendorFax : "N/A";
            lblVendor_Email.Text = (vendorEmail != "" && vendorEmail != null) ? vendorEmail : "N/A";
            lblVendor_Website.Text = (vendorWebsite != "" && vendorWebsite != null) ? vendorWebsite : "N/A";
            lblVendor_Remark.Text = (vendorRemark != "" && vendorRemark != null) ? vendorRemark : "N/A";

        }

        public void initVendor_PurchaseHist()
        {
            string vendorIDx = _vendorIDx;
            string sql = "V2o1_BASE_SubMaterial_Init_Vendor_Detail_Purchased_SelItem_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@vendorIDx";
            sParams[0].Value = vendorIDx;


        }
    }
}
