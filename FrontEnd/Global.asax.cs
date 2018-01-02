namespace FrontEnd
{
    using Ninject;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // dependency resolver para controlers mvc (no webapi)
        public static IKernel GetDR()
        {
            IKernel ninjectDependencyResolver;
            ninjectDependencyResolver = new StandardKernel();
            ninjectDependencyResolver.Load(Assembly.GetExecutingAssembly());
            return ninjectDependencyResolver;
        }
    }
}
