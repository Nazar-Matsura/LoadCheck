using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class SitemapParser : ISitemapParser
    {
        public List<Uri> ParseSitemap(XElement xml)
        {
            return xml.Descendants()
                .Where(e => e.Name.LocalName == "loc")
                .Select(e => new Uri(e.Value))
                .ToList();
        }
    }
}