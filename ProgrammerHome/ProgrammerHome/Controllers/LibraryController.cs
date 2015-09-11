using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToolLib.util;

namespace ProgrammerHome.Controllers
{
    public class LibraryController : Controller
    {
        LibraryService libService = new LibraryService();
        //
        // GET: /Library/

        public ActionResult Library()
        {
            var list = libService.GetGameTypeItems(1,0);
            return View(list);
        }

        [HttpPost]
        public ActionResult Library2(string parentLevel)
        {
            var list = libService.GetGameTypeItems(2, Normal.ParseInt(parentLevel));
            return Json(list);
        }
    }
}
