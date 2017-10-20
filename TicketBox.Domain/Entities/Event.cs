using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }        
        public string Location { get; set; }
        public DateTime TimeEvent { get; set; }
        public string Description { get; set; }
        public byte SpecialEvent { get; set; }

        //ссылка на тип мероприятия
        public TypeEvent Type { get; set; }
        //ссылка на список билетов
        public virtual List<Ticket> Tickets { get; set; }
    }
}
