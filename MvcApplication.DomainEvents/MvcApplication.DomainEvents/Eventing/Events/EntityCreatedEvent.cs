using DomainEvents.Events;

namespace MvcApplication.DomainEvents.Eventing.Events
{
    public class EntityCreatedEvent<T>:LoggableDomainEvent<T>
    {
        public EntityCreatedEvent(T data)
            : base(data)
        {
            LogMessage = string.Format("Entity was created");
            LogLevel = (int) Events.LogLevel.Info;
        }
    }
}