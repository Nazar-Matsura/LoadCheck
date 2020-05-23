using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace LoadCheck.Services.Interfaces
{
    public interface ISitemapFinder
    {
        Task<List<XmlDocument>> FindSitemapsFor(Uri url);
    }
}