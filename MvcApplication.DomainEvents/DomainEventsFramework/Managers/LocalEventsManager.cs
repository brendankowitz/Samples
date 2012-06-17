using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Linq;
using DomainEvents.Handlers;

namespace DomainEvents.Managers
{
    using SubscribedList = Dictionary<Tuple<Type, string>, IDisposable>;

    public class LocalEventsManager:ILocalEventsManager
    {
        public LocalEventsManager()
        {
            
        }

        internal LocalEventsManager(DomainEventsManager parent)
        {
            _parent = Subscribe(parent.Subject);
        }

        private Subject<IDomainEvent> _subject=new Subject<IDomainEvent>();

        public void Publish<TEvent>(TEvent evnt) where TEvent:IDomainEvent
        {
            if (HasEnded) EndedMessage();
            Subject.OnNext(evnt);            
        }

        protected bool HasEnded { get; private set; }

        internal Subject<IDomainEvent> Subject
        {
            get { return _subject; }
        }

        public virtual void Dispose()
        {
            if (!HasEnded)
            {
                if (_parent!=null) _parent.Dispose();
                EndSession();
            }
            
            if (Subject!=null)
            {  
                Subject.Dispose();
                foreach(var d in _subscribedTo.Values) d.Dispose();
                _subscribedTo.Clear();
                _subject = null;
            }
        }   
   
        public void EndSession()
        {
            Subject.OnCompleted();
            HasEnded = true;            
        }


        public void EndWithFailure(Exception ex)
        {
            Subject.OnError(ex);
            HasEnded = true;
        }
        

        SubscribedList _subscribedTo= new Dictionary<Tuple<Type, string>, IDisposable>();
        private IDisposable _parent;

        public IDisposable PublishFromEventSource(object source,string eventName)
        {
            return ListenFromSource(new SubscribeEvent(source, eventName));
        }
       
        IDisposable ListenFromSource(SubscribeEvent ev)
        {
         if (HasEnded) EndedMessage();
            var tup = _subscribedTo.Keys.FirstOrDefault(d => d.Item1 == ev.Key.Item1 && d.Item2 == ev.Key.Item2);
            if (tup != null)
            {
                _subscribedTo[tup].Dispose();
            }
            else
            {
                tup = ev.Key;
            }
            var rez = ev.Source.Subscribe(Subject);
            _subscribedTo[tup] = rez;
            return rez;
        }

        public IDisposable PublishFromEventSource(IDomainEventSource source)
        {
            return ListenFromSource(new SubscribeEvent(source));
        }

        public IDisposable Subscribe<T>(IHandleEvent<T> handler) where T : IDomainEvent
          {
              return Subscribe((IObserver<IDomainEvent>) handler);
          }

        public IDisposable Subscribe(IObserver<IDomainEvent> handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");
            if (HasEnded) EndedMessage();
            return Subject.Subscribe(handler);
        }

        void EndedMessage()
        {
            throw new InvalidOperationException("EventManager has finished!");
        }
    }

    internal class SubscribeEvent
    {
       
        public SubscribeEvent(IDomainEventSource source)
        {
             Source=Observable.FromEvent<Action<IDomainEvent>, IDomainEvent>(d => source.RaiseEvent += d,
                                                                     d => source.RaiseEvent -= d);
            Key = new Tuple<Type, string>(source.GetType(), "RaiseEvent");
        }

        public SubscribeEvent(object source,string name)
        {
            var tev = source.GetType().GetEvent(name);
            if (tev == null) throw new ArgumentException("Object doesn't have an event with this name", "eventName");
            var add = tev.GetAddMethod();
            var del = tev.GetRemoveMethod();
            Source= Observable.FromEvent<Action<IDomainEvent>, IDomainEvent>(d => add.Invoke(source, new[] { d }), d => del.Invoke(source, new[] { d })); 
            Key = new Tuple<Type, string>(source.GetType(), name);
        }

        public IObservable<IDomainEvent> Source { get; private set; }

        public Tuple<Type, string> Key { get; private set; }

    }
}