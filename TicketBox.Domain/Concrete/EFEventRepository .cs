using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Domain.Abstract;
using TicketBox.Domain.Entities;

namespace TicketBox.Domain.Concrete
{
    public class EFEventRepository : IEventRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Event> Events
        {
            get { return context.Events; }
        }
    }
}
