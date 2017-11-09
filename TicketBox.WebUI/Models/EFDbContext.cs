using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Hosting;

namespace TicketBox.WebUI.Models
{
    public class SampleInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        // В этом методе можно заполнить таблицу по умолчанию
        protected override void Seed(EFDbContext context)
        {
            List<TypeEvent> typeEvents = new List<TypeEvent>
            {
                new TypeEvent {Name = "Театр"},
                new TypeEvent {Name = "Концерт"},
                new TypeEvent {Name = "Спорт"},
                new TypeEvent {Name = "Детям"},
                new TypeEvent {Name = "Другое"}
            };

            foreach (TypeEvent city in typeEvents)
                context.TypeEvents.Add(city);

            List<TypeTicket> typeTickets = new List<TypeTicket>
            {
                new TypeTicket {Name = "Партер"},
                new TypeTicket {Name = "Балкон"},
                new TypeTicket {Name = "Ложа"},
                new TypeTicket {Name = "Бельэтаж"},
                new TypeTicket {Name = "1 ярус"},
                new TypeTicket {Name = "2 ярус"},
                new TypeTicket {Name = "3 ярус"},
                new TypeTicket {Name = "VIP-место"},
                new TypeTicket {Name = "Концерт"},
                new TypeTicket {Name = "Спорт"},
            };

            foreach (TypeTicket type in typeTickets)
                context.TypeTickets.Add(type);

            List<Event> events = new List<Event>
            {
                new Event{Name="Концерт", Description="Новый концерт", Location="Санкт-Петербург", TimeEvent=DateTime.Now, SpecialEvent = 0, TypeEvent=typeEvents[1], TypeEventID = typeEvents[1].TypeEventId, PathToImage= "concert/concert.jpg" },
                new Event{Name="Театр", Description="Новый театар", Location="Санкт-Петербург", TimeEvent=DateTime.Now, SpecialEvent = 1, TypeEvent=typeEvents[0], TypeEventID = typeEvents[0].TypeEventId, PathToImage= "theater/theater.jpg" },
                new Event{Name="Спорт", Description="Новый спорт", Location="Санкт-Петербург", TimeEvent=DateTime.Now, SpecialEvent = 2, TypeEvent=typeEvents[2], TypeEventID = typeEvents[2].TypeEventId, PathToImage= "sport/sport.png" }
            };            

            List<Ticket> tickets = new List<Ticket>
            {
                new Ticket { Place="2B", Delivery=true, Price = 100M,TypeTicket=typeTickets[0], Event=events[0], TypeTicketID=typeTickets[0].TypeTicketId, EventID=events[0].EventId},
                new Ticket { Place="1B", Delivery=true, Price = 250M,TypeTicket=typeTickets[2], Event=events[0], TypeTicketID=typeTickets[1].TypeTicketId, EventID=events[0].EventId},
                new Ticket { Place="2B", Delivery=true, Price = 177M,TypeTicket=typeTickets[3], Event=events[1], TypeTicketID=typeTickets[0].TypeTicketId, EventID=events[1].EventId},
                new Ticket { Place="1B", Delivery=true, Price = 666M,TypeTicket=typeTickets[1], Event=events[1], TypeTicketID=typeTickets[1].TypeTicketId, EventID=events[1].EventId},
                new Ticket { Place="0", Delivery=false, Price = 777M,TypeTicket=typeTickets[4], Event=events[2], TypeTicketID=typeTickets[2].TypeTicketId, EventID=events[2].EventId},
                new Ticket { Place="0", Delivery=true, Price = 50M,TypeTicket=typeTickets[5], Event=events[2], TypeTicketID=typeTickets[2].TypeTicketId, EventID=events[2].EventId}
            };

            foreach (Ticket t in tickets)
            {                
                context.Tickets.Add(t);
            }

            foreach (Event e in events)
            {
                e.Tickets = tickets.FindAll(x => x.Event == e);
                context.Events.Add(e);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }


    public class EFDbContext : DbContext
    {
        public EFDbContext()
        {
            // Установить новый инициализатор
            Database.SetInitializer(new SampleInitializer());
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TypeEvent> TypeEvents { get; set; }
        public DbSet<TypeTicket> TypeTickets { get; set; }
    }
}
