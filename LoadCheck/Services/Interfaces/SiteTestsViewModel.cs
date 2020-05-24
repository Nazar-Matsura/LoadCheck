using System;
using System.Collections.Generic;

namespace LoadCheck.Services.Interfaces
{
    public class SiteTestsViewModel
    {
        public Uri Root { get; set; }

        public List<TestResults> TestResults { get; set; }
    }
}