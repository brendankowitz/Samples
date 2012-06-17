using System.Linq;

namespace MvcApplication.DomainEvents.Persistence
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        IQueryable<T> Items { get; }
    }
}