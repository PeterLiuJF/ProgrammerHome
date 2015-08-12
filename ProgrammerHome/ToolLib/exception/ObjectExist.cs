using System;
using System.Runtime.Serialization;
namespace ToolLib.exception
{
	/// <summary>
	/// ObjectExistException 的摘要说明。
	/// </summary>
	public class ObjectExistException:Exception 
	{
		public ObjectExistException():base()
		{
			;
		}

		public ObjectExistException(String message):base(message) 
		{
			;
		}

		public ObjectExistException(String message,Exception ex):base(message,ex) 
		{
			;
		}

		public ObjectExistException( SerializationInfo info,	StreamingContext context):base(info,context) 
		{
			;
		}
	}
}
