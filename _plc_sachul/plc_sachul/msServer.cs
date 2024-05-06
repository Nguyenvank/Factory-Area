using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Rei
{
    class msServer
    {
        //DB 접속 정보
        string _serverinfo = "";
        string _mdbinfo = "";

        //실행할 쿼리
        public string _query = "";
        public string _mdbquery = "";

        private SqlConnection _conn;
        private OleDbConnection _mdbconn;

        public DataSet _ds;
        public DataSet _mdbds;

        /// <summary>
        /// 생성자
        /// </summary>
        public msServer()
        {

        }

        public void SetDBInfo(string ip, string name)
        {
            // ini 파일의 DB ip로 세팅, DB 계정 정보는 보안상 프로그램 내에 하드코딩하자!
            _serverinfo = "server=" + ip + "; database=" + name + "; user id=sait; password=it4118vcs";
        }
        public void SetMdbInfo()
        {
            _mdbinfo = "Provider = Microsoft.JET.OLEDB.4.0;" + "Data Source = c:\\disconnected.mdb";
        }

        public string State()
        {
            string state;

            //this._conn = new SqlConnection(_serverinfo);

            if (this._conn.State == ConnectionState.Open)
            {
                state = "YES";
            }
            else
            {
                state = "NO";
            }

            return state;
        }

        public void Open()
        {
            try
            {
                this._conn = new SqlConnection(_serverinfo);
                this._conn.Open();
            }
            catch (IndexOutOfRangeException e)
            {
                int a = 0;
            }
            catch (Exception e)
            {
                int a = 0;
            }
        }

        public void MdbOpen()
        {
            try
            {
                this._mdbconn = new OleDbConnection(_mdbinfo);
                this._mdbconn.Open();
            }
            catch (IndexOutOfRangeException e)
            {
                int a = 0;
            }
            catch (Exception e)
            {
                int a = 0;
            }

        }

        public void Close()
        {
            try
            {
                this._conn.Close();
            }
            catch (IndexOutOfRangeException e)
            {
                int a = 0;
            }
            catch (Exception e)
            {
                int a = 0;
            }
        }

        public void MdbClose()
        {
            try
            {
                this._mdbconn.Close();
            }
            catch (IndexOutOfRangeException e)
            {
                int a = 0;
            }
            catch (Exception e)
            {
                int a = 0;
            }
        }
                
        /// <summary>
        /// select 쿼리 실행
        /// </summary>
        public bool execQuery()
        {
            try
            {
                //SqlDataAdapter 객체 생성
                SqlDataAdapter _sda = new SqlDataAdapter();

                _sda.SelectCommand = new SqlCommand(_query, _conn);

                _ds = new DataSet();    //DataSet 객체 생성

                _sda.Fill(_ds); //생성한 DataSet 객체에 Sda 객체의 데이터를 채우기

                if (_ds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool MdbexecQuery()
        {
            try
            {
                OleDbDataAdapter _oda = new OleDbDataAdapter();

                _oda.SelectCommand = new OleDbCommand(_mdbquery, _mdbconn);

                _mdbds = new DataSet();

                _oda.Fill(_mdbds);

                if (_mdbds.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// insert, update 등의 쿼리 실행
        /// </summary>
        public bool execNonQuery()
        {
            try
            {
                SqlCommand _comm = new SqlCommand(_query, _conn);

                if (_comm.ExecuteNonQuery() == -1)  //성공
                {
                    return true;
                }
                else  //실패
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool MdbexecNonQuery()
        {
            try
            {
                OleDbCommand _mdbcomm = new OleDbCommand(_mdbquery, _mdbconn);

                if (_mdbcomm.ExecuteNonQuery() == -1)
                {
                    return true;
                }
                else  //실패
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
