using System.Configuration;
using LoadCheck.Application.Interfaces;

namespace LoadCheck.Helpers
{
    // I guess configuration can change in runtime, so we use Manager every time setting is requested.
    public class ConfigurationHelper : IConfiguration
    {
        public int UrlMeasurements => int.Parse(ConfigurationManager.AppSettings["UrlMeasurements"]);

        public int MaxUrlsCount => int.Parse(ConfigurationManager.AppSettings["MaxUrlsCount"]);

        public int MaxSitemapCount => int.Parse(ConfigurationManager.AppSettings["MaxSitemapCount"]);
    }
}