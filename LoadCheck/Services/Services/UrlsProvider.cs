using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using LoadCheck.Exceptions;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class UrlsProvider : IUrlsProvider
    {
        private const string sitemapPath = "/sitemap.xml";

        private readonly HttpClient _client;
        private readonly ISitemapParser _sitemapParser;

        public UrlsProvider(HttpClient client, ISitemapParser sitemapParser)
        {
            _client = client;
            _sitemapParser = sitemapParser;
        }

        public async Task<List<Uri>> GetSitemapFor(Uri uri)
        {
            var sitemaps = await FindSitemapsFor(uri);

            var maxUris = int.Parse(ConfigurationManager.AppSettings["MaxUrlsCount"]);

            var result = new List<Uri>();

            foreach (var sitemap in sitemaps)
            {
                if (result.Count == maxUris)
                    return result;

                var uris = _sitemapParser.ParseSitemap(sitemap);
                var numberOfUrisToAdd = maxUris - result.Count;
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
            var maxSiteMaps = int.Parse(ConfigurationManager.AppSettings["MaxSitemapCount"]);

            return elem.Descendants()
                .Where(e => e.Name.LocalName == "loc")
                .Take(maxSiteMaps)
                .Select(e => new Uri(e.Value))
                .ToList();
        }

        private bool IsSitemapIndex(XElement elem)
        {
            return elem.DescendantsAndSelf().Any(e => e.Name.LocalName == "sitemapindex");
        }
    }
}