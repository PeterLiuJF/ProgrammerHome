using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return Content("");
        }
    }
}
