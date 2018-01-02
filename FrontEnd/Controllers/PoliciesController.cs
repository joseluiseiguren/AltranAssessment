using FrontEnd.Dto;
using FrontEnd.Infraestructure;
using FrontEnd.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class PoliciesController : Controller
    {
        private IClientsRepository clientsRepository;

        public PoliciesController()
        {
            this.clientsRepository = MvcApplication.GetDR().Get<IClientsRepository>();
        }

        public PoliciesController(IClientsRepository clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }

        public ActionResult GetByClient(string id)
        {
            var client = this.clientsRepository.GetClients().Where(p => p.Id.Equals(id)).FirstOrDefault();

            return View(client.ToDto());
        }
    }
}