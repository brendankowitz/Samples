using System.Diagnostics;
using DomainEvents.Handlers;
using MvcApplication.DomainEvents.Eventing.Events;
using Sample.Domain;

namespace MvcApplication.DomainEvents.Eventing.Handlers
{
    public class SendWelcomeEmail2 : HandlerBase<EntityCreatedEvent<Forum>>
    {
        
        public SendWelcomeEmail2()
        {

        }

        public override void OnNext(EntityCreatedEvent<Forum> value)
        {
            Trace.WriteLine("Sending another email");   
        }

    }
}