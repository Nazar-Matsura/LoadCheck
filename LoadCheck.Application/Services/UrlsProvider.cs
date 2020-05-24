using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using LoadCheck.Application.Exceptions;
using LoadCheck.Application.Interfaces;

namespace LoadCheck.Application.Services
{
    public class UrlsProvider : IUrlsProvider
    {
        private const string sitemapPath = "/sitemap.xml";

        private readonly HttpClient _client;
        private readonly ISitemapParser _sitemapParser;

        private readonly int _maxUrlsToTest;
        private readonly int _maxSitemapsToProcess;

        public UrlsProvider(HttpClient client, ISitemapParser sitemapParser, IConfiguration configuration)
        {
            _client = client;
            _sitemapParser = sitemapParser;

            _maxUrlsToTest = configuration.MaxUrlsCount;
            _maxSitemapsToProcess = configuration.MaxSitemapCount;
        }

        public async Task<List<Uri>> GetSitemapFor(Uri uri)
        {
            var sitemaps = await FindSitemapsFor(uri);

            var result = new List<Uri>();

            foreach (var sitemap in sitemaps)
            {
                if (result.Count == _maxUrlsToTest)
                    return result;

                var uris = _sitemapParser.ParseSitemap(sitemap);
                var numberOfUrisToAdd = _maxUrlsToTest - result.Count;
                result.AddRange(uris.Take(numberOfUrisToAdd));
            }

            return result;
        }

        private async Task<List<XElement>> FindSitemapsFor(Uri url)
        {
            var authority = url.GetLeftPart(UriPartial.Authority);
            var sitemapUri = new Uri(authority + sitemapPath);

            XElement doc;
            try
            {
                doc = await GetXmlFromUri(sitemapUri);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("404"))
                    throw new SitemapNotFoundException($"Sitemap or sitemap index not found at {sitemapUri}");
                else
                    throw;
            }
            
            if (IsSitemapIndex(doc))
            {
                return await GetSitemapsFromIndex(doc);
            }

            return new List<XElement> {doc};
        }

        private async Task<List<XElement>> GetSitemapsFromIndex(XElement doc)
        {
            var sitemapUris = GetSitemapUrisFromIndex(doc);

            var tasks = sitemapUris.Select(GetXmlFromUri).ToList();

            var results = await Task.WhenAll(tasks);

            return results.ToList();
        }

        private async Task<XElement> GetXmlFromUri(Uri uri)
        {
            using (var stream = await _client.GetStreamAsync(uri))
            {
                var elem = XElement.Load(stream);
                return elem;
            }
        }

        private List<Uri> GetSitemapUrisFromIndex(XElement elem)
        {
            return elem.Descendants()
                .Where(e => e.Name.LocalName == "loc")
                .Take(_maxSitemapsToProcess)
                .Select(e => new Uri(e.Value))
                .ToList();
        }

        private bool IsSitemapIndex(XElement elem)
        {
            return elem.DescendantsAndSelf().Any(e => e.Name.LocalName == "sitemapindex");
        }
    }
}