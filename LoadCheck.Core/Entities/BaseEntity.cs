using System;

namespace LoadCheck.Core.Entities
{
    public class BaseEntity : IIdentifiable
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
