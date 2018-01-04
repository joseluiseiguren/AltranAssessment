namespace FrontEnd
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// configuracion de rutas
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// registracion de rutas
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Policies",
                url: "Clients/{id}/Policies",
                defaults: new { controller = "Policies", action = "GetByClient", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Clients", action = "Search", id = UrlParameter.Optional }
            );
        }
    }
}
