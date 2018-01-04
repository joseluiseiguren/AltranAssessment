namespace FrontEnd.Controllers
{
    using FrontEnd.Infraestructure;
    using FrontEnd.Interfaces;
    using Ninject;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// controler de policies
    /// </summary>
    [Authorize]
    public class PoliciesController : Controller
    {
        private IClientsRepository clientsRepository;

        /// <summary>
        /// constructor sin parametros para el motor mvc
        /// </summary>
        public PoliciesController()
        {
            this.clientsRepository = MvcApplication.GetDR().Get<IClientsRepository>();
        }

        /// <summary>
        /// constructor con parametros para los testing unitarios
        /// </summary>
        public PoliciesController(IClientsRepository clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }

        /// <summary>
        /// busqueda de clientes por id
        /// </summary>
        public ActionResult GetByClient(string id)
        {
            var client = this.clientsRepository.GetClients().Where(p => p.Id.Equals(id)).FirstOrDefault();

            return View(client.ToDto());
        }
    }
}