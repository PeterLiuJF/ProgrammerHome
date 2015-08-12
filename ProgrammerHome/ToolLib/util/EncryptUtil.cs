using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

/******************************************************************
 * Author: hehl 
 * Date: 2011-06-26 
 * Content: 加密类
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// 加密类
    /// </summary>
    public class EncryptUtil
    {
        #region MD5 加密
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="strInput">待加密的数据</param>
        /// <returns>加密后的数据</returns>
        public static string MD5Encrypt(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }

            //获取要加密的字段，并转化为Byte[]数组
            byte[] data = UnicodeEncoding.Default.GetBytes(strInput.ToCharArray());

            //建立加密服务
            MD5 md5 = new MD5CryptoServiceProvider();

            //加密Byte[]数组
            byte[] result = md5.ComputeHash(data);

            //将加密后的数组转化为字符串
            string sResult = BitConverter.ToString(result);

            return sResult;

        }
        #endregion

        #region DES 加密
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="strInput">待加密的数据</param>        
        /// <param name="KEY_64">KEY_64(8个字符64位)</param>
        /// <param name="IV_64">IV_64(8个字符64位)</param>
        /// <returns>加密后的数据</returns>
        public static string DesEncrypt(string strInput, string KEY_64, string IV_64)
        {
            DESCryptoServiceProvider cryptoProvider = null;
            MemoryStream ms = null;
            CryptoStream cst = null;
            StreamWriter sw = null;
            string strDesEncrypt = "";

            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }

            if (string.IsNullOrEmpty(KEY_64) || string.IsNullOrEmpty(IV_64))
            {
                return strInput;
            }

            if (KEY_64.Length != 8 || IV_64.Length != 8)
            {
                return strInput;
            }

            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                cryptoProvider = new DESCryptoServiceProvider();
                int i = cryptoProvider.KeySize;
                ms = new MemoryStream();
                cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                sw = new StreamWriter(cst);
                sw.Write(strInput);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();

                strDesEncrypt = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

                return strDesEncrypt;
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

                if (cst != null)
                {
                    cst.Dispose();
                }

                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }

                if (cryptoProvider != null)
                {
                    cryptoProvider = null;
                }
            }

        }


        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="strInput">待解密的数据</param>
        /// <param name="KEY_64">KEY_64(8个字符64位)</param>
        /// <param name="IV_64">IV_64(8个字符64位)</param>
        /// <returns>解密后的数据</returns>
        public static string DesDecrypt(string strInput, string KEY_64, string IV_64)
        {
            DESCryptoServiceProvider cryptoProvider = null;
            MemoryStream ms = null;
            CryptoStream cst = null;
            StreamReader sr = null;
            string strDesEncrypt = "";

            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }

            if (string.IsNullOrEmpty(KEY_64) || string.IsNullOrEmpty(IV_64))
            {
                return strInput;
            }

            if (KEY_64.Length != 8 || IV_64.Length != 8)
            {
                return strInput;
            }

            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
                byte[] byEnc;

                try
                {
                    byEnc = Convert.FromBase64String(strInput);
                }
                catch
                {
                    return "";
                }

                cryptoProvider = new DESCryptoServiceProvider();
                ms = new MemoryStream(byEnc);
                cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                sr = new StreamReader(cst);

                strDesEncrypt = sr.ReadToEnd();

                return strDesEncrypt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }

                if (cst != null)
                {
                    cst.Dispose();
                }

                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }

                if (cryptoProvider != null)
                {
                    cryptoProvider = null;
                }
            }
        }
        #endregion

        #region DES 加密(默认密钥)
        /// <summary>
        /// DES 加密(默认密钥)
        /// </summary>
        /// <param name="strInput">待加密的数据</param>
        /// <returns></returns>
        public static string DesEncrypt_Default(string strInput)
        {
            return DesEncrypt(strInput, "bhinfoLib", "bhinfoLib");
        }

        /// <summary>
        /// DES 解密(默认密钥)
        /// </summary>
        /// <param name="strInput">待解密的数据</param>
        /// <returns></returns>
        public static string DesDecrypt_Default(string strInput)
        {
            return DesDecrypt(strInput, "bhinfoLib", "bhinfoLib");
        }
        #endregion
    }
}
