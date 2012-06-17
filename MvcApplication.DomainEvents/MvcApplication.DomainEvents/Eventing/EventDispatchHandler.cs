using System;
using System.Collections.Generic;
using DomainEvents;
using DomainEvents.Handlers;
using Autofac;

namespace MvcApplication.DomainEvents.Eventing
{
    /// <summary>
    /// This EventDispatch handler is subscribed too all events, when an event occurs 
    /// the dispatcher will resolve handlers that support a closed generic of IHandleEvent over the concrete event type.
    /// eg. IHandleEvent&gt;LoggingEvent&lt;
    /// </summary>
    public class EventDispatchHandler : IObserver<IDomainEvent>
    {
        private readonly IComponentContext _componentContext;

        public EventDispatchHandler(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public void OnNext(IDomainEvent value)
        {
            var handlerType = typeof(IHandleEvent<>).MakeGenericType(value.GetType()); 
            var allHandlersType = typeof(IEnumerable<>).MakeGenericType(handlerType);
            var handlers = (IEnumerable<object>)_componentContext.Resolve(allHandlersType);
            foreach (dynamic handler in handlers)
            {
                handler.OnNext((dynamic)value);
            }
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }
    }
}