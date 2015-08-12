using System;
using System.Text;
namespace ToolLib.util
{
    /// <summary>
    /// ����������,��Ҫ����������ݵĸ�ʽ��
    /// </summary>
    public abstract class FormatUtil:ConvertUtil
    {

        /// <summary>
        /// 1.1 ��ʽ��int
        /// ��ʽ��
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
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">Ҫ��ʽ������</param>
        /// <returns>���ظ�ʽ�������</returns>
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
        /// 1.2 С����ʽ��int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatInt(int obj, int n)
        {
            return FormatInt("N" + n,obj);
        }

        /// <summary>
        /// 1.3 �ٷֱȸ�ʽ��int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentInt(int obj, int n)
        {
            return FormatInt( "P" + n,obj);
        }
        
        /// <summary>
        /// 1.4 ���Ҹ�ʽ��int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyInt(int obj, int n)
        {
            return FormatInt("C" + n,obj);
        }

        /// <summary>
        /// 2.1 ��ʽ��short
        /// ��ʽ��
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">Ҫ��ʽ������</param>
        /// <returns>���ظ�ʽ�������</returns>
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
        /// 2.2 С����ʽ��short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatShort(short obj, int n)
        {
            return FormatShort("N" + n, obj);
        }

        /// <summary>
        /// 2.3 �ٷֱȸ�ʽ��short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentShort(short obj, int n)
        {
            return FormatShort("P" + n, obj);
        }

        /// <summary>
        /// 2.4 ���Ҹ�ʽ��short
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyShort(short obj, int n)
        {
            return FormatShort("C" + n, obj);
        }

        /// <summary>
        /// 3.1 ��ʽ��long
        /// ��ʽ��
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">Ҫ��ʽ������</param>
        /// <returns>���ظ�ʽ�������</returns>
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
        /// 3.2 С����ʽ��long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatLong(long obj, int n)
        {
            return FormatLong("N" + n, obj);
        }

        /// <summary>
        /// 3.3 �ٷֱȸ�ʽ��long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentLong(long obj, int n)
        {
            return FormatLong("P" + n, obj);
        }

        /// <summary>
        /// 3.4 ���Ҹ�ʽ��long
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyLong(long obj, int n)
        {
            return FormatLong("C" + n, obj);
        }

        /// <summary>
        /// 4.1 ��ʽ��float
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">Ҫ��ʽ������</param>
        /// <returns>���ظ�ʽ�������</returns>
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
        /// 4.2 С����ʽ��float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatFloat(float obj, int n)
        {
            return FormatFloat("N" + n, obj);
        }

        /// <summary>
        /// 4.3 �ٷֱȸ�ʽ��float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentFloat(float obj, int n)
        {
            return FormatFloat("P" + n, obj);
        }

        /// <summary>
        /// 4.4 ���Ҹ�ʽ��float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyFloat(float obj, int n)
        {
            return FormatFloat("C" + n, obj);
        }


        /// <summary>
        /// 5.1 ��ʽ��double
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">Ҫ��ʽ������</param>
        /// <returns>���ظ�ʽ�������</returns>
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
        /// 5.2 С����ʽ��double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string FormatDouble(double obj, int n)
        {
            return FormatDouble("N" + n, obj);
        }

        /// <summary>
        /// 5.3 �ٷֱȸ�ʽ��double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string PercentDouble(double obj, int n)
        {
            return FormatDouble("P" + n, obj);
        }

        /// <summary>
        /// 5.4 ���Ҹ�ʽ��double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string CurrencyDouble(double obj, int n)
        {
            return FormatDouble("C" + n, obj);
        }

       

        /// <summary>
        /// 6.1.1 ��ʽ��object
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">��Ҫ������ַ�</param>
        /// <param name="defaultstr">Ĭ���ַ�</param>
        /// <returns>���ش������ַ�</returns>
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
        /// 6.1.2 ��ʽ��object(Ĭ�ϣ�
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(string formatstr, object obj)
        {
            return Format(formatstr, obj, "--");
        }

        /// <summary>
        /// 6.2.1 С����ʽ��object
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
        /// 6.2.2 С����ʽ��object(Ĭ�ϣ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Format(object obj, int n)
        {
            return Format("N" + n, obj,"--");
        }

