using System.Diagnostics;
using Sample.Domain;

namespace MvcApplication.DomainEvents.Eventing.Events
{
    public class TaskCreatedEvent : EntityCreatedEvent<Tasks>
    {
        public TaskCreatedEvent(Tasks data) : base(data)
        {
            LogMessage = "The task was created: " + data.Name;
            LogLevel = (int)Events.LogLevel.Info;
            Trace.WriteLine("Task created");
        }

        
    }
}