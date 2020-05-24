using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoadCheck.Application.Interfaces;
using LoadCheck.Core.Entities;
using LoadCheck.Core.ViewModels;
using LoadCheck.Infrastructure.Persistence;

namespace LoadCheck.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ISiteRootRepository _siteRootRepository;

        public HistoryService(ISiteRootRepository siteRootRepository)
        {
            _siteRootRepository = siteRootRepository;
        }

        public async Task<List<SiteTestsViewModel>> GetFullHistory()
        {
            var fullAggregates = await _siteRootRepository.GetFullAggregateSiteRoots();

            return fullAggregates
                .ToList()
                .Select(s => new SiteTestsViewModel
                {
                    Root = s.Authority,
                    TestResults = MapTestResults(s),
                })
                .ToList();
        }

        private List<TestResults> MapTestResults(SiteRoot r)
        {
            return r.Tests.Select(t => new TestResults
                {
                    TestStart = t.TimeOfTest,
                    Urls = MapUrls(t),
                })
                .OrderBy(tr => tr.TestStart)
                .ToList();
        }

        private List<UrlResponseTimes> MapUrls(Test test)
        {
            return test.TestResults
                .Select(tr => new UrlResponseTimes
                {
                    MinResponseTime = tr.MinResponseTime,
                    MaxResponseTime = tr.MaxResponseTime,
                    Url = new Uri(tr.Url),
                })
                .OrderByDescending(u => u.MaxResponseTime)
                .ToList();
        }
    }
}