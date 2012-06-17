namespace DomainEvents.Events
{
    /// <summary>
    /// Base class for domain events implementing ILoggableDomainEvent
    /// </summary>
    /// <typeparam name="T">Event Data Type</typeparam>
    public class LoggableDomainEvent<T>:DomainEventBase<T>,ILoggableDomainEvent
    {
        public LoggableDomainEvent(T data) : base(data)
        {
        }

        public string LogMessage { get; set; }
        public int LogLevel { get; set; }
    }   
}