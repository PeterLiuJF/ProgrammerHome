using System;
using System.Runtime.Serialization;
namespace ToolLib.exception
{
	/// <summary>
	/// ObjectNotExistException 的摘要说明。
	/// </summary>
	public class ObjectNotExistException:Exception 
	{
		public ObjectNotExistException():base()
		{
			;
		}

		public ObjectNotExistException(String message):base(message) 
		{
			;
		}

		public ObjectNotExistException(String message,Exception ex):base(message,ex) 
		{
			;
		}

		public ObjectNotExistException( SerializationInfo info,	StreamingContext context):base(info,context) 
		{
			;
		}
	}
}
