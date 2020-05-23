using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Models;

namespace LoadCheck.Services.Interfaces
{
    public interface IUrlsChecker
    {
        Task<List<UrlResponseTimes>> MeasureResponseTimes(List<object> urls);
    }
}