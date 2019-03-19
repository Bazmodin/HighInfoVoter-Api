using HighInfoVoter_Api.Services;
using HighInfoVoter_Api.Services.Interfaces;
using HighInfoVoter_Api.Services.Providers;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace HighInfoVoter_Api.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IConfigService, ConfigService>();

            container.RegisterType<IDataProvider, SqlDataProvider>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}