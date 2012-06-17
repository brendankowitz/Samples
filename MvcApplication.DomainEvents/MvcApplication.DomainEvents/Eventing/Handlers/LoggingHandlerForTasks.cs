using System.Diagnostics;
using DomainEvents.Handlers;
using MvcApplication.DomainEvents.Eventing.Events;

namespace MvcApplication.DomainEvents.Eventing.Handlers
{
    public class LoggingHandlerForTasks : HandlerBase<TaskCreatedEvent>
    {
        public override void OnNext(TaskCreatedEvent value)
        {
            Debug.WriteLine("LoggingHandlerForTasks - Severity:{0} Entry: {1}", ((LogLevel)value.LogLevel).ToString(), value.LogMessage);
        }
    }
}