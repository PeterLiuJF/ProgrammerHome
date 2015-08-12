using System;
using System.Data;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;

namespace ToolLib.util
{
    public class FileUtil
    {

        private static Random rnd = new Random();
        
        /// <summary>
        /// 1.1 获取附件情况,文件包含路径
        /// </summary>
        /// <param name="file">文件路径，注意不能带扩展名</param>
       /// <param name="ext">扩展名</param>
       /// <returns></returns>
        public static string GetDownLoad(string file,string[]ext)
        {
            StringBuilder s = new StringBuilder();
            string rootPath = Normal.ParseString(ConfigurationManager.AppSettings["ServerPath"]);// HttpContext.Current.Request.ApplicationPath;
            string downloadPath = rootPath +"/include/download.aspx?fileName=" + file; 
            string fileName = HttpContext.Current.Server.MapPath(file);
            //HttpContext.Current.Response.Write(downloadPath + "<br>" + fileName);
            for (int i = 0; i < ext.Length; i++)
            {
                if (File.Exists(fileName +"."+ Normal.Trim(ext[i])))
                {
                    s.Append(" " + "<a href='" + downloadPath + "."+ext[i]+"'>" +
                             "<img src='" + rootPath + "/images/logo/"+ext[i]+".gif' width=16 height=16 border=0></a>");
                }
            }

            return s.ToString();

        }

        /// <summary>
        /// 1.2 获取附件情况,文件包含路径,扩展名默认
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetDownLoad(string file)
        {
            string[] ext = new string[] { "txt", "xls", "pdf", "doc" };
            return GetDownLoad(file, ext);
        }

        /// <summary>
        /// 1.3 获取附件情况，文件与路径分开
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetDownLoad(string path, string file,string[]ext)
        {
           return GetDownLoad(path + "/" + file,ext);
        }


        /// <summary>
        /// 1.4 获取附件情况，文件与路径分开,扩展名默认
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetDownLoad(string path, string file)
        {
            string[] ext = new string[] { "txt", "xls", "pdf", "doc" };
            return GetDownLoad(path+"/"+file, ext);
        }

        

        /// <summary>
        /// 2.1 下载文件
        /// </summary>
        /// <param name="file">文件路径，相对路径</param>
        /// <param name="b">是否全路径，默认否</param>
        public static void DownloadFile(string file,bool b)
        {
            string filePath = "";
            if (!b)
                filePath = HttpContext.Current.Server.MapPath(file);
            else
                filePath = file;
            string saveFileName = file;
            int p = filePath.LastIndexOf("\\");
            if (p >= 0) saveFileName = filePath.Substring(p+1);
            //HttpContext.Current.Response.Write(file + "<br>" + saveFileName);
            
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(saveFileName));
            HttpContext.Current.Response.ContentType = "application/unknown";

            HttpContext.Current.Response.WriteFile(filePath);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();

            HttpContext.Current.Response.End();
            
        }

        /// <summary>
        ///  2.2 下载文件，文件与路径分开
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public static void DownloadFile(string path, string file)
        {
            DownloadFile(path + "/" + file,false);
        }

       /// <summary>
        ///  2.3 下载文件,文件包含路径
       /// </summary>
       /// <param name="file"></param>
        public static void DownloadFile(string file)
        {
             DownloadFile(file, false);
        }

        #region 获取一个不重复的文件名
        public static string GetUniquelyName()
        {
            const int RANDOM_MAX_VALUE = 1000;
            string strTemp, strYear, strMonth, strDay, strHour, strMinute, strSecond, strMillisecond;
            DateTime dt = DateTime.Now;
            int rndNumber = rnd.Next(RANDOM_MAX_VALUE);
            strYear = dt.Year.ToString();
            strMonth = (dt.Month > 9) ? dt.Month.ToString() : "0" + dt.Month.ToString();
            strDay = (dt.Day > 9) ? dt.Day.ToString() : "0" + dt.Day.ToString();
            strHour = (dt.Hour > 9) ? dt.Hour.ToString() : "0" + dt.Hour.ToString();
            strMinute = (dt.Minute > 9) ? dt.Minute.ToString() : "0" + dt.Minute.ToString();
            strSecond = (dt.Second > 9) ? dt.Second.ToString() : "0" + dt.Second.ToString();
            strMillisecond = dt.Millisecond.ToString();
            strTemp = strYear + strMonth + strDay + "_" + strHour + strMinute + strSecond + "_" + strMillisecond + "_" + rndNumber.ToString();
            return strTemp;
        }
        #endregion

        /// <summary>
        /// 获取当地路径
        /// </summary>
        public static string LocalPath(string virtualPath)
        {
            string vpath = "";
            if (virtualPath.Contains("~/"))
                vpath = virtualPath;
            else
                vpath = "~/" + virtualPath;

            return HttpContext.Current.Server.MapPath(vpath);
        }

        public static void SaveFile(MemoryStream stream, string localPath, string filename)
        {

            byte[] imgData = stream.ToArray();
            stream.Dispose();
            FileStream fs = null;
            if (System.IO.File.Exists(localPath + "\\" + filename))
            {
                System.IO.File.Delete(localPath + "\\" + filename);
            }
            fs = new FileStream(localPath + "\\" + filename, FileMode.Create, FileAccess.Write);
            if (fs != null)
            {
                fs.Write(imgData, 0, imgData.Length);
                fs.Close();
            }
        }



    }
}
