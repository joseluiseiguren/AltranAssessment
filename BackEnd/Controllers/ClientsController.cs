namespace BackEnd.Controllers
{
    using BackEnd.Interfaces;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Dto;
    using Infraestructure;

    public class ClientsController : ApiController
    {
        private IClientsRepository clientsRepository;
        private IPoliciesRepository policiesRepository;

        public ClientsController(
                            IClientsRepository clientsRepository,
                            IPoliciesRepository policiesRepository)
        {
            this.clientsRepository = clientsRepository;
            this.policiesRepository = policiesRepository;
        }

        //[Authorize(Roles = "users,admin")]
        [HttpGet]
        [ResponseType(typeof(ClientDTO))]
        public IHttpActionResult ById(string id)
        {
            var client = this.clientsRepository.GetById(id);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.Ok(client.ToDto());
        }

        //[Authorize(Roles = "users,admin")]
        [HttpGet]
        [ResponseType(typeof(ClientDTO))]
        public IHttpActionResult ByUserName(string username)
        {
            var client = this.clientsRepository.GetByName(username);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.Ok(client.ToDto());
        }

        //[Authorize(Roles = "admin")]
        [HttpGet]
        [ResponseType(typeof(ClientDTO))]
        public IHttpActionResult ByPolicyNumber(string policyNumber)
        {
            var policy = this.policiesRepository.GetById(policyNumber);
            if (policy == null)
            {
                return this.NotFound();
            }

            var client = this.clientsRepository.GetById(policy.ClientId);
            if (client == null)
            {
                return this.NotFound();
            }

            return this.Ok(client.ToDto());
        }
    }
}
