using System;

namespace LoadCheck.Core.Entities
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}