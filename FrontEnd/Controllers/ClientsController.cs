namespace FrontEnd.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// controler de clientes
    /// </summary>
    [Authorize]
    public class ClientsController : Controller
    {
        /// <summary>
        /// busqueda de clientes
        /// </summary>
        public ActionResult Search()
        {
            return View();
        }
    }
}