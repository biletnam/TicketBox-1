using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Concrete
{
    public class EFTypeTicketRepository : ITypeTicketRepository
    {
        EFDbContext typeTicketContext = new EFDbContext();

        public IEnumerable<TypeTicket> TypeTickets
        {
            get { return typeTicketContext.TypeTickets; }
        }
    }
}
