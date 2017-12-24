namespace BackEnd
{
    using System.Web.Http;
    using System.Web.Mvc;
    using NLog;

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new Filters.CustomHandleErrorAttribute());
        }

        protected void Application_Start()
        {
            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
