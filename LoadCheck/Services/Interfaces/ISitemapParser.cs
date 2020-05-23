using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace LoadCheck.Services.Interfaces
{
    public interface ISitemapParser
    {
        Task<List<object>> ParseSitemaps(List<XmlDocument> xml);
    }
}