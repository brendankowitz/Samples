using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using DomainEvents;
using MvcApplication.DomainEvents.Eventing;
using MvcApplication.DomainEvents.Persistence;

namespace MvcApplication.DomainEvents.Mvc
{
    public class TransactedActionFilter : IActionFilter
    {
        readonly ITransactionContext _transactionContext;
        private readonly EventDispatchHandler _dispatchHandler;
        private readonly ILocalEventsManager _domainEvents;
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        public TransactedActionFilter(ITransactionContext transactionContext, EventDispatchHandler dispatchHandler, ILocalEventsManager domainEvents)
        {
            if (transactionContext == null) throw new ArgumentNullException("transactionContext");
            _transactionContext = transactionContext;
            _dispatchHandler = dispatchHandler;
            _domainEvents = domainEvents;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _transactionContext.BeginTransaction(IsolationLevel.ReadCommitted);
            _subscriptions.Add(_domainEvents.Subscribe(_dispatchHandler));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _subscriptions.ForEach(d => d.Dispose());

            if (filterContext.Exception != null)
                _transactionContext.SetAbort();

            _transactionContext.EndTransaction();
        }
    }
}