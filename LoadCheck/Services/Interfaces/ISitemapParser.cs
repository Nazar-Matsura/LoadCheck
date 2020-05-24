using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LoadCheck.Services.Interfaces
{
    public interface ISitemapParser
    {
        List<Uri> ParseSitemap(XElement xml);
    }
}