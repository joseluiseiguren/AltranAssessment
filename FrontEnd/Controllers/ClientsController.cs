﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        public ActionResult Search()
        {
            return View();
        }
    }
}