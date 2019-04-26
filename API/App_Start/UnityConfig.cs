using BusinessLogic.Applications;
using BusinessLogic.Applications.Interfaces;
using Common.Persistence.Interfaces;
using Common.Persistence;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISupplierApplication, SupplierApplication>();
            container.RegisterType<ISupplierPersistence, SupplierPersistence>();

            container.RegisterType<IItemApplication, ItemApplication>();
            container.RegisterType<IItemPersistence, ItemPersistence>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}