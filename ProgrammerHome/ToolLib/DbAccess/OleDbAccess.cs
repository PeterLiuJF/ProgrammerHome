using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Configuration;
using ToolLib.util;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-15 
 * Content: OleDb 数据源访问类
 ******************************************************************/

namespace ToolLib.DbAccess
{
    /// <summary>
    /// OleDb 数据源访问类
    /// </summary>
    public class OleDbAccess
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OleDbAccess()
        {

        }
        #endregion

        #region 获取数据库链接
        /// <summary>
        /// 获取数据库链接，根据指定connName取相应connectionString；如果connName为空，默认取 "DefaultOleConnStr" 对应的 connectionString。
        /// </summary>
        /// <param name="connName">App.Config文件中connectionStrings节中 connectionString 对应的name</param>
        /// <returns>数据库链接 OleDbConnection 对象</returns>
        private static OleDbConnection GetConnection(string connName)
        {
            OleDbConnection conn = null;
            string connectString = "";

            //取得数据库连接字符串
            connectString = GetConnStr(connName);

            try
            {
                if (!string.IsNullOrEmpty(connectString))
                {
                    conn = new OleDbConnection(connectString);
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

            //如果connName为空，默认取 "DefaultOleConnStr" 对应的 connectionString
            if (string.IsNullOrEmpty(connName))
            {
                connName = "DefaultOleConnStr";
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
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        public static void QueryDs(DataSet ds, string tableName, string sql, OleDbParameter[] OleDbParams, string connName, int timeOut)
        {
            OleDbConnection conn = null;
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OleDbParams != null)
                {
                    foreach (OleDbParameter sp in OleDbParams)
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
        /// <param name="OleDbParams">参数数组</param>
        public static void QueryDs(DataSet ds, string sql, OleDbParameter[] OleDbParams)
        {
            QueryDs(ds, string.Empty, sql, OleDbParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataSet 中。
        /// </summary>
        /// <param name="ds">保存结果的数据集</param>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDs(DataSet ds, string sql, OleDbParameter[] OleDbParams, string connName)
        {
            QueryDs(ds, string.Empty, sql, OleDbParams, connName, 30);
        }
        #endregion

        #region QueryDt  执行查询操作，查询结果保存至 DataTable 中
        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>        
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        public static void QueryDt(DataTable dt, string sql, OleDbParameter[] OleDbParams, string connName, int timeOut)
        {
            OleDbConnection conn = null;
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OleDbParams != null)
                {
                    foreach (OleDbParameter sp in OleDbParams)
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
        /// <param name="OleDbParams">参数数组</param>
        public static void QueryDt(DataTable dt, string sql, OleDbParameter[] OleDbParams)
        {
            QueryDt(dt, sql, OleDbParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，查询结果保存至 DataTable 中。
        /// </summary>
        /// <param name="dt">保存结果的数据表</param>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接</param>
        public static void QueryDt(DataTable dt, string sql, OleDbParameter[] OleDbParams, string connName)
        {
            QueryDt(dt, sql, OleDbParams, connName, 30);
        }
        #endregion

        #region QueryDr  执行查询操作，返回 OleDbDataReader 对象
        /// <summary>
        /// 执行查询操作，返回 OleDbDataReader 对象。
        /// </summary>           
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        /// <returns>OleDbDataReader 对象</returns>
        public static OleDbDataReader QueryDr(string sql, OleDbParameter[] OleDbParams, string connName, int timeOut)
        {
            OleDbConnection conn = null;
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr = null;
            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OleDbParams != null)
                {
                    foreach (OleDbParameter sp in OleDbParams)
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
        /// 执行查询操作，返回 OleDbDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>OleDbDataReader 对象</returns>
        public static OleDbDataReader QueryDr(string sql)
        {
            return QueryDr(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 OleDbDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>OleDbDataReader 对象</returns>
        public static OleDbDataReader QueryDr(string sql, string connName)
        {
            return QueryDr(sql, null, connName, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 OleDbDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <returns>OleDbDataReader 对象</returns>
        public static OleDbDataReader QueryDr(string sql, OleDbParameter[] OleDbParams)
        {
            return QueryDr(sql, OleDbParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 OleDbDataReader 对象。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>OleDbDataReader 对象</returns>
        public static OleDbDataReader QueryDr(string sql, OleDbParameter[] OleDbParams, string connName)
        {
            return QueryDr(sql, OleDbParams, connName, 30);
        }

        #endregion

        #region QueryObj  执行查询操作，返回 Object 对象 (单值)
        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>           
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>  
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, OleDbParameter[] OleDbParams, string connName, int timeOut)
        {
            OleDbConnection conn = null;
            OleDbCommand cmd = new OleDbCommand();
            Object obj = null;

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OleDbParams != null)
                {
                    foreach (OleDbParameter sp in OleDbParams)
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
        /// <param name="OleDbParams">参数数组</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, OleDbParameter[] OleDbParams)
        {
            return QueryObj(sql, OleDbParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行查询操作，返回 Object 对象 (单值)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>Object 对象</returns>
        public static Object QueryObj(string sql, OleDbParameter[] OleDbParams, string connName)
        {
            return QueryObj(sql, OleDbParams, connName, 30);
        }

        #endregion

        #endregion

        #region Insert,Update,Delete 语句执行

        #region Execute  执行SQL语句，返回受影响的行数 (无事物)
        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, OleDbParameter[] OleDbParams, string connName, int timeOut)
        {
            int intResult = 0;

            OleDbConnection conn = null;
            OleDbCommand cmd = new OleDbCommand();

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OleDbParams != null)
                {
                    foreach (OleDbParameter sp in OleDbParams)
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
        /// <param name="OleDbParams">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, OleDbParameter[] OleDbParams)
        {
            return Execute(sql, OleDbParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, OleDbParameter[] OleDbParams, string connName)
        {
            return Execute(sql, OleDbParams, connName, 30);
        }

        #endregion

        #region ExecuteTran  执行SQL语句，返回受影响的行数 (带事物)
        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <param name="timeOut">等待命令执行的时间（以秒为单位）,默认值为 30 秒。</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, OleDbParameter[] OleDbParams, string connName, int timeOut)
        {
            int intResult = 0;

            OleDbConnection conn = null;
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction sqlTran = null;

            try
            {
                //取得数据库连接
                conn = GetConnection(connName);

                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                if (OleDbParams != null)
                {
                    foreach (OleDbParameter sp in OleDbParams)
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
        /// <param name="OleDbParams">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, OleDbParameter[] OleDbParams)
        {
            return ExecuteTran(sql, OleDbParams, string.Empty, 30);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="OleDbParams">参数数组</param>
        /// <param name="connName">指定使用的数据库连接名称</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteTran(string sql, OleDbParameter[] OleDbParams, string connName)
        {
            return ExecuteTran(sql, OleDbParams, connName, 30);
        }

        #endregion

        #endregion
    }
}
