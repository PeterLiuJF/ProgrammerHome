using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Configuration;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-14 
 * Content: 日志类
 ******************************************************************/

namespace ToolLib.util
{
    //日志级别枚举
    public enum LogLevel
    {
        Trace = 1,  //跟踪信息
        Debug = 2,  //调试信息
        Warn  = 3,  //警告信息
        Error = 4,  //一般错误
        Fatal = 5   //致命错误
    }

    public class LogUtil
    {
        //互斥令牌对象
        private static object obj = new object();

        //WriteLog函数委托
        private delegate void WriteLogDelegate(LogLevel enumLogLevel, string strClassName, string strMethodName, string strMsg);

        #region Trace
        /// <summary>
        /// 跟踪信息
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Trace(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Trace,"","", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// 跟踪信息
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Trace(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Trace, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region Debug
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Debug(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Debug,"","", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Debug(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Debug, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region Warn
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Warn(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Warn, "", "", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Warn(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Warn, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }  
        #endregion

        #region Error
        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Error(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Error, "", "", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Error(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Error, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region Fatal
        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Fatal(string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Fatal, "", "", strMsg, new AsyncCallback(CallBackMethod), wld);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Fatal(string strClassName, string strMethodName, string strMsg)
        {
            WriteLogDelegate wld = new WriteLogDelegate(WriteLog);
            IAsyncResult ar = wld.BeginInvoke(LogLevel.Fatal, strClassName, strMethodName, strMsg, new AsyncCallback(CallBackMethod), wld);
        }
        #endregion

        #region 日志级别检查（只输出高于配置级别的日志）
        /// <summary>
        /// 日志级别检查（只输出高于配置级别的日志）
        /// </summary>
        /// <param name="enumLogLevel">待检查的日志级别</param>
        /// <returns>符合输出返回true,否则返回false</returns>
        private static bool CheckLevel(LogLevel enumLogLevel)
        {
            //基准日志级别字符串（config文件中配置）
            string strSTlevel = ConfigurationManager.AppSettings["LogLevel"].Trim();

            //基准日志级别
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

        #region 异步写日志
        /// <summary>
        /// 异步写日志
        /// </summary>
        /// <param name="enumLogLevel">日志级别</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        private static void WriteLog(LogLevel enumLogLevel, string strClassName, string strMethodName, string strMsg)
        {
            if (string.IsNullOrEmpty(strMsg))
            {
                return;
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(enumLogLevel))
            {
                return;
            }

            //日志路径
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

            //日志路径格式 yyyy\mm
            strLogPath += DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString();

            //写入流
            StreamWriter sw = null;

            try
            {
                //如果路径不存在，重新创建
                if (!Directory.Exists(strLogPath))
                {
                    Directory.CreateDirectory(strLogPath);
                }

                //日志文件
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

                    //写日志
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

        #region 异步写日志回调函数
        /// <summary>
        /// 异步写日志回调函数
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
