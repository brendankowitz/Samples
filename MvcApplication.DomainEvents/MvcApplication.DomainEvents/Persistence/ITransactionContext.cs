using System.Data;

namespace MvcApplication.DomainEvents.Persistence
{
    public interface ITransactionContext
    {
        void BeginTransaction(IsolationLevel isolationLevel);
        void EndTransaction();
        void SetAbort();
    }
}
