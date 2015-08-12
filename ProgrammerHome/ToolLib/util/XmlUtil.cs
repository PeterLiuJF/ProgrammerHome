using System;
using System.IO;
using System.Xml;
using System.Collections;
namespace ToolLib.util
{
	/// <summary>
	/// ��ȡXML�ļ�
	/// </summary>
	public class XmlUtil
	{
		/// <summary>
		/// ����
		/// </summary>
		private Hashtable props=null;
		
		public XmlUtil(string filePath)
		{
			props=new Hashtable();
			LoadFile(filePath);
		}

		/// <summary>
		/// ����XML�ļ�
		/// </summary>
		/// <param name="filePath"></param>
		private void LoadFile(string filePath){
			try
			{
				string filePath1=System.Web.HttpContext.Current.Server.MapPath(filePath);
				XmlReader reader = new XmlTextReader(filePath1);
				int i=0;
				string key="";
				while (reader.Read( )) 	
				{
				    i=i+1;
					if (reader.NodeType == XmlNodeType.Element) 
					{
						key=reader.LocalName;
					}else if(reader.NodeType==XmlNodeType.Text||reader.NodeType ==XmlNodeType.CDATA)
					{
						if(!key.Equals(""))props.Add(key,reader.Value);
					}
				}
				reader.Close();
			}
			catch{}
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
