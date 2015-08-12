using System;
using System.Text;
namespace ToolLib.util
{
    /// <summary>
    /// ����������,��Ҫ��������������͵�ת��,
    /// </summary>
    public abstract class ConvertUtil
    {
        /// <summary>
        /// 1.1 ����ת��string
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
        /// 1.2 ����ת��string(Ĭ��)
        /// �ȴ���null,��ת��Ϊstring
        /// </summary>
        /// <param name="obj">Ҫ���������</param>
        /// <returns>���ش���������</returns>
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
        /// 2.1 ����ת��cookie
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
        /// 2.2 ����ת��cookie(Ĭ�ϣ�
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
        /// 3.1 ����ת��short
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
        /// 3.2 ����ת��short(Ĭ��)
        /// </summary>
        /// <param name="obj">Ҫ���������</param>
        /// <returns>���ش���������</returns>
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
        /// 4.1 ����ת��int
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
        /// 4.2 ����ת��int(Ĭ��)
        /// </summary>
        /// <param name="obj">Ҫ���������</param>
        /// <returns>���ش���������</returns>
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
        /// 5.1 ����ת��long
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
        /// 5.2 ����ת��long(Ĭ��)
        /// </summary>
        /// <param name="obj">Ҫ���������</param>
        /// <returns>���ش���������</returns>
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
        /// 6.1 ����ת��float
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
        /// 6.2 ����ת��float(Ĭ��)
        /// </summary>
        /// <param name="obj">Ҫ���������</param>
        /// <returns>���ش���������</returns>
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
        /// 7.1 ����ת��double
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
        /// 7.2 ����ת��double(Ĭ��)
        /// </summary>
        /// <param name="obj">Ҫ���������</param>
        /// <returns>���ش���������</returns>
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
        /// 8.1 ����ת��datetime
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
        /// 8.2 ����ת��datetime(Ĭ��)
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
        /// �� String ����ת��Ϊ bool ��,ת��ʧ��ʱ����Ĭ��ֵ
        /// </summary>
        /// <param name="strValue">��ת�� String</param>
        /// <param name="defaultValue">ת��ʧ��ʱ��Ĭ��ֵ</param>
        /// <returns>���� bool</returns>
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
        /// �� String ����ת��Ϊ bool ��,ת��ʧ��ʱ���� false
        /// </summary>
        /// <param name="strValue">��ת�� String</param>      
        /// <returns>���� bool</returns>
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
        /// �� Object ����ת��Ϊ bool ��,ת��ʧ��ʱ����Ĭ��ֵ
        /// </summary>
        /// <param name="objValue">��ת�� Object</param>
        /// <param name="defaultValue">ת��ʧ��ʱ��Ĭ��ֵ</param>
        /// <returns>���� bool</returns>
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
        /// �� Object ����ת��Ϊ bool ��,ת��ʧ��ʱ���� false
        /// </summary>
        /// <param name="objValue">��ת�� Object</param>      
        /// <returns>���� bool</returns>
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
        /// �� Byte ����ת��Ϊ bool ��,ת��ʧ��ʱ���� false (С�ڻ����0 -- False; ����0 -- True)
        /// </summary>
        /// <param name="byteValue">��ת�� Byte</param>      
        /// <returns>���� bool</returns>
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
        /// �� short ����ת��Ϊ bool ��,ת��ʧ��ʱ���� false (С�ڻ����0 -- False; ����0 -- True)
        /// </summary>
        /// <param name="shortValue">��ת�� short</param>      
        /// <returns>���� bool</returns>
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
        /// �� int ����ת��Ϊ bool ��,ת��ʧ��ʱ���� false ( С�ڻ����0 -- False; ����0 -- True)
        /// </summary>
        /// <param name="intValue">��ת�� int</param>      
        /// <returns>���� bool</returns>
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
        /// �� long ����ת��Ϊ bool ��,ת��ʧ��ʱ���� false ( С�ڻ����0 -- False; ����0 -- True)
        /// </summary>
        /// <param name="longValue">��ת�� long</param>      
        /// <returns>���� bool</returns>
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


        #region ParseDecimal (Decimal : 16 �ֽ�)
        /// <summary>
        /// �� String ����ת��Ϊ Decimal ��,ת��ʧ��ʱ����Ĭ��ֵ

        /// </summary>
        /// <param name="strValue">��ת�� String</param>
        /// <param name="defaultValue">ת��ʧ��ʱ��Ĭ��ֵ</param>
        /// <returns>���� Decimal</returns>
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
        /// �� String ����ת��Ϊ Decimal ��,ת��ʧ�ܷ��� decimal.MinValue
        /// </summary>
        /// <param name="strValue">��ת�� String</param>
        /// <returns>���� Decimal</returns>
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
        /// �� Object ����ת��Ϊ Decimal ��,ת��ʧ��ʱ����Ĭ��ֵ

        /// </summary>
        /// <param name="objValue">��ת�� Object</param>
        /// <param name="defaultValue">ת��ʧ��ʱ��Ĭ��ֵ</param>
        /// <returns>���� Decimal</returns>
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
        /// �� Object ����ת��Ϊ Decimal ��,ת��ʧ��ʱ���� decimal.MinValue
        /// </summary>
        /// <param name="objValue">��ת�� Object</param>      
        /// <returns>���� Decimal</returns>
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

        #region ȫ�ǰ��ת��
        /// <summary>
        /// תȫ�ǵĺ���(SBC case)
        /// </summary>
        /// <param name="strInput">�����ַ���</param>
        /// <returns>ȫ���ַ���</returns>
        ///<remarks>
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
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


        /// <summary> ת��ǵĺ���(DBC case) </summary>
        /// <param name="strInput">�����ַ���</param>
        /// <returns>����ַ���</returns>
        ///<remarks>
        ///ȫ�ǿո�Ϊ12288����ǿո�Ϊ32
        ///�����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ�ǣ������65248
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
