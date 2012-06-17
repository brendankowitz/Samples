namespace DomainEvents.Events
{
    public interface ILoggableDomainEvent : IDomainEvent
    {
        string LogMessage { get;  }
        int LogLevel { get; }
    }
}