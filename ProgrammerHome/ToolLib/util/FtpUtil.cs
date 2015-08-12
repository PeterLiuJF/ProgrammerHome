using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;
using System.Configuration;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-16
 * Content: FTP操作类
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// FTP操作类
    /// </summary>
    public class FtpUtil
    {
        #region 相关参数

        public static string strFtpUri = "";
        public static string strFtpPath = "";
        public static string strFtpUserID = "";
        public static string strFtpPassword = "";

        public static Uri serverUri = null;
        public static int intFtpReadWriteTimeout = 0;
        public static bool boolFtpUseBinary = true;
        public static bool boolFtpUsePassive = false;
        public static bool boolFtpKeepAlive = true;
        public static bool boolEnableSsl = false;
        #endregion

        #region 静态构造函数
        static FtpUtil()
        {
            GetPara();
        }
        #endregion

        #region 取得参数
        /// <summary>
        /// 取得参数
        /// </summary>
        /// <returns></returns>
        public static bool GetPara()
        {
            bool boolResult = true;

            string strFtpReadWriteTimeout = "";
            string strFtpUseBinary = "";
            string strFtpUsePassive = "";
            string strFtpKeepAlive = "";
            string strEnableSsl = "";

            strFtpUri = ConfigurationManager.AppSettings["FtpUri"];
            strFtpPath = ConfigurationManager.AppSettings["FtpPath"];
            strFtpUserID = ConfigurationManager.AppSettings["FtpUserID"];
            strFtpPassword = ConfigurationManager.AppSettings["FtpPassword"];
            strFtpReadWriteTimeout = ConfigurationManager.AppSettings["FtpReadWriteTimeout"];
            strFtpUseBinary = ConfigurationManager.AppSettings["FtpUseBinary"];
            strFtpUsePassive = ConfigurationManager.AppSettings["FtpUsePassive"];
            strFtpKeepAlive = ConfigurationManager.AppSettings["FtpKeepAlive"];
            strEnableSsl = ConfigurationManager.AppSettings["FtpEnableSsl"];

            if (string.IsNullOrEmpty(strFtpUri))
            {
                boolResult = false;
            }
            else
            {
                //没有输入路径
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    serverUri = new Uri(strFtpUri);

                    if (serverUri.Scheme != Uri.UriSchemeFtp)
                    {
                        boolResult = false;
                    }
                }
                else //带路径
                {
                    if (!strFtpUri.EndsWith(@"/"))
                    {
                        strFtpUri += @"/";
                    }

                    if (strFtpPath.StartsWith(@"/"))
                    {
                        strFtpPath = strFtpPath.Substring(1);
                    }

                    serverUri = new Uri(strFtpUri + strFtpPath);

                    if (serverUri.Scheme != Uri.UriSchemeFtp)
                    {
                        boolResult = false;
                    }
                }

            }

            if (string.IsNullOrEmpty(strFtpUserID))
            {
                boolResult = false;
            }

            if (string.IsNullOrEmpty(strFtpPassword))
            {
                boolResult = false;
            }

            if (string.IsNullOrEmpty(strFtpReadWriteTimeout))
            {
                //默认超时时间为5分钟
                strFtpReadWriteTimeout = "300000";
                intFtpReadWriteTimeout = 300000;
            }
            else
            {
                intFtpReadWriteTimeout = ConvertUtil.ParseInt(strFtpReadWriteTimeout, 300000);
            }

            if (string.IsNullOrEmpty(strFtpUseBinary))
            {
                //默认使用二进制传输
                strFtpUseBinary = "true";
                boolFtpUseBinary = true;
            }
            else
            {
                boolFtpUseBinary = ConvertUtil.ParseBool(strFtpUseBinary, true);
            }

            if (string.IsNullOrEmpty(strFtpUsePassive))
            {
                //默认使用被动模式
                strFtpUsePassive = "false";
                boolFtpUsePassive = false;
            }
            else
            {
                boolFtpUsePassive = ConvertUtil.ParseBool(strFtpUsePassive, false);
            }

            if (string.IsNullOrEmpty(strFtpKeepAlive))
            {
                //默认在请求完成之后保持到 FTP 服务器的控制连接
                strFtpKeepAlive = "true";
                boolFtpKeepAlive = true;
            }
            else
            {
                boolFtpKeepAlive = ConvertUtil.ParseBool(strFtpKeepAlive, true);
            }

            if (string.IsNullOrEmpty(strEnableSsl))
            {
                //默认不启用SSL安全套接字连接
                strEnableSsl = "false";
                boolEnableSsl = false;
            }
            else
            {
                boolEnableSsl = ConvertUtil.ParseBool(strEnableSsl, false);
            }

            return boolResult;
        }
        #endregion

        #region 取得文件列表(简单模式)
        /// <summary>
        /// 取得文件列表(简单模式)
        /// WebRequestMethods.Ftp.ListDirectory 模式
        /// </summary>
        /// <param name="strFtpUri">远程ftp地址("ftp://220.113.15.77/")</param>
        /// <param name="strFtpPath">远程ftp路径("test")</param>
        /// <param name="strFtpUserID">远程ftp用户名</param>
        /// <param name="strFtpPassword">远程ftp密码</param>
        /// <param name="intFtpReadWriteTimeout">读取或写入超时之前的毫秒数。默认值为 300,000 毫秒（5 分钟）</param>
        /// <param name="boolFtpUseBinary">true，指示服务器要传输的是二进制数据；false，指示数据为文本。默认值为true。</param>
        /// <param name="boolFtpUsePassive">false，被动模式；true，主动模式(主动模式可能被防火墙拦截)。默认值为false。</param>
        /// <param name="boolFtpKeepAlive">该值指定在请求完成之后是否关闭到 FTP 服务器的控制连接。</param>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns>字符数组</returns>       
        public static string[] ListDirectory(string strFtpUri, string strFtpPath, string strFtpUserID, string strFtpPassword, int intFtpReadWriteTimeout, bool boolFtpUseBinary, bool boolFtpUsePassive, bool boolFtpKeepAlive, bool boolEnableSsl)
        {
            Uri uri = null;
            string[] arrFileList = null;
            string strLine = null;
            StringBuilder sb = new StringBuilder();

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;
            Stream responseStream = null;
            StreamReader readStream = null;

            #region 输入参数检查

            //strFtpUri
            if (string.IsNullOrEmpty(strFtpUri))
            {
                LogUtil.Warn("FtpUtil", "ListDirectory()", "参数 strFtpUri 为空");
                return arrFileList;
            }
            else
            {
                //没有输入路径
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    uri = new Uri(strFtpUri);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "ListDirectory()", "参数 Uri 不是有效的Ftp路径模式 Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
                else //带路径
                {
                    if (!strFtpUri.EndsWith(@"/"))
                    {
                        strFtpUri += @"/";
                    }

                    if (strFtpPath.StartsWith(@"/"))
                    {
                        strFtpPath = strFtpPath.Substring(1);
                    }

                    uri = new Uri(strFtpUri + strFtpPath);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "ListDirectory()", "参数 Uri 不是有效的Ftp路径模式 Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
            }

            //strFtpUserID
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                LogUtil.Warn("FtpUtil", "ListDirectory()", "参数 strFtpUserID 为空");
                return arrFileList;
            }

            //strFtpPassword
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                LogUtil.Warn("FtpUtil", "ListDirectory()", "参数 strFtpPassword 为空");
                return arrFileList;
            }

            //intFtpReadWriteTimeout
            if (intFtpReadWriteTimeout == int.MaxValue || intFtpReadWriteTimeout == int.MinValue)
            {
                //默认超时时间为5分钟               
                intFtpReadWriteTimeout = 300000;
            }
            #endregion

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                //-------------------SSL 加密处理---------------------------------
                if (boolEnableSsl)
                {
                    requestFTP.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                        { return true; };

                }

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();
                responseStream = responseFTP.GetResponseStream();
                readStream = new StreamReader(responseStream, System.Text.Encoding.Default);

                strLine = readStream.ReadLine();

                while (strLine != null)
                {
                    sb.Append(strLine);
                    //以"|*|"分隔
                    sb.Append("|*|");
                    strLine = readStream.ReadLine();
                }

                sb.Remove(sb.ToString().LastIndexOf("|*|"), 3);

                arrFileList = sb.ToString().Split(new string[] { "|*|" }, StringSplitOptions.RemoveEmptyEntries);

            }
            catch (Exception ex)
            {
                LogUtil.Error("FtpUtil", "ListDirectory()", ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream.Dispose();
                }

                if (readStream != null)
                {
                    readStream.Close();
                    readStream.Dispose();
                }

            }

            return arrFileList;
        }

        /// <summary>
        /// 取得文件列表(简单模式)
        /// WebRequestMethods.Ftp.ListDirectory 模式
        /// </summary>
        /// <returns>字符数组</returns>
        public static string[] ListDirectory()
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// 取得文件列表(简单模式)
        /// WebRequestMethods.Ftp.ListDirectory 模式
        /// </summary>
        /// <param name="strFtpPath">远程ftp路径</param>
        /// <returns>字符数组</returns>
        public static string[] ListDirectory(string strFtpPath)
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// 取得文件列表(简单模式)
        /// WebRequestMethods.Ftp.ListDirectory 模式
        /// </summary>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns>字符数组</returns>
        public static string[] ListDirectory(bool boolEnableSsl)
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// 取得文件列表(简单模式)
        /// WebRequestMethods.Ftp.ListDirectory 模式
        /// </summary>
        /// <param name="strFtpPath">远程ftp路径</param>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns></returns>
        public static string[] ListDirectory(string strFtpPath, bool boolEnableSsl)
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }
        #endregion

        #region 取得文件列表(详细模式)
        /// <summary>
        /// 取得文件列表(详细模式)
        /// WebRequestMethods.Ftp.ListDirectoryDetails 模式
        /// </summary>
        /// <param name="strFtpUri">远程ftp地址("ftp://220.113.15.77/")</param>
        /// <param name="strFtpPath">远程ftp路径("test")</param>
        /// <param name="strFtpUserID">远程ftp用户名</param>
        /// <param name="strFtpPassword">远程ftp密码</param>
        /// <param name="intFtpReadWriteTimeout">读取或写入超时之前的毫秒数。默认值为 300,000 毫秒（5 分钟）</param>
        /// <param name="boolFtpUseBinary">true，指示服务器要传输的是二进制数据；false，指示数据为文本。默认值为true。</param>
        /// <param name="boolFtpUsePassive">false，被动模式；true，主动模式(主动模式可能被防火墙拦截)。默认值为false。</param>
        /// <param name="boolFtpKeepAlive">该值指定在请求完成之后是否关闭到 FTP 服务器的控制连接。</param>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns>字符数组</returns>
        public static string[] ListDirectoryDetails(string strFtpUri, string strFtpPath, string strFtpUserID, string strFtpPassword, int intFtpReadWriteTimeout, bool boolFtpUseBinary, bool boolFtpUsePassive, bool boolFtpKeepAlive, bool boolEnableSsl)
        {
            Uri uri = null;
            string[] arrFileList = null;
            string strLine = null;
            StringBuilder sb = new StringBuilder();

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;
            Stream responseStream = null;
            StreamReader readStream = null;

            #region 输入参数检查

            //strFtpUri
            if (string.IsNullOrEmpty(strFtpUri))
            {
                LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "参数 strFtpUri 为空");
                return arrFileList;
            }
            else
            {
                //没有输入路径
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    uri = new Uri(strFtpUri);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "参数 Uri 不是有效的Ftp路径模式 Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
                else //带路径
                {
                    if (!strFtpUri.EndsWith(@"/"))
                    {
                        strFtpUri += @"/";
                    }

                    if (strFtpPath.StartsWith(@"/"))
                    {
                        strFtpPath = strFtpPath.Substring(1);
                    }

                    uri = new Uri(strFtpUri + strFtpPath);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "参数 Uri 不是有效的Ftp路径模式 Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
            }

            //strFtpUserID
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "参数 strFtpUserID 为空");
                return arrFileList;
            }

            //strFtpPassword
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "参数 strFtpPassword 为空");
                return arrFileList;
            }

            //intFtpReadWriteTimeout
            if (intFtpReadWriteTimeout == int.MaxValue || intFtpReadWriteTimeout == int.MinValue)
            {
                //默认超时时间为5分钟               
                intFtpReadWriteTimeout = 300000;
            }
            #endregion

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                //-------------------SSL 加密处理---------------------------------
                if (boolEnableSsl)
                {
                    requestFTP.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                        { return true; };

                }

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();
                responseStream = responseFTP.GetResponseStream();
                readStream = new StreamReader(responseStream, System.Text.Encoding.Default);

                strLine = readStream.ReadLine();

                while (strLine != null)
                {
                    sb.Append(strLine);
                    //以"|*|"分隔
                    sb.Append("|*|");
                    strLine = readStream.ReadLine();
                }

                sb.Remove(sb.ToString().LastIndexOf("|*|"), 3);

                arrFileList = sb.ToString().Split(new string[] { "|*|" }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream.Dispose();
                }

                if (readStream != null)
                {
                    readStream.Close();
                    readStream.Dispose();
                }
            }

            return arrFileList;
        }

        /// <summary>
        /// 取得文件列表(详细模式)
        /// WebRequestMethods.Ftp.ListDirectoryDetails 模式
        /// </summary>
        /// <returns>字符数组</returns>
        public static string[] ListDirectoryDetails()
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// 取得文件列表(详细模式)
        /// WebRequestMethods.Ftp.ListDirectoryDetails 模式
        /// </summary>
        /// <param name="strFtpPath">远程ftp路径</param>
        /// <returns>字符数组</returns>
        public static string[] ListDirectoryDetails(string strFtpPath)
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// 取得文件列表(详细模式)
        /// WebRequestMethods.Ftp.ListDirectoryDetails 模式
        /// </summary>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns>字符数组</returns>
        public static string[] ListDirectoryDetails(bool boolEnableSsl)
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// 取得文件列表(详细模式)
        /// WebRequestMethods.Ftp.ListDirectoryDetails 模式
        /// </summary>
        /// <param name="strFtpPath">远程ftp路径</param>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns></returns>
        public static string[] ListDirectoryDetails(string strFtpPath, bool boolEnableSsl)
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="strLocalPath">待上传文件的本地路径(@"D:\abc.txt")</param>
        /// <param name="strFtpUri">远程ftp地址("ftp://220.113.15.77/")</param>
        /// <param name="strFtpPath">远程ftp路径("test")</param>
        /// <param name="strFtpUserID">远程ftp用户名</param>
        /// <param name="strFtpPassword">远程ftp密码</param>
        /// <param name="intFtpReadWriteTimeout">读取或写入超时之前的毫秒数。默认值为 300,000 毫秒（5 分钟）</param>
        /// <param name="boolFtpUseBinary">true，指示服务器要传输的是二进制数据；false，指示数据为文本。默认值为true。</param>
        /// <param name="boolFtpUsePassive">false，被动模式；true，主动模式(主动模式可能被防火墙拦截)。默认值为false。</param>
        /// <param name="boolFtpKeepAlive">该值指定在请求完成之后是否关闭到 FTP 服务器的控制连接。</param>
        /// <param name="boolEnableSsl">是否启用SSL安全套接字连接</param>
        /// <returns>true 代表上传成功;false 代表上传失败</returns>
        public static bool UploadFile(string strLocalPath, string strFtpUri, string strFtpPath, string strFtpUserID, string strFtpPassword, int intFtpReadWriteTimeout, bool boolFtpUseBinary, bool boolFtpUsePassive, bool boolFtpKeepAlive, bool boolEnableSsl)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;
            Stream requestStream = null;
            FileStream fileStream = null;
            FileInfo fileInfo = null;

            bool boolResult = false;

            #region 输入参数检查

            //strLocalPath
            if (string.IsNullOrEmpty(strLocalPath))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "参数 strLocalPath 为空");
                return boolResult;
            }
            else
            {
                if (!File.Exists(strLocalPath))
                {
                    LogUtil.Warn("FtpUtil", "UploadFile()", "参数 strLocalPath:" + strLocalPath + " 文件路径不存在");
                    return boolResult;
                }
                else
                {
                    fileInfo = new FileInfo(strLocalPath);
                }
            }

            //strFtpUri
            if (string.IsNullOrEmpty(strFtpUri))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "参数 strFtpUri 为空");
                return boolResult;
            }
            else
            {
                //没有输入路径
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    //ftp路径参数最后一位"/"处理
                    if (!strFtpUri.EndsWith(@"/"))
                    {
                        strFtpUri += @"/";
                    }

                    uri = new Uri(strFtpUri + fileInfo.Name);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "UploadFile()", "参数 Uri 不是有效的Ftp路径模式 Uri:" + uri.ToString());
                        return boolResult;
                    }
                }
                else //带路径
                {
                    if (!strFtpUri.EndsWith(@"/"))
                    {
                        strFtpUri += @"/";
                    }

                    if (strFtpPath.StartsWith(@"/"))
                    {
                        strFtpPath = strFtpPath.Substring(1);
                    }

                    if (!strFtpPath.EndsWith(@"/"))
                    {
                        strFtpPath += @"/";
                    }

                    uri = new Uri(strFtpUri + strFtpPath + fileInfo.Name);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "UploadFile()", "参数 Uri 不是有效的Ftp路径模式 Uri:" + uri.ToString());
                        return boolResult;
                    }
                }
            }

            //strFtpUserID
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "参数 strFtpUserID 为空");
                return boolResult;
            }

            //strFtpPassword
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "参数 strFtpPassword 为空");
                return boolResult;
            }

            //intFtpReadWriteTimeout
            if (intFtpReadWriteTimeout == int.MaxValue || intFtpReadWriteTimeout == int.MinValue)
            {
                //默认超时时间为5分钟               
                intFtpReadWriteTimeout = 300000;
            }
            #endregion

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.UploadFile;

                //-------------------SSL 加密处理---------------------------------
                if (boolEnableSsl)
                {
                    requestFTP.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                        { return true; };

                }

                requestStream = requestFTP.GetRequestStream();

                fileStream = File.Open(strLocalPath, FileMode.Open);

                byte[] buffer = new byte[2048];

                int bytesRead;

                while (true)
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    requestStream.Write(buffer, 0, bytesRead);

                }

                requestStream.Close();

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();

                boolResult = true;

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

                if (requestStream != null)
                {
                    requestStream.Close();
                    requestStream.Dispose();
                }

                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }

            }

            return boolResult;
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="strLocalDirectory">待下载文件的本地路径(@"D:\")</param>
        /// <param name="strUri">待下载文件的远程ftp路径("ftp://220.113.15.77/test/abc.txt")</param>
        /// <returns>true 代表下载成功;false 代表下载失败</returns>
        public static bool DownloadFile(string strLocalDirectory, string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;
            Stream responseStream = null;
            FileStream fileStream = null;
            string strFileName = "";

            bool boolResult = false;

            //输入参数检查 strLocalDirectory
            if (string.IsNullOrEmpty(strLocalDirectory))
            {
                LogUtil.Warn("FtpUtil->DownloadFile(): 参数 strLocalDirectory 为空");
                return boolResult;
            }
            else
            {
                //路径参数最后一位"\"处理
                if (!strLocalDirectory.EndsWith(@"\"))
                {
                    strLocalDirectory += @"\";
                }

                //路径不存在，则自动创建
                if (!Directory.Exists(strLocalDirectory))
                {
                    Directory.CreateDirectory(strLocalDirectory);
                }
            }

            //输入参数检查 strUri
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->DownloadFile(): 参数 strUri 为空");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                //URI FTP模式判定
                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->DownloadFile(): 参数 strUri 不是有效的Ftp路径模式 " + strUri);
                    return boolResult;
                }

                //待下载文件的文件名
                strFileName = strUri.Substring(strUri.LastIndexOf("/") + 1);
            }

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                //-------------------SSL 加密处理---------------------------------
                requestFTP.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    { return true; };

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();
                responseStream = responseFTP.GetResponseStream();

                //如存在相同文件，则先删除
                if (File.Exists(strLocalDirectory + strFileName))
                {
                    File.Delete(strLocalDirectory + strFileName);
                }

                fileStream = File.Create(strLocalDirectory + strFileName);

                byte[] buffer = new byte[2048];

                int bytesRead;

                while (true)
                {
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    fileStream.Write(buffer, 0, bytesRead);

                }

                //下载成功
                boolResult = true;

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream.Dispose();
                }

                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            return boolResult;
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件        
        /// </summary>
        /// <param name="strUri">远程ftp路径("ftp://220.113.15.77/test/abc.txt")</param>
        /// <returns>true 代表删除成功;false 代表删除失败</returns>
        public static bool DeleteFile(string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //输入参数检查
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->DeleteFile(): 参数 strUri 为空");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->DeleteFile(): 参数 strUri 不是有效的Ftp路径模式 " + strUri);
                    return boolResult;
                }
            }

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();

                boolResult = true;

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

            }

            return boolResult;
        }
        #endregion

        #region 重命名/移动 文件
        /// <summary>
        /// 重命名/移动 文件    
        /// 移动文件模式 strReName("../test111/abc111.txt")
        /// </summary>
        /// <param name="strUri">远程ftp路径("ftp://220.113.15.77/test/abc.txt")</param>
        /// <param name="strReName">重命名的文件("abc111.txt")</param>
        /// <returns>true 代表重命名成功;false 代表重命名失败</returns>
        public static bool RenameFile(string strUri, string strReName)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //输入参数检查
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->RenameFile(): 参数 strUri 为空");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->RenameFile(): 参数 strUri 不是有效的Ftp路径模式 " + strUri);
                    return boolResult;
                }
            }

            //输入参数检查
            if (string.IsNullOrEmpty(strReName))
            {
                LogUtil.Warn("FtpUtil->RenameFile(): 参数 strReName 为空");
                return boolResult;
            }

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.Rename;

                requestFTP.RenameTo = strReName;

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();

                boolResult = true;

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

            }

            return boolResult;
        }
        #endregion

        #region 新建目录
        /// <summary>
        /// 新建目录        
        /// </summary>
        /// <param name="strUri">远程ftp路径("ftp://220.113.15.77/test/")</param>
        /// <returns>true 代表新建成功;false 代表新建失败</returns>
        public static bool MakeDirectory(string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //输入参数检查
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->MakeDirectory(): 参数 strUri 为空");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->MakeDirectory(): 参数 strUri 不是有效的Ftp路径模式 " + strUri);
                    return boolResult;
                }
            }

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();

                boolResult = true;

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

            }

            return boolResult;
        }
        #endregion

        #region 删除目录
        /// <summary>
        /// 删除目录        
        /// </summary>
        /// <param name="strUri">远程ftp路径("ftp://220.113.15.77/test/")</param>
        /// <returns>true 代表删除成功;false 代表删除失败</returns>
        public static bool RemoveDirectory(string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //输入参数检查
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->RemoveDirectory(): 参数 strUri 为空");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->RemoveDirectory(): 参数 strUri 不是有效的Ftp路径模式 " + strUri);
                    return boolResult;
                }
            }

            try
            {
                requestFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                requestFTP.Credentials = new NetworkCredential(strFtpUserID, strFtpPassword);
                requestFTP.ReadWriteTimeout = intFtpReadWriteTimeout;
                requestFTP.UseBinary = boolFtpUseBinary;
                requestFTP.UsePassive = boolFtpUsePassive;
                requestFTP.KeepAlive = boolFtpKeepAlive;
                requestFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();

                boolResult = true;

            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
            finally
            {
                if (responseFTP != null)
                {
                    responseFTP.Close();
                }

            }

            return boolResult;
        }
        #endregion        
    }
}
