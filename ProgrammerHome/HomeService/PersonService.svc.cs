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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“PersonService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 PersonService.svc 或 PersonService.svc.cs，然后开始调试。
    public class PersonService : IPersonService
    {
        public UserInfoModel GetUserInfo(int id)
        {
            string sql = string.Format("select * from UserInfo where id={0}",id);
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            var model = new UserInfoModel()
            {
                ID = id,
                Name = Normal.ListStr(dt.Rows[0]["Name"]),
                Account = Normal.ListStr(dt.Rows[0]["Account"]),
                BirthDay = Normal.ParseDateTime(dt.Rows[0]["BirthDay"]).ToString("yyyy年MM月dd日"),
                Sex = Normal.ListStr(dt.Rows[0]["Sex"]),
                Company = Normal.ListStr(dt.Rows[0]["Company"]),
                HomeTown = Normal.ListStr(dt.Rows[0]["HomeTown"]),
                InterestIn = Normal.ListStr(dt.Rows[0]["InterestIn"]),
                IntroduceMyself = Normal.ListStr(dt.Rows[0]["IntroduceMyself"]),
                Marriage = Normal.ListStr(dt.Rows[0]["Marriage"]),
                Position = Normal.ListStr(dt.Rows[0]["Position"]),
                QQ = Normal.ListStr(dt.Rows[0]["QQ"]),
                WorkingConditions = Normal.ParseInt(dt.Rows[0]["WorkingConditions"]),
                EntryDate = Normal.ListStr(dt.Rows[0]["EntryDate"])
            };

            return model;
        }
    }
}
