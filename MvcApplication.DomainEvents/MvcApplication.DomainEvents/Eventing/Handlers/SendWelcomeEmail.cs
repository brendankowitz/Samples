using System.Diagnostics;
using DomainEvents;
using DomainEvents.Handlers;
using MvcApplication.DomainEvents.Eventing.Events;
using MvcApplication.DomainEvents.Persistence;
using Sample.Domain;

namespace MvcApplication.DomainEvents.Eventing.Handlers
{
    public class SendWelcomeEmail : HandlerBase<EntityCreatedEvent<Forum>>, IHandleEvent<EntityCreatedEvent<Tasks>>
    {
        private readonly IPublishDomainEvent _publisher;
        private readonly IRepository<Tasks> _tasksRepo;

        public SendWelcomeEmail(IPublishDomainEvent publisher, IRepository<Tasks> tasksRepo)
        {
            _publisher = publisher;
            _tasksRepo = tasksRepo;
        }

        public override void OnNext(EntityCreatedEvent<Forum> value)
        {
            //if successful
            _publisher.Publish(new LoggingEvent(value.Data.Name));

            var newTask = new Tasks { Name = "New Task", Forum = value.Data };
            _tasksRepo.Add(newTask);
        }

        public void OnNext(EntityCreatedEvent<Tasks> value)
        {
            Trace.WriteLine("Handle Task created event");
        }

    }
}