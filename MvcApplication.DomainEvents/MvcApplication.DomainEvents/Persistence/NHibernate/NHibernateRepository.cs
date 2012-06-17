using System;
using System.Linq;
using MvcApplication.DomainEvents.Eventing.Events;
using NHibernate;
using NHibernate.Linq;
using DomainEvents;

namespace MvcApplication.DomainEvents.Persistence.NHibernate
{
    public class NHibernateRepository<T> : IRepository<T>, IDomainEventSource
    {
        readonly ISession _session;

        public NHibernateRepository(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");
            _session = session;
        }

        public void Add(T item)
        {
            _session.Save(item);
            if (RaiseEvent != null) 
                RaiseEvent(new EntityCreatedEvent<T>(item));
        }

        public void Remove(T item)
        {
            _session.Delete(item);
        }

        public IQueryable<T> Items
        {
            get { return _session.Query<T>(); }
        }

        public event Action<IDomainEvent> RaiseEvent;

    }
}