using System;
using System.Collections.Generic;
using System.Data.Entity;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Concrete
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

            context.SaveChanges();

            //List<Event> events = new List<Event>
            //{
            //    new Event{Name="Концерт 1", Description="lolals", Location="City", SpecialEvent=0, TimeEvent=DateTime.Now, Type=context.TypeEvents.Find(1) },
            //    new Event{Name="Театр 2", Description="fgfhbgn", Location="City 2", SpecialEvent=1, TimeEvent=DateTime.Now, Type=context.TypeEvents.Find(2)},
            //    new Event{Name="Другое 1", Description="прпрпр", Location="City 3", SpecialEvent=56, TimeEvent=DateTime.Now, Type=context.TypeEvents.Find(3)}
            //};

            //foreach (Event e in events)
            //    context.Events.Add(e);

            //List<Ticket> tickets = new List<Ticket>
            //{
            //    new Ticket{Place="2B", Type=context.TypeTickets.Find(1), Delivery="+", Event=context.Events.Find(1)},
            //    new Ticket{Place="ghghgf", Type=context.TypeTickets.Find(4), Delivery="-", Event=context.Events.Find(2)},
            //};

            //foreach (Ticket t in tickets)
            //    context.Tickets.Add(t);

            //context.SaveChanges();
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
