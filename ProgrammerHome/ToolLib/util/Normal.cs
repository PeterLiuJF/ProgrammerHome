using System;
using System.Text;
namespace ToolLib.util
{
	/// <summary>
	/// ����������,��Ҫ��������������͵�ת��,��ʽ��
	/// </summary>
	public class Normal:FormatUtil
	{

		/// <summary>
		/// ȥ���ո�
		/// </summary>
		/// <param name="obj">Ҫ���������</param>
		/// <returns>���ش���������</returns>
		public static string Trim(object obj)
		{
			return ParseString(obj).Trim();
		}


		/// <summary>
		/// �����Ŵ���,�������ݿ����ʱ���ִ���
		/// </summary>
		/// <param name="obj">Ҫ���������</param>
		/// <returns>���ش���������</returns>
		public static string CheckPoint(object obj) 
		{
			return ParseString(obj).Replace("'", "''");
		}

		/// <summary>
		/// �ı���ʾ(����)
		///  ʮ���Ʊ���:
		///  < &lt; 
		///  > &gt;
		///  ' &#39;
		///  " &quot; 
		///  & &amp; 
		///  13 <br>
		///  * 32 &nbsp; 
		/// </summary>
		/// <param name="obj">Ҫ���������</param>
		/// <returns>���ش���������</returns>
		public static string ListStr(object obj) 
		{
			return ParseString(obj).Replace("<", "&lt;")
				.Replace(">", "&gt;").Replace("\"", "&quot;");
		}

		/// <summary>
		/// �ı���ʾ (����)
		/// </summary>
		/// <param name="obj">Ҫ���������</param>
		/// <returns>���ش���������</returns>
		public static string ListStrs(object obj) 
		{
			char ch1 = (char)13; // ����
			char ch2 = (char)32; // �ո�
			return ParseString(obj).Replace("<", "&lt;")
				.Replace(">", "&gt;")
				.Replace(ParseString(ch1), "<br>").Replace(
				Normal.ParseString(ch2), "&nbsp;");
		}

		/// <summary>
		/// IE����
		/// </summary>
		/// <param name="obj">Ҫ���������</param>
		/// <returns>���ش���������</returns>
		public static string ListStrsRev(object obj) 
		{
			return ParseString(obj).Replace("&lt;","<")
				.Replace("&gt;",">");
		}

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="levelStyle"></param>
        /// <returns></returns>
        public static string GetMessage(string msg, string levelStyle)
        {
            if (msg.Equals("")) msg = "û����Ҫ��ѯ������!";
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
