using System.Data.Entity;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TypeEvent> TypeEvents { get; set; }
        public DbSet<TypeTicket> TypeTickets { get; set; }
    }
}
