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
        //通过主键ID得到用户信息
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
                TheLatter = Normal.ListStr(dt.Rows[0]["TheLatter"]),
                InterestIn = Normal.ListStr(dt.Rows[0]["InterestIn"]),
                IntroduceMyself = Normal.ListStr(dt.Rows[0]["IntroduceMyself"]),
                Marriage = Normal.ListStr(dt.Rows[0]["Marriage"]),
                Position = Normal.ListStr(dt.Rows[0]["Position"]),
                Email = Normal.ListStr(dt.Rows[0]["Email"]),
                QQ = Normal.ListStr(dt.Rows[0]["QQ"]),
                WorkingConditions = Normal.ParseInt(dt.Rows[0]["WorkingConditions"]),
                EntryDate = Normal.ListStr(dt.Rows[0]["EntryDate"])
            };

            return model;
        }

        public bool AddUserInfo(UserInfoModel model)
        {
            string sql = "";
            if (string.IsNullOrEmpty(model.BirthDay))
            {
                sql = string.Format(@"insert into UserInfo(Account,[Password],[Email],Name,Sex,
            TheLatter,HomeTown,Marriage,Position,Company,InterestIn,IntroduceMyself,QQ,WorkingConditions) 
            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
                '{9}','{10}','{11}','{12}',{13})",
                    model.Account, model.Password, model.Email, model.Name, model.Sex,
                    model.TheLatter, model.HomeTown, model.Marriage,
                    model.Position, model.Company, model.InterestIn, model.IntroduceMyself,
                    model.QQ, model.WorkingConditions);
                
            }
            else
            {
                sql = string.Format(@"insert into UserInfo(Account,[Password],[Email],Name,Sex,
            TheLatter,HomeTown,Marriage,Position,Company,InterestIn,IntroduceMyself,QQ,WorkingConditions,BirthDay) 
            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',
                '{9}','{10}','{11}','{12}',{13},'{14}')",
                    model.Account, model.Password, model.Email, model.Name, model.Sex,
                    model.TheLatter, model.HomeTown, model.Marriage,
                    model.Position, model.Company, model.InterestIn, model.IntroduceMyself,
                    model.QQ, model.WorkingConditions, model.BirthDay);
            }
            return SqlAccess.Execute(sql) > 0;
        }

        public int Login(string account,string password)
        {
            string sql = string.Format("select id from UserInfo where Account='{0}' and [password]='{1}'", account, password);
            return Normal.ParseInt(SqlAccess.QueryObj(sql));
        }
    }
}