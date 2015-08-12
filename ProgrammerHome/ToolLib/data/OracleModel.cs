using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using ToolLib.exception;
using ToolLib.util;
using ToolLib.log;


namespace ToolLib.data
{
    public class OracleModel
    {

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static OracleConnection GetConnection(string key)
        {
            OracleConnection conn = null;
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
                conn = new OracleConnection(ConnectString);
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
        public static DataTable Query(string key,string sql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OracleConnection conn = null;
            try
            {
                conn = GetConnection(key);
                OracleDataAdapter sda = new OracleDataAdapter(sql, conn);
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
        /// <param name="key">数据库连接</param>
        /// <param name="sql"></param>
        /// <param name="pas">参数</param>
        /// <returns></returns>
        public static DataTable Query(string key, string sql, params OracleParameter[] pas)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OracleConnection conn = null;
            OracleCommand cmd = null;

            try
            {
                conn = GetConnection(key);
                cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;

                OracleParameter[] clonedParameters = new OracleParameter[pas.Length];
                //这将执行一个深拷贝（deep copy）不会出现问题 不然连续调用两次的时候就会报错！
                for (int i = 0, j = pas.Length; i < j; i++)
                {
                    clonedParameters[i] = (OracleParameter)((ICloneable)pas[i]).Clone();
                }

                foreach (OracleParameter temppa in clonedParameters)
                {
                    cmd.Parameters.Add(temppa);
                }
                da.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw new DAOException(sql + "\n" + e.Message);
            }
            finally
            {
                try { cmd.Dispose(); conn.Close(); conn.Dispose(); }
                catch { }
            }
            return dt;
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        public static int Execute(string key,string sql)
        {
            int n = -1;
            OracleConnection conn = null;
            try
            {
                conn = GetConnection(key);
                OracleCommand cmd = new OracleCommand(sql, conn);
                conn.Open();
                n=cmd.ExecuteNonQuery();
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
        public static string Execute(string key, string sql, params OracleParameter[] pas)
        {
            string n;
            
            OracleConnection conn = null;
            OracleCommand cmd = null;

            try
            {
                conn = GetConnection(key);
                cmd = new OracleCommand();                
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
               

                OracleParameter[] clonedParameters = new OracleParameter[pas.Length];
                //这将执行一个深拷贝（deep copy）不会出现问题 不然连续调用两次的时候就会报错！
                for (int i = 0, j = pas.Length; i < j; i++)
                {
                    clonedParameters[i] = (OracleParameter)((ICloneable)pas[i]).Clone();
                }

                foreach (OracleParameter temppa in clonedParameters)
                {
                    cmd.Parameters.Add(temppa);
                }
                cmd.ExecuteNonQuery();
                n = cmd.Parameters[pas.Length - 1].Value.ToString();


            }
            catch (Exception e)
            {
                throw new DAOException(sql + "\n" + e.Message);
            }
            finally
            {
                try { cmd.Dispose(); conn.Close(); conn.Dispose(); }
                catch { }
            }
            return n;
        }

        public static DataTable Query(string sql)
        {
            return OracleModel.Query(DbKey.ORA, sql);
        }
        public static DataTable Query(string sql, params OracleParameter[] pas)
        {
            return OracleModel.Query(DbKey.ORA, sql, pas);
        }
        public static int Execute(string sql)
        {
           return  OracleModel.Execute(DbKey.ORA, sql);
        }
        public static string Execute(string sql, params OracleParameter[] pas)
        {
            return OracleModel.Execute(DbKey.ORA, sql, pas);
        }
       
    }
}
