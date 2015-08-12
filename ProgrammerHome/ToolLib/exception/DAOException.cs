using System;
using System.Runtime.Serialization;
namespace ToolLib.exception
{
	/// <summary>
	/// DAOException ��ժҪ˵����
	/// </summary>
	public class DAOException:Exception 
	{
		public DAOException():base()
		{
			;
		}

		public DAOException(String message):base(message) 
		{
			;
		}

		public DAOException(String message,Exception ex):base(message,ex) 
		{
			;
		}

		public DAOException( SerializationInfo info,	StreamingContext context):base(info,context) 
		{
			;
		}
	}
}
