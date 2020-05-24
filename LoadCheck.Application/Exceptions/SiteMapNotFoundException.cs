using System;

namespace LoadCheck.Application.Exceptions
{
    public class SitemapNotFoundException : Exception
    {
        public SitemapNotFoundException(string message) : base(message)
        {
        }
    }
}