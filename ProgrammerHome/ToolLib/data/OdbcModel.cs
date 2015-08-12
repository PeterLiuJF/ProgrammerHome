using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Odbc;
using ToolLib.exception;


namespace ToolLib.data
{
    public class OdbcModel
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static OdbcConnection GetConnection(string connstr)
        {
            OdbcConnection conn = null;
            try
            {
                conn = new OdbcConnection(connstr);
            }
            catch (Exception e)
            {
                //MessageBox.Show("数据库连接失败", "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(e.Message);
            }
            return conn;
        }



        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static OdbcConnection GetConnection()
        {
            OdbcConnection conn = null;
            string ConnectString = ConfigurationManager.ConnectionStrings["DB2"].ConnectionString;
            try
            {
                conn = new OdbcConnection(ConnectString);
            }
            catch (Exception e)
            {
                //MessageBox.Show( "数据库连接失败", "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                throw new DatabaseException(e.Message);
            }
            return conn;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable Query(string sql, string connstr)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcDataAdapter oda = new OdbcDataAdapter(sql, conn);
                oda.SelectCommand.CommandTimeout = 2000;
                conn.Open();
                oda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                //  MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);// + "\n" + e.Message

                throw new DatabaseException(sql + "\n" + e.Message);

            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            return dt;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable Query(string sql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection();
                OdbcDataAdapter oda = new OdbcDataAdapter(sql, conn);
                oda.SelectCommand.CommandTimeout = 60000;
                conn.Open();
                oda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                throw new DatabaseException(sql + "\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            return dt;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        public static int Execute(string sql, string connstr, OdbcParameter[] parameters)
        {
            int n = -1;
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand();//sql, conn);
                cmd.CommandText = sql;
                cmd.Connection = conn;
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].ParameterName, parameters[i].Value);
                }
                conn.Open();
                n = cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(sql + "\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            conn = null;
            return n;

        }
        /// <summary>
        /// 查询插入后的id
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable QueryInsertID(string sql, string connstr, OdbcParameter[] parameters)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand();//sql, conn);
                cmd.CommandText = sql;
                cmd.Connection = conn;
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].ParameterName, parameters[i].Value);
                }
                OdbcDataAdapter oda = new OdbcDataAdapter(cmd);
                oda.SelectCommand.CommandTimeout = 2000;
                conn.Open();
                oda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {

                throw new DatabaseException(sql + "\n" + e.Message);

            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            return dt;
        }

        /// <summary>
        /// 利用存储过程查询插入后的id
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable QueryInsertIDByStored(string sql, string connstr, OdbcParameter[] parameters)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand();//sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = sql;
                cmd.Connection = conn;
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].ParameterName, parameters[i].Value);
                }
                OdbcDataAdapter oda = new OdbcDataAdapter(cmd);
                oda.SelectCommand.CommandTimeout = 2000;
                conn.Open();
                oda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {

                throw new DatabaseException(sql + "\n" + e.Message);

            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            return dt;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        public static int Execute(string sql, string connstr)
        {
            int n = -1;
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                conn.Open();
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(sql + "\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            conn = null;
            return n;

        }

        /// <summary>
        /// 执行UPDATE、INSERT 或 DELETE
        /// </summary>
        /// <param name="sql">UPDATE、INSERT 或 DELETE语句</param>
        /// <param name="connstr">数据库连接字符串</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns>受影响的行数</returns>
        public static int Execute(string sql, string connstr, int timeOut)
        {
            int n = -1;
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                cmd.CommandTimeout = timeOut;
                conn.Open();
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(sql + "\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            conn = null;
            return n;

        }

        /// <summary>
        /// 执行存储过程(没有返回值)
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="connstr">数据库连接</param>
        /// <param name="parameters">存储过程参数集合</param>
        public static void ExecuteProcedure(string procedureName, string connstr, OdbcParameter[] parameters)
        {
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = conn;
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].ParameterName, parameters[i].Value);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException("存储过程执行出错\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        /// <summary>
        /// 执行存储过程(没有返回值)
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="connstr">数据库连接</param>
        /// <param name="parameters">存储过程参数集合</param>
        /// <param name="intTimeOut">存储过程执行的超时时间(秒)</param>
        public static void ExecuteProcedure(string procedureName, string connstr, OdbcParameter[] parameters, int intTimeOut)
        {
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = conn;
                cmd.CommandText = procedureName;
                cmd.CommandTimeout = intTimeOut;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i].ParameterName, parameters[i].Value);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException("存储过程执行出错\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        /// <summary>
        /// 插入并取得插入记录自增长主键
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="connstr">连接</param>
        /// <param name="identityKey">自增长主键</param>
        /// <returns>受影响的行数</returns>
        public static int Insert(string sql, string connstr, out int identityKey)
        {
            identityKey = 0;
            int n = -1;
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection(connstr);
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                conn.Open();
                n = cmd.ExecuteNonQuery();
                cmd.CommandText = "select @@identity";
                identityKey = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(sql + "\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            conn = null;
            return n;

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        public static int Execute(string sql)
        {
            int n = -1;
            OdbcConnection conn = null;
            try
            {
                conn = GetConnection();
                OdbcCommand cmd = new OdbcCommand(sql, conn);
                conn.Open();
                n = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //MessageBox.Show( sql, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(sql + "\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            conn = null;
            return n;
        }
    }
}
