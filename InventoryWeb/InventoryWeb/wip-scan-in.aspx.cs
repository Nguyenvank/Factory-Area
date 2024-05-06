using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inventory_Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using ZXing;
//using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec.Data;
//using com.google.zxing.qrcode;
//using com.google.zxing.qrcode.detector;
//using COMMON = com.google.zxing.common;
//using com.google.zxing;
//using AForge.Video;
//using AForge.Video.DirectShow;
//using OnBarcode.Barcode.BarcodeScanner;
//using static System.Net.Mime.MediaTypeNames;
//using BarcodeLib.BarcodeReader;

namespace InventoryWeb
{
    public partial class wip_scan_in : System.Web.UI.Page
    {
        //string yourcode = textBox1.Text;
        //QRCodeEncoder enc = new QRCodeEncoder();
        //Bitmap qrcode = enc.Encode(yourcode);
        //pictureBox1.Image = qrcode as Image;//Displays generated code in PictureBox

        //QRCodeDecoder dec = new QRCodeDecoder();
        //textBox2.Text = (dec.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap)));


        protected void Page_Load(object sender, EventArgs e)
        {
            lblPartcode.Text = "N/A";
            lblLocation.Text = "N/A";
            lblLOT.Text = "N/A";
            lblPCS.Text = "0";
            lblBOX.Text = "0";
            lblCAR.Text = "0";
            lblPAL.Text = "0";
            flePackcode.Enabled = false;
            btnSave.Enabled = false;

            fnBindProdType();
        }

