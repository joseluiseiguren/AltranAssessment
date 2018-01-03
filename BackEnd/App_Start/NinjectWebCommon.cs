[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BackEnd.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BackEnd.App_Start.NinjectWebCommon), "Stop")]

namespace BackEnd.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    /// <summary>
    /// dependency resolver
    /// </summary>
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        
        /// <summary>
        /// se registran los modulos cuando inicia la aplicacion
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(Ninject.Web.Common.WebHost.OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(Ninject.Web.Common.WebHost.NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// se cierra la aplicacion
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// se crea el kernel encargado de hacer las DI
        /// </summary>
        /// <returns>kernel creado</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// se hacen los bindings de las interfaces
        /// </summary>
        /// <param name="kernel">kernel</param>
        private static void RegisterServices(IKernel kernel)
        {
            BackEnd.Infraestructure.Bindings.Binder(kernel);

        }
    }
}
