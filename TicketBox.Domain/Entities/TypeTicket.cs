using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox.Domain.Entities
{
    public class TypeTicket
    {
        public int TypeTicketId { get; set; }
        public string Name { get; set; }

        //ссылка на список билетов
        public virtual List<Ticket> Tickets { get; set; }
    }
}
