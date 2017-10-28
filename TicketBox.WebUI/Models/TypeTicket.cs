using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketBox.WebUI.Models
{
    public class TypeTicket
    {
        public int TypeTicketId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        //ссылка на список билетов
        public virtual List<Ticket> Tickets { get; set; }
    }
}
