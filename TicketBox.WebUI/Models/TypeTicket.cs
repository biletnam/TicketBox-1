using System.Collections.Generic;

namespace TicketBox.WebUI.Models
{
    public class TypeTicket
    {
        public int TypeTicketId { get; set; }
        public string Name { get; set; }

        //ссылка на список билетов
        public virtual List<Ticket> Tickets { get; set; }
    }
}
