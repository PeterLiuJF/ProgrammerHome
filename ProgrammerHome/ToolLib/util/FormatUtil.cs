using System;
using System.Text;
namespace ToolLib.util
{
    /// <summary>
    /// 公共函数类,主要处理各种数据的格式化
    /// </summary>
    public abstract class FormatUtil:ConvertUtil
    {

        /// <summary>
        /// 1.1 格式化int
        /// 格式串
        ///  (C) Currency: . . . . . . . . ($123.00)
        ///  (D) Decimal:. . . . . . . . . -123
        ///  (E) Scientific: . . . . . . . -1.234500E+002
        ///  (F) Fixed point:. . . . . . . -123.45
        ///  (G) General:. . . . . . . . . -123
        ///  (N) Number: . . . . . . . . . -123.00
        ///  (P) Percent:. . . . . . . . . -12,345.00 %
        ///  (R) Round-trip: . . . . . . . -123.45
        ///  (X) Hexadecimal:. . . . . . . FFFFFF85
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">要格式的数据</param>
        /// <returns>返回格式后的数据</returns>
        public static string FormatInt(string formatstr,int obj)
        {
            string result = "";
            try
            {
                result = obj.ToString(formatstr);
            }
            catch
            {
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>
        /// 1.2 小数格式化int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatInt(int obj, int n)
        {
            return FormatInt("N" + n,obj);
        }

        /// <summary>
        /// 1.3 百分比格式化int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentInt(int obj, int n)
        {
            return FormatInt( "P" + n,obj);
        }
        
        /// <summary>
        /// 1.4 货币格式化int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyInt(int obj, int n)
        {
            return FormatInt("C" + n,obj);
        }

        /// <summary>
        /// 2.1 格式化short
        /// 格式串
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">要格式的数据</param>
        /// <returns>返回格式后的数据</returns>
        public static string FormatShort(string formatstr, short obj)
        {
            string result = "";
            try
            {
                result = obj.ToString(formatstr);
            }
            catch
            {
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>
        /// 2.2 小数格式化short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatShort(short obj, int n)
        {
            return FormatShort("N" + n, obj);
        }

        /// <summary>
        /// 2.3 百分比格式化short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentShort(short obj, int n)
        {
            return FormatShort("P" + n, obj);
        }

        /// <summary>
        /// 2.4 货币格式化short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyShort(short obj, int n)
        {
            return FormatShort("C" + n, obj);
        }

        /// <summary>
        /// 3.1 格式化long
        /// 格式串
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">要格式的数据</param>
        /// <returns>返回格式后的数据</returns>
        public static string FormatLong(string formatstr, long obj)
        {
            string result = "";
            try
            {
                result = obj.ToString(formatstr);
            }
            catch
            {
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>
        /// 3.2 小数格式化long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatLong(long obj, int n)
        {
            return FormatLong("N" + n, obj);
        }

        /// <summary>
        /// 3.3 百分比格式化long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentLong(long obj, int n)
        {
            return FormatLong("P" + n, obj);
        }

        /// <summary>
        /// 3.4 货币格式化long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyLong(long obj, int n)
        {
            return FormatLong("C" + n, obj);
        }

        /// <summary>
        /// 4.1 格式化float
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">要格式的数据</param>
        /// <returns>返回格式后的数据</returns>
        public static string FormatFloat(string formatstr, float obj)
        {
            string result = "";
            try
            {
                result = obj.ToString(formatstr);
            }
            catch
            {
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>
        /// 4.2 小数格式化float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatFloat(float obj, int n)
        {
            return FormatFloat("N" + n, obj);
        }

        /// <summary>
        /// 4.3 百分比格式化float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentFloat(float obj, int n)
        {
            return FormatFloat("P" + n, obj);
        }

        /// <summary>
        /// 4.4 货币格式化float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyFloat(float obj, int n)
        {
            return FormatFloat("C" + n, obj);
        }


        /// <summary>
        /// 5.1 格式化double
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">要格式的数据</param>
        /// <returns>返回格式后的数据</returns>
        public static string FormatDouble(string formatstr, double obj)
        {
            string result = "";
            try
            {
                result = obj.ToString(formatstr);
            }
            catch
            {
                result = obj.ToString();
            }
            return result;
        }

        /// <summary>
        /// 5.2 小数格式化double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatDouble(double obj, int n)
        {
            return FormatDouble("N" + n, obj);
        }

        /// <summary>
        /// 5.3 百分比格式化double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentDouble(double obj, int n)
        {
            return FormatDouble("P" + n, obj);
        }

        /// <summary>
        /// 5.4 货币格式化double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyDouble(double obj, int n)
        {
            return FormatDouble("C" + n, obj);
        }

       

        /// <summary>
        /// 6.1.1 格式化object
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">需要处理的字符</param>
        /// <param name="defaultstr">默认字符</param>
        /// <returns>返回处理后的字符</returns>
        public static string Format(string formatstr, object obj,string defaultstr)
        {
            string result = "";
            if (obj == null||obj == DBNull.Value)
                result = defaultstr;
            else
            {
                try
                {
                    result = Normal.ParseDouble(obj).ToString(formatstr);
                }
                catch
                {
                    result = obj.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// 6.1.2 格式化object(默认）
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(string formatstr, object obj)
        {
            return Format(formatstr, obj, "--");
        }

        /// <summary>
        /// 6.2.1 小数格式化object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string Format(object obj, int n, string defaultstr)
        {
            return Format("N" + n, obj, defaultstr);
        }

        /// <summary>
        /// 6.2.2 小数格式化object(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Format(object obj, int n)
        {
            return Format("N" + n, obj,"--");
        }

        /// <summary>
        /// 6.3.1 百分比格式化object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string Percent(object obj, int n, string defaultstr)
        {
            return Format("P" + n, obj, defaultstr);
        }

        /// <summary>
        /// 6.3.2 百分比格式化object(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Percent(object obj, int n)
        {
            return Format("P" + n, obj, "--");
        }

        /// <summary>
        /// 6.4.1 货币格式化object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string Currency(object obj, int n, string defaultstr)
        {
            return Format("C" + n, obj, defaultstr);
        }

        /// <summary>
        /// 6.4.2 货币格式化object(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Currency(object obj, int n)
        {
            return Format("C" + n, obj, "--");
        }

        
        

        /// <summary>
        /// 7.1.1 格式化string
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">需要处理的字符</param>
        /// <param name="defaultstr">默认字符</param>
        /// <returns>返回处理后的字符</returns>
        public static string Format(string formatstr, string obj,string defaultstr)
        {
            string result = "";
            if (obj == null)
                result = defaultstr;
            else
            {
                try
                {
                    result = Normal.ParseDouble(obj).ToString(formatstr);
                }
                catch
                {
                    result = obj.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// 7.1.2 格式化string(默认）
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(string formatstr, string obj)
        {
            return Format(formatstr, obj, "--");
        }
       
        /// <summary>
        /// 7.2.1 小数格式化string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string Format(string obj, int n, string defaultstr)
        {
            return Format("N" + n, obj, defaultstr);
        }

        /// <summary>
        /// 7.2.2 小数格式化string(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Format(string obj, int n)
        {
            return Format("N" + n, obj,"--");
        }

        /// <summary>
        /// 7.3.1 百分比格式化string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string Percent(string obj, int n, string defaultstr)
        {
            return Format("P" + n, obj, defaultstr);
        }

        /// <summary>
        /// 7.3.2 百分比格式化string(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Percent(string obj, int n)
        {
            return Format("P" + n, obj,"--");
        }

        /// <summary>
        /// 7.4.1 货币格式化string 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string Currency(string obj, int n, string defaultstr)
        {
            return Format("C" + n, obj, defaultstr);
        }

        /// <summary>
        /// 7.4.2 货币格式化string(默认）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Currency(string obj, int n)
        {
            return Format("C" + n, obj);
        }

        

        /// <summary>
        /// 8.1.1 格式化日期 datetime
        /// 格式串
        ///  (yyyy-MM-dd hh:mm:ss) . . .   2004-06-26 20:11:04
        ///  (yyyy-MM-dd) . . . . . . . .  2004-06-26
        ///  (hh:mm:ss) . . . . . . . . .  20:11:04
        ///  (d) Short date: . . . . . . . 6/26/2004
        ///  (D) Long date:. . . . . . . . Saturday, June 26, 2004
        ///  (t) Short time: . . . . . . . 8:11 PM
        ///  (T) Long time:. . . . . . . . 8:11:04 PM
        ///  (f) Full date/short time: . . Saturday, June 26, 2004 8:11 PM
        ///  (R) RFC1123:. . . . . . . . . Sat, 26 Jun 2004 20:11:04 GMT
        ///  (s) Sortable: . . . . . . . . 2004-06-26T20:11:04
        ///  (u) Universal sortable: . . . 2004-06-26 20:11:04Z (invariant)
        ///  (U) Universal sortable: . . . Sunday, June 27, 2004 3:11:04 AM
        /// 举例:
        ///    Normal.Format("D",DateTime.Now);
        ///    Normal.Format("yyyy-MM-dd hh:mm:ss",DateTime.Now);
        ///    Normal.Format("yyyy年MM月dd日",DateTime.Now);
        ///    Normal.Format("hh:mm:ss",DateTime.Now);
        ///    Normal.Format("yyyy-MM-dd",DateTime.Now);
        ///    Normal.Format("hh:mm",DateTime.Now);
        /// 相当于   
        ///    DateTime.Now.ToLongDateString();
        ///    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");  
        ///    DateTime.Now.ToLongDateString();
        ///    DateTime.Now.ToLongTimeString();
        ///    DateTime.Now.ToShortDateString();
        ///    DateTime.Now.ToShortTimeString();
        /// </summary>
        /// <param name="formatstr">格式</param>
        /// <param name="obj">要格式的数据</param>
        /// <param name="defaultstr"></param>
        /// <returns>返回格式后的数据</returns>
        public static string Format(string formatstr, DateTime obj,string defaultstr)
        {
            string result = "";
            try
            {
                result = obj.ToString(formatstr);
            }
            catch
            {
                result = obj.ToString();
            }
            if (result.IndexOf("1900") >= 0) result = defaultstr;
            return result;
        }

        /// <summary>
        /// 8.1.2 格式化日期 datetime (默认）
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(string formatstr, DateTime obj)
        {
            return Format(formatstr, obj, "--");
        }

        /// <summary>
        /// 8.2.1 同8.1.1
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, DateTime obj,string defaultstr)
        {
            return Format(formatstr, obj,defaultstr);
        }

        /// <summary>
        /// 8.2.2 同8.1.2
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, DateTime obj)
        {
            return Format(formatstr, obj,"--");
        }

        /// <summary>
        /// 8.2.3 格式化日期 datetime(初始)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime obj)
        {
            return FormatDate("yyyy-MM-dd", obj, "--");
        }

        /// <summary>
        /// 8.3.1 格式化日期 string
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, string obj, string defaultstr)
        {
            if (obj==null||obj.IndexOf("1900") == 0)
                return defaultstr;
            try
            {
                return Format(formatstr, DateTime.Parse(obj),defaultstr);
            }
            catch
            {
                return defaultstr;
            }
        }

        /// <summary>
        /// 8.3.2 格式化日期 string(默认)
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, string obj)
        {
            return FormatDate(formatstr, obj, "--");
        }
        /// <summary>
        /// 8.3.3 格式化日期 string(初始)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string obj)
        {
            return FormatDate("yyyy-MM-dd", obj, "--");
        }

        /// <summary>
        /// 8.4.1 格式化日期 object
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <param name="defaultstr"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, object obj, string defaultstr)
        {
            if (obj==null||obj==DBNull.Value)
                return defaultstr;
            try
            {
                return Format(formatstr, DateTime.Parse(ParseString(obj)), defaultstr);
            }
            catch
            {
                return defaultstr;
            }
        }

        /// <summary>
        /// 8.4.2 格式化日期 object(默认)
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, object obj)
        {
            return FormatDate(formatstr, obj,"--");
        }

        /// <summary>
        /// 8.4.3 格式化日期 object(初始)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(object obj)
        {
            return FormatDate("yyyy-MM-dd", obj, "--");
        }
    }
}
