
namespace DomainEvents.Managers
{
    public class DomainEventsManager : LocalEventsManager,IDomainEventsManager
    {
        public ILocalEventsManager SpawnLocal()
        {
            var local = new LocalEventsManager(this);
            return local;
        }

       
      
    }
}