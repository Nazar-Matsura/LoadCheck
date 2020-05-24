using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoadCheck.Models;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class LoadCheckService : ILoadCheckService
    {
        private readonly IUrlsProvider _urlsProvider;
        private readonly IUrlsChecker _urlsChecker;

        public LoadCheckService(IUrlsProvider urlsProvider, IUrlsChecker urlsChecker)
        {
            _urlsProvider = urlsProvider;
            _urlsChecker = urlsChecker;
        }

        public async Task<List<UrlResponseTimes>> CheckLoadAt(Uri url)
        {
            Debug.WriteLine($"{url} - get sitemap");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var urls = await _urlsProvider.GetSitemapFor(url);
            Debug.WriteLine($"{url} - {stopWatch.ElapsedMilliseconds}");

            Debug.WriteLine($"{url} - measure urls");
            stopWatch.Restart();
            var result = await _urlsChecker.MeasureResponseTimes(urls);
            Debug.WriteLine($"{url} - {stopWatch.ElapsedMilliseconds}");

            return result.OrderByDescending(r => r.MaxResponseTime).ToList();
        }
    }
}