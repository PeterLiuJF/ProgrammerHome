﻿using ProgrammerHome.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammerHome.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            PersonService service=new PersonService();
            service.Login("", "");
            return Content("true");
        }
    }
}
