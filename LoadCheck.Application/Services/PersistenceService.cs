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
    public class PersistenceService : IPersistenceService
    {
        private readonly IRepository<TestResult> _testResultRepository;

        private readonly IRepository<Test> _testRepository;

        private readonly IRepository<SiteRoot> _siteRootRepository;

        public PersistenceService(IRepository<TestResult> testResultRepository,
            IRepository<Test> testRepository,
            IRepository<SiteRoot> siteRootRepository)
        {
            _testResultRepository = testResultRepository;
            _testRepository = testRepository;
            _siteRootRepository = siteRootRepository;
        }

        public async Task SaveTestResults(Uri uri, List<UrlResponseTimes> results)
        {
            var authority = uri.Authority;

            var site = await FindOrCreateSite(authority);

            var newTest = new Test {SiteRootId = site.Id, TimeOfTest = DateTime.Now};
            await _testRepository.AddOrUpdate(newTest);

            var testResults = results.Select(r => new TestResult
            {
                MaxResponseTime = r.MaxResponseTime,
                MinResponseTime = r.MinResponseTime,
                Url = r.Url.ToString(),
                TestId = newTest.Id
            });

            await _testResultRepository.AddRange(testResults);
        }

        private async Task<SiteRoot> FindOrCreateSite(string authority)
        {
            var result = await _siteRootRepository.GetSingle(sr => sr.Authority == authority);

            if (result == null)
            {
                result = new SiteRoot {Authority = authority};
                await _siteRootRepository.AddOrUpdate(result);
            }

            return result;
        }
    }
}