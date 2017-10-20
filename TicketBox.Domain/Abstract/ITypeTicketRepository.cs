using System.Collections.Generic;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Abstract
{
    public interface ITypeTicketRepository
    {
        IEnumerable<TypeTicket> TypeTickets { get; }
    }
}
