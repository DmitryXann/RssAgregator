using Microsoft.Practices.Unity;
using RssAgregator.BAL.Interfaces.Services;
using RssAgregator.BAL.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace RssAgregator.WEB
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new Unity.Mvc4.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            var settingsService = new SettingService();
            container.RegisterInstance<ISettingService>(settingsService);

            container.RegisterType<IOnlineRadioService, OnlineRadioService>(new InjectionConstructor(settingsService));

            container.RegisterType<ITemplateService, TemplateService>();
            container.RegisterType<INewsService, NewsService>();
            container.RegisterType<ITranslateService, TranslateService>();
        }
    }
}