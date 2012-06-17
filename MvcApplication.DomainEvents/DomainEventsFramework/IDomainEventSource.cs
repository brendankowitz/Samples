using System;

namespace DomainEvents
{
    public interface IDomainEventSource
    {
        event Action<IDomainEvent> RaiseEvent;
    }
}