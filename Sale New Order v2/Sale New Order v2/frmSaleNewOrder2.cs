using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory_Data
{
    public partial class frmSaleNewOrder2 : Form
    {
        public string _orderID;
        public string _orderCode;
        public string _orderDate;
        public string _itemID;
        public string _partID;
        public int _total;
        public int _quantityDA;
        public int _remain;
        public string _shipto;
        public int _delivery;
        public int _dgvOrderWidth = 0;
        public int _dgvSequenceWidth = 0;
        public int _dgvListWidth = 0;
        public int _dgvSequenceNumber;
        public int _dgvListNumber;

        public frmSaleNewOrder2()
        {
            InitializeComponent();

            this._dgvOrderWidth = cls.fnGetDataGridWidth(this.dgvOrder);
            this._dgvSequenceWidth = cls.fnGetDataGridWidth(this.dgvSequence);
            this._dgvListWidth = cls.fnGetDataGridWidth(dgvList);
        }

        private void frmSaleNewOrder2_Load(object sender, EventArgs e)
        {
            this.init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.fnGetDate();

            this._dgvOrderWidth = cls.fnGetDataGridWidth(this.dgvOrder);
            this._dgvSequenceWidth = cls.fnGetDataGridWidth(this.dgvSequence);
            this._dgvListWidth = cls.fnGetDataGridWidth(dgvList);

            int dgvSequenceNumber = getCount(this.fnReturnCheckSequenceSQL());
            bool flag = this._dgvSequenceNumber != dgvSequenceNumber;
            if (flag)
            {
                this.fnBindDataSequence();
                this._dgvSequenceNumber = dgvSequenceNumber;
            }
            int dgvListNumber = getCount(this.fnReturnCheckListSQL());
            bool flag2 = this._dgvListNumber != dgvListNumber;
            if (flag2)
            {
                this.fnBindDataList();
                this._dgvListNumber = dgvListNumber;
            }
        }


        public void init()
        {
            this.fnGetDate();
            this.fnInitData();
            this.fnBindData();
            this.fnCheckOrderNumber();
            this._dgvSequenceNumber = getCount(this.fnReturnCheckSequenceSQL());
            this._dgvListNumber = getCount(this.fnReturnCheckListSQL());
            this.chkDeliveryOK.Enabled = false;
            this.txtDeliveryOKReason.Enabled = false;
        }

        public string fnReturnCheckOrderSQL()
        {
            string doDate = this._orderDate;
            bool flag = this._orderID == "" || this._orderID == null;
            string sql;
            if (flag)
            {
                sql = "SELECT idx as [idx], doID as [OID], partID as [PID], partname as [Partname], partcode as [Partcode], doQuantity as [Request], shipTo as [Shipment], doUnit as [Unit], doTotal as [Delivery], doStockLG as [StockLG], doStockDA as [StockDA], doPlanToday as [Plan], doPlanTomorrow as [Plan+1], doRemain as [Remain], doFinish as [Finish], finishNote as [Reason], itemStatus as [Status] ";
                sql += "FROM dbo.DO_DeliveryOrderItems ";
                sql = sql + "WHERE doID=(select doID from DO_DeliveryOrder where datediff(day,doDate,cast('" + doDate + "' as datetime))=0)";
            }
            else
            {
                sql = "SELECT idx as [idx], doID as [OID], partID as [PID], partname as [Partname], partcode as [Partcode], doQuantity as [Request], shipTo as [Shipment], doUnit as [Unit], doTotal as [Delivery], doStockLG as [StockLG], doStockDA as [StockDA], doPlanToday as [Plan], doPlanTomorrow as [Plan+1], doRemain as [Remain], doFinish as [Finish], finishNote as [Reason], itemStatus as [Status] ";
                sql += "FROM dbo.DO_DeliveryOrderItems ";
                sql = string.Concat(new string[]
                {
                    sql,
                    "WHERE doID=(select doID from DO_DeliveryOrder where doID=",
                    this._orderID,
                    " and doCode='",
                    this._orderCode,
                    "')"
                });
            }
            return sql;
        }

        public string fnReturnCheckSequenceSQL()
        {
            string sql = "";
            sql += "SELECT dbo.BASE_Product.Name AS [Part name], dbo.BASE_Product.BarCode AS [Part number], ";
            sql += "dbo.BASE_Product.ProdSort AS [Sort],  dbo.BASE_Product.ProdId AS [ProdID]";
            sql += "FROM dbo.BASE_Product INNER JOIN dbo.BASE_Category ON dbo.BASE_Product.CategoryId = dbo.BASE_Category.CategoryId ";
            sql += "WHERE (dbo.BASE_Category.ParentCategoryId = 109) AND (dbo.BASE_Product.IsActive=1) ";
            return sql + "ORDER BY dbo.BASE_Product.ProdSort";
        }

        public string fnReturnCheckListSQL()
        {
            string sql = "SELECT doID AS [OID], doCode AS [Order code], doDate AS [Order date], doStatus AS [Order status] ";
            sql += "FROM dbo.DO_DeliveryOrder ";
            sql += "WHERE DATEDIFF(WEEK,doMake,GETDATE())>=0 AND DATEDIFF(WEEK,doMake,GETDATE())<=4 AND doStatus=1 ";
            return sql + "ORDER BY doMake DESC";
        }

        public void fnGetDate()
        {
            this.lblDateTime.Text = getProductInfo("dd/MM/yyyy HH:mm:ss tt");
        }

        public void fnInitData()
        {
            string sql = "SELECT dbo.BASE_Product.Name, dbo.BASE_Product.prodId ";
            sql += "FROM dbo.BASE_Product INNER JOIN dbo.BASE_Category ON dbo.BASE_Product.CategoryId = dbo.BASE_Category.CategoryId ";
            sql += "WHERE (dbo.BASE_Category.ParentCategoryId = 109) AND (dbo.BASE_Product.IsActive=1) ";
            sql += "AND ((dbo.BASE_Product.ProdSort<>'') OR (dbo.BASE_Product.ProdSort<>0)) ";
            sql += "ORDER BY ProdSort";
            DataTable tblPartname = new DataTable();
            tblPartname = getTable(sql);
            this.cbbPartname.DataSource = tblPartname;
            this.cbbPartname.DisplayMember = "Name";
            this.cbbPartname.ValueMember = "prodId";
            DataRow drSearchLine = tblPartname.NewRow();
            drSearchLine["Name"] = "";
            drSearchLine["prodId"] = 0;
            tblPartname.Rows.InsertAt(drSearchLine, 0);
            this.cbbPartname.SelectedIndex = 0;

            string sql_shipto = "V2o1_Sales_Order_Customers_List_SelItem_V1o0_Addnew";
            cls.Fnc_Data_To_Combobox(cbbShipTo, true, sql_shipto, "customer_nm", "customer_nm");

            //this.cbbShipTo.Items.Add("");
            //this.cbbShipTo.Items.Add("LGE");
            //this.cbbShipTo.Items.Add("Thailand");
            //this.cbbShipTo.Items.Add("Roki");
            //this.cbbShipTo.Items.Add("Sanaky");
            //this.cbbShipTo.Items.Add("VPMS");
            //this.cbbShipTo.Items.Add("Vinfast");
            //this.cbbShipTo.Items.Add("Panasonic");
            //this.cbbShipTo.Items.Add("Aqua");
            //this.cbbShipTo.Items.Add("Ariston");
            //this.cbbShipTo.Items.Add("US");
            //this.cbbShipTo.SelectedIndex = 0;

            this.lblPartnumber.Text = "N/A";
            bool flag = this._orderID != "" && this._orderID != null;
            if (flag)
            {
                this.rdbAdd.Enabled = false;
                this.rdbDel.Enabled = true;
                this.rdbUpd.Enabled = true;
                this.rdbAdd.Checked = false;
            }
            else
            {
                bool flag2 = this._itemID != "" && this._itemID != null;
                if (flag2)
                {
                    this.rdbAdd.Enabled = true;
                    this.rdbDel.Enabled = false;
                    this.rdbUpd.Enabled = true;
                    this.rdbAdd.Checked = true;
                }
                else
                {
                    this.rdbAdd.Enabled = true;
                    this.rdbDel.Enabled = false;
                    this.rdbUpd.Enabled = false;
                    this.rdbAdd.Checked = true;
                }
            }
        }

        public void fnBindData()
        {
            string sqlCheckOrderToday = "select doID from DO_DeliveryOrder where datediff(day,doDate,getdate())=0";
            bool flag = getCount(sqlCheckOrderToday) > 0;
            if (flag)
            {
            }
            this.fnBindDataSequence();
            this.fnBindDataList();
        }

        public void fnBindDataOrder()
        {
            string sql = this.fnReturnCheckOrderSQL();
            this.dgvOrder.DataSource = cls.bindingSource0;
            cls.GetData(sql, this.dgvOrder, cls.bindingSource0, cls.dataAdapter0);
            DataGridViewColumn dgvOrder_column0 = this.dgvOrder.Columns[0];
            DataGridViewColumn dgvOrder_column = this.dgvOrder.Columns[1];
            DataGridViewColumn dgvOrder_column2 = this.dgvOrder.Columns[2];
            DataGridViewColumn dgvOrder_column3 = this.dgvOrder.Columns[3];
            DataGridViewColumn dgvOrder_column4 = this.dgvOrder.Columns[4];
            DataGridViewColumn dgvOrder_column5 = this.dgvOrder.Columns[5];
            DataGridViewColumn dgvOrder_column6 = this.dgvOrder.Columns[6];
            DataGridViewColumn dgvOrder_column7 = this.dgvOrder.Columns[7];
            DataGridViewColumn dgvOrder_column8 = this.dgvOrder.Columns[8];
            DataGridViewColumn dgvOrder_column9 = this.dgvOrder.Columns[9];
            DataGridViewColumn dgvOrder_column10 = this.dgvOrder.Columns[10];
            DataGridViewColumn dgvOrder_column11 = this.dgvOrder.Columns[11];
            DataGridViewColumn dgvOrder_column12 = this.dgvOrder.Columns[12];
            DataGridViewColumn dgvOrder_column13 = this.dgvOrder.Columns[13];
            DataGridViewColumn dgvOrder_column14 = this.dgvOrder.Columns[14];
            DataGridViewColumn dgvOrder_column15 = this.dgvOrder.Columns[15];
            DataGridViewColumn dgvOrder_column16 = this.dgvOrder.Columns[16];

            this._dgvOrderWidth = cls.fnGetDataGridWidth(this.dgvOrder);

            dgvOrder_column2.Width = 5 * this._dgvOrderWidth / 100;
            dgvOrder_column3.Width = 13 * this._dgvOrderWidth / 100;
            dgvOrder_column4.Width = 10 * this._dgvOrderWidth / 100;
            dgvOrder_column5.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column6.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column7.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column9.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column11.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column12.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column14.Width = 6 * this._dgvOrderWidth / 100;
            dgvOrder_column15.Width = 30 * this._dgvOrderWidth / 100;
            this.dgvOrder.RowHeadersVisible = false;
            this.dgvOrder.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.dgvOrder.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvOrder.DefaultCellStyle.Font = new Font("Tahoma", 10f);
            this.dgvOrder.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvOrder.Columns[0].Visible = false;
            this.dgvOrder.Columns[1].Visible = false;
            this.dgvOrder.Columns[2].Visible = true;
            this.dgvOrder.Columns[3].Visible = true;
            this.dgvOrder.Columns[4].Visible = true;
            this.dgvOrder.Columns[5].Visible = true;
            this.dgvOrder.Columns[6].Visible = true;
            this.dgvOrder.Columns[7].Visible = true;
            this.dgvOrder.Columns[8].Visible = false;
            this.dgvOrder.Columns[9].Visible = true;
            this.dgvOrder.Columns[10].Visible = false;
            this.dgvOrder.Columns[11].Visible = true;
            this.dgvOrder.Columns[12].Visible = true;
            this.dgvOrder.Columns[13].Visible = false;
            this.dgvOrder.Columns[14].Visible = true;
            this.dgvOrder.Columns[15].Visible = true;
            this.dgvOrder.Columns[16].Visible = false;
        }

        public void fnBindDataSequence()
        {
            string sql = this.fnReturnCheckSequenceSQL();
            this.dgvSequence.DataSource = cls.bindingSource1;
            cls.GetData(sql, this.dgvSequence, cls.bindingSource1, cls.dataAdapter1);
            DataGridViewColumn dgvSequence_column = this.dgvSequence.Columns[0];
            DataGridViewColumn dgvSequence_column2 = this.dgvSequence.Columns[1];
            DataGridViewColumn dgvSequence_column3 = this.dgvSequence.Columns[2];
            DataGridViewColumn dgvSequence_column4 = this.dgvSequence.Columns[3];

            this._dgvSequenceWidth = cls.fnGetDataGridWidth(this.dgvSequence);

            dgvSequence_column.Width = 45 * this._dgvSequenceWidth / 100;
            dgvSequence_column2.Width = 40 * this._dgvSequenceWidth / 100;
            dgvSequence_column3.Width = 15 * this._dgvSequenceWidth / 100;
            this.dgvSequence.RowHeadersVisible = false;
            this.dgvSequence.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
            this.dgvSequence.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvSequence.DefaultCellStyle.Font = new Font("Tahoma", 9f);
            this.dgvSequence.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvSequence.Columns[0].ReadOnly = true;
            this.dgvSequence.Columns[1].ReadOnly = true;
            this.dgvSequence.Columns[2].ReadOnly = false;
            this.dgvSequence.Columns[3].Visible = false;
        }

        public void fnBindDataList()
        {
            string sql = this.fnReturnCheckListSQL();
            bool flag = getCount(sql) > 0;
            if (flag)
            {
                this.dgvList.DataSource = cls.bindingSource2;
                cls.GetData(sql, this.dgvList, cls.bindingSource2, cls.dataAdapter2);
                DataGridViewColumn dgvList_column = this.dgvList.Columns[0];
                DataGridViewColumn dgvList_column2 = this.dgvList.Columns[1];
                DataGridViewColumn dgvList_column3 = this.dgvList.Columns[2];
                DataGridViewColumn dgvList_column4 = this.dgvList.Columns[3];

                this._dgvListWidth = cls.fnGetDataGridWidth(this.dgvList);

                dgvList_column2.Width = 20 * this._dgvListWidth / 100;
                dgvList_column2.Width = 45 * this._dgvListWidth / 100;
                dgvList_column3.Width = 35 * this._dgvListWidth / 100;
                this.dgvList.RowHeadersVisible = false;
                this.dgvList.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
                this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvList.DefaultCellStyle.Font = new Font("Tahoma", 9f);
                this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvList.Columns[0].Visible = true;
                this.dgvList.Columns[1].Visible = true;
                this.dgvList.Columns[2].Visible = true;
                this.dgvList.Columns[3].Visible = false;
                this.dgvList.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void cbbPartname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool flag = this.cbbPartname.SelectedIndex > 0;
            if (flag)
            {
                this._itemID = this.cbbPartname.SelectedValue.ToString();
                string sqlBarcode = "select barcode from base_product where prodId=" + this._itemID;
                string barcode = getValue(sqlBarcode);
                this.lblPartnumber.Text = ((barcode != "") ? barcode : "N/A");
                string sqlDeliveryTotal = string.Concat(new string[]
                {
                    "select sum(boxquantity) from BASE_BoxesOutStockManagement where salesorderID=",
                    this._orderID,
                    " and productId=",
                    this._partID,
                    " and shipto='",
                    this._shipto,
                    "' group by productId"
                });
                string total = getValue(sqlDeliveryTotal);
                this.txtTotal.Text = ((total != "") ? total : "0");
                string sqlStockDA = "select cast(sum(quantity) as numeric(10,0)) from BASE_Inventory where LocationId in (100,104) and quantity>0 and ProdId=" + this._partID + " group by ProdId";
                string stockDA = getValue(sqlStockDA);
                this.txtStockDA.Text = ((stockDA != "") ? stockDA : "0");
                string sqlPlanToday = "select top 1 doPlanTomorrow from DO_DeliveryOrderItems where partID=" + this._partID + " and DATEDIFF(DAY,doDate,GETDATE())=0 order by addDate desc";
                string planToday = getValue(sqlPlanToday);
                int __planToday = (planToday != "") ? Convert.ToInt32(planToday) : 0;
                this.txtPlanToday.Text = ((__planToday > 0) ? planToday : "0");
                bool flag2 = this._orderID == "";
                if (flag2)
                {
                    this.txtStockLG.Text = "0";
                    this.txtPlanTomorrow.Text = "0";
                    this.txtRemain.Text = "0";
                }
                else
                {
                    string sqlStockLG = "select doStockLG from DO_DeliveryOrderItems where partID=" + this._itemID;
                    string stockLG = getValue(sqlStockLG);
                    this.txtStockLG.Text = ((stockLG != "") ? stockLG : "0");
                    string sqlPlanTomorrow = "select doPlanTomorrow from DO_DeliveryOrderItems where partID=" + this._itemID;
                    string planTomorrow = getValue(sqlPlanTomorrow);
                    this.txtPlanTomorrow.Text = ((planTomorrow != "") ? planTomorrow : "0");
                    string sqlQuantity = "select doQuantity from DO_DeliveryOrderItems where partID=" + this._itemID;
                    string quantity = getValue(sqlQuantity);
                    int _quantity = (quantity != "") ? Convert.ToInt32(quantity) : 0;
                    int arg_289_0 = (planToday != "") ? Convert.ToInt32(planToday) : 0;
                    int _deliveryTotal = (total != "") ? Convert.ToInt32(total) : 0;
                    this.txtRemain.Text = (_deliveryTotal - _quantity).ToString();
                }
                this._total = Convert.ToInt32(this.txtTotal.Text.Trim());
                this._quantityDA = Convert.ToInt32(this.txtStockDA.Text.Trim());
                this._remain = Convert.ToInt32(this.txtRemain.Text.Trim());
            }
            else
            {
                this.lblPartnumber.Text = "N/A";
                this.txtTotal.Text = "";
                this.txtStockLG.Text = "";
                this.txtStockDA.Text = "";
                this.txtPlanToday.Text = "";
                this.txtPlanTomorrow.Text = "";
                this.txtRemain.Text = "";
            }
            this.txtQuantity.Text = "";
        }

        private void fnCheckOrderNumber()
        {
            string requestNumber = getValue("select top 1 doCode from DO_DeliveryOrder where doStatus=1 order by doID desc");
            bool flag = requestNumber == "";
            string orderCode;
            if (flag)
            {
                orderCode = "SR-0000001";
            }
            else
            {
                int seperate = requestNumber.IndexOf("-");
                string prefix = requestNumber.Substring(0, seperate);
                string suffix = requestNumber.Substring(seperate + 1);
                int suffixConvert = Convert.ToInt32(suffix) + 1;
                orderCode = prefix + "-" + string.Format("{0:0000000}", suffixConvert);
            }
            bool flag2 = this._orderID != "" && this._orderID != null;
            if (flag2)
            {
                string sqlPrevCode = "select doCode from DO_DeliveryOrder where doID=" + this._orderID;
                string prevCode = getValue(sqlPrevCode);
                this.groupBox1.Text = prevCode + " | PART ITEM DETAILS (EDIT ORDER)";
                this._orderCode = prevCode;
            }
            else
            {
                this._orderCode = orderCode;
                this.groupBox1.Text = orderCode + " | PART ITEM DETAILS";
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            DateTime orderDate = Convert.ToDateTime(this.dtpDeliveryDate.Value.ToShortDateString());
            this._orderDate = string.Format("{0:yyyy-MM-dd}", orderDate);
            int partId = (this._itemID != "") ? Convert.ToInt32(this._itemID) : 0;
            string partname = this.cbbPartname.Text;
            string partcode = this.lblPartnumber.Text;
            int quantity = (this.txtQuantity.Text != "") ? Convert.ToInt32(this.txtQuantity.Text) : 0;
            string shipto = (this.cbbShipTo.SelectedIndex > 0) ? this.cbbShipTo.Text : "";
            string _stockLG = this.txtStockLG.Text.Trim();
            int stockLG = (_stockLG != "0" && _stockLG != "") ? Convert.ToInt32(_stockLG) : 0;
            string _planToday = this.txtPlanToday.Text.Trim();
            int planToday = (_planToday != "" && _planToday != "0") ? Convert.ToInt32(_planToday) : 0;
            string _planTomorrow = this.txtPlanTomorrow.Text.Trim();
            int planTomorrow = (_planTomorrow != "" && _planTomorrow != "0") ? Convert.ToInt32(_planTomorrow) : 0;
            bool flag = partId != 0 && partname != "" && partcode != "N/A" && quantity != 0 && quantity > 0 && shipto != "";
            if (flag)
            {
                string sqlCheckOrderExist = "select doID from DO_DeliveryOrder where doStatus=1 and datediff(day,doDate,cast('" + this._orderDate + "' as datetime))=0";
                int checkOrderExist = getCount(sqlCheckOrderExist);
                bool flag2 = checkOrderExist == 0 || (this._orderID != "" && this._orderID != null);
                if (flag2)
                {
                    string sqlCheckItemExist = string.Concat(new string[]
                    {
                        "select idx from DO_DeliveryOrderItems where partID=",
                        this._itemID,
                        " and shipTo='",
                        shipto,
                        "' and datediff(day,addDate,getdate())=0 and doID=(select doID from DO_DeliveryOrder where doID=DO_DeliveryOrderItems.doID and doCode='",
                        this._orderCode,
                        "')"
                    });
                    int checkItemExist = getCount(sqlCheckItemExist);
                    bool flag3 = checkItemExist == 0;
                    if (flag3)
                    {
                        bool flag4 = quantity >= this._total;
                        if (flag4)
                        {
                            string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                            SqlConnection con = new SqlConnection(sConn);
                            con.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "DO_DeliveryOrder_Addnew";
                            cmd.Parameters.Add("@orderCode", SqlDbType.VarChar).Value = this._orderCode;
                            cmd.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = orderDate;
                            cmd.Parameters.Add("@partId", SqlDbType.Int).Value = partId;
                            cmd.Parameters.Add("@partname", SqlDbType.NVarChar).Value = partname;
                            cmd.Parameters.Add("@partcode", SqlDbType.VarChar).Value = partcode;
                            cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                            cmd.Parameters.Add("@shipto", SqlDbType.VarChar).Value = shipto;
                            cmd.Parameters.Add("@stockLG", SqlDbType.Int).Value = stockLG;
                            cmd.Parameters.Add("@planToday", SqlDbType.Int).Value = planToday;
                            cmd.Parameters.Add("@planTomorrow", SqlDbType.Int).Value = planTomorrow;
                            cmd.Parameters.Add("@action", SqlDbType.TinyInt).Value = 0;
                            cmd.Connection = con;
                            try
                            {
                                cmd.ExecuteNonQuery();
                                this.fnBindDataOrder();
                                this.lblMessage.Text = "Add new item successful.";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "System Message :: SalesRequest");
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                        else
                        {
                            this.lblMessage.Text = "Cannot set quantity less than current delivery.";
                        }
                    }
                    else
                    {
                        this.lblMessage.Text = "This item already exist in current order.";
                    }
                }
                else
                {
                    this.lblMessage.Text = orderDate.ToShortDateString() + " has order already.";
                }
            }
            else
            {
                this.lblMessage.Text = "Please enter valid data.";
            }
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow rowList = this.dgvOrder.Rows[e.RowIndex];
            bool flag = this.dgvOrder.Rows.Count > 0;
            if (flag)
            {
                string idx = rowList.Cells[0].Value.ToString();
                string orderId = rowList.Cells[1].Value.ToString();
                string partId = rowList.Cells[2].Value.ToString();
                string partname = rowList.Cells[3].Value.ToString();
                string partcode = rowList.Cells[4].Value.ToString();
                string orderQty = rowList.Cells[5].Value.ToString();
                string shipto = rowList.Cells[6].Value.ToString();
                string partUnit = rowList.Cells[7].Value.ToString();
                string deliveryTotal = getValue(string.Concat(new string[]
                {
                    "select sum(boxquantity) from BASE_BoxesOutStockManagement where salesorderID=",
                    orderId,
                    " and productId=",
                    partId,
                    " and shipto='",
                    shipto,
                    "' group by productId"
                }));
                string stockLG = rowList.Cells[9].Value.ToString();
                string stockDA = getValue("select cast(sum(Quantity) as numeric(10,0)) from BASE_Inventory where LocationId in (100,104) and (quantity>0) and ProdId=" + partId + " group by ProdId");
                string planToday = rowList.Cells[11].Value.ToString();
                string planTomorrow = rowList.Cells[12].Value.ToString();
                int _orderQty = (orderQty != "" && orderQty != null) ? Convert.ToInt32(orderQty) : 0;
                int _deliveryTotal = (deliveryTotal != "" && deliveryTotal != null) ? Convert.ToInt32(deliveryTotal) : 0;
                int _stockLG = (stockLG != "" && stockLG != null) ? Convert.ToInt32(stockLG) : 0;
                int _stockDA = (stockDA != "" && stockDA != null) ? Convert.ToInt32(stockDA) : 0;
                int _planToday = (planToday != "" && planToday != null) ? Convert.ToInt32(planToday) : 0;
                int _planTomorrow = (planTomorrow != "" && planTomorrow != null) ? Convert.ToInt32(planTomorrow) : 0;
                string remain = (_deliveryTotal - _orderQty).ToString();
                string orderFinish = rowList.Cells[14].Value.ToString();
                string finishNote = rowList.Cells[15].Value.ToString();
                string itemStatus = rowList.Cells[16].Value.ToString();
                this._itemID = idx;
                this._partID = partId;
                this._shipto = shipto;
                this._delivery = _deliveryTotal;
                this.dtpDeliveryDate.Value = Convert.ToDateTime(getValue("select cast(doDate as datetime) from DO_DeliveryOrder where doID=" + orderId));
                this.cbbPartname.SelectedValue = partId;
                this.lblPartnumber.Text = partcode;
                this.txtQuantity.Text = orderQty;
                this.cbbShipTo.Text = shipto;
                this.txtTotal.Text = _deliveryTotal.ToString();
                this.txtStockLG.Text = _stockLG.ToString();
                this.txtStockDA.Text = _stockDA.ToString();
                this.txtPlanToday.Text = _planToday.ToString();
                this.txtPlanTomorrow.Text = _planTomorrow.ToString();
                this.txtRemain.Text = remain;
                this.chkDeliveryOK.Checked = (orderFinish == "True");
                this.txtDeliveryOKReason.Text = finishNote;
                this.dtpDeliveryDate.Enabled = false;
                this.cbbShipTo.Enabled = false;
                this.cbbPartname.Enabled = false;
                this.chkDeliveryOK.Enabled = true;
                this.txtDeliveryOKReason.Enabled = true;
                this.btnAddItem.Enabled = false;
                this.btnAddItem.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                this.rdbAdd.Enabled = false;
                this.rdbAdd.Checked = false;
                this.rdbUpd.Enabled = true;
                this.rdbDel.Enabled = true;
            }
            this.rdbUpd.Checked = false;
            this.rdbDel.Checked = false;
            this.btnOrderSave.Enabled = false;
            this.btnOrderCancel.Enabled = true;
        }

        private void chkDeliveryOK_CheckedChanged(object sender, EventArgs e)
        {
            bool @checked = this.chkDeliveryOK.Checked;
            if (@checked)
            {
                this.rdbAdd.Enabled = false;
                this.rdbUpd.Enabled = true;
                this.rdbUpd.Checked = true;
                this.rdbDel.Enabled = false;
                this.txtDeliveryOKReason.Focus();
            }
            else
            {
                bool flag = this._itemID == "";
                if (flag)
                {
                    this.rdbAdd.Enabled = true;
                    this.rdbAdd.Checked = true;
                    this.rdbUpd.Enabled = false;
                    this.rdbDel.Enabled = false;
                }
                else
                {
                    this.rdbAdd.Enabled = false;
                    this.rdbAdd.Checked = false;
                    this.rdbUpd.Enabled = true;
                    this.rdbDel.Enabled = true;
                }
            }
        }

        private void rdbUpd_CheckedChanged(object sender, EventArgs e)
        {
            bool @checked = this.chkDeliveryOK.Checked;
            if (@checked)
            {
                this.btnOrderSave.Enabled = true;
                this.btnOrderCancel.Enabled = true;
            }
            else
            {
                bool checked2 = this.rdbUpd.Checked;
                if (checked2)
                {
                    this.btnOrderSave.Enabled = true;
                    this.btnOrderCancel.Enabled = true;
                }
                else
                {
                    this.btnOrderSave.Enabled = false;
                    this.btnOrderCancel.Enabled = false;
                }
            }
        }

        private void rdbDel_CheckedChanged(object sender, EventArgs e)
        {
            bool @checked = this.chkDeliveryOK.Checked;
            if (@checked)
            {
                this.btnOrderSave.Enabled = true;
                this.btnOrderCancel.Enabled = true;
            }
            else
            {
                bool checked2 = this.rdbDel.Checked;
                if (checked2)
                {
                    this.btnOrderSave.Enabled = true;
                    this.btnOrderCancel.Enabled = true;
                }
                else
                {
                    this.btnOrderSave.Enabled = false;
                    this.btnOrderCancel.Enabled = false;
                }
            }
        }

        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            this._orderID = "";
            this._orderCode = "";
            this._itemID = "";
            this.lblMessage.Text = "";
            this.dtpDeliveryDate.Enabled = true;
            this.dtpDeliveryDate.Value = DateTime.Now;
            this.cbbPartname.Enabled = true;
            this.cbbPartname.SelectedIndex = 0;
            this.lblPartnumber.Text = "N/A";
            this.txtQuantity.Text = "";
            this.cbbShipTo.Enabled = true;
            this.cbbShipTo.SelectedIndex = 0;
            this.btnAddItem.Enabled = true;
            this.btnAddItem.BackColor = Color.DarkOrange;
            this.txtTotal.Text = "";
            this.txtStockLG.Text = "";
            this.txtStockDA.Text = "";
            this.txtPlanToday.Text = "";
            this.txtPlanTomorrow.Text = "";
            this.txtRemain.Text = "";
            this.chkDeliveryOK.Enabled = false;
            this.chkDeliveryOK.Checked = false;
            this.txtDeliveryOKReason.Enabled = false;
            this.txtDeliveryOKReason.Text = "";
            this.rdbAdd.Enabled = true;
            this.rdbAdd.Checked = true;
            this.rdbUpd.Enabled = false;
            this.rdbDel.Enabled = false;
            this.btnOrderSave.Enabled = true;
            this.btnOrderCancel.Enabled = true;
            this.dgvOrder.DataSource = "";
            this.dgvOrder.Refresh();
            this.fnCheckOrderNumber();
        }

        private void btnOrderSave_Click(object sender, EventArgs e)
        {
            bool @checked = this.rdbAdd.Checked;
            byte act;
            if (@checked)
            {
                act = 1;
            }
            else
            {
                bool checked2 = this.rdbUpd.Checked;
                if (checked2)
                {
                    act = 2;
                }
                else
                {
                    bool checked3 = this.rdbDel.Checked;
                    if (checked3)
                    {
                        act = 3;
                    }
                    else
                    {
                        act = 0;
                    }
                }
            }
            switch (act)
            {
                case 1:
                    this.fnOrderSaveAdd();
                    break;
                case 2:
                    {
                        string curQty = this.txtQuantity.Text.Trim();
                        int _curQty = (curQty != "" || curQty != "0") ? Convert.ToInt32(curQty) : 0;
                        string sqlCheckQty = string.Concat(new string[]
                        {
                    "select sum(boxquantity) from BASE_BoxesOutStockManagement where salesorderID=",
                    this._orderID,
                    " and productId=",
                    this._partID,
                    " and shipto='",
                    this._shipto,
                    "'"
                        });
                        string curDlv = getValue(sqlCheckQty);
                        int _curDlv = (curDlv != "" && curDlv != "0") ? Convert.ToInt32(curDlv) : 0;
                        bool flag = _curQty >= _curDlv;
                        if (flag)
                        {
                            DialogResult result = MessageBox.Show("Are you sure to make this request?", "System Message", MessageBoxButtons.YesNo);
                            bool flag2 = result == DialogResult.Yes;
                            if (flag2)
                            {
                                this.fnOrderSaveUpd();
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Concat(new string[]
                            {
                        "Cannot update for new quantity (",
                        curQty,
                        ") less than current quantity delivery (",
                        curDlv,
                        ")."
                            }));
                        }
                        break;
                    }
                case 3:
                    {
                        string sqlCheckDel = "select outstockId from BASE_BoxesOutStockManagement where salesorderID=" + this._orderID + " ";
                        bool flag3 = this._partID != "" && this._partID != null;
                        if (flag3)
                        {
                            sqlCheckDel = string.Concat(new string[]
                            {
                        sqlCheckDel,
                        "and productId=",
                        this._partID,
                        " and shipto='",
                        this._shipto,
                        "' "
                            });
                        }
                        bool flag4 = getCount(sqlCheckDel) == 0;
                        if (flag4)
                        {
                            DialogResult result2 = MessageBox.Show("Are you sure to make this request?", "System Message", MessageBoxButtons.YesNo);
                            bool flag5 = result2 == DialogResult.Yes;
                            if (flag5)
                            {
                                this.fnOrderSaveDel();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cannot delete this item of the DO because getting delivery already.");
                        }
                        break;
                    }
            }
        }

        public void fnOrderSaveAdd()
        {
            string orderCode = this._orderCode;
            string orderID = getValue("select doID from DO_DeliveryOrder where doCode='" + orderCode + "'");
            string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection con = new SqlConnection(sConn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DO_DeliveryOrder_Add_Addnew";
            cmd.Parameters.Add("@orderID", SqlDbType.BigInt).Value = orderID;
            cmd.Connection = con;
            try
            {
                cmd.ExecuteNonQuery();
                this.fnBindDataOrder();
                this.fnBindDataList();
                this.fnCheckOrderNumber();
                this.dgvOrder.DataSource = "";
                this.dgvOrder.Refresh();
                this.lblMessage.Text = "Addnew order successful.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "System Message :: SalesRequest");
            }
            finally
            {
                con.Close();
            }
        }

        public void fnOrderSaveUpd()
        {
            DateTime orderDate = Convert.ToDateTime(this.dtpDeliveryDate.Value.ToShortDateString());
            int partId = (this._partID != "" && this._partID != null) ? Convert.ToInt32(this._partID) : 0;
            string partname = this.cbbPartname.Text;
            string partcode = this.lblPartnumber.Text;
            int quantity = (this.txtQuantity.Text != "" && this.txtQuantity.Text != null) ? Convert.ToInt32(this.txtQuantity.Text) : 0;
            string shipto = (this.cbbShipTo.SelectedIndex > 0) ? this.cbbShipTo.Text : "";
            string _stockLG = this.txtStockLG.Text.Trim();
            int stockLG = (_stockLG != "0" && _stockLG != "") ? Convert.ToInt32(_stockLG) : 0;
            string _planToday = this.txtPlanToday.Text.Trim();
            int planToday = (_planToday != "" && _planToday != "0") ? Convert.ToInt32(_planToday) : 0;
            string _planTomorrow = this.txtPlanTomorrow.Text.Trim();
            int planTomorrow = (_planTomorrow != "" && _planTomorrow != "0") ? Convert.ToInt32(_planTomorrow) : 0;
            int finish = this.chkDeliveryOK.Checked ? 1 : 0;
            string fNote = this.txtDeliveryOKReason.Text.Trim();
            bool flag = quantity >= this._delivery;
            if (flag)
            {
                bool flag2 = partId != 0 && partname != "" && partcode != "N/A" && quantity != 0 && quantity > 0 && shipto != "";
                if (flag2)
                {
                    string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                    SqlConnection con = new SqlConnection(sConn);
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DO_DeliveryOrder_Upd_Addnew";
                    cmd.Parameters.Add("@orderID", SqlDbType.VarChar).Value = this._orderID;
                    cmd.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = orderDate;
                    cmd.Parameters.Add("@partId", SqlDbType.Int).Value = partId;
                    cmd.Parameters.Add("@partname", SqlDbType.NVarChar).Value = partname;
                    cmd.Parameters.Add("@partcode", SqlDbType.VarChar).Value = partcode;
                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = quantity;
                    cmd.Parameters.Add("@shipto", SqlDbType.VarChar).Value = shipto;
                    cmd.Parameters.Add("@stockLG", SqlDbType.Int).Value = stockLG;
                    cmd.Parameters.Add("@planToday", SqlDbType.Int).Value = planToday;
                    cmd.Parameters.Add("@planTomorrow", SqlDbType.Int).Value = planTomorrow;
                    cmd.Parameters.Add("@action", SqlDbType.TinyInt).Value = 0;
                    cmd.Parameters.Add("@finish", SqlDbType.Bit).Value = finish;
                    cmd.Parameters.Add("@finishNote", SqlDbType.NVarChar).Value = fNote;
                    cmd.Connection = con;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        this.fnBindDataOrder();
                        this.lblMessage.Text = "Update item successful.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "System Message :: SalesRequest");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    this.lblMessage.Text = "Please enter valid data.";
                }
            }
            else
            {
                this.lblMessage.Text = "Cannot set quantity less than current delivery.";
            }
        }

        public void fnOrderSaveDel()
        {
            string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection con = new SqlConnection(sConn);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DO_DeliveryOrder_Del_Addnew";
            cmd.Parameters.Add("@orderID", SqlDbType.BigInt).Value = this._orderID;
            cmd.Parameters.Add("@partId", SqlDbType.Int).Value = ((this._partID == "" || this._partID == null) ? "0" : this._partID);
            cmd.Parameters.Add("@action", SqlDbType.TinyInt).Value = ((this._partID != "" && this._partID != null) ? "1" : "0");
            cmd.Connection = con;
            try
            {
                cmd.ExecuteNonQuery();
                this.fnBindDataOrder();
                this.lblMessage.Text = "Update item successful.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "System Message :: SalesRequest");
            }
            finally
            {
                con.Close();
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow rowList = this.dgvList.Rows[e.RowIndex];
            bool flag = this.dgvList.Rows.Count > 0;
            if (flag)
            {
                this._orderID = rowList.Cells[0].Value.ToString();
                this._orderCode = rowList.Cells[1].Value.ToString();
                this._orderDate = rowList.Cells[2].Value.ToString();
                this.dtpDeliveryDate.Value = Convert.ToDateTime(this._orderDate);
                this.dtpDeliveryDate.Enabled = false;
                this.rdbDel.Enabled = true;
                this.fnBindDataOrder();
                this.fnCheckOrderNumber();
            }
        }

        private void btnSequenceSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to make this request?", "System Message", MessageBoxButtons.YesNo);
            bool flag = result == DialogResult.Yes;
            if (flag)
            {
                this.fnSortingSave();
                this.fnBindDataList();
                this.fnInitData();
                this.fnBindDataSequence();
            }
        }

        private void fnSortingSave()
        {
            int rCount = this.dgvSequence.Rows.Count;
            try
            {
                for (int i = 0; i < rCount; i++)
                {
                    string prodSort = this.dgvSequence.Rows[i].Cells[2].Value.ToString();
                    string prodID = this.dgvSequence.Rows[i].Cells[3].Value.ToString();
                    this.fnSetProductSort(prodSort, prodID);
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

        ///////////////////////////////////////////////////
        ///////////////////////////////////////////////////
        ///////////////////////////////////////////////////

        private void fnSetProductSort(string sSort, string prodID)
        {
            int rCount = this.dgvSequence.Rows.Count;
            bool flag = rCount > 0;
            if (flag)
            {
                string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
                SqlConnection con = new SqlConnection(sConn);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SR_SaleProductSortingUpdate_Addnew";
                cmd.Parameters.Add("@sort", SqlDbType.Int).Value = sSort;
                cmd.Parameters.Add("@prodID", SqlDbType.Int).Value = prodID;
                cmd.Connection = con;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "System Message :: SalesProductRequest");
                }
                finally
                {
                    con.Close();
                }
            }
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

        public DataTable getTable(string sql)
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

        public void getProductInfo()
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
    }
}
