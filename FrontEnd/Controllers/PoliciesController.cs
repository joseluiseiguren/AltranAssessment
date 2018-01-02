using FrontEnd.Dto;
using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class PoliciesController : Controller
    {
        public ActionResult GetByClient(string id)
        {
            ClientDTO client = new ClientDTO() { Id = id };
            return View(client);
        }
    }
}