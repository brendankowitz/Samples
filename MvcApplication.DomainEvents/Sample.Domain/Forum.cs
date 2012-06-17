using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainEvents;

namespace Sample.Domain
{
    partial class Forum : IDomainEventSource
    {
        public virtual event Action<IDomainEvent> RaiseEvent;
    }
}
