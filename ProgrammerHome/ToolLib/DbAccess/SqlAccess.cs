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
 * Content: SqlServer ����Դ������
 ******************************************************************/

namespace ToolLib.DbAccess
{
    /// <summary>
    /// SqlServer ����Դ������
    /// </summary>
    public class SqlAccess
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public SqlAccess()
        {

        }
        #endregion

        #region ��ȡ���ݿ�����
        /// <summary>
        /// ��ȡ���ݿ����ӣ�����ָ��connNameȡ��ӦconnectionString�����connNameΪ�գ�Ĭ��ȡ "DefaultSqlConnStr" ��Ӧ�� connectionString��
        /// </summary>
        /// <param name="connName">App.Config�ļ���connectionStrings���� connectionString ��Ӧ��name</param>
        /// <returns>���ݿ����� SqlConnection ����</returns>
        private static SqlConnection GetConnection(string connName)
        {
            SqlConnection conn = null;
            string connectString = "";

            //ȡ�����ݿ������ַ���
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

        #region ȡ�����ݿ������ַ���
        /// <summary>
        /// ȡ�����ݿ������ַ���
        /// </summary>
        /// <param name="connName">App.Config�ļ���connectionStrings���� connectionString ��Ӧ��name</param>
        /// <returns>���ݿ������ַ���</returns>
        private static string GetConnStr(string connName)
        {
            string connectString = "";
            string isEncrypt = "";

            //���connNameΪ�գ�Ĭ��ȡ "DefaultSqlConnStr" ��Ӧ�� connectionString
            if (string.IsNullOrEmpty(connName))
            {
                connName = "DefaultSqlConnStr";
            }

            connectString = ConfigurationManager.ConnectionStrings[connName].ConnectionString.ToString();

            if (isEncrypt.ToLower() == "true")
            {
                //����
                connectString = PwdUtil.DesDecrypt(connectString);
            }

            return connectString;
        }
        #endregion

        #region Select ���ִ��

        #region QueryDs  ִ�в�ѯ��������ѯ��������� DataSet ��
        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataSet �С�
        /// </summary>
        /// <param name="ds">�����������ݼ�</param>
        /// <param name="tableName">ָ������TableName</param>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        public static void QueryDs(DataSet ds, string tableName, string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                //ȡ�����ݿ�����
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

                //�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣 
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
        /// ִ�в�ѯ��������ѯ��������� DataSet �С�
        /// </summary>
        /// <param name="ds">�����������ݼ�</param>
        /// <param name="sql">sql���</param>
        public static void QueryDs(DataSet ds, string sql)
        {
            QueryDs(ds, string.Empty, sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataSet �С�
        /// </summary>
        /// <param name="ds">�����������ݼ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        public static void QueryDs(DataSet ds, string sql, string connName)
        {
            QueryDs(ds, string.Empty, sql, null, connName, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataSet �С�
        /// </summary>
        /// <param name="ds">�����������ݼ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        public static void QueryDs(DataSet ds, string sql, SqlParameter[] sqlParams)
        {
            QueryDs(ds, string.Empty, sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataSet �С�
        /// </summary>
        /// <param name="ds">�����������ݼ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        public static void QueryDs(DataSet ds, string sql, SqlParameter[] sqlParams, string connName)
        {
            QueryDs(ds, string.Empty, sql, sqlParams, connName, 30);
        }
        #endregion

        #region QueryDt  ִ�в�ѯ��������ѯ��������� DataTable ��
        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>        
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        public static void QueryDt(DataTable dt, string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                //ȡ�����ݿ�����
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

                //�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣 
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
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>
        /// <param name="sql">sql���</param>
        public static void QueryDt(DataTable dt, string sql)
        {
            QueryDt(dt, sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        public static void QueryDt(DataTable dt, string sql, string connName)
        {
            QueryDt(dt, sql, null, connName, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        public static void QueryDt(DataTable dt, string sql, SqlParameter[] sqlParams)
        {
            QueryDt(dt, sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        public static void QueryDt(DataTable dt, string sql, SqlParameter[] sqlParams, string connName)
        {
            QueryDt(dt, sql, sqlParams, connName, 30);
        }
        #endregion

        #region QueryDr  ִ�в�ѯ���������� SqlDataReader ����
        /// <summary>
        /// ִ�в�ѯ���������� SqlDataReader ����
        /// </summary>           
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        /// <returns>SqlDataReader ����</returns>
        public static SqlDataReader QueryDr(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            try
            {
                //ȡ�����ݿ�����
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

                //�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣 
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
        /// ִ�в�ѯ���������� SqlDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>SqlDataReader ����</returns>
        public static SqlDataReader QueryDr(string sql)
        {
            return QueryDr(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� SqlDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>SqlDataReader ����</returns>
        public static SqlDataReader QueryDr(string sql, string connName)
        {
            return QueryDr(sql, null, connName, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� SqlDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <returns>SqlDataReader ����</returns>
        public static SqlDataReader QueryDr(string sql, SqlParameter[] sqlParams)
        {
            return QueryDr(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� SqlDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>SqlDataReader ����</returns>
        public static SqlDataReader QueryDr(string sql, SqlParameter[] sqlParams, string connName)
        {
            return QueryDr(sql, sqlParams, connName, 30);
        }

        #endregion

        #region QueryObj  ִ�в�ѯ���������� Object ���� (��ֵ)
        /// <summary>
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>           
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            Object obj = null;

            try
            {
                //ȡ�����ݿ�����
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

                //�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣 
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
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql)
        {
            return QueryObj(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, string connName)
        {
            return QueryObj(sql, null, connName, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, SqlParameter[] sqlParams)
        {
            return QueryObj(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, SqlParameter[] sqlParams, string connName)
        {
            return QueryObj(sql, sqlParams, connName, 30);
        }

        #endregion

        #endregion

        #region Insert,Update,Delete ���ִ��

        #region Execute  ִ��SQL��䣬������Ӱ������� (������)
        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            int intResult = 0;

            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                //ȡ�����ݿ�����
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

                //�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣 
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
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql)
        {
            return Execute(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, string connName)
        {
            return Execute(sql, null, connName, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, SqlParameter[] sqlParams)
        {
            return Execute(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, SqlParameter[] sqlParams, string connName)
        {
            return Execute(sql, sqlParams, connName, 30);
        }

        #endregion

        #region ExecuteTran  ִ��SQL��䣬������Ӱ������� (������)
        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, SqlParameter[] sqlParams, string connName, int timeOut)
        {
            int intResult = 0;

            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            SqlTransaction sqlTran = null;

            try
            {
                //ȡ�����ݿ�����
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

                //�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣 
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
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql)
        {
            return ExecuteTran(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, string connName)
        {
            return ExecuteTran(sql, null, connName, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, SqlParameter[] sqlParams)
        {
            return ExecuteTran(sql, sqlParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="sqlParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, SqlParameter[] sqlParams, string connName)
        {
            return ExecuteTran(sql, sqlParams, connName, 30);
        }

        #endregion

        #endregion
    }
}
