using System;
using NHibernate;
using NHibernate.Type;
using DomainEvents;

namespace MvcApplication.DomainEvents.Persistence.NHibernate
{
    public class NHibernateDomainEventListener : EmptyInterceptor
    {
        readonly ILocalEventsManager _domainEventStore;

        public NHibernateDomainEventListener(ILocalEventsManager domainEventStore)
        {
            if (domainEventStore == null) throw new ArgumentNullException("domainEventStore");
            _domainEventStore = domainEventStore;
        }
        
        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            SubscribeToDomainEvents(entity);
            return base.OnLoad(entity, id, state, propertyNames, types);
        }

        void SubscribeToDomainEvents(object entity)
        {
            var rde = entity as IDomainEventSource;
            if (rde != null)
                _domainEventStore.PublishFromEventSource(rde);
        }
    }
}
