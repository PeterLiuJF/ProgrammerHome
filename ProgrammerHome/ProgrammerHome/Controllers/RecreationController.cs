using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
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
