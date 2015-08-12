using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ToolLib.util
{
    public class PassUtil
    {

        private  static  string[] arrkey =new string[]  { "qwserty", "uiopas", "dfghjk", "lzxcv", "bnmqwe", "rtyuio", "pasdfg", "hjklzx", "cvbnmq", "wertyu"};
        private static  string[] arrvalue =new string[]  { "iopasd", "fghjkl", "zxcvbn", "mqwert", "yuiopa", "sdfghj", "klzxcv", "bnmqwe", "rtyuio", "pasdfg" };

        public static string DesEncrypt(string key,string data)
        {
          int p=  key.GetHashCode();
          int index = p % 10;
          return DesDecrypt(data, arrkey[index], arrvalue[9 - index]);
        }

        public static string DesDecrypt(string key, string data)
        {
            int p = key.GetHashCode();
            int index = p % 10;
            return DesDecrypt(data, arrkey[index], arrvalue[9 - index]);
        }


        /// <summary>
        /// DESº”√‹
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DesEncrypt(string data, string KEY_64, string IV_64)
        {
            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                int i = cryptoProvider.KeySize;
                MemoryStream ms = new MemoryStream();
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);
                sw.Write(data);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch
            {
                return data;
            }

        }

        /// <summary>
        /// DESΩ‚√‹
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DesDecrypt(string data, string KEY_64, string IV_64)
        {
            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc;
                try
                {
                    byEnc = Convert.FromBase64String(data);
                }
                catch
                {
                    return data;
                }

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch
            {
                return data;
            }
        }

    }
}


