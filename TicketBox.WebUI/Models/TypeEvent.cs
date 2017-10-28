using System.Collections.Generic;

namespace TicketBox.WebUI.Models
{
    public class TypeEvent
    {
        public int TypeEventId { get; set; }
        public string Name { get; set; }

        //ссылка на список мероприятий
        public virtual List<Event> Events { get; set; }
    }
}
