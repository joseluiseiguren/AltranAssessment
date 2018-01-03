namespace BackEnd.Filters
{
    using BackEnd.Interfaces;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Ninject;
    using System.Linq;

    /// <summary>
    /// filtro encargado de manejar los accesos a los controlers
    /// </summary>
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// repositorio para buscar los clientes (usuarios) y roles
        /// </summary>
        public IClientsRepository clientsRepository;

        /// <summary>
        /// al construir un atributo se instancia el repositorio mediate el DR
        /// </summary>
        public CustomAuthAttribute()
        {
            this.clientsRepository = WebApiApplication.GetDR().Get<IClientsRepository>();
        }

        /// <summary>
        /// customizacion propia para validar el acceso a los resursos
        /// </summary>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Headers.Contains("user"))
            {
                return false;
            }

            // se busca el usuario en el repositorio
            var user = actionContext.Request.Headers.GetValues("user").ToList()[0];
            
            // se valida que exista el usuario
            var client = this.clientsRepository.GetByName(user);
            if (client == null)
            {
                return false;
            }

            // se valida que el usuario tenga el rol que pide la autorizacion para ingresar a la accion
            var rolesToAccess = this.Roles.Split(',');
            if (rolesToAccess.Where(p => p.ToUpper().Trim().Equals(client.Role.ToUpper().Trim())).FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }
    }
}