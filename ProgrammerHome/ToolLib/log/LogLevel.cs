using System;

namespace ToolLib.log
{
	/// <summary>
	/// 跟踪级别
	/// </summary>
	public class LogLevel
	{
		
		/// <summary>
		/// 默认,不输出
		/// </summary>
		public const int NORMAL=0;
	
		/// <summary>
		///  跟踪
		/// </summary>
		public const int TRACE = 1;

		/// <summary>
		/// 调试
		/// </summary>
		public const int DEBUG = 2;

		/// <summary>
		/// 信息
		/// </summary>
		public const  int INFO = 3;

		/// <summary>
		/// 警告
		/// </summary>
		public const int WARN = 4;

		/// <summary>
		/// 错误
		/// </summary>
		public const int ERROR = 5;

		/// <summary>
		/// 严重错误
		/// </summary>
		public const int FATAL = 6;
	}
}
