using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using ToolLib.data;

namespace ToolLib.util
{
    public class CacheUtil
    {

        /// <summary>
        /// ���û�������
        /// </summary>
        /// <param name="sql">��ѯ���</param>
        /// <param name="name">���������</param>
        /// <param name="minutes">����ʱ�䣬��λΪ����</param>
        /// <returns></returns>
        public static DataTable GetCache(string sql, string name,int minutes)
        {
            DataTable dt = (DataTable)HttpRuntime.Cache[name];
            if (dt == null)
            {
                try { dt = DbModel.Query(sql); }catch{}
                HttpRuntime.Cache.Insert(name, dt, null, DateTime.Now.AddMinutes(minutes), TimeSpan.Zero);

            }
            return dt;
        }

        public static DataTable GetCache(string sql, string name)
        {
            return GetCache(sql,name,3);
        }

        /// <summary>
        /// ���û�������
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="boolAbsolute">�Ƿ���Ե���</param>
        /// <param name="intMinutes">����ʱ�䣬��λΪ����</param>
        /// <returns>���������Ƿ�ɹ�</returns>
        public static bool SetCacheValue(string strKey, object objValue,bool boolAbsolute,int intMinutes)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            if (boolAbsolute)//���Ե���
            {
                HttpRuntime.Cache.Insert(strKey, objValue, null, DateTime.Now.AddMinutes(intMinutes), TimeSpan.Zero);
            }
            else //��������
            {
                HttpRuntime.Cache.Insert(strKey, objValue, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(intMinutes));
            }

            return true;
        }

        public static bool SetCacheValue(string strKey, object objValue)
        { 
            return SetCacheValue(strKey, objValue,true, 5);
        }

        /// <summary>
        /// ȡ�û�������
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>Value</returns>
        public static object GetCacheValue(string strKey)
        {
            return HttpRuntime.Cache[strKey];
        }
       
    }
}
