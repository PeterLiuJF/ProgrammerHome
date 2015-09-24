using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolLib.util;

namespace ProgrammerHome.Controllers
{
    public class BaseController : Controller
    {
        private string _loginName = "";
        private int _loginID;
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }
        public int LoginID
        {
            get { return _loginID; }
            set { _loginID = value; }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var myspace = Normal.ParseInt(Request["MySpace"]);
            ViewBag.Master = CommonService.GetMaster(myspace);
            if (myspace==1)
            {
                if (Request.Cookies["PH_User"] == null)
                {
                    Response.Write("<script>window.top.location.href='" + Url.Action("../Home/Index") + "';</script>");//跳转页面
                    Response.End();
                }
                _loginID = Normal.ParseInt(Request.Cookies["PH_User"].Values["User_Id"]);
                _loginName = Request.Cookies["PH_User"].Values["User_Account"];
            }
        }
    }
}
