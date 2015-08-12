using System;
using System.Configuration;
using System.IO;
using System.Web;
using ToolLib.util;
namespace ToolLib.log
{
	/// <summary>
	/// LOG��־���١�
	/// </summary>
	public class Log
	{
		/// <summary>
		/// ����
		/// </summary>
		private static int level = 0;

		/// <summary>
		/// ����ļ�Ŀ¼
		/// </summary>
		private static String outFile = "";

		/// <summary>
		/// �Ƿ��ʼ��
		/// </summary>
		private static Boolean isInit=false; 

		/// <summary>
		/// ��ʼ��
		/// </summary>
		private static void Init()
		{
			isInit=true;
			level =Normal.ParseInt(ConfigurationManager.AppSettings["LogLevel"]);
            outFile = ConfigurationManager.AppSettings["LogPath"];
		}


		/// <summary>
		/// ���ش������
		/// </summary>
		/// <param name="str"></param>
		public static void Fatal(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.FATAL)
				List("[FATAL]" + str);
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="str"></param>
		public static void Error(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.ERROR)
				List("[ERROR]" + str);
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="str"></param>
		public static void Warn(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.WARN)
				List("[WARN]" + str);
		}

		/// <summary>
		/// ��Ϣ���
		/// </summary>
		/// <param name="str"></param>
		public static void Info(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.INFO)
				List("[INFO]" + str);
		}


		/// <summary>
		/// �������
		/// </summary>
		/// <param name="str"></param>
		public static void Debug(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.DEBUG)
				List("[DEBUG]" + str);
		}

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="str"></param>
		public static void Trace(String str) 
		{
			if(isInit==false)Init();
			if (level > LogLevel.NORMAL && level <= LogLevel.TRACE)
				List("[TRACE]" + str);
		}

		/// <summary>
		/// ��־������ļ�
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
		/// ��־�����ҳ��
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
