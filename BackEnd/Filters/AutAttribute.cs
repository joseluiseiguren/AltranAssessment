using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BackEnd.Filters
{
    /// <summary>
    /// filtro encargado de manejar los errores
    /// </summary>
    public class AutAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string username = HttpContext.Current.User.Identity.Name;
        }
    }
}