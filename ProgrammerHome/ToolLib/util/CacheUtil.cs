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
        /// 设置缓存数据
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="name">缓存的名称</param>
        /// <param name="minutes">缓冲时间，单位为分钟</param>
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
        /// 设置缓存数据
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <param name="boolAbsolute">是否绝对到期</param>
        /// <param name="intMinutes">缓存时间，单位为分钟</param>
        /// <returns>缓存数据是否成功</returns>
        public static bool SetCacheValue(string strKey, object objValue,bool boolAbsolute,int intMinutes)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            if (boolAbsolute)//绝对到期
            {
                HttpRuntime.Cache.Insert(strKey, objValue, null, DateTime.Now.AddMinutes(intMinutes), TimeSpan.Zero);
            }
            else //滑动到期
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
        /// 取得缓存数据
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>Value</returns>
        public static object GetCacheValue(string strKey)
        {
            return HttpRuntime.Cache[strKey];
        }
       
    }
}
