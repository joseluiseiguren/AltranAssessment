namespace BackEnd.Controllers
{
    using BackEnd.Interfaces;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Dto;
    using System.Collections.Generic;
    using Infraestructure;

    public class PoliciesController : ApiController
    {
        private IClientsRepository clientsRepository;
        private IPoliciesRepository policiesRepository;

        public PoliciesController(
                    IClientsRepository clientsRepository,
                    IPoliciesRepository policiesRepository)
        {
            this.clientsRepository = clientsRepository;
            this.policiesRepository = policiesRepository;
        }

        //[Authorize(Roles = "admin")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<PolicyDTO>))]
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

            /*var config = new AutoMapper.MapperConfiguration(cfg => {
                cfg.CreateMap<Models.Policy, PolicyDTO>();
            });
            AutoMapper.IMapper iMapper = config.CreateMapper();
            List<PolicyDTO> destination = iMapper.Map<List<Models.Policy>, List<PolicyDTO>>(policies);*/

            return this.Ok(policies.ToDto());
        }
    }
}
