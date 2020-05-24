using System.Collections.Generic;

namespace LoadCheck.Core.ViewModels
{
    public class SiteTestsViewModel
    {
        public string Root { get; set; }

        public List<TestResults> TestResults { get; set; }
    }
}