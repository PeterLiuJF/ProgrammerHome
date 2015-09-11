using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToolLib.DbAccess;
using ToolLib.util;
using System.Data;

namespace ProgrammerHome.Service
{
    public class LibraryService
    {
        public List<LibraryTypeModel> GetGameTypeItems(int level, int parentLevel = 0)
        {
            string sql = string.Format(@"select * from noveltype where [level]={0}", level);
            if (parentLevel != 0)
            {
                sql += " and parentLevel=" + parentLevel;
            }
            List<LibraryTypeModel> list = new List<LibraryTypeModel>();
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LibraryTypeModel typeModel = null;
                typeModel = new LibraryTypeModel()
                {
                    ID = Normal.ParseInt(dt.Rows[i]["ID"]),
                    Name = Normal.ListStr(dt.Rows[i]["Name"]),
                    Level = Normal.ParseInt(dt.Rows[i]["Level"]),
                    ParentLevel = Normal.ParseInt(dt.Rows[i]["ParentLevel"])
                };
                list.Add(typeModel);
            }
            return list;
        }
    }
}