using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TestAPI.Contracts;
using TestAPI.Data;
using TestAPI.Models;
using TestAPI.Repositories;
using Unity.WebApi;

namespace TestAPI
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // register dependency resolver for WebAPI RC
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IDataContext, DataContext>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IViewFactory, ViewFactory>();

            return container;
        }
    }
}