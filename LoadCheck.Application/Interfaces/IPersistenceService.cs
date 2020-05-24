using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadCheck.Core.ViewModels;

namespace LoadCheck.Application.Interfaces
{
    public interface IPersistenceService
    {
        Task SaveTestResults(Uri uri, List<UrlResponseTimes> results);
    }
}