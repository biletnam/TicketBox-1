using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Concrete;
using TicketBox.Domain.Entities;
using TicketBox.WebUI.Models;

namespace TicketBox.WebUI.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository eventRepository;
        private ITypeEventRepository typeEventRepository;
        public int pageSize = 4;

        public EventController(IEventRepository repo, ITypeEventRepository typeEventRepo)
        {
            eventRepository = repo;
            typeEventRepository = typeEventRepo;
        }

        public ViewResult List(string category, int page = 1)
        {
            EFDbContext context = new EFDbContext();

            EventsListViewModel model = new EventsListViewModel
            {
                Events = context.Events
                                    .Where(p => category == null || p.Type.Name == category)
                                    .OrderBy(game => game.EventId)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        context.Events.Count() :
                        context.Events.Where(x => x.Type.Name == category).Count()
                },
                CurrentCategory = context.TypeEvents.FirstOrDefault(x => x.Name == category)

            };
            return View(model);
            
        }
    }
}