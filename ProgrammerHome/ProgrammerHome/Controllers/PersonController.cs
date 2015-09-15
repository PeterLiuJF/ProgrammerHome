using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolLib.util;

namespace ProgrammerHome.Controllers
{
    public class PersonController : Controller
    {
        PersonService personService = new PersonService();
        //
        // GET: /Person/

        public ActionResult ShowInfo()
        {
            return View(personService.GetUserInfo(1));
        }

        public ActionResult UserRegistered()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser()
        {
            var flag = false;
            try
            {
                var model = new UserInfoModel()
                            {
                                Account = Normal.ListStr(Request["Account"]),
                                Password = Normal.CheckPoint(PwdUtil.MD5Encrypt(Normal.ListStr(Request["Password"]))),
                                Email = Normal.ListStr(Request["Email"]),
                                Name = Normal.ListStr(Request["Name"]),
                                Sex = Normal.ListStr(Request["Sex"]),
                                BirthDay = Normal.ListStr(Request["BirthDay"]),
                                HomeTown = Normal.ListStr(Request["HomeTown"]),
                                Marriage = Normal.ListStr(Request["Marriage"]),
                                TheLatter = Normal.ListStr(Request["TheLatter"]),
                                Position = Normal.ListStr(Request["Position"]),
                                Company = Normal.ListStr(Request["Company"]),
                                WorkingConditions = Normal.ParseInt(Request["WorkingConditions"]),
                                InterestIn = Normal.ListStr(Request["InterestIn"]),
                                QQ = Normal.ListStr(Request["QQ"]),
                                IntroduceMyself = Normal.ListStr(Request["IntroduceMyself"])
                            };
                flag = personService.AddUserInfo(model);
                return flag ? Content("注册成功！") : Content("注册失败！");
            }
            catch (Exception)
            {
                return Content("注册失败！");
            }
            
        }
    }
}
