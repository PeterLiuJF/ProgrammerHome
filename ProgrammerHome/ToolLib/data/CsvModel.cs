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
        /// ��ȡ���ݿ�����
        /// �� Excel 2007 �д��ļ�ʱ, �����յ������ļ���ʽ�벻ͬ���ļ���չ��ָ����ʽ
        //        /// �� Windows XP �е��� ��ʼ�� ���� ���С� ���� regedit��Ȼ�󵥻� ȷ����
        //�ҵ�����������ע������
        //HKEY_CURRENT_USER\Software\Microsoft\Office\12.0\Excel\Security
        //�� �༭ �˵���ָ�� �½���Ȼ�󵥻� DWORD ֵ��
        //���� ExtensionHardening��Ȼ�� ENTER ����
        //������Ҽ����� ExtensionHardening��Ȼ�󵥻� �޸ġ�
        //�� ��ֵ���� ���м���ֵ�����ݣ�Ȼ�󵥻� ȷ����

        //������б���������� ExtensionHardening ���õ�ֵ���������ã�
        //0�� ������ļ���չ�����ļ����Ͳ��ƹ��ú����ľ�����Ϣ��
        //1�� ����ļ���չ�����ļ����͡�������ǲ�ƥ�����ʾ������Ϣ��
        //2�� ����ļ���չ�����ļ����͡�������ǲ�ƥ�䲻Ҫ�򿪸��ļ���
        //ע��Ĭ��ֵ����Ϊ 1��������ֵ��������Ϊ 1 ʱ����Ϊ���û��ע���ֵ����ʱ��ͬ������ֵ��������Ϊ 0���ļ���չ�����ļ����ݲ��������������¡� ���ǲ������ƹ��˹��ܡ�
        //textָ�����ı��ļ�
        //HDR=Yes����˼�ļ��д��б�����
        //FMT=Delimited����˼���Զ���ָ��� 
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
                // MessageBox.Show( e.Message, "ϵͳ�����������Ա��ϵ", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                throw new DatabaseException(e.Message);
            }
            return conn;
        }


        /// <summary>
        /// ��ѯ
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
        /// ��ѯ
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
