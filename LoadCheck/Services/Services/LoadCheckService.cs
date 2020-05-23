using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadCheck.Models;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class LoadCheckService : ILoadCheckService
    {
        private readonly ISitemapFinder _sitemapFinder;
        private readonly ISitemapParser _sitemapParser;
        private readonly IUrlsChecker _urlsChecker;

        public LoadCheckService(ISitemapFinder sitemapFinder, ISitemapParser sitemapParser, IUrlsChecker urlsChecker)
        {
            _sitemapFinder = sitemapFinder;
            _sitemapParser = sitemapParser;
            _urlsChecker = urlsChecker;
        }

        public async Task<List<UrlResponseTimes>> CheckLoadAt(Uri url)
        {
            var xml = await _sitemapFinder.FindSitemapsFor(url);

            var urls = await _sitemapParser.ParseSitemaps(xml);

            var responseTimes = await _urlsChecker.MeasureResponseTimes(urls);

            var result = new List<UrlResponseTimes>
            {
                new UrlResponseTimes {MaxResponseTime = 1500, MinResponseTime = 100, Url = new Uri("https://stackoverflow.com")},
                new UrlResponseTimes {MaxResponseTime = 500, MinResponseTime = 100, Url = new Uri("https://stackoverflow.com")},
                new UrlResponseTimes {MaxResponseTime = 700, MinResponseTime = 100, Url = new Uri("https://stackoverflow.com")},
                new UrlResponseTimes {MaxResponseTime = 1100, MinResponseTime = 100, Url = new Uri("https://stackoverflow.com")},
                new UrlResponseTimes {MaxResponseTime = 1200, MinResponseTime = 100, Url = new Uri("https://stackoverflow.com")},
            };

            return result.OrderByDescending(r => r.MaxResponseTime).ToList();
        }
    }
}