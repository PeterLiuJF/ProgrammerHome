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
    /// ���ݿ�������
    /// </summary>
    public class SqlTransModel
    {

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        private static SqlConnection conn = null;

        /// <summary>
        /// ����
        /// </summary>
        private static SqlTransaction trans = null;

        /// <summary>
        /// ��ȡ���ݿ�����
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
        /// ��ѯ
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
        /// ����
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
        /// ��ʼ����
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
        ///  �ύ����
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
        /// �ع�����
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
        /// �ر�����
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
