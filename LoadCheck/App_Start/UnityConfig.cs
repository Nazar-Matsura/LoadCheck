using System;
using System.Net.Http;
using LoadCheck.Application.Interfaces;
using LoadCheck.Application.Services;
using LoadCheck.Helpers;
using LoadCheck.Infrastructure.Persistence;
using Unity;

namespace LoadCheck
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<ILoadCheckService, LoadCheckService>();
            container.RegisterType<IUrlsProvider, UrlsProvider>();
            container.RegisterType<ISitemapParser, SitemapParser>();
            container.RegisterType<IUrlsChecker, UrlsChecker>();
            container.RegisterType<IConfiguration, ConfigurationHelper>();

            container.RegisterType<IPersistenceService, PersistenceService>();
            container.RegisterType<IHistoryService, HistoryService>();

            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<ISiteRootRepository, SiteRootRepository>();

            container.RegisterType<HttpClient, HttpClient>();
        }
    }
}