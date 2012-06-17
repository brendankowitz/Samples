using System;
using DomainEvents.Events;

namespace DomainEvents.Handlers
{
    public interface IHandleEvent<in T> : IObserver<T> where T : IDomainEvent
    {
    }

    public abstract class HandlerBase
    {
        public virtual void OnError(Exception error)
        {
            throw error;
        }

        public virtual void OnCompleted()
        {

        }
    }

    /// <summary>
    /// Base class for a domain event handler
    /// </summary>
    public abstract class HandlerBase<T> : HandlerBase, IHandleEvent<T> where T : IDomainEvent
    {
        public abstract void OnNext(T value);
    }
}