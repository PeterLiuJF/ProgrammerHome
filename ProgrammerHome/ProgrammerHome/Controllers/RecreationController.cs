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
    public class RecreationController : BaseController
    {
        //
        // GET: /Recreation/
        RecreationService reService = new RecreationService();

        #region Music
        public ActionResult RecreationMain()
        {
            var model = reService.GetPlayListItems(LoginID);
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadMusic(string type)
        {
            return Json(reService.GetMusicItems(Normal.ParseInt(type), LoginID));
        }

        public ActionResult AddMusic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadMusic()
        {
            List<PlayDetailModel> list = new List<PlayDetailModel>();
            var files = Request.Files;
            var path = HttpContext.Server.MapPath(@"..\Music");
            for (int i = 0; i < files.Count; i++)
            {
                var filename = files[i].FileName;
                files[i].SaveAs(Path.Combine(path, filename));
                var model = new PlayDetailModel()
                {
                    mp3 = @"..\Music\" + filename,
                };
                var title = filename.Split('-');
                if (title.Length > 1)
                {
                    model.title = title[1];
                    model.artist = title[0];
                }
                else
                {
                    model.title = filename;
                    model.artist = "未知";
                }
                list.Add(model);
            }
            reService.AddMusic(list,LoginID);
            return Content(ReloadPage("../Recreation/RecreationMain"));
        }
        #endregion

        #region Game
        public ActionResult GameMain()
        {
            var list = reService.GetGameTypeItems(LoginID);
            return View(list);
        }
        #endregion
    }
}
