using System;
using System.Configuration;
using System.Data;

namespace ToolLib.data
{
	/// <summary>
	/// 数据库操作。
	/// </summary>
	public class DbModel
	{

		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static DataTable Query(string type,string key,string sql)
		{
            DataTable dt=null;
            if (type.Equals("")) type = DbTypes.SQL;
            switch (type)
            {
                case DbTypes.SQL:
                    dt = SqlModel.Query(key, sql); break;
                case DbTypes.ORA:
                    dt = OracleModel.Query(key, sql); break;
            }
            return dt;
		}

        public static DataTable Query(string key, string sql)
        {
            return Query(DbTypes.SQL, key, sql);
        }

        public static DataTable Query(string sql)
        {
            return Query(DbTypes.SQL, DbKey.SQL, sql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="sql"></param>
        public static int Execute(string type, string key, string sql)
        {
            int n = -1;
            if (type.Equals("")) type = DbTypes.SQL;
            switch (type)
            {
                case DbTypes.SQL:
                   n= SqlModel.Execute(key, sql); break;
                case DbTypes.ORA:
                    n=OracleModel.Execute(key, sql); break;
            }
            return n;
        }

        public static int Execute(string key, string sql)
        {
            return Execute(DbTypes.SQL, key, sql);
        }

        public static int Execute(string sql)
        {
            return Execute(DbTypes.SQL, DbKey.SQL, sql);
        }

		
	}
}
