using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgrammerHome.DataBaseService;

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
            return View();
        }

        [HttpPost]
        public JsonResult LoadMusic()
        {
            return Json(reService.GetMusicItems());
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
