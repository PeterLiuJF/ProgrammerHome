using System;
using System.Text;
namespace ToolLib.util
{
    /// <summary>
    /// 公共函数类,主要处理各种数据类型的转换,
    /// </summary>
    public abstract class ConvertUtil
    {
        /// <summary>
        /// 1.1 类型转换string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str0"></param>
        /// <returns></returns>
        public static string ParseString(object obj, string str0)
        {
            string result = "";
            if (obj == null)
                result = str0;
            else
                result = obj.ToString();
            return result;
        }

        /// <summary>
        /// 1.2 类型转换string(默认)
        /// 先处理null,再转换为string
        /// </summary>
        /// <param name="obj">要处理的数据</param>
        /// <returns>返回处理后的数据</returns>
        public static string ParseString(object obj)
        {
            string result = "";
            if (obj == null)
                result = "";
            else
                result = obj.ToString();
            return result;
        }

        /// <summary>
        /// 2.1 类型转换cookie
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str0"></param>
        /// <returns></returns>
        public static string ParseCookie(System.Web.HttpCookie obj, string str0)
        {
            string result = "";
            if (obj == null)
                result = str0;
            else
                result = Normal.ParseString(obj.Value);//.ToString();
            return result;
        }

        /// <summary>
        /// 2.2 类型转换cookie(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParseCookie(System.Web.HttpCookie obj)
        {
            string result = "";
            if (obj != null)
                result = Normal.ParseString(obj.Value);//.ToString();
            return result;
        }

