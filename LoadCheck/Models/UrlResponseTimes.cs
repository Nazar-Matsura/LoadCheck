using System;

namespace LoadCheck.Models
{
    public class UrlResponseTimes
    {
        public Uri Url { get; set; }

        public long MinResponseTime { get; set; }

        public long MaxResponseTime { get; set; }
    }
}