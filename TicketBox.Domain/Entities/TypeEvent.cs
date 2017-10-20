using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class TypeEvent
    {
        public int TypeEventId { get; set; }
        public string Name { get; set; }

        //ссылка на список мероприятий
        public virtual List<Event> Events { get; set; }
    }
}
