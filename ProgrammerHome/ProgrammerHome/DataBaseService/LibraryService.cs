using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ToolLib.DbAccess;
using ToolLib.util;

namespace ProgrammerHome.DataBaseService
{
    public class LibraryService
    {
        public List<LibraryTypeModel> GetGameTypeItems()
        {

            string sql = @"select * from noveltype where level=1";
            List<LibraryTypeModel> list = new List<LibraryTypeModel>();
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LibraryTypeModel typeModel = null;
                typeModel = new LibraryTypeModel()
                {
                    ID = Normal.ParseInt(dt.Rows[i]["ID"]),
                    Name = Normal.ListStr(dt.Rows[i]["Name"])
                };
                list.Add(typeModel);
            }
            return list;
        }
    }
}