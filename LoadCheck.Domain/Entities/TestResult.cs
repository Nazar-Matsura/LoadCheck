using System;

namespace LoadCheck.Core.Entities
{
    public class TestResult
    {
        public Uri Url { get; set; }

        public int MinResponseTime { get; set; }

        public int MaxResponseTime { get; set; }
    }
}