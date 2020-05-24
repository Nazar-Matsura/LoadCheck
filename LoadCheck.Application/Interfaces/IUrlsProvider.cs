using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadCheck.Application.Interfaces
{
    public interface IUrlsProvider
    {
        Task<List<Uri>> GetSitemapFor(Uri uri);
    }
}