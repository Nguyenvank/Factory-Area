using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Dbconnect
{
    class msServer
    {
        //DB 접속 정보
        string _serverinfo = "";

        //실행할 쿼리
        public string _query = "";

        private SqlConnection _conn;

        public DataSet _ds;

        /// <summary>
        /// 생성자
        /// </summary>
        public msServer()
        {

        }

        public void SetDBInfo(string ip, string db)
        {
            // ini 파일의 DB ip로 세팅, DB 계정 정보는 보안상 프로그램 내에 하드코딩하자!
            _serverinfo = "server=" + ip + "; database=" + db + "; user id=vnuser; password=dung2016";
        }

        public void Open()
        {
            this._conn = new SqlConnection(_serverinfo);

            this._conn.Open();
        }

        public void Close()
        {
            this._conn.Close();
        }

        /// <summary>
        /// select 쿼리 실행
        /// </summary>
        public bool execQuery()
        {
            //SqlDataAdapter 객체 생성
            SqlDataAdapter _sda = new SqlDataAdapter();

            _sda.SelectCommand = new SqlCommand(_query, _conn);

            _ds = new DataSet();    //DataSet 객체 생성

            again:
            try
            {
                _sda.Fill(_ds); //생성한 DataSet 객체에 Sda 객체의 데이터를 채우기
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    return false;
                }
                else
                {
                    goto again;
                }
            }

            if (_ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// insert, update 등의 쿼리 실행
        /// </summary>
        public bool execNonQuery()
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
    }
}
