using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Inventory_Data
{
	public class frmInventoryOutStock3 : Form
	{
		public string _orderID = "";

		public string _orderCode = "";

		public string _prodID = "";

		public string _prodCode = "";

		public string _prodName = "";

		public string _shipto;

		public string _finish;

		public string _fNote;

		public string _tempFinish;

		public int _dgvOrderListRowSelected = 0;

		public int dgvInventoryWidth;

		public int dgvOrderDetailWidth;

		public int dgvOrderScanOutWidth;

		private IContainer components = null;

		private TableLayoutPanel tableLayoutPanel1;

		private Label lblTitle;

		private Label lblDateTime;

		private DataGridView dgvOrderList;

		private Label lblOrderTitle;

		private DataGridView dgvOrderDetail;

		private DataGridView dgvOrderScanOut;

		private Label lblOrderQuantity;

		private Label lblOrderInfo;

		private TableLayoutPanel tableLayoutPanel2;

		private Label lblOrderRequest;

		private Label lblOrderRespond;

		private Label lblOrderSubTotal;

		private Label lblOrderCurrent;

		private Label lblOrderRemain;

		private Label lblOrderTotal;

		private Label label11;

		private Timer timer1;

		private TextBox txtBoxcode;

		private Label lblOrderPercent;

		private DataGridView dgvInventory;

		private TableLayoutPanel tableLayoutPanel3;

		private Label lblItemTitle;

		private Label lblItemValue;

		public frmInventoryOutStock3()
		{
			InitializeComponent();
		}

		private void frmInventoryOutStock3_Load(object sender, EventArgs e)
		{
			fnGetDateTime();
			fnBindSalesRequests();
			fnGridViewFormat();
			init();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			fnGetDateTime();
			fnBindSalesRequests();
			bool flag = lblOrderSubTotal.Text != "0";
			if (flag)
			{
				txtBoxcode.Focus();
			}
			bool flag2 = lblOrderTotal.Text != "0";
			if (flag2)
			{
				dgvOrderList.Rows[_dgvOrderListRowSelected].Selected = true;
			}
			dgvInventoryWidth = dgvInventory.Width - 20;
			dgvOrderDetailWidth = dgvOrderDetail.Width - 20;
			dgvOrderScanOutWidth = dgvOrderScanOut.Width - 40;
		}

		public void init()
		{
			txtBoxcode.Enabled = false;
			lblOrderSubTotal.Text = "0";
			lblOrderTotal.Text = "0";
			lblOrderCurrent.Text = "0";
			lblOrderPercent.Text = "0";
			lblOrderRemain.Text = "0";
			dgvInventoryWidth = dgvInventory.Width - 20;
			dgvOrderDetailWidth = dgvOrderDetail.Width - 20;
			dgvOrderScanOutWidth = dgvOrderScanOut.Width - 40;
			fnItemInvetory();
			fnBindInventoryData();
		}

		public void fnGetDateTime()
		{
			lblDateTime.Text = getProductInfo("dd/MM/yyyy HH:mm:ss");
		}

		private void fnBindSalesRequests()
		{
			string sql = "SELECT doID AS [OID], doCode AS [Order code], doDate AS [Order date], doStatus AS [Order status] ";
			sql += "FROM dbo.DO_DeliveryOrder ";
			sql += "WHERE DATEDIFF(WEEK,doMake,GETDATE())>=0 AND DATEDIFF(WEEK,doMake,GETDATE())<=4 AND doStatus=1 ";
			sql += "ORDER BY doMake DESC";
			dgvOrderList.DataSource = cls.bindingSource0;
			cls.GetData(sql, dgvOrderList, cls.bindingSource0, cls.dataAdapter0);
			int dgvOrderListWidth = 290;
			int dgvOrderListHeight = dgvOrderList.Height;
			DataGridViewColumn dgvOrderList_column = dgvOrderList.Columns[0];
			DataGridViewColumn dgvOrderList_column2 = dgvOrderList.Columns[1];
			DataGridViewColumn dgvOrderList_column3 = dgvOrderList.Columns[2];
			DataGridViewColumn dgvOrderList_column4 = dgvOrderList.Columns[3];
			dgvOrderList_column.Width = 10 * dgvOrderListWidth / 100;
			dgvOrderList_column2.Width = 50 * dgvOrderListWidth / 100;
			dgvOrderList_column3.Width = 40 * dgvOrderListWidth / 100;
			dgvOrderList.RowHeadersVisible = false;
			dgvOrderList.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			dgvOrderList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvOrderList.DefaultCellStyle.Font = new Font("Tahoma", 9f);
			dgvOrderList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvOrderList.Columns[0].Visible = true;
			dgvOrderList.Columns[1].Visible = true;
			dgvOrderList.Columns[2].Visible = true;
			dgvOrderList.Columns[3].Visible = false;
			dgvOrderList.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
		}

		private void dgvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow rowOrderList = dgvOrderList.Rows[e.RowIndex];
			string orderID = rowOrderList.Cells[0].Value.ToString();
			string orderCode = rowOrderList.Cells[1].Value.ToString();
			string orderDate = rowOrderList.Cells[3].Value.ToString();
			bool flag = dgvOrderList.Rows.Count > 0;
			if (flag)
			{
				_orderID = orderID;
				_orderCode = orderCode;
				_dgvOrderListRowSelected = e.RowIndex;
				rowOrderList.Selected = true;
				fnBindDetail();
			}
			foreach (DataGridViewRow row in ((IEnumerable)dgvOrderDetail.Rows))
			{
				string idx = row.Cells[0].Value.ToString();
				string oID = row.Cells[1].Value.ToString();
				string pID = row.Cells[2].Value.ToString();
				string partname = row.Cells[3].Value.ToString();
				string partcode = row.Cells[4].Value.ToString();
				string shipto = row.Cells[5].Value.ToString();
				string qty = row.Cells[6].Value.ToString();
				string unit = row.Cells[7].Value.ToString();
				string scan = row.Cells[8].Value.ToString();
				string stockLG = row.Cells[9].Value.ToString();
				string stockDA = row.Cells[10].Value.ToString();
				string planToday = row.Cells[11].Value.ToString();
				string planTomorrow = row.Cells[12].Value.ToString();
				string remain = row.Cells[13].Value.ToString();
				string finish = row.Cells[14].Value.ToString();
				string note = row.Cells[15].Value.ToString();
				string sqlTotal = string.Concat(new string[]
				{
					"select sum(boxquantity) from BASE_BoxesOutStockManagement where salesorderID=",
					oID,
					" and productId=",
					pID,
					" and shipto='",
					shipto,
					"' group by productId"
				});
				string total = getValue(sqlTotal);
				int _total = (total != "") ? Convert.ToInt32(total) : 0;
				int _qty = (qty != "") ? Convert.ToInt32(qty) : 0;
				int arg_360_0 = (planToday != "") ? Convert.ToInt32(planToday) : 0;
				row.Cells[8].Value = _total;
				row.Cells[13].Value = _total - _qty;
				bool flag2 = scan == "" || scan == "0";
				if (flag2)
				{
					row.DefaultCellStyle.BackColor = Color.Gold;
				}
				else
				{
					bool flag3 = scan == qty || finish.ToLower() == "true";
					if (flag3)
					{
						row.DefaultCellStyle.BackColor = Color.Green;
					}
				}
			}
			fnSetOrderTotal(orderID);
			txtBoxcode.Enabled = false;
			dgvOrderScanOut.DataSource = "";
			dgvOrderScanOut.Refresh();
		}

		public void fnBindDetail()
		{
			txtBoxcode.Text = "";
			lblOrderSubTotal.Text = "0";
			lblOrderPercent.Text = "0";
			lblOrderRemain.Text = "0";
			string sql = "SELECT idx AS IDX, doID AS DID, partID AS PID, partname AS [Partname], partcode AS [Partcode], shipTo AS [Ship], doQuantity AS [Q'ty], doUnit AS [Unit] ";
			sql += ", (SELECT cast(sum(boxquantity) as numeric(10,0)) FROM BASE_BoxesOutStockManagement WHERE salesorderID=DO_DeliveryOrderItems.doID and productID=DO_DeliveryOrderItems.partID and shipto=DO_DeliveryOrderItems.shipto) as [Scan] ";
			sql += ", doStockLG AS [StockLG] ";
			sql += ", (SELECT cast(sum(quantity) as numeric(10,0)) FROM BASE_Inventory WHERE LocationId in (100,104) and prodId=DO_DeliveryOrderItems.partID GROUP BY prodId) AS [StockDA] ";
			sql += ", doPlanToday AS [Plan], doPlanTomorrow AS [Plan+1] ";
			sql += ", (SELECT cast(doPlanToday as numeric(10,0))-(SELECT cast(sum(boxquantity) as numeric(10,0)) FROM BASE_BoxesOutStockManagement WHERE salesorderID=DO_DeliveryOrderItems.doID and productID=DO_DeliveryOrderItems.partID and shipto=DO_DeliveryOrderItems.shipto)) AS [Remain] ";
			sql += ", doFinish AS Finish, finishNote AS Note ";
			sql += "FROM dbo.DO_DeliveryOrderItems ";
			sql = sql + "WHERE doID=" + _orderID;
			dgvOrderDetail.DataSource = cls.bindingSource1;
			cls.GetData(sql, dgvOrderDetail, cls.bindingSource1, cls.dataAdapter1);
			DataGridViewColumn dgvOrderDetail_column0 = dgvOrderDetail.Columns[0];
			DataGridViewColumn dgvOrderDetail_column = dgvOrderDetail.Columns[1];
			DataGridViewColumn dgvOrderDetail_column2 = dgvOrderDetail.Columns[2];
			DataGridViewColumn dgvOrderDetail_column3 = dgvOrderDetail.Columns[3];
			DataGridViewColumn dgvOrderDetail_column4 = dgvOrderDetail.Columns[4];
			DataGridViewColumn dgvOrderDetail_column5 = dgvOrderDetail.Columns[5];
			DataGridViewColumn dgvOrderDetail_column6 = dgvOrderDetail.Columns[6];
			DataGridViewColumn dgvOrderDetail_column7 = dgvOrderDetail.Columns[7];
			DataGridViewColumn dgvOrderDetail_column8 = dgvOrderDetail.Columns[8];
			DataGridViewColumn dgvOrderDetail_column9 = dgvOrderDetail.Columns[9];
			DataGridViewColumn dgvOrderDetail_column10 = dgvOrderDetail.Columns[10];
			DataGridViewColumn dgvOrderDetail_column11 = dgvOrderDetail.Columns[11];
			DataGridViewColumn dgvOrderDetail_column12 = dgvOrderDetail.Columns[12];
			DataGridViewColumn dgvOrderDetail_column13 = dgvOrderDetail.Columns[13];
			DataGridViewColumn dgvOrderDetail_column14 = dgvOrderDetail.Columns[14];
			DataGridViewColumn dgvOrderDetail_column15 = dgvOrderDetail.Columns[15];
			dgvOrderDetail_column0.Visible = false;
			dgvOrderDetail_column.Visible = false;
			dgvOrderDetail_column2.Visible = false;
			dgvOrderDetail_column3.Visible = true;
			dgvOrderDetail_column4.Visible = true;
			dgvOrderDetail_column5.Visible = true;
			dgvOrderDetail_column6.Visible = true;
			dgvOrderDetail_column7.Visible = true;
			dgvOrderDetail_column8.Visible = true;
			dgvOrderDetail_column9.Visible = true;
			dgvOrderDetail_column10.Visible = true;
			dgvOrderDetail_column11.Visible = true;
			dgvOrderDetail_column12.Visible = false;
			dgvOrderDetail_column13.Visible = true;
			dgvOrderDetail_column14.Visible = true;
			dgvOrderDetail_column15.Visible = true;
			dgvOrderDetail_column3.Width = 15 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column4.Width = 10 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column5.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column6.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column7.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column8.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column9.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column10.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column11.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column13.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column14.Width = 6 * dgvOrderDetailWidth / 100;
			dgvOrderDetail_column15.Width = 20 * dgvOrderDetailWidth / 100;
			dgvOrderDetail.RowHeadersVisible = false;
			dgvOrderDetail.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8f, FontStyle.Bold);
			dgvOrderDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvOrderDetail.DefaultCellStyle.Font = new Font("Tahoma", 8f);
			dgvOrderDetail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvOrderDetail_column0.ReadOnly = true;
			dgvOrderDetail_column.ReadOnly = true;
			dgvOrderDetail_column2.ReadOnly = true;
			dgvOrderDetail_column3.ReadOnly = true;
			dgvOrderDetail_column4.ReadOnly = true;
			dgvOrderDetail_column5.ReadOnly = true;
			dgvOrderDetail_column6.ReadOnly = true;
			dgvOrderDetail_column7.ReadOnly = true;
			dgvOrderDetail_column8.ReadOnly = true;
			dgvOrderDetail_column9.ReadOnly = true;
			dgvOrderDetail_column10.ReadOnly = true;
			dgvOrderDetail_column11.ReadOnly = true;
			dgvOrderDetail_column12.ReadOnly = true;
			dgvOrderDetail_column13.ReadOnly = true;
			dgvOrderDetail_column14.ReadOnly = true;
			dgvOrderDetail_column15.ReadOnly = true;
		}

		public void fnGridViewFormat()
		{
			int nOrderDetail = dgvOrderDetail.Rows.Count;
			bool flag = nOrderDetail > 0;
			if (flag)
			{
				int dgvOrderDetailWidth = getWidth(dgvOrderDetail) - 20;
				int dgvOrderDetailHeight = dgvOrderDetail.Height;
				DataGridViewColumn dgvOrderDetail_column = dgvOrderDetail.Columns[0];
				DataGridViewColumn dgvOrderDetail_column2 = dgvOrderDetail.Columns[1];
				DataGridViewColumn dgvOrderDetail_column3 = dgvOrderDetail.Columns[2];
				DataGridViewColumn dgvOrderDetail_column4 = dgvOrderDetail.Columns[3];
				DataGridViewColumn dgvOrderDetail_column5 = dgvOrderDetail.Columns[4];
				DataGridViewColumn dgvOrderDetail_column6 = dgvOrderDetail.Columns[5];
				dgvOrderDetail_column.Width = 10 * dgvOrderDetailWidth / 100;
				dgvOrderDetail_column2.Width = 30 * dgvOrderDetailWidth / 100;
				dgvOrderDetail_column3.Width = 20 * dgvOrderDetailWidth / 100;
				dgvOrderDetail_column4.Width = 15 * dgvOrderDetailWidth / 100;
				dgvOrderDetail_column5.Width = 10 * dgvOrderDetailWidth / 100;
				dgvOrderDetail_column6.Width = 15 * dgvOrderDetailWidth / 100;
				dgvOrderDetail.RowHeadersVisible = false;
				dgvOrderDetail.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
				dgvOrderDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dgvOrderDetail.DefaultCellStyle.Font = new Font("Tahoma", 9f);
				dgvOrderDetail.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dgvOrderDetail_column.ReadOnly = true;
				dgvOrderDetail_column2.ReadOnly = true;
				dgvOrderDetail_column3.ReadOnly = true;
				dgvOrderDetail_column4.ReadOnly = true;
				dgvOrderDetail_column5.ReadOnly = true;
				dgvOrderDetail_column6.ReadOnly = true;
				dgvOrderDetail_column.Visible = true;
				dgvOrderDetail_column2.Visible = true;
				dgvOrderDetail_column3.Visible = true;
				dgvOrderDetail_column4.Visible = true;
				dgvOrderDetail_column5.Visible = true;
				dgvOrderDetail_column6.Visible = true;
			}
			int nOrderList = dgvOrderList.Rows.Count;
			bool flag2 = nOrderList > 0;
			if (flag2)
			{
			}
			int nOrderScanOut = dgvOrderScanOut.Rows.Count;
			bool flag3 = nOrderScanOut > 0;
			if (flag3)
			{
			}
		}

		private void dgvOrderList_Scroll(object sender, ScrollEventArgs e)
		{
			fnResetTimer(timer1);
		}

		public void fnSetOrderTotal(string orderID)
		{
			string _total = getValue(string.Concat(new string[]
			{
				"select cast(SUM(doQuantity) as numeric(12,0)) from DO_DeliveryOrderItems where doID = ",
				orderID,
				" and shipto='",
				_shipto,
				"' group by doID"
			}));
			lblOrderTotal.Text = ((_total != "") ? string.Format("{0:0.0}", _total) : "0");
		}

		public void fnSetOrderTotal(string orderID, string subtotal)
		{
			fnSetOrderTotal(orderID);
			lblOrderSubTotal.Text = ((subtotal != "") ? string.Format("{0:0.0}", subtotal) : "0");
		}

		private void dgvOrderDetail_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow rowOrderDetail = dgvOrderDetail.Rows[e.RowIndex];
			string partID = rowOrderDetail.Cells[2].Value.ToString();
			string partName = rowOrderDetail.Cells[3].Value.ToString();
			string partNumber = rowOrderDetail.Cells[4].Value.ToString();
			string shipto = rowOrderDetail.Cells[5].Value.ToString();
			string finish = rowOrderDetail.Cells[14].Value.ToString();
			string fNote = rowOrderDetail.Cells[15].Value.ToString();
			string partQuantity = rowOrderDetail.Cells[6].Value.ToString();

			bool flag = dgvOrderDetail.Rows.Count > 0;
			if (flag)
			{
				_prodID = partID;
				_prodCode = partNumber;
				_prodName = partName;
				_shipto = shipto;
				_finish = finish;
				_fNote = fNote;
				dgvOrderScanOut.DataSource = "";
				dgvOrderScanOut.Refresh();
				txtBoxcode.Text = "";
				txtBoxcode.Enabled = true;
				txtBoxcode.Focus();
				rowOrderDetail.Selected = true;
				fnSetOrderTotal(_orderID, partQuantity);
				viewproductorderboxes();
				totalQuantityOutStock();
				checkTotalBoxOutStock();
				fnItemInvetory();
			}
		}

		private void txtBoxcode_KeyDown(object sender, KeyEventArgs e)
		{
            try
            {
                bool flag = e.KeyCode == Keys.Return;
                if (flag)
                {
                    string orderID = _orderID;
                    string orderCode = _orderCode;
                    string prodID = _prodID;
                    string prodCode = _prodCode;
                    string prodName = _prodName;
                    string shipto = _shipto;
                    string packingID = txtBoxcode.Text.Trim();
                    string packingKind = packingID.Substring(0, 3);
                    string packingType = packingID.Substring(4, 3);
                    string sqlPackingQty = "select cast([X] as numeric(10,0)) from BASE_Product where ProdId=" + _prodID;
                    string pcs = getValue(sqlPackingQty.Replace("[X]", "PackingOutStock"));
                    string box = getValue(sqlPackingQty.Replace("[X]", "PackingBox"));
                    string car = getValue(sqlPackingQty.Replace("[X]", "PackingCart"));
                    string pal = getValue(sqlPackingQty.Replace("[X]", "PackingPallete"));
                    bool flag2 = packingID != "" && packingKind.ToUpper() == "PRO";
                    if (flag2)
                    {
                        string a = packingType;
                        int packingQty;
                        if (!(a == "PCS"))
                        {
                            if (!(a == "BOX"))
                            {
                                if (!(a == "CAR"))
                                {
                                    if (!(a == "PAL"))
                                    {
                                        packingQty = 0;
                                    }
                                    else
                                    {
                                        packingQty = ((pal != "0" && pal != null) ? Convert.ToInt32(pal) : 0);
                                    }
                                }
                                else
                                {
                                    packingQty = ((car != "0" && car != null) ? Convert.ToInt32(car) : 0);
                                }
                            }
                            else
                            {
                                packingQty = ((box != "0" && box != null) ? Convert.ToInt32(box) : 0);
                            }
                        }
                        else
                        {
                            packingQty = ((pcs != "0" && pcs != null) ? Convert.ToInt32(pcs) : 0);
                        }
                        
                        if(packingQty>0)
                        {
	                        string sqlCheck1 = "select boxid from BASE_BoxesInOutActivities where boxcode='" + packingID + "' and IN_Stock=1 and boxlocate like 'mmt%'";
	                        if (getCount(sqlCheck1) <= 0 
	                            && prodName.ToLower().Contains("balance").ToString() == "False"
	                            && prodName.ToLower().Contains("ab vg").ToString() == "False"
	                            && prodName.ToLower().Contains("spirit").ToString() == "False")
	                        {
	                            MessageBox.Show("Mã kiện này chưa nhập kho", "System Message");
	                            txtBoxcode.Text = "";
	                        }
	                        else
	                        {
	                            string sqlCheck2 = "select boxid from BASE_BoxesInOutActivities where boxcode='" + packingID + "' and prodId=" + prodID + " and IN_Stock=1 and boxlocate like 'mmt%'";
	                            if (getCount(sqlCheck2) <= 0 
	                                && prodName.ToLower().Contains("balance").ToString() == "False"
	                                && prodName.ToLower().Contains("ab vg").ToString() == "False"
	                                && prodName.ToLower().Contains("spirit").ToString() == "False")
	                            {
	                                MessageBox.Show("Đang xuất sai loại hàng hiện tại", "System Message");
	                                txtBoxcode.Text = "";
	                            }
	                            else
	                            {
	                                string chkBoxOutQuantity = "SELECT boxquantity FROM dbo.BASE_BoxesManagement WHERE (boxcode = '" + packingID + "')";
	                                string currPackingQty = getValue(chkBoxOutQuantity);
	                                //int nCheckBoxOutQuantity = packingQty;
	                                int nCheckBoxOutQuantity = (currPackingQty != "" && currPackingQty != null) ? Convert.ToInt32(currPackingQty) : 0;
	                                int nCheckRemainQuantity = Convert.ToInt32(lblOrderRemain.Text);
	                                int nCheckDifferQuantity = nCheckBoxOutQuantity + nCheckRemainQuantity;
	                                bool flag3 = nCheckDifferQuantity > 0 && nCheckRemainQuantity != 0;
	                                if (flag3)
	                                {
	                                    MessageBox.Show("Số lượng còn lại không phù hợp với đơn hàng", "System Message");
	                                    txtBoxcode.Text = "";
	                                }
	                                else
	                                {
	                                    string sConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
	                                    SqlConnection con = new SqlConnection(sConn);
	                                    con.Open();
	                                    SqlCommand cmd = new SqlCommand();
	                                    cmd.CommandType = CommandType.StoredProcedure;
	                                    cmd.CommandText = "BASE_Product_OutStock2_Addnew";
	                                    cmd.Parameters.Add("boxcode", SqlDbType.VarChar).Value = packingID;
	                                    cmd.Parameters.Add("salesorderID", SqlDbType.Int).Value = orderID;
	                                    cmd.Parameters.Add("salesordernumber", SqlDbType.VarChar).Value = orderCode;
	                                    cmd.Parameters.Add("salesorderpartname", SqlDbType.VarChar).Value = prodName;
	                                    cmd.Parameters.Add("salesorderpartnumber", SqlDbType.VarChar).Value = prodCode;
	                                    cmd.Parameters.Add("productId", SqlDbType.Int).Value = prodID;
	                                    cmd.Parameters.Add("fnFinish", SqlDbType.TinyInt).Value = "0";
	                                    cmd.Parameters.Add("shipto", SqlDbType.VarChar).Value = shipto;
	                                    cmd.Connection = con;
	                                    try
	                                    {
	                                        cmd.ExecuteNonQuery();
	                                    }
	                                    catch (Exception ex)
	                                    {
	                                        MessageBox.Show(ex.ToString());
	                                    }
	                                    finally
	                                    {
	                                        con.Close();
	                                    }
	                                    txtBoxcode.Text = null;
	                                    viewproductorderboxes();
	                                    totalQuantityOutStock();
	                                    checkTotalBoxOutStock();
	                                    fnItemInvetory();
	                                    fnBindInventoryData();
	                                    fnBindDetail();
	                                }
	                            }
	                        }
                        
                        }
                        else
                        {
                        	txtBoxcode.Text = "";
                        	MessageBox.Show("Không tìm thấy số lượng packing chuẩn tương ứng.\r\nVui lòng kiểm tra lại", "System Message");
                        }
                    }
                    else
                    {
                        if (packingKind.ToUpper() != "PRO")
                        {
                            MessageBox.Show("Vui lòng quét đúng mã tem (PRO-BOX/CAR/PAK/PAL-XXXXXXXX)", "System Message");
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng quét mã thùng/xe trước", "System Message");
                        }
                        txtBoxcode.Text = "";
                        
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
		}

		private void dgvOrderList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			fnSetDatagridRowColor(dgvOrderList);
		}

		private void dgvOrderDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			fnSetDatagridRowColor(dgvOrderDetail);
		}

		private void dgvOrderScanOut_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			fnSetDatagridRowColor(dgvOrderScanOut);
		}

		private void viewproductorderboxes()
		{
			string sqlGetOrderBoxes = "SELECT distinct dbo.DO_DeliveryOrderItems.partID as ID,dbo.BASE_BoxesOutStockManagement.boxcode AS [Mã thùng/xe], dbo.BASE_BoxesOutStockManagement.packingdate AS [Ngày đóng hàng], dbo.BASE_BoxesOutStockManagement.boxLOT AS [Số LOT], ";
			sqlGetOrderBoxes += "dbo.BASE_BoxesOutStockManagement.boxquantity AS [Số lượng], dbo.BASE_BoxesOutStockManagement.boxpartno AS [Mã hàng], dbo.BASE_BoxesOutStockManagement.outstockfinishdate AS [Xuất hàng],outstockID ";
			sqlGetOrderBoxes += "FROM dbo.BASE_BoxesOutStockManagement INNER JOIN dbo.DO_DeliveryOrderItems ON dbo.BASE_BoxesOutStockManagement.salesorderID = dbo.DO_DeliveryOrderItems.doID ";
			sqlGetOrderBoxes = string.Concat(new string[]
			{
				sqlGetOrderBoxes,
				"WHERE (dbo.BASE_BoxesOutStockManagement.productId = ",
				_prodID,
				") AND (dbo.BASE_BoxesOutStockManagement.shipto = '",
				_shipto,
				"') AND (dbo.DO_DeliveryOrderItems.doID = ",
				_orderID,
				") "
			});
			sqlGetOrderBoxes = sqlGetOrderBoxes + "AND (dbo.DO_DeliveryOrderItems.partID = " + _prodID + ") AND (dbo.DO_DeliveryOrderItems.doQuantity > 0) ";
			sqlGetOrderBoxes += "ORDER BY [Xuất hàng] DESC";
			dgvOrderScanOut.DataSource = cls.bindingSource3;
			cls.GetData(sqlGetOrderBoxes, dgvOrderScanOut, cls.bindingSource3, cls.dataAdapter3);
			dgvOrderScanOut.Columns.Add(new DeleteColumn());
			bool flag = dgvOrderScanOut.Columns.Count > 9;
			if (flag)
			{
				dgvOrderScanOut.Columns.RemoveAt(0);
			}
			DataGridViewColumn dgvOrderScanOut_column = dgvOrderScanOut.Columns[0];
			DataGridViewColumn dgvOrderScanOut_column2 = dgvOrderScanOut.Columns[1];
			DataGridViewColumn dgvOrderScanOut_column3 = dgvOrderScanOut.Columns[2];
			DataGridViewColumn dgvOrderScanOut_column4 = dgvOrderScanOut.Columns[3];
			DataGridViewColumn dgvOrderScanOut_column5 = dgvOrderScanOut.Columns[4];
			DataGridViewColumn dgvOrderScanOut_column6 = dgvOrderScanOut.Columns[5];
			DataGridViewColumn dgvOrderScanOut_column7 = dgvOrderScanOut.Columns[6];
			DataGridViewColumn dgvOrderScanOut_column8 = dgvOrderScanOut.Columns[7];
			dgvOrderScanOut_column.Width = 10 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut_column2.Width = 20 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut_column3.Width = 20 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut_column4.Width = 10 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut_column5.Width = 10 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut_column6.Width = 15 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut_column7.Width = 15 * dgvOrderScanOutWidth / 100;
			dgvOrderScanOut.RowHeadersVisible = false;
			dgvOrderScanOut.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9f, FontStyle.Bold);
			dgvOrderScanOut.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvOrderScanOut.DefaultCellStyle.Font = new Font("Tahoma", 9f);
			dgvOrderScanOut.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvOrderScanOut_column.Visible = true;
			dgvOrderScanOut_column2.Visible = true;
			dgvOrderScanOut_column3.Visible = true;
			dgvOrderScanOut_column4.Visible = true;
			dgvOrderScanOut_column5.Visible = true;
			dgvOrderScanOut_column6.Visible = true;
			dgvOrderScanOut_column7.Visible = true;
			dgvOrderScanOut_column8.Visible = false;
			dgvOrderScanOut_column3.DefaultCellStyle.Format = "dd/MM/yyyy";
			dgvOrderScanOut_column7.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
		}

		private void totalQuantityOutStock()
		{
			string orderID = _orderID;
			string orderCode = _orderCode;
			string prodID = _prodID;
			string sqlGetTotalBoxesOutStock = string.Concat(new string[]
			{
				"select sum(boxquantity) from BASE_BoxesOutStockManagement where (salesorderID=",
				orderID,
				") and (productID=",
				prodID,
				") and (shipto='",
				_shipto,
				"') group by salesorderID"
			});
			string strQuantityOutStock = getValue(sqlGetTotalBoxesOutStock);
			lblOrderCurrent.Text = ((strQuantityOutStock != "") ? strQuantityOutStock : "0");
		}

		private void checkTotalBoxOutStock()
		{
			string orderID = _orderID;
			string orderCode = _orderCode;
			string prodID = _prodID;
			string shipto = _shipto;
			string sqlOutStockQuantity = string.Concat(new string[]
			{
				"select sum(boxquantity) from BASE_BoxesOutStockManagement where (salesorderID=",
				orderID,
				") and (productId = ",
				prodID,
				") and (shipto='",
				shipto,
				"') group by salesorderID"
			});
			string orderQuantity = lblOrderSubTotal.Text.Trim();
			string outstockQuantity = getValue(sqlOutStockQuantity);
			bool flag = outstockQuantity != "" && outstockQuantity != "0";
			if (flag)
			{
				int curOrderQty = Convert.ToInt32(lblOrderCurrent.Text);
				int curOrderNeed = Convert.ToInt32(lblOrderSubTotal.Text);
				int curOrderDiff = curOrderQty - curOrderNeed;
				lblOrderRemain.Text = curOrderDiff.ToString();
				int curOrderTotal = Convert.ToInt32(lblOrderTotal.Text);
				string sqlOutStockPercent = "select sum(boxquantity) from BASE_BoxesOutStockManagement where salesorderID=" + orderID;
				int curOrderPercent = (getValue(sqlOutStockPercent) != "") ? Convert.ToInt32(getValue(sqlOutStockPercent)) : 0;
				lblOrderPercent.Text = curOrderPercent * 100 / curOrderTotal + "%";
			}
			else
			{
				lblOrderRemain.Text = "0";
			}
			bool flag2 = orderQuantity == outstockQuantity;
			if (flag2)
			{
				txtBoxcode.Enabled = false;
				txtBoxcode.Text = "Mã " + _prodCode + " đã xuất đủ số lượng yêu cầu";
			}
			else
			{
				txtBoxcode.Enabled = true;
			}
		}

		public void fnItemInvetory()
		{
			string prodID = _prodID;
			bool flag = prodID != "";
			if (flag)
			{
				string sql = "select (select barcode from BASE_Product where ProdId=BASE_Inventory.ProdId) as [Name], ";
				sql += "cast(sum(quantity) as numeric(10,0)) as [Total] ";
				sql += "from BASE_Inventory ";
				sql = sql + "where LocationId in (100,104) and prodID = " + prodID + " ";
				sql += "group by ProdId";
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
					int found = ds.Tables["Sum"].Rows.Count;
					bool flag2 = found > 0;
					if (flag2)
					{
						lblItemTitle.Text = ds.Tables["Sum"].Rows[0][0].ToString();
						lblItemValue.Text = ds.Tables["Sum"].Rows[0][1].ToString();
					}
					else
					{
						lblItemTitle.Text = "-";
						lblItemValue.Text = "N/A";
					}
				}
				catch
				{
				}
				finally
				{
					con.Close();
				}
			}
			else
			{
				lblItemTitle.Text = "-";
				lblItemValue.Text = "N/A";
			}
		}

		public void fnBindInventoryData()
		{
			string sql = "select (select name from BASE_Product where ProdId=BASE_Inventory.ProdId) as [Name], ";
			sql += "cast(sum(quantity) as numeric(10,0)) as [Total] ";
			sql += "from BASE_Inventory ";
			sql += "where LocationId in (100,104) and prodID not in (269,270) ";
			sql += "group by ProdId";
			dgvInventory.DataSource = cls.bindingSource2;
			cls.GetData(sql, dgvInventory, cls.bindingSource2, cls.dataAdapter2);
			DataGridViewColumn dgvInventory_column = dgvInventory.Columns[0];
			DataGridViewColumn dgvInventory_column2 = dgvInventory.Columns[1];
			dgvInventory_column.Width = 70 * dgvInventoryWidth / 100;
			dgvInventory_column2.Width = 30 * dgvInventoryWidth / 100;
			dgvInventory.RowHeadersVisible = false;
			dgvInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10f, FontStyle.Bold);
			dgvInventory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvInventory.DefaultCellStyle.Font = new Font("Tahoma", 10f);
			dgvInventory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvInventory_column.ReadOnly = true;
			dgvInventory_column2.ReadOnly = true;
			dgvInventory_column.Visible = true;
			dgvInventory_column2.Visible = true;
		}

		private void dgvInventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			fnSetDatagridRowColor(dgvInventory);
		}

		private void dgvOrderScanOut_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex == 8;
			if (flag)
			{
				DataGridViewRow rowScanOut = dgvOrderScanOut.Rows[e.RowIndex];
				string partID = rowScanOut.Cells[0].Value.ToString();
				string boxcode = rowScanOut.Cells[1].Value.ToString();
				string packingdate = rowScanOut.Cells[2].Value.ToString();
				string boxLOT = rowScanOut.Cells[3].Value.ToString();
				string boxquantity = rowScanOut.Cells[4].Value.ToString();
				string boxpartno = rowScanOut.Cells[5].Value.ToString();
				string outstockfinishdate = rowScanOut.Cells[6].Value.ToString();
				string outstockID = rowScanOut.Cells[7].Value.ToString();
			}
		}

		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && components != null;
			if (flag)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
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

        public static void fnResetTimer(System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        public static int getWidth(DataGridView dgv)
        {
            return dgv.Width;
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



        private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblOrderTitle = new System.Windows.Forms.Label();
            this.dgvOrderDetail = new System.Windows.Forms.DataGridView();
            this.txtBoxcode = new System.Windows.Forms.TextBox();
            this.dgvOrderScanOut = new System.Windows.Forms.DataGridView();
            this.lblOrderQuantity = new System.Windows.Forms.Label();
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOrderRequest = new System.Windows.Forms.Label();
            this.lblOrderRespond = new System.Windows.Forms.Label();
            this.lblOrderSubTotal = new System.Windows.Forms.Label();
            this.lblOrderCurrent = new System.Windows.Forms.Label();
            this.lblOrderRemain = new System.Windows.Forms.Label();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblOrderPercent = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.dgvOrderList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.lblItemTitle = new System.Windows.Forms.Label();
            this.lblItemValue = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderScanOut)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderList)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblOrderTitle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvOrderDetail, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBoxcode, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvOrderScanOut, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblOrderQuantity, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblOrderInfo, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblDateTime, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvOrderList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1350, 730);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(273, 3);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(804, 67);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "INVENTORY OUT-STOCK SYSTEM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderTitle
            // 
            this.lblOrderTitle.AutoSize = true;
            this.lblOrderTitle.BackColor = System.Drawing.Color.YellowGreen;
            this.lblOrderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderTitle.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTitle.Location = new System.Drawing.Point(273, 76);
            this.lblOrderTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderTitle.Name = "lblOrderTitle";
            this.lblOrderTitle.Size = new System.Drawing.Size(804, 30);
            this.lblOrderTitle.TabIndex = 4;
            this.lblOrderTitle.Text = "DANH SÁCH HÀNG YÊU CẦU";
            this.lblOrderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvOrderDetail
            // 
            this.dgvOrderDetail.AllowUserToAddRows = false;
            this.dgvOrderDetail.AllowUserToDeleteRows = false;
            this.dgvOrderDetail.AllowUserToResizeColumns = false;
            this.dgvOrderDetail.AllowUserToResizeRows = false;
            this.dgvOrderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetail.Location = new System.Drawing.Point(273, 112);
            this.dgvOrderDetail.MultiSelect = false;
            this.dgvOrderDetail.Name = "dgvOrderDetail";
            this.dgvOrderDetail.ReadOnly = true;
            this.dgvOrderDetail.RowHeadersVisible = false;
            this.dgvOrderDetail.ShowCellErrors = false;
            this.dgvOrderDetail.ShowCellToolTips = false;
            this.dgvOrderDetail.ShowEditingIcon = false;
            this.dgvOrderDetail.ShowRowErrors = false;
            this.dgvOrderDetail.Size = new System.Drawing.Size(804, 249);
            this.dgvOrderDetail.TabIndex = 5;
            this.dgvOrderDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderDetail_CellClick);
            this.dgvOrderDetail.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOrderDetail_DataBindingComplete);
            // 
            // txtBoxcode
            // 
            this.txtBoxcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBoxcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxcode.Enabled = false;
            this.txtBoxcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxcode.Location = new System.Drawing.Point(273, 367);
            this.txtBoxcode.Name = "txtBoxcode";
            this.txtBoxcode.Size = new System.Drawing.Size(804, 35);
            this.txtBoxcode.TabIndex = 6;
            this.txtBoxcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxcode_KeyDown);
            // 
            // dgvOrderScanOut
            // 
            this.dgvOrderScanOut.AllowUserToAddRows = false;
            this.dgvOrderScanOut.AllowUserToDeleteRows = false;
            this.dgvOrderScanOut.AllowUserToResizeColumns = false;
            this.dgvOrderScanOut.AllowUserToResizeRows = false;
            this.dgvOrderScanOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderScanOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderScanOut.Location = new System.Drawing.Point(273, 403);
            this.dgvOrderScanOut.MultiSelect = false;
            this.dgvOrderScanOut.Name = "dgvOrderScanOut";
            this.dgvOrderScanOut.ReadOnly = true;
            this.dgvOrderScanOut.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.dgvOrderScanOut, 2);
            this.dgvOrderScanOut.ShowCellErrors = false;
            this.dgvOrderScanOut.ShowCellToolTips = false;
            this.dgvOrderScanOut.ShowEditingIcon = false;
            this.dgvOrderScanOut.ShowRowErrors = false;
            this.dgvOrderScanOut.Size = new System.Drawing.Size(804, 324);
            this.dgvOrderScanOut.TabIndex = 5;
            this.dgvOrderScanOut.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderScanOut_CellClick);
            this.dgvOrderScanOut.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOrderScanOut_DataBindingComplete);
            // 
            // lblOrderQuantity
            // 
            this.lblOrderQuantity.AutoSize = true;
            this.lblOrderQuantity.BackColor = System.Drawing.Color.Khaki;
            this.lblOrderQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderQuantity.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderQuantity.Location = new System.Drawing.Point(1083, 76);
            this.lblOrderQuantity.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderQuantity.Name = "lblOrderQuantity";
            this.lblOrderQuantity.Size = new System.Drawing.Size(264, 30);
            this.lblOrderQuantity.TabIndex = 7;
            this.lblOrderQuantity.Text = "THÔNG TIN SỐ LƯỢNG";
            this.lblOrderQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.AutoSize = true;
            this.lblOrderInfo.BackColor = System.Drawing.Color.Khaki;
            this.lblOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderInfo.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderInfo.Location = new System.Drawing.Point(1083, 367);
            this.lblOrderInfo.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.Size = new System.Drawing.Size(264, 30);
            this.lblOrderInfo.TabIndex = 7;
            this.lblOrderInfo.Text = "THÔNG TIN TỒN KHO";
            this.lblOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.lblOrderRequest, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblOrderRespond, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblOrderSubTotal, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblOrderCurrent, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblOrderRemain, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblOrderTotal, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblOrderPercent, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1080, 109);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(270, 255);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // lblOrderRequest
            // 
            this.lblOrderRequest.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblOrderRequest, 3);
            this.lblOrderRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderRequest.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderRequest.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblOrderRequest.Location = new System.Drawing.Point(3, 3);
            this.lblOrderRequest.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderRequest.Name = "lblOrderRequest";
            this.lblOrderRequest.Size = new System.Drawing.Size(141, 19);
            this.lblOrderRequest.TabIndex = 0;
            this.lblOrderRequest.Text = "Số lượng yêu cầu:";
            this.lblOrderRequest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderRespond
            // 
            this.lblOrderRespond.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblOrderRespond, 3);
            this.lblOrderRespond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderRespond.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderRespond.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblOrderRespond.Location = new System.Drawing.Point(3, 104);
            this.lblOrderRespond.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderRespond.Name = "lblOrderRespond";
            this.lblOrderRespond.Size = new System.Drawing.Size(141, 19);
            this.lblOrderRespond.TabIndex = 0;
            this.lblOrderRespond.Text = "Số lượng đáp ứng:";
            this.lblOrderRespond.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderSubTotal
            // 
            this.lblOrderSubTotal.AutoSize = true;
            this.lblOrderSubTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderSubTotal.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderSubTotal.Location = new System.Drawing.Point(3, 28);
            this.lblOrderSubTotal.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderSubTotal.Name = "lblOrderSubTotal";
            this.lblOrderSubTotal.Size = new System.Drawing.Size(115, 70);
            this.lblOrderSubTotal.TabIndex = 1;
            this.lblOrderSubTotal.Text = "25.648";
            this.lblOrderSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderCurrent
            // 
            this.lblOrderCurrent.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblOrderCurrent, 4);
            this.lblOrderCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderCurrent.Font = new System.Drawing.Font("Times New Roman", 70F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderCurrent.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblOrderCurrent.Location = new System.Drawing.Point(3, 129);
            this.lblOrderCurrent.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderCurrent.Name = "lblOrderCurrent";
            this.lblOrderCurrent.Size = new System.Drawing.Size(264, 123);
            this.lblOrderCurrent.TabIndex = 1;
            this.lblOrderCurrent.Text = "9.999";
            this.lblOrderCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderRemain
            // 
            this.lblOrderRemain.AutoSize = true;
            this.lblOrderRemain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderRemain.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderRemain.ForeColor = System.Drawing.Color.DarkOrchid;
            this.lblOrderRemain.Location = new System.Drawing.Point(150, 104);
            this.lblOrderRemain.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderRemain.Name = "lblOrderRemain";
            this.lblOrderRemain.Size = new System.Drawing.Size(117, 19);
            this.lblOrderRemain.TabIndex = 0;
            this.lblOrderRemain.Text = "150";
            this.lblOrderRemain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderTotal.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTotal.Location = new System.Drawing.Point(150, 28);
            this.lblOrderTotal.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(117, 70);
            this.lblOrderTotal.TabIndex = 1;
            this.lblOrderTotal.Text = "15.000";
            this.lblOrderTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label11, 2);
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(124, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 76);
            this.label11.TabIndex = 2;
            this.label11.Text = "/";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderPercent
            // 
            this.lblOrderPercent.AutoSize = true;
            this.lblOrderPercent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderPercent.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderPercent.ForeColor = System.Drawing.Color.DeepPink;
            this.lblOrderPercent.Location = new System.Drawing.Point(150, 3);
            this.lblOrderPercent.Margin = new System.Windows.Forms.Padding(3);
            this.lblOrderPercent.Name = "lblOrderPercent";
            this.lblOrderPercent.Size = new System.Drawing.Size(117, 19);
            this.lblOrderPercent.TabIndex = 0;
            this.lblOrderPercent.Text = "25%";
            this.lblOrderPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateTime.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.Location = new System.Drawing.Point(1083, 3);
            this.lblDateTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(264, 67);
            this.lblDateTime.TabIndex = 2;
            this.lblDateTime.Text = "label2";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvOrderList
            // 
            this.dgvOrderList.AllowUserToAddRows = false;
            this.dgvOrderList.AllowUserToDeleteRows = false;
            this.dgvOrderList.AllowUserToResizeColumns = false;
            this.dgvOrderList.AllowUserToResizeRows = false;
            this.dgvOrderList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderList.Location = new System.Drawing.Point(3, 3);
            this.dgvOrderList.MultiSelect = false;
            this.dgvOrderList.Name = "dgvOrderList";
            this.dgvOrderList.ReadOnly = true;
            this.dgvOrderList.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.dgvOrderList, 6);
            this.dgvOrderList.ShowCellErrors = false;
            this.dgvOrderList.ShowCellToolTips = false;
            this.dgvOrderList.ShowEditingIcon = false;
            this.dgvOrderList.ShowRowErrors = false;
            this.dgvOrderList.Size = new System.Drawing.Size(264, 724);
            this.dgvOrderList.TabIndex = 3;
            this.dgvOrderList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderList_CellClick);
            this.dgvOrderList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOrderList_DataBindingComplete);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.dgvInventory, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblItemTitle, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblItemValue, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1080, 400);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 10;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(270, 330);
            this.tableLayoutPanel3.TabIndex = 11;
            // 
            // dgvInventory
            // 
            this.dgvInventory.AllowUserToAddRows = false;
            this.dgvInventory.AllowUserToDeleteRows = false;
            this.dgvInventory.AllowUserToResizeColumns = false;
            this.dgvInventory.AllowUserToResizeRows = false;
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel3.SetColumnSpan(this.dgvInventory, 5);
            this.dgvInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInventory.Location = new System.Drawing.Point(3, 36);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.ReadOnly = true;
            this.tableLayoutPanel3.SetRowSpan(this.dgvInventory, 9);
            this.dgvInventory.RowTemplate.Height = 35;
            this.dgvInventory.Size = new System.Drawing.Size(264, 291);
            this.dgvInventory.TabIndex = 10;
            this.dgvInventory.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvInventory_DataBindingComplete);
            // 
            // lblItemTitle
            // 
            this.lblItemTitle.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblItemTitle, 3);
            this.lblItemTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemTitle.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemTitle.Location = new System.Drawing.Point(3, 3);
            this.lblItemTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblItemTitle.Name = "lblItemTitle";
            this.lblItemTitle.Size = new System.Drawing.Size(156, 27);
            this.lblItemTitle.TabIndex = 11;
            this.lblItemTitle.Text = "label1";
            this.lblItemTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemValue
            // 
            this.lblItemValue.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblItemValue, 2);
            this.lblItemValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemValue.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemValue.Location = new System.Drawing.Point(165, 3);
            this.lblItemValue.Margin = new System.Windows.Forms.Padding(3);
            this.lblItemValue.Name = "lblItemValue";
            this.lblItemValue.Size = new System.Drawing.Size(102, 27);
            this.lblItemValue.TabIndex = 11;
            this.lblItemValue.Text = "0";
            this.lblItemValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmInventoryOutStock3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "frmInventoryOutStock3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INVENTORY OUT-STOCK v2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInventoryOutStock3_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderScanOut)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderList)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
