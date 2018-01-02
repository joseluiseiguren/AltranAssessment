namespace FrontEnd.Controllers.Api
{
    using FrontEnd.Dto;
    using FrontEnd.Interfaces;
    using FrontEnd.Models;
    using System;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web.Http;
    using Infraestructure;

    public class PoliciesController : ApiController
    {
        private IPoliciesRepository policiesRepository;

        /// <summary>
        /// constructor: se le deben inyectar los repositorios con los que va interactuar
        /// </summary>
        public PoliciesController(
                            IPoliciesRepository policiesRepository)
        {
            this.policiesRepository = policiesRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(
                                    string clientId = null,
                                    string policyId = null,
                                    decimal? amountInsured = null,
                                    string email = null,
                                    bool? installmentPayment = null,
                                    DateTime? inceptionDate = null,
                                    [FromUri]PaginadoDTO paginado = null,
                                    [FromUri]OrdenamientoDTO ordenamiento = null)
        {
            //TODO: eliminar
            System.Threading.Thread.Sleep(1500);

            // no se puede hacer un pedido sin paginacion
            if (paginado == null)
            {
                // toma los valores por default del paginado
                paginado = new PaginadoDTO();
            }

            // no se puede hacer un pedido sin ordenamiento o de una columna que no existe
            if (ordenamiento == null ||
                typeof(Policy).GetProperties().Where(p => p.Name.ToUpper() == ordenamiento.ColumnaOrdenamiento.ToUpper()).FirstOrDefault() == null)
            {
                // toma los valores por default del ordenamiento
                ordenamiento = new OrdenamientoDTO();
                ordenamiento.ColumnaOrdenamiento = nameof(Policy.Id);
            }

            // se obtienen las policies del repositorio
            var policies = this.policiesRepository.GetPolicies();

            // filtro por id de cliente (equals)
            if (!string.IsNullOrEmpty(clientId))
            {
                policies = policies.Where(p => p.ClientId.Equals(clientId));
            }

            // filtro por id de policy (equals)
            if (!string.IsNullOrEmpty(policyId))
            {
                policies = policies.Where(p => p.Id.ToUpper().Trim().Equals(policyId.ToUpper().Trim()));
            }

            // filtro por amount insured (equals)
            if (amountInsured != null)
            {
                policies = policies.Where(p => p.AmountInsured.Equals(amountInsured));
            }

            // filtro por installment payment (equals)
            if (installmentPayment != null)
            {
                policies = policies.Where(p => p.InstallmentPayment.Equals(installmentPayment));
            }

            // filtro por email (starts with)
            if (!string.IsNullOrEmpty(email))
            {
                policies = policies.Where(p => p.Email.ToUpper().StartsWith(email.ToUpper()));
            }

            // filtro por inception date (equals)
            if (inceptionDate != null)
            {
                policies = policies.Where(p => p.InceptionDate.Year.Equals(inceptionDate.Value.Year))
                                   .Where(p => p.InceptionDate.Month.Equals(inceptionDate.Value.Month))
                                   .Where(p => p.InceptionDate.Day.Equals(inceptionDate.Value.Day));
            }

            // cantidad de registros (sin paginacion)
            var cantidadRegistros = policies.Count();

            // se aplica el ordenamiento
            policies = policies.OrderBy($"{ordenamiento.ColumnaOrdenamiento} {ordenamiento.GetSentidoOrdenamiento()}");

            // se aplica el paginado
            policies = policies.Skip((int)(paginado.Pagina - 1) * (int)paginado.FilasPorPagina).Take((int)paginado.FilasPorPagina);

            // dto de respuesta
            GenericListDTO<PolicyDTO> resp = new GenericListDTO<PolicyDTO>();
            resp.Lista = policies.ToDto();
            resp.PaginaActual = paginado.Pagina;
            resp.CantidadRegistros = cantidadRegistros;
            resp.CantidadPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / (decimal)paginado.FilasPorPagina);

            return this.Ok(resp);
        }
    }
}
