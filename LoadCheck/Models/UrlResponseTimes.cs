using System;

namespace LoadCheck.Models
{
    public class UrlResponseTimes
    {
        public Uri Url { get; set; }

        public int MinResponseTime { get; set; }

        public int MaxResponseTime { get; set; }
    }
}