        public void fnBindProdType()
        {
            if(!IsPostBack)
            {
                try
                {
                    string sql = "V2o1_BASE_Web_Warehouse_WIP_Partname_SelItem_Addnew";
                    DataTable dt = new DataTable();
                    dt = cls.fnSelect(sql);
                    ddlPartname.DataSource = dt;
                    ddlPartname.DataTextField = "Name";
                    ddlPartname.DataValueField = "prodId";
                    ddlPartname.DataBind();
                    ddlPartname.Items.Insert(0, new ListItem("", "0"));
                    ddlPartname.SelectedIndex = 0;
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        public enum BarcodeFormat
        {

            /** Aztec 2D barcode format. */
            AZTEC,

            /** CODABAR 1D format. */
            CODABAR,

            /** Code 39 1D format. */
            CODE_39,

            /** Code 93 1D format. */
            CODE_93,

            /** Code 128 1D format. */
            CODE_128,

            /** Data Matrix 2D barcode format. */
            DATA_MATRIX,

            /** EAN-8 1D format. */
            EAN_8,

            /** EAN-13 1D format. */
            EAN_13,

            /** ITF (Interleaved Two of Five) 1D format. */
            ITF,

            /** MaxiCode 2D barcode format. */
            MAXICODE,

            /** PDF417 format. */
            PDF_417,

            /** QR Code 2D barcode format. */
            QR_CODE,

            /** RSS 14 */
            RSS_14,

            /** RSS EXPANDED */
            RSS_EXPANDED,

            /** UPC-A 1D format. */
            UPC_A,

            /** UPC-E 1D format. */
            UPC_E,

            /** UPC/EAN extension format. Not a stand-alone format. */
            UPC_EAN_EXTENSION

        }

        public void writeQRCode()
        {
            //QRCodeWriter writer = new QRCodeWriter();
            //int width = 256, height = 256;
            //BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB); // create an empty image
            //int white = 255 << 16 | 255 << 8 | 255;
            //int black = 0;
            //try
            //{
            //    BitMatrix bitMatrix = writer.encode("http://www.codepool.biz/zxing-write-read-qrcode.html", BarcodeFormat.QR_CODE, width, height);
            //    for (int i = 0; i < width; i++)
            //    {
            //        for (int j = 0; j < height; j++)
            //        {
            //            image.setRGB(i, j, bitMatrix.get(i, j) ? black : white); // set pixel one by one
            //        }
            //    }

            //    try
            //    {
            //        ImageIO.write(image, "jpg", new File("dynamsoftbarcode.jpg")); // save QR image to disk
            //    }
            //    catch (IOException e)
            //    {
            //        // TODO Auto-generated catch block
            //        e.printStackTrace();
            //    }

            //}
            //catch (WriterException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}
        }

        public String readQRCode(String fileName)
        {
            //File file = new File(fileName);
            //BufferedImage image = null;
            //BinaryBitmap bitmap = null;
            //Result result = null;

            //try
            //{
            //    image = Image.read(file);
            //    int[] pixels = image.getRGB(0, 0, image.getWidth(), image.getHeight(), null, 0, image.getWidth());
            //    RGBLuminanceSource source = new RGBLuminanceSource(image.getWidth(), image.getHeight(), pixels);
            //    bitmap = new BinaryBitmap(new HybridBinarizer(source));
            //}
            //catch (IOException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}

            //if (bitmap == null)
            //    return null;

            //QRCodeReader reader = new QRCodeReader();
            //try
            //{
            //    result = reader.decode(bitmap);
            //    return result.getText();
            //}
            //catch (NotFoundException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}
            //catch (ChecksumException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}
            //catch (FormatException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}

            return null;
        }

        public void ddlPartname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartname.SelectedIndex > 0)
            {
                string pId = ddlPartname.SelectedValue.ToString();

                try
                {
                    string partId = "", partName = "", partCode = "", location = "", lot = "", pcs = "", box = "", car = "", pal = "";
                    string sql = "V2o1_BASE_Web_Warehouse_WIP_PartInfor_SelItem_Addnew";

                    SqlParameter[] sParams = new SqlParameter[1]; // Parameter count
                    sParams[0] = new SqlParameter();
                    sParams[0].SqlDbType = SqlDbType.VarChar;
                    sParams[0].ParameterName = "@partId";
                    sParams[0].Value = pId;

                    DataSet ds = new DataSet();
                    ds = cls.ExecuteDataSet(sql, sParams);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            partId = ds.Tables[0].Rows[0][0].ToString();
                            partName = ds.Tables[0].Rows[0][1].ToString();
                            partCode = ds.Tables[0].Rows[0][2].ToString();
                            location = ds.Tables[0].Rows[0][3].ToString();
                            lot = cls.fnGetDate("lot") + "-" + cls.fnGetDate("sn");
                            pcs = ds.Tables[0].Rows[0][4].ToString();
                            box = ds.Tables[0].Rows[0][5].ToString();
                            car = ds.Tables[0].Rows[0][6].ToString();
                            pal = ds.Tables[0].Rows[0][7].ToString();
                        }
                        else
                        {
                            partId = "0";
                            partName = "N/A";
                            partCode = "N/A";
                            location = "N/A";
                            lot = "N/A";
                            pcs = "0";
                            box = "0";
                            car = "0";
                            pal = "0";
                        }
                    }
                    else
                    {
                        partId = "0";
                        partName = "N/A";
                        partCode = "N/A";
                        location = "N/A";
                        lot = "N/A";
                        pcs = "0";
                        box = "0";
                        car = "0";
                        pal = "0";
                    }
                    lblPartcode.Text = (partCode != "" && partCode != null) ? partCode : "N/A";
                    lblLocation.Text = (location != "" && location != null) ? location : "N/A";
                    lblLOT.Text = lot;
                    lblPCS.Text = (pcs != "" && pcs != null) ? pcs : "0";
                    lblBOX.Text = (box != "" && box != null) ? box : "0";
                    lblCAR.Text = (car != "" && car != null) ? car : "0";
                    lblPAL.Text = (pal != "" && pal != null) ? pal : "0";

                    if (location != "N/A" && (pcs != "0" || box != "0" || car != "0" || pal != "0"))
                    {
                        flePackcode.Enabled = true;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        flePackcode.Enabled = false;
                        btnSave.Enabled = false;
                    }
                }
                catch
                {

                }
                finally
                {

                }
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if(flePackcode.FileName!="")
            {
                string suff = cls.fnGetDate("lot") + cls.fnGetDate("t").Replace(":", "");
                string partId = ddlPartname.SelectedValue.ToString();
                string partName = ddlPartname.Text;
                string partCode = lblPartcode.Text;
                string location = lblLocation.Text;
                string lot = lblLOT.Text;
                string pcs = lblPCS.Text;
                string box = lblBOX.Text;
                string car = lblCAR.Text;
                string pal = lblPAL.Text;
                string packCode = "", content = "";

                string filename = Path.Combine(System.Web.HttpContext.Current.Request.MapPath("~/images/upload/"), "packcode-" + suff + ".jpg");
                flePackcode.SaveAs(filename);

                //QRCodeDecoder dec = new QRCodeDecoder();
                //textBox2.Text = (dec.decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap)));

                //QRCodeDecoder dec = new QRCodeDecoder();

                //System.Drawing.Bitmap bitmapimage = new System.Drawing.Bitmap(filename);

                //string packCode = (dec.decode(new QRCodeBitmapImage(bitmapimage as Bitmap)));

                //IBarcodeReader reader = new BarcodeReader();
                //var barcodeBitmap = (Bitmap)Bitmap.FromFile(filename);
                //var result = reader.Decode(barcodeBitmap);
                //// do something with the result
                //if (result != null)
                //{
                //    packCode = result.BarcodeFormat.ToString();
                //    content = result.Text;
                //}

                //OptimizeSetting setting = new OptimizeSetting();
                //setting.setMaxOneBarcodePerPage(true);
                //ScanArea top20 = new ScanArea(new PointF(0.0F, 0.0F), new PointF(100.0F, 20.0F));
                //ScanArea bottom20 = new ScanArea(new PointF(0.0F, 80.0F), new PointF(100.0F, 100.0F));
                //List<ScanArea> areas = new List<ScanArea>();
                //areas.Add(top20);
                //areas.Add(bottom20);
                //setting.setAreas(areas);

                //string[] results = BarcodeReader.read(filename, BarcodeReader.QRCODE);
                //string[] datas = BarcodeScanner.Scan(filename, BarcodeType.QRCode);
                //packCode = string.Join("", datas);

                //MsgBox(packCode, this.Page, this);
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}