using System;
using System.Text;
namespace ToolLib.util
{
	/// <summary>
	/// 公共函数类,主要处理各种数据类型的转换,格式化
	/// </summary>
	public class Normal:FormatUtil
	{

		/// <summary>
		/// 去掉空格
		/// </summary>
		/// <param name="obj">要处理的数据</param>
		/// <returns>返回处理后的数据</returns>
		public static string Trim(object obj)
		{
			return ParseString(obj).Trim();
		}


		/// <summary>
		/// 单引号处理,避免数据库操作时出现错误
		/// </summary>
		/// <param name="obj">要处理的数据</param>
		/// <returns>返回处理后的数据</returns>
		public static string CheckPoint(object obj) 
		{
			return ParseString(obj).Replace("'", "''");
		}

		/// <summary>
		/// 文本显示(单行)
		///  十进制编码:
		///  < &lt; 
		///  > &gt;
		///  ' &#39;
		///  " &quot; 
		///  & &amp; 
		///  13 <br>
		///  * 32 &nbsp; 
		/// </summary>
		/// <param name="obj">要处理的数据</param>
		/// <returns>返回处理后的数据</returns>
		public static string ListStr(object obj) 
		{
			return ParseString(obj).Replace("<", "&lt;")
				.Replace(">", "&gt;").Replace("\"", "&quot;");
		}

		/// <summary>
		/// 文本显示 (多行)
		/// </summary>
		/// <param name="obj">要处理的数据</param>
		/// <returns>返回处理后的数据</returns>
		public static string ListStrs(object obj) 
		{
			char ch1 = (char)13; // 换行
			char ch2 = (char)32; // 空格
			return ParseString(obj).Replace("<", "&lt;")
				.Replace(">", "&gt;")
				.Replace(ParseString(ch1), "<br>").Replace(
				Normal.ParseString(ch2), "&nbsp;");
		}

		/// <summary>
		/// IE解析
		/// </summary>
		/// <param name="obj">要处理的数据</param>
		/// <returns>返回处理后的数据</returns>
		public static string ListStrsRev(object obj) 
		{
			return ParseString(obj).Replace("&lt;","<")
				.Replace("&gt;",">");
		}

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="levelStyle"></param>
        /// <returns></returns>
        public static string GetMessage(string msg, string levelStyle)
        {
            if (msg.Equals("")) msg = "没有您要查询的资料!";
            if (levelStyle.Equals("")) levelStyle = "error";

            StringBuilder str = new StringBuilder();

            str.Append("<TABLE WIDTH=100% BORDER=0 CELLSPACING=0 CELLPADDING=3 align=center>");
            str.Append("<TR>");
            str.Append("<TD align=center class=" + levelStyle + ">" + msg + "</TD>");
            str.Append("</TR>");
            str.Append("</TABLE>");
            return str.ToString();
        }

        public static string GetMessage(string msg)
        {
            return GetMessage(msg, "error");
        }

        
    }
}
