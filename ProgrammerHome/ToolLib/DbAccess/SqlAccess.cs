using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ToolLib.util;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-13 
 * Content: SqlServer 数据源访问类
 ******************************************************************/

namespace ToolLib.DbAccess
{
    /// <summary>
    /// SqlServer 数据源访问类
    /// </summary>
    public class SqlAccess
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlAccess()
        {

        }
        #endregion

        #region 获取数据库链接
        /// <summary>
        /// 获取数据库链接，根据指定connName取相应connectionString；如果connName为空，默认取 "DefaultSqlConnStr" 对应的 connectionString。
        /// </summary>
        /// <param name="connName">App.Config文件中connectionStrings节中 connectionString 对应的name</param>
        /// <returns>数据库链接 SqlConnection 对象</returns>
        private static SqlConnection GetConnection(string connName)
        {
            SqlConnection conn = null;
            string connectString = "";

            //取得数据库连接字符串
            connectString = GetConnStr(connName);

            try
            {
                if (!string.IsNullOrEmpty(connectString))
                {
                    conn = new SqlConnection(connectString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return conn;
        }
        #endregion

        #region 取得数据库连接字符串
        /// <summary>
        /// 取得数据库连接字符串
        /// </summary>
        /// <param name="connName">App.Config文件中connectionStrings节中 connectionString 对应的name</param>
        /// <returns>数据库连接字符串</returns>
        private static string GetConnStr(string connName)
        {
            string connectString = "";
            string isEncrypt = "";

            //如果connName为空，默认取 "DefaultSqlConnStr" 对应的 connectionString
            if (string.IsNullOrEmpty(connName))
            {
                connName = "DefaultSqlConnStr";
            }

            connectString = ConfigurationManager.ConnectionStrings[connName].ConnectionString.ToString();

            if (isEncrypt.ToLower() == "true")
            {
                //解密
                connectString = PwdUtil.DesDecrypt(connectString);
            }

            return connectString;
        }
        #endregion

        #region Select 语句执行

        #region QueryDs  执行查询操作，查询结果保存至 DataSet 中
        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="tableName">指定填充的TableName</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        public static void QueryDs(DataSet ds, string tableName, string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (sqlParams != null)
                {
                    foreach (SqlParameter sp in sqlParams)
                    {
                        cmd.Parameters.AddWithValue(sp.ParameterName, sp.Value);
                    }
                }

                //等待命令执行的时间（以秒为单位）,默认值为 30 秒。 
                if (timeOut <= 30)
                {
                    cmd.CommandTimeout = 30;
                }
                else
                {
                    cmd.CommandTimeout = timeOut;
                }

                adapter.SelectCommand = cmd;

                conn.Open();

                if (!string.IsNullOrEmpty(tableName))
                {
                    adapter.Fill(ds, tableName);
                }
                else
                {
                    adapter.Fill(ds);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                cmd.Dispose();
                adapter.Dispose();
            }
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="sql">sql语句</param>
        public static void QueryDs(DataSet ds, string sql)
        {
            QueryDs(ds, string.Empty, sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDs(DataSet ds, string sql, string connName)
        {
            QueryDs(ds, string.Empty, sql, null, connName, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        public static void QueryDs(DataSet ds, string sql, SqlParameter[] sqlParams)
        {
            QueryDs(ds, string.Empty, sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDs(DataSet ds, string sql, SqlParameter[] sqlParams, string connName)
        {
            QueryDs(ds, string.Empty, sql, sqlParams, connName, 30);
        }
        #endregion

        #region QueryDt  执行查询操作，查询结果保存至 DataTable 中
        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>        
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        public static void QueryDt(DataTable dt, string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (sqlParams != null)
                {
                    foreach (SqlParameter sp in sqlParams)
                    {
                        cmd.Parameters.AddWithValue(sp.ParameterName, sp.Value);
                    }
                }

                //等待命令执行的时间（以秒为单位）,默认值为 30 秒。 
                if (timeOut <= 30)
                {
                    cmd.CommandTimeout = 30;
                }
                else
                {
                    cmd.CommandTimeout = timeOut;
                }

                adapter.SelectCommand = cmd;

                conn.Open();

                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                cmd.Dispose();
                adapter.Dispose();
            }
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>
        /// <param name="sql">sql语句</param>
        public static void QueryDt(DataTable dt, string sql)
        {
            QueryDt(dt, sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDt(DataTable dt, string sql, string connName)
        {
            QueryDt(dt, sql, null, connName, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        public static void QueryDt(DataTable dt, string sql, SqlParameter[] sqlParams)
        {
            QueryDt(dt, sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDt(DataTable dt, string sql, SqlParameter[] sqlParams, string connName)
        {
            QueryDt(dt, sql, sqlParams, connName, 30);
        }
        #endregion

        #region QueryDr  执行查询操作，返回 SqlDataReader 对象
        /// <summary>
        /// 执行查询操作，返回 SqlDataReader 对象。
        /// </summary>           
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        /// <returns>SqlDataReader 对象</returns>
        public static SqlDataReader QueryDr(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (sqlParams != null)
                {
                    foreach (SqlParameter sp in sqlParams)
                    {
                        cmd.Parameters.AddWithValue(sp.ParameterName, sp.Value);
                    }
                }

                //等待命令执行的时间（以秒为单位）,默认值为 30 秒。 
                if (timeOut <= 30)
                {
                    cmd.CommandTimeout = 30;
                }
                else
                {
                    cmd.CommandTimeout = timeOut;
                }

                conn.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //conn.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 执行查询操作，返回 SqlDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>SqlDataReader 对象</returns>
        public static SqlDataReader QueryDr(string sql)
        {
            return QueryDr(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 SqlDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>SqlDataReader 对象</returns>
        public static SqlDataReader QueryDr(string sql, string connName)
        {
            return QueryDr(sql, null, connName, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 SqlDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <returns>SqlDataReader 对象</returns>
        public static SqlDataReader QueryDr(string sql, SqlParameter[] sqlParams)
        {
            return QueryDr(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 SqlDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>SqlDataReader 对象</returns>
        public static SqlDataReader QueryDr(string sql, SqlParameter[] sqlParams, string connName)
        {
            return QueryDr(sql, sqlParams, connName, 30);
        }

        #endregion

        #region QueryObj  执行查询操作，返回 Object 对象 (单值)
        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>           
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            Object obj = null;

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (sqlParams != null)
                {
                    foreach (SqlParameter sp in sqlParams)
                    {
                        cmd.Parameters.Add(sp);
                    }
                }

                //等待命令执行的时间（以秒为单位）,默认值为 30 秒。 
                if (timeOut <= 30)
                {
                    cmd.CommandTimeout = 30;
                }
                else
                {
                    cmd.CommandTimeout = timeOut;
                }

                conn.Open();

                obj = cmd.ExecuteScalar();

                return obj;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql)
        {
            return QueryObj(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, string connName)
        {
            return QueryObj(sql, null, connName, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, SqlParameter[] sqlParams)
        {
            return QueryObj(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, SqlParameter[] sqlParams, string connName)
        {
            return QueryObj(sql, sqlParams, connName, 30);
        }

        #endregion

        #endregion

        #region Insert,Update,Delete 语句执行

        #region Execute  执行SQL语句，返回受影响的行数 (无事物)
        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            int intResult = 0;

            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (sqlParams != null)
                {
                    foreach (SqlParameter sp in sqlParams)
                    {
                        cmd.Parameters.AddWithValue(sp.ParameterName, sp.Value);
                    }
                }

                //等待命令执行的时间（以秒为单位）,默认值为 30 秒。 
                if (timeOut <= 30)
                {
                    cmd.CommandTimeout = 30;
                }
                else
                {
                    cmd.CommandTimeout = timeOut;
                }

                conn.Open();

                intResult = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                cmd.Dispose();
            }

            return intResult;
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql)
        {
            return Execute(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, string connName)
        {
            return Execute(sql, null, connName, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, SqlParameter[] sqlParams)
        {
            return Execute(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, SqlParameter[] sqlParams, string connName)
        {
            return Execute(sql, sqlParams, connName, 30);
        }

        #endregion

        #region ExecuteTran  执行SQL语句，返回受影响的行数 (带事物)
        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            int intResult = 0;

            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlTransaction sqlTran = null;

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (sqlParams != null)
                {
                    foreach (SqlParameter sp in sqlParams)
                    {
                        cmd.Parameters.AddWithValue(sp.ParameterName, sp.Value);
                    }
                }

                //等待命令执行的时间（以秒为单位）,默认值为 30 秒。 
                if (timeOut <= 30)
                {
                    cmd.CommandTimeout = 30;
                }
                else
                {
                    cmd.CommandTimeout = timeOut;
                }

                conn.Open();

                sqlTran = conn.BeginTransaction();
                cmd.Transaction = sqlTran;

                intResult = cmd.ExecuteNonQuery();

                sqlTran.Commit();

            }
            catch (Exception ex)
            {
                sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                cmd.Dispose();
            }

            return intResult;
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql)
        {
            return ExecuteTran(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, string connName)
        {
            return ExecuteTran(sql, null, connName, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, SqlParameter[] sqlParams)
        {
            return ExecuteTran(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, SqlParameter[] sqlParams, string connName)
        {
            return ExecuteTran(sql, sqlParams, connName, 30);
        }

        #endregion

        #endregion
    }
}
