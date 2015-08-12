using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ToolLib.exception;
using ToolLib.util;
using ToolLib.log;

namespace ToolLib.data
{
    public abstract class SqlModel
    {

        /// <summary>
        /// ��ȡ���ݿ�����
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection(string key)
        {
            SqlConnection conn = null;

            string isEncrypt = Normal.ParseString(ConfigurationManager.AppSettings["Encrypt"]);
            isEncrypt = isEncrypt.ToLower();
            if (!isEncrypt.Equals("false")) isEncrypt = "true";
            string ConnectString = "";
            if (isEncrypt.Equals("true"))
                ConnectString = PwdUtil.DesDecrypt(ConfigurationManager.ConnectionStrings[key].ConnectionString);
            else
                ConnectString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            try
            {
                conn = new SqlConnection(ConnectString);
            }
            catch (Exception e)
            {
                throw new DatabaseException(ConnectString + "\n" + e.Message);
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
        public static DataTable Query(string key, string sql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            SqlConnection conn = null;
            try
            {
                conn = GetConnection(key);
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.SelectCommand.CommandTimeout = 90;//���ó�ʱʱ��,Ĭ��30�룬IIS��90�룬���Դ˴����90�룬�Ա���һ��


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
        /// ����
        /// </summary>
        /// <param name="sql"></param>
        public static int Execute(string key, string sql)
        {
            int n = -1;
            SqlConnection conn = null;
            try
            {
                conn = GetConnection(key);
                SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.CommandTimeout = 90;//���ó�ʱʱ��,Ĭ��30�룬IIS��90�룬���Դ˴����90�룬�Ա���һ��
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

        public static DataTable Query(string sql)
        {
            return SqlModel.Query(DbKey.SQL, sql);
        }

        public static int Execute(string sql)
        {
            return SqlModel.Execute(DbKey.SQL, sql);
        }
    }
}
