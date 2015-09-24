using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolLib.util;

namespace ProgrammerHome.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Master = CommonService.GetMaster(Normal.ParseInt(Request["MySpace"]));
            return View();
        }

        #region 登录&&登出
        [HttpPost]
        public ActionResult Login()
        {
            PersonService service = new PersonService();
            var account = Normal.ListStr(Request["Account"]);
            var password = Normal.CheckPoint(PwdUtil.MD5Encrypt(Request["Password"]));
            var id = service.Login(account, password);
            if (id > 0)
            {
                var cookie = new HttpCookie("PH_User");
                cookie.Expires = DateTime.Now.AddMonths(1);
                cookie.Values.Add("User_Account", account);
                cookie.Values.Add("User_Id", id.ToString());
                Response.AppendCookie(cookie);
                return Content("True");
            }
            return Content("False");
        }

        [HttpPost]
        public ActionResult Logon()
        {
            try
            {
                var cookie = Request.Cookies["PH_User"];
                cookie.Expires = DateTime.Now.AddMonths(-1);
                Response.Cookies.Add(cookie);
                return Content("Index");
            }
            catch (Exception)
            {
                return Content("<script>location.href='../Home/Index'</script>");
            }
            
        }
        #endregion

        #region 注册
        public ActionResult UserRegistered()
        {
            ViewBag.Master = CommonService.GetMaster(Normal.ParseInt(Request["MySpace"]));
            return View(new UserInfoModel() { BirthDay=DateTime.Now.ToString("yyyy-MM-dd")});
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
                    Password = Normal.CheckPoint(PwdUtil.MD5Encrypt(Request["Password"])),
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
                var personService = new PersonService();
                flag = personService.AddUserInfo(model);
                return flag ? Content("修改成功！") : Content("修改失败！");
            }
            catch (Exception)
            {
                return Content("修改失败！");
            }

        }
        #endregion
    }
}
