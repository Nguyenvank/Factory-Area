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
    public partial class frmDataPLC : Form
    {
        public frmDataPLC()
        {
            InitializeComponent();
        }

        private void frmDataPLC_Load(object sender, EventArgs e)
        {
            init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fnGetDate();
            fnGetDateFromPLC();
        }

        private void AB01_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromAutoBalance01();
            //fnUpdateData("Auto Balance-1", "OK", txtAB01OK.Text.Trim());
            //fnUpdateData("Auto Balance-1", "NG", txtAB01OK.Text.Trim());
        }

        private void AB02_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromAutoBalance02();
            //fnUpdateData("Auto Balance-2", "OK", txtAB02OK.Text.Trim());
            //fnUpdateData("Auto Balance-2", "NG", txtAB02NG.Text.Trim());
        }

        private void AB03_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromAutoBalance03();
            //fnUpdateData("Auto Balance-3", "OK", txtAB03OK.Text.Trim());
            //fnUpdateData("Auto Balance-3", "NG", txtAB03NG.Text.Trim());
        }

        private void WB01_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromWeightBalance01();
            //fnUpdateData("Weight Balance-1", "OK", txtWB01OK.Text.Trim());
            //fnUpdateData("Weight Balance-1", "NG", txtWB01NG.Text.Trim());
        }

        private void WB02_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromWeightBalance02();
            //fnUpdateData("Weight Balance-2", "OK", txtWB02OK.Text.Trim());
            //fnUpdateData("Weight Balance-2", "NG", txtWB02NG.Text.Trim());
        }

        private void WB03_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromWeightBalance03();
            //fnUpdateData("Weight Balance-3", "OK", txtWB03OK.Text.Trim());
            //fnUpdateData("Weight Balance-3", "NG", txtWB03NG.Text.Trim());

        }

        private void DI01_Tick(object sender, EventArgs e)
        {
            fnGetDataFromDispenser01();
            //fnUpdateData("Dispenser-1", "OK", txtDI01OK.Text.Trim());
            //fnUpdateData("Dispenser-1", "NG", txtDI01NG.Text.Trim());
        }

        private void DI02_Tick(object sender, EventArgs e)
        {
            fnGetDataFromDispenser02();
            //fnUpdateData("Dispenser-2", "OK", txtDI02OK.Text.Trim());
            //fnUpdateData("Dispenser-2", "NG", txtDI02NG.Text.Trim());
        }

        private void PU01_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromPump01();
            //fnUpdateData("Pump-1", "OK", txtPU01OK.Text.Trim());
            //fnUpdateData("Pump-1", "NG", txtPU01NG.Text.Trim());
        }

        private void PU02_Tick(object sender, EventArgs e)
        {
            //fnGetDataFromPump02();
            //fnUpdateData("Pump-2", "OK", txtPU02OK.Text.Trim());
            //fnUpdateData("Pump-2", "NG", txtPU02NG.Text.Trim());
        }
        public void init()
        {
            fnGetDate();
            fnGetDateFromPLC();
        }

        public void fnGetDate()
        {
            lblDate.Text = cls.fnGetDate("SD");
            lblTime.Text = cls.fnGetDate("CT");
        }

        public void fnGetDateFromPLC()
        {
            //fnGetDataFromAutoBalance01();
            //fnGetDataFromAutoBalance02();
            //fnGetDataFromAutoBalance03();

            //fnGetDataFromDispenser01();
            //fnGetDataFromDispenser02();

            //fnGetDataFromWeightBalance01();
            //fnGetDataFromWeightBalance02();
            //fnGetDataFromWeightBalance03();

            //fnGetDataFromPump01();
            //fnGetDataFromPump02();

            //fnGetDataFromPLC("autobalance1.ok", "autobalance1.ng", txtAB01OK, txtAB01NG);
            //fnGetDataFromPLC("autobalance2.ok", "autobalance2.ng", txtAB02OK, txtAB02NG);
            //fnGetDataFromPLC("autobalance3.ok", "autobalance3.ng", txtAB03OK, txtAB03NG);

            fnGetDataFromDispenser01();
            fnGetDataFromDispenser02();

            //fnGetDataFromPLC("weightbalance1.ok", "weightbalance1.ng", txtWB01OK, txtWB01NG);
            //fnGetDataFromPLC("weightbalance2.ok", "weightbalance2.ng", txtWB02OK, txtWB02NG);
            //fnGetDataFromPLC("weightbalance3.ok", "weightbalance3.ng", txtWB03OK, txtWB03NG);

            //fnGetDataFromPLC("pump1.ok", "pump1.ng", txtPU01OK, txtPU01NG);
            //fnGetDataFromPLC("pump2.ok", "pump2.ng", txtPU02OK, txtPU02NG);

            //fnGetDataFromPLC("welding1.ok", "welding1.ng", txtWD01OK, txtWD01NG);
            //fnGetDataFromPLC("welding2.ok", "welding2.ng", txtWD02OK, txtWD02NG);

            //fnGetDataFromPLC("mixing1.ok", "mixing1.ng", txtMX01OK, txtMX01NG);

            //fnGetDataFromPLC("blower1.ok", "blower1.ng", txtBL01OK, txtBL01NG);
            //fnGetDataFromPLC("blower2.ok", "blower2.ng", txtBL02OK, txtBL02NG);
        }

        //public void fnGetDataFromAutoBalance01()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='autobalance1.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='autobalance1.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtAB01OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtAB01NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        //public void fnGetDataFromAutoBalance02()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='autobalance2.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='autobalance2.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtAB02OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtAB02NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        //public void fnGetDataFromAutoBalance03()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='autobalance3.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='autobalance3.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtAB03OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtAB03NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        public void fnGetDataFromDispenser01()
        {
            string OK = "", NG = "";
            string sqlOK = "", sqlNG = "";

            sqlOK = "SELECT sum(achieveOK) FROM [inFlow].[dbo].[BASE_CapacityModelOrder2] where DATEDIFF(DAY,orderDate,GETDATE())=0 and orderLine='DISPENSER 01' group by orderDate,orderLine";
            sqlNG = "SELECT sum(achieveNG) FROM [inFlow].[dbo].[BASE_CapacityModelOrder2] where DATEDIFF(DAY,orderDate,GETDATE())=0 and orderLine='DISPENSER 01' group by orderDate,orderLine";

            DataSet dsOK = new DataSet();
            DataSet dsNG = new DataSet();

            dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "conn");
            dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "conn");

            txtDI01OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
            txtDI01NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        }

        public void fnGetDataFromDispenser02()
        {
            string OK = "", NG = "";
            string sqlOK = "", sqlNG = "";

            sqlOK = "SELECT sum(achieveOK) FROM [inFlow].[dbo].[BASE_CapacityModelOrder2] where DATEDIFF(DAY,orderDate,GETDATE())=0 and orderLine='DISPENSER 02' group by orderDate,orderLine";
            sqlNG = "SELECT sum(achieveNG) FROM [inFlow].[dbo].[BASE_CapacityModelOrder2] where DATEDIFF(DAY,orderDate,GETDATE())=0 and orderLine='DISPENSER 02' group by orderDate,orderLine";

            DataSet dsOK = new DataSet();
            DataSet dsNG = new DataSet();

            dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "conn");
            dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "conn");

            txtDI02OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
            txtDI02NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        }

        //public void fnGetDataFromWeightBalance01()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='weightbalance1.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='weightbalance1.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtWB01OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtWB01NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        //public void fnGetDataFromWeightBalance02()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='weightbalance2.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='weightbalance2.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtWB02OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtWB02NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        //public void fnGetDataFromWeightBalance03()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='weightbalance3.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='weightbalance3.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtWB03OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtWB03NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        //public void fnGetDataFromPump01()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='pump1.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='pump1.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtPU01OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtPU01NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        //public void fnGetDataFromPump02()
        //{
        //    string OK = "", NG = "";
        //    string sqlOK = "", sqlNG = "";

        //    sqlOK = "select top 1 VarValue from Data_log_10 where VarName='pump2.ok' order by TimeString desc";
        //    sqlNG = "select top 1 VarValue from Data_log_10 where VarName='pump2.ng' order by TimeString desc";

        //    DataSet dsOK = new DataSet();
        //    DataSet dsNG = new DataSet();

        //    dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
        //    dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

        //    txtPU02OK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
        //    txtPU02NG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        //}

        public void fnGetDataFromPLC(string fieldOK,string fieldNG,TextBox txtOK,TextBox txtNG)
        {
            string OK = "", NG = "";
            string sqlOK = "", sqlNG = "";

            sqlOK = "select top 1 VarValue from Data_log_10 where VarName='" + fieldOK + "' order by TimeString desc";
            sqlNG = "select top 1 VarValue from Data_log_10 where VarName='" + fieldNG + "' order by TimeString desc";

            DataSet dsOK = new DataSet();
            DataSet dsNG = new DataSet();

            dsOK = cls.ExecuteDataSet(sqlOK, CommandType.Text, "connPLC");
            dsNG = cls.ExecuteDataSet(sqlNG, CommandType.Text, "connPLC");

            txtOK.Text = (dsOK.Tables[0].Rows.Count > 0) ? dsOK.Tables[0].Rows[0][0].ToString() : "0";
            txtNG.Text = (dsNG.Tables[0].Rows.Count > 0) ? dsNG.Tables[0].Rows[0][0].ToString() : "0";
        }

        //public void fnUpdateData(string prodId,string name,string line,string type, string value)
        public void fnUpdateData(string lineID,string line,string type, string valueOK, string valueNG)
        {
            string shift = cls.fnGetDate("s").ToUpper();
            int chk = line.IndexOf("-");
            string name = line.Substring(0, chk);
            string pos = line.Substring(chk + 1);
            //string lineId = "0";


            string sql = "";
            sql = "V2_BASE_CAPACITY_GET_INS_PLC_DATA_ADDNEW";
            SqlParameter[] sParams = new SqlParameter[5]; // Parameter count

            sParams[0] = new SqlParameter();
            sParams[0].SqlDbType = SqlDbType.VarChar;
            sParams[0].ParameterName = "assyShift";
            sParams[0].Value = shift;

            sParams[1] = new SqlParameter();
            sParams[1].SqlDbType = SqlDbType.Int;
            sParams[1].ParameterName = "lineId";
            sParams[1].Value = lineID;

            sParams[2] = new SqlParameter();
            sParams[2].SqlDbType = SqlDbType.VarChar;
            sParams[2].ParameterName = "assyLine";
            sParams[2].Value = name + " 0" + pos;

            sParams[3] = new SqlParameter();
            sParams[3].SqlDbType = SqlDbType.Int;
            sParams[3].ParameterName = "valueOK";
            sParams[3].Value = valueOK;

            sParams[4] = new SqlParameter();
            sParams[4].SqlDbType = SqlDbType.Int;
            sParams[4].ParameterName = "valueNG";
            sParams[4].Value = valueNG;

            cls.fnUpdDel(sql, sParams);

            lblMessage.ForeColor = (type == "OK") ? Color.Blue : Color.Red;
            lblMessage.Text = "Update data for line " + name + " 0" + pos + " successfull at " + DateTime.Now + ".";
        }

        private void txtAB01OK_TextChanged(object sender, EventArgs e)
        {

            //fnUpdateData("1", "Auto Balance-1", "OK", txtAB01OK.Text.Trim(), txtAB01NG.Text.Trim());
        }

        private void txtAB01NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("1", "Auto Balance-1", "NG", txtAB01OK.Text.Trim(), txtAB01NG.Text.Trim());
        }

        private void txtAB02OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("2", "Auto Balance-2", "OK", txtAB02OK.Text.Trim(), txtAB02NG.Text.Trim());
        }

        private void txtAB02NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("2", "Auto Balance-2", "NG", txtAB02OK.Text.Trim(), txtAB02NG.Text.Trim());
        }

        private void txtAB03OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("3", "Auto Balance-3", "OK", txtAB03OK.Text.Trim(), txtAB03NG.Text.Trim());
        }

        private void txtAB03NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("3", "Auto Balance-3", "NG", txtAB03OK.Text.Trim(), txtAB03NG.Text.Trim());
        }

        private void txtDI01OK_TextChanged(object sender, EventArgs e)
        {
            fnUpdateData("4", "Dispenser-1", "OK", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
        }

        private void txtDI01NG_TextChanged(object sender, EventArgs e)
        {
            fnUpdateData("4", "Dispenser-1", "NG", txtDI01OK.Text.Trim(), txtDI01NG.Text.Trim());
        }

        private void txtDI02OK_TextChanged(object sender, EventArgs e)
        {
            fnUpdateData("5", "Dispenser-2", "OK", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
        }

        private void txtDI02NG_TextChanged(object sender, EventArgs e)
        {
            fnUpdateData("5", "Dispenser-2", "NG", txtDI02OK.Text.Trim(), txtDI02NG.Text.Trim());
        }

        private void txtWB01OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("7", "Weight Balance-1", "OK", txtWB01OK.Text.Trim(), txtWB01NG.Text.Trim());
        }

        private void txtWB01NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("7", "Weight Balance-1", "NG", txtWB01OK.Text.Trim(), txtWB01NG.Text.Trim());
        }

        private void txtWB02OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("8", "Weight Balance-2", "OK", txtWB02OK.Text.Trim(), txtWB02NG.Text.Trim());
        }

        private void txtWB02NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("8", "Weight Balance-2", "NG", txtWB02OK.Text.Trim(), txtWB02NG.Text.Trim());
        }

        private void txtWB03OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("9", "Weight Balance-3", "OK", txtWB03OK.Text.Trim(), txtWB03NG.Text.Trim());
        }

        private void txtWB03NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("9", "Weight Balance-3", "NG", txtWB03OK.Text.Trim(), txtWB03NG.Text.Trim());
        }

        private void txtPU01OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("6", "Pump-1", "OK", txtPU01OK.Text.Trim(), txtPU01NG.Text.Trim());
        }

        private void txtPU01NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("6", "Pump-1", "NG", txtPU01OK.Text.Trim(), txtPU01NG.Text.Trim());
        }

        private void txtPU02OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("0", "Pump-2", "OK", txtPU02OK.Text.Trim(), txtPU02NG.Text.Trim());
        }

        private void txtPU02NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("0", "Pump-2", "NG", txtPU02OK.Text.Trim(), txtPU02NG.Text.Trim());
        }

        private void txtWD01OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("13", "Welding-1", "OK", txtWD01OK.Text.Trim(), txtWD01NG.Text.Trim());
        }

        private void txtWD01NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("13", "Welding-1", "NG", txtWD01OK.Text.Trim(), txtWD01NG.Text.Trim());
        }

        private void txtWD02OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("14", "Welding-2", "OK", txtWD02OK.Text.Trim(), txtWD02NG.Text.Trim());
        }

        private void txtWD02NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("14", "Welding-2", "NG", txtWD02OK.Text.Trim(), txtWD02NG.Text.Trim());
        }

        private void txtMX01OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("15", "Mixing-1", "OK", txtMX01OK.Text.Trim(), txtMX01NG.Text.Trim());
        }

        private void txtMX01NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("15", "Mixing-1", "NG", txtMX01OK.Text.Trim(), txtMX01NG.Text.Trim());
        }

        private void txtBL01OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("11", "Blower-1", "OK", txtBL01OK.Text.Trim(), txtBL01NG.Text.Trim());
        }

        private void txtBL01NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("11", "Blower-1", "NG", txtBL01OK.Text.Trim(), txtBL01NG.Text.Trim());
        }

        private void txtBL02OK_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("12", "Blower-2", "OK", txtBL02OK.Text.Trim(), txtBL02NG.Text.Trim());
        }

        private void txtBL02NG_TextChanged(object sender, EventArgs e)
        {
            //fnUpdateData("12", "Blower-2", "OK", txtBL02OK.Text.Trim(), txtBL02NG.Text.Trim());
        }
    }
}
