using System.Diagnostics;
using DomainEvents;
using DomainEvents.Events;
using DomainEvents.Handlers;
using MvcApplication.DomainEvents.Eventing.Events;

namespace MvcApplication.DomainEvents.Eventing.Handlers
{
    public class LoggingHandler : HandlerBase<LoggingEvent>
    {
        public override void OnNext(LoggingEvent value)
        {
            Debug.WriteLine("Logging - Severity:{0} Entry: {1}",((LogLevel)value.LogLevel).ToString(),value.LogMessage);
        }
    }
}