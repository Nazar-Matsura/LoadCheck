using System;
using System.Collections.Generic;

namespace LoadCheck.Core.ViewModels
{
    public class TestResults
    {
        public DateTime TestStart { get; set; }

        public List<UrlResponseTimes> Urls { get; set; }
    }
}