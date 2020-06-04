using System;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.Unchanged;
    }
}