        /// <summary>
        /// 6.3.1 �ٷֱȸ�ʽ��object
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
        /// 6.3.2 �ٷֱȸ�ʽ��object(Ĭ�ϣ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Percent(object obj, int n)
        {
            return Format("P" + n, obj, "--");
        }

        /// <summary>
        /// 6.4.1 ���Ҹ�ʽ��object
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
        /// 6.4.2 ���Ҹ�ʽ��object(Ĭ�ϣ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Currency(object obj, int n)
        {
            return Format("C" + n, obj, "--");
        }

        
        

        /// <summary>
        /// 7.1.1 ��ʽ��string
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">��Ҫ������ַ�</param>
        /// <param name="defaultstr">Ĭ���ַ�</param>
        /// <returns>���ش������ַ�</returns>
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
        /// 7.1.2 ��ʽ��string(Ĭ�ϣ�
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(string formatstr, string obj)
        {
            return Format(formatstr, obj, "--");
        }
       
        /// <summary>
        /// 7.2.1 С����ʽ��string
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
        /// 7.2.2 С����ʽ��string(Ĭ�ϣ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Format(string obj, int n)
        {
            return Format("N" + n, obj,"--");
        }

        /// <summary>
        /// 7.3.1 �ٷֱȸ�ʽ��string
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
        /// 7.3.2 �ٷֱȸ�ʽ��string(Ĭ�ϣ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Percent(string obj, int n)
        {
            return Format("P" + n, obj,"--");
        }

        /// <summary>
        /// 7.4.1 ���Ҹ�ʽ��string 
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
        /// 7.4.2 ���Ҹ�ʽ��string(Ĭ�ϣ�
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Currency(string obj, int n)
        {
            return Format("C" + n, obj);
        }

        

        /// <summary>
        /// 8.1.1 ��ʽ������ datetime
        /// ��ʽ��
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
        /// ����:
        ///    Normal.Format("D",DateTime.Now);
        ///    Normal.Format("yyyy-MM-dd hh:mm:ss",DateTime.Now);
        ///    Normal.Format("yyyy��MM��dd��",DateTime.Now);
        ///    Normal.Format("hh:mm:ss",DateTime.Now);
        ///    Normal.Format("yyyy-MM-dd",DateTime.Now);
        ///    Normal.Format("hh:mm",DateTime.Now);
        /// �൱��   
        ///    DateTime.Now.ToLongDateString();
        ///    DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");  
        ///    DateTime.Now.ToLongDateString();
        ///    DateTime.Now.ToLongTimeString();
        ///    DateTime.Now.ToShortDateString();
        ///    DateTime.Now.ToShortTimeString();
        /// </summary>
        /// <param name="formatstr">��ʽ</param>
        /// <param name="obj">Ҫ��ʽ������</param>
        /// <param name="defaultstr"></param>
        /// <returns>���ظ�ʽ�������</returns>
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
        /// 8.1.2 ��ʽ������ datetime (Ĭ�ϣ�
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(string formatstr, DateTime obj)
        {
            return Format(formatstr, obj, "--");
        }

        /// <summary>
        /// 8.2.1 ͬ8.1.1
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
        /// 8.2.2 ͬ8.1.2
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, DateTime obj)
        {
            return Format(formatstr, obj,"--");
        }

        /// <summary>
        /// 8.2.3 ��ʽ������ datetime(��ʼ)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime obj)
        {
            return FormatDate("yyyy-MM-dd", obj, "--");
        }

        /// <summary>
        /// 8.3.1 ��ʽ������ string
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
        /// 8.3.2 ��ʽ������ string(Ĭ��)
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, string obj)
        {
            return FormatDate(formatstr, obj, "--");
        }
        /// <summary>
        /// 8.3.3 ��ʽ������ string(��ʼ)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string obj)
        {
            return FormatDate("yyyy-MM-dd", obj, "--");
        }

        /// <summary>
        /// 8.4.1 ��ʽ������ object
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
        /// 8.4.2 ��ʽ������ object(Ĭ��)
        /// </summary>
        /// <param name="formatstr"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(string formatstr, object obj)
        {
            return FormatDate(formatstr, obj,"--");
        }

        /// <summary>
        /// 8.4.3 ��ʽ������ object(��ʼ)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FormatDate(object obj)
        {
            return FormatDate("yyyy-MM-dd", obj, "--");
        }
    }
}
