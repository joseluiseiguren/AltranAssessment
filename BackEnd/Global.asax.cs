namespace BackEnd
{
    using System.Web.Http;
    using System.Web.Mvc;
    using NLog;
    using Ninject;
    using System.Reflection;

    /// <summary>
    /// tunning de la app
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// logger de la aplicacion (nlog)
        /// </summary>
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// registracion de filtros
        /// </summary>
        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new Filters.CustomHandleErrorAttribute());
        }

        /// <summary>
        /// configuracion de la webapi en el start
        /// </summary>
        protected void Application_Start()
        {
            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>
        /// dependency resolver para controlers mvc (no webapi)
        /// </summary>
        public static IKernel GetDR()
        {
            IKernel ninjectDependencyResolver;
            ninjectDependencyResolver = new StandardKernel();
            ninjectDependencyResolver.Load(Assembly.GetExecutingAssembly());
            return ninjectDependencyResolver;
        }
    }
}
