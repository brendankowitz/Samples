using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Configuration;
using Autofac.Features.Metadata;
using Autofac.Integration.Mvc;
using DomainEvents;
using DomainEvents.Events;
using DomainEvents.Handlers;
using DomainEvents.Managers;
using MvcApplication.DomainEvents.Eventing;
using MvcApplication.DomainEvents.Mvc;
using MvcApplication.DomainEvents.Persistence.NHibernate;
using MvcApplication.DomainEvents.Monitoring.Log4Net;

namespace MvcApplication.DomainEvents
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterModule<ConfigurationSettingsReader>();
            builder.RegisterModule<MvcModule>();
            builder.RegisterModule<Log4NetModule>();
            builder.RegisterModule<NHibernateModule>();

            builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly)
                .AsClosedTypesOf(typeof (DomainEventBase<>))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly)
                .AsClosedTypesOf(typeof(IHandleEvent<>))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.RegisterType<EventDispatchHandler>().AsSelf().InstancePerHttpRequest();

            builder.RegisterType<DomainEventsManager>().As<IDomainEventsManager>().SingleInstance();
            builder.Register(c => c.Resolve<IDomainEventsManager>().SpawnLocal())
                .As<ILocalEventsManager>()
                .As<IPublishDomainEvent>()
                .InstancePerHttpRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}