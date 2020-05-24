using System;

namespace LoadCheck.Core.Entities
{
    public class TestResult : BaseEntity
    {
        public string Url { get; set; }

        public long MinResponseTime { get; set; }

        public long MaxResponseTime { get; set; }

        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}