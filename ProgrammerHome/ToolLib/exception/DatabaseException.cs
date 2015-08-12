using System;
using System.Runtime.Serialization;
namespace ToolLib.exception
{
	/// <summary>
	/// DatabaseException 的摘要说明。
	/// </summary>
	public class DatabaseException:Exception 
	{
		public DatabaseException():base()
		{
			;
		}

		public DatabaseException(String message):base(message) 
		{
			;
		}

		public DatabaseException(String message,Exception ex):base(message,ex) 
		{
			;
		}

		public DatabaseException( SerializationInfo info,	StreamingContext context):base(info,context) 
		{
			;
		}
	}
}
