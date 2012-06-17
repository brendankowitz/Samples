using System;

namespace DomainEvents
{
    public interface IDomainEventsManager:ILocalEventsManager
    {
        /// <summary>
        /// Creates a local domain events manager.
        /// Every domain event received by the local manager will be also sent to the domain manager
        /// </summary>
        /// <returns></returns>
        ILocalEventsManager SpawnLocal();
    }
}