namespace BackEnd
{
    using System.Web.Http;

    /// <summary>
    /// configuracion de la webapi
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// registracion de rutas
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
