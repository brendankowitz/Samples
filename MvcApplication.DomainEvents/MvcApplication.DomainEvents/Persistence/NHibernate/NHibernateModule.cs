using Autofac;
using DomainEvents;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using Sample.Domain;
using NHibernate.ByteCode.LinFu;

namespace MvcApplication.DomainEvents.Persistence.NHibernate
{
    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => ConfigureSessionFactory())
                .As<ISessionFactory>()
                .SingleInstance();

            builder.RegisterType<NHibernateDomainEventListener>()
                .As<IInterceptor>();

            builder.RegisterGeneric(typeof(NHibernateRepository<>))
                .As(typeof(IRepository<>))
                .OnActivated(x =>
                                 {
                                     var _domainEvents = x.Context.Resolve<ILocalEventsManager>();
                                     _domainEvents.PublishFromEventSource((IDomainEventSource)x.Instance);
                                 });

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession(c.Resolve<IInterceptor>()))
                .As<ISession>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NHibernateTransactionContext>()
                .As<ITransactionContext>()
                .InstancePerLifetimeScope();
            
        }

        static ISessionFactory ConfigureSessionFactory()
        {
            var fact = Fluently.Configure()
                                              .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=.\sqlexpress;Initial Catalog=sampleapp;Integrated Security=True"))
                                              .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(Forum).Assembly))
                                              .ExposeConfiguration(c => c.AddXml(Sample.Domain.ConfigurationHelper.ModelMappingXml.ToString()))
                                              .ProxyFactoryFactory(typeof(ProxyFactoryFactory))  // example: can also use Spring or Castle Windsor
                                              .BuildSessionFactory();

            return fact;
        }
    }
}
