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
        /// ��ȡ���ݿ�����
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
                //MessageBox.Show("���ݿ�����ʧ��", "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException(e.Message);
            }
            return conn;
        }



        /// <summary>
        /// ��ȡ���ݿ�����
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
                //MessageBox.Show( "���ݿ�����ʧ��", "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                throw new DatabaseException(e.Message);
            }
            return conn;
        }

        /// <summary>
        /// ��ѯ
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
                //  MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);// + "\n" + e.Message

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
        /// ��ѯ
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
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
        /// ����
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ��ѯ������id
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
        /// ���ô洢���̲�ѯ������id
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
        /// ����
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ִ��UPDATE��INSERT �� DELETE
        /// </summary>
        /// <param name="sql">UPDATE��INSERT �� DELETE���</param>
        /// <param name="connstr">���ݿ������ַ���</param>
        /// <param name="timeOut">��ʱʱ��</param>
        /// <returns>��Ӱ�������</returns>
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ִ�д洢����(û�з���ֵ)
        /// </summary>
        /// <param name="procedureName">�洢������</param>
        /// <param name="connstr">���ݿ�����</param>
        /// <param name="parameters">�洢���̲�������</param>
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException("�洢����ִ�г���\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        /// <summary>
        /// ִ�д洢����(û�з���ֵ)
        /// </summary>
        /// <param name="procedureName">�洢������</param>
        /// <param name="connstr">���ݿ�����</param>
        /// <param name="parameters">�洢���̲�������</param>
        /// <param name="intTimeOut">�洢����ִ�еĳ�ʱʱ��(��)</param>
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new DatabaseException("�洢����ִ�г���\n" + e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
        }

        /// <summary>
        /// ���벢ȡ�ò����¼����������
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="connstr">����</param>
        /// <param name="identityKey">����������</param>
        /// <returns>��Ӱ�������</returns>
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ����
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
                //MessageBox.Show( sql, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
