namespace FrontEnd.Controllers.Api
{
    using FrontEnd.Interfaces;
    using Infraestructure;
    using System.Web.Http;
    using System.Linq;
    using System.Linq.Dynamic;
    using FrontEnd.Models;
    using FrontEnd.Dto;
    using System;

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
                                    string role = null,
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
                typeof(Client).GetProperties().Where(p => p.Name.ToUpper() == ordenamiento.ColumnaOrdenamiento.ToUpper()).FirstOrDefault() == null)
            {
                // toma los valores por default del ordenamiento
                ordenamiento = new OrdenamientoDTO();
                ordenamiento.ColumnaOrdenamiento = nameof(Client.Id);
            }

            // se obtienen los clientes del repositorio
            var clients = this.clientsRepository.GetClients();
            
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

            // cantidad de registros (sin paginacion)
            var cantidadRegistros = clients.Count();

            // se aplica el ordenamiento
            clients = clients.OrderBy($"{ordenamiento.ColumnaOrdenamiento} {ordenamiento.GetSentidoOrdenamiento()}");

            // se aplica el paginado
            clients = clients.Skip((int)(paginado.Pagina - 1) * (int)paginado.FilasPorPagina).Take((int)paginado.FilasPorPagina);
            
            // dto de respuesta
            GenericListDTO<ClientDTO> resp = new GenericListDTO<ClientDTO>();
            resp.Lista = clients.ToDto();
            resp.PaginaActual = paginado.Pagina;
            resp.CantidadRegistros = cantidadRegistros;
            resp.CantidadPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / (decimal)paginado.FilasPorPagina);

            return this.Ok(resp);
        }
    }
}
