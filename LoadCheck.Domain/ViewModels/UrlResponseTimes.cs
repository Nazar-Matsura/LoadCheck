using System;

namespace LoadCheck.Core.ViewModels
{
    public class UrlResponseTimes
    {
        public Uri Url { get; set; }

        public long MinResponseTime { get; set; }

        public long MaxResponseTime { get; set; }
    }
}