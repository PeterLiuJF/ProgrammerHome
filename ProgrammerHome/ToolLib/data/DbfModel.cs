using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data;
using System.IO;
using ToolLib.exception;
using System.Configuration;

namespace ToolLib.data
{
    public class DbfModel
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
                throw new DatabaseException(connstr + "\n" + e.Message);
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
            string ConnectString = ConfigurationManager.ConnectionStrings["DBF"].ConnectionString;
            try
            {
                conn = new OdbcConnection(ConnectString);
            }
            catch (Exception e)
            {
                throw new DatabaseException(ConnectString + "\n" + e.Message);
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
                OdbcDataAdapter sda = new OdbcDataAdapter(sql, conn);
                conn.Open();
                sda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw new DAOException(sql + "\n" + e.Message);
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
                OdbcDataAdapter sda = new OdbcDataAdapter(sql, conn);
                conn.Open();
                sda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw new DAOException(sql + "\n" + e.Message);
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
                throw new DAOException(sql + "\n" + e.Message);

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
                throw new DAOException(sql + "\n" + e.Message);

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
                throw new DAOException(sql + "\n" + e.Message);

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
