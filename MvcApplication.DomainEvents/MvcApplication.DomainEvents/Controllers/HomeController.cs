using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainEvents;
using MvcApplication.DomainEvents.Persistence;
using Sample.Domain;

namespace MvcApplication.DomainEvents.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Forum> _forumRepo;
        private readonly ILocalEventsManager _events;

        public HomeController(IRepository<Forum> forumRepo, ILocalEventsManager events)
        {
            _forumRepo = forumRepo;
            _events = events;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            var f = new Forum();
            f.Name = "Hi";
            _forumRepo.Add(f);

            return View();
        }
    }
}
