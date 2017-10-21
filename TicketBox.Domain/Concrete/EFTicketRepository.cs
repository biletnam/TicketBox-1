using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Concrete
{
    public class EFTicketRepository : ITicketRepository
    {
        EFDbContext ticketContext = new EFDbContext();

        public IEnumerable<Ticket> Tickets
        {
            get { return ticketContext.Tickets; }
        }
    }
}
