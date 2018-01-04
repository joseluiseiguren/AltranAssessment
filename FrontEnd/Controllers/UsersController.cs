namespace FrontEnd.Controllers
{
    using FrontEnd.Interfaces;
    using FrontEnd.Models;
    using System.Web.Mvc;
    using System.Web.Security;
    using Ninject;
    using System.Linq;

    /// <summary>
    /// controler de usuarios
    /// </summary>
    public class UsersController : Controller
    {
        private IClientsRepository clientsRepository;
        private IAuth auth;

        /// <summary>
        /// constructor sin parametros para el motor mvc
        /// </summary>
        public UsersController()
        {
            this.clientsRepository = MvcApplication.GetDR().Get<IClientsRepository>();
            this.auth = MvcApplication.GetDR().Get<IAuth>();
        }

        /// <summary>
        /// constructor con parametros para los testing unitarios
        /// </summary>
        public UsersController(IClientsRepository clientsRepository, IAuth auth)
        {
            this.clientsRepository = clientsRepository;
            this.auth = auth;
        }

        /// <summary>
        /// pagina de login
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// post de login
        /// </summary>
        [HttpPost]
        public ActionResult Login(UserLoginVm user)
        {
            if (ModelState.IsValid)
            {
                //Se valida que exista el usuario
                var userFind = this.clientsRepository.GetClients().Where(p => p.Email.ToUpper().Trim().Equals(user.Email.ToUpper().Trim())).FirstOrDefault();
                if (userFind != null)
                {
                    this.auth.SetAuthCookie(userFind.Name, false);
                    return RedirectToAction("Search", "Clients");
                }

                ModelState.AddModelError(nameof(user.Email), "Invalid Email");
            }

            return View(user);
        }

        /// <summary>
        /// pagina de logout
        /// </summary>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Users");
        }
    }
}