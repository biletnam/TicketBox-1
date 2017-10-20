using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Entities;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository repository;

        public EventController(IEventRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Events);
        }
    }
}