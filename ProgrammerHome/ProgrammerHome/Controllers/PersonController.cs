using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolLib.util;

namespace ProgrammerHome.Controllers
{
    public class PersonController : BaseController
    {
        PersonService personService = new PersonService();
        //
        // GET: /Person/
        #region 显示个人详细信息
        public ActionResult ShowInfo()
        {
            return View(personService.GetUserInfo(LoginID));
        }
        #endregion

        #region 修改个人信息
        public ActionResult UpdateUserInfo()
        {
            return View(personService.GetUserInfo(LoginID));
        }
        [HttpPost]
        public ActionResult DoUpdateUserInfo()
        {
            var flag = false;
            try
            {
                var model = new UserInfoModel()
                {
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
                flag = personService.UpdateUserInfo(model);
                return flag ? Content("注册成功！") : Content("注册失败！");
            }
            catch (Exception)
            {
                return Content("注册失败！");
            }

        }
        #endregion

        #region 图片中心
        public ActionResult ImageSpace()
        {
            return View(personService.GetImages(LoginID));
        }

        [HttpPost]
        public ActionResult UploadImages()
        {
            List<ImageModel> list = new List<ImageModel>();
            var files = Request.Files;
            var path = HttpContext.Server.MapPath(@"..\images");
            for (int i = 0; i < files.Count; i++)
            {
                var filename = DateTime.Now.ToString("yyyyMMddHHmmss") + files[i].FileName;
                files[i].SaveAs(Path.Combine(path, filename));
                list.Add(new ImageModel()
                {
                    PicPath = @"..\images\"+filename,
                    UserID = LoginID
                });
            }
            personService.AddImages(list);
            return Content("<script>window.open('../Person/ImageSpace?MySpace=1', 'newwindow', 'height=800, width=1000, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')</script>");
        }
        #endregion
    }
}
