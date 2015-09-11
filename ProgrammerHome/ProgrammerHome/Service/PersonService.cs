using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToolLib.DbAccess;
using ToolLib.util;
using System.Data;

namespace ProgrammerHome.Service
{
    public class PersonService
    {
        public UserInfoModel GetUserInfo(int id)
        {
            string sql = string.Format("select * from UserInfo where id={0}", id);
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