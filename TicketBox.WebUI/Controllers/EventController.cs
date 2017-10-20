using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Concrete;
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
            EFDbContext context = new EFDbContext();

            List<Event> events = new List<Event>
            {
                new Event{Name="Концерт 1", Description="lolals", Location="City", SpecialEvent=0, TimeEvent=DateTime.Now, Type=context.TypeEvents.Find(1) },
                new Event{Name="Театр 2", Description="fgfhbgn", Location="City 2", SpecialEvent=1, TimeEvent=DateTime.Now, Type=context.TypeEvents.Find(2)},
                new Event{Name="Другое 1", Description="прпрпр", Location="City 3", SpecialEvent=56, TimeEvent=DateTime.Now, Type=context.TypeEvents.Find(3)}
            };

            foreach (Event e in events)
                context.Events.Add(e);

            List<Ticket> tickets = new List<Ticket>
            {
                new Ticket{Place="2B", Type=context.TypeTickets.Find(1), Delivery="+", Event=context.Events.Find(1)},
                new Ticket{Place="ghghgf", Type=context.TypeTickets.Find(4), Delivery="-", Event=context.Events.Find(2)},
            };

            foreach (Ticket t in tickets)
                context.Tickets.Add(t);

            context.SaveChanges();

            return View(repository.Events);
        }
    }
}