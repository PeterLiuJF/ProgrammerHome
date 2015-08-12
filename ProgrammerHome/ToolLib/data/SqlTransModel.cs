using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ToolLib.exception;
using ToolLib.util;
using ToolLib.log;

namespace ToolLib.data
{
    /// <summary>
    /// 数据库事务处理
    /// </summary>
    public class SqlTransModel
    {

        /// <summary>
        /// 数据库连接
        /// </summary>
        private static SqlConnection conn = null;

        /// <summary>
        /// 事务
        /// </summary>
        private static SqlTransaction trans = null;

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        

        public static SqlConnection GetConnection(string key)
        {
           
            string connectString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[key].ConnectionString;

            try
            {
                conn = new SqlConnection(connectString);
            }
            catch (Exception ex)
            {
                throw new DAOException(connectString + "\n" + ex.Message);
            }
            return conn;
        }

        public static SqlConnection GetConnection()
        {
            return GetConnection(DbKey.SQL);
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
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new DAOException(sql + "\n" + ex.Message);
            }
            return dt;
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        public static void Execute(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn, trans);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DAOException(sql + "\n" + ex.Message);
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public static void BeginTrans()
        {
            BeginTrans(DbKey.SQL);
        }

        public static void BeginTrans(string key)
        {
            try
            {
                if (conn == null) conn=GetConnection(key);
                if (conn.State == ConnectionState.Closed) conn.Open();
                trans = conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                Log.Error("kao::"+ex.ToString());
                throw new Exception(key + "\n" + ex.Message);
            }
        }

        /// <summary>
        ///  提交事务
        /// </summary>
        public static void Commit()
        {
            try
            {
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void RollBack()
        {
            try
            {
                trans.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 关闭事务
        /// </summary>
        public static void EndTrans()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            conn = null;
        }
    }
}
