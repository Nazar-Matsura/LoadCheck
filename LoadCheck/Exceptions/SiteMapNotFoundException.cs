using System;

namespace LoadCheck.Exceptions
{
    public class SitemapNotFoundException : Exception
    {
        public SitemapNotFoundException(string message) : base(message)
        {
        }
    }
}