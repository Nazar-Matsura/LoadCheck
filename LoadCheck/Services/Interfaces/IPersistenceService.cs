using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Models;

namespace LoadCheck.Services.Interfaces
{
    public interface IPersistenceService
    {
        Task SaveTestResults(Uri uri, List<UrlResponseTimes> results);
    }
}