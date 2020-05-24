using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LoadCheck.Models;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class UrlsChecker : IUrlsChecker
    {
        private readonly HttpClient _httpClient;

        private readonly int _measurementsCount;

        public UrlsChecker(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _measurementsCount = int.Parse(ConfigurationManager.AppSettings["UrlMeasurements"]);
        }

        public async Task<List<UrlResponseTimes>> MeasureResponseTimes(List<Uri> urls)
        {
            var oldCacheHeaderValue = _httpClient.DefaultRequestHeaders.CacheControl;
            _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue {NoCache = true};

            var tasks = urls.Select(MeasureUrl).ToList();
            var result = await Task.WhenAll(tasks);

            _httpClient.DefaultRequestHeaders.CacheControl = oldCacheHeaderValue;

            return result.ToList();
        }

        private async Task<UrlResponseTimes> MeasureUrl(Uri url)
        {
            var measurements = new long[_measurementsCount];
            var stopWatch = new Stopwatch();
            for (int i = 0; i < _measurementsCount; i++)
            {
                stopWatch.Restart();
                await _httpClient.GetAsync(url);
                stopWatch.Stop();
                measurements[i] = stopWatch.ElapsedMilliseconds;
            }

            return new UrlResponseTimes
            {
                MaxResponseTime = measurements.Max(),
                MinResponseTime = measurements.Min(),
                Url = url,
            };
        }
    }
}