using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Configuration;
using ToolLib.util;
using System.Data.OracleClient;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-13 
 * Content: Oracle 数据源访问类
 ******************************************************************/

namespace ToolLib.DbAccess
{
    /// <summary>
    /// Oracle 数据源访问类
    /// </summary>
    public class OracleAccess
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OracleAccess()
        {

        }
        #endregion

        #region 获取数据库链接
        /// <summary>
        /// 获取数据库链接，根据指定connName取相应connectionString；如果connName为空，默认取 "DefaultOracleConnStr" 对应的 connectionString。
        /// </summary>
        /// <param name="connName">App.Config文件中connectionStrings节中 connectionString 对应的name</param>
        /// <returns>数据库链接 OleDbConnection 对象</returns>
        private static OracleConnection GetConnection(string connName)
        {
            OracleConnection conn = null;
            string connectString = "";

            //取得数据库连接字符串
            connectString = GetConnStr(connName);

            try
            {
                if (!string.IsNullOrEmpty(connectString))
                {
                    conn = new OracleConnection(connectString);
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

            //如果connName为空，默认取 "DefaultOracleConnStr" 对应的 connectionString
            if (string.IsNullOrEmpty(connName))
            {
                connName = "DefaultOracleConnStr";
            }

            connectString = ConfigurationManager.ConnectionStrings[connName.Trim()].ConnectionString.Trim();

            isEncrypt = ConfigurationManager.AppSettings["isEncrypt_" + connName].Trim();

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
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        public static void QueryDs(DataSet ds, string tableName, string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter adapter = new OracleDataAdapter();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OracleParams != null)
                {
                    foreach (OracleParameter sp in OracleParams)
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
        /// <param name="OracleParams">参数数组</param>
        public static void QueryDs(DataSet ds, string sql, OracleParameter[] OracleParams)
        {
            QueryDs(ds, string.Empty, sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDs(DataSet ds, string sql, OracleParameter[] OracleParams, string connName)
        {
            QueryDs(ds, string.Empty, sql, OracleParams, connName, 30);
        }
        #endregion

        #region QueryDt  执行查询操作，查询结果保存至 DataTable 中
        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>        
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        public static void QueryDt(DataTable dt, string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter adapter = new OracleDataAdapter();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OracleParams != null)
                {
                    foreach (OracleParameter sp in OracleParams)
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
        /// <param name="OracleParams">参数数组</param>
        public static void QueryDt(DataTable dt, string sql, OracleParameter[] OracleParams)
        {
            QueryDt(dt, sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDt(DataTable dt, string sql, OracleParameter[] OracleParams, string connName)
        {
            QueryDt(dt, sql, OracleParams, connName, 30);
        }
        #endregion

        #region QueryDr  执行查询操作，返回 OracleDataReader 对象
        /// <summary>
        /// 执行查询操作，返回 OracleDataReader 对象。
        /// </summary>           
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        /// <returns>OracleDataReader 对象</returns>
        public static OracleDataReader QueryDr(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr = null;
            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OracleParams != null)
                {
                    foreach (OracleParameter sp in OracleParams)
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
        /// 执行查询操作，返回 OracleDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>OracleDataReader 对象</returns>
        public static OracleDataReader QueryDr(string sql)
        {
            return QueryDr(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 OracleDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>OracleDataReader 对象</returns>
        public static OracleDataReader QueryDr(string sql, string connName)
        {
            return QueryDr(sql, null, connName, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 OracleDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <returns>OracleDataReader 对象</returns>
        public static OracleDataReader QueryDr(string sql, OracleParameter[] OracleParams)
        {
            return QueryDr(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 OracleDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>OracleDataReader 对象</returns>
        public static OracleDataReader QueryDr(string sql, OracleParameter[] OracleParams, string connName)
        {
            return QueryDr(sql, OracleParams, connName, 30);
        }

        #endregion

        #region QueryObj  执行查询操作，返回 Object 对象 (单值)
        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>           
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            Object obj = null;

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OracleParams != null)
                {
                    foreach (OracleParameter sp in OracleParams)
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
        /// <param name="OracleParams">参数数组</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, OracleParameter[] OracleParams)
        {
            return QueryObj(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, OracleParameter[] OracleParams, string connName)
        {
            return QueryObj(sql, OracleParams, connName, 30);
        }

        #endregion

        #endregion

        #region Insert,Update,Delete 语句执行

        #region Execute  执行SQL语句，返回受影响的行数 (无事物)
        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            int intResult = 0;

            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OracleParams != null)
                {
                    foreach (OracleParameter sp in OracleParams)
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
        /// <param name="OracleParams">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, OracleParameter[] OracleParams)
        {
            return Execute(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, OracleParameter[] OracleParams, string connName)
        {
            return Execute(sql, OracleParams, connName, 30);
        }

        #endregion

        #region ExecuteTran  执行SQL语句，返回受影响的行数 (带事物)
        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            int intResult = 0;

            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleTransaction sqlTran = null;

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OracleParams != null)
                {
                    foreach (OracleParameter sp in OracleParams)
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
        /// <param name="OracleParams">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, OracleParameter[] OracleParams)
        {
            return ExecuteTran(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OracleParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, OracleParameter[] OracleParams, string connName)
        {
            return ExecuteTran(sql, OracleParams, connName, 30);
        }

        #endregion

        #endregion
    }
}
