using System.Diagnostics;
using DomainEvents.Events;

namespace MvcApplication.DomainEvents.Eventing.Events
{
    public class LoggingEvent : LoggableDomainEvent<string>
    {
        public LoggingEvent(string data) : base(data)
        {
            LogMessage = "The welcome email to the user '" + data+ "' has been sent.";
            LogLevel = (int) Events.LogLevel.Info;
            Trace.WriteLine("Done email sent event");
        }
    }
}