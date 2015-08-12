using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Configuration;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-14 
 * Content: ��־��
 ******************************************************************/

namespace ToolLib.util
{
    //��־����ö��
    public enum LogLevel
    {
        Trace = 1,  //������Ϣ
        Debug = 2,  //������Ϣ
        Warn  = 3,  //������Ϣ
        Error = 4,  //һ�����
        Fatal = 5   //��������
    }

    public class LogUtil
    {
        //�������ƶ���
        private static object obj = new object();

        //WriteLog����ί��
        private delegate void WriteLogDelegate(LogLevel enumLogLevel, string strClassName, string strMethodName, string strMsg);

        #region Trace
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Trace(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Trace,"","", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="strClassName">����</param>
        /// <param name="strMethodName">������</param>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Trace(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Trace, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region Debug
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Debug(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Debug,"","", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="strClassName">����</param>
        /// <param name="strMethodName">������</param>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Debug(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Debug, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region Warn
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Warn(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Warn, "", "", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="strClassName">����</param>
        /// <param name="strMethodName">������</param>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Warn(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Warn, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }  
        #endregion

        #region Error
        /// <summary>
        /// һ�����
        /// </summary>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Error(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Error, "", "", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// һ�����
        /// </summary>
        /// <param name="strClassName">����</param>
        /// <param name="strMethodName">������</param>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Error(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Error, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region Fatal
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Fatal(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Fatal, "", "", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="strClassName">����</param>
        /// <param name="strMethodName">������</param>
        /// <param name="strMsg">��־��Ϣ</param>
        public static void Fatal(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Fatal, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region ��־�����飨ֻ����������ü������־��
        /// <summary>
        /// ��־�����飨ֻ����������ü������־��
        /// </summary>
        /// <param name="enumLogLevel">��������־����</param>
        /// <returns>�����������true,���򷵻�false</returns>
        private static bool CheckLevel(LogLevel enumLogLevel)
        {
            //��׼��־�����ַ�����config�ļ������ã�
            string strSTlevel = ConfigurationManager.AppSettings["LogLevel"].Trim();

            //��׼��־����
            LogLevel enumSTLogLevel = LogLevel.Trace;

            switch (strSTlevel.ToLower())
            {
                case "trace":
                    enumSTLogLevel = LogLevel.Trace;
                    break;
                case "debug":
                    enumSTLogLevel = LogLevel.Debug;
                    break;
                case "warn":
                    enumSTLogLevel = LogLevel.Warn;
                    break;
                case "error":
                    enumSTLogLevel = LogLevel.Error;
                    break;
                case "fatal":
                    enumSTLogLevel = LogLevel.Fatal;
                    break;
                default:
                    enumSTLogLevel = LogLevel.Error;
                    break;
            }

            if (enumLogLevel >= enumSTLogLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region �첽д��־
        /// <summary>
        /// �첽д��־
        /// </summary>
        /// <param name="enumLogLevel">��־����</param>
        /// <param name="strClassName">����</param>
        /// <param name="strMethodName">������</param>
        /// <param name="strMsg">��־��Ϣ</param>
        private static void WriteLog(LogLevel enumLogLevel, string strClassName, string strMethodName, string strMsg)
        {
            if (string.IsNullOrEmpty(strMsg))
            {
                return;
            }

            //��־�����飨ֻ����������ü������־��
            if (!CheckLevel(enumLogLevel))
            {
                return;
            }

            //��־·��
            string strLogPath = ConfigurationManager.AppSettings["LogPath"].Trim();

            if (string.IsNullOrEmpty(strLogPath) || strLogPath.ToLower() == "default")
            {
                strLogPath = Environment.CurrentDirectory;

                if (!strLogPath.EndsWith(@"\"))
                {
                    strLogPath += @"\";
                }

                strLogPath += "Logs";
            }          

            if (!strLogPath.EndsWith(@"\"))
            {
                strLogPath += @"\";
            }

            //��־·����ʽ yyyy\mm
            strLogPath += DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString();

            //д����
            StreamWriter sw = null;

            try
            {
                //���·�������ڣ����´���
                if (!Directory.Exists(strLogPath))
                {
                    Directory.CreateDirectory(strLogPath);
                }

                //��־�ļ�
                string strLogFile = strLogPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                lock (obj)
                {
                    if (File.Exists(strLogFile))
                    {
                        sw = File.AppendText(strLogFile);
                    }
                    else
                    {
                        sw = File.CreateText(strLogFile);
                    }

                    //д��־
                    sw.WriteLine("\r\n**********************************************************************************************");
                    sw.WriteLine("Log Time  : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                    //sw.WriteLine("Log Level : " + Enum.GetName(typeof(LogLevel), enumLogLevel) + "\r\n");
                    sw.WriteLine("Class Name : " + strClassName + "\r\n");
                    sw.WriteLine("Method Name : " + strMethodName + "\r\n");
                    sw.WriteLine("Log Msg   :\r\n");
                    sw.WriteLine(strMsg);
                   
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        #endregion

        #region �첽д��־�ص�����
        /// <summary>
        /// �첽д��־�ص�����
        /// </summary>
        /// <param name="ar"></param>
        private static void CallBackMethod(IAsyncResult ar)
        {
            WriteLogDelegate wld = (WriteLogDelegate)ar.AsyncState;
            wld.EndInvoke(ar);
        }
        #endregion
    }
}
