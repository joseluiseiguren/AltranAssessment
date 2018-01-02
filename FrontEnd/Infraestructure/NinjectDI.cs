using FrontEnd.Interfaces;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FrontEnd.Infraestructure
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientsRepository>().To<Dal.WebService.ClientsRepository>();
        }
    }
}