using System;
using System.Configuration;
using System.IO;
using System.Web;
using ToolLib.util;
namespace ToolLib.log
{
	/// <summary>
	/// LOG日志跟踪。
	/// </summary>
	public class Log
	{
		/// <summary>
		/// 级别
		/// </summary>
		private static int level = 0;

		/// <summary>
		/// 输出文件目录
		/// </summary>
		private static String outFile = "";

		/// <summary>
		/// 是否初始化
		/// </summary>
		private static Boolean isInit=false; 

		/// <summary>
		/// 初始化
		/// </summary>
		private static void Init()
		{
			isInit=true;
			level =Normal.ParseInt(ConfigurationManager.AppSettings["LogLevel"]);
            outFile = ConfigurationManager.AppSettings["LogPath"];
		}


		/// <summary>
		/// 严重错误输出
		/// </summary>
		/// <param name="str"></param>
		public static void Fatal(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.FATAL)
				List("[FATAL]" + str);
		}

		/// <summary>
		/// 错误输出
		/// </summary>
		/// <param name="str"></param>
		public static void Error(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.ERROR)
				List("[ERROR]" + str);
		}

		/// <summary>
		/// 警告输出
		/// </summary>
		/// <param name="str"></param>
		public static void Warn(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.WARN)
				List("[WARN]" + str);
		}

		/// <summary>
		/// 信息输出
		/// </summary>
		/// <param name="str"></param>
		public static void Info(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.INFO)
				List("[INFO]" + str);
		}


		/// <summary>
		/// 调试输出
		/// </summary>
		/// <param name="str"></param>
		public static void Debug(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.DEBUG)
				List("[DEBUG]" + str);
		}

		/// <summary>
		/// 跟踪输出
		/// </summary>
		/// <param name="str"></param>
		public static void Trace(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.TRACE)
				List("[TRACE]" + str);
		}

		/// <summary>
		/// 日志输出到文件
		/// </summary>
		/// <param name="str"></param>
		public static void List(String str) 
		{			
			try 
			{
				string dt=FormatUtil.Format("yyyy-MM-dd hh:mm:ss",System.DateTime.Now);
                string dt1 = FormatUtil.Format("yyyy-MM-dd", System.DateTime.Now);
                string logFile = HttpContext.Current.Request.PhysicalApplicationPath + "\\" + outFile + "\\" + dt1 + ".txt";////System.Web.HttpContext.Current.Server.MapPath(outFile + "/" + dt1 + ".txt");
				StreamWriter sw=null;
				if(File.Exists(logFile))
					sw=File.AppendText(logFile);
				else
					sw=File.CreateText(logFile);
				sw.WriteLine("\n["+ dt  + "]"+ str + "\n");
				sw.Close();
			}
			catch{}
		}

		/// <summary>
		/// 日志输出到页面
		/// </summary>
		/// <param name="res"></param>
		/// <param name="str"></param>
		public static void List(System.Web.HttpResponse res, String str) 
		{
			try 
			{
                string dt = FormatUtil.Format("yyyy-MM-dd hh:mm:ss", System.DateTime.Now);
				res.Write("[" + dt + "]"+ str + "<br>");
			} 
			catch
			{
			}
		}
	}
}