        /// <summary>
        /// 3.1 类型转换short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj0"></param>
        /// <returns></returns>
        public static short ParseShort(object obj, short obj0)
        {
            short result = obj0;
            try
            {
                result = Int16.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 3.2 类型转换short(默认)
        /// </summary>
        /// <param name="obj">要处理的数据</param>
        /// <returns>返回处理后的数据</returns>
        public static short ParseShort(object obj)
        {
            short result = 0;
            try
            {
                result = Int16.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 4.1 类型转换int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj0"></param>
        /// <returns></returns>
        public static int ParseInt(object obj, int obj0)
        {
            int result = obj0;
            try
            {
                result = Int32.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 4.2 类型转换int(默认)
        /// </summary>
        /// <param name="obj">要处理的数据</param>
        /// <returns>返回处理后的数据</returns>
        public static int ParseInt(object obj)
        {
            int result = 0;
            try
            {
                result = Int32.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        
        /// <summary>
        /// 5.1 类型转换long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj0"></param>
        /// <returns></returns>
        public static long ParseLong(object obj, long obj0)
        {
            long result = obj0;
            try
            {
                result = Int64.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 5.2 类型转换long(默认)
        /// </summary>
        /// <param name="obj">要处理的数据</param>
        /// <returns>返回处理后的数据</returns>
        public static long ParseLong(object obj)
        {
            long result = 0;
            try
            {
                result = Int64.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

       

        /// <summary>
        /// 6.1 类型转换float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj0"></param>
        /// <returns></returns>
        public static float ParseFloat(object obj, float obj0)
        {
            float result = obj0;
            try
            {
                result = Single.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 6.2 类型转换float(默认)
        /// </summary>
        /// <param name="obj">要处理的数据</param>
        /// <returns>返回处理后的数据</returns>
        public static float ParseFloat(object obj)
        {
            float result = 0;
            try
            {
                result = Single.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        

        /// <summary>
        /// 7.1 类型转换double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj0"></param>
        /// <returns></returns>
        public static double ParseDouble(object obj, double obj0)
        {
            double result = obj0;
            try
            {
                result = Double.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 7.2 类型转换double(默认)
        /// </summary>
        /// <param name="obj">要处理的数据</param>
        /// <returns>返回处理后的数据</returns>
        public static double ParseDouble(object obj)
        {
            double result = 0;
            try
            {
                result = Double.Parse(ParseString(obj));
            }
            catch
            {
            }
            return result;
        }

        

        /// <summary>
        /// 8.1 类型转换datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj1"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(object obj, DateTime obj0)
        {
            DateTime result = obj0;
            try
            {
                result = DateTime.Parse(ParseString(obj));
            }
            catch { }
            return result;
        }


        /// <summary>
        /// 8.2 类型转换datetime(默认)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(object obj)
        {
            DateTime result = DateTime.Now;
            try
            {
                result = DateTime.Parse(ParseString(obj));
            }
            catch { }
            return result;
        }

        #region ParseBool
        /// <summary>
        /// 将 String 类型转换为 bool 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 bool</returns>
        public static bool ParseBool(string strValue, bool defaultValue)
        {
            bool returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!bool.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 bool 型,转换失败时返回 false
        /// </summary>
        /// <param name="strValue">待转换 String</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(string strValue)
        {
            bool returnValue = false;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!bool.TryParse(strValue, out returnValue))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 bool 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 bool</returns>
        public static bool ParseBool(object objValue, bool defaultValue)
        {
            bool returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!bool.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 bool 型,转换失败时返回 false
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(object objValue)
        {
            bool returnValue = false;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!bool.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Byte 类型转换为 bool 型,转换失败时返回 false (小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="byteValue">待转换 Byte</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(byte byteValue)
        {
            bool returnValue = false;

            if (byteValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 short 类型转换为 bool 型,转换失败时返回 false (小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="shortValue">待转换 short</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(short shortValue)
        {
            bool returnValue = false;

            if (shortValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 int 类型转换为 bool 型,转换失败时返回 false ( 小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="intValue">待转换 int</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(int intValue)
        {
            bool returnValue = false;

            if (intValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 long 类型转换为 bool 型,转换失败时返回 false ( 小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="longValue">待转换 long</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(long longValue)
        {
            bool returnValue = false;

            if (longValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        #endregion


        #region ParseDecimal (Decimal : 16 字节)
        /// <summary>
        /// 将 String 类型转换为 Decimal 型,转换失败时返回默认值

        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(string strValue, decimal defaultValue)
        {
            decimal returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!decimal.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 Decimal 型,转换失败返回 decimal.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(string strValue)
        {
            decimal returnValue = decimal.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!decimal.TryParse(strValue, out returnValue))
                {
                    returnValue = decimal.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 Decimal 型,转换失败时返回默认值

        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(object objValue, decimal defaultValue)
        {
            decimal returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!decimal.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 Decimal 型,转换失败时返回 decimal.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(object objValue)
        {
            decimal returnValue = decimal.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!decimal.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = decimal.MinValue;
                }
            }

            return returnValue;
        }
        #endregion

        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="strInput">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string strInput)
        {
            string strResult = "";

            if (!string.IsNullOrEmpty(strInput))
            {
                char[] arrChar = strInput.ToCharArray();

                for (int i = 0; i < arrChar.Length; i++)
                {
                    if (arrChar[i] == 32)
                    {
                        arrChar[i] = (char)12288;
                        continue;
                    }

                    if (arrChar[i] < 127)
                    {
                        arrChar[i] = (char)(arrChar[i] + 65248);
                    }
                }

                strResult = new string(arrChar);
            }

            return strResult;
        }


        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="strInput">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string strInput)
        {
            string strResult = "";

            if (!string.IsNullOrEmpty(strInput))
            {
                char[] arrChar = strInput.ToCharArray();

                for (int i = 0; i < arrChar.Length; i++)
                {
                    if (arrChar[i] == 12288)
                    {
                        arrChar[i] = (char)32;
                        continue;
                    }

                    if (arrChar[i] > 65280 && arrChar[i] < 65375)
                    {
                        arrChar[i] = (char)(arrChar[i] - 65248);
                    }
                }

                strResult = new string(arrChar);
            }

            return strResult;
        }
        #endregion
        
    }
}
