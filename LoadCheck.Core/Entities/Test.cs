using System;
using System.Collections.Generic;

namespace LoadCheck.Core.Entities
{
    public class Test : BaseEntity
    {
        public DateTime TimeOfTest { get; set; }

        public Guid SiteRootId { get; set; }
        public virtual SiteRoot SiteRoot { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}