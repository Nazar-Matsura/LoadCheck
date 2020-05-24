using System;
using System.Collections.Generic;

namespace LoadCheck.Core.Entities
{
    public class SiteRoot : BaseEntity
    {
        public string Authority { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}