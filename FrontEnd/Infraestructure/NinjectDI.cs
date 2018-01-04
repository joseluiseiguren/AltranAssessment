﻿namespace FrontEnd.Infraestructure
{
    using FrontEnd.Interfaces;
    using Ninject;
    using Ninject.Modules;

    public class Bindings : NinjectModule
    {
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
            kernel.Bind<IAuth>().To<Security.FormsAuth>().InSingletonScope();
        }
    }
}