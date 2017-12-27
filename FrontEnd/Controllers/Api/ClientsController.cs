namespace FrontEnd.Controllers.Api
{
    using FrontEnd.Interfaces;
    using Infraestructure;
    using System.Web.Http;
    using System.Linq;

    public class ClientsController : ApiController
    {
        private IClientsRepository clientsRepository;

        /// <summary>
        /// constructor: se le deben inyectar los repositorios con los que va interactuar
        /// </summary>
        public ClientsController(
                            IClientsRepository clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(
                                    string id = null,
                                    string name = null,
                                    string email = null,
                                    string role = null)
        {
            var clients = this.clientsRepository.GetClients();
            if (clients == null)
            {
                return this.NotFound();
            }

            // filtro por id (equals)
            if (!string.IsNullOrEmpty(id))
            {
                clients = clients.Where(p => p.Id.Equals(id));
            }

            // filtro por nombre (contains)
            if (!string.IsNullOrEmpty(name))
            {
                clients = clients.Where(p => p.Name.ToUpper().Contains(name.ToUpper()));
            }

            // filtro por email (starts with)
            if (!string.IsNullOrEmpty(email))
            {
                clients = clients.Where(p => p.Email.ToUpper().StartsWith(email.ToUpper()));
            }

            // filtro por rol (equals)
            if (!string.IsNullOrEmpty(role))
            {
                clients = clients.Where(p => p.Role.ToUpper().Equals(role.ToUpper()));
            }

            var result = clients.ToList();
            if (result.Count <= 0)
            {
                return this.NotFound();
            }

            return this.Ok(clients.ToDto());
        }
    }
}
