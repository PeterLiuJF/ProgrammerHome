using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolLib.util;

namespace ProgrammerHome.Controllers
{
    public class RecreationController : Controller
    {
        //
        // GET: /Recreation/
        RecreationService reService = new RecreationService();

        #region Music
        public ActionResult RecreationMain()
        {
            var model = reService.GetPlayListItems();
            return View(model);
        }

        [HttpPost]
        public JsonResult LoadMusic(string type)
        {
            return Json(reService.GetMusicItems(Normal.ParseInt(type)));
        }
        #endregion

        #region Game
        public ActionResult GameMain()
        {
            var list = reService.GetGameTypeItems();
            return View(list);
        }
        #endregion
    }
}
