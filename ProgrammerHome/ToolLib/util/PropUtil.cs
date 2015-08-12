using System;
using System.Collections;
using System.IO;
namespace ToolLib.util
{
	/// <summary>
	/// ��ȡ�����ļ�
	/// </summary>
	public class PropUtil
	{
		/// <summary>
		/// ����
		/// </summary>
		private Hashtable props=null;
		
		/// <summary>
		/// ȱʡ��ע�ͷ���
		/// </summary>
		private static readonly string DEAFULT_NOTE_DENOTATION = @"#";		
		
		/// <summary>
		/// ȱʡ��key/value�ָ���.	
		/// </summary>
		private static readonly string DEFAULT_DELIMITER = @"=";	
		

		public PropUtil(string propFile)
		{
			props=new Hashtable();
			LoadFile(propFile);
		}

		/// <summary>
		/// ����properties�ļ�
		/// </summary>
		/// <param name="filePath">�ļ�·��</param>
		private void LoadFile(string filePath)
		{ 
			try
			{
				string filepath1=System.Web.HttpContext.Current.Server.MapPath(filePath);
				if (File.Exists(filepath1))
				{		      
					string textLine;
					StreamReader sr = new StreamReader(filepath1);
					while (sr.Peek() >= 0)
					{
						textLine = sr.ReadLine();
						if (textLine.Trim().Length > 0)
						{
							ParsePropertiesLine(props, textLine);
						}
					}
					sr.Close();
				}
			}
			catch{}
		}

		/// <summary>
		/// �����ַ�
		/// </summary>
		/// <param name="keyValues"></param>
		/// <param name="textLine"></param>
		private static void ParsePropertiesLine(Hashtable keyValues, string textLine) 
		{
			if (textLine==null||textLine=="")
			{
				return;
			}
  
			// ע��.
			if (textLine.StartsWith(DEAFULT_NOTE_DENOTATION))
			{
				return;
			}

			int pos = textLine.IndexOf(DEFAULT_DELIMITER);
			if (pos > 0)
			{
				keyValues.Add(textLine.Substring(0, pos).Trim(), textLine.Substring(pos + DEFAULT_DELIMITER.Length, textLine.Length - pos -DEFAULT_DELIMITER.Length));
			}
		}

		/// <summary>
		/// ��ȡ����VALUE
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string GetValue(string key) 
		{
			if(props!=null&&props.ContainsKey(key))
				return Normal.Trim(props[key]);
			return "";
		}
	
		/// <summary>
		/// ��������VALUE
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void SetValue(string key, string value) 
		{
			props[key]=value;
		}
	
		/// <summary>
		/// ��ȡ����
		/// </summary>
		/// <returns></returns>
		public Hashtable GetProperties()
		{			
			return props;
		}

		/// <summary>
		/// �������� 
		/// </summary>
		/// <param name="props"></param>
		public void SetProperties(Hashtable props)
		{
			this.props=props;
		}
	}
}
