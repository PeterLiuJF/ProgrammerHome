using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using ToolLib.exception;

namespace ToolLib.data
{
    public abstract class CsvModel
    {
        /// <summary>
        /// 获取数据库连接
        /// 在 Excel 2007 中打开文件时, 您将收到警告文件格式与不同的文件扩展名指定格式
        //        /// 在 Windows XP 中单击 开始、 单击 运行、 键入 regedit，然后单击 确定。
        //找到并单击以下注册表子项：
        //HKEY_CURRENT_USER\Software\Microsoft\Office\12.0\Excel\Security
        //在 编辑 菜单上指向 新建，然后单击 DWORD 值。
        //键入 ExtensionHardening，然后按 ENTER 键。
        //用鼠标右键单击 ExtensionHardening，然后单击 修改。
        //在 数值数据 框中键入值的数据，然后单击 确定。

        //下面的列表包含适用于 ExtensionHardening 设置的值的数据设置：
        //0： 不检查文件扩展名和文件类型并绕过该函数的警告消息。
        //1： 检查文件扩展名和文件类型。如果它们不匹配会显示警告消息。
        //2： 检查文件扩展名和文件类型。如果它们不匹配不要打开该文件。
        //注意默认值数据为 1。当该数值数据设置为 1 时，行为变得没有注册表值设置时相同。该数值数据设置为 0，文件扩展名和文件内容不检查在所有情况下。 我们不建议绕过此功能。
        //text指的是文本文件
        //HDR=Yes的意思文件中带有标题行
        //FMT=Delimited的意思是自定义分隔符 
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection GetConnection(string path)
        {

            OleDbConnection conn = null;
            string connstr = "";
            try
            {

                connstr = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path + @";Extended Properties=""Text;HDR=YES;FMT=Delimited"";Persist Security Info=False;";

                conn = new OleDbConnection(connstr);
            }
            catch (Exception e)
            {
                // MessageBox.Show( e.Message, "系统错误，请与管理员联系", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                throw new DatabaseException(e.Message);
            }
            return conn;
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable Query(string path, string tablename, string cond)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OleDbConnection conn = null;
            string sql = "select * from [" + tablename + "]";
            if (!string.IsNullOrEmpty(cond))
            {
                sql += " where " + cond;
            }
            try
            {
                conn = GetConnection(path);

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, conn);
                conn.Open();
                sda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw new DAOException(e.Message);
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
        public static DataTable Query(string path, string sql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            OleDbConnection conn = null;
            try
            {
                conn = GetConnection(path);

                OleDbDataAdapter sda = new OleDbDataAdapter(sql, conn);
                conn.Open();
                sda.Fill(dt);
                dt = ds.Tables[0];

            }
            catch (Exception e)
            {
                throw new DAOException(e.Message);
            }
            finally
            {
                try { conn.Close(); }
                catch { }
            }
            return dt;
        }

    }
}
