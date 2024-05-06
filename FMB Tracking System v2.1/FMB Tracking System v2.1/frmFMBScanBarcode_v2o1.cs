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
    public partial class frmFMBScanBarcode_v2o1 : Form
    {

        int _dgv_FMB_IN_List_Width, _dgv_FMB_OUT_List_Width;

        string _fmbWeight = "", _fmbMatIDx = "", _fmbMatName = "";
        string _mchName = "", _mchNum = "";


        string _msgText = "";
        int _msgType = 0;
        public DateTime _dt;
        Timer timer = new Timer();
        Timer timer2 = new Timer();

        public frmFMBScanBarcode_v2o1()
        {
            InitializeComponent();
        }

        private void frmFMBScanBarcode_v2o1_Load(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            init();
            //init_FMB_NG();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _dt = DateTime.Now;
            fnGetDate();
        }

        public void init()
        {
            fnGetDate();
            init_FMB_IN();
            init_FMB_OUT();
        }

        public void fnGetDate()
        {
            cls.fnSetDateTime(tssDateTime);
            //lbl_FMB_Status.Text = String.Format("{0:HH:mm:ss}", _dt);
        }

        public void init_FMB_Done()
        {
            //lbl_FMB_Status.Text = "";
            //lbl_FMB_Status.BackColor = Color.FromKnownColor(KnownColor.Control);

            rdb_FMB_75.Checked = false;
            rdb_FMB_150.Checked = false;
            rdb_FMB_225.Checked = false;
            rdb_FMB_300.Checked = false;
            chk_FMB_Mat.Enabled = false;
            //chk_FMB_Mat.ClearSelected();
            for (int i = 0; i < chk_FMB_Mat.Items.Count; ++i)
            {
                chk_FMB_Mat.SetItemChecked(i, false);
            }
            txt_FMB_IN.Text = "";
            txt_FMB_IN.Enabled = false;

            rdb_FMB_R01.Checked =
                rdb_FMB_R02.Checked =
                rdb_FMB_R03.Checked =
                rdb_FMB_R04.Checked =
                rdb_FMB_R05.Checked =
                rdb_FMB_R06.Checked =
                rdb_FMB_R07.Checked =
                rdb_FMB_R08.Checked =
                rdb_FMB_R09.Checked =
                rdb_FMB_R10.Checked =
                rdb_FMB_R11.Checked =
                rdb_FMB_R12.Checked =
                rdb_FMB_R13.Checked =
                rdb_FMB_R14.Checked =
                rdb_FMB_R15.Checked =
                rdb_FMB_R16.Checked =
                rdb_FMB_R17.Checked =
                rdb_FMB_R18.Checked =
                rdb_FMB_R19.Checked =
                rdb_FMB_R20.Checked = false;
            txt_FMB_OUT.Text = "";
            txt_FMB_OUT.Enabled = false;
        }

        private void tssMessage_TextChanged(object sender, EventArgs e)
        {
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            if (tssMessage.Text.Length > 0)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            tssMessage.Text = "";
            tssMessage.BackColor = Color.FromKnownColor(KnownColor.Control);
            tssMessage.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            timer.Stop();
        }

        private void lbl_FMB_Status_TextChanged(object sender, EventArgs e)
        {
            timer2.Interval = 10000;
            timer2.Enabled = true;
            timer2.Tick += new System.EventHandler(this.timer2_Tick);
            if (lbl_FMB_Status.Text.Length > 0)
            {
                timer2.Start();
            }
            else
            {
                timer2.Stop();
            }
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            lbl_FMB_Status.Text = "";
            lbl_FMB_Status.BackColor = Color.FromKnownColor(KnownColor.Control);
            lbl_FMB_Status.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            timer2.Stop();
        }

        public void init_FMB_OK()
        {
            lbl_FMB_Status.Text = "OK";
            lbl_FMB_Status.BackColor = Color.DodgerBlue;
            lbl_FMB_Status.ForeColor = Color.White;
        }

        public void init_FMB_NG()
        {
            lbl_FMB_Status.Text = "NG";
            lbl_FMB_Status.BackColor = Color.Red;
            lbl_FMB_Status.ForeColor = Color.White;
        }


        #region FMB SCAN IN


        public void init_FMB_IN()
        {
            init_FMB_IN_Material();
            init_FMB_IN_List();

            lbl_FMB_Status.Text = "";
            lbl_FMB_Status.BackColor = Color.FromKnownColor(KnownColor.Control);

            rdb_FMB_75.Checked = false;
            rdb_FMB_150.Checked = false;
            rdb_FMB_225.Checked = false;
            rdb_FMB_300.Checked = false;
            chk_FMB_Mat.Enabled = false;
            //chk_FMB_Mat.ClearSelected();
            for (int i = 0; i < chk_FMB_Mat.Items.Count; ++i)
            {
                chk_FMB_Mat.SetItemChecked(i, false);
            }
            txt_FMB_IN.Text = "";
            txt_FMB_IN.Enabled = false;

            rdb_FMB_R01.Checked =
                rdb_FMB_R02.Checked =
                rdb_FMB_R03.Checked =
                rdb_FMB_R04.Checked =
                rdb_FMB_R05.Checked =
                rdb_FMB_R06.Checked =
                rdb_FMB_R07.Checked =
                rdb_FMB_R08.Checked =
                rdb_FMB_R09.Checked =
                rdb_FMB_R10.Checked =
                rdb_FMB_R11.Checked =
                rdb_FMB_R12.Checked =
                rdb_FMB_R13.Checked =
                rdb_FMB_R14.Checked =
                rdb_FMB_R15.Checked =
                rdb_FMB_R16.Checked =
                rdb_FMB_R17.Checked =
                rdb_FMB_R18.Checked =
                rdb_FMB_R19.Checked =
                rdb_FMB_R20.Checked = false;
            txt_FMB_OUT.Text = "";
            txt_FMB_OUT.Enabled = false;

        }

        public void init_FMB_IN_Material()
        {
            string sql = "FMB_Material_List_SelItem_V2o1_Addnew";
            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);
            chk_FMB_Mat.DataSource = dt;
            chk_FMB_Mat.DisplayMember = "matCode";
            chk_FMB_Mat.ValueMember = "matIDx";
            //dt.Rows.InsertAt(dt.NewRow(), 0);
            //chk_FMB_Mat.SelectedIndex = 0;
        }

        public void init_FMB_IN_Weight_Selection()
        {
            if (rdb_FMB_75.Checked)
            {
                _fmbWeight = "75";
            }
            else if (rdb_FMB_150.Checked)
            {
                _fmbWeight = "150";
            }
            else if (rdb_FMB_225.Checked)
            {
                _fmbWeight = "225";
            }
            else if (rdb_FMB_300.Checked)
            {
                _fmbWeight = "300";
            }

            chk_FMB_Mat.Enabled = true;
            chk_FMB_Mat.ClearSelected();
        }

        public void init_FMB_IN_List()
        {
            string sql = "FMB_Material_Packing_List_IN_SelItem_V2o1_Addnew";

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgv_FMB_IN_List_Width = cls.fnGetDataGridWidth(dgv_FMB_IN_List);
            dgv_FMB_IN_List.DataSource = dt;

            dgv_FMB_IN_List.Columns[0].Width = 10 * _dgv_FMB_IN_List_Width / 100;    // STT
            //dgv_FMB_IN_List.Columns[1].Width = 5 * _dgv_FMB_IN_List_Width / 100;    // boxId
            dgv_FMB_IN_List.Columns[2].Width = 30 * _dgv_FMB_IN_List_Width / 100;    // boxcode
            //dgv_FMB_IN_List.Columns[3].Width = 5 * _dgv_FMB_IN_List_Width / 100;    // boxLOT
            //dgv_FMB_IN_List.Columns[4].Width = 15 * _dgv_FMB_IN_List_Width / 100;    // boxpartname
            dgv_FMB_IN_List.Columns[5].Width = 20 * _dgv_FMB_IN_List_Width / 100;    // boxpartno
            dgv_FMB_IN_List.Columns[6].Width = 15 * _dgv_FMB_IN_List_Width / 100;    // boxquantity
            dgv_FMB_IN_List.Columns[7].Width = 25 * _dgv_FMB_IN_List_Width / 100;    // packingdate

            dgv_FMB_IN_List.Columns[0].Visible = true;
            dgv_FMB_IN_List.Columns[1].Visible = false;
            dgv_FMB_IN_List.Columns[2].Visible = true;
            dgv_FMB_IN_List.Columns[3].Visible = false;
            dgv_FMB_IN_List.Columns[4].Visible = false;
            dgv_FMB_IN_List.Columns[5].Visible = true;
            dgv_FMB_IN_List.Columns[6].Visible = true;
            dgv_FMB_IN_List.Columns[7].Visible = true;

            dgv_FMB_IN_List.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgv_FMB_IN_List, 11, 30);
        }

        private void chk_FMB_Mat_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < chk_FMB_Mat.Items.Count; ++i)
            {
                if (i != e.Index)
                {
                    chk_FMB_Mat.SetItemChecked(i, false);
                }
            }
        }

        private void rdb_FMB_75_Click(object sender, EventArgs e)
        {
            init_FMB_IN_Weight_Selection();
        }

        private void rdb_FMB_150_Click(object sender, EventArgs e)
        {
            init_FMB_IN_Weight_Selection();
        }

        private void rdb_FMB_225_Click(object sender, EventArgs e)
        {
            init_FMB_IN_Weight_Selection();
        }

        private void rdb_FMB_300_Click(object sender, EventArgs e)
        {
            init_FMB_IN_Weight_Selection();
        }

        private void chk_FMB_Mat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = chk_FMB_Mat.SelectedIndex;
            if (sel != -1)
            {
                CheckState chkState = chk_FMB_Mat.GetItemCheckState(sel);
                if (chkState == CheckState.Checked)
                {
                    txt_FMB_IN.Text = "";
                    txt_FMB_IN.Enabled = true;
                    txt_FMB_IN.Focus();

                    _fmbMatIDx = chk_FMB_Mat.SelectedValue.ToString();
                    _fmbMatName = chk_FMB_Mat.Text;
                }
                else
                {
                    txt_FMB_IN.Text = "";
                    txt_FMB_IN.Enabled = false;

                    _fmbMatIDx = "";
                    _fmbMatName = "";
                }
            }
        }

        private void txt_FMB_IN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string pakWeight = _fmbWeight;
                    string matIDx = _fmbMatIDx;
                    string matName = _fmbMatName;
                    string matLot = cls.fnGetDate("ls");
                    string packing = txt_FMB_IN.Text.Trim();
                    if (packing.Length >= 12)
                    {
                        string packKind = packing.Substring(0, 3).ToUpper();
                        string packType = packing.Substring(4, 3).ToUpper();
                        string packCode = packing.Substring(8);

                        //MessageBox.Show(packCode + "|" + packType + "|" + packNum);
                        if (packKind == "PRO" || packKind == "BOX")
                        {
                            if (packType == "069" || packType == "CMF")
                            {
                                string sqlChk = "FMB_Material_IN_Exist_ChkItem_V2o1_Addnew";

                                SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                                sParamsChk[0] = new SqlParameter();
                                sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                                sParamsChk[0].ParameterName = "@packing";
                                sParamsChk[0].Value = packing;

                                DataSet ds = new DataSet();
                                ds = cls.ExecuteDataSet(sqlChk, sParamsChk);

                                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                                {
                                    //string sql = "FMB_Material_Packing_IN_AddItem_V2o1_Addnew";
                                    string sql = "FMB_Material_Packing_IN_AddItem_V2o2_Addnew";

                                    SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                                    sParams[0] = new SqlParameter();
                                    sParams[0].SqlDbType = SqlDbType.VarChar;
                                    sParams[0].ParameterName = "@boxcode";
                                    sParams[0].Value = packing;

                                    sParams[1] = new SqlParameter();
                                    sParams[1].SqlDbType = SqlDbType.VarChar;
                                    sParams[1].ParameterName = "@boxLOT";
                                    sParams[1].Value = matLot;

                                    sParams[2] = new SqlParameter();
                                    sParams[2].SqlDbType = SqlDbType.SmallInt;
                                    sParams[2].ParameterName = "@boxquantity";
                                    sParams[2].Value = pakWeight;

                                    sParams[3] = new SqlParameter();
                                    sParams[3].SqlDbType = SqlDbType.Int;
                                    sParams[3].ParameterName = "@prodIDx";
                                    sParams[3].Value = matIDx;

                                    cls.fnUpdDel(sql, sParams);

                                    init_FMB_OK();

                                    _msgText = "Nhập kiện vào phòng ủ thành công.";
                                    _msgType = 1;
                                }
                                else
                                {
                                    init_FMB_NG();

                                    _msgText = "Mã kiện này đã có trên hệ thống. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                                    _msgType = 2;
                                }
                            }
                            else
                            {
                                init_FMB_NG();

                                _msgText = "Mã kiện không đúng loại cho FMB. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                                _msgType = 2;
                            }
                        }
                        else
                        {
                            init_FMB_NG();

                            _msgText = "Mã kiện không đúng loại cho FMB. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                            _msgType = 2;
                        }
                    }
                    else
                    {
                        init_FMB_NG();

                        _msgText = "Mã kiện không đúng. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                        _msgType = 2;
                    }
                }
                catch (SqlException sqlEx)
                {
                    init_FMB_NG();

                    _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    init_FMB_NG();

                    _msgText = "Có lỗi phát sinh. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                    _msgType = 2;
                }
                finally
                {
                    init_FMB_Done();
                    init_FMB_IN_List();

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }



        #endregion


        #region FMB SCAN OUT



        public void init_FMB_OUT()
        {
            init_FMB_OUT_List();

            lbl_FMB_Status.Text = "";
            lbl_FMB_Status.BackColor = Color.FromKnownColor(KnownColor.Control);

            rdb_FMB_75.Checked = false;
            rdb_FMB_150.Checked = false;
            rdb_FMB_225.Checked = false;
            rdb_FMB_300.Checked = false;
            chk_FMB_Mat.Enabled = false;
            //chk_FMB_Mat.ClearSelected();
            for (int i = 0; i < chk_FMB_Mat.Items.Count; ++i)
            {
                chk_FMB_Mat.SetItemChecked(i, false);
            }
            txt_FMB_IN.Text = "";
            txt_FMB_IN.Enabled = false;

            rdb_FMB_R01.Checked =
                rdb_FMB_R02.Checked =
                rdb_FMB_R03.Checked =
                rdb_FMB_R04.Checked =
                rdb_FMB_R05.Checked =
                rdb_FMB_R06.Checked =
                rdb_FMB_R07.Checked =
                rdb_FMB_R08.Checked =
                rdb_FMB_R09.Checked =
                rdb_FMB_R10.Checked =
                rdb_FMB_R11.Checked =
                rdb_FMB_R12.Checked =
                rdb_FMB_R13.Checked =
                rdb_FMB_R14.Checked =
                rdb_FMB_R15.Checked =
                rdb_FMB_R16.Checked =
                rdb_FMB_R17.Checked =
                rdb_FMB_R18.Checked =
                rdb_FMB_R19.Checked =
                rdb_FMB_R20.Checked = false;
            txt_FMB_OUT.Text = "";
            txt_FMB_OUT.Enabled = false;
        }

        public void init_FMB_OUT_Machine()
        {
            _mchName = "RUBBER";
            if (rdb_FMB_R01.Checked)
            {
                _mchNum = "01";
            }
            else if (rdb_FMB_R02.Checked)
            {
                _mchNum = "02";
            }
            else if (rdb_FMB_R03.Checked)
            {
                _mchNum = "03";
            }
            else if (rdb_FMB_R04.Checked)
            {
                _mchNum = "04";
            }
            else if (rdb_FMB_R05.Checked)
            {
                _mchNum = "05";
            }
            else if (rdb_FMB_R06.Checked)
            {
                _mchNum = "06";
            }
            else if (rdb_FMB_R07.Checked)
            {
                _mchNum = "07";
            }
            else if (rdb_FMB_R08.Checked)
            {
                _mchNum = "08";
            }
            else if (rdb_FMB_R09.Checked)
            {
                _mchNum = "09";
            }
            else if (rdb_FMB_R10.Checked)
            {
                _mchNum = "10";
            }
            else if (rdb_FMB_R11.Checked)
            {
                _mchNum = "11";
            }
            else if (rdb_FMB_R12.Checked)
            {
                _mchNum = "12";
            }
            else if (rdb_FMB_R13.Checked)
            {
                _mchNum = "13";
            }
            else if (rdb_FMB_R14.Checked)
            {
                _mchNum = "14";
            }
            else if (rdb_FMB_R15.Checked)
            {
                _mchNum = "15";
            }
            else if (rdb_FMB_R16.Checked)
            {
                _mchNum = "16";
            }
            else if (rdb_FMB_R17.Checked)
            {
                _mchNum = "17";
            }
            else if (rdb_FMB_R18.Checked)
            {
                _mchNum = "18";
            }
            else if (rdb_FMB_R19.Checked)
            {
                _mchNum = "19";
            }
            else if (rdb_FMB_R20.Checked)
            {
                _mchNum = "20";
            }

            txt_FMB_OUT.Text = "";
            txt_FMB_OUT.Enabled = true;
            txt_FMB_OUT.Focus();
        }

        public void init_FMB_OUT_List()
        {
            string sql = "FMB_Material_Packing_List_OUT_SelItem_V2o2_Addnew";
            string fifo = "";
            int _fifo = 0;

            DataTable dt = new DataTable();
            dt = cls.fnSelect(sql);

            _dgv_FMB_OUT_List_Width = cls.fnGetDataGridWidth(dgv_FMB_OUT_List);
            dgv_FMB_OUT_List.DataSource = dt;

            dgv_FMB_OUT_List.Columns[0].Width = 10 * _dgv_FMB_OUT_List_Width / 100;    // STT
            //dgv_FMB_OUT_List.Columns[1].Width = 5 * _dgv_FMB_OUT_List_Width / 100;    // boxId
            dgv_FMB_OUT_List.Columns[2].Width = 25 * _dgv_FMB_OUT_List_Width / 100;    // boxcode
            //dgv_FMB_OUT_List.Columns[3].Width = 5 * _dgv_FMB_OUT_List_Width / 100;    // boxLOT
            //dgv_FMB_OUT_List.Columns[4].Width = 15 * _dgv_FMB_OUT_List_Width / 100;    // boxpartname
            //dgv_FMB_OUT_List.Columns[5].Width = 15 * _dgv_FMB_OUT_List_Width / 100;    // boxpartno
            //dgv_FMB_OUT_List.Columns[6].Width = 10 * _dgv_FMB_OUT_List_Width / 100;    // boxquantity
            dgv_FMB_OUT_List.Columns[7].Width = 25 * _dgv_FMB_OUT_List_Width / 100;    // packingdate
            dgv_FMB_OUT_List.Columns[8].Width = 25 * _dgv_FMB_OUT_List_Width / 100;    // transferdate
            dgv_FMB_OUT_List.Columns[9].Width = 15 * _dgv_FMB_OUT_List_Width / 100;    // coolingTime
            //dgv_FMB_OUT_List.Columns[10].Width = 15 * _dgv_FMB_OUT_List_Width / 100;    // fifo

            dgv_FMB_OUT_List.Columns[0].Visible = true;
            dgv_FMB_OUT_List.Columns[1].Visible = false;
            dgv_FMB_OUT_List.Columns[2].Visible = true;
            dgv_FMB_OUT_List.Columns[3].Visible = false;
            dgv_FMB_OUT_List.Columns[4].Visible = false;
            dgv_FMB_OUT_List.Columns[5].Visible = false;
            dgv_FMB_OUT_List.Columns[6].Visible = false;
            dgv_FMB_OUT_List.Columns[7].Visible = true;
            dgv_FMB_OUT_List.Columns[8].Visible = true;
            dgv_FMB_OUT_List.Columns[9].Visible = true;
            dgv_FMB_OUT_List.Columns[10].Visible = false;

            dgv_FMB_OUT_List.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgv_FMB_OUT_List.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            cls.fnFormatDatagridview(dgv_FMB_OUT_List, 11, 30);

            foreach(DataGridViewRow row in dgv_FMB_OUT_List.Rows)
            {
                fifo = row.Cells[10].Value.ToString();
                _fifo = (fifo != "" && fifo != null) ? Convert.ToInt32(fifo) : 0;

                if (_fifo > 0)
                {
                    row.DefaultCellStyle.BackColor = (_fifo == 1) ? Color.LimeGreen : Color.Pink;
                }
            }
        }

        private void txt_FMB_OUT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string mcName = _mchName;
                    string mcCode = _mchNum;
                    string packing = txt_FMB_OUT.Text.Trim();
                    string coolTime = "", boxRank = "", boxCode = "", boxDtIn = "";
                    int _boxRank = 0;
                    DateTime _boxDtIn;
                    if (packing.Length >= 12)
                    {
                        string packKind = packing.Substring(0, 3).ToUpper();
                        string packType = packing.Substring(4, 3).ToUpper();
                        string packCode = packing.Substring(8);

                        //MessageBox.Show(packCode + "|" + packType + "|" + packNum);
                        if (packKind == "PRO" || packKind == "BOX")
                        {
                            if (packType == "069" || packType == "CMF")
                            {
                                string sqlChk = "FMB_Material_OUT_Exist_ChkItem_V2o1_Addnew";

                                SqlParameter[] sParamsChk = new SqlParameter[1]; // Parameter count

                                sParamsChk[0] = new SqlParameter();
                                sParamsChk[0].SqlDbType = SqlDbType.VarChar;
                                sParamsChk[0].ParameterName = "@packing";
                                sParamsChk[0].Value = packing;

                                DataSet ds = new DataSet();
                                ds = cls.ExecuteDataSet(sqlChk, sParamsChk);

                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    coolTime = ds.Tables[0].Rows[0][5].ToString();
                                    boxRank = ds.Tables[0].Rows[0][6].ToString();
                                    boxCode = ds.Tables[0].Rows[0][7].ToString();
                                    boxDtIn = ds.Tables[0].Rows[0][8].ToString();

                                    decimal _coolTime = (coolTime != "" && coolTime != null) ? Convert.ToDecimal(coolTime) : 0;
                                    _boxRank = Convert.ToInt32(boxRank);
                                    _boxDtIn = (_boxRank > 0) ? Convert.ToDateTime(boxDtIn) : DateTime.Now;
                                    
                                    if (_coolTime >= 21600)
                                    {
                                        if (_boxRank == 0)
                                        {
                                            //string sql = "FMB_Material_Packing_OUT_AddItem_V2o2_Addnew";
                                            string sql = "FMB_Material_Packing_OUT_AddItem_V2o3_Addnew";

                                            SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                                            sParams[0] = new SqlParameter();
                                            sParams[0].SqlDbType = SqlDbType.VarChar;
                                            sParams[0].ParameterName = "@packing";
                                            sParams[0].Value = packing;

                                            sParams[1] = new SqlParameter();
                                            sParams[1].SqlDbType = SqlDbType.VarChar;
                                            sParams[1].ParameterName = "@mcName";
                                            sParams[1].Value = mcName;

                                            sParams[2] = new SqlParameter();
                                            sParams[2].SqlDbType = SqlDbType.VarChar;
                                            sParams[2].ParameterName = "@mcCode";
                                            sParams[2].Value = mcCode;

                                            sParams[3] = new SqlParameter();
                                            sParams[3].SqlDbType = SqlDbType.TinyInt;
                                            sParams[3].ParameterName = "@fifo";
                                            sParams[3].Value = 1;

                                            cls.fnUpdDel(sql, sParams);

                                            init_FMB_OK();

                                            _msgText = "Xuất kiện ra máy sản xuất thành công.";
                                            _msgType = 1;
                                        }
                                        else
                                        {
                                            init_FMB_NG();

                                            string msg = "";
                                            msg = "Có 1 kiện cần xuất trước kiện này!!!\r\n\r\n";
                                            msg += "Hãy tìm kiện có mã " + boxCode + " nhập kho ngày " + String.Format("{0:dd/MM/yyyy HH:mm}", _boxDtIn) + " để xuất kho trước\r\n\r\n";
                                            msg += "Nhấn YES để dừng lại và làm theo hướng dẫn FIFO\r\n";
                                            msg += "Nhấn NO để tiếp tục xuất nguyên liệu ra chuyền sản xuất (không theo FIFO)";
                                            DialogResult dialog = MessageBox.Show(msg, cls.appName(), MessageBoxButtons.YesNo);
                                            if (dialog == DialogResult.No)
                                            {
                                                string sql = "FMB_Material_Packing_OUT_AddItem_V2o2_Addnew";

                                                SqlParameter[] sParams = new SqlParameter[4]; // Parameter count

                                                sParams[0] = new SqlParameter();
                                                sParams[0].SqlDbType = SqlDbType.VarChar;
                                                sParams[0].ParameterName = "@packing";
                                                sParams[0].Value = packing;

                                                sParams[1] = new SqlParameter();
                                                sParams[1].SqlDbType = SqlDbType.VarChar;
                                                sParams[1].ParameterName = "@mcName";
                                                sParams[1].Value = mcName;

                                                sParams[2] = new SqlParameter();
                                                sParams[2].SqlDbType = SqlDbType.VarChar;
                                                sParams[2].ParameterName = "@mcCode";
                                                sParams[2].Value = mcCode;

                                                sParams[3] = new SqlParameter();
                                                sParams[3].SqlDbType = SqlDbType.TinyInt;
                                                sParams[3].ParameterName = "@fifo";
                                                sParams[3].Value = 2;

                                                cls.fnUpdDel(sql, sParams);

                                                init_FMB_OK();

                                                _msgText = "Xuất kiện ra máy sản xuất thành công nhưng không theo FIFO.";
                                                _msgType = 1;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Hãy tìm kiện có mã " + boxCode + " nhập kho ngày " + String.Format("{0:dd/MM/yyyy HH:mm}", _boxDtIn) + " để xuất kho trước");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        init_FMB_NG();

                                        _msgText = "Mã kiện này chưa đủ thời gian ủ trên hệ thống (" + String.Format("{0:0.0}", _coolTime / 3600) + " h) nên sẽ không xuất ra máy sản xuất được.";
                                        _msgType = 2;
                                    }
                                }
                                else
                                {
                                    init_FMB_NG();

                                    _msgText = "Mã kiện này không có trên hệ thống. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                                    _msgType = 2;
                                }
                            }
                            else
                            {
                                init_FMB_NG();

                                _msgText = "Mã kiện không đúng loại cho FMB. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                                _msgType = 2;
                            }
                        }
                        else
                        {
                            init_FMB_NG();

                            _msgText = "Mã kiện không đúng loại cho FMB. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                            _msgType = 2;
                        }
                    }
                    else
                    {
                        init_FMB_NG();

                        _msgText = "Mã kiện không đúng. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                        _msgType = 2;
                    }

                }
                catch (SqlException sqlEx)
                {
                    init_FMB_NG();

                    _msgText = "Có lỗi dữ liệu phát sinh. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                    _msgType = 3;
                }
                catch (Exception ex)
                {
                    init_FMB_NG();

                    _msgText = "Có lỗi phát sinh. Vui lòng kiểm tra và thử lại hoặc thay tem khác.";
                    _msgType = 2;
                }
                finally
                {
                    init_FMB_Done();
                    init_FMB_IN_List();
                    init_FMB_OUT_List();

                    cls.fnMessage(tssMessage, _msgText, _msgType);
                }
            }
        }

        private void rdb_FMB_R01_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R02_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R03_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R04_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R05_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R06_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R07_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R08_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R09_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R10_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R11_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R12_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R13_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R14_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R15_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R16_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R17_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R18_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R19_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }

        private void rdb_FMB_R20_MouseClick(object sender, MouseEventArgs e)
        {
            init_FMB_OUT_Machine();
        }



        #endregion

    }
}
