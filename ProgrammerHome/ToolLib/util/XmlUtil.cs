using System;
using System.IO;
using System.Xml;
using System.Collections;
namespace ToolLib.util
{
	/// <summary>
	/// 读取XML文件
	/// </summary>
	public class XmlUtil
	{
		/// <summary>
		/// 属性
		/// </summary>
		private Hashtable props=null;
		
		public XmlUtil(string filePath)
		{
			props=new Hashtable();
			LoadFile(filePath);
		}

		/// <summary>
		/// 加载XML文件
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
		/// 获取属性VALUE
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
		/// 设置属性VALUE
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void SetValue(string key, string value) 
		{
			props[key]=value;
		}
	
		/// <summary>
		/// 获取属性
		/// </summary>
		/// <returns></returns>
		public Hashtable GetProperties()
		{			
			return props;
		}

		/// <summary>
		/// 设置属性 
		/// </summary>
		/// <param name="props"></param>
		public void SetProperties(Hashtable props)
		{
			this.props=props;
		}
	}



	}
