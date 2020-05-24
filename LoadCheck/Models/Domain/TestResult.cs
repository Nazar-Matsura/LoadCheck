using System;

namespace LoadCheck.Models.Domain
{
    public class TestResult
    {
        public Uri Url { get; set; }

        public int MinResponseTime { get; set; }

        public int MaxResponseTime { get; set; }
    }
}