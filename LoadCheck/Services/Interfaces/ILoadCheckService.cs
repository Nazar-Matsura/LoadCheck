using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Models;

namespace LoadCheck.Services.Interfaces
{
    public interface ILoadCheckService
    {
        Task<List<UrlResponseTimes>> CheckLoadAt(Uri url);
    }
}