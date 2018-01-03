namespace BackEnd.Infraestructure
{
    using BackEnd.Interfaces;
    using Ninject;
    using Ninject.Modules;

    /// <summary>
    /// dependency resolver para objetos (no webapi)
    /// </summary>
    public class Bindings : NinjectModule
    {
        /// <summary>
        /// en el start sse hace el binding
        /// </summary>
        public override void Load()
        {
            Binder(Kernel);
        }

        /// <summary>
        /// se hacen los bindings del DI (es el unico lugar de toda la aplicacion donde se hace el binding)
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void Binder(IKernel kernel)
        {
            kernel.Bind<IClientsRepository>().To<Dal.WebService.ClientsRepository>().InSingletonScope();
            kernel.Bind<IPoliciesRepository>().To<Dal.WebService.PoliciesRepository>().InSingletonScope();
        }
    }
}