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
 * Content: FTP������
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// FTP������
    /// </summary>
    public class FtpUtil
    {
        #region ��ز���

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

        #region ��̬���캯��
        static FtpUtil()
        {
            GetPara();
        }
        #endregion

        #region ȡ�ò���
        /// <summary>
        /// ȡ�ò���
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
                //û������·��
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    serverUri = new Uri(strFtpUri);

                    if (serverUri.Scheme != Uri.UriSchemeFtp)
                    {
                        boolResult = false;
                    }
                }
                else //��·��
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
                //Ĭ�ϳ�ʱʱ��Ϊ5����
                strFtpReadWriteTimeout = "300000";
                intFtpReadWriteTimeout = 300000;
            }
            else
            {
                intFtpReadWriteTimeout = ConvertUtil.ParseInt(strFtpReadWriteTimeout, 300000);
            }

            if (string.IsNullOrEmpty(strFtpUseBinary))
            {
                //Ĭ��ʹ�ö����ƴ���
                strFtpUseBinary = "true";
                boolFtpUseBinary = true;
            }
            else
            {
                boolFtpUseBinary = ConvertUtil.ParseBool(strFtpUseBinary, true);
            }

            if (string.IsNullOrEmpty(strFtpUsePassive))
            {
                //Ĭ��ʹ�ñ���ģʽ
                strFtpUsePassive = "false";
                boolFtpUsePassive = false;
            }
            else
            {
                boolFtpUsePassive = ConvertUtil.ParseBool(strFtpUsePassive, false);
            }

            if (string.IsNullOrEmpty(strFtpKeepAlive))
            {
                //Ĭ�����������֮�󱣳ֵ� FTP �������Ŀ�������
                strFtpKeepAlive = "true";
                boolFtpKeepAlive = true;
            }
            else
            {
                boolFtpKeepAlive = ConvertUtil.ParseBool(strFtpKeepAlive, true);
            }

            if (string.IsNullOrEmpty(strEnableSsl))
            {
                //Ĭ�ϲ�����SSL��ȫ�׽�������
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

        #region ȡ���ļ��б�(��ģʽ)
        /// <summary>
        /// ȡ���ļ��б�(��ģʽ)
        /// WebRequestMethods.Ftp.ListDirectory ģʽ
        /// </summary>
        /// <param name="strFtpUri">Զ��ftp��ַ("ftp://220.113.15.77/")</param>
        /// <param name="strFtpPath">Զ��ftp·��("test")</param>
        /// <param name="strFtpUserID">Զ��ftp�û���</param>
        /// <param name="strFtpPassword">Զ��ftp����</param>
        /// <param name="intFtpReadWriteTimeout">��ȡ��д�볬ʱ֮ǰ�ĺ�������Ĭ��ֵΪ 300,000 ���루5 ���ӣ�</param>
        /// <param name="boolFtpUseBinary">true��ָʾ������Ҫ������Ƕ��������ݣ�false��ָʾ����Ϊ�ı���Ĭ��ֵΪtrue��</param>
        /// <param name="boolFtpUsePassive">false������ģʽ��true������ģʽ(����ģʽ���ܱ�����ǽ����)��Ĭ��ֵΪfalse��</param>
        /// <param name="boolFtpKeepAlive">��ֵָ�����������֮���Ƿ�رյ� FTP �������Ŀ������ӡ�</param>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns>�ַ�����</returns>       
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

            #region ����������

            //strFtpUri
            if (string.IsNullOrEmpty(strFtpUri))
            {
                LogUtil.Warn("FtpUtil", "ListDirectory()", "���� strFtpUri Ϊ��");
                return arrFileList;
            }
            else
            {
                //û������·��
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    uri = new Uri(strFtpUri);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "ListDirectory()", "���� Uri ������Ч��Ftp·��ģʽ Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
                else //��·��
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
                        LogUtil.Warn("FtpUtil", "ListDirectory()", "���� Uri ������Ч��Ftp·��ģʽ Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
            }

            //strFtpUserID
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                LogUtil.Warn("FtpUtil", "ListDirectory()", "���� strFtpUserID Ϊ��");
                return arrFileList;
            }

            //strFtpPassword
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                LogUtil.Warn("FtpUtil", "ListDirectory()", "���� strFtpPassword Ϊ��");
                return arrFileList;
            }

            //intFtpReadWriteTimeout
            if (intFtpReadWriteTimeout == int.MaxValue || intFtpReadWriteTimeout == int.MinValue)
            {
                //Ĭ�ϳ�ʱʱ��Ϊ5����               
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

                //-------------------SSL ���ܴ���---------------------------------
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
                    //��"|*|"�ָ�
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
        /// ȡ���ļ��б�(��ģʽ)
        /// WebRequestMethods.Ftp.ListDirectory ģʽ
        /// </summary>
        /// <returns>�ַ�����</returns>
        public static string[] ListDirectory()
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// ȡ���ļ��б�(��ģʽ)
        /// WebRequestMethods.Ftp.ListDirectory ģʽ
        /// </summary>
        /// <param name="strFtpPath">Զ��ftp·��</param>
        /// <returns>�ַ�����</returns>
        public static string[] ListDirectory(string strFtpPath)
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// ȡ���ļ��б�(��ģʽ)
        /// WebRequestMethods.Ftp.ListDirectory ģʽ
        /// </summary>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns>�ַ�����</returns>
        public static string[] ListDirectory(bool boolEnableSsl)
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// ȡ���ļ��б�(��ģʽ)
        /// WebRequestMethods.Ftp.ListDirectory ģʽ
        /// </summary>
        /// <param name="strFtpPath">Զ��ftp·��</param>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns></returns>
        public static string[] ListDirectory(string strFtpPath, bool boolEnableSsl)
        {
            return ListDirectory(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }
        #endregion

        #region ȡ���ļ��б�(��ϸģʽ)
        /// <summary>
        /// ȡ���ļ��б�(��ϸģʽ)
        /// WebRequestMethods.Ftp.ListDirectoryDetails ģʽ
        /// </summary>
        /// <param name="strFtpUri">Զ��ftp��ַ("ftp://220.113.15.77/")</param>
        /// <param name="strFtpPath">Զ��ftp·��("test")</param>
        /// <param name="strFtpUserID">Զ��ftp�û���</param>
        /// <param name="strFtpPassword">Զ��ftp����</param>
        /// <param name="intFtpReadWriteTimeout">��ȡ��д�볬ʱ֮ǰ�ĺ�������Ĭ��ֵΪ 300,000 ���루5 ���ӣ�</param>
        /// <param name="boolFtpUseBinary">true��ָʾ������Ҫ������Ƕ��������ݣ�false��ָʾ����Ϊ�ı���Ĭ��ֵΪtrue��</param>
        /// <param name="boolFtpUsePassive">false������ģʽ��true������ģʽ(����ģʽ���ܱ�����ǽ����)��Ĭ��ֵΪfalse��</param>
        /// <param name="boolFtpKeepAlive">��ֵָ�����������֮���Ƿ�رյ� FTP �������Ŀ������ӡ�</param>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns>�ַ�����</returns>
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

            #region ����������

            //strFtpUri
            if (string.IsNullOrEmpty(strFtpUri))
            {
                LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "���� strFtpUri Ϊ��");
                return arrFileList;
            }
            else
            {
                //û������·��
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    uri = new Uri(strFtpUri);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "���� Uri ������Ч��Ftp·��ģʽ Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
                else //��·��
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
                        LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "���� Uri ������Ч��Ftp·��ģʽ Uri:" + uri.ToString());
                        return arrFileList;
                    }
                }
            }

            //strFtpUserID
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "���� strFtpUserID Ϊ��");
                return arrFileList;
            }

            //strFtpPassword
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                LogUtil.Warn("FtpUtil", "ListDirectoryDetails()", "���� strFtpPassword Ϊ��");
                return arrFileList;
            }

            //intFtpReadWriteTimeout
            if (intFtpReadWriteTimeout == int.MaxValue || intFtpReadWriteTimeout == int.MinValue)
            {
                //Ĭ�ϳ�ʱʱ��Ϊ5����               
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

                //-------------------SSL ���ܴ���---------------------------------
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
                    //��"|*|"�ָ�
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
        /// ȡ���ļ��б�(��ϸģʽ)
        /// WebRequestMethods.Ftp.ListDirectoryDetails ģʽ
        /// </summary>
        /// <returns>�ַ�����</returns>
        public static string[] ListDirectoryDetails()
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// ȡ���ļ��б�(��ϸģʽ)
        /// WebRequestMethods.Ftp.ListDirectoryDetails ģʽ
        /// </summary>
        /// <param name="strFtpPath">Զ��ftp·��</param>
        /// <returns>�ַ�����</returns>
        public static string[] ListDirectoryDetails(string strFtpPath)
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// ȡ���ļ��б�(��ϸģʽ)
        /// WebRequestMethods.Ftp.ListDirectoryDetails ģʽ
        /// </summary>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns>�ַ�����</returns>
        public static string[] ListDirectoryDetails(bool boolEnableSsl)
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }

        /// <summary>
        /// ȡ���ļ��б�(��ϸģʽ)
        /// WebRequestMethods.Ftp.ListDirectoryDetails ģʽ
        /// </summary>
        /// <param name="strFtpPath">Զ��ftp·��</param>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns></returns>
        public static string[] ListDirectoryDetails(string strFtpPath, bool boolEnableSsl)
        {
            return ListDirectoryDetails(strFtpUri, strFtpPath, strFtpUserID, strFtpPassword, intFtpReadWriteTimeout, boolFtpUseBinary, boolFtpUsePassive, boolFtpKeepAlive, boolEnableSsl);
        }
        #endregion

        #region �ϴ��ļ�
        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="strLocalPath">���ϴ��ļ��ı���·��(@"D:\abc.txt")</param>
        /// <param name="strFtpUri">Զ��ftp��ַ("ftp://220.113.15.77/")</param>
        /// <param name="strFtpPath">Զ��ftp·��("test")</param>
        /// <param name="strFtpUserID">Զ��ftp�û���</param>
        /// <param name="strFtpPassword">Զ��ftp����</param>
        /// <param name="intFtpReadWriteTimeout">��ȡ��д�볬ʱ֮ǰ�ĺ�������Ĭ��ֵΪ 300,000 ���루5 ���ӣ�</param>
        /// <param name="boolFtpUseBinary">true��ָʾ������Ҫ������Ƕ��������ݣ�false��ָʾ����Ϊ�ı���Ĭ��ֵΪtrue��</param>
        /// <param name="boolFtpUsePassive">false������ģʽ��true������ģʽ(����ģʽ���ܱ�����ǽ����)��Ĭ��ֵΪfalse��</param>
        /// <param name="boolFtpKeepAlive">��ֵָ�����������֮���Ƿ�رյ� FTP �������Ŀ������ӡ�</param>
        /// <param name="boolEnableSsl">�Ƿ�����SSL��ȫ�׽�������</param>
        /// <returns>true �����ϴ��ɹ�;false �����ϴ�ʧ��</returns>
        public static bool UploadFile(string strLocalPath, string strFtpUri, string strFtpPath, string strFtpUserID, string strFtpPassword, int intFtpReadWriteTimeout, bool boolFtpUseBinary, bool boolFtpUsePassive, bool boolFtpKeepAlive, bool boolEnableSsl)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;
            Stream requestStream = null;
            FileStream fileStream = null;
            FileInfo fileInfo = null;

            bool boolResult = false;

            #region ����������

            //strLocalPath
            if (string.IsNullOrEmpty(strLocalPath))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "���� strLocalPath Ϊ��");
                return boolResult;
            }
            else
            {
                if (!File.Exists(strLocalPath))
                {
                    LogUtil.Warn("FtpUtil", "UploadFile()", "���� strLocalPath:" + strLocalPath + " �ļ�·��������");
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
                LogUtil.Warn("FtpUtil", "UploadFile()", "���� strFtpUri Ϊ��");
                return boolResult;
            }
            else
            {
                //û������·��
                if (string.IsNullOrEmpty(strFtpPath))
                {
                    //ftp·���������һλ"/"����
                    if (!strFtpUri.EndsWith(@"/"))
                    {
                        strFtpUri += @"/";
                    }

                    uri = new Uri(strFtpUri + fileInfo.Name);

                    if (uri.Scheme != Uri.UriSchemeFtp)
                    {
                        LogUtil.Warn("FtpUtil", "UploadFile()", "���� Uri ������Ч��Ftp·��ģʽ Uri:" + uri.ToString());
                        return boolResult;
                    }
                }
                else //��·��
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
                        LogUtil.Warn("FtpUtil", "UploadFile()", "���� Uri ������Ч��Ftp·��ģʽ Uri:" + uri.ToString());
                        return boolResult;
                    }
                }
            }

            //strFtpUserID
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "���� strFtpUserID Ϊ��");
                return boolResult;
            }

            //strFtpPassword
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                LogUtil.Warn("FtpUtil", "UploadFile()", "���� strFtpPassword Ϊ��");
                return boolResult;
            }

            //intFtpReadWriteTimeout
            if (intFtpReadWriteTimeout == int.MaxValue || intFtpReadWriteTimeout == int.MinValue)
            {
                //Ĭ�ϳ�ʱʱ��Ϊ5����               
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

                //-------------------SSL ���ܴ���---------------------------------
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

        #region �����ļ�
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="strLocalDirectory">�������ļ��ı���·��(@"D:\")</param>
        /// <param name="strUri">�������ļ���Զ��ftp·��("ftp://220.113.15.77/test/abc.txt")</param>
        /// <returns>true �������سɹ�;false ��������ʧ��</returns>
        public static bool DownloadFile(string strLocalDirectory, string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;
            Stream responseStream = null;
            FileStream fileStream = null;
            string strFileName = "";

            bool boolResult = false;

            //���������� strLocalDirectory
            if (string.IsNullOrEmpty(strLocalDirectory))
            {
                LogUtil.Warn("FtpUtil->DownloadFile(): ���� strLocalDirectory Ϊ��");
                return boolResult;
            }
            else
            {
                //·���������һλ"\"����
                if (!strLocalDirectory.EndsWith(@"\"))
                {
                    strLocalDirectory += @"\";
                }

                //·�������ڣ����Զ�����
                if (!Directory.Exists(strLocalDirectory))
                {
                    Directory.CreateDirectory(strLocalDirectory);
                }
            }

            //���������� strUri
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->DownloadFile(): ���� strUri Ϊ��");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                //URI FTPģʽ�ж�
                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->DownloadFile(): ���� strUri ������Ч��Ftp·��ģʽ " + strUri);
                    return boolResult;
                }

                //�������ļ����ļ���
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

                //-------------------SSL ���ܴ���---------------------------------
                requestFTP.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    { return true; };

                responseFTP = (FtpWebResponse)requestFTP.GetResponse();
                responseStream = responseFTP.GetResponseStream();

                //�������ͬ�ļ�������ɾ��
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

                //���سɹ�
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

        #region ɾ���ļ�
        /// <summary>
        /// ɾ���ļ�        
        /// </summary>
        /// <param name="strUri">Զ��ftp·��("ftp://220.113.15.77/test/abc.txt")</param>
        /// <returns>true ����ɾ���ɹ�;false ����ɾ��ʧ��</returns>
        public static bool DeleteFile(string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //����������
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->DeleteFile(): ���� strUri Ϊ��");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->DeleteFile(): ���� strUri ������Ч��Ftp·��ģʽ " + strUri);
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

        #region ������/�ƶ� �ļ�
        /// <summary>
        /// ������/�ƶ� �ļ�    
        /// �ƶ��ļ�ģʽ strReName("../test111/abc111.txt")
        /// </summary>
        /// <param name="strUri">Զ��ftp·��("ftp://220.113.15.77/test/abc.txt")</param>
        /// <param name="strReName">���������ļ�("abc111.txt")</param>
        /// <returns>true �����������ɹ�;false ����������ʧ��</returns>
        public static bool RenameFile(string strUri, string strReName)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //����������
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->RenameFile(): ���� strUri Ϊ��");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->RenameFile(): ���� strUri ������Ч��Ftp·��ģʽ " + strUri);
                    return boolResult;
                }
            }

            //����������
            if (string.IsNullOrEmpty(strReName))
            {
                LogUtil.Warn("FtpUtil->RenameFile(): ���� strReName Ϊ��");
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

        #region �½�Ŀ¼
        /// <summary>
        /// �½�Ŀ¼        
        /// </summary>
        /// <param name="strUri">Զ��ftp·��("ftp://220.113.15.77/test/")</param>
        /// <returns>true �����½��ɹ�;false �����½�ʧ��</returns>
        public static bool MakeDirectory(string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //����������
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->MakeDirectory(): ���� strUri Ϊ��");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->MakeDirectory(): ���� strUri ������Ч��Ftp·��ģʽ " + strUri);
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

        #region ɾ��Ŀ¼
        /// <summary>
        /// ɾ��Ŀ¼        
        /// </summary>
        /// <param name="strUri">Զ��ftp·��("ftp://220.113.15.77/test/")</param>
        /// <returns>true ����ɾ���ɹ�;false ����ɾ��ʧ��</returns>
        public static bool RemoveDirectory(string strUri)
        {
            Uri uri = null;

            FtpWebRequest requestFTP = null;
            FtpWebResponse responseFTP = null;

            bool boolResult = false;

            //����������
            if (string.IsNullOrEmpty(strUri))
            {
                LogUtil.Warn("FtpUtil->RemoveDirectory(): ���� strUri Ϊ��");
                return boolResult;
            }
            else
            {
                uri = new Uri(strUri);

                if (uri.Scheme != Uri.UriSchemeFtp)
                {
                    LogUtil.Warn("FtpUtil->RemoveDirectory(): ���� strUri ������Ч��Ftp·��ģʽ " + strUri);
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
