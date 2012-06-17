using System;
using DomainEvents;

namespace Sample.Domain
{
    partial class Tasks : IDomainEventSource
    {
        public virtual event Action<IDomainEvent> RaiseEvent;
    }
}
