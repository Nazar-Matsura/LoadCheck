using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class SitemapParser : ISitemapParser
    {
        public async Task<List<object>> ParseSitemaps(List<XmlDocument> xml)
        {
            throw new System.NotImplementedException();
        }
    }
}