using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Models;
using LoadCheck.Services.Interfaces;

namespace LoadCheck.Services.Services
{
    public class UrlsChecker : IUrlsChecker
    {
        public async Task<List<UrlResponseTimes>> MeasureResponseTimes(List<object> urls)
        {
            throw new System.NotImplementedException();
        }
    }
}