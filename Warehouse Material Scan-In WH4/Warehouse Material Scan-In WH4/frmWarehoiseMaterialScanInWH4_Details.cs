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

namespace Inventory_Data
{
    public partial class frmWarehoiseMaterialScanInWH4_Details : Form
    {
        public string _inoutIDx = "";


        public frmWarehoiseMaterialScanInWH4_Details(string inoutIDx)
        {
            InitializeComponent();

            _inoutIDx = inoutIDx;
        }

        private void frmWarehoiseMaterialScanInWH4_Details_Load(object sender, EventArgs e)
        {
            init();
        }

        public void init()
        {
            string inoutIDx = _inoutIDx;

            string packingCode = "", partName = "", partNumber = "", uom = "", packingQty = "", packingUnit = "", total = "";
            string dateMFG = "", dateEXP = "", importLOT = "", receiveDate = "", location = "", sublocate = "";
            string instockDate = "", inDate = "", outStock = "", outDate = "", outOrderCode = "", receiver = "";
            DateTime _dateMFG, _dateEXP, _receiveDate, _instockDate, _inDate, _outDate;

            string sql = "V2o1_BASE_Warehouse_Material_ScanIn_SelItem_Details_Addnew";

            SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.Int;
            sParams[0].ParameterName = "@inoutIDx";
            sParams[0].Value = inoutIDx;

            DataSet ds = new DataSet();
            ds = cls.ExecuteDataSet(sql, sParams);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                packingCode = ds.Tables[0].Rows[0][0].ToString();
                partName = ds.Tables[0].Rows[0][1].ToString();
                partNumber = ds.Tables[0].Rows[0][2].ToString();
                uom = ds.Tables[0].Rows[0][3].ToString();
                packingQty = ds.Tables[0].Rows[0][4].ToString();
                packingUnit = ds.Tables[0].Rows[0][5].ToString();
                total = ds.Tables[0].Rows[0][6].ToString();
                dateMFG = ds.Tables[0].Rows[0][7].ToString();
                dateEXP = ds.Tables[0].Rows[0][8].ToString();
                importLOT = ds.Tables[0].Rows[0][9].ToString();
                receiveDate = ds.Tables[0].Rows[0][10].ToString();
                location = ds.Tables[0].Rows[0][11].ToString();
                sublocate = ds.Tables[0].Rows[0][12].ToString();
                instockDate = ds.Tables[0].Rows[0][13].ToString();
                inDate = ds.Tables[0].Rows[0][14].ToString();
                outStock = ds.Tables[0].Rows[0][15].ToString();
                outDate = ds.Tables[0].Rows[0][16].ToString();
                outOrderCode = ds.Tables[0].Rows[0][17].ToString();
                receiver = ds.Tables[0].Rows[0][18].ToString();
            }
            else
            {
                packingCode = "";
                partName = "";
                partNumber = "";
                uom = "";
                packingQty = "";
                packingUnit = "";
                total = "";
                dateMFG = "";
                dateEXP = "";
                importLOT = "";
                receiveDate = "";
                location = "";
                sublocate = "";
                instockDate = "";
                inDate = "";
                outStock = "";
                outDate = "";
                outOrderCode = "";
                receiver = "";
            }

            _dateMFG = (dateMFG != "" && dateMFG != null) ? Convert.ToDateTime(dateMFG) : DateTime.Now;
            _dateEXP = (dateEXP != "" && dateEXP != null) ? Convert.ToDateTime(dateEXP) : DateTime.Now;
            _receiveDate = (receiveDate != "" && receiveDate != null) ? Convert.ToDateTime(receiveDate) : DateTime.Now;
            _instockDate = (instockDate != "" && instockDate != null) ? Convert.ToDateTime(instockDate) : DateTime.Now;
            _inDate = (inDate != "" && inDate != null) ? Convert.ToDateTime(inDate) : DateTime.Now;
            _outDate = (outDate != "" && outDate != null) ? Convert.ToDateTime(outDate) : DateTime.Now;

            lblPackingCode.Text = packingCode;
            lblPartname.Text = partName;
            lblPartnumber.Text = partNumber;
            lblLocation.Text = location.ToUpper();
            lblSublocate.Text = sublocate.ToUpper();
            lblPackingQty.Text = packingQty;
            lblTotalQty.Text = total;
            lblLOTnumber.Text = importLOT;
            lblInstockDate.Text = (instockDate != "" && instockDate != null) ? String.Format("{0:dd/MM/yyyy HH:mm:ss}", _instockDate) : "";
            lblDateMFG.Text = (dateMFG != "" && dateMFG != null) ? String.Format("{0:dd/MM/yyyy}", _dateMFG) : "";
            lblDateEXP.Text = (dateEXP != "" && dateEXP != null) ? String.Format("{0:dd/MM/yyyy}", _dateEXP) : "";
        }
    }
}
