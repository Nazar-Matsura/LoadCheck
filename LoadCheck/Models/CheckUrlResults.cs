using System.Collections.Generic;

namespace LoadCheck.Models
{
    public class CheckUrlResults
    {
        public IEnumerable<UrlResponseTimes> Data { get; set; }

        public byte[] ChartImage { get; set; }
    }
}