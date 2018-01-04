namespace FrontEnd
{
    using System.Web.Http;

    /// <summary>
    /// registracion de rutas de webapi
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// registracion de rutas de webapi
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
