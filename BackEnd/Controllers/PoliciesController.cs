namespace BackEnd.Controllers
{
    using BackEnd.Interfaces;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Dto;
    using System.Collections.Generic;
    using Infraestructure;
    using Swashbuckle.Swagger.Annotations;
    using System.Net;

    /// <summary>
    /// controller para el recurso policies
    /// </summary>
    public class PoliciesController : ApiController
    {
        private IClientsRepository clientsRepository;
        private IPoliciesRepository policiesRepository;

        /// <summary>
        /// constructor: se le deben inyectar los repositorios con los que va interactuar
        /// </summary>
        public PoliciesController(
                    IClientsRepository clientsRepository,
                    IPoliciesRepository policiesRepository)
        {
            this.clientsRepository = clientsRepository;
            this.policiesRepository = policiesRepository;
        }

        /// <summary>
        /// obtiene una lista de ´policies por username
        /// </summary>
        [HttpGet]
        //[Authorize(Roles = "admin")]
        [ResponseType(typeof(IEnumerable<PolicyDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult ByUserName(string username)
        {
            var client = this.clientsRepository.GetByName(username);
            if (client == null)
            {
                return this.NotFound();
            }

            var policies = this.policiesRepository.GetByClientId(client.Id);
            if (policies == null ||
                policies.Count <= 0)
            {
                return this.NotFound();
            }

            return this.Ok(policies.ToDto());
        }
    }
}
