using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LoadCheck.Application.Interfaces;
using LoadCheck.Core.ViewModels;

namespace LoadCheck.Application.Services
{
    public class UrlsChecker : IUrlsChecker
    {
        private readonly HttpClient _httpClient;

        private readonly int _measurementsCount;

        public UrlsChecker(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _measurementsCount = configuration.UrlMeasurements;
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