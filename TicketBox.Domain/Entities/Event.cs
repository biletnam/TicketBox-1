using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Location { get; set; }
        public DateTime TimeEvent { get; set; }
        public string Description { get; set; }
        public byte SpecialEvent { get; set; }

        public TypeEvent Type { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Event()
        {
            Tickets = new List<Ticket>();
        }
    }
}
