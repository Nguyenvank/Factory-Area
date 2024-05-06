using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Rei
{
    /// <summary>
    ///클래스   : ini 파일을 읽고 쓰는 클래스
    ///작성자   : 변영일(131001)
    ///작성일자 : 2013년 10월 14일
    /// </summary>
    class Ini
    {
        //ini 파일 위치
        private string iniPath;

        bool factory_index;

        /// <summary>
        /// Ini 클래스의 생성자
        /// </summary>
        /// <param name="path">ini 파일의 위치</param>
        public Ini(string path)
        {
            // TODO: Complete member initialization
            this.iniPath = path;
        }

        [DllImport("kernel32.dll")]
        //ini 파일 읽기
        private static extern int GetPrivateProfileString(String section, String key, String def, StringBuilder retVal, int size, String filepath);
        [DllImport("kernel32.dll")]
        //ini 파일 쓰기
        private static extern int WritePrivateProfileString(String section, String key, String val, String filepath);

        //ini 파일 유무
        /// <summary>
        /// ini 파일의 유무를 check
        /// </summary>
        /// <returns>존재 시 true, 없을 경우 false</returns>
        public bool IniExists()
        {
            factory_index = File.Exists(iniPath);

            return factory_index;
        }

        /// <summary>
        /// ini 파일 생성
        /// </summary>
        public void CreateIni()
        {
            File.Create(iniPath).Close();
        }

        /// <summary>
        /// ini 파일에서 값을 읽어옴
        /// </summary>
        /// <param name="section">ini 파일의 섹션</param>
        /// <param name="key">ini 파일의 키</param>
        /// <returns>해당 [섹션, 키]의 값을 반환</returns>
        public string GetIniValue(string section, string key)
        {
            StringBuilder result = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", result, 255, iniPath);
            return result.ToString();
        }


        public string GetIniValue(string section, string key, string value)
        {
            StringBuilder result = new StringBuilder(255);

            try
            {
                int i = GetPrivateProfileString(section, key, "", result, 255, iniPath);

                if (result.ToString() == "")
                {
                    SetIniValue(section, key, value);
                    return value.ToString();
                }
                else {
                    return result.ToString();
                }

            }
            catch (Exception )
            {
                SetIniValue(section, key, value);
                return value.ToString();

            }

        }
        /// <summary>
        /// ini 파일에 값을 씀
        /// </summary>
        /// <param name="section">섹션</param>
        /// <param name="key">키</param>
        /// <param name="val">값</param>
        public void SetIniValue(string section, string key, string val)
        {
            WritePrivateProfileString(section, key, val, iniPath);
        }
    }
}
