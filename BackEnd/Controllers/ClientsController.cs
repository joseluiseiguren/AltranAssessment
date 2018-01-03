namespace BackEnd.Controllers
{
    using BackEnd.Interfaces;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Dto;
    using Infraestructure;
    using Swashbuckle.Swagger.Annotations;
    using System.Net;
    using BackEnd.Filters;

    /// <summary>
    /// controller para el recurso clients
    /// </summary>
    public class ClientsController : ApiController
    {
        private IClientsRepository clientsRepository;
        private IPoliciesRepository policiesRepository;

        /// <summary>
        /// constructor: se le deben inyectar los repositorios con los que va interactuar
        /// </summary>
        public ClientsController(
                            IClientsRepository clientsRepository,
                            IPoliciesRepository policiesRepository)
        {
            this.clientsRepository = clientsRepository;
            this.policiesRepository = policiesRepository;
        }

        /// <summary>
        /// obtiene un cliente por ID
        /// </summary>
        [HttpGet]
        [CustomAuth(Roles = "users, admin")]
        [ResponseType(typeof(ClientDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult ById(string id)
        {
            var client = this.clientsRepository.GetById(id);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.Ok(client.ToDto());
        }

        /// <summary>
        /// obtiene un cliente por username
        /// </summary>
        [HttpGet]
        [CustomAuth(Roles = "users, admin")]
        [ResponseType(typeof(ClientDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult ByUserName(string username)
        {
            var client = this.clientsRepository.GetByName(username);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.Ok(client.ToDto());
        }

        /// <summary>
        /// obtiene un cliente por policy number
        /// </summary>
        [HttpGet]
        [CustomAuth(Roles = "admin")]
        [ResponseType(typeof(ClientDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult ByPolicyNumber(string policyNumber)
        {
            // se obtiene la id del cliente a partir del policy number
            var policy = this.policiesRepository.GetById(policyNumber);
            if (policy == null)
            {
                return this.NotFound();
            }

            // se obtienen todos los datos del cliente a partir del id previamente obtenido
            var client = this.clientsRepository.GetById(policy.ClientId);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.Ok(client.ToDto());
        }
    }
}
