using System;

namespace LoadCheck.Core.Entities
{
    public class TestResult
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public int MinResponseTime { get; set; }

        public int MaxResponseTime { get; set; }

        public Guid TestId { get; set; }
    }
}