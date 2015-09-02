using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgrammerHome.DataBaseService;

namespace ProgrammerHome.Controllers
{
    public class LibraryController : Controller
    {
        LibraryService libService = new LibraryService();
        //
        // GET: /Library/

        public ActionResult Library()
        {
            var list = libService.GetGameTypeItems();
            return View(list);
        }

    }
}
