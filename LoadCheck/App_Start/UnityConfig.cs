using System;
using System.Net.Http;
using LoadCheck.Services.Interfaces;
using LoadCheck.Services.Services;
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
            container.RegisterType<ISitemapFinder, SitemapFinder>();
            container.RegisterType<ISitemapParser, SitemapParser>();
            container.RegisterType<IUrlsChecker, UrlsChecker>();
            container.RegisterType<HttpClient, HttpClient>();
        }
    }
}