using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ToolLib.DbAccess;
using ToolLib.util;

namespace HomeService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“LibraryService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 LibraryService.svc 或 LibraryService.svc.cs，然后开始调试。
    public class LibraryService : ILibraryService
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
