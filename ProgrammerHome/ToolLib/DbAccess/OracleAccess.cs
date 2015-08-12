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
 * Content: Oracle ����Դ������
 ******************************************************************/

namespace ToolLib.DbAccess
{
    /// <summary>
    /// Oracle ����Դ������
    /// </summary>
    public class OracleAccess
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public OracleAccess()
        {

        }
        #endregion

        #region ��ȡ���ݿ�����
        /// <summary>
        /// ��ȡ���ݿ����ӣ�����ָ��connNameȡ��ӦconnectionString�����connNameΪ�գ�Ĭ��ȡ "DefaultOracleConnStr" ��Ӧ�� connectionString��
        /// </summary>
        /// <param name="connName">App.Config�ļ���connectionStrings���� connectionString ��Ӧ��name</param>
        /// <returns>���ݿ����� OleDbConnection ����</returns>
        private static OracleConnection GetConnection(string connName)
        {
            OracleConnection conn = null;
            string connectString = "";

            //ȡ�����ݿ������ַ���
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

            //���connNameΪ�գ�Ĭ��ȡ "DefaultOracleConnStr" ��Ӧ�� connectionString
            if (string.IsNullOrEmpty(connName))
            {
                connName = "DefaultOracleConnStr";
            }

            connectString = ConfigurationManager.ConnectionStrings[connName.Trim()].ConnectionString.Trim();

            isEncrypt = ConfigurationManager.AppSettings["isEncrypt_" + connName].Trim();

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
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        public static void QueryDs(DataSet ds, string tableName, string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter adapter = new OracleDataAdapter();

            try
            {
                //ȡ�����ݿ�����
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
        /// <param name="OracleParams">��������</param>
        public static void QueryDs(DataSet ds, string sql, OracleParameter[] OracleParams)
        {
            QueryDs(ds, string.Empty, sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataSet �С�
        /// </summary>
        /// <param name="ds">�����������ݼ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        public static void QueryDs(DataSet ds, string sql, OracleParameter[] OracleParams, string connName)
        {
            QueryDs(ds, string.Empty, sql, OracleParams, connName, 30);
        }
        #endregion

        #region QueryDt  ִ�в�ѯ��������ѯ��������� DataTable ��
        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>        
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        public static void QueryDt(DataTable dt, string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter adapter = new OracleDataAdapter();

            try
            {
                //ȡ�����ݿ�����
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
        /// <param name="OracleParams">��������</param>
        public static void QueryDt(DataTable dt, string sql, OracleParameter[] OracleParams)
        {
            QueryDt(dt, sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ��������ѯ��������� DataTable �С�
        /// </summary>
        /// <param name="dt">�����������ݱ�</param>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ�����</param>
        public static void QueryDt(DataTable dt, string sql, OracleParameter[] OracleParams, string connName)
        {
            QueryDt(dt, sql, OracleParams, connName, 30);
        }
        #endregion

        #region QueryDr  ִ�в�ѯ���������� OracleDataReader ����
        /// <summary>
        /// ִ�в�ѯ���������� OracleDataReader ����
        /// </summary>           
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        /// <returns>OracleDataReader ����</returns>
        public static OracleDataReader QueryDr(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleDataReader dr = null;
            try
            {
                //ȡ�����ݿ�����
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
        /// ִ�в�ѯ���������� OracleDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>OracleDataReader ����</returns>
        public static OracleDataReader QueryDr(string sql)
        {
            return QueryDr(sql, null, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� OracleDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>OracleDataReader ����</returns>
        public static OracleDataReader QueryDr(string sql, string connName)
        {
            return QueryDr(sql, null, connName, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� OracleDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <returns>OracleDataReader ����</returns>
        public static OracleDataReader QueryDr(string sql, OracleParameter[] OracleParams)
        {
            return QueryDr(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� OracleDataReader ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>OracleDataReader ����</returns>
        public static OracleDataReader QueryDr(string sql, OracleParameter[] OracleParams, string connName)
        {
            return QueryDr(sql, OracleParams, connName, 30);
        }

        #endregion

        #region QueryObj  ִ�в�ѯ���������� Object ���� (��ֵ)
        /// <summary>
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>           
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>  
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            Object obj = null;

            try
            {
                //ȡ�����ݿ�����
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
        /// <param name="OracleParams">��������</param>
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, OracleParameter[] OracleParams)
        {
            return QueryObj(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ�в�ѯ���������� Object ���� (��ֵ)
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>Object ����</returns>
        public static Object QueryObj(string sql, OracleParameter[] OracleParams, string connName)
        {
            return QueryObj(sql, OracleParams, connName, 30);
        }

        #endregion

        #endregion

        #region Insert,Update,Delete ���ִ��

        #region Execute  ִ��SQL��䣬������Ӱ������� (������)
        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            int intResult = 0;

            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();

            try
            {
                //ȡ�����ݿ�����
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
        /// <param name="OracleParams">��������</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, OracleParameter[] OracleParams)
        {
            return Execute(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>��Ӱ�������</returns>
        public static int Execute(string sql, OracleParameter[] OracleParams, string connName)
        {
            return Execute(sql, OracleParams, connName, 30);
        }

        #endregion

        #region ExecuteTran  ִ��SQL��䣬������Ӱ������� (������)
        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <param name="timeOut">�ȴ�����ִ�е�ʱ�䣨����Ϊ��λ��,Ĭ��ֵΪ 30 �롣</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, OracleParameter[] OracleParams, string connName, int timeOut)
        {
            int intResult = 0;

            OracleConnection conn = null;
            OracleCommand cmd = new OracleCommand();
            OracleTransaction sqlTran = null;

            try
            {
                //ȡ�����ݿ�����
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
        /// <param name="OracleParams">��������</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, OracleParameter[] OracleParams)
        {
            return ExecuteTran(sql, OracleParams, string.Empty, 30);
        }

        /// <summary>
        /// ִ��SQL��䣬������Ӱ���������
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <param name="OracleParams">��������</param>
        /// <param name="connName">ָ��ʹ�õ����ݿ���������</param>
        /// <returns>��Ӱ�������</returns>
        public static int ExecuteTran(string sql, OracleParameter[] OracleParams, string connName)
        {
            return ExecuteTran(sql, OracleParams, connName, 30);
        }

        #endregion

        #endregion
    }
}
