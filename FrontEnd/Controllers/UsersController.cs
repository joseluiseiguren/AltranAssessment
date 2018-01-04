namespace FrontEnd.Controllers
{
    using FrontEnd.Interfaces;
    using FrontEnd.Models;
    using System.Web.Mvc;
    using System.Web.Security;
    using Ninject;
    using System.Linq;

    public class UsersController : Controller
    {
        private IClientsRepository clientsRepository;
        private IAuth auth;

        public UsersController()
        {
            this.clientsRepository = MvcApplication.GetDR().Get<IClientsRepository>();
            this.auth = MvcApplication.GetDR().Get<IAuth>();
        }

        public UsersController(IClientsRepository clientsRepository, IAuth auth)
        {
            this.clientsRepository = clientsRepository;
            this.auth = auth;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

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

                ModelState.AddModelError("", "Invalid User");
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Users");
        }
    }
}