using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class SitemapFinder : ISitemapFinder
    {
        private const string sitemapPath = "/sitemap.xml";
        private readonly HttpClient client;

        public SitemapFinder(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<XmlDocument>> FindSitemapsFor(Uri url)
        {
            var authority = url.GetLeftPart(UriPartial.Authority);
            var sitemapUri = new Uri(authority + sitemapPath);

            using (var stream = await client.GetStreamAsync(sitemapUri))
            {
                var doc = new XmlDocument();
                doc.Load(stream);

                if (IsSitemapIndex(doc))
                {
                    return GetSitemapsFromIndex(doc);
                }

                return new List<XmlDocument> {doc};
            }
        }

        private List<XmlDocument> GetSitemapsFromIndex(XmlDocument doc)
        {
            
        }

        private bool IsSitemapIndex(XmlDocument doc)
        {
            return doc.ParentNode?.Name == "sitemapindex";
        }
    }
}