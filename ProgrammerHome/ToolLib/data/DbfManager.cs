using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using ToolLib.exception;

namespace ToolLib.data
{
    public class DbfManager
    {
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection GetConnection(string filename, bool hdr)
        {
            string filepath = "";
            string hdrstr = "YES";
            if (!hdr) hdrstr = "NO";

            OleDbConnection conn = null;
            string connstr = "";
            try
            {
                filepath = System.IO.Directory.GetParent(filename).FullName;
                //connstr = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + filepath + @";Extended Properties=""dBASE IV;"";";
                connstr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + @";Extended Properties=dBASE IV;";

                conn = new OleDbConnection(connstr);
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.Message);
            }
            return conn;
        }
        public static OleDbConnection GetConnection(string filename)
        {
            return GetConnection(filename, true);
        }

        //创建、执行
        public static int Execute(string filename, string sql)
        {
            int n = 0;
            OleDbConnection conn = null;
            try
            {
                conn = GetConnection(filename);


                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
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
                conn = null;
            }
            return n;
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="dt"></param>
        public static void InsertAll(string filename, string tablename, DataTable dt)
        {
            OleDbConnection conn = null;
            try
            {
                conn = GetConnection(filename);
                conn.Open();

                string sheetname = Path.GetFileName(filename);
                string sheetname1 = Path.GetFileNameWithoutExtension(filename);

                string sql = "select * from [" + tablename + "]";
                if (tablename.IndexOf("select") >= 0)
                    sql = tablename;

                OleDbDataAdapter Dad = new OleDbDataAdapter(sql, conn);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(Dad);
                if (Dad != null)
                {
                    DataSet ds = new DataSet();
                    Dad.Fill(ds, sheetname1);
                    DataTable dt1 = ds.Tables[sheetname1];
                    dt1.Merge(dt, true, MissingSchemaAction.Ignore);
                    Dad.Update(dt1);
                }
                // sql = sheetname;
            }
            catch (Exception e)
            {
                throw new DAOException("批量更新失败" + "\n" + e.ToString());
            }
            finally
            {
                try { conn.Close(); }
                catch { }
                conn = null;
            }
        }
    }
}
