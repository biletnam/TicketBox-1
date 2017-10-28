using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketBox.WebUI.Models
{
    public class TypeEvent
    {
        public int TypeEventId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        //ссылка на список мероприятий
        public virtual List<Event> Events { get; set; }
    }
}
