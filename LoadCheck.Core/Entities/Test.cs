using System;

namespace LoadCheck.Core.Entities
{
    public class Test
    {
        public Guid Id { get; set; }

        public DateTime TestStart { get; set; }

        public Guid SiteRootId { get; set; }
    }
}