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
        EFDbContext eventContext = new EFDbContext();

        ITypeEventRepository typeEventRepository;

        public IEnumerable<Event> Events
        {           
            get { return eventContext.Events; }
        }
    }
}
