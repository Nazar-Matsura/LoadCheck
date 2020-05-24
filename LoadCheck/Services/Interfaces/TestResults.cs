using System;
using System.Collections.Generic;
using LoadCheck.Models;

namespace LoadCheck.Services.Interfaces
{
    public class TestResults
    {
        public DateTime TestStart { get; set; }

        public List<UrlResponseTimes> Urls { get; set; }
    }
}