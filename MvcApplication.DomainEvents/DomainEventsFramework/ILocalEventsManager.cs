using System;
using DomainEvents.Handlers;

namespace DomainEvents
{
    public interface ILocalEventsManager : IObservable<IDomainEvent>, IDisposable, IPublishDomainEvent
    {
        /// <summary>
        /// Add handler to process domain events
        /// </summary>
        /// <exception cref="InvalidOperationException">if manager session has ended</exception>
        /// <param name="handler">handler for domain event</param>
        /// <returns></returns>
        IDisposable Subscribe<T>(IHandleEvent<T> handler) where T : IDomainEvent;
        /// <summary>
        /// Signals the manager to end its session.
        /// It basically calls OnComplete method on every handler
        /// </summary>
        void EndSession();
        /// <summary>
        /// Signals the manager to end its session because of an error.
        /// It baiscally calls OnError method on every handler
        /// </summary>
        void EndWithFailure(Exception ex);
        /// <summary>
        /// Automatically publish domain events from specified source
        /// </summary>
        /// <exception cref="InvalidOperationException">if manager session has ended</exception>
        /// <param name="source">instance of object raising domain events</param>
        /// <param name="eventName">name of event property</param>
        /// <returns></returns>
        IDisposable PublishFromEventSource(object source,string eventName);
        /// <summary>
        /// Automatically publish domain events from specified source
        /// </summary>
        /// <exception cref="InvalidOperationException">if manager session has ended</exception>
        /// <param name="source">instance of domain events source</param>
        /// <returns></returns>
        IDisposable PublishFromEventSource(IDomainEventSource source);
    }
}